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

using KN.Config.Biz;

namespace KN.Web.Config.Authority
{
    public partial class MemberMngList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        public string AlertText = string.Empty;
        int intPageNo = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // Alert Setting
                AlertText = AlertNm["INFO_HAS_NO_AUTH"];

                if (!IsPostBack)
                {
                    // 파라미터 초기화 
                    CheckParams();

                    // 컨트롤 초기화
                    InitControls();

                    // 데이터 로드 및 바인딩
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {
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

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            // Authority DropDownList Setting
            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_AUTHGRPINFO_S01
            dtReturn = AuthorityMngBlo.SpreadControlAuthGrpInfo(Session["CompCd"].ToString(), Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    ddlAuthGrp.Items.Clear();

                    ddlAuthGrp.Items.Insert(0, new ListItem(TextNm["AUTH"], ""));

                    foreach (DataRow dr in dtReturn.Select())
                    {
                        ddlAuthGrp.Items.Add(new ListItem(dr["MemAuthTyNm"].ToString(), dr["MemAuthTy"].ToString()));
                    }
                }
            }

            // Search Condition DropDownList Setting
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlKeyCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_ACCMNG);

            // Button Setting
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnWrite.Text = TextNm["ADD"];
            lnkbtnWrite.Visible = Master.isWriteAuthOk;
        }

        /// <summary>
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_COMM_SELECT_MEMINFO_S01
            dsReturn = MemberMngBlo.SpreadMemInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), ddlKeyCd.Text, txtKeyWord.Text, Session["MemAuthTy"].ToString(), ddlAuthGrp.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                lvMemberList.DataSource = dsReturn.Tables[1];
                lvMemberList.DataBind();

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
        protected void lvMemberList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvMemberList.FindControl("ltUserId")).Text = TextNm["USERID"];
            ((Literal)lvMemberList.FindControl("ltAuthGrp")).Text = TextNm["AUTHCD"];
            ((Literal)lvMemberList.FindControl("ltName")).Text = TextNm["NAME"];
            ((Literal)lvMemberList.FindControl("ltTel")).Text = TextNm["TEL"];
            ((Literal)lvMemberList.FindControl("ltMobile")).Text = TextNm["MOBILENO"];
            ((Literal)lvMemberList.FindControl("ltMail")).Text = TextNm["EMAIL"];
            ((Literal)lvMemberList.FindControl("ltInsDt")).Text = TextNm["REGISTDATE"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMemberList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltUserId")).Text = TextNm["USERID"];
                    ((Literal)e.Item.FindControl("ltAuthGrp")).Text = TextNm["AUTHCD"];
                    ((Literal)e.Item.FindControl("ltName")).Text = TextNm["NAME"];
                    ((Literal)e.Item.FindControl("ltTel")).Text = TextNm["TEL"];
                    ((Literal)e.Item.FindControl("ltMobile")).Text = TextNm["MOBILENO"];
                    ((Literal)e.Item.FindControl("ltMail")).Text = TextNm["EMAIL"];
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
        protected void lvMemberList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["UserId"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsUserId")).Text = drView["UserId"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MemAuthTyNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsAuthGrp")).Text = drView["MemAuthTyNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MemNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsName")).Text = TextLib.StringDecoder(drView["MemNm"].ToString());
                }

                Literal ltTelNo = ((Literal)iTem.FindControl("ltInsTel"));

                if (!string.IsNullOrEmpty(drView["TelTypeCd"].ToString()))
                {
                    ltTelNo.Text = drView["TelTypeCd"].ToString();

                    if (!string.IsNullOrEmpty(drView["TelFrontNo"].ToString()))
                    {
                        ltTelNo.Text = ltTelNo.Text + "-" + drView["TelFrontNo"].ToString();

                        if (!string.IsNullOrEmpty(drView["TelRearNo"].ToString()))
                        {
                            ltTelNo.Text = ltTelNo.Text + "-" + drView["TelRearNo"].ToString();
                        }
                    }
                }

                Literal ltMobileNo = ((Literal)iTem.FindControl("ltInsMobile"));

                if (!string.IsNullOrEmpty(drView["MobileTypeCd"].ToString()))
                {
                    ltMobileNo.Text = drView["MobileTypeCd"].ToString();

                    if (!string.IsNullOrEmpty(drView["MobileFrontNo"].ToString()))
                    {
                        ltMobileNo.Text = ltMobileNo.Text + "-" + drView["MobileFrontNo"].ToString();

                        if (!string.IsNullOrEmpty(drView["MobileRearNo"].ToString()))
                        {
                            ltMobileNo.Text = ltMobileNo.Text + "-" + drView["MobileRearNo"].ToString();
                        }
                    }
                }

                Literal ltEmail = ((Literal)iTem.FindControl("ltInsMail"));

                if (!string.IsNullOrEmpty(drView["EmailId"].ToString()))
                {
                    ltEmail.Text = TextLib.StringDecoder(drView["EmailId"].ToString());

                    if (!string.IsNullOrEmpty(drView["EmailServer"].ToString()))
                    {
                        ltEmail.Text = ltEmail.Text + "@" + TextLib.StringDecoder(drView["EmailServer"].ToString());
                    }
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
        /// 상세보기 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Session["MemAuthViewOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
                Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + hfMemSeq.Value + "&CompCd=" + hfCompCd.Value, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Session["MemAuthWriteOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                Response.Redirect(Master.PAGE_WRITE, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}