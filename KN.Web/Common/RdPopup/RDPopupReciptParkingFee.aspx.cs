using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupReciptParkingFee : BasePage
    {
        public string strMRDFile = string.Empty;
        public string strDatum0 = string.Empty;
        public string strDatum1 = string.Empty;
        public string strDatum2 = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string P_SEQ = string.Empty;
        public string SEQ = string.Empty;
        public string PAY_DT = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CheckParams())
            {
                var sbWarning = new StringBuilder();

                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                sbWarning.Append("');");
                sbWarning.Append("self.close();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            else
            {
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
                    strMRDFile = "EntireReceiptParkingFee_L.mrd";
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {
                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                    strMRDFile = "EntireReceiptParkingFee_L.mrd";
                }
            }
        }

        private bool CheckParams()
        {
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["Datum0"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["Datum0"]))
                {
                    P_SEQ = Request.Params["Datum0"];
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum1"]))
                {
                    SEQ = Request.Params["Datum1"];
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum2"]))
                {
                    PAY_DT = Request.Params["Datum2"];
                }
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }
    }
}