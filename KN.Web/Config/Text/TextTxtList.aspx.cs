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
    public partial class TextTxtList : BasePage
    {
        DataTable dtData = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                dtData = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_TEXT, CommValue.TEXT_TYPE_VALUE_ITEM);

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
            ltTxtTy.Text = TextNm["ALERTTY"];
            ltExpressCd.Text = TextNm["EXPRESSCD"];
            ltTxtVi.Text = TextNm["VIETLANG"];
            ltTxtEn.Text = TextNm["ENLANG"];
            ltTxtKr.Text = TextNm["KORLANG"];
            ltUseYn.Text = TextNm["USEYN"];

            CommCdDdlUtil.MakeSubCdDdlTitle(ddlTopTxtTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_TEXT, CommValue.TEXT_TYPE_VALUE_ITEM);
            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlSearch, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_ALERT, TextNm["SELECT"]);
            CommCdDdlUtil.MakeCommCdDdl(ddlTxtTy, dtData);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            imgbtnRegist.OnClientClick = "javascript:return fnCheckData('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["CONF_REGIST_ITEM"] + "');";
        }

        private void BindData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_TXTINFO_S00
            dtReturn = TxtMngBlo.SpreadTxtInfo(ddlTopTxtTy.SelectedValue, ddlSearch.SelectedValue, txtSearch.Text);

            if (dtReturn != null)
            {
                lvTxtList.DataSource = dtReturn;
                lvTxtList.DataBind();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvTxtList_ItemCreated(object sender, ListViewItemEventArgs e)
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
        protected void lvTxtList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["TxtTy"].ToString()))
                {
                    DropDownList ddlTmp = (DropDownList)iTem.FindControl("ddlTxtTy");
                    CommCdDdlUtil.MakeCommCdDdl(ddlTmp, dtData);
                    ddlTmp.SelectedValue = drView["TxtTy"].ToString();

                    ((TextBox)iTem.FindControl("txtHfTxtTy")).Text = drView["TxtTy"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TxtSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfTxtSeq")).Text = drView["TxtSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ExpressCd"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtExpressCd")).Text = drView["ExpressCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfExpressCd")).Text = drView["ExpressCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TxtVi"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtTxtVi")).Text = drView["TxtVi"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TxtEn"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtTxtEn")).Text = drView["TxtEn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TxtKr"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtTxtKr")).Text = drView["TxtKr"].ToString();
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

        protected void lvTxtList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DropDownList ddlTmpTxtTy = (DropDownList)lvTxtList.Items[e.ItemIndex].FindControl("ddlTxtTy");
                TextBox txtTmpTxtSeq = (TextBox)lvTxtList.Items[e.ItemIndex].FindControl("txtHfTxtSeq");

                // KN_USP_MNG_DELETE_TXTINFO_M00
                TxtMngBlo.RemoveTxtInfo(ddlTmpTxtTy.SelectedValue, txtTmpTxtSeq.Text);

                Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvTxtList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DropDownList ddlTmpTxtTy = (DropDownList)lvTxtList.Items[e.ItemIndex].FindControl("ddlTxtTy");
                DropDownList ddlTmpUseYn = (DropDownList)lvTxtList.Items[e.ItemIndex].FindControl("ddlUseYn");

                TextBox txtHfTmpTxtTy = (TextBox)lvTxtList.Items[e.ItemIndex].FindControl("txtHfTxtTy");
                TextBox txtHfTmpExpressCd = (TextBox)lvTxtList.Items[e.ItemIndex].FindControl("txtHfExpressCd");

                TextBox txtTmpTxtSeq = (TextBox)lvTxtList.Items[e.ItemIndex].FindControl("txtHfTxtSeq");
                TextBox txtTmpExpressCd = (TextBox)lvTxtList.Items[e.ItemIndex].FindControl("txtExpressCd");
                TextBox txtTmpTxtVi = (TextBox)lvTxtList.Items[e.ItemIndex].FindControl("txtTxtVi");
                TextBox txtTmpTxtEn = (TextBox)lvTxtList.Items[e.ItemIndex].FindControl("txtTxtEn");
                TextBox txtTmpTxtKr = (TextBox)lvTxtList.Items[e.ItemIndex].FindControl("txtTxtKr");

                // 어휘중 누락된 부분이 있을 경우 체크
                if (!string.IsNullOrEmpty(txtTmpTxtVi.Text) &&
                    !string.IsNullOrEmpty(txtTmpTxtEn.Text) &&
                    !string.IsNullOrEmpty(txtTmpTxtKr.Text))
                {
                    // 타입 혹은 표현코드가 변경된 부분이 있는지 체크
                    if ((ddlTmpTxtTy.SelectedValue.Equals(txtHfTmpTxtTy.Text)) && (txtTmpExpressCd.Text.Equals(txtHfTmpExpressCd.Text)))
                    {
                        // 타입 혹은 표현코드가 변경된 부분이 없을 경우 처리
                        // KN_USP_MNG_UPDATE_TXTINFO_M00
                        TxtMngBlo.ModifyTxtInfo(ddlTmpTxtTy.SelectedValue, txtTmpTxtSeq.Text, txtTmpExpressCd.Text, txtTmpTxtVi.Text, txtTmpTxtEn.Text, txtTmpTxtKr.Text, ddlTmpUseYn.SelectedValue);

                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        // 타입 혹은 표현코드가 변경된 부분이 있을 경우 처리
                        DataTable dtReturn = new DataTable();

                        // 변경된 데이터와 다른 데이터의 중복 여부 체크
                        // KN_USP_MNG_SELECT_TXTINFO_S01
                        dtReturn = TxtMngBlo.WatchExistTxtInfo(ddlTmpTxtTy.SelectedValue, txtTmpExpressCd.Text);

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count == 0)
                            {
                                // 기존 데이터가 존재하지 않으므로 수정처리
                                // 타입이 변경되었는지 체크
                                if (ddlTmpTxtTy.SelectedValue.Equals(txtHfTmpTxtTy.Text))
                                {
                                    // 타입이 변경되지 않았을 경우
                                    // KN_USP_MNG_UPDATE_TXTINFO_M00
                                    TxtMngBlo.ModifyTxtInfo(ddlTmpTxtTy.SelectedValue, txtTmpTxtSeq.Text, txtTmpExpressCd.Text, txtTmpTxtVi.Text, txtTmpTxtEn.Text, txtTmpTxtKr.Text, ddlTmpUseYn.SelectedValue);

                                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                                }
                                else
                                {
                                    // 타입이 변경되었을 경우
                                    // KN_USP_MNG_INSERT_TXTINFO_M00
                                    // KN_USP_MNG_DELETE_TXTINFO_M00
                                    TxtMngBlo.ModifyExistTxtInfo(txtHfTmpTxtTy.Text, ddlTmpTxtTy.SelectedValue, txtTmpTxtSeq.Text, txtTmpExpressCd.Text, txtTmpTxtVi.Text, txtTmpTxtEn.Text, txtTmpTxtKr.Text, ddlTmpUseYn.SelectedValue);

                                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                                }
                            }
                            else
                            {
                                // 기존 데이터가 존재하므로 에러처리
                                hfExgistMsg.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];
                            }
                        }
                    }
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

                // 데이터 중복 여부 체크
                // KN_USP_MNG_SELECT_TXTINFO_S01
                dtReturn = TxtMngBlo.WatchExistTxtInfo(ddlTxtTy.SelectedValue, txtExpressCd.Text);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count == 0)
                    {
                        // KN_USP_MNG_INSERT_TXTINFO_M00
                        TxtMngBlo.RegistryTxtInfo(ddlTxtTy.SelectedValue, txtExpressCd.Text, txtTxtVi.Text, txtTxtEn.Text, txtTxtKr.Text);

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