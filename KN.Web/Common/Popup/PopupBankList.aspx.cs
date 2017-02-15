using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Config.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupBankList : BasePage
    {
        public const string PARAM_DATA1 = "Params1";
        public const string PARAM_DATA2 = "Params2";

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

                    if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA2].ToString()))
                    {
                        txtParams.Text = Request.Params[PARAM_DATA2].ToString();
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
            }
            else
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturn;
        }

        protected void InitControl()
        {
            ltTopBankNm.Text = PopupNm["BANK"];
            ltTopAccountNo.Text = PopupNm["ACCOUNT"];
            ltTopAccCd.Text = PopupNm["ACCOUNTINGCD"];
        }

        protected void LoadData()
        {
            DataTable dtReturn = new DataTable();

            if (txtParams.Text.Equals(CommValue.MAIN_COMP_CD))
            {
                // KN_USP_MNG_SELECT_ACCOUNTINFO_S02
                dtReturn = AccountMngBlo.SpreadChestNutAccountInfo();
            }
            else
            {
                // KN_USP_MNG_SELECT_ACCOUNTINFO_S03
                dtReturn = AccountMngBlo.SpreadKNVinaAccountInfo();
            }

            if (dtReturn != null)
            {
                lvBankList.DataSource = dtReturn;
                lvBankList.DataBind();
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

        protected void lvBankList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvBankList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                Literal ltBankNm = (Literal)iTem.FindControl("ltBankNm");

                if (!string.IsNullOrEmpty(drView["BankNm"].ToString()))
                {
                    ltBankNm.Text = drView["BankNm"].ToString();
                }

                Literal ltAccountNo = (Literal)iTem.FindControl("ltAccountNo");

                if (!string.IsNullOrEmpty(drView["AccountNo"].ToString()))
                {
                    ltAccountNo.Text = drView["AccountNo"].ToString();
                }

                Literal ltAccCd = (Literal)iTem.FindControl("ltAccCd");

                if (!string.IsNullOrEmpty(drView["AccCd"].ToString()))
                {
                    ltAccCd.Text = drView["AccCd"].ToString();
                }
            }
        }
    }
}