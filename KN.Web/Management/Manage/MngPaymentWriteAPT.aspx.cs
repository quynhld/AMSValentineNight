using System;
using System.Data;
using System.IO;
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
using KN.Settlement.Biz;

namespace KN.Web.Management.Manage
{
    public partial class MngPaymentWriteAPT : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (IsPostBack) return;
                
                CheckParam();
                InitControls();
               // LoadUserInfo();
               // LoadData();
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
            //hfUserSeq.Value = Request.Params[Master.PARAM_DATA1];
            hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
        }
        protected void InitControls()
        {

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            // 섹션코드 조회
            // 차종 조회
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlPaymentTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);

            txtAmount.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtExRate.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            //txtSearchDt.Text = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.ToString("MM");
            MakeFeeTypeDdl();
            MakeAccountDdl(ddlTransfer);
            CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaidCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_PAYMENT);
            lnkbtnRegist.Visible = Master.isWriteAuthOk;
            lnkAdjustment.Visible = Master.isModDelAuthOk;            
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_MNG_SELECT_PAYMENTINFO_S05
            var currentDt = txtSearchDt.Text.Replace("-", "");
            var eCurrentDt = txtESearchDt.Text.Replace("-", "");
            var dsReturn = MngPaymentBlo.ListPaymentInfoApt(hfRentCd.Value, ddlFeeType.SelectedValue, currentDt, txtRoomNo.Text, txtCompanyNm.Text,eCurrentDt,ddlPaidCd.SelectedValue);

            lvPaymentList.DataSource = dsReturn;
            lvPaymentList.DataBind();
            ResetInputControls();
        }

        private void LoadUDetailPayment(int seqDt,string pSeq)
        {
            AuthCheckLib.CheckSession();
            // KN_USP_MNG_SELECT_PAYMENTINFO_S07
            lvPaymentDetails.Items.Clear();
            var dsReturn = MngPaymentBlo.ListPaymentAptDetails(seqDt, pSeq);
            lvPaymentDetails.DataSource = dsReturn;
            lvPaymentDetails.DataBind();
        }

        private void LoadReceivable()
        {
            AuthCheckLib.CheckSession();
            // KN_SCR_SELECT_RECEIVABLE_BYROOMNO
            var dsReturn = MngPaymentBlo.ListReceivableAptInfo(hfRentCd.Value, hfFeeTy.Value, hfRoomNo.Value);
            lvReceivable.DataSource = dsReturn;
            lvReceivable.DataBind();            
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
                CloseLoading();
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
                CloseLoading();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPaymentList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
            {
                var txthfSeq = (TextBox)iTem.FindControl("txthfSeq");
                txthfSeq.Text = TextLib.StringDecoder(drView["Seq"].ToString());
                var txthfRentCd = (TextBox)iTem.FindControl("txthfRentCd");
                txthfRentCd.Text = TextLib.StringDecoder(drView["RentCd"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["P_InvoiceNo"].ToString()))
            {
                var txthfPInvoice = (TextBox)iTem.FindControl("txthfPInvoice");
                txthfPInvoice.Text = TextLib.StringDecoder(drView["P_InvoiceNo"].ToString());
            }


            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoom = (Literal)iTem.FindControl("ltRoom");
                ltRoom.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
            {
                var ltInsName = (Literal)iTem.FindControl("ltInsName");
                ltInsName.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                var ltFeeTy = (Literal)iTem.FindControl("ltFeeTy");
                ltFeeTy.Text = TextLib.StringDecoder(drView["FeeNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["StartDt"].ToString()))
            {
                var ltPayDay = (Literal)iTem.FindControl("ltPayDay");
                ltPayDay.Text = TextLib.MakeDateSixDigit(drView["StartDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["ViAmount"].ToString()))
            {
                var ltInsTotalPay = (Literal)iTem.FindControl("ltInsTotalPay");
                ltInsTotalPay.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["ViAmount"].ToString())).ToString("###,##0"));
            }
            var ltInsPaidAmt = (Literal)iTem.FindControl("ltInsPaidAmt");
            ltInsPaidAmt.Text = !string.IsNullOrEmpty(drView["PayAmount"].ToString()) ? TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["PayAmount"].ToString())).ToString("###,##0")) : "0";
            var chkUsedYn = (CheckBox)iTem.FindControl("chkReceitCd");

            if (!string.IsNullOrEmpty(drView["Paid"].ToString()))
            {
                (chkUsedYn).Checked = drView["Paid"].ToString() == "Y";
            }
            chkUsedYn.Enabled = false;
        }

        protected void lvPaymentDetails_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
            {
                var txthfSeq = (TextBox)iTem.FindControl("txthfSeq");
                txthfSeq.Text = TextLib.StringDecoder(drView["Seq"].ToString());
                var txtPSeq = (TextBox)iTem.FindControl("txtPSeq");
                txtPSeq.Text = TextLib.StringDecoder(drView["P_Seq"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["TotalAmount"].ToString()))
            {
                var ltPayAmount = (Literal)iTem.FindControl("ltPayAmount");
                ltPayAmount.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["TotalAmount"].ToString())).ToString("###,##0"));
            }

            if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
            {
                var ltPayDay = (Literal)iTem.FindControl("ltPayDay");
                ltPayDay.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["ReturnAmt"].ToString()))
            {
                var ltRevertAmt = (Literal)iTem.FindControl("ltRevertAmt");
                ltRevertAmt.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["ReturnAmt"].ToString())).ToString("###,##0"));
            }

            if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
            {
                var ltPaidTy = (Literal)iTem.FindControl("ltPaidTy");
                ltPaidTy.Text = TextLib.StringDecoder(drView["PaymentNm"].ToString());
            }
            var imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
            imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            if (!string.IsNullOrEmpty(drView["P_INVOICE"].ToString()) || !string.IsNullOrEmpty(drView["SlipNo"].ToString()))
            {
                imgbtnDelete.Visible = false;
            }

            var imgbtnPrint = (ImageButton)iTem.FindControl("imgbtnPrint");
            imgbtnPrint.OnClientClick = "javascript:return fnConfirm('Do you want to revert this payment ?')";
            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()) && double.Parse(drView["ReturnBalance"].ToString()) > 0)
            {
                imgbtnPrint.Visible = true;
                imgbtnPrint.Visible = Master.isModDelAuthOk;
            }
            else
            {
                imgbtnPrint.Visible = false;
            }
        }

        protected void lvPaymentList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void lvPaymentDetails_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var seq = (TextBox)lvPaymentDetails.Items[e.ItemIndex].FindControl("txthfSeq");
            var pSeq = (TextBox)lvPaymentDetails.Items[e.ItemIndex].FindControl("txtPSeq");
            var sbPrintOut = new StringBuilder();
            sbPrintOut.Append("window.open('/Common/Popup/PopupRevertPaymentAPT.aspx?PSeq=" + pSeq.Text + "&Seq=" + seq.Text +"&Datum2=&Datum3=&Datum4=', 'RevertMoney', 'status=no, resizable=no, width=510, height=300, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Revert", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
        }

        protected void lvPaymentList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
        }

        protected void lvPaymentDetails_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var seq = (TextBox)lvPaymentDetails.Items[e.ItemIndex].FindControl("txtHfSeq");
                var pSeq = (TextBox)lvPaymentDetails.Items[e.ItemIndex].FindControl("txtPSeq");
                //KN_USP_MNG_DELETE_PAYMENTINFO_M01
                MngPaymentBlo.DeletePaymentAptDetails(Int32.Parse(seq.Text),pSeq.Text);
                var sbList = new StringBuilder();
                sbList.Append("alert('" + AlertNm["INFO_DELETE_ISSUE"] + "');ReLoadData();");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingDelete", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                LoadData();
                upSearch.Update();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }


        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var payCd = ddlPaymentTy.SelectedValue;
                var bankSeq = Int32.Parse(ddlTransfer.SelectedValue == "" ? "0" : ddlTransfer.SelectedValue);
                var userSeq = hfUserSeq.Value;
                var roomNo = hfRoomNo.Value;
                var rentCd = hfRentCd.Value;
                var billCd = hfFeeTy.Value;
                var billCdDetail = hfBillCdDt.Value;
                var pSeq = hfSeq.Value;
                var seq = Int32.Parse(hfSeqDt.Value);
                var payDate = txtPayDay.Text.Replace("-", "");
                var refSeq = hfRef_Seq.Value;
                var exRate = txtExRate.Text == "" ? "0.00" : txtExRate.Text;
                var moneyCd = rbMoneyCd.SelectedValue;
                var amount = double.Parse(txtRealAmount.Text == "" ? "0.00" : txtRealAmount.Text.Replace(",",""));
                var memNo = Session["MemNo"].ToString();
                var ip = Session["UserIP"].ToString();
                var period = hfPeriod.Value;               
                var billTy = Int32.Parse(payDate.Substring(0,6)) >= Int32.Parse(period) ? "NM" : "PP";

                var objReturn = MngPaymentBlo.InsertPaymentInfoApt(payCd, bankSeq, userSeq, roomNo, rentCd, billCd, billCdDetail, pSeq, seq, payDate, exRate, moneyCd, amount, memNo, ip, refSeq, billTy);
                if (objReturn!=null && !string.IsNullOrEmpty(objReturn.Rows[0]["Seq"].ToString()))
                {
                    seq = Int32.Parse(objReturn.Rows[0]["Seq"].ToString());
                    var sbAlert = new StringBuilder();
                    sbAlert.Append("Payed Money (VND) : " + TextLib.MakeVietIntNo((amount).ToString("###,##0")) + "\\n");
                    var sbResult = new StringBuilder();
                    sbResult.Append("alert('" + sbAlert + "');");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ManagememntFee", sbResult.ToString(), CommValue.AUTH_VALUE_TRUE);
                    LoadData();
                    ResetSearchControls();
                    upSearch.Update();
                    upListPayMent.Update();
                    upReceiveable.Update();
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


        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {

            var dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (var dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING))
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP))
                    {
                        ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
            }
        }

        protected void ResetSearchControls()
        {
            ddlTransfer.Enabled = false;
            txtAmount.Text = "";
            txtPayDay.Text = "";
            txtRealAmount.Text = "";
            txtReceiveAmount.Text = "";
            txtExRate.Text = "";
        }

        protected void ResetInputControls()
        {
            ddlTransfer.Enabled = false;
            txtAmount.Text = "";
            txtPayDay.Text = "";
            txtRealAmount.Text = "";
            txtReceiveAmount.Text = "";
            txtExRate.Text = "";
            LoadUDetailPayment(0, "");
            lvReceivable.DataSource = null;
            lvReceivable.DataBind();
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void MakeFeeTypeDdl()
        {
            ddlFeeType.Items.Clear();
            ddlFeeType.Items.Add(new ListItem("Fee Type", ""));
            ddlFeeType.Items.Add(new ListItem("Management Fee", "0001"));
            ddlFeeType.Items.Add(new ListItem("Rental Fee", "0002"));
            ddlFeeType.Items.Add(new ListItem("Utility", "0011"));
            ddlFeeType.Items.Add(new ListItem("Parking Fee", "0004"));
            ddlFeeType.Items.Add(new ListItem("ParkingCard Fee", "0007"));
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void ddlPaymentTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPaymentTy.SelectedValue == CommValue.PAYMENT_TYPE_VALUE_TRANSFER)
                {
                    ddlTransfer.Enabled = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ddlTransfer.SelectedValue = string.Empty;
                    ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        public void MakeAccountDdl(DropDownList ddlParams)
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
            // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
            // Utility Fee : Chestnut 매출
            // 그외 KeangNam 매출
            string strCompCd = string.Empty;
            string strRentCd = hfRentCd.Value;
            string strFeeTy = string.Empty;
            strCompCd = CommValue.MAIN_COMP_CD;
            var dtReturn = AccountMngBlo.SpreadBankAccountInfo(strCompCd);
            ddlParams.Items.Clear();

            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

            foreach (var dr in dtReturn.Select())
            {
                ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
            }
        }

        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            var pSeq = hfSeq.Value;
            var seqdt = Int32.Parse(hfSeqDt.Value);
            LoadUDetailPayment(seqdt, pSeq);
            LoadReceivable();
        }

        protected void imgbtnDetailPayment_Click(object sender, ImageClickEventArgs e)
        {           
        }

        protected void lvReceivable_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;

            if (!string.IsNullOrEmpty(drView["RECEIVABLE_AMOUNT"].ToString()))
            {
                var ltAmount = (Literal)iTem.FindControl("ltAmount");
                ltAmount.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["RECEIVABLE_AMOUNT"].ToString())).ToString("###,##0"));
            }

            if (!string.IsNullOrEmpty(drView["PayDay"].ToString()))
            {
                var ltPayday = (Literal)iTem.FindControl("ltPayday");
                ltPayday.Text = TextLib.MakeDateEightDigit(drView["PayDay"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                var ltFeeNm = (Literal)iTem.FindControl("ltFeeNm");
                ltFeeNm.Text = TextLib.StringDecoder(drView["FeeNm"].ToString());
            }

            if (string.IsNullOrEmpty(drView["PERIOD"].ToString())) return;
            var ltPeriod = (Literal)iTem.FindControl("ltPeriod");
            ltPeriod.Text = TextLib.StringDecoder(drView["PERIOD"].ToString());
        }

        protected void lvReceivable_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void ddlPaidCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            LoadData();
        }

        protected void lnkAdjustment_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var payCd = ddlPaymentTy.SelectedValue;
                var bankSeq = Int32.Parse(ddlTransfer.SelectedValue == "" ? "0" : ddlTransfer.SelectedValue);
                var userSeq = hfUserSeq.Value;
                var roomNo = hfRoomNo.Value;
                var rentCd = hfRentCd.Value;
                var billCd = hfFeeTy.Value;
                var billCdDetail = hfBillCdDt.Value;
                var pSeq = hfSeq.Value;
                var seq = Int32.Parse(hfSeqDt.Value);
                var payDate = txtPayDay.Text.Replace("-", "");
                var refSeq = hfRef_Seq.Value;
                var exRate = txtExRate.Text == "" ? "0.00" : txtExRate.Text;
                var moneyCd = rbMoneyCd.SelectedValue;
                var amount = -double.Parse(txtRealAmount.Text == "" ? "0.00" : txtRealAmount.Text.Replace(",", ""));
                var memNo = Session["MemNo"].ToString();
                var ip = Session["UserIP"].ToString();
                var pInvoice = hfPInvoice.Value;
                var objReturn = MngPaymentBlo.InsertPaymentInfoApt(payCd, bankSeq, userSeq, roomNo, rentCd, billCd, billCdDetail, pSeq, seq, payDate, exRate, moneyCd, amount, memNo, ip, refSeq,"AD");
                if (objReturn != null && !string.IsNullOrEmpty(objReturn.Rows[0]["Seq"].ToString()))
                {
                    seq = Int32.Parse(objReturn.Rows[0]["Seq"].ToString());
                    var sbAlert = new StringBuilder();
                    sbAlert.Append("Payed Money (VND) : " + TextLib.MakeVietIntNo((amount).ToString("###,##0")) + "\\n");
                    var sbResult = new StringBuilder();
                    sbResult.Append("alert('" + sbAlert + "');");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ManagememntFee", sbResult.ToString(), CommValue.AUTH_VALUE_TRUE);
                    LoadData();
                    ResetSearchControls();
                    upSearch.Update();
                    upListPayMent.Update();
                    upReceiveable.Update();
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

        protected void lnkExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var strYear ="";// DateTime.Now.ToString("yyyy");
                var strMonth = "";//DateTime.Now.ToString("MM");
                var currentDt = txtSearchDt.Text.Replace("-", "");
                var eCurrentDt = txtESearchDt.Text.Replace("-", "");
                var dtReturn = MngPaymentBlo.ListPaymentInfoApt(hfRentCd.Value, ddlFeeType.SelectedValue, currentDt, txtRoomNo.Text, txtCompanyNm.Text, eCurrentDt, ddlPaidCd.SelectedValue);
                if (dtReturn == null || dtReturn.Tables.Count < 0) return;

                var strRentNm = "";

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW).Replace("+", " ") + "(" + strYear + "-" + strMonth + ").xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                this.EnableViewState = false;

                var stringWriter = new StringWriter();
                var htmlTextWriter = new HtmlTextWriter(stringWriter);


                string strTitle = "<p align=center><font size=4 face=Gulim><b>APT - Cash Report - " + strYear + "-" + strMonth + "</b></font></p>";
                htmlTextWriter.Write(strTitle);

                var gv = new GridView();
                gv.Font.Name = "Tahoma";
                gv.DataSource = dtReturn;
                gv.DataBind();
                gv.RenderControl(htmlTextWriter);

                Response.Write(stringWriter.ToString());
                Response.End();

                stringWriter.Flush();
                stringWriter.Close();
                htmlTextWriter.Flush();
                htmlTextWriter.Close();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }


        protected void imgUpdateInvoice_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfInvoice.Value;
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();
            //KN_USP_INSERT_INVOICE_A0000_MERGE_I00
            InvoiceMngBlo.InsertMergeInvoiceA000HoadonInfoApt(reft, insCompCd, insMemNo, insMemIP);
            LoadData();
        }

        protected void CloseLoading()
        {
            var sbResult = new StringBuilder();
            sbResult.Append("CloseLoading();");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ManagememntFee", sbResult.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
        protected void LoadLoading()
        {
            var sbResult = new StringBuilder();
            sbResult.Append("ShowLoading('Loading data ....!');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ManagememntFee", sbResult.ToString(), CommValue.AUTH_VALUE_TRUE);
        }

        protected void lnkAdjBalance_Click(object sender, EventArgs e)
        {
            
            var seq = (TextBox)lvPaymentDetails.Items[0].FindControl("txthfSeq");
            var pSeq = (TextBox)lvPaymentDetails.Items[0].FindControl("txtPSeq");
            var sbPrintOut = new StringBuilder();
            sbPrintOut.Append("window.open('/Common/Popup/PopupRevertBalancePaymentAPT.aspx?PSeq=" + pSeq.Text + "&Seq=" + seq.Text + "&Datum2=&Datum3=&Datum4=', 'RevertMoney', 'status=no, resizable=no, width=510, height=300, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Revert", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
    }
}
