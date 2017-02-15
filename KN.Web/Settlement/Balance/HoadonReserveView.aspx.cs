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

using KN.Settlement.Biz;

namespace KN.Web.Settlement.Balance
{
    public partial class HoadonReserveView : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();

                        chkPayDt.Checked = CommValue.AUTH_VALUE_TRUE;
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
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    hfRentCd.Value = Request.Params[Master.PARAM_DATA1].ToString();

                    if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                    {
                        hfPaymentDt.Value = Request.Params[Master.PARAM_DATA2].ToString();

                        if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3].ToString()))
                        {
                            hfPaymentSeq.Value = Request.Params[Master.PARAM_DATA3].ToString();

                            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA4].ToString()))
                            {
                                hfPaymentDetSeq.Value = Request.Params[Master.PARAM_DATA4].ToString();

                                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA5].ToString()))
                                {
                                    hfCurrentPage.Value = Request.Params[Master.PARAM_DATA5].ToString();
                                }

                                isReturnOk = CommValue.AUTH_VALUE_TRUE;
                            }
                            else
                            {
                                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Request.Params[Master.PARAM_DATA1].ToString(), CommValue.AUTH_VALUE_FALSE);
                            }
                        }
                        else
                        {
                            Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Request.Params[Master.PARAM_DATA1].ToString(), CommValue.AUTH_VALUE_FALSE);
                        }
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Request.Params[Master.PARAM_DATA1].ToString(), CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltNm.Text = TextNm["NAME"];
            ltPaymentMethod.Text = TextNm["PAYMENTKIND"];
            chkPayDt.Text = TextNm["TODAY"];
            ltPayDt.Text = TextNm["PAYDAY"];
            ltInvoiceCont.Text = TextNm["TAXPAYER"];
            ltTaxCd.Text = TextNm["TAXCD"];
            ltTaxAddr.Text = TextNm["ADDR"];
            ltItemViAmt.Text = TextNm["PAYMENT"];
            ltItemTotViAmt.Text = TextNm["TOTALPAY"];

            lnkbtnApply.Text = TextNm["APPLY"];
            lnkbtnEntireApply.Text = TextNm["ENTIREAPPLY"];
            lnkbtnIssuing.Text = TextNm["ISSUING"];
            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnReIssuing.Text = TextNm["REISSUE"];

        }

        protected void LoadData()
        {
            int intPaymentSeq       = CommValue.NUMBER_VALUE_0;
            int intPaymentDetSeq    = CommValue.NUMBER_VALUE_0;

            if (!string.IsNullOrEmpty(hfPaymentSeq.Value))
            {
                intPaymentSeq = Int32.Parse(hfPaymentSeq.Value);
            }

            if (!string.IsNullOrEmpty(hfPaymentDetSeq.Value))
            {
                intPaymentDetSeq = Int32.Parse(hfPaymentDetSeq.Value);
            }

            CommCdDdlUtil.MakeSubCdDdlNoTitle(ddlInvoiceCont, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ACCOUNT, CommValue.ACCOUNT_VALUE_TAXPAYER);

            // KN_USP_MNG_SELECT_SETTLEMENT_S01
            DataSet dsReturn = BalanceMngBlo.SpreadReserveHoadonDetList(hfRentCd.Value, hfPaymentDt.Value, intPaymentSeq, intPaymentDetSeq, Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                if (dsReturn.Tables[0] != null)
                {
                    if (dsReturn.Tables[0].Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        ltInsRoomNo.Text = dsReturn.Tables[0].Rows[0]["RoomNo"].ToString();
                        txtHfUserSeq.Text = dsReturn.Tables[0].Rows[0]["UserSeq"].ToString();
                        txtInsNm.Text = TextLib.StringDecoder(dsReturn.Tables[0].Rows[0]["UserNm"].ToString());
                        txtInsPayDt.Text = TextLib.MakeDateEightDigit(dsReturn.Tables[0].Rows[0]["PrintOutDt"].ToString());
                        hfInsPayDt.Value = TextLib.MakeDateEightDigit(dsReturn.Tables[0].Rows[0]["PrintOutDt"].ToString());
                        txtHfPayDt.Text = TextLib.MakeDateEightDigit(dsReturn.Tables[0].Rows[0]["PaymentDt"].ToString());
                        txtInsTaxCd.Text = dsReturn.Tables[0].Rows[0]["UserTaxCd"].ToString();
                        txtHfTaxCd.Text = dsReturn.Tables[0].Rows[0]["UserTaxCd"].ToString();
                        txtInsTaxAddr.Text = TextLib.StringDecoder(dsReturn.Tables[0].Rows[0]["UserAddr"].ToString());
                        txtHfTaxAddr.Text = TextLib.StringDecoder(dsReturn.Tables[0].Rows[0]["UserAddr"].ToString());
                        txtInsTaxDetAddr.Text = TextLib.StringDecoder(dsReturn.Tables[0].Rows[0]["UserDetAddr"].ToString());
                        txtHfTaxDetAddr.Text = TextLib.StringDecoder(dsReturn.Tables[0].Rows[0]["UserDetAddr"].ToString());
                        ltInsPaymentNm.Text = TextLib.StringDecoder(dsReturn.Tables[0].Rows[0]["PaymentNm"].ToString());
                        txtHfPaymentSeq.Text = dsReturn.Tables[0].Rows[0]["PaymentSeq"].ToString();
                        txtHfPaymentDetSeq.Text = dsReturn.Tables[0].Rows[0]["PaymentDetSeq"].ToString();

                        if (!string.IsNullOrEmpty(dsReturn.Tables[0].Rows[0]["InvoiceContractYn"].ToString()))
                        {
                            if (dsReturn.Tables[0].Rows[0]["InvoiceContractYn"].ToString().Equals(CommValue.CHOICE_VALUE_NO))
                            {
                                ddlInvoiceCont.SelectedValue = CommValue.TAXPAYER_TYPE_VALUE_TENANT;
                            }
                            else
                            {
                                ddlInvoiceCont.SelectedValue = CommValue.TAXPAYER_TYPE_VALUE_CONTRACTOR;
                            }
                        }

                        if (Session["LangCd"].Equals(CommValue.LANG_VALUE_VIETNAMESE))
                        {
                            ltInsPaymentMethod.Text = dsReturn.Tables[0].Rows[0]["SvcMM"].ToString() + " / " + dsReturn.Tables[0].Rows[0]["SvcYear"].ToString() + " " + TextLib.StringDecoder(dsReturn.Tables[0].Rows[0]["ClassNm"].ToString());
                        }
                        else
                        {
                            ltInsPaymentMethod.Text = dsReturn.Tables[0].Rows[0]["SvcYear"].ToString() + " / " + dsReturn.Tables[0].Rows[0]["SvcMM"].ToString() + " " + TextLib.StringDecoder(dsReturn.Tables[0].Rows[0]["ClassNm"].ToString());
                        }

                        ltInsItemViAmt.Text = TextLib.MakeVietIntNo(Int32.Parse(dsReturn.Tables[0].Rows[0]["TotSellingPrice"].ToString()).ToString("###,##0"));
                        ltInsItemTotViAmt.Text = TextLib.MakeVietIntNo(Int32.Parse(dsReturn.Tables[0].Rows[0]["ItemTotViAmt"].ToString()).ToString("###,##0"));

                        if (!string.IsNullOrEmpty(dsReturn.Tables[0].Rows[0]["StatusCd"].ToString()))
                        {
                            if (dsReturn.Tables[0].Rows[0]["StatusCd"].ToString().Equals(CommValue.CALCULATE_STATUS_TYPE_VALUE_APPROVAL))
                            {
                                lnkbtnIssuing.Visible = CommValue.AUTH_VALUE_FALSE;
                                lnkbtnReIssuing.Visible = Master.isModDelAuthOk;


                                lnkbtnIssuing.Visible = CommValue.AUTH_VALUE_FALSE;
                                lnkbtnReIssuing.Visible = CommValue.AUTH_VALUE_FALSE;
                            }
                            else
                            {
                                lnkbtnIssuing.Visible = Master.isWriteAuthOk;
                                lnkbtnReIssuing.Visible = CommValue.AUTH_VALUE_FALSE;


                                lnkbtnIssuing.Visible = CommValue.AUTH_VALUE_FALSE;
                                lnkbtnReIssuing.Visible = CommValue.AUTH_VALUE_FALSE;
                            }
                        }
                    }
                }

                if (dsReturn.Tables[1] != null)
                {
                    if (dsReturn.Tables[1].Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        MakeDetail(dsReturn.Tables[1]);
                    }
                }
            }
        }

        protected void MakeDetail(DataTable dtParam)
        {
            lvInvoiceList.DataSource = dtParam;
            lvInvoiceList.DataBind();
        }

        protected void lvInvoiceList_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltPaymentDt = (Literal)lvInvoiceList.FindControl("ltPaymentDt");
            ltPaymentDt.Text = TextNm["PAYDAY"];
            Literal ltPaymentKind = (Literal)lvInvoiceList.FindControl("ltPaymentKind");
            ltPaymentKind.Text = TextNm["PAYMENTKIND"];
            Literal ltPayMethod = (Literal)lvInvoiceList.FindControl("ltPayMethod");
            ltPayMethod.Text = TextNm["PAYMETHOD"];
            Literal ltUnitPrimeCost = (Literal)lvInvoiceList.FindControl("ltUnitPrimeCost");
            ltUnitPrimeCost.Text = TextNm["UNITPRICE"];
            Literal ltQty = (Literal)lvInvoiceList.FindControl("ltQty");
            ltQty.Text = TextNm["QTY"];
            Literal ltVATAmount = (Literal)lvInvoiceList.FindControl("ltVATAmount");
            ltVATAmount.Text = TextNm["VAT"];
            Literal ltAmount = (Literal)lvInvoiceList.FindControl("ltAmount");
            ltAmount.Text = TextNm["TOTALAMT"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvInvoiceList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    ((Literal)e.Item.FindControl("ltPaymentDt")).Text = TextNm["PAYDAY"];
                    ((Literal)e.Item.FindControl("ltPaymentKind")).Text = TextNm["PAYMENTKIND"];
                    ((Literal)e.Item.FindControl("ltPayMethod")).Text = TextNm["PAYMETHOD"];
                    ((Literal)e.Item.FindControl("ltUnitPrimeCost")).Text = TextNm["UNITPRICE"];
                    ((Literal)e.Item.FindControl("ltQty")).Text = TextNm["QTY"];
                    ((Literal)e.Item.FindControl("ltVATAmount")).Text = TextNm["VAT"];
                    ((Literal)e.Item.FindControl("ltAmount")).Text = TextNm["TOTALAMT"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvInvoiceList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                Literal ltInsPaymentDt = (Literal)iTem.FindControl("ltInsPaymentDt");
                TextBox txtHfPaymentDt = (TextBox)iTem.FindControl("txtHfPaymentDt");
                TextBox txtHfPaymentSeq = (TextBox)iTem.FindControl("txtHfPaymentSeq");
                TextBox txtHfPaymentDetSeq = (TextBox)iTem.FindControl("txtHfPaymentDetSeq");

                Image imgCheck = (Image)iTem.FindControl("imgCheck");
                if (!string.IsNullOrEmpty(drView["NowItem"].ToString()))
                {
                    if (drView["NowItem"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        imgCheck.ImageUrl = "~/Common/Images/Icon/check.gif";
                    }
                    else
                    {
                        imgCheck.ImageUrl = "~/Common/Images/Common/blank.gif";
                    }
                }

                if (!string.IsNullOrEmpty(drView["PaymentDt"].ToString()))
                {
                    ltInsPaymentDt.Text = TextLib.MakeDateEightDigit(TextLib.StringDecoder(drView["PaymentDt"].ToString()));
                    txtHfPaymentDt.Text = TextLib.StringDecoder(drView["PaymentDt"].ToString());
                    txtHfPaymentSeq.Text = TextLib.StringDecoder(drView["PaymentSeq"].ToString());
                    txtHfPaymentDetSeq.Text = TextLib.StringDecoder(drView["PaymentDetSeq"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    Literal ltInsPaymentKind = (Literal)iTem.FindControl("ltInsPaymentKind");

                    if (Session["LangCd"].Equals(CommValue.LANG_VALUE_VIETNAMESE))
                    {
                        ltInsPaymentKind.Text = drView["SvcMM"].ToString() + " / " + drView["SvcYear"].ToString() + " " + TextLib.StringDecoder(drView["ClassNm"].ToString());
                    }
                    else
                    {
                        ltInsPaymentKind.Text = drView["SvcYear"].ToString() + " / " + drView["SvcMM"].ToString() + " " + TextLib.StringDecoder(drView["ClassNm"].ToString());
                    }
                }

                if (!string.IsNullOrEmpty(drView["PaymentNm"].ToString()))
                {
                    Literal ltInsPayMethod = (Literal)iTem.FindControl("ltInsPayMethod");
                    ltInsPayMethod.Text = TextLib.StringDecoder(drView["PaymentNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["UnitPrimeCost"].ToString()))
                {
                    Literal ltInsUnitPrimeCost = (Literal)iTem.FindControl("ltInsUnitPrimeCost");
                    ltInsUnitPrimeCost.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["UnitPrimeCost"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["Qty"].ToString()))
                {
                    Literal ltInsQty = (Literal)iTem.FindControl("ltInsQty");
                    ltInsQty.Text = drView["Qty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["VATAmount"].ToString()))
                {
                    Literal ltInsVATAmount = (Literal)iTem.FindControl("ltInsVATAmount");
                    ltInsVATAmount.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["VATAmount"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["TotSellingPrice"].ToString()))
                {
                    Literal ltInsAmount = (Literal)iTem.FindControl("ltInsAmount");
                    ltInsAmount.Text = TextLib.MakeVietIntNo(Int32.Parse(drView["TotSellingPrice"].ToString()).ToString("###,##0"));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailView_Click(object sender, EventArgs e)
        {
            try
            {
                LoadData();

                chkPayDt.Checked = CommValue.AUTH_VALUE_TRUE;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlInvoiceCont_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string strMemIp = Request.ServerVariables["REMOTE_ADDR"];
                string strInvoiceCont = string.Empty;

                if (ddlInvoiceCont.SelectedValue.Equals(CommValue.TAXPAYER_TYPE_VALUE_CONTRACTOR))
                {
                    strInvoiceCont = CommValue.CHOICE_VALUE_YES;
                }
                else if (ddlInvoiceCont.SelectedValue.Equals(CommValue.TAXPAYER_TYPE_VALUE_TENANT))
                {
                    strInvoiceCont = CommValue.CHOICE_VALUE_NO;
                }

                // KN_USP_MNG_UPDATE_HOADONCONTINFO_M00
                BalanceMngBlo.ModifyHoadonContInfo(hfRentCd.Value, ltInsRoomNo.Text, strInvoiceCont, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strMemIp);

                LoadData();

                chkPayDt.Checked = CommValue.AUTH_VALUE_TRUE;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void chkPayDt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPayDt.Checked)
            {
                string strToday = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
                strToday = TextLib.MakeDateEightDigit(strToday);

                txtInsPayDt.Text = strToday;
                hfInsPayDt.Value = strToday;
            }
            else
            {
                txtInsPayDt.Text = txtHfPayDt.Text;
                hfInsPayDt.Value = txtHfPayDt.Text;
            }
        }

        protected void lnkbtnApply_Click(object sender, EventArgs e)
        {
            try
            {
                string strOldNm = txtHfNm.Text;
                string strNewNm = txtInsNm.Text;
                string strOldTaxCd = txtHfTaxCd.Text;
                string strNewTaxCd = txtInsTaxCd.Text;
                string strOldTaxAddr = txtHfTaxAddr.Text;
                string strNewTaxAddr = txtInsTaxAddr.Text;
                string strOldTaxDetAddr = txtHfTaxDetAddr.Text;
                string strNewTaxDetAddr = txtInsTaxDetAddr.Text;
                string strDdlInvoiceCont = ddlInvoiceCont.SelectedValue;
                string strInvoiceContYn = string.Empty;

                string strPayDt = hfInsPayDt.Value.Replace("-", "");

                if (!strOldNm.Equals(strNewNm) ||
                    !strOldTaxCd.Equals(strNewTaxCd) ||
                    !strOldTaxAddr.Equals(strNewTaxAddr) ||
                    !strOldTaxDetAddr.Equals(strNewTaxDetAddr))
                {
                    string strPaymentDt = txtHfPayDt.Text.Replace("-", "");
                    string strPaymentSeq = txtHfPaymentSeq.Text;

                    int intPaymentSeq = CommValue.NUMBER_VALUE_0;

                    if (!string.IsNullOrEmpty(strPaymentSeq))
                    {
                        intPaymentSeq = Int32.Parse(strPaymentSeq);

                        if (strDdlInvoiceCont.Equals(CommValue.TAXPAYER_TYPE_VALUE_CONTRACTOR))
                        {
                            strInvoiceContYn = CommValue.CHOICE_VALUE_YES;
                        }
                        else
                        {
                            strInvoiceContYn = CommValue.CHOICE_VALUE_NO;
                        }

                        // KN_USP_MNG_UPDATE_SETTLEMENT_M01
                        BalanceMngBlo.ModifyTaxInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strPaymentDt, intPaymentSeq, strNewNm, strNewTaxCd, strNewTaxAddr,
                                                    strNewTaxDetAddr, strInvoiceContYn);
                    }
                }

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + hfRentCd.Value + "&" + Master.PARAM_DATA5 + "=" + hfCurrentPage.Value, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnEntireApply_Click(object sender, EventArgs e)
        {
            try
            {
                string strOldNm = txtHfNm.Text;
                string strNewNm = txtInsNm.Text;
                string strOldTaxCd = txtHfTaxCd.Text;
                string strNewTaxCd = txtInsTaxCd.Text;
                string strOldTaxAddr = txtHfTaxAddr.Text;
                string strNewTaxAddr = txtInsTaxAddr.Text;
                string strOldTaxDetAddr = txtHfTaxDetAddr.Text;
                string strNewTaxDetAddr = txtInsTaxDetAddr.Text;
                string strDdlInvoiceCont = ddlInvoiceCont.SelectedValue;
                string strInvoiceContYn = string.Empty;

                string strPayDt = hfInsPayDt.Value.Replace("-", "");

                if (!strOldNm.Equals(strNewNm) ||
                    !strOldTaxCd.Equals(strNewTaxCd) ||
                    !strOldTaxAddr.Equals(strNewTaxAddr) ||
                    !strOldTaxDetAddr.Equals(strNewTaxDetAddr))
                {
                    string strPaymentDt = txtHfPayDt.Text.Replace("-", "");
                    string strPaymentSeq = txtHfPaymentSeq.Text;

                    int intPaymentSeq = CommValue.NUMBER_VALUE_0;

                    if (!string.IsNullOrEmpty(strPaymentSeq))
                    {
                        intPaymentSeq = Int32.Parse(strPaymentSeq);

                        if (strDdlInvoiceCont.Equals(CommValue.TAXPAYER_TYPE_VALUE_CONTRACTOR))
                        {
                            strInvoiceContYn = CommValue.CHOICE_VALUE_YES;
                        }
                        else
                        {
                            strInvoiceContYn = CommValue.CHOICE_VALUE_NO;
                        }

                        // KN_USP_MNG_UPDATE_SETTLEMENT_M02
                        BalanceMngBlo.ModifyEntireTaxInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strPaymentDt, intPaymentSeq, strNewNm, strNewTaxCd, strNewTaxAddr,
                                                          strNewTaxDetAddr, strInvoiceContYn);
                    }
                }

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + hfRentCd.Value + "&" + Master.PARAM_DATA5 + "=" + hfCurrentPage.Value, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 발행
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnIssuing_Click(object sender, EventArgs e)
        {
            try
            {
                string strUserSeq = txtHfUserSeq.Text;
                string strInsNm = txtInsNm.Text;
                string strPaymentDt = txtHfPayDt.Text;
                string strPaymentSeq = txtHfPaymentSeq.Text;
                string strAddr = txtInsTaxAddr.Text;
                string strDetAddr = txtInsTaxDetAddr.Text;
                string strInsTaxCd = txtInsTaxCd.Text;
                string strPrintOutPaymentDt = hfInsPayDt.Value;
                string strIp = Request.ServerVariables["REMOTE_ADDR"];
                string strDdlInvoiceCont = ddlInvoiceCont.SelectedValue;
                string strInvoiceContYn = string.Empty;
                
                int intPaymentSeq = 0;

                if (!string.IsNullOrEmpty(strPaymentSeq))
                {
                    intPaymentSeq = Int32.Parse(strPaymentSeq);
                }

                if (strDdlInvoiceCont.Equals(CommValue.TAXPAYER_TYPE_VALUE_CONTRACTOR))
                {
                    strInvoiceContYn = CommValue.CHOICE_VALUE_YES;
                }
                else
                {
                    strInvoiceContYn = CommValue.CHOICE_VALUE_NO;
                }

                // 해당 정보로 변경처리함.
                // KN_USP_MNG_UPDATE_SETTLEMENT_M01
                BalanceMngBlo.ModifyTaxInfo(CommValue.DEBITNCREDIT_TYPE_VALUE_CREDIT, strPaymentDt, intPaymentSeq, strInsNm, strInsTaxCd, strAddr,
                                            strDetAddr, strInvoiceContYn);

                // 영수증 출력테이블에 해당 결제정보 등록시키고 결제완료 처리시킴.
                // KN_USP_MNG_INSERT_HOADONINFO_M00
                BalanceMngBlo.RegistryChestNutHoadonList(Session["LangCd"].ToString(), strUserSeq, strInsNm, strPaymentDt, intPaymentSeq, strAddr, strDetAddr, strInsTaxCd, 
                                                         strPrintOutPaymentDt, strInsNm, strInvoiceContYn, Session["CompCd"].ToString(), Session["MemNo"].ToString(),
                                                         strIp, string.Empty);

                // KN_USP_MNG_INSERT_HOADONINFO_M01
                BalanceMngBlo.RegistryKeangNamHoadonList(Session["LangCd"].ToString(), strUserSeq, strInsNm, strPaymentDt, intPaymentSeq, strAddr, strDetAddr, strInsTaxCd,
                                                         strPrintOutPaymentDt, strInsNm, strInvoiceContYn, Session["CompCd"].ToString(), Session["MemNo"].ToString(),
                                                         strIp, string.Empty);

                // 화돈 출력권한 부여
                Session["HoadonOk"] = CommValue.AUTH_VALUE_FULL;

                // 화돈 대상 데이터 출력
                StringBuilder sbRdHoadon = new StringBuilder();

                sbRdHoadon.Append("window.open(\"/Common/RdPopup/RDPopupHoadonList.aspx\", \"HoaDon\", \"status=no, resizable=no, width=740, height=700, left=100,top=100, scrollbars=no, menubar=no, toolbar=no, location=no\");");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "HoadonPrint", sbRdHoadon.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 재발행
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnReIssuing_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 목록
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + hfRentCd.Value + "&" + Master.PARAM_DATA5 + "=" + hfCurrentPage.Value, CommValue.AUTH_VALUE_FALSE);
        }
    }
}
