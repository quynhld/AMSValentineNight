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
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TempDBConnection"].ToString());

        public string DATA_APT = CommValue.RENTAL_VALUE_APT;
        public string DATA_APTSTORE = CommValue.RENTAL_VALUE_APTSHOP;

        string strIvnID = string.Empty;

        DataTable dtItemInfo = new DataTable();
        DataTable dtInInfo = new DataTable();
        DataTable dtOutInfo = new DataTable();  

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    InitControls();
                    if (Request.QueryString["ID"] != null)
                    {
                        strIvnID = Request.QueryString["ID"].ToString();
                        loadData(strIvnID);
                    }
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
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                string cmdStr = string.Empty;
                int IVN_ID = 0;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                if(Request.QueryString["ID"] == null)
                {
                    cmdStr = string.Format(stringInsertCommand(), getParameter());
                    cmd.CommandText = cmdStr;
                    IVN_ID = (int)cmd.ExecuteScalar();
                    Response.Redirect(string.Format("~/Inventory/ITEMS/InventoryAddNew.aspx?ID=={0}", IVN_ID));
                }
                else
                {
                    cmdStr = string.Format(stringUpdateCommand(),getParameterForUpdateCommand());
                    cmd.CommandText = cmdStr;
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

        private string stringInsertCommand()
        {
            string strInsert = string.Empty;
            strInsert = @"INSERT INTO [dbo].[Inventory_Items]
                                                   ([Item_Name]     --0
                                                   ,[Item_EName]    --1
                                                   ,[Item_Type]     --2
                                                   ,[Item_LC_Area]  --3
                                                   ,[Item_LC_Zone]  --4
                                                   ,[Item_LC_No]    --5
                                                   ,[Item_Size_W]   --6
                                                   ,[Item_Size_H]   --7
                                                   ,[Item_Size_L]   --8
                                                   ,[Item_Size_Ra]  --9
                                                   ,[Item_Amout]    --10
                                                   ,[Item_Photo]    --11
                                                   ,[Item_owner]    --12
                                                   ,[Item_owner_ID] --13
                                                   ,[Item_Group_ID] --14
                                                   ,[Item_Group_Name]   --15
                                                   ,[Item_Status]   --16
                                                   ,[Item_Model]    --17
                                                    ,[ItemUnit]     --18
                                                    ,[CreateDate]   --19
                                                    ,[CreateBy]) OUTPUT INSERTED.[IVN_ID]    --20
                                             VALUES
                                                   (N'{0}'                   
                                                   ,N'{1}'           
                                                   ,N'{2}'           
                                                   ,N'{3}'           
                                                   ,N'{4}'           
                                                   ,N'{5}'           
                                                   ,{6} --decimal       
                                                   ,{7} --decimal   
                                                   ,{8} --decimal   
                                                   ,{9} --decimal   
                                                   ,{10} -- int 
                                                   ,N'{11}'          
                                                   ,N'{12}'          
                                                   ,{13}            
                                                   ,{14}                
                                                   ,N'{15}'              
                                                   , {16}               
                                                   ,N'{17}'
                                                    ,N'{18}'
                                                    ,'{19}'
                                                    ,'{20}')";


            //strInsert = string.Format(strInsert, insertParams);
            return strInsert;
        }

        private string stringUpdateCommand()
        {
            string strUpdate = string.Empty;
            strUpdate = @"UPDATE [dbo].[Inventory_Items]
                           SET [Item_Name]      = N'{0}'  --<Item_Name, nvarchar(500),> 1
                              ,[Item_EName]     = N'{1}' --<Item_EName, nvarchar(500),> 2
                              ,[Item_Type]      = N'{2}' --<Item_Type, nvarchar(500),> 3
                              ,[Item_LC_Area]   = N'{3}' --<Item_LC_Area, nvarchar(50),> 4
                              ,[Item_LC_Zone]   = N'{4}' --<Item_LC_Zone, nvarchar(50),> 5
                              ,[Item_LC_No]     = N'{5}' --<Item_LC_No, nvarchar(50),> 6
                              ,[Item_Size_W]    = {6} --<Item_Size_W, decimal(18,0),> 7
                              ,[Item_Size_H]    = {7} --<Item_Size_H, decimal(18,0),>8
                              ,[Item_Size_L]    = {8} --<Item_Size_Wide, decimal(18,0),> 9
                              ,[Item_Size_Ra]   = {9} --<Item_Size_Ra, decimal(18,0),> 10
                              ,[Item_Amout]     = {10} --<Item_Amout, decimal(18,2),> 11
                              ,[Item_Photo]     = N'{11}' --<Item_Photo, nvarchar(500),> 12
                              ,[Item_owner]     = N'{12}' --<Item_owner, nvarchar(500),> 13
                              ,[Item_owner_ID]  = {13} --<Item_owner_ID, int,> 14
                              ,[Item_Group_ID]  = {14} --<Item_Group_ID, int,> 15
                              ,[Item_Group_Name] = N'{15}' --<Item_Group_Name, nvarchar(500),> 16
                              ,[Item_Status]    = '{16}' --<Item_Status, bit,> 17
                              ,[Item_Model]     = N'{17}' --<Item_Model, nvarchar(500),> 18
                              ,[ItemUnit]       = N'{18}' --<ItemUnit, nvarchar(50),> 19
                              ,[ModDate]        = '{19}' --<ModDate, datetime,> 22
                              ,[ModBy]          = '{20}' --<ModBy, nvarchar(500),> 23
                                 WHERE IVN_ID = {21}";
            return strUpdate;
        }

        private object[] getParameter()
        {

            string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            object[] insertParams = new object[21];
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

            decimal lenght = 0;
            if (txtIvtWide.Text != string.Empty)
            {
                lenght = Convert.ToDecimal(txtIvtWide.Text);
            }
            insertParams[8] = lenght;

            decimal radius = 0;
            if (txtIvtRadius.Text != string.Empty)
            {
                radius = Convert.ToDecimal(txtIvtRadius.Text);
            }
            insertParams[9] = radius;

            decimal amount = 0;
            if (txtIvtQuantity.Text != string.Empty)
            {
                amount = Convert.ToDecimal(txtIvtQuantity.Text);
            }
            insertParams[10] = amount;

            insertParams[11] = "";
            insertParams[12] = "";
            insertParams[13] = 0;
            insertParams[14] = 0;
            insertParams[15] = "";
            insertParams[16] = 0;
            insertParams[17] = txtIVTModel.Text;
            insertParams[18] = txtIVTUnit.Text;
            insertParams[19] = DateTime.Now;
            insertParams[20] = strInsMemIP;
            
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
            return insertParams;
        }

        private object[] getParameterForUpdateCommand()
        {

            string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            object[] insertParams = new object[22];
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

            decimal lenght = 0;
            if (txtIvtWide.Text != string.Empty)
            {
                lenght = Convert.ToDecimal(txtIvtWide.Text);
            }
            insertParams[8] = lenght;

            decimal radius = 0;
            if (txtIvtRadius.Text != string.Empty)
            {
                radius = Convert.ToDecimal(txtIvtRadius.Text);
            }
            insertParams[9] = radius;

            decimal amount = 0;//amount
            if (txtIvtQuantity.Text != string.Empty)
            {
                amount = Convert.ToDecimal(txtIvtQuantity.Text);
            }
            insertParams[10] = amount;

            insertParams[11] = "";//get images link behind
            insertParams[12] = "";
            insertParams[13] = 0;
            insertParams[14] = 0;
            insertParams[15] = "";
            insertParams[16] = "";//get from dropdownlist
            insertParams[17] = txtIVTModel.Text;
            insertParams[18] = txtIVTUnit.Text;
            insertParams[19] = DateTime.Now;
            insertParams[20] = strInsMemIP;
            
            insertParams[21] = Request.QueryString["ID"];

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
            return insertParams;
        }

        private void loadData(string id)
        {
            //get items info
            string strGetItemInfo = string.Format("select * from Inventory_Items where IVN_ID={0}", id);
            SqlCommand cmdgetItemInfo = new System.Data.SqlClient.SqlCommand(strGetItemInfo);
            cmdgetItemInfo.Connection = conn;
            SqlDataAdapter ItemAdapInf = new System.Data.SqlClient.SqlDataAdapter();
            ItemAdapInf.SelectCommand = cmdgetItemInfo;


            //get in info
            string getInInfo = string.Format("select * from Inventory_IN where IVN_IN_ID in (select INV_IN_ID from Inventory_IN_Details where IVN_ID = {0})", id);
            SqlCommand cmdGetInInfo = new System.Data.SqlClient.SqlCommand(getInInfo);
            cmdGetInInfo.Connection = conn;
            SqlDataAdapter InAdap = new System.Data.SqlClient.SqlDataAdapter();
            InAdap.SelectCommand = cmdgetItemInfo;

            //get out info
            string getOutInfo = string.Format("select * from Inventory_OUT where INV_OUT_ID in (select INV_OUT_ID from Inventory_OUT_Details where INV_ID = {0})", id);
            SqlCommand cmdGetOutInfo = new System.Data.SqlClient.SqlCommand(getOutInfo);
            cmdGetOutInfo.Connection = conn;
            SqlDataAdapter OutAdap = new System.Data.SqlClient.SqlDataAdapter();
            OutAdap.SelectCommand = cmdGetOutInfo;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                ItemAdapInf.Fill(dtItemInfo);
                InAdap.Fill(dtInInfo);
                OutAdap.Fill(dtOutInfo);

                bindata(dtItemInfo, dtInInfo, dtOutInfo);
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }
        private void bindata(DataTable Item, DataTable dtbIn, DataTable dtbOut)
        {
            txtIvtArea.Text = (Item.Rows[0]["Item_LC_Area"] == null ? string.Empty : Item.Rows[0]["Item_LC_Area"].ToString());
            txtIVTCategory.Text = (Item.Rows[0]["Item_Group_Name"] == null ? string.Empty : Item.Rows[0]["Item_Group_Name"].ToString());
            txtIVTEngName.Text = (Item.Rows[0]["Item_EName"] == null ? string.Empty : Item.Rows[0]["Item_EName"].ToString());
            txtIvtHeight.Text = (Item.Rows[0]["Item_Size_H"] == null ? string.Empty : Item.Rows[0]["Item_Size_H"].ToString());
            txtIVTModel.Text = (Item.Rows[0]["Item_Model"] == null ? string.Empty : Item.Rows[0]["Item_Model"].ToString());
            txtIvtNo.Text = (Item.Rows[0]["Item_LC_No"] == null ? string.Empty : Item.Rows[0]["Item_LC_No"].ToString());
            txtIvtQuantity.Text = (Item.Rows[0]["Item_Amout"] == null ? string.Empty : Item.Rows[0]["Item_Amout"].ToString());
            txtIvtRadius.Text = (Item.Rows[0]["Item_Size_Ra"] == null ? string.Empty : Item.Rows[0]["Item_Size_Ra"].ToString());
            txtIVTUnit.Text = (Item.Rows[0]["ItemUnit"] == null ? string.Empty : Item.Rows[0]["ItemUnit"].ToString());
            txtIVTViName.Text = (Item.Rows[0]["Item_Name"] == null ? string.Empty : Item.Rows[0]["Item_Name"].ToString());
            txtIvtWide.Text = (Item.Rows[0]["Item_Size_L"] == null ? string.Empty : Item.Rows[0]["Item_Size_L"].ToString());
            txtIvtWidth.Text = (Item.Rows[0]["Item_Size_W"] == null ? string.Empty : Item.Rows[0]["Item_Size_W"].ToString());
            txtIvtZone.Text = (Item.Rows[0]["Item_LC_Zone"] == null ? string.Empty : Item.Rows[0]["Item_LC_Zone"].ToString());
            IvtImage.ImageUrl = (Item.Rows[0]["Item_Photo"] == null ? string.Empty : Item.Rows[0]["Item_Photo"].ToString());

            lsvIN.DataSource = dtbIn;
            lsvIN.DataBind();

            lsvOut.DataSource = dtbOut;
            lsvOut.DataBind();
        }
    }

}