using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Resident.Biz;

namespace KN.Web.Resident.Counsel
{
    public partial class CounselList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;
        string strCounselCd = string.Empty;

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
                        // 컨트롤 초기화
                        InitControls();

                        // 데이터 로드;
                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + strCounselCd, CommValue.AUTH_VALUE_FALSE);
                    }
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
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlKeyCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_CONSULTING);

            // Button Setting
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnInput.Text = TextNm["INPUTCONSULT"];
            lnkbtnInput.Visible = Master.isWriteAuthOk;

            ltInsDt.Text = TextNm["REGIST PERIOD"];
            //CommCdDdlUtil.MakeSubCdDdlTitle(ddlArea, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_RENTAL, CommValue.RENTAL_VALUE_LEASE_AREA);
            ltContperiod.Text = TextNm["CONTPERIOD"];
            CommCdDdlUtil.MakeSubCdDdlTitle(ddlIndustry, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_BUSINESS);

            lnkbtnDetailSearch.Text = TextNm["SEARCH"];
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private bool CheckParams()
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

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    strCounselCd = Request.Params[Master.PARAM_DATA1].ToString();
                    txtHfCounselCd.Text = strCounselCd;

                    StringBuilder sbLink = new StringBuilder();
                    sbLink.Append(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=");
                    sbLink.Append(strCounselCd);

                    lnkbtnInput.PostBackUrl = sbLink.ToString();

                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        /// <summary>
        /// 데이터 바인딩
        /// </summary>
        private void LoadData()
        {
            if (string.IsNullOrEmpty(hfOrderCd.Value))
            {
                hfOrderCd.Value = string.Empty;
            }

            string strStartInsDt = (hfStartInsDt.Value).Replace("-", "");
            string strEndInsDt = (hfEndInsDt.Value).Replace("-", "");
            string strStartLeaseDt = (hfStartLeaseDt.Value).Replace("-", "");
            string strEndLeaseDt = (hfEndLeaseDt.Value).Replace("-", "");

            DataSet dsReturn = new DataSet();

            // KN_USP_RES_SELECT_COUNSELINFO_S00
            dsReturn = CounselMngBlo.SpreadCounselInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfCounselCd.Text, ddlKeyCd.SelectedValue, txtKeyWord.Text,
                                                       Session["MemAuthTy"].ToString(), Session["LangCd"].ToString(),Session["CompCd"].ToString(), Session["MemNo"].ToString(),
                                                       hfOrderCd.Value, strStartInsDt, strEndInsDt, CommValue.CODE_VALUE_EMPTY, ddlIndustry.SelectedValue, strStartLeaseDt, strEndLeaseDt);

            if (dsReturn != null)
            {
                lvCounselList.DataSource = dsReturn.Tables[1];
                lvCounselList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString()),
                                                         TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));
                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCounselList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvCounselList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvCounselList.FindControl("ltCompNm")).Text = TextNm["COMPNM"];
            ((Literal)lvCounselList.FindControl("ltCountry")).Text = TextNm["COUNTRY"];
            ((Literal)lvCounselList.FindControl("ltIndus")).Text = TextNm["INDUS"];
            ((Literal)lvCounselList.FindControl("ltUseArea")).Text = TextNm["USEAREA"];
            ((Literal)lvCounselList.FindControl("ltLeaseFee")).Text = TextNm["CURRRENTAL"];
            ((Literal)lvCounselList.FindControl("ltExpectedArea")).Text = TextNm["LEASINGAREAINNEED"];
            ((Literal)lvCounselList.FindControl("ltExpectedPeriod")).Text = TextNm["EXPECTEDPERIOD"];
            ((Literal)lvCounselList.FindControl("ltInsDt")).Text = TextNm["REGISTDATE"];

            ImageButton imgbtnSort = (ImageButton)lvCounselList.FindControl("imgbtnSort");
            ImageButton imgbtnSort1 = (ImageButton)lvCounselList.FindControl("imgbtnSort1");
            ImageButton imgbtnSort2 = (ImageButton)lvCounselList.FindControl("imgbtnSort2");
            ImageButton imgbtnSort3 = (ImageButton)lvCounselList.FindControl("imgbtnSort3");
            ImageButton imgbtnSort4 = (ImageButton)lvCounselList.FindControl("imgbtnSort4");
            ImageButton imgbtnSort5 = (ImageButton)lvCounselList.FindControl("imgbtnSort5");
            ImageButton imgbtnSort6 = (ImageButton)lvCounselList.FindControl("imgbtnSort6");
            ImageButton imgbtnSort7 = (ImageButton)lvCounselList.FindControl("imgbtnSort7");
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCounselList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltCompNm")).Text = TextNm["COMPNM"];
                    ((Literal)e.Item.FindControl("ltCountry")).Text = TextNm["COUNTRY"];
                    ((Literal)e.Item.FindControl("ltIndus")).Text = TextNm["INDUS"];
                    ((Literal)e.Item.FindControl("ltUseArea")).Text = TextNm["USEAREA"];
                    ((Literal)e.Item.FindControl("ltLeaseFee")).Text = TextNm["CURRRENTAL"];
                    ((Literal)e.Item.FindControl("ltExpectedArea")).Text = TextNm["LEASINGAREAINNEED"];
                    ((Literal)e.Item.FindControl("ltExpectedPeriod")).Text = TextNm["EXPECTEDRENTAL"];
                    ((Literal)e.Item.FindControl("ltInsDt")).Text = TextNm["REGISTDATE"];

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
        protected void lvCounselList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["CounselSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSeq")).Text = drView["CounselSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsCompNm")).Text = TextLib.TextCutString(TextLib.StringDecoder(drView["CompNm"].ToString()), 30, "..");
                }

                if (!string.IsNullOrEmpty(drView["CountryNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsCountry")).Text = TextLib.StringDecoder(drView["CountryNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["IndustryNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsIndus")).Text = TextLib.TextCutString(TextLib.StringDecoder(drView["IndustryNm"].ToString()), 12, "..");
                }

                if (!string.IsNullOrEmpty(drView["UsingAreaNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsUseArea")).Text = TextLib.StringDecoder(drView["UsingAreaNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["CurrRentalNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsLeaseFee")).Text = TextLib.StringDecoder(drView["CurrRentalNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["LeaseAreaNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsExpectedArea")).Text = drView["LeaseAreaNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ExpectedlPeriodNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsExpectedPeriod")).Text = drView["ExpectedlPeriodNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ModDt"].ToString()))
                {
                    string strModDt = drView["ModDt"].ToString();
                    StringBuilder sbModDt = new StringBuilder();

                    sbModDt.Append(strModDt.Substring(0, 4));
                    sbModDt.Append(".");
                    sbModDt.Append(strModDt.Substring(4, 2));
                    sbModDt.Append(".");
                    sbModDt.Append(strModDt.Substring(6, 2));

                    ((Literal)iTem.FindControl("ltInsInsDt")).Text = sbModDt.ToString();
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
                sbView.Append(hfCounselCd.Value);
                sbView.Append("&");
                sbView.Append(Master.PARAM_DATA2);
                sbView.Append("=");
                sbView.Append(hfCounselSeq.Value);

                Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);
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

                ImageButton imgbtnSort = (ImageButton)lvCounselList.FindControl("imgbtnSort");
                ImageButton imgbtnSort1 = (ImageButton)lvCounselList.FindControl("imgbtnSort1");
                ImageButton imgbtnSort2 = (ImageButton)lvCounselList.FindControl("imgbtnSort2");
                ImageButton imgbtnSort3 = (ImageButton)lvCounselList.FindControl("imgbtnSort3");
                ImageButton imgbtnSort4 = (ImageButton)lvCounselList.FindControl("imgbtnSort4");
                ImageButton imgbtnSort5 = (ImageButton)lvCounselList.FindControl("imgbtnSort5");
                ImageButton imgbtnSort6 = (ImageButton)lvCounselList.FindControl("imgbtnSort6");
                ImageButton imgbtnSort7 = (ImageButton)lvCounselList.FindControl("imgbtnSort7");

                hfOrderCd.Value = ((System.Web.UI.WebControls.LinkButton)(sender)).CommandArgument;

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                LoadSearch(imgbtnSort, imgbtnSort1, imgbtnSort2, imgbtnSort3, imgbtnSort4, imgbtnSort5, imgbtnSort6, imgbtnSort7);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnSort_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                ImageButton imgbtnSort = (ImageButton)lvCounselList.FindControl("imgbtnSort");
                ImageButton imgbtnSort1 = (ImageButton)lvCounselList.FindControl("imgbtnSort1");
                ImageButton imgbtnSort2 = (ImageButton)lvCounselList.FindControl("imgbtnSort2");
                ImageButton imgbtnSort3 = (ImageButton)lvCounselList.FindControl("imgbtnSort3");
                ImageButton imgbtnSort4 = (ImageButton)lvCounselList.FindControl("imgbtnSort4");
                ImageButton imgbtnSort5 = (ImageButton)lvCounselList.FindControl("imgbtnSort5");
                ImageButton imgbtnSort6 = (ImageButton)lvCounselList.FindControl("imgbtnSort6");
                ImageButton imgbtnSort7 = (ImageButton)lvCounselList.FindControl("imgbtnSort7");

                hfOrderCd.Value = ((System.Web.UI.WebControls.ImageButton)(sender)).CommandArgument;

                LoadSearch(imgbtnSort, imgbtnSort1, imgbtnSort2, imgbtnSort3, imgbtnSort4, imgbtnSort5, imgbtnSort6, imgbtnSort7);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void LoadSearch(ImageButton imgbtnSort, ImageButton imgbtnSort1, ImageButton imgbtnSort2, ImageButton imgbtnSort3, ImageButton imgbtnSort4, ImageButton imgbtnSort5, ImageButton imgbtnSort6, ImageButton imgbtnSort7)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            if (hfOrderCd.Value.Equals("0001"))
            {
                imgbtnSort.ImageUrl = "/Common/Images/Icon/sortIcon.gif";
                imgbtnSort1.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort2.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort3.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort4.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort5.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort6.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort7.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";

            }
            else if (hfOrderCd.Value.Equals("0002"))
            {
                imgbtnSort.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort1.ImageUrl = "/Common/Images/Icon/sortIcon.gif";
                imgbtnSort2.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort3.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort4.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort5.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort6.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort7.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
            }
            else if (hfOrderCd.Value.Equals("0003"))
            {
                imgbtnSort.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort1.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort2.ImageUrl = "/Common/Images/Icon/sortIcon.gif";
                imgbtnSort3.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort4.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort5.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort6.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort7.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
            }
            else if (hfOrderCd.Value.Equals("0004"))
            {
                imgbtnSort.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort1.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort2.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort3.ImageUrl = "/Common/Images/Icon/sortIcon.gif";
                imgbtnSort4.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort5.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort6.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort7.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
            }
            else if (hfOrderCd.Value.Equals("0005"))
            {
                imgbtnSort.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort1.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort2.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort3.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort4.ImageUrl = "/Common/Images/Icon/sortIcon.gif";
                imgbtnSort5.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort6.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort7.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
            }
            else if (hfOrderCd.Value.Equals("0006"))
            {
                imgbtnSort.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort1.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort2.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort3.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort4.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort5.ImageUrl = "/Common/Images/Icon/sortIcon.gif";
                imgbtnSort6.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort7.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
            }
            else if (hfOrderCd.Value.Equals("0007"))
            {
                imgbtnSort.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort1.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort2.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort3.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort4.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort5.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort6.ImageUrl = "/Common/Images/Icon/sortIcon.gif";
                imgbtnSort7.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
            }
            else
            {
                imgbtnSort.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort1.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort2.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort3.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort4.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort5.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort6.ImageUrl = "/Common/Images/Icon/sortIcon2.gif";
                imgbtnSort7.ImageUrl = "/Common/Images/Icon/sortIcon.gif";
            }

            LoadData();
        }
    }
}