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
    public partial class InventoryDetails : BasePage
    {
        StringBuilder sbInPageNavi = new StringBuilder();
        StringBuilder sbOutPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TempDBConnection"].ToString());
        public string DATA_APT = CommValue.RENTAL_VALUE_APT;
        public string DATA_APTSTORE = CommValue.RENTAL_VALUE_APTSHOP;
        string strAA = string.Empty;
        string strIvnID = string.Empty;

        DataTable dtItemInfo = new DataTable();
        DataSet dtInInfo = new DataSet();
        DataSet dtOutInfo = new DataSet();

        int intInPageNo = CommValue.NUMBER_VALUE_0;
        int intOutPageNo = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    CheckInParams();
                    CheckOutParams();
                    InitControls();
                    if (Request.QueryString["ID"] != null)
                    {
                        strIvnID = Request.QueryString["ID"].ToString();
                        loadData(strIvnID);
                        loadInData(strIvnID);
                        loadOutData(strIvnID);
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
            string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
            try
            {
                string strInsert = @"UPDATE [dbo].[Inventory_Items] SET

                                                   [Item_Name] = '{0}' 
                                                   ,[Item_EName] = '{1}' 
                                                   ,[Item_Type] = '{2}'
                                                   ,[Item_LC_Area] = '{3}' 
                                                   ,[Item_LC_Zone] = '{4}'
                                                   ,[Item_LC_No] = '{5}' 
                                                   ,[Item_Size_W] = {6} 
                                                   ,[Item_Size_H] = {7} 
                                                   ,[Item_Size_Wide] = {8}
                                                   ,[Item_Size_Ra] = {9}
                                                   ,[Item_Amout] = {10}
                                                   ,[Item_Photo] = '{11}'
                                                   ,[Item_owner] = '{12}'   
                                                   ,[Item_owner_ID] = {13} 
                                                   ,[Item_Group_ID] = {14} 
                                                   ,[Item_Group_Name] = '{15}' 
                                                   ,[Item_Status] = 0
                                                   ,[Item_Model] = '{16}'
                                                    ,[ModDate] = '{18}'
                                                    ,[ModBy] = '{19}'
                                             WHERE IVN_ID = {17}
                                                  ";               

                object[] UpdateParams = new object[20];
                UpdateParams[0] = txtIVTViName.Text;
                UpdateParams[1] = txtIVTEngName.Text;
                UpdateParams[2] = txtIVTCategory.Text;
                UpdateParams[3] = txtIvtArea.Text;

                UpdateParams[4] = txtIvtZone.Text;
                UpdateParams[5] = txtIvtNo.Text;

                decimal height = 0;
                if (txtIvtHeight.Text != string.Empty)
                {
                    height = Convert.ToDecimal(txtIvtHeight.Text);
                }
                UpdateParams[6] = height;

                decimal width = 0;
                if (txtIvtWidth.Text != string.Empty)
                {
                    width = Convert.ToDecimal(txtIvtWidth.Text);
                }
                UpdateParams[7] = width;

                decimal wide = 0;
                if (txtIvtWide.Text != string.Empty)
                {
                    wide = Convert.ToDecimal(txtIvtWide.Text);
                }
                UpdateParams[8] = wide;

                decimal radius = 0;
                if (txtIvtRadius.Text != string.Empty)
                {
                    radius = Convert.ToDecimal(txtIvtRadius.Text);
                }
                UpdateParams[9] = radius;

                int quantity = 0;
                if (txtIvtQuantity.Text != string.Empty)
                {
                    quantity = Convert.ToInt32(txtIvtQuantity.Text);
                }
                UpdateParams[10] = quantity;

                UpdateParams[11] = "";
                UpdateParams[12] = "";
                UpdateParams[13] = 0;
                UpdateParams[14] = 0;
                UpdateParams[15] = "";
                UpdateParams[16] = txtIVTModel.Text;
                UpdateParams[17] = strIvnID;
                UpdateParams[18] = DateTime.Now;
                UpdateParams[19] = strInsMemIP;


                string inventoryID = "";
                if (fuIvtImage.HasFile)
                {
                    try
                    {
                        string strImgFilePath = Server.MapPath(string.Format("~//InventoryImg//{0}", fuIvtImage.FileName));
                        fuIvtImage.SaveAs(strImgFilePath);
                        UpdateParams[11] = fuIvtImage.FileName;
                    }
                    catch (Exception ex)
                    {
                        string strErr = ex.Message;
                    }
                }

                strInsert = string.Format(strInsert, UpdateParams);
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

        private void loadData(string id)
        {
            //get items info
            string strGetItemInfo = string.Format("select * from Inventory_Items where IVN_ID={0}",id);
            SqlCommand cmdgetItemInfo = new System.Data.SqlClient.SqlCommand(strGetItemInfo);
            cmdgetItemInfo.Connection = conn;
            SqlDataAdapter ItemAdapInf = new System.Data.SqlClient.SqlDataAdapter();
            ItemAdapInf.SelectCommand = cmdgetItemInfo;

            try
            { 
                if(conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                ItemAdapInf.Fill(dtItemInfo);
                bindata(dtItemInfo);
            }
            catch(Exception ex)
            {
                conn.Close();
            }
        }

        private void loadInData(string id)
        {
            //get in info
            string getInInfo = string.Format(selectItemIn(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfInCurrentPage.Value), id));
            SqlCommand cmdGetInInfo = new System.Data.SqlClient.SqlCommand(getInInfo);
            cmdGetInInfo.Connection = conn;
            SqlDataAdapter InAdap = new System.Data.SqlClient.SqlDataAdapter();
            InAdap.SelectCommand = cmdGetInInfo;
            InAdap.Fill(dtInInfo);

            lsvIN.DataSource = dtInInfo.Tables[1];
            lsvIN.DataBind();
            if (dtInInfo.Tables[1].Rows.Count > 0)
            {
                sbInPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfInCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dtInInfo.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));
                spanInPageNavi.InnerHtml = sbInPageNavi.ToString();
            }
        }

        private void loadOutData(string id)
        {
            //get out info
            string getOutInfo = string.Format(selectItemOut(id, CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfOutCurrentPage.Value)));
            SqlCommand cmdGetOutInfo = new System.Data.SqlClient.SqlCommand(getOutInfo);
            cmdGetOutInfo.Connection = conn;
            SqlDataAdapter OutAdap = new System.Data.SqlClient.SqlDataAdapter();
            OutAdap.SelectCommand = cmdGetOutInfo;

            OutAdap.Fill(dtOutInfo);

            lsvOut.DataSource = dtOutInfo.Tables[1];
            lsvOut.DataBind();
            if (dtOutInfo.Tables[1].Rows.Count > 0)
            {
                sbOutPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfOutCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dtOutInfo.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));
                spanOutPageNavi.InnerHtml = sbOutPageNavi.ToString();
            }
        }

        public string selectItemIn( int pageSize, int pageNow,string ID)
        {
            string str = @"
	                        DECLARE
	                         @intPageSize		INT
	                        ,@intNowPage		INT
	                        ,@strStartDt		VARCHAR(8)
	                        ,@strEndDt			VARCHAR(8)
                            ,@StrID             varchar(20)

	                        SET @intPageSize	= {0} -- pagesize
	                        SET @intNowPage		= {1} -- pagenow
                            set @strID          = '{2}'

	                        SELECT  COUNT(*) AS TotalCnt
		                        FROM  dbo.Inventory_IN AS A
		                        WHERE  1 = 1
                                AND IVN_IN_ID in (select INV_IN_ID from Inventory_IN_Details where IVN_ID =  @strID)
	                        ;WITH LogList
	                        AS 
	                        (
	                        SELECT  ROW_NUMBER() OVER (ORDER BY A.[CreateDate] DESC) AS RealSeq
			                        ,A.[IVN_IN_ID]
		                            ,A.[CreateDate]
                                    ,A.CreateUser
		                            ,A.[ModifyDate]
		                            ,A.[ModifyUser]
		                            ,A.[UsedFor]
		                            ,A.[Note]
		                            ,A.[Status]
		                        FROM  dbo.Inventory_IN AS A
		                        WHERE  1 = 1
                                AND IVN_IN_ID in (select INV_IN_ID from Inventory_IN_Details where IVN_ID =  @strID)
	                        )
	                        SELECT  X.RealSeq
		                            ,X.IVN_IN_ID
                                    ,X.[CreateDate]
                                    ,X.CreateUser
		                            ,X.[ModifyDate]
		                            ,X.[ModifyUser]
		                            ,X.[UsedFor]
		                            ,X.[Note]
		                            ,X.[Status]
		                        FROM  LogList AS X
		                        WHERE  1 = 1
		                        AND  X.RealSeq <= @intNowPage * @intPageSize
		                        AND  X.RealSeq >= (@intNowPage-1) * @intPageSize + 1";
            return string.Format(str, pageSize, pageNow,ID);
        }

        public string selectItemOut(string ID, int pageSize, int pageNow)
        {
            string str = @"
	                        DECLARE
	                         @intPageSize		INT
	                        ,@intNowPage		INT
	                        ,@strStartDt		VARCHAR(8)
	                        ,@strEndDt			VARCHAR(8)
                            ,@StrID             varchar(20)

	                        SET @intPageSize	= {0} -- pagesize
	                        SET @intNowPage		= {1} -- pagenow
                            set @strID          = '{2}'

	                        SELECT  COUNT(*) AS TotalCnt
		                        FROM  dbo.Inventory_OUT AS A
		                        WHERE  1 = 1
                                AND INV_OUT_ID in (select INV_OUT_ID from Inventory_OUT_Details where INV_ID = @StrID)	 		
	                        ;WITH LogList
	                        AS 
	                        (
	                        SELECT  ROW_NUMBER() OVER (ORDER BY A.[CreateDate] DESC) AS RealSeq
			                        ,A.[INV_OUT_ID]
		                            ,A.[CreateDate]
                                    ,A.CreateUser
		                            ,A.[ModifyDate]
		                            ,A.[ModifyUser]
		                            ,A.[UsedFor]
		                            ,A.[Note]
		                            ,A.[Status]
		                        FROM  dbo.Inventory_OUT AS A
		                        WHERE  1 = 1
                                AND INV_OUT_ID in (select INV_OUT_ID from Inventory_OUT_Details where INV_ID = @StrID)
	                        )
	                        SELECT  X.RealSeq
		                            ,X.INV_OUT_ID
                                    ,X.[CreateDate]
                                    ,X.CreateUser
		                            ,X.[ModifyDate]
		                            ,X.[ModifyUser]
		                            ,X.[UsedFor]
		                            ,X.[Note]
		                            ,X.[Status]
		                        FROM  LogList AS X
		                        WHERE  1 = 1
		                        AND  X.RealSeq <= @intNowPage * @intPageSize
		                        AND  X.RealSeq >= (@intNowPage-1) * @intPageSize + 1";
            return string.Format(str, pageSize, pageNow,ID);
        }

        private void bindata(DataTable Item)
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
            txtIvtWide.Text = (Item.Rows[0]["Item_Size_Wide"] == null ? string.Empty : Item.Rows[0]["Item_Size_Wide"].ToString());
            txtIvtWidth.Text = (Item.Rows[0]["Item_Size_W"] == null ? string.Empty : Item.Rows[0]["Item_Size_W"].ToString());
            txtIvtZone.Text = (Item.Rows[0]["Item_LC_Zone"] == null ? string.Empty : Item.Rows[0]["Item_LC_Zone"].ToString());
            IvtImage.ImageUrl = (Item.Rows[0]["Item_Photo"] == null ? string.Empty : Item.Rows[0]["Item_Photo"].ToString());
        }

        protected void imgbtnOutPageMove_Click(object sender, ImageClickEventArgs e)
        {
            loadOutData(Request.QueryString["ID"].ToString());
        }

        protected void imgbtnInPageMove_Click(object sender, ImageClickEventArgs e)
        {
            loadInData(Request.QueryString["ID"].ToString());
        }

        private void CheckInParams()
        {
            if (!string.IsNullOrEmpty(hfInCurrentPage.Value))
            {
                intInPageNo = Int32.Parse(hfInCurrentPage.Value);
                hfInCurrentPage.Value = intInPageNo.ToString();
            }
            else
            {
                intInPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfInCurrentPage.Value = intInPageNo.ToString();
            }
        }

        private void CheckOutParams()
        {
            if (!string.IsNullOrEmpty(hfOutCurrentPage.Value))
            {
                intOutPageNo = Int32.Parse(hfOutCurrentPage.Value);
                hfOutCurrentPage.Value = intOutPageNo.ToString();
            }
            else
            {
                intOutPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfOutCurrentPage.Value = intOutPageNo.ToString();
            }
        }
    }

}