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
    public partial class ParkingDebitList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    InitControls();

                    //LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            ltInsRoomNo.Text = TextNm["ROOMNO"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            ltTopSeq.Text = TextNm["SEQ"];
            ltTopRoomNo.Text = TextNm["ROOMNO"];

            //매매기준율환율정보
            //ltTopBaseRate.Text = TextNm["BASERATE"];

            // 섹션코드 조회
            LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
            
           // LoadExchageDate();
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S05
            var dtReturn = ParkingMngBlo.GetParkingUserListInfo(ddlInsRentCd.SelectedValue, txtInsRoomNo.Text, txtTenanNm.Text,"");

            if (dtReturn == null) return;
            lvActMonthParkingCardList.DataSource = dtReturn;
            lvActMonthParkingCardList.DataBind();

            // ResetSearchControls();
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
            if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
            {
                var ltSeq = (Literal)iTem.FindControl("ltSeq");
                ltSeq.Text = drView["Seq"].ToString();
                var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                txtHfUserSeq.Text = drView["UserSeq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
            {
                var txtRentCd = (TextBox)iTem.FindControl("txtRentCd");
                txtRentCd.Text = TextLib.StringDecoder(drView["RentCd"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                ltRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["Tenant_Name"].ToString()))
            {
                var ltTenantNm = (Literal)iTem.FindControl("ltTenantNm");
                ltTenantNm.Text = TextLib.StringDecoder(drView["Tenant_Name"].ToString());
            }
        }

        protected void lvActMonthParkingCardList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var txtHfUserSeq = (TextBox) lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfUserSeq");

                // KN_USP_PRK_DELETE_USERPARKINGINFO_M00
                ParkingMngBlo.DeleteUserParkingInfo(txtHfUserSeq.Text);

                LoadData();

                StringBuilder sbList = new StringBuilder();
                sbList.Append("alert('" + AlertNm["INFO_DELETE_ISSUE"] + "');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ParkingDelete", sbList.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }


        protected void txtCardFee_TextChanged(object sender, EventArgs e)
        {
            MakeCalculate();
        }

        protected void txtParkingFee_TextChanged(object sender, EventArgs e)
        {
            MakeCalculate();
        }

        protected void chkGateList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int intGateCnt = CommValue.NUMBER_VALUE_0;
            
            //for (int intTmpI = 0; intTmpI < 3; intTmpI++)
            //{
            //    if (chkGateList.Items[intTmpI].Selected)
            //    {
            //        intGateCnt = intGateCnt + Int32.Parse(chkGateList.Items[intTmpI].Value);
            //    }
            //}

            //hfGateList.Value = intGateCnt.ToString().PadLeft(4, '0');
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

        public void MakeMonthDdl(DropDownList ddlParams)
        {
            ddlParams.Items.Clear();

            for (int intTmpI = 0; intTmpI < 12; intTmpI++)
            {
                ddlParams.Items.Add(new ListItem((intTmpI + 1).ToString(), (intTmpI + 1).ToString()));
            }
        }

        public void MakeAccountDdl(DropDownList ddlParams)
        {
            // KN_USP_MNG_SELECT_ACCOUNTINFO_S00
            // FeeTy : 0001 - RentCd : 9000 - Chestnut 매출
            // Utility Fee : Chestnut 매출
            // 그외 KeangNam 매출
            string strCompCd = string.Empty;

            strCompCd = CommValue.SUB_COMP_CD;

            DataTable dtReturn = AccountMngBlo.SpreadAccountInfo(strCompCd);

            ddlParams.Items.Clear();

            ddlParams.Items.Add(new ListItem(TextNm["SELECT"], string.Empty));

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParams.Items.Add(new ListItem(dr["BankNm"].ToString(), dr["BankCd"].ToString()));
            }
        }

        protected void MakeCalculate()
        {
            //if (string.IsNullOrEmpty(txtCardFee.Text))
            //{
            //    txtCardFee.Text = CommValue.NUMBER_VALUE_ZERO;
            //}

            //if (string.IsNullOrEmpty(txtParkingFee.Text))
            //{
            //    txtParkingFee.Text = CommValue.NUMBER_VALUE_ZERO;
            //}

            //txtTotalFee.Text = (Int32.Parse(txtParkingFee.Text.Replace(",", "")) + Int32.Parse(txtCardFee.Text)).ToString("###,##0");
        }

        protected void ResetSearchControls()
        {
            txtInsRoomNo.Text = string.Empty;
            txtInsRoomNo.Text = string.Empty;
            txtTenanNm.Text = string.Empty;
        }

        protected void lvActMonthParkingCardList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            TextBox txtHfUserSeq = (TextBox)lvActMonthParkingCardList.Items[e.ItemIndex].FindControl("txtHfUserSeq");

        }

        protected void lnkbtnWrite_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                Response.Redirect(Master.PAGE_WRITE, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
            
        }

        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var userSeq = txtUserSeq.Value;
                Response.Redirect(Master.PAGE_WRITE+"?"+Master.PARAM_DATA1+"="+userSeq, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlInsRentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}