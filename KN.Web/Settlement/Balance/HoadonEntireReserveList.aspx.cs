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

namespace KN.Web.Settlement.Balance
{
    public partial class HoadonEntireReserveList : BasePage
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
                    //// 2일이 지난 임시 출력 내용 삭제
                    //// KN_USP_SET_DELETE_PRINTINFO_M00
                    //ReceiptMngBlo.RemoveTempPrintList();

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
            ltRange.Text = TextNm["SCORPSECTION"];
            ltType.Text = TextNm["TYPE"];
            ltTitle.Text = TextNm["TITLE"];

            // DropDownList Setting
            // 년도
            MakeYearDdl();

            // 월
            MakeMonthDdl();

            // 수납 아이템
            MakePaymentDdl();

            // 범위
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlRange, Session["LANGCD"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_SCORPE);
            ddlRange.SelectedValue = CommValue.SCORPE_SEARCH_VALUE_ITEM;

            // 타입
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlType, Session["LANGCD"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_TYPE);
            ddlType.SelectedValue = CommValue.TYPE_SEARCH_VALUE_MAX;
            txtTitle.ReadOnly = CommValue.AUTH_VALUE_TRUE;

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

            // KN_USP_SET_SELECT_LEDGERINFO_S02
            DataTable dtReturn = ReceiptMngBlo.SpreadHoadonForEntireIssuing(txtSearchRoom.Text, hfRentCd.Value, ddlYear.SelectedValue, ddlMonth.SelectedValue,
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

                TextBox txtHfDebitCreditCd = (TextBox)iTem.FindControl("txtHfDebitCreditCd");
                TextBox txtHfPaymentDt = (TextBox)iTem.FindControl("txtHfPaymentDt");
                TextBox txtHfPaymentSeq = (TextBox)iTem.FindControl("txtHfPaymentSeq");
                TextBox txtHfPaymentDetSeq = (TextBox)iTem.FindControl("txtHfPaymentDetSeq");
                TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");   

                if (!string.IsNullOrEmpty(drView["PaymentDetSeq"].ToString()))
                {
                    txtHfDebitCreditCd.Text = drView["DebitCreditCd"].ToString();
                    txtHfPaymentDt.Text = drView["PaymentDt"].ToString();
                    txtHfPaymentSeq.Text = drView["PaymentSeq"].ToString();
                    txtHfPaymentDetSeq.Text = drView["PaymentDetSeq"].ToString();
                    txtHfUserSeq.Text = TextLib.StringDecoder(drView["UserSeq"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["SvcYear"].ToString()))
                {
                    Literal ltInsYear = (Literal)iTem.FindControl("ltInsYear");
                    ltInsYear.Text = drView["SvcYear"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["SvcMM"].ToString()))
                {
                    Literal ltInsMonth = (Literal)iTem.FindControl("ltInsMonth");
                    ltInsMonth.Text = drView["SvcMM"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    Literal ltInsRegDt = (Literal)iTem.FindControl("ltInsRegDt");
                    ltInsRegDt.Text = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["PaymentDt"].ToString()));
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                Literal ltInsItemNm = (Literal)iTem.FindControl("ltInsItemNm");
                TextBox txtHfItemCd = (TextBox)iTem.FindControl("txtHfItemCd");
                TextBox txtHfBillCd = (TextBox)iTem.FindControl("txtHfBillCd");
                TextBox txtHfBillNo = (TextBox)iTem.FindControl("txtHfBillNo");
                             

                if (!string.IsNullOrEmpty(drView["ItemNm"].ToString()))
                {
                    ltInsItemNm.Text = TextLib.StringDecoder(drView["ItemNm"].ToString());
                    txtHfItemCd.Text = TextLib.StringDecoder(drView["ItemCd"].ToString());
                    txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
                    txtHfBillNo.Text = TextLib.StringDecoder(drView["BillNo"].ToString());

                }

                if (!string.IsNullOrEmpty(drView["RemarkEn"].ToString()))
                {
                    Literal ltInsRemarkEn = (Literal)iTem.FindControl("ltInsRemarkEn");
                    ltInsRemarkEn.Text = TextLib.StringDecoder(drView["RemarkEn"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["TotSellingPrice"].ToString()))
                {
                    Literal ltInsTotSellingPrice = (Literal)iTem.FindControl("ltInsTotSellingPrice");
                    ltInsTotSellingPrice.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["TotSellingPrice"].ToString()).ToString("###,##0"));
                }

                //ImageButton imgbtnExample = (ImageButton)iTem.FindControl("imgbtnExample");

                //if (!string.IsNullOrEmpty(txtHfBillCd.Text) && !string.IsNullOrEmpty(hfRentCd.Value))
                //{
                //    if ((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) &&
                //        (txtHfBillCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                //        (txtHfBillCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)) ||
                //        (txtHfBillCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)))
                //    {
                //        imgbtnExample.OnClientClick = "javascript:return fnChestNutPreview('" + txtHfUserSeq.Text + "','" + txtHfPaymentDt.Text + "','" + txtHfPaymentSeq.Text + "');";
                //    }
                //    else
                //    {
                //        imgbtnExample.OnClientClick = "javascript:return fnKeangNamPreview('" + txtHfUserSeq.Text + "','" + txtHfPaymentDt.Text + "','" + txtHfPaymentSeq.Text + "');";
                //    }

                //    imgbtnExample.Visible = CommValue.AUTH_VALUE_TRUE;
                //}

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

            // 전체 해제일 경우
            if (!isAllCheck)
            {
                // 우선 그냥 해제
                for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                {
                    if (!((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                    {
                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_TRUE;
                    }

                    ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_FALSE;
                }
            }
            else
            {
                if (ddlType.SelectedValue.Equals(CommValue.TYPE_SEARCH_VALUE_MAX))
                {
                    // 최대치 설정시
                    // 체스넛 : 7개, 경남비나 3개
                    int intMaxCnt = CommValue.NUMBER_VALUE_0;
                    int intRealMaxCnt = CommValue.NUMBER_VALUE_0;
                    int intRealCnt = CommValue.NUMBER_VALUE_0;
                    string strItemCd = string.Empty;
                    string strFirstItemCd = string.Empty;
                    bool isChestNut = CommValue.AUTH_VALUE_TRUE;

                    if ((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) ||
                        (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA)) ||
                        (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB)))
                    {
                        // 아파트인 경우
                        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                            {
                                if (string.IsNullOrEmpty(strFirstItemCd))
                                {
                                    strFirstItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if ((!strFirstItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                        (!strFirstItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                        (!strFirstItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        // 맨 처음 것이 주차일경우
                                        isChestNut = CommValue.AUTH_VALUE_FALSE;
                                        intMaxCnt = CommValue.MAX_INVOICE_KEANGNAM;
                                    }
                                }

                                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                                {
                                    strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                        (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                        (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        // 맨 처음 것이 주차일경우
                                        isChestNut = CommValue.AUTH_VALUE_FALSE;
                                        intMaxCnt = CommValue.MAX_INVOICE_KEANGNAM;
                                    }
                                    else
                                    {
                                        // 맨 처음 것이 관리비, 수도세, 전기세일경우
                                        isChestNut = CommValue.AUTH_VALUE_TRUE;
                                        intMaxCnt = CommValue.MAX_INVOICE_CHESTNUT;
                                    }

                                    break;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(strItemCd))
                        {
                            strItemCd = strFirstItemCd;
                        }

                        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                            {
                                strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                if (intRealCnt < intMaxCnt)
                                {
                                    if (isChestNut)
                                    {
                                        if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                            (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                            (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                        {
                                            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                                            intRealCnt++;
                                            intRealMaxCnt++;
                                        }
                                        else
                                        {
                                            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                        }
                                    }
                                    else
                                    {
                                        if ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                            (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                            (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                        {
                                            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                                            intRealCnt++;
                                            intRealMaxCnt++;
                                        }
                                        else
                                        {
                                            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                        }
                                    }
                                }
                                else
                                {
                                    if (isChestNut)
                                    {
                                        if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                            (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                            (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                        {
                                            intRealMaxCnt++;
                                        }
                                        else
                                        {
                                            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                        }
                                    }
                                    else
                                    {
                                        if ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                            (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                            (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                        {
                                            intRealMaxCnt++;
                                        }
                                        else
                                        {
                                            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                        }
                                    }
                                }
                            }
                        }

                        // 최대치로 인해 전체 체크가 안될 경우 전체 체크 풀기
                        if (intRealMaxCnt > intRealCnt)
                        {
                            chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                        }
                    }
                    else
                    {
                        // 아파트가 아닌경우
                        intMaxCnt = CommValue.MAX_INVOICE_KEANGNAM;

                        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                            {
                                intRealMaxCnt++;

                                if (intRealCnt < intMaxCnt)
                                {
                                    ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                                    intRealCnt++;
                                }
                            }
                        }

                        // 최대치로 인해 전체 체크가 안될 경우 전체 체크 풀기
                        if (intRealMaxCnt > intRealCnt)
                        {
                            chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                        }
                    }
                }
                else
                {
                    // 전체 설정시
                    //int intMaxCnt = CommValue.NUMBER_VALUE_0;
                    int intRealMaxCnt = CommValue.NUMBER_VALUE_0;
                    string strItemCd = string.Empty;
                    string strFirstItemCd = string.Empty;
                    bool isChestNut = CommValue.AUTH_VALUE_TRUE;

                    if ((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) ||
                        (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA)) ||
                        (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB)))
                    {
                        // 아파트인 경우
                        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                            {
                                if (string.IsNullOrEmpty(strFirstItemCd))
                                {
                                    strFirstItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if ((!strFirstItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                        (!strFirstItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                        (!strFirstItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        // 맨 처음 것이 주차일경우
                                        isChestNut = CommValue.AUTH_VALUE_FALSE;
                                    }
                                }

                                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                                {
                                    strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                        (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                        (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        // 맨 처음 것이 주차일경우
                                        isChestNut = CommValue.AUTH_VALUE_FALSE;
                                    }
                                    else
                                    {
                                        isChestNut = CommValue.AUTH_VALUE_TRUE;
                                    }

                                    break;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(strItemCd))
                        {
                            strItemCd = strFirstItemCd;
                        }


                        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                            {
                                strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                if (isChestNut)
                                {
                                    if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                                    }
                                    else
                                    {
                                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                    }
                                }
                                else
                                {
                                    if ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                        (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                        (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                                    }
                                    else
                                    {
                                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                    }
                                }
                            }
                        }

                        // 각종 아이템이 혼재되어 있어서 전체 체크가 안될 경우 전체 체크 풀기
                        if (intRealMaxCnt > intRowsCnt)
                        {
                            chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                        }
                    }
                    else
                    {
                        // 아파트가 아닌 경우
                        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                            {
                                ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                            }
                        }
                    }
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

                bool isMaxCnt = CommValue.AUTH_VALUE_FALSE;
                int intMaxCnt = CommValue.NUMBER_VALUE_0;

                if (ddlType.SelectedValue.Equals(CommValue.TYPE_SEARCH_VALUE_MAX))
                {
                    isMaxCnt = CommValue.AUTH_VALUE_TRUE;
                }

                if ((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) ||
                    (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA)) ||
                    (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB)))
                {
                    // 아파트인 경우
                    string strItemCd = string.Empty;
                    bool isChestNut = CommValue.AUTH_VALUE_TRUE;

                    for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                    {
                        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                        {
                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                            {
                                // 맨 처음 체크된 것 조사
                                intCheckCount++;
                                strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                if ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                    (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                    (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                {
                                    // 맨 처음 체크된 것이 주차일경우
                                    isChestNut = CommValue.AUTH_VALUE_FALSE;
                                    break;
                                }
                            }
                        }
                    }

                    if (intCheckCount > CommValue.NUMBER_VALUE_0)
                    {
                        intCheckCount = CommValue.NUMBER_VALUE_0;

                        if (isChestNut)
                        {
                            if (isMaxCnt)
                            {
                                intMaxCnt = CommValue.MAX_INVOICE_CHESTNUT;

                                for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                                {
                                    strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                                        {
                                            intTotalCount++;

                                            if (intMaxCnt > intCheckCount)
                                            {
                                                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                                                {
                                                    intCheckCount++;
                                                }
                                            }
                                            else
                                            {
                                                ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_FALSE;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                    }
                                }
                            }
                            else
                            {
                                for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                                {
                                    strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
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
                                    else
                                    {
                                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (isMaxCnt)
                            {
                                intMaxCnt = CommValue.MAX_INVOICE_KEANGNAM;

                                for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                                {
                                    strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                    }
                                    else
                                    {
                                        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                                        {
                                            intTotalCount++;

                                            if (intMaxCnt > intCheckCount)
                                            {
                                                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                                                {
                                                    intCheckCount++;
                                                }
                                            }
                                            else
                                            {
                                                ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_FALSE;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                                {
                                    strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                    {
                                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_FALSE;
                                    }
                                    else
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
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_TRUE;
                        }

                        chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                    }
                }
                else
                {
                    // 아파트가 아닌 경우
                    if (isMaxCnt)
                    {
                        intMaxCnt = CommValue.MAX_INVOICE_KEANGNAM;

                        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                            {
                                intTotalCount++;

                                if (intMaxCnt > intCheckCount)
                                {
                                    if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                                    {
                                        intCheckCount++;
                                    }
                                }
                                else
                                {
                                    ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_FALSE;
                                }
                            }
                        }
                    }
                    else
                    {
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
                    }
                }

                if (intTotalCount == intCheckCount)
                {
                    if (intCheckCount > CommValue.NUMBER_VALUE_0)
                    {
                        chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                    }
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

                string strUserSeq = string.Empty;
                string strPaymentDt = string.Empty; 
                string strDebitCreditCd = string.Empty;
                int intPaymentSeq = CommValue.NUMBER_VALUE_0;
                int intPaymentDetSeq = CommValue.NUMBER_VALUE_0;
                string strTitle = string.Empty;

                if (lvPrintoutList.Items.Count > CommValue.NUMBER_VALUE_0)
                {
                    if ((ddlType.SelectedValue.Equals(CommValue.TYPE_SEARCH_VALUE_ENTIRE)) &&
                        (!string.IsNullOrEmpty(txtTitle.Text)) ||
                        (ddlType.SelectedValue.Equals(CommValue.TYPE_SEARCH_VALUE_MAX)))
                    {
                        string strPrintOutDt = string.Empty;
                        int intPrintOutSeq = CommValue.NUMBER_VALUE_0;
                        int intPrintOutDetSeq = CommValue.NUMBER_VALUE_0;
                        string strBillCd = string.Empty;
                        string strBillNo = string.Empty;
                        string strInsCompCd = Session["CompCd"].ToString();
                        string strInsMemNo = Session["MemNo"].ToString();
                        string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"];
                        bool isTotal = CommValue.AUTH_VALUE_FALSE;

                        // 제목있으면 모아찍기로 간주함.
                        if (!string.IsNullOrEmpty(txtTitle.Text))
                        {
                            isTotal = CommValue.AUTH_VALUE_TRUE;
                        }


                        for (int intTmpI = CommValue.NUMBER_VALUE_0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                        {
                            // CheckBox Check 여부에 따라서 데이터 임시 테이블에 등록후 해당 코드 받아올것
                            // 해당 프린트 출력자 정보 등록해줄 것.
                            if (!string.IsNullOrEmpty(hfRentCd.Value))
                            {
                                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                                {
                                    string strItemCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfItemCd")).Text;

                                    if (!hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) &&
                                        !hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA) &&
                                        !hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB))
                                    {
                                        isChestNut = CommValue.AUTH_VALUE_FALSE;
                                    }
                                    else
                                    {
                                        // 아파트 관리비와 아파트 수도 전기세 이외의 데이터 존재시 경남비나 세금계산서 출력
                                        if (((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) ||
                                             (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA)) ||
                                             (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB))) &&
                                            ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
                                             (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
                                             (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE))))
                                        {
                                            // 해당 사항은 체스넛 세금계산서 출력
                                            isChestNut = CommValue.AUTH_VALUE_FALSE;
                                        }
                                    }

                                    strDebitCreditCd = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfDebitCreditCd")).Text;
                                    strUserSeq = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfUserSeq")).Text;
                                    strPaymentDt = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPaymentDt")).Text;

                                    if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPaymentSeq")).Text))
                                    {
                                        intPaymentSeq = Int32.Parse(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPaymentSeq")).Text);
                                    }

                                    if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPaymentDetSeq")).Text))
                                    {
                                        intPaymentDetSeq = Int32.Parse(((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtHfPaymentDetSeq")).Text);
                                    }

                                    DataTable dtPrintReturn = new DataTable();

                                    if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) ||
                                        hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA) ||
                                        hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB))
                                    {
                                        if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                            (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                            (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                        {
                                            // 아파트 관련 임시 화돈 리스트 생성하기
                                            // 관리비 및 수도, 전기
                                            // KN_USP_SET_INSERT_TEMPHOADONINFO_S00
                                            dtPrintReturn = ReceiptMngBlo.RegistryCNAPTTempHoadonList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq,
                                                                                                      strPrintOutDt, intPrintOutSeq, strBillCd, strBillNo, Session["LangCd"].ToString(),
                                                                                                      strInsCompCd, strInsMemNo, strInsMemIP);
                                        }
                                        else
                                        {
                                            // 아파트 관련 임시 화돈 리스트 생성하기
                                            // 관리비 및 수도, 전기를 제외한 아이템
                                            // KN_USP_SET_INSERT_TEMPHOADONINFO_S01
                                            dtPrintReturn = ReceiptMngBlo.RegistryKNAPTTempHoadonList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq,
                                                                                                      strPrintOutDt, intPrintOutSeq, strBillCd, strBillNo, Session["LangCd"].ToString(),
                                                                                                      strInsCompCd, strInsMemNo, strInsMemIP);
                                        }
                                    }
                                    else if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTSHOP) ||
                                             hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                                             hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                                    {
                                        // 아파트 상가 관련 임시 화돈 리스트 생성하기
                                        // KN_USP_SET_INSERT_TEMPHOADONINFO_S01
                                        dtPrintReturn = ReceiptMngBlo.RegistryKNAPTTempHoadonList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq,
                                                                                                  strPrintOutDt, intPrintOutSeq, strBillCd, strBillNo, Session["LangCd"].ToString(),
                                                                                                  strInsCompCd, strInsMemNo, strInsMemIP);
                                    }
                                    else
                                    {
                                        // 오피스 및 리테일 관련 임시 화돈 리스트 생성하기
                                        // KN_USP_SET_INSERT_TEMPHOADONINFO_S02
                                        dtPrintReturn = ReceiptMngBlo.RegistryKNTempHoadonList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq,
                                                                                               strPrintOutDt, intPrintOutSeq, strBillCd, strBillNo, Session["LangCd"].ToString(),
                                                                                               strInsCompCd, strInsMemNo, strInsMemIP);
                                    }


                                    if (dtPrintReturn != null)
                                    {
                                        if (dtPrintReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                        {
                                            strPrintOutDt = dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutDt"].ToString();

                                            if (!string.IsNullOrEmpty(dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutSeq"].ToString()))
                                            {
                                                intPrintOutSeq = Int32.Parse(dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutSeq"].ToString());
                                            }
                                            else
                                            {
                                                intPrintOutSeq = CommValue.NUMBER_VALUE_0;
                                            }

                                            if (!string.IsNullOrEmpty(dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutDetSeq"].ToString()))
                                            {
                                                intPrintOutDetSeq = Int32.Parse(dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutDetSeq"].ToString());
                                            }
                                            else
                                            {
                                                intPrintOutDetSeq = CommValue.NUMBER_VALUE_0;
                                            }

                                            strBillCd = dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["BillCd"].ToString();
                                            strBillNo = dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["BillNo"].ToString();

                                            // 모아찍기가 아닐경우 동일 데이터 등록
                                            if (!isTotal)
                                            {
                                                if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) ||
                                                    hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA) ||
                                                    hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB))
                                                {
                                                    if ((strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) ||
                                                        (strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)))
                                                    {
                                                        // 아파트 관련 임시 화돈 리스트 생성하기
                                                        // 관리비 및 수도, 전기
                                                        // KN_USP_SET_INSERT_TEMPHOADONINFO_M00
                                                        ReceiptMngBlo.RegistryCNAPTTempHoadonTotalList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq,
                                                                                                           strPrintOutDt, intPrintOutSeq, strBillCd, strBillNo, Session["LangCd"].ToString(),
                                                                                                           strInsCompCd, strInsMemNo, strInsMemIP);
                                                    }
                                                    else
                                                    {
                                                        // 아파트 관련 임시 화돈 리스트 생성하기
                                                        // 관리비 및 수도, 전기를 제외한 아이템
                                                        // KN_USP_SET_INSERT_TEMPHOADONINFO_M01
                                                        ReceiptMngBlo.RegistryKNAPTTempHoadonTotalList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq,
                                                                                                       strPrintOutDt, intPrintOutSeq, strBillCd, strBillNo, Session["LangCd"].ToString(),
                                                                                                       strInsCompCd, strInsMemNo, strInsMemIP);
                                                    }
                                                }
                                                else if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTSHOP) ||
                                                         hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                                                         hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                                                {
                                                    // 아파트 상가 관련 임시 화돈 리스트 생성하기
                                                    // KN_USP_SET_INSERT_TEMPHOADONINFO_M01
                                                    ReceiptMngBlo.RegistryKNAPTTempHoadonTotalList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq,
                                                                                                   strPrintOutDt, intPrintOutSeq, strBillCd, strBillNo, Session["LangCd"].ToString(),
                                                                                                   strInsCompCd, strInsMemNo, strInsMemIP);
                                                }
                                                else
                                                {
                                                    // 오피스 및 리테일 관련 임시 화돈 리스트 생성하기
                                                    // KN_USP_SET_INSERT_TEMPHOADONINFO_M02
                                                    ReceiptMngBlo.RegistryKNTempHoadonTotalList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq,
                                                                                                strPrintOutDt, intPrintOutSeq, strBillCd, strBillNo, Session["LangCd"].ToString(),
                                                                                                strInsCompCd, strInsMemNo, strInsMemIP);
                                                }
                                            }

                                            intCheckRow++;
                                        }
                                    }
                                }
                            }
                        }

                        // 모아찍기일 경우 한줄로 넣어줌.
                        if (isTotal)
                        {
                            // KN_USP_SET_INSERT_TEMPHOADONTOTALINFO_M00
                            ReceiptMngBlo.RegistryTempHoadonTotalInfo(strPrintOutDt, intPrintOutSeq, txtTitle.Text);
                        }

                        // 선택 사항이 있는지 없는지 체크
                        if (intCheckRow > CommValue.NUMBER_VALUE_0)
                        {
                            StringBuilder sbPrintOut = new StringBuilder();

                            // 프린트 버튼 클릭시점에 정상 화돈 테이블로 이동 및 카운트 증가시킴
                            if (isChestNut)
                            {
                                if (!string.IsNullOrEmpty(txtTitle.Text))
                                {
                                    // 체스넛 화돈 edition 3
                                    sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupCombineReciptCN.aspx?Datum0=" + strPrintOutDt + "&Datum1=" + intPrintOutSeq + "\", \"Reciept\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                                }
                                else
                                {
                                    // 체스넛 화돈 edition 2
                                    sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupCombineReciptCN.aspx?Datum0=" + strPrintOutDt + "&Datum1=" + intPrintOutSeq + "\", \"Reciept\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(txtTitle.Text))
                                {
                                    // 경남비나 화돈 Edition 3
                                    sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupCombineReciptKN.aspx?Datum0=" + strPrintOutDt + "&Datum1=" + intPrintOutSeq + "\", \"Reciept\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                                }
                                else
                                {
                                    // 경남비나 화돈 Edition 2
                                    sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupCombineReciptCN.aspx?Datum0=" + strPrintOutDt + "&Datum1=" + intPrintOutSeq + "\", \"Reciept\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                                }
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
                    else
                    {
                        // 제목 없음
                        StringBuilder sbNoSelection = new StringBuilder();

                        sbNoSelection.Append("alert('" + AlertNm["ALERT_INSERT_TITLE"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoTitle", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                }
                else
                {
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

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue.Equals(CommValue.TYPE_SEARCH_VALUE_ENTIRE))
            {
                txtTitle.ReadOnly = CommValue.AUTH_VALUE_FALSE;
            }
            else if (ddlType.SelectedValue.Equals(CommValue.TYPE_SEARCH_VALUE_MAX))
            {
                txtTitle.Text = string.Empty;
                txtTitle.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            }

            // 우선 그냥 해제
            for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
            {
                if (!((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled = CommValue.AUTH_VALUE_TRUE;
                }

                ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_FALSE;
            }

            chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
        }

        protected void imgbtnHoadonPreView_Click(object sender, EventArgs e)
        {
            Session["HoadonOk"] = CommValue.AUTH_VALUE_FULL;
        }
    }
}