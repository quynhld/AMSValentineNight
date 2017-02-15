using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Common.Method.Lib;

using KN.Stock.Biz;

namespace KN.Web.Stock.Release
{
    public partial class ReleaseRequestList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        string strViewDt = string.Empty;
        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    InitControls();

                    CheckParams();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        private void InitControls()
        {
            ltSearch.Text = TextNm["SEARCH"];
            ltCdNm.Text = TextNm["CDNM"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnReset.Text = TextNm["RESET"];

            //lnkbtnPrint.Text = TextNm["PRINT"];
            lnkbtnRegist.Text = TextNm["REGIST"];
            lnkbtnRegist.Visible = Master.isWriteAuthOk;

            CommCdDdlUtil.MakeEtcSubCdDdlUserTitle(ddlRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL, TextNm["ENTIRE"]);

            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlProcessCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_RELEASE, TextNm["ENTIRE"]);

            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlStatusCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_APPROVAL, TextNm["ENTIRE"]);

            MakeSvcZoneDdl();

            MakeTopGrpDdl();

            MakeTopMainDdl();

            MakeTopSubDdl();
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
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_STK_SELECT_RELEASEREQUESTINFO_S00
            dsReturn = ReleaseInfoBlo.SpreadReleaseRequestInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value),
                ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue, ddlMainCd.SelectedValue, ddlSubCd.SelectedValue, 
                txtCdNm.Text, Session["LangCd"].ToString(), ddlProcessCd.SelectedValue, ddlStatusCd.SelectedValue);

            if (dsReturn != null)
            {
                lvReleaseList.DataSource = dsReturn.Tables[1];
                lvReleaseList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        private void MakeSvcZoneDdl()
        {
            ddlSvcZoneCd.Items.Clear();

            ddlSvcZoneCd.Items.Add(new ListItem(TextNm["ENTIRE"], ""));

            if (!string.IsNullOrEmpty(ddlRentCd.SelectedValue))
            {
                // KN_USP_STK_SELECT_WAREHOUSEINFO_S01
                DataTable dtSvcSection = WarehouseMngBlo.SpreadWarehouseInfo(ddlRentCd.SelectedValue);

                foreach (DataRow dr in dtSvcSection.Select())
                {
                    ddlSvcZoneCd.Items.Add(new ListItem(dr["SvcZoneCd"].ToString(), dr["SvcZoneCd"].ToString()));
                }
            }
        }

        private void MakeTopGrpDdl()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIGRPCD_S01
            DataTable dtGrpReturn = MaterialMngBlo.SpreadClassiGrpCdInfo(Session["LangCd"].ToString(), strViewDt, ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue);

            ddlGrpCd.Items.Clear();

            ddlGrpCd.DataSource = dtGrpReturn;
            ddlGrpCd.DataTextField = "CodeNm";
            ddlGrpCd.DataValueField = "CodeCd";
            ddlGrpCd.DataBind();

            ddlGrpCd.Items.Insert(0, new ListItem(TextNm["ENTIRE"], ""));

        }

        private void MakeTopMainDdl()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIMAINCD_S01
            DataTable dtMainReturn = MaterialMngBlo.SpreadClassiMainCdInfo(Session["LangCd"].ToString(), strViewDt, ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue);

            ddlMainCd.Items.Clear();

            ddlMainCd.DataSource = dtMainReturn;
            ddlMainCd.DataTextField = "CodeNm";
            ddlMainCd.DataValueField = "CodeCd";
            ddlMainCd.DataBind();

            ddlMainCd.Items.Insert(0, new ListItem(TextNm["ENTIRE"], ""));
        }

        private void MakeTopSubDdl()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSISUBCD_S00
            DataTable dtSubReturn = MaterialMngBlo.SpreadClassiSubCdInfo(ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue, ddlMainCd.SelectedValue);

            ddlSubCd.Items.Clear();

            ddlSubCd.DataSource = dtSubReturn;
            ddlSubCd.DataTextField = "CodeNm";
            ddlSubCd.DataValueField = "CodeCd";
            ddlSubCd.DataBind();

            ddlSubCd.Items.Insert(0, new ListItem(TextNm["ENTIRE"], ""));
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvReleaseList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvReleaseList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvReleaseList.FindControl("ltDpt")).Text = TextNm["DEPT"];
            ((Literal)lvReleaseList.FindControl("ltMemNm")).Text = TextNm["MEM"] + " " + TextNm["NAME"];
            ((Literal)lvReleaseList.FindControl("ltItemNm")).Text = TextNm["ITEM"] + " " + TextNm["NAME"];
            ((Literal)lvReleaseList.FindControl("ltItemCd")).Text = TextNm["ITEM"] + " " + TextNm["CD"];
            ((Literal)lvReleaseList.FindControl("ltQty")).Text = TextNm["RELEASE"] + " " + TextNm["REQUEST"] + " " + TextNm["QTY"];
            ((Literal)lvReleaseList.FindControl("ltProcessNm")).Text = TextNm["RELEASESTATUS"];
            ((Literal)lvReleaseList.FindControl("ltStateNm")).Text = TextNm["STATUS"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvReleaseList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltDpt")).Text = TextNm["DEPT"];
                    ((Literal)e.Item.FindControl("ltMemNm")).Text = TextNm["MEM"] + " " + TextNm["NAME"];
                    ((Literal)e.Item.FindControl("ltItemNm")).Text = TextNm["ITEM"] + " " + TextNm["NAME"];
                    ((Literal)e.Item.FindControl("ltItemCd")).Text = TextNm["ITEM"] + " " + TextNm["CD"];
                    ((Literal)e.Item.FindControl("ltQty")).Text = TextNm["RELEASE"] + " " + TextNm["REQUEST"] + " " + TextNm["QTY"];
                    ((Literal)e.Item.FindControl("ltProcessNm")).Text = TextNm["PURREQSTATUS"];
                    ((Literal)e.Item.FindControl("ltStateNm")).Text = TextNm["STATUS"];

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
        protected void lvReleaseList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["RealSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSeq")).Text = drView["RealSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ProcessDeptNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsDpt")).Text = drView["ProcessDeptNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ProcessMemNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsMemNm")).Text = drView["ProcessMemNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItemNm")).Text = drView["ClassNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiGrpCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiMainCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItemCd")).Text = drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + "-" + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["Qty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = drView["Qty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ProgressNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsProcessNm")).Text = drView["ProgressNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StateNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsStateNm")).Text = drView["StateNm"].ToString();
                }
            }
        }

        /// <summary>
        /// 페이지 이동 이벤트 처리
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
        /// 상세보기 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbList = new StringBuilder();

                sbList.Append(Master.PAGE_VIEW);
                sbList.Append("?");
                sbList.Append(Master.PARAM_DATA1);
                sbList.Append("=");
                sbList.Append(hfReleaseSeq.Value);

                Response.Redirect(sbList.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlRentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeSvcZoneDdl();
                MakeTopGrpDdl();
                MakeTopMainDdl();
                MakeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlSvcZoneCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeTopGrpDdl();
                MakeTopMainDdl();
                MakeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlGrpCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeTopMainDdl();
                MakeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlMainCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
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

        protected void lnkbtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                ddlRentCd.SelectedValue = string.Empty;
                ddlGrpCd.SelectedValue = string.Empty;
                ddlMainCd.SelectedValue = string.Empty;
                ddlSubCd.SelectedValue = string.Empty;
                ddlProcessCd.SelectedValue = string.Empty;
                ddlStatusCd.SelectedValue = string.Empty;
                txtCdNm.Text = string.Empty;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_WRITE, CommValue.AUTH_VALUE_FALSE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}
