using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

namespace KN.Web
{
    public partial class Index : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["chkAccessLangCd"] == null)
                {
                    Session["LangCd"] = CommValue.LANG_VALUE_ENGLISH;
                }
                else
                {
                    Session["LangCd"] = Request.Cookies["chkAccessLangCd"].Value;
                }

                CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlLang, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_LANG);
                ddlLang.SelectedValue = Session["LangCd"].ToString();

                chkRememberID.Text = TextNm["REMEMBERID"];

                if (Request.Cookies["chkAccessUserId"] != null)
                {
                    chkRememberID.Checked = CommValue.AUTH_VALUE_TRUE;
                    txtID.Text = Request.Cookies["chkAccessUserId"].Value;

                    if (Request.Cookies["chkAccessComp"] != null)
                    {
                        ddlComp.SelectedValue = Request.Cookies["chkAccessComp"].Value;
                    }

                    if (Request.Cookies["chkAccessLangCd"] != null)
                    {
                        ddlLang.SelectedValue = Request.Cookies["chkAccessLangCd"].Value;
                    }
                }
            }
        }

        protected void ddlLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, string> dcMenuNm = new Dictionary<string, string>();
            Dictionary<string, string> dcTextNm = new Dictionary<string, string>();

            Session["LangCd"] = ddlLang.SelectedValue;

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlLang, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_LANG);
            ddlLang.SelectedValue = Session["LangCd"].ToString();

            DataTable dtText = new DataTable();
            DataTable dtMenu = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = Session["LangCd"].ToString();
            objParams[1] = string.Empty;

            dtText = ExpressCdTxtUtil.MakeMultiLanguage(Session["LangCd"].ToString(), CommValue.TEXT_TYPE_VALUE_ITEM, CommValue.NEMUSEQ_VALUE_LOGIN);
            dtMenu = ExpressCdTxtUtil.MakeMultiLanguage(Session["LangCd"].ToString(), CommValue.TEXT_TYPE_VALUE_MENU, CommValue.NEMUSEQ_VALUE_LOGIN);

            foreach (DataRow dr in dtMenu.Select())
            {
                dcMenuNm[dr["ExpressCd"].ToString()] = dr["ExpressNm"].ToString();
            }

            foreach (DataRow dr in dtText.Select())
            {
                dcTextNm[dr["ExpressCd"].ToString()] = dr["ExpressNm"].ToString();
            }

            chkRememberID.Text = dcTextNm["REMEMBERID"];
        }

        protected void imgbtnEnter_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                DataTable dtReturn = new DataTable();

                dtReturn = CommMenuUtil.SpreadMemInfo(ddlComp.SelectedValue, txtID.Text, txtPWD.Text);

                if (dtReturn != null)
                {
                    string strIP = Request.ServerVariables["REMOTE_ADDR"];

                    if (dtReturn.Rows.Count > 0)
                    {
                        // Sessioni정보 세팅
                        // 1. MemNo         = "사원고유번호"
                        // 2. UserId        = "접속ID"
                        // 3. MemAuthTy     = "할당된 권한"
                        // 4. MemNm         = "회원명"
                        // 5. MemAccAuthTy  = "접속 권한"
                        // 6. CompCd        = "접속 기업코드"
                        // 7. LangCd        = "언어코드"
                        Session["MemNo"] = dtReturn.Rows[0]["MemNo"].ToString();
                        Session["UserId"] = dtReturn.Rows[0]["UserId"].ToString();
                        Session["MemAuthTy"] = dtReturn.Rows[0]["MemAuthTy"].ToString();
                        Session["MemNm"] = dtReturn.Rows[0]["MemNm"].ToString();
                        Session["MemAccAuthTy"] = dtReturn.Rows[0]["MemAccAuthTy"].ToString();
                        Session["CompCd"] = dtReturn.Rows[0]["CompNo"].ToString();
                        Session["LangCd"] = ddlLang.SelectedValue;
                        Session["UserIP"] = strIP;

                        if (chkRememberID.Checked)
                        {
                            // 쿠키 객체 생성.
                            var cookieUserId = new HttpCookie("chkAccessUserId");
                            var cookieUserComp = new HttpCookie("chkAccessComp");
                            var ccokieUserLangCd = new HttpCookie("chkAccessLangCd");
                            var ccokieMemNo = new HttpCookie("chkMemNo");

                            // 쿠키 유효일자 지정함.
                            var dtNow = DateTime.Now.AddDays(2);

                            // 쿠키의 유효일자를 7일로 세팅함.
                            var tsSpan = new TimeSpan(7, 0, 0, 0);
                            cookieUserId.Expires = dtNow + tsSpan;
                            cookieUserComp.Expires = dtNow + tsSpan;
                            ccokieUserLangCd.Expires = dtNow + tsSpan;
                            ccokieMemNo.Expires = dtNow + tsSpan;

                            cookieUserId.Value = txtID.Text;
                            cookieUserComp.Value = ddlComp.SelectedValue;
                            ccokieUserLangCd.Value = ddlLang.SelectedValue;
                            ccokieMemNo.Value = dtReturn.Rows[0]["MemNo"].ToString();

                            Response.Cookies.Add(cookieUserId);
                            Response.Cookies.Add(cookieUserComp);
                            Response.Cookies.Add(ccokieUserLangCd);
                            Response.Cookies.Add(ccokieMemNo);
                        }
                        else
                        {
                            Response.Cookies["chkAccessUserId"].Expires = DateTime.Today.AddDays(-1);
                            Response.Cookies["chkAccessComp"].Expires = DateTime.Today.AddDays(-1);
                            Response.Cookies["chkAccessLangCd"].Expires = DateTime.Today.AddDays(-1);
                        }

                        AccessLogger.MakeLogger(dtReturn.Rows[0]["CompNo"].ToString(), dtReturn.Rows[0]["MemNo"].ToString(), txtID.Text, txtPWD.Text, strIP);

                        Response.Redirect(CommValue.PAGE_VALUE_DEFAULT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        AccessLogger.MakeLogger("00000000", "KN999999999", txtID.Text, txtPWD.Text, strIP);

                        Response.Redirect(CommValue.PAGE_VALUE_INDEX, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        // 아이디 찾기 페이지 생성 후 처리
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

        // 비밀번호 찾기 페이지 생성 후 처리
        protected void imgbtnFindPW_Click(object sender, System.Web.UI.ImageClickEventArgs e)
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