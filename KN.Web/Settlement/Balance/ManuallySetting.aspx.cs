using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Manage.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class ManuallySetting : BasePage
    {
        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CheckParam();

                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params["RentCd"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["RentCd"]))
                {
                    txtHfRentCd.Text = Request.Params["RentCd"];
                }
                else
                {
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_APTSHOP;
                }
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APTSHOP;
            }
        }

        protected void InitControls()
        {
            if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                ltItem.Text = "항목";
            }
            else
            {
                ltItem.Text = "Item";
            }
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + CommValue.TENANTTY_VALUE_CORPORATION + "','" + CommValue.TERM_VALUE_LONGTERM + "','" + CommValue.TERM_VALUE_SHORTTERM + "');";
            MakeItemDdl();

            DateTime today = DateTime.Today;            
            DateTime lastday = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(-1);
            txtSSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 7)+ "-01";
            txtESearchDt.Text = lastday.ToString("s").Substring(0, 10);

        }

        protected void LoadData()
        {                 
            var payCode = ddlItems.SelectedValue;
            var strYear = txtSearchDt.Text.Replace("-", "").Substring(0, 4);// DateTime.Now.ToString("yyyy");
            var strMonth = txtSearchDt.Text.Replace("-", "").Substring(4, 2);//DateTime.Now.ToString("MM");
            var strDay = DateTime.Now.ToString("dd");
            var strtoYear = strYear;
            var strtoMonth = strMonth;
            var strtoDay = strDay;
            var roomNo = txtRoomNo.Text;
            var issStartDt = txtSSearchDt.Text.Replace("-", "").Substring(0, 8);
            var issEndDt = txtESearchDt.Text.Replace("-", "").Substring(0, 8);
            var dtReturn = MngPaymentBlo.SpreadManuallyRegistList(payCode, txtHfRentCd.Text, strYear, strMonth, strDay, strtoYear, strtoMonth, strtoDay, roomNo, issStartDt, issEndDt);
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
        }


        protected void MakeItemDdl()
        {

           var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItems.Items.Clear();

            ddlItems.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (var dr in dtReturn.Select())
            {
                if (dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_MNGFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE))
                {
                    ddlItems.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }


        protected void lvPrintoutList_LayoutCreated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPrintoutList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
                CloseLoading();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPrintoutList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");

            var txthfRentSeq = (TextBox)iTem.FindControl("txthfRentSeq");
            var txthfContractNo = (TextBox)iTem.FindControl("txthfContractNo");

            txthfRentSeq.Text = drView["RentSeq"].ToString();
            txthfContractNo.Text = drView["ContractNo"].ToString();

            if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
            {
                txtHfUserSeq.Text = drView["UserSeq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["FeeTyName"].ToString()))
            {
                var ltFeeType = (Literal)iTem.FindControl("ltFeeType");
                ltFeeType.Text = drView["FeeTyName"].ToString();
            }
            if (!string.IsNullOrEmpty(drView["FeeTy"].ToString()))
            {
                var txthfFeeTypeCode = (TextBox)iTem.FindControl("txthfFeeTypeCode");
                txthfFeeTypeCode.Text = drView["FeeTy"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["UnitPrice"].ToString()))
            {
                var txthfUnitPrice = (TextBox)iTem.FindControl("txthfUnitPrice");
                txthfUnitPrice.Text = drView["UnitPrice"].ToString();
            }
                

            if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
            {
                var txthfRentCd = (TextBox)iTem.FindControl("txthfRentCd");
                txthfRentCd.Text = drView["RentCd"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                ltInsRoomNo.Text = drView["RoomNo"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
            {
                var ltTenantNm = (Literal)iTem.FindControl("ltTenantNm");
                ltTenantNm.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["PAYCYCLE"].ToString()))
            {
                var txthfPayCycle = (TextBox)iTem.FindControl("txthfPayCycle");
                txthfPayCycle.Text = TextLib.StringDecoder(drView["PAYCYCLE"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["START_USING_DATE"].ToString()))
            {
                var txthfStartUsingDt = (TextBox)iTem.FindControl("txthfStartUsingDt");
                txthfStartUsingDt.Text = drView["START_USING_DATE"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["END_USING_DATE"].ToString()))
            {
                var txthfEndUsingDt = (TextBox)iTem.FindControl("txthfEndUsingDt");
                txthfEndUsingDt.Text = drView["END_USING_DATE"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
            {
                var txtDongToDollar = (TextBox)iTem.FindControl("txtDongToDollar");
                txtDongToDollar.Text = drView["DongToDollar"].ToString();
            }
            if (!string.IsNullOrEmpty(drView["RentLeasingArea"].ToString()))
            {
                var txthfLeasingArea = (TextBox)iTem.FindControl("txthfLeasingArea");
                txthfLeasingArea.Text = double.Parse(drView["RentLeasingArea"].ToString()).ToString(CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(drView["VatRatio"].ToString()))
            {
                var txthfVatRatio = (TextBox)iTem.FindControl("txthfVatRatio");
                txthfVatRatio.Text = double.Parse(drView["VatRatio"].ToString()).ToString();                   
            }
            if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
            {
                var txthfFloor = (TextBox)iTem.FindControl("txthfFloor");
                txthfFloor.Text = drView["FloorNo"].ToString();
            }
            if (!string.IsNullOrEmpty(drView["PAY_DATE"].ToString()))
            {
                var txthfPayDate = (TextBox)iTem.FindControl("txthfPayDate");
                txthfPayDate.Text = drView["PAY_DATE"].ToString();
            }
            if (!string.IsNullOrEmpty(drView["PAYCYCLE_TYPE"].ToString()))
            {
                var txthfPayCycleType = (TextBox)iTem.FindControl("txthfPayCycleType");
                txthfPayCycleType.Text = drView["PAYCYCLE_TYPE"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["ISSUING_DATE"].ToString()))
            {
                var ltIssuingDate = (Literal)iTem.FindControl("ltIssuingDate");
                ltIssuingDate.Text = TextLib.MakeDateEightDigit(drView["ISSUING_DATE"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["USING_PERIOD"].ToString()))
            {
                var ltUsingPeriod = (Literal)iTem.FindControl("ltUsingPeriod");
                ltUsingPeriod.Text = drView["USING_PERIOD"].ToString();
            }

            var ltFeeAmt = (Literal)iTem.FindControl("ltFeeAmt");
            var ltVatAmt = (Literal)iTem.FindControl("ltVATAmt");
            var ltTotal = (Literal)iTem.FindControl("LtTotal");
            if (!string.IsNullOrEmpty(drView["FeeAmount"].ToString()))
            {
                ltFeeAmt.Text = (double.Parse(drView["FeeAmount"].ToString())/ double.Parse(drView["DongToDollar"].ToString())).ToString("###,##0.##") ;
            }

            if (!string.IsNullOrEmpty(drView["VATRatio"].ToString()))
            {
                ltVatAmt.Text = (double.Parse(ltFeeAmt.Text) * 0.1).ToString("###,##0.##");
                ltTotal.Text = (double.Parse(drView["FeeAmount"].ToString()) * 1.1).ToString("###,##0.##");
            }
            else
            {
                ltTotal.Text = ltFeeAmt.Text;//double.Parse(drView["FeeAmount"].ToString()).ToString("###,##0.##0");
            }

            var ltExRate = (Literal)iTem.FindControl("ltExRate");
            if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
            {
                ltExRate.Text = double.Parse(drView["DongToDollar"].ToString()).ToString("###,##0");
            }
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
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

        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void lnkCreatedNote_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkPrintList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REFLECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkMakeDebitNote_Click(object sender, EventArgs e)
        {
            try
            {
                var objReturn = new object[2];
                var i = 0;
                foreach (var t in lvPrintoutList.Items)
                {
                    if (!((CheckBox) t.FindControl("chkboxList")).Checked) continue;
                    i++;
                    var rentFeePayAmt = double.Parse(((Literal)t.FindControl("ltFeeAmt")).Text.Replace(",",""));
                    var unitPrice = double.Parse(((TextBox)t.FindControl("txthfUnitPrice")).Text.Replace(",", ""));
                    var total = double.Parse(((Literal)t.FindControl("LtTotal")).Text.Replace(",", ""));
                    var rentCd = ((TextBox)t.FindControl("txthfRentCd")).Text;
                    var rentSeq = Int32.Parse(((TextBox)t.FindControl("txthfRentSeq")).Text);
                    var paymentDt = ((TextBox)t.FindControl("txthfPayDate")).Text;
                    var userSeq = ((TextBox)t.FindControl("txtHfUserSeq")).Text;
                    var roomNo = ((Literal)t.FindControl("ltInsRoomNo")).Text;
                    var floorNo = Int32.Parse(((TextBox)t.FindControl("txthfFloor")).Text);
                    var feeType = ((TextBox)t.FindControl("txthfFeeTypeCode")).Text;
                    var tenantNm = ((Literal)t.FindControl("ltTenantNm")).Text;
                    var leasingAre = double.Parse(((TextBox)t.FindControl("txthfLeasingArea")).Text);
                    var startUsingDt = ((TextBox)t.FindControl("txthfStartUsingDt")).Text.Replace("-", "");
                    var endtUsingDt = ((TextBox)t.FindControl("txthfEndUsingDt")).Text.Replace("-", "");
                    var payCycle = Int32.Parse(((TextBox)t.FindControl("txthfPayCycle")).Text);
                    var dongToDollar =double.Parse(((TextBox)t.FindControl("txtDongToDollar")).Text);
                    var startIsueDt = ((Literal)t.FindControl("ltIssuingDate")).Text.Replace("-", "");
                    var intRentSeq = ((TextBox)t.FindControl("txthfRentSeq")).Text;
                    var insMemNo = Session["MemNo"].ToString();
                    var insMemIP = Session["UserIP"].ToString();
                    objReturn = MngPaymentBlo.CreateDebitNote(userSeq, rentCd, rentSeq, feeType, paymentDt, floorNo, roomNo, leasingAre, tenantNm, payCycle, dongToDollar, rentFeePayAmt, startUsingDt, endtUsingDt, startIsueDt, unitPrice, total, Int32.Parse(intRentSeq),insMemNo,insMemIP);
                }
                CloseLoading();
                if (objReturn != null && i>0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Successful !')", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Please choose debit!')", CommValue.AUTH_VALUE_TRUE);
                    return;
                }
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnLoadData_Click(object sender, ImageClickEventArgs e)
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
        protected void CloseLoading()
        {
            var sbWarning = new StringBuilder();
            sbWarning.Append("CloseLoading();");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);            
        }
    }
}
