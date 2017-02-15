using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class HoadonPrintOutForAPTRetail: BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        string strInit = string.Empty;
        int intInit = CommValue.NUMBER_VALUE_0;
        object objTag = new object();

        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
                    // 2일이 지난 임시 출력 내용 삭제
                    // KN_USP_SET_DELETE_PRINTINFO_M00
                    ReceiptMngBlo.RemoveTempPrintList();

                    if (CheckParams())
                    {
                        InitControls();
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

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            string strMaxInvoiceNo = "0000000";

            ltSearchRoom.Text = TextNm["ROOMNO"];
            ltSearchYear.Text = TextNm["YEARS"];
            ltSearchMonth.Text = TextNm["MONTH"];
            ltStartDt.Text = TextNm["FROM"];
            ltEndDt.Text = TextNm["TO"];
            ltTxtCd.Text = TextNm["TAXCD"];
            ltRssNo.Text = TextNm["CERTINCORP"];
            ltMaxNo.Text = TextNm["MAXNUMBER"];

            // DropDownList Setting
            // 년도
            MakeYearDdl();

            // 월
            MakeMonthDdl();

            // 수납 아이템
            MakePaymentDdl();

            ltDate.Text = TextNm["MONTH"];
            ltInvoiceNo.Text = "Invoice NO.";
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltPaymentTy.Text = TextNm["ITEM"];
            ltPaymentDt.Text = TextNm["REGISTDATE"];
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];
            lnkbtnIssuing.Text = TextNm["ISSUING"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";

            chkAll.Enabled = CommValue.AUTH_VALUE_FALSE;

            // 세금계산서 최대값 조회
            DataTable dtReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.MAIN_COMP_CD);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MaxInvoiceNo"].ToString()))
                    {
                        strMaxInvoiceNo = dtReturn.Rows[0]["MaxInvoiceNo"].ToString().PadLeft(7, '0');
                    }
                }
            }

            ltInsMaxNo.Text = strMaxInvoiceNo;
        }

        protected void LoadData()
        {
            string strSearchNm = string.Empty;
            string strSearchRoomNo = string.Empty;
            string strMaxInvoiceNo = "0000000";

            if (lvPrintoutList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
            }

            if (!string.IsNullOrEmpty(txtSearchRoom.Text))
            {
                strSearchRoomNo = txtSearchRoom.Text;
            }

            strInit = CommValue.AUTH_VALUE_EMPTY;
            intInit = CommValue.NUMBER_VALUE_0;

            // KN_USP_SET_SELECT_HOADONINFO_S00
            DataTable dtReturn = ReceiptMngBlo.SpreadPrintListForHoadon(hfRentCd.Value, txtSearchRoom.Text, ddlYear.SelectedValue, ddlMonth.SelectedValue,
                                                                        hfStartDt.Value, hfEndDt.Value, ddlItemCd.SelectedValue, txtUserTaxCd.Text,
                                                                        txtRssNo.Text, Session["LANGCD"].ToString());

            if (dtReturn != null)
            {
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();

                if (intRowsCnt == CommValue.NUMBER_VALUE_0)
                {
                    chkAll.Enabled = CommValue.AUTH_VALUE_FALSE;
                }
                else
                {
                    chkAll.Enabled = CommValue.AUTH_VALUE_TRUE;
                }
            }

            // 세금계산서 최대값 조회
            DataTable dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.MAIN_COMP_CD);

            if (dtMaxReturn != null)
            {
                if (dtMaxReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    if (!string.IsNullOrEmpty(dtMaxReturn.Rows[0]["MaxInvoiceNo"].ToString()))
                    {
                        strMaxInvoiceNo = dtMaxReturn.Rows[0]["MaxInvoiceNo"].ToString().PadLeft(7, '0');
                    }
                }
            }

            ltInsMaxNo.Text = strMaxInvoiceNo;
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();

            ddlYear.Items.Add(new ListItem(TextNm["YEARS"], string.Empty));

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

            ddlMonth.Items.Add(new ListItem(TextNm["MONTH"], string.Empty));

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 
        /// </summary>
        private void MakePaymentDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItemCd.Items.Clear();

            ddlItemCd.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT))
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE))
                    {
                        ddlItemCd.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
                else
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE))
                    {
                        ddlItemCd.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
            }
        }

        protected void lvPrintoutList_LayoutCreated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPrintoutList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvPrintoutList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                HtmlTableCell tdChk = (HtmlTableCell)iTem.FindControl("tdChk");
                CheckBox chkboxList = (CheckBox)iTem.FindControl("chkboxList");

                TextBox txtHfPrintSeq = (TextBox)iTem.FindControl("txtHfPrintSeq");
                TextBox txtHfPrintDetSeq = (TextBox)iTem.FindControl("txtHfPrintDetSeq");
                TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");

                TextBox txtInvoiceNo = (TextBox)iTem.FindControl("txtInvoiceNo");
                TextBox txtOldInvoiceNo = (TextBox)iTem.FindControl("txtOldInvoiceNo");
                TextBox txtHfInvoiceNo = (TextBox)iTem.FindControl("txtHfInvoiceNo");

                if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
                {
                    if (!drView["InvoiceNo"].ToString().Equals(strInit))
                    {
                        chkboxList.Visible = CommValue.AUTH_VALUE_TRUE;
                        strInit = drView["InvoiceNo"].ToString();
                        intInit = CommValue.NUMBER_VALUE_0;
                        txtInvoiceNo.Text = drView["InvoiceNo"].ToString();
                        txtOldInvoiceNo.Text = drView["InvoiceNo"].ToString();
                        txtInvoiceNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                        txtHfInvoiceNo.Text = drView["InvoiceNo"].ToString();
                    }
                    else
                    {
                        chkboxList.Visible = CommValue.AUTH_VALUE_FALSE;
                        txtInvoiceNo.Text = drView["InvoiceNo"].ToString();
                        txtOldInvoiceNo.Text = drView["InvoiceNo"].ToString();
                        txtHfInvoiceNo.Text = drView["InvoiceNo"].ToString();
                        txtInvoiceNo.Visible = CommValue.AUTH_VALUE_FALSE;

                        if (intRowsCnt > CommValue.NUMBER_VALUE_0)
                        {
                            intInit = intInit + 1;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PrintSeq"].ToString()))
                {
                    txtHfPrintSeq.Text = drView["PrintSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PrintDetSeq"].ToString()))
                {
                    txtHfPrintDetSeq.Text = drView["PrintDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DataYear"].ToString()))
                {
                    Literal ltInsYear = (Literal)iTem.FindControl("ltInsYear");
                    ltInsYear.Text = drView["DataYear"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DataMonth"].ToString()))
                {
                    Literal ltInsMonth = (Literal)iTem.FindControl("ltInsMonth");
                    ltInsMonth.Text = drView["DataMonth"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDt"].ToString()))
                {
                    string strDate = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["InsDt"].ToString()));

                    TextBox txtInsRegDt = (TextBox)iTem.FindControl("txtInsRegDt");
                    HiddenField hfInsRegDt = (HiddenField)iTem.FindControl("hfInsRegDt");
                    Literal ltCalendarImg = (Literal)iTem.FindControl("ltCalendarImg");

                    txtInsRegDt.Text = strDate;
                    hfInsRegDt.Value = strDate.Replace("-", "");

                    StringBuilder sbInsdDt = new StringBuilder();

                    sbInsdDt.Append("<a href='#'><img align='absmiddle' onclick=\"Calendar(this, '");
                    sbInsdDt.Append(txtInsRegDt.ClientID);
                    sbInsdDt.Append("', '");
                    sbInsdDt.Append(hfInsRegDt.ClientID);
                    sbInsdDt.Append("', true)\" src='/Common/Images/Common/calendar.gif' style='cursor:pointer;' value='' /></a>");

                    ltCalendarImg.Text = sbInsdDt.ToString();
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                Literal ltInsBillNm = (Literal)iTem.FindControl("ltInsBillNm");

                if (!string.IsNullOrEmpty(drView["BillNm"].ToString()))
                {
                    ltInsBillNm.Text = TextLib.StringDecoder(drView["RealCd"].ToString());
                }

                TextBox txtHfBillCd = (TextBox)iTem.FindControl("txtHfBillCd");

                if (!string.IsNullOrEmpty(drView["BillCd"].ToString()))
                {
                    txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Description"].ToString()))
                {
                    TextBox txtInsDescription = (TextBox)iTem.FindControl("txtInsDescription");
                    txtInsDescription.Text = TextLib.StringDecoder(drView["Description"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["AmtViNo"].ToString()))
                {
                    TextBox txtInsAmtViNo = (TextBox)iTem.FindControl("txtInsAmtViNo");
                    txtInsAmtViNo.Text = TextLib.MakeVietIntNo(double.Parse(drView["AmtViNo"].ToString()).ToString("###,##0"));
                    txtInsAmtViNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["BillCd"].ToString()))
                {
                    txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
                }

                intRowsCnt++;
            }
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllCheck = chkAll.Checked;

            try
            {
                CheckAll(isAllCheck);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
        private void CheckAll(bool isAllCheck)
        {
            for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                }
            }
        }

        /// <summary>
        /// 리스트 각 행별 체크 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int intTotalCount = CommValue.NUMBER_VALUE_0;
                int intCheckCount = CommValue.NUMBER_VALUE_0;

                string strInvoiceNo = string.Empty;

                for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                    {
                        intTotalCount++;

                        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            strInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;
                            intCheckCount++;
                        }
                        else
                        {
                            string strOtherInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;

                            if (strOtherInvoiceNo != "" && strInvoiceNo.Equals(strOtherInvoiceNo))
                            {
                                ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_TRUE;
                            }
                        }
                    }

                    TextBox txtNewDt = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInsRegDt"));
                    HiddenField hfNewDt = ((HiddenField)lvPrintoutList.Items[intTmpI].FindControl("hfInsRegDt"));
                    txtNewDt.Text = TextLib.MakeDateEightDigit(hfNewDt.Value.Replace("-", ""));
                }

                if (intTotalCount == intCheckCount)
                {
                    chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 출력버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {
            try
            {
                int intCheckRow = CommValue.NUMBER_VALUE_0;

                string strPrintOutDt = string.Empty;
                string strPrintSeq = string.Empty;
                string strSearchData = string.Empty;
                string strDocNo = string.Empty;
                string strOldInvoiceNo = string.Empty;
                string strNewInvoiceNo = string.Empty;
                string strDescription = string.Empty;
                string strInvoiceDt = string.Empty;
                string strHoadonNo = string.Empty;

                double dblTotSellingPrice = CommValue.NUMBER_VALUE_0_0;

                int intDocSeq = CommValue.NUMBER_VALUE_0;
                int intPrintDetSeq = CommValue.NUMBER_VALUE_0;

                for (int intTmpI = CommValue.NUMBER_VALUE_0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                {
                    // CheckBox Check 여부에 따라서 데이터 임시 테이블에 등록후 해당 코드 받아올것
                    if (!string.IsNullOrEmpty(hfRentCd.Value))
                    {
                        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            string strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfBillCd")).Text;

                            strOldInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfInvoiceNo")).Text;

                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Visible)
                            {
                                strNewInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;
                            }

                            strDescription = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInsDescription")).Text;
                            strInvoiceDt = ((HiddenField)lvPrintoutList.Items[intTmpI].FindControl("hfInsRegDt")).Value;

                            if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPrintDetSeq")).Text))
                            {
                                intPrintDetSeq = Int32.Parse(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPrintDetSeq")).Text);
                            }

                            if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInsAmtViNo")).Text))
                            {
                                dblTotSellingPrice = double.Parse(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInsAmtViNo")).Text.Replace(".", "").Replace(",", ""));
                            }

                            //strTempDocNo, intTempDocSeq, strOldInvoiceNo, strNewInvoiceNo, intPrintDetSeq, strDescription, dblTotSellingPrice

                            // KN_USP_SET_INSERT_INVOICEFORTEMP_S04
                            DataTable dtPrintReturn = InvoiceMngBlo.InsertTempHoadonForTemp(strDocNo, intDocSeq, strOldInvoiceNo, strNewInvoiceNo, intPrintDetSeq, strDescription,
                                                                                            strInvoiceDt.Replace("-", ""), dblTotSellingPrice);

                            if (dtPrintReturn != null)
                            {
                                if (dtPrintReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    strDocNo = dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["TempDocNo"].ToString();
                                    intDocSeq = Int32.Parse(dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["TempDocSeq"].ToString());
                                    intCheckRow++;
                                }
                            }
                        }
                    }
                }

                // 선택 사항이 있는지 없는지 체크
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    // KN_USP_SET_INSERT_INVOICEFORTEMP_S05
                    InvoiceMngBlo.InsertTempHoadonBindForKeangNam(strDocNo);

                    StringBuilder sbPrintOut = new StringBuilder();

                    sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupHoadonKNPreview.aspx?Datum0=" + strDocNo + "\", \"Hoadon\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    // 화면 초기화
                    LoadData();

                    // 선택된 대상 없음
                    StringBuilder sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
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

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(((TextBox)(sender)).Text))
                {
                    TextBox txtOldInvoiceNo = (TextBox)lvPrintoutList.Items[((System.Web.UI.WebControls.ListViewDataItem)(((System.Web.UI.Control)(sender)).BindingContainer)).DataItemIndex].FindControl("txtOldInvoiceNo");

                    StringBuilder sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["ALERT_INSERT_SEVENDIGIT"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeSeven", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);

                    ((TextBox)(sender)).Text = txtOldInvoiceNo.Text;
                }
                else
                {
                    if (((TextBox)(sender)).Text.Length != 7)
                    {
                        TextBox txtOldInvoiceNo = (TextBox)lvPrintoutList.Items[((System.Web.UI.WebControls.ListViewDataItem)(((System.Web.UI.Control)(sender)).BindingContainer)).DataItemIndex].FindControl("txtOldInvoiceNo");

                        StringBuilder sbNoSelection = new StringBuilder();

                        sbNoSelection.Append("alert('" + AlertNm["ALERT_INSERT_SEVENDIGIT"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeSeven", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);

                        ((TextBox)(sender)).Text = txtOldInvoiceNo.Text;
                    }
                    else
                    {
                        // KN_USP_SET_SELECT_INVOICEFORCONFIRM_S01
                        DataTable dtExistReturn = InvoiceMngBlo.SelectExistLineForInvoiceNo(((TextBox)(sender)).Text);

                        if (dtExistReturn != null)
                        {
                            if (dtExistReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                            {
                                int intExistCnt = Int32.Parse(dtExistReturn.Rows[0]["RowCnt"].ToString());

                                if (intExistCnt > CommValue.NUMBER_VALUE_0)
                                {
                                    TextBox txtOldInvoiceNo = (TextBox)lvPrintoutList.Items[((System.Web.UI.WebControls.ListViewDataItem)(((System.Web.UI.Control)(sender)).BindingContainer)).DataItemIndex].FindControl("txtOldInvoiceNo");

                                    StringBuilder sbNoSelection = new StringBuilder();

                                    sbNoSelection.Append("alert('" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "');");

                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ExistSameLine", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);

                                    ((TextBox)(sender)).Text = txtOldInvoiceNo.Text;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnLoadData_Click(object sender, ImageClickEventArgs e)
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
    }
}