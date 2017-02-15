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
    public partial class HoaDonPrintOutForAPTParking : BasePage
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
            ltMaxNo.Text = "Max Number";
            txtPrintDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            hfprintDate.Value = txtPrintDt.Text.Replace("-", "").Replace(".", "");
            // DropDownList Setting
            // 년도
            // 수납 아이템
            MakePaymentDdl();

            MakeInvoiceYN();

            // MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlCompNo, "00000000");

            ltDate.Text = TextNm["MONTH"];
            //ltInvoiceNo.Text = "Invoice NO.";
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltPaymentTy.Text = TextNm["ITEM"];
            ltPaymentDt.Text = TextNm["REGISTDATE"];
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];
            lnkbtnIssuing.Text = TextNm["ISSUING"];

            hfRentCd.Value = "0004";

            //txtSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);
            //txtESearchDt.Text = DateTime.Now.ToString("s").Substring(0, 7);

            //InitFloorDdl(ddlStartFloor, CommValue.NUMBER_VALUE_0);
            //InitFloorDdl(ddlEndFloor, CommValue.NUMBER_VALUE_6);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            //lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";

            //chkAll.Enabled = CommValue.AUTH_VALUE_FALSE;

            // 세금계산서 최대값 조회
            //DataTable dtReturn = new DataTable();

            //if (ddlCompNo.SelectedValue.Equals(CommValue.MAIN_COMP_CD))
            //{
            //    dtReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();
            //}
            //else
            //{
            //    dtReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);
            //}

            
            //DataTable dtReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();

            //if (dtReturn != null)
            //{
            //    if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
            //    {
            //        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MaxInvoiceNo"].ToString()))
            //        {
            //            strMaxInvoiceNo = dtReturn.Rows[0]["MaxInvoiceNo"].ToString().PadLeft(7, '0');
            //        }
            //    }
            //}

            //ltInsMaxNo.Text = strMaxInvoiceNo;

            //KN_USP_SET_SELECT_NEXTINVOICE_NO
            DataTable dtReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);

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
            // KN_USP_SET_SELECT_APT_PKFEE_S00
            var dtReturn = ReceiptMngBlo.SelectPrintListHoadonAptPKF(hfRentCd.Value, txtRoomNo.Text, sPayDt,
                                                                        ddlItemCd.SelectedValue, ddlInvoiceYN.SelectedValue.ToString(), Session["LANGCD"].ToString());

            if (dtReturn != null)
            {
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();

                chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
            }

            //// 세금계산서 최대값 조회
            // DataTable dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);

            //if (ddlCompNo.SelectedValue.Equals(CommValue.MAIN_COMP_CD))
            //{
            var dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);
            //var dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();
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
            //hfPayDt.Value = ((TextBox)lvPrintoutList.Items[0].FindControl("txtInsRegDt")).Text.Replace("-", "");
            //hfUserSeq.Value = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfUserSeq")).Text;
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
                if (dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_PARKINGFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_PARKINGCARDFEE))
                {
                    ddlItemCd.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
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
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {

                var txtHfPrintSeq = (TextBox)iTem.FindControl("txtHfPrintSeq");
                var txtHfPrintDetSeq = (TextBox)iTem.FindControl("txtHfPrintDetSeq");
                var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");


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

                //if (!string.IsNullOrEmpty(drView["PrintSeq"].ToString()))
                //{
                //    txtHfPrintSeq.Text = drView["PrintSeq"].ToString();
                //}

                //if (!string.IsNullOrEmpty(drView["PrintDetSeq"].ToString()))
                //{
                //    txtHfPrintDetSeq.Text = drView["PrintDetSeq"].ToString();
                //}

                if (!string.IsNullOrEmpty(drView["DataYear"].ToString()))
                {
                    var ltPeriod = (Literal)iTem.FindControl("ltPeriod");
                    ltPeriod.Text = drView["DataYear"].ToString();
                }

                //if (!string.IsNullOrEmpty(drView["DataMonth"].ToString()))
                //{
                //    Literal ltInsMonth = (Literal)iTem.FindControl("ltInsMonth");
                //    ltInsMonth.Text = drView["DataMonth"].ToString();
                //}

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
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
                hfBillCd.Value = txtHfBillCd.Text;

                if (!string.IsNullOrEmpty(drView["Description"].ToString()))
                {
                    TextBox txtInsDescription = (TextBox)iTem.FindControl("txtInsDescription");
                    txtInsDescription.Text = TextLib.StringDecoder(drView["Description"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Amount"].ToString()))
                {
                    TextBox txtInsAmtViNo = (TextBox)iTem.FindControl("txtInsAmtViNo");
                    txtInsAmtViNo.Text = TextLib.MakeVietIntNo(double.Parse(drView["Amount"].ToString()).ToString("###,##0"));
                    txtInsAmtViNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                }
                TextBox txtInsRegDt = (TextBox)iTem.FindControl("txtInsRegDt");

                if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    txtInsRegDt.Text = TextLib.MakeDateEightDigit(drView["PaymentDt"].ToString());
                    hfPayDt.Value = txtInsRegDt.Text.Replace("-", "");
                }


                //TextBox txtHfInsTaxCd = (TextBox)iTem.FindControl("txtHfInsTaxCd");

                //if (!string.IsNullOrEmpty(drView["UserTaxCd"].ToString()))
                //{
                //    txtHfInsTaxCd.Text = TextLib.StringDecoder(drView["UserTaxCd"].ToString());
                //}

                TextBox txtHfPaymentDt = (TextBox)iTem.FindControl("txtHfPaymentDt");
                TextBox txtHfPaymentSeq = (TextBox)iTem.FindControl("txtHfPaymentSeq");
                TextBox txtHfPaymentDetSeq = (TextBox)iTem.FindControl("txtHfPaymentDetSeq");

                //if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                //{
                //    txtHfPaymentDt.Text = TextLib.StringDecoder(drView["PaymentDt"].ToString());
                //    txtHfPaymentSeq.Text = TextLib.StringDecoder(drView["PaymentSeq"].ToString());
                //    txtHfPaymentDetSeq.Text = TextLib.StringDecoder(drView["PaymentDetSeq"].ToString());
                //}

                //TextBox txtHfClassCd = (TextBox)iTem.FindControl("txtHfClassCd");

                //if (!string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                //{
                //    txtHfClassCd.Text = TextLib.StringDecoder(drView["ClassCd"].ToString());
                //}

                if (!string.IsNullOrEmpty(drView["seq"].ToString()))
                {
                    TextBox txtHfSeq = (TextBox)iTem.FindControl("txtHfSeq");
                    txtHfSeq.Text = TextLib.StringDecoder(drView["seq"].ToString());
                }

                intRowsCnt++;
            }
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
           
            // KN_SCR_UPDATE_RESET_REFPRINT_APT_PARKING_U00
            InvoiceMngBlo.UpdatingRefPrintNoAPTParkingForReset();

            try
            {
                var paydt = "";
                var strUserSeq = "";



                var intCheckRow = CommValue.NUMBER_VALUE_0;

                var refPrintNo = string.Empty;

                var billCd = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfBillCd")).Text;


                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }

                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    //if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;

                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    var PayDt = ((TextBox)lvPrintoutList.Items[i].FindControl("txtInsRegDt")).Text.Replace("-", "");
                    var strSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfSeq")).Text;

                    int intSeq = Int32.Parse(strSeq);


                    if (string.IsNullOrEmpty(refPrintNo))
                    {
                        refPrintNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    }

                    //KN_USP_UPDATE_HoaDonParkingAPT_REFPRINT_U00
                    InvoiceMngBlo.UpdatingRefPrintNoForHoaDonParkingAPT(refSeq, hfRentCd.Value, refPrintNo, PayDt, intSeq);
                    intCheckRow++;
                }
                hfsendParam.Value = refPrintNo;

                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + refPrintNo + "','" + billCd + "','" + paydt + "');", CommValue.AUTH_VALUE_TRUE);
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

        private void MakeInvoiceYN()
        {
            ddlInvoiceYN.Items.Clear();
            ddlInvoiceYN.Items.Add(new ListItem("No", "N"));
            ddlInvoiceYN.Items.Add(new ListItem("Yes", "Y"));
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

       

        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            hfUserSeq.Value = ((TextBox)e.Item.FindControl("txtHfUserSeq")).Text;
            hfPayDt.Value = ((TextBox)e.Item.FindControl("txtInsRegDt")).Text;
        }

        protected void ddlItemCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void lnkbtnMerge_Click(object sender, EventArgs e)
        {

            // KN_SCR_UPDATE_RESET_REFPRINT_APT_PARKING_U00
            InvoiceMngBlo.UpdatingRefPrintNoAPTParkingForReset();            

            try
            {
                var paydt = "";
                var strUserSeq = "";



                var intCheckRow = CommValue.NUMBER_VALUE_0;

                var refPrintNo = string.Empty;

                var billCd = ((TextBox)lvPrintoutList.Items[0].FindControl("txtHfBillCd")).Text;


                if (lvPrintoutList.Items.Count <= 0)
                {
                    return;
                }

                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    //if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;

                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    var PayDt = ((TextBox)lvPrintoutList.Items[i].FindControl("txtInsRegDt")).Text.Replace("-", "");
                    var strSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfSeq")).Text;

                    int intSeq = Int32.Parse(strSeq);


                    if (string.IsNullOrEmpty(refPrintNo))
                    {
                        refPrintNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    }
                    //KN_USP_UPDATE_APTPKF_PRINT_U00
                    InvoiceMngBlo.UpdatingMergeAptParkingFee(refSeq, hfRentCd.Value, refPrintNo, PayDt);
                    intCheckRow++;
                }
                hfsendParam1.Value = refPrintNo;

                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "LoadPopupMerge('" + refPrintNo + "');", CommValue.AUTH_VALUE_TRUE);
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

        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfsendParam.Value;
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();

            hfprintDate.Value = txtPrintDt.Text.Replace("-", "").Replace(".", "");
            var strPrintDt = hfprintDate.Value;

            //KN_USP_UPDATE_HOADON_APTPKF_TRAN_U00
            InvoiceMngBlo.UpdatingInvoiceNoHoadonAptPKF_Trans(reft, insCompCd, insMemNo, insMemIP, strPrintDt);
            LoadData();
        }

        protected void imgbtnDetailview1_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfsendParam1.Value;
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();

            hfprintDate.Value = txtPrintDt.Text.Replace("-", "").Replace(".", "");
            var strPrintDt = hfprintDate.Value;

            //KN_USP_INSERT_INVOICE_APTPKF_MERGE_I00
            InvoiceMngBlo.InsertMergeInvoiceAptPKF(reft, insCompCd, insMemNo, insMemIP, strPrintDt);
            LoadData();
        }

        protected void ddlInvoiceYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void txtPrintDt_TextChanged(object sender, EventArgs e)
        {
            hfprintDate.Value = txtPrintDt.Text.Replace("-", "").Replace(".", "");
        }

        protected void lvPrintoutList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

       
    }
}