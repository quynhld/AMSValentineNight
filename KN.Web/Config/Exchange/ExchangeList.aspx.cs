using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;
using ZedGraph;

namespace KN.Web.Config.Exchange
{
    public partial class ExchangeList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    CheckParams();

                    RemoveOtherForex();

                    InitControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void CheckParams()
        {
            string strRentCd = string.Empty;

            if (Request.Params[Master.PARAM_DATA2] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                {
                    txtHfRentCd.Text = Request.Params[Master.PARAM_DATA2].ToString();
                }
                else
                {
                    hfExchangeSeq.Value = CommValue.NUMBER_VALUE_ONE;
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
                }
            }
            else
            {
                hfExchangeSeq.Value = CommValue.NUMBER_VALUE_ONE;
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
            }

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

        protected void InitControls()
        {
            ltDate.Text = TextNm["BASEDATE"];
            ltBaseRate.Text = TextNm["BASERATE"];
            ltFluctAmt.Text = TextNm["FLUCTAMT"];
            ltFluctRatio.Text = TextNm["FLUCTRATIO"];

            lnkbtnWrite.Text = TextNm["WRITE"];
            lnkbtnWrite.Visible = Master.isWriteAuthOk;
        }

        protected void RemoveOtherForex()
        {
            // 달러 및 동을 제외한 환율정보 삭제
            // KN_USP_MNG_DELETE_CURRENCYINFO_M00
            ExchangeMngBlo.RemoveCurrencyInfo();
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // 가장 최근의 환율을 조회함.
            // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S01
            dtReturn = ExchangeMngBlo.WatchExchangeRateLastInfo(txtHfRentCd.Text);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["AppliedDt"].ToString()))
                    {
                        StringBuilder sb = new StringBuilder();
                        string strData = dtReturn.Rows[0]["AppliedDt"].ToString();

                        sb.Append(strData.Substring(0, 4));
                        sb.Append(".");
                        sb.Append(strData.Substring(4, 2));
                        sb.Append(".");
                        sb.Append(strData.Substring(6, 2));

                        ltInsDate.Text = sb.ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltInsBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0"));
                    }
                    else
                    {
                        ltInsBaseRate.Text = "-";
                    }

                    MakeFloat(dtReturn.Rows[0]["DifferAmt"].ToString(), ltInsFluctAmt);
                    MakeFloat(dtReturn.Rows[0]["DifferRatio"].ToString(), ltInsFluctRatio);

                    // 그래프 생성
                    InitializeComponent();

                    // 리스트 생성
                    DataSet dsReturn = new DataSet();

                    // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S03
                    dsReturn = ExchangeMngBlo.SpreadExchangeRateListInfo(CommValue.BOARD_VALUE_EXCHAGEPAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text);

                    if (dsReturn != null)
                    {
                        lvExchageList.DataSource = dsReturn.Tables[1];
                        lvExchageList.DataBind();

                        sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_EXCHAGEPAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                            , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                        spanPageNavi.InnerHtml = sbPageNavi.ToString();
                    }
                }
                else
                {
                    // 등록된 환율정보가 없을 경우 등록페이지로 바로 보냄
                    Response.Redirect(Master.PAGE_WRITE + "?" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
                }
            }
        }

        private void MakeFloat(object objParms, Literal ltParams)
        {
            if (!string.IsNullOrEmpty(objParms.ToString()))
            {
                string strFluctAmt = objParms.ToString();
                string strFluctIcon = string.Empty;

                if (double.Parse(strFluctAmt) < 0)
                {
                    strFluctIcon = "<img src='/common/images/icon/down.gif' width='5' height='7' alt='down' align='absmiddle'/>&nbsp;";
                }
                else if (double.Parse(strFluctAmt) > 0)
                {
                    strFluctIcon = "<img src='/common/images/icon/up.gif' width='5' height='7' alt='down' align='absmiddle'/>&nbsp;+";
                }
                else
                {
                    strFluctIcon = "<img src='/common/images/icon/equa.gif' width='5' height='7' alt='equal' align='absmiddle'/>";
                }

                ltParams.Text = strFluctIcon + " " + TextLib.MakeVietRealNo(double.Parse(strFluctAmt).ToString());
            }
            else
            {
                ltParams.Text = "<img src='/common/images/icon/equa.gif' width='5' height='7' alt='equal'/>"; ;
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvExchageList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvExchageList.FindControl("ltDate")).Text = TextNm["BASEDATE"];
            ((Literal)lvExchageList.FindControl("ltBaseRate")).Text = TextNm["BASERATE"];
            ((Literal)lvExchageList.FindControl("ltFluctAmt")).Text = TextNm["FLUCTAMT"];
            ((Literal)lvExchageList.FindControl("ltFluctRatio")).Text = TextNm["FLUCTRATIO"];
            ((Literal)lvExchageList.FindControl("ltCash")).Text = TextNm["CASH"];
            ((Literal)lvExchageList.FindControl("ltWireTrans")).Text = TextNm["WIRETRANS"];
            ((Literal)lvExchageList.FindControl("ltBuying")).Text = TextNm["BUYING"];
            ((Literal)lvExchageList.FindControl("ltSelling")).Text = TextNm["SELLING"];
            ((Literal)lvExchageList.FindControl("ltSending")).Text = TextNm["SENDING"];
            ((Literal)lvExchageList.FindControl("ltReceiving")).Text = TextNm["RECEIVING"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvExchageList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltDate")).Text = TextNm["BASEDATE"];
                    ((Literal)e.Item.FindControl("ltBaseRate")).Text = TextNm["BASERATE"];
                    ((Literal)e.Item.FindControl("ltFluctAmt")).Text = TextNm["FLUCTAMT"];
                    ((Literal)e.Item.FindControl("ltFluctRatio")).Text = TextNm["FLUCTRATIO"];
                    ((Literal)e.Item.FindControl("ltCash")).Text = TextNm["CASH"];
                    ((Literal)e.Item.FindControl("ltWireTrans")).Text = TextNm["WIRETRANS"];
                    ((Literal)e.Item.FindControl("ltBuying")).Text = TextNm["BUYING"];
                    ((Literal)e.Item.FindControl("ltSelling")).Text = TextNm["SELLING"];
                    ((Literal)e.Item.FindControl("ltSending")).Text = TextNm["SENDING"];
                    ((Literal)e.Item.FindControl("ltReceiving")).Text = TextNm["RECEIVING"];

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
        protected void lvExchageList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["AppliedDt"].ToString()))
                {
                    StringBuilder sb = new StringBuilder();
                    string strData = drView["AppliedDt"].ToString();

                    sb.Append(strData.Substring(0, 4));
                    sb.Append(".");
                    sb.Append(strData.Substring(4, 2));
                    sb.Append(".");
                    sb.Append(strData.Substring(6, 2));

                    ((Literal)iTem.FindControl("ltInsDate")).Text = sb.ToString();
                }

                if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
                {
                    StringBuilder sb = new StringBuilder();
                    string strData = drView["DongToDollar"].ToString();

                    ((Literal)iTem.FindControl("ltInsBaseRate")).Text = TextLib.MakeVietRealNo(double.Parse(strData).ToString("###,##0.##"));
                }

                MakeFloat(drView["DifferAmt"].ToString(), ((Literal)iTem.FindControl("ltInsFluctAmt")));
                MakeFloat(drView["DifferRatio"].ToString(), ((Literal)iTem.FindControl("ltInsFluctRatio")));

                if (!string.IsNullOrEmpty(drView["CashBuy"].ToString()))
                {
                    StringBuilder sb = new StringBuilder();
                    string strData = drView["CashBuy"].ToString();

                    ((Literal)iTem.FindControl("ltInsBuying")).Text = TextLib.MakeVietRealNo(double.Parse(strData).ToString("###,##0.##"));
                }

                if (!string.IsNullOrEmpty(drView["CashSell"].ToString()))
                {
                    StringBuilder sb = new StringBuilder();
                    string strData = drView["CashSell"].ToString();

                    ((Literal)iTem.FindControl("ltInsSelling")).Text = TextLib.MakeVietRealNo(double.Parse(strData).ToString("###,##0.##"));
                }

                if (!string.IsNullOrEmpty(drView["TransferSend"].ToString()))
                {
                    StringBuilder sb = new StringBuilder();
                    string strData = drView["TransferSend"].ToString();

                    ((Literal)iTem.FindControl("ltInsSending")).Text = TextLib.MakeVietRealNo(double.Parse(strData).ToString("###,##0.##"));
                }

                if (!string.IsNullOrEmpty(drView["TransferRecieve"].ToString()))
                {
                    StringBuilder sb = new StringBuilder();
                    string strData = drView["TransferRecieve"].ToString();

                    ((Literal)iTem.FindControl("ltInsReceiving")).Text = TextLib.MakeVietRealNo(double.Parse(strData).ToString("###,##0.##"));
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
        /// 목록 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + hfExchangeSeq.Value + "&" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 쓰기버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_WRITE + "?" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        #region Make Graph

        private void InitializeComponent()
        {
            this.zgwGraph.RenderGraph += new ZedGraph.Web.ZedGraphWebControlEventHandler(this.MakeGraph);
        }

        private void MakeGraph(ZedGraph.Web.ZedGraphWeb z, System.Drawing.Graphics g, ZedGraph.MasterPane masterPane)
        {
            GraphPane myPane = masterPane[0];

            // 각 축별 타이틀 생성
            myPane.XAxis.Title.Text = TextNm["DATE"];
            myPane.YAxis.Title.Text = TextNm["DONG"] + " / 1" + TextNm["DOLLAR"];

            // 디폴트값 생성
            int intTenCnt = 0;
            double dblMax = 0d;
            double dblMin = 99999999d;

            string[] strXLabels = new string[10];
            string[] strYLabels = new string[10];

            double[] dblXAxis = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            double[] dblExchange = new double[10];

            // 최근 10개 데이터 가져오는 부분
            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S02
            dtReturn = ExchangeMngBlo.WatchExchangeRateLastTenInfo(txtHfRentCd.Text);

            if (dtReturn.Rows.Count > 0)
            {
                foreach (DataRow dr in dtReturn.Select())
                {
                    string strTmp = dr["AppliedDt"].ToString();
                    strXLabels[intTenCnt] = strTmp.Substring(4, 2) + "/" + strTmp.Substring(6, 2);

                    if (!string.IsNullOrEmpty(dr["DongToDollar"].ToString()))
                    {
                        dblExchange[intTenCnt] = double.Parse(dr["DongToDollar"].ToString());

                        if (dblMin < dblExchange[intTenCnt])
                        {
                            dblMin = dblExchange[intTenCnt];
                        }

                        if (dblMax > dblExchange[intTenCnt])
                        {
                            dblMax = dblExchange[intTenCnt];
                        }
                    }

                    intTenCnt++;
                }
            }

            // 최근 데이터가 10개 미만일 경우 나머지는 중간 값으로 대신 입력하는 부분
            for (int intTmpI = intTenCnt; intTmpI < intTenCnt; intTmpI++)
            {
                strXLabels[intTmpI] = "";
                dblExchange[intTenCnt] = (dblMin + dblMax) / 2;
            }

            // 최소값 생성
            if (dblMin > 100d)
            {
                dblMin = dblMin - 100d;
            }

            // 최대값 생성
            dblMax = dblMax + 100d;

            // 지도 그릴 간격 생성하는 부분
            for (int intTmpJ = 0; intTmpJ < 10; intTmpJ++)
            {
                strYLabels[intTmpJ] = (dblMin + (dblMax - dblMin) / 10d).ToString();
            }

            // 라인 명칭 명명 및 특성 배정    
            LineItem oLineItem;
            oLineItem = myPane.AddCurve("Excange Ratio", dblXAxis, dblExchange, Color.Blue, SymbolType.Circle);

            // 각 축에 레이블 배정하는 부분
            myPane.XAxis.Scale.TextLabels = strXLabels;
            myPane.YAxis.Scale.TextLabels = strYLabels;
            myPane.XAxis.Type = AxisType.Text;
            myPane.XAxis.MajorTic.IsBetweenLabels = CommValue.AUTH_VALUE_TRUE;

            // 배경색 지정
            myPane.Chart.Fill = new Fill(Color.White, Color.FromArgb(255, 255, 166), 45.0F);

            masterPane.AxisChange(g);
        }

        #endregion
    }
}