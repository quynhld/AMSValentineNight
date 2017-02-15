using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Stock.Biz;
using KN.Common.Method.Lib;

namespace KN.Web.Stock.Warehousing
{
    public partial class StoredMngList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        string strViewDt = string.Empty;
        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    InitControls();

                    CheckParams();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void InitControls()
        {
            

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
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_STK_SELECT_WAREHOUSEINFO_S02
            dsReturn = WarehouseMngBlo.SpreadStoredInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value));

            if (dsReturn != null)
            {
                lvStoredOrderList.DataSource = dsReturn.Tables[1];
                lvStoredOrderList.DataBind();

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
        protected void lvStoredOrderList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvStoredOrderList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvStoredOrderList.FindControl("ltComp")).Text = TextNm["COMPNM"];
            ((Literal)lvStoredOrderList.FindControl("ltDept")).Text = TextNm["DEPT"];
            ((Literal)lvStoredOrderList.FindControl("ltItem")).Text = TextNm["ITEM"];
            ((Literal)lvStoredOrderList.FindControl("ltTotalAmount")).Text = TextNm["TOTALQTY"];
            ((Literal)lvStoredOrderList.FindControl("ltAmount")).Text = TextNm["RECEIPT"];
            ((Literal)lvStoredOrderList.FindControl("ltNoAmount")).Text = TextNm["NOTRECEIPT"];
            ((Literal)lvStoredOrderList.FindControl("ltTotalPrice")).Text = TextNm["TOTALAMT"];
            ((Literal)lvStoredOrderList.FindControl("ltReceitDt")).Text = TextNm["PAYPERIOD"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvStoredOrderList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltComp")).Text = TextNm["COMPNM"];
                    ((Literal)e.Item.FindControl("ltDept")).Text = TextNm["DEPT"];
                    ((Literal)e.Item.FindControl("ltItem")).Text = TextNm["ITEM"];
                    ((Literal)e.Item.FindControl("ltTotalAmount")).Text = TextNm["TOTALQTY"];
                    ((Literal)e.Item.FindControl("ltAmount")).Text = TextNm["RECEIPT"];
                    ((Literal)e.Item.FindControl("ltNoAmount")).Text = TextNm["NOTRECEIPT"];
                    ((Literal)e.Item.FindControl("ltTotalPrice")).Text = TextNm["TOTALAMT"];
                    ((Literal)e.Item.FindControl("ltReceitDt")).Text = TextNm["PAYPERIOD"];

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
        protected void lvStoredOrderList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["RealSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltSeqList")).Text = drView["RealSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltCompList")).Text = drView["CompNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DeptNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltDeptList")).Text = drView["DeptNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltItemList")).Text = drView["ClassNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RequestQty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltTotalAmountList")).Text = TextLib.MakeVietIntNo(Int32.Parse(drView["RequestQty"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["WarehousingQty"].ToString()))
                {


                    if (!string.IsNullOrEmpty(drView["RestQty"].ToString()))
                    {

                        if (drView["RestQty"].ToString().Equals("0"))
                        {
                            ((Literal)iTem.FindControl("ltAmountList")).Text = TextLib.MakeVietIntNo(Int32.Parse(drView["TotalQty"].ToString()).ToString("###,##0"));
                            ((Literal)iTem.FindControl("ltNoAmountList")).Text = "-";
                        }
                        else
                        {
                            ((Literal)iTem.FindControl("ltAmountList")).Text = TextLib.MakeVietIntNo(Int32.Parse(drView["TotalQty"].ToString()).ToString("###,##0"));
                            ((Literal)iTem.FindControl("ltNoAmountList")).Text = TextLib.MakeVietIntNo(Int32.Parse(drView["RestQty"].ToString()).ToString("###,##0"));

                        }
                    }
                    else
                    {
                        ((Literal)iTem.FindControl("ltAmountList")).Text = "-";
                        ((Literal)iTem.FindControl("ltNoAmountList")).Text = "-";
                    }
                    
                }
                else
                {
                    ((Literal)iTem.FindControl("ltAmountList")).Text = "-";
                    ((Literal)iTem.FindControl("ltNoAmountList")).Text = TextLib.MakeVietIntNo(Int32.Parse(drView["RequestQty"].ToString()).ToString("###,##0"));
                }                

                if (!string.IsNullOrEmpty(drView["TotalPrice"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltTotalPriceList")).Text = TextLib.MakeVietIntNo(double.Parse(drView["TotalPrice"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["DueDt"].ToString()))
                {
                    string strDueDt = drView["DueDt"].ToString();
                    
                    StringBuilder sbList = new StringBuilder();

                    sbList.Append(strDueDt.Substring(0, 4));
                    sbList.Append("-");
                    sbList.Append(strDueDt.Substring(4, 2));
                    sbList.Append("-");
                    sbList.Append(strDueDt.Substring(6, 2));

                    ((Literal)iTem.FindControl("ltReceitDtList")).Text = sbList.ToString();
                }
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

        /// <summary>
        /// 상세보기 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbList = new StringBuilder();

                sbList.Append(Master.PAGE_VIEW);
                sbList.Append("?");
                sbList.Append(Master.PARAM_DATA1);
                sbList.Append("=");
                sbList.Append(hfWarehouseSeq.Value);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA2);
                sbList.Append("=");
                sbList.Append(hfWarehouseDetSeq.Value);


                Response.Redirect(sbList.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

    }
}
