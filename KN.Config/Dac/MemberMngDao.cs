using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

using KN.Config.Ent;

namespace KN.Config.Dac
{
    /// <summary>
    /// 사원 및 운영자 관련
    /// </summary>
    public class MemberMngDao
    {
        #region SelectMemInfo : 회원정보 조회 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : SelectMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-24
         * 용       도 : 회원정보 조회 (계정관리)
         * Input    값 : SelectMemInfo(한페이지당 리스트 수, 현재페이지번호, 검색코드, 검색어, 접근권한, 권한그룹코드, 사원번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 회원정보 조회 (계정관리)
        /// </summary>
        /// <param name="intPageSize">한페이지당 리스트 수</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strAuthTy">권한그룹코드</param>
        /// <param name="strModCompCd">회사코드</param>
        /// <param name="strModMemNo">사원번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectMemInfo(int intPageSize, int intNowPage, string strKeyCd, string strKeyWord, string strAccessAuth,
                                            string strAuthTy, string strModCompCd, string strModMemNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();
            object[] objParam = new object[9];

            objParam[0] = intPageSize;
            objParam[1] = intNowPage;
            objParam[2] = strKeyCd;
            objParam[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strKeyWord));
            objParam[4] = strAuthTy;
            objParam[5] = strAccessAuth;
            objParam[6] = strModCompCd;
            objParam[7] = strModMemNo;
            objParam[8] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_COMM_SELECT_MEMINFO_S01", objParam);

            return dsReturn;
        }

        #endregion

        #region SelectUserID : 중복ID 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectUserID 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 중복ID 정보 조회
         * Input    값 : SelectUserID(사용자ID)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectUserID : 중복ID 정보 조회
        /// </summary>
        /// <param name="strUserID">사용자ID</param>
        /// <returns></returns>
        public static DataTable SelectUserID(string strCompCd, string strUserID)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[2];

            objParam[0] = strCompCd;
            objParam[1] = strUserID;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S02", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMemInfo : 회원정보 상세조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원정보 상세조회
         * Input    값 : SelectMemInfo(조회대상사번, 접근사번, 접근권한, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMemInfo : 회원정보 상세조회
        /// </summary>
        /// <param name="strCompCd">조회대상회사코드</param>
        /// <param name="strSearchMemNo">조회대상사번</param>
        /// <param name="strAccMemNo">접근사번</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectMemInfo(string strCompCd, string strSearchMemNo, string strAccMemNo, string strAccessAuth, string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[5];

            objParam[0] = strCompCd;
            objParam[1] = strSearchMemNo;
            objParam[2] = strAccMemNo;
            objParam[3] = strAccessAuth;
            objParam[4] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S03", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectKsysChestNutMemInfo : KSYSTEM에 등록된 회원정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectKsysChestNutMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-06-03
         * 용       도 : KSYSTEM에 등록된 회원정보 조회
         * Input    값 : SelectKsysChestNutMemInfo(사번, 사원명, 주소, 전화번호, 핸드폰번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectKsysChestNutMemInfo : KSYSTEM에 등록된 회원정보 조회
        /// </summary>
        /// <param name="strMemNo">사번</param>
        /// <param name="strMemNm">사원명</param>
        /// <param name="strAddr">주소</param>
        /// <param name="strTelNo">전화번호</param>
        /// <param name="strMobileNo">핸드폰번호</param>
        /// <returns></returns>
        public static DataTable SelectKsysChestNutMemInfo(string strMemNo, string strMemNm, string strAddr, string strTelNo, string strMobileNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[5];

            objParam[0] = strMemNo;
            objParam[1] = strMemNm;
            objParam[2] = strAddr;
            objParam[3] = strTelNo;
            objParam[4] = strMobileNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S08", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectKsysKeangNamMemInfo : KSYSTEM 경남비나에 등록된 회원정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectKsysKeangNamMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-06
         * 용       도 : KSYSTEM 경남비나에 등록된 회원정보 조회
         * Input    값 : SelectKsysKeangNamMemInfo(사번, 사원명, 주소, 전화번호, 핸드폰번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectKsysKeangNamMemInfo : KSYSTEM에 등록된 회원정보 조회
        /// </summary>
        /// <param name="strMemNo">사번</param>
        /// <param name="strMemNm">사원명</param>
        /// <param name="strAddr">주소</param>
        /// <param name="strTelNo">전화번호</param>
        /// <param name="strMobileNo">핸드폰번호</param>
        /// <returns></returns>
        public static DataTable SelectKsysKeangNamMemInfo(string strMemNo, string strMemNm, string strAddr, string strTelNo, string strMobileNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[5];

            objParam[0] = strMemNo;
            objParam[1] = strMemNm;
            objParam[2] = strAddr;
            objParam[3] = strTelNo;
            objParam[4] = strMobileNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S09", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMemIDByEmail : E-Mail로 ID찾기

        /**********************************************************************************************
         * Mehtod   명 : SelectMemIDByEmail 
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-08
         * 용       도 : E-Mail로 ID찾기
         * Input    값 : SelectMemIDByEmail(사용자이름, 사용자E-Mail)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// E-Mail로 ID찾기
        /// </summary>
        /// <param name="strEmailId">사용자E-Mail</param>
        /// <param name="strEmailServer">사용자E-Mail Server</param>
        /// <param name="strMemNm">사용자이름</param>
        /// <returns></returns>
        public static DataTable SelectMemIDByEmail(string strEmailId, string strEmailServer, string strMemNm)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[3];

            objParam[0] = strEmailId;
            objParam[1] = strEmailServer;
            objParam[2] = strMemNm;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S04", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMemIDByMobile : 휴대전화로 ID찾기

        /**********************************************************************************************
         * Mehtod   명 : SelectMemIDByMobile 
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-08
         * 용       도 : 휴대전화로로 ID찾기
         * Input    값 : SelectMemIDByMobile(사용자이름, 기지국번호코드, 모바일국번호, 개인식별번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMemNm"></param>
        /// <param name="strMobileTypeCd"></param>
        /// <param name="strMobileFrontNo"></param>
        /// <param name="strMobileRearNo"></param>
        /// <returns></returns>
        public static DataTable SelectMemIDByMobile(string strMemNm, string strMobileTypeCd, string strMobileFrontNo, string strMobileRearNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[4];

            objParam[0] = strMemNm;
            objParam[1] = strMobileTypeCd;
            objParam[2] = strMobileFrontNo;
            objParam[3] = strMobileRearNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S05", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMemPwdByEmail : E-Mail로 Password찾기

        /**********************************************************************************************
         * Mehtod   명 : SelectMemPwdByEmail 
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-09
         * 용       도 : E-Mail로 Password찾기
         * Input    값 : SelectMemPwdByEmail(사용자ID, 사용자E-Mail, 사용자이름)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// E-Mail로 Password찾기
        /// </summary>
        /// <param name="strUserId">사용자ID</param>
        /// <param name="strEmailId">사용자E-Mail</param>
        /// <param name="strEmailServer">사용자E-Mail</param>
        /// <param name="strMemNm">사용자이름</param>
        /// <returns></returns>
        public static DataTable SelectMemPwdByEmail(string strUserId, string strEmailId, string strEmailServer, string strMemNm)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[4];

            objParam[0] = strUserId;
            objParam[1] = strEmailId;
            objParam[2] = strEmailServer;
            objParam[3] = strMemNm;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S06", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMemPwdByMobile : 휴대전화로 Password찾기

        /**********************************************************************************************
         * Mehtod   명 : SelectMemPwdByMobile 
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-09
         * 용       도 : 휴대전화로 Password찾기
         * Input    값 : SelectMemIDByMobile(사용자이름, 기지국번호코드, 모바일국번호, 개인식별번호, 사용자ID)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 휴대전화로 Password찾기
        /// </summary>
        /// <param name="strMemNm">사용자이름</param>
        /// <param name="strMobileTypeCd">기지국번호코드</param>
        /// <param name="strMobileFrontNo">모바일국번호</param>
        /// <param name="strMobileRearNo">개인식별번호</param>
        /// <param name="strUserId">사용자ID</param>
        /// <returns></returns>
        public static DataTable SelectMemPwdByMobile(string strUserId, string strMemNm, string strMobileTypeCd, string strMobileFrontNo, string strMobileRearNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[5];

            objParam[0] = strUserId;
            objParam[1] = strMemNm;
            objParam[2] = strMobileTypeCd;
            objParam[3] = strMobileFrontNo;
            objParam[4] = strMobileRearNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMINFO_S07", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertMemInfo : 회원정보 등록 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : InsertMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-24
         * 용       도 : 회원정보 등록 (계정관리)
         * Input    값 : InsertMemInfo(회원정보객체)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertMemInfo : 회원정보 등록 (계정관리)
        /// </summary>
        /// <param name="msInfo">회원정보객체</param>
        /// <returns></returns>
        public static DataTable InsertMemInfo(MemberMngDs.MemberInfo msInfo)
        {
            object[] objParam = new object[13];
            DataTable dtReturn = new DataTable();

            objParam[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.CompNo));
            objParam[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.MemNo));
            objParam[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.UserId));
            objParam[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.Pwd));
            objParam[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.MemNm));
            objParam[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.MemAuthTy));
            objParam[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.MemAccAuthTy));
            objParam[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.FMSAuthYn));
            objParam[8] = TextLib.MakeNullToEmpty(msInfo.EnterDt);
            objParam[9] = TextLib.MakeNullToEmpty(msInfo.RetireDt);
            objParam[10] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.KNNo));
            objParam[11] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.InsCompNo));
            objParam[12] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.InsMemNo));

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_INSERT_MEMINFO_S00", objParam);

            return dtReturn;
        }

        #endregion
        
        #region InsertMemAddon : 회원추가정보 등록 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : InsertMemAddon 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원추가정보 등록 (계정관리)
         * Input    값 : InsertMemAddon(회원추가정보객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMemAddon : 회원추가정보 등록 (계정관리)
        /// </summary>
        /// <param name="msAddInfo">회원추가정보객체</param>
        /// <returns></returns>
        public static object[] InsertMemAddon(MemberMngDs.MemberAddon msAddInfo)
        {
            object[] objParam = new object[12];
            object[] objReturn = new object[2];

            objParam[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.CompNo));
            objParam[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.MemNo));
            objParam[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.TelTypeCd));
            objParam[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.TelFrontNo));
            objParam[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.TelRearNo));
            objParam[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.MobileTypeCd));
            objParam[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.MobileFrontNo));
            objParam[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.MobileRearNo));
            objParam[8] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.Addr));
            objParam[9] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.DetAddress));
            objParam[10] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.EmailId));
            objParam[11] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.EmailServer));

            objReturn = SPExecute.ExecReturnNo("KN_USP_COMM_INSERT_MEMADDON_M00", objParam);

            return objReturn;
        }

        #endregion

        #region UpdateMemInfo : 회원정보 수정 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : UpdateMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원정보 수정 (계정관리)
         * Input    값 : UpdateMemInfo(회원정보객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMemInfo : 회원정보 수정 (계정관리)
        /// </summary>
        /// <param name="msInfo">회원정보객체</param>
        /// <returns></returns>
        public static object[] UpdateMemInfo(MemberMngDs.MemberInfo msInfo)
        {
            object[] objParam = new object[13];
            object[] objReturn = new object[2];

            objParam[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.CompNo));
            objParam[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.MemNo));
            objParam[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.UserId));
            objParam[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.Pwd));
            objParam[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.MemNm));
            objParam[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.MemAuthTy));
            objParam[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.MemAccAuthTy));
            objParam[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.FMSAuthYn));
            objParam[8] = TextLib.MakeNullToEmpty(msInfo.EnterDt);
            objParam[9] = TextLib.MakeNullToEmpty(msInfo.RetireDt);
            objParam[10] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.KNNo));
            objParam[11] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.InsCompNo));
            objParam[12] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msInfo.InsMemNo));

            objReturn = SPExecute.ExecReturnNo("KN_USP_COMM_UPDATE_MEMINFO_M00", objParam);

            return objReturn;
        }

        #endregion

        #region UpdateMemAddon : 회원추가정보 수정 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : UpdateMemAddon 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원추가정보 수정 (계정관리)
         * Input    값 : UpdateMemAddon(회원추가정보객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMemAddon : 회원추가정보 수정 (계정관리)
        /// </summary>
        /// <param name="msAddInfo">회원추가정보객체</param>
        /// <returns></returns>
        public static object[] UpdateMemAddon(MemberMngDs.MemberAddon msAddInfo)
        {
            object[] objParam = new object[12];
            object[] objReturn = new object[2];

            objParam[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.CompNo));
            objParam[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.MemNo));
            objParam[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.TelTypeCd));
            objParam[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.TelFrontNo));
            objParam[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.TelRearNo));
            objParam[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.MobileTypeCd));
            objParam[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.MobileFrontNo));
            objParam[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.MobileRearNo));
            objParam[8] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.Addr));
            objParam[9] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.DetAddress));
            objParam[10] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.EmailId));
            objParam[11] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(msAddInfo.EmailServer));

            objReturn = SPExecute.ExecReturnNo("KN_USP_COMM_UPDATE_MEMADDON_M00", objParam);

            return objReturn;
        }

        #endregion

        #region DeleteMemInfo : 회원정보 삭제 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : DeleteMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원정보 삭제 (계정관리)
         * Input    값 : DeleteMemInfo(회사코드, 사번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteMemInfo : 회원정보 삭제 (계정관리)
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strMemNo">사번</param>
        /// <returns></returns>
        public static object[] DeleteMemInfo(string strCompCd, string strMemNo)
        {
            object[] objParam = new object[2];
            object[] objReturn = new object[2];

            objParam[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strCompCd));
            objParam[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMemNo));

            objReturn = SPExecute.ExecReturnNo("KN_USP_COMM_DELETE_MEMINFO_M00", objParam);

            return objReturn;
        }

        #endregion

        #region DeleteMemAddon : 회원추가정보 삭제 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : DeleteMemAddon 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원추가정보 삭제 (계정관리)
         * Input    값 : DeleteMemAddon(회사코드, 사번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteMemAddon : 회원추가정보 삭제 (계정관리)
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strMemNo">사번</param>
        /// <returns></returns>
        public static object[] DeleteMemAddon(string strCompCd, string strMemNo)
        {
            object[] objParam = new object[2];
            object[] objReturn = new object[2];

            objParam[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strCompCd));
            objParam[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMemNo));

            objReturn = SPExecute.ExecReturnNo("KN_USP_COMM_DELETE_MEMADDON_M00", objParam);

            return objReturn;
        }

        #endregion
    }
}
