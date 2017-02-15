using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class ManuallySettingCreated : BasePage
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
                if (!string.IsNullOrEmpty(Request.Params["RentCd"].ToString()))
                {
                    txtHfRentCd.Text = Request.Params["RentCd"].ToString();
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

            //ltItem.Text = TextNm[""];
            if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                ltItem.Text = "항목";
            }
            else
            {
                ltItem.Text = "Item";
            }
           // ltMonth.Text = TextNm["MONTH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + CommValue.TENANTTY_VALUE_CORPORATION + "','" + CommValue.TERM_VALUE_LONGTERM + "','" + CommValue.TERM_VALUE_SHORTTERM + "');";
            MakeItemDdl();

        }

        protected void LoadData()
        {
            var payCode = ddlItems.SelectedValue;
            var strYear = txtSearchDt.Text.Replace("-", "").Substring(0, 4);
            var strMonth = txtSearchDt.Text.Replace("-", "").Substring(4, 2);
            var strToYear = txtSearchDtTo.Text.Replace("-", "").Substring(0, 4);
            var strToMonth = txtSearchDtTo.Text.Replace("-", "").Substring(4, 2);
            var isPrinted = rbIsPrinted.SelectedValue;
            //KN_USP_AGT_MAKE_RENTFEE_CREATED_DEBIT_LIST_M00
            var dtReturn = MngPaymentBlo.SpreadManuallyCreatedDebitList(payCode, strYear, strMonth, strToYear, strToMonth, isPrinted,txtHfRentCd.Text);
            if (dtReturn == null) return;
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
        }



        protected void MakeItemDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItems.Items.Clear();

            ddlItems.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                if (dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_MNGFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_PARKINGFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_PARKINGCARDFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE))
                {
                    ddlItems.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        protected void lnkbtnMakeLine_Click(object sender, EventArgs e)
        {
            try
            {
                object[] objReturn = new object[2];


                // objReturn = MngPaymentBlo.RegistryManuallyEveryFeeRegistList(txtHfRentCd.Text, ddlYear.SelectedValue, ddlMonth.SelectedValue);

                if (objReturn != null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('" + AlertNm["INFO_MAKE_BILLING"] + "')", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('" + AlertNm["INFO_NOT_MAKE_BILLING"] + "')", CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
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
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPrintoutList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                // HtmlTableCell tdChk = (HtmlTableCell)iTem.FindControl("tdChk");
                 CheckBox chkboxList = (CheckBox)iTem.FindControl("chkboxList");

                TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");

              //  TextBox txthfRentSeq = (TextBox)iTem.FindControl("txthfRentSeq");
                //TextBox txthfContractNo = (TextBox)iTem.FindControl("txthfContractNo");

                //chkboxList.Visible = CommValue.AUTH_VALUE_TRUE;                
              // intInit = CommValue.NUMBER_VALUE_0;

              //  txthfRentSeq.Text = drView["RentSeq"].ToString();
               // txthfContractNo.Text = drView["ContractNo"].ToString();



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

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    var txtDebitCode = (TextBox)iTem.FindControl("txtDebitCode");
                    txtDebitCode.Text = drView["RentCd"].ToString();
                }
                if (!string.IsNullOrEmpty(drView["DebitType"].ToString()))
                {
                    var ltDebitType = (Literal)iTem.FindControl("ltDebitType");
                    ltDebitType.Text = drView["DebitType"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = drView["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
                {
                    var ltTenantNm = (Literal)iTem.FindControl("ltTenantNm");
                    ltTenantNm.Text = drView["TenantNm"].ToString();
                }
                if (!string.IsNullOrEmpty(drView["ISS_START_DATE"].ToString()))
                {
                    var ltUsingPeriod = (Literal)iTem.FindControl("ltUsingPeriod");
                    ltUsingPeriod.Text = TextLib.MakeDateEightDigit(drView["ISS_START_DATE"].ToString()) + "~" + TextLib.MakeDateEightDigit(drView["ISS_END_DATE"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["MonthAmtNo"].ToString()))
                {
                    var ltMonthFee = (Literal)iTem.FindControl("ltMonthFee");
                    ltMonthFee.Text = drView["MonthAmtNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["LeasingArea"].ToString()))
                {
                    var ltLeasingArea = (Literal)iTem.FindControl("ltLeasingArea");
                    ltLeasingArea.Text = drView["LeasingArea"].ToString();                    
                }
                if (!string.IsNullOrEmpty(drView["DB_PRINT_YN"].ToString()))
                {
                    var ltPrintStatus = (Literal)iTem.FindControl("ltPrintStatus");
                    ltPrintStatus.Text = drView["DB_PRINT_YN"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RealMonthViAmtNo"].ToString()))
                {
                    var txthfRealMonthViAmtNo = (TextBox)iTem.FindControl("txthfRealMonthViAmtNo");
                    txthfRealMonthViAmtNo.Text = drView["RealMonthViAmtNo"].ToString();
                }
                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    var txthfFloor = (TextBox)iTem.FindControl("txthfFloor");
                    txthfFloor.Text = drView["FloorNo"].ToString();
                }
                if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
                {
                    var txthfDongtoDollar = (TextBox)iTem.FindControl("txthfDongtoDollar");
                    txthfDongtoDollar.Text = drView["DongToDollar"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ISS_BUNDLE_SEQ"].ToString()))
                {
                    var txthfBundleSeqNo = (TextBox)iTem.FindControl("txthfBundleSeqNo");
                    txthfBundleSeqNo.Text = drView["ISS_BUNDLE_SEQ"].ToString();                   
                }



                if (!string.IsNullOrEmpty(drView["ISSUING_DATE"].ToString()))
                {
                    var txtIssuingDate = (TextBox)iTem.FindControl("txtIssuingDate");
                    txtIssuingDate.Text = TextLib.MakeDateEightDigit(drView["ISSUING_DATE"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["PAYMENT_DATE"].ToString()))
                {
                    var txtHfPaymentDate = (TextBox)iTem.FindControl("txtHfPaymetDt");
                    txtHfPaymentDate.Text = TextLib.MakeDateEightDigit(drView["PAYMENT_DATE"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ISS_START_DATE"].ToString()))
                {
                    var txthfStartUsingDt = (TextBox)iTem.FindControl("txthfStartUsingDt");
                    txthfStartUsingDt.Text = drView["ISS_START_DATE"].ToString();
                }
                if (!string.IsNullOrEmpty(drView["ISS_END_DATE"].ToString()))
                {
                    var txthfEndUsingDt = (TextBox)iTem.FindControl("txthfEndUsingDt");
                    txthfEndUsingDt.Text = drView["ISS_END_DATE"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ContractName"].ToString()))
                {
                    var ltConTract = (Literal)iTem.FindControl("ltConTract");
                    ltConTract.Text = drView["ContractName"].ToString();
                }
                if (!string.IsNullOrEmpty(drView["Address"].ToString()))
                {
                    var ltAddress = (Literal)iTem.FindControl("ltAddress");
                    ltAddress.Text = drView["Address"].ToString();
                }
                if (!string.IsNullOrEmpty(drView["TaxCode"].ToString()))
                {
                    var ltTaxCode = (Literal)iTem.FindControl("ltTaxCode");
                    ltTaxCode.Text = drView["TaxCode"].ToString();
                }

            }
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            var isAllCheck = chkAll.Checked;

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
            CheckBox cb = (CheckBox)sender;
            ListViewItem item = (ListViewItem)cb.NamingContainer;
            ListViewDataItem dataItem = (ListViewDataItem)item;
            //string code = li.DataKeys[dataItem.DisplayIndex].Value.ToString();
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void lbtNotCreated_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text , CommValue.AUTH_VALUE_FALSE);
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

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            foreach (var t in lvPrintoutList.Items)
            {
                if (!((CheckBox)t.FindControl("chkboxList")).Checked) continue;
                var rentCode = txtHfRentCd.Text;
                var userSeq = ((TextBox)t.FindControl("txtHfUserSeq")).Text;
                var feeTy = ((TextBox)t.FindControl("txthfFeeTypeCode")).Text;
               // var strYear = ((Literal)t.FindControl("ltCreatedDate")).Text.Substring(0,4);
               // var strMonth = ((Literal)t.FindControl("ltCreatedDate")).Text.Substring(5, 2);
                var strBundleSeq = ((TextBox)t.FindControl("txthfBundleSeqNo")).Text;
                var dtReturn = MngPaymentBlo.CancelCreatedList(rentCode, userSeq, feeTy, "", "", strBundleSeq);
                LoadData();
            }
        }

        protected void lnkDividualBilling_Click(object sender, EventArgs e)
        {
            foreach (var t in lvPrintoutList.Items)
            {
                var strRentCd = txtHfRentCd.Text;
                const int terMonth = 1;
                if (!((CheckBox)t.FindControl("chkboxList")).Checked) continue;

                var userSeq = ((TextBox)t.FindControl("txtHfUserSeq")).Text;
                var strFeeTy = ((TextBox)t.FindControl("txthfFeeTypeCode")).Text;
                var debitName = ((Literal)t.FindControl("ltDebitType")).Text;
                var floorNo = ((TextBox)t.FindControl("txthfFloor")).Text;
                var roomNo = ((Literal)t.FindControl("ltInsRoomNo")).Text;
                var leasingArea = ((Literal)t.FindControl("ltLeasingArea")).Text;
                var tenantNm = ((Literal)t.FindControl("ltTenantNm")).Text;
                var dongToDollar = ((TextBox)t.FindControl("txthfDongtoDollar")).Text;
                var monthViAmtNo = ((Literal)t.FindControl("ltMonthFee")).Text;
                var realMonthViAmtNo = ((TextBox)t.FindControl("txthfRealMonthViAmtNo")).Text;
                var sDate = ((TextBox)t.FindControl("txthfStartUsingDt")).Text;
                var edate = ((TextBox)t.FindControl("txthfEndUsingDt")).Text;
                const string unPaidAmount = "0.00";
                var refUserSeq = ((TextBox)t.FindControl("txtHfUserSeq")).Text;
                var refYear = ((TextBox)t.FindControl("txthfEndUsingDt")).Text;
                var refMonth = ((TextBox)t.FindControl("txthfEndUsingDt")).Text;
                var strBundleSeq = ((TextBox)t.FindControl("txthfBundleSeqNo")).Text;
                var strIssuingDate = ((TextBox)t.FindControl("txtIssuingDate")).Text.Replace("-","");
                var strPaymentDate = ((TextBox)t.FindControl("txtHfPaymetDt")).Text.Replace("-", "");
                var strMembNo = Session["MemNo"].ToString();
                //KN_USP_INSERT_DEBIT_LIST_I00
                var dtReturn = MngPaymentBlo.MakeMergeIndividualBilling(userSeq, strRentCd, strFeeTy, debitName, int.Parse(floorNo), roomNo, double.Parse(leasingArea), tenantNm, terMonth, double.Parse(dongToDollar), double.Parse(monthViAmtNo), double.Parse(realMonthViAmtNo), sDate, edate, double.Parse(unPaidAmount), refUserSeq, refYear, refMonth, strBundleSeq, strIssuingDate, strMembNo, strPaymentDate);
            }
            LoadData();

        }

        protected void lnkbtnMergeBilling_Click(object sender, EventArgs e)
        {
            var strRentCd = txtHfRentCd.Text;
            var terMonth = 0;
            var userSeq = "";
            var strFeeTy = "";
            var debitName = "";
            var floorNo = "";
            var roomNo = "";
            var leasingArea = "";
            var tenantNm = "";
            var dongToDollar = "";
            var monthViAmtNo = 0.00;
            var realMonthViAmtNo = 0.00;
            var sDate = "";
            var edate = "";
            const string unPaidAmount = "0.00";
            var refUserSeq = "";
            var refYear = "";
            var refMonth = "";
            var strBundleSeq = "";
            var strIssuingDate = "";
            var strPaymentDate = "";
            var strMembNo = Session["MemNo"].ToString();
            foreach (var t in lvPrintoutList.Items)
            {
                if (!((CheckBox)t.FindControl("chkboxList")).Checked) continue;

                 userSeq = ((TextBox)t.FindControl("txtHfUserSeq")).Text;
                 strFeeTy = ((TextBox)t.FindControl("txthfFeeTypeCode")).Text;
                 debitName = ((Literal)t.FindControl("ltDebitType")).Text;
                 floorNo = ((TextBox)t.FindControl("txthfFloor")).Text;
                 roomNo = ((Literal)t.FindControl("ltInsRoomNo")).Text;
                 leasingArea = ((Literal)t.FindControl("ltLeasingArea")).Text;
                 tenantNm = ((Literal)t.FindControl("ltTenantNm")).Text;
                 dongToDollar = ((TextBox)t.FindControl("txthfDongtoDollar")).Text;
                 monthViAmtNo += double.Parse(((Literal)t.FindControl("ltMonthFee")).Text);
                 realMonthViAmtNo += double.Parse(((TextBox)t.FindControl("txthfRealMonthViAmtNo")).Text);
                 strBundleSeq = ((TextBox)t.FindControl("txthfBundleSeqNo")).Text;
                 strIssuingDate = ((TextBox)t.FindControl("txtIssuingDate")).Text.Replace("-", "");
                 strPaymentDate = ((TextBox)t.FindControl("txtHfPaymetDt")).Text.Replace("-", "");
                terMonth++;
                if (terMonth==1)
                {
                    sDate = ((Literal)t.FindControl("txthfStartUsingDt")).Text;
                    edate = ((Literal)t.FindControl("txthfEndUsingDt")).Text;
                    refUserSeq = ((TextBox)t.FindControl("txtHfUserSeq")).Text;
                    refYear = ((Literal)t.FindControl("txthfEndUsingDt")).Text.Replace("/", "");
                    refMonth = ((Literal)t.FindControl("txthfEndUsingDt")).Text.Replace("/", "");
                }
                else
                {
                    edate = ((Literal)t.FindControl("txthfEndUsingDt")).Text;
                    refUserSeq += ","+((TextBox)t.FindControl("txtHfUserSeq")).Text;
                    refYear += "," + ((Literal)t.FindControl("txthfEndUsingDt")).Text.Replace("/", "");
                    refMonth += "," + ((Literal)t.FindControl("txthfEndUsingDt")).Text.Replace("/", "");
                }
            }
            if (terMonth <= 0) return;
            //KN_USP_INSERT_DEBIT_LIST_I00
            var dtReturn = MngPaymentBlo.MakeMergeIndividualBilling(userSeq, strRentCd, strFeeTy, debitName, int.Parse(floorNo), roomNo, double.Parse(leasingArea), tenantNm, terMonth, double.Parse(dongToDollar), monthViAmtNo, realMonthViAmtNo, sDate, edate, double.Parse(unPaidAmount), refUserSeq, refYear, refMonth, strBundleSeq, strIssuingDate, strMembNo, strPaymentDate);
            LoadData();
        }


    }
}
