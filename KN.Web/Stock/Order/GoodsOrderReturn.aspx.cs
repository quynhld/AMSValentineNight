using System;

using KN.Common.Base;
using KN.Common.Base.Code;

using KN.Common.Method.Lib;

namespace KN.Web.Stock.Order
{
    public partial class GoodsOrderReturn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 재고 목록에서 넘어온 발주 요청건 처리
            if (Session[Master.PARAM_DATA1] != null && Session[Master.PARAM_TRANSFER] != null)
            {
                if (!string.IsNullOrEmpty(Session[Master.PARAM_DATA1].ToString()) &&
                    !string.IsNullOrEmpty(Session[Master.PARAM_TRANSFER].ToString()))
                {
                    string strParams = Session[Master.PARAM_DATA1].ToString();
                    string strReturnPage = Session[Master.PARAM_TRANSFER].ToString();

                    Session[Master.PARAM_DATA1] = null;
                    Session[Master.PARAM_TRANSFER] = null;

                    Response.Redirect(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=" + strParams, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                }
            }
            else
            {
                // 메모에서 넘어온 발주상세보기건 처리
                if (Session[Master.PARAM_DATA2] != null)
                {
                    if (!string.IsNullOrEmpty(Session[Master.PARAM_DATA2].ToString()))
                    {
                        string strParams = Session[Master.PARAM_DATA2].ToString();

                        Session[Master.PARAM_DATA2] = null;

                        Response.Redirect(Master.PAGE_VIEW + "?" + Master.PARAM_DATA1 + "=" + strParams, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                    }
                }
                else
                {
                    Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                }
            }
        }
    }
}
