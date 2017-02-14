using System.Data;
using System.Text;

using KN.Common.Base;

namespace KN.Config.Dac
{
    public class AuthorityMngDao
    {
        #region SelectControlAuthGrpInfo : Control용 권한그룹코드 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectControlAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : Control용 권한그룹코드 정보 조회
         * Input    값 : SelectControlAuthGrpInfo(회사코드, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectControlAuthGrpInfo : Control용 권한그룹코드 정보 조회
        /// </summary>
        /// <param name="strAuthLvl">회사코드</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectControlAuthGrpInfo(string strAuthLvl, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParam = new object[2];

            objParam[0] = strAuthLvl;
            objParam[1] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_AUTHGRPINFO_S01", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectControlLimitAuthGrpInfo : Control용 제한권한그룹코드 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectControlLimitAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-18
         * 용       도 : Control용 제한권한그룹코드 정보 조회
         * Input    값 : SelectControlLimitAuthGrpInfo(언어코드, 권한코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectControlLimitAuthGrpInfo : Control용 제한권한그룹코드 정보 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="intAuthData">권한코드</param>
        /// <returns></returns>
        public static DataTable SelectControlLimitAuthGrpInfo(string strLangCd, int intAuthData)
        {
            DataTable dtReturn = new DataTable();

            object[] objParam = new object[2];

            objParam[0] = strLangCd;
            objParam[1] = intAuthData;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_AUTHGRPINFO_S02", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectAuthGrpInfo : 권한그룹코드 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-09
         * 용       도 : 권한그룹코드 정보 조회
         * Input    값 : SelectAuthGrpInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectAuthGrpInfo : 권한그룹코드 정보 조회
        /// </summary>
        /// <param name="strAuthLvl">회사코드</param>
        /// <returns></returns>
        public static DataTable SelectAuthGrpInfo(string strAuthLvl)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            objParam[0] = strAuthLvl;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_AUTHGRPINFO_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertAuthGrpInfo : 권한그룹코드 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-09
         * 용       도 : 권한그룹코드 정보 등록
         * Input    값 : InsertAuthGrpInfo(사원번호, 권한타입, 권한레벨, 베트남권한명, 영어권한명, 한글권한명)
         * Ouput    값 : Object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertAuthGrpInfo : 권한그룹코드 정보 등록
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strMemAuthTy">권한타입</param>
        /// <param name="strAuthLvl">권한레벨</param>
        /// <param name="strMemAuthTyNm">베트남권한명</param>
        /// <param name="strMemAuthTyENm">영어권한명</param>
        /// <param name="strMemAuthTyKNm">한글권한명</param>
        /// <returns></returns>
        public static object[] InsertAuthGrpInfo(string strCompNo, string strMemNo, string strMemAuthTy, string strAuthLvl, string strMemAuthTyNm, string strMemAuthTyENm, string strMemAuthTyKNm)
        {
            object[] objParams = new object[7];
            object[] objReturns = new object[2];

            objParams[0] = strCompNo;
            objParams[1] = strMemNo;
            objParams[2] = strMemAuthTy;
            objParams[3] = strAuthLvl;
            objParams[4] = strMemAuthTyNm;
            objParams[5] = strMemAuthTyENm;
            objParams[6] = strMemAuthTyKNm;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_AUTHGRPINFO_M00", objParams);

            return objReturns;
        }

        public static object[] InsertAuthGrpInfo(StringBuilder sbCompNo, StringBuilder sbMemNo, StringBuilder sbMemAuthTy, StringBuilder sbAuthLvl, StringBuilder sbMemAuthTyNm, StringBuilder sbMemAuthTyENm, StringBuilder sbMemAuthTyKNm)
        {
            object[] objParams = new object[7];
            object[] objReturns = new object[2];

            objParams[0] = sbCompNo;
            objParams[1] = sbMemNo;
            objParams[2] = sbMemAuthTy;
            objParams[3] = sbAuthLvl;
            objParams[4] = sbMemAuthTyNm;
            objParams[5] = sbMemAuthTyENm;
            objParams[6] = sbMemAuthTyKNm;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_AUTHGRPINFO_M00", objParams);

            return objReturns;
        }

        #endregion

        #region UpdateAuthGrpInfo : 권한그룹코드 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-09
         * 용       도 : 권한그룹코드 정보 수정
         * Input    값 : UpdateAuthGrpInfo(사원번호, 권한타입, 베트남권한명, 영어권한명, 한글권한명)
         * Ouput    값 : Object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateAuthGrpInfo : 권한그룹코드 정보 수정
        /// </summary>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strMemAuthTy">권한타입</param>
        /// <param name="strMemAuthTyNm">베트남권한명</param>
        /// <param name="strMemAuthTyENm">영어권한명</param>
        /// <param name="strMemAuthTyKNm">한글권한명</param>
        /// <returns></returns>
        public static object[] UpdateAuthGrpInfo(string strCompNo, string strMemNo, string strAuthLvl, string strMemAuthTy, string strMemAuthTyNm, string strMemAuthTyENm, string strMemAuthTyKNm)
        {
            object[] objParams = new object[7];
            object[] objReturns = new object[2];

            objParams[0] = strCompNo;
            objParams[1] = strMemNo;
            objParams[2] = strAuthLvl;
            objParams[3] = strMemAuthTy;
            objParams[4] = strMemAuthTyNm;
            objParams[5] = strMemAuthTyENm;
            objParams[6] = strMemAuthTyKNm;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_AUTHGRPINFO_M00", objParams);

            return objReturns;
        }

        public static object[] UpdateAuthGrpInfo(StringBuilder sbCompNo, StringBuilder sbMemNo, StringBuilder sbAuthLvl, StringBuilder sbMemAuthTy, StringBuilder sbMemAuthTyNm, StringBuilder sbMemAuthTyENm, StringBuilder sbMemAuthTyKNm)
        {
            object[] objParams = new object[7];
            object[] objReturns = new object[2];

            objParams[0] = sbCompNo;
            objParams[1] = sbMemNo;
            objParams[2] = sbAuthLvl;
            objParams[3] = sbMemAuthTy;
            objParams[4] = sbMemAuthTyNm;
            objParams[5] = sbMemAuthTyENm;
            objParams[6] = sbMemAuthTyKNm;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_AUTHGRPINFO_M00", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteAuthGrpInfo : 권한그룹코드 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteAuthGrpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-09
         * 용       도 : 권한그룹코드 정보 삭제
         * Input    값 : DeleteAuthGrpInfo(권한타입)
         * Ouput    값 : Object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteAuthGrpInfo : 권한그룹코드 정보 삭제
        /// </summary>
        /// <param name="strAuthLvl">권한그룹</param>
        /// <param name="strMemAuthTy">권한타입</param>
        /// <returns></returns>
        public static object[] DeleteAuthGrpInfo(string strAuthLvl, string strMemAuthTy)
        {
            object[] objParams = new object[2];
            object[] objReturns = new object[2];

            objParams[0] = strAuthLvl;
            objParams[1] = strMemAuthTy;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_AUTHGRPINFO_M00", objParams);

            return objReturns;
        }

        public static object[] DeleteAuthGrpInfo(StringBuilder sbAuthLvl, StringBuilder sbMemAuthTy)
        {
            object[] objParams = new object[2];
            object[] objReturns = new object[2];

            objParams[0] = sbAuthLvl;
            objParams[0] = sbMemAuthTy;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_AUTHGRPINFO_M00", objParams);

            return objReturns;
        }

        #endregion
    }
}
