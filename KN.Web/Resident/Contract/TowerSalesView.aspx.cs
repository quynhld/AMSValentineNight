using System;
using System.Data;
using System.EnterpriseServices;
using System.Linq;
using System.Text;
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

using System.Net;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace KN.Web.Resident.Contract
{
    [Transaction(TransactionOption.Required)]
    public partial class TowerSalesView : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // 세션체크
                    AuthCheckLib.CheckSession();

                    // 기존 임시 데이터 삭제
                    // KN_USP_RES_DELETE_TEMPRENTDEPOSITINFO_M01
                    // KN_USP_RES_DELETE_TEMPRENTFEEINFO_M01
                    ContractMngBlo.RemoveEntireTempRentMng();

                    CheckParams();

                    // 금일 환율정보가 없을 경우 환율등록 페이지로 이동시킬것.
                    // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S00
                    DataTable dtReturn = ExchangeMngBlo.WatchExchangeRateInfo(txtHfRentCd.Text);

                    if (dtReturn != null)
                    {
                        if (dtReturn.Rows.Count > 0)
                        {
                            txtHfExchangeRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();
                            hfExchangeRate.Value = dtReturn.Rows[0]["DongToDollar"].ToString();
                            ltContExchangeRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();

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
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        protected void InitControls()
        {
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
            ltExchangeRateTitle.Text = TextNm["EXCHANGERATE"];
            ltExchangeUnit.Text = TextNm["DONG"];
            //ltPayStartYYYYMM.Text = TextNm["COMMENCINGDT"];
            ltPayStartYYYYMMUnit.Text = TextNm["MONTH"];
            ltPayTermMonth.Text = TextNm["PAYMENTCYCLE"];
            ltPayTermMonthUnit.Text = TextNm["MONTHS"];
            ltPayDay.Text = TextNm["PAYDAY"];
            ltPayDayUnit.Text = TextNm["DAYS"];


            ltTopRentalFeeStartDt.Text = TextNm["COMMENCINGDT"];
            ltTopRentalFeeEndDt.Text = TextNm["EXPIRINGDT"];
            ltTopRentalFeeRate.Text = TextNm["EXCHANGERATE"];
            ltTopRentalFeeAmt.Text = TextNm["PAYMENT"] + " (" + TextNm["PERMMRENTUSD"] + ")";
            ltMinimumIncome.Text = TextNm["MINIMUM"];
            ltApplyRate.Text = TextNm["APPLYRATE"];

            ltMngFee.Text = TextNm["MANAGEFEE"];
            ltPerMMMngVND.Text = TextNm["PERMMRENTVND"];
            ltPerMMMngVNDNoUnit.Text = TextNm["DONG"];
            ltPerMMMngUSD.Text = TextNm["PERMMRENTUSD"];
            ltPerMMMngUSDNoUnit.Text = TextNm["DOLLAR"];

            ltInitMMMngDay.Text = TextNm["FITTINGDAY"];
            ltInitMMMngDayUnit.Text = TextNm["DAYS"];
            ltInitMMMngDt.Text = TextNm["FITTINGOUTDT"];
            ltInitPerMMMngVND.Text = TextNm["INITPERMMRENTVND"];
            ltInitPerMMMngVNDUnit.Text = TextNm["DONG"];
            ltInitPerMMMngUSD.Text = TextNm["INITPERMMRENTUSD"];
            ltInitPerMMMngUSDUnit.Text = TextNm["DOLLAR"];

            ltDeposit.Text = TextNm["DEPOSIT"];
            ltDepositExpDt.Text = TextNm["SCHEDULEDDATE"];
            ltDepositExpAmt.Text = TextNm["PAYMENT"];
            ltDepositExcRate.Text = TextNm["EXCHANGERATE"];
            ltDepositPayDt.Text = TextNm["PAYDAY"];
            ltDepositPayAmt.Text = TextNm["PAYMENT"];
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


            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlPersonal, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_TENANTTY);
            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlTerm, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_TERM);
            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlContStep, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_CONTSTEP);
            //CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlIndustry, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_INDUSTRY);
            //CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlNat, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_NAT);

            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnList.PostBackUrl = Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text;

            lnkbtnDel.Text = TextNm["DELETE"];
            lnkbtnDel.OnClientClick = "javascript:return fnShowModal('" + Master.PARAM_DATA1 + "','" + txtHfRentCd.Text + "','" + Master.PARAM_DATA2 + "'," + txtHfRentSeq.Text + ");";
            lnkbtnDel.Visible = Master.isModDelAuthOk;

            lnkbtnMod.Text = TextNm["MODIFY"];
            lnkbtnMod.PostBackUrl = Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfRentSeq.Text;
            lnkbtnMod.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_CONT"] + "');";
            lnkbtnMod.Visible = Master.isModDelAuthOk;
        }

        /// <summary>
        /// 파라미터 체크
        /// </summary>
        private void CheckParams()
        {
            string strRentCd = string.Empty;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) &&
                    !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                {
                    txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    txtHfRentSeq.Text = Request.Params[Master.PARAM_DATA2].ToString();
                }
                else
                {
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_OFFICE;
                    txtHfRentSeq.Text = CommValue.NUMBER_VALUE_ONE;
                }
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_OFFICE;
                txtHfRentSeq.Text = CommValue.NUMBER_VALUE_ONE;
            }
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // 임대 계약 상세 내용 조회
            // KN_USP_RES_SELECT_RENTINFO_S01
            var dsReturn = ContractMngBlo.WatchRentInfoView(Session["LangCd"].ToString(), txtHfRentCd.Text, Int32.Parse(txtHfRentSeq.Text));

            if (dsReturn == null) return;
            if (dsReturn.Tables[0] != null)
            {
                if (dsReturn.Tables[0].Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    MakeData(dsReturn.Tables[0]);

                    if (dsReturn.Tables[0].Rows[0]["PodiumYn"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
                    {
                        if (dsReturn.Tables[2] != null)
                        {
                            lvRentalFeeList.DataSource = dsReturn.Tables[2];
                            lvRentalFeeList.DataBind();

                            mvRentFee.ActiveViewIndex = CommValue.NUMBER_VALUE_0;
                        }
                    }
                    else
                    {
                        ltContMinimumIncome.Text = double.Parse(dsReturn.Tables[0].Rows[0]["MinimumRentFee"].ToString()).ToString("###,##0.##");
                        ltContApplyRate.Text = dsReturn.Tables[0].Rows[0]["ApplyRate"].ToString();

                        mvRentFee.ActiveViewIndex = CommValue.NUMBER_VALUE_1;
                    }

                    chkCC.Checked = dsReturn.Tables[0].Rows[0]["CURNCY_TYPE"].ToString() == "CC";
                    if (!chkCC.Checked)
                    {
                        txtFC.Text = dsReturn.Tables[0].Rows[0]["FIXED_DONGTODOLLAR"].ToString();
                        txtFC.ReadOnly = false;
                    }
                    else
                    {
                        txtFC.Visible = false;
                    }
                    txtFloation.Text = dsReturn.Tables[0].Rows[0]["INFLATION_RATE"].ToString();
                }
            }
            if (dsReturn.Tables[1] == null){}
            lvDepositList.DataSource = dsReturn.Tables[1];
            lvDepositList.DataBind();
            if (dsReturn.Tables[3].Rows.Count>0)
            {
                LoadFitOutFee(dsReturn.Tables[3]);
               // chkUsingMnFee.Visible = false;
                isApplyFeeMn.Checked = true;

            }
            else
            {
                lineRow.Visible = true;
                ListFitOutFee.Visible = false;
                isApplyFeeMn.Checked = false;
            }

            if (dsReturn.Tables[4].Rows.Count > 0)
            {
                LoadMngFee(dsReturn.Tables[4]);
            }
            else
            {
                lineRow1.Visible = true;
                ListMngFee.Visible = false;
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
                    foreach(var dr in dtParams.Select())
                    {
                        if (!string.IsNullOrEmpty(dr["InsKNMemNo"].ToString()))
                        {
                            ltContInchage.Text = TextLib.StringDecoder(dr["InsKNMemNo"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["PersonalCd"].ToString()))
                        {
                            ddlPersonal.SelectedValue = dr["PersonalCd"].ToString();
                        }

                        ddlPersonal.Enabled = CommValue.AUTH_VALUE_FALSE;

                        if (!string.IsNullOrEmpty(dr["ContStepCd"].ToString()))
                        {
                            ddlContStep.SelectedValue = dr["ContStepCd"].ToString();
                            if (ddlContStep.SelectedValue.Equals("0003"))
                            {
                                lnkbtnDel.Visible = false;
                                lnkbtnMod.Visible = false;
                            }
                        }

                        ddlContStep.Enabled = CommValue.AUTH_VALUE_FALSE;

                        if (!string.IsNullOrEmpty(dr["LandloadNm"].ToString()))
                        {
                            ltContLandloadNm.Text = TextLib.StringDecoder(dr["LandloadNm"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["IndustryNm"].ToString()))
                        {                            
                            ltIndustry.Text = dr["IndustryNm"].ToString();
                        }                        
                                                

                        if (!string.IsNullOrEmpty(dr["NatNm"].ToString()))
                        {                           
                            ltNat.Text = dr["NatNm"].ToString();
                        }
                                                

                        if (!string.IsNullOrEmpty(dr["RenewDT"].ToString()))
                        {
                            ltRenewDt.Text = TextLib.MakeDateEightDigit(dr["RenewDT"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["ContractNo"].ToString()))
                        {
                            ltContContNo.Text = TextLib.StringDecoder(dr["ContractNo"].ToString());
                            hfContContNo.Value = TextLib.StringDecoder(dr["ContractNo"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadAddr"].ToString()))
                        {
                            ltContLandloadAddr.Text = TextLib.StringDecoder(dr["LandloadAddr"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadDetAddr"].ToString()))
                        {
                            ltContLandloadDetAddr.Text = TextLib.StringDecoder(dr["LandloadDetAddr"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadCorpCert"].ToString()))
                        {
                            ltContLandloadCorpCert.Text = TextLib.StringDecoder(dr["LandloadCorpCert"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadIssuedDt"].ToString()))
                        {
                            ltContIssueDt.Text = TextLib.MakeDateEightDigit(dr["LandloadIssuedDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadTel"].ToString()))
                        {
                            ltContLandloadTel.Text = dr["LandloadTel"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadMobile"].ToString()))
                        {
                            ltContLandloadMobile.Text = dr["LandloadMobile"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadFAX"].ToString()))
                        {
                            ltContLandloadFAX.Text = dr["LandloadFAX"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadEmail"].ToString()))
                        {
                            ltContLandloadEmail.Text = TextLib.StringDecoder(dr["LandloadEmail"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadRepNm"].ToString()))
                        {
                            ltContLandloadRepNm.Text = TextLib.StringDecoder(dr["LandloadRepNm"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["LandloadTaxCd"].ToString()))
                        {
                            ltContLandloadTaxCd.Text = dr["LandloadTaxCd"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RentAddr"].ToString()))
                        {
                            ltContRentAddr.Text = dr["RentAddr"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RentDetAddr"].ToString()))
                        {
                            ltContRentDetAddr.Text = dr["RentDetAddr"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["OTLAgreeDt"].ToString()))
                        {
                            ltContOTLAgreeDt.Text = TextLib.MakeDateEightDigit(dr["OTLAgreeDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["RentAgreeDt"].ToString()))
                        {
                            ltContRentAgreeDt.Text = TextLib.MakeDateEightDigit(dr["RentAgreeDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["FreeRentMonth"].ToString()))
                        {
                            ltContFeeRentMonth.Text = dr["FreeRentMonth"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RentStartDt"].ToString()))
                        {
                            ltContRentStartDt.Text = TextLib.MakeDateEightDigit(dr["RentStartDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["RentEndDt"].ToString()))
                        {
                            ltContRentEndDt.Text = TextLib.MakeDateEightDigit(dr["RentEndDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["TermMonth"].ToString()))
                        {
                            ltContTermMonth.Text = dr["TermMonth"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["HandOverDt"].ToString()))
                        {
                            ltContHandOverDt.Text = TextLib.MakeDateEightDigit(dr["HandOverDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["FloorNo"].ToString()))
                        {
                            ltContFloor.Text = dr["FloorNo"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["RoomNo"].ToString()))
                        {
                            if (!string.IsNullOrEmpty(dr["ExtRoomNo"].ToString()))
                            {
                                ltContRoomNo.Text = dr["RoomNo"].ToString() + " " + dr["ExtRoomNo"].ToString();
                            }
                            else
                            {
                                ltContRoomNo.Text = dr["RoomNo"].ToString();
                            }
                        }

                        if (!string.IsNullOrEmpty(dr["RentLeasingArea"].ToString()))
                        {
                            ltContRentLeasingArea.Text = double.Parse(dr["RentLeasingArea"].ToString()).ToString("###,##0.##");
                        }

                        if (!string.IsNullOrEmpty(dr["AdditionArea"].ToString()))
                        {
                            ltContAdditionalRentArea.Text = double.Parse(dr["AdditionArea"].ToString()).ToString("###,##0.##");
                        }

                        if (!string.IsNullOrEmpty(dr["DongToDollar"].ToString()))
                        {
                            ltContExchangeRate.Text = double.Parse(dr["DongToDollar"].ToString()).ToString("###,##0.##");
                        }

                        if (!string.IsNullOrEmpty(dr["R_S_USING_DATE"].ToString()))
                        {
                            ltContPayStartYYYYMM.Text = TextLib.MakeDateEightDigit(dr["R_S_USING_DATE"].ToString());                       
                        }
                        if (!string.IsNullOrEmpty(dr["M_S_USING_DATE"].ToString()))
                        {
                            ltMCurrentUsingDt.Text = TextLib.MakeDateEightDigit(dr["M_S_USING_DATE"].ToString());                           
                        }
                        
                        if (!string.IsNullOrEmpty(dr["PayTermMonth"].ToString()))
                        {
                            ltContPayTermMonth.Text = dr["PayTermMonth"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["PayDay"].ToString()))
                        {
                            ltContPayDay.Text = dr["PayDay"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["TotViAmtNo"].ToString()))
                        {
                            //if (double.Parse(dr["TotViAmtNo"].ToString()) > CommValue.NUMBER_VALUE_0_0)
                            //{
                            //    ltContSumRentVNDNo.Text = double.Parse(dr["TotViAmtNo"].ToString()).ToString("###,##0");
                            //    ddlTerm.SelectedValue = CommValue.TERM_VALUE_LONGTERM;
                            //}
                            //else
                            //{
                            //    ltContSumRentVNDNo.Text = double.Parse(dr["TotViAmtNo"].ToString()).ToString("###,##0");
                            //    ddlTerm.SelectedValue = CommValue.TERM_VALUE_SHORTTERM;
                            //}
                        }
                        else
                        {
                            ddlTerm.SelectedValue = CommValue.TERM_VALUE_SHORTTERM;
                        }

                        if (!string.IsNullOrEmpty(dr["TotEnAmtNo"].ToString()))
                        {
                            //ltContSumRentUSDNo.Text = double.Parse(dr["TotEnAmtNo"].ToString()).ToString("###,##0.##");
                        }

                        ddlTerm.Enabled = CommValue.AUTH_VALUE_FALSE;

                        if (!string.IsNullOrEmpty(dr["InitDay"].ToString()))
                        {
                            ltContInitMMMngDay.Text = dr["InitDay"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["InitMMMngDt"].ToString()))
                        {
                            ltContInitMMMngDt.Text = TextLib.MakeDateEightDigit(dr["InitMMMngDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["InitMMMngVNDNo"].ToString()))
                        {
                            ltContInitPerMMMngVND.Text = double.Parse(dr["InitMMMngVNDNo"].ToString()).ToString("###,##0");
                        }

                        if (!string.IsNullOrEmpty(dr["InitMMMngUSDNo"].ToString()))
                        {
                            ltContInitPerMMMngUSD.Text = double.Parse(dr["InitMMMngUSDNo"].ToString()).ToString("###,##0.##");
                        }

                        if (!string.IsNullOrEmpty(dr["MMMngVNDNo"].ToString()))
                        {
                            ltContPerMMMngVND.Text =  double.Parse(dr["MMMngVNDNo"].ToString()).ToString("###,##0");
                        }

                        if (!string.IsNullOrEmpty(dr["MMMngUSDNo"].ToString()))
                        {
                            ltContPerMMMngUSD.Text = double.Parse(dr["MMMngUSDNo"].ToString()).ToString("###,##0.##");
                        }

                        if (!string.IsNullOrEmpty(dr["DepositViAmtNo"].ToString()))
                        {
                            ltContSumDepositVNDNo.Text = double.Parse(dr["DepositViAmtNo"].ToString()).ToString("###,##0");
                        }

                        if (!string.IsNullOrEmpty(dr["DepositEnAmtNo"].ToString()))
                        {
                            ltContSumDepositUSDNo.Text = double.Parse(dr["DepositEnAmtNo"].ToString()).ToString("###,##0.##");
                        }

                        if (!string.IsNullOrEmpty(dr["InteriorStartDt"].ToString()))
                        {
                            ltContInteriorStartDt.Text = TextLib.MakeDateEightDigit(dr["InteriorStartDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["InteriorEndDt"].ToString()))
                        {
                            ltContInteriorEndDt.Text = TextLib.MakeDateEightDigit(dr["InteriorEndDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["ConsDeposit"].ToString()))
                        {
                            ltContConsDeposit.Text = double.Parse(dr["ConsDeposit"].ToString()).ToString("###,##0");
                        }

                        if (!string.IsNullOrEmpty(dr["ConsDepositDt"].ToString()))
                        {
                            ltContConsDepositDt.Text = TextLib.MakeDateEightDigit(dr["ConsDepositDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["ConsRefund"].ToString()))
                        {
                            ltContConsRefund.Text = double.Parse(dr["ConsRefund"].ToString()).ToString("###,##0");
                        }

                        if (!string.IsNullOrEmpty(dr["ConsRefundDt"].ToString()))
                        {
                            ltContConsRefundDt.Text = TextLib.MakeDateEightDigit(dr["ConsRefundDt"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["DifferenceReason"].ToString()))
                        {
                            ltContDifferenceReason.Text = TextLib.StringDecoder(dr["DifferenceReason"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["Memo"].ToString()))
                        {
                            ltContMemo.Text = TextLib.StringDecoder(dr["Memo"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["REMARK"].ToString()))
                        {
                            txtContRemark.Text = TextLib.StringDecoder(dr["REMARK"].ToString());
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
                            ltMCurrentPayDt.Text = TextLib.MakeDateEightDigit(dr["M_S_PAY_DATE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["M_PAYCYCLE"].ToString()))
                        {
                            txtMPayCycle.Text = dr["M_PAYCYCLE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["M_PAYCYCLE_TYPE"].ToString()))
                        {
                            ddlMPaymentType.SelectedValue = TextLib.StringDecoder(dr["M_PAYCYCLE_TYPE"].ToString());
                         
                        }
                        if (!string.IsNullOrEmpty(dr["R_PAYCYCLE_TYPE"].ToString()))
                        {
                            ddlRPaymentCycle.SelectedValue = dr["R_PAYCYCLE_TYPE"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["R_S_PAY_DATE"].ToString()))
                        {
                            ltRSPayDt.Text = TextLib.MakeDateEightDigit(dr["R_S_PAY_DATE"].ToString());
                        }

                        if (!string.IsNullOrEmpty(dr["R_ISUE_DATE_TYPE"].ToString()))
                        {
                            ddlRIsueDateType.Text = TextLib.MakeDateEightDigit(dr["R_ISUE_DATE_TYPE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["M_ISUE_DATE_TYPE"].ToString()))
                        {
                            ddlMIsueDateType.Text = TextLib.MakeDateEightDigit(dr["M_ISUE_DATE_TYPE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["R_ISUE_DATE_ADJUST"].ToString()))
                        {
                            ltRIsueAdjustDt.Text = TextLib.MakeDateEightDigit(dr["R_ISUE_DATE_ADJUST"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["M_ISUE_DATE_ADJUST"].ToString()))
                        {
                            ltMIsueAdjustDt.Text = TextLib.MakeDateEightDigit(dr["M_ISUE_DATE_ADJUST"].ToString());
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
                            ltRIsueAdjustDt.Text = dr["R_ISUE_DATE_ADJUST"].ToString();
                        }
                        if (!string.IsNullOrEmpty(dr["M_ISUE_DATE_ADJUST"].ToString()))
                        {
                            ltMIsueAdjustDt.Text = dr["M_ISUE_DATE_ADJUST"].ToString();
                        }

                        if (!string.IsNullOrEmpty(dr["IS_SPECIAL"].ToString()))
                        {
                            chkSpecialContract.Checked = dr["IS_SPECIAL"].ToString()=="Y";
                        }

                        if (!string.IsNullOrEmpty(dr["R_E_USING_DATE"].ToString()))
                        {
                            ltSREndDateV.Text = TextLib.MakeDateEightDigit(dr["R_E_USING_DATE"].ToString());
                        }
                        if (!string.IsNullOrEmpty(dr["M_E_USING_DATE"].ToString()))
                        {
                            ltSMEndDateV.Text = TextLib.MakeDateEightDigit(dr["M_E_USING_DATE"].ToString());
                        }
                    }
                }
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

                if (!string.IsNullOrEmpty(drView["ContractNo"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtContractNo")).Text = drView["ContractNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentFeeSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtRentFeeSeq")).Text = drView["RentFeeSeq"].ToString();
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
                
                imgbtnDelete.Visible = Master.isModDelAuthOk;
                imgbtnModify.Visible = Master.isModDelAuthOk;                
            }
        }

        protected void lvRentalFeeList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strRentCd = txtHfRentCd.Text;

                string strContractNo = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtContractNo")).Text;
                string strRentFeeSeq = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentFeeSeq")).Text;
                string strRentalFeeStartDt = ((HiddenField)lvRentalFeeList.Items[e.ItemIndex].FindControl("hfRentalFeeStartDt")).Value.Replace("-", "");
                string strRentalFeeEndDt = ((HiddenField)lvRentalFeeList.Items[e.ItemIndex].FindControl("hfRentalFeeEndDt")).Value.Replace("-", "");
                string strRentalFeeExcRate = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentalFeeExcRate")).Text;
                string strRentalFeeExpAmt = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentalFeeExpAmt")).Text;

                double dblRentalFeeExcRate = CommValue.NUMBER_VALUE_0_0;
                double dblRentalFeeExpAmt = CommValue.NUMBER_VALUE_0_0;

                if (!string.IsNullOrEmpty(strRentalFeeExcRate))
                {
                    dblRentalFeeExcRate = double.Parse(strRentalFeeExcRate);
                }

                if (!string.IsNullOrEmpty(strRentalFeeExpAmt))
                {
                    dblRentalFeeExpAmt = double.Parse(strRentalFeeExpAmt);
                }

                // 오피스 / 리테일 임대료 데이터 수정
                // KN_USP_RES_UPDATE_RENTFEEINFO_M00
                ContractMngBlo.ModifyRentFeeInfo(strContractNo, Int32.Parse(strRentFeeSeq), strRentalFeeStartDt, strRentalFeeEndDt, dblRentalFeeExcRate, dblRentalFeeExpAmt);

                // 오피스 / 리테일 임대료 데이터 조회
                // KN_USP_RES_SELECT_RENTFEEINFO_S00
                DataTable dtRentalFee = ContractMngBlo.SpreadRentFeeInfo(strContractNo);

                if (dtRentalFee != null)
                {
                    lvRentalFeeList.DataSource = dtRentalFee;
                    lvRentalFeeList.DataBind();
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

        protected void lvRentalFeeList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strContractNo = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtContractNo")).Text;
                string strRentFeeSeq = ((TextBox)lvRentalFeeList.Items[e.ItemIndex].FindControl("txtRentFeeSeq")).Text;

                // 오피스 / 리테일 임대료 데이터 삭제
                // KN_USP_RES_DELETE_RENTFEEINFO_M00
                ContractMngBlo.RemoveRentFeeInfo(strContractNo, Int32.Parse(strRentFeeSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                // 오피스 / 리테일 임대료 데이터 조회
                // KN_USP_RES_SELECT_RENTFEEINFO_S00
                DataTable dtRentalFee = ContractMngBlo.SpreadRentFeeInfo(strContractNo);

                if (dtRentalFee != null)
                {
                    lvRentalFeeList.DataSource = dtRentalFee;
                    lvRentalFeeList.DataBind();
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

                if (!string.IsNullOrEmpty(drView["ContractNo"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtContractNo")).Text = drView["ContractNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DepositSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtDepositSeq")).Text = drView["DepositSeq"].ToString();
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

                HiddenField hfOldDepositPayAmt = ((HiddenField)iTem.FindControl("hfOldDepositPayAmt"));
                TextBox txtAftDepositPayAmt = ((TextBox)iTem.FindControl("txtAftDepositPayAmt"));

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");

                if (!string.IsNullOrEmpty(drView["DepositPayAmt"].ToString()))
                {
                    hfOldDepositPayAmt.Value = drView["DepositPayAmt"].ToString();
                    txtAftDepositPayAmt.Text = drView["DepositPayAmt"].ToString();

                    if (double.Parse(txtAftDepositPayAmt.Text) > CommValue.NUMBER_VALUE_0)
                    {
                        imgbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        imgbtnDelete.Visible = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                ((Literal)iTem.FindControl("ltDepositExpAmtUnit")).Text = TextNm["DONG"];
                ((Literal)iTem.FindControl("ltDepositExcRateUnit")).Text = TextNm["DONG"];
                ((Literal)iTem.FindControl("ltDepositPayAmtUnit")).Text = TextNm["DONG"];

                ((Literal)iTem.FindControl("ltExpCalendar")).Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"ContCalendar(this, '" + txtDepositExpDt.ClientID + "','" + hfDepositExpDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";
                ((Literal)iTem.FindControl("ltPayCalendar")).Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"ContCalendar(this, '" + txtDepositPayDt.ClientID + "','" + hfDepositPayDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";

                imgbtnModify.OnClientClick = "javascript:return fnCheckItems('" + txtDepositExpDt.ClientID + "','" + hfDepositExpDt.ClientID + "','" + txtDepositExpAmt.ClientID + "','" + txtDepositPayDt.ClientID + "','" + hfDepositPayDt.ClientID + "','" + hfOldDepositPayAmt.ClientID + "','" + txtAftDepositPayAmt.ClientID + "','" + AlertNm["ALERT_INSERT_BLANK"] + "')";

                imgbtnDelete.Visible = Master.isModDelAuthOk;
                imgbtnModify.Visible = Master.isModDelAuthOk;
            }
        }

        protected void lvDepositList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strRentCd = txtHfRentCd.Text;
                string strContractNo = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtContractNo")).Text;
                string strDepositSeq = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositSeq")).Text;
                string strDepositExpDt = ((HiddenField)lvDepositList.Items[e.ItemIndex].FindControl("hfDepositExpDt")).Value.Replace("-", "");
                string strDepositExpAmt = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositExpAmt")).Text;
                string strDepositExcRate = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositExcRate")).Text;
                string strDepositPayDt = ((HiddenField)lvDepositList.Items[e.ItemIndex].FindControl("hfDepositPayDt")).Value.Replace("-", "");
                string strAftDepositPayAmt = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtAftDepositPayAmt")).Text;

                double dblDepositExpAmt = CommValue.NUMBER_VALUE_0_0;
                double dblDepositExcRate = CommValue.NUMBER_VALUE_0_0;
                double dblDepositPayAmt = CommValue.NUMBER_VALUE_0_0;

                if (!string.IsNullOrEmpty(strDepositExpAmt))
                {
                    dblDepositExpAmt = double.Parse(strDepositExpAmt);
                }

                if (!string.IsNullOrEmpty(strDepositExcRate))
                {
                    dblDepositExcRate = double.Parse(strDepositExcRate);
                }

                if (!string.IsNullOrEmpty(strAftDepositPayAmt))
                {
                    dblDepositPayAmt = double.Parse(strAftDepositPayAmt);
                }

                // 보증금 데이터 수정
                // KN_USP_RES_UPDATE_RENTDEPOSITINFO_M00
                ContractMngBlo.ModifyRentDepoitInfo(strContractNo, Int32.Parse(strDepositSeq), strDepositExpDt, dblDepositExpAmt, dblDepositExcRate,
                                                    strDepositPayDt, dblDepositPayAmt);

                // 양영석 : 차후 보증금 관리가 필요할 경우 고려할 것.
                // 3441300 - Tower Retail
                // 3441400 - Tower Office
                // 1. 각종 세금 처리 원장등록
                // 2. 납입 계좌정보 등록
                // 3. 보증금 수납 및 환불
                // 4. 원장 상세 테이블 처리
                // 5. 출력 테이블에 등록
                // 6. 출력자 테이블에 등록
                // 7. 금액 로그 테이블 처리

                // 보증금 데이터 조회
                // KN_USP_RES_SELECT_RENTDEPOSITINFO_S00
                DataTable dtDeposit = ContractMngBlo.SpreadRentDepositInfo(strContractNo);

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

                string strContractNo = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtContractNo")).Text;
                string strDepositSeq = ((TextBox)lvDepositList.Items[e.ItemIndex].FindControl("txtDepositSeq")).Text;

                // 오피스 / 리테일 보증금 데이터 삭제
                // KN_USP_RES_DELETE_RENTDEPOSITINFO_M00
                ContractMngBlo.RemoveRentDepoitInfo(strContractNo, Int32.Parse(strDepositSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                // 오피스 / 리테일 보증금 데이터 조회
                // KN_USP_RES_SELECT_RENTDEPOSITINFO_S00
                DataTable dtDeposit = ContractMngBlo.SpreadRentDepositInfo(strContractNo);

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

        protected void imgbtnDel_Click(object sender, ImageClickEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 팝업의 불법적인 접근을 제한하기 위한 세션 생성
            Session["DelContractYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
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

        protected void imgbtnMove_Click(object sender, ImageClickEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
        }

        public void LoadFitOutFee(DataTable dsSet)
        {
            var listfitOutFee = new StringBuilder();

            foreach (var fee in from DataRow row in dsSet.Rows
                                select "<tr>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeStartDt"].ToString()) + "\"></td>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeEndDt"].ToString()) + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeeExcRate"] + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeePayAmt"] + "\"></td>" +
                                       "<td></td>" +
                                       "</tr>")
            {
                listfitOutFee.Append(fee);
            }
            diplayFitOutFee.InnerHtml = listfitOutFee.ToString();
        }

        public void LoadMngFee(DataTable dsSet)
        {
            var listMngFee = new StringBuilder();

            foreach (var fee in from DataRow row in dsSet.Rows
                                select "<tr>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeStartDt"].ToString()) + "\"></td>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeEndDt"].ToString()) + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeeExcRate"] + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeePayAmt"] + "\"></td>" +
                                       "<td></td>" +
                                       "</tr>")
            {
                listMngFee.Append(fee);
            }
            displayMngFee.InnerHtml = listMngFee.ToString();
        }
        protected string FormatDateTime(string dateTime)
        {
            if (String.IsNullOrEmpty(dateTime)) return "";
            string date = dateTime.Substring(0, 4) + "-" + dateTime.Substring(4, 2) + "-" + dateTime.Substring(6, 2);
            return date;
        }

        private void getpdfLink()
        { 
        
        }

        protected void btnPDF_Click1(object sender, EventArgs e)
        {
            string conLogconnString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(conLogconnString);
            string strCmd = string.Format("select * from ContractPDF where rentCD ='{0}' and rentSeq ='{1}'", Request["RentCd"], Request["RentSeq"]);
            SqlDataAdapter adap = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(strCmd, conn);
            adap.SelectCommand = cmd;
            DataTable dtbPDF = new DataTable();
            adap.Fill(dtbPDF);
            if (dtbPDF.Rows.Count > 0)
            {
                string FilePath = Server.MapPath(dtbPDF.Rows[0][4].ToString().Replace("\\", "//"));
                WebClient User = new WebClient();
                Byte[] FileBuffer = User.DownloadData(FilePath);
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }
            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('User has not upload contract file !')</script>");
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string url = string.Format("ContractPDFView.aspx?rentCD={0}&rentSeq={1}", Request["RentCd"], Request["RentSeq"]);
            //string s = "window.open('" +  + "', 'popup_window', 'width=800,height=600,left=100,top=100,resizable=yes');";
            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('"+url+"');", true);
        }
    }
}