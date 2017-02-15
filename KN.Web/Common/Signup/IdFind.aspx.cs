using System;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

namespace KN.Web.Common.Signup
{
    public partial class IdFind : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["UserId"] != null)
                {
                    if (!IsPostBack)
                    {
                        //컨트롤 초기화
                        InitControls();
                    }
                }
                else
                {
                    Response.Redirect(CommValue.PAGE_VALUE_INDEX, CommValue.AUTH_VALUE_FALSE);
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
            ltUserId.Text = string.Format(TextNm["IDFOUND_1"], "<span class='txtP'>" + Session["UserId"].ToString() + "</span>");
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

        // 비밀번호 찾기로 가기
        protected void imgbtnFindPwd_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect(CommValue.PAGE_VALUE_FINDPASSWORDEMAIL, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}