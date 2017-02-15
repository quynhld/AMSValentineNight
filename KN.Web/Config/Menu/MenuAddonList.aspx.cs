using System;
using System.Data;
using System.EnterpriseServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;

namespace KN.Web.Config.Menu
{
    [Transaction(TransactionOption.Required)]
    public partial class MenuAddonList : BasePage
    {
        DataTable dtLinkReturn = new DataTable();
        DataTable dtParamReturn = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // 세션체크
                    AuthCheckLib.CheckSession();

                    // 컨트롤 및 레이블 초기화
                    InitControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤 및 레이블 초기화
        /// </summary>
        protected void InitControls()
        {
            // 기본정보 세팅
            ltMenuTitle.Text = TextNm["TITLE"];
            ltMenuUrl.Text = TextNm["URL"];

            ltItemTitle.Text = TextNm["TITLE"];
            ltItemReadAuth.Text = TextNm["AUTH_READ"];
            ltItemWriteAuth.Text = TextNm["AUTH_WRITE"];
            ltItemModAuth.Text = TextNm["AUTH_MODDEL"];

            lnkbtnEntireRead.Text = TextNm["ENTIRE_SELECTION"];
            lnkbtnEntireRead.OnClientClick = "javascript:return fnCheckSelect('" + AlertNm["ALERT_SELECT_MENU"] + "');";

            lnkbtnEntireWrite.Text = TextNm["ENTIRE_SELECTION"];
            lnkbtnEntireWrite.OnClientClick = "javascript:return fnCheckSelect('" + AlertNm["ALERT_SELECT_MENU"] + "');";

            lnkbtnEntireMod.Text = TextNm["ENTIRE_SELECTION"];
            lnkbtnEntireMod.OnClientClick = "javascript:return fnCheckSelect('" + AlertNm["ALERT_SELECT_MENU"] + "');";

            lnkbtnAuthSave.Text = TextNm["MODIFY"];
            lnkbtnAuthSave.OnClientClick = "javascript:return fnCheckSelect('" + AlertNm["ALERT_SELECT_MENU"] + "');";

            // 추가정보중 링크정보 세팅
            dtLinkReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_LINK);

            CommCdDdlUtil.MakeCommCdDdl(ddlLink, dtLinkReturn);

            imgbtnLinkInsert.OnClientClick = "javascript:return fnCheckValidate('" + txtLink.ClientID + "','" + AlertNm["ALERT_SELECT_MENU"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            // 추가정보중 파라미터정보 세팅
            dtParamReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_PARAM);

            CommCdDdlUtil.MakeCommCdDdl(ddlParams, dtParamReturn);

            imgbtnParamInsert.OnClientClick = "javascript:return fnCheckValidate('" + txtParams.ClientID + "','" + AlertNm["ALERT_SELECT_MENU"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "');";
        }

        protected void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_MENUINFO_S01
            dtReturn = MenuMngBlo.SpreadMenuInfo(Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                lvMenulist.DataSource = dtReturn;
                lvMenulist.DataBind();
            }

            BindDetailData();
        }

        #region 메뉴목록 처리관련

        /// <summary>
        /// 빈 데이터일 경우 ItemTemplate 생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMenulist_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltMenuTitle")).Text = TextNm["TITLE"];
                    ((Literal)e.Item.FindControl("ltMenuUrl")).Text = TextNm["URL"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMenulist_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                int intWidth = 0;

                /* 각 컨트롤별 데이터 바인딩 시작 */
                if (!string.IsNullOrEmpty(drView["Depth1"].ToString()))
                {
                    if (Int32.Parse(drView["Depth1"].ToString()) > 0)
                    {
                        intWidth = intWidth + 3;
                    }
                }

                if (!string.IsNullOrEmpty(drView["Depth2"].ToString()))
                {
                    if (Int32.Parse(drView["Depth2"].ToString()) > 0)
                    {
                        intWidth = intWidth + 3;
                    }
                }

                if (!string.IsNullOrEmpty(drView["Depth3"].ToString()))
                {
                    if (Int32.Parse(drView["Depth3"].ToString()) > 0)
                    {
                        intWidth = intWidth + 3;
                    }
                }

                if (!string.IsNullOrEmpty(drView["Depth4"].ToString()))
                {
                    if (Int32.Parse(drView["Depth4"].ToString()) > 0)
                    {
                        intWidth = intWidth + 3;
                    }
                }

                if (!string.IsNullOrEmpty(drView["MenuTitle"].ToString()))
                {
                    Literal ltTmpTitle = ((Literal)iTem.FindControl("ltlMenuTitle"));

                    StringBuilder sbTitle = new StringBuilder();

                    sbTitle.Append("<b>");
                    sbTitle.Append("<img src=\"/Common/Images/Common/blank.gif\" width=\"");
                    sbTitle.Append((intWidth * 4).ToString());
                    sbTitle.Append("\" height=\"1px\" style=\"border:0px;\">");
                    sbTitle.Append(drView["MenuTxt"].ToString());
                    sbTitle.Append("</b>");

                    ltTmpTitle.Text = ltTmpTitle.Text + sbTitle.ToString();
                }

                if (!string.IsNullOrEmpty(drView["MenuUrl"].ToString()))
                {
                    Literal ltTmpUrl = ((Literal)iTem.FindControl("ltIMenuUrl"));

                    StringBuilder sbUrl = new StringBuilder();

                    sbUrl.Append(drView["MenuUrl"].ToString());

                    ltTmpUrl.Text = ltTmpUrl.Text + sbUrl.ToString();
                }
            }
        }

        #endregion

        #region 링크목록 처리관련

        /// <summary>
        /// Layout 생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLinkList_LayoutCreated(object sender, EventArgs e)
        {
            ((Literal)lvLinkList.FindControl("ltLinkTitle")).Text = TextNm["TITLE"];
            ((Literal)lvLinkList.FindControl("ltLinkContent")).Text = TextNm["HIDDEN"];
        }

        /// <summary>
        /// 빈 데이터일 경우 ItemTemplate 생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLinkList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltLinkTitle")).Text = TextNm["TITLE"];
                    ((Literal)e.Item.FindControl("ltLinkContent")).Text = TextNm["CONTENTS"];

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
        /// 링크등록 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnLinkInsert_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intMenuSeq = CommValue.NUMBER_VALUE_0;
                string strLinkCd = string.Empty;
                string strLinkPageNm = string.Empty;

                if (!string.IsNullOrEmpty(hfMenuSeq.Value))
                {
                    intMenuSeq = Int32.Parse(hfMenuSeq.Value);
                    strLinkCd = ddlLink.SelectedItem.Text;
                    strLinkPageNm = txtLink.Text;

                    // KN_USP_MNG_SELECT_MENULINK_S00
                    DataTable dtTmpLink = MenuMngBlo.WatchMenuLink(intMenuSeq, strLinkCd);

                    if (dtTmpLink != null)
                    {
                        if (dtTmpLink.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            StringBuilder sbWarning = new StringBuilder();

                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["INFO_CANT_INSERT_DEPTH"]);
                            sbWarning.Append("');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "LinkWarning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                            return;
                        }
                        else
                        {
                            // KN_USP_MNG_INSERT_MENULINK_M00
                            MenuMngBlo.RegistryMenuLink(intMenuSeq, strLinkCd, strLinkPageNm);
                        }
                    }
                }

                BindDetailData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 링크정보 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLinkList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                DropDownList ddlLinkList = (DropDownList)iTem.FindControl("ddlLink");

                CommCdDdlUtil.MakeCommCdDdl(ddlLinkList, dtLinkReturn);

                TextBox txtHfLink = (TextBox)iTem.FindControl("txtHfLink");
                TextBox txtLink = (TextBox)iTem.FindControl("txtLink");

                if (!string.IsNullOrEmpty(drView["LinkCd"].ToString()))
                {
                    ddlLinkList.SelectedItem.Text = drView["LinkCd"].ToString();
                    txtHfLink.Text = TextLib.StringDecoder(drView["LinkCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["LinkPageNm"].ToString()))
                {
                    txtLink.Text = TextLib.StringDecoder(drView["LinkPageNm"].ToString());
                }

                ImageButton imgbtnUpdate = (ImageButton)iTem.FindControl("imgbtnUpdate");
                imgbtnUpdate.OnClientClick = "javascript:return fnUpdateCheckValidate('" + txtLink.ClientID + "','" + AlertNm["ALERT_SELECT_MENU"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_MODIFY_ITEM"] + "');";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            }
        }

        #endregion

        #region 파라미터 처리관련

        /// <summary>
        /// Layout 생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvParamList_LayoutCreated(object sender, EventArgs e)
        {
            ((Literal)lvParamList.FindControl("ltParamsTitle")).Text = TextNm["TITLE"];
            ((Literal)lvParamList.FindControl("ltParamsContent")).Text = TextNm["PARAM"];
        }

        /// <summary>
        /// 빈 데이터일 경우 ItemTemplate 생성
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvParamList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltParamsTitle")).Text = TextNm["TITLE"];
                    ((Literal)e.Item.FindControl("ltParamsContent")).Text = TextNm["CONTENTS"];

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
        /// 파라미터등록 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnParamInsert_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int intMenuSeq = 0;
                string strParamCd = string.Empty;
                string strParamNm = string.Empty;

                if (!string.IsNullOrEmpty(hfMenuSeq.Value))
                {
                    intMenuSeq = Int32.Parse(hfMenuSeq.Value);
                    strParamCd = ddlParams.SelectedItem.Text;
                    strParamNm = txtParams.Text;

                    // KN_USP_MNG_SELECT_MENUPARAM_S00
                    DataTable dtTmpLink = MenuMngBlo.WatchMenuParam(intMenuSeq, strParamCd);

                    if (dtTmpLink != null)
                    {
                        if (dtTmpLink.Rows.Count > 0)
                        {
                            StringBuilder sbWarning = new StringBuilder();

                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["INFO_CANT_INSERT_DEPTH"]);
                            sbWarning.Append("');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParamWarning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                            return;
                        }
                        else
                        {
                            // KN_USP_MNG_INSERT_MENUPARAM_M00
                            MenuMngBlo.RegistryMenuParam(intMenuSeq, strParamCd, strParamNm);
                        }
                    }
                }

                BindDetailData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 파라미터 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvParamList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                DropDownList ddlParamList = (DropDownList)iTem.FindControl("ddlParams");

                CommCdDdlUtil.MakeCommCdDdl(ddlParamList, dtParamReturn);

                TextBox txtHfParams = (TextBox)iTem.FindControl("txtHfParams");
                TextBox txtParams = (TextBox)iTem.FindControl("txtParams");

                if (!string.IsNullOrEmpty(drView["ParamCd"].ToString()))
                {
                    ddlParamList.SelectedItem.Text = drView["ParamCd"].ToString();
                    txtHfParams.Text = drView["ParamCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ParamNm"].ToString()))
                {

                    txtParams.Text = drView["ParamNm"].ToString();
                }

                ImageButton imgbtnUpdate = (ImageButton)iTem.FindControl("imgbtnUpdate");
                imgbtnUpdate.OnClientClick = "javascript:return fnUpdateCheckValidate('" + txtParams.ClientID + "','" + AlertNm["ALERT_SELECT_MENU"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_MODIFY_ITEM"] + "');";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            }
        }

        #endregion

        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // 상세보기
                /* 그룹권한 정보를 가져오는 부분 시작 */
                DataTable dtReturn = new DataTable();

                // KN_USP_MNG_SELECT_AUTHGRPINFO_S01
                dtReturn = AuthorityMngBlo.SpreadControlAuthGrpInfo(CommValue.MAIN_COMP_CD, Session["LangCd"].ToString());

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        int intTotalAuth = 0;

                        chkReadAuthList.Items.Clear();
                        chkWriteAuthList.Items.Clear();
                        chkModAuthList.Items.Clear();

                        foreach (DataRow dr in dtReturn.Select())
                        {
                            if (!dr["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
                            {
                                chkReadAuthList.Items.Add(new ListItem(dr["MemAuthTyNm"].ToString(), dr["MemAuthTy"].ToString()));
                                chkWriteAuthList.Items.Add(new ListItem(dr["MemAuthTyNm"].ToString(), dr["MemAuthTy"].ToString()));
                                chkModAuthList.Items.Add(new ListItem(dr["MemAuthTyNm"].ToString(), dr["MemAuthTy"].ToString()));
                            }

                            intTotalAuth = intTotalAuth + Int32.Parse(dr["MemAuthTy"].ToString());
                        }

                        txtHfTotalAuthGrpTy.Text = intTotalAuth.ToString();

                        if (dtReturn.Rows.Count < 2)
                        {
                            txtHfReadAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
                            chkReadAuthList.Visible = CommValue.AUTH_VALUE_FALSE;

                            txtHfWriteAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
                            chkWriteAuthList.Visible = CommValue.AUTH_VALUE_FALSE;

                            txtHfModDelAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
                            chkModAuthList.Visible = CommValue.AUTH_VALUE_FALSE;
                        }
                    }
                    else
                    {
                        txtHfReadAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
                        txtHfWriteAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
                        txtHfModDelAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
                    }

                    BindDetailData();
                }
                else
                {
                    // 그룹권한이 없을 경우 목록으로 리턴
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                /* 그룹권한 정보를 가져오는 부분 끝 */
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 상세정보 바인딩
        /// </summary>
        private void BindDetailData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 상세정보 보기
            DataSet dsReturn;

            if (string.IsNullOrEmpty(hfMenuSeq.Value))
            {
                // KN_USP_MNG_SELECT_MENUMNG_S00
                dsReturn = MenuMngBlo.WatchMenuMng(Session["LangCd"].ToString(), CommValue.NUMBER_VALUE_0);
            }
            else
            {
                // KN_USP_MNG_SELECT_MENUMNG_S00
                dsReturn = MenuMngBlo.WatchMenuMng(Session["LangCd"].ToString(), Int32.Parse(hfMenuSeq.Value));
            }

            if (dsReturn != null)
            {
                // 기본정보 보여주기
                if (dsReturn.Tables[0].Rows.Count > 0)
                {
                    DataTable dtBasicInfo = new DataTable();

                    dtBasicInfo = dsReturn.Tables[0];

                    string strDataTitle = string.Empty;
                    string strDataUrl = string.Empty;

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["MenuTxt"].ToString()))
                    {
                        strDataTitle = dtBasicInfo.Rows[0]["MenuTxt"].ToString();
                    }
                    else
                    {
                        strDataTitle = string.Empty;
                    }

                    if (!string.IsNullOrEmpty(dtBasicInfo.Rows[0]["MenuUrl"].ToString()))
                    {
                        strDataUrl = dtBasicInfo.Rows[0]["MenuUrl"].ToString();
                    }
                    else
                    {
                        strDataUrl = string.Empty;
                    }

                    ltDataTitle.Text = strDataTitle + " ( " + strDataUrl + " )";

                    if (dtBasicInfo.Rows[0]["ReadAuth"].ToString().Equals(CommValue.AUTH_VALUE_ENTIRE))
                    {
                        AuthCheckLib.CheckFullData(chkReadAuthList, txtHfReadAuthGrpTy);
                    }
                    else
                    {
                        AuthCheckLib.CheckNoData(chkReadAuthList, Int32.Parse(dtBasicInfo.Rows[0]["ReadAuth"].ToString()), txtHfReadAuthGrpTy);
                    }

                    if (dtBasicInfo.Rows[0]["WriteAuth"].ToString().Equals(CommValue.AUTH_VALUE_ENTIRE))
                    {
                        AuthCheckLib.CheckFullData(chkWriteAuthList, txtHfWriteAuthGrpTy);
                    }
                    else
                    {
                        AuthCheckLib.CheckNoData(chkWriteAuthList, Int32.Parse(dtBasicInfo.Rows[0]["WriteAuth"].ToString()), txtHfWriteAuthGrpTy);
                    }

                    if (dtBasicInfo.Rows[0]["ModDelAuth"].ToString().Equals(CommValue.AUTH_VALUE_ENTIRE))
                    {
                        AuthCheckLib.CheckFullData(chkModAuthList, txtHfModDelAuthGrpTy);
                    }
                    else
                    {
                        AuthCheckLib.CheckNoData(chkModAuthList, Int32.Parse(dtBasicInfo.Rows[0]["ModDelAuth"].ToString()), txtHfModDelAuthGrpTy);
                    }
                }

                dtLinkReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_LINK);
                dtParamReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_PARAM);

                txtLink.Text = string.Empty;
                txtParams.Text = string.Empty;

                // 링크정보
                lvLinkList.DataSource = dsReturn.Tables[1];
                lvLinkList.DataBind();

                // 파라미터 정보
                lvParamList.DataSource = dsReturn.Tables[2];
                lvParamList.DataBind();
            }
        }

        protected void lvLinkList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                BindDetailData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvLinkList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                BindDetailData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvLinkList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (e.CommandName.Equals("UPDATE"))
                {
                    int intMenuSeq = 0;
                    string strOldLinkCd = string.Empty;
                    string strNewLinkCd = string.Empty;
                    string strLinkPageNm = string.Empty;

                    if (!string.IsNullOrEmpty(hfMenuSeq.Value))
                    {
                        DropDownList ddlLinkList = (DropDownList)e.Item.FindControl("ddlLink");
                        TextBox txtHfLinkList = (TextBox)e.Item.FindControl("txtHfLink");
                        TextBox txtLinkPageNm = (TextBox)e.Item.FindControl("txtLink");

                        intMenuSeq = Int32.Parse(hfMenuSeq.Value);
                        strOldLinkCd = txtHfLinkList.Text;
                        strNewLinkCd = ddlLinkList.SelectedItem.Text;
                        strLinkPageNm = txtLinkPageNm.Text;

                        // KN_USP_MNG_INSERT_MENULINK_M00
                        // KN_USP_MNG_DELETE_MENULINK_M01
                        MenuMngBlo.ModifyMenuLink(intMenuSeq, strOldLinkCd, strNewLinkCd, strLinkPageNm);
                    }
                }
                else if (e.CommandName.Equals("DELETE"))
                {
                    int intMenuSeq = 0;
                    string strLinkCd = string.Empty;

                    if (!string.IsNullOrEmpty(hfMenuSeq.Value))
                    {
                        TextBox txtHfLinkList = (TextBox)e.Item.FindControl("txtHfLink");

                        intMenuSeq = Int32.Parse(hfMenuSeq.Value);
                        strLinkCd = txtHfLinkList.Text;

                        // KN_USP_MNG_DELETE_MENULINK_M01
                        MenuMngBlo.RemoveMenuLink(intMenuSeq, strLinkCd);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvParamList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                BindDetailData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvParamList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                BindDetailData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvParamList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("UPDATE"))
                {
                    int intMenuSeq = 0;
                    string strOldParamCd = string.Empty;
                    string strNewParamCd = string.Empty;
                    string strParamNm = string.Empty;

                    if (!string.IsNullOrEmpty(hfMenuSeq.Value))
                    {
                        DropDownList ddlParams = (DropDownList)e.Item.FindControl("ddlParams");
                        TextBox txtHfParams = (TextBox)e.Item.FindControl("txtHfParams");
                        TextBox txtParams = (TextBox)e.Item.FindControl("txtParams");

                        intMenuSeq = Int32.Parse(hfMenuSeq.Value);
                        strOldParamCd = txtHfParams.Text;
                        strNewParamCd = ddlParams.SelectedItem.Text;
                        strParamNm = txtParams.Text;

                        // KN_USP_MNG_INSERT_MENUPARAM_M00
                        // KN_USP_MNG_DELETE_MENUPARAM_M01
                        MenuMngBlo.ModifyMenuParam(intMenuSeq, strOldParamCd, strNewParamCd, strParamNm);
                    }
                }
                else if (e.CommandName.Equals("DELETE"))
                {
                    int intMenuSeq = 0;
                    string strParamCd = string.Empty;

                    if (!string.IsNullOrEmpty(hfMenuSeq.Value))
                    {
                        TextBox txtHfParams = (TextBox)e.Item.FindControl("txtHfParams");

                        intMenuSeq = Int32.Parse(hfMenuSeq.Value);
                        strParamCd = txtHfParams.Text;

                        // KN_USP_MNG_DELETE_MENUPARAM_M01
                        MenuMngBlo.RemoveMenuParam(intMenuSeq, strParamCd);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void chkReadAuthList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuthCheckLib.CheckchkData(chkReadAuthList, txtHfReadAuthGrpTy);
        }

        protected void chkWriteAuthList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuthCheckLib.CheckchkData(chkWriteAuthList, txtHfWriteAuthGrpTy);
        }

        protected void chkModAuthList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuthCheckLib.CheckchkData(chkModAuthList, txtHfModDelAuthGrpTy);
        }

        protected void lnkbtnEntireRead_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckFullData(chkReadAuthList, txtHfReadAuthGrpTy);
        }

        protected void lnkbtnEntireWrite_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckFullData(chkWriteAuthList, txtHfWriteAuthGrpTy);
        }

        protected void lnkbtnEntireMod_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckFullData(chkModAuthList, txtHfModDelAuthGrpTy);
        }

        protected void lnkbtnAuthSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strReadAuth = txtHfReadAuthGrpTy.Text;
                string strWriteAuth = txtHfWriteAuthGrpTy.Text;
                string strModAuth = txtHfModDelAuthGrpTy.Text;
                string strTotalAuth = txtHfTotalAuthGrpTy.Text;

                if (strReadAuth.Equals(strTotalAuth))
                {
                    strReadAuth = CommValue.AUTH_VALUE_ENTIRE;
                }

                if (strWriteAuth.Equals(strTotalAuth))
                {
                    strWriteAuth = CommValue.AUTH_VALUE_ENTIRE;
                }

                if (strModAuth.Equals(strTotalAuth))
                {
                    strModAuth = CommValue.AUTH_VALUE_ENTIRE;
                }

                // KN_USP_MNG_UPDATE_MENUINFO_M01
                MenuMngBlo.ModifyMenuInfo(Int32.Parse(hfMenuSeq.Value), strReadAuth.PadLeft(8, '0'), strWriteAuth.PadLeft(8, '0'), strModAuth.PadLeft(8, '0'), Session["CompCd"].ToString(), Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());

                BindDetailData();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RegistItem", "javascript:alert('" + AlertNm["INFO_REGIST_ITEM"] + "')", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}