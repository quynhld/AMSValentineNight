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
using KN.Common.Method.Util;

using KN.Config.Biz;
using KN.Parking.Biz;
using KN.Resident.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Park
{
    public partial class ParkingDebitDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (IsPostBack) return;
                InitControls();
                CheckParam();                
                LoadUserInfo();
                LoadData();
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
            hfUserSeq.Value = Request.Params[Master.PARAM_DATA1];
            hfRentCd.Value = Request.Params["RentCd"];
            hfReftSeq.Value = Request.Params["RefSeq"];
            hfSendPeriod.Value = Request.Params["Period"];
        }

        protected void InitControls()
        {
            MakePrintYN();
            hfSendPeriod.Value = Request.Params["Period"].ToString();
            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            // 차종 조회
            
            lnkbtnPrint.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            //txtSearchDt.Text = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.ToString("MM");
            var month = hfSendPeriod.Value;
            txtSearchDt.Text = month.ToString().Substring(0, 4) + "-" + month.ToString().Substring(4, 2);
            string compNo = Session["CompCd"].ToString();
            string insMemNo = Session["MemNo"].ToString();
            string memIP = Session["UserIP"].ToString();
            string refSeq = hfReftSeq.Value.ToString();

        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S06
            var currentDt = txtSearchDt.Text.Replace("-", "");
            hfPeriod.Value = currentDt;

            var dsReturn = ParkingMngBlo.GetParkingDebitListPrint("", currentDt, hfUserSeq.Value, hfReftSeq.Value);

            lvActMonthParkingCardList.DataSource = dsReturn;
            lvActMonthParkingCardList.DataBind();
            //ResetSearchControls();
        }

        private void LoadUserInfo()
        {
            var dsReturn = ParkingMngBlo.GetParkingUserListInfo("0000", "", "", hfUserSeq.Value);
            if (dsReturn == null || dsReturn.Rows.Count <= 0) return;
            ltComPanyName.Text = dsReturn.Rows[0]["Tenant_Name"].ToString();
            ltRoomNo.Text = dsReturn.Rows[0]["RoomNo"].ToString();
        }

        protected void ResetSearchControls()
        {
            //txtInsRoomNo.Text = string.Empty;
            //txtInsCardNo.Text = string.Empty;
            //txtInsCarNo.Text = string.Empty;
        }

        private void MakePrintYN()
        {
            ddlPrintYN.Items.Clear();
            ddlPrintYN.Items.Add(new ListItem("No", "N"));
            ddlPrintYN.Items.Add(new ListItem("Yes", "Y"));           

        }

        protected void ResetInputControls()
        {
          
        }

        protected void lvActMonthParkingCardList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["SeqDebit"].ToString()))
                {
                    var ltSeq = (Literal)iTem.FindControl("ltSeq");
                    ltSeq.Text = drView["SeqDebit"].ToString();
                    var txtHfSeq = (TextBox)iTem.FindControl("txtHfSeq");
                    txtHfSeq.Text = TextLib.StringDecoder(drView["Seq"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Period"].ToString()))
                {
                    var ltDebitDt = (Literal)iTem.FindControl("ltDebitDt");
                    //ltDebitDt.Text = TextLib.StringDecoder(drView["Period"].ToString());
                    ltDebitDt.Text = drView["Period"].ToString().Substring(0, 4) + "/" + drView["Period"].ToString().Substring(4, 2);

                }

                if (!string.IsNullOrEmpty(drView["Description"].ToString()))
                {
                    var ltDes = (Literal)iTem.FindControl("ltDes");
                    ltDes.Text = TextLib.StringDecoder(drView["Description"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Unit"].ToString()))
                {
                    var ltUnit = (Literal)iTem.FindControl("ltUnit");
                    ltUnit.Text = TextLib.StringDecoder(drView["Unit"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["Amount"].ToString()))
                {
                    var ltAmount = (Literal)iTem.FindControl("ltAmount");                    
                    ltAmount.Text = double.Parse(drView["Amount"].ToString()).ToString("###,##0"); ;
                }

                if (!string.IsNullOrEmpty(drView["Quantity"].ToString()))
                {
                    var ltQuantity = (Literal)iTem.FindControl("ltQuantity");
                    ltQuantity.Text = TextLib.StringDecoder(drView["Quantity"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["UnitPrice"].ToString()))
                {
                    var ltUnitPrice = (Literal)iTem.FindControl("ltUnitPrice");
                    ltUnitPrice.Text = double.Parse(drView["UnitPrice"].ToString()).ToString("###,##0"); ;
                   
                }

                if (!string.IsNullOrEmpty(drView["Remark"].ToString()))
                {
                    var ltRemark = (Literal)iTem.FindControl("ltRemark");
                    ltRemark.Text = TextLib.StringDecoder(drView["Remark"].ToString());
                }
              
            }
        }

        protected void lvActMonthParkingCardList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvActMonthParkingCardList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {

        }

        protected void lvActMonthParkingCardList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }

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
       

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                foreach (var t in lvActMonthParkingCardList.Items)
                {
                    var userSeq = hfUserSeq.Value;
                    var seq = ((TextBox)t.FindControl("txtHfSeq")).Text;

                    var dtReturn = ParkingMngBlo.UpdatingParkingIsDebitN(userSeq, "");                    
                }
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList();", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlPrintYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void imgbtnDetailview_Click(object sender, ImageClickEventArgs e)
        {           
            try
            {
                // 세션체크
                    AuthCheckLib.CheckSession();
                     var compNo = Session["CompCd"].ToString();
                     var insMemNo = Session["MemNo"].ToString();
                    var memIP = Session["UserIP"].ToString();
                    var rentCd = hfRentCd.Value;
                    var refSeq = hfReftSeq.Value;                    
                    var dtInsert = ParkingMngBlo.InsertParkingHoadonDebit(compNo, insMemNo, memIP, rentCd, refSeq);
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}