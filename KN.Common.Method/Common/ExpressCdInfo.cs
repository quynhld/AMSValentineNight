using System.Data;
using System.Text;

using KN.Common.Base;

namespace KN.Common.Method.Common
{
    /// <summary>
    /// 언어 표현코드(메뉴, 각요소제목, 경고창용 텍스트) 조회 클래스
    /// </summary>
    public class ExpressCdInfo
    {
        #region SelectMenuTxt : 메뉴 텍스트 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectMenuTxt
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-05
         * 용       도 : 언어 표현코드(메뉴) 조회 클래스
         * Input    값 : SelectMenuTxt(언어코드, 표현코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 언어 표현코드(메뉴) 조회 클래스
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectMenuTxt(string strLangCd, string strExpressCd)
        {
            DataTable dtReturn = new DataTable();

            object [] objParams = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strExpressCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MENUTXT_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectMenuTxt(StringBuilder sbLangCd, StringBuilder sbExpressCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = sbLangCd;
            objParams[1] = sbExpressCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MENUTXT_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectMenuTxt(string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            string strExpressCd = string.Empty;

            dtReturn = SelectMenuTxt(strLangCd, strExpressCd);

            return dtReturn;
        }

        public static DataTable SelectMenuTxt(StringBuilder sbLangCd)
        {
            DataTable dtReturn = new DataTable();
            StringBuilder sbExpressCd = new StringBuilder("");

            dtReturn = SelectMenuTxt(sbLangCd, sbExpressCd);

            return dtReturn;
        }
        #endregion

        #region SelectWordTxt : 요소별 텍스트 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectWordTxt
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-05
         * 용       도 : 언어 표현코드(각요소텍스트) 조회 클래스
         * Input    값 : SelectWordTxt(언어코드, 표현코드, 메뉴카테고리번호, 메뉴순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 언어 표현코드(각요소제목) 조회 클래스
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectWordTxt(string strLangCd, string strExpressCd, int intMenuSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = strExpressCd;
            objParams[2] = intMenuSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_TXT_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectWordTxt(StringBuilder sbLangCd, StringBuilder sbExpressCd, int intMenuSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = sbLangCd;
            objParams[1] = sbExpressCd;
            objParams[2] = intMenuSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_TXT_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectWordTxt(string strLangCd, int intMenuSeq)
        {
            DataTable dtReturn = new DataTable();
            string strExpressCd = string.Empty;

            dtReturn = SelectWordTxt(strLangCd, strExpressCd, intMenuSeq);

            return dtReturn;
        }

        public static DataTable SelectWordTxt(StringBuilder sbLangCd, int intMenuSeq)
        {
            DataTable dtReturn = new DataTable();
            StringBuilder sbExpressCd = new StringBuilder("");

            dtReturn = SelectWordTxt(sbLangCd, sbExpressCd, intMenuSeq);

            return dtReturn;
        }
        #endregion

        #region SelectAlertTxt : 경고 텍스트 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectAlertTxt
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-05
         * 용       도 : 언어 표현코드(경고창) 조회 클래스
         * Input    값 : SelectAlertTxt(언어코드, 표현코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 언어 표현코드(경고창) 조회 클래스
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strExpressCd">표현코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectAlertTxt(string strLangCd, string strExpressCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strExpressCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_ALERT_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectAlertTxt(StringBuilder sbLangCd, StringBuilder sbExpressCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = sbLangCd;
            objParams[1] = sbExpressCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_ALERT_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectAlertTxt(string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            string strExpressCd = string.Empty;

            dtReturn = SelectAlertTxt(strLangCd, strExpressCd);

            return dtReturn;
        }

        public static DataTable SelectAlertTxt(StringBuilder sbLangCd)
        {
            DataTable dtReturn = new DataTable();
            StringBuilder sbExpressCd = new StringBuilder("");

            dtReturn = SelectMenuTxt(sbLangCd, sbExpressCd);

            return dtReturn;
        }
        #endregion

        #region SelectRentTxt : 섹션코드 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectRentTxt
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-05
         * 용       도 : 섹션코드 조회
         * Input    값 : SelectRentTxt(언어코드, 섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRentTxt : 섹션코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectRentTxt(string strLangCd, string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strRentCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_RENTCD_S00", objParams);

            return dtReturn;
        }
        #endregion
    }
}
