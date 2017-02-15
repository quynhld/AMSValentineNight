using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;
using KN.Config.Ent;

namespace KN.Web.Config.Authority
{
    public partial class MemberMngWrite : BasePage
    {
        MemberMngDs.MemberInfo msInfo = new MemberMngDs.MemberInfo();
        MemberMngDs.MemberAddon msAddon = new MemberMngDs.MemberAddon();

        string strMemAuthTy = string.Empty;

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
                        InitControls();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
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
        protected bool CheckParams()
        {
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            if (Session["MemAuthWriteOk"] != null)
            {
                if (Session["MemAuthWriteOk"].Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    Session["MemAuthWriteOk"] = null;
                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturn;
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        protected void InitControls()
        {
            // 권한정보 세팅
            // CheckAuthority();

            // Authority DropDownList Setting
            // AuthCheckLib.CheckFullData(chkAuth, txtHfAuthGrpTy);

            // DropDownList 세팅
            DdlAuthority();

            ltCompNm.Text = TextNm["COMPNM"];
            ltAuthority.Text = TextNm["AUTH"];
            ltUserID.Text = TextNm["USERID"];
            ltFMS.Text = TextNm["FMSAUTH"];
            ltEnterDt.Text = TextNm["ENTERDT"];
            ltName.Text = TextNm["NAME"];
            ltAuthGrp.Text = TextNm["AUTHCD"] + " " + TextNm["NAME"];
            ltPwd.Text = TextNm["PWD"];
            ltPwdConfirm.Text = TextNm["PWD"];
            ltTelNo.Text = TextNm["TEL"];
            ltMobileNo.Text = TextNm["MOBILENO"];
            ltEmail.Text = TextNm["EMAIL"];
            ltAddr.Text = TextNm["ADDR"];
            //ltFileAddon.Text = TextNm["FILEADDON"];

            // Button Setting
            lnkbtnMemInfo.Text = TextNm["MEM"] + " " + TextNm["SEARCH"];
            lnkbtnTemporary.Text = TextNm["TEMPID"];
            lnkbtnRegist.Text = TextNm["ADD"];
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnList.Text = TextNm["LIST"];

            txtUserID.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelTy.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileTy.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            lnkbtnTemporary.OnClientClick = "javascript:return fnAlertTempID('" + AlertNm["INFO_TEMP_ID_USING"] + "','" + AlertNm["INFO_MAKE_PROBLEM"] + "','" + AlertNm["INFO_RESPONSIVILITY"] + "');";
            lnkbtnReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_DIFFERENT_PWD"] + "');";

            lnkbtnRegist.Visible = Master.isWriteAuthOk;
            hfParamData.Value = hfParamData.ClientID.Replace("hfParamData", "");
            txtHfAuth.Text = ddlAuth.SelectedValue;

            // 회사코드 세팅
            MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlAuth, Session["CompCd"].ToString());
            //ddlAuth.Items.RemoveAt(CommValue.NUMBER_VALUE_0);

            MakeItemReadOnly(CommValue.AUTH_VALUE_TRUE);
        }

        /// <summary>
        /// 컨트롤을 리셋하는 메소드
        /// </summary>
        protected void ResetControls()
        {
            txtUserID.Text = string.Empty;
            txtName.Text = string.Empty;

            if (!string.IsNullOrEmpty(txtHfAuthGrp.Text))
            {
                ddlAuthGrp.SelectedValue = txtHfAuthGrp.Text;
            }

            txtPwd.Text = string.Empty;
            txtPwdConfirm.Text = string.Empty;
            txtEnterDt.Text = string.Empty;
            hfEnterDt.Value = string.Empty;
            txtMobileTy.Text = string.Empty;
            txtMobileFrontNo.Text = string.Empty;
            txtMobileRearNo.Text = string.Empty;
            txtTelTy.Text = string.Empty;
            txtTelFrontNo.Text = string.Empty;
            txtTelRearNo.Text = string.Empty;
            txtEmailID.Text = string.Empty;
            txtEmailServer.Text = string.Empty;
            txtAddr.Text = string.Empty;
            txtDetAddr.Text = string.Empty;

            txtName.ReadOnly = CommValue.AUTH_VALUE_TRUE;
            txtUserID.ReadOnly = CommValue.AUTH_VALUE_TRUE;
        }

        /// <summary>
        /// MemInfo 오브젝트 생성 메소드
        /// </summary>
        protected void MakeMemInfoObj()
        {
            msInfo.CompNo = ddlAuth.SelectedValue;
            msInfo.MemNo = hfMemNo.Value;

            if (string.IsNullOrEmpty(txtUserID.Text))
            {
                msInfo.UserId = hfUserID.Value;
            }
            else
            {
                msInfo.UserId = txtUserID.Text;
            }

            msInfo.Pwd = txtPwd.Text;

            if (string.IsNullOrEmpty(txtName.Text))
            {
                msInfo.MemNm = hfMemEngNm.Value;
            }
            else
            {
                msInfo.MemNm = txtName.Text;
            }

            msInfo.MemAuthTy = ddlAuthGrp.SelectedValue;
            msInfo.MemAccAuthTy = txtHfAuthGrpTy.Text;
            msInfo.FMSAuthYn = ddlFMSAuthYn.SelectedValue;
            msInfo.EnterDt = hfEnterDt.Value.Replace("-", "").Replace("/", "");
            msInfo.RetireDt = string.Empty;
            msInfo.KNNo = hfKNNo.Value;
            msInfo.InsCompNo = Session["CompCd"].ToString();
            msInfo.InsMemNo = Session["MemNo"].ToString();
        }

        /// <summary>
        /// MemAddon 오브젝트 생성 메소드
        /// </summary>
        protected void MakeMemAddonObj()
        {
            msAddon.CompNo = ddlAuth.SelectedValue;
            msAddon.MemNo = hfMemNo.Value;

            if (string.IsNullOrEmpty(txtAddr.Text))
            {
                msAddon.Addr = string.Empty;
            }
            else
            {
                msAddon.Addr = txtAddr.Text;
            }

            if (string.IsNullOrEmpty(txtDetAddr.Text))
            {
                msAddon.DetAddress = string.Empty;
            }
            else
            {
                msAddon.DetAddress = txtDetAddr.Text;
            }

            if (string.IsNullOrEmpty(txtTelTy.Text))
            {
                msAddon.TelTypeCd = string.Empty;
            }
            else
            {
                msAddon.TelTypeCd = txtTelTy.Text;
            }

            if (string.IsNullOrEmpty(txtTelFrontNo.Text))
            {
                msAddon.TelFrontNo = string.Empty;
            }
            else
            {
                msAddon.TelFrontNo = txtTelFrontNo.Text;
            }

            if (string.IsNullOrEmpty(txtTelRearNo.Text))
            {
                msAddon.TelRearNo = string.Empty;
            }
            else
            {
                msAddon.TelRearNo = txtTelRearNo.Text;
            }

            if (string.IsNullOrEmpty(txtMobileTy.Text))
            {
                msAddon.MobileTypeCd = string.Empty;
            }
            else
            {
                msAddon.MobileTypeCd = txtMobileTy.Text;
            }

            if (string.IsNullOrEmpty(txtMobileFrontNo.Text))
            {
                msAddon.MobileFrontNo = string.Empty;
            }
            else
            {
                msAddon.MobileFrontNo = txtMobileFrontNo.Text;
            }

            if (string.IsNullOrEmpty(txtMobileRearNo.Text))
            {
                msAddon.MobileRearNo = string.Empty;
            }
            else
            {
                msAddon.MobileRearNo = txtMobileRearNo.Text;
            }

            if (string.IsNullOrEmpty(txtEmailID.Text))
            {
                msAddon.EmailId = string.Empty;
            }
            else
            {
                msAddon.EmailId = txtEmailID.Text;
            }

            if (string.IsNullOrEmpty(txtEmailServer.Text))
            {
                msAddon.EmailServer = string.Empty;
            }
            else
            {
                msAddon.EmailServer = txtEmailServer.Text;
            }
        }

        protected void DdlAuthority()
        {
            int intAccAuthTy = 0;

            DataTable dtReturn = new DataTable();

            if (Session["MemAccAuthTy"] != null)
            {
                intAccAuthTy = 99999999;
            }
            else
            {
                intAccAuthTy = Int32.Parse(Session["MemAccAuthTy"].ToString());
            }

            // KN_USP_MNG_SELECT_AUTHGRPINFO_S02
            dtReturn = AuthorityMngBlo.SpreadControlLimitAuthGrpInfo(Session["LangCd"].ToString(), intAccAuthTy);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    ddlAuthGrp.Items.Clear();

                    foreach (DataRow dr in dtReturn.Select())
                    {
                        if (dr["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
                        {
                            if (Session["MemAuthTy"].ToString().Equals(CommValue.AUTH_VALUE_SUPER))
                            {
                                ddlAuthGrp.Items.Add(new ListItem(dr["MemAuthTyNm"].ToString(), dr["MemAuthTy"].ToString()));
                            }
                        }
                        else
                        {
                            ddlAuthGrp.Items.Add(new ListItem(dr["MemAuthTyNm"].ToString(), dr["MemAuthTy"].ToString()));
                        }
                    }
                }
            }
        }

        protected void CheckAuthority()
        {
            int intTotalAuth = 0;
            int intAccAuthTy = 0;

            /* 그룹권한 정보를 가져오는 부분 시작 */
            DataTable dtReturn = new DataTable();

            if (Session["MemAccAuthTy"] != null)
            {
                intAccAuthTy = 99999999;
            }
            else
            {
                intAccAuthTy = Int32.Parse(Session["MemAccAuthTy"].ToString());
            }

            // KN_USP_MNG_SELECT_AUTHGRPINFO_S02
            dtReturn = AuthorityMngBlo.SpreadControlLimitAuthGrpInfo(Session["LangCd"].ToString(), intAccAuthTy);

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

                    if (dtReturn.Rows.Count < 2)
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

        protected void ddlAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ResetControls();

                txtHfAuth.Text = ddlAuth.SelectedValue;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        #region | Event 모음 |

        protected void lnkbtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                ResetControls();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strAuth = txtHfAuthGrpTy.Text;
                string strTotalAuth = txtHfTotalGrpTy.Text;

                if (strAuth.Equals(strTotalAuth))
                {
                    strAuth = CommValue.AUTH_VALUE_ENTIRE;
                }

                MakeMemInfoObj();
                MakeMemAddonObj();

                // 전체 선택시 '99999999' 처리함.
                msInfo.MemAccAuthTy = strAuth;

                // KN_USP_COMM_SELECT_MEMINFO_S02
                DataTable dtMemCntReturn = MemberMngBlo.WatchUserID(msInfo.CompNo, msInfo.UserId);

                if (dtMemCntReturn != null)
                {
                    if (dtMemCntReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        if (Int32.Parse(dtMemCntReturn.Rows[0]["ExistCnt"].ToString()) > CommValue.NUMBER_VALUE_0)
                        {
                            StringBuilder sbWarning = new StringBuilder();

                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["INFO_CANT_MAKE_ID"]);
                            sbWarning.Append("');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // KN_USP_COMM_INSERT_MEMINFO_S00
                            // KN_USP_COMM_INSERT_MEMADDON_M00
                            MemberMngBlo.RegistryMemMng(msInfo, msAddon);

                            Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                        }
                    }
                }
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

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtUserID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtUserID.Text))
                {
                    DataTable dtRetrun = new DataTable();

                    // KN_USP_COMM_SELECT_MEMINFO_S02
                    dtRetrun = MemberMngBlo.WatchUserID(ddlAuth.SelectedValue, txtUserID.Text);

                    if (dtRetrun != null)
                    {
                        if (dtRetrun.Rows.Count > 0)
                        {
                            if (Int32.Parse(dtRetrun.Rows[0]["ExistCnt"].ToString()) > 0)
                            {
                                // 중복된 ID 처리
                                txtUserID.Text = string.Empty;

                                StringBuilder sbWarning = new StringBuilder();

                                sbWarning.Append("alert('");
                                sbWarning.Append(AlertNm["INFO_CANT_MAKE_ID"]);
                                sbWarning.Append("');");

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Exist", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                            }
                        }
                    }
                }
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

        protected void MakeItemReadOnly(bool isReadOnly)
        {
            txtName.ReadOnly = isReadOnly;
            txtUserID.ReadOnly = isReadOnly;
            txtEnterDt.ReadOnly = isReadOnly;
        }

        #endregion

        protected void lnkbtnTemporary_Click(object sender, EventArgs e)
        {
            ResetControls();

            MakeItemReadOnly(CommValue.AUTH_VALUE_FALSE);
        }

        protected void imgbtnHoldItem_Click(object sender, ImageClickEventArgs e)
        {
            ResetControls();

            MakeItemReadOnly(CommValue.AUTH_VALUE_TRUE);
        }
    }
}