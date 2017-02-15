using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;

namespace KN.Web.Management.LateFee
{
    public partial class LateFeeList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
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

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
            {
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();

                txtHfFeeTy.Text = Request.Params[Master.PARAM_DATA2].ToString();
                hfFeeTy.Value = Request.Params[Master.PARAM_DATA2].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            string strFeeTyTxt = string.Empty;

            ltFloor.Text = TextNm["FLOOR"];
            ltRoom.Text = TextNm["ROOMNO"];
            ltName.Text = TextNm["NAME"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnReport.Text = "Report" + TextNm["PRINT"];
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];
            lnkbtnExcelReport.Visible = Master.isWriteAuthOk;

            MakeYearDdl();
            MakeMonthDdl();

            if (txtHfFeeTy.Text.Equals(CommValue.FEETY_VALUE_MNGFEE))
            {
                strFeeTyTxt = TextNm["MANAGEFEE"];
            }
            else if (txtHfFeeTy.Text.Equals(CommValue.FEETY_VALUE_RENTALFEE))
            {
                strFeeTyTxt = TextNm["RENTALFEE"];
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

            //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
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

            //ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            int intSearchFloor = 0;

            if (!string.IsNullOrEmpty(txtSearchFloor.Text))
            {
                intSearchFloor = Int32.Parse(txtSearchFloor.Text);
            }

            // KN_USP_MNG_SELECT_LATEFEEINFO_S00
            dsReturn = MngPaymentBlo.SpreadLateFeeList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfFeeTy.Text,
                txtNm.Text, intSearchFloor, txtSearchRoom.Text, ddlYear.Text, ddlMonth.Text);

            if (dsReturn != null)
            {
                lvLateFeeList.DataSource = dsReturn.Tables[1];
                lvLateFeeList.DataBind();

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
        protected void lvLateFeeList_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltFloorRoom = (Literal)lvLateFeeList.FindControl("ltFloorRoom");
            ltFloorRoom.Text = TextNm["FLOOR"] + " / " + TextNm["ROOMNO"];
            Literal ltName = (Literal)lvLateFeeList.FindControl("ltName");
            ltName.Text = TextNm["NAME"];
            Literal ltYearMM = (Literal)lvLateFeeList.FindControl("ltYearMM");
            ltYearMM.Text = TextNm["YEARS"] + " / " + TextNm["MONTH"];
            Literal ltPayment = (Literal)lvLateFeeList.FindControl("ltPayment");
            ltPayment.Text = TextNm["LATEPAYMENT"];
            Literal ltLateFee = (Literal)lvLateFeeList.FindControl("ltLateFee");
            ltLateFee.Text = TextNm["ARREARS"];
            Literal ltLateDt = (Literal)lvLateFeeList.FindControl("ltLateDt");
            ltLateDt.Text = TextNm["LATEFEEDAY"];
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
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLateFeeList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvLateFeeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    TextBox txtHfContractNoList = (TextBox)iTem.FindControl("txtHfContractNoList");
                    txtHfContractNoList.Text = drView["UserSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    Literal ltFloorRoomList = (Literal)iTem.FindControl("ltFloorRoomList");
                    ltFloorRoomList.Text = TextLib.StringDecoder(drView["FloorNo"].ToString()) + " / " + TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RentalYear"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["RentalMM"].ToString()))
                    {
                        Literal ltYearMMList = (Literal)iTem.FindControl("ltYearMMList");
                        ltYearMMList.Text = drView["RentalYear"].ToString() + " / " + drView["RentalMM"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
                {
                    Literal ltNameList = (Literal)iTem.FindControl("ltNameList");
                    ltNameList.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Payment"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["Payment"].ToString()))
                    {
                        Literal ltPaymentList = (Literal)iTem.FindControl("ltPaymentList");
                        ltPaymentList.Text = TextLib.MakeVietIntNo(double.Parse(drView["Payment"].ToString()).ToString("###,##0"));

                        if (ltPaymentList.Text.Equals(""))
                        {
                            ltPaymentList.Text = "-";
                        }
                    }
                }

                if (!string.IsNullOrEmpty(drView["LateFee"].ToString()))
                {
                    Literal ltLateFeeList = (Literal)iTem.FindControl("ltLateFeeList");
                    ltLateFeeList.Text = TextLib.MakeVietIntNo(double.Parse(drView["LateFee"].ToString()).ToString("###,##0"));

                    if (ltLateFeeList.Text.Equals(""))
                    {
                        ltLateFeeList.Text = "-";
                    }
                }

                if (!string.IsNullOrEmpty(drView["LateDt"].ToString()))
                {
                    Literal ltLateDtList = (Literal)iTem.FindControl("ltLateDtList");
                    ltLateDtList.Text = drView["LateDt"].ToString();
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
        /// 상세보기 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbView = new StringBuilder();
                sbView.Append(Master.PAGE_VIEW);
                sbView.Append("?");
                sbView.Append(Master.PARAM_DATA1);
                sbView.Append("=");
                sbView.Append(txtHfRentCd.Text);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA2);
                sbView.Append("=");
                sbView.Append(txtHfFeeTy.Text);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA3);
                sbView.Append("=");
                sbView.Append(hfRentalYear.Value);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA4);
                sbView.Append("=");
                sbView.Append(hfRentalMM.Value);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA5);
                sbView.Append("=");
                sbView.Append(hfUserSeq.Value);

                Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
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

                DataSet dsReturn = new DataSet();

                int intSearchFloor = 0;

                if (!string.IsNullOrEmpty(txtSearchFloor.Text))
                {
                    intSearchFloor = Int32.Parse(txtSearchFloor.Text);
                }

                // KN_USP_MNG_SELECT_LATEFEEINFO_S00
                dsReturn = MngPaymentBlo.SpreadLateFeeList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text, txtHfFeeTy.Text,
                                                           txtNm.Text, intSearchFloor, txtSearchRoom.Text, ddlYear.Text, ddlMonth.Text);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                this.EnableViewState = false;

                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

                string strTitle = "<p align=center><font size=4 face=Gulim><b>" + Master.TITLE_NOW.ToString() + "</b></font></p>";
                htmlTextWriter.Write(strTitle);

                GridView gv = new GridView();

                gv.DataSource = dsReturn.Tables[2];
                gv.DataBind();
                gv.RenderControl(htmlTextWriter);

                Response.Write(stringWriter.ToString());
                Response.End();

                stringWriter.Flush();
                stringWriter.Close();
                htmlTextWriter.Flush();
                htmlTextWriter.Close();

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}