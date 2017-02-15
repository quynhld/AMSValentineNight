using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;
using KN.Manage.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Management.Remote
{
    public partial class UtilFeeReceiptForTower : BasePage
    {
        protected int intRowCnt = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    CheckParam();

                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                }
            }
        }

        protected void InitControls()
        {
            string strFeeTyTxt = string.Empty;
            string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
            string strNowDay = strNowDt.Substring(6, 2);
            string strEndDt = string.Empty;
            DateTime dtEndDate = DateTime.ParseExact(DateTime.Now.ToString("s").Substring(0, 7).ToString() + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

            ltInsRoomNo.Text = TextNm["ROOMNO"];
            ltInsPaidCd.Text = TextNm["PAYYN"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnExcelReport.Text = "EXCEL " + TextNm["PRINT"];

            ltTopRoomNo.Text = TextNm["ROOMNO"];
            ltTopName.Text = TextNm["NAME"];
            ltTopApplyDt.Text = TextNm["APPLYDT"];
            ltTopElecFee.Text = TextNm["ELECFEE"];
            ltTopWatFee.Text = TextNm["WATFEE"];
            ltTopGasFee.Text = TextNm["GASFEE"];
            //ltTopDetailView.Text = TextNm["DETAILVIEW"];

            ltRoomNo.Text = TextNm["ROOMNO"];
            ltEnterMonth.Text = TextNm["OCCUDT"];
            ltElecFee.Text = TextNm["ELECFEE"];
            ltWatFee.Text = TextNm["WATFEE"];
            ltGasFee.Text = TextNm["GASFEE"];
            ltTotalFee.Text = TextNm["ENTIRE"];
            ltPaymentCd.Text = TextNm["TOTALPAY"];

            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];

            lnkbtnRegist.Text = TextNm["REGIST"];

            MakeYearDdl();
            MakeMonthDdl();

            // DropDownList를 인자값에 따라서 변경처리
            MakeRentalDdl(ddlInsRentCd);
            CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaidCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_PAYMENT);
            CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaymentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);

            MakeAccountDdl(ddlTransfer);

            LoadExchageDate();

            ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;

            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            if (intRowCnt > CommValue.NUMBER_VALUE_0)
            {
                chkAll.Enabled = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                chkAll.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.Year; intTmpI++)
            {
                ddlYear.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }

            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl()
        {
            ddlMonth.Items.Clear();

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            ResetInputControls();

            DataTable dtReturn = new DataTable();

            intRowCnt = CommValue.NUMBER_VALUE_0;

            chkAll.Checked = CommValue.AUTH_VALUE_FALSE;

            // KN_USP_MNG_SELECT_MONTHENERGY_S09
            dtReturn = RemoteMngBlo.SpreadMonthUseEnergyListForTower(ddlInsRentCd.SelectedValue, txtInsRoomNo.Text, ddlPaidCd.SelectedValue);

            if (dtReturn != null)
            {
                lvUtilFeeList.DataSource = dtReturn;
                lvUtilFeeList.DataBind();
            }

            if (intRowCnt > CommValue.NUMBER_VALUE_0)
            {
                chkAll.Enabled = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                chkAll.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }

        private void MakeRentalDdl(DropDownList ddlParam)
        {
            DataTable dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            ddlParam.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APT))
                {
                    if (dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTA) ||
                        dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTB))
                    {
                        ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
                else if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APTSHOP))
                {
                    if (dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                        dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                    {
                        ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
                else
                {
                    if (dr["CodeCd"].ToString().Equals(txtHfRentCd.Text))
                    {
                        ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
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

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvUtilFeeList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvUtilFeeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            int intEnabledCnt = CommValue.NUMBER_VALUE_0;
            int intNotEnabledCnt = CommValue.NUMBER_VALUE_0;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                CheckBox chkLineList = (CheckBox)iTem.FindControl("chkLineList");
                CheckBox chkElecItem = (CheckBox)iTem.FindControl("chkElecItem");
                CheckBox chkWatItem = (CheckBox)iTem.FindControl("chkWatItem");
                CheckBox chkGasItem = (CheckBox)iTem.FindControl("chkGasItem");

                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    chkLineList.Attributes["onclick"] = "javascript:return fnChangedList('" + drView["Seq"].ToString() + "');";
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    TextBox txtHfRentCd = (TextBox)iTem.FindControl("txtHfRentCd");

                    txtHfRentCd.Text = drView["RentCd"].ToString();
                    txtHfRegRentCd.Text = drView["RentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    txtHfFloorNo.Text = drView["FloorNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                    TextBox txtHfRoomNo = (TextBox)iTem.FindControl("txtHfRoomNo");

                    ltRoomNo.Text = drView["RoomNo"].ToString();
                    txtHfRoomNo.Text = drView["RoomNo"].ToString();
                    ltRegRoomNo.Text = drView["RoomNo"].ToString();
                    txtHfRegRoomNo.Text = drView["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    Literal ltName = (Literal)iTem.FindControl("ltName");
                    TextBox txtHfName = (TextBox)iTem.FindControl("txtHfName");

                    ltName.Text = TextLib.TextCutString(drView["UserNm"].ToString(), 20, "..");
                    txtHfName.Text = drView["UserNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
                {
                    txtHfDongToDollar.Text = drView["DongToDollar"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["EnergyMonth"].ToString()))
                {
                    string strEnergyMonth = string.Empty;
                    Literal ltEnergyMonth = (Literal)iTem.FindControl("ltEnergyMonth");
                    TextBox txtHfEnergyMonth = (TextBox)iTem.FindControl("txtHfEnergyMonth");

                    strEnergyMonth = drView["EnergyMonth"].ToString();
                    ltEnergyMonth.Text = strEnergyMonth.Substring(4, 2) + "/" + strEnergyMonth.Substring(0, 4);
                    txtHfEnergyMonth.Text = drView["EnergyMonth"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["EnterDt"].ToString()))
                {
                    TextBox txtHfEnterMonth = (TextBox)iTem.FindControl("txtHfEnterMonth");

                    txtHfEnterMonth.Text = drView["EnterDt"].ToString();
                    ltRegEnterMonth.Text = drView["EnterDt"].ToString();
                    txtHfEnterMonth.Text = drView["EnterDt"].ToString();
                }

                TextBox txtElecFee = (TextBox)iTem.FindControl("txtElecFee");
                TextBox txtHfElecFee = (TextBox)iTem.FindControl("txtHfElecFee");

                if (!string.IsNullOrEmpty(drView["RequestEleAmt"].ToString()))
                {
                    txtElecFee.Text = drView["RequestEleAmt"].ToString();
                    txtHfElecFee.Text = drView["RequestEleAmt"].ToString();
                }

                txtElecFee.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

                TextBox txtWatFee = (TextBox)iTem.FindControl("txtWatFee");
                TextBox txtHfWatFee = (TextBox)iTem.FindControl("txtHfWatFee");

                if (!string.IsNullOrEmpty(drView["RequestWatAmt"].ToString()))
                {
                    txtWatFee.Text = drView["RequestWatAmt"].ToString();
                    txtHfWatFee.Text = drView["RequestWatAmt"].ToString();
                }

                txtWatFee.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

                TextBox txtGasFee = (TextBox)iTem.FindControl("txtGasFee");
                TextBox txtHfGasFee = (TextBox)iTem.FindControl("txtHfGasFee");

                if (!string.IsNullOrEmpty(drView["RequestGasAmt"].ToString()))
                {
                    txtGasFee.Text = drView["RequestGasAmt"].ToString();
                    txtHfGasFee.Text = drView["RequestGasAmt"].ToString();
                }

                txtGasFee.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

                ImageButton imgbtnElecCheck = (ImageButton)iTem.FindControl("imgbtnElecCheck");
                imgbtnElecCheck.OnClientClick = "javascript:return fnCheckProcess('" + AlertNm["CONF_PAYED_ENTIRE"] + "');";

                ImageButton imgbtnWatCheck = (ImageButton)iTem.FindControl("imgbtnWatCheck");
                imgbtnWatCheck.OnClientClick = "javascript:return fnCheckProcess('" + AlertNm["CONF_PAYED_ENTIRE"] + "');";

                ImageButton imgbtnGasCheck = (ImageButton)iTem.FindControl("imgbtnGasCheck");
                imgbtnGasCheck.OnClientClick = "javascript:return fnCheckProcess('" + AlertNm["CONF_PAYED_ENTIRE"] + "');";

                if (!string.IsNullOrEmpty(drView["ElecReceitCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["WatReceitCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["GasReceitCd"].ToString()))
                {
                    if ((drView["ElecReceitCd"].ToString().Equals(CommValue.PAYMENT_TYPE_VALUE_PAID)) ||
                       (double.Parse(drView["ElectricityUse"].ToString()) == CommValue.NUMBER_VALUE_0_0))
                    {
                        chkElecItem.Enabled = CommValue.AUTH_VALUE_FALSE;
                        txtElecFee.Enabled = CommValue.AUTH_VALUE_FALSE;
                        imgbtnElecCheck.Visible = CommValue.AUTH_VALUE_FALSE;
                        intNotEnabledCnt++;
                    }
                    else
                    {
                        chkElecItem.Enabled = CommValue.AUTH_VALUE_TRUE;
                        txtElecFee.Enabled = CommValue.AUTH_VALUE_TRUE;
                        imgbtnElecCheck.Visible = CommValue.AUTH_VALUE_TRUE;
                        intEnabledCnt++;
                    }

                    if ((drView["WatReceitCd"].ToString().Equals(CommValue.PAYMENT_TYPE_VALUE_PAID)) ||
                       (double.Parse(drView["WaterUse"].ToString()) == CommValue.NUMBER_VALUE_0_0) ||
                       (double.Parse(txtWatFee.Text) == CommValue.NUMBER_VALUE_0_0))
                    {
                        chkWatItem.Enabled = CommValue.AUTH_VALUE_FALSE;
                        txtWatFee.Enabled = CommValue.AUTH_VALUE_FALSE;
                        imgbtnWatCheck.Visible = CommValue.AUTH_VALUE_FALSE;
                        intNotEnabledCnt++;
                    }
                    else
                    {
                        chkWatItem.Enabled = CommValue.AUTH_VALUE_TRUE;
                        txtWatFee.Enabled = CommValue.AUTH_VALUE_TRUE;
                        imgbtnWatCheck.Visible = CommValue.AUTH_VALUE_TRUE;
                        intEnabledCnt++;
                    }

                    if ((drView["GasReceitCd"].ToString().Equals(CommValue.PAYMENT_TYPE_VALUE_PAID)) ||
                       (double.Parse(drView["GasUse"].ToString()) == CommValue.NUMBER_VALUE_0_0))
                    {
                        chkGasItem.Enabled = CommValue.AUTH_VALUE_FALSE;
                        txtGasFee.Enabled = CommValue.AUTH_VALUE_FALSE;
                        imgbtnGasCheck.Visible = CommValue.AUTH_VALUE_FALSE;
                        intNotEnabledCnt++;
                    }
                    else
                    {
                        chkGasItem.Enabled = CommValue.AUTH_VALUE_TRUE;
                        txtGasFee.Enabled = CommValue.AUTH_VALUE_TRUE;
                        imgbtnGasCheck.Visible = CommValue.AUTH_VALUE_TRUE;
                        intEnabledCnt++;
                    }

                    if (intNotEnabledCnt == CommValue.NUMBER_VALUE_3)
                    {
                        chkLineList.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        chkLineList.Enabled = CommValue.AUTH_VALUE_TRUE;
                        intRowCnt++;
                    }
                }

                //ImageButton imgbtnDetailView = (ImageButton)iTem.FindControl("imgbtnDetailView");
                //imgbtnDetailView.OnClientClick = "javascript:return fnDetailView('" + drView["RentCd"].ToString() + "','" + drView["RoomNo"].ToString() + "','" + drView["EnergyMonth"].ToString() + "');";
            }
        }

        /// <summary>
        /// list내의 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool isAllCheck = chkAll.Checked;

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
        protected void CheckAll(bool isParams)
        {
            double dblEleReturn = 0d;
            double dblWatReturn = 0d;
            double dblGasReturn = 0d;
            double dblTotalPaid = 0d;

            for (int intTmpI = 0; intTmpI < lvUtilFeeList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkLineList")).Enabled)
                {
                    ((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkLineList")).Checked = isParams;

                    dblEleReturn = dblEleReturn + CheckElecItem(isParams, intTmpI);
                    dblWatReturn = dblWatReturn + CheckWatItem(isParams, intTmpI);
                    dblGasReturn = dblGasReturn + CheckGasItem(isParams, intTmpI);
                }
            }

            dblTotalPaid = dblTotalPaid + dblEleReturn + dblWatReturn + dblGasReturn;

            ltRegElecFee.Text = dblEleReturn.ToString("###,##0");
            txtHfRegElecFee.Text = dblEleReturn.ToString();
            ltRegWatFee.Text = dblWatReturn.ToString("###,##0");
            txtHfRegWatFee.Text = dblWatReturn.ToString();
            ltRegGasFee.Text = dblGasReturn.ToString("###,##0");
            txtHfRegGasFee.Text = dblGasReturn.ToString();
            ltRegTotalFee.Text = dblTotalPaid.ToString("###,##0");
            txtHfRegTotalFee.Text = dblTotalPaid.ToString();
        }

        /// <summary>
        /// 리스트 체크시 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnListChange_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int intCheckCnt = 0;
                int intDisabledCnt = 0;
                int intSelectedLine = 0;

                double dblEleReturn = 0d;
                double dblWatReturn = 0d;
                double dblGasReturn = 0d;
                double dblTotalPaid = 0d;

                if (!string.IsNullOrEmpty(hfSelectedLine.Value))
                {
                    intSelectedLine = Int32.Parse(hfSelectedLine.Value);

                    if (((CheckBox)lvUtilFeeList.Items[intSelectedLine].FindControl("chkLineList")).Checked)
                    {
                        CheckElecItem(CommValue.AUTH_VALUE_TRUE, intSelectedLine);
                        CheckWatItem(CommValue.AUTH_VALUE_TRUE, intSelectedLine);
                        CheckGasItem(CommValue.AUTH_VALUE_TRUE, intSelectedLine);
                    }
                    else
                    {
                        CheckElecItem(CommValue.AUTH_VALUE_FALSE, intSelectedLine);
                        CheckWatItem(CommValue.AUTH_VALUE_FALSE, intSelectedLine);
                        CheckGasItem(CommValue.AUTH_VALUE_FALSE, intSelectedLine);
                    }

                    for (int intTmpI = 0; intTmpI < lvUtilFeeList.Items.Count; intTmpI++)
                    {
                        if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkLineList")).Enabled)
                        {
                            if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkLineList")).Checked)
                            {
                                TextBox txtElecFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtElecFee");

                                if (txtElecFee.Enabled)
                                {
                                    if (!string.IsNullOrEmpty(txtElecFee.Text))
                                    {
                                        dblEleReturn = dblEleReturn + double.Parse(txtElecFee.Text);
                                    }
                                }

                                TextBox txtWatFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtWatFee");

                                if (txtWatFee.Enabled)
                                {
                                    if (!string.IsNullOrEmpty(txtWatFee.Text))
                                    {
                                        dblWatReturn = dblWatReturn + double.Parse(txtWatFee.Text);
                                    }
                                }

                                TextBox txtGasFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtGasFee");

                                if (txtGasFee.Enabled)
                                {
                                    if (!string.IsNullOrEmpty(txtGasFee.Text))
                                    {
                                        dblGasReturn = dblGasReturn + double.Parse(txtGasFee.Text);
                                    }
                                }

                                intCheckCnt++;
                            }
                        }
                        else
                        {
                            intDisabledCnt++;
                        }
                    }

                    dblTotalPaid = dblTotalPaid + dblEleReturn + dblWatReturn + dblGasReturn;

                    ltRegElecFee.Text = dblEleReturn.ToString("###,##0");
                    txtHfRegElecFee.Text = dblEleReturn.ToString();
                    ltRegWatFee.Text = dblWatReturn.ToString("###,##0");
                    txtHfRegWatFee.Text = dblWatReturn.ToString();
                    ltRegGasFee.Text = dblGasReturn.ToString("###,##0");
                    txtHfRegGasFee.Text = dblGasReturn.ToString();
                    ltRegTotalFee.Text = dblTotalPaid.ToString("###,##0");
                    txtHfRegTotalFee.Text = dblTotalPaid.ToString();

                    if (intCheckCnt == (lvUtilFeeList.Items.Count - intDisabledCnt))
                    {
                        chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                    }
                }

                hfSelectedLine.Value = String.Empty;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 각 요금 아이템 체크시 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnItemChange_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int intCheckCnt = CommValue.NUMBER_VALUE_0;

                double dblEleReturn = 0d;
                double dblWatReturn = 0d;
                double dblGasReturn = 0d;
                double dblTotalPaid = 0d;

                for (int intTmpI = 0; intTmpI < lvUtilFeeList.Items.Count; intTmpI++)
                {
                    int intItemCnt = CommValue.NUMBER_VALUE_0;
                    int intDisabledCnt = CommValue.NUMBER_VALUE_0;

                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkElecItem")).Enabled)
                    {
                        if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkElecItem")).Checked)
                        {
                            dblEleReturn = dblEleReturn + CheckElecItem(CommValue.AUTH_VALUE_TRUE, intTmpI);
                            intItemCnt++;
                        }
                    }
                    else
                    {
                        intDisabledCnt++;
                    }

                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkWatItem")).Enabled)
                    {
                        if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkWatItem")).Checked)
                        {
                            dblWatReturn = dblWatReturn + CheckWatItem(CommValue.AUTH_VALUE_TRUE, intTmpI);
                            intItemCnt++;
                        }
                    }
                    else
                    {
                        intDisabledCnt++;
                    }

                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkGasItem")).Enabled)
                    {
                        if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkGasItem")).Checked)
                        {
                            dblGasReturn = dblGasReturn + CheckGasItem(CommValue.AUTH_VALUE_TRUE, intTmpI);
                            intItemCnt++;
                        }
                    }
                    else
                    {
                        intDisabledCnt++;
                    }

                    if (intItemCnt == CommValue.NUMBER_VALUE_3 - intDisabledCnt)
                    {
                        ((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkLineList")).Checked = CommValue.AUTH_VALUE_TRUE;
                        intCheckCnt++;
                    }
                    else
                    {
                        ((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkLineList")).Checked = CommValue.AUTH_VALUE_FALSE;
                    }
                }

                dblTotalPaid = dblTotalPaid + dblEleReturn + dblWatReturn + dblGasReturn;

                ltRegElecFee.Text = dblEleReturn.ToString("###,##0");
                txtHfRegElecFee.Text = dblEleReturn.ToString();
                ltRegWatFee.Text = dblWatReturn.ToString("###,##0");
                txtHfRegWatFee.Text = dblWatReturn.ToString();
                ltRegGasFee.Text = dblGasReturn.ToString("###,##0");
                txtHfRegGasFee.Text = dblGasReturn.ToString();
                ltRegTotalFee.Text = dblTotalPaid.ToString("###,##0");
                txtHfRegTotalFee.Text = dblTotalPaid.ToString();

                if (intCheckCnt == lvUtilFeeList.Items.Count)
                {
                    chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlPaymentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPaymentCd.SelectedValue == CommValue.PAYMENT_TYPE_VALUE_TRANSFER)
                {
                    ddlTransfer.Enabled = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ddlTransfer.SelectedValue = string.Empty;
                    ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private double CheckElecItem(bool isParams, int intParams)
        {
            double dblReturn = 0d;

            if (((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkElecItem")).Enabled)
            {
                ((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkElecItem")).Checked = isParams;

                if (((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkElecItem")).Checked)
                {
                    TextBox txtElecFee = (TextBox)lvUtilFeeList.Items[intParams].FindControl("txtElecFee");

                    if (!string.IsNullOrEmpty(txtElecFee.Text))
                    {
                        dblReturn = double.Parse(txtElecFee.Text);
                    }
                }
            }

            return dblReturn;
        }

        private double CheckWatItem(bool isParams, int intParams)
        {
            double dblReturn = 0d;

            if (((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkWatItem")).Enabled)
            {
                ((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkWatItem")).Checked = isParams;

                if (((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkWatItem")).Checked)
                {
                    TextBox txtWatFee = (TextBox)lvUtilFeeList.Items[intParams].FindControl("txtWatFee");

                    if (!string.IsNullOrEmpty(txtWatFee.Text))
                    {
                        dblReturn = double.Parse(txtWatFee.Text);
                    }
                }
            }

            return dblReturn;
        }

        private double CheckGasItem(bool isParams, int intParams)
        {
            double dblReturn = 0d;

            if (((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkGasItem")).Enabled)
            {
                ((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkGasItem")).Checked = isParams;

                if (((CheckBox)lvUtilFeeList.Items[intParams].FindControl("chkGasItem")).Checked)
                {
                    TextBox txtGasFee = (TextBox)lvUtilFeeList.Items[intParams].FindControl("txtGasFee");

                    if (!string.IsNullOrEmpty(txtGasFee.Text))
                    {
                        dblReturn = double.Parse(txtGasFee.Text);
                    }
                }
            }

            return dblReturn;
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (txtHfRegTotalFee.Text != null)
                {
                    if (string.IsNullOrEmpty(txtHfRegTotalFee.Text))
                    {
                        StringBuilder sbList = new StringBuilder();
                        sbList.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelectAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    else
                    {
                        DataTable dtLedgerAccnt = new DataTable();
                        DataTable dtLedgerDet = new DataTable();
                        DataTable dtPrintOut = new DataTable();

                        string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                        string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                        string strDirectCd = CommValue.DIRECT_TYPE_VALUE_DIRECT;
                        string strUserSeq = txtHfUserSeq.Text;
                        string strRentCd = txtHfRegRentCd.Text;
                        string strFloorNo = txtHfFloorNo.Text;
                        string strRoomNo = txtHfRegRoomNo.Text;
                        string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                        string strPaymentCd = ddlPaymentCd.SelectedValue;
                        string strVatRatio = string.Empty;
                        string strPrintSeq = string.Empty;
                        string strPrintDetSeq = string.Empty;

                        double dblDongToDollar = CommValue.NUMBER_VALUE_0_0;
                        double dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                        double dblItemTotViAmt = CommValue.NUMBER_VALUE_0_0;
                        double dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                        double dblUniPrime = CommValue.NUMBER_VALUE_0_0;
                        double dblPayedFee = CommValue.NUMBER_VALUE_0_0;

                        int intPaymentSeq = CommValue.NUMBER_VALUE_0;
                        int intPaymentDetSeq = CommValue.NUMBER_VALUE_0;
                        int intItemSeq = CommValue.NUMBER_VALUE_0;

                        #region 전기세 수납 처리

                        /* 전기세 수납 처리 시작 */

                        if (!string.IsNullOrEmpty(txtHfRegElecFee.Text))
                        {
                            dblItemTotViAmt = double.Parse(txtHfRegElecFee.Text);
                        }
                        else
                        {
                            dblItemTotViAmt = CommValue.NUMBER_VALUE_0_0;
                        }

                        if (!string.IsNullOrEmpty(txtHfDongToDollar.Text))
                        {
                            dblDongToDollar = double.Parse(txtHfDongToDollar.Text);
                            dblItemTotEnAmt = dblItemTotViAmt / dblDongToDollar;
                        }
                        else
                        {
                            dblDongToDollar = CommValue.NUMBER_VALUE_0_0;
                            dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                        }

                        if (dblItemTotViAmt > CommValue.NUMBER_VALUE_0_0)
                        {
                            // 0. 부가세 조회
                            // KN_USP_MNG_SELECT_VATINFO_S00
                            DataTable dtVatRatio = VatMngBlo.WatchVatInfo(CommValue.ITEM_TYPE_VALUE_ELECCHARGE);

                            if (dtVatRatio != null)
                            {
                                if (dtVatRatio.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    strVatRatio = dtVatRatio.Rows[0]["VatRatio"].ToString();
                                    dblVatRatio = double.Parse(strVatRatio);
                                    dblUniPrime = dblItemTotViAmt * (100) / (100 + dblVatRatio);
                                }
                                else
                                {
                                    dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                    dblUniPrime = dblItemTotViAmt;
                                }
                            }
                            else
                            {
                                dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                dblUniPrime = dblItemTotViAmt;
                            }

                            // 1. 각종 세금 처리 원장등록
                            // KN_USP_SET_INSERT_LEDGERINFO_S00
                            dtLedgerAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strNowDt, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, CommValue.ITEM_TYPE_VALUE_ELECCHARGE,
                                                                             CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, strUserSeq, string.Empty, dblDongToDollar,
                                                                             dblItemTotEnAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio,
                                                                             Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                            if (dtLedgerAccnt != null)
                            {
                                if (dtLedgerAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["PaymentSeq"].ToString());
                                    intItemSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["ItemSeq"].ToString());

                                    // 계좌이체시 관련 정보 등록
                                    if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                                    {
                                        // 2. 납입 계좌정보 등록
                                        // KN_USP_SET_INSERT_LEDGERINFO_S01
                                        BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strNowDt, intPaymentSeq, ddlTransfer.SelectedValue);
                                    }
                                }
                            }

                            for (int intTmpI = 0; intTmpI < lvUtilFeeList.Items.Count; intTmpI++)
                            {
                                if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkElecItem")).Enabled)
                                {
                                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkElecItem")).Checked)
                                    {
                                        TextBox txtElecFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtElecFee");
                                        TextBox txtHfElecFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtHfElecFee");
                                        TextBox txtHfEnergyMonth = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtHfEnergyMonth");

                                        if (!string.IsNullOrEmpty(txtElecFee.Text))
                                        {
                                            if (double.Parse(txtElecFee.Text) > CommValue.NUMBER_VALUE_0_0)
                                            {
                                                dblPayedFee = double.Parse(txtElecFee.Text);

                                                dblUniPrime = dblPayedFee * (100) / (100 + dblVatRatio);

                                                // 3. 전기세 수납
                                                // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_S00
                                                DataTable dtUtilReturn = RemoteMngBlo.ModifyUtilityFeeForTower(strRentCd, CommValue.CHARGETY_VALUE_ELECTRICITY, txtHfEnergyMonth.Text, strRoomNo, strPaymentCd,
                                                                                                               dblPayedFee, Session["CompCd"].ToString(), Session["MemNo"].ToString());

                                                if (dtUtilReturn != null)
                                                {
                                                    if (dtUtilReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                                    {
                                                        int intPaySeq = Int32.Parse(dtUtilReturn.Rows[0]["PaySeq"].ToString());

                                                        // 4. 원장 상세 테이블 처리
                                                        // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                                        dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strNowDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                                          strDirectCd, CommValue.ITEM_TYPE_VALUE_ELECCHARGE, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                                          CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblPayedFee, dblPayedFee,
                                                                                                          CommValue.NUMBER_VALUE_0, txtHfEnergyMonth.Text.Substring(0, 4), txtHfEnergyMonth.Text.Substring(4, 2), "Electricity", dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                                          Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                                                        if (dtLedgerDet != null)
                                                        {
                                                            if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                                            {
                                                                intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                                            }
                                                        }

                                                        // 5. 납부금액과 청구 금액이 같으면 완납처리
                                                        // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M01
                                                        if (txtElecFee.Text.Equals(txtHfElecFee.Text))
                                                        {
                                                            RemoteMngBlo.ModifyMonthReceitForTower(strRentCd, txtHfEnergyMonth.Text.Substring(0, 6), strRoomNo, CommValue.CHARGETY_VALUE_ELECTRICITY, CommValue.PAYMENT_TYPE_VALUE_PAID);
                                                        }

                                                        // 6. Utility Fee Sequence 할당
                                                        // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M02
                                                        RemoteMngBlo.ModifyMonthEnergySeqForTower(strRentCd, txtHfEnergyMonth.Text.Substring(0, 6), strRoomNo, CommValue.CHARGETY_VALUE_ELECTRICITY, intPaySeq, strNowDt, intPaymentSeq);

                                                        // 7. 출력 테이블에 등록
                                                        // KN_USP_SET_INSERT_PRINTINFO_S00
                                                        dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_ELECCHARGE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                                                           Int32.Parse(strFloorNo), strRoomNo, txtHfEnergyMonth.Text.Substring(0, 4), txtHfEnergyMonth.Text.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                                           Session["MemNo"].ToString(), txtHfEnergyMonth.Text.Substring(0, 4) + " / " + txtHfEnergyMonth.Text.Substring(4, 2) + " Electricity Charge ( " + strRoomNo + " )",
                                                                                                           dblPayedFee, dblDongToDollar, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, strDebitCreditCd,
                                                                                                           strNowDt, intPaymentSeq, intPaymentDetSeq);

                                                        if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                                                        {
                                                            strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                                            strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                                            // 8. 출력 정보 원장상세 테이블에 등록
                                                            // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                                            BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));

                                                            // 9. 출력자 테이블에 등록
                                                            // KN_USP_SET_INSERT_PRINTINFO_S01
                                                            ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                                        }

                                                        // 10.금액 로그 테이블 처리
                                                        // KN_USP_SET_INSERT_MONEYINFO_M00
                                                        ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        /* 전기세 수납 처리 끝 */

                        #endregion

                        #region 수도세 수납 처리

                        /* 수도세 수납 처리 시작 */

                        if (!string.IsNullOrEmpty(txtHfRegWatFee.Text))
                        {
                            dblItemTotViAmt = double.Parse(txtHfRegWatFee.Text);
                        }
                        else
                        {
                            dblItemTotViAmt = CommValue.NUMBER_VALUE_0_0;
                        }

                        if (!string.IsNullOrEmpty(txtHfDongToDollar.Text))
                        {
                            dblDongToDollar = double.Parse(txtHfDongToDollar.Text);
                            dblItemTotEnAmt = dblItemTotViAmt / dblDongToDollar;
                        }
                        else
                        {
                            dblDongToDollar = CommValue.NUMBER_VALUE_0_0;
                            dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                        }

                        if (dblItemTotViAmt > CommValue.NUMBER_VALUE_0_0)
                        {
                            // 0. 부가세 조회
                            // KN_USP_MNG_SELECT_VATINFO_S00
                            DataTable dtVatRatio = VatMngBlo.WatchVatInfo(CommValue.ITEM_TYPE_VALUE_WATERCHARGE);

                            if (dtVatRatio != null)
                            {
                                if (dtVatRatio.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    strVatRatio = dtVatRatio.Rows[0]["VatRatio"].ToString();
                                    dblVatRatio = double.Parse(strVatRatio);
                                    dblUniPrime = dblItemTotViAmt * (100) / (100 + dblVatRatio);
                                }
                                else
                                {
                                    dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                    dblUniPrime = dblItemTotViAmt;
                                }
                            }
                            else
                            {
                                dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                dblUniPrime = dblItemTotViAmt;
                            }

                            // 1. 각종 세금 처리 원장등록
                            // KN_USP_SET_INSERT_LEDGERINFO_S00
                            dtLedgerAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strNowDt, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, CommValue.ITEM_TYPE_VALUE_WATERCHARGE,
                                                                             CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, strUserSeq, string.Empty, dblDongToDollar,
                                                                             dblItemTotEnAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio,
                                                                             Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                            if (dtLedgerAccnt != null)
                            {
                                if (dtLedgerAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["PaymentSeq"].ToString());
                                    intItemSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["ItemSeq"].ToString());

                                    // 계좌이체시 관련 정보 등록
                                    if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                                    {
                                        // 2. 납입 계좌정보 등록
                                        // KN_USP_SET_INSERT_LEDGERINFO_S01
                                        BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strNowDt, intPaymentSeq, ddlTransfer.SelectedValue);
                                    }
                                }
                            }

                            for (int intTmpI = 0; intTmpI < lvUtilFeeList.Items.Count; intTmpI++)
                            {
                                if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkWatItem")).Enabled)
                                {
                                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkWatItem")).Checked)
                                    {
                                        TextBox txtWatFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtWatFee");
                                        TextBox txtHfWatFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtHfWatFee");
                                        TextBox txtHfEnergyMonth = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtHfEnergyMonth");

                                        if (!string.IsNullOrEmpty(txtWatFee.Text))
                                        {
                                            if (double.Parse(txtWatFee.Text) > CommValue.NUMBER_VALUE_0_0)
                                            {
                                                dblPayedFee = double.Parse(txtWatFee.Text);

                                                dblUniPrime = dblPayedFee * (100) / (100 + dblVatRatio);

                                                // 3. 수도세 수납
                                                // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_S00
                                                DataTable dtUtilReturn = RemoteMngBlo.ModifyUtilityFeeForTower(strRentCd, CommValue.CHARGETY_VALUE_WATER, txtHfEnergyMonth.Text, strRoomNo, strPaymentCd,
                                                                                                               dblPayedFee, Session["CompCd"].ToString(), Session["MemNo"].ToString());

                                                if (dtUtilReturn != null)
                                                {
                                                    if (dtUtilReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                                    {
                                                        int intPaySeq = Int32.Parse(dtUtilReturn.Rows[0]["PaySeq"].ToString());

                                                        // 4. 원장 상세 테이블 처리
                                                        // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                                        dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strNowDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                                          strDirectCd, CommValue.ITEM_TYPE_VALUE_WATERCHARGE, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                                          CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblPayedFee, dblPayedFee,
                                                                                                          CommValue.NUMBER_VALUE_0, txtHfEnergyMonth.Text.Substring(0, 4), txtHfEnergyMonth.Text.Substring(4, 2), "Water", dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                                          Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                                                        if (dtLedgerDet != null)
                                                        {
                                                            if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                                            {
                                                                intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                                            }
                                                        }

                                                        // 5. 납부금액과 청구 금액이 같으면 완납처리
                                                        // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M01
                                                        if (txtWatFee.Text.Equals(txtHfWatFee.Text))
                                                        {
                                                            RemoteMngBlo.ModifyMonthReceitForTower(strRentCd, txtHfEnergyMonth.Text.Substring(0, 6), strRoomNo, CommValue.CHARGETY_VALUE_WATER, CommValue.PAYMENT_TYPE_VALUE_PAID);
                                                        }

                                                        // 6. Utility Fee Sequence 할당
                                                        // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M02
                                                        RemoteMngBlo.ModifyMonthEnergySeqForTower(strRentCd, txtHfEnergyMonth.Text.Substring(0, 6), strRoomNo, CommValue.CHARGETY_VALUE_WATER, intPaySeq, strNowDt, intPaymentSeq);

                                                        // 7. 출력 테이블에 등록
                                                        // KN_USP_SET_INSERT_PRINTINFO_S00
                                                        dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_WATERCHARGE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                                                           Int32.Parse(strFloorNo), strRoomNo, txtHfEnergyMonth.Text.Substring(0, 4), txtHfEnergyMonth.Text.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                                           Session["MemNo"].ToString(), txtHfEnergyMonth.Text.Substring(0, 4) + " / " + txtHfEnergyMonth.Text.Substring(4, 2) + " Water Rate ( " + strRoomNo + " )",
                                                                                                           dblPayedFee, dblDongToDollar, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP,
                                                                                                           strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);

                                                        if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                                                        {
                                                            strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                                            strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                                            // 8. 출력 정보 원장상세 테이블에 등록
                                                            // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                                            BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));

                                                            // 9. 출력자 테이블에 등록
                                                            // KN_USP_SET_INSERT_PRINTINFO_S01
                                                            ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                                        }

                                                        // 10.금액 로그 테이블 처리
                                                        // KN_USP_SET_INSERT_MONEYINFO_M00
                                                        ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        /* 수도세 수납 처리 끝 */

                        #endregion

                        #region 가스비 수납 처리

                        /* 가스비 수납 처리 시작 */

                        if (!string.IsNullOrEmpty(txtHfRegGasFee.Text))
                        {
                            dblItemTotViAmt = double.Parse(txtHfRegGasFee.Text);
                        }
                        else
                        {
                            dblItemTotViAmt = CommValue.NUMBER_VALUE_0_0;
                        }

                        if (!string.IsNullOrEmpty(txtHfDongToDollar.Text))
                        {
                            dblDongToDollar = double.Parse(txtHfDongToDollar.Text);
                            dblItemTotEnAmt = dblItemTotViAmt / dblDongToDollar;
                        }
                        else
                        {
                            dblDongToDollar = CommValue.NUMBER_VALUE_0_0;
                            dblItemTotEnAmt = CommValue.NUMBER_VALUE_0_0;
                        }

                        if (dblItemTotViAmt > CommValue.NUMBER_VALUE_0_0)
                        {
                            // 0. 부가세 조회
                            // KN_USP_MNG_SELECT_VATINFO_S00
                            DataTable dtVatRatio = VatMngBlo.WatchVatInfo(CommValue.ITEM_TYPE_VALUE_GASCHARGE);

                            if (dtVatRatio != null)
                            {
                                if (dtVatRatio.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    strVatRatio = dtVatRatio.Rows[0]["VatRatio"].ToString();
                                    dblVatRatio = double.Parse(strVatRatio);
                                    dblUniPrime = dblItemTotViAmt * (100) / (100 + dblVatRatio);
                                }
                                else
                                {
                                    dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                    dblUniPrime = dblItemTotViAmt;
                                }
                            }
                            else
                            {
                                dblVatRatio = CommValue.NUMBER_VALUE_0_0;
                                dblUniPrime = dblItemTotViAmt;
                            }

                            // 1. 각종 세금 처리 원장등록
                            // KN_USP_SET_INSERT_LEDGERINFO_S00
                            dtLedgerAccnt = BalanceMngBlo.RegistryLedgerInfo(strDebitCreditCd, strNowDt, CommValue.NUMBER_VALUE_0, strRentCd, strDirectCd, CommValue.ITEM_TYPE_VALUE_GASCHARGE,
                                                                             CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, strUserSeq, string.Empty, dblDongToDollar,
                                                                             dblItemTotEnAmt, dblItemTotViAmt, strPaymentCd, dblVatRatio,
                                                                             Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                            if (dtLedgerAccnt != null)
                            {
                                if (dtLedgerAccnt.Rows.Count > CommValue.NUMBER_VALUE_0)
                                {
                                    intPaymentSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["PaymentSeq"].ToString());
                                    intItemSeq = Int32.Parse(dtLedgerAccnt.Rows[0]["ItemSeq"].ToString());

                                    // 계좌이체시 관련 정보 등록
                                    if (strPaymentCd.Equals(CommValue.PAYMENT_TYPE_VALUE_TRANSFER))
                                    {
                                        // 2. 납입 계좌정보 등록
                                        // KN_USP_SET_INSERT_LEDGERINFO_S01
                                        BalanceMngBlo.RegistryLedgerAddonInfo(strDebitCreditCd, strNowDt, intPaymentSeq, ddlTransfer.SelectedValue);
                                    }
                                }
                            }

                            for (int intTmpI = 0; intTmpI < lvUtilFeeList.Items.Count; intTmpI++)
                            {
                                if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkGasItem")).Enabled)
                                {
                                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkGasItem")).Checked)
                                    {
                                        TextBox txtGasFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtGasFee");
                                        TextBox txtHfGasFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtHfGasFee");
                                        TextBox txtHfEnergyMonth = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtHfEnergyMonth");

                                        if (!string.IsNullOrEmpty(txtGasFee.Text))
                                        {
                                            if (double.Parse(txtGasFee.Text) > CommValue.NUMBER_VALUE_0_0)
                                            {
                                                dblPayedFee = double.Parse(txtGasFee.Text);

                                                dblUniPrime = dblPayedFee * (100) / (100 + dblVatRatio);

                                                // 3. 가스비 수납
                                                // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_S00
                                                DataTable dtUtilReturn = RemoteMngBlo.ModifyUtilityFeeForTower(strRentCd, CommValue.CHARGETY_VALUE_GAS, txtHfEnergyMonth.Text, strRoomNo, strPaymentCd,
                                                                                                               dblPayedFee, Session["CompCd"].ToString(), Session["MemNo"].ToString());

                                                if (dtUtilReturn != null)
                                                {
                                                    if (dtUtilReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                                                    {
                                                        int intPaySeq = Int32.Parse(dtUtilReturn.Rows[0]["PaySeq"].ToString());

                                                        // 4. 원장 상세 테이블 처리
                                                        // KN_USP_SET_INSERT_LEDGERDETINFO_S00
                                                        dtLedgerDet = BalanceMngBlo.RegistryLedgerDetInfo(strDebitCreditCd, strNowDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, strRentCd,
                                                                                                          strDirectCd, CommValue.ITEM_TYPE_VALUE_GASCHARGE, intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty,
                                                                                                          CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH, dblUniPrime, dblUniPrime, dblPayedFee, dblPayedFee,
                                                                                                          CommValue.NUMBER_VALUE_0, txtHfEnergyMonth.Text.Substring(0, 4), txtHfEnergyMonth.Text.Substring(4, 2), "Gas", dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES,
                                                                                                          Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                                                        if (dtLedgerDet != null)
                                                        {
                                                            if (dtLedgerDet.Rows.Count > CommValue.NUMBER_VALUE_0)
                                                            {
                                                                intPaymentDetSeq = Int32.Parse(dtLedgerDet.Rows[0]["PaymentDetSeq"].ToString());
                                                            }
                                                        }

                                                        // 5. 납부금액과 청구 금액이 같으면 완납처리
                                                        // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M01
                                                        if (txtGasFee.Text.Equals(txtHfGasFee.Text))
                                                        {
                                                            RemoteMngBlo.ModifyMonthReceitForTower(strRentCd, txtHfEnergyMonth.Text.Substring(0, 6), strRoomNo, CommValue.CHARGETY_VALUE_GAS, CommValue.PAYMENT_TYPE_VALUE_PAID);
                                                        }

                                                        // 6. Utility Fee Sequence 할당
                                                        // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M02
                                                        RemoteMngBlo.ModifyMonthEnergySeqForTower(strRentCd, txtHfEnergyMonth.Text.Substring(0, 6), strRoomNo, CommValue.CHARGETY_VALUE_GAS, intPaySeq, strNowDt, intPaymentSeq);

                                                        // 7. 출력 테이블에 등록
                                                        // KN_USP_SET_INSERT_PRINTINFO_S00
                                                        dtPrintOut = ReceiptMngBlo.RegistryPrintReciptList(strPrintSeq, CommValue.ITEM_TYPE_VALUE_GASCHARGE, CommValue.DOCUMENT_VALUE_RECEIT, strRentCd,
                                                                                                           Int32.Parse(strFloorNo), strRoomNo, txtHfEnergyMonth.Text.Substring(0, 4), txtHfEnergyMonth.Text.Substring(4, 2), strPaymentCd, strUserSeq,
                                                                                                           Session["MemNo"].ToString(), txtHfEnergyMonth.Text.Substring(0, 4) + " / " + txtHfEnergyMonth.Text.Substring(4, 2) + " Gas Rate ( " + strRoomNo + " )",
                                                                                                           dblPayedFee, dblDongToDollar, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP,
                                                                                                           strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);

                                                        if (!string.IsNullOrEmpty(dtPrintOut.Rows[0]["PrintSeq"].ToString()))
                                                        {
                                                            strPrintSeq = dtPrintOut.Rows[0]["PrintSeq"].ToString();
                                                            strPrintDetSeq = dtPrintOut.Rows[0]["PrintDetSeq"].ToString();

                                                            // 8. 출력 정보 원장상세 테이블에 등록
                                                            // KN_USP_SET_UPDATE_LEDGERDETINFO_M00
                                                            BalanceMngBlo.ModifyLedgerDetInfoForPrint(strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, Int32.Parse(strPrintDetSeq));

                                                            // 9. 출력자 테이블에 등록
                                                            // KN_USP_SET_INSERT_PRINTINFO_S01
                                                            ReceiptMngBlo.RegistryPrintAddonList(strPrintSeq, Int32.Parse(strPrintDetSeq), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                                        }

                                                        // 10.금액 로그 테이블 처리
                                                        // KN_USP_SET_INSERT_MONEYINFO_M00
                                                        ReceiptMngBlo.RegistryMoneyInfo(strDebitCreditCd, strNowDt, intPaymentSeq, intPaymentDetSeq);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        /* 가스비 수납 처리 끝 */

                        #endregion

                        ResetSearchControls();

                        ddlInsRentCd.SelectedValue = txtHfRegRentCd.Text;
                        txtInsRoomNo.Text = txtHfRegRoomNo.Text;

                        LoadData();

                        if (!string.IsNullOrEmpty(txtHfRentCd.Text))
                        {
                            if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APT) ||
                                txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APTA) ||
                                txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APTB))
                            {
                                StringBuilder sbList = new StringBuilder();
                                sbList.Append("window.open('/Common/RdPopup/RDPopupReciptDetail.aspx?Datum0=" + strPrintSeq + "&Datum1=0&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UtilityFee", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                            }
                            else
                            {
                                StringBuilder sbList = new StringBuilder();
                                sbList.Append("window.open('/Common/RdPopup/RDPopupReciptKNDetail.aspx?Datum0=" + strPrintSeq + "&Datum1=0&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UtilityFee", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                            }
                        }
                        else
                        {
                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("window.open('/Common/RdPopup/RDPopupReciptDetail.aspx?Datum0=" + strPrintSeq + "&Datum1=0&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "UtilityFee", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        public void MakeAccountDdl(DropDownList ddlParams)
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
            // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
            // Utility Fee : Chestnut 매출
            // 그외 KeangNam 매출
            string strCompCd = string.Empty;

            strCompCd = CommValue.SUB_COMP_CD;

            DataTable dtReturn = AccountMngBlo.SpreadAccountInfo(strCompCd);

            ddlParams.Items.Clear();

            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
            }
        }

        protected void ResetSearchControls()
        {
            if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APT))
            {
                ddlInsRentCd.SelectedValue = CommValue.RENTAL_VALUE_APTA;
            }
            else if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APTSHOP))
            {
                ddlInsRentCd.SelectedValue = CommValue.RENTAL_VALUE_APTASHOP;
            }
            else
            {
                ddlInsRentCd.SelectedValue = txtHfRentCd.Text;
            }

            txtInsRoomNo.Text = string.Empty;
            ddlPaidCd.SelectedValue = CommValue.CODE_VALUE_EMPTY;
        }

        protected void ResetInputControls()
        {
            ltRegRoomNo.Text = string.Empty;
            txtHfRegRentCd.Text = string.Empty;
            txtHfRegRoomNo.Text = string.Empty;
            txtHfUserSeq.Text = string.Empty;
            txtHfDongToDollar.Text = string.Empty;
            txtHfFloorNo.Text = string.Empty;
            ltRegElecFee.Text = string.Empty;
            txtHfRegElecFee.Text = string.Empty;
            ltRegWatFee.Text = string.Empty;
            txtHfRegWatFee.Text = string.Empty;
            ltRegGasFee.Text = string.Empty;
            txtHfRegGasFee.Text = string.Empty;
            ltRegTotalFee.Text = string.Empty;
            txtHfRegTotalFee.Text = string.Empty;

            ddlPaymentCd.SelectedValue = CommValue.CODE_VALUE_EMPTY;
            ddlTransfer.Enabled = CommValue.AUTH_VALUE_TRUE;
            ddlTransfer.SelectedValue = string.Empty;
            ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;
            hfSelectedLine.Value = string.Empty;
        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // 가장 최근의 환율을 조회함.
            dtReturn = ExchangeMngBlo.WatchExchangeRateLastInfo(CommValue.RENTAL_VALUE_PARKING);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0") + "&nbsp;" + TextNm["DONG"].ToString());
                        hfRealBaseRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                    }
                }
                else
                {
                    ltRealBaseRate.Text = "-";
                }
            }
        }

        ///// <summary>
        ///// 상세보기 클릭시 이벤트 처리
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        //{
        //    try
        //    {
        //        Session["EnergyOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrLogger.MakeLogger(ex);
        //    }
        //}

        protected void lvRentItemList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            TextBox txtHfRentCd = (TextBox)lvUtilFeeList.Items[e.ItemIndex].FindControl("txtHfRentCd");
            TextBox txtHfRoomNo = (TextBox)lvUtilFeeList.Items[e.ItemIndex].FindControl("txtHfRoomNo");
            TextBox txtHfEnergyMonth = (TextBox)lvUtilFeeList.Items[e.ItemIndex].FindControl("txtHfEnergyMonth");

            // 완납처리
            // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M01
            RemoteMngBlo.ModifyMonthReceitForTower(txtHfRentCd.Text, txtHfEnergyMonth.Text, txtHfRoomNo.Text, CommValue.CHARGETY_VALUE_ELECTRICITY, CommValue.PAYMENT_TYPE_VALUE_PAID);

            LoadData();
        }

        protected void lvRentItemList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            TextBox txtHfRentCd = (TextBox)lvUtilFeeList.Items[e.ItemIndex].FindControl("txtHfRentCd");
            TextBox txtHfRoomNo = (TextBox)lvUtilFeeList.Items[e.ItemIndex].FindControl("txtHfRoomNo");
            TextBox txtHfEnergyMonth = (TextBox)lvUtilFeeList.Items[e.ItemIndex].FindControl("txtHfEnergyMonth");

            // 완납처리
            // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M01
            RemoteMngBlo.ModifyMonthReceitForTower(txtHfRentCd.Text, txtHfEnergyMonth.Text, txtHfRoomNo.Text, CommValue.CHARGETY_VALUE_WATER, CommValue.PAYMENT_TYPE_VALUE_PAID);

            LoadData();
        }

        protected void lvRentItemList_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            TextBox txtHfRentCd = (TextBox)lvUtilFeeList.Items[e.NewEditIndex].FindControl("txtHfRentCd");
            TextBox txtHfRoomNo = (TextBox)lvUtilFeeList.Items[e.NewEditIndex].FindControl("txtHfRoomNo");
            TextBox txtHfEnergyMonth = (TextBox)lvUtilFeeList.Items[e.NewEditIndex].FindControl("txtHfEnergyMonth");

            // 완납처리
            // KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M01
            RemoteMngBlo.ModifyMonthReceitForTower(txtHfRentCd.Text, txtHfEnergyMonth.Text, txtHfRoomNo.Text, CommValue.CHARGETY_VALUE_GAS, CommValue.PAYMENT_TYPE_VALUE_PAID);

            LoadData();
        }

        protected void imgbtnChange_Click(object sender, ImageClickEventArgs e)
        {
            double dblEleReturn = 0d;
            double dblWatReturn = 0d;
            double dblGasReturn = 0d;
            double dblTotalPaid = 0d;

            for (int intTmpI = 0; intTmpI < lvUtilFeeList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkElecItem")).Enabled)
                {
                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkElecItem")).Checked)
                    {
                        TextBox txtElecFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtElecFee");

                        if (!string.IsNullOrEmpty(txtElecFee.Text))
                        {
                            dblEleReturn = dblEleReturn + double.Parse(txtElecFee.Text);
                        }
                        else
                        {
                            txtElecFee.Text = CommValue.NUMBER_VALUE_ZERO;
                        }
                    }
                }

                if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkWatItem")).Enabled)
                {
                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkWatItem")).Checked)
                    {
                        TextBox txtWatFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtWatFee");

                        if (!string.IsNullOrEmpty(txtWatFee.Text))
                        {
                            dblWatReturn = dblWatReturn + double.Parse(txtWatFee.Text);
                        }
                        else
                        {
                            txtWatFee.Text = CommValue.NUMBER_VALUE_ZERO;
                        }
                    }
                }

                if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkGasItem")).Enabled)
                {
                    if (((CheckBox)lvUtilFeeList.Items[intTmpI].FindControl("chkGasItem")).Checked)
                    {
                        TextBox txtGasFee = (TextBox)lvUtilFeeList.Items[intTmpI].FindControl("txtGasFee");

                        if (!string.IsNullOrEmpty(txtGasFee.Text))
                        {
                            dblGasReturn = dblGasReturn + double.Parse(txtGasFee.Text);
                        }
                        else
                        {
                            txtGasFee.Text = CommValue.NUMBER_VALUE_ZERO;
                        }
                    }
                }
            }

            dblTotalPaid = dblTotalPaid + dblEleReturn + dblWatReturn + dblGasReturn;

            ltRegElecFee.Text = dblEleReturn.ToString("###,##0");
            txtHfRegElecFee.Text = dblEleReturn.ToString();
            ltRegWatFee.Text = dblWatReturn.ToString("###,##0");
            txtHfRegWatFee.Text = dblWatReturn.ToString();
            ltRegGasFee.Text = dblGasReturn.ToString("###,##0");
            txtHfRegGasFee.Text = dblGasReturn.ToString();
            ltRegTotalFee.Text = dblTotalPaid.ToString("###,##0");
            txtHfRegTotalFee.Text = dblTotalPaid.ToString();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strStartYear = CommValue.DATE_MOVE_INTO.Substring(0, 4);
            string strStartMonth = CommValue.DATE_MOVE_INTO.Substring(4, 2);

            int intNowYear = DateTime.Now.Year;
            int intNowMonth = DateTime.Now.Month;
            int intStartNo = CommValue.NUMBER_VALUE_0;
            int intEndNo = CommValue.NUMBER_VALUE_0;

            // 시작년 04 ~ 12
            if (ddlYear.SelectedValue.Equals(strStartYear))
            {
                intStartNo = Int32.Parse(strStartMonth);
                intEndNo = 12;
            }
            // 시작년 이후
            else if (Int32.Parse(ddlYear.SelectedValue) < intNowYear)
            {
                intStartNo = CommValue.NUMBER_VALUE_1;
                intEndNo = 12;
            }
            // 시작년 아닌 현재년
            else if (Int32.Parse(ddlYear.SelectedValue) == intNowYear)
            {
                intStartNo = CommValue.NUMBER_VALUE_1;
                intEndNo = intNowMonth;
            }

            ddlMonth.Items.Clear();

            for (int intTmpI = intStartNo; intTmpI <= intEndNo; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }
        }

        /// <summary>
        /// 엑셀리포트버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DataTable dtReturn = new DataTable();

                string strMakeMonth = string.Empty;

                // KN_USP_MNG_SELECT_MONTHENERGY_S10
                dtReturn = RemoteMngBlo.SpreadMonthExcelEnergyListForTower(ddlYear.SelectedValue, ddlMonth.SelectedValue);

                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    strMakeMonth = ddlYear.SelectedValue + "_" + ddlMonth.SelectedValue + "_";

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + strMakeMonth + "_" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Charset = "euc-kr";
                    Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                    this.EnableViewState = false;

                    StringWriter stringWriter = new StringWriter();
                    HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

                    string strTitle = "<p align=center><font size=4 face=Gulim><b>" + Master.TITLE_NOW.ToString() + "</b></font></p>";
                    htmlTextWriter.Write(strTitle);

                    GridView gv = new GridView();

                    gv.DataSource = dtReturn;
                    gv.DataBind();
                    gv.RenderControl(htmlTextWriter);

                    Response.Write(stringWriter.ToString());
                    Response.End();

                    stringWriter.Flush();
                    stringWriter.Close();
                    htmlTextWriter.Flush();
                    htmlTextWriter.Close();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:alert('" + AlertNm["INFO_HAS_NO_DATA"] + "');", CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}