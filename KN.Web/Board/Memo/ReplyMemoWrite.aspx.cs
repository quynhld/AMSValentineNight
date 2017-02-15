using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Board.Biz;

namespace KN.Web.Board.Memo
{
    public partial class ReplyMemoWrite : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // 파라미터 체크
                    CheckParams();

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
        /// 파라미터 체크하는 메소드
        /// </summary>
        private void CheckParams()
        {
            // 양영석 : 화일등록 갯수 인자 처리할 것
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
                {
                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2]))
                    {
                        if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3]))
                        {
                            txtHfMemoSeq.Text = Request.Params[Master.PARAM_DATA1].ToString();
                            txtHfInsMemNo.Text = Request.Params[Master.PARAM_DATA2].ToString();
                            txtHfInsCompNo.Text = Request.Params[Master.PARAM_DATA3].ToString();
                        }
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
            ltTitle.Text = TextNm["TITLE"];
            ltContent.Text = TextNm["CONTENTS"];
            ltFileAddon.Text = TextNm["FILEADDON"];

            lnkbtnRegist.Text = TextNm["SEND"];
            lnkbtnRegist.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_TITLE"] + "','" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";
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

                string strUserIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strMemNo = txtHfInsMemNo.Text;
                string strCompNo = txtHfInsCompNo.Text;

                string strOldFileNm = string.Empty;
                string strNewFileNm = string.Empty;

                // KN_USP_BRD_INSERT_MEMOINFO_M00
                MemoWriteUtil.RegistrySendMemoBoard(txtTitle.Text, txtContext.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strUserIP);

                // KN_USP_BRD_INSERT_MEMOINFO_S00
                DataTable dtReturn = MemoMngBlo.RegistrySendMemoDetail(strCompNo, strMemNo);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        if (fileAddon.Visible)
                        {
                            if (fileAddon.HasFile)
                            {
                                strOldFileNm = fileAddon.FileName;
                                strNewFileNm = UploadFile(fileAddon);

                                // KN_USP_BRD_INSERT_MEMOADDON_M00
                                MemoWriteUtil.RegistryMemoAddon(Int32.Parse(dtReturn.Rows[0]["MemoSeq"].ToString()), strNewFileNm, fileAddon.FileBytes.GetLength(0).ToString(), strOldFileNm);
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_REGIST_ISSUE"] + "','" + Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + strMemNo + "')", CommValue.AUTH_VALUE_TRUE);

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