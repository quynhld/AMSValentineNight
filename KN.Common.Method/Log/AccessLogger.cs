using System;
using System.Web;
using System.Web.UI;

using KN.Common.Base;

namespace KN.Common.Method.Log
{
    public class AccessLogger
    {
        #region MakeLogger : 접속로그 등록

        /// <summary>
        /// 접속로그 등록
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strMemNo">접속자사번</param>
        /// <param name="strUserId">사용자ID</param>
        /// <param name="strPwd">사용비번</param>
        /// <param name="strInsIP">접속IP</param>
        public static void MakeLogger(string strCompCd, string strMemNo, string strUserId, string strPwd, string strInsIP)
        {
            object[] objParams = new object[5];
            Page thisPage = new Page();

            objParams[0] = strCompCd;
            objParams[1] = strMemNo;
            objParams[2] = strUserId;
            objParams[3] = strPwd;
            objParams[4] = strInsIP;

            SPExecute.ExecReturnNo("KN_USP_COMM_INSERT_ACCESSLOG_M00", 1, objParams);
        }

        #endregion
    }
}
