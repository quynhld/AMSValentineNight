using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;
using KN.Manage.Biz;

namespace KN.Web.Management.Remote
{
    public partial class ChargeSettingList : BasePage
    {
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
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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
                txtHfChargeTy.Text = Request.Params[Master.PARAM_DATA2].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {

            ltAmountUsed.Text = TextNm["AMOUNTUSED"];
            ltCharge.Text = TextNm["PAYMENT"];
            ltStartDt.Text = TextNm["APPLYDT"];

            txtAmountStart.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtAmountEnd.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            imgbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnRegist.Visible = Master.isWriteAuthOk;
            lnkbtnEntireReset.Text = TextNm["ENTIRE"] + " " + TextNm["RESET"];
            lnkbtnEntireReset.Visible = Master.isModDelAuthOk;
            lnkbtnEntireReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ALL_LATEFEERATIO"] + "');";

            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];
            LoadExchageDate();
            //hfStartDt.Value =  txtStartDt.Text.Replace("-", "").Replace(".", "");

        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // 가장 최근의 환율을 조회함.
            dtReturn = ExchangeMngBlo.WatchExchangeRateLastInfo(CommValue.RENTAL_VALUE_PARKING);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0") + "&nbsp;" + TextNm["DONG"].ToString());
                        hfRealBaseRate.Value = dtReturn.Rows[0]["DongToDollar"].ToString();
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                    }
                }
                else
                {
                    ltRealBaseRate.Text = "-";
                }
            }
        }

        protected void LoadData()
        {
            DataTable dtListReturn = new DataTable();

            // KN_USP_MNG_SELECT_CHARGEINFO_S00
            dtListReturn = RemoteMngBlo.SpreadChargeInfoList(txtHfRentCd.Text, txtHfChargeTy.Text);

            if (dtListReturn != null)
            {
                lvChargeInfoList.DataSource = dtListReturn;
                lvChargeInfoList.DataBind();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvChargeInfoList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvChargeInfoList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["ChargeSeq"].ToString()))
                {
                    TextBox txtHfChargeSeq = (TextBox)iTem.FindControl("txtHfChargeSeq");
                    txtHfChargeSeq.Text = drView["ChargeSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AmountStart"].ToString()))
                {
                    TextBox txtAmountStart = (TextBox)iTem.FindControl("txtAmountStart");
                    txtAmountStart.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                    txtAmountStart.Text = drView["AmountStart"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AmountEnd"].ToString()))
                {
                    TextBox txtAmountEnd = (TextBox)iTem.FindControl("txtAmountEnd");
                    txtAmountEnd.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                    txtAmountEnd.Text = drView["AmountEnd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["Charge"].ToString()))
                {
                    TextBox txtCharge = (TextBox)iTem.FindControl("txtCharge");
                    txtCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["Charge"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["ChargeStartDt"].ToString()))
                {
                    string strDate = drView["ChargeStartDt"].ToString();

                    TextBox txtStartDt = (TextBox)iTem.FindControl("txtStartDt");
                    txtStartDt.Text = drView["ChargeStartDt"].ToString();
                    HiddenField hfStartDt = (HiddenField)iTem.FindControl("hfStartDt");
                    TextBox txtHfOriginDt = (TextBox)iTem.FindControl("txtHfOriginDt");

                    Literal ltStartDt = (Literal)iTem.FindControl("ltStartDt");

                    txtStartDt.Text = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2);
                    hfStartDt.Value = strDate;
                    txtHfOriginDt.Text = strDate;

                    StringBuilder sbInsdDt = new StringBuilder();

                    sbInsdDt.Append("<a href='#'><img align='absmiddle' onclick=\"Calendar(this, '");
                    sbInsdDt.Append(txtStartDt.ClientID);
                    sbInsdDt.Append("', '");
                    sbInsdDt.Append(hfStartDt.ClientID);
                    sbInsdDt.Append("', CommValue.AUTH_VALUE_TRUE)\" src='/Common/Images/Common/calendar.gif' style='cursor:pointer;' value='' /></a>");

                    ltStartDt.Text = sbInsdDt.ToString();
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

                int intAmountStart = Int32.Parse(txtAmountStart.Text);
                int intAmountEnd = Int32.Parse(txtAmountEnd.Text);
                double fltCharge = double.Parse(txtCharge.Text);

                string strInsDt = hfStartDt.Value.Replace("-", "").Replace(".", "");
                //string strInsDt = txtStartDt.Text.Replace("-", "").Replace(".", "");

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_MNG_SELECT_CHARGEINFO_S01
                DataTable dtReturn = RemoteMngBlo.WatchChargeStartDtCheck(txtHfRentCd.Text, txtHfChargeTy.Text, strInsDt);

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
                            // KN_USP_MNG_INSERT_CHARGEINFO_M00
                            // KN_USP_MNG_INSERT_CHARGEINFO_M01
                            // KN_USP_MNG_INSERT_CHARGEINFO_M02
                            RemoteMngBlo.RegistryChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text, intAmountStart, intAmountEnd, strInsDt, fltCharge, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                            Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }

        protected void lvChargeInfoList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfChargeSeq = (TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtHfChargeSeq");
                int intChargeSeq = Int32.Parse(txtHfChargeSeq.Text);

                // KN_USP_MNG_DELETE_CHARGEINFO_M00
                RemoteMngBlo.RemoveChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text, intChargeSeq);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargeInfoList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfChargeSeq = (TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtHfChargeSeq");
                int intChargeSeq = Int32.Parse(txtHfChargeSeq.Text);

                var dbCharge = CommValue.NUMBER_VALUE_0_0;

                if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtCharge")).Text))
                {
                    dbCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtCharge")).Text.Replace(".", ""));
                }

                TextBox txtAmountStart = (TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtAmountStart");
                TextBox txtAmountEnd = (TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtAmountEnd");
                TextBox txtStartDt = (TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtHfOriginDt");
                //TextBox txtCharge = (TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtCharge");

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_MNG_UPDATE_CHARGEINFO_M00
                RemoteMngBlo.ModifyChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text, intChargeSeq, Int32.Parse(txtAmountStart.Text), Int32.Parse(txtAmountEnd.Text), txtStartDt.Text, dbCharge, Session["MemNo"].ToString(), strInsMemIP);
                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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

                // KN_USP_MNG_DELETE_CHARGEINFO_M01
                RemoteMngBlo.RemoveChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text);

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtStartDt_TextChanged(object sender, EventArgs e)
        {
            hfStartDt.Value = txtStartDt.Text.Replace("-", "").Replace(".", "");
        }
    }
}