using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Config.Biz;
using KN.Manage.Biz;
using KN.Settlement.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace KN.Web.Management.Manage
{
    public partial class MergeInvoiceAPT : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (IsPostBack) return;
                InitControls();
                CheckParam();
               // LoadUserInfo();
               // LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params[Master.PARAM_DATA1] == null) return;
            if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1])) return;
            //hfUserSeq.Value = Request.Params[Master.PARAM_DATA1];
            hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
        }
        protected void InitControls()
        {

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            MakeFeeTypeDdl();
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            var feeTyDt = "";
            var feeTy = "";

            if (String.IsNullOrEmpty(ddlFeeType.SelectedValue))
                {
                    feeTy = "";
                    feeTyDt = "";
                }
                else if (ddlFeeType.SelectedValue.Equals("0001"))
                {
                    feeTy = ddlFeeType.SelectedValue;
                    feeTyDt = "";
                }
                else if (ddlFeeType.SelectedValue.Equals("0008"))
                {
                    feeTy = "0011";
                    feeTyDt = "0001";
                }
                else if (ddlFeeType.SelectedValue.Equals("0009"))
                {
                    feeTy = "0011";
                    feeTyDt = "0002";
                }
            // KN_USP_MNG_SELECT_PAYMENTINFO_APT_S01
            var currentDt = txtSearchDt.Text.Replace("-", "");
            var dsReturn = MngPaymentBlo.ListPaymentInfoAptForMerge(hfRentCd.Value, feeTy, currentDt, txtRoomNo.Text, txtCompanyNm.Text, "", feeTyDt);

            lvPaymentList.DataSource = dsReturn;
            lvPaymentList.DataBind();
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

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
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
                var sbWarning = new StringBuilder();
                sbWarning.Append("CloseLoading();");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
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
            if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
            {
                var txthfSeq = (TextBox)iTem.FindControl("txthfSeq");
                txthfSeq.Text = TextLib.StringDecoder(drView["Seq"].ToString());
                var txthfRentCd = (TextBox)iTem.FindControl("txthfRentCd");
                txthfRentCd.Text = TextLib.StringDecoder(drView["RentCd"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoom = (Literal)iTem.FindControl("ltRoom");
                ltRoom.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
            {
                var ltInsName = (Literal)iTem.FindControl("ltInsName");
                ltInsName.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                var ltFeeTy = (Literal)iTem.FindControl("ltFeeTy");
                ltFeeTy.Text = TextLib.StringDecoder(drView["FeeNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["PayDay"].ToString()))
            {
                var ltPayDay = (Literal)iTem.FindControl("ltPayDay");
                ltPayDay.Text = TextLib.MakeDateEightDigit(drView["PayDay"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["ViAmount"].ToString()))
            {
                var ltInsTotalPay = (Literal)iTem.FindControl("ltInsTotalPay");
                ltInsTotalPay.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["ViAmount"].ToString())).ToString("###,##0"));
            }
            var ltInsPaidAmt = (Literal)iTem.FindControl("ltInsPaidAmt");
            ltInsPaidAmt.Text = !string.IsNullOrEmpty(drView["PayAmount"].ToString()) ? TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["PayAmount"].ToString())).ToString("###,##0")) : "0";
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    var isHas = false;
                    var objReturn = new object[2];
                    var refPrintNo = string.Empty;
                    InvoiceMngBlo.UpdatedAptDebitPrintNoForMerge(refPrintNo, "0");
                    foreach (var t in lvPaymentList.Items)
                    {
                        if (((CheckBox)t.FindControl("chkReceitCd")).Checked)
                        {
                            var feeSeq = ((TextBox)t.FindControl("txthfSeq")).Text;
                            if (string.IsNullOrEmpty(refPrintNo))
                            {
                                refPrintNo = feeSeq;
                            }
                            //KN_SCR_UPDATE_DEBIT_PRINTNO_M00
                            objReturn = InvoiceMngBlo.UpdatedAptDebitPrintNoForMerge(refPrintNo, feeSeq);
                            isHas = true;
                        }
                    }
                    
                    if (objReturn == null || !isHas) return;
                    txtHfRefPrintNo.Value = refPrintNo;
                    hfRef_Seq.Value = refPrintNo;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "LoadPopupDebit('" + refPrintNo + "')", CommValue.AUTH_VALUE_TRUE);
                }
                catch (Exception ex)
                {
                    ErrLogger.MakeLogger(ex);
                }            

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }


        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            var dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (var dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING))
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP))
                    {
                        ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void MakeFeeTypeDdl()
        {
            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlFeeType.Items.Clear();

            ddlFeeType.Items.Add(new ListItem("All Fee", ""));

            foreach (var dr in dtReturn.Select())
            {
 
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE))
                    {
                        ddlFeeType.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }                

            }
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ddlPaidCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            LoadData();
        }

        protected void imgUpdateInvoice_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfRef_Seq.Value;
            if (string.IsNullOrEmpty(reft))
            {
                return;
            }
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();
            //KN_USP_INSERT_INVOICE_MERGE_I00
            InvoiceMngBlo.InsertMergeInvoiceHoadonInfoApt(reft, insCompCd, insMemNo, insMemIP); 
            LoadData();
        }

        protected void imgbtnLoadData_Click(object sender, ImageClickEventArgs e)
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
                //Set Sum 
                //tbls.Columns[11].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[10].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[9].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:F1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:F1"].AutoFitColumns();
                //using (var col = ws.Cells[2, 10, 2 + tbl.Rows.Count, 12])
                //{
                //    col.Style.Numberformat.Format = "#,##0";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //using (var col = ws.Cells[2, 5, 2 + tbl.Rows.Count, 5])
                //{
                //    col.Style.Numberformat.Format = "dd/MM/yyyy";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
        }

        protected void lnkExportExcel_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            var feeTyDt = "";
            var feeTy = "";

            if (String.IsNullOrEmpty(ddlFeeType.SelectedValue))
            {
                feeTy = "";
                feeTyDt = "";
            }
            else if (ddlFeeType.SelectedValue.Equals("0001"))
            {
                feeTy = ddlFeeType.SelectedValue;
                feeTyDt = "";
            }
            else if (ddlFeeType.SelectedValue.Equals("0008"))
            {
                feeTy = "0011";
                feeTyDt = "0001";
            }
            else if (ddlFeeType.SelectedValue.Equals("0009"))
            {
                feeTy = "0011";
                feeTyDt = "0002";
            }
            // KN_USP_MNG_SELECT_PAYMENTINFO_APT_S01
            var currentDt = txtSearchDt.Text.Replace("-", "");
            var dsReturn = MngPaymentBlo.ListPaymentInfoAptForMergeExcel(hfRentCd.Value, feeTy, currentDt, txtRoomNo.Text, txtCompanyNm.Text, txtPaidDt.Text.Replace("-", ""), feeTyDt);
            GenerateExcel(dsReturn.Tables[0], "List Merge Invoice - " + ddlFeeType.SelectedItem, "List Merge Invoice");
        }
    }
}
