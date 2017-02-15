using System;
using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;

namespace KN.Web.Config.Authority
{
    public partial class MemberMngView : BasePage
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
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            if (Session["MemAuthViewOk"] != null)
            {
                if (Session["MemAuthViewOk"].Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    if (Request.Params[Master.PARAM_DATA1] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                        {
                            txtHfMemSeq.Text = Request.Params[Master.PARAM_DATA1].ToString();

                            if (Request.Params["CompCd"] != null)
                            {
                                if (!string.IsNullOrEmpty(Request.Params["CompCd"].ToString()))
                                {
                                    txtHfCompCd.Text = Request.Params["CompCd"].ToString();
                                    Session["MemAuthViewOk"] = null;
                                    isReturn = CommValue.AUTH_VALUE_TRUE;
                                }
                            }
                        }
                    }
                }
            }

            return isReturn;
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            ltInsView.Text = TextNm["DETAILVIEW"];
            ltCompNm.Text = TextNm["COMPNM"];
            ltUserID.Text = TextNm["USERID"];
            ltName.Text = TextNm["NAME"];
            ltAuthGrp.Text = TextNm["AUTHCD"] + " " + TextNm["NAME"];
            ltPwd.Text = TextNm["PWD"];
            ltTelNo.Text = TextNm["TEL"];
            ltMobileNo.Text = TextNm["MOBILENO"];
            ltEmail.Text = TextNm["EMAIL"];
            ltAddr.Text = TextNm["ADDR"];

            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.Visible = Master.isModDelAuthOk;
            lnkbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ISSUE"] + "');";
            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnDelete.Visible = Master.isModDelAuthOk;
            lnkbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ISSUE"] + "');";
        }

        private void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_COMM_SELECT_MEMINFO_S03
            dtReturn = MemberMngBlo.WatchMemInfo(txtHfCompCd.Text, txtHfMemSeq.Text, Session["MemNo"].ToString(), Session["MemAuthTy"].ToString(), Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (dtReturn.Rows[0]["ModifyYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_NO))
                    {
                        lnkbtnModify.Visible = CommValue.AUTH_VALUE_FALSE;
                        lnkbtnDelete.Visible = CommValue.AUTH_VALUE_FALSE;
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
                        ltInsName.Text = dtReturn.Rows[0]["MemNm"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["UserId"].ToString()))
                    {
                        ltInsUserID.Text = dtReturn.Rows[0]["UserId"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MemAuthTyNm"].ToString()))
                    {
                        ltInsAuthGrp.Text = dtReturn.Rows[0]["MemAuthTyNm"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Pwd"].ToString()))
                    {
                        string strPwd = dtReturn.Rows[0]["Pwd"].ToString();

                        ltInsPwd.Text = strPwd.Substring(0, 2) + "************";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelTypeCd"].ToString()))
                    {
                        ltInsTel.Text = dtReturn.Rows[0]["TelTypeCd"].ToString();

                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelFrontNo"].ToString()))
                        {
                            ltInsTel.Text = ltInsTel.Text + ") " + dtReturn.Rows[0]["TelFrontNo"].ToString();

                            if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TelRearNo"].ToString()))
                            {
                                ltInsTel.Text = ltInsTel.Text + " - " + dtReturn.Rows[0]["TelRearNo"].ToString();
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileTypeCd"].ToString()))
                    {
                        ltInsMobile.Text = dtReturn.Rows[0]["MobileTypeCd"].ToString();

                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileFrontNo"].ToString()))
                        {
                            ltInsMobile.Text = ltInsMobile.Text + ") " + dtReturn.Rows[0]["MobileFrontNo"].ToString();

                            if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MobileRearNo"].ToString()))
                            {
                                ltInsMobile.Text = ltInsMobile.Text + " - " + dtReturn.Rows[0]["MobileRearNo"].ToString();
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["EmailId"].ToString()))
                    {
                        ltInsEmail.Text = dtReturn.Rows[0]["EmailId"].ToString();

                        if (!string.IsNullOrEmpty(dtReturn.Rows[0]["EmailServer"].ToString()))
                        {
                            ltInsEmail.Text = ltInsEmail.Text + "@" + dtReturn.Rows[0]["EmailServer"].ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Addr"].ToString()))
                    {
                        ltInsAddr.Text = dtReturn.Rows[0]["Addr"].ToString();
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DetAddress"].ToString()))
                    {
                        ltInsDetAddr.Text = dtReturn.Rows[0]["DetAddress"].ToString();
                    }
                }
            }
        }

        #region | Event |

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

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Session["MemAuthWriteOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfMemSeq.Text + "&CompCd=" + txtHfCompCd.Text, CommValue.AUTH_VALUE_FALSE);
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
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_COMM_DELETE_MEMINFO_M00
                // KN_USP_COMM_DELETE_MEMADDON_M00
                MemberMngBlo.RemoveMemMng(txtHfCompNo.Text, txtHfMemSeq.Text);

                Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfMemSeq.Text + "&CompCd=" + txtHfCompCd.Text);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        #endregion
    }
}