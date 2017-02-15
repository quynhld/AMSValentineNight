using System;
using System.Data;
using System.EnterpriseServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    [Transaction(TransactionOption.Required)]
    public partial class CancelList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = CommValue.NUMBER_VALUE_0;

        string strPayYn = string.Empty;

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
                        Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 매개변수 체크
        /// </summary>
        /// <returns></returns>
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

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        /// <summary>
        /// 각 컨트롤 초기화
        /// </summary>
        protected void InitControls()
        {

        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_MNG_SELECT_SETTELMEMTCANCEL_S00
            dsReturn = BalanceMngBlo.SpreadCancelSettelmentList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                lvCancelList.DataSource = dsReturn.Tables[1];
                lvCancelList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString()),
                                  TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCancelList_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltSeq = (Literal)lvCancelList.FindControl("ltSeq");
            ltSeq.Text = TextNm["SEQ"];
            Literal ltRentNm = (Literal)lvCancelList.FindControl("ltRentNm");
            ltRentNm.Text = TextNm["RENT"];
            Literal ltItemNm = (Literal)lvCancelList.FindControl("ltItemNm");
            ltItemNm.Text = TextNm["ITEMFEE"];
            Literal ltUserNm = (Literal)lvCancelList.FindControl("ltUserNm");
            ltUserNm.Text = TextNm["NAME"];
            Literal ltPaymentNm = (Literal)lvCancelList.FindControl("ltPaymentNm");
            ltPaymentNm.Text = TextNm["PAYMETHOD"];
            Literal ltAmt = (Literal)lvCancelList.FindControl("ltAmt");
            ltAmt.Text = TextNm["AMT"];
            Literal ltPaymentDt = (Literal)lvCancelList.FindControl("ltPaymentDt");
            ltPaymentDt.Text = TextNm["PAYDAY"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCancelList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    Literal ltSeq = (Literal)e.Item.FindControl("ltSeq");
                    ltSeq.Text = TextNm["SEQ"];
                    Literal ltRentNm = (Literal)e.Item.FindControl("ltRentNm");
                    ltRentNm.Text = TextNm["RENT"];
                    Literal ltItemNm = (Literal)e.Item.FindControl("ltItemNm");
                    ltItemNm.Text = TextNm["ITEMFEE"];
                    Literal ltUserNm = (Literal)e.Item.FindControl("ltUserNm");
                    ltUserNm.Text = TextNm["NAME"];
                    Literal ltPaymentNm = (Literal)e.Item.FindControl("ltPaymentNm");
                    ltPaymentNm.Text = TextNm["PAYMETHOD"];
                    Literal ltAmt = (Literal)e.Item.FindControl("ltAmt");
                    ltAmt.Text = TextNm["AMT"];
                    Literal ltPaymentDt = (Literal)e.Item.FindControl("ltPaymentDt");
                    ltPaymentDt.Text = TextNm["PAYDAY"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvCancelList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    Literal ltInsSeq = (Literal)iTem.FindControl("ltInsSeq");
                    ltInsSeq.Text = drView["Seq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    TextBox txtHfRentCd = (TextBox)iTem.FindControl("txtHfRentCd");
                    txtHfRentCd.Text = drView["RentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ItemNm"].ToString()))
                {
                    Literal ltInsItemNm = (Literal)iTem.FindControl("ltInsItemNm");
                    ltInsItemNm.Text = drView["ItemNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ItemCd"].ToString()))
                {
                    TextBox txtHfItemCd = (TextBox)iTem.FindControl("txtHfItemCd");
                    txtHfItemCd.Text = drView["ItemCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ItemSeq"].ToString()))
                {
                    TextBox txtHfItemSeq = (TextBox)iTem.FindControl("txtHfItemSeq");
                    txtHfItemSeq.Text = drView["ItemSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    Literal ltInsUserNm = (Literal)iTem.FindControl("ltInsUserNm");
                    ltInsUserNm.Text = TextLib.StringDecoder(drView["UserNm"].ToString());

                    TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();

                    TextBox txtHfFloor = (TextBox)iTem.FindControl("txtHfFloor");
                    txtHfFloor.Text = drView["FloorNo"].ToString();

                    TextBox txtHfRoom = (TextBox)iTem.FindControl("txtHfRoom");
                    txtHfRoom.Text = drView["RoomNo"].ToString();

                    Literal ltIntRentNm = (Literal)iTem.FindControl("ltIntRentNm");
                    ltIntRentNm.Text = drView["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["SvcYear"].ToString()))
                {
                    TextBox txtHfSvcYear = (TextBox)iTem.FindControl("txtHfSvcYear");
                    txtHfSvcYear.Text = drView["SvcYear"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["SvcMM"].ToString()))
                {
                    TextBox txtHfSvcMM = (TextBox)iTem.FindControl("txtHfSvcMM");
                    txtHfSvcMM.Text = drView["SvcMM"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PaymentCd"].ToString()))
                {
                    Literal ltInsPaymentNm = (Literal)iTem.FindControl("ltInsPaymentNm");
                    ltInsPaymentNm.Text = TextLib.StringDecoder(drView["PaymentNm"].ToString());

                    TextBox txtHfPaymentCd = (TextBox)iTem.FindControl("txtHfPaymentCd");
                    txtHfPaymentCd.Text = drView["PaymentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StatusCd"].ToString()))
                {
                    TextBox txtHfStatusCd = (TextBox)iTem.FindControl("txtHfStatusCd");
                    txtHfStatusCd.Text = drView["StatusCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ItemTotViAmt"].ToString()))
                {
                    Literal ltInsAmt = (Literal)iTem.FindControl("ltInsAmt");
                    ltInsAmt.Text = TextLib.MakeVietIntNo(double.Parse(drView["ItemTotViAmt"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    string strPaymentDt = string.Empty;

                    Literal ltInsPaymentDt = (Literal)iTem.FindControl("ltInsPaymentDt");
                    strPaymentDt = drView["PaymentDt"].ToString();

                    ltInsPaymentDt.Text = strPaymentDt.Substring(0, 4) + "-" + strPaymentDt.Substring(4, 2) + "-" + strPaymentDt.Substring(6, 2);
                }

                if (!string.IsNullOrEmpty(drView["PaymentSeq"].ToString()))
                {
                    TextBox txtHfPaymentSeq = (TextBox)iTem.FindControl("txtHfPaymentSeq");
                    txtHfPaymentSeq.Text = drView["PaymentSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PaymentDetSeq"].ToString()))
                {
                    TextBox txtHfPaymentDetSeq = (TextBox)iTem.FindControl("txtHfPaymentDetSeq");
                    txtHfPaymentDetSeq.Text = drView["PaymentDetSeq"].ToString();
                }

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.Visible = Master.isModDelAuthOk;
            }
        }

        protected void lvCancelList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                object[] objReturn = new object[2];
                string strInsMemIp = Request.ServerVariables["REMOTE_ADDR"];
                string strPrintSeq = string.Empty;
                string strPrintDetSeq = string.Empty;

                TextBox txtHfRentCd = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfRentCd");
                TextBox txtHfPaymentDt = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfPaymentDt");
                TextBox txtHfPaymentSeq = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfPaymentSeq");
                TextBox txtHfPaymentDetSeq = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfPaymentDetSeq");
                TextBox txtHfItemCd = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfItemCd");
                TextBox txtHfItemSeq = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfItemSeq");
                TextBox txtHfUserSeq = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfUserSeq");
                TextBox txtHfSvcYear = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfSvcYear");
                TextBox txtHfSvcMM = (TextBox)lvCancelList.Items[e.ItemIndex].FindControl("txtHfSvcMM");

                if ((txtHfItemCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) ||
                   (txtHfItemCd.Text.Equals(CommValue.ITEM_TYPE_VALUE_RENTALFEE)))
                {
                    /* 관리비 및 임대료 처리 부분 시작 */
                    // 1. 임대료 및 관리비 납부 정보 삭제
                    // KN_USP_RES_DELETE_RENTALMNGFEEADDON_S00
                    DataTable dsDeleteList = MngPaymentBlo.RemoveRentalMngFeeAddon(txtHfRentCd.Text, txtHfUserSeq.Text, txtHfItemCd.Text, txtHfSvcYear.Text, txtHfSvcMM.Text, Int32.Parse(txtHfItemSeq.Text));

                    if (dsDeleteList != null)
                    {
                        string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                        string strPaymentDt = dsDeleteList.Rows[0]["PaymentDt"].ToString();
                        string strPaymentSeq = dsDeleteList.Rows[0]["PaymentSeq"].ToString();

                        // 2. 출력 테이블에 차감 등록
                        // KN_USP_SET_INSERT_PRINTINFO_S03
                        DataTable dtPrintOut = ReceiptMngBlo.RegistryPrintReciptRentalMngMinusList(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq), txtHfItemCd.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIp);

                        if (dtPrintOut != null)
                        {
                            if (dtPrintOut.Rows.Count > CommValue.NUMBER_VALUE_0)
                            {
                                strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();

                                // 3. 금액 로그 테이블 차감 처리
                                // KN_USP_SET_INSERT_MONEYINFO_M01
                                ReceiptMngBlo.RegistryMoneyMinusInfo(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq), strPrintSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIp);

                                // 4. 결제정보 삭제 처리 (원장, 원장상세, 카드)
                                // KN_USP_SET_DELETE_LEDGERINFO_M00
                                BalanceMngBlo.RemoveLedgerMng(strDebitCreditCd, strPaymentDt, Int32.Parse(strPaymentSeq));
                            }
                        }

                        // 5. 완납취소 처리
                        // KN_USP_MNG_UPDATE_PAYMENTINFO_S01
                        MngPaymentBlo.ModifyPaymentInfo(txtHfRentCd.Text, txtHfItemCd.Text, txtHfSvcYear.Text, txtHfSvcMM.Text, txtHfUserSeq.Text, CommValue.PAYMENT_TYPE_VALUE_NOTPAID);

                        LoadData();

                        StringBuilder sbList = new StringBuilder();
                        sbList.Append("window.open('/Common/RdPopup/RDPopupReciptDetail.aspx?Datum0=" + strPrintSeq + "&Datum1=0&Datum2=&Datum3=&Datum4=', 'RentalNManageFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RentalNManageFee", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    /* 관리비 및 임대료 처리 부분 끝 */
                }
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
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}