using System.Data;
using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Settlement.Dac
{
    public class BalanceMngDao
    {
        #region SelectLedgerMngForParking : 정산정보조회 (월정주차 차량 삭제를 위한 조회)

        /**********************************************************************************************
         * Mehtod   명 : SelectLedgerMngForParking
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-26
         * 용       도 : 정산정보조회 (월정주차 차량 삭제를 위한 조회)
         * Input    값 : SelectLedgerMngForParking(사용자번호, 지불일자, 해당년, 해당월, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectLedgerMngForParking : 정산정보조회 (월정주차 차량 삭제를 위한 조회)
        /// </summary>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="strSvcYear">해당년</param>
        /// <param name="strSvcMM">해당월</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectLedgerMngForParking(string strUserSeq, string strPaymentDt, string strSvcYear, string strSvcMM, string strLangCd)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[5];

            objParams[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = TextLib.MakeNullToEmpty(strSvcYear);
            objParams[3] = TextLib.MakeNullToEmpty(strSvcMM);
            objParams[4] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_SET_SELECT_LEDGERINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectLedgerMngForRentalMng : 정산정보조회 (임대료 및 관리비 삭제를 위한 조회)

        /**********************************************************************************************
         * Mehtod   명 : SelectLedgerMngForRentalMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-04
         * 용       도 : 정산정보조회 (임대료 및 관리비 삭제를 위한 조회)
         * Input    값 : SelectLedgerMngForRentalMng(사용자번호, 지불일자, 해당년, 해당월, 언어코드, 항목코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectLedgerMngForRentalMng : 정산정보조회 (임대료 및 관리비 삭제를 위한 조회)
        /// </summary>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="strSvcYear">해당년</param>
        /// <param name="strSvcMM">해당월</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strItemCd">항목코드</param>
        /// <returns></returns>
        public static DataSet SelectLedgerMngForRentalMng(string strUserSeq, string strPaymentDt, string strSvcYear, string strSvcMM, string strLangCd, string strItemCd)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[6];

            objParams[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = TextLib.MakeNullToEmpty(strSvcYear);
            objParams[3] = TextLib.MakeNullToEmpty(strSvcMM);
            objParams[4] = strLangCd;
            objParams[5] = strItemCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_SET_SELECT_LEDGERINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMngAccountList : 정산정보조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMngAccountList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-14
         * 용       도 : 정산정보조회
         * Input    값 : SelectMngAccountList(strKind, strStartDt, strEndDt, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectMngAccountList(int intPageSize, int intNowPage, string strKind, string strStartDt, string strEndDt, string strLangCd, string strRentCd,
                                                   string strPaymentCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strKind;
            objParams[3] = strStartDt;
            objParams[4] = strEndDt;
            objParams[5] = strLangCd;
            objParams[6] = strRentCd;
            objParams[7] = strPaymentCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_ACCOUNTSINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectDailyIncomeParking

        /**********************************************************************************************
         * Mehtod   명 : SelectMngDailyIncomeParking
         * 개   발  자 : 양영석
         * 생   성  일 : 2013-06-11
         * 용       도 : 정산정보조회
         * Input    값 : SelectMngDailyIncomeParking(strStartDt, strEndDt, strLangCd, strRentCd , strPaymentCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectMngDailyIncomeParking(int intPageSize, int intNowPage, string strStartDt, string strEndDt, string strLangCd, string strRentCd,
                                                   string strPaymentCd, string strCarTyCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;            
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;
            objParams[4] = strLangCd;
            objParams[5] = strRentCd;
            objParams[6] = strPaymentCd;
            objParams[7] = strCarTyCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_DAILY_INC_PRK_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectExcelDailyIncomeParking

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelDailyIncomeParking
         * 개   발  자 : 양영석
         * 생   성  일 : 2013-07-15
         * 용       도 : 정산정보조회
         * Input    값 : SelectExcelDailyIncomeParking(strStartDt, strEndDt, strLangCd, strRentCd , strPaymentCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelDailyIncomeParking(int intPageSize, int intNowPage, string strStartDt, string strEndDt, string strLangCd, string strRentCd,
                                                   string strPaymentCd, string strCarTyCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;
            objParams[4] = strLangCd;
            objParams[5] = strRentCd;
            objParams[6] = strPaymentCd;
            objParams[7] = strCarTyCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_DAILY_INC_PRK_S01", objParams);

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

            object[] objParams = new object[5];

            objParams[0] = strRentcd;
            objParams[1] = strRoom;
            objParams[2] = strItemCd;
            objParams[3] = strStartdt;
            objParams[4] = strEndDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_EXC_SELECT_PENDING_PAYMENT", objParams);

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

            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strLangCd;
            objParams[2] = strIndusCd;
            objParams[3] = strNatCd;


            dsReturn = SPExecute.ExecReturnMulti("KN_USP_EXCEL_SELECT_OFFICE_TENANT_MASTER_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectExcelAPTAgingReport

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelAPTAgingReport
         * 개   발  자 : PhuongTV
         * 생   성  일 : 2014-01-10
         * 용       도 : 
         * Input    값 : SelectExcelAPTAgingReport(strFeeTy, strStartDt, strEndDt, strRoomNo)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelAPTAgingReport(string strFeeTy, string strStartDt, string strEndDt, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[4];

            objParams[0] = strFeeTy;
            objParams[1] = strStartDt;
            objParams[2] = strEndDt;
            objParams[3] = strRoomNo;
           

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_SELECT_APT_AR_AGING_REPORT_S01", objParams);

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

            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strStartDt;
            objParams[2] = strEndDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_RPT_SELECT_EXCEL_RENTAL_INFORMATION_S00", objParams);

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

            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strStartDt;
            objParams[2] = strEndDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_EXC_SELECT_EXPIRED_CONTRACT_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectExcelMngAccountList : Excel용 정산정보조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelMngAccountList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-15
         * 용       도 : Excel용 정산정보조회
         * Input    값 : SelectExcelMngAccountList(strKind, strStartDt, strEndDt, strLangCd, strRentCd, strPaymentCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExcelMngAccountList : Excel용 정산정보조회
        /// </summary>
        /// <param name="strKind">결제종류</param>
        /// <param name="strStartDt">시작일</param>
        /// <param name="strEndDt">종료일</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strPaymentCd">지불코드</param>
        /// <returns></returns>
        public static DataTable SelectExcelMngAccountList(string strKind, string strStartDt, string strEndDt, string strLangCd, string strRentCd, string strPaymentCd)
        {

            var objParams = new object[6];

            objParams[0] = strKind;
            objParams[1] = strStartDt.Replace("-", "");
            objParams[2] = strEndDt.Replace("-", "");
            objParams[3] = strLangCd;
            objParams[4] = strRentCd;
            objParams[5] = strPaymentCd;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTSINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region ExcelAllocationRevenueReport

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelAllocationRevenueReport
         * 개   발  자 : phuongtv
         * 생   성  일 : 2013-11-15
         * 용       도 : Excel
         * Input    값 : SelectExcelAllocationRevenueReport(strRoomNo, strSPeriod)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExcelMngAccountList : Excel용 정산정보조회
        /// </summary>
        /// <param name="strKind">결제종류</param>
        /// <param name="strStartDt">시작일</param>
        /// <param name="strEndDt">종료일</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strPaymentCd">지불코드</param>
        /// <returns></returns>
        public static DataTable SelectExcelAllocationRevenueReport(string strRoomNo, string strSPeriod)
        {

            var objParams = new object[2];

            objParams[0] = strRoomNo;
            objParams[1] = strSPeriod;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_SELECT_ALLOCATION_REVENUE_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectContractStatusReport : Excel

        /**********************************************************************************************
         * Mehtod   명 : SelectContractStatusReport
         * 개   발  자 : phuongtv
         * 생   성  일 : 2013-08-24
         * 용       도 : Excel용 정산정보조회
         * Input    값 : SelectContractStatusReport()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExcelMngAccountList : Excel용 정산정보조회
        /// </summary>
        /// <param name="strRentCd">결제종류</param>
        /// <returns></returns>
        public static DataTable SelectContractStatusReport(string strRentCd)
        {

            var objParams = new object[1];

            objParams[0] = strRentCd;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_SELECT_TENANT_CONTRACT_SUM_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectCancelSettelmentList : 정산취소

        /**********************************************************************************************
         * Mehtod   명 : SelectCancelSettelmentList
         * 개   발  자 : 김범수
         * 생   성  일 : 2011-01-25
         * 용       도 : 정산취소
         * Input    값 : SelectCancelSettelmentList(intPageSize, intNowPage, strRentCd, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectCancelSettelmentList : 정산취소
        /// </summary>
        /// <param name="intPageSize"></param>
        /// <param name="intNowPage"></param>
        /// <param name="strRentCd"></param>
        /// <param name="strLangCd"></param>
        /// <returns></returns>
        public static DataSet SelectCancelSettelmentList(int intPageSize, int intNowPage, string strRentCd, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[4];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_SETTELMEMTCANCEL_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectAccountsList : 미정산리스트

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountsList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-29
         * 용       도 : 미정산리스트
         * Input    값 : SelectAccountsList(페이지별 리스트 크기, 현재페이지, 임대구분코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 미정산리스트
        /// </summary>
        /// <param name="intPageSize">페이지별크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <returns></returns>
        public static DataSet SelectAccountsList(int intPageSize, int intNowPage, string strRentCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[3];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_ACCOUNTSINFO_S03", objParams);

            return dsReturn;
        }

        #endregion
        
        #region SelectAccountCdList : 회계 계정 코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountCdList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-31
         * 용       도 : 회계 계정 코드 조회
         * Input    값 : SelectAccountCdList(언어코드, 회사코드, 섹션코드, 회계코드, 품목코드)
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
        public static DataTable SelectAccountCdList(string strLangCd, string strCompCd, string strRentCd, string strSettleCd, string strItemCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[5];

            objParams[0] = TextLib.MakeNullToEmpty(strLangCd);
            objParams[1] = TextLib.MakeNullToEmpty(strCompCd);
            objParams[2] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[3] = TextLib.MakeNullToEmpty(strSettleCd);
            objParams[4] = TextLib.MakeNullToEmpty(strItemCd);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTCDINFO_S00", objParams);

            return dtReturn;
        }
        //Baotv
        public static DataTable SelectBankAccountList(string strLangCd, string strCompCd, string strRentCd, string feeType, string paymentType)
        {
            var objParams = new object[5];

            objParams[0] = TextLib.MakeNullToEmpty(strLangCd);
            objParams[1] = TextLib.MakeNullToEmpty(strCompCd);
            objParams[2] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[3] = TextLib.MakeNullToEmpty(feeType);
            objParams[4] = TextLib.MakeNullToEmpty(paymentType);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTCDINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectReserveHoadonList : 화돈발급 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectReserveHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-23
         * 용       도 : 화돈발급 목록 조회
         * Input    값 : SelectReserveHoadonList(한 페이지당 목록수, 현재 페이지 번호, 섹션코드, 이름, 호, 청구서종류, 로그인언어코드, 회사코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectReserveHoadonList : 화돈발급 목록 조회
        /// </summary>
        /// <param name="intPageSize">한 페이지당 목록수</param>
        /// <param name="intNowPage">현재 페이지 번호</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strNm">이름</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strDocCd">청구서종류</param>
        /// <param name="strLangCd">로그인언어코드</param>
        /// <param name="strCompCd">회사코드</param>
        /// <returns>DataSet</returns>
        public static DataSet SelectReserveHoadonList(int intPageSize, int intNowPage, string strRentCd, string strNm, string strRoomNo, string strDocCd, string strLangCd, string strCompCd)
        {
            object[] objParams = new object[8];
            DataSet dsReturn = new DataSet();

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strNm;
            objParams[4] = strRoomNo;
            objParams[5] = strDocCd;
            objParams[6] = strLangCd;
            objParams[7] = strCompCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_SETTLEMENT_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectReserveHoadonDetList : 화돈발급 목록 상세조회

        /**********************************************************************************************
         * Mehtod   명 : SelectReserveHoadonDetList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-19
         * 용       도 : 화돈발급 목록 상세조회
         * Input    값 : SelectReserveHoadonDetList(한 페이지당 목록수, 현재 페이지 번호, 섹션코드, 이름, 호, 청구서종류, 로그인언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectReserveHoadonDetList : 화돈발급 목록 상세조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strPaymentDt">발급일자</param>
        /// <param name="intPaymentSeq">발급순번</param>
        /// <param name="intPaymentDetSeq">발급상세순번</param>
        /// <param name="strLangCd">로그인언어코드</param>
        /// <returns></returns>
        public static DataSet SelectReserveHoadonDetList(string strRentCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq, string strLangCd)
        {
            object[] objParams = new object[5];
            DataSet dsReturn = new DataSet();

            objParams[0] = strRentCd;
            objParams[1] = strPaymentDt;
            objParams[2] = intPaymentSeq;
            objParams[3] = intPaymentDetSeq;
            objParams[4] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_SETTLEMENT_S01", objParams);

            return dsReturn;
        }

        //BaoTv
        public static DataSet SpreadDetListDetail(string strSeq)
        {
            var objParams = new object[1];
            objParams[0] = strSeq;
            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_SELECT_DEBIT_LIST_DETAIL_S00", objParams);
            return dsReturn;
        }

        #endregion 

        #region SelectExcelReceiptList : Excel용 수납내역조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelReceiptList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-18
         * 용       도 : Excel용 수납내역조회
         * Input    값 : SelectExcelReceiptList(strKind, strPaymentCd, strStartDt, strEndDt)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExcelReceiptList : Excel용 수납내역조회
        /// </summary>
        /// <param name="strKind">항목코드</param>
        /// <param name="strPaymentCd">지불코드</param>
        /// <param name="strStartDt">시작일</param>
        /// <param name="strEndDt">종료일</param>
        /// <returns></returns>
        public static DataTable SelectExcelReceiptList(string strKind, string strPaymentCd, string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[4];

            objParams[0] = TextLib.MakeNullToEmpty(strKind);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentCd);
            objParams[2] = strStartDt.Replace("-", "");
            objParams[3] = strEndDt.Replace("-", "");

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_RECEIPTLIST_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertLedgerInfo : 정산정보입력

        /**********************************************************************************************
         * Mehtod   명 : InsertLedgerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-01-17
         * 용       도 : 정산정보입력
         * Input    값 : InsertLedgerInfo(strDebitCreditCd, strPaymentDt,	intPaymentSeq, strRentCd, strDirectCd, strItemCd, intItemSeq, strUserTyCd,
         *                                  strUserSeq,	strUserTaxCd, fltDongToDollar, fltItemTotEnAmt, fltItemTotViAmt, strPaymentCd,
         *                                  fltPaymentRatio, strRemarkVi, strRemarkEn, strImCompNo, strInsMemNo, strInsMemIP)
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
        /// <param name="strInsCompNo">입력기업코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static DataTable InsertLedgerInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strRentCd, string strDirectCd, string strItemCd,
                                                 int intItemSeq, string strUserTyCd, string strUserSeq, string strUserTaxCd, double dblDongToDollar, double dblItemTotEnAmt,
                                                 double dblItemTotViAmt, string strPaymentCd, double dblPaymentRatio, string strInsCompNo, 
                                                 string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[18];

            objParams[0] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = intPaymentSeq;
            objParams[3] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[4] = TextLib.MakeNullToEmpty(strDirectCd);
            objParams[5] = TextLib.MakeNullToEmpty(strItemCd);
            objParams[6] = intItemSeq;
            objParams[7] = TextLib.MakeNullToEmpty(strUserTyCd);
            objParams[8] = TextLib.MakeNullToEmpty(strUserSeq);
            objParams[9] = TextLib.MakeNullToEmpty(strUserTaxCd);
            objParams[10] = dblDongToDollar;
            objParams[11] = dblItemTotEnAmt;
            objParams[12] = dblItemTotViAmt;
            objParams[13] = TextLib.MakeNullToEmpty(strPaymentCd);
            objParams[14] = dblPaymentRatio;
            objParams[15] = strInsCompNo;
            objParams[16] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParams[17] = TextLib.MakeNullToEmpty(strInsMemIP);            

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_LEDGERINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertLedgerDetInfo : 정산상세정보입력

        /**********************************************************************************************
         * Mehtod   명 : InsertLedgerDetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-01-18
         * 용       도 : 정산상세정보입력
         * Input    값 : InsertLedgerDetInfo(strDebitCreditCd	,strPaymentDt,intPaymentSeq,intPaymentDetSeq,strRentCd,strDirectCd,
         *                                     strItemCd,intItemSeq,intItemDetSeq,strSvcZoneCd,strClassiGrpCd,strClassiMainCd,strClassCd,
         *                                     intQty,strScaleCd,fltUnitPrimeCost,fltTotPrimeCost,fltUnitSellingPrice,fltTotSellingPrice,
         *                                     fltSvcCharge,fltVATRatio,strVATYn, string strInsCompNo, strInsMemNo,strInsMemIP)
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
        /// <param name="dblSvcCharge">실매가</param>
        /// <param name="strSvcYear">해당납부년도</param>
        /// <param name="strSvcMM">해당납부월</param>
        /// <param name="dblVATRatio">부가세율</param>
        /// <param name="strVATYn">부가세포함여부</param>
        /// <param name="strGoodsNm">상품명</param>
        /// <param name="strInsCompNo">입력기업번호</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static DataTable InsertLedgerDetInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq, string strRentCd, string strDirectCd,
                                                    string strItemCd, int intItemSeq, int intItemDetSeq, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd, 
                                                    string strClassCd, int intQty, string strScaleCd, double dblUnitPrimeCost, double dblTotPrimeCost, double dblUnitSellingPrice,
                                                    double dblTotSellingPrice, double dblSvcCharge, string strSvcYear, string strSvcMM, string strGoodsNm, double dblVATRatio,
                                                    string strVATYn, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[28];

            objParams[0] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = intPaymentSeq;
            objParams[3] = intPaymentDetSeq;
            objParams[4] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[5] = TextLib.MakeNullToEmpty(strDirectCd);
            objParams[6] = TextLib.MakeNullToEmpty(strItemCd);
            objParams[7] = intItemSeq;
            objParams[8] = intItemDetSeq;
            objParams[9] = TextLib.MakeNullToEmpty(strSvcZoneCd);
            objParams[10] = TextLib.MakeNullToEmpty(strClassiGrpCd);
            objParams[11] = TextLib.MakeNullToEmpty(strClassiMainCd);
            objParams[12] = TextLib.MakeNullToEmpty(strClassCd);
            objParams[13] = intQty;
            objParams[14] = TextLib.MakeNullToEmpty(strScaleCd);
            objParams[15] = dblUnitPrimeCost;
            objParams[16] = dblTotPrimeCost;
            objParams[17] = dblUnitSellingPrice;
            objParams[18] = dblTotSellingPrice;
            objParams[19] = dblSvcCharge;
            objParams[20] = strSvcYear;
            objParams[21] = strSvcMM;
            objParams[22] = strGoodsNm;
            objParams[23] = dblVATRatio;
            objParams[24] = TextLib.MakeNullToEmpty(strVATYn);
            objParams[25] = TextLib.MakeNullToEmpty(strInsCompNo); 
            objParams[26] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParams[27] = TextLib.MakeNullToEmpty(strInsMemIP);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_LEDGERDETINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertLedgerAddonInfo : 정산추가정보입력

        /**********************************************************************************************
         * Mehtod   명 : InsertLedgerAddonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-22
         * 용       도 : 정산추가정보입력
         * Input    값 : InsertLedgerAddonInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strAccountCd)
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
        public static DataTable InsertLedgerAddonInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strAccountCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = intPaymentSeq;
            objParams[3] = strAccountCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_LEDGERINFO_S01", objParams);

            return dtReturn;
        }

        #endregion 

        #region InsertSettelmentInfoByManual : 강제마감 처리

        /**********************************************************************************************
         * Mehtod   명 : InsertSettelmentInfoByManual
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-22
         * 용       도 : 강제마감 처리
         * Input    값 : RegistrySettelmentInfoByManual(섹션코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertSettelmentInfoByManual : 강제마감 처리
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static object[] InsertSettelmentInfoByManual(string strRentCd)
        {
            object[] objParams = new object[1];
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_SEND_SETTLEMENT_LIST_M03", objParams);

            return objReturn;
        }

        #endregion 

        #region InsertHoadonContInfo : 세금계산서대상정보입력

        /**********************************************************************************************
         * Mehtod   명 : InsertHoadonContInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-21
         * 용       도 : 세금계산서대상정보입력
         * Input    값 : InsertHoadonContInfo(strRentCd, strRoomNo, strContYn, strInsCompNo, strInsMemNo, strInsMemIP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertHoadonContInfo : 세금계산서대상정보입력
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strContYn">세금계산서대상 집주인여부</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] InsertHoadonContInfo(string strRentCd, string strRoomNo, string strContYn, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strContYn;
            objParams[3] = strInsCompNo;
            objParams[4] = strInsMemNo;
            objParams[5] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_HOADONCONTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertChestNutHoadonList : 화돈발급

        /**********************************************************************************************
         * Mehtod   명 : InsertChestNutHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-23
         * 용       도 : 화돈발급
         * Input    값 : InsertChestNutHoadonList(언어코드, 입주자번호, 사용자명, 지불일자, 지불순번, 주소, 상세주소, 입주자세금코드, 실제처리일자,
         *                                        기업명, 세금계산서대상자, 회사코드, 등록사번, 등록IP, 조회일)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertChestNutHoadonList : 화돈발급
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
        public static object[] InsertChestNutHoadonList(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt, int intPaymentSeq, string strAddr,
                                                        string strDetAddr, string strUserTaxCd, string strModPaymentDt, string strCompNm, string strInvoiceContYn, 
                                                        string strInsCompCd, string strInsMemNo, string strInsMemIP, string strNowDt)
        {
            object[] objParams = new object[15];
            object[] objReturn = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strUserSeq;
            objParams[2] = strUserNm;
            objParams[3] = strPaymentDt;
            objParams[4] = intPaymentSeq;
            objParams[5] = strAddr;
            objParams[6] = strDetAddr;
            objParams[7] = strUserTaxCd;
            objParams[8] = strModPaymentDt;
            objParams[9] = strCompNm;
            objParams[10] = strInvoiceContYn;
            objParams[11] = strInsCompCd;
            objParams[12] = strInsMemNo;
            objParams[13] = strInsMemIP;
            objParams[14] = strNowDt;

             objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_HOADONINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertKeangNamHoadonList : 화돈발급

        /**********************************************************************************************
         * Mehtod   명 : InsertKeangNamHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-26
         * 용       도 : 화돈발급
         * Input    값 : InsertKeangNamHoadonList(언어코드, 입주자번호, 사용자명, 지불일자, 지불순번, 주소, 상세주소, 입주자세금코드, 실제처리일자,
         *                                        기업명, 세금계산서대상자, 회사코드, 등록사번, 등록IP, 조회일)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertKeangNamHoadonList : 화돈발급
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
        public static object[] InsertKeangNamHoadonList(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt, int intPaymentSeq, string strAddr,
                                                        string strDetAddr, string strUserTaxCd, string strModPaymentDt, string strCompNm, string strInvoiceContYn,
                                                        string strInsCompCd, string strInsMemNo, string strInsMemIP, string strNowDt)
        {
            object[] objParams = new object[15];
            object[] objReturn = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strUserSeq;
            objParams[2] = strUserNm;
            objParams[3] = strPaymentDt;
            objParams[4] = intPaymentSeq;
            objParams[5] = strAddr;
            objParams[6] = strDetAddr;
            objParams[7] = strUserTaxCd;
            objParams[8] = strModPaymentDt;
            objParams[9] = strCompNm;
            objParams[10] = strInvoiceContYn;
            objParams[11] = strInsCompCd;
            objParams[12] = strInsMemNo;
            objParams[13] = strInsMemIP;
            objParams[14] = strNowDt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_HOADONINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region InsertReserveHoadonList : 화돈발급

        /**********************************************************************************************
         * Mehtod   명 : InsertReserveHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-23
         * 용       도 : 화돈발급
         * Input    값 : InsertReserveHoadonList(언어코드, 입주자번호, 지불일자, 지불순번, 지불상세번호, 입주자세금코드, 기업명, 주소, 등록사번
         *                                       등록IP, 조회일)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertReserveHoadonList : 화돈발급
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
        public static object[] InsertReserveHoadonList(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt, int intPaymentSeq, string strAddr,
                                                       string strDetAddr, string strUserTaxCd, string strModPaymentDt, string strCompNm, string strInvoiceContYn,
                                                       string strInsCompCd, string strInsMemNo, string strInsMemIP, string strNowDt)
        {
            object[] objParams = new object[15];
            object[] objReturn = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strUserSeq;
            objParams[2] = strUserNm;
            objParams[3] = strPaymentDt;
            objParams[4] = intPaymentSeq;
            objParams[5] = strAddr;
            objParams[6] = strDetAddr;
            objParams[7] = strUserTaxCd;
            objParams[8] = strModPaymentDt;
            objParams[9] = strCompNm;
            objParams[10] = strInvoiceContYn;
            objParams[11] = strInsCompCd;
            objParams[12] = strInsMemNo;
            objParams[13] = strInsMemIP;
            objParams[14] = strNowDt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_HOADONINFO_M02", objParams);

            return objReturn;
        }

        #endregion
        
        #region InsertAccountCdInfo : 회계계정코드 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertAccountCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-02
         * 용       도 : 회계계정코드 등록
         * Input    값 : InsertAccountCdInfo(회사코드, 섹션코드, 수납문서코드, 아이템코드, 회계코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertAccountCdInfo : 회계계정코드 등록
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strSettleCd">수납문서코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strAccCd">회계코드</param>
        /// <returns></returns>
        public static DataTable InsertAccountCdInfo(string strCompCd, string strRentCd, string strSettleCd, string strItemCd, string strAccCd)
        {
            object[] objParams = new object[5];
            DataTable dtReturn = new DataTable();

            objParams[0] = strCompCd;
            objParams[1] = strRentCd;
            objParams[2] = strSettleCd;
            objParams[3] = strItemCd;
            objParams[4] = strAccCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_ACCOUNTCDINFO_S00", objParams);

            return dtReturn;
        }

        public static DataTable RegistryAccountBankInfo(string strCompCd, string strRentCd, string feeType, string paymentType, string bankNm, string bankAccCd, int bankSeq, string usedYn, string strInsMemNo, string strInsMemIP)
        {
            var objParams = new object[9];

            objParams[0] = strCompCd;
            objParams[1] = strRentCd;
            objParams[2] = feeType;
            objParams[3] = paymentType;
            objParams[4] = bankNm;
            objParams[5] = bankAccCd;
            objParams[6] = usedYn;
            objParams[7] = bankSeq;
            objParams[8] = strInsMemNo;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_ACCOUNTCDINFO_S01", objParams);

            return dtReturn;
        }

        #endregion        

        #region UpdateLedgerDetInfoForPrint : 원장상세테이블에 프린트 번호 추가

        /**********************************************************************************************
         * Mehtod   명 : UpdateLedgerDetInfoForPrint
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-06
         * 용       도 : 원장상세테이블에 프린트 번호 추가
         * Input    값 : UpdateLedgerDetInfoForPrint(strDebitCreditCd, strPaymentDt,intPaymentSeq,intPaymentDetSeq,strPrintSeq,intPrintDetSeq)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateLedgerDetInfoForPrint : 원장상세테이블에 프린트 번호 추가
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="intPaymentDetSeq">지불일자별 상세순번</param>
        /// <param name="strPrintSeq">프린트번호</param>
        /// <param name="intPrintDetSeq">프린트상세번호</param>
        /// <returns></returns>
        public static object[] UpdateLedgerDetInfoForPrint(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq, string strPrintSeq, int intPrintDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = intPaymentSeq;
            objParams[3] = intPaymentDetSeq;
            objParams[4] = TextLib.MakeNullToEmpty(strPrintSeq);
            objParams[5] = intPrintDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_UPDATE_LEDGERDETINFO_M00", objParams);

            return objReturn;
        }

        #endregion 

        #region UpdateLedgerinfoForTaxCd: 원장테이블에 세금코드 추가

        /**********************************************************************************************
         * Mehtod   명 : UpdateLedgerinfoForTaxCd
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-16
         * 용       도 : 원장테이블에 세금코드 추가
         * Input    값 : UpdateLedgerinfoForTaxCd(strDebitCreditCd, strPaymentDt, intPaymentSeq, strUserTaxCd)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateLedgerinfoForTaxCd : 원장테이블에 세금코드 추가
        /// </summary>
        /// <param name="strDebitCreditCd">차대변코드 </param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자별 순번</param>
        /// <param name="strUserTaxCd">세금계산서코드</param>
        /// <returns></returns>
        public static object[] UpdateLedgerinfoForTaxCd(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strUserTaxCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = intPaymentSeq;
            objParams[3] = TextLib.MakeNullToEmpty(strUserTaxCd);

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_SETTLEMENT_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateTaxInfo: 세금관련 정보 변경

        /**********************************************************************************************
         * Mehtod   명 : UpdateTaxInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-25
         * 용       도 : 세금관련 정보 변경
         * Input    값 : UpdateTaxInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strNm, strUserTaxCd, strTaxAddr, strTaxDetAddr, strInvoiceContYn)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateTaxInfo: 세금관련 정보 변경
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
        public static object[] UpdateTaxInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strNm, string strUserTaxCd, string strTaxAddr,
                                             string strTaxDetAddr, string strInvoiceContYn)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[8];

            objParams[0] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = intPaymentSeq;
            objParams[3] = TextLib.MakeNullToEmpty(strNm);
            objParams[4] = TextLib.MakeNullToEmpty(strUserTaxCd);
            objParams[5] = TextLib.MakeNullToEmpty(strTaxAddr);
            objParams[6] = TextLib.MakeNullToEmpty(strTaxDetAddr);
            objParams[7] = TextLib.MakeNullToEmpty(strInvoiceContYn);

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_SETTLEMENT_M01", objParams);

            return objReturn;
        }

        public static object[] ModifyDebit(string rentCd, string feeTy, string roomNo, string strSeq, string sDt, string eDt, string payDt, string issueDt, double exRate, double feeAmount,double discount, double total, string subDes)
        {
            var objParams = new object[13];
            objParams[0] = strSeq;
            objParams[1] = sDt;
            objParams[2] = eDt;
            objParams[3] = payDt;
            objParams[4] = issueDt;
            objParams[5] = exRate;
            objParams[6] = feeAmount;
            objParams[7] = rentCd;
            objParams[8] = roomNo;
            objParams[9] = feeTy;
            objParams[10] = total;
            objParams[11] = discount;
            objParams[12] = subDes;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_DEBIT_M001", objParams);
            return objReturn;
        }

        #endregion 

        #region UpdateEntireTaxInfo: 세금관련 정보 변경

        /**********************************************************************************************
         * Mehtod   명 : UpdateEntireTaxInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-25
         * 용       도 : 세금관련 정보 변경
         * Input    값 : UpdateEntireTaxInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strNm, strUserTaxCd, strTaxAddr, strTaxDetAddr, strInvoiceContYn)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateEntireTaxInfo: 세금관련 정보 변경
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
        public static object[] UpdateEntireTaxInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strNm, string strUserTaxCd, string strTaxAddr,
                                                   string strTaxDetAddr, string strInvoiceContYn)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[8];

            objParams[0] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = intPaymentSeq;
            objParams[3] = TextLib.MakeNullToEmpty(strNm);
            objParams[4] = TextLib.MakeNullToEmpty(strUserTaxCd);
            objParams[5] = TextLib.MakeNullToEmpty(strTaxAddr);
            objParams[6] = TextLib.MakeNullToEmpty(strTaxDetAddr);
            objParams[7] = TextLib.MakeNullToEmpty(strInvoiceContYn);

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_SETTLEMENT_M02", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateHoadonContInfo : 세금계산서대상정보수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateHoadonContInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-21
         * 용       도 : 세금계산서대상정보수정
         * Input    값 : UpdateHoadonContInfo(strRentCd, strRoomNo, strContYn, strInsCompNo, strInsMemNo, strInsMemIP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateHoadonContInfo : 세금계산서대상정보수정
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strContYn">세금계산서대상 집주인여부</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] UpdateHoadonContInfo(string strRentCd, string strRoomNo, string strContYn, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strContYn;
            objParams[3] = strInsCompNo;
            objParams[4] = strInsMemNo;
            objParams[5] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_HOADONCONTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateAccountCdInfo : 회계계정코드 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateAccountCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-02
         * 용       도 : 회계계정코드 수정
         * Input    값 : UpdateAccountCdInfo(회사코드, 섹션코드, 수납문서코드, 아이템코드, 회계코드, 계정코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateAccountCdInfo : 회계계정코드 수정
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strSettleCd">수납문서코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strAccountCd">회계코드</param>
        /// <param name="strAccCd">계정코드</param>
        /// <returns></returns>
        public static object[] UpdateAccountCdInfo(string strCompCd, string strRentCd, string strSettleCd, string strItemCd, string strAccountCd, string strAccCd)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strCompCd;
            objParams[1] = strRentCd;
            objParams[2] = strSettleCd;
            objParams[3] = strItemCd;
            objParams[4] = strAccountCd;
            objParams[5] = strAccCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_ACCOUNTCDINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteLedgerMng : 정산정보삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteLedgerMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 정산정보삭제
         * Input    값 : DeleteLedgerMng(대차코드, 지불일자, 지불순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 정산정보삭제
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <returns></returns>
        public static object[] DeleteLedgerMng(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[3];

            objParams[0] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParams[1] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParams[2] = intPaymentSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_DELETE_LEDGERINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteAccountCdInfo : 회계계정코드 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteAccountCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-02
         * 용       도 : 회계계정코드 삭제
         * Input    값 : DeleteAccountCdInfo(회사코드, 섹션코드, 수납문서코드, 아이템코드, 계정코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteAccountCdInfo : 회계계정코드 삭제
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strSettleCd">수납문서코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strAccountCd">계정코드</param>
        /// <returns></returns>
        public static object[] DeleteAccountCdInfo(string strCompCd, string strRentCd, string strSettleCd, string strItemCd, string strAccountCd)
        {
            object[] objParams = new object[5];
            object[] objReturn = new object[2];

            objParams[0] = strCompCd;
            objParams[1] = strRentCd;
            objParams[2] = strSettleCd;
            objParams[3] = strItemCd;
            objParams[4] = strAccountCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_ACCOUNTCDINFO_M00", objParams);

            return objReturn;
        }

        //BaoTV
        public static object[] DeleteBankAccountInfo(string strCompCd, string strRentCd, string feeTy, string paymentTy, int bankSeq)
        {
            var objParams = new object[5];

            objParams[0] = strCompCd;
            objParams[1] = strRentCd;
            objParams[2] = feeTy;
            objParams[3] = paymentTy;
            objParams[4] = bankSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_ACCOUNTCDINFO_M01", objParams);

            return objReturn;
        }

        #endregion
    }
}
