using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Config.Biz;
using KN.Manage.Biz;
using KN.Resident.Biz;

namespace KN.Web.Management.Remote
{
    public partial class ChargeSettingListForTowerWithHVAC : BasePage
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

                        LoadData();
                    }
                    else
                    {
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
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
            Master.PARAM_DATA1 = "RentCd";
            Master.PARAM_DATA2 = "ChargeTy";
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
            {
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1];
                txtHfChargeTy.Text = Request.Params[Master.PARAM_DATA2];
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltCommon.Text = TextNm["GENERAL"];
            ltFirstSeq.Text = TextNm["SEQ"];
            ltFirstGenCharge.Text = TextNm["AVERAGEHOUR"];
            ltFirstPeakCharge.Text = TextNm["RUSHHOUR"];
            ltFirstNightCharge.Text = TextNm["LOWHOUR"];
            ltFirstStartDt.Text = TextNm["APPLYDT"];

            ltIndividual.Text = TextNm["INDIVIDUAL"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltSecondSeq.Text = TextNm["SEQ"];
            ltSecondGenCharge.Text = TextNm["AVERAGEHOUR"];
            ltSecondPeakCharge.Text = TextNm["RUSHHOUR"];
            ltSecondNightCharge.Text = TextNm["LOWHOUR"];
            ltSecondACCharge.Text = TextNm["AC"];
            ltSecondStartDt.Text = TextNm["APPLYDT"];

            //------ Check IsNumeric for not Waterate ------------------------------------------
            if (!string.IsNullOrEmpty(txtHfChargeTy.Text) && txtHfChargeTy.Text == "0002")
            {
                
            }
            else if (!string.IsNullOrEmpty(txtHfChargeTy.Text) && txtHfChargeTy.Text == "0003")
            {
                
            }
            //if (!string.IsNullOrEmpty(txtHfChargeTy.Text) && txtHfChargeTy.Text != "0002")
            else
            {
                txtFirstGenCharge.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                txtFirstPeakCharge.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                txtFirstNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

                txtSecondGenCharge.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                txtSecondPeakCharge.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
                txtSecondNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
            }

            imgbtnRegist.OnClientClick = "javascript:return fnFirstCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnRegist.Visible = Master.isWriteAuthOk;

            imgbtnInput.OnClientClick = "javascript:return fnSecondCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";
            imgbtnInput.Visible = Master.isWriteAuthOk;

            imgbtnSearchCompNm.OnClientClick = "javascript:return fnChangePopup('" + txtTitle.ClientID + "', '" + txtRoomNo.ClientID + "', '" + HfReturnUserSeqId.ClientID + "', '" + txtTitle.Text + "', '" + txtHfRentCd.Text + "');";

            //매매기준율환율정보
            ltFirstBaseRate.Text = TextNm["BASERATE"];
            LoadExchageDate();
        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // 가장 최근의 환율을 조회함.
            var dtReturn = ExchangeMngBlo.WatchExchangeRateLastInfo(CommValue.RENTAL_VALUE_PARKING);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0") + "&nbsp;" + TextNm["DONG"].ToString());
                        hfRealBaseRate.Value = dtReturn.Rows[0]["DongToDollar"].ToString();
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

        protected void LoadData()
        {
            // KN_USP_MNG_SELECT_UTILCHARGEINFO_S09
            var dsListReturn = RemoteMngBlo.SelectUtilChargeSetList(txtHfRentCd.Text, txtHfChargeTy.Text);

            if (dsListReturn == null) return;
            lvChargeInfoList.DataSource = dsListReturn.Tables[0];
            lvChargeInfoList.DataBind();

            lvChargelistForRoom.DataSource = dsListReturn.Tables[1];
            lvChargelistForRoom.DataBind();
        }

        protected void LoadRemoteData()
        {
            DataTable dtListReturn = new DataTable();

            // KN_USP_MNG_SELECT_UTILCHARGEINFO_S05
            dtListReturn = RemoteMngBlo.SelectUtilChargeSetAddonList(txtHfRentCd.Text, txtRoomNo.Text, txtHfChargeTy.Text);

            if (dtListReturn != null)
            {
                lvChargelistForRoom.DataSource = dtListReturn;
                lvChargelistForRoom.DataBind();
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvChargeInfoList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvChargeInfoList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["ChargeSeq"].ToString()))
                {
                    Literal ltSeq = (Literal)iTem.FindControl("ltSeq");
                    ltSeq.Text = drView["ChargeSeq"].ToString();

                    TextBox txtHfChargeSeq = (TextBox)iTem.FindControl("txtHfChargeSeq");
                    txtHfChargeSeq.Text = drView["ChargeSeq"].ToString();
                }

                //---------------------------------------------- Format for common water rate -------------------------------------------------------------
                if (txtHfChargeTy.Text == "0002")
                {
                    if (!string.IsNullOrEmpty(drView["Normal"].ToString()))
                    {
                        var txtGenCharge = (TextBox)iTem.FindControl("txtGenCharge");
                        txtGenCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtGenCharge.Text = double.Parse(drView["Normal"].ToString()).ToString("###,##0.##");
                    }

                    if (!string.IsNullOrEmpty(drView["Height"].ToString()))
                    {
                        TextBox txtPeakCharge = (TextBox)iTem.FindControl("txtPeakCharge");
                        txtPeakCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtPeakCharge.Text = double.Parse(drView["Height"].ToString()).ToString("###,##0.##");
                    }

                    if (!string.IsNullOrEmpty(drView["Low"].ToString()))
                    {
                        TextBox txtNightCharge = (TextBox)iTem.FindControl("txtNightCharge");
                        txtNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtNightCharge.Text = double.Parse(drView["Low"].ToString()).ToString("###,##0.##");
                    }
                }
                //---------------------------------------------- Format for common gas rate -------------------------------------------------------------
                else if (txtHfChargeTy.Text == "0003")
                {
                    if (!string.IsNullOrEmpty(drView["Normal"].ToString()))
                    {
                        var txtGenCharge = (TextBox)iTem.FindControl("txtGenCharge");
                        txtGenCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtGenCharge.Text = double.Parse(drView["Normal"].ToString()).ToString("###,##0.##");
                    }

                    if (!string.IsNullOrEmpty(drView["Height"].ToString()))
                    {
                        TextBox txtPeakCharge = (TextBox)iTem.FindControl("txtPeakCharge");
                        txtPeakCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtPeakCharge.Text = double.Parse(drView["Height"].ToString()).ToString("###,##0.##");
                    }

                    if (!string.IsNullOrEmpty(drView["Low"].ToString()))
                    {
                        TextBox txtNightCharge = (TextBox)iTem.FindControl("txtNightCharge");
                        txtNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtNightCharge.Text = double.Parse(drView["Low"].ToString()).ToString("###,##0.##");
                    }
                }
                else 
                {

                    if (!string.IsNullOrEmpty(drView["Normal"].ToString()))
                    {
                        var txtGenCharge = (TextBox)iTem.FindControl("txtGenCharge");
                        txtGenCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtGenCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["Normal"].ToString()).ToString("###,##0"));
                    }

                    if (!string.IsNullOrEmpty(drView["Height"].ToString()))
                    {
                        TextBox txtPeakCharge = (TextBox)iTem.FindControl("txtPeakCharge");
                        txtPeakCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtPeakCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["Height"].ToString()).ToString("###,##0"));
                    }

                    if (!string.IsNullOrEmpty(drView["Low"].ToString()))
                    {
                        TextBox txtNightCharge = (TextBox)iTem.FindControl("txtNightCharge");
                        txtNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                        txtNightCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["Low"].ToString()).ToString("###,##0"));
                    }
                }
                
                
                //--------------------------------------------------------------------------------------------------------------------------------------
              
                if (!string.IsNullOrEmpty(drView["ChargeStartDt"].ToString()))
                {
                    string strDate = drView["ChargeStartDt"].ToString();
                    var txtStartDt = (TextBox)iTem.FindControl("txtStartDt");
                    txtStartDt.Text = TextLib.MakeDateEightDigit(strDate);
                }

                var imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.Visible = Master.isModDelAuthOk;
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                var imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.Visible = Master.isModDelAuthOk;
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
            }
        }

        protected void imgbtnRegist_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                double dblGenCharge = CommValue.NUMBER_VALUE_0_00;
                double dblPeakCharge = CommValue.NUMBER_VALUE_0_00;
                double dblNightCharge = CommValue.NUMBER_VALUE_0_00;

                if (!string.IsNullOrEmpty(txtFirstGenCharge.Text))
                {
                    dblGenCharge = double.Parse(txtFirstGenCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtFirstPeakCharge.Text))
                {
                    dblPeakCharge = double.Parse(txtFirstPeakCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtFirstNightCharge.Text))
                {
                    dblNightCharge = double.Parse(txtFirstNightCharge.Text);
                }

                var strInsDt = txtFirstStartDt.Text.Replace("-", "");

                // KN_USP_MNG_INSERT_UTILCHARGEINFO_M06
                RemoteMngBlo.RegistryUtilChargeInfoCommon(txtHfRentCd.Text, txtHfChargeTy.Text,0, dblGenCharge, dblPeakCharge, dblNightCharge, strInsDt);
                ResetInput();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargeInfoList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var txtHfChargeSeq = (TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtHfChargeSeq");
                var intChargeSeq = Int32.Parse(txtHfChargeSeq.Text);

                // KN_USP_MNG_DELETE_UTILCHARGEINFO_M04
                RemoteMngBlo.RemoveUtilCommonInfo(txtHfRentCd.Text, txtHfChargeTy.Text, intChargeSeq);
                LoadData();
               // Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargeInfoList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var txtHfChargeSeq = (TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtHfChargeSeq");
                var intChargeSeq = Int32.Parse(txtHfChargeSeq.Text);

                var dblGenCharge = CommValue.NUMBER_VALUE_0_0;
                var dblPeakCharge = CommValue.NUMBER_VALUE_0_0;
                var dblNightCharge = CommValue.NUMBER_VALUE_0_0;

                //---------------------------------------------- Format for common water rate -------------------------------------------------------------
                if (txtHfChargeTy.Text == "0002")
                {
                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                    {
                        dblGenCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                    {
                        dblPeakCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                    {
                        dblNightCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text);
                    }
                }
                //---------------------------------------------- Format for common gas rate -------------------------------------------------------------
                else if (txtHfChargeTy.Text == "0003")
                {
                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                    {
                        dblGenCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                    {
                        dblPeakCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                    {
                        dblNightCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                    {
                        dblGenCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text.Replace(".", ""));
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                    {
                        dblPeakCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text.Replace(".", ""));
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                    {
                        dblNightCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text.Replace(".", ""));
                    }
                }

                //if (txtHfChargeTy.Text != "0002")
                //{
                //    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                //    {
                //        dblGenCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text.Replace(".", ""));
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                //    {
                //        dblPeakCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text.Replace(".", ""));
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                //    {
                //        dblNightCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text.Replace(".", ""));
                //    }
                //}
                //else
                //{
                //    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                //    {
                //        dblGenCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtGenCharge")).Text);
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                //    {
                //        dblPeakCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text);
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                //    {
                //        dblNightCharge = double.Parse(((TextBox)lvChargeInfoList.Items[e.ItemIndex].FindControl("txtNightCharge")).Text);
                //    }
                //}

                // KN_USP_MNG_INSERT_UTILCHARGEINFO_M06
                RemoteMngBlo.RegistryUtilChargeInfoCommon(txtHfRentCd.Text, txtHfChargeTy.Text, intChargeSeq, dblGenCharge, dblPeakCharge, dblNightCharge, "99999999");
                LoadData();
               // Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text + "&" + Master.PARAM_DATA2 + "=" + txtHfChargeTy.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargelistForRoom_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvChargelistForRoom_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (!e.Item.ItemType.Equals(ListViewItemType.DataItem)) return;
            var iTem = (ListViewDataItem)e.Item;
            var drView = (DataRowView)iTem.DataItem;

            if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
            {
                var ltRoomNo = (Literal)iTem.FindControl("ltRoomNo");
                ltRoomNo.Text = drView["RoomNo"].ToString();
            }

            if (!string.IsNullOrEmpty(drView["ChargeSeq"].ToString()))
            {
                var ltSeq = (Literal)iTem.FindControl("ltSeq");
                ltSeq.Text = drView["ChargeSeq"].ToString();

                var txtHfChargeSeq = (TextBox)iTem.FindControl("txtHfChargeSeq");
                txtHfChargeSeq.Text = drView["ChargeSeq"].ToString();
                var txtHfUserSeq = (TextBox)iTem.FindControl("txthfUserSeq");
                txtHfUserSeq.Text = drView["UserSeq"].ToString();
            }

            //---------------------------------------------- Format for Individual water rate -------------------------------------------------------------
            if (txtHfChargeTy.Text == "0002")
            {
                if (!string.IsNullOrEmpty(drView["Normal"].ToString()))
                {
                    var txtGenCharge = (TextBox)iTem.FindControl("txtGenCharge");
                    txtGenCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtGenCharge.Text = double.Parse(drView["Normal"].ToString()).ToString("###,##0.##");
                }

                if (!string.IsNullOrEmpty(drView["Height"].ToString()))
                {
                    var txtPeakCharge = (TextBox)iTem.FindControl("txtPeakCharge");
                    txtPeakCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtPeakCharge.Text = double.Parse(drView["Height"].ToString()).ToString("###,##0.##");
                }

                if (!string.IsNullOrEmpty(drView["Low"].ToString()))
                {
                    var txtNightCharge = (TextBox)iTem.FindControl("txtNightCharge");
                    txtNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtNightCharge.Text = double.Parse(drView["Low"].ToString()).ToString("###,##0.##");
                }


                if (!string.IsNullOrEmpty(drView["OverTime"].ToString()))
                {
                    var txtACCharge = (TextBox)iTem.FindControl("txtACCharge");
                    txtACCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtACCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["OverTime"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["Other"].ToString()))
                {
                    var txtOther = (TextBox)iTem.FindControl("txtOther");
                    txtOther.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtOther.Text = double.Parse(drView["Other"].ToString()).ToString("###,##0.##");
                }
            }
            //---------------------------------------------- Format for Individual gas rate -------------------------------------------------------------
            else if (txtHfChargeTy.Text == "0003")
            {
                if (!string.IsNullOrEmpty(drView["Normal"].ToString()))
                {
                    var txtGenCharge = (TextBox)iTem.FindControl("txtGenCharge");
                    txtGenCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtGenCharge.Text = double.Parse(drView["Normal"].ToString()).ToString("###,##0.##");
                }

                if (!string.IsNullOrEmpty(drView["Height"].ToString()))
                {
                    var txtPeakCharge = (TextBox)iTem.FindControl("txtPeakCharge");
                    txtPeakCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtPeakCharge.Text = double.Parse(drView["Height"].ToString()).ToString("###,##0.##");
                }

                if (!string.IsNullOrEmpty(drView["Low"].ToString()))
                {
                    var txtNightCharge = (TextBox)iTem.FindControl("txtNightCharge");
                    txtNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtNightCharge.Text = double.Parse(drView["Low"].ToString()).ToString("###,##0.##");
                }


                if (!string.IsNullOrEmpty(drView["OverTime"].ToString()))
                {
                    var txtACCharge = (TextBox)iTem.FindControl("txtACCharge");
                    txtACCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtACCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["OverTime"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["Other"].ToString()))
                {
                    var txtOther = (TextBox)iTem.FindControl("txtOther");
                    txtOther.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtOther.Text = double.Parse(drView["Other"].ToString()).ToString("###,##0.##");
                }
            }
            else
            {

                if (!string.IsNullOrEmpty(drView["Normal"].ToString()))
                {
                    var txtGenCharge = (TextBox)iTem.FindControl("txtGenCharge");
                    txtGenCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtGenCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["Normal"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["Height"].ToString()))
                {
                    var txtPeakCharge = (TextBox)iTem.FindControl("txtPeakCharge");
                    txtPeakCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtPeakCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["Height"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["Low"].ToString()))
                {
                    var txtNightCharge = (TextBox)iTem.FindControl("txtNightCharge");
                    txtNightCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtNightCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["Low"].ToString()).ToString("###,##0"));
                }


                if (!string.IsNullOrEmpty(drView["OverTime"].ToString()))
                {
                    var txtACCharge = (TextBox)iTem.FindControl("txtACCharge");
                    txtACCharge.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtACCharge.Text = TextLib.MakeVietIntNo(double.Parse(drView["OverTime"].ToString()).ToString("###,##0"));
                }

                if (!string.IsNullOrEmpty(drView["Other"].ToString()))
                {
                    var txtOther = (TextBox)iTem.FindControl("txtOther");
                    txtOther.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                    txtOther.Text = TextLib.MakeVietIntNo(double.Parse(drView["Other"].ToString()).ToString("###,##0"));
                }
            }
            

            if (!string.IsNullOrEmpty(drView["ChargeStartDt"].ToString()))
            {
                var strDate = drView["ChargeStartDt"].ToString();
                var txtStartDt = (TextBox)iTem.FindControl("txtStartDt");
                txtStartDt.Text = TextLib.MakeDateEightDigit(strDate);
            }

            var imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
            imgbtnModify.Visible = Master.isModDelAuthOk;
            imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

            var imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
            imgbtnDelete.Visible = Master.isModDelAuthOk;
            imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
        }

        protected void imgbtnInput_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var dblGenCharge = CommValue.NUMBER_VALUE_0_00;
                var dblPeakCharge = CommValue.NUMBER_VALUE_0_00;
                var dblNightCharge = CommValue.NUMBER_VALUE_0_00;
                var overTime = CommValue.NUMBER_VALUE_0_00;
                var otherCharge = CommValue.NUMBER_VALUE_0_00;
                var strUserSeq = HfReturnUserSeqId.Value;

                if (!string.IsNullOrEmpty(txtSecondGenCharge.Text))
                {
                    dblGenCharge = double.Parse(txtSecondGenCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtSecondPeakCharge.Text))
                {
                    dblPeakCharge = double.Parse(txtSecondPeakCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtSecondNightCharge.Text))
                {
                    dblNightCharge = double.Parse(txtSecondNightCharge.Text);
                }

                if (!string.IsNullOrEmpty(txtSecondACCharge.Text))
                {
                    overTime = double.Parse(txtSecondACCharge.Text);
                }
                if (!string.IsNullOrEmpty(txtOtherInput.Text))
                {
                    otherCharge = double.Parse(txtOtherInput.Text);
                }

                var strInsDt = txtSecondStartDt.Text.Replace("-", "");

                // KN_USP_MNG_INSERT_UTILCHARGEINFO_M07
                RemoteMngBlo.InsertUtilChargeIndi(txtHfRentCd.Text, txtRoomNo.Text,strUserSeq, txtHfChargeTy.Text, 0, dblGenCharge, dblPeakCharge, dblNightCharge, overTime, otherCharge, strInsDt);
                ResetInput();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargelistForRoom_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();
                var intChargeSeq = CommValue.NUMBER_VALUE_0;
                var txtHfChargeSeq = (TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtHfChargeSeq");
                var txtHfUserSeq = (TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txthfUserSeq");
                var ltRoomNo = (Literal)lvChargelistForRoom.Items[e.ItemIndex].FindControl("ltRoomNo");

                if (!string.IsNullOrEmpty(txtHfChargeSeq.Text))
                {
                    intChargeSeq = Int32.Parse(txtHfChargeSeq.Text);
                }
                // KN_USP_MNG_DELETE_UTILCHARGEINFO_M01
                RemoteMngBlo.DeleteUtilChargeIndi(txtHfRentCd.Text, ltRoomNo.Text, txtHfUserSeq.Text, txtHfChargeTy.Text, intChargeSeq);
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvChargelistForRoom_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                var ltRoomNo = (Literal)lvChargelistForRoom.Items[e.ItemIndex].FindControl("ltRoomNo");
                var dblGenCharge = CommValue.NUMBER_VALUE_0_0;
                var dblPeakCharge = CommValue.NUMBER_VALUE_0_0;
                var dblNightCharge = CommValue.NUMBER_VALUE_0_0;
                var dboverTime = CommValue.NUMBER_VALUE_0_0;
                var dblOther = CommValue.NUMBER_VALUE_0_0;
                var chargeSeq = 0;
                var strUserSeq = "";


                if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txthfUserSeq")).Text))
                {
                    strUserSeq = ((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txthfUserSeq")).Text;
                }

                //---------------------------------------------- Format for Individual water rate -------------------------------------------------------------
                if (txtHfChargeTy.Text == "0002")
                {
                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                    {
                        dblGenCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                    {
                        dblPeakCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                    {
                        dblNightCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text))
                    {
                        dboverTime = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text.Replace(".", ""));
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text))
                    {
                        dblOther = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text);
                    }
                }
                //---------------------------------------------- Format for Individual gas rate -------------------------------------------------------------
                else if (txtHfChargeTy.Text == "0003")
                {
                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                    {
                        dblGenCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                    {
                        dblPeakCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                    {
                        dblNightCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text);
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text))
                    {
                        dboverTime = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text.Replace(".", ""));
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text))
                    {
                        dblOther = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                    {
                        dblGenCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text.Replace(".", ""));
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                    {
                        dblPeakCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text.Replace(".", ""));
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                    {
                        dblNightCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text.Replace(".", ""));
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text))
                    {
                        dboverTime = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text.Replace(".", ""));
                    }

                    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text))
                    {
                        dblOther = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text.Replace(".", ""));
                    }
                }

                //if (txtHfChargeTy.Text != "0002")
                //{
                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                //    {
                //        dblGenCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text.Replace(".", ""));
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                //    {
                //        dblPeakCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text.Replace(".", ""));
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                //    {
                //        dblNightCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text.Replace(".", ""));
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text))
                //    {
                //        dboverTime = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text.Replace(".", ""));
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text))
                //    {
                //        dblOther = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text.Replace(".", ""));
                //    }
                //}
                //else
                //{
                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text))
                //    {
                //        dblGenCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtGenCharge")).Text);
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text))
                //    {
                //        dblPeakCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtPeakCharge")).Text);
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text))
                //    {
                //        dblNightCharge = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtNightCharge")).Text);
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text))
                //    {
                //        dboverTime = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtACCharge")).Text.Replace(".", ""));
                //    }

                //    if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text))
                //    {
                //        dblOther = double.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtOther")).Text);
                //    }
                //}


                if (!string.IsNullOrEmpty(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtHfChargeSeq")).Text))
                {
                    chargeSeq = int.Parse(((TextBox)lvChargelistForRoom.Items[e.ItemIndex].FindControl("txtHfChargeSeq")).Text);
                }
                // KN_USP_MNG_UPDATE_UTILCHARGEINFO_M01
                RemoteMngBlo.InsertUtilChargeIndi(txtHfRentCd.Text, ltRoomNo.Text, strUserSeq, txtHfChargeTy.Text, chargeSeq, dblGenCharge, dblPeakCharge, dblNightCharge, dboverTime, dblOther, "99999999");
                ResetInput();
                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlRoomNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadRemoteData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ResetInput()
        {
            txtFirstGenCharge.Text = "";
            txtFirstNightCharge.Text = "";
            txtFirstPeakCharge.Text = "";
            txtFirstStartDt.Text = "";
            txtTitle.Text = "";
            HfReturnUserSeqId.Value = "";
            txtRoomNo.Text = "";
            txtSecondStartDt.Text = "";
            txtSecondACCharge.Text = "";
            txtSecondGenCharge.Text = "";
            txtSecondNightCharge.Text = "";
            txtSecondPeakCharge.Text = "";

        }

    }
}