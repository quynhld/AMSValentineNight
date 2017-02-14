using System.Data;

using KN.Parking.Dac;

namespace KN.Parking.Biz
{
    public class ParkingMngBlo
    {
        #region SpreadAccountDayParkingFeeList : 일주차정산관리조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccountDayParkingFeeList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-11
         * 용       도 : 일주차정산관리조회
         * Input    값 : intPageSize, intNowPage, strStartDt, strEndDt, strCarNo
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 일주차정산관리조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="strCarNo">차번호</param>
        /// <returns></returns>
        public static DataSet SpreadAccountDayParkingFeeList(int intPageSize, int intNowPage, string strStartDt, string strEndDt, string strCarNo, string strCardNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ParkingMngDao.SelectAccountDayParkingFeeList(intPageSize, intNowPage, strStartDt, strEndDt, strCarNo, strCardNo);

            return dsReturn;
        }

        #endregion

        #region Excel SelectAccountDayParkingFeeList

        public static DataSet SelectExcelAccountDayParkingFeeList(string strStartDt, string strEndDt, string strCarNo, string strCardNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ParkingMngDao.SelectExcelAccountDayParkingFeeList(strStartDt, strEndDt, strCarNo, strCardNo);

            return dsReturn;
        }

        #endregion

        #region SpreadAccountDayParkingFeeList : 일일주차 정산내용 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccountDayParkingFeeList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-29
         * 용       도 : 일일주차 정산내용 조회
         * Input    값 : intPageSize, intNowPage, strStartDt, strEndDt, intSeq
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 일주차정산관리조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="intSeq">회차</param>
        /// <returns></returns>
        public static DataSet SpreadAccountDayParkingFeeList(int intPageSize, int intNowPage, string strStartDt, string strEndDt, int intSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ParkingMngDao.SelectAccountDayParkingFeeList(intPageSize, intNowPage, strStartDt, strEndDt, intSeq);

            return dsReturn;
        }

        #endregion

        #region SpreadDayParkingMaxSeq : 일주차 최대회차 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadDayParkingMaxSeq
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-29
         * 용       도 : 일주차 최대회차 조회
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 일주차 최대회차 조회
        /// </summary>
        /// <returns></returns>
        public static DataTable SpreadDayParkingMaxSeq()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectDayParkingMaxSeq();

            return dtReturn;
        }

        #endregion 

        #region SpreadDayParkingDataCheck : 일별 주차 데이터 체크

        /**********************************************************************************************
         * Mehtod   명 : SpreadDayParkingDataCheck
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-29
         * 용       도 : 일별 주차 데이터 체크
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 일별 주차 데이터 체크
        /// </summary>
        /// <returns></returns>
        public static DataTable SpreadDayParkingDataCheck()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectDayParkingDataCheck();

            return dtReturn;
        }

        #endregion 

        #region SpreadParkingTagListInfo : 주차태그 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadParkingTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-30
         * 용       도 : 주차태그 목록 조회
         * Input    값 : PageSize, NowPage, CardNo, CarTy, LangCd
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 주차태그 목록 조회
        /// </summary>
        /// <param name="intPageSize">페이지당리스트수</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strCardNo">주차카드번호</param>
        /// <param name="strCarTy">차량종류</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadParkingTagListInfo(int intPageSize, int intNowPage, string strCardNo, string strCarTy, string strLangCd, string strIssYN)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ParkingMngDao.SelectParkingTagListInfo(intPageSize, intNowPage, strCardNo, strCarTy, strLangCd, strIssYN);

            return dsReturn;
        }

        #endregion

        #region WatchMonthParkingFeeCheck : 월정액 주차비 항목 중복 체크

        /**********************************************************************************************
         * Mehtod   명 : WatchMonthParkingFeeCheck
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-03
         * 용       도 : 월정액 주차비 항목 중복 체크
         * Input    값 : SelectMonthParkingFeeCheck(섹션코드, 년, 월, 차종)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정액 주차비 항목 중복 체크
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strCarTyCd">차종</param>
        /// <returns></returns>
        public static DataTable WatchMonthParkingFeeCheck(string strRentCd, string strYear, string strMonth, string strCarTyCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectMonthParkingFeeCheck(strRentCd, strYear, strMonth, strCarTyCd);

            return dtReturn;
        }

        #endregion 

        #region SpreadMonthParkingInfoList : 월정액 주차비 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMonthParkingInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-03
         * 용       도 : 월정액 주차비 리스트 조회
         * Input    값 : SpreadMonthParkingInfoList(섹션코드, 년, 월, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정액 주차비 리스트 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년</param>
        /// <param name="strMonth">월</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadMonthParkingInfoList(string strRentCd, string strYear, string strMonth, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectMonthParkingInfoList(strRentCd, strYear, strMonth, strLangCd);

            return dtReturn;
        }

        #endregion

        #region SpreadParkingUserListInfo : 월정액주차 입주자 정보조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadParkingUserListInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-26
         * 용       도 : 월정액주차 입주자 정보조회
         * Input    값 : SpreadParkingUserListInfo(섹션코드, 호, 월정카드번호, 월정차번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정액주차 입주자 정보조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strCardNo">월정카드번호</param>
        /// <param name="strCarNo">월정차번호</param>SpreadParkingUserListInfo
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadParkingUserListInfo(string strRentCd, string strRoomNo, string strCardNo, string strCarNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectParkingUserListInfo(strRentCd, strRoomNo, strCardNo, strCarNo, strLangCd);

            return dtReturn;
        }

        //BAOTV
        public static DataTable GetParkingUserListInfo(string strRentCd, string strRoomNo, string strTenantNm,string strUserSeq)
        {
            var dtReturn = ParkingMngDao.GetParkingUserListInfo(strRentCd, strRoomNo, strTenantNm,strUserSeq);

            return dtReturn;
        }

        public static DataTable GetParkingDebitList(string strCarType, string strDebitDt, string strUserSeq)
        {
            var dtReturn = ParkingMngDao.GetParkingDebitList(strCarType, strDebitDt, strUserSeq);

            return dtReturn;
        }

        public static DataTable GetParkingUserListInfo1(string strRentCd, string strPayDt, string strRoomNo, string strTenantNm, string strUserSeq,string isDebit)
        {
            var dtReturn = ParkingMngDao.GetParkingUserListInfo1(strRentCd, strPayDt, strRoomNo, strTenantNm, strUserSeq,isDebit);

            return dtReturn;
        }

        #region Report Office Parking Monthly Info

        public static DataTable GetOfficeParkingMonthlyInfo(string strRoomNo, string strTenantNm, string strPeriod, string strLangCd)
        {
            var dtReturn = ParkingMngDao.GetOfficeParkingMonthlyInfo(strRoomNo, strTenantNm, strPeriod, strLangCd);

            return dtReturn;
        }

        #endregion

        public static DataTable GetParkingDebitListPrint(string strCarType, string strDebitDt, string strUserSeq, string strPrintYN)
        {
            var dtReturn = ParkingMngDao.GetParkingDebitListPrint(strCarType, strDebitDt, strUserSeq, strPrintYN);

            return dtReturn;
        }



        #endregion

        #region Select ParkingAptRetail

        public static DataTable GetParkingAptRetai(string strRentCd, string strRoomNo, string strTenantNm)
        {
            var dtReturn = ParkingMngDao.GetParkingAptRetai(strRentCd, strRoomNo, strTenantNm);

            return dtReturn;
        }

        #endregion

        #region SpreadExgistParkingCardInfo : 월정카드 중복 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadExgistParkingCardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-16
         * 용       도 : 월정카드 중복 조회
         * Input    값 : SpreadExgistParkingCardInfo(주차카드번호, 카드태그번호, 차종)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정카드 중복 조회
        /// </summary>
        /// <param name="strCardNo">주차카드번호</param>
        /// <param name="strTagNo">카드태그번호</param>
        /// <param name="strCarTyCd">차종</param>
        /// <returns></returns>
        public static DataTable SpreadExgistParkingCardInfo(string strCardNo, string strTagNo, string strCarTyCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectExgistParkingCardInfo(strCardNo, strTagNo, strCarTyCd);

            return dtReturn;
        }

        /**********************************************************************************************
         * Mehtod   명 : SpreadExgistParkingCardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-20
         * 용       도 : 주차카드발급 중복 체크
         * Input    값 : SpreadExgistParkingCardInfo(주차카드번호, 차번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 주차카드발급 중복 체크
        /// </summary>
        /// <param name="strCardNo">주차카드번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <returns></returns>
        public static DataTable SpreadExgistParkingCardInfo(string strCardNo, string strCarNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectExgistParkingCardInfo(strCardNo, strCarNo);

            return dtReturn;
        }

        #endregion

        #region [SelectExsistAccMonthPrkInfo]

        public static DataTable SelectExsistAccMonthPrkInfo(string strCardNo, string strRoomNo, string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectExsistAccMonthPrkInfo(strCardNo, strRoomNo, strStartDt, strEndDt);

            return dtReturn;
        }


        #endregion

        #region [SelectExsistHoaDonParkingAPTReturn]

        public static DataTable SelectExsistHoaDonParkingAPTReturn(string strCardNo, string strRoomNo, string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectExsistHoaDonParkingAPTReturn(strCardNo, strRoomNo, strStartDt, strEndDt);

            return dtReturn;
        }


        #endregion

        #region WatchExgistParkingTagListInfo : 카드태그정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExgistParkingTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-18
         * 용       도 : 카드태그정보 조회
         * Input    값 : WatchExgistParkingTagListInfo(카드번호, 차종류)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExgistParkingTagListInfo 카드태그정보 조회
        /// </summary>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strCarTyNo">차종류</param>
        /// <returns></returns>
        public static DataTable WatchExgistParkingTagListInfo(string strCardNo, string strCarTyNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectExgistParkingTagListInfo(strCardNo, strCarTyNo);

            return dtReturn;
        }

        #endregion 

        #region Select UserSeq by RoomNo

        public static DataTable SelectUserSeqByRoomNo(string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectUserSeqByRoomNo(strRoomNo);

            return dtReturn;
        }

        #endregion

        #region Select Count Rows In AccountMonthParkingInfo

        public static DataTable SelectCountAccountMonthParkingInfo(string strRoomNo, string strParkingCardNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectCountAccountMonthParkingInfo(strRoomNo, strParkingCardNo);

            return dtReturn;
        }

        #endregion

        #region SpreadParkingFeeInfoList : 남은 월 일수에 대한 월정주차 비용

        /**********************************************************************************************
         * Mehtod   명 : SpreadParkingFeeInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-03
         * 용       도 : 남은 월 일수에 대한 월정주차 비용
         * Input    값 : SpreadParkingFeeInfoList(섹션코드, 차종, 기준일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 남은 월 일수에 대한 월정주차 비용
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strCarTy">자동차타입</param>
        /// <param name="strDate">기준일자</param>
        /// <returns></returns>
        public static DataTable SpreadParkingFeeInfoList(string strRentCd, string strCarTy, string strDate)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectParkingFeeInfoList(strRentCd, strCarTy, strDate);

            return dtReturn;
        }

        #endregion

        #region SpreadAccountMonthParkingFeeForExcel : 월정액주차정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccountMonthParkingFeeList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-26
         * 용       도 : 월정액주차정보 조회
         * Input    값 : SpreadAccountMonthParkingFeeList(조회년, 조회월, 방번호, 차번호, 카드번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadAccountMonthParkingFeeList : 월정액주차정보 조회
        /// </summary>
        /// <param name="strYear">조회년</param>
        /// <param name="strMM">조회월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadAccountMonthParkingFeeList(string strYear, string strMM, string strRoomNo, string strCarNo, string strCardNo, string strLangCd, string strCarTyCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ParkingMngDao.SelectAccountMonthParkingFeeList(strYear, strMM, strRoomNo, strCarNo, strCardNo, strLangCd, strCarTyCd);

            return dsReturn;
        }

        #endregion

        #region SpreadSelectParkingFeeUpdate : PhuongTV

        /**********************************************************************************************
         * Mehtod   명 : SpreadSelectParkingFeeUpdate
         * 개   발  자 : PhuongTV
         * 생   성  일 : 2014-07-28
         * 용       도 : 월정액주차정보 조회
         * Input    값 : SpreadSelectParkingFeeUpdate(조회년, 조회월, 방번호, 차번호, 카드번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadSelectParkingFeeUpdate : 월정액주차정보 조회
        /// </summary>
        /// <param name="strYear">조회년</param>
        /// <param name="strMM">조회월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadSelectParkingFeeUpdate(string strYear, string strMM, string strRoomNo, string strCarNo, string strCardNo, string strLangCd, string strCarTyCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ParkingMngDao.SelectParkingFeeUpdate(strYear, strMM, strRoomNo, strCarNo, strCardNo, strLangCd, strCarTyCd);

            return dsReturn;
        }

        #endregion



        #region SelectParkingFeeRetail

        public static DataSet SelectParkingFeeRetail(string strYearMonth, string strPayDt, string strRoomNo, string strCarNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ParkingMngDao.SelectParkingFeeRetail(strYearMonth, strPayDt, strRoomNo,strCarNo, strLangCd);

            return dsReturn;
        }

        #endregion

        #region Select ParkingReciptDebit

        public static DataTable SelectParkingReciptDebit(string strYear, string strMM, string strRoomNo, string strCardNo, string strLangCd)
        {
            DataTable dsReturn = new DataTable();

            dsReturn = ParkingMngDao.SelectParkingReciptDebit(strYear, strMM, strRoomNo, strCardNo, strLangCd);

            return dsReturn;
        }

        #endregion

        #region SpreadAccountMonthParkingFeeForExcel : 엑셀용 월정액주차정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccountMonthParkingFeeForExcel
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-26
         * 용       도 : 엑셀용 월정액주차정보 조회
         * Input    값 : SpreadAccountMonthParkingFeeForExcel(조회년, 조회월, 방번호, 차번호, 카드번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadAccountMonthParkingFeeForExcel : 엑셀용 월정액주차정보 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMM">조회월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadAccountMonthParkingFeeForExcel(string strRentCd, string strYear, string strMM, string strRoomNo, string strCarNo, string strCardNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectAccountMonthParkingFeeForExcel(strRentCd, strYear, strMM, strRoomNo, strCarNo, strCardNo, strLangCd);

            return dtReturn;
        }

        #endregion
        
        #region WatchMonthParkingInfo : 월정주차정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchMonthParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-28
         * 용       도 : 월정주차정보 조회
         * Input    값 : WatchMonthParkingInfo(차번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchMonthParkingInfo : 월정액주차정보 조회
        /// </summary>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable WatchMonthParkingInfo(string strCarNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.SelectMonthParkingInfo(strCarNo, strLangCd);

            return dtReturn;
        }

        #endregion

        #region RegistryDayParkingDataMake : 일별 주차 데이터 수동생성

        /**********************************************************************************************
         * Mehtod   명 : RegistryDayParkingDataMake
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-29
         * 용       도 : 일별 주차 데이터 수동생성
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 일별 주차 데이터 수동생성
        /// </summary>
        /// <returns></returns>
        public static object[] RegistryDayParkingDataMake()
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.InsertDayParkingDataMake();

            return objReturn;
        }

        #endregion 

        #region RegistryTagListInfo : Tag 정보 생성

        /**********************************************************************************************
         * Mehtod   명 : RegistryTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-31
         * 용       도 : Tag 정보 생성
         * Input    값 : RegistryTagListInfo(strCardNo, strTagNo, strCarTyCd, strIssuedYn, strRejectedYn, strNotFirstYn)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Tag 정보 생성
        /// </summary>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strTagNo">TAG 번호</param>
        /// <param name="strCarTyCd">차종</param>
        /// <param name="strIssuedYn">발급여부</param>
        /// <param name="strRejectedYn">말소여부</param>
        /// <param name="strNotFirstYn">최초발급여부</param>
        /// <returns></returns>
        public static DataTable RegistryTagListInfo(string strCardNo, string strTagNo, string strCarTyCd, string strIssuedYn, string strRejectedYn, string strNotFirstYn)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.InsertTagListInfo(strCardNo, strTagNo, strCarTyCd, strIssuedYn, strRejectedYn, strNotFirstYn);

            return dtReturn;
        }

        #endregion 

        #region RegistryMonthParkingFee : 월정액 주차비 항목 추가

        /**********************************************************************************************
         * Mehtod   명 : RegistryMonthParkingFee
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-08
         * 용       도 : 월정액 주차비 항목 추가
         * Input    값 : InsertMonthParkingFee(년도, 월, 차종, 금액, 적용일, 기업번호, 사원번호, IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정액 주차비 항목 추가
        /// </summary>
        /// <param name="strYear">년도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strCarTyCd">차종</param>
        /// <param name="dbParkingFee">금액</param>
        /// <param name="strApplyDt">적용일</param>
        /// <param name="strInsCompNo">기업번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원IP</param>
        /// <returns></returns>
        public static DataTable RegistryMonthParkingFee(string strRentCd, string strYear, string strMonth, string strCarTyCd, double dbParkingFee, string strApplyDt, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.InsertMonthParkingFee(strRentCd, strYear, strMonth, strCarTyCd, dbParkingFee, strApplyDt, strInsCompNo, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion

        #region RegistryEntireMonthParkingFee : 월정액 이전월 복사

        /**********************************************************************************************
         * Mehtod   명 : RegistryEntireMonthParkingFee
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-07
         * 용       도 : 월정액 이전월 복사
         * Input    값 : RegistryMonthParkingFee(섹션코드, 년도, 월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정액 이전월 복사
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도</param>
        /// <param name="strMonth">월</param>
        /// <returns></returns>
        public static DataTable RegistryEntireMonthParkingFee(string strRentCd, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.InsertEntireMonthParkingFee(strRentCd, strYear, strMonth);

            return dtReturn;
        }

        #endregion

        #region RegistryUserParkingInfo : 입주자 차량관련 카드 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : RegistryUserParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 차량관련 카드 정보 저장
         * Input    값 : RegistryUserParkingInfo(입주자순번, 주차태그정보, 주차카드번호, 주차차량번호, 임대자와의 관계코드, 입력회사코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUserParkingInfo : 입주자 차량관련 카드 정보 저장
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="strParkingTagNo">주차태그정보</param>
        /// <param name="strParkingCardNo">주차카드번호</param>
        /// <param name="strParkingCarNo">주차차량번호</param>
        /// <param name="strCarCd">차종</param>
        /// <param name="strGateCd">게이트코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static DataTable RegistryUserParkingInfo(string strUserSeq, string strParkingTagNo, string strParkingCardNo,
                                                     string strParkingCarNo, string strCarCd, string strGateCd, string strInsCompNo, string strInsMemNo,
                                                     string strInsMemIP, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.InsertUserParkingInfo(strUserSeq, strParkingTagNo, strParkingCardNo, strParkingCarNo, strCarCd, strGateCd,
                                                            strInsCompNo, strInsMemNo, strInsMemIP, strRoomNo);

            return dtReturn;
        }

        //baotv
        public static object[] InsertUserParkingInfo(string strUserSeq, string strRentCd, string strRoomNo,
                                                     string strContractNo, string strTeanantNm, string strAddr, string strAddr1, string strTaxCd)
        {
            var objReturn = ParkingMngDao.InsertUserParking(strUserSeq,strRentCd,strRoomNo,strContractNo,strTeanantNm,strAddr,strAddr1,strTaxCd);

            return objReturn;
        }
        public static object[] InsertParkingDebit(string strUserSeq, int strSeq, string strPeriod,
                                                     string strUnit, int strQuantity, double strUnitPrice, double strAmount, string strDes, string strCarTy, string strRemark, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            var objReturn = ParkingMngDao.InsertParkingDebit(strUserSeq, strSeq, strPeriod, strUnit, strQuantity, strUnitPrice, strAmount, strDes,strCarTy,strRemark, strInsCompNo,  strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region Insert Parking  Hoadon Debit When Print

        public static object[] InsertParkingHoadonDebit(string strInsComno, string strInsMemNo, string strIP,
                                                    string strRentCd, string strRefSeq)
        {
            var objReturn = ParkingMngDao.InsertParkingHoadonDebit(strInsComno, strInsMemNo, strIP, strRentCd, strRefSeq);

            return objReturn;
        }

        #endregion

        #region RegistryUserParkingCardFeeInfo : 입주자 주차카드비용 수납처리

        /**********************************************************************************************
         * Mehtod   명 : RegistryUserParkingCardFeeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-25
         * 용       도 : 입주자 주차카드비용 수납처리
         * Input    값 : RegistryUserParkingCardFeeInfo(임대구분코드, 주차카드번호, 층, 호, 카드TAG번호, 주차차량번호,
         *                                            차량타입, 주차카드비, 납부주차비, 결제방법, 시작일, 종료일, 회사코드, 입력사번, 
         *                                            입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUserParkingCardFeeInfo : 입주자 주차카드비용 수납처리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strParkingCardNo">주차카드번호</param>
        /// <param name="intFloorNo">층</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strParkingTagNo">카드TAG번호</param>
        /// <param name="strParkingCarNo">주차차량번호</param>
        /// <param name="strCarCd">차량타입</param>
        /// <param name="dblParkingCardFee">주차카드비</param>
        /// <param name="dblParkingFee">납부주차비</param>
        /// <param name="strPaymentCd">결제방법</param>
        /// <param name="strStartDt">시작일</param>
        /// <param name="strEndDt">종료일</param>
        /// <param name="strInsComCd">회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] RegistryUserParkingCardFeeInfo(string strRentCd, string strParkingCardNo, int intFloorNo, string strRoomNo, string strParkingTagNo,
                                                            string strParkingCarNo, string strCarCd, double dblParkingCardFee, double dblParkingFee,
                                                            string strPaymentCd, string strStartDt, string strEndDt, string strInsComCd, string strInsMemNo, string strInsMemIP, string strPaymentDt)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.InsertUserParkingCardFeeInfo(strRentCd, strParkingCardNo,intFloorNo, strRoomNo, strParkingTagNo, strParkingCarNo,
                                                                   strCarCd, dblParkingCardFee, dblParkingFee, strPaymentCd, strStartDt,
                                                                   strEndDt, strInsComCd, strInsMemNo, strInsMemIP, strPaymentDt);

            return objReturn;
        }

        #endregion

        #region RegistryHoaDonParkingApt

        public static object[] RegistryHoaDonParkingApt(string strRentCd, string strParkingTagNo, string strStartDt, string strEndDt)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.InsertHoaDonParkingApt(strRentCd, strParkingTagNo, strStartDt, strEndDt);

            return objReturn;
        }

        #endregion

        #region RegistryAUTOParkingSystemInfo : 오토바이 주차관리 시스템 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryAUTOParkingSystemInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-06
         * 용       도 : 오토바이 주차관리 시스템 정보 등록
         * Input    값 : RegistryAUTOParkingSystemInfo(카드번호, 시작일자, 종료일자, 납입금, 이용개월수)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryAUTOParkingSystemInfo : 오토바이 주차관리 시스템 정보 등록
        /// </summary>
        /// <param name="strParkingCardNo">카드번호</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="dblPaidMoney">납입금</param>
        /// <param name="intMonthCnt">이용개월수</param>
        /// <returns></returns>
        public static object[] RegistryAUTOParkingSystemInfo(string strParkingCardNo, string strStartDt, string strEndDt, double dblPaidMoney, int intMonthCnt)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.InsertAUTOParkingSystemInfo(strParkingCardNo, strStartDt, strEndDt, dblPaidMoney, intMonthCnt);

            return objReturn;
        }

        #endregion
        
        #region ModifyTagListInfo : Tag 정보 등록여부 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-31
         * 용       도 : Tag 정보 등록여부 수정
         * Input    값 : ModifyTagListInfo(intCardSeq)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Tag 정보 등록여부 수정
        /// </summary>
        /// <param name="strCardNo">카드순번</param>
        /// <returns></returns>
        public static DataTable ModifyTagListInfo(int intCardSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.UpdateTagListInfo(intCardSeq);

            return dtReturn;
        }

        #endregion

        #region ModifyMonthParkingFee : 월정액 주차비수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMonthParkingFee
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-02
         * 용       도 : 월정액 주차비수정
         * Input    값 : ModifyMonthParkingFee(섹션코드, 년, 월, 차종, 적용일, 금액);
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMonthParkingFee : 월정액 주차비수정
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년</param>
        /// <param name="strMonth">월</param>
        /// <param name="strCarTyCd">차종</param>
        /// <param name="strApplyDt">적용일</param>
        /// <param name="dblFee">금액</param>
        /// <returns></returns>
        public static object[] ModifyMonthParkingFee(string strRentCd, string strYear, string strMonth, string strCarTyCd, string strApplyDt, double dblFee)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.UpdateMonthParkingFee(strRentCd, strYear, strMonth, strCarTyCd, strApplyDt, dblFee);

            return objReturn;
        }

        #endregion  

        #region ModifyUserParkingInfo : 월정 차량 관련 카드 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyUserParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-16
         * 용       도 : 월정 차량 관련 카드 정보 수정
         * Input    값 : ModifyUserParkingInfo(사용자순번, 사용자상세순번, Tag번호, 카드번호, 차번호, 차종
         *                                     입력회사번호, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 월정 차량 관련 카드 정보 수정
        /// </summary>
        /// <param name="strUserSeq">사용자순번</param>
        /// <param name="dblUserDetSeq">사용자상세순번</param>
        /// <param name="strParkingTagNo">Tag번호</param>
        /// <param name="strParkingCardNo">카드번호</param>
        /// <param name="strParkingCarNo">차번호</param>
        /// <param name="strCarCd">차종</param>
        /// <param name="strGateCd">게이트코드</param>
        /// <param name="strInsCompNo">입력회사번호</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] ModifyUserParkingInfo(string strUserSeq, double dblUserDetSeq, string strParkingTagNo, string strParkingCardNo,
                                                     string strParkingCarNo, string strCarCd, string strGateCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.UpdateUserParkingInfo(strUserSeq, dblUserDetSeq, strParkingTagNo, strParkingCardNo, strParkingCarNo, strCarCd, strGateCd,
                                                            strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion 

        #region UpdateParkingFeeApt : PhuongTV

        /**********************************************************************************************
         * Mehtod   명 : UpdateParkingFeeApt
         * 개   발  자 : 양영석
         * 생   성  일 : 2014-07-29
         * 용       도 : 월정 차량 관련 카드 정보 수정
         * Input    값 : UpdateParkingFeeApt(사용자순번, 사용자상세순번, Tag번호, 카드번호, 차번호, 차종
         *                                     입력회사번호, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/

        public static object[] ModifyParkingFeeApt(string strRentCd, string strRoomNo, string strParkingCardNo, string strParkingYear,
                                                     string strParkingMonth, double dbParkingFee, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.UpdateParkingFeeApt(strRentCd, strRoomNo, strParkingCardNo, strParkingYear,
                                                     strParkingMonth, dbParkingFee, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyParkingSystemInfo : 주차관리 시스템 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyParkingSystemInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-24
         * 용       도 : 주차관리 시스템 정보 수정
         * Input    값 : ModifyParkingSystemInfo(카드번호, 시작일자, 종료일자, 납입금, 이용개월수)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 주차관리 시스템 정보 수정
        /// </summary>
        /// <param name="strParkingCardNo">카드번호</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="dblPaidMoney">납입금</param>
        /// <param name="intMonthCnt">이용개월수</param>
        /// <returns></returns>
        public static object[] ModifyParkingSystemInfo(string strParkingCardNo, string strStartDt, string strEndDt, double dblPaidMoney, int intMonthCnt)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.UpdateParkingSystemInfo(strParkingCardNo, strStartDt, strEndDt, dblPaidMoney, intMonthCnt);

            return objReturn;
        }

        #endregion

        #region ModifyORParkingSystemInfo : 오피스 및 리테일 주차관리 시스템 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyParkingSystemInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-04
         * 용       도 : 오피스 및 리테일 주차관리 시스템 정보 수정
         * Input    값 : ModifyParkingSystemInfo(카드번호, 시작일자, 종료일자, 납입금, 이용개월수)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyORParkingSystemInfo : 오피스 및 리테일 주차관리 시스템 정보 수정
        /// </summary>
        /// <param name="strParkingCardNo">카드번호</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="dblPaidMoney">납입금</param>
        /// <param name="intMonthCnt">이용개월수</param>
        /// <returns></returns>
        public static object[] ModifyORParkingSystemInfo(string strParkingCardNo, string strStartDt, string strEndDt, double dblPaidMoney, int intMonthCnt)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.UpdateORParkingSystemInfo(strParkingCardNo, strStartDt, strEndDt, dblPaidMoney, intMonthCnt);

            return objReturn;
        }

        #endregion

        #region ModifyAUTOParkingSystemInfo : 오토바이 주차관리 시스템 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyAUTOParkingSystemInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-06
         * 용       도 : 오토바이 주차관리 시스템 정보 수정
         * Input    값 : ModifyAUTOParkingSystemInfo(카드번호, 시작일자, 종료일자, 납입금, 이용개월수)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyAUTOParkingSystemInfo : 오토바이 주차관리 시스템 정보 수정
        /// </summary>
        /// <param name="strParkingCardNo">카드번호</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="dblPaidMoney">납입금</param>
        /// <param name="intMonthCnt">이용개월수</param>
        /// <returns></returns>
        public static object[] ModifyAUTOParkingSystemInfo(string strParkingCardNo, string strStartDt, string strEndDt, double dblPaidMoney, int intMonthCnt)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.UpdateAUTOParkingSystemInfo(strParkingCardNo, strStartDt, strEndDt, dblPaidMoney, intMonthCnt);

            return objReturn;
        }

        #endregion

        #region ModifyPrintParkingDebitList

        public static DataTable UpdatingParkingIsDebitY(string userSeq, string seq)
        {
            var dtreturn = new DataTable();

            dtreturn = ParkingMngDao.UpdatingParkingIsDebitY(userSeq, seq);

            return dtreturn;
        }

        public static DataTable UpdatingParkingIsDebitN(string userSeq, string seq)
        {
            var dtreturn = new DataTable();

            dtreturn = ParkingMngDao.UpdatingParkingIsDebitN(userSeq, seq);

            return dtreturn;
        }


        #endregion

        #region Modify ParkingReciptDebit PRINT_BUNDLE_NO

        public static DataTable UpdatingParkingReciptDebitNull(string rentCd)
        {
            var dtreturn = new DataTable();

            dtreturn = ParkingMngDao.UpdatingParkingReciptDebitNull(rentCd);

            return dtreturn;
        }

        #endregion

        #region Modify ParkingReciptDebiy For Set PRINTBUNDLE_NO = REF_SEQ

        public static DataTable UpdatingParkingReciptDebitREF(string refSeq, string printBundleNo)
        {
            var dtreturn = new DataTable();

            dtreturn = ParkingMngDao.UpdatingParkingReciptDebitREF(refSeq, printBundleNo);

            return dtreturn;
        }

        #endregion


        #region RemoveTagListInfo : Tag 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-31
         * 용       도 : Tag 정보 삭제
         * Input    값 : RemoveTagListInfo(intCardSeq)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Tag 정보 삭제
        /// </summary>
        /// <param name="strCardNo">카드순번</param>
        /// <returns></returns>
        public static DataTable RemoveTagListInfo(int intCardSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ParkingMngDao.DeleteTagListInfo(intCardSeq);

            return dtReturn;
        }

        #endregion 

        #region RemoveMonthParkingFee : 월정액 주차비삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMonthParkingFee
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-08
         * 용       도 : 월정액 주차비삭제
         * Input    값 : strYear, strMonth, strCarTyCd
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 월정액 주차비삭제
        /// </summary>
        /// <param name="strYear">년도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strCarTyCd">차종</param>
        /// <returns></returns>
        public static object[] RemoveMonthParkingFee(string strRentCd, string strYear, string strMonth, string strCarTyCd)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.DeleteMonthParkingFee(strRentCd, strYear, strMonth, strCarTyCd);

            return objReturn;
        }

        #endregion  

        #region RemoveUserParkingInfo : 입주자 차량 관련 카드 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveUserParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 차량 관련 카드 정보 삭제
         * Input    값 : RemoveUserParkingInfo(입주자순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveUserParkingInfo : 입주자 차량 관련 카드 정보 삭제
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] RemoveUserParkingInfo(string strUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.DeleteUserParkingInfo(strUserSeq, intUserDetSeq);

            return objReturn;
        }

        //baotv
        public static object[] DeleteUserParkingInfo(string strUserSeq)
        {
            var objReturn = ParkingMngDao.DeleteUserParking(strUserSeq);

            return objReturn;
        }
        //baotv
        public static object[] DeleteDebitParkingInfo(string strUserSeq,int intSeq)
        {
            var objReturn = ParkingMngDao.DeleteDebitParking(strUserSeq,intSeq);

            return objReturn;
        }

        public static object[] DeleteDebitParkingList(string refSeq)
        {
            var objReturn = ParkingMngDao.DeleteDebitParkingList(refSeq);

            return objReturn;
        }

        #endregion

        #region RemoveMonthParkingInfo : 월정액 주차정보삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMonthParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 월정액 주차정보삭제
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveMonthParkingInfo : 월정액 주차정보삭제
        /// </summary>
        /// <param name="strRoomNo">호정보</param>
        /// <param name="strParkingCardNo">주차카드정보</param>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">지불일</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <returns></returns>
        public static object[] RemoveMonthParkingInfo(string strRoomNo, string strParkingCardNo, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strRentCd, string strUserSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.DeleteMonthParkingInfo(strRoomNo, strParkingCardNo, strDebitCreditCd, strPaymentDt, intPaymentSeq, strRentCd, strUserSeq);

            return objReturn;
        }

        #endregion


        #region DeleteHoaDonParkingAPTReturn

        public static object[] DeleteHoaDonParkingAPTReturn(string strCardNo, string strRoomNo, string strStartDt, string strEndDt)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.DeleteHoaDonParkingAPTReturn(strCardNo, strRoomNo, strStartDt, strEndDt);

            return objReturn;
        }

        #endregion

        #region RemoveParkingMasterInfo : 주차차량 등록정보 정지

        /**********************************************************************************************
         * Mehtod   명 : RemoveParkingMasterInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-16
         * 용       도 : 주차차량 등록정보 수정
         * Input    값 : RemoveParkingMasterInfo(입주자순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveParkingMasterInfo : 주차차량 등록정보 정지
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] RemoveParkingMasterInfo(string strUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ParkingMngDao.DeleteParkingMasterInfo(strUserSeq, intUserDetSeq);

            return objReturn;
        }

        #endregion
    }
}