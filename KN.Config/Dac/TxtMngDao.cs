using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Config.Dac
{
    public class TxtMngDao
    {
        #region SelectAlertInfo : Alert 문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : Alert 문구 조회
         * Input    값 : SelectAlertInfo(알림문구 타입, 검색코드, 검색어)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectAlertInfo : Alert 문구 조회
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataTable SelectAlertInfo(string strAlertTy, string strKeyCd, string strKeyWord)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strAlertTy;
            objParams[1] = strKeyCd;
            objParams[2] = strKeyWord;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ALERTINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExistAlertInfo : Alert 중복 문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : Alert 중복 문구 조회
         * Input    값 : SelectExistAlertInfo(알림문구 타입, 표현코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExistAlertInfo : Alert 중복 문구 조회
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <returns></returns>
        public static DataTable SelectExistAlertInfo(string strAlertTy, string strExpressCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strAlertTy;
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ALERTINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertAlertInfo : Alert 문구 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : Alert 문구 등록
         * Input    값 : InsertAlertInfo(알림문구 타입, 표현코드, 베트남어, 영어, 한국어)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertAlertInfo : Alert 문구 등록
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strAlertVi">베트남어</param>
        /// <param name="strAlertEn">영어</param>
        /// <param name="strAlertKr">한국어</param>
        /// <returns></returns>
        public static object[] InsertAlertInfo(string strAlertTy, string strExpressCd, string strAlertVi, string strAlertEn, string strAlertKr)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strAlertTy;
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strAlertVi));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strAlertEn));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strAlertKr));

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_ALERTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateAlertInfo : Alert 문구 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : Alert 문구 수정
         * Input    값 : UpdateAlertInfo(알림문구 타입, 알림문구 순번, 표현코드, 베트남어, 영어, 한국어, 사용여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateAlertInfo : Alert 문구 수정
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strAlertSeq">알림문구 순번</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strAlertVi">베트남어</param>
        /// <param name="strAlertEn">영어</param>
        /// <param name="strAlertKr">한국어</param>
        /// <param name="strUseYn">사용여부</param>
        /// <returns></returns>
        public static object[] UpdateAlertInfo(string strAlertTy, string strAlertSeq, string strExpressCd, string strAlertVi, string strAlertEn, string strAlertKr, string strUseYn)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = strAlertTy;
            objParams[1] = strAlertSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strAlertVi));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strAlertEn));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strAlertKr));
            objParams[6] = strUseYn;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_ALERTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteAlertInfo : Alert 문구 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteAlertInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : Alert 문구 삭제
         * Input    값 : DeleteAlertInfo(알림문구 타입, 알림문구 순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteAlertInfo : Alert 문구 삭제
        /// </summary>
        /// <param name="strAlertTy">알림문구 타입</param>
        /// <param name="strAlertSeq">알림문구 순번</param>
        /// <returns></returns>
        public static object[] DeleteAlertInfo(string strAlertTy, string strAlertSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strAlertTy;
            objParams[1] = strAlertSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_ALERTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region SelectMenuTxtInfo : 메뉴문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴문구 조회
         * Input    값 : SelectMenuTxtInfo(검색코드, 검색어)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMenuTxtInfo : 메뉴문구 조회
        /// </summary>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataTable SelectMenuTxtInfo(string strKeyCd, string strKeyWord)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strKeyCd;
            objParams[1] = strKeyWord;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MENUTXTINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExistMenuTxtInfo : 메뉴 중복 문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴 중복 문구 조회
         * Input    값 : SelectExistMenuTxtInfo(표현코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExistMenuTxtInfo : 메뉴 중복 문구 조회
        /// </summary>
        /// <param name="strExpressCd">표현코드</param>
        /// <returns></returns>
        public static DataTable SelectExistMenuTxtInfo(string strExpressCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MENUTXTINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertMenuTxtInfo : 메뉴문구 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴문구 등록
         * Input    값 : InsertMenuTxtInfo(표현코드, 베트남어, 영어, 한국어)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMenuTxtInfo : 메뉴문구 등록
        /// </summary>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strMenuTxtVi">베트남어</param>
        /// <param name="strMenuTxtEn">영어</param>
        /// <param name="strMenuTxtKr">한국어</param>
        /// <returns></returns>
        public static object[] InsertMenuTxtInfo(string strExpressCd, string strMenuVi, string strMenuEn, string strMenuKr)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMenuVi));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMenuEn));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMenuKr));

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MENUTXTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateMenuTxtInfo : 메뉴문구 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : Menu 문구 수정
         * Input    값 : UpdateMenuTxtInfo(메뉴문구 순번, 표현코드, 베트남어, 영어, 한국어, 사용여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMenuTxtInfo : 메뉴문구 수정
        /// </summary>
        /// <param name="strMenuTxtSeq">메뉴문구 순번</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strMenuVi">베트남어</param>
        /// <param name="strMenuEn">영어</param>
        /// <param name="strMenuKr">한국어</param>
        /// <param name="strUseYn">사용여부</param>
        /// <returns></returns>
        public static object[] UpdateMenuTxtInfo(string strMenuTxtSeq, string strExpressCd, string strMenuVi, string strMenuEn, string strMenuKr, string strUseYn)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strMenuTxtSeq;
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMenuVi));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMenuEn));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMenuKr));
            objParams[5] = strUseYn;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MENUTXTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteMenuTxtInfo : 메뉴문구 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMenuTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : 메뉴문구 삭제
         * Input    값 : DeleteMenuTxtInfo(메뉴문구 순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteMenuTxtInfo : 메뉴문구 삭제
        /// </summary>
        /// <param name="strMenuTxtSeq">메뉴문구 순번</param>
        /// <returns></returns>
        public static object[] DeleteMenuTxtInfo(string strMenuTxtSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = strMenuTxtSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_MENUTXTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region SelectTxtInfo : 항목문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 조회
         * Input    값 : SelectTxtInfo(항목문구 타입, 검색코드, 검색어)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectTxtInfo : 항목문구 조회
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataTable SelectTxtInfo(string strTxtTy, string strKeyCd, string strKeyWord)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strTxtTy;
            objParams[1] = strKeyCd;
            objParams[2] = strKeyWord;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_TXTINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectExistTxtInfo : 항목 중복 문구 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목 중복 문구 조회
         * Input    값 : SelectExistTxtInfo(항목문구 타입, 표현코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExistTxtInfo : 항목 중복 문구 조회
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <returns></returns>
        public static DataTable SelectExistTxtInfo(string strTxtTy, string strExpressCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strTxtTy;
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_TXTINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertTxtInfo : 항목문구 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 등록
         * Input    값 : InsertTxtInfo(항목문구 타입, 표현코드, 베트남어, 영어, 한국어, 사용여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertTxtInfo : 항목문구 등록
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strTxtVi">베트남어</param>
        /// <param name="strTxtEn">영어</param>
        /// <param name="strTxtKr">한국어</param>
        /// <param name="strUseYn">사용여부</param>
        /// <returns></returns>
        public static object[] InsertTxtInfo(string strTxtTy, string strExpressCd, string strTxtVi, string strTxtEn, string strTxtKr, string strUseYn)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strTxtTy;
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTxtVi));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTxtEn));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTxtKr));
            objParams[5] = strUseYn;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_TXTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateTxtInfo : 항목문구 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 수정
         * Input    값 : UpdateTxtInfo(항목문구 타입, 항목문구 순번, 표현코드, 베트남어, 영어, 한국어, 사용여부)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateTxtInfo : 항목문구 수정
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strTxtSeq">항목문구 순번</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="strTxtVi">베트남어</param>
        /// <param name="strTxtEn">영어</param>
        /// <param name="strTxtKr">한국어</param>
        /// <param name="strUseYn">사용여부</param>
        /// <returns></returns>
        public static object[] UpdateTxtInfo(string strTxtTy, string strTxtSeq, string strExpressCd, string strTxtVi, string strTxtEn, string strTxtKr, string strUseYn)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = strTxtTy;
            objParams[1] = strTxtSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strExpressCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTxtVi));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTxtEn));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTxtKr));
            objParams[6] = strUseYn;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_TXTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteTxtInfo : 항목문구 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTxtInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 항목문구 삭제
         * Input    값 : DeleteTxtInfo(항목문구 타입, 항목문구 순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteTxtInfo : 항목문구 삭제
        /// </summary>
        /// <param name="strTxtTy">항목문구 타입</param>
        /// <param name="strTxtSeq">항목문구 순번</param>
        /// <returns></returns>
        public static object[] DeleteTxtInfo(string strTxtTy, string strTxtSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strTxtTy;
            objParams[1] = strTxtSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_TXTINFO_M00", objParams);

            return objReturn;
        }

        #endregion
    }
}