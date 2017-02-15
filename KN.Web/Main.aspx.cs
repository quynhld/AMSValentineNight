using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Board.Biz;

namespace KN.Web
{
    public partial class Main : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        string strBoardTy = string.Empty;
        string strBoardCd = string.Empty;

        string Depth1 = string.Empty;
        int intRowCnt = 0;

        bool isStart = CommValue.AUTH_VALUE_TRUE;
        bool isSecondStart = CommValue.AUTH_VALUE_TRUE;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // 컨트롤 초기화
                    InitControls();

                    // 데이터 로드 및 바인딩(공지사항)
                    LoadNoticeData();

                    // 데이터 로드 및 바인딩(자료실)
                    LoadArchiveData();

                    // 데이터 로드 및 바인딩(자료실)
                    LoadMemoData();

                    // 데이터 로드 및 바인딩(사이트맵)
                    LoadSiteMapData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            ltNotice.Text = TextNm["NOTICE"];
            ltArchives.Text = TextNm["ARCHIVES"];
            ltMemo.Text = TextNm["MEMO"];
            lnkMore1.Text = TextNm["MORE"];
            lnkMore2.Text = TextNm["MORE"];
            lnkMore3.Text = TextNm["MORE"];
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {

        }

        /// <summary>
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadNoticeData()
        {
            // 파라미터 체크
            CheckParams();

            int intCurrentPage = CommValue.NUMBER_VALUE_1;
            string strBoardTy = CommValue.BBS_TYPE_VALUE_BOARD;
            string strBoardCd = CommValue.BOARD_TYPE_VALUE_NOTICE;
            string strKeyCd = "";
            string strKeyWord = "";

            DataSet dsReturn = new DataSet();

            // KN_USP_BRD_SELECT_BOARDINFO_S00
            dsReturn = BoardInfoBlo.SpreadBoardInfo(CommValue.BOARD_VALUE_MINIPAGESIZE, intCurrentPage, strBoardTy, strBoardCd, strKeyCd, strKeyWord, Session["MemAuthTy"].ToString(), Session["CompCd"].ToString(), Session["MemNo"].ToString());

            if (dsReturn != null)
            {
                lvBoardList.DataSource = dsReturn.Tables[1];
                lvBoardList.DataBind();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBoardList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvBoardList.FindControl("ltTitle")).Text = TextNm["TITLE"];
            ((Literal)lvBoardList.FindControl("ltInsDt")).Text = TextNm["DAY"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBoardList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의

                    ((Literal)e.Item.FindControl("ltTitle")).Text = TextNm["TITLE"];
                    ((Literal)e.Item.FindControl("ltInsDt")).Text = TextNm["DAY"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBoardList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["BoardTitle"].ToString()))
                {
                    // Int32의 크기 :  -2147483647 ~ 2147483647
                    int intDepth = Int32.Parse(drView["BoardDepth"].ToString());

                    StringBuilder sbDepth = new StringBuilder();
                    StringBuilder sbReply = new StringBuilder();
                    StringBuilder sbTitle = new StringBuilder();
                    StringBuilder sbDisk = new StringBuilder();


                    // 문자열이 특정 길이 이상일 경우 잘라주는 부분
                    if (drView["BoardTitle"].ToString().Length > 30)
                    {
                        // 제목이 60Bytes 이상일 경우 뒤는 자르고 '...'를 삽입함.
                        sbTitle.Append(TextLib.TextCutString(drView["BoardTitle"].ToString(), 25, "..."));
                    }
                    else
                    {
                        sbTitle.Append(drView["BoardTitle"].ToString());
                    }

                    ((LinkButton)e.Item.FindControl("lnkTitleList")).Text = sbTitle.ToString();

                    ((TextBox)iTem.FindControl("txtHfSeq")).Text = drView["BoardSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ModDt"].ToString()))
                {
                    string strModDt = drView["ModDt"].ToString();
                    StringBuilder sbModDt = new StringBuilder();

                    if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                    {
                        sbModDt.Append(strModDt.Substring(4, 2));
                        sbModDt.Append(".");
                        sbModDt.Append(strModDt.Substring(6, 2));
                    }
                    else
                    {
                        sbModDt.Append(strModDt.Substring(6, 2));
                        sbModDt.Append("/");
                        sbModDt.Append(strModDt.Substring(4, 2));
                    }

                    ((Literal)iTem.FindControl("ltInsDtList")).Text = sbModDt.ToString();
                }
            }
        }

        protected void lnkTitleList_Click(object sender, EventArgs e)
        {
            try
            {
                strBoardTy = CommValue.BBS_TYPE_VALUE_BOARD;
                strBoardCd = CommValue.BOARD_TYPE_VALUE_NOTICE;
                string strBoardSeq = string.Empty;

                // 선택된 항목 넘겨주는 부분
                Session[Master.PARAM_DATA1] = strBoardTy;
                Session[Master.PARAM_DATA2] = strBoardCd;

                Session[Master.PARAM_DATA3] = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument;

                // Return 시킬 페이지 정보를 넘겨주는 부분
                Session[Master.PARAM_TRANSFER] = Master.PAGE_REFLECT;

                Response.Redirect(Master.PAGE_NOAUTH + "?" + Master.PARAM_DATA1 + "=" + strBoardTy + "&" + Master.PARAM_DATA2 + "=" + strBoardCd, CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadArchiveData()
        {
            // 파라미터 체크
            CheckParams();

            int intCurrentPage = CommValue.NUMBER_VALUE_1;
            string strBoardTy = CommValue.BBS_TYPE_VALUE_DOWNLOAD;
            string strBoardCd = CommValue.DOWNLOAD_TYPE_VALUE_DOWNLOAD;
            string strKeyCd = string.Empty;
            string strKeyWord = string.Empty;

            DataSet dsReturn = new DataSet();

            // KN_USP_BRD_SELECT_BOARDINFO_S00
            dsReturn = BoardInfoBlo.SpreadBoardInfo(CommValue.BOARD_VALUE_MINIPAGESIZE, intCurrentPage, strBoardTy, strBoardCd, strKeyCd, strKeyWord, Session["MemAuthTy"].ToString(), Session["CompCd"].ToString(), Session["MemNo"].ToString());

            if (dsReturn != null)
            {
                lvArchiveList.DataSource = dsReturn.Tables[1];
                lvArchiveList.DataBind();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvArchiveList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvArchiveList.FindControl("ltTitle")).Text = TextNm["TITLE"];
            ((Literal)lvArchiveList.FindControl("ltInsDt")).Text = TextNm["DAY"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvArchiveList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의

                    ((Literal)e.Item.FindControl("ltTitle")).Text = TextNm["TITLE"];
                    ((Literal)e.Item.FindControl("ltInsDt")).Text = TextNm["DAY"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvArchiveList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["BoardTitle"].ToString()))
                {
                    // Int32의 크기 :  -2147483647 ~ 2147483647
                    int intDepth = Int32.Parse(drView["BoardDepth"].ToString());

                    StringBuilder sbDepth = new StringBuilder();
                    StringBuilder sbReply = new StringBuilder();
                    StringBuilder sbTitle = new StringBuilder();
                    StringBuilder sbDisk = new StringBuilder();

                    // 문자열이 특정 길이 이상일 경우 잘라주는 부분
                    if (drView["BoardTitle"].ToString().Length > 30)
                    {
                        // 제목이 60Bytes 이상일 경우 뒤는 자르고 '...'를 삽입함.
                        sbTitle.Append(TextLib.TextCutString(drView["BoardTitle"].ToString(), 25, "..."));
                    }
                    else
                    {
                        sbTitle.Append(drView["BoardTitle"].ToString());
                    }

                    ((LinkButton)e.Item.FindControl("lnkTitleList2")).Text = sbTitle.ToString();

                    ((TextBox)iTem.FindControl("txtHfSeq")).Text = drView["BoardSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ModDt"].ToString()))
                {
                    string strModDt = drView["ModDt"].ToString();
                    StringBuilder sbModDt = new StringBuilder();

                    if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                    {
                        sbModDt.Append(strModDt.Substring(4, 2));
                        sbModDt.Append(".");
                        sbModDt.Append(strModDt.Substring(6, 2));
                    }
                    else
                    {
                        sbModDt.Append(strModDt.Substring(6, 2));
                        sbModDt.Append("/");
                        sbModDt.Append(strModDt.Substring(4, 2));
                    }

                    ((Literal)iTem.FindControl("ltInsDtList")).Text = sbModDt.ToString();
                }
            }
        }

        protected void lnkTitleList2_Click(object sender, EventArgs e)
        {
            try
            {
                string strBoardTy = CommValue.BBS_TYPE_VALUE_DOWNLOAD;
                string strBoardCd = CommValue.DOWNLOAD_TYPE_VALUE_DOWNLOAD;
                string strBoardSeq = string.Empty;

                // 선택된 항목 넘겨주는 부분
                Session[Master.PARAM_DATA1] = strBoardTy;
                Session[Master.PARAM_DATA2] = strBoardCd;

                Session[Master.PARAM_DATA3] = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument;

                // Return 시킬 페이지 정보를 넘겨주는 부분
                Session[Master.PARAM_TRANSFER] = Master.PAGE_REFLECT;

                Response.Redirect(Master.PAGE_NOAUTH + "?" + Master.PARAM_DATA1 + "=" + strBoardTy + "&" + Master.PARAM_DATA2 + "=" + strBoardCd, CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadMemoData()
        {
            int intCurrentPage = 1;
            string strKeyCd = string.Empty;
            string strKeyWord = string.Empty;

            // 파라미터 체크
            CheckParams();

            DataSet dsReturn = new DataSet();
            dsReturn = MemoMngBlo.SpreadMemoInfo(CommValue.BOARD_VALUE_MINIPAGESIZE, intCurrentPage, Session["MemNo"].ToString(), strKeyCd, strKeyWord);

            if (dsReturn != null)
            {
                lvMemoList.DataSource = dsReturn.Tables[1];
                lvMemoList.DataBind();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMemoList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvMemoList.FindControl("ltTitle")).Text = TextNm["TITLE"];
            ((Literal)lvMemoList.FindControl("ltReceiveDate")).Text = TextNm["DAY"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMemoList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의

                    ((Literal)e.Item.FindControl("ltTitle")).Text = TextNm["TITLE"];
                    ((Literal)e.Item.FindControl("ltReceiveDate")).Text = TextNm["DAY"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMemoList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["MemoTitle"].ToString()))
                {
                    StringBuilder sbDepth = new StringBuilder();
                    StringBuilder sbReply = new StringBuilder();
                    StringBuilder sbTitle = new StringBuilder();
                    StringBuilder sbDisk = new StringBuilder();

                    // 문자열이 특정 길이 이상일 경우 잘라주는 부분
                    if (drView["MemoTitle"].ToString().Length > 30)
                    {
                        if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                        {
                            // 제목이 60Bytes 이상일 경우 뒤는 자르고 '...'를 삽입함.
                            sbTitle.Append(TextLib.TextCutString(drView["MemoTitle"].ToString(), 30, "..."));
                        }
                        else
                        {
                            // 제목이 30Bytes 이상일 경우 뒤는 자르고 '...'를 삽입함.
                            sbTitle.Append(TextLib.TextCutString(drView["MemoTitle"].ToString(), 25, "..."));
                        }
                    }
                    else
                    {
                        sbTitle.Append(drView["MemoTitle"].ToString());
                    }

                    ((LinkButton)e.Item.FindControl("lnkTitleList3")).Text = sbTitle.ToString();

                    ((TextBox)iTem.FindControl("txtHfSeq")).Text = drView["MemoSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDt"].ToString()))
                {
                    string strModDt = drView["InsDt"].ToString();
                    StringBuilder sbModDt = new StringBuilder();

                    if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                    {
                        sbModDt.Append(strModDt.Substring(4, 2));
                        sbModDt.Append(".");
                        sbModDt.Append(strModDt.Substring(6, 2));
                    }
                    else
                    {
                        sbModDt.Append(strModDt.Substring(6, 2));
                        sbModDt.Append("/");
                        sbModDt.Append(strModDt.Substring(4, 2));
                    }

                    ((Literal)iTem.FindControl("ltReceiveDateList")).Text = sbModDt.ToString();
                }
            }
        }

        protected void lnkTitleList3_Click(object sender, EventArgs e)
        {
            try
            {
                // 선택된 항목 넘겨주는 부분
                Session[Master.PARAM_DATA4] = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument;


                // Return 시킬 페이지 정보를 넘겨주는 부분
                Session[Master.PARAM_TRANSFER] = Master.PAGE_REDIRECT;

                Response.Redirect(Master.PAGE_TRANSFER, CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 데이터를 로드하는 메소드
        /// </summary>
        private void LoadSiteMapData()
        {

            DataTable dtReturn = new DataTable();

            // 사이트맵 리스트 조회
            dtReturn = SiteMapUtil.SpreadMainSiteMapInfo(Session["LangCd"].ToString(), Session["MemAuthTy"].ToString());

            if (dtReturn != null)
            {
                lvSiteMapList.DataSource = dtReturn;
                lvSiteMapList.DataBind();

            }
        }

        /// <summary>
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvSiteMapList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {

                StringBuilder sbContent = new StringBuilder();

                if (!string.IsNullOrEmpty(drView["Depth1"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["Depth2"].ToString()))
                    {
                        Literal ltContent = (Literal)iTem.FindControl("ltContent");
                        ltContent.Text = drView["MenuNm"].ToString();

                        StringBuilder sbDepth1 = new StringBuilder();

                        // Depth1 메뉴 
                        if (drView["Depth3"].ToString().Equals("0"))
                        {
                            if (isStart)
                            {
                                isStart = CommValue.AUTH_VALUE_FALSE;
                            }
                            else
                            {
                                if (drView["Depth1"].ToString().Equals("3") || drView["Depth1"].ToString().Equals("4"))
                                {
                                    if (intRowCnt >= 4)
                                    {
                                        // 네번째 사이트맵 에서 떨어뜨림
                                        if (intRowCnt % 4 == 0)
                                        {
                                            sbDepth1.Append("</table><div class='Clear'></div>");
                                            sbDepth1.Append("<table class='MainS'>");
                                        }
                                        else
                                        {
                                            sbDepth1.Append("</table><table class='MainS'>");
                                        }
                                    }
                                    else
                                    {
                                        sbDepth1.Append("</table><table class='MainS'>");

                                    }
                                }
                                else
                                {
                                    if (isSecondStart)
                                    {
                                        isSecondStart = CommValue.AUTH_VALUE_FALSE;
                                        intRowCnt = 0;
                                        sbDepth1.Append("</table><div class='Clear'></div>");
                                        sbDepth1.Append("<table class='MainS'>");
                                    }
                                    else
                                    {

                                        if (intRowCnt >= 5)
                                        {
                                            // 네번째 사이트맵 에서 떨어뜨림
                                            if (intRowCnt % 5 == 0)
                                            {
                                                sbDepth1.Append("</table><div class='Clear'></div>");
                                                sbDepth1.Append("<table class='MainS'>");
                                            }
                                            else
                                            {
                                                sbDepth1.Append("</table><table class='MainS'>");
                                            }
                                        }
                                        else
                                        {
                                            sbDepth1.Append("</table><table class='MainS'>");
                                        }
                                    }
                                }
                            }
                            intRowCnt++;
                            sbDepth1.Append("<th><span class='Tb-Tp-tit'>");
                            sbDepth1.Append(ltContent.Text);
                            sbDepth1.Append("</th>");
                        }

                        // Depth2 메뉴
                        else
                        {
                            sbDepth1.Append("<td>");
                            sbDepth1.Append("<a href=" + drView["MenuUrl"].ToString() + ">" + ltContent.Text + "</a>");
                            sbDepth1.Append("</td>");
                        }
                        ltContent.Text = sbContent.Append(sbDepth1).ToString();
                    }
                }
            }
        }

        protected void lnMore1_Click(object sender, EventArgs e)
        {
            try
            {
                strBoardTy = CommValue.BBS_TYPE_VALUE_BOARD;
                strBoardCd = CommValue.BOARD_TYPE_VALUE_NOTICE;
                string strBoardSeq = string.Empty;

                // 선택된 항목 넘겨주는 부분
                Session[Master.PARAM_DATA1] = strBoardTy;
                Session[Master.PARAM_DATA2] = strBoardCd;

                Session[Master.PARAM_DATA3] = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument;

                // Return 시킬 페이지 정보를 넘겨주는 부분
                Session[Master.PARAM_TRANSFER] = Master.PAGE_REFLECT;

                Response.Redirect(Master.PAGE_NOAUTH, CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}