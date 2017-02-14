using System;
using System.Web;
using System.Collections.Generic;
using System.Data;

using KN.Common.Base.Code;

namespace KN.Common.Base
{
    public class BasePage : System.Web.UI.Page
    {
        public Dictionary<string, string> AlertNm = new Dictionary<string, string>();
        public Dictionary<string, string> MenuNm = new Dictionary<string, string>();
        public Dictionary<string, string> TextNm = new Dictionary<string, string>();
        public Dictionary<string, string> PopupNm = new Dictionary<string, string>();

        #region OnInit : 페이지 초기화 처리
        protected override void OnInit(EventArgs e)
        {
            int intMenuSeq = 0;
            //페이지 타이틀 설정
            //this.SetPageTitle();
            if (Session["LangCd"] == null)
            {
                Session["LangCd"] = CommValue.LANG_VALUE_ENGLISH;
            }

            Session["NowPageUrl"] = HttpContext.Current.Request.Url.OriginalString;

            if (Session["MasterMenuSeq"] != null)
            {
                if (string.IsNullOrEmpty(Session["MasterMenuSeq"].ToString()))
                {
                    intMenuSeq = 0;
                }
                else
                {
                    intMenuSeq = Int32.Parse(Session["MasterMenuSeq"].ToString());
                }
            }
            else
            {
                intMenuSeq = 0;
            }

            SetTextInfo(Session["LangCd"].ToString(), intMenuSeq);
        }
        #endregion

        #region Page_Error : 에러이벤트 처리
        public void Page_Error(object sender, EventArgs e)
        {
            //오류 로그를 작성 후 저장소에 저장 한다.   
            //Server.Transfer("/Manage/Board/BoardList.aspx");
            if (!Response.IsRequestBeingRedirected)
            HttpContext.Current.Response.Redirect(CommValue.PAGE_VALUE_DEFAULT, false);
        }
        #endregion

        #region DisplayAlert : 자바스크립트 에러창 처리
        protected virtual void DisplayAlert(string message)
        {
            ClientScript.RegisterStartupScript(
                            this.GetType(),
                            Guid.NewGuid().ToString(),
                            string.Format("alert('{0}');", message.Replace("'", @"\'")),
                            true
                        );
        }
        #endregion

        #region SetTextInfo : 언어 선택

        protected virtual void SetTextInfo(string strLangCd, int intMenuSeq)
        {
            DataTable dtAlert = new DataTable();
            DataTable dtText = new DataTable();
            DataTable dtMenu = new DataTable();
            DataTable dtPopup = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = string.Empty;
            objParams[2] = intMenuSeq;

            object[] objParam = new object[2];

            objParam[0] = strLangCd;
            objParam[1] = string.Empty;

            dtMenu = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MENUTXT_S00", objParam);
            dtAlert = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_ALERT_S00", objParam);
            dtText = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_TXT_S00", objParams);
            dtPopup = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_TXT_S01", objParam);

            foreach (DataRow dr in dtMenu.Select())
            {
                MenuNm[dr["ExpressCd"].ToString()] = dr["ExpressNm"].ToString();
            }

            foreach (DataRow dr in dtText.Select())
            {
                TextNm[dr["ExpressCd"].ToString()] = dr["ExpressNm"].ToString();
            }

            foreach (DataRow dr in dtAlert.Select())
            {
                AlertNm[dr["ExpressCd"].ToString()] = dr["ExpressNm"].ToString();
            }

            foreach (DataRow dr in dtPopup.Select())
            {
                PopupNm[dr["ExpressCd"].ToString()] = dr["ExpressNm"].ToString();
            }
        }

        #endregion
    }
}
