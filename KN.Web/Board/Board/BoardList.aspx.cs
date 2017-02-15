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

namespace KN.Web.Board.Board
{
    public partial class BoardList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = CommValue.NUMBER_VALUE_0;

        string strBoardTy = string.Empty;
        string strBoardCd = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // 컨트롤 초기화
                    InitControls();

                    // 데이터 로드 및 바인딩
                    LoadData();
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
            // DropDownList Setting
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlKeyCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_BOARD);

            // Button Setting
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnWrite.Text = TextNm["WRITE"];
            lnkbtnWrite.Visible = Master.isWriteAuthOk;
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {
            if (!string.IsNullOrEmpty(hfCurrentPage.Value))
            {
                intPageNo = Int32.Parse(hfCurrentPage.Value);
                hfCurrentPage.Value = intPageNo.ToString();
            }
            else
            {
                intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfCurrentPage.Value = intPageNo.ToString();
            }

            if (Request.Params[Master.PARAM_DATA1] == null)
            {
                strBoardTy = CommValue.BBS_TYPE_VALUE_BOARD;
                txtHfBoardTy.Text = strBoardTy;
            }
            else
            {
                strBoardTy = Request.Params[Master.PARAM_DATA1].ToString();
                txtHfBoardTy.Text = strBoardTy;
            }

            if (Request.Params[Master.PARAM_DATA2] == null)
            {
                strBoardCd = CommValue.BOARD_TYPE_VALUE_NOTICE;
                txtHfBoardCd.Text = strBoardCd;
            }
            else
            {
                strBoardCd = Request.Params[Master.PARAM_DATA2].ToString();
                txtHfBoardCd.Text = strBoardCd;
            }

            StringBuilder sbLink = new StringBuilder();
            sbLink.Append(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=");
            sbLink.Append(strBoardTy);
            sbLink.Append("&" + Master.PARAM_DATA2 + "=");
            sbLink.Append(strBoardCd);

            lnkbtnWrite.PostBackUrl = sbLink.ToString();
        }

        /// <summary>
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadData()
        {

            // 파라미터 체크
            CheckParams();

            DataSet dsReturn = new DataSet();

            // KN_USP_BRD_SELECT_BOARDINFO_S00
            dsReturn = BoardInfoBlo.SpreadBoardInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfBoardTy.Text, txtHfBoardCd.Text, ddlKeyCd.Text, txtKeyWord.Text, Session["MemAuthTy"].ToString(), Session["CompCd"].ToString(), Session["MemNo"].ToString());

            if (dsReturn != null)
            {
                lvBoardList.DataSource = dsReturn.Tables[1];
                lvBoardList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
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
            ((Literal)lvBoardList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvBoardList.FindControl("ltTitle")).Text = TextNm["TITLE"];
            ((Literal)lvBoardList.FindControl("ltInsDt")).Text = TextNm["INSDT"];
            ((Literal)lvBoardList.FindControl("ltViewCnt")).Text = TextNm["VIEWCNT"];
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
                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltTitle")).Text = TextNm["TITLE"];
                    ((Literal)e.Item.FindControl("ltInsDt")).Text = TextNm["INSDT"];
                    ((Literal)e.Item.FindControl("ltViewCnt")).Text = TextNm["VIEWCNT"];

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

                if (!string.IsNullOrEmpty(drView["RealSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltLSeq")).Text = drView["RealSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["BoardTitle"].ToString()))
                {
                    // Int32의 크기 :  -2147483647 ~ 2147483647
                    int intDepth = Int32.Parse(drView["BoardDepth"].ToString());

                    StringBuilder sbDepth = new StringBuilder();
                    StringBuilder sbReply = new StringBuilder();
                    StringBuilder sbTitle = new StringBuilder();
                    StringBuilder sbDisk = new StringBuilder();

                    // 세션체크
                    AuthCheckLib.CheckSession();

                    sbTitle.Append("<a href='" + Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=");
                    sbTitle.Append(strBoardTy);
                    sbTitle.Append("&" + Master.PARAM_DATA2 + "=");
                    sbTitle.Append(strBoardCd);
                    sbTitle.Append("&" + Master.PARAM_DATA3 + "=");
                    sbTitle.Append(drView["BoardSeq"].ToString());
                    sbTitle.Append("'>");

                    // Depth가 2단계 이상부터는 앞에 Blank 이미지를 삽입
                    if (intDepth > 1)
                    {
                        sbDepth.Append("<img src='/Common/Images/Common/blank.gif' width='");
                        sbDepth.Append((intDepth * 13).ToString());
                        sbDepth.Append("px' height='1px' valign='absmiddle' style='border:0'/>");
                    }

                    // Depth가 1단계 이상부터는 앞에 Reply 이미지를 삽입
                    if (intDepth > 0)
                    {
                        sbReply.Append("<img src='/Common/Images/Common/reply.gif' valign='absmiddle' style='border:0'/>");
                        sbReply.Append("<img src='/Common/Images/Common/blank.gif' width='3px' height='1px' valign='absmiddle' style='border:0'/>");
                    }

                    sbTitle.Append(sbDepth);
                    sbTitle.Append(sbReply);

                    // 문자열이 특정 길이 이상일 경우 잘라주는 부분
                    if (drView["BoardTitle"].ToString().Length > 30)
                    {
                        // 제목이 60Bytes 이상일 경우 뒤는 자르고 '...'를 삽입함.
                        sbTitle.Append(TextLib.TextCutString(drView["BoardTitle"].ToString(), 60, "..."));
                    }
                    else
                    {
                        sbTitle.Append(drView["BoardTitle"].ToString());
                    }

                    if (!string.IsNullOrEmpty(drView["FilePath1"].ToString()) ||
                        !string.IsNullOrEmpty(drView["FilePath2"].ToString()) ||
                        !string.IsNullOrEmpty(drView["FilePath3"].ToString()))
                    {
                        sbDisk.Append("<img src='/Common/Images/Common/blank.gif' width='3' valign='absmiddle' style='border:0'/>");
                        sbDisk.Append("<img src='/Common/Images/Common/disk.gif' title='첨부파일' valign='absmiddle' style='border:0'/>");
                    }

                    sbTitle.Append(sbDisk);

                    sbTitle.Append("</a>");

                    ((Literal)iTem.FindControl("ltLTitle")).Text = sbTitle.ToString();
                }

                if (!string.IsNullOrEmpty(drView["ModDt"].ToString()))
                {
                    string strModDt = drView["ModDt"].ToString();
                    StringBuilder sbModDt = new StringBuilder();

                    sbModDt.Append(strModDt.Substring(0, 4));
                    sbModDt.Append(".");
                    sbModDt.Append(strModDt.Substring(4, 2));
                    sbModDt.Append(".");
                    sbModDt.Append(strModDt.Substring(6, 2));

                    ((Literal)iTem.FindControl("ltLInsDt")).Text = sbModDt.ToString();
                }

                if (!string.IsNullOrEmpty(drView["ViewCnt"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltLViewCnt")).Text = drView["ViewCnt"].ToString();
                }
            }
        }

        /// <summary>
        /// 페이징버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 검색버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 등록버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    StringBuilder sbLink = new StringBuilder();
            //    sbLink.Append(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=");
            //    sbLink.Append(txtHfBoardTy.Text);
            //    sbLink.Append("&" + Master.PARAM_DATA2 + "=");
            //    sbLink.Append(txtHfBoardCd.Text);

            //    Response.Redirect(sbLink.ToString(), CommValue.AUTH_VALUE_FALSE);
            //}
            //catch (Exception ex)
            //{
            //    ErrLogger.MakeLogger(ex);
            //}
        }
    }
}