using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;
using KN.Manage.Biz;

namespace KN.Web.Management.Manage
{
    public partial class MonthMngInfoView : BasePage
    {
        string strRentCd = string.Empty;
        string strFeeTy = string.Empty;
        string strMngYear = string.Empty;
        string strMngMM = string.Empty;
        string strPage = string.Empty;
        double dblVatRation = CommValue.NUMBER_VALUE_0_0;

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
                        // 접근페이지정보
                        string[] strSegments = HttpContext.Current.Request.Url.Segments;

                        // 대상일자 정보
                        string strNowDt = string.Empty;

                        // 부가세 테이블
                        DataTable dtVatReturn = new DataTable();

                        if (!string.IsNullOrEmpty(hfLimitDt.Value))
                        {
                            strNowDt = hfLimitDt.Value.Replace("-", "").ToString();
                        }
                        else
                        {
                            strNowDt = DateTime.Today.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");
                        }

                        // KN_USP_MNG_SELECT_VATINFO_S02
                        dtVatReturn = VatMngBlo.WatchVatDetailInfo(strSegments[strSegments.Length - 1], Request.Params[Master.PARAM_DATA4].ToString(), string.Empty, strNowDt);

                        if (dtVatReturn.Rows.Count == CommValue.NUMBER_VALUE_0)
                        {
                            StringBuilder sbWarning = new StringBuilder();
                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["ALERT_REGISTER_VATRATIO"]);
                            sbWarning.Append("');");
                            sbWarning.Append("document.location.href=\"" + Master.PAGE_TRANSFER + "\";");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            hfVatRation.Value = dtVatReturn.Rows[0]["VatRatio"].ToString();
                            dblVatRation = double.Parse(dtVatReturn.Rows[0]["VatRatio"].ToString());

                            InitControls();
                            LoadData();
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
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            ltDeadLine.Text = TextNm["DEADLINE"];
            ltUseYn.Text = TextNm["USEYN"];
            ltMngFeeCd.Text = TextNm["TOPICCD"];
            ltMngFeeNm.Text = TextNm["TOPIC"];
            ltMngFeeNET.Text = TextNm["MNGFEEUNIT"];
            ltMngFeeVAT.Text = TextNm["VAT"];
            ltMngFee.Text = TextNm["CLAIMFEEUNIT"];

            lnkbtnRegist.Text = TextNm["MODIFY"];
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnList.Text = TextNm["LIST"];

            lnkbtnReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "');";

            lnkbtnRegist.Visible = Master.isWriteAuthOk;

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
                        txtTopBaseRate.Text = strDong;
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                        txtTopBaseRate.Text = string.Empty;
                    }
                }
                else
                {
                    ltRealBaseRate.Text = "-";
                    txtTopBaseRate.Text = string.Empty;
                }
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
                    if (Request.Params[Master.PARAM_DATA1] != null &&
                        Request.Params[Master.PARAM_DATA2] != null &&
                        Request.Params[Master.PARAM_DATA3] != null)
                    {
                        strRentCd = Request.Params[Master.PARAM_DATA1].ToString();
                        txtHfRentCd.Text = strRentCd;

                        strMngYear = Request.Params[Master.PARAM_DATA2].ToString();
                        txtHfMngYear.Text = strMngYear;

                        strMngMM = Request.Params[Master.PARAM_DATA3].ToString();
                        txtHfMngMM.Text = strMngMM;

                        strFeeTy = Request.Params[Master.PARAM_DATA4].ToString();
                        txtHfFeeTy.Text = strFeeTy;

                        Session["ConsultingOk"] = null;

                        isParamOk = CommValue.AUTH_VALUE_TRUE;
                    }
                }
            }

            return isParamOk;
        }

        /// <summary>
        /// 데이터 로딩하는 메소드
        /// </summary>
        /// <param name="dtReturn"></param>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S02
            dtReturn = MngPaymentBlo.SpreadMngInfo(Session["LangCd"].ToString(), txtHfRentCd.Text, txtHfFeeTy.Text, txtHfMngYear.Text, txtHfMngMM.Text);

            if (dtReturn != null)
            {
                lvMonthMngInfoView.DataSource = dtReturn;
                lvMonthMngInfoView.DataBind();
            }
        }

        protected void lvMonthMngInfoView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string chkUseYn;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["UseYn"].ToString()))
                {
                    CheckBox chkInsUseYn = (CheckBox)iTem.FindControl("chkInsUseYn");
                    chkUseYn = drView["UseYn"].ToString();

                    TextBox txtInsMngFeeNet = (TextBox)iTem.FindControl("txtInsMngFeeNet");
                    if (CommValue.CHOICE_VALUE_YES.Equals(chkUseYn))
                    {
                        chkInsUseYn.Checked = CommValue.AUTH_VALUE_TRUE;
                        txtInsMngFeeNet.Enabled = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        chkInsUseYn.Checked = CommValue.AUTH_VALUE_FALSE;
                        txtInsMngFeeNet.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }

                    Literal ltInsMngFeeVat = (Literal)iTem.FindControl("ltInsMngFeeVat");
                    Literal ltInsMngFee = (Literal)iTem.FindControl("ltInsMngFee");

                    if (drView["MngFee"].ToString().Equals("0.00"))
                    {
                        txtInsMngFeeNet.Text = CommValue.NUMBER_VALUE_ZERO;
                        ltInsMngFeeVat.Text = CommValue.NUMBER_VALUE_ZERO;
                        ltInsMngFee.Text = CommValue.NUMBER_VALUE_ZERO;
                    }
                    else if (!string.IsNullOrEmpty(drView["MngFee"].ToString()))
                    {
                        txtInsMngFeeNet.Text = (double.Parse(drView["MngFee"].ToString())).ToString("##0");
                        ltInsMngFeeVat.Text = TextLib.MakeVietIntNo((double.Parse(drView["MngFee"].ToString()) * dblVatRation / 100).ToString("###,##0"));
                        ltInsMngFee.Text = TextLib.MakeVietIntNo((double.Parse(drView["MngFee"].ToString()) + (double.Parse(drView["MngFee"].ToString()) * dblVatRation / 100)).ToString("###,##0"));
                    }

                    txtInsMngFeeNet.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["MngFeeCd"].ToString()))
                {
                    Literal ltInsMngFeeCd = (Literal)iTem.FindControl("ltInsMngFeeCd");
                    ltInsMngFeeCd.Text = drView["MngFeeCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ExpressNm"].ToString()))
                {
                    Literal ltInsMngFeeNm = (Literal)iTem.FindControl("ltInsMngFeeNm");
                    ltInsMngFeeNm.Text = TextLib.StringDecoder(drView["ExpressNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["LimitDt"].ToString()))
                {
                    string strLimitDt = drView["LimitDt"].ToString();

                    txtLimitDt.Text = strLimitDt.Substring(0, 4) + "-" + strLimitDt.Substring(4, 2) + "-" + strLimitDt.Substring(6, 2);
                    hfLimitDt.Value = strLimitDt;
                    txtHfOriginDt.Text = strLimitDt;

                    StringBuilder sbInsdDt = new StringBuilder();

                    sbInsdDt.Append("<a href='#'><img align='absmiddle' onclick=\"Calendar(this, '");
                    sbInsdDt.Append(txtLimitDt.ClientID);
                    sbInsdDt.Append("', '");
                    sbInsdDt.Append(hfLimitDt.ClientID);
                    sbInsdDt.Append("', tue)\" src='/Common/Images/Common/calendar.gif' style='cursor:pointer;' value='' /></a>");

                    ltLimitDt.Text = sbInsdDt.ToString();
                }
            }
        }

        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S00
                MngPaymentBlo.SpreadMngInfoList(CommValue.BOARD_VALUE_PAGESIZE, txtHfRentCd.Text, txtHfFeeTy.Text, txtHfMngYear.Text);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA4 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                ResetControls();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

            try
            {
                for (int intTmpI = 0; intTmpI < lvMonthMngInfoView.Items.Count; intTmpI++)
                {
                    string strChkYn = CommValue.CONCLUSION_TYPE_TEXT_NO;

                    CheckBox chkInsUseYn = (CheckBox)lvMonthMngInfoView.Items[intTmpI].FindControl("chkInsUseYn");

                    Literal ltInsMngFeeCd = (Literal)lvMonthMngInfoView.Items[intTmpI].FindControl("ltInsMngFeeCd");

                    TextBox txtInsMngFeeNet = (TextBox)lvMonthMngInfoView.Items[intTmpI].FindControl("txtInsMngFeeNet");
                    string strInsMngFeeNet = txtInsMngFeeNet.Text.Replace(".", "");

                    if (string.IsNullOrEmpty(txtInsMngFeeNet.Text))
                    {
                        txtInsMngFeeNet.Text = CommValue.NUMBER_VALUE_ZERO;
                    }

                    if (chkInsUseYn.Checked)
                    {
                        strChkYn = CommValue.CONCLUSION_TYPE_TEXT_YES;
                    }

                    string strInsDt = hfLimitDt.Value.Replace("-", "");

                    // KN_USP_MNG_UPDATE_MONTHMNGMENUINFO_M00
                    MngPaymentBlo.ModifyMonthMngInfo(txtHfRentCd.Text, txtHfFeeTy.Text, ltInsMngFeeCd.Text, txtHfMngYear.Text, txtHfMngMM.Text, strInsMngFeeNet, strChkYn, 
                                                     strInsDt, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP,"0");
                }

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA4 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtInsMngFeeNet_TextChanged(object sender, EventArgs e)
        {
            int intRows = ((System.Web.UI.WebControls.TextBox)(sender)).Rows;
            string strMngFeeNet = ((TextBox)lvMonthMngInfoView.Items[intRows].FindControl("txtInsMngFeeNet")).Text;

            Literal ltInsMngFeeVat = (Literal)lvMonthMngInfoView.Items[intRows].FindControl("ltInsMngFeeVat");
            Literal ltInsMngFee = (Literal)lvMonthMngInfoView.Items[intRows].FindControl("ltInsMngFee");

            if (strMngFeeNet.Equals("0"))
            {
                ltInsMngFeeVat.Text = "0";
                ltInsMngFee.Text = "0";
            }

            else if (!string.IsNullOrEmpty(strMngFeeNet) && !string.IsNullOrEmpty(hfVatRation.Value))
            {
                ltInsMngFeeVat.Text = TextLib.MakeVietIntNo((double.Parse(strMngFeeNet) * double.Parse(hfVatRation.Value) / 100).ToString("###,##0"));
                ltInsMngFee.Text = (double.Parse(ltInsMngFeeVat.Text.Replace(".", "")) + double.Parse(strMngFeeNet)).ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkInsUseYn_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int intTmpI = 0; intTmpI < lvMonthMngInfoView.Items.Count; intTmpI++)
                {
                    CheckBox chkInsUseYn = (CheckBox)lvMonthMngInfoView.Items[intTmpI].FindControl("chkInsUseYn");
                    TextBox txtInsMngFeeNet = (TextBox)lvMonthMngInfoView.Items[intTmpI].FindControl("txtInsMngFeeNet");

                    if (chkInsUseYn.Checked)
                    {

                        txtInsMngFeeNet.Enabled = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        txtInsMngFeeNet.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤을 리셋하는 메소드
        /// </summary>
        private void ResetControls()
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbView = new StringBuilder();
                sbView.Append(Master.PAGE_VIEW);
                sbView.Append("?");
                sbView.Append(Master.PARAM_DATA1);
                sbView.Append("=");
                sbView.Append(txtHfRentCd.Text);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA2);
                sbView.Append("=");
                sbView.Append(txtHfMngYear.Text);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA3);
                sbView.Append("=");
                sbView.Append(txtHfMngMM.Text);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA4);
                sbView.Append("=");
                sbView.Append(txtHfFeeTy.Text);

                Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}