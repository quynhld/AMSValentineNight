using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Settlement.Biz;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupHoadonKNConfirm: BasePage
    {
        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string DOC_NO = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
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
                // 내부IP
                string strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
                string strHostPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString();

                if (!strHostPort.Equals(CommValue.PUBLIC_VALUE_PORT))
                {
                    strHostIp = strHostIp + ":" + strHostPort;
                }

                if (strHostIp.Equals(CommValue.PUBLIC_VALUE_DOMAIN.ToLower()) ||
                    strHostIp.Equals(CommValue.PUBLIC_VALUE_TESTHOST.ToLower()) ||
                    strHostIp.Equals(CommValue.PUBLIC_VALUE_TESTDOMAIN.ToLower()))
                {
                    // 공식외부IP
                    NOW_DOMAIN = CommValue.PUBLIC_VALUE_DOMAIN;
                    strMRDFile = "Invoice_KNSFourth_Reprint_L.mrd";
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {
                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                    strMRDFile = "Invoice_KNSFourth_Reprint_L.mrd";
                }
            }
        }

        private bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["Datum0"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["Datum0"].ToString()))
                {
                    DOC_NO = Request.Params["Datum0"].ToString();
                    txtHfDocNo.Text = Request.Params["Datum0"].ToString();
                }

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                isReturnOk = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturnOk;
        }

        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // KN_USP_SET_UPDATE_INVOICEFORTEMP_M00
                InvoiceMngBlo.UpdateInvoiceConfirm(txtHfDocNo.Text);

                StringBuilder sbNoSelection = new StringBuilder();

                sbNoSelection.Append("opener.window.fnLoadData();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RefreshParent", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }
    }
}