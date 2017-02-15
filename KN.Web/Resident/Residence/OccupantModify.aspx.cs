using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Config.Biz;
using KN.Resident.Biz;

namespace KN.Web.Resident.Residence
{
    public partial class OccupantModify : BasePage
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
                        Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
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
            bool isReturnOk = CommValue.AUTH_VALUE_TRUE;

            chkSameLessor.Enabled = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[Master.PARAM_DATA1] != null && Request.Params[Master.PARAM_DATA2] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) &&
                    !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                {
                    txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    txtHfRentSeq.Text = Request.Params[Master.PARAM_DATA2].ToString();

                    chkSameLessor.Enabled = CommValue.AUTH_VALUE_TRUE;

                    if (Request.Params[Master.PARAM_DATA3] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA3].ToString()))
                        {
                            txtHfUserSeq.Text = Request.Params[Master.PARAM_DATA3].ToString();
                        }
                    }
                }
                else
                {
                    isReturnOk = CommValue.AUTH_VALUE_FALSE;
                }
            }
            else
            {
                isReturnOk = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltFloor.Text = TextNm["BLD"];
            
            ltGender.Text = TextNm["GENDER"];
            ltNm.Text = TextNm["NAME"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltMobileNo.Text = TextNm["MOBILENO"];
            ltTelNo.Text = TextNm["TEL"];
            ltOccupationDt.Text = TextNm["OCCUDT"];
            ltTaxCd.Text = TextNm["TAXCD"];
            ltTaxAddr.Text = TextNm["ADDR"];
            chkSameLessor.Text = TextNm["SAMETENANT"];

            ltMngAddon.Text = TextNm["MNGADDON"];
            ltAddonUser.Text = TextNm["COHABITANTNAME"];
            ltAddonoSex.Text = TextNm["GENDER"];
            ltAddonRelation.Text = TextNm["RELATIONSHIP"];

            ltMngCard.Text = TextNm["MNGCARD"];
            ltCardUser.Text = TextNm["USER"];
            ltRelation.Text = TextNm["RELATIONSHIP"];
            ltMngCardNo.Text = TextNm["MNGCARDNO"];

            //ltMngParkingCard.Text = TextNm["MNGPARKINGCARDNO"];
            //ltParkingCardNo.Text = TextNm["ParkingTagNo"];
            //ltCarNo.Text = TextNm["CARNO"];
            //ltCarTy.Text = TextNm["CARTY"];
            //ltInsParkingCardNo.Text = TextNm["ParkingTagNo"];
            //ltInsCarNo.Text = TextNm["CARNO"];
            //ltInsCarTy.Text = TextNm["CARTY"];
            //ltInsCardFee.Text = TextNm["CARDFEE"];

            imgbtnMngCardListInsert.OnClientClick = "javascript:return fnCheckCardValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            imgbtnMngAddonInsert.OnClientClick = "javascript:return fnCheckNameValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            //imgbtnMngParkingCardInsert.OnClientClick = "javascript:return fnCheckParkingValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "')";

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlAddonUser, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RELATION);

            CommCdRdoUtil.MakeSubCdRdoNoTitle(rdoSex, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_GENDER, RepeatDirection.Horizontal);

            CommCdRdoUtil.MakeSubCdRdoNoTitle(rdoGender, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_GENDER, RepeatDirection.Horizontal);

            CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlUser, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RELATION);

            //CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlCarTy, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_CARTY);

            rdoGender.SelectedValue = CommValue.GENDER_TYPE_VALUE_MALE;

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["CONF_MODIFY_ISSUE"] + "', '" + AlertNm["ALERT_INSERT_BLANK"] + "')";
            lnkbtnCancel.Text = TextNm["CANCEL"];
            lnkbtnCancel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_CANCEL_POST"] + "')";

            lnkbtnModify.Visible = Master.isModDelAuthOk;

            // 기존 임시 정보 삭제 처리하는 부분
            // KN_USP_RES_DELETE_TEMPUSERINFO_M00
            ResidentMngBlo.RemoveTempUserInfo();

            // 입주자 번호가 없을 경우 미등록 입주자임
            if (string.IsNullOrEmpty(txtHfUserSeq.Text))
            {
                // 임시회원 번호 생성하는 부분
                // KN_USP_RES_INSERT_TEMPUSERINFO_S00
                DataTable dtReturn = ResidentMngBlo.RegistryTempUserInfo(Session["CompCd"].ToString(), Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString());

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > 0)
                    {
                        txtHfTmpSeq.Text = dtReturn.Rows[0]["UserSeq"].ToString();
                    }
                }
            }
            //매매기준율환율정보
            ltTopBaseRate.Text = TextNm["BASERATE"];
            if (txtHfRentCd.Text.Equals("9000"))
            {
                KsystemCode.Visible = false;
                ltBirthDt.Text = "Commencing Date";
            }
            else
            {
                ltBirthDt.Text = TextNm["BIRTHDATE"];
            }

            LoadExchageDate();
        }

        /// <summary>
        /// 매매기준율환율정보
        /// </summary>
        protected void LoadExchageDate()
        {
            // 금일 환율정보가 없을 경우 환율등록 페이지로 이동시킬것.
            // KN_USP_MNG_SELECT_EXCHANGERATEINFO_S00
            DataTable dtReturn = ExchangeMngBlo.WatchExchangeRateInfo(txtHfRentCd.Text);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DongToDollar"].ToString()))
                    {
                        string strDong = dtReturn.Rows[0]["DongToDollar"].ToString();
                        ltRealBaseRate.Text = TextLib.MakeVietIntNo(double.Parse(strDong).ToString("###,##0")) + "&nbsp;" + TextNm["DONG"].ToString();
                        hfRealBaseRate.Text = dtReturn.Rows[0]["DongToDollar"].ToString();
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
            // 세션체크
            AuthCheckLib.CheckSession();

            if (string.IsNullOrEmpty(txtHfUserSeq.Text))
            {
                DataSet dsReturn = new DataSet();

                // KN_USP_RES_SELECT_USERINFO_S02
                dsReturn = ResidentMngBlo.SpreadUserTmpInfo(Int32.Parse(txtHfTmpSeq.Text));

                if (dsReturn != null)
                {
                    DataTable dtAddonInfo = dsReturn.Tables[0];

                    if (dtAddonInfo != null)
                    {
                        lvMngAddon.DataSource = dtAddonInfo;
                        lvMngAddon.DataBind();

                        txtAddonUser.Text = string.Empty;
                        ddlAddonUser.SelectedIndex = CommValue.NEMUSEQ_VALUE_LOGIN;
                        rdoSex.SelectedValue = CommValue.GENDER_TYPE_VALUE_MALE;
                    }

                    DataSet dsViewReturn = new DataSet();

                    // KN_USP_RES_SELECT_USERINFO_S03
                    dsViewReturn = ResidentMngBlo.WatchSalesUserView(txtHfUserSeq.Text, txtHfRentCd.Text, Int32.Parse(txtHfRentSeq.Text), Session["LangCd"].ToString());

                    if (dsViewReturn != null)
                    {
                        if (!IsPostBack)
                        {
                            DataTable dtMainInfo = dsViewReturn.Tables[0];

                            if (dtMainInfo != null)
                            {
                                BindMainInfo(dtMainInfo);
                            }
                        }
                    }

                    DataTable dtAccessInfo = dsReturn.Tables[1];

                    if (dtAccessInfo != null)
                    {
                        lvMngCardList.DataSource = dtAccessInfo;
                        lvMngCardList.DataBind();

                        txtUserNm.Text = string.Empty;
                        txtMngCardNo.Text = string.Empty;

                        ddlUser.SelectedIndex = CommValue.NEMUSEQ_VALUE_LOGIN;
                    }

                    DataTable dtParkingInfo = dsReturn.Tables[2];

                    //if (dtParkingInfo != null)
                    //{
                    //    lvMngParkingCard.DataSource = dtParkingInfo;
                    //    lvMngParkingCard.DataBind();

                    //    txtParkingCardNo.Text = string.Empty;
                    //    txtCarNo.Text = string.Empty;

                    //    ddlCarTy.SelectedIndex = CommValue.NEMUSEQ_VALUE_LOGIN;
                    //}
                }

                txtOccupationDt.Text = hfOccupationDt.Value;
                txtBirthDt.Text = hfBirthDt.Value;
            }
            else
            {
                // KN_USP_RES_SELECT_USERINFO_S03
                DataSet dsReturn = ResidentMngBlo.WatchSalesUserView(txtHfUserSeq.Text, txtHfRentCd.Text, Int32.Parse(txtHfRentSeq.Text), Session["LangCd"].ToString());

                if (dsReturn != null)
                {
                    if (!IsPostBack)
                    {
                        DataTable dtMainInfo = dsReturn.Tables[0];

                        if (dtMainInfo != null)
                        {
                            BindMainInfo(dtMainInfo);
                        }
                    }

                    DataTable dtAddonInfo = dsReturn.Tables[1];

                    if (dtAddonInfo != null)
                    {
                        lvMngAddon.DataSource = dtAddonInfo;
                        lvMngAddon.DataBind();

                        txtAddonUser.Text = string.Empty;
                        ddlAddonUser.SelectedIndex = CommValue.NEMUSEQ_VALUE_LOGIN;
                        rdoSex.SelectedValue = CommValue.GENDER_TYPE_VALUE_MALE;
                    }

                    DataTable dtAccessInfo = dsReturn.Tables[2];

                    if (dtAccessInfo != null)
                    {
                        lvMngCardList.DataSource = dtAccessInfo;
                        lvMngCardList.DataBind();

                        txtUserNm.Text = string.Empty;
                        txtMngCardNo.Text = string.Empty;
                        ddlUser.SelectedIndex = CommValue.NEMUSEQ_VALUE_LOGIN;
                    }
                }
            }

            txtOccupationDt.Text = hfOccupationDt.Value;
            txtBirthDt.Text = hfBirthDt.Value;
        }

        protected void BindMainInfo(DataTable dtParam)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            if (dtParam.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtParam.Rows[0]["FloorNo"].ToString()))
                {
                    if (txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APT) || txtHfRentCd.Text.Equals(CommValue.RENTAL_VALUE_APTSHOP))
                    {
                        ltInsFloor.Text = dtParam.Rows[0]["RentNm"].ToString();
                    }
                    else
                    {
                        ltInsFloor.Text = dtParam.Rows[0]["FloorNo"].ToString();
                    }

                    txtHfFloor.Text = dtParam.Rows[0]["FloorNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["RentCd"].ToString()))
                {
                    txtHfRentCd.Text = dtParam.Rows[0]["RentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["RoomNo"].ToString()))
                {
                    ltInsRoomNo.Text = dtParam.Rows[0]["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserNm"].ToString()))
                {
                    txtNm.Text =  TextLib.StringDecoder(dtParam.Rows[0]["UserNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserMobileTyCd"].ToString()))
                {
                    txtMobileFrontNo.Text = dtParam.Rows[0]["UserMobileTyCd"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserMobileFrontNo"].ToString()))
                {
                    txtMobileMidNo.Text = dtParam.Rows[0]["UserMobileFrontNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserMobileRearNo"].ToString()))
                {
                    txtMobileRearNo.Text = dtParam.Rows[0]["UserMobileRearNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserTelTyCd"].ToString()))
                {
                    txtTelFrontNo.Text = dtParam.Rows[0]["UserTelTyCd"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserTelFrontNo"].ToString()))
                {
                    txtTelMidNo.Text = dtParam.Rows[0]["UserTelFrontNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserTelRearNo"].ToString()))
                {
                    txtTelRearNo.Text = dtParam.Rows[0]["UserTelRearNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["BirthDt"].ToString()))
                {
                    txtBirthDt.Text = TextLib.MakeDateEightDigit(dtParam.Rows[0]["BirthDt"].ToString());
                    hfBirthDt.Value = TextLib.MakeDateEightDigit(dtParam.Rows[0]["BirthDt"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["Gender"].ToString()))
                {
                    if (dtParam.Rows[0]["Gender"].ToString().Equals(CommValue.GENDER_TYPE_TEXT_MALE))
                    {
                        rdoGender.SelectedValue = CommValue.GENDER_TYPE_VALUE_MALE;
                    }
                    else
                    {
                        rdoGender.SelectedValue = CommValue.GENDER_TYPE_VALUE_FEMALE;
                    }
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["OccupationDt"].ToString()))
                {
                    txtOccupationDt.Text = TextLib.MakeDateEightDigit(dtParam.Rows[0]["OccupationDt"].ToString());
                    hfOccupationDt.Value = TextLib.MakeDateEightDigit(dtParam.Rows[0]["OccupationDt"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserTaxCd"].ToString()))
                {
                    txtTaxCd.Text = dtParam.Rows[0]["UserTaxCd"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserAddr"].ToString()))
                {
                    txtTaxAddr.Text =  TextLib.StringDecoder(dtParam.Rows[0]["UserAddr"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserDetAddr"].ToString()))
                {
                    txtTaxDetAddr.Text =  TextLib.StringDecoder(dtParam.Rows[0]["UserDetAddr"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["KSys_CustCd"].ToString()))
                {
                    txtKsystemCode.Text = dtParam.Rows[0]["KSys_CustCd"].ToString();
                }
            }
        }

        /// <summary>
        /// 임대자와 동일 체크박스 처리 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkSameLessor_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSameLessor.Checked)
                {
                    DataTable dtReturn = new DataTable();

                    if (!string.IsNullOrEmpty(txtHfRentCd.Text) && !string.IsNullOrEmpty(txtHfRentSeq.Text))
                    {
                        // 임대구분코드 및 임대구분순번 입력받아서 임대자 관련 정보 가져오는 부분
                        // KN_USP_RES_SELECT_SALESINFO_S04
                        dtReturn = ResidentMngBlo.WatchOccpantInfo(txtHfRentCd.Text, Int32.Parse(txtHfRentSeq.Text));

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count > 0)
                            {
                                ltFloor.Text = TextNm["FLOOR"];
                                ltInsFloor.Text = dtReturn.Rows[0]["FloorNo"].ToString();
                                ltInsRoomNo.Text = dtReturn.Rows[0]["RoomNo"].ToString();
                                txtNm.Text =  TextLib.StringDecoder(dtReturn.Rows[0]["TenantNm"].ToString());
                                txtMobileFrontNo.Text = dtReturn.Rows[0]["UserMobileTyCd"].ToString();
                                txtMobileMidNo.Text = dtReturn.Rows[0]["UserMobileFrontNo"].ToString();
                                txtMobileRearNo.Text = dtReturn.Rows[0]["UserMobileRearNo"].ToString();
                                txtTelFrontNo.Text = dtReturn.Rows[0]["TenantTelNo"].ToString();
                                txtTelMidNo.Text = dtReturn.Rows[0]["TenantTelFrontNo"].ToString();
                                txtTelRearNo.Text = dtReturn.Rows[0]["TenantTelMidNo"].ToString();
                                txtOccupationDt.Text = TextLib.MakeDateEightDigit(dtReturn.Rows[0]["RentStartDt"].ToString());
                                hfOccupationDt.Value = TextLib.MakeDateEightDigit(dtReturn.Rows[0]["RentStartDt"].ToString());
                                txtTaxCd.Text = dtReturn.Rows[0]["TaxCd"].ToString();
                                txtTaxAddr.Text =  TextLib.StringDecoder(dtReturn.Rows[0]["Addr"].ToString());
                                txtTaxDetAddr.Text =  TextLib.StringDecoder(dtReturn.Rows[0]["DetailAddr"].ToString());
                            }
                        }
                    }
                }

                txtOccupationDt.Text = hfOccupationDt.Value;
                txtBirthDt.Text = hfBirthDt.Value;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngAddon_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvMngAddon_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["UserDetSeq"].ToString()))
                {
                    TextBox txtUserDetSeq = (TextBox)iTem.FindControl("txtUserDetSeq");
                    txtUserDetSeq.Text = drView["UserDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AccessNm"].ToString()))
                {
                    TextBox txtAddonUser = (TextBox)iTem.FindControl("txtAddonUser");
                    txtAddonUser.Text =  TextLib.StringDecoder(drView["AccessNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RelationCd"].ToString()))
                {
                    DropDownList ddlAddonUser = (DropDownList)iTem.FindControl("ddlAddonUser");

                    CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlAddonUser, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RELATION);

                    ddlAddonUser.SelectedValue = drView["RelationCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["Gender"].ToString()))
                {
                    RadioButtonList rdoSex = (RadioButtonList)iTem.FindControl("rdoSex");

                    CommCdRdoUtil.MakeSubCdRdoNoTitle(rdoSex, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_GENDER, RepeatDirection.Horizontal);

                    if (drView["Gender"].ToString().Equals(CommValue.GENDER_TYPE_TEXT_MALE))
                    {
                        rdoSex.SelectedValue = CommValue.GENDER_TYPE_VALUE_MALE;
                    }
                    else
                    {
                        rdoSex.SelectedValue = CommValue.GENDER_TYPE_VALUE_FEMALE;
                    }
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
            }
        }

        protected void lvMngAddon_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtUserDetSeq = (TextBox)lvMngAddon.Items[e.ItemIndex].FindControl("txtUserDetSeq");
                TextBox txtAddonUser = (TextBox)lvMngAddon.Items[e.ItemIndex].FindControl("txtAddonUser");
                DropDownList ddlAddonUser = (DropDownList)lvMngAddon.Items[e.ItemIndex].FindControl("ddlAddonUser");
                RadioButtonList rdoSex = (RadioButtonList)lvMngAddon.Items[e.ItemIndex].FindControl("rdoSex");

                if (!string.IsNullOrEmpty(txtUserDetSeq.Text) &&
                    !string.IsNullOrEmpty(txtAddonUser.Text))
                {
                    string strGender = string.Empty;
                    string strUserSeq = string.Empty;
                    string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    if (rdoSex.SelectedValue.Equals(CommValue.GENDER_TYPE_VALUE_MALE))
                    {
                        strGender = CommValue.GENDER_TYPE_TEXT_MALE;
                    }
                    else
                    {
                        strGender = CommValue.GENDER_TYPE_TEXT_FEMALE;
                    }

                    if (string.IsNullOrEmpty(txtHfUserSeq.Text))
                    {
                        strUserSeq = txtHfTmpSeq.Text;

                        // KN_USP_RES_UPDATE_USERADDONINFO_M00
                        ResidentMngBlo.ModifyUserAddonTmpInfo(Int32.Parse(strUserSeq), Int32.Parse(txtUserDetSeq.Text), txtAddonUser.Text, strGender, ddlAddonUser.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                    }
                    else
                    {
                        strUserSeq = txtHfUserSeq.Text;

                        // KN_USP_RES_UPDATE_USERADDONINFO_M01
                        ResidentMngBlo.ModifyUserAddonInfo(strUserSeq, Int32.Parse(txtUserDetSeq.Text), txtAddonUser.Text, strGender, ddlAddonUser.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                    }
                }
                else
                {
                    hfAlertText.Value = AlertNm["ALERT_INSERT_BLANK"];
                }

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngAddon_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strUserSeq = txtHfTmpSeq.Text;

                TextBox txtUserDetSeq = (TextBox)lvMngAddon.Items[e.ItemIndex].FindControl("txtUserDetSeq");

                if (string.IsNullOrEmpty(txtHfUserSeq.Text))
                {
                    // KN_USP_RES_DELETE_USERADDONINFO_M00
                    ResidentMngBlo.RemoveUserAddonTmpInfo(Int32.Parse(strUserSeq), Int32.Parse(txtUserDetSeq.Text));
                }
                else
                {
                    // KN_USP_RES_DELETE_USERADDONINFO_M01
                    ResidentMngBlo.RemoveUserAddonInfo(txtHfUserSeq.Text, Int32.Parse(txtUserDetSeq.Text));
                }

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngCardList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvMngCardList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["UserDetSeq"].ToString()))
                {
                    TextBox txtUserDetSeq = (TextBox)iTem.FindControl("txtUserDetSeq");
                    txtUserDetSeq.Text = drView["UserDetSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AccessNm"].ToString()))
                {
                    TextBox txtUserNm = (TextBox)iTem.FindControl("txtUserNm");
                    txtUserNm.Text =  TextLib.StringDecoder(drView["AccessNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RelationCd"].ToString()))
                {
                    DropDownList ddlUser = (DropDownList)iTem.FindControl("ddlUser");

                    CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlUser, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RELATION);

                    ddlUser.SelectedValue = drView["RelationCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["AccessCardNo"].ToString()))
                {
                    TextBox txtMngCardNo = (TextBox)iTem.FindControl("txtMngCardNo");
                    txtMngCardNo.Text = drView["AccessCardNo"].ToString();
                }

                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
            }
        }

        protected void lvMngCardList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                TextBox txtUserDetSeq = (TextBox)lvMngCardList.Items[e.ItemIndex].FindControl("txtUserDetSeq");
                TextBox txtUserNm = (TextBox)lvMngCardList.Items[e.ItemIndex].FindControl("txtUserNm");
                DropDownList ddlUser = (DropDownList)lvMngCardList.Items[e.ItemIndex].FindControl("ddlUser");
                TextBox txtMngCardNo = (TextBox)lvMngCardList.Items[e.ItemIndex].FindControl("txtMngCardNo");

                if (!string.IsNullOrEmpty(txtUserDetSeq.Text) &&
                    !string.IsNullOrEmpty(txtUserNm.Text) &&
                    !string.IsNullOrEmpty(txtMngCardNo.Text))
                {
                    string strUserSeq = txtHfTmpSeq.Text;
                    string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    if (string.IsNullOrEmpty(txtHfUserSeq.Text))
                    {
                        strUserSeq = txtHfTmpSeq.Text;

                        // KN_USP_RES_UPDATE_USERACCESSINFO_M00
                        ResidentMngBlo.ModifyUserAccessTmpInfo(Int32.Parse(strUserSeq), Int32.Parse(txtUserDetSeq.Text), txtMngCardNo.Text, txtUserNm.Text, ddlUser.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                    }
                    else
                    {
                        strUserSeq = txtHfUserSeq.Text;

                        // KN_USP_RES_UPDATE_USERACCESSINFO_M01
                        ResidentMngBlo.ModifyUserAccessInfo(strUserSeq, Int32.Parse(txtUserDetSeq.Text), txtMngCardNo.Text, txtUserNm.Text, ddlUser.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                    }
                }
                else
                {
                    hfAlertText.Value = AlertNm["ALERT_INSERT_BLANK"];
                }

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMngCardList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strUserSeq = txtHfTmpSeq.Text;

                TextBox txtUserDetSeq = (TextBox)lvMngCardList.Items[e.ItemIndex].FindControl("txtUserDetSeq");

                if (string.IsNullOrEmpty(txtHfUserSeq.Text))
                {
                    // KN_USP_RES_DELETE_USERACCESSINFO_M00
                    ResidentMngBlo.RemoveUserAccessTmpInfo(Int32.Parse(strUserSeq), Int32.Parse(txtUserDetSeq.Text));
                }
                else
                {
                    // KN_USP_RES_DELETE_USERACCESSINFO_M01
                    ResidentMngBlo.RemoveUserAccessInfo(txtHfUserSeq.Text, Int32.Parse(txtUserDetSeq.Text));
                }

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnMngAddonInsert_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strGender = string.Empty;
                string strUserSeq = txtHfTmpSeq.Text;
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                if (rdoSex.SelectedValue.Equals(CommValue.GENDER_TYPE_VALUE_MALE))
                {
                    strGender = CommValue.GENDER_TYPE_TEXT_MALE;
                }
                else
                {
                    strGender = CommValue.GENDER_TYPE_TEXT_FEMALE;
                }

                if (string.IsNullOrEmpty(txtHfUserSeq.Text))
                {
                    // KN_USP_RES_INSERT_USERADDONINFO_M00
                    ResidentMngBlo.RegistryUserAddonTmpInfo(Int32.Parse(strUserSeq), txtAddonUser.Text, strGender, ddlAddonUser.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                }
                else
                {
                    // KN_USP_RES_INSERT_USERADDONINFO_M01
                    ResidentMngBlo.RegistryUserAddonInfo(txtHfUserSeq.Text, txtAddonUser.Text, strGender, ddlAddonUser.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                }

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnMngCardListInsert_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strUserSeq = txtHfTmpSeq.Text;
                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                if (string.IsNullOrEmpty(txtHfUserSeq.Text))
                {
                    // KN_USP_RES_INSERT_USERACCESSINFO_M00
                    ResidentMngBlo.RegistryUserAccessTmpInfo(Int32.Parse(strUserSeq), txtMngCardNo.Text, txtUserNm.Text, ddlUser.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                }
                else
                {
                    // KN_USP_RES_INSERT_USERACCESSINFO_M01
                    ResidentMngBlo.RegistryUserAccessInfo(txtHfUserSeq.Text, txtMngCardNo.Text, txtUserNm.Text, ddlUser.SelectedValue, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                }

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intTmpUserSeq = 0;
                int intRentSeq;
                string strOccuptionDt = string.Empty;
                string strBirthDt = string.Empty;
                string strGender = string.Empty;
                string strIP = Request.ServerVariables["REMOTE_ADDR"];
                

                if (string.IsNullOrEmpty(txtHfRentSeq.Text))
                {
                    intRentSeq = 0;
                }
                else
                {
                    intRentSeq = Int32.Parse(txtHfRentSeq.Text);
                }

                if (!string.IsNullOrEmpty(hfOccupationDt.Value))
                {
                    strOccuptionDt = txtOccupationDt.Text.Replace("-", "").Replace(".", "");
                }

                if (!string.IsNullOrEmpty(hfBirthDt.Value))
                {
                    strBirthDt = hfBirthDt.Value.Replace("-", "").Replace(".", "");
                }

                if (rdoGender.SelectedValue.Equals(CommValue.GENDER_TYPE_VALUE_MALE))
                {
                    strGender = CommValue.GENDER_TYPE_TEXT_MALE;
                }
                else
                {
                    strGender = CommValue.GENDER_TYPE_TEXT_FEMALE;
                }                 

                // KN_USP_RES_SELECT_USERINFO_S03
                var dsReturnRentSeq = new DataSet();
                dsReturnRentSeq = ResidentMngBlo.SelectRentSeqUserInfo(txtHfUserSeq.Text, txtHfRentCd.Text, Int32.Parse(txtHfRentSeq.Text));


                if (string.IsNullOrEmpty(txtHfUserSeq.Text))
                    {
                        intTmpUserSeq = Int32.Parse(txtHfTmpSeq.Text);

                        // 거주자 정보 등록 (사실상 등록 개념은 없음)
                        // KN_USP_RES_INSERT_USERINFO_M00
                        ResidentMngBlo.RegistryUserMng(intTmpUserSeq, Int32.Parse(txtHfFloor.Text), ltInsRoomNo.Text, txtHfRentCd.Text, intRentSeq, txtNm.Text, strOccuptionDt, 
                                                       strBirthDt, strGender, txtTelFrontNo.Text, txtTelMidNo.Text, txtTelRearNo.Text, txtMobileFrontNo.Text, txtMobileMidNo.Text, 
                                                       txtMobileRearNo.Text, txtTaxCd.Text, txtTaxAddr.Text, txtTaxDetAddr.Text, Session["CompCd"].ToString(), 
                                                       Session["MemNo"].ToString(), strIP,txtKsystemCode.Text);
                   }
                   
                else
                {
                    // 거주자 정보 수정
                    // KN_USP_RES_UPDATE_USERINFO_M00
                    ResidentMngBlo.ModifyUserInfo(txtHfUserSeq.Text, Int32.Parse(txtHfFloor.Text), ltInsRoomNo.Text, txtHfRentCd.Text, intRentSeq, txtNm.Text, strOccuptionDt,
                                                  strBirthDt, strGender, txtTelFrontNo.Text, txtTelMidNo.Text, txtTelRearNo.Text, txtMobileFrontNo.Text, txtMobileMidNo.Text,
                                                  txtMobileRearNo.Text, txtTaxCd.Text, txtTaxAddr.Text, txtTaxDetAddr.Text, Session["CompCd"].ToString(), 
                                                  Session["MemNo"].ToString(), strIP,txtKsystemCode.Text);


                    // KSystem에 거래처 정보 생성
                    // KN_USP_RES_INSERT_USERKSYSMATCHINFO_M00
                    // KN_USP_RES_INSERT_USERKSYSMATCHINFO_M01
                    //ResidentMngBlo.RegistryUserKSysMatchInfo(txtHfUserSeq.Text);
                }

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Request.Params[Master.PARAM_DATA1].ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Request.Params[Master.PARAM_DATA1].ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}