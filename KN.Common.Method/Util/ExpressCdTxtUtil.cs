using System.Data;

using KN.Common.Base.Code;
using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    /// <summary>
    /// 다국어 텍스트 처리 Utility
    /// </summary>
    public class ExpressCdTxtUtil
    {
        #region MakeMultiLanguage : 다국어 관련 텍스트 정보를 가져옴.
        /**********************************************************************************************
         * Mehtod   명 : MakeMultiLanguage
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-06
         * 용       도 : 다국어 관련 텍스트 정보를 가져옴.
         * Input    값 : MakeMultiLanguage(언어코드, 가져올 텍스트 종류, 메뉴순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 다국어 관련 텍스트 정보를 가져옴.
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strTextTy">가져올 텍스트 종류</param>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <returns>DataTable</returns>
        public static DataTable MakeMultiLanguage(string strLangCd, string strTextTy, int intMenuSeq)
        {
            DataTable dtReturn = new DataTable();

            // 메뉴에 관한 텍스트를 가져올 경우
            if (strTextTy.Equals(CommValue.TEXT_TYPE_VALUE_MENU))
            {
                dtReturn = ExpressCdInfo.SelectMenuTxt(strLangCd);
            }
            // 항목에 관한 텍스트를 가져올 경우
            else if (strTextTy.Equals(CommValue.TEXT_TYPE_VALUE_ITEM))
            {
                dtReturn = ExpressCdInfo.SelectWordTxt(strLangCd, intMenuSeq);
            }
            // 경고문에 관한 텍스트를 가져올 경우
            else if (strTextTy.Equals(CommValue.TEXT_TYPE_VALUE_ALERT))
            {
                dtReturn = ExpressCdInfo.SelectAlertTxt(strLangCd);
            }

            return dtReturn;
        }
        #endregion

        #region MakeRentTxt : 섹션 코드값에 따른 섹션 텍스트 표기
        /**********************************************************************************************
         * Mehtod   명 : MakeRentTxt
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-06
         * 용       도 : 섹션 코드값에 따른 섹션 텍스트 표기
         * Input    값 : MakeRentTxt(언어코드, 섹션코드)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeRentTxt : 섹션 코드값에 따른 섹션 텍스트 표기
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns>DataTable</returns>
        public static string MakeRentTxt(string strLangCd, string strRentCd)
        {
            string strReturn = string.Empty;
            DataTable dtReturn  = new DataTable();

            dtReturn = ExpressCdInfo.SelectRentTxt(strLangCd, strRentCd);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    strReturn = dtReturn.Rows[0]["RentNm"].ToString();
                }
            }

            return strReturn;
        }
        #endregion
    }
}
