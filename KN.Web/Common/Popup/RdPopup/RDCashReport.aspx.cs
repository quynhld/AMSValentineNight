using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;

namespace KN.Web.Common.RdPopup
{
    public partial class RDCashReport : BasePage
    {
        public string SearchYear = string.Empty;
        public string SearchMonth = string.Empty;
        public string SearchDay = string.Empty;
        public string RentCd = string.Empty;
        public string FeeTy = string.Empty;
        public string DateType = string.Empty;
        public string strMRDFile = string.Empty;
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

            if (Request.Params["SearchYear"] != null)
            {
                SearchYear = Request.Params["SearchYear"];
                //hfDatum0.Value = strDatum0;

                if (Request.Params["SearchMonth"] != null)
                {
                    SearchMonth = Request.Params["SearchMonth"];
                    //hfDatum1.Value = strDatum1;

                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    isReturnOk = CommValue.AUTH_VALUE_FALSE;
                }
                if (Request.Params["SearchDay"] != null)
                {
                    SearchDay = Request.Params["SearchDay"];
                    //hfDatum1.Value = strDatum1;

                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    isReturnOk = CommValue.AUTH_VALUE_FALSE;
                }
                if (Request.Params["RentCd"] != null)
                {
                    RentCd = Request.Params["RentCd"];
                    //hfDatum1.Value = strDatum1;

                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    isReturnOk = CommValue.AUTH_VALUE_FALSE;
                }
                if (Request.Params["FeeTy"] != null)
                {
                    FeeTy = Request.Params["FeeTy"];
                    //hfDatum1.Value = strDatum1;

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

            if (Request.Params["DateType"] != null)
            {
                DateType = Request.Params["DateType"];
            }

            if (strHostIp.Equals(CommValue.PUBLIC_VALUE_DOMAIN.ToLower()) ||
                strHostIp.Equals(CommValue.PUBLIC_VALUE_TESTHOST.ToLower()) ||
                strHostIp.Equals(CommValue.PUBLIC_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식외부IP
                NOW_DOMAIN = CommValue.PUBLIC_VALUE_DOMAIN;

                switch (DateType)
                {
                    case "M": strMRDFile = "Cash_Report.mrd";
                        break;
                    case "Y": strMRDFile = "Cash_Report_Yearly.mrd";
                        break;
                    case "D": strMRDFile = "Cash_Report_Daily.mrd";
                        break;
                }
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;

                switch (DateType)
                {
                    case "M": strMRDFile = "Cash_Report.mrd";
                        break;
                    case "Y": strMRDFile = "Cash_Report_Yearly.mrd";
                        break;
                    case "D": strMRDFile = "Cash_Report_Daily.mrd";
                        break;
                }
            }

            return isReturnOk;
        }
    }
}