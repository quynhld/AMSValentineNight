using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Stock.Dac
{
    public class StaffMngDao
    {
        #region SelectDeptInfo : 부서 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectDeptInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-30
         * 용       도 : 부서 조회
         * Input    값 : SelectDeptInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectDeptInfo : 부서 조회
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectDeptInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_DEPTINFO_S00");

            return dtReturn;
        }

        #endregion

        #region SelectStaffInfo : 직원 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectStaffInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-30
         * 용       도 : 직원 조회
         * Input    값 : SelectStaffInfo(부서코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectStaffInfo : 직원 조회
        /// </summary>
        /// <param name="strDeptCd">부서코드</param>
        /// <returns></returns>
        public static DataTable SelectStaffInfo(string strDeptCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objReturn = new object[1];

            objReturn[0] = TextLib.MakeNullToEmpty(strDeptCd);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_STAFFINFO_S00", objReturn);

            return dtReturn;
        }

        #endregion
    }
}
