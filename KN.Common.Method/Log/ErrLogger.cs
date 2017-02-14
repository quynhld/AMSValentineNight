using System;
using System.Web;
using System.Web.UI;

using KN.Common.Base;

namespace KN.Common.Method.Log
{
    /// <summary>
    /// 오류사항 처리 클래스
    /// </summary>
    public class ErrLogger
    {
        #region MakeLogger : Default 에러처리 등록 (소스 에러)
        /// <summary>
        /// 에러처리 등록
        /// </summary>
        /// <param name="e">에러 메세지</param>
        public static void MakeLogger(Exception e)
        {
            object[] objParams = new object[5];
            Page thisPage = new Page();

            objParams[0] = "0001";
            if (thisPage.Session["NowPageUrl"] != null)
            {
                objParams[1] = thisPage.Session["NowPageUrl"].ToString();
            }
            else
            {
                objParams[1] = e.Source;
            }
            objParams[2] = e.StackTrace;
            objParams[3] = e.Message;
            objParams[4] = HttpContext.Current.Server.MachineName;

            if (!objParams[1].ToString().Equals("mscorlib"))
            {
                SPExecute.ExecReturnNo("KN_USP_COMM_INSERT_LOG_M00", 1, objParams);
            }
        }
        #endregion

        #region MakeLogger : 에러처리 등록
        /// <summary>
        /// 에러처리 등록
        /// </summary>
        /// <param name="e">에러 메세지</param>
        /// <param name="strType">에러 발생 타입</param>
        /// <value>0001 : 소스 에러</value>
        /// <value>0002 : DB 에러</value>
        public static void MakeLogger(Exception e, string strType)
        {
            object[] objParams = new object[5];
            Page thisPage = new Page();

            objParams[0] = strType;
            if (thisPage.Session["NowPageUrl"] != null)
            {
                objParams[1] = thisPage.Session["NowPageUrl"].ToString();
            }
            else
            {
                objParams[1] = e.Source;
            }
            objParams[2] = e.StackTrace;
            objParams[3] = e.Message;
            objParams[4] = e.TargetSite.ToString();

            if (!objParams[1].ToString().Equals("mscorlib"))
            {
                SPExecute.ExecReturnNo("KN_USP_COMM_INSERT_LOG_M00", 1, objParams);
            }
        }
        #endregion
    }
}
