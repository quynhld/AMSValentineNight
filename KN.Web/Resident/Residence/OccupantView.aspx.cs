using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Resident.Biz;

namespace KN.Web.Resident.Residence
{
    public partial class OccupantView : BasePage
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

        /// <summary>
        /// 매개변수 체크
        /// </summary>
        /// <returns></returns>
        protected bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_TRUE;

            if (Request.Params[Master.PARAM_DATA1] != null && Request.Params[Master.PARAM_DATA2] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()) &&
                    !string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA2].ToString()))
                {
                    txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    txtHfRentSeq.Text = Request.Params[Master.PARAM_DATA2].ToString();

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

        /// <summary>
        /// 컨트롤 초기화
        /// </summary>
        protected void InitControls()
        {
            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnList.Text = TextNm["LIST"];

            lnkbtnDel.Text = TextNm["DELETE"];
            lnkbtnDel.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_CONT"] + "');";
            lnkbtnDel.Visible = Master.isModDelAuthOk;

            /* 입주자 기본정보 */
            ltFloor.Text = TextNm["BLD"];
            
            ltGender.Text = TextNm["GENDER"];
            ltNm.Text = TextNm["NAME"];
            ltRoomNo.Text = TextNm["ROOMNO"];
            ltMobileNo.Text = TextNm["MOBILENO"];
            ltTelNo.Text = TextNm["TEL"];
            
            ltTenantNm.Text = TextNm["NAME"];
            ltTenantTelNo.Text = TextNm["TEL"];
            ltRentalinfo.Text = TextNm["TENANTINFO"];
            ltTenantInfo.Text = TextNm["STEP1_2_TITLE"];
            ltTaxCd.Text = TextNm["TAXCD"];
            ltTaxAddr.Text = TextNm["ADDR"];

            /* 동거인 정보 */
            ltMngAddon.Text = TextNm["MNGADDON"];

            /* 출입카드 관련 정보 */
            ltMngCard.Text = TextNm["MNGCARD"];
            ltOccupationDt.Text = TextNm["OCCUDT"];

            /* 주차카드 관련 정보 */
            //ltMngParkingCard.Text = TextNm["MNGPARKINGCARDNO"];

            lnkbtnModify.Visible = Master.isModDelAuthOk;
            if (txtHfRentCd.Text.Equals("9000"))
            {
                KsystemCode.Visible = false;
                ltBirthDt.Text = "Commencing Date";
            }
            else
            {
                ltBirthDt.Text = TextNm["BIRTHDATE"];
            }
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            var dsReturn = new DataSet();

            // KN_USP_RES_SELECT_USERINFO_S03
            dsReturn = ResidentMngBlo.WatchSalesUserView(txtHfUserSeq.Text, txtHfRentCd.Text, Int32.Parse(txtHfRentSeq.Text), Session["LangCd"].ToString());

            if (dsReturn != null)
            {
                DataTable dtMainInfo = dsReturn.Tables[0];

                if (dtMainInfo != null)
                {
                    BindMainInfo(dtMainInfo);
                }

                DataTable dtAddonInfo = dsReturn.Tables[1];

                if (dtAddonInfo != null)
                {
                    lvMngAddon.DataSource = dtAddonInfo;
                    lvMngAddon.DataBind();
                }

                DataTable dtAccessInfo = dsReturn.Tables[2];

                if (dtAccessInfo != null)
                {
                    lvMngCardList.DataSource = dtAccessInfo;
                    lvMngCardList.DataBind();
                }

                DataTable dtTenantInfo = dsReturn.Tables[0];

                if (dtTenantInfo != null)
                {
                    BindTenantInfo(dtTenantInfo);
                }
            }
        }

        protected void BindMainInfo(DataTable dtParam)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            if (dtParam.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtParam.Rows[0]["RentNm"].ToString()))
                {
                    ltInsFloor.Text = dtParam.Rows[0]["RentNm"].ToString();
                    txtHfRentCd.Text = dtParam.Rows[0]["RentCd"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["RoomNo"].ToString()))
                {
                    ltInsRoomNo.Text = dtParam.Rows[0]["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserNm"].ToString()))
                {
                    ltInsNm.Text =  TextLib.StringDecoder(dtParam.Rows[0]["UserNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["BirthDt"].ToString()))
                {
                    ltInsBirthDt.Text = TextLib.MakeDateEightDigit(dtParam.Rows[0]["BirthDt"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["Gender"].ToString()))
                {
                    ltInsGender.Text = dtParam.Rows[0]["Gender"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["OccupationDt"].ToString()))
                {
                    ltInsOccupationDt.Text = TextLib.MakeDateEightDigit(dtParam.Rows[0]["OccupationDt"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserMobileTyCd"].ToString()) &&
                    !string.IsNullOrEmpty(dtParam.Rows[0]["UserMobileFrontNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtParam.Rows[0]["UserMobileRearNo"].ToString()))
                {
                    StringBuilder sbMobile = new StringBuilder();

                    sbMobile.Append(dtParam.Rows[0]["UserMobileTyCd"].ToString());
                    sbMobile.Append(") ");
                    sbMobile.Append(dtParam.Rows[0]["UserMobileFrontNo"].ToString());
                    sbMobile.Append("-");
                    sbMobile.Append(dtParam.Rows[0]["UserMobileRearNo"].ToString());

                    ltInsMobileNo.Text = sbMobile.ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserTelTyCd"].ToString()))
                {
                    StringBuilder sbTel = new StringBuilder();

                    sbTel.Append(dtParam.Rows[0]["UserTelTyCd"].ToString());
                    sbTel.Append(") ");
                    sbTel.Append(dtParam.Rows[0]["UserTelFrontNo"].ToString());
                    sbTel.Append("-");
                    sbTel.Append(dtParam.Rows[0]["UserTelRearNo"].ToString());

                    ltInsTelNo.Text = sbTel.ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserTaxCd"].ToString()))
                {
                    ltInsTaxCd.Text = dtParam.Rows[0]["UserTaxCd"].ToString();
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserAddr"].ToString()))
                {
                    ltInsTaxAddr.Text =  TextLib.StringDecoder(dtParam.Rows[0]["UserAddr"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["UserDetAddr"].ToString()))
                {
                    ltInsTaxDetAddr.Text =  TextLib.StringDecoder(dtParam.Rows[0]["UserDetAddr"].ToString());
                }
                if (!string.IsNullOrEmpty(dtParam.Rows[0]["KSys_CustCd"].ToString()))
                {
                    ltKsystemCode.Text = dtParam.Rows[0]["KSys_CustCd"].ToString();
                }
            }
        }

        protected void BindTenantInfo(DataTable dtParam)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            if (dtParam.Rows.Count > 0)
            {

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["ContractNm"].ToString()))
                {
                    ltInsTenantNm.Text =  TextLib.StringDecoder(dtParam.Rows[0]["ContractNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtParam.Rows[0]["TelFontNo"].ToString()))
                {
                    StringBuilder sbTel = new StringBuilder();

                    sbTel.Append(dtParam.Rows[0]["TelFontNo"].ToString());
                    sbTel.Append(") ");
                    sbTel.Append(dtParam.Rows[0]["TelMidNo"].ToString());
                    sbTel.Append("-");
                    sbTel.Append(dtParam.Rows[0]["TelRearNo"].ToString());

                    ltInsTenantTelNo.Text = sbTel.ToString();
                }
            }
        }

        protected void lvMngAddon_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvMngAddon.FindControl("ltAddonUser")).Text = TextNm["COHABITANTNAME"];
            ((Literal)lvMngAddon.FindControl("ltAddonoSex")).Text = TextNm["GENDER"];
            ((Literal)lvMngAddon.FindControl("ltAddonRelation")).Text = TextNm["RELATIONSHIP"];
        }

        protected void lvMngAddon_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltAddonUser")).Text = TextNm["COHABITANTNAME"];
                    ((Literal)e.Item.FindControl("ltAddonoSex")).Text = TextNm["GENDER"];
                    ((Literal)e.Item.FindControl("ltAddonRelation")).Text = TextNm["RELATIONSHIP"];

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

                if (!string.IsNullOrEmpty(drView["AccessNm"].ToString()))
                {
                    Literal ltInsAddonUser = (Literal)iTem.FindControl("ltInsAddonUser");
                    ltInsAddonUser.Text =  TextLib.StringDecoder(drView["AccessNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RelationCd"].ToString()))
                {
                    DropDownList ddlAddonUser = (DropDownList)iTem.FindControl("ddlAddonUser");

                    CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlAddonUser, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RELATION);

                    ddlAddonUser.SelectedValue = drView["RelationCd"].ToString();
                    ddlAddonUser.Enabled = CommValue.AUTH_VALUE_FALSE;
                }

                if (!string.IsNullOrEmpty(drView["Gender"].ToString()))
                {
                    Literal ltInsAddonoSex = (Literal)iTem.FindControl("ltInsAddonoSex");
                    ltInsAddonoSex.Text = drView["Gender"].ToString();
                }
            }
        }

        protected void lvMngCardList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvMngCardList.FindControl("ltCardUser")).Text = TextNm["USER"];
            ((Literal)lvMngCardList.FindControl("ltRelation")).Text = TextNm["RELATIONSHIP"];
            ((Literal)lvMngCardList.FindControl("ltMngCardNo")).Text = TextNm["MNGCARDNO"];
        }

        protected void lvMngCardList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltCardUser")).Text = TextNm["USER"];
                    ((Literal)e.Item.FindControl("ltRelation")).Text = TextNm["RELATIONSHIP"];
                    ((Literal)e.Item.FindControl("ltMngCardNo")).Text = TextNm["MNGCARDNO"];

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

                if (!string.IsNullOrEmpty(drView["AccessNm"].ToString()))
                {
                    Literal ltInsCardUser = (Literal)iTem.FindControl("ltInsCardUser");
                    ltInsCardUser.Text =  TextLib.StringDecoder(drView["AccessNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["RelationCd"].ToString()))
                {
                    DropDownList ddlInsRelation = (DropDownList)iTem.FindControl("ddlInsRelation");

                    CommCdDdlUtil.MakeEtcSubCdDdlNoTitle(ddlInsRelation, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RELATION);

                    ddlInsRelation.SelectedValue = drView["RelationCd"].ToString();
                    ddlInsRelation.Enabled = CommValue.AUTH_VALUE_FALSE;
                }

                if (!string.IsNullOrEmpty(drView["AccessCardNo"].ToString()))
                {
                    Literal ltInsMngCardNo = (Literal)iTem.FindControl("ltInsMngCardNo");
                    ltInsMngCardNo.Text = drView["AccessCardNo"].ToString();
                }
            }
        }

        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbLink = new StringBuilder();

                sbLink.Append(Master.PAGE_MODIFY);
                sbLink.Append("?");
                sbLink.Append(Master.PARAM_DATA1);
                sbLink.Append("=");
                sbLink.Append(Request.Params[Master.PARAM_DATA1].ToString());
                sbLink.Append("&");
                sbLink.Append(Master.PARAM_DATA2);
                sbLink.Append("=");
                sbLink.Append(txtHfRentSeq.Text);

                if (!string.IsNullOrEmpty(txtHfUserSeq.Text))
                {
                    sbLink.Append("&");
                    sbLink.Append(Master.PARAM_DATA3);
                    sbLink.Append("=");
                    sbLink.Append(txtHfUserSeq.Text);
                }

                Response.Redirect(sbLink.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnList_Click(object sender, EventArgs e)
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

        protected void lnkbtnDel_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                // KN_USP_RES_DELETE_USERINFO_M00
                ResidentMngBlo.RemoveUserInfo(txtHfUserSeq.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP, txtHfRentCd.Text);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + Request.Params[Master.PARAM_DATA1].ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}