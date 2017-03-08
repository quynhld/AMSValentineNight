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

using KN.Config.Biz;
using KN.Parking.Biz;
using KN.Resident.Biz;
using KN.Settlement.Biz;
using KN.Manage.Biz;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace KN.Web.Park
{
    public partial class MonthParkingCardList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    InitControls();

                    //LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            string strFeeTyTxt = string.Empty;
            string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
            string strNowDay = strNowDt.Substring(6, 2);
            string strEndDt = string.Empty;
            txtPayDt.Text = TextLib.MakeDateEightDigit(strNowDt);
            txtStartDt.Text = TextLib.MakeDateEightDigit(strNowDt);
            DateTime dtEndDate = DateTime.ParseExact(DateTime.Now.ToString("s").Substring(0, 7).ToString() + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

            ltInsRoomNo.Text = TextNm["ROOMNO"];
            ltInsCarNo.Text = TextNm["CARNO"];
            ltInsCardNo.Text = TextNm["CARDNO"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            ltTopSeq.Text = TextNm["SEQ"];
            ltTopRoomNo.Text = TextNm["ROOMNO"];
            ltTopName.Text = TextNm["NAME"];
            ltTopCarNo.Text = TextNm["CARNO"];
            ltTopCardNo.Text = TextNm["PARKINGTAGNO"];
            ltTopCarTy.Text = TextNm["CARTY"];
            ltTopCardFee.Text = TextNm["CARDFEE"];
            ltRegRent.Text = TextNm["RENT"];
            ltRegRoomNo.Text = TextNm["ROOMNO"];
            ltRegParkingCarNo.Text = TextNm["CARNO"];
            ltRegParkingCardNo.Text = TextNm["PARKINGTAGNO"];
            ltGateTy.Text = TextNm["GATE"];
            ltRegStartDt.Text = TextNm["COMMENCINGDT"];
            ltRegCarTy.Text = TextNm["CARTY"];
            ltRegCardFee.Text = TextNm["CARDFEE"];
            ltRegParkingFee.Text = TextNm["PARKIKNGFEE"];
            ltRegPaymentCd.Text = TextNm["PAYMENTKIND"];
            ltRegTotalFee.Text = TextNm["ENTIRE"];

            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];

            lnkbtnRegist.Text = TextNm["REGIST"];

            // 섹션코드 조회
            LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            // 차종 조회
            CommCdDdlUtil.MakeEtcSubCdDdlTitle(ddlCarTy, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_CARTY);

            // 납부 방법 조회
            CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaymentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);

            // 섹션코드 조회
            LoadRentDdl(ddlRegRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            // 주차일수
            CommCdRdoUtil.MakeEtcSubCdRdoTitle(rdobtnParkingDays, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_PARKINGDAYS, RepeatDirection.Horizontal);

            // 게이트조회
            CommCdChkUtil.MakeEtcSubCdChkNoTitle(chkGateList, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_GATE, RepeatDirection.Horizontal);

            MakeAccountDdl(ddlTransfer);

            MakeMonthDdl(ddlDuringMonth);

            if (Int32.Parse(strNowDay) >= 15)
            {
                //ddlDuringMonth.SelectedValue = CommValue.NUMBER_VALUE_TWO;
                ddlDuringMonth.SelectedValue = CommValue.NUMBER_VALUE_ONE;
            }

            strEndDt = dtEndDate.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue)).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

            LoadExchageDate();

            txtStartDt.Text = TextLib.MakeDateEightDigit(strNowDt);
            hfStartDt.Value = strNowDt;
            rdobtnParkingDays.SelectedValue = CommValue.PARKINGDAYS_VALUE_00;
            txtEndDt.Text = TextLib.MakeDateEightDigit(strEndDt);
            hfEndDt.Value = strEndDt;
            ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;

            txtCardFee.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            string strCardNo = string.Empty;

            //strCardNo = txtInsCardNo.Text;

            //if (!string.IsNullOrEmpty(strCardNo))
            //{
            //    if (strCardNo.Length < 8)
            //    {
            //        strCardNo = txtInsCardNo.Text.PadLeft(8, '0');
            //    }
            //}

            // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S01
            dtReturn = ParkingMngBlo.SpreadParkingUserListInfo(ddlInsRentCd.SelectedValue, txtInsRoomNo.Text, txtInsCardNo.Text, txtInsCarNo.Text, Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                lvActMonthParkingCardList.DataSource = dtReturn;
                lvActMonthParkingCardList.DataBind();
            }

            // ResetSearchControls();
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
        protected void lvActMonthParkingCardList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvActMonthParkingCardList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    Literal ltSeq = (Literal)iTem.FindControl("ltSeq");
                    ltSeq.Text = drView["Seq"].ToString();
                    TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                    TextBox txtHfUserDetSeq = (TextBox)iTem.FindControl("txtHfUserDetSeq");
                    txtHfUserDetSeq.Text = drView["UserDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    TextBox txtRentCd = (TextBox)iTem.FindControl("txtRentCd");
                    txtRentCd.Text = TextLib.StringDecoder(drView["RentCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                    ltRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    Literal ltName = (Literal)iTem.FindControl("ltName");
                    ltName.Text = TextLib.StringDecoder(drView["UserNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ParkingCarNo"].ToString()))
                {
                    TextBox txtCarNo = (TextBox)iTem.FindControl("txtCarNo");
                    txtCarNo.Text = TextLib.StringDecoder(drView["ParkingCarNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ParkingCardNo"].ToString()))
                {
                    TextBox txtCardNo = (TextBox)iTem.FindControl("txtCardNo");
                    txtCardNo.Text = TextLib.StringDecoder(drView["ParkingCardNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ParkingTagNo"].ToString()))
                {
                    TextBox txtHfTagNo = (TextBox)iTem.FindControl("txtHfTagNo");
                    txtHfTagNo.Text = TextLib.StringDecoder(drView["ParkingTagNo"].ToString());
                }

                TextBox txtCardFee = (TextBox)iTem.FindControl("txtCardFee");
                txtCardFee.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

                if (!string.IsNullOrEmpty(drView["CarNm"].ToString()))
                {
                    Literal ltCarTyNm = (Literal)iTem.FindControl("ltCarTyNm");
                    ltCarTyNm.Text = TextLib.StringDecoder(drView["CarNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["CarCd"].ToString()))
                {
                    TextBox txtHfCarTyCd = (TextBox)iTem.FindControl("txtHfCarTyCd");
                    txtHfCarTyCd.Text = TextLib.StringDecoder(drView["CarCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ParkingYYYYMM"].ToString()))
                {
                    TextBox txtHfParkingYYYYMM = (TextBox)iTem.FindControl("txtHfParkingYYYYMM");
                    txtHfParkingYYYYMM.Text = drView["ParkingYYYYMM"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PayDt"].ToString()))
                {
                    TextBox txtHfPayDt = (TextBox)iTem.FindControl("txtHfPayDt");
                    txtHfPayDt.Text = drView["PayDt"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["GateCd"].ToString()))
                {
                    int intGateCnt = CommValue.NUMBER_VALUE_1;

                    CheckBoxList chkGateCd = (CheckBoxList)iTem.FindControl("chkGateCd");

                    // 게이트조회
                    CommCdChkUtil.MakeEtcSubCdChkNoTitle(chkGateCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_GATE, RepeatDirection.Horizontal);
                    HiddenField hfGateCd = (HiddenField)iTem.FindControl("hfGateCd");
                    TextBox txtHfGateCd = (TextBox)iTem.FindControl("txtHfGateCd");

                    hfGateCd.Value = drView["GateCd"].ToString();
                    txtHfGateCd.Text = drView["GateCd"].ToString();

                    if (!string.IsNullOrEmpty(hfGateCd.Value))
                    {
                        intGateCnt = Int32.Parse(hfGateCd.Value);
                    }

                    // 오토바이 게이트 등록요청 여부 체크
                    if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE))
                    {
                        chkGateCd.Items[2].Selected = CommValue.AUTH_VALUE_TRUE;

                        // 차감
                        intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE);
                    }

                    // 오피스 리테일 게이트 등록요청 여부 체크
                    if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL))
                    {
                        chkGateCd.Items[1].Selected = CommValue.AUTH_VALUE_TRUE;

                        // 차감
                        intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL);
                    }

                    // 아파트 게이트 등록요청 여부 체크
                    if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_APARTMENT))
                    {
                        chkGateCd.Items[0].Selected = CommValue.AUTH_VALUE_TRUE;

                        // 차감
                        intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_APARTMENT);
                    }
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_SELECTED_ITEM"] + "');";
            }
        }

        //-------- function get Bank Acc----------------------------
        public void MakeAccountDdl(DropDownList ddlParams)
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
            // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
            // Utility Fee : Chestnut 매출
            // 그외 KeangNam 매출
            const string strCompCd = CommValue.MAIN_COMP_CD;
            var dtReturn = AccountMngBlo.SpreadBankAccountInfo(strCompCd);

            ddlParams.Items.Clear();

            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

            foreach (var dr in dtReturn.Select())
            {
                ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
            }
        }


        protected void lvActMonthParkingCardList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strSeq = string.Empty;
                string strRentCd = string.Empty;
                string strRoomNo = string.Empty;
                string strName = string.Empty;
                string strCarTyCd = string.Empty;
                string strCarNo = string.Empty;
                string strCardNo = string.Empty;
                string strCardFee = string.Empty;
                string strGateCd = string.Empty;
                string strTagNo = string.Empty;
                string strUserSeq = string.Empty;
                string strUserDetSeq = string.Empty;
                string strOriginDt = string.Empty;
                string strParkingYYYYMM = string.Empty;
                string strPayDt = string.Empty;
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;

                double dblCardFee = CommValue.NUMBER_VALUE_0;
                double dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                double dblVatRatio = CommValue.NUMBER_VALUE_0;
                double dblUnitPrime = CommValue.NUMBER_VALUE_0;

                int intDongToDollar = CommValue.NUMBER_VALUE_1;
                int intPaymentSeq = CommValue.NUMBER_VALUE_0;
                int intItemSeq = CommValue.NUMBER_VALUE_0;
                int intPaymentDetSeq = CommValue.NUMBER_VALUE_0;

                Literal ltSeq = (Literal)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("ltSeq");
                Literal ltRoomNo = (Literal)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("ltRoomNo");
                Literal ltName = (Literal)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("ltName");
                Literal ltCarTyNm = (Literal)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("ltCarTyNm");

                TextBox txtRentCd = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtRentCd");
                TextBox txtCarNo = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtCarNo");
                TextBox txtCardNo = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtCardNo");
                TextBox txtCardFee = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtCardFee");

                TextBox txtHfCarTyCd = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfCarTyCd");
                TextBox txtHfTagNo = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfTagNo");
                TextBox txtHfUserSeq = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfUserSeq");
                TextBox txtHfUserDetSeq = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfUserDetSeq");
                TextBox txtHfOriginDt = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfOriginDt");
                TextBox txtHfParkingYYYYMM = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfParkingYYYYMM");
                TextBox txtHfPayDt = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfPayDt");

                HiddenField hfGateCd = (HiddenField)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("hfGateCd");

                strRentCd = txtRentCd.Text;
                strRoomNo = ltRoomNo.Text;
                strUserSeq = txtHfUserSeq.Text;
                strUserDetSeq = txtHfUserDetSeq.Text;
                strTagNo = txtHfTagNo.Text;
                strCardNo = txtCardNo.Text;
                strCardFee = txtCardFee.Text;
                strCarNo = txtCarNo.Text;
                strCarTyCd = txtHfCarTyCd.Text;
                strGateCd = hfGateCd.Value;

                if (!string.IsNullOrEmpty(hfRealBaseRate.Text))
                {
                    intDongToDollar = Int32.Parse(hfRealBaseRate.Text);
                }

                if (!string.IsNullOrEmpty(strCarNo) && !string.IsNullOrEmpty(strCardNo))
                {
                    // KN_USP_PRK_SELECT_PARKINGTAGLISTINFO_S00
                    DataTable dtExgist = ParkingMngBlo.SpreadExgistParkingCardInfo(strCardNo, strTagNo, strCarTyCd);

                    if (dtExgist != null)
                    {
                        if (dtExgist.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            if (Int32.Parse(dtExgist.Rows[0]["ReturnCnt"].ToString()) == CommValue.NUMBER_VALUE_1)
                            {
                                // 해당 항이 1개만 존재할 때
                                // KN_USP_PRK_UPDATE_MONTHPARKINGINFO_M00
                                object[] objReturn = ParkingMngBlo.ModifyUserParkingInfo(strUserSeq, Int32.Parse(strUserDetSeq), strTagNo, strCardNo, strCarNo, strCarTyCd,
                                                                                         strGateCd, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                                if (!string.IsNullOrEmpty(strCardFee))
                                {
                                    dblCardFee = double.Parse(strCardFee.Replace(",", ""));
                                    dblItemTotEnAmt = dblCardFee / intDongToDollar;
                                }

                                // 카드변경 비용이 발생할 경우
                                if (dblCardFee > CommValue.NUMBER_VALUE_0)
                                {
                                    // KN_USP_MNG_SELECT_VATINFO_S00
                                    DataTable dtVatRatio = VatMngBlo.WatchVatInfo(CommValue.ITEM_TYPE_VALUE_PARKINGFEE);

                                    if (dtVatRatio != null)
                                    {
                                        if (dtVatRatio.Rows.Count > CommValue.NUMBER_VALUE_0)
                                        {
                                            dblVatRatio = double.Parse(dtVatRatio.Rows[0]["VatRatio"].ToString());
                                            dblUnitPrime = dblCardFee * (100) / (100 + dblVatRatio);
                                        }
                                        else
                                        {
                                            dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                            dblUnitPrime = dblCardFee;
                                        }
                                    }

                                    string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                    string strPaymentDt = txtPayDt.Text.Replace("-", "").Replace(".", "");

                                    // 무조건 현금만 수납
                                    // 카드비 처리 원장등록
                                    // KN_USP_SET_INSERT_LEDGERINFO_S00
                                    DataTable dtLedgerAccnt = BalanceMngBlo.RegistryLedgerInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strPaymentDt, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                               CommValue.DIRECT_TYPE_VALUE_DIRECT, CommValue.ITEM_TYPE_VALUE_PARKINGCARDFEE,
                                                                                               CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, strUserSeq, string.Empty,
                                                                                               intDongToDollar, dblItemTotEnAmt, dblCardFee, CommValue.PAYMENT_TYPE_VALUE_CASH, dblVatRatio,
                                                                                               Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                    if (dtLedgerAccnt != null)
                                    {
                                        if (dtLedgerAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                                        {
                                            intPaymentSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["PaymentSeq"].ToString());
                                            intItemSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["ItemSeq"].ToString());
                                        }
                                    }

                                    // 카드비 처리 상세정보등록
                                    // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                    DataTable dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strPaymentDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                                CommValue.DIRECT_TYPE_VALUE_DIRECT, CommValue.ITEM_TYPE_VALUE_PARKINGCARDFEE, intItemSeq, CommValue.NUMBER_VALUE_0,
                                                                                                string.Empty, string.Empty, string.Empty, string.Empty, CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_SINGLEITEM,
                                                                                                dblUnitPrime, dblUnitPrime, dblCardFee, dblCardFee, CommValue.NUMBER_VALUE_0, strNowDt.Substring(0, 4), strNowDt.Substring(4, 2),
                                                                                                txtParkingCardNo.Text, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                                    if (dtLedgerDet != null)
                                    {
                                        if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                        {
                                            intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                        }
                                    }

                                    // 8. 출력 테이블에 등록
                                    // KN_USP_SET_INSERT_PRINTINFO_S00
                                    DataTable dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(string.Empty, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                                                 Int32.Parse(strRoomNo.Substring(1, 2)), strRoomNo, strNowDt.Substring(0, 4), strNowDt.Substring(4, 2), CommValue.PAYMENT_TYPE_VALUE_CASH,
                                                                                                 strUserSeq, Session["MemNo"].ToString(), strCarNo + " Parking Card Fee ( " + strCardNo + " )", dblCardFee, intDongToDollar, Session["CompCd"].ToString(),
                                                                                                 Session["MemNo"].ToString(), strInsMemIP, CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strNowDt, intPaymentSeq, intPaymentDetSeq);
                                    if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                                    {
                                        strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                        strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                        // 9. 출력 정보 원장상세 테이블에 등록
                                        // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                        //BalanceMngBlo.ModifyLedgerDetInfoForPrint(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strNowDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));
                                        BalanceMngBlo.ModifyLedgerDetInfoForPrint(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));

                                        // 10. 출력자 테이블에 등록
                                        // KN_USP_SET_INSERT_PRINTINFO_S01
                                        ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                    }

                                    // 11. 금액 로그 테이블 처리
                                    // KN_USP_SET_INSERT_MONEYINFO_M00
                                    //ReceiptMngBlo.RegistryMoneyInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strNowDt, intPaymentSeq, intPaymentDetSeq);
                                    ReceiptMngBlo.RegistryMoneyInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strPaymentDt, intPaymentSeq, intPaymentDetSeq);

                                    LoadData();

                                    StringBuilder sbList = new StringBuilder();
                                    sbList.Append("window.open('/Common/RdPopup/RDPopupReciptDetail.aspx?Datum0=" + strPrintSeq + "&Datum1=0&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingFee", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                                }
                                else
                                {
                                    LoadData();

                                    StringBuilder sbList = new StringBuilder();
                                    sbList.Append("alert('" + AlertNm["INFO_MODIFY_ISSUE"] + "');");

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingModify", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                                }
                            }
                            else if (Int32.Parse(dtExgist.Rows[0]["ReturnCnt"].ToString()) > CommValue.NUMBER_VALUE_1)
                            {
                                // 해당 항이 2개이상 존재할 때
                                StringBuilder sbList = new StringBuilder();
                                sbList.Append("alert('" + AlertNm["ALERT_ISSUED_CARD"] + "');");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingModify", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                            }
                            else
                            {
                                // 해당 항이 없을 경우
                                StringBuilder sbList = new StringBuilder();
                                sbList.Append("alert('" + AlertNm["ALERT_REMOVED_CARD"] + "');");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingModify", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                            }
                        }
                    }
                }
                else
                {
                    StringBuilder sbList = new StringBuilder();
                    sbList.Append("alert('" + AlertNm["ALERT_INSERT_BLANK"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvActMonthParkingCardList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strUserSeq = string.Empty;
                string strUserDetSeq = string.Empty;
                string strCarCd = string.Empty;

                TextBox txtHfUserSeq = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfUserSeq");
                TextBox txtHfUserDetSeq = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfUserDetSeq");
                TextBox txtHfCarTyCd = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfCarTyCd");

                strUserSeq = txtHfUserSeq.Text;
                strUserDetSeq = txtHfUserDetSeq.Text;
                strCarCd = txtHfCarTyCd.Text;

                // KN_USP_PRK_DELETE_USERPARKINGINFO_M00
                ParkingMngBlo.RemoveUserParkingInfo(strUserSeq, Int32.Parse(strUserDetSeq));

                if (strCarCd.Equals(CommValue.CARTY_VALUE_VEHICLE))
                {
                    // KN_USP_PRK_DELETE_USERPARKINGINFO_M01
                    ParkingMngBlo.RemoveParkingMasterInfo(strUserSeq, Int32.Parse(strUserDetSeq));
                }

                LoadData();

                StringBuilder sbList = new StringBuilder();
                sbList.Append("alert('" + AlertNm["INFO_DELETE_ISSUE"] + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingDelete", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlDuringMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNumber();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void rdobtnParkingDays_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNumber();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlPaymentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPaymentCd.SelectedValue == CommValue.PAYMENT_TYPE_VALUE_TRANSFER)
                {
                    ddlTransfer.Enabled = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ddlTransfer.SelectedValue = string.Empty;
                    ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

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

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
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

                //string strUserSeq = hfUserSeq.Value;
                string strRentCd = ddlRegRentCd.SelectedValue;
                string strCardFee = string.Empty;
                //string strFloorNo = hfFloorNo.Value;
                string strRoomNo = txtRegRoomNo.Text;
                string strCarNo = txtCarNo.Text;
                string strCarTy = ddlCarTy.SelectedValue;
                string strDuringMonth = ddlDuringMonth.SelectedValue;
                string strStartDt = hfStartDt.Value;
                string strEndDt = hfEndDt.Value;
                string strPaymentCd = ddlPaymentCd.Text;
                string strCardNo = txtParkingCardNo.Text;
                string strTagNo = hfParkingTagNo.Value;
                string strVatRatio = string.Empty;

                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;
                string paymentDT = txtPayDt.Text;

                string strCardCost = string.Empty;

                DataTable dtUser = ParkingMngBlo.SelectUserSeqByRoomNo(strRoomNo);
                string strUserSeq = dtUser.Rows[0]["UserSeq"].ToString();
                string strFloorNo = dtUser.Rows[0]["FloorNo"].ToString();
                var bankcd = Int32.Parse(ddlTransfer.SelectedValue != "" ? ddlTransfer.SelectedValue : "0");
                var dbCardCost = double.Parse(txtCardFee.Text != "" ? txtCardFee.Text : "0");

                txtParkingFee.Text = string.Empty;
                txtHfMonthlyFee.Text = string.Empty;

                if (rbMoneyFree.SelectedValue == "Y")
                {
                    txtParkingFee.Text = "0";
                    MakeCalculate();
                }
                else
                {
                    CalculateNumber();
                }

                int intGateCnt = CommValue.NUMBER_VALUE_1;

                if (string.IsNullOrEmpty(txtCardFee.Text))
                {
                    strCardFee = "0";
                }
                else
                {
                    strCardFee = txtCardFee.Text;
                }

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

                if (!string.IsNullOrEmpty(strCardNo) && !string.IsNullOrEmpty(strTagNo) && !string.IsNullOrEmpty(strCarNo))
                {
                    DateTime dtNowDate;
                    string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S03
                    DataTable dtReturn = ParkingMngBlo.SpreadExgistParkingCardInfo(strCardNo, strCarNo);
                    DataTable dtUserParkingInfo = new DataTable();

                    if (dtReturn.Rows.Count > 0)
                    {
                        StringBuilder sbWarning = new StringBuilder();
                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["ALERT_ISSUED_CARD"]);
                        sbWarning.Append("');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                        ResetSearchControls();
                    }
                    else
                    {
                        // 1. 사용자주차목록에 등록
                        // 2. 주차태그 사용중 처리
                        // KN_USP_PRK_INSERT_USERPARKINGINFO_M00
                        dtUserParkingInfo = ParkingMngBlo.RegistryUserParkingInfo(strUserSeq, strTagNo, strCardNo, strCarNo, ddlCarTy.SelectedValue, hfGateList.Value,
                                                              Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strRoomNo);
                        if (dtUserParkingInfo.Rows.Count > 0)
                        {
                            string strNowDay = hfStartDt.Value.Replace("-", "").Substring(6, 2);
                            string strEndDay = string.Empty;
                            string strInsDt = hfStartDt.Value.Replace("-", "");
                            double dblParkingFee = CommValue.NUMBER_VALUE_0_0;
                            double dblMonthlyFee = CommValue.NUMBER_VALUE_0_0;
                            double dblPayedFee = CommValue.NUMBER_VALUE_0_0;
                            int intLoopCnt = CommValue.NUMBER_VALUE_0;


                            // KN_USP_MNG_SELECT_VATINFO_S00
                            DataTable dtVatRatio = VatMngBlo.WatchVatInfo(CommValue.ITEM_TYPE_VALUE_PARKINGFEE);
                            DataTable dtPrintOut = new DataTable();
                            DataTable dtLedgerDet = new DataTable();

                            // 추가일수가 존재할 경우 Loop를 한바퀴 더 돌음.
                            if (Int32.Parse(rdobtnParkingDays.SelectedValue) > CommValue.NUMBER_VALUE_0)
                            {
                                intLoopCnt = Int32.Parse(ddlDuringMonth.SelectedValue) + CommValue.NUMBER_VALUE_1;
                            }
                            else
                            {
                                intLoopCnt = Int32.Parse(ddlDuringMonth.SelectedValue);
                            }

                            // 수납금액 상수화
                            if (!string.IsNullOrEmpty(txtParkingFee.Text))
                            {
                                dblParkingFee = double.Parse(txtParkingFee.Text.Replace(",", ""));
                            }

                            // 월정금액 처리
                            if (!string.IsNullOrEmpty(txtHfMonthlyFee.Text))
                            {
                                dblMonthlyFee = double.Parse(txtHfMonthlyFee.Text);
                            }

                            // 주차카드비 정산테이블에 입력
                            double dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                            double dblItemTotViAmt = CommValue.NUMBER_VALUE_0_0;
                            double dblDongToDollar = CommValue.NUMBER_VALUE_0_0;
                            double dblUniPrime = CommValue.NUMBER_VALUE_0_0;
                            double dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                            int intPaymentSeq = CommValue.NUMBER_VALUE_0;
                            int intPaymentDetSeq = CommValue.NUMBER_VALUE_0;
                            int intItemSeq = CommValue.NUMBER_VALUE_0;

                            if (!string.IsNullOrEmpty(hfRealBaseRate.Text) && !string.IsNullOrEmpty(strCardFee))
                            {
                                dblDongToDollar = double.Parse(hfRealBaseRate.Text);
                                dblItemTotViAmt = double.Parse(strCardFee) + dblParkingFee;

                                if (dblDongToDollar > 0d)
                                {
                                    dblItemTotEnAmt = dblItemTotViAmt / dblDongToDollar;
                                }
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
                            string strPaymentDt = txtPayDt.Text.Replace("-", "").Replace(".", "");

                            // 3. 주차비 및 카드비 처리 원장등록
                            // KN_USP_SET_INSERT_LEDGERINFO_S00
                            DataTable dtLedgerAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strPaymentDt, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, strParkingItemCd,
                                                                                       CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, strUserSeq, string.Empty,
                                                                                       dblDongToDollar, dblItemTotEnAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio,
                                                                                       Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                            if (dtLedgerAccnt != null)
                            {
                                if (dtLedgerAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["PaymentSeq"].ToString());
                                    intItemSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["ItemSeq"].ToString());

                                    // 계좌이체시 관련 정보 등록
                                    if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                                    {
                                        // 4. 납입 계좌정보 등록
                                        // KN_USP_SET_INSERT_LEDGERINFO_S01
                                        //BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strNowDt, intPaymentSeq, ddlTransfer.SelectedValue);
                                        BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, ddlTransfer.SelectedValue);
                                    }
                                }
                            }

                            // 개월수만큼 Loop 돌음
                            for (int intTmpI = 0; intTmpI < intLoopCnt; intTmpI++)
                            {
                                // 시작일자에 따른 처리
                                // 첫 Loop를 제외한 나머지는 매월 1일로 시작해야 함.
                                if (intTmpI != CommValue.NUMBER_VALUE_0)
                                {
                                    dtNowDate = DateTime.ParseExact((TextLib.MakeDateEightDigit(hfStartDt.Value.Replace("-", ""))).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);
                                    dtNowDate = dtNowDate.AddMonths(intTmpI);
                                    strStartDt = dtNowDate.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                    strCardFee = CommValue.NUMBER_VALUE_ZERO;

                                    // 월정 주차 금액 처리
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
                                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S02
                                    strStartDt = hfStartDt.Value.Replace("-", "");
                                    DataTable dtParkReturn = ParkingMngBlo.SpreadParkingFeeInfoList(ddlRegRentCd.SelectedValue, ddlCarTy.SelectedValue, strStartDt);

                                    dblPayedFee = double.Parse(TextLib.MakeRoundDownThousand(double.Parse(dtParkReturn.Rows[0]["ParkingFee"].ToString())).ToString());

                                    // 월정 주차 금액 처리
                                    if (dblParkingFee > dblPayedFee)
                                    {
                                        dblParkingFee = dblParkingFee - dblPayedFee;
                                    }
                                    else
                                    {
                                        dblPayedFee = dblParkingFee;
                                        dblParkingFee = CommValue.NUMBER_VALUE_0_0;
                                    }

                                    //dblPayedFee = dblMonthlyFee;
                                }

                                // 마지막 Loop에서 추가일수에 따른 처리
                                // 마지막 일자에 대한 처리
                                if (intTmpI + CommValue.NUMBER_VALUE_1 == intLoopCnt)
                                {
                                    strEndDt = hfEndDt.Value.Replace("-", "");
                                }
                                else
                                {
                                    dtNowDate = DateTime.ParseExact((TextLib.MakeDateEightDigit(hfStartDt.Value.Replace("-", ""))).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);
                                    dtNowDate = (dtNowDate.AddMonths(intTmpI + CommValue.NUMBER_VALUE_1)).AddDays(-1);
                                    strEndDt = dtNowDate.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                }

                                // 5. 월정주차목록에 등록
                                // 6. 월정주차 캘린더 생성
                                // KN_USP_PRK_INSERT_PARKINGFEEINFO_M00
                                ParkingMngBlo.RegistryUserParkingCardFeeInfo(strRentCd, strCardNo, Int32.Parse(strFloorNo), strRoomNo, strTagNo,
                                                                             strCarNo, strCarTy, double.Parse(strCardFee), dblPayedFee, strPaymentCd,
                                                                             strStartDt, strEndDt, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strPaymentDt);

                                //6.1-- Insert HoaDonParkingAPT ---- Add by phuongtv
                                //KN_USP_PRK_INSERT_HOADONPARKING_APT_I00
                                ParkingMngBlo.RegistryHoaDonParkingApt(strRentCd, strTagNo, strStartDt, strEndDt);

                                // 7. 원장 상세 테이블 처리
                                // 주차카드비 처리
                                if (intTmpI == CommValue.NUMBER_VALUE_0 && !strCardFee.Equals(CommValue.NUMBER_VALUE_ZERO))
                                {
                                    dblUniPrime = double.Parse(strCardFee) * (100) / (100 + dblVatRatio);

                                    // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                    dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                      strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                      CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_SINGLEITEM, dblUniPrime, dblUniPrime, double.Parse(strCardFee), double.Parse(strCardFee),
                                                                                      CommValue.NUMBER_VALUE_0, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), txtParkingCardNo.Text, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                      Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                                    if (dtLedgerDet != null)
                                    {
                                        if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                        {
                                            intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                        }
                                    }

                                    // 8. 출력 테이블에 등록
                                    // KN_USP_SET_INSERT_PRINTINFO_S00
                                    dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                                      Int32.Parse(strFloorNo), strRoomNo, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                      Session["MemNo"].ToString(), strCarNo + " Parking Card Fee ( " + strCardNo + " )",
                                                                                      double.Parse(strCardFee), double.Parse(hfRealBaseRate.Text),
                                                                                      Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);

                                    if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                                    {
                                        strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                        strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                        // 9. 출력 정보 원장상세 테이블에 등록
                                        // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                        BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));

                                        // 10. 출력자 테이블에 등록
                                        // KN_USP_SET_INSERT_PRINTINFO_S01
                                        ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                    }

                                    // 11.금액 로그 테이블 처리
                                    // KN_USP_SET_INSERT_MONEYINFO_M00
                                    ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);
                                }

                                dblUniPrime = dblPayedFee * (100) / (100 + dblVatRatio);

                                // 7. 원장 상세 테이블 처리
                                // 주차비 처리
                                // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                   strDirectCd, strParkingItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                   CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblPayedFee, dblPayedFee,
                                                                                   CommValue.NUMBER_VALUE_0, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), txtCarNo.Text, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                   Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);


                                //---------------Add by BaoTV-------------------
                                //KN_USP_MNG_INSERT_RENOVATIONINFO_M00
                                if (string.IsNullOrEmpty(strCardCost))
                                {
                                    strCardCost = txtCardFee.Text;
                                    if (dbCardCost > 0)
                                    {
                                        var objReturn = MngPaymentBlo.InsertRenovationInfoApt(strPaymentCd, bankcd, txtRegRoomNo.Text, "0007", strPaymentDt, "0", "", dbCardCost, Session["MemNo"].ToString(), strInsMemIP, strCardNo);
                                    }
                                }
                                //-----------------------------------------------

                                if (dtLedgerDet != null)
                                {
                                    if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                    {
                                        intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                    }
                                }

                                // 8. 출력 테이블에 등록
                                // KN_USP_SET_INSERT_PRINTINFO_S00
                                dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                                   Int32.Parse(strFloorNo), strRoomNo, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                   Session["MemNo"].ToString(), strStartDt.Substring(0, 4) + " / " + strStartDt.Substring(4, 2) + " Parking Fee ( " + txtCarNo.Text + " )",
                                                                                   dblPayedFee, double.Parse(hfRealBaseRate.Text),
                                                                                   Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);

                                if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                                {
                                    strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                    strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                    // 9. 출력 정보 원장상세 테이블에 등록
                                    // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                    BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));

                                    // 10. 출력자 테이블에 등록
                                    // KN_USP_SET_INSERT_PRINTINFO_S01
                                    ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                }

                                // 11.금액 로그 테이블 처리
                                // KN_USP_SET_INSERT_MONEYINFO_M00
                                ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);
                            }

                            // 12.주차카드 관리 업체에 등록

                            if (rbMoneyFree.SelectedValue == "Y")
                            {
                                dblItemTotViAmt = 875000.0;

                            }


                            if (!string.IsNullOrEmpty(hfGateList.Value))
                            {
                                intGateCnt = Int32.Parse(hfGateList.Value);
                            }

                            // 오토바이 게이트 등록요청 여부 체크
                            if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE))
                            {
                                // KN_USP_PRK_UPDATE_PARKINGFEEINFO_M02
                                //ParkingMngBlo.RegistryAUTOParkingSystemInfo(strCardNo, hfStartDt.Value.Replace("-", "").Replace("/", ""), hfEndDt.Value.Replace("-", "").Replace("/", ""), dblItemTotViAmt, intLoopCnt);

                                // 차감
                                intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE);
                            }

                            // 오피스 리테일 게이트 등록요청 여부 체크
                            if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL))
                            {
                                // KN_USP_PRK_UPDATE_PARKINGFEEINFO_M01
                                ParkingMngBlo.ModifyORParkingSystemInfo(strCardNo, hfStartDt.Value.Replace("-", "").Replace("/", ""), hfEndDt.Value.Replace("-", "").Replace("/", ""), dblItemTotViAmt, intLoopCnt);

                                // 차감
                                intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL);
                            }

                            // 아파트 게이트 등록요청 여부 체크
                            if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_APARTMENT))
                            {
                                // KN_USP_PRK_UPDATE_PARKINGFEEINFO_M00
                                ParkingMngBlo.ModifyParkingSystemInfo(strCardNo, hfStartDt.Value.Replace("-", "").Replace("/", ""), hfEndDt.Value.Replace("-", "").Replace("/", ""), dblItemTotViAmt, intLoopCnt);

                                // 차감
                                intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_APARTMENT);
                            }

//                            #region brecus
//                            string brecusConn = ConfigurationManager.ConnectionStrings["BrecusParkDB"].ConnectionString;
//                            string Insert_tbl_customer = @"
//                                                              insert into PerfectParking.dbo.tbl_customer 
//                                                              ([CustomerCode]
//                                                              ,[CustomerName]
//                                                              ,[Address]
//                                                              ,[TaxCode]
//                                                              ,[Gender]
//                                                              ,[Phone]
//                                                              ,[CreateDate])
//                                                     Values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')";
//                            string Insert_tbl_cardholder = @"insert into PerfectParking.[dbo].[Tbl_CardHolder]
//                                                                                 ( [CardID],
//                                                                                [TagNo],
//                                                                                [CardTypeID],
//                                                                                [CustomerID],
//                                                                                [IssueDate],
//                                                                                [ExpiryDate],
//                                                                                [CreateDate] )
//                                                                                Values ('{0}','{1}','{2}',(select ID from tbl_customer where customercode= '{3}'),'{4}','{5}','{6}')";
//                            string Insert_Tbl_CustomerVehicle = @"insert into [PerfectParking].[dbo].[Tbl_CustomerVehicle]
//                                                                         (
//	                                                                        [CustomerID],
//	                                                                        [VehicleType],
//	                                                                        [VehicleLPN]
//                                                                         )
//                                                                         Values ((select ID from tbl_customer where customercode= '{0}'),'{1}','{2}')";
//                            string SelectCustomerExists = "select * from tbl_customer where customercode = '{0}'";
//                            SqlConnection sqlconn = new SqlConnection(brecusConn);
//                            SqlCommand cmd = new SqlCommand();
//                            try
//                            {
//                                sqlconn.Open();
//                                //insert into tbl_customer
//                                cmd.Connection = sqlconn;
//                                string stgender = "0";
//                                cmd.CommandText = string.Format(SelectCustomerExists, strUserSeq);
//                                var x = cmd.ExecuteScalar();
//                                if (x == null)
//                                {
//                                    switch (strUserGender)
//                                    {
//                                        case "M":
//                                            stgender = "0";
//                                            break;
//                                        case "F":
//                                            stgender = "1";
//                                            break;
//                                    }
//                                    cmd.CommandText = string.Format(Insert_tbl_customer, strUserSeq, strUserNm, strUserAddress, strUserTaxcd, stgender, strUserPhoneNo, DateTime.Now.ToString());
//                                    cmd.ExecuteNonQuery();
//                                }

//                                //insert into tbl_cardholder
//                                string selectCardExists = "select * from [dbo].[Tbl_CardHolder] where tagno='{1}'";
//                                string enddtIns = DateTime.ParseExact(strEndDt, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToShortDateString();
//                                cmd.CommandText = string.Format(selectCardExists, cardID, strTagNo);
//                                var cardexists = cmd.ExecuteScalar();
//                                if (cardexists == null)
//                                {
//                                    cmd.CommandText = string.Format(Insert_tbl_cardholder, cardID, strTagNo, cartype, strUserSeq, strStartDt, enddtIns, DateTime.Now.ToString());
//                                    cmd.ExecuteNonQuery();
//                                }
//                                else
//                                {
//                                    string updateExpireDate = "update [dbo].[Tbl_CardHolder] set [ExpiryDate] = '{0}' where tagno='{1}'";
//                                    cmd.CommandText = string.Format(updateExpireDate, enddtIns, strTagNo);
//                                    cmd.ExecuteNonQuery();
//                                }

//                                //insert into [Tbl_CustomerVehicle]
//                                cmd.CommandText = string.Format(Insert_Tbl_CustomerVehicle, strUserSeq, cartype, carID);
//                                cmd.ExecuteNonQuery();
//                            }
//                            catch (Exception ex)
//                            {

//                            }
//                            #endregion


                            ResetSearchControls();

                            txtInsRoomNo.Text = txtRegRoomNo.Text;

                            ResetInputControls();

                            LoadData();

                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("window.open('/Common/RdPopup/RDPopupReciptParkingFee.aspx?Datum0=" + strPrintSeq + "&Datum1=0" + "&Datum2=" + paymentDT + "&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");
                            //sbList.Append("window.open('/Common/RdPopup/RDPopupReciptDetail.aspx?Datum0=" + strPrintSeq + "&Datum1=0&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingFee", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            StringBuilder sbWarning = new StringBuilder();
                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["INFO_CANT_INSERT_DEPTH"]);
                            sbWarning.Append("');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                            ResetInputControls();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 섹션 변경시 처리
        /// Autopostback의 폐단에 의한 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnRentChange_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string strRentCd = ddlRegRentCd.SelectedValue;
                string strRoomNo = txtRegRoomNo.Text;
                string strSearchDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                // KN_USP_RES_SELECT_ROOMINFO_S00
                DataTable drRoomCnt = RoomMngBlo.WatchExigstRoomInfo(strRentCd, strRoomNo, strSearchDt);

                if (drRoomCnt != null)
                {
                    if (drRoomCnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        if (Int32.Parse(drRoomCnt.Rows[0]["ExistCnt"].ToString()) == CommValue.NUMBER_VALUE_0)
                        {
                            // 해당 호실이 없는 경우
                            txtRegRoomNo.Text = string.Empty;
                            hfUserSeq.Value = string.Empty;
                            hfFloorNo.Value = string.Empty;

                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");
                            sbList.Append("document.getElementById('" + txtRegRoomNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoRoomAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // 해당 호실이 있는 경우
                            // KN_USP_RES_SELECT_ROOMINFO_S01
                            DataTable dtRoomUser = RoomMngBlo.WatchRoomUserInfo(strRentCd, strRoomNo, strSearchDt);

                            if (dtRoomUser != null)
                            {
                                // CheckBox 자동처리
                                if (!ddlRegRentCd.SelectedValue.Equals(CommValue.CODE_VALUE_EMPTY))
                                {
                                    if (ddlRegRentCd.SelectedValue.Equals(CommValue.RENTAL_VALUE_APTA) ||
                                        ddlRegRentCd.SelectedValue.Equals(CommValue.RENTAL_VALUE_APTB) ||
                                        ddlRegRentCd.SelectedValue.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                                        ddlRegRentCd.SelectedValue.Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                                    {
                                        chkGateList.SelectedValue = CommValue.GATE_VALUE_APARTMENT;
                                        hfGateList.Value = CommValue.GATE_VALUE_APARTMENT;
                                    }
                                    else
                                    {
                                        chkGateList.SelectedValue = CommValue.GATE_VALUE_OFFICERETAIL;
                                        hfGateList.Value = CommValue.GATE_VALUE_OFFICERETAIL;
                                    }
                                }

                                if (dtRoomUser.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    hfUserSeq.Value = dtRoomUser.Rows[0]["UserSeq"].ToString();
                                    hfFloorNo.Value = dtRoomUser.Rows[0]["FloorNo"].ToString();

                                    StringBuilder sbList = new StringBuilder();
                                    sbList.Append("document.getElementById('" + txtCarNo.ClientID + "').focus();");

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoRoomAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 차량번호 변경시 처리
        /// Autopostback의 폐단에 의한 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnCardNoChange_Click(object sender, ImageClickEventArgs e)
        {
            string strParkingCardNo = string.Empty;

            if (!string.IsNullOrEmpty(txtParkingCardNo.Text) && !ddlCarTy.SelectedValue.Equals(CommValue.CODE_VALUE_EMPTY))
            {
                // KN_USP_PRK_SELECT_PARKINGTAGLISTINFO_S02
                DataTable dtReturn = ParkingMngBlo.WatchExgistParkingTagListInfo(txtParkingCardNo.Text, ddlCarTy.SelectedValue);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        if (dtReturn.Rows[0]["StatusYn"].Equals("R"))
                        {
                            txtParkingCardNo.Text = string.Empty;
                            hfParkingTagNo.Value = string.Empty;

                            // (R)emoved card임
                            StringBuilder sbWarning = new StringBuilder();
                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["ALERT_REMOVED_CARD"]);
                            sbWarning.Append("');");
                            sbWarning.Append("document.getElementById('" + txtParkingCardNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Removed", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else if (dtReturn.Rows[0]["StatusYn"].Equals("U"))
                        {
                            // (U)sed card임
                            txtParkingCardNo.Text = string.Empty;
                            hfParkingTagNo.Value = string.Empty;

                            StringBuilder sbWarning = new StringBuilder();
                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["ALERT_ISSUED_CARD"]);
                            sbWarning.Append("');");
                            sbWarning.Append("document.getElementById('" + txtParkingCardNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Issued", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else if (dtReturn.Rows[0]["StatusYn"].Equals("X"))
                        {
                            txtParkingCardNo.Text = string.Empty;
                            hfParkingTagNo.Value = string.Empty;

                            // not e(X)ist card임.
                            StringBuilder sbWarning = new StringBuilder();
                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["ALERT_USELESS_CARD"]);
                            sbWarning.Append("');");
                            sbWarning.Append("document.getElementById('" + txtParkingCardNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Useless", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // CheckBox 자동처리
                            if (!ddlRegRentCd.SelectedValue.Equals(CommValue.CODE_VALUE_EMPTY))
                            {
                                if (ddlRegRentCd.SelectedValue.Equals(CommValue.RENTAL_VALUE_APTA) ||
                                    ddlRegRentCd.SelectedValue.Equals(CommValue.RENTAL_VALUE_APTB) ||
                                    ddlRegRentCd.SelectedValue.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                                    ddlRegRentCd.SelectedValue.Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                                {
                                    chkGateList.SelectedValue = CommValue.GATE_VALUE_APARTMENT;
                                    hfGateList.Value = CommValue.GATE_VALUE_APARTMENT;
                                }
                                else
                                {
                                    chkGateList.SelectedValue = CommValue.GATE_VALUE_OFFICERETAIL;
                                    hfGateList.Value = CommValue.GATE_VALUE_OFFICERETAIL;
                                }
                            }

                            if (ddlCarTy.SelectedValue.Equals(CommValue.CARTY_VALUE_VEHICLE))
                            {
                                // 등록하려는 차종이 자동차일경우 오토바이 게이트 Check Cancel
                                chkGateList.Items[CommValue.NUMBER_VALUE_2].Selected = CommValue.AUTH_VALUE_FALSE;
                            }
                            else
                            {
                                // 등록하려는 차종이 오토바일경우 아파트, 리테일/오피스 Check Cancel
                                chkGateList.Items[CommValue.NUMBER_VALUE_0].Selected = CommValue.AUTH_VALUE_FALSE;
                                chkGateList.Items[CommValue.NUMBER_VALUE_1].Selected = CommValue.AUTH_VALUE_FALSE;
                                chkGateList.Items[CommValue.NUMBER_VALUE_2].Selected = CommValue.AUTH_VALUE_TRUE;
                                hfGateList.Value = CommValue.GATE_VALUE_MOTORBIKE;
                            }


                            CalculateNumber();
                            hfParkingTagNo.Value = dtReturn.Rows[0]["TagNo"].ToString();

                            // 정상적인 카드임.
                            StringBuilder sbWarning = new StringBuilder();
                            sbWarning.Append("document.getElementById('" + ddlDuringMonth.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OkCard", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                    }
                    else
                    {
                        txtParkingCardNo.Text = string.Empty;
                        hfParkingTagNo.Value = string.Empty;
                    }
                }
                else
                {
                    txtParkingCardNo.Text = string.Empty;
                    hfParkingTagNo.Value = string.Empty;
                }
            }
            else
            {
                if (ddlCarTy.SelectedValue.Equals(CommValue.CODE_VALUE_EMPTY))
                {
                    txtParkingFee.Text = string.Empty;
                    txtTotalFee.Text = string.Empty;

                    StringBuilder sbWarning = new StringBuilder();
                    sbWarning.Append("document.getElementById('" + ddlCarTy.ClientID + "').focus();");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CheckMoney", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    CalculateNumber();

                    StringBuilder sbWarning = new StringBuilder();
                    sbWarning.Append("document.getElementById('" + txtParkingCardNo.ClientID + "').focus();");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CheckMoney", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
        }

        protected void txtCardFee_TextChanged(object sender, EventArgs e)
        {
            MakeCalculate();
        }

        protected void txtParkingFee_TextChanged(object sender, EventArgs e)
        {
            MakeCalculate();
        }

        protected void chkGateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int intGateCnt = CommValue.NUMBER_VALUE_0;

            for (int intTmpI = 0; intTmpI < 3; intTmpI++)
            {
                if (chkGateList.Items[intTmpI].Selected)
                {
                    intGateCnt = intGateCnt + Int32.Parse(chkGateList.Items[intTmpI].Value);
                }
            }

            hfGateList.Value = intGateCnt.ToString().PadLeft(4, '0');
        }

        protected void chkGateCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int intTmpI = CommValue.NUMBER_VALUE_0; intTmpI < lvActMonthParkingCardList.Items.Count; intTmpI++)
            {
                int intGateCnt = CommValue.NUMBER_VALUE_0;

                HiddenField hfGateCd = ((HiddenField)lvActMonthParkingCardList.Items[intTmpI].FindControl("hfGateCd"));
                CheckBoxList chkGateCd = ((CheckBoxList)lvActMonthParkingCardList.Items[intTmpI].FindControl("chkGateCd"));

                for (int intTmpJ = CommValue.NUMBER_VALUE_0; intTmpJ < chkGateCd.Items.Count; intTmpJ++)
                {
                    if (chkGateCd.Items[intTmpJ].Selected)
                    {
                        intGateCnt = intGateCnt + Int32.Parse(chkGateList.Items[intTmpJ].Value);
                    }
                }

                hfGateCd.Value = intGateCnt.ToString().PadLeft(4, '0');
            }
        }

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
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

        public void MakeMonthDdl(DropDownList ddlParams)
        {
            ddlParams.Items.Clear();

            for (int intTmpI = 0; intTmpI < 12; intTmpI++)
            {
                ddlParams.Items.Add(new ListItem((intTmpI + 1).ToString(), (intTmpI + 1).ToString()));
            }
        }

        //public void MakeAccountDdl(DropDownList ddlParams)
        //{
        //    // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
        //    // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
        //    // Utility Fee : Chestnut 매출
        //    // 그외 KeangNam 매출
        //    string strCompCd = string.Empty;

        //    strCompCd = CommValue.SUB_COMP_CD;

        //    DataTable dtReturn = AccountMngBlo.SpreadAccountInfo(strCompCd);

        //    ddlParams.Items.Clear();

        //    ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

        //    foreach (DataRow dr in dtReturn.Select())
        //    {
        //        ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
        //    }
        //}

        protected void MakeCalculate()
        {
            if (string.IsNullOrEmpty(txtCardFee.Text))
            {
                txtCardFee.Text = CommValue.NUMBER_VALUE_ZERO;
            }

            if (string.IsNullOrEmpty(txtParkingFee.Text))
            {
                txtParkingFee.Text = CommValue.NUMBER_VALUE_ZERO;
            }

            txtTotalFee.Text = (Int32.Parse(txtParkingFee.Text.Replace(",", "")) + Int32.Parse(txtCardFee.Text)).ToString("###,##0");

        }

        protected void ResetSearchControls()
        {
            txtInsRoomNo.Text = string.Empty;
            txtInsCardNo.Text = string.Empty;
            txtInsCarNo.Text = string.Empty;
        }

        protected void ResetInputControls()
        {
            ddlRegRentCd.SelectedValue = CommValue.CODE_VALUE_EMPTY;
            txtRegRoomNo.Text = string.Empty;
            txtCarNo.Text = string.Empty;
            hfCarNo.Value = string.Empty;
            ddlCarTy.SelectedValue = CommValue.CODE_VALUE_EMPTY;
            txtParkingCardNo.Text = string.Empty;
            hfUserSeq.Value = string.Empty;
            hfFloorNo.Value = string.Empty;
            hfParkingTagNo.Value = string.Empty;
            chkGateList.Items[0].Selected = CommValue.AUTH_VALUE_FALSE;
            chkGateList.Items[1].Selected = CommValue.AUTH_VALUE_FALSE;
            chkGateList.Items[2].Selected = CommValue.AUTH_VALUE_FALSE;
            hfGateList.Value = string.Empty;
            txtStartDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            hfStartDt.Value = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
            ddlDuringMonth.SelectedValue = CommValue.NUMBER_VALUE_ONE;
            rdobtnParkingDays.SelectedValue = CommValue.CODE_VALUE_EMPTY;

            txtEndDt.Text = (DateTime.ParseExact((TextLib.MakeDateEightDigit(DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", ""))).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null).AddMonths(CommValue.NUMBER_VALUE_1)).AddDays(-1).ToString("s").Substring(0, 10);
            hfEndDt.Value = (DateTime.ParseExact((TextLib.MakeDateEightDigit(DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", ""))).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null).AddMonths(CommValue.NUMBER_VALUE_1)).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
            txtCardFee.Text = string.Empty;
            txtParkingFee.Text = string.Empty;
            txtHfMonthlyFee.Text = string.Empty;
            txtTotalFee.Text = string.Empty;
            ddlPaymentCd.SelectedValue = CommValue.CODE_VALUE_EMPTY;
            ddlTransfer.Enabled = CommValue.AUTH_VALUE_TRUE;
            ddlTransfer.SelectedValue = string.Empty;
            ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;
        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // 가장 최근의 환율을 조회함.
            dtReturn = ExchangeMngBlo.WatchExchangeRateLastInfo(CommValue.RENTAL_VALUE_PARKING);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0") + "&nbsp;" + TextNm["DONG"].ToString());
                        hfRealBaseRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();
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
        }

        protected void txtStartDt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateNumber();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvActMonthParkingCardList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void rbMoneyFree_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtParkingFee.Text = string.Empty;
            txtHfMonthlyFee.Text = string.Empty;

            if (rbMoneyFree.SelectedValue == "Y")
            {
                txtParkingFee.Text = "0";
                MakeCalculate();
            }
            else
            {
                CalculateNumber();
            }
        }

        protected void lnkbtnReport_Click(object sender, EventArgs e)
        {
            try
            {
                var rentcd = string.Empty;
                var room = string.Empty;
                var carno = string.Empty;
                var cardno = string.Empty;
                var langcd = Session["LangCd"].ToString();

                rentcd = ddlInsRentCd.SelectedValue;
                room = txtInsRoomNo.Text;
                carno = txtInsCarNo.Text;
                cardno = txtInsCardNo.Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnAccountList('" + rentcd + "','" + room + "','" + carno + "','" + cardno + "','" + langcd + "');", CommValue.AUTH_VALUE_TRUE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void importExcel()
        {
            try
            {
                #region define abc
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                string strDirectCd = CommValue.DIRECT_TYPE_VALUE_DIRECT;
                string strItemCd = CommValue.ITEM_TYPE_VALUE_PARKINGCARDFEE;
                string strParkingItemCd = CommValue.ITEM_TYPE_VALUE_PARKINGFEE;

                //string strUserSeq = hfUserSeq.Value;
                string strRentCd = ddlRegRentCd.SelectedValue;
                string strCardFee = string.Empty;
                //string strFloorNo = hfFloorNo.Value;
                string strRoomNo = txtRegRoomNo.Text;
                string strCarNo = txtCarNo.Text;
                string strCarTy = ddlCarTy.SelectedValue;
                string strDuringMonth = ddlDuringMonth.SelectedValue;
                string strStartDt = hfStartDt.Value;
                string strEndDt = hfEndDt.Value;
                string strPaymentCd = ddlPaymentCd.Text;
                string strCardNo = txtParkingCardNo.Text;
                string strTagNo = hfParkingTagNo.Value;
                string strVatRatio = string.Empty;

                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;
                string paymentDT = txtPayDt.Text;

                string strCardCost = string.Empty;

                DataTable dtUser = ParkingMngBlo.SelectUserSeqByRoomNo(strRoomNo);
                string strUserSeq = dtUser.Rows[0]["UserSeq"].ToString();
                string strFloorNo = dtUser.Rows[0]["FloorNo"].ToString();
                var bankcd = Int32.Parse(ddlTransfer.SelectedValue != "" ? ddlTransfer.SelectedValue : "0");
                var dbCardCost = double.Parse(txtCardFee.Text != "" ? txtCardFee.Text : "0");

                txtParkingFee.Text = string.Empty;
                txtHfMonthlyFee.Text = string.Empty;

                if (rbMoneyFree.SelectedValue == "Y")
                {
                    txtParkingFee.Text = "0";
                    MakeCalculate();
                }
                else
                {
                    CalculateNumber();
                }
                int intGateCnt = CommValue.NUMBER_VALUE_1;
                if (string.IsNullOrEmpty(txtCardFee.Text))
                {
                    strCardFee = "0";
                }
                else
                {
                    strCardFee = txtCardFee.Text;
                }
                #endregion
                //get card tagno
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
                //if cardno, tagno, carno is not empty -> regist infomation
                #region create new car regist info
                if (!string.IsNullOrEmpty(strCardNo) && !string.IsNullOrEmpty(strTagNo) && !string.IsNullOrEmpty(strCarNo))
                {
                    DateTime dtNowDate;
                    string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S03
                    //check card has regist
                    DataTable dtReturn = ParkingMngBlo.SpreadExgistParkingCardInfo(strCardNo, strCarNo);
                    DataTable dtUserParkingInfo = new DataTable();
                    //if exist - raise alert
                    if (dtReturn.Rows.Count > 0)
                    {
                        StringBuilder sbWarning = new StringBuilder();
                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["ALERT_ISSUED_CARD"]);
                        sbWarning.Append("');");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        ResetSearchControls();
                    }//else begin insert
                    else
                    {
                        //1. chua co thong tin thi insert vao bang userparkinginfo, update parkingtaglistinfo thanh issued - yes
                        // KN_USP_PRK_INSERT_USERPARKINGINFO_M00
                        dtUserParkingInfo = ParkingMngBlo.RegistryUserParkingInfo(strUserSeq, strTagNo, strCardNo, strCarNo, ddlCarTy.SelectedValue, hfGateList.Value,
                                                              Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strRoomNo);
                        if (dtUserParkingInfo.Rows.Count > 0)
                        {
                            string strNowDay = hfStartDt.Value.Replace("-", "").Substring(6, 2);
                            string strEndDay = string.Empty;
                            string strInsDt = hfStartDt.Value.Replace("-", "");
                            double dblParkingFee = CommValue.NUMBER_VALUE_0_0;
                            double dblMonthlyFee = CommValue.NUMBER_VALUE_0_0;
                            double dblPayedFee = CommValue.NUMBER_VALUE_0_0;
                            int intLoopCnt = CommValue.NUMBER_VALUE_0;
                            // KN_USP_MNG_SELECT_VATINFO_S00
                            DataTable dtVatRatio = VatMngBlo.WatchVatInfo(CommValue.ITEM_TYPE_VALUE_PARKINGFEE);
                            DataTable dtPrintOut = new DataTable();
                            DataTable dtLedgerDet = new DataTable();
                            
                            if (Int32.Parse(rdobtnParkingDays.SelectedValue) > CommValue.NUMBER_VALUE_0)
                                intLoopCnt = Int32.Parse(ddlDuringMonth.SelectedValue) + CommValue.NUMBER_VALUE_1;
                            else
                                intLoopCnt = Int32.Parse(ddlDuringMonth.SelectedValue);
                            
                            if (!string.IsNullOrEmpty(txtParkingFee.Text))
                                dblParkingFee = double.Parse(txtParkingFee.Text.Replace(",", ""));
                            
                            if (!string.IsNullOrEmpty(txtHfMonthlyFee.Text))
                                dblMonthlyFee = double.Parse(txtHfMonthlyFee.Text);
                            
                            double dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                            double dblItemTotViAmt = CommValue.NUMBER_VALUE_0_0;
                            double dblDongToDollar = CommValue.NUMBER_VALUE_0_0;
                            double dblUniPrime = CommValue.NUMBER_VALUE_0_0;
                            double dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                            int intPaymentSeq = CommValue.NUMBER_VALUE_0;
                            int intPaymentDetSeq = CommValue.NUMBER_VALUE_0;
                            int intItemSeq = CommValue.NUMBER_VALUE_0;
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
                            string strPaymentDt = txtPayDt.Text.Replace("-", "").Replace(".", "");
                            // 2. insert  LedgerInfo
                            // KN_USP_SET_INSERT_LEDGERINFO_S00
                            DataTable dtLedgerAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strPaymentDt, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, strParkingItemCd,
                                                                                       CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, strUserSeq, string.Empty,
                                                                                       dblDongToDollar, dblItemTotEnAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio,
                                                                                       Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                            if (dtLedgerAccnt != null)
                            {
                                if (dtLedgerAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["PaymentSeq"].ToString());
                                    intItemSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["ItemSeq"].ToString());
                                    
                                    if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                                    {
                                        // KN_USP_SET_INSERT_LEDGERINFO_S01
                                        BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, ddlTransfer.SelectedValue);
                                    }
                                }
                            }
                            // lap theo tung thang insert chi tiet tung thang
                            #region tien nop chi tiet tung thang
                            for (int intTmpI = 0; intTmpI < intLoopCnt; intTmpI++)
                            {
                                if (intTmpI != CommValue.NUMBER_VALUE_0)
                                {
                                    #region luoi doc dai khai la ko phai phat dau tien
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
                                    #endregion
                                }
                                else
                                {
                                    #region lay thong tin shot thu 2 tro len tu bang MonthParkingFeeInfo
                                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S02
                                    strStartDt = hfStartDt.Value.Replace("-", "");
                                    DataTable dtParkReturn = ParkingMngBlo.SpreadParkingFeeInfoList(ddlRegRentCd.SelectedValue, ddlCarTy.SelectedValue, strStartDt);
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
                                    strEndDt = hfEndDt.Value.Replace("-", "");
                                }
                                else
                                {
                                    dtNowDate = DateTime.ParseExact((TextLib.MakeDateEightDigit(hfStartDt.Value.Replace("-", ""))).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);
                                    dtNowDate = (dtNowDate.AddMonths(intTmpI + CommValue.NUMBER_VALUE_1)).AddDays(-1);
                                    strEndDt = dtNowDate.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                                }
                                // KN_USP_PRK_INSERT_PARKINGFEEINFO_M00
                                ParkingMngBlo.RegistryUserParkingCardFeeInfo(strRentCd, strCardNo, Int32.Parse(strFloorNo), strRoomNo, strTagNo,
                                                                             strCarNo, strCarTy, double.Parse(strCardFee), dblPayedFee, strPaymentCd,
                                                                             strStartDt, strEndDt, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strPaymentDt);
                                //6.1-- Insert HoaDonParkingAPT ---- Add by phuongtv
                                //KN_USP_PRK_INSERT_HOADONPARKING_APT_I00
                                ParkingMngBlo.RegistryHoaDonParkingApt(strRentCd, strTagNo, strStartDt, strEndDt);
                                if (intTmpI == CommValue.NUMBER_VALUE_0 && !strCardFee.Equals(CommValue.NUMBER_VALUE_ZERO))
                                {
                                    dblUniPrime = double.Parse(strCardFee) * (100) / (100 + dblVatRatio);
                                    // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                    dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                      strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                      CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_SINGLEITEM, dblUniPrime, dblUniPrime, double.Parse(strCardFee), double.Parse(strCardFee),
                                                                                      CommValue.NUMBER_VALUE_0, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), txtParkingCardNo.Text, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                      Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                    if (dtLedgerDet != null)
                                    {
                                        if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                            intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                    }
                                    // KN_USP_SET_INSERT_PRINTINFO_S00
                                    dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                                      Int32.Parse(strFloorNo), strRoomNo, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                      Session["MemNo"].ToString(), strCarNo + " Parking Card Fee ( " + strCardNo + " )",
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
                                dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                   strDirectCd, strParkingItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                   CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblPayedFee, dblPayedFee,
                                                                                   CommValue.NUMBER_VALUE_0, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), txtCarNo.Text, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                   Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                //---------------Add by BaoTV-------------------
                                //KN_USP_MNG_INSERT_RENOVATIONINFO_M00
                                if (string.IsNullOrEmpty(strCardCost))
                                {
                                    strCardCost = txtCardFee.Text;
                                    if (dbCardCost > 0)
                                    {
                                        var objReturn = MngPaymentBlo.InsertRenovationInfoApt(strPaymentCd, bankcd, txtRegRoomNo.Text, "0007", strPaymentDt, "0", "", dbCardCost, Session["MemNo"].ToString(), strInsMemIP, strCardNo);
                                    }
                                }
                                //-----------------------------------------------
                                if (dtLedgerDet != null)
                                {
                                    if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                    {
                                        intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                    }
                                }
                                // KN_USP_SET_INSERT_PRINTINFO_S00
                                dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_PARKINGFEE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                                   Int32.Parse(strFloorNo), strRoomNo, strStartDt.Substring(0, 4), strStartDt.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                   Session["MemNo"].ToString(), strStartDt.Substring(0, 4) + " / " + strStartDt.Substring(4, 2) + " Parking Fee ( " + txtCarNo.Text + " )",
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
                            // 12.주차카드 관리 업체에 등록
                            if (rbMoneyFree.SelectedValue == "Y")
                            {
                                dblItemTotViAmt = 875000.0;
                            }
                            if (!string.IsNullOrEmpty(hfGateList.Value))
                            {
                                intGateCnt = Int32.Parse(hfGateList.Value);
                            }
                            if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE))
                            {
                                intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE);
                            }
                            if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL))
                            {
                                // KN_USP_PRK_UPDATE_PARKINGFEEINFO_M01
                                ParkingMngBlo.ModifyORParkingSystemInfo(strCardNo, hfStartDt.Value.Replace("-", "").Replace("/", ""), hfEndDt.Value.Replace("-", "").Replace("/", ""), dblItemTotViAmt, intLoopCnt);
                                intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL);
                            }
                            if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_APARTMENT))
                            {
                                // KN_USP_PRK_UPDATE_PARKINGFEEINFO_M00
                                ParkingMngBlo.ModifyParkingSystemInfo(strCardNo, hfStartDt.Value.Replace("-", "").Replace("/", ""), hfEndDt.Value.Replace("-", "").Replace("/", ""), dblItemTotViAmt, intLoopCnt);
                                intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_APARTMENT);
                            }
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

        protected void lnkBtnImportExcel_Click(object sender, EventArgs e)
        {

            Response.Redirect("../park/MonthParkingExcelImport.aspx");
        }
    }
}