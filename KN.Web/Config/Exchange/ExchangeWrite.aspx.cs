using System;
using System.Data;
using System.Web.UI;
using System.Text;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;

namespace KN.Web.Config.Exchange
{
    public partial class ExchangeWrite : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    CheckParams();

                    InitControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void CheckParams()
        {
            string strRentCd = string.Empty;

            if (Request.Params[Master.PARAM_DATA2] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                {
                    txtHfRentCd.Text = Request.Params[Master.PARAM_DATA2].ToString();
                }
                else
                {
                    txtHfExchangeSeq.Text = CommValue.NUMBER_VALUE_ONE;
                    txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
                }
            }
            else
            {
                txtHfExchangeSeq.Text = CommValue.NUMBER_VALUE_ONE;
                txtHfRentCd.Text = CommValue.RENTAL_VALUE_APT;
            }
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

            lnkbtnSave.Text = TextNm["REGISTRATION"];
            lnkbtnSave.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["CONF_REGIST_ITEM"] + "', '" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            lnkbtnSave.Visible = Master.isWriteAuthOk;
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "');";
        }

        protected void LoadData()
        {
            // KN_USP_MNG_SELECT_CURRENCYINFO_S00
            DataTable dtReturn = ExchangeMngBlo.WatchCurrencyInfo(txtHfRentCd.Text);

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
                    strFluctAmt = txtFluctAmt.Text.Replace(",", "");
                }
                else
                {
                    strFluctAmt = "0";
                }

                if (!string.IsNullOrEmpty(txtFluctRatio.Text) && !txtFluctRatio.Text.Equals("-"))
                {
                    strFluctRatio = txtFluctRatio.Text.Replace(",", "");
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

                // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S00
                DataTable dtExistRate = ExchangeMngBlo.WatchExchangeRateInfo(txtDate.Text.Replace("-", ""), txtHfRentCd.Text);

                if (dtExistRate != null)
                {
                    if (dtExistRate.Rows.Count == CommValue.NUMBER_VALUE_0)
                    {
                        // 
                        ExchangeMngBlo.RegistryExchangeRateInfo(txtHfRentCd.Text, txtDate.Text.Replace("-", ""), double.Parse(strBaseRate), double.Parse(strFluctAmt), 
                                                                double.Parse(strFluctRatio),double.Parse(strBuying.Replace(",", "")), double.Parse(strSelling.Replace(",", "")), 
                                                                double.Parse(strSending.Replace(",", "")), double.Parse(strReceiving.Replace(",", "")), Session["CompCd"].ToString(),
                                                                Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());

                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA2 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('" + AlertNm["INFO_CANT_INSERT_DEPTH"] + "');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "WarningExistRate", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                }
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
                txtBaseRate.Text = txtBaseRate.Text.Replace(".", "");

                // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S00
                DataTable dtExistReturn = ExchangeMngBlo.WatchExchangeRateInfo(hfDate.Value, txtHfRentCd.Text);

                if (dtExistReturn != null)
                {
                    if (dtExistReturn.Rows.Count > 0)
                    {
                        StringBuilder sbWarning = new StringBuilder();
                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["INFO_CANT_INSERT_DEPTH"]);
                        sbWarning.Append("');");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                        txtDate.Text = string.Empty;
                        hfDate.Value = string.Empty;
                    }
                    else
                    {
                        // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S04
                        DataTable dtPrevReturn = ExchangeMngBlo.WatchExchangeRatePrevInfo(hfDate.Value.Replace("-", ""), txtHfRentCd.Text);

                        if (dtPrevReturn != null)
                        {
                            if (dtPrevReturn.Rows.Count > 0)
                            {
                                double dblPrevData = Double.Parse(dtPrevReturn.Rows[0]["DongToDollar"].ToString());
                                double dblNowData = Double.Parse(txtBaseRate.Text);
                                double dblGap = 0d;

                                dblGap = dblNowData - dblPrevData;

                                if (dblGap != 0d)
                                {
                                    txtFluctAmt.Text = (dblGap).ToString();
                                    txtFluctRatio.Text = (dblGap / dblPrevData * 100).ToString();
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

                        txtDate.Text = hfDate.Value;
                    }
                }
            }
            else
            {
                txtFluctAmt.Text = "0";
                txtFluctRatio.Text = "0";
            }
        }
    }
}