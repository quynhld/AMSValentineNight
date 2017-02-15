using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Lib;

using KN.Stock.Biz;

namespace KN.Web.Stock.Material
{
    public partial class MaterialClassificationList : BasePage
    {
        string strViewDt = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    InitControls();

                    LoadGrpData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            ltCodeGrpCd.Text = TextNm["GRPCD"];
            ltCodeMainCd.Text = TextNm["MAINCD"];
            ltCodeNmVi.Text = TextNm["VIETNAMESE"];
            ltCodeNmEn.Text = TextNm["ENGLISH"];
            ltCodeNmKr.Text = TextNm["KOREAN"];
            lnkbtnIns.Text = TextNm["ADD"];
            lnkbtnMod.Text = TextNm["MODIFY"];
            lnkbtnDel.Text = TextNm["DELETE"];

            lnkbtnIns.OnClientClick = "javascript:return fnRegCheck('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "','" + AlertNm["CONF_REGIST_ITEM"] + "')";
            lnkbtnMod.OnClientClick = "javascript:return fnModCheck('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "','" + AlertNm["CONF_MODIFY_ITEM"] + "')";
            lnkbtnDel.OnClientClick = "javascript:return fnDelCheck('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["CONF_DELETE_ITEM"] + "')";
        }

        private void LoadGrpData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIGRPCD_S00
            DataTable dtGrpReturn = MaterialClassificationBlo.SpreadClassiGrpCdInfo(Session["LangCd"].ToString(), strViewDt);

            lbGrpClass.Items.Clear();

            foreach (DataRow dr in dtGrpReturn.Select())
            {
                lbGrpClass.Items.Add(new ListItem(dr["CodeNm"].ToString() + " ( " + dr["CodeCd"].ToString() + " )", dr["CodeCd"].ToString()));
            }

            if (!string.IsNullOrEmpty(lbGrpClass.SelectedValue))
            {
                // KN_USP_STK_SELECT_CLASSIMAINCD_S00
                DataTable dtMainReturn = MaterialClassificationBlo.SpreadClassiMainCdInfo(Session["LangCd"].ToString(), strViewDt, lbGrpClass.SelectedValue);

                lbMainClass.Items.Clear();

                foreach (DataRow dr in dtMainReturn.Select())
                {
                    lbMainClass.Items.Add(new ListItem(dr["CodeNm"].ToString() + " ( " + dr["CodeCd"].ToString() + " )", dr["CodeCd"].ToString()));
                }
            }
        }

        private void LoadDetailData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_STK_SELECT_MATERIALCLASSIFICATIONINFO_S00
            DataTable dtDetailReturn = MaterialClassificationBlo.WatchMaterialClassificationInfo(strViewDt, lbGrpClass.SelectedValue, lbMainClass.SelectedValue);

            if (dtDetailReturn != null)
            {
                txtCodeGrpCd.Text = dtDetailReturn.Rows[0]["ClassiGrpCd"].ToString();
                txtCodeMainCd.Text = dtDetailReturn.Rows[0]["ClassiMainCd"].ToString();
                txtCodeNmVi.Text = dtDetailReturn.Rows[0]["ClassiNmVi"].ToString();
                txtCodeNmEn.Text = dtDetailReturn.Rows[0]["ClassiNmEn"].ToString();
                txtCodeNmKr.Text = dtDetailReturn.Rows[0]["ClassiNmKr"].ToString();
                hfCodeGrpCd.Value = dtDetailReturn.Rows[0]["ClassiGrpCd"].ToString();
                hfCodeMainCd.Value = dtDetailReturn.Rows[0]["ClassiMainCd"].ToString();
                hfCodeNmVi.Value = dtDetailReturn.Rows[0]["ClassiNmVi"].ToString();
                hfCodeNmEn.Value = dtDetailReturn.Rows[0]["ClassiNmEn"].ToString();
                hfCodeNmKr.Value = dtDetailReturn.Rows[0]["ClassiNmKr"].ToString();
            }
        }

        private void LoadMainData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIMAINCD_S00
            DataTable dtMainReturn = MaterialClassificationBlo.SpreadClassiMainCdInfo(Session["LangCd"].ToString(), strViewDt, lbGrpClass.SelectedValue);

            lbMainClass.Items.Clear();

            foreach (DataRow dr in dtMainReturn.Select())
            {
                lbMainClass.Items.Add(new ListItem(dr["CodeNm"].ToString() + " ( " + dr["CodeCd"].ToString() + " )", dr["CodeCd"].ToString()));
            }
        }

        private void ResetDetail()
        {
            txtCodeGrpCd.Text = string.Empty;
            txtCodeMainCd.Text = string.Empty;
            txtCodeNmVi.Text = string.Empty;
            txtCodeNmEn.Text = string.Empty;
            txtCodeNmKr.Text = string.Empty;
            hfCodeGrpCd.Value = string.Empty;
            hfCodeMainCd.Value = string.Empty;
            hfCodeNmVi.Value = string.Empty;
            hfCodeNmEn.Value = string.Empty;
            hfCodeNmKr.Value = string.Empty;
        }

        protected void lbGrpClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            LoadMainData();

            LoadDetailData();
        }

        protected void lbMainClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            LoadDetailData();
        }

        protected void lnkbtnIns_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strIp = Request.ServerVariables["REMOTE_ADDR"];

                if (txtCodeMainCd.Text.Equals(CommValue.CODE_VALUE_EMPTY))
                {
                    // 그룹코드 등록시
                    strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

                    // KN_USP_STK_SELECT_MATERIALCLASSIFICATIONINFO_S00
                    DataTable dtGrpRetrun = MaterialClassificationBlo.WatchMaterialClassificationInfo(strViewDt, txtCodeGrpCd.Text, txtCodeMainCd.Text);

                    if (dtGrpRetrun.Rows.Count > 0)
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningExistGrpCd", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    else
                    {
                        // KN_USP_STK_INSERT_MATERIALCLASSIFICATIONINFO_M00
                        MaterialClassificationBlo.RegistryMaterialClassificationInfo(txtCodeGrpCd.Text, txtCodeMainCd.Text, txtCodeNmEn.Text, txtCodeNmVi.Text, txtCodeNmKr.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                        LoadGrpData();

                        lbMainClass.Items.Clear();

                        ResetDetail();
                    }
                }
                else
                {
                    // 메인코드 등록시
                    strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

                    // KN_USP_STK_SELECT_MATERIALCLASSIFICATIONINFO_S00
                    DataTable dtGrpRetrun = MaterialClassificationBlo.WatchMaterialClassificationInfo(strViewDt, txtCodeGrpCd.Text, CommValue.CODE_VALUE_EMPTY);

                    if (dtGrpRetrun.Rows.Count == 0)
                    {
                        // 그룹코드가 존재하지 않을 경우
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('" + AlertNm["ALERT_INSERT_GRPCD"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningNoGrpCd", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    else
                    {
                        // 그룹코드가 존재할 경우
                        strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

                        // KN_USP_STK_SELECT_MATERIALCLASSIFICATIONINFO_S00
                        DataTable dtMainRetrun = MaterialClassificationBlo.WatchMaterialClassificationInfo(strViewDt, txtCodeGrpCd.Text, txtCodeMainCd.Text);

                        if (dtMainRetrun.Rows.Count > 0)
                        {
                            StringBuilder sbWarning = new StringBuilder();

                            sbWarning.Append("alert('" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningExistMainCd", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // KN_USP_STK_INSERT_MATERIALCLASSIFICATIONINFO_M00
                            MaterialClassificationBlo.RegistryMaterialClassificationInfo(txtCodeGrpCd.Text, txtCodeMainCd.Text, txtCodeNmEn.Text, txtCodeNmVi.Text, txtCodeNmKr.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                            LoadGrpData();

                            lbMainClass.Items.Clear();

                            ResetDetail();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnMod_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strIp = Request.ServerVariables["REMOTE_ADDR"];

                strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

                if (txtCodeMainCd.Text.Equals(CommValue.CODE_VALUE_EMPTY))
                {
                    // 그룹코드 수정시
                    strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

                    // KN_USP_STK_SELECT_MATERIALCLASSIFICATIONINFO_S00
                    DataTable dtGrpRetrun = MaterialClassificationBlo.WatchMaterialClassificationInfo(strViewDt, txtCodeGrpCd.Text, txtCodeMainCd.Text);

                    if (dtGrpRetrun.Rows.Count == 0)
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('" + AlertNm["INFO_HAS_NO_MODIFIED_ITEM"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningNoExistGrpCd", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    else
                    {
                        // 기존 항목을 삭제 혹은 전일로 변경
                        // KN_USP_STK_DELETE_MATERIALCLASSIFICATIONINFO_M00
                        MaterialClassificationBlo.RemoveMaterialClassificationInfo(txtCodeGrpCd.Text, txtCodeMainCd.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                        // 새 항목을 입력
                        // KN_USP_STK_INSERT_MATERIALCLASSIFICATIONINFO_M00
                        MaterialClassificationBlo.RegistryMaterialClassificationInfo(txtCodeGrpCd.Text, txtCodeMainCd.Text, txtCodeNmEn.Text, txtCodeNmVi.Text, txtCodeNmKr.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                        LoadGrpData();

                        lbMainClass.Items.Clear();

                        ResetDetail();
                    }
                }
                else
                {
                    // 메인코드 수정시
                    strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

                    // KN_USP_STK_SELECT_MATERIALCLASSIFICATIONINFO_S00
                    DataTable dtGrpRetrun = MaterialClassificationBlo.WatchMaterialClassificationInfo(strViewDt, txtCodeGrpCd.Text, CommValue.CODE_VALUE_EMPTY);

                    if (dtGrpRetrun.Rows.Count == 0)
                    {
                        // 그룹코드가 존재하지 않을 경우
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('" + AlertNm["ALERT_INSERT_GRPCD"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningNoGrpCd", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    else
                    {
                        // 그룹코드가 존재할 경우
                        strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

                        // KN_USP_STK_SELECT_MATERIALCLASSIFICATIONINFO_S00
                        DataTable dtMainRetrun = MaterialClassificationBlo.WatchMaterialClassificationInfo(strViewDt, txtCodeGrpCd.Text, txtCodeMainCd.Text);

                        if (dtMainRetrun.Rows.Count == 0)
                        {
                            // 메인코드가 존재하지 않을 경우
                            StringBuilder sbWarning = new StringBuilder();

                            sbWarning.Append("alert('" + AlertNm["INFO_HAS_NO_MODIFIED_ITEM"] + "');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningNoExistMainCd", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                        else
                        {
                            // 기존 항목을 삭제 혹은 전일로 변경
                            // KN_USP_STK_DELETE_MATERIALCLASSIFICATIONINFO_M00
                            MaterialClassificationBlo.RemoveMaterialClassificationInfo(txtCodeGrpCd.Text, txtCodeMainCd.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                            // 새 항목을 입력
                            // KN_USP_STK_INSERT_MATERIALCLASSIFICATIONINFO_M00
                            MaterialClassificationBlo.RegistryMaterialClassificationInfo(txtCodeGrpCd.Text, txtCodeMainCd.Text, txtCodeNmEn.Text, txtCodeNmVi.Text, txtCodeNmKr.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                            LoadGrpData();

                            lbMainClass.Items.Clear();

                            ResetDetail();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

                string strIp = Request.ServerVariables["REMOTE_ADDR"];

                // KN_USP_STK_DELETE_MATERIALCLASSIFICATIONINFO_M00
                MaterialClassificationBlo.RemoveMaterialClassificationInfo(txtCodeGrpCd.Text, txtCodeMainCd.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strIp);

                LoadGrpData();

                lbMainClass.Items.Clear();

                ResetDetail();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}