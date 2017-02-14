using System.Data;

using KN.Resident.Dac;

namespace KN.Resident.Biz
{
    public class ResidentMngBlo
    {
        #region SpreadSalesUserInfo : 아파트 및 아파트 상가 입주자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadSalesUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-30
         * 용       도 : 아파트 및 아파트 상가 입주자 목록 조회
         * Input    값 : SpreadSalesUserInfo(페이지별 리스트 크기, 현재페이지, 임대구분코드, 검색대상명, 검색층, 검색호, 검색입주시작일, 검색입주종료일)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadSalesUserInfo : 아파트 및 아파트 상가 입주자 목록 조회
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
        public static DataSet SpreadSalesUserInfo(int intPageSize, int intNowPage, string strRentCd, string strNm, string strRoomNo,
                                                  string strRentStartDt, string strRentEndDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ResidentMngDao.SelectSalesUserInfo(intPageSize, intNowPage, strRentCd, strNm, strRoomNo, strRentStartDt, strRentEndDt);

            return dsReturn;
        }

        #endregion

        #region SpreadSalesExcelUserInfo : 엑셀용 아파트 및 아파트 상가 입주자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadSalesExcelUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-30
         * 용       도 : 엑셀용 아파트 및 아파트 상가 입주자 목록 조회
         * Input    값 : SpreadSalesExcelUserInfo(페이지별 리스트 크기, 현재페이지, 임대구분코드, 검색대상명, 검색층, 검색호, 검색입주시작일, 검색입주종료일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadSalesExcelUserInfo : 엑셀용 아파트 및 아파트 상가 입주자 목록 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strNm">검색대상명</param>
        /// <param name="strRoomNo">검색호</param>
        /// <param name="strRentStartDt">검색입주시작일</param>
        /// <param name="strRentEndDt">검색입주종료일</param>
        /// <returns></returns>
        public static DataTable SpreadSalesExcelUserInfo(string strRentCd, string strNm, string strRoomNo, string strRentStartDt, string strRentEndDt, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ResidentMngDao.SelectSalesExcelUserInfo(strRentCd, strNm, strRoomNo, strRentStartDt, strRentEndDt, strLangCd);

            return dtReturn;
        }

        #endregion

        #region SpreadRentUserInfo : 오피스 및 리테일 입주자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRentUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-30
         * 용       도 : 오피스 및 리테일 입주자 목록 조회
         * Input    값 : SpreadRentUserInfo(페이지별 리스트 크기, 현재페이지, 임대구분코드, 검색대상명, 검색층, 검색호, 검색입주시작일, 검색입주종료일)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadRentUserInfo : 오피스 및 리테일 입주자 목록 조회
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
        public static DataSet SpreadRentUserInfo(int intPageSize, int intNowPage, string strRentCd, string strNm, string strRoomNo,
                                                 string strRentStartDt, string strRentEndDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ResidentMngDao.SelectRentUserInfo(intPageSize, intNowPage, strRentCd, strNm, strRoomNo, strRentStartDt, strRentEndDt);

            return dsReturn;
        }

        #endregion

        #region SpreadRentExcelUserInfo : 엑셀용 오피스 및 리테일 입주자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRentExcelUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-15
         * 용       도 : 엑셀용 오피스 및 리테일 입주자 목록 조회
         * Input    값 : SpreadRentExcelUserInfo(임대구분코드, 검색대상명, 검색층, 검색호, 검색입주시작일, 검색입주종료일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadRentExcelUserInfo : 엑셀용 오피스 및 리테일 입주자 목록 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strNm">검색대상명</param>
        /// <param name="strRoomNo">검색호</param>
        /// <param name="strRentStartDt">검색입주시작일</param>
        /// <param name="strRentEndDt">검색입주종료일</param>
        /// <returns></returns>
        public static DataTable SpreadRentExcelUserInfo(string strRentCd, string strNm, string strRoomNo, string strRentStartDt, string strRentEndDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ResidentMngDao.SelectRentExcelUserInfo(strRentCd, strNm, strRoomNo, strRentStartDt, strRentEndDt);

            return dtReturn;
        }

        #endregion

        #region SpreadUserTmpInfo : 입주자 관련 임시정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadUserTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 관련 임시정보 조회
         * Input    값 : SpreadUserTmpInfo(입주자 임시순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadUserTmpInfo : 입주자 관련 임시정보 조회
        /// </summary>
        /// <param name="intUserSeq">입주자 임시순번</param>
        /// <returns></returns>
        public static DataSet SpreadUserTmpInfo(int intUserSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ResidentMngDao.SelectUserTmpInfo(intUserSeq);

            return dsReturn;
        }

        #endregion

        #region WatchSalesUserView : 아파트 및 아파트 상가 입주자 상세 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchSalesUserView
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-14
         * 용       도 : 아파트 및 아파트 상가 입주자 상세 정보 조회
         * Input    값 : WatchSalesUserView(입주자코드, 임대구분코드, 임대구분상세순번, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchSalesUserView : 아파트 및 아파트 상가 입주자 상세 정보 조회
        /// </summary>
        /// <param name="strUserSeq">입주자코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분상세순번</param>
        /// <returns></returns>
        public static DataSet WatchSalesUserView(string strUserSeq, string strRentCd, int intRentSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ResidentMngDao.SelectSalesUserView(strUserSeq, strRentCd, intRentSeq, strLangCd);

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

            dsReturn = ResidentMngDao.SelectRentSeqUserInfo(strUserSeq, strRentCd, intRentSeq);

            return dsReturn;
        }

        #endregion

        #region WatchRentUserView : 오피스 및 리테일 입주자 상세 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchRentUserView
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-31
         * 용       도 : 오피스 및 리테일 입주자 상세 정보 조회
         * Input    값 : WatchRentUserView(입주자코드, 임대구분코드, 임대구분상세순번, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchRentUserView : 오피스 및 리테일 입주자 상세 정보 조회
        /// </summary>
        /// <param name="strUserSeq">입주자코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분상세순번</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet WatchRentUserView(string strUserSeq, string strRentCd, int intRentSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ResidentMngDao.SelectRentUserView(strUserSeq, strRentCd, intRentSeq, strLangCd);

            return dsReturn;
        }

        #endregion

        #region WatchOccpantInfo : 아파트 및 아파트 상가 입주자 기존 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchOccpantInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 아파트 및 아파트 상가 입주자 기존 정보 조회
         * Input    값 : WatchOccpantInfo(임대구분코드, 임대구분순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchOccpantInfo : 아파트 및 아파트 상가 입주자 기존 정보 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분순번</param>
        /// <returns></returns>
        public static DataTable WatchOccpantInfo(string strRentCd, int intRentSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ResidentMngDao.SelectOccpantInfo(strRentCd, intRentSeq);

            return dtReturn;
        }

        #endregion

        #region WatchTenantInfo : 오피스 및 리테일 입주자 기존 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchTenantInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 오피스 및 리테일 입주자 기존 정보 조회
         * Input    값 : WatchTenantInfo(임대구분코드, 임대구분순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchTenantInfo : 오피스 및 리테일 입주자 기존 정보 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대구분순번</param>
        /// <returns></returns>
        public static DataTable WatchTenantInfo(string strRentCd, int intRentSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ResidentMngDao.SelectTenantInfo(strRentCd, intRentSeq);

            return dtReturn;
        }

        #endregion

        #region RegistryTempUserInfo : 추가 정보 관리를 위한 임시정보 등록후 임시번호 리턴

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 추가 정보 관리를 위한 임시정보 등록후 임시번호 리턴
         * Input    값 : RegistryTempUserInfo(회사코드, 등록사원번호, 접근IP정보)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempUserInfo : 추가 정보 관리를 위한 임시정보 등록후 임시번호 리턴
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">등록사원번호</param>
        /// <param name="strMemIP">접근IP정보</param>
        /// <returns></returns>
        public static DataTable RegistryTempUserInfo(string strCompNo, string strMemNo, string strMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ResidentMngDao.InsertTempUserInfo(strCompNo, strMemNo, strMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryUserAddonTmpInfo : 입주자 관련 동거인 임시정보 수정

        /**********************************************************************************************
         * Mehtod   명 : RegistryUserAddonTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-12
         * 용       도 : 입주자 관련 동거인 임시정보 수정
         * Input    값 : RegistryUserAddonTmpInfo(임시순번, 동거인명, 성별, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUserAddonTmpInfo : 입주자 관련 동거인 임시정보 수정
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="strAccessNm">동거인명</param>
        /// <param name="strGender">성별</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] RegistryUserAddonTmpInfo(int intUserSeq, string strAccessNm, string strGender, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.InsertUserAddonTmpInfo(intUserSeq, strAccessNm, strGender, strRelationCd, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryUserAddonInfo : 입주자 관련 동거인 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : RegistryUserAddonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 관련 동거인 정보 저장
         * Input    값 : RegistryUserAddonInfo(입주자순번, 동거인명, 성별, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUserAddonInfo : 입주자 관련 동거인 정보 저장
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="strAccessNm">동거인명</param>
        /// <param name="strGender">성별</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] RegistryUserAddonInfo(string strUserSeq, string strAccessNm, string strGender, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.InsertUserAddonInfo(strUserSeq, strAccessNm, strGender, strRelationCd, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryUserAccessTmpInfo : 입주자 카드 임시 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : RegistryUserAccessTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 카드 임시 정보 저장
         * Input    값 : RegistryUserAccessTmpInfo(임시순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUserAccessTmpInfo : 입주자 카드 임시 정보 저장
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="strAccessCardNo">카드번호</param>
        /// <param name="strAccessNm">접속자명</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] RegistryUserAccessTmpInfo(int intUserSeq, string strAccessCardNo, string strAccessNm, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.InsertUserAccessTmpInfo(intUserSeq, strAccessCardNo, strAccessNm, strRelationCd, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryUserAccessInfo : 입주자 카드 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : RegistryUserAccessInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 카드 정보 저장
         * Input    값 : RegistryUserAccessInfo(입주자순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUserAccessInfo : 입주자 카드 정보 저장
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="strAccessCardNo">카드번호</param>
        /// <param name="strAccessNm">접속자명</param>
        /// <param name="strRelationCd">임대자와의 관계코드</param>
        /// <param name="strInsCompNo">입력회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] RegistryUserAccessInfo(string strUserSeq, string strAccessCardNo, string strAccessNm, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.InsertUserAccessInfo(strUserSeq, strAccessCardNo, strAccessNm, strRelationCd, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryUserMng : 입주자 관련 정보 저장

        /**********************************************************************************************
         * Mehtod   명 : RegistryUserMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 카드 임시 정보 저장
         * Input    값 : RegistryUserMng(임시순번, 층번호, 방번호, 임대구분코드, 임대구분순번, 입주일, 생일, 성별, 전화번호지역번호, 전화번호국번호, 
         *                             핸드폰기지국번호, 핸드폰국번호, 핸드폰식별번호, 등록사번, 등록IP)
         * )
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUserMng : 입주자 관련 정보 저장
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
        /// <returns></returns>
        public static object[] RegistryUserMng(int intTmpUserSeq, int intFloor, string strRoomNo, string strRentCd, int intRentSeq, string strUserNm, string strOccupationDt,
                                               string strBirthDt, string strGender, string strTelTyCd, string strTelFrontNo, string strTelRearNo, string strMobileTyCd,
                                               string strMobileFrontNo, string strMobileRearNo, string strUserTaxCd, string strUserAddr, string strUserDetAddr,
                                               string strInsCompNo, string strInsMemNo, string strInsMemIP,string KsysCd)
        {
            var objReturn = ResidentMngDao.InsertUserMng(intTmpUserSeq, intFloor, strRoomNo, strRentCd, intRentSeq, strUserNm, strOccupationDt, strBirthDt,
                                                              strGender, strTelTyCd, strTelFrontNo, strTelRearNo, strMobileTyCd, strMobileFrontNo, strMobileRearNo,
                                                              strUserTaxCd, strUserAddr, strUserDetAddr, strInsCompNo, strInsMemNo, strInsMemIP,KsysCd);
            return objReturn;
        }

        #endregion

        #region RegistryUserKSysMatchInfo: KSystem 고객번호 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryUserKSysMatchInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-03
         * 용       도 : KSystem 고객번호 등록
         * Input    값 : RegistryUserKSysMatchInfo(사용자 번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryUserKSysMatchInfo: KSystem 고객번호 등록
        /// </summary>
        /// <param name="strUserNo">사용자 번호</param>
        /// <returns></returns>
        public static object[] RegistryUserKSysMatchInfo(string strUserNo)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.InsertUserKSysMatchInfo(strUserNo);

            return objReturn;
        }

        #endregion

        #region ModifyUserAddonTmpInfo : 입주자 관련 동거인 임시정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyUserAddonTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-12
         * 용       도 : 입주자 관련 동거인 임시정보 수정
         * Input    값 : ModifyUserAddonTmpInfo(임시순번, 임시상세순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyUserAddonTmpInfo : 입주자 관련 동거인 임시정보 수정
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
        public static object[] ModifyUserAddonTmpInfo(int intUserSeq, int intUserDetSeq, string strAccessNm, string strGender, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.UpdateUserAddonTmpInfo(intUserSeq, intUserDetSeq, strAccessNm, strGender, strRelationCd, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyUserAddonInfo : 입주자 관련 동거인 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyUserAddonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 관련 동거인 정보 수정
         * Input    값 : ModifyUserAddonInfo(입주자번호, 입주자상세순번, 동거인명, 성별, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyUserAddonInfo : 입주자 관련 동거인 정보 수정
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
        public static object[] ModifyUserAddonInfo(string strUserSeq, int intUserDetSeq, string strAccessNm, string strGender, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.UpdateUserAddonInfo(strUserSeq, intUserDetSeq, strAccessNm, strGender, strRelationCd, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyUserAccessTmpInfo : 입주자 관련 카드 임시정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyUserAccessTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 관련 카드 임시정보 수정
         * Input    값 : ModifyUserAccessTmpInfo(임시순번, 임시상세순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyUserAccessTmpInfo : 입주자 관련 카드 임시정보 수정
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
        public static object[] ModifyUserAccessTmpInfo(int intUserSeq, int intUserDetSeq, string strAccessCardNo, string strAccessNm, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.UpdateUserAccessTmpInfo(intUserSeq, intUserDetSeq, strAccessCardNo, strAccessNm, strRelationCd, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyUserAccessInfo : 입주자 관련 카드 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyUserAccessInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 관련 카드 정보 수정
         * Input    값 : ModifyUserAccessInfo(입주자번호, 입주자상세순번, 카드번호, 접속자명, 임대자와의 관계코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyUserAccessInfo : 입주자 관련 카드 정보 수정
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
        public static object[] ModifyUserAccessInfo(string strUserSeq, int intUserDetSeq, string strAccessCardNo, string strAccessNm, string strRelationCd, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.UpdateUserAccessInfo(strUserSeq, intUserDetSeq, strAccessCardNo, strAccessNm, strRelationCd, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyUserInfo : 입주자 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 정보 수정
         * Input    값 : UpdateUserInfo(입주자번호, 층번호, 방번호, 임대구분코드, 임대구분순번, 입주일, 생일, 성별, 전화번호지역번호, 전화번호국번호, 
         *                             핸드폰기지국번호, 핸드폰국번호, 핸드폰식별번호, 등록사번, 등록IP)
         * )
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// ModifyUserInfo : 입주자 정보 수정
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
        public static object[] ModifyUserInfo(string strUserSeq, int intFloor, string strRoomNo, string strRentCd, int intRentSeq, string strUserNm, string strOccupationDt,
                                              string strBirthDt, string strGender, string strTelTyCd, string strTelFrontNo, string strTelRearNo, string strMobileTyCd,
                                              string strMobileFrontNo, string strMobileRearNo, string strUserTaxCd, string strUserAddr, string strUserDetAddr,
                                              string strInsCompNo, string strInsMemNo, string strInsMemIP, string ksysCd)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.UpdateUserInfo(strUserSeq, intFloor, strRoomNo, strRentCd, intRentSeq, strUserNm, strOccupationDt, strBirthDt, strGender, strTelTyCd,
                                                      strTelFrontNo, strTelRearNo, strMobileTyCd, strMobileFrontNo, strMobileRearNo, strUserTaxCd, strUserAddr, strUserDetAddr,
                                                      strInsCompNo, strInsMemNo, strInsMemIP,ksysCd);

            return objReturn;
        }

        #endregion

        #region RemoveTempUserInfo : 2일이 지난 입주자 임시정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 2일이 지난 입주자 임시정보 삭제
         * Input    값 : RemoveTempUserInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempUserInfo : 2일이 지난 입주자 임시정보 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveTempUserInfo()
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.DeleteTempUserInfo();

            return objReturn;
        }

        #endregion

        #region RemoveUserAddonTmpInfo : 입주자 관련 동거인 임시정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveUserAddonTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-12
         * 용       도 : 입주자 관련 동거인 임시정보 삭제
         * Input    값 : RemoveUserAddonTmpInfo(임시순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveUserAddonTmpInfo : 입주자 관련 동거인 임시정보 삭제
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] RemoveUserAddonTmpInfo(int intUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.DeleteUserAddonTmpInfo(intUserSeq, intUserDetSeq);

            return objReturn;
        }

        #endregion

        #region RemoveUserAddonInfo : 입주자 관련 동거인 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveUserAddonInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 관련 동거인 정보 삭제
         * Input    값 : RemoveUserAddonInfo(입주자순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveUserAddonInfo : 입주자 관련 동거인 정보 삭제
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] RemoveUserAddonInfo(string strUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.DeleteUserAddonInfo(strUserSeq, intUserDetSeq);

            return objReturn;
        }

        #endregion

        #region RemoveUserAccessTmpInfo : 입주자 관련 카드 임시정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveUserAccessTmpInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-11
         * 용       도 : 입주자 관련 카드 임시정보 삭제
         * Input    값 : RemoveUserAccessTmpInfo(임시순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveUserAccessTmpInfo : 입주자 관련 카드 임시정보 삭제
        /// </summary>
        /// <param name="intUserSeq">임시순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] RemoveUserAccessTmpInfo(int intUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.DeleteUserAccessTmpInfo(intUserSeq, intUserDetSeq);

            return objReturn;
        }

        #endregion

        #region RemoveUserAccessInfo : 입주자 관련 카드 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveUserAccessInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 입주자 관련 카드 임시정보 삭제
         * Input    값 : RemoveUserAccessInfo(입주자순번, 상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveUserAccessInfo : 입주자 관련 카드 정보 삭제
        /// </summary>
        /// <param name="strUserSeq">입주자순번</param>
        /// <param name="intUserDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] RemoveUserAccessInfo(string strUserSeq, int intUserDetSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.DeleteUserAccessInfo(strUserSeq, intUserDetSeq);

            return objReturn;
        }

        #endregion

        #region RemoveUserInfo : 입주자 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveUserInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-14
         * 용       도 : 입주자 삭제
         * Input    값 : RemoveUserInfo(입주자번호, 등록사번, 등록IP)
         * )
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyUserInfo : 입주자 정보 수정
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strUserSeq">입주자번호</param>      
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <param name="strRentCd">언어코드</param>
        /// <returns></returns>
        public static object[] RemoveUserInfo(string strUserSeq, string strCompNo, string strInsMemNo, string strInsMemIP, string strRentCd)
        {
            object[] objReturn = new object[2];

            objReturn = ResidentMngDao.DeleteUserInfo(strUserSeq, strCompNo, strInsMemNo, strInsMemIP, strRentCd);

            return objReturn;
        }

        #endregion
    }
}
