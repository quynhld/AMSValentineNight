using System;
using System.Data;
using System.IO;
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

namespace KN.Web.Park
{
    public partial class CardCost : BasePage
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
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            ltSearchCardNo.Text = TextNm["PARKINGTAGNO"];
            ltSearchCarNo.Text = TextNm["CARNO"];
            ltSearchRoom.Text = TextNm["ROOMNO"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSearchCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            ltTopYearMM.Text = TextNm["MONTH"];
            ltTopFloorRoom.Text = TextNm["ROOMNO"];
            ltTopCarNo.Text = TextNm["CARNO"];
            ltTopCarTyCd.Text = TextNm["CARTY"];
            ltTopFee.Text = TextNm["PAYMENT"];
            ltTopPaymentCd.Text = TextNm["PAYMETHOD"];
            ltTopPayDt.Text = TextNm["PAYDAY"];

            MakeYearDdl(ddlSearchYear);
            MakeMonthDdlTitle(ddlSearchMonth);
            MakeAccountDdl(ddlTransfer);
            MakeMonthDdl(ddlDuringMonth);

            string strFeeTyTxt = string.Empty;
            string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
            string strNowDay = strNowDt.Substring(6, 2);
            string strEndDt = string.Empty;

            

            DateTime dtEndDate = DateTime.ParseExact(DateTime.Now.ToString("s").Substring(0, 7).ToString() + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

            ltRegRent.Text = TextNm["RENT"];
            ltRegRoomNo.Text = TextNm["ROOMNO"];
            ltRegParkingCarNo.Text = TextNm["CARNO"];
            ltRegParkingCardNo.Text = TextNm["PARKINGTAGNO"];
            ltGateTy.Text = TextNm["GATE"];
            ltRegStartDt.Text = TextNm["COMMENCINGDT"];
            ltRegCarTy.Text = TextNm["CARTY"];
            ltRegCardFee.Text = TextNm["CARDFEE"];
            ltRegParkingFee.Text = "";
            ltRegPaymentCd.Text = TextNm["PAYMENTKIND"];
            ltRegTotalFee.Text = TextNm["ENTIRE"];

            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];

            lnkbtnRegist.Text = TextNm["REGIST"];

            // 섹션코드 조회
            LoadRentDdl(ddlRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            // 차종 조회
            LoadCarTyDdl(ddlCarTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_CARTY);

            MakeCarNoDdl(ddlCarNo, null);

            CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaymentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);
            CommCdRdoUtil.MakeEtcSubCdRdoTitle(rdobtnParkingDays, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_PARKINGDAYS, RepeatDirection.Horizontal);
            CommCdChkUtil.MakeEtcSubCdChkNoTitle(chkGateList, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_GATE, RepeatDirection.Horizontal);

            if (Int32.Parse(strNowDay) >= 15)
            {
                ddlDuringMonth.SelectedValue = CommValue.NUMBER_VALUE_ONE;
            }

            strEndDt = dtEndDate.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue)).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

            LoadExchageDate();

            txtStartDt.Text = TextLib.MakeDateEightDigit(strNowDt);
            txtPayDt.Text = TextLib.MakeDateEightDigit(strNowDt);
            hfStartDt.Value = strNowDt;
            rdobtnParkingDays.SelectedValue = CommValue.PARKINGDAYS_VALUE_00;
            txtEndDt.Text = TextLib.MakeDateEightDigit(strEndDt);
            hfEndDt.Value = strEndDt;

            hfpayDate.Value = txtPayDt.Text;
            ddlPaymentCd.SelectedValue = CommValue.PAYMENT_TYPE_VALUE_CASH;
            ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;

            lnkbtnRegist.OnClientClick = "javascript:return fnRegistCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnReport.Text = "Report" + TextNm["PRINT"];
            lnkbtnReport.Visible = Master.isWriteAuthOk;

            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];
            lnkbtnExcelReport.Visible = Master.isWriteAuthOk;
        }


        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            string strCardNo = string.Empty;

            //strCardNo = txtSearchCardNo.Text;

            //if (!string.IsNullOrEmpty(strCardNo))
            //{
            //    if (strCardNo.Length < 8)
            //    {
            //        strCardNo = txtSearchCardNo.Text.PadLeft(8, '0');
            //    }
            //}

            // KN_USP_PRK_SELECT_ACCOUNTMONTHPARKINGINFO_S00
            dsReturn = ParkingMngBlo.SpreadAccountMonthParkingFeeList(ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue, txtSearchRoom.Text,
                                                                      txtSearchCarNo.Text, txtSearchCardNo.Text, Session["LangCd"].ToString(), ddlCarTy.SelectedValue);

            if (dsReturn != null)
            {
                lvActMonthParkingFeeList.DataSource = dsReturn.Tables[1];
                lvActMonthParkingFeeList.DataBind();
            }

            // ResetSearchControls();
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl(DropDownList ddlParam)
        {
            ddlParam.Items.Clear();
            ddlParam.Items.Add(new ListItem(TextNm["YEARS"], ""));

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.AddYears(1).Year; intTmpI++)
            {
                ddlParam.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdlTitle(DropDownList ddlParam)
        {
            ddlParam.Items.Clear();
            ddlParam.Items.Add(new ListItem(TextNm["MONTH"], ""));

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlParam.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }
        }

        private void MakeMonthDdl(DropDownList ddlParams)
        {
            ddlParams.Items.Clear();

            for (int intTmpI = 0; intTmpI < 12; intTmpI++)
            {
                ddlParams.Items.Add(new ListItem((intTmpI + 1).ToString(), (intTmpI + 1).ToString()));
            }
        }

        private void MakeAccountDdl(DropDownList ddlParams)
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
            // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
            // Utility Fee : Chestnut 매출
            // 그외 KeangNam 매출
            string strCompCd = string.Empty;

            strCompCd = CommValue.SUB_COMP_CD;

            DataTable dtReturn = AccountMngBlo.SpreadAccountInfo(strCompCd);

            ddlParams.Items.Clear();

            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
            }
        }

        ////-------- function get Bank Acc----------------------------
        //public void MakeAccountDdl(DropDownList ddlParams)
        //{
        //    // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
        //    // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
        //    // Utility Fee : Chestnut 매출
        //    // 그외 KeangNam 매출
        //    const string strCompCd = CommValue.MAIN_COMP_CD;
        //    var dtReturn = AccountMngBlo.SpreadBankAccountInfo(strCompCd);

        //    ddlParams.Items.Clear();

        //    ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

        //    foreach (var dr in dtReturn.Select())
        //    {
        //        ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
        //    }
        //}

        private void MakeCarNoDdl(DropDownList ddlParams, DataTable dtParams)
        {
            ddlParams.Items.Clear();
            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], ""));

            if (dtParams != null)
            {
                string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                if (dtParams.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    foreach (DataRow dr in dtParams.Select())
                    {
                        if (!string.IsNullOrEmpty(dr["ParkingCarNo"].ToString()))
                        {
                            ddlParams.Items.Add(new ListItem(dr["ParkingCarNo"].ToString(), dr["ParkingCarNo"].ToString()));
                        }
                        else
                        {
                            ltCarTy.Text = string.Empty;
                            txtHfCarTy.Text = string.Empty;
                            ltParkingCardNo.Text = string.Empty;
                            txtHfParkingCardNo.Text = string.Empty;

                            txtStartDt.Text = TextLib.MakeDateEightDigit(strNowDt);
                            hfStartDt.Value = strNowDt;
                        }
                    }
                }
                else
                {
                    ltCarTy.Text = string.Empty;
                    txtHfCarTy.Text = string.Empty;
                    ltParkingCardNo.Text = string.Empty;
                    txtHfParkingCardNo.Text = string.Empty;

                    txtStartDt.Text = TextLib.MakeDateEightDigit(strNowDt);
                    hfStartDt.Value = strNowDt;
                }
            }
        }

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_SR))
                {
                    ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        protected void LoadCarTyDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.CARTY_VALUE_FREE_EXCEPTION))
                {
                    ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
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

        protected void CalculateNumber()
        {
            string strParkingFee = string.Empty;

            DateTime now = DateTime.Now;
            hfStartDt.Value = txtStartDt.Text;

            string strDate = hfStartDt.Value.Replace("-", "");
            
            string strNowYear = strDate.Substring(0, 4);
            string strNowMonth = strDate.Substring(4, 2);
            string strNowDay = strDate.Substring(6, 2);
            string strDuringMonth = ddlDuringMonth.SelectedValue;
            string strEndDt = string.Empty;
            string strEndDays = string.Empty;

            // 월정 주차비 미등록시에 주차비 등록 페이지로 이동.
            if (!string.IsNullOrEmpty(txtHfCarTy.Text))
            {
                if (!txtHfCarTy.Text.Equals(CommValue.CODE_VALUE_EMPTY))
                {
                    // KN_USP_PRK_SELECT_MONTHPARKINGFEEINFO_S00
                    DataTable dtReturn = ParkingMngBlo.WatchMonthParkingFeeCheck(txtHfRentCd.Text, strNowYear, strNowMonth, txtHfCarTy.Text);

                    if (dtReturn != null)
                    {
                        if (Int32.Parse(dtReturn.Rows[0]["ExistCnt"].ToString()) > 0)
                        {
                            // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S02
                            DataTable dtParkReturn = ParkingMngBlo.SpreadParkingFeeInfoList(txtHfRentCd.Text, txtHfCarTy.Text, strDate);

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
                DateTime dtEndDate = DateTime.ParseExact(TextLib.MakeDateEightDigit(strDate).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);
                DateTime dtEndDays = DateTime.ParseExact(TextLib.MakeDateEightDigit(strDate).Substring(0, 7) + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

                strEndDays = dtEndDays.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue) + 1).AddDays(-1).ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                strEndDays = strEndDays.Substring(6, 2);
                strEndDt = dtEndDate.AddMonths(Int32.Parse(ddlDuringMonth.SelectedValue)).AddDays(-1).AddDays(Int32.Parse(rdobtnParkingDays.SelectedValue)).ToString("s").Substring(0, 10);

                txtEndDt.Text = strEndDt;
                hfEndDt.Value = strEndDt.Replace("/", "").Replace("-", "");

                txtParkingFee.Text = string.Empty;
                txtHfMonthlyFee.Text = string.Empty;
            }

            MakeCalculate();

            string strTmpStartDt = hfStartDt.Value.Replace("-", "");
            txtStartDt.Text = strTmpStartDt.Substring(0, 4) + "-" + strTmpStartDt.Substring(4, 2) + "-" + strTmpStartDt.Substring(6, 2);
        }

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
            txtSearchRoom.Text = string.Empty;
            ddlSearchYear.Text = string.Empty;
            ddlSearchMonth.Text = string.Empty;
            txtSearchRoom.Text = string.Empty;
            txtSearchCarNo.Text = string.Empty;
        }

        protected void ResetInputControls()
        {
            ltRentCd.Text = string.Empty;
            txtHfRentCd.Text = string.Empty;
            txtRegRoomNo.Text = string.Empty;
            ddlCarNo.SelectedValue = string.Empty;
            ltCarTy.Text = string.Empty;
            txtHfCarTy.Text = string.Empty;
            ltParkingCardNo.Text = string.Empty;
            txtHfParkingCardNo.Text = string.Empty;
            hfUserSeq.Value = string.Empty;
            hfFloorNo.Value = string.Empty;
            hfParkingTagNo.Value = string.Empty;
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

        protected void lvActMonthParkingFeeList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvActMonthParkingFeeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                Literal ltYearMM = (Literal)iTem.FindControl("ltYearMM");

                if (!string.IsNullOrEmpty(drView["ParkingYear"].ToString()) && !string.IsNullOrEmpty(drView["ParkingMonth"].ToString()))
                {
                    ltYearMM.Text = drView["ParkingYear"].ToString() + "/" + drView["ParkingMonth"].ToString();
                }

                TextBox txtHfParkingYear = (TextBox)iTem.FindControl("txtHfParkingYear");

                if (!string.IsNullOrEmpty(drView["ParkingYear"].ToString()))
                {
                    txtHfParkingYear.Text = drView["ParkingYear"].ToString();
                }

                TextBox txtHfParkingMonth = (TextBox)iTem.FindControl("txtHfParkingMonth");

                if (!string.IsNullOrEmpty(drView["ParkingMonth"].ToString()))
                {
                    txtHfParkingMonth.Text = drView["ParkingMonth"].ToString();
                }

                TextBox txtHfRentCd = (TextBox)iTem.FindControl("txtHfRentCd");

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    txtHfRentCd.Text = drView["RentCd"].ToString();
                }

                Literal ltFloorRoom = (Literal)iTem.FindControl("ltFloorRoom");
                TextBox txtHfRoomNo = (TextBox)iTem.FindControl("txtHfRoomNo");

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    ltFloorRoom.Text = drView["RoomNo"].ToString();
                    txtHfRoomNo.Text = drView["RoomNo"].ToString();
                }

                Literal ltCarNo = (Literal)iTem.FindControl("ltCarNo");
                TextBox txtHfCardNo = (TextBox)iTem.FindControl("txtHfCardNo");                

                if (!string.IsNullOrEmpty(drView["ParkingCarNo"].ToString()) && !string.IsNullOrEmpty(drView["ParkingCardNo"].ToString()))
                {
                    ltCarNo.Text = drView["ParkingCarNo"].ToString() + " (" + drView["ParkingCardNo"].ToString().Trim() + ")";
                    txtHfCardNo.Text = drView["ParkingCardNo"].ToString().Trim();
                }

                Literal ltCarTyCd = (Literal)iTem.FindControl("ltCarTyCd");

                if (!string.IsNullOrEmpty(drView["CarTyNm"].ToString()))
                {
                    ltCarTyCd.Text = drView["CarTyNm"].ToString();
                }

                TextBox txtHfCarTyCd = (TextBox)iTem.FindControl("txtHfCarTyCd");

                if (!string.IsNullOrEmpty(drView["CarTyCd"].ToString()))
                {
                    txtHfCarTyCd.Text = drView["CarTyCd"].ToString();
                }

                Literal ltPaymentCd = (Literal)iTem.FindControl("ltPaymentCd");

                if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
                {
                    ltPaymentCd.Text = drView["PaymentNm"].ToString();
                }

                Literal ltFee = (Literal)iTem.FindControl("ltFee");

                if (!string.IsNullOrEmpty(drView["ParkingFee"].ToString()))
                {
                    ltFee.Text = drView["ParkingFee"].ToString();
                }

                Literal ltPayDt = (Literal)iTem.FindControl("ltPayDt");
                TextBox txtHfPayDt = (TextBox)iTem.FindControl("txtHfPayDt");

                if (!string.IsNullOrEmpty(drView["PayDt"].ToString()))
                {
                    ltPayDt.Text = TextLib.MakeDateEightDigit(drView["PayDt"].ToString());
                    txtHfPayDt.Text = drView["PayDt"].ToString();
                }

                TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                }

                TextBox txtHfUserDetSeq = (TextBox)iTem.FindControl("txtHfUserDetSeq");

                if (!string.IsNullOrEmpty(drView["UserDetSeq"].ToString()))
                {
                    txtHfUserDetSeq.Text = drView["UserDetSeq"].ToString();
                }

                TextBox txtHfYear = (TextBox)iTem.FindControl("txtHfYear");

                if (!string.IsNullOrEmpty(drView["ParkingYear"].ToString()))
                {
                    txtHfYear.Text = drView["ParkingYear"].ToString();
                }

                TextBox txtHfMM = (TextBox)iTem.FindControl("txtHfMM");

                if (!string.IsNullOrEmpty(drView["ParkingMonth"].ToString()))
                {
                    txtHfMM.Text = drView["ParkingMonth"].ToString();
                }

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");

                if (!string.IsNullOrEmpty(drView["AccountYn"].ToString()))
                {
                    if (drView["AccountYn"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        // 정산이 끝난 데이터는 삭제 버튼 볼수 없음.
                        imgbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        // 정산이 끝나지 않은 데이터는 삭제 버튼 볼수 있음.
                        imgbtnDelete.Visible = CommValue.AUTH_VALUE_TRUE;
                    }
                }
                else
                {
                    // 정산이 끝나지 않은 데이터는 삭제 버튼 볼수 있음.
                    imgbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;
                }

                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["INFO_CANT_DELETE_PART"] + "\\n" + AlertNm["INFO_MUST_DELETE_ENTIRE"] + "\\n" + AlertNm["CONF_PRCEED_WORK"] + "');";
            }
        }

        protected void lvActMonthParkingFeeList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                TextBox txtHfUserSeq = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfUserSeq");
                TextBox txtHfPayDt = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfPayDt");
                TextBox txtHfYear = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfYear");
                TextBox txtHfMM = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfMM");
                TextBox txtHfRoomNo = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfRoomNo");
                TextBox txtHfRentCd = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfRentCd");
                TextBox txtHfCarTyCd = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfCarTyCd");
                TextBox txtHfUserDetSeq = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfUserDetSeq");
                TextBox txtHfCardNo = (TextBox)lvActMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfCardNo");

                string strText = string.Empty;

                // KN_USP_SET_SELECT_LEDGERINFO_S00
                DataSet dsDeleteList = BalanceMngBlo.WatchLedgerMngForParking(txtHfUserSeq.Text, txtHfPayDt.Text, txtHfYear.Text, txtHfMM.Text, Session["LangCd"].ToString());

                //if (dsDeleteList != null)
                //{
                    //if (dsDeleteList.Tables[0] != null && dsDeleteList.Tables[1] != null)
                    //{
                    //    if (dsDeleteList.Tables[0].Rows.Count > CommValue.NUMBER_VALUE_0 && dsDeleteList.Tables[1].Rows.Count > CommValue.NUMBER_VALUE_0)
                    //    {
                            txtHfDelDebitCreditCd.Text = dsDeleteList.Tables[0].Rows[0]["DebitCreditCd"].ToString();
                            txtHfDelPaymentDt.Text = dsDeleteList.Tables[0].Rows[0]["PaymentDt"].ToString();
                            txtHfDelPaymentSeq.Text = dsDeleteList.Tables[0].Rows[0]["PaymentSeq"].ToString();
                            txtHfDelPaymentDetSeq.Text = dsDeleteList.Tables[0].Rows[0]["PaymentDetSeq"].ToString();
                            txtHfDelUserSeq.Text = txtHfUserSeq.Text;
                            txtHfDelRoomNo.Text = txtHfRoomNo.Text;
                            txtHfDelRentCd.Text = txtHfRentCd.Text;
                            txtHfDelCardNo.Text = txtHfCardNo.Text;
                            txtHfDelCarTyCd.Text = txtHfCarTyCd.Text;
                            txtHfDelUserDetSeq.Text = txtHfUserDetSeq.Text;

                            foreach (DataRow dr in dsDeleteList.Tables[1].Select())
                            {
                                strText = strText + dr["SvcYear"].ToString() + "/" + dr["SvcMM"].ToString() + " ";
                                strText = strText + dr["ItemNm"].ToString() + " " + dr["TotSellingPrice"].ToString() + "\\n";
                            }

                            strText = strText + "\\n" + AlertNm["INFO_MUST_DELETE_ENTIRE"] + "\\n" + AlertNm["CONF_PRCEED_WORK"];

                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("javascript:fnReConfirm('" + strText + "');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingDelete", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                    //    }
                    //}
                //}
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 삭제실행 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDelMonthInfo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                DataTable dtPrintOut = new DataTable();
                DataTable dtReturn = new DataTable();

                string strRoomNo = txtHfDelRoomNo.Text;
                string strCardNo = txtHfDelCardNo.Text;
                string strDebitCreditCd = txtHfDelDebitCreditCd.Text;
                string strPaymentDt = txtHfDelPaymentDt.Text;
                string strPaymentSeq = txtHfDelPaymentSeq.Text;
                string strPaymentDetSeq = txtHfDelPaymentDetSeq.Text;
                string strRentCd = txtHfDelRentCd.Text;
                string strUserSeq = txtHfDelUserSeq.Text;
                string strUserDetSeq = txtHfDelUserDetSeq.Text;
                string strCarTyCd = txtHfDelCarTyCd.Text;
                string strParkingYear = ((TextBox)lvActMonthParkingFeeList.Items[0].FindControl("txtHfParkingYear")).Text;
                string strParkingMonth = ((TextBox)lvActMonthParkingFeeList.Items[0].FindControl("txtHfParkingMonth")).Text;
               
                string strAccessIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;

                //INSERT INTO [HoaDonParkingAPTReturn] FOR REFUND

                dtReturn = ReceiptMngBlo.InsertHoaDonParkingAPTReturn(strRentCd, strRoomNo, strCardNo, strPaymentDt, strPaymentSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strAccessIP);

                // 1. 출력 테이블에 차감 등록
                // KN_USP_SET_INSERT_PRINTINFO_S02
                dtPrintOut = ReceiptMngBlo.RegistryPrintReciptParkingCardMinusList(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strAccessIP);

                if (dtPrintOut != null)
                {
                    if (dtPrintOut.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();

                        // 2. 금액 로그 테이블 차감 처리
                        // KN_USP_SET_INSERT_MONEYINFO_M01
                        ReceiptMngBlo.RegistryMoneyMinusInfo(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq), strPrintSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strAccessIP);

                        // 3. 월정주차 정보 삭제 처리
                        // KN_USP_PRK_DELETE_MONTHPARKINGFEEINFO_M01
                        ParkingMngBlo.RemoveMonthParkingInfo(strRoomNo, strCardNo, strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq), strRentCd, strUserSeq);

                        // 4. 주차비 및 카드비 삭제 처리
                        // KN_USP_SET_DELETE_LEDGERINFO_M00
                        BalanceMngBlo.RemoveLedgerMng(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq));

                        if (dtPrintOut != null)
                        {
                            if (dtPrintOut.Rows.Count > CommValue.NUMBER_VALUE_0)
                            {
                                // 5.자동차 차량이면서 카드결제정보도 삭제되었을 경우 주차카드 관리 업체에 삭제
                                if (strCarTyCd.Equals(CommValue.CARTY_VALUE_VEHICLE) &&
                                    dtPrintOut.Rows[0]["RtnValue"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                                {
                                    // KN_USP_PRK_DELETE_USERPARKINGINFO_M01
                                    ParkingMngBlo.RemoveParkingMasterInfo(strUserSeq, Int32.Parse(strUserDetSeq));
                                }
                            }
                        }

                        txtHfDelDebitCreditCd.Text = string.Empty;
                        txtHfDelPaymentDt.Text = string.Empty;
                        txtHfDelPaymentSeq.Text = string.Empty;
                        txtHfDelUserSeq.Text = string.Empty;
                        txtHfDelRoomNo.Text = string.Empty;
                        txtHfDelCardNo.Text = string.Empty;
                        txtHfDelRentCd.Text = string.Empty;
                        txtHfDelUserDetSeq.Text = string.Empty;
                        txtHfDelCarTyCd.Text = string.Empty;

                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        
        /// <summary>
        /// 삭제실행 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
        {
            txtHfDelDebitCreditCd.Text = string.Empty;
            txtHfDelPaymentDt.Text = string.Empty;
            txtHfDelPaymentSeq.Text = string.Empty;
            txtHfDelUserSeq.Text = string.Empty;
            txtHfDelRoomNo.Text = string.Empty;
            txtHfDelCardNo.Text = string.Empty;
            txtHfDelRentCd.Text = string.Empty;
            txtHfDelUserDetSeq.Text = string.Empty;
            txtHfDelCarTyCd.Text = string.Empty;
        }

        /// <summary>
        /// 섹션 또는 룸 변경시 처리
        /// Autopostback의 폐단에 의한 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnRoomChange_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string strRoomNo = txtRegRoomNo.Text;
                string strSearchDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                // KN_USP_RES_SELECT_ROOMINFO_S02
                DataSet dsRoomInfo = RoomMngBlo.SpreadRoomInfo(strRoomNo, strSearchDt);

                if (dsRoomInfo != null)
                {
                    if (dsRoomInfo.Tables[0].Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        if (dsRoomInfo.Tables[0].Rows[0]["RtnValue"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                        {
                            // 해당 호실이 있는 경우
                            ltRentCd.Text = dsRoomInfo.Tables[0].Rows[0]["RentNm"].ToString();
                            txtHfRentCd.Text = dsRoomInfo.Tables[0].Rows[0]["RentCd"].ToString();
                            hfUserSeq.Value = dsRoomInfo.Tables[0].Rows[0]["UserSeq"].ToString();

                            MakeCarNoDdl(ddlCarNo, dsRoomInfo.Tables[1]);

                            CalculateNumber();

                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("document.getElementById('" + ddlCarNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoRoomAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // 해당 호실이 없는 경우
                            ltRentCd.Text = string.Empty;
                            txtHfRentCd.Text = string.Empty;

                            MakeCarNoDdl(ddlCarNo, dsRoomInfo.Tables[1]);

                            CalculateNumber();

                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");
                            sbList.Append("document.getElementById('" + txtRegRoomNo.ClientID + "').value = '';");
                            sbList.Append("document.getElementById('" + txtRegRoomNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoRoomAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
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
        /// 섹션 또는 룸 변경시 처리
        /// Autopostback의 폐단에 의한 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnCardNoChange_Click(object sender, ImageClickEventArgs e)
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

        protected void ddlCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strCarNo = ddlCarNo.SelectedValue;
                int intGateCnt = CommValue.NUMBER_VALUE_0;

                if (!string.IsNullOrEmpty(strCarNo))
                {
                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S04
                    DataTable dtReturn = ParkingMngBlo.WatchMonthParkingInfo(strCarNo, Session["LangCd"].ToString());

                    ltCarTy.Text = dtReturn.Rows[0]["CarTyNm"].ToString();
                    txtHfCarTy.Text = dtReturn.Rows[0]["CarTyCd"].ToString();
                    ltParkingCardNo.Text = dtReturn.Rows[0]["ParkingCardNo"].ToString();
                    txtHfParkingCardNo.Text = dtReturn.Rows[0]["ParkingCardNo"].ToString();

                    txtStartDt.Text = TextLib.MakeDateEightDigit(dtReturn.Rows[0]["StartDt"].ToString());
                    hfStartDt.Value = dtReturn.Rows[0]["StartDt"].ToString();

                    if (dtReturn.Rows[0]["GateCd"] != null)
                    {
                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["GateCd"].ToString()))
                        {
                            hfGateList.Value = dtReturn.Rows[0]["GateCd"].ToString();
                            intGateCnt = Int32.Parse(dtReturn.Rows[0]["GateCd"].ToString());
                        }
                    }

                    chkGateList.Items[0].Selected = CommValue.AUTH_VALUE_FALSE;
                    chkGateList.Items[1].Selected = CommValue.AUTH_VALUE_FALSE;
                    chkGateList.Items[2].Selected = CommValue.AUTH_VALUE_FALSE;

                    // 오토바이 게이트 등록요청 여부 체크
                    if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE))
                    {
                        // 오토바이 게이트에 해당되는 게이트 체크
                        chkGateList.Items[2].Selected = CommValue.AUTH_VALUE_TRUE;

                        // 차감
                        intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_MOTORBIKE);
                    }

                    // 오피스 리테일 게이트 등록요청 여부 체크
                    if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL))
                    {
                        // 오피스 리테일 게이트에 해당되는 게이트 체크
                        chkGateList.Items[1].Selected = CommValue.AUTH_VALUE_TRUE;

                        // 차감
                        intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_OFFICERETAIL);
                    }

                    // 아파트 게이트 등록요청 여부 체크
                    if (intGateCnt >= Int32.Parse(CommValue.GATE_VALUE_APARTMENT))
                    {
                        // 아파트 게이트에 해당되는 게이트 체크
                        chkGateList.Items[0].Selected = CommValue.AUTH_VALUE_TRUE;

                        // 차감
                        intGateCnt = intGateCnt - Int32.Parse(CommValue.GATE_VALUE_APARTMENT);
                    }

                    CalculateNumber();
                }
                else
                {
                    string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                    ltCarTy.Text = string.Empty;
                    txtHfCarTy.Text = string.Empty;
                    ltParkingCardNo.Text = string.Empty;
                    txtHfParkingCardNo.Text = string.Empty;

                    txtStartDt.Text = TextLib.MakeDateEightDigit(strNowDt);
                    hfStartDt.Value = strNowDt;

                    CalculateNumber();
                }
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

                string strUserSeq = hfUserSeq.Value;
                string strRentCd = txtHfRentCd.Text;
                string strCardFee = string.Empty;
                //string strFloorNo = hfFloorNo.Value;
                string strRoomNo = txtRegRoomNo.Text;
                string strCarNo = ddlCarNo.SelectedValue;
                string strCarTy = txtHfCarTy.Text;
                string strDuringMonth = ddlDuringMonth.SelectedValue;
                string strStartDt = hfStartDt.Value;
                string strEndDt = hfEndDt.Value;
                string strPaymentCd = ddlPaymentCd.Text;
                string strCardNo = txtHfParkingCardNo.Text;
                string strTagNo = hfParkingTagNo.Value;
                string strVatRatio = string.Empty;

                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;
                string paymentDT = txtPayDt.Text;

                string strCardCost = string.Empty;
                var bankcd = Int32.Parse(ddlTransfer.SelectedValue != "" ? ddlTransfer.SelectedValue : "0");
                var dbCardCost = double.Parse(txtCardFee.Text != "" ? txtCardFee.Text : "0");

                DataTable dtUser = ParkingMngBlo.SelectUserSeqByRoomNo(strRoomNo);
                //string strUserSeq = dtUser.Rows[0]["UserSeq"].ToString();
                string strFloorNo = dtUser.Rows[0]["FloorNo"].ToString();

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
                    string strPaymentDt = txtPayDt.Text.Replace("-", "").Replace(".", "");

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

                    DataTable dtAccPrkMonthInfo = new DataTable();
                    DataTable dtHoaDonParkingAPTReturn = new DataTable();

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

                    strStartDt = hfStartDt.Value.Replace("-", "");

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
                    
                }

                ResetSearchControls();

                txtSearchRoom.Text = txtRegRoomNo.Text;

                txtSearchCarNo.Text = ddlCarNo.SelectedValue;

                ResetInputControls();

                LoadData();     
            }
            
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DataTable dtReturn = new DataTable();

                // KN_USP_PRK_SELECT_ACCOUNTMONTHPARKINGINFO_S01
                dtReturn = ParkingMngBlo.SpreadAccountMonthParkingFeeForExcel(ddlRentCd.SelectedValue, ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue,
                                                                              txtSearchRoom.Text, txtSearchCarNo.Text, txtSearchCardNo.Text, Session["LangCd"].ToString());


                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
                Response.ContentType = "application/vnd.ms-excel";

                if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                {
                    Response.Charset = "euc-kr";
                }
                else
                {
                    Response.Charset = "utf-8";
                }

                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                this.EnableViewState = false;

                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

                string strTitle = "<p align='center'><font size='4'><b>" + Master.TITLE_NOW.ToString() + "</b></font></p>";
                htmlTextWriter.Write(strTitle);

                GridView gv = new GridView();

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

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllCheck = chkAll.Checked;

            try
            {
                CheckAll(isAllCheck);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void CheckAll(bool isAllCheck)
        {
            for (int intTmpI = 0; intTmpI < lvActMonthParkingFeeList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvActMonthParkingFeeList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)lvActMonthParkingFeeList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                }
            }
        }
       

        protected void lnkbtnPrint_Click1(object sender, EventArgs e)
        {
            try
            {
                var intCheckRow = CommValue.NUMBER_VALUE_0;
                DataTable dtParkingReciptDebit = new DataTable();
                var refPrintBundleNo = string.Empty;

                if (lvActMonthParkingFeeList.Items.Count <= 0)
                {
                    return;
                }

                //KN_USP_RPT_UPDATE_ParkingReciptDebit_U00
                ParkingMngBlo.UpdatingParkingReciptDebitNull("");

                for (var i = CommValue.NUMBER_VALUE_0; i < lvActMonthParkingFeeList.Items.Count; i++)
                {
                    if (!((CheckBox)lvActMonthParkingFeeList.Items[i].FindControl("chkboxList")).Checked) continue;


                    //var refSeq = ((TextBox)lvActMonthParkingFeeList.Items[i].FindControl("txtRef_Seq")).Text;
                    var cardNo = ((TextBox)lvActMonthParkingFeeList.Items[i].FindControl("txtHfCardNo")).Text.Trim();
                    var year = ((TextBox)lvActMonthParkingFeeList.Items[i].FindControl("txtHfYear")).Text;
                    var month = ((TextBox)lvActMonthParkingFeeList.Items[i].FindControl("txtHfMM")).Text;
                    var roomno = ((TextBox)lvActMonthParkingFeeList.Items[i].FindControl("txtHfRoomNo")).Text;
                    //KN_USP_PRK_SELECT_PARKINGRECIPTDEBIT_S00
                    dtParkingReciptDebit = ParkingMngBlo.SelectParkingReciptDebit(year, month, roomno,cardNo, Session["LangCd"].ToString());

                    var refseq = dtParkingReciptDebit.Rows[0]["REF_SEQ"].ToString();

                    if (string.IsNullOrEmpty(refPrintBundleNo))
                    {
                        refPrintBundleNo = refseq;
                    }

                    //KN_USP_RPT_UPDATE_PARKINGRECIPTDEBIT_U01
                    ParkingMngBlo.UpdatingParkingReciptDebitREF(refseq, refPrintBundleNo);
                    intCheckRow++;
                }
              
                // 선택 사항이 있는지 없는지 체크
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + refPrintBundleNo + "');", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    // 화면 초기화
                    LoadData();
                    // 선택된 대상 없음
                    var sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtPayDt_TextChanged(object sender, EventArgs e)
        {
            hfpayDate.Value = txtPayDt.Text;
        }
    }
}