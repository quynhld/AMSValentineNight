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

using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class DebitDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack) return;
                if (!CheckParams()) return;
                InitControls();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["seq"] != null)
            {
                hfSeq.Value = Request.Params["seq"];
                isReturnOk = true;
                if (!string.IsNullOrEmpty(Request.Params["RentCd"]))
                {
                    hfRentCd.Value = Request.Params["RentCd"];

                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltRoomNo.Text = TextNm["ROOMNO"];

        }

        protected void LoadData()
        {

            // KN_USP_SELECT_DEBIT_LIST_DETAIL_S00
            var dsReturn = BalanceMngBlo.SpreadDetListDetail(hfSeq.Value);
            if (dsReturn == null) return;
            if (dsReturn.Tables[0] == null) return;
            if (dsReturn.Tables[0].Rows.Count <= CommValue.NUMBER_VALUE_0) return;
            ltRoomNo.Text = dsReturn.Tables[0].Rows[0]["RoomNo"].ToString();
            ltCompNm.Text = dsReturn.Tables[0].Rows[0]["TenantNm"].ToString();
            txtSUsingDt.Text = TextLib.MakeDateEightDigit(dsReturn.Tables[0].Rows[0]["StartDt"].ToString());
            txtEUsingDt.Text = TextLib.MakeDateEightDigit(dsReturn.Tables[0].Rows[0]["EndDt"].ToString());
            txtPayDt.Text = TextLib.MakeDateEightDigit(dsReturn.Tables[0].Rows[0]["PaymentDt"].ToString());
            txtIssueDt.Text = TextLib.MakeDateEightDigit(dsReturn.Tables[0].Rows[0]["IssuingDt"].ToString());
            txtFeeAmount.Text = double.Parse(dsReturn.Tables[0].Rows[0]["MonthViAmtNo"].ToString()).ToString("###,##0.##");
            txtExchangeRate.Text = double.Parse(dsReturn.Tables[0].Rows[0]["DongToDollar"].ToString()).ToString("###,##0.##");
            txtTotal.Text = double.Parse(dsReturn.Tables[0].Rows[0]["RealMonthViAmtNo"].ToString()).ToString("###,##0");
            hfItemRentCd.Value = dsReturn.Tables[0].Rows[0]["RentCd"].ToString();
            hfFeeTy.Value = dsReturn.Tables[0].Rows[0]["FeeTy"].ToString();
            txtDiscount.Text = double.Parse(dsReturn.Tables[0].Rows[0]["Discount"].ToString()).ToString("###,##0.##");
            lnkbtnWrite.Visible = string.IsNullOrEmpty(dsReturn.Tables[0].Rows[0]["InvoiceNo"].ToString());
            txtSubDes.Text = dsReturn.Tables[0].Rows[0]["SubDes"].ToString();
        }


        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            var feeAmount = double.Parse(txtFeeAmount.Text.Replace(",", ""));
            var exRate = double.Parse(txtExchangeRate.Text.Replace(",", ""));
            var sDt = txtSUsingDt.Text.Replace("-", "");
            var eDt = txtEUsingDt.Text.Replace("-", "");
            var payDt = txtPayDt.Text.Replace("-", "");
            var issueDt = txtIssueDt.Text.Replace("-", "");
            var roomNo = ltRoomNo.Text;
            var rentCd = hfItemRentCd.Value;
            var feeTy = hfFeeTy.Value;
            var discount = double.Parse(txtDiscount.Text.Replace(",", ""));
            var total = double.Parse(txtTotal.Text.Replace(",", ""));
            var subDes = txtSubDes.Text;
            try
            {
                var objReturn = BalanceMngBlo.ModifyDebit(rentCd, feeTy, roomNo, hfSeq.Value, sDt, eDt, payDt, issueDt, exRate, feeAmount, discount, total, subDes);
                if ((bool)objReturn[0])
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
            Response.Redirect("ManuallySettingPrintList.aspx?RentCd="+hfRentCd.Value, CommValue.AUTH_VALUE_FALSE);
        }
    }
}
