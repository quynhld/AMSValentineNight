using System;
using System.Data;
using System.Globalization;
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

using KN.Manage.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class HiddenPrintedDebit : BasePage
    {

        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;
        public string DOC_NO = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {                  

                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params["RentCd"] != null)
            {
                txtHfRentCd.Text = !string.IsNullOrEmpty(Request.Params["RentCd"]) ? Request.Params["RentCd"] : CommValue.RENTAL_VALUE_APTSHOP;
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APTSHOP;
            }

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
            }

           
        }

        protected void InitControls()
        {
            if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                ltItem.Text = "항목";
            }
            else
            {
                ltItem.Text = "Item";
            }
            //Period
            txtStartDt.Text = DateTime.Now.AddMonths(-1).ToString("s").Substring(0, 10);
            txtEndDt.Text = DateTime.Now.AddMonths(1).ToString("s").Substring(0, 10);            
            
            
            hfStartDt.Value = txtStartDt.Text.Replace("-", "").Replace(".", "");
            hfEndDt.Value = txtEndDt.Text.Replace("-", "").Replace(".", "");
            hfLink.Value = Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text;
            MakePrintYNDdl();
            MakeItemDdl();          
            LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
        }

        protected void LoadData()
        {
            var payCode = ddlItems.SelectedValue;
            var roomNo = txtRoomNo.Text;
            var rentCode = ddlInsRentCd.SelectedValue;
            var nm = txtTenant.Text;
            var strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            var strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");
            //KN_USP_SELECT_HIDDEN_PRINTED_DEBIT_I00
            var dtReturn = MngPaymentBlo.SelectHiddenPrintedDebit(rentCode, roomNo, payCode, nm, strStartDt, strEndDt, ddlPrintYN.SelectedValue);            
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
        }



        protected void MakeItemDdl()
        {

            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItems.Items.Clear();

            ddlItems.Items.Add(new ListItem("All Fee", ""));

            foreach (var dr in dtReturn.Select())
            {
                if (dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_MNGFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE))
                {
                    ddlItems.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }
      

       

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING))
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                    {
                        ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
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
            var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                
            if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
            {
                txtHfUserSeq.Text = drView["UserSeq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["FeeTyName"].ToString()))
            {
                var ltFeeType = (Literal)iTem.FindControl("ltFeeType");
                ltFeeType.Text = drView["FeeTyName"].ToString();
            }
            if (!string.IsNullOrEmpty(drView["FeeTy"].ToString()))
            {
                var txthfFeeTypeCode = (TextBox)iTem.FindControl("txthfFeeTypeCode");
                txthfFeeTypeCode.Text = drView["FeeTy"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
            {
                var txtDebitCode = (TextBox)iTem.FindControl("txtDebitCode");
                txtDebitCode.Text = drView["RentCd"].ToString();
                hfRentCd.Value = drView["RentCd"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                ltInsRoomNo.Text = drView["RoomNo"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
            {
                var ltInvoice = (Literal)iTem.FindControl("ltInvoice");
                ltInvoice.Text = TextLib.StringDecoder(drView["InvoiceNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
            {
                var ltTenantNm = (Literal)iTem.FindControl("ltTenantNm");
                ltTenantNm.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["StartDt"].ToString()))
            {
                var ltPeriod = (Literal)iTem.FindControl("ltPeriod");
                ltPeriod.Text = TextLib.MakeDateEightSlash(drView["StartDt"].ToString()) + " ~ " + TextLib.MakeDateEightSlash(drView["EndDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["RealMonthViAmtNo"].ToString()))
            {
                var ltTotalAmount = (Literal)iTem.FindControl("ltTotalAmount");
                ltTotalAmount.Text = double.Parse(drView["RealMonthViAmtNo"].ToString()).ToString("###,##0");
            }
                                             
            if (!string.IsNullOrEmpty(drView["Discount"].ToString()))
            {
                var ltDiscount = (Literal)iTem.FindControl("ltDiscount");
                ltDiscount.Text = double.Parse(drView["Discount"].ToString()).ToString("###,##0");                   
            }               

            var txtRefSeq = (TextBox)iTem.FindControl("txtRef_Seq");

            if (!string.IsNullOrEmpty(drView["REF_SEQ"].ToString()))
            {
                txtRefSeq.Text = TextLib.StringDecoder(drView["REF_SEQ"].ToString());
            }
            
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
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
            var cb = (CheckBox)sender;
            var item = (ListViewItem)cb.NamingContainer;
            var dataItem = (ListViewDataItem)item;

            var status = (((CheckBox)lvPrintoutList.Items[dataItem.DataItemIndex].FindControl("chkboxList")).Checked == true)?true:false;
            var code = ((Literal)lvPrintoutList.Items[dataItem.DataItemIndex].FindControl("ltInsRoomNo")).Text;

            for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
            {
                if (status )
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

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void lbtNotCreated_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text , CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        protected void lnkCreatedNote_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
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
                var intCheckRow = CommValue.NUMBER_VALUE_0;
                var refPrintBundleNo = string.Empty;

                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }
                InvoiceMngBlo.UpdatingNullPrintBundleNoHoadonInfo(hfRentCd.Value);

                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;

                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtRef_Seq")).Text;

                    if (string.IsNullOrEmpty(refPrintBundleNo))
                    {
                        refPrintBundleNo = refSeq;
                    }

                    InvoiceMngBlo.UpdatingPrintBundleNoHoadonInfo(refSeq, refPrintBundleNo);
                    intCheckRow++;
                }
                txthfPrintBundleNo.Value = refPrintBundleNo;
                // 선택 사항이 있는지 없는지 체크
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    InvoiceMngBlo.UpdatingHoadonInfoHiddenN(refPrintBundleNo);
                    LoadData();
                }
                else
                {
                    Aleart(AlertNm["INFO_HAS_NO_SELECTED_ITEM"]);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkPrint_Click(object sender, EventArgs e)
        {
            try
            {
                var intCheckRow = CommValue.NUMBER_VALUE_0;                
                var refPrintBundleNo = string.Empty;

                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }
                InvoiceMngBlo.UpdatingNullPrintBundleNoHoadonInfo(hfRentCd.Value);

                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;

                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtRef_Seq")).Text; 
                    
                    if (string.IsNullOrEmpty(refPrintBundleNo))
                    {
                        refPrintBundleNo = refSeq;
                    }

                    InvoiceMngBlo.UpdatingPrintBundleNoHoadonInfo(refSeq, refPrintBundleNo);
                    intCheckRow++;
                }
                txthfPrintBundleNo.Value = refPrintBundleNo; 
                // 선택 사항이 있는지 없는지 체크
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    InvoiceMngBlo.UpdatingHoadonInfoHiddenY(refPrintBundleNo);
                    LoadData();
                }
                else
                {
                    Aleart(AlertNm["INFO_HAS_NO_SELECTED_ITEM"]);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlUV_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
            chkAll.Checked = false;
        }

        protected void lvPrintoutList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfRefSeq.Value;
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                Response.Redirect("DebitDetail.aspx?seq="+ reft+"&RentCd="+hfRentCd.Value, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

       

        private void MakePrintYNDdl()
        {
            ddlPrintYN.Items.Clear();
            ddlPrintYN.Items.Add(new ListItem("No", "N"));
            ddlPrintYN.Items.Add(new ListItem("Yes", "Y"));
        }
               

        protected void imgbtnLoadData_Click(object sender, ImageClickEventArgs e)
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

        protected void CloseLoading()
        {
            var sbWarning = new StringBuilder();
            sbWarning.Append("CloseLoading();");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);            
        }

        protected void Aleart(string message)
        {
            var sbNoSelection = new StringBuilder();
            sbNoSelection.Append("alert('" + message + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);            
        }

        protected void ddlPrintYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {                             
                if (ddlPrintYN.SelectedValue.Equals("N"))
                {
                    lnkbtnCancel.Visible = false;
                    lnkPrint.Visible =  true;
                    LoadData();  
                }
                else if (ddlPrintYN.SelectedValue.Equals("Y"))
                {
                    lnkbtnCancel.Visible = true;
                    lnkPrint.Visible = false;
                    LoadData();  
                }
                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}
