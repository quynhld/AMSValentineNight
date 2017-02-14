using System.Data;

using KN.Manage.Dac;
using KN.Common.Base.Code;
using KN.Manage.Ent;

namespace KN.Manage.Biz
{
    public class MngPaymentBlo
    {
        #region WatchRentalMngFeeCalendar : 관리비 및 임대료용 달력(년도) 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchRentalMngFeeCalendar
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-01
         * 용       도 : 관리비 및 임대료용 달력(년도) 조회
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 관리비 및 임대료용 달력(년도) 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns></returns>
        public static DataTable WatchRentalMngFeeCalendar(string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectRentalMngFeeCalendar(strRentCd);

            return dtReturn;
        }

        #endregion

        #region WatchRentalMngFeeCalendar : 관리비 및 임대료용 달력(월) 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchRentalMngFeeCalendar
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-01
         * 용       도 : 관리비 및 임대료용 달력(월) 조회
         * Input    값 : 년도
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 관리비 및 임대료용 달력(월) 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도</param>
        /// <returns></returns>
        public static DataTable WatchRentalMngFeeCalendar(string strRentCd, string strYear)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectRentalMngFeeCalendar(strRentCd, strYear);

            return dtReturn;
        }

        #endregion

        #region SpreadMngPaymentList : 월별 수납/미수 현황 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngPaymentList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-16
         * 용       도 : 월별 수납/미수 현황 리스트 조회
         * Input    값 : SpreadMngPaymentList(페이지별 리스트 크기, 현재페이지, 임대구분코드, 비용대상, 이름, 검색층, 검색호, 수납여부, 연체여부, 년, 월)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납/미수 현황 리스트 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strTenantNm">이름</param>
        /// <param name="intFloorNo">검색층</param>
        /// <param name="strRoomNo">검색호</param>
        /// <param name="strReceitYn">수납여부</param>
        /// <param name="strLateYn">연체여부</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <returns></returns>
        public static DataSet SpreadMngPaymentList(int intPageSize, int intNowPage, string strRentCd, string strFeeTy, string strTenantNm, int intFloorNo, string strRoomNo,
                                                   string strReceitYn, string strLateYn, string strRentalYear, string strRentalMM, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MngPaymentDao.SelectMngPaymentList(intPageSize, intNowPage, strRentCd, strFeeTy, strTenantNm, intFloorNo, strRoomNo, strReceitYn, strLateYn, strRentalYear, strRentalMM, strLangCd);

            return dsReturn;
        }

        #endregion

        #region SpreadMngMenuinfo : 관리비 및 임대료 항목 관리

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngMenuinfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 관리
         * Input    값 : strRentCd, strFeeTy
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMngMenuinfo : 관리비 및 임대료 항목 관리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <returns></returns>
        public static DataTable SpreadMngMenuinfo(string strRentCd, string strFeeTy)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectMngMenuinfo(strRentCd, strFeeTy);

            return dtReturn;
        }

        #endregion

        #region WatchPaymentInfo : 월별 수납/미수 현황 상세보기

        /**********************************************************************************************
         * Mehtod   명 : WatchPaymentInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-17
         * 용       도 : 월별 수납/미수 현황 상세보기
         * Input    값 : WatchPaymentInfo(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납/미수 현황 상세보기
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">순번</param>
        /// <returns></returns>
        public static DataSet WatchPaymentInfo(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MngPaymentDao.SelectMngPaymentInfo(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

            return dsReturn;
        }
        //Baotv
        public static DataSet ListPaymentInfo(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName,string strEDt,string strPayment)
        {
            var dsReturn = MngPaymentDao.GetsMngPaymentInfo(strRentCd, strFeeTy, strDt, strRoomNo, strName,strEDt,strPayment);

            return dsReturn;
        }

        //Baotv
        public static DataSet ListPaymentInfoApt(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strPayment)
        {
            var dsReturn = MngPaymentDao.GetsMngPaymentInfoApt(strRentCd, strFeeTy, strDt, strRoomNo, strName, strEDt, strPayment);

            return dsReturn;
        }

        //Baotv
        public static DataSet ListRenovationInfoApt(string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt)
        {
            var dsReturn = MngPaymentDao.GetsMngRenovationInfoApt(strFeeTy, strDt, strRoomNo, strName, strEDt);

            return dsReturn;
        }

        #region SelectExcelRenovationAndCarCard

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelRenovationAndCarCard
         * 개   발  자 : PhuongTV
         * 생   성  일 : 2014-04-28
         * 용       도 : 
         * Input    값 : SelectExcelRenovationAndCarCard(strFeeTy, strDt, strRoomNo, strName , strEDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelRenovationAndCarCard(string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MngPaymentDao.SelectExcelRenovationAndCarCard(strFeeTy, strDt, strRoomNo, strName, strEDt);

            return dsReturn;
        }


        #endregion

        //Baotv
        public static DataSet ListPaymentInfoAptForMerge(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strFeeTyDt)
        {
            var dsReturn = MngPaymentDao.ListPaymentInfoAptForMerge(strRentCd, strFeeTy, strDt, strRoomNo, strName, strEDt, strFeeTyDt);

            return dsReturn;
        }

        #region Export Excel List Merged Invoice

        public static DataSet ListMergedInvoice(string strInvoiceNo)
        {
            var dsReturn = MngPaymentDao.ListMergedInvoice(strInvoiceNo);

            return dsReturn;
        }

        #endregion

        //Baotv
        public static DataSet ListPaymentInfoAptForMergeExcel(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strFeeTyDt)
        {
            var dsReturn = MngPaymentDao.ListPaymentInfoAptForMergeExcel(strRentCd, strFeeTy, strDt, strRoomNo, strName, strEDt, strFeeTyDt);

            return dsReturn;
        }

        //Baotv
        public static DataSet ListPaymentInfoAptForAdjust(string strRentCd, string strFeeTy,string feeTyDt,string strPeriod, string strRoomNo, string paidDt)
        {
            var dsReturn = MngPaymentDao.ListPaymentInfoAptForAdjust(strRentCd, strFeeTy, feeTyDt, strPeriod, strRoomNo, paidDt);

            return dsReturn;
        }

        //Baotv
        public static DataSet ListPaymentInfoAptForAdjustExcel(string strRentCd, string strFeeTy, string feeTyDt, string strPeriod, string strRoomNo, string paidDt, string invoiceNo)
        {
            var dsReturn = MngPaymentDao.ListPaymentInfoAptForAdjustExcel(strRentCd, strFeeTy, feeTyDt, strPeriod, strRoomNo, paidDt,invoiceNo);

            return dsReturn;
        }

        //BaoTv
        public static DataSet ListPaymentDetails(int seq,string strSeq)
        {
            var dsReturn = MngPaymentDao.ListPaymentDetails(seq,strSeq);

            return dsReturn;
        }

        //BaoTv
        public static DataSet ListPaymentAptDetails(int seq, string strSeq)
        {
            var dsReturn = MngPaymentDao.ListPaymentAptDetails(seq, strSeq);

            return dsReturn;
        }

        public static DataSet ListReceivableInfo(string strRentCd, string strFeeTy, string strRoomNo)
        {
            var dsReturn = MngPaymentDao.ListReceivableInfo(strRentCd, strFeeTy, strRoomNo);

            return dsReturn;
        }

        public static DataSet ListReceivableAptInfo(string strRentCd, string strFeeTy, string strRoomNo)
        {
            var dsReturn = MngPaymentDao.ListReceivableAptInfo(strRentCd, strFeeTy, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region WatchPaymentInfoList : 월별 수납 현황 리스트

        /**********************************************************************************************
         * Mehtod   명 : WatchPaymentInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-17
         * 용       도 : 수납 입력
         * Input    값 : WatchPaymentInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납 현황 리스트
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">순번</param>
        /// <returns></returns>
        public static DataTable WatchPaymentInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectPaymentInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

            return dtReturn;
        }

        #endregion

        #region WatchExpenceInfoList : 상세 관리비 금액(평당)

        /**********************************************************************************************
         * Mehtod   명 : WatchExpenceInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-20
         * 용       도 : 상세 관리비 금액(평당)
         * Input    값 : WatchExpenceInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strLangCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 상세 관리비 금액(평당)
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable WatchExpenceInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectExpenceInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strLangCd);

            return dtReturn;
        }

        #endregion

        #region WatchIncompleteList : 미완전납부처리 사유조회

        /**********************************************************************************************
         * Mehtod   명 : WatchIncompleteList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-07
         * 용       도 : 미완전납부처리 사유조회
         * Input    값 : WatchIncompleteList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 미완전납부처리 사유조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">사원번호</param>
        /// <returns></returns>
        public static DataTable WatchIncompleteList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectIncompleteList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

            return dtReturn;
        }

        #endregion

        #region WatchMngMenuItemCheck : 관리비 및 임대료 항목 중복 체크

        /**********************************************************************************************
         * Mehtod   명 : WatchMngMenuItemCheck
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 중복 체크
         * Input    값 : 임대구분코드, 항목타입, 항목코드
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchMngMenuItemCheck : 관리비 및 임대료 항목 중복 체크
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">항목타입</param>
        /// <param name="strMngFeeCd">관리비 및 임대료코드</param>
        /// <returns></returns>
        public static DataTable WatchMngMenuItemCheck(string strRentCd, string strFeeTy, string strMngFeeCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectMngMenuItemCheck(strRentCd, strFeeTy, strMngFeeCd);

            return dtReturn;
        }

        #endregion

        #region WatchLateFeeInfo : 연체료 현황 상세보기

        /**********************************************************************************************
         * Mehtod   명 : WatchLateFeeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-16
         * 용       도 : 월별 수납/미수 현황 상세보기
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 연체료 현황 상세보기
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">순번</param>
        /// <returns></returns>
        public static DataSet WatchLateFeeInfo(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MngPaymentDao.SelectLateFeeInfo(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

            return dsReturn;
        }

        #endregion 

        #region SpreadLateFeeRatioList : 연체요율 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadLateFeeRatioList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-10
         * 용       도 : 연체요율 리스트 조회
         * Input    값 : SpreadLateFeeRatioList(임대구분코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 연체요율 리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">관리비 및 임대료 요금타입</param>
        /// <returns></returns>
        public static DataTable SpreadLateFeeRatioList(string strRentCd, string strFeeTy)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectLateFeeRatioList(strRentCd, strFeeTy);

            return dtReturn;
        }

        #endregion

        #region SpreadLateFeeStartDtCheck : 연체요율 적용일 체크

        /**********************************************************************************************
         * Mehtod   명 : SpreadLateFeeStartDtCheck
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-08
         * 용       도 : 연체요율 적용일 체크
         * Input    값 : strRentCd, strFeeTy, strLateFeeStartDt
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 연체요율 적용일 체크
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strLateFeeStartDt">연체시작일</param>
        /// <returns></returns>
        public static DataTable SpreadLateFeeStartDtCheck(string strRentCd, string strFeeTy, string strLateFeeStartDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectLateFeeStartDtCheck(strRentCd, strFeeTy, strLateFeeStartDt);

            return dtReturn;
        }

        #endregion  

        #region SpreadMngPaymentList : 연체료 현황 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadLateFeeList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-20
         * 용       도 : 연체료 현황 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strFeeTy, strTenantNm, intFloorNo, strRoomNo, strRentalYear, strRentalMM
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 연체료 현황 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strTenantNm">이름</param>
        /// <param name="intFloorNo">검색층</param>
        /// <param name="strRoomNo">검색호</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <returns></returns>
        public static DataSet SpreadLateFeeList(int intPageSize, int intNowPage, string strRentCd, string strFeeTy, string strTenantNm, int intFloorNo, string strRoomNo, string strRentalYear, string strRentalMM)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MngPaymentDao.SelectLateFeeList(intPageSize, intNowPage, strRentCd, strFeeTy, strTenantNm, intFloorNo, strRoomNo, strRentalYear, strRentalMM);

            return dsReturn;
        }

        #endregion

        #region WatchLateFeeInfoList : 월별 연체료 납부 현황 리스트

        /**********************************************************************************************
         * Mehtod   명 : WatchLateFeeInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-20
         * 용       도 : 수납 입력
         * Input    값 : WatchLateFeeInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 연체료 납부 현황 리스트
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">순번</param>
        /// <returns></returns>
        public static DataTable WatchLateFeeInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectLateFeeInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

            return dtReturn;
        }

        #endregion   

        #region SpreadMngInfoList : 월별 수납항목 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-08
         * 용       도 : 월별 수납항목 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strFeeTy, strYear
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadMngInfoList : 월별 수납항목 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <returns></returns>
        public static DataSet SpreadMngInfoList(int intPageSize, int intNowPage, string strRentCd, string strFeeTy, string strYear)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MngPaymentDao.SelectMngInfoList(intPageSize, intNowPage, strRentCd, strFeeTy, strYear);

            return dsReturn;
        }

        #endregion

        #region SpreadMngInfoList : 월별 수납항목 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-08
         * 용       도 : 월별 수납항목 조회
         * Input    값 : intPageSize, strRentCd, strFeeTy, strYear
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadMngInfoList : 월별 수납항목 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <returns></returns>
        public static DataSet SpreadMngInfoList(int intPageSize, string strRentCd, string strFeeTy, string strYear)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MngPaymentDao.SelectMngInfoList(intPageSize, strRentCd, strFeeTy, strYear);

            return dsReturn;
        }

        #endregion

        #region SpreadMngInfo : 월별 수납 항목상세보기

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-09
         * 용       도 : 월별 수납 항목상세보기
         * Input    값 : strLangCd, strRentCd, strFeeTy, strMngYear, strMngMM
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납 항목상세보기
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <returns></returns>
        public static DataTable SpreadMngInfo(string strLangCd, string strRentCd, string strFeeTy, string strMngYear, string strMngMM)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectMngInfo(strLangCd, strRentCd, strFeeTy, strMngYear, strMngMM);

            return dtReturn;
        }

        #endregion

        #region SpreadMngInfo : 월별 수납 항목상세보기

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-09
         * 용       도 : 월별 수납 항목상세보기
         * Input    값 : strLangCd, strRentCd, strFeeTy
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납 항목상세보기
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <returns></returns>
        public static DataSet SpreadMngInfo(string strLangCd, string strRentCd, string strFeeTy)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MngPaymentDao.SelectMngInfo(strLangCd, strRentCd, strFeeTy);

            return dsReturn;
        }

        #endregion

        #region SpreadMngFeeMonthSetInfo: 관리비 청구월 세팅

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngFeeMonthSetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-14
         * 용       도 : 관리비 청구월 세팅
         * Input    값 : SpreadMngFeeMonthSetInfo(섹션코드, 적용년, 적용월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMngFeeMonthSetInfo: 관리비 청구월 세팅
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strAppliedYear">적용년</param>
        /// <param name="strAppliedMonth">적용월</param>
        /// <returns></returns>
        public static DataTable SpreadMngFeeMonthSetInfo(string strRentCd, string strAppliedYear, string strAppliedMonth)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.SelectMngFeeMonthSetInfo(strRentCd, strAppliedYear, strAppliedMonth);

            return dtReturn;
        }

        #endregion

        #region SelectUploadAPTMFList 
        /**********************************************************************************************
         * Mehtod   명 : SelectUploadAPTMFList
         * 개   발  자 : PhuongTV
         * 생   성  일 : 2014-06-25
         * 용       도 : 
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/

        public static DataTable SelectUploadAPTMFList(string strRentCd, string strRoomNo, string strYear, string strMonth)
        {
            //KN_MN_GET_UPLOAD_MNGFEE_APT_S00
            var dtReturn = MngPaymentDao.SelectUploadAPTMFList(strRentCd, strRoomNo, strYear, strMonth);
            return dtReturn;
        }

        #endregion


        #region SpreadManuallyRegistList : 수동생성 대상조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadManuallyRegistList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-10
         * 용       도 : 수동생성 대상조회
         * Input    값 : SpreadManuallyRegistList(아이템코드, 임대구분코드, 해당년, 해당월)
         * Ouput    값 : DataTable
         **********************************************************************************************/

        /// <summary>
        /// SpreadManuallyRegistList : 수동생성 대상조회
        /// </summary>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <param name="strMonthE"> </param>
        /// <param name="isTenant"> </param>
        /// <param name="strDay"> </param>
        /// <param name="strYearE"> </param>
        /// <param name="roomNo"> </param>
        /// <param name="strDayE"> </param>
        /// <param name="strRoomNo"> </param>
        /// <returns></returns>
        ///  BaoTv
        public static DataTable SpreadManuallyRegistList(string strItemCd, string strRentCd, string strYear, string strMonth, string strDay, string strYearE, string strMonthE, string strDayE, string strRoomNo, string issStartDt, string issEndDt)
        {
            var dtReturn = new DataTable();

            if (strItemCd.Equals(CommValue.RECEIT_VALUE_MNGFEE))
            {
                //KN_USP_GETS_MNGFEE_LIST_S001
                dtReturn = MngPaymentDao.SelectManuallyMngFeeList(strRentCd, strYear, strMonth, strDay, strYearE, strMonthE, strDayE, strRoomNo, issStartDt, issEndDt);
            }
            else if (strItemCd.Equals(CommValue.RECEIT_VALUE_UTILFEE))
            {
                // 수도 및 전기세 수동생성
                //dtReturn = RemoteMngDao.SelectManuallyRegistList(strRentCd, strYear, strMonth);
            }
            else if (strItemCd.Equals(CommValue.RECEIT_VALUE_RENTALFEE))
            {
                //KN_USP_GETS_RENTFEE_LIST_S001
                dtReturn = MngPaymentDao.SelectManuallyRentFeeList(strRentCd, strYear, strMonth, strDay, strYearE, strMonthE, strDayE, strRoomNo, issStartDt, issEndDt);
            }
            return dtReturn;
        }
        //Baotv
        public static DataTable SpreadAptDebitList(string strItemCd, string strTime, string strTenantNm, string strRoomNo, string strIsDebit)
        {
            //KN_SCR_SELECT_APT_DEBIT_LIST
            var dtReturn = MngPaymentDao.SelectAptDebitList(strItemCd, strTime, strTenantNm, strRoomNo, strIsDebit);
            return dtReturn;
        }

        //BaoTv
        public static DataTable SpreadManuallyCreatedDebitList(string strItemCd, string strYear, string strMonth, string strYearE, string stMonthE, string isPrinted,string rentCode)
        {
            //KN_USP_AGT_MAKE_RENTFEE_CREATED_DEBIT_LIST_M00
            var dtReturn = MngPaymentDao.SelectManuallyCreatedDebitList(strItemCd, strYear, strMonth, strYearE, stMonthE, isPrinted,rentCode);
            return dtReturn;
        }

        public static DataTable SelectSpecialDebitList(string roomNo, string compNm, string feeTy, string rentCd, string StartDt, string EndDt, string printedYN, string refSeq)
        {
            //KN_USP_AGT_MAKE_RENTFEE_CREATED_DEBIT_LIST_M00
            var dtReturn = MngPaymentDao.SelectSpecialDebitList(roomNo, compNm, feeTy, rentCd, StartDt, EndDt, printedYN, refSeq);
            return dtReturn;
        }

        #region Select APTParkingCmpInfo
        public static DataTable SelectAPTParkingCmpInfo(string strRoomNo, string strCompNm, string strCarNo, string strParkingCardNo, string strLangType, string strCarTy)
        {
            //KN_USP_PRK_SELECT_APTParkingCmpInfo_S00
            var dtReturn = MngPaymentDao.SelectAPTParkingCmpInfo(strRoomNo, strCompNm, strCarNo, strParkingCardNo, strLangType, strCarTy);
            return dtReturn;
        }

        public static DataTable SelectDetailAPTParkingCmpInfo(string strRoomNo, string strParkingTagNo, string strLangType)
        {
            //KN_USP_PRK_SELECT_APTParkingCmpInfo_S01
            var dtReturn = MngPaymentDao.SelectDetailAPTParkingCmpInfo(strRoomNo, strParkingTagNo, strLangType);
            return dtReturn;
        }
        #endregion

        public static DataTable SelectSpecialDebitListDetail(string refSeq, string printedYN)
        {
            //KN_USP_AGT_MAKE_RENTFEE_CREATED_DEBIT_LIST_M00
            var dtReturn = MngPaymentDao.SelectSpecialDebitListDetail(refSeq, printedYN);
            return dtReturn;
        }

        //BaoTv
        public static DataTable SpreadManuallyPrintingList(string rentCode,string roomNo,string strItemCd, string strNm, string StartDt, string EndDt, string printedYN)
        {
            var dtReturn = MngPaymentDao.SelectManuallyPrintingList(rentCode, roomNo, strItemCd, strNm, StartDt, EndDt, printedYN);
            return dtReturn;
        }

        #region Select hidden printed debit note

        public static DataTable SelectHiddenPrintedDebit(string rentCode, string roomNo, string strItemCd, string strNm, string StartDt, string EndDt, string printedYN)
        {
            var dtReturn = MngPaymentDao.SelectHiddenPrintedDebit(rentCode, roomNo, strItemCd, strNm, StartDt, EndDt, printedYN);
            return dtReturn;
        }

        #endregion

        // Cancel Printing List - baoTV
        public static object[] CancelPrintingList(string rentCd, string userSeq, string feeTy, string strRoomNo, string bundleSeq)
        {
            var objReturn = MngPaymentDao.CancelPrintingList(rentCd, userSeq, feeTy,strRoomNo, bundleSeq);
            return objReturn;
        }

        public static object[] CancelSpecialDebit(string feeTy, string bundleSeq)
        {
            var objReturn = MngPaymentDao.CancelSpecialDebit(feeTy, bundleSeq);
            return objReturn;
        }

        // Cancel Created Debit List - baoTV
        public static object[] CancelCreatedList(string strRentCd, string userSeq, string strFeeTy, string strYear, string strMonth,string strBundleSeq)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.CancelCreatedList(strRentCd, userSeq,strFeeTy,strYear,strMonth,strBundleSeq);

            return objReturn;
        }
        // Cancel Merge and Individual Debit List - baoTV
        public static object[] MakeMergeIndividualBilling(string userSeq, string strRentCd, string strFeeTy, string debitName, int floorNo, string roomNo, double leasingArea, string tenantNm, int terMonth, double dongToDollar, double monthViAmtNo, double realMonthViAmtNo, string sDate, string Edate, double unPaidAmount, string refUserSeq, string refYear, string refMonth, string strBundleSeq, string strIssuingDate, string strMembNo, string strPaymentDate)
        {
            var objReturn = MngPaymentDao.MakeMergeIndividualBilling(userSeq, strRentCd, strFeeTy, debitName, floorNo, roomNo, leasingArea, tenantNm, terMonth, dongToDollar, monthViAmtNo, realMonthViAmtNo, sDate, Edate, unPaidAmount, refUserSeq, refYear, refMonth, strBundleSeq, strIssuingDate, strMembNo, strPaymentDate);

            return objReturn;
        }

        #region CancelAPTParkingCmpInfo

        public static object[] CancelAPTParkingCmpInfo(string strRoomNo, string strTagNo, string strCarNo)
        {
            var objReturn = MngPaymentDao.CancelAPTParkingCmpInfo(strRoomNo, strTagNo, strCarNo);
            return objReturn;        
        }

        #endregion

        #region Insert Special Debit

        public static object[] MakeSpecialDebit(string strRentCd, string strFeeTy, string strRoomNo, string strTenantNm, string strStartDt, string strEndDt, string strPaymentDt, string strIssDt, double dbmonthViAmtNo, double dbrealMonthViAmtNo, double dbExRate, double dbVatAmt, string strDesVi,string strDesEng, string debitTy, string requestDt, double dbQty, double dbUnitPrice )
        {
            var objReturn = MngPaymentDao.MakeSpecialDebit(strRentCd, strFeeTy, strRoomNo, strTenantNm, strStartDt, strEndDt, strPaymentDt, strIssDt, dbmonthViAmtNo, dbrealMonthViAmtNo, dbExRate, dbVatAmt, strDesVi, strDesEng, debitTy, requestDt, dbQty, dbUnitPrice);

            return objReturn;
        }
        #endregion

        public static object[] InsertSpecialDebitToHoadonInfo(string strTempDocNo)
        {
            var objReturn = MngPaymentDao.InsertSpecialDebitToHoadonInfo(strTempDocNo);

            return objReturn;
        }

        #region Insert PrintoutMergeInvoice

        public static DataTable InsertPrintoutInvoiceMerge(string strPaydt, string strPrintdt, double dbInvAmt, string strComNo, string strInsMemNo, string strInsMemIP)
        {

            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.InsertPrintoutInvoiceMerge(strPaydt, strPrintdt, dbInvAmt, strComNo, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region Insert APTParkingCmpInfo

        public static object[] InsertAptCmpInfo(string strRoomNo, string ParkingTagNo, string strParkingCardNo, string strParkingCarNo, string strCarTyCd, string strTaxCd, string strCmpNm, string strAddr, string strUserDetAddr, string strComNo, string strInsMemNo, string strInsMemIP)
        {
            var objReturn = MngPaymentDao.InsertAptCmpInfo(strRoomNo, ParkingTagNo, strParkingCardNo, strParkingCarNo, strCarTyCd, strTaxCd, strCmpNm, strAddr, strUserDetAddr, strComNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region UpdateHoadonPrintoutMerge

        public static DataTable UpdatingHoadonPrintMerge(string strRefPrint, string strComNo, string strInsMemNo, string strInsMemIP)
        {
            var dtReturn = MngPaymentDao.UpdatingHoadonPrintMerge(strRefPrint,  strComNo,  strInsMemNo,  strInsMemIP);
            return dtReturn;
        }

        #endregion

        public static DataTable SelectStatementList(string listType, string rentType, string searchDate, string sendCode)        
        {
            var dtReturn = MngPaymentDao.SelectStatementList(listType, rentType, searchDate, sendCode);
            return dtReturn;
        }

        public static DataTable SelectCancelCode(string typeCode, string langCode, string commNo)
        {
            var dtReturn = MngPaymentDao.SelectCancelCode(typeCode, langCode, commNo);
            return dtReturn;
        }

        public static DataTable SelectPayTypeDdl(string sbLangCd, string typeCode)
        {
            var dtReturn = MngPaymentDao.SelectPayTypeDdl(sbLangCd, typeCode);
            return dtReturn;
        }

        #endregion

        #region RegistryPaymentInfo : 월별 수납 현황 등록(납부방법포함)

        /**********************************************************************************************
         * Mehtod   명 : RegistryPaymentInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-22
         * 용       도 : 수납 입력
         * Input    값 : RegistryPaymentInfo(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strPayAmt, strPayDt, strPaymentCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납 현황 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">순번</param>
        /// <param name="strPayAmt">금액</param>
        /// <param name="strPayDt">납부날짜</param>
        /// <param name="strCompNo"></param>
        /// <param name="strMemNo"></param>
        /// <param name="strMemIP"></param>
        /// <param name="strPaymentCd">납부방법</param>
        /// <returns></returns>
        public static DataTable RegistryPaymentInfo(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq, string strPayAmt,
                                                    string strPayDt, string strCompNo, string strMemNo, string strMemIP, string strPaymentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.InsertPaymentInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strPayAmt, strPayDt, strCompNo, strMemNo, strMemIP, strPaymentCd);

            return dtReturn;
        }

        //BaoTv
        public static DataTable InsertPaymentInfo(string strPayCd,int intBankSeq,string strUserSeq,string strRoomNo,string strRentCd, string strFeeTy, string strFeeTyDetails, string strPSeq, int intSeq, string strPayDt,
                                                    string strExRate,string strMoneyCd,double totalAmount, string strMemNo, string strMemIP,string refSeq,string billType)
        {
            var dtReturn = MngPaymentDao.InsertPayment(strPayCd, intBankSeq, strUserSeq, strRoomNo, strRentCd, strFeeTy, strFeeTyDetails, strPSeq, intSeq, strPayDt, strExRate, strMoneyCd, totalAmount, strMemNo, strMemIP,refSeq,billType);

            return dtReturn;
        }
        //BaoTv
        public static DataTable InsertPaymentInfoApt(string strPayCd, int intBankSeq, string strUserSeq, string strRoomNo, string strRentCd, string strFeeTy, string strFeeTyDetails, string strPSeq, int intSeq, string strPayDt,
                                                    string strExRate, string strMoneyCd, double totalAmount, string strMemNo, string strMemIP, string refSeq,string paymentTy)
        {
            var dtReturn = MngPaymentDao.InsertPaymentApt(strPayCd, intBankSeq, strUserSeq, strRoomNo, strRentCd, strFeeTy, strFeeTyDetails, strPSeq, intSeq, strPayDt, strExRate, strMoneyCd, totalAmount, strMemNo, strMemIP, refSeq, paymentTy);

            return dtReturn;
        }
        public static DataTable InsertRevertPaymentApt(string strPSeq, int intSeq, string strPayDt, double totalAmount, string strMemNo, string strMemIP)
        {
            var dtReturn = MngPaymentDao.InsertRevertPaymentApt( strPSeq, intSeq, strPayDt, totalAmount, strMemNo, strMemIP);

            return dtReturn;
        }
        //BaoTv
        public static DataTable InsertRenovationInfoApt(string strPayCd, int intBankSeq, string strRoomNo, string strFeeTy, string strPayDt,
                                                    string strExRate, string strMoneyCd, double totalAmount, string strMemNo, string strMemIP,string cardNo)
        {
            var dtReturn = MngPaymentDao.InsertRenovationInfoApt(strPayCd, intBankSeq, strRoomNo, strFeeTy, strPayDt, strExRate, strMoneyCd, totalAmount, strMemNo, strMemIP,cardNo);

            return dtReturn;
        }

        #endregion

        #region RegistryMngFeeItem : 관리비 및 임대료 항목 추가

        /**********************************************************************************************
         * Mehtod   명 : RegistryMngFeeItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 추가
         * Input    값 : strRentCd, strFeeTy, strMngFeeCd, strMngFeeNmEn, strMngFeeNmVi, strMngFeeNmKr, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryMngFeeItem : 관리비 및 임대료 항목 추가
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngFeeCd">관리비 및 임대료 항목코드</param>
        /// <param name="strMngFeeNmEn">영어관리비항목</param>
        /// <param name="strMngFeeNmVi">베트남관리비항목</param>
        /// <param name="strMngFeeNmKr">한국어관리비항목</param>
        /// <param name="strInsCompNo">회사번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static DataTable RegistryMngFeeItem(string strRentCd, string strFeeTy, string strMngFeeCd, string strMngFeeNmEn, string strMngFeeNmVi, string strMngFeeNmKr,
                                                   string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.InsertMngFeeItem(strRentCd, strFeeTy, strMngFeeCd, strMngFeeNmEn, strMngFeeNmVi, strMngFeeNmKr, strInsCompNo, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion

        #region RegistryLateFeeRatioList : 연체요율 추가 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryLateFeeRatioList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-10
         * 용       도 : 연체요율 추가 등록
         * Input    값 : strRentCd, strFeeTy, intLateFeeStartDay, intLateFeeEndDay, strLateFeeStartDt, fltLateFeeRatio, strInsMemNo, strInsMemIp
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 연체요율 추가 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="intLateFeeStartDay">연체시작일</param>
        /// <param name="intLateFeeEndDay">연체마감일</param>
        /// <param name="strLateFeeStartDt">적용시작일</param>
        /// <param name="fltLateFeeRatio">연체율</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static object[] RegistryLateFeeRatioList(string strRentCd, string strFeeTy, int intLateFeeStartDay, int intLateFeeEndDay, string strLateFeeStartDt,
                                                         double fltLateFeeRatio, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.InsertLateFeeRatioList(strRentCd, strFeeTy, intLateFeeStartDay, intLateFeeEndDay, strLateFeeStartDt, fltLateFeeRatio, strInsCompNo,
                                                            strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion

        #region RegistryMonthMngInfo : 월별 수납항목 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryMonthMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-09
         * 용       도 : 월별 수납항목 등록
         * Input    값 : strRentCd, strFeeTy, strMngFeeCd, strMngYear, strMngMM, strMngFee, strUseYn, strLimitDt, strInsCompNo, strInsMemNo, strInsMemIp
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// RegistryMonthMngInfo : 월별 수납항목 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngFeeCd">요금관리코드</param>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strMngFee">금액</param>
        /// <param name="strUseYn">사용여부</param>
        /// <param name="strLimitDt">납부날짜</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static object[] RegistryMonthMngInfo(string strRentCd, string strFeeTy, string strMngFeeCd, string strMngYear, string strMngMM, string strMngFee, 
                                                     string strUseYn, string strLimitDt, string strInsCompNo, string strInsMemNo, string strInsMemIp,string VAT)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.InsertMonthMngInfo(strRentCd, strFeeTy, strMngFeeCd, strMngYear, strMngMM, strMngFee, strUseYn, strLimitDt, strInsCompNo, strInsMemNo, strInsMemIp,VAT);

            return objReturn;
        }

        #endregion
        
        #region RegistryRentalMngReasonInfo : 리테일 및 오피스 임대 임대료 마감 사유 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryRentalMngReasonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-23
         * 용       도 : 리테일 및 오피스 임대 임대료 마감 사유 등록
         * Input    값 : strRentCd, strFeeTy, strUserSeq, strMngYear, strMngMM, strContext, strInsCompNo, strInsMemNo, strInsMemIp
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryRentalMngReasonInfo : 리테일 및 오피스 임대 임대료 마감 사유 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strContext">사유</param>
        /// <param name="strInsCompNo">기업코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static object[] RegistryRentalMngReasonInfo(string strRentCd, string strFeeTy, string strUserSeq, string strMngYear, string strMngMM,
                                                           string strContext, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.InsertRentalMngReasonInfo(strRentCd, strFeeTy, strUserSeq, strMngYear, strMngMM, strContext, strInsCompNo, strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion

        #region RegistryInitMngFeeMonthSetInfo: 관리비 청구월 초기세팅

        /**********************************************************************************************
         * Mehtod   명 : RegistryInitMngFeeMonthSetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-14
         * 용       도 : 관리비 청구월 초기세팅
         * Input    값 : RegistryInitMngFeeMonthSetInfo(섹션코드, 적용년, 적용월, 등록회사코드, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryInitMngFeeMonthSetInfo: 관리비 청구월 초기세팅
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strAppliedYear">적용년</param>
        /// <param name="strAppliedMonth">적용월</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] RegistryInitMngFeeMonthSetInfo(string strRentCd, string strAppliedYear, string strAppliedMonth, string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.InsertInitMngFeeMonthSetInfo(strRentCd, strAppliedYear, strAppliedMonth, strInsCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryMngFeeMonthSetInfo: 관리비 청구월 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryMngFeeMonthSetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-15
         * 용       도 : 관리비 청구월 초기세팅
         * Input    값 : RegistryMngFeeMonthSetInfo(섹션코드, 적용년, 적용월, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMngFeeMonthSetInfo: 관리비 청구월 초기세팅
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intMonthSeq">월순번</param>
        /// <param name="strStartDt">시작일</param>
        /// <param name="intDuringMonth">기간(월)</param>
        /// <param name="intRealDuringMonth">실제기간(월)</param>
        /// <param name="intStartMonth">적용시작월</param>
        /// <param name="intRealStartMonth">실제적용시작월</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] RegistryMngFeeMonthSetInfo(string strRentCd, int intMonthSeq, string strStartDt, int intDuringMonth, int intRealDuringMonth,
                                                          int intStartMonth, int intRealStartMonth, string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.InsertMngFeeMonthSetInfo(strRentCd, intMonthSeq, strStartDt, intDuringMonth, intRealDuringMonth, intStartMonth, intRealStartMonth,
                                                               strInsCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryAPTManuallyInfo : 수동생성

        /**********************************************************************************************
         * Mehtod   명 : RegistryAPTManuallyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성
         * Input    값 : RegistryAPTManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryAPTManuallyInfo : 수동생성 ( 아파트 관리비 )
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <returns></returns>
        public static object[] RegistryAPTManuallyInfo(string strRentCd, string strYear, string strMonth)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strYear;
            objParams[1] = strMonth;

            if (strRentCd.Equals(CommValue.RENTAL_VALUE_APT))
            {
                // KN_USP_MNG_INSERT_RENTALMNGFEE_M00
                objReturn = MngPaymentDao.InsertAPTMmgFeeManuallyInfo(strYear, strMonth);
            }
            else if (strRentCd.Equals(CommValue.RENTAL_VALUE_APTSHOP))
            {
                // KN_USP_MNG_INSERT_RENTALMNGFEE_M01
                objReturn = MngPaymentDao.InsertAPTRetailMmgFeeManuallyInfo(strYear, strMonth);
            }

            return objReturn;
        }

        #endregion

        #region RegistryUtilFeeManuallyInfo : 수동생성 (수도 및 전기세)

        /**********************************************************************************************
         * Mehtod   명 : RegistryUtilFeeManuallyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 (수도 및 전기세)
         * Input    값 : RegistryUtilFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUtilFeeManuallyInfo : 수동생성 ( 아파트 관리비 )
        /// </summary>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <returns></returns>
        public static object[] RegistryUtilFeeManuallyInfo(string strYear, string strMonth)
        {
            object[] objReturn = new object[2];

            // KN_USP_RES_INSERT_MONTHENERGY_M00
            objReturn = RemoteMngDao.InsertUtilFeeManuallyInfo(strYear, strMonth);

            return objReturn;
        }

        #endregion

        #region InsertAptMFDebitNote : Phuongtv

        public static object[] RegistryAptMFDebitNot(string strUserSeq, string strRentCd, string strRoomNo, int intFloor, string tenantNm, double dbTotal, string strPeriod)
        {            
            var objReturn = MngPaymentDao.CreateAptMFDebitNote(strUserSeq, strRentCd, strRoomNo, intFloor, tenantNm, dbTotal, strPeriod);

            return objReturn;
        }


        #endregion

        #region RegistryManuallyEveryFeeRegistList : 수동생성 대상조회 ( 관리비 )

        /**********************************************************************************************
         * Mehtod   명 : RegistryManuallyEveryFeeRegistList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-10
         * 용       도 : 수동생성 대상조회 ( 관리비 )
         * Input    값 : RegistryManuallyEveryFeeRegistList(임대구분코드, 해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryManuallyEveryFeeRegistList : 수동생성 대상조회 ( 관리비 )
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <returns></returns>
        public static object[] RegistryManuallyEveryFeeRegistList(string strRentCd, string strYear, string strMonth)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.InsertManuallyEveryFeeRegistList(strRentCd, strYear, strMonth);

            return objReturn;
        }

        public static object[] CreateDebitNote(string strUserSeq, string strRentCd, int rentSeq, string feeType, string strPaymentDt, int intFloor, string strRoomNo, double strArea, string tenantNm, int intPayCycle, double exChangeRate, double feeAmount, string startUsingDt, string endUsingDt, string strIssuingDt, double unitPrice, double total,int intRentSeq, string memNo, string memIp)
        {
            var objReturn = MngPaymentDao.CreateDebitNote(strUserSeq, strRentCd, rentSeq, feeType, strPaymentDt, intFloor, strRoomNo, strArea, tenantNm, intPayCycle, exChangeRate, feeAmount, startUsingDt, endUsingDt, strIssuingDt,unitPrice,total,intRentSeq,memNo,memIp);
            return objReturn;
        }

        public static object[] CreateDebitNoteNewTeanant(string strRentCd, string contractNo, string rentSeq, string feeType, string strYear, string strMonth, string strDay, string vatRatio, string tenantNm, string dongToDollar, string rentFeePayAmt, string leasingAre,string userSeq,string roomNo)
        {
            var objReturn = MngPaymentDao.CreateDebitNoteNewTeanant(strRentCd, contractNo, rentSeq, feeType, strYear, strMonth, strDay, vatRatio, tenantNm, dongToDollar, rentFeePayAmt, leasingAre,userSeq,roomNo);

            return objReturn;
        }

        #endregion

        #region [Transfer Statement]

        public static DataSet TransferStatement(string invoiceNo, string compNo, string rentCode, string feeTy, string paymentCode, string userSeq, string tenantsNm, string description, string roomNo, double netAmount, double vatAmount, double totAmount, string sendType, string paymentDt, string insMemNo, double exchangeRate, string refSeq, string billType, string memIP, string debitAcc, string creditAcc, string vatAcc, string feeTyDt, int seq)
        {
            var objReturn = MngPaymentDao.TransferStatement(invoiceNo, compNo, rentCode, feeTy, paymentCode, userSeq, tenantsNm, description, roomNo, netAmount, vatAmount, totAmount, sendType, paymentDt, insMemNo, exchangeRate, refSeq, billType, memIP, debitAcc, creditAcc, vatAcc, feeTyDt, seq);
            return objReturn;
        }


        public static DataSet TransferStatementApt(string invoiceNo, string compNo, string rentCode, string feeTy, string paymentCode, string userSeq, string tenantsNm, string description, string roomNo, double netAmount, double vatAmount, double totAmount, string sendType, string paymentDt, string insMemNo, double exchangeRate, string refSeq, string billType, string memIP, string debitAcc, string creditAcc, string vatAcc, string feeTyDt, int seq)
        {
            var objReturn = MngPaymentDao.TransferStatementApt(invoiceNo, compNo, rentCode, feeTy, paymentCode, userSeq, tenantsNm, description, roomNo, netAmount, vatAmount, totAmount, sendType, paymentDt, insMemNo, exchangeRate, refSeq, billType, memIP,debitAcc,creditAcc,vatAcc,feeTyDt,seq);
            return objReturn;
        }

        public static object[] CancelData(string roomNo, string userSeq, string insMemNo, string insIP, string Reason, string refSerialNo, string refInvoiceNo, string cancelType, string sendType, string slipNo)
        {
            var objReturn = MngPaymentDao.CancelData( roomNo,  userSeq,  insMemNo,  insIP,  Reason,  refSerialNo,  refInvoiceNo,  cancelType, sendType, slipNo);
            return objReturn;
        }

        #endregion

        #region ModifySpecialDebit

        public static object[] UpdateSpecialDebit(string strFeeTy, string strPaymentDt, double strDongToDollar, double strMonthViAmtNo, double strRealMonthViAmt, string strStartDt, string strEndDt, string strIssuingDt, string strREF_SEQ, double vatAmt, string strDescVi, string strDescEng, string debitTy, string strRequestDt, double dbQty, double dbUnitPrice)
        {
            var objReturn = MngPaymentDao.UpdateSpecialDebit(strFeeTy, strPaymentDt, strDongToDollar, strMonthViAmtNo, strRealMonthViAmt,
                                                                  strStartDt, strEndDt, strIssuingDt, strREF_SEQ, vatAmt, strDescVi, strDescEng, debitTy, strRequestDt, dbQty, dbUnitPrice);

            return objReturn;
        }

        public static DataTable UpdatingNullSpecialDebit(string rentCd)
        {
            var dtreturn = MngPaymentDao.UpdatingNullSpecialDebit(rentCd);
            return dtreturn;
        }

        public static DataTable UpdatingPrintBundleNoSpecialDebit(string refSeq, string printBundleNo)
        {
            var dtreturn = MngPaymentDao.UpdatingPrintBundleNoSpecialDebit(refSeq, printBundleNo);

            return dtreturn;
        }

        public static DataTable UpdatingPrintedYNSpecialDebit(string printBundleNo)
        {
            var dtreturn = new DataTable();

            dtreturn = MngPaymentDao.UpdatingPrintedYNSpecialDebit(printBundleNo);

            return dtreturn;
        }

        #endregion

        #region Update APTParkingCmpInfo

        public static object[] UpdateAPTParkingCmpInfo(string strRoomNo, string ParkingTagNo, string strParkingCarNo, string strTaxCd, string strCmpNm, string strAddr, string strUserDetAddr, string strComNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.UpdateAPTParkingCmpInfo(strRoomNo, ParkingTagNo, strParkingCarNo, strTaxCd, strCmpNm, strAddr, strUserDetAddr, strComNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion 

        #region ModifyPaymentInfo : 월별 수납/연체여부 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyPaymentInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-20
         * 용       도 : 월별 수납/연체여부 수정
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strReceitCd, strLateCd, strLimitDt
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납/연체여부 수정
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">순번</param>
        /// <param name="strReceitCd">수납여부</param>
        /// <param name="strLateCd">연체여부</param>
        /// <param name="strLimitDt">납부기한</param>
        /// <returns></returns>
        public static DataTable ModifyPaymentInfo(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq, string strReceitCd, string strLateCd, string strLimitDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.UpdatePaymentInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strReceitCd, strLateCd, strLimitDt);

            return dtReturn;
        }

        #endregion

        #region ModifyPaymentInfo : 월별 수납/연체여부 수정(자동)

        /**********************************************************************************************
         * Mehtod   명 : ModifyPaymentInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-20
         * 용       도 : 월별 수납/연체여부 수정(자동)
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strReceitCd
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납/연체여부 수정(자동)
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strUserSeq">순번</param>
        /// <param name="strReceitCd">수납여부</param>
        /// <returns></returns>
        public static DataTable ModifyPaymentInfo(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq, string strReceitCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.UpdatePaymentInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strReceitCd);

            return dtReturn;
        }

        #endregion

        #region ModifyPaymentAmoumt : 월별 수납/연체여부 금액 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyPaymentAmoumt
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-19
         * 용       도 : 월별 수납/연체여부 금액 수정
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strPaySeq, dblPayAmt, strMemIP, strMemNo)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납/연체여부 수정
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">순번</param>
        /// <param name="strPaySeq">수납순서</param>
        /// <param name="dblPayAmt">금액</param>
        /// <param name="strMemIP">IP</param>
        /// <param name="strMemNo">사원번호</param>
        /// <returns></returns>
        public static DataTable ModifyPaymentAmoumt(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq, string strPaySeq, double dblPayAmt, string strMemIP, string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.UpdatePaymentAmoumt(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strPaySeq, dblPayAmt, strMemIP, strMemNo);

            return dtReturn;
        }

        #endregion

        #region ModifySettelmentInfo : 과금관리정산수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateSettelmentInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2011-01-04
         * 용       도 : 과금관리정산수정
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        public static DataTable ModifySettelmentInfo(string strUserSeq, string strRentCd, string strItemCd, string strRentalYear, string strRentalMM, int intPaySeq, double dblPayAmt,
            string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.UpdateSettelmentInfo(strUserSeq, strRentCd, strItemCd, strRentalYear, strRentalMM, intPaySeq, dblPayAmt, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion

        #region ModifyCancelSettelmentInfo : 정산취소

        /**********************************************************************************************
         * Mehtod   명 : ModifyCancelSettelmentInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2011-01-25
         * 용       도 : 납부금액수정
         * Input    값 : strPayDt, intPaySeq, strRentCd, strItemCd, dblUniPrime, dblPayAmt, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 납부금액수정
        /// </summary>
        /// <param name="strPayDt">납부일자</param>
        /// <param name="intPaySeq">납부순서</param>
        /// <param name="strRentCd">임대정보</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="dblUniPrime">원금</param>
        /// <param name="dblPayAmt">납부금액</param>
        /// <param name="strInsMemNo">수정자번호</param>
        /// <param name="strInsMemIp">수정자IP</param>
        /// <returns></returns>
        public static DataTable ModifyCancelSettelmentInfo(string strPayDt, int intPaySeq, string strRentCd, string strItemCd, double dblUniPrime, double dblPayAmt, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.UpdateCancelSettelmentInfo(strPayDt, intPaySeq, strRentCd, strItemCd, dblUniPrime, dblPayAmt, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion

        #region ModifyRentalMngFeeAddon : 관리비 및 임대료 Sequence 할당

        /**********************************************************************************************
         * Mehtod   명 : ModifyRentalMngFeeAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-06
         * 용       도 : 관리비 및 임대료 Sequence 할당
         * Input    값 : ModifyRentalMngFeeAddon(strRentCd, strItemCd, strRentalYear, strRentalMM, strUserSeq, intPaySeq, strPaymentDt, strPaymentSeq)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyRentalMngFeeAddon : 관리비 및 임대료 Sequence 할당
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strRentalYear">해당년도</param>
        /// <param name="strRentalMM">해당월</param>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="intPaySeq">관리비지불순번</param>
        /// <param name="strPaymentDt">원장지불일자</param>
        /// <param name="strPaymentSeq">원장지불순번</param>
        /// <returns></returns>
        public static object[] ModifyRentalMngFeeAddon(string strRentCd, string strItemCd, string strRentalYear, string strRentalMM, string strUserSeq, int intPaySeq,
                                                       string strPaymentDt, int strPaymentSeq)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.UpdateRentalMngFeeAddon(strRentCd, strItemCd, strRentalYear, strRentalMM, strUserSeq, intPaySeq, strPaymentDt, strPaymentSeq);

            return objReturn;
        }

        #endregion

        #region ModifyMngFeeItem : 관리비 및 임대료 항목 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMngFeeItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 수정
         * Input    값 : strRentCd, strFeeTy, strMngFeeCd, strMngFeeNmEn, strMngFeeNmVi, strMngFeeNmKr
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMngFeeItem : 관리비 및 임대료 항목 수정
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngFeeCd">관리비코드</param>
        /// <param name="strMngFeeNmEn">영어관리비항목</param>
        /// <param name="MngFeeNmVi">베트남관리비항목</param>
        /// <param name="MngFeeNmKr">한국어관리비항목</param>
        /// <returns></returns>
        public static object[] ModifyMngFeeItem(string strRentCd, string strFeeTy, string strMngFeeCd, string strMngFeeNmEn, string strMngFeeNmVi, string strMngFeeNmKr)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.UpdateMngFeeItem(strRentCd, strFeeTy, strMngFeeCd, strMngFeeNmEn, strMngFeeNmVi, strMngFeeNmKr);

            return objReturn;
        }

        #endregion
        
        #region ModifyLateFeeRatioList : 연체요율 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : ModifyLateFeeRatioList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-10
         * 용       도 : 연체요율 수정 처리
         * Input    값 : strRentCd, strFeeTy, intLateFeeSeq, intLateFeeStartDay, intLateFeeEndDay, strLateFeeStartDt, fltLateFeeRatio, strInsCompNo, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// ModifyLateFeeRatioList : 연체요율 수정 처리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="intLateFeeSeq">연체순번</param>
        /// <param name="intLateFeeStartDay">연체시작일</param>
        /// <param name="intLateFeeEndDay">연체종료일</param>
        /// <param name="strLateFeeStartDt">적용시작일</param>
        /// <param name="fltLateFeeRatio">연체율</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static DataTable ModifyLateFeeRatioList(string strRentCd, string strFeeTy, int intLateFeeSeq, int intLateFeeStartDay, int intLateFeeEndDay, string strLateFeeStartDt,
                                                       double fltLateFeeRatio, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.UpdateLateFeeRatioList(strRentCd, strFeeTy, intLateFeeSeq, intLateFeeStartDay, intLateFeeEndDay, strLateFeeStartDt, fltLateFeeRatio, strInsCompNo, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion   

        #region ModifyMonthMngInfo : 월별 수납 항목 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMonthMngInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-15
         * 용       도 : 월별 수납 항목 수정
         * Input    값 : strRentCd, strFeeTy, strMngFeeCd, strMngYear, strMngMM, strUseYn, strLimitDt, strInsCompNo, strMngFee, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// ModifyMonthMngInfo : 월별 수납 항목 수정
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngFeeCd">요금관리코드</param>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strMngFee">금액</param>
        /// <param name="strUseYn">사용여부</param>
        /// <param name="strLimitDt">납부날짜</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static DataTable ModifyMonthMngInfo(string strRentCd, string strFeeTy, string strMngFeeCd, string strMngYear, string strMngMM, string strMngFee,
                                                   string strUseYn, string strLimitDt, string strInsCompNo, string strInsMemNo, string strInsMemIp, string VAT)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.UpdateMonthMngInfo(strRentCd, strFeeTy, strMngFeeCd, strMngYear, strMngMM, strMngFee, strUseYn, strLimitDt, strInsCompNo, strInsMemNo, strInsMemIp,VAT);

            return dtReturn;
        }

        #endregion 

        #region ModifyMngRefundInfo : 환불금 처리 변경

        /**********************************************************************************************
         * Mehtod   명 : ModifyMngRefundInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-29
         * 용       도 : 환불금 처리 변경
         * Input    값 : ModifyMngRefundInfo(strMngYear, strMngMM, strUserSeq, strInsCompNo, strInsMemNo, strInsMemIp)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMngRefundInfo : 환불금 처리 변경
        /// </summary>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static object[] ModifyMngRefundInfo(string strMngYear, string strMngMM, string strUserSeq, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.UpdateMngRefundInfo(strMngYear, strMngMM, strUserSeq, strInsCompNo, strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion

        #region ModifyMngRefundInfo : 환불금 미처리 변경

        /**********************************************************************************************
         * Mehtod   명 : ModifyMngRefundInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-29
         * 용       도 : 환불금 미처리 변경
         * Input    값 : ModifyMngRefundInfo(strMngYear, strMngMM, strUserSeq)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMngRefundInfo : 환불금 미처리 변경
        /// </summary>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <returns></returns>
        public static object[] ModifyMngRefundInfo(string strMngYear, string strMngMM, string strUserSeq)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.UpdateMngRefundInfo(strMngYear, strMngMM, strUserSeq);

            return objReturn;
        }

        #endregion

        #region RemoveRentalMngFeeAddon : 관리비 및 임대료 납부 테이블 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveRentalMngFeeAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-04
         * 용       도 : 관리비 및 임대료 납부 테이블 삭제
         * Input    값 : RemoveRentalMngFeeAddon(strRentCd, strUserSeq, strItemCd, intFloorNo, strRoomNo, dblDongToDollar, strPaymentCd, strPayDt, strFeeVat, strInsMemNo, strInsMemIp, strCarTy, dblPayAmt
         * Ouput    값 : DataTable
         **********************************************************************************************/
        public static DataTable RemoveRentalMngFeeAddon(string strRentCd, string strUserSeq, string strFeeTy, string strRentalYear, string strRentalMM, int intPaySeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentDao.DeleteRentalMngFeeAddon(strRentCd, strUserSeq, strFeeTy, strRentalYear, strRentalMM, intPaySeq);

            return dtReturn;
        }

        #endregion

        #region RemoveMngFeeItem : 임대료 항목 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMngFeeItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 삭제
         * Input    값 : DeleteMngFeeItem(strRentCd, strFeeTy, strMngFeeCd)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveMngFeeItem : 임대료 항목 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngFeeCd">관리비코드</param>
        /// <returns></returns>
        public static object[] RemoveMngFeeItem(string strRentCd, string strFeeTy, string strMngFeeCd)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.DeleteMngFeeItem(strRentCd, strFeeTy, strMngFeeCd);

            return objReturn;
        }

        #endregion

        #region RemoveLateFeeRatio : 연체요율 종료처리

        /**********************************************************************************************
         * Mehtod   명 : RemoveLateFeeRatio
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-10
         * 용       도 : 연체요율 종료처리
         * Input    값 : strRentCd, strFeeTy, intLateFeeSeq 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 연체요율 종료처리
        /// </summary>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="intLateFeeSeq">연체료순번</param>
        /// <returns></returns>
        public static object[] RemoveLateFeeRatio(string strRentCd, string strFeeTy, int intLateFeeSeq)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.DeleteLateFeeRatioList(strRentCd, strFeeTy, intLateFeeSeq);

            return objReturn;
        }

        #endregion

        #region RemoveLateFeeRatio : 연체요율 전체초기화

        /**********************************************************************************************
         * Mehtod   명 : RemoveLateFeeRatio
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-13
         * 용       도 : 연체요율 전체초기화
         * Input    값 : strRentCd(임대구분코드), strFeeTy
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 연체요율 전체초기화
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param> 
        /// <returns></returns>
        public static object[] RemoveLateFeeRatio(string strRentCd, string strFeeTy)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.DeleteLateFeeRatio(strRentCd, strFeeTy);

            return objReturn;
        }

        #endregion 
                
        #region RemoveRentalMngReasonInfo : 리테일 및 오피스 임대 임대료 마감 사유 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveRentalMngReasonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-23
         * 용       도 : 리테일 및 오피스 임대 임대료 마감 사유 삭제
         * Input    값 : strRentCd, strFeeTy, strUserSeq, strMngYear, strMngMM, strInsCompNo, strInsMemNo, strInsMemIp
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveRentalMngReasonInfo : 리테일 및 오피스 임대 임대료 마감 사유 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strInsCompNo">기업코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static object[] RemoveRentalMngReasonInfo(string strRentCd, string strFeeTy, string strUserSeq, string strMngYear, string strMngMM,
                                                         string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = MngPaymentDao.DeleteRentalMngReasonInfo(strRentCd, strFeeTy, strUserSeq, strMngYear, strMngMM, strInsCompNo, strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion


        #region SelectMngUtilInfo : 월별 수납 항목상세보기

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngInfo
         * 개   발  자 : BAOTV
         * 생   성  일 : 2011-11-09
         * 용       도 : 월별 수납 항목상세보기
         * Input    값 : strLangCd, strRentCd, strFeeTy
         * Ouput    값 : DataSet
         **********************************************************************************************/

        /// <summary>
        /// 월별 수납 항목상세보기
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param> 
        /// <param name="strRoomNo"> </param>
        /// <param name="uSeq"> </param>
        /// <param name="isDebit"> </param>
        /// <param name="compNm"> </param>
        /// <param name="period"> </param>
        /// <returns></returns>
        public static DataTable SelectMngUtilInfo(string strRentCd, string strFeeTy, string strRoomNo, string uSeq, string isDebit, string compNm, string period)
        {
            var dsReturn = MngPaymentDao.SelectMngUtilInfo(strRentCd, strFeeTy, strRoomNo, uSeq, isDebit, compNm, period);

            return dsReturn;
        }

        public static DataTable SelectMngUtilInfoExcel(string strRentCd, string strFeeTy, string strRoomNo, int strChargeSeq, string isDebit, string compNm, string period)
        {
            var dsReturn = MngPaymentDao.SelectMngUtilInfoExcel(strRentCd, strFeeTy, strRoomNo, strChargeSeq, isDebit, compNm, period);

            return dsReturn;
        }

        public static DataTable SelectMngUtilInfoOverTime(string strRentCd, string strFeeTy, string strRoomNo, string ouSeq, string isDebit, string compNm, string period)
        {
            var dsReturn = MngPaymentDao.SelectMngUtilInfoOverTime(strRentCd, strFeeTy, strRoomNo, ouSeq, isDebit, compNm, period);

            return dsReturn;
        }

        #endregion

        #region RegistryUtilManuallyInfo : 수동생성 (수도 및 전기세)

        /**********************************************************************************************
         * Mehtod   명 : RegistryUtilFeeManuallyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 (수도 및 전기세)
         * Input    값 : RegistryUtilFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUtilFeeManuallyInfo : 수동생성 ( 아파트 관리비 )
        /// </summary>
        /// <returns></returns>
        public static object[] RegistryUtilManuallyInfo(MngUtilDs.UtilityInfo utilityInfo)
        {
            // KN_USP_MNG_INSERT_UTILCHARGEINFO_M05
            var objReturn = RemoteMngDao.RegistryUtilManuallyInfo(utilityInfo);

            return objReturn;
        }

        public static object[] RegistryUtilOverManuallyInfo(MngUtilDs.UtilityInfo utilityInfo)
        {
            // KN_USP_MNG_INSERT_UTILCHARGEINFO_M06
            var objReturn = RemoteMngDao.RegistryUtilOverManuallyInfo(utilityInfo);

            return objReturn;
        }

        #endregion


        #region DeleteUtilManuallyInfo : Delete Util (Bao TV)

        /**********************************************************************************************
         * Mehtod   명 : DeleteUtilManuallyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 (수도 및 전기세)
         * Input    값 : RegistryUtilFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUtilFeeManuallyInfo : 수동생성 ( 아파트 관리비 )
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteUtilManuallyInfo(string strRentCd, string uSeq, string strRoomNo)
        {
            // KN_USP_MNG_DELETE_UTILCHARGEINFO_M02
            var objReturn = RemoteMngDao.DeleteUtilManuallyInfo(strRentCd, uSeq, strRoomNo);

            return objReturn;
        }
        public static object[] DeleteUtilOverManuallyInfo(string strRentCd, string strChargeTy, string strRoomNo, string strChargeSeq)
        {
            // KN_USP_RES_INSERT_MONTHENERGY_M03
            var objReturn = RemoteMngDao.DeleteUtilOverManuallyInfo(strRentCd, strChargeTy, strRoomNo, strChargeSeq);

            return objReturn;
        }

        public static object[] DeletePaymentDetails(int seq, string strPSeq)
        {
            // KN_USP_MNG_DELETE_PAYMENTINFO_M00
            var objReturn = RemoteMngDao.DeletePaymentDetails(seq,strPSeq);

            return objReturn;
        }

        public static object[] DeletePaymentAptDetails(int seq, string strPSeq)
        {
            // KN_USP_MNG_DELETE_PAYMENTINFO_M01
            var objReturn = RemoteMngDao.DeletePaymentAptDetails(seq, strPSeq);

            return objReturn;
        }

        public static object[] DeleteRenovationAptDetails(string strPSeq, int sttCd,string memNo,string memIp,string returnPaymentCd,string returnDt)
        {
            // KN_USP_MNG_DELETE_RENOVATION_M00
            var objReturn = RemoteMngDao.DeleteRenovationAptDetails(strPSeq,sttCd,memNo,memIp,returnPaymentCd,returnDt);

            return objReturn;
        }


        #endregion
    }
}