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

namespace KN.Web.Board.Memo
{
    public partial class MemoList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = CommValue.NUMBER_VALUE_0;

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
            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ISSUE"] + "')";
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
        }

        /// <summary>
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 파라미터 체크
            CheckParams();

            DataSet dsReturn = new DataSet();

            // KN_USP_BRD_SELECT_MEMOINFO_S00
            dsReturn = MemoMngBlo.SpreadMemoInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), Session["MemNo"].ToString(), ddlKeyCd.Text, txtKeyWord.Text);

            if (dsReturn != null)
            {
                lvMemoList.DataSource = dsReturn.Tables[1];
                lvMemoList.DataBind();

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
        protected void lvMemoList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의

            ((Literal)lvMemoList.FindControl("ltTitle")).Text = TextNm["TITLE"];
            ((Literal)lvMemoList.FindControl("ltReceiveDate")).Text = TextNm["RECEIVEDTIME"];
            ((Literal)lvMemoList.FindControl("ltSender")).Text = TextNm["SENDER"];
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
                    ((Literal)e.Item.FindControl("ltReceiveDate")).Text = TextNm["RECEIVEDTIME"];
                    ((Literal)e.Item.FindControl("ltSender")).Text = TextNm["SENDER"];

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
                    StringBuilder sbTitle = new StringBuilder();
                    StringBuilder sbDisk = new StringBuilder();

                    if (drView["CheckYn"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
                    {
                        // 문자열이 특정 길이 이상일 경우 잘라주는 부분
                        if (drView["MemoTitle"].ToString().Length > 30)
                        {
                            // 제목이 50Bytes 이상일 경우 뒤는 자르고 '...'를 삽입함.
                            sbTitle.Append("<b>");
                            sbTitle.Append("<a href=" + Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + drView["MemoSeq"].ToString() + ">");
                            sbTitle.Append(TextLib.StringDecoder(TextLib.TextCutString(drView["MemoTitle"].ToString(), 50, "...")));
                            Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
                            sbTitle.Append("</a>");
                            sbTitle.Append("</b>");
                        }
                        else
                        {
                            sbTitle.Append("<b>");
                            sbTitle.Append("<a href=" + Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + drView["MemoSeq"].ToString() + ">");
                            sbTitle.Append(TextLib.StringDecoder(drView["MemoTitle"].ToString()));
                            Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
                            sbTitle.Append("</a>");
                            sbTitle.Append("</b>");
                        }
                    }
                    else
                    {
                        // 문자열이 특정 길이 이상일 경우 잘라주는 부분
                        if (drView["MemoTitle"].ToString().Length > 30)
                        {
                            // 제목이 50Bytes 이상일 경우 뒤는 자르고 '...'를 삽입함.
                            sbTitle.Append("<a href=" + Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + drView["MemoSeq"].ToString() + ">");
                            sbTitle.Append(TextLib.StringDecoder(TextLib.TextCutString(drView["MemoTitle"].ToString(), 30, "...")));
                            Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
                            sbTitle.Append("</a>");
                        }
                        else
                        {
                            sbTitle.Append("<a href=" + Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + drView["MemoSeq"].ToString() + ">");
                            sbTitle.Append(TextLib.StringDecoder(drView["MemoTitle"].ToString()));
                            Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
                            sbTitle.Append("</a>");
                        }
                    }
                    TextBox txtHfMemoSeq = (TextBox)iTem.FindControl("txtHfMemoSeq");
                    txtHfMemoSeq.Text = drView["MeMoSeq"].ToString();

                    ((Literal)iTem.FindControl("ltInsTitle")).Text = sbTitle.ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDate"].ToString()))
                {

                    ((Literal)iTem.FindControl("ltInsReceiveDate")).Text = drView["InsDate"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsMemNo"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSender")).Text = drView["MemNm"].ToString();
                    ((TextBox)iTem.FindControl("txtHfInsSender")).Text = TextLib.StringDecoder(drView["InsMemNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["FilePath"].ToString()))
                {
                    TextBox txtHfFilePath = (TextBox)iTem.FindControl("txtHfFilePath");
                    txtHfFilePath.Text = TextLib.StringDecoder(drView["FilePath"].ToString());
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

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intTmpCnt = 0;

                for (int intTmpI = 0; intTmpI < lvMemoList.Items.Count; intTmpI++)
                {
                    CheckBox cbkMemoSeq = (CheckBox)lvMemoList.Items[intTmpI].FindControl("cbkMemoSeq");

                    if (cbkMemoSeq.Checked)
                    {
                        int intMemoSeq = Int32.Parse(((TextBox)lvMemoList.Items[intTmpI].FindControl("txtHfMemoSeq")).Text);

                        string strFilePath = ((TextBox)lvMemoList.Items[intTmpI].FindControl("txtHfFilePath")).Text;

                        // KN_USP_BRD_DELETE_MEMOINFO_M00
                        // KN_USP_BRD_DELETE_MEMOADDON_M00
                        MemoMngBlo.RemoveMemoInfo(intMemoSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strFilePath);

                        intTmpCnt++;
                    }
                }

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Session["MemNo"].ToString());
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllCheck = ((CheckBox)lvMemoList.FindControl("chkAll")).Checked;

            try
            {
                CheckAll(isAllCheck);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
        private void CheckAll(bool isAllCheck)
        {
            for (int intTmpI = 0; intTmpI < lvMemoList.Items.Count; intTmpI++)
            {
                ((CheckBox)lvMemoList.Items[intTmpI].FindControl("cbkMemoSeq")).Checked = isAllCheck;
            }
        }

        /// <summary>
        /// 리스트 각 행별 체크 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int intCheckCount = 0;

                for (int intTmpI = 0; intTmpI < lvMemoList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvMemoList.Items[intTmpI].FindControl("cbkMemoSeq")).Checked)
                    {
                        intCheckCount++;
                    }

                }

                if (intCheckCount == lvMemoList.Items.Count)
                {
                    ((CheckBox)lvMemoList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ((CheckBox)lvMemoList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;
                }
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
    }
}