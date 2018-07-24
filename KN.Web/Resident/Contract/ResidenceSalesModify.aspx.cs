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

using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;

namespace KN.Web.Resident.Contract
{
   // [Transaction(TransactionOption.Required)]
    public partial class ResidenceSalesModify : BasePage
    {
        RentMngDs.SalesInfo rsDs = new RentMngDs.SalesInfo();
        RentMngDs.SalesCompInfo rsComDs = new RentMngDs.SalesCompInfo();
        RentMngDs.SalesColInfo rsColDs = new RentMngDs.SalesColInfo();

        DataTable dtReturn = new DataTable();
        DataTable dtFee = new DataTable("FeeData");

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                // 세션체크
                AuthCheckLib.CheckSession();

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
                        txtExchangeRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();

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
            ltIncharge.Text = TextNm["INCHARGE"];
            ltBasicInfo.Text = TextNm["TENANTINFO"];
            ltTenantNm.Text = TextNm["TENANTNM"];
            ltContNo.Text = TextNm["CONTNO"];
            ltAddr.Text = TextNm["ADDR"];
            ltICPN.Text = TextNm["IDNO"];
            ltIssueDt.Text = TextNm["ISSUEDT"];
            ltIssuePlace.Text = TextNm["ISSUEPLACE"];
            ltTel.Text = TextNm["TEL"];
            ltMobileNo.Text = TextNm["MOBILE"];
            ltFAX.Text = TextNm["FAX"];
            ltEmail.Text = TextNm["EMAIL"];
            ltRepresent.Text = TextNm["REPRESENTATIVE"];
            ltPosition.Text = TextNm["POS"];
            ltBank.Text = TextNm["BANKACC"];
            ltTaxCd.Text = TextNm["TAXCD"];
            ltConcYn.Text = TextNm["CONCYN"];

            ltCoInfo.Text = TextNm["COOWNERINFO"];
            ltCoOwnerNm.Text = TextNm["COOWNERNM"];
            ltRelationShip.Text = TextNm["POS"];
            ltCoRss.Text = TextNm["IDNO"];
            ltCoIssueDt.Text = TextNm["ISSUEDT"];
            ltCoIssuePlace.Text = TextNm["ISSUEPLACE"];
            ltCoAddr.Text = TextNm["ADDR"];

            ltRoomInfo.Text = TextNm["ROOMINFO"];
            ltUnitNo.Text = TextNm["UNITNO"];
            ltFloor.Text = TextNm["FLOOR"];
            ltLeasingArea.Text = TextNm["LEASINGAREA"];

            ltRentTerm.Text = TextNm["TERMINFO"];
            ltCommencingDt.Text = TextNm["COMMENCINGDT"];
            ltExpiringDt.Text = TextNm["EXPIRINGDT"];
            ltLastKeyDt.Text = TextNm["LASTKEYDT"];

            ltRetalFee.Text = TextNm["RENTALFEE"];
            ltExchangeRate.Text = TextNm["EXCHANGERATE"];
            ltSumRentUSDNo.Text = TextNm["SUMRENTOFUSDNO"];
            ltSumRentUSDNoUnit.Text = TextNm["DOLLAR"];
            ltSumRentVNDNo.Text = TextNm["SUMRENTOFDONGNO"];
            ltSumRentVNDNoUnit.Text = TextNm["DONG"];
            ltDeposit.Text = TextNm["DEPOSIT"];
            ltSumDepositUSDNo.Text = TextNm["SUMDEPOSITUSD"];
            ltDepositSumUSDNoUnit.Text = TextNm["DOLLAR"];
            ltSumDepositVNDNo.Text = TextNm["SUMDEPOSITVND"];
            ltDepositSumVNDNoUnit.Text = TextNm["DONG"];

            ltMngFee.Text = TextNm["MANAGEFEE"];
            lnkbtnChange.Text = TextNm["TEMPCHANGE"];
            ltInitMMMngDay.Text = TextNm["FITTINGDAY"];
            ltInitMMMngDt.Text = TextNm["FITTINGOUTDT"];
            //ltInitPerMMMngVND.Text = TextNm["INITPERMMRENTVND"];
            //ltInitPerMMMngVNDUnit.Text = TextNm["DONG"];
            //ltInitPerMMMngUSD.Text = TextNm["INITPERMMRENTUSD"];
            //ltInitPerMMMngUSDUnit.Text = TextNm["DOLLAR"];
            ltPerMMRentUSD.Text = TextNm["MMRENTUSD"];
            ltPerMMRentUSDUnit.Text = TextNm["DOLLAR"];
            ltPerMMRentVND.Text = TextNm["MMRENTVND"];
            ltPerMMRentVNDUnit.Text = TextNm["DONG"];
            //ltPerMMMngUSD.Text = TextNm["MMMNGUSD"];
            //ltPerMMMngUSDNoUnit.Text = TextNm["DOLLAR"];
            //ltPerMMMngVND.Text = TextNm["MMMNGVND"];
            //ltPerMMMngVNDNoUnit.Text = TextNm["DONG"];

            ltUse.Text = TextNm["USE"];
            ltTradeNm.Text = TextNm["TRADE"];
            ltPurpose.Text = TextNm["PURPOSE"];

            ltOtherCondition.Text = TextNm["OTHERCOND"];
            ltPlusCondDt.Text = TextNm["COMMENCINGDT"];
            ltPlusCond.Text = TextNm["OTHERCOND"];

            ltOther.Text = TextNm["OTHERS"];
            ltMemo.Text = TextNm["MEMO"];

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.Visible = Master.isModDelAuthOk;
            lnkbtnCancel.Text = TextNm["CANCEL"];

            lnkbtnModify.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + CommValue.TENANTTY_VALUE_CORPORATION + "','" + CommValue.TERM_VALUE_LONGTERM + "','" + CommValue.TERM_VALUE_SHORTTERM + "');";
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";
            lnkbtnChange.OnClientClick = "javascript:return fnChangePopup('" + txtHfExchangeRate.Text + "','" + txtExchangeRate.ClientID + "','" + hfExchangeRate.ClientID + "');";
            txtFloor.Attributes["onblur"] = "checkRoom()";
            txtUnitNo.Attributes["onblur"] = "checkRoom()";

            txtInchage.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelMidNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileMidNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFAXFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFAXMidNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFAXRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFloor.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtPerMMRentVND.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtPerMMRentUSD.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            txtSumRentVNDNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtSumRentUSDNo.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            txtSumDepositVNDNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtSumDepositUSDNo.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            //txtPerMMMngVND.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            //txtPerMMMngUSD.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            txtRepresent.Text = string.Empty;
            txtRepresent.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            txtPosition.Text = string.Empty;
            txtPosition.ReadOnly = CommValue.AUTH_VALUE_TRUE;

            txtPerMMRentUSD.Text = string.Empty;
            txtPerMMRentUSD.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            txtPerMMRentVND.Text = string.Empty;
            txtPerMMRentVND.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            txtSumDepositUSDNo.Text = string.Empty;
            txtSumDepositUSDNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            txtSumDepositVNDNo.Text = string.Empty;
            txtSumDepositVNDNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;

            txtSumRentUSDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
            txtSumRentVNDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlContTy, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_APARTMENTTY);
            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlTerm, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_TERM);
            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlPersonal, Session["LANGCD"].ToString(), CommValue.ETCCD_VALUE_TENANTTY);

            if (txtHfRentCd.Text.Equals(CommValue.APARTMENTTY_VALUE_APART_A) ||
                txtHfRentCd.Text.Equals(CommValue.APARTMENTTY_VALUE_APART_B) ||
                txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APT))
            {
                ddlContTy.Items.FindByValue(CommValue.APARTMENTTY_VALUE_APARTSTORE_A).Enabled = CommValue.AUTH_VALUE_FALSE;
                ddlContTy.Items.FindByValue(CommValue.APARTMENTTY_VALUE_APARTSTORE_B).Enabled = CommValue.AUTH_VALUE_FALSE;

                ddlContTy.SelectedValue = CommValue.APARTMENTTY_VALUE_APART_A;
            }
            else if (txtHfRentCd.Text.Equals(CommValue.APARTMENTTY_VALUE_APARTSTORE_A) ||
                     txtHfRentCd.Text.Equals(CommValue.APARTMENTTY_VALUE_APARTSTORE_B) ||
                     txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APTSHOP))
            {
                ddlContTy.Items.FindByValue(CommValue.APARTMENTTY_VALUE_APART_A).Enabled = CommValue.AUTH_VALUE_FALSE;
                ddlContTy.Items.FindByValue(CommValue.APARTMENTTY_VALUE_APART_B).Enabled = CommValue.AUTH_VALUE_FALSE;

                ddlContTy.SelectedValue = CommValue.APARTMENTTY_VALUE_APARTSTORE_A;
            }
            LoadLessor();

        }

        private void CheckParams()
        {
            string strRentCd = string.Empty;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) &&
                    !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                {
                    if (Request.Params[Master.PARAM_DATA1].ToString().Equals(CommValue.RENTAL_VALUE_APTA) ||
                        Request.Params[Master.PARAM_DATA1].ToString().Equals(CommValue.RENTAL_VALUE_APTB))
                    {
                        txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
                    }
                    else if (Request.Params[Master.PARAM_DATA1].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                             Request.Params[Master.PARAM_DATA1].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                    {
                        txtHfRentCd.Text = CommValue.RENTAL_VALUE_APTSHOP;
                    }

                    txtHfParamRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    txtHfParamRentSeq.Text = Request.Params[Master.PARAM_DATA2].ToString();
                }
                else
                {
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
                    txtHfParamRentCd.Text = CommValue.RENTAL_VALUE_APTA;
                    txtHfParamRentSeq.Text = CommValue.NUMBER_VALUE_ONE;
                }
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
                txtHfParamRentCd.Text = CommValue.RENTAL_VALUE_APTA;
                txtHfParamRentSeq.Text = CommValue.NUMBER_VALUE_ONE;
            }
        }

        private void LoadData()
        {
            DataSet dsReturn = new DataSet();

            // KN_USP_RES_SELECT_SALESINFO_S01
            dsReturn = ContractMngBlo.WatchSalesInfoView(Session["LangCd"].ToString(), txtHfParamRentCd.Text, Int32.Parse(txtHfParamRentSeq.Text));

            if (dsReturn != null)
            {
                if (dsReturn.Tables[0].Rows.Count > 0)
                {
                    InitControls();
                    MakeData(dsReturn);
                    if (dsReturn.Tables[3].Rows.Count > 0)
                    {
                        LoadFitOutFee(dsReturn.Tables[3]);
                        //chkUsingMnFee.Visible = false;
                        //lineRow.Visible = false;
                        isApplyFeeMn.Checked = true;
                        //txtFC.Text = hfExchangeRate.Value;
                    }
                    else
                    {
                        //ListFitOutFee.Visible = false;
                        //lineRow.Visible = true;
                        isApplyFeeMn.Checked = false;
                        //txtFC.Text = hfExchangeRate.Value;
                    }
                    if (dsReturn.Tables[4].Rows.Count < 0)return;
                    LoadMngFee(dsReturn.Tables[4]);

                }
                else
                {
                    Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                }
            }
            else
            {
                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
        }

        /// <summary>
        /// 데이터 바인딩
        /// </summary>
        /// <param name="dsParams"></param>
        public void MakeData(DataSet dsParams)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtRentInfo = new DataTable();

            dtRentInfo = dsParams.Tables[0];

            txtInchage.Text = dtRentInfo.Rows[0]["InsKNMemNo"].ToString();

            //입주자정보
            ddlPersonal.SelectedValue = dtRentInfo.Rows[0]["PersonalCd"].ToString();
            txtTenantNm.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["ContractNm"].ToString());
            ddlConcYn.SelectedValue = dtRentInfo.Rows[0]["ContChk"].ToString();
            ddlContTy.SelectedValue = dtRentInfo.Rows[0]["RentCd"].ToString();
            ddlTerm.SelectedValue = dtRentInfo.Rows[0]["RentTy"].ToString();
            txtContNo.Text = dtRentInfo.Rows[0]["ContractNo"].ToString();
            hfContNo.Value = dtRentInfo.Rows[0]["ContractNo"].ToString();
            txtAddr.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Addr"].ToString());
            txtDetAddr.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["DetailAddr"].ToString());
            txtICPN.Text = dtRentInfo.Rows[0]["RssNo"].ToString();
            txtIssueDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["IssueDt"].ToString());
            hfIssueDt.Value = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["IssueDt"].ToString());
            txtIssuePlace.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["IssuePlace"].ToString());
            txtTelFrontNo.Text = dtRentInfo.Rows[0]["TelFontNo"].ToString();
            txtTelMidNo.Text = dtRentInfo.Rows[0]["TelMidNo"].ToString();
            txtTelRearNo.Text = dtRentInfo.Rows[0]["TelRearNo"].ToString();
            txtMobileFrontNo.Text = dtRentInfo.Rows[0]["MobileFrontNo"].ToString();
            txtMobileMidNo.Text = dtRentInfo.Rows[0]["MobileMidNo"].ToString();
            txtMobileRearNo.Text = dtRentInfo.Rows[0]["MobileRearNo"].ToString();
            txtFAXFrontNo.Text = dtRentInfo.Rows[0]["OfficeTelFontNo"].ToString();
            txtFAXMidNo.Text = dtRentInfo.Rows[0]["OfficeTelMidNo"].ToString();
            txtFAXRearNo.Text = dtRentInfo.Rows[0]["OfficeTelRearNo"].ToString();
            txtEmailID.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["EmailID"].ToString());
            txtEmailServer.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["EmailServer"].ToString());
            txtRepresent.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["LegalRep"].ToString());
            txtPosition.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Position"].ToString());
            txtBank.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["BankAcc"].ToString());
            txtTaxCd.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["TaxCd"].ToString());

            // 공동명의인
            txtCoOwnerNm.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["CoOwner"].ToString());
            txtRelationShip.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Relationship"].ToString());
            txtCoRss.Text = dtRentInfo.Rows[0]["CoRssNo"].ToString();
            txtCoIssueDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["CoIssueDt"].ToString());
            hfCoIssueDt.Value = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["CoIssueDt"].ToString());
            txtCoIssuePlace.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["CoIssuePlace"].ToString());
            txtCoAddr.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["CoAddr"].ToString());
            txtCoDetAddr.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["CoDetailAddr"].ToString());

            //기간
            txtCommencingDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["ContDt"].ToString());
            hfCommencingDt.Value = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["ContDt"].ToString());
            txtExpiringDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["ResaleDt"].ToString());
            hfExpiringDt.Value = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["ResaleDt"].ToString());
            txtLastKeyDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["LastKeyDt"].ToString());
            hfLastKeyDt.Value = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["LastKeyDt"].ToString());

            //방정보
            txtUnitNo.Text = dtRentInfo.Rows[0]["RoomNo"].ToString();
            txtFloor.Text = dtRentInfo.Rows[0]["FloorNo"].ToString();
            txtLeasingArea.Text = dtRentInfo.Rows[0]["LeasingArea"].ToString();
            txtRoomNoExt.Text = dtRentInfo.Rows[0]["RoomNoExt"].ToString();
            ddlLessor.SelectedValue = string.IsNullOrEmpty(dtRentInfo.Rows[0]["Ref_ContractNo"].ToString()) ? "" : dtRentInfo.Rows[0]["Ref_ContractNo"].ToString();
            txtLessorRoomNo.Text = string.IsNullOrEmpty(dtRentInfo.Rows[0]["Ref_ContractNo"].ToString()) ? "" : dtRentInfo.Rows[0]["Ref_ContractNo"].ToString();

            //임대비용
            txtExchangeRate.Text = dtRentInfo.Rows[0]["DongToDollar"].ToString();
            hfExchangeRate.Value = dtRentInfo.Rows[0]["DongToDollar"].ToString();

            if (dtRentInfo.Rows[0]["RentTy"].ToString().Equals("0001"))
            {
                txtSumRentVNDNo.Text = double.Parse(dtRentInfo.Rows[0]["RentalFeeVNDNo"].ToString()).ToString("###,##0");
                txtSumRentUSDNo.Text = double.Parse(dtRentInfo.Rows[0]["RentalFeeUSDNo"].ToString()).ToString("###,##0.##");

            }
            else
            {
                txtPerMMRentVND.Text = double.Parse(dtRentInfo.Rows[0]["RentalFeeVNDNo"].ToString()).ToString("###,##0");
                txtPerMMRentUSD.Text = double.Parse(dtRentInfo.Rows[0]["RentalFeeUSDNo"].ToString()).ToString("###,##0.##");
            }

            //보증금
            txtSumDepositVNDNo.Text = double.Parse(dtRentInfo.Rows[0]["DepositVNDNo"].ToString()).ToString("###,##0");
            txtSumDepositUSDNo.Text = double.Parse(dtRentInfo.Rows[0]["DepositUSDNo"].ToString()).ToString("###,##0.##");

            //관리비용
            txtInitMMMngDay.Text = dtRentInfo.Rows[0]["InitDay"].ToString();
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["InitMMMngDt"].ToString()))
            {
                txtInitMMMngDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["InitMMMngDt"].ToString());
                hfInitMMMngDt.Value = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["InitMMMngDt"].ToString());
            }
           // txtInitPerMMMngVND.Text = double.Parse(dtRentInfo.Rows[0]["InitMMMngVNDNo"].ToString()).ToString("###,##0");
           // txtInitPerMMMngUSD.Text = double.Parse(dtRentInfo.Rows[0]["InitMMMngUSDNo"].ToString()).ToString("###,##0.##");
           // txtPerMMMngVND.Text = double.Parse(dtRentInfo.Rows[0]["MMMngVNDNo"].ToString()).ToString("###,##0");
           // txtPerMMMngUSD.Text = double.Parse(dtRentInfo.Rows[0]["MMMngUSDNo"].ToString()).ToString("###,##0.##");

            //용도
            txtTradeNm.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["TradeNm"].ToString());
            txtPurpose.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Purpose"].ToString());

            //기타조건
            txtPlusCondDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["PlusCondDt"].ToString());
            hfPlusCondDt.Value = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["PlusCondDt"].ToString());
            txtPlusCond.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Descript1"].ToString());

            //기타
            txtMemo.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Descript2"].ToString());

            string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
            string strResaleDt = dtRentInfo.Rows[0]["ResaleDt"].ToString();
            //isApplyFeeMn.CheckedChanged = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_CONT"] + "');";
            isApplyFeeMn.Attributes["onclick"] = "fnApplyFee()";
            chkCC.Checked = dtRentInfo.Rows[0]["CURNCY_TYPE"].ToString() == "CC";
            if (!chkCC.Checked)
            {
                txtFC.Text = dtRentInfo.Rows[0]["FIXED_DONGTODOLLAR"].ToString();
            }
            //rbContractType.SelectedValue = dtRentInfo.Rows[0]["FIXED_DONGTODOLLAR"].ToString();
            chkCC.Attributes["onclick"] = "chkCCChange(this)";
            hfCCtype.Value = dtRentInfo.Rows[0]["CURNCY_TYPE"].ToString();
            txtFloation.Text = dtRentInfo.Rows[0]["INFLATION_RATE"].ToString();
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["CONTRACT_TYPE"].ToString()))
            {
                rbContractType.SelectedValue = dtRentInfo.Rows[0]["CONTRACT_TYPE"].ToString();
            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["M_S_PAY_DATE"].ToString()))
            {
                txtMSPayDate.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["M_S_PAY_DATE"].ToString());
            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["M_PAYCYCLE"].ToString()))
            {
                txtMPayCycle.Text = dtRentInfo.Rows[0]["M_PAYCYCLE"].ToString();
            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["M_PAYCYCLE_TYPE"].ToString()))
            {
                ddlMPaymentCycle.SelectedValue = TextLib.StringDecoder(dtRentInfo.Rows[0]["M_PAYCYCLE_TYPE"].ToString());

            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["M_S_USING_DATE"].ToString()))
            {
                txtMSUsingDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["M_S_USING_DATE"].ToString());
            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["M_ISUE_DATE_TYPE"].ToString()))
            {
                ddlMIsueDateType.SelectedValue = dtRentInfo.Rows[0]["M_ISUE_DATE_TYPE"].ToString();
            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["M_ISUE_DATE_ADJUST"].ToString()))
            {
                txtMAdjustDate.Text = dtRentInfo.Rows[0]["M_ISUE_DATE_ADJUST"].ToString();
            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["PAYMENT_TYPE"].ToString()))
            {
                ddlPaymentType.SelectedValue = dtRentInfo.Rows[0]["PAYMENT_TYPE"].ToString();
            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["IS_SPECIAL"].ToString()))
            {
                chkSpecialContract.Checked = dtRentInfo.Rows[0]["IS_SPECIAL"].ToString()=="Y";
            }
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["REMARK"].ToString()))
            {
                txtContRemark.Text = dtRentInfo.Rows[0]["REMARK"].ToString();
            }
        }

        protected void ddlPersonal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPersonal.SelectedValue.Equals(CommValue.TENANTTY_VALUE_PERSONAL))
            {
                ltIssuePlace.Text = TextNm["ISSUEPLACE"];
                ltICPN.Text = TextNm["IDNO"];

                txtRepresent.Text = string.Empty;
                txtRepresent.CssClass = "bgType3";
                txtRepresent.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtPosition.Text = string.Empty;
                txtPosition.CssClass = "bgType3";
                txtPosition.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtTaxCd.CssClass = "bgType3";
                //txtTaxCd.Text = string.Empty;
                //txtTaxCd.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                ltIssuePlace.Text = TextNm["ISSUEOFFICE"];
                ltICPN.Text = TextNm["CERTINCORP"];

                txtRepresent.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtRepresent.CssClass = "bgType2";
                txtPosition.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtPosition.CssClass = "bgType2";
                txtTaxCd.CssClass = "bgType2";
                //txtTaxCd.ReadOnly = CommValue.AUTH_VALUE_FALSE;
            }
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
                txtPerMMRentUSD.Text = string.Empty;
                txtPerMMRentVND.Text = string.Empty;
                txtSumDepositUSDNo.Text = string.Empty;
                txtSumDepositVNDNo.Text = string.Empty;
                txtSumRentUSDNo.Text = string.Empty;
                txtSumRentVNDNo.Text = string.Empty;

                txtExchangeRate.Text = hfExchangeRate.Value;
                txtHfExchangeRate.Text = hfExchangeRate.Value;
                txtLastKeyDt.Text = hfLastKeyDt.Value;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTerm.SelectedValue.Equals(CommValue.TERM_VALUE_LONGTERM))
            {
                // 분양자
                // 월간 임대비 Zero
                // 보증금 Zero
                txtPerMMRentUSD.Text = string.Empty;
                txtPerMMRentUSD.CssClass = "bgType3";
                txtPerMMRentUSD.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtPerMMRentVND.Text = string.Empty;
                txtPerMMRentVND.CssClass = "bgType3";
                txtPerMMRentVND.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtSumDepositUSDNo.Text = string.Empty;
                txtSumDepositUSDNo.CssClass = "bgType3";
                txtSumDepositUSDNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtSumDepositVNDNo.Text = string.Empty;
                txtSumDepositVNDNo.CssClass = "bgType3";
                txtSumDepositVNDNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;

                txtSumRentUSDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtSumRentUSDNo.CssClass = "bgType2";
                txtSumRentVNDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtSumRentVNDNo.CssClass = "bgType2";
            }
            else
            {
                // 분양자
                // 총 임대비 Zero
                txtSumRentUSDNo.Text = string.Empty;
                txtSumRentUSDNo.CssClass = "bgType3";
                txtSumRentUSDNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtSumRentVNDNo.Text = string.Empty;
                txtSumRentVNDNo.CssClass = "bgType3";
                txtSumRentVNDNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;

                txtPerMMRentUSD.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtPerMMRentUSD.CssClass = "bgType2";
                txtPerMMRentVND.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtPerMMRentVND.CssClass = "bgType2";
                txtSumDepositUSDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtSumDepositUSDNo.CssClass = "bgType2";
                txtSumDepositVNDNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtSumDepositVNDNo.CssClass = "bgType2";
            }
        }

        protected void txtSumRentVNDNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSumRentVNDNo.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            {
                // $에서 VND로 변환하는 부분
                txtSumRentUSDNo.Text = ((double.Parse(txtSumRentVNDNo.Text) / double.Parse(txtExchangeRate.Text))).ToString();
            }
            else
            {
                txtSumRentUSDNo.Text = string.Empty;
            }

            txtCoIssueDt.Text = hfCoIssueDt.Value;
            txtCommencingDt.Text = hfCommencingDt.Value;
            txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtExpiringDt.Text = hfExpiringDt.Value;
            txtPlusCondDt.Text = hfPlusCondDt.Value;
            txtLastKeyDt.Text = hfLastKeyDt.Value;
        }

        protected void txtSumRentUSDNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSumRentUSDNo.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            {
                // $에서 VND로 변환하는 부분
                txtSumRentVNDNo.Text = ((double.Parse(txtSumRentUSDNo.Text) * double.Parse(txtExchangeRate.Text))).ToString();
            }
            else
            {
                txtSumRentVNDNo.Text = string.Empty;
            }

            txtCoIssueDt.Text = hfCoIssueDt.Value;
            txtCommencingDt.Text = hfCommencingDt.Value;
            txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtExpiringDt.Text = hfExpiringDt.Value;
            txtPlusCondDt.Text = hfPlusCondDt.Value;
            txtLastKeyDt.Text = hfLastKeyDt.Value;
        }

        protected void txtPerMMRentVND_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPerMMRentVND.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            {
                // $에서 VND로 변환하는 부분
                txtPerMMRentUSD.Text = ((double.Parse(txtPerMMRentVND.Text) / double.Parse(txtExchangeRate.Text))).ToString();
            }
            else
            {
                txtPerMMRentUSD.Text = string.Empty;
            }

            txtCoIssueDt.Text = hfCoIssueDt.Value;
            txtCommencingDt.Text = hfCommencingDt.Value;
            txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtExpiringDt.Text = hfExpiringDt.Value;
            txtPlusCondDt.Text = hfPlusCondDt.Value;
            txtLastKeyDt.Text = hfLastKeyDt.Value;
        }

        protected void txtPerMMRentUSD_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPerMMRentUSD.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            {
                // $에서 VND로 변환하는 부분
                txtPerMMRentVND.Text = ((double.Parse(txtPerMMRentUSD.Text) * double.Parse(txtExchangeRate.Text))).ToString();
            }
            else
            {
                txtPerMMRentVND.Text = string.Empty;
            }

            txtCoIssueDt.Text = hfCoIssueDt.Value;
            txtCommencingDt.Text = hfCommencingDt.Value;
            txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtExpiringDt.Text = hfExpiringDt.Value;
            txtPlusCondDt.Text = hfPlusCondDt.Value;
            txtLastKeyDt.Text = hfLastKeyDt.Value;
        }

        protected void txtSumDepositVNDNo_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSumDepositVNDNo.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            {
                // $에서 VND로 변환하는 부분
                txtSumDepositUSDNo.Text = ((double.Parse(txtSumDepositVNDNo.Text) / double.Parse(txtExchangeRate.Text))).ToString();
            }
            else
            {
                txtSumDepositUSDNo.Text = string.Empty;
            }

            txtCoIssueDt.Text = hfCoIssueDt.Value;
            txtCommencingDt.Text = hfCommencingDt.Value;
            txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtExpiringDt.Text = hfExpiringDt.Value;
            txtPlusCondDt.Text = hfPlusCondDt.Value;
            txtLastKeyDt.Text = hfLastKeyDt.Value;
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

            txtCoIssueDt.Text = hfCoIssueDt.Value;
            txtCommencingDt.Text = hfCommencingDt.Value;
            txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            txtExpiringDt.Text = hfExpiringDt.Value;
            txtPlusCondDt.Text = hfPlusCondDt.Value;
            txtLastKeyDt.Text = hfLastKeyDt.Value;
        }

        protected void txtInitPerMMMngVND_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtInitPerMMMngVND.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            //{
            //    // $에서 VND로 변환하는 부분
            //    txtInitPerMMMngUSD.Text = ((double.Parse(txtInitPerMMMngVND.Text) / double.Parse(txtExchangeRate.Text))).ToString();
            //}
            //else
            //{
            //    txtInitPerMMMngUSD.Text = string.Empty;
            //}

            //txtCoIssueDt.Text = hfCoIssueDt.Value;
            //txtCommencingDt.Text = hfCommencingDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            //txtExpiringDt.Text = hfExpiringDt.Value;
            //txtPlusCondDt.Text = hfPlusCondDt.Value;
            //txtLastKeyDt.Text = hfLastKeyDt.Value;
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

            //txtCoIssueDt.Text = hfCoIssueDt.Value;
            //txtCommencingDt.Text = hfCommencingDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            //txtExpiringDt.Text = hfExpiringDt.Value;
            //txtPlusCondDt.Text = hfPlusCondDt.Value;
            //txtLastKeyDt.Text = hfLastKeyDt.Value;
        }

        protected void txtPerMMMngVND_TextChanged(object sender, EventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtPerMMMngVND.Text) && !string.IsNullOrEmpty(hfExchangeRate.Value))
            //{
            //    // $에서 VND로 변환하는 부분
            //    txtPerMMMngUSD.Text = ((double.Parse(txtPerMMMngVND.Text) / double.Parse(txtExchangeRate.Text))).ToString();
            //}
            //else
            //{
            //    txtPerMMMngUSD.Text = string.Empty;
            //}

            //txtCoIssueDt.Text = hfCoIssueDt.Value;
            //txtCommencingDt.Text = hfCommencingDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            //txtExpiringDt.Text = hfExpiringDt.Value;
            //txtPlusCondDt.Text = hfPlusCondDt.Value;
            //txtLastKeyDt.Text = hfLastKeyDt.Value;
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

            //txtCoIssueDt.Text = hfCoIssueDt.Value;
            //txtCommencingDt.Text = hfCommencingDt.Value;
            //txtInitMMMngDt.Text = hfInitMMMngDt.Value;
            //txtExpiringDt.Text = hfExpiringDt.Value;
            //txtPlusCondDt.Text = hfPlusCondDt.Value;
            //txtLastKeyDt.Text = hfLastKeyDt.Value;
        }

        protected void txtUnitNo_TextChanged(object sender, EventArgs e)
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
                CheckLeasingArea();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void CheckLeasingArea()
        {
            if ((!string.IsNullOrEmpty(txtUnitNo.Text)) && (!string.IsNullOrEmpty(txtFloor.Text)))
            {
                DataTable dtExistReturn = new DataTable();

                // 호실정보 중복 조회
                // KN_USP_RES_SELECT_SALESINFO_S03
                dtExistReturn = ContractMngBlo.WatchExistSalesInfo(Int32.Parse(txtFloor.Text), txtUnitNo.Text);

                if (dtExistReturn != null)
                {
                    if (dtExistReturn.Rows.Count == 0)
                    {
                        DataTable dtReturn = new DataTable();
                        string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "").Replace("/", "");

                        txtLeasingArea.Text = "";

                        // KN_USP_RES_SELECT_ROOMINFO_S05
                        dtReturn = RoomMngBlo.SpreadRoomInfo(ddlContTy.SelectedValue, Int32.Parse(txtFloor.Text), txtUnitNo.Text, strNowDt);

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count > 0)
                            {
                                txtLeasingArea.Text = dtReturn.Rows[0]["LeasingArea"].ToString();
                            }
                        }
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('" + TextNm["UNITNO"] + " : " + txtUnitNo.Text + "\\n" + TextNm["FLOOR"] + " : " + txtFloor.Text + "\\n" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningExistRoomNo", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                        txtFloor.Text = string.Empty;
                        txtUnitNo.Text = string.Empty;
                    }
                }
            }

            txtIssueDt.Text = hfIssueDt.Value;
            txtCoIssueDt.Text = hfCoIssueDt.Value;
            txtCommencingDt.Text = hfCommencingDt.Value;
            txtExpiringDt.Text = hfExpiringDt.Value;
            txtPlusCondDt.Text = hfPlusCondDt.Value;
            txtLastKeyDt.Text = hfLastKeyDt.Value;
        }

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 임대정보
                rsDs.ContractNm = txtTenantNm.Text;
                rsDs.RentCd = ddlContTy.SelectedValue;
                rsDs.RentSeq = Int32.Parse(txtHfParamRentSeq.Text);
                rsDs.RentTy = ddlTerm.SelectedValue;
                rsDs.PersonalCd = ddlPersonal.SelectedValue;
                rsDs.ContractNo = hfContNo.Value;
                rsDs.Addr = txtAddr.Text;
                rsDs.DetailAddr = txtDetAddr.Text;
                rsDs.RssNo = txtICPN.Text;
                rsDs.IssueDt = hfIssueDt.Value.Replace("-", "");
                rsDs.IssuePlace = txtIssuePlace.Text;
                rsDs.TelFontNo = txtTelFrontNo.Text;
                rsDs.TelMidNo = txtTelMidNo.Text;
                rsDs.TelRearNo = txtTelRearNo.Text;
                rsDs.OfficeTelFontNo = txtFAXFrontNo.Text;
                rsDs.OfficeTelMidNo = txtFAXMidNo.Text;
                rsDs.OfficeTelRearNo = txtFAXRearNo.Text;
                rsDs.MobileFrontNo = txtMobileFrontNo.Text;
                rsDs.MobileMidNo = txtMobileMidNo.Text;
                rsDs.MobileRearNo = txtMobileRearNo.Text;
                rsDs.EmailID = txtEmailID.Text;
                rsDs.EmailServer = txtEmailServer.Text;
                rsDs.BankAcc = txtBank.Text;
                rsDs.ConcYn = ddlConcYn.SelectedValue;
                rsDs.RoomNo = txtUnitNo.Text.ToUpper();
                rsDs.FloorNo = txtFloor.Text;
                rsDs.ContDt = hfCommencingDt.Value.Replace("-", "");
                rsDs.ResaleDt = hfExpiringDt.Value.Replace("-", "");
                rsDs.LeasingArea = double.Parse(txtLeasingArea.Text.Replace(",", ""));
                rsDs.DongToDollar = double.Parse(txtExchangeRate.Text.Replace(",", ""));
                rsDs.LastKeyDt = hfLastKeyDt.Value.Replace("-", "");
                rsDs.ListFitFee = hfListFitFeeMng.Value;
                rsDs.ListMngFee = hfListFeeMng.Value;
                if (ddlTerm.SelectedValue.Equals(CommValue.TERM_VALUE_LONGTERM))
                {
                    rsDs.RentalFeeUSDNo = double.Parse(txtSumRentUSDNo.Text.Replace(",", ""));
                    rsDs.RentalFeeVNDNo = double.Parse(txtSumRentVNDNo.Text.Replace(",", ""));
                    rsDs.DepositUSDNo = 0;
                    rsDs.DepositVNDNo = 0;
                }
                else
                {
                    rsDs.RentalFeeUSDNo = double.Parse(txtPerMMRentUSD.Text.Replace(",", ""));
                    rsDs.RentalFeeVNDNo = double.Parse(txtPerMMRentVND.Text.Replace(",", ""));
                    rsDs.DepositUSDNo = double.Parse(txtSumDepositUSDNo.Text.Replace(",", ""));
                    rsDs.DepositVNDNo = double.Parse(txtSumDepositVNDNo.Text.Replace(",", ""));
                }

                if (!string.IsNullOrEmpty(txtInitMMMngDay.Text))
                {
                    rsDs.InitDay = Int32.Parse(txtInitMMMngDay.Text);
                }
                else
                {
                    rsDs.InitDay = CommValue.NUMBER_VALUE_0;
                }

                rsDs.InitMMMngDt = hfInitMMMngDt.Value.Replace("-", "");

                //if (!string.IsNullOrEmpty(txtInitPerMMMngUSD.Text))
                //{
                //    rsDs.InitMMMngUSDNo = double.Parse(txtInitPerMMMngUSD.Text.Replace(",", ""));
                //}
                //else
                //{
                //    rsDs.InitMMMngUSDNo = CommValue.NUMBER_VALUE_0_0;
                //}

                //if (!string.IsNullOrEmpty(txtInitPerMMMngVND.Text))
                //{
                //    rsDs.InitMMMngVNDNo = double.Parse(txtInitPerMMMngVND.Text.Replace(",", ""));
                //}
                //else
                //{
                //    rsDs.InitMMMngVNDNo = CommValue.NUMBER_VALUE_0_0;
                //}

                //rsDs.MMMngUSDNo = double.Parse(txtPerMMMngUSD.Text.Replace(",", ""));
                //rsDs.MMMngVNDNo = double.Parse(txtPerMMMngVND.Text.Replace(",", ""));
                rsDs.TradeNm = txtTradeNm.Text;
                rsDs.Purpose = txtPurpose.Text;
                rsDs.PlusCondDt = hfPlusCondDt.Value.Replace("-", "");
                rsDs.Descript1 = txtPlusCond.Text;
                rsDs.Descript2 = txtMemo.Text;
                rsDs.InsMemIp = Request.ServerVariables["REMOTE_ADDR"];
                rsDs.InsCompNo = Session["CompCd"].ToString();
                rsDs.InsMemNo = Session["MemNo"].ToString();
                rsDs.InsKNMemNo = txtInchage.Text;
                rsDs.CURNCY_TYPE = hfCCtype.Value;
                rsDs.INFLATION_RATE = double.Parse(txtFloation.Text);
                if (rsDs.CURNCY_TYPE != "CC")
                {
                    rsDs.FIXED_DONGTODOLLAR = double.Parse(txtFC.Text);
                }
                rsDs.APPL_YN = hfIsApplyFeeMn.Value;
                rsDs.ListFitFee = hfListFitFeeMng.Value;
                rsDs.ListMngFee = hfListFeeMng.Value;
                rsDs.M_PAYCYCLE = int.Parse(txtMPayCycle.Text == "" ? "0" : txtMPayCycle.Text);
                rsDs.M_PAYCYCLE_TYPE = ddlMPaymentCycle.SelectedValue;
                rsDs.M_S_PAY_DATE = txtMSPayDate.Text.Replace("-", "");
                rsDs.M_S_USING_DATE = txtMSUsingDt.Text.Replace("-", "");
                rsDs.M_ISUE_DATE_TYPE = ddlMIsueDateType.SelectedValue;
                rsDs.CONTRACT_TYPE = rbContractType.SelectedValue;
                rsDs.M_ISUE_DATE_ADJUST = int.Parse(string.IsNullOrEmpty(txtMAdjustDate.Text) ? "0" : txtMAdjustDate.Text);
                rsDs.IS_SPECIAL = chkSpecialContract.Checked ? "Y" : "N";
                rsDs.PAYMENT_TYPE = ddlPaymentType.SelectedValue;
                // 임대구매 정보
                if (!string.IsNullOrEmpty(txtRepresent.Text) ||
                    !string.IsNullOrEmpty(txtPosition.Text) ||
                    !string.IsNullOrEmpty(txtTaxCd.Text))
                {
                    rsComDs.RentCd = ddlContTy.SelectedValue;
                    rsComDs.LegalRep = txtRepresent.Text;
                    rsComDs.Position = txtPosition.Text;
                    rsComDs.TaxCd = txtTaxCd.Text;
                    rsComDs.RentSeq = Int32.Parse(txtHfParamRentSeq.Text);
                }
                else
                {
                    rsComDs = null;
                }

                // 공동소유주 정보
                if (!string.IsNullOrEmpty(txtCoOwnerNm.Text) ||
                    !string.IsNullOrEmpty(txtRelationShip.Text) ||
                    !string.IsNullOrEmpty(txtCoRss.Text) ||
                    !string.IsNullOrEmpty(hfCoIssueDt.Value) ||
                    !string.IsNullOrEmpty(txtCoIssuePlace.Text) ||
                    !string.IsNullOrEmpty(txtCoAddr.Text) ||
                    !string.IsNullOrEmpty(txtCoDetAddr.Text))
                {
                    rsColDs.RentCd = ddlContTy.SelectedValue;
                    rsColDs.CoOwner = txtCoOwnerNm.Text;
                    rsColDs.Relationship = txtRelationShip.Text;
                    rsColDs.CoRssNo = txtCoRss.Text;
                    rsColDs.CoIssueDt = hfCoIssueDt.Value.Replace("-", "");
                    rsColDs.CoIssuePlace = txtCoIssuePlace.Text;
                    rsColDs.CoAddr = txtCoAddr.Text;
                    rsColDs.CoDetailAddr = txtCoDetAddr.Text;
                }
                else
                {
                    rsColDs = null;
                }
                rsDs.RefContractNo = ddlLessor.SelectedValue;
                rsDs.CONTRACT_TYPE = rbContractType.SelectedValue;
                rsDs.SubYn = string.IsNullOrEmpty(rsDs.RefContractNo) ? "N" : "Y";
                rsDs.REMARK = txtContRemark.Text;
                rsDs.RoomNoExt = txtRoomNoExt.Text;
                // KN_USP_RES_UPDATE_SALESINFO_M00
                // KN_USP_RES_UPDATE_SALESCOMPINFO_M00
                // KN_USP_RES_UPDATE_SALESCOINFO_M00
                var objReturn = ContractMngBlo.ModifySalesMngInfo(rsDs, rsColDs, rsComDs);

                if (objReturn == null) return;
                var strAlert = string.Format(AlertNm["INFO_MODIFY_CONT"], hfContNo.Value);
                if (fileUpl.HasFile)
                {
                    try
                    {
                        string conLogconnString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
                        SqlConnection conn = new SqlConnection(conLogconnString);
                        string strCmd = string.Format(@"INSERT INTO ContractPDF ([RentCD],[RentSeq],[FileName],[FilePath],[Status]) VALUES('{0}','{1}','{2}','{3}','0')"
                            , rsDs.RentCd, rsDs.RentSeq, rsDs.ContractNo + ".PDF", "//ContractPDF//" + rsDs.ContractNo + ".PDF");

                        SqlCommand cmd = new SqlCommand(strCmd, conn);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        cmd.ExecuteNonQuery();
                        string pdfFile = Path.GetFileName(rsDs.ContractNo + ".PDF");

                        fileUpl.SaveAs(Server.MapPath("~//ContractPDF//") + pdfFile);

                        //mo connection insert vao bang pdf contract

                    }
                    catch (Exception ex)
                    {
                        string strErr = ex.Message;
                    }
                }
                var sbAlert = new StringBuilder();

                sbAlert.Append("alert('");
                sbAlert.Append(strAlert);
                sbAlert.Append("');");
                sbAlert.Append("document.location.href=\"" + Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "\";");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbAlert.ToString(), CommValue.AUTH_VALUE_TRUE);
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
            diplayFitOutFee.InnerHtml = listfitOutFee.ToString();
        }

        public void LoadMngFee(DataTable dsSet)
        {
            var listMngFee = new StringBuilder();

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
                listMngFee.Append(fee);
            }
            diplayMngFee.InnerHtml = listMngFee.ToString();
        }


        protected string FormatDateTime(string dateTime)
        {
            if (String.IsNullOrEmpty(dateTime)) return "";
            string date = dateTime.Substring(0, 4) + "-" + dateTime.Substring(4, 2) + "-" + dateTime.Substring(6, 2);
            return date;
        }

        protected void isApplyFeeMn_CheckedChanged(object sender, EventArgs e)
        {
            ListFitOutFee.Visible = !isApplyFeeMn.Checked;
        }

        protected void imgDeleteFee_Click(object sender, ImageClickEventArgs e)
        {
            var feeSeq = hfFeeSeqDel.Value;
            var contractNo = txtContNo.Text;
            object[] objReturn = new object[2];
            objReturn = ContractMngBlo.DeleteMngInfo(Int32.Parse(feeSeq), contractNo); 
        }
        private void LoadLessor()
        {
            ddlLessor.Items.Clear();
            var dtReturn = ContractMngBlo.SpreadSalesInfoExcelView(txtHfRentCd.Text, "", "", Session["LangCd"].ToString());
            ddlLessor.Items.Add(new ListItem("Choose Lessor", ""));
            foreach (var dr in dtReturn.Select())
            {
                ddlLessor.Items.Add(new ListItem(dr["User Name"].ToString(), dr["RoomNo"].ToString()));
            }

            ddlLessor.SelectedValue = "";
        }

        protected void imgbtnCheckRoom_Click(object sender, ImageClickEventArgs e)
        {
            CheckLeasingArea();
        }
    }
}