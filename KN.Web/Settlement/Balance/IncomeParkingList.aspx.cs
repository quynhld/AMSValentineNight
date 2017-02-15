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
using KN.Common.Method.Common;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;

namespace KN.Web.Settlement.Balance
{
    public partial class IncomeParkingList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {                   
                        InitControls();

                        LoadData();
                    
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
            txtHfTotalSend.Text = "0";
            // DropDownList Setting
            //CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlAccount, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT, TextNm["ENTIRE"]);
            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlPayment, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD, TextNm["ENTIRE"]);

            //LoadRentDdl(ddlRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
            
            LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            // 차종 조회
            LoadCarTyDdl(ddlCarTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_CARTY);

            lnkbtnSearch.Text = TextNm["SEARCH"];

            if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APT))
            {
                divReceipt.Visible = CommValue.AUTH_VALUE_TRUE;
                lnkbtnReceiptList.Text = TextNm["RECEIPTLIST"];
            }
            else
            {
                divReceipt.Visible = CommValue.AUTH_VALUE_FALSE;
            }

            
            ltPayMethod.Text = TextNm["PAYMETHOD"];
            ltTerm.Text = TextNm["TERM"];

            lnkbtnPrint.Text = "Report" + " " + TextNm["PRINT"];
            lnkbtnPrint.Visible = Master.isWriteAuthOk;
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + " " + TextNm["PRINT"];
            lnkbtnExcelReport.Visible = Master.isWriteAuthOk;

            txtStartDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtEndDt.Text = DateTime.Now.ToString("s").Substring(0, 10);

            // 내부IP
            //IPHostEntry host = Dns.Resolve(Dns.GetHostName());
            //string strHostIp = host.AddressList[0].ToString();

            //string strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            //string strHostPort = HttpContext.Current.Request.Url.Port.ToString();
            string strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
            string strHostPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString();


            if (!strHostPort.Equals(CommValue.PUBLIC_VALUE_PORT))
            {
                strHostIp = strHostIp + ":" + strHostPort;
            }

            txtIP.Text = strHostIp;
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

            lvAccountsList.DataSource = null;
            lvAccountsList.DataBind();

            DataSet dsReturn = new DataSet();
            dsReturn.Clear();

            // KN_USP_MNG_SELECT_DAILY_INC_PRK_S00
            dsReturn = BalanceMngBlo.SelectMngDailyIncomeParking(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), strStartDt, strEndDt, Session["LangCd"].ToString(), ddlInsRentCd.SelectedValue, ddlPayment.SelectedValue, ddlCarTy.SelectedValue);

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

                if (dsReturn.Tables[2].Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    ltAptTotalAmtAll.Text = double.Parse(dsReturn.Tables[2].Rows[0]["AptSellingPrice"].ToString()).ToString("###,##0");

                    ltAptFeeNETAll.Text = double.Parse(dsReturn.Tables[2].Rows[0]["AptPrimeCost"].ToString()).ToString("###,##0");

                    ltAptFeeVATAll.Text = double.Parse(dsReturn.Tables[2].Rows[0]["AptVATAmount"].ToString()).ToString("###,##0");
                }
                else
                {
                    ltAptTotalAmtAll.Text = "0";

                    ltAptFeeNETAll.Text = "0";

                    ltAptFeeVATAll.Text = "0";
                }


                // spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
            else
            {
                ltTotalAmtAll.Text = "0";

                ltFeeNETAll.Text = "0";

                ltFeeVATAll.Text = "0";

                ltAptTotalAmtAll.Text = "0";

                ltAptFeeNETAll.Text = "0";

                ltAptFeeVATAll.Text = "0";
            }
            txtHfTotalSend.Text = ltTotalAmtAll.Text;
        }

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_SR) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_OFFICE) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_SHOP) )
                {
                    ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        //protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        //{
        //    DataTable dtReturn = new DataTable();

        //    dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

        //    ddlParamNm.Items.Clear();

        //    foreach (DataRow dr in dtReturn.Select())
        //    {
        //        if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING))
        //        {
        //            if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
        //                !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
        //                !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP))
        //            {
        //                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
        //            }
        //        }
        //    }
        //}

        protected void LoadCarTyDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.CARTY_VALUE_FREE_EXCEPTION) &&
                    !dr["CodeCd"].ToString().Equals("0004"))
                {
                    ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
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
            Literal ltPaymentDt = (Literal)lvAccountsList.FindControl("ltPaymentDt");
            ltPaymentDt.Text = TextNm["PAYDAY"];
            Literal ltKind = (Literal)lvAccountsList.FindControl("ltRoomNo");
            ltKind.Text = "Room No";
            Literal ltCarNo = (Literal)lvAccountsList.FindControl("ltCarNo");
            ltCarNo.Text = "Car No";
            Literal ltCarType = (Literal)lvAccountsList.FindControl("ltCarType");
            ltCarType.Text = "Car Type";
            Literal ltPaymentCd = (Literal)lvAccountsList.FindControl("ltPaymentCd");
            ltPaymentCd.Text = TextNm["PAYMETHOD"];
            Literal ltFeeNET = (Literal)lvAccountsList.FindControl("ltFeeNET");
            ltFeeNET.Text = TextNm["NET"];
            Literal ltFeeVAT = (Literal)lvAccountsList.FindControl("ltFeeVAT");
            ltFeeVAT.Text = TextNm["VAT"];
            Literal ltTotalAmt = (Literal)lvAccountsList.FindControl("ltTotalAmt");
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
                    Literal ltPaymentDt = (Literal)lvAccountsList.FindControl("ltPaymentDt");
                    ltPaymentDt.Text = TextNm["PAYDAY"];
                    Literal ltKind = (Literal)lvAccountsList.FindControl("ltRoomNo");
                    ltKind.Text = "Room No";
                    Literal ltCarNo = (Literal)lvAccountsList.FindControl("ltCarNo");
                    ltCarNo.Text = "Car No";
                    Literal ltCarType = (Literal)lvAccountsList.FindControl("ltCarType");
                    ltCarType.Text = "Car Type";
                    Literal ltPaymentCd = (Literal)lvAccountsList.FindControl("ltPaymentCd");
                    ltPaymentCd.Text = TextNm["PAYMETHOD"];
                    Literal ltFeeNET = (Literal)lvAccountsList.FindControl("ltFeeNET");
                    ltFeeNET.Text = TextNm["NET"];
                    Literal ltFeeVAT = (Literal)lvAccountsList.FindControl("ltFeeVAT");
                    ltFeeVAT.Text = TextNm["VAT"];
                    Literal ltTotalAmt = (Literal)lvAccountsList.FindControl("ltTotalAmt");
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
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["PayDt"].ToString()))
                {
                    Literal ltInsPaymentDt = (Literal)iTem.FindControl("ltInsPaymentDt");
                    ltInsPaymentDt.Text = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["PayDt"].ToString()));
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                    ltRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString()) + " (" + drView["ParkingMonth"].ToString().Trim() + ")";
                }

                Literal ltCarNo = (Literal)iTem.FindControl("ltCarNo");                

                if (!string.IsNullOrEmpty(drView["ParkingCarNo"].ToString()) && !string.IsNullOrEmpty(drView["ParkingCardNo"].ToString()))
                {
                    ltCarNo.Text = drView["ParkingCarNo"].ToString() + " (" + drView["ParkingCardNo"].ToString().Trim() + ")";                    
                }

                Literal ltCarType = (Literal)iTem.FindControl("ltCarType");

                if (!string.IsNullOrEmpty(drView["CarTyNm"].ToString()))
                {
                    ltCarType.Text = drView["CarTyNm"].ToString();
                }

                Literal ltInsPaymentNm = (Literal)iTem.FindControl("ltInsPaymentNm");

                if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
                {
                    ltInsPaymentNm.Text = drView["PaymentNm"].ToString();
                }
               

                if (!string.IsNullOrEmpty(drView["PrimeCost"].ToString()))
                {
                    Literal ltInsFeeNET = (Literal)iTem.FindControl("ltInsFeeNET");
                    ltInsFeeNET.Text = TextLib.MakeVietIntNo(double.Parse(drView["PrimeCost"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["VATAmount"].ToString()))
                {
                    Literal ltInsFeeVAT = (Literal)iTem.FindControl("ltInsFeeVAT");
                    ltInsFeeVAT.Text = TextLib.MakeVietIntNo(double.Parse(drView["VATAmount"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["SellingPrice"].ToString()))
                {
                    Literal ltInsTotalAmt = (Literal)iTem.FindControl("ltInsFeeAmt");
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

            try
            {
                //DataSet dtSource = new DataSet();
                // 세션체크
                AuthCheckLib.CheckSession();
                string strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
                string strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

                // KN_USP_RES_SELECT_SALESINFO_S02

                
                //intInit = CommValue.NUMBER_VALUE_0;

                // KN_SCR_SELECT_INVOICE_LIST_EXCEL
                // KN_USP_MNG_SELECT_ACCOUNTSINFO_S01
                var dtSource = BalanceMngBlo.SelectExcelDailyIncomeParking(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), strStartDt, strEndDt, Session["LangCd"].ToString(), ddlInsRentCd.SelectedValue, ddlPayment.SelectedValue, ddlCarTy.SelectedValue);
               
                if (dtSource.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                var CarTy = ddlCarTy.SelectedValue == "" ? "" : "-" + ddlCarTy.SelectedItem.ToString();
                var rentalNm = ddlInsRentCd.SelectedValue == "0000" ? "" : "-" + ddlInsRentCd.SelectedItem.ToString();

                var fileName = Server.UrlEncode("Income Parking List ").Replace("+", " ") + rentalNm + CarTy;

                GenerateExcel(dtSource.Tables[0], fileName, fileName);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

            //try
            //{
            //    // 세션체크
            //    AuthCheckLib.CheckSession();

            //    string strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            //    string strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

            //    DataSet dsReturnExcel = new DataSet();
                
            //    // KN_USP_MNG_SELECT_ACCOUNTSINFO_S01
            //    dsReturnExcel = BalanceMngBlo.SelectExcelDailyIncomeParking(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), strStartDt, strEndDt, Session["LangCd"].ToString(), ddlInsRentCd.SelectedValue, ddlPayment.SelectedValue, ddlCarTy.SelectedValue);

            //    Response.Clear();
            //    Response.Buffer = true;
            //    Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
            //    Response.ContentType = "application/vnd.ms-excel";
            //    Response.Charset = "utf-8";
            //    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            //    this.EnableViewState = false;

            //    StringWriter stringWriter = new StringWriter();
            //    HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            //    string strTitle = "<p align=center><font size=4 face=Gulim><b>" + Master.TITLE_NOW.ToString() + "</b></font></p>";
            //    htmlTextWriter.Write(strTitle);

            //    GridView gv = new GridView();
            //    gv.Font.Name = "Tahoma";
            //    gv.DataSource = dsReturnExcel;
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
        }

        protected void lnkbtnReceiptList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
                string strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

                DataTable dtReturn = new DataTable();

                // KN_USP_MNG_SELECT_RECEIPTLIST_S00
                //dtReturn = BalanceMngBlo.SpreadExcelReceiptList(ddlAccount.SelectedValue, ddlPayment.SelectedValue, strStartDt, strEndDt);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString() + "_" + TextNm["RECEIPTLIST"]).Replace("+", " ") + ".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                this.EnableViewState = false;

                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

                string strTitle = "<p align=center><font size=4 face=Gulim><b>" + Master.TITLE_NOW.ToString() + "_" + TextNm["RECEIPTLIST"] + "</b></font></p>";
                htmlTextWriter.Write(strTitle);

                GridView gv = new GridView();
                gv.Font.Name = "Tahoma";
                gv.DataSource = dtReturn;
                gv.DataBind();
                gv.RenderControl(htmlTextWriter);

                Response.Write(stringWriter.ToString());
                Response.End();

                stringWriter.Flush();
                stringWriter.Close();
                htmlTextWriter.Flush();
                htmlTextWriter.Close();

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
                ////نمایش جمع ستون هزینه‌های ماه‌ها
                var tbls = ws.Tables[0];

                tbls.ShowTotal = true;
                //Set Sum 
                tbls.Columns[12].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[11].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[10].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:R1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:Q1"].AutoFitColumns();
                using (var col = ws.Cells[2, 11, 2 + tbl.Rows.Count, 13])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                using (var col = ws.Cells[2, 2, 2 + tbl.Rows.Count, 2])
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

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnPrint_Click1(object sender, EventArgs e)
        {
            try
            {
                var totalSend = string.Empty;
                var rentCd = string.Empty;

                totalSend = ltAptTotalAmtAll.Text;
                rentCd = ddlInsRentCd.SelectedValue;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnAccountList('" + totalSend + "','" + rentCd + "');", CommValue.AUTH_VALUE_TRUE);
                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}