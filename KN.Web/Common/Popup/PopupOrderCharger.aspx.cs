using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Manage.Biz;
using KN.Stock.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupOrderCharger : BasePage
    {
        public const string PARAM_DATA1 = "CompTy";

        int intStep = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadDeptData();

                        LoadStaffData();

                        LoadInitData();
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                        sbWarning.Append("');");
                        sbWarning.Append("self.close();");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 매개변수 처리
        /// </summary>
        /// <returns></returns>
        protected bool CheckParams()
        {
            bool isCheckOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA1].ToString()))
                {
                    if (Session["ExchageOrderYn"] != null)
                    {
                        if (Session["ExchageOrderYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                        {
                            isCheckOk = CommValue.AUTH_VALUE_TRUE;
                            Session["ExchageOrderYn"] = null;
                            txtHfParamTxt.Text = Request.Params[PARAM_DATA1].ToString();
                        }
                    }
                }
            }

            return isCheckOk;
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
            // KN_USP_COMM_SELECT_DEPTINFO_S00
            DataTable dtReturn = StaffMngBlo.SpreadDeptInfo();

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
                // KN_USP_COMM_SELECT_STAFFINFO_S00
                DataTable dtReturn = StaffMngBlo.SpreadStaffInfo(lbDeptList.SelectedValue);

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
                if (lvAssignList.Items.Count > 0)
                {
                    StringBuilder sbList = new StringBuilder();

                    sbList.Append("opener.document.getElementById('" + txtHfParamTxt.Text + "').value = " + txtHfTmpSeq.Text + ";");
                    sbList.Append("opener.window.fnOriginData();");
                    sbList.Append("window.close();");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OkWindow", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ErrorWindow", "javascript:alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');", CommValue.AUTH_VALUE_TRUE);
                }
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
                // 기존 임시 정보 삭제
                //MaterialMngBlo.RemoveReleaseChargerInfo();
                int intSeq = 0;
                string strMovePage = HttpContext.Current.Request.Url.ToString();

                if (!string.IsNullOrEmpty(txtHfTmpSeq.Text))
                {
                    intSeq = Int32.Parse(txtHfTmpSeq.Text);
                }

                // KN_USP_STK_DELETE_TEMPCHARGERINFO_M00
                MaterialMngBlo.RemoveTempChargerInfo(intSeq);

                LoadAssignData();
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
                // 임시 정보 삭제 처리
                int intSeq = 0;

                if (!string.IsNullOrEmpty(txtHfTmpSeq.Text))
                {
                    intSeq = Int32.Parse(txtHfTmpSeq.Text);
                }

                // KN_USP_STK_DELETE_TEMPCHARGERINFO_M00
                MaterialMngBlo.RemoveTempChargerInfo(intSeq);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CloseWindow", "javascript:window.close();", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}