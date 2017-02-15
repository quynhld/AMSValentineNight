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
    public partial class RDPopupHoaDonParkingMergeAPTPreview : BasePage
    {
        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string SERIAL_NO = string.Empty;
        public string DOC_NO = string.Empty;
        public string PAY_DT = string.Empty;
        public string USER_SEQ = string.Empty;
        public string BILL_CD = string.Empty;
        public string strDatum0 = string.Empty;
        public string strDatum1 = string.Empty;
        public string strDatum2 = string.Empty;

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
                    if (!string.IsNullOrEmpty(Request.Params["Datum0"].ToString()))
                    {
                        strDatum0 = Request.Params["Datum0"].ToString();
                        strDatum0 = strDatum0.Replace("-", "");
                        txtHfDocNo.Text = Request.Params["Datum0"].ToString();
                    }
                   
                    if (!string.IsNullOrEmpty(Request.Params["Datum1"].ToString()))
                    {
                        strDatum1 = Request.Params["Datum1"].ToString();
                        strDatum1 = strDatum1.Replace("-", "");
                        //txtHfUserSeq.Text = Request.Params["Datum1"].ToString();
                    }
                    if (!string.IsNullOrEmpty(Request.Params["Datum2"].ToString()))
                    {
                        strDatum2 = Request.Params["Datum2"].ToString();
                        
                    }
                   
                    
                  
                }

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
                    
                      //strMRDFile = "Invoice_ParkingFee_Preview_L.mrd";
                    strMRDFile = "Invoice_SpecialMerge_ParkingFee_Preview_L_KNS.mrd";
                    
                  
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {
                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                    strMRDFile = "Invoice_SpecialMerge_ParkingFee_Preview_L_KNS.mrd";
                    //strMRDFile = "Invoice_ParkingFee_Preview_L.mrd";
                    
                  
                }
            }
        }

        private bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Session["HoadonOk"] != null)
            {
                if (Session["HoadonOk"].ToString().Equals(CommValue.AUTH_VALUE_FULL))
                {
                    Session["HoadonOk"] = null;

                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }
            else
            {
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // KN_USP_SET_INSERT_INVOICEFORTEMP_S02
                //InvoiceMngBlo.InsertTempHoadonForConfirmAPT(txtHfDocNo.Text);
                //InvoiceMngBlo.UpdatingInvoiceNoHoadonInfoApt(txtHfUserSeq.Text, txtHfPayDt.Text);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }
    }
}