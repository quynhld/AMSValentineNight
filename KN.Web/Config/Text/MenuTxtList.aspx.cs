using System;
using System.Data;
using System.EnterpriseServices;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Common.Method.Lib;

using KN.Config.Biz;

namespace KN.Web.Config.Text
{
    [Transaction(TransactionOption.Required)]
    public partial class MenuTxtList : BasePage
    {
        DataTable dtData = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    InitControls();

                    BindData();
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
            ltMenuTxtSeq.Text = TextNm["SEQ"];
            ltExpressCd.Text = TextNm["EXPRESSCD"];
            ltMenuVi.Text = TextNm["VIETLANG"];
            ltMenuEn.Text = TextNm["ENLANG"];
            ltMenuKr.Text = TextNm["KORLANG"];
            ltUseYn.Text = TextNm["USEYN"];

            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlSearch, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_ALERT, TextNm["SELECT"]);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            imgbtnRegist.OnClientClick = "javascript:return fnCheckData('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["CONF_REGIST_ITEM"] + "');";
        }

        private void BindData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_MENUTXTINFO_S00
            dtReturn = TxtMngBlo.SpreadMenuTxtInfo(ddlSearch.SelectedValue, txtSearch.Text);

            if (dtReturn != null)
            {
                lvMenuList.DataSource = dtReturn;
                lvMenuList.DataBind();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMenuList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        /// <summary>
        /// ListView Data Binding
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMenuList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["MenuTxtSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtMenuTxtSeq")).Text = drView["MenuTxtSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ExpressCd"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtExpressCd")).Text = drView["ExpressCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MenuVi"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtMenuVi")).Text = drView["MenuVi"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MenuEn"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtMenuEn")).Text = drView["MenuEn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MenuKr"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtMenuKr")).Text = drView["MenuKr"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UseYn"].ToString()))
                {
                    ((DropDownList)iTem.FindControl("ddlUseYn")).SelectedValue = drView["UseYn"].ToString();
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "')";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            }
        }

        protected void lvMenuList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtTmpMenuTxtSeq = (TextBox)lvMenuList.Items[e.ItemIndex].FindControl("txtMenuTxtSeq");

                // KN_USP_MNG_DELETE_MENUTXTINFO_M00
                TxtMngBlo.RemoveMenuTxtInfo(txtTmpMenuTxtSeq.Text);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMenuList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DropDownList ddlTmpUseYn = (DropDownList)lvMenuList.Items[e.ItemIndex].FindControl("ddlUseYn");
                TextBox txtTmpMenuTxtSeq = (TextBox)lvMenuList.Items[e.ItemIndex].FindControl("txtMenuTxtSeq");
                TextBox txtTmpExpressCd = (TextBox)lvMenuList.Items[e.ItemIndex].FindControl("txtExpressCd");
                TextBox txtTmpMenuVi = (TextBox)lvMenuList.Items[e.ItemIndex].FindControl("txtMenuVi");
                TextBox txtTmpMenuEn = (TextBox)lvMenuList.Items[e.ItemIndex].FindControl("txtMenuEn");
                TextBox txtTmpMenuKr = (TextBox)lvMenuList.Items[e.ItemIndex].FindControl("txtMenuKr");

                if (!string.IsNullOrEmpty(txtTmpMenuVi.Text) &&
                    !string.IsNullOrEmpty(txtTmpMenuEn.Text) &&
                    !string.IsNullOrEmpty(txtTmpMenuKr.Text))
                {
                    // KN_USP_MNG_UPDATE_MENUTXTINFO_M00
                    TxtMngBlo.ModifyMenuTxtInfo(txtTmpMenuTxtSeq.Text, txtTmpExpressCd.Text, txtTmpMenuVi.Text, txtTmpMenuEn.Text, txtTmpMenuKr.Text, ddlTmpUseYn.SelectedValue);

                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    hfExgistMsg.Value = AlertNm["ALERT_INSERT_BLANK"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        #region 이벤트 처리 부분

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                BindData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnRegist_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DataTable dtReturn = new DataTable();

                // KN_USP_MNG_SELECT_MENUTXTINFO_S01
                dtReturn = TxtMngBlo.WatchExistMenuTxtInfo(txtExpressCd.Text);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count == 0)
                    {
                        // KN_USP_MNG_INSERT_MENUTXTINFO_M00
                        TxtMngBlo.RegistryMenuTxtInfo(txtExpressCd.Text, txtMenuVi.Text, txtMenuEn.Text, txtMenuKr.Text);

                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        hfExgistMsg.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        #endregion
    }
}