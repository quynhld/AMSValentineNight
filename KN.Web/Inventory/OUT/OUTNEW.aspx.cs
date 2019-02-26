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

using KN.Inventory.Biz;

namespace KN.Web.Inventory
{
    public partial class OUTNEW : BasePage
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        string strSelect = string.Empty;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    InitControls();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);// note for make log edit,create write as : user: action(insert,update,delete): date(datetime.now);
            }
        }

        protected void InitControls()
        {
        }

        private void LoadData()
        {

            DataTable dtbOut = new DataTable();
            DataTable dtbOutDetails = new DataTable();
            cmd.Connection = conn;

            if (Request.QueryString["outId"] != null && Request.QueryString["outId"].ToString() != string.Empty)
            {
                //bind out
                dtbOut = InventoryBiz.IVN_OUT_SELECT_BY_ID(Convert.ToInt32(Request.QueryString["outId"]));

                //set text 
                txtUsedFor.Text = dtbOut.Rows[0]["UsedFor"] != null ? dtbOut.Rows[0]["UsedFor"].ToString():string.Empty;
                txtNote.Text = dtbOut.Rows[0]["Note"] != null ? dtbOut.Rows[0]["UsedFor"].ToString() : string.Empty;
                ltBindCreateBy.Text = dtbOut.Rows[0]["CreateUser"].ToString();
                ltCreateDate.Text = dtbOut.Rows[0]["CreateDate"].ToString();
                ltModDate.Text = dtbOut.Rows[0]["ModifyDate"].ToString();
                ltBindModBy.Text = dtbOut.Rows[0]["ModifyUser"].ToString();
                //load details ---KN_USP_IVN_OUT_DETAIL_SELECT_BY_ID

                //bind details
                dtbOutDetails = InventoryBiz.IVN_OUT_DETAIL_SELECT_BY_ID(Convert.ToInt32(Request.QueryString["outId"]));
                lvOutDetails.DataSource = dtbOutDetails;
                lvOutDetails.DataBind();
            }
            else
            {
                lvOutDetails.DataSource = dtbOutDetails;
                lvOutDetails.DataBind();
            }
        }

        //insert Inventory_OUT and OUT DETAILS
        protected void ltAdd_Click(object sender, EventArgs e)
        {
            cmd.Connection = conn;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            Literal txtUnit = (Literal)lvOutDetails.InsertItem.FindControl("txtUnit");
            TextBox txtItemAmount = (TextBox)lvOutDetails.InsertItem.FindControl("txtItemAmount");
            TextBox txtInsertNote = (TextBox)lvOutDetails.InsertItem.FindControl("txtInsertNote");
            DropDownList ddrSelectItems = (DropDownList)lvOutDetails.InsertItem.FindControl("ddrSelectItems");

            int itemID = Convert.ToInt32(ddrSelectItems.SelectedValue);
            decimal amount = Convert.ToDecimal(txtItemAmount.Text);
            string strInsertNote = txtInsertNote.Text;

            //fist check amount of item  not lager than amount remains in database
            string selectRemainAmount =string.Format(  @"SELECT [Item_Amout]  FROM [dbo].[Inventory_Items] where IVN_ID={0}",itemID);
            cmd.CommandText = selectRemainAmount;
            var remainAmount = cmd.ExecuteScalar();
            if (amount > Convert.ToDecimal(remainAmount))
            {
                //show alert that items remain not enought
                string scriptAlert = string.Format( "alert('Not enought amount, remain amount for item is about :{0}')",remainAmount);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", scriptAlert, true);
            }
            else
            {
                try
                {

                    int OUT_ID = 0;
                    //1.insert Inventory_OUT -- if query string exits - only add to outDetails
                    if (Request.QueryString["outId"] == null)
                    {
                        string cmdInsert_Inventory_OUT = @"INSERT INTO [dbo].[Inventory_OUT]
                                                           (
                                                           [CreateDate]
                                                           ,[ModifyDate]
                                                           ,[CreateUser]
                                                           ,[ModifyUser]
                                                           ,[UsedFor]
                                                           ,[Note]
                                                           ,[Status]) OUTPUT INSERTED.[INV_OUT_ID]
                                                     VALUES
                                                           (
                                                            '{0}' --<CreateDate, datetime,>
                                                           ,'{1}' --<ModifyDate, datetime,>
                                                           ,'{2}' --<CreateUser, nvarchar(50),>
                                                           ,'{3}' --<ModifyUser, nvarchar(50),>
                                                           ,'{4}' --<UsedFor, nvarchar(500),>
                                                           ,'{5}' --<Note, nvarchar(2000),>
                                                           ,1 )  --<Status, int,>";
                        string usedFor = txtUsedFor.Text;
                        string note = txtNote.Text;
                        DateTime createDate = DateTime.Now;
                        DateTime modifyDate = DateTime.Now;
                        string createUser = Session["MemNo"].ToString();
                        string modifyUser = Session["MemNo"].ToString();

                        cmd.CommandText = string.Format(cmdInsert_Inventory_OUT, createDate, modifyDate, createUser, modifyUser, usedFor, note);
                        OUT_ID = (int)cmd.ExecuteScalar();//get id return
                    }
                    else
                    {
                        OUT_ID = Convert.ToInt32(Request.QueryString["outId"]);
                    }
                    //2.insert Inventory_OUT_DETAILS
                    string cmdInsert_Inventory_OUT_Details = @"INSERT INTO [dbo].[Inventory_OUT_Details]
                                                                           ([INV_OUT_ID]
                                                                           ,[INV_ID]
                                                                           ,[Amount]
                                                                           ,[Note]
                                                                           ,[status])
                                                                     VALUES
                                                                           ({0} --<INV_OUT_ID, int,>
                                                                           ,{1} --<INV_ID, int,>
                                                                           ,{2} --<Amount, int,>
                                                                           ,'{3}' --<Note, nvarchar(500),>
                                                                           ,{4}) --<status, bit,>";
                    //read values from insert item template

                    cmd.CommandText = string.Format(cmdInsert_Inventory_OUT_Details, OUT_ID, itemID, amount, strInsertNote, 1);
                    cmd.ExecuteNonQuery();

                    //update amount of items
                    cmd.CommandText = @"update Inventory_Items set Item_Amout = Item_Amout - " + amount + " where IVN_ID =" + itemID.ToString();
                    cmd.ExecuteNonQuery();

                    //3.reload data
                    Response.Redirect(string.Format("~/Inventory/OUT/OUTNEW.aspx?outId={0}", OUT_ID));
                }
                catch (Exception ex)
                {
                    conn.Close();
                }
            }
        }

        protected void lvOutDetails_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if(e.Item.ItemType == ListViewItemType.InsertItem)
            {
                DataTable dtb = new DataTable();
                cmd.CommandText = @"SELECT [IVN_ID]
                                      ,[Item_Name]
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
                                      ,[ItemUnit]
                                      ,[CreateDate]
                                      ,[CreateBy]
                                      ,[ModDate]
                                      ,[ModBy]
                                  FROM [dbo].[Inventory_Items]";
                cmd.Connection = conn;

                adapter.SelectCommand = cmd;
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    adapter.Fill(dtb);
                }
                catch (Exception ex)
                {
                    conn.Close();
                }
                //bind data

                DropDownList dllSelectItem = (e.Item.FindControl("ddrSelectItems") as DropDownList);
                dllSelectItem.DataSource = dtb;
                dllSelectItem.DataTextField = "Item_Name";
                dllSelectItem.DataValueField = "IVN_ID";
                dllSelectItem.DataBind();

                Literal txtUnit = (Literal)lvOutDetails.InsertItem.FindControl("txtUnit");
                TextBox txtItemAmount = (TextBox)lvOutDetails.InsertItem.FindControl("txtItemAmount");
                TextBox txtInsertNote = (TextBox)lvOutDetails.InsertItem.FindControl("txtInsertNote");
                DropDownList ddrSelectItems = (DropDownList)lvOutDetails.InsertItem.FindControl("ddrSelectItems");
                txtUnit.Text = dtb.Rows[ddrSelectItems.SelectedIndex]["ItemUnit"].ToString();
                
            }
        }

        protected void ddrSelectItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtb = new DataTable();
                cmd.CommandText = @"SELECT [IVN_ID]
                                      ,[Item_Name]
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
                                      ,[ItemUnit]
                                      ,[CreateDate]
                                      ,[CreateBy]
                                      ,[ModDate]
                                      ,[ModBy]
                                  FROM [dbo].[Inventory_Items]";
                cmd.Connection = conn;

                adapter.SelectCommand = cmd;
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    adapter.Fill(dtb);
                }
                catch (Exception ex)
                {
                    conn.Close();
                }
                DropDownList dllSelectItem = (sender as DropDownList);
                Literal txtInsertUnit = (Literal)lvOutDetails.InsertItem.FindControl("txtUnit");
                txtInsertUnit.Text = dtb.Rows[dllSelectItem.SelectedIndex - 1]["ItemUnit"].ToString();
            }
            catch (Exception ex)
            { }

        }

        protected void ltRemove_Click(object sender, EventArgs e)
        {
            
        }

        protected void lvOutDetails_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if(e.CommandName=="Remove")
            {
                if(conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                e.Item.Visible = false;
                //remove in database
                int outDetailsId = Convert.ToInt32( ((Literal)e.Item.FindControl("ltOutDetailID")).Text);
                int itemID = Convert.ToInt32(((Literal)e.Item.FindControl("ltItemID")).Text); 
                decimal amoutRemove = Convert.ToDecimal(((Literal)e.Item.FindControl("ltItemAmount")).Text);
                cmd.CommandText = @"Delete from Inventory_OUT_Details where ID ="+outDetailsId.ToString();
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                //update amout of item in Inventory_Items
                cmd.CommandText = @"update Inventory_Items set Item_Amout = Item_Amout + " + amoutRemove + " where IVN_ID ="+itemID.ToString();
                cmd.ExecuteNonQuery();
                conn.Close();
                //reload()
                LoadData();
            }
        }
    }
}