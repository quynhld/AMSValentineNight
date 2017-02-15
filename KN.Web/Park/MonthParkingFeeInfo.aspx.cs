using System;
using System.Data;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Parking.Biz;

namespace KN.Web.Park
{
    public partial class MonthParkingFeeInfo : BasePage
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
            chkSamePrev.Text = TextNm["PREVMONTH"];

            ltApplyDt.Text = TextNm["APPLYDT"];
            ltKind.Text = TextNm["KIND"];
            ltFee.Text = TextNm["AMT"];

            lnkbtnRegist.Text = TextNm["ENTIRE_REG"];

            // DropDownList Setting

            // 섹션코드 조회
            LoadRentDdl(ddlRentNm, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            ddlRentNm.SelectedValue = CommValue.RENTAL_VALUE_APT;

            MakeYearDdl();
            MakeMonthDdl();
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem(TextNm["YEARS"], ""));

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.AddYears(1).Year; intTmpI++)
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
            ddlMonth.Items.Add(new ListItem(TextNm["MONTH"], ""));

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');

        }
        
        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTA) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTB) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                {
                    ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            chkSamePrev.Enabled = CommValue.AUTH_VALUE_TRUE;
            lnkbtnRegist.Visible = CommValue.AUTH_VALUE_TRUE;

            DataTable dtListReturn = new DataTable();

            // KN_USP_PRK_SELECT_MONTHPARKINGFEEINFO_S01
            dtListReturn = ParkingMngBlo.SpreadMonthParkingInfoList(ddlRentNm.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue, Session["LangCd"].ToString());

            if (dtListReturn != null)
            {
                lvMonthParkingFeeList.DataSource = dtListReturn;
                lvMonthParkingFeeList.DataBind();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMonthParkingFeeList_LayoutCreated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMonthParkingFeeList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvMonthParkingFeeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["CarTyCd"].ToString()))
                {
                    DropDownList ddlCarTy = (DropDownList)iTem.FindControl("ddlCarTy");
                    CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlCarTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_CARTY);
                    ddlCarTy.SelectedValue = drView["CarTyCd"].ToString();

                    TextBox txtHfCarTy = (TextBox)iTem.FindControl("txtHfCarTy");
                    txtHfCarTy.Text = drView["CarTyCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ParkingFee"].ToString()))
                {
                    TextBox txtFee = (TextBox)iTem.FindControl("txtFee");
                    txtFee.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtFee.Text = drView["ParkingFee"].ToString();
                    Literal ltDong = (Literal)iTem.FindControl("ltDong");
                    ltDong.Text = TextNm["DONG"];
                }

                TextBox txtApplyDt = (TextBox)iTem.FindControl("txtApplyDt");
                HiddenField hfApplyDt = (HiddenField)iTem.FindControl("hfApplyDt");
                Literal ltCalendar = (Literal)iTem.FindControl("ltCalendar");

                ltCalendar.Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"Calendar(this, '" + txtApplyDt.ClientID + "','" + hfApplyDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";

                if (!string.IsNullOrEmpty(drView["ApplyDt"].ToString()))
                {
                    txtApplyDt.Text = TextLib.MakeDateEightDigit(drView["ApplyDt"].ToString());
                    hfApplyDt.Value = drView["ApplyDt"].ToString();
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.Visible = Master.isModDelAuthOk;
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.Visible = Master.isModDelAuthOk;
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";

                if (!string.IsNullOrEmpty(drView["InsMemNo"].ToString()))
                {
                    chkSamePrev.Enabled = CommValue.AUTH_VALUE_FALSE;
                    lnkbtnRegist.Visible = CommValue.AUTH_VALUE_FALSE;
                }
            }
        }

        protected void lvMonthParkingFeeList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfCarTy = (TextBox)lvMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfCarTy");

                // KN_USP_PRK_DELETE_MONTHPARKINGFEEINFO_M00
                ParkingMngBlo.RemoveMonthParkingFee(ddlRentNm.SelectedValue, ddlYear.Text, ddlMonth.Text, txtHfCarTy.Text);

                Response.Redirect(Master.PAGE_LIST);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMonthParkingFeeList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfCarTy = (TextBox)lvMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtHfCarTy");
                TextBox txtFee = (TextBox)lvMonthParkingFeeList.Items[e.ItemIndex].FindControl("txtFee");
                HiddenField hfApplyDt = (HiddenField)lvMonthParkingFeeList.Items[e.ItemIndex].FindControl("hfApplyDt");

                string strFee = string.Empty;
                double dblFee = 0d;

                if (!string.IsNullOrEmpty(txtFee.Text))
                {
                    dblFee = double.Parse(txtFee.Text);
                }

                // KN_USP_PRK_UPDATE_MONTHPARKINGFEEINFO_M00
                ParkingMngBlo.ModifyMonthParkingFee(ddlRentNm.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue, txtHfCarTy.Text, hfApplyDt.Value.Replace(".", "").Replace("-", ""), dblFee);

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddl_SelectChanged(object sender, EventArgs e)
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

        protected void chkSamePrev_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // KN_USP_PRK_INSERT_MONTHPARKINGFEEINFO_M01
                ParkingMngBlo.RegistryEntireMonthParkingFee(ddlRentNm.SelectedValue, ddlYear.SelectedValue, ddlMonth.SelectedValue);

                chkSamePrev.Checked = CommValue.AUTH_VALUE_FALSE;

                LoadData();
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
                // 세션체크
                AuthCheckLib.CheckSession();

                int intRowCnt = lvMonthParkingFeeList.Items.Count;

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                string strYear = ddlYear.SelectedValue;
                string strMonth = ddlMonth.SelectedValue;
                string strCarTyCd = string.Empty;

                bool isNoExist = CommValue.AUTH_VALUE_TRUE;
                string strFee = string.Empty;
                double dblFee = 0d;
                string strApplyDt = string.Empty;
                string strCompNo = string.Empty;

                for (int intTmpI = 0; intTmpI < intRowCnt; intTmpI++)
                {
                    strCarTyCd = ((TextBox)lvMonthParkingFeeList.Items[intTmpI].FindControl("txtHfCarTy")).Text;
                    strFee = ((TextBox)lvMonthParkingFeeList.Items[intTmpI].FindControl("txtFee")).Text;
                    strApplyDt = ((HiddenField)lvMonthParkingFeeList.Items[intTmpI].FindControl("hfApplyDt")).Value.Replace(".", "").Replace("-", "");
                    strCompNo = Session["CompCd"].ToString();

                    if (string.IsNullOrEmpty(strFee))
                    {
                        dblFee = CommValue.NUMBER_VALUE_0_0;
                    }
                    else
                    {
                        dblFee = double.Parse(strFee);
                    }

                    // KN_USP_PRK_SELECT_MONTHPARKINGFEEINFO_S00
                    DataTable dtReturn = ParkingMngBlo.WatchMonthParkingFeeCheck(ddlRentNm.SelectedValue, strYear, strMonth, strCarTyCd);

                    if (dtReturn != null)
                    {
                        if (dtReturn.Rows.Count > 0)
                        {
                            if (Int32.Parse(dtReturn.Rows[0]["ExistCnt"].ToString()) > 0)
                            {
                                hfAlertText.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];
                                isNoExist = CommValue.AUTH_VALUE_FALSE;
                                break;
                            }
                            else
                            {
                                // KN_USP_PRK_INSERT_MONTHPARKINGFEEINFO_M00
                                ParkingMngBlo.RegistryMonthParkingFee(ddlRentNm.SelectedValue, strYear, strMonth, strCarTyCd, dblFee, strApplyDt, strCompNo, Session["MemNo"].ToString(), strInsMemIP);
                            }
                        }
                    }
                }

                if (isNoExist)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}