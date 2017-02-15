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
    public partial class MngModifyMonthForRoom : BasePage
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
                txtHfYear.Text = Request.Params[Master.PARAM_DATA3].ToString();
                txtHfMonth.Text = Request.Params[Master.PARAM_DATA4].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltFloor.Text = TextNm["FLOOR"];
            ltRoom.Text = TextNm["ROOMNO"];

            ltTopFloorRoom.Text = TextNm["FLOOR"] + " / " + TextNm["ROOMNO"];
            ltTopYearMonth.Text = TextNm["YEARS"] + " / " + TextNm["MONTH"];
            ltTopGenUse.Text = TextNm["GENERAL"];
            ltTopPeakUse.Text = TextNm["PEAK"];
            ltTopNightUse.Text = TextNm["NIGHT"];
            ltTopACUse.Text = TextNm["ACAMT"];
            ltTopHVACUse.Text = TextNm["HVACAMT"];

            ltTopGenVal.Text = TextNm["GENERAL"];
            ltTopPeakVal.Text = TextNm["PEAK"];
            ltTopNightVal.Text = TextNm["NIGHT"];
            ltTopACVal.Text = TextNm["ACAMT"];
            ltTopHVACVal.Text = TextNm["HVACAMT"];

            ltTopAccmulated.Text = TextNm["ACCUMULATED"];
            ltTopAmounted.Text = TextNm["AMOUNTUSED"];

            ltMonth.Text = TextNm["MONTH"];
            lnkbtnDay.Text = TextNm["DAYS"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            MakeYearDdl();
            MakeMonthDdl();

            lnkbtnModify.Text = TextNm["SAVE"];
            lnkbtnList.Text = TextNm["LIST"];

            txtSearchFloor.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            ddlYear.SelectedValue = txtHfYear.Text;
            ddlMonth.SelectedValue = txtHfMonth.Text;

            ddlYear.Enabled = CommValue.AUTH_VALUE_FALSE;
            ddlMonth.Enabled = CommValue.AUTH_VALUE_FALSE;
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_MNG_SELECT_MONTHENERGYFORTOWER_S00
            // KN_USP_MNG_SELECT_MONTHENERGYFORTOWER_S01
            dsReturn = RemoteMngBlo.SelectMonthChargeListForKeangNam(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfChargeTy.Text, ddlYear.Text, ddlMonth.Text, txtSearchRoom.Text);

            if (dsReturn != null)
            {
                lvMonthChargeList.DataSource = dsReturn.Tables[1];
                lvMonthChargeList.DataBind();

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
        protected void lvMonthChargeList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvMonthChargeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    TextBox txtHfRentCd = (TextBox)iTem.FindControl("txtHfRentCd");
                    txtHfRentCd.Text = drView["RentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                    {
                        Literal ltFloorRoomList = (Literal)iTem.FindControl("ltFloorRoomList");
                        TextBox txtHfFloorNo = (TextBox)iTem.FindControl("txtHfFloorNo");
                        TextBox txtHfRoomNo = (TextBox)iTem.FindControl("txtHfRoomNo");

                        ltFloorRoomList.Text = TextLib.StringDecoder(drView["FloorNo"].ToString()) + " / " + TextLib.StringDecoder(drView["RoomNo"].ToString());

                        txtHfFloorNo.Text = drView["FloorNo"].ToString();
                        txtHfRoomNo.Text = drView["RoomNo"].ToString();
                    }
                }

                Literal ltYearMonthList = (Literal)iTem.FindControl("ltYearMonthList");

                if (!string.IsNullOrEmpty(drView["EnergyMonth"].ToString()))
                {
                    string strDate = drView["EnergyMonth"].ToString();
                    ltYearMonthList.Text = strDate.Substring(0, 4) + " / " + strDate.Substring(4, 2);

                    TextBox txtHfYearMonth = (TextBox)iTem.FindControl("txtHfYearMonth");
                    txtHfYearMonth.Text = drView["EnergyMonth"].ToString();
                }

                TextBox txtGenUse = (TextBox)iTem.FindControl("txtGenUse");
                TextBox txtPeakUse = (TextBox)iTem.FindControl("txtPeakUse");
                TextBox txtNightUse = (TextBox)iTem.FindControl("txtNightUse");
                TextBox txtACUse = (TextBox)iTem.FindControl("txtACUse");
                TextBox txtHVACUse = (TextBox)iTem.FindControl("txtHVACUse");

                TextBox txtGenVal = (TextBox)iTem.FindControl("txtGenVal");
                TextBox txtPeakVal = (TextBox)iTem.FindControl("txtPeakVal");
                TextBox txtNightVal = (TextBox)iTem.FindControl("txtNightVal");
                TextBox txtACVal = (TextBox)iTem.FindControl("txtACVal");
                TextBox txtHVACVal = (TextBox)iTem.FindControl("txtHVACVal");

                if (!string.IsNullOrEmpty(drView["GenVal"].ToString()))
                {
                    txtGenVal.Text = double.Parse(drView["GenVal"].ToString()).ToString("###,##0.##");
                    txtGenVal.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["PeakVal"].ToString()))
                {
                    txtPeakVal.Text = double.Parse(drView["PeakVal"].ToString()).ToString("###,##0.##");
                    txtPeakVal.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["NightVal"].ToString()))
                {
                    txtNightVal.Text = double.Parse(drView["NightVal"].ToString()).ToString("###,##0.##");
                    txtNightVal.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["ACVal"].ToString()))
                {
                    txtACVal.Text = double.Parse(drView["ACVal"].ToString()).ToString("###,##0.##");
                    txtACVal.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["HVACVal"].ToString()))
                {
                    txtHVACVal.Text = double.Parse(drView["HVACVal"].ToString()).ToString("###,##0.##");
                    txtHVACVal.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["GenUse"].ToString()))
                {
                    txtGenUse.Text = double.Parse(drView["GenUse"].ToString()).ToString("###,##0.##");
                    txtGenUse.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["PeakUse"].ToString()))
                {
                    txtPeakUse.Text = double.Parse(drView["PeakUse"].ToString()).ToString("###,##0.##");
                    txtPeakUse.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["NightUse"].ToString()))
                {
                    txtNightUse.Text = double.Parse(drView["NightUse"].ToString()).ToString("###,##0.##");
                    txtNightUse.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["ACUse"].ToString()))
                {
                    txtACUse.Text = double.Parse(drView["ACUse"].ToString()).ToString("###,##0.##");
                    txtACUse.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["HVACUse"].ToString()))
                {
                    txtHVACUse.Text = double.Parse(drView["HVACUse"].ToString()).ToString("###,##0.##");
                    txtHVACUse.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
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

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                for (int intTmpI = 0; intTmpI < lvMonthChargeList.Items.Count; intTmpI++)
                {
                    TextBox txtHfRentCd = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHfRentCd");
                    TextBox txtHfFloorNo = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHfFloorNo");
                    TextBox txtHfRoomNo = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHfRoomNo");
                    TextBox txtHfYearMonth = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHfYearMonth");

                    TextBox txtGenUse = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtGenUse");
                    TextBox txtPeakUse = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtPeakUse");
                    TextBox txtNightUse = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtNightUse");
                    TextBox txtACUse = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtACUse");
                    TextBox txtHVACUse = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHVACUse");

                    TextBox txtGenVal = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtGenVal");
                    TextBox txtPeakVal = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtPeakVal");
                    TextBox txtNightVal = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtNightVal");
                    TextBox txtACVal = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtACVal");
                    TextBox txtHVACVal = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHVACVal");

                    double dblGenUse = CommValue.NUMBER_VALUE_0_0;
                    double dblGenVal = CommValue.NUMBER_VALUE_0_0;
                    double dblPeakUse = CommValue.NUMBER_VALUE_0_0;
                    double dblPeakVal = CommValue.NUMBER_VALUE_0_0;
                    double dblNightUse = CommValue.NUMBER_VALUE_0_0;
                    double dblNightVal = CommValue.NUMBER_VALUE_0_0;
                    double dblACUse = CommValue.NUMBER_VALUE_0_0;
                    double dblACVal = CommValue.NUMBER_VALUE_0_0;
                    double dblHVACUse = CommValue.NUMBER_VALUE_0_0;
                    double dblHVACVal = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(txtGenUse.Text))
                    {
                        dblGenUse = double.Parse(txtGenUse.Text);
                    }

                    if (!string.IsNullOrEmpty(txtGenVal.Text))
                    {
                        dblGenVal = double.Parse(txtGenVal.Text);
                    }

                    if (!string.IsNullOrEmpty(txtPeakUse.Text))
                    {
                        dblPeakUse = double.Parse(txtPeakUse.Text);
                    }

                    if (!string.IsNullOrEmpty(txtACUse.Text))
                    {
                        dblACUse = double.Parse(txtACUse.Text);
                    }

                    if (!string.IsNullOrEmpty(txtHVACUse.Text))
                    {
                        dblHVACUse = double.Parse(txtHVACUse.Text);
                    }

                    if (!string.IsNullOrEmpty(txtPeakVal.Text))
                    {
                        dblPeakVal = double.Parse(txtPeakVal.Text);
                    }

                    if (!string.IsNullOrEmpty(txtNightUse.Text))
                    {
                        dblNightUse = double.Parse(txtNightUse.Text);
                    }

                    if (!string.IsNullOrEmpty(txtNightVal.Text))
                    {
                        dblNightVal = double.Parse(txtNightVal.Text);
                    }

                    if (!string.IsNullOrEmpty(txtACVal.Text))
                    {
                        dblACVal = double.Parse(txtACVal.Text);
                    }

                    if (!string.IsNullOrEmpty(txtHVACVal.Text))
                    {
                        dblHVACVal = double.Parse(txtHVACVal.Text);
                    }

                    // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M00
                    RemoteMngBlo.ModifyMonthChargeListForTower(txtHfRentCd.Text, txtHfYearMonth.Text, txtHfFloorNo.Text, txtHfRoomNo.Text, txtHfChargeTy.Text, dblGenVal,
                                                               dblGenUse, dblPeakVal, dblPeakUse, dblNightVal, dblNightUse, dblACVal, dblACUse, dblHVACVal, dblHVACUse);
                }

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDay_Click(object sender, EventArgs e)
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

        protected void lnkbtnList_Click(object sender, EventArgs e)
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

                Response.Redirect(Master.PAGE_POPUP1 + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}