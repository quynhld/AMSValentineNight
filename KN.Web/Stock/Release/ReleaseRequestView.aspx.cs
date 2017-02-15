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

namespace KN.Web.Stock.Release
{
    [Transaction(TransactionOption.Required)]
    public partial class ReleaseRequestView : BasePage
    {
        bool isStatusCd = CommValue.AUTH_VALUE_FALSE;
        bool isDelYn = CommValue.AUTH_VALUE_TRUE;
        bool isAccept = CommValue.AUTH_VALUE_FALSE;
        bool isDeny = CommValue.AUTH_VALUE_FALSE;
        bool isPending = CommValue.AUTH_VALUE_FALSE;
        bool isPrevPending = CommValue.AUTH_VALUE_FALSE;
        bool isReleased = CommValue.AUTH_VALUE_FALSE;
        bool isReqOrder = CommValue.AUTH_VALUE_FALSE;

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
                    txtHfReleaseSeq.Text = Request.Params[Master.PARAM_DATA1].ToString();

                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturn;
        }

        protected void InitControls()
        {
            ltDpt.Text = TextNm["DEPT"];
            ltProcessMemNo.Text = TextNm["MEM"] + " " + TextNm["NAME"];
            ltProcessDt.Text = TextNm["RELEASED"] + " " + TextNm["DATE"];
            ltApprovalYn.Text = TextNm["STATUS"];
            ltRemark.Text = TextNm["REMARK"];
            ltFmsFee.Text = TextNm["FMS"] + " " + TextNm["CHARGE"];
            ltFmsRemark.Text = TextNm["FMS"] + " " + TextNm["REMARK"];

            // 출고대기 이외에는 삭제 및 수정 불가
            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnUpdate.Text = TextNm["MODIFY"];
            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnCancel.Text = TextNm["RELEASED"] + " " + TextNm["CANCEL"];

            //if (!txtHfProgressCd.Text.Equals(CommValue.APPROVAL_TYPE_VALUE_APPROVAL) && !isReleased)
            if (!isReleased && !isReqOrder)
            {
                lnkbtnModify.Visible = Master.isModDelAuthOk;
                lnkbtnUpdate.Visible = !(Master.isModDelAuthOk);
                lnkbtnCancel.Visible = Master.isModDelAuthOk;
                lnkbtnDelete.Visible = Master.isModDelAuthOk;

                divModi.Visible = Master.isModDelAuthOk; 
                divUpdate.Visible = !(Master.isModDelAuthOk);
                divCancel.Visible = Master.isModDelAuthOk;
                divDel.Visible = Master.isModDelAuthOk;
            }
            else
            {
                lnkbtnModify.Visible = CommValue.AUTH_VALUE_FALSE;
                lnkbtnUpdate.Visible = CommValue.AUTH_VALUE_TRUE;
                lnkbtnCancel.Visible = CommValue.AUTH_VALUE_FALSE;
                lnkbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;

                divModi.Visible = CommValue.AUTH_VALUE_FALSE;
                divUpdate.Visible = CommValue.AUTH_VALUE_TRUE;
                divCancel.Visible = CommValue.AUTH_VALUE_FALSE;
                divDel.Visible = CommValue.AUTH_VALUE_FALSE;
            }

            if (!Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_ADMIN) &&
                !Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
            {
                if (Master.isModDelAuthOk.Equals(CommValue.AUTH_VALUE_TRUE))
                {
                    lnkbtnDelete.Visible = isDelYn;
                    lnkbtnCancel.Visible = isDelYn;
                    lnkbtnModify.Visible = isDelYn;
                    lnkbtnUpdate.Visible = !isDelYn;

                    divDel.Visible = isDelYn;
                    divCancel.Visible = isDelYn;
                    divModi.Visible = isDelYn;
                    divUpdate.Visible = !isDelYn;
                }
            }

            //lnkbtnModify.Visible = isDelYn;
            //lnkbtnDelete.Visible = isDelYn;
        }

        protected void LoadData()
        {
            // KN_USP_STK_SELECT_RELEASEREQUESTINFO_S01
            DataSet dsReturn = ReleaseInfoBlo.WatchReleaseRequestDetailInfo(txtHfReleaseSeq.Text, Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                if (dsReturn.Tables.Count > 0)
                {
                    // 기본정보 조회
                    dtBasicReturn = dsReturn.Tables[0];

                    txtHfRequestCnt.Text = dtBasicReturn.Rows.Count.ToString();

                    // 승인 담당자 정보 조회
                    dtChargerReturn = dsReturn.Tables[1];

                    txtHfChargerCnt.Text = (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count).ToString();

                    // 출고정보 조회
                    lvRequestList.DataSource = dtBasicReturn;
                    lvRequestList.DataBind();

                    // 기본정보 조회
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

                    if (!string.IsNullOrEmpty(dtBasicReturn.Rows[0]["ProcessFee"].ToString()))
                    {
                        ltInsFmsFee.Text = TextLib.MakeVietIntNo(double.Parse(dtBasicReturn.Rows[0]["ProcessFee"].ToString()).ToString("###,##0"));
                        ltDong.Text = "&nbsp;" + TextNm["DONG"];
                    }

                    ltInsFmsRemark.Text = dtBasicReturn.Rows[0]["ProcessRemark"].ToString();

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
            ((Literal)lvRequestList.FindControl("ltHaveQty")).Text = TextNm["HAVEQTY"];
            ((Literal)lvRequestList.FindControl("ltQty")).Text = TextNm["DEMANDEDQTY"];
            ((Literal)lvRequestList.FindControl("ltProgressCd")).Text = TextNm["RELEASESTATUS"];
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

                if (!string.IsNullOrEmpty(drView["ReleaseDetSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSeq")).Text = drView["ReleaseDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()) &&
                    !string.IsNullOrEmpty(drView["RentCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiGrpCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiMainCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItem")).Text = drView["ClassNm"].ToString() + " (" + drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + "-" + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString() +")";
                    ((TextBox)iTem.FindControl("txtHfRentCd")).Text =  drView["RentCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfSvcZoneCd")).Text = drView["SvcZoneCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassiGrpCd")).Text = drView["ClassiGrpCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassiMainCd")).Text = drView["ClassiMainCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassCd")).Text = drView["ClassCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RequestQty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = drView["RequestQty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["HaveQty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsHaveQty")).Text = drView["HaveQty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RealQty"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfRealQty")).Text = drView["RealQty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ProgressNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsProgressCd")).Text = drView["ProgressNm"].ToString();

                    if (drView["ProgressCd"].ToString().Equals(CommValue.RELEASE_TYPE_VALUE_RELEASED))
                    {
                        isReleased = CommValue.AUTH_VALUE_TRUE;
                    }

                    if (drView["ProgressCd"].ToString().Equals(CommValue.RELEASE_TYPE_VALUE_PURCHASEREQ))
                    {
                        isReqOrder = CommValue.AUTH_VALUE_TRUE;
                    }
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
                    if (isDelYn && !drView["ProgressCd"].ToString().Equals(CommValue.RELEASE_TYPE_VALUE_WAITING))
                    {
                        isDelYn = CommValue.AUTH_VALUE_FALSE;
                        txtHfProgressCd.Text = drView["ProgressCd"].ToString();
                    }
                }

                HtmlGenericControl divRelease = (HtmlGenericControl)iTem.FindControl("divRelease");
                LinkButton lnkRelease = (LinkButton)iTem.FindControl("lnkbtnRelease");
                lnkRelease.Text = TextNm["RELEASE"];

                if (drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_APPROVAL) &&
                    drView["ProgressCd"].ToString().Equals(CommValue.RELEASE_TYPE_VALUE_WAITING))
                {
                    if (Master.isModDelAuthOk.Equals(CommValue.AUTH_VALUE_TRUE) ||
                        txtHfProcessMemNo.Text.Equals(Session["MemNo"].ToString()))
                    {
                        divRelease.Visible = CommValue.AUTH_VALUE_TRUE;
                        lnkRelease.Visible = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        divRelease.Visible = CommValue.AUTH_VALUE_FALSE;
                        lnkRelease.Visible = CommValue.AUTH_VALUE_FALSE;
                    }
                }
                else
                {
                    divRelease.Visible = CommValue.AUTH_VALUE_FALSE;
                    lnkRelease.Visible = CommValue.AUTH_VALUE_FALSE;
                }
            }
        }

        protected void lvRequestList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strReleaseSeq = string.Empty;
                int intReleaseDetSeq = 0;

                strReleaseSeq = txtHfReleaseSeq.Text;

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex * Int32.Parse(txtHfChargerCnt.Text)].FindControl("txtHfReleaseDetSeq")).Text))
                {
                    intReleaseDetSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex * Int32.Parse(txtHfChargerCnt.Text)].FindControl("txtHfReleaseDetSeq")).Text);
                }

                // 출고완료 처리
                // KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00
                ReleaseInfoBlo.ModifyReleaseRequestItem(strReleaseSeq, intReleaseDetSeq, CommValue.RELEASE_TYPE_VALUE_RELEASED, CommValue.APPROVAL_TYPE_VALUE_APPROVAL);

                //Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + txtHfReleaseSeq.Text, CommValue.AUTH_VALUE_FALSE);
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
                TextBox txtHfReleaseDetSeq = (TextBox)iTem.FindControl("txtHfReleaseDetSeq");

                if (intRowCnt % (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count) == 0)
                {
                    tdSeq.RowSpan = dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count;
                    ltChargerSeq.Text = (intRowCnt / (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count) + 1).ToString();
                    txtHfReleaseDetSeq.Text = (intRowCnt / (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count) + 1).ToString();
                }
                else
                {
                    txtHfReleaseDetSeq.Text = (intRowCnt / (dtChargerReturn.Rows.Count / dtBasicReturn.Rows.Count) + 1).ToString();
                    tdSeq.Visible = CommValue.AUTH_VALUE_FALSE;
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
                string strReleaseSeq = string.Empty;
                int intReleaseDetSeq = 0;
                int intChargeSeq = 0;
                string strRemark = string.Empty;
                string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                strReleaseSeq = txtHfReleaseSeq.Text;

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfReleaseDetSeq")).Text))
                {
                    intReleaseDetSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfReleaseDetSeq")).Text);
                }

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfChargerSeq")).Text))
                {
                    intChargeSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfChargerSeq")).Text);
                }

                strRemark = ((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtRemark")).Text;

                // KN_USP_STK_UPDATE_RELEASEREQUESTDENY_M00
                // KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00
                ReleaseInfoBlo.ModifyReleaseRequestDeny(strReleaseSeq, intReleaseDetSeq, intChargeSeq, strRemark, CommValue.RELEASE_TYPE_VALUE_CANCELRELEASE ,CommValue.APPROVAL_TYPE_VALUE_REJECTED);

                // 대상자에게 반려 쪽지 발송
                // 양영석 : 출고 폼메일 제목 정하기
                MemoWriteUtil.RegistrySendMemo(TextNm["RELEASE"] + " " + TextNm["REJECTED"], MemoFormLib.MakeReleaseDenyForm(ltInsProcessMemNm.Text, strReleaseSeq), Session["CompCd"].ToString(), txtHfProcessMemNo.Text, strIP);
                // 출고 신청자 처리
                MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), txtHfProcessMemNo.Text );

                // 양영석 재고 히스토리 처리

                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + txtHfReleaseSeq.Text, CommValue.AUTH_VALUE_FALSE);
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
                string strReleaseSeq = string.Empty;
                string strOrderSeq = string.Empty;

                int intReleaseDetSeq = 0;
                int intChargeSeq = 0;
                int intChargeCnt = 0;
                int intOrderChargerCnt = 1;

                string strRemark = string.Empty;
                string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                strReleaseSeq = txtHfReleaseSeq.Text;

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfReleaseDetSeq")).Text))
                {
                    intReleaseDetSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfReleaseDetSeq")).Text);
                }

                if (!string.IsNullOrEmpty(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfChargerSeq")).Text))
                {
                    intChargeSeq = Int32.Parse(((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtHfChargerSeq")).Text);
                }

                strRemark = ((TextBox)lvChargerList.Items[e.ItemIndex].FindControl("txtRemark")).Text;

                // KN_USP_STK_UPDATE_RELEASEREQUESTCONFIRM_M00
                ReleaseInfoBlo.ModifyReleaseRequestConfirm(strReleaseSeq, intReleaseDetSeq, intChargeSeq, strRemark);

                if (!string.IsNullOrEmpty(txtHfChargerCnt.Text))
                {
                    intChargeCnt = Int32.Parse(txtHfChargerCnt.Text);
                }

                // 다음 단계 체크 후 있으면 쪽지 발송
                if (intChargeCnt > intChargeSeq)
                {
                    string strChargerNo = ((TextBox)lvChargerList.Items[intChargeSeq].FindControl("txtHfChargeMemNo")).Text;
                    string strChargerNm = ((Literal)lvChargerList.Items[intChargeSeq].FindControl("ltChargerNm")).Text;

                    // 출고 담당자 쪽지 발송
                    // 양영석 : 출고 폼메일 제목 정하기
                    MemoWriteUtil.RegistrySendMemo(TextNm["RELEASE"] + " " + TextNm["REQUEST"], MemoFormLib.MakeReleaseChargeForm(strChargerNm, strReleaseSeq), Session["CompCd"].ToString(), strChargerNo, strIP);
                    // 출고 다음 담당자 처리
                    MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), strChargerNo);

                    // 양영석 재고 히스토리 처리
                }
                // 다음 단계 체크 후 없으면 현재 수량 조회
                else
                {
                    int intRealQty = 0;
                    int intRequestQty = 0;
                    int intReturnDetSeq = 0;

                    double dblProcessFee = 0.0d;

                    string strRentCd = string.Empty;
                    string strSvcZoneCd = string.Empty;
                    string strGrpCd = string.Empty;
                    string strMainCd = string.Empty;
                    string strSubCd = string.Empty;

                    if (!string.IsNullOrEmpty(((Literal)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("ltInsHaveQty")).Text))
                    {
                        intRealQty = Int32.Parse(((Literal)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("ltInsHaveQty")).Text);
                    }

                    if (!string.IsNullOrEmpty(((Literal)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("ltInsQty")).Text))
                    {
                        intRequestQty = Int32.Parse(((Literal)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("ltInsQty")).Text);
                    }
                    
                    strRentCd = ((TextBox)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("txtHfRentCd")).Text;
                    strSvcZoneCd = ((TextBox)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("txtHfSvcZoneCd")).Text;
                    strGrpCd = ((TextBox)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("txtHfClassiGrpCd")).Text;
                    strMainCd = ((TextBox)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("txtHfClassiMainCd")).Text;
                    strSubCd = ((TextBox)lvRequestList.Items[intReleaseDetSeq - 1].FindControl("txtHfClassCd")).Text;

                    // 재고가 많으면 출고대기 후 대상자에게 승인 쪽지 발송
                    if (intRealQty >= intRequestQty)
                    {
                        // 출고대기 물량차감
                        // KN_USP_STK_UPDATE_GOODSINFO_M00
                        GoodsOrderInfoBlo.ModifyGoodsInfo(strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, intRequestQty, Session["CompCd"].ToString(), Session["LangCd"].ToString(), strIP);

                        // 출고대기 수정처리
                        // KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00
                        ReleaseInfoBlo.ModifyReleaseRequestItem(strReleaseSeq, intReleaseDetSeq, CommValue.RELEASE_TYPE_VALUE_WAITING, CommValue.APPROVAL_TYPE_VALUE_APPROVAL);

                        // 출고 신청자 승인 쪽지 발송
                        // 양영석 : 출고 폼메일 제목 정하기
                        MemoWriteUtil.RegistrySendMemo(TextNm["RELEASE"] + " " + TextNm["APPROVAL"], MemoFormLib.MakeReleaseConfirmForm(ltInsProcessMemNm.Text, strReleaseSeq), Session["CompCd"].ToString(), txtHfProcessMemNo.Text, strIP);
                        // 출고 신청자 처리
                        MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), txtHfProcessMemNo.Text);
                    }
                    // 재고가 적으면 구매요청 후 담당자에게 구매요청 쪽지 발송
                    else
                    {
                        // 보유수량이 존재할 경우
                        if (intRealQty > 0)
                        {
                            // 보유개수 물량차감
                            // KN_USP_STK_UPDATE_GOODSINFO_M00
                            GoodsOrderInfoBlo.ModifyGoodsInfo(strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, intRealQty, Session["CompCd"].ToString(), Session["LangCd"].ToString(), strIP);

                            // 출고대기 수정처리
                            // KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00
                            ReleaseInfoBlo.ModifyReleaseRequestItem(strReleaseSeq, intReleaseDetSeq, CommValue.RELEASE_TYPE_VALUE_WAITING, CommValue.APPROVAL_TYPE_VALUE_APPROVAL, intRealQty);

                            if (!string.IsNullOrEmpty(ltInsFmsFee.Text))
                            {
                                dblProcessFee = double.Parse(ltInsFmsFee.Text);
                            }

                            // 출고목록상 구매요청 추가처리
                            // KN_USP_STK_INSERT_RELEASEREQUESTINFO_S01
                            DataTable dtRelease = ReleaseInfoBlo.RegistryReleaseRequestInfo(strReleaseSeq, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, txtHfDptCd.Text
                                , txtHfProcessMemNo.Text, ltInsProcessDt.Text.Replace("-", ""), intRequestQty - intRealQty, ltInsRemark.Text, dblProcessFee
                                , ltInsFmsRemark.Text, CommValue.RELEASE_TYPE_VALUE_PURCHASEREQ, CommValue.APPROVAL_TYPE_VALUE_APPROVAL, CommValue.CHOICE_VALUE_NO
                                , Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                            intReturnDetSeq = Int32.Parse(dtRelease.Rows[0]["ReleaseDetSeq"].ToString());

                            // 출고 담당자 추가 후 자동승인 처리
                            // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M01
                            ReleaseInfoBlo.RegistryReleaseRequestAddon(strReleaseSeq, Int32.Parse(CommValue.AUTH_VALUE_EMPTY), CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                        }
                        else
                        {
                            // 출고대기 수정처리
                            // KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00
                            ReleaseInfoBlo.ModifyReleaseRequestItem(strReleaseSeq, intReleaseDetSeq, CommValue.RELEASE_TYPE_VALUE_PURCHASEREQ, CommValue.APPROVAL_TYPE_VALUE_APPROVAL, intRealQty);
                        }

                        DateTime dtNewDate = new DateTime();

                        dtNewDate = DateTime.ParseExact(ltInsProcessDt.Text + " 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

                        // 자동구매 요청시 청구일을 2일뒤로 자동연장
                        // 양영석 : 내부 규칙으로 정하던지 아니면 별도 처리가 필요함.
                        string strHfExpringDt = dtNewDate.AddDays(Int32.Parse(CommValue.NUMBER_VALUE_TWO)).ToString("s").Substring(0,10);

                        // 구매목록상 구매요청처리 등록처리
                        // 구매요청처리 ( 구매요청 / 미정 / 요청개수 )
                        if (intReturnDetSeq == 0)
                        {
                            intReturnDetSeq = intReleaseDetSeq;
                        }

                        // KN_USP_STK_INSERT_GOODSORDERINFO_S00
                        DataTable dtOrderReturn = GoodsOrderInfoBlo.RegistryGoodsOrderInfo(strOrderSeq, intOrderChargerCnt, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd
                            , txtHfDptCd.Text, Session["CompCd"].ToString(), txtHfProcessMemNo.Text, strHfExpringDt.Replace("-", ""), intRequestQty - intRealQty, ltInsRemark.Text, CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ, CommValue.APPROVAL_TYPE_VALUE_PENDING
                            , CommValue.CHOICE_VALUE_NO, strReleaseSeq, intReturnDetSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                        if (dtOrderReturn != null)
                        {
                            strOrderSeq = dtOrderReturn.Rows[0]["OrderSeq"].ToString();
                        }

                        // 구매 담당자 업무배정 처리
                        // KN_USP_STK_INSERT_GOODSORDERADDON_M01
                        GoodsOrderInfoBlo.RegistryGoodsOrderAddon(strOrderSeq, intOrderChargerCnt, CommValue.CHOICE_VALUE_NOTCONFIRM, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                        // KN_USP_STK_SELECT_GOODSORDERCHARGERINFO_S00
                        DataTable dtPurchaseCharger = GoodsOrderInfoBlo.SpreadGoodsOrderChargeInfo(ltInsProcessDt.Text.Replace("-", ""));

                        if (dtPurchaseCharger != null)
                        {
                            if (dtPurchaseCharger.Rows.Count > 0)
                            {
                                // 구매담당자에게 쪽지 발송
                                MemoWriteUtil.RegistrySendMemo(TextNm["PURCHASE"] + " " + TextNm["REQUEST"], MemoFormLib.MakeOrderChargeForm(dtPurchaseCharger.Rows[0]["MemNm"].ToString(), strReleaseSeq), Session["CompCd"].ToString(), dtPurchaseCharger.Rows[0]["ChargeMemNo"].ToString(), strIP);
                                MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), dtPurchaseCharger.Rows[0]["ChargeMemNo"].ToString());
                            }
                        }

                        intOrderChargerCnt++;

                        // 양영석 재고 히스토리 처리
                    }
                }

                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + txtHfReleaseSeq.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch(Exception ex)
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
                //Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfReleaseSeq.Text , CommValue.AUTH_VALUE_FALSE);
                Response.Redirect(Master.PAGE_TRANSFER + "?" + Master.PARAM_DATA1 + "=" + txtHfReleaseSeq.Text, CommValue.AUTH_VALUE_FALSE);
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
                Response.Redirect(Master.PAGE_TRANSFER + "?" + Master.PARAM_DATA1 + "=" + txtHfReleaseSeq.Text, CommValue.AUTH_VALUE_FALSE);
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
                    string strRentCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfRentCd")).Text;
                    string strSvcZoneCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfSvcZoneCd")).Text;
                    string strGrpCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfClassiGrpCd")).Text;
                    string strMainCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfClassiMainCd")).Text;
                    string strSubCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfClassCd")).Text;
                    string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string strStatusCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtInsStatusCd")).Text;

                    int intQty = -1 * Int32.Parse(((Literal)lvRequestList.Items[intTmpI].FindControl("ltInsQty")).Text);

                    // 승인시
                    if (strStatusCd.Equals(CommValue.APPROVAL_TYPE_VALUE_APPROVAL))
                    {
                        // 수량 복구
                        // KN_USP_STK_UPDATE_GOODSINFO_M00
                        GoodsOrderInfoBlo.ModifyGoodsInfo(strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, intQty, Session["CompCd"].ToString(), Session["LangCd"].ToString(), strIP);
                    }

                    // 출고신청자료 반려 후 취소 처리
                    // KN_USP_STK_UPDATE_RELEASEREQUESTDENY_M00
                    // KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00
                    ReleaseInfoBlo.ModifyReleaseRequestDeny(txtHfReleaseSeq.Text, intTmpI + 1, 1, string.Empty, CommValue.RELEASE_TYPE_VALUE_CANCELRELEASE, CommValue.APPROVAL_TYPE_VALUE_REJECTED);
                }

                // 양영석 재고 히스토리 처리

                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + txtHfReleaseSeq.Text, CommValue.AUTH_VALUE_FALSE);
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
                for (int intTmpI = 0; intTmpI < lvRequestList.Items.Count; intTmpI++)
                {
                    string strRentCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfRentCd")).Text;
                    string strSvcZoneCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfSvcZoneCd")).Text;
                    string strGrpCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfClassiGrpCd")).Text;
                    string strMainCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfClassiMainCd")).Text;
                    string strSubCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfClassCd")).Text;
                    string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string strStatusCd = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtInsStatusCd")).Text;

                    int intQty = -1 * Int32.Parse(((Literal)lvRequestList.Items[intTmpI].FindControl("ltInsQty")).Text);

                    // 승인시
                    if (strStatusCd.Equals(CommValue.APPROVAL_TYPE_VALUE_APPROVAL))
                    {
                        // 수량 복구
                        // KN_USP_STK_UPDATE_GOODSINFO_M00
                        GoodsOrderInfoBlo.ModifyGoodsInfo(strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, intQty, Session["CompCd"].ToString(), Session["LangCd"].ToString(), strIP);
                    }
                }

                // 출고신청자료 삭제
                // KN_USP_STK_DELETE_RELEASEREQUESTINFO_M00
                ReleaseInfoBlo.RemoveReleaseRequestMng(txtHfReleaseSeq.Text);

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