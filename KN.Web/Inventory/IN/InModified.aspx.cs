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
    public partial class InModified : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adap = new SqlDataAdapter();

        string editIdIn = null;
        string status = null;

        DataTable InTable = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

            AuthCheckLib.CheckSession();
            
            try
            {
                if (!IsPostBack)
                {
                    editIdIn = Request.QueryString["editIdIn"].ToString();
                    status = Request.QueryString["status"].ToString();
                    if (editIdIn != null && status.Equals("0"))
                    {
                        loadData(editIdIn);
                    }
                    if (editIdIn != null && status.Equals("1"))
                    {
                        delete(editIdIn);
                        Response.Redirect("..//IN//InList.aspx");

                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        public void loadData(string id)
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;

            string commandText = string.Format("select * from Inventory_IN where IVN_IN_ID={0}", id);

            cmd.CommandText = commandText;
            cmd.Connection = conn;

            adap.SelectCommand = cmd;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                adap.Fill(InTable);
                Binddata(InTable);

            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        public void Binddata(DataTable intable)
        {
            txtCreateUser.Text = (intable.Rows[0]["CreateUser"] == null ? string.Empty : intable.Rows[0]["CreateUser"].ToString());
            txtModifyUser.Text = (intable.Rows[0]["ModifyUser"] == null ? string.Empty : intable.Rows[0]["ModifyUser"].ToString());
            txtUsedFor.Text = (intable.Rows[0]["UsedFor"] == null ? string.Empty : intable.Rows[0]["UsedFor"].ToString());
            txtNote.Text = (intable.Rows[0]["Note"] == null ? string.Empty : intable.Rows[0]["Note"].ToString());
        }

        protected void btnSave(object sender, EventArgs e)
        {
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
                cmd.Connection = conn;

                string cmdStr = string.Empty;

                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                editIdIn = Request.QueryString["editIdIn"].ToString();

                if (editIdIn == null)
                {
                    cmdStr = string.Format(stringInsertCommand(), getParameter());
                    cmd.CommandText = cmdStr;
                    cmd.ExecuteScalar();
                }
                else
                {
                    cmdStr = string.Format(stringUpdateCommand(editIdIn), getParameter());
                    cmd.CommandText = cmdStr;
                    cmd.ExecuteNonQuery();
                }


            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }

        private void delete(string id)
        {
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
                cmd.Connection = conn;
                string cmdStr = "DELETE FROM [dbo].[Inventory_IN] WHERE [IVN_IN_ID] = N'{0}'";
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                cmdStr = string.Format(cmdStr, id);
                cmd.CommandText = cmdStr;
                cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                conn.Close();
            }
        }
        private string stringUpdateCommand(string id)
        {
            string strUpdate = string.Empty;
            strUpdate = @"UPDATE [dbo].[Inventory_IN] "
                + "SET [ModifyDate] = GETDATE(), [CreateUser] = N'{0}', [ModifyUser] = N'{1}', "
                + "[UsedFor] = N'{2}', [Note] = N'{3}', [Status] = N'{4}' "
                + "WHERE [IVN_IN_ID] = " + id;
            return strUpdate;
        }

        private string stringInsertCommand()
        {
            string strInsert = string.Empty;
            strInsert = @"INSERT INTO [dbo].[Inventory_IN]([CreateDate],[ModifyDate],[CreateUser],[ModifyUser],[UsedFor],[Note],[Status])"
                + "VALUES(GETDATE(),GETDATE(),N'{0}',N'{1}',N'{2}',N'{3}',N'{4}')";

            return strInsert;
        }

        private object[] getParameter()
        {

            object[] insertParams = new object[5];
            insertParams[0] = txtCreateUser.Text;
            insertParams[1] = txtModifyUser.Text;
            insertParams[2] = txtUsedFor.Text;
            insertParams[3] = txtNote.Text;

            insertParams[4] = "";


            return insertParams;
        }
    }
}
