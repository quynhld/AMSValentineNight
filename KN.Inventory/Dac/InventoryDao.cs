using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;

namespace KN.Inventory.Dac
{
    public class InventoryDao
    {
        #region SelectRentalMngFeeCalendar : 관리비 및 임대료용 달력(년도) 조회

        public static DataTable SelectRentalMngFeeCalendar(string strRentCd)
        {
            object[] objParams = new object[1];
            DataTable dtReturn = new DataTable();

            objParams[0] = strRentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_RENTALMNGFEECALENDAR_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRentalMngFeeCalendar : 관리비 및 임대료용 달력(월) 조회

        public static DataTable SelectRentalMngFeeCalendar(string strRentCd, string strYear)
        {
            object[] objParam = new object[2];
            DataTable dtReturn = new DataTable();            

            objParam[0] = strRentCd;
            objParam[1] = strYear;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_RENTALMNGFEECALENDAR_S01", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMngPaymentList : 월별 수납/미수 현황 리스트 조회

        public static DataSet SelectMngPaymentList(int intPageSize, int intNowPage, string strRentCd, string strFeeTy, string strTenantNm, int intFloorNo, string strRoomNo,
                                                   string strReceitYn, string strLateYn, string strRentalYear, string strRentalMM, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[12];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strFeeTy;
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTenantNm));
            objParams[5] = intFloorNo;
            objParams[6] = TextLib.MakeNullToEmpty(strRoomNo);
            objParams[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strReceitYn));
            objParams[8] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strLateYn));
            objParams[9] = strRentalYear;
            objParams[10] = strRentalMM;
            objParams[11] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMngPaymentInfo : 월별 수납/미수 현황 상세보기

        public static DataSet SelectMngPaymentInfo(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_S01", objParams);

            return dsReturn;
        }

        //BaoTv
        public static DataSet GetsMngPaymentInfo(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strPayment)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strDt;
            objParams[3] = strRoomNo;
            objParams[4] = strName;
            objParams[5] = strEDt;
            objParams[6] = strPayment;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_S05", objParams);

            return dsReturn;
        }

        //BaoTv
        public static DataSet GetsMngPaymentInfoApt(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strPayment)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strDt;
            objParams[3] = strRoomNo;
            objParams[4] = strName;
            objParams[5] = strEDt;
            objParams[6] = strPayment;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_APT_S00", objParams);

            return dsReturn;
        }

        public static DataSet GetsMngRenovationInfoApt(string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt)
        {
            var objParams = new object[5];

            objParams[0] = strFeeTy;
            objParams[1] = strDt;
            objParams[2] = strRoomNo;
            objParams[3] = strName;
            objParams[4] = strEDt;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_RENOVATION_APT_S00", objParams);

            return dsReturn;
        }

        #region SelectExcelRenovationAndCarCard

        public static DataSet SelectExcelRenovationAndCarCard(string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt)
        {
            var objParams = new object[5];

            objParams[0] = strFeeTy;
            objParams[1] = strDt;
            objParams[2] = strRoomNo;
            objParams[3] = strName;
            objParams[4] = strEDt;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_EXCEL_SELECT_RENOVATION_APT_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region Export Excel List Merged Invoice

        public static DataSet ListMergedInvoice(string strInvoiceNo)
        {
            var objParams = new object[1];

            objParams[0] = strInvoiceNo;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_APT_MERGE_S00", objParams);

            return dsReturn;
        }

        #endregion

        //BaoTv
        public static DataSet ListPaymentInfoAptForMerge(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strFeeTyDt)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strDt;
            objParams[3] = strRoomNo;
            objParams[4] = strName;
            objParams[5] = strEDt;
            objParams[6] = strFeeTyDt;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_APT_S01", objParams);

            return dsReturn;
        }

        //BaoTv
        public static DataSet ListPaymentInfoAptForMergeExcel(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strFeeTyDt)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strDt;
            objParams[3] = strRoomNo;
            objParams[4] = strName;
            objParams[5] = strEDt;
            objParams[6] = strFeeTyDt;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_APT_S03", objParams);

            return dsReturn;
        }

        //BaoTv
        public static DataSet ListPaymentInfoAptForAdjust(string strRentCd, string strFeeTy, string feeTyDt, string strPeriod, string strRoomNo, string paidDt)
        {
            var objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = feeTyDt;
            objParams[3] = strPeriod;
            objParams[4] = strRoomNo;
            objParams[5] = paidDt;
            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_APT_S02", objParams);

            return dsReturn;
        }

        //BaoTv
        public static DataSet ListPaymentInfoAptForAdjustExcel(string strRentCd, string strFeeTy, string feeTyDt, string strPeriod, string strRoomNo, string paidDt, string invoiceNo)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = feeTyDt;
            objParams[3] = strPeriod;
            objParams[4] = strRoomNo;
            objParams[5] = paidDt;
            objParams[6] = invoiceNo;
            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_APT_S04", objParams);

            return dsReturn;
        }

        public static DataSet ListPaymentDetails(int seq, string strPSeq)
        {
            var objParams = new object[2];

            objParams[0] = seq;
            objParams[1] = strPSeq;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_S06", objParams);

            return dsReturn;
        }

        public static DataSet ListPaymentAptDetails(int seq, string strPSeq)
        {
            var objParams = new object[2];

            objParams[0] = seq;
            objParams[1] = strPSeq;

            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_PAYMENTINFO_S07", objParams);

            return dsReturn;
        }

        public static DataSet ListReceivableInfo(string strRentCd, string strFeeTy, string strRoomNo)
        {
            var objParams = new object[3];

            objParams[0] = strRoomNo;
            objParams[1] = strFeeTy;
            objParams[2] = strRentCd;

            var dsReturn = SPExecute.ExecReturnMulti("KN_SCR_SELECT_RECEIVABLE_BYROOMNO", objParams);

            return dsReturn;
        }

        public static DataSet ListReceivableAptInfo(string strRentCd, string strFeeTy, string strRoomNo)
        {
            var objParams = new object[3];

            objParams[0] = strRoomNo;
            objParams[1] = strFeeTy;
            objParams[2] = strRentCd;

            var dsReturn = SPExecute.ExecReturnMulti("KN_SCR_SELECT_RECEIVABLE_APT_BYROOMNO", objParams);

            return dsReturn;
        }

        #endregion



        #region SelectPaymentInfoList : 월별 수납 현황 리스트


        public static DataTable SelectPaymentInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_PAYMENTINFO_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExpenceInfoList : 상세 관리비 금액(평당)


        public static DataTable SelectExpenceInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_PAYMENTINFO_S03", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectIncompleteList : 미완전납부처리 사유조회

        /**********************************************************************************************
         * Mehtod   명 : SelectIncompleteList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-07
         * 용       도 : 미완전납부처리 사유조회
         * Input    값 : SelectIncompleteList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectIncompleteList : 미완전납부처리 사유조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">비용대상</param>
        /// <param name="strRentalYear">연도</param>
        /// <param name="strRentalMM">월</param>
        /// <param name="strUserSeq">사원번호</param>
        /// <returns></returns>
        public static DataTable SelectIncompleteList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_PAYMENTINFO_S04", objParams);

            return dtReturn;
        }

        #endregion
        
        #region SelectMngMenuinfo : 관리비 및 임대료 항목 관리

        /**********************************************************************************************
         * Mehtod   명 : SelectMngMenuinfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 관리
         * Input    값 : strRentCd, strFeeTy
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMngMenuinfo : 관리비 및 임대료 항목 관리
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <returns></returns>
        public static DataTable SelectMngMenuinfo(string strRentCd, string strFeeTy)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MNGMENUINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMngMenuItemCheck : 관리비 및 임대료 항목 중복 체크

        /**********************************************************************************************
         * Mehtod   명 : SelectMngMenuItemCheck
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 중복 체크
         * Input    값 : 임대구분코드, 항목타입, 항목코드
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMngMenuItemCheck : 관리비 및 임대료 항목 중복 체크
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">항목타입</param>
        /// <param name="strMngFeeCd">관리비 및 임대료코드</param>
        /// <returns></returns>
        public static DataTable SelectMngMenuItemCheck(string strRentCd, string strFeeTy, string strMngFeeCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strMngFeeCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MNGMENUINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectLateFeeRatioList : 연체요율 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectLateFeeRatioList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-10
         * 용       도 : 연체요율 리스트 조회
         * Input    값 : SelectLateFeeRatioList(임대구분코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 연체요율 리스트 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">관리비 및 임대료 요금타입</param>
        /// <returns></returns>
        public static DataTable SelectLateFeeRatioList(string strRentCd, string strFeeTy)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_LATEFEERATIOINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectLateFeeStartDtCheck : 연체요율 적용일 체크

        /**********************************************************************************************
         * Mehtod   명 : SelectLateFeeStartDtCheck
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
        public static DataTable SelectLateFeeStartDtCheck(string strRentCd, string strFeeTy, string strLateFeeStartDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strLateFeeStartDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_LATEFEERATIOINFO_S01", objParams);

            return dtReturn;
        }

        #endregion  

        #region SelectLateFeeList : 연체료 현황 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectLateFeeList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-16
         * 용       도 : 연체료 현황 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strFeeTy, strTenantNm, intFloorNo, strRoomNo, strRentalYear, strRentalMM
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectLateFeeList : 연체료 현황 조회
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
        public static DataSet SelectLateFeeList(int intPageSize, int intNowPage, string strRentCd, string strFeeTy, string strTenantNm, int intFloorNo, string strRoomNo, 
                                                string strRentalYear, string strRentalMM)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[9];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strFeeTy;
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTenantNm));
            objParams[5] = intFloorNo;
            objParams[6] = TextLib.MakeNullToEmpty(strRoomNo);
            objParams[7] = strRentalYear;
            objParams[8] = strRentalMM;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_LATEFEEINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectLateFeeInfo : 연체료 현황 상세보기

        /**********************************************************************************************
         * Mehtod   명 : SelectLateFeeInfo
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
        public static DataSet SelectLateFeeInfo(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_LATEFEEINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectLateFeeInfoList : 월별 연체료 납부 현황 리스트

        /**********************************************************************************************
         * Mehtod   명 : SelectLateFeeInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-17
         * 용       도 : 월별 연체료 납부 현황 리스트
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq
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
        public static DataTable SelectLateFeeInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_LATEFEEINFO_S02", objParams);

            return dtReturn;
        }

        #endregion 

        #region SelectMngInfoList : 월별 수납항목 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMngInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-08
         * 용       도 : 월별 수납항목 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strFeeTy, strYear
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectMngInfoList : 월별 수납항목 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <returns></returns>
        public static DataSet SelectMngInfoList(int intPageSize, int intNowPage, string strRentCd, string strFeeTy, string strYear)
        {
            object[] objParams = new object[5];
            DataSet dsReturn = new DataSet();

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strFeeTy;
            objParams[4] = strYear;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMngInfoList : 월별 수납항목 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMngInfoList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-08
         * 용       도 : 월별 수납항목 조회
         * Input    값 : intPageSize, strRentCd, strFeeTy, strYear
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectMngInfoList : 월별 수납항목 조회
        /// </summary>
        /// <param name="intPageSize">페이지사이즈</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strYear">연도</param>
        /// <returns></returns>
        public static DataSet SelectMngInfoList(int intPageSize, string strRentCd, string strFeety, string strYear)
        {
            object[] objParams = new object[4];
            DataSet dsReturn = new DataSet();

            objParams[0] = intPageSize;
            objParams[1] = strRentCd;
            objParams[2] = strFeety;
            objParams[3] = strYear;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMngInfo : 월별 수납 항목상세보기

        /**********************************************************************************************
         * Mehtod   명 : SelectMngInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-14
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
        public static DataTable SelectMngInfo(string strLangCd, string strRentCd, string strFeeTy, string strMngYear, string strMngMM)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[5];

            objParams[0] = strLangCd;
            objParams[1] = strRentCd;
            objParams[2] = strFeeTy;
            objParams[3] = strMngYear;
            objParams[4] = strMngMM;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMngInfo : 월별 수납 항목상세보기

        /**********************************************************************************************
         * Mehtod   명 : SelectMngInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-14
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
        public static DataSet SelectMngInfo(string strLangCd, string strRentCd, string strFeeTy)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = strRentCd;
            objParams[2] = strFeeTy;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S03", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMngFeeMonthSetInfo: 관리비 청구월 세팅

        /**********************************************************************************************
         * Mehtod   명 : SelectMngFeeMonthSetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-14
         * 용       도 : 관리비 청구월 세팅
         * Input    값 : SelectMngFeeMonthSetInfo(입주자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMngFeeMonthSetInfo: 관리비 청구월 세팅
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strAppliedYear">적용년</param>
        /// <param name="strAppliedMonth">적용월</param>
        /// <returns></returns>
        public static DataTable SelectMngFeeMonthSetInfo(string strRentCd, string strAppliedYear, string strAppliedMonth)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strAppliedYear;
            objParams[2] = strAppliedMonth;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MNGFEEMONTHSETINFO_S00", objParams);

            return dtReturn;
        }

        #endregion


        public static object[] DeleteRentalMngReasonInfo(string strRentCd, string strFeeTy, string strUserSeq, string strMngYear, string strMngMM,
                                                 string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[8];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strUserSeq;
            objParams[3] = strMngYear;
            objParams[4] = strMngMM;
            objParams[5] = strInsCompNo;
            objParams[6] = strInsMemNo;
            objParams[7] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_RENTALMNGREASONINFO_M00", objParams);
            
            return objReturn;
        }

        //quynhld----------------------------------------------
        public static object[] insertCategory(string groupID, string groupName, string grTypeID, string grTypeName, string grSubTypeID, string grSubTypeName)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = groupID;
            objParams[1] = groupName;
            objParams[2] = grTypeID;
            objParams[3] = grTypeName;
            objParams[4] = grSubTypeID;
            objParams[5] = grSubTypeName;

            objReturn = SPExecute.ExecReturnNo("KN_USP_IVN_INSERT_CATEGORY", objParams);
            return objReturn;
        }

        public static DataSet selectCategory(int pageSize, int pageNow)
        {
            DataSet ds = new DataSet();
            object[] objParams = new object[2];
            objParams[0] = pageSize;
            objParams[1] = pageNow;
            ds = SPExecute.ExecReturnMulti("KN_USP_IVN_SELECT_CATEGORY", objParams);
            return ds;
        }
        
    }
}