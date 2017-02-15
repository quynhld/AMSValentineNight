using System;
using System.Data;
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
using KN.Manage.Biz;
using KN.Settlement.Biz;
using KN.Resident.Biz;
using KN.Settlement.Biz;


namespace KN.Web.Management.Manage
{
    public partial class HoaDonRevoke : BasePage
    {
        string strInit = string.Empty;
        int intInit = CommValue.NUMBER_VALUE_0;
        object objTag = new object();

        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;


        DataTable dtMaster = new DataTable();

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
                      
            ltInvoiceNo.Text = "Invoice No";
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltFeeName.Text = "Fee Name";                                    

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            //lnkbtnReplace.Visible = Master.isModDelAuthOk;
            lnkbtnReplace.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";
            MakePrintSerialDdl();
            MakePaymentDdl();
        }

        protected void LoadData()
        {         
            lvPrintoutList.DataSource = null;
            lvPrintoutList.DataBind();
            var serialNo = ddlSerial.SelectedValue;

            //KN_USP_INSERT_INVOICEAPT_TEMP_M001
            var dsReturn = InvoiceMngBlo.InsertTempInvoiceApt(txtInvoice.Text);
            hfOldInvoiceNo.Value = txtInvoice.Text;
            dtMaster = ReceiptMngBlo.SelectCancelListAptHoadon(txtInvoice.Text,serialNo);
           
            lvPrintoutList.DataSource = dtMaster;
            lvPrintoutList.DataBind();
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
            var ltInvoiceNoP = (Literal)iTem.FindControl("ltInvoiceNoP");
            if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
            {
                ltInvoiceNoP.Text = drView["InvoiceNo"].ToString();
                hfOldInvoiceNo.Value = ltInvoiceNoP.Text;
            }


            var ltInsRoomNoP = (Literal)iTem.FindControl("ltInsRoomNoP");

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                ltInsRoomNoP.Text = drView["RoomNo"].ToString();
            }


            var ltInsDescription = (Literal)iTem.FindControl("ltInsDescription");

            if (!string.IsNullOrEmpty(drView["Description"].ToString()))
            {
                ltInsDescription.Text = TextLib.StringDecoder(drView["Description"].ToString());
            }

            var ltTotal = (Literal)iTem.FindControl("ltTotal");

            if (!string.IsNullOrEmpty(drView["Amount"].ToString()))
            {
                ltTotal.Text = TextLib.MakeVietIntNo(double.Parse(drView["Amount"].ToString()).ToString("###,##0"));
            }
        }

        protected void lvPrintoutList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                double netAmt = CommValue.NUMBER_VALUE_0_0;
                double vatAmt = CommValue.NUMBER_VALUE_0_0;
                var pSeq = "";
                var seq = 0;

                if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtPSeq")).Text))
                {
                    pSeq = ((TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtPSeq")).Text;
                }

                if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtSeq")).Text))
                {
                    seq = Int32.Parse(((TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtSeq")).Text);
                }

                if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtNetPrice")).Text))
                {
                    netAmt = double.Parse(((TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtNetPrice")).Text.Replace(".", ""));
                }

                if (!string.IsNullOrEmpty(((TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtVat")).Text))
                {
                    vatAmt = double.Parse(((TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtVat")).Text.Replace(".", ""));
                }

                // KN_USP_UPDATE_CANCELINVOICE_APT_U00
                var dsReturn = InvoiceMngBlo.UpdatingCancelInvoiceForApt(pSeq,seq,netAmt,vatAmt);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPrintoutList_LayoutCreated(object sender, EventArgs e)
        {

        }

        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
           // txthfrefSerialNo.Value = ((Label)e.Item.FindControl("txtHfrefSerialNo1")).Text;
        }

        protected void lvPrintoutList_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
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
        protected void LoadDetails(string refSeq)
        {
           
        }

        

        protected void lnkbtnReplace_Click(object sender, EventArgs e)
        {
                       
            try
            {
                var reason = rbReason.SelectedValue;
                StringBuilder sbNoSelection;
                var refPrintNo = string.Empty;

                if (lvPrintoutList.Items.Count <= 0)
                {
                     sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                    return;
                }

                if (reason=="2")
                {
                    if (lvPrintoutListNew.Items.Count <= 0)
                    {
                        sbNoSelection = new StringBuilder();

                        sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                        return;
                    }

                    var intCheckRow = CommValue.NUMBER_VALUE_0;


                    if (lvPrintoutListNew.Items.Count <= 0)
                    {
                        return;
                    }
                    InvoiceMngBlo.UpdatingRefPrintNoAPTForReset();
                    for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutListNew.Items.Count; i++)
                    {
                        if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                        if (!((CheckBox)lvPrintoutListNew.Items[i].FindControl("chkboxList")).Checked) continue;

                        var refSeq = ((TextBox)lvPrintoutListNew.Items[i].FindControl("txtHfRefSeq")).Text;
                        var payDt = ((TextBox)lvPrintoutListNew.Items[i].FindControl("txtInsRegDt")).Text.Replace("-", "");
                        var seq = ((TextBox)lvPrintoutListNew.Items[i].FindControl("txtHfPrintDetSeq")).Text;

                        if (string.IsNullOrEmpty(refPrintNo))
                        {
                            refPrintNo = ((TextBox)lvPrintoutListNew.Items[i].FindControl("txtHfRefSeq")).Text;
                        }
                        InvoiceMngBlo.UpdatingRefPrintNoForAPTNew(refSeq, hfRentCd.Value, refPrintNo, payDt, Int32.Parse(seq));
                        intCheckRow++;
                    }
                    hfsendParam.Value = refPrintNo;
                }
                                               
                var sbPrintOut = new StringBuilder();

                sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupCancelInvoiceAPT.aspx?Datum0=" + hfOldInvoiceNo.Value + "&Datum1=" + refPrintNo + "&Datum2=" + rbReason.SelectedValue + "\", \"HoadonCancel\", \"status=no, resizable=no, width=740, height=900, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }


        protected void chkAll_CheckedChanged1(object sender, EventArgs e)
        {
            
        }


        protected void lnkbtnSearchNew_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                LoadDataNew();

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void LoadDataNew()
        {
            string strSearchNm = string.Empty;
            string strSearchRoomNo = string.Empty;
            string strMaxInvoiceNo = "0000000";

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
            //var eDt = txtESearchDt.Text.Replace("-", "");
            // KN_USP_SET_SELECT_APT_HOADONINFO_S01
            var dtReturn = ReceiptMngBlo.SelectPrintListAptHoadon(hfRentCd.Value, txtRoomNo.Text, sPayDt,
                                                                        ddlItemCd.SelectedValue, txtCompanyNm.Text, Session["LANGCD"].ToString());

            if (dtReturn != null)
            {
                lvPrintoutListNew.DataSource = dtReturn;
                lvPrintoutListNew.DataBind();

                chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
            }

            //// 세금계산서 최대값 조회
            // DataTable dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);

            //if (ddlCompNo.SelectedValue.Equals(CommValue.MAIN_COMP_CD))
            //{
            var dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();
            //}
            //else
            //{
            //    dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.MAIN_COMP_CD);
            //}

            if (dtMaxReturn != null)
            {
                if (dtMaxReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    if (!string.IsNullOrEmpty(dtMaxReturn.Rows[0]["MaxInvoiceNo"].ToString()))
                    {
                        strMaxInvoiceNo = dtMaxReturn.Rows[0]["MaxInvoiceNo"].ToString().PadLeft(7, '0');
                    }
                }
            }

            ltInsMaxNo.Text = strMaxInvoiceNo;          
        }


        protected void lvPrintoutListNew_LayoutCreated(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPrintoutListNew_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvPrintoutListNew_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
            var txtHfPrintDetSeq = (TextBox)iTem.FindControl("txtHfPrintDetSeq");

            var txtHfRefSeq = (TextBox)iTem.FindControl("txtHfRefSeq");
            if (!string.IsNullOrEmpty(drView["Ref_Seq"].ToString()))
            {
                txtHfRefSeq.Text = drView["Ref_Seq"].ToString();
            }

            var txtHfRefPrintNo = (TextBox)iTem.FindControl("txtHfRefPrintNo");
            if (!string.IsNullOrEmpty(drView["Ref_PrintNo"].ToString()))
            {
                txtHfRefPrintNo.Text = drView["Ref_PrintNo"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
            {
                txtHfUserSeq.Text = drView["UserSeq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["DataYear"].ToString()))
            {
                var ltPeriod = (Literal)iTem.FindControl("ltPeriod");
                ltPeriod.Text = drView["DataYear"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["seq"].ToString()))
            {
                txtHfPrintDetSeq.Text = drView["seq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["Description"].ToString()))
            {
                var txtInsDescription = (TextBox)iTem.FindControl("txtInsDescription");
                txtInsDescription.Text = TextLib.StringDecoder(drView["Description"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Amount"].ToString()))
            {
                var txtInsAmtViNo = (TextBox)iTem.FindControl("txtInsAmtViNo");
                txtInsAmtViNo.Text = TextLib.MakeVietIntNo(double.Parse(drView["Amount"].ToString()).ToString("###,##0"));
                txtInsAmtViNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            }
            var txtInsRegDt = (TextBox)iTem.FindControl("txtInsRegDt");

            if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
            {
                txtInsRegDt.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());
            }

            intRowsCnt++;
        }

        private void MakePrintSerialDdl()
        {
            ddlSerial.Items.Clear();
            ddlSerial.Items.Add(new ListItem("CA/15T", "CA/15T"));
            ddlSerial.Items.Add(new ListItem("CA/14T", "CA/14T"));
            ddlSerial.Items.Add(new ListItem("CA/13T", "CA/13T"));
        }


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

        protected void imgUpdateInvoice_Click(object sender, ImageClickEventArgs e)
        {
            try
            {               
                var insCompCd = Session["CompCd"].ToString();
                var insMemNo = Session["MemNo"].ToString();
                var insMemIP = Session["UserIP"].ToString();
                //KN_USP_UPDATE_INVOICENO_HOADONINFOAPT_U00
                var oldInvoice = hfOldInvoiceNo.Value;
                var refPrint = hfsendParam.Value;

                InvoiceMngBlo.InsertCancelInvoiceHoadonInfoApt(refPrint, oldInvoice, rbReason.SelectedValue, insCompCd, insMemNo, insMemIP);   
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}
