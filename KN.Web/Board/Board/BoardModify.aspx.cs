using System;
using System.Configuration;
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
    public partial class BoardModify : BasePage
    {
        string strBoardTy = string.Empty;
        string strBoardCd = string.Empty;
        string strBoardSeq = string.Empty;

        int intFilePos = 0;

        bool isFirstFile = CommValue.AUTH_VALUE_FALSE;
        bool isSecondFile = CommValue.AUTH_VALUE_FALSE;

        //파일 업로드 경로 설정
        public static readonly string strServerFileUpload = ConfigurationSettings.AppSettings["UploadServerFolder"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DataTable dtReturn = new DataTable();

                    // 게시판일 경우 반드시 게시판관리 테이블에 등록되어야 함.
                    // KN_USP_BRD_SELECT_BOARDMNGINFO_S00
                    dtReturn = BoardInfoBlo.WatchBoardFileCntInfo(Int32.Parse(Master.PAGE_SEQ));
                    LoadBoardData(dtReturn);

                    // 파라미터 체크
                    CheckParams();

                    // 컨트롤 초기화
                    InitControl();

                    // 데이터 로드
                    LoadData();
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
        private void LoadBoardData(DataTable dtReturn)
        {
            DataRow dr = dtReturn.Rows[0];
            txtHfFileCnt.Text = dr["FileAddonCnt"].ToString();
            txtReplyYn.Text = dr["ReplyYn"].ToString();
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {
            // 양영석 : 화일등록 갯수 인자 처리할 것
            if (!IsPostBack)
            {
                int intTotalAuth = 0;

                if (Request.Params[Master.PARAM_DATA1] == null || Request.Params[Master.PARAM_DATA2] == null || Request.Params[Master.PARAM_DATA3] == null)
                {
                    // 게시판정보가 없을 경우 목록으로 리턴
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) ||
                        string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()) ||
                        string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3].ToString()))
                    {
                        // 게시판정보가 없을 경우 목록으로 리턴
                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        strBoardTy = Request.Params[Master.PARAM_DATA1].ToString();
                        strBoardCd = Request.Params[Master.PARAM_DATA2].ToString();
                        strBoardSeq = Request.Params[Master.PARAM_DATA3].ToString();

                        txtHfBoardTy.Text = strBoardTy;
                        txtHfBoardCd.Text = strBoardCd;
                        txtHfBoardSeq.Text = strBoardSeq;

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
                            else
                            {
                                txtHfAuthGrpTy.Text = CommValue.AUTH_VALUE_ENTIRE;
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
            ltModify.Text = TextNm["MODIFY"];
            ltAuthority.Text = TextNm["AUTH"];
            ltTitle.Text = TextNm["TITLE"];
            ltContent.Text = TextNm["CONTENTS"];
            ltFileAddon1.Text = TextNm["FILEADDON"];
            ltFileAddon2.Text = TextNm["FILEADDON"];
            ltFileAddon3.Text = TextNm["FILEADDON"];

            chkFileAddon1.Text = TextNm["DELETE"];
            chkFileAddon2.Text = TextNm["DELETE"];
            chkFileAddon3.Text = TextNm["DELETE"];

            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnRegist.Text = TextNm["MODIFY"];
            lnkbtnRegist.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_TITLE"] + "','" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            // 회사코드 세팅
            MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlAuth, Session["CompCd"].ToString());

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
        /// 데이터를 로드하는 메소드
        /// </summary>
        private void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_BRD_UPDATE_BOARDVIEWCNT_M00
            // KN_USP_BRD_SELECT_BOARDINFO_S01
            dtReturn = BoardInfoBlo.WatchBoardInfo(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), Session["MemAuthTy"].ToString(), Session["CompCd"].ToString(), Session["MemNo"].ToString());

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    DataRow dr = dtReturn.Rows[0];

                    if (!string.IsNullOrEmpty(dr["BoardTitle"].ToString()))
                    {
                        txtTitle.Text = TextLib.StringDecoder(dr["BoardTitle"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dr["BoardContent"].ToString()))
                    {
                        txtContext.Text = TextLib.StringDecoder(dr["BoardContent"].ToString());
                    }

                    if (!string.IsNullOrEmpty(dr["ViewCompNo"].ToString()))
                    {
                        ddlAuth.SelectedValue = dr["ViewCompNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dr["FilePath1"].ToString()) && !string.IsNullOrEmpty(dr["FileRealNm1"].ToString()))
                    {
                        ltFileNm1.Text = dr["FileRealNm1"].ToString();
                        txtHfFilePath1.Text = strServerFileUpload + dr["FilePath1"].ToString().Replace("/", "\\");
                    }
                    else
                    {
                        ltFileNm1.Visible = CommValue.AUTH_VALUE_FALSE;
                        chkFileAddon1.Visible = CommValue.AUTH_VALUE_FALSE;
                    }

                    if (!string.IsNullOrEmpty(dr["FilePath2"].ToString()) && !string.IsNullOrEmpty(dr["FileRealNm2"].ToString()))
                    {
                        ltFileNm2.Text = dr["FileRealNm2"].ToString();
                        txtHfFilePath2.Text = strServerFileUpload + dr["FilePath2"].ToString().Replace("/", "\\");
                    }
                    else
                    {
                        ltFileNm2.Visible = CommValue.AUTH_VALUE_FALSE;
                        chkFileAddon2.Visible = CommValue.AUTH_VALUE_FALSE;
                    }

                    if (!string.IsNullOrEmpty(dr["FilePath3"].ToString()) && !string.IsNullOrEmpty(dr["FileRealNm3"].ToString()))
                    {
                        ltFileNm3.Text = dr["FileRealNm3"].ToString();
                        txtHfFilePath3.Text = strServerFileUpload + dr["FilePath3"].ToString().Replace("/", "\\");
                    }
                    else
                    {
                        ltFileNm3.Visible = CommValue.AUTH_VALUE_FALSE;
                        chkFileAddon3.Visible = CommValue.AUTH_VALUE_FALSE;
                    }

                    if (dr["ViewAuth"].ToString().Equals(CommValue.AUTH_VALUE_ENTIRE))
                    {
                        AuthCheckLib.CheckFullData(chkAuth, txtHfAuthGrpTy);
                    }
                    else
                    {
                        AuthCheckLib.CheckNoData(chkAuth, Int32.Parse(dr["ViewAuth"].ToString()), txtHfAuthGrpTy);
                    }
                }
                else
                {
                    // 게시판정보가 없을 경우 목록으로 리턴
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
            }
            else
            {
                // 게시판정보가 없을 경우 목록으로 리턴
                Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
            }
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
                string strMemNo = string.Empty;

                string strAuth = txtHfAuthGrpTy.Text;
                string strTotalAuth = txtHfTotalGrpTy.Text;

                strMemNo = Session["MemNo"].ToString();

                if (strAuth.Equals(strTotalAuth))
                {
                    strAuth = CommValue.AUTH_VALUE_ENTIRE;
                }

                object[] objReturn = new object[2];

                // KN_USP_BRD_UPDATE_BOARDINFO_M00
                objReturn = BoardInfoBlo.ModifyBoardInfo(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), txtTitle.Text, txtContext.Text, ddlAuth.SelectedValue, strAuth, strAuth, strAuth, Session["CompCd"].ToString(), strMemNo, strUserIP);

                if (objReturn != null)
                {
                    if (fileAddon1.Visible)
                    {
                        string strExistFile = string.Empty;

                        strExistFile = txtHfFilePath1.Text;

                        if (chkFileAddon1.Visible)
                        {
                            if (chkFileAddon1.Checked)
                            {
                                // KN_USP_BRD_DELETE_BOARDADDON_M01
                                BoardInfoBlo.RemoveBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), CommValue.FILEADDON_VALUE_FIRST, strExistFile);
                            }
                        }

                        if (fileAddon1.HasFile)
                        {
                            strOldFileNm = fileAddon1.FileName;
                            strNewFileNm = UploadFile(fileAddon1);

                            if (!string.IsNullOrEmpty(strExistFile))
                            {
                                // KN_USP_BRD_UPDATE_BOARDADDON_M00
                                BoardInfoBlo.ModifyBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), CommValue.FILEADDON_VALUE_FIRST, strNewFileNm,
                                                              fileAddon1.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo, strExistFile);
                            }
                            else
                            {
                                // KN_USP_BRD_INSERT_BOARDADDON_M00
                                BoardInfoBlo.RegistryBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), CommValue.FILEADDON_VALUE_FIRST, strNewFileNm,
                                                                fileAddon1.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                            }

                            isFirstFile = CommValue.AUTH_VALUE_TRUE;
                        }
                        else
                        {
                            if (chkFileAddon1.Visible)
                            {
                                if (chkFileAddon1.Checked)
                                {
                                    // KN_USP_BRD_DELETE_BOARDADDON_M01
                                    BoardInfoBlo.RemoveBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), CommValue.FILEADDON_VALUE_FIRST, strExistFile);
                                }
                            }

                            if (chkFileAddon2.Visible)
                            {
                                if (chkFileAddon2.Checked)
                                {
                                    // KN_USP_BRD_DELETE_BOARDADDON_M01
                                    BoardInfoBlo.RemoveBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), CommValue.FILEADDON_VALUE_SECOND, strExistFile);
                                }
                            }

                            if (chkFileAddon3.Visible)
                            {
                                if (chkFileAddon3.Checked)
                                {
                                    // KN_USP_BRD_DELETE_BOARDADDON_M01
                                    BoardInfoBlo.RemoveBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), CommValue.FILEADDON_VALUE_THIRD, strExistFile);
                                }
                            }
                        }
                    }

                    if (fileAddon2.Visible)
                    {
                        string strExistFile = string.Empty;

                        strExistFile = txtHfFilePath2.Text;

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

                            if (!string.IsNullOrEmpty(strExistFile))
                            {
                                // KN_USP_BRD_UPDATE_BOARDADDON_M00
                                BoardInfoBlo.ModifyBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), intFilePos, strNewFileNm, 
                                                              fileAddon2.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo, strExistFile);
                            }
                            else
                            {
                                // KN_USP_BRD_INSERT_BOARDADDON_M00
                                BoardInfoBlo.RegistryBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), intFilePos, strNewFileNm, 
                                                                fileAddon2.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                            }
                        }
                        else
                        {
                            if (chkFileAddon2.Visible)
                            {
                                if (chkFileAddon2.Checked)
                                {
                                    // KN_USP_BRD_DELETE_BOARDADDON_M01
                                    BoardInfoBlo.RemoveBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), CommValue.FILEADDON_VALUE_SECOND, strExistFile);
                                }
                            }
                        }
                    }

                    if (fileAddon3.Visible)
                    {
                        string strExistFile = string.Empty;

                        strExistFile = txtHfFilePath3.Text;

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

                            if (!string.IsNullOrEmpty(strExistFile))
                            {
                                // KN_USP_BRD_UPDATE_BOARDADDON_M00
                                BoardInfoBlo.ModifyBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), intFilePos, strNewFileNm, 
                                                              fileAddon3.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo, strExistFile);
                            }
                            else
                            {
                                // KN_USP_BRD_INSERT_BOARDADDON_M00
                                BoardInfoBlo.RegistryBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), intFilePos, strNewFileNm,
                                                                fileAddon3.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                            }
                        }
                        else
                        {
                            if (chkFileAddon3.Visible)
                            {
                                if (chkFileAddon3.Checked)
                                {
                                    // KN_USP_BRD_DELETE_BOARDADDON_M01
                                    BoardInfoBlo.RemoveBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), CommValue.FILEADDON_VALUE_THIRD, strExistFile);
                                }
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_MODIFY_ISSUE"] + "','" + Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfBoardTy.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfBoardCd.Text + "')", CommValue.AUTH_VALUE_TRUE);
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