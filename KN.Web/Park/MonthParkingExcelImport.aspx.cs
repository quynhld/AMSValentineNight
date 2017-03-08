using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;

using KN.Parking.Biz;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Board;
using KN.Settlement.Biz;
using KN.Config.Biz;
using System.Text;
using KN.Common.Method.Lib;
using System.IO;
using System.Configuration;


namespace KN.Web.Park
{
    public partial class MonthParkingExcelImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void importExcel(string cartype,string roomno,string carID,string cardID)
        {
            try
            {
                //LicensePlate_CarCard_RoomNumber_Price_PaymentMethod
                DataTable dtExchangeRate = new DataTable();
                TextBox hfRealBaseRate = new TextBox();
                Literal ltRealBaseRate = new Literal();
                HiddenField hfStartDt = new HiddenField();
                string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                hfStartDt.Value = strNowDt;
                dtExchangeRate = ExchangeMngBlo.WatchExchangeRateLastInfo(CommValue.RENTAL_VALUE_PARKING);
                string strEndDt = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month)).ToString(); //dtEndDate.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue)).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                if (dtExchangeRate != null)
                {
                    if (dtExchangeRate.Rows.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(dtExchangeRate.Rows[0]["DongToDollar"].ToString()))
                        {
                            string strDong = dtExchangeRate.Rows[0]["DongToDollar"].ToString();
                            //ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0") + "&nbsp;" + TextNm["DONG"].ToString());
                            ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0") + "&nbsp;") ;//+ TextNm["DONG"].ToString());
                            hfRealBaseRate.Text = dtExchangeRate.Rows[0]["DongToDollar"].ToString();
                        }
                        else
                        {
                            ltRealBaseRate.Text = "-";
                        }
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                    }
                }

                #region define abc
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                string strDirectCd = CommValue.DIRECT_TYPE_VALUE_DIRECT;
                string strItemCd = CommValue.ITEM_TYPE_VALUE_PARKINGCARDFEE;
                string strParkingItemCd = CommValue.ITEM_TYPE_VALUE_PARKINGFEE;
                string strCardFee = string.Empty;
                string strCarTy = cartype;
                string strDuringMonth = string.Empty;// default 1
                string strStartDt = string.Empty; // default from fist day of month
                string strPaymentCd = string.Empty; // read from excel file
                string strCardNo = cardID; // read from excel file
                string strTagNo = string.Empty; // get from db ParkingTagListInfo
                string strVatRatio = string.Empty;
                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;
                string paymentDT = string.Empty;// default get from input day
                string strCardCost = string.Empty;
                DataTable dtUser = ParkingMngBlo.SelectUserSeqByRoomNo(roomno);
                string strUserSeq = dtUser.Rows[0]["UserSeq"].ToString();
                string strFloorNo = dtUser.Rows[0]["FloorNo"].ToString();
                string rentCD = dtUser.Rows[0]["RentCd"].ToString();
                //var bankcd = Int32.Parse(ddlTransfer.SelectedValue != "" ? ddlTransfer.SelectedValue : "0");
                //var dbCardCost = double.Parse(txtCardFee.Text != "" ? txtCardFee.Text : "0");

                int intGateCnt = CommValue.NUMBER_VALUE_1;
                //phí thẻ : lưu ý và bổ xung vào file excel
                strCardFee = "0";

                #endregion
                #region get card tagno
                if (string.IsNullOrEmpty(strTagNo) && !string.IsNullOrEmpty(strCardNo))
                {
                    // KN_USP_PRK_SELECT_PARKINGTAGLISTINFO_S02 
                    DataTable dtTagReturn = ParkingMngBlo.WatchExgistParkingTagListInfo(strCardNo, strCarTy);
                    if (dtTagReturn != null)
                    {
                        if (dtTagReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            strTagNo = dtTagReturn.Rows[0]["TagNo"].ToString();
                        }
                    }
                }
                #endregion
                #region create new car regist info
                // có đủ thông tin, bắt đầu check chi tiết và insert vào các bảng
                if (!string.IsNullOrEmpty(strCardNo) && !string.IsNullOrEmpty(strTagNo) && !string.IsNullOrEmpty(carID))
                {
                    DateTime dtNowDate = DateTime.Now ;
                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S03
                    DataTable dtReturn = ParkingMngBlo.SpreadExgistParkingCardInfo(strCardNo, carID);
                    DataTable dtUserParkingInfo = new DataTable();
                    //if exist - raise alert
                    if (dtReturn.Rows.Count > 0)
                    {
                    }
                    else
                    {
                        //1. chua co thong tin thi insert vao bang userparkinginfo, update parkingtaglistinfo thanh issued - yes - co 1 list the san
                        // KN_USP_PRK_INSERT_USERPARKINGINFO_M00
                        #region insert part 1 KN_USP_PRK_INSERT_USERPARKINGINFO_M00
                        dtUserParkingInfo = ParkingMngBlo.RegistryUserParkingInfo(strUserSeq, strTagNo, strCardNo
                            , carID, "002", "001", Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, roomno);
                        if (dtUserParkingInfo.Rows.Count > 0)
                        {
                            //string strNowDay = hfStartDt.Value.Replace("-", "").Substring(6, 2);
                            string strNowDay = DateTime.Now.Date.ToString().Replace("-", "").Substring(6, 2);
                            string strEndDay = string.Empty;
                            //string strInsDt = hfStartDt.Value.Replace("-", "");
                            string strInsDt = DateTime.Now.ToString().Replace("-", "");
                            double dblParkingFee = CommValue.NUMBER_VALUE_0_0;//read from excel
                            double dblMonthlyFee = CommValue.NUMBER_VALUE_0_0;
                            double dblPayedFee = CommValue.NUMBER_VALUE_0_0;
                            int intLoopCnt = 1;// CommValue.NUMBER_VALUE_0;
                            // KN_USP_MNG_SELECT_VATINFO_S00
                            DataTable dtVatRatio = VatMngBlo.WatchVatInfo(CommValue.ITEM_TYPE_VALUE_PARKINGFEE);
                            DataTable dtPrintOut = new DataTable();
                            DataTable dtLedgerDet = new DataTable();
                            //if (!string.IsNullOrEmpty(txtParkingFee.Text))
                            //    dblParkingFee = double.Parse(txtParkingFee.Text.Replace(",", ""));
                            //if (!string.IsNullOrEmpty(txtHfMonthlyFee.Text))
                            //    dblMonthlyFee = double.Parse(txtHfMonthlyFee.Text);
                            //
                            double dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                            double dblItemTotViAmt = CommValue.NUMBER_VALUE_0_0;
                            double dblDongToDollar = CommValue.NUMBER_VALUE_0_0;
                            double dblUniPrime = CommValue.NUMBER_VALUE_0_0;
                            double dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                            int intPaymentSeq = CommValue.NUMBER_VALUE_0;
                            int intPaymentDetSeq = CommValue.NUMBER_VALUE_0;
                            int intItemSeq = CommValue.NUMBER_VALUE_0;
                            #region temp
                            if (!string.IsNullOrEmpty(hfRealBaseRate.Text) && !string.IsNullOrEmpty(strCardFee))
                            {
                                dblDongToDollar = double.Parse(hfRealBaseRate.Text);
                                dblItemTotViAmt = double.Parse(strCardFee) + dblParkingFee;

                                if (dblDongToDollar > 0d)
                                    dblItemTotEnAmt = dblItemTotViAmt / dblDongToDollar;
                            }
                            if (dtVatRatio != null)
                            {
                                if (dtVatRatio.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    strVatRatio = dtVatRatio.Rows[0]["VatRatio"].ToString();
                                    dblVatRatio = double.Parse(strVatRatio);
                                    dblUniPrime = dblItemTotViAmt * (100) / (100 + dblVatRatio);
                                }
                                else
                                {
                                    dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                    dblUniPrime = dblItemTotViAmt;
                                }
                            }
                            else
                            {
                                dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                dblUniPrime = dblItemTotViAmt;
                            }
                            #endregion
                            string strPaymentDt = DateTime.Now.ToString().Replace("-", "").Replace(".", ""); //txtPayDt.Text.Replace("-", "").Replace(".", "");
                        #endregion
                            // 2. insert  LedgerInfo
                            #region Insert part 2 KN_USP_SET_INSERT_LEDGERINFO_S00
                            // KN_USP_SET_INSERT_LEDGERINFO_S00
                            DataTable dtLedgerAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strPaymentDt, CommValue.NUMBER_VALUE_0,rentCD , strDirectCd, strParkingItemCd,
                                                                                       CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, strUserSeq, string.Empty,
                                                                                       dblDongToDollar, dblItemTotEnAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio,
                                                                                       Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                            #endregion
                            if (dtLedgerAccnt != null)
                            {
                                if (dtLedgerAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["PaymentSeq"].ToString());
                                    intItemSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["ItemSeq"].ToString());

                                    if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                                    {
                                        // KN_USP_SET_INSERT_LEDGERINFO_S01
                                        BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, "0");
                                    }
                                }
                            }
                            // lap theo tung thang insert chi tiet tung thang
                            #region tien nop chi tiet tung thang
                            for (int intTmpI = 0; intTmpI < intLoopCnt; intTmpI++)
                            {
                                if (intTmpI != CommValue.NUMBER_VALUE_0)
                                {
                                    dtNowDate = DateTime.ParseExact((TextLib.MakeDateEightDigit(hfStartDt.Value.Replace("-", ""))).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);
                                    dtNowDate = dtNowDate.AddMonths(intTmpI);
                                    strStartDt = dtNowDate.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                    strCardFee = CommValue.NUMBER_VALUE_ZERO;
                                    if (dblParkingFee > dblMonthlyFee)
                                    {
                                        dblParkingFee = dblParkingFee - dblMonthlyFee;
                                        dblPayedFee = dblMonthlyFee;
                                    }
                                    else
                                    {
                                        dblPayedFee = dblParkingFee;
                                        dblParkingFee = CommValue.NUMBER_VALUE_0_0;
                                    }
                                }
                                else
                                {
                                    #region lay thong tin shot thu 2 tro len tu bang MonthParkingFeeInfo
                                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S02
                                    strStartDt = hfStartDt.Value.Replace("-", "");
                                    string strRentCD = string.Empty;
                                    string strCarTys = string.Empty;
                                    DataTable dtParkReturn = ParkingMngBlo.SpreadParkingFeeInfoList(strRentCD, strCarTys, strStartDt);
                                    dblPayedFee = double.Parse(TextLib.MakeRoundDownThousand(double.Parse(dtParkReturn.Rows[0]["ParkingFee"].ToString())).ToString());
                                    if (dblParkingFee > dblPayedFee)
                                    {
                                        dblParkingFee = dblParkingFee - dblPayedFee;
                                    }
                                    else
                                    {
                                        dblPayedFee = dblParkingFee;
                                        dblParkingFee = CommValue.NUMBER_VALUE_0_0;
                                    }
                                    #endregion
                                }
                                if (intTmpI + CommValue.NUMBER_VALUE_1 == intLoopCnt)
                                {
                                    strEndDt = dtNowDate.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                }
                                else
                                {
                                    dtNowDate = DateTime.ParseExact((TextLib.MakeDateEightDigit(hfStartDt.Value.Replace("-", ""))).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);
                                    dtNowDate = (dtNowDate.AddMonths(intTmpI + CommValue.NUMBER_VALUE_1)).AddDays(-1);
                                    strEndDt = dtNowDate.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                }
                                // KN_USP_PRK_INSERT_PARKINGFEEINFO_M00
                                ParkingMngBlo.RegistryUserParkingCardFeeInfo(rentCD, strCardNo, Int32.Parse(strFloorNo), roomno, strTagNo,
                                                                             carID, strCarTy, double.Parse(strCardFee), dblPayedFee, strPaymentCd,
                                                                             strStartDt, strEndDt, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strPaymentDt);
                                //6.1-- Insert HoaDonParkingAPT ---- Add by phuongtv
                                //KN_USP_PRK_INSERT_HOADONPARKING_APT_I00
                                ParkingMngBlo.RegistryHoaDonParkingApt(rentCD, strTagNo, strStartDt, strEndDt);
                                if (intTmpI == CommValue.NUMBER_VALUE_0 && !strCardFee.Equals(CommValue.NUMBER_VALUE_ZERO))
                                {
                                    dblUniPrime = double.Parse(strCardFee) * (100) / (100 + dblVatRatio);
                                    // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                    dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, rentCD,
                                                                                      strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                      CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_SINGLEITEM, dblUniPrime, dblUniPrime, double.Parse(strCardFee), double.Parse(strCardFee),
                                                                                      CommValue.NUMBER_VALUE_0, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), cardID, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                      Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                    if (dtLedgerDet != null)
                                    {
                                        if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                            intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                    }
                                    // KN_USP_SET_INSERT_PRINTINFO_S00
                                    dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, rentCD,
                                                                                      Int32.Parse(strFloorNo), roomno, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                      Session["MemNo"].ToString(), carID + " Parking Card Fee ( " + strCardNo + " )",
                                                                                      double.Parse(strCardFee), double.Parse(hfRealBaseRate.Text),
                                                                                      Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);
                                    if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                                    {
                                        strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                        strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();
                                        // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                        BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));
                                        // KN_USP_SET_INSERT_PRINTINFO_S01
                                        ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                    }
                                    // KN_USP_SET_INSERT_MONEYINFO_M00
                                    ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);
                                }
                                dblUniPrime = dblPayedFee * (100) / (100 + dblVatRatio);
                                // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, rentCD,
                                                                                   strDirectCd, strParkingItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                   CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblPayedFee, dblPayedFee,
                                                                                   CommValue.NUMBER_VALUE_0, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), carID, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                   Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                //---------------Add by BaoTV-------------------
                                //KN_USP_MNG_INSERT_RENOVATIONINFO_M00 -- chưa tính tiền
                                //if (string.IsNullOrEmpty(strCardCost))
                                //{
                                //    strCardCost = txtCardFee.Text;
                                //    if (dbCardCost > 0)
                                //    {
                                //        var objReturn = MngPaymentBlo.InsertRenovationInfoApt(strPaymentCd, bankcd, txtRegRoomNo.Text, "0007", strPaymentDt, "0", "", dbCardCost, Session["MemNo"].ToString(), strInsMemIP, strCardNo);
                                //    }
                                //}
                                //-----------------------------------------------
                                if (dtLedgerDet != null)
                                {
                                    if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                    {
                                        intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                    }
                                }
                                // KN_USP_SET_INSERT_PRINTINFO_S00
                                dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, rentCD,
                                                                                   Int32.Parse(strFloorNo), roomno, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                   Session["MemNo"].ToString(), strStartDt.Substring(0, 4) + " / " + strStartDt.Substring(4, 2) + " Parking Fee ( " + carID + " )",
                                                                                   dblPayedFee, double.Parse(hfRealBaseRate.Text),
                                                                                   Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);
                                if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                                {
                                    strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                    strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();
                                    // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                    BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));
                                    // KN_USP_SET_INSERT_PRINTINFO_S01
                                    ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                }
                                // KN_USP_SET_INSERT_MONEYINFO_M00
                                ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);
                            }
                            #endregion
                            // 12. update thông tin hệ thống carparking
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        
        protected void MakeCalculate()
        {
            //if (string.IsNullOrEmpty(txtCardFee.Text))
            //{
            //    txtCardFee.Text = CommValue.NUMBER_VALUE_ZERO;
            //}

            //if (string.IsNullOrEmpty(txtParkingFee.Text))
            //{
            //    txtParkingFee.Text = CommValue.NUMBER_VALUE_ZERO;
            //}

            //txtTotalFee.Text = (Int32.Parse(txtParkingFee.Text.Replace(",", "")) + Int32.Parse(txtCardFee.Text)).ToString("###,##0");

        }

        protected void btnReadFile_Click(object sender, EventArgs e)
        {
            
            #region read excel file
            ExcelReaderLib dsd = new ExcelReaderLib();
            if (excelFileLoad.HasFile)
            {
                string fileUplName =  excelFileLoad.FileName;
                string fullPathUpload = Path.Combine(Server.MapPath(" "), fileUplName);
                excelFileLoad.SaveAs(fullPathUpload);

                DataTable dtb = dsd.ExtractDataTable(fullPathUpload);
                for (int x =0;x<dtb.Rows.Count ;x++)
                {
                    if(string.IsNullOrEmpty( dtb.Rows[x].ItemArray[0].ToString()))
                        dtb.Rows.RemoveAt(x);
                }
                dtlRecortUploaded.DataSource = dtb;
                dtlRecortUploaded.DataBind();

                grdExcelContent.DataSource = dtb;
                grdExcelContent.DataBind();
            }
            #endregion
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            ExcelReaderLib dsd = new ExcelReaderLib();
            if (excelFileLoad.HasFile)
            {
                string fileUplName = excelFileLoad.FileName;
                string fullPathUpload = Path.Combine(Server.MapPath(" "), fileUplName);
                excelFileLoad.SaveAs(fullPathUpload);

                DataTable dtb = dsd.ExtractDataTable(fullPathUpload);
                for (int x = 0; x < dtb.Rows.Count; x++)
                {
                    if (!string.IsNullOrEmpty(dtb.Rows[x].ItemArray[0].ToString()))
                    {
                        string cartype = "0001";//0001 oto,0002 xe may
                        string roomno = dtb.Rows[x].ItemArray[5].ToString() ;
                        string carID = dtb.Rows[x].ItemArray[2].ToString() ;
                        string cardID = dtb.Rows[x].ItemArray[3].ToString() ;
                        string cardCost = dtb.Rows[x].ItemArray[9].ToString() ;
                        //importExcel(cartype, roomno, carID, cardID);

                        //importExcel(dtb.Rows[x].ItemArray[2].ToString(),5,3,
                        RegistMonthly(cartype, roomno, carID, cardID, "0", "2", cardCost,ddlMonth.SelectedValue);
                    }
                }
                dtlRecortUploaded.DataSource = dtb;
                dtlRecortUploaded.DataBind();

                grdExcelContent.DataSource = dtb;
                grdExcelContent.DataBind();
            }
        }

        #region temp close
        /*
        protected void CalculateNumber()
        {
            string strParkingFee = string.Empty;

            DateTime now = DateTime.Now;

            string strDate = hfStartDt.Value.Replace("-", "");
            string strNowYear = strDate.Substring(0, 4);
            string strNowMonth = strDate.Substring(4, 2);
            string strNowDay = strDate.Substring(6, 2);
            string strDuringMonth = ddlDuringMonth.SelectedValue;
            string strEndDt = string.Empty;
            string strEndDays = string.Empty;

            // 월정 주차비 미등록시에 주차비 등록 페이지로 이동.
            if (!string.IsNullOrEmpty(ddlCarTy.Text))
            {
                if (!ddlCarTy.Text.Equals(CommValue.CODE_VALUE_EMPTY))
                {
                    // KN_USP_PRK_SELECT_MONTHPARKINGFEEINFO_S00
                    DataTable dtReturn = ParkingMngBlo.WatchMonthParkingFeeCheck(ddlRegRentCd.SelectedValue, strNowYear, strNowMonth, ddlCarTy.Text);

                    if (dtReturn != null)
                    {
                        if (Int32.Parse(dtReturn.Rows[0]["ExistCnt"].ToString()) > 0)
                        {
                            // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S02
                            DataTable dtParkReturn = ParkingMngBlo.SpreadParkingFeeInfoList(ddlRegRentCd.SelectedValue, ddlCarTy.SelectedValue, strDate);

                            if (dtParkReturn != null)
                            {
                                if (dtParkReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    strParkingFee = dtParkReturn.Rows[0]["ParkingFee"].ToString();
                                    txtHfMonthlyFee.Text = dtParkReturn.Rows[0]["MonthlyFee"].ToString();

                                    DateTime dtEndDate = DateTime.ParseExact(TextLib.MakeDateEightDigit(strDate).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);
                                    DateTime dtEndDays = DateTime.ParseExact(TextLib.MakeDateEightDigit(strDate).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

                                    strEndDays = dtEndDays.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue) + 1).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                    strEndDays = strEndDays.Substring(6, 2);
                                    strEndDt = dtEndDate.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue)).AddDays(-1).AddDays(Int32.Parse(rdobtnParkingDays.SelectedValue)).ToString("s").Substring(0, 10);

                                    txtParkingFee.Text = (TextLib.MakeRoundDownThousand(double.Parse(strParkingFee) + double.Parse(txtHfMonthlyFee.Text) * (Int32.Parse(strDuringMonth) - 1) + double.Parse(txtHfMonthlyFee.Text) * double.Parse(rdobtnParkingDays.SelectedValue) / double.Parse(strEndDays))).ToString("###,##0");
                                    txtEndDt.Text = strEndDt;
                                    hfEndDt.Value = strEndDt.Replace("/", "").Replace("-", "");
                                }
                                else
                                {
                                    StringBuilder sbWarning = new StringBuilder();

                                    sbWarning.Append("alert('" + AlertNm["ALERT_REGISTER_MONTHPARKINGFEE"] + "');");
                                    sbWarning.Append("document.location.href=\"" + Master.PAGE_REDIRECT + "\";");

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                                }
                            }
                            else if (dtParkReturn != null && txtParkingFee.Text == "0")
                            {
                                strParkingFee = "0";
                                txtHfMonthlyFee.Text = "0";

                                DateTime dtEndDate = DateTime.ParseExact(TextLib.MakeDateEightDigit(strDate).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);
                                DateTime dtEndDays = DateTime.ParseExact(TextLib.MakeDateEightDigit(strDate).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

                                strEndDays = dtEndDays.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue) + 1).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                strEndDays = strEndDays.Substring(6, 2);
                                strEndDt = dtEndDate.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue)).AddDays(-1).AddDays(Int32.Parse(rdobtnParkingDays.SelectedValue)).ToString("s").Substring(0, 10);

                                txtParkingFee.Text = (TextLib.MakeRoundDownThousand(double.Parse(strParkingFee) + double.Parse(txtHfMonthlyFee.Text) * (Int32.Parse(strDuringMonth) - 1) + double.Parse(txtHfMonthlyFee.Text) * double.Parse(rdobtnParkingDays.SelectedValue) / double.Parse(strEndDays))).ToString("###,##0");
                                txtEndDt.Text = strEndDt;
                                hfEndDt.Value = strEndDt.Replace("/", "").Replace("-", "");
                            }

                            else
                            {
                                StringBuilder sbWarning = new StringBuilder();

                                sbWarning.Append("alert('" + AlertNm["ALERT_REGISTER_MONTHPARKINGFEE"] + "');");
                                sbWarning.Append("document.location.href=\"" + Master.PAGE_REDIRECT + "\";");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                            }
                        }
                        else
                        {
                            StringBuilder sbWarning = new StringBuilder();

                            sbWarning.Append("alert('" + AlertNm["ALERT_REGISTER_MONTHPARKINGFEE"] + "');");
                            sbWarning.Append("document.location.href=\"" + Master.PAGE_REDIRECT + "\";");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                    }
                    else
                    {
                        txtParkingFee.Text = string.Empty;
                        txtHfMonthlyFee.Text = string.Empty;
                    }
                }
                else
                {
                    txtParkingFee.Text = string.Empty;
                    txtHfMonthlyFee.Text = string.Empty;
                }
            }

            else
            {
                txtParkingFee.Text = string.Empty;
                txtHfMonthlyFee.Text = string.Empty;
            }

            MakeCalculate();

            string strTmpStartDt = hfStartDt.Value.Replace("-", "");
            txtStartDt.Text = strTmpStartDt.Substring(0, 4) + "-" + strTmpStartDt.Substring(4, 2) + "-" + strTmpStartDt.Substring(6, 2);
        }
         * */
        #endregion

        private void RegistMonthly(string cartype, string roomno, string carID, string cardID, string cardFee, string paymentCD, string cardCost,string month)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                string strDirectCd = CommValue.DIRECT_TYPE_VALUE_DIRECT;
                string strItemCd = CommValue.ITEM_TYPE_VALUE_PARKINGCARDFEE;
                string strParkingItemCd = CommValue.ITEM_TYPE_VALUE_PARKINGFEE;
                //user info
                DataTable dtUser = ParkingMngBlo.SelectUserSeqByRoomNo(roomno);
                string strUserSeq = dtUser.Rows[0]["UserSeq"].ToString();
                string strFloorNo = dtUser.Rows[0]["FloorNo"].ToString();
                string strRentCd = dtUser.Rows[0]["RentCd"].ToString();
                string strUserNm = dtUser.Rows[0]["UserNm"].ToString();
                string strUserAddress = dtUser.Rows[0]["UserAddr"].ToString();
                string strUserTaxcd = dtUser.Rows[0]["UserTaxCd"].ToString(); 
                string strUserGender = dtUser.Rows[0]["Gender"].ToString(); 
                string strUserPhoneNo = dtUser.Rows[0]["phoneno"].ToString();  
                string strRoomNo = roomno;
                string strCarNo = carID;
                string strCarTy = cartype;
                
                //date info
                
                string strStartDt = DateTime.Now.Year.ToString() + month + "01"; //DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                string strEndDt = DateTime.ParseExact(strStartDt, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).AddMonths(1).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", ""); // DateTime.ParseExact(strStartDt + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null).AddMonths(Int32.Parse("1")).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", ""); ;
                string paymentDT = TextLib.MakeDateEightDigit(strStartDt);
                //card info
                string strCardNo = cardID;
                string strCardFee = string.Empty;
                string strTagNo = string.Empty;
                string strVatRatio = string.Empty;
                string strCardCost = cardCost;
                var dbCardCost = double.Parse(strCardFee != "" ? strCardFee : "0");
                //payment info
                string strPaymentCd = string.Empty;
                DataTable dtParkReturn = ParkingMngBlo.SpreadParkingFeeInfoList(strRentCd, cartype, strStartDt);
                string monthlyFee = dtParkReturn.Rows[0]["MonthlyFee"].ToString();
                #region payment cd
                switch (paymentCD)
                {
                    case "1":
                        strPaymentCd = "0001";
                        break;
                    case "2":
                        strPaymentCd = "0002";
                        break;
                    case "3":
                        strPaymentCd = "0003";
                        break;
                };//0001 card;0002 cash;0003 transfer
                #endregion
                var bankcd = 0;// Int32.Parse(ddlTransfer.SelectedValue != "" ? ddlTransfer.SelectedValue : "0");

                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;
                int intGateCnt = CommValue.NUMBER_VALUE_1;
                strCardFee = cardFee;
                #region get tagno
                if (string.IsNullOrEmpty(strTagNo) && !string.IsNullOrEmpty(strCardNo))
                {
                    // KN_USP_PRK_SELECT_PARKINGTAGLISTINFO_S02
                    DataTable dtTagReturn = ParkingMngBlo.WatchExgistParkingTagListInfo(strCardNo, strCarTy);
                    if (dtTagReturn != null)
                    {
                        if (dtTagReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            strTagNo = dtTagReturn.Rows[0]["TagNo"].ToString();
                        }
                    }
                }
                #endregion

                if (!string.IsNullOrEmpty(strCardNo) && !string.IsNullOrEmpty(strTagNo) && !string.IsNullOrEmpty(strCarNo))
                {
                    DateTime dtNowDate;
                    string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                    string strPaymentDt = strNowDt; //txtPayDt.Text.Replace("-", "").Replace(".", "");
                    string strNowDay = strNowDt.Substring(6, 2);// hfStartDt.Value.Replace("-", "").Substring(6, 2);
                    string strEndDay = string.Empty;
                    string strInsDt = strNowDt;//hfStartDt.Value.Replace("-", "");
                    double dblParkingFee = CommValue.NUMBER_VALUE_0_0;
                    double dblMonthlyFee = CommValue.NUMBER_VALUE_0_0;
                    double dblPayedFee = CommValue.NUMBER_VALUE_0_0;

                    // KN_USP_MNG_SELECT_VATINFO_S00
                    DataTable dtVatRatio = VatMngBlo.WatchVatInfo(CommValue.ITEM_TYPE_VALUE_PARKINGFEE);
                    DataTable dtPrintOut = new DataTable();
                    DataTable dtLedgerDet = new DataTable();

                    DataTable dtAccPrkMonthInfo = new DataTable();
                    DataTable dtHoaDonParkingAPTReturn = new DataTable();

                    dblParkingFee = double.Parse(cardCost);
                    dblMonthlyFee = double.Parse(monthlyFee);

                    double dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                    double dblItemTotViAmt = CommValue.NUMBER_VALUE_0_0;
                    double dblDongToDollar = CommValue.NUMBER_VALUE_0_0;
                    double dblUniPrime = CommValue.NUMBER_VALUE_0_0;
                    double dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                    int intPaymentSeq = CommValue.NUMBER_VALUE_0;
                    int intPaymentDetSeq = CommValue.NUMBER_VALUE_0;
                    int intItemSeq = CommValue.NUMBER_VALUE_0;
                    #region exchangerate
                    DataTable dtExchangeRate = ExchangeMngBlo.WatchExchangeRateLastInfo(CommValue.RENTAL_VALUE_PARKING);
                    string strRealBaseRate = dtExchangeRate.Rows[0]["DongToDollar"].ToString();
                    if (!string.IsNullOrEmpty(strRealBaseRate) && !string.IsNullOrEmpty(strCardFee))
                    {
                        dblDongToDollar = double.Parse(strRealBaseRate);
                        dblItemTotViAmt = double.Parse(strCardFee) + dblParkingFee;
                        if (dblDongToDollar > 0d)
                        {
                            dblItemTotEnAmt = dblItemTotViAmt / dblDongToDollar;
                        }
                    }
                    #endregion
                    #region getvatratio
                    if (dtVatRatio != null)
                    {
                        if (dtVatRatio.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            strVatRatio = dtVatRatio.Rows[0]["VatRatio"].ToString();
                            dblVatRatio = double.Parse(strVatRatio);
                            dblUniPrime = dblItemTotViAmt * (100) / (100 + dblVatRatio);
                        }
                        else
                        {
                            dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                            dblUniPrime = dblItemTotViAmt;
                        }
                    }
                    else
                    {
                        dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                        dblUniPrime = dblItemTotViAmt;
                    }
                    #endregion
                    #region tổng hợp tham số insert, dùng chung lưu lại 1 chỗ
                    string strCompNo = Session["CompCd"].ToString();
                    string strInsMemNo = Session["MemNo"].ToString();
                    #endregion

                    #region insert ledgerinfo
                    // KN_USP_SET_INSERT_LEDGERINFO_S00
                    DataTable dtLedgerAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strPaymentDt, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, strParkingItemCd,
                                                                               CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, strUserSeq, string.Empty,
                                                                               dblDongToDollar, dblItemTotEnAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio,strCompNo,strInsMemNo,strInsMemIP);

                    if (dtLedgerAccnt != null)
                    {
                        if (dtLedgerAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            intPaymentSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["PaymentSeq"].ToString());
                            intItemSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["ItemSeq"].ToString());
                            if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                            {
                                // KN_USP_SET_INSERT_LEDGERINFO_S01
                                BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, string.Empty);
                            }
                        }
                    }
                    #endregion

                    #region Insert vào các bảng
                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S02
                    DataTable dtParkFeeReturn = ParkingMngBlo.SpreadParkingFeeInfoList(strRentCd, cartype, strStartDt);
                    dblPayedFee = double.Parse(TextLib.MakeRoundDownThousand(double.Parse(dtParkFeeReturn.Rows[0]["ParkingFee"].ToString())).ToString());

                    if (dblParkingFee > dblPayedFee)
                    {
                        dblParkingFee = dblParkingFee - dblPayedFee;
                    }
                    else
                    {
                        dblPayedFee = dblParkingFee;
                        dblParkingFee = CommValue.NUMBER_VALUE_0_0;
                    }
                    // 5. 
                    // 6. 
                    // KN_USP_PRK_INSERT_PARKINGFEEINFO_M00 -- insert AccountMonthParkingInfo
                    ParkingMngBlo.RegistryUserParkingCardFeeInfo(strRentCd, strCardNo, Int32.Parse(strFloorNo), strRoomNo, strTagNo,
                                                                 strCarNo, strCarTy, double.Parse(strCardFee), dblPayedFee, strPaymentCd,
                                                                 strStartDt, strEndDt, strCompNo, strInsMemNo,
                                                                 strInsMemIP, strPaymentDt);
                    //6.1-- Insert HoaDonParkingAPT ---- Add by phuongtv
                    //KN_USP_PRK_INSERT_HOADONPARKING_APT_I00 insert HoaDonParkingAPT
                    ParkingMngBlo.RegistryHoaDonParkingApt(strRentCd, strTagNo, strStartDt, strEndDt);

                    // KN_USP_PRK_SELECT_DEL_HoaDonParkingAPTReturn_S01 - insert HoaDonParkingAPTreturn
                    dtHoaDonParkingAPTReturn = ParkingMngBlo.SelectExsistHoaDonParkingAPTReturn(strCardNo, strRoomNo, strStartDt, strEndDt);
                    if (dtHoaDonParkingAPTReturn.Rows.Count > 0)
                    {
                        // KN_USP_PRK_DELETE_HoaDonParkingAPTReturn_M01
                        ParkingMngBlo.DeleteHoaDonParkingAPTReturn(strCardNo, strRoomNo, strStartDt, strEndDt);
                    }
                    #region xXx - tính phí thẻ - tạm thời lưu kho
                    /*
                        if (intTmpI == CommValue.NUMBER_VALUE_0 && !strCardFee.Equals(CommValue.NUMBER_VALUE_ZERO))
                        {
                            dblUniPrime = double.Parse(strCardFee) * (100) / (100 + dblVatRatio);

                            // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                            dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                             strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                             CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_SINGLEITEM, dblUniPrime, dblUniPrime, double.Parse(strCardFee), double.Parse(strCardFee),
                                                                             CommValue.NUMBER_VALUE_0, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), cardID, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                             Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                            if (dtLedgerDet != null)
                            {
                                if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                }
                            }

                            #region cardfee
                            //---------------Add by BaoTV-------------------
                            //KN_USP_MNG_INSERT_RENOVATIONINFO_M00
                            //if (string.IsNullOrEmpty(strCardCost))
                            //{
                            //    strCardCost = txtCardFee.Text;
                            //    if (dbCardCost > 0)
                            //    {
                            //        var objReturn = MngPaymentBlo.InsertRenovationInfoApt(strPaymentCd, bankcd, txtRegRoomNo.Text, "0007", strPaymentDt, "0", "", dbCardCost, Session["MemNo"].ToString(), strInsMemIP, strCardNo);
                            //    }
                            //}
                            #endregion
                            #region 8. insert print info
                            // KN_USP_SET_INSERT_PRINTINFO_S00
                            dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                              Int32.Parse(strFloorNo), strRoomNo, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                              Session["MemNo"].ToString(), strCarNo + " Parking Card Fee ( " + strCardNo + " )",
                                                                              double.Parse(strCardFee), double.Parse(strRealBaseRate),
                                                                              Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);
                            if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                            {
                                strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();
                                // 9. 
                                // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));
                                // 10. 
                                // KN_USP_SET_INSERT_PRINTINFO_S01
                                ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                            }
                            #endregion
                            #region 11. insert moneyinfo
                            // KN_USP_SET_INSERT_MONEYINFO_M00
                            ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);
                            #endregion
                        }
                         * */
                    #endregion
                    dblUniPrime = dblPayedFee * (100) / (100 + dblVatRatio);
                    // 7.
                    // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                    dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, 0, strRentCd,
                                                                       strDirectCd, strParkingItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                       1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblPayedFee, dblPayedFee,
                                                                       0, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), carID, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                       strCompNo, strInsMemNo, strInsMemIP);
                    if (dtLedgerDet != null)
                    {
                        if (dtLedgerDet.Rows.Count > 0)
                        {
                            intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                        }
                    }

                    #region 8. Region insert print info
                    // KN_USP_SET_INSERT_PRINTINFO_S00
                    dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                       Int32.Parse(strFloorNo), strRoomNo, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                       strInsMemNo, strStartDt.Substring(0, 4) + " / " + strStartDt.Substring(4, 2) + " Parking Fee ( " + carID + " )",
                                                                       dblPayedFee, double.Parse(strRealBaseRate),
                                                                       strCompNo, strInsMemNo, strInsMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);
                    if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                    {
                        strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                        strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();
                        // 9. 
                        // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                        BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));
                        // 10. 
                        // KN_USP_SET_INSERT_PRINTINFO_S01
                        ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), strCompNo, strInsMemNo, strInsMemIP);
                    }
                    // 11.
                    // KN_USP_SET_INSERT_MONEYINFO_M00
                    ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);
                    #endregion
                    #endregion

                    //=================Get Price for Free====================
                    if (dblItemTotViAmt == 0)
                    {
                        dblItemTotViAmt = 875000.0;
                    }
                    //=======================================================
                    #region 12.update parking system

                    #region brecus
                    string brecusConn = ConfigurationManager.ConnectionStrings["BrecusParkDB"].ConnectionString;
                    string Insert_tbl_customer = @"
                                                              insert into PerfectParking.dbo.tbl_customer 
                                                              ([CustomerCode]
                                                              ,[CustomerName]
                                                              ,[Address]
                                                              ,[TaxCode]
                                                              ,[Gender]
                                                              ,[Phone]
                                                              ,[CreateDate])
                                                     Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
                    string Insert_tbl_cardholder = @"insert into PerfectParking.[dbo].[Tbl_CardHolder]
                                                                                 ( [CardID],
                                                                                [TagNo],
                                                                                [CardTypeID],
                                                                                [CustomerID],
                                                                                [IssueDate],
                                                                                [ExpiryDate],
                                                                                [CreateDate] )
                                                                                Values ('{0}','{1}','{2}',(select ID from tbl_customer where customercode= '{3}'),'{4}','{5}','{6}')";
                    string Insert_Tbl_CustomerVehicle = @"insert into [PerfectParking].[dbo].[Tbl_CustomerVehicle]
                                                                         (
	                                                                        [CustomerID],
	                                                                        [VehicleType],
	                                                                        [VehicleLPN]
                                                                         )
                                                                         Values ((select ID from tbl_customer where customercode= '{0}'),'{1}','{2}')";
                    string SelectCustomerExists = "select * from tbl_customer where customercode = '{0}'";
                    SqlConnection sqlconn = new SqlConnection(brecusConn);
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        sqlconn.Open();
                        //insert into tbl_customer
                        cmd.Connection = sqlconn;
                        string stgender = "0" ;
                        cmd.CommandText = string.Format(SelectCustomerExists, strUserSeq);
                        var x = cmd.ExecuteScalar();
                        if (x == null)
                        {
                            switch (strUserGender)
                            {
                                case "M":
                                    stgender = "0";
                                    break;
                                case "F":
                                    stgender = "1";
                                    break;
                            }
                            cmd.CommandText = string.Format(Insert_tbl_customer, strUserSeq, strUserNm, strUserAddress, strUserTaxcd, stgender, strUserPhoneNo, DateTime.Now.ToString());
                            cmd.ExecuteNonQuery();
                        }

                        //insert into tbl_cardholder
                        string selectCardExists = "select * from [dbo].[Tbl_CardHolder] where tagno='{1}'";
                        string enddtIns = DateTime.ParseExact(strEndDt, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString();
                        cmd.CommandText = string.Format(selectCardExists, cardID, strTagNo);
                        var cardexists = cmd.ExecuteScalar();
                        if (cardexists == null)
                        {
                            cmd.CommandText = string.Format(Insert_tbl_cardholder, cardID, strTagNo, cartype, strUserSeq, strStartDt, enddtIns, DateTime.Now.ToString());
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            string updateExpireDate = "update [dbo].[Tbl_CardHolder] set [ExpiryDate] = '{0}' where tagno='{1}'";
                            cmd.CommandText = string.Format(updateExpireDate, enddtIns, strTagNo);
                            cmd.ExecuteNonQuery();
                        }
                        
                        //insert into [Tbl_CustomerVehicle]
                        cmd.CommandText = string.Format(Insert_Tbl_CustomerVehicle, strUserSeq, cartype, carID);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    { 
                        
                    }
                    #endregion
                    //if (!string.IsNullOrEmpty(hfGateList.Value))
                    //{
                    //    intGateCnt = Int32.Parse(hfGateList.Value);
                    //}

                    //// 오토바이 게이트 등록요청 여부 체크
                    //if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE))
                    //{
                    //    // KN_USP_PRK_UPDATE_PARKINGFEEINFO_M03
                    //    //ParkingMngBlo.ModifyAUTOParkingSystemInfo(strCardNo, hfStartDt.Value.Replace("-", "").Replace("/", ""), hfEndDt.Value.Replace("-", "").Replace("/", ""), dblItemTotViAmt, intLoopCnt);
                    //    // 차감
                    //    intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE);
                    //}

                    //// 오피스 리테일 게이트 등록요청 여부 체크
                    //if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL))
                    //{
                    //    // KN_USP_PRK_UPDATE_PARKINGFEEINFO_M01
                    //    ParkingMngBlo.ModifyORParkingSystemInfo(strCardNo, hfStartDt.Value.Replace("-", "").Replace("/", ""), hfEndDt.Value.Replace("-", "").Replace("/", ""), dblItemTotViAmt, intLoopCnt);
                    //    // 차감
                    //    intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL);
                    //}

                    //// 아파트 게이트 등록요청 여부 체크
                    //if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_APARTMENT))
                    //{
                    //    // KN_USP_PRK_UPDATE_PARKINGFEEINFO_M00
                    //    ParkingMngBlo.ModifyParkingSystemInfo(strCardNo, hfStartDt.Value.Replace("-", "").Replace("/", ""), hfEndDt.Value.Replace("-", "").Replace("/", ""), dblItemTotViAmt, intLoopCnt);
                    //    // 차감
                    //    intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_APARTMENT);
                    //}

                    // chú ý: in ra tính sau
                    //StringBuilder sbList = new StringBuilder();
                    //sbList.Append("window.open('/Common/RdPopup/RDPopupReciptParkingFee.aspx?Datum0=" + strPrintSeq + "&Datum1=0" + "&Datum2=" + paymentDT + "&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingFee", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void initValues()
        { 
            
        }
    }
    public class ParkingInfo
    {
        public string Id { get; set; }
        public string cardNo { get; set; }
        public string cardType { get; set; }
        public string cardTagNo { get; set; }
        public string carType { get; set; }
        public string monthPay { get; set; }
    }

}