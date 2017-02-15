using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Lib;

using KN.Manage.Biz;
using KN.Stock.Biz;

namespace KN.Web.Stock.Order
{
    public partial class GoodsOrderChargerList : BasePage
    {
        int intStep = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    InitControls();

                    LoadDeptData();

                    LoadStaffData();

                    LoadInitData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void InitControls()
        {
            ltDeptGrp.Text = TextNm["DEPT"];
            ltChargerGrp.Text = TextNm["CHARGER"];
            ltAssignment.Text = TextNm["ASSIGNTASK"];

            ltDept.Text = TextNm["DEPT"];
            ltCharger.Text = TextNm["CHARGER"];
            ltException.Text = TextNm["EXCEPT"];

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_REGIST_ITEM"] + "');";
            lnkbtnDelete.Text = TextNm["ENTIRE"] + TextNm["DELETE"];
            lnkbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";
        }

        /// <summary>
        /// 부서 데이터 로드
        /// </summary>
        protected void LoadDeptData()
        {
            //DataTable dtReturn = StaffMngBlo.SpreadDeptInfo();
            DataTable dtReturn = null;
            if (dtReturn != null)
            {
                lbDeptList.DataSource = dtReturn;
                lbDeptList.DataTextField = "DeptNm";
                lbDeptList.DataValueField = "DeptCd";
                lbDeptList.DataBind();
            }
        }

        /// <summary>
        /// 직원 데이터 로드
        /// </summary>
        protected void LoadStaffData()
        {
            if (!string.IsNullOrEmpty(lbDeptList.SelectedValue))
            {
                //DataTable dtReturn = StaffMngBlo.SpreadStaffInfo(lbDeptList.SelectedValue);
                DataTable dtReturn = null;

                if (dtReturn != null)
                {
                    lbChagerList.DataSource = dtReturn;
                    lbChagerList.DataTextField = "MemNm";
                    lbChagerList.DataValueField = "MemNo";
                    lbChagerList.DataBind();
                }
            }
        }

        /// <summary>
        /// 기존 목록 로딩
        /// </summary>
        protected void LoadInitData()
        {
            // KN_USP_STK_INSERT_TEMPCHARGERINFO_S02
            DataTable dtTmpSeqReturn = MaterialMngBlo.RegistryTempChargerFromGoodsOrderCharger();
            
            if (dtTmpSeqReturn != null)
            {
                if (dtTmpSeqReturn.Rows.Count > 0)
                {
                    txtHfTmpSeq.Text = dtTmpSeqReturn.Rows[0]["TempSeq"].ToString();
                }
            }

            LoadAssignData();
        }

        /// <summary>
        /// 배정 목록 조회
        /// </summary>
        protected void LoadAssignData()
        {
            // KN_USP_STK_SELECT_TEMPCHARGERINFO_S00
            DataTable dtTmpListReturn = MaterialMngBlo.SpreadTempChargerInfo(Int32.Parse(txtHfTmpSeq.Text));

            lvAssignList.DataSource = dtTmpListReturn;
            lvAssignList.DataBind();
        }

        /// <summary>
        /// 각 컨트롤 초기화
        /// </summary>
        protected void ResetControls()
        {
            lbDeptList.Items.Clear();
            lbChagerList.Items.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvAssignList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                intStep++;

                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                Literal ltStep = (Literal)iTem.FindControl("ltStep");
                ltStep.Text = TextNm["STEP"] + "&nbsp;" + intStep.ToString();

                if (!string.IsNullOrEmpty(drView["DeptNm"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtDeptNm")).Text = drView["DeptNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DeptCd"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfDeptCd")).Text = drView["DeptCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ChargeMemNm"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtMemNm")).Text = drView["ChargeMemNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ChargeMemNo"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfMemNo")).Text = drView["ChargeMemNo"].ToString();
                }
            }
        }

        /// <summary>
        /// 부서 데이터 선택 변경에 따른 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbDeptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadStaffData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lbChagerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool isExist = CommValue.AUTH_VALUE_FALSE;

                if (!string.IsNullOrEmpty(lbChagerList.SelectedValue))
                {
                    for (int intTmpI = 0; intTmpI < lvAssignList.Items.Count; intTmpI++)
                    {
                        TextBox txtHfMemNo = (TextBox)lvAssignList.Items[intTmpI].FindControl("txtHfMemNo");

                        if (txtHfMemNo.Text.Equals(lbChagerList.SelectedValue))
                        {
                            isExist = CommValue.AUTH_VALUE_TRUE;
                        }
                    }

                    if (!isExist)
                    {
                        int intTmpSeq = 0;
                        string strIP = Request.ServerVariables["REMOTE_ADDR"];

                        if (!string.IsNullOrEmpty(txtHfTmpSeq.Text))
                        {
                            intTmpSeq = Int32.Parse(txtHfTmpSeq.Text);
                        }

                        // KN_USP_STK_INSERT_TEMPCHARGERINFO_S00
                        DataTable dtTmpSeqReturn = MaterialMngBlo.RegistryTempChargerInfo(intTmpSeq, lbChagerList.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                        if (dtTmpSeqReturn != null)
                        {
                            txtHfTmpSeq.Text = dtTmpSeqReturn.Rows[0]["TempSeq"].ToString();

                            LoadAssignData();
                        }
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningExistGrpCd", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
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

                // 기존 정보 삭제
                // KN_USP_STK_DELETE_GOODSORDERCHARGERINFO_M00
                MaterialMngBlo.RemoveGoodsOrderChargerInfo();

                string strIP = Request.ServerVariables["REMOTE_ADDR"];
                int intCnt = 1;

                // 신규 정보 등록
                for (int intTmpI = 0; intTmpI < lvAssignList.Items.Count; intTmpI++)
                {
                    CheckBox chkException = (CheckBox)lvAssignList.Items[intTmpI].FindControl("chkList");

                    if (!chkException.Checked)
                    {
                        TextBox txtHfMemNo = (TextBox)lvAssignList.Items[intTmpI].FindControl("txtHfMemNo");

                        // KN_USP_STK_INSERT_GOODSORDERCHARGERINFO_M00
                        MaterialMngBlo.RegistryGoodsOrderChargerInfo(intCnt, txtHfMemNo.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                        intCnt++;
                    }
                }

                // 임시 정보 삭제 처리
                int intSeq = 0;

                if (!string.IsNullOrEmpty(txtHfTmpSeq.Text))
                {
                    intSeq = Int32.Parse(txtHfTmpSeq.Text);
                }

                // KN_USP_STK_DELETE_TEMPCHARGERINFO_M00
                MaterialMngBlo.RemoveTempChargerInfo(intSeq);

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
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

                // 기존 정보 삭제
                // KN_USP_STK_DELETE_GOODSORDERCHARGERINFO_M00
                MaterialMngBlo.RemoveGoodsOrderChargerInfo();

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
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

                // 임시 정보 삭제 처리
                int intSeq = 0;

                if (!string.IsNullOrEmpty(txtHfTmpSeq.Text))
                {
                    intSeq = Int32.Parse(txtHfTmpSeq.Text);
                }

                // KN_USP_STK_DELETE_TEMPCHARGERINFO_M00
                MaterialMngBlo.RemoveTempChargerInfo(intSeq);

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}