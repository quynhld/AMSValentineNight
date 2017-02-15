using System;
using System.Data;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;

namespace KN.Web.Management.Manage
{
    public partial class MngManualSetList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CheckParam();

                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params["RentCd"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["RentCd"].ToString()))
                {
                    txtHfRentCd.Text = Request.Params["RentCd"].ToString();
                }
                else
                {
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
                }
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
            }
        }

        protected void InitControls()
        {
            ltYear.Text = TextNm["YEARS"];
            ltMonth.Text = TextNm["MONTH"];

            MakeYearDdl();
            MakeMonthDdl();
            MakeItemDdl();

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnMakingLine('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            ltTopPayment.Text = TextNm["PAYMENTKIND"];
            ltTopMngYYYYMM.Text = TextNm["MONTH"];
            ltTopRoomNo.Text = TextNm["ROOMNO"];
            ltTopMovieInDt.Text = TextNm["OCCUDT"];
            ltTopAmt.Text = TextNm["AMT"];

            lnkbtnMakeLine.Text = TextNm["MAKEBILL"];
            lnkbtnMakeLine.OnClientClick = "javascript:return fnMakingLine('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

        }

        protected void LoadData()
        {
            // KN_USP_MNG_SELECT_RENTALMNGFEE_S00
            // KN_USP_RES_SELECT_MONTHENERGY_S00
            DataTable dtReturn = MngPaymentBlo.SpreadManuallyRegistList(ddlItems.SelectedValue,"","","","", "",txtHfRentCd.Text, ddlYear.SelectedValue, ddlMonth.SelectedValue,"","");

            if (dtReturn != null)
            {
                lvMngManualList.DataSource = dtReturn;
                lvMngManualList.DataBind();
            }
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        protected void MakeYearDdl()
        {
            if (ddlItems.SelectedValue.Equals(CommValue.RECEIT_VALUE_MNGFEE))
            {
                MultiCdDdlUtil.MakeManuallyMngYear(ddlYear, txtHfRentCd.Text, TextNm["YEARS"]);
            }
            else if (ddlItems.SelectedValue.Equals(CommValue.RECEIT_VALUE_UTILFEE))
            {
                MultiCdDdlUtil.MakeManuallyUtilYear(ddlYear, txtHfRentCd.Text, TextNm["YEARS"]);
            }
            else
            {
                ddlYear.Items.Clear();
                ddlYear.Items.Add(new ListItem(TextNm["YEARS"], ""));
            }
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        protected void MakeMonthDdl()
        {
            if (ddlItems.SelectedValue.Equals(CommValue.RECEIT_VALUE_MNGFEE))
            {
                MultiCdDdlUtil.MakeManuallyMngMonth(ddlMonth, txtHfRentCd.Text, ddlYear.SelectedValue, TextNm["MONTH"]);
            }
            else if (ddlItems.SelectedValue.Equals(CommValue.RECEIT_VALUE_UTILFEE))
            {
                MultiCdDdlUtil.MakeManuallyUtilMonth(ddlMonth, txtHfRentCd.Text, ddlYear.SelectedValue, TextNm["MONTH"]);
            }
            else
            {
                ddlMonth.Items.Clear();
                ddlMonth.Items.Add(new ListItem(TextNm["MONTH"], ""));
            }
        }

        protected void MakeItemDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItems.Items.Clear();

            ddlItems.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                if (dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_MNGFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_PARKINGFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_PARKINGCARDFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE))
                {
                    ddlItems.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        protected void lvMngManualList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
            if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
            {
                // ListView에서 빈 데이터의 경우 알림메세지 정의
                ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
            }
        }

        protected void lvMngManualList_LayoutCreated(object sender, EventArgs e)
        {
        }

        protected void lvMngManualList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                DropDownList ddlItem = (DropDownList)iTem.FindControl("ddlItem");

                if (!string.IsNullOrEmpty(drView["ItemCd"].ToString()))
                {
                    CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlItem, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RECEIT);

                    ddlItem.SelectedValue = drView["ItemCd"].ToString();
                    ddlItem.Enabled = CommValue.AUTH_VALUE_FALSE;
                }

                Literal ltMngYYYYMM = (Literal)iTem.FindControl("ltMngYYYYMM");

                if (!string.IsNullOrEmpty(drView["MngYear"].ToString()) &&
                    !string.IsNullOrEmpty(drView["MngMM"].ToString()))
                {
                    if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                    {
                        ltMngYYYYMM.Text = drView["MngYear"].ToString() + "/" + drView["MngMM"].ToString();
                    }
                    else
                    {
                        ltMngYYYYMM.Text = drView["MngMM"].ToString() + "." + drView["MngYear"].ToString();
                    }
                }

                Literal ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    ltRoomNo.Text = drView["RoomNo"].ToString();
                }

                Literal ltHandOverDt = (Literal)iTem.FindControl("ltMoveInDt");

                if (!string.IsNullOrEmpty(drView["HandOverDt"].ToString()))
                {
                    ltHandOverDt.Text = TextLib.MakeDateEightDigit(drView["HandOverDt"].ToString());
                }

                Literal ltAmt = (Literal)iTem.FindControl("ltAmt");

                if (!string.IsNullOrEmpty(drView["RealMonthViAmtNo"].ToString()))
                {
                    ltAmt.Text = drView["RealMonthViAmtNo"].ToString();
                }
            }
        }

        protected void lnkbtnMakeLine_Click(object sender, EventArgs e)
        {
            try
            {
                object[] objReturn = new object[2];

                if (ddlItems.SelectedValue.Equals(CommValue.RECEIT_VALUE_MNGFEE))
                {
                    // KN_USP_MNG_INSERT_RENTALMNGFEE_M00
                    // KN_USP_MNG_INSERT_RENTALMNGFEE_M01
                    objReturn = MngPaymentBlo.RegistryAPTManuallyInfo(txtHfRentCd.Text, ddlYear.SelectedValue, ddlMonth.SelectedValue);
                }
                else if (ddlItems.SelectedValue.Equals(CommValue.RECEIT_VALUE_UTILFEE))
                {
                    // KN_USP_RES_INSERT_MONTHENERGY_M00
                    objReturn = MngPaymentBlo.RegistryUtilFeeManuallyInfo(ddlYear.SelectedValue, ddlMonth.SelectedValue);
                }

                if (objReturn != null)
                {
                    MakeItemDdl();
                    MakeYearDdl();
                    MakeMonthDdl();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
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

        protected void ddlItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeYearDdl();
            MakeMonthDdl();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeMonthDdl();
        }
    }
}