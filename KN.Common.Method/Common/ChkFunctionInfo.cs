using System.Data;

using KN.Common.Base;

namespace KN.Common.Method.Common
{
    public class ChkFunctionInfo
    {
        #region SelectCheckDate : 월간 기간 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectCheckDate
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-14
         * 용       도 : 월간 기간 조회
         * Input    값 : SelectCheckDate(시작일자, 종료일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectCheckDate : 월간 기간 조회
        /// </summary>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectCheckDate(string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strStartDt;
            objParams[1] = strEndDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_COMPAREMONTH_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectChangeDate : 변경된 날짜 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectChangeDate
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-14
         * 용       도 : 변경된 날짜 조회
         * Input    값 : SelectChangeDate(기준일자, 추가일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectChangeDate : 변경된 날짜 조회
        /// </summary>
        /// <param name="strStandardDt">기준일자</param>
        /// <param name="intAddedDays">추가일</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectChangeDate(string strStandardDt, int intAddedDays)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strStandardDt;
            objParams[1] = intAddedDays;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_COMPAREMONTH_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectCheckDateForDay : 기간 조회 (일간)

        /**********************************************************************************************
         * Mehtod   명 : SelectCheckDateForDay
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-11-02
         * 용       도 : 기간 조회 (일간)
         * Input    값 : SelectCheckDateForDay(시작일자, 종료일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectCheckDateForDay : 월간 기간 조회
        /// </summary>
        /// <param name="strStartDt">시작일자</param>
        /// <param name="strEndDt">종료일자</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectCheckDateForDay(string strStartDt, string strEndDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strStartDt;
            objParams[1] = strEndDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_COMPAREDAY_S00", objParams);

            return dtReturn;
        }

        #endregion
    }
}
