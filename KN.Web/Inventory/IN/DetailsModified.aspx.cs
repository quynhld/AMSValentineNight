using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KN.Web.Inventory.IN
{
    public partial class DetailsModified : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adap = new SqlDataAdapter();

        string editIdDetails = null;
        string status = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    editIdDetails = Request.QueryString["editIdDetails"].ToString();
                    status = Request.QueryString["status"].ToString();

                    if (editIdDetails != null && status.Equals("0"))
                    {
                        loadData(editIdDetails);
                    }
                    if (editIdDetails != null && status.Equals("1"))
                    {
                        delete(editIdDetails);

                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void delete(string editIdIn)
        {
            throw new NotImplementedException();
        }

        private void loadData(string editIdDetails)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;

            string commandText1 = "select dbo.Inventory_IN.IVN_IN_ID from Inventory_IN ";
            string commandText2 = "select dbo.Inventory_Items.IVN_ID from Inventory_Items ";
            string commandText3 = "select * from dbo.Inventory_IN_Details order by INV_IN_ID asc ";

            string commandText = commandText1 + commandText2 + commandText3;

            try
            {
                cmd.Connection = conn;
                cmd.CommandText = commandText;
                adap.SelectCommand = cmd;
                conn.Open();

                DataSet ds = new DataSet();
                adap.Fill(ds);

                DataTable Inventory_IN = ds.Tables[0];
                DataTable Inventory_Items = ds.Tables[1];
                DataTable Inventory_IN_Details = ds.Tables[2];

                ddlInvInId.DataSource = Inventory_IN;
                ddlInvInId.DataTextField = "IVN_IN_ID"; //Text hiển thị
                ddlInvInId.DataValueField = "IVN_IN_ID"; //Giá trị khi chọn
                ddlInvInId.DataBind();

                ddlIvnId.DataSource = Inventory_Items;
                ddlIvnId.DataTextField = "IVN_ID";
                ddlIvnId.DataValueField = "IVN_ID";
                ddlIvnId.DataBind();





            }
            catch (Exception ex)
            {
                conn.Close();
            }

        }

        protected void btnSave(object sender, EventArgs e)
        {

        }
    }
}