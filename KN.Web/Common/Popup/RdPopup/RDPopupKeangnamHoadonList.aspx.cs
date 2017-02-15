using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Manage.Biz;

using KN.Settlement.Biz;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupKeangnamHoadonList : BasePage
    {
        public string strMRDFile = string.Empty;
        public string HOADON_NO = string.Empty;
        public string NOW_DOMAIN = string.Empty;

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
        }

        private bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            // 내부IP
            string strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"].ToString();
            string strHostPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"].ToString();
            string strNowDate = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

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
                strMRDFile = "Invoice_KNSFourth_L.mrd";
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                strMRDFile = "Invoice_KNSFourth_L.mrd";
            }

            if (Request.Params["Datum1"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["Datum1"].ToString()))
                {
                    HOADON_NO = Request.Params["Datum1"].ToString();
                }
            }

            Session["HoadonOk"] = null;

            isReturnOk = CommValue.AUTH_VALUE_TRUE;

            return isReturnOk;
        }

        protected void imgbtnPrintOut_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // KN_USP_MNG_INSERT_HOADONADDON_M00
                //InvoiceMngBlo.InsertHoadonAddon(Session["CompCd"].ToString(), Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), SEARCH_DATA);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}