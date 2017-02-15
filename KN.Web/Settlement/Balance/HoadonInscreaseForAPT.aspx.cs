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

using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class HoadonInscreaseForAPT : BasePage
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
            Master.PARAM_DATA1 = "RentCd";
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
            ltPaymentTy.Text = TextNm["ITEM"];
            ltPaymentDt.Text = TextNm["REGISTDATE"];
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];
            lnkbtnIssuing.Text = TextNm["ISSUING"];
            txtSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";
            lnkbtnIssuing.Visible = Master.isWriteAuthOk;

        }

        protected void LoadData()
        {
            string strSearchRoomNo = string.Empty;
            if (!string.IsNullOrEmpty(txtRoomNo.Text))
            {
                strSearchRoomNo = txtRoomNo.Text;
            }
            var sPayDt = txtSearchDt.Text.Replace("-", "");
            // KN_USP_SET_SELECT_APT_HOADONINFO_S02
            var dtReturn = ReceiptMngBlo.SelectReprintListAptHoadon(hfRentCd.Value, strSearchRoomNo, sPayDt,"", txtInvoiceNo.Text, Session["LANGCD"].ToString());
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
            chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
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
                CloseLoading();
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

            var txtHfRefSeq = (TextBox)iTem.FindControl("txtHfRefSeq");
            if (!string.IsNullOrEmpty(drView["Ref_Seq"].ToString()))
            {
                txtHfRefSeq.Text = drView["Ref_Seq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["SerialNo"].ToString()))
            {
                var ltSerialNo = (Literal)iTem.FindControl("ltSerialNo");
                ltSerialNo.Text = drView["SerialNo"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
            {
                var ltInvoiceNo = (Literal)iTem.FindControl("ltInvoiceNo");
                ltInvoiceNo.Text = TextLib.StringDecoder(drView["InvoiceNo"].ToString());
            }

            var ltInsBillNm = (Literal)iTem.FindControl("ltInsBillNm");

            if (!string.IsNullOrEmpty(drView["BillNm"].ToString()))
            {
                ltInsBillNm.Text = drView["BillNm"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["Description"].ToString()))
            {
                var txtInsDescription = (TextBox)iTem.FindControl("txtInsDescription");
                txtInsDescription.Text = drView["Description"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["Amount"].ToString()))
            {
                var ltAmount = (Literal)iTem.FindControl("ltAmount");
                ltAmount.Text = TextLib.MakeVietIntNo(double.Parse(drView["Amount"].ToString()).ToString("###,##0"));
            }
            var ltPaymentDt = (Literal)iTem.FindControl("ltPaymentDt");

            if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
            {
                ltPaymentDt.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());
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
            AuthCheckLib.CheckSession();
            try
            {
                var intCheckRow = CommValue.NUMBER_VALUE_0;
                var refPrintNo = string.Empty;
                if (lvPrintoutList.Items.Count <= 0)
                {
                    var sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                    return;
                }

                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {                   
                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    if (string.IsNullOrEmpty(refPrintNo))
                    {
                        refPrintNo = refSeq;
                    }
                    intCheckRow++;
                }
                hfsendParam.Value = refPrintNo;

                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + refPrintNo + "','" + txtAmount.Text.Replace(",","") + "');", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    // 화면 초기화
                    LoadData();

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

        protected void imgUpdateInvoice_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfsendParam.Value;
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();
            //KN_USP_INSERT_INCREASE_INVOICE_HOADONAPT_M00
            InvoiceMngBlo.InsertInvoiceIncreaseApt(reft,double.Parse(txtAmount.Text.Replace(",","")), insCompCd, insMemNo, insMemIP);
        }

        protected void CloseLoading()
        {
            var sbWarning = new StringBuilder();
            sbWarning.Append("CloseLoading();");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);            
        }

        protected void imgbtnLoadData_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
    }
}