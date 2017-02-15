using System;
using System.Data;
using System.IO;
using System.Text;
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
    public partial class UtilFeeWriteOverTime : BasePage
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
            Master.PARAM_DATA1 = "RentCd";
            Master.PARAM_DATA2 = "ChargeTy";
            if (Request.Params[Master.PARAM_DATA1] == null) return;
            if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1])) return;
            txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1];
            txtHfChargeTy.Text = Request.Params[Master.PARAM_DATA2];
            if (Request.Params["RoomNo"] == null || Request.Params["ChargeSeq"] == null) return;
            txthfRoomNo.Text = Request.Params["RoomNo"];
            txthfChargeSeq.Text = Request.Params["ChargeSeq"];
            HfReturnUserSeqId.Value = Request.Params["ChargeSeq"];
        }

        protected void InitControls()
        {

            lnkbtnDelete.OnClientClick = "javascript:return fnDeleteData('');";
            lnkbtnWrite.OnClientClick = "javascript:return fnValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtTitle.ClientID + "', '" + txtRoomNo.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtTitle.Text + "', '" + txtHfRentCd.Text + "', '" + txtSquare.ClientID + "');";
            if (Session["currentExOver"] != null)
            {
                txtExchangeRate.Text = Session["currentExOver"].ToString();
            }
            if (string.IsNullOrEmpty(txthfRoomNo.Text)||string.IsNullOrEmpty(txthfChargeSeq.Text))
            {
                return;
            }
            var roomNo = txthfRoomNo.Text;
            LoadUserInfoEdit(roomNo, txthfChargeSeq.Text);
        }

        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                utilDs.UserSeq = HfReturnUserSeqId.Value;
                utilDs.RoomNo = txtRoomNo.Text;
                utilDs.RentCd = txtHfRentCd.Text;
                utilDs.ChargeTy = txtHfChargeTy.Text;
                utilDs.ChargeSeq = txthfChargeSeq.Text;
                utilDs.Period = txtPeriod.Text.Replace("-", "");
                if (!string.IsNullOrEmpty(txtHoursOver.Text))
                {
                    utilDs.HoursOver = double.Parse(txtHoursOver.Text);
                }
                
                utilDs.UnitPrice = double.Parse(txtUnitPrice.Text);
                if (!string.IsNullOrEmpty(txtSquare.Text))
                {
                    utilDs.Square = double.Parse(txtSquare.Text);
                }
                
                utilDs.PaymentType = ddlPaymentType.SelectedValue;
                utilDs.ExchangRate = double.Parse(txtExchangeRate.Text);
                utilDs.PayDate = txtRequestDt.Text.Replace("-", "");
                utilDs.DueDate = txtDueDt.Text.Replace("-", "");
                utilDs.Discount = double.Parse(txtDisCount.Text);
                utilDs.IncludeVat = chkIncludeVat.Checked ? "Y" : "N";
                utilDs.SubDes = txtSubDes.Text;
                if (txtExchangeRate.Text != "0" && txtExchangeRate.Text!="")
                {
                    if (!_isEdit)
                    {
                        Session["currentExOver"] = txtExchangeRate.Text;
                    }
                    
                }
                //KN_USP_MNG_INSERT_UTILCHARGEINFO_M08
                var objReturn = MngPaymentBlo.RegistryUtilOverManuallyInfo(utilDs);
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

                Response.Redirect("MngReadMonthForOverTime.aspx?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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
            //KN_USP_RES_INSERT_MONTHENERGY_M03
            var objReturn = MngPaymentBlo.DeleteUtilOverManuallyInfo(txtHfRentCd.Text,txtHfChargeTy.Text,txthfRoomNo.Text,txthfChargeSeq.Text);
            if ((bool)objReturn[0])
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:DeleteSuccess()", CommValue.AUTH_VALUE_TRUE);
            }
        }

        protected void LoadUserInfoEdit(string strRoomNo,string strChargeSeq)
        {
            imgbtnSearchCompNm.Visible = false;
            lnkbtnDelete.Visible = true;
            txtRoomNo.Text = strRoomNo;
            txtTitle.Enabled = false;
            var dsReturn = MngPaymentBlo.SelectMngUtilInfoOverTime(txtHfRentCd.Text, txtHfChargeTy.Text, strRoomNo, strChargeSeq, "", "", "");
            if (dsReturn == null || dsReturn.Rows.Count <= 0) return;
            _isEdit = true;
            txtPeriod.Text = TextLib.MakeDateSixDigit(dsReturn.Rows[0]["Period"].ToString());
            txtTitle.Text = dsReturn.Rows[0]["UserNm"].ToString();
            txtRequestDt.Text = TextLib.MakeDateEightDigit(dsReturn.Rows[0]["PayDate"].ToString());
            txtDueDt.Text = TextLib.MakeDateEightDigit(dsReturn.Rows[0]["DueDate"].ToString());
            txtHoursOver.Text = dsReturn.Rows[0]["HoursOver"].ToString();
            txtUnitPrice.Text = dsReturn.Rows[0]["UnitPrice"].ToString();
            txtSquare.Text = dsReturn.Rows[0]["Square"].ToString() == "0.00" ? "" : dsReturn.Rows[0]["Square"].ToString();
            txtDisCount.Text = dsReturn.Rows[0]["Discount"].ToString();
            ddlPaymentType.SelectedValue = dsReturn.Rows[0]["PaymentType"].ToString();
            txtExchangeRate.Text = dsReturn.Rows[0]["ExchangeRate"].ToString();
            chkIncludeVat.Checked = dsReturn.Rows[0]["IncludeVAT"].ToString() == "Y";
            lnkbtnDelete.Visible = string.IsNullOrEmpty(dsReturn.Rows[0]["InvoiceNo"].ToString());
            lnkbtnWrite.Visible = string.IsNullOrEmpty(dsReturn.Rows[0]["InvoiceNo"].ToString());
            txtSubDes.Text = dsReturn.Rows[0]["SubDes"].ToString();


        }
    }
}