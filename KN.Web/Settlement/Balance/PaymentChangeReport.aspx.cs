using System;
using System.Data;
using System.IO;
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

using KN.Resident.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class PaymentChangeReport : BasePage
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

            // DropDownList Setting
            // 년도
            // 수납 아이템
            MakePaymentDdl();

            ltDate.Text = TextNm["MONTH"];
            //ltInvoiceNo.Text = "Invoice NO.";
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltPaymentTy.Text = TextNm["ITEM"];
            ltPaymentDt.Text = TextNm["REGISTDATE"];
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];

            txtSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 10);

            //lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            //lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            //lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";
            CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaidCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_PAYMENT);

            lnkbtnIssuing.Visible = Master.isWriteAuthOk;
        }

        protected void LoadData()
        {
            string strSearchRoomNo = string.Empty;

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
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();

                chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        private void MakePaymentDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItemCd.Items.Clear();

            ddlItemCd.Items.Add(new ListItem("All Fee", ""));

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

                //if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                //{
                //    TextBox txtHfInsNm = (TextBox)iTem.FindControl("txtHfInsNm");
                //    txtHfInsNm.Text = TextLib.StringDecoder(drView["UserNm"].ToString());
                //}

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

            var feeTyDt = "";
            var feeTy = "";
            try
            {
                var paydt = txtSearchDt.Text.Trim().Replace("-","");
                var paydtE = txtSearchDtE.Text.Trim().Replace("-", "");               
                var roomNo = txtRoomNo.Text.Trim();
                var userNm = txtCompanyNm.Text.Trim();
                var paidCd = ddlPaidCd.SelectedValue;               

                if ( String.IsNullOrEmpty(ddlItemCd.SelectedValue))
                {
                    feeTy = "";
                    feeTyDt = "";

                }
                else if (ddlItemCd.SelectedValue.Equals("0001"))
                {
                    feeTy = ddlItemCd.SelectedValue;
                    feeTyDt = "";
                }
                else if (ddlItemCd.SelectedValue.Equals("0008"))
                {
                    feeTy = "0011";
                    feeTyDt = "0001";
                }
                else if (ddlItemCd.SelectedValue.Equals("0009"))
                {
                    feeTy = "0011";
                    feeTyDt = "0002";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + feeTy + "','" + feeTyDt +  "','" + paydt + "','" + paydtE + "','" + roomNo + "','" + userNm + "','" + paidCd  + "');", CommValue.AUTH_VALUE_TRUE);
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


        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            hfUserSeq.Value = ((TextBox)e.Item.FindControl("txtHfUserSeq")).Text;
            hfPayDt.Value = ((TextBox)e.Item.FindControl("txtInsRegDt")).Text;
        }

        protected void llnkExportExcel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var feeTyDt = "";
                var feeTy = "";
       
                    var paydt = txtSearchDt.Text.Trim().Replace("-", "");
                    var paydtE = txtSearchDtE.Text.Trim().Replace("-", "");                    
                    var roomNo = txtRoomNo.Text.Trim();
                    var userNm = txtCompanyNm.Text.Trim();                    

                    if (String.IsNullOrEmpty(ddlItemCd.SelectedValue))
                    {
                        feeTy = "";
                        feeTyDt = "";

                    }
                    else if (ddlItemCd.SelectedValue.Equals("0001"))
                    {
                        feeTy = ddlItemCd.SelectedValue;
                        feeTyDt = "";
                    }
                    else if (ddlItemCd.SelectedValue.Equals("0008"))
                    {
                        feeTy = "0011";
                        feeTyDt = "0001";
                    }
                    else if (ddlItemCd.SelectedValue.Equals("0009"))
                    {
                        feeTy = "0011";
                        feeTyDt = "0002";
                    }

                   // var dtReturn = ContractMngBlo.SpreadTenantBalanceInvoiceExcelView(feeTy, feeTyDt, period, periodE, paydt, paydtE, roomNo, userNm, ddlPaidCd.SelectedValue, invoiceNo);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW).Replace("+", " ") + ddlItemCd.SelectedItem +".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = Encoding.Unicode;
                Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                this.EnableViewState = false;

                var stringWriter = new StringWriter();
                var htmlTextWriter = new HtmlTextWriter(stringWriter);

                var strTitle = "<p align='center'><font size='4'><b>" + Master.TITLE_NOW +"-"+ddlItemCd.SelectedItem+ "</b></font></p>";

                htmlTextWriter.Write(strTitle);

                var gv = new GridView();
                gv.Font.Name = "Tahoma";
                //gv.DataSource = dtReturn;
                gv.DataBind();
                gv.RenderControl(htmlTextWriter);

                Response.Write(stringWriter.ToString());
                Response.Flush();

                // Prevents any other content from being sent to the browser
                Response.SuppressContent = true;

                // Directs the thread to finish, bypassing additional processing
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                stringWriter.Flush();
                stringWriter.Close();
                htmlTextWriter.Flush();
                htmlTextWriter.Close();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}