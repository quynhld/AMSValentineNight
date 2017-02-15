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
    public partial class HoadonReprintOut: BasePage
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
            ltSearchRoom.Text = TextNm["ROOMNO"];
            ltSearchYear.Text = TextNm["YEARS"];
            ltSearchMonth.Text = TextNm["MONTH"];
            ltStartDt.Text = TextNm["FROM"];
            ltEndDt.Text = TextNm["TO"];
            ltTxtCd.Text = TextNm["TAXCD"];
            ltRssNo.Text = TextNm["CERTINCORP"];

            // DropDownList Setting
            // 년도
            MakeYearDdl();

            // 월
            MakeMonthDdl();

            // 수납 아이템
            MakePaymentDdl();

            ltDate.Text = TextNm["MONTH"];
            ltInvoiceNo.Text = "Invoice NO.";
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltCount.Text = TextNm["COUNT"];
            ltPaymentDt.Text = TextNm["REGISTDATE"];
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];
            //lnkbtnIssuing.Text = TextNm["ISSUING"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
        }

        protected void LoadData()
        {
            string strSearchNm = string.Empty;
            string strSearchRoomNo = string.Empty;

            if (!string.IsNullOrEmpty(txtSearchRoom.Text))
            {
                strSearchRoomNo = txtSearchRoom.Text;
            }

            strInit = CommValue.AUTH_VALUE_EMPTY;
            intInit = CommValue.NUMBER_VALUE_0;

            // KN_USP_SET_SELECT_INVOICEFORCONFIRM_S00
            DataTable dtReturn = InvoiceMngBlo.SelectInvoiceReprintList(hfRentCd.Value, txtSearchRoom.Text, ddlYear.SelectedValue, ddlMonth.SelectedValue,
                                                                        hfStartDt.Value, hfEndDt.Value, ddlItemCd.SelectedValue, txtUserTaxCd.Text,
                                                                        txtRssNo.Text, Session["LANGCD"].ToString());

            if (dtReturn != null)
            {
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();
            }
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();

            ddlYear.Items.Add(new ListItem(TextNm["YEARS"], string.Empty));

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.AddYears(1).Year; intTmpI++)
            {
                ddlYear.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }

            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl()
        {
            ddlMonth.Items.Clear();

            ddlMonth.Items.Add(new ListItem(TextNm["MONTH"], string.Empty));

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
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
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                //HtmlTableCell tdChk = (HtmlTableCell)iTem.FindControl("tdChk");
                //CheckBox chkboxList = (CheckBox)iTem.FindControl("chkboxList");

                TextBox txtHfPrintSeq = (TextBox)iTem.FindControl("txtHfPrintSeq");
                TextBox txtHfPrintDetSeq = (TextBox)iTem.FindControl("txtHfPrintDetSeq");
                TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");

                Literal ltInvoiceNo = (Literal)iTem.FindControl("ltInvoiceNo");
                TextBox txtHfInvoiceNo = (TextBox)iTem.FindControl("txtHfInvoiceNo");

                ImageButton imgbtnPrint = (ImageButton)iTem.FindControl("imgbtnPrint");
                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");

                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_PRCEED_WORK"] + "');";

                if (!string.IsNullOrEmpty(drView["CancelYn"].ToString()))
                {
                    if (drView["CancelYn"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        imgbtnPrint.Visible = CommValue.AUTH_VALUE_FALSE;
                        imgbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        imgbtnPrint.Visible = CommValue.AUTH_VALUE_TRUE;
                        imgbtnDelete.Visible = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                if (!string.IsNullOrEmpty(drView["NewInvoiceNo"].ToString()))
                {
                    if (!drView["NewInvoiceNo"].ToString().Equals(strInit))
                    {
                        strInit = drView["NewInvoiceNo"].ToString();
                        intInit = CommValue.NUMBER_VALUE_0;
                        ltInvoiceNo.Text = drView["NewInvoiceNo"].ToString();
                        txtHfInvoiceNo.Text = drView["NewInvoiceNo"].ToString();
                    }
                    else
                    {
                        ltInvoiceNo.Text = drView["NewInvoiceNo"].ToString();
                        txtHfInvoiceNo.Text = drView["NewInvoiceNo"].ToString();
                        ltInvoiceNo.Visible = CommValue.AUTH_VALUE_FALSE;
                        imgbtnPrint.Visible = CommValue.AUTH_VALUE_FALSE;
                        imgbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;

                        if (intRowsCnt > CommValue.NUMBER_VALUE_0)
                        {
                            intInit = intInit + 1;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    txtHfPrintSeq.Text = drView["PrintSeq"].ToString();
                    //txtHfPrintDetSeq.Text = drView["PrintDetSeq"].ToString();
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DataYear"].ToString()))
                {
                    Literal ltInsYear = (Literal)iTem.FindControl("ltInsYear");
                    ltInsYear.Text = drView["DataYear"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DataMonth"].ToString()))
                {
                    Literal ltInsMonth = (Literal)iTem.FindControl("ltInsMonth");
                    ltInsMonth.Text = drView["DataMonth"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDt"].ToString()))
                {
                    Literal ltInsRegDt = (Literal)iTem.FindControl("ltInsRegDt");
                    ltInsRegDt.Text = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["InsDt"].ToString()));
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                    ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }
                
                TextBox txtHfBillCd = (TextBox)iTem.FindControl("txtHfBillCd");

                if (!string.IsNullOrEmpty(drView["BillCd"].ToString()))
                {
                    txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
                }

                Literal ltInsCount = (Literal)iTem.FindControl("ltInsCount");

                if (!string.IsNullOrEmpty(drView["PrintoutCnt"].ToString()))
                {
                    ltInsCount.Text = TextLib.StringDecoder(drView["PrintoutCnt"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Description"].ToString()))
                {
                    Literal ltInsDescription = (Literal)iTem.FindControl("ltInsDescription");
                    ltInsDescription.Text = TextLib.StringDecoder(drView["Description"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["AmtViNo"].ToString()))
                {
                    Literal ltInsAmtViNo = (Literal)iTem.FindControl("ltInsAmtViNo");
                    ltInsAmtViNo.Text = TextLib.MakeVietIntNo(double.Parse(drView["AmtViNo"].ToString()).ToString("###,##0"));
                }

                TextBox txtNewInvoiceNo = (TextBox)iTem.FindControl("txtNewInvoiceNo");
                Literal ltNewInvoiceNo = (Literal)iTem.FindControl("ltNewInvoiceNo");
                ImageButton imgbtnConfirm = (ImageButton)iTem.FindControl("imgbtnConfirm");

                if (!string.IsNullOrEmpty(drView["ReInvoiceNo"].ToString()))
                {
                    txtNewInvoiceNo.Text = drView["ReInvoiceNo"].ToString();
                    ltNewInvoiceNo.Text = drView["ReInvoiceNo"].ToString();

                    txtNewInvoiceNo.Visible = CommValue.AUTH_VALUE_FALSE;
                    imgbtnConfirm.Visible = CommValue.AUTH_VALUE_FALSE;
                }
                else
                {
                    txtNewInvoiceNo.Visible = CommValue.AUTH_VALUE_TRUE;
                    imgbtnConfirm.Visible = CommValue.AUTH_VALUE_TRUE;
                }

                intRowsCnt++;
            }
        }

        protected void lvPrintoutList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Literal ltInvoiceNo = (Literal)lvPrintoutList.Items[e.ItemIndex].FindControl("ltInvoiceNo");

            StringBuilder sbPrintOut = new StringBuilder();

            sbPrintOut.Append("window.open(\"/Common/RdPopup/RDPopupHoadonKNConfirm.aspx?Datum0=" + ltInvoiceNo.Text + "\", \"Reprint\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Reprint", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
        }

        protected void lvPrintoutList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            Literal ltInvoiceNo = (Literal)lvPrintoutList.Items[e.ItemIndex].FindControl("ltInvoiceNo");
            TextBox txtNewInvoiceNo = (TextBox)lvPrintoutList.Items[e.ItemIndex].FindControl("txtNewInvoiceNo");

            // KN_USP_SET_UPDATE_INVOICEFORTEMP_M01
            object[] objReturn = InvoiceMngBlo.UpdateInvoiceConfirm(ltInvoiceNo.Text, txtNewInvoiceNo.Text);

            LoadData();
        }

        protected void lvPrintoutList_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            Literal ltInvoiceNo = (Literal)lvPrintoutList.Items[e.NewEditIndex].FindControl("ltInvoiceNo");
            TextBox txtNewInvoiceNo = (TextBox)lvPrintoutList.Items[e.NewEditIndex].FindControl("txtNewInvoiceNo");

            if (string.IsNullOrEmpty(txtNewInvoiceNo.Text))
            {
                StringBuilder sbNoSelection = new StringBuilder();

                sbNoSelection.Append("alert('" + AlertNm["ALERT_INSERT_SEVENDIGIT"] + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeSeven", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);

                txtNewInvoiceNo.Text = "";
            }
            else
            {
                // KN_USP_SET_UPDATE_INVOICEFORTEMP_M01
                object[] objReturn = InvoiceMngBlo.UpdateInvoiceConfirm(ltInvoiceNo.Text, txtNewInvoiceNo.Text);

                LoadData();
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

        protected void txtNewInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((TextBox)(sender)).Text.Length != 7)
                {
                    StringBuilder sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("alert('" + AlertNm["ALERT_INSERT_SEVENDIGIT"] + "');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeSeven", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE);

                    ((TextBox)(sender)).Text = "";
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();               

                // KN_USP_RES_SELECT_SALESINFO_S02
                DataTable dtSource = new DataTable();

                strInit = CommValue.AUTH_VALUE_EMPTY;
                intInit = CommValue.NUMBER_VALUE_0;

                // KN_USP_SET_SELECT_INVOICEFORCONFIRM_S06
                dtSource = InvoiceMngBlo.SelectInvoiceReprintExcel(hfRentCd.Value, txtSearchRoom.Text, ddlYear.SelectedValue, ddlMonth.SelectedValue,
                                                                            hfStartDt.Value, hfEndDt.Value, ddlItemCd.SelectedValue, txtUserTaxCd.Text,
                                                                            txtRssNo.Text, Session["LANGCD"].ToString());
                //GenerateExcel(dtReturn);
                //return;
                if (dtSource.Rows.Count <= 0)
                {
                    return;
                }
                HttpContext.Current.Response.Clear();
                //HttpContext.Current.Response.Headers.Clear();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW).Replace("+", " ") + ".xls");
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                //Response.Charset = "UTF-8";
                HttpContext.Current.Response.ContentEncoding = Encoding.Unicode;
                HttpContext.Current.Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                EnableViewState = false;

                //StringWriter stringWriter = new StringWriter();
                //HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);

                var sbDocBody = new StringBuilder(); ;
                // Declare Styles
                sbDocBody.Append("<style>");
                sbDocBody.Append(".Header {  background-color:Navy; color:#ffffff; font-weight:bold;font-family:Verdana; font-size:12px;border-collapse: collapse;border-color: gray;border: solid 1px;}");
                sbDocBody.Append(".SectionHeader { background-color:#8080aa; color:#ffffff; font-family:Verdana; font-size:12px;font-weight:bold;}");
                sbDocBody.Append(".Content { background-color:#ccccff; color:#000000; font-family:Verdana; font-size:12px;text-align:left;border-collapse: collapse;border-color: gray;border: solid 1px;}");
                sbDocBody.Append(".Label { background-color:#ccccee; color:#000000; font-family:Verdana; font-size:12px; text-align:right;}");
                sbDocBody.Append("</style>");
                //
                StringBuilder sbContent = new StringBuilder(); ;
                sbDocBody.Append("<br><table align=\"center\"  style=\"background-color:#000000;\">");
                sbDocBody.Append("<tr><td width=\"500\">");
                sbDocBody.Append("<table width=\"100%\" style=\"background-color:#ffffff;\">");
                //
                if (dtSource.Rows.Count > 0)
                {
                    sbDocBody.Append("<tr><td>");
                    sbDocBody.Append("<table width=\"600\" cellpadding=\"0\" cellspacing=\"2\"><tr><td>");
                    //
                    // Add Column Headers
                    sbDocBody.Append("<tr><td width=\"25\"> </td></tr>");
                    sbDocBody.Append("<tr>");
                    sbDocBody.Append("<td> </td>");
                    for (int i = 0; i < dtSource.Columns.Count; i++)
                    {
                        sbDocBody.Append("<td class=\"Header\" width=\"120\">" + dtSource.Columns[i].ToString().Replace(".", "<br>") + "</td>");
                    }
                    sbDocBody.Append("</tr>");
                    //
                    // Add Data Rows
                    for (int i = 0; i < dtSource.Rows.Count; i++)
                    {
                        sbDocBody.Append("<tr>");
                        sbDocBody.Append("<td> </td>");
                        for (int j = 0; j < dtSource.Columns.Count; j++)
                        {
                            sbDocBody.Append("<td class=\"Content\">" + dtSource.Rows[i][j] + "</td>");
                        }
                        sbDocBody.Append("</tr>");
                    }
                    sbDocBody.Append("</table>");
                    sbDocBody.Append("</td></tr></table>");
                    sbDocBody.Append("</td></tr></table>");
                }

                //htmlTextWriter.Write(strTitle);

               // GridView gv = new GridView();

               // gv.DataSource = dtReturn;
               // gv.Font.Name = "Tahoma";
               // gv.DataBind();
               // gv.RenderControl(htmlTextWriter);

                HttpContext.Current.Response.Write(sbDocBody.ToString());
                HttpContext.Current.Response.Flush();

                // Prevents any other content from being sent to the browser
                HttpContext.Current.Response.SuppressContent = true;

                // Directs the thread to finish, bypassing additional processing
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                //sbDocBody.Flush();
                //sbDocBody.Close();
                //htmlTextWriter.Flush();
                //htmlTextWriter.Close();
                //Response.End();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

            private void ExporttoExcel(DataTable table)
            {
                    HttpContext.Current.Response.Write("<Td>");

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.ContentType = "application/ms-excel";
                    HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                   HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=Reports.xls");
          
                    HttpContext.Current.Response.Charset = "utf-8";
                    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
                      //sets font
                    HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
                    HttpContext.Current.Response.Write("<BR><BR><BR>");
                    HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' borderColor='#000000' cellSpacing='0' cellPadding='0' style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR>");
                    var columnscount = table.Columns.Count;
 
                    for (int j = 0; j < columnscount; j++)
                    {
                    //Makes headers bold
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(table.Columns[j].ToString());
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
                foreach (DataRow row in table.Rows)
                {
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }
                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table>");
                HttpContext.Current.Response.Write("</font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }      

    }
}