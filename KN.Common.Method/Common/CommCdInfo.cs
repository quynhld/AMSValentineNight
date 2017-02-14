using System;
using System.Data;
using System.Text;

using KN.Common.Base;
using KN.Common.Base.Code;

namespace KN.Common.Method.Common
{
    /// <summary>
    /// 공통코드 조회 클래스
    /// </summary>
    public class CommCdInfo
    {
        #region SelectGrpCd : 공통 그룹코드 리스트 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectGrpCd
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통 그룹코드 리스트 조회
         * Input    값 : SelectGrpCd(언어코드, 조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectGrpCd : 공통 그룹코드 리스트 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectGrpCd(string strLangCd, string strViewDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_GRPCD_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectGrpCd(StringBuilder sbLangCd, StringBuilder sbViewDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = sbLangCd;
            objParams[1] = sbViewDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_GRPCD_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectGrpCd(string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectGrpCd(strLangCd, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            return dtReturn;
        }

        public static DataTable SelectGrpCd(StringBuilder sbLangCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectGrpCd(sbLangCd, sbViewDt);

            return dtReturn;
        }

        public static DataTable SelectGrpCd()
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbLangCd = new StringBuilder(CommValue.LANG_VALUE_ENGLISH);
            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectGrpCd(sbLangCd, sbViewDt);

            return dtReturn;
        }
        #endregion

        #region SelectMainCdWithTitle : 제목있는 공통 메인코드 리스트 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectMainCdWithTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 제목있는 공통 메인코드 리스트 조회
         * Input    값 : SelectMainCdWithTitle(언어코드, 조회일자, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMainCdWithTitle : 제목있는 공통 메인코드 리스트 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectMainCdWithTitle(string strLangCd, string strViewDt, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;
            objParams[2] = strGrpCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MAINCD_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithTitle(StringBuilder sbLangCd, StringBuilder sbViewDt, StringBuilder sbGrpCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = sbLangCd;
            objParams[1] = sbViewDt;
            objParams[2] = sbGrpCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MAINCD_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithTitle(string strLangCd, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectMainCdWithTitle(strLangCd, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""), strGrpCd);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithTitle(StringBuilder sbLangCd, StringBuilder sbGrpCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectMainCdWithTitle(sbLangCd, sbViewDt, sbGrpCd);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithTitle(string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectMainCdWithTitle(CommValue.LANG_VALUE_ENGLISH, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""), strGrpCd);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithTitle(StringBuilder sbGrpCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbLangCd = new StringBuilder(CommValue.LANG_VALUE_ENGLISH);
            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectMainCdWithTitle(sbLangCd, sbViewDt, sbGrpCd);

            return dtReturn;
        }
        #endregion

        #region SelectMainCdWithNoTitle : 제목없는 공통 메인코드 리스트 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectMainCdWithNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 제목없는 공통 메인코드 리스트 조회
         * Input    값 : SelectMainCdWithNoTitle(언어코드, 조회일자, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMainCdWithNoTitle : 제목없는 공통 메인코드 리스트 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectMainCdWithNoTitle(string strLangCd, string strViewDt, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;
            objParams[2] = strGrpCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MAINCD_S01", objParams);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithNoTitle(StringBuilder sbLangCd, StringBuilder sbViewDt, StringBuilder sbGrpCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = sbLangCd;
            objParams[1] = sbViewDt;
            objParams[2] = sbGrpCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_MAINCD_S01", objParams);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithNoTitle(string strLangCd, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectMainCdWithNoTitle(strLangCd, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""), strGrpCd);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithNoTitle(StringBuilder sbLangCd, StringBuilder sbGrpCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectMainCdWithNoTitle(sbLangCd, sbViewDt, sbGrpCd);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithNoTitle(string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectMainCdWithNoTitle(CommValue.LANG_VALUE_ENGLISH, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""), strGrpCd);

            return dtReturn;
        }

        public static DataTable SelectMainCdWithNoTitle(StringBuilder sbGrpCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbLangCd = new StringBuilder(CommValue.LANG_VALUE_ENGLISH);
            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectMainCdWithNoTitle(sbLangCd, sbViewDt, sbGrpCd);

            return dtReturn;
        }
        #endregion

        #region SelectSubCdWithTitle : 제목있는 공통 서브코드 리스트 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectSubCdWithTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 제목있는 공통 서브코드 리스트 조회
         * Input    값 : SelectSubCdWithTitle(언어코드, 조회일자, 그룹코드, 메인코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectSubCdWithTitle : 제목있는 공통 서브코드 리스트 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectSubCdWithTitle(string strLangCd, string strViewDt, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;
            objParams[2] = strGrpCd;
            objParams[3] = strMainCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_SUBCD_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithTitle(StringBuilder sbLangCd, StringBuilder sbViewDt, StringBuilder sbGrpCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = sbLangCd;
            objParams[1] = sbViewDt;
            objParams[2] = sbGrpCd;
            objParams[3] = sbMainCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_SUBCD_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithTitle(string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectSubCdWithTitle(strLangCd, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""), strGrpCd, strMainCd);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithTitle(StringBuilder sbLangCd, StringBuilder sbGrpCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectSubCdWithTitle(sbLangCd, sbViewDt, sbGrpCd, sbMainCd);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithTitle(string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectSubCdWithTitle(CommValue.LANG_VALUE_ENGLISH, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""), strGrpCd, strMainCd);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithTitle(StringBuilder sbGrpCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbLangCd = new StringBuilder(CommValue.LANG_VALUE_ENGLISH);
            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectSubCdWithTitle(sbLangCd, sbViewDt, sbGrpCd, sbMainCd);

            return dtReturn;
        }
        #endregion

        #region SelectSubCdWithNoTitle : 제목없는 공통 서브코드 리스트 조회
        /**********************************************************************************************
         * Mehtod   명 : SelectSubCdWithNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 제목없는 공통 서브코드 리스트 조회
         * Input    값 : SelectSubCdWithNoTitle(언어코드, 조회일자, 그룹코드, 메인코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectSubCdWithNoTitle : 제목없는 공통 서브코드 리스트 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectSubCdWithNoTitle(string strLangCd, string strViewDt, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;
            objParams[2] = strGrpCd;
            objParams[3] = strMainCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_SUBCD_S01", objParams);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithNoTitle(StringBuilder sbLangCd, StringBuilder sbViewDt, StringBuilder sbGrpCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = sbLangCd;
            objParams[1] = sbViewDt;
            objParams[2] = sbGrpCd;
            objParams[3] = sbMainCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_SUBCD_S01", objParams);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithNoTitle(string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectSubCdWithNoTitle(strLangCd, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""), strGrpCd, strMainCd);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithNoTitle(StringBuilder sbLangCd, StringBuilder sbGrpCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectSubCdWithNoTitle(sbLangCd, sbViewDt, sbGrpCd, sbMainCd);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithNoTitle(string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SelectSubCdWithNoTitle(CommValue.LANG_VALUE_ENGLISH, DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""), strGrpCd, strMainCd);

            return dtReturn;
        }

        public static DataTable SelectSubCdWithNoTitle(StringBuilder sbGrpCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();

            StringBuilder sbLangCd = new StringBuilder(CommValue.LANG_VALUE_ENGLISH);
            StringBuilder sbViewDt = new StringBuilder(DateTime.Now.ToString("s").Substring(0,10).Replace("/", "").Replace("-", ""));

            dtReturn = SelectSubCdWithNoTitle(sbLangCd, sbViewDt, sbGrpCd, sbMainCd);

            return dtReturn;
        }

        public static DataTable SelectSpecialFee(string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[1];

            objParams[0] = strLangCd;


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_SPCFEE_S01", objParams);

            return dtReturn;
        }
        #endregion
    }

    
}
