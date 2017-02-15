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

namespace KN.Web.Management.LateFee
{
    public partial class LateFeeInfoView : BasePage
    {
        double fltSum = 0.0d;

        public double dblItemTotEmAmt = 0d;
        public string strUserTaxCd = string.Empty;
        public string strBillCd = string.Empty;
        public string strBillNo = string.Empty;
        public double dblUniPrime = 0d;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            string strRentCd = Request.Params[Master.PARAM_DATA1].ToString();
            string strFeeTy = Request.Params[Master.PARAM_DATA2].ToString();
            string strRentalYear = Request.Params[Master.PARAM_DATA3].ToString();
            string strRentalMM = Request.Params[Master.PARAM_DATA4].ToString();
            string strUserSeq = Request.Params[Master.PARAM_DATA5].ToString();

            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        DataSet dsReturn = new DataSet();

                        // KN_USP_MNG_SELECT_LATEFEEINFO_S01
                        dsReturn = MngPaymentBlo.WatchLateFeeInfo(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

                        if (dsReturn.Tables[1] == null)
                        {
                            // 게시판 조회시 Null값 반환시 목록으로 리턴
                            Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                        }
                        else
                        {
                            LoadData(dsReturn);
                        }
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
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                txtHfFeeTy.Text = Request.Params[Master.PARAM_DATA2].ToString();
                txtHfRentalYear.Text = Request.Params[Master.PARAM_DATA3].ToString();
                txtHfRentalMM.Text = Request.Params[Master.PARAM_DATA4].ToString();
                txtHfUserSeq.Text = Request.Params[Master.PARAM_DATA5].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            string strFeeTyTxt = string.Empty;

            //ltContractorNo.Text = TextNm["CONTRACTORNO"];
            ltPaymentCd.Text = TextNm["PAYMENTKIND"];
            ltFloorRoom.Text = TextNm["FLOOR"] + "/" + TextNm["ROOMNO"];
            ltName.Text = TextNm["NAME"];
            ltReceitCd.Text = TextNm["PAY"];
            ltLateFee.Text = TextNm["TOTALPAY"];
            ltLateFeeRatio.Text = TextNm["LATEFEERATIO"];
            ltLateDt.Text = TextNm["LATEFEEDAY"];
            ltPayDay.Text = TextNm["PAYDAY"];
            ltPay.Text = TextNm["PAYMENT"];
            ltTotalPay.Text = TextNm["PAYMENT"];
            ltLatePayment.Text = TextNm["ARREARS"];

            lnkbtnList.Text = TextNm["LIST"];

            if (txtHfFeeTy.Text.Equals(CommValue.FEETY_VALUE_MNGFEE))
            {
                strFeeTyTxt = TextNm["MANAGEFEE"];
            }
            else if (txtHfFeeTy.Text.Equals(CommValue.FEETY_VALUE_RENTALFEE))
            {
                strFeeTyTxt = TextNm["RENTALFEE"];
            }

            if (txtHfFeeTy.Text.Equals(CommValue.FEETY_VALUE_MNGFEE))
            {
                strFeeTyTxt = TextNm["MANAGEFEE"];
            }
            else if (txtHfFeeTy.Text.Equals(CommValue.FEETY_VALUE_RENTALFEE))
            {
                strFeeTyTxt = TextNm["RENTALFEE"];
            }

            // DropDownList Setting
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlPaymentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);

            txtInsPay.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            imgbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_PAYMENT"] + "');";

            ltPaymentTitle.Text = string.Format(TextNm["PAYMENTTITLE"], txtHfRentalYear.Text, txtHfRentalMM.Text, strFeeTyTxt);

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
                        txtRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(dtReturn.Rows[0]["DongToDollar"].ToString()).ToString("###,##0"));
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                        txtRealBaseRate.Text = "0";

                    }
                }
                else
                {
                    ltRealBaseRate.Text = "-";
                    txtRealBaseRate.Text = "0";
                }
            }
        }

        /// <summary>
        /// 데이터 로딩하는 메소드
        /// </summary>
        /// <param name="dtReturn"></param>
        private void LoadData(DataSet dsReturn)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataRow dr = dsReturn.Tables[1].Rows[0];

            DataRow dr1 = dsReturn.Tables[0].Rows[0];

            StringBuilder sbModDt = new StringBuilder();
            StringBuilder sbLimitDt = new StringBuilder();

            string strModDt = string.Empty;
            string strReceitCd = string.Empty;
            string strLateYn = string.Empty;
            string strLimitDt = string.Empty;

            //ltInsContractNo.Text = dr["UserSeq"].ToString();
            ltInsFloorRoom.Text = TextLib.StringDecoder(dr["FloorNo"].ToString()) + "/" + TextLib.StringDecoder(dr["RoomNo"].ToString());
            ltInsName.Text = TextLib.StringDecoder(dr["TenantNm"].ToString());
            txtHfRentCd.Text = dr["RentCd"].ToString();
            txtHfRoomNo.Text = dr["RoomNo"].ToString();

            strReceitCd = dr["ReceitCd"].ToString();

            if (!string.IsNullOrEmpty(dr1["LateCnt"].ToString()))
            {
                if (Int32.Parse(dr1["LateCnt"].ToString()) > 0)
                {
                    txtInsPay.Enabled = CommValue.AUTH_VALUE_FALSE;
                    imgbtnRegist.Visible = CommValue.AUTH_VALUE_FALSE;
                }
            }

            if ((CommValue.FEETY_VALUE_MNGFEE).Equals(strReceitCd))
            {
                chkReceitCd.Checked = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                chkReceitCd.Checked = CommValue.AUTH_VALUE_FALSE;
            }

            ltInsLateFee.Text = TextLib.MakeVietIntNo(double.Parse(dr["LateFee"].ToString()).ToString("###,##0"));
            ltInsLateFeeRatio.Text = dr["LateFeeRatio"].ToString();
            ltInsLateDt.Text = dr["LateDt"].ToString();

            if (!string.IsNullOrEmpty(dr["TotalPayAmt"].ToString()))
            {
                ltInsTotalPay.Text = TextLib.MakeVietIntNo(double.Parse(dr["TotalPayAmt"].ToString()).ToString("###,##0"));
            }
            else
            {
                ltInsTotalPay.Text = "-";
            }

            if (!string.IsNullOrEmpty(dr["LatePayment"].ToString()))
            {
                ltInsLatePayment.Text = TextLib.MakeVietIntNo(double.Parse(dr["LatePayment"].ToString()).ToString("###,##0"));

                if (ltInsLatePayment.Text.Equals(""))
                {
                    ltInsLatePayment.Text = "0";
                }
            }

            if (!string.IsNullOrEmpty(dr["VatRatio"].ToString()))
            {
                txtHfVatRatio.Text = TextLib.MakeVietIntNo(double.Parse(dr["VatRatio"].ToString()).ToString("###,##0"));

            }

            DataTable dtListReturn = new DataTable();

            // KN_USP_MNG_SELECT_LATEFEEINFO_S02
            dtListReturn = MngPaymentBlo.WatchLateFeeInfoList(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text);

            if (dtListReturn != null)
            {
                lvMngLateFeeInfoView.DataSource = dtListReturn;
                lvMngLateFeeInfoView.DataBind();
            }
        }

        protected void imgbtnRegist_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                //AuthCheckLib.CheckSession();

                //string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                //string strRoomNo = string.Empty;

                //string strInsPayDay = hfInsPayDay.Value.Replace("-", "");

                //string strReceit = string.Empty;

                //double fltTotalPayment = 0.0d;

                //double fltHfTotalPayment = 0.0d;
                //double fltInsPayment = 0.0d;

                //if (!string.IsNullOrEmpty(txtInsPay.Text))
                //{
                //    if (!string.IsNullOrEmpty(hfTotalPayment.Value))
                //    {
                //        fltHfTotalPayment = double.Parse(hfTotalPayment.Value);
                //    }

                //    if (!string.IsNullOrEmpty(txtInsPay.Text))
                //    {
                //        fltInsPayment = double.Parse(txtInsPay.Text);
                //    }

                //    fltTotalPayment = fltHfTotalPayment + fltInsPayment;
                //}

                //if (chkReceitCd.Checked)
                //{
                //    strReceit = CommValue.PAYMENT_TYPE_VALUE_PAID;
                //}
                //else
                //{
                //    strReceit = CommValue.PAYMENT_TYPE_VALUE_NOTPAID;
                //}

                //string strItemCd = string.Empty;
                //if (txtHfFeeTy.Text.Equals("0001"))
                //{
                //    strItemCd = "0005";
                //}
                //else
                //{
                //    strItemCd = "0006";
                //}

                //if (!string.IsNullOrEmpty(txtHfRoomNo.Text))
                //{
                //    strRoomNo = txtHfRoomNo.Text;
                //}

                //MngPaymentBlo.ModifyLateFeeInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, strReceit);
                //MngPaymentBlo.ModifyPaymentInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, strReceit);

                //if (!hfInsPayDay.Value.Equals("") && !txtInsPay.Text.Equals(""))
                //{
                //    if (double.Parse(txtInsPay.Text) >= double.Parse(ltInsLatePayment.Text.Replace(",", "").Replace(".", "").ToString()))
                //    {
                //        if (double.Parse(ltInsLateFee.Text.Replace(",", "").Replace(".", "")) > fltTotalPayment)
                //        {
                //            MngLateFeeBlo.RegistryLateFeeInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, txtInsPay.Text, strInsPayDay, ltInsLateDt.Text, ltInsLateFeeRatio.Text, Session["MemNo"].ToString(), strInsMemIP, ddlPaymentCd.Text);
                //            LogInfoBlo.RegistryMoneyLogInfo(txtHfRentCd.Text, strItemCd, double.Parse(txtInsPay.Text), Session["MemNo"].ToString(), strInsMemIP);

                //            MngAccountsBlo.RegistrySettelmentInfo(txtHfUserSeq.Text, txtHfRentCd.Text, strItemCd, txtHfRentalYear.Text, txtHfRentalMM.Text, double.Parse(txtRealBaseRate.Text.Replace(".", "")),
                //            double.Parse(txtInsPay.Text), ddlPaymentCd.Text, strInsPayDay, txtHfVatRatio.Text, Session["MemNo"].ToString(), strInsMemIP);

                //            dblItemTotEmAmt = double.Parse(txtInsPay.Text) / double.Parse(txtRealBaseRate.Text.Replace(".", "").Replace(",", ""));

                //            DataTable dtAccnt = MngAccountsBlo.RegistryAccountsInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strInsPayDay, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text, CommValue.DIRECT_TYPE_VALUE_AGENT
                //    , CommValue.ITEM_TYPE_VALUE_LATEMNGFEE, CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, txtHfUserSeq.Text, strUserTaxCd, strBillCd, strBillNo
                //    , double.Parse(txtRealBaseRate.Text.Replace(".", "")), dblItemTotEmAmt, double.Parse(txtInsPay.Text), ddlPaymentCd.Text, double.Parse(txtHfVatRatio.Text), string.Empty, string.Empty, Session["MemNo"].ToString(), strInsMemIP);

                //            if (dtAccnt != null)
                //            {
                //                if (dtAccnt.Rows.Count > 0)
                //                {
                //                    int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                //                    int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                //                    strUserTaxCd = dtAccnt.Rows[0]["UserTaxCd"].ToString();
                //                    strBillCd = dtAccnt.Rows[0]["BillCd"].ToString();
                //                    strBillNo = dtAccnt.Rows[0]["BillNo"].ToString();

                //                    dblUniPrime = double.Parse(txtInsPay.Text) * (100 - double.Parse(txtHfVatRatio.Text)) / 100;

                //                    MngAccountsBlo.RegistryAccountsDetInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strInsPayDay, intPaymentSeq, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text, CommValue.DIRECT_TYPE_VALUE_AGENT
                //                        , CommValue.ITEM_TYPE_VALUE_LATEMNGFEE, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty, CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH
                //                        , dblUniPrime, dblUniPrime, double.Parse(txtInsPay.Text), double.Parse(txtInsPay.Text), CommValue.NUMBER_VALUE_0, txtHfRentalYear.Text, txtHfRentalMM.Text, strRoomNo
                //                        , double.Parse(txtHfVatRatio.Text), CommValue.CONCLUSION_TYPE_TEXT_YES, Session["MemNo"].ToString(), strInsMemIP);
                //                }

                //            }
                //        }
                //        else if (double.Parse(ltInsLateFee.Text.Replace(",", "").Replace(".", "")) == fltTotalPayment)
                //        {
                //            MngLateFeeBlo.RegistryLateFeeInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, txtInsPay.Text, strInsPayDay, ltInsLateDt.Text, ltInsLateFeeRatio.Text, Session["MemNo"].ToString(), strInsMemIP, ddlPaymentCd.Text);
                //            MngLateFeeBlo.ModifyLateFeeInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_PAID);
                //            MngPaymentBlo.ModifyPaymentInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_PAID);
                //            LogInfoBlo.RegistryMoneyLogInfo(txtHfRentCd.Text, strItemCd, double.Parse(txtInsPay.Text), Session["MemNo"].ToString(), strInsMemIP);

                //            MngAccountsBlo.RegistrySettelmentInfo(txtHfUserSeq.Text, txtHfRentCd.Text, strItemCd, txtHfRentalYear.Text, txtHfRentalMM.Text, double.Parse(txtRealBaseRate.Text.Replace(".", "")),
                //            double.Parse(txtInsPay.Text), ddlPaymentCd.Text, strInsPayDay, txtHfVatRatio.Text, Session["MemNo"].ToString(), strInsMemIP);

                //            dblItemTotEmAmt = double.Parse(txtInsPay.Text) / double.Parse(txtRealBaseRate.Text.Replace(".", "").Replace(",", ""));

                //            DataTable dtAccnt = MngAccountsBlo.RegistryAccountsInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strInsPayDay, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text, CommValue.DIRECT_TYPE_VALUE_AGENT
                //    , CommValue.ITEM_TYPE_VALUE_LATEMNGFEE, CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, txtHfUserSeq.Text, strUserTaxCd, strBillCd, strBillNo
                //    , double.Parse(txtRealBaseRate.Text.Replace(".", "")), dblItemTotEmAmt, double.Parse(txtInsPay.Text), ddlPaymentCd.Text, double.Parse(txtHfVatRatio.Text), string.Empty, string.Empty, Session["MemNo"].ToString(), strInsMemIP);

                //            if (dtAccnt != null)
                //            {
                //                if (dtAccnt.Rows.Count > 0)
                //                {
                //                    int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                //                    int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                //                    strUserTaxCd = dtAccnt.Rows[0]["UserTaxCd"].ToString();
                //                    strBillCd = dtAccnt.Rows[0]["BillCd"].ToString();
                //                    strBillNo = dtAccnt.Rows[0]["BillNo"].ToString();

                //                    dblUniPrime = double.Parse(txtInsPay.Text) * (100 - double.Parse(txtHfVatRatio.Text)) / 100;

                //                    MngAccountsBlo.RegistryAccountsDetInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strInsPayDay, intPaymentSeq, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text, CommValue.DIRECT_TYPE_VALUE_AGENT
                //                        , CommValue.ITEM_TYPE_VALUE_LATEMNGFEE, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty, CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH
                //                        , dblUniPrime, dblUniPrime, double.Parse(txtInsPay.Text), double.Parse(txtInsPay.Text), CommValue.NUMBER_VALUE_0, txtHfRentalYear.Text, txtHfRentalMM.Text, strRoomNo
                //                        , double.Parse(txtHfVatRatio.Text), CommValue.CONCLUSION_TYPE_TEXT_YES, Session["MemNo"].ToString(), strInsMemIP);
                //                }

                //            }
                //        }
                //        else
                //        {
                //            double fltTempPayment = double.Parse(ltInsLateFee.Text.Replace(",", "").Replace(".", "")) - fltHfTotalPayment;
                //            string strTempPayment = fltTempPayment.ToString();
                //            MngLateFeeBlo.RegistryLateFeeInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, strTempPayment, strInsPayDay, ltInsLateDt.Text, ltInsLateFeeRatio.Text, Session["MemNo"].ToString(), strInsMemIP, ddlPaymentCd.Text);
                //            MngLateFeeBlo.ModifyLateFeeInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_PAID);
                //            MngPaymentBlo.ModifyPaymentInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_PAID);
                //            LogInfoBlo.RegistryMoneyLogInfo(txtHfRentCd.Text, strItemCd, double.Parse(txtInsPay.Text), Session["MemNo"].ToString(), strInsMemIP);

                //            MngAccountsBlo.RegistrySettelmentInfo(txtHfUserSeq.Text, txtHfRentCd.Text, strItemCd, txtHfRentalYear.Text, txtHfRentalMM.Text, double.Parse(txtRealBaseRate.Text.Replace(".", "")),
                //            double.Parse(strTempPayment), ddlPaymentCd.Text, strInsPayDay, txtHfVatRatio.Text, Session["MemNo"].ToString(), strInsMemIP);

                //            dblItemTotEmAmt = double.Parse(strTempPayment) / double.Parse(txtRealBaseRate.Text.Replace(".", "").Replace(",", ""));

                //            DataTable dtAccnt = MngAccountsBlo.RegistryAccountsInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strInsPayDay, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text, CommValue.DIRECT_TYPE_VALUE_AGENT
                //    , CommValue.ITEM_TYPE_VALUE_LATEMNGFEE, CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, txtHfUserSeq.Text, strUserTaxCd, strBillCd, strBillNo
                //    , double.Parse(txtRealBaseRate.Text.Replace(".", "")), dblItemTotEmAmt, double.Parse(strTempPayment), ddlPaymentCd.Text, double.Parse(txtHfVatRatio.Text), string.Empty, string.Empty, Session["MemNo"].ToString(), strInsMemIP);

                //            if (dtAccnt != null)
                //            {
                //                if (dtAccnt.Rows.Count > 0)
                //                {
                //                    int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                //                    int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                //                    strUserTaxCd = dtAccnt.Rows[0]["UserTaxCd"].ToString();
                //                    strBillCd = dtAccnt.Rows[0]["BillCd"].ToString();
                //                    strBillNo = dtAccnt.Rows[0]["BillNo"].ToString();

                //                    dblUniPrime = double.Parse(txtInsPay.Text) * (100 - double.Parse(txtHfVatRatio.Text)) / 100;

                //                    MngAccountsBlo.RegistryAccountsDetInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strInsPayDay, intPaymentSeq, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text, CommValue.DIRECT_TYPE_VALUE_AGENT
                //                        , CommValue.ITEM_TYPE_VALUE_LATEMNGFEE, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty, CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH
                //                        , dblUniPrime, dblUniPrime, double.Parse(strTempPayment), double.Parse(strTempPayment), CommValue.NUMBER_VALUE_0, txtHfRentalYear.Text, txtHfRentalMM.Text, strRoomNo
                //                        , double.Parse(txtHfVatRatio.Text), CommValue.CONCLUSION_TYPE_TEXT_YES, Session["MemNo"].ToString(), strInsMemIP);
                //                }

                //            }
                //        }

                //        StringBuilder sbView = new StringBuilder();

                //        sbView.Append(Master.PAGE_VIEW);
                //        sbView.Append("?");
                //        sbView.Append(Master.PARAM_DATA1);
                //        sbView.Append("=");
                //        sbView.Append(Request.Params[Master.PARAM_DATA1].ToString());
                //        sbView.Append("&");
                //        sbView.Append(Master.PARAM_DATA2);
                //        sbView.Append("=");
                //        sbView.Append(txtHfFeeTy.Text);
                //        sbView.Append("&");
                //        sbView.Append(Master.PARAM_DATA3);
                //        sbView.Append("=");
                //        sbView.Append(txtHfRentalYear.Text);
                //        sbView.Append("&");
                //        sbView.Append(Master.PARAM_DATA4);
                //        sbView.Append("=");
                //        sbView.Append(txtHfRentalMM.Text);
                //        sbView.Append("&");
                //        sbView.Append(Master.PARAM_DATA5);
                //        sbView.Append("=");
                //        sbView.Append(txtHfUserSeq.Text);

                //        Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);

                //    }

                //    else
                //    {
                //        StringBuilder sbInfo = new StringBuilder();

                //        sbInfo.Append("alert('");
                //        sbInfo.Append(AlertNm["INFO_LATEFEE"]);
                //        sbInfo.Append("');");

                //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LatePayment", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
                //    }
                //}

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
        protected void lvMngLateFeeInfoView_ItemCreated(object sender, ListViewItemEventArgs e)
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
        protected void lvMngLateFeeInfoView_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltPay = (Literal)lvMngLateFeeInfoView.FindControl("ltPay");
            ltPay.Text = TextNm["PAYMENT"];
            Literal ltPayDay = (Literal)lvMngLateFeeInfoView.FindControl("ltPayDay");
            ltPayDay.Text = TextNm["PAYDAY"];
            Literal ltPay1 = (Literal)lvMngLateFeeInfoView.FindControl("ltPay1");
            ltPay1.Text = TextNm["PAYMENT"];
            Literal ltPayDay1 = (Literal)lvMngLateFeeInfoView.FindControl("ltPayDay1");
            ltPayDay1.Text = TextNm["PAYDAY"];
        }

        protected void lvMngLateFeeInfoView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["PayAmt"].ToString()))
                {
                    TextBox txtPayList = (TextBox)iTem.FindControl("txtPayList");
                    fltSum = fltSum + double.Parse(drView["PayAmt"].ToString());
                    txtPayList.Text = TextLib.MakeVietIntNo(double.Parse(drView["PayAmt"].ToString()).ToString("###,##0"));
                }
                else
                {
                    TextBox txtPayList = (TextBox)iTem.FindControl("txtPayList");
                    txtPayList.Text = "-";
                }

                if (!string.IsNullOrEmpty(drView["PayDt"].ToString()))
                {
                    string strPayDt = drView["PayDt"].ToString();

                    Literal ltPayDayList = (Literal)iTem.FindControl("ltPayDayList");
                    ltPayDayList.Text = strPayDt.Substring(0, 4) + "-" + strPayDt.Substring(4, 2) + "-" + strPayDt.Substring(6, 2);
                }
                else
                {
                    Literal ltPayDayList = (Literal)iTem.FindControl("ltPayDayList");
                    ltPayDayList.Text = "-";
                }

                hfTotalPayment.Value = fltSum.ToString();

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                if (!string.IsNullOrEmpty(drView["InsDate"].ToString()))
                {
                    TextBox txtHfInsDate = (TextBox)iTem.FindControl("txtHfInsDate");
                    txtHfInsDate.Text = drView["InsDate"].ToString();

                    string strToday = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("-", "").Substring(0, 8);
                    int strTodayTmp = Int32.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace("-", "").Substring(9, 2));
                    string strInsDt = txtHfInsDate.Text.Replace("-", "").Substring(0, 8);

                    if (!strToday.Equals(strInsDt))
                    {
                        if ((double.Parse(strInsDt) + 1).ToString().Equals(strToday))
                        {
                            if (strTodayTmp <= 9)
                            {
                                TextBox txtPayList = (TextBox)iTem.FindControl("txtPayList");
                                txtPayList.ReadOnly = false;
                                txtPayList.Width = 130;
                                imgbtnModify.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        TextBox txtPayList = (TextBox)iTem.FindControl("txtPayList");
                        txtPayList.ReadOnly = false;
                        txtPayList.Width = 130;
                        imgbtnModify.Visible = true;
                    }
                }

                if (!string.IsNullOrEmpty(drView["PaySeq"].ToString()))
                {
                    TextBox txtHfPaySeq = (TextBox)iTem.FindControl("txtHfPaySeq");
                    txtHfPaySeq.Text = drView["PaySeq"].ToString();
                }
            }
        }

        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + Request.Params[Master.PARAM_DATA1].ToString() + "&" + Master.PARAM_DATA2 + "=" + Request.Params[Master.PARAM_DATA2].ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngLateFeeInfoView_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                //double dblTotal = 0;
                //double dblTemp = 0;

                //TextBox txtHfPaySeq = (TextBox)lvMngLateFeeInfoView.Items[e.ItemIndex].FindControl("txtHfPaySeq");
                //TextBox txtPayList = (TextBox)lvMngLateFeeInfoView.Items[e.ItemIndex].FindControl("txtPayList");
                //Literal ltPayDayList = (Literal)lvMngLateFeeInfoView.Items[e.ItemIndex].FindControl("ltPayDayList");
                //double dblPayList = double.Parse(txtPayList.Text.Replace(",", "").Replace(".", ""));

                //dblUniPrime = dblPayList * (100 - double.Parse(txtHfVatRatio.Text)) / 100;

                //string strMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                //string strMemNo = Session["MemNo"].ToString();
                //string strPayDt = ltPayDayList.Text.Replace("-", "");

                //string strItemCd = string.Empty;
                //if (txtHfFeeTy.Text.Equals("0001"))
                //{
                //    strItemCd = "0005";
                //}
                //else
                //{
                //    strItemCd = "0006";
                //}

                //for (int intTmpI = 0; intTmpI < lvMngLateFeeInfoView.Items.Count; intTmpI++)
                //{
                //    dblTemp = double.Parse(((TextBox)lvMngLateFeeInfoView.Items[intTmpI].FindControl("txtPayList")).Text.ToString().Replace(".", "").Replace(",", ""));

                //    dblTotal = dblTotal + dblTemp;
                //}

                //if (dblTotal > double.Parse(ltInsLateFee.Text.Replace(".", "").Replace(",", "")))
                //{
                //    StringBuilder sbWarning = new StringBuilder();

                //    sbWarning.Append("alert('");
                //    sbWarning.Append(AlertNm["ALERT_MODIFY_AMOUNT"]);
                //    sbWarning.Append("');");

                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                //}
                //else
                //{
                //    MngLateFeeBlo.ModifyLateFeeAmoumt(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfRentalYear.Text, txtHfRentalMM.Text, txtHfUserSeq.Text, txtHfPaySeq.Text, dblPayList, strMemIP, strMemNo);
                //    LogInfoBlo.RegistryMoneyLogInfo(txtHfRentCd.Text, strItemCd, dblPayList, Session["MemNo"].ToString(), strMemIP);

                //    //과금관리정산수정
                //    MngAccountsBlo.ModifySettelmentInfo(txtHfUserSeq.Text, txtHfRentCd.Text, strItemCd, txtHfRentalYear.Text, txtHfRentalMM.Text, Int32.Parse(txtHfPaySeq.Text), dblPayList, Session["MemNo"].ToString(), strMemIP);

                //    //금액수정LedgerInfo
                //    MngAccountsBlo.ModifyCancelSettelmentInfo(strPayDt, Int32.Parse(txtHfPaySeq.Text), txtHfRentCd.Text, CommValue.ITEM_TYPE_VALUE_LATEMNGFEE, dblUniPrime, dblPayList, strMemNo, strMemIP);
                //}
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}