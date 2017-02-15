using System;
using System.Data;
using System.Drawing;
using System.IO;
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
using KN.Resident.Biz;
using KN.Settlement.Biz;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace KN.Web.Settlement.Balance
{
    public partial class HoadonPrintOut : BasePage
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
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
                {
                    hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            var strMaxInvoiceNo = "0000000";
            
            ltSearchRoom.Text = TextNm["ROOMNO"];
            ltEndDt.Text = TextNm["TO"];
            ltMaxNo.Text = TextNm["MAXNUMBER"];

            // 수납 아이템
            MakePaymentDdl();
            LoadRentDdl(ddlRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
            //ltDate.Text = TextNm["MONTH"];
            //ltInvoiceNo.Text = "Invoice NO.";
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltPaymentTy.Text = TextNm["ITEM"];
            ltPaymentDt.Text = "Issuing Date";
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];
            lnkbtnIssuing.Text = TextNm["ISSUING"];
            ltCompNo.Text = "Company Name";

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";

            chkAll.Enabled = CommValue.AUTH_VALUE_FALSE;

            // 세금계산서 최대값 조회
            var dtReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);

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
            var strMaxInvoiceNo = "0000000";

            var strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
            var strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

            if (lvPrintoutList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
            }

            // KN_USP_SET_SELECT_HOADONINFO_S00
            var dtReturn = ReceiptMngBlo.SpreadPrintListForHoadon(ddlRentCd.SelectedValue, txtSearchRoom.Text,txtCompanyNm.Text.Trim(), "",
                                                                        strStartDt, strEndDt, ddlItemCd.SelectedValue, txtUserTaxCd.Text,
                                                                        "", Session["LANGCD"].ToString());

            if (dtReturn != null)
            {
                lvPrintoutList.DataSource = dtReturn;
                lvPrintoutList.DataBind();

                chkAll.Enabled = intRowsCnt != CommValue.NUMBER_VALUE_0;
            }

            // 세금계산서 최대값 조회
            var dtMaxReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);

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
            var txtHfPrintSeq = (TextBox)iTem.FindControl("txtHfPrintSeq");
            var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
            var txtHfRefSerialNo = (TextBox)iTem.FindControl("txtHfRefSerialNo");
            var txtHfContractType = (TextBox) iTem.FindControl("txtHfContractType");
               
                    
            if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
            {
                var ltlnsCompNo = (Literal)iTem.FindControl("ltlnsCompNo");
                ltlnsCompNo.Text = drView["CompNm"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
            {
                txtHfUserSeq.Text = drView["UserSeq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["REF_SERIAL_NO"].ToString()))
            {
                txtHfRefSerialNo.Text = drView["REF_SERIAL_NO"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["CONTRACT_TYPE"].ToString()))
            {
                txtHfContractType.Text = drView["CONTRACT_TYPE"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["PrintSeq"].ToString()))
            {
                txtHfPrintSeq.Text = drView["PrintSeq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["ISSUING_DATE"].ToString()))
            {
                var strDate = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["ISSUING_DATE"].ToString()));
                var ltIssuingDt = (Literal)iTem.FindControl("ltIssuingDt");
                ltIssuingDt.Text = strDate;
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                ltInsRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            var ltInsBillNm = (Literal)iTem.FindControl("ltInsBillNm");
            var txtHfBillCd = (TextBox)iTem.FindControl("txtHfBillCd");

            if (!string.IsNullOrEmpty(drView["BillNm"].ToString()))
            {
                ltInsBillNm.Text = TextLib.StringDecoder(drView["RealCd"].ToString());
                txtHfBillCd.Text = TextLib.StringDecoder(drView["BillCd"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Description"].ToString()))
            {
                var txtInsDescription = (TextBox)iTem.FindControl("txtInsDescription");
                txtInsDescription.Text = TextLib.StringDecoder(drView["Description"].ToString()) ;
            }

            if (!string.IsNullOrEmpty(drView["AmtViNo"].ToString()))
            {
                var ltViAmount = (Literal)iTem.FindControl("ltViAmount");
                ltViAmount.Text = TextLib.MakeVietIntNo(double.Parse(drView["AmtViNo"].ToString()).ToString("###,##0"));
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
            foreach (var t in lvPrintoutList.Items)
            {
                if (((CheckBox)t.FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)t.FindControl("chkboxList")).Checked = isAllCheck;
                }
            }
        }

        /// <summary>
        /// 리스트 각 행별 체크 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int intTotalCount = CommValue.NUMBER_VALUE_0;
                int intCheckCount = CommValue.NUMBER_VALUE_0;
                
                string strInvoiceNo = string.Empty;

                for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                    {
                        intTotalCount++;

                        if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            strInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;
                            intCheckCount++;
                        }
                        else
                        {
                            string strOtherInvoiceNo = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInvoiceNo")).Text;

                            if (strOtherInvoiceNo != "" && strInvoiceNo.Equals(strOtherInvoiceNo))
                            {
                                ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = CommValue.AUTH_VALUE_TRUE;
                            }
                        }
                    }

                    var txtNewDt = ((TextBox)lvPrintoutList.Items[intTmpI].FindControl("txtInsRegDt"));
                    var hfNewDt = ((HiddenField)lvPrintoutList.Items[intTmpI].FindControl("hfInsRegDt"));
                    txtNewDt.Text = TextLib.MakeDateEightDigit(hfNewDt.Value.Replace("-", ""));
                }

                if (intTotalCount == intCheckCount)
                {
                    chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 출력버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {
            // Initializing Print Bundle No 
            InvoiceMngBlo.UpdatingBundleSeqNoForReset();

            try
            {
                var intCheckRow = CommValue.NUMBER_VALUE_0;

                var refPrintBundleNo = string.Empty;

                if (lvPrintoutList.Items.Count <= 0)
                {   
                    return;
                }

                for (var i = CommValue.NUMBER_VALUE_0; i < lvPrintoutList.Items.Count; i++)
                {
                    if (string.IsNullOrEmpty(hfRentCd.Value)) continue;
                    if (!((CheckBox) lvPrintoutList.Items[i].FindControl("chkboxList")).Checked) continue;
                    var contractType = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfContractType")).Text;
                    var refSerialNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSerialNo")).Text;
                    var monthAmtNo = ((Literal)lvPrintoutList.Items[i].FindControl("ltViAmount")).Text;
                    if (string.IsNullOrEmpty(refPrintBundleNo))
                    {
                        refPrintBundleNo = ((TextBox)lvPrintoutList.Items[i].FindControl("txtHfRefSerialNo")).Text;
                    }
                    //Set same print no to all checked invoice        
                    InvoiceMngBlo.UpdatingInvoiceNoForHoadon(contractType, refSerialNo, monthAmtNo,hfRentCd.Value, refPrintBundleNo);

                    intCheckRow++;
                }

                // 선택 사항이 있는지 없는지 체크
                if (intCheckRow > CommValue.NUMBER_VALUE_0)
                {
                    if(chkSetIssueDt.Checked)
                    {
                        if (string.IsNullOrEmpty(txtIssueDt.Text))
                        {
                            var sbNoSelection = new StringBuilder();

                            sbNoSelection.Append("CloseLoading();alert('Check special issue date');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "NoSelection", sbNoSelection.ToString(), CommValue.AUTH_VALUE_TRUE); 
                            return;
                        }
                        InvoiceMngBlo.UpdatingInvoiceSpecialIssueDt(refPrintBundleNo,txtIssueDt.Text.Replace("-","").Trim());
                    }
                    var sbPrintOut = new StringBuilder();

                    sbPrintOut.Append("CloseLoading();window.open(\"/Common/RdPopup/RDPopupHoadonKNPreview.aspx?Datum0=" + refPrintBundleNo + "\", \"Hoadon\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);  
                }
                else
                {
                    // 화면 초기화
                    LoadData();

                    // 선택된 대상 없음
                    var sbNoSelection = new StringBuilder();

                    sbNoSelection.Append("CloseLoading();alert('" + AlertNm["INFO_HAS_NO_SELECTED_ITEM"] + "');");

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

        protected void lnkbtnExcel_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
           

            try
            {



                var strStartDt = txtStartDt.Text.Replace("-", "").Replace(".", "");
                var strEndDt = txtEndDt.Text.Replace("-", "").Replace(".", "");

                // KN_USP_SET_SELECT_HOADONINFO_S03
                var dtReturn = ReceiptMngBlo.SpreadPrintExcelForHoadon(ddlRentCd.SelectedValue, txtSearchRoom.Text,
                                                                   txtCompanyNm.Text.Trim(), "",
                                                                   strStartDt, strEndDt, ddlItemCd.SelectedValue,
                                                                   txtUserTaxCd.Text,
                                                                   "", Session["LANGCD"].ToString());
                if (dtReturn.Rows.Count <= 0)
                {
                    return;
                }
                var feeTy = ddlItemCd.SelectedValue == "" ? "" : "-" + ddlItemCd.SelectedItem;
                var fileName = Server.UrlEncode("Invoice List ").Replace("+", " ") + feeTy;
                GenerateExcel(dtReturn, fileName, fileName);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
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
                tbls.Columns[11].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[10].TotalsRowFunction = RowFunctions.Sum;
                tbls.Columns[9].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:M1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:M1"].AutoFitColumns();
                using (var col = ws.Cells[2, 10, 2 + tbl.Rows.Count, 12])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                using (var col = ws.Cells[2, 5, 2 + tbl.Rows.Count, 5])
                {
                    col.Style.Numberformat.Format = "dd/MM/yyyy";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
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

        protected void lvPrintoutList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
