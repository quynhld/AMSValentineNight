using System;
using System.Data;
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
using KN.Resident.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class MakingSpecialDepit : BasePage
    {
        DataTable dtReturn1;
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
            //if (Request.Params[Master.PARAM_DATA1] == null) return;
            if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1])) return;
            //hfUserSeq.Value = Request.Params[Master.PARAM_DATA1];
            hfRentCd.Value = Request.Params[Master.PARAM_DATA1];

            txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1];
            
        }
        protected void InitControls()
        {
            
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            txtInputAmt.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtInputEx.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtInputRealAmt.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtESearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputESearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputPayDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputIssDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtReqDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            hfReqDt.Value = txtReqDt.Text.Replace("-", "").Replace(".", "");

            var strPaymentDt = txtInputPayDt.Text.Replace("-", "");
            dtReturn1 = InvoiceMngBlo.SelectExRateByPayDt(txtHfRentCd.Text, strPaymentDt);
            txtInputEx.Text = double.Parse(dtReturn1.Rows[0][0].ToString()).ToString("###,##0.00");
            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtTitle.ClientID + "', '" + txtInputRoom.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtTitle.Text + "', '" + txtHfRentCd.Text + "');";

            //ddlCmpName.Enabled = false;
            txtInputRoom.Enabled = false;

            lnkbtnRegist.Visible = false;
            lnkbtnBack.Visible = false;
            lnkbtnCreate.Visible = true;

            LoadRoomNo();

            MakeItemDdl();

            MakeInputItemDdl();

            MakePrintYNDdl();

            var roomNo = txthfRoomNo.Text;
            var chargeSeq = Int32.Parse(txthfChargeSeq.Text);
            //LoadUserInfoEdit(roomNo, chargeSeq);
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_SELECT_SPECIAL_DEBIT_I00
            var roomNo = txtRoomNo.Text;
            var compNm = txtCompanyNm.Text;
            var feeTy = ddlItems.SelectedValue;
            var rentCd = txtHfRentCd.Text;
            var strStartDt = txtSearchDt.Text.Replace("-", "");
            var strEndDt = txtESearchDt.Text.Replace("-", "");
            var printedYN = ddlPrintYN.SelectedValue;
            //KN_USP_AGT_MAKE_RENTFEE_CREATED_DEBIT_LIST_M00
            var dtReturn = MngPaymentBlo.SelectSpecialDebitList(roomNo, compNm, feeTy, rentCd, strStartDt, strEndDt, printedYN, "");
            hfRefSeq.Value = "";
            if (dtReturn == null) return;
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
            txtTitle.Enabled = false;
            lnkbtnUpdate.Visible = false;
            lnkbtnCancel.Visible = false;
            lnkbtnPrint.Visible = false;
            var sbWarning = new StringBuilder();
            sbWarning.Append("createDebit(false)");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Register", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
        }

        //---------------------------New Function ---------------------------------
        //Make Dropdownlist For ddlItems
        protected void MakeItemDdl()
        {
            var dtReturn = CommCdInfo.SelectSpecialFee(Session["LangCd"].ToString());

            ddlItems.Items.Clear();

            ddlItems.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (var dr in dtReturn.Select())
            {
                ddlItems.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        //Make Dropdownlist For ddlInputItem
        protected void MakeInputItemDdl()
        {
            var dtReturn = CommCdInfo.SelectSpecialFee(Session["LangCd"].ToString());

            ddlInputItem.Items.Clear();

            ddlInputItem.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (var dr in dtReturn.Select())
            {
                ddlInputItem.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        //Make Dropdownlist For ddlCmpName
        protected void LoadRoomNo()
        {
        }

        protected void ddlCmpName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //txtInputRoom.Text = ddlCmpName.SelectedValue;
            //lvPrintoutList.DataSource = null;
            //lvPrintoutList.DataBind();
        }

        private void MakePrintYNDdl()
        {
            ddlPrintYN.Items.Clear();
            ddlPrintYN.Items.Add(new ListItem("No", "N"));
            ddlPrintYN.Items.Add(new ListItem("Yes", "Y"));
            

        }

        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
            //string conmpareVal1 = lvPrintoutList.Items[e].FindControl("ltInsRoomNo").ToString();

            var cb = (CheckBox)sender;
            var item = (ListViewItem)cb.NamingContainer;
            var dataItem = (ListViewDataItem)item;

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

        protected void LoadDetails(string refSeq)
        {

            //ddlCmpName.SelectedIndex = 0;
            txtDescVi.Text = "";
            txtDescEng.Text = "";
            txtInputVat.Text = "";
            txtInputAmt.Text = "";
            txtInputRealAmt.Text = "";
            ddlItems.SelectedIndex = 0;
            txtInputRoom.Text = "";
            txtInputSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputESearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputPayDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputIssDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            hfRefSeq.Value = refSeq;
            txtInputQty.Text = "";
            txtInputUnitPrice.Text = "";

            txtTitle.Enabled = false;
            ddlItems.Enabled = false;
            imgbtnSearchCompNm.Enabled = false;
            txtInputRoom.Enabled = true;
            var printedYN = ddlPrintYN.SelectedValue;

            var dtDetail = MngPaymentBlo.SelectSpecialDebitListDetail(refSeq, printedYN);
            txtTitle.Text = dtDetail.Rows[0]["UserNm"].ToString();
            //ddlCmpName.SelectedValue = dtDetail.Rows[0]["RoomNo"].ToString();
            txtDescVi.Text = dtDetail.Rows[0]["Desciption"].ToString();
            txtDescEng.Text = dtDetail.Rows[0]["DesciptionEng"].ToString();
            txtInputSearchDt.Text = dtDetail.Rows[0]["StartDt"].ToString().Substring(0, 4) + "-" + dtDetail.Rows[0]["StartDt"].ToString().Substring(4, 2) + "-" + dtDetail.Rows[0]["StartDt"].ToString().Substring(6, 2);
            txtInputESearchDt.Text = DateTime.ParseExact(dtDetail.Rows[0]["EndDt"].ToString(),"yyyyMMdd",null).ToString("yyyy-MM-dd");
            txtInputPayDt.Text = DateTime.ParseExact(dtDetail.Rows[0]["PaymentDt"].ToString(),"yyyyMMdd", null).ToString("yyyy-MM-dd");
            txtInputIssDt.Text = DateTime.ParseExact(dtDetail.Rows[0]["IssuingDt"].ToString(),"yyyyMMdd", null).ToString("yyyy-MM-dd");

            txtInputEx.Text = double.Parse(dtDetail.Rows[0]["DongToDollar"].ToString()).ToString("###,##0.00");         
            ddlInputItem.SelectedValue = dtDetail.Rows[0]["FeeTy"].ToString();
            txtInputRoom.Text = dtDetail.Rows[0]["RoomNo"].ToString();
            txtInputAmt.Text = double.Parse(dtDetail.Rows[0]["RealMonthViAmtNo"].ToString()).ToString("###,##0.00");
            txtInputRealAmt.Text = double.Parse(dtDetail.Rows[0]["MonthViAmtNo"].ToString()).ToString("###,##0.00");
            txtInputVat.Text = double.Parse(dtDetail.Rows[0]["VATAmt"].ToString()).ToString("###,##0.00");
            rbMoneyCd.SelectedIndex = txtInputEx.Text == "1.00" ? 1 : 0;
            rbIsDebit.SelectedValue = dtDetail.Rows[0]["BillTy"].ToString();
            txtInputQty.Text = double.Parse(dtDetail.Rows[0]["Qty"].ToString()).ToString("###,##0.00");
            txtInputUnitPrice.Text = double.Parse(dtDetail.Rows[0]["UnitPrice"].ToString()).ToString("###,##0.00");
        }
      

        //-------------------------------------------------------------------------

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


        protected void lnkbtnCreate_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();

            txtTitle.Text = "";
            txtDescVi.Text = "";
            txtDescEng.Text = "";
            txtInputVat.Text = "";
            txtInputAmt.Text = "";
            txtInputRealAmt.Text = "";
            ddlItems.SelectedIndex = 0;
            txtInputRoom.Text = "";
            txtInputSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputESearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputPayDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputIssDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputQty.Text = "";
            txtInputUnitPrice.Text = "";
            

            txtTitle.Enabled = true;
            imgbtnSearchCompNm.Enabled = true;
            txtInputRoom.Enabled = true;            
            lnkbtnPrint.Visible = false;
            lnkbtnUpdate.Visible = false;
            lnkbtnRegist.Visible = true;
            lnkbtnCreate.Visible = false;
            lnkbtnCancel.Visible = false;
            lnkbtnBack.Visible = true;
            txtInputRoom.Enabled = false;
            HfReturnUserSeqId.Value = "";

            txtTitle.Focus();
            var sbWarning = new StringBuilder();
            sbWarning.Append("createDebit(true)");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Register", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
        }


        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {            

            try
            {
                 // 세션체크
                var strRentCd = txtHfRentCd.Text;
                const int terMonth = 1;
                var strFeeTy = ddlInputItem.SelectedValue;
                var strRoomNo = txtInputRoom.Text;
                var strTenantNm = HfReturnUserSeqId.Value;
                var strStartDt = txtInputSearchDt.Text.Replace("-", "");
                var strEndDt = txtInputESearchDt.Text.Replace("-", "");
                var strPaymentDt = txtInputPayDt.Text.Replace("-", "");
                var strIssDt = txtInputIssDt.Text.Replace("-", "");
                var strMonthViAmtNo = txtInputRealAmt.Text;
                var strRealMonthViAmtNo = txtInputAmt.Text;
                var strExRate = txtInputEx.Text;
                var strVatAmt = txtInputVat.Text;
                var strDesVi  = txtDescVi.Text;
                var strDesEng = txtDescEng.Text;
                var strQty = txtInputQty.Text;
                var strUnitPrice = txtInputUnitPrice.Text;

                var requestDt = txtReqDt.Text.Replace("-", "");
                
                var dbmonthViAmtNo = CommValue.NUMBER_VALUE_0_0;
                var dbrealMonthViAmtNo = CommValue.NUMBER_VALUE_0_0;
                var dbExRate = CommValue.NUMBER_VALUE_0_0;
                var dbVatAmt = CommValue.NUMBER_VALUE_0_0;
                var dbQty   = CommValue.NUMBER_VALUE_0_0;
                var dbUnitPrice = CommValue.NUMBER_VALUE_0_0;

                if (!string.IsNullOrEmpty(strMonthViAmtNo))
                {
                    dbmonthViAmtNo = double.Parse(strMonthViAmtNo);
                }
                if (!string.IsNullOrEmpty(strRealMonthViAmtNo))
                {
                    dbrealMonthViAmtNo = double.Parse(strRealMonthViAmtNo);
                }
                if (!string.IsNullOrEmpty(strExRate))
                {
                    dbExRate = double.Parse(strExRate);
                }
                 if (!string.IsNullOrEmpty(strVatAmt))
                {
                    dbVatAmt = double.Parse(strVatAmt);
                }

                 if (!string.IsNullOrEmpty(strQty))
                 {
                     dbQty = double.Parse(strQty);
                 }

                 if (!string.IsNullOrEmpty(strUnitPrice))
                 {
                     dbUnitPrice = double.Parse(strUnitPrice);
                 }

                 var dtReturn = MngPaymentBlo.MakeSpecialDebit(strRentCd, strFeeTy, strRoomNo, strTenantNm, strStartDt, strEndDt, strPaymentDt, strIssDt, dbmonthViAmtNo, dbrealMonthViAmtNo, dbExRate, dbVatAmt, strDesVi, strDesEng, rbIsDebit.SelectedValue, requestDt, dbQty, dbUnitPrice);
                //LoadData();

                var sbWarning = new StringBuilder();
                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
                sbWarning.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Register", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                LoadData();

                txtTitle.Text = "";
                txtDescVi.Text = "";
                txtDescEng.Text = "";
                txtInputVat.Text = "";
                txtInputAmt.Text = "";
                txtInputRealAmt.Text = "";
                ddlItems.SelectedIndex = 0;
                txtInputRoom.Text = "";
                txtInputSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
                txtInputESearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
                txtInputPayDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
                txtInputIssDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
                imgbtnSearchCompNm.Enabled = false;
                txtInputRoom.Enabled = false;
                HfReturnUserSeqId.Value = "";
                txtInputQty.Text = "";
                txtInputUnitPrice.Text = "";
               
                lnkbtnPrint.Visible = true;
                lnkbtnUpdate.Visible = true;
                lnkbtnRegist.Visible = false;
                lnkbtnCreate.Visible = true;
                lnkbtnBack.Visible = false;
                lnkbtnCancel.Visible = true;
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
                var sbNoSelection = new StringBuilder();
                if (!string.IsNullOrEmpty(hfRefSeq.Value))
                {
                    MngPaymentBlo.CancelSpecialDebit("", hfRefSeq.Value);
                    sbNoSelection.Append("alert('Deleted !');");  
                    LoadData();
                }
                else
                {
                    // 선택된 대상 없음                    
                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");                              
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);          
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

       

        protected void ddlPaymentTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        

        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            imgbtnSearchCompNm.Enabled = false;
            txtTitle.Enabled = false;
            txtInputRoom.Enabled = false;

            if (txtInputEx.Text == "1.00")
            {
                rbMoneyCd.SelectedIndex = 1;
            }
            else
                rbMoneyCd.SelectedIndex = 0;
            rbIsDebit.SelectedValue = hfbillTy.Value;
            lnkbtnBack.Visible = false;
            lnkbtnUpdate.Visible = true;
            lnkbtnCancel.Visible = true;
            lnkbtnPrint.Visible = true;

        }

        protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfsendParam.Value;
            try
            {
                MngPaymentBlo.InsertSpecialDebitToHoadonInfo(reft);
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtInputPayDt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AuthCheckLib.CheckSession();
                var strPaymentDt = txtInputPayDt.Text.Replace("-", "");
                var dsReturn = InvoiceMngBlo.SelectExRateByPayDt(txtHfRentCd.Text, strPaymentDt);                
                txtInputEx.Text = double.Parse(dtReturn1.Rows[0][0].ToString()).ToString("###,##0.00"); 
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
           
        }

        protected void lvPrintoutList_LayoutCreated(object sender, EventArgs e)
        {

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
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                var ltFeeType = (Literal)iTem.FindControl("ltFeeType");
                ltFeeType.Text = drView["FeeNm"].ToString();
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

            if (!string.IsNullOrEmpty(drView["StartDt"].ToString()))
            {
                var ltUsingPeriod = (Literal)iTem.FindControl("ltUsingPeriod");
                ltUsingPeriod.Text = TextLib.MakeDateEightSlash(drView["StartDt"].ToString()) + " ~ " + TextLib.MakeDateEightSlash(drView["EndDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["RealMonthViAmtNo"].ToString()))
            {
                var ltFee = (Literal)iTem.FindControl("ltFee");                    
                ltFee.Text = TextLib.MakeVietIntNo(double.Parse(drView["RealMonthViAmtNo"].ToString()).ToString("###,##0"));
            }

            if (!string.IsNullOrEmpty(drView["IssuingDt"].ToString()))
            {
                var ltIssDt = (Literal)iTem.FindControl("ltIssDt");
                ltIssDt.Text = TextLib.MakeDateEightSlash(drView["IssuingDt"].ToString());
            }
            //==========================Visible======================
            if (!string.IsNullOrEmpty(drView["StartDt"].ToString()))
            {
                var txthfStartUsingDt = (TextBox)iTem.FindControl("txthfStartUsingDt");
                txthfStartUsingDt.Text = TextLib.StringDecoder(drView["StartDt"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["EndDt"].ToString()))
            {
                var txthfEndUsingDt = (TextBox)iTem.FindControl("txthfEndUsingDt");
                txthfEndUsingDt.Text = TextLib.StringDecoder(drView["EndDt"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
            {
                var txtHfPaymetDt = (TextBox)iTem.FindControl("txtHfPaymetDt");
                txtHfPaymetDt.Text = TextLib.StringDecoder(drView["PaymentDt"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["IssuingDt"].ToString()))
            {
                var txthfIssDt = (TextBox)iTem.FindControl("txthfIssDt");
                txthfIssDt.Text = TextLib.StringDecoder(drView["IssuingDt"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
            {
                var txthfDongtoDollar = (TextBox)iTem.FindControl("txthfDongtoDollar");
                txthfDongtoDollar.Text = TextLib.StringDecoder(drView["DongToDollar"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["MonthViAmtNo"].ToString()))
            {
                var txthfMonthViAmtNo = (TextBox)iTem.FindControl("txthfMonthViAmtNo");
                txthfMonthViAmtNo.Text = TextLib.StringDecoder(drView["MonthViAmtNo"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["RealMonthViAmtNo"].ToString()))
            {
                var txthfRealMonthViAmtNo = (TextBox)iTem.FindControl("txthfRealMonthViAmtNo");
                txthfRealMonthViAmtNo.Text = TextLib.StringDecoder(drView["RealMonthViAmtNo"].ToString());
            }
                
            if (!string.IsNullOrEmpty(drView["REF_SEQ"].ToString()))
            {
                var txthfBundleSeqNo = (TextBox)iTem.FindControl("txthfBundleSeqNo");
                txthfBundleSeqNo.Text = TextLib.StringDecoder(drView["REF_SEQ"].ToString());
                hfRefSeq.Value = TextLib.StringDecoder(drView["REF_SEQ"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
            {
                var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                txtHfUserSeq.Text = TextLib.StringDecoder(drView["UserSeq"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["FeeTy"].ToString()))
            {
                var txthfFeeCd = (TextBox)iTem.FindControl("txthfFeeCd");
                txthfFeeCd.Text = TextLib.StringDecoder(drView["FeeTy"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["RequestDt"].ToString()))
            {
                var txthfReqDt = (TextBox)iTem.FindControl("txthfReqDt");
                txthfReqDt.Text = TextLib.StringDecoder(drView["RequestDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Qty"].ToString()))
            {
                var txthfQty = (TextBox)iTem.FindControl("txthfQty");
                txthfQty.Text = TextLib.StringDecoder(drView["Qty"].ToString());
            }
            if (!string.IsNullOrEmpty(drView["UnitPrice"].ToString()))
            {
                var txthfUnitPrice = (TextBox)iTem.FindControl("txthfUnitPrice");
                txthfUnitPrice.Text = TextLib.StringDecoder(drView["UnitPrice"].ToString());
            }
        }

        

        protected void lnkbtUpdate_Click(object sender, EventArgs e)
        {

        }

        protected void lnkbtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();


                string strRentCd = txtHfRentCd.Text;
                string strUserSeq = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfUserSeq")).Text;
                string strRoomNo = ((Literal)lvPrintoutList.Items[0].FindControl("ltInsRoomNo")).Text;
                string strREF_SEQ = hfRefSeq.Value;
                string strTenantNm = ((Literal)lvPrintoutList.Items[0].FindControl("ltTenantNm")).Text;

                string strFeeTy = ddlInputItem.SelectedValue;
                var strStartDt = txtInputSearchDt.Text.Replace("-", "");
                var strEndDt = txtInputESearchDt.Text.Replace("-", "");
                var strPaymentDt = txtInputPayDt.Text.Replace("-", "");
                var strIssDt = txtInputIssDt.Text.Replace("-", "");
                var strMonthViAmtNo = txtInputRealAmt.Text;
                var strRealMonthViAmtNo = txtInputAmt.Text;
                var strExRate = txtInputEx.Text;
                var strvatAmt = txtInputVat.Text;
                var strDescVi = txtDescVi.Text;
                var strDescEng = txtDescEng.Text;
                var requestDt = txtReqDt.Text.Replace("-", "");
                var strQty = txtInputQty.Text;
                var strUnitPrice = txtInputUnitPrice.Text;

                double dbmonthViAmtNo = CommValue.NUMBER_VALUE_0_0;
                double dbrealMonthViAmtNo = CommValue.NUMBER_VALUE_0_0;
                double dbExRate = CommValue.NUMBER_VALUE_0_0;
                double dbvatAmt = CommValue.NUMBER_VALUE_0_0;
                double dbQty = CommValue.NUMBER_VALUE_0_0;
                double dbUnitPrice = CommValue.NUMBER_VALUE_0_0;

                if (!string.IsNullOrEmpty(strMonthViAmtNo))
                {
                    dbmonthViAmtNo = double.Parse(strMonthViAmtNo);
                }
                if (!string.IsNullOrEmpty(strRealMonthViAmtNo))
                {
                    dbrealMonthViAmtNo = double.Parse(strRealMonthViAmtNo);
                }
                if (!string.IsNullOrEmpty(strExRate))
                {
                    dbExRate = double.Parse(strExRate);
                }
                if (!string.IsNullOrEmpty(strvatAmt))
                {
                    dbvatAmt = double.Parse(strvatAmt);
                }

                if (!string.IsNullOrEmpty(strQty))
                {
                    dbQty = double.Parse(strQty);
                }

                if (!string.IsNullOrEmpty(strUnitPrice))
                {
                    dbUnitPrice = double.Parse(strUnitPrice);
                }                               


                // 오피스 / 리테일 데이터 수정
                // KN_SCR_UPDATE_SPECIAL_DEBIT_U00
                MngPaymentBlo.UpdateSpecialDebit(strFeeTy, strPaymentDt, dbExRate, dbmonthViAmtNo, dbrealMonthViAmtNo,
                                                           strStartDt, strEndDt, strIssDt, strREF_SEQ, dbvatAmt, strDescVi, strDescEng, rbIsDebit.SelectedValue, requestDt, dbQty, dbUnitPrice);

                LoadData();
                LoadDetails(strREF_SEQ);

                imgbtnSearchCompNm.Enabled = false;
                txtInputRoom.Enabled = false;


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

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }
               
                // 선택 사항이 있는지 없는지 체크
                if (!string.IsNullOrEmpty(hfRefSeq.Value))
                {
                    MngPaymentBlo.UpdatingNullSpecialDebit(hfRentCd.Value);
                    MngPaymentBlo.UpdatingPrintBundleNoSpecialDebit(hfRefSeq.Value, hfRefSeq.Value);
                    var strFeeTy = ddlInputItem.SelectedValue;
                    hfPrintBundleNo.Value = hfRefSeq.Value;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + hfPrintBundleNo.Value + "','" + strFeeTy + "');", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    // 화면 초기화
                    // 선택된 대상 없음
                    var sbNoSelection = new StringBuilder();
                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPrintoutList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtReqDt_TextChanged(object sender, EventArgs e)
        {
            hfReqDt.Value = txtReqDt.Text.Replace("-", "").Replace(".", "");
        }

        protected void lnkbtnBack_Click(object sender, EventArgs e)
        {
            lnkbtnPrint.Visible = true;
            lnkbtnUpdate.Visible = true;
            lnkbtnRegist.Visible = false;
            lnkbtnCreate.Visible = true;
            lnkbtnCancel.Visible = true;
            lnkbtnBack.Visible = false;

            txtTitle.Text = "";
            txtDescVi.Text = "";
            txtDescEng.Text = "";
            txtInputVat.Text = "";
            txtInputAmt.Text = "";
            txtInputRealAmt.Text = "";
            ddlItems.SelectedIndex = 0;
            txtInputRoom.Text = "";
            txtInputSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputESearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputPayDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputIssDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtInputRoom.Enabled = false;
            imgbtnSearchCompNm.Enabled = false;
            lvPrintoutList.DataSource = null;
            lvPrintoutList.DataBind();
            HfReturnUserSeqId.Value = "";
            var sbWarning = new StringBuilder();
            sbWarning.Append("createDebit(false)");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Register", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
        }

        protected void rbMoneyCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            var strPaymentDt = txtInputPayDt.Text.Replace("-", "");
            dtReturn1 = InvoiceMngBlo.SelectExRateByPayDt(txtHfRentCd.Text, strPaymentDt);
            
            if (rbMoneyCd.SelectedValue == "VND")
            {
                txtInputEx.Text = "1.00";
            }
            else
            {
                txtInputEx.Text = double.Parse(dtReturn1.Rows[0][0].ToString()).ToString("###,##0.00");
            }
        }


        protected void imgLoadData_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
    }
}
