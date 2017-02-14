using System.Data;

using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    public class SiteMapUtil
    {
        #region SpreadSiteMapInfo : 사이트맵 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadSiteMapInfo
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
        public static DataTable SpreadSiteMapInfo(string strLangCd, string strAccessAuth)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SiteMapInfo.SelectSiteMapInfo(strLangCd, strAccessAuth);

            return dtReturn;
        }

        #endregion

        #region SpreadMainSiteMapInfo : 메인화면 사이트맵 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMainSiteMapInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-26
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
        public static DataTable SpreadMainSiteMapInfo(string strLangCd, string strAccessAuth)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SiteMapInfo.SelectMainSiteMapInfo(strLangCd, strAccessAuth);

            return dtReturn;
        }

        #endregion
    }
}
