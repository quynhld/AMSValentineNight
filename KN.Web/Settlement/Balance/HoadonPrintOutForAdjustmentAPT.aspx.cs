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

using KN.Resident.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class HoadonPrintOutForAdjustmentAPT : BasePage
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
            var strMaxInvoiceNo = "0000000";
            ltMaxNo.Text = TextNm["MAXNUMBER"];
            // DropDownList Setting
            MakePaymentDdl();
            ltDate.Text = TextNm["MONTH"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltPaymentTy.Text = TextNm["ITEM"];
            ltPaymentDt.Text = TextNm["REGISTDATE"];
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];
            lnkbtnIssuing.Text = TextNm["ISSUING"];

            txtSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnIssuing.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";

            var dtReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MaxInvoiceNo"].ToString()))
                    {
                        strMaxInvoiceNo = dtReturn.Rows[0]["MaxInvoiceNo"].ToString().PadLeft(7, '0');
                    }
                }
            }

            ltInsMaxNo.Text = strMaxInvoiceNo;
            lnkbtnIssuing.Visible = Master.isWriteAuthOk;

        }

        protected void LoadData()
        {
            var strMaxInvoiceNo = "0000000";
            if (lvPrintoutList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
            }

            strInit = CommValue.AUTH_VALUE_EMPTY;
            intInit = CommValue.NUMBER_VALUE_0;
            var sPayDt = txtSearchDt.Text.Replace("-", "");
            // KN_USP_SET_SELECT_APT_HOADONINFO_S01
            var dtReturn = ReceiptMngBlo.SelectPrintListAptAdjHoadon(hfRentCd.Value, txtRoomNo.Text, sPayDt,
                                                                        ddlItemCd.SelectedValue, txtCompanyNm.Text, Session["LANGCD"].ToString());

            if (dtReturn != null)
            {
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();

                chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
            }

            //// Load max invoice no
              var  dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();
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
            hfsendOldInvoiceNo.Value = txtInvoiceNo.Text;
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
            var txtHfPrintSeq = (TextBox)iTem.FindControl("txtHfPrintSeq");
            var txtHfPrintDetSeq = (TextBox)iTem.FindControl("txtHfPrintDetSeq");
            var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");


            var txtHfRefSeq = (TextBox)iTem.FindControl("txtHfRefSeq");
            if (!string.IsNullOrEmpty(drView["Ref_Seq"].ToString()))
            {
                txtHfRefSeq.Text = drView["Ref_Seq"].ToString();
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

            var ltInsBillNm = (Literal)iTem.FindControl("ltInsBillNm");

            if (!string.IsNullOrEmpty(drView["BillNm"].ToString()))
            {
                ltInsBillNm.Text = TextLib.StringDecoder(drView["BillNm"].ToString());
            }

            var txtHfBillCd = (TextBox)iTem.FindControl("txtHfBillCd");

            if (!string.IsNullOrEmpty(drView["BillCd"].ToString()))
            {
                txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
            }
            hfBillCd.Value = txtHfBillCd.Text;

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
                hfPayDt.Value = txtInsRegDt.Text.Replace("-", "");
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
        /// 출력버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {
           
            try
            {
                var paydt = "";
                var intCheckRow = CommValue.NUMBER_VALUE_0;
                var refPrintNo = string.Empty;
                var billCd = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfBillCd")).Text;
                var invoiceNo = txtInvoiceNo.Text;
                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }
                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (intCheckRow == CommValue.NUMBER_VALUE_0)
                    {
                        // Initializing Ref_PrintNo
                        InvoiceMngBlo.UpdatingRefPrintNoAPTForReset();
                    }
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;
                    
                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    var seq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfPrintDetSeq")).Text;
                    var payDt = ((TextBox)lvPrintoutList.Items[i].FindControl("txtInsRegDt")).Text.Replace("-", "");
                    if (string.IsNullOrEmpty(refPrintNo))
                    {
                        refPrintNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    }
                    InvoiceMngBlo.UpdatingRefPrintNoForAPTNew(refSeq, hfRentCd.Value, refPrintNo, payDt,Int32.Parse(seq));
                    intCheckRow++;
                }               
                hfsendParam.Value = refPrintNo;
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    if (intCheckRow <= 7)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + refPrintNo + "','" + billCd + "','" + paydt + "','" + invoiceNo + "');", CommValue.AUTH_VALUE_TRUE);                        
                    }
                    else
                    {
                        Alert("Please select under 8 paid amount for each invoice !");                        
                    }
                }
                else
                {
                    // 화면 초기화
                    LoadData();
                    // 선택된 대상 없음
                    Alert(AlertNm["INFO_HAS_NO_SELECTED_ITEM"]);
                }
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

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void chkAll_CheckedChanged1(object sender, EventArgs e)
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

        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {             
                var reft = hfsendParam.Value;
                var insCompCd = Session["CompCd"].ToString();
                var insMemNo = Session["MemNo"].ToString();
                var insMemIP = Session["UserIP"].ToString();
                //KN_USP_UPDATE_INVOICENO_HOADONINFOAPT_U00
                InvoiceMngBlo.UpdatingInvoiceNoHoadonInfoApt(reft, insCompCd, insMemNo, insMemIP);         
                 LoadData();                                   
        }

        protected void imgUpdateInvoice_Click(object sender, ImageClickEventArgs e)
        {
            
            var reft = hfsendParam.Value;
            if (string.IsNullOrEmpty(reft)) return;
            var oldInvoice = hfsendOldInvoiceNo.Value;
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();
            //KN_USP_UPDATE_INVOICENO_HOADONINFOAPT_U01
            InvoiceMngBlo.UpdatingInvoiceAdjNoHoadonInfoAptNew(reft, oldInvoice, insCompCd, insMemNo, insMemIP);
            LoadData();
        }


        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            hfUserSeq.Value = ((TextBox)e.Item.FindControl("txtHfUserSeq")).Text;
            hfPayDt.Value = ((TextBox)e.Item.FindControl("txtInsRegDt")).Text;
        }

        protected void Alert(string message)
        {
            var sbNoSelection = new StringBuilder();
            sbNoSelection.Append("alert('" +message + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);            
        }
    }
}