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
using System.Configuration;

using KN.Settlement.Biz;
using KN.Inventory.Biz;

namespace KN.Web.Inventory
{
    public partial class InventoryCommCode : BasePage
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
        protected void InitControls()
        {
        }
        private void LoadData(string itemname)
        {
            int pageSize = 10, nowPage = 1;
            DataSet ds = new DataSet();
            ds = InventoryBiz.selectCategory(pageSize, nowPage);
            lvItemList.DataSource = ds.Tables[1];
            lvItemList.DataBind();
            if (ds.Tables[1].Rows.Count > 0)
            {
                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(ds.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));
                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData(txtSearchNm.Text);
        }

        protected void lnkBtnAdd_Click1(object sender, EventArgs e)
        {
            InventoryBiz.insertCategory("", txtGroupName.Text, "", txtTypeName.Text, "", txtSubTypeName.Text);
            LoadData(txtSearchNm.Text);
        }

        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
        {

        }

    }

}