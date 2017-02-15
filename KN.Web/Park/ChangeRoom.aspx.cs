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
using KN.Manage.Biz;

namespace KN.Web.Park
{
    public partial class ChangeRoom : BasePage
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
            string strFeeTyTxt = string.Empty;
            string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
            string strNowDay = strNowDt.Substring(6, 2);
            string strEndDt = string.Empty;
           
            DateTime dtEndDate = DateTime.ParseExact(DateTime.Now.ToString("s").Substring(0, 7).ToString() + "-01 00:00:00.000", "yyyy-MM-dd HH:mm:ss.fff", null);

            ltInsRoomNo.Text = TextNm["ROOMNO"];
            ltInsCarNo.Text = TextNm["CARNO"];
            ltInsCardNo.Text = TextNm["CARDNO"];
            

            
            ltTopRoomNo.Text = TextNm["ROOMNO"];
            ltTopName.Text = TextNm["NAME"];
            ltTopCarNo.Text = TextNm["CARNO"];
            ltTopCardNo.Text = TextNm["PARKINGTAGNO"];
            ltTopCarTy.Text = TextNm["CARTY"];
            

            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];

            lnkbtnRegist.Text = TextNm["REGIST"];

            // 섹션코드 조회
            LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

           

            // 섹션코드 조회
            LoadRentDdl(ddlRegRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            var loadRentddl = ddlRegRentCd.SelectedValue;
           
            
            LoadExchageDate();
            
            //imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtTitle.ClientID + "', '" + txtInputRoom.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + "', '" + loadRentddl.ToString() + "');";
            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtTitle.ClientID + "', '" + txtInputRoom.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtTitle.Text + "', '" + loadRentddl + "');";
         
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            string strCardNo = string.Empty;

            //strCardNo = txtInsCardNo.Text;

            //if (!string.IsNullOrEmpty(strCardNo))
            //{
            //    if (strCardNo.Length < 8)
            //    {
            //        strCardNo = txtInsCardNo.Text.PadLeft(8, '0');
            //    }
            //}

            // KN_USP_PRK_SELECT_MONTHPARKINGINFO_S01
            dtReturn = ParkingMngBlo.SpreadParkingUserListInfo(ddlInsRentCd.SelectedValue, txtInsRoomNo.Text, txtInsCardNo.Text, txtInsCarNo.Text, Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                lvActMonthParkingCardList.DataSource = dtReturn;
                lvActMonthParkingCardList.DataBind();
            }

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
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["Seq"].ToString()))
                {                   
                    TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                    TextBox txtHfUserDetSeq = (TextBox)iTem.FindControl("txtHfUserDetSeq");
                    txtHfUserDetSeq.Text = drView["UserDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    TextBox txtRentCd = (TextBox)iTem.FindControl("txtRentCd");
                    txtRentCd.Text = TextLib.StringDecoder(drView["RentCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                    ltRoomNo.Text = TextLib.StringDecoder(drView["RoomNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    Literal ltName = (Literal)iTem.FindControl("ltName");
                    ltName.Text = TextLib.StringDecoder(drView["UserNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ParkingCarNo"].ToString()))
                {
                    TextBox txtCarNo = (TextBox)iTem.FindControl("txtCarNo");
                    txtCarNo.Text = TextLib.StringDecoder(drView["ParkingCarNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ParkingCardNo"].ToString()))
                {
                    TextBox txtCardNo = (TextBox)iTem.FindControl("txtCardNo");
                    txtCardNo.Text = TextLib.StringDecoder(drView["ParkingCardNo"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ParkingTagNo"].ToString()))
                {
                    TextBox txtHfTagNo = (TextBox)iTem.FindControl("txtHfTagNo");
                    txtHfTagNo.Text = TextLib.StringDecoder(drView["ParkingTagNo"].ToString());
                }
                               

                if (!string.IsNullOrEmpty(drView["CarNm"].ToString()))
                {
                    Literal ltCarTyNm = (Literal)iTem.FindControl("ltCarTyNm");
                    ltCarTyNm.Text = TextLib.StringDecoder(drView["CarNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["CarCd"].ToString()))
                {
                    TextBox txtHfCarTyCd = (TextBox)iTem.FindControl("txtHfCarTyCd");
                    txtHfCarTyCd.Text = TextLib.StringDecoder(drView["CarCd"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ParkingYYYYMM"].ToString()))
                {
                    TextBox txtHfParkingYYYYMM = (TextBox)iTem.FindControl("txtHfParkingYYYYMM");
                    txtHfParkingYYYYMM.Text = drView["ParkingYYYYMM"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PayDt"].ToString()))
                {
                    TextBox txtHfPayDt = (TextBox)iTem.FindControl("txtHfPayDt");
                    txtHfPayDt.Text = drView["PayDt"].ToString();
                }

              
            }
        }

        //-------- function get Bank Acc----------------------------
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


        protected void lvActMonthParkingCardList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
           
        }

        protected void lvActMonthParkingCardList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            
        }

        
        protected void lnkbtnRegist_Click(object sender, EventArgs e)
        {
            
        }
        
        /// <summary>
        /// 섹션 변경시 처리
        /// Autopostback의 폐단에 의한 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnRentChange_Click(object sender, ImageClickEventArgs e)
        {           
            
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


        protected void ResetSearchControls()
        {
            txtInsRoomNo.Text = string.Empty;
            txtInsCardNo.Text = string.Empty;
            txtInsCarNo.Text = string.Empty;
        }

        protected void ResetInputControls()
        {
            ddlRegRentCd.SelectedValue = CommValue.CODE_VALUE_EMPTY;
          
            
        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtReturn = new DataTable();

            // 가장 최근의 환율을 조회함.
            dtReturn = ExchangeMngBlo.WatchExchangeRateLastInfo(CommValue.RENTAL_VALUE_PARKING);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0") + "&nbsp;" + TextNm["DONG"].ToString());
                        hfRealBaseRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();
                    }
                    else
                    {
                        ltRealBaseRate.Text = "-";
                    }
                }
                else
                {
                    ltRealBaseRate.Text = "-";
                }
            }
        }

        protected void chkAll_CheckedChanged1(object sender, EventArgs e)
        {
            bool isAllCheck = chkAll.Checked;

            try
            {
                CheckAll(isAllCheck);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            var cb = (CheckBox)sender;
            var item = (ListViewItem)cb.NamingContainer;
            var dataItem = (ListViewDataItem)item;

            bool status = (((CheckBox)lvActMonthParkingCardList.Items[dataItem.DataItemIndex].FindControl("chkboxList")).Checked == true) ? true : false;
            string code = ((Literal)lvActMonthParkingCardList.Items[dataItem.DataItemIndex].FindControl("ltInsRoomNo")).Text;

            for (var i = CommValue.NUMBER_VALUE_0; i < lvActMonthParkingCardList.Items.Count; i++)
            {
                if (status)
                {
                    if (((CheckBox)lvActMonthParkingCardList.Items[i].FindControl("chkboxList")).Checked == false && ((Literal)lvActMonthParkingCardList.Items[i].FindControl("ltInsRoomNo")).Text.Equals(code))
                    {
                        ((CheckBox)lvActMonthParkingCardList.Items[i].FindControl("chkboxList")).Checked = true;
                    }

                }
                else
                {
                    if (((CheckBox)lvActMonthParkingCardList.Items[i].FindControl("chkboxList")).Checked == true && ((Literal)lvActMonthParkingCardList.Items[i].FindControl("ltInsRoomNo")).Text.Equals(code))
                    {
                        ((CheckBox)lvActMonthParkingCardList.Items[i].FindControl("chkboxList")).Checked = false;
                    }
                }

            }

        }

        private void CheckAll(bool isAllCheck)
        {
            for (int intTmpI = 0; intTmpI < lvActMonthParkingCardList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvActMonthParkingCardList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)lvActMonthParkingCardList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                }
            }
        }
        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
            //string conmpareVal1 = lvPrintoutList.Items[e].FindControl("ltInsRoomNo").ToString();

            var cb = (CheckBox)sender;
            var item = (ListViewItem)cb.NamingContainer;
            var dataItem = (ListViewDataItem)item;

            bool status = (((CheckBox)lvActMonthParkingCardList.Items[dataItem.DataItemIndex].FindControl("chkboxList")).Checked == true) ? true : false;
            string code = ((Literal)lvActMonthParkingCardList.Items[dataItem.DataItemIndex].FindControl("ltInsRoomNo")).Text;

            for (var i = CommValue.NUMBER_VALUE_0; i < lvActMonthParkingCardList.Items.Count; i++)
            {
                if (status)
                {
                    if (((CheckBox)lvActMonthParkingCardList.Items[i].FindControl("chkboxList")).Checked == false && ((Literal)lvActMonthParkingCardList.Items[i].FindControl("ltInsRoomNo")).Text.Equals(code))
                    {
                        ((CheckBox)lvActMonthParkingCardList.Items[i].FindControl("chkboxList")).Checked = true;
                    }

                }
                else
                {
                    if (((CheckBox)lvActMonthParkingCardList.Items[i].FindControl("chkboxList")).Checked == true && ((Literal)lvActMonthParkingCardList.Items[i].FindControl("ltInsRoomNo")).Text.Equals(code))
                    {
                        ((CheckBox)lvActMonthParkingCardList.Items[i].FindControl("chkboxList")).Checked = false;
                    }
                }

            }

        }
    }
}