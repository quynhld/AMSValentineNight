using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;

namespace KN.Resident.Dac
{
    public class ResidentMngDao
    {
        #region SelectSalesUserInfo : 아파트 및 아파트 상가 입주자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectSalesUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-30
         * 용       도 : 아파트 및 아파트 상가 입주자 목록 조회
         * Input    값 : SelectSalesUserInfo(페이지별 리스트 크기, 현재페이지, 임대구분코드, 검색대상명, 검색층, 검색호, 검색입주시작일, 검색입주종료일)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectSalesUserInfo : 아파트 및 아파트 상가 입주자 목록 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strNm">검색대상명</param>
        /// <param name="strFloorNo">검색층</param>
        /// <param name="strRoomNo">검색호</param>
        /// <param name="strRentStartDt">검색입주시작일</param>
        /// <param name="strRentEndDt">검색입주종료일</param>
        /// <returns></returns>
        public static DataSet SelectSalesUserInfo(int intPageSize, int intNowPage, string strRentCd, string strNm, string strRoomNo, 
                                                  string strRentStartDt, string strRentEndDt)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[7];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[3] = TextLib.MakeNullToEmpty(strNm);
            objParams[4] = strRoomNo;
            objParams[5] = strRentStartDt;
            objParams[6] = strRentEndDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_USERINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectSalesExcelUserInfo : 엑셀용 아파트 및 아파트 상가 입주자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectSalesExcelUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-30
         * 용       도 : 엑셀용 아파트 및 아파트 상가 입주자 목록 조회
         * Input    값 : SelectSalesExcelUserInfo(페이지별 리스트 크기, 현재페이지, 임대구분코드, 검색대상명, 검색층, 검색호, 검색입주시작일, 검색입주종료일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectSalesExcelUserInfo : 엑셀용 아파트 및 아파트 상가 입주자 목록 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strNm">검색대상명</param>
        /// <param name="strRoomNo">검색호</param>
        /// <param name="strRentStartDt">검색입주시작일</param>
        /// <param name="strRentEndDt">검색입주종료일</param>
        /// <returns></returns>
        public static DataTable SelectSalesExcelUserInfo(string strRentCd, string strNm, string strRoomNo, string strRentStartDt, string strRentEndDt, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[6];

            objParams[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[1] = TextLib.MakeNullToEmpty(strNm);
            objParams[2] = strRoomNo;
            objParams[3] = strRentStartDt;
            objParams[4] = strRentEndDt;
            objParams[5] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_USERINFO_S05", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRentUserInfo : 오피스 및 리테일 입주자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-30
         * 용       도 : 오피스 및 리테일 입주자 목록 조회
         * Input    값 : SelectRentUserInfo(페이지별 리스트 크기, 현재페이지, 임대구분코드, 검색대상명, 검색층, 검색호, 검색입주시작일, 검색입주종료일)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectRentUserInfo : 오피스 및 리테일 입주자 목록 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strNm">검색대상명</param>
        /// <param name="strRoomNo">검색호</param>
        /// <param name="strRentStartDt">검색입주시작일</param>
        /// <param name="strRentEndDt">검색입주종료일</param>
        /// <returns></returns>
        public static DataSet SelectRentUserInfo(int intPageSize, int intNowPage, string strRentCd, string strNm, string strRoomNo,
                                                 string strRentStartDt, string strRentEndDt)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[7];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[3] = TextLib.MakeNullToEmpty(strNm);
            objParams[4] = strRoomNo;
            objParams[5] = strRentStartDt;
            objParams[6] = strRentEndDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_USERINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectRentExcelUserInfo : 엑셀용 오피스 및 리테일 입주자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentExcelUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-15
         * 용       도 : 엑셀용 오피스 및 리테일 입주자 목록 조회
         * Input    값 : SelectRentExcelUserInfo(임대구분코드, 검색대상명, 검색층, 검색호, 검색입주시작일, 검색입주종료일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRentExcelUserInfo : 엑셀용 오피스 및 리테일 입주자 목록 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strNm">검색대상명</param>
        /// <param name="strRoomNo">검색호</param>
        /// <param name="strRentStartDt">검색입주시작일</param>
        /// <param name="strRentEndDt">검색입주종료일</param>
        /// <returns></returns>
        public static DataTable SelectRentExcelUserInfo(string strRentCd, string strNm, string strRoomNo, string strRentStartDt, string strRentEndDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[5];

            objParams[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[1] = TextLib.MakeNullToEmpty(strNm);
            objParams[2] = strRoomNo;
            objParams[3] = strRentStartDt;
            objParams[4] = strRentEndDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_USERINFO_S06", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectUserTmpInfo : 입주자 관련 임시정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectUserTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 조회
         * Input    값 : SelectUserTmpInfo(입주자 임시순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectUserTmpInfo : 입주자 관련 임시정보 조회
        /// </summary>
        /// <param name="intUserSeq">입주자 임시순번</param>
        /// <returns></returns>
        public static DataSet SelectUserTmpInfo(int intUserSeq)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[1];

            objParams[0] = intUserSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_USERINFO_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectSalesUserView : 아파트 및 아파트 상가 입주자 상세 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectSalesUserView
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-14
         * 용       도 : 아파트 및 아파트 상가 입주자 상세 정보 조회
         * Input    값 : SelectSalesUserView(입주자코드, 임대구분코드, 임대구분상세순번, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectSalesUserView : 아파트 및 아파트 상가 입주자 상세 정보 조회
        /// </summary>
        /// <param name="strUserSeq">입주자코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분상세순번</param>
        /// <returns></returns>
        public static DataSet SelectSalesUserView(string strUserSeq, string strRentCd, int intRentSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[4];

            objParams[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParams[1] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[2] = intRentSeq;
            objParams[3] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_USERINFO_S03", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectRentSeqUserInfo

        /**********************************************************************************************
         * Method   명 : KN_USP_RES_SELECT_RENTSEQ_S01
         * Author      : PHUONGTV
         * DATE        : 2014-01-06
         * Description : Check Userseq,RentSeq in UserInfo
         * Input    값 : KN_USP_RES_SELECT_RENTSEQ_S01(strUserSeq, strRentCd, intRentSeq)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// KN_USP_RES_SELECT_RENTSEQ_S01 : 
        /// </summary>
        /// <param name="strUserSeq">입주자코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분상세순번</param>
        /// <returns></returns>
        public static DataSet SelectRentSeqUserInfo(string strUserSeq, string strRentCd, int intRentSeq)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[3];

            objParams[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParams[1] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[2] = intRentSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_RENTSEQ_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectRentUserView : 오피스 및 리테일 입주자 상세 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRentUserView
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-14
         * 용       도 : 오피스 및 리테일 입주자 상세 정보 조회
         * Input    값 : SelectRentUserView(입주자코드, 임대구분코드, 임대구분상세순번, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectRentUserView : 오피스 및 리테일 입주자 상세 정보 조회
        /// </summary>
        /// <param name="strUserSeq">입주자코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분상세순번</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectRentUserView(string strUserSeq, string strRentCd, int intRentSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[4];

            objParams[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParams[1] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[2] = intRentSeq;
            objParams[3] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_USERINFO_S04", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectOccpantInfo : 아파트 및 아파트 상가 입주자 기존 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectOccpantInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 아파트 및 아파트 상가 입주자 기존 정보 조회
         * Input    값 : SelectOccpantInfo(임대구분코드, 임대구분순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectOccpantInfo : 아파트 및 아파트 상가 입주자 기존 정보 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분순번</param>
        /// <returns></returns>
        public static DataTable SelectOccpantInfo(string strRentCd, int intRentSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[1] = intRentSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_SALESINFO_S04", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectTenantInfo : 리테일 및 오피스 입주자 기존 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectTenantInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 리테일 및 오피스 입주자 기존 정보 조회
         * Input    값 : SelectTenantInfo(임대구분코드, 임대구분순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectTenantInfo : 리테일 및 오피스 입주자 기존 정보 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분순번</param>
        /// <returns></returns>
        public static DataTable SelectTenantInfo(string strRentCd, int intRentSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[1] = intRentSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_RENTINFO_S05", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertTempUserInfo : 추가 정보 관리를 위한 임시정보 등록후 임시번호 리턴

        /**********************************************************************************************
         * Mehtod   명 : InsertTempUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 추가 정보 관리를 위한 임시정보 등록후 임시번호 리턴
         * Input    값 : InsertTempUserInfo(등록회사코드, 등록사원번호, 접근IP정보)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempUserInfo : 추가 정보 관리를 위한 임시정보 등록후 임시번호 리턴
        /// </summary>
        /// <param name="strCompNo">등록회사코드</param>
        /// <param name="strMemNo">등록사원번호</param>
        /// <param name="strMemIP">접근IP정보</param>
        /// <returns></returns>
        public static DataTable InsertTempUserInfo(string strCompNo, string strMemNo, string strMemIP)
        {
            object[] objParmas = new object[3];
            DataTable dtReturn = new DataTable();

            objParmas[0] = strCompNo;
            objParmas[1] = strMemNo;
            objParmas[2] = strMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_TEMPUSERINFO_S00", objParmas);

            return dtReturn;
        }

        #endregion

        #region InsertUserAddonTmpInfo : 입주자 관련 동거인 임시정보 저장

        /**********************************************************************************************
         * Mehtod   명 : InsertUserAddonTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-12
         * 용       도 : 입주자 관련 동거인 임시정보 저장
         * Input    값 : InsertUserAddonTmpInfo(임시순번, 동거인명, 성별, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUserAddonTmpInfo : 입주자 관련 동거인 임시정보 저장
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="strAccessNm">동거인명</param>
        /// <param name="strGender">성별</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] InsertUserAddonTmpInfo(int intUserSeq, string strAccessNm, string strGender, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = intUserSeq;
            objParams[1] = TextLib.MakeNullToEmpty(TextLib.StringEncoder(strAccessNm));
            objParams[2] = strGender;
            objParams[3] = strRelationCd;
            objParams[4] = strInsCompNo;
            objParams[5] = strInsMemNo;
            objParams[6] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_USERADDONINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertUserAddonInfo : 입주자 관련 동거인 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : InsertUserAddonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 관련 동거인 정보 저장
         * Input    값 : InsertUserAddonInfo(입주자순번, 동거인명, 성별, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUserAddonInfo : 입주자 관련 동거인 정보 저장
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="strAccessNm">동거인명</param>
        /// <param name="strGender">성별</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] InsertUserAddonInfo(string strUserSeq, string strAccessNm, string strGender, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = strUserSeq;
            objParams[1] = TextLib.MakeNullToEmpty(TextLib.StringEncoder(strAccessNm));
            objParams[2] = strGender;
            objParams[3] = strRelationCd;
            objParams[4] = strInsCompNo;
            objParams[5] = strInsMemNo;
            objParams[6] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_USERADDONINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region InsertUserAccessTmpInfo : 입주자 카드 임시 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : InsertUserAccessTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 카드 임시 정보 저장
         * Input    값 : InsertUserAccessTmpInfo(임시순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUserAccessTmpInfo : 입주자 카드 임시 정보 저장
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="strAccessCardNo">카드번호</param>
        /// <param name="strAccessNm">접속자명</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] InsertUserAccessTmpInfo(int intUserSeq, string strAccessCardNo, string strAccessNm, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = intUserSeq;
            objParams[1] = strAccessCardNo;
            objParams[2] = TextLib.MakeNullToEmpty(TextLib.StringEncoder(strAccessNm));
            objParams[3] = strRelationCd;
            objParams[4] = strInsCompNo;
            objParams[5] = strInsMemNo;
            objParams[6] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_USERACCESSINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertUserAccessInfo : 입주자 카드 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : InsertUserAccessInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 카드 정보 저장
         * Input    값 : InsertUserAccessInfo(입주자순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUserAccessInfo : 입주자 카드 정보 저장
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="strAccessCardNo">카드번호</param>
        /// <param name="strAccessNm">접속자명</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] InsertUserAccessInfo(string strUserSeq, string strAccessCardNo, string strAccessNm, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = strUserSeq;
            objParams[1] = strAccessCardNo;
            objParams[2] = TextLib.MakeNullToEmpty(TextLib.StringEncoder(strAccessNm));
            objParams[3] = strRelationCd;
            objParams[4] = strInsCompNo;
            objParams[5] = strInsMemNo;
            objParams[6] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_USERACCESSINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region InsertUserMng : 입주자 관련 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : InsertUserMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 카드 임시 정보 저장
         * Input    값 : InsertUserMng(임시순번, 층번호, 방번호, 임대구분코드, 임대구분순번, 입주일, 생일, 성별, 전화번호지역번호, 전화번호국번호, 
         *                             핸드폰기지국번호, 핸드폰국번호, 핸드폰식별번호, 등록사번, 등록IP)
         * )
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// InsertUserMng : 입주자 관련 정보 저장
        /// </summary>
        /// <param name="intTmpUserSeq">임시순번</param>
        /// <param name="intFloor">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분순번</param>
        /// <param name="strUserNm">거주자이름</param>
        /// <param name="strOccupationDt">입주일</param>
        /// <param name="strBirthDt">생일</param>
        /// <param name="strGender">성별</param>
        /// <param name="strTelTyCd">전화번호지역번호</param>
        /// <param name="strTelFrontNo">전화번호국번호</param>
        /// <param name="strTelRearNo">전화번호식별번호</param>
        /// <param name="strMobileTyCd">핸드폰기지국번호</param>
        /// <param name="strMobileFrontNo">핸드폰국번호</param>
        /// <param name="strMobileRearNo">핸드폰식별번호</param>
        /// <param name="strUserTaxCd">세금코드</param>
        /// <param name="strUserAddr">TAX용주소</param>
        /// <param name="strUserDetAddr">TAX용상세주소</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <param name="ksysCd"> </param>
        /// <returns></returns>
        public static object[] InsertUserMng(int intTmpUserSeq, int intFloor, string strRoomNo, string strRentCd, int intRentSeq, string strUserNm, string strOccupationDt,
                                             string strBirthDt, string strGender, string strTelTyCd, string strTelFrontNo, string strTelRearNo, string strMobileTyCd, 
                                             string strMobileFrontNo, string strMobileRearNo, string strUserTaxCd, string strUserAddr, string strUserDetAddr,
                                             string strInsCompNo, string strInsMemNo, string strInsMemIP,string ksysCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[22];

            objParams[0] = intTmpUserSeq;
            objParams[1] = intFloor;
            objParams[2] = strRoomNo;
            objParams[3] = strRentCd;
            objParams[4] = intRentSeq;
            objParams[5] = strUserNm;
            objParams[6] = TextLib.MakeNullToEmpty(strOccupationDt);
            objParams[7] = TextLib.MakeNullToEmpty(strBirthDt);
            objParams[8] = strGender;
            objParams[9] = TextLib.MakeNullToEmpty(strTelTyCd);
            objParams[10] = TextLib.MakeNullToEmpty(strTelFrontNo);
            objParams[11] = TextLib.MakeNullToEmpty(strTelRearNo);
            objParams[12] = TextLib.MakeNullToEmpty(strMobileTyCd);
            objParams[13] = TextLib.MakeNullToEmpty(strMobileFrontNo);
            objParams[14] = TextLib.MakeNullToEmpty(strMobileRearNo);
            objParams[15] = TextLib.MakeNullToEmpty(strUserTaxCd);
            objParams[16] = TextLib.MakeNullToEmpty(strUserAddr);
            objParams[17] = TextLib.MakeNullToEmpty(strUserDetAddr);
            objParams[18] = strInsCompNo;
            objParams[19] = strInsMemNo;
            objParams[20] = strInsMemIP;
            objParams[21] = ksysCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_USERINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertUserKSysMatchInfo: KSystem 고객번호 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertUserKSysMatchInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-03
         * 용       도 : KSystem 고객번호 등록
         * Input    값 : InsertUserKSysMatchInfo(사용자 번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertUserKSysMatchInfo: KSystem 고객번호 등록
        /// </summary>
        /// <param name="strUserNo">사용자 번호</param>
        /// <returns></returns>
        public static object[] InsertUserKSysMatchInfo(string strUserNo)
        {
            object[] objParams = new object[1];
            object[] objReturn = new object[2];

            objParams[0] = strUserNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_USERKSYSMATCHINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateUserAddonTmpInfo : 입주자 관련 동거인 임시정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateUserAddonTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-12
         * 용       도 : 입주자 관련 동거인 임시정보 수정
         * Input    값 : UpdateUserAddonTmpInfo(임시순번, 임시상세순번, 동거인명, 성별, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateUserAddonTmpInfo : 입주자 관련 동거인 임시정보 수정
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <param name="strAccessNm">동거인명</param>
        /// <param name="strGender">성별</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] UpdateUserAddonTmpInfo(int intUserSeq, int intUserDetSeq, string strAccessNm, string strGender, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[8];

            objParams[0] = intUserSeq;
            objParams[1] = intUserDetSeq;
            objParams[2] = TextLib.MakeNullToEmpty(TextLib.StringEncoder(strAccessNm));
            objParams[3] = strGender;
            objParams[4] = strRelationCd;
            objParams[5] = strInsCompNo;
            objParams[6] = strInsMemNo;
            objParams[7] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_USERADDONINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateUserAddonInfo : 입주자 관련 동거인 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateUserAddonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 관련 동거인 정보 수정
         * Input    값 : UpdateUserAddonInfo(입주자번호, 입주자상세순번, 동거인명, 성별, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateUserAddonInfo : 입주자 관련 동거인 정보 수정
        /// </summary>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="intUserDetSeq">입주자상세순번</param>
        /// <param name="strAccessNm">동거인명</param>
        /// <param name="strGender">성별</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] UpdateUserAddonInfo(string strUserSeq, int intUserDetSeq, string strAccessNm, string strGender, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[8];

            objParams[0] = strUserSeq;
            objParams[1] = intUserDetSeq;
            objParams[2] = TextLib.MakeNullToEmpty(TextLib.StringEncoder(strAccessNm));
            objParams[3] = strGender;
            objParams[4] = strRelationCd;
            objParams[5] = strInsCompNo;
            objParams[6] = strInsMemNo;
            objParams[7] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_USERADDONINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateUserAccessTmpInfo : 입주자 관련 카드 임시정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateUserAccessTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 관련 카드 임시정보 수정
         * Input    값 : UpdateUserAccessTmpInfo(임시순번, 임시상세순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateUserAccessTmpInfo : 입주자 관련 카드 임시정보 수정
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <param name="strAccessCardNo">카드번호</param>
        /// <param name="strAccessNm">접속자명</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] UpdateUserAccessTmpInfo(int intUserSeq, int intUserDetSeq, string strAccessCardNo, string strAccessNm, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[8];

            objParams[0] = intUserSeq;
            objParams[1] = intUserDetSeq;
            objParams[2] = strAccessCardNo;
            objParams[3] = TextLib.MakeNullToEmpty(TextLib.StringEncoder(strAccessNm));
            objParams[4] = strRelationCd;
            objParams[5] = strInsCompNo;
            objParams[6] = strInsMemNo;
            objParams[7] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_USERACCESSINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateUserAccessInfo : 입주자 관련 카드 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateUserAccessInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 관련 카드 정보 수정
         * Input    값 : UpdateUserAccessInfo(입주자번호, 입주자상세순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateUserAccessInfo : 입주자 관련 카드 정보 수정
        /// </summary>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="intUserDetSeq">입주자상세순번</param>
        /// <param name="strAccessCardNo">카드번호</param>
        /// <param name="strAccessNm">접속자명</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] UpdateUserAccessInfo(string strUserSeq, int intUserDetSeq, string strAccessCardNo, string strAccessNm, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[8];

            objParams[0] = strUserSeq;
            objParams[1] = intUserDetSeq;
            objParams[2] = strAccessCardNo;
            objParams[3] = TextLib.MakeNullToEmpty(TextLib.StringEncoder(strAccessNm));
            objParams[4] = strRelationCd;
            objParams[5] = strInsCompNo;
            objParams[6] = strInsMemNo;
            objParams[7] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_USERACCESSINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateUserInfo : 입주자 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 정보 수정
         * Input    값 : UpdateUserInfo(입주자번호, 층번호, 방번호, 임대구분코드, 임대구분순번, 입주일, 생일, 성별, 전화번호지역번호, 전화번호국번호, 
         *                             핸드폰기지국번호, 핸드폰국번호, 핸드폰식별번호, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// UpdateUserInfo : 입주자 정보 수정
        /// </summary>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="intFloor">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분순번</param>
        /// <param name="strUserNm">거주자이름</param>
        /// <param name="strOccupationDt">입주일</param>
        /// <param name="strBirthDt">생일</param>
        /// <param name="strGender">성별</param>
        /// <param name="strTelTyCd">전화번호지역번호</param>
        /// <param name="strTelFrontNo">전화번호국번호</param>
        /// <param name="strTelRearNo">전화번호식별번호</param>
        /// <param name="strMobileTyCd">핸드폰기지국번호</param>
        /// <param name="strMobileFrontNo">핸드폰국번호</param>
        /// <param name="strMobileRearNo">핸드폰식별번호</param>
        /// <param name="strUserTaxCd">세금코드</param>
        /// <param name="strUserAddr">TAX용주소</param>
        /// <param name="strUserDetAddr">TAX용상세주소</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <param name="ksysCd"> </param>
        /// <returns></returns>
        public static object[] UpdateUserInfo(string strUserSeq, int intFloor, string strRoomNo, string strRentCd, int intRentSeq, string strUserNm, string strOccupationDt,
                                              string strBirthDt, string strGender, string strTelTyCd, string strTelFrontNo, string strTelRearNo, string strMobileTyCd,
                                              string strMobileFrontNo, string strMobileRearNo, string strUserTaxCd, string strUserAddr, string strUserDetAddr,
                                              string strInsCompNo, string strInsMemNo, string strInsMemIP, string ksysCd)
        {
            var objParams = new object[22];

            objParams[0] = strUserSeq;
            objParams[1] = intFloor;
            objParams[2] = strRoomNo;
            objParams[3] = strRentCd;
            objParams[4] = intRentSeq;
            objParams[5] = strUserNm;
            objParams[6] = TextLib.MakeNullToEmpty(strOccupationDt);
            objParams[7] = TextLib.MakeNullToEmpty(strBirthDt);
            objParams[8] = strGender;
            objParams[9] = TextLib.MakeNullToEmpty(strTelTyCd);
            objParams[10] = TextLib.MakeNullToEmpty(strTelFrontNo);
            objParams[11] = TextLib.MakeNullToEmpty(strTelRearNo);
            objParams[12] = TextLib.MakeNullToEmpty(strMobileTyCd);
            objParams[13] = TextLib.MakeNullToEmpty(strMobileFrontNo);
            objParams[14] = TextLib.MakeNullToEmpty(strMobileRearNo);
            objParams[15] = TextLib.MakeNullToEmpty(strUserTaxCd);
            objParams[16] = TextLib.MakeNullToEmpty(strUserAddr);
            objParams[17] = TextLib.MakeNullToEmpty(strUserDetAddr);
            objParams[18] = strInsCompNo;
            objParams[19] = strInsMemNo;
            objParams[20] = strInsMemIP;
            objParams[21] = ksysCd;

            var objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_USERINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteTempUserInfo : 2일이 지난 입주자 임시정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 2일이 지난 입주자 임시정보 삭제
         * Input    값 : DeleteTempUserInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempUserInfo : 2일이 지난 입주자 임시정보 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteTempUserInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_TEMPUSERINFO_M00");

            return objReturn;
        }

        #endregion

        #region DeleteUserAddonTmpInfo : 입주자 관련 동거인 임시정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteUserAddonTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-12
         * 용       도 : 입주자 관련 동거인 임시정보 삭제
         * Input    값 : DeleteUserAddonTmpInfo(임시순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteUserAddonTmpInfo : 입주자 관련 동거인 임시정보 삭제
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] DeleteUserAddonTmpInfo(int intUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intUserSeq;
            objParams[1] = intUserDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_USERADDONINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteUserAddonInfo : 입주자 관련 동거인 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteUserAddonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-12
         * 용       도 : 입주자 관련 동거인 정보 삭제
         * Input    값 : DeleteUserAddonInfo(입주자순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteUserAddonInfo : 입주자 관련 동거인 정보 삭제
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] DeleteUserAddonInfo(string strUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strUserSeq;
            objParams[1] = intUserDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_USERADDONINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteUserAccessTmpInfo : 입주자 관련 카드 임시정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteUserAccessTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 관련 카드 임시정보 삭제
         * Input    값 : DeleteUserAccessTmpInfo(임시순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteUserAccessTmpInfo : 입주자 관련 카드 임시정보 삭제
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] DeleteUserAccessTmpInfo(int intUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intUserSeq;
            objParams[1] = intUserDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_USERACCESSINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteUserAccessInfo : 입주자 관련 카드 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteUserAccessInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 관련 카드 임시정보 삭제
         * Input    값 : DeleteUserAccessInfo(입주자순번, 상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteUserAccessInfo : 입주자 관련 카드 정보 삭제
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] DeleteUserAccessInfo(string strUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strUserSeq;
            objParams[1] = intUserDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_USERACCESSINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteUserInfo : 입주자 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteUserInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-14
         * 용       도 : 입주자 삭제
         * Input    값 : DeleteUserInfo(입주자번호, 등록회사코드, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteUserInfo : 입주자 삭제
        /// </summary>
        /// <param name="strUserSeq">입주자번호</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <param name="strRentCd">언어코드</param>
        /// <returns></returns>
        public static object[] DeleteUserInfo(string strUserSeq, string strInsCompNo, string strInsMemNo, string strInsMemIP, string strRentCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strUserSeq;
            objParams[1] = strInsCompNo;
            objParams[2] = strInsMemNo;
            objParams[3] = strInsMemIP;
            objParams[4] = strRentCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_USERINFO_M00", objParams);

            return objReturn;
        }

        #endregion
    }
}
