using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace KN.Web.Inventory
{
    public partial class InventoryAddNew : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        public string DATA_APT = CommValue.RENTAL_VALUE_APT;
        public string DATA_APTSTORE = CommValue.RENTAL_VALUE_APTSHOP;
        

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {

                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {

        }

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TempDBConnection"].ToString());
            try
            {
                string strInsert = @"INSERT INTO [dbo].[Inventory_Items]
                                                   ([Item_Name]
                                                   ,[Item_EName]
                                                   ,[Item_Type]
                                                   ,[Item_LC_Area]
                                                   ,[Item_LC_Zone]
                                                   ,[Item_LC_No]
                                                   ,[Item_Size_W]
                                                   ,[Item_Size_H]
                                                   ,[Item_Size_Wide]
                                                   ,[Item_Size_Ra]
                                                   ,[Item_Amout]
                                                   ,[Item_Photo]
                                                   ,[Item_owner]
                                                   ,[Item_owner_ID]
                                                   ,[Item_Group_ID]
                                                   ,[Item_Group_Name]
                                                   ,[Item_Status]
                                                   ,[Item_Model]
                                                    ,[CreateDate]
                                                    ,[CreateBy])
                                             VALUES
                                                   ('{0}'                   
                                                   ,'{1}'           
                                                   ,'{2}'           
                                                   ,'{3}'           
                                                   ,'{4}'           
                                                   ,'{5}'           
                                                   ,{6} --decimal       
                                                   ,{7} --decimal   
                                                   ,{8} --decimal   
                                                   ,{9} --decimal   
                                                   ,{10} -- int 
                                                   ,'{11}'          
                                                   ,'{12}'          
                                                   ,{13}            
                                                   ,{14}                
                                                   ,'{15}'              
                                                   ,0               
                                                   ,'{16}'
                                                    ,'{17}'
                                                    ,'{18}')";               

                object[] insertParams = new object[19];
                insertParams[0] = txtIVTViName.Text;
                insertParams[1] = txtIVTEngName.Text;
                insertParams[2] = txtIVTCategory.Text;
                insertParams[3] = txtIvtArea.Text;

                insertParams[4] = txtIvtZone.Text;
                insertParams[5] = txtIvtNo.Text;

                decimal height = 0;
                if (txtIvtHeight.Text != string.Empty)
                {
                    height = Convert.ToDecimal(txtIvtHeight.Text);
                }
                insertParams[6] = height;

                decimal width = 0;
                if (txtIvtWidth.Text != string.Empty)
                {
                    width = Convert.ToDecimal(txtIvtWidth.Text);
                }
                insertParams[7] = width;

                decimal wide = 0;
                if (txtIvtWide.Text != string.Empty)
                {
                    wide = Convert.ToDecimal(txtIvtWide.Text);
                }
                insertParams[8] = wide;

                decimal radius = 0;
                if (txtIvtRadius.Text != string.Empty)
                {
                    radius = Convert.ToDecimal(txtIvtRadius.Text);
                }
                insertParams[9] = radius;

                int quantity = 0;
                if (txtIvtQuantity.Text != string.Empty)
                {
                    quantity = Convert.ToInt32(txtIvtQuantity.Text);
                }
                insertParams[10] = quantity;

                insertParams[11] = "";
                insertParams[12] = "";
                insertParams[13] = 0;
                insertParams[14] = 0;
                insertParams[15] = "";
                insertParams[16] = txtIVTModel.Text;
                insertParams[17] = DateTime.Now;
                insertParams[18] = strInsMemIP;

                string inventoryID = "";
                if (fuIvtImage.HasFile)
                {
                    try
                    {
                        string strImgFilePath = Server.MapPath(string.Format("~//InventoryImg//{0}", fuIvtImage.FileName));
                        fuIvtImage.SaveAs(strImgFilePath);
                        insertParams[11] = fuIvtImage.FileName;
                    }
                    catch (Exception ex)
                    {
                        string strErr = ex.Message;
                    }
                }

                strInsert = string.Format(strInsert, insertParams);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = strInsert;

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        protected void lnkBtnImportExcel_Click(object sender, EventArgs e)
        {
            if (fuExcelUpload.HasFile)
            {
                string strPathUpload = Server.MapPath(string.Format("~//InventoryImg//{0}", fuExcelUpload.FileName));
                fuExcelUpload.SaveAs(strPathUpload);
                //string filePath = Server.MapPath(strPathUpload);
                char[] chDiv = { '.' };
                string strFileType = fuExcelUpload.PostedFile.ContentType.ToString();
                string[] strTmpArray = fuExcelUpload.PostedFile.FileName.Split(chDiv);
                string strExtension = string.Empty;

                DataTable dtTmpTable = new DataTable();

                if (strTmpArray.Length > 0)
                {
                    strExtension = strTmpArray[strTmpArray.Length - 1];
                    //if (strFileType == CommValue.EXCEL_CONTTYPE_VALUE_XLS && strExtension.ToLower().Equals(CommValue.EXCEL_TYPE_TEXT_XLS))
                    
                    if (strFileType == CommValue.EXCEL_CONTTYPE_VALUE_XLSX && strExtension.ToLower().Equals(CommValue.EXCEL_TYPE_TEXT_XLSX))
                    {
                        // Excel Data 리딩
                        ExcelReaderLib erReader = new ExcelReaderLib();
                        DataTable dtTable = new DataTable();

                        //dtTable = erReader.ExtractDataTable(strPathUpload);
                        DataSet ds = erReader.ExtractDataTable2010(strPathUpload);
                        
                    }
                }
            }
        }
    }

}