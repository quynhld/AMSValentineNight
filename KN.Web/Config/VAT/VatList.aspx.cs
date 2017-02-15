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

namespace KN.Web.Config.VAT
{
    public partial class VatList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    CheckParams();

                    InitControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

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

        protected void InitControls()
        {
            ltTopItems.Text = TextNm["ITEM"];
            ltTopStartDt.Text = TextNm["APPLYDT"];
            ltTopRatio.Text = TextNm["RATIO"];

            CommCdDdlUtil.MakeSubCdDdlTitle(ddlManageFee, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_ITEM);

            txtVatRatio.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            imgbtnAdd.OnClientClick = "javascript:return fnCheckData('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_CANT_SELECT_PREDATE"] + "')";
        }

        /// <summary>
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_MNG_SELECT_VATINFO_S03
            dsReturn = VatMngBlo.SpreadVatInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), Session["LangCd"].ToString(), string.Empty);

            if (dsReturn != null)
            {
                lvVatList.DataSource = dsReturn.Tables[1];
                lvVatList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ResetData()
        {
            ddlManageFee.SelectedValue = CommValue.CODE_VALUE_EMPTY;
            txtAppliedDt.Text = string.Empty;
            hfAppliedDt.Value = string.Empty;
            txtVatRatio.Text = string.Empty;
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvVatList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvVatList.FindControl("ltItems")).Text = TextNm["ITEM"];
            ((Literal)lvVatList.FindControl("ltStartDt")).Text = TextNm["FROM"];
            ((Literal)lvVatList.FindControl("ltEndDt")).Text = TextNm["TO"];
            ((Literal)lvVatList.FindControl("ltRatio")).Text = TextNm["RATIO"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvVatList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltItems")).Text = TextNm["ITEM"];
                    ((Literal)e.Item.FindControl("ltStartDt")).Text = TextNm["FROM"];
                    ((Literal)e.Item.FindControl("ltEndDt")).Text = TextNm["TO"];
                    ((Literal)e.Item.FindControl("ltRatio")).Text = TextNm["RATIO"];

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
        protected void lvVatList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["VatNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltManageFee")).Text = drView["VatNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StartDt"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltStartDt")).Text = TextLib.MakeDateEightDigit(drView["StartDt"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["EndDt"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltEndDt")).Text = TextLib.MakeDateEightDigit(drView["EndDt"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["VatRatio"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltVatRatio")).Text = drView["VatRatio"].ToString();
                }
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

        protected void imgbtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // 기존 데이터가 존재하는지 체크.
                // KN_USP_MNG_SELECT_VATINFO_S01
                DataTable dtReturn = VatMngBlo.SpreadExistVatInfo(ddlManageFee.SelectedValue);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        // 기존 데이터가 존재할 경우 수정
                        // KN_USP_MNG_UPDATE_VATINFO_M00
                        VatMngBlo.ModifyVatInfo(ddlManageFee.SelectedValue, hfAppliedDt.Value.Replace("-", ""), double.Parse(txtVatRatio.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                    }
                    else
                    {
                        // 기존 데이터가 존재하지 않을 경우 등록
                        // KN_USP_MNG_INSERT_VATINFO_M00
                        VatMngBlo.RegistryVatInfo(ddlManageFee.SelectedValue, hfAppliedDt.Value.Replace("-", ""), double.Parse(txtVatRatio.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);
                    }

                    LoadData();

                    ResetData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlManageFee_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAppliedDt.Text = string.Empty;
            hfAppliedDt.Value = string.Empty;
            txtVatRatio.Text = string.Empty;

            // 기존 데이터가 존재하는지 체크.
            // KN_USP_MNG_SELECT_VATINFO_S01
            DataTable dtReturn = VatMngBlo.SpreadExistVatInfo(ddlManageFee.SelectedValue);

            if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
            {
                // 기존 데이터가 존재할 경우 Hidden field에 최종 시작일 넣어줌.
                hfStartDt.Value = dtReturn.Rows[0]["StartDt"].ToString();
            }
        }
    }
}