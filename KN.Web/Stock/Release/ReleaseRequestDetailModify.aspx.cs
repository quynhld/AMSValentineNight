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

using KN.Config.Biz;
using KN.Stock.Biz;

namespace KN.Web.Stock.Release
{
    [Transaction(TransactionOption.Required)]
    public partial class ReleaseRequestDetailModify : BasePage
    {
        string strViewDt = string.Empty;
        int intStartSeq = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    // KN_USP_STK_DELETE_TMPRELEASEINFO_M00
                    ReleaseInfoBlo.RemoveLatestTempReleaseRequestInfo();

                    CheckParams();

                    InitControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParams()
        {
            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    string[] strParams = Request.Params[Master.PARAM_DATA1].ToString().Split(CommValue.PARAM_VALUE_CHAR_ELEMENT);
                    int intRowCnt = 0;

                    for (int intTmpI = 0; intTmpI < strParams.Length; intTmpI++)
                    {
                        string strRentCd = strParams[intTmpI].Replace("-", "").Substring(0, 4);
                        string strSvcZoneCd = strParams[intTmpI].Replace("-", "").Substring(4, 4);
                        string strGrpCd = strParams[intTmpI].Replace("-", "").Substring(8, 4);
                        string strMainCd = strParams[intTmpI].Replace("-", "").Substring(12, 4);
                        string strSubCd = strParams[intTmpI].Replace("-", "").Substring(16, 4);

                        // KN_USP_STK_INSERT_TMPRELEASEINFO_S01
                        DataTable dtReturn = ReleaseInfoBlo.RegistryTempReleaseRequestInfo(intStartSeq, intTmpI + 1, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd);

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count > 0)
                            {
                                intStartSeq = Int32.Parse(dtReturn.Rows[0]["ReleaseSeq"].ToString());
                                intRowCnt++;
                            }
                        }
                    }

                    txtHfTmpReleaseSeq.Text = intStartSeq.ToString();
                    txtHfReleaseDetSeq.Text = intRowCnt.ToString();
                }
            }
        }

        protected void InitControls()
        {
            ltDpt.Text = TextNm["DEPT"];
            ltProcessMemNo.Text = TextNm["MEM"] + " " + TextNm["NAME"];
            ltProcessDt.Text = TextNm["RELEASED"] + " " + TextNm["DATE"];
            ltApprovalYn.Text = TextNm["STATUS"];

            ltCompNm.Text = TextNm["ITEM"] + TextNm["NAME"];
            ltQty.Text = TextNm["QTY"];
            ltRemark.Text = TextNm["REMARK"];
            ltFmsFee.Text = TextNm["FMS"] + " " + TextNm["CHARGE"];
            ltFmsDong.Text = TextNm["DONG"];
            ltFmsRemark.Text = TextNm["FMS"] + " " + TextNm["REMARK"];
            ltHaveQty.Text = TextNm["HAVEQTY"];
            ltChargeList.Text = TextNm["ACCEPTANCE"];

            lnkbtnTmpExchage.Text = TextNm["TEMPCHANGE"];

            MakeDeptDdl();

            MakeStaffDdl();

            txtProcessDt.Text = DateTime.Now.ToString("s").Substring(0,10);
            hfProcessDt.Value = DateTime.Now.ToString("s").Substring(0,10);

            txtQty.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtFmsFee.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtItemNm.Attributes["onclick"] = "javascript:return fnSearchPopup('" + txtItemNm.ClientID + "','" + txtInsHaveQty.ClientID + "','" + hfGoodsInfo.ClientID + "');";

            hfToday.Value = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            lnkbtnReleaseRequest.Text = TextNm["RELEASEDITEMADD"];
            lnkbtnReleaseRequest.Visible = Master.isWriteAuthOk;
            lnkbtnReleaseRequest.OnClientClick = "javascript:return fnCheckTmpRegist('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnTmpExchage.OnClientClick = "javascript:return fnChangePopup('" + hfTmpSeq.ClientID + "');";

            lnkbtnRegist.Text = TextNm["REGIST"];
            lnkbtnRegist.Visible = Master.isWriteAuthOk;
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_CANT_SELECT_PREDATE"] + "');";
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            lnkbtnSearchItem.Text = TextNm["SEARCH"];
            lnkbtnSearchItem.OnClientClick = "javascript:return fnSearchPopup('" + txtItemNm.ClientID + "','" + txtInsHaveQty.ClientID + "','" + hfGoodsInfo.ClientID + "');";

            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];
            LoadExchageDate();

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

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            if (!string.IsNullOrEmpty(txtHfTmpReleaseSeq.Text))
            {
                // KN_USP_STK_SELECT_TMPRELEASEINFO_S00
                DataTable dtTmpReturn = ReleaseInfoBlo.SpreadTempReleaseRequestInfo(Int32.Parse(txtHfTmpReleaseSeq.Text));

                if (dtTmpReturn != null)
                {
                    lvRequestList.DataSource = dtTmpReturn;
                    lvRequestList.DataBind();
                }
            }

            string strShowDt = string.Empty;

            strShowDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_RELEASECHARGERINFO_S00
            DataTable dtChargeList = ReleaseInfoBlo.SpreadReleaseChargeInfo(strShowDt);

            if (dtChargeList != null)
            {
                if (dtChargeList.Rows.Count > 0)
                {
                    StringBuilder sbList = new StringBuilder();

                    foreach (DataRow dr in dtChargeList.Select())
                    {
                        sbList.Append(dr["DeptNm"].ToString());
                        sbList.Append(" (");
                        sbList.Append(dr["MemNm"].ToString());
                        sbList.Append(") <img src='/Common/Images/Common/icon01.gif' alt='Next' align='absmiddle'/><img src='/Common/Images/Common/icon01.gif' alt='Next' align='absmiddle'/> ");
                    }

                    sbList.Append(TextNm["RELEASED"]);

                    ltReleaseChargeList.Text = sbList.ToString();
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
            ((Literal)lvRequestList.FindControl("ltItem")).Text = TextNm["ITEM"] + " " + TextNm["NAME"];
            ((Literal)lvRequestList.FindControl("ltItemCd")).Text = TextNm["ITEM"] + " " + TextNm["CD"];
            ((Literal)lvRequestList.FindControl("ltCompNm")).Text = TextNm["COMP"] + " " + TextNm["NAME"];
            ((Literal)lvRequestList.FindControl("ltCompCd")).Text = TextNm["COMP"] + " " + TextNm["CD"];
            ((Literal)lvRequestList.FindControl("ltQty")).Text = TextNm["QTY"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRequestList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
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

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItem")).Text = drView["ClassNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TmpReleaseDetSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfTmpReleaseDetSeq")).Text = drView["TmpReleaseDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ApprovalYn"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfApprovalYn")).Text = drView["ApprovalYn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiGrpCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiMainCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItemCd")).Text = drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + "-" + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfInsItemCd")).Text = drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsCompNm")).Text = drView["CompNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompNo"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsCompCd")).Text = drView["CompNo"].ToString();
                }

                TextBox txtInsQty = ((TextBox)iTem.FindControl("txtInsQty"));
                if (!string.IsNullOrEmpty(drView["Qty"].ToString()))
                {
                    txtInsQty.Text = drView["Qty"].ToString();
                    txtInsQty.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["HaveQty"].ToString()))
                {
                    TextBox txtInsHaveQty = ((TextBox)iTem.FindControl("txtInsHaveQty"));
                    txtInsHaveQty.Text = drView["HaveQty"].ToString();
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnQtyCheckValidate('" + txtInsQty.ClientID + "','" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_MODIFY_ITEM"] + "');";

            }
        }

        protected void lvRequestList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfTmpReleaseDetSeq = (TextBox)lvRequestList.Items[e.ItemIndex].FindControl("txtHfTmpReleaseDetSeq");
                TextBox txtInsQty = (TextBox)lvRequestList.Items[e.ItemIndex].FindControl("txtInsQty");

                if (!string.IsNullOrEmpty(txtInsQty.Text))
                {
                    // KN_USP_STK_UPDATE_TMPRELEASEINFO_M00
                    ReleaseInfoBlo.ModifyTempReleaseRequestInfo(Int32.Parse(txtHfTmpReleaseSeq.Text), Int32.Parse(txtHfTmpReleaseDetSeq.Text), Int32.Parse(txtInsQty.Text));

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvRequestList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfTmpReleaseDetSeq = (TextBox)lvRequestList.Items[e.ItemIndex].FindControl("txtHfTmpReleaseDetSeq");

                // KN_USP_STK_DELETE_TMPRELEASEINFO_M01
                ReleaseInfoBlo.RemoveTempReleaseRequestInfo(Int32.Parse(txtHfTmpReleaseSeq.Text), Int32.Parse(txtHfTmpReleaseDetSeq.Text));

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void MakeDeptDdl()
        {
            // KN_USP_COMM_SELECT_DEPTINFO_S00
            DataTable dtDept = StaffMngBlo.SpreadDeptInfo();

            ddlDept.Items.Clear();

            foreach (DataRow dr in dtDept.Select())
            {
                ddlDept.Items.Add(new ListItem(dr["DeptNm"].ToString(), dr["DeptCd"].ToString()));
            }
        }

        private void MakeStaffDdl()
        {
            // KN_USP_COMM_SELECT_STAFFINFO_S00
            DataTable dtStaff = StaffMngBlo.SpreadStaffInfo(ddlDept.SelectedValue);

            ddlProcessMemNo.Items.Clear();

            foreach (DataRow dr in dtStaff.Select())
            {
                ddlProcessMemNo.Items.Add(new ListItem(dr["MemNm"].ToString(), dr["MemNo"].ToString()));
            }
        }

        protected void ddlDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeStaffDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnReleaseRequest_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (string.IsNullOrEmpty(txtHfTmpReleaseSeq.Text))
                {
                    txtHfTmpReleaseSeq.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (string.IsNullOrEmpty(txtHfReleaseDetSeq.Text))
                {
                    txtHfReleaseDetSeq.Text = CommValue.NUMBER_VALUE_ONE;
                }
                else
                {
                    txtHfReleaseDetSeq.Text = (Int32.Parse(txtHfReleaseDetSeq.Text) + 1).ToString();
                }

                hfReturnValue.Value = txtItemNm.Text;
                string[] arrStrData = hfGoodsInfo.Value.Split(CommValue.PARAM_VALUE_CHAR_ELEMENT);

                // KN_USP_STK_INSERT_TMPRELEASEINFO_S00
                DataTable dtReturn = ReleaseInfoBlo.RegistryTempReleaseRequestInfo(Int32.Parse(txtHfTmpReleaseSeq.Text), Int32.Parse(txtHfReleaseDetSeq.Text), arrStrData[2], arrStrData[4], arrStrData[5], arrStrData[7], arrStrData[9], Int32.Parse(txtQty.Text), Int32.Parse(arrStrData[11]), arrStrData[12]);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        txtHfTmpReleaseSeq.Text = dtReturn.Rows[0]["ReleaseSeq"].ToString();
                    }
                }

                LoadData();

                hfGoodsInfo.Value = string.Empty;
                txtQty.Text = string.Empty;

                //ResetControls();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (lvRequestList.Items.Count > 0)
                {
                    string strEmergency = string.Empty;
                    string strRequestSeq = string.Empty;
                    string strOrderSeq = string.Empty;
                    string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    int intRowCnt = 0;

                    bool isMemoSendOk = CommValue.AUTH_VALUE_FALSE;

                    for (int intTmpI = 0; intTmpI < lvRequestList.Items.Count; intTmpI++)
                    {
                        DataTable dtReleaseReturn;
                        DataTable dtOrderReturn;

                        TextBox txtInsQty = (TextBox)lvRequestList.Items[intTmpI].FindControl("txtInsQty");
                        TextBox txtInsHaveQty = (TextBox)lvRequestList.Items[intTmpI].FindControl("txtInsHaveQty");
                        TextBox txtHfItemCd = (TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfInsItemCd");
                        TextBox txtHfApprovalYn = (TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfApprovalYn");

                        string strRentCd = txtHfItemCd.Text.Substring(0, 4);
                        string strSvcZoneCd = txtHfItemCd.Text.Substring(4, 4);
                        string strGrpCd = txtHfItemCd.Text.Substring(8, 4);
                        string strMainCd = txtHfItemCd.Text.Substring(12, 4);
                        string strSubCd = txtHfItemCd.Text.Substring(16, 4);
                        string strProcessCd = string.Empty;
                        string strStatusCd = string.Empty;


                        int intQty = Int32.Parse(txtInsQty.Text);
                        int intHaveQty = Int32.Parse(txtInsHaveQty.Text);

                        intRowCnt++;

                        if (!string.IsNullOrEmpty(txtHfReleaseSeq.Text))
                        {
                            strRequestSeq = txtHfReleaseSeq.Text;
                        }

                        double dblProcessFee = 0.0d;

                        if (!string.IsNullOrEmpty(txtFmsFee.Text))
                        {
                            dblProcessFee = double.Parse(txtFmsFee.Text);
                        }

                        if (chkApprovalYn.Checked)
                        {
                            strEmergency = CommValue.CONCLUSION_TYPE_TEXT_YES;
                        }
                        else
                        {
                            strEmergency = CommValue.CONCLUSION_TYPE_TEXT_NO;
                        }

                        if (strEmergency.Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                        {
                            strEmergency = txtHfApprovalYn.Text;
                        }

                        if (strEmergency.Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                        {
                            // 긴급이면서 보유수량보다 많을 경우
                            if (intQty > intHaveQty)
                            {
                                // 일부출고 && 일부 발주
                                // 출고대기처리 ( 출고대기 / 승인 / 보유개수 )
                                if (intHaveQty > 0)
                                {
                                    strProcessCd = CommValue.RELEASE_TYPE_VALUE_WAITING;
                                    strStatusCd = CommValue.APPROVAL_TYPE_VALUE_APPROVAL;

                                    // 출고대기 물량차감
                                    // KN_USP_STK_UPDATE_GOODSINFO_M00
                                    GoodsOrderInfoBlo.ModifyGoodsInfo(strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, intHaveQty, Session["CompCd"].ToString(), Session["LangCd"].ToString(), strIP);

                                    // 출고대기 등록처리
                                    // KN_USP_STK_INSERT_RELEASEREQUESTINFO_S00
                                    dtReleaseReturn = ReleaseInfoBlo.RegistryReleaseRequestInfo(strRequestSeq, intRowCnt, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd
                                        , ddlDept.SelectedValue, Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, hfProcessDt.Value.Replace("-", ""), intHaveQty, txtRemark.Text
                                        , dblProcessFee, txtFmsRemark.Text, strProcessCd, strStatusCd, strEmergency, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                                    if (dtReleaseReturn != null)
                                    {
                                        txtHfReleaseSeq.Text = dtReleaseReturn.Rows[0]["ReleaseSeq"].ToString();
                                        strRequestSeq = txtHfReleaseSeq.Text;
                                    }

                                    // 출고 담당자 선승인처리
                                    if (!string.IsNullOrEmpty(hfTmpSeq.Value))
                                    {
                                        // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M00
                                        ReleaseInfoBlo.RegistryTmpReleaseRequestAddon(strRequestSeq, intRowCnt, Int32.Parse(hfTmpSeq.Value), CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                                    }
                                    else
                                    {
                                        // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M01
                                        ReleaseInfoBlo.RegistryReleaseRequestAddon(strRequestSeq, intRowCnt, CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                                    }

                                    // 출고 신청자 승인 쪽지 발송
                                    // 양영석 : 출고 폼메일 제목 정하기
                                    MemoWriteUtil.RegistrySendMemo(TextNm["RELEASE"] + " " + TextNm["APPROVAL"], MemoFormLib.MakeReleaseConfirmForm(ddlProcessMemNo.Text, strRequestSeq), Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, strIP);
                                    // 출고 신청자 처리
                                    MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue);

                                    // 양영석 재고 히스토리 처리

                                    intRowCnt++;
                                }

                                // 출고대기처리 ( 구매요청 / 승인 / 요청개수 - 보유개수 )
                                strProcessCd = CommValue.RELEASE_TYPE_VALUE_PURCHASEREQ;
                                strStatusCd = CommValue.APPROVAL_TYPE_VALUE_APPROVAL;

                                // 출고목록상 구매요청 처리
                                // KN_USP_STK_INSERT_RELEASEREQUESTINFO_S00
                                dtReleaseReturn = ReleaseInfoBlo.RegistryReleaseRequestInfo(strRequestSeq, intRowCnt, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd
                                    , ddlDept.SelectedValue, Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, hfProcessDt.Value.Replace("-", ""), intQty - intHaveQty, txtRemark.Text
                                    , dblProcessFee, txtFmsRemark.Text, strProcessCd, strStatusCd, strEmergency, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                                if (dtReleaseReturn != null)
                                {
                                    txtHfReleaseSeq.Text = dtReleaseReturn.Rows[0]["ReleaseSeq"].ToString();
                                    strRequestSeq = txtHfReleaseSeq.Text;
                                }

                                // 출고 담당자 선승인처리
                                if (!string.IsNullOrEmpty(hfTmpSeq.Value))
                                {
                                    // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M00
                                    ReleaseInfoBlo.RegistryTmpReleaseRequestAddon(strRequestSeq, intRowCnt, Int32.Parse(hfTmpSeq.Value), CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                                }
                                else
                                {
                                    // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M01
                                    ReleaseInfoBlo.RegistryReleaseRequestAddon(strRequestSeq, intRowCnt, CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                                }

                                // 구매목록상 구매요청처리 등록처리
                                // 구매요청처리 ( 구매요청 / 승인 / 요청개수 )
                                strProcessCd = CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ;
                                strStatusCd = CommValue.APPROVAL_TYPE_VALUE_APPROVAL;

                                DateTime dtNewDate = new DateTime();

                                dtNewDate = DateTime.ParseExact(hfProcessDt.Value + " 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

                                // 자동구매 요청시 청구일을 2일뒤로 자동연장
                                // 양영석 : 내부 규칙으로 정하던지 아니면 별도 처리가 필요함.
                                string strHfExpringDt = dtNewDate.AddDays(Int32.Parse(CommValue.NUMBER_VALUE_TWO)).ToString("s").Substring(0,10);

                                // 구매요청 등록처리
                                // KN_USP_STK_INSERT_GOODSORDERINFO_S00
                                dtOrderReturn = GoodsOrderInfoBlo.RegistryGoodsOrderInfo(strOrderSeq, intRowCnt, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd
                                    , ddlDept.SelectedValue, Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, strHfExpringDt.Replace("-", ""), intQty - intHaveQty, txtRemark.Text, strProcessCd, strStatusCd
                                    , strEmergency, strRequestSeq, intRowCnt, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                                if (dtOrderReturn != null)
                                {
                                    strOrderSeq = dtOrderReturn.Rows[0]["OrderSeq"].ToString();
                                }

                                // 구매 담당자 선승인처리
                                // KN_USP_STK_INSERT_GOODSORDERADDON_M01
                                GoodsOrderInfoBlo.RegistryGoodsOrderAddon(strOrderSeq, intRowCnt, CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                                // KN_USP_STK_SELECT_GOODSORDERCHARGERINFO_S00
                                DataTable dtPurchaseCharger = GoodsOrderInfoBlo.SpreadGoodsOrderChargeInfo(hfProcessDt.Value.Replace("-", ""));

                                if (dtPurchaseCharger != null)
                                {
                                    if (dtPurchaseCharger.Rows.Count > 0)
                                    {
                                        // 구매담당자에게 쪽지 발송
                                        MemoWriteUtil.RegistrySendMemo(TextNm["PURCHASE"] + " " + TextNm["REQUEST"], MemoFormLib.MakePurchaseRequestForm(dtPurchaseCharger.Rows[0]["MemNm"].ToString(), strRequestSeq), Session["CompCd"].ToString(), dtPurchaseCharger.Rows[0]["ChargeMemNo"].ToString(), strIP);
                                        MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), dtPurchaseCharger.Rows[0]["ChargeMemNo"].ToString());
                                    }
                                }

                                // 양영석 재고 히스토리 처리

                            }
                            // 긴급이면서 보유수량 그 이하일 경우
                            else
                            {
                                // 출고대기처리 ( 출고대기 / 승인 / 요청개수 )
                                strProcessCd = CommValue.RELEASE_TYPE_VALUE_WAITING;
                                strStatusCd = CommValue.APPROVAL_TYPE_VALUE_APPROVAL;

                                // 출고대기 물량차감
                                // KN_USP_STK_UPDATE_GOODSINFO_M00
                                GoodsOrderInfoBlo.ModifyGoodsInfo(strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, intQty, Session["CompCd"].ToString(), Session["LangCd"].ToString(), strIP);

                                // 출고대기 등록처리
                                // KN_USP_STK_INSERT_RELEASEREQUESTINFO_S00
                                dtReleaseReturn = ReleaseInfoBlo.RegistryReleaseRequestInfo(strRequestSeq, intRowCnt, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd
                                    , ddlDept.SelectedValue, Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, hfProcessDt.Value.Replace("-", ""), intQty, txtRemark.Text
                                    , dblProcessFee, txtFmsRemark.Text, strProcessCd, strStatusCd, strEmergency, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                                if (dtReleaseReturn != null)
                                {
                                    txtHfReleaseSeq.Text = dtReleaseReturn.Rows[0]["ReleaseSeq"].ToString();
                                    strRequestSeq = txtHfReleaseSeq.Text;
                                }

                                // 출고 담당자 선승인처리
                                if (!string.IsNullOrEmpty(hfTmpSeq.Value))
                                {
                                    // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M00
                                    ReleaseInfoBlo.RegistryTmpReleaseRequestAddon(strRequestSeq, intRowCnt, Int32.Parse(hfTmpSeq.Value), CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                                }
                                else
                                {
                                    // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M01
                                    ReleaseInfoBlo.RegistryReleaseRequestAddon(strRequestSeq, intRowCnt, CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                                }

                                // 출고 신청자 승인 쪽지 발송
                                // 양영석 : 출고 폼메일 제목 정하기
                                MemoWriteUtil.RegistrySendMemo(TextNm["RELEASE"] + " " + TextNm["APPROVAL"], MemoFormLib.MakeReleaseConfirmForm(ddlProcessMemNo.Text, strRequestSeq), Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, strIP);
                                // 출고 신청자 처리
                                MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue);

                                // 양영석 재고 히스토리 처리
                            }
                        }
                        else
                        {
                            // 긴급이 아닐경우 ( 출고대기 / 미결 / 요청개수 )
                            strProcessCd = CommValue.RELEASE_TYPE_VALUE_WAITING;
                            strStatusCd = CommValue.APPROVAL_TYPE_VALUE_PENDING;

                            // 출고대기 중 등록처리
                            // KN_USP_STK_INSERT_RELEASEREQUESTINFO_S00
                            dtReleaseReturn = ReleaseInfoBlo.RegistryReleaseRequestInfo(strRequestSeq, intRowCnt, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd
                                , ddlDept.SelectedValue, Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, hfProcessDt.Value.Replace("-", ""), intQty, txtRemark.Text
                                , dblProcessFee, txtFmsRemark.Text, strProcessCd, strStatusCd, strEmergency, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                            if (dtReleaseReturn != null)
                            {
                                txtHfReleaseSeq.Text = dtReleaseReturn.Rows[0]["ReleaseSeq"].ToString();
                                strRequestSeq = txtHfReleaseSeq.Text;
                            }

                            // 출고 담당자 미결처리
                            if (!string.IsNullOrEmpty(hfTmpSeq.Value))
                            {
                                // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M00
                                ReleaseInfoBlo.RegistryTmpReleaseRequestAddon(strRequestSeq, intRowCnt, Int32.Parse(hfTmpSeq.Value), CommValue.CHOICE_VALUE_NOTCONFIRM, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                            }
                            else
                            {
                                // KN_USP_STK_INSERT_RELEASEREQUESTADDON_M01
                                ReleaseInfoBlo.RegistryReleaseRequestAddon(strRequestSeq, intRowCnt, CommValue.CHOICE_VALUE_NOTCONFIRM, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                            }

                            // 양영석 재고 히스토리 처리

                            // 출고 담당자에게 메모 발생처리 승인
                            isMemoSendOk = CommValue.AUTH_VALUE_TRUE;

                        }
                    }

                    if (isMemoSendOk)
                    {
                        // 출고 담당자 조회
                        // KN_USP_STK_SELECT_RELEASEREQUESTADDON_S00
                        DataTable dtChargeReturn = ReleaseInfoBlo.SpreadReleaseRequestAddon(strRequestSeq);

                        if (dtChargeReturn != null)
                        {
                            if (dtChargeReturn.Rows.Count > 0)
                            {
                                string strChargeMemNo = dtChargeReturn.Rows[0]["ChargeMemNo"].ToString();
                                string strChargeMemNm = dtChargeReturn.Rows[0]["ChargeMemNm"].ToString();

                                // 출고 담당자 쪽지 발송
                                // 양영석 : 출고 폼메일 제목 정하기
                                MemoWriteUtil.RegistrySendMemo(TextNm["RELEASE"] + " " + TextNm["REQUEST"], MemoFormLib.MakeReleaseChargeForm(strChargeMemNm, strRequestSeq), Session["CompCd"].ToString(), strChargeMemNo, strIP);
                                // 출고 최하위 담당자 처리
                                MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), strChargeMemNo);
                            }
                        }
                    }

                    Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RegistItem", "javascript:alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "')", CommValue.AUTH_VALUE_TRUE);
                }
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

                if (!string.IsNullOrEmpty(txtHfTmpReleaseSeq.Text))
                {
                    // KN_USP_STK_DELETE_TMPRELEASEINFO_M02
                    ReleaseInfoBlo.RemoveTempReleaseRequestInfo(Int32.Parse(txtHfTmpReleaseSeq.Text));
                }

                if (!string.IsNullOrEmpty(hfTmpSeq.Value))
                {
                    // KN_USP_STK_DELETE_TEMPCHARGERINFO_M00
                    MaterialMngBlo.RemoveTempChargerInfo(Int32.Parse(hfTmpSeq.Value));
                }

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnCharge_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["ExchageReleaseYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnOriginData_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_STK_SELECT_TEMPCHARGERINFO_S00
                DataTable dtTmpListReturn = MaterialMngBlo.SpreadTempChargerInfo(Int32.Parse(hfTmpSeq.Value));

                if (dtTmpListReturn != null)
                {
                    if (dtTmpListReturn.Rows.Count > 0)
                    {
                        StringBuilder sbList = new StringBuilder();

                        foreach (DataRow dr in dtTmpListReturn.Select())
                        {
                            sbList.Append(dr["DeptNm"].ToString());
                            sbList.Append(" (");
                            sbList.Append(dr["ChargeMemNm"].ToString());
                            sbList.Append(") <img src='/Common/Images/Common/icon01.gif' alt='Next' align='absmiddle'/><img src='/Common/Images/Common/icon01.gif' alt='Next' align='absmiddle'/> ");
                        }

                        sbList.Append(TextNm["RELEASED"]);

                        ltReleaseChargeList.Text = sbList.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnSearchItem_Click(object sender, EventArgs e)
        {
            try
            {
                Session["FindSearchYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}