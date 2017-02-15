using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupReceivableFromTenantAPT : BasePage
    {
        public string strMRDFile = string.Empty;
        public string strDatum0 = string.Empty;
        public string strDatum1 = string.Empty;
        public string strDatum2 = string.Empty;
        public string strDatum3 = string.Empty;
        public string strDatum4 = string.Empty;
        public string strDatum5 = string.Empty;
        public string strDatum6 = string.Empty;
        public string strDatum7 = string.Empty;
        public string strDatum8 = string.Empty;
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
            var isReturnOk = false;

            // 내부IP
            var strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
            var strHostPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];

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

                if (CommValue.LANG_VALUE_KOREAN.Equals(Session["LangCd"].ToString()))
                {
                    strMRDFile = "ReceivableFromTenantAPT.mrd";
                }
                else
                {
                    strMRDFile = "ReceivableFromTenantAPT.mrd";
                }
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;

                if (CommValue.LANG_VALUE_KOREAN.Equals(Session["LangCd"].ToString()))
                {
                    strMRDFile = "ReceivableFromTenantAPT.mrd";
                }
                else
                {
                    strMRDFile = "ReceivableFromTenantAPT.mrd";
                }
            }

            if (Request.Params["Datum0"] != null)
            {
                strDatum0 = Request.Params["Datum0"];
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            if (Request.Params["Datum1"] != null)
            {
                strDatum1 = Request.Params["Datum1"];
            }

            if (Request.Params["Datum2"] != null)
            {
                strDatum2 = Request.Params["Datum2"];
            }

            //if (Request.Params["Datum3"] != null)
            //{
            //    strDatum3 = Request.Params["Datum3"].ToString();
            //}

            //if (Request.Params["Datum4"] != null)
            //{
            //    strDatum4 = Request.Params["Datum4"].ToString();
            //}

            //if (Request.Params["Datum5"] != null)
            //{
            //    strDatum5 = Request.Params["Datum5"].ToString();
            //}
            //if (Request.Params["Datum6"] != null)
            //{
            //    strDatum6 = Request.Params["Datum6"].ToString();
            //}
            //if (Request.Params["Datum7"] != null)
            //{
            //    strDatum7 = Request.Params["Datum7"].ToString();
            //}
            //if (Request.Params["Datum8"] != null)
            //{
            //    strDatum8 = Request.Params["Datum8"].ToString();
            //}

            return isReturnOk;
        }
    }
}