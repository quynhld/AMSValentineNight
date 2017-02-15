using System;
using System.Data;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Stock.Biz;

namespace KN.Web.Stock.Material
{
    public partial class MaterialMngView : BasePage
    {
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
            ltInventoryReport.Text = TextNm["INVENTORYREPORT"];
            ltClassi.Text = TextNm["CLASSI"];
            ltItemNm.Text = TextNm["ITEM"] + TextNm["NAME"];
            ltComp.Text = TextNm["COMPNM"];
            ltPrimeCost.Text = TextNm["PRIMECOST"];
            ltPrimeDong.Text = TextNm["DONG"];
            ltSellingPrice.Text = TextNm["SELLINGCOST"];
            ltSellingDong.Text = TextNm["DONG"];
            ltAutoApproval.Text = TextNm["AUTOAPPROVAL"];
            ltQty.Text = TextNm["QTY"];
            ltVATRatio.Text = TextNm["VAT"];
            ltVATYn.Text = TextNm["INCLUDEVAT"];
            ltStatus.Text = TextNm["USEYN"];
            ltRemark.Text = TextNm["REMARK"];

            ltDetails.Text = TextNm["DETAILS"];
            ltReleaseReq.Text = TextNm["RELEASE"] + TextNm["REQUEST"];
            ltOrderReq.Text = TextNm["ORDER"] + TextNm["REQUEST"];
            ltQuantity.Text = TextNm["QTY"];


            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnList.Text = TextNm["LIST"];
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

                ltInsClassi.Text = dr["RentNm"].ToString() + "> " + dr["ClassiGrpNm"].ToString() + "> " + dr["ClassiMainNm"].ToString();
                ltInsItemNm.Text = dr["ClassNm"].ToString();
                ltGoodsCd.Text = dr["RentCd"].ToString() + dr["SvcZoneCd"].ToString() + "-" + dr["ClassiGrpCd"].ToString() + dr["ClassiMainCd"].ToString() + dr["ClassCd"].ToString();
                imgbtnGraph.OnClientClick = "javascript:fnGraphPopup('" + dr["RentCd"].ToString() + "','" + dr["SvcZoneCd"].ToString() + "','" + dr["ClassiGrpCd"].ToString() + "','" + dr["ClassiMainCd"].ToString() + "','" + dr["ClassCd"].ToString() + "');";
                imgPos.ToolTip = dr["SvcZoneRemark"].ToString();

                if (!string.IsNullOrEmpty(dr["CompNm"].ToString()))
                {
                    ltInsComp.Text = TextLib.StringDecoder(dr["CompNm"].ToString()) + " ( " + dr["CompNo"].ToString() + " )";
                }

                if (!string.IsNullOrEmpty(dr["UnitPrimeCost"].ToString()))
                {
                    ltInsPrimeCost.Text = dr["UnitPrimeCost"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["UnitSellingPrice"].ToString()))
                {
                    ltInsSellingPrice.Text = dr["UnitSellingPrice"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["EmergencyYn"].ToString()))
                {
                    ltInsAutoApproval.Text = dr["EmergencyYn"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["Qty"].ToString()))
                {
                    double dblReadyQty = 0.0d;
                    double dblQty = 0.0d;

                    if (!string.IsNullOrEmpty(dr["ReadyQty"].ToString()))
                    {
                        dblReadyQty = Double.Parse(dr["ReadyQty"].ToString());
                    }

                    dblQty = Double.Parse(dr["Qty"].ToString());

                    ltInsQty.Text = (dblQty + dblReadyQty).ToString();
                }

                if (!string.IsNullOrEmpty(dr["ScaleNm"].ToString()))
                {
                    ltScale.Text = dr["ScaleNm"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["VATRatio"].ToString()))
                {
                    ltInsVATRatio.Text = dr["VATRatio"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["VATYn"].ToString()))
                {
                    ltInsVATYn.Text = dr["VATYn"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["StatusNm"].ToString()))
                {
                    ltInsStatus.Text = dr["StatusNm"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["Remark"].ToString()))
                {
                    txtRemark.Text = TextLib.StringDecoder(dr["Remark"].ToString());
                }
            }

            // KN_USP_STK_SELECT_GOODSINFO_S03
            DataTable dtDetail = MaterialMngBlo.WatchGoodsDetailViewInfo(txtHfRentCd.Text, txtHfSvcZoneCd.Text, txtHfGrpCd.Text, txtHfMainCd.Text, txtHfSubCd.Text);

            if (dtDetail != null)
            {
                DataRow dr = dtDetail.Rows[0];

                if (!string.IsNullOrEmpty(dr["ReleaseQty"].ToString()))
                {
                    ltInsReleaseQt.Text = dr["ReleaseQty"].ToString();
                }
                else
                {
                    ltInsReleaseQt.Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(dr["OrderQty"].ToString()))
                {
                    ltInsOrderQt.Text = dr["OrderQty"].ToString();
                }
                else
                {
                    ltInsOrderQt.Text = CommValue.NUMBER_VALUE_ZERO;
                }
            }

        }

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbList = new StringBuilder();

                sbList.Append(Master.PAGE_MODIFY);
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

        protected void imgbtnCheck_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Session["GraphPopup"] = CommValue.CHOICE_VALUE_YES;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}
