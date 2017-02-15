using System;
using System.Data;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;

namespace KN.Web.Config.Exchange
{
    public partial class ExchangeModify : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        CheckExchage();

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
            ltRealRate.Text = TextNm["ACTUALFOREX"];
            ltTopDate.Text = TextNm["BASEDATE"];
            ltTopBaseRate.Text = TextNm["BASERATE"];
            ltTopBuying.Text = TextNm["BUYING"];
            ltTopSelling.Text = TextNm["SELLING"];

            ltAppliedRate.Text = TextNm["APPLIEDFOREX"];
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

            txtBaseRate.Attributes["onkeypress"] = "javascript:return fnCheckDate('" + AlertNm["ALERT_SELECT_DATE"] + "');";

            lnkbtnSave.Text = TextNm["MODIFY"];
            lnkbtnSave.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["CONF_MODIFY_ITEM"] + "', '" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnSave.Visible = Master.isModDelAuthOk;
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POSTMODIFY"] + "');";
        }

        protected void CheckExchage()
        {
            // KN_USP_MNG_SELECT_CURRENCYINFO_S00
            DataTable dtReturn = ExchangeMngBlo.WatchCurrencyInfo(txtHfExchangeDate.Text, txtHfRentCd.Text);

            double dblVong = 0.0d;

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    ltRealDate.Text = TextLib.MakeDateEightDigit(dtReturn.Rows[0]["CurrencyDt"].ToString());

                    foreach (DataRow dr in dtReturn.Select())
                    {
                        switch (dr["Code"].ToString())
                        {
                            case "VND": dblVong = double.Parse(dr["Currency"].ToString());
                                break;
                        }
                    }

                    if (dblVong > 0)
                    {
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo((dblVong).ToString("###,##0"));
                        ltRealBuying.Text = TextLib.MakeVietIntNo((dblVong * CommValue.RATE_VALUE_BUYING).ToString("###,##0"));
                        ltRealSelling.Text = TextLib.MakeVietIntNo((dblVong * CommValue.RATE_VALUE_SELLING).ToString("###,##0"));
                    }
                }
            }
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
                        txtDate.Text = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2);
                        hfDate.Value = strDate;
                    }
                    else
                    {
                        txtDate.Text = "-";
                        hfDate.Value = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["DongToDollar"].ToString());
                        txtBaseRate.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        txtBaseRate.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DifferAmt"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["DifferAmt"].ToString());
                        txtFluctAmt.Text = TextLib.MakeVietRealNo(dblData.ToString());
                    }
                    else
                    {
                        txtFluctAmt.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DifferRatio"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["DifferRatio"].ToString());
                        txtFluctRatio.Text = TextLib.MakeVietRealNo(dblData.ToString());
                    }
                    else
                    {
                        txtFluctRatio.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CashBuy"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["CashBuy"].ToString());
                        txtBuying.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        txtBuying.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CashSell"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["CashSell"].ToString());
                        txtSelling.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        txtSelling.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TransferSend"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["TransferSend"].ToString());
                        txtSending.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        txtSending.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["TransferRecieve"].ToString()))
                    {
                        double dblData = Double.Parse(dtReturn.Rows[0]["TransferRecieve"].ToString());
                        txtReceiving.Text = TextLib.MakeVietRealNo(dblData.ToString("###,##0.##"));
                    }
                    else
                    {
                        txtReceiving.Text = "-";
                    }

                    txtHfExchangeSeq.Text = dtReturn.Rows[0]["ExchangeSeq"].ToString();
                }
                else
                {
                    Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
                }
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
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

        protected void lnkbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strBaseRate = string.Empty;
                string strFluctAmt = string.Empty;
                string strFluctRatio = string.Empty;
                string strBuying = string.Empty;
                string strSelling = string.Empty;
                string strSending = string.Empty;
                string strReceiving = string.Empty;

                if (!string.IsNullOrEmpty(txtBaseRate.Text) && !txtBaseRate.Text.Equals("-"))
                {
                    strBaseRate = txtBaseRate.Text.Replace(",", "").Replace(".", "");
                }
                else
                {
                    strBaseRate = "0";
                }

                if (!string.IsNullOrEmpty(txtFluctAmt.Text) && !txtFluctAmt.Text.Equals("-"))
                {
                    strFluctAmt = TextLib.MakeOriginIntNo(txtFluctAmt.Text).Replace(",", "");
                }
                else
                {
                    strFluctAmt = "0";
                }

                if (!string.IsNullOrEmpty(txtFluctRatio.Text) && !txtFluctRatio.Text.Equals("-"))
                {
                    strFluctRatio = TextLib.MakeOriginRealNo(txtFluctRatio.Text).Replace(",", "");
                }
                else
                {
                    strFluctRatio = "0";
                }

                if (!string.IsNullOrEmpty(txtBuying.Text) && !txtBuying.Text.Equals("-"))
                {
                    strBuying = txtBuying.Text.Replace(",", "").Replace(".", "");
                }
                else
                {
                    strBuying = "0"; ;
                }

                if (!string.IsNullOrEmpty(txtSelling.Text) && !txtSelling.Text.Equals("-"))
                {
                    strSelling = txtSelling.Text.Replace(",", "").Replace(".", "");
                }
                else
                {
                    strSelling = "0";
                }

                if (!string.IsNullOrEmpty(txtSending.Text) && !txtSending.Text.Equals("-"))
                {
                    strSending = txtSending.Text.Replace(",", "").Replace(".", "");
                }
                else
                {
                    strSending = "0";
                }

                if (!string.IsNullOrEmpty(txtReceiving.Text) && !txtReceiving.Text.Equals("-"))
                {
                    strReceiving = txtReceiving.Text.Replace(",", "").Replace(".", "");
                }
                else
                {
                    strReceiving = "0";
                }

                // KN_USP_MNG_UPDATE_EXCHANGERATEINFO_M00
                ExchangeMngBlo.ModifyExchangeRateInfo(Int32.Parse(txtHfExchangeSeq.Text), txtHfRentCd.Text, hfDate.Value, double.Parse(strBaseRate), double.Parse(strFluctAmt), 
                                                      double.Parse(strFluctRatio), double.Parse(strBuying.Replace(",", "")), double.Parse(strSelling.Replace(",", "")),
                                                      double.Parse(strSending.Replace(",", "")), double.Parse(strReceiving.Replace(",", "")), Session["CompCd"].ToString(),
                                                      Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void txtBaseRate_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfDate.Value) && !string.IsNullOrEmpty(txtBaseRate.Text))
            {
                // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S04
                DataTable dtPrevReturn = ExchangeMngBlo.WatchExchangeRatePrevInfo(hfDate.Value, txtHfRentCd.Text);

                if (dtPrevReturn != null)
                {
                    if (dtPrevReturn.Rows.Count > 0)
                    {
                        double dblPrevData = Double.Parse(dtPrevReturn.Rows[0]["DongToDollar"].ToString());
                        double dblNowData = Double.Parse(txtBaseRate.Text.Replace(".", "").Replace(",", ""));
                        double dblGap = 0d;

                        dblGap = dblNowData - dblPrevData;

                        if (dblGap != 0d)
                        {
                            txtFluctAmt.Text = TextLib.MakeVietRealNo((dblGap).ToString("###,##0.##"));
                            txtFluctRatio.Text = TextLib.MakeVietRealNo((dblGap / dblPrevData * 100).ToString("###,##0.##"));
                        }
                        else
                        {
                            txtFluctAmt.Text = "0";
                            txtFluctRatio.Text = "0";
                        }

                        if (dblNowData != 0d)
                        {
                            txtBuying.Text = (dblNowData * CommValue.RATE_VALUE_BUYING).ToString("###,##0");
                            txtSelling.Text = (dblNowData * CommValue.RATE_VALUE_SELLING).ToString("###,##0");
                            txtSending.Text = (dblNowData * CommValue.RATE_VALUE_SELLING).ToString("###,##0");
                            txtReceiving.Text = (dblNowData * CommValue.RATE_VALUE_SELLING).ToString("###,##0");
                        }
                        else
                        {
                            txtBuying.Text = "0";
                            txtSelling.Text = "0";
                            txtSending.Text = "0";
                            txtReceiving.Text = "0";
                        }
                    }
                }
            }
        }
    }
}