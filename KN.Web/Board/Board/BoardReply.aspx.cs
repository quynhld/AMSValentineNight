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

using KN.Board.Biz;
using KN.Config.Biz;

namespace KN.Web.Board.Board
{
    [Transaction(TransactionOption.Required)]
    public partial class BoardReply : BasePage
    {
        string strBoardTy = string.Empty;
        string strBoardCd = string.Empty;
        string strBoardSeq = string.Empty;

        int intFilePos = 0;

        bool isFirstFile = CommValue.AUTH_VALUE_FALSE;
        bool isSecondFile = CommValue.AUTH_VALUE_FALSE;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 파라미터 체크
                CheckParams();

                // 컨트롤 초기화
                InitControl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {
            // 양영석 : 화일등록 갯수 인자 처리할 것
            if (!IsPostBack)
            {
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

                        /* 그룹 권한 정보를 가져오는 부분 시작 */
                        // KN_USP_BRD_INSERT_BOARDINFO_M00
                        DataTable dtAuth = BoardInfoBlo.WatchBoardInfo(strBoardTy, strBoardCd, Int32.Parse(strBoardSeq), Session["MemAuthTy"].ToString(), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                        if (dtAuth != null)
                        {
                            if (dtAuth.Rows.Count > 0)
                            {
                                txtHfAuthGrpTy.Text = dtAuth.Rows[0]["ViewAuth"].ToString();
                            }
                            else
                            {
                                Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                            }
                        }
                        else
                        {
                            // Parent글 정보가 없을 경우 목록으로 리턴
                            Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                        }
                        /* 그룹 권한 정보를 가져오는 부분 끝 */
                    }
                }
            }
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControl()
        {
            ltReply.Text = TextNm["REPLY"];
            ltTitle.Text = TextNm["TITLE"];
            ltContent.Text = TextNm["CONTENTS"];
            ltFileAddon1.Text = TextNm["FILEADDON"];
            ltFileAddon2.Text = TextNm["FILEADDON"];
            ltFileAddon3.Text = TextNm["FILEADDON"];

            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnRegist.Text = TextNm["REPLY"];
            lnkbtnRegist.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_TITLE"] + "','" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            // 세션체크
            AuthCheckLib.CheckSession();

            StringBuilder sbResetLink = new StringBuilder();
            sbResetLink.Append(Master.PAGE_REPLY + "?" + Master.PARAM_DATA1 + "=");
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

                strMemNo = Session["MemNo"].ToString();

                // KN_USP_BRD_SELECT_BOARDINFO_S02
                DataTable dtAccessReturn = BoardInfoBlo.WatchBoardAccess(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text));

                if (dtAccessReturn != null)
                {
                    if (dtAccessReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        string strViewCompNo = dtAccessReturn.Rows[0]["ViewCompNo"].ToString();

                        // KN_USP_BRD_INSERT_BOARDINFO_M01
                        DataTable dtReturn = BoardInfoBlo.RegistryBoardReply(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(txtHfBoardSeq.Text), txtTitle.Text, txtContext.Text, strViewCompNo,
                                                                             txtHfAuthGrpTy.Text, txtHfAuthGrpTy.Text, txtHfAuthGrpTy.Text, Session["CompCd"].ToString(), strMemNo, strUserIP);

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                            {
                                if (fileAddon1.Visible)
                                {
                                    if (fileAddon1.HasFile)
                                    {
                                        strOldFileNm = fileAddon1.FileName;
                                        strNewFileNm = UploadFile(fileAddon1);

                                        // KN_USP_BRD_INSERT_BOARDADDON_M00
                                        BoardInfoBlo.RegistryBoardAddon(txtHfBoardTy.Text, txtHfBoardCd.Text, Int32.Parse(dtReturn.Rows[0]["BoardSeq"].ToString()), CommValue.FILEADDON_VALUE_FIRST,
                                                                        strNewFileNm, fileAddon1.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);

                                        isFirstFile = CommValue.AUTH_VALUE_TRUE;
                                    }
                                }

                                if (fileAddon2.Visible)
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
                                                                        fileAddon2.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                                    }
                                }

                                if (fileAddon3.Visible)
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
                                                                        fileAddon3.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                                    }
                                }
                            }
                        }

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_REGIST_ISSUE"] + "','" + Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfBoardTy.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfBoardCd.Text + "')", CommValue.AUTH_VALUE_TRUE);
                    }
                }
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
    }
}