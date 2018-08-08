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
using KN.Settlement.Biz;
using System.Configuration ;
namespace KN.Web.Inventory
{
    public partial class OUTList : BasePage
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        string strSelect = string.Empty;
        SqlDataAdapter adapter = new SqlDataAdapter();
        //conn.ConnectionString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
        DataSet ds = new DataSet();

        int intPageNo = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    InitControls();
                }
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
        }
       
        private void LoadData()
        {
            cmd.CommandText = @"SELECT [INV_OUT_ID]
                                  ,[CreateDate]
                                  ,[ModifyDate]
                                  ,[CreateUser]
                                  ,[ModifyUser]
                                  ,[UsedFor]
                                  ,[Note]
                                  ,[Status]
                              FROM [dbo].[Inventory_OUT]";
            cmd.Connection = conn;

            adapter.SelectCommand = cmd;
            try
            {
                if(conn.State != ConnectionState.Open )
                {
                    conn.Open();
                }
                adapter.Fill(ds);
                lvLstOUT.DataSource = ds.Tables[0];
                lvLstOUT.DataBind();
            }
            catch(Exception ex)
            {
                conn.Close();
            }
        }
        
    }
}