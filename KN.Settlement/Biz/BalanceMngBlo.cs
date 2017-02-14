using System.Data;

using KN.Settlement.Dac;

namespace KN.Settlement.Biz
{
    public class BalanceMngBlo
    {
        #region WatchLedgerMngForParking : 정산정보조회 (월정주차 차량 삭제를 위한 조회)

        /**********************************************************************************************
         * Mehtod   명 : WatchLedgerMngForParking
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-26
         * 용       도 : 정산정보조회 (월정주차 차량 삭제를 위한 조회)
         * Input    값 : WatchLedgerMngForParking(사용자번호, 지불일자, 해당년, 해당월, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchLedgerMngForParking : 정산정보조회 (월정주차 차량 삭제를 위한 조회)
        /// </summary>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="strSvcYear">해당년</param>
        /// <param name="strSvcMM">해당월</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet WatchLedgerMngForParking(string strUserSeq, string strPaymentDt, string strSvcYear, string strSvcMM, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectLedgerMngForParking(strUserSeq, strPaymentDt, strSvcYear, strSvcMM, strLangCd);

            return dsReturn;
        }

        #endregion

        #region WatchLedgerMngForRentalMng : 정산정보조회 (임대료 및 관리비 삭제를 위한 조회)

        /**********************************************************************************************
         * Mehtod   명 : WatchLedgerMngForRentalMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-26
         * 용       도 : 정산정보조회 (임대료 및 관리비 삭제를 위한 조회)
         * Input    값 : WatchLedgerMngForParking(사용자번호, 지불일자, 해당년, 해당월, 언어코드, 아이템코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchLedgerMngForRentalMng : 정산정보조회 (임대료 및 관리비 삭제를 위한 조회)
        /// </summary>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="strSvcYear">해당년</param>
        /// <param name="strSvcMM">해당월</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <returns></returns>
        public static DataSet WatchLedgerMngForRentalMng(string strUserSeq, string strPaymentDt, string strSvcYear, string strSvcMM, string strLangCd, string strItemCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectLedgerMngForRentalMng(strUserSeq, strPaymentDt, strSvcYear, strSvcMM, strLangCd, strItemCd);

            return dsReturn;
        }

        #endregion

        #region SpreadMngAccountList : 정산정보조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngAccountList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-14
         * 용       도 : 정산정보조회
         * Input    값 : SpreadMngAccountList(strKind, strStartDt, strEndDt, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SpreadMngAccountList(int intPageSize, int intNowPage, string strKind, string strStartDt, string strEndDt, string strLangCd, string strRentCd,
                                                   string strPaymentCd)
        {

           var  dsReturn = BalanceMngDao.SelectMngAccountList(intPageSize, intNowPage, strKind, strStartDt, strEndDt, strLangCd, strRentCd, strPaymentCd);

            return dsReturn;
        }

        //public static DataSet SpreadMngAccountListTower( string strKind, string strStartDt, string strEndDt, string strLangCd, string strRentCd,
        //                                           string strPaymentCd)
        //{

        //   var  dsReturn = BalanceMngDao.SelectMngAccountList( strKind, strStartDt, strEndDt, strLangCd, strRentCd, strPaymentCd);

        //    return dsReturn;
        //}

        #endregion

        #region SelectDailyIncomeParking : 정산정보조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngAccountList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-14
         * 용       도 : 정산정보조회
         * Input    값 : SpreadMngAccountList(strKind, strStartDt, strEndDt, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectMngDailyIncomeParking(int intPageSize, int intNowPage, string strStartDt, string strEndDt, string strLangCd, string strRentCd,
                                                   string strPaymentCd, string strCarTyCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectMngDailyIncomeParking(intPageSize, intNowPage, strStartDt, strEndDt, strLangCd, strRentCd, strPaymentCd, strCarTyCd);

            return dsReturn;
        }

        #endregion



        #region SelectExcelDailyIncomeParking : 정산정보조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelDailyIncomeParking
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-14
         * 용       도 : 정산정보조회
         * Input    값 : SelectExcelDailyIncomeParking(strKind, strStartDt, strEndDt, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelDailyIncomeParking(int intPageSize, int intNowPage, string strStartDt, string strEndDt, string strLangCd, string strRentCd,
                                                   string strPaymentCd, string strCarTyCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectExcelDailyIncomeParking(intPageSize, intNowPage, strStartDt, strEndDt, strLangCd, strRentCd, strPaymentCd, strCarTyCd);

            return dsReturn;
        }


        #endregion        

        #region SelectExcelPendingPaymentList 

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelPendingPaymentList
         * 개   발  자 : 양영석
         * 생   성  일 : 2015-04-09
         * 용       도 : 정산정보조회
         * Input    값 : SelectExcelPendingPaymentList(strRentcd, strRoom, strItemCd, strStartdt , strEndDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelPendingPaymentList(string strRentcd, string strRoom, string strItemCd, string strStartdt,
                                                   string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectExcelPendingPaymentList(strRentcd, strRoom, strItemCd, strStartdt, strEndDt);

            return dsReturn;
        }


        #endregion     

        #region SelectExcelOfficeMasterList  

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelOfficeMasterList
         * 개   발  자 : PhuongTV
         * 생   성  일 : 2014-03-27
         * 용       도 : 정산정보조회
         * Input    값 : SelectExcelOfficeMasterList(strRentCd, strLangCd, strIndusCd, strNatCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelOfficeMasterList(string strRentCd, string strLangCd, string strIndusCd, string strNatCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectExcelOfficeMasterList(strRentCd, strLangCd, strIndusCd, strNatCd);

            return dsReturn;
        }

        #endregion

        #region SelectExcelAPTAgingReport

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelAPTAgingReport
         * 개   발  자 : PhuongTV
         * 생   성  일 : 2014-01-10
         * 용       도 : 
         * Input    값 : SelectExcelAPTAgingReport(strFeeTy, strStartDt, strEndDt, strRoomNo,)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelAPTAgingReport(string strFeeTy, string strStartDt, string strEndDt, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectExcelAPTAgingReport(strFeeTy, strStartDt, strEndDt, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region SelectExcelRentalInformationReport

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelRentalInformationReport
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-10-07
         * 용       도 : 
         * Input    값 : SelectExcelRentalInformationReport(strRentCd,strStartDt, strEndDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelRentalInformationReport(string strRentCd, string strStartDt, string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectExcelRentalInformationReport(strRentCd, strStartDt, strEndDt);

            return dsReturn;
        }

        #endregion

        #region SelectExcelContractExpired

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelContractExpired
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2014-08-01
         * 용       도 : 
         * Input    값 : SelectExcelContractExpired(strRentCd,strStartDt, strEndDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelContractExpired(string strRentCd, string strStartDt, string strEndDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectExcelContractExpired(strRentCd, strStartDt, strEndDt);

            return dsReturn;
        }

        #endregion

        #region SpreadExcelMngAccountList : Excel용 정산정보조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadExcelMngAccountList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-15
         * 용       도 : Excel용 정산정보조회
         * Input    값 : SpreadExcelMngAccountList(strKind, strStartDt, strEndDt, strLangCd, strRentCd, strPaymentCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadExcelMngAccountList : Excel용 정산정보조회
        /// </summary>
        /// <param name="strKind">결제종류</param>
        /// <param name="strStartDt">시작일</param>
        /// <param name="strEndDt">종료일</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strPaymentCd">지불코드</param>
        /// <returns></returns>
        public static DataTable SpreadExcelMngAccountList(string strKind, string strStartDt, string strEndDt, string strLangCd, string strRentCd, string strPaymentCd)
        {

           var  dtReturn = BalanceMngDao.SelectExcelMngAccountList(strKind, strStartDt, strEndDt, strLangCd, strRentCd, strPaymentCd);

            return dtReturn;
        }

        #endregion

        #region ExcelAllocationRevenueReport : Excel용 정산정보조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelAllocationRevenueReport
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-09-24
         * 용       도 : Excel용 정산정보조회
         * Input    값 : SelectExcelAllocationRevenueReport(strRoomNo, strSPeriod)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadExcelMngAccountList : Excel
        /// </summary>
        /// <param name="strRoomNo">결제종류</param>
        /// <param name="strSPeriod">시작일</param>       
        /// <returns></returns>
        public static DataTable SelectExcelAllocationRevenueReport(string strRoomNo, string strSPeriod)
        {
            var dtReturn = BalanceMngDao.SelectExcelAllocationRevenueReport(strRoomNo, strSPeriod);

            return dtReturn;
        }

        #endregion

        #region SelectContractStatusReport : Excel 

        /**********************************************************************************************
         * Mehtod   명 : SelectContractStatusReport
         * 개   발  자 : PhuongTV
         * 생   성  일 : 2011-11-15
         * 용       도 : Excel용 정산정보조회
         * Input    값 : SelectContractStatusReportstrRentCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadExcelMngAccountList : Excel용 정산정보조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable SelectContractStatusReport(string strRentCd)
        {

            var dtReturn = BalanceMngDao.SelectContractStatusReport(strRentCd);

            return dtReturn;
        }

        #endregion

        #region SpreadCancelSettelmentList : 정산취소

        /**********************************************************************************************
         * Mehtod   명 : SpreadCancelSettelmentList
         * 개   발  자 : 김범수
         * 생   성  일 : 2011-01-25
         * 용       도 : 정산취소
         * Input    값 : SpreadCancelSettelmentList(intPageSize, intNowPage, strRentCd, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 정산취소
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>   
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadCancelSettelmentList(int intPageSize, int intNowPage, string strRentCd, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectCancelSettelmentList(intPageSize, intNowPage, strRentCd, strLangCd);

            return dsReturn;
        }

        #endregion

        #region SpreadAccountsList : 미정산리스트

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccountsList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-29
         * 용       도 : 미정산리스트
         * Input    값 : SpreadAccountsList(intPageSize, intNowPage, strRentCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 미정산리스트
        /// </summary>
        /// <param name="intPageSize">페이지별크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <returns>DataSet</returns>
        public static DataSet SpreadAccountsList(int intPageSize, int intNowPage, string strRentCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectAccountsList(intPageSize, intNowPage, strRentCd);

            return dsReturn;
        }

        #endregion

        #region SpreadAccountCdList : 회계 계정 코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccountCdList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-31
         * 용       도 : 회계 계정 코드 조회
         * Input    값 : SpreadAccountCdList(언어코드, 회사코드, 섹션코드, 회계코드, 품목코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 회계 계정 코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strSettleCd">회계코드</param>
        /// <param name="strItemCd">품목코드</param>
        /// <returns></returns>
        public static DataTable SpreadAccountCdList(string strLangCd, string strCompCd, string strRentCd, string strSettleCd, string strItemCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            dtReturn = BalanceMngDao.SelectAccountCdList(strLangCd, strCompCd, strRentCd, strSettleCd, strItemCd);

            return dtReturn;
        }

        //Baotv
        public static DataTable SpreadBankAccountList(string strLangCd, string strCompCd, string strRentCd, string feeType, string paymentType)
        {
            var dtReturn = BalanceMngDao.SelectBankAccountList(strLangCd, strCompCd, strRentCd, feeType, paymentType);

            return dtReturn;
        }

        #endregion

        #region SpreadReserveHoadonList : 화돈발급 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadReserveHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-23
         * 용       도 : 화돈발급 목록 조회
         * Input    값 : SpreadReserveHoadonList(한 페이지당 목록수, 현재 페이지 번호, 섹션코드, 이름, 호, 청구서종류, 로그인언어코드, 회사코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadReserveHoadonList : 화돈발급 목록 조회
        /// </summary>
        /// <param name="intPageSize">한 페이지당 목록수</param>
        /// <param name="intNowPage">현재 페이지 번호</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strNm">이름</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strDocCd">청구서종류</param>
        /// <param name="strLangCd">로그인언어코드</param>
        /// <param name="strCompCd">회사코드</param>
        /// <returns></returns>
        public static DataSet SpreadReserveHoadonList(int intPageSize, int intNowPage, string strRentCd, string strNm, string strRoomNo, string strDocCd, string strLangCd, string strCompCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectReserveHoadonList(intPageSize, intNowPage, strRentCd, strNm, strRoomNo, strDocCd, strLangCd, strCompCd);

            return dsReturn;
        }

        #endregion

        #region SpreadReserveHoadonDetList : 화돈발급 목록 상세조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadReserveHoadonDetList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-19
         * 용       도 : 화돈발급 목록 상세조회
         * Input    값 : SpreadReserveHoadonDetList(한 페이지당 목록수, 현재 페이지 번호, 섹션코드, 이름, 호, 청구서종류, 로그인언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadReserveHoadonDetList : 화돈발급 목록 상세조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strPaymentDt">발급일자</param>
        /// <param name="intPaymentSeq">발급순번</param>
        /// <param name="intPaymentDetSeq">발급상세순번</param>
        /// <param name="strLangCd">로그인언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadReserveHoadonDetList(string strRentCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BalanceMngDao.SelectReserveHoadonDetList(strRentCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strLangCd);

            return dsReturn;
        }
        //BaoTv
        public static DataSet SpreadDetListDetail(string strSeq)
        {
            var dsReturn = BalanceMngDao.SpreadDetListDetail(strSeq);

            return dsReturn;
        }

        #endregion 

        #region SpreadExcelReceiptList : Excel용 수납내역조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadExcelReceiptList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-18
         * 용       도 : Excel용 수납내역조회
         * Input    값 : SpreadExcelReceiptList(strKind, strPaymentCd, strStartDt, strEndDt)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadExcelReceiptList : Excel용 수납내역조회
        /// </summary>
        /// <param name="strKind">항목코드</param>
        /// <param name="strPaymentCd">지불코드</param>
        /// <param name="strStartDt">시작일</param>
        /// <param name="strEndDt">종료일</param>
        /// <returns></returns>
        public static DataTable SpreadExcelReceiptList(string strKind, string strPaymentCd, string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BalanceMngDao.SelectExcelReceiptList(strKind, strPaymentCd, strStartDt, strEndDt);

            return dtReturn;
        }

        #endregion

        #region RegistryLedgerInfo : 정산정보입력

        /**********************************************************************************************
         * Mehtod   명 : RegistryLedgerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-01-17
         * 용       도 : 정산정보입력
         * Input    값 : RegistryLedgerInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strRentCd, strDirectCd, strItemCd, intItemSeq, strUserTyCd,
         *                                  strUserSeq,	strUserTaxCd, fltDongToDollar, fltItemTotEnAmt, fltItemTotViAmt, strPaymentCd,
         *                                  fltPaymentRatio, strRemarkVi, strRemarkEn, strInsCompNo, strInsMemNo, strInsMemIP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 정산정보입력
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strDirectCd">직영코드</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="intItemSeq">항목별순번</param>
        /// <param name="strUserTyCd">사용자구분코드</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strUserTaxCd">사용자Tax 코드</param>
        /// <param name="dblDongToDollar">1달러 대비 동환율</param>
        /// <param name="dblItemTotEnAmt">달러총액</param>
        /// <param name="dblItemTotViAmt">베트남동총액</param>
        /// <param name="strPaymentCd">지불수단코드</param>
        /// <param name="dblPaymentRatio">수수료율</param>
        /// <param name="strComNo">입력기업코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static DataTable RegistryLedgerInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strRentCd, string strDirectCd, string strItemCd,
                                                   int intItemSeq, string strUserTyCd, string strUserSeq, string strUserTaxCd, double dblDongToDollar, double dblItemTotEnAmt,
                                                   double dblItemTotViAmt, string strPaymentCd, double dblPaymentRatio,
                                                   string strComNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BalanceMngDao.InsertLedgerInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strRentCd, strDirectCd, strItemCd, intItemSeq, strUserTyCd,
                                                      strUserSeq, strUserTaxCd, dblDongToDollar, dblItemTotEnAmt, dblItemTotViAmt, strPaymentCd, dblPaymentRatio,
                                                      strComNo, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryLedgerDetInfo : 정산상세정보입력

        /**********************************************************************************************
         * Mehtod   명 : RegistryLedgerDetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-01-18
         * 용       도 : 정산상세정보입력
         * Input    값 : RegistryLedgerDetInfo(strDebitCreditCd	,strPaymentDt,intPaymentSeq,intPaymentDetSeq,strRentCd,strDirectCd,
         *                                     strItemCd,intItemSeq,intItemDetSeq,strSvcZoneCd,strClassiGrpCd,strClassiMainCd,strClassCd,
         *                                     intQty,strScaleCd,fltUnitPrimeCost,fltTotPrimeCost,fltUnitSellingPrice,fltTotSellingPrice,
         *                                     fltSvcCharge,fltVATRatio,strVATYn,strInsMemNo,strInsMemIP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 정산상세정보입력
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="intPaymentDetSeq">지불일자별 상세순번</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strDirectCd">직영코드</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="intItemSeq">항목별순번</param>
        /// <param name="intItemDetSeq">항목별상세순번</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">대분류코드</param>
        /// <param name="strClassiMainCd">중분류코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <param name="intQty">수량</param>
        /// <param name="strScaleCd">단위코드</param>
        /// <param name="dblUnitPrimeCost">개별원가</param>
        /// <param name="dblTotPrimeCost">총원가</param>
        /// <param name="dblUnitSellingPrice">개별매가</param>
        /// <param name="dblTotSellingPrice">총매가</param>
        /// <param name="strSvcYear">서비스년</param>
        /// <param name="strSvcMM">서비스월</param>
        /// <param name="strGoodsNm">상품명</param>
        /// <param name="dblSvcCharge">별도서비스가격</param>
        /// <param name="dblVATRatio">부가세율</param>
        /// <param name="strVATYn">부가세포함여부</param>
        /// <param name="strInsCompNo">등록기업코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static DataTable RegistryLedgerDetInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                      string strRentCd, string strDirectCd, string strItemCd, int intItemSeq, int intItemDetSeq,
                                                      string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd, string strClassCd, int intQty,
                                                      string strScaleCd, double dblUnitPrimeCost, double dblTotPrimeCost, double dblUnitSellingPrice,
                                                      double dblTotSellingPrice, double dblSvcCharge, string strSvcYear, string strSvcMM,
                                                      string strGoodsNm, double dblVATRatio, string strVATYn, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BalanceMngDao.InsertLedgerDetInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strRentCd, strDirectCd, strItemCd, intItemSeq,
                                                         intItemDetSeq, strSvcZoneCd, strClassiGrpCd, strClassiMainCd, strClassCd, intQty, strScaleCd, dblUnitPrimeCost,
                                                         dblTotPrimeCost, dblUnitSellingPrice, dblTotSellingPrice, dblSvcCharge, strSvcYear, strSvcMM, strGoodsNm, 
                                                         dblVATRatio, strVATYn, strInsCompNo, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryLedgerAddonInfo : 정산추가정보입력

        /**********************************************************************************************
         * Mehtod   명 : RegistryLedgerAddonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-22
         * 용       도 : 정산추가정보입력
         * Input    값 : RegistryLedgerAddonInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strAccountCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 정산추가정보입력
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="strAccountCd">통장코드</param>
        /// <returns></returns>
        public static DataTable RegistryLedgerAddonInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strAccountCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BalanceMngDao.InsertLedgerAddonInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strAccountCd);

            return dtReturn;
        }

        #endregion 

        #region RegistrySettelmentInfoByManual : 강제마감 처리

        /**********************************************************************************************
         * Mehtod   명 : RegistrySettelmentInfoByManual
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-22
         * 용       도 : 강제마감 처리
         * Input    값 : RegistrySettelmentInfoByManual(섹션코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistrySettelmentInfoByManual : 강제마감 처리
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static object[] RegistrySettelmentInfoByManual(string strRentCd)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.InsertSettelmentInfoByManual(strRentCd);

            return objReturn;
        }

        #endregion 

        #region RegistryHoadonContInfo : 세금계산서대상정보입력

        /**********************************************************************************************
         * Mehtod   명 : RegistryHoadonContInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-21
         * 용       도 : 세금계산서대상정보입력
         * Input    값 : RegistryHoadonContInfo(strRentCd, strRoomNo, strContYn, strInsCompNo, strInsMemNo, strInsMemIP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryHoadonContInfo : 세금계산서대상정보입력
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strContYn">세금계산서대상 집주인여부</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] RegistryHoadonContInfo(string strRentCd, string strRoomNo, string strContYn, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.InsertHoadonContInfo(strRentCd, strRoomNo, strContYn, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryChestNutHoadonList : 화돈발급

        /**********************************************************************************************
         * Mehtod   명 : RegistryChestNutHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-23
         * 용       도 : 화돈발급
         * Input    값 : RegistryChestNutHoadonList(언어코드, 입주자번호, 사용자명, 지불일자, 지불순번, 주소, 상세주소, 입주자세금코드, 실제처리일자,
         *                                         기업명, 세금계산서대상자, 회사코드, 등록사번, 등록IP, 조회일)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryChestNutHoadonList : 화돈발급
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="strUserNm">사용자명</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="strAddr">주소</param>
        /// <param name="strDetAddr">상세주소</param>
        /// <param name="strUserTaxCd">입주자세금코드</param>
        /// <param name="strModPaymentDt">실제처리일자</param>
        /// <param name="strCompNm">기업명</param>
        /// <param name="strInvoiceContYn">세금계산서대상자</param>
        /// <param name="strInsCompCd">회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <param name="strNowDt">조회일</param>
        /// <returns></returns>
        public static object[] RegistryChestNutHoadonList(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt, int intPaymentSeq, string strAddr,
                                                          string strDetAddr, string strUserTaxCd, string strModPaymentDt, string strCompNm, string strInvoiceContYn,
                                                          string strInsCompCd, string strInsMemNo, string strInsMemIP, string strNowDt)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.InsertChestNutHoadonList(strLangCd, strUserSeq, strUserNm, strPaymentDt, intPaymentSeq, strAddr, strDetAddr, strUserTaxCd, 
                                                               strModPaymentDt, strCompNm, strInvoiceContYn, strInsCompCd, strInsMemNo, strInsMemIP, strNowDt);

            return objReturn;
        }

        #endregion

        #region RegistryKeangNamHoadonList : 화돈발급

        /**********************************************************************************************
         * Mehtod   명 : RegistryKeangNamHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-23
         * 용       도 : 화돈발급
         * Input    값 : RegistryKeangNamHoadonList(언어코드, 입주자번호, 사용자명, 지불일자, 지불순번, 주소, 상세주소, 입주자세금코드, 실제처리일자,
         *                                         기업명, 세금계산서대상자, 회사코드, 등록사번, 등록IP, 조회일)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryKeangNamHoadonList : 화돈발급
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="strUserNm">사용자명</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="strAddr">주소</param>
        /// <param name="strDetAddr">상세주소</param>
        /// <param name="strUserTaxCd">입주자세금코드</param>
        /// <param name="strModPaymentDt">실제처리일자</param>
        /// <param name="strCompNm">기업명</param>
        /// <param name="strInvoiceContYn">세금계산서대상자</param>
        /// <param name="strInsCompCd">회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <param name="strNowDt">조회일</param>
        /// <returns></returns>
        public static object[] RegistryKeangNamHoadonList(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt, int intPaymentSeq, string strAddr,
                                                          string strDetAddr, string strUserTaxCd, string strModPaymentDt, string strCompNm, string strInvoiceContYn,
                                                          string strInsCompCd, string strInsMemNo, string strInsMemIP, string strNowDt)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.InsertKeangNamHoadonList(strLangCd, strUserSeq, strUserNm, strPaymentDt, intPaymentSeq, strAddr, strDetAddr, strUserTaxCd,
                                                               strModPaymentDt, strCompNm, strInvoiceContYn, strInsCompCd, strInsMemNo, strInsMemIP, strNowDt);

            return objReturn;
        }

        #endregion

        #region RegistryReserveHoadonList : 화돈발급

        /**********************************************************************************************
         * Mehtod   명 : RegistryReserveHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-23
         * 용       도 : 화돈발급
         * Input    값 : RegistryReserveHoadonList(언어코드, 입주자번호, 지불일자, 지불순번, 지불상세번호, 입주자세금코드, 기업명, 주소, 등록사번
         *                                       등록IP, 조회일)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryReserveHoadonList : 화돈발급
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="intPaymentDetSeq">지불상세번호</param>
        /// <param name="strModPaymentDt">실제처리일자</param>
        /// <param name="strUserTaxCd">입주자세금코드</param>
        /// <param name="strCompNm">기업명</param>
        /// <param name="strAddr">주소</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <param name="strNowDt">조회일</param>
        /// <returns></returns>
        public static object[] RegistryReserveHoadonList(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt, int intPaymentSeq, string strAddr,
                                                         string strDetAddr, string strUserTaxCd, string strModPaymentDt, string strCompNm, string strInvoiceContYn,
                                                         string strInsCompCd, string strInsMemNo, string strInsMemIP, string strNowDt)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.InsertReserveHoadonList(strLangCd, strUserSeq, strUserNm, strPaymentDt, intPaymentSeq, strAddr, strDetAddr, strUserTaxCd,
                                                              strModPaymentDt, strCompNm, strInvoiceContYn, strInsCompCd, strInsMemNo, strInsMemIP, strNowDt);

            return objReturn;
        }

        #endregion

        #region RegistryAccountCdInfo : 회계계정코드 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryAccountCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-02
         * 용       도 : 회계계정코드 등록
         * Input    값 : RegistryAccountCdInfo(회사코드, 섹션코드, 수납문서코드, 아이템코드, 회계코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryAccountCdInfo : 회계계정코드 등록
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strSettleCd">수납문서코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strAccCd">회계코드</param>
        /// <returns></returns>
        public static DataTable RegistryAccountCdInfo(string strCompCd, string strRentCd, string strSettleCd, string strItemCd, string strAccCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BalanceMngDao.InsertAccountCdInfo(strCompCd, strRentCd, strSettleCd, strItemCd, strAccCd);

            return dtReturn;
        }

        public static DataTable RegistryAccountBankInfo(string strCompCd, string strRentCd, string feeType, string paymentType, string bankNm, string bankAccCd, int bankSeq, string usedYn, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BalanceMngDao.RegistryAccountBankInfo(strCompCd, strRentCd, feeType, paymentType, bankNm,bankAccCd,bankSeq,usedYn,strInsMemNo,strInsMemIP);

            return dtReturn;
        }

        #endregion    
        
        #region ModifyLedgerDetInfoForPrint : 원장상세테이블에 프린트 번호 추가

        /**********************************************************************************************
         * Mehtod   명 : ModifyLedgerDetInfoForPrint
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-06
         * 용       도 : 원장상세테이블에 프린트 번호 추가
         * Input    값 : ModifyLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt,intPaymentSeq,intPaymentDetSeq,strPrintSeq,intPrintDetSeq)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyLedgerDetInfoForPrint : 원장상세테이블에 프린트 번호 추가
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="intPaymentDetSeq">지불일자별 상세순번</param>
        /// <param name="strPrintSeq">프린트번호</param>
        /// <param name="intPrintDetSeq">프린트상세번호</param>
        /// <returns></returns>
        public static object[] ModifyLedgerDetInfoForPrint(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq, string strPrintSeq, int intPrintDetSeq)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.UpdateLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintSeq, intPrintDetSeq);

            return objReturn;
        }

        #endregion

        #region ModifyLedgerinfoForTaxCd: 원장테이블에 세금코드 추가

        /**********************************************************************************************
         * Mehtod   명 : ModifyLedgerinfoForTaxCd
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-16
         * 용       도 : 원장테이블에 세금코드 추가
         * Input    값 : ModifyLedgerinfoForTaxCd(strDebitCreditCd, strPaymentDt, intPaymentSeq, strUserTaxCd)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyLedgerinfoForTaxCd : 원장테이블에 세금코드 추가
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="strUserTaxCd">세금계산서코드</param>
        /// <returns></returns>
        public static object[] ModifyLedgerinfoForTaxCd(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strUserTaxCd)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.UpdateLedgerinfoForTaxCd(strDebitCreditCd, strPaymentDt, intPaymentSeq, strUserTaxCd);

            return objReturn;
        }

        #endregion

        #region ModifyTaxInfo: 세금관련 정보 변경

        /**********************************************************************************************
         * Mehtod   명 : ModifyTaxInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-25
         * 용       도 : 세금관련 정보 변경
         * Input    값 : ModifyTaxInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strNm, strUserTaxCd, strTaxAddr, strTaxDetAddr, strInvoiceContYn)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyTaxInfo: 세금관련 정보 변경
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="strNm">대상자이름</param>
        /// <param name="strUserTaxCd">세금계산서코드</param>
        /// <param name="strTaxAddr">대상자주소</param>
        /// <param name="strTaxDetAddr">대상자상세주소</param>
        /// <param name="strInvoiceContYn">세금계산서발급대상</param>
        /// <returns></returns>
        public static object[] ModifyTaxInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strNm, string strUserTaxCd, string strTaxAddr,
                                             string strTaxDetAddr, string strInvoiceContYn)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.UpdateTaxInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strNm, strUserTaxCd, strTaxAddr, strTaxDetAddr, strInvoiceContYn);

            return objReturn;
        }

        //Baotv
        public static object[] ModifyDebit(string rentCd,string feety,string roomNo,string strSeq, string sDt,string eDt,string payDt,string issueDt,double exRate,double feeAmount,double discount,double total, string subDes)
        {
            var objReturn = BalanceMngDao.ModifyDebit(rentCd, feety, roomNo, strSeq, sDt, eDt, payDt, issueDt, exRate, feeAmount, discount, total, subDes);

            return objReturn;
        }

        #endregion

        #region ModifyEntireTaxInfo: 세금관련 정보 변경

        /**********************************************************************************************
         * Mehtod   명 : ModifyEntireTaxInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-25
         * 용       도 : 세금관련 정보 변경
         * Input    값 : ModifyEntireTaxInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strNm, strUserTaxCd, strTaxAddr, strTaxDetAddr, strInvoiceContYn)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyEntireTaxInfo: 세금관련 정보 변경
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="strNm">대상자이름</param>
        /// <param name="strUserTaxCd">세금계산서코드</param>
        /// <param name="strTaxAddr">대상자주소</param>
        /// <param name="strTaxDetAddr">대상자상세주소</param>
        /// <param name="strInvoiceContYn">세금계산서발급대상</param>
        /// <returns></returns>
        public static object[] ModifyEntireTaxInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strNm, string strUserTaxCd, string strTaxAddr,
                                                   string strTaxDetAddr, string strInvoiceContYn)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.UpdateEntireTaxInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strNm, strUserTaxCd, strTaxAddr, strTaxDetAddr, strInvoiceContYn);

            return objReturn;
        }

        #endregion
        
        #region ModifyHoadonContInfo : 세금계산서대상정보수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyHoadonContInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-21
         * 용       도 : 세금계산서대상정보수정
         * Input    값 : ModifyHoadonContInfo(strRentCd, strRoomNo, strContYn, strInsCompNo, strInsMemNo, strInsMemIP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyHoadonContInfo : 세금계산서대상정보수정
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strContYn">세금계산서대상 집주인여부</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] ModifyHoadonContInfo(string strRentCd, string strRoomNo, string strContYn, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.UpdateHoadonContInfo(strRentCd, strRoomNo, strContYn, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyAccountCdInfo : 회계계정코드 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyAccountCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-02
         * 용       도 : 회계계정코드 수정
         * Input    값 : ModifyAccountCdInfo(회사코드, 섹션코드, 수납문서코드, 아이템코드, 회계코드, 계정코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyAccountCdInfo : 회계계정코드 수정
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strSettleCd">수납문서코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strAccountCd">회계코드</param>
        /// <param name="strAccCd">계정코드</param>
        /// <returns></returns>
        public static object[] ModifyAccountCdInfo(string strCompCd, string strRentCd, string strSettleCd, string strItemCd, string strAccountCd, string strAccCd)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.UpdateAccountCdInfo(strCompCd, strRentCd, strSettleCd, strItemCd, strAccountCd, strAccCd);

            return objReturn;
        }

        #endregion

        #region RemoveLedgerMng : 정산정보삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveLedgerMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 정산정보삭제
         * Input    값 : RemoveLedgerMng(대차코드, 지불일자, 지불순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 정산정보삭제
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <returns></returns>
        public static object[] RemoveLedgerMng(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.DeleteLedgerMng(strDebitCreditCd, strPaymentDt, intPaymentSeq);

            return objReturn;
        }

        #endregion

        #region RemoveAccountCdInfo : 회계계정코드 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveAccountCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-02
         * 용       도 : 회계계정코드 삭제
         * Input    값 : RemoveAccountCdInfo(회사코드, 섹션코드, 수납문서코드, 아이템코드, 계정코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveAccountCdInfo : 회계계정코드 삭제
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strSettleCd">수납문서코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strAccountCd">계정코드</param>
        /// <returns></returns>
        public static object[] RemoveAccountCdInfo(string strCompCd, string strRentCd, string strSettleCd, string strItemCd, string strAccountCd)
        {
            object[] objReturn = new object[2];

            objReturn = BalanceMngDao.DeleteAccountCdInfo(strCompCd, strRentCd, strSettleCd, strItemCd, strAccountCd);

            return objReturn;
        }

        //BaoTV
        public static object[] RemoveBankAccountInfo(string strCompCd, string strRentCd, string feeTy, string paymentTy, int bankSeq)
        {
            var objReturn = BalanceMngDao.DeleteBankAccountInfo(strCompCd, strRentCd, feeTy, paymentTy, bankSeq);

            return objReturn;
        }

        #endregion
    }
}