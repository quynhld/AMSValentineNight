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
    public partial class MonthMngInfoWrite : BasePage
    {
        string strRentCd = string.Empty;
        string strFeeTy = string.Empty;
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

                        strNowDt = DateTime.Today.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

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

                            // 컨트롤 초기화
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
        /// 데이터 로딩하는 메소드
        /// </summary>
        /// <param name="dtReturn"></param>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S03
            dsReturn = MngPaymentBlo.SpreadMngInfo(Session["LangCd"].ToString(), txtHfRentCd.Text, txtHfFeeTy.Text);

            if (dsReturn != null)
            {
                if (dsReturn.Tables[0] != null)
                {
                    lvMonthMngInfoWrite.DataSource = dsReturn.Tables[0];
                    lvMonthMngInfoWrite.DataBind();
                }

                if (dsReturn.Tables[1] != null)
                {
                    if (dsReturn.Tables[1].Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        ddlYear.SelectedValue = dsReturn.Tables[1].Rows[0]["MngYear"].ToString();
                        ddlMonth.SelectedValue = dsReturn.Tables[1].Rows[0]["MngMM"].ToString();
                        txtHfYear.Text = dsReturn.Tables[1].Rows[0]["MngYear"].ToString();
                        txtHfMonth.Text = dsReturn.Tables[1].Rows[0]["MngMM"].ToString();
                        ddlYear.Enabled = CommValue.AUTH_VALUE_FALSE;
                        ddlMonth.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                        ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
                        txtHfYear.Text = DateTime.Now.Year.ToString();
                        txtHfMonth.Text = DateTime.Now.Month.ToString().PadLeft(2, '0');
                        ddlYear.Enabled = CommValue.AUTH_VALUE_FALSE;
                        ddlMonth.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                }
            }
        }

        protected void lvMonthMngInfoWrite_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;
                
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

                TextBox txtInsMngFeeNet = (TextBox)iTem.FindControl("txtInsMngFeeNet");
                txtInsMngFeeNet.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            }
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private bool CheckParams()
        {
            bool isParamOk = CommValue.AUTH_VALUE_FALSE;

            // 접근 Session 체크
            if (Session["ConsultingOk"] != null)
            {
                if (Session["ConsultingOk"].Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    // 접근 Parameter 체크
                    if (Request.Params[Master.PARAM_DATA1] != null)
                    {
                        strRentCd = Request.Params[Master.PARAM_DATA1].ToString();
                        txtHfRentCd.Text = strRentCd;

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
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            ltUseYn.Text = TextNm["USEYN"];
            ltMngFeeCd.Text = TextNm["TOPICCD"];
            ltMngFeeNm.Text = TextNm["TOPIC"];
            ltMngFee.Text = TextNm["MANAGEFEE"];
            ltMngFeeNET.Text = TextNm["MANAGEFEE"] + " ( " + TextNm["NET"] + " )";
            ltMngFeeVAT.Text = TextNm["VAT"];
            ltChargeMonth.Text = TextNm["CHARGEMONTH"];

            lnkbtnRegist.Text = TextNm["ADD"];
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnList.Text = TextNm["LIST"];

            lnkbtnReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "');";

            lnkbtnRegist.Visible = Master.isWriteAuthOk;

            MakeYearDdl();

            MakeMonthDdl();
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

                Response.Redirect(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA4 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
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
            string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S02
                DataTable dtReturn = MngPaymentBlo.SpreadMngInfo(Session["LangCd"].ToString(), txtHfRentCd.Text, txtHfFeeTy.Text, txtHfYear.Text, txtHfMonth.Text);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        hfAlertText.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];
                        LoadData();
                    }
                    else
                    {
                        for (int intTmpI = 0; intTmpI < lvMonthMngInfoWrite.Items.Count; intTmpI++)
                        {
                            string strChkYn = CommValue.CONCLUSION_TYPE_TEXT_NO;

                            CheckBox chkInsUseYn = (CheckBox)lvMonthMngInfoWrite.Items[intTmpI].FindControl("chkInsUseYn");

                            Literal ltInsMngFeeCd = (Literal)lvMonthMngInfoWrite.Items[intTmpI].FindControl("ltInsMngFeeCd");

                            TextBox txtInsMngFeeNet = (TextBox)lvMonthMngInfoWrite.Items[intTmpI].FindControl("txtInsMngFeeNet");

                            if (string.IsNullOrEmpty(txtInsMngFeeNet.Text))
                            {
                                txtInsMngFeeNet.Text = CommValue.NUMBER_VALUE_ZERO;
                            }

                            if (chkInsUseYn.Checked)
                            {
                                strChkYn = CommValue.CONCLUSION_TYPE_TEXT_YES;
                            }

                            DateTime dtLastDay = new DateTime(Int32.Parse(txtHfYear.Text), Int32.Parse(txtHfMonth.Text), 1).AddMonths(0).AddDays(-1);  //마지막 일

                            string strLastDay = dtLastDay.ToString("s").Substring(0,10).Replace("/", "").Replace("-", "");

                            // KN_USP_MNG_INSERT_MONTHMNGMENUINFO_M00
                            MngPaymentBlo.RegistryMonthMngInfo(txtHfRentCd.Text, txtHfFeeTy.Text, ltInsMngFeeCd.Text, txtHfYear.Text, txtHfMonth.Text, txtInsMngFeeNet.Text,
                                                               strChkYn, strLastDay, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP,"0");
                        }

                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA4 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

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
            string strMngFeeNet = ((TextBox)lvMonthMngInfoWrite.Items[intRows].FindControl("txtInsMngFeeNet")).Text;

            Literal ltInsMngFeeVat = (Literal)lvMonthMngInfoWrite.Items[intRows].FindControl("ltInsMngFeeVat");
            Literal ltInsMngFee = (Literal)lvMonthMngInfoWrite.Items[intRows].FindControl("ltInsMngFee");

            if (!string.IsNullOrEmpty(strMngFeeNet) && !string.IsNullOrEmpty(hfVatRation.Value))
            {
                ltInsMngFeeVat.Text = TextLib.MakeVietIntNo((double.Parse(strMngFeeNet) * double.Parse(hfVatRation.Value) / 100).ToString("###,##0"));
                ltInsMngFee.Text = TextLib.MakeVietIntNo((double.Parse(ltInsMngFeeVat.Text.Replace(".", "")) + double.Parse(strMngFeeNet)).ToString("###,##0"));
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
                for (int intTmpI = 0; intTmpI < lvMonthMngInfoWrite.Items.Count; intTmpI++)
                {
                    CheckBox chkInsUseYn = (CheckBox)lvMonthMngInfoWrite.Items[intTmpI].FindControl("chkInsUseYn");
                    TextBox txtInsMngFeeNet = (TextBox)lvMonthMngInfoWrite.Items[intTmpI].FindControl("txtInsMngFeeNet");

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
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.Year + 1; intTmpI++)
            {
                ddlYear.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }

            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl()
        {
            ddlMonth.Items.Clear();

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
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
    }
}