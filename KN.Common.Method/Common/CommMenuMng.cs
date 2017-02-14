using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Common.Method.Common
{
    /// <summary>
    /// 공통영역 : 로그인 및 메뉴 관련 처리
    /// </summary>
    public class CommMenuMng
    {
        #region SelectMemInfo : 회원정보 조회 (공통영역)

        /**********************************************************************************************
         * Mehtod   명 : SelectMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 회원정보 조회
         * Input    값 : SelectMemInfo(소속기업코드, 회원ID, 비밀번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMemInfo : 메뉴 조회 (공통영역)
        /// </summary>
        /// <param name="strCompCd">소속기업코드</param>
        /// <param name="strUserId">회원ID</param>
        /// <param name="strPwd">비밀번호</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectMemInfo(string strCompCd, string strUserId, string strPwd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[3];

            objParam[0] = TextLib.StringEncoder(strCompCd);
            objParam[1] = TextLib.StringEncoder(strUserId);
            objParam[2] = TextLib.StringEncoder(strPwd);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMenuMng : 메뉴 조회 (공통영역)

        /**********************************************************************************************
         * Mehtod   명 : SelectMenuMng 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 조회
         * Input    값 : SelectMenuMng(언어코드, 접속권한, 현재메뉴경로)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectMenuMng : 메뉴 조회 (공통영역)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strAccessAuth">접속권한</param>
        /// <param name="strMenuUrl">현재메뉴경로</param>
        /// <returns>DataSet</returns>
        public static DataSet SelectMenuMng(string strLangCd, string strAccessAuth, string strMenuUrl, int intMenuSeq)
        {
            DataSet dsReturn = new DataSet();
            object[] objParam = new object[4];

            objParam[0] = strLangCd;
            objParam[1] = strAccessAuth;
            objParam[2] = strMenuUrl;
            objParam[3] = intMenuSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_COMM_SELECT_MENUMNG_S00", objParam);

            return dsReturn;
        }

        #endregion
    }
}
