using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace KN.Web.Management.Remote
{
    public partial class MngReadMonthForRoom : BasePage
    {
        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
                    }
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

            if (!string.IsNullOrEmpty(hfCurrentPage.Value))
            {
                intPageNo = Int32.Parse(hfCurrentPage.Value);
                hfCurrentPage.Value = intPageNo.ToString();
            }
            else
            {
                intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfCurrentPage.Value = intPageNo.ToString();
            }

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
            {
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1];
                txtHfChargeTy.Text = Request.Params["ChargeTy"];

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltRoom.Text = TextNm["ROOMNO"];
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkCreateUtil.Visible = Master.isWriteAuthOk;
            lnkbtnMakeInvoice.Text = TextNm["MAKEBILL"];
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_MNG_SELECT_UTILCHARGEINFO_S07
            var dsReturn = MngPaymentBlo.SelectMngUtilInfo(txtHfRentCd.Text, txtHfChargeTy.Text, txtSearchRoom.Text, "", ddlPrintYN.SelectedValue,txtCompNm.Text,txtSearchDt.Text);
            if (dsReturn == null) return;
            lvDayChargeList.DataSource = dsReturn;
            lvDayChargeList.DataBind();

        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvDayChargeList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvDayChargeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            var ltNo = (Literal)iTem.FindControl("ltNo");

            if (!string.IsNullOrEmpty(drView["SEQ"].ToString()))
            {
                ltNo.Text = drView["SEQ"].ToString();
            }

            var ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                ltRoomNo.Text = drView["RoomNo"].ToString();
            }
            var ltUserNm = (Literal)iTem.FindControl("ltUserNm");

            if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
            {
                ltUserNm.Text = drView["UserNm"].ToString();
            }

            var ltSUsing = (Literal)iTem.FindControl("ltSUsing");

            if (!string.IsNullOrEmpty(drView["StartDate"].ToString()))
            {                 
                ltSUsing.Text = TextLib.MakeDateEightDigit(drView["StartDate"].ToString());
            }

            var ltEUsing = (Literal)iTem.FindControl("ltEUsing");

            if (!string.IsNullOrEmpty(drView["EndDate"].ToString()))
            {                   
                ltEUsing.Text = TextLib.MakeDateEightDigit(drView["EndDate"].ToString());
            }

            var ltFistIndex = (Literal)iTem.FindControl("ltFistIndex");

            if (!string.IsNullOrEmpty(drView["FistIndex"].ToString()))
            {
                ltFistIndex.Text = drView["FistIndex"].ToString();
            }

            var ltEndIndex = (Literal)iTem.FindControl("ltEndIndex");

            if (!string.IsNullOrEmpty(drView["EndIndex"].ToString()))
            {
                ltEndIndex.Text = drView["EndIndex"].ToString();
            }

            var ltNormalUsing = (Literal)iTem.FindControl("ltNormalUsing");

            if (!string.IsNullOrEmpty(drView["NormalUsing"].ToString()))
            {
                ltNormalUsing.Text = TextLib.MakeVietIntNo(double.Parse(drView["NormalUsing"].ToString()).ToString("###,##0.##"));
            }
            else
            {
                ltNormalUsing.Text = CommValue.NUMBER_VALUE_ZERO;
            }

            var ltHeightUsing = (Literal)iTem.FindControl("ltHeightUsing");

            if (!string.IsNullOrEmpty(drView["HightUsing"].ToString()))
            {
                ltHeightUsing.Text = TextLib.MakeVietIntNo(double.Parse(drView["HightUsing"].ToString()).ToString("###,##0.##"));
            }
            else
            {
                ltHeightUsing.Text = CommValue.NUMBER_VALUE_ZERO;
            }

            var ltLowUsing = (Literal)iTem.FindControl("ltLowUsing");

            if (!string.IsNullOrEmpty(drView["LowUsing"].ToString()))
            {
                ltLowUsing.Text = TextLib.MakeVietIntNo(double.Parse(drView["LowUsing"].ToString()).ToString("###,##0.##"));
            }
            else
            {
                ltLowUsing.Text = CommValue.NUMBER_VALUE_ZERO;
            }

            var ltOtherUsing = (Literal)iTem.FindControl("ltOtherUsing");

            if (!string.IsNullOrEmpty(drView["NormalOtherUsing"].ToString()))
            {
                ltOtherUsing.Text = TextLib.MakeVietIntNo(double.Parse(drView["NormalOtherUsing"].ToString()).ToString("###,##0.##"));
            }
            else
            {
                ltOtherUsing.Text = CommValue.NUMBER_VALUE_ZERO;
            }

            var ltDisCount = (Literal)iTem.FindControl("ltDisCount");

            if (!string.IsNullOrEmpty(drView["Discount"].ToString()))
            {
                ltDisCount.Text = TextLib.MakeVietIntNo(double.Parse(drView["Discount"].ToString()).ToString("###,##0.##"));
            }
            else
            {
                ltDisCount.Text = CommValue.NUMBER_VALUE_ZERO;
            }
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>

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

                var dtReturn = MngPaymentBlo.SelectMngUtilInfoExcel(txtHfRentCd.Text, txtHfChargeTy.Text, txtSearchRoom.Text, -2, ddlPrintYN.SelectedValue, txtCompNm.Text, txtSearchDt.Text);
                var fileName = Server.UrlEncode(Master.TITLE_NOW).Replace("+", " ");
                GenerateExcel(dtReturn,fileName,fileName);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDay_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnYear_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_POPUP2 + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkCreateUtil_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect("UtilFeeWrite.aspx?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 페이징버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 상세보기 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var roomNo = txthfRoomNo.Value;
                var chargeSeq = txthfChargeSeq.Value;
                Response.Redirect("UtilFeeWrite.aspx?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text + "&RoomNo=" + roomNo + "&USeq=" + chargeSeq, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnMakeInvoice_Click(object sender, EventArgs e)
        {
        }


        protected void ddlPrintYN_SelectedIndexChanged(object sender, EventArgs e)
        {
           LoadData();
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
                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:M1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:M1"].AutoFitColumns();
                using (var col = ws.Cells[2, 7, 2 + tbl.Rows.Count, 13])
                {
                    col.Style.Numberformat.Format = "#,##0.00";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                using (var col = ws.Cells[2, 3, 2 + tbl.Rows.Count, 6])
                {
                    col.Style.Numberformat.Format = "dd/MM/yyyy";
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
    }
}