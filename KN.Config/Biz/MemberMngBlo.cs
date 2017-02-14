using System.Data;

using KN.Config.Dac;
using KN.Config.Ent;

namespace KN.Config.Biz
{
    /// <summary>
    /// 사원 및 운영자 관련
    /// </summary>
    public class MemberMngBlo
    {
        #region SpreadMemInfo : 회원정보 조회 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : SpreadMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-24
         * 용       도 : 회원정보 조회 (계정관리)
         * Input    값 : SpreadMemInfo(한페이지당 리스트 수, 현재페이지번호, 검색코드, 검색어, 접근권한, 권한그룹코드, 회사코드, 사원번호, 언어코드)
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
        public static DataSet SpreadMemInfo(int intPageSize, int intNowPage, string strKeyCd, string strKeyWord, string strAccessAuth,
                                            string strAuthTy, string strModCompCd, string strModMemNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MemberMngDao.SelectMemInfo(intPageSize, intNowPage, strKeyCd, strKeyWord, strAccessAuth, strAuthTy, strModCompCd, strModMemNo, strLangCd);

            return dsReturn;
        }

        #endregion

        #region WatchUserID : 중복ID 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchUserID 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 중복ID 정보 조회
         * Input    값 : WatchUserID(사용자ID)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchUserID : 중복ID 정보 조회
        /// </summary>
        /// <param name="strUserID">사용자ID</param>
        /// <returns></returns>
        public static DataTable WatchUserID(string strCompCd, string strUserID)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemberMngDao.SelectUserID(strCompCd, strUserID);

            return dtReturn;
        }

        #endregion

        #region WatchMemInfo : 회원정보 상세조회

        /**********************************************************************************************
         * Mehtod   명 : WatchMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원정보 상세조회
         * Input    값 : WatchMemInfo(조회대상사번, 접근사번, 접근권한, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchMemInfo : 회원정보 상세조회
        /// </summary>
        /// <param name="strCompCd">조회대상회사코드</param>
        /// <param name="strSearchMemNo">조회대상사번</param>
        /// <param name="strAccMemNo">접근사번</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable WatchMemInfo(string strCompCd, string strSearchMemNo, string strAccMemNo, string strAccessAuth, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemberMngDao.SelectMemInfo(strCompCd, strSearchMemNo, strAccMemNo, strAccessAuth, strLangCd);

            return dtReturn;
        }

        #endregion

        #region SpreadKsysChestNutMemInfo : KSYSTEM에 등록된 회원정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadKsysChestNutMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-06-03
         * 용       도 : KSYSTEM에 등록된 회원정보 조회
         * Input    값 : SpreadKsysChestNutMemInfo(사번, 사원명, 주소, 전화번호, 핸드폰번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadKsysChestNutMemInfo : KSYSTEM에 등록된 회원정보 조회
        /// </summary>
        /// <param name="strMemNo">사번</param>
        /// <param name="strMemNm">사원명</param>
        /// <param name="strAddr">주소</param>
        /// <param name="strTelNo">전화번호</param>
        /// <param name="strMobileNo">핸드폰번호</param>
        /// <returns></returns>
        public static DataTable SpreadKsysChestNutMemInfo(string strMemNo, string strMemNm, string strAddr, string strTelNo, string strMobileNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemberMngDao.SelectKsysChestNutMemInfo(strMemNo, strMemNm, strAddr, strTelNo, strMobileNo);

            return dtReturn;
        }

        #endregion

        #region SpreadKsysKeangNamMemInfo : KSYSTEM 경남비나에 등록된 회원정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadKsysKeangNamMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-06
         * 용       도 : KSYSTEM 경남비나에 등록된 회원정보 조회
         * Input    값 : SpreadKsysKeangNamMemInfo(사번, 사원명, 주소, 전화번호, 핸드폰번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadKsysKeangNamMemInfo : KSYSTEM 경남비나에 등록된 회원정보 조회
        /// </summary>
        /// <param name="strMemNo">사번</param>
        /// <param name="strMemNm">사원명</param>
        /// <param name="strAddr">주소</param>
        /// <param name="strTelNo">전화번호</param>
        /// <param name="strMobileNo">핸드폰번호</param>
        /// <returns></returns>
        public static DataTable SpreadKsysKeangNamMemInfo(string strMemNo, string strMemNm, string strAddr, string strTelNo, string strMobileNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemberMngDao.SelectKsysKeangNamMemInfo(strMemNo, strMemNm, strAddr, strTelNo, strMobileNo);

            return dtReturn;
        }

        #endregion

        #region WatchMemIDByEmail : E-Mail로 ID 찾기

        /**********************************************************************************************
         * Mehtod   명 : WatchMemIDByEmail 
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-08
         * 용       도 : E-Mail로 ID 찾기
         * Input    값 : WatchUserID(사용자이름, 사용자Email, 사용자EmailServer)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// E-Mail로 ID 찾기
        /// </summary>
        /// <param name="strMemNm">사용자이름</param>
        /// <param name="strEmailId">사용자Email</param>
        /// <param name="strEmailServer">사용자EmailServer</param>
        /// <returns></returns>
        /// 
        public static DataTable WatchMemIDByEmail(string strEmailId, string strEmailServer, string strMemNm)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemberMngDao.SelectMemIDByEmail(strEmailId, strEmailServer, strMemNm);

            return dtReturn;
        }

        #endregion

        #region WatchMemIDByMobile : 휴대전화로 ID 찾기

        /**********************************************************************************************
         * Mehtod   명 : WatchMemIDByMobile 
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-08
         * 용       도 : 휴대전화로 ID 찾기
         * Input    값 : WatchMemIDByMobile(사용자이름, 기지국번호코드, 모바일국번호, 개인식별번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 휴대전화로 ID 찾기
        /// </summary>
        /// <param name="strMemNm">사용자이름</param>
        /// <param name="strMobileTypeCd">기지국번호코드</param>
        /// <param name="strMobileFrontNo">모바일국번호</param>
        /// <param name="strMobileRearNo">개인식별번호</param>
        /// <returns></returns>
        public static DataTable WatchMemIDByMobile(string strMemNm, string strMobileTypeCd, string strMobileFrontNo, string strMobileRearNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemberMngDao.SelectMemIDByMobile(strMemNm, strMobileTypeCd, strMobileFrontNo, strMobileRearNo);

            return dtReturn;
        }

        #endregion

        #region WatchMemPwdByEmail : E-Mail로 Password 찾기

        /**********************************************************************************************
         * Mehtod   명 : WatchMemPwdByEmail 
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-09
         * 용       도 : E-Mail로 ID 찾기
         * Input    값 : WatchUserID(사용자ID, 사용자이름, 사용자Email, 사용자EmailServer)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// E-Mail로 Password 찾기
        /// </summary>
        /// <param name="strUserId">사용자ID</param>
        /// <param name="strEmailId">사용자Email</param>
        /// <param name="strEmailServer">사용자EmailServer</param>
        /// <param name="strMemNm">사용자이름</param>
        /// <returns></returns>
        /// 
        public static DataTable WatchMemPwdByEmail(string strUserId, string strEmailId, string strEmailServer, string strMemNm)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemberMngDao.SelectMemPwdByEmail(strUserId, strEmailId, strEmailServer, strMemNm);

            return dtReturn;
        }

        #endregion

        #region WatchMemPwdByMobile : 휴대전화로 Password 찾기

        /**********************************************************************************************
         * Mehtod   명 : WatchMemPwdByMobile 
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-09-09
         * 용       도 : 휴대전화로 Password 찾기
         * Input    값 : WatchMemIDByMobile(사용자이름, 기지국번호코드, 모바일국번호, 개인식별번호, 사용자ID)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 휴대전화로 ID 찾기
        /// </summary>
        /// <param name="strMemNm">사용자이름</param>
        /// <param name="strMobileTypeCd">기지국번호코드</param>
        /// <param name="strMobileFrontNo">모바일국번호</param>
        /// <param name="strMobileRearNo">개인식별번호</param>
        /// <param name="strUserId">사용자ID</param>
        /// <returns></returns>
        /// 
        public static DataTable WatchMemPwdByMobile(string strUserId, string strMemNm, string strMobileTypeCd, string strMobileFrontNo, string strMobileRearNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemberMngDao.SelectMemPwdByMobile(strUserId, strMemNm, strMobileTypeCd, strMobileFrontNo, strMobileRearNo);

            return dtReturn;
        }

        #endregion

        #region RegistryMemMng : 회원관련정보 등록 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : RegistryMemMng 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원관련정보 등록 (계정관리)
         * Input    값 : RegistryMemMng(회원정보객체, 회원추가정보객체)
         * Ouput    값 : object[]
         * **********************************************************************************************/
        /// <summary>
        /// RegistryMemMng : 회원관련정보 등록 (계정관리)
        /// </summary>
        /// <param name="msInfo">회원정보객체</param>
        /// <param name="msAddInfo">회원추가정보객체</param>
        /// <returns></returns>
        public static object[] RegistryMemMng(MemberMngDs.MemberInfo msInfo, MemberMngDs.MemberAddon msAddInfo)
        {
            object[] objReturn = new object[2];
            DataTable dtReturn = new DataTable();

            // 회원정보 등록
            dtReturn = MemberMngDao.InsertMemInfo(msInfo);

            if (dtReturn != null)
            {
                // 회원추가정보 등록
                if (dtReturn.Rows.Count > 0)
                {
                    msAddInfo.MemNo = dtReturn.Rows[0]["MemNo"].ToString();

                    objReturn = MemberMngDao.InsertMemAddon(msAddInfo);
                }
                else
                {
                    objReturn = null;
                }
            }
            else
            {
                objReturn = null;
            }

            return objReturn;
        }

        #endregion

        #region RegistryMemAddon : 회원추가정보 등록 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : RegistryMemAddon 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원추가정보 등록 (계정관리)
         * Input    값 : RegistryMemAddon(회원추가정보객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMemAddon : 회원추가정보 등록 (계정관리)
        /// </summary>
        /// <param name="msAddInfo">회원추가정보객체</param>
        /// <returns></returns>
        public static object[] RegistryMemAddon(MemberMngDs.MemberAddon msAddInfo)
        {
            object[] objReturn = new object[2];

            objReturn = MemberMngDao.InsertMemAddon(msAddInfo);

            return objReturn;
        }

        #endregion

        #region ModifyMemMng : 회원관련정보 수정 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : ModifyMemMng 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원관련정보 수정 (계정관리)
         * Input    값 : ModifyMemMng(회원정보객체, 회원추가정보객체)
         * Ouput    값 : object[]
         * **********************************************************************************************/
        /// <summary>
        /// ModifyMemMng : 회원관련정보 수정 (계정관리)
        /// </summary>
        /// <param name="msInfo">회원정보객체</param>
        /// <param name="msAddInfo">회원추가정보객체</param>
        /// <returns></returns>
        public static object[] ModifyMemMng(MemberMngDs.MemberInfo msInfo, MemberMngDs.MemberAddon msAddInfo)
        {
            object[] objReturn = new object[2];

            // 회원정보 등록
            objReturn = MemberMngDao.UpdateMemInfo(msInfo);

            if (objReturn != null)
            {
                // 회원추가정보 수정
                objReturn = MemberMngDao.UpdateMemAddon(msAddInfo);
            }
            else
            {
                objReturn = null;
            }

            return objReturn;
        }

        #endregion

        #region ModifyMemInfo : 회원정보 수정 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : ModifyMemInfo 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원정보 수정 (계정관리)
         * Input    값 : ModifyMemInfo(회원정보객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMemInfo : 회원정보 수정 (계정관리)
        /// </summary>
        /// <param name="msInfo">회원정보객체</param>
        /// <returns></returns>
        public static object[] ModifyMemInfo(MemberMngDs.MemberInfo msInfo)
        {
            object[] objReturn = new object[2];

            objReturn = MemberMngDao.UpdateMemInfo(msInfo);

            return objReturn;
        }

        #endregion

        #region ModifyMemAddon : 회원추가정보 수정 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : ModifyMemAddon 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원추가정보 수정 (계정관리)
         * Input    값 : ModifyMemAddon(회원추가정보객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMemAddon : 회원추가정보 수정 (계정관리)
        /// </summary>
        /// <param name="msAddInfo">회원추가정보객체</param>
        /// <returns></returns>
        public static object[] ModifyMemAddon(MemberMngDs.MemberAddon msAddInfo)
        {
            object[] objReturn = new object[2];

            objReturn = MemberMngDao.UpdateMemAddon(msAddInfo);

            return objReturn;
        }

        #endregion
        
        #region RemoveMemMng : 회원정보 삭제 (계정관리)

        /**********************************************************************************************
         * Mehtod   명 : RemoveMemMng 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-25
         * 용       도 : 회원정보 삭제 (계정관리)
         * Input    값 : RemoveMemInfo(회사코드, 사번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveMemMng : 회원정보 삭제 (계정관리)
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strMemNo">사번</param>
        /// <returns></returns>
        public static object[] RemoveMemMng(string strCompCd, string strMemNo)
        {
            object[] objReturn = new object[2];

            // 추가정보 삭제
            objReturn = MemberMngDao.DeleteMemAddon(strCompCd, strMemNo);

            if (objReturn != null)
            {
                // 기본정보 삭제
                objReturn = MemberMngDao.DeleteMemInfo(strCompCd, strMemNo);
            }

            return objReturn;
        }

        #endregion        
    }
}