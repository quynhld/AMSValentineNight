using System;
using System.Data;
using System.EnterpriseServices;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Board.Biz;
using KN.Config.Biz;
using KN.Config.Ent;

namespace KN.Web.Config.Menu
{
    [Transaction(TransactionOption.Required)]
    public partial class MenuList : BasePage
    {
        MenuDs.MenuInfo miMenu = new MenuDs.MenuInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
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
            ltSeq.Text = TextNm["SEQ"];
            ltDepth.Text = TextNm["DEPTH"];
            ltTitle.Text = TextNm["TITLE"];
            ltUrl.Text = TextNm["URL"];
            ltHiddenYn.Text = TextNm["HIDDEN"];
            ltBoardYn.Text = TextNm["BOARD"];

            txtDepth1.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtDepth2.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtDepth3.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtDepth4.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            imgbtnSearch.OnClientClick = "javascript:return fnChangePopup('" + Master.PARAM_DATA1 + "', '" + txtTitle.ClientID + "');";
            imgbtnInsert.OnClientClick = "javascript:return fnSubmitCheck('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["ALERT_INSERT_TITLE"] + "','" + AlertNm["ALERT_INSERT_LINK"] + "');";
        }

        protected void ResetControls()
        {
            txtDepth1.Text = string.Empty;
            txtDepth2.Text = string.Empty;
            txtDepth3.Text = string.Empty;
            txtDepth4.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtUrl.Text = string.Empty;
            ddlHiddenYn.SelectedValue = CommValue.CHOICE_VALUE_YES;
            ddlBoardYn.SelectedValue = CommValue.CHOICE_VALUE_NO;

            txtUrl.ReadOnly = CommValue.AUTH_VALUE_TRUE;
        }

        protected void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_MENUINFO_S00
            dtReturn = MenuMngBlo.SpreadMenuInfo();

            if (dtReturn != null)
            {
                lvMnglist.DataSource = dtReturn;
                lvMnglist.DataBind();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMnglist_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltDepth")).Text = TextNm["DEPTH"];
                    ((Literal)e.Item.FindControl("ltTitle")).Text = TextNm["TITLE"];
                    ((Literal)e.Item.FindControl("ltUrl")).Text = TextNm["URL"];
                    ((Literal)e.Item.FindControl("ltHiddenYn")).Text = TextNm["HIDDEN"];
                    ((Literal)e.Item.FindControl("ltBoardYn")).Text = TextNm["BOARD"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMnglist_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                /* 각 컨트롤별 데이터 바인딩 시작 */
                if (!string.IsNullOrEmpty(drView["MenuSeq"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtSeq")).Text = drView["MenuSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["Depth1"].ToString()))
                {
                    TextBox txtTmpDepth1 = (TextBox)iTem.FindControl("txtDepth1");
                    txtTmpDepth1.Text = drView["Depth1"].ToString();
                    txtTmpDepth1.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["Depth2"].ToString()))
                {
                    TextBox txtTmpDepth2 = (TextBox)iTem.FindControl("txtDepth2");
                    txtTmpDepth2.Text = drView["Depth2"].ToString();
                    txtTmpDepth2.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["Depth3"].ToString()))
                {
                    TextBox txtTmpDepth3 = (TextBox)iTem.FindControl("txtDepth3");
                    txtTmpDepth3.Text = drView["Depth3"].ToString();
                    txtTmpDepth3.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["Depth4"].ToString()))
                {
                    TextBox txtTmpDepth4 = (TextBox)iTem.FindControl("txtDepth4");
                    txtTmpDepth4.Text = drView["Depth4"].ToString();
                    txtTmpDepth4.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }

                if (!string.IsNullOrEmpty(drView["MenuTitle"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtTitle")).Text = TextLib.StringDecoder(drView["MenuTitle"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["MenuUrl"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtUrl")).Text = TextLib.StringDecoder(drView["MenuUrl"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["HiddenYn"].ToString()))
                {
                    ((DropDownList)iTem.FindControl("ddlHiddenYn")).SelectedValue = drView["HiddenYn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["BoardYn"].ToString()))
                {
                    ((DropDownList)iTem.FindControl("ddlBoardYn")).SelectedValue = drView["BoardYn"].ToString();
                }
                /* 각 컨트롤별 데이터 바인딩 끝 */

                /* 각 라인별 수정/삭제 버튼 세팅 시작 */
                ImageButton imgbtnUpdate = (ImageButton)iTem.FindControl("imgbtnUpdate");
                imgbtnUpdate.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "')";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
                /* 각 라인별 수정/삭제 버튼 세팅 끝 */

                ImageButton imgbtnTmpSelect = (ImageButton)iTem.FindControl("imgbtnSearch");
                imgbtnTmpSelect.OnClientClick = "javascript:return fnChangePopup('" + Master.PARAM_DATA1 + "', '" + ((TextBox)iTem.FindControl("txtTitle")).ClientID + "');";
            }
        }

        #region 입력단 이벤트 처리

        /// <summary>
        /// Link 여부 선택 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlHiddenYn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlHiddenYn.SelectedValue.Equals(CommValue.CHOICE_VALUE_YES))
            {
                txtUrl.Text = string.Empty;
                txtUrl.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                txtUrl.ReadOnly = CommValue.AUTH_VALUE_FALSE;
            }
        }

        /// <summary>
        /// 등록버튼 클릭 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnInsert_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                bool isBreak = CommValue.AUTH_VALUE_FALSE;

                miMenu.Depth1 = Int32.Parse(txtDepth1.Text);
                miMenu.Depth2 = Int32.Parse(txtDepth2.Text);
                miMenu.Depth3 = Int32.Parse(txtDepth3.Text);
                miMenu.Depth4 = Int32.Parse(txtDepth4.Text);
                miMenu.MenuTitle = txtTitle.Text;
                miMenu.MenuUrl = txtUrl.Text;
                miMenu.HiddenYn = ddlHiddenYn.SelectedItem.Text;
                miMenu.ReadAuth = CommValue.AUTH_VALUE_ENTIRE;
                miMenu.WriteAuth = CommValue.AUTH_VALUE_ENTIRE;
                miMenu.ModDelAuth = CommValue.AUTH_VALUE_ENTIRE;
                miMenu.BoardYn = ddlBoardYn.SelectedItem.Text;
                miMenu.BoardTy = string.Empty;
                miMenu.BoardCd = string.Empty;
                miMenu.InsCompNo = Session["CompCd"].ToString();
                miMenu.InsMemNo = Session["MemNo"].ToString();
                miMenu.InsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                /* 중복 라인 처리 시작 */
                if (lvMnglist.Items.Count > 0)
                {
                    for (int intTmpI = 0; intTmpI < lvMnglist.Items.Count; intTmpI++)
                    {
                        string strDepth1 = ((TextBox)lvMnglist.Items[intTmpI].FindControl("txtDepth1")).Text;
                        string strDepth2 = ((TextBox)lvMnglist.Items[intTmpI].FindControl("txtDepth2")).Text;
                        string strDepth3 = ((TextBox)lvMnglist.Items[intTmpI].FindControl("txtDepth3")).Text;
                        string strDepth4 = ((TextBox)lvMnglist.Items[intTmpI].FindControl("txtDepth4")).Text;

                        if (!string.IsNullOrEmpty(txtDepth1.Text))
                        {
                            if (strDepth1.Equals(txtDepth1.Text) &&
                                strDepth2.Equals(txtDepth2.Text) &&
                                strDepth3.Equals(txtDepth3.Text) &&
                                strDepth4.Equals(txtDepth4.Text))
                            {
                                hfExgistLine.Value = (intTmpI + 1).ToString() + " " + TextNm["LINE"];
                                hfExgistMsg.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];
                                isBreak = CommValue.AUTH_VALUE_TRUE;
                                break;
                            }
                        }
                    }
                }
                /* 중복 라인 처리 끝 */

                if (!isBreak)
                {
                    // KN_USP_MNG_INSERT_MENUINFO_M00
                    MenuMngBlo.RegistryMenuInfo(miMenu);

                    // Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    ResetControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        #endregion

        #region 각 라인별 이벤트 처리

        /// <summary>
        /// 각 라인별 삭제버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMnglist_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtTmpSeq = (TextBox)lvMnglist.Items[e.ItemIndex].FindControl("txtSeq");

                // KN_USP_MNG_DELETE_MENULINK_M00
                // KN_USP_MNG_DELETE_MENUPARAM_M00
                // KN_USP_MNG_DELETE_MENUINFO_M00
                MenuMngBlo.RemoveMenuMng(Int32.Parse(txtTmpSeq.Text));

                //Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                ResetControls();

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 각 라인별 수정버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMnglist_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                bool isExist = CommValue.AUTH_VALUE_FALSE;

                TextBox txtTmpDepth1 = (TextBox)lvMnglist.Items[e.ItemIndex].FindControl("txtDepth1");
                TextBox txtTmpDepth2 = (TextBox)lvMnglist.Items[e.ItemIndex].FindControl("txtDepth2");
                TextBox txtTmpDepth3 = (TextBox)lvMnglist.Items[e.ItemIndex].FindControl("txtDepth3");
                TextBox txtTmpDepth4 = (TextBox)lvMnglist.Items[e.ItemIndex].FindControl("txtDepth4");

                // 누락된 Depth정보 확인
                if ((string.IsNullOrEmpty(txtTmpDepth1.Text)) ||
                    (string.IsNullOrEmpty(txtTmpDepth2.Text)) ||
                    (string.IsNullOrEmpty(txtTmpDepth3.Text)) ||
                    (string.IsNullOrEmpty(txtTmpDepth4.Text)))
                {
                    hfExgistMsg.Value = AlertNm["ALERT_INSERT_BLANK"];
                    return;
                }

                // 기존 데이터와 중복데이터가 존재하는지 체크
                for (int intTmpI = 0; intTmpI < lvMnglist.Items.Count; intTmpI++)
                {
                    if (intTmpI != e.ItemIndex)
                    {
                        TextBox txtTmpCompareDepth1 = (TextBox)lvMnglist.Items[intTmpI].FindControl("txtDepth1");
                        TextBox txtTmpCompareDepth2 = (TextBox)lvMnglist.Items[intTmpI].FindControl("txtDepth2");
                        TextBox txtTmpCompareDepth3 = (TextBox)lvMnglist.Items[intTmpI].FindControl("txtDepth3");
                        TextBox txtTmpCompareDepth4 = (TextBox)lvMnglist.Items[intTmpI].FindControl("txtDepth4");

                        if ((txtTmpDepth1.Text.Equals(txtTmpCompareDepth1.Text)) &&
                           (txtTmpDepth2.Text.Equals(txtTmpCompareDepth2.Text)) &&
                           (txtTmpDepth3.Text.Equals(txtTmpCompareDepth3.Text)) &&
                           (txtTmpDepth4.Text.Equals(txtTmpCompareDepth4.Text)))
                        {
                            // 중복데이터가 존재할 경우 에러메세지 세팅
                            isExist = CommValue.AUTH_VALUE_TRUE;
                            hfExgistLine.Value = (intTmpI + 1).ToString();
                            hfExgistMsg.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];
                            break;
                        }
                    }
                }

                // 중복데이터가 존재하지 않을 경우 수정처리
                if (!isExist)
                {
                    DropDownList ddlTmpHiddenYn = (DropDownList)lvMnglist.Items[e.ItemIndex].FindControl("ddlHiddenYn");
                    DropDownList ddlTmpBoardYn = (DropDownList)lvMnglist.Items[e.ItemIndex].FindControl("ddlBoardYn");

                    TextBox txtTmpSeq = (TextBox)lvMnglist.Items[e.ItemIndex].FindControl("txtSeq");
                    TextBox txtTmpTitle = (TextBox)lvMnglist.Items[e.ItemIndex].FindControl("txtTitle");
                    TextBox txtTmpUrl = (TextBox)lvMnglist.Items[e.ItemIndex].FindControl("txtUrl");

                    /* 수정할 데이터를 객체에 담는 부부분 시작 */
                    miMenu.MenuSeq = Int32.Parse(txtTmpSeq.Text);
                    miMenu.Depth1 = Int32.Parse(txtTmpDepth1.Text);
                    miMenu.Depth2 = Int32.Parse(txtTmpDepth2.Text);
                    miMenu.Depth3 = Int32.Parse(txtTmpDepth3.Text);
                    miMenu.Depth4 = Int32.Parse(txtTmpDepth4.Text);
                    miMenu.MenuTitle = txtTmpTitle.Text;
                    miMenu.MenuUrl = txtTmpUrl.Text;
                    miMenu.HiddenYn = ddlTmpHiddenYn.SelectedItem.Text;
                    miMenu.ReadAuth = CommValue.AUTH_VALUE_ENTIRE;
                    miMenu.WriteAuth = CommValue.AUTH_VALUE_ENTIRE;
                    miMenu.ModDelAuth = CommValue.AUTH_VALUE_ENTIRE;
                    miMenu.BoardYn = ddlTmpBoardYn.SelectedItem.Text;
                    miMenu.BoardTy = string.Empty;
                    miMenu.BoardCd = string.Empty;
                    miMenu.ModCompNo = Session["CompCd"].ToString();
                    miMenu.ModMemNo = Session["MemNo"].ToString();
                    miMenu.ModMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    /* 수정할 데이터를 객체에 담는 부부분 끝 */

                    // KN_USP_MNG_UPDATE_MENUINFO_M00
                    MenuMngBlo.ModifyMenuInfo(miMenu);

                    if (miMenu.BoardYn.Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        // KN_USP_MNG_INSERT_FILEADDONCOUNTINFO_M00
                        BoardInfoBlo.RegistryFileAddonCntInfo(miMenu.MenuSeq);
                    }
                    else
                    {
                        // KN_USP_MNG_DELETE_FILEADDONCOUNTINFO_M00
                        BoardInfoBlo.RemoveFileAddonCntInfo(miMenu.MenuSeq);
                    }

                    // Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    ResetControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnTitle_Click(object sender, ImageClickEventArgs e)
        {
            // 팝업의 불법적인 접근을 제한하기 위한 세션 생성
            Session["FindTitleYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
        }

        #endregion
    }
}