using System;
using System.Data;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;

namespace KN.Web.Config.Exchange
{
    public partial class ExchangeView : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (CheckParams())
                {
                    InitControls();

                    LoadData();
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

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (Request.Params[Master.PARAM_DATA2] != null)
                {
                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) &&
                        !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                    {
                        txtHfExchangeDate.Text = Request.Params[Master.PARAM_DATA1].ToString();
                        txtHfRentCd.Text = Request.Params[Master.PARAM_DATA2].ToString();
                        isReturnOk = CommValue.AUTH_VALUE_TRUE;
                    }
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltDate.Text = TextNm["BASEDATE"];
            ltBaseRate.Text = TextNm["BASERATE"];
            ltFluctAmt.Text = TextNm["FLUCTAMT"];
            ltFluctRatio.Text = TextNm["FLUCTRATIO"];
            ltCash.Text = TextNm["CASH"];
            ltWireTrans.Text = TextNm["WIRETRANS"];
            ltBuying.Text = TextNm["BUYING"];
            ltSelling.Text = TextNm["SELLING"];
            ltSending.Text = TextNm["SENDING"];
            ltReceiving.Text = TextNm["RECEIVING"];

            lnkbtnList.Text = TextNm["LIST"];

            lnkbtnDel.Text = TextNm["DELETE"];
            lnkbtnDel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
            lnkbtnDel.Visible = Master.isModDelAuthOk;

            lnkbtnMod.Text = TextNm["MODIFY"];
            lnkbtnMod.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";
            lnkbtnMod.Visible = Master.isModDelAuthOk;
        }

        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S00
            DataTable dtReturn = ExchangeMngBlo.WatchExchangeRateInfo(txtHfExchangeDate.Text, txtHfRentCd.Text);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["AppliedDt"].ToString()))
                    {
                        string strDate = dtReturn.Rows[0]["AppliedDt"].ToString();
                        ltInsDate.Text = strDate.Substring(0, 4) + "." + strDate.Substring(4, 2) + "." + strDate.Substring(6, 2);
                    }
                    else
                    {
                        ltInsDate.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["DongToDollar"].ToString());
                        ltInsBaseRate.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        ltInsBaseRate.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DifferAmt"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["DifferAmt"].ToString());
                        ltInsFluctAmt.Text = TextLib.MakeVietRealNo(dblData.ToString());
                    }
                    else
                    {
                        ltInsFluctAmt.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DifferRatio"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["DifferRatio"].ToString());
                        ltInsFluctRatio.Text = TextLib.MakeVietRealNo(dblData.ToString());
                    }
                    else
                    {
                        ltInsFluctRatio.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CashBuy"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["CashBuy"].ToString());
                        ltInsBuying.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        ltInsBuying.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CashSell"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["CashSell"].ToString());
                        ltInsSelling.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        ltInsSelling.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TransferSend"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["TransferSend"].ToString());
                        ltInsSending.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        ltInsSending.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TransferRecieve"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["TransferRecieve"].ToString());
                        ltInsReceiving.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        ltInsReceiving.Text = "-";
                    }

                    txtHfExchangeSeq.Text = dtReturn.Rows[0]["ExchangeSeq"].ToString();
                }
                else
                {
                    Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                }
            }
        }

        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnMod_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfExchangeDate.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_MNG_DELETE_EXCHANGERATEINFO_M00
                ExchangeMngBlo.RemoveExchangeRateInfo(Int32.Parse(txtHfExchangeSeq.Text), txtHfRentCd.Text, Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"]);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}