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
    public partial class InvoiceAptCancel : BasePage
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

            //ltSearchRoom.Text = TextNm["ROOMNO"];
            //ltSearchYear.Text = TextNm["YEARS"];
            //ltSearchMonth.Text = TextNm["MONTH"];
            //ltStartDt.Text = TextNm["FROM"];
            //ltEndDt.Text = TextNm["TO"];
            //ltFloor.Text = TextNm["FLOOR"];
            //ltTxtCd.Text = TextNm["TAXCD"];
            //ltRssNo.Text = TextNm["CERTINCORP"];
           // ltMaxNo.Text = TextNm["MAXNUMBER"];

            // DropDownList Setting
            // 년도
            // 수납 아이템
            MakePaymentDdl();

            // MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlCompNo, "00000000");

            ltDate.Text = TextNm["MONTH"];            
            ltRoomNo.Text = "RoomNo";
            //ltRoomNo.Text = TextNm["ROOMNO"];            
            ltPaymentDt.Text = "Pay Date";
            ltIssDate.Text = "Iss Date";
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];
            lnkbtnIssuing.Text = "Cancel";

            txtSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            //lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
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

            strInit = CommValue.AUTH_VALUE_EMPTY;
            intInit = CommValue.NUMBER_VALUE_0;
            var sPayDt = txtSearchDt.Text.Replace("-", "");
            // KN_USP_SET_SELECT_APT_HOADONINFO_S02
            var dtReturn = ReceiptMngBlo.SelectListAptHoadonForCancel(hfRentCd.Value, strSearchRoomNo, sPayDt,
                                                                        ddlItemCd.SelectedValue, txtInvoiceNo.Text, Session["LANGCD"].ToString());

            if (dtReturn == null) return;
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();

            if (lvPrintoutList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                //bool isAllCheck = chkAll.Checked;
                chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
                CheckAll(CommValue.AUTH_VALUE_TRUE);
            }
            

            //chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
        }


        /// <summary>
        /// 
        /// </summary>
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

            var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
            var txtHfRefSeq = (TextBox)iTem.FindControl("txtHfRefSeq");
            var txtHfInvoiceNo = (TextBox)iTem.FindControl("txtHfInvoiceNo");
            var txtHfPayDate = (TextBox)iTem.FindControl("txtHfPayDate");
            var txtHfRoomNo = (TextBox)iTem.FindControl("txtHfRoomNo");

            if (!string.IsNullOrEmpty(drView["Ref_Seq"].ToString()))
            {
                txtHfRefSeq.Text = drView["Ref_Seq"].ToString();
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
                txtHfRoomNo.Text = drView["RoomNo"].ToString();
            }

            var ltInvoiceNo = (Literal)iTem.FindControl("ltInvoiceNo");
            if (!string.IsNullOrEmpty(drView["InvoiceNo"].ToString()))
            {
                ltInvoiceNo.Text = drView["InvoiceNo"].ToString();
                txtHfInvoiceNo.Text = drView["InvoiceNo"].ToString();
            }

            var ltInsBillNm = (Literal)iTem.FindControl("ltInsBillNm");

            if (!string.IsNullOrEmpty(drView["BillNm"].ToString()))
            {
                ltInsBillNm.Text = TextLib.StringDecoder(drView["BillNm"].ToString());
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
                hfPayDt.Value = txtInsRegDt.Text.Replace("-", "");
            }

            var txtInsPayDate = (TextBox)iTem.FindControl("txtInsPayDate");

            if (!string.IsNullOrEmpty(drView["PayDate"].ToString()))
            {
                txtInsPayDate.Text = TextLib.MakeDateEightDigit(drView["PayDate"].ToString());
                txtHfPayDate.Text = drView["PayDate"].ToString();
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
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
        //private void CheckAll(bool isAllCheck)
        //{
        //    for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
        //    {
        //        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
        //        {
        //            ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
        //        }
        //    }
        //}

        /// <summary>
        /// 리스트 각 행별 체크 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string strInvoiceNo = string.Empty;

        //        int intTotalCount = CommValue.NUMBER_VALUE_0;
        //        int intCheckCount = CommValue.NUMBER_VALUE_0;

        //        for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
        //        {
        //            if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
        //            {
        //                intTotalCount++;

        //                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
        //                {
        //                    strInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;
        //                    intCheckCount++;
        //                }
        //                else
        //                {
        //                    string strOtherInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;

        //                    if (strOtherInvoiceNo != "" && strInvoiceNo.Equals(strOtherInvoiceNo))
        //                    {
        //                        ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_TRUE;
        //                    }
        //                }
        //            }

        //            TextBox txtNewDt = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInsRegDt"));
        //            HiddenField hfNewDt = ((HiddenField)lvPrintoutList.Items[intTmpI].FindControl("hfInsRegDt"));
        //            txtNewDt.Text = TextLib.MakeDateEightDigit(hfNewDt.Value.Replace("-", ""));
        //        }

        //        if (intTotalCount == intCheckCount)
        //        {
        //          //  chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
        //        }
        //        else
        //        {
        //           // chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrLogger.MakeLogger(ex);
        //    }
        //}

        /// <summary>
        /// 출력버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            // Initializing Ref_PrintNo on HoaDon table
            InvoiceMngBlo.UpdatingRefPrintNoHoaDonAPTForReset();

            try
            {
                var intCheckRow = CommValue.NUMBER_VALUE_0;
                var refPrintNo = string.Empty;
                var invoiceNo = string.Empty;
                var roomNo = string.Empty;
                var payDate = string.Empty;

                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }

                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;

                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;  

                    if (string.IsNullOrEmpty(invoiceNo))
                    {
                        invoiceNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfInvoiceNo")).Text;
                    }

                    if (string.IsNullOrEmpty(roomNo))
                    {
                        roomNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRoomNo")).Text;
                    }

                    if (string.IsNullOrEmpty(payDate))
                    {
                        payDate = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfPayDate")).Text;
                    }

                    if (string.IsNullOrEmpty(refPrintNo))
                    {
                        refPrintNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    }
                    //Update printNo foreach row on HoadonAPT table
                    InvoiceMngBlo.UpdatingRefPrintNoForHoaDonAPT(refSeq, refPrintNo);
                    intCheckRow++;
                }
                hfsendParam.Value = refPrintNo;

                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    InvoiceMngBlo.UpdatingHoadonInfoAPTNewCC(refPrintNo);
                    InvoiceMngBlo.UpdatingCancelPaymentListAPTNew(invoiceNo, roomNo, payDate);
                    LoadData();
                }
                else
                {
                    // 화면 초기화
                    Aleart(AlertNm["INFO_HAS_NO_SELECTED_ITEM"]);
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

        protected void ddlStartFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //InitFloorDdl(ddlEndFloor, Int32.Parse(ddlStartFloor.SelectedValue));
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void chkAll_CheckedChanged1(object sender, EventArgs e)
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
            //string conmpareVal1 = lvPrintoutList.Items[e].FindControl("ltInsRoomNo").ToString();

            CheckBox cb = (CheckBox)sender;
            ListViewItem item = (ListViewItem)cb.NamingContainer;
            ListViewDataItem dataItem = (ListViewDataItem)item;

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
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();
            //KN_USP_UPDATE_INVOICENO_HOADONINFOAPT_U00
            InvoiceMngBlo.UpdatingInvoiceNoHoadonInfoAptNew(reft, insCompCd, insMemNo, insMemIP);
            LoadData();
        }


        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            hfUserSeq.Value = ((TextBox)e.Item.FindControl("txtHfUserSeq")).Text;
            hfPayDt.Value = ((TextBox)e.Item.FindControl("txtInsRegDt")).Text;
        }

        protected void Aleart(string message)
        {
            var sbNoSelection = new StringBuilder();
            sbNoSelection.Append("alert('" + message + "');");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);
        }
    }
}