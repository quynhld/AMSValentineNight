using System;
using System.Data;
using System.Web.UI.WebControls;

using KN.Common.Base.Code;

using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Board.Biz;

namespace KN.Web.Common.Template
{
    public partial class MainFrame : System.Web.UI.MasterPage
    {
        public string TITLE_NOW = string.Empty;
        public string USER_NM = string.Empty;
        public string PAGE_TITLE = string.Empty;

        private int intRowCnt = 0;
        private int intLeftCnt = 0;

        private int intTotalRow = 0;
        private int intTotalLeft = 0;

        private bool isLeftMenuReturnOk = false;
        private bool isTopMenuReturnOk = false;

        // 페이지별 Sequence 값
        public string PAGE_SEQ = string.Empty;

        // 페이지별 세팅 경로
        public string PAGE_REDIRECT = string.Empty;
        public string PAGE_NOAUTH = string.Empty;
        public string PAGE_LIST = string.Empty;
        public string PAGE_WRITE = string.Empty;
        public string PAGE_VIEW = string.Empty;
        public string PAGE_MODIFY = string.Empty;
        public string PAGE_REPLY = string.Empty;
        public string PAGE_POPUP1 = string.Empty;
        public string PAGE_POPUP2 = string.Empty;
        public string PAGE_REFLECT = string.Empty;
        public string PAGE_TRANSFER = string.Empty;

        // 페이지별 세팅 파라미터
        public string PARAM_DATA1 = string.Empty;
        public string PARAM_DATA2 = string.Empty;
        public string PARAM_DATA3 = string.Empty;
        public string PARAM_DATA4 = string.Empty;
        public string PARAM_DATA5 = string.Empty;
        public string PARAM_RETURN = string.Empty;
        public string PARAM_REDIRECT = string.Empty;
        public string PARAM_REFLECT = string.Empty;
        public string PARAM_TRANSFER = string.Empty;

        // 쓰기 및 수정삭제 권한여부
        public bool isWriteAuthOk = false;
        public bool isModDelAuthOk = false;

        // 권한변수
        public string strReadAuth = string.Empty;
        public string strWriteAuth = string.Empty;
        public string strModDelAuth = string.Empty;

        public void Page_Init(object sender, EventArgs e)
        {
            try
            {
                // 권한체크
                AuthCheckLib.CheckSession();

                string strPath = Request.Url.PathAndQuery;

                DataSet dsReturn = new DataSet();

                if (Session["MasterMenuSeq"] != null)
                {
                    txtMasterMenuSeq.Text = Session["MasterMenuSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(txtMasterMenuSeq.Text))
                {
                    PAGE_SEQ = txtMasterMenuSeq.Text;
                }
                else
                {
                    PAGE_SEQ = "1";
                }

                dsReturn = CommMenuUtil.SpreadMenuMng(Session["LangCd"].ToString(), Session["MemAuthTy"].ToString(), TextLib.StringEncoder(strPath), Int32.Parse(PAGE_SEQ));

                if (dsReturn != null)
                {
                    // Top 메뉴 정보 조회
                    SetTop1stMenuInfo(dsReturn.Tables[0]);
                    SetTop2ndMenuInfo(dsReturn.Tables[1]);

                    // Left 메뉴 정보 조회
                    SetLeftMenuInfo(dsReturn.Tables[2]);

                    // 페이지별 링크정보 세팅
                    SetLinkInfo(dsReturn.Tables[3]);

                    // 페이지별 파라미터정보 세팅
                    SetPararmInfo(dsReturn.Tables[4]);

                    if (!isLeftMenuReturnOk || !isTopMenuReturnOk)
                    {
                        Response.Redirect(CommValue.PAGE_VALUE_NOMENU, false);
                    }
                    else
                    {
                        // 인사말 세팅
                        SetGreeting();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        public void Page_Load(object sender, EventArgs e)
        {
            ltHome.Text = "<a href='" + CommValue.PAGE_VALUE_DEFAULT + "'>HOME</a>";
            ltTopHome.Text = "<a href='" + CommValue.PAGE_VALUE_DEFAULT + "'>HOME</a>";
            ltTopLogOut.Text = "<a href='" + CommValue.PAGE_VALUE_LOGOUT + "'>LOG OUT</a>";
            ltTopSiteMap.Text = "<a href='" + CommValue.PAGE_VALUE_SITEMAP + "'>SITE MAP</a>";
        }

        /// <summary>
        /// 인사말 세팅
        /// </summary>
        private void SetGreeting()
        {
            DataTable dtMemoReturn = MemoMngBlo.SpreadMemoMng(Session["MemNo"].ToString());

            string strMemoPath = string.Empty;
            string strMemoCnt = "0";

            if (dtMemoReturn.Rows.Count > 0)
            {
                strMemoCnt = dtMemoReturn.Rows[0]["MemoCnt"].ToString();
            }

            strMemoPath = strMemoPath + "<a href='" + CommValue.PAGE_VALUE_MEMO + "'>";
            strMemoPath = strMemoPath + "<img src='/Common/Images/Icon/mail.gif' alt='Memo' align='absmiddle'/>(";
            strMemoPath = strMemoPath + strMemoCnt;
            strMemoPath = strMemoPath + ")";
            strMemoPath = strMemoPath + "</a>";

            switch (Session["LangCd"].ToString())
            {
                case CommValue.LANG_VALUE_ENGLISH:
                    USER_NM = "Hello, <a href='" + CommValue.PAGE_VALUE_ACCOUNTMNG + "'>" + Session["UserId"].ToString() + "</a>&nbsp;" + strMemoPath;
                    break;

                case CommValue.LANG_VALUE_KOREAN:
                    USER_NM = "<a href='" + CommValue.PAGE_VALUE_ACCOUNTMNG + "'>" + Session["MemNm"].ToString() + "님" + "</a>&nbsp;" + strMemoPath;
                    break;

                case CommValue.LANG_VALUE_VIETNAMESE:
                    USER_NM = "Xin chào, <a href='" + CommValue.PAGE_VALUE_ACCOUNTMNG + "'>" + Session["UserId"].ToString() + "</a>&nbsp;" + strMemoPath;
                    break;

                default:
                    USER_NM = "Hello, <a href='" + CommValue.PAGE_VALUE_ACCOUNTMNG + "'>" + Session["UserId"].ToString() + "</a>&nbsp;" + strMemoPath;
                    break;
            }
        }

        /// <summary>
        /// 상단 1st메뉴정보 세팅
        /// </summary>
        /// <param name="dtTopMenu">상단 1st메뉴정보 테이블</param>
        private void SetTop1stMenuInfo(DataTable dtTopMenu)
        {
            if (dtTopMenu.Rows.Count > 0)
            {
                intTotalRow = dtTopMenu.Rows.Count;

                lvTop1stMenu.DataSource = dtTopMenu;
                lvTop1stMenu.DataBind();

                isTopMenuReturnOk = true;
            }
            else
            {
                isTopMenuReturnOk = false;
            }
        }

        /// <summary>
        /// 상단 2nd메뉴정보 세팅
        /// </summary>
        /// <param name="dtTopMenu">상단 2nd메뉴정보 테이블</param>
        private void SetTop2ndMenuInfo(DataTable dtTopMenu)
        {
            if (dtTopMenu.Rows.Count > 0)
            {
                intTotalRow = dtTopMenu.Rows.Count;

                lvTop2ndMenu.DataSource = dtTopMenu;
                lvTop2ndMenu.DataBind();

                isTopMenuReturnOk = true;
            }
            else
            {
                isTopMenuReturnOk = false;
            }
        }

        /// <summary>
        /// 왼쪽메뉴정보 세팅
        /// </summary>
        /// <param name="dtLeftMenu">왼쪽메뉴정보 테이블</param>
        private void SetLeftMenuInfo(DataTable dtLeftMenu)
        {
            if (dtLeftMenu.Rows.Count > 0)
            {
                intTotalLeft = dtLeftMenu.Rows.Count;

                lvLeftMenu.DataSource = dtLeftMenu;
                lvLeftMenu.DataBind();

                // 읽기 권한 재확인
                AuthCheckLib.CheckAuthMenu(Session["MemAuthTy"].ToString(), strReadAuth);

                // 수정 권한 확인
                if (AuthCheckLib.CheckAuthPage(Session["MemAuthTy"].ToString(), strWriteAuth))
                {
                    isWriteAuthOk = true;
                }

                // 쓰기 권한 확인
                if (AuthCheckLib.CheckAuthPage(Session["MemAuthTy"].ToString(), strModDelAuth))
                {
                    isModDelAuthOk = true;
                }

                isLeftMenuReturnOk = true;
            }
            else
            {
                isLeftMenuReturnOk = false;
            }
        }

        /// <summary>
        /// 링크정보 세팅
        /// </summary>
        /// <param name="dtPage">링크정보 테이블</param>
        private void SetLinkInfo(DataTable dtPage)
        {
            if (dtPage.Rows.Count > 0)
            {
                foreach (DataRow dr in dtPage.Select())
                {
                    switch (dr["LinkCd"].ToString())
                    {
                        case "PAGE_REDIRECT":
                            PAGE_REDIRECT = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_NOAUTH":
                            PAGE_NOAUTH = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_LIST":
                            PAGE_LIST = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_WRITE":
                            PAGE_WRITE = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_VIEW":
                            PAGE_VIEW = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_MODIFY":
                            PAGE_MODIFY = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_REPLY":
                            PAGE_REPLY = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_POPUP1":
                            PAGE_POPUP1 = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_POPUP2":
                            PAGE_POPUP2 = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_REFLECT":
                            PAGE_REFLECT = dr["LinkPageNm"].ToString();
                            break;
                        case "PAGE_TRANSFER":
                            PAGE_TRANSFER = dr["LinkPageNm"].ToString();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 파라미터 정보 세팅
        /// </summary>
        /// <param name="dtParams">파라미터 정보 테이블</param>
        private void SetPararmInfo(DataTable dtParams)
        {
            if (dtParams.Rows.Count > 0)
            {
                foreach (DataRow dr in dtParams.Select())
                {
                    switch (dr["ParamCd"].ToString())
                    {
                        case "PARAM_DATA1":
                            PARAM_DATA1 = dr["ParamNm"].ToString();
                            break;
                        case "PARAM_DATA2":
                            PARAM_DATA2 = dr["ParamNm"].ToString();
                            break;
                        case "PARAM_DATA3":
                            PARAM_DATA3 = dr["ParamNm"].ToString();
                            break;
                        case "PARAM_DATA4":
                            PARAM_DATA4 = dr["ParamNm"].ToString();
                            break;
                        case "PARAM_DATA5":
                            PARAM_DATA5 = dr["ParamNm"].ToString();
                            break;
                        case "PARAM_RETURN":
                            PARAM_RETURN = dr["ParamNm"].ToString();
                            break;
                        case "PARAM_REDIRECT":
                            PARAM_REDIRECT = dr["ParamNm"].ToString();
                            break;
                        case "PARAM_REFLECT":
                            PARAM_REFLECT = dr["ParamNm"].ToString();
                            break;
                        case "PARAM_TRANSFER":
                            PARAM_TRANSFER = dr["ParamNm"].ToString();
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Top 1st 메뉴 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvTop1stMenu_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                intRowCnt++;

                string strNowSelectDepth1 = string.Empty;
                string strPath = string.Empty;

                Literal ltTopMenu = (Literal)iTem.FindControl("ltTopMenu");

                if (!string.IsNullOrEmpty(drView["NowYn"].ToString()))
                {
                    // 현재 페이지일 경우 
                    // 1. class="Tn-hover CursorNon" (선택용 Css) 삽입
                    // 2. 링크 걸지 않음
                    if (drView["NowYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                    {
                        strNowSelectDepth1 = " class=\"Tn-hover CursorNon\"";

                        strPath = TextLib.StringDecoder(drView["MenuTxt"].ToString());

                        if (!string.IsNullOrEmpty(drView["HiddenYn"].ToString()))
                        {
                            if (drView["HiddenYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                            {
                                // Navigator Depth1 생성
                                ltDepth1.Text = "<a href=\"" + TextLib.StringDecoder(drView["MenuUrl"].ToString()) + "\">" + TextLib.StringDecoder(drView["MenuTxt"].ToString()) + "</a>";
                            }
                        }
                    }
                    // 현재 페이지가 아닐경우 링크처리
                    else
                    {
                        strPath = "<a href=\"" + TextLib.StringDecoder(drView["MenuUrl"].ToString()) + "\">" + TextLib.StringDecoder(drView["MenuTxt"].ToString()) + "</a>";
                    }
                }

                if (!string.IsNullOrEmpty(drView["HiddenYn"].ToString()))
                {
                    if (drView["HiddenYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                    {
                        ltTopMenu.Text = "<span" + strNowSelectDepth1 + ">" + strPath + "</span>";
                    }
                }
            }
        }

        /// <summary>
        /// Top 2nd 메뉴 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvTop2ndMenu_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                intRowCnt++;

                string strNowSelectDepth2 = string.Empty;
                string strPath = string.Empty;

                Literal ltTopMenu = (Literal)iTem.FindControl("ltTopMenu");

                if (!string.IsNullOrEmpty(drView["NowYn"].ToString()))
                {
                    // 현재 페이지일 경우 
                    // 1. class="Sn-hover CursorNon" (선택용 Css) 삽입
                    // 2. 링크 걸지 않음
                    if (drView["NowYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                    {
                        strNowSelectDepth2 = " class=\"Sn-hover CursorNon\"";

                        strPath = TextLib.StringDecoder(drView["MenuTxt"].ToString());

                        if (!string.IsNullOrEmpty(drView["HiddenYn"].ToString()))
                        {
                            if (drView["HiddenYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                            {
                                ltTopMenu.Text = "<span" + strNowSelectDepth2 + ">" + strPath + "</span>";

                                // Left Menu 제목 생성
                                ltLeftTitle.Text = TextLib.StringDecoder(drView["MenuTxt"].ToString());

                                // Navigator Depth2 생성
                                ltDepth2.Text = "<a href=\"" + TextLib.StringDecoder(drView["MenuUrl"].ToString()) + "\">" + TextLib.StringDecoder(drView["MenuTxt"].ToString()) + "</a>";
                            }
                        }
                    }
                    // 현재 페이지가 아닐경우 링크처리
                    else
                    {
                        //strPath = "<a href=\"" + TextLib.StringDecoder(drView["MenuUrl"].ToString()) + "\">" + TextLib.StringDecoder(drView["MenuTxt"].ToString()) + "</a>";
                        //ltTopMenu.Text = "<span" + strNowSelectDepth2 + ">" + strPath + "</span>";
                        if (!string.IsNullOrEmpty(drView["HiddenYn"].ToString()))
                        {
                            if (drView["HiddenYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                            {
                                ltTopMenu.Text = "<a href=\"" + TextLib.StringDecoder(drView["MenuUrl"].ToString()) + "\">" + "<span" + strNowSelectDepth2 + ">" + TextLib.StringDecoder(drView["MenuTxt"].ToString()) + "</span>" + "</a>";
                            }
                        }
                    }
                }

                //ltTopMenu.Text = "<span" + strNowSelectDepth2 + ">" + strPath + "</span>";
            }
        }

        /// <summary>
        /// Left 메뉴 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLeftMenu_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                intLeftCnt++;

                string strNowSelectDepth3 = string.Empty;
                string strActiveLnb = string.Empty;
                string strFullPath = string.Empty;
                string strPartialPath = string.Empty;

                Literal ltLeftMenu = (Literal)iTem.FindControl("ltLeftMenu");

                // 선택된 3 Depth 메뉴 선택용 Css 세팅
                if (!string.IsNullOrEmpty(drView["NowYn"].ToString()))
                {
                    if (drView["NowYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                    {
                        strNowSelectDepth3 = " class=\"S3-hover CursorNon\"";
                        strReadAuth = drView["ReadAuth"].ToString();
                        strWriteAuth = drView["WriteAuth"].ToString();
                        strModDelAuth = drView["ModDelAuth"].ToString();

                        // Web Page 제목 생성
                        TITLE_NOW = TextLib.StringDecoder(drView["MenuTxt"].ToString());
                        PAGE_TITLE = TextLib.StringDecoder(drView["MenuTxt"].ToString());

                        // 페이지 고유순번 처리
                        PAGE_SEQ = drView["MenuSeq"].ToString();
                        txtMasterMenuSeq.Text = PAGE_SEQ;
                        Session["MasterMenuSeq"] = PAGE_SEQ;
                    }
                }

                // 경로처리
                if (!string.IsNullOrEmpty(drView["LinkYn"].ToString()))
                {
                    if (drView["LinkYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                    {
                        // 3 Depth 메뉴의 경우 링크 살림
                        strFullPath = "<a href=\"" + TextLib.StringDecoder(drView["MenuUrl"].ToString()) + "\"" + strActiveLnb + ">" + TextLib.StringDecoder(drView["MenuTxt"].ToString()) + "</a>";
                        strPartialPath = TextLib.StringDecoder(drView["MenuTxt"].ToString());
                    }
                    else
                    {
                        // 링크정보가 없는 메뉴의 경우 그냥 텍스트만 뿌려줌
                        strFullPath = TextLib.StringDecoder(drView["MenuTxt"].ToString());
                        strPartialPath = TextLib.StringDecoder(drView["MenuTxt"].ToString());
                    }
                }

                if (!string.IsNullOrEmpty(drView["HiddenYn"].ToString()))
                {
                    if (drView["HiddenYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                    {
                        ltLeftMenu.Text = ltLeftMenu.Text + "<li" + strNowSelectDepth3 + ">" + strFullPath + "</li>";
                    }
                }
            }
        }
    }
}