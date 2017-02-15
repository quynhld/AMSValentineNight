using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;

namespace KN.Web.Settlement.Balance
{
    public partial class AccountList : BasePage
    {
        int intPageNo = CommValue.NUMBER_VALUE_0;

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
                        Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 매개변수 체크
        /// </summary>
        /// <returns></returns>
        protected bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

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
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        /// <summary>
        /// 각 컨트롤 초기화
        /// </summary>
        protected void InitControls()
        {
            // DropDownList Setting
            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlAccount, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT, TextNm["ENTIRE"]);
            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlPayment, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD, TextNm["ENTIRE"]);

            lnkbtnSearch.Text = TextNm["SEARCH"];

            ltClass.Text = TextNm["CLASSI"];
            ltPayMethod.Text = TextNm["PAYMETHOD"];
            ltTerm.Text = TextNm["TERM"];

            lnkbtnPrint.Text = "Report" + " " + TextNm["PRINT"];
            lnkbtnPrint.Visible = Master.isWriteAuthOk;
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + " " + TextNm["PRINT"];
            lnkbtnExcelReport.Visible = Master.isWriteAuthOk;

            txtStartDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtEndDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            if(!hfRentCd.Value.Equals("9000"))
            {
                lnkbtnPrint8.Visible = false;
            }
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            var strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");

            var strEndDt =txtEndDt.Text.Replace("-", "").Replace(".", "");


            // KN_USP_MNG_SELECT_ACCOUNTSINFO_S00
            var dsReturn = BalanceMngBlo.SpreadMngAccountList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), ddlAccount.SelectedValue, strStartDt, strEndDt, Session["LangCd"].ToString(), txtHfRentCd.Text, ddlPayment.SelectedValue);

            if (dsReturn != null)
            {
                lvAccountsList.DataSource = dsReturn.Tables[0];
                lvAccountsList.DataBind();

                if (dsReturn.Tables[1].Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    ltTotalAmtAll.Text = double.Parse(dsReturn.Tables[1].Rows[0]["SellingPrice"].ToString()).ToString("###,##0");
                    
                    ltFeeNETAll.Text = double.Parse(dsReturn.Tables[1].Rows[0]["PrimeCost"].ToString()).ToString("###,##0");

                    ltFeeVATAll.Text = double.Parse(dsReturn.Tables[1].Rows[0]["VATAmount"].ToString()).ToString("###,##0");
                }
                else
                {
                    ltTotalAmtAll.Text = "0";

                    ltFeeNETAll.Text = "0";

                    ltFeeVATAll.Text = "0";
                }


                // spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
            else
            {
                ltTotalAmtAll.Text = "0";

                ltFeeNETAll.Text = "0";

                ltFeeVATAll.Text = "0";
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

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvAccountsList_LayoutCreated(object sender, EventArgs e)
        {
            var ltPaymentDt = (Literal)lvAccountsList.FindControl("ltPaymentDt");
            ltPaymentDt.Text = TextNm["PAYDAY"];            
            var ltKind = (Literal)lvAccountsList.FindControl("ltKind");
            ltKind.Text = TextNm["RENT"];
            var ltName = (Literal)lvAccountsList.FindControl("ltName");
            ltName.Text = TextNm["NAME"];
            var ltPaymentCd = (Literal)lvAccountsList.FindControl("ltPaymentCd");
            ltPaymentCd.Text = TextNm["PAYMETHOD"];
            var ltFeeNet = (Literal)lvAccountsList.FindControl("ltFeeNET");
            ltFeeNet.Text = TextNm["NET"];
            var ltFeeVat = (Literal)lvAccountsList.FindControl("ltFeeVAT");
            ltFeeVat.Text = TextNm["VAT"];
            var ltTotalAmt = (Literal)lvAccountsList.FindControl("ltTotalAmt");
            ltTotalAmt.Text = TextNm["TOTALAMT"];


            string strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            string strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvAccountsList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    var ltPaymentDt = (Literal)e.Item.FindControl("ltPaymentDt");
                    ltPaymentDt.Text = TextNm["PAYDAY"]; 
                    var ltKind = (Literal)e.Item.FindControl("ltKind");
                    ltKind.Text = TextNm["RENT"];
                    var ltName = (Literal)e.Item.FindControl("ltName");
                    ltName.Text = TextNm["NAME"];
                    var ltPaymentCd = (Literal)e.Item.FindControl("ltPaymentCd");
                    ltPaymentCd.Text = TextNm["PAYMETHOD"];
                    var ltFeeNet = (Literal)e.Item.FindControl("ltFeeNET");
                    ltFeeNet.Text = TextNm["NET"];
                    var ltFeeVat = (Literal)e.Item.FindControl("ltFeeVAT");
                    ltFeeVat.Text = TextNm["VAT"];
                    var ltTotalAmt = (Literal)e.Item.FindControl("ltTotalAmt");
                    ltTotalAmt.Text = TextNm["TOTALAMT"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvAccountsList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    var ltInsPaymentDt = (Literal)iTem.FindControl("ltInsPaymentDt");
                    ltInsPaymentDt.Text = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["PaymentDt"].ToString()));
                }

                if (!string.IsNullOrEmpty(drView["SvcYear"].ToString()) && !string.IsNullOrEmpty(drView["SvcMM"].ToString()))
                {
                    var ltInsMonth = (Literal)iTem.FindControl("ltInsMonth");
                    ltInsMonth.Text = drView["SvcYear"].ToString() + "/" + drView["SvcMM"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ItemNm"].ToString()) &&
                    !string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    var ltInsKind = (Literal)iTem.FindControl("ltInsKind");
                    ltInsKind.Text = "<img align=\"absmiddle\" width=\"10\" src=\"/Common/Images/Common/blank.gif\"/>" + TextLib.StringDecoder(drView["ItemNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
                {
                    var ltInsName = (Literal)iTem.FindControl("ltInsName");
                    ltInsName.Text = "(" + TextLib.StringDecoder(drView["RoomNo"].ToString()) + ") " + TextLib.TextCutString(TextLib.StringDecoder(drView["TenantNm"].ToString()), 20, "...");
                }

                if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
                {
                    var ltInsPaymentCd = (Literal)iTem.FindControl("ltInsPaymentNm");
                    ltInsPaymentCd.Text = TextLib.StringDecoder(drView["PaymentNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["PrimeCost"].ToString()))
                {
                    var ltInsFeeNet = (Literal)iTem.FindControl("ltInsFeeNET");
                    ltInsFeeNet.Text = TextLib.MakeVietIntNo(double.Parse(drView["PrimeCost"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["VATAmount"].ToString()))
                {
                    var ltInsFeeVat = (Literal)iTem.FindControl("ltInsFeeVAT");
                    ltInsFeeVat.Text = TextLib.MakeVietIntNo(double.Parse(drView["VATAmount"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["SellingPrice"].ToString()))
                {
                    var ltInsTotalAmt = (Literal)iTem.FindControl("ltInsFeeAmt");
                    ltInsTotalAmt.Text = TextLib.MakeVietIntNo(double.Parse(drView["SellingPrice"].ToString()).ToString("###,##0"));
                }
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
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 엑셀리포트버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    // 세션체크
            //    AuthCheckLib.CheckSession();

            //    var strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            //    var strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

            //    // KN_USP_MNG_SELECT_ACCOUNTSINFO_S01
            //    var dtReturn = BalanceMngBlo.SpreadExcelMngAccountList(ddlAccount.SelectedValue, strStartDt, strEndDt, Session["LangCd"].ToString(), hfRentCd.Value, ddlPayment.SelectedValue);

            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW).Replace("+", " ") + ".xls");
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.ContentEncoding = Encoding.Unicode;
            //    Response.BinaryWrite(Encoding.Unicode.GetPreamble());
            //    this.EnableViewState = false;

            //    var stringWriter = new StringWriter();
            //    var htmlTextWriter = new HtmlTextWriter(stringWriter);

            //    var strTitle = "<p align=center><font size=4 face=Gulim><b>" + Master.TITLE_NOW +"-"+ txtStartDt.Text+ "</b></font></p>";
            //    htmlTextWriter.Write(strTitle);

            //    var gv = new GridView();
            //    gv.Font.Name = "Tahoma";
            //    gv.DataSource = dtReturn;
            //    gv.DataBind();
            //    gv.RenderControl(htmlTextWriter);

            //    Response.Write(stringWriter.ToString());
            //    Response.End();

            //    stringWriter.Flush();
            //    stringWriter.Close();
            //    htmlTextWriter.Flush();
            //    htmlTextWriter.Close();

            //}
            //catch (Exception ex)
            //{
            //    ErrLogger.MakeLogger(ex);
            //}

            try
            {
                //DataSet dtSource = new DataSet();
                // 세션체크
                AuthCheckLib.CheckSession();
                string strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
                string strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");


                // KN_USP_MNG_SELECT_ACCOUNTSINFO_S01
                var dtSource = BalanceMngBlo.SpreadExcelMngAccountList(ddlAccount.SelectedValue, strStartDt, strEndDt, Session["LangCd"].ToString(), hfRentCd.Value, ddlPayment.SelectedValue);

                if (dtSource.Rows.Count <= 0)
                {
                    return;
                }

                var sheetname =  strStartDt + "~" + strEndDt;
                var rentalNm = ddlAccount.SelectedValue == "0000" ? "" : "- " + ddlAccount.SelectedItem + " ";

                var fileName = Server.UrlEncode("Income Transactions ").Replace("+", " ") + rentalNm + txtStartDt.Text + "~" + txtEndDt.Text;

                GenerateExcel(dtSource, sheetname, fileName);
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

               
                ws.SelectedRange[1, 1, 1, 14].Merge = true;
                ws.SelectedRange[2, 1, 2, 14].Merge = true;
                ws.Cells["A1"].Value = fileName;
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Name = "Calibri";
                ws.Cells["A1"].Style.Font.Size = 18;
                ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;       

                pck.Workbook.Properties.Title = @"This code is part of tutorials available";
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A3"].LoadFromDataTable(tbl, true, TableStyles.None);
                ////نمایش جمع ستون هزینه‌های ماه‌ها
                var tbls = ws.Tables[0];

                tbls.ShowTotal = true;
                //Set Sum 
                tbls.Columns[9].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[8].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[7].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A3:N3"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A3:Q3"].AutoFitColumns();
                using (var col = ws.Cells[4, 8, 4 + tbl.Rows.Count, 10])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                using (var col = ws.Cells[4, 4, 4 + tbl.Rows.Count, 4])
                {
                    col.Style.Numberformat.Format = "dd/MM/yyyy";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                
                var stringWriter = new StringWriter();
                var htmlTextWriter = new HtmlTextWriter(stringWriter);
           

                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
        }
    }
}