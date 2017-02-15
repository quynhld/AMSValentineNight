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

using KN.Manage.Biz;

namespace KN.Web.Management.Manage
{
    public partial class MngFeeItemList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
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
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
            {
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                txtHfFeeTy.Text = Request.Params[Master.PARAM_DATA2].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltMngRentCd.Text = TextNm["RENT"];
            ltItemTy.Text = TextNm["TOPICCD"];
            ltMngFeeNmEn.Text = TextNm["TOPICENM"];
            ltMngFeeNmVi.Text = TextNm["TOPICNM"];
            ltMngFeeNmKr.Text = TextNm["TOPICKNM"];

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL);

            if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APT))
            {
                ddlRentCd.SelectedValue = CommValue.RENTAL_VALUE_APTA;
            }
            else if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APTSHOP))
            {
                ddlRentCd.SelectedValue = CommValue.RENTAL_VALUE_APTASHOP;
            }
            else
            {
                ddlRentCd.SelectedValue = txtHfRentCd.Text;
            }

            imgbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnRegist.Visible = Master.isWriteAuthOk;
        }

        protected void ResetControls()
        {
            if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APT))
            {
                ddlRentCd.SelectedValue = CommValue.RENTAL_VALUE_APTA;
            }
            else if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APTSHOP))
            {
                ddlRentCd.SelectedValue = CommValue.RENTAL_VALUE_APTASHOP;
            }
            else
            {
                ddlRentCd.SelectedValue = txtHfRentCd.Text;
            }

            txtMngFeeCd.Text = string.Empty;
            txtMngFeeNmEn.Text = string.Empty;
            txtMngFeeNmVi.Text = string.Empty;
            txtMngFeeNmKr.Text = string.Empty;
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtListReturn = new DataTable();

            // KN_USP_MNG_SELECT_MNGMENUINFO_S00
            dtListReturn = MngPaymentBlo.SpreadMngMenuinfo(txtHfRentCd.Text, txtHfFeeTy.Text);

            if (dtListReturn != null)
            {
                lvRentItemList.DataSource = dtListReturn;
                lvRentItemList.DataBind();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRentItemList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvRentItemList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                DropDownList ddlRentCd = (DropDownList)iTem.FindControl("ddlRentCd");
                TextBox txtRentCd = (TextBox)iTem.FindControl("txtRentCd");

                CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL);

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    ddlRentCd.SelectedValue = drView["RentCd"].ToString();
                    txtRentCd.Text = drView["RentCd"].ToString();
                }

                ddlRentCd.Enabled = CommValue.AUTH_VALUE_FALSE;

                if (!string.IsNullOrEmpty(drView["MngFeeCd"].ToString()))
                {
                    Literal ltMngItemTy = (Literal)iTem.FindControl("ltMngItemTy");
                    ltMngItemTy.Text = drView["MngFeeCd"].ToString();

                    TextBox txtMngItemTy = (TextBox)iTem.FindControl("txtMngItemTy");
                    txtMngItemTy.Text = drView["MngFeeCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MngFeeNmEn"].ToString()))
                {
                    TextBox txtMngFeeNmEn = (TextBox)iTem.FindControl("txtMngFeeNmEn");
                    txtMngFeeNmEn.Text = TextLib.StringDecoder(drView["MngFeeNmEn"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["MngFeeNmVi"].ToString()))
                {
                    TextBox txtMngFeeNmVi = (TextBox)iTem.FindControl("txtMngFeeNmVi");
                    txtMngFeeNmVi.Text = TextLib.StringDecoder(drView["MngFeeNmVi"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["MngFeeNmKr"].ToString()))
                {
                    TextBox txtMngFeeNmKr = (TextBox)iTem.FindControl("txtMngFeeNmKr");
                    txtMngFeeNmKr.Text = TextLib.StringDecoder(drView["MngFeeNmKr"].ToString());
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.Visible = Master.isModDelAuthOk;
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.Visible = Master.isModDelAuthOk;
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
            }
        }

        protected void lvRentItemList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtRentCd = (TextBox)lvRentItemList.Items[e.ItemIndex].FindControl("txtRentCd");
                TextBox txtMngItemTy = (TextBox)lvRentItemList.Items[e.ItemIndex].FindControl("txtMngItemTy");

                // KN_USP_MNG_DELETE_MNGMENUINFO_M00
                MngPaymentBlo.RemoveMngFeeItem(txtRentCd.Text, txtHfFeeTy.Text, txtMngItemTy.Text);

                LoadData();

                StringBuilder sbWarning = new StringBuilder();

                sbWarning.Append("alert('" + AlertNm["INFO_DELETE_ITEM"] + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteItem", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvRentItemList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtRentCd = (TextBox)lvRentItemList.Items[e.ItemIndex].FindControl("txtRentCd");
                TextBox txtMngItemTy = (TextBox)lvRentItemList.Items[e.ItemIndex].FindControl("txtMngItemTy");

                TextBox txtMngFeeNmEn = (TextBox)lvRentItemList.Items[e.ItemIndex].FindControl("txtMngFeeNmEn");
                TextBox txtMngFeeNmVi = (TextBox)lvRentItemList.Items[e.ItemIndex].FindControl("txtMngFeeNmVi");
                TextBox txtMngFeeNmKr = (TextBox)lvRentItemList.Items[e.ItemIndex].FindControl("txtMngFeeNmKr");

                // KN_USP_MNG_UPDATE_MNGMENUINFO_M00
                MngPaymentBlo.ModifyMngFeeItem(txtRentCd.Text, txtHfFeeTy.Text, txtMngItemTy.Text, txtMngFeeNmEn.Text, txtMngFeeNmVi.Text, txtMngFeeNmKr.Text);

                LoadData();

                StringBuilder sbWarning = new StringBuilder();

                sbWarning.Append("alert('" + AlertNm["INFO_MODIFY_ITEM"] + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModifyItem", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnRegist_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_MNG_SELECT_MNGMENUINFO_S01
                DataTable dtReturn = MngPaymentBlo.WatchMngMenuItemCheck(ddlRentCd.SelectedValue, txtHfFeeTy.Text, txtMngFeeCd.Text);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        if (Int32.Parse(dtReturn.Rows[0]["ExistCnt"].ToString()) > 0)
                        {
                            StringBuilder sbWarning = new StringBuilder();

                            sbWarning.Append("alert('" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // KN_USP_MNG_INSERT_MNGMENUINFO_M00
                            MngPaymentBlo.RegistryMngFeeItem(ddlRentCd.SelectedValue, txtHfFeeTy.Text, txtMngFeeCd.Text, txtMngFeeNmEn.Text, txtMngFeeNmVi.Text, 
                                                             txtMngFeeNmKr.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                            ResetControls();

                            LoadData();

                            StringBuilder sbWarning = new StringBuilder();

                            sbWarning.Append("alert('" + AlertNm["INFO_REGIST_ITEM"] + "');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModifyItem", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}