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
    public partial class RDPopupHoadonCNIncrease : BasePage
    {
        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string SERIAL_NO = string.Empty;
        public string DOC_NO = string.Empty;
        public string AMOUNT = string.Empty;
        public string USER_SEQ = string.Empty;
        public string BILL_CD = string.Empty;

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
                    strMRDFile = "Invoice_CNSThird_Increase_LP.mrd";
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {
                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                    strMRDFile = "Invoice_CNSThird_Increase_LP.mrd";
                }
            }
        }

        private bool CheckParams()
        {
            var isReturnOk = false;

            if (Request.Params["Datum0"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["Datum0"]))
                {
                    DOC_NO = Request.Params["Datum0"];
                    txtHfDocNo.Text = Request.Params["Datum0"];
                    isReturnOk = true;
                }

                if (!string.IsNullOrEmpty(Request.Params["Datum1"]))
                {
                    AMOUNT = Request.Params["Datum1"];
                    isReturnOk = true;
                }
            }

            return isReturnOk;
        }

        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }
    }
}