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

namespace KN.Web.Common.Popup
{
    public partial class PopupChestNutMemberList : BasePage
    {
        public const string PARAM_DATA1 = "Params1";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControl();

                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            // 인자값이 제대로 넘어오지 않을 경우 리턴
            if (Request.Params[PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA1].ToString()))
                {
                    hfParams1.Value = Request.Params[PARAM_DATA1].ToString();
                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    isReturn = CommValue.AUTH_VALUE_FALSE;
                }
            }
            else
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturn;
        }

        protected void InitControl()
        {
            ltSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.Text = TextNm["SEARCH"];

            ltTopSeq.Text = TextNm["SEQ"];
            ltTopMemNm.Text = TextNm["NAME"];
            ltTopUserId.Text = TextNm["USERID"];
            ltTopTelNo.Text = TextNm["TEL"];
            ltTopCellNo.Text = TextNm["MOBILENO"];
            //ltTopEnterDt.Text = TextNm["MOBILENO"];

            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlSearch, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SEARCH_CONDITION, CommValue.SEARCH_COND_VALUE_FINDMEMBER);
        }

        protected void LoadData()
        {
            string strSearchTy = string.Empty;
            string strSearchWord = string.Empty;

            string strMemNo = string.Empty;
            string strMemNm = string.Empty;
            string strAddr = string.Empty;
            string strTel = string.Empty;
            string strCell = string.Empty;

            strSearchTy = ddlSearch.SelectedValue;
            strSearchWord = txtSearch.Text;

            if (strSearchTy.Equals(CommValue.MEMBER_SEARCH_VALUE_NO))
            {
                strMemNo = strSearchWord;
            }
            else if (strSearchTy.Equals(CommValue.MEMBER_SEARCH_VALUE_NM))
            {
                strMemNm = strSearchWord;
            }
            else if (strSearchTy.Equals(CommValue.MEMBER_SEARCH_VALUE_ADDR))
            {
                strAddr = strSearchWord;
            }
            else if (strSearchTy.Equals(CommValue.MEMBER_SEARCH_VALUE_TEL))
            {
                strTel = strSearchWord;
            }
            else if (strSearchTy.Equals(CommValue.MEMBER_SEARCH_VALUE_CELL))
            {
                strCell = strSearchWord;
            }

            // KN_USP_COMM_SELECT_MEMINFO_S08
            DataTable dtReturn = MemberMngBlo.SpreadKsysChestNutMemInfo(strMemNo, strMemNm, strAddr, strTel, strCell);

            if (dtReturn != null)
            {
                lvMemList.DataSource = dtReturn;
                lvMemList.DataBind();
            }
            else
            {
                // 게시판 조회시 Null값 반환시 목록으로 리턴
                StringBuilder sbWarning = new StringBuilder();

                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                sbWarning.Append("');");
                sbWarning.Append("self.close();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
        }

        protected void lvMemList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvMemList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltSeq")).Text = drView["Seq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MemEngNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltMemNm")).Text = drView["MemEngNm"].ToString();
                    ((HiddenField)iTem.FindControl("hfMemNo")).Value = drView["MemNo"].ToString();
                    ((HiddenField)iTem.FindControl("hfMemEngNm")).Value = drView["MemEngNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UserId"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltUserId")).Text = drView["UserId"].ToString();
                    ((HiddenField)iTem.FindControl("hfUserId")).Value = drView["UserId"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TelNo"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltTelNo")).Text = drView["TelFrontNo"].ToString() + "-" + drView["TelMidNo"].ToString() + "-" + drView["TelRearNo"].ToString();
                    ((HiddenField)iTem.FindControl("hfTelFrontNo")).Value = drView["TelFrontNo"].ToString();
                    ((HiddenField)iTem.FindControl("hfTelMidNo")).Value = drView["TelMidNo"].ToString();
                    ((HiddenField)iTem.FindControl("hfTelRearNo")).Value = drView["TelRearNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CellNo"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltCellNo")).Text = drView["CellFrontNo"].ToString() + "-" + drView["CellMidNo"].ToString() + "-" + drView["CellRearNo"].ToString();
                    ((HiddenField)iTem.FindControl("hfCellFrontNo")).Value = drView["CellFrontNo"].ToString();
                    ((HiddenField)iTem.FindControl("hfCellMidNo")).Value = drView["CellMidNo"].ToString();
                    ((HiddenField)iTem.FindControl("hfCellRearNo")).Value = drView["CellRearNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["EntDt"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltEnterDt")).Text = TextLib.MakeDateEightDigit(drView["EntDt"].ToString());
                    ((HiddenField)iTem.FindControl("hfEntDt")).Value = drView["EntDt"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["EntireAddr"].ToString()))
                {
                    ((HiddenField)iTem.FindControl("hfAddr")).Value = drView["Addr"].ToString();
                    ((HiddenField)iTem.FindControl("hfDetAddr")).Value = drView["DetAddr"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["EmailID"].ToString()))
                {
                    ((HiddenField)iTem.FindControl("hfEmailID")).Value = drView["EmailID"].ToString();
                    ((HiddenField)iTem.FindControl("hfEmailServer")).Value = drView["EmailServer"].ToString();
                }
            }
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}