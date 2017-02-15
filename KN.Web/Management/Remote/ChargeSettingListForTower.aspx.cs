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
using KN.Resident.Biz;

namespace KN.Web.Management.Remote
{
    public partial class ChargeSettingListForTower : BasePage
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
            ltCommon.Text = TextNm["GENERAL"];
            ltFirstSeq.Text = TextNm["SEQ"];
            ltFirstGenCharge.Text = TextNm["AVERAGEHOUR"];
            ltFirstPeakCharge.Text = TextNm["RUSHHOUR"];
            ltFirstNightCharge.Text = TextNm["LOWHOUR"];
            ltFirstStartDt.Text = TextNm["APPLYDT"];

            ltIndividual.Text = TextNm["INDIVIDUAL"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltSecondSeq.Text = TextNm["SEQ"];
            ltSecondGenCharge.Text = TextNm["AVERAGEHOUR"];
            ltSecondPeakCharge.Text = TextNm["RUSHHOUR"];
            ltSecondNightCharge.Text = TextNm["LOWHOUR"];
            ltSecondStartDt.Text = TextNm["APPLYDT"];

            txtFirstGenCharge.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFirstPeakCharge.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFirstNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            txtSecondGenCharge.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtSecondPeakCharge.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtSecondNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            imgbtnRegist.OnClientClick = "javascript:return fnFirstCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnRegist.Visible = Master.isWriteAuthOk;

            imgbtnInput.OnClientClick = "javascript:return fnSecondCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnInput.Visible = Master.isWriteAuthOk;

            //매매기준율환율정보
            ltFirstBaseRate.Text = TextNm["BASERATE"];
            LoadExchageDate();

            LoadRoomNo();
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
        
        protected void LoadRoomNo()
        {
            DataTable dtReturn = new DataTable();

            string strNowDt = DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", "");

            // KN_USP_RES_SELECT_ROOMINFO_S08
            dtReturn = RoomMngBlo.SelectRoomInfo(txtHfRentCd.Text, "", strNowDt);

            if (dtReturn != null)
            {
                ddlRoomNo.Items.Clear();
                ddlRoomNo.Items.Add(new ListItem(TextNm["ROOMNO"], ""));

                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    foreach (DataRow dr in dtReturn.Select())
                    {
                        ddlRoomNo.Items.Add(new ListItem(dr["RoomNo"].ToString(), dr["RoomNo"].ToString()));
                    }
                }
            }
        }

        protected void LoadData()
        {
            DataSet dsListReturn = new DataSet();

            // KN_USP_MNG_SELECT_UTILCHARGEINFO_S03
            dsListReturn = RemoteMngBlo.SelectUtilChargeSetInfoList(txtHfRentCd.Text, txtHfChargeTy.Text);

            if (dsListReturn != null)
            {
                lvChargeInfoList.DataSource = dsListReturn.Tables[0];
                lvChargeInfoList.DataBind();

                lvChargelistForRoom.DataSource = dsListReturn.Tables[1];
                lvChargelistForRoom.DataBind();
            }
        }

        protected void LoadRemoteData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_UTILCHARGEINFO_S04
            dtReturn = RemoteMngBlo.SelectUtilChargeSetInfoList(txtHfRentCd.Text, ddlRoomNo.SelectedValue, txtHfChargeTy.Text);

            lvChargelistForRoom.DataSource = dtReturn;
            lvChargelistForRoom.DataBind();
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
                    Literal ltSeq = (Literal)iTem.FindControl("ltSeq");
                    ltSeq.Text = drView["ChargeSeq"].ToString();

                    TextBox txtHfChargeSeq = (TextBox)iTem.FindControl("txtHfChargeSeq");
                    txtHfChargeSeq.Text = drView["ChargeSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["GenCharge"].ToString()))
                {
                    TextBox txtGenCharge = (TextBox)iTem.FindControl("txtGenCharge");
                    txtGenCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtGenCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["GenCharge"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["PeakCharge"].ToString()))
                {
                    TextBox txtPeakCharge = (TextBox)iTem.FindControl("txtPeakCharge");
                    txtPeakCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtPeakCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["PeakCharge"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["NightCharge"].ToString()))
                {
                    TextBox txtNightCharge = (TextBox)iTem.FindControl("txtNightCharge");
                    txtNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtNightCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["NightCharge"].ToString()).ToString("###,##0"));
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

                double dblGenCharge = CommValue.NUMBER_VALUE_0_00;
                double dblPeakCharge = CommValue.NUMBER_VALUE_0_00;
                double dblNightCharge = CommValue.NUMBER_VALUE_0_00;

                if (!string.IsNullOrEmpty(txtFirstGenCharge.Text))
                {
                    dblGenCharge = double.Parse(txtFirstGenCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtFirstPeakCharge.Text))
                {
                    dblPeakCharge = double.Parse(txtFirstPeakCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtFirstNightCharge.Text))
                {
                    dblNightCharge = double.Parse(txtFirstNightCharge.Text);
                }

                string strInsDt = hfFirstStartDt.Value.Replace("-", "");

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_MNG_INSERT_UTILCHARGEINFO_M00
                // KN_USP_MNG_INSERT_UTILCHARGEINFO_M01
                RemoteMngBlo.RegistryUtilChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text, dblGenCharge, dblPeakCharge, dblNightCharge, strInsDt, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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

                // KN_USP_MNG_DELETE_UTILCHARGEINFO_M00
                RemoteMngBlo.RemoveUtilChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text, intChargeSeq);

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

                double dblGenCharge = CommValue.NUMBER_VALUE_0_0;
                double dblPeakCharge = CommValue.NUMBER_VALUE_0_0;
                double dblNightCharge = CommValue.NUMBER_VALUE_0_0;

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                {
                    dblGenCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text.Replace(".",""));
                }

                if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                {
                    dblPeakCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text.Replace(".", ""));
                }

                if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                {
                    dblNightCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text.Replace(".", ""));
                }

                // KN_USP_MNG_UPDATE_UTILCHARGEINFO_M00
                RemoteMngBlo.ModifyUtilChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text, intChargeSeq, dblGenCharge, dblPeakCharge, dblNightCharge);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargelistForRoom_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvChargelistForRoom_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                    ltRoomNo.Text = drView["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ChargeSeq"].ToString()))
                {
                    Literal ltSeq = (Literal)iTem.FindControl("ltSeq");
                    ltSeq.Text = drView["ChargeSeq"].ToString();

                    TextBox txtHfChargeSeq = (TextBox)iTem.FindControl("txtHfChargeSeq");
                    txtHfChargeSeq.Text = drView["ChargeSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["GenCharge"].ToString()))
                {
                    TextBox txtGenCharge = (TextBox)iTem.FindControl("txtGenCharge");
                    txtGenCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtGenCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["GenCharge"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["PeakCharge"].ToString()))
                {
                    TextBox txtPeakCharge = (TextBox)iTem.FindControl("txtPeakCharge");
                    txtPeakCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtPeakCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["PeakCharge"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["NightCharge"].ToString()))
                {
                    TextBox txtNightCharge = (TextBox)iTem.FindControl("txtNightCharge");
                    txtNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtNightCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["NightCharge"].ToString()).ToString("###,##0"));
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

        protected void imgbtnInput_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                double dblGenCharge = CommValue.NUMBER_VALUE_0_00;
                double dblPeakCharge = CommValue.NUMBER_VALUE_0_00;
                double dblNightCharge = CommValue.NUMBER_VALUE_0_00;

                if (!string.IsNullOrEmpty(txtSecondGenCharge.Text))
                {
                    dblGenCharge = double.Parse(txtSecondGenCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtSecondPeakCharge.Text))
                {
                    dblPeakCharge = double.Parse(txtSecondPeakCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtSecondNightCharge.Text))
                {
                    dblNightCharge = double.Parse(txtSecondNightCharge.Text);
                }

                string strInsDt = hfSecondStartDt.Value.Replace("-", "");

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_MNG_INSERT_UTILCHARGEINFO_M03
                RemoteMngBlo.InsertUtilChargeInfoForRoom(txtHfRentCd.Text, ddlRoomNo.SelectedValue, txtHfChargeTy.Text, dblGenCharge, dblPeakCharge, dblNightCharge, strInsDt,
                                                         Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargelistForRoom_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfChargeSeq = (TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtHfChargeSeq");
                int intChargeSeq = Int32.Parse(txtHfChargeSeq.Text);

                // KN_USP_MNG_DELETE_UTILCHARGEINFO_M00
                RemoteMngBlo.RemoveUtilChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text, intChargeSeq);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargelistForRoom_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfChargeSeq = (TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtHfChargeSeq");
                int intChargeSeq = Int32.Parse(txtHfChargeSeq.Text);

                double dblGenCharge = CommValue.NUMBER_VALUE_0_0;
                double dblPeakCharge = CommValue.NUMBER_VALUE_0_0;
                double dblNightCharge = CommValue.NUMBER_VALUE_0_0;

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                {
                    dblGenCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text.Replace(".", ""));
                }

                if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                {
                    dblPeakCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text.Replace(".", ""));
                }

                if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                {
                    dblNightCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text.Replace(".", ""));
                }

                // KN_USP_MNG_UPDATE_UTILCHARGEINFO_M00
                RemoteMngBlo.ModifyUtilChargeInfo(txtHfRentCd.Text, txtHfChargeTy.Text, intChargeSeq, dblGenCharge, dblPeakCharge, dblNightCharge);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRemoteData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}