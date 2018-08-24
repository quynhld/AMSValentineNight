using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace KN.Web.Inventory.IN
{
    public partial class InDetails : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adap = new SqlDataAdapter();
        string strA = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            string str = string.Empty;
            try
            {
                if (!IsPostBack)
                {
                    LoadData(txtSearchNm.Text);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }


        private void LoadData(string itemname)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;

            string commandText = "select * from Inventory_IN_Details";
            string id = Request.QueryString["ID"];

            if (itemname != string.Empty)
            {
                commandText = string.Format("select * from Inventory_IN_Details where INV_IN_ID like '%{0}%'", itemname);
            }
            if (id != null)
            {
                commandText = string.Format("select * from Inventory_IN_Details where INV_IN_ID like '%{0}%'", id);
            }
            cmd.CommandText = commandText;
            cmd.Connection = conn;
            DataSet ds = new DataSet();
            adap.SelectCommand = cmd;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                adap.Fill(ds);
                lvPaymentList.DataSource = ds.Tables[0];
                lvPaymentList.DataBind();
            }
            catch (Exception ex)
            {
                conn.Close();
            }


        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("..//IN//DetailsModified.aspx?editIdDetails=&status=0");
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearchNm.Text);
        }
    }
}