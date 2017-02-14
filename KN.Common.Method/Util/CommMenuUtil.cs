using System.Data;

using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    /// <summary>
    /// 공통영역 : 로그인 및 메뉴 관련 처리
    /// </summary>
    public class CommMenuUtil
    {
        #region SpreadMemInfo : 회원정보 조회 (공통영역)

        /**********************************************************************************************
         * Mehtod   명 : SpreadMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 회원정보 조회 (공통영역)
         * Input    값 : SpreadMenuMng(소속기업코드, 회원ID, 비밀번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMemInfo : 회원정보 조회 (공통영역)
        /// </summary>
        /// <param name="strCompCd">소속기업코드</param>
        /// <param name="strUserId">회원ID</param>
        /// <param name="strPwd">비밀번호</param>
        /// <returns>DataTable</returns>
        public static DataTable SpreadMemInfo(string strCompCd, string strUserId, string strPwd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommMenuMng.SelectMemInfo(strCompCd, strUserId, strPwd);

            return dtReturn;
        }

        #endregion

        #region SpreadMenuMng : 메뉴 조회 (공통영역)

        /**********************************************************************************************
         * Mehtod   명 : SpreadMenuMng 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 조회
         * Input    값 : SpreadMenuMng(언어코드, 접속권한, 현재메뉴경로, )
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadMenuMng : 메뉴 조회 (공통영역)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strAccessAuth">접속권한</param>
        /// <param name="strMenuUrl">현재메뉴경로</param>
        /// <returns>DataSet</returns>
        public static DataSet SpreadMenuMng(string strLangCd, string strAccessAuth, string strMenuUrl, int intMenuSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = CommMenuMng.SelectMenuMng(strLangCd, strAccessAuth, strMenuUrl, intMenuSeq);

            return dsReturn;
        }

        #endregion
    }
}
