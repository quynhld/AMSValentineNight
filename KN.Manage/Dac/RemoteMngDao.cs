using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Manage.Ent;

namespace KN.Manage.Dac
{
    public class RemoteMngDao
    {
        #region SelectChargeInfoList : 원격검침관리 요금설정리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectChargeInfoList
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
        public static DataTable SelectChargeInfoList(string strRentCd, string strChargeTy)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_CHARGEINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectChargeStartDtCheck : 중복 체크

        /**********************************************************************************************
         * Mehtod   명 : SelectChargeStartDtCheck
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
        public static DataTable SelectChargeStartDtCheck(string strRentCd, string strFeeTy, string strChargeStartDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strChargeStartDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_CHARGEINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectEnergyMonthCheck : 원격검침 Agent 정상작동 체크

        /**********************************************************************************************
         * Mehtod   명 : SelectEnergyMonthCheck
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-30
         * 용       도 : 원격검침 Agent 정상작동 체크
         * Input    값 : strRentCd(임대구분코드), strChargeTy(요금타입), strChargeStartDt(적용일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectEnergyMonthCheck : 원격검침 Agent 정상작동 체크
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectEnergyMonthCheck()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ENERGYMONTHCHECK_S00");

            return dtReturn;
        }

        #endregion

        #region SelectMonthUseChargeList : 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthUseChargeList
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
        public static DataSet SelectMonthUseChargeList(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strChargeTy;
            objParams[4] = strYear;
            objParams[5] = strMonth;
            objParams[6] = intFlooNo;
            objParams[7] = strRoomNo;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_CHARGEINFO_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMonthChargeListForTower : 타워용 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthChargeListForTower
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
        public static DataSet SelectMonthChargeListForTower(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strYear;
            objParams[3] = strMonth;
            objParams[4] = strChargeTy;
            objParams[5] = intPageSize;
            objParams[6] = intNowPage;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_MONTHENERGYFORTOWER_S00", objParams);

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
        /// 아파트리테일용 월별 검침 데이터 조회
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
            object[] objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strYear;
            objParams[3] = strMonth;
            objParams[4] = strChargeTy;
            objParams[5] = intPageSize;
            objParams[6] = intNowPage;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_MONTHENERGYFORTOWER_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectExcelMonthAmountUse : 엑셀용 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelMonthAmountUse
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-04-14
         * 용       도 : 엑셀용 정보 조회
         * Input    값 : SelectExcelMonthAmountUse(요금코드, 조회 대상년도, 조회 대상월, 입주자번호, 입주자명, 섹션구분코드, 층정보, 방정보)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExcelMonthAmountUse : 엑셀용 정보 조회
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
        public static DataTable SelectExcelMonthAmountUse(string strChargeTy, string strYear, string strMonth, string strUserSeq, string strUserNm, string strRentCd,
                                                          string strFloorNo, string strRoomNo)
        {
            object[] objParams = new object[7];
            DataTable dtReturn = new DataTable();

            objParams[0] = strYear;
            objParams[1] = strMonth;
            objParams[2] = strUserSeq;
            objParams[3] = strUserNm;
            objParams[4] = strRentCd;
            objParams[5] = strFloorNo;
            objParams[6] = strRoomNo;

            if (strRentCd.Equals(CommValue.RENTAL_VALUE_APT) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTA) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTB) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTSHOP) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTBSHOP))
            {
                if (strChargeTy.Equals(CommValue.CHARGETY_VALUE_ELECTRICITY))
                {
                    dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S01", objParams);
                }
                else if (strChargeTy.Equals(CommValue.CHARGETY_VALUE_WATER))
                {
                    dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S02", objParams);
                }
                else if (strChargeTy.Equals(CommValue.CHARGETY_VALUE_GAS))
                {
                    dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S03", objParams);
                }
            }
            else if (strRentCd.Equals(CommValue.RENTAL_VALUE_OFFICE) ||
                     strRentCd.Equals(CommValue.RENTAL_VALUE_SHOP))

            {
                if (strChargeTy.Equals(CommValue.CHARGETY_VALUE_ELECTRICITY))
                {
                    dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S05", objParams);
                }
                else if (strChargeTy.Equals(CommValue.CHARGETY_VALUE_WATER))
                {
                    dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S06", objParams);
                }
                else if (strChargeTy.Equals(CommValue.CHARGETY_VALUE_GAS))
                {
                    dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S07", objParams);
                }
            }

            return dtReturn;
        }

        #endregion 

        #region SelectDayUseChargeList : 일별 검침 데이터 조회(페이지)

        /**********************************************************************************************
         * Mehtod   명 : SelectDayUseChargeList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-29
         * 용       도 : 일별 검침 데이터 조회(페이지)
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo
         * Ouput    값 : DataSet
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
        public static DataSet SelectDayUseChargeList(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strChargeTy;
            objParams[4] = strYear;
            objParams[5] = strMonth;
            objParams[6] = intFlooNo;
            objParams[7] = strRoomNo;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_CHARGEINFO_S05", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMonthUseChargeList : 월별 검침 데이터 조회(그래프)

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthUseChargeList(그래프)
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
        public static DataTable SelectMonthUseChargeList(string strRentCd, string strChargeTy, string strYear, int intFlooNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = strYear;
            objParams[3] = intFlooNo;
            objParams[4] = strRoomNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_CHARGEINFO_S03", objParams);

            return dtReturn;
        }

        #endregion  

        #region SelectYearUseChargeList : 년별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectYearUseChargeList
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
        public static DataTable SelectYearUseChargeList(string strRentCd, string strChargeTy, int intFlooNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = intFlooNo;
            objParams[3] = strRoomNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_CHARGEINFO_S04", objParams);

            return dtReturn;
        }

        #endregion 

        #region SelectMonthUseEnergyList : 원격검침관리 요금리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthUseEnergyList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-04
         * 용       도 : 원격검침관리 요금리스트 조회
         * Input    값 : SelectMonthUseEnergyList(임대구분코드, 호번호, 지불코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMonthUseEnergyList : 원격검침관리 요금리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">호번호</param>
        /// <param name="strPaidCd">지불코드</param>
        /// <returns></returns>
        public static DataTable SelectMonthUseEnergyList(string strRentCd, string strRoomNo, string strPaidCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPaidCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S04", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMonthUseEnergyListForTower : 타워용 원격검침관리 요금리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthUseEnergyListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-04
         * 용       도 : 타워용 원격검침관리 요금리스트 조회
         * Input    값 : SelectMonthUseEnergyListForTower(임대구분코드, 호번호, 지불코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMonthUseEnergyListForTower : 타워용 원격검침관리 요금리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">호번호</param>
        /// <param name="strPaidCd">지불코드</param>
        /// <returns></returns>
        public static DataTable SelectMonthUseEnergyListForTower(string strRentCd, string strRoomNo, string strPaidCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPaidCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S09", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMonthUseEnergyForTowerList : 오피스 및 리테일용 원격검침관리 요금설정리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthUseEnergyForTowerList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-01
         * 용       도 : 오피스 및 리테일용 원격검침관리 요금설정리스트 조회
         * Input    값 : SelectMonthUseEnergyForTowerList(임대구분코드, 호번호, 지불코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMonthUseEnergyForTowerList : 원격검침관리 요금설정리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">호번호</param>
        /// <param name="strPaidCd">지불코드</param>
        /// <returns></returns>
        public static DataTable SelectMonthUseEnergyForTowerList(string strRentCd, string strRoomNo, string strPaidCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPaidCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S09", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMonthExcelEnergyList : 원격검침관리 요금설정리스트 조회(엑셀용)

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthExcelEnergyList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-13
         * 용       도 : 원격검침관리 요금설정리스트 조회
         * Input    값 : SelectMonthExcelEnergyList(엑셀년도, 엑셀월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMonthExcelEnergyList : 원격검침관리 요금설정리스트 조회
        /// </summary>
        /// <param name="strExcelYear">엑셀년도</param>
        /// <param name="strExcelMM">엑셀월</param>
        /// <returns></returns>
        public static DataTable SelectMonthExcelEnergyList(string strExcelYear, string strExcelMM)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strExcelYear;
            objParams[1] = strExcelMM;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S08", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMonthExcelEnergyListForTower : 타워용 원격검침관리 요금설정리스트 조회(엑셀용)

        /**********************************************************************************************
         * Mehtod   명 : SelectMonthExcelEnergyListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-04
         * 용       도 : 타워용 원격검침관리 요금설정리스트 조회
         * Input    값 : SelectMonthExcelEnergyListForTower(엑셀년도, 엑셀월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMonthExcelEnergyListForTower : 타워용 원격검침관리 요금설정리스트 조회
        /// </summary>
        /// <param name="strExcelYear">엑셀년도</param>
        /// <param name="strExcelMM">엑셀월</param>
        /// <returns></returns>
        public static DataTable SelectMonthExcelEnergyListForTower(string strExcelYear, string strExcelMM)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strExcelYear;
            objParams[1] = strExcelMM;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHENERGY_S10", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectManuallyRegistList : 수동생성 대상조회 (수도 및 전기세)

        /**********************************************************************************************
         * Mehtod   명 : SelectManuallyRegistList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-10
         * 용       도 : 수동생성 대상조회 (수도 및 전기세)
         * Input    값 : SelectManuallyRegistList(임대구분코드, 해당년, 해당월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectManuallyRegistList : 수동생성 대상조회 (수도 및 전기세)
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <returns></returns>
        public static DataTable SelectManuallyRegistList(string strRentCd, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_MONTHENERGY_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectUtilChargeInfoList : 경남비나용 원격검침관리 요금설정리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectUtilChargeInfoList
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
        public static DataTable SelectUtilChargeInfoList(string strRentCd, string strChargeTy)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_UTILCHARGEINFO_S00", objParams);

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
        /// SelectUtilChargeSetInfoList : 경남비나용 원격검침관리 요금설정리스트 조회2
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금종류</param>
        /// <returns>DataSet</returns>
        public static DataSet SelectUtilChargeSetInfoList(string strRentCd, string strChargeTy)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_UTILCHARGEINFO_S03", objParams);

            return dsReturn;
        }

        //BaoTV
        public static DataSet SelectUtilChargeSetList(string strRentCd, string strChargeTy)
        {
            var objParams = new object[2];
            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_UTILCHARGEINFO_S09", objParams);

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
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strChargeTy;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_UTILCHARGEINFO_S04", objParams);

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
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strChargeTy;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_UTILCHARGEINFO_S05", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectUtilMonthUseChargeList : 경남비나용 월별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectUtilMonthUseChargeList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-20
         * 용       도 : 경남비나용 월별 검침 데이터 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, intFlooNo, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectUtilMonthUseChargeList : 경남비나용 월별 검침 데이터 조회
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
        public static DataSet SelectUtilMonthUseChargeList(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strChargeTy;
            objParams[4] = strYear;
            objParams[5] = strMonth;
            objParams[6] = intFlooNo;
            objParams[7] = strRoomNo;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_UTILCHARGEINFO_S01", objParams);

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
            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strChargeTy;
            objParams[4] = strYear;
            objParams[5] = strMonth;
            objParams[6] = intFlooNo;
            objParams[7] = strRoomNo;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_UTILCHARGEINFO_S06", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectUtilDayUseChargeList : 일별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectUtilDayUseChargeList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 일별 검침 데이터 조회
         * Input    값 : SelectUtilDayUseChargeList(페이지크기, 현재페이지, 임대구분코드, 요금타입, 연도, 월, 층, 방번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectUtilDayUseChargeList : 일별 검침 데이터 조회
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
        public static DataSet SelectUtilDayUseChargeList(int intPageSize, int intNowPage, string strRentCd, string strChargeTy, string strYear, string strMonth, int intFlooNo, string strRoomNo)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strChargeTy;
            objParams[4] = strYear;
            objParams[5] = strMonth;
            objParams[6] = intFlooNo;
            objParams[7] = strRoomNo;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_UTILCHARGEINFO_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectDayChargeListForTower : 타워용 일별 검침 데이터 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectDayChargeListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 타워용 월별 검침 데이터 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strChargeTy, strYear, strMonth, strRoomNo
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectDayChargeListForTower : 타워용 일별 검침 데이터 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <returns></returns>
        public static DataSet SelectDayChargeListForTower(string strRentCd, string strRoomNo, string strYear, string strMonth, string strChargeTy, int intPageSize, int intNowPage)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strYear;
            objParams[3] = strMonth;
            objParams[4] = strChargeTy;
            objParams[5] = intPageSize;
            objParams[6] = intNowPage;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_DAYENERGYFORTOWER_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region InsertUtilFeeManuallyInfo : 수동생성 (수도 및 전기세)

        /**********************************************************************************************
         * Mehtod   명 : InsertUtilFeeManuallyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 (수도 및 전기세)
         * Input    값 : InsertUtilFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUtilFeeManuallyInfo : 수동생성 ( 아파트 관리비 )
        /// </summary>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <returns></returns>
        public static object[] InsertUtilFeeManuallyInfo(string strYear, string strMonth)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strYear;
            objParams[1] = strMonth;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_MONTHENERGY_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertChargeInfo : 요금 추가 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertChargeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-27
         * 용       도 : 연체요율 추가 등록
         * Input    값 : strRentCd, strChargeTy, intAmountStart, intAmountEnd, strChargeStartDt, fltCharge, strInsCompNo, strInsMemNo, strInsMemIp
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
        public static DataTable InsertChargeInfo(string strRentCd, string strChargeTy, int intAmountStart, int intAmountEnd, string strChargeStartDt,
                                                 double fltCharge, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = intAmountStart;
            objParams[3] = intAmountEnd;
            objParams[4] = strChargeStartDt;
            objParams[5] = fltCharge;
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            if (strRentCd.Equals(CommValue.RENTAL_VALUE_APTA) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTB) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APT))
            {
                dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_CHARGEINFO_M01", objParams);
            }
            else if (strRentCd.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTBSHOP) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTSHOP))
            {
                dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_CHARGEINFO_M02", objParams);
            }
            else
            {
                dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_CHARGEINFO_M00", objParams);
            }

            return dtReturn;
        }

        #endregion

        #region InsertUtilChargeInfo : 요금 추가 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertUtilChargeInfo
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
        public static DataTable InsertUtilChargeInfo(string strRentCd, string strChargeTy, double dblGenCharge, double dblPeakCharge, double dblNightCharge, 
                                                     string strChargeStartDt, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = dblGenCharge;
            objParams[3] = dblPeakCharge;
            objParams[4] = dblNightCharge;
            objParams[5] = strChargeStartDt;
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            if (strRentCd.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTBSHOP) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTSHOP))
            {
                dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_UTILCHARGEINFO_M01", objParams);
            }
            else
            {
                dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_UTILCHARGEINFO_M00", objParams);
            }

            return dtReturn;
        }

        public static DataTable InsertUtilChargeInfoCommon(string strRentCd, string strChargeTy,int chargeSeq, double dblGenCharge, double dblPeakCharge, double dblNightCharge,
                                                     string strChargeStartDt)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = dblGenCharge;
            objParams[3] = dblPeakCharge;
            objParams[4] = dblNightCharge;
            objParams[5] = strChargeStartDt;
            objParams[6] = chargeSeq;
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_UTILCHARGEINFO_M06", objParams);
            return dtReturn;
        }

        #endregion

        #region InsertMonthEnergyPO : PO측 데이터 수동 생성

        /**********************************************************************************************
         * Mehtod   명 : InsertMonthEnergyPO
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-30
         * 용       도 : PO측 데이터 수동 생성
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMonthEnergyPO : PO측 데이터 수동 생성
        /// </summary>
        /// <returns></returns>
        public static object[] InsertMonthEnergyPO()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_MONTHENERGY_LST_M03");

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
            object[] objParams = new object[10];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strChargeTy;
            objParams[3] = dblGenCharge;
            objParams[4] = dblPeakCharge;
            objParams[5] = dblNightCharge;
            objParams[6] = strChargeStartDt;
            objParams[7] = strInsCompNo;
            objParams[8] = strInsMemNo;
            objParams[9] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_UTILCHARGEINFO_M03", objParams);

            return dtReturn;
        }

        public static DataTable InsertUtilChargeInfoIndi(string strRentCd, string strRoomNo,string strUserSeq, string strChargeTy, int intChargeSeq, double dblGenCharge, double dblPeakCharge, double dblNightCharge, double dbOverTime, double dbOtherTime,
                                                     string strChargeStartDt)
        {
            var objParams = new object[11];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strUserSeq;
            objParams[3] = strChargeTy;
            objParams[4] = dblGenCharge;
            objParams[5] = dblPeakCharge;
            objParams[6] = dblNightCharge;
            objParams[7] = dbOverTime;
            objParams[8] = dbOtherTime;
            objParams[9] = strChargeStartDt;
            objParams[10] = intChargeSeq;
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_UTILCHARGEINFO_M07", objParams);

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
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strChargeTy;
            objParams[3] = dblACCharge;
            objParams[4] = dblHVACCharge;
            objParams[5] = strChargeStartDt;
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_UTILCHARGEINFO_M04", objParams);

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
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strEnergyYear;
            objParams[2] = strEnergyMonth;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_HOADONINFO_M04", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateChargeInfo : 요금 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : UpdateChargeInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-27
         * 용       도 : 요금 수정 처리
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
        public static DataTable UpdateChargeInfo(string strRentCd, string strChargeTy, int intChargeSeq, int intAmountStart, int intAmountEnd, string strChargeStartDt, double fltCharge, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = intChargeSeq;
            objParams[3] = intAmountStart;
            objParams[4] = intAmountEnd;
            objParams[5] = strChargeStartDt;
            objParams[6] = fltCharge;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_CHARGEINFO_M00", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdateUtilChargeInfo : 타워용 요금 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : UpdateUtilChargeInfo
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
        public static DataTable UpdateUtilChargeInfo(string strRentCd, string strChargeTy, int intChargeSeq, double dblGenCharge, double dblPeakCharge, double dblNightCharge)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = intChargeSeq;
            objParams[3] = dblGenCharge;
            objParams[4] = dblPeakCharge;
            objParams[5] = dblNightCharge;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_UTILCHARGEINFO_M00", objParams);

            return dtReturn;
        }

        #endregion 

        #region UpdateMonthAmountUse : 월간 사용량 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthAmountUse
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-04-13
         * 용       도 : 월간 사용량 수정
         * Input    값 : UpdateMonthAmountUse(섹션구분코드, 년월정보, 방정보, 요금타입, 월간사용량)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMonthAmountUse : 월간 사용량 수정
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strAmountUse">월간사용량</param>
        /// <returns></returns>
        public static object[] UpdateMonthAmountUse(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, string strAmountUse)
        {
            object[] objParams = new object[5];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strEnergyMonth;
            objParams[2] = strRoomNo;
            objParams[3] = strChargeTy;
            objParams[4] = double.Parse(strAmountUse);

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MONTHENERGY_M01", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateMonthReceit : 완납 / 미납처리

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthReceit
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-11
         * 용       도 : 완납 / 미납처리
         * Input    값 : UpdateMonthReceit(섹션구분코드, 년월정보, 방정보, 요금타입, 수납여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMonthReceit : 완납 / 미납처리
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strPaidCd">수납여부</param>
        /// <returns></returns>
        public static object[] UpdateMonthReceit(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, string strPaidCd)
        {
            object[] objParams = new object[5];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strEnergyMonth;
            objParams[2] = strRoomNo;
            objParams[3] = strChargeTy;
            objParams[4] = strPaidCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MONTHENERGY_M02", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateMonthReceitForTower : 타워용 완납 / 미납처리

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthReceitForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2014-06-04
         * 용       도 : 완납 / 미납처리
         * Input    값 : UpdateMonthReceitForTower(섹션구분코드, 년월정보, 방정보, 요금타입, 수납여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMonthReceitForTower : 완납 / 미납처리
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strPaidCd">수납여부</param>
        /// <returns></returns>
        public static object[] UpdateMonthReceitForTower(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, string strPaidCd)
        {
            object[] objParams = new object[5];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strEnergyMonth;
            objParams[2] = strRoomNo;
            objParams[3] = strChargeTy;
            objParams[4] = strPaidCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M01", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateMonthEnergySeq : 유틸리티비용 Sequence 할당

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthEnergySeq
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-11
         * 용       도 : 유틸리티비용 Sequence 할당
         * Input    값 : UpdateMonthEnergySeq(섹션구분코드, 년월정보, 방정보, 요금타입, 유틸순번, 지불일자, 지불일자순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMonthEnergySeq : 유틸리티비용 Sequence 할당
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intPaySeq">유틸순번</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자순번</param>
        /// <returns></returns>
        public static object[] UpdateMonthEnergySeq(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, int intPaySeq,
                                                    string strPaymentDt, int intPaymentSeq)
        {
            object[] objParams = new object[7];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strEnergyMonth;
            objParams[2] = strRoomNo;
            objParams[3] = strChargeTy;
            objParams[4] = intPaySeq;
            objParams[5] = strPaymentDt;
            objParams[6] = intPaymentSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MONTHENERGY_M03", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateAmountUse : 사용량 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateAmountUse
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-28
         * 용       도 : 사용량 수정
         * Input    값 : strRentCd, strChargeTy, strEnergyDay, intFlooNo, strRoomNo, strAmountUse
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 사용량 수정
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="strEnergyDay">요일</param>
        /// <param name="intFlooNo">층</param>
        /// <param name="strRoomNo">번호</param>
        /// <param name="strAmountUse">사용량</param>
        /// <returns></returns>
        public static DataTable UpdateAmountUse(string strRentCd, string strChargeTy, string strEnergyDay, int intFlooNo, string strRoomNo, string strAmountUse)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = strEnergyDay;
            objParams[3] = intFlooNo;
            objParams[4] = strRoomNo;
            objParams[5] = strAmountUse;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_CHARGEINFO_M01", objParams);

            return dtReturn;
        }

        #endregion  

        #region UpdateUtilityFee : 각종 세금 수납

        /**********************************************************************************************
         * Mehtod   명 : UpdateUtilityFee
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-06
         * 용       도 : 각종 세금 수납
         * Input    값 : UpdateUtilityFee(섹션코드, 요금종류, 해당월, 방번호, 지불수단, 지불금액, 회사코드, 입력사번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateUtilityFee : 각종 세금 수납
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
        public static DataTable UpdateUtilityFee(string strRentCd, string strChargeTy, string strEnergyMonth, string strRoomNo, string strPaymentCd, double dblPayedUtilAmt,
                                                 string strUtilCompNo, string strUtilMemNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[8];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = strEnergyMonth;
            objParams[3] = strRoomNo;
            objParams[4] = strPaymentCd;
            objParams[5] = dblPayedUtilAmt;
            objParams[6] = strUtilCompNo;
            objParams[7] = strUtilMemNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_MONTHENERGY_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdateMonthChargeListForTower : 타워용 월간 요금 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthChargeListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 타워용 요금 수정 처리
         * Input    값 : UpdateMonthChargeListForTower(임대구분코드, 해당월, 호실, 에너지타입, 일반시침, 일반사용량, 피크시침, 피크사용량, 심야시침, 심야사용량)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateMonthChargeListForTower : 타워용 요금 수정 처리
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
        public static object[] UpdateMonthChargeListForTower(string strRentCd, string strEnergyMonth, string strFloorNo, string strRoomNo, string strChargeTy, double dblGenVal,
                                                             double dblGenUse, double dblPeakVal, double dblPeakUse, double dblNightVal, double dblNightUse,
                                                             double dblACVal, double dblACUse, double dblHVACVal, double dblHVACUse)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[15];

            objParams[0] = strRentCd;
            objParams[1] = strEnergyMonth;
            objParams[2] = strFloorNo;
            objParams[3] = strRoomNo;
            objParams[4] = strChargeTy;
            objParams[5] = dblGenVal;
            objParams[6] = dblGenUse;
            objParams[7] = dblPeakVal;
            objParams[8] = dblPeakUse;
            objParams[9] = dblNightVal;
            objParams[10] = dblNightUse;
            objParams[11] = dblACVal;
            objParams[12] = dblACUse;
            objParams[13] = dblHVACVal;
            objParams[14] = dblHVACUse;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateDayChargeListForTower : 타워용 일일 요금 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : UpdateDayChargeListForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 타워용 일일 요금 수정 처리
         * Input    값 : UpdateDayChargeListForTower(임대구분코드, 해당일, 호실, 에너지타입, 일반시침, 일반사용량, 피크시침, 피크사용량, 심야시침, 심야사용량)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateDayChargeListForTower : 타워용 일일 요금 수정 처리
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
        public static object[] UpdateDayChargeListForTower(string strRentCd, string strEnergyDay, string strFloorNo, string strRoomNo, string strChargeTy, double dblGenVal,
                                                             double dblGenUse, double dblPeakVal, double dblPeakUse, double dblNightVal, double dblNightUse)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[11];

            objParams[0] = strRentCd;
            objParams[1] = strEnergyDay;
            objParams[2] = strFloorNo;
            objParams[3] = strRoomNo;
            objParams[4] = strChargeTy;
            objParams[5] = dblGenVal;
            objParams[6] = dblGenUse;
            objParams[7] = dblPeakVal;
            objParams[8] = dblPeakUse;
            objParams[9] = dblNightVal;
            objParams[10] = dblNightUse;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_DAYENERGYFORTOWER_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateUtilityFeeForTower : 각종 세금 수납

        /**********************************************************************************************
         * Mehtod   명 : UpdateUtilityFeeForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-04
         * 용       도 : 각종 세금 수납
         * Input    값 : UpdateUtilityFeeFprTower(섹션코드, 요금종류, 해당월, 방번호, 지불수단, 지불금액, 회사코드, 입력사번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateUtilityFeeForTower : 각종 세금 수납
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
        public static DataTable UpdateUtilityFeeForTower(string strRentCd, string strChargeTy, string strEnergyMonth, string strRoomNo, string strPaymentCd, double dblPayedUtilAmt,
                                                         string strUtilCompNo, string strUtilMemNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[8];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = strEnergyMonth;
            objParams[3] = strRoomNo;
            objParams[4] = strPaymentCd;
            objParams[5] = dblPayedUtilAmt;
            objParams[6] = strUtilCompNo;
            objParams[7] = strUtilMemNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdateMonthEnergySeqForTower : 타워용 유틸리티비용 Sequence 할당

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthEnergySeqForTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-06-04
         * 용       도 : 타워용 유틸리티비용 Sequence 할당
         * Input    값 : UpdateMonthEnergySeqForTower(섹션구분코드, 년월정보, 방정보, 요금타입, 유틸순번, 지불일자, 지불일자순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMonthEnergySeqForTower : 타워용 유틸리티비용 Sequence 할당
        /// </summary>
        /// <param name="strRentCd">섹션구분코드</param>
        /// <param name="strEnergyMonth">년월정보</param>
        /// <param name="strRoomNo">방정보</param>
        /// <param name="strChargeTy">요금타입</param>
        /// <param name="intPaySeq">유틸순번</param>
        /// <param name="strPaymentDt">지불일자</param>
        /// <param name="intPaymentSeq">지불일자순번</param>
        /// <returns></returns>
        public static object[] UpdateMonthEnergySeqForTower(string strRentCd, string strEnergyMonth, string strRoomNo, string strChargeTy, int intPaySeq,
                                                            string strPaymentDt, int intPaymentSeq)
        {
            object[] objParams = new object[7];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strEnergyMonth;
            objParams[2] = strRoomNo;
            objParams[3] = strChargeTy;
            objParams[4] = intPaySeq;
            objParams[5] = strPaymentDt;
            objParams[6] = intPaymentSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MONTHENERGYFORTOWER_M02", objParams);

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
            object[] objReturn = new object[2];
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strChargeTy;
            objParams[3] = intChargeSeq;
            objParams[4] = dblGenCharge;
            objParams[5] = dblPeakCharge;
            objParams[6] = dblNightCharge;
            objParams[7] = dblACCharge;
            objParams[8] = dblHVACCharge;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_UTILCHARGEINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteChargeInfo : 요금 삭제처리

        /**********************************************************************************************
         * Mehtod   명 : DeleteChargeInfo
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
        public static DataTable DeleteChargeInfo(string strRentCd, string strChargeTy, int intChargeSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = intChargeSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_DELETE_CHARGEINFO_M00", objParams);

            return dtReturn;
        }

        #endregion

        #region DeleteUtilChargeInfo : 타워용 요금 삭제처리

        /**********************************************************************************************
         * Mehtod   명 : DeleteUtilChargeInfo
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
        public static DataTable DeleteUtilChargeInfo(string strRentCd, string strChargeTy, int intChargeSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = intChargeSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_DELETE_UTILCHARGEINFO_M00", objParams);

            return dtReturn;
        }

        public static DataTable RemoveUtilCommonInfo(string strRentCd, string strChargeTy, int intChargeSeq)
        {
            var objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;
            objParams[2] = intChargeSeq;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_DELETE_UTILCHARGEINFO_M04", objParams);

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
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strChargeTy;
            objParams[3] = intChargeSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_UTILCHARGEINFO_M01", objParams);

            return objReturn;
        }
        //BaoTV
        public static object[] DeleteUtilChargeIndi(string strRentCd, string strRoomNo, string userSeq,string strChargeTy, int intChargeSeq)
        {
            var objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strChargeTy;
            objParams[3] = intChargeSeq;
            objParams[4] = userSeq;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_UTILCHARGEINFO_M05", objParams);

            return objReturn;
        }

        #endregion 
        
        #region DeleteChargeInfo : 요금 전체초기화

        /**********************************************************************************************
         * Mehtod   명 : DeleteChargeInfo
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
        public static DataTable DeleteChargeInfo(string strRentCd, string strChargeTy)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strChargeTy;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_DELETE_CHARGEINFO_M01", objParams);

            return dtReturn;
        }

        #endregion      


        #region RegistryUtilManuallyInfo : Insert and Update MonthUseEnergyTower (BaoTv)

        /**********************************************************************************************
         * Mehtod   명 : InsertUtilFeeManuallyInfo
         * 개   발  자 : Baotv
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 (수도 및 전기세)
         * Input    값 : InsertUtilFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// InsertUtilFeeManuallyInfo : 수동생성 ( 아파트 관리비 )
        /// </summary>
        /// <param name="utilDs"> </param>
        /// <returns></returns>
        public static object[] RegistryUtilManuallyInfo(MngUtilDs.UtilityInfo utilDs)
        {
            var objParams = new object[19];
            objParams[0] = utilDs.RoomNo;
            objParams[1] = utilDs.UserSeq;
            objParams[2] = utilDs.USeq;
            objParams[3] = utilDs.RentCd;
            objParams[4] = utilDs.ChargeTy;
            objParams[5] = utilDs.StartDate;
            objParams[6] = utilDs.EndDate;
            objParams[7] = utilDs.FistIndex;
            objParams[8] = utilDs.EndIndex;
            objParams[9] = utilDs.NormalUsing;
            objParams[10] = utilDs.HightUsing;
            objParams[11] = utilDs.LowUsing;
            objParams[12] = utilDs.NormalOtherUsing;
            objParams[13] = utilDs.PaymentType;
            objParams[14] = utilDs.ExchangRate;
            objParams[15] = utilDs.PayDate;
            objParams[16] = utilDs.Discount;
            objParams[17] = utilDs.DueDate;
            objParams[18] = utilDs.SubDes;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_UTILCHARGEINFO_M05", objParams);

            return objReturn;
        }
        //Over Time - Baotv
        public static object[] RegistryUtilOverManuallyInfo(MngUtilDs.UtilityInfo utilDs)
        {
            var objParams = new object[16];
            objParams[0] = utilDs.RoomNo;
            objParams[1] = utilDs.ChargeSeq;
            objParams[2] = utilDs.RentCd;
            objParams[3] = utilDs.ChargeTy;
            objParams[4] = utilDs.Period;
            objParams[5] = utilDs.Square;
            objParams[6] = utilDs.HoursOver;
            objParams[7] = utilDs.UnitPrice;
            objParams[8] = utilDs.PaymentType;
            objParams[9] = utilDs.ExchangRate;
            objParams[10] = utilDs.PayDate;
            objParams[11] = utilDs.Discount;
            objParams[12] = utilDs.DueDate;
            objParams[13] = utilDs.IncludeVat;
            objParams[14] = utilDs.UserSeq;
            objParams[15] = utilDs.SubDes;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_UTILCHARGEINFO_M08", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteUtilManuallyInfo : Delete MonthUseEnergyTower (BaoTv)

        /**********************************************************************************************
         * Mehtod   명 : DeleteUtilManuallyInfo
         * 개   발  자 : Baotv
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 (수도 및 전기세)
         * Input    값 : InsertUtilFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// InsertUtilFeeManuallyInfo : 수동생성 ( 아파트 관리비 )
        /// </summary>
        /// <param name="strRentCd"> </param>
        /// <param name="uSeq"> </param>
        /// <param name="strRoomNo"> </param>
        /// <returns></returns>
        public static object[] DeleteUtilManuallyInfo(string strRentCd, string uSeq, string strRoomNo)
        {
            var objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = uSeq;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_UTILCHARGEINFO_M02", objParams);

            return objReturn;
        }

        public static object[] DeleteUtilOverManuallyInfo(string strRentCd, string strChargeTy, string strRoomNo, string strChargeSeq)
        {
            var objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strChargeTy;
            objParams[3] = strChargeSeq;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_UTILCHARGEINFO_M03", objParams);

            return objReturn;
        }

        public static object[] DeletePaymentDetails(int seq, string strPSeq)
        {
            var objParams = new object[2];

            objParams[0] = seq;
            objParams[1] = strPSeq;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_PAYMENTINFO_M00", objParams);

            return objReturn;
        }

        public static object[] DeletePaymentAptDetails(int seq, string strPSeq)
        {
            var objParams = new object[2];

            objParams[0] = seq;
            objParams[1] = strPSeq;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_PAYMENTINFO_M01", objParams);

            return objReturn;
        }

        public static object[] DeleteRenovationAptDetails(string strPSeq, int sttCd, string memNo, string memIp, string returnPaymentCd, string returnDt)
        {
            var objParams = new object[6];
            objParams[0] = strPSeq;
            objParams[1] = sttCd;
            objParams[2] = memNo;
            objParams[3] = memIp;
            objParams[4] = returnPaymentCd!=""?returnPaymentCd:"0002";
            objParams[5] = returnDt;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_RENOVATION_M00", objParams);

            return objReturn;
        }

        #endregion



        #region RegistryCashReportManually : Insert and Update Cash Report By Excel (BaoTv)

        /**********************************************************************************************
         * Mehtod   명 : RegistryCashReportManually
         * 개   발  자 : Baotv
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 (수도 및 전기세)
         * Input    값 : InsertUtilFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// RegistryCashReportManually : 수동생성 ( 아파트 관리비 )
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
        public static object[] RegistryCashReportManually(string roomNo,string rentalYear,string rentalMonth,double monthFee,string payDate,double paidFee,double cash,double card,double transfer,string feeType,string rentCd,string paymentCd)
        {
            var objParams = new object[12];

            objParams[0] = rentalYear;
            objParams[1] = rentalMonth;
            objParams[2] = roomNo;
            objParams[3] = monthFee;
            objParams[4] = paidFee;
            objParams[5] = payDate;
            objParams[6] = cash;
            objParams[7] = card;
            objParams[8] = transfer;
            objParams[9] = rentCd;
            objParams[10] = paymentCd;
            objParams[11] = 0.0d;

            var objReturn = SPExecute.ExecReturnNo("KN_EXCEL_UPLOADING_APT_MNGFEE", objParams);

            return objReturn;
        }


        public static object[] DebitListByExcel(string roomNo, double rAmount, string period, double amount, string feeTy, string payDay, string paymentTy, string feetyDt)
        {
            var objParams = new object[8];

            objParams[0] = period;
            objParams[1] = roomNo;
            objParams[2] = feeTy;
            objParams[3] = feetyDt;
            objParams[4] = amount;
            objParams[5] = rAmount;
            objParams[6] = payDay;
            objParams[7] = paymentTy;

            //var objReturn = SPExecute.ExecReturnNo("KN_EXCEL_UPLOADING_APT_BLANCE", objParams);
            var objReturn = SPExecute.ExecReturnNo("KN_EXCEL_UPLOADING_APT_RECEIVABLE", objParams);

            return objReturn;
        }

        public static object[] BalanceTowerByExcel(string ksysCd, string feeTy, string feetyDt, double amount)
        {
            var objParams = new object[4];

            objParams[0] = ksysCd;
            objParams[1] = amount;
            objParams[2] = feeTy;
            objParams[3] = feetyDt;
            var objReturn = SPExecute.ExecReturnNo("KN_EXCEL_UPLOADING_TOWER_BALANCE", objParams);
            return objReturn;
        }

        #endregion

        #region SelectCashReport : 일별 검침 데이터 조회 - BAOTV

        /**********************************************************************************************
         * Mehtod   명 : SelectCashReport
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-31
         * 용       도 : 일별 검침 데이터 조회
         * Input    값 : SelectCashReport()
         * Ouput    값 : DataSet
         **********************************************************************************************/

        /// <summary>
        /// SelectCashReport : 일별 검침 데이터 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy"> </param>
        /// <param name="strYear">연도</param>
        /// <param name="strMonth">월</param>
        /// <param name="strDay"> </param>
        /// <param name="datetype"> </param>
        /// <returns></returns>
        public static DataSet SelectCashReport(string strRentCd, string strFeeTy, string strYear, string strMonth, string strDay, string datetype)
        {
            var objParams = new object[6];

            objParams[0] = strYear;
            objParams[1] = strMonth;
            objParams[2] = strDay;
            objParams[3] = strRentCd;
            objParams[4] = strFeeTy;
            objParams[5] = datetype;

            var dsReturn = SPExecute.ExecReturnMulti("KN_RPT_CASH_REPORT_LIST", objParams);

            return dsReturn;
        }

        #endregion


    }
}