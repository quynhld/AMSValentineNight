using System;
using System.Data;
using System.EnterpriseServices;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;

namespace KN.Web.Config.Authority
{
    [Transaction(TransactionOption.Required)]
    public partial class AuthorityGroupList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                
                if (!IsPostBack)
                {
                    InitContorols();

                    LoadData();
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
        protected void InitContorols()
        {
            ltSeq.Text = TextNm["SEQ"];
            ltAuthCd.Text = TextNm["AUTHCD"];
            ltAuthNm.Text = TextNm["AUTHNM"];
            ltAuthENm.Text = TextNm["AUTHENM"];
            ltAuthKNm.Text = TextNm["AUTHKNM"];
            txtHfAuthLvl.Text = CommValue.MAIN_COMP_CD;

            imgbtnInsert.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
        }

        protected void ResetControls()
        {
            txtInsAuthNm.Text = string.Empty;
            txtInsAuthENm.Text = string.Empty;
            txtInsAuthKNm.Text = string.Empty;
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        protected void LoadData()
        {
            ResetControls();

            DataTable dtReturn = new DataTable();

            // KN_USP_MNG_SELECT_AUTHGRPINFO_S00
            dtReturn = AuthorityMngBlo.SpreadAuthGrpInfo(txtHfAuthLvl.Text);

            if (dtReturn == null)
            {
                throw new Exception("DB와의 연결이 끊어졌습니다.");
            }
            else
            {
                lvAuthGrp.DataSource = dtReturn;
                lvAuthGrp.DataBind();
            }
        }
        
        protected void lvAuthGrp_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            TextBox txtHfAuthLvl = (TextBox)lvAuthGrp.Items[e.ItemIndex].FindControl("txtHfAuthLvl");
            TextBox txtTmpAuthCd = (TextBox)lvAuthGrp.Items[e.ItemIndex].FindControl("txtAuthCd");
            TextBox txtTmpAuthNm = (TextBox)lvAuthGrp.Items[e.ItemIndex].FindControl("txtAuthNm");
            TextBox txtTmpAuthENm = (TextBox)lvAuthGrp.Items[e.ItemIndex].FindControl("txtAuthENm");
            TextBox txtTmpAuthKNm = (TextBox)lvAuthGrp.Items[e.ItemIndex].FindControl("txtAuthKNm");

            // KN_USP_MNG_UPDATE_AUTHGRPINFO_M00
            AuthorityMngBlo.ModifyAuthGrpInfo(Session["CompCd"].ToString(), Session["MemNo"].ToString(), txtHfAuthLvl.Text, txtTmpAuthCd.Text, txtTmpAuthNm.Text, txtTmpAuthENm.Text, txtTmpAuthKNm.Text);

            LoadData();
        }

        protected void lvAuthGrp_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            TextBox txtHfAuthLvl = (TextBox)lvAuthGrp.Items[e.ItemIndex].FindControl("txtHfAuthLvl");
            TextBox txtTmpAuthCd = (TextBox)lvAuthGrp.Items[e.ItemIndex].FindControl("txtAuthCd");

            // KN_USP_MNG_DELETE_AUTHGRPINFO_M00
            AuthorityMngBlo.RemoveAuthGrpInfo(txtHfAuthLvl.Text, txtTmpAuthCd.Text);

            LoadData();
        }

        protected void lvAuthGrp_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // ListView 데이터 바인딩 처리
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                TextBox txtTmpSeq = (TextBox)iTem.FindControl("txtSeq");
                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {
                    txtTmpSeq.Text = drView["Seq"].ToString();
                }

                TextBox txtHfAuthLvl = (TextBox)iTem.FindControl("txtHfAuthLvl");

                if (!string.IsNullOrEmpty(drView["AuthLvl"].ToString()))
                {
                    txtHfAuthLvl.Text = drView["AuthLvl"].ToString();
                }

                TextBox txtTmpAuthCd = (TextBox)iTem.FindControl("txtAuthCd");
                if (!string.IsNullOrEmpty(drView["MemAuthTy"].ToString()))
                {
                    txtTmpAuthCd.Text = drView["MemAuthTy"].ToString();
                    txtMaxAuthCd.Text = (Int32.Parse(txtTmpAuthCd.Text) * 2).ToString().PadLeft(8, '0');
                    txtInsAuthCd.Text = txtMaxAuthCd.Text;
                    txtHfAuthCd.Text = txtMaxAuthCd.Text;
                }

                TextBox txtTmpAuthNm = (TextBox)iTem.FindControl("txtAuthNm");
                if (!string.IsNullOrEmpty(drView["MemAuthTyNm"].ToString()))
                {
                    txtTmpAuthNm.Text = drView["MemAuthTyNm"].ToString();
                }

                TextBox txtTmpAuthENm = (TextBox)iTem.FindControl("txtAuthENm");
                if (!string.IsNullOrEmpty(drView["MemAuthTyENm"].ToString()))
                {
                    txtTmpAuthENm.Text = drView["MemAuthTyENm"].ToString();
                }

                TextBox txtTmpAuthKNm = (TextBox)iTem.FindControl("txtAuthKNm");
                if (!string.IsNullOrEmpty(drView["MemAuthTyKNm"].ToString()))
                {
                    txtTmpAuthKNm.Text = drView["MemAuthTyKNm"].ToString();
                }

                ImageButton imgbtnUpdate = (ImageButton)iTem.FindControl("imgbtnUpdate");
                imgbtnUpdate.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "')";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            }
        }

        protected void lvAuthGrp_ItemCommand(object sender, ListViewCommandEventArgs e)
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

        protected void lvAuthGrp_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];

                    txtMaxAuthCd.Text = CommValue.AUTH_VALUE_SUPER;
                    txtInsAuthCd.Text = CommValue.AUTH_VALUE_SUPER;
                    txtHfAuthCd.Text = CommValue.AUTH_VALUE_SUPER;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnInsert_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            // KN_USP_MNG_INSERT_AUTHGRPINFO_M00
            AuthorityMngBlo.RegistryAuthGrpInfo(Session["CompCd"].ToString(), Session["MemNo"].ToString(), txtInsAuthCd.Text, txtHfAuthLvl.Text, txtInsAuthNm.Text, txtInsAuthENm.Text, txtInsAuthKNm.Text);

            LoadData();
        }
    }
}