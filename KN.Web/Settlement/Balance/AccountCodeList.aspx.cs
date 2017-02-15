using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class AccountCodeList : BasePage
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
            ltTopCompCd.Text = TextNm["COMPNM"];
            ltTopRent.Text = TextNm["RENT"];
            //ltTopSettleCd.Text = TextNm["ITEM"];
            //ltTopItemCd.Text = TextNm["ITEM"];
            //ltTopAccountCd.Text = TextNm["SEQ"];
           // ltTopAccCd.Text = TextNm["ACCOUNTINGCD"];

            hfParamData.Value = hfParamData.ClientID.Replace("hfParamData", "");

            // 회사코드 조회
            MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlCompNm, Session["CompCd"].ToString());

            // 섹션코드 조회
            LoadRentDdl(ddlRentNm, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
            

            ddlRentNm.SelectedValue = CommValue.RENTAL_VALUE_APT;
            // 회계계정 조회
           // CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlSettleNm, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_ACCOUNTCODE);

            // 지불방법 조회
           // CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlItemNm, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);
            //CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlFeeType, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RECEIT);
            MakePaymentTypeDdl();
            MakeFeeTypeDdl();
            lnkbtnWrite.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
        }

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTA) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTB) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                {
                    ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        protected void LoadData()
        {
            // KN_USP_MNG_SELECT_ACCOUNTCDINFO_S00
            var dtReturn = BalanceMngBlo.SpreadBankAccountList(Session["LangCd"].ToString(), ddlCompNm.SelectedValue, ddlRentNm.SelectedValue, ddlFeeType.SelectedValue,
                                                                   ddlPaymentType.SelectedValue);

            if (dtReturn == null) return;
            lvAccountCdList.DataSource = dtReturn;
            lvAccountCdList.DataBind();
        }

        protected void lvAccountCdList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // ListView 데이터 바인딩 처리
            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            var ltCompNm = (Literal)iTem.FindControl("ltCompNm");

            if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
            {
                ltCompNm.Text = drView["CompNm"].ToString();
            }

            var txtHfCompCd = (TextBox)iTem.FindControl("txtHfCompCd");

            if (!string.IsNullOrEmpty(drView["CompCd"].ToString()))
            {
                txtHfCompCd.Text = drView["CompCd"].ToString();
            }

            var ltRentNm = (Literal)iTem.FindControl("ltRentNm");

            if (!string.IsNullOrEmpty(drView["RentNm"].ToString()))
            {
                ltRentNm.Text = drView["RentNm"].ToString();
            }

            var txtHfRentCd = (TextBox)iTem.FindControl("txtHfRentCd");

            if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
            {
                txtHfRentCd.Text = drView["RentCd"].ToString();
            }

            var ltFeeName = (Literal)iTem.FindControl("ltFeeName");

            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                ltFeeName.Text = drView["FeeNm"].ToString();
            }

            var txtHfFeeCd = (TextBox)iTem.FindControl("txtHfFeeCd");

            if (!string.IsNullOrEmpty(drView["FeeTy"].ToString()))
            {
                txtHfFeeCd.Text = drView["FeeTy"].ToString();
            }

            var ltPayMentType = (Literal)iTem.FindControl("ltPayMentType");

            if (!string.IsNullOrEmpty(drView["PaymentTy"].ToString()))
            {
                ltPayMentType.Text = drView["PaymentTy"].ToString();
            }

            var txtHfBankSeq = (TextBox)iTem.FindControl("txtHfBankSeq");

            if (!string.IsNullOrEmpty(drView["BankSeq"].ToString()))
            {
                txtHfBankSeq.Text = drView["BankSeq"].ToString();
            }

            var txtBankName = (TextBox)iTem.FindControl("txtBankName");

            if (!string.IsNullOrEmpty(drView["BankNm"].ToString()))
            {
                txtBankName.Text = drView["BankNm"].ToString();
            }

            var txtBankCode = (TextBox)iTem.FindControl("txtBankCode");

            if (!string.IsNullOrEmpty(drView["BankAccount"].ToString()))
            {
                txtBankCode.Text = drView["BankAccount"].ToString();
            }

            var chkUsedYn = (CheckBox)iTem.FindControl("chkUsedYN");

            if (!string.IsNullOrEmpty(drView["UsedYN"].ToString()))
            {
                (chkUsedYn).Checked = drView["UsedYN"].ToString() == "Y";
            }


            var imgbtnUpdate = (ImageButton)iTem.FindControl("imgbtnUpdate");
            imgbtnUpdate.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "')";

            var imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
            imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
        }

        protected void lvAccountCdList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                TextBox txtHfCompCd = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtHfCompCd");
                TextBox txtHfRentCd = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtHfRentCd");
                TextBox txtHfFeeTy = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtHfFeeCd");
                Literal ltPayment = (Literal)lvAccountCdList.Items[e.ItemIndex].FindControl("ltPayMentType");
                TextBox txtbankSeq = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtHfBankSeq");
                TextBox txtbankName = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtBankName");
                TextBox txtBankCode = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtBankCode");
                var UsedYN = ((CheckBox)lvAccountCdList.Items[e.ItemIndex].FindControl("chkUsedYN")).Checked ? "Y" : "N";


                // KN_USP_MNG_UPDATE_ACCOUNTCDINFO_M00
                BalanceMngBlo.RegistryAccountBankInfo(txtHfCompCd.Text, txtHfRentCd.Text, txtHfFeeTy.Text, ltPayment.Text, txtbankName.Text, txtBankCode.Text, Int32.Parse(txtbankSeq.Text), UsedYN, Session["MemNo"].ToString(), "");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:SaveSuccess()", CommValue.AUTH_VALUE_TRUE);

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvAccountCdList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                var txtHfCompCd = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtHfCompCd");
                var txtHfRentCd = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtHfRentCd");
                var txtHfFeeTy = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtHfFeeCd");
                var ltPayment = (Literal)lvAccountCdList.Items[e.ItemIndex].FindControl("ltPayMentType");
                var txtbankSeq = (TextBox)lvAccountCdList.Items[e.ItemIndex].FindControl("txtHfBankSeq");
                // KN_USP_MNG_DELETE_ACCOUNTCDINFO_M00
                BalanceMngBlo.RemoveBankAccountInfo(txtHfCompCd.Text, txtHfRentCd.Text, txtHfFeeTy.Text, ltPayment.Text, Int32.Parse(txtbankSeq.Text));
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:fnDeleted()", CommValue.AUTH_VALUE_TRUE);
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvAccountCdList_ItemCommand(object sender, ListViewCommandEventArgs e)
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

        protected void lvAccountCdList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void ddlCompNm_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlRentNm.SelectedValue = ddlCompNm.SelectedValue.Equals(CommValue.MAIN_COMP_CD) ? CommValue.RENTAL_VALUE_APT : CommValue.RENTAL_VALUE_OFFICE;
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlRentNm_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                // KN_USP_MNG_INSERT_ACCOUNTCDINFO_S00
                 BalanceMngBlo.RegistryAccountBankInfo(ddlCompNm.SelectedValue, ddlRentNm.SelectedValue, ddlFeeType.SelectedValue, ddlPaymentType.SelectedValue, txtBankName.Text, txtBankAccount.Text, 0, "Y", Session["MemNo"].ToString(),"");

                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:SaveSuccess()", CommValue.AUTH_VALUE_TRUE);

                LoadData();
                txtBankAccount.Text = "";
                txtBankName.Text = "";
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void MakePaymentTypeDdl()
        {
            ddlPaymentType.Items.Clear();
            ddlPaymentType.Items.Add(new ListItem("Payment Type", ""));
            ddlPaymentType.Items.Add(new ListItem("VND", "VND"));
            ddlPaymentType.Items.Add(new ListItem("USD", "USD"));
        }
        protected void MakeFeeTypeDdl()
        {
            ddlFeeType.Items.Clear();
            ddlFeeType.Items.Add(new ListItem("Fee Type", ""));
            ddlFeeType.Items.Add(new ListItem("Management Fee", "0001"));
            ddlFeeType.Items.Add(new ListItem("Rental Fee", "0002"));
            ddlFeeType.Items.Add(new ListItem("Utility", "0011"));
            ddlFeeType.Items.Add(new ListItem("Parking Fee", "0004"));
            ddlFeeType.Items.Add(new ListItem("ParkingCard Fee", "0007"));
            ddlFeeType.Items.Add(new ListItem("Special Fee", "0012"));
            ddlFeeType.Items.Add(new ListItem("Deposit Contract", "0013"));
            ddlFeeType.Items.Add(new ListItem("Deposit Security", "0014"));

        }
    }
}