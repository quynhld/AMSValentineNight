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
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;
using KN.Settlement.Biz;
namespace KN.Web.Management.Manage
{
    public partial class MngPaymentRenovationAPT : BasePage
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
               // LoadUserInfo();
               // LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void CheckParam()
        {
            if (Request.Params[Master.PARAM_DATA1] == null) return;
            if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1])) return;
            //hfUserSeq.Value = Request.Params[Master.PARAM_DATA1];
            hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
        }
        protected void InitControls()
        {

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            // 섹션코드 조회
            // LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            // 차종 조회
            //CommCdDdlUtil.MakeEtcSubCdDdlTitle(ddlCarTy, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_CARTY);
            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlPaymentTy, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_PAYMENT_METHOD);

            txtAmount.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtExRate.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            //txtSearchDt.Text = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.ToString("MM");
            MakeFeeTypeDdl(ddlFeeType);
            MakeFeeTypeDdl(ddlFeeTypeR);
            MakeAccountDdl(ddlTransfer);
           // CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaidCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_PAYMENT);
            lnkbtnRegist.Visible = Master.isWriteAuthOk;
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_MNG_SELECT_RENOVATION_APT_S00
            var currentDt = txtSearchDt.Text.Replace("-", "");
            var eCurrentDt = txtESearchDt.Text.Replace("-", "");
            var dsReturn = MngPaymentBlo.ListRenovationInfoApt(ddlFeeType.SelectedValue, currentDt, txtRoomNo.Text, txtCompanyNm.Text,eCurrentDt);

            lvPaymentList.DataSource = dsReturn;
            lvPaymentList.DataBind();
            ResetInputControls();
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
                // 세션체크
                AuthCheckLib.CheckSession();
                var payCd = ddlPaymentTy.SelectedValue;
                var bankSeq = Int32.Parse(ddlTransfer.SelectedValue == "" ? "0" : ddlTransfer.SelectedValue);
                var roomNo = txtRomNoR.Text;
                var payDate = txtPayDay.Text.Replace("-", "");
                var exRate = txtExRate.Text == "" ? "0.00" : txtExRate.Text;
                var moneyCd = rbMoneyCd.SelectedValue;
                var amount = double.Parse(txtRealAmount.Text == "" ? "0.00" : txtRealAmount.Text.Replace(",",""));
                var memNo = Session["MemNo"].ToString();
                var ip = Session["UserIP"].ToString();
                //KN_USP_MNG_INSERT_RENOVATIONINFO_M00
                var objReturn = MngPaymentBlo.InsertRenovationInfoApt(payCd, bankSeq, roomNo,ddlFeeTypeR.SelectedValue, payDate, exRate,moneyCd, amount, memNo, ip,txtCarcardNo.Text);
                if (objReturn != null && objReturn.Rows[0]["UserSeq"].ToString() != "")
                {
                    var refSeq = objReturn.Rows[0]["RefSeq"].ToString();
                    var sbAlert = new StringBuilder();
                    sbAlert.Append("Payed Money (VND) : " + TextLib.MakeVietIntNo((amount).ToString("###,##0")) + "\\n");
                    var sbResult = new StringBuilder();
                    sbResult.Append("alert('" + sbAlert + "');");
                    sbResult.Append("window.open('/Common/RdPopup/RDPopupReciptRenovationAPT.aspx?Datum0=" + refSeq + "&Datum1=" + 1 + "&Datum2=&Datum3=&Datum4=', 'Renovation', 'status=no, resizable=yes, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ManagememntFee", sbResult.ToString(), CommValue.AUTH_VALUE_TRUE);
                    LoadData();
                    ResetSearchControls();
                    upSearch.Update();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Check Infomation')", CommValue.AUTH_VALUE_TRUE);
                }                

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
            ddlTransfer.Enabled = false;
            txtAmount.Text = "";
            txtPayDay.Text = "";
            txtRealAmount.Text = "";
            txtRomNoR.Text = "";
            txtExRate.Text = "";
        }

        protected void ResetInputControls()
        {
            //ddlPaymentTy.SelectedValue = CommValue.CODE_VALUE_EMPTY;
            ddlTransfer.Enabled = false;
            txtAmount.Text = "";
            txtPayDay.Text = "";
            txtRealAmount.Text = "";
            txtRomNoR.Text = "";
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

        protected void ddlPaymentTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPaymentTy.SelectedValue == CommValue.PAYMENT_TYPE_VALUE_TRANSFER)
                {
                    ddlTransfer.Enabled = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ddlTransfer.SelectedValue = string.Empty;
                    ddlTransfer.Enabled = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        public void MakeAccountDdl(DropDownList ddlParams)
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
            // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
            // Utility Fee : Chestnut 매출
            // 그외 KeangNam 매출
            const string strCompCd = CommValue.MAIN_COMP_CD;
            var dtReturn = AccountMngBlo.SpreadBankAccountInfo(strCompCd);

            ddlParams.Items.Clear();

            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

            foreach (var dr in dtReturn.Select())
            {
                ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
            }
        }

        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            var sbPrintOut = new StringBuilder();

            sbPrintOut.Append("window.open('/Common/RdPopup/RDPopupReciptRenovationAPT.aspx?Datum0=" + txtHfRefSeq.Text + "&Datum1=" + 2 + "&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Renovation", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
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
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var memNo = Session["MemNo"].ToString();
                var ip = Session["UserIP"].ToString();
                var seq = (TextBox)lvPaymentList.Items[e.ItemIndex].FindControl("txthfSeq");
                //KN_USP_MNG_DELETE_RENOVATION_M00
                MngPaymentBlo.DeleteRenovationAptDetails(seq.Text,1,memNo,ip,"","");
                var sbList = new StringBuilder();
                sbList.Append("alert('" + AlertNm["INFO_DELETE_ISSUE"] + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Renovation", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                LoadData();
                upSearch.Update();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }            
        }

        protected void lvPaymentDetails_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                AuthCheckLib.CheckSession();
                var seq = (TextBox)lvPaymentList.Items[e.ItemIndex].FindControl("txthfSeq");
                hfRef_Seq.Value = seq.Text;
                var sbPrintOut = new StringBuilder();
                sbPrintOut.Append("window.open('/Common/Popup/PopupReturnCarCardAndRenovation.aspx?RefSeq=" + seq.Text + "&Datum1=" + 2 + "&Datum2=&Datum3=&Datum4=', 'ParkingFee', 'status=no, resizable=yes, width=550, height=300, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no');");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Renovation", sbPrintOut.ToString(), CommValue.AUTH_VALUE_TRUE);
                LoadData();
                upSearch.Update();
            }
            catch (Exception ex)
            {

                ErrLogger.MakeLogger(ex);
            }

        }


       
    }
}
