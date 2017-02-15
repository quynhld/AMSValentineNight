using System;
using System.Data;
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
using KN.Common.Method.Util;

using KN.Resident.Biz;
using KN.Settlement.Biz;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Table;
using OfficeOpenXml;

namespace KN.Web.Settlement.Balance
{
    public partial class TrialBalanceReport : BasePage
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

                    //if (CheckParams())
                    //{
                        
                    //}
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

            ltDate.Text = TextNm["MONTH"];
            //ltInvoiceNo.Text = "Invoice NO.";
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltPaymentTy.Text = TextNm["ITEM"];
            ltPaymentDt.Text = TextNm["REGISTDATE"];
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];

            MakePaymentDdl(ddlItemCd);

            //lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            

            //lnkbtnIssuing.Visible = Master.isModDelAuthOk;
            //lnkbtnIssuing.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";
            // 섹션코드 조회
            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtCompanyNm.ClientID + "', '" + txtSearchRoom.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtCompanyNm.Text + "', '" + ddlRentCd.SelectedValue + "');";

            LoadRentDdl(ddlRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
        }

        protected void LoadData()
        {
            string strSearchRoomNo = string.Empty;

            if (lvPrintoutList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                //chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
            }

          

            strInit = CommValue.AUTH_VALUE_EMPTY;
            intInit = CommValue.NUMBER_VALUE_0;

        }


        /// <summary>
        /// 
        /// </summary>
       

        //protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        //{
        //    DataTable dtReturn = new DataTable();

        //    dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

        //    ddlParamNm.Items.Clear();

        //    foreach (DataRow dr in dtReturn.Select())
        //    {
        //        if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING))
        //        {
        //            if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
        //                !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
        //                !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) &&
        //                !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP))
        //            {
        //                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
        //            }
        //        }
        //    }
        //}
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

        private void MakePaymentDdl(DropDownList ddlParam)
        {

            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlParam.Items.Clear();

            ddlParam.Items.Add(new ListItem("PAYMENTKIND", ""));

            foreach (var dr in dtReturn.Select())
            {
                ddlParam.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
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


                if (!string.IsNullOrEmpty(drView["DataYear"].ToString()))
                {
                    var ltPeriod = (Literal)iTem.FindControl("ltPeriod");
                    ltPeriod.Text = drView["DataYear"].ToString();
                }

                
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
                
                TextBox txtHfPaymentDt = (TextBox)iTem.FindControl("txtHfPaymentDt");
                TextBox txtHfPaymentSeq = (TextBox)iTem.FindControl("txtHfPaymentSeq");
                TextBox txtHfPaymentDetSeq = (TextBox)iTem.FindControl("txtHfPaymentDetSeq");
             

                intRowsCnt++;
            }
        }

       

        /// <summary>
        /// 출력버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {

            try
            {
             
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

                var rentCd = ddlRentCd.SelectedValue;
                var feeTy = ddlItemCd.SelectedValue;
                var roomNo = txtSearchRoom.Text;
                var tenantName = txtCompanyNm.Text;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnAccountList('" + rentCd + "','" + feeTy + "','" + roomNo + "','" + tenantName + "');", CommValue.AUTH_VALUE_TRUE);

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

        public void GenerateExcel(DataTable tbl, string excelSheetName, string fileName)
        {
            using (var pck = new ExcelPackage())
            {
                //Create the worksheet
                var ws = pck.Workbook.Worksheets.Add(excelSheetName);


                ws.SelectedRange[1, 1, 1, 23].Merge = true;
                ws.SelectedRange[2, 1, 2, 23].Merge = true;
                ws.Cells["A1"].Value = fileName;
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A1"].Style.Font.Name = "Calibri";
                ws.Cells["A1"].Style.Font.Size = 18;
                ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                pck.Workbook.Properties.Title = @"This code is part of tutorials available";
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A3"].LoadFromDataTable(tbl, true, TableStyles.None);
                ////نمایش جمع ستون هزینه‌های ماه‌ها
                var tbls = ws.Tables[0];

                tbls.ShowTotal = true;
                //Set Sum 
                //tbls.Columns[9].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[8].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[7].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A3:W3"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A3:W3"].AutoFitColumns();
                using (var col = ws.Cells[4, 1, 4 + tbl.Rows.Count, 2])
                {
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                using (var col = ws.Cells[4, 8, 4 + tbl.Rows.Count, 10])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                //using (var col = ws.Cells[4, 6, 4 + tbl.Rows.Count, 6])
                //{
                //    col.Style.Numberformat.Format = "dd/MM/yyyy";
                //    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //}
                using (var col = ws.Cells[4, 8, 4 + tbl.Rows.Count, 9])
                {
                    col.Style.Numberformat.Format = "dd/MM/yyyy";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                Response.Clear();
                //Write it back to the client
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=" + fileName + ".xlsx");

                var stringWriter = new StringWriter();
                var htmlTextWriter = new HtmlTextWriter(stringWriter);


                Response.BinaryWrite(pck.GetAsByteArray());
                Response.End();
            }
        }

        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
          
        }

        protected void lnkLoadData_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
        
    }
}