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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

using KN.Settlement.Biz;

namespace KN.Web.Inventory
{
    public partial class InventoryList : BasePage
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adap = new SqlDataAdapter();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                        InitControls();
                        LoadData(txtSearchNm.Text);
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
        private void LoadData(string itemname)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
            string commandText = "select * from inventory_items";
            if(itemname != string.Empty  )
            {
                commandText = string.Format("select * from inventory_items where item_name like '%{0}%'",itemname)  ; 
            }
            cmd.CommandText = commandText ;
            cmd.Connection = conn ;
            DataSet ds = new DataSet();
            adap.SelectCommand = cmd ;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                adap.Fill(ds);
                lvItemList.DataSource = ds.Tables[0];
                lvItemList.DataBind();
            }
            catch(Exception ex)
            {
                conn.Close();
            }
            
            //lvItemList
              
        }

        protected void lnkBtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory/ITEMS/InventoryAddNew.aspx");
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearchNm.Text);
        }

        protected void lnkAddCategory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Inventory/ITEMS/InventoryCommCode.aspx");
        }
        
    }

}