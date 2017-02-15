using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using System.Text;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupTrialBalanceReport : BasePage
    {

        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string DOC_NO = string.Empty;
        public string strDatum0 = string.Empty;
        public string strDatum1 = string.Empty;
        public string strDatum2 = string.Empty;
        public string strDatum3 = string.Empty;

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
                    strMRDFile = "Trial_Balance_Report_L.mrd";
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {
                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                    strMRDFile = "Trial_Balance_Report_L.mrd";
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
                    strDatum0 = Request.Params["Datum0"];
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum1"]))
                {
                    strDatum1 = Request.Params["Datum1"];
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum2"]))
                {
                    strDatum2 = Request.Params["Datum2"];
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum3"]))
                {
                    strDatum3 = Request.Params["Datum3"];                    
                }              

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }
    }
}