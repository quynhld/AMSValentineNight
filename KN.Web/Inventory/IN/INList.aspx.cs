using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Inventory.Biz;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace KN.Web.Inventory
{
    public partial class INList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();
        int intPageNo = CommValue.NUMBER_VALUE_0;
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();
                        try
            {
                if (!IsPostBack)
                {
                    CheckParams();
                    LoadData();
                                    }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
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

        private void LoadData()
        {
            
            ds = InventoryBiz.INV_IN_SELECT_PAGING_ALL(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), hfStartDt.Value.Replace("-", ""), hfEndDt.Value.Replace("-", ""));
            lvLstIN.DataSource = ds.Tables[1];
            lvLstIN.DataBind();
            if (ds.Tables[1].Rows.Count > 0)
            {
                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(ds.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));
                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        
        protected void imgbtnPageMove_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            LoadData();
        }

    }
}