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

using KN.Board.Biz;

namespace KN.Web.Config.Board
{
    public partial class BoardMngList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
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
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_FILEADDONCOUNTINFO_M00
            dtReturn = BoardInfoBlo.SpreadBoardMngInfo(Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                lvBoarMngdList.DataSource = dtReturn;
                lvBoarMngdList.DataBind();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBoarMngdList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvBoarMngdList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvBoarMngdList.FindControl("ltTitle")).Text = TextNm["BOARDNM"];
            ((Literal)lvBoarMngdList.FindControl("ltFileCnt")).Text = TextNm["FILEADDONCNT"];
            ((Literal)lvBoarMngdList.FindControl("ltReplyYn")).Text = TextNm["REPLYYN"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBoarMngdList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvBoarMngdList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    Literal ltSeqList = (Literal)iTem.FindControl("ltSeqList");
                    ltSeqList.Text = drView["Seq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MemuNm"].ToString()))
                {
                    Literal ltTitleList = (Literal)iTem.FindControl("ltTitleList");
                    ltTitleList.Text = TextLib.StringDecoder(drView["MemuNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["FileAddonCnt"].ToString()))
                {
                    DropDownList ddlFileCntList = (DropDownList)iTem.FindControl("ddlFileCntList");
                    ddlFileCntList.Items.Clear();

                    for (int intTmpI = 1; intTmpI <= 3; intTmpI++)
                    {
                        ddlFileCntList.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
                    }

                    ddlFileCntList.SelectedValue = drView["FileAddonCnt"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MenuSeq"].ToString()))
                {
                    TextBox txtHfMenuSeqList = (TextBox)iTem.FindControl("txtHfMenuSeqList");
                    txtHfMenuSeqList.Text = drView["MenuSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ReplyYn"].ToString()))
                {
                    CheckBox chkReplyYnList = (CheckBox)iTem.FindControl("chkReplyYnList");
                    string strReplyYn = drView["ReplyYn"].ToString();

                    if (strReplyYn.Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        chkReplyYnList.Checked = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        chkReplyYnList.Checked = CommValue.AUTH_VALUE_FALSE;
                    }
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

            }
        }

        protected void lvBoarMngdList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                DropDownList ddlFileCntList = (DropDownList)lvBoarMngdList.Items[e.ItemIndex].FindControl("ddlFileCntList");
                TextBox txtHfMenuSeqList = (TextBox)lvBoarMngdList.Items[e.ItemIndex].FindControl("txtHfMenuSeqList");
                CheckBox chkReplyYnList = (CheckBox)lvBoarMngdList.Items[e.ItemIndex].FindControl("chkReplyYnList");

                string strReplyYn = "";

                if (chkReplyYnList.Checked)
                {
                    strReplyYn = CommValue.CHOICE_VALUE_YES;
                }
                else
                {
                    strReplyYn = CommValue.CHOICE_VALUE_NO;
                }

                // KN_USP_MNG_UPDATE_FILEADDONCOUNTINFO_M00
                BoardInfoBlo.ModifyBoardMngInfo(Int32.Parse(txtHfMenuSeqList.Text), Int32.Parse(ddlFileCntList.Text), strReplyYn);

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}