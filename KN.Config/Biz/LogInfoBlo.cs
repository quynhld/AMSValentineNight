using System.Data;

using KN.Config.Dac;

namespace KN.Config.Biz
{
    public class LogInfoBlo
    {
        #region SpreadLogInfo : 로그 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadLogInfo
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
        public static DataSet SpreadLogInfo(int intPageSize, int intNowPage, string strKeyCd, string strKeyWord, string strStartDt, string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = LogInfoDao.SelectLogInfo(intPageSize, intNowPage, strKeyCd, strKeyWord, strStartDt, strEndDt);

            return dsReturn;
        }

        #endregion

        #region SpreadAccessLogInfo : 접속로그 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccessLogInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-13
         * 용       도 : 접속로그 조회
         * Input    값 : SpreadAccessLogInfo(페이지별 리스트 크기, 현재페이지)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectLogInfo : 접속로그 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>\
        /// <param name="strStartDt">조회시작일</param>
        /// <param name="strEndDt">조회종료일</param>
        /// <returns></returns>
        public static DataSet SpreadAccessLogInfo(int intPageSize, int intNowPage, string strStartDt, string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = LogInfoDao.SelectAccessLogInfo(intPageSize, intNowPage, strStartDt, strEndDt);

            return dsReturn;
        }

        #endregion

        #region SpreadLogDetailInfo : 로그 상세 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadLogDetailInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-28
         * 용       도 : 로그 상세 조회
         * Input    값 : SpreadLogDetailInfo(로그순번, 로그타입)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadLogDetailInfo : 로그 상세 조회
        /// </summary>
        /// <param name="intLogSeq">로그순번</param>
        /// <param name="strErrTy">로그타입</param>
        /// <returns></returns>
        public static DataTable SpreadLogDetailInfo(int intLogSeq, string strErrTy)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = LogInfoDao.SelectLogDetailInfo(intLogSeq, strErrTy);

            return dtReturn;
        }

        #endregion

        #region SpreadAccountLogInfo : 정산 로그 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccountLogInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-03
         * 용       도 : 정산 로그 조회
         * Input    값 : SpreadAccountLogInfo(페이지별 리스트 크기, 현재페이지, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 정산 로그 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadAccountLogInfo(int intPageSize, int intNowPage, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = LogInfoDao.SelectAccountLogInfo(intPageSize, intNowPage, strLangCd);

            return dsReturn;
        }

        #endregion

        #region SpreadMoneyLogInfo : 금전처리 로그 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMoneyLogInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-20
         * 용       도 : 금전처리 로그 조회
         * Input    값 : SpreadMoneyLogInfo(페이지별 리스트 크기, 현재페이지, 언어코드)
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
        public static DataSet SpreadMoneyLogInfo(int intPageSize, int intNowPage, string strLangCd, string strStartDt, string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = LogInfoDao.SelectMoneyLogInfo(intPageSize, intNowPage, strLangCd, strStartDt, strEndDt);

            return dsReturn;
        }

        #endregion

        #region RemoveLogInfo : 로그 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveLogInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-28
         * 용       도 : 로그 삭제
         * Input    값 : RemoveLogInfo(로그순번, 로그타입)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveLogInfo : 로그 삭제
        /// </summary>
        /// <param name="intLogSeq">로그순번</param>
        /// <param name="strErrTy">로그타입</param>
        /// <returns></returns>
        public static object[] RemoveLogInfo(int intLogSeq, string strErrTy)
        {
            object[] objReturn = new object[2];

            objReturn = LogInfoDao.DeleteLogInfo(intLogSeq, strErrTy);

            return objReturn;
        }

        #endregion

        #region RemoveAccessLogInfo : 접속로그 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveAccessLogInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-13
         * 용       도 : 접속로그 삭제
         * Input    값 : RemoveAccessLogInfo(로그순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveLogInfo : 접속로그 삭제
        /// </summary>
        /// <param name="intLogSeq">로그순번</param>
        /// <returns></returns>
        public static object[] RemoveAccessLogInfo(int intLogSeq)
        {
            object[] objReturn = new object[2];

            objReturn = LogInfoDao.DeleteAccessLogInfo(intLogSeq);

            return objReturn;
        }

        #endregion
    }
}
