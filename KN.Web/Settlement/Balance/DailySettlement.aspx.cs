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

using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class DailySettlement : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = CommValue.NUMBER_VALUE_0;

        string strPayYn = string.Empty;

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
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            lnkbtnAccounts.Text = TextNm["ACCOUNTS"];
            lnkbtnAccounts.Visible = Master.isModDelAuthOk;
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_MNG_SELECT_ACCOUNTSINFO_S03
            dsReturn = BalanceMngBlo.SpreadAccountsList(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), txtHfRentCd.Text);

            if (dsReturn != null)
            {
                lvAccountsList.DataSource = dsReturn.Tables[1];
                lvAccountsList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString()),
                                  TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvAccountsList_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltFloorRoom = (Literal)lvAccountsList.FindControl("ltFloorRoom");
            ltFloorRoom.Text = TextNm["FLOOR"] + " / " + TextNm["ROOMNO"];
            Literal ltName = (Literal)lvAccountsList.FindControl("ltName");
            ltName.Text = TextNm["NAME"];
            Literal ltItem = (Literal)lvAccountsList.FindControl("ltItem");
            ltItem.Text = TextNm["PAYMENTKIND"];
            Literal ltPayAmt = (Literal)lvAccountsList.FindControl("ltPayAmt");
            ltPayAmt.Text = TextNm["PAYMENT"];
            Literal ltPayDt = (Literal)lvAccountsList.FindControl("ltPayDt");
            ltPayDt.Text = TextNm["REGISTDATE"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvAccountsList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    Literal ltFloorRoom = (Literal)e.Item.FindControl("ltFloorRoom");
                    ltFloorRoom.Text = TextNm["FLOOR"] + " / " + TextNm["ROOMNO"];
                    Literal ltName = (Literal)e.Item.FindControl("ltName");
                    ltName.Text = TextNm["NAME"];
                    Literal ltItem = (Literal)e.Item.FindControl("ltItem");
                    ltItem.Text = TextNm["PAYMENTKIND"];
                    Literal ltPayAmt = (Literal)e.Item.FindControl("ltPayAmt");
                    ltPayAmt.Text = TextNm["PAYMENT"];
                    Literal ltPayDt = (Literal)e.Item.FindControl("ltPayDt");
                    ltPayDt.Text = TextNm["REGISTDATE"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvAccountsList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                    {
                        Literal ltFloorRoomList = (Literal)iTem.FindControl("ltFloorRoomList");
                        ltFloorRoomList.Text = TextLib.StringDecoder(drView["FloorNo"].ToString()) + " / " + TextLib.StringDecoder(drView["RoomNo"].ToString());

                        TextBox txtHfRentCd1 = (TextBox)iTem.FindControl("txtHfRentCd1");
                        txtHfRentCd1.Text = drView["RentCd"].ToString();
                    }
                }

                if (!string.IsNullOrEmpty(drView["UserSeq"].ToString()))
                {
                    TextBox txtHfUserSeq = (TextBox)iTem.FindControl("txtHfUserSeq");
                    txtHfUserSeq.Text = drView["UserSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TenantNm"].ToString()))
                {
                    Literal ltNameList = (Literal)iTem.FindControl("ltNameList");
                    ltNameList.Text = TextLib.StringDecoder(drView["TenantNm"].ToString());
                }

                TextBox txtHfRentalYear = (TextBox)iTem.FindControl("txtHfRentalYear");
                txtHfRentalYear.Text = drView["RentalYear"].ToString();

                TextBox txtHfRentalMM = (TextBox)iTem.FindControl("txtHfRentalMM");
                txtHfRentalMM.Text = drView["RentalMM"].ToString();

                if (!string.IsNullOrEmpty(drView["ItemCd"].ToString()))
                {
                    Literal ltItemList = (Literal)iTem.FindControl("ltItemList");
                    TextBox txtHfItemCd = (TextBox)iTem.FindControl("txtHfItemCd");
                    txtHfItemCd.Text = drView["ItemCd"].ToString();

                    DataTable dtReturn = new DataTable();

                    dtReturn = CommCdInfo.SelectSubCdWithTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_ITEM);

                    if (dtReturn.Rows.Count > 0)
                    {
                        if (Session["LangCd"].Equals(CommValue.LANG_VALUE_VIETNAMESE))
                        {
                            ltItemList.Text = txtHfRentalMM.Text + "/" + txtHfRentalYear.Text + " " + dtReturn.Rows[Int32.Parse(drView["ItemCd"].ToString())]["CodeNm"].ToString();
                        }
                        else
                        {
                            ltItemList.Text = txtHfRentalYear.Text + "/" + txtHfRentalMM.Text + " " + dtReturn.Rows[Int32.Parse(drView["ItemCd"].ToString())]["CodeNm"].ToString();
                        }
                    }

                    TextBox txtHfVatRatio = (TextBox)iTem.FindControl("txtHfVatRatio");
                    txtHfVatRatio.Text = drView["VatRatio"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PayAmt"].ToString()))
                {
                    Literal ltPayAmtList = (Literal)iTem.FindControl("ltPayAmtList");
                    ltPayAmtList.Text = TextLib.MakeVietIntNo(double.Parse(drView["PayAmt"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["PayDt"].ToString()))
                {
                    string strPayDt = drView["PayDt"].ToString();

                    Literal ltPayDtList = (Literal)iTem.FindControl("ltPayDtList");
                    ltPayDtList.Text = strPayDt.Substring(0, 4) + "-" + strPayDt.Substring(4, 2) + "-" + strPayDt.Substring(6, 2);
                }

                if (!string.IsNullOrEmpty(drView["DongToDollar"].ToString()))
                {
                    HiddenField hfDongToDollarList = (HiddenField)iTem.FindControl("hfDongToDollarList");
                    hfDongToDollarList.Value = drView["DongToDollar"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["PaymentCd"].ToString()))
                {
                    HiddenField hfPaymentCdList = (HiddenField)iTem.FindControl("hfPaymentCdList");
                    hfPaymentCdList.Value = drView["PaymentCd"].ToString();
                }
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

        protected void lnkbtnAccounts_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                #region 내부 로직 변경에 따른 주석 처리

                //for (int intTmpI = 0; intTmpI < lvAccountsList.Items.Count; intTmpI++)
                //{
                //    string strDebitCreditCd = CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT;
                //    string strDirectCd = CommValue.DIRECT_TYPE_VALUE_DIRECT;
                //    string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //    Literal ltItemList = (Literal)lvAccountsList.Items[intTmpI].FindControl("ltItemList");
                //    TextBox txtHfItemCd = (TextBox)lvAccountsList.Items[intTmpI].FindControl("txtHfItemCd");
                //    HiddenField hfDongToDollarList = (HiddenField)lvAccountsList.Items[intTmpI].FindControl("hfDongToDollarList");
                //    Literal ltPayAmtList = (Literal)lvAccountsList.Items[intTmpI].FindControl("ltPayAmtList");
                //    HiddenField hfPaymentCdList = (HiddenField)lvAccountsList.Items[intTmpI].FindControl("hfPaymentCdList");
                //    Literal ltPayDtList = (Literal)lvAccountsList.Items[intTmpI].FindControl("ltPayDtList");
                //    TextBox txtHfVatRatio = (TextBox)lvAccountsList.Items[intTmpI].FindControl("txtHfVatRatio");
                //    TextBox txtHfRentCd1 = (TextBox)lvAccountsList.Items[intTmpI].FindControl("txtHfRentCd1");
                //    TextBox txtHfUserSeq = (TextBox)lvAccountsList.Items[intTmpI].FindControl("txtHfUserSeq");
                //    TextBox txtHfRentalYear = (TextBox)lvAccountsList.Items[intTmpI].FindControl("txtHfRentalYear");
                //    TextBox txtHfRentalMM = (TextBox)lvAccountsList.Items[intTmpI].FindControl("txtHfRentalMM");
                //    string strPayDt = ltPayDtList.Text.Replace("-", "");

                //    double dblItemTotEmAmt = 0d;
                //    double dblItemTotViAmt = 0d;
                //    double dblVatRatio = 0d;
                //    double dblUniPrime = 0d;

                //    if (!string.IsNullOrEmpty(ltPayAmtList.Text) && !string.IsNullOrEmpty(hfDongToDollarList.Value))
                //    {
                //        dblItemTotViAmt = double.Parse(ltPayAmtList.Text.Replace(".","").Replace(",",""));

                //        if (double.Parse(hfDongToDollarList.Value) > 0d)
                //        {
                //            dblItemTotEmAmt = dblItemTotViAmt / double.Parse(hfDongToDollarList.Value);
                //        }
                //    }

                //    if (!string.IsNullOrEmpty(txtHfVatRatio.Text))
                //    {
                //        dblVatRatio = double.Parse(txtHfVatRatio.Text);
                //        dblUniPrime = dblItemTotViAmt * (100 - dblVatRatio) / 100;
                //    }

                //    DataTable dtAccnt = MngAccountsBlo.RegistryAccountsInfo(strDebitCreditCd, strPayDt, CommValue.NUMBER_VALUE_0, txtHfRentCd1.Text, strDirectCd, txtHfItemCd.Text,
                //        CommValue.NUMBER_VALUE_0, CommValue.USERTYCD_VALUE_PERSON_CLIENT, txtHfUserSeq.Text, string.Empty, string.Empty, string.Empty,
                //        double.Parse(hfDongToDollarList.Value), dblItemTotEmAmt, double.Parse(ltPayAmtList.Text.Replace(",", "").Replace(".", "")), hfPaymentCdList.Value, dblVatRatio,
                //        string.Empty, string.Empty, Session["MemNo"].ToString(), strInsMemIP);

                //    if (dtAccnt != null)
                //    {
                //        if (dtAccnt.Rows.Count > 0)
                //        {
                //            int intPaymentSeq = Int32.Parse(dtAccnt.Rows[0]["PaymentSeq"].ToString());
                //            int intItemSeq = Int32.Parse(dtAccnt.Rows[0]["ItemSeq"].ToString());

                //            MngAccountsBlo.RegistryAccountsDetInfo(strDebitCreditCd, strPayDt, intPaymentSeq, CommValue.NUMBER_VALUE_0, txtHfRentCd1.Text, strDirectCd, txtHfItemCd.Text,
                //                intItemSeq, CommValue.NUMBER_VALUE_0, string.Empty, string.Empty, string.Empty, string.Empty, CommValue.NUMBER_VALUE_1, CommValue.SCALE_TYPE_VALUE_MONTH,
                //                dblUniPrime, dblUniPrime, double.Parse(ltPayAmtList.Text.Replace(",", "").Replace(".", "")), double.Parse(ltPayAmtList.Text.Replace(",", "").Replace(".", "")), double.Parse(ltPayAmtList.Text.Replace(",", "").Replace(".", "")), txtHfRentalYear.Text, txtHfRentalMM.Text,
                //                dblVatRatio, CommValue.CONCLUSION_TYPE_TEXT_YES, Session["MemNo"].ToString(), strInsMemIP);
                //        }
                //    }
                //}

                #endregion

                // KN_USP_AGT_SEND_SETTLEMENT_LIST_M03
                BalanceMngBlo.RegistrySettelmentInfoByManual(txtHfRentCd.Text);

                StringBuilder sbAlert = new StringBuilder();

                sbAlert.Append("alert(");
                sbAlert.Append(AlertNm["INFO_REGISTER_CONT"]);
                sbAlert.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Info", sbAlert.ToString(), CommValue.AUTH_VALUE_TRUE);

                StringBuilder sbView = new StringBuilder();

                sbView.Append(Master.PAGE_LIST);
                sbView.Append("?");
                sbView.Append(Master.PARAM_DATA1);
                sbView.Append("=");
                sbView.Append(txtHfRentCd.Text);

                Response.Redirect(sbView.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}