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

using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class HoadonReserveList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        public string DATA_APT = CommValue.RENTAL_VALUE_APT;
        public string DATA_APTSTORE = CommValue.RENTAL_VALUE_APTSHOP;

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

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();

                    if (Request.Params[Master.PARAM_DATA5] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA5].ToString()))
                        {
                            hfCurrentPage.Value = Request.Params[Master.PARAM_DATA5].ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(hfCurrentPage.Value))
                    {
                        intPageNo = Int32.Parse(hfCurrentPage.Value);
                        hfCurrentPage.Value = intPageNo.ToString();

                        isReturnOk = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                        hfCurrentPage.Value = intPageNo.ToString();

                        isReturnOk = CommValue.AUTH_VALUE_TRUE;
                    }
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltSearchName.Text = TextNm["NAME"];
            ltSearchRoom.Text = TextNm["ROOMNO"];
            lnkbtnIssuing.Text = TextNm["ISSUING"];
            lnkbtnEntireIssuing.Text = TextNm["TOTISSUING"];

            MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlCompNo, "00000000");
  
            // Payment Document
            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlPayment, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT, TextNm["PAYMENTKIND"]);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnEntireIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";
            lnkbtnEntireIssuing.OnClientClick = "javascript:return fnEntireIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";

            lnkbtnEntireIssuing.Visible = CommValue.AUTH_VALUE_FALSE;
        }

        protected void LoadData()
        {
            string strSearchNm = string.Empty;
            string strSearchRoomNo = string.Empty;

            if (lvInvoiceList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                ((CheckBox)lvInvoiceList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;
            }

            if (!string.IsNullOrEmpty(txtSearchNm.Text))
            {
                strSearchNm = txtSearchNm.Text;
            }

            if (!string.IsNullOrEmpty(txtSearchRoom.Text))
            {
                strSearchRoomNo = txtSearchRoom.Text;
            }

            // KN_USP_MNG_SELECT_SETTLEMENT_S00
            DataSet dsReturn = BalanceMngBlo.SpreadReserveHoadonList((CommValue.BOARD_VALUE_PAGESIZE), Int32.Parse(hfCurrentPage.Value), hfRentCd.Value,
                                                                     txtSearchNm.Text, txtSearchRoom.Text, ddlPayment.SelectedValue, Session["LangCd"].ToString(),
                                                                     ddlCompNo.SelectedValue);

            if (dsReturn != null)
            {
                lvInvoiceList.DataSource = dsReturn.Tables[1];
                lvInvoiceList.DataBind();

                CheckBox chkAll = (CheckBox)lvInvoiceList.FindControl("chkAll");

                if (lvInvoiceList.Items.Count > CommValue.NUMBER_VALUE_0)
                {
                    if (intRowsCnt == CommValue.NUMBER_VALUE_0)
                    {
                        chkAll.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        chkAll.Enabled = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString()), 
                                  TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        protected void lvInvoiceList_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltRoomNo = (Literal)lvInvoiceList.FindControl("ltRoomNo");
            ltRoomNo.Text = TextNm["ROOMNO"];
            Literal ltName = (Literal)lvInvoiceList.FindControl("ltName");
            ltName.Text = TextNm["NAME"];
            Literal ltTaxCd = (Literal)lvInvoiceList.FindControl("ltTaxCd");
            ltTaxCd.Text = TextNm["TAXCD"];
            Literal ltPaymentKind = (Literal)lvInvoiceList.FindControl("ltPaymentKind");
            ltPaymentKind.Text = TextNm["PAYMENTKIND"];
            Literal ltPaymentDt = (Literal)lvInvoiceList.FindControl("ltPaymentDt");
            ltPaymentDt.Text = TextNm["PAYDAY"];
            Literal ltPayMethod = (Literal)lvInvoiceList.FindControl("ltPayMethod");
            ltPayMethod.Text = TextNm["PAYMETHOD"];
            Literal ltAmount = (Literal)lvInvoiceList.FindControl("ltAmount");
            ltAmount.Text = TextNm["TOTALAMT"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvInvoiceList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    ((Literal)e.Item.FindControl("ltRoomNo")).Text = TextNm["ROOMNO"];
                    ((Literal)e.Item.FindControl("ltName")).Text = TextNm["NAME"];
                    ((Literal)e.Item.FindControl("ltTaxCd")).Text = TextNm["TAXCD"];
                    ((Literal)e.Item.FindControl("ltPaymentKind")).Text = TextNm["PAYMENTKIND"];
                    ((Literal)e.Item.FindControl("ltPaymentDt")).Text = TextNm["PAYDAY"];
                    ((Literal)e.Item.FindControl("ltPayMethod")).Text = TextNm["PAYMETHOD"];
                    ((Literal)e.Item.FindControl("ltAmount")).Text = TextNm["TOTALAMT"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvInvoiceList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                CheckBox chkboxList = (CheckBox)iTem.FindControl("chkboxList");
                TextBox txtInsTaxCd = (TextBox)iTem.FindControl("txtInsTaxCd");
                TextBox txtHfInsTaxCd = (TextBox)iTem.FindControl("txtHfInsTaxCd");

                if (!string.IsNullOrEmpty(drView["UserTaxCd"].ToString()))
                {
                    txtInsTaxCd.Text = TextLib.StringDecoder(drView["UserTaxCd"].ToString());
                    txtHfInsTaxCd.Text = TextLib.StringDecoder(drView["UserTaxCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["StatusCd"].ToString()))
                {
                    if (drView["StatusCd"].ToString().Equals(CommValue.CALCULATE_STATUS_TYPE_VALUE_APPROVAL))
                    {
                        chkboxList.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        chkboxList.Enabled = CommValue.AUTH_VALUE_TRUE;
                        intRowsCnt++;
                    }
                }                

                Literal ltInsPaymentDt = (Literal)iTem.FindControl("ltInsPaymentDt");
                HiddenField hfInsPaymentDt = (HiddenField)iTem.FindControl("hfInsPaymentDt");

                TextBox txtHfPaymentDt = (TextBox)iTem.FindControl("txtHfPaymentDt");
                TextBox txtHfPaymentSeq = (TextBox)iTem.FindControl("txtHfPaymentSeq");
                TextBox txtHfPaymentDetSeq = (TextBox)iTem.FindControl("txtHfPaymentDetSeq");
                TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");

                if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    ltInsPaymentDt.Text = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["PrintOutDt"].ToString()));
                    hfInsPaymentDt.Value = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["PaymentDt"].ToString()));
                    
                    txtHfPaymentDt.Text = TextLib.StringDecoder(drView["PaymentDt"].ToString());
                    txtHfPaymentSeq.Text = TextLib.StringDecoder(drView["PaymentSeq"].ToString());                                        
                    txtHfPaymentDetSeq.Text = TextLib.StringDecoder(drView["PaymentDetSeq"].ToString());                    
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                TextBox txtHfClassCd = (TextBox)iTem.FindControl("txtHfClassCd");

                if (!string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    txtHfClassCd.Text = TextLib.StringDecoder(drView["ClassCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    Literal ltInsNm = (Literal)iTem.FindControl("ltInsNm");
                    ltInsNm.Text = TextLib.TextCutString(TextLib.StringDecoder(drView["UserNm"].ToString()), 20, "...");
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    Literal ltInsPaymentKind = (Literal)iTem.FindControl("ltInsPaymentKind");

                    if (Session["LangCd"].Equals(CommValue.LANG_VALUE_VIETNAMESE))
                    {
                        ltInsPaymentKind.Text = drView["SvcMM"].ToString() + " / " + drView["SvcYear"].ToString() + " " + TextLib.StringDecoder(drView["ClassNm"].ToString());
                    }
                    else
                    {
                        ltInsPaymentKind.Text = drView["SvcYear"].ToString() + " / " + drView["SvcMM"].ToString() + " " + TextLib.StringDecoder(drView["ClassNm"].ToString());
                    }
                }

                if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
                {
                    Literal ltInsPayMethod = (Literal)iTem.FindControl("ltInsPayMethod");
                    ltInsPayMethod.Text = TextLib.StringDecoder(drView["PaymentNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ItemTotViAmt"].ToString()))
                {
                    Literal ltInsAmount = (Literal)iTem.FindControl("ltInsAmount");
                    ltInsAmount.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["ItemTotViAmt"].ToString()).ToString("###,##0"));
                }

                ImageButton imgbtnEdit = (ImageButton)iTem.FindControl("imgbtnEdit");

                imgbtnEdit.OnClientClick = "javascript:return fnEditor('" + hfInsPaymentDt.Value + "','" + txtHfPaymentSeq.Text + "','" + txtHfPaymentDetSeq.Text + "');";

                ImageButton imgbtnExample = (ImageButton)iTem.FindControl("imgbtnExample");

                if ((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) &&
                    (txtHfClassCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                    (txtHfClassCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE)) ||
                    (txtHfClassCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)))
                {
                    //imgbtnExample.OnClientClick = "javascript:return fnChestNutPreview('" + txtHfUserSeq.Text + "','" + txtHfPrintSeq.Text + "','" + txtHfPrintDetSeq.Text + "');";
                    imgbtnExample.OnClientClick = "javascript:return fnChestNutPreview('" + txtHfUserSeq.Text + "','" + txtHfPaymentDt.Text.Replace("-", "") + "','" + txtHfPaymentSeq.Text + "','" + txtInsTaxCd.ClientID + "','" + ltInsPaymentDt.Text.Replace("-", "") + "','Y');";
                }
                else
                {
                    imgbtnExample.OnClientClick = "javascript:return fnKeangNamPreview('" + txtHfUserSeq.Text + "','" + txtHfPaymentDt.Text.Replace("-", "") + "','" + txtHfPaymentSeq.Text + "','" + txtInsTaxCd.ClientID + "','" + ltInsPaymentDt.Text.Replace("-", "") + "','Y');";
                }

                // lnkbtnPreview.Visible = Master.isModDelAuthOk;
                // 양영석 : 임시 테스트용 실제상황에서는 수정 및 삭제 권한에 따라 처리함.
                if (Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
                {
                    imgbtnEdit.Visible = CommValue.AUTH_VALUE_TRUE;
                    //imgbtnExample.Visible = CommValue.AUTH_VALUE_TRUE;
                }

                //Literal ltCalendar = (Literal)iTem.FindControl("ltCalendar");
                //ltCalendar.Text = "<a href=\"#\"><img align=\"absmiddle\" alt=\"Calendar\" onclick=\"Calendar(this, '" + ltInsPaymentDt.ClientID + "','" + hfInsPaymentDt.ClientID + "', true)\" src=\"/Common/Images/Common/calendar.gif\" style=\"cursor:pointer;\" value=\"\"/></a>";
            }
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllCheck = ((CheckBox)lvInvoiceList.FindControl("chkAll")).Checked;

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
            for (int intTmpI = 0; intTmpI < lvInvoiceList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvInvoiceList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)lvInvoiceList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
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

                for (int intTmpI = 0; intTmpI < lvInvoiceList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvInvoiceList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                    {
                        intTotalCount++;

                        if (((CheckBox)lvInvoiceList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            intCheckCount++;
                        }
                    }
                }

                if (intTotalCount == intCheckCount)
                {
                    ((CheckBox)lvInvoiceList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ((CheckBox)lvInvoiceList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtInsTaxCd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                for (int intTmpI = CommValue.NUMBER_VALUE_0; intTmpI < lvInvoiceList.Items.Count; intTmpI++)
                {
                    string strOldTaxCd = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfinsTaxCd")).Text;
                    string strNewTaxCd = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtInsTaxCd")).Text;

                    if (!strOldTaxCd.Equals(strNewTaxCd))
                    {
                        string strPaymentDt = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentDt")).Text;
                        string strPaymentSeq = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentSeq")).Text;

                        int intPaymentSeq = CommValue.NUMBER_VALUE_0;

                        if (!string.IsNullOrEmpty(strPaymentSeq))
                        {
                            intPaymentSeq = Int32.Parse(strPaymentSeq);

                            // KN_USP_MNG_UPDATE_SETTLEMENT_M00
                            BalanceMngBlo.ModifyLedgerinfoForTaxCd(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strPaymentDt, intPaymentSeq, strNewTaxCd);
                        }
                    }
                }

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnHoadonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + hfRentCd.Value + "&" + Master.PARAM_DATA2 + "=" + hfPaymentDt.Value.Replace("-", "") + "&" + Master.PARAM_DATA3 + "=" + hfPaymentSeq.Value + "&" + Master.PARAM_DATA4 + "=" + hfPaymentDetSeq.Value + "&" + Master.PARAM_DATA5 + "=" + hfCurrentPage.Value, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 전체 발행
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnEntireIssuing_Click(object sender, EventArgs e)
        {
            try
            {
                int intRowCnt = lvInvoiceList.Items.Count;

                if (intRowCnt > CommValue.NUMBER_VALUE_0)
                {
                    string strIp = Request.ServerVariables["REMOTE_ADDR"];

                    //// 영수증 출력테이블에 모든 데이터를 등록시키고 결제완료 처리시킴.
                    //MngAccountsBlo.RegistryReserveEntireHoadonList(Session["MemNo"].ToString(), strIp);

                    //hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                    //// 화면 초기화
                    //LoadData();

                    //// 화돈 출력권한 부여
                    //Session["HoadonOk"] = CommValue.AUTH_VALUE_FULL;

                    //// 화돈 대상 데이터 출력
                    //StringBuilder sbRdHoadon = new StringBuilder();

                    //sbRdHoadon.Append("window.open(\"/Common/RdPopup/RDPopupHoadonList.aspx\", \"HoaDon\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");

                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HoadonPrint", sbRdHoadon.ToString(), CommValue.AUTH_VALUE_TRUE);
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
        /// 발행
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {
            try
            {
                //// 체크된 데이터 영수증 발행
                //int intRowCnt = lvInvoiceList.Items.Count;
                //int intCheckCnt = CommValue.NUMBER_VALUE_0;

                //bool isChestNut = CommValue.AUTH_VALUE_FALSE;

                //if (intRowCnt > CommValue.NUMBER_VALUE_0)
                //{
                //    for (int intTmpI = intRowCnt - 1; intTmpI >= 0; intTmpI--)
                //    {
                //        if (!string.IsNullOrEmpty(hfRentCd.Value))
                //        {
                //            if (((CheckBox)lvInvoiceList.Items[intTmpI].FindControl("chkboxList")).Checked)
                //            {
                //                string strClassCd = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfClassCd")).Text;
                                
                //                string strPaymentDt = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentDt")).Text.Replace("-", "");
                //                string strPaymentSeq = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentSeq")).Text;
                //                string strUserSeq = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfUserSeq")).Text;
                //                string strInsNm = ((Literal)lvInvoiceList.Items[intTmpI].FindControl("ltInsNm")).Text;
                //                string strInsTaxCd = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtInsTaxCd")).Text;
                //                string strInsPaymentDt = ((HiddenField)lvInvoiceList.Items[intTmpI].FindControl("hfInsPaymentDt")).Value.Replace("-", "");
                //                string strIp = Request.ServerVariables["REMOTE_ADDR"];

                //                int intPaymentSeq = 0;

                //                //string strPaymentDetSeq = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentDetSeq")).Text;
                //                //string strInsRoomNo = ((Literal)lvInvoiceList.Items[intTmpI].FindControl("ltInsRoomNo")).Text;

                //                if (!string.IsNullOrEmpty(strPaymentSeq))
                //                {
                //                    intPaymentSeq = Int32.Parse(strPaymentSeq);
                //                }

                //                if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) &&
                //                    (strClassCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE) ||
                //                     strClassCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE) ||
                //                     strClassCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE) ||
                //                     strClassCd.Equals(CommValue.ITEM_TYPE_VALUE_GASCHARGE)))
                //                {
                //                    // 아파트 관련 관리비 및 수도광열비 영수증 출력테이블에 해당 결제정보 등록시키고 결제완료 처리시킴.
                //                    // KN_USP_MNG_INSERT_HOADONINFO_M00
                //                    BalanceMngBlo.RegistryChestNutHoadonList(Session["LangCd"].ToString(), strUserSeq, strInsNm, strPaymentDt, intPaymentSeq, string.Empty,
                //                                                             string.Empty, strInsTaxCd, strInsPaymentDt, string.Empty, string.Empty,
                //                                                             Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp, string.Empty);

                //                    isChestNut = CommValue.AUTH_VALUE_TRUE;
                //                }
                //                else
                //                {
                //                    if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) ||
                //                        hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTSHOP))
                //                    {
                //                        // 아파트 기타 잡비외 아파트 상가 관련 처리
                //                        // KN_USP_MNG_INSERT_HOADONINFO_M01
                //                        BalanceMngBlo.RegistryKeangNamHoadonList(Session["LangCd"].ToString(), strUserSeq, strInsNm, strPaymentDt, intPaymentSeq, string.Empty,
                //                                                                 string.Empty, strInsTaxCd, strInsPaymentDt, string.Empty, string.Empty,
                //                                                                 Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp, string.Empty);
                //                    }
                //                    else
                //                    {
                //                        // 리테일 및 오피스 관련 처리
                //                        // KN_USP_MNG_INSERT_HOADONINFO_M02
                //                        BalanceMngBlo.RegistryReserveHoadonList(Session["LangCd"].ToString(), strUserSeq, strInsNm, strPaymentDt, intPaymentSeq, string.Empty,
                //                                                                string.Empty, strInsTaxCd, strInsPaymentDt, string.Empty, string.Empty,
                //                                                                Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp, string.Empty);
                //                    }
                //                }

                //                intCheckCnt++;
                //            }
                //        }
                //    }

                //    hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                //    // 화면 초기화
                //    LoadData();

                //    if (intCheckCnt > CommValue.NUMBER_VALUE_0)
                //    {
                //        // 화돈 출력권한 부여
                //        Session["HoadonOk"] = CommValue.AUTH_VALUE_FULL;

                //        // 화돈 대상 데이터 출력
                //        StringBuilder sbRdHoadon = new StringBuilder();

                //        if (isChestNut)
                //        {
                //            sbRdHoadon.Append("window.open(\"/Common/RdPopup/RDPopupChestNutHoadonList.aspx\", \"HoaDon\", \"status=no, resizable=no, width=729, height=600, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                //        }
                //        else
                //        {
                //            sbRdHoadon.Append("window.open(\"/Common/RdPopup/RDPopupKeangnamHoadonList.aspx\", \"HoaDon\", \"status=no, resizable=no, width=729, height=600, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                //        }

                //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HoadonPrint", sbRdHoadon.ToString(), CommValue.AUTH_VALUE_TRUE);
                //    }
                //    else
                //    {
                //        // 선택된 대상 없음
                //        StringBuilder sbNoSelection = new StringBuilder();

                //        sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                //    }
                //}

                // 체크된 데이터 영수증 발행
                int intRowCnt = lvInvoiceList.Items.Count;
                int intCheckCnt = CommValue.NUMBER_VALUE_0;

                bool isChestNut = CommValue.AUTH_VALUE_FALSE;
                string strHoadonNo = string.Empty;

                if (intRowCnt > CommValue.NUMBER_VALUE_0)
                {
                    for (int intTmpI = intRowCnt - 1; intTmpI >= 0; intTmpI--)
                    {
                        if (!string.IsNullOrEmpty(hfRentCd.Value))
                        {
                            if (((CheckBox)lvInvoiceList.Items[intTmpI].FindControl("chkboxList")).Checked)
                            {
                                // 임시 테이블로 데이터 복제
                                string strClassCd = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfClassCd")).Text;

                                string strPaymentDt = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentDt")).Text.Replace("-", "");
                                string strPaymentSeq = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentSeq")).Text;
                                string strPaymentDetSeq = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentDetSeq")).Text;

                                string strUserSeq = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfUserSeq")).Text;
                                string strInsNm = ((Literal)lvInvoiceList.Items[intTmpI].FindControl("ltInsNm")).Text;
                                string strInsTaxCd = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtInsTaxCd")).Text;
                                string strInsPaymentDt = ((HiddenField)lvInvoiceList.Items[intTmpI].FindControl("hfInsPaymentDt")).Value.Replace("-", "");
                                string strIp = Request.ServerVariables["REMOTE_ADDR"];

                                int intPaymentSeq = 0;
                                int intPaymentDetSeq = 0;

                                //string strPaymentDetSeq = ((TextBox)lvInvoiceList.Items[intTmpI].FindControl("txtHfPaymentDetSeq")).Text;
                                //string strInsRoomNo = ((Literal)lvInvoiceList.Items[intTmpI].FindControl("ltInsRoomNo")).Text;

                                if (!string.IsNullOrEmpty(strPaymentSeq))
                                {
                                    intPaymentSeq = Int32.Parse(strPaymentSeq);
                                }

                                if (!string.IsNullOrEmpty(strPaymentDetSeq))
                                {
                                    intPaymentDetSeq = Int32.Parse(strPaymentDetSeq);
                                }

                                if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) &&
                                    (strClassCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE) ||
                                     strClassCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE) ||
                                     strClassCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE) ||
                                     strClassCd.Equals(CommValue.ITEM_TYPE_VALUE_GASCHARGE)))
                                {
                                    // 아파트 관련 관리비 및 수도광열비 영수증 출력테이블에 해당 결제정보 등록시키고 결제완료 처리시킴.
                                    // KN_USP_MNG_INSERT_TEMPINVOICEINFO_M00
                                    DataTable dtReturn = InvoiceMngBlo.InsertTempHoadonForEachAPT(Session["LangCd"].ToString(), strUserSeq, strInsNm, strPaymentDt, intPaymentSeq, intPaymentDetSeq, string.Empty,
                                                                                                  string.Empty, strInsTaxCd, strInsPaymentDt, string.Empty, string.Empty, strHoadonNo,
                                                                                                  string.Empty, string.Empty, CommValue.NUMBER_VALUE_0_0,
                                                                                                  Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                                    if (dtReturn != null)
                                    {
                                        if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                        {
                                            strHoadonNo = dtReturn.Rows[0]["TempSerialNo"].ToString();
                                        }
                                    }

                                    isChestNut = CommValue.AUTH_VALUE_TRUE;
                                }
                                else
                                {
                                    if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) ||
                                        hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTSHOP))
                                    {
                                        // 아파트 기타 잡비외 아파트 상가 관련 처리
                                        // KN_USP_MNG_INSERT_TEMPINVOICEINFO_M01
                                        DataTable dtReturn = InvoiceMngBlo.InsertTempHoadonForEachAPTRetail(Session["LangCd"].ToString(), strUserSeq, strInsNm, strPaymentDt, intPaymentSeq, intPaymentDetSeq, string.Empty,
                                                                                                            string.Empty, strInsTaxCd, strInsPaymentDt, string.Empty, string.Empty, strHoadonNo,
                                                                                                            Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                                        if (dtReturn != null)
                                        {
                                            if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                            {
                                                strHoadonNo = dtReturn.Rows[0]["HoadonNo"].ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 리테일 및 오피스 관련 처리
                                        // KN_USP_MNG_INSERT_TEMPINVOICEINFO_M02
                                        DataTable dtReturn = InvoiceMngBlo.InsertTempHoadonForEachTower(Session["LangCd"].ToString(), strUserSeq, strInsNm, strPaymentDt, intPaymentSeq, string.Empty,
                                                                                                        string.Empty, strInsTaxCd, strInsPaymentDt, string.Empty, string.Empty, strHoadonNo,
                                                                                                        Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                                        if (dtReturn != null)
                                        {
                                            if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                            {
                                                strHoadonNo = dtReturn.Rows[0]["HoadonNo"].ToString();
                                            }
                                        }
                                    }
                                }

                                intCheckCnt++;
                            }
                        }
                    }

                    hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                    // 화면 초기화
                    LoadData();

                    if (intCheckCnt > CommValue.NUMBER_VALUE_0)
                    {
                        // 팝업을 띄움.
                        // 팝업에서 프린터 및 값처리 기능 필요
                        // 화돈 출력권한 부여
                        Session["HoadonOk"] = CommValue.AUTH_VALUE_FULL;

                        // 화돈 대상 데이터 출력
                        StringBuilder sbRdHoadon = new StringBuilder();

                        if (isChestNut)
                        {
                            sbRdHoadon.Append("window.open(\"/Common/RdPopup/RDPopupHoadonCNPreview.aspx?Datum0=" + strHoadonNo + "\", \"HoaDon\", \"status=no, resizable=no, width=729, height=600, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                        }
                        else
                        {
                            sbRdHoadon.Append("window.open(\"/Common/RdPopup/RDPopupHoadonKNPreview2.aspx?Datum0=" + strHoadonNo + "\", \"HoaDon\", \"status=no, resizable=no, width=729, height=600, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                        }

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HoadonTempPrint", sbRdHoadon.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    else
                    {
                        // 선택된 대상 없음
                        StringBuilder sbNoSelection = new StringBuilder();

                        sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
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

        protected void imgbtnHoadonPreView_Click(object sender, EventArgs e)
        {
            Session["HoadonOk"] = CommValue.AUTH_VALUE_FULL;
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
    }
}