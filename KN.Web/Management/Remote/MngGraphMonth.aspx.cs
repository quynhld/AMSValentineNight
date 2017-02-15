using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Manage.Biz;
using ZedGraph;

namespace KN.Web.Management.Remote
{
    public partial class MngGraphMonth : BasePage
    {
        DataTable dtListReturn = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
            {
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                txtHfChargeTy.Text = Request.Params[Master.PARAM_DATA2].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltFloor.Text = TextNm["FLOOR"];
            ltRoom.Text = TextNm["ROOMNO"];
            ltAmountUsed.Text = TextNm["AMOUNTUSED"];
            ltCharge.Text = TextNm["SPENDING"];
            lnkbtnYear.Text = TextNm["YEARS"];
            ltMonth.Text = TextNm["MONTH"];
            lnkbtnDay.Text = TextNm["DAYS"];
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnTable.Text = TextNm["TABLE"];

            MakeYearDdl();
            //AlertRemote();

            txtSearchFloor.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem(TextNm["YEARS"], ""));

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.AddYears(1).Year; intTmpI++)
            {
                ddlYear.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }

            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        private void AlertRemote()
        {
            //// KN_USP_MNG_SELECT_ENERGYMONTHCHECK_S00
            //DataTable dtRemoteWarn = RemoteMngBlo.WatchEnergyMonthCheck();

            //if (dtRemoteWarn != null)
            //{
            //    if (dtRemoteWarn.Rows.Count > CommValue.NUMBER_VALUE_0)
            //    {
            //        if (dtRemoteWarn.Rows[0]["RtnValue"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
            //        {
            //            ltEnergyWarning.Text = "<li><b><font color=\"red\" size=\"4\">" + AlertNm["ALERT_REMOTE_PROBLEMS"] + "</font></b></li>";
            //            imgbtnCheckData.Visible = Master.isModDelAuthOk;
            //        }
            //        else
            //        {
            //            ltEnergyWarning.Visible = CommValue.AUTH_VALUE_FALSE;
            //            imgbtnCheckData.Visible = CommValue.AUTH_VALUE_FALSE;
            //        }
            //    }
            //}
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            int intSearchFloor = 0;

            if (!string.IsNullOrEmpty(txtSearchFloor.Text))
            {
                intSearchFloor = Int32.Parse(txtSearchFloor.Text);
            }

            // KN_USP_MNG_SELECT_CHARGEINFO_S03
            dtListReturn = RemoteMngBlo.SpreadMonthUseChargeList(txtHfRentCd.Text, txtHfChargeTy.Text, ddlYear.Text, intSearchFloor, txtSearchRoom.Text);

            if (dtListReturn != null)
            {
                lvMngMonthReadView.DataSource = dtListReturn;
                lvMngMonthReadView.DataBind();

                InitializeComponent();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMngMonthReadView_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngMonthReadView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["EnergyMonth"].ToString()))
                {
                    string strDate = drView["EnergyMonth"].ToString();
                    Literal ltMonthVal = (Literal)iTem.FindControl("ltMonthVal");

                    ltMonthVal.Text = strDate.Substring(4, 2);
                }

                if (!string.IsNullOrEmpty(drView["EnergyRealUse"].ToString()))
                {
                    Literal ltTotalUse = (Literal)iTem.FindControl("ltTotalUse");
                    ltTotalUse.Text = TextLib.MakeVietIntNo((double.Parse(drView["EnergyRealUse"].ToString())).ToString("###,##0"));

                    if (txtHfChargeTy.Text.Equals(CommValue.CHARGETY_VALUE_ELECTRICITY))
                    {
                        ltTotalUse.Text = ltTotalUse.Text + " kW";
                    }
                    else if (txtHfChargeTy.Text.Equals(CommValue.CHARGETY_VALUE_WATER))
                    {
                        ltTotalUse.Text = ltTotalUse.Text + " <img src=\"/Common/Images/Icon/tons.png\" alt=\"tons\" height=\"13\" width=\"13\" style=\"vertical-align:text-bottom\"/>";
                    }
                    else if (txtHfChargeTy.Text.Equals(CommValue.CHARGETY_VALUE_GAS))
                    {
                        ltTotalUse.Text = ltTotalUse.Text + " <img src=\"/Common/Images/Icon/tons.png\" alt=\"tons\" height=\"13\" width=\"13\" style=\"vertical-align:text-bottom\"/>";
                    }
                }

                if (!string.IsNullOrEmpty(drView["EnergyRealPrice"].ToString()))
                {
                    Literal ltTotalCharge = (Literal)iTem.FindControl("ltTotalCharge");
                    ltTotalCharge.Text = TextLib.MakeVietIntNo((double.Parse(drView["EnergyRealPrice"].ToString())).ToString("###,##0")) + " VND";
                }
            }
        }

        protected void imgbtnRecompile_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_AGT_MAKE_MONTHENERGY_LST_M03
                RemoteMngBlo.RegistryMonthEnergyPO();

                Response.Redirect(Master.PAGE_POPUP1 + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnYear_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_POPUP2 + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDay_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REPLY + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 테이블 보기
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnTable_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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
            myPane.YAxis.Title.Text = TextNm["DONG"];

            // 디폴트값 생성
            int intTenCnt = 0;
            double dblMax = 0d;
            double dblMin = 99999999d;

            string[] strXLabels = new string[12];
            string[] strYLabels = new string[12];

            double[] dblXAxis = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            double[] dblExchange = new double[12];

            // 최근 12개 데이터 가져오는 부분
            if (dtListReturn.Rows.Count > 0)
            {
                foreach (DataRow dr in dtListReturn.Select())
                {
                    string strTmp = dr["EnergyMonth"].ToString();
                    strXLabels[intTenCnt] = strTmp.Substring(0, 4) + "." + strTmp.Substring(4, 2);

                    if (!string.IsNullOrEmpty(dr["EnergyRealPrice"].ToString()))
                    {
                        dblExchange[intTenCnt] = double.Parse(dr["EnergyRealPrice"].ToString());

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