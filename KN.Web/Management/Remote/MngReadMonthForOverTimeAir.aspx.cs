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
    public partial class MngReadMonthForOverTimeAir : BasePage
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
            ltFloor.Text = TextNm["FLOOR"];
            ltRoom.Text = TextNm["ROOMNO"];

            ltMonth.Text = TextNm["MONTH"];
            lnkbtnDay.Text = TextNm["DAYS"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnModify.OnClientClick = "javascript:return fnModifyData('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            MakeYearDdl();
            MakeMonthDdl();

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnMakeInvoice.Text = TextNm["MAKEBILL"];
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];

            txtSearchFloor.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

           // DataSet dsReturn = new DataSet();

            int intSearchFloor = 0;

            if (!string.IsNullOrEmpty(txtSearchFloor.Text))
            {
                intSearchFloor = Int32.Parse(txtSearchFloor.Text);
            }

            // KN_USP_MNG_SELECT_UTILCHARGEINFO_S06
            var dsReturn = MngPaymentBlo.SelectMngUtilInfo(txtHfRentCd.Text, txtHfChargeTy.Text, txtSearchRoom.Text, "", "N","","");
            if (dsReturn == null) return;
            lvDayChargeList.DataSource = dsReturn;
            lvDayChargeList.DataBind();

            lnkbtnModify.Visible = Master.isModDelAuthOk;
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

                //if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                //{
                //    if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                //    {
                //        Literal ltFloorRoomList = (Literal)iTem.FindControl("ltFloorRoomList");
                //        ltFloorRoomList.Text = TextLib.StringDecoder(drView["FloorNo"].ToString()) + " / " + TextLib.StringDecoder(drView["RoomNo"].ToString());
                //    }
                //}

                var txthfChargeSeq = (HiddenField)iTem.FindControl("txthfChargeSeq");

                if (!string.IsNullOrEmpty(drView["ChargeSeq"].ToString()))
                {
                    txthfChargeSeq.Value = drView["ChargeSeq"].ToString();
                }

                var ltUserNm = (Literal)iTem.FindControl("ltUserNm");

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    ltUserNm.Text = drView["UserNm"].ToString();
                }

                var ltSUsing = (Literal)iTem.FindControl("ltSUsing");

                if (!string.IsNullOrEmpty(drView["StartDate"].ToString()))
                {                 
                    ltSUsing.Text = TextLib.MakeDateEightDigit(drView["StartDate"].ToString());
                }

                var ltEUsing = (Literal)iTem.FindControl("ltEUsing");

                if (!string.IsNullOrEmpty(drView["EndDate"].ToString()))
                {                   
                    ltEUsing.Text = TextLib.MakeDateEightDigit(drView["EndDate"].ToString());
                }

                var ltFistIndex = (Literal)iTem.FindControl("ltFistIndex");

                if (!string.IsNullOrEmpty(drView["FistIndex"].ToString()))
                {
                    ltFistIndex.Text = drView["FistIndex"].ToString();
                }

                var ltEndIndex = (Literal)iTem.FindControl("ltEndIndex");

                if (!string.IsNullOrEmpty(drView["EndIndex"].ToString()))
                {
                    ltEndIndex.Text = drView["EndIndex"].ToString();
                }

                Literal ltNormalUsing = (Literal)iTem.FindControl("ltNormalUsing");

                if (!string.IsNullOrEmpty(drView["NormalUsing"].ToString()))
                {
                    ltNormalUsing.Text = TextLib.MakeVietIntNo(double.Parse(drView["NormalUsing"].ToString()).ToString("###,##0.##"));
                }
                else
                {
                    ltNormalUsing.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                Literal ltHeightUsing = (Literal)iTem.FindControl("ltHeightUsing");

                if (!string.IsNullOrEmpty(drView["HightUsing"].ToString()))
                {
                    ltHeightUsing.Text = TextLib.MakeVietIntNo(double.Parse(drView["HightUsing"].ToString()).ToString("###,##0.##"));
                }
                else
                {
                    ltHeightUsing.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                Literal ltLowUsing = (Literal)iTem.FindControl("ltLowUsing");

                if (!string.IsNullOrEmpty(drView["LowUsing"].ToString()))
                {
                    ltLowUsing.Text = TextLib.MakeVietIntNo(double.Parse(drView["LowUsing"].ToString()).ToString("###,##0.##"));
                }
                else
                {
                    ltLowUsing.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                Literal ltOtherUsing = (Literal)iTem.FindControl("ltOtherUsing");

                if (!string.IsNullOrEmpty(drView["NormalOtherUsing"].ToString()))
                {
                    ltOtherUsing.Text = TextLib.MakeVietIntNo(double.Parse(drView["NormalOtherUsing"].ToString()).ToString("###,##0.##"));
                }
                else
                {
                    ltOtherUsing.Text = CommValue.NUMBER_VALUE_ZERO;
                }


                //string strFeeVAT = string.Empty;

                //if (!string.IsNullOrEmpty(drView["VatRatio"].ToString()))
                //{
                //    if (!drView["VatRatio"].ToString().Equals(CommValue.NUMBER_VALUE_0_00.ToString()))
                //    {
                //        strFeeVAT = drView["VatRatio"].ToString();
                //    }
                //    else
                //    {
                //        strFeeVAT = CommValue.NUMBER_VALUE_0_00.ToString();
                //    }
                //}

                //Literal ltCharge = (Literal)iTem.FindControl("ltCharge");
                //Literal ltInsMngFeeVAT = (Literal)iTem.FindControl("ltInsMngFeeVAT");
                //Literal ltInsMngFeeNET = (Literal)iTem.FindControl("ltInsMngFeeNET");

                //if (!string.IsNullOrEmpty(drView["UtilCharge"].ToString()))
                //{
                //    if (!double.Parse(drView["UtilCharge"].ToString()).Equals(CommValue.NUMBER_VALUE_0_00))
                //    {
                //        ltCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["UtilCharge"].ToString()).ToString("###,##0"));
                //        ltInsMngFeeVAT.Text = TextLib.MakeVietIntNo(double.Parse(drView["UtilVAT"].ToString()).ToString("###,##0"));
                //        ltInsMngFeeNET.Text = TextLib.MakeVietIntNo(double.Parse(drView["UtilNET"].ToString()).ToString("###,##0"));
                //    }
                //    else
                //    {
                //        ltCharge.Text = CommValue.NUMBER_VALUE_ZERO;
                //        ltInsMngFeeVAT.Text = CommValue.NUMBER_VALUE_ZERO;
                //        ltInsMngFeeNET.Text = CommValue.NUMBER_VALUE_ZERO;
                //    }
                //}
                //else
                //{
                //    ltCharge.Text = CommValue.NUMBER_VALUE_ZERO;
                //    ltInsMngFeeVAT.Text = CommValue.NUMBER_VALUE_ZERO;
                //    ltInsMngFeeNET.Text = CommValue.NUMBER_VALUE_ZERO;
                //}
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

        protected void lnkCreateUtil_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect("UtilFeeWrite.aspx?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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
        /// 상세보기 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var roomNo = txthfRoomNo.Value;
                var chargeSeq = txthfChargeSeq.Value;
                Response.Redirect("UtilFeeWrite.aspx?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text + "&RoomNo=" + roomNo + "&ChargeSeq=" + chargeSeq, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnMakeInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string strRentCd = txtHfRentCd.Text;
                string strYear = ddlYear.SelectedValue;
                string strMonth = ddlMonth.SelectedValue;

                object[] objReturn = new object[2];

                // KN_USP_MNG_INSERT_HOADONINFO_M04
                objReturn = RemoteMngBlo.InsertHoadonForUtilFee(strRentCd, strYear, strMonth);

                if (objReturn != null)
                {
                    StringBuilder sbWarning = new StringBuilder();

                    sbWarning.Append("alert('");
                    sbWarning.Append(AlertNm["INFO_MAKE_BILLING"]);
                    sbWarning.Append("');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeBill", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    StringBuilder sbWarning = new StringBuilder();

                    sbWarning.Append("alert('");
                    sbWarning.Append(AlertNm["INFO_NOT_MAKE_BILLING"]);
                    sbWarning.Append("');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeBill", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }


    }
}