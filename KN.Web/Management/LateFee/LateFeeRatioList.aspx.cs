using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Manage.Biz;

namespace KN.Web.Management.LateFee
{
    public partial class LateFeeRatioList : BasePage
    {
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
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
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

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            //if( !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2]))
            //{
            //    txtHfLateFeeSeq.Text = Request.Params[Master.PARAM_DATA2].ToString();
            //    isReturnOk = CommValue.AUTH_VALUE_TRUE;
            //}

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltLateDay.Text = TextNm["LATEFEEDAY"];
            ltLateRatio.Text = TextNm["LATEFEERATIO"];
            ltStartDt.Text = TextNm["APPLYDT"];

            txtLateStartDate.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLateEndDate.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtLateFeeRatio.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            imgbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnRegist.Visible = Master.isWriteAuthOk;
            lnkbtnEntireReset.Text = TextNm["ENTIRE"] + " " + TextNm["RESET"];
            lnkbtnEntireReset.Visible = Master.isModDelAuthOk;
            lnkbtnEntireReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ALL_LATEFEERATIO"] + "');";
        }

        protected void LoadData()
        {
            DataTable dtListReturn = new DataTable();

            // KN_USP_MNG_SELECT_LATEFEERATIOINFO_S00
            dtListReturn = MngPaymentBlo.SpreadLateFeeRatioList(txtHfRentCd.Text, txtHfFeeTy.Text);

            if (dtListReturn != null)
            {
                lvLateFeeRationList.DataSource = dtListReturn;
                lvLateFeeRationList.DataBind();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLateFeeRationList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvLateFeeRationList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["LateFeeSeq"].ToString()))
                {
                    TextBox txtHfLateFeeSeq = (TextBox)iTem.FindControl("txtHfLateFeeSeq");
                    txtHfLateFeeSeq.Text = drView["LateFeeSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["LateFeeStartDay"].ToString()))
                {
                    TextBox txtLateStartDate = (TextBox)iTem.FindControl("txtLateStartDate");
                    txtLateStartDate.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                    txtLateStartDate.Text = drView["LateFeeStartDay"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["LateFeeEndDay"].ToString()))
                {
                    TextBox txtLateEndDate = (TextBox)iTem.FindControl("txtLateEndDate");
                    txtLateEndDate.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                    txtLateEndDate.Text = drView["LateFeeEndDay"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["LateFeeRatio"].ToString()))
                {
                    TextBox txtLateFeeRatio = (TextBox)iTem.FindControl("txtLateFeeRatio");
                    txtLateFeeRatio.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtLateFeeRatio.Text = drView["LateFeeRatio"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["LateFeeStartDt"].ToString()))
                {
                    string strDate = drView["LateFeeStartDt"].ToString();

                    TextBox txtInsDt = (TextBox)iTem.FindControl("txtInsDt");
                    txtInsDt.Text = drView["LateFeeStartDt"].ToString();
                    HiddenField hfInsDt = (HiddenField)iTem.FindControl("hfInsDt");
                    TextBox txtHfOriginDt = (TextBox)iTem.FindControl("txtHfOriginDt");

                    Literal ltInsDt = (Literal)iTem.FindControl("ltInsDt");

                    txtInsDt.Text = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2);
                    hfInsDt.Value = strDate;
                    txtHfOriginDt.Text = strDate;

                    ltInsDt.Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"ContCalendar(this, '" + txtInsDt.ClientID + "','" + hfInsDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.Visible = Master.isModDelAuthOk;
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.Visible = Master.isModDelAuthOk;
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
            }
        }

        protected void imgbtnRegist_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intLateStartDate = Int32.Parse(txtLateStartDate.Text);
                int intLateEndDate = Int32.Parse(txtLateEndDate.Text);
                double fltLateFeeRatio = double.Parse(txtLateFeeRatio.Text);

                string strInsDt = hfInsDt.Value.Replace("-", "");

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_MNG_SELECT_LATEFEERATIOINFO_S01
                DataTable dtReturn = MngPaymentBlo.SpreadLateFeeStartDtCheck(txtHfRentCd.Text, txtHfFeeTy.Text, strInsDt);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        if (Int32.Parse(dtReturn.Rows[0]["ExistCnt"].ToString()) > 0)
                        {
                            hfAlertText.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];

                            LoadData();
                        }
                        else
                        {
                            // KN_USP_MNG_INSERT_LATEFEERATIOINFO_M00
                            MngPaymentBlo.RegistryLateFeeRatioList(txtHfRentCd.Text, txtHfFeeTy.Text, intLateStartDate, intLateEndDate, strInsDt, fltLateFeeRatio, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                            Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvLateFeeRationList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfLateFeeSeq = (TextBox)lvLateFeeRationList.Items[e.ItemIndex].FindControl("txtHfLateFeeSeq");
                int intLateFeeSeq = Int32.Parse(txtHfLateFeeSeq.Text);

                // KN_USP_MNG_DELETE_LATEFEERATIOINFO_M00
                MngPaymentBlo.RemoveLateFeeRatio(txtHfRentCd.Text, txtHfFeeTy.Text, intLateFeeSeq);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvLateFeeRationList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfLateFeeSeq = (TextBox)lvLateFeeRationList.Items[e.ItemIndex].FindControl("txtHfLateFeeSeq");
                int intLateFeeSeq = Int32.Parse(txtHfLateFeeSeq.Text);

                TextBox txtLateStartDay = (TextBox)lvLateFeeRationList.Items[e.ItemIndex].FindControl("txtLateStartDate");
                TextBox txtLateEndDay = (TextBox)lvLateFeeRationList.Items[e.ItemIndex].FindControl("txtLateEndDate");
                TextBox txtLateStartDt = (TextBox)lvLateFeeRationList.Items[e.ItemIndex].FindControl("txtHfOriginDt");
                TextBox txtLateFeeRatio = (TextBox)lvLateFeeRationList.Items[e.ItemIndex].FindControl("txtLateFeeRatio");

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_MNG_UPDATE_LATEFEERATIOINFO_M00
                MngPaymentBlo.ModifyLateFeeRatioList(txtHfRentCd.Text, txtHfFeeTy.Text, intLateFeeSeq, Int32.Parse(txtLateStartDay.Text), Int32.Parse(txtLateEndDay.Text), txtLateStartDt.Text, double.Parse(txtLateFeeRatio.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnEntireReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_MNG_DELETE_LATEFEERATIOINFO_M01
                MngPaymentBlo.RemoveLateFeeRatio(txtHfRentCd.Text, txtHfFeeTy.Text);

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}