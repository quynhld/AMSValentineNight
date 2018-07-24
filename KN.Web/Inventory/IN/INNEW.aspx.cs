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

namespace KN.Web.Inventory
{
    public partial class INNEW : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString);
        public string strINV_IN_ID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            if (Request.QueryString["IVN_IN_ID"] != null)
            {
                strINV_IN_ID = Request.QueryString["IVN_IN_ID"].ToString();
            }
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
            bindata();
        }
        public void selectCommand()
        {
            string strDataSourceSelect = @"select ";
            //SqlDataSource1.SelectCommand = 
        }

        public DataTable selectItems()
        {
            DataTable dtb = new DataTable();
            //SqlCommand cmd = new SqlCommand("select * from Inventory_items", conn);
            SqlDataAdapter adap = new SqlDataAdapter("select * from Inventory_items", conn);
            try
            {
                if (conn.State != ConnectionState.Open)
                { conn.Open(); }
                adap.Fill(dtb);
            }
            catch
            {
                conn.Close();
            }
            return dtb;
        }
        public void bindata()
        {
            SqlDataSource ds = new SqlDataSource();
           string DeleteCommand ="DELETE FROM [Inventory_Items] WHERE [IVN_ID] = @original_IVN_ID AND (([Item_Name] = @original_Item_Name) OR ([Item_Name] IS NULL AND @original_Item_Name IS NULL)) AND (([Item_EName] = @original_Item_EName) OR ([Item_EName] IS NULL AND @original_Item_EName IS NULL)) AND (([Item_Amout] = @original_Item_Amout) OR ([Item_Amout] IS NULL AND @original_Item_Amout IS NULL)) AND (([Item_Type] = @original_Item_Type) OR ([Item_Type] IS NULL AND @original_Item_Type IS NULL))";
           string InsertCommand = "INSERT INTO [Inventory_Items] ([Item_Name], [Item_EName], [Item_Amout], [Item_Type]) VALUES (@Item_Name, @Item_EName, @Item_Amout, @Item_Type)";
           string Select_IVNITEMS_Command ="SELECT [IVN_ID], [Item_Name], [Item_EName], [Item_Amout], [Item_Type] FROM [Inventory_Items]" ;
           string UpdateCommand ="UPDATE [Inventory_Items] SET [Item_Name] = @Item_Name, [Item_EName] = @Item_EName, [Item_Amout] = @Item_Amout, [Item_Type] = @Item_Type WHERE [IVN_ID] = @original_IVN_ID AND (([Item_Name] = @original_Item_Name) OR ([Item_Name] IS NULL AND @original_Item_Name IS NULL)) AND (([Item_EName] = @original_Item_EName) OR ([Item_EName] IS NULL AND @original_Item_EName IS NULL)) AND (([Item_Amout] = @original_Item_Amout) OR ([Item_Amout] IS NULL AND @original_Item_Amout IS NULL)) AND (([Item_Type] = @original_Item_Type) OR ([Item_Type] IS NULL AND @original_Item_Type IS NULL))";
           string strSelectIN = @"Select 
                                    I.CreateDate
                                    ,i.CreateUser
                                    ,i.UsedFor
                                    ,i.Note
                                    ,iDetails.Amount
                                    ,items.Item_Amout
                                    ,items.Item_EName
                                    ,items.Item_EName
                                    ,items.Item_Photo
                                    ,items.Item_Type
                                    From inventory_IN I 
                                    inner join Inventory_IN_Details iDetails on iDetails.INV_IN_ID = I.IVN_IN_ID
                                    inner join Inventory_Items items on items.IVN_ID = iDetails.IVN_ID
                                    where I.IVN_IN_ID = {0}";
           DataTable dtbIN = new DataTable();
           SqlDataAdapter adapIN = new SqlDataAdapter(string.Format(strSelectIN, strINV_IN_ID), conn);
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch
            {
                conn.Close();
            }
            adapIN.Fill(dtbIN);
            lsvAddIn.DataSource = dtbIN;
            lsvAddIn.DataBind();

        }

        protected void lsvAddIn_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if((e.Item != null) && (e.Item.ItemType== ListViewItemType.InsertItem))
            {
                DropDownList drLoadItemsName = (DropDownList)lsvAddIn.InsertItem.FindControl("Item_NameTextBox");
                drLoadItemsName.DataSource = selectItems();
                drLoadItemsName.DataBind();

                DropDownList drLoadItemsEName = (DropDownList)lsvAddIn.InsertItem.FindControl("Item_ENameTextBox");
                drLoadItemsEName.DataSource = selectItems();
                drLoadItemsEName.DataBind();
            }
            int a= 0;
        }
    }
}