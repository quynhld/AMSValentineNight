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

using KN.Config.Biz;

namespace KN.Web.Config.Log
{
    public partial class ErrorlogList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    CheckParams();

                    InitContorols();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 매개변수 처리 메소드
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
        /// 컨트롤 초기화
        /// </summary>
        private void InitContorols()
        {
            ltTerm.Text = TextNm["TERM"];
            lnkbtnSearch.Text = TextNm["SEARCH"];

            lnkbtnPrint.Text = "Report" + " " + TextNm["PRINT"];
            lnkbtnDel.Text = TextNm["DELETE"];
            lnkbtnDel.Attributes["onkeypress"] = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_SELECTED_ITEM"] + "');";
            lnkbtnDel.Visible = Master.isModDelAuthOk;

            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlKeyCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_EVENT, TextNm["SELECT"]);
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_COMM_SELECT_LOG_S00
            dsReturn = LogInfoBlo.SpreadLogInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), ddlKeyCd.Text, txtKeyWord.Text, hfStartDt.Value.Replace("-", ""), hfEndDt.Value.Replace("-", ""));

            if (dsReturn != null)
            {
                lvLogList.DataSource = dsReturn.Tables[1];
                lvLogList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// 레이아웃 처리 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLogList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvLogList.FindControl("ltTitleLogSeq")).Text = TextNm["SEQ"];
            ((Literal)lvLogList.FindControl("ltTitleUrl")).Text = TextNm["URL"];
            ((Literal)lvLogList.FindControl("ltTitleDate")).Text = TextNm["REGISTDATE"];
        }

        /// <summary>
        /// ListView 데이터 바인딩 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLogList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["LogSeq"].ToString()))
                {
                    Literal ltLogSeq = (Literal)iTem.FindControl("ltLogSeq");
                    ltLogSeq.Text = drView["LogSeq"].ToString();

                    TextBox txtHfSeq = (TextBox)iTem.FindControl("txtHfSeq");
                    txtHfSeq.Text = drView["LogSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ErrTy"].ToString()))
                {
                    TextBox txtHfErrTy = (TextBox)iTem.FindControl("txtHfErrTy");
                    txtHfErrTy.Text = drView["ErrTy"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDate"].ToString()))
                {
                    Literal ltDate = (Literal)iTem.FindControl("ltDate");
                    ltDate.Text = drView["InsDate"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ErrUrl"].ToString()))
                {
                    Literal ltUrl = (Literal)iTem.FindControl("ltUrl");
                    ltUrl.Text = drView["ErrUrl"].ToString();
                }
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLogList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltTitleLogSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltTitleUrl")).Text = TextNm["URL"];
                    ((Literal)e.Item.FindControl("ltTitleDate")).Text = TextNm["REGISTDATE"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        #region 관련 이벤트

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllCheck = ((CheckBox)lvLogList.FindControl("chkAll")).Checked;

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
            for (int intTmpI = 0; intTmpI < lvLogList.Items.Count; intTmpI++)
            {
                ((CheckBox)lvLogList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
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

                for (int intTmpI = 0; intTmpI < lvLogList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvLogList.Items[intTmpI].FindControl("chkboxList")).Checked)
                    {
                        intCheckCount++;
                    }

                }

                if (intCheckCount == lvLogList.Items.Count)
                {
                    ((CheckBox)lvLogList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ((CheckBox)lvLogList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 페이지 이동 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                ((CheckBox)lvLogList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 조회 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                ((CheckBox)lvLogList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 삭제 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intTmpCnt = 0;

                for (int intTmpI = 0; intTmpI < lvLogList.Items.Count; intTmpI++)
                {
                    CheckBox chkLogSeq = (CheckBox)lvLogList.Items[intTmpI].FindControl("chkboxList");

                    if (chkLogSeq.Checked)
                    {
                        int intLogSeq = Int32.Parse(((TextBox)lvLogList.Items[intTmpI].FindControl("txtHfSeq")).Text);
                        string strErrTy = ((TextBox)lvLogList.Items[intTmpI].FindControl("txtHfErrTy")).Text;

                        // KN_USP_COMM_DELETE_LOG_M00
                        LogInfoBlo.RemoveLogInfo(intLogSeq, strErrTy);

                        intTmpCnt++;
                    }
                }

                ((CheckBox)lvLogList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                LoadData();

                if (intTmpCnt > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RegistItem", "javascript:alert('" + AlertNm["INFO_DELETE_SELECTED_ITEM"] + "')", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RegistItem", "javascript:alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "')", CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        #endregion
    }
}
