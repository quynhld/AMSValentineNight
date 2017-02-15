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
    public partial class AlertTxtList : BasePage
    {
        DataTable dtData = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                dtData = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_TEXT, CommValue.TEXT_TYPE_VALUE_ALERT);

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
            ltAlertTy.Text = TextNm["ALERTTY"];
            ltExpressCd.Text = TextNm["EXPRESSCD"];
            ltAlertVi.Text = TextNm["VIETLANG"];
            ltAlertEn.Text = TextNm["ENLANG"];
            ltAlertKr.Text = TextNm["KORLANG"];
            ltUseYn.Text = TextNm["USEYN"];

            CommCdDdlUtil.MakeSubCdDdlTitle(ddlTopAlertTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_TEXT, CommValue.TEXT_TYPE_VALUE_ALERT);
            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlSearch, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_ALERT, TextNm["SELECT"]);
            CommCdDdlUtil.MakeCommCdDdl(ddlAlertTy, dtData);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            imgbtnRegist.OnClientClick = "javascript:return fnCheckData('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["CONF_REGIST_ITEM"] + "');";
        }

        private void BindData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_ALERTINFO_S00
            dtReturn = TxtMngBlo.SpreadAlertInfo(ddlTopAlertTy.SelectedValue, ddlSearch.SelectedValue, txtSearch.Text);

            if (dtReturn != null)
            {
                lvAlertList.DataSource = dtReturn;
                lvAlertList.DataBind();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvAlertList_ItemCreated(object sender, ListViewItemEventArgs e)
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
        protected void lvAlertList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["AlertTy"].ToString()))
                {
                    DropDownList ddlTmp = (DropDownList)iTem.FindControl("ddlAlertTy");
                    CommCdDdlUtil.MakeCommCdDdl(ddlTmp, dtData);
                    ddlTmp.SelectedValue = drView["AlertTy"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AlertSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfAlertSeq")).Text = drView["AlertSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ExpressCd"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtExpressCd")).Text = drView["ExpressCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AlertVi"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtAlertVi")).Text = drView["AlertVi"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AlertEn"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtAlertEn")).Text = drView["AlertEn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AlertKr"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtAlertKr")).Text = drView["AlertKr"].ToString();
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

        protected void lvAlertList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DropDownList ddlTmpAlertTy = (DropDownList)lvAlertList.Items[e.ItemIndex].FindControl("ddlAlertTy");
                TextBox txtTmpAlertSeq = (TextBox)lvAlertList.Items[e.ItemIndex].FindControl("txtHfAlertSeq");

                // KN_USP_MNG_DELETE_ALERTINFO_M00
                TxtMngBlo.RemoveAlertInfo(ddlTmpAlertTy.SelectedValue, txtTmpAlertSeq.Text);
                Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvAlertList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DropDownList ddlTmpAlertTy = (DropDownList)lvAlertList.Items[e.ItemIndex].FindControl("ddlAlertTy");
                DropDownList ddlTmpUseYn = (DropDownList)lvAlertList.Items[e.ItemIndex].FindControl("ddlUseYn");
                TextBox txtTmpAlertSeq = (TextBox)lvAlertList.Items[e.ItemIndex].FindControl("txtHfAlertSeq");
                TextBox txtTmpExpressCd = (TextBox)lvAlertList.Items[e.ItemIndex].FindControl("txtExpressCd");
                TextBox txtTmpAlertVi = (TextBox)lvAlertList.Items[e.ItemIndex].FindControl("txtAlertVi");
                TextBox txtTmpAlertEn = (TextBox)lvAlertList.Items[e.ItemIndex].FindControl("txtAlertEn");
                TextBox txtTmpAlertKr = (TextBox)lvAlertList.Items[e.ItemIndex].FindControl("txtAlertKr");

                if (!string.IsNullOrEmpty(txtTmpAlertVi.Text) &&
                    !string.IsNullOrEmpty(txtTmpAlertEn.Text) &&
                    !string.IsNullOrEmpty(txtTmpAlertKr.Text))
                {
                    // KN_USP_MNG_UPDATE_ALERTINFO_M00
                    TxtMngBlo.ModifyAlertInfo(ddlTmpAlertTy.SelectedValue, txtTmpAlertSeq.Text, txtTmpExpressCd.Text, txtTmpAlertVi.Text, txtTmpAlertEn.Text, txtTmpAlertKr.Text, ddlTmpUseYn.SelectedValue);

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

                // KN_USP_MNG_SELECT_ALERTINFO_S01
                dtReturn = TxtMngBlo.WatchExistAlertInfo(ddlAlertTy.SelectedValue, txtExpressCd.Text);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count == 0)
                    {
                        // KN_USP_MNG_INSERT_ALERTINFO_M00
                        TxtMngBlo.RegistryAlertInfo(ddlAlertTy.SelectedValue, txtExpressCd.Text, txtAlertVi.Text, txtAlertEn.Text, txtAlertKr.Text);

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