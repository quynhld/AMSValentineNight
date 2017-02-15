using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Config.Biz;
using KN.Manage.Biz;
using KN.Settlement.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace KN.Web.Management.Manage
{
    public partial class MergeInvoiceAdjustAPT : BasePage
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

            txtSearchDt.Text = DateTime.Now.ToString("s").Substring(0, 7);

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

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

            var period = txtPeriod.Text.Replace("-", "");
            var paidDt = txtSearchDt.Text.Replace("-", "");
            var feeTyDt = "";
            var feeTy = "";

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
            // KN_USP_MNG_SELECT_PAYMENTINFO_APT_S02
            var dtReturn = MngPaymentBlo.ListPaymentInfoAptForAdjust(hfRentCd.Value, feeTy,feeTyDt, period,
                                                                        txtRoomNo.Text.Trim(), paidDt);

            if (dtReturn != null)
            {
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();

                chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
            }

            //// 세금계산서 최대값 조회
            var dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNoForAPT();

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
        }


        /// <summary>
        /// 
        /// </summary>
        private void MakePaymentDdl()
        {
            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

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
                var sbWarning = new StringBuilder();
                sbWarning.Append("CloseLoading();");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
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
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox)lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;

                    var refSeq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    var seq = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfPrintDetSeq")).Text;
                    var PayDt = ((TextBox)lvPrintoutList.Items[i].FindControl("txtInsRegDt")).Text.Replace("-", "");



                    if (string.IsNullOrEmpty(refPrintNo))
                    {
                        refPrintNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSeq")).Text;
                    }
                    InvoiceMngBlo.UpdatingRefPrintNoForAPTNew(refSeq, hfRentCd.Value, refPrintNo, PayDt, Int32.Parse(seq));
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

        protected void imgUpdateInvoice_Click(object sender, ImageClickEventArgs e)
        {
            var reft = hfsendParam.Value;
            if (string.IsNullOrEmpty(reft))
            {
                return;
            }
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();
            //KN_USP_INSERT_INVOICE_ADJUST_I00
            InvoiceMngBlo.InsertAdjustInvoiceHoadonInfoApt(reft, insCompCd, insMemNo, insMemIP);   
        }


        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            hfUserSeq.Value = ((TextBox)e.Item.FindControl("txtHfUserSeq")).Text;
            hfPayDt.Value = ((TextBox)e.Item.FindControl("txtInsRegDt")).Text;
        }

        protected void imgbtnLoadData_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }

        protected void lnkExportExcel_Click(object sender, EventArgs e)
        {
            var period = txtPeriod.Text.Replace("-", "");
            var paidDt = txtSearchDt.Text.Replace("-", "");
            var feeTyDt = "";
            var feeTy = "";

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
            // KN_USP_MNG_SELECT_PAYMENTINFO_APT_S02
            var dtReturn = MngPaymentBlo.ListPaymentInfoAptForAdjustExcel(hfRentCd.Value, feeTy, feeTyDt, period,
                                                                        txtRoomNo.Text.Trim(), paidDt,txtInvoice.Text.Trim());
            GenerateExcel(dtReturn.Tables[0], "Adjustment Invoice", "Adjustment Invoice - " + ddlItemCd.SelectedItem+"-"+txtPeriod.Text);
        }

        public void GenerateExcel(DataTable tbl, string excelSheetName, string fileName)
        {
            using (var pck = new ExcelPackage())
            {
                //Create the worksheet
                var ws = pck.Workbook.Worksheets.Add(excelSheetName);

                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(tbl, true, TableStyles.None);
                var tbls = ws.Tables[0];

                tbls.ShowTotal = true;
                //Set Sum 
                //tbls.Columns[11].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[10].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[9].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:H1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:H1"].AutoFitColumns();
                //using (var col = ws.Cells[2, 10, 2 + tbl.Rows.Count, 12])
                //{
                //    col.Style.Numberformat.Format = "#,##0";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}

                //using (var col = ws.Cells[2, 5, 2 + tbl.Rows.Count, 5])
                //{
                //    col.Style.Numberformat.Format = "dd/MM/yyyy";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                //}
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
        }
    }
}
