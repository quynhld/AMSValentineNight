using System;
using System.Data;
using System.Text;
using System.EnterpriseServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Stock.Biz;

namespace KN.Web.Stock.Order
{
    [Transaction(TransactionOption.Required)]
    public partial class GoodsOrderView : BasePage
    {
        bool isStatusCd = CommValue.AUTH_VALUE_FALSE;
        bool isDelYn = CommValue.AUTH_VALUE_TRUE;
        bool isAccept = CommValue.AUTH_VALUE_FALSE;
        bool isDeny = CommValue.AUTH_VALUE_FALSE;
        bool isPending = CommValue.AUTH_VALUE_FALSE;
        bool isPrevPending = CommValue.AUTH_VALUE_FALSE;

        string strStatusNm = string.Empty;
        int intRowCnt = 0;

        DataTable dtBasicReturn = new DataTable();
        DataTable dtChargerReturn = new DataTable();

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
                        LoadData();

                        InitControls();
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                        sbWarning.Append("');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
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
                    txtHfOrderSeq.Text = Request.Params[Master.PARAM_DATA1].ToString();

                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturn;
        }

        protected void InitControls()
        {
            ltDpt.Text = TextNm["DEPT"];
            ltProcessMemNo.Text = TextNm["MEM"] + " " + TextNm["NAME"];
            ltProcessDt.Text = TextNm["WAREHOUSING"] + " " + TextNm["DATE"];
            ltApprovalYn.Text = TextNm["STATUS"];
            ltRemark.Text = TextNm["REMARK"];

            // 구매요청 이외에는 삭제 및 수정 불가
            lnkbtnOrder.Text = TextNm["ORDER"];
            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnUpdate.Text = TextNm["MODIFY"];
            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnCancel.Text = TextNm["ORDER"] + " " + TextNm["CANCEL"];

            if (!txtHfProgressCd.Text.Equals(CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ))
            {
                lnkbtnOrder.Visible = !(Master.isModDelAuthOk);
                lnkbtnModify.Visible = Master.isModDelAuthOk;
                lnkbtnUpdate.Visible = !(Master.isModDelAuthOk);
                lnkbtnCancel.Visible = Master.isModDelAuthOk;
                lnkbtnDelete.Visible = Master.isModDelAuthOk;

                divOrder.Visible = !(Master.isModDelAuthOk);
                divModi.Visible = Master.isModDelAuthOk;
                divUpdate.Visible = !(Master.isModDelAuthOk);
                divCancel.Visible = Master.isModDelAuthOk;
                divDel.Visible = Master.isModDelAuthOk;

                if (!Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_ADMIN) &&
                    !Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
                {
                    if (Master.isModDelAuthOk.Equals(CommValue.AUTH_VALUE_TRUE))
                    {
                        lnkbtnOrder.Visible = !isDelYn;
                        lnkbtnDelete.Visible = isDelYn;
                        lnkbtnCancel.Visible = isDelYn;
                        lnkbtnModify.Visible = isDelYn;
                        lnkbtnUpdate.Visible = !isDelYn;

                        divOrder.Visible = !isDelYn;
                        divDel.Visible = isDelYn;
                        divCancel.Visible = isDelYn;
                        divModi.Visible = isDelYn;
                        divUpdate.Visible = !isDelYn;
                    }
                }
            }
            else
            {
                lnkbtnOrder.Visible = CommValue.AUTH_VALUE_TRUE;
                lnkbtnModify.Visible = CommValue.AUTH_VALUE_FALSE;
                lnkbtnUpdate.Visible = CommValue.AUTH_VALUE_TRUE;
                lnkbtnCancel.Visible = CommValue.AUTH_VALUE_FALSE;
                lnkbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;

                divOrder.Visible = CommValue.AUTH_VALUE_TRUE;
                divModi.Visible = CommValue.AUTH_VALUE_FALSE;
                divUpdate.Visible = CommValue.AUTH_VALUE_TRUE;
                divCancel.Visible = CommValue.AUTH_VALUE_FALSE;
                divDel.Visible = CommValue.AUTH_VALUE_FALSE;
            }

            //lnkbtnModify.Visible = isDelYn;
            //lnkbtnDelete.Visible = isDelYn;
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_STK_SELECT_GOODSORDERINFO_S01
            DataSet dsReturn = GoodsOrderInfoBlo.WatchGoodsOrderDetailInfo(txtHfOrderSeq.Text, Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                if (dsReturn.Tables.Count > 0)
                {
                    // 기본정보 조회
                    dtBasicReturn = dsReturn.Tables[0];

                    txtHfRequestCnt.Text = dtBasicReturn.Rows.Count.ToString();

                    // 승인 담당자 정보 조회
                    dtChargerReturn = dsReturn.Tables[1];

                    if (dtBasicReturn.Rows.Count > 0)
                    {
                        txtHfChargerCnt.Text = (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count).ToString();
                    }
                    else
                    {
                        txtHfChargerCnt.Text = CommValue.NUMBER_VALUE_ZERO;
                    }

                    // 발주정보 조회
                    lvRequestList.DataSource = dtBasicReturn;
                    lvRequestList.DataBind();

                    // 기본정보 조회
                    if (dtBasicReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        ltInsDpt.Text = dtBasicReturn.Rows[0]["ProcessDeptNm"].ToString();
                        txtHfDptCd.Text = dtBasicReturn.Rows[0]["ProcessDeptCd"].ToString();
                        ltInsProcessMemNm.Text = dtBasicReturn.Rows[0]["ProcessMemNm"].ToString();

                        txtHfProcessMemNo.Text = dtBasicReturn.Rows[0]["ProcessMemNo"].ToString();

                        if (!string.IsNullOrEmpty(dtBasicReturn.Rows[0]["ProcessDt"].ToString()))
                        {
                            ltInsProcessDt.Text = TextLib.MakeDateEightDigit(dtBasicReturn.Rows[0]["ProcessDt"].ToString());
                        }

                        if (isStatusCd)
                        {
                            ltInsApproval.Text = strStatusNm;
                            txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_PENDING;
                        }
                        else
                        {
                            DataTable dtStatus = CommCdInfo.SelectSubCdWithTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_APPROVAL);

                            if (isPending)
                            {
                                ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_PENDING)]["CodeNm"].ToString();
                                txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_PENDING;
                            }
                            else
                            {
                                if (isAccept && isDeny)
                                {
                                    ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_PARTIALAPPROVAL)]["CodeNm"].ToString();
                                    txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_PARTIALAPPROVAL;
                                }
                                else if (isAccept && !isDeny)
                                {
                                    ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_APPROVAL)]["CodeNm"].ToString();
                                    txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_APPROVAL;
                                }
                                else if (!isAccept && isDeny)
                                {
                                    ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_REJECTED)]["CodeNm"].ToString();
                                    txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_REJECTED;
                                }
                            }
                        }

                        ltInsRemark.Text = dtBasicReturn.Rows[0]["Remark"].ToString();
                    }

                    lvChargerList.DataSource = dtChargerReturn;
                    lvChargerList.DataBind();
                }
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRequestList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvRequestList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvRequestList.FindControl("ltItem")).Text = TextNm["ITEM"] + " " + TextNm["NAME"];
            ((Literal)lvRequestList.FindControl("ltCompNm")).Text = TextNm["COMPNM"];
            ((Literal)lvRequestList.FindControl("ltQty")).Text = TextNm["DEMANDEDQTY"];
            ((Literal)lvRequestList.FindControl("ltSellingPrice")).Text = TextNm["SELLINGCOST"];
            ((Literal)lvRequestList.FindControl("ltTotPrice")).Text = TextNm["TOTAL"];
            ((Literal)lvRequestList.FindControl("ltProgressCd")).Text = TextNm["PURREQSTATUS"];
            ((Literal)lvRequestList.FindControl("ltStatusCd")).Text = TextNm["STATUS"];
        }

        /// <summary>
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRequestList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["OrderDetSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSeq")).Text = drView["OrderDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()) &&
                    !string.IsNullOrEmpty(drView["RentCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiGrpCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiMainCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItem")).Text = drView["ClassNm"].ToString() + " (" + drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + "-" + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString() + ")";
                    ((TextBox)iTem.FindControl("txtHfRentCd")).Text = drView["RentCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfSvcZoneCd")).Text = drView["SvcZoneCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassiGrpCd")).Text = drView["ClassiGrpCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassiMainCd")).Text = drView["ClassiMainCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassCd")).Text = drView["ClassCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsCompNm")).Text = drView["CompNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RequestQty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = drView["RequestQty"].ToString();
                }
                else
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["UnitSellingPrice"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSellingPrice")).Text = TextLib.MakeVietIntNo(double.Parse(drView["UnitSellingPrice"].ToString()).ToString("###,##0"));
                    ((Literal)iTem.FindControl("ltInsTotPrice")).Text = TextLib.MakeVietIntNo((double.Parse(drView["RequestQty"].ToString()) * double.Parse(drView["UnitSellingPrice"].ToString())).ToString("###,##0"));
                }
                else
                {
                    ((Literal)iTem.FindControl("ltInsSellingPrice")).Text = CommValue.NUMBER_VALUE_ZERO;
                    ((Literal)iTem.FindControl("ltInsTotPrice")).Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["ProgressNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsProgressCd")).Text = drView["ProgressNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StateNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsStatusCd")).Text = drView["StateNm"].ToString();
                    ((TextBox)iTem.FindControl("txtInsStatusCd")).Text = drView["StateCd"].ToString();

                    if (!isStatusCd && drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_PENDING))
                    {
                        isStatusCd = CommValue.AUTH_VALUE_TRUE;
                        strStatusNm = drView["StateNm"].ToString();
                    }

                    if (drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_PENDING))
                    {
                        isPending = CommValue.AUTH_VALUE_TRUE;
                    }

                    if (drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_APPROVAL))
                    {
                        isAccept = CommValue.AUTH_VALUE_TRUE;
                    }

                    if (drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_REJECTED))
                    {
                        isDeny = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                if (!string.IsNullOrEmpty(drView["ProgressCd"].ToString()))
                {
                    if (isDelYn && !drView["ProgressCd"].ToString().Equals(CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ))
                    {
                        isDelYn = CommValue.AUTH_VALUE_FALSE;
                        txtHfProgressCd.Text = drView["ProgressCd"].ToString();
                    }
                }

                //HtmlGenericControl divOrder = (HtmlGenericControl)iTem.FindControl("divOrder");
                //LinkButton lnkOrder = (LinkButton)iTem.FindControl("lnkbtnOrder");
                //lnkOrder.Text = TextNm["ORDER"];
                CheckBox chkRequestSeq = (CheckBox)iTem.FindControl("chkRequestSeq");

                if (drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_APPROVAL) &&
                    drView["ProgressCd"].ToString().Equals(CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ))
                {
                    if (Master.isModDelAuthOk.Equals(CommValue.AUTH_VALUE_TRUE) ||
                        txtHfProcessMemNo.Text.Equals(Session["MemNo"].ToString()))
                    {
                        chkRequestSeq.Enabled = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        chkRequestSeq.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                }
                else
                {
                    chkRequestSeq.Enabled = CommValue.AUTH_VALUE_FALSE;
                }
            }
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllCheck = ((CheckBox)lvRequestList.FindControl("chkAll")).Checked;

            try
            {
                if (!CheckAll(isAllCheck))
                {
                    ((CheckBox)lvRequestList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;
                }
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
        private bool CheckAll(bool isAllCheck)
        {
            int intCnt = 0;
            bool isReturn = CommValue.AUTH_VALUE_TRUE;

            for (int intTmpI = 0; intTmpI < lvRequestList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvRequestList.Items[intTmpI].FindControl("chkRequestSeq")).Enabled)
                {
                    ((CheckBox)lvRequestList.Items[intTmpI].FindControl("chkRequestSeq")).Checked = isAllCheck;
                    intCnt++;
                }
            }

            if (intCnt == 0)
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturn;
        }

        /// <summary>
        /// 리스트 각 행별 체크 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkRequestSeq_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int intCheckCnt = 0;
                int intEnableCnt = 0;

                for (int intTmpI = 0; intTmpI < lvRequestList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvRequestList.Items[intTmpI].FindControl("chkRequestSeq")).Enabled)
                    {
                        intEnableCnt++;

                        if (((CheckBox)lvRequestList.Items[intTmpI].FindControl("chkRequestSeq")).Checked)
                        {
                            intCheckCnt++;
                        }
                    }

                }

                if (intCheckCnt == intEnableCnt)
                {
                    ((CheckBox)lvRequestList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ((CheckBox)lvRequestList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargerList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvChargerList.FindControl("ltCharger")).Text = TextNm["ACCEPTANCE"];
            ((Literal)lvChargerList.FindControl("ltStatus")).Text = TextNm["STATUS"];
            ((Literal)lvChargerList.FindControl("ltRemark")).Text = TextNm["REMARK"];
        }

        protected void lvChargerList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                HtmlTableCell tdSeq = (HtmlTableCell)iTem.FindControl("tdSeq");
                Literal ltChargerSeq = (Literal)iTem.FindControl("ltChargeSeq");
                TextBox txtHfOrderDetSeq = (TextBox)iTem.FindControl("txtHfOrderDetSeq");

                if (dtBasicReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    if (intRowCnt % (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count) == 0)
                    {
                        tdSeq.RowSpan = dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count;
                        ltChargerSeq.Text = (intRowCnt / (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count) + 1).ToString();
                        txtHfOrderDetSeq.Text = (intRowCnt / (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count) + 1).ToString();
                    }
                    else
                    {
                        txtHfOrderDetSeq.Text = (intRowCnt / (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count) + 1).ToString();
                        tdSeq.Visible = CommValue.AUTH_VALUE_FALSE;
                    }
                }

                Literal ltChargerNm = (Literal)iTem.FindControl("ltChargerNm");

                if (!string.IsNullOrEmpty(drView["ChargeMemNm"].ToString()))
                {
                    ltChargerNm.Text = drView["ChargeMemNm"].ToString();
                }

                TextBox txtHfChargerSeq = (TextBox)iTem.FindControl("txtHfChargerSeq");

                if (!string.IsNullOrEmpty(drView["ChargeSeq"].ToString()))
                {
                    txtHfChargerSeq.Text = drView["ChargeSeq"].ToString();
                }

                TextBox txtHfChargeMemNo = (TextBox)iTem.FindControl("txtHfChargeMemNo");
                if (!string.IsNullOrEmpty(drView["ChargeMemNo"].ToString()))
                {
                    txtHfChargeMemNo.Text = drView["ChargeMemNo"].ToString();
                }

                TextBox txtRemark = (TextBox)iTem.FindControl("txtRemark");
                if (!string.IsNullOrEmpty(drView["Remark"].ToString()))
                {
                    txtRemark.Text = drView["Remark"].ToString();
                }

                LinkButton lnkbtnApproval = (LinkButton)iTem.FindControl("lnkbtnApproval");
                lnkbtnApproval.Text = TextNm["APPROVAL"];
                lnkbtnApproval.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_PRCEED_WORK"] + "');";

                LinkButton lnkbtnRejected = (LinkButton)iTem.FindControl("lnkbtnRejected");
                lnkbtnRejected.Text = TextNm["REJECTED"];
                lnkbtnRejected.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_PRCEED_WORK"] + "');";

                HtmlGenericControl divUpdate = (HtmlGenericControl)iTem.FindControl("divUpdate");
                HtmlGenericControl divReject = (HtmlGenericControl)iTem.FindControl("divReject");

                Literal ltConclusion = (Literal)iTem.FindControl("ltConclusion");

                if (drView["ApprovalYn"].ToString().Equals(CommValue.CHOICE_VALUE_NOTCONFIRM))
                {
                    if ((Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER)) ||
                        (Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER)) ||
                        (Session["MemNo"].ToString().Equals(txtHfChargeMemNo.Text)))
                    {
                        // 접근권한이 있을 경우라도 선임이 미결이면 무조건 미결처리
                        if (Int32.Parse(txtHfChargerSeq.Text) > 1 && isPrevPending)
                        {
                            lnkbtnApproval.Visible = CommValue.AUTH_VALUE_FALSE;
                            lnkbtnRejected.Visible = CommValue.AUTH_VALUE_FALSE;
                            divReject.Visible = CommValue.AUTH_VALUE_FALSE;
                            divUpdate.Visible = CommValue.AUTH_VALUE_FALSE;

                            txtRemark.ReadOnly = CommValue.AUTH_VALUE_TRUE;

                            ltConclusion.Text = TextNm["PENDING"];
                        }
                        else
                        {
                            ltConclusion.Visible = CommValue.AUTH_VALUE_FALSE;
                        }
                    }
                    else
                    {
                        lnkbtnApproval.Visible = CommValue.AUTH_VALUE_FALSE;
                        lnkbtnRejected.Visible = CommValue.AUTH_VALUE_FALSE;
                        divReject.Visible = CommValue.AUTH_VALUE_FALSE;
                        divUpdate.Visible = CommValue.AUTH_VALUE_FALSE;

                        txtRemark.ReadOnly = CommValue.AUTH_VALUE_TRUE;

                        ltConclusion.Text = TextNm["PENDING"];
                    }

                    isPrevPending = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    // 승인 상태가 미결이 아닐경우 현재 결정상태를 보여줌.
                    if (drView["ApprovalYn"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        ltConclusion.Text = TextNm["APPROVAL"];
                    }
                    else if (drView["ApprovalYn"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
                    {
                        ltConclusion.Text = TextNm["REJECTED"];
                    }

                    txtRemark.ReadOnly = CommValue.AUTH_VALUE_TRUE;
                    lnkbtnApproval.Visible = CommValue.AUTH_VALUE_FALSE;
                    lnkbtnRejected.Visible = CommValue.AUTH_VALUE_FALSE;
                    divReject.Visible = CommValue.AUTH_VALUE_FALSE;
                    divUpdate.Visible = CommValue.AUTH_VALUE_FALSE;

                    isPrevPending = CommValue.AUTH_VALUE_FALSE;
                }

                intRowCnt++;
            }
        }

        protected void lvChargerList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 반려 등록 및 상태코드 변경
                string strOrderSeq = string.Empty;
                int intOrderDetSeq = 0;
                int intChargeSeq = 0;
                string strRemark = string.Empty;
                string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                strOrderSeq = txtHfOrderSeq.Text;

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfOrderDetSeq")).Text))
                {
                    intOrderDetSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfOrderDetSeq")).Text);
                }

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfChargerSeq")).Text))
                {
                    intChargeSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfChargerSeq")).Text);
                }

                strRemark = ((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtRemark")).Text;

                GoodsOrderInfoBlo.ModifyGooodsOrderDeny(strOrderSeq, intOrderDetSeq, intChargeSeq, strRemark, CommValue.PURCHASE_TYPE_VALUE_CANCELPURCHASE, CommValue.APPROVAL_TYPE_VALUE_REJECTED);

                // 대상자에게 반려 쪽지 발송
                // 양영석 : 발주 폼메일 제목 정하기
                MemoWriteUtil.RegistrySendMemo(TextNm["ORDER"] + " " + TextNm["REJECTED"], MemoFormLib.MakePurchaseDenyForm(ltInsProcessMemNm.Text, strOrderSeq), txtHfProcessMemNo.Text, Session["MemNo"].ToString(), strIP);
                // 발주 신청자 처리
                MemoWriteUtil.RegistrySendMemoDetail(txtHfProcessMemNo.Text, Session["MemNo"].ToString());

                // 양영석 재고 히스토리 처리

                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + txtHfOrderSeq.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargerList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 승인 등록
                string strOrderSeq = string.Empty;

                int intOrderDetSeq = 0;
                int intChargeSeq = 0;
                int intChargeCnt = 0;

                string strRemark = string.Empty;
                string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                strOrderSeq = txtHfOrderSeq.Text;

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfOrderDetSeq")).Text))
                {
                    intOrderDetSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfOrderDetSeq")).Text);
                }

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfChargerSeq")).Text))
                {
                    intChargeSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfChargerSeq")).Text);
                }

                strRemark = ((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtRemark")).Text;

                // 구매담당자 승인 처리
                // KN_USP_STK_UPDATE_GOODSORDERCONFIRM_M00
                GoodsOrderInfoBlo.ModifyGoodsOrderConfirm(strOrderSeq, intOrderDetSeq, intChargeSeq, strRemark);

                if (!string.IsNullOrEmpty(txtHfChargerCnt.Text))
                {
                    intChargeCnt = Int32.Parse(txtHfChargerCnt.Text);
                }

                // 다음 단계 체크 후 있으면 쪽지 발송
                if (intChargeCnt > intChargeSeq)
                {
                    string strChargerNo = ((TextBox)lvChargerList.Items[intChargeSeq].FindControl("txtHfChargeMemNo")).Text;
                    string strChargerNm = ((Literal)lvChargerList.Items[intChargeSeq].FindControl("ltChargerNm")).Text;

                    // 구매 담당자 쪽지 발송
                    // 양영석 : 구매 폼메일 제목 정하기
                    MemoWriteUtil.RegistrySendMemo(TextNm["ORDER"] + " " + TextNm["REQUEST"], MemoFormLib.MakeOrderChargeForm(strChargerNm, strOrderSeq), strChargerNo, Session["MemNo"].ToString(), strIP);
                    // 구매 다음 담당자 처리
                    MemoWriteUtil.RegistrySendMemoDetail(strChargerNo, Session["MemNo"].ToString());

                    // 양영석 재고 히스토리 처리
                }
                // 다음 단계 체크 후 없으면 상태값 변경후 구매쪽지 발송
                else
                {
                    // 구매 목록 테이블 상태값 변경
                    // KN_USP_STK_UPDATE_GOODSORDERINFO_M00
                    GoodsOrderInfoBlo.ModifyGoodsOrderItem(strOrderSeq, intOrderDetSeq, CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ, CommValue.APPROVAL_TYPE_VALUE_APPROVAL);

                    // 구매 담당자(Step1 담당자)에게 구매 요청 메세지 발송
                    // KN_USP_STK_SELECT_GOODSORDERCHARGERINFO_S00
                    DataTable dtPurchaseCharger = GoodsOrderInfoBlo.SpreadGoodsOrderChargeInfo(ltInsProcessDt.Text.Replace("-", ""));

                    if (dtPurchaseCharger != null)
                    {
                        if (dtPurchaseCharger.Rows.Count > 0)
                        {
                            // 구매담당자에게 쪽지 발송
                            MemoWriteUtil.RegistrySendMemo(TextNm["PURCHASE"] + " " + TextNm["APPROVAL"], MemoFormLib.MakePurchaseRequestForm(dtPurchaseCharger.Rows[0]["MemNm"].ToString(), strOrderSeq), dtPurchaseCharger.Rows[0]["ChargeMemNo"].ToString(), Session["MemNo"].ToString(), strIP);
                            MemoWriteUtil.RegistrySendMemoDetail(dtPurchaseCharger.Rows[0]["ChargeMemNo"].ToString(), Session["MemNo"].ToString());
                        }
                    }

                    // 양영석 재고 히스토리 처리
                }

                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + txtHfOrderSeq.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 양영석 : 
                // 원기능 : 
                // 발주서 프린트용 팝업호출
                // 진행코드는 입고대기로 변경할 것
                // 팝업에서 프린트 버튼 클릭시프린트 후 입고테이블로 데이터 복사진행
                int intRowCnt = 0;
                string strWareHouseSeq = string.Empty;
                string strCompNo = string.Empty;
                string strIP = Request.ServerVariables["REMOTE_ADDR"];

                for (int intTmpI = 0; intTmpI < lvRequestList.Items.Count; intTmpI++)
                {
                    CheckBox chkRequestSeq = (CheckBox)lvRequestList.Items[intTmpI].FindControl("chkRequestSeq");

                    if (chkRequestSeq.Enabled)
                    {
                        if (chkRequestSeq.Checked)
                        {
                            string strIntSeq = ((Literal)(lvRequestList.Items[intTmpI].FindControl("ltInsSeq"))).Text;
                            
                            if (!string.IsNullOrEmpty(strIntSeq))
                            {
                                // KN_USP_STK_INSERT_WAREHOUSEMNGINFO_S00
                                DataTable dtWareSeq = WarehouseMngBlo.RegistryWareHouseMngInfo(strWareHouseSeq, txtHfOrderSeq.Text, Int32.Parse(strIntSeq), strCompNo, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                                strWareHouseSeq = dtWareSeq.Rows[0]["WarehouseSeq"].ToString();
                                strCompNo = dtWareSeq.Rows[0]["CompNo"].ToString();

                                intRowCnt++;
                            }
                        }
                    }                    
                }

                if (intRowCnt == 0)
                {
                    StringBuilder sbWarning = new StringBuilder();

                    sbWarning.Append("alert('");
                    sbWarning.Append(AlertNm["INFO_HAS_NO_SELECTED_ITEM"]);
                    sbWarning.Append("');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelectedItem", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                }
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

                // 처리대상
                // 1. 미출고사항                                ProgressCd != 0003
                // 2. 구매테이블에 정보가 넘어가지 않은 사항    ProgressCd != 0002
                // 3. 취소되지 않은 사항                        ProgressCd != 0004 / 0005
                // 4. 반려되지 않은 사항                        StateCd != 0003 
                // 양영석 수정의 범위가 정해지면 처리하도록 할 것.
                // 우선은 기본정보 수정 수준으로 처리
                //Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfOrderSeq.Text , CommValue.AUTH_VALUE_FALSE);
                Response.Redirect(Master.PAGE_TRANSFER + "?" + Master.PARAM_DATA1 + "=" + txtHfOrderSeq.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 처리대상
                // 모든 대상 가능
                Response.Redirect(Master.PAGE_TRANSFER + "?" + Master.PARAM_DATA1 + "=" + txtHfOrderSeq.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 처리대상
                // 1. 미출고사항                                ProgressCd != 0003
                // 2. 구매테이블에 정보가 넘어가지 않은 사항    ProgressCd != 0002
                // 3. 취소되지 않은 사항                        ProgressCd != 0004 / 0005
                // 4. 반려되지 않은 사항                        StateCd != 0003
                for (int intTmpI = 0; intTmpI < lvRequestList.Items.Count; intTmpI++)
                {
                    // 발주신청자료 반려 후 취소 처리
                    // KN_USP_STK_UPDATE_GOODSORDERDENY_M00
                    // KN_USP_STK_UPDATE_GOODSORDERINFO_M00
                    GoodsOrderInfoBlo.ModifyGooodsOrderDeny(txtHfOrderSeq.Text, intTmpI + 1, 1, string.Empty, CommValue.PURCHASE_TYPE_VALUE_CANCELPURCHASE, CommValue.APPROVAL_TYPE_VALUE_REJECTED);
                }

                // 양영석 재고 히스토리 처리

                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + txtHfOrderSeq.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 처리대상
                // 1. 미출고사항                                ProgressCd != 0003
                // 2. 구매테이블에 정보가 넘어가지 않은 사항    ProgressCd != 0002
                // 3. 취소되지 않은 사항                        ProgressCd != 0004 / 0005
                // 4. 반려되지 않은 사항                        StateCd != 0003 

                // 발주신청자료 삭제
                // KN_USP_STK_DELETE_GOODSORDERINFO_M00
                GoodsOrderInfoBlo.RemoveGoodsOrderMng(txtHfOrderSeq.Text);

                // 양영석 재고 히스토리 처리

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}
