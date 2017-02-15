using System;
using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;

namespace KN.Web.Config.Authority
{
    public partial class PersonalMngView : BasePage
    {
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

                        // 데이터 로딩
                        LoadData();
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
        private bool CheckParams()
        {
            bool isReturn = CommValue.AUTH_VALUE_TRUE;

            return isReturn;
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            ltCompNm.Text = TextNm["COMPNM"];
            lblView.Text = TextNm["DETAILVIEW"];
            ltUserID.Text = TextNm["USERID"];
            ltName.Text = TextNm["NAME"];
            ltAuthGrp.Text = TextNm["AUTHCD"] + " " + TextNm["NAME"];
            ltPwd.Text = TextNm["PWD"];
            ltTelNo.Text = TextNm["TEL"];
            ltMobileNo.Text = TextNm["MOBILENO"];
            ltEmail.Text = TextNm["EMAIL"];
            ltAddr.Text = TextNm["ADDR"];

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ISSUE"] + "');";
        }

        private void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_COMM_SELECT_MEMINFO_S03
            dtReturn = MemberMngBlo.WatchMemInfo(Session["CompCd"].ToString(), Session["MemNo"].ToString(), Session["MemNo"].ToString(), Session["MemAuthTy"].ToString(), Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (dtReturn.Rows[0]["ModifyYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                    {
                        lnkbtnModify.Visible = CommValue.AUTH_VALUE_FALSE;
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

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MemAuthTyNm"].ToString()))
                    {
                        lblAuthGrp.Text = dtReturn.Rows[0]["MemAuthTyNm"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Pwd"].ToString()))
                    {
                        string strPwd = dtReturn.Rows[0]["Pwd"].ToString();

                        lblPwd.Text = strPwd.Substring(0, 2) + "************";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelTypeCd"].ToString()))
                    {
                        lblTel.Text = dtReturn.Rows[0]["TelTypeCd"].ToString();

                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelFrontNo"].ToString()))
                        {
                            lblTel.Text = lblTel.Text + ") " + dtReturn.Rows[0]["TelFrontNo"].ToString();

                            if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelRearNo"].ToString()))
                            {
                                lblTel.Text = lblTel.Text + " - " + dtReturn.Rows[0]["TelRearNo"].ToString();
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileTypeCd"].ToString()))
                    {
                        lblMobile.Text = dtReturn.Rows[0]["MobileTypeCd"].ToString();

                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileFrontNo"].ToString()))
                        {
                            lblMobile.Text = lblMobile.Text + ") " + dtReturn.Rows[0]["MobileFrontNo"].ToString();

                            if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileRearNo"].ToString()))
                            {
                                lblMobile.Text = lblMobile.Text + " - " + dtReturn.Rows[0]["MobileRearNo"].ToString();
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["EmailId"].ToString()))
                    {
                        lblEmail.Text = dtReturn.Rows[0]["EmailId"].ToString();

                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["EmailServer"].ToString()))
                        {
                            lblEmail.Text = lblEmail.Text + "@" + dtReturn.Rows[0]["EmailServer"].ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Addr"].ToString()))
                    {
                        lblAddr.Text = dtReturn.Rows[0]["Addr"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DetAddress"].ToString()))
                    {
                        lblDetAddr.Text = dtReturn.Rows[0]["DetAddress"].ToString();
                    }
                }
            }
        }

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Session["MemAuthWriteOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                Response.Redirect(Master.PAGE_MODIFY, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}