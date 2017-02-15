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
    public partial class KNHoaDonCancel : BasePage
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

            //Period
            txtStartDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            txtEndDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            MakePaymentDdl(ddlItemCd);

            MakeInvoiceYN();
            LoadRentDdl(ddlRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            ltInvoiceNo.Text = "Invoice No";
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltFeeName.Text = "Fee Name";
            ltUserNm.Text = "Tenant";            
            ltAmount.Text = "Total";
            ltIssDt.Text = "Iss Date";
            ltBillTy.Text = "Bill Type";

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtCompanyNm.ClientID + "', '" + txtSearchRoom.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtCompanyNm.Text + "', '" + ddlRentCd.SelectedValue + "');";

            
            
        }

        protected void LoadData()
        {
            var strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            var strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");
            lvPrintoutList.DataSource = null;
            lvPrintoutList.DataBind();
            hfRentCd.Value = ddlRentCd.SelectedValue;
            //KN_SCR_SELECT_KN_CANCELINVOICE_LIST_S00
            var dtMaster = InvoiceMngBlo.SelectKNHoadonListCanCel(hfRentCd.Value, txtSearchRoom.Text, txtInvoice.Text, ddlItemCd.SelectedValue, Session["LangCd"].ToString(), ddlInvoiceYN.SelectedValue, strStartDt, strEndDt, txtCompanyNm.Text.Trim());
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
            }


            var ltInsRoomNoP = (Literal)iTem.FindControl("ltInsRoomNoP");

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                ltInsRoomNoP.Text = drView["RoomNo"].ToString();
            }


            var ltInsFeeNameP = (Literal)iTem.FindControl("ltInsFeeNameP");

            if (!string.IsNullOrEmpty(drView["FeeName"].ToString()))
            {
                ltInsFeeNameP.Text = TextLib.StringDecoder(drView["FeeName"].ToString());
            }

            var ltInsUserNmP = (Literal)iTem.FindControl("ltInsUserNmP");

            if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
            {
                ltInsUserNmP.Text = TextLib.StringDecoder(drView["UserNm"].ToString());
            }


            var ltInsAmtViNoP = (Literal)iTem.FindControl("ltInsAmtViNoP");

            if (!string.IsNullOrEmpty(drView["TotSellingPrice"].ToString()))
            {
                ltInsAmtViNoP.Text = TextLib.MakeVietIntNo(double.Parse(drView["TotSellingPrice"].ToString()).ToString("###,##0"));
            }

            var ltnsIssDtP = (Literal)iTem.FindControl("ltnsIssDtP");

            if (!string.IsNullOrEmpty(drView["IssuingDate"].ToString()))
            {
                ltnsIssDtP.Text = ltPeriod.Text = TextLib.MakeDateEightSlash(drView["IssuingDate"].ToString());
            }

            var ltnsBillTy = (Literal)iTem.FindControl("ltnsBillTy");

            if (!string.IsNullOrEmpty(drView["Bill_Type"].ToString()))
            {
                ltnsBillTy.Text = drView["Bill_Type"].ToString();
            }

            var txtRefSeq = (TextBox)iTem.FindControl("txtRef_Seq");

            if (!string.IsNullOrEmpty(drView["REF_SERIAL_NO"].ToString()))
            {
                txtRefSeq.Text = TextLib.StringDecoder(drView["REF_SERIAL_NO"].ToString());
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
               
                StringBuilder sbNoSelection;
                var refPrintNo = string.Empty;

                if (lvPrintoutList.Items.Count <= 0)
                {
                     sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
                    return;
                }
                
                                               
                var sbPrintOut = new StringBuilder();
                
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


        protected void lnkbtnSearchNew_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
               

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
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

            var txtRefSeq = (TextBox)iTem.FindControl("txtRef_Seq");

            if (!string.IsNullOrEmpty(drView["REF_SEQ"].ToString()))
            {
                txtRefSeq.Text = TextLib.StringDecoder(drView["REF_SEQ"].ToString());
            }

            intRowsCnt++;
        }
        
       

        protected void imgUpdateInvoice_Click(object sender, ImageClickEventArgs e)
        {
            
        }

        protected void lnkLoadData_Click(object sender, ImageClickEventArgs e)
        {
            //LoadData();
        }

        private void MakePaymentDdl(DropDownList ddlParam)
        {

            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlParam.Items.Clear();

            ddlParam.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (var dr in dtReturn.Select())
            {
                ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
         }

        private void MakeInvoiceYN()
        {
            ddlInvoiceYN.Items.Clear();
            ddlInvoiceYN.Items.Add(new ListItem("Normal", "NM"));
            ddlInvoiceYN.Items.Add(new ListItem("Cancel", "CC"));            

        }

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {

            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);
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

        protected void Aleart(string message)
        {
            var sbNoSelection = new StringBuilder();
            sbNoSelection.Append("alert('" + message + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
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

                    InvoiceMngBlo.UpdatingPrintBundleNoKNCanHoadonInfo(refSeq, refPrintBundleNo);
                    intCheckRow++;
                }
                txthfPrintBundleNo.Value = refPrintBundleNo;
                // 선택 사항이 있는지 없는지 체크
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    InvoiceMngBlo.UpdatingKNHoadonInfoCancelCC(refPrintBundleNo);
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

                    InvoiceMngBlo.UpdatingPrintBundleNoKNCanHoadonInfo(refSeq, refPrintBundleNo);
                    intCheckRow++;
                }
                txthfPrintBundleNo.Value = refPrintBundleNo;
                // 선택 사항이 있는지 없는지 체크
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    InvoiceMngBlo.UpdatingKNHoadonInfoCancelNM(refPrintBundleNo);
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

        protected void ddlInvoiceYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlInvoiceYN.SelectedValue.Equals("NM"))
                {
                    lnkbtnCancel.Visible = false;
                    lnkPrint.Visible = true;
                    LoadData();
                }
                else if (ddlInvoiceYN.SelectedValue.Equals("CC"))
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
