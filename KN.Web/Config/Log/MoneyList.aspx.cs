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
    public partial class MoneyList : BasePage
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
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_COMM_SELECT_LOG_S04
            dsReturn = LogInfoBlo.SpreadMoneyLogInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), Session["LangCd"].ToString(), hfStartDt.Value.Replace("-", ""), hfEndDt.Value.Replace("-", ""));

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
            ((Literal)lvLogList.FindControl("ltSeq")).Text = TextNm["SEQ"];

            ((Literal)lvLogList.FindControl("ltRentNm")).Text = TextNm["RENT"];

            ((Literal)lvLogList.FindControl("ltItemNm")).Text = TextNm["ITEM"];
            ((Literal)lvLogList.FindControl("ltAmount")).Text = TextNm["AMT"];
            ((Literal)lvLogList.FindControl("ltInsMemNo")).Text = TextNm["MEMNO"];
            ((Literal)lvLogList.FindControl("ltInsMemIP")).Text = TextNm["IP"];
            ((Literal)lvLogList.FindControl("ltInsDt")).Text = TextNm["INSDT"];
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

                if (!string.IsNullOrEmpty(drView["RealSeq"].ToString()))
                {
                    Literal ltInsSeq = (Literal)iTem.FindControl("ltInsSeq");
                    ltInsSeq.Text = drView["RealSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentNm"].ToString()))
                {
                    Literal ltInsRentNm = (Literal)iTem.FindControl("ltInsRentNm");
                    ltInsRentNm.Text = drView["RentNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ItemNm"].ToString()))
                {
                    Literal ltInsItemNm = (Literal)iTem.FindControl("ltInsItemNm");
                    ltInsItemNm.Text = drView["ItemNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["Amt"].ToString()))
                {
                    Literal ltInsAmount = (Literal)iTem.FindControl("ltInsAmount");
                    ltInsAmount.Text = TextLib.MakeVietIntNo(double.Parse(drView["Amt"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["InsMemNo"].ToString()))
                {
                    Literal ltInsMemNoList = (Literal)iTem.FindControl("ltInsMemNoList");
                    ltInsMemNoList.Text = drView["InsMemNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsMemIP"].ToString()))
                {
                    Literal ltInsMemIPList = (Literal)iTem.FindControl("ltInsMemIPList");
                    ltInsMemIPList.Text = drView["InsMemIP"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDate"].ToString()))
                {
                    Literal ltInsDtList = (Literal)iTem.FindControl("ltInsDtList");

                    ((Literal)iTem.FindControl("ltInsDtList")).Text = drView["InsDate"].ToString();
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

                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltRentNm")).Text = TextNm["RENT"];

                    ((Literal)e.Item.FindControl("ltItemNm")).Text = TextNm["ITEM"];
                    ((Literal)e.Item.FindControl("ltAmount")).Text = TextNm["AMT"];
                    ((Literal)e.Item.FindControl("ltInsMemNo")).Text = TextNm["MEMNO"];
                    ((Literal)e.Item.FindControl("ltInsMemIP")).Text = TextNm["IP"];
                    ((Literal)e.Item.FindControl("ltInsDt")).Text = TextNm["INSDT"];

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
        /// 검색버튼 클릭시 이벤트 처리
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

                LoadData();
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

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}