using System;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Manage.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupMngComplete : BasePage
    {
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
                        sbWarning.Append("returnValue='CANCEL';");
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
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            // PreCondition
            // 1. 인자값이 제대로 넘어오지 않을 경우 완료할 수 없음.
            if (Session["CompleteYn"] != null)
            {
                // 상세보기 페이지를 거쳐서 넘어오지 않을 경우 완료할 수 없음.
                if (Session["CompleteYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    // 인자값이 제대로 넘어오지 않을 경우 삭제할 수 없음.
                    if (Request.Params["UserSeq"] != null &&
                        Request.Params["RentCd"] != null &&
                        Request.Params["FeeTy"] != null &&
                        Request.Params["RentalYear"] != null &&
                        Request.Params["RentalMM"] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params["UserSeq"].ToString()) &&
                            !string.IsNullOrEmpty(Request.Params["RentCd"].ToString()) &&
                            !string.IsNullOrEmpty(Request.Params["FeeTy"].ToString()) &&
                            !string.IsNullOrEmpty(Request.Params["RentalYear"].ToString()) &&
                            !string.IsNullOrEmpty(Request.Params["RentalMM"].ToString()))
                        {
                            txtHfRentCd.Text = Request.Params["RentCd"].ToString();
                            txtHfUserSeq.Text = Request.Params["UserSeq"].ToString();
                            txtHfFeeTy.Text = Request.Params["FeeTy"].ToString();
                            txtHfRentalYear.Text = Request.Params["RentalYear"].ToString();
                            txtHfRentalMM.Text = Request.Params["RentalMM"].ToString();

                            Session["CompleteYn"] = null;

                            isReturn = CommValue.AUTH_VALUE_TRUE;
                        }
                        else
                        {
                            isReturn = CommValue.AUTH_VALUE_FALSE;
                        }
                    }
                    else
                    {
                        isReturn = CommValue.AUTH_VALUE_FALSE;
                    }
                }
                else
                {
                    isReturn = CommValue.AUTH_VALUE_FALSE;
                }
            }
            else
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturn;
        }

        protected void InitControls()
        {
            ltReason.Text = TextNm["REASON"];
            ltContents.Text = TextNm["CONTENTS"];

            lnkbtnRegist.Text = TextNm["REGIST"];
            lnkbtnCancel.Text = TextNm["CANCEL"];

            lnkbtnRegist.OnClientClick = "javascript:return fnPopupConfirm('" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                object[] objReturn = new object[2];
                string strIp = Request.ServerVariables["REMOTE_ADDR"];

                // KN_USP_RES_INSERT_RENTALMNGREASONINFO_M00
                objReturn = MngPaymentBlo.RegistryRentalMngReasonInfo(txtHfRentCd.Text, txtHfFeeTy.Text, txtHfUserSeq.Text, txtHfRentalYear.Text, txtHfRentalMM.Text,
                                                                      txtCompleteReason.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                if (objReturn != null)
                {
                    StringBuilder sbInfo = new StringBuilder();

                    sbInfo.Append("alert('");
                    sbInfo.Append(AlertNm["INFO_PAYMENT"]);
                    sbInfo.Append("');");
                    sbInfo.Append("returnValue='FINISHED';");
                    sbInfo.Append("self.close();");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Complete", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            StringBuilder sbInfo = new StringBuilder();

            sbInfo.Append("alert('");
            sbInfo.Append(AlertNm["INFO_CANCEL"]);
            sbInfo.Append("');");
            sbInfo.Append("returnValue='CANCEL';");
            sbInfo.Append("self.close();");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cancel", sbInfo.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
    }
}