using System.Data;

using KN.Stock.Dac;

namespace KN.Stock.Biz
{
    public class MaterialClassificationBlo
    {
        #region SpreadClassiGrpCdInfo : 자재분류 그룹코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadClassiGrpCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-20
         * 용       도 : 자재분류 그룹코드 조회
         * Input    값 : SpreadClassiGrpCdInfo(언어코드, 조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadClassiGrpCdInfo : 자재분류 그룹코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <returns></returns>
        public static DataTable SpreadClassiGrpCdInfo(string strLangCd, string strViewDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialClassificationDao.SelectClassiGrpCdInfo(strLangCd, strViewDt);

            return dtReturn;
        }

        #endregion

        #region SpreadClassiMainCdInfo : 자재분류 메인코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadClassiMainCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-20
         * 용       도 : 자재분류 메인코드 조회
         * Input    값 : SpreadClassiMainCdInfo(언어코드, 조회일자, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadClassiMainCdInfo : 자재분류 메인코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <returns></returns>
        public static DataTable SpreadClassiMainCdInfo(string strLangCd, string strViewDt, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialClassificationDao.SelectClassiMainCdInfo(strLangCd, strViewDt, strGrpCd);

            return dtReturn;
        }

        #endregion

        #region SpreadClassiMainCdInfoWithNoTitle : 자재분류 메인코드 조회 (제목 없음)

        /**********************************************************************************************
         * Mehtod   명 : SpreadClassiMainCdInfoWithNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-26
         * 용       도 : 자재분류 메인코드 조회 (제목 없음)
         * Input    값 : SpreadClassiMainCdInfo(언어코드, 조회일자, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadClassiMainCdInfoWithNoTitle : 자재분류 메인코드 조회 (제목 없음)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <returns></returns>
        public static DataTable SpreadClassiMainCdInfoWithNoTitle(string strLangCd, string strViewDt, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialClassificationDao.SelectClassiMainCdInfoWithNoTitle(strLangCd, strViewDt, strGrpCd);

            return dtReturn;
        }

        #endregion

        #region WatchMaterialClassificationInfo : 자재분류 상세보기 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchMaterialClassificationInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-20
         * 용       도 : 자재분류 메인코드 조회
         * Input    값 : WatchMaterialClassificationInfo(언어코드, 조회일자, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchMaterialClassificationInfo : 자재분류 메인코드 조회
        /// </summary>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <returns></returns>
        public static DataTable WatchMaterialClassificationInfo(string strViewDt, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialClassificationDao.SelectMaterialClassificationInfo(strViewDt, strGrpCd, strMainCd);

            return dtReturn;
        }

        #endregion

        #region RegistryMaterialClassificationInfo : 자재분류 코드 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryMaterialClassificationInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-24
         * 용       도 : 자재분류 코드 등록
         * Input    값 : RegistryMaterialClassificationInfo(그룹코드, 메인코드, 영문명, 베트남명, 한글명, 입력사번, 입력IP)
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
        public static object[] RegistryMaterialClassificationInfo(string strGrpCd, string strMainCd, string strNmEn, string strNmVi, string strNmKr, string strCompCd, string strMemNo, string strMemIp)
        {
            object[] objReturns = new object[2];

            objReturns = MaterialClassificationDao.InsertMaterialClassificationInfo(strGrpCd, strMainCd, strNmEn, strNmVi, strNmKr, strCompCd, strMemNo, strMemIp);

            return objReturns;
        }

        #endregion

        #region RemoveMaterialClassificationInfo : 자재분류 코드 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMaterialClassificationInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-24
         * 용       도 : 자재분류 코드 삭제
         * Input    값 : RemoveMaterialClassificationInfo(그룹코드, 메인코드, 입력사번, 입력IP)
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
        public static object[] RemoveMaterialClassificationInfo(string strGrpCd, string strMainCd, string strCompCd, string strMemNo, string strMemIp)
        {
            object[] objReturns = new object[2];

            objReturns = MaterialClassificationDao.DeleteMaterialClassificationInfo(strGrpCd, strMainCd, strCompCd, strMemNo, strMemIp);

            return objReturns;
        }

        #endregion
    }
}
