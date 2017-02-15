using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Manage.Biz;
using KN.Resident.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class CashReportByExcel : BasePage
    {
        string strInit = string.Empty;
        int intInit = CommValue.NUMBER_VALUE_0;
        object objTag = new object();

        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;

        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack) return;
                CheckParam();
                InitControls();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params["RentCd"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["RentCd"]))
                {
                    txtHfRentCd.Text = Request.Params["RentCd"];
                }
                else
                {
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_APTSHOP;
                }
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APTSHOP;
            }
        }

        protected void InitControls()
        {

            //ltAddonFile.Text = TextNm["EXCELUPLOAD"];
            //ltMonth.Text = TextNm["MONTH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + CommValue.TENANTTY_VALUE_CORPORATION + "','" + CommValue.TERM_VALUE_LONGTERM + "','" + CommValue.TERM_VALUE_SHORTTERM + "');";
            lnkMakeCashReport.OnClientClick = "javascript:return fnCashReport('')";

            MakeItemDdl();
        }

        protected void LoadData()
        {                 
            
            var strYear = txtSearchDt.Text.Replace("-", "").Substring(0, 4);// DateTime.Now.ToString("yyyy");
            var strMonth = txtSearchDt.Text.Replace("-", "").Substring(4, 2);//DateTime.Now.ToString("MM");
            var strDay = string.Empty;
            
            var dataType = rdbDate.SelectedValue;

            if (dataType == "D")
            {
                strDay = txtSearchDt.Text.Replace("-", "").Substring(6, 2);
            }

            var feeType = ddlItems.SelectedValue;

            var dtReturn = RemoteMngBlo.SelectCashReport(txtHfRentCd.Text, feeType, strYear, strMonth, strDay, dataType);
            if (dtReturn == null || dtReturn.Tables.Count<0) return;
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
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
            var drView = (System.Data.DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            if (!string.IsNullOrEmpty(drView["FeeTyName"].ToString()))
            {
                var ltFeeType = (Literal)iTem.FindControl("ltFeeType");
                ltFeeType.Text = drView["FeeTyName"].ToString();
            }
            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                ltRoomNo.Text = drView["RoomNo"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RentalYear"].ToString()))
            {
                var dateType = rdbDate.SelectedValue;

                var ltRentalYn = (Literal)iTem.FindControl("ltRentalYN");
                ltRentalYn.Text = dateType == "M" && dateType == "D" ? TextLib.StringDecoder(drView["RentalYear"] + "/" + drView["RentalMM"]) : drView["RentalYear"].ToString();
            }
            if (!string.IsNullOrEmpty(drView["TOTAL_PAID"].ToString()))
            {
                var ltMonthFee = (Literal)iTem.FindControl("ltMonthFee");
                ltMonthFee.Text = TextLib.MakeVietIntNo(double.Parse(drView["TOTAL_PAID"].ToString()).ToString("###,##0"));
            }

            if (!string.IsNullOrEmpty(drView["RECEIVABLE"].ToString()))
            {
                var ltReceivable = (Literal)iTem.FindControl("ltReceivable");
                ltReceivable.Text = TextLib.MakeVietIntNo(double.Parse(drView["RECEIVABLE"].ToString()).ToString("###,##0"));   
            }
            if (!string.IsNullOrEmpty(drView["CASH"].ToString()))
            {
                var ltCash = (Literal)iTem.FindControl("ltCash");
                ltCash.Text = TextLib.MakeVietIntNo(double.Parse(drView["CASH"].ToString()).ToString("###,##0"));
            }
            if (!string.IsNullOrEmpty(drView["CARD"].ToString()))
            {
                var ltCard = (Literal)iTem.FindControl("ltCard");
                ltCard.Text =  TextLib.MakeVietIntNo(double.Parse(drView["CARD"].ToString()).ToString("###,##0"));
            }
            if (!string.IsNullOrEmpty(drView["TRANSFER"].ToString()))
            {
                var ltTransfer = (Literal)iTem.FindControl("ltTransfer");
                ltTransfer.Text =  TextLib.MakeVietIntNo(double.Parse(drView["TRANSFER"].ToString()).ToString("###,##0"));                   
            }

            if (!string.IsNullOrEmpty(drView["BALANCE"].ToString()))
            {
                var ltBlance = (Literal)iTem.FindControl("ltBlance");
                ltBlance.Text = TextLib.MakeVietIntNo(double.Parse(drView["BALANCE"].ToString()).ToString("###,##0"));
            }
            if (string.IsNullOrEmpty(drView["PAY_DATE"].ToString())) return;
            var ltPaydate = (Literal)iTem.FindControl("ltPaydate");


            var dataType = rdbDate.SelectedValue;

            switch (dataType)
            {
                case "M": ltPaydate.Text = TextLib.MakeDateSixDigit(drView["PAY_DATE"].ToString());
                    break;
                case "Y": ltPaydate.Text = TextLib.MakeDateSixDigit(drView["PAY_DATE"].ToString());
                    break;
                case "D": ltPaydate.Text = TextLib.MakeDateEightDigit(drView["PAY_DATE"].ToString());
                    break;
            }
        }        

        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void lnkbtnFileUpload_Click(object sender, EventArgs e)
        {
            var strroomNO = string.Empty;

            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (fuExcelUpload.HasFile)
                {
                    char[] chDiv = { '.' };
                    var strFileType = fuExcelUpload.PostedFile.ContentType;
                    var strTmpArray = fuExcelUpload.PostedFile.FileName.Split(chDiv);
                    

                    if (strTmpArray.Length > 0)
                    {
                        var isFailInsert = CommValue.AUTH_VALUE_FALSE;
                        var strExtension = strTmpArray[strTmpArray.Length - 1];

                        if (strFileType == CommValue.EXCEL_CONTTYPE_VALUE_XLS || strExtension.ToLower().Equals(CommValue.EXCEL_TYPE_TEXT_XLS) || strExtension.ToLower().Equals(CommValue.EXCEL_TYPE_TEXT_XLSX))
                        {
                            // Excel Data 리딩
                            var erReader = new ExcelReaderLib();
                            var dtTable = erReader.ExtractDataTable2010(fuExcelUpload.PostedFile.FileName).Tables[0];

                           
                            // 각 컬럼별 Validation 체크 후 등록
                            foreach (var dr in dtTable.Select())
                            {
                                string strRentCd;
                                string paymentCode;
                                string feeTy;
                                var payDate = string.Empty;                                
                                var rentalYear = string.Empty;
                                var rentalMonth = string.Empty;
                                var roomNo = string.Empty;                              
                                var transfer = 0.0d;
                                var card = 0.0d;
                                var cash = 0.0d;
                                var paidFee = 0.0d;
                                var monthlyFee = 0.0d;
                               

                                if (!string.IsNullOrEmpty(dr["RentCode"].ToString()))
                                {
                                    strRentCd = dr["RentCode"].ToString().PadLeft(4, '0');
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["PaymentCode"].ToString()))
                                {
                                     paymentCode = dr["PaymentCode"].ToString().PadLeft(4, '0');
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["FeeTy"].ToString()))
                                {
                                    feeTy = dr["FeeTy"].ToString().PadLeft(4, '0');
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["Transfer"].ToString().Trim()))
                                {

                                    transfer = double.Parse(dr["Transfer"].ToString());
                                     
                                }
                                else
                                {
                                   // isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    //break;
                                    transfer = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["Card"].ToString().Trim()))
                                {
                                    card = dr["Card"].ToString().Trim() == "" ? 0.0d : double.Parse(dr["Card"].ToString().Trim());
                                }
                                else
                                {
                                  //  isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                   // break;
                                    card = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["Cash"].ToString().Trim()))
                                {
                                     if (dr["cash"] is string)
                                     {
                                         cash = dr["cash"].ToString().Trim() == "" ? 0.0d : double.Parse(dr["Cash"].ToString().Trim());
                                     }
                                     else
                                     {
                                         cash = double.Parse(dr["Cash"].ToString());
                                     }
                                }
                                else
                                {
                                  //  isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                   // break;
                                    cash = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["PaidFee"].ToString().Trim()))
                                {
                                     paidFee = double.Parse(dr["PaidFee"].ToString());
                                }
                                else
                                {
                                  //  isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                  //  break;
                                    paidFee = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["PayDate"].ToString().Trim()))
                                {
                                  //  var payDate1 = DateTime.ParseExact(dr["PayDate"].ToString(), "yyyyMMdd", provider: System.IFormatProvider);
                                    if (dr["PayDate"] is string )
                                    {
                                        var dt = dr["PayDate"].ToString() == "" ? "" : dr["PayDate"].ToString();

                                        payDate = dt;
                                    }
                                    else
                                    {
                                        var dateTime = Convert.ToDateTime(dr["PayDate"].ToString());

                                        payDate = dateTime.ToString("yyyyMMdd");
                                    }
                                }
                                else
                                {
                                  //  isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                   // break;   
                                    payDate = "";
                                }

                                if (!string.IsNullOrEmpty(dr["MonthlyFee"].ToString()))
                                {
                                     monthlyFee = double.Parse(dr["MonthlyFee"].ToString());
                                }
                                else
                                { 
                                   // isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                   // break;     
                                    monthlyFee = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["RentalMonth"].ToString()))
                                {
                                     rentalMonth  = dr["RentalMonth"].ToString();
                                }
                                else
                                {
                                   // isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                   // break;
                                   // card = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["RentalYear"].ToString()))
                                {
                                     rentalYear = dr["RentalYear"].ToString();
                                }
                                else
                                {
                                   // isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    //break;
                                }

                                if (!string.IsNullOrEmpty(dr["RoomNo"].ToString()))
                                {
                                     roomNo = dr["RoomNo"].ToString();
                                     strroomNO = dr["RoomNo"].ToString();
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;                                  
                                }

                                // KN_EXCEL_UPLOADING_APT_MNGFEE
                                RemoteMngBlo.RegistryCashReportManually(roomNo,rentalYear,rentalMonth,monthlyFee,payDate,paidFee,cash,card,transfer,feeTy,strRentCd,paymentCode);
                            
                            }

                            if (isFailInsert)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                                LoadData();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_REGIST_ITEM"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                            }
                        }
                        else
                        {                                           
                           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_PERMIT_ONLY_XLS"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                            LoadData();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["ALERT_SELECT_FILE"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                        LoadData();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["ALERT_SELECT_FILE"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
                DisplayAlert(strroomNO);
            }
        }

        protected void lnkMakeCashReport_Click(object sender, EventArgs e)
        {

        }

        protected void MakeItemDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItems.Items.Clear();

            ddlItems.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                if (dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_MNGFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_PARKINGFEE) ||
                    //dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_PARKINGCARDFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) ||
                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE))
                {
                    ddlItems.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        protected void lnkExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var strYear = txtSearchDt.Text.Replace("-", "").Substring(0, 4);// DateTime.Now.ToString("yyyy");
                var strMonth = txtSearchDt.Text.Replace("-", "").Substring(4, 2);//DateTime.Now.ToString("MM");
                var strDay = DateTime.Now.ToString("dd");
                var feeType = ddlItems.SelectedValue;
                var dateType = rdbDate.SelectedValue;
                var dtReturn = RemoteMngBlo.SelectCashReport(txtHfRentCd.Text, feeType, strYear, strMonth, strDay, dateType);
                if (dtReturn == null || dtReturn.Tables.Count < 0) return;

                var strRentNm = "";

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW).Replace("+", " ")+"("+ strYear + "-" + strMonth +").xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.Charset = "utf-8";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
                this.EnableViewState = false;

                var stringWriter = new StringWriter();
                var htmlTextWriter = new HtmlTextWriter(stringWriter);


                string strTitle = "<p align=center><font size=4 face=Gulim><b>APT - Cash Report - " + strYear + "-" + strMonth + "</b></font></p>";
                htmlTextWriter.Write(strTitle);

                var gv = new GridView();
                gv.Font.Name = "Tahoma";
                gv.DataSource = dtReturn;
                gv.DataBind();
                gv.RenderControl(htmlTextWriter);

                Response.Write(stringWriter.ToString());
                Response.End();

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
