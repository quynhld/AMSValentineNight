using System.Data;
using System.Text;
using System.Web.UI.WebControls;

using KN.Common.Base.Code;
using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    /// <summary>
    /// CheckBoxList용 공통코드 Utility
    /// </summary>
    public class CommCdChkUtil
    {
        #region MakeMainCdChkNoTitle : 공통코드(메인코드)로 CheckBoxList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeMainCdChkNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : 공통코드(메인코드)로 CheckBoxList를 생성
         * Input    값 : MakeMainCdChkNoTitle(CheckBoxList 객체, 언어코드, 그룹코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(메인코드)로 CheckBoxList를 생성
        /// </summary>
        /// <param name="rdoParamNm">CheckBoxList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        public static void MakeMainCdChkNoTitle(CheckBoxList rdoParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd, RepeatDirection rptDirect)
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

        public static void MakeMainCdChkNoTitle(CheckBoxList rdoParamNm, string strLangCd, string strGrpCd, RepeatDirection rptDirect)
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

        #region MakeSubCdChkNoTitle : 공통코드(서브코드)로 CheckBoxList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeSubCdChkNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : 공통코드(서브코드)로 CheckBoxList를 생성
         * Input    값 : MakeSubCdChkNoTitle(CheckBoxList 객체, 언어코드, 그룹코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(서브코드)로 CheckBoxList를 생성
        /// </summary>
        /// <param name="rdoParamNm">생성할 CheckBoxList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeSubCdChkNoTitle(CheckBoxList rdoParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd, StringBuilder sbMainCd, RepeatDirection rptDirect)
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

        public static void MakeSubCdChkNoTitle(CheckBoxList rdoParamNm, string strLangCd, string strGrpCd, string strMainCd, RepeatDirection rptDirect)
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

        #region MakeEtcSubCdChkNoTitle : 기타코드로 CheckBoxList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeEtcSubCdChkNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : 기타코드로 CheckBoxList를 생성
         * Input    값 : MakeEtcSubCdChkNoTitle(CheckBoxList 객체, 언어코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 기타코드로 CheckBoxList를 생성
        /// </summary>
        /// <param name="rdoParamNm">생성할 CheckBoxList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeEtcSubCdChkNoTitle(CheckBoxList rdoParamNm, StringBuilder sbLangCd, StringBuilder sbMainCd, RepeatDirection rptDirect)
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

        public static void MakeEtcSubCdChkNoTitle(CheckBoxList rdoParamNm, string strLangCd, string strMainCd, RepeatDirection rptDirect)
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

        #region MakeCommCdChk : CheckBoxList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeCommCdChk
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-16
         * 용       도 : CheckBoxList를 생성
         * Input    값 : MakeCommCdChk(CheckBoxList 객체, DataTable 객체)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// MakeCommCdChk : CheckBoxList를 생성
        /// </summary>
        /// <param name="chkParamNm">생성할 CheckBoxList 객체</param>
        /// <param name="dtParams">DataTable 객체</param>
        public static void MakeCommCdChk(CheckBoxList chkParamNm, DataTable dtParams)
        {
            chkParamNm.Items.Clear();

            foreach (DataRow dr in dtParams.Select())
            {
                chkParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        #endregion
    }
}
