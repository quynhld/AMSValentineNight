using System;
using System.Data;
using System.EnterpriseServices;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Board.Biz;
using KN.Config.Biz;

namespace KN.Web.Board.Board
{
    [Transaction(TransactionOption.Required)]
    public partial class BoardWrite : BasePage
    {
        string strBoardTy = string.Empty;
        string strBoardCd = string.Empty;

        int intFilePos = 0;

        bool isFirstFile = CommValue.AUTH_VALUE_FALSE;
        bool isSecondFile = CommValue.AUTH_VALUE_FALSE;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // 파라미터 체크
                    CheckParams();

                    DataTable dtReturn = new DataTable();

                    // 게시판일 경우 반드시 게시판관리 테이블에 등록되어야 함.
                    // KN_USP_BRD_SELECT_BOARDMNGINFO_S00
                    dtReturn = BoardInfoBlo.WatchBoardFileCntInfo(Int32.Parse(Master.PAGE_SEQ));

                    if (dtReturn == null)
                    {
                        // 게시판 조회시 Null값 반환시 목록으로 리턴
                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        if (dtReturn.Rows.Count < CommValue.NUMBER_VALUE_1)
                        {
                            // 게시판관리 테이블에 미등록의 경우 목록으로 리턴
                            Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                        }
                        else
                        {
                            LoadData(dtReturn);
                        }
                    }

                    // 컨트롤 초기화
                    InitControl();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 데이터 로딩하는 메소드
        /// </summary>
        /// <param name="dtReturn"></param>
        private void LoadData(DataTable dtReturn)
        {
            DataRow dr = dtReturn.Rows[0];
            txtHfFileCnt.Text = dr["FileAddonCnt"].ToString();
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {
            // 양영석 : 화일등록 갯수 인자 처리할 것
            if (!IsPostBack)
            {
                int intTotalAuth = CommValue.NUMBER_VALUE_0;

                if (Request.Params[Master.PARAM_DATA1] == null || Request.Params[Master.PARAM_DATA2] == null)
                {
                    // 게시판정보가 없을 경우 목록으로 리턴
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) ||
                        string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                    {
                        // 게시판정보가 없을 경우 목록으로 리턴
                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        strBoardTy = Request.Params[Master.PARAM_DATA1].ToString();
                        strBoardCd = Request.Params[Master.PARAM_DATA2].ToString();

                        txtHfBoardTy.Text = strBoardTy;
                        txtHfBoardCd.Text = strBoardCd;

                        /* 그룹권한 정보를 가져오는 부분 시작 */
                        DataTable dtReturn = new DataTable();

                        // KN_USP_MNG_SELECT_AUTHGRPINFO_S01
                        dtReturn = AuthorityMngBlo.SpreadControlAuthGrpInfo(Session["CompCd"].ToString(), Session["LangCd"].ToString());

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtReturn.Select())
                                {
                                    if (!dr["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
                                    {
                                        chkAuth.Items.Add(new ListItem(dr["MemAuthTyNm"].ToString(), dr["MemAuthTy"].ToString()));
                                    }

                                    intTotalAuth = intTotalAuth + Int32.Parse(dr["MemAuthTy"].ToString());
                                }

                                txtHfTotalGrpTy.Text = intTotalAuth.ToString();

                                if (dtReturn.Rows.Count < CommValue.NUMBER_VALUE_2)
                                {
                                    txtHfAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
                                    chkAuth.Visible = CommValue.AUTH_VALUE_FALSE;
                                }
                            }
                        }
                        else
                        {
                            // 게시판정보가 없을 경우 목록으로 리턴
                            Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                        }
                        /* 그룹권한 정보를 가져오는 부분 끝 */
                    }
                }
            }
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControl()
        {
            ltWrite.Text = TextNm["WRITE"];
            ltAuthority.Text = TextNm["AUTH"];
            ltTitle.Text = TextNm["TITLE"];
            ltContent.Text = TextNm["CONTENTS"];
            ltFileAddon1.Text = TextNm["FILEADDON"];
            ltFileAddon2.Text = TextNm["FILEADDON"];
            ltFileAddon3.Text = TextNm["FILEADDON"];

            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnRegist.Text = TextNm["WRITE"];
            lnkbtnRegist.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_TITLE"] + "','" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            // 회사코드 세팅
            MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlAuth, Session["CompCd"].ToString());

            // 권한정보 세팅
            AuthCheckLib.CheckFullData(chkAuth, txtHfAuthGrpTy);

            trFileAddon1.Visible = CommValue.AUTH_VALUE_FALSE;
            trFileAddon2.Visible = CommValue.AUTH_VALUE_FALSE;
            trFileAddon3.Visible = CommValue.AUTH_VALUE_FALSE;

            if (txtHfFileCnt.Text.Equals(CommValue.NUMBER_VALUE_ONE))
            {
                trFileAddon1.Visible = CommValue.AUTH_VALUE_TRUE;
            }
            else if (txtHfFileCnt.Text.Equals(CommValue.NUMBER_VALUE_TWO))
            {
                trFileAddon1.Visible = CommValue.AUTH_VALUE_TRUE;
                trFileAddon2.Visible = CommValue.AUTH_VALUE_TRUE;
            }
            else if (txtHfFileCnt.Text.Equals(CommValue.NUMBER_VALUE_THREE))
            {
                trFileAddon1.Visible = CommValue.AUTH_VALUE_TRUE;
                trFileAddon2.Visible = CommValue.AUTH_VALUE_TRUE;
                trFileAddon3.Visible = CommValue.AUTH_VALUE_TRUE;
            }

            // 세션체크
            AuthCheckLib.CheckSession();

            // 하단버튼 링크 세팅
            StringBuilder sbResetLink = new StringBuilder();

            sbResetLink.Append(Master.PAGE_WRITE + "?" + Master.PARAM_DATA1 + "=");
            sbResetLink.Append(strBoardTy);
            sbResetLink.Append("&" + Master.PARAM_DATA2 + "=");
            sbResetLink.Append(strBoardCd);

            lnkbtnReset.PostBackUrl = sbResetLink.ToString();

            StringBuilder sbListLink = new StringBuilder();

            sbListLink.Append(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=");
            sbListLink.Append(strBoardTy);
            sbListLink.Append("&" + Master.PARAM_DATA2 + "=");
            sbListLink.Append(strBoardCd);

            lnkbtnList.PostBackUrl = sbListLink.ToString();
        }

        /// <summary>
        /// 게시판을 초기화함.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                txtTitle.Text = string.Empty;
                txtContext.Text = string.Empty;
                AuthCheckLib.CheckFullData(chkAuth, txtHfAuthGrpTy);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 정보를 등록함.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strOldFileNm = string.Empty;
                string strNewFileNm = string.Empty;
                string strUserIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strCompNo = string.Empty;
                string strMemNo = string.Empty;

                string strAuth = txtHfAuthGrpTy.Text;
                string strTotalAuth = txtHfTotalGrpTy.Text;

                strMemNo = Session["MemNo"].ToString();
                strCompNo = Session["CompCd"].ToString();

                if (strAuth.Equals(strTotalAuth))
                {
                    strAuth = CommValue.AUTH_VALUE_ENTIRE;
                }

                // KN_USP_BRD_INSERT_BOARDINFO_M00
                DataTable dtReturn = BoardInfoBlo.RegistryBoardInfo(txtHfBoardTy.Text, txtHfBoardCd.Text, txtTitle.Text, txtContext.Text, ddlAuth.SelectedValue, strAuth, 
                                                                    strAuth, strAuth, strCompNo, strMemNo, strUserIP);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        if (trFileAddon1.Visible)
                        {
                            if (fileAddon1.HasFile)
                            {
                                strOldFileNm = fileAddon1.FileName;
                                strNewFileNm = UploadFile(fileAddon1);

                                // KN_USP_BRD_INSERT_BOARDADDON_M00
                                BoardInfoBlo.RegistryBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(dtReturn.Rows[0]["BoardSeq"].ToString()), CommValue.FILEADDON_VALUE_FIRST,
                                                                strNewFileNm, fileAddon1.FileBytes.GetLength(0).ToString(), strOldFileNm, strCompNo, strMemNo);

                                isFirstFile = CommValue.AUTH_VALUE_TRUE;
                            }
                        }

                        if (trFileAddon2.Visible)
                        {
                            if (fileAddon2.HasFile)
                            {
                                strOldFileNm = fileAddon2.FileName;
                                strNewFileNm = UploadFile(fileAddon2);

                                if (isFirstFile)
                                {
                                    intFilePos = CommValue.FILEADDON_VALUE_SECOND;
                                    isSecondFile = CommValue.AUTH_VALUE_TRUE;
                                }
                                else
                                {
                                    intFilePos = CommValue.FILEADDON_VALUE_FIRST;
                                    isFirstFile = CommValue.AUTH_VALUE_TRUE;
                                }

                                // KN_USP_BRD_INSERT_BOARDADDON_M00
                                BoardInfoBlo.RegistryBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(dtReturn.Rows[0]["BoardSeq"].ToString()), intFilePos, strNewFileNm, 
                                                                fileAddon2.FileBytes.GetLength(0).ToString(), strOldFileNm, strCompNo, strMemNo);
                            }
                        }

                        if (trFileAddon3.Visible)
                        {
                            if (fileAddon3.HasFile)
                            {
                                strOldFileNm = fileAddon3.FileName;
                                strNewFileNm = UploadFile(fileAddon3);

                                if (isFirstFile)
                                {
                                    if (isSecondFile)
                                    {
                                        intFilePos = CommValue.FILEADDON_VALUE_THIRD;
                                    }
                                    else
                                    {
                                        intFilePos = CommValue.FILEADDON_VALUE_SECOND;
                                    }
                                }
                                else
                                {
                                    intFilePos = CommValue.FILEADDON_VALUE_FIRST;
                                }

                                // KN_USP_BRD_INSERT_BOARDADDON_M00
                                BoardInfoBlo.RegistryBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(dtReturn.Rows[0]["BoardSeq"].ToString()), intFilePos, strNewFileNm,
                                                                fileAddon3.FileBytes.GetLength(0).ToString(), strOldFileNm, strCompNo, strMemNo);
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_REGIST_ISSUE"] + "','" + Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfBoardTy.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfBoardCd.Text + "')", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 화일을 업로드 처리함.
        /// </summary>
        /// <param name="fuFiles"></param>
        /// <returns></returns>
        public string UploadFile(FileUpload fuFiles)
        {
            string strReturn = string.Empty;
            object[] objReturns = FileLib.UploadImageFiles(fuFiles, "", "Images");

            if (objReturns != null)
            {
                strReturn = objReturns[1].ToString();
                return strReturn;
            }
            else
            {
                return null;
            }
        }

        protected void ddlAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int intTotalAuth = CommValue.NUMBER_VALUE_0;

                /* 그룹권한 정보를 가져오는 부분 시작 */
                DataTable dtReturn = new DataTable();

                // KN_USP_MNG_SELECT_AUTHGRPINFO_S01
                dtReturn = AuthorityMngBlo.SpreadControlAuthGrpInfo(ddlAuth.SelectedValue, Session["LangCd"].ToString());

                if (dtReturn != null)
                {
                    chkAuth.Items.Clear();

                    if (dtReturn.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtReturn.Select())
                        {
                            if (!dr["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
                            {
                                chkAuth.Items.Add(new ListItem(dr["MemAuthTyNm"].ToString(), dr["MemAuthTy"].ToString()));
                            }

                            intTotalAuth = intTotalAuth + Int32.Parse(dr["MemAuthTy"].ToString());
                        }

                        txtHfTotalGrpTy.Text = intTotalAuth.ToString();

                        if (dtReturn.Rows.Count < 2)
                        {
                            txtHfAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
                            chkAuth.Visible = CommValue.AUTH_VALUE_FALSE;
                        }
                    }

                    // 권한정보 세팅
                    AuthCheckLib.CheckFullData(chkAuth, txtHfAuthGrpTy);
                }
                else
                {
                    // 게시판정보가 없을 경우 목록으로 리턴
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                /* 그룹권한 정보를 가져오는 부분 끝 */
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void chkAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuthCheckLib.CheckchkData(chkAuth, txtHfAuthGrpTy);
        }
    }
}