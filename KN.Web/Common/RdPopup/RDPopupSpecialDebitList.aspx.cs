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
using KN.Manage.Biz;

namespace KN.Web.Common.RdPopup
{
    public partial class RDPopupSpecialDebitList : BasePage
    {
        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string DOC_NO = string.Empty;
        public string REQ_DT = string.Empty;
        public string PAY_TYPE = string.Empty;
        public string RENT_CD = string.Empty;
        public string LINK_NM = string.Empty;
        public string FEE_TYPE = string.Empty;

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

                    if (FEE_TYPE.Equals("0001") || FEE_TYPE.Equals("0018"))
                    {
                        strMRDFile = "SpecialDebitNoteMngFee_L.mrd";

                    }
                    else 
                    {
                        strMRDFile = "SpecialDebitNote_L.mrd";
                    }

                    
                                      
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {

                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;

                    //strMRDFile = "SpecialDebitNote_L.mrd";
                    if (FEE_TYPE.Equals("0001") || FEE_TYPE.Equals("0018"))
                    {
                        strMRDFile = "SpecialDebitNoteMngFee_L.mrd";

                    }
                    else
                    {
                        strMRDFile = "SpecialDebitNote_L.mrd";
                    }
                }
            }
        }

        private bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["Datum0"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["Datum0"].ToString()))
                {
                    DOC_NO = Request.Params["Datum0"].ToString();
                    txtHfDocNo.Text = Request.Params["Datum0"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum1"].ToString()))
                {
                    REQ_DT = Request.Params["Datum1"].ToString();
                    //txtHfDocNo.Text = Request.Params["Datum0"].ToString();
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum2"].ToString()))
                {
                    FEE_TYPE = Request.Params["Datum2"].ToString();                    
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
                //KN_USP_SET_INSERT_INVOICE_SPECIAL_M00 
                MngPaymentBlo.InsertSpecialDebitToHoadonInfo(txtHfDocNo.Text);

                StringBuilder sbNoSelection = new StringBuilder();

                sbNoSelection.Append("opener.window.fnLoadData();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RefreshParent", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }

        protected void imgBtnReprint_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AuthCheckLib.CheckSession();

                

                //StringBuilder sb = new StringBuilder();
                //sb.Append("<script language='JavaScript'>");
                //sb.AppendFormat("window.close");
                //sb.Append("</script>");
                //Page.RegisterStartupScript("Test", sb.ToString());

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "rd_Print1();", CommValue.AUTH_VALUE_TRUE);
                

                InvoiceMngBlo.UpdatingCreateYN(DOC_NO);

                

                //Response.Redirect(LINK_NM, CommValue.AUTH_VALUE_FALSE);
                
                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}