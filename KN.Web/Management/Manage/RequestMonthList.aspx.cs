using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;

namespace KN.Web.Management.Manage
{
    public partial class RequestMonthList : BasePage
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

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            MakeYearDdl(ddlSearchYear);
            MakeMonthDdl(ddlSearchMonth, CommValue.NUMBER_VALUE_1, string.Empty);
            CommCdDdlUtil.MakeEtcSubCdDdlUserTitle(ddlSearchRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_APARTMENTTY, TextNm["SELECT"]);

            lnkbtnMakeList.OnClientClick = "javascript:return fnCheckRentCd('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnMakeList.Text = TextNm["AUTOABSTRACT"];
            lnkbtnRegist.Text = TextNm["REGIST"];

            chkDirectlyInput.Text = TextNm["DIRECTWRITE"];
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl(DropDownList ddlParams)
        {
            ddlParams.Items.Clear();

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.AddYears(1).Year; intTmpI++)
            {
                ddlParams.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }

            ddlParams.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl(DropDownList ddlParams, int intStartMonth, string strDefaultMM)
        {
            ddlParams.Items.Clear();

            for (int intTmpI = intStartMonth; intTmpI <= 12; intTmpI++)
            {
                ddlParams.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            if (string.IsNullOrEmpty(strDefaultMM))
            {
                ddlParams.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
            }
            else
            {
                ddlParams.SelectedValue = strDefaultMM.PadLeft(2, '0');
            }
        }

        protected void LoadData()
        {
            DataTable dtListReturn = new DataTable();

            // KN_USP_MNG_SELECT_MNGFEEMONTHSETINFO_S00
            dtListReturn = MngPaymentBlo.SpreadMngFeeMonthSetInfo(ddlSearchRentCd.SelectedValue, ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue);

            if (dtListReturn != null)
            {
                lvRequestMonthList.DataSource = dtListReturn;
                lvRequestMonthList.DataBind();

                if (dtListReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    lnkbtnMakeList.Visible = CommValue.AUTH_VALUE_FALSE;
                }
                else
                {
                    lnkbtnMakeList.Visible = CommValue.AUTH_VALUE_TRUE;
                }
            }
        }

        protected void lvRequestMonthList_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltTopMonth = (Literal)lvRequestMonthList.FindControl("ltTopMonth");
            Literal ltTopMonthCnt = (Literal)lvRequestMonthList.FindControl("ltTopMonthCnt");
            Literal ltTopStartDt = (Literal)lvRequestMonthList.FindControl("ltTopStartDt");
            Literal ltTopStartMonth = (Literal)lvRequestMonthList.FindControl("ltTopStartMonth");

            ltTopMonth.Text = TextNm["MONTH"];
            ltTopMonthCnt.Text = TextNm["MONTHS"];
            ltTopStartDt.Text = TextNm["FROM"];
            ltTopStartMonth.Text = TextNm["APPLYDT"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRequestMonthList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    Literal ltTopMonth = (Literal)e.Item.FindControl("ltTopMonth");
                    Literal ltTopMonthCnt = (Literal)e.Item.FindControl("ltTopMonthCnt");
                    Literal ltTopStartDt = (Literal)e.Item.FindControl("ltTopStartDt");
                    Literal ltTopStartMonth = (Literal)e.Item.FindControl("ltTopStartMonth");

                    ltTopMonth.Text = TextNm["MONTH"];
                    ltTopMonthCnt.Text = TextNm["MONTHS"];
                    ltTopStartDt.Text = TextNm["FROM"];
                    ltTopStartMonth.Text = TextNm["APPLYDT"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvRequestMonthList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["MonthSeq"].ToString()))
                {
                    Literal ltMonth = (Literal)iTem.FindControl("ltMonth");
                    ltMonth.Text = drView["MonthSeq"].ToString();
                }

                //ddlMonth
                if (!string.IsNullOrEmpty(drView["DuringMonth"].ToString()))
                {
                    DropDownList ddlMonthCnt = (DropDownList)iTem.FindControl("ddlMonthCnt");
                    MakeMonthDdl(ddlMonthCnt, CommValue.NUMBER_VALUE_0, drView["DuringMonth"].ToString());

                    TextBox txtOrgMonthCnt = (TextBox)iTem.FindControl("txtHfOrgMonthCnt");
                    txtOrgMonthCnt.Text = drView["DuringMonth"].ToString();

                    TextBox txtHfChgMonthCnt = (TextBox)iTem.FindControl("txtHfChgMonthCnt");
                    txtHfChgMonthCnt.Text = drView["DuringMonth"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RealDuringMonth"].ToString()))
                {
                    TextBox txtHfMonthCnt = (TextBox)iTem.FindControl("txtHfMonthCnt");
                    txtHfMonthCnt.Text = drView["RealDuringMonth"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StartDt"].ToString()))
                {
                    string strDate = drView["StartDt"].ToString();

                    TextBox txtStartDt = (TextBox)iTem.FindControl("txtStartDt");
                    HiddenField hfStartDt = (HiddenField)iTem.FindControl("hfStartDt");

                    Literal ltCalendarImg = (Literal)iTem.FindControl("ltCalendarImg");

                    txtStartDt.Text = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2);
                    hfStartDt.Value = strDate;

                    StringBuilder sbInsdDt = new StringBuilder();

                    sbInsdDt.Append("<a href='#'><img align='absmiddle' onclick=\"Calendar(this, '");
                    sbInsdDt.Append(txtStartDt.ClientID);
                    sbInsdDt.Append("', '");
                    sbInsdDt.Append(hfStartDt.ClientID);
                    sbInsdDt.Append("', true)\" src='/Common/Images/Common/calendar.gif' style='cursor:pointer;' value='' /></a>");

                    ltCalendarImg.Text = sbInsdDt.ToString();
                }

                if (!string.IsNullOrEmpty(drView["StartMonth"].ToString()))
                {
                    DropDownList ddlStartMonth = (DropDownList)iTem.FindControl("ddlStartMonth");
                    MakeMonthDdl(ddlStartMonth, CommValue.NUMBER_VALUE_1, drView["StartMonth"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RealStartMonth"].ToString()))
                {
                    DropDownList ddlRealStartMonth = (DropDownList)iTem.FindControl("ddlRealStartMonth");
                    MakeMonthDdl(ddlRealStartMonth, CommValue.NUMBER_VALUE_1, drView["RealStartMonth"].ToString());
                }
            }
        }

        protected void lnkbtnMakeList_Click(object sender, EventArgs e)
        {
            try
            {
                string strUserIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_MNG_INSERT_MNGFEEMONTHSETINFO_M00
                MngPaymentBlo.RegistryInitMngFeeMonthSetInfo(ddlSearchRentCd.SelectedValue, ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue,
                                                             Session["CompCd"].ToString(), Session["MemNo"].ToString(), strUserIP);

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlSearchRentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlMonthCnt_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int intTmpI = 0;

                if (!chkDirectlyInput.Checked)
                {
                    bool isStart = CommValue.AUTH_VALUE_FALSE;
                    int intSelectedDuring = ((DropDownList)(sender)).SelectedIndex;
                    int intInsertDuring = ((DropDownList)(sender)).SelectedIndex;

                    for (intTmpI = 0; intTmpI <= 11; intTmpI++)
                    {
                        if (((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlMonthCnt")).Enabled)
                        {
                            ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfChgMonthCnt")).Text = Int32.Parse(((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlMonthCnt")).Text).ToString();
                            string strTmp = ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfChgMonthCnt")).Text;
                        }

                        string strStartDt = ((HiddenField)lvRequestMonthList.Items[intTmpI].FindControl("hfStartDt")).Value.Replace("-", "").Replace("/", "");
                        ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtStartDt")).Text = TextLib.MakeDateEightDigit(strStartDt);
                    }

                    for (intTmpI = 0; intTmpI <= 11; intTmpI++)
                    {
                        string strOrgData = ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfOrgMonthCnt")).Text.PadLeft(2, '0');
                        string strChgData = ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfChgMonthCnt")).Text.PadLeft(2, '0');

                        if (!strOrgData.Equals(strChgData) || isStart)
                        {
                            isStart = CommValue.AUTH_VALUE_TRUE;

                            if (intSelectedDuring > 0)
                            {
                                ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfOrgMonthCnt")).Text = intSelectedDuring.ToString();
                                ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfChgMonthCnt")).Text = intSelectedDuring.ToString();
                                DropDownList ddlMontCnt = ((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlMonthCnt"));

                                if (intSelectedDuring < intInsertDuring)
                                {
                                    ddlMontCnt.SelectedIndex = CommValue.NUMBER_VALUE_0;
                                    ddlMontCnt.Enabled = CommValue.AUTH_VALUE_FALSE;
                                }
                                intSelectedDuring--;
                            }
                        }
                    }

                    if (intSelectedDuring > 0)
                    {
                        for (intTmpI = 0; intTmpI <= 11; intTmpI++)
                        {
                            string strOrgData = ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfOrgMonthCnt")).Text.PadLeft(2, '0');
                            string strChgData = ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfChgMonthCnt")).Text.PadLeft(2, '0');

                            ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfOrgMonthCnt")).Text = intSelectedDuring.ToString();
                            ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfChgMonthCnt")).Text = intSelectedDuring.ToString();
                            DropDownList ddlMontCnt = ((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlMonthCnt"));

                            if (intSelectedDuring < intInsertDuring)
                            {
                                ddlMontCnt.SelectedIndex = CommValue.NUMBER_VALUE_0;
                                ddlMontCnt.Enabled = CommValue.AUTH_VALUE_FALSE;
                            }
                            intSelectedDuring--;

                            if (intSelectedDuring <= CommValue.NUMBER_VALUE_0)
                            {
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (intTmpI = 0; intTmpI <= 11; intTmpI++)
                    {
                        DropDownList ddlSelected = ((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlMonthCnt"));

                        if (ddlSelected.Enabled && ddlSelected.SelectedIndex > CommValue.NUMBER_VALUE_0)
                        {
                            ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfOrgMonthCnt")).Text = Int32.Parse(ddlSelected.SelectedValue).ToString();
                            ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfChgMonthCnt")).Text = Int32.Parse(ddlSelected.SelectedValue).ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void chkDirectlyInput_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int intTmpI = 0;

                if (chkDirectlyInput.Checked)
                {
                    for (intTmpI = 0; intTmpI < 12; intTmpI++)
                    {
                        ((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlMonthCnt")).Enabled = CommValue.AUTH_VALUE_TRUE;
                    }
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
                int intTmpI = 0;
                string strIp = Request.ServerVariables["REMOTE_ADDR"];

                for (intTmpI = 0; intTmpI < 12; intTmpI++)
                {
                    string strMonth = ((Literal)lvRequestMonthList.Items[intTmpI].FindControl("ltMonth")).Text;
                    string strStartDt = ((HiddenField)lvRequestMonthList.Items[intTmpI].FindControl("hfStartDt")).Value.Replace("/", "").Replace("-", "");
                    string strChgMonthCnt = ((TextBox)lvRequestMonthList.Items[intTmpI].FindControl("txtHfChgMonthCnt")).Text;
                    string strStartMonth = ((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlStartMonth")).SelectedValue;
                    string strRealStartMonth = ((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlRealStartMonth")).SelectedValue;

                    DropDownList ddlMonthCnt = ((DropDownList)lvRequestMonthList.Items[intTmpI].FindControl("ddlMonthCnt"));
                    ddlMonthCnt.Enabled = CommValue.AUTH_VALUE_TRUE;

                    // KN_USP_MNG_INSERT_MNGFEEMONTHSETINFO_M01
                    MngPaymentBlo.RegistryMngFeeMonthSetInfo(ddlSearchRentCd.SelectedValue, Int32.Parse(strMonth), strStartDt, Int32.Parse(ddlMonthCnt.SelectedValue),
                                                            Int32.Parse(strChgMonthCnt), Int32.Parse(strStartMonth), Int32.Parse(strRealStartMonth),
                                                            Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);
                }

                ddlSearchRentCd.SelectedIndex = CommValue.NUMBER_VALUE_0;
                ddlSearchYear.SelectedValue = DateTime.Now.Year.ToString();
                ddlSearchMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
                chkDirectlyInput.Checked = CommValue.AUTH_VALUE_FALSE;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}