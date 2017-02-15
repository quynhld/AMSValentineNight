using System;
using System.Data;
using System.IO;
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
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;
using KN.Settlement.Biz;
namespace KN.Web.Management.Manage
{
    public partial class PaymentRenovationAPTReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (IsPostBack) return;
                
                CheckParam();
                InitControls();
               // LoadUserInfo();
               // LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params[Master.PARAM_DATA1] == null) return;
            if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1])) return;
            //hfUserSeq.Value = Request.Params[Master.PARAM_DATA1];
            //hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
        }
        protected void InitControls()
        {

            lnkbtnSearch.Text = TextNm["SEARCH"];            

            // 섹션코드 조회
            // LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
            
            
            //txtSearchDt.Text = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.ToString("MM");
            MakeFeeTypeDdl(ddlFeeType);                
           
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_MNG_SELECT_RENOVATION_APT_S00
            var currentDt = txtSearchDt.Text.Replace("-", "");
            var eCurrentDt = txtESearchDt.Text.Replace("-", "");
            var dsReturn = MngPaymentBlo.ListRenovationInfoApt(ddlFeeType.SelectedValue, currentDt, txtRoomNo.Text, txtCompanyNm.Text,eCurrentDt);

            lvPaymentList.DataSource = dsReturn;
            lvPaymentList.DataBind();
            
        }

        /// <summary>
        /// 검색버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();            

            try
            {
                var roomNo = string.Empty;
                var feeTy = string.Empty;
                var tenantNm = string.Empty;

                roomNo = txtRoomNo.Text;
                feeTy = ddlFeeType.SelectedValue;
                tenantNm = txtCompanyNm.Text;
                var startDt = txtSearchDt.Text.Trim().Replace("-", "");
                var endDt = txtESearchDt.Text.Trim().Replace("-", "");


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + feeTy + "','" + startDt + "','" + roomNo + "','" + tenantNm + "','" + endDt + "');", CommValue.AUTH_VALUE_TRUE);

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPaymentList_ItemCreated(object sender, ListViewItemEventArgs e)
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
        protected void lvPaymentDetails_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvPaymentList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;

            if (!string.IsNullOrEmpty(drView["RefSeq"].ToString()))
            {
                var txthfSeq = (TextBox)iTem.FindControl("txthfSeq");
                txthfSeq.Text = TextLib.StringDecoder(drView["RefSeq"].ToString());
            }


            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoom = (Literal)iTem.FindControl("ltRoom");
                ltRoom.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
            {
                var ltInsName = (Literal)iTem.FindControl("ltInsName");
                ltInsName.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                var ltFeeTy = (Literal)iTem.FindControl("ltFeeTy");
                ltFeeTy.Text = TextLib.StringDecoder(drView["FeeNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["DepositPayDt"].ToString()))
            {
                var ltPayDay = (Literal)iTem.FindControl("ltPayDay");
                ltPayDay.Text = TextLib.MakeDateEightDigit(drView["DepositPayDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["DepositReturnDt"].ToString()))
            {
                var ltReturnDate = (Literal)iTem.FindControl("ltReturnDate");
                ltReturnDate.Text = TextLib.MakeDateEightDigit(drView["DepositReturnDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["DepositPayAmt"].ToString()))
            {
                var ltInsTotalAmt = (Literal)iTem.FindControl("ltInsTotalAmt");
                ltInsTotalAmt.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["DepositPayAmt"].ToString())).ToString("###,##0"));
            }

            var imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
            imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            var imgbtnPrint = (ImageButton)iTem.FindControl("imgbtnPrint");
            imgbtnPrint.OnClientClick = "javascript:return fnConfirmR('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            if (!string.IsNullOrEmpty(drView["DepositReturnDt"].ToString()))
            {
                imgbtnPrint.Visible = false;
            }
        }



        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {                       

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
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
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP))
                    {
                        ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
            }
        }

       
        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void MakeFeeTypeDdl( DropDownList ddllist)
        {
            ddllist.Items.Clear();
            ddllist.Items.Add(new ListItem("Fee Type", ""));
            ddllist.Items.Add(new ListItem("Renovation Fee", "0015"));
            ddllist.Items.Add(new ListItem("ParkingCard Fee", "0007"));
            ddllist.Items.Add(new ListItem("Return car card", "0017"));
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

       
        public void MakeAccountDdl(DropDownList ddlParams)
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
            // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
            // Utility Fee : Chestnut 매출
            // 그외 KeangNam 매출
            const string strCompCd = CommValue.MAIN_COMP_CD;
            var dtReturn = AccountMngBlo.SpreadBankAccountInfo(strCompCd);

            ddlParams.Items.Clear();

            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

            foreach (var dr in dtReturn.Select())
            {
                ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
            }
        }

        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            var sbPrintOut = new StringBuilder();

            sbPrintOut.Append("window.open('/Common/RdPopup/RDPopupReciptRenovationAPT.aspx?Datum0=" + txtHfRefSeq.Text + "&Datum1=" + 2 + "&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Renovation", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
        }

        protected void lvReceivable_ItemCreated(object sender, ListViewItemEventArgs e)
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


        protected void lvPaymentDetails_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var memNo = Session["MemNo"].ToString();
                var ip = Session["UserIP"].ToString();
                var seq = (TextBox)lvPaymentList.Items[e.ItemIndex].FindControl("txthfSeq");
                //KN_USP_MNG_DELETE_RENOVATION_M00
                MngPaymentBlo.DeleteRenovationAptDetails(seq.Text,1,memNo,ip,"","");
                var sbList = new StringBuilder();
                sbList.Append("alert('" + AlertNm["INFO_DELETE_ISSUE"] + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Renovation", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                LoadData();
                upSearch.Update();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }            
        }

        protected void lvPaymentDetails_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

                ErrLogger.MakeLogger(ex);
            }

        }      

        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                //DataSet dtSource = new DataSet();
                // 세션체크
                AuthCheckLib.CheckSession();
                var currentDt = txtSearchDt.Text.Replace("-", "");
                var eCurrentDt = txtESearchDt.Text.Replace("-", "");
                var dtSource = MngPaymentBlo.SelectExcelRenovationAndCarCard(ddlFeeType.SelectedValue, currentDt, txtRoomNo.Text, txtCompanyNm.Text, eCurrentDt);                

                if (dtSource.Tables[0].Rows.Count <= 0)
                {
                    return;
                }
                var feeTy = ddlFeeType.SelectedValue == "" ? "" : "-" + ddlFeeType.SelectedItem.ToString();
                var period = " " + currentDt + "~" + eCurrentDt;

                var fileName = Server.UrlEncode("Renovation and Carcard ").Replace("+", " ") + feeTy + period;
                //var fileName = "AA";

                GenerateExcel(dtSource.Tables[0], fileName, fileName);
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
                ////نمایش جمع ستون هزینه‌های ماه‌ها
                var tbls = ws.Tables[0];

                //tbls.ShowTotal = true;
                ////Set Sum 
                //tbls.Columns[12].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[11].TotalsRowFunction = RowFunctions.Sum;
                //tbls.Columns[10].TotalsRowFunction = RowFunctions.Sum;


                //Format the header for column 1-3

                using (var rng = ws.Cells["A1:F1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                ws.Cells["A1:F1"].AutoFitColumns();
                using (var col = ws.Cells[2, 6, 2 + tbl.Rows.Count, 6])
                {
                    col.Style.Numberformat.Format = "#,##0";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                using (var col = ws.Cells[2, 4, 2 + tbl.Rows.Count, 5])
                {
                    col.Style.Numberformat.Format = "dd/MM/yyyy";
                    col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
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
