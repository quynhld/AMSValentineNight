using System;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Resident.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupSalesContractDel : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (!CheckParams())
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                        sbWarning.Append("');");
                        sbWarning.Append("self.close();");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    else
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
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            // PreCondition
            // 1. 계약서 보기 페이지를 거쳐서 넘어오지 않을 경우 삭제를 할 수 없음
            // 2. 인자값이 제대로 넘어오지 않을 경우 삭제할 수 없음.
            if (Session["DelContractYn"] != null)
            {
                // 계약서 보기 페이지를 거쳐서 넘어오지 않을 경우 삭제를 할 수 없음
                if (Session["DelContractYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    // 인자값이 제대로 넘어오지 않을 경우 삭제할 수 없음.
                    if (Request.Params["RentCd"] != null && Request.Params["RentSeq"] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params["RentCd"].ToString()) && !string.IsNullOrEmpty(Request.Params["RentSeq"].ToString()))
                        {
                            txtHfRentCd.Text = Request.Params["RentCd"].ToString();
                            txtHfRentSeq.Text = Request.Params["RentSeq"].ToString();
                            Session["DelContractYn"] = null;
                            isReturn = CommValue.AUTH_VALUE_TRUE;
                        }
                        else
                        {
                            isReturn = CommValue.AUTH_VALUE_FALSE;
                        }
                    }
                    else
                    {
                        isReturn = CommValue.AUTH_VALUE_FALSE;
                    }
                }
                else
                {
                    isReturn = CommValue.AUTH_VALUE_FALSE;
                }
            }
            else
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturn;
        }

        protected void InitControls()
        {
            ltDeleteTitle.Text = TextNm["CONTRACT"] + " " + TextNm["DELETE"];
            ltCategory.Text = TextNm["CATEGORY"];
            ltContents.Text = TextNm["CONTENTS"];

            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnCancel.Text = TextNm["CANCEL"];

            CommCdDdlUtil.MakeEtcSubCdDdlTitle(ddlCategory, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_CONTDEL);
            lnkbtnDelete.OnClientClick = "javascript:return fnPopupConfirm('" + AlertNm["ALERT_SELECT_CATEGORY"] + "','" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                object[] objReturn = new object[2];
                string strIp = Request.ServerVariables["REMOTE_ADDR"];

                // KN_USP_RES_DELETE_SALESINFO_M00
                objReturn = ContractMngBlo.RemoveSaleInfo(txtHfRentCd.Text, Int32.Parse(txtHfRentSeq.Text), txtDelReason.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                if (objReturn != null)
                {
                    StringBuilder sbInfo = new StringBuilder();

                    sbInfo.Append("alert('");
                    sbInfo.Append(AlertNm["INFO_DELETE_CONT"]);
                    sbInfo.Append("');");
                    sbInfo.Append("returnValue='DELETE';");
                    sbInfo.Append("self.close();");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteComplete", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            StringBuilder sbInfo = new StringBuilder();

            sbInfo.Append("alert('");
            sbInfo.Append(AlertNm["INFO_CANCEL"]);
            sbInfo.Append("');");
            sbInfo.Append("returnValue='CANCEL';");
            sbInfo.Append("self.close();");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cancel", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
    }
}