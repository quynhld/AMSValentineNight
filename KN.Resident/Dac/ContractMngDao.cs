using System.Data;
using System.Globalization;
using KN.Common.Base;
using KN.Common.Method.Lib;

using KN.Resident.Ent;

namespace KN.Resident.Dac
{
    public class ContractMngDao
    {
        #region SelectSalesInfo : 아파트 임대계약정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectSalesInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-13
         * 용       도 : 아파트 임대계약정보 조회
         * Input    값 : SelectSalesInfo(페이지크기, 현재페이지번호, 섹션코드, 검색조건코드, 검색어, 저장여부, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/

        /// <summary>
        /// SelectSalesInfo : 임대계약정보 목록조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strKeyCd">검색조건코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strSaveYn">저장여부</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strLessorTy"> </param>
        /// <returns></returns>
        public static DataSet SelectSalesInfo(int intPageSize, int intNowPage, string strRentCd, string strKeyCd, string strKeyWord, string strSaveYn, string strLangCd, string strLessorTy)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[8];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strKeyCd;
            objParams[4] = strKeyWord;
            objParams[5] = strSaveYn;
            objParams[6] = strLangCd;
            objParams[7] = strLessorTy;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_SALESINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectSalesInfoView : 아파트 임대계약정보 상세보기 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectSalesInfoView (상세보기 조회용)
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-13
         * 용       도 : 임대계약정보 상세보기 조회
         * Input    값 : SelectSalesInfoView(언어코드, 임대구분코드, 임대순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectRentInfoView : 임대계약정보 상세보기 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대순번</param>
        /// <returns></returns>
        public static DataSet SelectSalesInfoView(string strLangCd, string strRentCd, int intRentSeq)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = strRentCd;
            objParams[2] = intRentSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_SALESINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectSalesInfoExcelView : 아파트 임대계약정보 엑셀 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectSalesInfoExcelView (상세보기 조회용)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-18
         * 용       도 : 아파트 임대계약정보 엑셀 조회
         * Input    값 : SelectSalesInfoExcelView(섹션코드, 검색조건코드, 검색어, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectSalesInfoExcelView : 아파트 임대계약정보 엑셀 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strKeyCd">검색조건코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectSalesInfoExcelView(string strRentCd, string strKeyCd, string strKeyWord, string strLangCd)
        {
            var objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strKeyCd;
            objParams[2] = strKeyWord;
            objParams[3] = strLangCd;

            var  dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_SALESINFO_S02", objParams);
            //var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_CONTRACT_EXCEL_LIST", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExistSalesInfo : 임대계약 중복 방번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistSalesInfo (임대계약 중복 방번호 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-06
         * 용       도 : 임대계약 중복 방번호 조회
         * Input    값 : SelectExistSalesInfo(층번호, 방번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectExistSalesInfo : 임대계약 중복 방번호 조회
        /// </summary>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataTable SelectExistSalesInfo(int intFloorNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = intFloorNo;
            objParams[1] = strRoomNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_SALESINFO_S03", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRentInfo : 오피스 및 리테일 임대계약정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-19
         * 용       도 : 오피스 및 리테일 임대계약정보 조회
         * Input    값 : SelectRentInfo(페이지크기, 현재페이지번호, 섹션코드, 검색조건코드, 검색어, 저장여부, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectRentInfo : 오피스 및 리테일 임대계약정보 조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strKeyCd">검색조건코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strSaveYn">저장여부</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectRentInfo(int intPageSize, int intNowPage, string strRentCd, string strKeyCd, string strKeyWord, string strSaveYn, string strLangCd)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[7];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strRentCd;
            objParams[3] = strKeyCd;
            objParams[4] = TextLib.StringEncoder(strKeyWord);
            objParams[5] = strSaveYn;
            objParams[6] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_RENTINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectRentInfoExcelView : 오피스 및 리테일 임대계약정보 엑셀 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentInfoExcelView (상세보기 조회용)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-19
         * 용       도 : 오피스 및 리테일 임대계약정보 엑셀 조회
         * Input    값 : SelectRentInfoExcelView(섹션코드, 검색조건코드, 검색어, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRentInfoExcelView : 오피스 및 리테일 임대계약정보 엑셀 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strKeyCd">검색조건코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectRentInfoExcelView(string strRentCd, string strKeyCd, string strKeyWord, string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strKeyCd;
            objParams[2] = strKeyWord;
            //objParams[3] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_CONTRACT_EXCEL_LIST", objParams);
            //dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_RENTINFO_S02", objParams);

            return dtReturn;
        }
        public static DataTable SpreadTenantBalanceExcelView(string feeTy, string feeTyDt, string period, string periodE, string payDt, string payDtE, string roomNo, string userNm,string paidCd)
        {
            var objParams = new object[10];

            objParams[0] = feeTy;
            objParams[1] = feeTyDt;
            objParams[2] = period;
            objParams[3] = periodE;
            objParams[4] = payDt;
            objParams[5] = payDtE;
            objParams[6] = "";
            objParams[7] = roomNo;
            objParams[8] = userNm;
            objParams[9] = paidCd;
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTSINFO_S06", objParams);
            return dtReturn;
        }

        public static DataTable SpreadTenantBalanceInvoiceExcelView(string feeTy, string feeTyDt, string period, string periodE, string payDt, string payDtE, string roomNo, string userNm, string paidCd, string invoiceNo)
        {
            var objParams = new object[11];

            objParams[0] = feeTy;
            objParams[1] = feeTyDt;
            objParams[2] = period;
            objParams[3] = periodE;
            objParams[4] = payDt;
            objParams[5] = payDtE;
            objParams[6] = "";
            objParams[7] = roomNo;
            objParams[8] = userNm;
            objParams[9] = paidCd;
            objParams[10] = invoiceNo;
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_TENANT_BALANCE_PINVOICE_S02", objParams);
            return dtReturn;
        }

        public static DataTable SpreadVatOutputExcelView(string period)
        {
            var objParams = new object[1];

            objParams[0] = period;
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTSINFO_S08", objParams);
            return dtReturn;
        }


        public static DataTable SpreadReceivableFromTenant(string period, string feeTy, string feeTyDt)
        {
            var objParams = new object[3];

            objParams[0] = feeTy;
            objParams[1] = feeTyDt;
            objParams[2] = period;
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTSINFO_S10", objParams);
            return dtReturn;
        }

        public static DataTable SSpreadPrepaidRevenue(string period, string periodE, string paidDt, string paidDtE)
        {
            var objParams = new object[4];

            objParams[0] = paidDt;
            objParams[1] = paidDtE;
            objParams[2] = period;
            objParams[3] = periodE;
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTSINFO_S12", objParams);
            return dtReturn;
        }

        #endregion

        #region SelectRentInfoView : 임대계약정보 상세보기 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentInfoView (상세보기 조회용)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-27
         * 용       도 : 임대계약정보 상세보기 조회
         * Input    값 : SelectRentInfoView(언어코드, 임대구분코드, 임대순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectRentInfoView : 임대계약정보 상세보기 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대순번</param>
        /// <returns></returns>
        public static DataSet SelectRentInfoView(string strLangCd, string strRentCd, int intRentSeq)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = strRentCd;
            objParams[2] = intRentSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_RENTINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectRentInfoDetailView : 임대계약정보 상세보기 수정용 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentInfoDetailView (수정용 조회용)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-29
         * 용       도 : 임대계약정보 상세보기 수정용 조회
         * Input    값 : SelectRentInfoDetailView(언어코드, 임대구분코드, 임대순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectRentInfoDetailView : 임대계약정보 상세보기 수정용 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대순번</param>
        /// <returns></returns>
        public static DataSet SelectRentInfoDetailView(string strLangCd, string strRentCd, string strCompNo, string strMemNo, int intRentSeq)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[5];

            objParams[0] = strLangCd;
            objParams[1] = strRentCd;
            objParams[2] = strCompNo;
            objParams[3] = strMemNo;
            objParams[4] = intRentSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_RENTINFO_S04", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectExistRentInfo : 오피스 및 리테일 임대계약 중복 방번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistRentInfo (오피스 및 리테일 임대계약 중복 방번호 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-22
         * 용       도 : 오피스 및 리테일 임대계약 중복 방번호 조회
         * Input    값 : SelectExistRentInfo(층번호, 방번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectExistRentInfo : 오피스 및 리테일 임대계약 중복 방번호 조회
        /// </summary>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataTable SelectExistRentInfo(int intFloorNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = intFloorNo;
            objParams[1] = strRoomNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_RENTINFO_S03", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectTempRentDepositInfo : 오피스 및 리테일 선수금 임시정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectTempRentDepositInfo (오피스 및 리테일 선수금 임시정보 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 및 리테일 선수금 임시정보 조회
         * Input    값 : SelectTempRentDepositInfo(임시순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectTempRentDepositInfo : 오피스 및 리테일 선수금 임시정보 조회
        /// </summary>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <returns></returns>
        public static DataTable SelectTempRentDepositInfo(int intDepositTmpSeq, string strCompNo, string strMemNo)
        {
            object[] objParams = new object[3];
            DataTable dtReturn = new DataTable();

            objParams[0] = intDepositTmpSeq;
            objParams[1] = strCompNo;
            objParams[2] = strMemNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_TEMPRENTDEPOSITINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRentDepositInfo : 오피스 및 리테일 선수금 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentDepositInfo (오피스 및 리테일 선수금 정보 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-28
         * 용       도 : 오피스 및 리테일 선수금 정보 조회
         * Input    값 : SelectRentDepositInfo(계약코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRentDepositInfo : 오피스 및 리테일 선수금 정보 조회
        /// </summary>
        /// <param name="strContractNo">계약코드</param>
        /// <returns></returns>
        public static DataTable SelectRentDepositInfo(string strContractNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[1];

            objParams[0] = strContractNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_RENTDEPOSITINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectTempRentFeeInfo : 오피스 및 리테일 임대료 임시정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectTempRentFeeInfo (오피스 및 리테일 임대료 임시정보 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 오피스 및 리테일 선수금 임시정보 조회
         * Input    값 : SelectTempRentFeeInfo(임시순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectTempRentFeeInfo : 오피스 및 리테일 임대료 임시정보 조회
        /// </summary>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <returns></returns>
        public static DataTable SelectTempRentFeeInfo(int intRentFeeTmpSeq, string strCompNo, string strMemNo)
        {
            object[] objParams = new object[3];
            DataTable dtReturn = new DataTable();

            objParams[0] = intRentFeeTmpSeq;
            objParams[1] = strCompNo;
            objParams[2] = strMemNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_TEMPRENTFEEINFO_S00", objParams);

            return dtReturn;
        }

        #endregion
        
        #region SelectRentFeeInfo : 오피스 및 리테일 임대료 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentFeeInfo (오피스 및 리테일 임대료 정보 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 및 리테일 임대료 정보 조회
         * Input    값 : SelectRentFeeInfo(계약코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRentFeeInfo : 오피스 및 리테일 임대료 정보 조회
        /// </summary>
        /// <param name="strContractNo">계약코드</param>
        /// <returns></returns>
        public static DataTable SelectRentFeeInfo(string strContractNo)
        {
            object[] objParams = new object[1];
            DataTable dtReturn = new DataTable();

            objParams[0] = strContractNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_RENTFEEINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertSalesInfo : 아파트 계약정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertSalesInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-12
         * 용       도 : 아파트 계약정보 등록
         * Input    값 : InsertSalesInfo(아파트 계약정보 객체)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertSalesInfo : 아파트 계약정보 등록
        /// </summary>
        /// <param name="rsDs">아파트 계약정보 객체</param>
        /// <returns></returns>
        public static DataTable InsertSalesInfo(RentMngDs.SalesInfo rsDs)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[73];

            objParams[0] = rsDs.RentCd;
            objParams[1] = rsDs.PersonalCd;
            objParams[2] = rsDs.RentTy;
            objParams[3] = TextLib.MakeNullToEmpty(rsDs.CompClass);
            objParams[4] = TextLib.MakeNullToEmpty(rsDs.CompNo);
            objParams[5] = rsDs.CompSeq;
            objParams[6] = TextLib.MakeNullToEmpty(rsDs.NoClass);
            objParams[7] = TextLib.MakeNullToEmpty(rsDs.FloorNo);
            objParams[8] = TextLib.MakeNullToEmpty(rsDs.RoomNo);
            objParams[9] = rsDs.DongToDollar;
            objParams[10] = rsDs.RentalFeeVNDNo;
            objParams[11] = rsDs.RentalFeeUSDNo;
            objParams[12] = rsDs.DepositVNDNo;
            objParams[13] = rsDs.DepositUSDNo;
            objParams[14] = rsDs.InitDay;
            objParams[15] = rsDs.InitMMMngDt;
            objParams[16] = rsDs.InitMMMngVNDNo;
            objParams[17] = rsDs.InitMMMngUSDNo;
            objParams[18] = rsDs.MMMngVNDNo;
            objParams[19] = rsDs.MMMngUSDNo;
            objParams[20] = TextLib.MakeNullToEmpty(rsDs.ContDt).Replace("-", "");
            objParams[21] = TextLib.MakeNullToEmpty(rsDs.ContractNm);
            objParams[22] = TextLib.MakeNullToEmpty(rsDs.ContractNo);
            objParams[23] = TextLib.MakeNullToEmpty(rsDs.RssNo);
            objParams[24] = TextLib.MakeNullToEmpty(rsDs.TelFontNo);
            objParams[25] = TextLib.MakeNullToEmpty(rsDs.TelMidNo);
            objParams[26] = TextLib.MakeNullToEmpty(rsDs.TelRearNo);
            objParams[27] = TextLib.MakeNullToEmpty(rsDs.OfficeTelFontNo);
            objParams[28] = TextLib.MakeNullToEmpty(rsDs.OfficeTelMidNo);
            objParams[29] = TextLib.MakeNullToEmpty(rsDs.OfficeTelRearNo);
            objParams[30] = TextLib.MakeNullToEmpty(rsDs.PostCd);
            objParams[31] = TextLib.MakeNullToEmpty(rsDs.Addr);
            objParams[32] = TextLib.MakeNullToEmpty(rsDs.DetailAddr);
            objParams[33] = TextLib.MakeNullToEmpty(rsDs.CustCd);
            objParams[34] = TextLib.MakeNullToEmpty(rsDs.BankAcc);
            objParams[35] = TextLib.MakeNullToEmpty(rsDs.BusinessNo);
            objParams[36] = TextLib.MakeNullToEmpty(rsDs.MobileFrontNo);
            objParams[37] = TextLib.MakeNullToEmpty(rsDs.MobileMidNo);
            objParams[38] = TextLib.MakeNullToEmpty(rsDs.MobileRearNo);
            objParams[39] = TextLib.MakeNullToEmpty(rsDs.EmailID);
            objParams[40] = TextLib.MakeNullToEmpty(rsDs.EmailServer);
            objParams[41] = TextLib.MakeNullToEmpty(rsDs.PlusCondDt).Replace("-", "");
            objParams[42] = TextLib.MakeNullToEmpty(rsDs.IssueDt).Replace("-", "");
            objParams[43] = TextLib.MakeNullToEmpty(rsDs.IssuePlace);
            objParams[44] = TextLib.MakeNullToEmpty(rsDs.ResaleDt).Replace("-", "");
            objParams[45] = rsDs.ResaleAmt;
            objParams[46] = TextLib.MakeNullToEmpty(rsDs.ResaleReason);
            objParams[47] = TextLib.MakeNullToEmpty(rsDs.Descript1);
            objParams[48] = TextLib.MakeNullToEmpty(rsDs.Descript2);
            objParams[49] = TextLib.MakeNullToEmpty(rsDs.TradeNm);
            objParams[50] = TextLib.MakeNullToEmpty(rsDs.Purpose);
            objParams[51] = TextLib.MakeNullToEmpty(rsDs.LastKeyDt).Replace("-", "");
            objParams[52] = rsDs.InsCompNo;
            objParams[53] = rsDs.InsMemNo;
            objParams[54] = rsDs.InsMemIp;
            //objParams[54] = "192.168.1.8";
            objParams[55] = rsDs.InsKNMemNo;
            objParams[56] = rsDs.LeasingArea;
            objParams[57] = rsDs.ConcYn;
            objParams[58] = rsDs.CURNCY_TYPE;
            objParams[59] = rsDs.FIXED_DONGTODOLLAR;
            objParams[60] = rsDs.INFLATION_RATE;
            objParams[61] = rsDs.M_PAYCYCLE_TYPE;
            objParams[62] = rsDs.M_PAYCYCLE;
            objParams[63] = rsDs.M_S_PAY_DATE;
            objParams[64] = rsDs.M_S_USING_DATE;
            objParams[65] = rsDs.M_ISUE_DATE_TYPE;
            objParams[66] = rsDs.M_ISUE_DATE_ADJUST;
            objParams[67] = rsDs.CONTRACT_TYPE;
            objParams[68] = rsDs.SubYn;
            objParams[69] = rsDs.RefContractNo;
            objParams[70] = rsDs.IS_SPECIAL;
            objParams[71] = rsDs.PAYMENT_TYPE;
            objParams[72] = rsDs.REMARK;


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_SALESINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertSalesCoInfo : 아파트 계약정보 공동소유주 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertSalesCoInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-12
         * 용       도 : 아파트 계약정보 공동소유주 등록
         * Input    값 : InsertSalesCoInfo(아파트 계약정보 공동소유주 객체)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// InsertSalesCoInfo : 아파트 계약정보 공동소유주 등록
        /// </summary>
        /// <param name="rsColDs">아파트 계약정보 공동소유주 등록</param>
        /// <returns></returns>
        public static object[] InsertSalesCoInfo(RentMngDs.SalesColInfo rsColDs)
        {
            object[] objParams = new object[9];
            object[] objReturn = new object[2];

            objParams[0] = rsColDs.RentCd;
            objParams[1] = rsColDs.RentSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoOwner));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.Relationship));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoIssueDt).Replace("-", ""));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoIssuePlace));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoRssNo));
            objParams[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoAddr));
            objParams[8] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoDetailAddr));

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_SALESCOINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertSalesCoInfo : 아파트 계약정보 공동소유주 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertSalesCoInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-12
         * 용       도 : 아파트 계약정보 공동소유주 등록
         * Input    값 : InsertSalesCoInfo(아파트 계약정보 공동소유주 객체)
         * Ouput    값 : Bool
         **********************************************************************************************/

        /// <summary>
        /// InsertFitFee : 아파트 계약정보 공동소유주 등록
        /// </summary>
        /// <param name="fee"> </param>
        /// <param name="contractNo"> </param>
        /// <param name="isApplyMn"> </param>
        /// <returns></returns>
        public static object[] InsertMngFee(string[] fee,string contractNo,string isApplyMn)
        {
            var objParams = new object[9];

            objParams[0] = contractNo;
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(fee[0]).Replace("-", ""));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(fee[1]).Replace("-", ""));
            objParams[3] = double.Parse(fee[2]);
            objParams[4] = double.Parse(fee[3]);
            objParams[5] = isApplyMn;
            objParams[6] = fee[4];
            objParams[7] = fee[5];
            objParams[8] = fee[6];

            var objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_MNGFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion


        #region InsertSalesCompInfo : 아파트 계약정보 법인 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertSalesCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-12
         * 용       도 : 아파트 계약정보 법인 등록
         * Input    값 : InsertSalesCompInfo(아파트 계약정보 법인 객체)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// InsertSalesCompInfo : 아파트 계약정보 법인 등록
        /// </summary>
        /// <param name="rsColDs">아파트 계약정보 법인 등록</param>
        /// <returns></returns>
        public static object[] InsertSalesCompInfo(RentMngDs.SalesCompInfo rsComDs)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = rsComDs.RentCd;
            objParams[1] = rsComDs.RentSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsComDs.LegalRep));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsComDs.Position));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsComDs.TaxCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsComDs.IssueOrg));

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_SALESCOMPINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertRentInfo : 리테일 및 오피스 임대 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertRentInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-25
         * 용       도 : 리테일 및 오피스 임대 정보 등록
         * Input    값 : InsertRentInfo(리테일 및 오피스 임대 정보 객체)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertRentInfo : 리테일 및 오피스 임대 정보 등록
        /// </summary>
        /// <param name="riDs">리테일 및 오피스 임대 정보 객체</param>
        /// <returns></returns>
        public static DataTable InsertRentInfo(RentMngDs.RentInfo riDs)
        {
            var objParams = new object[99];

            objParams[0] = TextLib.MakeNullToEmpty(riDs.RentCd);
            objParams[1] = riDs.RentSeq;
            objParams[2] = TextLib.MakeNullToEmpty(riDs.PersonalCd);
            objParams[3] = TextLib.MakeNullToEmpty(riDs.ContStepCd);
            objParams[4] = riDs.DongToDollar;
            objParams[5] = TextLib.MakeNullToEmpty(riDs.ContractNo);
            objParams[6] = riDs.FloorNo;
            objParams[7] = TextLib.MakeNullToEmpty(riDs.RoomNo);
            objParams[8] = TextLib.MakeNullToEmpty(riDs.ExtRoomNo);
            objParams[9] = TextLib.MakeNullToEmpty(riDs.OTLAgreeDt);
            objParams[10] = TextLib.MakeNullToEmpty(riDs.RentAgreeDt);
            objParams[11] = TextLib.MakeNullToEmpty(riDs.RentStartDt);
            objParams[12] = TextLib.MakeNullToEmpty(riDs.RentEndDt);
            objParams[13] = riDs.TermMonth;
            objParams[14] = riDs.FreeRentMonth;
            objParams[15] = riDs.DepositViAmtNo;
            objParams[16] = TextLib.MakeNullToEmpty(riDs.DepositViAmtEn);
            objParams[17] = riDs.DepositEnAmtNo;
            objParams[18] = TextLib.MakeNullToEmpty(riDs.DepositEnAmtEn);
            objParams[19] = TextLib.MakeNullToEmpty(riDs.DepositViAmtUnitCd);
            objParams[20] = TextLib.MakeNullToEmpty(riDs.PayStartYYYYMM);
            objParams[21] = riDs.PayTermMonth;
            objParams[22] = riDs.PayDay;
            objParams[23] = riDs.M_E_USING_DATE;
            objParams[24] = riDs.R_E_USING_DATE;
            objParams[25] = TextLib.MakeNullToEmpty(riDs.TotViAmtUnitCd);
            objParams[26] = riDs.TotEnAmtNo; //
            objParams[27] = TextLib.MakeNullToEmpty(riDs.TotEnAmtEn);
            objParams[28] = riDs.PerMsMonthEnAmtNo; //
            objParams[29] = riDs.PerMsMonthViAmtNo; //
            objParams[30] = TextLib.MakeNullToEmpty(riDs.PerMsMonthViAmtUnitCd);
            objParams[31] = riDs.InitDay; //
            objParams[32] = TextLib.MakeNullToEmpty(riDs.InitMMMngDt);
            objParams[33] = riDs.InitMMMngVNDNo; //
            objParams[34] = riDs.InitMMMngUSDNo; //
            objParams[35] = riDs.MMMngVNDNo;
            objParams[36] = riDs.MMMngUSDNo;
            objParams[37] = riDs.RentAddr;
            objParams[38] = riDs.RentDetAddr;
            objParams[39] = riDs.RentLeasingArea;
            objParams[40] = TextLib.MakeNullToEmpty(riDs.HandOverDt);
            objParams[41] = TextLib.MakeNullToEmpty(riDs.InteriorStartDt);
            objParams[42] = TextLib.MakeNullToEmpty(riDs.InteriorEndDt);
            objParams[43] = riDs.ConsDeposit; // 
            objParams[44] = TextLib.MakeNullToEmpty(riDs.ConsDepositDt);
            objParams[45] = riDs.ConsRefund; //
            objParams[46] = TextLib.MakeNullToEmpty(riDs.ConsRefundDt);
            objParams[47] = TextLib.MakeNullToEmpty(riDs.DifferenceReason);
            objParams[48] = TextLib.MakeNullToEmpty(riDs.LandloadCompNm);
            objParams[49] = TextLib.MakeNullToEmpty(riDs.LandloadRepNm);
            objParams[50] = TextLib.MakeNullToEmpty(riDs.LandloadNm);
            objParams[51] = riDs.LandloadAddr;
            objParams[52] = riDs.LandloadDetAddr;
            objParams[53] = riDs.LandloadTelTyCd;
            objParams[54] = riDs.LandloadTelFrontNo;
            objParams[55] = riDs.LandloadTelRearNo;
            objParams[56] = TextLib.MakeNullToEmpty(riDs.LandloadMobileTyCd);
            objParams[57] = TextLib.MakeNullToEmpty(riDs.LandloadMobileFrontNo);
            objParams[58] = TextLib.MakeNullToEmpty(riDs.LandloadMobileRearNo);
            objParams[59] = TextLib.MakeNullToEmpty(riDs.LandloadFaxTyCd);
            objParams[60] = TextLib.MakeNullToEmpty(riDs.LandloadFaxFrontNo);
            objParams[61] = TextLib.MakeNullToEmpty(riDs.LandloadFaxRearNo);
            objParams[62] = TextLib.MakeNullToEmpty(riDs.EmailID);
            objParams[63] = TextLib.MakeNullToEmpty(riDs.EmailServer);
            objParams[64] = TextLib.MakeNullToEmpty(riDs.LandloadTaxCd);
            objParams[65] = TextLib.MakeNullToEmpty(riDs.LandloadCorpCert);
            objParams[66] = TextLib.MakeNullToEmpty(riDs.LandloadIssuedDt);
            objParams[67] = riDs.PodiumYn;
            objParams[68] = riDs.MinimumRentFee;
            objParams[69] = riDs.ApplyRate;
            objParams[70] = TextLib.MakeNullToEmpty(riDs.Memo);
            objParams[71] = TextLib.MakeNullToEmpty(riDs.InsKNMemNo);
            objParams[72] = TextLib.MakeNullToEmpty(riDs.InsCompNo);
            objParams[73] = TextLib.MakeNullToEmpty(riDs.InsMemNo);
            objParams[74] = TextLib.MakeNullToEmpty(riDs.InsMemIP);
            objParams[75] = TextLib.MakeNullToEmpty(riDs.SaveYn);
            objParams[76] = TextLib.MakeNullToEmpty(riDs.CURNCY_TYPE);
            objParams[77] = riDs.FIXED_DONGTODOLLAR;
            objParams[78] = riDs.INFLATION_RATE;
            objParams[79] = riDs.CONTRACT_TYPE;
            objParams[80] = riDs.PAYMENT_TYPE;
            objParams[81] = riDs.CPI;
            objParams[82] = riDs.R_PAYCYCLE_TYPE;
            objParams[83] = riDs.R_PAYCYCLE;
            objParams[84] = riDs.R_S_PAY_DATE;
            objParams[85] = riDs.M_PAYCYCLE_TYPE;
            objParams[86] = riDs.M_PAYCYCLE;
            objParams[87] = riDs.M_S_PAY_DATE;
            objParams[88] = riDs.M_S_USING_DATE;
            objParams[89] = riDs.R_S_USING_DATE;
            objParams[90] = riDs.R_ISUE_DATE_TYPE;
            objParams[91] = riDs.M_ISUE_DATE_TYPE;
            objParams[92] = riDs.R_ISUE_DATE_ADJUST;
            objParams[93] = riDs.M_ISUE_DATE_ADJUST;
            objParams[94] = riDs.IS_SPECIAL;
            objParams[95] = riDs.REMARK;
            objParams[96] = riDs.IndustryCd;
            objParams[97] = riDs.NatCd;
            objParams[98] = riDs.RenewDt;


            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_RENTINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertRentDepositInfo : 리테일 및 오피스 임대 보증금 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-25
         * 용       도 : 리테일 및 오피스 임대 보증금 정보 등록
         * Input    값 : InsertRentDepositInfo(계약번호, 임시순번, 회사번호, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertRentDepositInfo : 리테일 및 오피스 임대 보증금 정보 등록
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <param name="strInsCompNo">회사번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static DataTable InsertRentDepositInfo(string strContractNo, int intDepositTmpSeq, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[5];
            DataTable dtReturn = new DataTable();

            objParams[0] = strContractNo;
            objParams[1] = intDepositTmpSeq;
            objParams[2] = strInsCompNo;
            objParams[3] = strInsMemNo;
            objParams[4] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_RENTDEPOSITINFO_M00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertRentFeeInfo : 리테일 및 오피스 임대료 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 리테일 및 오피스 임대료 정보 등록
         * Input    값 : InsertRentFeeInfo(계약번호, 임시순번, 회사번호, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/

        /// <summary>
        /// InsertRentFeeInfo : 리테일 및 오피스 임대 임대료 정보 등록
        /// </summary>
        /// <param name="rentFee"> </param>
        /// <returns></returns>
        public static DataTable InsertRentFeeInfo(string[] rentFee)
        {
            var objParams = new object[8];

            objParams[0] = rentFee[0];
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rentFee[1]).Replace("-", ""));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rentFee[2]).Replace("-", "")); 
            objParams[3] = double.Parse(rentFee[3]);
            objParams[4] = double.Parse(rentFee[4]);
            objParams[5] = rentFee[5];
            objParams[6] = rentFee[6];
            objParams[7] = rentFee[7];

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_RENTFEEINFO_M00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertTempRentDepositInfo : 오피스 / 리테일 보증금 임시 저장

        /**********************************************************************************************
         * Mehtod   명 : InsertTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 / 리테일 보증금 임시 저장
         * Input    값 : InsertTempRentDepositInfo(섹션코드, 임시순번, 상세순번, 보증금예상납부일, 보증금, 적용환율, 보증금수납일, 보증금수납액)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempRentDepositInfo : 오피스 / 리테일 보증금 임시 저장
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <param name="strDepositExpDt">보증금예상납부일</param>
        /// <param name="dblDepositExpAmt">보증금</param>
        /// <param name="dblDepositExcRate">적용환율</param>
        /// <param name="strDepositPayDt">보증금수납일</param>
        /// <param name="dblDepositPayAmt">보증금수납액</param>
        /// <returns></returns>
        public static DataTable InsertTempRentDepositInfo(string strRentCd, int intDepositTmpSeq, int intDepositSeq, string strCompNo, string strMemNo, string strDepositExpDt, double dblDepositExpAmt, double dblDepositExcRate, string strDepositPayDt, double dblDepositPayAmt)
        {
            object[] objParams = new object[10];
            DataTable dtReturn = new DataTable();

            objParams[0] = strRentCd;
            objParams[1] = intDepositTmpSeq;
            objParams[2] = intDepositSeq;
            objParams[3] = strCompNo;
            objParams[4] = strMemNo;
            objParams[5] = strDepositExpDt;
            objParams[6] = dblDepositExpAmt;
            objParams[7] = dblDepositExcRate;
            objParams[8] = strDepositPayDt;
            objParams[9] = dblDepositPayAmt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_TEMPRENTDEPOSITINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertBackUpRentInfo : 임대정보 백업

        /**********************************************************************************************
         * Mehtod   명 : InsertBackUpRentInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-05
         * 용       도 : 임대정보 백업
         * Input    값 : InsertBackUpRentInfo(임대구분코드, 임대순번, 삭제사유, 삭제회사코드, 삭제사원번호, 삭제IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertBackUpRentInfo : 임대정보 백업
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대순번</param>
        /// <param name="strDelReson">삭제사유</param>
        /// <param name="strDelCompNo">삭제회사코드</param>
        /// <param name="strDelMemNo">삭제사원번호</param>
        /// <param name="strDelIP">삭제IP</param>
        /// <returns></returns>
        public static DataTable InsertBackUpRentInfo(string strRentCd, int intRentSeq, string strDelReson, string strDelCompNo, string strDelMemNo, string strDelIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = intRentSeq;
            objParams[2] = TextLib.StringEncoder(strDelReson);
            objParams[3] = strDelCompNo;
            objParams[4] = strDelMemNo;
            objParams[5] = strDelIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_BACKUPRENTINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertBackUpRentDepositInfo : 임대정보 보증금 자료 백업

        /**********************************************************************************************
         * Mehtod   명 : InsertBackUpRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-05
         * 용       도 : 임대정보 보증금 자료 백업
         * Input    값 : InsertBackUpRentDepositInfo(임대정보백업순번, 임대계약코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertBackUpRentDepositInfo : 임대정보 보증금 자료 백업
        /// </summary>
        /// <param name="intRentBackupSeq">임대정보백업순번</param>
        /// <param name="strContractNo">임대구분코드</param>
        /// <returns></returns>
        public static object[] InsertBackUpRentDepositInfo(int intRentBackupSeq, string ContractNo)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intRentBackupSeq;
            objParams[1] = ContractNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_BACKUPRENTDEPOSITINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertBackUpRentFeeInfo : 임대정보 임대료 자료 백업

        /**********************************************************************************************
         * Mehtod   명 : InsertBackUpRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 임대정보 임대료 자료 백업
         * Input    값 : InsertBackUpRentFeeInfo(임대정보백업순번, 임대계약코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertBackUpRentFeeInfo : 임대정보 보증금 자료 백업
        /// </summary>
        /// <param name="intRentBackupSeq">임대정보백업순번</param>
        /// <param name="strContractNo">임대구분코드</param>
        /// <returns></returns>
        public static object[] InsertBackUpRentFeeInfo(int intRentBackupSeq, string ContractNo)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intRentBackupSeq;
            objParams[1] = ContractNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_BACKUPRENTFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertTempRentFeeInfo : 오피스 / 리테일 임대료 임시 저장

        /**********************************************************************************************
         * Mehtod   명 : InsertTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 오피스 / 리테일 임대료 임시 저장
         * Input    값 : InsertTempRentFeeInfo(섹션코드, 임시순번, 상세순번, 임대료적용시작일, 임대료적용종료일, 적용환율, 임대료청구액)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempRentFeeInfo : 오피스 / 리테일 보증금 임시 저장
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <param name="intRentFeeSeq">상세순번</param>
        /// <param name="strRentFeeStartDt">임대료적용시작일</param>
        /// <param name="strRentFeeEndDt">임대료적용종료일</param>
        /// <param name="dblRentFeeExcRate">적용환율</param>
        /// <param name="dblRentFeePayAmt">임대료청구액</param>
        /// <returns></returns>
        public static DataTable InsertTempRentFeeInfo(string strRentCd, int intRentFeeTmpSeq, int intRentFeeSeq, string strCompNo, string strMemNo, string strRentFeeStartDt, string strRentFeeEndDt, double dblRentFeeExcRate, double dblRentFeePayAmt)
        {
            object[] objParams = new object[9];
            DataTable dtReturn = new DataTable();

            objParams[0] = strRentCd;
            objParams[1] = intRentFeeTmpSeq;
            objParams[2] = intRentFeeSeq;
            objParams[3] = strCompNo;
            objParams[4] = strMemNo;
            objParams[5] = strRentFeeStartDt;
            objParams[6] = strRentFeeEndDt;
            objParams[7] = dblRentFeeExcRate;
            objParams[8] = dblRentFeePayAmt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_TEMPRENTFEEINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertMakeAPTMngFeeListInfo : 아파트 관리비 대상자 수동 생성

        /**********************************************************************************************
         * Mehtod   명 : InsertMakeAPTMngFeeListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-15
         * 용       도 : 아파트 관리비 대상자 수동 생성
         * Input    값 : InsertMakeAPTMngFeeListInfo(관리비 대상자 수동 생성)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMakeAPTMngFeeListInfo : 아파트 관리비 대상자 수동 생성
        /// </summary>
        /// <returns></returns>
        public static object[] InsertMakeAPTMngFeeListInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_MNGFEE_APT_LIST_M00");

            return objReturn;
        }

        #endregion

        #region InsertMakeAPTRMngFeeListInfo : 아파트 리테일 관리비 대상자 수동 생성

        /**********************************************************************************************
         * Mehtod   명 : InsertMakeAPTRMngFeeListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-15
         * 용       도 : 아파트 리테일 관리비 대상자 수동 생성
         * Input    값 : InsertMakeAPTRMngFeeListInfo(관리비 대상자 수동 생성)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMakeAPTRMngFeeListInfo : 아파트 리테일 관리비 대상자 수동 생성
        /// </summary>
        /// <returns></returns>
        public static object[] InsertMakeAPTRMngFeeListInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_MNGFEE_APTR_LIST_M00");

            return objReturn;
        }

        #endregion

        #region InsertMakeOfficeRetailMngFeeListInfo : 관리비 대상자 수동 생성

        /**********************************************************************************************
         * Mehtod   명 : InsertMakeOfficeRetailMngFeeListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-15
         * 용       도 : 관리비 대상자 수동 생성
         * Input    값 : InsertMakeOfficeRetailMngFeeListInfo(관리비 대상자 수동 생성)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMakeOfficeRetailMngFeeListInfo : 관리비 대상자 수동 생성
        /// </summary>
        /// <returns></returns>
        public static object[] InsertMakeOfficeRetailMngFeeListInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_MNGFEE_RENT_LIST_M00");

            if (objReturn != null)
            {
                objReturn = SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_MNGFEE_RENT_LIST_M01");

                if (objReturn != null)
                {
                    SPExecute.ExecReturnNo("KN_USP_AGT_MAKE_RENTFEE_RENT_LIST_M00");
                }
            }

            return objReturn;
        }

        #endregion

        #region UpdateSalesInfo : 아파트 계약정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateSalesInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-29
         * 용       도 : 아파트 계약정보 수정
         * Input    값 : UpdateSalesInfo(아파트 계약정보 객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateSalesInfo : 아파트 계약정보 수정
        /// </summary>
        /// <param name="rsDs">아파트 계약정보 객체</param>
        /// <returns></returns>
        public static object[] UpdateSalesInfo(RentMngDs.SalesInfo rsDs)
        {
            var objParams = new object[70];

            objParams[0] = rsDs.RentCd;
            objParams[1] = rsDs.RentSeq;
            objParams[2] = rsDs.PersonalCd;
            objParams[3] = rsDs.RentTy;
            objParams[4] = TextLib.MakeNullToEmpty(rsDs.FloorNo);
            objParams[5] = TextLib.MakeNullToEmpty(rsDs.RoomNo);
            objParams[6] = rsDs.DongToDollar;
            objParams[7] = rsDs.RentalFeeVNDNo;
            objParams[8] = rsDs.RentalFeeUSDNo;
            objParams[9] = rsDs.DepositVNDNo;
            objParams[10] = rsDs.DepositUSDNo;
            objParams[11] = rsDs.InitDay;
            objParams[12] = rsDs.InitMMMngDt;
            objParams[13] = rsDs.InitMMMngVNDNo;
            objParams[14] = rsDs.InitMMMngUSDNo;
            objParams[15] = rsDs.MMMngVNDNo;
            objParams[16] = rsDs.MMMngUSDNo;
            objParams[17] = TextLib.MakeNullToEmpty(rsDs.ContDt).Replace("-", "");
            objParams[18] = TextLib.MakeNullToEmpty(rsDs.ContractNm);
            objParams[19] = TextLib.MakeNullToEmpty(rsDs.ContractNo);
            objParams[20] = TextLib.MakeNullToEmpty(rsDs.RssNo);
            objParams[21] = TextLib.MakeNullToEmpty(rsDs.TelFontNo);
            objParams[22] = TextLib.MakeNullToEmpty(rsDs.TelMidNo);
            objParams[23] = TextLib.MakeNullToEmpty(rsDs.TelRearNo);
            objParams[24] = TextLib.MakeNullToEmpty(rsDs.OfficeTelFontNo);
            objParams[25] = TextLib.MakeNullToEmpty(rsDs.OfficeTelMidNo);
            objParams[26] = TextLib.MakeNullToEmpty(rsDs.OfficeTelRearNo);
            objParams[27] = TextLib.MakeNullToEmpty(rsDs.PostCd);
            objParams[28] = TextLib.MakeNullToEmpty(rsDs.Addr);
            objParams[29] = TextLib.MakeNullToEmpty(rsDs.DetailAddr);
            objParams[30] = TextLib.MakeNullToEmpty(rsDs.CustCd);
            objParams[31] = TextLib.MakeNullToEmpty(rsDs.BankAcc);
            objParams[32] = TextLib.MakeNullToEmpty(rsDs.BusinessNo);
            objParams[33] = TextLib.MakeNullToEmpty(rsDs.MobileFrontNo);
            objParams[34] = TextLib.MakeNullToEmpty(rsDs.MobileMidNo);
            objParams[35] = TextLib.MakeNullToEmpty(rsDs.MobileRearNo);
            objParams[36] = TextLib.MakeNullToEmpty(rsDs.EmailID);
            objParams[37] = TextLib.MakeNullToEmpty(rsDs.EmailServer);
            objParams[38] = TextLib.MakeNullToEmpty(rsDs.PlusCondDt).Replace("-", "");
            objParams[39] = TextLib.MakeNullToEmpty(rsDs.IssueDt).Replace("-", "");
            objParams[40] = TextLib.MakeNullToEmpty(rsDs.IssuePlace);
            objParams[41] = TextLib.MakeNullToEmpty(rsDs.ResaleDt).Replace("-", "");
            objParams[42] = rsDs.ResaleAmt;
            objParams[43] = TextLib.MakeNullToEmpty(rsDs.ResaleReason);
            objParams[44] = TextLib.MakeNullToEmpty(rsDs.Descript1);
            objParams[45] = TextLib.MakeNullToEmpty(rsDs.Descript2);
            objParams[46] = TextLib.MakeNullToEmpty(rsDs.TradeNm);
            objParams[47] = TextLib.MakeNullToEmpty(rsDs.Purpose);
            objParams[48] = TextLib.MakeNullToEmpty(rsDs.LastKeyDt).Replace("-", "");
            objParams[49] = rsDs.InsCompNo;
            objParams[50] = rsDs.InsMemNo;
            objParams[51] = rsDs.InsMemIp;
            objParams[52] = rsDs.InsKNMemNo;
            objParams[53] = rsDs.LeasingArea;
            objParams[54] = rsDs.ConcYn;
            objParams[55] = rsDs.CURNCY_TYPE;
            objParams[56] = rsDs.FIXED_DONGTODOLLAR;
            objParams[57] = rsDs.INFLATION_RATE;
            objParams[58] = rsDs.M_PAYCYCLE_TYPE;
            objParams[59] = rsDs.M_PAYCYCLE;
            objParams[60] = rsDs.M_S_PAY_DATE;
            objParams[61] = rsDs.M_S_USING_DATE;
            objParams[62] = rsDs.M_ISUE_DATE_TYPE;
            objParams[63] = rsDs.M_ISUE_DATE_ADJUST;
            objParams[64] = rsDs.CONTRACT_TYPE;
            objParams[65] = rsDs.SubYn;
            objParams[66] = rsDs.RefContractNo;
            objParams[67] = rsDs.IS_SPECIAL;
            objParams[68] = rsDs.PAYMENT_TYPE;
            objParams[69] = rsDs.REMARK;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_SALESINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateSalesCoInfo : 아파트 계약정보 공동소유주 등록

        /**********************************************************************************************
         * Mehtod   명 : UpdateSalesCoInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-12
         * 용       도 : 아파트 계약정보 공동소유주 등록
         * Input    값 : UpdateSalesCoInfo(아파트 계약정보 공동소유주 객체)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// UpdateSalesCoInfo : 아파트 계약정보 공동소유주 등록
        /// </summary>
        /// <param name="rsColDs">아파트 계약정보 공동소유주 등록</param>
        /// <returns></returns>
        public static object[] UpdateSalesCoInfo(RentMngDs.SalesColInfo rsColDs)
        {
            object[] objParams = new object[9];
            object[] objReturn = new object[2];

            objParams[0] = rsColDs.RentCd;
            objParams[1] = rsColDs.RentSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoOwner));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.Relationship));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoIssueDt).Replace("-", ""));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoIssuePlace));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoRssNo));
            objParams[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoAddr));
            objParams[8] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsColDs.CoDetailAddr));

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_SALESCOINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateSalesCompInfo : 아파트 계약정보 법인 등록

        /**********************************************************************************************
         * Mehtod   명 : UpdateSalesCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-12
         * 용       도 : 아파트 계약정보 법인 등록
         * Input    값 : UpdateSalesCompInfo(아파트 계약정보 법인 객체)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// UpdateSalesCompInfo : 아파트 계약정보 법인 등록
        /// </summary>
        /// <param name="rsColDs">아파트 계약정보 법인 등록</param>
        /// <returns></returns>
        public static object[] UpdateSalesCompInfo(RentMngDs.SalesCompInfo rsComDs)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = rsComDs.RentCd;
            objParams[1] = rsComDs.RentSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsComDs.LegalRep));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsComDs.Position));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsComDs.TaxCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(rsComDs.IssueOrg));

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_SALESCOMPINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateTempRentDepositInfo : 오피스 / 리테일 보증금 임시 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 / 리테일 보증금 임시 수정
         * Input    값 : UpdateTempRentDepositInfo(섹션코드, 임시순번, 상세순번, 보증금예상납부일, 보증금, 적용환율, 보증금수납일, 보증금수납액)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateTempRentDepositInfo : 오피스 / 리테일 보증금 임시 수정
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <param name="strDepositExpDt">보증금예상납부일</param>
        /// <param name="dblDepositExpAmt">보증금</param>
        /// <param name="dblDepositExcRate">적용환율</param>
        /// <param name="strDepositPayDt">보증금수납일</param>
        /// <param name="dblDepositPayAmt">보증금수납액</param>
        /// <returns></returns>
        public static object[] UpdateTempRentDepositInfo(string strRentCd, int intDepositTmpSeq, int intDepositSeq, string strCompNo, string strMemNo, string strDepositExpDt, double dblDepositExpAmt, double dblDepositExcRate, string strDepositPayDt, double dblDepositPayAmt)
        {
            object[] objParams = new object[10];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = intDepositTmpSeq;
            objParams[2] = intDepositSeq;
            objParams[3] = strCompNo;
            objParams[4] = strMemNo;
            objParams[5] = strDepositExpDt;
            objParams[6] = dblDepositExpAmt;
            objParams[7] = dblDepositExcRate;
            objParams[8] = strDepositPayDt;
            objParams[9] = dblDepositPayAmt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_TEMPRENTDEPOSITINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateTempRentFeeInfo : 오피스 / 리테일 임대료 임시 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 오피스 / 리테일 임대료 임시 수정
         * Input    값 : UpdateTempRentFeeInfo(섹션코드, 임시순번, 상세순번, 임대료적용시작일, 임대료적용종료일, 임대료적용환율, 임대료적용달러)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateTempRentFeeInfo : 오피스 / 리테일 임대료 임시 수정
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <param name="intRentFeeSeq">상세순번</param>
        /// <param name="strRentFeeStartDt">임대료적용시작일</param>
        /// <param name="strRentFeeEndDt">임대료적용종료일</param>
        /// <param name="dblRentFeeExcRate">임대료적용환율</param>
        /// <param name="dblRentFeePayAmt">임대료적용달러</param>
        /// <returns></returns>
        public static object[] UpdateTempRentFeeInfo(string strRentCd, int intRentFeeTmpSeq, int intRentFeeSeq, string strCompNo, string strMemNo, string strRentFeeStartDt, string strRentFeeEndDt, 
                                                     double dblRentFeeExcRate, double dblRentFeePayAmt)
        {
            object[] objParams = new object[9];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = intRentFeeTmpSeq;
            objParams[2] = intRentFeeSeq;
            objParams[3] = strCompNo;
            objParams[4] = strMemNo;
            objParams[5] = strRentFeeStartDt;
            objParams[6] = strRentFeeEndDt;
            objParams[7] = dblRentFeeExcRate;
            objParams[8] = dblRentFeePayAmt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_TEMPRENTFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateRentDepoitInfo : 오피스 / 리테일 보증금 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateRentDepoitInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-28
         * 용       도 : 오피스 / 리테일 보증금 수정
         * Input    값 : UpdateRentDepoitInfo(계약번호, 임시순번, 상세순번, 보증금예상납부일, 보증금, 적용환율, 보증금수납일, 보증금수납액)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateRentDepoitInfo : 오피스 / 리테일 보증금 임시 수정
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <param name="strDepositExpDt">보증금예상납부일</param>
        /// <param name="dblDepositExpAmt">보증금</param>
        /// <param name="dblDepositExcRate">적용환율</param>
        /// <param name="strDepositPayDt">보증금수납일</param>
        /// <param name="dblDepositPayAmt">보증금수납액</param>
        /// <returns></returns>
        public static object[] UpdateRentDepoitInfo(string strContractNo, int intDepositSeq, string strDepositExpDt, double dblDepositExpAmt, double dblDepositExcRate, string strDepositPayDt, double dblDepositPayAmt)
        {
            object[] objParams = new object[7];
            object[] objReturn = new object[2];

            objParams[0] = strContractNo;
            objParams[1] = intDepositSeq;
            objParams[2] = strDepositExpDt;
            objParams[3] = dblDepositExpAmt;
            objParams[4] = dblDepositExcRate;
            objParams[5] = strDepositPayDt;
            objParams[6] = dblDepositPayAmt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_RENTDEPOSITINFO_M00", objParams);

            return objReturn;
        }

        #endregion
        
        #region UpdateRentFeeInfo : 오피스 / 리테일 임대료 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 / 리테일 임대료 수정
         * Input    값 : UpdateRentFeeInfo(계약번호, 상세순번, 임대료적용시작일, 임대료적용종료일, 임대료적용환율, 임대료적용달러)
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// UpdateRentFeeInfo : 오피스 / 리테일 임대료 임시 수정
        /// </summary>
        /// <param name="feeItem"> </param>
        /// <returns></returns>
        public static object[] UpdateRentFeeInfo(string[] feeItem)
        {
            var objParams = new object[6];

            objParams[0] = feeItem[0];
            objParams[1] = int.Parse(feeItem[1]);
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(feeItem[2]).Replace("-", ""));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(feeItem[3]).Replace("-", ""));
            objParams[4] = double.Parse(feeItem[4]);
            objParams[5] = double.Parse(feeItem[5]);

            var objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_RENTFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateRentInfo : 리테일 및 오피스 임대 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateRentInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-30
         * 용       도 : 리테일 및 오피스 임대 정보 수정
         * Input    값 : UpdateRentInfo(리테일 및 오피스 임대 정보 객체)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateRentInfo : 리테일 및 오피스 임대 정보 등록
        /// </summary>
        /// <param name="riDs">리테일 및 오피스 임대 정보 객체</param>
        /// <returns></returns>
        public static object[] UpdateRentInfo(RentMngDs.RentInfo riDs)
        {
            var objParams = new object[99];

            objParams[0] = TextLib.MakeNullToEmpty(riDs.RentCd);
            objParams[1] = riDs.RentSeq;
            objParams[2] = TextLib.MakeNullToEmpty(riDs.PersonalCd);
            objParams[3] = TextLib.MakeNullToEmpty(riDs.ContStepCd);
            objParams[4] = riDs.DongToDollar;
            objParams[5] = TextLib.MakeNullToEmpty(riDs.ContractNo);
            objParams[6] = riDs.FloorNo;
            objParams[7] = TextLib.MakeNullToEmpty(riDs.RoomNo);
            objParams[8] = TextLib.MakeNullToEmpty(riDs.ExtRoomNo);
            objParams[9] = TextLib.MakeNullToEmpty(riDs.OTLAgreeDt);
            objParams[10] = TextLib.MakeNullToEmpty(riDs.RentAgreeDt);
            objParams[11] = TextLib.MakeNullToEmpty(riDs.RentStartDt);
            objParams[12] = TextLib.MakeNullToEmpty(riDs.RentEndDt);
            objParams[13] = riDs.TermMonth;
            objParams[14] = riDs.FreeRentMonth;
            objParams[15] = riDs.DepositViAmtNo;
            objParams[16] = TextLib.MakeNullToEmpty(riDs.DepositViAmtEn);
            objParams[17] = riDs.DepositEnAmtNo;
            objParams[18] = TextLib.MakeNullToEmpty(riDs.DepositEnAmtEn);
            objParams[19] = TextLib.MakeNullToEmpty(riDs.DepositViAmtUnitCd);
            objParams[20] = TextLib.MakeNullToEmpty(riDs.PayStartYYYYMM);
            objParams[21] = riDs.PayTermMonth;
            objParams[22] = riDs.PayDay;
            objParams[23] = riDs.M_E_USING_DATE;
            objParams[24] = riDs.R_E_USING_DATE;
            objParams[25] = TextLib.MakeNullToEmpty(riDs.TotViAmtUnitCd);
            objParams[26] = riDs.TotEnAmtNo; //
            objParams[27] = TextLib.MakeNullToEmpty(riDs.TotEnAmtEn);
            objParams[28] = riDs.PerMsMonthEnAmtNo; //
            objParams[29] = riDs.PerMsMonthViAmtNo; //
            objParams[30] = TextLib.MakeNullToEmpty(riDs.PerMsMonthViAmtUnitCd);
            objParams[31] = riDs.InitDay; //
            objParams[32] = TextLib.MakeNullToEmpty(riDs.InitMMMngDt);
            objParams[33] = riDs.InitMMMngVNDNo; //
            objParams[34] = riDs.InitMMMngUSDNo; //
            objParams[35] = riDs.MMMngVNDNo;
            objParams[36] = riDs.MMMngUSDNo;
            objParams[37] = TextLib.MakeNullToEmpty(riDs.RentAddr);
            objParams[38] = TextLib.MakeNullToEmpty(riDs.RentDetAddr);
            objParams[39] = riDs.RentLeasingArea;
            objParams[40] = TextLib.MakeNullToEmpty(riDs.HandOverDt);
            objParams[41] = TextLib.MakeNullToEmpty(riDs.InteriorStartDt);
            objParams[42] = TextLib.MakeNullToEmpty(riDs.InteriorEndDt);
            objParams[43] = riDs.ConsDeposit; // 
            objParams[44] = TextLib.MakeNullToEmpty(riDs.ConsDepositDt);
            objParams[45] = riDs.ConsRefund; //
            objParams[46] = TextLib.MakeNullToEmpty(riDs.ConsRefundDt);
            objParams[47] = TextLib.MakeNullToEmpty(riDs.DifferenceReason);
            objParams[48] = TextLib.MakeNullToEmpty(riDs.LandloadCompNm);
            objParams[49] = TextLib.MakeNullToEmpty(riDs.LandloadRepNm);
            objParams[50] = TextLib.MakeNullToEmpty(riDs.LandloadNm);
            objParams[51] = TextLib.MakeNullToEmpty(riDs.LandloadAddr);
            objParams[52] = TextLib.MakeNullToEmpty(riDs.LandloadDetAddr);
            objParams[53] = TextLib.MakeNullToEmpty(riDs.LandloadTelTyCd);
            objParams[54] = TextLib.MakeNullToEmpty(riDs.LandloadTelFrontNo);
            objParams[55] = TextLib.MakeNullToEmpty(riDs.LandloadTelRearNo);
            objParams[56] = TextLib.MakeNullToEmpty(riDs.LandloadMobileTyCd);
            objParams[57] = TextLib.MakeNullToEmpty(riDs.LandloadMobileFrontNo);
            objParams[58] = TextLib.MakeNullToEmpty(riDs.LandloadMobileRearNo);
            objParams[59] = TextLib.MakeNullToEmpty(riDs.LandloadFaxTyCd);
            objParams[60] = TextLib.MakeNullToEmpty(riDs.LandloadFaxFrontNo);
            objParams[61] = TextLib.MakeNullToEmpty(riDs.LandloadFaxRearNo);
            objParams[62] = TextLib.MakeNullToEmpty(riDs.EmailID);
            objParams[63] = TextLib.MakeNullToEmpty(riDs.EmailServer);
            objParams[64] = TextLib.MakeNullToEmpty(riDs.LandloadTaxCd);
            objParams[65] = TextLib.MakeNullToEmpty(riDs.LandloadCorpCert);
            objParams[66] = TextLib.MakeNullToEmpty(riDs.LandloadIssuedDt);
            objParams[67] = TextLib.MakeNullToEmpty(riDs.PodiumYn);
            objParams[68] = riDs.MinimumRentFee;
            objParams[69] = riDs.ApplyRate;
            objParams[70] = TextLib.MakeNullToEmpty(riDs.Memo);
            objParams[71] = TextLib.MakeNullToEmpty(riDs.InsKNMemNo);
            objParams[72] = TextLib.MakeNullToEmpty(riDs.InsCompNo);
            objParams[73] = TextLib.MakeNullToEmpty(riDs.InsMemNo);
            objParams[74] = TextLib.MakeNullToEmpty(riDs.InsMemIP);
            objParams[75] = TextLib.MakeNullToEmpty(riDs.SaveYn);
            objParams[76] = TextLib.MakeNullToEmpty(riDs.CURNCY_TYPE);
            objParams[77] = riDs.FIXED_DONGTODOLLAR;
            objParams[78] = riDs.INFLATION_RATE;
            objParams[79] = riDs.CONTRACT_TYPE;
            objParams[80] = riDs.PAYMENT_TYPE;
            objParams[81] = riDs.CPI;
            objParams[82] = riDs.R_PAYCYCLE_TYPE;
            objParams[83] = riDs.R_PAYCYCLE;
            objParams[84] = riDs.R_S_PAY_DATE;
            objParams[85] = riDs.M_PAYCYCLE_TYPE;
            objParams[86] = riDs.M_PAYCYCLE;
            objParams[87] = riDs.M_S_PAY_DATE;
            objParams[88] = riDs.M_S_USING_DATE;
            objParams[89] = riDs.R_S_USING_DATE;
            objParams[90] = riDs.R_ISUE_DATE_TYPE;
            objParams[91] = riDs.M_ISUE_DATE_TYPE;
            objParams[92] = riDs.R_ISUE_DATE_ADJUST;
            objParams[93] = riDs.M_ISUE_DATE_ADJUST;
            objParams[94] = riDs.IS_SPECIAL;
            objParams[95] = riDs.REMARK;
            objParams[96] = riDs.IndustryCd;
            objParams[97] = riDs.NatCd;
            objParams[98] = riDs.RenewDt;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_RENTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteEntireTempRentDepositInfo : 오피스 / 리테일 보증금 미삭제 데이터 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteEntireTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 / 리테일 보증금 미삭제 데이터 삭제
         * Input    값 : DeleteEntireTempRentDepositInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteEntireTempRentDepositInfo : 오피스 / 리테일 보증금 미삭제 데이터 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteEntireTempRentDepositInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_TEMPRENTDEPOSITINFO_M01");

            return objReturn;
        }

        #endregion

        #region DeleteEntireTempRentFeeInfo : 오피스 / 리테일 임대료 미삭제 데이터 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteEntireTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 / 리테일 임대료 미삭제 데이터 삭제
         * Input    값 : DeleteEntireTempRentFeeInfo()
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// DeleteEntireTempRentFeeInfo : 오피스 / 리테일 임대료 미삭제 데이터 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteEntireTempRentFeeInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_TEMPRENTFEEINFO_M01");

            return objReturn;
        }

        #endregion

        #region DeleteTempRentDepositInfo : 오피스 / 리테일 보증금 임시저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 / 리테일 보증금 임시저장부분 삭제
         * Input    값 : DeleteTempRentDepositInfo(임시순번, 상세순번)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempRentDepositInfo : 오피스 / 리테일 보증금 임시저장부분 삭제
        /// </summary>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <returns></returns>
        public static object[] DeleteTempRentDepositInfo(int intDepositTmpSeq, int intDepositSeq, string strCompNo, string strMemNo)
        {
            object[] objParams = new object[4];
            object[] objReturn = new object[2];

            objParams[0] = intDepositTmpSeq;
            objParams[1] = intDepositSeq;
            objParams[2] = strCompNo;
            objParams[3] = strMemNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_TEMPRENTDEPOSITINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteTempRentFeeInfo : 오피스 / 리테일 임대료 임시저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 오피스 / 리테일 임대료 임시저장부분 삭제
         * Input    값 : DeleteTempRentFeeInfo(임시순번, 상세순번)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempRentFeeInfo : 오피스 / 리테일 임대료 임시저장부분 삭제
        /// </summary>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <param name="intRentFeeSeq">상세순번</param>
        /// <returns></returns>
        public static object[] DeleteTempRentFeeInfo(int intRentFeeTmpSeq, int intRentFeeSeq, string strCompNo, string strMemNo)
        {
            object[] objParams = new object[4];
            object[] objReturn = new object[2];

            objParams[0] = intRentFeeTmpSeq;
            objParams[1] = intRentFeeSeq;
            objParams[2] = strCompNo;
            objParams[3] = strMemNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_TEMPRENTFEEINFO_M00", objParams);

            return objReturn;
        }

        //Baotv - Delete Fee
        public static object[] DeleteMngInfo(int intSeq,string strContractNo)
        {
            var objParams = new object[2];

            objParams[0] = strContractNo;
            objParams[1] = intSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_MNGFEEINFO_D00", objParams);

            return objReturn;
        }

        //Baotv - Update MngFee
        public static object[] UpdateMngInfo(string[] listfee, string strContractNo)
        {
            var objParams = new object[9];

            objParams[0] = strContractNo;
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(listfee[0]).Replace("-", ""));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(listfee[1]).Replace("-", ""));
            objParams[3] = double.Parse(listfee[2]);
            objParams[4] = double.Parse(listfee[3]);
            objParams[5] = int.Parse(listfee[4]);
            objParams[6] = listfee[5];
            objParams[7] = listfee[6];
            objParams[8] = listfee[7];

            var objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_MNGFEEINFO_U00", objParams);

            return objReturn;
        }
               

        #endregion
        
        #region DeleteTempRentDepositInfo : 오피스 / 리테일 보증금 임시저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-21
         * 용       도 : 오피스 / 리테일 보증금 임시저장부분 삭제
         * Input    값 : DeleteTempRentDepositInfo(임시순번)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempRentDepositInfo : 오피스 / 리테일 보증금 임시저장부분 삭제
        /// </summary>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <returns></returns>
        public static object[] DeleteTempRentDepositInfo(int intDepositTmpSeq, string strCompNo, string strMemNo)
        {
            object[] objParams = new object[3];
            object[] objReturn = new object[2];

            objParams[0] = intDepositTmpSeq;
            objParams[1] = strCompNo;
            objParams[2] = strMemNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_TEMPRENTDEPOSITINFO_M02", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteTempRentFeeInfo : 오피스 / 리테일 임대료 임시저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-21
         * 용       도 : 오피스 / 리테일 임대료 임시저장부분 삭제
         * Input    값 : DeleteTempRentFeeInfo(임시순번)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempRentFeeInfo : 오피스 / 리테일 임대료 임시저장부분 삭제
        /// </summary>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <returns></returns>
        public static object[] DeleteTempRentFeeInfo(int intRentFeeTmpSeq, string strCompNo, string strMemNo)
        {
            object[] objParams = new object[3];
            object[] objReturn = new object[2];

            objParams[0] = intRentFeeTmpSeq;
            objParams[1] = strCompNo;
            objParams[2] = strMemNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_TEMPRENTFEEINFO_M02", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteRentDepoitInfo : 오피스 / 리테일 보증금 저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRentDepoitInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-28
         * 용       도 : 오피스 / 리테일 보증금 저장부분 삭제
         * Input    값 : DeleteRentDepoitInfo(계약번호, 상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteRentDepoitInfo : 오피스 / 리테일 보증금 저장부분 삭제
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <returns></returns>
        public static object[] DeleteRentDepoitInfo(string strContractNo, int intDepositSeq, string strCompNo, string strMemNo)
        {
            object[] objParams = new object[4];
            object[] objReturn = new object[2];

            objParams[0] = strContractNo;
            objParams[1] = intDepositSeq;
            objParams[2] = strCompNo;
            objParams[3] = strMemNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_RENTDEPOSITINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteRentFeeInfo : 오피스 / 리테일 임대료 저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 / 리테일 임대료 저장부분 삭제
         * Input    값 : DeleteRentFeeInfo(계약번호, 상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// DeleteRentFeeInfo : 오피스 / 리테일 임대료 저장부분 삭제
        /// </summary>
        /// <param name="feeSeq"> </param>
        /// <param name="strContractNo">계약번호</param>
        /// <returns></returns>
        public static object[] DeleteRentFeeInfo(int feeSeq, string strContractNo)
        {
            var objParams = new object[2];

            objParams[0] = strContractNo;
            objParams[1] = feeSeq;

            object[] objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_RENTFEEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteSaleInfo : 아파트 임대계약정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteSaleInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-13
         * 용       도 : 아파트 임대계약정보 삭제
         * Input    값 : DeleteSaleInfo(섹션코드, 섹션순번, 삭제사유, 삭제기업, 삭제사번, 삭제IP)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 아파트 임대계약정보 삭제
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intRentSeq">섹션순번</param>
        /// <param name="strDelReason">삭제사유</param>
        /// <param name="strCompNo">삭제기업</param>
        /// <param name="strDelMemNo">삭제사번</param>
        /// <param name="strDelIP">삭제IP</param>
        /// <returns></returns>
        public static object[] DeleteSaleInfo(string strRentCd, int intRentSeq, string strDelReason, string strCompNo, string strDelMemNo, string strDelIP)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = intRentSeq;
            objParams[2] = strDelReason;
            objParams[3] = strCompNo;
            objParams[4] = TextLib.StringEncoder(strDelMemNo);
            objParams[5] = strDelIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_SALESINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteRentInfo : 임대계약 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRentInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-05
         * 용       도 : 임대계약 삭제
         * Input    값 : DeleteRentInfo(임대구분코드, 임대순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteRentInfo : 임대계약 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대순번</param>
        /// <returns></returns>
        public static object[] DeleteRentInfo(string strRentCd, int intRentSeq)
        {
            var objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = intRentSeq;

            var objReturns = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_RENTINFO_M00", objParams);

            return objReturns;
        }

        public static object[] TerminateRentInfo(string strRentCd, int intRentSeq, string terminateDt)
        {
            var objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = intRentSeq;
            objParams[2] = terminateDt;

            var objReturns = SPExecute.ExecReturnNo("KN_USP_RES_TERMINATE_RENTINFO_M00", objParams);

            return objReturns;
        }

        #endregion        

        #region DeleteRentDepositionInfo : 임대보증금 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRentDepositionInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-27
         * 용       도 : 임대보증금 삭제
         * Input    값 : DeleteRentDepositionInfo(계약번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteRentDepositionInfo : 임대계약 삭제
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <returns></returns>
        public static object[] DeleteRentDepositionInfo(string strContractNo)
        {
            object[] objReturns = new object[2];
            object[] objParams = new object[1];

            objParams[0] = strContractNo;

            objReturns = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_RENTDEPOSITINFO_M01", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteRentFeeInfo : 임대료 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 임대료 삭제
         * Input    값 : DeleteRentFeeInfo(계약번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteRentFeeInfo : 임대료 삭제
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <returns></returns>
        public static object[] DeleteRentFeeInfo(string strContractNo)
        {
            object[] objReturns = new object[2];
            object[] objParams = new object[1];

            objParams[0] = strContractNo;

            objReturns = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_RENTFEEINFO_M01", objParams);

            return objReturns;
        }

        #endregion
    }
}