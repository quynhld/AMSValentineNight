using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;
using KN.Manage.Biz;
using KN.Resident.Biz;
using KN.Settlement.Biz;
using KN.Common.Method.Common;


namespace KN.Web.Settlement.Balance
{
    public partial class TransferStatement : BasePage
	{
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;

		protected void Page_Load(object sender, EventArgs e)
		{
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
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

        protected void InitControls()
        {
          
           

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnTran.Visible = false; 
          
            txtStartDt.Text = DateTime.Now.ToString("s").Substring(0, 10);            

            //hfPrintBundleNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txthfBundleSeq")).Text;

            MakeRevenueTypeDdl();
            MakeInvoicePaymentDdl();
            MakeSendNotSendDdl();

        }
        private void MakeInvoicePaymentDdl()
        {
            ddlItems.Items.Clear();
            ddlItems.Items.Add(new ListItem("INVOICE", "0001"));
            ddlItems.Items.Add(new ListItem("PAYMENT", "0002"));

        }

        private void MakeSendNotSendDdl()
        {
            ddlSNS.Items.Clear();            
            ddlSNS.Items.Add(new ListItem("Send", "0001"));
            ddlSNS.Items.Add(new ListItem("Not Send", "0002"));
            
        }

        private void MakeRevenueTypeDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentBlo.SelectPayTypeDdl(Session["LangCd"].ToString(), "RentCd");

            ddlPayType.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlPayType.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));                
            }

        }

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

        protected void LoadData()
        {
            var listType = ddlItems.SelectedItem.Text;
            var rentType = ddlPayType.SelectedValue;
            string searchDate = txtStartDt.Text.Replace("-", "").Replace(".", "");
            var sendCode = ddlSNS.SelectedValue;


            var dtReturn = MngPaymentBlo.SelectStatementList(listType, rentType, searchDate, sendCode);   
            if (dtReturn == null) return;
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
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

        protected void lknbtnCancel_Click(object sender, EventArgs e)
        {

            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                int intCheckRow = CommValue.NUMBER_VALUE_0;
                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }
                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;
                    string invoiceNO = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsInvoiceNo")).Text;
                    string compNo = Session["CompCd"].ToString();
                    string rentCode = hfRentCd.Value;
                    string feeTy = ((TextBox)lvPrintoutList.Items[i].FindControl("txtFeeType")).Text;
                    string paymentCode = ((TextBox)lvPrintoutList.Items[i].FindControl("txtPaymentCode")).Text;
                    string userSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtUserSeq")).Text;
                    string tenantsNm = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsTenant")).Text;
                    string description = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsDescription")).Text;
                    string roomNo = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsRoomNo")).Text;
                    string netAmount = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsAmount")).Text;
                    string vatAmount = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsTax")).Text;
                    string totAmount = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsTotal")).Text;
                    string sendType = ddlItems.SelectedItem.ToString();
                    string memIP = Session["UserIP"].ToString();
                   
                    string paymentDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
                    string insMemNo = Session["MemNo"].ToString();
                    string exchangeRate = ((TextBox)lvPrintoutList.Items[i].FindControl("txtExchangeRate")).Text;
                    string refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtRefSeq")).Text;
                    string billType = ((TextBox)lvPrintoutList.Items[i].FindControl("txtBillType")).Text;
                    string listType = ((TextBox)lvPrintoutList.Items[i].FindControl("txtListType")).Text;

                    string slipno = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsSlipNo")).Text;

                    hfRefInvoiceNo.Value = invoiceNO.ToString();
                    hfRoomNo.Value = roomNo.ToString();
                    hfUserSeq.Value = userSeq.ToString();
                    hfMemNo.Value = insMemNo.ToString();
                    hfIP.Value = memIP.ToString();
                    hfRefSeq.Value = refSeq.ToString();
                    hfListType.Value = listType.ToString();
                    hfSlipNo.Value = slipno.ToString();
                    

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Cancel Transfer", "fnOccupantList();", CommValue.AUTH_VALUE_TRUE);
                    
                    intCheckRow++;
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


            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {


                var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    ltInsRoomNo.Text = drView["RoomNo"].ToString();
                }

                var txtPaymentCode = (TextBox)iTem.FindControl("txtPaymentCode");

                if (!string.IsNullOrEmpty(drView["PAYMENTCODE"].ToString()))
                {
                    txtPaymentCode.Text = TextLib.StringDecoder(drView["PAYMENTCODE"].ToString());
                }

                

                var txtBillType = (TextBox)iTem.FindControl("txtBillType");

                if (!string.IsNullOrEmpty(drView["BILL_TYPE"].ToString()))
                {
                    txtBillType.Text = TextLib.StringDecoder(drView["BILL_TYPE"].ToString());
                }

                var txtRefSeq = (TextBox)iTem.FindControl("txtRefSeq");

                if (!string.IsNullOrEmpty(drView["REF_SEQ"].ToString()))
                {
                    txtRefSeq.Text = TextLib.StringDecoder(drView["REF_SEQ"].ToString());
                }

                var txtExchangeRate = (TextBox)iTem.FindControl("txtExchangeRate");

                if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
                {
                    txtExchangeRate.Text = TextLib.StringDecoder(drView["DongToDollar"].ToString());
                }

                var txtFeeType = (TextBox)iTem.FindControl("txtFeeType");

                if (!string.IsNullOrEmpty(drView["Type"].ToString()))
                {
                    txtFeeType.Text = TextLib.StringDecoder(drView["Type"].ToString());
                }
              
                var txtUserSeq = (TextBox)iTem.FindControl("txtUserSeq");

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    txtUserSeq.Text = TextLib.StringDecoder(drView["UserSeq"].ToString());
                }

                var txtListType = (TextBox)iTem.FindControl("txtListType");

                if (!string.IsNullOrEmpty(drView["LIST_TYPE"].ToString()))
                {
                    txtListType.Text = TextLib.StringDecoder(drView["LIST_TYPE"].ToString());
                }               
               

                var ltInsTenant = (Literal)iTem.FindControl("ltInsTenant");

                if (!string.IsNullOrEmpty(drView["Tenant"].ToString()))
                {
                    ltInsTenant.Text = drView["Tenant"].ToString();
                }

                var ltInsType = (Literal)iTem.FindControl("ltInsType");

                if (!string.IsNullOrEmpty(drView["TypeName"].ToString()))
                {
                    ltInsType.Text = drView["TypeName"].ToString();
                }


                var ltInsTotal = (Literal)iTem.FindControl("ltInsTotal");

                if (!string.IsNullOrEmpty(drView["Total"].ToString()))
                {
                    ltInsTotal.Text = double.Parse(TextLib.StringDecoder(drView["Total"].ToString())).ToString("###,##0");
                }

                var ltInsInvoiceNo = (Literal)iTem.FindControl("ltInsInvoiceNo");

                if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
                {
                    ltInsInvoiceNo.Text = TextLib.StringDecoder(drView["InvoiceNo"].ToString());
                }

                //Literal ltInsDescription = (Literal)iTem.FindControl("ltInsDescription");

                //if (!string.IsNullOrEmpty(drView["Desciption"].ToString()))
                //{
                //    ltInsDescription.Text = drView["Desciption"].ToString();
                //}

                var ltInsSlipNo = (Literal)iTem.FindControl("ltInsSlipNo");

                if (!string.IsNullOrEmpty(drView["SlipNo"].ToString()))
                {
                    ltInsSlipNo.Text = TextLib.StringDecoder(drView["SlipNo"].ToString());
                }

                var ltlnsTrEmp = (Literal)iTem.FindControl("ltlnsTrEmp");

                if (!string.IsNullOrEmpty(drView["Tr_Emp_ID"].ToString()))
                {
                    ltlnsTrEmp.Text = TextLib.StringDecoder(drView["Tr_Emp_ID"].ToString());
                }

                var ltInsTrDate = (Literal)iTem.FindControl("ltInsTrDate");

                if (!string.IsNullOrEmpty(drView["Tr_Date"].ToString()))
                {
                    ltInsTrDate.Text = TextLib.StringDecoder(drView["Tr_Date"].ToString());
                }                                
                intRowsCnt++;
            }
        }

        protected void lvPrintoutList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {                
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

        protected void lnkbtnTran_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                int intCheckRow = CommValue.NUMBER_VALUE_0;
                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }
                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;
                    string invoiceNO = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsInvoiceNo")).Text;
                    string compNo = Session["CompCd"].ToString();
                    string rentCode = hfRentCd.Value;
                    string feeTy = ((TextBox)lvPrintoutList.Items[i].FindControl("txtFeeType")).Text;
                    string paymentCode = ((TextBox)lvPrintoutList.Items[i].FindControl("txtPaymentCode")).Text;
                    string userSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtUserSeq")).Text;
                    string tenantsNm = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsTenant")).Text;
                    string description = "Test";
                    string roomNo = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsRoomNo")).Text;
                    string netAmount = "0";
                    string vatAmount = "0";
                    string totAmount = ((Literal)lvPrintoutList.Items[i].FindControl("ltInsTotal")).Text;
                    string sendType = ddlItems.SelectedItem.ToString();
                    string memIP = Session["UserIP"].ToString();

                    //string  = "";
                    string paymentDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
                    string insMemNo = Session["MemNo"].ToString();
                    string exchangeRate = ((TextBox)lvPrintoutList.Items[i].FindControl("txtExchangeRate")).Text;
                    string refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtRefSeq")).Text;
                    string billType = ((TextBox)lvPrintoutList.Items[i].FindControl("txtBillType")).Text;
                    string listType = ((TextBox)lvPrintoutList.Items[i].FindControl("txtListType")).Text;

                    double dblInsExchageRate = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(exchangeRate))
                    {
                        dblInsExchageRate = double.Parse(exchangeRate);
                    }

                    double dbnetAmount = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(netAmount))
                    {
                        dbnetAmount = double.Parse(netAmount);
                    }
                    double dbvatAmount = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(vatAmount))
                    {
                        dbvatAmount = double.Parse(vatAmount);
                    }
                    double dbytotAmount = CommValue.NUMBER_VALUE_0_0;

                    if (!string.IsNullOrEmpty(totAmount))
                    {
                        dbytotAmount = double.Parse(totAmount);
                    }
                    compNo = "11111112";
                    if (compNo == "11111112")
                    {
                        //MngPaymentBlo.TransferStatement(invoiceNO, compNo, rentCode, feeTy, paymentCode, userSeq, tenantsNm, description,
                        //                                 roomNo, dbnetAmount, dbvatAmount, dbytotAmount, sendType, paymentDt, insMemNo, dblInsExchageRate, refSeq, billType, memIP);                        
                    }
                    else
                    {
                        //MngPaymentBlo.TransferStatementApt(invoiceNO, compNo, rentCode, feeTy, paymentCode, userSeq, tenantsNm, description,
                        //                                 roomNo, dbnetAmount, dbvatAmount, dbytotAmount, sendType, paymentDt, insMemNo, dblInsExchageRate, refSeq, billType, memIP);
                    }
                    intCheckRow++;
                }
                LoadData();

                StringBuilder sbWarning = new StringBuilder();
                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_MODIFY_ISSUE"]);
                sbWarning.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
             catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }        
        }

        protected void chkAll_CheckedChanged(object sender, EventArgs e)
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

        protected void ddlSNS_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlSNS.SelectedValue.ToString() == "0001")
            {
                lnkbtnTran.Visible = false;
                lknbtnCancel.Visible = true;
            }
            else
            {
                lnkbtnTran.Visible = true;
                lknbtnCancel.Visible = false;
            }

            LoadData();
        }

        /// <summary>
        /// 상세보기 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                //// 세션체크
                //AuthCheckLib.CheckSession();

                //StringBuilder sbView = new StringBuilder();
                //sbView.Append(Master.PAGE_VIEW);
                //sbView.Append("?");
                //sbView.Append(Master.PARAM_DATA1);
                //sbView.Append("=");
                //sbView.Append(hfCounselCd.Value);
                //sbView.Append("&");
                //sbView.Append(Master.PARAM_DATA2);
                //sbView.Append("=");
                //sbView.Append(hfCounselSeq.Value);

                //Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                //Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);
                Response.Redirect("PopupCancelTransfer.aspx", CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPrintoutList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
	}
}