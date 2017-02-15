using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Manage.Biz;
using KN.Resident.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupReturnCarCardAndRenovation : BasePage
    {
        public string strDatum0 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    else
                    {
                        InitControls();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            var isReturn = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["RefSeq"] != null )
            {
                if (!string.IsNullOrEmpty(Request.Params["RefSeq"]))
                {
                    txtHfRefSeq.Text = Request.Params["RefSeq"];
                    strDatum0 = txtHfRefSeq.Text;
                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
            }
            return isReturn;
        }

        protected void InitControls()
        {
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnDelete.OnClientClick = "javascript:return fnPopupConfirm('" + AlertNm["ALERT_SELECT_CATEGORY"] + "','" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlPaymentTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                AuthCheckLib.CheckSession();
                var seq = txtHfRefSeq.Text;
                var memNo = Session["MemNo"].ToString();
                var ip = Session["UserIP"].ToString();
                var returnDt = txtPayDay.Text.Replace("-","");
                //KN_USP_MNG_DELETE_RENOVATION_M00
                MngPaymentBlo.DeleteRenovationAptDetails(seq, 2, memNo, ip, ddlPaymentTy.SelectedValue,returnDt);
                var sbPrintOut = new StringBuilder();
                sbPrintOut.Append("closePopup('" + seq + "');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Renovation", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            var sbInfo = new StringBuilder();

            sbInfo.Append("alert('");
            sbInfo.Append(AlertNm["INFO_CANCEL"]);
            sbInfo.Append("');");
            sbInfo.Append("returnValue='CANCEL';");
            sbInfo.Append("self.close();");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cancel", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
    }
}