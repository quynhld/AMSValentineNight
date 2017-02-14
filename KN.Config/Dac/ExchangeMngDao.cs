using System;
using System.Data;

using KN.Common.Base;

namespace KN.Config.Dac
{
    public class ExchangeMngDao
    {
        #region SelectExchangeRateInfo : 특정시점의 환율조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExchangeRateInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-27
         * 용       도 : 특정시점의 환율조회
         * Input    값 : SelectExchangeRateInfo(특정일자, 섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExchangeRateInfo : 특정시점의 환율조회
        /// </summary>
        /// <param name="strNowDt">특정일자</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable SelectExchangeRateInfo(string strNowDt, string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strNowDt.Replace("-", "");
            objParams[1] = strRentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_EXCHANGERATEINFO_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectExchangeRateInfo(string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            string strNowDt = string.Empty;

            strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "").Replace("/", "");

            dtReturn = SelectExchangeRateInfo(strNowDt, strRentCd);

            return dtReturn;
        }

        #endregion

        #region SelectCurrencyInfo : 특정시점의 실제 환율조회

        /**********************************************************************************************
         * Mehtod   명 : SelectCurrencyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-02
         * 용       도 : SelectCurrencyInfo : 특정시점의 실제 환율조회
         * Input    값 : SelectCurrencyInfo(특정일자, 섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectCurrencyInfo : 특정시점의 환율조회
        /// </summary>
        /// <param name="strNowDt">특정일자</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable SelectCurrencyInfo(string strRentCd, string strNowDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strNowDt.Replace("-", "");

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_CURRENCYINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExchangeRateLastInfo : 최종시점의 환율조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExchangeRateLastInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 최종시점의 환율조회
         * Input    값 : SelectExchangeRateLastInfo(섹터코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExchangeRateLastInfo : 최종시점의 환율조회
        /// </summary>
        public static DataTable SelectExchangeRateLastInfo(string strRentCd)
        {
            object[] objParam = new object[1];
            DataTable dtReturn = new DataTable();

            objParam[0] = strRentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_EXCHANGERATEINFO_S01", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectOfficeExchangeRateLastInfo : 오피스 최종시점의 환율조회

        /**********************************************************************************************
         * Mehtod   명 : SelectOfficeExchangeRateLastInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-12
         * 용       도 : 오피스 최종시점의 환율조회
         * Input    값 : SelectOfficeExchangeRateLastInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectOfficeExchangeRateLastInfo : 오피스 최종시점의 환율조회
        /// </summary>
        public static DataTable SelectOfficeExchangeRateLastInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_EXCHANGERATEINFO_S05");

            return dtReturn;
        }

        #endregion

        #region SelectExchangeRateLastTenInfo : 최근 10일간의 환율조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExchangeRateLastTenInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 최근 10일간의 환율조회
         * Input    값 : SelectExchangeRateLastTenInfo(섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExchangeRateLastTenInfo : 최근 10일간의 환율조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable SelectExchangeRateLastTenInfo(string strRentCd)
        {
            DataTable dtReturn = new DataTable();
            object [] objParam = new object[1];

            objParam[0] = strRentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_EXCHANGERATEINFO_S02", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectExchangeRateListInfo : 환율조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExchangeRateListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 환율조회
         * Input    값 : SelectExchangeRateListInfo(페이지크기, 현재페이지번호, 섹션코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectExchangeRateListInfo : 환율조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataSet SelectExchangeRateListInfo(int intPageSize, int intNowPage, string strRentCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[3];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_EXCHANGERATEINFO_S03", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectExchangeRatePrevInfo : 이전 환율정보조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExchangeRatePrevInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-27
         * 용       도 : 환율정보 중복조회
         * Input    값 : SelectExchangeRatePrevInfo(특정일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExchangeRatePrevInfo : 이전 환율정보조회
        /// </summary>
        /// <param name="strNowDt">특정일자</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable SelectExchangeRatePrevInfo(string strNowDt, string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strNowDt.Replace("-", "");
            objParams[1] = strRentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_EXCHANGERATEINFO_S04", objParams);

            return dtReturn;
        }

        #endregion
        
        #region InsertExchangeRateInfo : 환율등록

        /**********************************************************************************************
         * Mehtod   명 : InsertExchangeRateInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 환율등록
         * Input    값 : InsertExchangeRateInfo(적용일자, 섹션코드, 달러대비동, 차액, 차이율, 현금구매액, 현금판매액, 
         *                                      송금발송액, 송금수신액, 기업코드, 사번, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertExchangeRateInfo : 환율등록
        /// </summary>
        /// <param name="strAppliedDt">적용일자</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="fltDongToDollar">달러대비동</param>
        /// <param name="fltDifferAmt">차액</param>
        /// <param name="fltDifferRatio">차이율</param>
        /// <param name="fltCashBuy">현금구매액</param>
        /// <param name="fltCashSell">현금판매액</param>
        /// <param name="fltTransferSend">송금발송액</param>
        /// <param name="fltTransferRecieve">송금수신액</param>
        /// <param name="strInsCompNo">기업코드</param>
        /// <param name="strInsMemNo">사번</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] InsertExchangeRateInfo(string strRentCd, string strAppliedDt, double fltDongToDollar, double fltDifferAmt, double fltDifferRatio, double fltCashBuy, double fltCashSell,
            double fltTransferSend, double fltTransferRecieve, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[12];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strAppliedDt.Replace("-", "");
            objParams[2] = fltDongToDollar;
            objParams[3] = fltDifferAmt;
            objParams[4] = fltDifferRatio;
            objParams[5] = fltCashBuy;
            objParams[6] = fltCashSell;
            objParams[7] = fltTransferSend;
            objParams[8] = fltTransferRecieve;
            objParams[9] = strInsCompNo;
            objParams[10] = strInsMemNo;
            objParams[11] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_EXCHANGERATEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateExchangeRateInfo : 환율수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateExchangeRateInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 환율수정
         * Input    값 : UpdateExchangeRateInfo(환율순번, 적용일자, 달러대비동, 차액, 차이율, 현금구매액, 현금판매액, 송금발송액, 송금수신액, 사번, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateExchangeRateInfo : 환율수정
        /// </summary>
        /// <param name="intExchangeSeq">환율순번</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strAppliedDt">적용일자</param>
        /// <param name="fltDongToDollar">달러대비동</param>
        /// <param name="fltDifferAmt">차액</param>
        /// <param name="fltDifferRatio">차이율</param>
        /// <param name="fltCashBuy">현금구매액</param>
        /// <param name="fltCashSell">현금판매액</param>
        /// <param name="fltTransferSend">송금발송액</param>
        /// <param name="fltTransferRecieve">송금수신액</param>
        /// <param name="strInsCompNo">섹션코드</param>
        /// <param name="strInsMemNo">사번</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] UpdateExchangeRateInfo(int intExchangeSeq, string strRentCd, string strAppliedDt, double fltDongToDollar, double fltDifferAmt, 
                                                      double fltDifferRatio, double fltCashBuy, double fltCashSell, double fltTransferSend, double fltTransferRecieve, 
                                                      string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[13];
            object[] objReturn = new object[2];

            objParams[0] = intExchangeSeq;
            objParams[1] = strRentCd;
            objParams[2] = strAppliedDt.Replace("-", "");
            objParams[3] = fltDongToDollar;
            objParams[4] = fltDifferAmt;
            objParams[5] = fltDifferRatio;
            objParams[6] = fltCashBuy;
            objParams[7] = fltCashSell;
            objParams[8] = fltTransferSend;
            objParams[9] = fltTransferRecieve;
            objParams[10] = strInsCompNo;
            objParams[11] = strInsMemNo;
            objParams[12] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_EXCHANGERATEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteExchangeRateInfo : 환율삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteExchangeRateInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 환율삭제
         * Input    값 : DeleteExchangeRateInfo(환율순번, 섹션코드, 사번, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteExchangeRateInfo : 환율삭제
        /// </summary>
        /// <param name="intExchangeSeq">환율순번</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strInsMemNo">사번</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] DeleteExchangeRateInfo(int intExchangeSeq, string strRentCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[4];
            object[] objReturn = new object[2];

            objParams[0] = intExchangeSeq;
            objParams[1] = strRentCd;
            objParams[2] = strInsMemNo;
            objParams[3] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_EXCHANGERATEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteCurrencyInfo : 기타환율삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteCurrencyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 기타환율삭제
         * Input    값 : DeleteCurrencyInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteCurrencyInfo : 환율삭제
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteCurrencyInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_CURRENCYINFO_M00");

            return objReturn;
        }

        #endregion
    }
}