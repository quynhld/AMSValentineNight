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

namespace KN.Web.Stock.Warehousing
{
    public partial class WarehouseMngList : BasePage
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
            ltSvcCd.Text = TextNm["WAREHOUSECD"];
            ltRemark.Text = TextNm["REMARK"];
            lnkbtnSearch.Text = TextNm["SEARCH"];

            txtSvcCd.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            imgbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            CommCdDdlUtil.MakeEtcSubCdDdlUserTitle(ddlTopRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL, TextNm["ENTIRE"]);
            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL);
        }

        protected void LoadData()
        {
            // KN_USP_STK_SELECT_WAREHOUSEINFO_S00
            DataSet dsReturn = WarehouseMngBlo.SpreadWarehouseInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), ddlTopRentCd.SelectedValue, txtTopSvcCd.Text, txtTopRemark.Text);

            if (dsReturn != null)
            {
                lvCargoList.DataSource = dsReturn.Tables[1];
                lvCargoList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// 레이아웃 컨트롤 세팅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCargoList_LayoutCreated(object sender, EventArgs e)
        {
            ((Literal)lvCargoList.FindControl("ltSection")).Text = TextNm["SECTIONCD"];
            ((Literal)lvCargoList.FindControl("ltSvcCd")).Text = TextNm["WAREHOUSECD"];
            ((Literal)lvCargoList.FindControl("ltRemark")).Text = TextNm["REMARK"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCargoList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    ((Literal)e.Item.FindControl("ltSection")).Text = TextNm["SECTIONCD"];
                    ((Literal)e.Item.FindControl("ltSvcCd")).Text = TextNm["WAREHOUSECD"];
                    ((Literal)e.Item.FindControl("ltRemark")).Text = TextNm["REMARK"];

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
        /// ListView Data Binding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCargoList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                DropDownList ddlInsRentCd = (DropDownList)iTem.FindControl("ddlRentCd");

                CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL);

                if (!string.IsNullOrEmpty(drView["SectionCd"].ToString()))
                {
                    ddlInsRentCd.SelectedValue = drView["SectionCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtSvcCd")).Text = drView["SvcZoneCd"].ToString();
                    ((HiddenField)iTem.FindControl("hfSvcCd")).Value = drView["SvcZoneCd"].ToString();
                }

                TextBox txtRemarkTxt = (TextBox)iTem.FindControl("txtRemarkTxt");

                if (!string.IsNullOrEmpty(drView["Remark"].ToString()))
                {
                    txtRemarkTxt.Text = drView["Remark"].ToString();
                }

                ImageButton imgbtnUpdate = (ImageButton)iTem.FindControl("imgbtnModify");
                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnUpdate.OnClientClick = "javascript:return fnModifyConfirm('" + txtRemarkTxt.ClientID + "','" + AlertNm["CONF_MODIFY_ITEM"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "')";
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            }
        }

        protected void lvCargoList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DropDownList ddlRentCd = (DropDownList)lvCargoList.Items[e.ItemIndex].FindControl("ddlRentCd");
                HiddenField hfSvcCd = (HiddenField)lvCargoList.Items[e.ItemIndex].FindControl("hfSvcCd");

                // KN_USP_STK_DELETE_WAREHOUSEINFO_M00
                WarehouseMngBlo.RemoveWarehouseInfo(ddlRentCd.SelectedValue, hfSvcCd.Value);

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvCargoList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DropDownList ddlRentCd = (DropDownList)lvCargoList.Items[e.ItemIndex].FindControl("ddlRentCd");
                HiddenField hfSvcCd = (HiddenField)lvCargoList.Items[e.ItemIndex].FindControl("hfSvcCd");
                TextBox txtRemarkTxt = (TextBox)lvCargoList.Items[e.ItemIndex].FindControl("txtRemarkTxt");
                string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_STK_UPDATE_WAREHOUSEINFO_M00
                WarehouseMngBlo.ModifyWarehouseInfo(ddlRentCd.SelectedValue, hfSvcCd.Value, txtRemarkTxt.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                LoadData();
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
                
        protected void imgbtnRegist_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_STK_INSERT_WAREHOUSEINFO_M00
                WarehouseMngBlo.RegistryWarehouseInfo(ddlRentCd.SelectedValue, txtSvcCd.Text, txtRemarkTxt.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIP);

                ResetContorls();
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

                ResetContorls();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ResetContorls()
        {
            txtSvcCd.Text = string.Empty;
            txtRemarkTxt.Text = string.Empty;
        }
    }
}
