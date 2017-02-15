using System;
using System.Data;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class BankList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
                    InitControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            ltTopCompNm.Text = TextNm["COMPNM"];
            ltTopAccountCd.Text = TextNm["SEQ"];
            ltTopBankNm.Text = TextNm["BANK"];
            ltTopAccountNo.Text = TextNm["ACCOUNT"];
            ltTopAccCd.Text = TextNm["ACCOUNTINGCD"];
            hfParamData.Value = hfParamData.ClientID.Replace("hfParamData", "");

            // 회사코드 조회
            MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlCompNm, Session["CompCd"].ToString());
        }

        protected void LoadData()
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S01
            DataTable dtReturn = AccountMngBlo.SpreadBankBookInfo(ddlCompNm.SelectedValue);

            if (dtReturn != null)
            {
                lvBankList.DataSource = dtReturn;
                lvBankList.DataBind();
            }
        }

        protected void lvBankList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // ListView 데이터 바인딩 처리
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                Literal ltCompNm = (Literal)iTem.FindControl("ltCompNm");

                if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
                {
                    ltCompNm.Text = drView["CompNm"].ToString();
                }

                TextBox txtHfCompCd = (TextBox)iTem.FindControl("txtHfCompCd");

                if (!string.IsNullOrEmpty(drView["CompNo"].ToString()))
                {
                    txtHfCompCd.Text = drView["CompNo"].ToString();
                }

                Literal ltAccountCd = (Literal)iTem.FindControl("ltAccountCd");
                TextBox txtHfAccountCd = (TextBox)iTem.FindControl("txtHfAccountCd");

                if (!string.IsNullOrEmpty(drView["AccountCd"].ToString()))
                {
                    ltAccountCd.Text = drView["AccountCd"].ToString();
                    txtHfAccountCd.Text = drView["AccountCd"].ToString();
                }

                TextBox txtBankNm = (TextBox)iTem.FindControl("txtBankNm");

                if (!string.IsNullOrEmpty(drView["BankNm"].ToString()))
                {
                    txtBankNm.Text = drView["BankNm"].ToString();
                }

                TextBox txtAccountNo = (TextBox)iTem.FindControl("txtAccountNo");

                if (!string.IsNullOrEmpty(drView["AccountNo"].ToString()))
                {
                    txtAccountNo.Text = drView["AccountNo"].ToString();
                }

                TextBox txtAccCd = (TextBox)iTem.FindControl("txtAccCd");

                if (!string.IsNullOrEmpty(drView["AccCd"].ToString()))
                {
                    txtAccCd.Text = drView["AccCd"].ToString();
                }

                ImageButton imgbtnUpdate = (ImageButton)iTem.FindControl("imgbtnUpdate");
                imgbtnUpdate.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "')";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            }
        }

        protected void lvBankList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            TextBox txtHfCompCd = (TextBox)lvBankList.Items[e.ItemIndex].FindControl("txtHfCompCd");
            TextBox txtHfAccountCd = (TextBox)lvBankList.Items[e.ItemIndex].FindControl("txtHfAccountCd");
            TextBox txtBankNm = (TextBox)lvBankList.Items[e.ItemIndex].FindControl("txtBankNm");
            TextBox txtAccountNo = (TextBox)lvBankList.Items[e.ItemIndex].FindControl("txtAccountNo");
            TextBox txtAccCd = (TextBox)lvBankList.Items[e.ItemIndex].FindControl("txtAccCd");

            // KN_USP_MNG_UPDATE_ACCOUNTINFO_M00
            AccountMngBlo.ModifyAccountInfo(txtHfCompCd.Text, txtHfAccountCd.Text, txtBankNm.Text, txtAccountNo.Text, txtAccCd.Text);

            LoadData();
        }

        protected void lvBankList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            TextBox txtHfCompCd = (TextBox)lvBankList.Items[e.ItemIndex].FindControl("txtHfCompCd");
            TextBox txtHfAccountCd = (TextBox)lvBankList.Items[e.ItemIndex].FindControl("txtHfAccountCd");

            // KN_USP_MNG_DELETE_ACCOUNTINFO_M00
            AccountMngBlo.RemoveAccountInfo(txtHfCompCd.Text, txtHfAccountCd.Text);

            LoadData();
        }

        protected void lvBankList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvBankList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
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

        protected void imgbtnInsert_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                // KN_USP_MNG_INSERT_ACCOUNTINFO_M00
                AccountMngBlo.RegistryAccountInfo(ddlCompNm.SelectedValue, txtBankNm.Text, txtAccountNo.Text, txtAccCd.Text);

                txtBankNm.Text = string.Empty;
                txtAccountNo.Text = string.Empty;
                txtAccCd.Text = string.Empty;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlCompNm_SelectedIndexChanged(object sender, EventArgs e)
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
