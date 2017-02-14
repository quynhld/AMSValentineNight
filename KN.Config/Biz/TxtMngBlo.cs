using System.Data;

using KN.Config.Dac;

namespace KN.Config.Biz
{
    public class TxtMngBlo
    {
        #region SpreadAlertInfo : Alert 문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : Alert 문구 조회
         * Input    값 : SpreadAlertInfo(알림문구 타입, 검색코드, 검색어)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadAlertInfo : Alert 문구 조회
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataTable SpreadAlertInfo(string strAlertTy, string strKeyCd, string strKeyWord)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = TxtMngDao.SelectAlertInfo(strAlertTy, strKeyCd, strKeyWord);

            return dtReturn;
        }

        #endregion

        #region WatchExistAlertInfo : Alert 중복 문구 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExistAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : Alert 중복 문구 조회
         * Input    값 : WatchExistAlertInfo(알림문구 타입, 표현코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExistAlertInfo : Alert 중복 문구 조회
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <returns></returns>
        public static DataTable WatchExistAlertInfo(string strAlertTy, string strExpressCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = TxtMngDao.SelectExistAlertInfo(strAlertTy, strExpressCd);

            return dtReturn;
        }

        #endregion

        #region RegistryAlertInfo : Alert 문구 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : Alert 문구 등록
         * Input    값 : RegistryAlertInfo(알림문구 타입, 표현코드, 베트남어, 영어, 한국어)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryAlertInfo : Alert 문구 등록
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strAlertVi">베트남어</param>
        /// <param name="strAlertEn">영어</param>
        /// <param name="strAlertKr">한국어</param>
        /// <returns></returns>
        public static object[] RegistryAlertInfo(string strAlertTy, string strExpressCd, string strAlertVi, string strAlertEn, string strAlertKr)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.InsertAlertInfo(strAlertTy, strExpressCd, strAlertVi, strAlertEn, strAlertKr);

            return objReturn;
        }

        #endregion

        #region ModifyAlertInfo : Alert 문구 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : Alert 문구 수정
         * Input    값 : ModifyAlertInfo(알림문구 타입, 알림문구 순번, 표현코드, 베트남어, 영어, 한국어, 사용여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyAlertInfo : Alert 문구 수정
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strAlertSeq">알림문구 순번</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strAlertVi">베트남어</param>
        /// <param name="strAlertEn">영어</param>
        /// <param name="strAlertKr">한국어</param>
        /// <param name="strUseYn">사용여부</param>
        /// <returns></returns>
        public static object[] ModifyAlertInfo(string strAlertTy, string strAlertSeq, string strExpressCd, string strAlertVi, string strAlertEn, string strAlertKr, string strUseYn)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.UpdateAlertInfo(strAlertTy, strAlertSeq, strExpressCd, strAlertVi, strAlertEn, strAlertKr, strUseYn);

            return objReturn;
        }

        #endregion

        #region RemoveAlertInfo : Alert 문구 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : Alert 문구 삭제
         * Input    값 : RemoveAlertInfo(알림문구 타입, 알림문구 순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveAlertInfo : Alert 문구 삭제
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strAlertSeq">알림문구 순번</param>
        /// <returns></returns>
        public static object[] RemoveAlertInfo(string strAlertTy, string strAlertSeq)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.DeleteAlertInfo(strAlertTy, strAlertSeq);

            return objReturn;
        }

        #endregion

        #region SpreadMenuTxtInfo : 메뉴문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴문구 조회
         * Input    값 : SpreadMenuTxtInfo(검색코드, 검색어)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMenuTxtInfo : 메뉴문구 조회
        /// </summary>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataTable SpreadMenuTxtInfo(string strKeyCd, string strKeyWord)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = TxtMngDao.SelectMenuTxtInfo(strKeyCd, strKeyWord);

            return dtReturn;
        }

        #endregion

        #region WatchExistMenuTxtInfo : 메뉴 중복 문구 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExistMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴 중복 문구 조회
         * Input    값 : WatchExistMenuTxtInfo(표현코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExistMenuTxtInfo : 메뉴 중복 문구 조회
        /// </summary>
        /// <param name="strExpressCd">표현코드</param>
        /// <returns></returns>
        public static DataTable WatchExistMenuTxtInfo(string strExpressCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = TxtMngDao.SelectExistMenuTxtInfo(strExpressCd);

            return dtReturn;
        }

        #endregion

        #region RegistryMenuTxtInfo : 메뉴문구 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴문구 등록
         * Input    값 : RegistryMenuTxtInfo(표현코드, 베트남어, 영어, 한국어)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMenuTxtInfo : 메뉴문구 등록
        /// </summary>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strMenuVi">베트남어</param>
        /// <param name="strMenuEn">영어</param>
        /// <param name="strMenuKr">한국어</param>
        /// <returns></returns>
        public static object[] RegistryMenuTxtInfo(string strExpressCd, string strMenuVi, string strMenuEn, string strMenuKr)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.InsertMenuTxtInfo(strExpressCd, strMenuVi, strMenuEn, strMenuKr);

            return objReturn;
        }

        #endregion

        #region ModifyMenuTxtInfo : 메뉴문구 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴문구 수정
         * Input    값 : ModifyMenuTxtInfo(메뉴문구 순번, 표현코드, 베트남어, 영어, 한국어, 사용여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMenuTxtInfo : 메뉴문구 수정
        /// </summary>
        /// <param name="strMenuTxtSeq">메뉴문구 순번</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strMenuVi">베트남어</param>
        /// <param name="strMenuEn">영어</param>
        /// <param name="strMenuKr">한국어</param>
        /// <param name="strUseYn">사용여부</param>
        /// <returns></returns>
        public static object[] ModifyMenuTxtInfo(string strMenuTxtSeq, string strExpressCd, string strMenuVi, string strMenuEn, string strMenuKr, string strUseYn)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.UpdateMenuTxtInfo(strMenuTxtSeq, strExpressCd, strMenuVi, strMenuEn, strMenuKr, strUseYn);

            return objReturn;
        }

        #endregion

        #region RemoveMenuTxtInfo : 메뉴문구 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴문구 삭제
         * Input    값 : RemoveMenuTxtInfo(메뉴문구 순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveMenuTxtInfo : 메뉴문구 삭제
        /// </summary>
        /// <param name="strMenuTxtSeq">메뉴문구 순번</param>
        /// <returns></returns>
        public static object[] RemoveMenuTxtInfo(string strMenuTxtSeq)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.DeleteMenuTxtInfo(strMenuTxtSeq);

            return objReturn;
        }

        #endregion

        #region SpreadTxtInfo : 항목문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 조회
         * Input    값 : SpreadTxtInfo(항목문구 타입, 검색코드, 검색어)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadTxtInfo : 항목문구 조회
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataTable SpreadTxtInfo(string strTxtTy, string strKeyCd, string strKeyWord)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = TxtMngDao.SelectTxtInfo(strTxtTy, strKeyCd, strKeyWord);

            return dtReturn;
        }

        #endregion

        #region WatchExistTxtInfo : 항목 중복 문구 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExistTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목 중복 문구 조회
         * Input    값 : WatchExistTxtInfo(항목문구 타입, 표현코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExistTxtInfo : 항목 중복 문구 조회
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <returns></returns>
        public static DataTable WatchExistTxtInfo(string strTxtTy, string strExpressCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = TxtMngDao.SelectExistTxtInfo(strTxtTy, strExpressCd);

            return dtReturn;
        }

        #endregion

        #region RegistryTxtInfo : 항목문구 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 등록
         * Input    값 : RegistryTxtInfo(항목문구 타입, 표현코드, 베트남어, 영어, 한국어)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryTxtInfo : 항목문구 등록
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strTxtVi">베트남어</param>
        /// <param name="strTxtEn">영어</param>
        /// <param name="strTxtKr">한국어</param>
        /// <returns></returns>
        public static object[] RegistryTxtInfo(string strTxtTy, string strExpressCd, string strTxtVi, string strTxtEn, string strTxtKr)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.InsertTxtInfo(strTxtTy, strExpressCd, strTxtVi, strTxtEn, strTxtKr, "Y");

            return objReturn;
        }

        #endregion

        #region ModifyTxtInfo : 항목문구 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 수정
         * Input    값 : ModifyTxtInfo(항목문구 타입, 항목문구 순번, 표현코드, 베트남어, 영어, 한국어, 사용여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyTxtInfo : 항목문구 수정
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strTxtSeq">항목문구 순번</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strTxtVi">베트남어</param>
        /// <param name="strTxtEn">영어</param>
        /// <param name="strTxtKr">한국어</param>
        /// <param name="strUseYn">사용여부</param>
        /// <returns></returns>
        public static object[] ModifyTxtInfo(string strTxtTy, string strTxtSeq, string strExpressCd, string strTxtVi, string strTxtEn, string strTxtKr, string strUseYn)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.UpdateTxtInfo(strTxtTy, strTxtSeq, strExpressCd, strTxtVi, strTxtEn, strTxtKr, strUseYn);

            return objReturn;
        }

        #endregion

        #region ModifyExistTxtInfo : 항목문구 완전수정 (타입 변경시)

        /**********************************************************************************************
         * Mehtod   명 : ModifyExistTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 완전수정
         * Input    값 : ModifyExistTxtInfo(기존항목문구 타입, 새항목문구 타입, 항목문구 순번, 표현코드, 베트남어, 영어, 한국어, 사용여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyExistTxtInfo : 항목문구 완전수정 (타입 변경시)
        /// </summary>
        /// <param name="strOldTxtTy">기존항목문구 타입</param>
        /// <param name="strNewTxtTy">새항목문구 타입</param>
        /// <param name="strTxtSeq">항목문구 순번</param>
        /// <param name="strNewExpressCd">표현코드</param>
        /// <param name="strTxtVi">베트남어</param>
        /// <param name="strTxtEn">영어</param>
        /// <param name="strTxtKr">한국어</param>
        /// <param name="strUseYn">사용여부</param>
        /// <returns></returns>
        public static object[] ModifyExistTxtInfo(string strOldTxtTy, string strNewTxtTy, string strTxtSeq, string strNewExpressCd, string strTxtVi, string strTxtEn, string strTxtKr, string strUseYn)
        {
            object[] objReturn = new object[2];

            // 새항목 등록
            objReturn = TxtMngDao.InsertTxtInfo(strNewTxtTy, strNewExpressCd, strTxtVi, strTxtEn, strTxtKr, strUseYn);

            if (objReturn != null)
            {
                // 기존항목 삭제
                objReturn = TxtMngDao.DeleteTxtInfo(strOldTxtTy, strTxtSeq);
            }

            return objReturn;
        }

        #endregion

        #region RemoveTxtInfo : 항목문구 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 삭제
         * Input    값 : RemoveTxtInfo(항목문구 타입, 항목문구 순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTxtInfo : 항목문구 삭제
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strTxtSeq">항목문구 순번</param>
        /// <returns></returns>
        public static object[] RemoveTxtInfo(string strTxtTy, string strTxtSeq)
        {
            object[] objReturn = new object[2];

            objReturn = TxtMngDao.DeleteTxtInfo(strTxtTy, strTxtSeq);

            return objReturn;
        }

        #endregion
    }
}