using System;
using System.Data;
using System.IO;
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
    public partial class MngReadMonth : BasePage
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
            //AlertRemote();

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];

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

            // KN_USP_MNG_SELECT_CHARGEINFO_S02
            dsReturn = RemoteMngBlo.SpreadMonthUseChargeList(CommValue.BOARD_VALUE_ROOMSIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfChargeTy.Text, ddlYear.Text, ddlMonth.Text, intSearchFloor, txtSearchRoom.Text);

            if (dsReturn != null)
            {
                lvDayChargeList.DataSource = dsReturn.Tables[1];
                lvDayChargeList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_ROOMSIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();

                txtHfYYYYMM.Text = ddlYear.Text + ddlMonth.Text;

                string strNowYYYY = DateTime.Now.AddMonths(-1).Year.ToString();
                string strNowMM = DateTime.Now.AddMonths(-1).Month.ToString();

                string strNowYYYYMM = strNowYYYY + strNowMM.PadLeft(2, '0');
                string strSearchYYYYMM = ddlYear.Text + ddlMonth.Text;

                if (Int32.Parse(strNowYYYYMM) >= Int32.Parse(strSearchYYYYMM) && dsReturn.Tables[1].Rows.Count > 0)
                {
                    lnkbtnModify.Visible = Master.isModDelAuthOk;
                }
                else
                {
                    lnkbtnModify.Visible = CommValue.AUTH_VALUE_FALSE;
                }
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

                Literal ltYearMonthList = (Literal)iTem.FindControl("ltYearMonthList");

                if (!string.IsNullOrEmpty(drView["EnergyMonth"].ToString()))
                {
                    string strDate = drView["EnergyMonth"].ToString();
                    ltYearMonthList.Text = strDate.Substring(0, 4) + " / " + strDate.Substring(4, 2);
                }

                Literal ltAmountUsedList = (Literal)iTem.FindControl("ltAmountUsedList");

                if (!string.IsNullOrEmpty(drView["TotalUse"].ToString()))
                {
                    ltAmountUsedList.Text = TextLib.MakeVietIntNo(double.Parse(drView["TotalUse"].ToString()).ToString("###,##0.##"));
                }
                else
                {
                    ltAmountUsedList.Text = CommValue.NUMBER_VALUE_ZERO;
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
                        ltChargeList.Text = TextLib.MakeVietIntNo(double.Parse(drView["TotalCharge"].ToString()).ToString("###,##0"));
                        ltInsMngFeeVAT.Text = (double.Parse(drView["TotalCharge"].ToString()) * double.Parse(strFeeVAT) / (100 + double.Parse(strFeeVAT))).ToString("###,##0");
                        ltInsMngFeeNET.Text = (double.Parse(drView["TotalCharge"].ToString()) - (double.Parse(drView["TotalCharge"].ToString()) * double.Parse(strFeeVAT) / (100 + double.Parse(strFeeVAT)))).ToString("###,##0");
                    }
                    else
                    {
                        ltChargeList.Text = CommValue.NUMBER_VALUE_ZERO;
                        ltInsMngFeeVAT.Text = CommValue.NUMBER_VALUE_ZERO;
                        ltInsMngFeeNET.Text = CommValue.NUMBER_VALUE_ZERO;
                    }
                }
                else
                {
                    ltChargeList.Text = CommValue.NUMBER_VALUE_ZERO;
                    ltInsMngFeeVAT.Text = CommValue.NUMBER_VALUE_ZERO;
                    ltInsMngFeeNET.Text = CommValue.NUMBER_VALUE_ZERO;
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

            ddlYear.SelectedValue = DateTime.Now.AddMonths(-1).Year.ToString();
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

            ddlMonth.SelectedValue = DateTime.Now.AddMonths(-1).Month.ToString().PadLeft(2, '0');
        }

        private void AlertRemote()
        {
            // KN_USP_MNG_SELECT_ENERGYMONTHCHECK_S00
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

                Response.Redirect(Master.PAGE_TRANSFER + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text + "&" + Master.PARAM_DATA3 + "=" + ddlYear.Text + "&" + Master.PARAM_DATA4 + "=" + ddlMonth.Text, CommValue.AUTH_VALUE_FALSE);
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

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnMakeExcel_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strYear = ddlYear.Text;
                string strMonth = ddlMonth.Text;
                string strFloorNo = txtSearchFloor.Text;
                string strRoomNo = txtSearchRoom.Text;

                DataTable dtReturn = new DataTable();

                // 아파트 및 아파트 상가
                // KN_USP_MNG_SELECT_MONTHENERGY_S01
                // KN_USP_MNG_SELECT_MONTHENERGY_S02
                // KN_USP_MNG_SELECT_MONTHENERGY_S03
                // 오피스 및 리테일
                // KN_USP_MNG_SELECT_MONTHENERGY_S05
                // KN_USP_MNG_SELECT_MONTHENERGY_S06
                // KN_USP_MNG_SELECT_MONTHENERGY_S07
                dtReturn = RemoteMngBlo.SpreadExcelMonthAmountUse(txtHfChargeTy.Text, strYear, strMonth, string.Empty, string.Empty, txtHfRentCd.Text, strFloorNo, strRoomNo);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                //this.EnableViewState = false;
                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

                string strTitle = "<p align='center'><font size='4'><b>" + Master.TITLE_NOW.ToString() + "</b></font></p>";
                htmlTextWriter.Write(strTitle);

                GridView gv = new GridView();
                gv.Font.Name = "Tahoma";
                gv.DataSource = dtReturn;
                gv.DataBind();

                gv.RenderControl(htmlTextWriter);
                Response.Write(stringWriter.ToString());
                Response.End();

                stringWriter.Flush();
                stringWriter.Close();
                htmlTextWriter.Flush();
                htmlTextWriter.Close();
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

        /// <summary>
        /// 상세보기 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["EnergyOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}