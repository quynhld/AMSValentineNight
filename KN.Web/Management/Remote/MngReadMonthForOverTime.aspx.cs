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
    public partial class MngReadMonthForOverTime : BasePage
    {
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
            Master.PARAM_DATA1 = "RentCd";
            Master.PARAM_DATA2 = "ChargeTy";
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

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
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1];
                txtHfChargeTy.Text = Request.Params[Master.PARAM_DATA2];

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltRoom.Text = TextNm["ROOMNO"];
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            // KN_USP_MNG_SELECT_UTILCHARGEINFO_S06           
            var dsReturn = MngPaymentBlo.SelectMngUtilInfoOverTime(txtHfRentCd.Text, txtHfChargeTy.Text, txtSearchRoom.Text, "", ddlPrintYN.SelectedValue,txtCompNm.Text,txtSearchDt.Text);
            if (dsReturn == null) return;
            lvDayChargeList.DataSource = dsReturn;
            lvDayChargeList.DataBind();
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
            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;

            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            var ltNo = (Literal)iTem.FindControl("ltNo");

            if (!string.IsNullOrEmpty(drView["SEQ"].ToString()))
            {
                ltNo.Text = drView["SEQ"].ToString();
            }

            var ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                ltRoomNo.Text = drView["RoomNo"].ToString();
            }

            var ltUserNm = (Literal)iTem.FindControl("ltUserNm");

            if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
            {
                ltUserNm.Text = drView["UserNm"].ToString();
            }

            var ltPeriod = (Literal)iTem.FindControl("ltPeriod");

            if (!string.IsNullOrEmpty(drView["Period"].ToString()))
            {
                ltPeriod.Text = TextLib.MakeDateSixDigit(drView["Period"].ToString());
            }

            var ltSquare = (Literal)iTem.FindControl("ltSquare");

            if (!string.IsNullOrEmpty(drView["Square"].ToString()))
            {
                ltSquare.Text = drView["Square"].ToString() == "0.00" ? "" : drView["Square"].ToString();
            }

            var ltHoursOver = (Literal)iTem.FindControl("ltHoursOver");

            if (!string.IsNullOrEmpty(drView["HoursOver"].ToString()))
            {
                ltHoursOver.Text = drView["HoursOver"].ToString();
            }

            var ltUnitPrice = (Literal)iTem.FindControl("ltUnitPrice");

            ltUnitPrice.Text = !string.IsNullOrEmpty(drView["UnitPrice"].ToString()) ? TextLib.MakeVietIntNo(double.Parse(drView["UnitPrice"].ToString()).ToString("###,##0.##")) : CommValue.NUMBER_VALUE_ZERO;

            var ltExchangRate = (Literal)iTem.FindControl("ltExchangRate");

            ltExchangRate.Text = !string.IsNullOrEmpty(drView["ExchangeRate"].ToString()) ? TextLib.MakeVietIntNo(double.Parse(drView["ExchangeRate"].ToString()).ToString("###,##0.##")) : CommValue.NUMBER_VALUE_ZERO;

            var ltDiscount = (Literal)iTem.FindControl("ltDiscount");

            ltDiscount.Text = !string.IsNullOrEmpty(drView["Discount"].ToString()) ? TextLib.MakeVietIntNo(double.Parse(drView["Discount"].ToString()).ToString("###,##0.##")) : CommValue.NUMBER_VALUE_ZERO;
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

        protected void imgbtnMakeExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                // 아파트 및 아파트 상가
                var dtReturn = MngPaymentBlo.SelectMngUtilInfoOverTime(txtHfRentCd.Text, txtHfChargeTy.Text, txtSearchRoom.Text, "", ddlPrintYN.SelectedValue, txtCompNm.Text, txtSearchDt.Text);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "utf-8";
                Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                //this.EnableViewState = false;
                var stringWriter = new StringWriter();
                var htmlTextWriter = new HtmlTextWriter(stringWriter);

                string strTitle = "<p align='center'><font size='4'><b>" + Master.TITLE_NOW + "</b></font></p>";
                htmlTextWriter.Write(strTitle);

                var gv = new GridView();
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

        protected void lnkCreateUtil_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

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
                Response.Redirect(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text + "&RoomNo=" + roomNo + "&ChargeSeq=" + chargeSeq, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlPrintYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}