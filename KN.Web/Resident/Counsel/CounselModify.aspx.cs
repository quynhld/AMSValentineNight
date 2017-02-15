using System;
using System.Web.UI.WebControls;
using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Resident.Biz;
using KN.Resident.Ent;

namespace KN.Web.Resident.Counsel
{
    public partial class CounselModify : BasePage
    {
        private const string START_YEAR = "1997";
        private const string START_MONTH = "1";
        private const string COMMENCE_START_YEAR = "2011";
        private const string COMMENCE_START_MONTH = "7";

        CounselMngDs.CounselInfo counselInfo = new CounselMngDs.CounselInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfCounselCd.Text, CommValue.AUTH_VALUE_FALSE);
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

            if (Request.Params["CounselCd"] != null && Request.Params["CounselSeq"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["CounselCd"].ToString()) &&
                    !string.IsNullOrEmpty(Request.Params["CounselSeq"].ToString()))
                {
                    txtHfCounselCd.Text = Request.Params["CounselCd"].ToString();
                    txtHfCounselSeq.Text = Request.Params["CounselSeq"].ToString();
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            // 각 레이블별 명명
            ltTitleCompAddr.Text = TextNm["COMPADDR"];
            ltTitleIndus.Text = TextNm["INDUS"];
            ltTitleCountry.Text = TextNm["COUNTRY"];
            ltTitleCompNm.Text = TextNm["COMPNM"];
            ltTitleGrade.Text = TextNm["GRADE"];
            ltTitleCompTy.Text = TextNm["COMPTYPE"];
            ltTitleChargerNm.Text = TextNm["CHARGENM"];
            ltTitleTelNo.Text = TextNm["TEL"];
            ltTitleFaxNo.Text = TextNm["FAX"];
            ltTitleEmail.Text = TextNm["EMAIL"];
            ltTitleCounsultTitle1.Text = TextNm["COUNSULT_MNG_1_TITLE"];
            ltTitleUseArea.Text = TextNm["USEAREA"];
            ltTitleCurrRental.Text = TextNm["CURRRENTAL"];
            ltTitleCurrMng.Text = TextNm["CURRMNG"];
            ltTitleCurrFare.Text = TextNm["CURRFEE"];
            ltTitleStaffNo.Text = TextNm["STAFFNO"];
            ltTitleCommecingYear.Text = TextNm["COMMECINGYEAR"];
            ltTitleContPeriod.Text = TextNm["CONTPERIOD"];
            ltTitleCurrParkingNo.Text = TextNm["CURRPARKINGNO"];
            ltTitleCar.Text = TextNm["CAR"];
            ltTitleMotorBike.Text = TextNm["MOTORBIKE"];
            ltTitleReasonTrans.Text = TextNm["REASONTRANS"];
            ltTitleBudgetTrans.Text = TextNm["BUDGETTRANS"];
            ltTitleCounsultTitle2.Text = TextNm["COUNSULT_MNG_2_TITLE"];
            ltTitleLeaseAreaInNeed.Text = TextNm["LEASINGAREAINNEED"];
            ltTitleExpectedRentals.Text = TextNm["EXPECTEDRENTAL"];
            ltTitleFavorateFloor.Text = TextNm["FAVORATE"] + " " + TextNm["FLOOR"];
            ltTitleFavorateDir.Text = TextNm["FAVORATE"] + " " + TextNm["DIRECT"];
            ltTitleCompBudget.Text = TextNm["COMPBUDGET"];
            ltTitleExpectedPeriod.Text = TextNm["EXPECTEDPERIOD"];
            ltTitlePossibleLeasePeriod.Text = TextNm["POSSIBLELEASEPERIOD"];
            ltTitleFinalDecision.Text = TextNm["FINALDECISION"];
            ltTitleNeedParkingNo.Text = TextNm["NEEDPARKINGNO"];
            ltTitleInternalConst.Text = TextNm["INTERNALCONST"];
            ltTitleMovingLocate.Text = TextNm["MOVINGLOCATE"];
            ltTitleRemark.Text = TextNm["REMARK"];

            ltTitleSMeter1.Text = TextNm["SMETER"];
            ltTitleSMeter2.Text = TextNm["SMETER"];
            ltTitleDollar1.Text = TextNm["DOLLAR"];
            ltTitleDollar2.Text = TextNm["DOLLAR"];
            ltTitleDollar3.Text = TextNm["DOLLAR"];
            ltTitleDollar4.Text = TextNm["DOLLAR"];
            ltTitleDollar5.Text = TextNm["DOLLAR"];
            ltTitleYears1.Text = TextNm["YEARS"];
            ltTitleYears3.Text = TextNm["YEARS"];
            ltTitleMonth2.Text = TextNm["MONTHS"];
            ltTitleDays.Text = TextNm["DAYS"];

            // 각 컨트롤별 세팅
            // 업종
            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlIndus, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_BUSINESS);

            // 국적
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlCountry, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_COUNTRY);

            // 기업타입
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlCompTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_COMP_TY);

            // 사용면적
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlUseArea, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_LEASE_AREA);

            // 현 임대료
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlCurrRental, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_RENTALFEE);

            // 서비스 사용료
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlCurrFare, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_SERVICEFARE);

            // 직원수
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlStaffNo, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_STAFFNO);

            // 계약 개시년도
            MakeDdlCommencingYear();

            // 계약기간
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlContPeriod, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_CONT_PERIOD);

            // 주차대수 - 자동차
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlCar, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_NO_PARKING_CAR);

            // 주차대수 - 오토바이
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlMotorBike, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_NO_PARKING_BIKE);

            // 필요면적
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlLeaseAreaInNeed, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_LEASE_AREA);

            // 희망 임대료
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlExpectedRentals, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_RENTALFEE);

            // 희망 임대 기간
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlExpectedPeriod, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_LEASE_PERIOD);

            // 임차 가능시기
            MakeCommenceStartYear();
            MakeCommenceStartMonth(COMMENCE_START_MONTH);

            // 최종 결정 시간
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlFinalDecision, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_PERIOD);

            // 예상 내부 공사기간
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlInternalConst, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_CONSTRUCT_PERIOD);

            // 이전 고려 위치
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlMovingLocate, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_REGION);

            // 자바스크립트 세팅
            txtTelNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelMidNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtFaxNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFaxFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFaxMidNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFaxRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtEtcUseArea.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcCurrRental.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCurrMng.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcCurrFare.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcStaffNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcContPeriod.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcCar.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcMotorBike.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtBudgetTrans.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcLeaseAreaInNeed.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcExpectedRentals.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompBudgert.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcExpectedPeriod.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcFinalDecision.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtEtcInternalConst.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtNeedParkingNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.Visible = Master.isModDelAuthOk;
            lnkbtnModify.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + CommValue.COMMCD_VALUE_ETC + "');";
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POSTMODIFY"] + "')";
        }

        protected void LoadData()
        {
            if (!string.IsNullOrEmpty(txtHfCounselSeq.Text))
            {
                DataTable dtReturn = new DataTable();

                // KN_USP_RES_SELECT_COUNSELINFO_S02
                dtReturn = CounselMngBlo.WatchCounselDetailView(txtHfCounselCd.Text, Int32.Parse(txtHfCounselSeq.Text), Session["MemAuthTy"].ToString(), Session["CompCd"].ToString(), Session["MemNo"].ToString(), Session["LangCd"].ToString());

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        txtCompAddr.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CompAddr"].ToString());
                        txtCompDetAddr.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CompDetAddr"].ToString());

                        if (dtReturn.Rows[0]["IndustryCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlIndus.SelectedValue = dtReturn.Rows[0]["IndustryCd"].ToString();
                            txtEtcIndus.Text = TextLib.StringDecoder(dtReturn.Rows[0]["IndustryEtcNm"].ToString());
                            txtEtcIndus.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcIndus.ReadOnly = CommValue.AUTH_VALUE_FALSE;

                        }
                        else
                        {
                            ddlIndus.SelectedValue = dtReturn.Rows[0]["IndustryCd"].ToString();
                            txtEtcIndus.Text = dtReturn.Rows[0]["IndustryEtcNm"].ToString();
                            txtEtcIndus.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcIndus.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        if (dtReturn.Rows[0]["CountryCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlCountry.SelectedValue = dtReturn.Rows[0]["CountryCd"].ToString();
                            txtEtcCountry.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CountryEtcNm"].ToString());
                            txtEtcCountry.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCountry.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlCountry.SelectedValue = dtReturn.Rows[0]["CountryCd"].ToString();
                            txtEtcCountry.Text = dtReturn.Rows[0]["CountryEtcNm"].ToString();
                            txtEtcCountry.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCountry.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        txtCompNm.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CompNm"].ToString());
                        txtGrade.Text = dtReturn.Rows[0]["CompGrade"].ToString();

                        if (dtReturn.Rows[0]["CompTyCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlCompTy.SelectedValue = dtReturn.Rows[0]["CompTyCd"].ToString();
                            txtEtcCompTy.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CompEtcTyNm"].ToString());
                            txtEtcCompTy.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCompTy.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlCompTy.SelectedValue = dtReturn.Rows[0]["CompTyCd"].ToString();
                            txtEtcCompTy.Text = dtReturn.Rows[0]["CompEtcTyNm"].ToString();
                            txtEtcCompTy.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCompTy.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        txtChargerNm.Text = TextLib.StringDecoder(dtReturn.Rows[0]["ChargerNm"].ToString());
                        txtTelNo.Text = dtReturn.Rows[0]["CompTelNo"].ToString();
                        txtTelFrontNo.Text = dtReturn.Rows[0]["CompTelFrontNo"].ToString();
                        txtTelMidNo.Text = dtReturn.Rows[0]["CompTelMidNo"].ToString();
                        txtTelRearNo.Text = dtReturn.Rows[0]["CompTelRearNo"].ToString();
                        txtFaxNo.Text = dtReturn.Rows[0]["CompFaxNo"].ToString();
                        txtFaxFrontNo.Text = dtReturn.Rows[0]["CompFaxFrontNo"].ToString();
                        txtFaxMidNo.Text = dtReturn.Rows[0]["CompFaxMidNo"].ToString();
                        txtFaxRearNo.Text = dtReturn.Rows[0]["CompFaxRearNo"].ToString();
                        txtEmailID.Text = TextLib.StringDecoder(dtReturn.Rows[0]["EmailID"].ToString());
                        txtEmailServer.Text = TextLib.StringDecoder(dtReturn.Rows[0]["EmailServer"].ToString());

                        if (dtReturn.Rows[0]["UsingAreaCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlUseArea.SelectedValue = dtReturn.Rows[0]["UsingAreaCd"].ToString();
                            txtEtcUseArea.Text = TextLib.StringDecoder(dtReturn.Rows[0]["UsingAreaEtcNm"].ToString());
                            txtEtcUseArea.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcUseArea.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlUseArea.SelectedValue = dtReturn.Rows[0]["UsingAreaCd"].ToString();
                            txtEtcUseArea.Text = dtReturn.Rows[0]["UsingAreaEtcNm"].ToString();
                            txtEtcUseArea.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcUseArea.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        if (dtReturn.Rows[0]["CurrRentalCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlCurrRental.SelectedValue = dtReturn.Rows[0]["CurrRentalCd"].ToString();
                            txtEtcCurrRental.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CurrRentalEtcNm"].ToString());
                            txtEtcCurrRental.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCurrRental.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlCurrRental.SelectedValue = dtReturn.Rows[0]["CurrRentalCd"].ToString();
                            txtEtcCurrRental.Text = dtReturn.Rows[0]["CurrRentalEtcNm"].ToString();
                            txtEtcCurrRental.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCurrRental.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        txtCurrMng.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CurrMngFee"].ToString());

                        if (dtReturn.Rows[0]["CurrServiceFareCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlCurrFare.SelectedValue = dtReturn.Rows[0]["CurrServiceFareCd"].ToString();
                            txtEtcCurrFare.Text = dtReturn.Rows[0]["CurrServiceFareEtcNm"].ToString();
                            txtEtcCurrFare.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCurrFare.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlCurrFare.SelectedValue = dtReturn.Rows[0]["CurrServiceFareCd"].ToString();
                            txtEtcCurrFare.Text = dtReturn.Rows[0]["CurrServiceFareEtcNm"].ToString();
                            txtEtcCurrFare.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCurrFare.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        if (dtReturn.Rows[0]["StaffNo"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlStaffNo.SelectedValue = dtReturn.Rows[0]["StaffNo"].ToString();
                            txtEtcStaffNo.Text = dtReturn.Rows[0]["StaffNoEtc"].ToString();
                            txtEtcStaffNo.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcStaffNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlStaffNo.SelectedValue = dtReturn.Rows[0]["StaffNo"].ToString();
                            txtEtcStaffNo.Text = dtReturn.Rows[0]["StaffNoEtc"].ToString();
                            txtEtcStaffNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcStaffNo.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        ddlCommecingYear.SelectedValue = dtReturn.Rows[0]["ContCommeceYear"].ToString();

                        if (dtReturn.Rows[0]["ContPeriodCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlContPeriod.SelectedValue = dtReturn.Rows[0]["ContPeriodCd"].ToString();
                            txtEtcContPeriod.Text = dtReturn.Rows[0]["ContPeriodEtcNm"].ToString();
                            txtEtcContPeriod.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcContPeriod.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlContPeriod.SelectedValue = dtReturn.Rows[0]["ContPeriodCd"].ToString();
                            txtEtcContPeriod.Text = dtReturn.Rows[0]["ContPeriodEtcNm"].ToString();
                            txtEtcContPeriod.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcContPeriod.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        if (dtReturn.Rows[0]["CarNoCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlCar.SelectedValue = dtReturn.Rows[0]["CarNoCd"].ToString();
                            txtEtcCar.Text = dtReturn.Rows[0]["CarNoEtcNm"].ToString();
                            txtEtcCar.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCar.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlCar.SelectedValue = dtReturn.Rows[0]["CarNoCd"].ToString();
                            txtEtcCar.Text = dtReturn.Rows[0]["CarNoEtcNm"].ToString();
                            txtEtcCar.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcCar.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        if (dtReturn.Rows[0]["MotoBikeNoCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlMotorBike.SelectedValue = dtReturn.Rows[0]["MotoBikeNoCd"].ToString();
                            txtEtcMotorBike.Text = dtReturn.Rows[0]["MotoBikeNoEtcNm"].ToString();
                            txtEtcMotorBike.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcMotorBike.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlMotorBike.SelectedValue = dtReturn.Rows[0]["MotoBikeNoCd"].ToString();
                            txtEtcMotorBike.Text = dtReturn.Rows[0]["MotoBikeNoEtcNm"].ToString();
                            txtEtcMotorBike.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcMotorBike.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        txtReasonTrans.Text = TextLib.StringDecoder(dtReturn.Rows[0]["TransferPlanReason"].ToString());
                        txtBudgetTrans.Text = dtReturn.Rows[0]["TransferCostBuget"].ToString();

                        if (dtReturn.Rows[0]["LeaseAreaCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlLeaseAreaInNeed.SelectedValue = dtReturn.Rows[0]["LeaseAreaCd"].ToString();
                            txtEtcLeaseAreaInNeed.Text = dtReturn.Rows[0]["LeaseAreaEtcNm"].ToString();
                            txtEtcLeaseAreaInNeed.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcLeaseAreaInNeed.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlLeaseAreaInNeed.SelectedValue = dtReturn.Rows[0]["LeaseAreaCd"].ToString();
                            txtEtcLeaseAreaInNeed.Text = dtReturn.Rows[0]["LeaseAreaEtcNm"].ToString();
                            txtEtcLeaseAreaInNeed.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcLeaseAreaInNeed.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        if (dtReturn.Rows[0]["ExpectedRentalCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlExpectedRentals.SelectedValue = dtReturn.Rows[0]["ExpectedRentalCd"].ToString();
                            txtEtcExpectedRentals.Text = dtReturn.Rows[0]["ExpectedRentalEtcNm"].ToString();
                            txtEtcExpectedRentals.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcExpectedRentals.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlExpectedRentals.SelectedValue = dtReturn.Rows[0]["ExpectedRentalCd"].ToString();
                            txtEtcExpectedRentals.Text = dtReturn.Rows[0]["ExpectedRentalEtcNm"].ToString();
                            txtEtcExpectedRentals.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcExpectedRentals.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        txtFavorateFloor.Text = dtReturn.Rows[0]["FavorateFloor"].ToString();
                        txtFavorateDir.Text = TextLib.StringDecoder(dtReturn.Rows[0]["FavorateDir"].ToString());
                        txtCompBudgert.Text = dtReturn.Rows[0]["CompBudget"].ToString();

                        if (dtReturn.Rows[0]["ExpectedlPeriodCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlExpectedPeriod.SelectedValue = dtReturn.Rows[0]["ExpectedlPeriodCd"].ToString();
                            txtEtcExpectedPeriod.Text = dtReturn.Rows[0]["ExpectedlPeriodEtcNm"].ToString();
                            txtEtcExpectedPeriod.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcExpectedPeriod.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlExpectedPeriod.SelectedValue = dtReturn.Rows[0]["ExpectedlPeriodCd"].ToString();
                            txtEtcExpectedPeriod.Text = dtReturn.Rows[0]["ExpectedlPeriodEtcNm"].ToString();
                            txtEtcExpectedPeriod.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcExpectedPeriod.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        ddlYear.SelectedValue = dtReturn.Rows[0]["LeaseYear"].ToString();
                        ddlMonth.SelectedValue = dtReturn.Rows[0]["LeaseMonth"].ToString();

                        if (dtReturn.Rows[0]["DecisionCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlFinalDecision.SelectedValue = dtReturn.Rows[0]["DecisionCd"].ToString();
                            txtEtcFinalDecision.Text = dtReturn.Rows[0]["DecisionEtcNm"].ToString();
                            txtEtcFinalDecision.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcFinalDecision.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlFinalDecision.SelectedValue = dtReturn.Rows[0]["DecisionCd"].ToString();
                            txtEtcFinalDecision.Text = dtReturn.Rows[0]["DecisionEtcNm"].ToString();
                            txtEtcFinalDecision.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcFinalDecision.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        txtNeedParkingNo.Text = dtReturn.Rows[0]["NeedParkNo"].ToString();

                        if (dtReturn.Rows[0]["InternalConstCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlInternalConst.SelectedValue = dtReturn.Rows[0]["InternalConstCd"].ToString();
                            txtEtcInternalConst.Text = dtReturn.Rows[0]["InternalConstEtcNm"].ToString();
                            txtEtcInternalConst.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcInternalConst.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlInternalConst.SelectedValue = dtReturn.Rows[0]["InternalConstCd"].ToString();
                            txtEtcInternalConst.Text = dtReturn.Rows[0]["InternalConstEtcNm"].ToString();
                            txtEtcInternalConst.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcInternalConst.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        if (dtReturn.Rows[0]["MovingLocationCd"].ToString().Equals(CommValue.COMMCD_VALUE_ETC))
                        {
                            ddlMovingLocate.SelectedValue = dtReturn.Rows[0]["MovingLocationCd"].ToString();
                            txtEtcMovingLocate.Text = TextLib.StringDecoder(dtReturn.Rows[0]["MovingLocationEtcNm"].ToString());
                            txtEtcMovingLocate.Enabled = CommValue.AUTH_VALUE_TRUE;
                            txtEtcMovingLocate.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            ddlMovingLocate.SelectedValue = dtReturn.Rows[0]["MovingLocationCd"].ToString();
                            txtEtcMovingLocate.Text = dtReturn.Rows[0]["MovingLocationEtcNm"].ToString();
                            txtEtcMovingLocate.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                            txtEtcMovingLocate.Enabled = CommValue.AUTH_VALUE_FALSE;
                        }

                        txtRemark.Text = TextLib.StringDecoder(dtReturn.Rows[0]["Remark"].ToString());
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfCounselCd.Text, CommValue.AUTH_VALUE_FALSE);
                    }
                }
                else
                {
                    Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfCounselCd.Text, CommValue.AUTH_VALUE_FALSE);
                }
            }
        }

        protected void MakeDdlCommencingYear()
        {
            int intNowYear = Int32.Parse(DateTime.Now.Year.ToString());

            ddlCommecingYear.Items.Clear();

            for (int intTmpI = Int32.Parse(START_YEAR); intTmpI <= intNowYear; intTmpI++)
            {
                ddlCommecingYear.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }
        }

        protected void MakeCommenceStartYear()
        {
            int intEndYear = Int32.Parse(DateTime.Now.AddYears(2).Year.ToString());

            ddlYear.Items.Clear();

            for (int intTmpI = Int32.Parse(COMMENCE_START_YEAR); intTmpI <= intEndYear; intTmpI++)
            {
                ddlYear.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }
        }

        protected void MakeCommenceStartMonth(string strStartMonth)
        {
            ddlMonth.Items.Clear();

            for (int intTmpI = Int32.Parse(strStartMonth); intTmpI <= 12; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString().PadLeft(2, '0'), intTmpI.ToString().PadLeft(2, '0')));
            }
        }

        protected void MakeCounselInfoObj()
        {
            // Agreement Information
            counselInfo.CompAddr = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtCompAddr.Text));
            counselInfo.CompDetAddr = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtCompDetAddr.Text));
            counselInfo.IndustryCd = ddlIndus.SelectedValue;
            counselInfo.IndustryEtcNm = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtEtcIndus.Text));
            counselInfo.CountryCd = ddlCountry.SelectedValue;
            counselInfo.CountryEtcNm = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtEtcCountry.Text));
            counselInfo.CompTyCd = ddlCompTy.SelectedValue;
            counselInfo.CompEtcTyNm = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtEtcCompTy.Text));
            counselInfo.CompNm = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtCompNm.Text));
            counselInfo.CompGrade = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtGrade.Text));
            counselInfo.ChargerNm = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtChargerNm.Text));
            counselInfo.CompTelNo = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtTelNo.Text));
            counselInfo.CompTelFrontNo = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtTelFrontNo.Text));
            counselInfo.CompTelMidNo = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtTelMidNo.Text));
            counselInfo.CompTelRearNo = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtTelRearNo.Text));
            counselInfo.CompFaxNo = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtFaxNo.Text));
            counselInfo.CompFaxFrontNo = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtFaxFrontNo.Text));
            counselInfo.CompFaxMidNo = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtFaxMidNo.Text));
            counselInfo.CompFaxRearNo = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtFaxRearNo.Text));
            counselInfo.EmailID = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtEmailID.Text));
            counselInfo.EmailServer = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtEmailServer.Text));

            // Office Information
            counselInfo.UsingAreaCd = ddlUseArea.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcUseArea.Text))
            {
                counselInfo.UsingAreaEtcNm = Int32.Parse(txtEtcUseArea.Text);
            }
            counselInfo.CurrRentalCd = ddlCurrRental.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcCurrRental.Text))
            {
                counselInfo.CurrRentalEtcNm = double.Parse(txtEtcCurrRental.Text);
            }
            if (!string.IsNullOrEmpty(txtCurrMng.Text))
            {
                counselInfo.CurrMngFee = double.Parse(txtCurrMng.Text);
            }
            counselInfo.CurrServiceFareCd = ddlCurrFare.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcCurrFare.Text))
            {
                counselInfo.CurrServiceFareEtcNm = double.Parse(txtEtcCurrFare.Text);
            }
            counselInfo.StaffNo = ddlStaffNo.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcStaffNo.Text))
            {
                counselInfo.StaffNoEtc = Int32.Parse(txtEtcStaffNo.Text);
            }
            counselInfo.ContCommeceYear = ddlCommecingYear.SelectedValue;
            counselInfo.ContPeriodCd = ddlContPeriod.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcContPeriod.Text))
            {
                counselInfo.ContPeriodEtcNm = Int32.Parse(txtEtcContPeriod.Text);
            }
            counselInfo.CarNoCd = ddlCar.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcCar.Text))
            {
                counselInfo.CarNoEtcNm = Int32.Parse(txtEtcCar.Text);
            }
            counselInfo.MotoBikeNoCd = ddlMotorBike.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcMotorBike.Text))
            {
                counselInfo.MotoBikeNoEtcNm = Int32.Parse(txtEtcMotorBike.Text);
            }
            if (!string.IsNullOrEmpty(txtReasonTrans.Text))
            {
                counselInfo.TransferPlanReason = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtReasonTrans.Text));
            }
            if (!string.IsNullOrEmpty(txtBudgetTrans.Text))
            {
                counselInfo.TransferCostBuget = Int32.Parse(txtBudgetTrans.Text);
            }

            // Requirement Information
            counselInfo.LeaseAreaCd = ddlLeaseAreaInNeed.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcLeaseAreaInNeed.Text))
            {
                counselInfo.LeaseAreaEtcNm = Int32.Parse(txtEtcLeaseAreaInNeed.Text);
            }
            counselInfo.ExpectedlPeriodCd = ddlExpectedPeriod.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcExpectedPeriod.Text))
            {
                counselInfo.ExpectedlPeriodEtcNm = Int32.Parse(txtEtcExpectedPeriod.Text);
            }
            counselInfo.FavorateFloor = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtFavorateFloor.Text));
            counselInfo.FavorateDirection = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtFavorateDir.Text));
            if (!string.IsNullOrEmpty(txtCompBudgert.Text))
            {
                counselInfo.CompBudget = Int32.Parse(txtCompBudgert.Text);
            }
            counselInfo.ExpectedRentalCd = ddlExpectedRentals.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcExpectedRentals.Text))
            {
                counselInfo.ExpectedRentalEtcNm = Int32.Parse(txtEtcExpectedRentals.Text);
            }
            counselInfo.LeaseYear = ddlYear.SelectedValue;
            counselInfo.LeaseMonth = ddlMonth.SelectedValue;
            counselInfo.DecisionCd = ddlFinalDecision.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcFinalDecision.Text))
            {
                counselInfo.DecisionEtcNm = Int32.Parse(txtEtcFinalDecision.Text);
            }
            if (!string.IsNullOrEmpty(txtNeedParkingNo.Text))
            {
                counselInfo.NeedParkNo = Int32.Parse(txtNeedParkingNo.Text);
            }
            counselInfo.InternalConstCd = ddlInternalConst.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcInternalConst.Text))
            {
                counselInfo.InternalConstEtcNm = Int32.Parse(txtEtcInternalConst.Text);
            }
            counselInfo.MovingLocationCd = ddlMovingLocate.SelectedValue;
            if (!string.IsNullOrEmpty(txtEtcMovingLocate.Text))
            {
                counselInfo.MovingLocationEtcNm = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtEtcMovingLocate.Text));
            }
            counselInfo.Remark = TextLib.StringEncoder(TextLib.MakeNullToEmpty(txtRemark.Text));
            counselInfo.CounselCd = TextLib.MakeNullToEmpty(txtHfCounselCd.Text);

            if (Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
            {
                counselInfo.ReadAuth = Session["MemAuthTy"].ToString();
                counselInfo.WriteAuth = Session["MemAuthTy"].ToString();
                counselInfo.ModDelAuth = Session["MemAuthTy"].ToString();
            }
            else
            {
                counselInfo.ReadAuth = (Int32.Parse(Session["MemAuthTy"].ToString()) - 1).ToString().PadLeft(8, '0');
                counselInfo.WriteAuth = (Int32.Parse(Session["MemAuthTy"].ToString()) - 1).ToString().PadLeft(8, '0');
                counselInfo.ModDelAuth = (Int32.Parse(Session["MemAuthTy"].ToString()) - 1).ToString().PadLeft(8, '0');
            }

            if (!string.IsNullOrEmpty(txtHfCounselSeq.Text))
            {
                counselInfo.CounselSeq = Int32.Parse(txtHfCounselSeq.Text);
            }
            else
            {
                counselInfo.CounselSeq = 0;
            }

            counselInfo.ModCompNo = Session["CompCd"].ToString();
            counselInfo.ModMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            counselInfo.ModMemNo = Session["MemNo"].ToString();
        }

        #region 각 이벤트 처리

        protected void ddlIndus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIndus.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcIndus.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcIndus.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcIndus.Text = string.Empty;
            }
            else
            {
                txtEtcIndus.Text = string.Empty;
                txtEtcIndus.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcIndus.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCountry.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcCountry.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcCountry.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcCountry.Text = string.Empty;
            }
            else
            {
                txtEtcCountry.Text = string.Empty;
                txtEtcCountry.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcCountry.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlCompTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCompTy.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcCompTy.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcCompTy.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcCompTy.Text = string.Empty;
            }
            else
            {
                txtEtcCompTy.Text = string.Empty;
                txtEtcCompTy.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcCompTy.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlUseArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUseArea.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcUseArea.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcUseArea.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcUseArea.Text = string.Empty;
            }
            else
            {
                txtEtcUseArea.Text = string.Empty;
                txtEtcUseArea.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcUseArea.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlCurrRental_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrRental.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcCurrRental.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcCurrRental.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcCurrRental.Text = string.Empty;
            }
            else
            {
                txtEtcCurrRental.Text = string.Empty;
                txtEtcCurrRental.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcCurrRental.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlCurrFare_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCurrFare.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcCurrFare.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcCurrFare.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcCurrFare.Text = string.Empty;
            }
            else
            {
                txtEtcCurrFare.Text = string.Empty;
                txtEtcCurrFare.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcCurrFare.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlStaffNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStaffNo.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcStaffNo.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcStaffNo.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcStaffNo.Text = string.Empty;
            }
            else
            {

                txtEtcStaffNo.Text = string.Empty;
                txtEtcStaffNo.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcStaffNo.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlContPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlContPeriod.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcContPeriod.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcContPeriod.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcContPeriod.Text = string.Empty;
            }
            else
            {
                txtEtcContPeriod.Text = string.Empty;
                txtEtcContPeriod.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcContPeriod.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCar.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcCar.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcCar.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcCar.Text = string.Empty;
            }
            else
            {
                txtEtcCar.Text = string.Empty;
                txtEtcCar.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcCar.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlMotorBike_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMotorBike.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcMotorBike.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcMotorBike.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcMotorBike.Text = string.Empty;
            }
            else
            {
                txtEtcMotorBike.Text = string.Empty;
                txtEtcMotorBike.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcMotorBike.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlLeaseAreaInNeed_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLeaseAreaInNeed.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcLeaseAreaInNeed.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcLeaseAreaInNeed.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcLeaseAreaInNeed.Text = string.Empty;
            }
            else
            {
                txtEtcLeaseAreaInNeed.Text = string.Empty;
                txtEtcLeaseAreaInNeed.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcLeaseAreaInNeed.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlExpectedRentals_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExpectedRentals.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcExpectedRentals.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcExpectedRentals.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcExpectedRentals.Text = string.Empty;
            }
            else
            {
                txtEtcExpectedRentals.Text = string.Empty;
                txtEtcExpectedRentals.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcExpectedRentals.Enabled = CommValue.AUTH_VALUE_FALSE;
            }

        }

        protected void ddlExpectedPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlExpectedPeriod.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcExpectedPeriod.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcExpectedPeriod.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcExpectedPeriod.Text = string.Empty;
            }
            else
            {
                txtEtcExpectedPeriod.Text = string.Empty;
                txtEtcExpectedPeriod.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcExpectedPeriod.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedValue.Equals(COMMENCE_START_YEAR))
            {
                MakeCommenceStartMonth(COMMENCE_START_MONTH);
            }
            else
            {
                MakeCommenceStartMonth(START_MONTH);
            }
        }

        protected void ddlFinalDecision_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlFinalDecision.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcFinalDecision.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcFinalDecision.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcFinalDecision.Text = string.Empty;
            }
            else
            {
                txtEtcFinalDecision.Text = string.Empty;
                txtEtcFinalDecision.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcFinalDecision.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlInternalConst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlInternalConst.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcInternalConst.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcInternalConst.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcInternalConst.Text = string.Empty;
            }
            else
            {
                txtEtcInternalConst.Text = string.Empty;
                txtEtcInternalConst.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcInternalConst.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        protected void ddlMovingLocate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMovingLocate.SelectedValue.Equals(CommValue.COMMCD_VALUE_ETC))
            {
                txtEtcMovingLocate.Enabled = CommValue.AUTH_VALUE_TRUE;
                txtEtcMovingLocate.ReadOnly = CommValue.AUTH_VALUE_FALSE;
                txtEtcMovingLocate.Text = string.Empty;
            }
            else
            {
                txtEtcMovingLocate.Text = string.Empty;
                txtEtcMovingLocate.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                txtEtcMovingLocate.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        #endregion

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                MakeCounselInfoObj();

                // KN_USP_RES_UPDATE_COUNSELINFO_M00
                // KN_USP_RES_UPDATE_COUNSELADDON_M00
                CounselMngBlo.ModifyCounselInfo(counselInfo);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfCounselCd.Text, CommValue.AUTH_VALUE_FALSE);
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

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfCounselCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}