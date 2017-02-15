using System;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

namespace KN.Web.Common.Popup
{
    public partial class PopupTmpExchangeRate : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (InitParams())
                    {
                        InitControl();
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

        private bool InitParams()
        {
            bool isReturn = CommValue.AUTH_VALUE_TRUE;

            if (Request.Params["NowRate"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["NowRate"]))
                {
                    ltTitlePreExchangeRate.Text = Request.Params["NowRate"].ToString();

                    if (Request.Params["ReturnBox1"] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params["ReturnBox1"]))
                        {
                            hfReturnBox1.Value = Request.Params["ReturnBox1"].ToString();

                            if (Request.Params["ReturnBox2"] != null)
                            {
                                if (!string.IsNullOrEmpty(Request.Params["ReturnBox2"]))
                                {
                                    hfReturnBox2.Value = Request.Params["ReturnBox2"].ToString();
                                }
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
            }
            else
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturn;
        }

        private void InitControl()
        {
            ltTitlePreTitle.Text = TextNm["NOW"];
            ltTitlePostTitle.Text = TextNm["AFTER"];
            ltTitleUnit.Text = TextNm["DONG"];
            lnkbtnChange.Text = TextNm["MODIFY"];
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnChange.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnCancel.OnClientClick = "javascript:return fnWindowsClose();";
            imgbtnProcess.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            txtPostExchageRate.Attributes["onkeypress"] = "javascript:fnApplyChange('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
        }

        protected void lnkbtnChange_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void imgbtnProcess_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}