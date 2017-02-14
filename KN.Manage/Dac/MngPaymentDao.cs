using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;

namespace KN.Manage.Dac
{
    public class MngPaymentDao
    {
        #region SelectRentalMngFeeCalendar : 관리비 및 임대료용 달력(년도) 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentalMngFeeCalendar
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-01
         * 용       도 : 관리비 및 임대료용 달력(년도) 조회
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 관리비 및 임대료용 달력(년도) 조회
        /// </summary>
        /// <returns></returns>
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

        /**********************************************************************************************
         * Mehtod   명 : SelectRentalMngFeeCalendar
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-01
         * 용       도 : 관리비 및 임대료용 달력(월) 조회
         * Input    값 : 년도
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 관리비 및 임대료용 달력(월) 조회
        /// </summary>
        /// <param name="strYear">년도</param>
        /// <returns></returns>
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

        /**********************************************************************************************
         * Mehtod   명 : SelectMngPaymentList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-16
         * 용       도 : 월별 수납/미수 현황 리스트 조회
         * Input    값 : intPageSize, intNowPage, strRentCd, strFeeTy, strTenantNm, intFloorNo, strRoomNo, strReceitYn, strLateYn, strRentalYear, strRentalMM
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

        /**********************************************************************************************
         * Mehtod   명 : SelectMngPaymentInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-16
         * 용       도 : 월별 수납/미수 현황 상세보기
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq
         * Ouput    값 : DataTable
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

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelRenovationAndCarCard
         * 개   발  자 : 양영석
         * 생   성  일 : 2013-07-15
         * 용       도 : 정산정보조회
         * Input    값 : SelectExcelRenovationAndCarCard(strFeeTy, strDt, strRoomNo, strName , strEDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
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

        /**********************************************************************************************
         * Mehtod   명 : SelectPaymentInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-17
         * 용       도 : 월별 수납 현황 리스트
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq
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

        /**********************************************************************************************
         * Mehtod   명 : SelectExpenceInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-20
         * 용       도 : 상세 관리비 금액(평당)
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strLangCd 
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


        #region SelectUploadAPTMFList
        /**********************************************************************************************
         * Mehtod   명 : SelectUploadAPTMFList
         * 개   발  자 : PhuongTV
         * 생   성  일 : 2014-06-25
         * 용       도 : 
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/

        public static DataTable SelectUploadAPTMFList(string strRentCd,string strRoomNo, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strYear;
            objParams[3] = strMonth;

            dtReturn = SPExecute.ExecReturnSingle("KN_MN_GET_UPLOAD_MNGFEE_APT_S00", objParams);

            return dtReturn;
        }

        #endregion


        #region SelectManuallyRegistList : 수동생성 대상조회 ( 관리비 )

        /**********************************************************************************************
         * Mehtod   명 : SelectManuallyRegistList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-10
         * 용       도 : 수동생성 대상조회 ( 관리비 )
         * Input    값 : InsertManuallyRegistList(임대구분코드, 해당년, 해당월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectManuallyRegistList : 수동생성 대상조회 ( 관리비 )
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

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_RENTALMNGFEE_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectAptDebitList(string strItemCd, string strTime, string strTenantNm, string strRoomNo, string strIsDebit)
        {
            var objParams = new object[5];

            objParams[0] = strItemCd;
            objParams[1] = strTime;
            objParams[2] = strTenantNm;
            objParams[3] = strRoomNo;
            objParams[4] = strIsDebit;

            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_APT_DEBIT_LIST", objParams);

            return dtReturn;
        }

        //Baotv - Mng Fee making manually
        public static DataTable SelectManuallyMngFeeList(string strRentCd, string strYear, string strMonth, string strDay, string strYearE, string strMonthE, string strDayE, string strRoomNo, string issStartDt, string issEndDt)
        {
            var objParams = new object[10];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;
            objParams[3] = strDay;
            objParams[4] = strYearE;
            objParams[5] = strMonthE;
            objParams[6] = strDayE;
            objParams[7] = strRoomNo;
            objParams[8] = issStartDt;
            objParams[9] = issEndDt;
            //KN_USP_GETS_MNGFEE_LIST_S001
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_GETS_MNGFEE_LIST_S001", objParams);

            return dtReturn;
        }

        //public static DataTable SelectManuallyMngFeeListIssDt(string strRentCd, string strYear, string strMonth, string strDay, string strYearE, string strMonthE, string strDayE, string strRoomNo, string issStartDt, string issEndDt)
        //{
        //    var objParams = new object[10];

        //    objParams[0] = strRentCd;
        //    objParams[1] = strYear;
        //    objParams[2] = strMonth;
        //    objParams[3] = strDay;
        //    objParams[4] = strYearE;
        //    objParams[5] = strMonthE;
        //    objParams[6] = strDayE;
        //    objParams[7] = strRoomNo;
        //    objParams[8] = issStartDt;
        //    objParams[9] = issEndDt;
        //    //KN_USP_GETS_MNGFEE_LIST_S001
        //    var dtReturn = SPExecute.ExecReturnSingle("KN_USP_GETS_MNGFEE_LIST_S001", objParams);

        //    return dtReturn;
        //}

        //Baotv - Rent Fee making manually
        public static DataTable SelectManuallyRentFeeList(string strRentCd, string strYear, string strMonth, string strDay, string strYearE, string strMonthE, string strDayE, string strRoomNo, string issStartDt, string issEndDt)
        {
            var objParams = new object[10];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;
            objParams[3] = strDay;
            objParams[4] = strYearE;
            objParams[5] = strMonthE;
            objParams[6] = strDayE;
            objParams[7] = strRoomNo;
            objParams[8] = issStartDt;
            objParams[9] = issEndDt;
            //KN_USP_GETS_RENTFEE_LIST_S001
            //var dtReturn = SPExecute.ExecReturnSingle("KN_USP_AGT_MAKE_RENTFEE_NOT_DEBIT_LIST_M00", objParams);
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_GETS_RENTFEE_LIST_S001", objParams);

            return dtReturn;
        }

        //Baotv - Mng Fee making manually
        public static DataTable SelectManuallyCreatedDebitList(string strItemCd, string strYear, string strMonth, string strYearE, string stMonthE, string isPrinted,string rentCode)
        {
            var objParams = new object[7];

            objParams[0] = strItemCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;
            objParams[3] = strYearE;
            objParams[4] = stMonthE;
            objParams[5] = isPrinted;
            objParams[6] = rentCode;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_AGT_MAKE_RENTFEE_CREATED_DEBIT_LIST_M00", objParams);

            return dtReturn;
        }
        //Baotv - Mng Fee making manually
        public static DataTable SelectManuallyPrintingList(string rentCode,string roomNo, string strItemCd, string strNm, string StartDt, string EndDt, string printedYN )
        {
            var objParams = new object[7];

            objParams[0] = roomNo;
            objParams[1] = strItemCd;
            objParams[2] = rentCode;
            objParams[3] = strNm;
            objParams[4] = StartDt;
            objParams[5] = EndDt;
            objParams[6] = printedYN;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SELECT_DEBIT_LIST_S00", objParams);

            return dtReturn;
        }

        #region Select hidden printed debit note

        public static DataTable SelectHiddenPrintedDebit(string rentCode, string roomNo, string strItemCd, string strNm, string StartDt, string EndDt, string printedYN)
        {
            var objParams = new object[7];

            objParams[0] = roomNo;
            objParams[1] = strItemCd;
            objParams[2] = rentCode;
            objParams[3] = strNm;
            objParams[4] = StartDt;
            objParams[5] = EndDt;
            objParams[6] = printedYN;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SELECT_HIDDEN_PRINTED_DEBIT_I00", objParams);

            return dtReturn;
        }

        #endregion

        #region Select Special Debit

        public static DataTable SelectSpecialDebitList(string roomNo, string compNm, string feeTy, string rentCd, string StartDt, string EndDt, string printedYN,string refSeq)
        {
            var objParams = new object[8];

            objParams[0] = roomNo;
            objParams[1] = compNm;
            objParams[2] = feeTy;
            objParams[3] = rentCd;
            objParams[4] = StartDt;
            objParams[5] = EndDt;
            objParams[6] = printedYN;
            objParams[7] = refSeq;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SELECT_SPECIAL_DEBIT_I00", objParams);

            return dtReturn;
        }

        public static DataTable SelectSpecialDebitListDetail(string refSeq, string printedYN)
        {
            var objParams = new object[2];
           
            objParams[0] = refSeq;
            objParams[1] = printedYN;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SELECT_SPECIAL_DEBIT_I01", objParams);

            return dtReturn;
        }

        #endregion

        #region Select APTParkingCmpInfo

        public static DataTable SelectAPTParkingCmpInfo(string strRoomNo, string strCompNm, string strCarNo, string strParkingCardNo, string strLangType, string strCarTy)
        {
            var objParams = new object[6];

            objParams[0] = strRoomNo;
            objParams[1] = strCompNm;
            objParams[2] = strCarNo;
            objParams[3] = strParkingCardNo;
            objParams[4] = strLangType;
            objParams[5] = strCarTy;            

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_APTParkingCmpInfo_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectDetailAPTParkingCmpInfo(string strRoomNo, string strParkingTagNo, string strLangType)
        {
            var objParams = new object[3];

            objParams[0] = strRoomNo;
            objParams[1] = strParkingTagNo;
            objParams[2] = strLangType;


            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_SELECT_APTParkingCmpInfo_S01", objParams);

            return dtReturn;
        }
        #endregion


        public static DataTable SelectStatementList(string listType, string rentType, string searchDate, string sendCode)
        {
            var objParams = new object[4];

            objParams[0] = listType;
            objParams[1] = rentType;
            objParams[2] = searchDate;
            objParams[3] = sendCode;

            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_STATEMENT_LIST", objParams);

            return dtReturn;
        }

        public static DataTable SelectCancelCode(string typeCode,string langCode, string commNo)
        {
            var objParams = new object[3];

            objParams[0] = typeCode;
            objParams[1] = langCode;
            objParams[2] = commNo;


            var dtReturn = SPExecute.ExecReturnSingle("KN_COMM_SELECT_COMM_CODE_LIST", objParams);

            return dtReturn;
        }

        public static DataTable SelectPayTypeDdl(string sbLangCd, string typeCode)
        {
            var objParams = new object[2];

            objParams[0] = sbLangCd;
            objParams[1] = typeCode;   

            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_PAYMENT_TYPE", objParams);

            return dtReturn;
        }

        #endregion

        
        #region InsertPaymentInfoList : 월별 수납 현황 등록(납부방법포함)

        /**********************************************************************************************
         * Mehtod   명 : InsertPaymentInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-22
         * 용       도 : 월별 수납 현황 등록(납부방법포함)
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strPayAmt, strPayDt, strPaymentCd
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
        public static DataTable InsertPaymentInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq, string strPayAmt,
                                                      string strPayDt, string strCompNo, string strMemNo, string strMemIP, string strPaymentCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[11];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;
            objParams[5] = double.Parse(strPayAmt);
            objParams[6] = strPayDt;
            objParams[7] = strCompNo;
            objParams[8] = strMemNo;
            objParams[9] = strMemIP;
            objParams[10] = strPaymentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_PAYMENTINFO_S01", objParams);

            return dtReturn;
        }

        //Baotv
        public static DataTable InsertPayment(string strPayCd, int intBankSeq, string strUserSeq, string strRoomNo, string strRentCd, string strFeeTy, string strFeeTyDetails, string strPSeq, int intSeq, string strPayDt,
                                                    string strExRate, string strMoneyCd, double totalAmount, string strMemNo, string strMemIP,string refSeq,string billType)
        {
            var objParams = new object[17];

            objParams[0] = strPayCd;
            objParams[1] = intBankSeq;
            objParams[2] = strUserSeq;
            objParams[3] = strRoomNo;
            objParams[4] = strRentCd;
            objParams[5] = strFeeTy;
            objParams[6] = strFeeTyDetails;
            objParams[7] = strPSeq;
            objParams[8] = intSeq;
            objParams[9] = strPayDt;
            objParams[10] = strExRate;
            objParams[11] = strMoneyCd;
            objParams[12] = totalAmount;
            objParams[13] = strMemNo;
            objParams[14] = strMemIP;
            objParams[15] = refSeq;
            objParams[16] = billType;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_PAYMENTINFO_S02", objParams);

            return dtReturn;
        }

        //Baotv
        public static DataTable InsertPaymentApt(string strPayCd, int intBankSeq, string strUserSeq, string strRoomNo, string strRentCd, string strFeeTy, string strFeeTyDetails, string strPSeq, int intSeq, string strPayDt,
                                                    string strExRate, string strMoneyCd, double totalAmount, string strMemNo, string strMemIP, string refSeq, string paymentTy)
        {
            var objParams = new object[17];

            objParams[0] = strPayCd;
            objParams[1] = intBankSeq;
            objParams[2] = strUserSeq;
            objParams[3] = strRoomNo;
            objParams[4] = strRentCd;
            objParams[5] = strFeeTy;
            objParams[6] = strFeeTyDetails;
            objParams[7] = strPSeq;
            objParams[8] = intSeq;
            objParams[9] = strPayDt;
            objParams[10] = strExRate;
            objParams[11] = strMoneyCd;
            objParams[12] = totalAmount;
            objParams[13] = strMemNo;
            objParams[14] = strMemIP;
            objParams[15] = refSeq;
            objParams[16] = paymentTy;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_PAYMENTINFO_S03", objParams);

            return dtReturn;
        }

        //Baotv
        public static DataTable InsertRevertPaymentApt(string strPSeq, int intSeq, string strPayDt, double totalAmount, string strMemNo, string strMemIP)
        {
            var objParams = new object[6];
            objParams[0] = strPSeq;
            objParams[1] = intSeq;
            objParams[2] = strPayDt;
            objParams[3] = totalAmount;
            objParams[4] = strMemNo;
            objParams[5] = strMemIP;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_PAYMENTINFO_S04", objParams);

            return dtReturn;
        }

        //Baotv
        public static DataTable InsertRenovationInfoApt(string strPayCd, int intBankSeq, string strRoomNo, string strFeeTy, string strPayDt,
                                                    string strExRate, string strMoneyCd, double totalAmount, string strMemNo, string strMemIP,string cardNo)
        {
            var objParams = new object[11];

            objParams[0] = strPayCd;
            objParams[1] = intBankSeq;
            objParams[2] = strRoomNo;
            objParams[3] = strFeeTy;
            objParams[4] = strPayDt;
            objParams[5] = strExRate;
            objParams[6] = strMoneyCd;
            objParams[7] = totalAmount;
            objParams[8] = strMemNo;
            objParams[9] = strMemIP;
            objParams[10] = cardNo;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_RENOVATIONINFO_M00", objParams);

            return dtReturn;
        }

        #endregion 

        #region InsertMngFeeItem : 관리비 및 임대료 항목 추가

        /**********************************************************************************************
         * Mehtod   명 : InsertMngFeeItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 추가
         * Input    값 : strRentCd, strFeeTy, strMngFeeCd, strMngFeeNmEn, strMngFeeNmVi, strMngFeeNmKr, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertMngFeeItem : 관리비 및 임대료 항목 추가
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
        public static DataTable InsertMngFeeItem(string strRentCd, string strFeeTy, string strMngFeeCd, string strMngFeeNmEn, string strMngFeeNmVi, string strMngFeeNmKr,
                                                 string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strMngFeeCd;
            objParams[3] = TextLib.StringEncoder(strMngFeeNmEn);
            objParams[4] = TextLib.StringEncoder(strMngFeeNmVi);
            objParams[5] = TextLib.StringEncoder(strMngFeeNmKr);
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_MNGMENUINFO_M00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertLateFeeRatioList : 연체요율 추가 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertLateFeeRatioList
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
        public static object[] InsertLateFeeRatioList(string strRentCd, string strFeeTy, int intLateFeeStartDay, int intLateFeeEndDay, string strLateFeeStartDt,
                                                       double fltLateFeeRatio, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = intLateFeeStartDay;
            objParams[3] = intLateFeeEndDay;
            objParams[4] = strLateFeeStartDt;
            objParams[5] = fltLateFeeRatio;
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_LATEFEERATIOINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertMonthMngInfo : 월별 수납 항목 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertMonthMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-09
         * 용       도 : 월별 수납 항목 등록
         * Input    값 : strRentCd, strFeeTy, strMngFeeCd, strMngYear, strMngMM, strMngFee, strUseYn, strLimitDt, strInsCompNo, strInsMemNo, strInsMemIp
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMonthMngInfo : 월별 수납 항목 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngFeeCd">요금관리코드</param>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strMngFee">금액</param>
        /// <param name="strUseYn">사용여부</param>
        /// <param name="strLimitDt">납부날짜</param>
        /// <param name="strInsCompNo">기업코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static object[] InsertMonthMngInfo(string strRentCd, string strFeeTy, string strMngFeeCd, string strMngYear, string strMngMM, string strMngFee, string strUseYn, 
                                                   string strLimitDt, string strInsCompNo, string strInsMemNo, string strInsMemIp,string VAT)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[12];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strMngFeeCd;
            objParams[3] = strMngYear;
            objParams[4] = strMngMM;
            objParams[5] = double.Parse(strMngFee);
            objParams[6] = strUseYn;
            objParams[7] = strLimitDt;
            objParams[8] = strInsCompNo;
            objParams[9] = TextLib.StringEncoder(strInsMemNo);
            objParams[10] = strInsMemIp;
            objParams[11] = double.Parse(VAT);

            if (strRentCd.Equals(CommValue.RENTAL_VALUE_APTA) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTB) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APT))
            {
                objParams[0] = CommValue.RENTAL_VALUE_APTA;
                SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MONTHMNGMENUINFO_M00", objParams);

                objParams[0] = CommValue.RENTAL_VALUE_APTB;
                objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MONTHMNGMENUINFO_M00", objParams);
            }
            else if (strRentCd.Equals(CommValue.RENTAL_VALUE_APTASHOP) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTBSHOP) ||
                strRentCd.Equals(CommValue.RENTAL_VALUE_APTSHOP))
            {
                objParams[0] = CommValue.RENTAL_VALUE_APTASHOP;
                objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MONTHMNGMENUINFO_M00", objParams);

                objParams[0] = CommValue.RENTAL_VALUE_APTBSHOP;
                objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MONTHMNGMENUINFO_M00", objParams);
            }
            else
            {
                objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MONTHMNGMENUINFO_M00", objParams);
            }

            return objReturn;
        }

        #endregion

        #region InsertRentalMngReasonInfo : 리테일 및 오피스 임대 임대료 마감 사유 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertRentalMngReasonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-23
         * 용       도 : 리테일 및 오피스 임대 임대료 마감 사유 등록
         * Input    값 : strRentCd, strFeeTy, strUserSeq, strMngYear, strMngMM, strContext, strInsCompNo, strInsMemNo, strInsMemIp
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertRentalMngReasonInfo : 리테일 및 오피스 임대 임대료 마감 사유 등록
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
        public static object[] InsertRentalMngReasonInfo(string strRentCd, string strFeeTy, string strUserSeq, string strMngYear, string strMngMM, 
                                                         string strContext, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strUserSeq;
            objParams[3] = strMngYear;
            objParams[4] = strMngMM;
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strContext));
            objParams[6] = strInsCompNo;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_RENTALMNGREASONINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertInitMngFeeMonthSetInfo: 관리비 청구월 초기세팅

        /**********************************************************************************************
         * Mehtod   명 : InsertInitMngFeeMonthSetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-14
         * 용       도 : 관리비 청구월 초기세팅
         * Input    값 : InsertInitMngFeeMonthSetInfo(섹션코드, 적용년, 적용월, 등록회사코드, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertInitMngFeeMonthSetInfo: 관리비 청구월 초기세팅
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strAppliedYear">적용년</param>
        /// <param name="strAppliedMonth">적용월</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] InsertInitMngFeeMonthSetInfo(string strRentCd, string strAppliedYear, string strAppliedMonth, string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strAppliedYear;
            objParams[2] = strAppliedMonth;
            objParams[3] = strInsCompCd;
            objParams[4] = strInsMemNo;
            objParams[5] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MNGFEEMONTHSETINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertMngFeeMonthSetInfo: 관리비 청구월 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertMngFeeMonthSetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-15
         * 용       도 : 관리비 청구월 초기세팅
         * Input    값 : InsertMngFeeMonthSetInfo(섹션코드, 적용년, 적용월, 등록회사코드, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMngFeeMonthSetInfo: 관리비 청구월 초기세팅
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
        public static object[] InsertMngFeeMonthSetInfo(string strRentCd, int intMonthSeq, string strStartDt, int intDuringMonth, int intRealDuringMonth,
                                                        int intStartMonth, int intRealStartMonth, string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[10];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = intMonthSeq;
            objParams[2] = strStartDt;
            objParams[3] = intDuringMonth;
            objParams[4] = intRealDuringMonth;
            objParams[5] = intStartMonth;
            objParams[6] = intRealStartMonth;
            objParams[7] = strInsCompCd;
            objParams[8] = strInsMemNo;
            objParams[9] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MNGFEEMONTHSETINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region InsertAPTMmgFeeManuallyInfo : 수동생성 ( 아파트 관리비 )

        /**********************************************************************************************
         * Mehtod   명 : InsertAPTMmgFeeManuallyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 ( 아파트 관리비 )
         * Input    값 : InsertAPTMmgFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertAPTMmgFeeManuallyInfo : 수동생성 ( 아파트 관리비 )
        /// </summary>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <returns></returns>
        public static object[] InsertAPTMmgFeeManuallyInfo(string strYear, string strMonth)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strYear;
            objParams[1] = strMonth;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_RENTALMNGFEE_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertAPTRetailMmgFeeManuallyInfo : 수동생성 ( 아파트 상가 관리비 )

        /**********************************************************************************************
         * Mehtod   명 : InsertAPTRetailMmgFeeManuallyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동생성 ( 아파트 상가 관리비 )
         * Input    값 : InsertAPTRetailMmgFeeManuallyInfo(해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertAPTRetailMmgFeeManuallyInfo : 수동생성 ( 아파트 상가 관리비 )
        /// </summary>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <returns></returns>
        public static object[] InsertAPTRetailMmgFeeManuallyInfo(string strYear, string strMonth)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strYear;
            objParams[1] = strMonth;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_RENTALMNGFEE_M01", objParams);

            return objReturn;
        }

        #endregion

        #region InsertAptMFDebitNote : Phuongtv

        public static object[] CreateAptMFDebitNote(string strUserSeq, string strRentCd, string strRoomNo, int intFloor, string tenantNm, double dbTotal, string strPeriod)
        {
            var objParams = new object[7];

            objParams[0] = strUserSeq;
            objParams[1] = strRentCd;
            objParams[2] = strRoomNo;
            objParams[3] = intFloor;            
            objParams[4] = tenantNm;
            objParams[5] = dbTotal;
            objParams[6] = strPeriod;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_INSERT_DEBIT_LIST__APT_I00", objParams);
            return objReturn;
        }

        #endregion

        #region InsertManuallyEveryFeeRegistList : 수동생성 대상조회 ( 관리비 )

        /**********************************************************************************************
         * Mehtod   명 : InsertManuallyEveryFeeRegistList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-10
         * 용       도 : 수동생성 대상조회 ( 관리비 )
         * Input    값 : InsertManuallyEveryFeeRegistList(임대구분코드, 해당년, 해당월)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// SelectManuallyRegistList : 수동생성 대상조회 ( 관리비 )
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strYear">해당년</param>
        /// <param name="strMonth">해당월</param>
        /// <returns></returns>
        public static object[] InsertManuallyEveryFeeRegistList(string strRentCd, string strYear, string strMonth)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strYear;
            objParams[1] = strMonth;

            if (strRentCd.Equals(CommValue.RENTAL_VALUE_APT))
            {
                objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_MNGFEE_APT_LIST_M02", objParams);
            }
            else if (strRentCd.Equals(CommValue.RENTAL_VALUE_OFFICE) ||
                     strRentCd.Equals(CommValue.RENTAL_VALUE_SHOP))
            {
                objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_MNGFEE_RENT_LIST_M03", objParams);
            }

            return objReturn;
        }

        public static object[] CreateDebitNote(string strUserSeq, string strRentCd, int rentSeq, string feeType, string strPaymentDt, int intFloor, string strRoomNo, double strArea, string tenantNm, int intPayCycle, double exChangeRate, double feeAmount, string startUsingDt, string endUsingDt, string strIssuingDt, double unitPrice, double total, int intRentSeq, string memNo, string memIp)
        {
            var objParams = new object[19];

            objParams[0] = strUserSeq;
            objParams[1] = strRentCd;
            objParams[2] = feeType;
            objParams[3] = strPaymentDt;
            objParams[4] = intFloor;
            objParams[5] = strRoomNo;
            objParams[6] = strArea;
            objParams[7] = tenantNm;
            objParams[8] = intPayCycle;
            objParams[9] = exChangeRate;
            objParams[10] = feeAmount;
            objParams[11] = startUsingDt;
            objParams[12] = endUsingDt;
            objParams[13] = strIssuingDt;
            objParams[14] = unitPrice;
            objParams[15] = total;
            objParams[16] = intRentSeq;
            objParams[17] = memNo;
            objParams[18] = memIp;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_INSERT_DEBIT_LIST_I00", objParams);
            return objReturn;
        }
       

        public static object[] CreateDebitNoteNewTeanant(string strRentCd, string contractNo, string rentSeq, string feeType, string strYear, string strMonth, string strDay, string vatRatio, string TenantNm, string dongToDollar, string rentFeePayAmt, string leasingAre, string userSeq, string roomNo)
        {
            var objParams = new object[14];

            objParams[0] = strRentCd;
            objParams[1] = contractNo;
            objParams[2] = int.Parse(rentSeq);
            objParams[3] = feeType;
            objParams[4] = strYear;
            objParams[5] = strMonth;
            objParams[6] = strDay;
            objParams[7] = double.Parse(vatRatio);
            objParams[8] = TenantNm;
            objParams[9] = double.Parse(dongToDollar);
            objParams[10] = double.Parse(rentFeePayAmt);
            objParams[11] = double.Parse(leasingAre);
            objParams[12] = userSeq;
            objParams[13] = roomNo;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_MAKE_RENTFEE_NEWTENANT_DEBIT_LIST_M00", objParams);
            return objReturn;
        }

        public static object[] CancelPrintingList(string rentCd, string userSeq, string feeTy, string strRoomNo, string bundleSeq)
        {
            var objParams = new object[5];

            objParams[0] = rentCd;
            objParams[1] = strRoomNo;
            objParams[2] = userSeq;
            objParams[3] = feeTy;
            objParams[4] = bundleSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_CANCEL_DEBIT_LIST_M00", objParams);

            return objReturn;
        }

        public static object[] CancelSpecialDebit(string feeTy, string bundleSeq)
        {
            var objParams = new object[2];
            
            objParams[0] = feeTy;
            objParams[1] = bundleSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_CANCEL_DEBIT_SPECIAL_M00", objParams);

            return objReturn;
        }
        

        public static object[] CancelCreatedList(string strRentCd, string userSeq, string strFeeTy, string strYear, string strMonth, string strBundleSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = userSeq;
            objParams[2] = strFeeTy;
            objParams[3] = strYear;
            objParams[4] = strMonth;
            objParams[5] = strBundleSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_DELETE_RENTFEE_DEBIT_D00", objParams);

            return objReturn;
        }

        public static object[] MakeMergeIndividualBilling(string userSeq, string strRentCd, string strFeeTy, string debitName, int floorNo, string roomNo, double leasingArea, string tenantNm, int terMonth, double dongToDollar, double monthViAmtNo, double realMonthViAmtNo, string sDate, string Edate, double unPaidAmount, string refUserSeq, string refYear, string refMonth, string strBundleSeq, string strIssuingDate, string strMembNo ,string strPaymentDate)
        {
            var objParams = new object[22];

            objParams[0] = userSeq;
            objParams[1] = strRentCd;
            objParams[2] = strFeeTy;
            objParams[3] = debitName;
            objParams[4] = floorNo;
            objParams[5] = roomNo;
            objParams[6] = leasingArea;
            objParams[7] = tenantNm;
            objParams[8] = terMonth;
            objParams[9] = dongToDollar;
            objParams[10] = monthViAmtNo;
            objParams[11] = realMonthViAmtNo;
            objParams[12] = sDate;
            objParams[13] = Edate;
            objParams[14] = unPaidAmount;
            objParams[15] = refUserSeq;
            objParams[16] = refYear;
            objParams[17] = refMonth;
            objParams[18] = strBundleSeq;
            objParams[19] = strIssuingDate;
            objParams[20] = strMembNo;
            objParams[21] = strPaymentDate;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_INSERT_DEBIT_LIST_I00", objParams);
            return objReturn;
        }        

        #endregion

        #region CancelAPTParkingCmpInfo

        public static object[] CancelAPTParkingCmpInfo(string strRoomNo, string strTagNo, string strCarNo)
        {
            var objParams = new object[3];

            objParams[0] = strRoomNo;
            objParams[1] = strTagNo;
            objParams[2] = strCarNo;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_CANCEL_APTParkingCmpInfo_M00", objParams);

            return objReturn;
        }

        #endregion

        #region Insert Special Debit

        public static object[] MakeSpecialDebit(string strRentCd, string strFeeTy, string strRoomNo, string strTenantNm, string strStartDt, string strEndDt, string strPaymentDt, string strIssDt, double dbmonthViAmtNo, double dbrealMonthViAmtNo, double dbExRate, double dbVatAmt, string strDesVi, string strDesEng, string debitTy, string requestDt, double dbQty, double dbUnitPrice)
        {
            var objParams = new object[18];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRoomNo;
            objParams[3] = strTenantNm;
            objParams[4] = strStartDt;
            objParams[5] = strEndDt;
            objParams[6] = strPaymentDt;
            objParams[7] = strIssDt;
            objParams[8] = dbmonthViAmtNo;
            objParams[9] = dbrealMonthViAmtNo;
            objParams[10] = dbExRate;
            objParams[11] = dbVatAmt;
            objParams[12] = strDesVi;
            objParams[13] = strDesEng;
            objParams[14] = debitTy;
            objParams[15] = requestDt;
            objParams[16] = dbQty;
            objParams[17] = dbUnitPrice;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_INSERT_SPC_DEBIT_LIST_I00", objParams);
            return objReturn;
        }
        #endregion

        #region Insert HoadonInfo for Printout Invoice Merge

        public static DataTable InsertPrintoutInvoiceMerge(string strPaydt, string strPrintdt, double dbInvAmt, string strComNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[6];            

            objParams[0] = strPaydt;
            objParams[1] = strPrintdt;
            objParams[2] = dbInvAmt;
            objParams[3] = strComNo;
            objParams[4] = strInsMemNo;
            objParams[5] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_CREATE_HOADON_MERGE_U00", objParams);
            return dtReturn;
        }
       

        #endregion

        #region Insert APTParkingCmpInfo

        public static object[] InsertAptCmpInfo(string strRoomNo, string ParkingTagNo, string strParkingCardNo, string strParkingCarNo, string strCarTyCd, string strTaxCd, string strCmpNm, string strAddr, string strUserDetAddr, string strComNo, string strInsMemNo, string strInsMemIP)
        {
            var objParams = new object[12];

            objParams[0] = strRoomNo;
            objParams[1] = ParkingTagNo;
            objParams[2] = strParkingCardNo;
            objParams[3] = strParkingCarNo;
            objParams[4] = strCarTyCd;
            objParams[5] = strTaxCd;
            objParams[6] = strCmpNm;
            objParams[7] = strAddr;
            objParams[8] = strUserDetAddr;
            objParams[9] = strComNo;
            objParams[10] = strInsMemNo;
            objParams[11] = strInsMemIP;


            var objReturn = SPExecute.ExecReturnNo("KN_USP_INSERT_APTParkingCmpInfo_I00", objParams);
            return objReturn;
        }

        public static object[] InsertSpecialDebitToHoadonInfo(string strTempDocNo)
        {
            var objParams = new object[1];

            objParams[0] = strTempDocNo;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_INVOICE_SPECIAL_M00", objParams);

            return objReturn;
        }

        #endregion

        #region Update HoadonPrintoutMerge

        public static DataTable UpdatingHoadonPrintMerge(string strRefPrint, string strComNo, string strInsMemNo, string strInsMemIP)
        {
            var dtReturn = new DataTable();
            var objParam = new object[4];

            objParam[0] = strRefPrint;
            objParam[1] = strComNo;
            objParam[2] = strInsMemNo;
            objParam[3] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_HOADON_PRK_MERGE_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region [Transfer Statement]

        public static DataSet TransferStatement(string invoiceNo, string compNo, string rentCode, string feeTy, string paymentCode, string userSeq, string tenantsNm, string description, string roomNo, double netAmount, double vatAmount, double totAmount, string sendType, string paymentDt, string insMemNo, double exchangeRate, string refSeq, string billType, string memIP, string debitAcc, string creditAcc, string vatAcc, string feeTyDt, int seq)
        {
            var objParams = new object[24];

            objParams[0] = TextLib.MakeNullToEmpty(invoiceNo);
            objParams[1] = TextLib.MakeNullToEmpty(compNo);
            objParams[2] = TextLib.MakeNullToEmpty(rentCode);
            objParams[3] = TextLib.MakeNullToEmpty(feeTy);
            objParams[4] = TextLib.MakeNullToEmpty(paymentCode);
            objParams[5] = TextLib.MakeNullToEmpty(userSeq);
            objParams[6] = TextLib.MakeNullToEmpty(tenantsNm);
            objParams[7] = TextLib.MakeNullToEmpty(description);
            objParams[8] = TextLib.MakeNullToEmpty(roomNo);
            objParams[9] = netAmount;
            objParams[10] = vatAmount;
            objParams[11] = totAmount;
            objParams[12] = TextLib.MakeNullToEmpty(sendType);
            objParams[13] = TextLib.MakeNullToEmpty(paymentDt);
            objParams[14] = TextLib.MakeNullToEmpty(insMemNo);
            objParams[15] = exchangeRate;
            objParams[16] = TextLib.MakeNullToEmpty(refSeq);
            objParams[17] = TextLib.MakeNullToEmpty(billType);
            objParams[18] = TextLib.MakeNullToEmpty(memIP);
            objParams[19] = TextLib.MakeNullToEmpty(debitAcc);
            objParams[20] = TextLib.MakeNullToEmpty(creditAcc);
            objParams[21] = TextLib.MakeNullToEmpty(vatAcc);
            objParams[22] = seq;
            objParams[23] = TextLib.MakeNullToEmpty(feeTyDt);


            var objReturn = SPExecute.ExecReturnMulti("KN_KSYSTEM_TRANSFER_DATA", objParams);
            return objReturn;
        }



        public static DataSet TransferStatementApt(string invoiceNO, string compNo, string rentCode, string feeTy, string paymentCode, string userSeq, string tenantsNm, string description, string roomNo, double netAmount, double vatAmount, double totAmount, string sendType, string paymentDt, string insMemNo, double exchangeRate, string refSeq, string billType, string memIP, string debitAcc, string creditAcc, string vatAcc, string feeTyDt, int seq)
        {
            var objParams = new object[24];

            objParams[0] = TextLib.MakeNullToEmpty(invoiceNO);
            objParams[1] = TextLib.MakeNullToEmpty(compNo);
            objParams[2] = TextLib.MakeNullToEmpty(rentCode);
            objParams[3] = TextLib.MakeNullToEmpty(feeTy);
            objParams[4] = TextLib.MakeNullToEmpty(paymentCode);
            objParams[5] = TextLib.MakeNullToEmpty(userSeq);
            objParams[6] = TextLib.MakeNullToEmpty(tenantsNm);
            objParams[7] = TextLib.MakeNullToEmpty(description);
            objParams[8] = TextLib.MakeNullToEmpty(roomNo);
            objParams[9] = netAmount;
            objParams[10] = vatAmount;
            objParams[11] = totAmount;
            objParams[12] = TextLib.MakeNullToEmpty(sendType);
            objParams[13] = TextLib.MakeNullToEmpty(paymentDt);
            objParams[14] = TextLib.MakeNullToEmpty(insMemNo);
            objParams[15] = exchangeRate;
            objParams[16] = TextLib.MakeNullToEmpty(refSeq);
            objParams[17] = TextLib.MakeNullToEmpty(billType);
            objParams[18] = TextLib.MakeNullToEmpty(memIP);
            objParams[19] = TextLib.MakeNullToEmpty(debitAcc);
            objParams[20] = TextLib.MakeNullToEmpty(creditAcc);
            objParams[21] = TextLib.MakeNullToEmpty(vatAcc);
            objParams[22] = seq;
            objParams[23] = TextLib.MakeNullToEmpty(feeTyDt);

            var objReturn = SPExecute.ExecReturnMulti("KN_KSYSTEM_TRANSFER_APT_DATA", objParams);
            return objReturn;
        }

        public static object[] CancelData(string roomNo, string userSeq, string insMemNo, string insIP, string reason, string refSerialNo, string refInvoiceNo, string cancelType, string sendType, string slipNo)
        {
            var objParams = new object[10];

            objParams[0] = TextLib.MakeNullToEmpty(roomNo);
            objParams[1] = TextLib.MakeNullToEmpty(userSeq);
            objParams[2] = TextLib.MakeNullToEmpty(insMemNo);
            objParams[3] = TextLib.MakeNullToEmpty(insIP);
            objParams[4] = TextLib.MakeNullToEmpty(reason);
            objParams[5] = TextLib.MakeNullToEmpty(refSerialNo);
            objParams[6] = TextLib.MakeNullToEmpty(refInvoiceNo);
            objParams[7] = TextLib.MakeNullToEmpty(cancelType);
            objParams[8] = TextLib.MakeNullToEmpty(sendType);
            objParams[9] = TextLib.MakeNullToEmpty(slipNo);

            var objReturn = SPExecute.ExecReturnNo("KN_SCR_INSERT_CANCEL_DATA", objParams);
            return objReturn;
        }

        #endregion

        #region Update Special Debit

        
        public static object[] UpdateSpecialDebit(string strFeeTy , string strPaymentDt, double strDongToDollar, double strMonthViAmtNo, double strRealMonthViAmt, string strStartDt, string strEndDt, string strIssuingDt, string strREF_SEQ, double vatAmt, string strDescVi, string strDescEng,string debitTy, string requestDt, double dbQty, double dbUnitPrice)
        {
            var objParam = new object[16];
           
           
            objParam[0] = TextLib.MakeNullToEmpty(strFeeTy);
            objParam[1] = TextLib.MakeNullToEmpty(strPaymentDt);           
            objParam[2] = strDongToDollar;
            objParam[3] = strMonthViAmtNo;
            objParam[4] = strRealMonthViAmt;           
            objParam[5] = TextLib.MakeNullToEmpty(strStartDt);
            objParam[6] = TextLib.MakeNullToEmpty(strEndDt);            
            objParam[7] = TextLib.MakeNullToEmpty(strIssuingDt);            
            objParam[8] = TextLib.MakeNullToEmpty(strREF_SEQ);
            objParam[9] = vatAmt;
            objParam[10] = TextLib.MakeNullToEmpty(strDescVi);
            objParam[11] = TextLib.MakeNullToEmpty(strDescEng);
            objParam[12] = TextLib.MakeNullToEmpty(debitTy);
            objParam[13] = TextLib.MakeNullToEmpty(requestDt);
            objParam[14] = dbQty;
            objParam[15] = dbUnitPrice;

            var objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_SPECIAL_DEBIT_U00", objParam);

            return objReturn;
        }



        public static DataTable UpdatingNullSpecialDebit(string rentCd)
        {
            var objParam = new object[1];
            objParam[0] = TextLib.MakeNullToEmpty(rentCd);
            var  dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_NULL_SPECIAL_DEBIT_U00", objParam);
            return dtReturn;
        }

        public static DataTable UpdatingPrintBundleNoSpecialDebit(string refSeq, string printBundleNo)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refSeq);
            objParam[1] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_SPECIAL_DEBIT_U00", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingPrintedYNSpecialDebit(string printBundleNo)
        {
            var dtReturn = new DataTable();
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(printBundleNo);


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_PRINTYN_SPECIAL_DEBIT_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region Update APTParkingCmpInfo

        public static object[] UpdateAPTParkingCmpInfo(string strRoomNo, string ParkingTagNo, string strParkingCarNo, string strTaxCd, string strCmpNm, string strAddr, string strUserDetAddr, string strComNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParam = new object[10];


            objParam[0] = TextLib.MakeNullToEmpty(strRoomNo);
            objParam[1] = TextLib.MakeNullToEmpty(ParkingTagNo);
            objParam[2] = TextLib.MakeNullToEmpty(strParkingCarNo);
            objParam[3] = TextLib.MakeNullToEmpty(strTaxCd);
            objParam[4] = TextLib.MakeNullToEmpty(strCmpNm);
            objParam[5] = TextLib.MakeNullToEmpty(strAddr);
            objParam[6] = TextLib.MakeNullToEmpty(strUserDetAddr);
            objParam[7] = TextLib.MakeNullToEmpty(strComNo);
            objParam[8] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParam[9] = TextLib.MakeNullToEmpty(strInsMemIP);

            objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_APTParkingCmpInfo_U00", objParam);

            return objReturn;
        }

        #endregion

        #region UpdatePaymentInfoList : 월별 수납/연체여부 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdatePaymentInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-17
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
        public static DataTable UpdatePaymentInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq, string strReceitCd, string strLateCd, string strLimitDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[8];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;
            objParams[5] = strReceitCd;
            objParams[6] = strLateCd;
            objParams[7] = strLimitDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_PAYMENTINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdatePaymentInfoList : 월별 수납/연체여부 수정(자동)

        /**********************************************************************************************
         * Mehtod   명 : UpdatePaymentInfoList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-17
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
        public static DataTable UpdatePaymentInfoList(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq, string strReceitCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;
            objParams[5] = strReceitCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_PAYMENTINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdatePaymentAmoumt : 월별 수납/연체여부 금액 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdatePaymentAmoumt
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-17
         * 용       도 : 월별 수납/연체여부 금액 수정
         * Input    값 : strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq, strPaySeq, strPayAmt
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
        /// <param name="strPayAmt">금액</param>
        /// <returns></returns>
        public static DataTable UpdatePaymentAmoumt(string strRentCd, string strFeeTy, string strRentalYear, string strRentalMM, string strUserSeq, string strPaySeq, double dblPayAmt, string strMemIP, string strMemNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[9];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;
            objParams[5] = strPaySeq;
            objParams[6] = dblPayAmt;
            objParams[7] = strMemIP;
            objParams[8] = strMemNo;


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_PAYMENTINFO_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdateSettelmentInfo : 과금관리정산수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateSettelmentInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-29
         * 용       도 : 과금관리정산수정
         * Input    값 : strUserSeq, strRentCd, strItemCd, intFloorNo, strRoomNo, dblDongToDollar, strPaymentCd, strPayDt, strFeeVat, strInsMemNo, strInsMemIp, strCarTy, dblPayAmt
         * Ouput    값 : DataTable
         **********************************************************************************************/
        public static DataTable UpdateSettelmentInfo(string strUserSeq, string strRentCd, string strItemCd, string strRentalYear, string strRentalMM, int intPaySeq, double dblPayAmt,
            string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[9];

            objParams[0] = strUserSeq;
            objParams[1] = strRentCd;
            objParams[2] = strItemCd;
            objParams[3] = strRentalYear;
            objParams[4] = strRentalMM;
            objParams[5] = intPaySeq;
            objParams[6] = dblPayAmt;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_ACCOUNTSINFO_M00", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdateCancelSettelmentInfo : 납부금액수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateCancelSettelmentInfo
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
        public static DataTable UpdateCancelSettelmentInfo(string strPayDt, int intPaySeq, string strRentCd, string strItemCd, double dblUniPrime, double dblPayAmt, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[8];

            objParams[0] = strPayDt;
            objParams[1] = intPaySeq;
            objParams[2] = strRentCd;
            objParams[3] = strItemCd;
            objParams[4] = dblUniPrime;
            objParams[5] = dblPayAmt;
            objParams[6] = strInsMemNo;
            objParams[7] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_ACCOUNTSINFO_M01", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdateRentalMngFeeAddon : 관리비 및 임대료 Sequence 할당

        /**********************************************************************************************
         * Mehtod   명 : UpdateRentalMngFeeAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-06
         * 용       도 : 관리비 및 임대료 Sequence 할당
         * Input    값 : UpdateRentalMngFeeAddon(strRentCd, strItemCd, strRentalYear, strRentalMM, strUserSeq, intPaySeq, strPaymentDt, strPaymentSeq)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateRentalMngFeeAddon : 관리비 및 임대료 Sequence 할당
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
        public static object[] UpdateRentalMngFeeAddon(string strRentCd, string strItemCd, string strRentalYear, string strRentalMM, string strUserSeq, int intPaySeq,
                                                    string strPaymentDt, int strPaymentSeq)
        {
            object[] objParams = new object[8];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strItemCd;
            objParams[2] = strRentalYear;
            objParams[3] = strRentalMM;
            objParams[4] = strUserSeq;
            objParams[5] = intPaySeq;
            objParams[6] = strPaymentDt;
            objParams[7] = strPaymentSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_RENTALMNGFEEADDON_S00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateMngFeeItem : 관리비 및 임대료 항목 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateMngFeeItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 수정
         * Input    값 : strRentCd, strFeeTy, strMngFeeCd, strMngFeeNmEn, strMngFeeNmVi, strMngFeeNmKr
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMngFeeItem : 관리비 및 임대료 항목 수정
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngFeeCd">관리비코드</param>
        /// <param name="strMngFeeNmEn">영어관리비항목</param>
        /// <param name="MngFeeNmVi">베트남관리비항목</param>
        /// <param name="MngFeeNmKr">한국어관리비항목</param>
        /// <returns></returns>
        public static object[] UpdateMngFeeItem(string strRentCd, string strFeeTy, string strMngFeeCd, string strMngFeeNmEn, string strMngFeeNmVi, string strMngFeeNmKr)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strMngFeeCd;
            objParams[3] = TextLib.StringEncoder(strMngFeeNmEn);
            objParams[4] = TextLib.StringEncoder(strMngFeeNmVi);
            objParams[5] = TextLib.StringEncoder(strMngFeeNmKr);

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MNGMENUINFO_M00", objParams);

            return objReturn;
        }

        #endregion  

        #region UpdateLateFeeRatioList : 연체요율 수정 처리

        /**********************************************************************************************
         * Mehtod   명 : UpdateLateFeeRatioList
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-10
         * 용       도 : 연체요율 수정 처리
         * Input    값 : strRentCd, strFeeTy, intLateFeeSeq, intLateFeeStartDay, intLateFeeEndDay, strLateFeeStartDt, fltLateFeeRatio, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateLateFeeRatioList : 연체요율 수정 처리
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
        public static DataTable UpdateLateFeeRatioList(string strRentCd, string strFeeTy, int intLateFeeSeq, int intLateFeeStartDay, int intLateFeeEndDay, string strLateFeeStartDt, 
                                                       double fltLateFeeRatio, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[10];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = intLateFeeSeq;
            objParams[3] = intLateFeeStartDay;
            objParams[4] = intLateFeeEndDay;
            objParams[5] = strLateFeeStartDt;
            objParams[6] = fltLateFeeRatio;
            objParams[7] = strInsCompNo;
            objParams[8] = TextLib.StringEncoder(strInsMemNo);
            objParams[9] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_LATEFEERATIOINFO_M00", objParams);

            return dtReturn;
        }

        #endregion   

        #region UpdateMonthMngInfo : 월별 수납 항목 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateMonthMngInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-15
         * 용       도 : 월별 수납 항목 수정
         * Input    값 : strRentCd, strFeeTy, strMngFeeCd, strMngYear, strMngMM, strUseYn, strLimitDt, strInsCompNo, strMngFee, strInsMemNo, strInsMemIp
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 월별 수납항목 수정
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
        public static DataTable UpdateMonthMngInfo(string strRentCd, string strFeeTy, string strMngFeeCd, string strMngYear, string strMngMM, string strMngFee, 
                                                   string strUseYn, string strLimitDt, string strInsCompNo, string strInsMemNo, string strInsMemIp,string VAT)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[12];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strMngFeeCd;
            objParams[3] = strMngYear;
            objParams[4] = strMngMM;
            objParams[5] = double.Parse(strMngFee);
            objParams[6] = strUseYn;
            objParams[7] = strLimitDt;
            objParams[8] = strInsCompNo;
            objParams[9] = TextLib.StringEncoder(strInsMemNo);
            objParams[10] = strInsMemIp;
            objParams[11] = double.Parse(VAT);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_MONTHMNGMENUINFO_M00", objParams);

            return dtReturn;
        }

        #endregion 
        
        #region UpdateMngRefundInfo : 환불금 처리 변경

        /**********************************************************************************************
         * Mehtod   명 : UpdateMngRefundInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-29
         * 용       도 : 환불금 처리 변경
         * Input    값 : UpdateMngRefundInfo(strMngYear, strMngMM, strUserSeq, strInsCompNo, strInsMemNo, strInsMemIp)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMngRefundInfo : 환불금 처리 변경
        /// </summary>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIp">사원아이피</param>
        /// <returns></returns>
        public static object[] UpdateMngRefundInfo(string strMngYear, string strMngMM, string strUserSeq, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strMngYear;
            objParams[1] = strMngMM;
            objParams[2] = strUserSeq;
            objParams[3] = strInsCompNo;
            objParams[4] = TextLib.StringEncoder(strInsMemNo);
            objParams[5] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MNGREFUND_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateMngRefundInfo : 환불금 미처리 변경

        /**********************************************************************************************
         * Mehtod   명 : UpdateMngRefundInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-29
         * 용       도 : 환불금 미처리 변경
         * Input    값 : UpdateMngRefundInfo(strMngYear, strMngMM, strUserSeq)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMngRefundInfo : 환불금 미처리 변경
        /// </summary>
        /// <param name="strMngYear">연도</param>
        /// <param name="strMngMM">월</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <returns></returns>
        public static object[] UpdateMngRefundInfo(string strMngYear, string strMngMM, string strUserSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[3];

            objParams[0] = strMngYear;
            objParams[1] = strMngMM;
            objParams[2] = strUserSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MNGREFUND_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteRentalMngFeeAddon : 관리비 및 임대료 납부 테이블 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRentalMngFeeAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-04
         * 용       도 : 관리비 및 임대료 납부 테이블 삭제
         * Input    값 : DeleteRentalMngFeeAddon(strRentCd, strUserSeq, strItemCd, intFloorNo, strRoomNo, dblDongToDollar, strPaymentCd, strPayDt, strFeeVat, strInsMemNo, strInsMemIp, strCarTy, dblPayAmt
         * Ouput    값 : DataTable
         **********************************************************************************************/
        public static DataTable DeleteRentalMngFeeAddon(string strRentCd, string strUserSeq, string strFeeTy, string strRentalYear, string strRentalMM, int intPaySeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strUserSeq;
            objParams[2] = strFeeTy;
            objParams[3] = strRentalYear;
            objParams[4] = strRentalMM;
            objParams[5] = intPaySeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_DELETE_RENTALMNGFEEADDON_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region DeleteMngFeeItem : 관리비 및 임대료 항목 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMngFeeItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-07
         * 용       도 : 관리비 및 임대료 항목 삭제
         * Input    값 : DeleteMngFeeItem(strRentCd, strFeeTy, strMngFeeCd)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteMngFeeItem : 관리비 및 임대료 항목 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strFeeTy">요금타입</param>
        /// <param name="strMngFeeCd">관리비코드</param>
        /// <returns></returns>
        public static object[] DeleteMngFeeItem(string strRentCd, string strFeeTy, string strMngFeeCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = strMngFeeCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_MNGMENUINFO_M00", objParams);

            return objReturn;
        }

        #endregion    

        #region DeleteLateFeeRatioList : 연체요율 종료처리

        /**********************************************************************************************
         * Mehtod   명 : DeleteLateFeeRatioList
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
        public static object[] DeleteLateFeeRatioList(string strRentCd, string strFeeTy, int intLateFeeSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;
            objParams[2] = intLateFeeSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_LATEFEERATIOINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteLateFeeRatio : 연체요율 전체초기화

        /**********************************************************************************************
         * Mehtod   명 : DeleteLateFeeRatio
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
        public static object[] DeleteLateFeeRatio(string strRentCd, string strFeeTy)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strFeeTy;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_LATEFEERATIOINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteRentalMngReasonInfo : 리테일 및 오피스 임대 임대료 마감 사유 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRentalMngReasonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-23
         * 용       도 : 리테일 및 오피스 임대 임대료 마감 사유 삭제
         * Input    값 : strRentCd, strFeeTy, strUserSeq, strMngYear, strMngMM, strInsCompNo, strInsMemNo, strInsMemIp
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteRentalMngReasonInfo : 리테일 및 오피스 임대 임대료 마감 사유 삭제
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

        #endregion


        #region SelectMngUtilInfo : Get Utility 

        /**********************************************************************************************
         * Mehtod   명 : SelectMngInfo
         * 개   발  자 : BaoTv
         * 생   성  일 : 
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
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strFeeTy;
            objParams[3] = uSeq;
            objParams[4] = isDebit;
            objParams[5] = compNm;
            objParams[6] = period.Replace("-","");

            var dsReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_UTILCHARGEINFO_S07", objParams);

            return dsReturn;
        }

        public static DataTable SelectMngUtilInfoExcel(string strRentCd, string strFeeTy, string strRoomNo, int strChargeSeq, string isDebit, string compNm, string period)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strFeeTy;
            objParams[3] = strChargeSeq;
            objParams[4] = isDebit;
            objParams[5] = compNm;
            objParams[6] = period.Replace("-", "");

            var dsReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_UTILCHARGEINFO_S10", objParams);

            return dsReturn;
        }

        public static DataTable SelectMngUtilInfoOverTime(string strRentCd, string strFeeTy, string strRoomNo, string ouSeq, string isDebit, string compNm, string period)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strFeeTy;
            objParams[3] = ouSeq;
            objParams[4] = isDebit;
            objParams[5] = compNm;
            objParams[6] = period.Replace("-", "");

            var dsReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_UTILCHARGEINFO_S08", objParams);

            return dsReturn;
        }

        #endregion
    }
}