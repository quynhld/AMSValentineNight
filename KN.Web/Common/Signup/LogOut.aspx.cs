using System;

using KN.Common.Base;
using KN.Common.Base.Code;

namespace KN.Web.Common.Signup
{
    public partial class LogOut : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect(CommValue.PAGE_VALUE_INDEX, false);
        }
    }
}