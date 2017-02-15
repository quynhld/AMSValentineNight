using System;
using System.Data;
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
using KN.Manage.Biz;
using KN.Resident.Biz;
using KN.Settlement.Biz;
using KN.Parking.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class MakeAptCmpInfo : BasePage
    {
        DataTable dtReturn1;
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
            //if (Request.Params[Master.PARAM_DATA1] == null) return;
            if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1])) return;
            //hfUserSeq.Value = Request.Params[Master.PARAM_DATA1];
            hfRentCd.Value = Request.Params[Master.PARAM_DATA1];

            txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1];
            
        }
        protected void InitControls()
        {
            MakeCarNoDdl(ddlCarNo, null);
            LoadCarTyDdl(ddlCarTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_CARTY);
            EnableButton();
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_SELECT_SPECIAL_DEBIT_I00
            var strRoomNo = txtRoomNo.Text;
            var strCompNm = txtCompanyNm.Text;
            var strCarNo = txtSearchCarNo.Text;
            var strParkingCardNo = txtSearchCardNo.Text;
            var strLangType = Session["LangCd"].ToString();
            var strCarTy = ddlCarTy.SelectedValue;            
           
            //KN_USP_PRK_SELECT_APTParkingCmpInfo_S00
            var dtReturn = MngPaymentBlo.SelectAPTParkingCmpInfo(strRoomNo, strCompNm, strCarNo, strParkingCardNo, strLangType, strCarTy);
            if (dtReturn == null) return;
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();

            //  var reft_seq = string.Empty;
             

            //  if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
            //  {

            //      //reft_seq = ((TextBox)lvPrintoutList.Items[0].FindControl("hfRefSeq")).Text;
            //      reft_seq = hfRefSeq.Value;
            //      //}
            //      if (!string.IsNullOrEmpty((reft_seq)))
            //      {
            //          LoadDetails((reft_seq));
            //      }
            //  }

           
           
        }

       

        //---------------------------New Function ---------------------------------

        protected void LoadCarTyDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.CARTY_VALUE_FREE_EXCEPTION) &&
                    !dr["CodeCd"].ToString().Equals("0004"))
                {
                    ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }
        //------------------------------------------------------------------------------------------
       
        //---------------------Change Room Get Car No -----------------------------------------------

        protected void imgbtnRoomChange_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string strRoomNo = txtRegRoomNo.Text;
                string strSearchDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                // KN_USP_RES_SELECT_ROOMINFO_S02
                DataSet dsRoomInfo = RoomMngBlo.SpreadRoomInfo(strRoomNo, strSearchDt);

                if (dsRoomInfo != null)
                {
                    if (dsRoomInfo.Tables[0].Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        if (dsRoomInfo.Tables[0].Rows[0]["RtnValue"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                        {
                            // 해당 호실이 있는 경우                            
                            txtHfRentCd.Text = dsRoomInfo.Tables[0].Rows[0]["RentCd"].ToString();
                            hfUserSeq.Value = dsRoomInfo.Tables[0].Rows[0]["UserSeq"].ToString();
                            if(!string.IsNullOrEmpty(txtTagNo.Text))
                            {

                            }
                            else
                            {
                                MakeCarNoDdl(ddlCarNo, dsRoomInfo.Tables[1]);
                            }
                            

                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("document.getElementById('" + ddlCarNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoRoomAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // 해당 호실이 없는 경우                           
                            txtHfRentCd.Text = string.Empty;

                            MakeCarNoDdl(ddlCarNo, dsRoomInfo.Tables[1]);
                            

                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");
                            sbList.Append("document.getElementById('" + txtRegRoomNo.ClientID + "').value = '';");
                            sbList.Append("document.getElementById('" + txtRegRoomNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoRoomAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        //-------------------------------------Make Car no-------------------------------------------------------

        private void MakeCarNoDdl(DropDownList ddlParams, DataTable dtParams)
        {
            ddlParams.Items.Clear();
            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], ""));

            if (dtParams != null)
            {
                string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                if (dtParams.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    foreach (DataRow dr in dtParams.Select())
                    {
                        if (!string.IsNullOrEmpty(dr["ParkingCarNo"].ToString()))
                        {
                            ddlParams.Items.Add(new ListItem(dr["ParkingCarNo"].ToString(), dr["ParkingCarNo"].ToString()));
                        }
                        else
                        {
                            txtCarTy.Text = string.Empty;                            
                        }
                    }
                }
                else
                {
                    txtCarTy.Text = string.Empty;
                  
                }
            }
        }
        //--------------------------MakeCarNo1-------------------------------------------------------------------
        private void MakeCarNoDdlOne(DropDownList ddlParams, DataTable dtParams)
        {
            ddlParams.Items.Clear();            

            if (dtParams != null)
            {
                string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                if (dtParams.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    foreach (DataRow dr in dtParams.Select())
                    {
                        if (!string.IsNullOrEmpty(dr["ParkingCarNo"].ToString()))
                        {
                            ddlParams.Items.Add(new ListItem(dr["ParkingCarNo"].ToString(), dr["ParkingCarNo"].ToString()));
                        }                        
                    }
                }
                
            }
        }
        //---------------------------------------------------------------------------------------------
                       
        

        protected void LoadDetails(string refSeq)
        {

            
        }
      

        //---------------------------------o----------------------------------------


        /// <summary>
        /// 검색버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPaymentList_ItemCreated(object sender, ListViewItemEventArgs e)
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
        protected void lvPaymentDetails_ItemCreated(object sender, ListViewItemEventArgs e)
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

        

        

        protected void lvPaymentList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void lvPaymentDetails_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
           
        }

        protected void lvPaymentList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
        }

        protected void lvPaymentDetails_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
           
        }
       

        protected void DisableButton()
        {
            lnkbtnRegist.Visible = true;
            lnkbtnBack.Visible = true;

            lnkbtnUpdate.Visible = true;
            lnkbtnCancel.Visible = true;

            txtRegRoomNo.Text = "";
            txtTagNo.Text = "";
            txtCarTy.Text = "";
            txtParkingCardNo.Text = "";
            txtTaxNo.Text = "";
            txtCmpName.Text = "";
            txtAddress.Text = "";
            txtDetAddress.Text = "";
            txtRegRoomNo.Focus();
        }

        protected void EnableButton()
        {
            lnkbtnRegist.Visible = true;
            lnkbtnBack.Visible = true;
            
            lnkbtnUpdate.Visible = true;
            lnkbtnCancel.Visible = true;

            txtRegRoomNo.Enabled = true;
            ddlCarNo.Enabled = true;

            ddlCarNo.SelectedIndex = 0;
            txtRegRoomNo.Text = "";
            txtTagNo.Text = "";
            txtCarTy.Text = "";
            txtParkingCardNo.Text = "";
            txtTaxNo.Text = "";
            txtCmpName.Text = "";
            txtAddress.Text = "";
            txtDetAddress.Text = "";

            txtRegRoomNo.Focus();
        }



        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                var strRentCd = txtHfRentCd.Text;
                const int terMonth = 1;
                var strRoomNo = txtRegRoomNo.Text;
                var strCarNo = ddlCarNo.SelectedItem.Text;
                var strTagNo = txtTagNo.Text;
                var strCarTy = txtHfCarTy.Text;
                var strCardNo = txtParkingCardNo.Text;
                var strTaxCode = txtTaxNo.Text;
                var strCmpNm = txtCmpName.Text;
                var strAddress = txtAddress.Text;
                var strDetAddress = txtDetAddress.Text;
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                var strCmCd = Session["CompCd"].ToString();
                var strMemNo = Session["MemNo"].ToString();


                //KN_USP_INSERT_APTParkingCmpInfo_I00

                var dtReturn = MngPaymentBlo.InsertAptCmpInfo(strRoomNo, strTagNo, strCardNo, strCarNo, strCarTy, strTaxCode, strCmpNm, strAddress, strDetAddress, strCmCd, strMemNo, strInsMemIP);
               

                StringBuilder sbWarning = new StringBuilder();
                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
                sbWarning.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Register", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                LoadData();
                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }           
    
        }     
       

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                AuthCheckLib.CheckSession();

                string strText = string.Empty;

                strText = AlertNm["INFO_MUST_DELETE_ENTIRE"] + "\\n" + AlertNm["CONF_PRCEED_WORK"];

                StringBuilder sbList = new StringBuilder();
                sbList.Append("javascript:fnReConfirm('" + strText + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingDelete", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);

                //// 세션체크
                //AuthCheckLib.CheckSession();

                //var strRoomNo = txtRegRoomNo.Text;
                //var strCarNo = ddlCarNo.SelectedItem.Text;
                //var strTagNo = txtTagNo.Text;               

                //var dtReturn = MngPaymentBlo.CancelAPTParkingCmpInfo(strRoomNo, strTagNo, strCarNo);
                //LoadData();
               
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnDelMonthInfo_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var strRoomNo = txtRegRoomNo.Text;
                var strCarNo = ddlCarNo.SelectedItem.Text;
                var strTagNo = txtTagNo.Text;

                var dtReturn = MngPaymentBlo.CancelAPTParkingCmpInfo(strRoomNo, strTagNo, strCarNo);
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnCancel_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }

        protected void ddlPaymentTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        

        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            var strRoomNo = txtRegRoomNo.Text;            
            var strParkingTagNo = txtTagNo.Text;
            var strLangType = Session["LangCd"].ToString();
           
            ////KN_USP_PRK_SELECT_APTParkingCmpInfo_S00
            //var dtReturn = MngPaymentBlo.SelectDetailAPTParkingCmpInfo(strRoomNo, strParkingTagNo, strLangType);
            //ddlCarNo.Text = dtReturn.Rows[0]["ParkingCarNo"].ToString();
            if (!string.IsNullOrEmpty(txtTagNo.Text))
            {                              
                                
                //KN_USP_PRK_SELECT_APTParkingCmpInfo_S00
                DataTable dtReturn = MngPaymentBlo.SelectDetailAPTParkingCmpInfo(strRoomNo, strParkingTagNo, strLangType);
                MakeCarNoDdlOne(ddlCarNo, dtReturn);                
            }
            ddlCarNo.Enabled = false;
            txtRegRoomNo.Enabled = false;
                            
        }

        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfsendParam.Value;
            try
            {
                string docNo = string.Empty;
                //MngPaymentBlo.UpdatingPrintedYNSpecialDebit(reft);
                MngPaymentBlo.InsertSpecialDebitToHoadonInfo(reft);
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }

        protected void imgbtnDetailPayment_Click(object sender, ImageClickEventArgs e)
        {           
        }

       

        protected void ddlPaidCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            LoadData();
        }

        

        protected void LoadOldUserInfo(string roomNo)
        {

          
        }

       

        protected void txtInputRoom_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtInputPayDt_TextChanged(object sender, EventArgs e)
        {
            
           
        }

        protected void lvPrintoutList_LayoutCreated(object sender, EventArgs e)
        {

        }

        protected void lvPrintoutList_ItemCreated(object sender, ListViewItemEventArgs e)
        {

        }

        protected void lvPrintoutList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {    

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = drView["RoomNo"].ToString();
                }

                Literal ltCarNo = (Literal)iTem.FindControl("ltCarNo");

                if (!string.IsNullOrEmpty(drView["ParkingCarNo"].ToString()) && !string.IsNullOrEmpty(drView["ParkingCardNo"].ToString()))
                {
                    ltCarNo.Text = drView["ParkingCarNo"].ToString() + " (" + drView["ParkingCardNo"].ToString().Trim() + ")";
                    //ltCarNo.Text = drView["ParkingCarNo"].ToString();
                }

                Literal ltCarType = (Literal)iTem.FindControl("ltCarType");

                if (!string.IsNullOrEmpty(drView["CarTyNm"].ToString()))
                {
                    ltCarType.Text = drView["CarTyNm"].ToString();
                }

                Literal ltCmpNm = (Literal)iTem.FindControl("ltCmpNm");

                if (!string.IsNullOrEmpty(drView["CmpNm"].ToString()))
                {
                    ltCmpNm.Text = drView["CmpNm"].ToString();
                }
                Literal ltTaxCode = (Literal)iTem.FindControl("ltTaxCode");

                if (!string.IsNullOrEmpty(drView["CmpTaxCd"].ToString()))
                {
                    ltTaxCode.Text = drView["CmpTaxCd"].ToString();
                }
            }
        }

        

        protected void lnkbtUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var strRentCd = txtHfRentCd.Text;
                
                var strRoomNo = txtRegRoomNo.Text;
                var strCarNo = ddlCarNo.SelectedItem.Text;
                var strTagNo = txtTagNo.Text;
                var strCarTy = txtHfCarTy.Text;
                var strCardNo = txtParkingCardNo.Text;
                var strTaxCode = txtTaxNo.Text;
                var strCmpNm = txtCmpName.Text;
                var strAddress = txtAddress.Text;
                var strDetAddress = txtDetAddress.Text;
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                var strCmCd = Session["CompCd"].ToString();
                var strMemNo = Session["MemNo"].ToString();

                // 오피스 / 리테일 데이터 수정
                // KN_SCR_UPDATE_APTParkingCmpInfo_U00
                MngPaymentBlo.UpdateAPTParkingCmpInfo(strRoomNo, strTagNo, strCarNo, strTaxCode, strCmpNm, strAddress, strDetAddress, strCmCd, strMemNo, strInsMemIP);

                LoadData();

                txtRegRoomNo.Enabled = false;
                ddlCarNo.Enabled = false;


                StringBuilder sbWarning = new StringBuilder();
                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
                sbWarning.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Modify", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }        
        }
        

        protected void txtReqDt_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void lnkbtnBack_Click(object sender, EventArgs e)
        {
            EnableButton();
            MakeCarNoDdl(ddlCarNo, null);
        }

        protected void rbMoneyCd_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlCarNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strCarNo = ddlCarNo.SelectedValue;
                if (!string.IsNullOrEmpty(strCarNo))
                {
                    // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S04
                    DataTable dtReturn = ParkingMngBlo.WatchMonthParkingInfo(strCarNo, Session["LangCd"].ToString());
                    txtHfCarTy.Text = dtReturn.Rows[0]["CarTyCd"].ToString();
                    txtCarTy.Text = dtReturn.Rows[0]["CarTyNm"].ToString();                    
                    txtParkingCardNo.Text = dtReturn.Rows[0]["ParkingCardNo"].ToString();
                    txtTagNo.Text = dtReturn.Rows[0]["ParkingTagNo"].ToString();                    
                }
                else
                {
                    string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                    txtCarTy.Text = string.Empty;
                    txtParkingCardNo.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvAccountsList_ItemCreated(object sender, ListViewItemEventArgs e)
        {

        }

        protected void lvAccountsList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void lvAccountsList_LayoutCreated(object sender, EventArgs e)
        {

        }

        protected void chkAll_CheckedChanged1(object sender, EventArgs e)
        {
            bool isAllCheck = chkAll.Checked;

            try
            {
                CheckAll(isAllCheck);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>   
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
        private void CheckAll(bool isAllCheck)
        {
            for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                }
            }
        }

        protected void txtRegRoomNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string strRoomNo = txtRegRoomNo.Text;
                var strParkingTagNo = txtTagNo.Text;
                var strLangType = Session["LangCd"].ToString();
                string strSearchDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                ddlCarNo.Items.Clear();

                // KN_USP_RES_SELECT_ROOMINFO_S11
                DataSet dsRoomInfo = RoomMngBlo.SelectParkingCarNoPrintOutSpcInv(strRoomNo, strSearchDt);

                if (dsRoomInfo != null)
                {
                    if (dsRoomInfo.Tables[0].Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        if (dsRoomInfo.Tables[0].Rows[0]["RtnValue"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                        {
                            // 해당 호실이 있는 경우                            
                            txtHfRentCd.Text = dsRoomInfo.Tables[0].Rows[0]["RentCd"].ToString();
                            hfUserSeq.Value = dsRoomInfo.Tables[0].Rows[0]["UserSeq"].ToString();                            

                            MakeCarNoDdl(ddlCarNo, dsRoomInfo.Tables[1]);


                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("document.getElementById('" + ddlCarNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoRoomAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // 해당 호실이 없는 경우                           
                            txtHfRentCd.Text = string.Empty;

                            MakeCarNoDdl(ddlCarNo, dsRoomInfo.Tables[1]);


                            StringBuilder sbList = new StringBuilder();
                            sbList.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");
                            sbList.Append("document.getElementById('" + txtRegRoomNo.ClientID + "').value = '';");
                            sbList.Append("document.getElementById('" + txtRegRoomNo.ClientID + "').focus();");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoRoomAlert", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPrintoutList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       

    }
}
