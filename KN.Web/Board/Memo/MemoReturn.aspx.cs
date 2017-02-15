using System;

using KN.Common.Base;
using KN.Common.Base.Code;

using KN.Common.Method.Lib;

namespace KN.Web.Board.Memo
{
    public partial class MemoReturn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 메인페이지에서 넘어온 쪽지
            if (Session[Master.PARAM_DATA1] != null && Session[Master.PARAM_TRANSFER] != null)
            {
                if (!string.IsNullOrEmpty(Session[Master.PARAM_DATA1].ToString()) &&
                    !string.IsNullOrEmpty(Session[Master.PARAM_TRANSFER].ToString()))
                {
                    string strParams1 = Session[Master.PARAM_DATA1].ToString();
                    string strReturnPage = Session[Master.PARAM_TRANSFER].ToString();

                    Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + strParams1, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                }

                Session[Master.PARAM_DATA1] = null;
                Session[Master.PARAM_TRANSFER] = null;

            }
            else
            {
                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
        }
    }
}