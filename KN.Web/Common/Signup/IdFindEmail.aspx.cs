using System;
using System.Data;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Dac;

namespace KN.Web.Common.Signup
{
    public partial class IdFindEmail : BasePage
    {
        string strUserId = string.Empty;

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
            ltFindID.Text = TextNm["FINDID"];
            ltEmail.Text = TextNm["EMAIL"];
            ltNm.Text = TextNm["NAME"];

            CommCdRdoUtil.MakeSubCdRdoNoTitle(rdoFindID, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_FINDID, RepeatDirection.Horizontal);
            rdoFindID.SelectedValue = CommValue.ID_SEARCH_VALUE_EMAIL;
            imgbtnFindID.OnClientClick = "javascript:return fnLoginCheck('" + AlertNm["ALERT_INSERT_EMAIL"] + "' , '" + AlertNm["ALERT_INSERT_NAME"] + "');";
        }
        
        // 아이디 찾기 처리
        protected void imgbtnFindID_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                DataTable dtReturn = new DataTable();

                // KN_USP_COMM_SELECT_MEMINFO_S04
                dtReturn = MemberMngDao.SelectMemIDByEmail(txtEmailId.Text, txtEmailServer.Text, txtNm.Text);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {

                        strUserId = dtReturn.Rows[0]["UserId"].ToString();
                        Session["UserId"] = strUserId;

                        Response.Redirect(CommValue.PAGE_VALUE_FINDID, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        Response.Redirect(CommValue.PAGE_VALUE_NOTFOUND, CommValue.AUTH_VALUE_FALSE);
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
            Response.Redirect(CommValue.PAGE_VALUE_FINDIDMOBILE, CommValue.AUTH_VALUE_FALSE);
        }
    }
}