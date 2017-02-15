using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Board.Biz;

namespace KN.Web.Board.Board
{
    public partial class BoardView : BasePage
    {
        string strBoardTy = string.Empty;
        string strBoardCd = string.Empty;

        int intBoardSeq = CommValue.NUMBER_VALUE_0;

        //파일 업로드 경로 설정
        public static readonly string strDBFileUpload = ConfigurationSettings.AppSettings["UploadDBFolder"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 파라미터 체크
                CheckParams();

                if (Master.PAGE_SEQ.Equals(CommValue.PAGESEQ_VALUE_MAIN))
                {
                    if (strBoardTy.Equals(CommValue.BOARD_TYPE_VALUE_NOTICE))
                    {
                        Master.PAGE_SEQ = CommValue.PAGESEQ_VALUE_NOTICE;
                    }
                    else
                    {
                        Master.PAGE_SEQ = CommValue.PAGESEQ_VALUE_ARCHIVES;
                    }
                }

                DataTable dtMemoReturn = new DataTable();

                // KN_USP_BRD_SELECT_BOARDMNGINFO_S00
                dtMemoReturn = BoardInfoBlo.WatchBoardFileCntInfo(Int32.Parse(Master.PAGE_SEQ));

                LoadBoardData(dtMemoReturn);

                DataTable dtBoardReturn = new DataTable();

                // KN_USP_BRD_UPDATE_BOARDVIEWCNT_M00
                // KN_USP_BRD_SELECT_BOARDINFO_S01
                dtBoardReturn = BoardInfoBlo.WatchBoardInfo(strBoardTy, strBoardCd, intBoardSeq, Session["MemAuthTy"].ToString(), Session["CompCd"].ToString(), Session["MemNo"].ToString());

                if (dtBoardReturn == null)
                {
                    // 게시판 조회시 Null값 반환시 목록으로 리턴
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    if (dtBoardReturn.Rows.Count < CommValue.NUMBER_VALUE_1)
                    {
                        // 조회되는 글이 없을 경우 목록으로 리턴
                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        LoadData(dtBoardReturn);
                    }
                }

                // 컨트롤 초기화
                InitControls();

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            ltView.Text = TextNm["DETAILVIEW"];
            ltTitle.Text = TextNm["TITLE"];
            ltName.Text = TextNm["WRITE_NAME"];
            ltRegistDate.Text = TextNm["REGISTDATE"];
            ltModifyDate.Text = TextNm["MODIFYDATE"];
            ltContent.Text = TextNm["CONTENTS"];
            ltAccessIP.Text = TextNm["ACCESSIP"];
            ltFileAddon1.Text = TextNm["FILEADDON"];
            ltFileAddon2.Text = TextNm["FILEADDON"];
            ltFileAddon3.Text = TextNm["FILEADDON"];

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.Visible = Master.isModDelAuthOk;
            lnkbtnReply.Text = TextNm["REPLY"];

            if (txtReplyYn.Text.Equals(CommValue.CHOICE_VALUE_NO))
            {
                lnkbtnReply.Visible = CommValue.AUTH_VALUE_FALSE;
            }
            else
            {
                lnkbtnReply.Visible = Master.isWriteAuthOk;
            }

            trFileAddon1.Visible = CommValue.AUTH_VALUE_FALSE;
            trFileAddon2.Visible = CommValue.AUTH_VALUE_FALSE;
            trFileAddon3.Visible = CommValue.AUTH_VALUE_FALSE;

            if (!txtHfFilePath1.Text.Equals(""))
            {
                trFileAddon1.Visible = CommValue.AUTH_VALUE_TRUE;
            }

            if (!txtHfFilePath2.Text.Equals(""))
            {
                trFileAddon2.Visible = CommValue.AUTH_VALUE_TRUE;
            }
            if (!txtHfFilePath3.Text.Equals(""))
            {
                trFileAddon3.Visible = CommValue.AUTH_VALUE_TRUE;
            }

            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnDelete.Visible = Master.isModDelAuthOk;
            lnkbtnList.Text = TextNm["LIST"];

            lnkbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ISSUE"] + "')";
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 양영석 : 화일등록 갯수 인자 처리할 것
            if (Request.Params[Master.PARAM_DATA1] == null)
            {
                // 게시판 구분정보가 없을 경우 목록으로 리턴
                Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
            }
            else
            {
                if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    // 게시판 구분정보가 없을 경우 목록으로 리턴
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    strBoardTy = Request.Params[Master.PARAM_DATA1].ToString();

                    if (Request.Params[Master.PARAM_DATA2] == null)
                    {
                        // 게시판 코드정보가 없을 경우 목록으로 리턴
                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                        {
                            // 게시판 코드정보가 없을 경우 목록으로 리턴
                            Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                        }
                        else
                        {
                            strBoardCd = Request.Params[Master.PARAM_DATA2].ToString();

                            if (Request.Params[Master.PARAM_DATA3] == null)
                            {
                                // 게시판 상세번호정보가 없을 경우 목록으로 리턴
                                Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3].ToString()))
                                {
                                    // 게시판 상세번호정보가 없을 경우 목록으로 리턴
                                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                                }
                                else
                                {
                                    intBoardSeq = Int32.Parse(Request.Params[Master.PARAM_DATA3].ToString());

                                    StringBuilder sbReplyLink = new StringBuilder();
                                    sbReplyLink.Append(Master.PAGE_REPLY + "?" + Master.PARAM_DATA1 + "=");
                                    sbReplyLink.Append(strBoardTy);
                                    sbReplyLink.Append("&" + Master.PARAM_DATA2 + "=");
                                    sbReplyLink.Append(strBoardCd);
                                    sbReplyLink.Append("&" + Master.PARAM_DATA3 + "=");
                                    sbReplyLink.Append(intBoardSeq.ToString());

                                    lnkbtnReply.PostBackUrl = sbReplyLink.ToString();

                                    StringBuilder sbListLink = new StringBuilder();
                                    sbListLink.Append(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=");
                                    sbListLink.Append(strBoardTy);
                                    sbListLink.Append("&" + Master.PARAM_DATA2 + "=");
                                    sbListLink.Append(strBoardCd);

                                    lnkbtnList.PostBackUrl = sbListLink.ToString();

                                    StringBuilder sbModifyLink = new StringBuilder();
                                    sbModifyLink.Append(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=");
                                    sbModifyLink.Append(strBoardTy);
                                    sbModifyLink.Append("&" + Master.PARAM_DATA2 + "=");
                                    sbModifyLink.Append(strBoardCd);
                                    sbModifyLink.Append("&" + Master.PARAM_DATA3 + "=");
                                    sbModifyLink.Append(intBoardSeq.ToString());

                                    lnkbtnModify.PostBackUrl = sbModifyLink.ToString();

                                    txtHfBoardTy.Text = strBoardTy;
                                    txtHfBoardCd.Text = strBoardCd;
                                    txtHfBoardSeq.Text = intBoardSeq.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 데이터 로딩하는 메소드
        /// </summary>
        /// <param name="dtParams"></param>
        private void LoadBoardData(DataTable dtParams)
        {
            DataRow dr = dtParams.Rows[0];
            txtHfFileCnt.Text = dr["FileAddonCnt"].ToString();
            txtReplyYn.Text = dr["ReplyYn"].ToString();
        }

        /// <summary>
        /// 데이터 로딩하는 메소드
        /// </summary>
        /// <param name="dtParams"></param>
        private void LoadData(DataTable dtParams)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataRow dr = dtParams.Rows[0];

            StringBuilder sbInsDt = new StringBuilder();
            StringBuilder sbModDt = new StringBuilder();

            string strInsDt = string.Empty;
            string strModDt = string.Empty;

            ltInsTitle.Text = dr["BoardTitle"].ToString();
            ltInsNm.Text = dr["ModMemNm"].ToString();


            if (!string.IsNullOrEmpty(dr["InsDt"].ToString()))
            {
                strInsDt = dr["InsDt"].ToString();
                sbInsDt.Append(strInsDt.Substring(0, 4));
                sbInsDt.Append(".");
                sbInsDt.Append(strInsDt.Substring(4, 2));
                sbInsDt.Append(".");
                sbInsDt.Append(strInsDt.Substring(6, 2));

                ltInsRegDate.Text = sbInsDt.ToString();
            }

            if (!string.IsNullOrEmpty(dr["ModDt"].ToString()))
            {
                strModDt = dr["ModDt"].ToString();
                sbModDt.Append(strModDt.Substring(0, 4));
                sbModDt.Append(".");
                sbModDt.Append(strModDt.Substring(4, 2));
                sbModDt.Append(".");
                sbModDt.Append(strModDt.Substring(6, 2));

                ltinsModDate.Text = sbModDt.ToString();
            }

            ltInsAccessIP.Text = dr["ModMemIP"].ToString();
            ltInsContent.Text = dr["BoardContent"].ToString();

            if (!string.IsNullOrEmpty(dr["FileRealNm1"].ToString()))
            {
                StringBuilder sbLink = new StringBuilder();

                sbLink.Append("<a href='");
                sbLink.Append(strDBFileUpload);
                sbLink.Append(dr["FilePath1"].ToString());
                sbLink.Append("'>");
                sbLink.Append(dr["FileRealNm1"].ToString());
                sbLink.Append("</a>");

                ltInsFileAddon1.Text = sbLink.ToString();
                txtHfFilePath1.Text = dr["FilePath1"].ToString();
            }

            if (!string.IsNullOrEmpty(dr["FileRealNm2"].ToString()))
            {
                StringBuilder sbLink = new StringBuilder();

                sbLink.Append("<a href='");
                sbLink.Append(strDBFileUpload);
                sbLink.Append(dr["FilePath2"].ToString());
                sbLink.Append("'>");
                sbLink.Append(dr["FileRealNm2"].ToString());
                sbLink.Append("</a>");

                ltInsFileAddon2.Text = sbLink.ToString();
                txtHfFilePath2.Text = dr["FilePath2"].ToString();
            }

            if (!string.IsNullOrEmpty(dr["FileRealNm3"].ToString()))
            {
                StringBuilder sbLink = new StringBuilder();

                sbLink.Append("<a href='");
                sbLink.Append(strDBFileUpload);
                sbLink.Append(dr["FilePath3"].ToString());
                sbLink.Append("'>");
                sbLink.Append(dr["FileRealNm3"].ToString());
                sbLink.Append("</a>");

                ltInsFileAddon3.Text = sbLink.ToString();
                txtHfFilePath3.Text = dr["FilePath3"].ToString();
            }

            if (!string.IsNullOrEmpty(dr["ParentYn"].ToString()))
            {
                if (dr["ParentYn"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                {
                    lnkbtnDelete.OnClientClick = "javascript:return fnNoDelete('" + AlertNm["INFO_CANT_DELETE_ISSUE"] + "');";
                }
            }

            if (!Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
            {
                if (Session["MemNo"].ToString().Equals(dr["ModMemNo"].ToString()))
                {
                    lnkbtnDelete.Visible = CommValue.AUTH_VALUE_TRUE;
                    lnkbtnModify.Visible = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    lnkbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;
                    lnkbtnModify.Visible = CommValue.AUTH_VALUE_FALSE;
                }
            }
        }

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnReply_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                BoardInfoBlo.RemoveDetailView(strBoardTy, strBoardCd, intBoardSeq, txtHfFilePath1.Text, txtHfFilePath2.Text, txtHfFilePath3.Text);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteItem", "javascript:fnAlert('" + AlertNm["INFO_DELETE_ISSUE"] + "','" + Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfBoardTy.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfBoardCd.Text + "')", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}