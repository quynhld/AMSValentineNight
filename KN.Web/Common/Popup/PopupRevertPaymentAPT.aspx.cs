using System;
using System.Text;
using System.Web.UI;
using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Manage.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupRevertPaymentAPT : BasePage
    {
        public string strDatum0 = string.Empty;
        public string strDatum1 = string.Empty;
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
            var isReturn = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["PSeq"] != null )
            {
                if (!string.IsNullOrEmpty(Request.Params["PSeq"]))
                {
                    txtHfPSeq.Text = Request.Params["PSeq"];
                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
                if (!string.IsNullOrEmpty(Request.Params["Seq"]))
                {
                    txtSeq.Text = Request.Params["Seq"];
                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
            }
            return isReturn;
        }

        protected void InitControls()
        {
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnDelete.OnClientClick = "javascript:return fnPopupConfirm('" + AlertNm["ALERT_SELECT_CATEGORY"] + "','" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var pSeq = txtHfPSeq.Text;
                var seq = Int32.Parse(txtSeq.Text);
                var invoiceNo = txtInvoice.Text;
                var payDate = txtPayDay.Text.Replace("-", "");
                var amount = -double.Parse(txtAmount.Text == "" ? "0.00" : txtAmount.Text.Replace(",", ""));
                var memNo = Session["MemNo"].ToString();
                var ip = Session["UserIP"].ToString();
                var objReturn = MngPaymentBlo.InsertRevertPaymentApt(pSeq, seq, payDate, amount, memNo, ip);
                if (objReturn != null && !string.IsNullOrEmpty(objReturn.Rows[0]["Seq"].ToString()))
                {
                    var sbAlert = new StringBuilder();
                    sbAlert.Append("Revert Money (VND) : " + TextLib.MakeVietIntNo((amount).ToString("###,##0")) + "\\n");
                    var sbResult = new StringBuilder();
                    sbResult.Append("alert('" + sbAlert + "');");
                    sbResult.Append("ReLoadData('" + invoiceNo + "');"); 
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ManagememntFee", sbResult.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Check Infomation')", CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            var sbInfo = new StringBuilder();

            sbInfo.Append("alert('");
            sbInfo.Append(AlertNm["INFO_CANCEL"]);
            sbInfo.Append("');");
            sbInfo.Append("returnValue='CANCEL';");
            sbInfo.Append("self.close();");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cancel", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
    }
}