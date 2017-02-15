using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Manage.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Management.Manage
{
    public partial class APTDebitList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    CheckParam();

                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params["RentCd"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["RentCd"].ToString()))
                {
                    txtHfRentCd.Text = Request.Params["RentCd"].ToString();
                }
                else
                {
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
                }
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
            }
        }

        protected void InitControls()
        {
            ltYear.Text = TextNm["YEARS"];
            //ltMonth.Text = TextNm["MONTH"];
            MakeItemDdl();

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnMakingLine('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            //ltTopPayment.Text = TextNm["PAYMENTKIND"];
            //ltTopMngYYYYMM.Text = TextNm["MONTH"];
            //ltTopRoomNo.Text = TextNm["ROOMNO"];
            //ltTopMovieInDt.Text = TextNm["OCCUDT"];
            //ltTopAmt.Text = TextNm["AMT"];

            lnkbtnMakeLine.Text = TextNm["MAKEBILL"];
            lnkbtnMakeLine.OnClientClick = "javascript:return fnMakingLine('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

        }

        protected void LoadData()
        {
            //KN_SCR_SELECT_APT_DEBIT_LIST
            var dtReturn = MngPaymentBlo.SpreadAptDebitList(ddlItems.SelectedValue, txtSearchDt.Text.Replace("-", ""), txtTennantNmS.Text.Trim(), txtRoomNoS.Text.Trim(), rbIsDebit.SelectedValue);

            if (dtReturn == null) return;
            lvMngManualList.DataSource = dtReturn;
            lvMngManualList.DataBind();
        }


        protected void MakeItemDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);

            ddlItems.Items.Clear();

            ddlItems.Items.Add(new ListItem(TextNm["PAYMENTKIND"], ""));

            foreach (var dr in dtReturn.Select())
            {
                if (dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_MNGFEE) ||


                    dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE))
                {
                    ddlItems.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        protected void lvMngManualList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
            if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
            {
                // ListView에서 빈 데이터의 경우 알림메세지 정의
                ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
            }
        }

        protected void lvMngManualList_LayoutCreated(object sender, EventArgs e)
        {
        }

        protected void lvMngManualList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                var iTem = (ListViewDataItem)e.Item;
                var drView = (DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["FeeTy"].ToString()))
                {
                    var txtHfFeeTy = (TextBox)iTem.FindControl("txtHfFeeTy");
                    txtHfFeeTy.Text = drView["FeeTy"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    var txtHfRentCd = (TextBox)iTem.FindControl("txtHfRentCd");
                    txtHfRentCd.Text = drView["RentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["Fee_Seq"].ToString()))
                {
                    var txthfFeeSeq = (TextBox)iTem.FindControl("txthfFeeSeq");
                    txthfFeeSeq.Text = drView["Fee_Seq"].ToString();
                }

                var ltPeriod = (Literal)iTem.FindControl("ltPeriod");

                if (!string.IsNullOrEmpty(drView["RentalYear"].ToString()) &&
                    !string.IsNullOrEmpty(drView["RentalMM"].ToString()))
                {
                        ltPeriod.Text = drView["RentalYear"] + "/" + drView["RentalMM"];               
                }

                var ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    ltRoomNo.Text = drView["RoomNo"].ToString();
                }

                var ltFeeTy = (Literal)iTem.FindControl("ltFeeTy");

                if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
                {
                    ltFeeTy.Text = drView["FeeNm"].ToString();
                }

                var ltTenantNm = (Literal)iTem.FindControl("ltTenantNm");

                if (!string.IsNullOrEmpty(drView["TenantsNm"].ToString()))
                {
                    ltTenantNm.Text = TextLib.TextCutString(TextLib.StringDecoder(drView["TenantsNm"].ToString()), 22, "..");
                }

                var ltVndAmt = (Literal)iTem.FindControl("ltVNDAmt");

                if (!string.IsNullOrEmpty(drView["VNDRoundingAmount"].ToString()))
                {
                    ltVndAmt.Text = TextLib.MakeVietIntNo(double.Parse(drView["VNDRoundingAmount"].ToString()).ToString("###,##0"));
                }


                var ltUsdAmt = (Literal)iTem.FindControl("ltUSDAmt");

                if (!string.IsNullOrEmpty(drView["USDAmount"].ToString()))
                {
                    ltUsdAmt.Text = TextLib.MakeVietIntNo(double.Parse(drView["USDAmount"].ToString()).ToString("###,##0")) +" $";
                }

                var ltExRate = (Literal)iTem.FindControl("ltExRate");

                if (!string.IsNullOrEmpty(drView["ExchangeRate"].ToString()))
                {
                    ltExRate.Text = TextLib.MakeVietIntNo(double.Parse(drView["ExchangeRate"].ToString()).ToString("###,##0"));
                }
            }
        }

        protected void lnkbtnMakeLine_Click(object sender, EventArgs e)
        {
            try
            {
                var isHas = false;
                var objReturn = new object[2];
                var refPrintNo = string.Empty;
                InvoiceMngBlo.UpdatedAptDebitPrintNo(refPrintNo, "0");
                if (ddlItems.SelectedValue.Equals(CommValue.RECEIT_VALUE_MNGFEE))
                {
                    foreach (var t in lvMngManualList.Items)
                    {
                        if (((CheckBox)t.FindControl("chkboxList")).Checked)
                        {
                            var feeSeq = ((TextBox) t.FindControl("txthfFeeSeq")).Text;
                            if (string.IsNullOrEmpty(refPrintNo))
                            {
                                refPrintNo = feeSeq;
                            }
                            //KN_SCR_UPDATE_DEBIT_PRINTNO_M00
                            objReturn = InvoiceMngBlo.UpdatedAptDebitPrintNo(refPrintNo, feeSeq);
                            isHas = true;
                        }
                    }
                    
                }
                else if (ddlItems.SelectedValue.Equals(CommValue.RECEIT_VALUE_UTILFEE))
                {
                    foreach (var t in lvMngManualList.Items)
                    {
                        if (((CheckBox)t.FindControl("chkboxList")).Checked)
                        {
                            var feeSeq = ((TextBox)t.FindControl("txthfFeeSeq")).Text;
                            if (string.IsNullOrEmpty(refPrintNo))
                            {
                                refPrintNo = feeSeq;
                            }
                            //KN_SCR_UPDATE_DEBIT_PRINTNO_M00
                            objReturn = InvoiceMngBlo.UpdatedAptDebitPrintNo(refPrintNo, feeSeq);
                            isHas = true;
                        }
                    }
                }

                if (objReturn == null || !isHas) return;
                txtHfRefPrintNo.Text = refPrintNo;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "LoadPopupDebit('" + refPrintNo + "')", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            var isAllCheck = chkAll.Checked;

            try
            {
                CheckAll(isAllCheck);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
        private void CheckAll(bool isAllCheck)
        {
            foreach (var t in lvMngManualList.Items)
            {
                if (((CheckBox)t.FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)t.FindControl("chkboxList")).Checked = isAllCheck;
                }
            }
        }

        protected void imgUpdatePrint_Click(object sender, ImageClickEventArgs e)
        {
            InvoiceMngBlo.UpdatedAptDebitPrinted(txtHfRefPrintNo.Text);
            LoadData();
        }
    }
}