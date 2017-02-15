using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Stock.Biz;
using KN.Manage.Biz;
using KN.Common.Method.Lib;
using KN.Config.Biz;

namespace KN.Web.Stock.Material
{
    public partial class MaterialMngWrite : BasePage
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

                    LoadSvcZoneData();

                    LoadGrpData();

                    LoadMainData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            //lnkbtnPrint.Text= TextNm["PRINT"];
            ltRentCd.Text = TextNm["SECTIONCD"];
            ltSvcZoneCd.Text = TextNm["WAREHOUSECD"];
            ltGrpCd.Text = TextNm["GRPCD"];
            ltMainCd.Text = TextNm["MAINCD"];
            ltSubCd.Text = TextNm["SUBCD"];
            chkAuto.Text = TextNm["AUTOABSTRACT"];
            ltCdNm.Text = TextNm["CDNM"];
            chkAutoApproval.Text = TextNm["AUTOAPPROVAL"];
            ltCompNm.Text = TextNm["COMPNM"];

            lnkbtnCdsearch.Text = TextNm["CD"] + " " + TextNm["SEARCH"];
            ltQty.Text = TextNm["QTY"];
            ltPrimeCost.Text = TextNm["PRIMECOST"];
            ltPrimeDong.Text = TextNm["DONG"];
            ltSellingCost.Text = TextNm["SELLINGCOST"];
            ltSellingDong.Text = TextNm["DONG"];
            ltVat.Text = TextNm["VAT"];
            ltIncludedVat.Text = TextNm["INCLUDEVAT"];
            ltRemark.Text = TextNm["REMARK"];

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL);
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlScale, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_SCALE);
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlVatYn, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_INCLUDED);

            lnkbtnCdsearch.OnClientClick = "javascript:return fnCompSearch('" + Master.PARAM_DATA1 + "', '" + txtCompNm.ClientID + "','" + Master.PARAM_DATA2 + "', '" + hfCompCd.ClientID + "');";

            txtSubCd.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtQty.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtVatRatio.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            lnkbtnRegist.Text = TextNm["REGIST"];
            lnkbtnRegist.Visible = Master.isWriteAuthOk;
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["CONF_REGIST_ITEM"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            txtCompNm.Attributes["onclick"] = "javascript:return fnCompSearch('" + Master.PARAM_DATA1 + "', '" + txtCompNm.ClientID + "','" + Master.PARAM_DATA2 + "', '" + hfCompCd.ClientID + "');";

            txtPrimeCost.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtSellingCost.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

           
            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];
            LoadExchageDate();

        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            DataTable dtReturn = new DataTable();

            // 가장 최근의 환율을 조회함.
            // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S01
            dtReturn = ExchangeMngBlo.WatchExchangeRateLastInfo(ddlRentCd.SelectedValue);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(Int32.Parse(strDong).ToString("###,##0")) + "&nbsp;" + TextNm["DONG"].ToString();
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                    }
                }
                else
                {
                    ltRealBaseRate.Text = "-";
                }
            }
        }

        private void LoadSvcZoneData()
        {
            ddlSvcZoneCd.Items.Clear();

            ddlSvcZoneCd.Items.Add(new ListItem(TextNm["ENTIRE"], ""));

            if (!string.IsNullOrEmpty(ddlRentCd.SelectedValue))
            {
                // KN_USP_STK_SELECT_WAREHOUSEINFO_S01
                DataTable dtSvcSection = WarehouseMngBlo.SpreadWarehouseInfo(ddlRentCd.SelectedValue);

                foreach (DataRow dr in dtSvcSection.Select())
                {
                    ddlSvcZoneCd.Items.Add(new ListItem(dr["SvcZoneCd"].ToString(), dr["SvcZoneCd"].ToString()));
                }
            }
        }

        private void LoadGrpData()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIGRPCD_S00
            DataTable dtGrpReturn = MaterialClassificationBlo.SpreadClassiGrpCdInfo(Session["LangCd"].ToString(), strViewDt);

            ddlGrpCd.Items.Clear();

            foreach (DataRow dr in dtGrpReturn.Select())
            {
                ddlGrpCd.Items.Add(new ListItem(dr["CodeNm"].ToString() + " ( " + dr["CodeCd"].ToString() + " )", dr["CodeCd"].ToString()));
            }
        }

        private void LoadMainData()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            if (!string.IsNullOrEmpty(ddlGrpCd.SelectedValue))
            {
                // KN_USP_STK_SELECT_CLASSIMAINCD_S02
                DataTable dtMainReturn = MaterialClassificationBlo.SpreadClassiMainCdInfoWithNoTitle(Session["LangCd"].ToString(), strViewDt, ddlGrpCd.SelectedValue);

                ddlMainCd.Items.Clear();

                foreach (DataRow dr in dtMainReturn.Select())
                {
                    ddlMainCd.Items.Add(new ListItem(dr["CodeNm"].ToString() + " ( " + dr["CodeCd"].ToString() + " )", dr["CodeCd"].ToString()));
                }
            }
            else
            {
                ddlMainCd.Items.Clear();
                ddlMainCd.Items.Add(new ListItem(TextNm["ENTIRE"], string.Empty));
            }
        }
        
        protected void ddlRentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSvcZoneData();
        }

        protected void ddlGrpCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMainData();
        }

        protected void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAuto.Checked)
            {
                txtSubCd.Text = string.Empty;
                txtSubCd.Enabled = CommValue.AUTH_VALUE_FALSE;
            }
            else
            {
                txtSubCd.Enabled = CommValue.AUTH_VALUE_TRUE;
            }
        }

        protected void imgbtnComp_Click(object sender, ImageClickEventArgs e)
        {
            // 팝업의 불법적인 접근을 제한하기 위한 세션 생성
            Session["FindCompYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                bool isExist = CommValue.AUTH_VALUE_FALSE;

                if (!chkAuto.Checked)
                {
                    // KN_USP_STK_SELECT_GOODSINFO_S01
                    DataTable dtReturn = MaterialMngBlo.SpreadExgistGoodsInfo(ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue, ddlMainCd.SelectedValue, txtSubCd.Text);

                    if (dtReturn.Rows.Count > 0)
                    {
                        isExist = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                if (!isExist)
                {
                    int intQty = 0;
                    double dblUnitPrimeCost = 0d;
                    double dblUnitSellingPrice = 0d;
                    double dblVATRatio = 0d;

                    string strDdlVatYn = string.Empty;
                    string strChkEmergencyYn = string.Empty;
                    string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    if (!string.IsNullOrEmpty(txtQty.Text))
                    {
                        intQty = Int32.Parse(txtQty.Text);
                    }

                    if (!string.IsNullOrEmpty(txtPrimeCost.Text))
                    {
                        dblUnitPrimeCost = double.Parse(txtPrimeCost.Text);
                    }

                    if (!string.IsNullOrEmpty(txtSellingCost.Text))
                    {
                        dblUnitSellingPrice = double.Parse(txtSellingCost.Text);
                    }

                    if (!string.IsNullOrEmpty(txtVatRatio.Text))
                    {
                        dblVATRatio = double.Parse(txtVatRatio.Text);
                    }

                    if (!string.IsNullOrEmpty(ddlVatYn.SelectedValue))
                    {
                        if (ddlVatYn.SelectedValue.Equals(CommValue.INCLUDED_TYPE_VALUE_INCLUDED))
                        {
                            strDdlVatYn = CommValue.CONCLUSION_TYPE_TEXT_YES;
                        }
                        else
                        {
                            strDdlVatYn = CommValue.CONCLUSION_TYPE_TEXT_NO;
                        }
                    }

                    if (chkAutoApproval.Checked)
                    {
                        strChkEmergencyYn = CommValue.CONCLUSION_TYPE_TEXT_YES;
                    }
                    else
                    {
                        strChkEmergencyYn = CommValue.CONCLUSION_TYPE_TEXT_NO;
                    }

                    // KN_USP_STK_INSERT_GOODSINFO_M00
                    MaterialMngBlo.RegistryGoodsInfo(ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue, ddlMainCd.SelectedValue,
                                                     txtSubCd.Text, txtCdNm.Text, hfCompCd.Value, intQty, ddlScale.SelectedValue, dblUnitPrimeCost, 
                                                     dblUnitSellingPrice, dblVATRatio, strDdlVatYn, strChkEmergencyYn, txtRemark.Text,
                                                     Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_REGIST_ITEM"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    StringBuilder sbWarning = new StringBuilder();

                    sbWarning.Append("alert('");
                    sbWarning.Append(AlertNm["INFO_CANT_INSERT_DEPTH"]);
                    sbWarning.Append("');");

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
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
    }
}