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

using KN.Resident.Biz;

namespace KN.Web.Resident.Residence
{
    public partial class OccupantList : BasePage
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
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 매개변수 체크
        /// </summary>
        /// <returns></returns>
        protected bool CheckParams()
        {
            bool isReturn = CommValue.AUTH_VALUE_TRUE;

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

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                txthfRentCd.Text = Request.Params[Master.PARAM_DATA1];
                hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
            }
            else
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturn;
        }

        /// <summary>
        /// 각 컨트롤 초기화
        /// </summary>
        protected void InitControls()
        {
            ltNm.Text = TextNm["NAME"];
            ltFloor.Text = TextNm["FLOOR"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltSearchCond.Text = TextNm["SEARCH"] + TextNm["COND"];
            divSearchCond.Visible = CommValue.AUTH_VALUE_FALSE;

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnMakeBill.Text = TextNm["MAKEBILL"];
            lnkbtnReport.Text = "Report" + TextNm["PRINT"];
            lnkbtnExcelReport.Text = TextNm["EXCEL"] + TextNm["PRINT"];
            lnkbtnMakeBill.Visible = Master.isWriteAuthOk;
            lnkbtnReport.Visible = Master.isWriteAuthOk;
            lnkbtnExcelReport.Visible = Master.isWriteAuthOk;
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            string strStartDt = hfStartDt.Value.Replace("-", "").Replace(".", "");
            string strEndDt = hfEndDt.Value.Replace("-", "").Replace(".", "");
            string strSearchFloor = txtSearchFloor.Text;
            string strSearchRoomNo = txtSearchRoomNo.Text;
            string strDuring = "~";
            string strTotalSearchTxt = string.Empty;

            if (!string.IsNullOrEmpty(strStartDt) || !string.IsNullOrEmpty(strEndDt) || !string.IsNullOrEmpty(txtNm.Text) ||
                !string.IsNullOrEmpty(strSearchFloor) || !string.IsNullOrEmpty(strSearchRoomNo))
            {
                divSearchCond.Visible = CommValue.AUTH_VALUE_TRUE;

                if (!string.IsNullOrEmpty(strStartDt))
                {
                    strTotalSearchTxt = TextLib.MakeDateEightDigit(strStartDt) + strDuring;
                    strDuring = "";
                }

                if (!string.IsNullOrEmpty(strEndDt))
                {
                    strTotalSearchTxt = strTotalSearchTxt + " " + strDuring + TextLib.MakeDateEightDigit(strEndDt);
                }

                if (!string.IsNullOrEmpty(txtNm.Text))
                {
                    strTotalSearchTxt = strTotalSearchTxt + " " + txtNm.Text;
                }

                if (!string.IsNullOrEmpty(strSearchFloor))
                {
                    strTotalSearchTxt = strTotalSearchTxt + " " + strSearchFloor;
                }
                else
                {
                    strSearchFloor = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(strSearchRoomNo))
                {
                    strTotalSearchTxt = strTotalSearchTxt + " " + strSearchRoomNo;
                }

                ltSearchTxt.Text = strTotalSearchTxt;
            }
            else
            {
                divSearchCond.Visible = CommValue.AUTH_VALUE_FALSE;
                strSearchFloor = CommValue.NUMBER_VALUE_ZERO;
            }

            DataSet dsReturn = new DataSet();

            // KN_USP_RES_SELECT_USERINFO_S00
            dsReturn = ResidentMngBlo.SpreadSalesUserInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txthfRentCd.Text, txtNm.Text,
                                                          strSearchRoomNo, strStartDt, strEndDt);

            if (dsReturn != null)
            {
                lvUserList.DataSource = dsReturn.Tables[1];
                lvUserList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        protected void ResetContorls()
        {
            txtNm.Text = string.Empty;
            txtSearchFloor.Text = string.Empty;
            txtSearchRoomNo.Text = string.Empty;
            txtStartDt.Text = string.Empty;
            txtEndDt.Text = string.Empty;
            hfStartDt.Value = string.Empty;
            hfEndDt.Value = string.Empty;
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvUserList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvUserList.FindControl("ltTopSeq")).Text = TextNm["SEQ"];
            ((Literal)lvUserList.FindControl("ltTopNm")).Text = TextNm["NAME"];
            ((Literal)lvUserList.FindControl("ltTopFloor")).Text = TextNm["FLOOR"];
            ((Literal)lvUserList.FindControl("ltTopRoomNo")).Text = TextNm["ROOMNO"];
            ((Literal)lvUserList.FindControl("ltTopPhone")).Text = TextNm["TEL"];
            ((Literal)lvUserList.FindControl("ltTopOccuDt")).Text = TextNm["OCCUDT"];
            ((Literal)lvUserList.FindControl("ltTopNoPerson")).Text = TextNm["NOOFPERSON"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvUserList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltTopSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltTopNm")).Text = TextNm["NAME"];
                    ((Literal)e.Item.FindControl("ltTopFloor")).Text = TextNm["FLOOR"];
                    ((Literal)e.Item.FindControl("ltTopRoomNo")).Text = TextNm["ROOMNO"];
                    ((Literal)e.Item.FindControl("ltTopPhone")).Text = TextNm["TEL"];
                    ((Literal)e.Item.FindControl("ltTopOccuDt")).Text = TextNm["OCCUDT"];
                    ((Literal)e.Item.FindControl("ltTopNoPerson")).Text = TextNm["NOOFPERSON"];

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
        protected void lvUserList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["RealSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSeq")).Text = drView["RealSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsNm")).Text =  TextLib.StringDecoder(drView["UserNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsFloor")).Text = drView["FloorNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsRoomNo")).Text = drView["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TelTyCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["TelFrontNo"].ToString()) &&
                    !string.IsNullOrEmpty(drView["TelRearNo"].ToString()))
                {
                    StringBuilder sbTelNo = new StringBuilder();

                    sbTelNo.Append(drView["TelTyCd"].ToString());
                    sbTelNo.Append(") ");
                    sbTelNo.Append(drView["TelFrontNo"].ToString());
                    sbTelNo.Append("-");
                    sbTelNo.Append(drView["TelRearNo"].ToString());

                    ((Literal)iTem.FindControl("ltInsPhone")).Text = sbTelNo.ToString();
                }

                if (!string.IsNullOrEmpty(drView["OccupationDt"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsOccuDt")).Text = TextLib.MakeDateEightDigit(drView["OccupationDt"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["UserCnt"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsNoPerson")).Text = drView["UserCnt"].ToString();
                }
            }
        }

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

        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbLink = new StringBuilder();

                sbLink.Append(Master.PAGE_VIEW);
                sbLink.Append("?");
                sbLink.Append(Master.PARAM_DATA1);
                sbLink.Append("=");
                sbLink.Append(txthfRentCd.Text);
                sbLink.Append("&");
                sbLink.Append(Master.PARAM_DATA2);
                sbLink.Append("=");
                sbLink.Append(hfRentSeq.Value);

                var dsReturnRentSeq = new DataSet();
                dsReturnRentSeq = ResidentMngBlo.SelectRentSeqUserInfo(hfUserSeq.Value, txthfRentCd.Text, Int32.Parse(hfRentSeq.Value));

                if (dsReturnRentSeq.Tables[0].Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(hfUserSeq.Value))
                    {
                        sbLink.Append("&");
                        sbLink.Append(Master.PARAM_DATA3);
                        sbLink.Append("=");
                        sbLink.Append(hfUserSeq.Value);
                    }
                }

                Response.Redirect(sbLink.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                ResetContorls();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnMakeBill_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                object[] objReturn = new object[2];

                if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT))
                {
                    // KN_USP_AGT_MAKE_MNGFEE_APT_LIST_M00
                    objReturn = ContractMngBlo.RegistryMakeAPTMngFeeListInfo();
                }
                else if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTSHOP))
                {
                    // KN_USP_AGT_MAKE_MNGFEE_APTR_LIST_M00
                    objReturn = ContractMngBlo.RegistryMakeAPTRMngFeeListInfo();
                }

                if (objReturn != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('" + AlertNm["INFO_MAKE_BILLING"] + "')", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('" + AlertNm["INFO_NOT_MAKE_BILLING"] + "')", CommValue.AUTH_VALUE_TRUE);
                }
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

                string strStartDt = hfStartDt.Value.Replace("-", "").Replace(".", "");
                string strEndDt = hfEndDt.Value.Replace("-", "").Replace(".", "");

                string strSearchRoomNo = txtSearchRoomNo.Text;
                string strDuring = "~";
                string strTotalSearchTxt = string.Empty;

                if (!string.IsNullOrEmpty(strStartDt) || !string.IsNullOrEmpty(strEndDt) || !string.IsNullOrEmpty(txtNm.Text) || !string.IsNullOrEmpty(strSearchRoomNo))
                {
                    divSearchCond.Visible = CommValue.AUTH_VALUE_TRUE;

                    if (!string.IsNullOrEmpty(strStartDt))
                    {
                        strTotalSearchTxt = TextLib.MakeDateEightDigit(strStartDt) + strDuring;
                        strDuring = "";
                    }

                    if (!string.IsNullOrEmpty(strEndDt))
                    {
                        strTotalSearchTxt = strTotalSearchTxt + " " + strDuring + TextLib.MakeDateEightDigit(strEndDt);
                    }

                    if (!string.IsNullOrEmpty(txtNm.Text))
                    {
                        strTotalSearchTxt = strTotalSearchTxt + " " + txtNm.Text;
                    }

                    if (!string.IsNullOrEmpty(strSearchRoomNo))
                    {
                        strTotalSearchTxt = strTotalSearchTxt + " " + strSearchRoomNo;
                    }

                    ltSearchTxt.Text = strTotalSearchTxt;
                }
                else
                {
                    divSearchCond.Visible = CommValue.AUTH_VALUE_FALSE;
                }

                DataTable dtReturn = new DataTable();

                // KN_USP_RES_SELECT_USERINFO_S05
                dtReturn = ResidentMngBlo.SpreadSalesExcelUserInfo(txthfRentCd.Text, txtNm.Text, strSearchRoomNo, strStartDt, strEndDt, Session["LangCd"].ToString());

                string strRentNm = ExpressCdTxtUtil.MakeRentTxt(Session["LangCd"].ToString(), hfRentCd.Value);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW.ToString()).Replace("+", " ") + ".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                this.EnableViewState = false;

                StringWriter stringWriter = new StringWriter();
                HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);


                string strTitle = "<p align=center><font size=4 face=Gulim><b>" + strRentNm + " " + Master.TITLE_NOW.ToString() + "</b></font></p>";
                htmlTextWriter.Write(strTitle);

                GridView gv = new GridView();
                gv.Font.Name = "Tahoma";
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
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}