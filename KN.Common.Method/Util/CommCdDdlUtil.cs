using System.Data;
using System.Text;
using System.Web.UI.WebControls;

using KN.Common.Base.Code;
using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    /// <summary>
    /// DropDownList용 공통코드 Utility
    /// </summary>
    public class CommCdDdlUtil
    {
        #region MakeMainCdDdlTitle : 공통코드(메인코드)로 타이틀 있는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeMainCdDdlTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(메인코드)로 타이틀 있는 DropdownList를 생성
         * Input    값 : MakeMainCdDdlTitle(DropDownList 객체, 언어코드, 그룹코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(메인코드)로 타이틀 있는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">DropDownList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        public static void MakeMainCdDdlTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectMainCdWithTitle(sbLangCd, sbGrpCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeMainCdDdlTitle(DropDownList ddlParamNm, string strLangCd, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectMainCdWithTitle(strLangCd, strGrpCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeMainCdDdlNoTitle : 공통코드(메인코드)로 타이틀 없는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeMainCdDdlNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(메인코드)로 타이틀 없는 DropdownList를 생성
         * Input    값 : MakeMainCdDdlNoTitle(DropDownList 객체, 언어코드, 그룹코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(메인코드)로 타이틀 없는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">DropDownList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        public static void MakeMainCdDdlNoTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectMainCdWithNoTitle(sbLangCd, sbGrpCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeMainCdDdlNoTitle(DropDownList ddlParamNm, string strLangCd, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectMainCdWithNoTitle(strLangCd, strGrpCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeMainCdDdlUserTitle : 공통코드(메인코드)로 사용자정의 타이틀이 있는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeMainCdDdlUserTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(메인코드)로 사용자정의 타이틀이 있는 DropdownList를 생성
         * Input    값 : MakeMainCdDdlUserTitle(DropDownList 객체, 언어코드, 그룹코드, 사용자정의 타이틀)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(메인코드)로 사용자정의 타이틀이 있는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">생성할 DropDownList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        /// <param name="sbTitle">사용자정의 타이틀</param>
        public static void MakeMainCdDdlUserTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd, StringBuilder sbTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectMainCdWithNoTitle(sbLangCd, sbGrpCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }

            ddlParamNm.Items.Insert(0, new ListItem(sbTitle.ToString(), ""));
        }

        public static void MakeMainCdDdlUserTitle(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectMainCdWithNoTitle(strLangCd, strGrpCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }

            ddlParamNm.Items.Insert(0, new ListItem(strTitle, ""));
        }
        #endregion

        #region MakeSubCdDdlTitle : 공통코드(서브코드)로 타이틀 있는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeSubCdDdlTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(서브코드)로 타이틀 있는 DropdownList를 생성
         * Input    값 : MakeSubCdDdlTitle(DropDownList 객체, 언어코드, 그룹코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(서브코드)로 타이틀 있는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">생성할 DropDownList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeSubCdDdlTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(sbLangCd, sbGrpCd, sbMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeSubCdDdlTitle(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeSubCdDdlNoTitle : 공통코드(서브코드)로 타이틀 없는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeSubCdDdlNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(서브코드)로 타이틀 없는 DropdownList를 생성
         * Input    값 : MakeSubCdDdlNoTitle(DropDownList 객체, 언어코드, 그룹코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(서브코드)로 타이틀 없는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">생성할 DropDownList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeSubCdDdlNoTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(sbLangCd, sbGrpCd, sbMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeSubCdDdlNoTitle(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeSubCdDdlUserTitle : 공통코드(서브코드)로 사용자정의 타이틀이 있는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeSubCdDdlUserTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(서브코드)로 사용자정의 타이틀이 있는 DropdownList를 생성
         * Input    값 : MakeSubCdDdlUserTitle(DropDownList 객체, 언어코드, 그룹코드, 메인코드, 사용자정의 타이틀)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(서브코드)로 사용자정의 타이틀이 있는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">생성할 DropDownList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbGrpCd">그룹코드</param>
        /// <param name="sbMainCd">메인코드</param>
        /// <param name="sbTitle">생성할 DropDownList의 제목</param>
        public static void MakeSubCdDdlUserTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbGrpCd, StringBuilder sbMainCd, StringBuilder sbTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(sbLangCd, sbGrpCd, sbMainCd);
            ddlParamNm.Items.Clear();

            ddlParamNm.Items.Add(new ListItem(sbTitle.ToString(), ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeSubCdDdlUserTitle(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd, string strTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);
            ddlParamNm.Items.Clear();

            ddlParamNm.Items.Add(new ListItem(strTitle, ""));

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeEtcSubCdDdlTitle : 기타코드로 타이틀 있는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeEtcSubCdDdlTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 기타코드로 타이틀 있는 DropdownList를 생성
         * Input    값 : MakeEtcSubCdDdlTitle(DropDownList 객체, 언어코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 기타코드로 타이틀 있는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">생성할 DropDownList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeEtcSubCdDdlTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();
            StringBuilder sbGrpCd = new StringBuilder(CommValue.COMMCD_VALUE_ETC);

            dtReturn = CommCdInfo.SelectSubCdWithTitle(sbLangCd, sbGrpCd, sbMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeEtcSubCdDdlTitle(DropDownList ddlParamNm, string strLangCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, CommValue.COMMCD_VALUE_ETC, strMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeEtcSubCdDdlNoTitle : 기타코드로 타이틀 없는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeEtcSubCdDdlNoTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 기타코드로 타이틀 없는 DropdownList를 생성
         * Input    값 : MakeEtcSubCdDdlNoTitle(DropDownList 객체, 언어코드, 메인코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 기타코드로 타이틀 없는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">생성할 DropDownList 객체</param>
        /// <param name="sbLangCd">언어코드</param>
        /// <param name="sbMainCd">메인코드</param>
        public static void MakeEtcSubCdDdlNoTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbMainCd)
        {
            DataTable dtReturn = new DataTable();
            StringBuilder sbGrpCd = new StringBuilder(CommValue.COMMCD_VALUE_ETC);

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(sbLangCd, sbGrpCd, sbMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        public static void MakeEtcSubCdDdlNoTitle(DropDownList ddlParamNm, string strLangCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, CommValue.COMMCD_VALUE_ETC, strMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }
        #endregion

        #region MakeEtcSubCdDdlUserTitle : 기타코드로 사용자정의 타이틀이 있는 DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeEtcSubCdDdlUserTitle
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-30
         * 용       도 : 공통코드(서브코드)로 사용자정의 타이틀이 있는 DropdownList를 생성
         * Input    값 : MakeEtcSubCdDdlUserTitle(DropDownList 객체, 언어코드, 메인코드, 사용자정의 타이틀)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 기타코드로 사용자정의 타이틀이 있는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">생성할 DropDownList 객체</param>
        /// <param name="sbMainCd">메인코드</param>
        /// <param name="sbTitle">생성할 DropDownList의 제목</param>
        public static void MakeEtcSubCdDdlUserTitle(DropDownList ddlParamNm, StringBuilder sbLangCd, StringBuilder sbMainCd, StringBuilder sbTitle)
        {
            DataTable dtReturn = new DataTable();
            StringBuilder sbGrpCd = new StringBuilder(CommValue.COMMCD_VALUE_ETC);

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(sbLangCd, sbGrpCd, sbMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }

            ddlParamNm.Items.Insert(0, new ListItem(sbTitle.ToString(), ""));
        }

        public static void MakeEtcSubCdDdlUserTitle(DropDownList ddlParamNm, string strLangCd, string strMainCd, string strTitle)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, CommValue.COMMCD_VALUE_ETC, strMainCd);
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }

            ddlParamNm.Items.Insert(0, new ListItem(strTitle, ""));
        }
        #endregion

        #region MakeCommCdDdl : DropdownList를 생성
        /**********************************************************************************************
         * Mehtod   명 : MakeCommCdDdl
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-10
         * 용       도 : DropdownList를 생성
         * Input    값 : MakeCommCdDdl(DropDownList 객체, DataTable 객체)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// MakeCommCdDdl : DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">생성할 DropDownList 객체</param>
        /// <param name="dtParams">DataTable 객체</param>
        public static void MakeCommCdDdl(DropDownList ddlParamNm, DataTable dtParams)
        {
            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtParams.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }
        }

        #endregion
    }
}
