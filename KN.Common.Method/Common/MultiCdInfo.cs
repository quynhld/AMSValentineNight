using System.Data;

using KN.Common.Base;

namespace KN.Common.Method.Common
{
    public class MultiCdInfo
    {
        #region SelectMemCompCd : 회사코드 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMemCompCd
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-07
         * 용       도 : 회사코드 리스트 조회
         * Input    값 : SelectMemCompCd(회사코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMemCompCd : 공통 그룹코드 리스트 조회
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectMemCompCd(string strCompNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[1];

            objParams[0] = strCompNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MEMCOMPINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectManuallyMngYear : 수동등록 년도 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectManuallyMngYear
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동등록 년도 조회
         * Input    값 : SelectManuallyMngYear(섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectManuallyMngYear : 수동등록 년도 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectManuallyMngYear(string strRentCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[1];

            objParams[0] = strRentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MANUALLYMONTH_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectManuallyMngMonth : 수동등록 월 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectManuallyMngMonth
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동등록 월 조회
         * Input    값 : SelectManuallyMngMonth(섹션코드, 년도코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectManuallyMngMonth : 수동등록 년도 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectManuallyMngMonth(string strRentCd, string strYear)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strYear;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MANUALLYMONTH_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectManuallyUtilYear : 수동등록 년도 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectManuallyUtilYear
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-12
         * 용       도 : 수동등록 년도 조회
         * Input    값 : SelectManuallyUtilYear(섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectManuallyUtilYear : 수동등록 년도 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectManuallyUtilYear(string strRentCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[1];

            objParams[0] = strRentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MANUALLYMONTH_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectManuallyUtilMonth : 수동등록 월 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectManuallyUtilMonth
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동등록 월 조회
         * Input    값 : SelectManuallyUtilMonth(섹션코드, 년도코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectManuallyUtilMonth : 수동등록 년도 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectManuallyUtilMonth(string strRentCd, string strYear)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strRentCd;
            objParams[1] = strYear;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MANUALLYMONTH_S03", objParams);

            return dtReturn;
        }

        #endregion
    }
}
