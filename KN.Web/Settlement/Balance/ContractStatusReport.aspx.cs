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
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Table;
using OfficeOpenXml;

namespace KN.Web.Settlement.Balance
{
    public partial class ContractStatusReport : BasePage
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
         
            LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
            
        }

        protected void LoadData()
        {
           
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
           
        }

        protected void lvPrintoutList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
           
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
                //DataSet dtSource = new DataSet();
                // 세션체크
                AuthCheckLib.CheckSession();
                

                // KN_USP_MNG_SELECT_ACCOUNTSINFO_S01
                var dtSource = BalanceMngBlo.SelectContractStatusReport(ddlInsRentCd.SelectedValue);

                if (dtSource.Rows.Count <= 0)
                {
                    return;
                }

                var sheetname = "Contract Status";
                var rentalNm = ddlInsRentCd.SelectedValue == "0000" ? "" : "- " + ddlInsRentCd.SelectedItem + " ";

                var fileName = Server.UrlEncode("Summarisation of Contract Status ").Replace("+", " ") + rentalNm ;

                GenerateExcel(dtSource, sheetname, fileName);
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
          
        


        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            
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


                ws.SelectedRange[1, 1, 1, 22].Merge = true;
                ws.SelectedRange[2, 1, 2, 22].Merge = true;
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

                using (var rng = ws.Cells["A3:V3"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                //ws.Cells["A3:V3"].AutoFitColumns();
                using (var col = ws.Cells[4, 1, 4 + tbl.Rows.Count, 21])
                {
                    ws.Cells.AutoFitColumns();
                }

                using (var col = ws.Cells[4, 1, 4 + tbl.Rows.Count, 2])
                {
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
                using (var col = ws.Cells[4, 8, 4 + tbl.Rows.Count, 10])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                using (var col = ws.Cells[4, 6, 4 + tbl.Rows.Count, 6])
                {
                    col.Style.Numberformat.Format = "dd/MM/yyyy";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
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
            try
            {

                AuthCheckLib.CheckSession();


                // KN_USP_MNG_SELECT_ACCOUNTSINFO_S01
                var dtSource = BalanceMngBlo.SelectContractStatusReport(ddlInsRentCd.SelectedValue);

                if (dtSource.Rows.Count <= 0)
                {
                    return;
                }

                var sheetname = "Contract Status";
                var rentalNm = ddlInsRentCd.SelectedValue == "0000" ? "" : "- " + ddlInsRentCd.SelectedItem + " ";

                var fileName = Server.UrlEncode("Summarisation of Contract Status ").Replace("+", " ") + rentalNm;

                GenerateExcel(dtSource, sheetname, fileName);

                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        
    }
}