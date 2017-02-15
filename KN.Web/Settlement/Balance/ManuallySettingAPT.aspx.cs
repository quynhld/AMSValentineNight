using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Manage.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class ManuallySettingAPT : BasePage
    {
        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;
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
                if (!string.IsNullOrEmpty(Request.Params["RentCd"]))
                {
                    txtHfRentCd.Text = Request.Params["RentCd"];
                }
                else
                {
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_APTSHOP;
                }
            }
            else
            {
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APTSHOP;
            }
        }

        protected void InitControls()
        {
            lnkbtnSearch.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + CommValue.TENANTTY_VALUE_CORPORATION + "','" + CommValue.TERM_VALUE_LONGTERM + "','" + CommValue.TERM_VALUE_SHORTTERM + "');";
            
            LoadRentDdl(ddlInsRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            DateTime today = DateTime.Today;            
            DateTime lastday = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(-1);
            

        }

        protected void LoadData()
        {                 
            
            var strYear = txtSearchDt.Text.Replace("-", "").Substring(0, 4);// DateTime.Now.ToString("yyyy");
            var strMonth = txtSearchDt.Text.Replace("-", "").Substring(4, 2);//DateTime.Now.ToString("MM");
            var strDay = DateTime.Now.ToString("dd");
            var roomNo = txtRoomNo.Text;
            var dtReturn = MngPaymentBlo.SelectUploadAPTMFList(ddlInsRentCd.SelectedValue, roomNo, strYear, strMonth);
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
        }

        

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommCdInfo.SelectSubCdWithTitle(strLangCd, strGrpCd, strMainCd);

            ddlParamNm.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                if (!dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APT) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTSHOP) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_SR) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_PARKING) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_OFFICE) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_SHOP)  &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) &&
                    !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP))
                {
                    ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                }
            }
        }

        protected void lvPrintoutList_LayoutCreated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPrintoutList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
                CloseLoading();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvPrintoutList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;

            //Hide       
            
            if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
            {
                var txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                txtHfUserSeq.Text = drView["UserSeq"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
            {
                var txthfRentCd = (TextBox)iTem.FindControl("txthfRentCd");
                txthfRentCd.Text = drView["RentCd"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RentSeq"].ToString()))
            {
                var txthfRentSeq = (TextBox)iTem.FindControl("txthfRentSeq");
                txthfRentSeq.Text = drView["RentSeq"].ToString();
            }
           
            if (!string.IsNullOrEmpty(drView["FeeTy"].ToString()))
            {
                var txthfFeeTypeCode = (TextBox)iTem.FindControl("txthfFeeTypeCode");
                txthfFeeTypeCode.Text = drView["FeeTy"].ToString();
            }            
            

            if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
            {
                var txthfFloor = (TextBox)iTem.FindControl("txthfFloor");
                txthfFloor.Text = drView["FloorNo"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["LeasingArea"].ToString()))
            {
                var txthfLeasingArea = (TextBox)iTem.FindControl("txthfLeasingArea");
                txthfLeasingArea.Text = double.Parse(drView["LeasingArea"].ToString()).ToString(CultureInfo.InvariantCulture);
            }

            //Show

            if (!string.IsNullOrEmpty(drView["FeeTyName"].ToString()))
            {
                var ltFeeType = (Literal)iTem.FindControl("ltFeeType");
                ltFeeType.Text = drView["FeeTyName"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltInsRoomNo = (Literal)iTem.FindControl("ltInsRoomNo");
                ltInsRoomNo.Text = drView["RoomNo"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
            {
                var ltTenantNm = (Literal)iTem.FindControl("ltTenantNm");
                ltTenantNm.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
            }

            if (!string.IsNullOrEmpty(drView["StartDt"].ToString()))
            {
                var ltUsingPeriod = (Literal)iTem.FindControl("ltUsingPeriod");
                ltUsingPeriod.Text = drView["StartDt"].ToString();                
            }

            var ltFeeAmt = (Literal)iTem.FindControl("ltFeeAmt");
            var ltVatAmt = (Literal)iTem.FindControl("ltVATAmt");
            var ltTotal = (Literal)iTem.FindControl("LtTotal");

            if (!string.IsNullOrEmpty(drView["NetAmt"].ToString()))
            {
                ltFeeAmt.Text = double.Parse(drView["NetAmt"].ToString()).ToString("###,##0");
            }

            if (!string.IsNullOrEmpty(drView["VatAmt"].ToString()))
            {
                ltVatAmt.Text = double.Parse(drView["VatAmt"].ToString()).ToString("###,##0");                
            }

            if (!string.IsNullOrEmpty(drView["ViAmount"].ToString()))
            {
                ltTotal.Text = double.Parse(drView["ViAmount"].ToString()).ToString("###,##0");
            }

            var ltUnitPrice = (Literal)iTem.FindControl("ltUnitPrice");
            if (!string.IsNullOrEmpty(drView["UnitPrice"].ToString()))
            {
                ltUnitPrice.Text = double.Parse(drView["UnitPrice"].ToString()).ToString("###,##0");
            }

            if (!string.IsNullOrEmpty(drView["PayDay"].ToString()))
            {
                var ltPayDate = (Literal)iTem.FindControl("ltPayDate");
                ltPayDate.Text = TextLib.MakeDateEightDigit(drView["PayDay"].ToString());
            }           
           
           
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
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

        /// <summary>   
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
        private void CheckAll(bool isAllCheck)
        {
            for (int intTmpI = 0; intTmpI < lvPrintoutList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)lvPrintoutList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                }
            }
        }

        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void lnkCreatedNote_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REDIRECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkPrintList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_REFLECT + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkMakeDebitNote_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var objReturn = new object[2];
                var i = 0;
                foreach (var t in lvPrintoutList.Items)
                {
                    if (!((CheckBox) t.FindControl("chkboxList")).Checked) continue;
                    i++;
                    var rentFeePayAmt = double.Parse(((Literal)t.FindControl("ltFeeAmt")).Text.Replace(",",""));                    
                    var total = double.Parse(((Literal)t.FindControl("LtTotal")).Text.Replace(",", ""));
                    var rentCd = ((TextBox)t.FindControl("txthfRentCd")).Text;
                    var rentSeq = Int32.Parse(((TextBox)t.FindControl("txthfRentSeq")).Text);                    
                    var userSeq = ((TextBox)t.FindControl("txtHfUserSeq")).Text;
                    var roomNo = ((Literal)t.FindControl("ltInsRoomNo")).Text;
                    var floorNo = Int32.Parse(((TextBox)t.FindControl("txthfFloor")).Text);                    
                    var tenantNm = ((Literal)t.FindControl("ltTenantNm")).Text;                    
                    var period = ((Literal)t.FindControl("ltUsingPeriod")).Text.Replace("-", "");                    

                    objReturn = MngPaymentBlo.RegistryAptMFDebitNot(userSeq, rentCd, roomNo, floorNo, tenantNm, total, period);
                }
                CloseLoading();
                if (objReturn != null && i>0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Successful !')", CommValue.AUTH_VALUE_TRUE);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "MakeList", "javascript:alert('Please choose debit!')", CommValue.AUTH_VALUE_TRUE);
                    return;
                }
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnLoadData_Click(object sender, ImageClickEventArgs e)
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
        protected void CloseLoading()
        {
            var sbWarning = new StringBuilder();
            sbWarning.Append("CloseLoading();");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Transfer", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);            
        }
    }
}
