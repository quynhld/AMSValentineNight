using System;

using KN.Common.Base;
using KN.Common.Base.Code;

using KN.Common.Method.Lib;

namespace KN.Web.Board.Board
{
    public partial class BoardReturn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 메인페이지에서 넘어온 뷰
            if (Session[Master.PARAM_DATA1] != null && Session[Master.PARAM_TRANSFER] != null)
            {
                if (!string.IsNullOrEmpty(Session[Master.PARAM_DATA1].ToString()) &&
                    !string.IsNullOrEmpty(Session[Master.PARAM_TRANSFER].ToString()))
                {
                    string strParams1 = Session[Master.PARAM_DATA1].ToString();
                    string strParams2 = Session[Master.PARAM_DATA2].ToString();
                    string strParams3 = Session[Master.PARAM_DATA3].ToString();
                    string strReturnPage = Session[Master.PARAM_TRANSFER].ToString();

                    if (strParams3.Equals(""))
                    {
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + strParams1 + "&" + Master.PARAM_DATA2 + "=" + strParams2, CommValue.AUTH_VALUE_FALSE);

                        Session[Master.PARAM_DATA1] = null;
                        Session[Master.PARAM_DATA2] = null;
                        Session[Master.PARAM_DATA3] = null;
                        Session[Master.PARAM_TRANSFER] = null;

                        return;
                    }

                    Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + strParams1 + "&" + Master.PARAM_DATA2 + "=" + strParams2 + "&" + Master.PARAM_DATA3 + "=" + strParams3, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                }

                Session[Master.PARAM_DATA1] = null;
                Session[Master.PARAM_DATA2] = null;
                Session[Master.PARAM_DATA3] = null;
                Session[Master.PARAM_TRANSFER] = null;
            }

            //메뉴에서 넘어온 뷰
            else
            {
                string strParams1 = Request.Params[Master.PARAM_DATA1].ToString();
                string strParams2 = Request.Params[Master.PARAM_DATA2].ToString();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + strParams1 + "&" + Master.PARAM_DATA2 + "=" + strParams2, CommValue.AUTH_VALUE_FALSE);
            }
        }
    }
}