using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;

namespace KN.Web.Management.Manage
{
    public partial class MonthMngInfoList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
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

            if (!string.IsNullOrEmpty(hfCurrentPage.Value))
            {
                intPageNo = Int32.Parse(hfCurrentPage.Value);
                hfCurrentPage.Value = intPageNo.ToString();
            }
            else
            {
                intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfCurrentPage.Value = intPageNo.ToString();
            }

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
            {
                txtHfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();
                txtHfFeeTy.Value = Request.Params[Master.PARAM_DATA4].ToString();
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            
            lnkbtnRegist.Visible = Master.isWriteAuthOk;
            txtFeeNet.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            txtVAT.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnConfirmSave('');";
            MakeYearDdl();
        }


        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            var dsReturn = new DataSet();
           // var dateTime = txtSearchDt.Text.Replace("-", "");
            var dateTime = ddlYear.SelectedValue;//string.IsNullOrEmpty(dateTime) ? "" : dateTime.Substring(0, 4);
            // KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S00
            dsReturn = MngPaymentBlo.SpreadMngInfoList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Value, txtHfFeeTy.Value, dateTime);

            if (dsReturn == null) return;
            lvMngFeeList.DataSource = dsReturn.Tables[1];
            lvMngFeeList.DataBind();
            sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                                                     , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));
            spanPageNavi.InnerHtml = sbPageNavi.ToString();
            txtVAT.Text = dsReturn.Tables[2].Rows[0]["VatRatio"].ToString();
        }

        /// <summary>
        /// 상세보기 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbView = new StringBuilder();
                //sbView.Append(Master.PAGE_VIEW);
                //sbView.Append("?");
                //sbView.Append(Master.PARAM_DATA1);
                //sbView.Append("=");
                //sbView.Append(txtHfRentCd.Text);
                //sbView.Append("&");
                //sbView.Append(Master.PARAM_DATA2);
                //sbView.Append("=");
                //sbView.Append(hfMngYear.Value);
                //sbView.Append("&");
                //sbView.Append(Master.PARAM_DATA3);
                //sbView.Append("=");
                //sbView.Append(hfMngMM.Value);
                //sbView.Append("&");
                //sbView.Append(Master.PARAM_DATA4);
                //sbView.Append("=");
                //sbView.Append(txtHfFeeTy.Text);

                Session["ConsultingOk"] = CommValue.CONCLUSION_TYPE_TEXT_YES;

                Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngFeeList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngFeeList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!string.IsNullOrEmpty(drView["FeeTy"].ToString()))
            {
                var ltFeeName = (Literal)iTem.FindControl("ltFeeName");
                ltFeeName.Text = drView["FeeName"].ToString();

                var txtHfFeeType = (TextBox)iTem.FindControl("txtHfFeeType");
                txtHfFeeType.Text = drView["FeeTy"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["MngYear"].ToString()))
            {
                var strDate = drView["MngYear"].ToString() + drView["MngMM"];
                var ltAppliedDt = (Literal)iTem.FindControl("ltAppliedDt");
                ltAppliedDt.Text = TextLib.MakeDateSixDigit(strDate);

                var txtHfMngYear = (TextBox)iTem.FindControl("txtHfMngYear");
                txtHfMngYear.Text = drView["MngYear"].ToString();

                var txtHfMngMM = (TextBox)iTem.FindControl("txtHfMngMM");
                txtHfMngMM.Text = drView["MngMM"].ToString();

                var txtHfMngFeeCode = (TextBox)iTem.FindControl("txtHfMngFeeCode");
                txtHfMngFeeCode.Text = drView["MngFeeCd"].ToString();

            }

            if (!string.IsNullOrEmpty(drView["MngFee"].ToString()))
            {
                var mngFee = drView["MngFee"].ToString();
                var txtMngFee = (TextBox)iTem.FindControl("txtMngFee");
                txtMngFee.Text = TextLib.MakeVietIntNo(mngFee);
                txtMngFee.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";               
            }
            var ltVat = (Literal)iTem.FindControl("ltVAT");
            if (!string.IsNullOrEmpty(drView["VatRatio"].ToString()))
            {
                var vat = drView["VatRatio"].ToString();

                ltVat.Text = TextLib.MakeVietIntNo(vat);
                var ltUnitPrice = (Literal)iTem.FindControl("ltUnitPrice");
                ltUnitPrice.Text = TextLib.MakeVietIntNo((double.Parse(drView["MngFee"].ToString()) * (1 + (double.Parse(vat) / 100))).ToString("###,##0"));
            }
            else
            {
                ltVat.Text = "0";
                var ltUnitPrice = (Literal)iTem.FindControl("ltUnitPrice");
                ltUnitPrice.Text = TextLib.MakeVietIntNo(double.Parse(drView["MngFee"].ToString()).ToString("###,##0"));
            }    

            var imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
            imgbtnModify.Visible = Master.isModDelAuthOk;
            imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

        }
        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            var strInsMemIP = Request.ServerVariables["REMOTE_ADDR"];

            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var dateTime = txtFeeAppDt.Text.Replace("-","");
                // KN_USP_MNG_SELECT_MONTHMNGMENUINFO_S02
                var dtReturn = MngPaymentBlo.SpreadMngInfo(Session["LangCd"].ToString(), txtHfRentCd.Value, txtHfFeeTy.Value, dateTime.Substring(0,4), dateTime.Substring(4,2));

                if (dtReturn == null) return;
                if (dtReturn.Rows.Count > 0)
                {
                    hfAlertText.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];
                    
                }
                else
                {
                    //CheckBox chkInsUseYn = (CheckBox)lvMonthMngInfoWrite.Items[intTmpI].FindControl("chkInsUseYn");

                    //Literal ltInsMngFeeCd = (Literal)lvMonthMngInfoWrite.Items[intTmpI].FindControl("ltInsMngFeeCd");

                    //TextBox txtInsMngFeeNet = (TextBox)lvMonthMngInfoWrite.Items[intTmpI].FindControl("txtInsMngFeeNet");

                    //if (string.IsNullOrEmpty(txtInsMngFeeNet.Text))
                    //{
                    //    txtInsMngFeeNet.Text = CommValue.NUMBER_VALUE_ZERO;
                    //}

                    //if (chkInsUseYn.Checked)
                    //{
                    const string strChkYn = CommValue.CONCLUSION_TYPE_TEXT_YES;
                    //}

                    //DateTime dtLastDay = new DateTime(Int32.Parse(txtHfYear.Text), Int32.Parse(txtHfMonth.Text), 1).AddMonths(0).AddDays(-1);  //마지막 일

                    const string strLastDay = "99999999"; //dtLastDay.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");

                    // KN_USP_MNG_INSERT_MONTHMNGMENUINFO_M00
                           
                        
                    MngPaymentBlo.RegistryMonthMngInfo(txtHfRentCd.Value, txtHfFeeTy.Value, txtHfFeeTy.Value, dateTime.Substring(0, 4), dateTime.Substring(4, 2), txtFeeNet.Text,
                                                       strChkYn, strLastDay, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP,txtVAT.Text);
                        
                    //Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA4 + "=" + txtHfFeeTy.Text, CommValue.AUTH_VALUE_FALSE);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }
        /// <summary>
        /// 페이징버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkSearch_Click(object sender, EventArgs e)
        {
           LoadData();
        }

        protected void lvMngFeeList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            var strInsMemIP = Request.ServerVariables["REMOTE_ADDR"];
            var feeType = (TextBox)lvMngFeeList.Items[e.ItemIndex].FindControl("txtHfFeeType");
            var MngYear = (TextBox)lvMngFeeList.Items[e.ItemIndex].FindControl("txtHfMngYear");
            var MngMM = (TextBox)lvMngFeeList.Items[e.ItemIndex].FindControl("txtHfMngMM");
            var MngFeeCd = (TextBox)lvMngFeeList.Items[e.ItemIndex].FindControl("txtHfMngFeeCode");
            var MngFee = (TextBox)lvMngFeeList.Items[e.ItemIndex].FindControl("txtMngFee");
            var MngVAT = (Literal)lvMngFeeList.Items[e.ItemIndex].FindControl("ltVAT");
            try
            {
                MngPaymentBlo.ModifyMonthMngInfo(txtHfRentCd.Value, feeType.Text, MngFeeCd.Text, MngYear.Text, MngMM.Text, MngFee.Text, "Y", "99999999", Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP,MngVAT.Text);
                LoadData();
            }
            catch (Exception ex)
            {

                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngFeeList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();

            for (var intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.Year + 5; intTmpI++)
            {
                ddlYear.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }

            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
           LoadData();
        }
    }
}