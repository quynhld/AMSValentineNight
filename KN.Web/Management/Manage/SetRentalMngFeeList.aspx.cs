using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace KN.Web.Management.Manage
{
    public partial class SetRentalMngFeeList : BasePage
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
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();
                        hfRentCd.Value = ddlRentCd.SelectedValue;
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

            if (Request.Params["RentCd"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["RentCd"].ToString()))
                {
                    hfRentCd.Value = Request.Params["RentCd"].ToString();
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
             //Period
            txtStartDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtEndDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            MakePaymentDdl(ddlItemCd);

            MakeInvoiceYN();
            LoadRentDdl(ddlRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            ltInvoiceNo.Text = "Invoice No";
 
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltFeeName.Text = "Fee Name";
            ltUserNm.Text = "Tenant";
            ltPrice.Text = "Unit Price";
            ltAmount.Text = "Total";
            ltIssDt.Text = "Iss Date";

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtCompanyNm.ClientID + "', '" + txtSearchRoom.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtCompanyNm.Text + "', '" + ddlRentCd.SelectedValue + "');";

            lnkbtnUpdate.Visible = Master.isModDelAuthOk;
            lnkbtnCancel.Visible = Master.isModDelAuthOk;
            lnkbtnRegist.Visible = Master.isModDelAuthOk;            
        }

        protected void LoadData()
        {
            var strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            var strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");          
            lvPrintoutList.DataSource = null;
            lvPrintoutList.DataBind();
            hfRentCd.Value = ddlRentCd.SelectedValue;
            //KN_SCR_SELECT_INVOICE_LIST1
            var dtMaster = InvoiceMngBlo.SelectHoadonListMaster1(hfRentCd.Value, txtSearchRoom.Text,txtInvoice.Text, ddlItemCd.SelectedValue, Session["LangCd"].ToString(), ddlInvoiceYN.SelectedValue, strStartDt, strEndDt,txtCompanyNm.Text.Trim());
            lvPrintoutList.DataSource = dtMaster;
            lvPrintoutList.DataBind();
            if (dtMaster.Rows.Count > CommValue.NUMBER_VALUE_0)
            {
                var reftSeq = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
                if (!string.IsNullOrEmpty((reftSeq)))
                {
                    txthfrefSerialNo.Value = reftSeq;
                    LoadDetails((reftSeq));
                }

                if (ddlInvoiceYN.SelectedValue == "Y")
                {

                    ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = false;
                    ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = false;

                }
                else
                {
                    ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = true;
                    ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = true;

                }
            }
            else
            {
                lvRentalMngList.DataSource = null;
                lvRentalMngList.DataBind();
                lvRentalMngList.Items.Clear();
            }
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl(DropDownList ddlParam)
        {
            ddlParam.Items.Clear();

            ddlParam.Items.Add(new ListItem(TextNm["YEARS"], string.Empty));

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.AddYears(1).Year; intTmpI++)
            {
                ddlParam.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }

            ddlParam.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {

            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);
            ddlParamNm.Items.Clear();
            ddlParamNm.Items.Add(new ListItem("Rental Name", "0000"));

            foreach (var dr in dtReturn.Select().Where(dr => !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTA) &&
                                                                 !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTB) &&
                                                                 !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) &&
                                                                 !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP)))
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
            
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl(DropDownList ddlParam)
        {
            ddlParam.Items.Clear();

            ddlParam.Items.Add(new ListItem(TextNm["MONTH"], string.Empty));

            for (var intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlParam.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            ddlParam.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 
        /// </summary>
        private void MakePaymentDdl(DropDownList ddlParam)
        {

           var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlParam.Items.Clear();

            ddlParam.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (var dr in dtReturn.Select())
            {
                    ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        private void MakeBillDdl(DropDownList ddlParam)
        {
            var dtReturn = InvoiceMngBlo.SelectBillCode("BillCd",Session["LangCd"].ToString(),"");
            ddlParam.Items.Clear();
            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
         }

        private void MakeInvoiceYN()
        {
            ddlInvoiceYN.Items.Clear();
            ddlInvoiceYN.Items.Add(new ListItem("Yes", "Y"));
            ddlInvoiceYN.Items.Add(new ListItem("No", "N"));

        }

       

        protected void lvRentalMngList_LayoutCreated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRentalMngList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvRentalMngList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem1 = (ListViewDataItem)e.Item;
            var drView1 = (DataRowView)iTem1.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            var ltInsAddr = (Literal)iTem1.FindControl("ltInsAddr");
            var ltnsQty1 = (Literal)iTem1.FindControl("ltnsQty1");
            var ltnsVat1 = (Literal)iTem1.FindControl("ltnsVat1");
            var ltnsExchangeRate = (Literal)iTem1.FindControl("ltnsExchangeRate");
            var ltnsTotal = (Literal)iTem1.FindControl("ltnsTotal");
            Literal ltlnsDescription = (Literal)iTem1.FindControl("ltlnsDescription");
            Literal ltlnsCpNm = (Literal)iTem1.FindControl("ltlnsCpNm");
            Literal ltlnDt = (Literal)iTem1.FindControl("ltlnDt");
            Literal ltInvoiceNo = (Literal)iTem1.FindControl("ltInvoiceNo");
            Literal ltlnInvoice = (Literal)iTem1.FindControl("ltlnInvoice");
            Literal ltlnRoom = (Literal)iTem1.FindControl("ltlnRoom");
            Literal ltlnsBillCode = (Literal)iTem1.FindControl("ltlnsBillCode");
            Literal ltlnTaxCode = (Literal)iTem1.FindControl("ltlnTaxCode");
            Literal ltlnIsDate = (Literal)iTem1.FindControl("ltlnIsDate");
            Literal ltlnSDate = (Literal)iTem1.FindControl("ltlnSDate");
            Literal ltlnEDate = (Literal)iTem1.FindControl("ltlnEDate");
            Literal ltnnInvoiceChoice = (Literal)iTem1.FindControl("ltnnInvoiceChoice");

            ltInsAddr.Text = TextNm["ADDR"];
            ltnsQty1.Text = "Qty";
            ltnsVat1.Text = "Vat";
            ltnsExchangeRate.Text = "Exchange Rate";
            ltnsTotal.Text = "Total";
            ltlnsDescription.Text = "Description";
            ltlnsCpNm.Text = "Name";
            ltlnInvoice.Text = "Invoice No";
            ltlnDt.Text = "Year/Month";
            ltlnRoom.Text = "Room No";
            ltlnsBillCode.Text = "Bill Code";
            ltlnTaxCode.Text = "Tax Code";
            ltlnIsDate.Text = "Isusing Date";
            ltlnSDate.Text = "Start Date";
            ltlnEDate.Text = "End Date";
            ltnnInvoiceChoice.Text = "Reprint Invoice No";

                
                

            var ddllvYear = (DropDownList)iTem1.FindControl("ddllvYear");
            MakeYearDdl(ddllvYear);
            var ddllvMonth = (DropDownList)iTem1.FindControl("ddllvMonth");
            MakeMonthDdl(ddllvMonth);
            var ddlInvoiceChoice = (DropDownList)iTem1.FindControl("ddlInvoiceChoice");
            MakeReprintInvoiceNo(ddlInvoiceChoice);

            var ddlBillCd = (DropDownList)iTem1.FindControl("ddlBillCd");
            MakeBillDdl(ddlBillCd);

            if (!string.IsNullOrEmpty(drView1["InvoiceNo"].ToString()))
            {
                ltInvoiceNo.Text = drView1["InvoiceNo"].ToString();
            }
            var txtHfInvoiceNo = (TextBox)iTem1.FindControl("txtHfInvoiceNo");

            if (!string.IsNullOrEmpty(drView1["InvoiceNo"].ToString()))
            {
                txtHfInvoiceNo.Text = TextLib.StringDecoder(drView1["InvoiceNo"].ToString());
            }
            else
            {
                txtHfInvoiceNo.Text = ltInvoiceNo.Text;
            }


            var txtHfSerialNo = (TextBox)iTem1.FindControl("txtHfSerialNo");

            if (!string.IsNullOrEmpty(drView1["SerialNo"].ToString()))
            {
                txtHfSerialNo.Text = TextLib.StringDecoder(drView1["SerialNo"].ToString());
            }
                

            var ltInsYear = (Literal)iTem1.FindControl("ltInsYear");

            if (!string.IsNullOrEmpty(drView1["SvcYear"].ToString()))
            {
                ltInsYear.Text = drView1["SvcYear"].ToString();
            }

            var txtHfYear = (TextBox)iTem1.FindControl("txtHfYear");

            if (!string.IsNullOrEmpty(drView1["SvcYear"].ToString()))
            {
                txtHfYear.Text = TextLib.StringDecoder(drView1["SvcYear"].ToString());
            }
                

            var ltInsMonth = (Literal)iTem1.FindControl("ltInsMonth");

            if (!string.IsNullOrEmpty(drView1["SvcMM"].ToString()))
            {
                ltInsMonth.Text = drView1["SvcMM"].ToString();
            }
            var txtHfMonth = (TextBox)iTem1.FindControl("txtHfMonth");

            if (!string.IsNullOrEmpty(drView1["SvcMM"].ToString()))
            {
                //txtHfMonth.Text = TextLib.StringDecoder(drView1["SvcMM"].ToString());
                txtHfMonth.Text = ddllvMonth.SelectedValue;
            }

            var ddllvYear1 = (DropDownList)iTem1.FindControl("ddllvYear");
            if (!string.IsNullOrEmpty(drView1["SvcYear"].ToString()))
            {
                //txtHfMonth.Text = TextLib.StringDecoder(drView1["SvcMM"].ToString());
                ddllvYear1.SelectedValue = drView1["SvcYear"].ToString();
            }

            var ddllvMonth1 = (DropDownList)iTem1.FindControl("ddllvMonth");
            if (!string.IsNullOrEmpty(drView1["SvcMM"].ToString()))
            {
                //txtHfMonth.Text = TextLib.StringDecoder(drView1["SvcMM"].ToString());
                ddllvMonth1.SelectedValue = drView1["SvcMM"].ToString();
            }

            var ddlBillCd1 = (DropDownList)iTem1.FindControl("ddlBillCd");
            if (!string.IsNullOrEmpty(drView1["BillCd"].ToString()))
            {
                //txtHfMonth.Text = TextLib.StringDecoder(drView1["SvcMM"].ToString());
                ddlBillCd1.SelectedValue = drView1["BillCd"].ToString();
            }

            var ltInsRoomNo = (Literal)iTem1.FindControl("ltInsRoomNo");

            if (!string.IsNullOrEmpty(drView1["RoomNo"].ToString()))
            {
                ltInsRoomNo.Text = drView1["RoomNo"].ToString();
                hfRoomNo.Value = drView1["RoomNo"].ToString();
            }


            var ltInsPaymentDt = (Literal)iTem1.FindControl("ltInsPaymentDt");

            if (!string.IsNullOrEmpty(drView1["PaymentDt"].ToString()))
            {
                ltInsPaymentDt.Text = drView1["PaymentDt"].ToString();
            }

            var txtHfPaymentDt = (TextBox)iTem1.FindControl("txtHfPaymentDt");

            if (!string.IsNullOrEmpty(drView1["PaymentDt"].ToString()))
            {
                txtHfPaymentDt.Text = TextLib.StringDecoder(drView1["PaymentDt"].ToString());
            }

            var txtHfBillCd = (TextBox)iTem1.FindControl("txtHfBillCd");

            if (!string.IsNullOrEmpty(drView1["BillCd"].ToString()))
            {
                txtHfBillCd.Text = TextLib.StringDecoder(drView1["BillCd"].ToString());
            }



            var txtInsUserNm = (TextBox)iTem1.FindControl("txtInsUserNm");

            if (!string.IsNullOrEmpty(drView1["UserNm"].ToString()))
            {
                txtInsUserNm.Text = TextLib.StringDecoder(drView1["UserNm"].ToString());
            }

            var txtInsAddress = (TextBox)iTem1.FindControl("txtInsAddress");

            if (!string.IsNullOrEmpty(drView1["LandloadAddr"].ToString()))
            {
                txtInsAddress.Text = TextLib.StringDecoder(drView1["LandloadAddr"].ToString());
            }

            var txtInsDetAddress = (TextBox)iTem1.FindControl("txtInsDetAddress");

            if (!string.IsNullOrEmpty(drView1["LandloadDetAddr"].ToString()))
            {
                txtInsDetAddress.Text = TextLib.StringDecoder(drView1["LandloadDetAddr"].ToString());
            }

            var txtInsUserCd = (TextBox)iTem1.FindControl("txtInsUserCd");

            if (!string.IsNullOrEmpty(drView1["LandloadTaxCd"].ToString()))
            {
                txtInsUserCd.Text = TextLib.StringDecoder(drView1["LandloadTaxCd"].ToString());
            }

            var txtltnsDescription = (TextBox)iTem1.FindControl("txtltnsDescription");

            if (!string.IsNullOrEmpty(drView1["Desciption"].ToString()))
            {
                txtltnsDescription.Text = TextLib.StringDecoder(drView1["Desciption"].ToString());
            }

            var txtInsExchageRate = (TextBox)iTem1.FindControl("txtInsExchageRate");

            if (!string.IsNullOrEmpty(drView1["DongToDollar"].ToString()))
            {
                txtInsExchageRate.Text = drView1["DongToDollar"].ToString();
                txtInsExchageRate.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            }

            var txtInsAmtViNo = (TextBox)iTem1.FindControl("txtInsAmtViNo");

            if (!string.IsNullOrEmpty(drView1["TotSellingPrice"].ToString()))
            {
                txtInsAmtViNo.Text = double.Parse(drView1["TotSellingPrice"].ToString()).ToString("##0");                    
                txtInsAmtViNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            }


            var txtHfUserSeq1 = (TextBox)iTem1.FindControl("txtHfUserSeq1");

            if (!string.IsNullOrEmpty(drView1["UserSeq"].ToString()))
            {
                txtHfUserSeq1.Text = TextLib.StringDecoder(drView1["UserSeq"].ToString());
            }

            var txtQty1 = (TextBox)iTem1.FindControl("txtQty1");

            if (!string.IsNullOrEmpty(drView1["Qty"].ToString()))
            {
                txtQty1.Text = TextLib.StringDecoder(drView1["Qty"].ToString());
            }

            var txtVat1 = (TextBox)iTem1.FindControl("txtVat1");

            if (!string.IsNullOrEmpty(drView1["VATAmt"].ToString()))
            {
                txtVat1.Text = double.Parse(drView1["VATAmt"].ToString()).ToString("##0");    
            }

            var txtUserTaxCd = (TextBox)iTem1.FindControl("txtUserTaxCd");

            if (!string.IsNullOrEmpty(drView1["LandloadTaxCd"].ToString()))
            {
                txtUserTaxCd.Text = TextLib.StringDecoder(drView1["LandloadTaxCd"].ToString());
            }

            var txtIssDt = (TextBox)iTem1.FindControl("txtIssDt");

            if (!string.IsNullOrEmpty(drView1["IssuingDate"].ToString()))
            {
                txtIssDt.Text = TextLib.StringDecoder(drView1["IssuingDate"].ToString());
                   
            }

            var txtStartDt = (TextBox)iTem1.FindControl("txtStartDt");

            if (!string.IsNullOrEmpty(drView1["SvcStartDt"].ToString()))
            {
                txtStartDt.Text = TextLib.StringDecoder(drView1["SvcStartDt"].ToString());
            }

            var txtEndDt = (TextBox)iTem1.FindControl("txtEndDt");

            if (!string.IsNullOrEmpty(drView1["SvcEndDt"].ToString()))
            {
                txtEndDt.Text = TextLib.StringDecoder(drView1["SvcEndDt"].ToString());
            }                
        }

        private void MakeReprintInvoiceNo(DropDownList ddlParam)
        {
            var dtReturn = InvoiceMngBlo.SelectReprintInvoiceNo("11111112");
            ddlParam.Items.Clear();
            foreach (var dr in dtReturn.Select())
            {
                ddlParam.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
            ddlParam.Items.Add(new ListItem("", ""));
            ddlParam.SelectedValue = "";

        }

        protected void lvRentalMngList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

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
                //var reft = txthfrefSerialNo.Value;
                var reft_seq = string.Empty;
                LoadData();
               
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

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
                var sbWarning = new StringBuilder();
                sbWarning.Append("CloseLoading();");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
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
            var ltInvoiceNoP = (Literal)iTem.FindControl("ltInvoiceNoP");
            if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
            {
                ltInvoiceNoP.Text = drView["InvoiceNo"].ToString();
            }

            var txtHfrefSerialNoP = (TextBox)iTem.FindControl("txtHfrefSerialNoP");
            if (!string.IsNullOrEmpty(drView["REF_SERIAL_NO"].ToString()))
            {
                txtHfrefSerialNoP.Text = drView["REF_SERIAL_NO"].ToString();
            }

            var ltVATAmt = (Literal)iTem.FindControl("ltVATAmt");

            if (!string.IsNullOrEmpty(drView["VATAmt"].ToString()))
            {
                ltVATAmt.Text = TextLib.MakeVietIntNo(double.Parse(drView["VATAmt"].ToString()).ToString("###,##0"));
            }

            var ltInsRoomNoP = (Literal)iTem.FindControl("ltInsRoomNoP");

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                ltInsRoomNoP.Text = drView["RoomNo"].ToString();
            }

            var ltInsFeeNameP = (Literal)iTem.FindControl("ltInsFeeNameP");

            if (!string.IsNullOrEmpty(drView["FeeName"].ToString()))
            {
                ltInsFeeNameP.Text = TextLib.StringDecoder(drView["FeeName"].ToString());
            }

            var ltInsUserNmP = (Literal)iTem.FindControl("ltInsUserNmP");

            if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
            {
                ltInsUserNmP.Text = TextLib.StringDecoder(drView["UserNm"].ToString());
            }

            var ltInsUPriceP = (Literal)iTem.FindControl("ltInsUPriceP");

            if (!string.IsNullOrEmpty(drView["UnitPrice"].ToString()))
            {
                ltInsUPriceP.Text = TextLib.MakeVietIntNo(double.Parse(drView["UnitPrice"].ToString()).ToString("###,##0"));
            }

            var ltInsAmtViNoP = (Literal)iTem.FindControl("ltInsAmtViNoP");

            if (!string.IsNullOrEmpty(drView["TotSellingPrice"].ToString()))
            {
                ltInsAmtViNoP.Text = TextLib.MakeVietIntNo(double.Parse(drView["TotSellingPrice"].ToString()).ToString("###,##0"));
            }

            var ltnsIssDtP = (Literal)iTem.FindControl("ltnsIssDtP");

            if (!string.IsNullOrEmpty(drView["IssuingDate"].ToString()))
            {
                ltnsIssDtP.Text = TextLib.MakeDateEightDigit(drView["IssuingDate"].ToString());
            }

            intRowsCnt++;
        }

        protected void lvPrintoutList_LayoutCreated(object sender, EventArgs e)
        {

        }

        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            txthfrefSerialNo.Value = ((Label)e.Item.FindControl("txtHfrefSerialNo1")).Text;
        }

        protected void lvPrintoutList_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            lvRentalMngList.DataSource = null;
            lvRentalMngList.DataBind();
            var reft = txthfrefSerialNo.Value;
            LoadDetails(reft);

            if (ddlInvoiceYN.SelectedValue == "Y")
            {

                ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = false;
                ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = false;

            }
            else
            {
                ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = true;
                ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = true;

            }

        }
        protected void LoadDetails(string refSeq)
        {
            lvRentalMngList.DataSource = null;
            lvRentalMngList.DataBind();
            var  dtDetail = InvoiceMngBlo.SelectHoadonListDetail(refSeq);
            lvRentalMngList.DataSource = dtDetail;
            lvRentalMngList.DataBind();        
        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var reft = txthfrefSerialNo.Value;
                if (string.IsNullOrEmpty(reft))
                {
                    return;
                }
                var strInvoiceNo = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Text;
                var strInsBillCd = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlBillCd")).SelectedValue;
                var strInsUserNm = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsUserNm")).Text;

                var strSerialNo = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfSerialNo")).Text;
                var strInsAddress = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsAddress")).Text;
                var strInsDetAddress = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsDetAddress")).Text;
                var strInsUserCd = ((TextBox)lvRentalMngList.Items[0].FindControl("txtUserTaxCd")).Text;
                var strPaymentDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfPaymentDt")).Text;
                var strInsYear = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvYear")).Text;
                var strInsMonth = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvMonth")).Text;

                var strInsDescription = ((TextBox)lvRentalMngList.Items[0].FindControl("txtltnsDescription")).Text;
                var intQty = ((TextBox)lvRentalMngList.Items[0].FindControl("txtQty1")).Text;
                var strInsAmtViNo = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsAmtViNo")).Text;
                var strInsVat = ((TextBox)lvRentalMngList.Items[0].FindControl("txtVat1")).Text;
                var strInsInsDate = ((TextBox)lvRentalMngList.Items[0].FindControl("txtIssDt")).Text;
                var strInsStartDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtStartDt")).Text;
                var strInsEndDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtEndDt")).Text;
                var strInsExchageRate = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsExchageRate")).Text;
                var strRoomNo = ((Literal)lvRentalMngList.Items[0].FindControl("ltInsRoomNo")).Text;


                var dblInsExchageRate = CommValue.NUMBER_VALUE_0_0;
                var dblInsAmtViNo = CommValue.NUMBER_VALUE_0_0;
                var dbintQty = CommValue.NUMBER_VALUE_0;
                var dblInsVat = CommValue.NUMBER_VALUE_0_0;   

                if (!string.IsNullOrEmpty(strInsAmtViNo))
                {
                    dblInsAmtViNo = double.Parse(strInsAmtViNo);
                }
                if (!string.IsNullOrEmpty(strInsVat))
                {
                    dblInsVat = double.Parse(strInsVat);
                }

                if (!string.IsNullOrEmpty(intQty))
                {
                    dbintQty = Int32.Parse(intQty);
                }

                if (!string.IsNullOrEmpty(strInsExchageRate))
                {
                    dblInsExchageRate = double.Parse(strInsExchageRate);
                }
                        // 오피스 / 리테일 데이터 수정
                      // KN_SCR_UPDATE_INVOICE
                     InvoiceMngBlo.UpdateHoadonListNew(strInvoiceNo,strInsBillCd, strSerialNo, strInsYear, strInsMonth, strInsUserNm, strInsAddress, strInsDetAddress,
                                                     strInsUserCd,  strInsDescription,  strPaymentDt, strInsStartDt, strInsEndDt, dblInsAmtViNo, dblInsVat, dblInsExchageRate,
                                                    dblInsAmtViNo, strInsInsDate, dbintQty, reft, strRoomNo);

                        LoadData();                                            
                        var sbWarning = new StringBuilder();
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
        //BaoTv
        protected void lnkbtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
                var strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

                // KN_SCR_SELECT_INVOICE_LIST_EXCEL
                var dtSource = InvoiceMngBlo.SelectHoadonListExcel(ddlRentCd.SelectedValue, txtSearchRoom.Text.Trim(), txtInvoice.Text, ddlItemCd.SelectedValue, Session["LangCd"].ToString(), ddlInvoiceYN.SelectedValue, strStartDt, strEndDt,txtCompanyNm.Text.Trim());
                if (dtSource.Rows.Count <= 0)
                {
                    return;
                }
                var feeTy = ddlItemCd.SelectedValue == "" ? "" : "-" + ddlItemCd.SelectedItem;
                var rentalNm = ddlRentCd.SelectedValue == "0000" ? "" : "-" + ddlRentCd.SelectedItem;

                var fileName = Server.UrlEncode("Invoice List ").Replace("+", " ") + rentalNm + feeTy;

                GenerateExcel(dtSource, fileName, fileName);
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
                var reft = txthfrefSerialNo.Value;
                if(string.IsNullOrEmpty(reft))
                {
                    return;
                }
                InvoiceMngBlo.CancelHoadon(reft);
                LoadData();
                var sbWarning = new StringBuilder();
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

        protected void lnkbtnReprint_Click(object sender, EventArgs e)
        {
            try
            {
                var reft= txthfrefSerialNo.Value;
                var refPrintBundleNo = reft;
                var strRoomNo = ((Literal)lvRentalMngList.Items[0].FindControl("ltInsRoomNo")).Text;
                if (lvRentalMngList.Items.Count <= 0)
                {
                    return;
                }

                //========================Update PRINT_BUNDLE_NO = ''============
                InvoiceMngBlo.UpdatelHoadonReprintList1("");

                //=======Update PRINT_BUNDLE_NO = REF_SERIAL_NO =================
                InvoiceMngBlo.UpdatelHoadonReprintList(hfRentCd.Value, reft, refPrintBundleNo);
          
                 var dtreturn = InvoiceMngBlo.SelectReprintHoadon(reft);                
                //=============================Print=============================
                if (dtreturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + refPrintBundleNo + "','" + strRoomNo + "');", CommValue.AUTH_VALUE_TRUE);
                }            
            }
             catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

            
        }

        protected void lvRentalMngList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ((Literal)lvRentalMngList.Items[0].FindControl("ltInvoiceNo")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).SelectedValue.ToString(); 
        }
        protected void ddllvYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfYear")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvYear")).SelectedValue.ToString();

        }
        protected void ddllvMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfMonth")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvMonth")).SelectedValue.ToString();

        }
        protected void ddlInvoiceChoice_SelectedIndexChanged(object sender, EventArgs e)
        {            
            ((Literal)lvRentalMngList.Items[0].FindControl("ltInvoiceNo")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).SelectedValue.ToString(); 
        }              

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var rentCd = hfRentCd.Value;

                Response.Redirect(Master.PAGE_WRITE + "?" + "RentCd=" + rentCd , CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }            
        }


        protected void lnkLoadData_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
        public void GenerateExcel(DataTable tbl, string excelSheetName, string fileName)
        {
            using (var pck = new ExcelPackage())
            {
                //Create the worksheet
                var ws = pck.Workbook.Worksheets.Add(excelSheetName);

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(tbl, true,TableStyles.None);
                ////
                var tbls = ws.Tables[0];
                
                tbls.ShowTotal = true;
                //Set Sum 
                tbls.Columns[11].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[10].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[9].TotalsRowFunction = RowFunctions.Sum;
    

                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:M1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:M1"].AutoFitColumns();
                using (var col = ws.Cells[2, 10, 2 + tbl.Rows.Count, 12])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                using (var col = ws.Cells[2, 5, 2 + tbl.Rows.Count, 5])
                {
                    col.Style.Numberformat.Format = "dd/MM/yyyy";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename="+fileName+".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
        }
    }
    }
