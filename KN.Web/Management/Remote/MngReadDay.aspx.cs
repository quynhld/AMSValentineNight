using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;

namespace KN.Web.Management.Remote
{
    public partial class MngReadDay : BasePage
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
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3]))
                {
                    txtHfEnergyYear.Text = Request.Params[Master.PARAM_DATA3].ToString();
                    txtHfEnergyMonth.Text = Request.Params[Master.PARAM_DATA4].ToString();
                }

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
            ltTabDay.Text = TextNm["DAYS"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnGraph.Text = TextNm["GRAPH"];

            ltMngFeeNET.Text = TextNm["AMT"] + " (" + TextNm["NET"] + ")";
            ltMngFeeVAT.Text = TextNm["VAT"];

            MakeYearDdl();
            MakeMonthDdl();
            //AlertRemote();

            lnkbtnModify.Text = TextNm["MODIFY"];

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
            dsReturn = RemoteMngBlo.SpreadDayUseChargeList(CommValue.BOARD_VALUE_ROOMSIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfChargeTy.Text, ddlYear.Text, ddlMonth.Text, intSearchFloor, txtSearchRoom.Text);

            if (dsReturn != null)
            {
                lvDayChargeList.DataSource = dsReturn.Tables[1];
                lvDayChargeList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_ROOMSIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
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
                    Literal ltUsedAmt = (Literal)iTem.FindControl("ltUsedAmt");
                    ltUsedAmt.Text = drView["TotalUse"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["VatRatio"].ToString()))
                {
                    Literal ltChargeVAT = (Literal)iTem.FindControl("ltChargeVAT");

                    int intChargeVAT = CommValue.NUMBER_VALUE_0;

                    string strChargeVAT = string.Empty;
                    string strFeeVAT = drView["VatRatio"].ToString();

                    ltChargeVAT.Text = TextLib.MakeVietIntNo((double.Parse(drView["TotalCharge"].ToString()) * double.Parse(strFeeVAT) / (100 + double.Parse(strFeeVAT))).ToString("###,##0"));

                    strChargeVAT = (double.Parse(drView["TotalCharge"].ToString()) * double.Parse(strFeeVAT) / (100 + double.Parse(strFeeVAT))).ToString("###,##0");

                    intChargeVAT = Int32.Parse(strChargeVAT.Replace(",", ""));

                    Literal ltChargeNET = (Literal)iTem.FindControl("ltChargeNET");
                    ltChargeNET.Text = TextLib.MakeVietIntNo((double.Parse(drView["TotalCharge"].ToString()) - intChargeVAT).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["TotalCharge"].ToString()))
                {
                    Literal ltCharge = (Literal)iTem.FindControl("ltCharge");
                    ltCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["TotalCharge"].ToString()).ToString("###,##0"));
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

            if (!string.IsNullOrEmpty(txtHfEnergyYear.Text))
            {
                ddlYear.SelectedValue = txtHfEnergyYear.Text;
            }
            else
            {
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
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
            if (!string.IsNullOrEmpty(txtHfEnergyMonth.Text))
            {
                ddlMonth.SelectedValue = txtHfEnergyMonth.Text;
            }
            else
            {
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
            }
        }

        private void AlertRemote()
        {
            //// KN_USP_MNG_SELECT_ENERGYMONTHCHECK_S00
            //DataTable dtRemoteWarn = RemoteMngBlo.WatchEnergyMonthCheck();

            //if (dtRemoteWarn != null)
            //{
            //    if (dtRemoteWarn.Rows.Count > CommValue.NUMBER_VALUE_0)
            //    {
            //        if (dtRemoteWarn.Rows[0]["RtnValue"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
            //        {
            //            ltEnergyWarning.Text = "<li><b><font color=\"red\" size=\"4\">" + AlertNm["ALERT_REMOTE_PROBLEMS"] + "</font></b></li>";
            //            imgbtnCheckData.Visible = Master.isModDelAuthOk;
            //        }
            //        else
            //        {
            //            ltEnergyWarning.Visible = CommValue.AUTH_VALUE_FALSE;
            //            imgbtnCheckData.Visible = CommValue.AUTH_VALUE_FALSE;
            //        }
            //    }
            //}
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

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text + "&" + Master.PARAM_DATA3 + "=" + ddlYear.SelectedValue + "&" + Master.PARAM_DATA4 + "=" + ddlMonth.SelectedValue, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnRecompile_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_AGT_MAKE_MONTHENERGY_LST_M03
                RemoteMngBlo.RegistryMonthEnergyPO();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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

        /// <summary>
        /// 그래프 보기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnGraph_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REPLY + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text + "&" + Master.PARAM_DATA3 + "=" + ddlYear.Text + "&" + Master.PARAM_DATA4 + "=" + ddlMonth.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}