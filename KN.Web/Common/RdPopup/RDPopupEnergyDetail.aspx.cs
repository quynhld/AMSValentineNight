using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupEnergyDetail : BasePage
    {
        public string strMRDFile = string.Empty;
        public string USER_SEQ = string.Empty;
        public string RENT_CD = string.Empty;
        public string ROOM_NO = string.Empty;
        public string YEAR_MM = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string MEM_IP = string.Empty;
        public string MEM_NO = string.Empty;

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
                MEM_IP = Request.ServerVariables["REMOTE_ADDR"];
                MEM_NO = Session["LangCd"].ToString();
                //MngAccountsBlo.RegistryHoadonFirstPrintList(Session["MemNo"].ToString(), strIp);
            }
        }

        private bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

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
                strMRDFile = "EnergyDetail_L.mrd";
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                strMRDFile = "EnergyDetail_L.mrd";
            }

            if (Request.Params["Datum1"] != null)
            {
                USER_SEQ = Request.Params["Datum1"].ToString();
                //hfDatum1.Value = strDatum1;
            }

            if (Request.Params["Datum2"] != null)
            {
                RENT_CD = Request.Params["Datum2"].ToString();
                //hfDatum2.Value = strDatum2;
            }

            if (Request.Params["Datum3"] != null)
            {
                ROOM_NO = Request.Params["Datum3"].ToString();
                //hfDatum3.Value = strDatum3;
            }

            if (Request.Params["Datum4"] != null)
            {
                YEAR_MM = Request.Params["Datum4"].ToString();
                //hfDatum4.Value = strDatum4;
            }

            if (Session["EnergyOk"] != null)
            {
                if (Session["EnergyOk"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    Session["EnergyOk"] = null;
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
    }
}