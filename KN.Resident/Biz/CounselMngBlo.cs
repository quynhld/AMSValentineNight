using System;
using System.Data;

using KN.Resident.Dac;
using KN.Resident.Ent;

namespace KN.Resident.Biz
{
    public class CounselMngBlo
    {
        #region SpreadCounselInfo : 상담 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadCounselInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 조회
         * Input    값 : SpreadCounselInfo(페이지 사이즈, 현재 페이지번호, 상담코드, 검색어 코드, 검색어, 접근권한, 언어코드, 접근회사코드, 사원번호, 주문코드
         *                                 등록시작일, 등록종료일, 지역코드, 산업코드, 임대시작일, 임대종료일)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectCounselInfo : 상담 정보 조회
        /// </summary>
        /// <param name="intPageSize">페이지 사이즈</param>
        /// <param name="intNowPage">현재 페이지번호</param>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="strKeyCd">검색어 코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strCompNo">접근회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strOrderCd">주문코드</param>
        /// <param name="strStartInsDt">등록시작일</param>
        /// <param name="strEndInsDt">등록종료일</param>
        /// <param name="strAreaCd">지역코드</param>
        /// <param name="strIndustryCd">산업코드</param>
        /// <param name="strStartLeaseDt">임대시작일</param>
        /// <param name="strEndLeaseDt">임대종료일</param>
        /// <returns></returns>
        public static DataSet SpreadCounselInfo(int intPageSize, int intNowPage, string strCounselCd, string strKeyCd, string strKeyWord, string strAccessAuth,
                                                string strLangCd, string strCompNo, string strMemNo, string strOrderCd, string strStartInsDt, string strEndInsDt,
                                                string strAreaCd, string strIndustryCd, string strStartLeaseDt, string strEndLeaseDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = CounselMngDao.SelectCounselInfo(intPageSize, intNowPage, strCounselCd, strKeyCd, strKeyWord, strAccessAuth, strLangCd, strCompNo, strMemNo, 
                                                       strOrderCd, strStartInsDt, strEndInsDt, strAreaCd, strIndustryCd, strStartLeaseDt, strEndLeaseDt);

            return dsReturn;
        }

        #endregion

        #region WatchCounselInfo : 상담 정보 상세 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchCounselDetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 상세 조회
         * Input    값 : WatchCounselDetInfo(상담코드, 상담순번, 접근권한, 회사코드, 사원번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectCounselDetInfo : 상담 정보 상세 조회
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet WatchCounselDetInfo(string strCounselCd, int intCounselSeq, string strAccessAuth, string strCompNo, string strMemNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = CounselMngDao.SelectCounselDetInfo(strCounselCd, intCounselSeq, strAccessAuth, strCompNo, strMemNo, strLangCd);

            return dsReturn;
        }

        #endregion

        #region WatchCounselDetailView : 상담 정보 상세 조회 (수정용)

        /**********************************************************************************************
         * Mehtod   명 : WatchCounselDetailView
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-24
         * 용       도 : 상담 정보 상세 조회
         * Input    값 : WatchCounselDetailView(상담코드, 상담순번, 접근권한, 회사코드, 사원번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchCounselDetailView : 상담 정보 상세 조회
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable WatchCounselDetailView(string strCounselCd, int intCounselSeq, string strAccessAuth, string strCompNo, string strMemNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CounselMngDao.SelectCounselDetailView(strCounselCd, intCounselSeq, strAccessAuth, strCompNo, strMemNo, strLangCd);

            return dtReturn;
        }

        #endregion

        #region RegistryCounselMng : 상담 관련 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryCounselMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 관련 정보 등록
         * Input    값 : RegistryCounselMng(CounselMngDs.CounselInfo 객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryCounselMng : 상담 관련 정보 등록
        /// </summary>
        /// <param name="csinfo">CounselDs.CounselInfo 객체</param>
        /// <returns></returns>
        public static object[] RegistryCounselMng(CounselMngDs.CounselInfo csinfo)
        {
            DataTable dtReturn = CounselMngDao.InsertCounselInfo(csinfo);
            object[] objReturn = new object[2];

            int intCounselSeq = 0;

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    intCounselSeq = Int32.Parse(dtReturn.Rows[0]["CounselSeq"].ToString());

                    objReturn = CounselMngDao.InsertCounselAddon(csinfo.CounselCd, intCounselSeq, csinfo.InsCompNo, csinfo.InsMemNo, csinfo.InsMemIP, csinfo.Remark);
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

        #region RegistryCounselAddon : 상담 정보 중 비고 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryCounselAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 중 비고 등록
         * Input    값 : RegistryCounselAddon(상담코드, 상담순번, 회사코드, 등록사번, 등록사원IP, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryCounselAddon : 상담 정보 중 비고 등록
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">등록사원IP</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] RegistryCounselAddon(string strCounselCd, int intCounselSeq, string strInsCompNo, string strInsMemNo, string strInsMemIp, string strRemark)
        {
            object[] objReturn = new object[2];

            objReturn = CounselMngDao.InsertCounselAddon(strCounselCd, intCounselSeq, strInsCompNo, strInsMemNo, strInsMemIp, strRemark);

            return objReturn;
        }

        #endregion

        #region ModifyCounselInfo : 상담 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyCounselInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 수정
         * Input    값 : ModifyCounselInfo(CounselDs.CounselInfo 객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyCounselInfo : 상담 정보 수정
        /// </summary>
        /// <param name="csinfo">CounselDs.CounselInfo 객체</param>
        /// <returns></returns>
        public static object[] ModifyCounselInfo(CounselMngDs.CounselInfo csinfo)
        {
            object[] objReturn = new object[2];

            objReturn = CounselMngDao.UpdateCounselInfo(csinfo);

            if (objReturn != null)
            {
                objReturn = CounselMngDao.UpdateCounselAddon(csinfo.CounselCd, csinfo.CounselSeq, csinfo.ModCompNo, csinfo.ModMemNo, csinfo.ModMemIP, csinfo.Remark);
            }

            return objReturn;
        }

        #endregion

        #region RemoveCounselInfo : 상담 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveCounselInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-24
         * 용       도 : 상담 정보 삭제
         * Input    값 : RemoveCounselInfo(상담코드, 상담순번, 회사코드, 등록사번, 등록사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveCounselInfo : 상담 정보 삭제
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">등록사원IP</param>
        /// <returns></returns>
        public static object[] RemoveCounselInfo(string strCounselCd, int intCounselSeq, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = CounselMngDao.DeleteCounselInfo(strCounselCd, intCounselSeq, strInsCompNo, strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion
    }
}