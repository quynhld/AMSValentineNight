using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Manage.Ent;
using KN.Parking.Biz;
using KN.Resident.Biz;

namespace KN.Web.Park
{
    public partial class ParkingContractWrite : BasePage
    {
        MngUtilDs.UtilityInfo utilDs = new MngUtilDs.UtilityInfo();
        protected int intRowCnt = CommValue.NUMBER_VALUE_0;
        private bool _isEdit = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (IsPostBack) return;
                CheckParam();

                InitControls();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params[Master.PARAM_DATA1] == null) return;
            if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1])) return;
            txtHfUserSeq.Text = Request.Params[Master.PARAM_DATA1];
        }

        protected void InitControls()
        {

            lnkbtnDelete.OnClientClick = "javascript:return fnDeleteData('');";
            lnkbtnWrite.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
            LoadRoomNo();
            if (string.IsNullOrEmpty(txtHfUserSeq.Text))
            {
                return;
            }
            LoadUserInfoEdit(txtHfUserSeq.Text);
        }

        protected void LoadRoomNo() 
        {
            DataTable dtReturn = new DataTable();

            string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

            // KN_USP_RES_SELECT_ROOMINFO_S08
            dtReturn = RoomMngBlo.WatchRoomUserInfo(txtHfRentCd.Text, "", strNowDt);

            if (dtReturn == null) return;
            ddlCompanyName.Items.Clear();
            ddlCompanyName.Items.Add(new ListItem("Company Name", ""));

            if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
            {
                foreach (DataRow dr in dtReturn.Select())
                {
                    ddlCompanyName.Items.Add(new ListItem(TextLib.StringDecoder(dr["UserNm"].ToString()), dr["RoomNo"].ToString()));
                }
            }
        }

        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                //KN_USP_MNG_INSERT_UTILCHARGEINFO_M05
                var tenantNm = txtTenantName.Text;
                var addr = txtAddress.Text;
                var addr1 = txtAddress1.Text;
                var taxCd = txtTaxCd.Text;
                var roomNo = txtRoomNo.Text;
                var contractNo = txthfContractNo.Text;
                var rentCd = txtHfRentCd.Text;
                var userSeq = txtHfUserSeq.Text;
                var objReturn = ParkingMngBlo.InsertUserParkingInfo(userSeq,rentCd,roomNo,contractNo,tenantNm,addr,addr1,taxCd);
                if ((bool) objReturn[0])
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:SaveSuccess()", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Check Infomation')", CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception)
            {
                
                throw;
            }


        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }            
        }

        protected void lnkReload_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

               // Response.Redirect("UtilFeeWrite.aspx?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            //KN_USP_MNG_INSERT_UTILCHARGEINFO_M05
            var objReturn = ParkingMngBlo.DeleteUserParkingInfo(txtHfUserSeq.Text);
            if ((bool)objReturn[0])
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:DeleteSuccess()", CommValue.AUTH_VALUE_TRUE);
            }
        }

        protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRoomNo.Text = ddlCompanyName.SelectedValue;
            LoadOldUserInfo(txtRoomNo.Text);
        }

        protected void LoadOldUserInfo(string roomNo)
        {
            if (string.IsNullOrEmpty(roomNo))
            {
                ResetControl();
            }
            else
            {                           
                var strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                // KN_USP_RES_SELECT_ROOMINFO_S08
                var dtReturn = RoomMngBlo.WatchRoomUserInfo(txtHfRentCd.Text, roomNo, strNowDt);

                if (dtReturn == null) return;
                if (dtReturn.Rows.Count <= CommValue.NUMBER_VALUE_0) return;
                foreach (var dr in dtReturn.Select())
                {
                    ddlCompanyName.Items.Add(new ListItem(TextLib.StringDecoder(dr["UserNm"].ToString()), dr["RoomNo"].ToString()));
                    txtTenantName.Text = dr["UserNm"].ToString();
                    txtAddress.Text = dr["UserAddr"].ToString();
                    txtAddress1.Text = dr["UserDetAddr"].ToString();
                    txtTaxCd.Text = dr["UserTaxCd"].ToString();
                    txthfContractNo.Text = dr["ContractNo"].ToString();
                }
            }
        }

        protected void LoadUserInfoEdit(string strUserSeq)
        {
            ddlCompanyName.Enabled = false;
            
            var dsReturn = ParkingMngBlo.GetParkingUserListInfo("0000", "", "", strUserSeq);
            if (dsReturn == null || dsReturn.Rows.Count <= 0) return;
            txtTenantName.Text = dsReturn.Rows[0]["Tenant_Name"].ToString();
            txtAddress.Text = dsReturn.Rows[0]["Tax_Address"].ToString();
            txtAddress1.Text = dsReturn.Rows[0]["Tax_Address1"].ToString();
            txtTaxCd.Text = dsReturn.Rows[0]["Tax_Code"].ToString();
            lnkbtnWrite.Visible = dsReturn.Rows[0]["SubLessor"].ToString() == "Y";
            lnkbtnDelete.Visible = dsReturn.Rows[0]["SubLessor"].ToString() == "Y";
            ddlCompanyName.SelectedValue = dsReturn.Rows[0]["RoomNo"].ToString();
            txtRoomNo.Text = dsReturn.Rows[0]["RoomNo"].ToString();
            txtRoomNo.ReadOnly = true;
            try
            {
                ddlInsRentCd.SelectedValue = dsReturn.Rows[0]["RentCd"].ToString();
                ddlInsRentCd.Enabled = false;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void txtRoomNo_TextChanged(object sender, EventArgs e)
        {
            //ddlCompanyName.SelectedValue = txtRoomNo.Text;
        }
        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING))
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP))
                    {
                        ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
            }
        }

        protected void ddlInsRentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtHfRentCd.Text = ddlInsRentCd.SelectedValue;
            LoadRoomNo();
        }
        protected void ResetControl()
        {
            txtAddress.Text = "";
            txtAddress1.Text = "";
            txtTenantName.Text = "";
            txthfContractNo.Text = "";
            txtTaxCd.Text = "";
        }
    }
}