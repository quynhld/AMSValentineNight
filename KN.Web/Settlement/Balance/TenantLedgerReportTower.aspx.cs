using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Resident.Biz;
using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class TenantLedgerReportTower : BasePage
    {
        public int intPageNo = CommValue.NUMBER_VALUE_0;
        public int intRowsCnt = CommValue.NUMBER_VALUE_0;
        public int intTotRowsCnt = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (IsPostBack) return;
                if (CheckParams())
                {
                    InitControls();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected bool CheckParams()
        {
            Master.PARAM_DATA1 = "RentCd";
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
                {
                    hfRentCd.Value = Request.Params[Master.PARAM_DATA1];
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            // DropDownList Setting
            MakePaymentDdl();
            ltDate.Text = TextNm["MONTH"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltPaymentTy.Text = TextNm["ITEM"];
            ltPaymentDt.Text = TextNm["REGISTDATE"];
            ltDescription.Text = TextNm["CONTENTS"];
            ltAmount.Text = TextNm["PAYMENT"];

            txtSearchDtE.Text = DateTime.Now.ToString("s").Substring(0, 10);

            lnkbtnSearch.OnClientClick = "javascript:return fnSelectCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            CommCdDdlUtil.MakeSubCdDdlTitle(ddlPaidCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_PAYMENT);

            lnkbtnIssuing.Visible = Master.isWriteAuthOk;
            LoadRentDdl(ddlRentCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);
        }

        protected void LoadData()
        {
            var sPayDt = txtSearchDt.Text.Replace("-", "");
            // KN_USP_SET_SELECT_APT_HOADONINFO_S01
            var dtReturn = ReceiptMngBlo.SelectPrintListAptHoadon(hfRentCd.Value, txtRoomNo.Text, sPayDt,
                                                                        ddlItemCd.SelectedValue, txtCompanyNm.Text, Session["LANGCD"].ToString());
            lvPrintoutList.DataSource = dtReturn;
            lvPrintoutList.DataBind();
        }


        /// <summary>
        /// 
        /// </summary>
        private void MakePaymentDdl()
        {
            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RECEIT);
            ddlItemCd.Items.Clear();
            ddlItemCd.Items.Add(new ListItem("All Fee", ""));
            foreach (var dr in dtReturn.Select())
            {
                if (hfRentCd.Value.Equals(CommValue.RENTAL_VALUE_APT))
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_RENTALFEE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_UTILFEE))
                    {
                        ddlItemCd.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
                }
                else
                {
                    if (!dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_ELECTRICITYFEE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_WATERATE) &&
                        !dr["CodeCd"].ToString().Equals(CommValue.RECEIT_VALUE_GASRATE))
                    {
                        ddlItemCd.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
                    }
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
            var txtHfRefSeq = (TextBox)iTem.FindControl("txtHfRefSeq");
            if (!string.IsNullOrEmpty(drView["Ref_Seq"].ToString()))
            {
                txtHfRefSeq.Text = drView["Ref_Seq"].ToString();
            }
            intRowsCnt++;
        }
        /// <summary>
        /// 출력버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {      

            var feeTyDt = "";
            var feeTy = "";
            var rentCd = ddlRentCd.SelectedValue;
            try
            {
                var paydt = txtSearchDt.Text.Trim().Replace("-","");
                var paydtE = txtSearchDtE.Text.Trim().Replace("-", "");
                var period = txtPeriod.Text.Trim().Replace("-", "");
                var periodE = txtPeriodE.Text.Trim().Replace("-", "");
                var roomNo = txtRoomNo.Text.Trim();
                var userNm = txtCompanyNm.Text.Trim();
                var paidCd = ddlPaidCd.SelectedValue;
                feeTy = ddlItemCd.SelectedValue;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "fnOccupantList('" + feeTy + "','" + feeTyDt + "','" + period + "','" + periodE + "','" + paydt + "','" + paydtE + "','" + roomNo + "','" + userNm + "','" + paidCd + "','" + rentCd + "');", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
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

        protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlCompNo_SelectedIndexChanged(object sender, EventArgs e)
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

        protected void ddlStartFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddlFeeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
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


        protected void lvPrintoutList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            hfUserSeq.Value = ((TextBox)e.Item.FindControl("txtHfUserSeq")).Text;
            hfPayDt.Value = ((TextBox)e.Item.FindControl("txtInsRegDt")).Text;
        }

        protected void lnkbtnExcelReport_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var feeTyDt = "";
                var feeTy = "";
       
                    var paydt = txtSearchDt.Text.Trim().Replace("-", "");
                    var paydtE = txtSearchDtE.Text.Trim().Replace("-", "");
                    var period = txtPeriod.Text.Trim().Replace("-", "");
                    var periodE = txtPeriodE.Text.Trim().Replace("-", "");
                    var roomNo = txtRoomNo.Text.Trim();
                    var userNm = txtCompanyNm.Text.Trim();

                    if (String.IsNullOrEmpty(ddlItemCd.SelectedValue))
                    {
                        feeTy = "";
                        feeTyDt = "";

                    }
                    else if (ddlItemCd.SelectedValue.Equals("0001"))
                    {
                        feeTy = ddlItemCd.SelectedValue;
                        feeTyDt = "";
                    }
                    else if (ddlItemCd.SelectedValue.Equals("0008"))
                    {
                        feeTy = "0011";
                        feeTyDt = "0001";
                    }
                    else if (ddlItemCd.SelectedValue.Equals("0009"))
                    {
                        feeTy = "0011";
                        feeTyDt = "0002";
                    }

                var dtReturn = ContractMngBlo.SpreadTenantBalanceExcelView(feeTy,feeTyDt,period,periodE,paydt,paydtE,roomNo,userNm,ddlPaidCd.SelectedValue);

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(Master.TITLE_NOW).Replace("+", " ") + ddlItemCd.SelectedItem +".xls");
                Response.ContentType = "application/vnd.ms-excel";
                Response.ContentEncoding = Encoding.Unicode;
                Response.BinaryWrite(Encoding.Unicode.GetPreamble());
                this.EnableViewState = false;

                var stringWriter = new StringWriter();
                var htmlTextWriter = new HtmlTextWriter(stringWriter);

                var strTitle = "<p align='center'><font size='4'><b>" + Master.TITLE_NOW +"-"+ddlItemCd.SelectedItem+ "</b></font></p>";

                htmlTextWriter.Write(strTitle);

                var gv = new GridView();
                gv.Font.Name = "Tahoma";
                gv.DataSource = dtReturn;
                gv.DataBind();
                gv.RenderControl(htmlTextWriter);

                Response.Write(stringWriter.ToString());
                Response.Flush();

                // Prevents any other content from being sent to the browser
                Response.SuppressContent = true;

                // Directs the thread to finish, bypassing additional processing
                HttpContext.Current.ApplicationInstance.CompleteRequest();

                stringWriter.Flush();
                stringWriter.Close();
                htmlTextWriter.Flush();
                htmlTextWriter.Close();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void LoadRentDdl(DropDownList ddlParamNm, string strLangCd, string strGrpCd, string strMainCd)
        {

            var dtReturn = CommCdInfo.SelectSubCdWithNoTitle(strLangCd, strGrpCd, strMainCd);
            ddlParamNm.Items.Clear();
            ddlParamNm.Items.Add(new ListItem("Rental Name", "0000"));

            foreach (var dr in dtReturn.Select().Where(dr => !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTA) &&
                                                                 !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTB) &&
                                                                 !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTASHOP) &&
                                                                 !dr["CodeCd"].ToString().Equals(CommValue.RENTAL_VALUE_APTBSHOP)))
            {
                ddlParamNm.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }

        }
    }
}