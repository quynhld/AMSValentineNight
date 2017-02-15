using System;
using System.Data;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;
using KN.Config.Ent;

namespace KN.Web.Config.Authority
{
    public partial class PersonalMngModify : BasePage
    {
        MemberMngDs.MemberInfo msInfo = new MemberMngDs.MemberInfo();
        MemberMngDs.MemberAddon msAddon = new MemberMngDs.MemberAddon();

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

                        // 데이터 로딩
                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_VIEW, CommValue.AUTH_VALUE_FALSE);
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
        private void InitControls()
        {
            // Authority DropDownList Setting
            DataTable dtReturn = new DataTable();
            
            // KN_USP_MNG_SELECT_AUTHGRPINFO_S01
            dtReturn = AuthorityMngBlo.SpreadControlAuthGrpInfo(CommValue.MAIN_COMP_CD, Session["LangCd"].ToString());

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

            ltWrite.Text = TextNm["MODIFY"];
            ltCompNm.Text = TextNm["COMPNM"];
            ltUserID.Text = TextNm["USERID"];
            ltName.Text = TextNm["NAME"];
            ltFMS.Text = TextNm["FMSAUTH"];
            ltEnterDt.Text = TextNm["ENTERDT"];
            ltRetireDt.Text = TextNm["RETIREDT"];
            ltAuthGrp.Text = TextNm["AUTHCD"] + " " + TextNm["NAME"];
            ltPwd.Text = TextNm["PWD"];
            ltPwdConfirm.Text = TextNm["PWD"];
            ltTelNo.Text = TextNm["TEL"];
            ltMobileNo.Text = TextNm["MOBILENO"];
            ltEmail.Text = TextNm["EMAIL"];
            ltAddr.Text = TextNm["ADDR"];

            // Button Setting
            lnkbtnRegist.Text = TextNm["MODIFY"];
            lnkbtnReset.Text = TextNm["RESET"];
            lnkbtnList.Text = TextNm["LIST"];

            txtTelTy.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtTelRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileTy.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileFrontNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtMobileRearNo.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            lnkbtnReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_DIFFERENT_PWD"] + "');";

            lnkbtnRegist.Visible = Master.isModDelAuthOk;
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // KN_USP_COMM_SELECT_MEMINFO_S03
            dtReturn = MemberMngBlo.WatchMemInfo(Session["CompCd"].ToString(), Session["MemNo"].ToString(), Session["MemNo"].ToString(), Session["MemAuthTy"].ToString(), Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (dtReturn.Rows[0]["ModifyYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                    {
                        lnkbtnRegist.Visible = CommValue.AUTH_VALUE_FALSE;
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompNm"].ToString()))
                    {
                        ltInsCompNm.Text = dtReturn.Rows[0]["CompNm"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompNo"].ToString()))
                    {
                        txtHfCompNo.Text = dtReturn.Rows[0]["CompNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MemNm"].ToString()))
                    {
                        lblName.Text = dtReturn.Rows[0]["MemNm"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["UserId"].ToString()))
                    {
                        lblUserID.Text = dtReturn.Rows[0]["UserId"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MemNo"].ToString()))
                    {
                        txtHfMemNo.Text = dtReturn.Rows[0]["MemNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MemAuthTy"].ToString()))
                    {
                        ddlAuthGrp.SelectedValue = dtReturn.Rows[0]["MemAuthTy"].ToString();
                        ddlAuthGrp.Enabled = CommValue.AUTH_VALUE_FALSE;
                        txtHfAuthGrp.Text = dtReturn.Rows[0]["MemAuthTy"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["FMSAuthYn"].ToString()))
                    {
                        ddlFMSAuthYn.SelectedValue = dtReturn.Rows[0]["FMSAuthYn"].ToString();
                        ddlFMSAuthYn.Enabled = CommValue.AUTH_VALUE_FALSE;
                        txtHfFMSAuthYn.Text = dtReturn.Rows[0]["FMSAuthYn"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["EnterDt"].ToString()))
                    {
                        lblEnterDt.Text = TextLib.MakeDateEightDigit(dtReturn.Rows[0]["EnterDt"].ToString());
                        hfEnterDt.Value = dtReturn.Rows[0]["EnterDt"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["RetireDt"].ToString()))
                    {
                        lblRetireDt.Text = TextLib.MakeDateEightDigit(dtReturn.Rows[0]["RetireDt"].ToString());
                        hfRetireDt.Value = dtReturn.Rows[0]["RetireDt"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelTypeCd"].ToString()))
                    {
                        txtTelTy.Text = dtReturn.Rows[0]["TelTypeCd"].ToString();
                        txtHfTelTy.Text = dtReturn.Rows[0]["TelTypeCd"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelFrontNo"].ToString()))
                    {
                        txtTelFrontNo.Text = dtReturn.Rows[0]["TelFrontNo"].ToString();
                        txtHfTelFrontNo.Text = dtReturn.Rows[0]["TelFrontNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelRearNo"].ToString()))
                    {
                        txtTelRearNo.Text = dtReturn.Rows[0]["TelRearNo"].ToString();
                        txtHfTelRearNo.Text = dtReturn.Rows[0]["TelRearNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileTypeCd"].ToString()))
                    {
                        txtMobileTy.Text = dtReturn.Rows[0]["MobileTypeCd"].ToString();
                        txtHfMobileTy.Text = dtReturn.Rows[0]["MobileTypeCd"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileFrontNo"].ToString()))
                    {
                        txtMobileFrontNo.Text = dtReturn.Rows[0]["MobileFrontNo"].ToString();
                        txtHfMobileFrontNo.Text = dtReturn.Rows[0]["MobileFrontNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileRearNo"].ToString()))
                    {
                        txtMobileRearNo.Text = dtReturn.Rows[0]["MobileRearNo"].ToString();
                        txtHfMobileRearNo.Text = dtReturn.Rows[0]["MobileRearNo"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["EmailId"].ToString()))
                    {
                        txtEmailID.Text = dtReturn.Rows[0]["EmailId"].ToString();
                        txtHfEmailID.Text = dtReturn.Rows[0]["EmailId"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["EmailServer"].ToString()))
                    {
                        txtEmailServer.Text = dtReturn.Rows[0]["EmailServer"].ToString();
                        txtHfEmailServer.Text = dtReturn.Rows[0]["EmailServer"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Addr"].ToString()))
                    {
                        txtAddr.Text = dtReturn.Rows[0]["Addr"].ToString();
                        txtHfAddr.Text = dtReturn.Rows[0]["Addr"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DetAddress"].ToString()))
                    {
                        txtDetAddr.Text = dtReturn.Rows[0]["DetAddress"].ToString();
                        txtHfDetAddr.Text = dtReturn.Rows[0]["DetAddress"].ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 컨트롤을 리셋하는 메소드
        /// </summary>
        private void ResetControls()
        {
            txtMobileTy.Text = txtHfMobileTy.Text;
            txtMobileFrontNo.Text = txtHfMobileFrontNo.Text;
            txtMobileRearNo.Text = txtHfMobileRearNo.Text;
            txtTelTy.Text = txtHfTelTy.Text;
            txtTelFrontNo.Text = txtHfTelFrontNo.Text;
            txtTelRearNo.Text = txtHfTelRearNo.Text;
            txtEmailID.Text = txtHfEmailID.Text;
            txtEmailServer.Text = txtHfEmailServer.Text;
            txtAddr.Text = txtHfAddr.Text;
            txtDetAddr.Text = txtHfDetAddr.Text;
        }

        /// <summary>
        /// MemInfo 오브젝트 생성 메소드
        /// </summary>
        private void MakeMemInfoObj()
        {
            msInfo.CompNo = txtHfCompNo.Text;
            msInfo.MemNo = txtHfMemNo.Text;
            msInfo.UserId = lblUserID.Text;
            msInfo.Pwd = txtPwd.Text;
            msInfo.MemNm = lblName.Text;
            msInfo.MemAuthTy = ddlAuthGrp.SelectedValue;
            msInfo.FMSAuthYn = ddlFMSAuthYn.SelectedValue;

            if (!string.IsNullOrEmpty(hfEnterDt.Value))
            {
                msInfo.EnterDt = hfEnterDt.Value.Replace("-", "").Replace("/", "");
            }
            else
            {
                msInfo.EnterDt = string.Empty;
            }

            if (!string.IsNullOrEmpty(hfRetireDt.Value))
            {
                msInfo.RetireDt = hfRetireDt.Value.Replace("-", "").Replace("/", "");
            }
            else
            {
                msInfo.RetireDt = string.Empty;
            }

            msInfo.InsCompNo = Session["CompCd"].ToString();
            msInfo.InsMemNo = Session["MemNo"].ToString();
        }

        /// <summary>
        /// MemAddon 오브젝트 생성 메소드
        /// </summary>
        private void MakeMemAddonObj()
        {
            msAddon.CompNo = txtHfCompNo.Text;
            msAddon.MemNo = txtHfMemNo.Text;

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

                MakeMemInfoObj();
                MakeMemAddonObj();

                // KN_USP_COMM_UPDATE_MEMINFO_M00
                // KN_USP_COMM_UPDATE_MEMADDON_M00
                MemberMngBlo.ModifyMemMng(msInfo, msAddon);

                Response.Redirect(Master.PAGE_VIEW, CommValue.AUTH_VALUE_FALSE);
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

                Response.Redirect(Master.PAGE_VIEW, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}