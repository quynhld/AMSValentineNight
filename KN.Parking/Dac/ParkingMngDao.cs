using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Parking.Dac
{
    public class ParkingMngDao
    {
        #region SelectAccountDayParkingFeeList : 일주차정산관리조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountDayParkingFeeList
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
        public static DataSet SelectAccountDayParkingFeeList(int intPageSize, int intNowPage, string strStartDt, string strEndDt, string strCarNo, string strCardNo)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[6];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;
            objParams[4] = strCarNo;
            objParams[5] = strCardNo;
            

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_PRK_SELECT_ACCOUNTDAYPARKINGINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region Excel SelectAccountDayParkingFeeList

        public static DataSet SelectExcelAccountDayParkingFeeList(string strStartDt, string strEndDt, string strCarNo, string strCardNo)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[4];
            
            objParams[0] = strStartDt;
            objParams[1] = strEndDt;
            objParams[2] = strCarNo;
            objParams[3] = strCardNo;


            dsReturn = SPExecute.ExecReturnMulti("KN_USP_PRK_SELECT_EXCELDAYPARKINGINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectAccountDayParkingFeeList : 일일주차 정산내용 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountDayParkingFeeList
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
        public static DataSet SelectAccountDayParkingFeeList(int intPageSize, int intNowPage, string strStartDt, string strEndDt, int intSeq)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[5];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;
            objParams[4] = intSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_PRK_SELECT_ACCOUNTDAYPARKINGINFO_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectDayParkingMaxSeq : 일주차 최대회차 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectDayParkingMaxSeq
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
        public static DataTable SelectDayParkingMaxSeq()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_ACCOUNTDAYPARKINGINFO_S01");

            return dtReturn;
        }

        #endregion 

        #region SelectDayParkingDataCheck : 일별 주차 데이터 체크

        /**********************************************************************************************
         * Mehtod   명 : SelectDayParkingDataCheck
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
        public static DataTable SelectDayParkingDataCheck()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_ACCOUNTDAYPARKINGINFO_S03");

            return dtReturn;
        }

        #endregion 

        #region SelectParkingTagListInfo : 주차태그 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectParkingTagListInfo
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
        public static DataSet SelectParkingTagListInfo(int intPageSize, int intNowPage, string strCardNo, string strCarTy, string strLangCd, string strIssYN)
        {
            DataSet dsReturn = new DataSet();

            object[] objParam = new object[6];

            objParam[0] = intPageSize;
            objParam[1] = intNowPage;
            objParam[2] = strCardNo;
            objParam[3] = strCarTy;
            objParam[4] = strLangCd;
            objParam[5] = strIssYN;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_PRK_SELECT_PARKINGTAGLISTINFO_S01", objParam);

            return dsReturn;
        }

        #endregion 

        #region SelectMonthParkingFeeCheck : 월정액 주차비 항목 중복 체크

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthParkingFeeCheck
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
        public static DataTable SelectMonthParkingFeeCheck(string strRentCd, string strYear, string strMonth, string strCarTyCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;
            objParams[3] = TextLib.StringEncoder(strCarTyCd);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGFEEINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMonthParkingInfoList : 월정액 주차비 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthParkingInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-03
         * 용       도 : 월정액 주차비 리스트 조회
         * Input    값 : SelectMonthParkingInfoList(섹션코드, 년, 월, 언어코드)
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
        public static DataTable SelectMonthParkingInfoList(string strRentCd, string strYear, string strMonth, string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;
            objParams[3] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGFEEINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectParkingUserListInfo : 월정액주차 입주자 정보조회

        /**********************************************************************************************
         * Mehtod   명 : SelectParkingUserListInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-26
         * 용       도 : 월정액주차 입주자 정보조회
         * Input    값 : SelectParkingUserListInfo(섹션코드, 호, 월정카드번호, 월정차번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정액주차 입주자 정보조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strCardNo">월정카드번호</param>
        /// <param name="strCarNo">월정차번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectParkingUserListInfo(string strRentCd, string strRoomNo, string strCardNo, string strCarNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strCardNo;
            objParams[3] = strCarNo;
            objParams[4] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGINFO_S01", objParams);

            return dtReturn;
        }

        //baotv
        public static DataTable GetParkingUserListInfo(string strRentCd, string strRoomNo, string strTenantNm, string strUserSeq)
        {
            var objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strTenantNm;
            objParams[3] = strUserSeq;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGINFO_S05", objParams);

            return dtReturn;
        }

        public static DataTable GetParkingDebitList(string strCarType, string strDebitDt, string strUserSeq)
        {
            var objParams = new object[3];

            objParams[0] = strUserSeq;
            objParams[1] = strCarType;
            objParams[2] = strDebitDt;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGINFO_S06", objParams);

            return dtReturn;
        }

        public static DataTable GetParkingUserListInfo1(string strRentCd, string strPayDt, string strRoomNo, string strTenantNm, string strUserSeq,string isDebit)
        {
            var objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strPayDt;
            objParams[2] = strRoomNo;
            objParams[3] = strTenantNm;
            objParams[4] = strUserSeq;
            objParams[5] = isDebit;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGINFO_S07", objParams);

            return dtReturn;
        }

        #region Report Office Parking Monthly Info

        public static DataTable GetOfficeParkingMonthlyInfo(string strRoomNo, string strTenantNm, string strPeriod, string strLangCd)
        {
            var objParams = new object[4];

            objParams[0] = strRoomNo;
            objParams[1] = strTenantNm;
            objParams[2] = strPeriod;
            objParams[3] = strLangCd;


            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_SELECT_OFFICEMONTHPARKING_S01", objParams);

            return dtReturn;
        }

        #endregion

        public static DataTable GetParkingDebitListPrint(string strCarType, string strDebitDt, string strUserSeq,string strPrintYN)
        {
            var objParams = new object[4];

            objParams[0] = strUserSeq;
            objParams[1] = strCarType;
            objParams[2] = strDebitDt;
            objParams[3] = strPrintYN;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGINFO_S08", objParams);

            return dtReturn;
        }

        #endregion

        #region Select ParkingAptRetail

        public static DataTable GetParkingAptRetai(string strRentCd, string strRoomNo, string strTenantNm)
        {
            var objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strTenantNm;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_APTRETAIL_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExgistParkingCardInfo : 월정카드 중복 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExgistParkingCardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-16
         * 용       도 : 월정카드 중복 조회
         * Input    값 : SelectExgistParkingCardInfo(주차카드번호, 카드태그번호, 차종)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정카드 중복 조회
        /// </summary>
        /// <param name="strCardNo">주차카드번호</param>
        /// <param name="strTagNo">카드태그번호</param>
        /// <param name="strCarTyCd">차종</param>
        /// <returns></returns>
        public static DataTable SelectExgistParkingCardInfo(string strCardNo, string strTagNo, string strCarTyCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strCardNo;
            objParams[1] = strTagNo;
            objParams[2] = strCarTyCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_PARKINGTAGLISTINFO_S00", objParams);

            return dtReturn;
        }

        /**********************************************************************************************
         * Mehtod   명 : SelectExgistParkingCardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-20
         * 용       도 : 주차카드발급 중복 체크
         * Input    값 : SelectExgistParkingCardInfo(주차카드번호, 차번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 주차카드발급 중복 체크
        /// </summary>
        /// <param name="strCardNo">주차카드번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <returns></returns>
        public static DataTable SelectExgistParkingCardInfo(string strCardNo, string strCarNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = TextLib.MakeNullToEmpty(strCardNo);
            objParams[1] = TextLib.MakeNullToEmpty(strCarNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGINFO_S03", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExsistAccMonthPrkInfo

        /**********************************************************************************************
         * Mehtod   명 : SelectExsistAccMonthPrkInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2013-06-14
         * 용       도 : 월정카드 중복 조회
         * Input    값 : SelectExsistAccMonthPrkInfo(주차카드번호, 카드태그번호, 차종)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정카드 중복 조회
        /// </summary>
        /// <param name="strCardNo">주차카드번호</param>
        /// <param name="strRoomNo">카드태그번호</param>
        /// <param name="strStartDt">차종</param>
        /// <param name="strEndDt">차종</param>
        /// <returns></returns>
        public static DataTable SelectExsistAccMonthPrkInfo(string strCardNo, string strRoomNo, string strStartDt,string strEndDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[4];

            objParams[0] = strCardNo;
            objParams[1] = strRoomNo;
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_AccMonthParkingInfo_S06", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExsistHoaDonParkingAPTReturn

        /**********************************************************************************************
         * Mehtod   명 : SelectExsistHoaDonParkingAPTReturn
         * 개   발  자 : 양영석
         * 생   성  일 : 2013-07-27
         * 용       도 : 월정카드 중복 조회
         * Input    값 : SelectExsistHoaDonParkingAPTReturn(주차카드번호, 카드태그번호, 차종)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정카드 중복 조회
        /// </summary>
        /// <param name="strCardNo">주차카드번호</param>
        /// <param name="strRoomNo">카드태그번호</param>
        /// <param name="strStartDt">차종</param>
        /// <param name="strEndDt">차종</param>
        /// <returns></returns>
        public static DataTable SelectExsistHoaDonParkingAPTReturn(string strCardNo, string strRoomNo, string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[4];

            objParams[0] = strCardNo;
            objParams[1] = strRoomNo;
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_DEL_HoaDonParkingAPTReturn_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExgistParkingTagListInfo : 카드태그정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExgistParkingTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-18
         * 용       도 : 카드태그정보 조회
         * Input    값 : SelectExgistParkingTagListInfo(카드번호, 차종류)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExgistParkingTagListInfo 카드태그정보 조회
        /// </summary>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strCarTyNo">차종류</param>
        /// <returns></returns>
        public static DataTable SelectExgistParkingTagListInfo(string strCardNo, string strCarTyNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = TextLib.StringEncoder(strCardNo);
            objParams[1] = strCarTyNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_PARKINGTAGLISTINFO_S02", objParams);

            return dtReturn;
        }

        #endregion 

        #region SelectParkingFeeInfoList : 남은 월 일수에 대한 월정주차 비용

        /**********************************************************************************************
         * Mehtod   명 : SelectParkingFeeInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-03
         * 용       도 : 남은 월 일수에 대한 월정주차 비용
         * Input    값 : SelectParkingFeeInfoList(섹션코드, 차종, 기준일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 남은 월 일수에 대한 월정주차 비용
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strCarTy">자동차타입</param>
        /// <param name="strDate">기준일자</param>
        /// <returns></returns>
        public static DataTable SelectParkingFeeInfoList(string strRentCd, string strCarTy, string strDate)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strCarTy;
            objParams[2] = strDate;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGINFO_S02", objParams);

            return dtReturn;
        }

        #region Get UserSeq by RoomNo

        public static DataTable SelectUserSeqByRoomNo(string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strRoomNo;


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_USERSEQ_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region Select Count Rows In AccountMonthParkingInfo

        public static DataTable SelectCountAccountMonthParkingInfo(string strRoomNo, string strParkingCardNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strRoomNo;
            objParams[1] = strParkingCardNo;


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_COUNT_AccMonth_S00", objParams);

            return dtReturn;
        }

        #endregion

        #endregion

        #region SelectAccountMonthParkingFeeList : 월정액주차정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountMonthParkingFeeList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-26
         * 용       도 : 월정액주차정보 조회
         * Input    값 : SelectAccountMonthParkingFeeList(조회년, 조회월, 방번호, 차번호, 카드번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectAccountMonthParkingFeeList : 월정액주차정보 조회
        /// </summary>
        /// <param name="strYear">조회년</param>
        /// <param name="strMM">조회월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectAccountMonthParkingFeeList(string strYear, string strMM, string strRoomNo, string strCarNo, string strCardNo, string strLangCd, string strCarTyCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[7];

            objParams[0] = strYear;
            objParams[1] = strMM;
            objParams[2] = strRoomNo;
            objParams[3] = strCarNo;
            objParams[4] = strCardNo;
            objParams[5] = strLangCd;
            objParams[6] = strCarTyCd; 

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_PRK_SELECT_ACCOUNTMONTHPARKINGINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectParkingFeeUpdate : PhuongTV

        /**********************************************************************************************
         * Mehtod   명 : SelectParkingFeeUpdate
         * 개   발  자 : 양영석
         * 생   성  일 : 2014-07-28
         * 용       도 : 월정액주차정보 조회
         * Input    값 : SelectParkingFeeUpdate(조회년, 조회월, 방번호, 차번호, 카드번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectAccountMonthParkingFeeList : 월정액주차정보 조회
        /// </summary>
        /// <param name="strYear">조회년</param>
        /// <param name="strMM">조회월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectParkingFeeUpdate(string strYear, string strMM, string strRoomNo, string strCarNo, string strCardNo, string strLangCd, string strCarTyCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[7];

            objParams[0] = strYear;
            objParams[1] = strMM;
            objParams[2] = strRoomNo;
            objParams[3] = strCarNo;
            objParams[4] = strCardNo;
            objParams[5] = strLangCd;
            objParams[6] = strCarTyCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_PRK_SELECT_ParkingFeeUPDATE_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectParkingFeeRetail

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountMonthParkingFeeList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-26
         * 용       도 : 월정액주차정보 조회
         * Input    값 : SelectParkingFeeRetail
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectParkingFeeRetail : 월정액주차정보 조회
        /// </summary>
        /// <param name="strYear">조회년</param>
        /// <param name="strMM">조회월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectParkingFeeRetail(string strYearMonth, string strPayDt, string strRoomNo, string strCarNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[5];

            objParams[0] = strYearMonth;
            objParams[1] = strPayDt;
            objParams[2] = strRoomNo;
            objParams[3] = strCarNo;
            objParams[4] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_PRK_SELECT_PKF_RETAIL_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region Select ParkingReciptDebit

        public static DataTable SelectParkingReciptDebit(string strYear, string strMM, string strRoomNo, string strCardNo, string strLangCd)
        {
            DataTable dsReturn = new DataTable();

            object[] objParams = new object[5];

            objParams[0] = strYear;
            objParams[1] = strMM;
            objParams[2] = strRoomNo;          
            objParams[3] = strCardNo;
            objParams[4] = strLangCd;


            dsReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_PARKINGRECIPTDEBIT_S00", objParams);

            return dsReturn;
        }

       

        #endregion

        #region SelectAccountMonthParkingFeeForExcel : 엑셀용 월정액주차정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountMonthParkingFeeForExcel
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-29
         * 용       도 : 월정액주차정보 조회
         * Input    값 : SelectAccountMonthParkingFeeForExcel(조회년, 조회월, 방번호, 차번호, 카드번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectAccountMonthParkingFeeForExcel : 월정액주차정보 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMM">조회월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strCardNo">카드번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectAccountMonthParkingFeeForExcel(string strRentCd, string strYear, string strMM, string strRoomNo, string strCarNo, string strCardNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[7];

            objParams[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[1] = TextLib.MakeNullToEmpty(strYear);
            objParams[2] = TextLib.MakeNullToEmpty(strMM);
            objParams[3] = TextLib.MakeNullToEmpty(strRoomNo);
            objParams[4] = TextLib.MakeNullToEmpty(strCarNo);
            objParams[5] = TextLib.MakeNullToEmpty(strCardNo);
            objParams[6] = TextLib.MakeNullToEmpty(strLangCd);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_ACCOUNTMONTHPARKINGINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMonthParkingInfo : 월정주차정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-28
         * 용       도 : 월정주차정보 조회
         * Input    값 : SelectMonthParkingInfo(차번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMonthParkingInfo : 월정액주차정보 조회
        /// </summary>
        /// <param name="strCarNo">차번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectMonthParkingInfo(string strCarNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strCarNo;
            objParams[1] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_MONTHPARKINGINFO_S04", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertDayParkingDataMake : 일별 주차 데이터 수동생성

        /**********************************************************************************************
         * Mehtod   명 : InsertDayParkingDataMake
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
        public static object[] InsertDayParkingDataMake()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_DAILYPARKING_LIST_M00");

            return objReturn;
        }

        #endregion 

        #region InsertTagListInfo : Tag 정보 생성

        /**********************************************************************************************
         * Mehtod   명 : InsertTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-31
         * 용       도 : Tag 정보 생성
         * Input    값 : InsertTagListInfo(strCardNo, strTagNo, strCarTyCd, strIssuedYn, strRejectedYn, strNotFirstYn)
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
        public static DataTable InsertTagListInfo(string strCardNo, string strTagNo, string strCarTyCd, string strIssuedYn, string strRejectedYn, string strNotFirstYn)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[6];

            objParam[0] = strCardNo;
            objParam[1] = strTagNo;
            objParam[2] = strCarTyCd;
            objParam[3] = strIssuedYn;
            objParam[4] = strRejectedYn;
            objParam[5] = strNotFirstYn;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_INSERT_PARKINGTAGLISTINFO_S00", objParam);

            return dtReturn;
        }

        #endregion 

        #region InsertMonthParkingFee : 월정액 주차비 항목 추가

        /**********************************************************************************************
         * Mehtod   명 : InsertMonthParkingFee
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-08
         * 용       도 : 월정액 주차비 항목 추가
         * Input    값 : InsertMonthParkingFee(년도, 월, 차종, 금액, 적용일, 기업번호, 사원번호, IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정액 주차비 항목 추가
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strCarTyCd">차종</param>
        /// <param name="dbParkingFee">금액</param>
        /// <param name="strApplyDt">적용일</param>
        /// <param name="strInsCompNo">기업번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원IP</param>
        /// <returns></returns>
        public static DataTable InsertMonthParkingFee(string strRentCd, string strYear, string strMonth, string strCarTyCd, double dbParkingFee, string strApplyDt, 
                                                      string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;
            objParams[3] = TextLib.StringEncoder(strCarTyCd);
            objParams[4] = dbParkingFee;
            objParams[5] = strApplyDt;
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_INSERT_MONTHPARKINGFEEINFO_M00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertEntireMonthParkingFee : 월정액 이전월 복사

        /**********************************************************************************************
         * Mehtod   명 : InsertEntireMonthParkingFee
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-07
         * 용       도 : 월정액 이전월 복사
         * Input    값 : InsertMonthParkingFee(섹션코드, 년도, 월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월정액 이전월 복사
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도</param>
        /// <param name="strMonth">월</param>
        /// <returns></returns>
        public static DataTable InsertEntireMonthParkingFee(string strRentCd, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_INSERT_MONTHPARKINGFEEINFO_M01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertUserParkingInfo : 입주자 차량관련 카드 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : InsertUserParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 차량관련 카드 정보 저장
         * Input    값 : InsertUserParkingInfo(입주자순번, 주차태그정보, 주차카드번호, 주차차량번호, 임대자와의 관계코드, 입력회사코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUserParkingInfo : 입주자 차량관련 카드 정보 저장
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
        public static DataTable InsertUserParkingInfo(string strUserSeq, string strParkingTagNo, string strParkingCardNo,
                                                     string strParkingCarNo, string strCarCd, string strGateCd, string strInsCompNo, string strInsMemNo, 
                                                     string strInsMemIP, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[10];

            objParams[0] = strUserSeq;
            objParams[1] = strParkingTagNo;
            objParams[2] = strParkingCardNo;
            objParams[3] = strParkingCarNo;
            objParams[4] = strCarCd;
            objParams[5] = strGateCd;
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIP;
            objParams[9] = strRoomNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_INSERT_USERPARKINGINFO_M00", objParams);

            return dtReturn;
        }
        //baotv
        public static object[] InsertUserParking(string strUserSeq, string strRentCd, string strRoomNo,
                                                     string strContractNo, string strTeanantNm, string strAddr, string strAddr1, string strTaxCd)
        {
            var objParams = new object[8];

            objParams[0] = strUserSeq;
            objParams[1] = strRentCd;
            objParams[2] = strRoomNo;
            objParams[3] = strContractNo;
            objParams[4] = strTeanantNm;
            objParams[5] = strTaxCd;
            objParams[6] = strAddr;
            objParams[7] = strAddr1;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_INSERT_USERPARKINGINFO_M01", objParams);

            return objReturn;
        }

        //BAOTV
        public static object[] InsertParkingDebit(string strUserSeq, int strSeq, string strPeriod,
                                                     string strUnit, int strQuantity, double strUnitPrice, double strAmount, string strDes, string strCarTy, string strRemark, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            var objParams = new object[13];

            objParams[0] = strUserSeq;
            objParams[1] = strSeq;
            objParams[2] = strPeriod;
            objParams[3] = strUnit;
            objParams[4] = strQuantity;
            objParams[5] = strUnitPrice;
            objParams[6] = strAmount;
            objParams[7] = strDes;
            objParams[8] = strCarTy;
            objParams[9] = strRemark;
            objParams[10] = strInsCompNo;
            objParams[11] = strInsMemNo;
            objParams[12] = strInsMemIP;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_INSERT_PARKINGDEBIT_M00", objParams);

            return objReturn;
        }

        #endregion

        #region Insert Parking Hoadon Debit When Print

        public static object[] InsertParkingHoadonDebit(string strInsComno, string strInsMemNo, string strIP,
                                                    string strRentCd, string strRefSeq)
        {
            var objParams = new object[5];

            objParams[0] = strInsComno;
            objParams[1] = strInsMemNo;
            objParams[2] = strIP;
            objParams[3] = strRentCd;
            objParams[4] = strRefSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_SCR_INSERT_PARKING_HOADON_DEBIT", objParams);

            return objReturn;
        }

        #endregion

        #region InsertUserParkingCardFeeInfo : 입주자 주차카드비용 수납처리

        /**********************************************************************************************
         * Mehtod   명 : InsertUserParkingCardFeeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-25
         * 용       도 : 입주자 주차카드비용 수납처리
         * Input    값 : InsertUserParkingCardFeeInfo(임대구분코드, 주차카드번호, 층, 호, 카드TAG번호, 주차차량번호, 
         *                                            차량타입, 주차카드비, 납부주차비, 결제방법, 시작일, 종료일, 회사코드, 입력사번, 
         *                                            입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUserParkingCardFeeInfo : 입주자 주차카드비용 수납처리
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
        public static object[] InsertUserParkingCardFeeInfo(string strRentCd, string strParkingCardNo,int intFloorNo, string strRoomNo, string strParkingTagNo,
                                                            string strParkingCarNo, string strCarCd, double dblParkingCardFee, double dblParkingFee,
                                                            string strPaymentCd, string strStartDt, string strEndDt, string strInsComCd, string strInsMemNo, string strInsMemIP, string strPaymentDt)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[16];

            objParams[0] = strRentCd;
            objParams[1] = strParkingCardNo;
            objParams[2] = intFloorNo;    
            objParams[3] = strRoomNo;
            objParams[4] = strParkingTagNo;
            objParams[5] = strParkingCarNo;
            objParams[6] = strCarCd;
            objParams[7] = dblParkingCardFee;
            objParams[8] = dblParkingFee;
            objParams[9] = strPaymentCd;
            objParams[10] = strStartDt;
            objParams[11] = strEndDt;
            objParams[12] = strInsComCd;
            objParams[13] = strInsMemNo;
            objParams[14] = strInsMemIP;
            objParams[15] = strPaymentDt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_INSERT_PARKINGFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertHoaDonParkingApt 

        /**********************************************************************************************
         * Mehtod   명 : InsertHoaDonParkingApt
         * 개   발  자 : 김범수
         * 생   성  일 : 2013-06-10
         * 용       도 : 입주자 주차카드비용 수납처리
         * Input    값 : InsertHoaDonParkingApt
         * Ouput    값 : object[]
         **********************************************************************************************/

        public static object[] InsertHoaDonParkingApt(string strRentCd, string strParkingTagNo,string strStartDt, string strEndDt)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strRentCd;          
            objParams[1] = TextLib.StringEncoder(strParkingTagNo);            
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_INSERT_HOADONPARKING_APT_I00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertAUTOParkingSystemInfo : 오토바이 주차관리 시스템 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertAUTOParkingSystemInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-06
         * 용       도 : 오토바이 주차관리 시스템 정보 등록
         * Input    값 : InsertAUTOParkingSystemInfo(카드번호, 시작일자, 종료일자, 납입금, 이용개월수)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertAUTOParkingSystemInfo : 오토바이 주차관리 시스템 정보 등록
        /// </summary>
        /// <param name="strParkingCardNo">카드번호</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="dblPaidMoney">납입금</param>
        /// <param name="intMonthCnt">이용개월수</param>
        /// <returns></returns>
        public static object[] InsertAUTOParkingSystemInfo(string strParkingCardNo, string strStartDt, string strEndDt, double dblPaidMoney, int intMonthCnt)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strParkingCardNo;
            objParams[1] = strStartDt;
            objParams[2] = strEndDt;
            objParams[3] = dblPaidMoney;
            objParams[4] = intMonthCnt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_UPDATE_PARKINGFEEINFO_M02", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateTagListInfo : Tag 정보 등록여부 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-31
         * 용       도 : Tag 정보 등록여부 수정
         * Input    값 : UpdateTagListInfo(intCardSeq)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Tag 정보 등록여부 수정
        /// </summary>
        /// <param name="strCardNo">카드순번</param>
        /// <returns></returns>
        public static DataTable UpdateTagListInfo(int intCardSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            objParam[0] = intCardSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_UPDATE_PARKINGTAGLISTINFO_S00", objParam);

            return dtReturn;
        }

        #endregion 

        #region UpdateMonthParkingFee : 월정액 주차비수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthParkingFee
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-02
         * 용       도 : 월정액 주차비수정
         * Input    값 : UpdateMonthParkingFee(섹션코드, 년, 월, 차종, 적용일, 금액);
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
        public static object[] UpdateMonthParkingFee(string strRentCd, string strYear, string strMonth, string strCarTyCd, string strApplyDt, double dblFee)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;
            objParams[3] = strCarTyCd;
            objParams[4] = strApplyDt;
            objParams[5] = dblFee;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_UPDATE_MONTHPARKINGFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion 

        #region UpdateUserParkingInfo : 월정 차량 관련 카드 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateUserParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-16
         * 용       도 : 월정 차량 관련 카드 정보 수정
         * Input    값 : UpdateUserParkingInfo(사용자순번, 사용자상세순번, Tag번호, 카드번호, 차번호, 차종
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
        /// <param name="strInsCompNo">입력회사번호</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] UpdateUserParkingInfo(string strUserSeq, double dblUserDetSeq, string strParkingTagNo, string strParkingCardNo, string strParkingCarNo,
                                                     string strCarCd, string strGateCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[10];

            objParams[0] = strUserSeq;
            objParams[1] = dblUserDetSeq;
            objParams[2] = strParkingTagNo;
            objParams[3] = strParkingCardNo;
            objParams[4] = strParkingCarNo;
            objParams[5] = strCarCd;
            objParams[6] = strGateCd;
            objParams[7] = strInsCompNo;
            objParams[8] = strInsMemNo;
            objParams[9] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_UPDATE_MONTHPARKINGINFO_M00", objParams);

            return objReturn;
        }

        #endregion 

        #region UpdateParkingFeeApt : PhuongTV

        /**********************************************************************************************
         * Mehtod   명 : UpdateParkingFeeApt
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-16
         * 용       도 : 월정 차량 관련 카드 정보 수정
         * Input    값 : UpdateParkingFeeApt(사용자순번, 사용자상세순번, Tag번호, 카드번호, 차번호, 차종
         *                                     입력회사번호, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/

        public static object[] UpdateParkingFeeApt(string strRentCd, string strRoomNo, string strParkingCardNo, string strParkingYear,
                                                     string strParkingMonth, double dbParkingFee, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strParkingCardNo;
            objParams[3] = strParkingYear;
            objParams[4] = strParkingMonth;
            objParams[5] = dbParkingFee;
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIP;


            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_UPDATE_PARKINGFEE_M02", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateParkingSystemInfo : 주차관리 시스템 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateParkingSystemInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-24
         * 용       도 : 주차관리 시스템 정보 수정
         * Input    값 : UpdateParkingSystemInfo(카드번호, 시작일자, 종료일자, 납입금, 이용개월수)
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
        public static object[] UpdateParkingSystemInfo(string strParkingCardNo, string strStartDt, string strEndDt, double dblPaidMoney, int intMonthCnt)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strParkingCardNo;
            objParams[1] = strStartDt;
            objParams[2] = strEndDt;
            objParams[3] = dblPaidMoney;
            objParams[4] = intMonthCnt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_UPDATE_PARKINGFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion 

        #region UpdateORParkingSystemInfo : 오피스 및 리테일 주차관리 시스템 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateORParkingSystemInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-04
         * 용       도 : 오피스 및 리테일 주차관리 시스템 정보 수정
         * Input    값 : UpdateORParkingSystemInfo(카드번호, 시작일자, 종료일자, 납입금, 이용개월수)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateORParkingSystemInfo : 오피스 및 리테일 주차관리 시스템 정보 수정
        /// </summary>
        /// <param name="strParkingCardNo">카드번호</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="dblPaidMoney">납입금</param>
        /// <param name="intMonthCnt">이용개월수</param>
        /// <returns></returns>
        public static object[] UpdateORParkingSystemInfo(string strParkingCardNo, string strStartDt, string strEndDt, double dblPaidMoney, int intMonthCnt)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strParkingCardNo;
            objParams[1] = strStartDt;
            objParams[2] = strEndDt;
            objParams[3] = dblPaidMoney;
            objParams[4] = intMonthCnt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_UPDATE_PARKINGFEEINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateAUTOParkingSystemInfo : 오토바이 주차관리 시스템 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateAUTOParkingSystemInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-01-06
         * 용       도 : 오토바이 주차관리 시스템 정보 수정
         * Input    값 : UpdateAUTOParkingSystemInfo(카드번호, 시작일자, 종료일자, 납입금, 이용개월수)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateAUTOParkingSystemInfo : 오토바이 주차관리 시스템 정보 수정
        /// </summary>
        /// <param name="strParkingCardNo">카드번호</param>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <param name="dblPaidMoney">납입금</param>
        /// <param name="intMonthCnt">이용개월수</param>
        /// <returns></returns>
        public static object[] UpdateAUTOParkingSystemInfo(string strParkingCardNo, string strStartDt, string strEndDt, double dblPaidMoney, int intMonthCnt)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strParkingCardNo;
            objParams[1] = strStartDt;
            objParams[2] = strEndDt;
            objParams[3] = dblPaidMoney;
            objParams[4] = intMonthCnt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_UPDATE_PARKINGFEEINFO_M03", objParams);

            return objReturn;
        }

        #endregion

        #region Updating Parking Debit List For Print

        /**********************************************************************************************
         * Mehtod   명 : UpdatingIsDebitForPrintParkingDebitList
         * 개   발  자 : Jeong Seung Hwa
         * 생   성  일 : 2013-03-05
         * 용       도 : Updating IsDebit(Y/N) for print  
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Updating IsDebit For Print Parking DebitL ist
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="refSerialNo"></param>
        /// <param name="monthAmtNo"></param>
        /// <param name="rentCd"></param>
        /// <param name="printBundleNo"></param>
        /// <returns></returns>
        public static DataTable UpdatingParkingIsDebitY(string userSeq, string seq)
        {
            var dtReturn = new DataTable();
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(userSeq);
            objParam[1] = TextLib.MakeNullToEmpty(seq);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_PARKING_DEBITNOTE_U00", objParam);

            return dtReturn;
        }

        /**********************************************************************************************
         * Mehtod   명 : UpdatingIsDebitForPrintParkingDebitList
         * 개   발  자 : Jeong Seung Hwa
         * 생   성  일 : 2013-03-05
         * 용       도 : Updating IsDebit(Y/N) for print  
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Updating IsDebit For Print Parking DebitL ist
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="refSerialNo"></param>
        /// <param name="monthAmtNo"></param>
        /// <param name="rentCd"></param>
        /// <param name="printBundleNo"></param>
        /// <returns></returns>
        public static DataTable UpdatingParkingIsDebitN(string userSeq, string seq)
        {
            var dtReturn = new DataTable();
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(userSeq);
            objParam[1] = TextLib.MakeNullToEmpty(seq);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_DEBIT_NOTE_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region Update ParkingReciptDebit FOR PRINT_BUNDLE_NO

        /*----------------------Update PRINT_BUNDLE_NO to Null---------------*/

        public static DataTable UpdatingParkingReciptDebitNull(string rentCd)
        {
            var dtReturn = new DataTable();
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_ParkingReciptDebit_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region Update ParkingReciptDebit FOR Set PRINT_BUNDLE_NO = REF_SEQ

        /* Update Set PRINT_BUNDLE_NO = REF_SEQ_NO */
        public static DataTable UpdatingParkingReciptDebitREF(string refSeq, string printBundleNo)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refSeq);
            objParam[1] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_PARKINGRECIPTDEBIT_U01", objParam);

            return dtReturn;
        }
        #endregion

        #region DeleteTagListInfo : Tag 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTagListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-08-31
         * 용       도 : Tag 정보 삭제
         * Input    값 : DeleteTagListInfo(intCardSeq)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Tag 정보 삭제
        /// </summary>
        /// <param name="strCardNo">카드순번</param>
        /// <returns></returns>
        public static DataTable DeleteTagListInfo(int intCardSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            objParam[0] = intCardSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_DELETE_PARKINGTAGLISTINFO_M00", objParam);

            return dtReturn;
        }

        #endregion 

        #region DeleteMonthParkingFee : 월정액 주차비삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMonthParkingFee
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
        public static object[] DeleteMonthParkingFee(string strRentCd, string strYear, string strMonth, string strCarTyCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;
            objParams[3] = strCarTyCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_DELETE_MONTHPARKINGFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteUserParkingInfo : 입주자 차량 관련 카드 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteUserParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 차량 관련 카드 정보 삭제
         * Input    값 : DeleteUserParkingInfo(입주자순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteUserParkingInfo : 입주자 차량 관련 카드 정보 삭제
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] DeleteUserParkingInfo(string strUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strUserSeq;
            objParams[1] = intUserDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_DELETE_USERPARKINGINFO_M00", objParams);

            return objReturn;
        }
        //baotv
        public static object[] DeleteUserParking(string strUserSeq)
        {
            var objParams = new object[1];

            objParams[0] = strUserSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_DELETE_USERPARKINGINFO_M02", objParams);

            return objReturn;
        }
        //baotv
        public static object[] DeleteDebitParking(string strUserSeq,int intSeq)
        {
            var objParams = new object[2];

            objParams[0] = strUserSeq;
            objParams[1] = intSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_DELETE_USERPARKINGINFO_M03", objParams);

            return objReturn;
        }

        //baotv
        public static object[] DeleteDebitParkingList(string refSeq)
        {
            var objParams = new object[1];

            objParams[0] = refSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_DELETE_PARKINGDEBIT_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteMonthParkingInfo : 월정액 주차정보삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMonthParkingInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 월정액 주차정보삭제
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteMonthParkingInfo : 월정액 주차정보삭제
        /// </summary>
        /// <param name="strRoomNo">호정보</param>
        /// <param name="strParkingCardNo">주차카드정보</param>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">지불일</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <returns></returns>
        public static object[] DeleteMonthParkingInfo(string strRoomNo, string strParkingCardNo, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strRentCd, string strUserSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = strRoomNo;
            objParams[1] = strParkingCardNo;
            objParams[2] = strDebitCreditCd;
            objParams[3] = strPaymentDt;
            objParams[4] = intPaymentSeq;
            objParams[5] = strRentCd;
            objParams[6] = strUserSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_DELETE_MONTHPARKINGFEEINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteHoaDonParkingAPTReturn 

        /**********************************************************************************************
         * Mehtod   명 : DeleteHoaDonParkingAPTReturn
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-07-27
         * 용       도 : 월정액 주차정보삭제
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/

        public static object[] DeleteHoaDonParkingAPTReturn(string strCardNo, string strRoomNo, string strStartDt, string strEndDt)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strCardNo;
            objParams[1] = strRoomNo;
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_DELETE_HoaDonParkingAPTReturn_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteParkingMasterInfo : 주차차량 등록정보 정지

        /**********************************************************************************************
         * Mehtod   명 : DeleteParkingMasterInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-16
         * 용       도 : 주차차량 등록정보 정지
         * Input    값 : DeleteParkingMasterInfo(입주자순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateParkingMasterInfo : 주차차량 등록정보 정지
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] DeleteParkingMasterInfo(string strUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strUserSeq;
            objParams[1] = intUserDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_PRK_DELETE_USERPARKINGINFO_M01", objParams);

            return objReturn;
        }

        #endregion
    }
}