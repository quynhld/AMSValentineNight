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
    public partial class InventoryList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();
        int intPageNo = CommValue.NUMBER_VALUE_0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    CheckParams();
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

            DataSet ds = InventoryBiz.selectAllItem(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), hfStartDt.Value.Replace("-", ""), hfEndDt.Value.Replace("-", ""));
            lvItemList.DataSource = ds.Tables[1];
            lvItemList.DataBind();
            if (ds.Tables[1].Rows.Count > 0)
            {
                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(ds.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));
                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
              
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
        private void CheckParams()
        {
            if (!string.IsNullOrEmpty(hfCurrentPage.Value))
            {
                intPageNo = Int32.Parse(hfCurrentPage.Value);
                hfCurrentPage.Value = intPageNo.ToString();
            }
            else
            {
                intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfCurrentPage.Value = intPageNo.ToString();
            }
        }

        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
        {
            LoadData(txtSearchNm.Text);
        }


    }

}