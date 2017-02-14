using System.Data;

using KN.Common.Base.Code;
using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    public class ChkFunctionUtil
    {
        #region MakeCheckDate : 월간 기간 조회

        /**********************************************************************************************
         * Mehtod   명 : MakeCheckDate
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-14
         * 용       도 : 월간 기간 조회
         * Input    값 : MakeCheckDate(시작일자, 종료일자)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeCheckDate : 월간 기간 조회
        /// </summary>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <returns>string</returns>
        public static string MakeCheckDate(string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();
            string strReturn = string.Empty;

            dtReturn = ChkFunctionInfo.SelectCheckDate(strStartDt, strEndDt);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    strReturn = dtReturn.Rows[0]["GapMonth"].ToString();
                }
            }

            return strReturn;
        }

        #endregion

        #region MakeChangeDate : 변경된 날짜 조회

        /**********************************************************************************************
         * Mehtod   명 : MakeChangeDate
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-14
         * 용       도 : 변경된 날짜 조회
         * Input    값 : MakeChangeDate(기준일자, 추가일)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeChangeDate : 변경된 날짜 조회
        /// </summary>
        /// <param name="strStandardDt">기준일자</param>
        /// <param name="intAddedDays">추가일</param>
        /// <returns>string</returns>
        public static string MakeChangeDate(string strStandardDt, int intAddedDays)
        {
            DataTable dtReturn = new DataTable();
            string strReturn = string.Empty;

            dtReturn = ChkFunctionInfo.SelectChangeDate(strStandardDt, intAddedDays);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    strReturn = dtReturn.Rows[0]["ChangedDt"].ToString();
                }
            }

            return strReturn;
        }

        #endregion

        #region MakeCheckDay : 기간 조회 (일간)

        /**********************************************************************************************
         * Mehtod   명 : MakeCheckDay
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-11-02
         * 용       도 : 기간 조회 (일간)
         * Input    값 : MakeCheckDay(시작일자, 종료일자)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeCheckDay : 기간 조회 (일간)
        /// </summary>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <returns>string</returns>
        public static string MakeCheckDay(string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();
            string strReturn = string.Empty;

            dtReturn = ChkFunctionInfo.SelectCheckDateForDay(strStartDt, strEndDt);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    strReturn = dtReturn.Rows[0]["GapDay"].ToString();
                }
            }

            return strReturn;
        }

        #endregion
    }
}
