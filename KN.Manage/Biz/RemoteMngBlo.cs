using System.Data;

using KN.Manage.Dac;
using KN.Common.Base.Code;

namespace KN.Manage.Biz
{
    public class RemoteMngBlo
    {
        #region SpreadChargeInfoList : 원격검침관리 요금설정리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadChargeInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-27
         * 용       도 : 원격검침관리 요금설정리스트 조회
         * Input    값 : strRentCd, strChargeTy
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 원격검침관리 요금설정리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금종류</param>
        /// <returns></returns>
        public static DataTable SpreadChargeInfoList(string strRentCd, string strChargeTy)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectChargeInfoList(strRentCd, strChargeTy);

            return dtReturn;
        }

        #endregion

        #region WatchChargeStartDtCheck : 중복 체크

        /**********************************************************************************************
         * Mehtod   명 : WatchChargeStartDtCheck
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-27
         * 용       도 : 중복 체크
         * Input    값 : strRentCd(임대구분코드), strChargeTy(요금타입), strChargeStartDt(적용일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 중복 체크
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strChargeStartDt">적용일</param>
        /// <returns></returns>
        public static DataTable WatchChargeStartDtCheck(string strRentCd, string strFeeTy, string strChargeStartDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectChargeStartDtCheck(strRentCd, strFeeTy, strChargeStartDt);

            return dtReturn;
        }

        #endregion

        #region WatchEnergyMonthCheck : 원격검침 Agent 정상작동 체크

        /**********************************************************************************************
         * Mehtod   명 : WatchEnergyMonthCheck
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-30
         * 용       도 : 원격검침 Agent 정상작동 체크
         * Input    값 : strRentCd(임대구분코드), strChargeTy(요금타입), strChargeStartDt(적용일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchEnergyMonthCheck : 원격검침 Agent 정상작동 체크
        /// </summary>
        /// <returns></returns>
        public static DataTable WatchEnergyMonthCheck()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectEnergyMonthCheck();

            return dtReturn;
        }

        #endregion

        #region SpreadMonthUseChargeList : 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMonthUseChargeList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-28
         * 용       도 : 월별 검침 데이터 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 검침 데이터 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataSet SpreadMonthUseChargeList(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectMonthUseChargeList(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region SpreadMonthChargeListForTower : 타워용 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMonthChargeListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 타워용 월별 검침 데이터 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 타워용 월별 검침 데이터 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataSet SpreadMonthChargeListForTower(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectMonthChargeListForTower(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region SelectMonthChargeListForAPTRetail : 아파트리테일용 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthChargeListForAPTRetail
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-12
         * 용       도 : 아파트리테일용 월별 검침 데이터 조회
         * Input    값 : SelectMonthChargeListForAPTRetail(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMonthChargeListForAPTRetail : 아파트리테일용 월별 검침 데이터 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataSet SelectMonthChargeListForAPTRetail(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectMonthChargeListForAPTRetail(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region SelectMonthChargeListForKeangNam : 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthChargeListForKeangNam
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-12
         * 용       도 : 아파트리테일용 월별 검침 데이터 조회
         * Input    값 : SelectMonthChargeListForKeangNam(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMonthChargeListForKeangNam : 아파트리테일용 월별 검침 데이터 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataSet SelectMonthChargeListForKeangNam(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            if (strRentCd.Equals(CommValue.RENTAL_VALUE_OFFICE) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_SHOP))
            {
                dsReturn = RemoteMngDao.SelectMonthChargeListForTower(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo);
            }
            else
            {
                dsReturn = RemoteMngDao.SelectMonthChargeListForAPTRetail(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo);
            }
            
            return dsReturn;
        }

        #endregion

        #region SpreadExcelMonthAmountUse : 엑셀용 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadExcelMonthAmountUse
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-04-14
         * 용       도 : 엑셀용 정보 조회
         * Input    값 : SpreadExcelMonthAmountUse(요금코드, 조회 대상년도, 조회 대상월, 입주자번호, 입주자명, 섹션구분코드, 층정보, 방정보)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadExcelMonthAmountUse : 엑셀용 정보 조회
        /// </summary>
        /// <param name="strChargeTy">요금코드</param>
        /// <param name="strYear">조회 대상년도</param>
        /// <param name="strMonth">조회 대상월</param>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="strUserNm">입주자명</param>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strFloorNo">층정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <returns></returns>
        public static DataTable SpreadExcelMonthAmountUse(string strChargeTy, string strYear, string strMonth, string strUserSeq, string strUserNm, string strRentCd,
                                                          string strFloorNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectExcelMonthAmountUse(strChargeTy, strYear, strMonth, strUserSeq, strUserNm, strRentCd, strFloorNo, strRoomNo);

            return dtReturn;
        }

        #endregion 

        #region SpreadDayUseChargeList : 일별 검침 데이터 조회(페이지)

        /**********************************************************************************************
         * Mehtod   명 : SpreadDayUseChargeList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-29
         * 용       도 : 일별 검침 데이터 조회(페이지)
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 일별 검침 데이터 조회(페이지)
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataSet SpreadDayUseChargeList(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectDayUseChargeList(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region SpreadMonthUseChargeList : 월별 검침 데이터 조회(그래프)

        /**********************************************************************************************
         * Mehtod   명 : SpreadMonthUseChargeList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-28
         * 용       도 : 월별 검침 데이터 조회(그래프)
         * Input    값 : strRentCd, strChargeTy, strYear, intFlooNo, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 검침 데이터 조회(그래프)
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataTable SpreadMonthUseChargeList(string strRentCd, string strChargeTy, string strYear, int intFlooNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectMonthUseChargeList(strRentCd, strChargeTy, strYear, intFlooNo, strRoomNo);

            return dtReturn;
        }

        #endregion

        #region SpreadYearUseChargeList : 년별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadYearUseChargeList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-28
         * 용       도 : 년별 검침 데이터 조회
         * Input    값 : strRentCd, strChargeTy, intFlooNo, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 년별 검침 데이터 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">번호</param>
        /// <returns></returns>
        public static DataTable SpreadYearUseChargeList(string strRentCd, string strChargeTy, int intFlooNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectYearUseChargeList(strRentCd, strChargeTy, intFlooNo, strRoomNo);

            return dtReturn;
        }

        #endregion 

        #region SpreadMonthUseEnergyList : 원격검침관리 요금리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMonthUseEnergyList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-04
         * 용       도 : 원격검침관리 요금리스트 조회
         * Input    값 : SpreadMonthUseEnergyList(임대구분코드, 호번호, 지불코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMonthUseEnergyList : 원격검침관리 요금리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">호번호</param>
        /// <param name="strPaidCd">지불코드</param>
        /// <returns></returns>
        public static DataTable SpreadMonthUseEnergyList(string strRentCd, string strRoomNo, string strPaidCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectMonthUseEnergyList(strRentCd, strRoomNo, strPaidCd);

            return dtReturn;
        }

        #endregion

        #region SpreadMonthUseEnergyListForTower : 타워용 원격검침관리 요금리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMonthUseEnergyListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-04
         * 용       도 : 타워용 원격검침관리 요금리스트 조회
         * Input    값 : SpreadMonthUseEnergyListForTower(임대구분코드, 호번호, 지불코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMonthUseEnergyListForTower : 타워용 원격검침관리 요금리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">호번호</param>
        /// <param name="strPaidCd">지불코드</param>
        /// <returns></returns>
        public static DataTable SpreadMonthUseEnergyListForTower(string strRentCd, string strRoomNo, string strPaidCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectMonthUseEnergyForTowerList(strRentCd, strRoomNo, strPaidCd);

            return dtReturn;
        }

        #endregion

        #region SpreadMonthExcelEnergyList : 원격검침관리 요금설정리스트 조회(엑셀용)

        /**********************************************************************************************
         * Mehtod   명 : SpreadMonthExcelEnergyList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-13
         * 용       도 : 원격검침관리 요금설정리스트 조회
         * Input    값 : SpreadMonthExcelEnergyList(엑셀년도, 엑셀월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMonthExcelEnergyList : 원격검침관리 요금설정리스트 조회
        /// </summary>
        /// <param name="strExcelYear">엑셀년도</param>
        /// <param name="strExcelMM">엑셀월</param>
        /// <returns></returns>
        public static DataTable SpreadMonthExcelEnergyList(string strExcelYear, string strExcelMM)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectMonthExcelEnergyList(strExcelYear, strExcelMM);

            return dtReturn;
        }

        #endregion

        #region SpreadMonthExcelEnergyListForTower : 타워용 원격검침관리 요금설정리스트 조회(엑셀용)

        /**********************************************************************************************
         * Mehtod   명 : SpreadMonthExcelEnergyListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-04
         * 용       도 : 타워용 원격검침관리 요금설정리스트 조회
         * Input    값 : SpreadMonthExcelEnergyListForTower(엑셀년도, 엑셀월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMonthExcelEnergyListForTower : 타워용 원격검침관리 요금설정리스트 조회
        /// </summary>
        /// <param name="strExcelYear">엑셀년도</param>
        /// <param name="strExcelMM">엑셀월</param>
        /// <returns></returns>
        public static DataTable SpreadMonthExcelEnergyListForTower(string strExcelYear, string strExcelMM)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectMonthExcelEnergyListForTower(strExcelYear, strExcelMM);

            return dtReturn;
        }

        #endregion

        #region SpreadUtilChargeInfoList : 경남비나용 원격검침관리 요금설정리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadUtilChargeInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-20
         * 용       도 : 경남비나용 원격검침관리 요금설정리스트 조회
         * Input    값 : strRentCd, strChargeTy
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 경남비나용 원격검침관리 요금설정리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금종류</param>
        /// <returns></returns>
        public static DataTable SpreadUtilChargeInfoList(string strRentCd, string strChargeTy)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectUtilChargeInfoList(strRentCd, strChargeTy);

            return dtReturn;
        }

        #endregion

        #region SelectUtilChargeSetInfoList : 경남비나용 원격검침관리 요금설정리스트 조회2

        /**********************************************************************************************
         * Mehtod   명 : SelectUtilChargeSetInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-03
         * 용       도 : 경남비나용 원격검침관리 요금설정리스트 조회2
         * Input    값 : strRentCd, strChargeTy
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 경남비나용 원격검침관리 요금설정리스트 조회2
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금종류</param>
        /// <returns>DataSet</returns>
        public static DataSet SelectUtilChargeSetInfoList(string strRentCd, string strChargeTy)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectUtilChargeSetInfoList(strRentCd, strChargeTy);

            return dsReturn;
        }

        public static DataSet SelectUtilChargeSetList(string strRentCd, string strChargeTy)
        {

            var dsReturn = RemoteMngDao.SelectUtilChargeSetList(strRentCd, strChargeTy);

            return dsReturn;
        }

        #endregion

        #region SelectUtilChargeSetInfoList : 경남비나용 원격검침관리 요금설정리스트 조회3

        /**********************************************************************************************
         * Mehtod   명 : SelectUtilChargeSetInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-03
         * 용       도 : 경남비나용 원격검침관리 요금설정리스트 조회3
         * Input    값 : strRentCd, strRoomNo, strChargeTy
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectUtilChargeSetInfoList : 경남비나용 원격검침관리 요금설정리스트 조회3
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strChargeTy">요금종류</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectUtilChargeSetInfoList(string strRentCd, string strRoomNo, string strChargeTy)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectUtilChargeSetInfoList(strRentCd, strRoomNo, strChargeTy);

            return dtReturn;
        }

        #endregion

        #region SelectUtilChargeSetAddonList : 경남비나용 원격검침관리 에어컨 및 공조기 요금설정리스트

        /**********************************************************************************************
         * Mehtod   명 : SelectUtilChargeSetAddonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-09
         * 용       도 : 경남비나용 원격검침관리 에어컨 및 공조기 요금설정리스트
         * Input    값 : strRentCd, strRoomNo, strChargeTy
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectUtilChargeSetAddonList : 경남비나용 원격검침관리 에어컨 및 공조기 요금설정리스트
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strChargeTy">요금종류</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectUtilChargeSetAddonList(string strRentCd, string strRoomNo, string strChargeTy)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.SelectUtilChargeSetAddonList(strRentCd, strRoomNo, strChargeTy);

            return dtReturn;
        }

        #endregion

        #region SpreadUtilMonthUseChargeList : 경남비나용 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadUtilMonthUseChargeList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-20
         * 용       도 : 경남비나용 월별 검침 데이터 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadUtilMonthUseChargeList : 경남비나용 월별 검침 데이터 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataSet SpreadUtilMonthUseChargeList(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectUtilMonthUseChargeList(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region SelectUtilMonthUseChargeListForRoom : 경남비나용 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectUtilMonthUseChargeListForRoom
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-09
         * 용       도 : 경남비나용 월별 검침 데이터 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectUtilMonthUseChargeListForRoom : 경남비나용 월별 검침 데이터 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataSet SelectUtilMonthUseChargeListForRoom(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectUtilMonthUseChargeListForRoom(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region SpreadUtilDayUseChargeList : 일별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadUtilDayUseChargeList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 일별 검침 데이터 조회
         * Input    값 : SpreadUtilDayUseChargeList(페이지크기, 현재페이지, 임대구분코드, 요금타입, 연도, 월, 층, 방번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadUtilDayUseChargeList : 일별 검침 데이터 조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataSet SpreadUtilDayUseChargeList(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectUtilDayUseChargeList(intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo);

            return dsReturn;
        }

        #endregion

        #region SpreadDayChargeListForTower : 타워용 일별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadDayChargeListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 타워용 월별 검침 데이터 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadDayChargeListForTower : 타워용 일별 검침 데이터 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <returns></returns>
        public static DataSet SpreadDayChargeListForTower(string strRentCd, string strRoomNo, string strYear, string strMonth, string strChargeTy, int intPageSize, int intNowPage)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RemoteMngDao.SelectDayChargeListForTower(strRentCd, strRoomNo, strYear, strMonth, strChargeTy, intPageSize, intNowPage);

            return dsReturn;
        }

        #endregion

        #region RegistryChargeInfo : 요금 추가등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryChargeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-27
         * 용       도 : 요금 추가등록
         * Input    값 : strRentCd, strChargeTy, intAmountStart, intAmountEnd, strChargeStartDt, fltCharge, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 요금 추가등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intAmountStart">사용양(시작)</param>
        /// <param name="intAmountEnd">사용양(끝)</param>
        /// <param name="strChargeStartDt">적용시점</param>
        /// <param name="fltCharge">금액</param>
        /// <param name="strInsCompNo">기업코드번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static DataTable RegistryChargeInfo(string strRentCd, string strChargeTy, int intAmountStart, int intAmountEnd, string strChargeStartDt,
                                                   double fltCharge, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.InsertChargeInfo(strRentCd, strChargeTy, intAmountStart, intAmountEnd, strChargeStartDt, fltCharge, strInsCompNo, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion

        #region RegistryUtilChargeInfo : 요금 추가 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryUtilChargeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-20
         * 용       도 : 연체요율 추가 등록
         * Input    값 : strRentCd, strChargeTy, intAmountStart, intAmountEnd, strChargeStartDt, fltCharge, strInsCompNo, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 요금 추가등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="dblGenCharge">일반요금</param>
        /// <param name="dblPeakCharge">피크요금</param>
        /// <param name="dblNightCharge">심야요금</param>
        /// <param name="strChargeStartDt">적용시점</param>
        /// <param name="strInsCompNo">기업코드번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static DataTable RegistryUtilChargeInfo(string strRentCd, string strChargeTy, double dblGenCharge, double dblPeakCharge, double dblNightCharge,
                                                       string strChargeStartDt, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.InsertUtilChargeInfo(strRentCd, strChargeTy, dblGenCharge, dblPeakCharge, dblNightCharge, strChargeStartDt, strInsCompNo,
                                                         strInsMemNo, strInsMemIp);
            return dtReturn;
        }
        //Create Common Fee Utility
        public static DataTable RegistryUtilChargeInfoCommon(string strRentCd, string strChargeTy, int chargeSeq,double dblGenCharge, double dblPeakCharge, double dblNightCharge,
                                                       string strChargeStartDt)
        {
            var dtReturn = RemoteMngDao.InsertUtilChargeInfoCommon(strRentCd, strChargeTy,chargeSeq ,dblGenCharge, dblPeakCharge, dblNightCharge, strChargeStartDt);
            return dtReturn;
        }

        #endregion

        #region RegistryMonthEnergyPO : PO측 데이터 수동 생성

        /**********************************************************************************************
         * Mehtod   명 : RegistryMonthEnergyPO
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-30
         * 용       도 : PO측 데이터 수동 생성
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMonthEnergyPO : PO측 데이터 수동 생성
        /// </summary>
        /// <returns></returns>
        public static object[] RegistryMonthEnergyPO()
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.InsertMonthEnergyPO();

            return objReturn;
        }

        #endregion

        #region InsertUtilChargeInfoForRoom : 방별 요금 추가 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertUtilChargeInfoForRoom : 방별 요금 추가 등록
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-05
         * 용       도 : 방별 요금 추가 등록
         * Input    값 : InsertUtilChargeInfoForRoom(strRentCd, strRoomNo, strChargeTy, intAmountStart, intAmountEnd, strChargeStartDt, fltCharge, strInsCompNo, 
         *                                           strInsMemNo, strInsMemIp)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertUtilChargeInfoForRoom : 방별 요금 추가 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="dblGenCharge">일반요금</param>
        /// <param name="dblPeakCharge">피크요금</param>
        /// <param name="dblNightCharge">심야요금</param>
        /// <param name="strChargeStartDt">적용시점</param>
        /// <param name="strInsCompNo">기업코드번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static DataTable InsertUtilChargeInfoForRoom(string strRentCd, string strRoomNo, string strChargeTy, double dblGenCharge, double dblPeakCharge, double dblNightCharge,
                                                     string strChargeStartDt, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.InsertUtilChargeInfoForRoom(strRentCd, strRoomNo, strChargeTy, dblGenCharge, dblPeakCharge, dblNightCharge, strChargeStartDt, strInsCompNo,
                                                                strInsMemNo, strInsMemIp);

            return dtReturn;
        }
        //Baotv
        public static DataTable InsertUtilChargeIndi(string strRentCd, string strRoomNo,string strUserSeq, string strChargeTy, int chargeSeq, double dblGenCharge, double dblPeakCharge, double dblNightCharge, double dbOverTime, double dbOtherTime,
                                                     string strChargeStartDt)
        {
            var dtReturn = RemoteMngDao.InsertUtilChargeInfoIndi(strRentCd, strRoomNo, strUserSeq, strChargeTy, chargeSeq, dblGenCharge, dblPeakCharge, dblNightCharge, dbOverTime, dbOtherTime, strChargeStartDt);

            return dtReturn;
        }

        #endregion
        
        #region InsertUtilChargeInfoForAC : 방별 에어컨 및 공조기 요금 추가 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertUtilChargeInfoForAC : 방별 에어컨 및 공조기 요금 추가 등록
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-09
         * 용       도 : 방별 에어컨 및 공조기 요금 추가 등록
         * Input    값 : InsertUtilChargeInfoForAC(strRentCd, strRoomNo, strChargeTy, dblACCharge, dblHVACCharge, strChargeStartDt, strInsCompNo, strInsMemNo, strInsMemIp)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUtilChargeInfoForAC : 방별 에어컨 및 공조기 요금 추가 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="dblACCharge">에어컨요금</param>
        /// <param name="dblHVACCharge">공조요금</param>
        /// <param name="strChargeStartDt">적용시점</param>
        /// <param name="strInsCompNo">기업코드번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static object[] InsertUtilChargeInfoForAC(string strRentCd, string strRoomNo, string strChargeTy, double dblACCharge, double dblHVACCharge,
                                                          string strChargeStartDt, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.InsertUtilChargeInfoForAC(strRentCd, strRoomNo, strChargeTy, dblACCharge, dblHVACCharge, strChargeStartDt, strInsCompNo, strInsMemNo,
                                                               strInsMemIp);

            return objReturn;
        }

        #endregion

        #region InsertHoadonForUtilFee : 유틸리티 요금 화돈 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertHoadonForUtilFee : 유틸리티 요금 화돈 등록
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-13
         * 용       도 : 유틸리티 요금 화돈 등록
         * Input    값 : InsertHoadonForUtilFee(섹션코드, 에너지대상년, 에너지대상월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertHoadonForUtilFee : 유틸리티 요금 화돈 등록
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strEnergyYear">에너지대상년</param>
        /// <param name="strEnergyMonth">에너지대상월</param>
        /// <returns>object[]</returns>
        public static object[] InsertHoadonForUtilFee(string strRentCd, string strEnergyYear, string strEnergyMonth)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.InsertHoadonForUtilFee(strRentCd, strEnergyYear, strEnergyMonth);

            return objReturn;
        }

        #endregion

        #region ModifyChargeInfo : 요금 수정처리

        /**********************************************************************************************
         * Mehtod   명 : ModifyChargeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-27
         * 용       도 : 요금 수정처리
         * Input    값 : strRentCd, strChargeTy, intChargeSeq, intAmountStart, intAmountEnd, strChargeStartDt, fltCharge, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 요금 수정처리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intChargeSeq">요금순서</param>
        /// <param name="intAmountStart">사용양(시작)</param>
        /// <param name="intAmountEnd">사용양(끝)</param>
        /// <param name="strChargeStartDt">적용시점</param>
        /// <param name="fltCharge">금액</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static DataTable ModifyChargeInfo(string strRentCd, string strChargeTy, int intChargeSeq, int intAmountStart, int intAmountEnd, string strChargeStartDt, double fltCharge, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.UpdateChargeInfo(strRentCd, strChargeTy, intChargeSeq, intAmountStart, intAmountEnd, strChargeStartDt, fltCharge, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion

        #region ModifyUtilChargeInfo : 타워용 요금 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : ModifyUtilChargeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-20
         * 용       도 : 타워용 요금 수정 처리
         * Input    값 : strRentCd, strChargeTy, intChargeSeq, intAmountStart, intAmountEnd, strChargeStartDt, fltCharge, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 요금 수정처리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intChargeSeq">요금순서</param>
        /// <param name="dblGenCharge">일반요금</param>
        /// <param name="dblPeakCharge">피크요금</param>
        /// <param name="dblNightCharge">심야요금</param>
        /// <returns></returns>
        public static DataTable ModifyUtilChargeInfo(string strRentCd, string strChargeTy, int intChargeSeq, double dblGenCharge, double dblPeakCharge, double dblNightCharge)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.UpdateUtilChargeInfo(strRentCd, strChargeTy, intChargeSeq, dblGenCharge, dblPeakCharge, dblNightCharge);

            return dtReturn;
        }

        #endregion 

        #region ModifyMonthAmountUse : 월간 사용량 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMonthAmountUse
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-04-13
         * 용       도 : 월간 사용량 수정
         * Input    값 : ModifyMonthAmountUse(섹션구분코드, 년월정보, 방정보, 요금타입, 월간사용량)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMonthAmountUse : 월간 사용량 수정
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strAmountUse">월간사용량</param>
        /// <returns></returns>
        public static object[] ModifyMonthAmountUse(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, string strAmountUse)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.UpdateMonthAmountUse(strRentCd, strEnergyMonth, strRoomNo, strChargeTy, strAmountUse);

            return objReturn;
        }

        #endregion  
                
        #region ModifyMonthReceit : 완납 / 미납처리

        /**********************************************************************************************
         * Mehtod   명 : ModifyMonthReceit
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-11
         * 용       도 : 완납 / 미납처리
         * Input    값 : ModifyMonthReceit(섹션구분코드, 년월정보, 방정보, 요금타입, 수납여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMonthReceit : 완납 / 미납처리
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strPaidCd">수납여부</param>
        /// <returns></returns>
        public static object[] ModifyMonthReceit(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, string strPaidCd)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.UpdateMonthReceit(strRentCd, strEnergyMonth, strRoomNo, strChargeTy, strPaidCd);

            return objReturn;
        }

        #endregion

        #region ModifyMonthReceitForTower : 타워용 완납 / 미납처리

        /**********************************************************************************************
         * Mehtod   명 : ModifyMonthReceitForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2014-06-04
         * 용       도 : 완납 / 미납처리
         * Input    값 : ModifyMonthReceitForTower(섹션구분코드, 년월정보, 방정보, 요금타입, 수납여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMonthReceitForTower : 완납 / 미납처리
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strPaidCd">수납여부</param>
        /// <returns></returns>
        public static object[] ModifyMonthReceitForTower(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, string strPaidCd)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.UpdateMonthReceitForTower(strRentCd, strEnergyMonth, strRoomNo, strChargeTy, strPaidCd);

            return objReturn;
        }

        #endregion

        #region ModifyMonthEnergySeq : 유틸리티비용 Sequence 할당

        /**********************************************************************************************
         * Mehtod   명 : ModifyMonthEnergySeq
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-11
         * 용       도 : 유틸리티비용 Sequence 할당
         * Input    값 : ModifyMonthReceit(섹션구분코드, 년월정보, 방정보, 요금타입, 유틸순번, 지불일자, 지불일자순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMonthEnergySeq : 유틸리티비용 Sequence 할당
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intPaySeq">유틸순번</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자순번</param>
        /// <returns></returns>
        public static object[] ModifyMonthEnergySeq(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, int intPaySeq,
                                                    string strPaymentDt, int intPaymentSeq)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.UpdateMonthEnergySeq(strRentCd, strEnergyMonth, strRoomNo, strChargeTy, intPaySeq, strPaymentDt, intPaymentSeq);

            return objReturn;
        }

        #endregion

        #region ModifyAmountUsed : 사용량 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyAmountUsed
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-28
         * 용       도 : 사용량 수정
         * Input    값 : strRentCd, strChargeTy, strEnergyDay, intFlooNo, strRoomNo, strAmountUse
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// ModifyAmountUsed : 사용량 수정
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strEnergyDay">요일</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">번호</param>
        /// <param name="strAmountUse">사용량</param>
        /// <returns></returns>
        public static DataTable ModifyAmountUse(string strRentCd, string strChargeTy, string strEnergyDay, int intFlooNo, string strRoomNo, string strAmountUse)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.UpdateAmountUse(strRentCd, strChargeTy, strEnergyDay, intFlooNo, strRoomNo, strAmountUse);

            return dtReturn;
        }

        #endregion        

        #region ModifyUtilityFee : 각종 세금 수납

        /**********************************************************************************************
         * Mehtod   명 : ModifyUtilityFee
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-06
         * 용       도 : 각종 세금 수납
         * Input    값 : ModifyUtilityFee(섹션코드, 요금종류, 해당월, 방번호, 지불수단, 지불금액, 회사코드, 입력사번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// ModifyUtilityFee : 각종 세금 수납
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strChargeTy">요금종류</param>
        /// <param name="strEnergyMonth">해당월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strPaymentCd">지불수단</param>
        /// <param name="dblPayedUtilAmt">지불금액</param>
        /// <param name="strUtilCompNo">회사코드</param>
        /// <param name="strUtilMemNo">입력사번</param>
        /// <returns></returns>
        public static DataTable ModifyUtilityFee(string strRentCd, string strChargeTy, string strEnergyMonth, string strRoomNo, string strPaymentCd, double dblPayedUtilAmt,
                                                string strUtilCompNo, string strUtilMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.UpdateUtilityFee(strRentCd, strChargeTy, strEnergyMonth, strRoomNo, strPaymentCd, dblPayedUtilAmt, strUtilCompNo, strUtilMemNo);

            return dtReturn;
        }

        #endregion

        #region ModifyMonthChargeListForTower : 타워용 요금 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : ModifyMonthChargeListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 타워용 요금 수정 처리
         * Input    값 : ModifyMonthChargeListForTower(임대구분코드, 해당월, 층, 호실, 에너지타입, 일반시침, 
         *                                             일반사용량, 피크시침, 피크사용량, 심야시침, 심야사용량)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// ModifyMonthChargeListForTower : 타워용 요금 수정 처리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strEnergyMonth">해당월</param>
        /// <param name="strFloorNo">층</param>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strChargeTy">에너지타입</param>
        /// <param name="dblGenVal">일반시침</param>
        /// <param name="dblGenUse">일반사용량</param>
        /// <param name="dblPeakVal">피크시침</param>
        /// <param name="dblPeakUse">피크사용량</param>
        /// <param name="dblNightVal">심야시침</param>
        /// <param name="dblNightUse">심야사용량</param>
        /// <returns></returns>
        public static object[] ModifyMonthChargeListForTower(string strRentCd, string strEnergyMonth, string strFloorNo, string strRoomNo, string strChargeTy, double dblGenVal, double dblGenUse,
                                                             double dblPeakVal, double dblPeakUse, double dblNightVal, double dblNightUse, double dblACVal, double dblACUse, 
                                                             double dblHVACVal, double dblHVACUse)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.UpdateMonthChargeListForTower(strRentCd, strEnergyMonth, strFloorNo, strRoomNo, strChargeTy, dblGenVal, dblGenUse, dblPeakVal, dblPeakUse,
                                                                   dblNightVal, dblNightUse, dblACVal, dblACUse, dblHVACVal, dblHVACUse);

            return objReturn;
        }

        public static object[] ModifyMonthChargeListForTower(string strRentCd, string strEnergyMonth, string strFloorNo, string strRoomNo, string strChargeTy, double dblGenVal, double dblGenUse,
                                                              double dblPeakVal, double dblPeakUse, double dblNightVal, double dblNightUse)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.UpdateMonthChargeListForTower(strRentCd, strEnergyMonth, strFloorNo, strRoomNo, strChargeTy, dblGenVal, dblGenUse, dblPeakVal, dblPeakUse,
                                                                   dblNightVal, dblNightUse, 0, 0, 0, 0);

            return objReturn;
        }

        #endregion

        #region ModifyDayChargeListForTower : 타워용 일일 요금 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : ModifyDayChargeListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 타워용 일일 요금 수정 처리
         * Input    값 : ModifyDayChargeListForTower(임대구분코드, 해당일, 호실, 에너지타입, 일반시침, 일반사용량, 피크시침, 피크사용량, 심야시침, 심야사용량)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// ModifyDayChargeListForTower : 타워용 일일 요금 수정 처리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strEnergyDay">해당일</param>
        /// <param name="strFloorNo">층</param>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strChargeTy">에너지타입</param>
        /// <param name="dblGenVal">일반시침</param>
        /// <param name="dblGenUse">일반사용량</param>
        /// <param name="dblPeakVal">피크시침</param>
        /// <param name="dblPeakUse">피크사용량</param>
        /// <param name="dblNightVal">심야시침</param>
        /// <param name="dblNightUse">심야사용량</param>
        /// <returns></returns>
        public static object[] ModifyDayChargeListForTower(string strRentCd, string strEnergyDay, string strFloorNo, string strRoomNo, string strChargeTy, double dblGenVal,
                                                           double dblGenUse, double dblPeakVal, double dblPeakUse, double dblNightVal, double dblNightUse)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.UpdateDayChargeListForTower(strRentCd, strEnergyDay, strFloorNo, strRoomNo, strChargeTy, dblGenVal, dblGenUse, dblPeakVal, dblPeakUse, 
                                                                 dblNightVal, dblNightUse);

            return objReturn;
        }

        #endregion 

        #region ModifyUtilityFeeForTower : 각종 세금 수납

        /**********************************************************************************************
         * Mehtod   명 : ModifyUtilityFeeForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-04
         * 용       도 : 각종 세금 수납
         * Input    값 : ModifyUtilityFeeForTower(섹션코드, 요금종류, 해당월, 방번호, 지불수단, 지불금액, 회사코드, 입력사번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// ModifyUtilityFeeForTower : 각종 세금 수납
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strChargeTy">요금종류</param>
        /// <param name="strEnergyMonth">해당월</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strPaymentCd">지불수단</param>
        /// <param name="dblPayedUtilAmt">지불금액</param>
        /// <param name="strUtilCompNo">회사코드</param>
        /// <param name="strUtilMemNo">입력사번</param>
        /// <returns></returns>
        public static DataTable ModifyUtilityFeeForTower(string strRentCd, string strChargeTy, string strEnergyMonth, string strRoomNo, string strPaymentCd, double dblPayedUtilAmt,
                                                         string strUtilCompNo, string strUtilMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.UpdateUtilityFeeForTower(strRentCd, strChargeTy, strEnergyMonth, strRoomNo, strPaymentCd, dblPayedUtilAmt, strUtilCompNo, strUtilMemNo);

            return dtReturn;
        }

        #endregion

        #region ModifyMonthEnergySeqForTower : 유틸리티비용 Sequence 할당

        /**********************************************************************************************
         * Mehtod   명 : ModifyMonthEnergySeqForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-04
         * 용       도 : 유틸리티비용 Sequence 할당
         * Input    값 : ModifyMonthEnergySeqForTower(섹션구분코드, 년월정보, 방정보, 요금타입, 유틸순번, 지불일자, 지불일자순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMonthEnergySeqForTower : 유틸리티비용 Sequence 할당
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intPaySeq">유틸순번</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자순번</param>
        /// <returns></returns>
        public static object[] ModifyMonthEnergySeqForTower(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, int intPaySeq,
                                                            string strPaymentDt, int intPaymentSeq)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.UpdateMonthEnergySeqForTower(strRentCd, strEnergyMonth, strRoomNo, strChargeTy, intPaySeq, strPaymentDt, intPaymentSeq);

            return objReturn;
        }

        #endregion


        #region UpdateMonthChargeSetForRoom : 타워용 개별요금 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthChargeSetForRoom
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-09
         * 용       도 : 타워용 개별요금 수정 처리
         * Input    값 : UpdateMonthChargeListForTower(임대구분코드, 호, 에너지타입, 순번, 일반단가, 피크단가, 심야단가, 에어컨단가, 공조단가)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateMonthChargeSetForRoom : 타워용 개별요금 수정 처리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strChargeTy">에너지타입</param>
        /// <param name="intChargeSeq">순번</param>
        /// <param name="dblGenCharge">일반단가</param>
        /// <param name="dblPeakCharge">피크단가</param>
        /// <param name="dblNightCharge">심야단가</param>
        /// <param name="dblACCharge">에어컨단가</param>
        /// <param name="dblHVACCharge">공조단가</param>
        /// <returns></returns>
        public static object[] UpdateMonthChargeSetForRoom(string strRentCd, string strRoomNo, string strChargeTy, int intChargeSeq, double dblGenCharge, double dblPeakCharge,
                                                           double dblNightCharge, double dblACCharge, double dblHVACCharge)
        {
            var objReturn = RemoteMngDao.UpdateMonthChargeSetForRoom(strRentCd, strRoomNo, strChargeTy, intChargeSeq, dblGenCharge, dblPeakCharge, dblNightCharge, dblACCharge,
                                                                          dblHVACCharge);

            return objReturn;
        }

        #endregion

        #region RemoveChargeInfo : 요금 삭제처리

        /**********************************************************************************************
         * Mehtod   명 : RemoveChargeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-27
         * 용       도 : 요금 삭제처리
         * Input    값 : strRentCd, strChargeTy, intChargeSeq
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 요금 삭제처리
        /// </summary>
        /// <param name="strRentCd">요금구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intChargeSeq">요금순서</param>
        /// <returns></returns>
        public static DataTable RemoveChargeInfo(string strRentCd, string strChargeTy, int intChargeSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.DeleteChargeInfo(strRentCd, strChargeTy, intChargeSeq);

            return dtReturn;
        }

        #endregion  
 
        #region RemoveUtilChargeInfo : 타워용 요금 삭제처리

        /**********************************************************************************************
         * Mehtod   명 : RemoveUtilChargeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-20
         * 용       도 : 타워용 요금 삭제처리
         * Input    값 : strRentCd, strChargeTy, intChargeSeq
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 요금 삭제처리
        /// </summary>
        /// <param name="strRentCd">요금구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intChargeSeq">요금순서</param>
        /// <returns></returns>
        public static DataTable RemoveUtilChargeInfo(string strRentCd, string strChargeTy, int intChargeSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.DeleteUtilChargeInfo(strRentCd, strChargeTy, intChargeSeq);

            return dtReturn;
        }

        public static DataTable RemoveUtilCommonInfo(string strRentCd, string strChargeTy, int intChargeSeq)
        {
            var dtReturn = RemoteMngDao.RemoveUtilCommonInfo(strRentCd, strChargeTy, intChargeSeq);

            return dtReturn;
        }

        #endregion 

        #region RemoveChargeInfo : 요금 전체초기화

        /**********************************************************************************************
         * Mehtod   명 : RemoveChargeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-27
         * 용       도 : 요금 전체초기화
         * Input    값 : strRentCd, strChargeTy
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 요금 전체초기화
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <returns></returns>
        public static DataTable RemoveChargeInfo(string strRentCd, string strChargeTy)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RemoteMngDao.DeleteChargeInfo(strRentCd, strChargeTy);

            return dtReturn;
        }

        #endregion

        #region DeleteUtilChargeInfoForRoom : 타워용 개별요금 삭제처리

        /**********************************************************************************************
         * Mehtod   명 : DeleteUtilChargeInfoForRoom
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-09
         * 용       도 : 타워용 개별요금 삭제처리
         * Input    값 : strRentCd, strChargeTy, intChargeSeq
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteUtilChargeInfoForRoom : 타워용 개별요금 삭제처리
        /// </summary>
        /// <param name="strRentCd">요금구분코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intChargeSeq">요금순서</param>
        /// <returns>object[]</returns>
        public static object[] DeleteUtilChargeInfoForRoom(string strRentCd, string strRoomNo, string strChargeTy, int intChargeSeq)
        {
            object[] objReturn = new object[2];

            objReturn = RemoteMngDao.DeleteUtilChargeInfoForRoom(strRentCd, strRoomNo, strChargeTy, intChargeSeq);

            return objReturn;
        }

        //BaoTV
        public static object[] DeleteUtilChargeIndi(string strRentCd, string strRoomNo, string userSeq,string strChargeTy, int intChargeSeq)
        {
            var objReturn = RemoteMngDao.DeleteUtilChargeIndi(strRentCd, strRoomNo,userSeq, strChargeTy, intChargeSeq);

            return objReturn;
        }

        #endregion 

        #region RegistryCashReportManually :    baotv

        /**********************************************************************************************
         * Mehtod   명 : RegistryCashReportManually : 방별 에어컨 및 공조기 요금 추가 등록
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-09
         * 용       도 : 방별 에어컨 및 공조기 요금 추가 등록
         * Input    값 : RegistryCashReportManually()
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// RegistryCashReportManually : 방별 에어컨 및 공조기 요금 추가 등록
        /// </summary>
        /// <param name="roomNo"> </param>
        /// <param name="rentalYear"> </param>
        /// <param name="rentalMonth"> </param>
        /// <param name="monthFee"> </param>
        /// <param name="payDate"> </param>
        /// <param name="paidFee"> </param>
        /// <param name="cash"> </param>
        /// <param name="card"> </param>
        /// <param name="transfer"> </param>
        /// <param name="feeType"> </param>
        /// <param name="rentCd"> </param>
        /// <param name="paymentCd"> </param>
        /// <returns></returns>
        public static object[] RegistryCashReportManually(string roomNo, string rentalYear, string rentalMonth, double monthFee, string payDate, double paidFee, double cash, double card, double transfer, string feeType, string rentCd, string paymentCd)
        {
            var objReturn = RemoteMngDao.RegistryCashReportManually(roomNo, rentalYear, rentalMonth, monthFee, payDate, paidFee, cash, card, transfer,feeType,rentCd,paymentCd);

            return objReturn;
        }

        public static object[] DebitListByExcel(string roomNo, double rAmount, string period, double amount, string feeTy, string payDay, string paymentTy, string feetyDt)
        {
            var objReturn = RemoteMngDao.DebitListByExcel(roomNo,rAmount,period,amount,feeTy,payDay,paymentTy,feetyDt);

            return objReturn;
        }

        public static object[] BalanceTowerByExcel(string ksysCd,  string feeTy, string feetyDt,double amount)
        {
            var objReturn = RemoteMngDao.BalanceTowerByExcel(ksysCd, feeTy, feetyDt, amount);

            return objReturn;
        }

        #endregion

        #region SelectCashReport : BAOTV

        /**********************************************************************************************
         * Mehtod   명 : SelectCashReport
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-09
         * 용       도 : 경남비나용 월별 검침 데이터 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/

        /// <summary>
        /// SelectCashReport : 경남비나용 월별 검침 데이터 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy"> </param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strDay"> </param>
        /// <param name="datetype"> </param>
        /// <returns></returns>
        public static DataSet SelectCashReport(string strRentCd, string strFeeTy, string strYear, string strMonth, string strDay,string datetype)
        {
            var dsReturn = RemoteMngDao.SelectCashReport(strRentCd,strFeeTy,strYear,strMonth,strDay,datetype);

            return dsReturn;
        }

        #endregion
    }
}