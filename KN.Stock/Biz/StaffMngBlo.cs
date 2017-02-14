using System.Data;

using KN.Stock.Dac;

namespace KN.Stock.Biz
{
    public class StaffMngBlo
    {
        #region SpreadDeptInfo : 부서 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadDeptInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-30
         * 용       도 : 부서 조회
         * Input    값 : SpreadDeptInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadDeptInfo : 부서 조회
        /// </summary>
        /// <returns></returns>
        public static DataTable SpreadDeptInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = StaffMngDao.SelectDeptInfo();

            return dtReturn;
        }

        #endregion

        #region SpreadStaffInfo : 직원 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadStaffInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-30
         * 용       도 : 직원 조회
         * Input    값 : SpreadStaffInfo(부서코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadStaffInfo : 직원 조회
        /// </summary>
        /// <param name="strDeptCd">부서코드</param>
        /// <returns></returns>
        public static DataTable SpreadStaffInfo(string strDeptCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = StaffMngDao.SelectStaffInfo(strDeptCd);

            return dtReturn;
        }

        #endregion
    }
}