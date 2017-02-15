using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Manage.Biz;
using KN.Manage.Ent;
using KN.Resident.Biz;

namespace KN.Web.Management.Remote
{
    public partial class UtilFeeWrite : BasePage
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
            txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1];
            txtHfChargeTy.Text = Request.Params[Master.PARAM_DATA2];
            if (Request.Params["RoomNo"] == null || Request.Params["USeq"] == null) return;
            txthfRoomNo.Text = Request.Params["RoomNo"];
            txthfUSeq.Text = Request.Params["USeq"];
            HfReturnUserSeqId.Value = Request.Params["USeq"];
        }

        protected void InitControls()
        {
            lnkbtnWrite.Visible = Master.isWriteAuthOk;
            lnkbtnDelete.Visible = Master.isModDelAuthOk;
            lnkbtnDelete.OnClientClick = "javascript:return fnDeleteData('');";
            lnkbtnWrite.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtTitle.ClientID + "', '" + txtRoomNo.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtTitle.Text + "', '" + txtHfRentCd.Text + "');";
            txtLastIndex.Attributes["onblur"] = "countIndex();";
            txtFistIndex.Attributes["onblur"] = "countIndex();";
            if(Session["currentEx"] !=null)
            {
                txtExchangeRate.Text = Session["currentEx"].ToString();
            }
            if (string.IsNullOrEmpty(txthfRoomNo.Text)||string.IsNullOrEmpty(txthfChargeSeq.Text))
            {
                return;
            }
            var roomNo = txthfRoomNo.Text;            
            LoadUserInfoEdit(roomNo,txthfUSeq.Text);

        }

        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                utilDs.UserSeq = HfReturnUserSeqId.Value;
                utilDs.RoomNo = txtRoomNo.Text;
                utilDs.USeq = txthfUSeq.Text;
                utilDs.RentCd = txtHfRentCd.Text;
                utilDs.ChargeTy = txtHfChargeTy.Text;
                utilDs.ChargeSeq = txthfChargeSeq.Text;
                utilDs.StartDate = txtSUsingDt.Text.Replace("-", "");
                utilDs.EndDate = txtEUsingDt.Text.Replace("-", "");
                utilDs.SubDes = txtSubDes.Text;
                if (!string.IsNullOrEmpty(txtFistIndex.Text))
                {
                    utilDs.FistIndex = double.Parse(txtFistIndex.Text);
                }
                if (!string.IsNullOrEmpty(txtLastIndex.Text))
                {
                    utilDs.EndIndex = double.Parse(txtLastIndex.Text);
                }
                if (!string.IsNullOrEmpty(txtNormalHours.Text))
                {
                    utilDs.NormalUsing = double.Parse(txtNormalHours.Text);
                }
                if (!string.IsNullOrEmpty(txtHightUsing.Text))
                {
                    utilDs.HightUsing = double.Parse(txtHightUsing.Text);
                }
                if (!string.IsNullOrEmpty(txtLowUsing.Text))
                {
                    utilDs.LowUsing = double.Parse(txtLowUsing.Text);
                }
                if (!string.IsNullOrEmpty(txtNormalOtherUsing.Text))
                {
                    utilDs.NormalOtherUsing = double.Parse(txtNormalOtherUsing.Text);
                }                                                              
                utilDs.PaymentType = ddlPaymentType.SelectedValue;
                if (!string.IsNullOrEmpty(txtExchangeRate.Text))
                {
                    utilDs.ExchangRate = double.Parse(txtExchangeRate.Text);
                }
                
                utilDs.PayDate = txtRequestDt.Text.Replace("-", "");
                utilDs.DueDate = txtDueDt.Text.Replace("-", "");
                if (!string.IsNullOrEmpty(txtDisCount.Text))
                {
                    utilDs.Discount = double.Parse(txtDisCount.Text);
                }
                
                if (txtExchangeRate.Text != "0" && txtExchangeRate.Text!="")
                {
                    if (!_isEdit)
                    {
                        Session["currentEx"] = txtExchangeRate.Text;
                    }
                    
                }
                //KN_USP_MNG_INSERT_UTILCHARGEINFO_M05
                var objReturn = MngPaymentBlo.RegistryUtilManuallyInfo(utilDs);
                if ((bool) objReturn[0])
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:SaveSuccess()", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Check Infomation')", CommValue.AUTH_VALUE_TRUE);
                }

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
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect("MngReadMonthForRoom.aspx?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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

                Response.Redirect("UtilFeeWrite.aspx?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            //KN_USP_MNG_INSERT_UTILCHARGEINFO_M05
            var objReturn = MngPaymentBlo.DeleteUtilManuallyInfo(txtHfRentCd.Text,txthfUSeq.Text,txthfRoomNo.Text);
            if ((bool)objReturn[0])
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:DeleteSuccess()", CommValue.AUTH_VALUE_TRUE);
            }
        }

        protected void LoadOldUserInfo(string roomNo)
        {

            txtFistIndex.Text = "";
            txtSUsingDt.Text = "";
            var dsReturn = MngPaymentBlo.SelectMngUtilInfo(txtHfRentCd.Text, txtHfChargeTy.Text, roomNo, "","","","");
            if (dsReturn == null || dsReturn.Rows.Count<=0) return;
            txtFistIndex.Text = dsReturn.Rows[0]["EndIndex"].ToString();
            txtSUsingDt.Text = TextLib.MakeDateEightDigit(dsReturn.Rows[0]["EndDate"].ToString());
        }

        protected void LoadUserInfoEdit(string strRoomNo,string uSeq)
        {
            txtTitle.Enabled = false;
            lnkbtnDelete.Visible = true;
            imgbtnSearchCompNm.Visible = false;
            txtRoomNo.Text = strRoomNo;
            var dsReturn = MngPaymentBlo.SelectMngUtilInfo(txtHfRentCd.Text, txtHfChargeTy.Text, strRoomNo, uSeq, "","","");
            if (dsReturn == null || dsReturn.Rows.Count <= 0) return;
            txtTitle.Text = dsReturn.Rows[0]["UserNm"].ToString();
            txtFistIndex.Text = dsReturn.Rows[0]["FistIndex"].ToString();
            txtLastIndex.Text = dsReturn.Rows[0]["EndIndex"].ToString();
            txtSUsingDt.Text = TextLib.MakeDateEightDigit(dsReturn.Rows[0]["StartDate"].ToString());
            txtEUsingDt.Text = TextLib.MakeDateEightDigit(dsReturn.Rows[0]["EndDate"].ToString());
            txtRequestDt.Text = TextLib.MakeDateEightDigit(dsReturn.Rows[0]["PayDate"].ToString());
            txtDueDt.Text = TextLib.MakeDateEightDigit(dsReturn.Rows[0]["DueDate"].ToString());
            txtNormalHours.Text = dsReturn.Rows[0]["NormalUsing"].ToString();
            txtHightUsing.Text = dsReturn.Rows[0]["HightUsing"].ToString();
            txtLowUsing.Text = dsReturn.Rows[0]["LowUsing"].ToString();
            txtNormalOtherUsing.Text = dsReturn.Rows[0]["NormalOtherUsing"].ToString();
            txtDisCount.Text = dsReturn.Rows[0]["Discount"].ToString();
            ddlPaymentType.SelectedValue = dsReturn.Rows[0]["PaymentType"].ToString();
            txtExchangeRate.Text = dsReturn.Rows[0]["ExchangRate"].ToString();
            lnkbtnDelete.Visible = string.IsNullOrEmpty(dsReturn.Rows[0]["InvoiceNo"].ToString());
            lnkbtnWrite.Visible = string.IsNullOrEmpty(dsReturn.Rows[0]["InvoiceNo"].ToString());
            txtSubDes.Text = dsReturn.Rows[0]["SubDes"].ToString();
        }

        protected void lnkLoadOldRoom_Click(object sender, ImageClickEventArgs e)
        {
            LoadOldUserInfo(txtRoomNo.Text);
        }
    }
}