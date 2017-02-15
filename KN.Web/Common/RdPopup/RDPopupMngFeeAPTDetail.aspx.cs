using System;
using System.Text;
using System.Web;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Settlement.Biz;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupMngFeeAPTDetail : BasePage
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
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;
            //string strHostIp = "http://" + HttpContext.Current.Request.Url.Authority.ToString().ToLower();

            // 내부IP
            //IPHostEntry host = Dns.Resolve(Dns.GetHostName());
            //string strHostIp = host.AddressList[0].ToString();

            //string strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["SERVER_NAME"].ToString();
            //string strHostPort = HttpContext.Current.Request.Url.Port.ToString();
            string strHostIp = "http://" + HttpContext.Current.Request.ServerVariables["LOCAL_ADDR"];
            string strHostPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];

            if (Request.Params["Datum0"] != null)
            {
                strDatum0 = Request.Params["Datum0"].ToString();
                //hfDatum0.Value = strDatum0;
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
                //hfDatum3.Value = strDatum3;
            }

            if (Request.Params["Datum4"] != null)
            {
                strDatum4 = Request.Params["Datum4"].ToString();
                //hfDatum4.Value = strDatum4;
            }

            if (Request.Params["Datum5"] != null)
            {
                strDatum5 = Request.Params["Datum5"];
            }

            if (strHostIp.Equals(CommValue.PUBLIC_VALUE_DOMAIN.ToLower()) ||
                strHostIp.Equals(CommValue.PUBLIC_VALUE_TESTHOST.ToLower()) ||
                strHostIp.Equals(CommValue.PUBLIC_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식외부IP
                NOW_DOMAIN = CommValue.PUBLIC_VALUE_DOMAIN;
                strMRDFile = "DebitNoteMngFeeAPTNew_L.mrd";

                //if (strDatum1.Equals(CommValue.RENTAL_VALUE_APT) ||
                //    strDatum1.Equals(CommValue.RENTAL_VALUE_APTA) ||
                //    strDatum1.Equals(CommValue.RENTAL_VALUE_APTB))
                //{
                //    strMRDFile = "DebitNoteMngFeeAPT_L.mrd";
                //}
                //else if (strDatum1.Equals(CommValue.RENTAL_VALUE_APTSHOP) ||
                //         strDatum1.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                //         strDatum1.Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                //{
                //    strMRDFile = "DebitNoteMngFeeAPTRetail_L.mrd";
                //}
                //else if (strDatum1.Equals(CommValue.RENTAL_VALUE_OFFICE))
                //{
                //    strMRDFile = "DebitNoteMngFeeTowerOffice_L.mrd";
                //}
                //else if (strDatum1.Equals(CommValue.RENTAL_VALUE_SHOP))
                //{
                //    strMRDFile = "DebitNoteMngFeeTowerRetail_L.mrd";
                //}
            }
            else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
            {
                // 공식내부IP
                NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                strMRDFile = "DebitNoteMngFeeAPTNew_L.mrd";

                //if (strDatum1.Equals(CommValue.RENTAL_VALUE_APT) ||
                //    strDatum1.Equals(CommValue.RENTAL_VALUE_APTA) ||
                //    strDatum1.Equals(CommValue.RENTAL_VALUE_APTB))
                //{
                //    strMRDFile = "DebitNoteMngFeeAPT_L.mrd";
                //}
                //else if (strDatum1.Equals(CommValue.RENTAL_VALUE_APTSHOP) ||
                //         strDatum1.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                //         strDatum1.Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                //{
                    
                //}
                //else if (strDatum1.Equals(CommValue.RENTAL_VALUE_OFFICE))
                //{
                //    strMRDFile = "DebitNoteMngFeeTowerOffice_L.mrd";
                //}
                //else if (strDatum1.Equals(CommValue.RENTAL_VALUE_SHOP))
                //{
                //    strMRDFile = "DebitNoteMngFeeTowerRetail_L.mrd";
                //}
            }

            //Session["ReportingOk"] = null;            

            return isReturnOk;
        }

        protected void imgUpdatePrint_Click(object sender, ImageClickEventArgs e)
        {
            InvoiceMngBlo.UpdatedAptDebitPrinted(txtHfPrintNo.Text);
            //LoadData();
        }
    }
}