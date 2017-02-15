using System;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

namespace KN.Web.Common.Signup
{
    public partial class IdNotFind : BasePage
    {
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
            ltIdNotFound1.Text = TextNm["IDNOTFOUND_1"];
            ltIdNotFound2.Text = TextNm["IDNOTFOUND_2"];
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

        // 아이디 찾기로 가기
        protected void imgbtnFindID_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect(CommValue.PAGE_VALUE_FINDIDEMAIL, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}