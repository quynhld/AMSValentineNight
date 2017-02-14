using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Config.Dac
{
    class LogInfoDao
    {
        #region SelectLogInfo : 로그 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectLogInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-27
         * 용       도 : 로그 조회
         * Input    값 : SelectLogInfo(페이지별 리스트 크기, 현재페이지, 검색어코드, 검색어)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectLogInfo : 로그 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strKeyCd">검색어코드(0001 : 내용)</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strStartDt">조회시작일자</param>
        /// <param name="strEndDt">조회종료일자</param>
        /// <returns></returns>
        public static DataSet SelectLogInfo(int intPageSize, int intNowPage, string strKeyCd, string strKeyWord, string strStartDt, string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[6];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strKeyCd;
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strKeyWord));
            objParams[4] = strStartDt;
            objParams[5] = strEndDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_COMM_SELECT_LOG_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectAccessLogInfo : 접속로그 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAccessLogInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-13
         * 용       도 : 접속로그 조회
         * Input    값 : SelectAccessLogInfo(페이지별 리스트 크기, 현재페이지)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectLogInfo : 접속로그 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strStartDt">조회시작일</param>
        /// <param name="strEndDt">조회종료일</param>
        /// <returns></returns>
        public static DataSet SelectAccessLogInfo(int intPageSize, int intNowPage, string strStartDt, string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[4];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_COMM_SELECT_LOG_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectLogDetailInfo : 로그 상세 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectLogDetailInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-28
         * 용       도 : 로그 상세 조회
         * Input    값 : SelectLogDetailInfo(로그순번, 로그타입)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectLogDetailInfo : 로그 상세 조회
        /// </summary>
        /// <param name="intLogSeq">로그순번</param>
        /// <param name="strErrTy">로그타입</param>
        /// <returns></returns>
        public static DataTable SelectLogDetailInfo(int intLogSeq, string strErrTy)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = intLogSeq;
            objParams[1] = strErrTy;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_LOG_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectAccountLogInfo : 정산 로그 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountLogInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-03
         * 용       도 : 정산 로그 조회
         * Input    값 : SelectAccountLogInfo(페이지별 리스트 크기, 현재페이지, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 정산 로그 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectAccountLogInfo(int intPageSize, int intNowPage, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[3];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_COMM_SELECT_LOG_S03", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMoneyLogInfo : 금전처리 로그 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMoneyLogInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-20
         * 용       도 : 금전처리 로그 조회
         * Input    값 : SelectMoneyLogInfo(페이지별 리스트 크기, 현재페이지, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 금액처리 로그 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strStartDt">조회시작일자</param>
        /// <param name="strEndDt">조회종료일자</param>
        /// <returns></returns>
        public static DataSet SelectMoneyLogInfo(int intPageSize, int intNowPage, string strLangCd, string strStartDt, string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[5];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strLangCd;
            objParams[3] = strStartDt;
            objParams[4] = strEndDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_COMM_SELECT_LOG_S04", objParams);

            return dsReturn;
        }

        #endregion

        #region DeleteLogInfo : 로그 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteLogInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-28
         * 용       도 : 로그 삭제
         * Input    값 : DeleteLogInfo(로그순번, 로그타입)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteLogInfo : 로그 삭제
        /// </summary>
        /// <param name="intLogSeq">로그순번</param>
        /// <param name="strErrTy">로그타입</param>
        /// <returns></returns>
        public static object[] DeleteLogInfo(int intLogSeq, string strErrTy)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intLogSeq;
            objParams[1] = strErrTy;

            objReturn = SPExecute.ExecReturnNo("KN_USP_COMM_DELETE_LOG_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteAccessLogInfo : 접속로그 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteAccessLogInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-13
         * 용       도 : 접속로그 삭제
         * Input    값 : DeleteAccessLogInfo(로그순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteLogInfo : 접속로그 삭제
        /// </summary>
        /// <param name="intLogSeq">로그순번</param>
        /// <returns></returns>
        public static object[] DeleteAccessLogInfo(int intLogSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = intLogSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_COMM_DELETE_LOG_M01", objParams);

            return objReturn;
        }

        #endregion
    }
}
