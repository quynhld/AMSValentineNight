using System;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;
using KN.Resident.Biz;
using KN.Resident.Ent;
using System.Configuration;

using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace KN.Web.Resident.Contract
{
    [Transaction(TransactionOption.Required)]
    public partial class TowerSalesModify : BasePage
    {
        RentMngDs.RentInfo riDs = new RentMngDs.RentInfo();
        public string test = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 기존 임시 데이터 삭제
                // KN_USP_RES_DELETE_TEMPRENTDEPOSITINFO_M01
                ContractMngBlo.RemoveEntireTempRentDepositInfo();

                CheckParams();

                // 금일 환율정보가 없을 경우 환율등록 페이지로 이동시킬것.
                // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S00
                DataTable dtExchangeReturn = ExchangeMngBlo.WatchExchangeRateInfo(txtHfRentCd.Text);

                if (dtExchangeReturn != null)
                {
                    if (dtExchangeReturn.Rows.Count > 0)
                    {
                        txtHfExchangeRate.Text = dtExchangeReturn.Rows[0]["DongToDollar"].ToString();
                        hfExchangeRate.Value = dtExchangeReturn.Rows[0]["DongToDollar"].ToString();
                        txtExchangeRate.Text = dtExchangeReturn.Rows[0]["DongToDollar"].ToString();

                        InitControls();

                        LoadData();
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();
                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["ALERT_REGISTER_EXCHANGERATE"]);
                        sbWarning.Append("');");
                        sbWarning.Append("document.location.href=\"" + Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "\";");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                }
                else
                {
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
            }
        }

        protected void InitControls()
        {
            ltPodium.Text = TextNm["PODIUMYN"];
            ltBasicInfo.Text = TextNm["CONTRACTINFO"];
            ltIncharge.Text = TextNm["INCHARGE"];
            ltLandloadNm.Text = TextNm["CONTRACTOR"];
            ltContNo.Text = TextNm["CONTNO"];
            ltLandloadAddr.Text = TextNm["ADDR"];
            ltRentAddr.Text = TextNm["ADDR"];
            ltLandloadCorpCert.Text = TextNm["IDNO"];
            ltIssueDt.Text = TextNm["ISSUEDT"];
            ltLandloadTelNo.Text = TextNm["TEL"];
            ltLandloadMobileNo.Text = TextNm["MOBILE"];
            ltLandloadFAX.Text = TextNm["FAX"];
            ltLandloadEmail.Text = TextNm["EMAIL"];
            ltLandloadRepNm.Text = TextNm["REPRESENTATIVE"];
            ltLandloadTaxCd.Text = TextNm["TAXCD"];

            ltRentTerm.Text = TextNm["TERMINFO"];
            ltOTLAgreeDt.Text = TextNm["OTLFIXDT"];
            ltRentAgreeDt.Text = TextNm["RENTFIXDT"];
            ltFreeRentMonth.Text = TextNm["RENTFREEDT"];
            ltMonthUnit.Text = TextNm["MONTHS"];
            ltRentStartDt.Text = TextNm["COMMENCINGDT"];
            ltRentEndDt.Text = TextNm["EXPIRINGDT"];
            ltTermMonth.Text = TextNm["TERMYEAR"];
            ltMonth.Text = TextNm["MONTHS"];
            ltHandOverDt.Text = TextNm["HANDOVERDT"];

            ltRoomInfo.Text = TextNm["ROOMINFO"];
            ltRoomNo.Text = TextNm["UNITNO"];
            ltFloor.Text = TextNm["FLOOR"];
            ltRentLeasingArea.Text = TextNm["LEASINGAREA"];

            ltRetalFee.Text = TextNm["RENTALFEE"];
            ltExchangeRate.Text = TextNm["EXCHANGERATE"];
            ltExchangeUnit.Text = TextNm["DONG"];
            lnkbtnChange.Text = TextNm["TEMPCHANGE"];
            //ltPayStartYYYYMM.Text = TextNm["COMMENCINGDT"];
            ltPayStartYYYYMMUnit.Text = TextNm["MONTH"];
            ltPayTermMonth.Text = TextNm["PAYMENTCYCLE"];
            ltPayTermMonthUnit.Text = TextNm["MONTHS"];
            ltPayDay.Text = TextNm["PAYDAY"];
            ltPayDayUnit.Text = TextNm["DAYS"];

            //ltTopRentalFeeStartDt.Text = TextNm["COMMENCINGDT"];
            ltTopRentalFeeEndDt.Text = TextNm["EXPIRINGDT"];
            ltTopRentalFeeRate.Text = TextNm["EXCHANGERATE"];
            ltTopRentalFeeAmt.Text = TextNm["PAYMENT"] + " (" + TextNm["PERMMRENTUSD"] + ")";

            ltMinimumIncome.Text = TextNm["MINIMUM"];
            ltApplyRate.Text = TextNm["APPLYRATE"];

            ltRentalFeeExcRateUnit.Text = TextNm["DONG"];
            ltRentalFeeAmtUnit.Text = TextNm["DOLLAR"];

            ltMngFee.Text = TextNm["MANAGEFEE"];
            //ltPerMMMngVND.Text = TextNm["PERMMRENTVND"];
            //ltPerMMMngVNDNoUnit.Text = TextNm["DONG"];
            //ltPerMMMngUSD.Text = TextNm["PERMMRENTUSD"];
            //ltPerMMMngUSDNoUnit.Text = TextNm["DOLLAR"];

            //ltInitMMMngDay.Text = TextNm["FITTINGDAY"];
            //ltInitMMMngDayUnit.Text = TextNm["DAYS"];
            //ltInitMMMngDt.Text = TextNm["FITTINGOUTDT"];
            //ltInitPerMMMngVND.Text = TextNm["INITPERMMRENTVND"];
            //ltInitPerMMMngVNDUnit.Text = TextNm["DONG"];
            //ltInitPerMMMngUSD.Text = TextNm["INITPERMMRENTUSD"];
            //ltInitPerMMMngUSDUnit.Text = TextNm["DOLLAR"];

            ltDeposit.Text = TextNm["DEPOSIT"];
            ltDepositExpDt.Text = TextNm["SCHEDULEDDATE"];
            ltDepositExpAmt.Text = TextNm["PAYMENT"];
            ltDepositExpAmtUnit.Text = TextNm["DONG"];
            ltDepositExcRate.Text = TextNm["EXCHANGERATE"];
            ltDepositExcRateUnit.Text = TextNm["DONG"];
            ltDepositPayDt.Text = TextNm["PAYDAY"];
            ltDepositPayAmt.Text = TextNm["PAYMENT"];
            ltDepositPayAmtUnit.Text = TextNm["DONG"];
            ltSumDepositVNDNo.Text = TextNm["SUMDEPOSITVND"];
            ltDepositSumVNDNoUnit.Text = TextNm["DONG"];
            ltSumDepositUSDNo.Text = TextNm["SUMDEPOSITUSD"];
            ltDepositSumUSDNoUnit.Text = TextNm["DOLLAR"];

            ltInterior.Text = TextNm["INTERIOR"];
            ltInteriorStartDt.Text = TextNm["COMMENCINGDT"];
            ltInteriorEndDt.Text = TextNm["EXPIRINGDT"];
            ltConsDeposit.Text = TextNm["BALANCECONST"];
            ltConsDepositUnit.Text = TextNm["DONG"];
            ltConsDepositDt.Text = TextNm["BALANCEDT"];
            ltConsRefund.Text = TextNm["REFUNDCONST"];
            ltConsRefundUnit.Text = TextNm["DONG"];
            ltConsRefundDt.Text = TextNm["REFUNDDT"];
            ltDifferenceReason.Text = TextNm["DIFFERENCEREASON"];

            ltOther.Text = TextNm["OTHERS"];
            ltMemo.Text = TextNm["MEMO"];

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.Visible = Master.isWriteAuthOk;
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnModify.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + CommValue.TENANTTY_VALUE_CORPORATION + "','" + CommValue.TERM_VALUE_LONGTERM + "','" + CommValue.TERM_VALUE_SHORTTERM + "');";
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";
            lnkbtnChange.OnClientClick = "javascript:return fnChangePopup('" + txtHfExchangeRate.Text + "','" + txtExchangeRate.ClientID + "','" + hfExchangeRate.ClientID + "');";

            imgbtnRegist.OnClientClick = "javascript:return fnCheckDeposit('" + AlertNm["ALERT_INSERT_BLANK"] + "')";

            txtInchage.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadTelFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadTelMidNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadTelRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadMobileFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadMobileMidNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadMobileRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadFAXFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadFAXMidNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLandloadFAXRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtFreeRentMonth.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTermMonth.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFloor.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtRentLeasingArea.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            txtPayStartYYYYMM.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtPayTermMonth.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtPayDay.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            //txtRentalFeeExcRate.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            //txtRentalFeeExpAmt.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            txtMinimumIncome.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtApplyRate.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            txtSumDepositVNDNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtSumDepositUSDNo.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            //txtPerMMMngVND.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            //txtPerMMMngUSD.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            txtDepositExpAmt.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtDepositPayAmt.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtLandloadRepNm.Text = string.Empty;
            txtLandloadRepNm.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            txtLandloadTaxCd.Text = string.Empty;

            txtSumDepositUSDNo.Text = string.Empty;
            txtSumDepositUSDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
            txtSumDepositVNDNo.Text = string.Empty;
            txtSumDepositVNDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;


            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlPersonal, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_TENANTTY);
            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlTerm, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_TERM);
            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlContStep, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_CONTSTEP);
            CommCdDdlUtil.MakeEtcSubCdDdlTitle(ddlIndustry, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_INDUSTRY);
            CommCdDdlUtil.MakeEtcSubCdDdlTitle(ddlNat, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_NAT);          

            ddlPodium.SelectedValue = CommValue.CHOICE_VALUE_NO;
            txtHfPodium.Text = CommValue.CHOICE_VALUE_NO;

            if (!txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_SHOP))
            {
                ddlPodium.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
            else
            {
                ddlPodium.Enabled = CommValue.AUTH_VALUE_TRUE;
            }

            ddlTerm.SelectedValue = CommValue.TERM_VALUE_SHORTTERM;

            //MakeNumberDdl(ddlThird);
            //MakeNumberDdl(ddlFouth);
            //MakeNumberDdl(ddlFifth);
            isApplyFeeMn.Attributes["onclick"] = "fnApplyFee()";

            chkCC.Attributes["onclick"] = "chkCCChange(this)";
        }

        protected void CheckParams()
        {
            string strRentCd = string.Empty;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) &&
                    !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                {
                    txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    txtHfParamRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    txtHfParamRentSeq.Text = Request.Params[Master.PARAM_DATA2].ToString();
                }
                else
                {
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_OFFICE;
                    txtHfParamRentCd.Text = CommValue.RENTAL_VALUE_OFFICE;
                    txtHfParamRentSeq.Text = CommValue.NUMBER_VALUE_ONE;
                }
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_OFFICE;
                txtHfParamRentCd.Text = CommValue.RENTAL_VALUE_OFFICE;
                txtHfParamRentSeq.Text = CommValue.NUMBER_VALUE_ONE;
            }
        }

        private void LoadData()
        {
            // 임대계약 상세정보 조회 (수정용)
            // KN_USP_RES_SELECT_RENTINFO_S04
            //DataSet dsReturn = ContractMngBlo.WatchRentInfoDetailView(Session["LangCd"].ToString(), txtHfRentCd.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), Int32.Parse(txtHfParamRentSeq.Text));
            DataSet dsReturn = ContractMngBlo.WatchRentInfoView(Session["LangCd"].ToString(), txtHfRentCd.Text, Int32.Parse(txtHfParamRentSeq.Text));

            if (dsReturn != null)
            {
                if (dsReturn.Tables[0] != null)
                {
                    if (dsReturn.Tables[0].Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        MakeData(dsReturn.Tables[0]);

                        if (dsReturn.Tables[0].Rows[0]["PodiumYn"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
                        {
                            if (dsReturn.Tables[2] != null)
                            {
                                LoadRentFee(dsReturn.Tables[2]);

                                mvRentFee.ActiveViewIndex = CommValue.NUMBER_VALUE_0;
                            }
                        }
                        else
                        {
                            txtMinimumIncome.Text = double.Parse(dsReturn.Tables[0].Rows[0]["MinimumRentFee"].ToString()).ToString("###,##0.##");
                            txtApplyRate.Text = dsReturn.Tables[0].Rows[0]["ApplyRate"].ToString();

                            mvRentFee.ActiveViewIndex = CommValue.NUMBER_VALUE_1;
                        }
                        hfCCtype.Value = dsReturn.Tables[0].Rows[0]["CURNCY_TYPE"].ToString();
                        chkCC.Checked = dsReturn.Tables[0].Rows[0]["CURNCY_TYPE"].ToString() == "CC";
                        if (!chkCC.Checked)
                        {
                            txtExchangeRate.Text = dsReturn.Tables[0].Rows[0]["FIXED_DONGTODOLLAR"].ToString();
                            txtExchangeRate.ReadOnly = false;
                        }
                        txtFloation.Text = dsReturn.Tables[0].Rows[0]["INFLATION_RATE"].ToString();
                    }
                }

                if (dsReturn.Tables[1] != null)
                {
                    lvDepositList.DataSource = dsReturn.Tables[1];
                    lvDepositList.DataBind();
                }
                if (dsReturn.Tables[3].Rows.Count > 0)
                {
                    LoadFitOutFee(dsReturn.Tables[3]);
                    isApplyFeeMn.Checked = true;
                }
                else
                {
                    isApplyFeeMn.Checked = false;
                  //  listFitOutFee.Visible = false;
                }

                if (dsReturn.Tables[4].Rows.Count > 0)
                {
                    LoadMngFee(dsReturn.Tables[4]);
                }
                else
                {
                    //lineRow1.Visible = true;
                    //ListMngFee.Visible = false;
                }
            }
        }

        /// <summary>
        /// 데이터 바인딩
        /// </summary>
        /// <param name="dtParams"></param>
        private void MakeData(DataTable dtParams)
        {
            if (dtParams != null)
            {
                if (dtParams.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    foreach (DataRow dr in dtParams.Select())
                    {
                        if (!string.IsNullOrEmpty(dr["PersonalCd"].ToString()))
                        {
                            ddlPersonal.SelectedValue = dr["PersonalCd"].ToString();

                            if (ddlPersonal.SelectedValue.Equals(CommValue.TENANTTY_VALUE_PERSONAL))
                            {
                                ltLandloadCorpCert.Text = TextNm["IDNO"];
                                ltLandloadNm.Text = TextNm["CONTRACTOR"];

                                txtLandloadRepNm.Text = string.Empty;
                                txtLandloadRepNm.CssClass = "bgType3";
                                txtLandloadRepNm.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                                txtLandloadTaxCd.CssClass = "bgType3";
                            }
                            else
                            {
                                ltLandloadCorpCert.Text = TextNm["CERTINCORP"];
                                ltLandloadNm.Text = TextNm["COMPNM"];

                                txtLandloadRepNm.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                                txtLandloadRepNm.CssClass = "bgType2";
                                txtLandloadTaxCd.CssClass = "bgType2";
                            }
                        }

                        if (!string.IsNullOrEmpty(dr["InsKNMemNo"].ToString()))
                        {
                            txtInchage.Text = TextLib.StringDecoder(dr["InsKNMemNo"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["ContStepCd"].ToString()))
                        {
                            ddlContStep.SelectedValue = dr["ContStepCd"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["IndustryCd"].ToString()))
                        {
                            ddlIndustry.SelectedValue = dr["IndustryCd"].ToString();
                        }
                        else
                        {
                            ddlIndustry.SelectedIndex = 0;
                        }

                        if (!string.IsNullOrEmpty(dr["NatCd"].ToString()))
                        {
                            ddlNat.SelectedValue = dr["NatCd"].ToString();
                        }
                        else
                        {
                            ddlNat.SelectedIndex = 0;
                        }

                        if (!string.IsNullOrEmpty(dr["RenewDT"].ToString()))
                        {
                             txtRenewDt.Text = TextLib.MakeDateEightDigit(dr["RenewDT"].ToString());                            
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadNm"].ToString()))
                        {
                            txtLandloadNm.Text = TextLib.StringDecoder(dr["LandloadNm"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["ContractNo"].ToString()))
                        {
                            txtContNo.Text = dr["ContractNo"].ToString();
                            hfContNo.Value = dr["ContractNo"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadAddr"].ToString()))
                        {
                            txtLandloadAddr.Text = TextLib.StringDecoder(dr["LandloadAddr"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadDetAddr"].ToString()))
                        {
                            txtLandloadDetAddr.Text = TextLib.StringDecoder(dr["LandloadDetAddr"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadCorpCert"].ToString()))
                        {
                            txtLandloadCorpCert.Text = TextLib.StringDecoder(dr["LandloadCorpCert"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadIssuedDt"].ToString()))
                        {
                            txtIssueDt.Text = TextLib.MakeDateEightDigit(dr["LandloadIssuedDt"].ToString());
                            hfIssueDt.Value = TextLib.MakeDateEightDigit(dr["LandloadIssuedDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadTel"].ToString()))
                        {
                            string[] strTelNo = dr["LandloadTel"].ToString().Split('-');
                            string[] strTelSubNo = strTelNo[0].Replace(" ", "").Split(')');

                            txtLandloadTelFrontNo.Text = strTelSubNo[0];
                            txtLandloadTelMidNo.Text = strTelSubNo[1];
                            txtLandloadTelRearNo.Text = strTelNo[1];
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadMobile"].ToString()))
                        {
                            string[] strMobileNo = dr["LandloadMobile"].ToString().Split('-');
                            string[] strMobileSubNo = strMobileNo[0].Replace(" ", "").Split(')');

                            txtLandloadMobileFrontNo.Text = strMobileSubNo[0];
                            txtLandloadMobileMidNo.Text = strMobileSubNo[1];
                            txtLandloadMobileRearNo.Text = strMobileNo[1];
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadFAX"].ToString()))
                        {
                            string[] strFaxNo = dr["LandloadFAX"].ToString().Split('-');
                            string[] strFaxSubNo = strFaxNo[0].Replace(" ", "").Split(')');

                            txtLandloadFAXFrontNo.Text = strFaxSubNo[0];
                            txtLandloadFAXMidNo.Text = strFaxSubNo[1];
                            txtLandloadFAXRearNo.Text = strFaxNo[1];
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadEmail"].ToString()))
                        {
                            string [] strEmail = dr["LandloadEmail"].ToString().Split('@');

                            txtLandloadEmailID.Text = strEmail[0];
                            txtLandloadEmailServer.Text = strEmail[1];
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadRepNm"].ToString()))
                        {
                            txtLandloadRepNm.Text = TextLib.StringDecoder(dr["LandloadRepNm"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadTaxCd"].ToString()))
                        {
                            txtLandloadTaxCd.Text = dr["LandloadTaxCd"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RentAddr"].ToString()))
                        {
                            txtRentAddr.Text = dr["RentAddr"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RentDetAddr"].ToString()))
                        {
                            txtRentDetAddr.Text = dr["RentDetAddr"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["OTLAgreeDt"].ToString()))
                        {
                            txtOTLAgreeDt.Text = TextLib.MakeDateEightDigit(dr["OTLAgreeDt"].ToString());
                            hfOTLAgreeDt.Value = TextLib.MakeDateEightDigit(dr["OTLAgreeDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["RentAgreeDt"].ToString()))
                        {
                            txtRentAgreeDt.Text = TextLib.MakeDateEightDigit(dr["RentAgreeDt"].ToString());
                            hfRentAgreeDt.Value = TextLib.MakeDateEightDigit(dr["RentAgreeDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["FreeRentMonth"].ToString()))
                        {
                            txtFreeRentMonth.Text = dr["FreeRentMonth"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RentStartDt"].ToString()))
                        {
                            txtRentStartDt.Text = TextLib.MakeDateEightDigit(dr["RentStartDt"].ToString());
                            hfRentStartDt.Value = TextLib.MakeDateEightDigit(dr["RentStartDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["RentEndDt"].ToString()))
                        {
                            txtRentEndDt.Text = TextLib.MakeDateEightDigit(dr["RentEndDt"].ToString());
                            hfRentEndDt.Value = TextLib.MakeDateEightDigit(dr["RentEndDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["TermMonth"].ToString()))
                        {
                            txtTermMonth.Text = dr["TermMonth"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["HandOverDt"].ToString()))
                        {
                            txtHandOverDt.Text = TextLib.MakeDateEightDigit(dr["HandOverDt"].ToString());
                            hfHandOverDt.Value = TextLib.MakeDateEightDigit(dr["HandOverDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["FloorNo"].ToString()))
                        {
                            txtFloor.Text = dr["FloorNo"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RoomNo"].ToString()))
                        {
                            string strRoomNo = dr["RoomNo"].ToString();
                            txtRoomNo.Text = strRoomNo;

                            //if (strRoomNo.Length >= 5)
                            //{
                            //    ddlFirst.Text = strRoomNo.Substring(0, 1);

                            //    if (ddlFirst.SelectedValue.Equals("B"))
                            //    {
                            //        ddlSecond.Items.Clear();
                            //        ddlSecond.Items.Add(new ListItem("-", "-"));
                            //    }
                            //    else
                            //    {
                            //        MakeNumberDdl(ddlSecond);
                            //    }

                            //    ddlSecond.Text = strRoomNo.Substring(1, 1);
                            //    ddlThird.Text = strRoomNo.Substring(2, 1);
                            //    ddlFouth.Text = strRoomNo.Substring(3, 1);
                            //    ddlFifth.Text = strRoomNo.Substring(4, 1);
                            //}

                            //if (strRoomNo.Length >= 6)
                            //{
                            //    ddlSixth.Text = strRoomNo.Substring(5, 1);
                            //}

                            //if (strRoomNo.Length == 7)
                            //{
                            //    MakeNumberDdl(ddlSeventh);
                            //    MakeNumberDdl(ddlEighth);

                            //    ddlSeventh.Text = strRoomNo.Substring(6, 1);
                            //}
                            //else if (strRoomNo.Length > 7)
                            //{
                            //    MakeNumberDdl(ddlSeventh);
                            //    MakeNumberDdl(ddlEighth);

                            //    ddlSeventh.Text = strRoomNo.Substring(6, 1);
                            //    ddlEighth.Text = strRoomNo.Substring(7, 1);
                            //}
                        }

                        if (!string.IsNullOrEmpty(dr["ExtRoomNo"].ToString()))
                        {
                            txtExtRoomNo.Text = dr["ExtRoomNo"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RentLeasingArea"].ToString()))
                        {
                            txtRentLeasingArea.Text = dr["RentLeasingArea"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["DongToDollar"].ToString()))
                        {
                            txtExchangeRate.Text = dr["DongToDollar"].ToString();
                            hfExchangeRate.Value = dr["DongToDollar"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["PayStartYYYYMM"].ToString()))
                        {
                            txtPayStartYYYYMM.Text = dr["PayStartYYYYMM"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["PayTermMonth"].ToString()))
                        {
                            txtPayTermMonth.Text = dr["PayTermMonth"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["PayDay"].ToString()))
                        {
                            txtPayDay.Text = dr["PayDay"].ToString();
                        }

                        //if (!string.IsNullOrEmpty(dr["TotViAmtNo"].ToString()))
                        //{
                        //    if (double.Parse(dr["TotViAmtNo"].ToString()) > CommValue.NUMBER_VALUE_0_0)
                        //    {
                        //        txtSumRentVNDNo.Text = dr["TotViAmtNo"].ToString();
                        //        ddlTerm.SelectedValue = CommValue.TERM_VALUE_LONGTERM;
                        //    }
                        //    else
                        //    {
                        //        txtSumRentVNDNo.Text = dr["TotViAmtNo"].ToString();
                        //        ddlTerm.SelectedValue = CommValue.TERM_VALUE_SHORTTERM;
                        //    }
                        //}
                        //else
                        //{
                        //    ddlTerm.SelectedValue = CommValue.TERM_VALUE_SHORTTERM;
                        //}

                        //if (!string.IsNullOrEmpty(dr["TotEnAmtNo"].ToString()))
                        //{
                        //    txtSumRentUSDNo.Text = dr["TotEnAmtNo"].ToString();
                        //}

                        if (!string.IsNullOrEmpty(dr["DepositViAmtNo"].ToString()))
                        {
                            txtSumDepositVNDNo.Text = dr["DepositViAmtNo"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["DepositEnAmtNo"].ToString()))
                        {
                            txtSumDepositUSDNo.Text = dr["DepositEnAmtNo"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["InteriorStartDt"].ToString()))
                        {
                            txtInteriorStartDt.Text = TextLib.MakeDateEightDigit(dr["InteriorStartDt"].ToString());
                            hfInteriorStartDt.Value = TextLib.MakeDateEightDigit(dr["InteriorStartDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["InteriorEndDt"].ToString()))
                        {
                            txtInteriorEndDt.Text = TextLib.MakeDateEightDigit(dr["InteriorEndDt"].ToString());
                            hfInteriorEndDt.Value = TextLib.MakeDateEightDigit(dr["InteriorEndDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["ConsDeposit"].ToString()))
                        {
                            txtConsDeposit.Text = dr["ConsDeposit"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["ConsDepositDt"].ToString()))
                        {
                            txtConsDepositDt.Text = TextLib.MakeDateEightDigit(dr["ConsDepositDt"].ToString());
                            hfConsDepositDt.Value = TextLib.MakeDateEightDigit(dr["ConsDepositDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["ConsRefund"].ToString()))
                        {
                            txtConsRefund.Text = dr["ConsRefund"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["ConsRefundDt"].ToString()))
                        {
                            txtConsRefundDt.Text = TextLib.MakeDateEightDigit(dr["ConsRefundDt"].ToString());
                            hfConsRefundDt.Value = TextLib.MakeDateEightDigit(dr["ConsRefundDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["DifferenceReason"].ToString()))
                        {
                            txtDifferenceReason.Text = TextLib.StringDecoder(dr["DifferenceReason"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["Memo"].ToString()))
                        {
                            txtMemo.Text = TextLib.StringDecoder(dr["Memo"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["CONTRACT_TYPE"].ToString()))
                        {
                            rbContractType.SelectedValue = dr["CONTRACT_TYPE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["PAYMENT_TYPE"].ToString()))
                        {
                            ddlPaymentType.SelectedValue = dr["PAYMENT_TYPE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["M_S_PAY_DATE"].ToString()))
                        {
                            txtMSPayDate.Text = TextLib.MakeDateEightDigit(dr["M_S_PAY_DATE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["M_PAYCYCLE"].ToString()))
                        {
                            txtMPayCycle.Text = dr["M_PAYCYCLE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["M_PAYCYCLE_TYPE"].ToString()))
                        {
                            ddlMPaymentCycle.SelectedValue = TextLib.StringDecoder(dr["M_PAYCYCLE_TYPE"].ToString());

                        }
                        if (!string.IsNullOrEmpty(dr["R_PAYCYCLE_TYPE"].ToString()))
                        {
                            ddlRPaymentCycle.SelectedValue = dr["R_PAYCYCLE_TYPE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["R_S_PAY_DATE"].ToString()))
                        {
                            txtRSPayDate.Text = TextLib.MakeDateEightDigit(dr["R_S_PAY_DATE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["M_S_USING_DATE"].ToString()))
                        {
                            txtMSUsingDt.Text = TextLib.MakeDateEightDigit(dr["M_S_USING_DATE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["R_S_USING_DATE"].ToString()))
                        {
                            txtRSUsingDt.Text = TextLib.MakeDateEightDigit(dr["R_S_USING_DATE"].ToString());
                        }

                        //----------------------------------------------------------------------------------
                        if (!string.IsNullOrEmpty(dr["R_ISUE_DATE_TYPE"].ToString()))
                        {
                            ddlRIsueDateType.SelectedValue = dr["R_ISUE_DATE_TYPE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["M_ISUE_DATE_TYPE"].ToString()))
                        {
                            ddlMIsueDateType.SelectedValue = dr["M_ISUE_DATE_TYPE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["R_ISUE_DATE_ADJUST"].ToString()))
                        {
                            txtRAdjustDate.Text = dr["R_ISUE_DATE_ADJUST"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["M_ISUE_DATE_ADJUST"].ToString()))
                        {
                            txtMAdjustDate.Text = dr["M_ISUE_DATE_ADJUST"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["IS_SPECIAL"].ToString()))
                        {
                            chkSpecialContract.Checked = dr["IS_SPECIAL"].ToString() == "Y";
                        }

                        if (!string.IsNullOrEmpty(dr["M_E_USING_DATE"].ToString()))
                        {
                            txtSMEndDate.Text = TextLib.MakeDateEightDigit(dr["M_E_USING_DATE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["R_E_USING_DATE"].ToString()))
                        {
                            txtSREndDate.Text = TextLib.MakeDateEightDigit(dr["R_E_USING_DATE"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["REMARK"].ToString()))
                        {
                            txtRemark.Text = TextLib.StringDecoder(dr["REMARK"].ToString());
                        }

                    }
                }
            }
        }

        protected void ddlPodium_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPodium.SelectedValue.Equals(CommValue.CHOICE_VALUE_YES))
            {
                mvRentFee.ActiveViewIndex = CommValue.NUMBER_VALUE_1;
                txtHfPodium.Text = CommValue.CHOICE_VALUE_YES;
                txtTermMonth.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
            else if (ddlPodium.SelectedValue.Equals(CommValue.CHOICE_VALUE_NO))
            {
                mvRentFee.ActiveViewIndex = CommValue.NUMBER_VALUE_0;
                txtHfPodium.Text = CommValue.CHOICE_VALUE_NO;
                txtTermMonth.Enabled = CommValue.AUTH_VALUE_TRUE;
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            hfRentalFeeStartDt.Value = string.Empty;
            hfRentalFeeEndDt.Value = string.Empty;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void ddlPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPersonal.SelectedValue.Equals(CommValue.TENANTTY_VALUE_PERSONAL))
            {
                ltLandloadCorpCert.Text = TextNm["IDNO"];
                ltLandloadNm.Text = TextNm["CONTRACTOR"];

                txtLandloadRepNm.Text = string.Empty;
                txtLandloadRepNm.CssClass = "bgType3";
                txtLandloadRepNm.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtLandloadTaxCd.CssClass = "bgType3";
            }
            else
            {
                ltLandloadCorpCert.Text = TextNm["CERTINCORP"];
                ltLandloadNm.Text = TextNm["COMPNM"];

                txtLandloadRepNm.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtLandloadRepNm.CssClass = "bgType2";
                txtLandloadTaxCd.CssClass = "bgType2";
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value; 
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        /// <summary>
        /// 분양 및 임대 선택
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTerm.SelectedValue.Equals(CommValue.TERM_VALUE_LONGTERM))
            {
                // 분양자
                // 월간 임대비 Zero
                // 보증금 Zero
                txtSumDepositUSDNo.Text = string.Empty;
                txtSumDepositUSDNo.CssClass = "bgType1";
                txtSumDepositUSDNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtSumDepositVNDNo.Text = string.Empty;
                txtSumDepositVNDNo.CssClass = "bgType1";
                txtSumDepositVNDNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;


            }
            else
            {
                // 임대자
                // 총 임대비 Zero

                txtSumDepositUSDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtSumDepositUSDNo.CssClass = "bgType2";
                txtSumDepositVNDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtSumDepositVNDNo.CssClass = "bgType2";
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        /// <summary>
        /// 환율변경시 금액관련 사항 Reset시킴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnResetCurrency_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                txtSumDepositUSDNo.Text = string.Empty;
                txtSumDepositVNDNo.Text = string.Empty;


                txtExchangeRate.Text = hfExchangeRate.Value;
                txtHfExchangeRate.Text = hfExchangeRate.Value;

                txtIssueDt.Text = hfIssueDt.Value;
                txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
                txtRentAgreeDt.Text = hfRentAgreeDt.Value;
                txtRentStartDt.Text = hfRentStartDt.Value;
                txtRentEndDt.Text = hfRentEndDt.Value;
                //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
                txtHandOverDt.Text = hfHandOverDt.Value;
                txtInteriorStartDt.Text = hfInteriorStartDt.Value;
                txtInteriorEndDt.Text = hfInteriorEndDt.Value;
                txtConsDepositDt.Text = hfConsDepositDt.Value;
                txtConsRefundDt.Text = hfConsRefundDt.Value;
                hfRentalFeeStartDt.Value = string.Empty;
                hfRentalFeeEndDt.Value = string.Empty;
                txtDepositExpDt.Text = hfDepositExpDt.Value;
                txtDepositPayDt.Text = hfDepositPayDt.Value;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtSumRentVNDNo_TextChanged(object sender, EventArgs e)
        {


            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void txtSumRentUSDNo_TextChanged(object sender, EventArgs e)
        {


            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void txtSumDepositVNDNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSumDepositVNDNo.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            {
                // $에서 VND로 변환하는 부분
                txtSumDepositUSDNo.Text = ((double.Parse(txtSumDepositVNDNo.Text) / double.Parse(txtExchangeRate.Text))).ToString("##0.##");
            }
            else
            {
                txtSumDepositUSDNo.Text = string.Empty;
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void txtSumDepositUSDNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSumDepositUSDNo.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            {
                // $에서 VND로 변환하는 부분
                txtSumDepositVNDNo.Text = ((double.Parse(txtSumDepositUSDNo.Text) * double.Parse(txtExchangeRate.Text))).ToString();
            }
            else
            {
                txtSumDepositVNDNo.Text = string.Empty;
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void txtInitMMMngDay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // 초기관리비 일자 처리

                txtIssueDt.Text = hfIssueDt.Value;
                txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
                txtRentAgreeDt.Text = hfRentAgreeDt.Value;
                txtRentStartDt.Text = hfRentStartDt.Value;
                txtRentEndDt.Text = hfRentEndDt.Value;
                //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
                txtHandOverDt.Text = hfHandOverDt.Value;
                txtInteriorStartDt.Text = hfInteriorStartDt.Value;
                txtInteriorEndDt.Text = hfInteriorEndDt.Value;
                txtConsDepositDt.Text = hfConsDepositDt.Value;
                txtConsRefundDt.Text = hfConsRefundDt.Value;
                //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
                //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
                txtDepositExpDt.Text = hfDepositExpDt.Value;
                txtDepositPayDt.Text = hfDepositPayDt.Value;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtInitPerMMMngVND_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtInitPerMMMngVND.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            //{
            //    // $에서 VND로 변환하는 부분
            //    txtInitPerMMMngUSD.Text = ((double.Parse(txtInitPerMMMngVND.Text) / double.Parse(txtExchangeRate.Text))).ToString("##0.##");
            //}
            //else
            //{
            //    txtInitPerMMMngUSD.Text = string.Empty;
            //}

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void txtInitPerMMMngUSD_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtInitPerMMMngUSD.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            //{
            //    // $에서 VND로 변환하는 부분
            //    txtInitPerMMMngVND.Text = ((double.Parse(txtInitPerMMMngUSD.Text) * double.Parse(txtExchangeRate.Text))).ToString();
            //}
            //else
            //{
            //    txtInitPerMMMngVND.Text = string.Empty;
            //}

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void txtPerMMMngVND_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtPerMMMngVND.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            //{
            //    // $에서 VND로 변환하는 부분
            //    txtPerMMMngUSD.Text = ((double.Parse(txtPerMMMngVND.Text) / double.Parse(txtExchangeRate.Text))).ToString("##0.##");
            //}
            //else
            //{
            //    txtPerMMMngUSD.Text = string.Empty;
            //}

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void txtPerMMMngUSD_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtPerMMMngUSD.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            //{
            //    // $에서 VND로 변환하는 부분
            //    txtPerMMMngVND.Text = ((double.Parse(txtPerMMMngUSD.Text) * double.Parse(txtExchangeRate.Text))).ToString();
            //}
            //else
            //{
            //    txtPerMMMngVND.Text = string.Empty;
            //}

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
           // txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void txtRoomNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLeasingArea();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtFloor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLeasingAreaDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckLeasingArea()
        {
            if ((!string.IsNullOrEmpty(txtRoomNo.Text)) && (!string.IsNullOrEmpty(txtFloor.Text)))
            {

                // 호실정보 중복 조회
                // KN_USP_RES_SELECT_RENTINFO_S03
               var dtExistReturn = ContractMngBlo.WatchExistRentInfo(Int32.Parse(txtFloor.Text), txtRoomNo.Text);

                if (dtExistReturn != null)
                {
                    if (dtExistReturn.Rows.Count == 0)
                    {
                        string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "").Replace("/", "");

                        txtRentLeasingArea.Text = "";

                        // KN_USP_RES_SELECT_ROOMINFO_S05
                        var dtReturn = RoomMngBlo.SpreadRoomInfo(ddlContStep.SelectedValue, Int32.Parse(txtFloor.Text), txtRoomNo.Text, strNowDt);

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count > 0)
                            {
                                txtRentLeasingArea.Text = dtReturn.Rows[0]["LeasingArea"].ToString();
                            }
                        }
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('" + TextNm["UNITNO"] + " : " + txtRoomNo.Text + "\\n" + TextNm["FLOOR"] + " : " + txtFloor.Text + "\\n" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningExistRoomNo", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                        txtFloor.Text = string.Empty;
                        txtRoomNo.Text = string.Empty;
                    }
                }
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
           // txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
        }

        protected void CheckDdlLeasingArea()
        {


            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
        }
        
        protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strRentFeeTmpSeq = txtHfRentFeeTmpSeq.Text;
                string strRentFeeSeq = txtHfRentFeeSeq.Text;
               // string strRentalFeeExpAmt = txtRentalFeeExpAmt.Text;
               // string strRentalFeeExcRate = txtRentalFeeExcRate.Text;

                double dblRentalFeeExpAmt = CommValue.NUMBER_VALUE_0_0;
                double dblRentalFeeExcRate = CommValue.NUMBER_VALUE_0;

                int intRentFeeTmpSeq = CommValue.NUMBER_VALUE_0;
                int intRentFeeSeq = CommValue.NUMBER_VALUE_0;

                if (!string.IsNullOrEmpty(strRentFeeTmpSeq))
                {
                    intRentFeeTmpSeq = Int32.Parse(strRentFeeTmpSeq);
                }

                if (!string.IsNullOrEmpty(strRentFeeSeq))
                {
                    intRentFeeSeq = Int32.Parse(strRentFeeSeq);
                }

                //if (!string.IsNullOrEmpty(strRentalFeeExpAmt))
                //{
                //    dblRentalFeeExpAmt = double.Parse(strRentalFeeExpAmt);
                //}

                //if (!string.IsNullOrEmpty(strRentalFeeExcRate))
                //{
                //    dblRentalFeeExcRate = double.Parse(strRentalFeeExcRate);
                //}

                // 오피스 / 리테일 임대료 임시 저장
                // KN_USP_RES_INSERT_TEMPRENTFEEINFO_S00
                DataTable dtTmpRentalFee = ContractMngBlo.RegistryTempRentFeeInfo(txtHfRentCd.Text, intRentFeeTmpSeq, intRentFeeSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), hfRentalFeeStartDt.Value.Replace("-", ""),
                                                                                  hfRentalFeeEndDt.Value.Replace("-", ""), dblRentalFeeExcRate, dblRentalFeeExpAmt);

                if (dtTmpRentalFee != null)
                {
                    if (dtTmpRentalFee.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        txtHfRentFeeTmpSeq.Text = dtTmpRentalFee.Rows[0]["RentFeeTmpSeq"].ToString();
                        txtHfRentFeeSeq.Text = dtTmpRentalFee.Rows[0]["RentFeeSeq"].ToString();
                    }
                }

                // 오피스 / 리테일 임대료 임시 데이터 조회
                // KN_USP_RES_SELECT_TEMPRENTFEEINFO_S00
                DataTable dtRentalFee = ContractMngBlo.SpreadTempRentFeeInfo(Int32.Parse(txtHfRentFeeTmpSeq.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                if (dtRentalFee != null)
                {
                   // lvRentalFeeList.DataSource = dtRentalFee;
                    //lvRentalFeeList.DataBind();
                }

               // txtRentalFeeStartDt.Text = string.Empty;
                hfRentalFeeStartDt.Value = string.Empty;
               // txtRentalFeeEndDt.Text = string.Empty;
                hfRentalFeeEndDt.Value = string.Empty;
               // txtRentalFeeExcRate.Text = string.Empty;
                //txtRentalFeeExpAmt.Text = string.Empty;

                txtIssueDt.Text = hfIssueDt.Value;
                txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
                txtRentAgreeDt.Text = hfRentAgreeDt.Value;
                txtRentStartDt.Text = hfRentStartDt.Value;
                txtRentEndDt.Text = hfRentEndDt.Value;
               // txtInitMMMngDt.Text = hfInitMMMngDt.Value;
                txtHandOverDt.Text = hfHandOverDt.Value;
                txtInteriorStartDt.Text = hfInteriorStartDt.Value;
                txtInteriorEndDt.Text = hfInteriorEndDt.Value;
                txtConsDepositDt.Text = hfConsDepositDt.Value;
                txtConsRefundDt.Text = hfConsRefundDt.Value;
               //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
                //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
                txtDepositExpDt.Text = hfDepositExpDt.Value;
                txtDepositPayDt.Text = hfDepositPayDt.Value;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnRegist_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strDepositTmpSeq = txtHfDepositTmpSeq.Text;
                string strDepositSeq = txtHfDepositSeq.Text;
                string strDepositExpAmt = txtDepositExpAmt.Text;
                string strDepositPayAmt = txtDepositPayAmt.Text;
                string strDepositExRate = txtDepositExcRate.Text;

                double dblDepositExpAmt = CommValue.NUMBER_VALUE_0_0;
                double dblDepositPayAmt = CommValue.NUMBER_VALUE_0_0;
                double dblDepositExRate = CommValue.NUMBER_VALUE_0;

                int intDepositTmpSeq = CommValue.NUMBER_VALUE_0;
                int intDepositSeq = CommValue.NUMBER_VALUE_0;

                if (!string.IsNullOrEmpty(strDepositTmpSeq))
                {
                    intDepositTmpSeq = Int32.Parse(strDepositTmpSeq);
                }

                if (!string.IsNullOrEmpty(strDepositSeq))
                {
                    intDepositSeq = Int32.Parse(strDepositSeq);
                }

                if (!string.IsNullOrEmpty(strDepositExpAmt))
                {
                    dblDepositExpAmt = double.Parse(strDepositExpAmt);
                }

                if (!string.IsNullOrEmpty(strDepositExRate))
                {
                    dblDepositExRate = double.Parse(strDepositExRate);
                }

                if (!string.IsNullOrEmpty(strDepositPayAmt))
                {
                    dblDepositPayAmt = double.Parse(strDepositPayAmt);
                }

                // 선수금 데이터 등록
                // KN_USP_RES_INSERT_TEMPRENTDEPOSITINFO_S00
                DataTable dtTmpDeposit = ContractMngBlo.RegistryTempRentDepositInfo(txtHfRentCd.Text, intDepositTmpSeq, intDepositSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), hfDepositExpDt.Value.Replace("-", ""), dblDepositExpAmt, dblDepositExRate,
                                                                                   hfDepositPayDt.Value.Replace("-", ""), dblDepositPayAmt);

                if (dtTmpDeposit != null)
                {
                    if (dtTmpDeposit.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        txtHfDepositTmpSeq.Text = dtTmpDeposit.Rows[0]["DepositTempSeq"].ToString();
                        txtHfDepositSeq.Text = dtTmpDeposit.Rows[0]["DepositSeq"].ToString();
                    }
                }

                // 선수금 임시 데이터 조회
                // KN_USP_RES_SELECT_TEMPRENTDEPOSITINFO_S00
                DataTable dtDeposit = ContractMngBlo.SpreadTempRentDepositInfo(Int32.Parse(txtHfDepositTmpSeq.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                if (dtDeposit != null)
                {
                    lvDepositList.DataSource = dtDeposit;
                    lvDepositList.DataBind();
                }

                txtDepositExpDt.Text = string.Empty;
                hfDepositExpDt.Value = string.Empty;
                txtDepositExpAmt.Text = string.Empty;
                txtDepositExcRate.Text = string.Empty;
                txtDepositPayDt.Text = string.Empty;
                hfDepositPayDt.Value = string.Empty;
                txtDepositPayAmt.Text = string.Empty;

                txtIssueDt.Text = hfIssueDt.Value;
                txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
                txtRentAgreeDt.Text = hfRentAgreeDt.Value;
                txtRentStartDt.Text = hfRentStartDt.Value;
                txtRentEndDt.Text = hfRentEndDt.Value;
                //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
                txtHandOverDt.Text = hfHandOverDt.Value;
                txtInteriorStartDt.Text = hfInteriorStartDt.Value;
                txtInteriorEndDt.Text = hfInteriorEndDt.Value;
                txtConsDepositDt.Text = hfConsDepositDt.Value;
                txtConsRefundDt.Text = hfConsRefundDt.Value;
                //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
               // txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
                txtDepositExpDt.Text = hfDepositExpDt.Value;
                txtDepositPayDt.Text = hfDepositPayDt.Value;
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
        protected void lvDepositList_ItemCreated(object sender, ListViewItemEventArgs e)
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
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvDepositList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
           
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                //if (!string.IsNullOrEmpty(drView["DepositTmpSeq"].ToString()))
                //{
                //    ((TextBox)iTem.FindControl("txtDepositTmpSeq")).Text = drView["DepositTmpSeq"].ToString();
                //    txtHfDepositTmpSeq.Text = drView["DepositTmpSeq"].ToString();
                //}

                if (!string.IsNullOrEmpty(drView["DepositSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtDepositSeq")).Text = drView["DepositSeq"].ToString();
                    txtHfDepositSeq.Text = drView["DepositSeq"].ToString();
                }

                TextBox txtDepositExpDt = ((TextBox)iTem.FindControl("txtDepositExpDt"));
                HiddenField hfDepositExpDt = ((HiddenField)iTem.FindControl("hfDepositExpDt"));

                if (!string.IsNullOrEmpty(drView["DepositExpDt"].ToString()))
                {
                    txtDepositExpDt.Text = TextLib.MakeDateEightDigit(drView["DepositExpDt"].ToString());
                    hfDepositExpDt.Value = drView["DepositExpDt"].ToString();
                }

                TextBox txtDepositExpAmt = ((TextBox)iTem.FindControl("txtDepositExpAmt"));

                if (!string.IsNullOrEmpty(drView["DepositExpAmt"].ToString()))
                {
                    txtDepositExpAmt.Text = drView["DepositExpAmt"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DepositExcRate"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtDepositExcRate")).Text = drView["DepositExcRate"].ToString();
                }

                TextBox txtDepositPayDt = ((TextBox)iTem.FindControl("txtDepositPayDt"));
                HiddenField hfDepositPayDt = ((HiddenField)iTem.FindControl("hfDepositPayDt"));

                if (!string.IsNullOrEmpty(drView["DepositPayDt"].ToString()))
                {
                    txtDepositPayDt.Text = TextLib.MakeDateEightDigit(drView["DepositPayDt"].ToString());
                    hfDepositPayDt.Value = drView["DepositPayDt"].ToString();
                }

                TextBox txtDepositPayAmt = ((TextBox)iTem.FindControl("txtDepositPayAmt"));

                if (!string.IsNullOrEmpty(drView["DepositPayAmt"].ToString()))
                {
                    txtDepositPayAmt.Text = drView["DepositPayAmt"].ToString();
                }

                ((Literal)iTem.FindControl("ltDepositExpAmtUnit")).Text = TextNm["DONG"];
                ((Literal)iTem.FindControl("ltDepositExcRateUnit")).Text = TextNm["DONG"];
                ((Literal)iTem.FindControl("ltDepositPayAmtUnit")).Text = TextNm["DONG"];

                ((Literal)iTem.FindControl("ltExpCalendar")).Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"ContCalendar(this, '" + txtDepositExpDt.ClientID + "','" + hfDepositExpDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";
                ((Literal)iTem.FindControl("ltPayCalendar")).Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"ContCalendar(this, '" + txtDepositPayDt.ClientID + "','" + hfDepositPayDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");

                imgbtnModify.OnClientClick = "javascript:return fnCheckItems('" + txtDepositExpDt.ClientID + "','" + hfDepositExpDt.ClientID + "','" + txtDepositExpAmt.ClientID + "','" + txtDepositPayDt.ClientID + "','" + hfDepositPayDt.ClientID + "','" + txtDepositPayAmt.ClientID + "','" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            }
        }

        protected void lvDepositList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strRentCd = txtHfRentCd.Text;
                string strDepositTmpSeq = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositTmpSeq")).Text;
                string strDepositSeq = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositSeq")).Text;
                string strDepositExpDt = ((HiddenField)lvDepositList.Items[e.ItemIndex].FindControl("hfDepositExpDt")).Value.Replace("-", "");
                string strDepositExpAmt = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositExpAmt")).Text;
                string strDepositExcRate = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositExcRate")).Text;
                string strDepositPayDt = ((HiddenField)lvDepositList.Items[e.ItemIndex].FindControl("hfDepositPayDt")).Value.Replace("-", "");
                string strDepositPayAmt = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositPayAmt")).Text;

                double dblDepositExpAmt = CommValue.NUMBER_VALUE_0_0;
                double dblDepositExcRate = CommValue.NUMBER_VALUE_0_0;
                double dblDepositPayAmt = CommValue.NUMBER_VALUE_0_0;

                if (!string.IsNullOrEmpty(strDepositExpAmt))
                {
                    dblDepositExpAmt = double.Parse(strDepositExpAmt);
                }

                if (!string.IsNullOrEmpty(strDepositPayAmt))
                {
                    dblDepositExcRate = double.Parse(strDepositExcRate);
                }

                if (!string.IsNullOrEmpty(strDepositPayAmt))
                {
                    dblDepositPayAmt = double.Parse(strDepositPayAmt);
                }

                // 선수금 임시 데이터 수정
                // KN_USP_RES_UPDATE_TEMPRENTDEPOSITINFO_M00
                ContractMngBlo.ModifyTempRentDepositInfo(strRentCd, Int32.Parse(strDepositTmpSeq), Int32.Parse(strDepositSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strDepositExpDt, dblDepositExpAmt, dblDepositExcRate,
                                                         strDepositPayDt, dblDepositPayAmt);

                // 선수금 임시 데이터 조회
                // KN_USP_RES_SELECT_TEMPRENTDEPOSITINFO_S00
                DataTable dtDeposit = ContractMngBlo.SpreadTempRentDepositInfo(Int32.Parse(txtHfDepositTmpSeq.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                if (dtDeposit != null)
                {
                    lvDepositList.DataSource = dtDeposit;
                    lvDepositList.DataBind();
                }

                StringBuilder sbWarning = new StringBuilder();
                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
                sbWarning.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Modify", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvDepositList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strDepositTmpSeq = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositTmpSeq")).Text;
                string strDepositSeq = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositSeq")).Text;

                // 선수금 임시 데이터 삭제
                // KN_USP_RES_DELETE_TEMPRENTDEPOSITINFO_M00
                ContractMngBlo.RemoveTempRentDepositInfo(Int32.Parse(strDepositTmpSeq), Int32.Parse(strDepositSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                // 선수금 임시 데이터 조회
                // KN_USP_RES_SELECT_TEMPRENTDEPOSITINFO_S00
                DataTable dtDeposit = ContractMngBlo.SpreadTempRentDepositInfo(Int32.Parse(txtHfDepositTmpSeq.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                if (dtDeposit != null)
                {
                    lvDepositList.DataSource = dtDeposit;
                    lvDepositList.DataBind();
                }

                StringBuilder sbWarning = new StringBuilder();
                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_DELETE_ISSUE"]);
                sbWarning.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Modify", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
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
        protected void lvRentalFeeList_ItemCreated(object sender, ListViewItemEventArgs e)
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
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRentalFeeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                TextBox txtRentalFeeStartDt = ((TextBox)iTem.FindControl("txtRentalFeeStartDt"));
                HiddenField hfRentalFeeStartDt = ((HiddenField)iTem.FindControl("hfRentalFeeStartDt"));
                TextBox txtRentalFeeEndDt = ((TextBox)iTem.FindControl("txtRentalFeeEndDt"));
                HiddenField hfRentalFeeEndDt = ((HiddenField)iTem.FindControl("hfRentalFeeEndDt"));
                TextBox txtRentalFeeExcRate = ((TextBox)iTem.FindControl("txtRentalFeeExcRate"));
                TextBox txtRentalFeeExpAmt = ((TextBox)iTem.FindControl("txtRentalFeeExpAmt"));

                if (!string.IsNullOrEmpty(drView["RentFeeTmpSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtRentFeeTmpSeq")).Text = drView["RentFeeTmpSeq"].ToString();
                    txtHfRentFeeTmpSeq.Text = drView["RentFeeTmpSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentFeeSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtRentFeeSeq")).Text = drView["RentFeeSeq"].ToString();
                    txtHfRentFeeSeq.Text = drView["RentFeeSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentFeeStartDt"].ToString()))
                {
                    txtRentalFeeStartDt.Text = TextLib.MakeDateEightDigit(drView["RentFeeStartDt"].ToString());
                    hfRentalFeeStartDt.Value = drView["RentFeeStartDt"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentFeeEndDt"].ToString()))
                {
                    txtRentalFeeEndDt.Text = TextLib.MakeDateEightDigit(drView["RentFeeEndDt"].ToString());
                    hfRentalFeeEndDt.Value = drView["RentFeeEndDt"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentFeeExcRate"].ToString()))
                {
                    txtRentalFeeExcRate.Text = drView["RentFeeExcRate"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentFeePayAmt"].ToString()))
                {
                    txtRentalFeeExpAmt.Text = drView["RentFeePayAmt"].ToString();
                }

                ((Literal)iTem.FindControl("ltRentalFeeExcRateUnit")).Text = TextNm["DONG"];
                ((Literal)iTem.FindControl("ltRentalFeeAmtUnit")).Text = TextNm["DOLLAR"];

                ((Literal)iTem.FindControl("ltRentStartCalendar")).Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"ContCalendar(this, '" + txtRentalFeeStartDt.ClientID + "','" + hfRentalFeeStartDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";
                ((Literal)iTem.FindControl("ltRentEndCalendar")).Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"ContCalendar(this, '" + txtRentalFeeEndDt.ClientID + "','" + hfRentalFeeEndDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");

                imgbtnModify.OnClientClick = "javascript:return fnCheckRentalFee('" + txtRentalFeeStartDt.ClientID + "','" + hfRentalFeeStartDt.ClientID + "','" + txtRentalFeeEndDt.ClientID + "','" + hfRentalFeeEndDt.ClientID + "','" + txtRentalFeeExpAmt.ClientID + "','" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            }
        }

        protected void lvRentalFeeList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            //try
            //{
            //    // 세션체크
            //    AuthCheckLib.CheckSession();

            //    string strRentCd = txtHfRentCd.Text;

            //    string strRentFeeTmpSeq = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentFeeTmpSeq")).Text;
            //    string strRentFeeSeq = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentFeeSeq")).Text;
            //    string strRentalFeeStartDt = ((HiddenField)lvRentalFeeList.Items[e.ItemIndex].FindControl("hfRentalFeeStartDt")).Value.Replace("-", "");
            //    string strRentalFeeEndDt = ((HiddenField)lvRentalFeeList.Items[e.ItemIndex].FindControl("hfRentalFeeEndDt")).Value.Replace("-", "");
            //    string strRentalFeeExcRate = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentalFeeExcRate")).Text;
            //    string strRentalFeeExpAmt = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentalFeeExpAmt")).Text;

            //    double dblRentalFeeExcRate = CommValue.NUMBER_VALUE_0_0;
            //    double dblRentalFeeExpAmt = CommValue.NUMBER_VALUE_0_0;

            //    if (!string.IsNullOrEmpty(strRentalFeeExcRate))
            //    {
            //        dblRentalFeeExcRate = double.Parse(strRentalFeeExcRate);
            //    }

            //    if (!string.IsNullOrEmpty(strRentalFeeExpAmt))
            //    {
            //        dblRentalFeeExpAmt = double.Parse(strRentalFeeExpAmt);
            //    }

            //    // 오피스 / 리테일 임대료 임시 데이터 수정
            //    // KN_USP_RES_UPDATE_TEMPRENTFEEINFO_M00
            //    ContractMngBlo.ModifyTempRentFeeInfo(strRentCd, Int32.Parse(strRentFeeTmpSeq), Int32.Parse(strRentFeeSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strRentalFeeStartDt, strRentalFeeEndDt,
            //                                         dblRentalFeeExcRate, dblRentalFeeExpAmt);

            //    // 오피스 / 리테일 임대료 임시 데이터 조회
            //    // KN_USP_RES_SELECT_TEMPRENTFEEINFO_S00
            //    DataTable dtRentalFee = ContractMngBlo.SpreadTempRentFeeInfo(Int32.Parse(strRentFeeTmpSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString());

            //    if (dtRentalFee != null)
            //    {
            //        lvRentalFeeList.DataSource = dtRentalFee;
            //        lvRentalFeeList.DataBind();
            //    }

            //    StringBuilder sbWarning = new StringBuilder();
            //    sbWarning.Append("alert('");
            //    sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
            //    sbWarning.Append("');");

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Modify", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            //}
            //catch (Exception ex)
            //{
            //    ErrLogger.MakeLogger(ex);
            //}
        }

        protected void lvRentalFeeList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            //try
            //{
            //    // 세션체크
            //    AuthCheckLib.CheckSession();

            //    string strRentFeeTmpSeq = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentFeeTmpSeq")).Text;
            //    string strRentFeeSeq = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentFeeSeq")).Text;

            //    // 오피스 / 리테일 임대료 임시 데이터 삭제
            //    // KN_USP_RES_DELETE_TEMPRENTFEEINFO_M00
            //    ContractMngBlo.RemoveTempRentFeeInfo(Int32.Parse(strRentFeeTmpSeq), Int32.Parse(strRentFeeSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString());

            //    // 오피스 / 리테일 임대료 임시 데이터 조회
            //    // KN_USP_RES_SELECT_TEMPRENTFEEINFO_S00
            //    DataTable dtRentalFee = ContractMngBlo.SpreadTempRentFeeInfo(Int32.Parse(txtHfRentFeeTmpSeq.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString());

            //    if (dtRentalFee != null)
            //    {
            //        lvRentalFeeList.DataSource = dtRentalFee;
            //        lvRentalFeeList.DataBind();
            //    }

            //    StringBuilder sbWarning = new StringBuilder();
            //    sbWarning.Append("alert('");
            //    sbWarning.Append(AlertNm["INFO_DELETE_ISSUE"]);
            //    sbWarning.Append("');");

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Modify", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            //}
            //catch (Exception ex)
            //{
            //    ErrLogger.MakeLogger(ex);
            //}
        }

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                int intRentFeeTmpSeq = CommValue.NUMBER_VALUE_0;
                int intDepositTmpSeq = CommValue.NUMBER_VALUE_0;

                // 계약자정보
                riDs.PodiumYn = txtHfPodium.Text;
                riDs.RentCd = txtHfParamRentCd.Text;
                riDs.RentSeq = Int32.Parse(txtHfParamRentSeq.Text);
                riDs.InsKNMemNo = txtInchage.Text;
                riDs.PersonalCd = ddlPersonal.SelectedValue;

                if (ddlPersonal.SelectedValue.Equals(CommValue.TENANTTY_VALUE_CORPORATION))
                {
                    riDs.LandloadCompNm = txtLandloadNm.Text;
                    riDs.LandloadNm = string.Empty;
                }
                else if (ddlPersonal.SelectedValue.Equals(CommValue.TENANTTY_VALUE_PERSONAL))
                {
                    riDs.LandloadCompNm = string.Empty;
                    riDs.LandloadNm = txtLandloadNm.Text;
                }

                riDs.ContStepCd = ddlContStep.SelectedValue;
                riDs.ContractNo = txtContNo.Text;
                riDs.LandloadAddr = txtLandloadAddr.Text;
                riDs.LandloadDetAddr = txtLandloadDetAddr.Text;
                riDs.LandloadCorpCert = txtLandloadCorpCert.Text;
                riDs.LandloadIssuedDt = hfIssueDt.Value.Replace("-", "");
                riDs.LandloadTelTyCd = txtLandloadTelFrontNo.Text;
                riDs.LandloadTelFrontNo = txtLandloadTelMidNo.Text;
                riDs.LandloadTelRearNo = txtLandloadTelRearNo.Text;
                riDs.LandloadMobileTyCd = txtLandloadMobileFrontNo.Text;
                riDs.LandloadMobileFrontNo = txtLandloadMobileMidNo.Text;
                riDs.LandloadMobileRearNo = txtLandloadMobileRearNo.Text;
                riDs.LandloadFaxTyCd = txtLandloadFAXFrontNo.Text;
                riDs.LandloadFaxFrontNo = txtLandloadFAXMidNo.Text;
                riDs.LandloadFaxRearNo = txtLandloadFAXRearNo.Text;
                riDs.EmailID = txtLandloadEmailID.Text;
                riDs.EmailServer = txtLandloadEmailServer.Text;
                riDs.LandloadRepNm = txtLandloadRepNm.Text;
                riDs.LandloadTaxCd = txtLandloadTaxCd.Text;
                riDs.RentAddr = txtRentAddr.Text;
                riDs.RentDetAddr = txtRentDetAddr.Text;
                riDs.CURNCY_TYPE = hfCCtype.Value;
                riDs.INFLATION_RATE = double.Parse(txtFloation.Text);
                riDs.APPL_YN = hfApplyFeeMn.Value;
                if (riDs.CURNCY_TYPE != "CC")
                {
                    riDs.FIXED_DONGTODOLLAR = double.Parse(txtExchangeRate.Text);
                }
                riDs.ListFitFee = hfListFitFeeMng.Value;
                riDs.ListMngFee = hfListFeeMng.Value;
                riDs.ListRentFee = hfListRentFee.Value;

                // 계약 기간
                riDs.OTLAgreeDt = hfOTLAgreeDt.Value.Replace("-", "");
                riDs.RentAgreeDt = hfRentAgreeDt.Value.Replace("-", "");

                if (!string.IsNullOrEmpty(txtFreeRentMonth.Text))
                {
                    riDs.FreeRentMonth = Int32.Parse(txtFreeRentMonth.Text);
                }
                else
                {
                    riDs.FreeRentMonth = CommValue.NUMBER_VALUE_0;
                }

                riDs.RentStartDt = hfRentStartDt.Value.Replace("-", "");
                riDs.RentEndDt = hfRentEndDt.Value.Replace("-", "");

                if (string.IsNullOrEmpty(txtTermMonth.Text))
                {
                    riDs.TermMonth = CommValue.NUMBER_VALUE_0;
                }
                else
                {
                    riDs.TermMonth = Int32.Parse(txtTermMonth.Text);
                }

                riDs.HandOverDt = hfHandOverDt.Value.Replace("-", "");

                // 방정보
                string strRoomNo = string.Empty;

                //strRoomNo = ddlFirst.SelectedValue + ddlSecond.SelectedValue + ddlThird.SelectedValue + ddlFouth.SelectedValue + ddlFifth.SelectedValue;

                //if (!string.IsNullOrEmpty(ddlSixth.SelectedValue))
                //{
                //    strRoomNo = strRoomNo + ddlSixth.SelectedValue + ddlSeventh.SelectedValue + ddlEighth.SelectedValue;
                //}

                riDs.RoomNo = txtRoomNo.Text.ToUpper();//strRoomNo.ToUpper();
                riDs.ExtRoomNo = txtExtRoomNo.Text.ToUpper();
                riDs.FloorNo = Int32.Parse(txtFloor.Text);
                riDs.RentLeasingArea = double.Parse(txtRentLeasingArea.Text);
                riDs.CONTRACT_TYPE = rbContractType.SelectedValue;
                riDs.PAYMENT_TYPE = ddlPaymentType.SelectedValue;
                riDs.CPI = double.Parse(txtCPI.Text);
                riDs.R_PAYCYCLE = int.Parse(string.IsNullOrEmpty(txtPayTermMonth.Text)?"0":txtPayTermMonth.Text);
                riDs.R_PAYCYCLE_TYPE = ddlRPaymentCycle.SelectedValue;
                riDs.R_S_PAY_DATE = txtRSPayDate.Text.Replace("-", "");
                riDs.M_PAYCYCLE = int.Parse(string.IsNullOrEmpty(txtMPayCycle.Text) ? "0" : txtMPayCycle.Text);
                riDs.M_PAYCYCLE_TYPE = ddlMPaymentCycle.SelectedValue;
                riDs.M_S_PAY_DATE = txtMSPayDate.Text.Replace("-", "");
                riDs.M_S_USING_DATE = txtMSUsingDt.Text.Replace("-", "");
                riDs.R_S_USING_DATE = txtRSUsingDt.Text.Replace("-", "");
                riDs.R_ISUE_DATE_TYPE = ddlRIsueDateType.SelectedValue;
                riDs.M_ISUE_DATE_TYPE = ddlMIsueDateType.SelectedValue;
                riDs.R_ISUE_DATE_ADJUST = int.Parse(string.IsNullOrEmpty(txtRAdjustDate.Text) ? "0" : txtRAdjustDate.Text);
                riDs.M_ISUE_DATE_ADJUST = int.Parse(string.IsNullOrEmpty(txtMAdjustDate.Text) ? "0" : txtMAdjustDate.Text);
                riDs.IS_SPECIAL = chkSpecialContract.Checked ? "Y" : "N";

                if (ddlIndustry.SelectedValue.ToString() == CommValue.CODE_VALUE_EMPTY)
                {
                    riDs.IndustryCd = "";
                }
                else
                {
                    riDs.IndustryCd = ddlIndustry.SelectedValue;
                }

                if (ddlNat.SelectedValue.ToString() == CommValue.CODE_VALUE_EMPTY)
                {
                    riDs.NatCd = "";
                }
                else
                {
                    riDs.NatCd = ddlNat.SelectedValue;
                }
                               
                riDs.RenewDt = txtRenewDt.Text.Replace("-", "");

                // 임대비용
                if (!string.IsNullOrEmpty(hfExchangeRate.Value))
                {
                    riDs.DongToDollar = double.Parse(hfExchangeRate.Value);
                }
                else
                {
                    riDs.DongToDollar = CommValue.NUMBER_VALUE_0;
                }

                riDs.PayStartYYYYMM = txtPayStartYYYYMM.Text;

                if (txtHfPodium.Text.Equals(CommValue.CHOICE_VALUE_YES))
                {
                    riDs.PayTermMonth = CommValue.NUMBER_VALUE_0;
                }
                else
                {
                    if (string.IsNullOrEmpty(txtTermMonth.Text))
                    {
                        riDs.PayTermMonth = CommValue.NUMBER_VALUE_0;
                    }
                    else
                    {
                        riDs.PayTermMonth = Int32.Parse(txtPayTermMonth.Text);
                    }
                }

                if (string.IsNullOrEmpty(txtPayDay.Text))
                {
                    riDs.PayDay = CommValue.NUMBER_VALUE_0;
                }
                else
                {
                    riDs.PayDay = Int32.Parse(txtPayDay.Text);
                }

                //if (string.IsNullOrEmpty(txtSumRentVNDNo.Text))
                //{
                //    riDs.TotViAmtNo = CommValue.NUMBER_VALUE_0;
                //}
                //else
                //{
                //    riDs.TotViAmtNo = double.Parse(txtSumRentVNDNo.Text);
                //}

                //if (string.IsNullOrEmpty(txtSumRentUSDNo.Text))
                //{
                //    riDs.TotEnAmtNo = CommValue.NUMBER_VALUE_0;
                //}
                //else
                //{
                //    riDs.TotEnAmtNo = double.Parse(txtSumRentUSDNo.Text);
                //}
                riDs.M_E_USING_DATE = txtSMEndDate.Text.Replace("-", "");
                riDs.R_E_USING_DATE = txtSREndDate.Text.Replace("-", "");

                if (txtHfPodium.Text.Equals(CommValue.CHOICE_VALUE_YES))
                {
                    riDs.MinimumRentFee = double.Parse(txtMinimumIncome.Text);
                    riDs.ApplyRate = double.Parse(txtApplyRate.Text);
                }

                // 임대료는 DB에서 테이블 간 처리
                if (!string.IsNullOrEmpty(txtHfRentFeeTmpSeq.Text))
                {
                    intRentFeeTmpSeq = Int32.Parse(txtHfRentFeeTmpSeq.Text);
                }

                // 보증금처리
                if (string.IsNullOrEmpty(txtSumDepositVNDNo.Text))
                {
                    riDs.DepositViAmtNo = CommValue.NUMBER_VALUE_0_0;
                }
                else
                {
                    riDs.DepositViAmtNo = double.Parse(txtSumDepositVNDNo.Text);
                }

                if (string.IsNullOrEmpty(txtSumDepositUSDNo.Text))
                {
                    riDs.DepositEnAmtNo = CommValue.NUMBER_VALUE_0_0;
                }
                else
                {
                    riDs.DepositEnAmtNo = double.Parse(txtSumDepositUSDNo.Text);
                }

                // 보증금 분할 납부는 DB에서 테이블 간 처리
                if (!string.IsNullOrEmpty(txtHfDepositTmpSeq.Text))
                {
                    intDepositTmpSeq = Int32.Parse(txtHfDepositTmpSeq.Text);
                }

                // 인테리어
                riDs.InteriorStartDt = hfInteriorStartDt.Value.Replace("-", "");
                riDs.InteriorEndDt = hfInteriorEndDt.Value.Replace("-", "");

                if (string.IsNullOrEmpty(txtConsDeposit.Text))
                {
                    riDs.ConsDeposit = CommValue.NUMBER_VALUE_0_0;
                }
                else
                {
                    riDs.ConsDeposit = double.Parse(txtConsDeposit.Text);
                }

                riDs.ConsDepositDt = hfConsDepositDt.Value.Replace("-", "");

                if (string.IsNullOrEmpty(txtConsRefund.Text))
                {
                    riDs.ConsRefund = CommValue.NUMBER_VALUE_0_0;
                }
                else
                {
                    riDs.ConsRefund = double.Parse(txtConsRefund.Text);
                }

                riDs.ConsRefundDt = hfConsRefundDt.Value.Replace("-", "");
                riDs.DifferenceReason = txtDifferenceReason.Text;

                riDs.Memo = txtMemo.Text;
                riDs.InsMemIP = Request.ServerVariables["REMOTE_ADDR"];
                riDs.InsCompNo = Session["CompCd"].ToString();
                riDs.InsMemNo = Session["MemNo"].ToString();
                riDs.InsKNMemNo = txtInchage.Text;
                riDs.SaveYn = CommValue.CHOICE_VALUE_YES;
                riDs.REMARK = txtRemark.Text;

                // 리테일 및 오피스 계약정보 수정
                // KN_USP_RES_UPDATE_RENTINFO_M00
                // KN_USP_RES_DELETE_RENTFEEINFO_M01
                // KN_USP_RES_INSERT_RENTFEEINFO_M00
                // KN_USP_RES_DELETE_TEMPRENTFEEINFO_M02
                // KN_USP_RES_DELETE_RENTDEPOSITINFO_M01
                // KN_USP_RES_INSERT_RENTDEPOSITINFO_M00
                // KN_USP_RES_DELETE_TEMPRENTDEPOSITINFO_M02
                object[] objReturn = ContractMngBlo.ModifyRentMngInfo(riDs, intRentFeeTmpSeq, intDepositTmpSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                //quynhld modify
                if (fileUpl.HasFile)
                {
                    try
                    {
                        string conLogconnString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
                        SqlConnection conn = new SqlConnection(conLogconnString);
                        string strCmd = string.Format(@"INSERT INTO ContractPDF ([RentCD],[RentSeq],[FileName],[FilePath],[Status]) VALUES('{0}','{1}','{2}','{3}','0')"
                            , riDs.RentCd, riDs.RentSeq, riDs.ContractNo + ".PDF", "//ContractPDF//" + riDs.ContractNo + ".PDF");

                        SqlCommand cmd = new SqlCommand(strCmd, conn);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        cmd.ExecuteNonQuery();
                        string pdfFile = Path.GetFileName(riDs.ContractNo + ".PDF");

                        fileUpl.SaveAs(Server.MapPath("~//ContractPDF//") + pdfFile);

                        //mo connection insert vao bang pdf contract

                    }
                    catch (Exception ex)
                    {
                        string strErr = ex.Message;
                    }
                }
                //quynhld end modify
                if (objReturn != null)
                {
                    string strAlert = string.Format(AlertNm["INFO_MODIFY_CONT"], riDs.ContractNo);

                    StringBuilder sbAlert = new StringBuilder();

                    sbAlert.Append("alert('");
                    sbAlert.Append(strAlert);
                    sbAlert.Append("');");
                    sbAlert.Append("document.location.href=\"" + Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "\";");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbAlert.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnStartCheck_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(hfRentStartDt.Value))
            {
                txtPayStartYYYYMM.Text = hfRentStartDt.Value.Replace("-", "").Substring(0, 6);
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
           // txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void imgbtnEndCheck_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtGapReturn = new DataTable();

            if (!string.IsNullOrEmpty(hfRentStartDt.Value) && !string.IsNullOrEmpty(hfRentEndDt.Value))
            {
                string strStartDt = hfRentStartDt.Value.Replace("-", "");
                string strEndDt = hfRentEndDt.Value.Replace("-", "");

                // KN_USP_COMM_SELECT_COMPAREMONTH_S00
                string strGap = ChkFunctionUtil.MakeCheckDate(strStartDt, strEndDt);

                txtTermMonth.Text = strGap;
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
            //txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        //protected void imgbtnInitDay_Click(object sender, ImageClickEventArgs e)
        //{
        //    DataTable dtGapReturn = new DataTable();

        //    if (!string.IsNullOrEmpty(hfRentStartDt.Value) && !string.IsNullOrEmpty(txtInitMMMngDay.Text))
        //    {
        //        string strStandard = hfRentStartDt.Value.Replace("-", "");
        //        string strInitDay = txtInitMMMngDay.Text;

        //        // KN_USP_COMM_SELECT_COMPAREMONTH_S01
        //        string strInitDt = ChkFunctionUtil.MakeChangeDate(strStandard, Int32.Parse(strInitDay) * -1);

        //        txtInitMMMngDt.Text = TextLib.MakeDateEightDigit(strInitDt);
        //        hfInitMMMngDt.Value = TextLib.MakeDateEightDigit(strInitDt);
        //    }

        //    txtIssueDt.Text = hfIssueDt.Value;
        //    txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
        //    txtRentAgreeDt.Text = hfRentAgreeDt.Value;
        //    txtRentStartDt.Text = hfRentStartDt.Value;
        //    txtRentEndDt.Text = hfRentEndDt.Value;
        //    txtInitMMMngDt.Text = hfInitMMMngDt.Value;
        //    txtHandOverDt.Text = hfHandOverDt.Value;
        //    txtInteriorStartDt.Text = hfInteriorStartDt.Value;
        //    txtInteriorEndDt.Text = hfInteriorEndDt.Value;
        //    txtConsDepositDt.Text = hfConsDepositDt.Value;
        //    txtConsRefundDt.Text = hfConsRefundDt.Value;
        //    txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
        //    txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
        //    txtDepositExpDt.Text = hfDepositExpDt.Value;
        //    txtDepositPayDt.Text = hfDepositPayDt.Value;
        //}

        protected void imgbtnInitMMMngDt_Click(object sender, ImageClickEventArgs e)
        {
            //if (!string.IsNullOrEmpty(hfInitMMMngDt.Value) && !string.IsNullOrEmpty(hfRentStartDt.Value))
            //{
            //    string strStartDt = hfInitMMMngDt.Value.Replace("-", "");
            //    string strEndDt = hfRentStartDt.Value.Replace("-", "");

            //    if (!string.IsNullOrEmpty(strStartDt))
            //    {
            //        if (!string.IsNullOrEmpty(strEndDt))
            //        {
            //            // KN_USP_COMM_SELECT_COMPAREDAY_S00
            //            string strInitDay = ChkFunctionUtil.MakeCheckDay(strStartDt, strEndDt);

            //            txtInitMMMngDay.Text = strInitDay;

            //            if (Int32.Parse(strInitDay) > CommValue.NUMBER_VALUE_0)
            //            {
            //                // KN_USP_COMM_SELECT_COMPAREMONTH_S01
            //                string strInitDt = ChkFunctionUtil.MakeChangeDate(strStartDt, Int32.Parse(strInitDay) - CommValue.NUMBER_VALUE_1);

            //               // txtInteriorStartDt.Text = hfInitMMMngDt.Value;
            //               // hfInteriorStartDt.Value = hfInitMMMngDt.Value;
            //                txtInteriorEndDt.Text = TextLib.MakeDateEightDigit(strInitDt);
            //                hfInteriorEndDt.Value = TextLib.MakeDateEightDigit(strInitDt);
            //            }
            //            else
            //            {
            //                // KN_USP_COMM_SELECT_COMPAREMONTH_S01
            //                string strInitDt = ChkFunctionUtil.MakeChangeDate(strStartDt, Int32.Parse(strInitDay));

            //               // txtInteriorStartDt.Text = hfInitMMMngDt.Value;
            //               // hfInteriorStartDt.Value = hfInitMMMngDt.Value;
            //                txtInteriorEndDt.Text = TextLib.MakeDateEightDigit(strInitDt);
            //                hfInteriorEndDt.Value = TextLib.MakeDateEightDigit(strInitDt);
            //            }
            //        }
            //    }
            //}

            txtIssueDt.Text = hfIssueDt.Value;
            txtOTLAgreeDt.Text = hfOTLAgreeDt.Value;
            txtRentAgreeDt.Text = hfRentAgreeDt.Value;
            txtRentStartDt.Text = hfRentStartDt.Value;
            txtRentEndDt.Text = hfRentEndDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtHandOverDt.Text = hfHandOverDt.Value;
            txtInteriorStartDt.Text = hfInteriorStartDt.Value;
            txtInteriorEndDt.Text = hfInteriorEndDt.Value;
            txtConsDepositDt.Text = hfConsDepositDt.Value;
            txtConsRefundDt.Text = hfConsRefundDt.Value;
           // txtRentalFeeStartDt.Text = hfRentalFeeStartDt.Value;
            //txtRentalFeeEndDt.Text = hfRentalFeeEndDt.Value;
            txtDepositExpDt.Text = hfDepositExpDt.Value;
            txtDepositPayDt.Text = hfDepositPayDt.Value;
        }

        protected void ddlFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ddlFirst.SelectedValue.Equals("B"))
            //{
            //    ddlSecond.Items.Clear();
            //    ddlSecond.Items.Add(new ListItem("-", "-"));
            //}
            //else
            //{
            //    MakeNumberDdl(ddlSecond);
            //}
        }

        protected void ddlSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLeasingAreaDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlThird_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLeasingAreaDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlFouth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLeasingAreaDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlFifth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLeasingAreaDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlSixth_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(ddlSixth.SelectedValue))
            //{
            //    MakeNumberDdl(ddlSeventh);
            //    MakeNumberDdl(ddlEighth);
            //}
            //else
            //{
            //    ddlSeventh.Items.Clear();
            //    ddlEighth.Items.Clear();
            //}
        }

        protected void ddlSeventh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLeasingAreaDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlEighth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckLeasingAreaDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void MakeNumberDdl(DropDownList ddlParams)
        {
            ddlParams.Items.Clear();
            ddlParams.Items.Add(new ListItem("", ""));

            for (int intTmpI = CommValue.NUMBER_VALUE_0; intTmpI <= 9; intTmpI++)
            {
                ddlParams.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }
        }

        private void CheckLeasingAreaDdl()
        {
            //if (!string.IsNullOrEmpty(ddlFirst.SelectedValue) &&
            //    !string.IsNullOrEmpty(ddlSecond.SelectedValue) &&
            //    !string.IsNullOrEmpty(ddlThird.SelectedValue) &&
            //    !string.IsNullOrEmpty(ddlFouth.SelectedValue) &&
            //    !string.IsNullOrEmpty(ddlFifth.SelectedValue))
            //{
            //    if (!string.IsNullOrEmpty(ddlSixth.SelectedValue) &&
            //        !string.IsNullOrEmpty(ddlSeventh.SelectedValue))
            //    {
            //        // - 가 있는 경우
            //        // 중복 데이터 체크
            //        CheckDdlLeasingArea();
            //    }
            //    else
            //    {
            //        if (string.IsNullOrEmpty(ddlSixth.SelectedValue) &&
            //            string.IsNullOrEmpty(ddlSeventh.SelectedValue) &&
            //            string.IsNullOrEmpty(ddlEighth.SelectedValue))
            //        {
            //            // - 가 없는 경우
            //            // 중복 데이터 체크
            //            CheckDdlLeasingArea();
            //        }
            //    }
            //}
        }

        public void LoadFitOutFee(DataTable dsSet)
        {
            var listfitOutFee = new StringBuilder();

            foreach (var fee in from DataRow row in dsSet.Rows
                                select "<tr id='fitfeeNum" + row["MngFeeSeq"] + "'>" +
                                       "<input type=\"hidden\" id=\"isNew\" value=\"N\"/>" +
                                       "<input type=\"hidden\" id=\"feeSeq\" value=\"" + row["MngFeeSeq"] + "\"/>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeStartDt"].ToString()) + "\"></td>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeEndDt"].ToString()) + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeeExcRate"] + "\">&nbsp;VND</td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeePayAmt"] + "\">&nbsp;$</td>" +
                                       "<td align=\"center\" class=\"P0\">" +
                                       "<span><image type=\"image\"   src=\"../../Common/Images/Icon/edit.gif\"  style=\"border-width:0px;\" onclick=\"editFitFee('" + row["MngFeeSeq"] + "')\"></span>" +
                                       "<span><image type=\"image\"  src=\"../../Common/Images/Icon/Trash.gif\" style=\"border-width:0px;\" onclick=\"deleteFitFee('" + row["MngFeeSeq"] + "')\"></span>" +
                                       "</td>" +
                                       "<input type=\"hidden\" value=\"|\"/>" +
                                       "</tr>")
            {
                listfitOutFee.Append(fee);
            }
            displayFitOutFee.InnerHtml = listfitOutFee.ToString();
        }

        public void LoadMngFee(DataTable dsSet)
        {
            var listfitOutFee = new StringBuilder();

            foreach (var fee in from DataRow row in dsSet.Rows
                                select "<tr id='feeNum" + row["MngFeeSeq"] + "'>" +
                                       "<input type=\"hidden\" id=\"isNew\" value=\"N\"/>" +
                                       "<input type=\"hidden\" id=\"feeSeq\" value=\"" + row["MngFeeSeq"] + "\"/>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeStartDt"].ToString()) + "\"></td>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeEndDt"].ToString()) + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeeExcRate"] + "\">&nbsp;VND</td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeePayAmt"] + "\">&nbsp;$</td>" +
                                       "<td align=\"center\" class=\"P0\">" +
                                       "<span><image type=\"image\"   src=\"../../Common/Images/Icon/edit.gif\"  style=\"border-width:0px;\" onclick=\"editMngFee('" + row["MngFeeSeq"] + "')\"></span>" +
                                       "<span><image type=\"image\"  src=\"../../Common/Images/Icon/Trash.gif\" style=\"border-width:0px;\" onclick=\"deleteMngFee('" + row["MngFeeSeq"] + "')\"></span>" +
                                       "</td>" +
                                       "<input type=\"hidden\" value=\"|\"/>" +
                                       "</tr>")
            {
                listfitOutFee.Append(fee);
            }
            diplayMngFee.InnerHtml = listfitOutFee.ToString();
        }

        public void LoadRentFee(DataTable dsSet)
        {
            var strlistRentFee = new StringBuilder();

            foreach (var fee in from DataRow row in dsSet.Rows
                                select "<tr id='rentfeeNum" + row["RentFeeSeq"] + "'>" +
                                       "<input type=\"hidden\" id=\"isNew\" value=\"N\"/>" +
                                       "<input type=\"hidden\" id=\"feeSeq\" value=\"" + row["RentFeeSeq"] + "\"/>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["RentFeeStartDt"].ToString()) + "\"></td>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["RentFeeEndDt"].ToString()) + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["RentFeeExcRate"] + "\">&nbsp;VND</td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["RentFeePayAmt"] + "\">&nbsp;$</td>" +
                                       "<td align=\"center\" class=\"P0\">" +
                                       "<span><image type=\"image\"   src=\"../../Common/Images/Icon/edit.gif\"  style=\"border-width:0px;\" onclick=\"editRentFee('" + row["RentFeeSeq"] + "')\"></span>" +
                                       "<span><image type=\"image\"  src=\"../../Common/Images/Icon/Trash.gif\" style=\"border-width:0px;\" onclick=\"deleteRentFee('" + row["RentFeeSeq"] + "')\"></span>" +
                                       "</td>" +
                                       "<input type=\"hidden\" value=\"|\"/>" +
                                       "</tr>")
            {
                strlistRentFee.Append(fee);
            }
            listRentFee.InnerHtml = strlistRentFee.ToString();
        }

        protected string FormatDateTime(string dateTime)
        {
            if (String.IsNullOrEmpty(dateTime)) return "";
            string date = dateTime.Substring(0, 4) + "-" + dateTime.Substring(4, 2) + "-" + dateTime.Substring(6, 2);
            return date;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public static string GetCustomers()
        {
            return "baokk";
        }

        protected void imgDeleteRentFee_Click(object sender, ImageClickEventArgs e)
        {
            var feeSeq = hfRentFeeSeq.Value;
            var contractNo = txtContNo.Text;
            ContractMngBlo.DeleteRentFeeInfo(Int32.Parse(feeSeq), contractNo); 
            LoadData();

        }

        protected void imgDeleteFee_Click(object sender, ImageClickEventArgs e)
        {
            var feeSeq = hfFeeSeqDel.Value;
            var contractNo = txtContNo.Text;
            ContractMngBlo.DeleteMngInfo(Int32.Parse(feeSeq), contractNo); 
        }
    }
}