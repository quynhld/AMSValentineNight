using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Config.Dac
{
    public class VatMngDao
    {
        #region SelectVatInfo : 항목별 부가세율 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-21
         * 용       도 : 항목별 부가세율 조회
         * Input    값 : SelectVatInfo(섹터코드, 기준일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectVatInfo : 최종시점의 환율조회
        /// </summary>
        /// <param name="strRentCd">섹터코드</param>
        /// <param name="strSearchDt">기준일자</param>
        /// <returns></returns>
        public static DataTable SelectVatInfo(string strRentCd, string strSearchDt)
        {
            object[] objParam = new object[2];
            DataTable dtReturn = new DataTable();

            objParam[0] = strRentCd;
            objParam[1] = TextLib.MakeNullToEmpty(strSearchDt);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_VATINFO_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectExistVatInfo : 부가세 중복조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-19
         * 용       도 : 부가세 중복조회
         * Input    값 : SelectExistVatInfo(strVatCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExistVatInfo : 부가세 중복조회
        /// </summary>
        /// <param name="strVatCd">부가세종류</param>
        /// <returns></returns>
        public static DataTable SelectExistVatInfo(string strVatCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strVatCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_VATINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectVatDetailInfo : 부가세율 상세조회

        /**********************************************************************************************
         * Mehtod   명 : SelectVatDetailInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-20
         * 용       도 : 부가세율 상세조회
         * Input    값 : SelectVatDetailInfo(적용일, 부가세종류)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectVatDetailInfo : 부가세율 상세조회
        /// </summary>
        /// <param name="strNowDt">적용일</param>
        /// <param name="strVatCd">부가세종류</param>
        /// <returns></returns>
        public static DataTable SelectVatDetailInfo(string strNowDt, string strVatCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strNowDt;
            objParams[1] = strVatCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_VATINFO_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectVatInfo : 부가세 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-18
         * 용       도 : 부가세 조회
         * Input    값 : SelectVatInfo(페이지별 리스트 크기, 현재페이지, 언어코드, 부가세종류)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectVatInfo : 부가세 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strVatCd">부가세종류</param>
        /// <returns></returns>
        public static DataSet SelectVatInfo(int intPageSize, int intNowPage, string strLangCd, string strVatCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[4];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strLangCd;
            objParams[3] = strVatCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_VATINFO_S03", objParams);

            return dsReturn;
        }

        #endregion

        #region InsertVatInfo : 부가세 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-18
         * 용       도 : 부가세 등록
         * Input    값 : InsertVatInfo(부가세종류코드, 적용시작일자, 부가세율, 등록회사코드, 등록사번, 등록IP)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 부가세 등록
        /// </summary>
        /// <param name="strVatCd">부가세종류코드</param>
        /// <param name="strStartDt">적용시작일자</param>
        /// <param name="dblVatRatio">부가세율</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strIp">등록IP</param>
        /// <returns></returns>
        public static object[] InsertVatInfo(string strVatCd, string strStartDt, double dblVatRatio, string strInsCompNo, string strInsMemNo, string strIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strVatCd;
            objParams[1] = strStartDt;
            objParams[2] = dblVatRatio;
            objParams[3] = strInsCompNo;
            objParams[4] = strInsMemNo;
            objParams[5] = strIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_VATINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateVatInfo : 부가세 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-19
         * 용       도 : 부가세 수정
         * Input    값 : UpdateVatInfo(부가세종류코드, 적용시작일자, 부가세율, 등록회사코드, 등록사번, 등록IP)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 부가세 등록
        /// </summary>
        /// <param name="strVatCd">부가세종류코드</param>
        /// <param name="strStartDt">적용시작일자</param>
        /// <param name="dblVatRatio">부가세율</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strIp">등록IP</param>
        /// <returns></returns>
        public static object[] UpdateVatInfo(string strVatCd, string strStartDt, double dblVatRatio, string strInsCompNo, string strInsMemNo, string strIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strVatCd;
            objParams[1] = strStartDt;
            objParams[2] = dblVatRatio;
            objParams[3] = strInsCompNo;
            objParams[4] = strInsMemNo;
            objParams[5] = strIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_VATINFO_M00", objParams);

            return objReturn;
        }

        #endregion
    }
}
