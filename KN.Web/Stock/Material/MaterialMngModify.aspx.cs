using System;
using System.Data;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Stock.Biz;

namespace KN.Web.Stock.Material
{
    public partial class MaterialMngModify : BasePage
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
                    if (CheckParams())
                    {
                        InitControls();

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

        protected bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]) &&
                !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2]) &&
                !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3]) &&
                !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA4]) &&
                !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA5]))
            {
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                txtHfSvcZoneCd.Text = Request.Params[Master.PARAM_DATA2].ToString();
                txtHfGrpCd.Text = Request.Params[Master.PARAM_DATA3].ToString();
                txtHfMainCd.Text = Request.Params[Master.PARAM_DATA4].ToString();
                txtHfSubCd.Text = Request.Params[Master.PARAM_DATA5].ToString();

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            //lnkbtnPrint.Text= TextNm["PRINT"];
            ltRentCd.Text = TextNm["SECTIONCD"];
            ltSvcZoneCd.Text = TextNm["WAREHOUSECD"];
            ltGrpCd.Text = TextNm["GRPCD"];
            ltMainCd.Text = TextNm["MAINCD"];
            ltSubCd.Text = TextNm["SUBCD"];
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

            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlScale, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_SCALE);
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlVatYn, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_INCLUDED);

            lnkbtnCdsearch.OnClientClick = "javascript:return fnCompSearch('" + Master.PARAM_DATA1 + "', '" + txtCompNm.ClientID + "','" + Master.PARAM_DATA2 + "', '" + hfCompCd.ClientID + "');";

            txtQty.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtVatRatio.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            lnkbtnRegist.Text = TextNm["MODIFY"];
            lnkbtnRegist.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "')";
            lnkbtnRegist.Visible = Master.isWriteAuthOk;
//          lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["CONF_REGISTER_CONT"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            txtCompNm.Attributes["onclick"] = "javascript:return fnCompSearch('" + Master.PARAM_DATA1 + "', '" + txtCompNm.ClientID + "','" + Master.PARAM_DATA2 + "', '" + hfCompCd.ClientID + "');";

            txtPrimeCost.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtSellingCost.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
        }

        protected void imgbtnComp_Click(object sender, ImageClickEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 팝업의 불법적인 접근을 제한하기 위한 세션 생성
            Session["FindCompYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
        }

        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strAutoApproval = "";
                if (chkAutoApproval.Checked)
                {
                    strAutoApproval = CommValue.CHOICE_VALUE_YES;
                }
                else
                {
                    strAutoApproval = CommValue.CHOICE_VALUE_NO;
                }

                string strScale = ddlScale.SelectedValue;
                string strVatYn = ddlVatYn.SelectedValue;

                if (!string.IsNullOrEmpty(ddlVatYn.SelectedValue))
                {
                    if (ddlVatYn.SelectedValue.Equals(CommValue.INCLUDED_TYPE_VALUE_INCLUDED))
                    {
                        strVatYn = CommValue.CONCLUSION_TYPE_TEXT_YES;
                    }
                    else
                    {
                        strVatYn = CommValue.CONCLUSION_TYPE_TEXT_NO;
                    }
                }

                string strModMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_STK_UPDATE_GOODSINFO_M01
                MaterialMngBlo.ModifyGoodsInfo(txtHfRentCd.Text, txtHfSvcZoneCd.Text, txtHfGrpCd.Text, txtHfMainCd.Text, txtHfSubCd.Text, hfCompCd.Value, 
                                               int.Parse(txtQty.Text), strScale, double.Parse(txtPrimeCost.Text), double.Parse(txtSellingCost.Text),
                                               double.Parse(txtVatRatio.Text), strVatYn, strAutoApproval, txtRemark.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), 
                                               strModMemIP );

                StringBuilder sbList = new StringBuilder();

                sbList.Append(Master.PAGE_VIEW);
                sbList.Append("?");
                sbList.Append(Master.PARAM_DATA1);
                sbList.Append("=");
                sbList.Append(txtHfRentCd.Text);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA2);
                sbList.Append("=");
                sbList.Append(txtHfSvcZoneCd.Text);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA3);
                sbList.Append("=");
                sbList.Append(txtHfGrpCd.Text);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA4);
                sbList.Append("=");
                sbList.Append(txtHfMainCd.Text);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA5);
                sbList.Append("=");
                sbList.Append(txtHfSubCd.Text);

                Response.Redirect(sbList.ToString(), CommValue.AUTH_VALUE_FALSE);
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

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_STK_SELECT_GOODSINFO_S02
            DataTable dtReturn = MaterialMngBlo.WatchGoodsViewInfo(Session["LangCd"].ToString(), txtHfRentCd.Text, txtHfSvcZoneCd.Text, txtHfGrpCd.Text, txtHfMainCd.Text, txtHfSubCd.Text);

            if (dtReturn != null)
            {
                DataRow dr = dtReturn.Rows[0];

                ltInsRentCd.Text = dr["RentNm"].ToString() + " ( " + dr["RentCd"].ToString() + " ) ";
                ltInsSvcZoneCd.Text = dr["SvcZoneCd"].ToString() + " ( " + dr["SvcZoneRemark"].ToString() + " ) ";
                ltInsGrpCd.Text = dr["ClassiGrpNm"].ToString() + " ( " + dr["ClassiGrpCd"].ToString() + " ) ";
                ltInsMainCd.Text = dr["ClassiMainNm"].ToString() + " ( " + dr["ClassiMainCd"].ToString() + " ) ";
                ltInsSubCd.Text = dr["ClassNm"].ToString() + " ( " + dr["ClassCd"].ToString() + " ) ";

                if (!string.IsNullOrEmpty(dr["CompNm"].ToString()))
                {
                    txtCompNm.Text = TextLib.StringDecoder(dr["CompNm"].ToString());
                    hfCompCd.Value = dr["CompNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["Qty"].ToString()))
                {
                    txtQty.Text = dr["Qty"].ToString();
                }
                else
                {
                    txtQty.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(dr["ReadyQty"].ToString()))
                {
                    txtReadyQty.Text = dr["ReadyQty"].ToString();
                }
                else
                {
                    txtReadyQty.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                ltSumQty.Text = TextLib.MakeVietIntNo((Double.Parse(txtQty.Text) + Double.Parse(txtReadyQty.Text)).ToString("###,##0"));

                if (!string.IsNullOrEmpty(dr["UnitPrimeCost"].ToString()))
                {
                    txtPrimeCost.Text = dr["UnitPrimeCost"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["UnitSellingPrice"].ToString()))
                {
                    txtSellingCost.Text = dr["UnitSellingPrice"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["VATRatio"].ToString()))
                {
                    txtVatRatio.Text = dr["VATRatio"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["Remark"].ToString()))
                {
                    txtRemark.Text = TextLib.StringDecoder(dr["Remark"].ToString());
                }

                if (!string.IsNullOrEmpty(dr["EmergencyYn"].ToString()))
                {
                    string strAutoApproval = dr["EmergencyYn"].ToString();

                    if (strAutoApproval.Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        chkAutoApproval.Checked = CommValue.AUTH_VALUE_TRUE;
                    }
                    else
                    {
                        chkAutoApproval.Checked = CommValue.AUTH_VALUE_FALSE;
                    }                    
                }

                if (!string.IsNullOrEmpty(dr["ScaleCd"].ToString()))
                {
                    DataTable dtReturn1 = new DataTable();

                    dtReturn1 = CommCdInfo.SelectSubCdWithTitle(CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_SCALE);

                    if (dtReturn1.Rows.Count > 0)
                    {
                        ddlScale.SelectedValue = dtReturn1.Rows[Int32.Parse(dr["ScaleCd"].ToString())]["CodeCd"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(dr["VATYn"].ToString()))
                {
                    string strVATYn = dr["VATYn"].ToString();

                    if (strVATYn.Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        ddlVatYn.SelectedValue = CommValue.INCLUDED_TYPE_VALUE_INCLUDED;
                    }
                    else
                    {
                        ddlVatYn.SelectedValue = CommValue.INCLUDED_TYPE_VALUE_EXCEPTIONG;
                    }
                }
            }
        }

        protected void txtQty_TextChanged(object sender, EventArgs e)
        {
            double dblQty = 0.0d;
            double dblReadyQty = 0.0d;

            if (!string.IsNullOrEmpty(txtQty.Text))
            {
                dblQty = double.Parse(txtQty.Text);
            }

            if (!string.IsNullOrEmpty(txtReadyQty.Text))
            {
                dblReadyQty = double.Parse(txtReadyQty.Text);
            }

            ltSumQty.Text = TextLib.MakeVietIntNo((dblQty + dblReadyQty).ToString("###,##0"));
        }
    }
}