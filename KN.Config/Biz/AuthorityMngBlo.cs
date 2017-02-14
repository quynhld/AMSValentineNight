using System.Data;
using System.Text;

using KN.Config.Dac;

namespace KN.Config.Biz
{
    public class AuthorityMngBlo
    {
        #region SpreadControlAuthGrpInfo : Control용 권한그룹코드 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadControlAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : Control용 권한그룹코드 정보 조회
         * Input    값 : SpreadControlAuthGrpInfo(언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadControlAuthGrpInfo : Control용 권한그룹코드 정보 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadControlAuthGrpInfo(string strAuthLvl, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = AuthorityMngDao.SelectControlAuthGrpInfo(strAuthLvl, strLangCd);

            return dtReturn;
        }

        #endregion

        #region SpreadControlLimitAuthGrpInfo : Control용 제한권한그룹코드 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadControlLimitAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-18
         * 용       도 : Control용 제한권한그룹코드 정보 조회
         * Input    값 : SpreadControlLimitAuthGrpInfo(언어코드, 권한코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadControlLimitAuthGrpInfo : Control용 제한권한그룹코드 정보 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="intAuthData">권한코드</param>
        /// <returns></returns>
        public static DataTable SpreadControlLimitAuthGrpInfo(string strLangCd, int intAuthData)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = AuthorityMngDao.SelectControlLimitAuthGrpInfo(strLangCd, intAuthData);

            return dtReturn;
        }

        #endregion

        #region SpreadAuthGrpInfo : 권한그룹코드 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-09
         * 용       도 : 권한그룹코드 정보 조회
         * Input    값 : SpreadAuthGrpInfo(strAuthLvl)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadAuthGrpInfo : 권한그룹코드 정보 조회
        /// </summary>
        /// <param name="strAuthLvl">회사코드</param>
        /// <returns></returns>
        public static DataTable SpreadAuthGrpInfo(string strAuthLvl)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = AuthorityMngDao.SelectAuthGrpInfo(strAuthLvl);

            return dtReturn;
        }

        #endregion

        #region RegistryAuthGrpInfo : 권한그룹코드 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-09
         * 용       도 : 권한그룹코드 정보 등록
         * Input    값 : RegistryAuthGrpInfo(사원번호, 권한타입, 권한레벨, 베트남권한명, 영어권한명, 한글권한명)
         * Ouput    값 : Object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryAuthGrpInfo : 권한그룹코드 정보 등록
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strMemAuthTy">권한타입</param>
        /// <param name="intAuthLvl">권한레벨</param>
        /// <param name="strMemAuthTyNm">베트남권한명</param>
        /// <param name="strMemAuthTyENm">영어권한명</param>
        /// <param name="strMemAuthTyKNm">한글권한명</param>
        /// <returns></returns>
        public static object[] RegistryAuthGrpInfo(string strCompNo, string strMemNo, string strMemAuthTy, string strAuthLvl, string strMemAuthTyNm, string strMemAuthTyENm, string strMemAuthTyKNm)
        {
            object[] objReturns = new object[2];

            objReturns = AuthorityMngDao.InsertAuthGrpInfo(strCompNo, strMemNo, strMemAuthTy, strAuthLvl, strMemAuthTyNm, strMemAuthTyENm, strMemAuthTyKNm);

            return objReturns;
        }

        public static object[] RegistryAuthGrpInfo(StringBuilder sbCompNo, StringBuilder sbMemNo, StringBuilder sbMemAuthTy, StringBuilder sbAuthLvl, StringBuilder sbMemAuthTyNm, StringBuilder sbMemAuthTyENm, StringBuilder sbMemAuthTyKNm)
        {
            object[] objReturns = new object[2];

            objReturns = AuthorityMngDao.InsertAuthGrpInfo(sbCompNo, sbMemNo, sbMemAuthTy, sbAuthLvl, sbMemAuthTyNm, sbMemAuthTyENm, sbMemAuthTyKNm);

            return objReturns;
        }

        #endregion

        #region ModifyAuthGrpInfo : 권한그룹코드 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-09
         * 용       도 : 권한그룹코드 정보 수정
         * Input    값 : ModifyAuthGrpInfo(사원번호, 권한타입, 베트남권한명, 영어권한명, 한글권한명)
         * Ouput    값 : Object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyAuthGrpInfo : 권한그룹코드 정보 수정
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strAuthLvl">권한그룹</param>
        /// <param name="strMemAuthTy">권한타입</param>
        /// <param name="strMemAuthTyNm">베트남권한명</param>
        /// <param name="strMemAuthTyENm">영어권한명</param>
        /// <param name="strMemAuthTyKNm">한글권한명</param>
        /// <returns></returns>
        public static object[] ModifyAuthGrpInfo(string strCompNo, string strMemNo, string strAuthLvl, string strMemAuthTy, string strMemAuthTyNm, string strMemAuthTyENm, string strMemAuthTyKNm)
        {
            object[] objReturns = new object[2];

            objReturns = AuthorityMngDao.UpdateAuthGrpInfo(strCompNo, strMemNo, strAuthLvl, strMemAuthTy, strMemAuthTyNm, strMemAuthTyENm, strMemAuthTyKNm);

            return objReturns;
        }

        public static object[] ModifyAuthGrpInfo(StringBuilder sbCompNo, StringBuilder sbMemNo, StringBuilder sbAuthLvl, StringBuilder sbMemAuthTy, StringBuilder sbMemAuthTyNm, 
                                                 StringBuilder sbMemAuthTyENm, StringBuilder sbMemAuthTyKNm)
        {
            object[] objReturns = new object[2];

            objReturns = AuthorityMngDao.UpdateAuthGrpInfo(sbCompNo, sbMemNo, sbAuthLvl, sbMemAuthTy, sbMemAuthTyNm, sbMemAuthTyENm, sbMemAuthTyKNm);

            return objReturns;
        }

        #endregion

        #region RemoveAuthGrpInfo : 권한그룹코드 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-09
         * 용       도 : 권한그룹코드 정보 삭제
         * Input    값 : RemoveAuthGrpInfo(권한타입)
         * Ouput    값 : Object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveAuthGrpInfo : 권한그룹코드 정보 삭제
        /// </summary>
        /// <param name="strAuthLvl">권한그룹</param>
        /// <param name="strMemAuthTy">권한타입</param>
        /// <returns></returns>
        public static object[] RemoveAuthGrpInfo(string strAuthLvl, string strMemAuthTy)
        {
            object[] objReturns = new object[2];

            objReturns = AuthorityMngDao.DeleteAuthGrpInfo(strAuthLvl, strMemAuthTy);

            return objReturns;
        }

        public static object[] RemoveAuthGrpInfo(StringBuilder sbAuthLvl, StringBuilder sbMemAuthTy)
        {
            object[] objReturns = new object[2];

            objReturns = AuthorityMngDao.DeleteAuthGrpInfo(sbAuthLvl, sbMemAuthTy);

            return objReturns;
        }

        #endregion
    }
}
