﻿using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupTenantList : BasePage
    {
        public string strMRDFile = string.Empty;
        public string strShop = string.Empty;
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

            if (Request.Params["Datum0"] != null)
            {
                strDatum0 = Request.Params["Datum0"].ToString();
                //hfDatum0.Value = strDatum0;

                string strTemp = string.Empty;
                if (CommValue.RENTAL_VALUE_APTSHOP.Equals(strDatum0))
                {
                    strShop = "Shop";
                }
            }

            if (Request.Params["Datum1"] != null)
            {
                strDatum1 = Request.Params["Datum1"].ToString();
                //hfDatum1.Value = strDatum1;
            }

            if (Request.Params["Datum2"] != null)
            {
                strDatum2 = Request.Params["Datum2"].ToString();
                //hfDatum2.Value = strDatum2;
            }

            if (Request.Params["Datum3"] != null)
            {
                strDatum3 = Request.Params["Datum3"].ToString();
                //hfDatum3.Value = strDatum2;
            }

            if (Request.Params["Datum4"] != null)
            {
                strDatum4 = Request.Params["Datum4"].ToString();
                strDatum4 = strDatum4.Replace("-", "");
                //hfDatum3.Value = strDatum3;
            }

            if (Request.Params["Datum5"] != null)
            {
                strDatum5 = Request.Params["Datum5"].ToString();
                strDatum5 = strDatum5.Replace("-", "");
                //hfDatum3.Value = strDatum3;
            }

            if (strHostIp.Equals(CommValue.PUBLIC_VALUE_DOMAIN.ToLower()) ||
                strHostIp.Equals(CommValue.PUBLIC_VALUE_TESTHOST.ToLower()) ||
                strHostIp.Equals(CommValue.PUBLIC_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식외부IP
                NOW_DOMAIN = CommValue.PUBLIC_VALUE_DOMAIN;

                if (CommValue.LANG_VALUE_KOREAN.Equals(Session["LangCd"].ToString()))
                {
                    strMRDFile = "TenantListKOR_L.mrd";
                }
                else
                {
                    strMRDFile = "TenantListENG_L.mrd";
                }
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;

                if (CommValue.LANG_VALUE_KOREAN.Equals(Session["LangCd"].ToString()))
                {
                    strMRDFile = "TenantListKOR_L.mrd";
                }
                else
                {
                    strMRDFile = "TenantListENG_L.mrd";
                }
            }

            //Session["ReportingOk"] = null;
            isReturnOk = CommValue.AUTH_VALUE_TRUE;

            return isReturnOk;
        }
    }
}