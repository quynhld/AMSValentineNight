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
using KN.Resident.Biz;

using ZedGraph;

namespace KN.Web.Management.Remote
{
    public partial class MngGraphDay : BasePage
    {
        DataSet dsReturn = new DataSet();

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

                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3]))
                {
                    txtHfEnergyYear.Text = Request.Params[Master.PARAM_DATA3].ToString();
                    txtHfEnergyMonth.Text = Request.Params[Master.PARAM_DATA4].ToString();
                }

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltFloor.Text = TextNm["FLOOR"];
            ltRoom.Text = TextNm["ROOMNO"];

            lnkbtnYear.Text = TextNm["YEARS"];
            lnkbtnMonth.Text = TextNm["MONTH"];
            ltTabDay.Text = TextNm["DAYS"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnTable.Text = TextNm["TABLE"];

            MakeYearDdl();
            MakeMonthDdl();
            //AlertRemote();

            MakeSearchFloor();
            MakeSearchRoom();

            lnkbtnModify.Text = TextNm["MODIFY"];
        }

        protected void LoadData()
        {
            int intSearchFloor = CommValue.NUMBER_VALUE_0;
            int intEndDay = CommValue.NUMBER_VALUE_0;
            string strYear = string.Empty;
            string strMonth = string.Empty;

            if (!string.IsNullOrEmpty(ddlSearchFloor.SelectedValue))
            {
                intSearchFloor = Int32.Parse(ddlSearchFloor.SelectedValue);
            }

            if (string.IsNullOrEmpty(ddlYear.SelectedValue))
            {
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
                strYear = DateTime.Now.Year.ToString();
            }
            else
            {
                strYear = ddlYear.SelectedValue;
            }

            if (string.IsNullOrEmpty(ddlMonth.SelectedValue))
            {
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
                strMonth = DateTime.Now.Month.ToString().PadLeft(2, '0');
            }
            else
            {
                strMonth = ddlMonth.SelectedValue;
            }

            DateTime dtEndDate = DateTime.ParseExact(strYear + "-" + strMonth + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

            intEndDay = dtEndDate.AddMonths(CommValue.NUMBER_VALUE_1).AddDays(-1 * CommValue.NUMBER_VALUE_1).Day;

            // KN_USP_MNG_SELECT_CHARGEINFO_S05
            dsReturn = RemoteMngBlo.SpreadDayUseChargeList(intEndDay, CommValue.BOARD_VALUE_DEFAULTPAGE, txtHfRentCd.Text, txtHfChargeTy.Text, ddlYear.Text, ddlMonth.Text, intSearchFloor, ddlSearchRoom.SelectedValue);

            if (dsReturn != null)
            {
                InitializeComponent();
            }
        }

        private void MakeSearchFloor()
        {
            // KN_USP_RES_SELECT_ROOMINFO_S03
            // KN_USP_RES_SELECT_ROOMINFO_S04
            DataTable dtReturn = RoomMngBlo.SpreadRoomList(txtHfRentCd.Text, CommValue.NUMBER_VALUE_0);

            if (dtReturn != null)
            {
                ddlSearchFloor.Items.Clear();

                foreach (DataRow dr in dtReturn.Select())
                {
                    ddlSearchFloor.Items.Add(new ListItem(dr["FloorNo"].ToString(), dr["FloorNo"].ToString()));
                }
            }
        }

        private void MakeSearchRoom()
        {
            // KN_USP_RES_SELECT_ROOMINFO_S03
            // KN_USP_RES_SELECT_ROOMINFO_S04
            DataTable dtReturn = RoomMngBlo.SpreadRoomList(txtHfRentCd.Text, Int32.Parse(ddlSearchFloor.SelectedValue));

            if (dtReturn != null)
            {
                ddlSearchRoom.Items.Clear();

                foreach (DataRow dr in dtReturn.Select())
                {
                    ddlSearchRoom.Items.Add(new ListItem(dr["RoomNo"].ToString(), dr["RoomNo"].ToString()));
                }
            }
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

            ddlYear.SelectedValue = txtHfEnergyYear.Text;
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl()
        {
            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem(TextNm["MONTH"], ""));

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            ddlMonth.SelectedValue = txtHfEnergyMonth.Text;
        }

        private void AlertRemote()
        {
            //// KN_USP_MNG_SELECT_ENERGYMONTHCHECK_S00
            //DataTable dtRemoteWarn = RemoteMngBlo.WatchEnergyMonthCheck();

            //if (dtRemoteWarn != null)
            //{
            //    if (dtRemoteWarn.Rows.Count > CommValue.NUMBER_VALUE_0)
            //    {
            //        //if (dtRemoteWarn.Rows[0]["RtnValue"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
            //        //{
            //        //    ltEnergyWarning.Text = "<li><b><font color=\"red\" size=\"4\">" + AlertNm["ALERT_REMOTE_PROBLEMS"] + "</font></b></li>";
            //        //    imgbtnCheckData.Visible = Master.isModDelAuthOk;
            //        //}
            //        //else
            //        //{
            //        //    ltEnergyWarning.Visible = CommValue.AUTH_VALUE_FALSE;
            //        //    imgbtnCheckData.Visible = CommValue.AUTH_VALUE_FALSE;
            //        //}
            //    }
            //}
        }

        protected void ddlSearchFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeSearchRoom();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text + "?" + Master.PARAM_DATA3 + "=" + ddlYear.SelectedValue + "&" + Master.PARAM_DATA4 + "=" + ddlMonth.SelectedValue, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
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

                Response.Redirect(Master.PAGE_REPLY + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnMonth_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_POPUP1 + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text + "&" + Master.PARAM_DATA3 + "=" + ddlYear.Text + "&" + Master.PARAM_DATA4 + "=" + ddlMonth.Text, CommValue.AUTH_VALUE_FALSE);
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

            string[] strXLabels = new string[31];
            string[] strYLabels = new string[31];

            double[] dblXAxis = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
            double[] dblExchange = new double[31];

            // 최근 10개 데이터 가져오는 부분
            DataTable dtReturn = dsReturn.Tables[1];

            if (dtReturn.Rows.Count > 0)
            {
                foreach (DataRow dr in dtReturn.Select())
                {
                    string strDate = dr["EnergyDay"].ToString();
                    strXLabels[intTenCnt] = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2);

                    if (!string.IsNullOrEmpty(dr["TotalCharge"].ToString()))
                    {
                        dblExchange[intTenCnt] = double.Parse(dr["TotalCharge"].ToString());

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