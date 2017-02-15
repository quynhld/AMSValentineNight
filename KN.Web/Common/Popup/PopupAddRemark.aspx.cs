using System;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Resident.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupAddRemark : BasePage
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
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Session["RemarkAddOk"] != null)
            {
                if (Session["RemarkAddOk"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    if (Request.Params["CounselCd"] != null &&
                        Request.Params["CounselSeq"] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params["CounselCd"].ToString()) &&
                            !string.IsNullOrEmpty(Request.Params["CounselSeq"].ToString()))
                        {
                            txtHfCounselCd.Text = Request.Params["CounselCd"].ToString();
                            txtHfCounselSeq.Text = Request.Params["CounselSeq"].ToString();
                            Session["RemarkAddOk"] = null;
                            isReturnOk = CommValue.AUTH_VALUE_TRUE;
                        }
                    }
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltRemark.Text = TextNm["REMARK"];
            lnkbtnAdd.Text = TextNm["ADD"];
            lnkbtnCancel.Text = TextNm["CANCEL"];

            lnkbtnAdd.OnClientClick = "javascript:return fnValidateCheck('" + AlertNm["ALERT_INSERT_MEMO"] + "')";
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtHfCounselSeq.Text))
                {
                    object[] objReturn = new object[2];

                    // KN_USP_RES_INSERT_COUNSELADDON_M00
                    objReturn = CounselMngBlo.RegistryCounselAddon(txtHfCounselCd.Text, Int32.Parse(txtHfCounselSeq.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), txtRemark.Text);

                    if (objReturn != null)
                    {
                        Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                        StringBuilder sbInfo = new StringBuilder();

                        sbInfo.Append("window.opener.document.location.href = window.opener.document.URL;");
                        sbInfo.Append("alert('");
                        sbInfo.Append(AlertNm["INFO_REGIST_ISSUE"]);
                        sbInfo.Append("');");
                        sbInfo.Append("self.close();");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AddRemark", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
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
                StringBuilder sbInfo = new StringBuilder();

                sbInfo.Append("alert('");
                sbInfo.Append(AlertNm["INFO_CANCEL"]);
                sbInfo.Append("');");
                sbInfo.Append("self.close();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cancel", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}