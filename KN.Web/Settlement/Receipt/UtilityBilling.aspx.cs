using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace KN.Web.Settlement.Receipt
{
    public partial class UtilityBilling : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            try
            {
                if (IsPostBack) return;
                if (CheckParams())
                {
                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["RentCd"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["RentCd"]))
                {
                    hfRentCd.Value = Request.Params["RentCd"];                    
                    isReturnOk = true;
                }
            }
            return isReturnOk;
        }

        protected void InitControls()
        {
            // DropDownList Setting

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnPrint.Text = TextNm["PRINT"];
            lnkbtnPrint.OnClientClick = "javascript:return fnDetailViewJs('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');";
            // 수납 아이템
            MakePaymentDdl();
        }

        private void MakePaymentDdl()
        {
            ddlPayment.Items.Clear();
            ddlPayment.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));
            ddlPayment.Items.Add(new ListItem("Electric & Water & Gas","0003" ));
            ddlPayment.Items.Add(new ListItem("Electric Over Time","0004"));
            ddlPayment.Items.Add(new ListItem("Electric Air-con","0005"));
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            // KN_USP_MNG_SELECT_BILLINGINFO_S02
            var dsReturn = ReceiptMngBlo.GetUtilBillingList(hfRentCd.Value, "", ddlPayment.SelectedValue, "", txtSearchDt.Text.Replace("-", ""),rbIsDebit.SelectedValue);
            if(dsReturn.Tables.Count<=0)
            {
                CloseLoading();
                return;
            }
            lvPaymentList.DataSource = dsReturn.Tables[0];
            lvPaymentList.DataBind();
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPaymentList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
                CloseLoading();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPaymentList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                ltRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["FeeTypeName"].ToString()))
            {
                var ltFeeName = (Literal)iTem.FindControl("ltFeeName");
                ltFeeName.Text = TextLib.StringDecoder(drView["FeeTypeName"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Period"].ToString()))
            {
                var ltPeriod = (Literal)iTem.FindControl("ltPeriod");
                ltPeriod.Text = TextLib.StringDecoder(drView["Period"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
            {
                var ltUserNm = (Literal)iTem.FindControl("ltUserNm");
                ltUserNm.Text = TextLib.TextCutString(TextLib.StringDecoder(drView["UserNm"].ToString()), 45, "..");
            }

            var ltAmountMoney = (Literal)iTem.FindControl("ltAmountMoney");

            if (!string.IsNullOrEmpty(drView["TotalMoney"].ToString()))
            {
                ltAmountMoney.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["TotalMoney"].ToString())).ToString("###,##0"));
            }
            else
            {
                ltAmountMoney.Text = "-";
            }
        }

        /// <summary>
        /// 검색버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnMakeExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                // KN_USP_MNG_SELECT_BILLINGINFO_S02
                var dtReturn = ReceiptMngBlo.GetUtilBillingList(hfRentCd.Value, "", ddlPayment.SelectedValue, "", txtSearchDt.Text.Replace("-", ""), rbIsDebit.SelectedValue);
                if (dtReturn.Tables.Count <= 0)
                {
                    CloseLoading();
                    return;
                }
                
                var fileName = Server.UrlEncode(Master.TITLE_NOW).Replace("+", " ");
                CloseLoading();
                GenerateExcel(dtReturn.Tables[0],fileName,fileName);
               
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        public void GenerateExcel(DataTable tbl, string excelSheetName, string fileName)
        {
            using (var pck = new ExcelPackage())
            {
                //Create the worksheet
                var ws = pck.Workbook.Worksheets.Add(excelSheetName);

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(tbl, true, TableStyles.None);
                var tbls = ws.Tables[0];

                tbls.ShowTotal = true;
                //Set Sum 
                //tbls.Columns[13].TotalsRowFunction = RowFunctions.Sum;
                //Format the header for column 1-3
                using (var rng = ws.Cells["A1:N1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:N1"].AutoFitColumns();
                using (var col = ws.Cells[2, 14, 2 + tbl.Rows.Count, 14])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();               
            }
        }

        protected void CloseLoading()
        {
            var sbWarning = new StringBuilder();
            sbWarning.Append("CloseLoading();");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
    }
}