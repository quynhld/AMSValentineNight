using System.Data;

using KN.Config.Dac;

namespace KN.Config.Biz
{
    public class ExchangeMngBlo
    {
        #region WatchExchangeRateInfo : 특정시점의 환율조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExchangeRateInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-27
         * 용       도 : 특정시점의 환율조회
         * Input    값 : WatchExchangeRateInfo(특정일자, 섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExchangeRateInfo : 특정시점의 환율조회
        /// </summary>
        /// <param name="strNowDt">특정일자</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable WatchExchangeRateInfo(string strNowDt, string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ExchangeMngDao.SelectExchangeRateInfo(strNowDt, strRentCd);

            return dtReturn;
        }

        public static DataTable WatchExchangeRateInfo(string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ExchangeMngDao.SelectExchangeRateInfo(strRentCd);

            return dtReturn;
        }

        #endregion

        #region WatchCurrencyInfo : 특정시점의 실제 환율조회

        /**********************************************************************************************
         * Mehtod   명 : WatchCurrencyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-02
         * 용       도 : 특정시점의 실제 환율조회
         * Input    값 : WatchCurrencyInfo(특정일자, 섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchCurrencyInfo : 특정시점의 환율조회
        /// </summary>
        /// <param name="strNowDt">특정일자</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable WatchCurrencyInfo(string strNowDt, string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ExchangeMngDao.SelectCurrencyInfo(strRentCd, strNowDt);

            return dtReturn;
        }

        public static DataTable WatchCurrencyInfo(string strRentCd)
        {
            string strEmpty = string.Empty;

            DataTable dtReturn = new DataTable();

            dtReturn = ExchangeMngDao.SelectCurrencyInfo(strRentCd, strEmpty);

            return dtReturn;
        }

        #endregion

        #region WatchExchangeRateLastInfo : 최종시점의 환율조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExchangeRateLastInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 최종시점의 환율조회
         * Input    값 : WatchExchangeRateLastInfo(섹터코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExchangeRateLastInfo : 최종시점의 환율조회
        /// </summary>
        /// <param name="strRentCd">섹터코드</param>
        /// <returns></returns>
        public static DataTable WatchExchangeRateLastInfo(string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ExchangeMngDao.SelectExchangeRateLastInfo(strRentCd);

            return dtReturn;
        }

        #endregion

        #region WatchOfficeExchangeRateLastInfo : 오피스 최종시점의 환율조회

        /**********************************************************************************************
         * Mehtod   명 : WatchOfficeExchangeRateLastInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-12
         * 용       도 : 오피스 최종시점의 환율조회
         * Input    값 : WatchOfficeExchangeRateLastInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchOfficeExchangeRateLastInfo : 오피스 최종시점의 환율조회
        /// </summary>
        /// <returns></returns>
        public static DataTable WatchOfficeExchangeRateLastInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ExchangeMngDao.SelectOfficeExchangeRateLastInfo();

            return dtReturn;
        }

        #endregion

        #region WatchExchangeRateLastTenInfo : 최근 10일간의 환율조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExchangeRateLastTenInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 최근 10일간의 환율조회
         * Input    값 : WatchExchangeRateLastTenInfo(섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExchangeRateLastTenInfo : 최근 10일간의 환율조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable WatchExchangeRateLastTenInfo(string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ExchangeMngDao.SelectExchangeRateLastTenInfo(strRentCd);

            return dtReturn;
        }

        #endregion

        #region SpreadExchangeRateListInfo : 환율조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadExchangeRateListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 환율조회
         * Input    값 : SpreadExchangeRateListInfo(페이지크기, 현재페이지번호, 섹션코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadExchangeRateListInfo : 환율조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataSet SpreadExchangeRateListInfo(int intPageSize, int intNowPage, string strRentCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ExchangeMngDao.SelectExchangeRateListInfo(intPageSize, intNowPage, strRentCd);

            return dsReturn;
        }

        #endregion

        #region WatchExchangeRatePrevInfo : 이전 환율정보조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExchangeRatePrevInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-27
         * 용       도 : 환율정보 중복조회
         * Input    값 : WatchExchangeRatePrevInfo(특정일자, 섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExchangeRatePrevInfo : 이전 환율정보조회
        /// </summary>
        /// <param name="strNowDt">특정일자</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable WatchExchangeRatePrevInfo(string strNowDt, string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ExchangeMngDao.SelectExchangeRatePrevInfo(strNowDt, strRentCd);

            return dtReturn;
        }

        #endregion

        #region RegistryExchangeRateInfo : 환율등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryExchangeRateInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 환율등록
         * Input    값 : RegistryExchangeRateInfo(적용일자, 섹션코드, 달러대비동, 차액, 차이율, 현금구매액, 현금판매액, 송금발송액, 송금수신액, 기업코드, 사번, 사원IP)
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
        public static object[] RegistryExchangeRateInfo(string strRentCd, string strAppliedDt, double fltDongToDollar, double fltDifferAmt, double fltDifferRatio, double fltCashBuy, double fltCashSell,
                                                        double fltTransferSend, double fltTransferRecieve, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ExchangeMngDao.InsertExchangeRateInfo(strRentCd, strAppliedDt, fltDongToDollar, fltDifferAmt, fltDifferRatio, fltCashBuy, fltCashSell, fltTransferSend, fltTransferRecieve,
                                                              strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyExchangeRateInfo : 환율수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyExchangeRateInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 환율수정
         * Input    값 : ModifyExchangeRateInfo(환율순번, 섹션코드, 적용일자, 달러대비동, 차액, 차이율, 현금구매액, 
         *                                      현금판매액, 송금발송액, 송금수신액, 수정사번, 사번, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyExchangeRateInfo : 환율수정
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
        /// <param name="strInsCompNo">수정사번</param>
        /// <param name="strInsMemNo">사번</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] ModifyExchangeRateInfo(int intExchangeSeq, string strRentCd, string strAppliedDt, double fltDongToDollar, double fltDifferAmt, double fltDifferRatio, 
                                                      double fltCashBuy, double fltCashSell, double fltTransferSend, double fltTransferRecieve, string strInsCompNo, 
                                                      string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ExchangeMngDao.UpdateExchangeRateInfo(intExchangeSeq, strRentCd, strAppliedDt, fltDongToDollar, fltDifferAmt, fltDifferRatio, fltCashBuy, fltCashSell, fltTransferSend, 
                                                              fltTransferRecieve, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RemoveExchangeRateInfo : 환율삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveExchangeRateInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 환율삭제
         * Input    값 : RemoveExchangeRateInfo(환율순번, 섹션코드, 사번, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveExchangeRateInfo : 환율삭제
        /// </summary>
        /// <param name="intExchangeSeq">환율순번</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strInsMemNo">사번</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] RemoveExchangeRateInfo(int intExchangeSeq, string strRentCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ExchangeMngDao.DeleteExchangeRateInfo(intExchangeSeq, strRentCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RemoveCurrencyInfo : 기타환율삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveCurrencyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-26
         * 용       도 : 기타환율삭제
         * Input    값 : RemoveCurrencyInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveCurrencyInfo : 환율삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveCurrencyInfo()
        {
            object[] objReturn = new object[2];

            objReturn = ExchangeMngDao.DeleteCurrencyInfo();

            return objReturn;
        }

        #endregion
    }
}