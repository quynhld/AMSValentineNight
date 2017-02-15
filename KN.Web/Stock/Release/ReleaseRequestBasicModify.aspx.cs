using System;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Stock.Biz;

namespace KN.Web.Stock.Release
{
    public partial class ReleaseRequestBasicModify : BasePage
    {
        bool isStatusCd = CommValue.AUTH_VALUE_FALSE;
        bool isDelYn = CommValue.AUTH_VALUE_TRUE;
        bool isAccept = CommValue.AUTH_VALUE_FALSE;
        bool isDeny = CommValue.AUTH_VALUE_FALSE;
        bool isPending = CommValue.AUTH_VALUE_FALSE;

        string strStatusNm = string.Empty;

        DataTable dtBasicReturn = new DataTable();
        DataTable dtChargerReturn = new DataTable();

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
                    txtHfReleaseSeq.Text = Request.Params[Master.PARAM_DATA1].ToString();

                    isReturn = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturn;
        }

        protected void InitControls()
        {
            ltDpt.Text = TextNm["DEPT"];
            ltProcessMemNo.Text = TextNm["MEM"] + " " + TextNm["NAME"];
            ltProcessDt.Text = TextNm["RELEASED"] + " " + TextNm["DATE"];
            ltApprovalYn.Text = TextNm["STATUS"];
            ltRemark.Text = TextNm["REMARK"];
            ltFmsFee.Text = TextNm["FMS"] + " " + TextNm["CHARGE"];
            ltFmsRemark.Text = TextNm["FMS"] + " " + TextNm["REMARK"];

            txtFmsFee.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            hfToday.Value = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + AlertNm["INFO_CANT_SELECT_PREDATE"] + "');";
            lnkbtnList.Text = TextNm["LIST"];

            lnkbtnModify.Visible = Master.isModDelAuthOk;

        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_STK_SELECT_RELEASEREQUESTINFO_S01
            DataSet dsReturn = ReleaseInfoBlo.WatchReleaseRequestDetailInfo(txtHfReleaseSeq.Text, Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                if (dsReturn.Tables.Count > 0)
                {
                    // 기본정보 조회
                    dtBasicReturn = dsReturn.Tables[0];

                    // 출고정보 조회
                    lvRequestList.DataSource = dtBasicReturn;
                    lvRequestList.DataBind();

                    // 기본정보 조회
                    ltInsDpt.Text = dtBasicReturn.Rows[0]["ProcessDeptNm"].ToString();
                    ltInsProcessMemNm.Text = dtBasicReturn.Rows[0]["ProcessMemNm"].ToString();

                    if (!string.IsNullOrEmpty(dtBasicReturn.Rows[0]["ProcessDt"].ToString()))
                    {
                        txtProcessDt.Text = TextLib.MakeDateEightDigit(dtBasicReturn.Rows[0]["ProcessDt"].ToString());
                        hfProcessDt.Value = TextLib.MakeDateEightDigit(dtBasicReturn.Rows[0]["ProcessDt"].ToString());
                    }

                    if (isStatusCd)
                    {
                        ltInsApproval.Text = strStatusNm;
                        txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_PENDING;
                    }
                    else
                    {
                        DataTable dtStatus = CommCdInfo.SelectSubCdWithTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_APPROVAL);

                        if (isPending)
                        {
                            ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_PENDING)]["CodeNm"].ToString();
                            txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_PENDING;
                        }
                        else
                        {
                            if (isAccept && isDeny)
                            {
                                ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_PARTIALAPPROVAL)]["CodeNm"].ToString();
                                txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_PARTIALAPPROVAL;
                            }
                            else if (isAccept && !isDeny)
                            {
                                ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_APPROVAL)]["CodeNm"].ToString();
                                txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_APPROVAL;
                            }
                            else if (!isAccept && isDeny)
                            {
                                ltInsApproval.Text = dtStatus.Rows[Int32.Parse(CommValue.APPROVAL_TYPE_VALUE_REJECTED)]["CodeNm"].ToString();
                                txtHfProgressCd.Text = CommValue.APPROVAL_TYPE_VALUE_REJECTED;
                            }
                        }
                    }

                    txtRemark.Text = dtBasicReturn.Rows[0]["Remark"].ToString();
                    txtFmsFee.Text = dtBasicReturn.Rows[0]["ProcessFee"].ToString();
                    txtFmsRemark.Text = dtBasicReturn.Rows[0]["ProcessRemark"].ToString();
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
            ((Literal)lvRequestList.FindControl("ltHaveQty")).Text = TextNm["HAVEQTY"];
            ((Literal)lvRequestList.FindControl("ltQty")).Text = TextNm["DEMANDEDQTY"];
            ((Literal)lvRequestList.FindControl("ltProgressCd")).Text = TextNm["RELEASESTATUS"];
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

                if (!string.IsNullOrEmpty(drView["ReleaseDetSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSeq")).Text = drView["ReleaseDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()) &&
                    !string.IsNullOrEmpty(drView["RentCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiGrpCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiMainCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsItem")).Text = drView["ClassNm"].ToString() + " (" + drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + "-" + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString() + ")";
                    ((TextBox)iTem.FindControl("txtHfRentCd")).Text = drView["RentCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfSvcZoneCd")).Text = drView["SvcZoneCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassiGrpCd")).Text = drView["ClassiGrpCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassiMainCd")).Text = drView["ClassiMainCd"].ToString();
                    ((TextBox)iTem.FindControl("txtHfClassCd")).Text = drView["ClassCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RequestQty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = drView["RequestQty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["HaveQty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsHaveQty")).Text = drView["HaveQty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RealQty"].ToString()))
                {
                    ((TextBox)iTem.FindControl("txtHfRealQty")).Text = drView["RealQty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ProgressNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsProgressCd")).Text = drView["ProgressNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StateNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsStatusCd")).Text = drView["StateNm"].ToString();
                    ((TextBox)iTem.FindControl("txtInsStatusCd")).Text = drView["StateCd"].ToString();

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

                if (!string.IsNullOrEmpty(drView["ProgressCd"].ToString()))
                {
                    if (isDelYn && !drView["ProgressCd"].ToString().Equals(CommValue.RELEASE_TYPE_VALUE_WAITING))
                    {
                        isDelYn = CommValue.AUTH_VALUE_FALSE;
                        txtHfProgressCd.Text = drView["ProgressCd"].ToString();
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

                double dblReleaseFee = 0.0d;

                if (!string.IsNullOrEmpty(txtFmsFee.Text.Replace(",",".")))
                {
                    dblReleaseFee = double.Parse(txtFmsFee.Text.Replace(",", "."));
                }

                // 수정
                // KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M02
                ReleaseInfoBlo.ModifyReleaseRequestBasicInfo(txtHfReleaseSeq.Text, hfProcessDt.Value.Replace("-", ""), txtRemark.Text, dblReleaseFee, txtFmsRemark.Text);

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
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
    }
}