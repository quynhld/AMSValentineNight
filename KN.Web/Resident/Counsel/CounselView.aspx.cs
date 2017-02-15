using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Resident.Biz;

namespace KN.Web.Resident.Counsel
{
    public partial class CounselView : BasePage
    {
        private string strCounselCd = string.Empty;
        private string strCounselSeq = string.Empty;

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
                    else
                    {
                        Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + strCounselCd, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        protected bool CheckParams()
        {
            bool isParamOk = CommValue.AUTH_VALUE_FALSE;

            // 접근 Session 체크
            if (Session["ConsultingOk"] != null)
            {
                if (Session["ConsultingOk"].Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    // 접근 Parameter 체크
                    if (Request.Params["CounselCd"] != null &&
                        Request.Params["CounselSeq"] != null)
                    {
                        strCounselCd = Request.Params["CounselCd"].ToString();
                        txtHfCounselCd.Text = strCounselCd;

                        strCounselSeq = Request.Params["CounselSeq"].ToString();
                        txtHfCounselSeq.Text = strCounselSeq;
                        Session["ConsultingOk"] = null;

                        isParamOk = CommValue.AUTH_VALUE_TRUE;
                    }
                }
            }

            return isParamOk;
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        protected void InitControls()
        {
            ltPrint.Text = TextNm["PRINT"];

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

            ltTitleSMeter1.Text = TextNm["SMETER"];
            ltTitleSMeter2.Text = TextNm["SMETER"];
            ltTitleYears1.Text = TextNm["YEARS"];
            ltTitleYears2.Text = TextNm["YEARS"];
            ltTitleYears3.Text = TextNm["YEARS"];
            ltTitleMonth1.Text = TextNm["MONTHS"];
            ltTitleDays1.Text = TextNm["DAYS"];

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.Visible = Master.isModDelAuthOk;
            lnkbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ISSUE"] + "')";
            lnkbtnAdd.Text = TextNm["ADD"];
            lnkbtnAdd.OnClientClick = "javascript:return fnConfirmAndPopup('" + AlertNm["CONF_ADD_REMARK"] + "','" + txtHfCounselCd.Text + "','" + txtHfCounselSeq.Text + "')";
            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ISSUE"] + "')";
            lnkbtnDelete.Visible = Master.isModDelAuthOk;
            lnkbtList.Text = TextNm["LIST"];
        }

        /// <summary>
        /// 데이터 로드
        /// </summary>
        protected void LoadData()
        {

            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            int intCounselSeq = Int32.Parse(strCounselSeq);

            // KN_USP_RES_SELECT_COUNSELINFO_S01
            dsReturn = CounselMngBlo.WatchCounselDetInfo(strCounselCd, intCounselSeq, Session["MemAuthTy"].ToString(), Session["CompCd"].ToString(), Session["MemNo"].ToString(), Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                DataTable dtBasicInfo = new DataTable();

                dtBasicInfo = dsReturn.Tables[0];

                if (dtBasicInfo.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CompNm"].ToString()))
                    {
                        ltCompNm.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CompNm"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CompAddr"].ToString()))
                    {
                        ltCompAddr.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CompAddr"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CompDetAddr"].ToString()))
                    {
                        ltCompDetAddr.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CompDetAddr"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["IndustryNm"].ToString()))
                    {
                        ltIndustryNm.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["IndustryNm"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CountryNm"].ToString()))
                    {
                        ltCountryNm.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CountryNm"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CompGrade"].ToString()))
                    {
                        ltCompGrade.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CompGrade"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CompTyNm"].ToString()))
                    {
                        ltCompTyNm.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CompTyNm"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["ChargerNm"].ToString()))
                    {
                        ltChargerNm.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["ChargerNm"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["TelNo"].ToString()))
                    {
                        ltTelNo.Text = dtBasicInfo.Rows[0]["TelNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["FaxNo"].ToString()))
                    {
                        ltFaxNo.Text = dtBasicInfo.Rows[0]["FaxNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["Email"].ToString()))
                    {
                        ltEmail.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["Email"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["UsingAreaNm"].ToString()))
                    {
                        if (dtBasicInfo.Rows[0]["UsingAreaNm"].ToString().IndexOf("~") > 0)
                        {
                            ltUsingAreaNm.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["UsingAreaNm"].ToString());
                        }
                        else
                        {
                            ltUsingAreaNm.Text = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["UsingAreaNm"].ToString())).ToString("###,##0.##"));
                        }
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CurrRentalNm"].ToString()))
                    {
                        string strCurrRentalNm = string.Empty;

                        if (dtBasicInfo.Rows[0]["CurrRentalNm"].ToString().IndexOf("~") > 0)
                        {
                            strCurrRentalNm = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CurrRentalNm"].ToString());
                        }
                        else
                        {
                            strCurrRentalNm = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["CurrRentalNm"].ToString())).ToString("###,##0.##"));
                        }

                        if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                        {
                            ltCurrRentalNm.Text = strCurrRentalNm + " " + TextNm["DOLLAR"];
                        }
                        else
                        {
                            ltCurrRentalNm.Text = TextNm["DOLLAR"] + " " + strCurrRentalNm;
                        }
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CurrMngFee"].ToString()))
                    {
                        string strCurrMngFee = string.Empty;

                        if (dtBasicInfo.Rows[0]["CurrMngFee"].ToString().IndexOf("~") > 0)
                        {
                            strCurrMngFee = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CurrMngFee"].ToString());
                        }
                        else
                        {
                            strCurrMngFee = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["CurrMngFee"].ToString())).ToString("###,##0.##"));
                        }

                        if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                        {
                            ltCurrMngFee.Text = strCurrMngFee + " " + TextNm["DOLLAR"];
                        }
                        else
                        {
                            ltCurrMngFee.Text = TextNm["DOLLAR"] + " " + strCurrMngFee;
                        }
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CurrServiceFareNm"].ToString()))
                    {
                        ltCurrServiceFareNm.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CurrServiceFareNm"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["StaffNoNm"].ToString()))
                    {
                        string strStaffNoNm = string.Empty;

                        if (dtBasicInfo.Rows[0]["StaffNoNm"].ToString().IndexOf("~") > 0)
                        {
                            strStaffNoNm = TextLib.StringDecoder(dtBasicInfo.Rows[0]["StaffNoNm"].ToString());
                        }
                        else
                        {
                            strStaffNoNm = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["StaffNoNm"].ToString())).ToString("###,##0.##"));
                        }

                        ltStaffNoNm.Text = strStaffNoNm;
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["ContCommeceYear"].ToString()))
                    {
                        ltContCommeceYear.Text = dtBasicInfo.Rows[0]["ContCommeceYear"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["ContPeriodNm"].ToString()))
                    {
                        string strContPeriodNm = string.Empty;

                        if (dtBasicInfo.Rows[0]["ContPeriodNm"].ToString().IndexOf("~") > 0)
                        {
                            strContPeriodNm = TextLib.StringDecoder(dtBasicInfo.Rows[0]["ContPeriodNm"].ToString());
                        }
                        else
                        {
                            strContPeriodNm = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["ContPeriodNm"].ToString())).ToString("###,##0.##"));
                        }

                        ltContPeriodNm.Text = strContPeriodNm;
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CarNoNm"].ToString()))
                    {
                        ltCarNoNm.Text = dtBasicInfo.Rows[0]["CarNoNm"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["MotoBikeNoNm"].ToString()))
                    {
                        ltMotoBikeNoNm.Text = dtBasicInfo.Rows[0]["MotoBikeNoNm"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["TransferPlanReason"].ToString()))
                    {
                        ltTransferPlanReason.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["TransferPlanReason"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["TransferCostBuget"].ToString()))
                    {
                        string strTransferCostBuget = string.Empty;

                        if (dtBasicInfo.Rows[0]["TransferCostBuget"].ToString().IndexOf("~") > 0)
                        {
                            strTransferCostBuget = TextLib.StringDecoder(dtBasicInfo.Rows[0]["TransferCostBuget"].ToString());
                        }
                        else
                        {
                            strTransferCostBuget = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["TransferCostBuget"].ToString())).ToString("###,##0.##"));
                        }

                        if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                        {
                            ltTransferCostBuget.Text = strTransferCostBuget + " " + TextNm["DOLLAR"];
                        }
                        else
                        {
                            ltTransferCostBuget.Text = TextNm["DOLLAR"] + " " + strTransferCostBuget;
                        }
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["LeaseAreaNm"].ToString()))
                    {
                        string strLeaseAreaNm = string.Empty;

                        if (dtBasicInfo.Rows[0]["LeaseAreaNm"].ToString().IndexOf("~") > 0)
                        {
                            strLeaseAreaNm = TextLib.StringDecoder(dtBasicInfo.Rows[0]["LeaseAreaNm"].ToString());
                        }
                        else
                        {
                            strLeaseAreaNm = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["LeaseAreaNm"].ToString())).ToString("###,##0.##"));
                        }

                        ltLeaseAreaNm.Text = strLeaseAreaNm;
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["ExpectedRentalNm"].ToString()))
                    {
                        string strExpectedRentalNm = string.Empty;

                        if (dtBasicInfo.Rows[0]["ExpectedRentalNm"].ToString().IndexOf("~") > 0)
                        {
                            strExpectedRentalNm = TextLib.StringDecoder(dtBasicInfo.Rows[0]["ExpectedRentalNm"].ToString());
                        }
                        else
                        {
                            strExpectedRentalNm = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["ExpectedRentalNm"].ToString())).ToString("###,##0.##"));
                        }

                        ltExpectedRentalNm.Text = strExpectedRentalNm;
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["FavorateFloor"].ToString()))
                    {
                        ltFavorateFloor.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["FavorateFloor"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["FavorateDir"].ToString()))
                    {
                        ltFavorateDir.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["FavorateDir"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["CompBudget"].ToString()))
                    {
                        string strCompBudget = string.Empty;

                        if (dtBasicInfo.Rows[0]["CompBudget"].ToString().IndexOf("~") > 0)
                        {
                            strCompBudget = TextLib.StringDecoder(dtBasicInfo.Rows[0]["CompBudget"].ToString());
                        }
                        else
                        {
                            strCompBudget = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["CompBudget"].ToString())).ToString("###,##0.##"));
                        }

                        if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                        {
                            ltCompBudget.Text = strCompBudget + " " + TextNm["DOLLAR"];
                        }
                        else
                        {
                            ltCompBudget.Text = TextNm["DOLLAR"] + " " + strCompBudget;
                        }
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["ExpectedlPeriodNm"].ToString()))
                    {
                        string strExpectedlPeriodNm = string.Empty;

                        if (dtBasicInfo.Rows[0]["ExpectedlPeriodNm"].ToString().IndexOf("~") > 0)
                        {
                            strExpectedlPeriodNm = TextLib.StringDecoder(dtBasicInfo.Rows[0]["ExpectedlPeriodNm"].ToString());
                        }
                        else
                        {
                            strExpectedlPeriodNm = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["ExpectedlPeriodNm"].ToString())).ToString("###,##0.##"));
                        }

                        ltExpectedlPeriodNm.Text = strExpectedlPeriodNm;
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["LeaseYear"].ToString()))
                    {
                        ltLeaseYear.Text = dtBasicInfo.Rows[0]["LeaseYear"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["LeaseMonth"].ToString()))
                    {
                        ltLeaseMonth.Text = dtBasicInfo.Rows[0]["LeaseMonth"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["DecisionNm"].ToString()))
                    {
                        string strDecisionNm = string.Empty;

                        if (dtBasicInfo.Rows[0]["DecisionNm"].ToString().IndexOf("~") > 0)
                        {
                            strDecisionNm = TextLib.StringDecoder(dtBasicInfo.Rows[0]["DecisionNm"].ToString());
                        }
                        else
                        {
                            strDecisionNm = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["DecisionNm"].ToString())).ToString("###,##0.##"));
                        }

                        ltDecisionNm.Text = strDecisionNm;
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["NeedParkNo"].ToString()))
                    {
                        ltNeedParkNo.Text = dtBasicInfo.Rows[0]["NeedParkNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["InternalConstNm"].ToString()))
                    {
                        string strInternalConstNm = string.Empty;

                        if (dtBasicInfo.Rows[0]["InternalConstNm"].ToString().IndexOf("~") > 0)
                        {
                            strInternalConstNm = TextLib.StringDecoder(dtBasicInfo.Rows[0]["InternalConstNm"].ToString());
                        }
                        else
                        {
                            strInternalConstNm = TextLib.MakeVietRealNo(double.Parse(TextLib.StringDecoder(dtBasicInfo.Rows[0]["InternalConstNm"].ToString())).ToString("###,##0.##"));
                        }

                        ltInternalConstNm.Text = strInternalConstNm;
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["MovingLocationNm"].ToString()))
                    {
                        ltMovingLocationNm.Text = TextLib.StringDecoder(dtBasicInfo.Rows[0]["MovingLocationNm"].ToString());
                    }

                    DataTable dtRemark = new DataTable();

                    dtRemark = dsReturn.Tables[1];

                    lvRemark.DataSource = dtRemark;
                    lvRemark.DataBind();
                }
                else
                {
                    Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + strCounselCd, CommValue.AUTH_VALUE_FALSE);
                }
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRemark_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltTitleRemark")).Text = TextNm["REMARK"];
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
        protected void lvRemark_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                ((Literal)iTem.FindControl("ltTitleRemark")).Text = TextNm["REMARK"];

                if (!string.IsNullOrEmpty(drView["Remark"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltRemark")).Text = drView["Remark"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDt"].ToString()))
                {
                    string strDate = string.Empty;
                    strDate = drView["InsDt"].ToString();

                    StringBuilder sbList = new StringBuilder();

                    sbList.Append("- ");
                    sbList.Append(strDate.Substring(0, 4));
                    sbList.Append(".");
                    sbList.Append(strDate.Substring(4, 2));
                    sbList.Append(".");
                    sbList.Append(strDate.Substring(6, 2));

                    ((Literal)iTem.FindControl("ltInsDate")).Text = sbList.ToString();
                }
            }
        }

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfCounselCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfCounselSeq.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!string.IsNullOrEmpty(txtHfCounselSeq.Text))
                {
                    // KN_USP_RES_DELETE_COUNSELINFO_M00
                    CounselMngBlo.RemoveCounselInfo(txtHfCounselCd.Text, Int32.Parse(txtHfCounselSeq.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());

                    Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfCounselCd.Text, CommValue.AUTH_VALUE_FALSE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfCounselCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnAddRemark_Click(object sender, ImageClickEventArgs e)
        {
            Session["RemarkAddOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
        }
    }
}