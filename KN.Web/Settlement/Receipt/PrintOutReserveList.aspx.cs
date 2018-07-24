
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;

namespace KN.Web.Settlement.Receipt
{
    public partial class PrintOutReserveList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

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
            ltSearchRoom.Text = TextNm["ROOMNO"];
            ltSearchYear.Text = TextNm["YEARS"];
            ltSearchMonth.Text = TextNm["MONTH"];
            ltStartDt.Text = TextNm["FROM"];
            ltEndDt.Text = TextNm["TO"];
            ltTxtCd.Text = TextNm["TAXCD"];
            ltRssNo.Text = TextNm["CERTINCORP"];

            // DropDownList Setting
            // 년도
            MakeYearDdl();

            // 월
            MakeMonthDdl();

            // 수납 아이템
            MakePaymentDdl();

            ltDate.Text = TextNm["YEARS"] + "/" + TextNm["MONTH"];
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
        }

        protected void LoadData()
        {
            string strSearchNm = string.Empty;
            string strSearchRoomNo = string.Empty;

            if (lvPrintoutList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
            }

            if (!string.IsNullOrEmpty(txtSearchRoom.Text))
            {
                strSearchRoomNo = txtSearchRoom.Text;
            }

            // KN_USP_SET_SELECT_PRINTINFO_S00
            DataTable dtReturn = ReceiptMngBlo.SpreadPrintListForEntireIssuing(hfRentCd.Value, txtSearchRoom.Text, ddlYear.SelectedValue, ddlMonth.SelectedValue,
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
                CheckBox chkboxList = (CheckBox)iTem.FindControl("chkboxList");

                TextBox txtHfPrintSeq = (TextBox)iTem.FindControl("txtHfPrintSeq");
                TextBox txtHfPrintDetSeq = (TextBox)iTem.FindControl("txtHfPrintDetSeq");
                TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    txtHfPrintSeq.Text = drView["PrintSeq"].ToString();
                    txtHfPrintDetSeq.Text = drView["PrintDetSeq"].ToString();
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
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
                    Literal ltInsRegDt = (Literal)iTem.FindControl("ltInsRegDt");
                    ltInsRegDt.Text = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["InsDt"].ToString()));
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                Literal ltInsBillNm = (Literal)iTem.FindControl("ltInsBillNm");
                TextBox txtHfBillCd = (TextBox)iTem.FindControl("txtHfBillCd");

                if (!string.IsNullOrEmpty(drView["BillNm"].ToString()))
                {
                    ltInsBillNm.Text = TextLib.StringDecoder(drView["BillNm"].ToString());
                    txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Description"].ToString()))
                {
                    Literal ltInsDescription = (Literal)iTem.FindControl("ltInsDescription");
                    ltInsDescription.Text = TextLib.StringDecoder(drView["Description"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["AmtViNo"].ToString()))
                {
                    Literal ltInsAmtViNo = (Literal)iTem.FindControl("ltInsAmtViNo");
                    ltInsAmtViNo.Text = TextLib.MakeVietIntNo(double.Parse(drView["AmtViNo"].ToString()).ToString("###,##0"));
                }

                ImageButton imgbtnExample = (ImageButton)iTem.FindControl("imgbtnExample");

                if (!string.IsNullOrEmpty(txtHfBillCd.Text) && !string.IsNullOrEmpty(hfRentCd.Value))
                {
                    if ((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) &&
                        (txtHfBillCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                        (txtHfBillCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)) ||
                        (txtHfBillCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)))
                    {
                        imgbtnExample.OnClientClick = "javascript:return fnChestNutPreview('" + txtHfUserSeq.Text + "','" + txtHfPrintSeq.Text + "','" + txtHfPrintDetSeq.Text + "');";
                    }
                    else
                    {
                        imgbtnExample.OnClientClick = "javascript:return fnKeangNamPreview('" + txtHfUserSeq.Text + "','" + txtHfPrintSeq.Text + "','" + txtHfPrintDetSeq.Text + "');";
                    }

                    imgbtnExample.Visible = CommValue.AUTH_VALUE_TRUE;
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

                for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                    {
                        intTotalCount++;

                        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            intCheckCount++;
                        }
                    }
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
                bool isChestNut = CommValue.AUTH_VALUE_TRUE;
                int intCheckRow = CommValue.NUMBER_VALUE_0;

                string strPrintOutDt = string.Empty;
                string strPrintSeq = string.Empty;

                int intPrintOutSeq = CommValue.NUMBER_VALUE_0;
                int intPrintDetSeq = CommValue.NUMBER_VALUE_0;

                for (int intTmpI = CommValue.NUMBER_VALUE_0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                {
                    // CheckBox Check 여부에 따라서 데이터 임시 테이블에 등록후 해당 코드 받아올것
                    // 해당 프린트 출력자 정보 등록해줄 것.
                    if (!string.IsNullOrEmpty(hfRentCd.Value))
                    {
                        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            string strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfBillCd")).Text;

                            if (!hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) &&
                                !hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA) &&
                                !hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB))
                            {
                                isChestNut = CommValue.AUTH_VALUE_FALSE;
                            }
                            else
                            {
                                // 아파트 관리비와 아파트 수도 전기세 이외의 데이터 존재시 경남비나 수납증 출력
                                if (((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) ||
                                     (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA)) ||
                                     (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB))) &&
                                    ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                     (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                     (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE))))
                                {                                    
                                    // 해당 사항은 체스넛 수납증 출력
                                    isChestNut = CommValue.AUTH_VALUE_FALSE;
                                }
                            }

                            strPrintSeq = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPrintSeq")).Text;

                            if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPrintDetSeq")).Text))
                            {
                                intPrintDetSeq = Int32.Parse(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPrintDetSeq")).Text);
                            }

                            // KN_USP_SET_INSERT_PRINTINFO_S04
                            DataTable dtPrintReturn = ReceiptMngBlo.RegistryTempPrintOutList(strPrintOutDt, intPrintOutSeq, strPrintSeq, intPrintDetSeq);

                            if (dtPrintReturn != null)
                            {
                                if (dtPrintReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    strPrintOutDt = dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutDt"].ToString();
                                    intPrintOutSeq = Int32.Parse(dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutSeq"].ToString());
                                    intCheckRow++;
                                }
                            }
                        }
                    }
                }

                // 선택 사항이 있는지 없는지 체크
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    StringBuilder sbPrintOut = new StringBuilder();

                    if (isChestNut)
                    {
                        sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupCombineReciptCN.aspx?Datum0=" + strPrintOutDt + "&Datum1=" + intPrintOutSeq +"\", \"Reciept\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                    }
                    else
                    {
                        sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupCombineReciptKN.aspx?Datum0=" + strPrintOutDt + "&Datum1=" + intPrintOutSeq + "\", \"Reciept\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                    }

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RecieptPrint", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
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
    }
}