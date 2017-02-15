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
    public partial class RDPopupUltilDebitDetails : BasePage
    {
        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;
        public string DOC_NO = string.Empty;
        public string RENT_CODE = string.Empty;
        public string CHARGE_TYPE = string.Empty;
        public string DATE_S = string.Empty;
        public string DATE_R = string.Empty;
        public string COMPANY_CODE = string.Empty;
        public string IS_PRINT = string.Empty;


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
                if (!string.IsNullOrEmpty(DATE_R))
                {
                    var ds = ReceiptMngBlo.UpdateUtilRequestDt(RENT_CODE, CHARGE_TYPE, DATE_R, DATE_S, IS_PRINT);
                    DATE_S = ds.Rows[0][0].ToString();
                }
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
                    if (CHARGE_TYPE.Equals("0003"))
                    {
                        strMRDFile = "NoticeTowerOfficeUtility_L.mrd";

                    }
                    else if (CHARGE_TYPE.Equals("0004"))
                    {
                        strMRDFile = "NoticeTowerOfficeUtilityOverTime_L.mrd";
                    }
                    else
                    {
                        if (RENT_CODE == "9900")
                        {
                            strMRDFile = "NoticeTowerOfficeUtilityAirConAPT_L.mrd";
                        }
                        else
                        {
                            strMRDFile = "NoticeTowerOfficeUtilityOverTimeAirCon_L.mrd";
                        }
                        
                    }
                }
                else if (strHostIp.Equals(CommValue.PRIVATE_VALUE_DOMAIN.ToLower()) || strHostIp.Equals(CommValue.PRIVATE_VALUE_TESTDOMAIN.ToLower()))
                {
                    // 공식내부IP
                    NOW_DOMAIN = CommValue.PRIVATE_VALUE_DOMAIN;
                    if (CHARGE_TYPE.Equals("0003"))
                    {
                        strMRDFile = "NoticeTowerOfficeUtility_L.mrd";

                    }else if(CHARGE_TYPE.Equals("0004"))
                    {
                        strMRDFile = "NoticeTowerOfficeUtilityOverTime_L.mrd";
                    }
                    else
                    {
                        strMRDFile = "NoticeTowerOfficeUtilityOverTimeAirCon_L.mrd";
                    }

                }
            }
        }

        private bool CheckParams()
        {
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["Datum0"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["Datum0"]))
                {
                    RENT_CODE = Request.Params["Datum0"];                   
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum1"]))
                {
                    CHARGE_TYPE = Request.Params["Datum1"];                    
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum2"]))
                {
                    DATE_S = Request.Params["Datum2"];                    
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum3"]))
                {
                    DATE_R = Request.Params["Datum3"];
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum4"]))
                {
                    IS_PRINT = Request.Params["Datum4"];
                    txthfIsPrint.Text = IS_PRINT;
                }
                COMPANY_CODE = Session["CompCd"].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (!txthfIsPrint.Text.Equals("N"))
                {
                    return;
                }
                ReceiptMngBlo.UpdateUtilBillingList(RENT_CODE, "", CHARGE_TYPE, "", DATE_S);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }
    }
}