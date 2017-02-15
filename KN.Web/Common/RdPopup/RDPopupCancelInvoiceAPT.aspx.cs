using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Resident.Biz;


using KN.Settlement.Biz;
using System.Text;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupCancelInvoiceAPT : BasePage
    {
        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string DOC_NO = string.Empty;
        public string REQ_DT = string.Empty;
        public string RESON = string.Empty;
        public string PRINT_NO = string.Empty;
        public string LINK_NM = string.Empty;

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
                    if(RESON=="1")
                    {
                        strMRDFile = "Invoice_Cancel_CNSThird_Preview_L.mrd";      
                    }
                    else
                    {
                         strMRDFile = "Invoice_Cancel_Amount_CNSThird_Preview_L.mrd";
                        //strMRDFile = "Invoice_Cancel_CNSThird_Preview_Test_L.mrd";
                    }
                                  
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {

                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                    if (RESON == "1")
                    {
                        strMRDFile = "Invoice_Cancel_CNSThird_Preview_L.mrd";
                    }
                    else
                    {
                        strMRDFile = "Invoice_Cancel_Amount_CNSThird_Preview_L.mrd";
                        //strMRDFile = "Invoice_Cancel_CNSThird_Preview_Test_L.mrd";
                    }         
                }
            }
        }

        private bool CheckParams()
        {
            bool isReturnOk;

            if (Request.Params["Datum0"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["Datum0"]))
                {
                    DOC_NO = Request.Params["Datum0"];
                    txthfInvoiceNo.Text = Request.Params["Datum0"];
                }

                if (!string.IsNullOrEmpty(Request.Params["Datum1"]))
                {
                    PRINT_NO = Request.Params["Datum1"];
                    txthfPrintNo.Text = Request.Params["Datum1"];
                }

                if (!string.IsNullOrEmpty(Request.Params["Datum2"]))
                {
                    RESON = Request.Params["Datum2"];
                    txthfReason.Text = Request.Params["Datum2"];
                }

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                isReturnOk = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturnOk;
        }

        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //var oldInvoiceNo = txthfInvoiceNo.Text;
                //var insCompCd = Session["CompCd"].ToString();
                //var insMemNo = Session["MemNo"].ToString();
                //var insMemIP = Session["UserIP"].ToString();
                //KN_USP_UPDATE_INVOICENO_HOADONINFOAPT_U00

                //InvoiceMngBlo.InsertCancelInvoiceHoadonInfoApt(txthfPrintNo.Text,oldInvoiceNo,txthfReason.Text, insCompCd, insMemNo, insMemIP);   

                //var sbNoSelection = new StringBuilder();

                //sbNoSelection.Append("opener.window.fnLoadData();");

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RefreshParent", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }
    }
}