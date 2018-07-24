using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;

namespace KN.Web.Inventory
{
    public partial class TimesReportList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        public string DATA_APT = CommValue.RENTAL_VALUE_APT;
        public string DATA_APTSTORE = CommValue.RENTAL_VALUE_APTSHOP;

        int intPageNo = CommValue.NUMBER_VALUE_0;

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

            if (Request.Params["RentCd"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["RentCd"].ToString()))
                {
                    hfRentCd.Value = Request.Params["RentCd"].ToString();

                    if (!string.IsNullOrEmpty(hfCurrentPage.Value))
                    {
                        intPageNo = Int32.Parse(hfCurrentPage.Value);
                        hfCurrentPage.Value = intPageNo.ToString();

                        isReturnOk = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                        hfCurrentPage.Value = intPageNo.ToString();

                        isReturnOk = CommValue.AUTH_VALUE_TRUE;
                    }
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltSearchName.Text = TextNm["NAME"];
            ltSearchRoom.Text = TextNm["ROOMNO"];

            // DropDownList Setting
            CommCdDdlUtil.MakeEtcSubCdDdlUserTitle(ddlRentNm, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL, TextNm["SELECT"]);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            ltSeq.Text = TextNm["SEQ"];
            ltRentNm.Text = TextNm["RENT"];
            ltName.Text = TextNm["NAME"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltMngFee.Text = TextNm["MNGFEE"];
            ltRentalFee.Text = TextNm["RENTALFEE"];
            ltUtilFee.Text = TextNm["UTILITYFEE"];

            lnkbtnPrint.Text = TextNm["PRINT"];
            lnkbtnPrint.OnClientClick = "javascript:return fnDetailViewJs('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');";

            // DropDownList Setting
            // 년도
            MakeYearDdl();

            // 월
            MakeMonthDdl();

            // 수납 아이템
            MakePaymentDdl();

            // 문서 종류
            MakeDocumentDdl();
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.AddYears(1).Year; intTmpI++)
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

        private void MakePaymentDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);
            ddlPayment.Items.Clear();

            ddlPayment.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE))
                {
                    ddlPayment.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }

            ddlPayment.Items.Add(new ListItem("0004", "Electric Over Time"));
            ddlPayment.Items.Add(new ListItem("0005", "Electric Air-con Over Time"));
        }

        private void MakeDocumentDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_DOCUMENT);
            ddlDocument.Items.Clear();

            ddlDocument.Items.Add(new ListItem(TextNm["DOCUMENTKIND"], ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["Codecd"].ToString().Equals(CommValue.DOCUMENT_VALUE_TAX))
                {
                    if (!ddlPayment.SelectedValue.Equals(CommValue.RECEIT_VALUE_PARKINGCARDFEE) &&
                        !ddlPayment.SelectedValue.Equals(CommValue.RECEIT_VALUE_PARKINGFEE))
                    {
                        ddlDocument.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                    else
                    {
                        if (!dr["CodeCd"].ToString().Equals(CommValue.DOCUMENT_VALUE_BILL))
                        {
                            ddlDocument.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                        }
                    }
                }
            }
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            //int intSearchFloor = 0;
            string strRentCd = string.Empty;

            if (string.IsNullOrEmpty(ddlRentNm.SelectedValue))
            {
                strRentCd = hfRentCd.Value;
            }
            else
            {
                strRentCd = ddlRentNm.SelectedValue;
            }

            hfRentCd.Value = strRentCd;

            // 오피스 및 리테일 청구 대상 리스트
            // KN_USP_MNG_SELECT_BILLINGINFO_S01
            dsReturn = ReceiptMngBlo.SpreadMngRentBillingList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtSearchNm.Text, strRentCd, txtSearchRoom.Text, Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                lvPaymentList.DataSource = dsReturn.Tables[1];
                lvPaymentList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPaymentList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvPaymentList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    Literal ltInsSeq = (Literal)iTem.FindControl("ltInsSeq");
                    ltInsSeq.Text = TextLib.StringDecoder(drView["Seq"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RentNm"].ToString()))
                {
                    Literal ltInsRentNm = (Literal)iTem.FindControl("ltInsRentNm");
                    ltInsRentNm.Text = TextLib.StringDecoder(drView["RentNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    Literal ltInsName = (Literal)iTem.FindControl("ltInsName");
                    ltInsName.Text = TextLib.TextCutString(TextLib.StringDecoder(drView["UserNm"].ToString()), 30, "..");
                }

                Literal ltInsMngFee = (Literal)iTem.FindControl("ltInsMngFee");

                if (!string.IsNullOrEmpty(drView["MngMonthViAmtNo"].ToString()))
                {
                    ltInsMngFee.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["MngMonthViAmtNo"].ToString())).ToString("###,##0"));
                }
                else
                {
                    ltInsMngFee.Text = "-";
                }

                Literal ltInsRentalFee = (Literal)iTem.FindControl("ltInsRentalFee");

                if (!string.IsNullOrEmpty(drView["RentMonthViAmtNo"].ToString()))
                {

                    ltInsRentalFee.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["RentMonthViAmtNo"].ToString())).ToString("###,##0"));
                }
                else
                {
                    ltInsRentalFee.Text = "-";
                }

                Literal ltInsUtilFee = (Literal)iTem.FindControl("ltInsUtilFee");

                if (!string.IsNullOrEmpty(drView["RequestUtilAmt"].ToString()))
                {
                    ltInsUtilFee.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["RequestUtilAmt"].ToString())).ToString("###,##0"));
                }
                else
                {
                    ltInsUtilFee.Text = "-";
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
        /// 상세보기 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Session["ReportingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            MakeDocumentDdl();
        }
    }
}