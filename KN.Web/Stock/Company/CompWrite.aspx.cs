using System;
using System.Data;
using System.EnterpriseServices;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Stock.Biz;

namespace KN.Web.Stock.Company
{
    [Transaction(TransactionOption.Required)]
    public partial class CompWrite : BasePage
    {
        int intFilePos = 0;
        bool isFirstFile = CommValue.AUTH_VALUE_FALSE;
        bool isSecondFile = CommValue.AUTH_VALUE_FALSE;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
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
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControl()
        {
            ltCompNm.Text = TextNm["COMPNM"];
            ltRepresentiveNm.Text = TextNm["CEO"];
            ltChargerNm.Text = TextNm["CHARGERNM"];
            ltIndus.Text = TextNm["INDUS"];
            ltTel.Text = TextNm["TEL"];
            ltFax.Text = TextNm["FAX"];
            ltAddr.Text = TextNm["ADDR"];
            ltCompTy.Text = TextNm["COMPTY"];
            ltFileAddon1.Text = TextNm["FILEADDON"];
            ltFileAddon2.Text = TextNm["FILEADDON"];
            ltFileAddon3.Text = TextNm["FILEADDON"];
            ltIntro.Text = TextNm["INTRO"];

            txtEtcIndus.Enabled = CommValue.AUTH_VALUE_FALSE;

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlIndus, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_BUSINESS);

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlCompTyCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_COMTY);

            txtCompTelFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompTelMidNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompTelRearNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtCompFaxFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompFaxMidNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompFaxRearNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnRegist.Text = TextNm["WRITE"];
            lnkbtnRegist.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            lnkbtnRegist.Visible = Master.isWriteAuthOk;
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
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_WRITE, CommValue.AUTH_VALUE_FALSE);
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
                string strMemNo = Session["MemNo"].ToString();

                // KN_USP_STK_INSERT_COMPINFO_S00
                DataTable dtReturn = CompInfoBlo.RegistryCompInfo(txtCompNm.Text, ddlIndus.SelectedValue, txtEtcIndus.Text, txtRepresentiveNm.Text, txtIntro.Text, 
                                                                  txtCompTelFrontNo.Text, txtCompTelMidNo.Text, txtCompTelRearNo.Text, 
                                                                  txtCompFaxFrontNo.Text, txtCompFaxMidNo.Text, txtCompFaxRearNo.Text,
                                                                  txtChargerNm.Text, "", "", ddlCompTyCd.SelectedValue, txtAddr.Text, txtDetAddr.Text,
                                                                  Session["CompCd"].ToString(), strMemNo, strUserIP);
                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        if (fileAddon1.Visible)
                        {
                            if (fileAddon1.HasFile)
                            {
                                strOldFileNm = fileAddon1.FileName;
                                strNewFileNm = UploadFile(fileAddon1);

                                // KN_USP_STK_INSERT_COMPADDON_M00
                                CompInfoBlo.RegistryCompAddon(dtReturn.Rows[0]["CompNo"].ToString(), CommValue.FILEADDON_VALUE_FIRST,
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

                                // KN_USP_STK_INSERT_COMPADDON_M00
                                CompInfoBlo.RegistryCompAddon(dtReturn.Rows[0]["CompNo"].ToString(), intFilePos,
                                                              strNewFileNm, fileAddon2.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
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
                                
                                // KN_USP_STK_INSERT_COMPADDON_M00
                                CompInfoBlo.RegistryCompAddon(dtReturn.Rows[0]["CompNo"].ToString(), intFilePos,
                                                              strNewFileNm, fileAddon3.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_REGIST_ISSUE"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 목록으로 이동
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
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

        protected void ddlIndus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIndus.SelectedValue.Equals(CommValue.CODE_VALUE_ETC))
            {
                txtEtcIndus.Enabled = CommValue.AUTH_VALUE_TRUE;
            }
            else
            {
                txtEtcIndus.Text = string.Empty;
                txtEtcIndus.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
        }
    }
}
