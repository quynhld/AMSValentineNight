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
using KN.Resident.Biz;
using KN.Settlement.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;

namespace KN.Web.Settlement.Balance
{
    public partial class TransferInvoiceAPT : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        string strInit = string.Empty;
        int intInit = CommValue.NUMBER_VALUE_0;
        object objTag = new object();

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
                    // 2일이 지난 임시 출력 내용 삭제
                    // KN_USP_SET_DELETE_PRINTINFO_M00
                    ReceiptMngBlo.RemoveTempPrintList();

                    if (CheckParams())
                    {
                        InitControls();
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

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();
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
            //ltRoomNo.Text = TextNm["ROOMNO"];
            ltAmount.Text = TextNm["PAYMENT"];
            //lnkbtnIssuing.Text = TextNm["ISSUING"];

            txtSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            //lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";            
            
        }

        protected void LoadData()
        {
            string strSearchNm = string.Empty;
            string strSearchRoomNo = string.Empty;
            if (lvPrintoutList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                //chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
            }

            if (!string.IsNullOrEmpty(txtRoomNo.Text))
            {
                strSearchRoomNo = txtRoomNo.Text;
            }

            strInit = CommValue.AUTH_VALUE_EMPTY;
            intInit = CommValue.NUMBER_VALUE_0;
            var sPayDt = txtSearchDt.Text.Replace("-", "");
            var eDt = txtESearchDt.Text.Replace("-", "");
            // KN_USP_SET_SELECT_APT_HOADONINFO_S04
            var dtReturn = ReceiptMngBlo.SelectInvoiceAptForTransfer(hfRentCd.Value, txtRoomNo.Text, sPayDt, eDt,
                                                                        ddlItemCd.SelectedValue, txtCompanyNm.Text, rbIsTransfer.SelectedValue);

            if (dtReturn != null)
            {
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();

                chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
            }
            chkAll.Enabled = !rbIsTransfer.SelectedValue.Equals(CommValue.CHOICE_VALUE_YES);    
        }


        /// <summary>
        /// 
        /// </summary>
        private void MakePaymentDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItemCd.Items.Clear();

            ddlItemCd.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (DataRow dr in dtReturn.Select())
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
                ltPeriod.Text = TextLib.MakeDateSixDigit(drView["Period"].ToString());
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
            var txthfFeeTyDt = (TextBox)iTem.FindControl("txthfFeeTyDt");

            if (!string.IsNullOrEmpty(drView["BillCdDt"].ToString()))
            {
                txthfFeeTyDt.Text = TextLib.StringDecoder(drView["BillCdDt"].ToString());
            }

            var txtHfInvoiceNo = (TextBox)iTem.FindControl("txtHfInvoiceNo");

            if (!string.IsNullOrEmpty(drView["P_INVOICE"].ToString()))
            {
                txtHfInvoiceNo.Text = TextLib.StringDecoder(drView["P_INVOICE"].ToString());
            }

            var txthfDes = (TextBox)iTem.FindControl("txthfDes");

            if (!string.IsNullOrEmpty(drView["Desciption"].ToString()))
            {
                txthfDes.Text = TextLib.StringDecoder(drView["Desciption"].ToString());
            }
            

            hfBillCd.Value = txtHfBillCd.Text;

            if (!string.IsNullOrEmpty(drView["NetPrice"].ToString()))
            {
                var ltNet = (Literal)iTem.FindControl("ltNet");
                ltNet.Text = TextLib.MakeVietIntNo(double.Parse(drView["NetPrice"].ToString()).ToString("###,##0"));                  
            }

            if (!string.IsNullOrEmpty(drView["VatAmt"].ToString()))
            {
                var ltVat = (Literal)iTem.FindControl("ltVAT");
                ltVat.Text = TextLib.MakeVietIntNo(double.Parse(drView["VatAmt"].ToString()).ToString("###,##0"));
            }

            var ltPaidDate = (Literal)iTem.FindControl("ltPaidDate");
            if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
            {
                ltPaidDate.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());                      
            }

            var ltInvoiceNo = (Literal)iTem.FindControl("ltInvoiceNo");
            if (!string.IsNullOrEmpty(drView["P_INVOICE"].ToString()))
            {
                ltInvoiceNo.Text = drView["P_INVOICE"].ToString();                      
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
            var ltSlipNo = (Literal)iTem.FindControl("ltSlipNo");
            if (!string.IsNullOrEmpty(drView["SlipNo"].ToString()))
            {
                ltSlipNo.Text = drView["SlipNo"].ToString();
            }
            var ltCreditAcc = (Literal)iTem.FindControl("ltCreditAcc");
            if (!string.IsNullOrEmpty(drView["CreditCd"].ToString()))
            {
                ltCreditAcc.Text = drView["CreditCd"].ToString();
            }
            var chkboxList = (CheckBox)iTem.FindControl("chkboxList");
            if (rbIsTransfer.SelectedValue.Equals(CommValue.CHOICE_VALUE_YES))
            {
                chkboxList.Visible = false;
            }
            intRowsCnt++;
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            //bool isAllCheck = chkAll.Checked;

            //try
            //{
            //    CheckAll(isAllCheck);
            //}
            //catch (Exception ex)
            //{
            //    ErrLogger.MakeLogger(ex);
            //}
        }

        /// <summary>
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
        //private void CheckAll(bool isAllCheck)
        //{
        //    for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
        //    {
        //        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
        //        {
        //            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
        //        }
        //    }
        //}

        /// <summary>
        /// 리스트 각 행별 체크 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string strInvoiceNo = string.Empty;

        //        int intTotalCount = CommValue.NUMBER_VALUE_0;
        //        int intCheckCount = CommValue.NUMBER_VALUE_0;

        //        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
        //        {
        //            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
        //            {
        //                intTotalCount++;

        //                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
        //                {
        //                    strInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;
        //                    intCheckCount++;
        //                }
        //                else
        //                {
        //                    string strOtherInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;

        //                    if (strOtherInvoiceNo != "" && strInvoiceNo.Equals(strOtherInvoiceNo))
        //                    {
        //                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_TRUE;
        //                    }
        //                }
        //            }

        //            TextBox txtNewDt = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInsRegDt"));
        //            HiddenField hfNewDt = ((HiddenField)lvPrintoutList.Items[intTmpI].FindControl("hfInsRegDt"));
        //            txtNewDt.Text = TextLib.MakeDateEightDigit(hfNewDt.Value.Replace("-", ""));
        //        }

        //        if (intTotalCount == intCheckCount)
        //        {
        //          //  chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
        //        }
        //        else
        //        {
        //           // chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrLogger.MakeLogger(ex);
        //    }
        //}

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
                var isOk = true;
                var sbWarning = new StringBuilder();
                sbWarning.Append("CloseLoading();alert('");

                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }
                if (rbIsTransfer.SelectedValue.Equals(CommValue.CHOICE_VALUE_YES))
                {
                    return;
                }
                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;
                    var invoiceNO = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfInvoiceNo")).Text;
                    var compNo = Session["CompCd"].ToString();
                    var rentCode = hfRentCd.Value;
                    var feeTy = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfBillCd")).Text;
                    var paymentCode = "";
                    var userSeq = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsRoomNo")).Text;
                    var tenantsNm = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsRoomNo")).Text;
                    var description = ((TextBox)lvPrintoutList.Items[i].FindControl("txthfDes")).Text;
                    var roomNo = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsRoomNo")).Text;
                    var netAmount = ((Literal)lvPrintoutList.Items[i].FindControl("ltNet")).Text.Replace(".", "");
                    var vatAmount = ((Literal)lvPrintoutList.Items[i].FindControl("ltVAT")).Text.Replace(".", "");
                    var sendType = "INVOICE";
                    var memIP = Session["UserIP"].ToString();

                    //string  = "";
                    var paymentDt = ((Literal)lvPrintoutList.Items[i].FindControl("ltPaidDate")).Text.Replace("-","");
                    //var insMemNo = Session["MemNo"].ToString();
                    var insMemNo = Session["UserId"].ToString();
                    var exchangeRate = "1";
                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    var billType = ((TextBox)lvPrintoutList.Items[i].FindControl("txthfBillTy")).Text;

                    var debitAcc = ((Literal)lvPrintoutList.Items[i].FindControl("ltDebitAcc")).Text;
                    var creditAcc = ((Literal)lvPrintoutList.Items[i].FindControl("ltCreditAcc")).Text;
                    var seq = Int32.Parse(((TextBox)lvPrintoutList.Items[i].FindControl("txtHfPrintDetSeq")).Text);
                    var feeTyDt = ((TextBox)lvPrintoutList.Items[i].FindControl("txthfFeeTyDt")).Text;

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

                    var dbytotAmount = dbnetAmount + dbvatAmount;
                    
                     var  dsReturn =  MngPaymentBlo.TransferStatementApt(invoiceNO, compNo, rentCode, feeTy, paymentCode, userSeq, tenantsNm, description,
                                                         roomNo, dbnetAmount, dbvatAmount, dbytotAmount, sendType, paymentDt, insMemNo, dblInsExchageRate, refSeq, billType, memIP,debitAcc,creditAcc,vatAcc,feeTyDt,seq);

                     if (dsReturn.Tables.Count > 0)
                     {
                         if (dsReturn.Tables[0].Rows[0][0].ToString().Equals("Y"))
                         {

                         }
                         else
                         {
                             isOk = false;
                             sbWarning.Append(dsReturn.Tables[0].Rows[0][1] + " on invoice " + invoiceNO);
                             break;
                         }
                     }
                     else
                     {
                         isOk = false;
                         sbWarning.Append("Check error on invoice " + invoiceNO);
                         break;                         
                     }
                    intCheckRow++;
                }                
                if (isOk)
                {
                    sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
                }
                sbWarning.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                LoadData();
            }
            catch (Exception ex)
            {
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

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlCompNo_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlStartFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //InitFloorDdl(ddlEndFloor, Int32.Parse(ddlStartFloor.SelectedValue));
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
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

        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
            //string conmpareVal1 = lvPrintoutList.Items[e].FindControl("ltInsRoomNo").ToString();

            CheckBox cb = (CheckBox)sender;
            ListViewItem item = (ListViewItem)cb.NamingContainer;
            ListViewDataItem dataItem = (ListViewDataItem)item;

            bool status = (((CheckBox)lvPrintoutList.Items[dataItem.DataItemIndex].FindControl("chkboxList")).Checked == true) ? true : false;
            string code = ((Literal)lvPrintoutList.Items[dataItem.DataItemIndex].FindControl("ltInsRoomNo")).Text;

            for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
            {
                if (status)
                {
                    if (((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked == false && ((Literal)lvPrintoutList.Items[i].FindControl("ltInsRoomNo")).Text.Equals(code))
                    {
                        ((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked = true;
                    }
                }
                else
                {
                    if (((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked == true && ((Literal)lvPrintoutList.Items[i].FindControl("ltInsRoomNo")).Text.Equals(code))
                    {
                        ((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked = false;
                    }
                }

            }

        }


        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            hfUserSeq.Value = ((TextBox)e.Item.FindControl("txtHfUserSeq")).Text;
            hfPayDt.Value = ((TextBox)e.Item.FindControl("txtInsRegDt")).Text;
        }

        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                //DataSet dtSource = new DataSet();
                // 세션체크
                AuthCheckLib.CheckSession();

                var sPayDt = txtSearchDt.Text.Replace("-", "");
                var eDt = txtESearchDt.Text.Replace("-", "");

                var dtSource = ReceiptMngBlo.SelectExcelInvoiceAptForTransfer(hfRentCd.Value, txtRoomNo.Text, sPayDt, eDt,
                                                                       ddlItemCd.SelectedValue, txtCompanyNm.Text, rbIsTransfer.SelectedValue);

                if (dtSource.Tables[0].Rows.Count <= 0)
                {
                    return;
                }            

                var fileName = Server.UrlEncode("Transfer List ").Replace("+", " ") ;

                GenerateExcel(dtSource.Tables[0], fileName, fileName);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        public void GenerateExcel(DataTable tbl, string excelSheetName, string fileName)
        {
            using (var pck = new ExcelPackage())
            {
                //Create the worksheet
                var ws = pck.Workbook.Worksheets.Add(excelSheetName);

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(tbl, true, TableStyles.None);
                ////نمایش جمع ستون هزینه‌های ماه‌ها
                var tbls = ws.Tables[0];

                tbls.ShowTotal = true;
                //Set Sum 
                tbls.Columns[4].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[3].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[2].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:M1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:M1"].AutoFitColumns();
                using (var col = ws.Cells[2, 2, 2 + tbl.Rows.Count, 5])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                //using (var col = ws.Cells[2, 6, 2 + tbl.Rows.Count, 6])
                //{
                //    col.Style.Numberformat.Format = "dd/MM/yyyy";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
        }
    }
}