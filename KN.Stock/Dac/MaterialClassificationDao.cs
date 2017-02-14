using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Stock.Dac
{
    public class MaterialClassificationDao
    {
        #region SelectClassiGrpCdInfo : 자재분류 그룹코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectClassiGrpCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-20
         * 용       도 : 자재분류 그룹코드 조회
         * Input    값 : SelectClassiGrpCdInfo(언어코드, 조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectClassiGrpCdInfo : 자재분류 그룹코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <returns></returns>
        public static DataTable SelectClassiGrpCdInfo(string strLangCd, string strViewDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_CLASSIGRPCD_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectClassiMainCdInfo : 자재분류 메인코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectClassiMainCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-20
         * 용       도 : 자재분류 메인코드 조회
         * Input    값 : SelectClassiMainCdInfo(언어코드, 조회일자, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectClassiMainCdInfo : 자재분류 메인코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <returns></returns>
        public static DataTable SelectClassiMainCdInfo(string strLangCd, string strViewDt, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;
            objParams[2] = strGrpCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_CLASSIMAINCD_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectClassiMainCdInfoWithNoTitle : 자재분류 메인코드 조회 (제목 없음)

        /**********************************************************************************************
         * Mehtod   명 : SelectClassiMainCdInfoWithNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-26
         * 용       도 : 자재분류 메인코드 조회 (제목 없음)
         * Input    값 : SelectClassiMainCdInfo(언어코드, 조회일자, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectClassiMainCdInfoWithNoTitle : 자재분류 메인코드 조회 (제목 없음)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <returns></returns>
        public static DataTable SelectClassiMainCdInfoWithNoTitle(string strLangCd, string strViewDt, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;
            objParams[2] = strGrpCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_CLASSIMAINCD_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMaterialClassificationInfo : 자재분류 상세보기 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMaterialClassificationInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-20
         * 용       도 : 자재분류 메인코드 조회
         * Input    값 : SelectMaterialClassificationInfo(언어코드, 조회일자, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMaterialClassificationInfo : 자재분류 메인코드 조회
        /// </summary>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <returns></returns>
        public static DataTable SelectMaterialClassificationInfo(string strViewDt, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strViewDt;
            objParams[1] = TextLib.MakeNullToEmpty(strGrpCd);
            objParams[2] = TextLib.MakeNullToEmpty(strMainCd);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_MATERIALCLASSIFICATIONINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertMaterialClassificationInfo : 자재분류 코드 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertMaterialClassificationInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-24
         * 용       도 : 자재분류 코드 등록
         * Input    값 : InsertMaterialClassificationInfo(그룹코드, 메인코드, 영문명, 베트남명, 한글명, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 자재분류 코드 등록
        /// </summary>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strNmEn">영문명</param>
        /// <param name="strNmVi">베트남명</param>
        /// <param name="strNmKr">한글명</param>
        /// <param name="strMemNo">입력사번</param>
        /// <param name="strMemIp">입력IP</param>
        /// <returns></returns>
        public static object[] InsertMaterialClassificationInfo(string strGrpCd, string strMainCd, string strNmEn, string strNmVi, string strNmKr, string strCompCd, string strMemNo, string strMemIp)
        {
            object[] objReturns = new object[2];
            object[] objParams = new object[8];

            objParams[0] = TextLib.MakeNullToEmpty(strGrpCd);
            objParams[1] = TextLib.MakeNullToEmpty(strMainCd);
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strNmEn));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strNmVi));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strNmKr));
            objParams[5] = strCompCd;
            objParams[6] = strMemNo;
            objParams[7] = strMemIp;

            objReturns = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_MATERIALCLASSIFICATIONINFO_M00", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteMaterialClassificationInfo : 자재분류 코드 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMaterialClassificationInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-24
         * 용       도 : 자재분류 코드 삭제
         * Input    값 : DeleteMaterialClassificationInfo(그룹코드, 메인코드, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 자재분류 코드 삭제
        /// </summary>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strMemNo">입력사번</param>
        /// <param name="strMemIp">입력IP</param>
        /// <returns></returns>
        public static object[] DeleteMaterialClassificationInfo(string strGrpCd, string strMainCd, string strCompCd, string strMemNo, string strMemIp)
        {
            object[] objReturns = new object[2];
            object[] objParams = new object[5];

            objParams[0] = TextLib.MakeNullToEmpty(strGrpCd);
            objParams[1] = TextLib.MakeNullToEmpty(strMainCd);
            objParams[2] = strCompCd;
            objParams[3] = strMemNo;
            objParams[4] = strMemIp;

            objReturns = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_MATERIALCLASSIFICATIONINFO_M00", objParams);

            return objReturns;
        }

        #endregion
    }
}