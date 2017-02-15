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
    public partial class ParkingDebitWrite : BasePage
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
        }
        protected void InitControls()
        {

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            // 섹션코드 조회
           // LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            // 차종 조회
            CommCdDdlUtil.MakeEtcSubCdDdlTitle(ddlCarTy, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_CARTY);           

            txtQuantityReg.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            txtUnitPriceReg.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            lnkbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            txtSearchDt.Text = DateTime.Now.ToString("yyyy") + "-" + DateTime.Now.ToString("MM");
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S06
            var currentDt = txtSearchDt.Text.Replace("-","");
            var dsReturn = ParkingMngBlo.GetParkingDebitList("", currentDt, hfUserSeq.Value);

            lvActMonthParkingCardList.DataSource = dsReturn;
            lvActMonthParkingCardList.DataBind();
            ResetSearchControls();
        }

        private void LoadUserInfo()
        {
            var dsReturn = ParkingMngBlo.GetParkingUserListInfo("0000", "", "", hfUserSeq.Value);
            if (dsReturn == null || dsReturn.Rows.Count <= 0) return;
            ltComPanyName.Text = dsReturn.Rows[0]["Tenant_Name"].ToString();
            ltRoomNo.Text = dsReturn.Rows[0]["RoomNo"].ToString();
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

        protected void lvActMonthParkingCardList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            if (!string.IsNullOrEmpty(drView["SeqDebit"].ToString()))
            {
                var ltSeq = (Literal)iTem.FindControl("ltSeq");
                ltSeq.Text = drView["SeqDebit"].ToString();
                var txtHfSeq = (TextBox)iTem.FindControl("txtHfSeq");
                txtHfSeq.Text = TextLib.StringDecoder(drView["Seq"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Description"].ToString()))
            {
                var txtDes = (TextBox)iTem.FindControl("txtDes");
                txtDes.Text = TextLib.StringDecoder(drView["Description"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Unit"].ToString()))
            {
                var ltUnit = (Literal)iTem.FindControl("ltUnit");
                ltUnit.Text = TextLib.StringDecoder(drView["Unit"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Amount"].ToString()))
            {
                var txtAmount = (TextBox)iTem.FindControl("txtAmount");
                txtAmount.Text = TextLib.StringDecoder(drView["Amount"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Quantity"].ToString()))
            {
                var txtQuantity = (TextBox)iTem.FindControl("txtQuantity");
                txtQuantity.Text = TextLib.StringDecoder(drView["Quantity"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["UnitPrice"].ToString()))
            {
                var txtUnitPrice = (TextBox)iTem.FindControl("txtUnitPrice");
                txtUnitPrice.Text = TextLib.StringDecoder(drView["UnitPrice"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Remark"].ToString()))
            {
                var txtRemark = (TextBox)iTem.FindControl("txtRemark");
                txtRemark.Text = TextLib.StringDecoder(drView["Remark"].ToString());
            }

            var imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
            imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

            var imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
            imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_SELECTED_ITEM"] + "');";
            if (string.IsNullOrEmpty(drView["InvoiceNo"].ToString())) return;
            imgbtnDelete.Visible = false;
            imgbtnModify.Visible = false;
        }

        protected void lvActMonthParkingCardList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                var carTy = ddlCarTy.SelectedValue;
                var des = ((TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtDes")).Text;
                var unit = "";
                var quantity = int.Parse(((TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtQuantity")).Text);
                var unitPrice = double.Parse(((TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtUnitPrice")).Text);
                var amount = double.Parse(((TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtAmount")).Text);
                var remark = ((TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtRemark")).Text;
                var userSeq = hfUserSeq.Value;
                var seq = Int32.Parse(((TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfSeq")).Text);
                var period = txtSearchDt.Text.Replace("-", "");
                var objReturn = ParkingMngBlo.InsertParkingDebit(userSeq, seq, period, unit, quantity, unitPrice, amount, des, carTy, remark, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                if ((bool)objReturn[0])
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:SaveSuccess()", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Check Infomation')", CommValue.AUTH_VALUE_TRUE);
                }
                LoadData();
                ResetInputControls();

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvActMonthParkingCardList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var txtSeq = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfSeq");

                ParkingMngBlo.DeleteDebitParkingInfo(hfUserSeq.Value, Int32.Parse(txtSeq.Text));                    
                var sbList = new StringBuilder();
                sbList.Append("alert('" + AlertNm["INFO_DELETE_ISSUE"] + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingDelete", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }


        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                var carTy = ddlCarTy.SelectedValue;
                var des = txtDesReg.Text;
                var unit = txtUnitReg.Text;
                var quantity = int.Parse(txtQuantityReg.Text);
                var unitPrice = double.Parse(txtUnitPriceReg.Text);
                var amount = double.Parse(txtAmountReg.Text);
                var remark = txtRemarkReg.Text;
                var userSeq = hfUserSeq.Value;
                var seq = Int32.Parse(hfSeq.Value);
                var period = txtSearchDt.Text.Replace("-", "");
                var objReturn = ParkingMngBlo.InsertParkingDebit(userSeq, seq, period, unit, quantity, unitPrice, amount, des, carTy, remark, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                if ((bool)objReturn[0])
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:SaveSuccess()", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Check Infomation')", CommValue.AUTH_VALUE_TRUE);
                }
                LoadData();
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
            //txtInsRoomNo.Text = string.Empty;
            //txtInsCardNo.Text = string.Empty;
            //txtInsCarNo.Text = string.Empty;
        }

        protected void ResetInputControls()
        {
            ddlCarTy.SelectedValue = CommValue.CODE_VALUE_EMPTY;
            txtAmountReg.Text = "";
            txtDesReg.Text = "";
            txtRemarkReg.Text = "";
            txtUnitPriceReg.Text = "";
            txtQuantityReg.Text = "";
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