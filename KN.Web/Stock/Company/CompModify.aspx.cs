using System;
using System.Configuration;
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
    public partial class CompModify : BasePage
    {
        int intFilePos = 0;

        bool isFirstFile = CommValue.AUTH_VALUE_FALSE;
        bool isSecondFile = CommValue.AUTH_VALUE_FALSE;

        //파일 업로드 경로 설정
        public static readonly string strServerFileUpload = ConfigurationSettings.AppSettings["UploadServerFolder"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        // 컨트롤 초기화
                        InitControl();

                        LoadData();
                    }
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
        private bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    txtHfCompNo.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
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

            chkFileAddon1.Text = TextNm["DELETE"];
            chkFileAddon2.Text = TextNm["DELETE"];
            chkFileAddon3.Text = TextNm["DELETE"];

            txtEtcIndus.Enabled = CommValue.AUTH_VALUE_FALSE;

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlIndus, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_BUSINESS);

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlCompTyCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_COMTY);

            txtCompTelFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompTelMidNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompTelRearNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            txtCompFaxFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompFaxMidNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtCompFaxRearNo.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            lnkbtnReset.Text = TextNm["CANCEL"];
            lnkbtnRegist.Text = TextNm["WRITE"];
            lnkbtnRegist.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnList.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // KN_USP_STK_SELECT_COMPINFO_S01
            DataTable dtReturn = CompInfoBlo.WatchCompInfo(txtHfCompNo.Text, Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompNm"].ToString()))
                {
                    txtCompNm.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CompNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["PresidentNm"].ToString()))
                {
                    txtRepresentiveNm.Text = TextLib.StringDecoder(dtReturn.Rows[0]["PresidentNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["ChargerNm"].ToString()))
                {
                    txtChargerNm.Text = TextLib.StringDecoder(dtReturn.Rows[0]["ChargerNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["BizIndusCd"].ToString()))
                {
                    ddlIndus.SelectedValue = TextLib.StringDecoder(dtReturn.Rows[0]["BizIndusCd"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["BizNm"].ToString()))
                {
                    txtEtcIndus.Text = TextLib.StringDecoder(dtReturn.Rows[0]["BizNm"].ToString());
                }
                else
                {
                    txtEtcIndus.Enabled = CommValue.AUTH_VALUE_FALSE;
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompTyCd"].ToString()))
                {
                    ddlCompTyCd.SelectedValue = TextLib.StringDecoder(dtReturn.Rows[0]["CompTyCd"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompTelFrontNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtReturn.Rows[0]["CompTelMidNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtReturn.Rows[0]["CompTelRearNo"].ToString()))
                {
                    txtCompTelFrontNo.Text = dtReturn.Rows[0]["CompTelFrontNo"].ToString();
                    txtCompTelMidNo.Text = dtReturn.Rows[0]["CompTelMidNo"].ToString();
                    txtCompTelRearNo.Text = dtReturn.Rows[0]["CompTelRearNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompFaxFrontNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtReturn.Rows[0]["CompFaxMidNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtReturn.Rows[0]["CompFaxRearNo"].ToString()))
                {
                    txtCompFaxFrontNo.Text = dtReturn.Rows[0]["CompFaxFrontNo"].ToString();
                    txtCompFaxMidNo.Text = dtReturn.Rows[0]["CompFaxMidNo"].ToString();
                    txtCompFaxRearNo.Text = dtReturn.Rows[0]["CompFaxRearNo"].ToString();
                }


                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Addr"].ToString()))
                {
                    txtAddr.Text = TextLib.StringDecoder(dtReturn.Rows[0]["Addr"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DetAddr"].ToString()))
                {
                    txtDetAddr.Text = TextLib.StringDecoder(dtReturn.Rows[0]["DetAddr"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["FilePath1"].ToString()))
                {
                    lblFileNm1.Text = dtReturn.Rows[0]["FileRealNm1"].ToString();
                    txtHfFilePath1.Text = dtReturn.Rows[0]["FilePath1"].ToString().Replace("/", "\\");
                }
                else
                {
                    lblFileNm1.Visible = CommValue.AUTH_VALUE_FALSE;
                    chkFileAddon1.Visible = CommValue.AUTH_VALUE_FALSE;
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["FilePath2"].ToString()))
                {
                    lblFileNm2.Text = dtReturn.Rows[0]["FileRealNm2"].ToString();
                    txtHfFilePath2.Text = dtReturn.Rows[0]["FilePath2"].ToString().Replace("/", "\\");
                }
                else
                {
                    lblFileNm2.Visible = CommValue.AUTH_VALUE_FALSE;
                    chkFileAddon2.Visible = CommValue.AUTH_VALUE_FALSE;
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["FilePath3"].ToString()))
                {
                    lblFileNm3.Text = dtReturn.Rows[0]["FileRealNm3"].ToString();
                    txtHfFilePath3.Text = dtReturn.Rows[0]["FilePath3"].ToString().Replace("/", "\\");
                }
                else
                {
                    lblFileNm3.Visible = CommValue.AUTH_VALUE_FALSE;
                    chkFileAddon3.Visible = CommValue.AUTH_VALUE_FALSE;
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Introduce"].ToString()))
                {
                    txtIntro.Text = TextLib.StringDecoder(dtReturn.Rows[0]["Introduce"].ToString());
                }
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
                string strMemNo = string.Empty;

                object [] objReturn = new object[2];

                // KN_USP_STK_UPDATE_COMPINFO_M00
                objReturn = CompInfoBlo.ModifyCompInfo(txtHfCompNo.Text, txtCompNm.Text, ddlIndus.SelectedValue, txtEtcIndus.Text, txtRepresentiveNm.Text, txtIntro.Text,
                                                       txtCompTelFrontNo.Text, txtCompTelMidNo.Text, txtCompTelRearNo.Text,
                                                       txtCompFaxFrontNo.Text, txtCompFaxMidNo.Text, txtCompFaxRearNo.Text,
                                                       txtChargerNm.Text, "", "", ddlCompTyCd.SelectedValue, txtAddr.Text, txtDetAddr.Text,
                                                       Session["CompCd"].ToString(), strMemNo, strUserIP);
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
                                // KN_USP_RES_DELETE_COMPADDON_M01
                                CompInfoBlo.RemoveCompAddon(txtHfCompNo.Text, CommValue.FILEADDON_VALUE_FIRST, strExistFile);
                            }
                        }

                        if (fileAddon1.HasFile)
                        {
                            strOldFileNm = fileAddon1.FileName;
                            strNewFileNm = UploadFile(fileAddon1);

                            if (!string.IsNullOrEmpty(strExistFile))
                            {
                                // KN_USP_STK_UPDATE_COMPADDON_M00
                                CompInfoBlo.ModifyCompAddon(txtHfCompNo.Text, CommValue.FILEADDON_VALUE_FIRST, strNewFileNm, fileAddon1.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo, strExistFile);
                            }
                            else
                            {
                                // KN_USP_STK_INSERT_COMPADDON_M00
                                CompInfoBlo.RegistryCompAddon(txtHfCompNo.Text, CommValue.FILEADDON_VALUE_FIRST, strNewFileNm, fileAddon1.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                            }

                            isFirstFile = CommValue.AUTH_VALUE_TRUE;
                        }
                        else
                        {
                            if (chkFileAddon1.Visible)
                            {
                                if (chkFileAddon1.Checked)
                                {
                                    // KN_USP_RES_DELETE_COMPADDON_M01
                                    CompInfoBlo.RemoveCompAddon(txtHfCompNo.Text, CommValue.FILEADDON_VALUE_FIRST, strExistFile);
                                }
                            }

                            if (chkFileAddon2.Visible)
                            {
                                if (chkFileAddon2.Checked)
                                {
                                    // KN_USP_RES_DELETE_COMPADDON_M01
                                    CompInfoBlo.RemoveCompAddon(txtHfCompNo.Text, CommValue.FILEADDON_VALUE_SECOND, strExistFile);
                                }
                            }

                            if (chkFileAddon3.Visible)
                            {
                                if (chkFileAddon3.Checked)
                                {
                                    // KN_USP_RES_DELETE_COMPADDON_M01
                                    CompInfoBlo.RemoveCompAddon(txtHfCompNo.Text, CommValue.FILEADDON_VALUE_THIRD, strExistFile);
                                }
                            }
                        }
                    }

                    if (fileAddon2.Visible)
                    {
                        string strExistFile = string.Empty;

                        strExistFile = txtHfFilePath1.Text;

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
                                // KN_USP_STK_UPDATE_COMPADDON_M00
                                CompInfoBlo.ModifyCompAddon(txtHfCompNo.Text, intFilePos, strNewFileNm, fileAddon2.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo, strExistFile);
                            }
                            else
                            {
                                // KN_USP_STK_INSERT_COMPADDON_M00
                                CompInfoBlo.RegistryCompAddon(txtHfCompNo.Text, intFilePos, strNewFileNm, fileAddon2.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                            }
                        }
                        else
                        {
                            if (chkFileAddon2.Visible)
                            {
                                if (chkFileAddon2.Checked)
                                {
                                    // KN_USP_RES_DELETE_COMPADDON_M01
                                    CompInfoBlo.RemoveCompAddon(txtHfCompNo.Text, CommValue.FILEADDON_VALUE_SECOND, strExistFile);
                                }
                            }
                        }
                    }

                    if (fileAddon3.Visible)
                    {
                        string strExistFile = string.Empty;

                        strExistFile = txtHfFilePath1.Text;

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
                                // KN_USP_STK_UPDATE_COMPADDON_M00
                                CompInfoBlo.ModifyCompAddon(txtHfCompNo.Text, intFilePos, strNewFileNm, fileAddon3.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo, strExistFile);
                            }
                            else
                            {
                                // KN_USP_STK_INSERT_COMPADDON_M00
                                CompInfoBlo.RegistryCompAddon(txtHfCompNo.Text, intFilePos, strNewFileNm, fileAddon3.FileBytes.GetLength(0).ToString(), strOldFileNm, Session["CompCd"].ToString(), strMemNo);
                            }
                        }
                        else
                        {
                            if (chkFileAddon3.Visible)
                            {
                                if (chkFileAddon3.Checked)
                                {
                                    // KN_USP_RES_DELETE_COMPADDON_M01
                                    CompInfoBlo.RemoveCompAddon(txtHfCompNo.Text, CommValue.FILEADDON_VALUE_THIRD, strExistFile);
                                }
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:alert('" + AlertNm["INFO_REGIST_ISSUE"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);

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