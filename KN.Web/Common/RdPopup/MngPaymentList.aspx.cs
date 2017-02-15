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

namespace KN.Web.Management.Manage
{
    public partial class MngPaymentList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;

        string strPayYn = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
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
                hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();

                txtHfFeeTy.Text = Request.Params[Master.PARAM_DATA2].ToString();
                hfFeeTy.Value = Request.Params[Master.PARAM_DATA2].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }
            return isReturnOk;
        }

        protected void InitControls()
        {
            string strFeeTyTxt = string.Empty;

            ltFloor.Text = TextNm["FLOOR"];
            ltRoom.Text = TextNm["ROOMNO"];
            ltNameTitle.Text = TextNm["NAME"];
            ltReceiteTitle.Text = TextNm["PAY"] + " / " + TextNm["NOTPAY"];

            lnkbtnPrint.Text = TextNm["PRINT"];
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnBillPrint.Text = TextNm["PRINTBILL"];
            lnkbtnBillPrint.OnClientClick = "javascript:return fnPrintOutBill('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            ltPayNoPaid.Text = TextNm["PAY"] + "/" + TextNm["NOTPAY"];
            ltFloorRoom.Text = TextNm["FLOOR"] + " / " + TextNm["ROOMNO"];
            ltName.Text = TextNm["NAME"];
            ltPayNoPaid.Text = TextNm["PAY"] + " / " + TextNm["NOTPAY"];
            ltLateYn.Text = TextNm["LATEFEEYN"];
            ltTotalPay.Text = TextNm["CHARGEDMOUNT"];
            ltPayment.Text = TextNm["PAYMENT"];
            ltPayDay.Text = TextNm["CHARGEMONTH"];
            ltRemainder.Text = TextNm["REMAINDER"];

            lnkbtnChartPrint.Text = "Chart " + TextNm["PRINT"];
            lnkbtnChartPrint.OnClientClick = "javascript:return fnMngFeeChartPrint('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnPrint.Text = "Report " + TextNm["PRINT"];
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];
            lnkbtnExcelReport.OnClientClick = "javascript:return fnExcelPrint('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnExcelReport.Visible = Master.isWriteAuthOk;

            MakeYearDdl();
            MakeMonthDdl();

            if (txtHfFeeTy.Text.Equals(CommValue.FEETY_VALUE_MNGFEE))
            {
                strFeeTyTxt = TextNm["MANAGEFEE"];
            }
            else if (txtHfFeeTy.Text.Equals(CommValue.FEETY_VALUE_RENTALFEE))
            {
                strFeeTyTxt = TextNm["RENTALFEE"];
            }

            // DropDownList Setting
            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlPayYn, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_PAYMENT, TextNm["ENTIRE"]);
            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlLateYn, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_LATE, TextNm["ENTIRE"]);
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_RENTALMNGFEECALENDAR_S00
            dtReturn = MngPaymentBlo.WatchRentalMngFeeCalendar(txtHfRentCd.Text);

            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem(TextNm["YEARS"], ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlYear.Items.Add(new ListItem(dr["RentalYear"].ToString(), dr["RentalYear"].ToString()));                
            }

            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl()
        {
            DataTable dtReturn = new DataTable();

            string strYear = ddlYear.SelectedValue;
            string strMonth = string.Empty;

            // KN_USP_MNG_SELECT_RENTALMNGFEECALENDAR_S01
            dtReturn = MngPaymentBlo.WatchRentalMngFeeCalendar(txtHfRentCd.Text, strYear);

            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem(TextNm["MONTH"], ""));

            if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
            {
                foreach (DataRow dr in dtReturn.Select())
                {
                    ddlMonth.Items.Add(new ListItem(dr["RentalMM"].ToString().PadLeft(2, '0'), dr["RentalMM"].ToString().PadLeft(2, '0')));
                    strMonth = dr["RentalMM"].ToString().PadLeft(2, '0');
                }

                ddlMonth.SelectedValue = strMonth;
            }
            else
            {
                ddlMonth.SelectedValue = string.Empty;
            }
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            int intSearchFloor = 0;

            if (!string.IsNullOrEmpty(txtSearchFloor.Text))
            {
                intSearchFloor = Int32.Parse(txtSearchFloor.Text);
            }

            // KN_USP_MNG_SELECT_PAYMENTINFO_S00
            dsReturn = MngPaymentBlo.SpreadMngPaymentList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfFeeTy.Text, txtNm.Text,
                                                          intSearchFloor, txtSearchRoom.Text, ddlPayYn.Text, ddlLateYn.Text, ddlYear.Text, ddlMonth.Text, 
                                                          Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                lvMngPaymentList.DataSource = dsReturn.Tables[1];
                lvMngPaymentList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMngPaymentList_LayoutCreated(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMngPaymentList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvMngPaymentList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                string strTotalPayment = string.Empty;
                string strPayAmt = string.Empty;

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    Literal ltFloorRoomList = (Literal)iTem.FindControl("ltFloorRoomList");
                    ltFloorRoomList.Text = TextLib.StringDecoder(drView["FloorNo"].ToString()) + " / " + TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
                {
                    Literal ltNameList = (Literal)iTem.FindControl("ltNameList");
                    ltNameList.Text = TextLib.TextCutString(TextLib.StringDecoder(drView["TenantNm"].ToString()), 25, "");
                }

                if (!string.IsNullOrEmpty(drView["ReceitCd"].ToString()))
                {
                    string strReceitYn = string.Empty;

                    Literal ltPayNoPaidList = (Literal)iTem.FindControl("ltPayNoPaidList");
                    strReceitYn = drView["ReceitCd"].ToString();

                    if (CommValue.PAYMENT_TYPE_VALUE_PAID.Equals(strReceitYn))
                    {
                        ltPayNoPaidList.Text = TextNm["PAY"];
                    }
                    else
                    {
                        ltPayNoPaidList.Text = TextNm["NOTPAY"];
                    }
                }

                if (!string.IsNullOrEmpty(drView["LateFeeYn"].ToString()))
                {
                    string strLateYn = string.Empty;

                    Literal ltLateYnList = (Literal)iTem.FindControl("ltLateYnList");
                    strLateYn = drView["LateFeeYn"].ToString();

                    if (CommValue.OVERDUE_TYPE_VALUE_TRUE.Equals(strLateYn))
                    {
                        ltLateYnList.Text = TextNm["LATE"];
                    }
                    else
                    {
                        ltLateYnList.Text = "-";
                    }
                }

                if (!string.IsNullOrEmpty(drView["MonthViAmtNo"].ToString()))
                {
                    Literal ltTotalPayList = (Literal)iTem.FindControl("ltTotalPayList");
                    ltTotalPayList.Text = TextLib.MakeVietIntNo(double.Parse(drView["MonthViAmtNo"].ToString()).ToString("###,##0"));
                    strTotalPayment = ltTotalPayList.Text;
                }

                Literal ltPaymentList = (Literal)iTem.FindControl("ltPaymentList");
                Literal ltRemainderList = (Literal)iTem.FindControl("ltRemainderList");

                if (!string.IsNullOrEmpty(drView["PayAmt"].ToString()))
                {
                    ltPaymentList.Text = TextLib.MakeVietIntNo(double.Parse(drView["PayAmt"].ToString()).ToString("###,##0"));
                    ltRemainderList.Text = TextLib.MakeVietIntNo((double.Parse(strTotalPayment.Replace(".", "")) - double.Parse(ltPaymentList.Text.Replace(".", ""))).ToString("###,##0"));

                    if (ltRemainderList.Text.Equals(""))
                    {
                        ltRemainderList.Text = "-";
                    }
                }
                else
                {
                    ltPaymentList.Text = "-";
                    if (!strTotalPayment.Equals(""))
                    {
                        ltRemainderList.Text = strTotalPayment;
                    }
                }

                if (!string.IsNullOrEmpty(drView["RentalYear"].ToString()) &&
                    !string.IsNullOrEmpty(drView["RentalMM"].ToString()))
                {
                    //string strPayDt = drView["RentalYear"].ToString();
                    //DateTime dtPayDt = new DateTime(Int32.Parse(strPayDt.Substring(0, 4)), Int32.Parse(strPayDt.Substring(4, 2)), 1).AddMonths(2).AddDays(-1);  //마지막 일
                    //string strPayYear = dtPayDt.ToString("s").Replace("/", "").Replace("-", "").Substring(0, 4);
                    //string strPayMM = dtPayDt.ToString("s").Replace("/", "").Replace("-", "").Substring(4, 2);
                    string strPayYear = drView["RentalYear"].ToString();
                    string strPayMM = drView["RentalMM"].ToString();

                    Literal ltPayDayList = (Literal)iTem.FindControl("ltPayDayList");
                    ltPayDayList.Text = strPayYear + " / " + strPayMM;
                }
                else
                {
                    Literal ltPayDayList = (Literal)iTem.FindControl("ltPayDayList");
                    ltPayDayList.Text = "-";
                }
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeMonthDdl();
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

                StringBuilder sbView = new StringBuilder();
                sbView.Append(Master.PAGE_VIEW);
                sbView.Append("?");
                sbView.Append(Master.PARAM_DATA1);
                sbView.Append("=");
                sbView.Append(txtHfRentCd.Text);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA2);
                sbView.Append("=");
                sbView.Append(txtHfFeeTy.Text);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA3);
                sbView.Append("=");
                sbView.Append(hfRentalYear.Value);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA4);
                sbView.Append("=");
                sbView.Append(hfRentalMM.Value);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA5);
                sbView.Append("=");
                sbView.Append(hfUserSeq.Value);

                Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 엑셀리포트버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            int intSearchFloor = 0;

            if (!string.IsNullOrEmpty(txtSearchFloor.Text))
            {
                intSearchFloor = Int32.Parse(txtSearchFloor.Text);
            }

            // KN_USP_MNG_SELECT_PAYMENTINFO_S00
            dsReturn = MngPaymentBlo.SpreadMngPaymentList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfFeeTy.Text, txtNm.Text,
                                                          intSearchFloor, txtSearchRoom.Text, ddlPayYn.Text, ddlLateYn.Text, ddlYear.Text, ddlMonth.Text,
                                                          Session["LangCd"].ToString());

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
            Response.ContentType = "application/vnd.ms-excel";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            this.EnableViewState = false;

            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

            string strTitle = "<p align=center><font size=4 face=Gulim><b>" + Master.TITLE_NOW.ToString() + "</b></font></p>";
            htmlTextWriter.Write(strTitle);

            GridView gv = new GridView();

            gv.DataSource = dsReturn.Tables[2];
            gv.DataBind();
            gv.RenderControl(htmlTextWriter);

            Response.Write(stringWriter.ToString());

            stringWriter.Flush();
            stringWriter.Close();
            htmlTextWriter.Flush();
            htmlTextWriter.Close();

            Response.End();
        }

        protected void lnkbtnBillPrint_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}