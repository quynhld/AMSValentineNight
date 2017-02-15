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
    public partial class RDPopupHoadonCNRePreview : BasePage
    {
        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string SERIAL_NO = string.Empty;
        public string DOC_NO = string.Empty;
        public string PAY_DT = string.Empty;
        public string USER_SEQ = string.Empty;
        public string BILL_CD = string.Empty;

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
                if (Request.Params["Datum0"] != null)
                {
                    if (!string.IsNullOrEmpty(Request.Params["Datum0"]))
                    {
                        DOC_NO = Request.Params["Datum0"];
                        txtHfDocNo.Text = Request.Params["Datum0"];
                    }                                    
                }

                // 내부IP
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

                    if (BILL_CD.Equals("0004"))
                    {
                       // strMRDFile = "Invoice_ParkingFee_Preview_L.mrd";
                    }
                    else
                    {
                        strMRDFile = "Invoice_CNSThird_Reprint_Preview_L.mrd";
                    }
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {
                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;

                    if (BILL_CD.Equals("0004"))
                    {
                       // strMRDFile = "Invoice_ParkingFee_Preview_L.mrd";
                    }
                    else
                    {
                        strMRDFile = "Invoice_CNSThird_Reprint_Preview_L.mrd";
                    }
                }
            }
        }

        private bool CheckParams()
        {
            var isReturnOk = !string.IsNullOrEmpty(Request.Params["Datum0"]);

            return isReturnOk;
        }
    }
}