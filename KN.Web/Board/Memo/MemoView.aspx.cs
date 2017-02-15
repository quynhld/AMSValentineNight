using System;
using System.Configuration;
using System.Data;
using System.Text;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Board.Biz;

namespace KN.Web.Board.Memo
{
    public partial class MemoView : BasePage
    {
        int intMemoSeq;

        //파일 업로드 경로 설정
        public static readonly string strDBFileUpload = ConfigurationSettings.AppSettings["UploadDBFolder"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 컨트롤 초기화
                InitControls();

                // 파라미터 체크
                CheckParams();

                DataTable dtReturn = new DataTable();

                // KN_USP_BRD_SELECT_MEMODETAILINFO_S01
                dtReturn = MemoMngBlo.WatchMemoDetail(intMemoSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString());

                if (dtReturn == null)
                {
                    // 게시판 조회시 Null값 반환시 목록으로 리턴
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    if (dtReturn.Rows.Count < CommValue.NUMBER_VALUE_1)
                    {
                        // 조회되는 글이 없을 경우 목록으로 리턴
                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        LoadData(dtReturn);
                    }
                }
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
            ltView.Text = TextNm["VIEWMEMO"];
            ltTitle.Text = TextNm["TITLE"];
            ltNm.Text = TextNm["SENDER"];
            ltReceiveDate.Text = TextNm["RECEIVEDTIME"];
            ltContent.Text = TextNm["CONTENTS"];
            ltFileAddon.Text = TextNm["FILEADDON"];

            lnkbtnReply.Text = TextNm["SENDREPLY"];
            lnkbtnReply.Visible = Master.isWriteAuthOk;
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
                    intMemoSeq = Int32.Parse(Request.Params[Master.PARAM_DATA1].ToString());
                }
            }
        }

        /// <summary>
        /// 데이터 로딩하는 메소드
        /// </summary>
        /// <param name="dtReturn"></param>
        private void LoadData(DataTable dtReturn)
        {
            DataRow dr = dtReturn.Rows[0];

            StringBuilder sbInsDate = new StringBuilder();
            StringBuilder sbModDate = new StringBuilder();

            string strInsDate = string.Empty;
            string strModDate = string.Empty;

            ltInsTitle.Text = TextLib.StringDecoder(dr["MemoTitle"].ToString());
            ltInsNm.Text = TextLib.StringDecoder(dr["MemNm"].ToString());
            txtHfInsMemNo.Text = TextLib.StringDecoder(dr["InsMemNo"].ToString());
            txtHfInsCompNo.Text = TextLib.StringDecoder(dr["InsCompNo"].ToString());
            
            txtHfMemoSeq.Text = TextLib.StringDecoder(dr["MemoSeq"].ToString());

            if (!string.IsNullOrEmpty(dr["InsDate"].ToString()))
            {

                ltInsReceiveDate.Text = dr["InsDate"].ToString();
            }

            ltInsContent.Text = dr["MemoContent"].ToString();

            if (!string.IsNullOrEmpty(dr["FileRealNm"].ToString()))
            {
                StringBuilder sbLink = new StringBuilder();

                sbLink.Append("<a href='");
                sbLink.Append(strDBFileUpload);
                sbLink.Append(TextLib.StringDecoder(dr["FilePath"].ToString()));
                sbLink.Append("'>");
                sbLink.Append(TextLib.StringDecoder(dr["FileRealNm"].ToString()));
                sbLink.Append("</a>");

                ltInsFileAddon.Text = sbLink.ToString();
                txtHfFilePath.Text = TextLib.StringDecoder(dr["FilePath"].ToString());
            }

            if (dr["CheckYn"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
            {
                // KN_USP_MNG_UPDATE_MEMOINFO_M00
                MemoMngBlo.ModifyMemoInfo(Int32.Parse(txtHfMemoSeq.Text), Session["CompCd"].ToString(), Session["MemNo"].ToString());
            }
        }

        protected void lnkbtnReply_Click(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            Response.Redirect(Master.PAGE_REPLY + "?" + Master.PARAM_DATA1 + "=" + txtHfMemoSeq.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfInsMemNo.Text + "&" + Master.PARAM_DATA3 + "=" + txtHfInsCompNo.Text);
        }

        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_BRD_DELETE_MEMOINFO_M00
                // KN_USP_BRD_DELETE_MEMOADDON_M00
                MemoMngBlo.RemoveMemoInfo(intMemoSeq, Session["CompCd"].ToString(), Session["MemNo"].ToString(), txtHfFilePath.Text);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Session["MemNo"].ToString());

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
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Session["MemNo"].ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}