using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Resident.Biz;
using System.IO;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web;
using System.Net;

namespace KN.Web.Resident.Contract
{
    public partial class ResidenceSalesView : BasePage
    {
        protected string strRentCd = string.Empty;
        protected int intRentSeq = 0;

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
                        DataSet dsReturn = new DataSet();

                        // KN_USP_RES_SELECT_SALESINFO_S01
                        dsReturn = ContractMngBlo.WatchSalesInfoView(Session["LangCd"].ToString(), strRentCd, intRentSeq);

                        if (dsReturn != null)
                        {
                            if (dsReturn.Tables[0].Rows.Count > 0)
                            {
                                InitControls();
                                LoadData(dsReturn);
                                if(dsReturn.Tables[3].Rows.Count>0)
                                {
                                    LoadFitOutFee(dsReturn.Tables[3]);
                                    chkUsingMnFee.Visible = true;
                                    lineRow.Visible = false;
                                    isApplyFeeMn.Checked = true;
                                }
                                else
                                {
                                    ListFitOutFee.Visible = false;
                                    lineRow.Visible = true;
                                    isApplyFeeMn.Checked = false;
                                }
                                if(dsReturn.Tables[4].Rows.Count>0)
                                {
                                    LoadMngFee(dsReturn.Tables[4]);
                                }
                                if (dsReturn.Tables[5].Rows.Count > 0)
                                {
                                    LoadSubLessor(dsReturn.Tables[5]);
                                }else
                                {
                                    divSubLessor.Visible = false;
                                }
                            }
                            else
                            {
                                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                            }
                        }
                        else
                        {
                            Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                        }
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 매개변수 체크
        /// </summary>
        /// <returns></returns>
        public bool CheckParams()
        {
            bool isReturn = CommValue.AUTH_VALUE_TRUE;

            if (Session["ViewContract"] == null)
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }
            else
            {
                if (!Session["ViewContract"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    isReturn = CommValue.AUTH_VALUE_FALSE;
                }
                else
                {
                    Session["ViewContract"] = null;

                    if (Request.Params[Master.PARAM_DATA1] == null || Request.Params[Master.PARAM_DATA2] == null)
                    {
                        isReturn = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) || string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                        {
                            isReturn = CommValue.AUTH_VALUE_FALSE;
                        }
                        else
                        {
                            strRentCd = Request.Params[Master.PARAM_DATA1].ToString();
                            intRentSeq = Int32.Parse(Request.Params[Master.PARAM_DATA2].ToString());
                            txtHfRentCd.Text = strRentCd;
                            txtHfRentSeq.Text = intRentSeq.ToString();
                        }
                    }
                }
            }
            return isReturn;
        }

        protected void InitControls()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            ltBasicInfo.Text = TextNm["TENANTINFO"];
            ltTenantNm.Text = TextNm["TENANTNM"];
            ltConcYn.Text = TextNm["CONCYN"];
            ltContNo.Text = TextNm["CONTNO"];
            ltAddr.Text = TextNm["ADDR"];
            ltICPN.Text = TextNm["IDNO"];
            ltIssueDt.Text = TextNm["ISSUEDT"];
            ltIssuePlace.Text = TextNm["ISSUEPLACE"];
            ltTel.Text = TextNm["TEL"];
            ltMobileNo.Text = TextNm["MOBILE"];
            ltFAX.Text = TextNm["FAX"];
            ltEmail.Text = TextNm["EMAIL"];
            ltRepresent.Text = TextNm["REPRESENTATIVE"];
            ltPosition.Text = TextNm["POS"];
            ltBank.Text = TextNm["BANKACC"];
            ltTaxCd.Text = TextNm["TAXCD"];

            ltCoInfo.Text = TextNm["COOWNERINFO"];
            ltCoOwnerNm.Text = TextNm["COOWNERNM"];
            ltRelationShip.Text = TextNm["POS"];
            ltCoRss.Text = TextNm["IDNO"];
            ltCoIssueDt.Text = TextNm["ISSUEDT"];
            ltCoIssuePlace.Text = TextNm["ISSUEPLACE"];
            ltCoAddr.Text = TextNm["ADDR"];

            ltRoomInfo.Text = TextNm["ROOMINFO"];
            ltUnitNo.Text = TextNm["UNITNO"];
            ltFloor.Text = TextNm["FLOOR"];
            ltLeasingArea.Text = TextNm["LEASINGAREA"];

            ltRentTerm.Text = TextNm["TERMINFO"];
            ltCommencingDt.Text = TextNm["COMMENCINGDT"];
            ltExpiringDt.Text = TextNm["EXPIRINGDT"];
            ltLastKeyDt.Text = TextNm["LASTKEYDT"];

            ltRetalFee.Text = TextNm["RENTALFEE"];
            ltExchangeRate.Text = TextNm["EXCHANGERATE"];
            ltExchangeRateTitle.Text = TextNm["EXCHANGERATE"];
            ltSumRentUSDNo.Text = TextNm["SUMRENTOFUSDNO"];
            ltSumRentUSDNoUnit.Text = TextNm["DOLLAR"];
            ltSumRentVNDNo.Text = TextNm["SUMRENTOFDONGNO"];
            ltSumRentVNDNoUnit.Text = TextNm["DONG"];
            ltDeposit.Text = TextNm["DEPOSIT"];
            ltSumDepositUSDNo.Text = TextNm["SUMDEPOSITUSD"];
            ltDepositSumUSDNoUnit.Text = TextNm["DOLLAR"];
            ltSumDepositVNDNo.Text = TextNm["SUMDEPOSITVND"];
            ltDepositSumVNDNoUnit.Text = TextNm["DONG"];

            ltMngFee.Text = TextNm["MANAGEFEE"] + " (" + TextNm["MNGFEEUNIT"] + ")";
            ltInitMMMngDay.Text = TextNm["FITTINGDAY"];
            ltInitMMMngDayUnit.Text = TextNm["DAYS"];
            ltInitMMMngDt.Text = TextNm["FITTINGOUTDT"];
            //ltInitPerMMMngVND.Text = TextNm["INITPERMMRENTVND"];
           // ltInitPerMMMngVNDUnit.Text = TextNm["DONG"];
            //ltInitPerMMMngUSD.Text = TextNm["INITPERMMRENTUSD"];
           // ltInitPerMMMngUSDUnit.Text = TextNm["DOLLAR"];

            ltPerMMRentUSD.Text = TextNm["MMRENTUSD"];
            ltPerMMRentUSDUnit.Text = TextNm["DOLLAR"];
            ltPerMMRentVND.Text = TextNm["MMRENTVND"];
            ltPerMMRentVNDUnit.Text = TextNm["DONG"];
          //  ltPerMMMngUSD.Text = TextNm["MMMNGUSD"];
           // ltPerMMMngUSDNoUnit.Text = TextNm["DOLLAR"];
          //  ltPerMMMngVND.Text = TextNm["MMMNGVND"];
         //   ltPerMMMngVNDNoUnit.Text = TextNm["DONG"];

            ltUse.Text = TextNm["USE"];
            ltTradeNm.Text = TextNm["TRADE"];
            ltPurpose.Text = TextNm["PURPOSE"];

            ltOtherCondition.Text = TextNm["OTHERCOND"];
            ltPlusCondDt.Text = TextNm["COMMENCINGDT"];
            ltPlusCond.Text = TextNm["OTHERCOND"];

            ltOther.Text = TextNm["OTHERS"];
            ltMemo.Text = TextNm["MEMO"];

            lnkbtnList.Text = TextNm["LIST"];
            lnkbtnList.PostBackUrl = Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + strRentCd;

            lnkbtnDel.Text = TextNm["DELETE"];
            lnkbtnDel.OnClientClick = "javascript:return fnShowModal('" + Master.PARAM_DATA1 + "','" + strRentCd + "','" + Master.PARAM_DATA2 + "'," + intRentSeq.ToString() + ");";
            lnkbtnDel.Visible = Master.isModDelAuthOk;

            lnkbtnMod.Text = TextNm["MODIFY"];
            lnkbtnMod.PostBackUrl = Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + strRentCd + "&" + Master.PARAM_DATA2 + "=" + intRentSeq.ToString();
            lnkbtnMod.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_CONT"] + "');";
            lnkbtnMod.Visible = Master.isModDelAuthOk;

        }

        /// <summary>
        /// 데이터 바인딩
        /// </summary>
        /// <param name="dsParams"></param>
        public void LoadData(DataSet dsParams)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            DataTable dtRentInfo = new DataTable();

            dtRentInfo = dsParams.Tables[0];

            //입주자정보
            ltInsPersonal.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["PersonalNm"].ToString());
            ltInsTenantNm.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["ContractNm"].ToString());
            ltInsConcYn.Text = dtRentInfo.Rows[0]["ContChk"].ToString();
            ltInsContTy.Text = dtRentInfo.Rows[0]["RentNm"].ToString();
            ltInsTerm.Text = dtRentInfo.Rows[0]["RentTyNm"].ToString();
            ltInsContNo.Text = dtRentInfo.Rows[0]["ContractNo"].ToString();
            ltInsAddr.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Addr"].ToString());
            ltDetAddr.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["DetailAddr"].ToString());
            ltInsICPN.Text = dtRentInfo.Rows[0]["RssNo"].ToString();
            ltInsIssueDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["IssueDt"].ToString());
            ltInsIssuePlace.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["IssuePlace"].ToString());
            ltInsTelFrontNo.Text = dtRentInfo.Rows[0]["TelFontNo"].ToString();
            ltInsTelMidNo.Text = dtRentInfo.Rows[0]["TelMidNo"].ToString();
            ltIntTelRearNo.Text = dtRentInfo.Rows[0]["TelRearNo"].ToString();
            ltInsMobileFrontNo.Text = dtRentInfo.Rows[0]["MobileFrontNo"].ToString();
            ltInsMobileMidNo.Text = dtRentInfo.Rows[0]["MobileMidNo"].ToString();
            ltInsMobileRearNo.Text = dtRentInfo.Rows[0]["MobileRearNo"].ToString();
            ltInsFAXFrontNo.Text = dtRentInfo.Rows[0]["OfficeTelFontNo"].ToString();
            ltInsFAXMidNo.Text = dtRentInfo.Rows[0]["OfficeTelMidNo"].ToString();
            ltInsFAXRearNo.Text = dtRentInfo.Rows[0]["OfficeTelRearNo"].ToString();
            ltInsEmailID.Text = dtRentInfo.Rows[0]["EmailID"].ToString();
            ltInsEmailServer.Text = dtRentInfo.Rows[0]["EmailServer"].ToString();
            ltInsRepresent.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["LegalRep"].ToString());
            ltInsPosition.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Position"].ToString());
            ltInsBank.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["BankAcc"].ToString());
            ltInsTaxCd.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["TaxCd"].ToString());
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["Ref_ContractNo"].ToString()))
            {
                ltSubLessorNm.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["SubLessorNm"].ToString()) + "(" + TextLib.StringDecoder(dtRentInfo.Rows[0]["SubLessorRoom"].ToString()) + ")";
            }else
            {
                tdSubLessor.Visible = false;
            }
            

            // 공동명의인
            ltInsCoOwnerNm.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["CoOwner"].ToString());
            ltInsRelationShip.Text =TextLib.StringDecoder( dtRentInfo.Rows[0]["Relationship"].ToString());
            ltInsCoRss.Text = dtRentInfo.Rows[0]["CoRssNo"].ToString();
            ltInsColssueDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["CoIssueDt"].ToString());
            ltColssuePlace.Text = dtRentInfo.Rows[0]["CoIssuePlace"].ToString();
            ltInsCoAddr.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["CoAddr"].ToString());
            ltInsCoDetAddr.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["CoDetailAddr"].ToString());

            //기간
            ltInsCommencingDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["ContDt"].ToString());
            ltInsExpiringDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["ResaleDt"].ToString());

            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["LastKeyDt"].ToString()))
            {
                ltInsLastKeyDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["LastKeyDt"].ToString());
            }

            //방정보
            ltInsUnitNo.Text = dtRentInfo.Rows[0]["RoomNo"].ToString();
            ltInsFloor.Text = dtRentInfo.Rows[0]["FloorNo"].ToString();
            ltInsLeasingArea.Text = dtRentInfo.Rows[0]["LeasingArea"].ToString();

            //임대비용
            //ltInsExcangeRate.Text = dtRentInfo.Rows[0]["DongToDollar"].ToString();
            chkCC.Checked = dtRentInfo.Rows[0]["CURNCY_TYPE"].ToString() == "CC";
            if (!chkCC.Checked)
            {
                txtFC.Text = dtRentInfo.Rows[0]["FIXED_DONGTODOLLAR"].ToString();
            }
            else
            {
                txtFC.Visible = false;
            }
            txtFloation.Text = dtRentInfo.Rows[0]["INFLATION_RATE"].ToString();

            if (dtRentInfo.Rows[0]["RentTy"].ToString().Equals("0001"))
            {
                trSumRent.Visible = true;
                trPerMMRent.Visible = false;

                ltInsSumRentVNDNo.Text = TextLib.MakeVietIntNo(double.Parse(dtRentInfo.Rows[0]["RentalFeeVNDNo"].ToString()).ToString("###,##0"));
                ltInsSumRentUSDNo.Text = TextLib.MakeVietRealNo(double.Parse(dtRentInfo.Rows[0]["RentalFeeUSDNo"].ToString()).ToString("###,##0.##"));

            }
            else
            {
                trSumRent.Visible = false;
                trPerMMRent.Visible = true;
                ltInsPerMMRentVND.Text = TextLib.MakeVietIntNo(double.Parse(dtRentInfo.Rows[0]["RentalFeeVNDNo"].ToString()).ToString("###,##0"));
                ltInsPerMMREntUSD.Text = TextLib.MakeVietRealNo(double.Parse(dtRentInfo.Rows[0]["RentalFeeUSDNo"].ToString()).ToString("###,##0.##"));
            }

            //보증금
            ltInsSumDepositVNDNo.Text = TextLib.MakeVietIntNo(double.Parse(dtRentInfo.Rows[0]["DepositVNDNo"].ToString()).ToString("###,##0"));
            ltInsSumDepositUSDNo.Text = TextLib.MakeVietRealNo(double.Parse(dtRentInfo.Rows[0]["DepositUSDNo"].ToString()).ToString("###,##0.##"));

            //관리비용
            ltContInitMMMngDay.Text = dtRentInfo.Rows[0]["InitDay"].ToString();
            if (!string.IsNullOrEmpty(dtRentInfo.Rows[0]["InitMMMngDt"].ToString()))
            {
                ltContInitMMMngDt.Text =  TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["InitMMMngDt"].ToString());
            }
           // ltContInitPerMMMngVND.Text = TextLib.MakeVietIntNo(double.Parse(dtRentInfo.Rows[0]["InitMMMngVNDNo"].ToString()).ToString("###,##0"));
            //ltContInitPerMMMngUSD.Text = TextLib.MakeVietRealNo(double.Parse(dtRentInfo.Rows[0]["InitMMMngUSDNo"].ToString()).ToString("###,##0.##"));
           // ltInsPerMMMngVND.Text = TextLib.MakeVietIntNo(double.Parse(dtRentInfo.Rows[0]["MMMngVNDNo"].ToString()).ToString("###,##0"));
           // ltInsPerMMMngUSD.Text = TextLib.MakeVietRealNo(double.Parse(dtRentInfo.Rows[0]["MMMngUSDNo"].ToString()).ToString("###,##0.##"));

            //용도
            ltInsTradeNm.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["TradeNm"].ToString());
            ltInsPurpose.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Purpose"].ToString());

            //기타조건
            ltInsPlusCondDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["PlusCondDt"].ToString());
            ltInsPlusCond.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Descript1"].ToString());

            //기타
            ltInsMemo.Text = TextLib.StringDecoder(dtRentInfo.Rows[0]["Descript2"].ToString());

            string strNowDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("/", "").Replace("-", "");
            string strResaleDt = dtRentInfo.Rows[0]["ResaleDt"].ToString();

            if (Int32.Parse(strNowDt) > Int32.Parse(strResaleDt))
            {
                lnkbtnMod.Visible = false;
                lnkbtnDel.Visible = false;
            }
            rbContractType.SelectedValue = dtRentInfo.Rows[0]["CONTRACT_TYPE"].ToString();
            chkSpecialContract.Checked = dtRentInfo.Rows[0]["IS_SPECIAL"].ToString() == "Y";
            ltPayMentTy.Text = dtRentInfo.Rows[0]["PAYMENT_TYPE"].ToString();
            ltM_UsingDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["M_S_USING_DATE"].ToString());
            ddlMPaymentCycle.Text = dtRentInfo.Rows[0]["M_PAYCYCLE_TYPE"].ToString();
            ltM_PayDt.Text = TextLib.MakeDateEightDigit(dtRentInfo.Rows[0]["M_S_PAY_DATE"].ToString());
            ddlMIsueDateType.SelectedValue = dtRentInfo.Rows[0]["M_ISUE_DATE_TYPE"].ToString();
            ltM_Adjust.Text = dtRentInfo.Rows[0]["M_ISUE_DATE_ADJUST"].ToString();
            ltM_PayCyle.Text = dtRentInfo.Rows[0]["M_PAYCYCLE"].ToString();
            txtContRemark.Text = dtRentInfo.Rows[0]["REMARK"].ToString();    
        }

        #region 이벤트 처리하는 부분

        protected void imgbtnDel_Click(object sender, ImageClickEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 팝업의 불법적인 접근을 제한하기 위한 세션 생성
            Session["DelContractYn"] = CommValue.CONCLUSION_TYPE_TEXT_YES;
        }

        protected void imgbtnMove_Click(object sender, ImageClickEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
        }
        public void LoadFitOutFee(DataTable dsSet)
        {
            var listfitOutFee = new StringBuilder();
            
            foreach (var fee in from DataRow row in dsSet.Rows select "<tr>" +
                                                                      "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeStartDt"].ToString()) + "\"></td>" +
                                                                      "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeEndDt"].ToString()) + "\"></td>" +
                                                                      "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeeExcRate"] + "\"></td>" +
                                                                      "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeePayAmt"] + "\"></td>" +
                                                                      "<td></td>" +
                                                                      "</tr>")
            {
                listfitOutFee.Append(fee);
            }
            diplayFitOutFee.InnerHtml = listfitOutFee.ToString();
        }

        public void LoadMngFee(DataTable dsSet)
        {
            var listMngFee = new StringBuilder();

            foreach (var fee in from DataRow row in dsSet.Rows
                                select "<tr>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeStartDt"].ToString()) + "\"></td>" +
                                       "<td align=\"left\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;margin-left: 48px;\" value=\"" + FormatDateTime(row["MngFeeEndDt"].ToString()) + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeeExcRate"] + "\"></td>" +
                                       "<td align=\"center\" class=\"P0\"><input name=\"\" type=\"text\" maxlength=\"10\" readonly=\"readonly\" id=\"\" class=\"grBg bgType1\" style=\"width:70px;\" value=\"" + row["MngFeePayAmt"] + "\"></td>" +
                                       "<td></td>" +
                                       "</tr>")
            {
                listMngFee.Append(fee);
            }
            diplayMngFee.InnerHtml = listMngFee.ToString();
        }

        #endregion

        protected string FormatDateTime(string dateTime)
        {
            if (String.IsNullOrEmpty(dateTime)) return "";
            string date = dateTime.Substring(0, 4) + "-" + dateTime.Substring(4, 2) + "-" + dateTime.Substring(6, 2);
            return date;
        }

        private void LoadSubLessor(DataTable dsSet)
        {
            var listSubLessor = new StringBuilder();
            foreach (var fee in from DataRow row in dsSet.Rows
                                select "<tr>" +
                                       "<td class=\"Bd-Lt TbTxtCenter\">" + row["ContractNo"] + "(" + row["RoomNo"] + ")" + "</td>" +
                                       "<td class=\"Bd-Lt TbTxtCenter\">" + row["ContractNm"] + "</td>" +
                                       "<td class=\"Bd-Lt TbTxtCenter\">" + row["PhoneNum"] + "</td>" +
                                       "</tr>")
            {
                listSubLessor.Append(fee);
            }
            lstSubLessor.InnerHtml = listSubLessor.ToString();
         
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            string url = string.Format("ContractPDFView.aspx?rentCD={0}&rentSeq={1}", Request["RentCd"], Request["RentSeq"]);
            //string s = "window.open('" +  + "', 'popup_window', 'width=800,height=600,left=100,top=100,resizable=yes');";
            //ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('" + url + "');", true);
        }

        protected void btnPDF_Click1(object sender, EventArgs e)
        {
            string conLogconnString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(conLogconnString);
            string strCmd = string.Format("select * from ContractPDF where rentCD ='{0}' and rentSeq ='{1}'", Request["RentCd"], Request["RentSeq"]);
            SqlDataAdapter adap = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(strCmd, conn);
            adap.SelectCommand = cmd;
            DataTable dtbPDF = new DataTable();
            adap.Fill(dtbPDF);
            if (dtbPDF.Rows.Count > 0)
            {
                string FilePath = Server.MapPath(dtbPDF.Rows[0][4].ToString().Replace("\\", "//"));
                WebClient User = new WebClient();
                Byte[] FileBuffer = User.DownloadData(FilePath);
                if (FileBuffer != null)
                {
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }
            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('User has not upload contract file !')</script>");
            }
        }
    }
}