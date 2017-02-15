using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupDayAccountInfo : BasePage
    {
        public string strMRDFile = string.Empty;
        public string strDatum0 = string.Empty;
        public string strDatum1 = string.Empty;
        public string strDatum2 = string.Empty;
        public string strDatum3 = string.Empty;
        public string strDatum4 = string.Empty;
        public string strDatum5 = string.Empty;
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


            if (Request.Params["Datum0"] != null)
            {
                strDatum0 = Request.Params["Datum0"];
                strDatum0 = strDatum0.Replace("-", "");
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            if (Request.Params["Datum1"] != null)
            {
                strDatum1 = Request.Params["Datum1"];
                strDatum1 = strDatum1.Replace("-", "");
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            if (Request.Params["Datum2"] != null)
            {
                strDatum2 = Request.Params["Datum2"];
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            if (Request.Params["Datum3"] != null)
            {
                strDatum3 = Request.Params["Datum3"];
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            if (Request.Params["Datum4"] != null)
            {
                strDatum4 = Request.Params["Datum4"];
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            if (Request.Params["Datum5"] != null)
            {
                strDatum5 = Request.Params["Datum5"];
            }

            var strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
            var strHostPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString();

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

                if(strDatum3.Equals("9000"))
                {
                    strMRDFile = strDatum5.Equals("01") ? "DayAccountInfoENG_L.mrd" : "DayAccountInfoFromAugustENG_L.mrd";
                }
                else
                {
                    strMRDFile = "DayAccountInfoTowerENG_L.mrd";
                }
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;

                if (strDatum3.Equals("9000"))
                {
                    strMRDFile = strDatum5.Equals("01") ? "DayAccountInfoENG_L.mrd" : "DayAccountInfoFromAugustENG_L.mrd";
                }
                else
                {
                    strMRDFile = "DayAccountInfoTowerENG_L.mrd";
                }
            }
            
            return isReturnOk;
        }
    }
}