using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;

namespace KN.Web.Management.Remote
{
    public partial class MngModifyDay : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;

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

                        if (Request.Params[Master.PARAM_DATA3] != null)
                        {
                            ddlYear.SelectedValue = Request.Params[Master.PARAM_DATA3].ToString();
                        }

                        if (Request.Params[Master.PARAM_DATA4] != null)
                        {
                            ddlMonth.SelectedValue = Request.Params[Master.PARAM_DATA4].ToString();
                        }

                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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

            if (!string.IsNullOrEmpty(hfCurrentPage.Value))
            {
                intPageNo = Int32.Parse(hfCurrentPage.Value);
                hfCurrentPage.Value = intPageNo.ToString();
            }
            else
            {
                intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfCurrentPage.Value = intPageNo.ToString();
            }

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
            ltFloorRoom.Text = TextNm["FLOOR"] + " / " + TextNm["ROOMNO"];
            ltAmountUsed.Text = TextNm["AMOUNTUSED"];
            ltCharge.Text = TextNm["PAYMENT"];
            ltDay.Text = TextNm["DAYS"];

            ltFloor.Text = TextNm["FLOOR"];
            ltRoom.Text = TextNm["ROOMNO"];

            lnkbtnYear.Text = TextNm["YEARS"];
            lnkbtnMonth.Text = TextNm["MONTH"];
            lnkbtnDay1.Text = TextNm["DAYS"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            MakeYearDdl();
            MakeMonthDdl();

            lnkbtList.Text = TextNm["LIST"];

            txtSearchFloor.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            int intSearchFloor = 0;

            if (!string.IsNullOrEmpty(txtSearchFloor.Text))
            {
                intSearchFloor = Int32.Parse(txtSearchFloor.Text);
            }

            // KN_USP_MNG_SELECT_CHARGEINFO_S05
            dsReturn = RemoteMngBlo.SpreadDayUseChargeList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfChargeTy.Text, ddlYear.Text, ddlMonth.Text, intSearchFloor, txtSearchRoom.Text);

            if (dsReturn != null)
            {
                lvDayChargeList.DataSource = dsReturn.Tables[1];
                lvDayChargeList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvDayChargeList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvDayChargeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                    {
                        Literal ltFloorRoomList = (Literal)iTem.FindControl("ltFloorRoomList");
                        ltFloorRoomList.Text = TextLib.StringDecoder(drView["FloorNo"].ToString()) + " / " + TextLib.StringDecoder(drView["RoomNo"].ToString());

                        TextBox txtHfFloor = (TextBox)iTem.FindControl("txtHfFloor");
                        txtHfFloor.Text = drView["FloorNo"].ToString();

                        TextBox txtHfRoom = (TextBox)iTem.FindControl("txtHfRoom");
                        txtHfRoom.Text = drView["RoomNo"].ToString();

                        TextBox txtHfRentCd = (TextBox)iTem.FindControl("txtHfRentCd");
                        txtHfRentCd.Text = drView["RentCd"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(drView["EnergyDay"].ToString()))
                {
                    string strDate = drView["EnergyDay"].ToString();
                    Literal ltDayList = (Literal)iTem.FindControl("ltDayList");
                    ltDayList.Text = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2);
                }

                if (!string.IsNullOrEmpty(drView["TotalUse"].ToString()))
                {
                    TextBox txtAmountUsedList = (TextBox)iTem.FindControl("txtAmountUsedList");
                    txtAmountUsedList.Text = drView["TotalUse"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TotalCharge"].ToString()))
                {
                    Literal ltChargeList = (Literal)iTem.FindControl("ltChargeList");
                    ltChargeList.Text = TextLib.MakeVietIntNo(double.Parse(drView["TotalCharge"].ToString()).ToString("###,##0"));
                }
            }
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

        /// <summary>
        /// 검색버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                LoadData();
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

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvDayChargeList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intSearchFloor = 0;

                if (!string.IsNullOrEmpty(txtSearchFloor.Text))
                {
                    intSearchFloor = Int32.Parse(txtSearchFloor.Text);
                }

                TextBox txtAmountUsedList = (TextBox)lvDayChargeList.Items[e.ItemIndex].FindControl("txtAmountUsedList");
                Literal ltDayList = (Literal)lvDayChargeList.Items[e.ItemIndex].FindControl("ltDayList");
                TextBox txtHfFloor = (TextBox)lvDayChargeList.Items[e.ItemIndex].FindControl("txtHfFloor");
                TextBox txtHfRoom = (TextBox)lvDayChargeList.Items[e.ItemIndex].FindControl("txtHfRoom");
                TextBox txtHfRentCd = (TextBox)lvDayChargeList.Items[e.ItemIndex].FindControl("txtHfRentCd");
                HiddenField hfFloor = (HiddenField)lvDayChargeList.Items[e.ItemIndex].FindControl("hfFloor");
                HiddenField hfRoom = (HiddenField)lvDayChargeList.Items[e.ItemIndex].FindControl("hfRoom");

                string strDay = ltDayList.Text.Replace("-", "");

                RemoteMngBlo.ModifyAmountUse(txtHfRentCd.Text, txtHfChargeTy.Text, strDay, Int32.Parse(txtHfFloor.Text), txtHfRoom.Text, txtAmountUsedList.Text);

                StringBuilder sbView = new StringBuilder();

                sbView.Append(Master.PAGE_MODIFY);
                sbView.Append("?");
                sbView.Append(Master.PARAM_DATA1);
                sbView.Append("=");
                sbView.Append(txtHfRentCd.Text);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA2);
                sbView.Append("=");
                sbView.Append(txtHfChargeTy.Text);

                if (Master.PARAM_DATA3 != null)
                {
                    sbView.Append("&");
                    sbView.Append(Master.PARAM_DATA3);
                    sbView.Append("=");
                    sbView.Append(ddlYear.SelectedValue);
                }

                if (Master.PARAM_DATA4 != null)
                {
                    sbView.Append("&");
                    sbView.Append(Master.PARAM_DATA4);
                    sbView.Append("=");
                    sbView.Append(ddlMonth.SelectedValue);
                }

                Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 페이징버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
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

        protected void lnkbtnMonth_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnYear_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_POPUP2 + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}