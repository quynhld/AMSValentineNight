using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;

namespace KN.Web.Config.Log
{
    public partial class AccountlogList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    CheckParams();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 매개변수 처리 메소드
        /// </summary>
        private void CheckParams()
        {
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
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataSet dsReturn = new DataSet();

            // KN_USP_COMM_SELECT_LOG_S03
            dsReturn = LogInfoBlo.SpreadAccountLogInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                lvLogList.DataSource = dsReturn.Tables[1];
                lvLogList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// 레이아웃 처리 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLogList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvLogList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvLogList.FindControl("ltDebitNm")).Text = TextNm["DEBITCREDIT"];
            ((Literal)lvLogList.FindControl("ltRentNm")).Text = TextNm["RENT"];
            ((Literal)lvLogList.FindControl("ltDirectNm")).Text = TextNm["DIRECT"];
            ((Literal)lvLogList.FindControl("ltItemNm")).Text = TextNm["ITEM"];
            ((Literal)lvLogList.FindControl("ltAmount")).Text = TextNm["AMT"];
            ((Literal)lvLogList.FindControl("ltPaymentNm")).Text = TextNm["PAYMETHOD"];
            ((Literal)lvLogList.FindControl("ltInsDt")).Text = TextNm["INSDT"];
            ((Literal)lvLogList.FindControl("ltMngFeeNET")).Text = TextNm["AMT"] + " (" + TextNm["NET"] + ")";
            ((Literal)lvLogList.FindControl("ltMngFeeVAT")).Text = TextNm["VAT"];
        }

        /// <summary>
        /// ListView 데이터 바인딩 메소드
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLogList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["RealSeq"].ToString()))
                {
                    Literal ltInsSeq = (Literal)iTem.FindControl("ltInsSeq");
                    ltInsSeq.Text = drView["RealSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DebitNm"].ToString()))
                {
                    Literal ltInsDebitNm = (Literal)iTem.FindControl("ltInsDebitNm");
                    ltInsDebitNm.Text = drView["DebitNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentNm"].ToString()))
                {
                    Literal ltInsRentNm = (Literal)iTem.FindControl("ltInsRentNm");
                    ltInsRentNm.Text = drView["RentNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["DirectNm"].ToString()))
                {
                    Literal ltInsDirectNm = (Literal)iTem.FindControl("ltInsDirectNm");
                    ltInsDirectNm.Text = drView["DirectNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ItemNm"].ToString()))
                {
                    Literal ltInsItemNm = (Literal)iTem.FindControl("ltInsItemNm");
                    ltInsItemNm.Text = drView["ItemNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["Amt"].ToString()))
                {
                    Literal ltInsAmount = (Literal)iTem.FindControl("ltInsAmount");
                    ltInsAmount.Text = double.Parse(drView["Amt"].ToString()).ToString("###,##0");
                }

                if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
                {
                    Literal ltInsPaymentCd = (Literal)iTem.FindControl("ltInsPaymentCd");
                    ltInsPaymentCd.Text = drView["PaymentNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["InsDate"].ToString()))
                {
                    Literal ltInsDtList = (Literal)iTem.FindControl("ltInsDtList");

                    string strInsDt = drView["InsDt"].ToString();

                    StringBuilder sbModDt = new StringBuilder();

                    if (Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
                    {
                        sbModDt.Append(strInsDt.Substring(0, 4));
                        sbModDt.Append(".");
                        sbModDt.Append(strInsDt.Substring(4, 2));
                        sbModDt.Append(".");
                        sbModDt.Append(strInsDt.Substring(6, 2));
                    }
                    else
                    {
                        sbModDt.Append(strInsDt.Substring(6, 2));
                        sbModDt.Append("/");
                        sbModDt.Append(strInsDt.Substring(4, 2));
                        sbModDt.Append("/");
                        sbModDt.Append(strInsDt.Substring(0, 4));
                    }

                    ((Literal)iTem.FindControl("ltInsDtList")).Text = sbModDt.ToString();

                }

                if (!string.IsNullOrEmpty(drView["VatRatio"].ToString()))
                {
                    string strAmt = string.Empty;
                    string strVatRatio = string.Empty;

                    Literal ltInsMngFeeVAT = (Literal)iTem.FindControl("ltInsMngFeeVAT");

                    TextBox txtHfFeeVat = (TextBox)iTem.FindControl("txtHfFeeVat");

                    if (!string.IsNullOrEmpty(drView["Amt"].ToString()))
                    {
                        strAmt = drView["Amt"].ToString();
                    }
                    else
                    {
                        strAmt = CommValue.NUMBER_VALUE_ZERO;
                    }

                    txtHfFeeVat.Text = strAmt;

                    if (!string.IsNullOrEmpty(drView["VATRatio"].ToString()))
                    {
                        strVatRatio = drView["VATRatio"].ToString();
                    }
                    else
                    {
                        strVatRatio = CommValue.NUMBER_VALUE_ZERO;
                    }

                    ltInsMngFeeVAT.Text = TextLib.MakeVietIntNo(((double.Parse(strAmt)) * (double.Parse(strVatRatio)) / (100 + double.Parse(strVatRatio))).ToString("###,##0"));

                    string strFeeVAT = string.Empty;

                    if (!string.IsNullOrEmpty(ltInsMngFeeVAT.Text))
                    {
                        strFeeVAT = ltInsMngFeeVAT.Text.Replace(".", "");
                    }
                    else
                    {
                        strFeeVAT = CommValue.NUMBER_VALUE_ZERO;
                    }

                    Literal ltInsMngFeeNET = (Literal)iTem.FindControl("ltInsMngFeeNET");
                    ltInsMngFeeNET.Text = TextLib.MakeVietIntNo((double.Parse(strAmt) - double.Parse(strFeeVAT.Replace(".", "").Replace(",", ""))).ToString("###,##0"));

                }
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvLogList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의

                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltDebitNm")).Text = TextNm["DEBITCREDIT"];
                    ((Literal)e.Item.FindControl("ltRentNm")).Text = TextNm["RENT"];
                    ((Literal)e.Item.FindControl("ltDirectNm")).Text = TextNm["DIRECT"];
                    ((Literal)e.Item.FindControl("ltItemNm")).Text = TextNm["ITEM"];
                    ((Literal)e.Item.FindControl("ltAmount")).Text = TextNm["AMT"];
                    ((Literal)e.Item.FindControl("ltPaymentNm")).Text = TextNm["PAYMETHOD"];
                    ((Literal)e.Item.FindControl("ltInsDt")).Text = TextNm["INSDT"];
                    ((Literal)e.Item.FindControl("ltMngFeeNET")).Text = TextNm["AMT"] + " (" + TextNm["NET"] + ")";
                    ((Literal)e.Item.FindControl("ltMngFeeVAT")).Text = TextNm["VAT"];


                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 페이지 이동 이벤트 처리
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
    }
}