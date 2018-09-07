using System.Data;

using KN.Inventory.Dac;
using KN.Common.Base.Code;


namespace KN.Inventory.Biz
{
    public class InventoryBiz
    {
 
        
        public static object[] insertItem(object[] parameters)
        {
            return InventoryDao.insertItem(parameters);
        }

        public static void updateItem(object[] parameters)
        {
            InventoryDao.updateItem(parameters);
        }

        public static object[] insertCategory(string groupID,string groupName,string grTypeID, string grTypeName, string grSubTypeID, string grSubTypeName)
        {
            return InventoryDao.insertCategory(groupID,groupName,grTypeID,grTypeName,grSubTypeID,grSubTypeName);
        }

        public static DataSet selectCategory(int pageSize, int pageNow)
        {
            return InventoryDao.selectCategory(pageSize,pageNow);
        }


        public static DataTable selectOneItem(int ivn_ID)
        {
            return InventoryDao.selectOneItem(ivn_ID);
        }

        public static DataSet selectAllItem(int pageSize, int pageNow, string startDate, string endDate)
        {
            return InventoryDao.selectAllItem(pageSize,pageNow,startDate,endDate);
        }

        public static DataSet INV_OUT_SELECT_PAGING_BY_ITEMID(int pageSize, int nowPage, string ivnID)
        {
            return InventoryDao.INV_OUT_SELECT_PAGING_BY_ITEMID(pageSize,nowPage ,ivnID);
        }

        public static DataSet INV_IN_SELECT_PAGING_BY_ITEMID(int pageSize, int nowPage,  string ivnID)
        {
            return InventoryDao.INV_IN_SELECT_PAGING_BY_ITEMID(pageSize, nowPage, ivnID);
        }
<<<<<<< .mine


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

        public static DataSet selectInDetailsCategory(int pageSize, int nowPage)
        {
            return InventoryDao.selectInDetailsCategory(pageSize, nowPage);
        }

        public static DataSet selectInCategory(int pageSize, int nowPage)
        {
            return InventoryDao.selectInCategory(pageSize, nowPage);
        }
||||||| .r36


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
        
=======
>>>>>>> .r55
    }
}