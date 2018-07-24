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

namespace KN.Web.Inventory
{
    public partial class INList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        public string DATA_APT = CommValue.RENTAL_VALUE_APT;
        public string DATA_APTSTORE = CommValue.RENTAL_VALUE_APTSHOP;

        int intPageNo = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
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

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();
            DataSet dsReturn = new DataSet();
        }

        protected void lnkAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("..//IN//INNEW.aspx");
        }

    }
}