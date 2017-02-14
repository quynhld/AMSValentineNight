using System.Data;
using System.Text;
using System.Web.UI.WebControls;

using KN.Common.Base.Code;
using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    /// <summary>
    /// RadioButtonList용 공통코드 Utility
    /// </summary>
    public class CommCdRdoUtil
    {
        #region MakeMainCdRdoNoTitle : 공통코드(메인코드)로 RadioButtonList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeMainCdRdoNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(메인코드)로 RadioButtonList를 생성
         * Input    값 : MakeMainCdRdoNoTitle(RadioButtonList 객체, 언어코드, 그룹코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(메인코드)로 RadioButtonList를 생성
        /// </summary>
        /// <param name="rdoParamNm">RadioButtonList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        public static void MakeMainCdRdoNoTitle(RadioButtonList rdoParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd, RepeatDirection rptDirect)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectMainCdWithNoTitle(sbLangCd, sbGrpCd);

            rdoParamNm.Items.Clear();
            rdoParamNm.RepeatDirection = rptDirect;

            foreach (DataRow dr in dtReturn.Select())
            {
                rdoParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeMainCdRdoNoTitle(RadioButtonList rdoParamNm, string strLangCd, string strGrpCd, RepeatDirection rptDirect)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectMainCdWithNoTitle(strLangCd, strGrpCd);

            rdoParamNm.Items.Clear();
            rdoParamNm.RepeatDirection = rptDirect;

            foreach (DataRow dr in dtReturn.Select())
            {
                rdoParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeSubCdRdoNoTitle : 공통코드(서브코드)로 RadioButtonList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeSubCdRdoNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(서브코드)로 RadioButtonList를 생성
         * Input    값 : MakeSubCdRdoNoTitle(RadioButtonList 객체, 언어코드, 그룹코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(서브코드)로 RadioButtonList를 생성
        /// </summary>
        /// <param name="rdoParamNm">생성할 RadioButtonList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeSubCdRdoNoTitle(RadioButtonList rdoParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd, StringBuilder sbMainCd, RepeatDirection rptDirect)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(sbLangCd, sbGrpCd, sbMainCd);

            rdoParamNm.Items.Clear();
            rdoParamNm.RepeatDirection = rptDirect;

            foreach (DataRow dr in dtReturn.Select())
            {
                rdoParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeSubCdRdoNoTitle(RadioButtonList rdoParamNm, string strLangCd, string strGrpCd, string strMainCd, RepeatDirection rptDirect)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);

            rdoParamNm.Items.Clear();
            rdoParamNm.RepeatDirection = rptDirect;

            foreach (DataRow dr in dtReturn.Select())
            {
                rdoParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeEtcSubCdRdoNoTitle : 기타코드로 RadioButtonList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeEtcSubCdRdoNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 기타코드로 RadioButtonList를 생성
         * Input    값 : MakeEtcSubCdRdoNoTitle(RadioButtonList 객체, 언어코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 기타코드로 RadioButtonList를 생성
        /// </summary>
        /// <param name="rdoParamNm">생성할 RadioButtonList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeEtcSubCdRdoNoTitle(RadioButtonList rdoParamNm, StringBuilder sbLangCd, StringBuilder sbMainCd, RepeatDirection rptDirect)
        {
            DataTable dtReturn = new DataTable();
            StringBuilder sbGrpCd = new StringBuilder(CommValue.COMMCD_VALUE_ETC);

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(sbLangCd, sbGrpCd, sbMainCd);

            rdoParamNm.Items.Clear();
            rdoParamNm.RepeatDirection = rptDirect;

            foreach (DataRow dr in dtReturn.Select())
            {
                rdoParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeEtcSubCdRdoNoTitle(RadioButtonList rdoParamNm, string strLangCd, string strMainCd, RepeatDirection rptDirect)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, CommValue.COMMCD_VALUE_ETC, strMainCd);

            rdoParamNm.Items.Clear();
            rdoParamNm.RepeatDirection = rptDirect;

            foreach (DataRow dr in dtReturn.Select())
            {
                rdoParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeEtcSubCdRdoTitle : 기타코드로 RadioButtonList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeEtcSubCdRdoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-19
         * 용       도 : 기타코드로 RadioButtonList를 생성
         * Input    값 : MakeEtcSubCdRdoTitle(RadioButtonList 객체, 언어코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 기타코드로 RadioButtonList를 생성
        /// </summary>
        /// <param name="rdoParamNm">생성할 RadioButtonList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeEtcSubCdRdoTitle(RadioButtonList rdoParamNm, StringBuilder sbLangCd, StringBuilder sbMainCd, RepeatDirection rptDirect)
        {
            DataTable dtReturn = new DataTable();
            StringBuilder sbGrpCd = new StringBuilder(CommValue.COMMCD_VALUE_ETC);

            dtReturn = CommCdInfo.SelectSubCdWithTitle(sbLangCd, sbGrpCd, sbMainCd);

            rdoParamNm.Items.Clear();
            rdoParamNm.RepeatDirection = rptDirect;

            foreach (DataRow dr in dtReturn.Select())
            {
                rdoParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeEtcSubCdRdoTitle(RadioButtonList rdoParamNm, string strLangCd, string strMainCd, RepeatDirection rptDirect)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, CommValue.COMMCD_VALUE_ETC, strMainCd);

            rdoParamNm.Items.Clear();
            rdoParamNm.RepeatDirection = rptDirect;

            foreach (DataRow dr in dtReturn.Select())
            {
                rdoParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion
    }
}