using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Settlement.Biz;
using KN.Common.Method.Lib;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupReciptPaymentAPT : BasePage
    {
        public string strMRDFile = string.Empty;
        public string strDatum0 = string.Empty;
        public string strDatum1 = string.Empty;
        public string strDatum2 = string.Empty;
        public string NOW_DOMAIN = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
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
            }
        }

        private bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            // 내부IP
            string strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
            string strHostPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];

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
                strMRDFile = "EntireReceiptPaymentCNNewAPT_L.mrd";
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                strMRDFile = "EntireReceiptPaymentCNNewAPT_L.mrd";
            }

            if (Request.Params["Datum0"] != null)
            {
                strDatum0 = Request.Params["Datum0"];

                if (Request.Params["Datum1"] != null)
                {
                    strDatum1 = Request.Params["Datum1"];

                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    isReturnOk = CommValue.AUTH_VALUE_FALSE;
                }
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
                AuthCheckLib.CheckSession();
                // KN_USP_SET_INSERT_INVOICEFORTEMP_S02
                //InvoiceMngBlo.InsertTempHoadonForConfirmAPT(txtHfDocNo.Text);                   
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }
    }
}