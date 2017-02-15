using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Drawing;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Config.Biz;
using KN.Manage.Biz;
using KN.Parking.Biz;

using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace KN.Web.Management.Manage
{
    public partial class MngPaymentWrite : BasePage
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
            hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
        }
        protected void InitControls()
        {

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgLoadData.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtCompanyNm.ClientID + "', '" + txtRoomNo.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtCompanyNm.Text + "', '" + ddlRentCd.SelectedValue + "');";

            // 섹션코드 조회
            LoadRentDdl(ddlRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            // 차종 조회
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlPaymentTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);

            txtAmount.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtExRate.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            MakeFeeTypeDdl();
            MakeAccountDdl(ddlTransfer);
            CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaidCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_PAYMENT);           
        }


        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_MNG_SELECT_PAYMENTINFO_S05
            var currentDt = txtSearchDt.Text.Replace("-", "");
            var eCurrentDt = txtESearchDt.Text.Replace("-", "");
            var dsReturn = MngPaymentBlo.ListPaymentInfo(ddlRentCd.SelectedValue, ddlFeeType.SelectedValue, currentDt, txtRoomNo.Text, txtCompanyNm.Text,eCurrentDt,ddlPaidCd.SelectedValue);
            if (dsReturn == null || dsReturn.Tables[0].Rows.Count <=0)
            {
                ltTotalAmtAll.Text = "0";
                ltUAll.Text = "0";
                ltMFAll.Text = "0";
                ltRFAll.Text = "0";
                ltBalanceAll.Text = "0";
                ltPaidAll.Text = "0";
                ResetInputControls();
                lvPaymentList.DataSource = null;
                lvPaymentList.DataBind();
                return;
            }
            lvPaymentList.DataSource = dsReturn.Tables[0];
            lvPaymentList.DataBind();
            if (dsReturn.Tables[1].Rows.Count > CommValue.NUMBER_VALUE_0)
            {
                ltMFAll.Text = double.Parse(dsReturn.Tables[1].Rows[0]["TotalMF"].ToString()).ToString("###,##0");
                ltUAll.Text = double.Parse(dsReturn.Tables[1].Rows[0]["TotalUtility"].ToString()).ToString("###,##0");
                ltRFAll.Text = double.Parse(dsReturn.Tables[1].Rows[0]["TotalRF"].ToString()).ToString("###,##0");
                ltTotalAmtAll.Text = double.Parse(dsReturn.Tables[1].Rows[0]["TotalAll"].ToString()).ToString("###,##0");
                ltBalanceAll.Text = (double.Parse(dsReturn.Tables[1].Rows[0]["TotalAll"].ToString()) - double.Parse(dsReturn.Tables[1].Rows[0]["TotalPaid"].ToString())).ToString("###,##0");
                ltPaidAll.Text = double.Parse(dsReturn.Tables[1].Rows[0]["TotalPaid"].ToString()).ToString("###,##0");
                ltParking.Text = double.Parse(dsReturn.Tables[1].Rows[0]["TotalParking"].ToString()).ToString("###,##0");
            }
            else
            {
                ltTotalAmtAll.Text = "0";
                ltUAll.Text = "0";
                ltMFAll.Text = "0";
                ltRFAll.Text = "0";
                ltBalanceAll.Text = "0";
                ltPaidAll.Text = "0";
            }
            ResetInputControls();
        }

        private void LoadUDetailPayment(int seqDt,string pSeq)
        {
            AuthCheckLib.CheckSession();
            // KN_USP_MNG_SELECT_PAYMENTINFO_S06
            lvPaymentDetails.Items.Clear();
            var dsReturn = MngPaymentBlo.ListPaymentDetails(seqDt, pSeq);

            lvPaymentDetails.DataSource = dsReturn;
            lvPaymentDetails.DataBind();
        }

        private void LoadReceivable()
        {
            AuthCheckLib.CheckSession();

            // KN_SCR_SELECT_RECEIVABLE_BYROOMNO
            var dsReturn = MngPaymentBlo.ListReceivableInfo(hfRentCd.Value,hfFeeTy.Value,hfRoomNo.Value);

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
            if (!string.IsNullOrEmpty(drView["REF_SERIAL_NO"].ToString()))
            {
                var txthfSeq = (TextBox)iTem.FindControl("txthfSeq");
                txthfSeq.Text = TextLib.StringDecoder(drView["REF_SERIAL_NO"].ToString());
                var txthfRentCd = (TextBox)iTem.FindControl("txthfRentCd");
                txthfRentCd.Text = TextLib.StringDecoder(drView["RentCd"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoom = (Literal)iTem.FindControl("ltRoom");
                ltRoom.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
            {
                var ltInsName = (Literal)iTem.FindControl("ltInsName");
                //ltInsName.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
                ltInsName.Text = TextLib.TextCutString(TextLib.StringDecoder(drView["TenantNm"].ToString()), 40, "..");
            }

            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                var ltFeeTy = (Literal)iTem.FindControl("ltFeeTy");
                ltFeeTy.Text = TextLib.StringDecoder(drView["FeeNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["IssueDt"].ToString()))
            {
                var ltPayDay = (Literal)iTem.FindControl("ltPayDay");
                ltPayDay.Text = TextLib.MakeDateEightDigit(drView["IssueDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["TotSellingPrice"].ToString()))
            {
                var ltInsTotalPay = (Literal)iTem.FindControl("ltInsTotalPay");
                ltInsTotalPay.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["TotSellingPrice"].ToString())).ToString("###,##0"));
            }
            var ltInsPaidAmt = (Literal)iTem.FindControl("ltInsPaidAmt");
            ltInsPaidAmt.Text = !string.IsNullOrEmpty(drView["PayAmount"].ToString()) ? TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["PayAmount"].ToString())).ToString("###,##0")) : "0";
            var chkUsedYn = (CheckBox)iTem.FindControl("chkReceitCd");

            if (!string.IsNullOrEmpty(drView["Paid"].ToString()))
            {
                (chkUsedYn).Checked = drView["Paid"].ToString() == "Y";
            }
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

            if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
            {
                var ltPaidTy = (Literal)iTem.FindControl("ltPaidTy");
                ltPaidTy.Text = TextLib.StringDecoder(drView["PaymentNm"].ToString());
            }
            var imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
            imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
        }

        protected void lvPaymentList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void lvPaymentDetails_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var seq = (TextBox)lvPaymentDetails.Items[e.ItemIndex].FindControl("txthfSeq");
            var pSeq = (TextBox)lvPaymentDetails.Items[e.ItemIndex].FindControl("txtPSeq");

            var sbPrintOut = new StringBuilder();

            sbPrintOut.Append("window.open('/Common/RdPopup/RDPopupReciptDetails.aspx?Datum0=" + seq.Text + "&Datum1=" + pSeq.Text + "&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Reprint", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
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

                MngPaymentBlo.DeletePaymentDetails(Int32.Parse(seq.Text),pSeq.Text);
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
                var objReturn = MngPaymentBlo.InsertPaymentInfo(payCd,bankSeq,userSeq,roomNo,rentCd,billCd,billCdDetail,pSeq,seq,payDate,exRate,moneyCd,amount,memNo,ip,refSeq,"NM");
                if (objReturn!=null && !string.IsNullOrEmpty(objReturn.Rows[0]["Seq"].ToString()))
                {
                    seq = Int32.Parse(objReturn.Rows[0]["Seq"].ToString());
                    var sbAlert = new StringBuilder();
                    sbAlert.Append("Payed Money (VND) : " + TextLib.MakeVietIntNo((amount).ToString("###,##0")) + "\\n");
                    var sbResult = new StringBuilder();
                    sbResult.Append("alert('" + sbAlert + "');");
                    sbResult.Append("window.open('/Common/RdPopup/RDPopupReciptDetails.aspx?Datum0=" + seq + "&Datum1=" + pSeq + "&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");
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
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);
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
            //ddlPaymentTy.SelectedValue = CommValue.CODE_VALUE_EMPTY;
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
            string strRentCd = string.Empty;
            string strFeeTy = string.Empty;

            if ((strRentCd.Equals(CommValue.RENTAL_VALUE_APT) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTA) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTB)) &&
                strFeeTy.Equals(CommValue.FEETY_VALUE_MNGFEE))
            {
                strCompCd = CommValue.MAIN_COMP_CD;
            }
            else
            {
                strCompCd = CommValue.SUB_COMP_CD;
            }

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
                var objReturn = MngPaymentBlo.InsertPaymentInfo(payCd, bankSeq, userSeq, roomNo, rentCd, billCd, billCdDetail, pSeq, seq, payDate, exRate, moneyCd, amount, memNo, ip, refSeq,"AD");
                if (objReturn != null && !string.IsNullOrEmpty(objReturn.Rows[0]["Seq"].ToString()))
                {
                    seq = Int32.Parse(objReturn.Rows[0]["Seq"].ToString());
                    var sbAlert = new StringBuilder();
                    sbAlert.Append("Payed Money (VND) : " + TextLib.MakeVietIntNo((amount).ToString("###,##0")) + "\\n");
                    var sbResult = new StringBuilder();
                    sbResult.Append("alert('" + sbAlert + "');");
                    sbResult.Append("window.open('/Common/RdPopup/RDPopupReciptDetails.aspx?Datum0=" + seq + "&Datum1=" + pSeq + "&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");
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

        protected void imgLoadData_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                CloseLoading();
                ErrLogger.MakeLogger(ex);
            }
           
        }

        protected void lnkExport_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();


            try
            {

                var currentDt = txtSearchDt.Text.Replace("-", "");
                var eCurrentDt = txtESearchDt.Text.Replace("-", "");
                var dtReturn = MngPaymentBlo.ListPaymentInfo(ddlRentCd.SelectedValue, ddlFeeType.SelectedValue, currentDt, txtRoomNo.Text, txtCompanyNm.Text, eCurrentDt, ddlPaidCd.SelectedValue).Tables[0];
                if (dtReturn.Rows.Count <= 0)
                {
                    return;
                }
                
                var fileName = Server.UrlEncode("Tenant Fee").Replace("+", " ");
                GenerateExcel(dtReturn, fileName, fileName);
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
                //create new table
                DataTable dtbs = new DataTable();
                dtbs.Columns.Add("Room");
                dtbs.Columns.Add("TenantName");
                dtbs.Columns.Add("Fee");
                dtbs.Columns.Add("IssueDate");
                dtbs.Columns.Add("Charge");
                dtbs.Columns.Add("Balance");
                dtbs.Columns.Add("IsPaid");
                foreach (DataRow dr in tbl.Rows)
                {
                    DataRow drs = dtbs.NewRow();
                    drs["Room"] = dr["RoomNo"];
                    drs["TenantName"] = dr["TenantNm"];
                    drs["Fee"] = dr["BillENm"];
                    drs["IssueDate"] = dr["IssueDt"];
                    drs["Charge"] = dr["ReAmount"];
                    drs["Balance"] = dr["PayAmount"];
                    drs["IsPaid"] = dr["Paid"];

                    dtbs.Rows.Add(drs);
                }

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(dtbs, true, TableStyles.None);
                var tbls = ws.Tables[0];

                tbls.ShowTotal = true;
                //Set Sum 



                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:M1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:M1"].AutoFitColumns();
                //using (var col = ws.Cells[2, 10, 2 + tbl.Rows.Count, 12])
                //{
                //    col.Style.Numberformat.Format = "#,##0";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //using (var col = ws.Cells[2, 5, 2 + tbl.Rows.Count, 5])
                //{
                //    col.Style.Numberformat.Format = "dd/MM/yyyy";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                //Response.End();
            }
        }
    }
}
