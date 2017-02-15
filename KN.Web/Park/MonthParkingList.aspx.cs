using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Parking.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;
using KN.Settlement.Biz;


namespace KN.Web.Park
{
    public partial class MonthParkingList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

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
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (!string.IsNullOrEmpty(hfCurrentPage.Value))
            {
                intPageNo = Int32.Parse(hfCurrentPage.Value);
                hfCurrentPage.Value = intPageNo.ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfCurrentPage.Value = intPageNo.ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltParkingDay.Text = TextNm["PARKINGDAY"];
            ltTotal.Text = TextNm["TOTAL"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnReport.Text = "Report " + TextNm["PRINT"];

            ltInsCarNo.Text = TextNm["CARNO"];
            ltInsCardNo.Text = TextNm["CARDNO"];

            //lnkbtnAccounts.Text = TextNm["ACCOUNTS"];
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            //string strParkingDay = DateTime.Now.ToShortDateString();
            //txtParkingDay.Text = strParkingDay.ToString().Replace("-","");

            lvActDayParkingFeeList.DataSource = null;
            lvActDayParkingFeeList.DataBind();

            DataSet dsReturn = new DataSet();
            dsReturn.Clear();

            //string strStartDt = hfStartDt.Value;
            //string strEndDt = hfEndDt.Value;

            string strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            string strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

            if (strStartDt.Equals(""))
            {
                strStartDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                hfStartDt.Value = DateTime.Now.ToString("s").Substring(0, 10);
            }

            if (strEndDt.Equals(""))
            {
                strEndDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                hfEndDt.Value = DateTime.Now.ToString("s").Substring(0, 10);
            }
            //KN_USP_PRK_SELECT_ACCOUNTDAYPARKINGINFO_S00
            dsReturn = ParkingMngBlo.SpreadAccountDayParkingFeeList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), strStartDt.Replace("-", ""), strEndDt.Replace("-", ""), txtInsCarNo.Text, txtInsCardNo.Text);

            if (dsReturn != null)
            {
                lvActDayParkingFeeList.DataSource = dsReturn.Tables[2];
                lvActDayParkingFeeList.DataBind();

                if (!string.IsNullOrEmpty(dsReturn.Tables[1].Rows[0]["TotalFee"].ToString()))
                {
                    ltTotalFee.Text = TextLib.MakeVietIntNo(double.Parse(dsReturn.Tables[1].Rows[0]["TotalFee"].ToString()).ToString("###,##0"));
                }
                else
                {
                    ltTotalFee.Text = "0";
                }
                if (!string.IsNullOrEmpty(dsReturn.Tables[1].Rows[0]["TotalNET"].ToString()))
                {
                    ltTotalNET.Text = TextLib.MakeVietIntNo(double.Parse(dsReturn.Tables[1].Rows[0]["TotalNET"].ToString()).ToString("###,##0"));
                }
                else
                {
                    ltTotalNET.Text = "0";
                }
                if (!string.IsNullOrEmpty(dsReturn.Tables[1].Rows[0]["TotalVAT"].ToString()))
                {
                    ltTotlaVAT.Text = TextLib.MakeVietIntNo(double.Parse(dsReturn.Tables[1].Rows[0]["TotalVAT"].ToString()).ToString("###,##0"));
                }
                else
                {
                    ltTotlaVAT.Text = "0";
                }

                //sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                //    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                //spanPageNavi.InnerHtml = sbPageNavi.ToString();

                txtStartDt.Text = TextLib.MakeDateEightDigit(strStartDt);
                txtEndDt.Text = TextLib.MakeDateEightDigit(strEndDt);
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvActDayParkingFeeList_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltCarNo = (Literal)lvActDayParkingFeeList.FindControl("ltCarNo");
            ltCarNo.Text = TextNm["CARNO"];
            Literal ltUseTime = (Literal)lvActDayParkingFeeList.FindControl("ltUseTime");
            ltUseTime.Text = TextNm["USETIME"];
            Literal ltCarTy = (Literal)lvActDayParkingFeeList.FindControl("ltCarTy");
            ltCarTy.Text = TextNm["CARTY"];
            Literal ltFee = (Literal)lvActDayParkingFeeList.FindControl("ltFee");
            ltFee.Text = TextNm["AMT"];
            Literal ltMngFeeNET = (Literal)lvActDayParkingFeeList.FindControl("ltMngFeeNET");
            ltMngFeeNET.Text = TextNm["AMT"] + " (" + TextNm["NET"] + ")";
            Literal ltMngFeeVAT = (Literal)lvActDayParkingFeeList.FindControl("ltMngFeeVAT");
            ltMngFeeVAT.Text = TextNm["VAT"];
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

                txtStartDt.Text = TextLib.MakeDateEightDigit(hfStartDt.Value.Replace("-", "").Replace("/", ""));
                txtEndDt.Text = TextLib.MakeDateEightDigit(hfEndDt.Value.Replace("-", "").Replace("/", ""));
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
        protected void lvActDayParkingFeeList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltCarNo")).Text = TextNm["CARNO"];
                    ((Literal)e.Item.FindControl("ltUseTime")).Text = TextNm["USETIME"];
                    ((Literal)e.Item.FindControl("ltCarTy")).Text = TextNm["CARTY"];
                    ((Literal)e.Item.FindControl("ltMngFeeNET")).Text = TextNm["AMT"] + " (" + TextNm["NET"] + ")";
                    ((Literal)e.Item.FindControl("ltMngFeeVAT")).Text = TextNm["VAT"];
                    ((Literal)e.Item.FindControl("ltFee")).Text = TextNm["AMT"];
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvActDayParkingFeeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    TextBox txtHfRentCd = (TextBox)iTem.FindControl("txtHfRentCd");
                    txtHfRentCd.Text = drView["RentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ParkingCardNo"].ToString()))
                {
                    string strCarNo = string.Empty;

                    Literal ltCarNoList = (Literal)iTem.FindControl("ltCarNoList");

                    if (!string.IsNullOrEmpty(drView["ParkingCardNo"].ToString()))
                    {
                        strCarNo = TextLib.StringDecoder(drView["ParkingCarNo"].ToString()) + " (" + drView["ParkingCardNo"].ToString() + ")";
                    }
                    else
                    {
                        strCarNo = TextLib.StringDecoder(drView["ParkingCardNo"].ToString());
                    }

                    ltCarNoList.Text = strCarNo;
                }

                if (!string.IsNullOrEmpty(drView["ParkingStartTime"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["ParkingEndTime"].ToString()))
                    {
                        Literal ltUseTimeList = (Literal)iTem.FindControl("ltUseTimeList");

                        TextBox txtHfStartTime = (TextBox)iTem.FindControl("txtHfStartTime");
                        txtHfStartTime.Text = TextLib.StringDecoder(drView["ParkingStartTime"].ToString());
                        TextBox txtHfEndTime = (TextBox)iTem.FindControl("txtHfEndTime");
                        txtHfEndTime.Text = TextLib.StringDecoder(drView["ParkingEndTime"].ToString());

                        ltUseTimeList.Text = TextLib.StringDecoder(drView["ParkingStartTime"].ToString()) + " ~ " + TextLib.StringDecoder(drView["ParkingEndTime"].ToString());

                        TextBox txtHfVatRatio = (TextBox)iTem.FindControl("txtHfVatRatio");
                        txtHfVatRatio.Text = drView["VatRatio"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(drView["CarTyCd"].ToString()))
                {
                    Literal ltCarTyList = (Literal)iTem.FindControl("ltCarTyList");

                    DataTable dtReturn = new DataTable();

                    dtReturn = CommCdInfo.SelectSubCdWithTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_CARTY);

                    if (dtReturn.Rows.Count > 0)
                    {
                        ltCarTyList.Text = dtReturn.Rows[Int32.Parse(drView["CarTyCd"].ToString())]["CodeNm"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(drView["ParkingFee"].ToString()))
                {
                    Literal ltFeeList = (Literal)iTem.FindControl("ltFeeList");
                    ltFeeList.Text = TextLib.MakeVietIntNo(double.Parse(drView["ParkingFee"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["AccountYn"].ToString()))
                {
                    TextBox txtHfAccountYn = (TextBox)iTem.FindControl("txtHfAccountYn");
                    txtHfAccountYn.Text = drView["AccountYn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
                {
                    TextBox txtHfDongToDollarList = (TextBox)iTem.FindControl("txtHfDongToDollarList");
                    txtHfDongToDollarList.Text = TextLib.MakeVietIntNo(double.Parse(drView["DongToDollar"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["VatRatio"].ToString()))
                {

                    Literal ltInsMngFeeVAT = (Literal)iTem.FindControl("ltInsMngFeeVAT");
                    string strFeeVAT = drView["VatRatio"].ToString();
                    ltInsMngFeeVAT.Text = TextLib.MakeVietIntNo((double.Parse(drView["ParkingFee"].ToString()) * double.Parse(strFeeVAT) / (100 + double.Parse(strFeeVAT))).ToString("###,##0"));

                    Literal ltInsMngFeeNET = (Literal)iTem.FindControl("ltInsMngFeeNET");
                    ltInsMngFeeNET.Text = TextLib.MakeVietIntNo((double.Parse(drView["ParkingFee"].ToString()) - double.Parse(drView["ParkingFee"].ToString()) * double.Parse(strFeeVAT) / (100 + double.Parse(strFeeVAT))).ToString("###,##0"));
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
                // 세션체크
                AuthCheckLib.CheckSession();

                LoadData();

                txtStartDt.Text = TextLib.MakeDateEightDigit(hfStartDt.Value.Replace("-", "").Replace("/", ""));
                txtEndDt.Text = TextLib.MakeDateEightDigit(hfEndDt.Value.Replace("-", "").Replace("/", ""));
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
                //tbls.Columns[12].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[11].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[10].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:E1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:Q1"].AutoFitColumns();
                using (var col = ws.Cells[2, 1, 2 + tbl.Rows.Count, 5])
                {
                    //col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                using (var col = ws.Cells[2, 3, 2 + tbl.Rows.Count, 4])
                {
                    col.Style.Numberformat.Format = "dd/MM/yyyy hh:mm:ss";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
        }

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
                //var dtSource = BalanceMngBlo.SelectExcelDailyIncomeParking(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), strStartDt, strEndDt, Session["LangCd"].ToString(), "0000", "", "0000");
                var dtSource = ParkingMngBlo.SelectExcelAccountDayParkingFeeList(strStartDt.Replace("-", ""), strEndDt.Replace("-", ""), txtInsCarNo.Text, txtInsCardNo.Text);

                if (dtSource.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                var CarNo = txtCarNo.Text == "" ? "" : "-" + txtCarNo.Text;
                var CardNo = txtInsCardNo.Text == "" ? "" : "-" + txtInsCardNo.Text;               

                var fileName = Server.UrlEncode("Daily Parking List ").Replace("+", " ") + CarNo ;

                GenerateExcel(dtSource.Tables[0], fileName, fileName);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        

        /*
        protected void lnkbtnAccounts_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                for (int intTmpI = 0; intTmpI < lvActDayParkingFeeList.Items.Count; intTmpI++)
                {
                    TextBox txtHfAccountYn = (TextBox)lvActDayParkingFeeList.Items[intTmpI].FindControl("txtHfAccountYn");

                    string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                    string strDirectCd = CommValue.DIRECT_TYPE_VALUE_DIRECT;
                    string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    TextBox txtHfRentCd = (TextBox)lvActDayParkingFeeList.Items[intTmpI].FindControl("txtHfRentCd");
                    string strItemCd = CommValue.ITEM_TYPE_VALUE_PARKINGFEE;
                    TextBox txtHfDongToDollarList = (TextBox)lvActDayParkingFeeList.Items[intTmpI].FindControl("txtHfDongToDollarList");
                    TextBox txtHfPayDtList = (TextBox)lvActDayParkingFeeList.Items[intTmpI].FindControl("txtHfEndTime");
                    string strPayDtList = (txtHfPayDtList.Text).Replace("-", "");

                    Literal ltFeeList = (Literal)lvActDayParkingFeeList.Items[intTmpI].FindControl("ltFeeList");
                    string strPaymentCd = CommValue.PAYMENT_TYPE_VALUE_CASH;

                    TextBox txtHfStartTime = (TextBox)lvActDayParkingFeeList.Items[intTmpI].FindControl("txtHfStartTime");
                    TextBox txtHfEndTime = (TextBox)lvActDayParkingFeeList.Items[intTmpI].FindControl("txtHfEndTime");

                    TextBox txtHfVatRatio = (TextBox)lvActDayParkingFeeList.Items[intTmpI].FindControl("txtHfVatRatio");


                    Literal ltCarNoList = (Literal)lvActDayParkingFeeList.Items[intTmpI].FindControl("ltCarNoList");

                    double dblItemTotViAmt = 0d;
                    double dblItemTotEmAmt = 0d;
                    double dblDongToDollar = 0d;
                    double dblUniPrime = 0d;
                    double dblVatRatio = 0d;

                    string strCarNo = string.Empty;

                    if (!string.IsNullOrEmpty(ltCarNoList.Text))
                    {
                        strCarNo = ltCarNoList.Text;
                    }

                    if (txtHfAccountYn.Text.Equals(CommValue.CHOICE_VALUE_NO))
                    {
                        if (!string.IsNullOrEmpty(ltFeeList.Text) && !string.IsNullOrEmpty(txtHfDongToDollarList.Text))
                        {
                            dblDongToDollar = double.Parse(txtHfDongToDollarList.Text);
                            dblItemTotViAmt = double.Parse(ltFeeList.Text);

                            if (dblDongToDollar > 0d)
                            {
                                dblItemTotEmAmt = dblItemTotViAmt / dblDongToDollar;
                            }
                        }

                        if (!string.IsNullOrEmpty(txtHfPayDtList.Text))
                        {
                            dblVatRatio = double.Parse(txtHfPayDtList.Text);
                            dblUniPrime = dblUniPrime * (100 - dblVatRatio) / 100;
                        }

                        DataTable dtAccnt = MngAccountsBlo.RegistryAccountsInfo(strDebitCreditCd, txtHfPayDtList.Text, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text, strDirectCd, strItemCd,
                            CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, string.Empty, string.Empty, string.Empty, string.Empty, dblDongToDollar,
                            dblItemTotEmAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio, string.Empty, string.Empty, Session["MemNo"].ToString(), strInsMemIP);

                        if (dtAccnt != null)
                        {
                            if (dtAccnt.Rows.Count > 0)
                            {
                                int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                                int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                                MngAccountsBlo.RegistryAccountsDetInfo(strDebitCreditCd, txtHfPayDtList.Text, intPaymentSeq, CommValue.NUMBER_VALUE_0
                                    , txtHfRentCd.Text, strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty
                                    , string.Empty, CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblItemTotViAmt
                                    , dblItemTotViAmt, CommValue.NUMBER_VALUE_0, strPayDtList.Substring(0, 4), strPayDtList.Substring(4, 2), strCarNo, dblVatRatio
                                    , CommValue.CONCLUSION_TYPE_TEXT_YES, Session["MemNo"].ToString(), strInsMemIP);
                            }
                        }

                        ParkingFeeBlo.ModifyAccountDayParkingFeeInfo(txtHfRentCd.Text, strCarNo, txtHfStartTime.Text);
                    }
                }


                StringBuilder sbView = new StringBuilder();

                sbView.Append(Master.PAGE_LIST);

                Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        */
    }
}