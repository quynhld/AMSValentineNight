using System;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Stock.Biz;

using ZedGraph;
using System.Data;
using System.Drawing;

namespace KN.Web.Common.Popup
{
    public partial class PopupGoodsGraph : BasePage
    {
        public const string PARAM_DATA1 = "MainCd";
        DataSet dsReturn = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (CheckParams())
                {
                    InitControls();
                    LoadData();
                }
                else
                {
                    StringBuilder sbWarning = new StringBuilder();

                    sbWarning.Append("alert('");
                    sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                    sbWarning.Append("');");
                    sbWarning.Append("self.close();");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            //if (Session["GraphPopup"] != null)
            //{
            //    if (Session["GraphPopup"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
            //    {
            //        if (Request.Params[""] != null)
            //        {
            //            if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA1].ToString()))
            //            {
            //                isReturn = CommValue.AUTH_VALUE_TRUE;
            //                txtParams.Text = Request.Params[PARAM_DATA1].ToString();
            //                Session["GraphPopup"] = null;
            //            }
            //        }
            //    }
            //}

            if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA1].ToString()))
            {
                isReturn = CommValue.AUTH_VALUE_TRUE;
                txtParams.Text = Request.Params[PARAM_DATA1].ToString();
            }

            return isReturn;
        }

        protected void InitControls()
        {
            ltEntireInventory.Text = TextNm["ENTIRE"] + " " + TextNm["INVENTORYREPORT"];
            ltInventory.Text = TextNm["INVENTORYREPORT"];
        }

        protected void LoadData()
        {
            string strItem = txtParams.Text;

            string strRentCd = strItem.Substring(0, 4);
            string strSvcZoneCd = strItem.Substring(4, 4);
            string strClassiGrpCd = strItem.Substring(8, 4);
            string strClassMainCd = strItem.Substring(12, 4);
            string strClassSubCd = strItem.Substring(16, 4);

            // KN_USP_STK_SELECT_RELEASECHARGERINFO_S01
            dsReturn = MaterialMngBlo.WatchGoodsGraphInfo(strRentCd, strSvcZoneCd, strClassiGrpCd, strClassMainCd, strClassSubCd);

            if (dsReturn != null)
            {
                InitializeComponent();
            }
        }


        #region Make Graph

        private void InitializeComponent()
        {
            this.zgwEntireGoodsGraph.RenderGraph += new ZedGraph.Web.ZedGraphWebControlEventHandler(this.MakeGraph);
        }

        private void MakeGraph(ZedGraph.Web.ZedGraphWeb z, System.Drawing.Graphics g, ZedGraph.MasterPane masterPane)
        {
            GraphPane myPane = masterPane[0];

            // 각 축별 타이틀 생성
            myPane.XAxis.Title.Text = TextNm["DATE"];
            myPane.YAxis.Title.Text = TextNm["DONG"];

            // 디폴트값 생성
            int intTenCnt = 0;
            double dblMax = 0d;
            double dblMin = 9999999999999999d;

            string[] strXLabels = new string[12];
            string[] strYLabels = new string[30];

            double[] dblXAxis = new double[12];
            double[] dblExchange = new double[30];

            // 최근 12개 데이터 가져오는 부분
            DataTable dtReturn = dsReturn.Tables[0];

            if (dtReturn.Rows.Count > 0)
            {
                foreach (DataRow dr in dtReturn.Select())
                {
                    string strDate = dr["PayedDt"].ToString();
                    strXLabels[intTenCnt] = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2);

                    if (!string.IsNullOrEmpty(dr["ReceiptQty"].ToString()))
                    {
                        dblExchange[intTenCnt] = double.Parse(dr["ReceiptQty"].ToString());

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
            oLineItem = myPane.AddCurve("Use", dblXAxis, dblExchange, Color.Blue, SymbolType.Circle);

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
