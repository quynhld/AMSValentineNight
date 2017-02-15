using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;
using KN.Manage.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Management.Manage
{
    public partial class MngPaymentView : BasePage
    {
        public double dblSum = CommValue.NUMBER_VALUE_0_0;
        public double dblItemTotEmAmt = CommValue.NUMBER_VALUE_0_0;
        public double dblUniPrime = CommValue.NUMBER_VALUE_0_0;

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
                        LoadData();

                        InitControls();
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

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
            {
                if (Request.Params[Master.PARAM_DATA1] != null)
                {
                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                    {
                        txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    }
                }

                if (Request.Params[Master.PARAM_DATA2] != null)
                {
                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                    {
                        hfFeeTy.Value = Request.Params[Master.PARAM_DATA2].ToString();
                    }
                }

                if (Request.Params[Master.PARAM_DATA3] != null)
                {
                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3].ToString()))
                    {
                        txtHfRentalYear.Text = Request.Params[Master.PARAM_DATA3].ToString();
                        hfRentalYear.Value = Request.Params[Master.PARAM_DATA3].ToString();
                    }
                }

                if (Request.Params[Master.PARAM_DATA4] != null)
                {
                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA4].ToString()))
                    {
                        txtHfRentalMM.Text = Request.Params[Master.PARAM_DATA4].ToString();
                        hfRentalMM.Value = Request.Params[Master.PARAM_DATA4].ToString();
                    }
                }

                if (Request.Params[Master.PARAM_DATA5] != null)
                {
                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA5].ToString()))
                    {
                        txtHfUserSeq.Text = Request.Params[Master.PARAM_DATA5].ToString();
                        hfUserSeq.Value = Request.Params[Master.PARAM_DATA5].ToString();
                    }
                }

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            string strFeeTyTxt = string.Empty;

            ltPaymentCd.Text = TextNm["PAYMENTKIND"];
            ltFloorRoom.Text = TextNm["FLOOR"] + "/" + TextNm["ROOMNO"];
            ltName.Text = TextNm["NAME"];
            ltPayNoPaid.Text = TextNm["PAY"] + "/" + TextNm["NOTPAY"];
            ltLateYn.Text = TextNm["LATEFEEYN"];
            ltPayLimitDay.Text = TextNm["PAYMENTDEADLINE"];
            ltTotalPay.Text = TextNm["CHARGEDMOUNT"];
            ltTotalPaid.Text = TextNm["TOTALPAY"];
            ltPayDay.Text = TextNm["PAYDAY"];
            ltPay.Text = TextNm["PAYMENT"];
            ltPayDetail.Text = TextNm["ADMINISTRATIVEDETAILS"];
            ltItem.Text = TextNm["ITEMFEE"];
            ltMemo.Text = TextNm["REASON"];

            txtInsPayment.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            chkReceitCd.Attributes["onclick"] = "javascript:return fnCheckAccoutValidate('" + AlertNm["INFO_CANCEL"] + "','" + txtHfUserSeq.Text + "','" + hfRentCd.Value + "','" + hfFeeTy.Value + "','" + txtHfRentalYear.Text + "','" + txtHfRentalMM.Text + "');";

            lnkbtnBillPrint.Text = TextNm["PRINTBILL"];
            lnkbtnBillPrint.OnClientClick = "javascript:return fnPrintOutBill();";

            lnkbtnList.Text = TextNm["LIST"];
            imgbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_PAYMENT"] + "');";

            // DropDownList Setting
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlPaymentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);

            MakeAccountDdl(ddlTransfer);

            if (hfFeeTy.Value.Equals(CommValue.FEETY_VALUE_MNGFEE))
            {
                strFeeTyTxt = TextNm["MANAGEFEE"];
            }
            else if (hfFeeTy.Value.Equals(CommValue.FEETY_VALUE_RENTALFEE))
            {
                strFeeTyTxt = TextNm["RENTALFEE"];
            }

            ltPaymentTitle.Text = string.Format(TextNm["PAYMENTTITLE"], txtHfRentalYear.Text, (Int32.Parse(txtHfRentalMM.Text)).ToString(), strFeeTyTxt);

            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];

            LoadExchageDate();
        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            DataTable dtReturn = new DataTable();

            // 가장 최근의 환율을 조회함.
            // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S01
            dtReturn = ExchangeMngBlo.WatchExchangeRateLastInfo(txtHfRentCd.Text);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0")) + "&nbsp;" + TextNm["DONG"].ToString();
                        txtHfRealBaseRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                        txtHfRealBaseRate.Text = "0";
                    }
                }
                else
                {
                    ltRealBaseRate.Text = "-";
                    txtHfRealBaseRate.Text = "0";
                }
            }
        }

        public void MakeAccountDdl(DropDownList ddlParams)
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
            // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
            // Utility Fee : Chestnut 매출
            // 그외 KeangNam 매출
            string strCompCd = string.Empty;
            string strRentCd = string.Empty;
            string strFeeTy =  string.Empty;

            strRentCd = txtHfRentCd.Text;
            strFeeTy = hfFeeTy.Value;

            if ((strRentCd.Equals(CommValue.RENTAL_VALUE_APT) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTA) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTB)) &&
                strFeeTy.Equals(CommValue.FEETY_VALUE_MNGFEE))
            {
                strCompCd = CommValue.MAIN_COMP_CD;
            }
            else
            {
                strCompCd = CommValue.SUB_COMP_CD;
            }

            var dtReturn = AccountMngBlo.SpreadBankAccountInfo(strCompCd);

            ddlParams.Items.Clear();

            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

            foreach (var dr in dtReturn.Select())
            {
                ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
            }
        }

        private void ResetData()
        {
            txtInsPayLimitDay.Text = string.Empty;
            hfInsPayLimitDay.Value = string.Empty;
            txtHfOriginLimitDt.Text = string.Empty;
            txtHfTotalPay.Text = string.Empty;
            txtInsPayDay.Text = string.Empty;
            hfInsPayDay.Value = string.Empty;
            txtHfOriginDt.Text = string.Empty;
            txtInsPayment.Text = string.Empty;

            ddlPaymentCd.SelectedValue = CommValue.PAYMENT_TYPE_VALUE_CASH;
            ddlTransfer.SelectedValue = string.Empty;
            ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;
        }

        /// <summary>
        /// 데이터 로딩하는 메소드
        /// </summary>
        /// <param name="dtReturn"></param>
        private void LoadData()
        {
            DataSet dsReturn = new DataSet();

            ResetData();

            // KN_USP_MNG_SELECT_PAYMENTINFO_S01
            dsReturn = MngPaymentBlo.WatchPaymentInfo(txtHfRentCd.Text, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text);

            if (dsReturn.Tables[1] == null)
            {
                // 게시판 조회시 Null값 반환시 목록으로 리턴
                Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
            }
            else
            {
                DataRow drLateReturn = dsReturn.Tables[0].Rows[0];
                DataRow drBasicReturn = dsReturn.Tables[1].Rows[0];

                StringBuilder sbModDt = new StringBuilder();
                StringBuilder sbLimitDt = new StringBuilder();

                string strModDt = string.Empty;
                string strReceitCd = string.Empty;
                string strLateYn = string.Empty;
                string strLimitDt = string.Empty;

                int intCheckCnt = CommValue.NUMBER_VALUE_0;

                ltInsFloorRoom.Text = TextLib.StringDecoder(drBasicReturn["FloorNo"].ToString()) + "/" + TextLib.StringDecoder(drBasicReturn["RoomNo"].ToString());
                txtHfFloor.Text = drBasicReturn["FloorNo"].ToString();
                ltInsName.Text = TextLib.StringDecoder(drBasicReturn["TenantNm"].ToString());

                hfRentCd.Value = drBasicReturn["RentCd"].ToString();
                txtHfRoomNo.Text = drBasicReturn["RoomNo"].ToString();

                strLateYn = drBasicReturn["LateFeeYn"].ToString();

                if (!string.IsNullOrEmpty(drLateReturn["LateCnt"].ToString()))
                {
                    if (Int32.Parse(drLateReturn["LateCnt"].ToString()) > CommValue.NUMBER_VALUE_0)
                    {
                        intCheckCnt++;
                    }
                }

                if ((CommValue.FEETY_VALUE_MNGFEE).Equals(strLateYn))
                {
                    intCheckCnt++;
                    chkInsLateYn.Checked = CommValue.AUTH_VALUE_TRUE;
                    chkInsLateYn.Enabled = CommValue.AUTH_VALUE_FALSE;
                    chkReceitCd.Enabled = CommValue.AUTH_VALUE_FALSE;
                }
                else
                {
                    chkInsLateYn.Checked = CommValue.AUTH_VALUE_FALSE;
                }

                strReceitCd = drBasicReturn["ReceitCd"].ToString();

                if ((CommValue.FEETY_VALUE_MNGFEE).Equals(strReceitCd))
                {
                    intCheckCnt++;
                    chkReceitCd.Checked = CommValue.AUTH_VALUE_TRUE;                    
                }
                else
                {
                    chkReceitCd.Checked = CommValue.AUTH_VALUE_FALSE;
                }

                if (intCheckCnt > CommValue.NUMBER_VALUE_0)
                {
                    txtInsPayment.Enabled = CommValue.AUTH_VALUE_FALSE;
                    imgbtnRegist.Visible = CommValue.AUTH_VALUE_FALSE;
                    tdInsPayDay.Visible = CommValue.AUTH_VALUE_FALSE;
                    ddlPaymentCd.Enabled = CommValue.AUTH_VALUE_FALSE;
                }
                else
                {
                    txtInsPayment.Enabled = CommValue.AUTH_VALUE_TRUE;
                    imgbtnRegist.Visible = CommValue.AUTH_VALUE_TRUE;
                    tdInsPayDay.Visible = CommValue.AUTH_VALUE_TRUE;
                    ddlPaymentCd.Enabled = CommValue.AUTH_VALUE_TRUE;
                }

                if (!string.IsNullOrEmpty(drBasicReturn["MonthViAmtNo"].ToString()))
                {
                    ltInsTotalPay.Text = TextLib.MakeVietIntNo(double.Parse(drBasicReturn["MonthViAmtNo"].ToString()).ToString("###,##0"));
                    txtHfTotalPay.Text = drBasicReturn["MonthViAmtNo"].ToString();
                }
                else
                {
                    ltInsTotalPay.Text = CommValue.NUMBER_VALUE_0_0.ToString();
                    txtHfTotalPay.Text = CommValue.NUMBER_VALUE_0_0.ToString();
                }

                if (!string.IsNullOrEmpty(drBasicReturn["PaidAmt"].ToString()))
                {
                    ltInsPaidAmt.Text = TextLib.MakeVietIntNo(double.Parse(drBasicReturn["PaidAmt"].ToString()).ToString("###,##0"));
                }
                else
                {
                    ltInsPaidAmt.Text = CommValue.NUMBER_VALUE_0_0.ToString();
                }

                if (!string.IsNullOrEmpty(drBasicReturn["LimitDt"].ToString()))
                {
                    strLimitDt = drBasicReturn["LimitDt"].ToString();
                    sbLimitDt.Append(strLimitDt.Substring(0, 4));
                    sbLimitDt.Append("-");
                    sbLimitDt.Append(strLimitDt.Substring(4, 2));
                    sbLimitDt.Append("-");
                    sbLimitDt.Append(strLimitDt.Substring(6, 2));

                    txtInsPayLimitDay.Text = sbLimitDt.ToString();
                    hfInsPayLimitDay.Value = sbLimitDt.ToString();
                }

                if (!string.IsNullOrEmpty(drBasicReturn["VatRatio"].ToString()))
                {
                    txtHfVatRatio.Text = drBasicReturn["VatRatio"].ToString();
                }

                if (!string.IsNullOrEmpty(drBasicReturn["NotPair"].ToString()))
                {
                    if (drBasicReturn["NotPair"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        chkReceitCd.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        chkReceitCd.Enabled = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                DataTable dtListReturn = new DataTable();

                // KN_USP_MNG_SELECT_PAYMENTINFO_S02
                dtListReturn = MngPaymentBlo.WatchPaymentInfoList(hfRentCd.Value, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text);

                if (dtListReturn != null)
                {
                    lvMngPaymentView.DataSource = dtListReturn;
                    lvMngPaymentView.DataBind();
                }

                DataTable dtDetailViewReturn = new DataTable();

                // KN_USP_MNG_SELECT_PAYMENTINFO_S03
                dtDetailViewReturn = MngPaymentBlo.WatchExpenceInfoList(hfRentCd.Value, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, Session["LangCd"].ToString());

                if (dtDetailViewReturn != null)
                {
                    lvMngPaymentViewDetail.DataSource = dtDetailViewReturn;
                    lvMngPaymentViewDetail.DataBind();
                }

                DataTable dtReaseonReturn = new DataTable();

                // KN_USP_MNG_SELECT_PAYMENTINFO_S04
                dtReaseonReturn = MngPaymentBlo.WatchIncompleteList(hfRentCd.Value, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text);

                if (dtReaseonReturn != null)
                {
                    if (dtReaseonReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        ltMemoText.Text = dtReaseonReturn.Rows[0]["CompNm"].ToString() + " / " + dtReaseonReturn.Rows[0]["MemNm"].ToString() + " (" + dtReaseonReturn.Rows[0]["InsMemIP"].ToString() + ")" + " / " + dtReaseonReturn.Rows[0]["FinishReason"].ToString();
                    }
                }

                ddlPaymentCd.SelectedValue = CommValue.PAYMENT_TYPE_VALUE_CASH;
                ddlTransfer.SelectedValue = string.Empty;
                ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMngPaymentView_ItemCreated(object sender, ListViewItemEventArgs e)
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

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMngPaymentView_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltPay = (Literal)lvMngPaymentView.FindControl("ltPay");
            ltPay.Text = TextNm["PAYMENT"];
            Literal ltPayDay = (Literal)lvMngPaymentView.FindControl("ltPayDay");
            ltPayDay.Text = TextNm["PAYDAY"];
            Literal ltPay1 = (Literal)lvMngPaymentView.FindControl("ltPay1");
            ltPay1.Text = TextNm["PAYMENT"];
            Literal ltPayDay1 = (Literal)lvMngPaymentView.FindControl("ltPayDay1");
            ltPayDay1.Text = TextNm["PAYDAY"];
        }

        protected void lvMngPaymentView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    TextBox txtHfItemUserSeq = (TextBox)iTem.FindControl("txtHfItemUserSeq");
                    txtHfItemUserSeq.Text = drView["UserSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    TextBox txtHfItemRentCd = (TextBox)iTem.FindControl("txtHfItemRentCd");
                    txtHfItemRentCd.Text = drView["RentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["FeeTy"].ToString()))
                {
                    TextBox txtHfItemFeeTy = (TextBox)iTem.FindControl("txtHfItemFeeTy");
                    txtHfItemFeeTy.Text = drView["FeeTy"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentalYear"].ToString()))
                {
                    TextBox txtHfItemRentalYear = (TextBox)iTem.FindControl("txtHfItemRentalYear");
                    txtHfItemRentalYear.Text = drView["RentalYear"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentalMM"].ToString()))
                {
                    TextBox txtHfItemRentalMM = (TextBox)iTem.FindControl("txtHfItemRentalMM");
                    txtHfItemRentalMM.Text = drView["RentalMM"].ToString();
                }

                TextBox txtPayList = (TextBox)iTem.FindControl("txtPayList");
                TextBox txtHfItemPayAmt = (TextBox)iTem.FindControl("txtHfItemPayAmt");

                if (!string.IsNullOrEmpty(drView["PayAmt"].ToString()))
                {
                    txtPayList.Text = TextLib.MakeVietIntNo(double.Parse(drView["PayAmt"].ToString()).ToString("###,##0"));
                    txtPayList.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtHfItemPayAmt.Text = drView["PayAmt"].ToString();

                    dblSum = dblSum + double.Parse(drView["PayAmt"].ToString());
                }
                else
                {
                    txtPayList.Text = "-";
                    txtHfItemPayAmt.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["PaySeq"].ToString()))
                {
                    TextBox txtHfItemPaySeq = (TextBox)iTem.FindControl("txtHfItemPaySeq");
                    txtHfItemPaySeq.Text = drView["PaySeq"].ToString();
                }

                Literal ltPayDayList = (Literal)iTem.FindControl("ltPayDayList");
                TextBox txtHfItemPayDt = (TextBox)iTem.FindControl("txtHfItemPayDt");

                if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    ltPayDayList.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());
                    txtHfItemPayDt.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());
                }
                else
                {
                    ltPayDayList.Text = "-";
                    txtHfItemPayDt.Text = string.Empty;
                }

                txtHfTotalPayment.Text = dblSum.ToString();

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["INFO_CANT_DELETE_PART"] + "\\n" + AlertNm["INFO_MUST_DELETE_ENTIRE"] + "\\n" + AlertNm["CONF_PRCEED_WORK"] + "');";

                TextBox txtHfItemInsDate = (TextBox)iTem.FindControl("txtHfItemInsDate");

                if (!string.IsNullOrEmpty(drView["InsDate"].ToString()))
                {                    
                    txtHfItemInsDate.Text = drView["InsDate"].ToString();

                    string strToday = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("-", "").Substring(0, 8);
                    int strTodayTmp = Int32.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("-", "").Substring(9, 2));
                    string strInsDt = txtHfItemInsDate.Text.Replace("-", "").Substring(0, 8);

                    if (!strToday.Equals(strInsDt))
                    {
                        if ((double.Parse(strInsDt) + 1).ToString().Equals(strToday))
                        {
                            if (strTodayTmp <= 9)
                            {                                
                                txtPayList.ReadOnly = false;
                                txtPayList.Width = 130;
                                txtHfItemInsDate.Visible = true;
                                imgbtnDelete.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        txtPayList.ReadOnly = false;
                        txtPayList.Width = 130;
                        imgbtnDelete.Visible = true;
                    }
                }
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

                txtInsPayLimitDay.Text = hfInsPayLimitDay.Value;
                txtInsPayDay.Text = hfInsPayDay.Value;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnAccount_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string strInsMemIp = Request.ServerVariables["REMOTE_ADDR"];

                if (chkReceitCd.Checked)
                {
                    // 수납처리 (완납처리)
                    // KN_USP_MNG_UPDATE_PAYMENTINFO_S01
                    MngPaymentBlo.ModifyPaymentInfo(txtHfRentCd.Text, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_PAID);

                    // 환불금처리
                    // KN_USP_MNG_UPDATE_MNGREFUND_M00
                    MngPaymentBlo.ModifyMngRefundInfo(txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIp);
                }
                else
                {
                    // 수납처리 (미납처리)
                    // KN_USP_MNG_UPDATE_PAYMENTINFO_S01
                    MngPaymentBlo.ModifyPaymentInfo(txtHfRentCd.Text, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_NOTPAID);

                    // 환불금처리
                    // KN_USP_MNG_UPDATE_MNGREFUND_M01
                    MngPaymentBlo.ModifyMngRefundInfo(txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
            finally
            {
                LoadData();
            }
        }

        protected void imgbtnCancelAcc_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                object[] objReturn = new object[2];
                string strInsMemIp = Request.ServerVariables["REMOTE_ADDR"];

                // KN_USP_RES_DELETE_RENTALMNGREASONINFO_M00
                objReturn = MngPaymentBlo.RemoveRentalMngReasonInfo(txtHfRentCd.Text, hfFeeTy.Value, txtHfUserSeq.Text, txtHfRentalYear.Text, txtHfRentalMM.Text,
                                                                    Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIp);

                // 수납처리 (미납처리)
                // KN_USP_MNG_UPDATE_PAYMENTINFO_S01
                MngPaymentBlo.ModifyPaymentInfo(txtHfRentCd.Text, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_NOTPAID);

                // 환불금처리
                // KN_USP_MNG_UPDATE_MNGREFUND_M01
                MngPaymentBlo.ModifyMngRefundInfo(txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
            finally
            {
                LoadData();
            }
        }

        protected void imgbtnRegist_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strRoomNo = string.Empty;
                string strMemNo = Session["MemNo"].ToString();
                string strRentCd = hfRentCd.Value;
                string strItemTy = string.Empty;
                string strInsPayDay = hfInsPayDay.Value.Replace("-", "");
                string strLimitDt = hfInsPayLimitDay.Value.Replace("-", "");

                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;

                string strPaySeq = string.Empty;

                double dblHfTotalPayment = CommValue.NUMBER_VALUE_0_0;
                double dblInsPayment = CommValue.NUMBER_VALUE_0_0;
                double dblHfTotalPay = CommValue.NUMBER_VALUE_0_0;

                string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                StringBuilder sbAlert = new StringBuilder();

                if (string.IsNullOrEmpty(strLimitDt))
                {
                    strLimitDt = txtInsPayLimitDay.Text.Replace("-", "");
                }

                double dblTotalPayment = CommValue.NUMBER_VALUE_0_0;

                if (!string.IsNullOrEmpty(txtHfTotalPay.Text))
                {
                    if (!string.IsNullOrEmpty(txtHfTotalPay.Text))
                    {
                        // 총청구액
                        dblHfTotalPay = double.Parse(txtHfTotalPay.Text);
                    }
                }

                if (!string.IsNullOrEmpty(txtInsPayment.Text))
                {
                    if (!string.IsNullOrEmpty(txtHfTotalPayment.Text))
                    {
                        // 기수납액
                        dblHfTotalPayment = double.Parse(txtHfTotalPayment.Text);
                    }

                    if (!string.IsNullOrEmpty(txtInsPayment.Text))
                    {
                        // 현수납액
                        dblInsPayment = double.Parse(txtInsPayment.Text);
                    }

                    // 총 수납액
                    dblTotalPayment = dblHfTotalPayment + dblInsPayment;
                }

                string strLate = string.Empty;

                if (chkInsLateYn.Checked)
                {
                    strLate = CommValue.OVERDUE_TYPE_VALUE_TRUE;
                }
                else
                {
                    strLate = CommValue.OVERDUE_TYPE_VALUE_FALSE;
                }

                string strReceit = string.Empty;

                if (chkReceitCd.Checked)
                {
                    strReceit = CommValue.PAYMENT_TYPE_VALUE_PAID;
                }
                else
                {
                    strReceit = CommValue.PAYMENT_TYPE_VALUE_NOTPAID;
                }

                if (!string.IsNullOrEmpty(txtHfRoomNo.Text))
                {
                    strRoomNo = txtHfRoomNo.Text;
                }

                if (!string.IsNullOrEmpty(hfFeeTy.Value))
                {
                    strItemTy = hfFeeTy.Value;
                }

                // 1. 관리비 및 임대료 메인테이블 수납처리
                // KN_USP_MNG_UPDATE_PAYMENTINFO_S00
                MngPaymentBlo.ModifyPaymentInfo(strRentCd, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, strReceit, strLate, strLimitDt);

                string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT; //대변
                string strDirectCd = CommValue.DIRECT_TYPE_VALUE_DIRECT;            //대리인

                int intPaymentDetSeq = CommValue.NUMBER_VALUE_0;

                string strItemCd = hfFeeTy.Value;
                string strPaymentCd = ddlPaymentCd.Text;

                // 기수납액 + 현납부액 < 총 청구액 : 미납 처리
                // 기수납액 + 현납부액 > 총 청구액 : 잔돈 처리
                // 기수납액 + 현납부액 = 총 청구액 : 잔돈 없음
                if (dblTotalPayment < dblHfTotalPay)
                {
                    sbAlert.Append("Payed Money (VND) : " + TextLib.MakeVietIntNo((dblInsPayment).ToString("###,##0")) + "\\n");

                    // 2. 관리비 및 임대료 수납금액 저장
                    // KN_USP_MNG_INSERT_PAYMENTINFO_S01
                    DataTable dtRentalReturn = MngPaymentBlo.RegistryPaymentInfo(strRentCd, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, dblInsPayment.ToString(),
                                                                                 strInsPayDay, Session["CompCd"].ToString(), strMemNo, strMemIP, strPaymentCd);

                    strPaySeq = dtRentalReturn.Rows[0]["PaySeq"].ToString();

                    dblItemTotEmAmt = dblInsPayment / double.Parse(txtHfRealBaseRate.Text.Replace(",", ""));

                    // 3. 관리비 및 임대료 처리 원장등록
                    // KN_USP_SET_INSERT_LEDGERINFO_S00
                    DataTable dtAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strInsPayDay, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, hfFeeTy.Value,
                                                                         CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, hfUserSeq.Value, string.Empty,
                                                                         double.Parse(txtHfRealBaseRate.Text.Replace(",", "")), dblItemTotEmAmt, dblInsPayment, strPaymentCd, double.Parse(txtHfVatRatio.Text),
                                                                         Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);

                    sbAlert.Append(txtHfRentalYear.Text + "/" + txtHfRentalMM.Text + " Management Fee : " + TextLib.MakeVietIntNo(dblHfTotalPay.ToString("###,##0")) + "\\n");

                    if (dtAccnt != null)
                    {
                        if (dtAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                            int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                            // 계좌이체시 관련 정보 등록
                            if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                            {
                                // 4. 납입 계좌정보 등록
                                // KN_USP_SET_INSERT_LEDGERINFO_S01
                                BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, ddlTransfer.SelectedValue);
                            }

                            dblUniPrime = dblInsPayment * 100 / (100 + double.Parse(txtHfVatRatio.Text));

                            DataTable dtLedgerDet = new DataTable();

                            // 5. 상세원장등록
                            // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                            dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                              strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                              CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_SINGLEITEM, dblUniPrime, dblUniPrime, dblInsPayment, dblInsPayment,
                                                                              CommValue.NUMBER_VALUE_0, txtHfRentalYear.Text, txtHfRentalMM.Text, strRoomNo, double.Parse(txtHfVatRatio.Text), CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                              Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);

                            if (dtLedgerDet != null)
                            {
                                if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                }
                            }

                            // 6. 관리비 및 임대료 Sequence 할당
                            // KN_USP_MNG_UPDATE_RENTALMNGFEEADDON_S00
                            MngPaymentBlo.ModifyRentalMngFeeAddon(strRentCd, strItemCd, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, Int32.Parse(strPaySeq),
                                                                  strInsPayDay, intPaymentSeq);

                            // 7. 출력 테이블에 등록
                            // KN_USP_SET_INSERT_PRINTINFO_S00
                            DataTable dtPrintOut = new DataTable();
                            dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, strItemCd, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                               Int32.Parse(txtHfFloor.Text), strRoomNo, txtHfRentalYear.Text, txtHfRentalMM.Text, strPaymentCd, hfUserSeq.Value,
                                                                               Session["MemNo"].ToString(), txtHfRentalYear.Text + " / " + txtHfRentalMM.Text + " Management Fee ( " + strRoomNo + " )",
                                                                               dblInsPayment, double.Parse(txtHfRealBaseRate.Text.Replace(",", "")),
                                                                               Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);

                            if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                            {
                                strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                // 8. 출력 정보 원장상세 테이블에 등록
                                // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strInsPayDay, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));

                                // 9. 출력자 테이블에 등록
                                // KN_USP_SET_INSET_PRINTINFO_S01
                                ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);
                            }

                            // 10.금액 로그 테이블 처리
                            // KN_USP_SET_INSERT_MONEYINFO_M00
                            ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, intPaymentDetSeq);
                        }
                    }

                    sbAlert.Append("Total Payed Amount : " + TextLib.MakeVietIntNo(dblTotalPayment.ToString("###,##0")) + "\\n");
                    sbAlert.Append("Not Payed Amount : " + TextLib.MakeVietIntNo((dblHfTotalPay - dblTotalPayment).ToString("###,##0")) + " VND " + "\\n");

                    // dblInsPayment 수납처리됨.
                }
                else if (dblTotalPayment == dblHfTotalPay)
                {
                    sbAlert.Append("Payed Money (VND) : " + TextLib.MakeVietIntNo((dblInsPayment).ToString("###,##0")) + "\\n");

                    // 2. 관리비 및 임대료 수납금액 저장
                    // KN_USP_MNG_INSERT_PAYMENTINFO_S01
                    DataTable dtRentalReturn = MngPaymentBlo.RegistryPaymentInfo(strRentCd, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, dblInsPayment.ToString(),
                                                                                 strInsPayDay, Session["CompCd"].ToString(), strMemNo, strMemIP, strPaymentCd);

                    strPaySeq = dtRentalReturn.Rows[0]["PaySeq"].ToString();

                    // 3. 수납처리 (완납처리)
                    // KN_USP_MNG_UPDATE_PAYMENTINFO_S01
                    MngPaymentBlo.ModifyPaymentInfo(strRentCd, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_PAID);

                    dblItemTotEmAmt = dblInsPayment / double.Parse(txtHfRealBaseRate.Text.Replace(",", ""));

                    // 4. 관리비 및 임대료 처리 원장등록
                    // KN_USP_SET_INSERT_LEDGERINFO_S00
                    DataTable dtAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strInsPayDay, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, strItemTy,
                                                                         CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, hfUserSeq.Value, string.Empty,
                                                                         double.Parse(txtHfRealBaseRate.Text.Replace(",", "")), dblItemTotEmAmt, dblInsPayment, strPaymentCd, double.Parse(txtHfVatRatio.Text),
                                                                         Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);

                    sbAlert.Append(txtHfRentalYear.Text + "/" + txtHfRentalMM.Text + " Management Fee : " + TextLib.MakeVietIntNo(dblHfTotalPay.ToString("###,##0")) + "\\n");

                    if (dtAccnt != null)
                    {
                        if (dtAccnt.Rows.Count > 0)
                        {
                            int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                            int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                            // 계좌이체시 관련 정보 등록
                            if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                            {
                                // 5. 납입 계좌정보 등록
                                // KN_USP_SET_INSERT_LEDGERINFO_S01
                                BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, ddlTransfer.SelectedValue);
                            }

                            dblUniPrime = dblInsPayment * 100 / (100 + double.Parse(txtHfVatRatio.Text));

                            DataTable dtLedgerDet = new DataTable();

                            // 6. 원장상세정보 등록
                            // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                            dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                              strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                              CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_SINGLEITEM, dblUniPrime, dblUniPrime, dblInsPayment, dblInsPayment,
                                                                              CommValue.NUMBER_VALUE_0, txtHfRentalYear.Text, txtHfRentalMM.Text, strRoomNo, double.Parse(txtHfVatRatio.Text), CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                              Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);

                            if (dtLedgerDet != null)
                            {
                                if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                }
                            }

                            // 7. 관리비 및 임대료 Sequence 할당
                            // KN_USP_MNG_UPDATE_RENTALMNGFEEADDON_S00
                            MngPaymentBlo.ModifyRentalMngFeeAddon(strRentCd, strItemCd, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, Int32.Parse(strPaySeq),
                                                                  strInsPayDay, intPaymentSeq);

                            // 8. 출력 테이블에 등록
                            // KN_USP_SET_INSERT_PRINTINFO_S00
                            DataTable dtPrintOut = new DataTable();
                            dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, strItemCd, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                               Int32.Parse(txtHfFloor.Text), strRoomNo, txtHfRentalYear.Text, txtHfRentalMM.Text, strPaymentCd, hfUserSeq.Value,
                                                                               Session["MemNo"].ToString(), txtHfRentalYear.Text + " / " + txtHfRentalMM.Text + " Management Fee ( " + strRoomNo + " )",
                                                                               dblInsPayment, double.Parse(txtHfRealBaseRate.Text.Replace(",", "")),
                                                                               Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);

                            if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                            {
                                strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                // 9. 출력 정보 원장상세 테이블에 등록
                                // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strInsPayDay, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));

                                // 10. 출력자 테이블에 등록
                                // KN_USP_SET_INSERT_PRINTINFO_S01
                                ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);
                            }

                            // 11.금액 로그 테이블 처리
                            // KN_USP_SET_INSERT_MONEYINFO_M00
                            ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, intPaymentDetSeq);

                            // 12. 환불금처리
                            // KN_USP_MNG_UPDATE_MNGREFUND_M00
                            MngPaymentBlo.ModifyMngRefundInfo(txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);
                        }
                    }

                    sbAlert.Append("Total Payed Amount : " + TextLib.MakeVietIntNo(dblTotalPayment.ToString("###,##0")) + "\\n");
                    sbAlert.Append("Change : 0 VND " + "\\n");

                    // dblInsPayment 수납처리됨.
                }
                else
                {
                    // 잔돈
                    double dblTempPayment = dblTotalPayment - dblHfTotalPay;
                    // 실제 납부 금액
                    double dblRealPayment = dblHfTotalPay - dblHfTotalPayment;
                    string strTempPayment = dblTempPayment.ToString();

                    sbAlert.Append("Payed Money (VND) : " + TextLib.MakeVietIntNo((dblInsPayment).ToString("###,##0")) + "\\n");

                    // 2. 관리비 및 임대료 수납금액 저장
                    // KN_USP_MNG_INSERT_PAYMENTINFO_S01
                    DataTable dtRentalReturn = MngPaymentBlo.RegistryPaymentInfo(strRentCd, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, dblRealPayment.ToString(),
                                                                                 strInsPayDay, Session["CompCd"].ToString(), strMemNo, strMemIP, strPaymentCd);

                    strPaySeq = dtRentalReturn.Rows[0]["PaySeq"].ToString();

                    // 3. 수납처리 (완납처리)
                    // KN_USP_MNG_UPDATE_PAYMENTINFO_S01
                    MngPaymentBlo.ModifyPaymentInfo(strRentCd, hfFeeTy.Value, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_PAID);

                    dblItemTotEmAmt = dblRealPayment / double.Parse(txtHfRealBaseRate.Text.Replace(",", ""));

                    // 4. 관리비 및 임대료 처리 원장등록
                    // KN_USP_SET_INSERT_LEDGERINFO_S00
                    DataTable dtAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strInsPayDay, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, strItemTy,
                                                                         CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, hfUserSeq.Value, string.Empty,
                                                                         double.Parse(txtHfRealBaseRate.Text.Replace(",", "")), dblItemTotEmAmt, dblRealPayment, strPaymentCd, double.Parse(txtHfVatRatio.Text),
                                                                         Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);

                    sbAlert.Append(txtHfRentalYear.Text + "/" + txtHfRentalMM.Text + " Management Fee : " + TextLib.MakeVietIntNo(dblRealPayment.ToString("###,##0")) + "\\n");

                    if (dtAccnt != null)
                    {
                        if (dtAccnt.Rows.Count > 0)
                        {
                            int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                            int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                            // 계좌이체시 관련 정보 등록
                            if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                            {
                                // 5. 납입 계좌정보 등록
                                // KN_USP_SET_INSERT_LEDGERINFO_S01
                                BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, ddlTransfer.SelectedValue);
                            }

                            dblUniPrime = dblRealPayment * (100) / (100 + double.Parse(txtHfVatRatio.Text));

                            DataTable dtLedgerDet = new DataTable();

                            // 6. 원장상세정보 등록
                            // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                            dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                              strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                              CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblRealPayment, dblRealPayment,
                                                                              CommValue.NUMBER_VALUE_0, txtHfRentalYear.Text, txtHfRentalMM.Text, strRoomNo, double.Parse(txtHfVatRatio.Text), CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                              Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);

                            if (dtLedgerDet != null)
                            {
                                if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                }
                            }

                            // 7. 관리비 및 임대료 Sequence 할당
                            // KN_USP_MNG_UPDATE_RENTALMNGFEEADDON_S00
                            MngPaymentBlo.ModifyRentalMngFeeAddon(strRentCd, strItemCd, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, Int32.Parse(strPaySeq),
                                                                  strInsPayDay, intPaymentSeq);

                            // 8. 출력 테이블에 등록
                            // KN_USP_SET_INSERT_PRINTINFO_S00
                            DataTable dtPrintOut = new DataTable();
                            dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, strItemCd, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                               Int32.Parse(txtHfFloor.Text), strRoomNo, txtHfRentalYear.Text, txtHfRentalMM.Text, strPaymentCd, hfUserSeq.Value,
                                                                               Session["MemNo"].ToString(), txtHfRentalYear.Text + " / " + txtHfRentalMM.Text + " Management Fee ( " + strRoomNo + " )",
                                                                               dblRealPayment, double.Parse(txtHfRealBaseRate.Text.Replace(",", "")),
                                                                               Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP, strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);

                            if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                            {
                                strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                // 9. 출력 정보 원장상세 테이블에 등록
                                // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strInsPayDay, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));
                                
                                // 10. 출력자 테이블에 등록
                                // KN_USP_SET_INSERT_PRINTINFO_S01
                                ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);
                            }

                            // 11.금액 로그 테이블 처리
                            // KN_USP_SET_INSERT_MONEYINFO_M00
                            ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strInsPayDay, intPaymentSeq, intPaymentDetSeq);

                            // 12. 환불금처리
                            // KN_USP_MNG_UPDATE_MNGREFUND_M00
                            MngPaymentBlo.ModifyMngRefundInfo(txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);
                        }
                    }

                    sbAlert.Append("Total Payed Amount : " + TextLib.MakeVietIntNo(dblTotalPayment.ToString("###,##0")) + "\\n");
                    sbAlert.Append("Change : " + TextLib.MakeVietIntNo(dblTempPayment.ToString("###,##0")) + "\\n");

                    // dblInsPayment 수납처리됨.
                }

                StringBuilder sbResult = new StringBuilder();

                sbResult.Append("alert('" + sbAlert.ToString() + "');");

                sbResult.Append("window.open('/Common/RdPopup/RDPopupReciptDetail.aspx?Datum0=" + strPrintSeq + "&Datum1=0&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ManagememntFee", sbResult.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
            finally
            {
                LoadData();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMngPaymentViewDetail_ItemCreated(object sender, ListViewItemEventArgs e)
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

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMngPaymentViewDetail_LayoutCreated(object sender, EventArgs e)
        {

        }

        protected void lvMngPaymentViewDetail_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (CommValue.FEETY_VALUE_MNGFEE.Equals(hfFeeTy.Value))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
                {
                    if (!string.IsNullOrEmpty(drView["ExpressNm"].ToString()))
                    {
                        Literal ltMenuList = (Literal)iTem.FindControl("ltMenuList");
                        ltMenuList.Text = TextLib.StringDecoder(drView["ExpressNm"].ToString());
                    }

                    if (!string.IsNullOrEmpty(drView["MngFee"].ToString()))
                    {
                        Literal ltPaymentList = (Literal)iTem.FindControl("ltPaymentList");
                        ltPaymentList.Text = TextLib.MakeVietIntNo(double.Parse(drView["MngFee"].ToString()).ToString("###,##0"));
                    }
                }
            }
        }

        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + hfFeeTy.Value, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intNowLine = CommValue.NUMBER_VALUE_0;

                if (!string.IsNullOrEmpty(txtHfNowLine.Text))
                {
                    intNowLine = Int32.Parse(txtHfNowLine.Text);
                }

                TextBox txtPayList = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtPayList");
                Literal ltPayDayList = (Literal)lvMngPaymentView.Items[intNowLine].FindControl("ltPayDayList");

                TextBox txtHfItemUserSeq = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtHfItemUserSeq");
                TextBox txtHfItemRentCd = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtHfItemRentCd");
                TextBox txtHfItemFeeTy = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtHfItemFeeTy");
                TextBox txtHfItemRentalYear = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtHfItemRentalYear");
                TextBox txtHfItemRentalMM = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtHfItemRentalMM");
                TextBox txtHfItemPaySeq = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtHfItemPaySeq");
                TextBox txtHfItemInsDate = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtHfItemInsDate");
                TextBox txtHfItemPayDt = (TextBox)lvMngPaymentView.Items[intNowLine].FindControl("txtHfItemPayDt");

                double dblPayList = double.Parse(txtPayList.Text.Replace(".", ""));

                string strMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strMemNo = Session["MemNo"].ToString();
                string strPayDt = ltPayDayList.Text.Replace("-", "");

                // 1. 임대료 및 관리비 납부 정보 삭제
                // KN_USP_RES_DELETE_RENTALMNGFEEADDON_S00
                DataTable dsDeleteList = MngPaymentBlo.RemoveRentalMngFeeAddon(txtHfItemRentCd.Text, txtHfItemUserSeq.Text, txtHfItemFeeTy.Text, txtHfItemRentalYear.Text, txtHfItemRentalMM.Text, Int32.Parse(txtHfItemPaySeq.Text));

                if (dsDeleteList != null)
                {
                    string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                    string strPaymentDt = dsDeleteList.Rows[0]["PaymentDt"].ToString();
                    string strPaymentSeq = dsDeleteList.Rows[0]["PaymentSeq"].ToString();
                    string strPrintSeq = string.Empty;

                    // 2. 출력 테이블에 차감 등록
                    // KN_USP_SET_INSERT_PRINTINFO_S03
                    DataTable dtPrintOut = ReceiptMngBlo.RegistryPrintReciptRentalMngMinusList(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq), txtHfItemFeeTy.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);

                    if (dtPrintOut != null)
                    {
                        if (dtPrintOut.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();

                            // 3. 금액 로그 테이블 차감 처리
                            // KN_USP_SET_INSERT_MONEYINFO_M01
                            ReceiptMngBlo.RegistryMoneyMinusInfo(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq), strPrintSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIP);

                            // 4. 결제정보 삭제 처리 (원장, 원장상세, 카드)
                            // KN_USP_SET_DELETE_LEDGERINFO_M00
                            BalanceMngBlo.RemoveLedgerMng(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq));
                        }
                    }

                    // 5. 완납취소 처리
                    // KN_USP_MNG_UPDATE_PAYMENTINFO_S01
                    MngPaymentBlo.ModifyPaymentInfo(txtHfItemRentCd.Text, txtHfItemFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_NOTPAID);

                    StringBuilder sbList = new StringBuilder();
                    sbList.Append("window.open('/Common/RdPopup/RDPopupReciptDetail.aspx?Datum0=" + strPrintSeq + "&Datum1=0&Datum2=&Datum3=&Datum4=', 'RentalNManageFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RentalNManageFee", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
            finally
            {
                LoadData();
            }
        }

        protected void imgbtnComplete_Click(object sender, ImageClickEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 팝업의 불법적인 접근을 제한하기 위한 세션 생성
            Session["CompleteYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
        }

        protected void lvMngPaymentView_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                txtHfNowLine.Text = e.ItemIndex.ToString();

                // 세션체크
                AuthCheckLib.CheckSession();

                // 삭제 버튼으로 이동
                StringBuilder sbList = new StringBuilder();
                sbList.Append("javascript:fnDataDelete();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DataDelete", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}