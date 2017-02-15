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

namespace KN.Web.Stock.Order
{
    [Transaction(TransactionOption.Required)]
    public partial class GoodsOrderWrite : BasePage
    {
        string strViewDt = string.Empty;
        int intStartSeq = 0;
        double dblSumPrice = 0.0d;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    // 최근 임시 발주요청 정보 삭제
                    // KN_USP_STK_DELETE_TMPGOODSORDERINFO_M00
                    GoodsOrderInfoBlo.RemoveLatestTempGoodOrderInfo();

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

                        // KN_USP_STK_INSERT_TMPGOODSORDERINFO_S00
                        DataTable dtReturn = GoodsOrderInfoBlo.RegistryTempGoodsOrderInfo(intStartSeq, intTmpI + 1, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd);

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count > 0)
                            {
                                intStartSeq = Int32.Parse(dtReturn.Rows[0]["GoodsOrderSeq"].ToString());
                                intRowCnt++;
                            }
                        }
                    }
                    txtHfTmpOrderSeq.Text = intStartSeq.ToString();
                    txtHfGoodsOrderDetSeq.Text = intRowCnt.ToString();
                }
            }
        }

        protected void InitControls()
        {
            ltDpt.Text = TextNm["DEPT"];
            ltProcessMemNo.Text = TextNm["MEM"] + " " + TextNm["NAME"];
            ltProcessDt.Text = TextNm["WAREHOUSING"] + " " + TextNm["DATE"];
            ltApprovalYn.Text = TextNm["STATUS"];

            ltHaveQty.Text = TextNm["HAVEQTY"];
            ltQty.Text = TextNm["QTY"];
            ltTotPrice.Text = TextNm["TOTAL"];
            ltChargeList.Text = TextNm["ACCEPTANCE"];
            ltRemark.Text = TextNm["REMARK"];
            ltItemNm.Text = TextNm["ITEM"] + TextNm["NAME"];

            lnkbtnTmpExchage.Text = TextNm["TEMPCHANGE"];

            MakeDeptDdl();

            MakeStaffDdl();

            txtProcessDt.Text = DateTime.Now.ToString("s").Substring(0,10);
            hfProcessDt.Value = DateTime.Now.ToString("s").Substring(0,10);

            txtQty.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            hfToday.Value = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            lnkbtnGoodsOrder.Text = TextNm["ORDERITEMADD"];
            lnkbtnGoodsOrder.Visible = Master.isWriteAuthOk;
            lnkbtnGoodsOrder.OnClientClick = "javascript:return fnCheckTmpRegist('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnTmpExchage.OnClientClick = "javascript:return fnChangePopup('" + hfTmpSeq.ClientID + "');";
            lnkbtnRegist.Text = TextNm["REGIST"];
            lnkbtnRegist.Visible = Master.isWriteAuthOk;
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_CANT_SELECT_PREDATE"] + "');";
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            lnkbtnSearchItem.Text = TextNm["SEARCH"];
            lnkbtnSearchItem.OnClientClick = "javascript:return fnSearchPopup('" + txtItemNm.ClientID + "','" + txtInsHaveQty.ClientID + "','" + hfGoodsInfo.ClientID + "');";

            txtItemNm.Attributes["onclick"] = "javascript:return fnSearchPopup('" + txtItemNm.ClientID + "','" + txtInsHaveQty.ClientID + "','" + hfGoodsInfo.ClientID + "');";


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
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(Int32.Parse(strDong).ToString("###,##0")) + "&nbsp;" + TextNm["DONG"].ToString();
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

            if (!string.IsNullOrEmpty(txtHfTmpOrderSeq.Text))
            {
                // KN_USP_STK_SELECT_TMPGOODSORDERINFO_S00
                DataTable dtTmpReturn = GoodsOrderInfoBlo.SpreadTempGoodsOrderInfo(Int32.Parse(txtHfTmpOrderSeq.Text));
                
                if (dtTmpReturn != null)
                {
                    lvRequestList.DataSource = dtTmpReturn;
                    lvRequestList.DataBind();
                }
           
            }

            string strShowDt = string.Empty;

            strShowDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_GOODSORDERCHARGERINFO_S00
            DataTable dtChargeList = GoodsOrderInfoBlo.SpreadGoodsOrderChargeInfo(strShowDt);

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

                    sbList.Append(TextNm["ORDER"]);

                    ltReleaseChargeList.Text = sbList.ToString();
                }
            }
        }
        
        private void CheckSumPrice()
        {
            double dblTotPrice = 0.0d;

            for (int intTmpI = 0; intTmpI < lvRequestList.Items.Count; intTmpI++)
            {
                TextBox txtInsQty = ((TextBox)lvRequestList.Items[intTmpI].FindControl("txtInsQty"));
                Literal ltInsSellingPrice = ((Literal)lvRequestList.Items[intTmpI].FindControl("ltInsSellingPrice"));
                Literal ltInsTotPrice = ((Literal)lvRequestList.Items[intTmpI].FindControl("ltInsTotPrice"));

                if (!string.IsNullOrEmpty(ltInsSellingPrice.Text) && !string.IsNullOrEmpty(txtInsQty.Text))
                {
                    ltInsTotPrice.Text = TextLib.MakeVietIntNo((double.Parse(ltInsSellingPrice.Text) * double.Parse(txtInsQty.Text)).ToString("###,##0"));
                    dblTotPrice = dblTotPrice + double.Parse(ltInsSellingPrice.Text) * double.Parse(txtInsQty.Text);
                }
            }

            ltSumPrice.Text = TextLib.MakeVietIntNo(dblTotPrice.ToString("###,##0"));
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
            ((Literal)lvRequestList.FindControl("ltCompNm")).Text = TextNm["COMP"] + " " + TextNm["NAME"];
            ((Literal)lvRequestList.FindControl("ltQty")).Text = TextNm["QTY"];
            ((Literal)lvRequestList.FindControl("ltSellingPrice")).Text = TextNm["SELLINGCOST"];
            ((Literal)lvRequestList.FindControl("ltTotPrice")).Text = TextNm["TOTAL"];
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

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()) &&
                    !string.IsNullOrEmpty(drView["RentCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiGrpCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiMainCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItem")).Text = drView["ClassNm"].ToString() + " (" + drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + "-" + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString() + ") ";
                    ((TextBox)iTem.FindControl("txtHfItemCd")).Text = drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TmpGoodsOrderDetSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfTmpOrderDetSeq")).Text = drView["TmpGoodsOrderDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ApprovalYn"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfApprovalYn")).Text = drView["ApprovalYn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsCompNm")).Text = drView["CompNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompNo"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfCompCd")).Text = drView["CompNo"].ToString();
                }

                TextBox txtInsQty = ((TextBox)iTem.FindControl("txtInsQty"));
                txtInsQty.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                txtInsQty.Attributes["onchange"] = "javascript:fnCheckData();";

                if (!string.IsNullOrEmpty(drView["Qty"].ToString()))
                {
                    txtInsQty.Text = drView["Qty"].ToString();
                }
                else
                {
                    txtInsQty = ((TextBox)iTem.FindControl("txtInsQty"));
                    txtInsQty.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["HaveQty"].ToString()))
                {
                    TextBox txtInsHaveQty = ((TextBox)iTem.FindControl("txtInsHaveQty"));
                    txtInsHaveQty.Text = drView["HaveQty"].ToString();
                }

                Literal ltInsSellingPrice = ((Literal)iTem.FindControl("ltInsSellingPrice"));
                Literal ltInsTotPrice = ((Literal)iTem.FindControl("ltInsTotPrice"));

                if (!string.IsNullOrEmpty(drView["UnitSellingPrice"].ToString()))
                {
                    ltInsSellingPrice.Text = TextLib.MakeVietIntNo(double.Parse(drView["UnitSellingPrice"].ToString()).ToString("###,##0"));

                    if (!string.IsNullOrEmpty(txtInsQty.Text) && !string.IsNullOrEmpty(ltInsSellingPrice.Text))
                    {
                        
                        //ltInsTotPrice.Text = TextLib.MakeVietIntNo((Int32.Parse(txtInsQty.Text) * double.Parse(ltInsSellingPrice.Text)).ToString("###,##0"));
                        ltInsTotPrice.Text = TextLib.MakeVietIntNo((double.Parse(drView["UnitSellingPrice"].ToString()) * double.Parse(drView["Qty"].ToString())).ToString("###,##0"));
                        dblSumPrice = dblSumPrice + double.Parse(drView["UnitSellingPrice"].ToString()) * double.Parse(drView["Qty"].ToString());
                    }
                    else
                    {
                        ltInsTotPrice.Text = CommValue.NUMBER_VALUE_ZERO;
                    }
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnQtyCheckValidate('" + txtInsQty.ClientID + "','" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_MODIFY_ITEM"] + "');";

                ltSumPrice.Text = TextLib.MakeVietIntNo(dblSumPrice.ToString("###,##0"));
            }
        }

        protected void imgbtnCheckSum_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                CheckSumPrice();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvRequestList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtHfTmpOrderDetSeq = (TextBox)lvRequestList.Items[e.ItemIndex].FindControl("txtHfTmpOrderDetSeq");
                TextBox txtInsQty = (TextBox)lvRequestList.Items[e.ItemIndex].FindControl("txtInsQty");

                if (!string.IsNullOrEmpty(txtInsQty.Text))
                {
                    // KN_USP_STK_UPDATE_TMPGOODSORDERINFO_M00
                    GoodsOrderInfoBlo.ModifyTempGoodsOrderInfo(Int32.Parse(txtHfTmpOrderSeq.Text), Int32.Parse(txtHfTmpOrderDetSeq.Text), Int32.Parse(txtInsQty.Text));

                    LoadData();
                    CheckSumPrice();
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

                TextBox txtHfTmpOrderDetSeq = (TextBox)lvRequestList.Items[e.ItemIndex].FindControl("txtHfTmpOrderDetSeq");

                // KN_USP_STK_DELETE_TMPGOODSORDERINFO_M01
                GoodsOrderInfoBlo.RemoveTempGoodsOrderInfo(Int32.Parse(txtHfTmpOrderSeq.Text), Int32.Parse(txtHfTmpOrderDetSeq.Text));

                LoadData();
                CheckSumPrice();
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

        private void ResetControls()
        {
            ltInsScale.Text = string.Empty;
            ltInsTotPrice.Text = CommValue.NUMBER_VALUE_ZERO;
            txtItemNm.Text = string.Empty;
            txtInsHaveQty.Text = string.Empty;
            txtQty.Text = string.Empty;
            hfGoodsInfo.Value = string.Empty;
            chkApprovalYn.Checked = false;
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

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            string[] arrStrData = hfGoodsInfo.Value.Split(CommValue.PARAM_VALUE_CHAR_ELEMENT);

            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(hfGoodsInfo.Value))
            {
                ltInsTotPrice.Text = TextLib.MakeVietIntNo((double.Parse(arrStrData[13]) * double.Parse(txtQty.Text)).ToString("###,##0")) + " " + TextNm["DONG"];
            }
            else
            {
                ltInsTotPrice.Text = CommValue.NUMBER_VALUE_ZERO + " " + TextNm["DONG"];
            }

            txtItemNm.Text = arrStrData[10];
            txtInsHaveQty.Text = arrStrData[11];
        }

        protected void lnkbtnGoodsOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHfTmpOrderSeq.Text))
                {
                    txtHfTmpOrderSeq.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (string.IsNullOrEmpty(txtHfGoodsOrderDetSeq.Text))
                {
                    txtHfGoodsOrderDetSeq.Text = CommValue.NUMBER_VALUE_ONE;
                }
                else
                {
                    txtHfGoodsOrderDetSeq.Text = (Int32.Parse(txtHfGoodsOrderDetSeq.Text) + 1).ToString();
                }

                hfReturnValue.Value = txtItemNm.Text;
                string[] arrStrData = hfGoodsInfo.Value.Split(CommValue.PARAM_VALUE_CHAR_ELEMENT);

                // KN_USP_STK_INSERT_TMPGOODSORDERINFO_S01
                DataTable dtReturn = GoodsOrderInfoBlo.RegistryTempGoodsOrderInfo(Int32.Parse(txtHfTmpOrderSeq.Text), Int32.Parse(txtHfGoodsOrderDetSeq.Text), arrStrData[2], arrStrData[4], arrStrData[5], arrStrData[7], arrStrData[9], Int32.Parse(txtQty.Text), Int32.Parse(arrStrData[11]), arrStrData[12]);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        txtHfTmpOrderSeq.Text = dtReturn.Rows[0]["GoodsOrderSeq"].ToString();
                    }
                }

                LoadData();

                ResetControls();
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
                    string strOrderSeq = string.Empty;
                    string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    int intRowCnt = 0;

                    bool isMemoSendOk = CommValue.AUTH_VALUE_FALSE;

                    for (int intTmpI = 0; intTmpI < lvRequestList.Items.Count; intTmpI++)
                    {
                        //DataTable dtReleaseReturn;
                        DataTable dtOrderReturn;

                        TextBox txtInsQty = (TextBox)lvRequestList.Items[intTmpI].FindControl("txtInsQty");
                        TextBox txtInsHaveQty = (TextBox)lvRequestList.Items[intTmpI].FindControl("txtInsHaveQty");
                        TextBox txtHfItemCd = (TextBox)lvRequestList.Items[intTmpI].FindControl("txtHfItemCd");
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

                        if (!string.IsNullOrEmpty(txtHfGoodsOrderSeq.Text))
                        {
                            strOrderSeq = txtHfGoodsOrderSeq.Text;
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
                            // 긴급처리건의 경우
                            // 구매요청처리 ( 구매요청 / 승인 / 요청개수 )
                            strProcessCd = CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ;
                            strStatusCd = CommValue.APPROVAL_TYPE_VALUE_APPROVAL;

                            // 구매요청 등록처리
                            // KN_USP_STK_INSERT_GOODSORDERINFO_S00
                            dtOrderReturn = GoodsOrderInfoBlo.RegistryGoodsOrderInfo(strOrderSeq, intRowCnt, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd
                                , ddlDept.SelectedValue, Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, hfProcessDt.Value.Replace("-", ""), intQty, txtRemark.Text, strProcessCd, strStatusCd
                                , strEmergency, string.Empty, 0, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                            if (dtOrderReturn != null)
                            {
                                txtHfGoodsOrderSeq.Text = dtOrderReturn.Rows[0]["OrderSeq"].ToString();
                                strOrderSeq = txtHfGoodsOrderSeq.Text;
                            }

                            // 구매 담당자 선승인처리
                            if (!string.IsNullOrEmpty(hfTmpSeq.Value))
                            {
                                // KN_USP_STK_INSERT_GOODSORDERADDON_M00
                                GoodsOrderInfoBlo.RegistryTmpGoodsOrderAddon(strOrderSeq, intRowCnt, Int32.Parse(hfTmpSeq.Value), CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                            }
                            else
                            {
                                // KN_USP_STK_INSERT_GOODSORDERADDON_M01
                                GoodsOrderInfoBlo.RegistryGoodsOrderAddon(strOrderSeq, intRowCnt, CommValue.CHOICE_VALUE_YES, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                            }

                            // 구매 담당자에게 구매 요청 메세지 발송
                            // KN_USP_STK_SELECT_GOODSORDERCHARGERINFO_S00
                            DataTable dtPurchaseCharger = GoodsOrderInfoBlo.SpreadGoodsOrderChargeInfo(hfProcessDt.Value.Replace("-", ""));

                            if (dtPurchaseCharger != null)
                            {
                                if (dtPurchaseCharger.Rows.Count > 0)
                                {
                                    // 구매담당자에게 쪽지 발송
                                    MemoWriteUtil.RegistrySendMemo(TextNm["PURCHASE"] + " " + TextNm["APPROVAL"], MemoFormLib.MakePurchaseRequestForm(dtPurchaseCharger.Rows[0]["MemNm"].ToString(), strOrderSeq), Session["CompCd"].ToString(), dtPurchaseCharger.Rows[0]["ChargeMemNo"].ToString(), strIP);
                                    MemoWriteUtil.RegistrySendMemoDetail(Session["CompCd"].ToString(), dtPurchaseCharger.Rows[0]["ChargeMemNo"].ToString());
                                }
                            }

                            // 양영석 재고 히스토리 처리
                        }
                        else
                        {
                            // 긴급이 아닐경우 ( 구매요청 / 미결 / 요청개수 )
                            strProcessCd = CommValue.PURCHASE_TYPE_VALUE_PURCHASEREQ;
                            strStatusCd = CommValue.APPROVAL_TYPE_VALUE_PENDING;

                            // 구매요청 등록처리
                            // KN_USP_STK_INSERT_GOODSORDERINFO_S00
                            dtOrderReturn = GoodsOrderInfoBlo.RegistryGoodsOrderInfo(strOrderSeq, intRowCnt, strRentCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd
                                , ddlDept.SelectedValue, Session["CompCd"].ToString(), ddlProcessMemNo.SelectedValue, hfProcessDt.Value.Replace("-", ""), intQty, txtRemark.Text, strProcessCd, strStatusCd
                                , strEmergency, string.Empty, 0, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                            if (dtOrderReturn != null)
                            {
                                txtHfGoodsOrderSeq.Text = dtOrderReturn.Rows[0]["OrderSeq"].ToString();
                                strOrderSeq = txtHfGoodsOrderSeq.Text;
                            }

                            // 구매 담당자 미결처리
                            if (!string.IsNullOrEmpty(hfTmpSeq.Value))
                            {
                                // KN_USP_STK_INSERT_GOODSORDERADDON_M00
                                GoodsOrderInfoBlo.RegistryTmpGoodsOrderAddon(strOrderSeq, intRowCnt, Int32.Parse(hfTmpSeq.Value), CommValue.CHOICE_VALUE_NOTCONFIRM, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                            }
                            else
                            {
                                // KN_USP_STK_INSERT_GOODSORDERADDON_M01
                                GoodsOrderInfoBlo.RegistryGoodsOrderAddon(strOrderSeq, intRowCnt, CommValue.CHOICE_VALUE_NOTCONFIRM, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                            }

                            // 양영석 재고 히스토리 처리

                            // 구매 담당자에게 메모 발생처리 승인
                            isMemoSendOk = CommValue.AUTH_VALUE_TRUE;
                        }
                    }

                    if (isMemoSendOk)
                    {
                        // 구매 담당자 조회
                        // KN_USP_STK_SELECT_GOODSORDERADDON_S00
                        DataTable dtChargeReturn = GoodsOrderInfoBlo.SpreadGoodsOrderAddon(strOrderSeq);

                        if (dtChargeReturn != null)
                        {
                            if (dtChargeReturn.Rows.Count > 0)
                            {
                                string strChargeMemNo = dtChargeReturn.Rows[0]["ChargeMemNo"].ToString();
                                string strChargeMemNm = dtChargeReturn.Rows[0]["ChargeMemNm"].ToString();

                                // 구매 담당자 결제 요청 메모 발송
                                MemoWriteUtil.RegistrySendMemo(TextNm["PURCHASE"] + " " + TextNm["REQUEST"], MemoFormLib.MakeOrderChargeForm(strChargeMemNm, strOrderSeq), Session["CompCd"].ToString(), strChargeMemNo, strIP);
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

                if (!string.IsNullOrEmpty(txtHfTmpOrderSeq.Text))
                {
                    // KN_USP_STK_DELETE_TMPGOODSORDERINFO_M02
                    GoodsOrderInfoBlo.RemoveTempGoodsOrderInfo(Int32.Parse(txtHfTmpOrderSeq.Text));
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
                Session["ExchageOrderYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
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

                        sbList.Append(TextNm["ORDER"]);

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