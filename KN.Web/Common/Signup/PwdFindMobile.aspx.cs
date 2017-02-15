using System;
using System.Data;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;

namespace KN.Web.Common.Signup
{
    public partial class PwdFindMobile : BasePage
    {
        string strUserPwd = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    //컨트롤 초기화
                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤을 초기화 하는 메소드 
        /// </summary>
        private void InitControls()
        {
            ltUserId.Text = TextNm["USERID"];
            ltFindPwd.Text = TextNm["FINDPASSWORD"];
            ltMobile.Text = TextNm["MOBILENO"];
            ltNm.Text = TextNm["NAME"];

            CommCdRdoUtil.MakeSubCdRdoNoTitle(rdoFindPwd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_FINDPASSWORD, RepeatDirection.Horizontal);
            rdoFindPwd.SelectedValue = CommValue.PASSWORD_SEARCH_VALUE_MOBILE;
            imgbtnFindPwd.OnClientClick = "javascript:return fnLoginCheck('" + AlertNm["ALERT_INSERT_MOBILENO"] + "' , '" + AlertNm["ALERT_INSERT_NAME"] + "', '" + AlertNm["ALERT_INSERT_USERID"] + "');";
        }

        protected void imgbtnFindPwd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                DataTable dtReturn = new DataTable();

                // KN_USP_COMM_SELECT_MEMINFO_S07
                dtReturn = MemberMngBlo.WatchMemPwdByMobile(txtUserId.Text, txtNm.Text, txtMobileTypeCd.Text, txtMobileFrontNo.Text, txtMobileRearNo.Text);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        strUserPwd = dtReturn.Rows[0]["Pwd"].ToString();
                        Session["UserPwd"] = strUserPwd;

                        Response.Redirect(CommValue.PAGE_VALUE_FINDPASSWORD, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        Response.Redirect(CommValue.PAGE_VALUE_FINDNOTPASSWORD, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        // 로그인으로 가기
        protected void imgbtnMoveLogin_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect(CommValue.PAGE_VALUE_INDEX, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void rdoContNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(CommValue.PAGE_VALUE_FINDPASSWORDEMAIL, CommValue.AUTH_VALUE_FALSE);
        }
    }
}