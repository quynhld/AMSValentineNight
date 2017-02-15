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
    public partial class HoadonCancel : BasePage
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
            string strMaxInvoiceNo = "0000000";

           
            ltMaxNo.Text = TextNm["MAXNUMBER"];

         


            DataTable dtReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();

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
        }

        protected void LoadData()
        {
           
            strInit = CommValue.AUTH_VALUE_EMPTY;
            intInit = CommValue.NUMBER_VALUE_0;
            ;
            // KN_USP_SET_SELECT_APT_HOADONINFO_S01
            var dtReturn = ReceiptMngBlo.SelectAptHoadonForReplace(txtInvoiceNo.Text);

            if (dtReturn != null)
            {
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();

                txtRevInvoiceNo.Text = dtReturn.Rows[0]["InvoiceNo"].ToString();
                txtIRevNetAmt.Text = dtReturn.Rows[0]["UnitSellingPrice"].ToString();
                txtIRevVatAmt.Text = dtReturn.Rows[0]["VatAmt"].ToString();
                txtIRevGrandAmt.Text = dtReturn.Rows[0]["TotSellingPrice"].ToString();
                
            }
           
            var dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();          

        }

        protected void LoadDetails(string invoiceNo)
        {

            var dtReturn1 = ReceiptMngBlo.SelectAptHoadonForReplace(invoiceNo);

            txtRevInvoiceNo.Text = dtReturn1.Rows[0]["InvoiceNo"].ToString();
            txtIRevNetAmt.Text = dtReturn1.Rows[0]["UnitSellingPrice"].ToString();
            txtIRevVatAmt.Text = dtReturn1.Rows[0]["VatAmt"].ToString();
            txtIRevGrandAmt.Text = dtReturn1.Rows[0]["TotSellingPrice"].ToString();
           
            
        }

        /// <summary>
        /// 
        /// </summary>
      

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

                var txtHfPrintSeq = (TextBox)iTem.FindControl("txtHfPrintSeq");
                var txtHfPrintDetSeq = (TextBox)iTem.FindControl("txtHfPrintDetSeq");
                var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");


                if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    Literal lnsDate = (Literal)iTem.FindControl("lnsDate");
                    lnsDate.Text = TextLib.StringDecoder(drView["PaymentDt"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["SerialNo"].ToString()))
                {
                    Literal lnsSerialNo = (Literal)iTem.FindControl("lnsSerialNo");
                    lnsSerialNo.Text = TextLib.StringDecoder(drView["SerialNo"].ToString());
                }
                if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
                {
                    Literal lnsInvoiceNo = (Literal)iTem.FindControl("lnsInvoiceNo");
                    lnsInvoiceNo.Text = TextLib.StringDecoder(drView["InvoiceNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal InsRoomNo = (Literal)iTem.FindControl("InsRoomNo");
                    InsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Description"].ToString()))
                {
                    TextBox txtInsDescription = (TextBox)iTem.FindControl("txtInsDescription");
                    txtInsDescription.Text = TextLib.StringDecoder(drView["Description"].ToString());
                }
                
                if (!string.IsNullOrEmpty(drView["TotSellingPrice"].ToString()))
                {
                    TextBox txtInsTotal = (TextBox)iTem.FindControl("txtInsTotal");
                    txtInsTotal.Text = TextLib.MakeVietIntNo(double.Parse(drView["TotSellingPrice"].ToString()).ToString("###,##0"));                    
                }



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

                
                
                

                Literal ltInsBillNm = (Literal)iTem.FindControl("ltInsBillNm");

                if (!string.IsNullOrEmpty(drView["BillNm"].ToString()))
                {
                    ltInsBillNm.Text = TextLib.StringDecoder(drView["BillNm"].ToString());
                }

                TextBox txtHfBillCd = (TextBox)iTem.FindControl("txtHfBillCd");

                if (!string.IsNullOrEmpty(drView["BillCd"].ToString()))
                {
                    txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
                }

             

                  TextBox txtInsRegDt = (TextBox)iTem.FindControl("txtInsRegDt");

                  if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    txtInsRegDt.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());
                }
                


                TextBox txtHfPaymentDt = (TextBox)iTem.FindControl("txtHfPaymentDt");
                TextBox txtHfPaymentSeq = (TextBox)iTem.FindControl("txtHfPaymentSeq");
                TextBox txtHfPaymentDetSeq = (TextBox)iTem.FindControl("txtHfPaymentDetSeq");

                

                intRowsCnt++;
            }
        }
    
        
        /// <summary>
        /// 출력버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {
            // Initializing Ref_PrintNo
            InvoiceMngBlo.UpdatingRefPrintNoAPTForReset();

            try
            {
                bool isChestNut = CommValue.AUTH_VALUE_TRUE;   
            
                string com = Session["CompCd"].ToString();

                string strPrintOutDt = string.Empty;
                string strPrintSeq = string.Empty;
                string strSearchData = string.Empty;
                string strOldInvoiceNo = string.Empty;
                string strNewInvoiceNo = string.Empty;
                string strDescription = string.Empty;

                int intCheckRow = CommValue.NUMBER_VALUE_0;

                DataTable dtreturn = new DataTable();

                string strDocNo = string.Empty;

                string refPrintNo = string.Empty;

                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }

                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;
                    
                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                  
                    if (string.IsNullOrEmpty(refPrintNo))
                    {
                        refPrintNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    }
                    dtreturn = InvoiceMngBlo.UpdatingRefPrintNoForAPT(refSeq, hfRentCd.Value, refPrintNo, hfPayDt.Value);

                    intCheckRow++;
                }

                    hfsendParam.Value = refPrintNo;

                 if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    var sbPrintOut = new StringBuilder();

                    //    sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupDebitList1.aspx?Datum0=" + refPrintBundleNo + "&Datum1=" + txtReqDt.Text + "&Datum2=" + ddlUV.SelectedValue + "\", \"Debit List\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + refPrintNo + "');", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    // 화면 초기화
                    LoadData();

                    // 선택된 대상 없음
                    StringBuilder sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
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

  
      
      

        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            var invoiceNo = hfInvoiceNo.Value;
            //LoadData();
            LoadDetails(invoiceNo);      
           
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            
             try
            {
                bool isChestNut = CommValue.AUTH_VALUE_TRUE;   
            
                string com = Session["CompCd"].ToString();

                string strPrintOutDt = string.Empty;
                string strPrintSeq = string.Empty;
                string strSearchData = string.Empty;
                string strOldInvoiceNo = string.Empty;
                string strNewInvoiceNo = string.Empty;
                string strDescription = string.Empty;

                DataTable dtreturn = new DataTable();

                string strDocNo = string.Empty;

                string refPrintNo = string.Empty;


                var RevInvoiceNo = txtRevInvoiceNo.Text;
                var NewNetAmt = txtInputNetAmt.Text;
                var NewVatAmt = txtInputVatAmt.Text;
                var NewGrandAmt = txtInputGrandAmt.Text;

                double dbNewNetAmt = CommValue.NUMBER_VALUE_0_0;
                double dbNewGrandAmt = CommValue.NUMBER_VALUE_0_0;               
                double dbNewVatAmt = CommValue.NUMBER_VALUE_0_0;

                if (!string.IsNullOrEmpty(NewNetAmt))
                {
                    dbNewNetAmt = double.Parse(NewNetAmt);
                }
                if (!string.IsNullOrEmpty(NewGrandAmt))
                {
                    dbNewGrandAmt = double.Parse(NewGrandAmt);
                }

                if (!string.IsNullOrEmpty(NewVatAmt))
                {
                    dbNewVatAmt = double.Parse(NewVatAmt);
                }

                var InsCompCd = Session["CompCd"].ToString();
                var InsMemNo = Session["MemNo"].ToString();

                var InsMemIP = Session["UserIP"].ToString();

                string docNo = string.Empty;
                //InvoiceMngBlo.UpdatingIsPrintAPT(reft);
                InvoiceMngBlo.ReplaceHoadonInfoApt(RevInvoiceNo, dbNewNetAmt, dbNewVatAmt, dbNewGrandAmt, InsCompCd, InsMemNo, InsMemIP);  
               
                 
              
                    var sbPrintOut = new StringBuilder();

                    //    sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupDebitList1.aspx?Datum0=" + refPrintBundleNo + "&Datum1=" + txtReqDt.Text + "&Datum2=" + ddlUV.SelectedValue + "\", \"Debit List\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + refPrintNo + "');", CommValue.AUTH_VALUE_TRUE);
               
                    // 화면 초기화
                    LoadData();

                    // 선택된 대상 없음
                    StringBuilder sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}