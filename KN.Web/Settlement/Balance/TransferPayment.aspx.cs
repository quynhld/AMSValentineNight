﻿using System;
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
using KN.Resident.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class TransferPayment : BasePage
    {
        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (IsPostBack) return;
                if (CheckParams())
                {
                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
                {
                    hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {

            // DropDownList Setting
            // 년도
            // 수납 아이템
            MakePaymentDdl();
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltAmount.Text = TextNm["PAYMENT"];
            txtESearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";
        }

        protected void LoadData()
        {
            var sPayDt = txtSearchDt.Text.Replace("-", "");
            var eDt = txtESearchDt.Text.Replace("-", "");
            // KN_USP_SET_SELECT_APT_HOADONINFO_S06
            var dtReturn = ReceiptMngBlo.SelectPaymentTowerForTransfer(hfRentCd.Value, txtRoomNo.Text, sPayDt, eDt,
                                                                        ddlItemCd.SelectedValue, txtCompanyNm.Text, rbIsTransfer.SelectedValue);
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
            chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
            if (dtReturn == null || dtReturn.Rows.Count<=0 || rbIsTransfer.SelectedValue.Equals(CommValue.CHOICE_VALUE_YES))
            {
                lnkbtnIssuing.Visible = false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void MakePaymentDdl()
        {

            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItemCd.Items.Clear();

            ddlItemCd.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (var dr in dtReturn.Select())
            {
                if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT))
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE))
                    {
                        ddlItemCd.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
                else
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE))
                    {
                        ddlItemCd.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
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
                ClodeLoading();
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
            var txtHfPrintDetSeq = (TextBox)iTem.FindControl("txtHfPrintDetSeq");
            var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");


            var txtHfRefSeq = (TextBox)iTem.FindControl("txtHfRefSeq");
            if (!string.IsNullOrEmpty(drView["P_Seq"].ToString()))
            {
                txtHfRefSeq.Text = drView["P_Seq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["seq"].ToString()))
            {
                txtHfPrintDetSeq.Text = drView["seq"].ToString();
            }

            var txtHfRefPrintNo = (TextBox)iTem.FindControl("txtHfRefPrintNo");
            if (!string.IsNullOrEmpty(drView["Ref_PrintNo"].ToString()))
            {
                txtHfRefPrintNo.Text = drView["Ref_PrintNo"].ToString();
            }

            var txthfBillTy = (TextBox)iTem.FindControl("txthfBillTy");
            if (!string.IsNullOrEmpty(drView["Bill_Type"].ToString()))
            {
                txthfBillTy.Text = drView["Bill_Type"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
            {
                txtHfUserSeq.Text = drView["UserSeq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["Period"].ToString()))
            {
                var ltPeriod = (Literal)iTem.FindControl("ltPeriod");
                ltPeriod.Text = drView["Period"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            var ltInsBillNm = (Literal)iTem.FindControl("ltInsBillNm");

            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                ltInsBillNm.Text = TextLib.StringDecoder(drView["FeeNm"].ToString());
            }

            var txtHfBillCd = (TextBox)iTem.FindControl("txtHfBillCd");

            if (!string.IsNullOrEmpty(drView["BillCd"].ToString()))
            {
                txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
            }

            var txthfTenantNm = (TextBox)iTem.FindControl("txthfTenantNm");

            if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
            {
                txthfTenantNm.Text = TextLib.StringDecoder(drView["UserNm"].ToString());
            }

            var txthfFeeTyDt = (TextBox)iTem.FindControl("txthfFeeTyDt");
            if (!string.IsNullOrEmpty(drView["BillCdDetail"].ToString()))
            {
                txthfFeeTyDt.Text = drView["BillCdDetail"].ToString();
            }

            var txtHfInvoiceNo = (TextBox)iTem.FindControl("txtHfInvoiceNo");

            if (!string.IsNullOrEmpty(drView["P_INVOICE"].ToString()))
            {
                txtHfInvoiceNo.Text = TextLib.StringDecoder(drView["P_INVOICE"].ToString());
            }

            hfBillCd.Value = txtHfBillCd.Text;

            if (!string.IsNullOrEmpty(drView["Amount"].ToString()))
            {
                var ltTotalAmt = (Literal)iTem.FindControl("ltTotalAmt");
                ltTotalAmt.Text = TextLib.MakeVietIntNo(double.Parse(drView["Amount"].ToString()).ToString("###,##0"));                  
            }

            var ltPaidDate = (Literal)iTem.FindControl("ltPaidDate");
            if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
            {
                ltPaidDate.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());                      
            }

            var ltPaymentTy = (Literal)iTem.FindControl("ltPaymentTy");
            if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
            {
                ltPaymentTy.Text = drView["PaymentNm"].ToString();
            }

            var ltDebitAcc = (Literal)iTem.FindControl("ltDebitAcc");
            if (!string.IsNullOrEmpty(drView["DebitCd"].ToString()))
            {
                ltDebitAcc.Text = drView["DebitCd"].ToString();
            }

            var chkboxList = (CheckBox)iTem.FindControl("chkboxList");
            if (rbIsTransfer.SelectedValue.Equals(CommValue.CHOICE_VALUE_YES))
            {
                chkboxList.Visible = false;
            }
            var ltSlipNo = (Literal)iTem.FindControl("ltSlipNo");
            if (!string.IsNullOrEmpty(drView["SlipNo"].ToString()))
            {
                ltSlipNo.Text = drView["SlipNo"].ToString();
                chkboxList.Visible = false;
            }
            var ltCreditAcc = (Literal)iTem.FindControl("ltCreditAcc");
            if (!string.IsNullOrEmpty(drView["CreditCd"].ToString()))
            {
                ltCreditAcc.Text = drView["CreditCd"].ToString();
            }
            intRowsCnt++;
        }

        /// <summary>
        /// 출력버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {
            var intCheckRow = CommValue.NUMBER_VALUE_0;
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var sbWarning = new StringBuilder();
                sbWarning.Append("CloseLoading();alert('");
                var isOk = true;
                if (lvPrintoutList.Items.Count <= 0)
                {
                    ClodeLoading();
                    Aleart("No data for transfer");
                    return;
                }
                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;
                    intCheckRow++;
                    var invoiceNO = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfInvoiceNo")).Text;
                    var compNo = Session["CompCd"].ToString();
                    var rentCode = hfRentCd.Value;
                    var feeTy = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfBillCd")).Text;
                    var paymentCode = "";
                    var userSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfUserSeq")).Text;
                    var tenantsNm = ((TextBox)lvPrintoutList.Items[i].FindControl("txthfTenantNm")).Text;
                    var description = "";
                    var roomNo = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsRoomNo")).Text;
                    var netAmount = "0";
                    var vatAmount = "0";
                    var totAmount = ((Literal)lvPrintoutList.Items[i].FindControl("ltTotalAmt")).Text.Replace(".","");
                    var sendType = "PAYMENT";
                    var memIP = Session["UserIP"].ToString();                    
                    var paymentDt = ((Literal)lvPrintoutList.Items[i].FindControl("ltPaidDate")).Text.Replace("-","");
                    var insMemNo = Session["MemNo"].ToString();
                    var exchangeRate = "1";
                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    var billType = ((TextBox)lvPrintoutList.Items[i].FindControl("txthfBillTy")).Text;
                    var seq = Int32.Parse(((TextBox)lvPrintoutList.Items[i].FindControl("txtHfPrintDetSeq")).Text);
                    var feeTyDt = ((TextBox)lvPrintoutList.Items[i].FindControl("txthfFeeTyDt")).Text;

                    var debitAcc = ((Literal)lvPrintoutList.Items[i].FindControl("ltDebitAcc")).Text;
                    var creditAcc = ((Literal)lvPrintoutList.Items[i].FindControl("ltCreditAcc")).Text;
                    var vatAcc = "";

                    var dblInsExchageRate = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(exchangeRate))
                    {
                        dblInsExchageRate = double.Parse(exchangeRate);
                    }

                    var dbnetAmount = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(netAmount))
                    {
                        dbnetAmount = double.Parse(netAmount);
                    }
                    var dbvatAmount = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(vatAmount))
                    {
                        dbvatAmount = double.Parse(vatAmount);
                    }
                    var dbytotAmount = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(totAmount))
                    {
                        dbytotAmount = double.Parse(totAmount);
                    }
                    var dsReturn = MngPaymentBlo.TransferStatement(invoiceNO, compNo, rentCode, feeTy, paymentCode, userSeq, tenantsNm, description,
                                                          roomNo, dbnetAmount, dbvatAmount, dbytotAmount, sendType, paymentDt, insMemNo, dblInsExchageRate, refSeq, billType, memIP, debitAcc, creditAcc, vatAcc, feeTyDt, seq);
                    if (dsReturn.Tables.Count > 0)
                    {
                        if (dsReturn.Tables[0].Rows[0][0].ToString().Equals("Y"))
                        {

                        }
                        else
                        {
                            isOk = false;
                            sbWarning.Append(dsReturn.Tables[0].Rows[0][1] + " on room " + roomNo);
                            break;
                        }
                    }
                    else
                    {
                        isOk = false;
                        sbWarning.Append("Check error on room " + roomNo);
                        break;
                    }
                }
                if (isOk)
                {
                    sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
                }
                else
                {
                    if (intCheckRow <= 0)
                    {
                        ClodeLoading();
                        Aleart("Please choose payment by checkbox to transfer !");
                        return;
                    }                    
                }
                sbWarning.Append("');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                LoadData();
            }
            catch (Exception ex)
            {
                ClodeLoading();
                ErrLogger.MakeLogger(ex);
            }
        }


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

        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            hfUserSeq.Value = ((TextBox)e.Item.FindControl("txtHfUserSeq")).Text;
            hfPayDt.Value = ((TextBox)e.Item.FindControl("txtInsRegDt")).Text;
        }

        protected void ClodeLoading()
        {
            var sbWarning = new StringBuilder();
            sbWarning.Append("CloseLoading();");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "CloseLoading", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);            
        }
        protected void Aleart(string message)
        {
            var sbWarning = new StringBuilder();
            sbWarning.Append("alert('"+message+"')");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
    }
}