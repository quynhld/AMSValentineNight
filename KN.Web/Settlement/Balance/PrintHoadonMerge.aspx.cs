using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Config.Biz;
using KN.Manage.Biz;
using KN.Resident.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class PrintHoadonMerge : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (IsPostBack) return;
                
                CheckParam();
                InitControls();
              
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params[Master.PARAM_DATA1] == null) return;
            //hfUserSeq.Value = Request.Params[Master.PARAM_DATA1];
            hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
        }
        protected void InitControls()
        {
            string strMaxInvoiceNo = "0000000";
            

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:formatMoney()";

            // 섹션코드 조회
            // LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            // 차종 조회
          

            txtAmount.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtExRate.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            //KN_USP_SET_SELECT_NEXTINVOICE_NO
            DataTable dtReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MaxInvoiceNo"].ToString()))
                    {
                        strMaxInvoiceNo = dtReturn.Rows[0]["MaxInvoiceNo"].ToString().PadLeft(7, '0');
                    }
                }
            }

            ltInsMaxNo.Text = strMaxInvoiceNo;
           
            lnkbtnRegist.Visible = Master.isWriteAuthOk;
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_MNG_SELECT_RENOVATION_APT_S00
            //var currentDt = txtSearchDt.Text.Replace("-", "");
            string strMaxInvoiceNo = string.Empty;
            ResetInputControls();

            var IssDt = txtSearchDt.Text.Replace("-", "");
            //KN_USP_MNG_SELECT_ISS_TOTAL_S00
            var dsReturn = ReceiptMngBlo.SelectIssAmtPrintoutHoadonMerge(IssDt);
            txtExRate.Text = double.Parse(dsReturn.Tables[0].Rows[0]["IssTotal"].ToString()).ToString("###,##0");
            if (dsReturn.Tables[1].Rows.Count > 0)
            {
                txtAmount.Text = double.Parse(dsReturn.Tables[1].Rows[0]["Total"].ToString()).ToString("###,##0");
                txtRealAmount.Text = double.Parse(dsReturn.Tables[1].Rows[0]["InvTotal"].ToString()).ToString("###,##0");
                txtAmount.Enabled = false;
                txtRealAmount.Enabled = false;
            }
            else
            {
                txtAmount.Enabled = true;
                txtRealAmount.Enabled = true;
            }

            //KN_USP_SET_SELECT_NEXTINVOICE_NO
            DataTable dtReturn = InvoiceMngBlo.SelectMaxInvoiceNo(CommValue.SUB_COMP_CD);
            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["MaxInvoiceNo"].ToString()))
                    {
                        strMaxInvoiceNo = dtReturn.Rows[0]["MaxInvoiceNo"].ToString().PadLeft(7, '0');
                    }
                }
            }

            ltInsMaxNo.Text = strMaxInvoiceNo;
        }

        /// <summary>
        /// 검색버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
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

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPaymentList_ItemCreated(object sender, ListViewItemEventArgs e)
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
        protected void lvPaymentDetails_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvPaymentList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;

            if (!string.IsNullOrEmpty(drView["RefSeq"].ToString()))
            {
                var txthfSeq = (TextBox)iTem.FindControl("txthfSeq");
                txthfSeq.Text = TextLib.StringDecoder(drView["RefSeq"].ToString());
            }


            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoom = (Literal)iTem.FindControl("ltRoom");
                ltRoom.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
            {
                var ltInsName = (Literal)iTem.FindControl("ltInsName");
                ltInsName.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["FeeNm"].ToString()))
            {
                var ltFeeTy = (Literal)iTem.FindControl("ltFeeTy");
                ltFeeTy.Text = TextLib.StringDecoder(drView["FeeNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["DepositPayDt"].ToString()))
            {
                var ltPayDay = (Literal)iTem.FindControl("ltPayDay");
                ltPayDay.Text = TextLib.MakeDateEightDigit(drView["DepositPayDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["DepositReturnDt"].ToString()))
            {
                var ltReturnDate = (Literal)iTem.FindControl("ltReturnDate");
                ltReturnDate.Text = TextLib.MakeDateEightDigit(drView["DepositReturnDt"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["DepositPayAmt"].ToString()))
            {
                var ltInsTotalAmt = (Literal)iTem.FindControl("ltInsTotalAmt");
                ltInsTotalAmt.Text = TextLib.MakeVietIntNo(double.Parse(TextLib.StringDecoder(drView["DepositPayAmt"].ToString())).ToString("###,##0"));
            }

            var imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
            imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            var imgbtnPrint = (ImageButton)iTem.FindControl("imgbtnPrint");
            imgbtnPrint.OnClientClick = "javascript:return fnConfirmR('" + AlertNm["CONF_DELETE_ITEM"] + "')";
            if (!string.IsNullOrEmpty(drView["DepositReturnDt"].ToString()))
            {
                imgbtnPrint.Visible = false;
            }
        }



        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtRefPrint = new DataTable();
                var strRefPrint = string.Empty;
                var strPaydt = txtSearchDt.Text.Replace("-", "");
                var strPrintdt = txtPayDay.Text.Replace("-", "");
                var strInvAmt = txtRealAmount.Text;
                double dbInvAmt = CommValue.NUMBER_VALUE_0_0;


                if (!string.IsNullOrEmpty(strInvAmt))
                {
                    dbInvAmt = double.Parse(strInvAmt);
                }
                var insCompCd = Session["CompCd"].ToString();
                var insMemNo = Session["MemNo"].ToString();
                var insMemIP = Session["UserIP"].ToString();

                var dsReturn = ReceiptMngBlo.SelectIssAmtPrintoutHoadonMerge(strPaydt);
                if (dsReturn.Tables[1].Rows.Count > 0)
                {
                    strRefPrint = dsReturn.Tables[2].Rows[0]["REF_SERIAL_NO"].ToString();
                }
                else
                {

                    //KN_USP_CREATE_HOADON_MERGE_U00
                    dtRefPrint = MngPaymentBlo.InsertPrintoutInvoiceMerge(strPaydt, strPrintdt, dbInvAmt, insCompCd, insMemNo, insMemIP);

                    if (dtRefPrint != null)
                    {
                        if (dtRefPrint.Rows.Count > CommValue.NUMBER_VALUE_0)
                        {
                            strRefPrint = dtRefPrint.Rows[0]["RefPrint"].ToString();
                        }
                    }                   
                 }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + strPaydt + "','" + strPrintdt + "','" + strRefPrint + "');", CommValue.AUTH_VALUE_TRUE);

                ResetInputControls();

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }


        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING))
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP))
                    {
                        ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
            }
        }

        protected void ResetSearchControls()
        {
            
            txtAmount.Text = "";
            txtPayDay.Text = "";
            txtRealAmount.Text = "";
            
            txtExRate.Text = "";
        }

        protected void ResetInputControls()
        {
           
            txtAmount.Text = "";
            txtPayDay.Text = "";
            txtRealAmount.Text = "";            
            txtExRate.Text = "";
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

        protected void MakeFeeTypeDdl( DropDownList ddllist)
        {
            ddllist.Items.Clear();
            ddllist.Items.Add(new ListItem("Fee Type", ""));
            ddllist.Items.Add(new ListItem("Renovation Fee", "0015"));
            ddllist.Items.Add(new ListItem("ParkingCard Fee", "0007"));
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

       
      

        protected void lvReceivable_ItemCreated(object sender, ListViewItemEventArgs e)
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


        protected void lvPaymentDetails_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            
        }

        protected void lvPaymentDetails_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
           

        }

        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {
            var strPaydt = txtSearchDt.Text.Replace("-", "");
            var strPrintdt = txtPayDay.Text.Replace("-", "");
            var strInvAmt = txtRealAmount.Text;
            var strRefPrint = hfRefPrint.Value;
            double dbInvAmt = CommValue.NUMBER_VALUE_0_0;


            if (!string.IsNullOrEmpty(strInvAmt))
            {
                dbInvAmt = double.Parse(strInvAmt);
            }
            var insCompCd = Session["CompCd"].ToString();
            var insMemNo = Session["MemNo"].ToString();
            var insMemIP = Session["UserIP"].ToString();

            var dsReturn = ReceiptMngBlo.SelectIssAmtPrintoutHoadonMerge(strPaydt);
            if (dsReturn.Tables[2].Rows.Count > 0)
            {
                return;
            }
            else
            {

                //KN_USP_UPDATE_HOADON_PRK_MERGE_U00
                MngPaymentBlo.UpdatingHoadonPrintMerge(strRefPrint, insCompCd, insMemNo, insMemIP);
            }
            
        }
    }
}
