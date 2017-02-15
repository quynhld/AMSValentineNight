using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Stock.Biz;

namespace KN.Web.Stock.Order
{
    public partial class GoodsOrderBasicModify : BasePage
    {
        bool isStatusCd = CommValue.AUTH_VALUE_FALSE;
        bool isAccept = CommValue.AUTH_VALUE_FALSE;
        bool isDeny = CommValue.AUTH_VALUE_FALSE;
        bool isPending = CommValue.AUTH_VALUE_FALSE;

        string strStatusNm = string.Empty;

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
                        LoadData();

                        InitControls();
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                        sbWarning.Append("');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
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
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    txtHfOrderSeq.Text = Request.Params[Master.PARAM_DATA1].ToString();

                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturn;
        }

        protected void InitControls()
        {
            ltDpt.Text = TextNm["DEPT"];
            ltProcessMemNo.Text = TextNm["MEM"] + " " + TextNm["NAME"];
            ltProcessDt.Text = TextNm["WAREHOUSING"] + " " + TextNm["DATE"];
            ltApproval.Text = TextNm["STATUS"];
            ltRemark.Text = TextNm["REMARK"];

            hfToday.Value = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnCancel.Text = TextNm["CANCEL"];

            lnkbtnModify.Visible = Master.isModDelAuthOk;
            divModi.Visible = Master.isModDelAuthOk;
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_STK_SELECT_GOODSORDERINFO_S01
            DataSet dsReturn = GoodsOrderInfoBlo.WatchGoodsOrderDetailInfo(txtHfOrderSeq.Text, Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                if (dsReturn.Tables.Count > 0)
                {
                    // 기본정보 조회
                    DataTable dtBasicReturn = dsReturn.Tables[0];

                    // 발주정보 조회
                    lvRequestList.DataSource = dtBasicReturn;
                    lvRequestList.DataBind();

                    // 기본정보 조회
                    ltInsDpt.Text = dtBasicReturn.Rows[0]["ProcessDeptNm"].ToString();
                    ltInsProcessMemNm.Text = dtBasicReturn.Rows[0]["ProcessMemNm"].ToString();
                    txtHfOrderSeq.Text = dtBasicReturn.Rows[0]["OrderSeq"].ToString();

                    if (!string.IsNullOrEmpty(dtBasicReturn.Rows[0]["ProcessDt"].ToString()))
                    {
                        txtProcessDt.Text = TextLib.MakeDateEightDigit(dtBasicReturn.Rows[0]["ProcessDt"].ToString());
                        hfProcessDt.Value = TextLib.MakeDateEightDigit(dtBasicReturn.Rows[0]["ProcessDt"].ToString());
                    }

                    if (isStatusCd)
                    {
                        ltInsApproval.Text = strStatusNm;
                        //txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_PENDING;
                    }
                    else
                    {
                        DataTable dtStatus = CommCdInfo.SelectSubCdWithTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_APPROVAL);

                        if (isPending)
                        {
                            ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_PENDING)]["CodeNm"].ToString();
                        }
                        else
                        {
                            if (isAccept && isDeny)
                            {
                                ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_PARTIALAPPROVAL)]["CodeNm"].ToString();
                            }
                            else if (isAccept && !isDeny)
                            {
                                ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_APPROVAL)]["CodeNm"].ToString();
                            }
                            else if (!isAccept && isDeny)
                            {
                                ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_REJECTED)]["CodeNm"].ToString();
                            }
                        }
                    }
                    txtRemark.Text = dtBasicReturn.Rows[0]["Remark"].ToString();
                }
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRequestList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvRequestList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvRequestList.FindControl("ltItem")).Text = TextNm["ITEM"] + " " + TextNm["NAME"];
            ((Literal)lvRequestList.FindControl("ltCompNm")).Text = TextNm["COMPNM"];
            ((Literal)lvRequestList.FindControl("ltQty")).Text = TextNm["DEMANDEDQTY"];
            ((Literal)lvRequestList.FindControl("ltSellingPrice")).Text = TextNm["SELLINGCOST"];
            ((Literal)lvRequestList.FindControl("ltTotPrice")).Text = TextNm["TOTAL"];
            ((Literal)lvRequestList.FindControl("ltProgressCd")).Text = TextNm["PURREQSTATUS"];
            ((Literal)lvRequestList.FindControl("ltStatusCd")).Text = TextNm["STATUS"];
        }

        /// <summary>
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvRequestList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["OrderDetSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSeq")).Text = drView["OrderDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()) &&
                    !string.IsNullOrEmpty(drView["RentCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiGrpCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiMainCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItem")).Text = drView["ClassNm"].ToString() + " (" + drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + "-" + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString() + ")";
                }

                if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsCompNm")).Text = drView["CompNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RequestQty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = drView["RequestQty"].ToString();
                }
                else
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["UnitSellingPrice"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSellingPrice")).Text = TextLib.MakeVietIntNo(double.Parse(drView["UnitSellingPrice"].ToString()).ToString("###,##0"));
                    ((Literal)iTem.FindControl("ltInsTotPrice")).Text = TextLib.MakeVietIntNo((double.Parse(drView["RequestQty"].ToString()) * double.Parse(drView["UnitSellingPrice"].ToString())).ToString("###,##0"));
                }
                else
                {
                    ((Literal)iTem.FindControl("ltInsSellingPrice")).Text = CommValue.NUMBER_VALUE_ZERO;
                    ((Literal)iTem.FindControl("ltInsTotPrice")).Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["ProgressNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsProgressCd")).Text = drView["ProgressNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StateNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsStatusCd")).Text = drView["StateNm"].ToString();

                    if (!isStatusCd && drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_PENDING))
                    {
                        isStatusCd = CommValue.AUTH_VALUE_TRUE;
                        strStatusNm = drView["StateNm"].ToString();
                    }

                    if (drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_PENDING))
                    {
                        isPending = CommValue.AUTH_VALUE_TRUE;
                    }

                    if (drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_APPROVAL))
                    {
                        isAccept = CommValue.AUTH_VALUE_TRUE;
                    }

                    if (drView["StateCd"].ToString().Equals(CommValue.APPROVAL_TYPE_VALUE_REJECTED))
                    {
                        isDeny = CommValue.AUTH_VALUE_TRUE;
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

                // 수정
                // KN_USP_STK_UPDATE_GOODSORDERINFO_M01
                GoodsOrderInfoBlo.ModifyGoodsOrderBasicInfo(txtHfOrderSeq.Text, hfProcessDt.Value.Replace("-", ""), txtRemark.Text);

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
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
