using System;
using System.Data;
using System.EnterpriseServices;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Stock.Biz;
using System.Text;
using KN.Manage.Biz;
using KN.Config.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Stock.Warehousing
{
    public partial class StoredMngView : BasePage
    {
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

                        LoadDataList();

                        LoadGoodsData();

                        LoadPaidData();
                        
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
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                    {

                        txtHfWarehouseSeq.Text = Request.Params[Master.PARAM_DATA1].ToString();
                        txtHfWarehouseDetSeq.Text = Request.Params[Master.PARAM_DATA2].ToString();

                        isReturn = CommValue.AUTH_VALUE_TRUE;
                    }
                }
            }

            return isReturn;
        }

        protected void InitControls()
        {
            ltItem.Text = TextNm["ITEM"].ToString();
            ltComp.Text = TextNm["COMPNM"];
            ltInsDept.Text = TextNm["DEPT"];
            ltInsRestAmount.Text = TextNm["NOTRECEIPT"];
            ltInsStoredAmount.Text = TextNm["RECEIPT"];
            ltInsLastReceitDt.Text = TextNm["PAYPERIOD"];
            ltIntReceitDt.Text = TextNm["RECEIPTDT"];
            ltInsRemark.Text = TextNm["REMARK"];

            ltItemPay.Text = TextNm["ITEM"].ToString();
            ltInsDeptPay.Text = TextNm["COMPNM"];
            ltInsRestPrice.Text = TextNm["RESTPRICE"];
            ltInsPayAmt.Text = TextNm["AMOUNT"];
            ltInsPaidDt.Text = TextNm["PAYMENTDT"];
            ltInsPaymentCd.Text = TextNm["PAYMETHOD"];
            ltInsRemarkPay.Text = TextNm["REMARK"];

            lnkbtnStoredConfirm.Text = TextNm["STOREDCONFIRM"];
            lnkbtnPaymentConfirm.Text = TextNm["PAYMENTCONFIRM"];

            imgbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["CONF_REGIST_ITEM"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            imgbtnRegistPay.OnClientClick = "javascript:return fnPayCheckValidate('" + AlertNm["CONF_REGIST_ITEM"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            
            MakeItemDdl();

            txtStoredAmount.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtPayAmt.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];
            LoadExchageDate();
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // KN_USP_STK_SELECT_WAREHOUSEINFO_S05
            dtReturn = WarehouseMngBlo.WatchStoredGoodDetailInfo(txtHfWarehouseSeq.Text, Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                lvStoredGoodList.DataSource = dtReturn;
                lvStoredGoodList.DataBind();
            }   

        }

        private void MakeItemDdl()
        {
            // KN_USP_STK_SELECT_WAREHOUSEINFO_S03
            DataTable dtItemReturn = WarehouseMngBlo.WatchStoredDetailInfo(txtHfWarehouseSeq.Text);

            ddlItem.Items.Clear();

            ddlItem.DataSource = dtItemReturn;

            //foreach (DataRow dr in dtItemReturn.Select())
            //{
            //    ddlItem.Items.Add(new ListItem(dr["ClassNm"].ToString(), dr["WarehouseDetSeq"].ToString()));
            //}
            ddlItem.DataTextField = "ClassNm";
            ddlItem.DataValueField = "WarehouseDetSeq";
            ddlItem.DataBind();

            ltIncComp.Text = dtItemReturn.Rows[0]["CompNm"].ToString();

            ddlItemPay.Items.Clear();

            ddlItemPay.DataSource = dtItemReturn;
            ddlItemPay.DataTextField = "ClassNm";
            ddlItemPay.DataValueField = "WarehouseDetSeq";
            ddlItemPay.DataBind();

        }

        protected void imgbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strInsDt = hfInsDt.Value.Replace("-", "");
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strStoredAmount = txtStoredAmount.Text;
                int intRequestQty = Int32.Parse(txtHfTotalAmount.Text) * -1;

                if (Int32.Parse(strStoredAmount) + Int32.Parse(txtHfRestQty.Text) >= Int32.Parse(txtHfTotalAmount.Text))
                {

                    strStoredAmount = ltRestAmount.Text;

                    DataTable dtReleaseReturn = new DataTable();

                    // KN_USP_STK_SELECT_GOODSORDERINFO_S02
                    dtReleaseReturn = WarehouseMngBlo.WatchReleaseRequestInfo(txtHfOrderSeq.Text, Int32.Parse(txtHfOrderDetSeq.Text));

                    if (dtReleaseReturn.Rows.Count > 0)
                    {
                        if ((!string.IsNullOrEmpty(dtReleaseReturn.Rows[0]["ReleaseSeq"].ToString())) && (!string.IsNullOrEmpty(dtReleaseReturn.Rows[0]["ReleaseDetSeq"].ToString())))
                        {
                            // 출고대기 수정처리
                            // KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00
                            ReleaseInfoBlo.ModifyReleaseRequestItem(txtHfOrderSeq.Text, Int32.Parse(txtHfOrderDetSeq.Text), CommValue.RELEASE_TYPE_VALUE_WAITING, CommValue.APPROVAL_TYPE_VALUE_APPROVAL);

                            // 구매 목록 테이블 상태값 변경
                            // KN_USP_STK_UPDATE_GOODSORDERINFO_M00
                            GoodsOrderInfoBlo.ModifyGoodsOrderItem(txtHfOrderSeq.Text, Int32.Parse(txtHfOrderDetSeq.Text), CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ, CommValue.APPROVAL_TYPE_VALUE_APPROVAL);                           
                        }
                        else
                        {
                            // 출고대기 물량차감
                            // KN_USP_STK_UPDATE_GOODSINFO_M00
                            GoodsOrderInfoBlo.ModifyGoodsInfo(txtHfRentCd.Text, txtHfSvczoneCd.Text, txtHfGroupCd.Text, txtHfMainCd.Text, txtHfClassCd.Text, intRequestQty, Session["CompCd"].ToString(), Session["LangCd"].ToString(), strInsMemIP);
                        }
                    }
                    else
                    {
                        // 출고대기 물량차감
                        // KN_USP_STK_UPDATE_GOODSINFO_M00
                        GoodsOrderInfoBlo.ModifyGoodsInfo(txtHfRentCd.Text, txtHfSvczoneCd.Text, txtHfGroupCd.Text, txtHfMainCd.Text, txtHfClassCd.Text, intRequestQty, Session["CompCd"].ToString(), Session["LangCd"].ToString(), strInsMemIP);
                    }
                   
                    // 출고 신청자 승인 쪽지 발송
                    // 양영석 : 출고 폼메일 제목 정하기
                    MemoWriteUtil.RegistrySendMemo(TextNm["RELEASE"] + " " + TextNm["APPROVAL"], MemoFormLib.MakeReleaseConfirmForm(txtHfReceitMemNo.Text, txtHfOrderSeq.Text), txtHfReceitMemNo.Text, Session["MemNo"].ToString(), strInsMemIP);

                    // 출고 신청자 처리
                    MemoWriteUtil.RegistrySendMemoDetail(txtHfReceitMemNo.Text, Session["MemNo"].ToString());

                }

                // KN_USP_STK_UPDATE_WAREHOUSEINFO_M01
                WarehouseMngBlo.ModifyStoredGoodInfo(txtHfWarehouseSeq.Text, Int32.Parse(txtHfWarehouseDetSeq.Text), Int32.Parse(strStoredAmount), strInsDt);

                // KN_USP_STK_INSERT_WAREHOUSEINFO_M01
                WarehouseMngBlo.RegistryStoredGoodInfo(txtHfWarehouseSeq.Text, Int32.Parse(txtHfWarehouseDetSeq.Text), Int32.Parse(strStoredAmount), strInsDt, Session["MemNo"].ToString(), txtRemark.Text, Session["CompCd"].ToString(),Session["MemNo"].ToString(), strInsMemIP);

                StringBuilder sbList = new StringBuilder();

                sbList.Append(Master.PAGE_VIEW);
                sbList.Append("?");
                sbList.Append(Master.PARAM_DATA1);
                sbList.Append("=");
                sbList.Append(txtHfWarehouseSeq.Text);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA2);
                sbList.Append("=");
                sbList.Append(txtHfWarehouseDetSeq.Text);

                Response.Redirect(sbList.ToString(), CommValue.AUTH_VALUE_FALSE);
                 
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }

        protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGoodsData();
        }

        private void LoadGoodsData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            if (!string.IsNullOrEmpty(ddlItem.SelectedValue))
            {
                // KN_USP_STK_SELECT_WAREHOUSEINFO_S04
                DataTable dtMainReturn = WarehouseMngBlo.WatchStoredGoodInfo(txtHfWarehouseSeq.Text, Int32.Parse(ddlItem.SelectedValue));

                ltDept.Text = dtMainReturn.Rows[0]["DeptNm"].ToString();
                
                
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["RestQty"].ToString()))
                {
                    ltRestAmount.Text = TextLib.MakeVietIntNo(Int32.Parse(dtMainReturn.Rows[0]["RestQty"].ToString()).ToString("###,##0"));

                    //미수령
                    if (ltRestAmount.Text.Equals(""))
                    {
                        ltRestAmount.Text = "-";

                        imgbtnRegist.Visible = false;
                        txtStoredAmount.Visible = false;
                        txtRemark.Visible = false;
                        divCal.Visible = false;
                        lnkbtnStoredConfirm.Visible = Master.isWriteAuthOk; ;
                        divConfirm.Visible = Master.isWriteAuthOk; ;    

                    }
                    else
                    {
                       
                        imgbtnRegist.Visible = Master.isWriteAuthOk;
                        txtStoredAmount.Visible = Master.isWriteAuthOk;
                        txtRemark.Visible = Master.isWriteAuthOk;
                        lnkbtnStoredConfirm.Visible = false;
                        divConfirm.Visible = false;
                        divCal.Visible = Master.isWriteAuthOk;
                        lnkbtnStoredConfirm.Visible = Master.isWriteAuthOk;
                        divConfirm.Visible = Master.isWriteAuthOk;
                    }
                }
                else
                {
                    ltRestAmount.Text = TextLib.MakeVietIntNo(Int32.Parse(dtMainReturn.Rows[0]["RequestQty"].ToString()).ToString("###,##0"));
                    imgbtnRegist.Visible = Master.isWriteAuthOk;
                    txtStoredAmount.Visible = Master.isWriteAuthOk;
                    txtRemark.Visible = Master.isWriteAuthOk;
                    lnkbtnStoredConfirm.Visible = false;
                    divConfirm.Visible = false;
                    divCal.Visible = Master.isWriteAuthOk;
                    
                }

                //수령한 총 량
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["RequestQty"].ToString()))
                {
                    txtHfTotalAmount.Text = dtMainReturn.Rows[0]["RequestQty"].ToString();
                }

                //남은 수량
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["RestQty"].ToString()))
                {
                    txtHfRestQty.Text = (Int32.Parse(dtMainReturn.Rows[0]["RequestQty"].ToString()) - Int32.Parse(dtMainReturn.Rows[0]["RestQty"].ToString())).ToString();
                }
                else
                {
                    txtHfRestQty.Text = "0";
                }

                string strLastReceitDt = dtMainReturn.Rows[0]["DueDt"].ToString();

                StringBuilder sbList = new StringBuilder();

                sbList.Append(strLastReceitDt.Substring(0, 4));
                sbList.Append("-");
                sbList.Append(strLastReceitDt.Substring(4, 2));
                sbList.Append("-");
                sbList.Append(strLastReceitDt.Substring(6, 2));

                ltLastReceitDt.Text = sbList.ToString();

                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["WarehouseDetSeq"].ToString()))
                {
                    txtHfWarehouseDetSeq.Text = dtMainReturn.Rows[0]["WarehouseDetSeq"].ToString();
                }
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["ProcessMemNo"].ToString()))
                {
                    txtHfReceitMemNo.Text = dtMainReturn.Rows[0]["ProcessMemNo"].ToString();
                }
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["OrderSeq"].ToString()))
                {
                    txtHfOrderSeq.Text = dtMainReturn.Rows[0]["OrderSeq"].ToString();
                }
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["OrderDetSeq"].ToString()))
                {
                    txtHfOrderDetSeq.Text = dtMainReturn.Rows[0]["OrderDetSeq"].ToString();
                }
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["RentCd"].ToString()))
                {
                    txtHfRentCd.Text = dtMainReturn.Rows[0]["RentCd"].ToString();
                }
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["SvcZoneCd"].ToString()))
                {
                    txtHfSvczoneCd.Text = dtMainReturn.Rows[0]["SvcZoneCd"].ToString();
                }
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["ClassiGrpCd"].ToString()))
                {
                    txtHfGroupCd.Text = dtMainReturn.Rows[0]["ClassiGrpCd"].ToString();
                }
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["ClassiMainCd"].ToString()))
                {
                    txtHfMainCd.Text = dtMainReturn.Rows[0]["ClassiMainCd"].ToString();
                }
                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["ClassCd"].ToString()))
                {
                    txtHfClassCd.Text = dtMainReturn.Rows[0]["ClassCd"].ToString();
                }

            }            
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvStoredGoodList_LayoutCreated(object sender, EventArgs e)
        {
            ((Literal)lvStoredGoodList.FindControl("ltSeq")).Text = TextNm["SEQ"];
    
            ((Literal)lvStoredGoodList.FindControl("ltDept")).Text = TextNm["DEPT"];
            ((Literal)lvStoredGoodList.FindControl("ltItem")).Text = TextNm["ITEM"];
            ((Literal)lvStoredGoodList.FindControl("ltTotalAmount")).Text = TextNm["TOTALQTY"];
            ((Literal)lvStoredGoodList.FindControl("ltStoredAmount")).Text = TextNm["RECEIPT"];
            ((Literal)lvStoredGoodList.FindControl("ltUnit")).Text = TextNm["UNIT"];
            ((Literal)lvStoredGoodList.FindControl("ltReceitDt")).Text = TextNm["LASTRECEIT"];
            ((Literal)lvStoredGoodList.FindControl("ltLastReceitDt")).Text = TextNm["PAYPERIOD"];
            ((Literal)lvStoredGoodList.FindControl("ltRemark")).Text = TextNm["REMARK"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvStoredGoodList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
            
                    ((Literal)e.Item.FindControl("ltDept")).Text = TextNm["DEPT"];
                    ((Literal)e.Item.FindControl("ltItem")).Text = TextNm["ITEM"];
                    ((Literal)e.Item.FindControl("ltTotalAmount")).Text = TextNm["TOTALQTY"];
                    ((Literal)e.Item.FindControl("ltStoredAmount")).Text = TextNm["RECEIPT"];
                    ((Literal)e.Item.FindControl("ltUnit")).Text = TextNm["UNIT"];
                    ((Literal)e.Item.FindControl("ltLastReceitDt")).Text = TextNm["PAYPERIOD"];
                    ((Literal)e.Item.FindControl("ltReceitDt")).Text = TextNm["LASTRECEIT"];
                    ((Literal)e.Item.FindControl("ltRemark")).Text = TextNm["REMARK"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvStoredGoodList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    Literal ltSeqList = (Literal)iTem.FindControl("ltSeqList");
                    ltSeqList.Text = drView["Seq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    Literal ltItemList = (Literal)iTem.FindControl("ltItemList");
                    ltItemList.Text = drView["ClassNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DeptNm"].ToString()))
                {
                    Literal ltDeptList = (Literal)iTem.FindControl("ltDeptList");
                    ltDeptList.Text = drView["DeptNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RequestQty"].ToString()))
                {
                    Literal ltTotalAmountList = (Literal)iTem.FindControl("ltTotalAmountList");
                    ltTotalAmountList.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["RequestQty"].ToString()).ToString("###,##0"));
                }                

                if (!string.IsNullOrEmpty(drView["ReceiptQty"].ToString()))
                {
                    Literal ltStoredAmount = (Literal)iTem.FindControl("ltStoredAmount");
                    ltStoredAmount.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["ReceiptQty"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["ScaleNm"].ToString()))
                {
                    Literal ltUnitList = (Literal)iTem.FindControl("ltUnitList");
                    ltUnitList.Text = drView["ScaleNm"].ToString();                    
                }

                if (!string.IsNullOrEmpty(drView["DueDt"].ToString()))
                {

                    string strDueDt = drView["DueDt"].ToString();

                    StringBuilder sbList = new StringBuilder();

                    sbList.Append(strDueDt.Substring(0, 4));
                    sbList.Append("-");
                    sbList.Append(strDueDt.Substring(4, 2));
                    sbList.Append("-");
                    sbList.Append(strDueDt.Substring(6, 2));

                    ((Literal)iTem.FindControl("ltLastReceitDtList")).Text = sbList.ToString();
                }

                if (!string.IsNullOrEmpty(drView["ReceiptDate"].ToString()))
                {

                    string strReceitDt = drView["ReceiptDate"].ToString();

                    StringBuilder sbList = new StringBuilder();
                    sbList.Append(strReceitDt.Substring(0, 10));

                    ((Literal)iTem.FindControl("ltReceitDtList")).Text = sbList.ToString();
                }

                if (!string.IsNullOrEmpty(drView["Remark"].ToString()))
                {
                    Literal ltRemarkList = (Literal)iTem.FindControl("ltRemarkList");
                    ltRemarkList.Text = drView["Remark"].ToString();
                }
            }
        }

        protected void ddlItemPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPaidData();
        }

        private void LoadPaidData()
        {

            if (!string.IsNullOrEmpty(ddlItemPay.SelectedValue))
            {
                // KN_USP_STK_SELECT_WAREHOUSEINFO_S07
                DataTable dtMainReturn = WarehouseMngBlo.WatchStoredPayInfo(txtHfWarehouseSeq.Text, Int32.Parse(ddlItemPay.SelectedValue));

                ltDeptPay.Text = dtMainReturn.Rows[0]["DeptNm"].ToString();

                if (!string.IsNullOrEmpty(dtMainReturn.Rows[0]["RestPrice"].ToString()))
                {
                    if (dtMainReturn.Rows[0]["RestPrice"].ToString().Equals("0"))
                    {
                        ltRestPrice.Text = "-";
                        txtHfTotalPrice.Text = dtMainReturn.Rows[0]["TotalPrice"].ToString();
                        txtHfRestPrice.Text = "0";
                        imgbtnRegistPay.Visible = false;
                        txtPayAmt.Visible = false;
                        txtRemarkPay.Visible = false;
                        ddlPaymentCd.Visible = false;
                        divCal1.Visible = false;

                    }
                    else
                    {
                        ltRestPrice.Text = TextLib.MakeVietIntNo(double.Parse(dtMainReturn.Rows[0]["RestPrice"].ToString()).ToString("###,##0"));
                        txtHfTotalPrice.Text = dtMainReturn.Rows[0]["TotalPrice"].ToString();
                        txtHfRestPrice.Text = (double.Parse(dtMainReturn.Rows[0]["TotalPrice"].ToString())-double.Parse(dtMainReturn.Rows[0]["RestPrice"].ToString())).ToString();
                        imgbtnRegistPay.Visible = Master.isWriteAuthOk;
                        txtPayAmt.Visible = Master.isWriteAuthOk;
                        divCal1.Visible = Master.isWriteAuthOk;
                        txtRemarkPay.Visible = Master.isWriteAuthOk;
                        ddlPaymentCd.Visible = Master.isWriteAuthOk;
                    }                    

                }
                else
                {
                    ltRestPrice.Text = TextLib.MakeVietIntNo(double.Parse(dtMainReturn.Rows[0]["TotalPrice"].ToString()).ToString("###,##0"));
                    txtHfRestPrice.Text = "0";
                    txtHfTotalPrice.Text = dtMainReturn.Rows[0]["TotalPrice"].ToString();
                    imgbtnRegistPay.Visible = Master.isWriteAuthOk;
                    txtPayAmt.Visible = Master.isWriteAuthOk;
                    divCal1.Visible = Master.isWriteAuthOk;
                    txtRemarkPay.Visible = Master.isWriteAuthOk;
                    ddlPaymentCd.Visible = Master.isWriteAuthOk;
                }

                string strLastReceitDt = dtMainReturn.Rows[0]["DueDt"].ToString();

                txtHfWarehouseDetSeqPay.Text = dtMainReturn.Rows[0]["WarehouseDetSeq"].ToString();
                txtHfReceitMemNoPay.Text = dtMainReturn.Rows[0]["ProcessMemNo"].ToString();  
                MakePaymentDdl();                

            }
        }

        protected void imgbtnRegistPay_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strInsDt = hfPaidDt.Value.Replace("-", "");
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strPaymentCd = ddlPaymentCd.SelectedValue.ToString();
                string strPayAmt = txtPayAmt.Text;

                string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                string strDirectCd = CommValue.DIRECT_TYPE_VALUE_DIRECT;
                string strItemCd = CommValue.ITEM_TYPE_VALUE_MATERIALFEE;


                if (Int32.Parse(strPayAmt) + Int32.Parse(txtHfRestPrice.Text) >= Int32.Parse(txtHfTotalPrice.Text))
                {                   
                    strPayAmt = ltRestPrice.Text;                           
                }

                // KN_USP_STK_INSERT_WAREHOUSEINFO_M02
                WarehouseMngBlo.RegistryStoredPayInfo(txtHfWarehouseSeq.Text, Int32.Parse(txtHfWarehouseDetSeqPay.Text), double.Parse(strPayAmt), strInsDt, strPaymentCd, txtRemarkPay.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                /* 양영석 : 부가세율 안보임, 추후 확인필요할 듯.. */
                double dblItemTotEmAmt = 0d;
                double dblItemTotViAmt = 0d;
                double dblDongToDollar = 0d;
                double dblUniPrime = 0d;
                double dblVatRatio = 0d;

                if (!string.IsNullOrEmpty(hfRealBaseRate.Text) && !string.IsNullOrEmpty(strPayAmt))
                {
                    dblDongToDollar = double.Parse(hfRealBaseRate.Text);
                    dblItemTotViAmt = double.Parse(strPayAmt);

                    if (dblDongToDollar > 0d)
                    {
                        dblItemTotEmAmt = dblItemTotViAmt / dblDongToDollar;
                        dblUniPrime = dblUniPrime * (100 - dblVatRatio) / 100;
                    }
                }

                // KN_USP_SET_INSERT_LEDGERINFO_S00
                DataTable dtAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strInsDt, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text, strDirectCd, strItemCd,
                                                                     CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, string.Empty, string.Empty,
                                                                     dblDongToDollar, dblItemTotEmAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio,
                                                                     Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                if (dtAccnt != null)
                {
                    if (dtAccnt.Rows.Count > 0)
                    {
                        int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                        int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                        DataTable dtLedgerDet = new DataTable();

                        // 5. 상세원장등록
                        // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                        dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strInsDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, txtHfRentCd.Text,
                                                                          strDirectCd, strItemCd, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                          CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblItemTotViAmt, dblItemTotViAmt,
                                                                          CommValue.NUMBER_VALUE_0, strInsDt.Substring(0, 4), strInsDt.Substring(4, 2), string.Empty, dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                          Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                    }
                }

                StringBuilder sbList = new StringBuilder();

                sbList.Append(Master.PAGE_VIEW);
                sbList.Append("?");
                sbList.Append(Master.PARAM_DATA1);
                sbList.Append("=");
                sbList.Append(txtHfWarehouseSeq.Text);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA2);
                sbList.Append("=");
                sbList.Append(txtHfWarehouseDetSeq.Text);

                Response.Redirect(sbList.ToString(), CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }

        private void MakePaymentDdl()
        {
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlPaymentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);           
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvStoredPayList_LayoutCreated(object sender, EventArgs e)
        {
            ((Literal)lvStoredPayList.FindControl("ltSeq")).Text = TextNm["SEQ"];

            ((Literal)lvStoredPayList.FindControl("ltDept")).Text = TextNm["DEPT"];
            ((Literal)lvStoredPayList.FindControl("ltItem")).Text = TextNm["ITEM"];
            ((Literal)lvStoredPayList.FindControl("ltTotalPrice")).Text = TextNm["TOTALAMT"];
            ((Literal)lvStoredPayList.FindControl("ltPayedMount")).Text = TextNm["AMOUNT"];
            ((Literal)lvStoredPayList.FindControl("ltPayDt")).Text = TextNm["PAYMENTDT"];
            ((Literal)lvStoredPayList.FindControl("ltPaymentCd")).Text = TextNm["PAYMETHOD"];
            ((Literal)lvStoredPayList.FindControl("ltRemark")).Text = TextNm["REMARK"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvStoredPayList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];

                    ((Literal)e.Item.FindControl("ltDept")).Text = TextNm["DEPT"];
                    ((Literal)e.Item.FindControl("ltItem")).Text = TextNm["ITEM"];
                    ((Literal)e.Item.FindControl("ltTotalPrice")).Text = TextNm["TOTALAMT"];
                    ((Literal)e.Item.FindControl("ltPayedMount")).Text = TextNm["AMOUNT"];
                    ((Literal)e.Item.FindControl("ltPayDt")).Text = TextNm["PAYMENTDT"];
                    ((Literal)e.Item.FindControl("ltPaymentCd")).Text = TextNm["PAYMETHOD"];
                    ((Literal)e.Item.FindControl("ltRemark")).Text = TextNm["REMARK"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvStoredPayList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    Literal ltSeqList = (Literal)iTem.FindControl("ltSeqList");
                    ltSeqList.Text = drView["Seq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    Literal ltItemList = (Literal)iTem.FindControl("ltItemList");
                    ltItemList.Text = drView["ClassNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DeptNm"].ToString()))
                {
                    Literal ltDeptList = (Literal)iTem.FindControl("ltDeptList");
                    ltDeptList.Text = drView["DeptNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TotalPrice"].ToString()))
                {
                    Literal ltTotalPriceList = (Literal)iTem.FindControl("ltTotalPriceList");
                    ltTotalPriceList.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["TotalPrice"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["PayedAmt"].ToString()))
                {
                    Literal ltPayedMountList = (Literal)iTem.FindControl("ltPayedMountList");
                    ltPayedMountList.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["PayedAmt"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
                {
                    Literal ltPaymentCdList = (Literal)iTem.FindControl("ltPaymentCdList");
                    ltPaymentCdList.Text = drView["PaymentNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PayedDt"].ToString()))
                {

                    string strPaidDt = drView["PayedDt"].ToString();

                    StringBuilder sbList = new StringBuilder();

                    sbList.Append(strPaidDt.Substring(0, 4));
                    sbList.Append("-");
                    sbList.Append(strPaidDt.Substring(4, 2));
                    sbList.Append("-");
                    sbList.Append(strPaidDt.Substring(6, 2));

                    ((Literal)iTem.FindControl("ltPayDtList")).Text = sbList.ToString();
                }
                
                if (!string.IsNullOrEmpty(drView["Remark"].ToString()))
                {
                    Literal ltRemarkList = (Literal)iTem.FindControl("ltRemarkList");
                    ltRemarkList.Text = drView["Remark"].ToString();
                }
            }
        }

        protected void LoadDataList()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_STK_SELECT_WAREHOUSEINFO_S06
            dtReturn = WarehouseMngBlo.WatchStoredPayDetailInfo(txtHfWarehouseSeq.Text, Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                lvStoredPayList.DataSource = dtReturn;
                lvStoredPayList.DataBind();
            }

        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            DataTable dtReturn = new DataTable();

            // 오피스 가장 최근의 환율을 조회함.
            // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S05
            dtReturn = ExchangeMngBlo.WatchOfficeExchangeRateLastInfo();

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0")) + "&nbsp;" + TextNm["DONG"].ToString();
                        hfRealBaseRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                    }
                }
                else
                {
                    ltRealBaseRate.Text = "-";
                }
            }
        }
    }
}
