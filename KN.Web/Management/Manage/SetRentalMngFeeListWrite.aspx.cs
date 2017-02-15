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

using KN.Settlement.Biz;

namespace KN.Web.Management.Manage
{
    public partial class SetRentalMngFeeListWrite : BasePage
    {
        string strInit = string.Empty;
        object objTag = new object();

        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;

        DataTable dtMaster = new DataTable();
        DataTable dtDetail = new DataTable();
        DataTable dtParamReturn = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {                    
                    InitControls();
                    CheckParams();
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
            ltSearchRoom.Text = TextNm["ROOMNO"];

             //Period
            txtStartDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtEndDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            //ltSearchYear.Text = TextNm["YEARS"];
            //ltSearchMonth.Text = TextNm["MONTH"];

            // DropDownList Setting
            // 년도
           // MakeYearDdl(ddlYear);

            
            // 월
            //MakeMonthDdl(ddlMonth);

            // 수납 아이템
            MakePaymentDdl(ddlItemCd);

            MakeInvoiceYN();


            MakeYearDdl(ddllvYear);

            MakeMonthDdl(ddllvMonth);

            MakeBillDdl(ddlBillCd);

            //MakeReprintInvoiceNo(ddlInvoiceChoice);

            ltInvoiceNo.Text = "Invoice No";
            ltDate.Text = TextNm["MONTH"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltFeeName.Text = "Fee Name";
            ltUserNm.Text = "Tenant";
            ltPrice.Text = "Unit Price";
            ltAmount.Text = "Total";
            ltIssDt.Text = "Iss Date";

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

            

            //ltDate.Text = TextNm["YEARS"] + "/" + TextNm["MONTH"];
            //ltRoomNo.Text = TextNm["ROOMNO"];
            //ltPaymentTy.Text = TextNm["ITEM"];
            //ltPaymentDt.Text = TextNm["EXCHANGERATE"];
            //ltDescription.Text = TextNm["CONTENTS"];
            //ltAmount.Text = TextNm["PAYMENT"];

            //ltContItemCd.Text = TextNm["ITEM"];
            //ltContYearMM.Text = TextNm["YEARS"] + "/" + TextNm["MONTH"];
            //ltContRoomNo.Text = TextNm["ROOMNO"];
            //ltContExchageRate.Text = TextNm["EXCHANGERATE"];
            //ltContAmt.Text = TextNm["PAYMENT"];
            //ltUnitDong.Text = TextNm["DONG"];
            //ltContContext.Text = TextNm["CONTENTS"];

            //MakePaymentDdl(ddlContItemCd);

            //MakeYearDdl(ddlContYear);

            //MakeMonthDdl(ddlContMM);

            //lnkbtnIssuing.Text = TextNm["REGIST"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            //lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            //lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";

            //chkAll.Enabled = CommValue.AUTH_VALUE_FALSE;
        }

        protected void LoadData()
        {
            string strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            string strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

            lvPrintoutList.DataSource = null;
            lvPrintoutList.DataBind();

            dtMaster = InvoiceMngBlo.SelectHoadonListMaster1(hfRentCd.Value, txtSearchRoom.Text,txtInvoice.Text, ddlItemCd.SelectedValue, Session["LangCd"].ToString(), ddlInvoiceYN.SelectedValue, strStartDt,"","");

            lvPrintoutList.DataSource = dtMaster;
            lvPrintoutList.DataBind();
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

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl(DropDownList ddlParam)
        {
            ddlParam.Items.Clear();

            ddlParam.Items.Add(new ListItem(TextNm["MONTH"], string.Empty));

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
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
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlParam.Items.Clear();

            ddlParam.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                //if (dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) ||
                //    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_MNGFEE))
                //{
                    ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                //}
            }
        }

        private void MakeBillDdl(DropDownList ddlParam)
        {
            DataTable dtReturn = new DataTable();
            dtReturn = InvoiceMngBlo.SelectBillCode("BillCd",Session["LangCd"].ToString(),"");
            ddlParam.Items.Clear();
            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
            //ddlParam.Items.Add(new ListItem("", ""));
            //ddlParam.SelectedValue = "";
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
            ListViewDataItem iTem1 = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView1 = (System.Data.DataRowView)iTem1.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {

                Literal ltInsAddr = (Literal)iTem1.FindControl("ltInsAddr");
                Literal ltInsRemark = (Literal)iTem1.FindControl("ltInsRemark");
                Literal ltnsQty1 = (Literal)iTem1.FindControl("ltnsQty1");
                Literal ltnsVat1 = (Literal)iTem1.FindControl("ltnsVat1");
                Literal ltnsExchangeRate = (Literal)iTem1.FindControl("ltnsExchangeRate");
                Literal ltnsTotal = (Literal)iTem1.FindControl("ltnsTotal");
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

                

                
                

                DropDownList ddllvYear = (DropDownList)iTem1.FindControl("ddllvYear");
                //CommCdDdlUtil.MakeCommCdDdl(ddllvYear, dtParamReturn);
                MakeYearDdl(ddllvYear);
                //if (!string.IsNullOrEmpty(drView1["SvcYear"].ToString()))
                //{
                //    ddllvYear.SelectedItem.Text = drView1["SvcYear"].ToString();
                    
                //}                

                DropDownList ddllvMonth = (DropDownList)iTem1.FindControl("ddllvMonth");
                MakeMonthDdl(ddllvMonth);
                DropDownList ddlInvoiceChoice = (DropDownList)iTem1.FindControl("ddlInvoiceChoice");
                MakeReprintInvoiceNo(ddlInvoiceChoice);

                DropDownList ddlBillCd = (DropDownList)iTem1.FindControl("ddlBillCd");
                MakeBillDdl(ddlBillCd);

                if (!string.IsNullOrEmpty(drView1["InvoiceNo"].ToString()))
                {
                    ltInvoiceNo.Text = drView1["InvoiceNo"].ToString();
                    //txtHfSerialNo.Text = drView["SerialNo"].ToString();
                    //txtHfPrintSeq.Text = drView["PrintSeq"].ToString();
                    //txtHfPrintDetSeq.Text = drView["PrintDetSeq"].ToString();
                }
                TextBox txtHfInvoiceNo = (TextBox)iTem1.FindControl("txtHfInvoiceNo");

                if (!string.IsNullOrEmpty(drView1["InvoiceNo"].ToString()))
                {
                    txtHfInvoiceNo.Text = TextLib.StringDecoder(drView1["InvoiceNo"].ToString());
                }
                else
                {
                    txtHfInvoiceNo.Text = ltInvoiceNo.Text;
                }


                TextBox txtHfSerialNo = (TextBox)iTem1.FindControl("txtHfSerialNo");

                if (!string.IsNullOrEmpty(drView1["SerialNo"].ToString()))
                {
                    txtHfSerialNo.Text = TextLib.StringDecoder(drView1["SerialNo"].ToString());
                }
                

                Literal ltInsYear = (Literal)iTem1.FindControl("ltInsYear");

                if (!string.IsNullOrEmpty(drView1["SvcYear"].ToString()))
                {
                    ltInsYear.Text = drView1["SvcYear"].ToString();
                }

                TextBox txtHfYear = (TextBox)iTem1.FindControl("txtHfYear");

                if (!string.IsNullOrEmpty(drView1["SvcYear"].ToString()))
                {
                    txtHfYear.Text = TextLib.StringDecoder(drView1["SvcYear"].ToString());
                }
                

                Literal ltInsMonth = (Literal)iTem1.FindControl("ltInsMonth");

                if (!string.IsNullOrEmpty(drView1["SvcMM"].ToString()))
                {
                    ltInsMonth.Text = drView1["SvcMM"].ToString();
                }
                TextBox txtHfMonth = (TextBox)iTem1.FindControl("txtHfMonth");

                if (!string.IsNullOrEmpty(drView1["SvcMM"].ToString()))
                {
                    //txtHfMonth.Text = TextLib.StringDecoder(drView1["SvcMM"].ToString());
                    txtHfMonth.Text = ddllvMonth.SelectedValue;
                }

                DropDownList ddllvYear1 = (DropDownList)iTem1.FindControl("ddllvYear");
                if (!string.IsNullOrEmpty(drView1["SvcYear"].ToString()))
                {
                    //txtHfMonth.Text = TextLib.StringDecoder(drView1["SvcMM"].ToString());
                    ddllvYear1.SelectedValue = drView1["SvcYear"].ToString();
                }

                DropDownList ddllvMonth1 = (DropDownList)iTem1.FindControl("ddllvMonth");
                if (!string.IsNullOrEmpty(drView1["SvcMM"].ToString()))
                {
                    //txtHfMonth.Text = TextLib.StringDecoder(drView1["SvcMM"].ToString());
                    ddllvMonth1.SelectedValue = drView1["SvcMM"].ToString();
                }

                DropDownList ddlBillCd1 = (DropDownList)iTem1.FindControl("ddlBillCd");
                if (!string.IsNullOrEmpty(drView1["BillCd"].ToString()))
                {
                    //txtHfMonth.Text = TextLib.StringDecoder(drView1["SvcMM"].ToString());
                    ddlBillCd1.SelectedValue = drView1["BillCd"].ToString();
                }

                Literal ltInsRoomNo = (Literal)iTem1.FindControl("ltInsRoomNo");

                if (!string.IsNullOrEmpty(drView1["RoomNo"].ToString()))
                {
                    ltInsRoomNo.Text = drView1["RoomNo"].ToString();
                }


                Literal ltInsPaymentDt = (Literal)iTem1.FindControl("ltInsPaymentDt");

                if (!string.IsNullOrEmpty(drView1["PaymentDt"].ToString()))
                {
                    ltInsPaymentDt.Text = drView1["PaymentDt"].ToString();
                }

                TextBox txtHfPaymentDt = (TextBox)iTem1.FindControl("txtHfPaymentDt");

                if (!string.IsNullOrEmpty(drView1["PaymentDt"].ToString()))
                {
                    txtHfPaymentDt.Text = TextLib.StringDecoder(drView1["PaymentDt"].ToString());
                }

                //Literal ltlnsBillCd = (Literal)iTem1.FindControl("ltlnsBillCd");

                //if (!string.IsNullOrEmpty(drView1["BillCd"].ToString()))
                //{
                //    ltlnsBillCd.Text = drView1["BillCd"].ToString();
                //}

                TextBox txtHfBillCd = (TextBox)iTem1.FindControl("txtHfBillCd");

                if (!string.IsNullOrEmpty(drView1["BillCd"].ToString()))
                {
                    txtHfBillCd.Text = TextLib.StringDecoder(drView1["BillCd"].ToString());
                }



                TextBox txtInsUserNm = (TextBox)iTem1.FindControl("txtInsUserNm");

                if (!string.IsNullOrEmpty(drView1["UserNm"].ToString()))
                {
                    txtInsUserNm.Text = TextLib.StringDecoder(drView1["UserNm"].ToString());
                }

                TextBox txtInsAddress = (TextBox)iTem1.FindControl("txtInsAddress");

                if (!string.IsNullOrEmpty(drView1["LandloadAddr"].ToString()))
                {
                    txtInsAddress.Text = TextLib.StringDecoder(drView1["LandloadAddr"].ToString());
                }

                TextBox txtInsDetAddress = (TextBox)iTem1.FindControl("txtInsDetAddress");

                if (!string.IsNullOrEmpty(drView1["LandloadDetAddr"].ToString()))
                {
                    txtInsDetAddress.Text = TextLib.StringDecoder(drView1["LandloadDetAddr"].ToString());
                }

                TextBox txtInsUserCd = (TextBox)iTem1.FindControl("txtInsUserCd");

                if (!string.IsNullOrEmpty(drView1["LandloadTaxCd"].ToString()))
                {
                    txtInsUserCd.Text = TextLib.StringDecoder(drView1["LandloadTaxCd"].ToString());
                }

                TextBox txtltnsDescription = (TextBox)iTem1.FindControl("txtltnsDescription");

                if (!string.IsNullOrEmpty(drView1["Desciption"].ToString()))
                {
                    txtltnsDescription.Text = TextLib.StringDecoder(drView1["Desciption"].ToString());
                }

                TextBox txtInsExchageRate = (TextBox)iTem1.FindControl("txtInsExchageRate");

                if (!string.IsNullOrEmpty(drView1["DongToDollar"].ToString()))
                {
                    txtInsExchageRate.Text = drView1["DongToDollar"].ToString();
                    txtInsExchageRate.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }

                TextBox txtInsAmtViNo = (TextBox)iTem1.FindControl("txtInsAmtViNo");

                if (!string.IsNullOrEmpty(drView1["TotSellingPrice"].ToString()))
                {
                    txtInsAmtViNo.Text = double.Parse(drView1["TotSellingPrice"].ToString()).ToString("##0");                    
                    txtInsAmtViNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }


                TextBox txtHfUserSeq1 = (TextBox)iTem1.FindControl("txtHfUserSeq1");

                if (!string.IsNullOrEmpty(drView1["UserSeq"].ToString()))
                {
                    txtHfUserSeq1.Text = TextLib.StringDecoder(drView1["UserSeq"].ToString());
                }

                TextBox txtQty1 = (TextBox)iTem1.FindControl("txtQty1");

                if (!string.IsNullOrEmpty(drView1["Qty"].ToString()))
                {
                    txtQty1.Text = TextLib.StringDecoder(drView1["Qty"].ToString());
                }

                TextBox txtVat1 = (TextBox)iTem1.FindControl("txtVat1");

                if (!string.IsNullOrEmpty(drView1["VATAmt"].ToString()))
                {
                    txtVat1.Text = double.Parse(drView1["VATAmt"].ToString()).ToString("##0");    
                }

                TextBox txtUserTaxCd = (TextBox)iTem1.FindControl("txtUserTaxCd");

                if (!string.IsNullOrEmpty(drView1["LandloadTaxCd"].ToString()))
                {
                    txtUserTaxCd.Text = TextLib.StringDecoder(drView1["LandloadTaxCd"].ToString());
                }

                TextBox txtIssDt = (TextBox)iTem1.FindControl("txtIssDt");

                if (!string.IsNullOrEmpty(drView1["IssuingDate"].ToString()))
                {
                    txtIssDt.Text = TextLib.StringDecoder(drView1["IssuingDate"].ToString());
                   
                }

                TextBox txtStartDt = (TextBox)iTem1.FindControl("txtStartDt");

                if (!string.IsNullOrEmpty(drView1["SvcStartDt"].ToString()))
                {
                    txtStartDt.Text = TextLib.StringDecoder(drView1["SvcStartDt"].ToString());
                }

                TextBox txtEndDt = (TextBox)iTem1.FindControl("txtEndDt");

                if (!string.IsNullOrEmpty(drView1["SvcEndDt"].ToString()))
                {
                    txtEndDt.Text = TextLib.StringDecoder(drView1["SvcEndDt"].ToString());
                }
                
                


                


                //TextBox txtInsRemark = (TextBox)iTem.FindControl("txtInsRemark");

                //if (!string.IsNullOrEmpty(drView["Remark"].ToString()))
                //{
                //    txtInsRemark.Text = TextLib.StringDecoder(drView["Remark"].ToString());
                //}

                //ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                //imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "')";
            }
        }

        private void MakeReprintInvoiceNo(DropDownList ddlParam)
        {
            DataTable dtReturn = new DataTable();
            dtReturn = InvoiceMngBlo.SelectReprintInvoiceNo("11111112");
            ddlParam.Items.Clear();
            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParam.Items.Add(new ListItem(dr[0].ToString(), dr[0].ToString()));
            }
            ddlParam.Items.Add(new ListItem("", ""));
            ddlParam.SelectedValue = "";

        }
        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        //protected void chkAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    bool isAllCheck = chkAll.Checked;

        //    try
        //    {
        //        CheckAll(isAllCheck);
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrLogger.MakeLogger(ex);
        //    }
        //}

        /// <summary>
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
       

        /// <summary>
        /// 리스트 각 행별 체크 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //int intTotalCount = CommValue.NUMBER_VALUE_0;
                //int intCheckCount = CommValue.NUMBER_VALUE_0;

                //for (int intTmpI = 0; intTmpI < lvRentalMngList.Items.Count; intTmpI++)
                //{
                //    if (((CheckBox)lvRentalMngList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                //    {
                //        intTotalCount++;

                //        if (((CheckBox)lvRentalMngList.Items[intTmpI].FindControl("chkboxList")).Checked)
                //        {
                //            intCheckCount++;
                //        }
                //    }
                //}

                //if (intTotalCount == intCheckCount)
                //{
                //    chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
                //}
                //else
                //{
                //    chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                //}
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvRentalMngList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            //try
            //{
            //    // 세션체크
            //    AuthCheckLib.CheckSession();

            //    string strRentCd = hfRentCd.Value;

            //    string strInvoiceNo = ((Literal)lvRentalMngList.Items[e.ItemIndex].FindControl("ltInvoiceNo")).Text;
            //    string strSerialNo = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtHfSerialNo")).Text;
            //    //string strPrintSeq = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtHfPrintSeq")).Text;
            //    //string strPrintDetSeq = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtHfPrintDetSeq")).Text;
            //    //string strUserSeq = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtHfUserSeq")).Text;
            //    string strInsUserNm = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtInsUserNm")).Text;
            //    string strInsAddress = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtInsAddress")).Text;
            //    string strInsDetAddress = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtInsDetAddress")).Text;
            //    string strInsUserCd = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtInsUserCd")).Text;
            //    string strPaymentDt = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("ltInsPaymentDt")).Text;
            //    string strInsYear = ((Literal)lvRentalMngList.Items[e.ItemIndex].FindControl("ltInsYear")).Text;
            //    string strInsMonth = ((Literal)lvRentalMngList.Items[e.ItemIndex].FindControl("ltInsMonth")).Text;
            //    string strInsDescription = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtInsDescription")).Text;
            //    string intQty = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtQty1")).Text;
            //    string strInsAmtViNo = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtInsAmtViNo")).Text;
            //    string strInsVAT = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtVat1")).Text;
            //    string strInsInsDate = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtIssDt")).Text;
            //    string strInsStartDt = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtStartDt")).Text;
            //    string strInsEndDt = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtEndDt")).Text;
            //    double dblInsExchageRate = CommValue.NUMBER_VALUE_0_0;
            //    double dblInsFeeExpAmt = CommValue.NUMBER_VALUE_0_0;
            //    int dbintQty = CommValue.NUMBER_VALUE_0;
            //    double dblInsVat = CommValue.NUMBER_VALUE_0_0;

            //    string strInsRoomNo = ((Literal)lvRentalMngList.Items[e.ItemIndex].FindControl("ltInsRoomNo")).Text;
            //    string strInsBillNm = ((Literal)lvRentalMngList.Items[e.ItemIndex].FindControl("ltInsBillNm")).Text;
            //    string strInsBillCd = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtHfBillCd")).Text;
            //    string strInsExchageRate = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtInsExchageRate")).Text;
                
                
                
                
                
                
            //    string strInsRemark = ((TextBox)lvRentalMngList.Items[e.ItemIndex].FindControl("txtInsRemark")).Text;

            //    if (!string.IsNullOrEmpty(strInsExchageRate))
            //    {
            //        dblInsExchageRate = double.Parse(strInsExchageRate);
            //    }

            //    if (!string.IsNullOrEmpty(strInsAmtViNo))
            //    {
            //        dblInsFeeExpAmt = double.Parse(strInsAmtViNo);
            //    }
            //    if (!string.IsNullOrEmpty(strInsVAT))
            //    {
            //        dblInsVat = double.Parse(strInsVAT);
            //    }

            //    if (!string.IsNullOrEmpty(intQty))
            //    {
            //        dbintQty = Int32.Parse(intQty);
            //    }

            //    // 오피스 / 리테일 데이터 수정
            //    // KN_USP_MNG_UPDATE_RENTALMNGFEE_M00
            //    InvoiceMngBlo.ModifyHoadonListInfo(strInvoiceNo, strSerialNo, strPrintSeq, Int32.Parse(strPrintDetSeq), strInsYear, strInsMonth, dblInsExchageRate,
            //                                       dblInsFeeExpAmt, strInsDescription, strInsUserNm, strInsUserCd, strUserSeq, strInsAddress, strInsDetAddress,
            //                                       hfRentCd.Value, strInsBillCd, strInsRemark);

            //    LoadData();

            //    StringBuilder sbWarning = new StringBuilder();
            //    sbWarning.Append("alert('");
            //    sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
            //    sbWarning.Append("');");

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Modify", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            //}
            //catch (Exception ex)
            //{
            //    ErrLogger.MakeLogger(ex);
            //}
        }

        ///// <summary>
        ///// 출력버튼 클릭시 이벤트 처리
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool isChestNut = CommValue.AUTH_VALUE_TRUE;
        //        int intCheckRow = CommValue.NUMBER_VALUE_0;

        //        string strPrintOutDt = string.Empty;
        //        int intPrintOutSeq = CommValue.NUMBER_VALUE_0;
        //        string strPrintSeq = string.Empty;
        //        int intPrintDetSeq = CommValue.NUMBER_VALUE_0;
        //        string strInvoceNo = string.Empty;
        //        string strSearchData = string.Empty;

        //        for (int intTmpI = CommValue.NUMBER_VALUE_0; intTmpI < lvRentalMngList.Items.Count; intTmpI++)
        //        {
        //            // CheckBox Check 여부에 따라서 데이터 임시 테이블에 등록후 해당 코드 받아올것
        //            // 해당 프린트 출력자 정보 등록해줄 것.
        //            if (!string.IsNullOrEmpty(hfRentCd.Value))
        //            {
        //                if (((CheckBox)lvRentalMngList.Items[intTmpI].FindControl("chkboxList")).Checked)
        //                {
        //                    string strItemCd = ((TextBox)lvRentalMngList.Items[intTmpI].FindControl("txtHfBillCd")).Text;

        //                    if (!hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT) &&
        //                        !hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA) &&
        //                        !hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB))
        //                    {
        //                        isChestNut = CommValue.AUTH_VALUE_FALSE;
        //                    }
        //                    else
        //                    {
        //                        // 아파트 관리비와 아파트 수도 전기세 이외의 데이터 존재시 경남비나 수납증 출력
        //                        if (((hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT)) ||
        //                             (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTA)) ||
        //                             (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APTB))) &&
        //                            ((!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_MNGFEE)) &&
        //                             (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_WATERCHARGE)) &&
        //                             (!strItemCd.Equals(CommValue.ITEM_TYPE_VALUE_ELECCHARGE))))
        //                        {
        //                            // 해당 사항은 체스넛 수납증 출력
        //                            isChestNut = CommValue.AUTH_VALUE_FALSE;
        //                        }
        //                    }

        //                    strPrintSeq = ((TextBox)lvRentalMngList.Items[intTmpI].FindControl("txtHfPrintSeq")).Text;

        //                    if (!string.IsNullOrEmpty(((TextBox)lvRentalMngList.Items[intTmpI].FindControl("txtHfPrintDetSeq")).Text))
        //                    {
        //                        intPrintDetSeq = Int32.Parse(((TextBox)lvRentalMngList.Items[intTmpI].FindControl("txtHfPrintDetSeq")).Text);
        //                    }

        //                    strInvoceNo = ((TextBox)lvRentalMngList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;

        //                    if (!string.IsNullOrEmpty(strInvoceNo))
        //                    {
        //                        strSearchData = strSearchData + "[" + strInvoceNo + "],";
        //                    }

        //                    // KN_USP_SET_INSERT_PRINTINFO_S04
        //                    DataTable dtPrintReturn = ReceiptMngBlo.RegistryTempPrintOutList(strPrintOutDt, intPrintOutSeq, strPrintSeq, intPrintDetSeq);

        //                    if (dtPrintReturn != null)
        //                    {
        //                        if (dtPrintReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
        //                        {
        //                            strPrintOutDt = dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutDt"].ToString();
        //                            intPrintOutSeq = Int32.Parse(dtPrintReturn.Rows[CommValue.NUMBER_VALUE_0]["PrintOutSeq"].ToString());
        //                            intCheckRow++;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        // 선택 사항이 있는지 없는지 체크
        //        if (intCheckRow > CommValue.NUMBER_VALUE_0)
        //        {
        //            StringBuilder sbPrintOut = new StringBuilder();

        //            if (isChestNut)
        //            {
        //                sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupCombineReciptCN.aspx?Datum0=" + strPrintOutDt + "&Datum1=" + intPrintOutSeq + "\", \"Reciept\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
        //            }
        //            else
        //            {
        //                strSearchData = strSearchData.Substring(0, strSearchData.Length - 1);
        //                sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupKeangnamHoadonList.aspx?Datum0=" + hfRentCd.Value + "&Datum1=" + strSearchData + "\", \"Hoadon\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
        //            }

        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "RecieptPrint", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
        //        }
        //        else
        //        {
        //            // 화면 초기화
        //            LoadData();

        //            // 선택된 대상 없음
        //            StringBuilder sbNoSelection = new StringBuilder();

        //            sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

        //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrLogger.MakeLogger(ex);
        //    }
        //}

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
               
                if (dtMaster.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    //string serial = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
                    //if (!string.IsNullOrEmpty(serial))
                    //{
                        reft_seq = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
                    //}
                    if (!string.IsNullOrEmpty((reft_seq)))
                    {
                        LoadDetails((reft_seq));
                    }

                    //if (ddlInvoiceYN.SelectedValue.ToString() == "Y")
                    //{

                    //    ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = false;
                    //    ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = false;

                    //}
                    //else
                    //{
                    //    ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = true;
                    //    ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = true;

                    //}
                }
                else
                {
                    //lvRentalMngList.DataSource = null;
                    //lvRentalMngList.DataBind();
                    //lvRentalMngList.Items.Clear();
                }
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


                Literal ltInvoiceNoP = (Literal)iTem.FindControl("ltInvoiceNoP");
                if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
                {
                    ltInvoiceNoP.Text = drView["InvoiceNo"].ToString();
                }

                //TextBox txtHfInvoiceNoP = (TextBox)iTem.FindControl("txtHfInvoiceNoP");
                //if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
                //{
                //    txtHfInvoiceNo.Text = drView["InvoiceNo"].ToString();
                //}


                TextBox txtHfrefSerialNoP = (TextBox)iTem.FindControl("txtHfrefSerialNoP");
                if (!string.IsNullOrEmpty(drView["REF_SERIAL_NO"].ToString()))
                {
                    txtHfrefSerialNoP.Text = drView["REF_SERIAL_NO"].ToString();
                }

                Literal ltInsYearP = (Literal)iTem.FindControl("ltInsYearP");

                if (!string.IsNullOrEmpty(drView["SvcYear"].ToString()))
                {
                    ltInsYearP.Text = drView["SvcYear"].ToString();
                }

                Literal ltInsMonthP = (Literal)iTem.FindControl("ltInsMonthP");

                if (!string.IsNullOrEmpty(drView["SvcMM"].ToString()))
                {
                    ltInsMonthP.Text = drView["SvcMM"].ToString();
                }
                //TextBox txtHfYearP = (TextBox)iTem.FindControl("txtHfYearP");
                //if (!string.IsNullOrEmpty(drView["SvcYear"].ToString()))
                //{
                //    txtHfYearP.Text = drView["SvcYear"].ToString();
                //}
                //TextBox txtHfMonthP = (TextBox)iTem.FindControl("txtHfMonthP");
                //if (!string.IsNullOrEmpty(drView["SvcMM"].ToString()))
                //{
                //    txtHfMonthP.Text = drView["SvcMM"].ToString();
                //}

                Literal ltInsRoomNoP = (Literal)iTem.FindControl("ltInsRoomNoP");

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    ltInsRoomNoP.Text = drView["RoomNo"].ToString();
                }

                //TextBox txtHfRoomNoP = (TextBox)iTem.FindControl("txtHfRoomNoP");
                //if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                //{
                //    txtHfRoomNoP.Text = drView["RoomNo"].ToString();
                //}

                Literal ltInsFeeNameP = (Literal)iTem.FindControl("ltInsFeeNameP");

                if (!string.IsNullOrEmpty(drView["FeeName"].ToString()))
                {
                    ltInsFeeNameP.Text = TextLib.StringDecoder(drView["FeeName"].ToString());
                }

                Literal ltInsUserNmP = (Literal)iTem.FindControl("ltInsUserNmP");

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    ltInsUserNmP.Text = TextLib.StringDecoder(drView["UserNm"].ToString());
                }

                Literal ltInsUPriceP = (Literal)iTem.FindControl("ltInsUPriceP");

                if (!string.IsNullOrEmpty(drView["UnitPrice"].ToString()))
                {
                    ltInsUPriceP.Text = TextLib.MakeVietIntNo(double.Parse(drView["UnitPrice"].ToString()).ToString("###,##0"));
                }

                Literal ltInsAmtViNoP = (Literal)iTem.FindControl("ltInsAmtViNoP");

                if (!string.IsNullOrEmpty(drView["TotSellingPrice"].ToString()))
                {
                    ltInsAmtViNoP.Text = TextLib.MakeVietIntNo(double.Parse(drView["TotSellingPrice"].ToString()).ToString("###,##0"));
                }

                Literal ltnsIssDtP = (Literal)iTem.FindControl("ltnsIssDtP");

                if (!string.IsNullOrEmpty(drView["IssuingDate"].ToString()))
                {
                    ltnsIssDtP.Text = TextLib.MakeDateEightDigit(drView["IssuingDate"].ToString());
                }

                //string refSerialNo = ((Literal)lvPrintoutList.Items[dataItem.DataItemIndex].FindControl("txtHfrefSerialNo1")).Text;

                intRowsCnt++;
            }
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
            //lvRentalMngList.DataSource = null;
            //lvRentalMngList.DataBind();
            //var reft = txthfrefSerialNo.Value;
            //LoadDetails(reft);

            //if (ddlInvoiceYN.SelectedValue.ToString() == "Y")
            //{

            //    ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = false;
            //    ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = false;

            //}
            //else
            //{
            //    ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = true;
            //    ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = true;

            //}

        }
        protected void LoadDetails(string refSeq)
        {
            //lvRentalMngList.DataSource = null;
            //lvRentalMngList.DataBind();
            //var  dtDetail = InvoiceMngBlo.SelectHoadonListDetail(refSeq);
            //lvRentalMngList.DataSource = dtDetail;
            //lvRentalMngList.DataBind();        
        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var reft = txthfrefSerialNo.Value;
                var reft_seq = string.Empty;
                string strRentCd = hfRentCd.Value;


                //string strInvoiceNo = ((Literal)lvRentalMngList.Items[0].FindControl("ltInvoiceNo")).Text;
                //string strInvoiceNo = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Text;
                ////string strInsBillCd = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfBillCd")).Text;
                //string strInsBillCd = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlBillCd")).SelectedValue.ToString();

                ////string strInvoiceNo = ((Literal)lvRentalMngList.FindControl("ltInvoiceNo")).Text;
               
                ////string strPrintSeq = ((TextBox)lvRentalMngList.FindControl("txtHfPrintSeq")).Text;
                ////string strPrintDetSeq = ((TextBox)lvRentalMngList.FindControl("txtHfPrintDetSeq")).Text;
                ////string strUserSeq = ((TextBox)lvRentalMngList.FindControl("txtHfUserSeq")).Text;
                //string strInsUserNm = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsUserNm")).Text;

                //string strSerialNo = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfSerialNo")).Text;
                //string strInsAddress = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsAddress")).Text;
                //string strInsDetAddress = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsDetAddress")).Text;
                //string strInsUserCd = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsUserCd")).Text;
                //string strPaymentDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfPaymentDt")).Text;
                ////string strInsYear = ((Literal)lvRentalMngList.FindControl("ltInsYear")).Text;
                ////string strInsMonth = ((Literal)lvRentalMngList.FindControl("ltInsMonth")).Text;

                ////string strInsYear = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfYear")).Text;
                ////string strInsMonth = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfMonth")).Text;
                //string strInsYear = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvYear")).Text;
                //string strInsMonth = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvMonth")).Text;

                //string strInsDescription = ((TextBox)lvRentalMngList.Items[0].FindControl("txtltnsDescription")).Text;
                //string intQty = ((TextBox)lvRentalMngList.Items[0].FindControl("txtQty1")).Text;
                //string strInsAmtViNo = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsAmtViNo")).Text;
                //string strInsVAT = ((TextBox)lvRentalMngList.Items[0].FindControl("txtVat1")).Text;
                //string strInsInsDate = ((TextBox)lvRentalMngList.Items[0].FindControl("txtIssDt")).Text;
                //string strInsStartDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtStartDt")).Text;
                //string strInsEndDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtEndDt")).Text;
                //string strInsExchageRate = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsExchageRate")).Text;


                double dblInsExchageRate = CommValue.NUMBER_VALUE_0_0;
                double dblInsAmtViNo = CommValue.NUMBER_VALUE_0_0;
                int dbintQty = CommValue.NUMBER_VALUE_0;
                double dblInsVat = CommValue.NUMBER_VALUE_0_0;   

                //if (!string.IsNullOrEmpty(strInsAmtViNo))
                //{
                //    dblInsAmtViNo = double.Parse(strInsAmtViNo);
                //}
                //if (!string.IsNullOrEmpty(strInsVAT))
                //{
                //    dblInsVat = double.Parse(strInsVAT);
                //}

                //if (!string.IsNullOrEmpty(intQty))
                //{
                //    dbintQty = Int32.Parse(intQty);
                //}

                //if (!string.IsNullOrEmpty(strInsExchageRate))
                //{
                //    dblInsExchageRate = double.Parse(strInsExchageRate);
                //}


                        // 오피스 / 리테일 데이터 수정
                        // KN_USP_MNG_UPDATE_RENTALMNGFEE_M00
                     //InvoiceMngBlo.UpdateHoadonListNew(strInvoiceNo,strInsBillCd, strSerialNo, strInsYear, strInsMonth, strInsUserNm, strInsAddress, strInsDetAddress,
                     //                                strInsUserCd,  strInsDescription,  strPaymentDt, strInsStartDt, strInsEndDt, dblInsAmtViNo, dblInsVat, dblInsExchageRate,
                     //                               dblInsAmtViNo,  strInsInsDate,  dbintQty,  reft);

                        LoadData();
                        if (dtMaster.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            //string serial = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
                            //if (!string.IsNullOrEmpty(serial))
                            //{
                            reft_seq = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
                            //}
                            if (!string.IsNullOrEmpty((reft_seq)))
                            {
                                LoadDetails((reft_seq));
                            }

                            if (ddlInvoiceYN.SelectedValue.ToString() == "Y")
                            {

                                //((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = false;
                                //((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = false;

                            }
                            else
                            {
                                //((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = true;
                                //((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = true;

                            }
                        }
                        else
                        {
                            //lvRentalMngList.DataSource = null;
                            //lvRentalMngList.DataBind();
                            //lvRentalMngList.Items.Clear();
                        }                        
                        

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

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
           
        }

        protected void lnkbtnReprint_Click(object sender, EventArgs e)
        {
            try
            {

                var reft = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
            
            string refPrintBundleNo = string.Empty;
            DataTable dtreturn = new DataTable();

          
            //dtNull = InvoiceMngBlo.UpdatingNullPrintBundleNo(hfRentCd.Value);

            //var refSerialNo = ((TextBox)lvRentalMngList.Items[0].FindControl("txthfBundleSeq")).Text;            

            if (string.IsNullOrEmpty(refPrintBundleNo))
            {
                refPrintBundleNo= reft;
            }

            //========================Update PRINT_BUNDLE_NO = ''============
            InvoiceMngBlo.UpdatelHoadonReprintList1(refPrintBundleNo);

            //=======Update PRINT_BUNDLE_NO = REF_SERIAL_NO =================
            InvoiceMngBlo.UpdatelHoadonReprintList(hfRentCd.Value, reft, refPrintBundleNo);
          
                var sbPrintOut = new StringBuilder();
                dtreturn = InvoiceMngBlo.SelectReprintHoadon(reft);
                
            //=============================Print=============================
                if (dtreturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + refPrintBundleNo + "');", CommValue.AUTH_VALUE_TRUE);


                   
                }
                // 화면 초기화
                LoadData();
                LoadDetails(reft);
            
            }
             catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

            
        }

        protected void lvRentalMngList_SelectedIndexChanged(object sender, EventArgs e)
        {
           // ((Literal)lvRentalMngList.Items[0].FindControl("ltInvoiceNo")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).SelectedValue.ToString(); 
        }
        protected void ddllvYear_SelectedIndexChanged(object sender, EventArgs e)
        {

            //((TextBox)lvRentalMngList.Items[0].FindControl("txtHfYear")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvYear")).SelectedValue.ToString();

        }
        protected void ddllvMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            //((TextBox)lvRentalMngList.Items[0].FindControl("txtHfMonth")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvMonth")).SelectedValue.ToString();

        }
        protected void ddlInvoiceChoice_SelectedIndexChanged(object sender, EventArgs e)
        {

            //((TextBox)lvRentalMngList.Items[0].FindControl("txtHfMonth")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvMonth")).SelectedValue.ToString();
            //((Literal)lvRentalMngList.Items[0].FindControl("ltInvoiceNo")).Text = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).SelectedValue.ToString(); 
        }
               

        protected void ddlInvoiceYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            var reft_seq = string.Empty;
            LoadData();

            if (dtMaster.Rows.Count > CommValue.NUMBER_VALUE_0)
            {
                reft_seq = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
               
                if (!string.IsNullOrEmpty((reft_seq)))
                {
                    LoadDetails((reft_seq));
                }

                if (ddlInvoiceYN.SelectedValue.ToString() == "Y")
                {

                    //((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = false;
                    //((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = false;

                }
                else
                {
                    //((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = true;
                    //((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = true;

                }
            }
            else
            {
                //lvRentalMngList.DataSource = null;
                //lvRentalMngList.DataBind();
                //lvRentalMngList.Items.Clear();
            }            

        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {

            LoadDetails(null);
            //try
            //{
            //    // 세션체크
            //    AuthCheckLib.CheckSession();
            //    var reft = txthfrefSerialNo.Value;
            //    var reft_seq = string.Empty;
            //    string strRentCd = hfRentCd.Value;


            //    //string strInvoiceNo = ((Literal)lvRentalMngList.Items[0].FindControl("ltInvoiceNo")).Text;
            //    string strInvoiceNo = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Text;
            //    //string strInsBillCd = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfBillCd")).Text;
            //    string strInsBillCd = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlBillCd")).SelectedValue.ToString();

            //    //string strInvoiceNo = ((Literal)lvRentalMngList.FindControl("ltInvoiceNo")).Text;

            //    //string strPrintSeq = ((TextBox)lvRentalMngList.FindControl("txtHfPrintSeq")).Text;
            //    //string strPrintDetSeq = ((TextBox)lvRentalMngList.FindControl("txtHfPrintDetSeq")).Text;
            //    //string strUserSeq = ((TextBox)lvRentalMngList.FindControl("txtHfUserSeq")).Text;
            //    string strInsUserNm = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsUserNm")).Text;

            //    string strSerialNo = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfSerialNo")).Text;
            //    string strInsAddress = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsAddress")).Text;
            //    string strInsDetAddress = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsDetAddress")).Text;
            //    string strInsUserCd = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsUserCd")).Text;
            //    string strPaymentDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfPaymentDt")).Text;
            //    //string strInsYear = ((Literal)lvRentalMngList.FindControl("ltInsYear")).Text;
            //    //string strInsMonth = ((Literal)lvRentalMngList.FindControl("ltInsMonth")).Text;

            //    //string strInsYear = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfYear")).Text;
            //    //string strInsMonth = ((TextBox)lvRentalMngList.Items[0].FindControl("txtHfMonth")).Text;
            //    string strInsYear = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvYear")).Text;
            //    string strInsMonth = ((DropDownList)lvRentalMngList.Items[0].FindControl("ddllvMonth")).Text;

            //    string strInsDescription = ((TextBox)lvRentalMngList.Items[0].FindControl("txtltnsDescription")).Text;
            //    string intQty = ((TextBox)lvRentalMngList.Items[0].FindControl("txtQty1")).Text;
            //    string strInsAmtViNo = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsAmtViNo")).Text;
            //    string strInsVAT = ((TextBox)lvRentalMngList.Items[0].FindControl("txtVat1")).Text;
            //    string strInsInsDate = ((TextBox)lvRentalMngList.Items[0].FindControl("txtIssDt")).Text;
            //    string strInsStartDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtStartDt")).Text;
            //    string strInsEndDt = ((TextBox)lvRentalMngList.Items[0].FindControl("txtEndDt")).Text;
            //    string strInsExchageRate = ((TextBox)lvRentalMngList.Items[0].FindControl("txtInsExchageRate")).Text;


            //    double dblInsExchageRate = CommValue.NUMBER_VALUE_0_0;
            //    double dblInsAmtViNo = CommValue.NUMBER_VALUE_0_0;
            //    int dbintQty = CommValue.NUMBER_VALUE_0;
            //    double dblInsVat = CommValue.NUMBER_VALUE_0_0;

            //    if (!string.IsNullOrEmpty(strInsAmtViNo))
            //    {
            //        dblInsAmtViNo = double.Parse(strInsAmtViNo);
            //    }
            //    if (!string.IsNullOrEmpty(strInsVAT))
            //    {
            //        dblInsVat = double.Parse(strInsVAT);
            //    }

            //    if (!string.IsNullOrEmpty(intQty))
            //    {
            //        dbintQty = Int32.Parse(intQty);
            //    }

            //    if (!string.IsNullOrEmpty(strInsExchageRate))
            //    {
            //        dblInsExchageRate = double.Parse(strInsExchageRate);
            //    }


            //    // 오피스 / 리테일 데이터 수정
            //    // KN_USP_MNG_UPDATE_RENTALMNGFEE_M00
            //    InvoiceMngBlo.UpdateHoadonListNew(strInvoiceNo, strInsBillCd, strSerialNo, strInsYear, strInsMonth, strInsUserNm, strInsAddress, strInsDetAddress,
            //                                    strInsUserCd, strInsDescription, strPaymentDt, strInsStartDt, strInsEndDt, dblInsAmtViNo, dblInsVat, dblInsExchageRate,
            //                                   dblInsAmtViNo, strInsInsDate, dbintQty, reft);

            //    LoadData();
            //    if (dtMaster.Rows.Count > CommValue.NUMBER_VALUE_0)
            //    {
            //        //string serial = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
            //        //if (!string.IsNullOrEmpty(serial))
            //        //{
            //        reft_seq = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfrefSerialNoP")).Text;
            //        //}
            //        if (!string.IsNullOrEmpty((reft_seq)))
            //        {
            //            LoadDetails((reft_seq));
            //        }

            //        if (ddlInvoiceYN.SelectedValue.ToString() == "Y")
            //        {

            //            ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = false;
            //            ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = false;

            //        }
            //        else
            //        {
            //            ((Literal)lvRentalMngList.Items[0].FindControl("ltnnInvoiceChoice")).Visible = true;
            //            ((DropDownList)lvRentalMngList.Items[0].FindControl("ddlInvoiceChoice")).Visible = true;

            //        }
            //    }
            //    else
            //    {
            //        lvRentalMngList.DataSource = null;
            //        lvRentalMngList.DataBind();
            //        lvRentalMngList.Items.Clear();
            //    }


            //    StringBuilder sbWarning = new StringBuilder();
            //    sbWarning.Append("alert('");
            //    sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
            //    sbWarning.Append("');");

            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Modify", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            //}
            //catch (Exception ex)
            //{
            //    ErrLogger.MakeLogger(ex);
            //}              
        }
        

    }
    }
