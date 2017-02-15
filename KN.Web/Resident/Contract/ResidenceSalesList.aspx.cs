using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Resident.Biz;

namespace KN.Web.Resident.Contract
{
    public partial class ResidenceSalesList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = CommValue.NUMBER_VALUE_0;
        string strRentCd = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    // 컨트롤 초기화
                    InitControls();

                    // 데이터 로드
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            // DropDownList Setting
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlKeyCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_LEASE);
            CommCdDdlUtil.MakeSubCdDdlTitle(ddlConclusionYn, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_CONCLUSION);

            ddlKeyCd.SelectedValue = CommValue.LEASE_SEARCH_VALUE_ROOMNO;

            // Button Setting
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnWrite.Text = TextNm["WRITE"];
            lnkbtnWrite.Visible = Master.isWriteAuthOk;

            lnkbtnReport.Text = "Report" + TextNm["PRINT"];

            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];
            lnkbtnExcelReport.Visible = Master.isWriteAuthOk;
        }

        /// <summary>
        /// 매개변수 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {
            if (!string.IsNullOrEmpty(hfCurrentPage.Value))
            {
                intPageNo = Int32.Parse(hfCurrentPage.Value);
            }
            else
            {
                intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfCurrentPage.Value = intPageNo.ToString();
            }

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();
                    strRentCd = Request.Params[Master.PARAM_DATA1].ToString();
                }
                else
                {
                    hfRentCd.Value = CommValue.RENTAL_VALUE_APT;
                    strRentCd = CommValue.RENTAL_VALUE_APT;
                }
            }
            else
            {
                hfRentCd.Value = CommValue.RENTAL_VALUE_APT;
                strRentCd = CommValue.RENTAL_VALUE_APT;
            }

            StringBuilder sbLink = new StringBuilder();
            sbLink.Append(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=");
            sbLink.Append(strRentCd);

            lnkbtnWrite.PostBackUrl = sbLink.ToString();
        }

        /// <summary>
        /// 데이터를 로드하는 메소드
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 매개변수 체크
            CheckParams();

            DataSet dsReturn = new DataSet();

            // KN_USP_RES_SELECT_SALESINFO_S00
            dsReturn = ContractMngBlo.SpreadSalesInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), hfRentCd.Value, ddlKeyCd.Text, txtKeyWord.Text, ddlConclusionYn.Text, Session["LangCd"].ToString(),ddlTenantTy.SelectedValue);

            if (dsReturn != null)
            {
                lvRentList.DataSource = dsReturn.Tables[1];
                lvRentList.DataBind();

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
        protected void lvRentList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvRentList.FindControl("ltNo")).Text = TextNm["NO"];
            ((Literal)lvRentList.FindControl("ltFloor")).Text = TextNm["FLOOR"];
            ((Literal)lvRentList.FindControl("ltRoomNo")).Text = TextNm["ROOMNO"];
            ((Literal)lvRentList.FindControl("ltTenant")).Text = TextNm["TENANT"];
            ((Literal)lvRentList.FindControl("ltContPeriod")).Text = TextNm["CONTPERIOD"];
            ((Literal)lvRentList.FindControl("ltConcYn")).Text = TextNm["CONCYN"];
            ((Literal)lvRentList.FindControl("ltInsDt")).Text = TextNm["INSDT"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRentList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltNo")).Text = TextNm["NO"];
                    ((Literal)e.Item.FindControl("ltFloor")).Text = TextNm["FLOOR"];
                    ((Literal)e.Item.FindControl("ltRoomNo")).Text = TextNm["ROOMNO"];
                    ((Literal)e.Item.FindControl("ltTenant")).Text = TextNm["TENANT"];
                    ((Literal)e.Item.FindControl("ltContPeriod")).Text = TextNm["CONTPERIOD"];
                    ((Literal)e.Item.FindControl("ltConcYn")).Text = TextNm["CONCYN"];
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
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRentList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["DisplaySeq"].ToString()))
                {
                    Literal ltInsNo = (Literal)e.Item.FindControl("ltInsNo");
                    ltInsNo.Text = drView["DisplaySeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    Literal ltInsFloor = (Literal)e.Item.FindControl("ltInsFloor");
                    ltInsFloor.Text = drView["FloorNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltInsRoomNo = (Literal)e.Item.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = drView["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ContractNm"].ToString()))
                {
                    Literal ltInsTenant = (Literal)e.Item.FindControl("ltInsTenant");
                    ltInsTenant.Text = drView["ContractNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ContDt"].ToString()) && !string.IsNullOrEmpty(drView["ResaleDt"].ToString()))
                {
                    StringBuilder sbDuration = new StringBuilder();

                    if (!string.IsNullOrEmpty(drView["ContDt"].ToString()))
                    {
                        sbDuration.Append(drView["ContDt"].ToString().Substring(0, 4));
                        sbDuration.Append(".");
                        sbDuration.Append(drView["ContDt"].ToString().Substring(4, 2));
                        sbDuration.Append(".");
                        sbDuration.Append(drView["ContDt"].ToString().Substring(6, 2));
                    }

                    sbDuration.Append(" ~ ");

                    if (!string.IsNullOrEmpty(drView["ResaleDt"].ToString()))
                    {
                        sbDuration.Append(drView["ResaleDt"].ToString().Substring(0, 4));
                        sbDuration.Append(".");
                        sbDuration.Append(drView["ResaleDt"].ToString().Substring(4, 2));
                        sbDuration.Append(".");
                        sbDuration.Append(drView["ResaleDt"].ToString().Substring(6, 2));
                    }

                    Literal ltInsContPeriod = (Literal)e.Item.FindControl("ltInsContPeriod");
                    ltInsContPeriod.Text = sbDuration.ToString();
                }

                if (!string.IsNullOrEmpty(drView["SaveYn"].ToString()))
                {
                    Literal ltInsConcYn = (Literal)e.Item.FindControl("ltInsConcYn");
                    ltInsConcYn.Text = drView["SaveYn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDt"].ToString()))
                {
                    StringBuilder sbAgreeDt = new StringBuilder();

                    sbAgreeDt.Append(drView["InsDt"].ToString().Substring(0, 4));
                    sbAgreeDt.Append(".");
                    sbAgreeDt.Append(drView["InsDt"].ToString().Substring(4, 2));
                    sbAgreeDt.Append(".");
                    sbAgreeDt.Append(drView["InsDt"].ToString().Substring(6, 2));

                    Literal ltInsInsDt = (Literal)e.Item.FindControl("ltInsInsDt");
                    ltInsInsDt.Text = sbAgreeDt.ToString();
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

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                // 데이터 로드
                LoadData();
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

                // 데이터 로드
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 페이지 상세보기 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Session["ViewContract"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + hfRentCd.Value + "&" + Master.PARAM_DATA2 + "=" + hfRentSeq.Value, CommValue.AUTH_VALUE_FALSE);
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

                DataTable dtReturn = new DataTable();

                // KN_USP_RES_SELECT_SALESINFO_S02
                dtReturn = ContractMngBlo.SpreadSalesInfoExcelView(hfRentCd.Value, ddlKeyCd.Text, txtKeyWord.Text, Session["LangCd"].ToString());

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
                Response.ContentType = "application/vnd.ms-excel";
                //Response.Charset = "UTF-8";
                Response.ContentEncoding = Encoding.Unicode;
                Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                this.EnableViewState = false;

                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

                string strTitle = "<p align='center'><font size='4'><b>" + Master.TITLE_NOW.ToString() + "</b></font></p>";

                htmlTextWriter.Write(strTitle);

                GridView gv = new GridView();

                gv.DataSource = dtReturn;
                gv.Font.Name = "Tahoma";
                gv.DataBind();
                gv.RenderControl(htmlTextWriter);

                Response.Write(stringWriter.ToString());
                Response.Flush();

                // Prevents any other content from being sent to the browser
                Response.SuppressContent = true;

                // Directs the thread to finish, bypassing additional processing
                HttpContext.Current.ApplicationInstance.CompleteRequest();

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