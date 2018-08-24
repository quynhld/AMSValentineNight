using System.Data;

using KN.Inventory.Dac;
using KN.Common.Base.Code;


namespace KN.Inventory.Biz
{
    public class InventoryBiz
    {
        #region WatchRentalMngFeeCalendar : 관리비 및 임대료용 달력(년도) 조회

        public static DataTable WatchRentalMngFeeCalendar(string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InventoryDao.SelectRentalMngFeeCalendar(strRentCd);

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

            dtReturn = InventoryDao.SelectRentalMngFeeCalendar(strRentCd, strYear);

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

            dsReturn = InventoryDao.SelectMngPaymentList(intPageSize, intNowPage, strRentCd, strFeeTy, strTenantNm, intFloorNo, strRoomNo, strReceitYn, strLateYn, strRentalYear, strRentalMM, strLangCd);

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

            dtReturn = InventoryDao.SelectMngMenuinfo(strRentCd, strFeeTy);

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

            dsReturn = InventoryDao.SelectMngPaymentInfo(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

            return dsReturn;
        }
        //Baotv
        public static DataSet ListPaymentInfo(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName,string strEDt,string strPayment)
        {
            var dsReturn = InventoryDao.GetsMngPaymentInfo(strRentCd, strFeeTy, strDt, strRoomNo, strName,strEDt,strPayment);

            return dsReturn;
        }

        //Baotv
        public static DataSet ListPaymentInfoApt(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strPayment)
        {
            var dsReturn = InventoryDao.GetsMngPaymentInfoApt(strRentCd, strFeeTy, strDt, strRoomNo, strName, strEDt, strPayment);

            return dsReturn;
        }

        //Baotv
        public static DataSet ListRenovationInfoApt(string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt)
        {
            var dsReturn = InventoryDao.GetsMngRenovationInfoApt(strFeeTy, strDt, strRoomNo, strName, strEDt);

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

            dsReturn = InventoryDao.SelectExcelRenovationAndCarCard(strFeeTy, strDt, strRoomNo, strName, strEDt);

            return dsReturn;
        }


        #endregion

        //Baotv
        public static DataSet ListPaymentInfoAptForMerge(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strFeeTyDt)
        {
            var dsReturn = InventoryDao.ListPaymentInfoAptForMerge(strRentCd, strFeeTy, strDt, strRoomNo, strName, strEDt, strFeeTyDt);

            return dsReturn;
        }

        #region Export Excel List Merged Invoice

        public static DataSet ListMergedInvoice(string strInvoiceNo)
        {
            var dsReturn = InventoryDao.ListMergedInvoice(strInvoiceNo);

            return dsReturn;
        }

        #endregion

        //Baotv
        public static DataSet ListPaymentInfoAptForMergeExcel(string strRentCd, string strFeeTy, string strDt, string strRoomNo, string strName, string strEDt, string strFeeTyDt)
        {
            var dsReturn = InventoryDao.ListPaymentInfoAptForMergeExcel(strRentCd, strFeeTy, strDt, strRoomNo, strName, strEDt, strFeeTyDt);

            return dsReturn;
        }

        //Baotv
        public static DataSet ListPaymentInfoAptForAdjust(string strRentCd, string strFeeTy,string feeTyDt,string strPeriod, string strRoomNo, string paidDt)
        {
            var dsReturn = InventoryDao.ListPaymentInfoAptForAdjust(strRentCd, strFeeTy, feeTyDt, strPeriod, strRoomNo, paidDt);

            return dsReturn;
        }

        //Baotv
        public static DataSet ListPaymentInfoAptForAdjustExcel(string strRentCd, string strFeeTy, string feeTyDt, string strPeriod, string strRoomNo, string paidDt, string invoiceNo)
        {
            var dsReturn = InventoryDao.ListPaymentInfoAptForAdjustExcel(strRentCd, strFeeTy, feeTyDt, strPeriod, strRoomNo, paidDt,invoiceNo);

            return dsReturn;
        }

        //BaoTv
        public static DataSet ListPaymentDetails(int seq,string strSeq)
        {
            var dsReturn = InventoryDao.ListPaymentDetails(seq,strSeq);

            return dsReturn;
        }

        //BaoTv
        public static DataSet ListPaymentAptDetails(int seq, string strSeq)
        {
            var dsReturn = InventoryDao.ListPaymentAptDetails(seq, strSeq);

            return dsReturn;
        }

        public static DataSet ListReceivableInfo(string strRentCd, string strFeeTy, string strRoomNo)
        {
            var dsReturn = InventoryDao.ListReceivableInfo(strRentCd, strFeeTy, strRoomNo);

            return dsReturn;
        }

        public static DataSet ListReceivableAptInfo(string strRentCd, string strFeeTy, string strRoomNo)
        {
            var dsReturn = InventoryDao.ListReceivableAptInfo(strRentCd, strFeeTy, strRoomNo);

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

            dtReturn = InventoryDao.SelectPaymentInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

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

            dtReturn = InventoryDao.SelectExpenceInfoList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strLangCd);

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

            dtReturn = InventoryDao.SelectIncompleteList(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

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

            dtReturn = InventoryDao.SelectMngMenuItemCheck(strRentCd, strFeeTy, strMngFeeCd);

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

            dsReturn = InventoryDao.SelectLateFeeInfo(strRentCd, strFeeTy, strRentalYear, strRentalMM, strUserSeq);

            return dsReturn;
        }

        #endregion 

        public static object[] insertCategory(string groupID,string groupName,string grTypeID, string grTypeName, string grSubTypeID, string grSubTypeName)
        {
            return InventoryDao.insertCategory(groupID,groupName,grTypeID,grTypeName,grSubTypeID,grSubTypeName);
        }

        public static DataSet selectCategory(int pageSize, int pageNow)
        {
            return InventoryDao.selectCategory(pageSize,pageNow);
        }
        
    }
}