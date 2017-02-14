using System.Data;
using System.Web.UI.WebControls;

using KN.Common.Base.Code;
using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    public class MultiCdDdlUtil
    {
        #region MakeMemCompCdNoTitle : 제목없는 회사코드 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : MakeMemCompCdNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-07
         * 용       도 : 제목없는 회사코드 리스트 조회
         * Input    값 : SelectMemCompCd(드롭다운리스트객체, 회사코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// MakeMemCompCdNoTitle : 회사코드 리스트 조회
        /// </summary>
        /// <param name="ddlParamNm">드롭다운리스트객체</param>
        /// <param name="strCompNo">회사코드</param>
        /// <returns>DataTable</returns>
        public static void MakeMemCompCdNoTitle(DropDownList ddlParamNm, string strCompNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MultiCdInfo.SelectMemCompCd(strCompNo);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CompNm"].ToString(), dr["CompNo"].ToString()));
            }
        }

        #endregion

        #region MakeMemCompCdTitle : 제목있는 회사코드 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : MakeMemCompCdTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-07
         * 용       도 : 제목있는 회사코드 리스트 조회
         * Input    값 : MakeMemCompCdTitle(드롭다운리스트객체, 회사코드, 타이틀명, 타이틀 코드값)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// MakeMemCompCdTitle : 제목있는 회사코드 리스트 조회
        /// </summary>
        /// <param name="ddlParamNm">드롭다운리스트객체</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strTitle">제목</param>
        /// <param name="strTitleCd">제목코드</param>
        /// <returns>DataTable</returns>
        public static void MakeMemCompCdTitle(DropDownList ddlParamNm, string strCompNo, string strTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MultiCdInfo.SelectMemCompCd(strCompNo);

            ddlParamNm.Items.Clear();

            ddlParamNm.Items.Add(new ListItem(strCompNo, string.Empty));
            ddlParamNm.Items.Add(new ListItem("Entire", CommValue.AUTH_VALUE_EMPTY));

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CompNm"].ToString(), dr["CompNo"].ToString()));
            }
        }

        #endregion

        #region MakeManuallyMngYear : 수동등록 년도 조회

        /**********************************************************************************************
         * Mehtod   명 : MakeManuallyMngYear
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동등록 년도 조회
         * Input    값 : MakeManuallyMngYear(객체, 섹션코드, 제목)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// MakeManuallyMngYear : 수동등록 년도 조회
        /// </summary>
        /// <param name="ddlParamNm">객체</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strTitle">제목</param>
        public static void MakeManuallyMngYear(DropDownList ddlParamNm, string strRentCd, string strTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MultiCdInfo.SelectManuallyMngYear(strRentCd);

            ddlParamNm.Items.Clear();

            if (!string.IsNullOrEmpty(strTitle))
            {
                ddlParamNm.Items.Add(new ListItem(strTitle, ""));
            }

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["MngYear"].ToString(), dr["MngYear"].ToString()));
            }
        }

        #endregion

        #region MakeManuallyMngMonth : 수동등록 월 조회

        /**********************************************************************************************
         * Mehtod   명 : MakeManuallyMngMonth
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-11
         * 용       도 : 수동등록 월 조회
         * Input    값 : MakeManuallyMngMonth(객체, 섹션코드, 년도, 제목)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// MakeManuallyMngMonth : 수동등록 월 조회
        /// </summary>
        /// <param name="ddlParamNm">객체</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도</param>
        /// <param name="strTitle">제목</param>
        public static void MakeManuallyMngMonth(DropDownList ddlParamNm, string strRentCd, string strYear, string strTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MultiCdInfo.SelectManuallyMngMonth(strRentCd, strYear);

            ddlParamNm.Items.Clear();

            if (!string.IsNullOrEmpty(strTitle))
            {
                ddlParamNm.Items.Add(new ListItem(strTitle, ""));
            }

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["MngMM"].ToString(), dr["MngMM"].ToString()));
            }
        }

        #endregion

        #region MakeManuallyUtilYear : 수동등록 년도 조회

        /**********************************************************************************************
         * Mehtod   명 : MakeManuallyUtilYear
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-12
         * 용       도 : 수동등록 년도 조회
         * Input    값 : MakeManuallyUtilYear(객체, 섹션코드, 제목)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// MakeManuallyUtilYear : 수동등록 년도 조회
        /// </summary>
        /// <param name="ddlParamNm">객체</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strTitle">제목</param>
        public static void MakeManuallyUtilYear(DropDownList ddlParamNm, string strRentCd, string strTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MultiCdInfo.SelectManuallyUtilYear(strRentCd);

            ddlParamNm.Items.Clear();

            if (!string.IsNullOrEmpty(strTitle))
            {
                ddlParamNm.Items.Add(new ListItem(strTitle, ""));
            }

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["MngYear"].ToString(), dr["MngYear"].ToString()));
            }
        }

        #endregion

        #region MakeManuallyUtilMonth : 수동등록 월 조회

        /**********************************************************************************************
         * Mehtod   명 : MakeManuallyUtilMonth
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-12
         * 용       도 : 수동등록 월 조회
         * Input    값 : MakeManuallyUtilMonth(객체, 섹션코드, 년도, 제목)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// MakeManuallyUtilMonth : 수동등록 월 조회
        /// </summary>
        /// <param name="ddlParamNm">객체</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strYear">년도</param>
        /// <param name="strTitle">제목</param>
        public static void MakeManuallyUtilMonth(DropDownList ddlParamNm, string strRentCd, string strYear, string strTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MultiCdInfo.SelectManuallyUtilMonth(strRentCd, strYear);

            ddlParamNm.Items.Clear();

            if (!string.IsNullOrEmpty(strTitle))
            {
                ddlParamNm.Items.Add(new ListItem(strTitle, ""));
            }

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["MngMM"].ToString(), dr["MngMM"].ToString()));
            }
        }

        #endregion
    }
}