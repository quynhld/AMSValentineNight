using System.Configuration;
using System.Data;

using KN.Common.DB;

namespace KN.Common.Base
{
    /// <summary>
    /// .Net과 Procedure간 데이터 전송 클래스
    /// </summary>
    public static class SPExecute
    {
        public static readonly string conWebconnString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
        public static readonly string conLogconnString = ConfigurationManager.ConnectionStrings["CommDBConnection"].ConnectionString;

        #region ExecReturnMulti : 멀티 Select 처리
        /// <summary>
        /// 멀티 Select 처리
        /// </summary>
        /// <param name="strSPNm">처리할 SP명</param>
        /// <param name="intType">
        /// <value>1 Common</value>
        /// <value>2 KN WEB</value>
        /// </param>
        /// <param name="objParams">매개변수</param>
        /// <returns>DataSet</returns>
        public static DataSet ExecReturnMulti(string strSPNm, int intType, params object[] objParams)
        {
            DataSet dsReturn = new DataSet();

            switch (intType)
            {
                case 1:

                    dsReturn = SPConnect.ExecSPMultiReturn(conLogconnString, strSPNm, objParams);
                    break;

                case 2:
                    dsReturn = SPConnect.ExecSPMultiReturn(conWebconnString, strSPNm, objParams);
                    break;
            }

            return dsReturn;
        }

        public static DataSet ExecReturnMulti(string strSPNm, params object[] objParams)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = SPConnect.ExecSPMultiReturn(conWebconnString, strSPNm, objParams);

            return dsReturn;
        }
        #endregion

        #region ExecReturnSingle : 싱글 Select 처리
        /// <summary>
        /// 싱글 Select 처리
        /// </summary>
        /// <param name="strSPNm">처리할 SP명</param>
        /// <param name="intType">
        /// <value>1 Common</value>
        /// <value>2 KN WEB</value>
        /// </param>
        /// <param name="objParams">매개변수</param>
        /// <returns>DataTable</returns>
        public static DataTable ExecReturnSingle(string strSPNm, int intType, params object[] objParams)
        {
            DataTable dtRetrun = new DataTable();

            switch (intType)
            {
                case 1:
                    dtRetrun = SPConnect.ExecSPSingleReturn(conLogconnString, strSPNm, objParams);
                    break;

                case 2:
                    dtRetrun = SPConnect.ExecSPSingleReturn(conWebconnString, strSPNm, objParams);
                    break;
            }

            return dtRetrun;
        }

        public static DataTable ExecReturnSingle(string strSPNm, params object[] objParams)
        {
            DataTable dtRetrun = new DataTable();

            dtRetrun = SPConnect.ExecSPSingleReturn(conWebconnString, strSPNm, objParams);

            return dtRetrun;
        }
        #endregion

        #region ExecReturnNo : Select가 없는 SP처리
        /// <summary>
        /// Select가 없는 SP처리
        /// </summary>
        /// <param name="strSPNm">처리할 SP명</param>
        /// <param name="intType">
        /// <value>1 Common</value>
        /// <value>2 KN WEB</value>
        /// </param>
        /// <param name="objParams">매개변수</param>
        /// <returns>
        /// <value>true / false</value>
        /// <value>라인수 / 에러메세지</value>
        /// </returns>
        public static object[] ExecReturnNo(string strSPNm, int intType, params object[] objParams)
        {
            object[] objReturn = new object[2];

            switch (intType)
            {
                case 1:
                    objReturn = SPConnect.ExecSPNoReturn(conLogconnString, strSPNm, objParams);
                    break;

                case 2:
                    objReturn = SPConnect.ExecSPNoReturn(conWebconnString, strSPNm, objParams);
                    break;
            }

            return objReturn;
        }

        public static object[] ExecReturnNo(string strSPNm, params object[] objParams)
        {
            object[] objReturn = new object[2];

            objReturn = SPConnect.ExecSPNoReturn(conWebconnString, strSPNm, objParams);

            return objReturn;
        }
        #endregion
    }
}
