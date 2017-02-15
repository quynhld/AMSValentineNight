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
    public partial class MngModifyMonth : BasePage
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
            ltFloorRoom.Text = TextNm["FLOOR"] + " / " + TextNm["ROOMNO"];
            ltAmountUsed.Text = TextNm["AMOUNTUSED"];
            ltCharge.Text = TextNm["PAYMENT"];
            ltYearMonth.Text = TextNm["YEARS"] + " / " + TextNm["MONTH"];

            ltFloor.Text = TextNm["FLOOR"];
            ltRoom.Text = TextNm["ROOMNO"];

            lnkbtnYear.Text = TextNm["YEARS"];
            ltMonth.Text = TextNm["MONTH"];
            lnkbtnDay.Text = TextNm["DAYS"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnGraph.Text = TextNm["GRAPH"];

            ltMngFeeNET.Text = TextNm["AMT"] + " (" + TextNm["NET"] + ")";
            ltMngFeeVAT.Text = TextNm["VAT"];

            MakeYearDdl();
            MakeMonthDdl();

            lnkbtnModify.Text = TextNm["MODIFY"];

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

            int intSearchFloor = 0;

            if (!string.IsNullOrEmpty(txtSearchFloor.Text))
            {
                intSearchFloor = Int32.Parse(txtSearchFloor.Text);
            }

            // KN_USP_MNG_SELECT_CHARGEINFO_S02
            dsReturn = RemoteMngBlo.SpreadMonthUseChargeList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfChargeTy.Text, ddlYear.Text, ddlMonth.Text, intSearchFloor, txtSearchRoom.Text);

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
                        ltFloorRoomList.Text = TextLib.StringDecoder(drView["FloorNo"].ToString()) + " / " + TextLib.StringDecoder(drView["RoomNo"].ToString());

                        TextBox txtHfRoomNo = (TextBox)iTem.FindControl("txtHfRoomNo");
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

                Literal ltAmountUsedList = (Literal)iTem.FindControl("ltAmountUsedList");
                TextBox txtAmountUsedList = (TextBox)iTem.FindControl("txtAmountUsedList");
                TextBox txtHfAmountUsedList = (TextBox)iTem.FindControl("txtHfAmountUsedList");

                if (!string.IsNullOrEmpty(drView["TotalUse"].ToString()))
                {
                    ltAmountUsedList.Text = double.Parse(drView["TotalUse"].ToString()).ToString("###,##0.##");
                    txtAmountUsedList.Text = double.Parse(drView["TotalUse"].ToString()).ToString("###,##0.##");
                    txtHfAmountUsedList.Text = double.Parse(drView["TotalUse"].ToString()).ToString("###,##0.##");
                }
                else
                {
                    txtHfAmountUsedList.Text = CommValue.NUMBER_VALUE_ZERO;
                    txtHfAmountUsedList.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["ReceitCd"].ToString()))
                {
                    if (drView["ReceitCd"].ToString().Equals(CommValue.PAYMENT_TYPE_VALUE_PAID))
                    {
                        txtAmountUsedList.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        txtAmountUsedList.Enabled = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                string strFeeVAT = string.Empty;

                if (!string.IsNullOrEmpty(drView["VatRatio"].ToString()))
                {
                    if (!drView["VatRatio"].ToString().Equals(CommValue.NUMBER_VALUE_0_00.ToString()))
                    {
                        strFeeVAT = drView["VatRatio"].ToString();
                    }
                    else
                    {
                        strFeeVAT = CommValue.NUMBER_VALUE_0_00.ToString();
                    }
                }

                Literal ltChargeList = (Literal)iTem.FindControl("ltChargeList");
                Literal ltInsMngFeeVAT = (Literal)iTem.FindControl("ltInsMngFeeVAT");
                Literal ltInsMngFeeNET = (Literal)iTem.FindControl("ltInsMngFeeNET");

                if (!string.IsNullOrEmpty(drView["TotalCharge"].ToString()))
                {
                    if (!double.Parse(drView["TotalCharge"].ToString()).Equals(CommValue.NUMBER_VALUE_0_00))
                    {
                        ltChargeList.Text = double.Parse(drView["TotalCharge"].ToString()).ToString("###,##0");
                        ltInsMngFeeVAT.Text = (double.Parse(drView["TotalCharge"].ToString()) * double.Parse(strFeeVAT) / (100 + double.Parse(strFeeVAT))).ToString("###,##0");
                        ltInsMngFeeNET.Text = (double.Parse(drView["TotalCharge"].ToString()) - (double.Parse(drView["TotalCharge"].ToString()) * double.Parse(strFeeVAT) / (100 + double.Parse(strFeeVAT)))).ToString("###,##0");
                    }
                    else
                    {
                        ltChargeList.Text = CommValue.NUMBER_VALUE_ZERO;
                        ltInsMngFeeVAT.Text = CommValue.NUMBER_VALUE_ZERO.ToString();
                        ltInsMngFeeNET.Text = CommValue.NUMBER_VALUE_ZERO.ToString();
                    }
                }
                else
                {
                    ltChargeList.Text = CommValue.NUMBER_VALUE_ZERO;
                    ltInsMngFeeVAT.Text = CommValue.NUMBER_VALUE_ZERO.ToString();
                    ltInsMngFeeNET.Text = CommValue.NUMBER_VALUE_ZERO.ToString();
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

                bool isChanged = CommValue.AUTH_VALUE_FALSE;

                for (int intTmpI = 0; intTmpI < lvMonthChargeList.Items.Count; intTmpI++)
                {
                    TextBox txtHfRentCd = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHfRentCd");
                    TextBox txtHfRoomNo = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHfRoomNo");
                    TextBox txtHfYearMonth = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHfYearMonth");
                    TextBox txtAmountUsedList = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtAmountUsedList");
                    TextBox txtHfAmountUsedList = (TextBox)lvMonthChargeList.Items[intTmpI].FindControl("txtHfAmountUsedList");

                    if (!txtAmountUsedList.Text.Equals(txtHfAmountUsedList.Text))
                    {
                        // 정보 수정 처리
                        if (string.IsNullOrEmpty(txtAmountUsedList.Text))
                        {
                            txtAmountUsedList.Text = CommValue.NUMBER_VALUE_0_00.ToString();
                        }

                        // KN_USP_MNG_UPDATE_MONTHENERGY_M01
                        RemoteMngBlo.ModifyMonthAmountUse(txtHfRentCd.Text, txtHfYearMonth.Text, txtHfRoomNo.Text, txtHfChargeTy.Text, txtAmountUsedList.Text);
                        isChanged = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                if (!isChanged)
                {
                    StringBuilder sbChanged = new StringBuilder();

                    sbChanged.Append("alert('" + AlertNm["INFO_HAS_NO_MODIFIED_ITEM"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoChanged", sbChanged.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    LoadData();
                }
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

                Response.Redirect(Master.PAGE_POPUP1 + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}