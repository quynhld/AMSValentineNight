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

using KN.Inventory;
using KN.Inventory.Biz;

namespace KN.Web.Inventory
{
    public partial class InventoryDetails : BasePage
    {
        StringBuilder sbInPageNavi = new StringBuilder();
        StringBuilder sbOutPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();
        
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
            InventoryBiz.updateItem(getParameterForUpdateCommand());
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
            insertParams[16] = true;//get from dropdownlist
            insertParams[17] = txtIVTModel.Text;
            insertParams[18] = txtIVTUnit.Text;
            insertParams[19] = DateTime.Now;
            insertParams[20] = strInsMemIP;

            insertParams[21] = Request.QueryString["ID"];

            string inventoryID = "";
            if (fuIvtImage.HasFile)
            {
                try
                {
                    string strImgFilePath = Server.MapPath(string.Format("~//InventoryImg//{0}", fuIvtImage.FileName));
                    fuIvtImage.SaveAs(strImgFilePath);
                    insertParams[11] = "~//InventoryImg//" + fuIvtImage.FileName;
                }
                catch (Exception ex)
                {
                    string strErr = ex.Message;
                }
            }
            else
            {
                insertParams[11] = IvtImage.ImageUrl;
            }
            return insertParams;
        }
        private void loadData(string id)
        {
            bindata(InventoryBiz.selectOneItem(Convert.ToInt32(id)));
        }

        private void loadInData(string id)
        {
            dtInInfo = InventoryBiz.INV_IN_SELECT_PAGING_BY_ITEMID(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfInCurrentPage.Value), id);
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
            dtOutInfo = InventoryBiz.INV_OUT_SELECT_PAGING_BY_ITEMID(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfOutCurrentPage.Value), id);
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
            txtIvtWide.Text = (Item.Rows[0]["Item_Size_L"] == null ? string.Empty : Item.Rows[0]["Item_Size_L"].ToString());
            txtIvtWidth.Text = (Item.Rows[0]["Item_Size_W"] == null ? string.Empty : Item.Rows[0]["Item_Size_W"].ToString());
            txtIvtZone.Text = (Item.Rows[0]["Item_LC_Zone"] == null ? string.Empty : Item.Rows[0]["Item_LC_Zone"].ToString());
            IvtImage.ImageUrl = (Item.Rows[0]["Item_Photo"] == null ? string.Empty :  Item.Rows[0]["Item_Photo"].ToString());
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