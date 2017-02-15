using System;
using System.Data;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Config.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupLogView : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (CheckParams())
                {
                    InitControls();

                    LoadData();
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
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["LogSeq"] != null &&
                Request.Params["ErrTy"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["LogSeq"].ToString()) &&
                    !string.IsNullOrEmpty(Request.Params["ErrTy"].ToString()))
                {
                    txtHfLogSeq.Text = Request.Params["LogSeq"].ToString();
                    txtHfErrTy.Text = Request.Params["ErrTy"].ToString();
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            lblDate.Text = TextNm["REGISTDATE"];
            lblMachId.Text = TextNm["SERVER"];
            lblUrl.Text = TextNm["URL"];
            lblErrPos.Text = TextNm["LOCATION"];
            lblErrTxt.Text = TextNm["CONTENTS"];

            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnCancel.Text = TextNm["CANCEL"];

            lnkbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
        }

        protected void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_COMM_SELECT_LOG_S01
            dtReturn = LogInfoBlo.SpreadLogDetailInfo(Int32.Parse(txtHfLogSeq.Text), txtHfErrTy.Text);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    lblInsDate.Text = dtReturn.Rows[0]["InsDate"].ToString();
                    lblInsLogSeq.Text = TextNm["NO"] + dtReturn.Rows[0]["LogSeq"].ToString();
                    lblInsMachId.Text = dtReturn.Rows[0]["MachId"].ToString();
                    lblInsUrl.Text = dtReturn.Rows[0]["ErrUrl"].ToString();
                    lblInsErrPos.Text = dtReturn.Rows[0]["ErrPos"].ToString();
                    lblInsErrTxt.Text = dtReturn.Rows[0]["ErrTxt"].ToString();
                    lblInsErrTy.Text = dtReturn.Rows[0]["ErrTy"].ToString();
                }
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            int intLogSeq = Int32.Parse(txtHfLogSeq.Text);
            string strErrTy = txtHfErrTy.Text;

            // KN_USP_COMM_DELETE_LOG_M00
            LogInfoBlo.RemoveLogInfo(intLogSeq, strErrTy);

            StringBuilder sbInfo = new StringBuilder();

            sbInfo.Append("window.opener.document.location.href = window.opener.document.URL;");
            sbInfo.Append("alert('");
            sbInfo.Append(AlertNm["INFO_DELETE_ITEM"]);
            sbInfo.Append("');");
            sbInfo.Append("self.close();");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteItems", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sbInfo = new StringBuilder();

                sbInfo.Append("alert('");
                sbInfo.Append(AlertNm["INFO_CLOSE"]);
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