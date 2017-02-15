using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupElectFeeDetail : BasePage
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
            // Datum0 : 입주자번호
            // Datum1 : 섹션코드
            // Datum2 : 관리비/주차비/임대료
            // Datum3 : 거주년
            // Datum4 : 거주월
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            // 내부IP
            //IPHostEntry host = Dns.Resolve(Dns.GetHostName());
            //string strHostIp = host.AddressList[0].ToString();

            //string strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            //string strHostPort = HttpContext.Current.Request.Url.Port.ToString();
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

                strMRDFile = "UtilFeeElectricity_L.mrd";
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;

                strMRDFile = "UtilFeeElectricity_L.mrd";
            }

            if (Request.Params["Datum0"] != null)
            {
                strDatum0 = Request.Params["Datum0"].ToString();
            }

            if (Request.Params["Datum1"] != null)
            {
                strDatum1 = Request.Params["Datum1"].ToString();
            }

            if (Request.Params["Datum2"] != null)
            {
                strDatum2 = Request.Params["Datum2"].ToString();
            }

            if (Request.Params["Datum3"] != null)
            {
                strDatum3 = Request.Params["Datum3"].ToString();
                strDatum3 = strDatum3.Replace("-", "");
            }

            if (Request.Params["Datum4"] != null)
            {
                strDatum4 = Request.Params["Datum4"].ToString();
                strDatum4 = strDatum4.Replace("-", "");
            }

            if (Request.Params["Datum5"] != null)
            {
                strDatum5 = Request.Params["Datum5"].ToString();
            }

            isReturnOk = CommValue.AUTH_VALUE_TRUE;

            return isReturnOk;
        }
    }
}