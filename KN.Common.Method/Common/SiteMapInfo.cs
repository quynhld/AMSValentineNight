using System.Data;

using KN.Common.Base;

namespace KN.Common.Method.Common
{
    public class SiteMapInfo
    {
        #region SelectSiteMapInfo : 사이트맵 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectSiteMapInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-26
         * 용       도 : 사이트맵 조회
         * Input    값 : strLangCd(언어코드), strAccessAuth(권한코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 사이트맵 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strAccessAuth">권한코드</param>
        /// <returns></returns>
        public static DataTable SelectSiteMapInfo(string strLangCd, string strAccessAuth)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strAccessAuth;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_SITEMAP_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectMainSiteMapInfo : 메인화면 사이트맵 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMainSiteMapInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-28
         * 용       도 : 메인화면 사이트맵 조회
         * Input    값 : strLangCd(언어코드), strAccessAuth(권한코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 메인화면 사이트맵 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strAccessAuth">권한코드</param>
        /// <returns></returns>
        public static DataTable SelectMainSiteMapInfo(string strLangCd, string strAccessAuth)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strAccessAuth;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_SITEMAP_S01", objParams);

            return dtReturn;
        }

        #endregion
    }
}
