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

using KN.Stock.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupItemSearch : BasePage
    {
        public const string PARAM_DATA1 = "CompTy";
        public const string PARAM_DATA2 = "ReleaseData";
        public const string PARAM_DATA3 = "ReturnPage";
        public const string PARAM_DATA4 = "PurchaseData";

        string strViewDt = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CheckTextBox();

                if (!IsPostBack)
                {
                    

                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
                    }
                    else
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                        sbWarning.Append("');");
                        sbWarning.Append("self.close();");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                }                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 파라미터 체크
        /// </summary>
        /// <returns></returns>
        protected bool CheckParams()
        {
            bool isReturn = CommValue.AUTH_VALUE_FALSE;

            // PreCondition
            // 1. 메뉴 관리 페이지를 거쳐서 넘어오지 않을 경우 리턴
            // 2. 인자값이 제대로 넘어오지 않을 경우 리턴
            if (Session["FindSearchYn"] != null)
            {
                // 메뉴 관리 페이지를 거쳐서 넘어오지 않을 경우 리턴
                if (Session["FindSearchYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    // 인자값이 제대로 넘어오지 않을 경우 리턴
                    if (Request.Params[PARAM_DATA1] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA1].ToString()))
                        {
                            HfReturnValue.Value = Request.Params[PARAM_DATA1].ToString();
                            HfReturnValueInfo.Value = Request.Params[PARAM_DATA3].ToString();

                            if (Request.Params[PARAM_DATA2] != null)
                            {
                                HfReturnQtyValue.Value = Request.Params[PARAM_DATA2].ToString();    
                            }
                            else if (Request.Params[PARAM_DATA4] != null)
                            {
                                HfReturnQtyValue.Value = Request.Params[PARAM_DATA4].ToString();
                            }

                            Session["FindTitleYn"] = null;
                            isReturn = CommValue.AUTH_VALUE_TRUE;
                        }
                        else
                        {
                            isReturn = CommValue.AUTH_VALUE_FALSE;
                        }
                    }
                    else
                    {
                        isReturn = CommValue.AUTH_VALUE_FALSE;
                    }
                }
                else
                {
                    isReturn = CommValue.AUTH_VALUE_FALSE;
                }
            }
            else
            {
                isReturn = CommValue.AUTH_VALUE_FALSE;
            }

            return isReturn;
        }

        /// <summary>
        /// 컨트롤초기화
        /// </summary>
        protected void InitControls()
        {
            ltRentCd.Text = TextNm["SECTIONCD"];
            ltSvcZoneCd.Text = TextNm["WAREHOUSECD"];
            ltGrpCd.Text = TextNm["SECTION"];
            ltMainCd.Text = TextNm["DETAIL"];
            ltSubCd.Text = TextNm["ITEM"] + TextNm["NAME"];

            ltCdSearch.Text = TextNm["CD"] + TextNm["SEARCH"];
            ltCdNmSearch.Text = TextNm["CDNM"] + TextNm["SEARCH"];

            //txtCdSearch.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            //txtCdNmSearch.Attributes["onkeypress"] = "javascript:IsNumeric('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";

            lnkbtnSearchCd.Text = TextNm["SEARCH"];
            lnkbtnSearchCd.OnClientClick = "javascript:return fnCheckCd('" + AlertNm["ALERT_INSERT_BLANK"] + "')";          

            lnkbtnSearchCdNm.Text = TextNm["SEARCH"];
            lnkbtnSearchCdNm.OnClientClick = "javascript:return fnCheckCdNm('" + AlertNm["ALERT_INSERT_BLANK"] + "')";            

            MakeTopRentDdl();

            MakeTopSvcDdl();

            MakeTopGrpDdl();

            MakeTopMainDdl();

            MakeTopSubDdl();
        }

        private void CheckTextBox()
        {
            //텍스트박스에서 엔터치면 버튼 클릭이벤트 작동
            txtCdSearch.Attributes["onkeypress"] = "javascript:fnCheckCdEnter('" + AlertNm["ALERT_INSERT_ONLY_NO"] + "')";

            //텍스트박스에서 엔터치면 버튼 클릭이벤트 작동
            txtCdNmSearch.Attributes["onkeypress"] = "javascript:fnCheckCdNmEnter()";
        }

        private void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_STK_SELECT_GOODSINFO_S02
            dtReturn = MaterialMngBlo.WatchGoodsViewInfo(Session["LangCd"].ToString(), ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue, ddlMainCd.SelectedValue, ddlSubCd.SelectedValue);

            lvItemList.DataSource = dtReturn;
            lvItemList.DataBind();
           
        }

        private void LoadSearchCdData()
        {
            DataTable dtReturn1 = new DataTable();

            // KN_USP_STK_SELECT_GOODSINFO_S04
            dtReturn1 = MaterialMngBlo.WatchGoodsViewInfo(Session["LangCd"].ToString(), txtCdSearch.Text);

            lvItemList.DataSource = dtReturn1;
            lvItemList.DataBind();

        }

        private void LoadSearchCdNmData()
        {
            DataTable dtReturn2 = new DataTable();

            // KN_USP_STK_SELECT_GOODSINFO_S05
            dtReturn2 = MaterialMngBlo.WatchGoodsViewInfo1(Session["LangCd"].ToString(), txtCdNmSearch.Text);

            lvItemList.DataSource = dtReturn2;
            lvItemList.DataBind();

        }

        private void MakeTopRentDdl()
        {
            DataTable dtReturn = CommCdInfo.SelectSubCdWithNoTitle(Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

            ddlRentCd.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlRentCd.Items.Add(new ListItem(dr["CodeNm"].ToString() + " (" + dr["CodeCd"].ToString() + ") ", dr["CodeCd"].ToString()));
            }
        }

        private void MakeTopSvcDdl()
        {
            ddlSvcZoneCd.Items.Clear();

            if (!string.IsNullOrEmpty(ddlRentCd.SelectedValue))
            {
                // KN_USP_STK_SELECT_WAREHOUSEINFO_S01
                // KN_USP_STK_SELECT_WAREHOUSEINFO_S00
                DataTable dtSvcSection = WarehouseMngBlo.SpreadWarehouseInfo(ddlRentCd.SelectedValue);

                foreach (DataRow dr in dtSvcSection.Select())
                {
                    ddlSvcZoneCd.Items.Add(new ListItem(dr["SvcZoneCd"].ToString(), dr["SvcZoneCd"].ToString()));
                }
            }
        }

        private void MakeTopGrpDdl()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIGRPCD_S01
            DataTable dtGrpReturn = MaterialMngBlo.SpreadClassiGrpCdInfo(Session["LangCd"].ToString(), strViewDt, ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue);

            ddlGrpCd.Items.Clear();
            ddlGrpCd.Items.Add(new ListItem(TextNm["ENTIRE"], ""));

            foreach (DataRow dr in dtGrpReturn.Select())
            {
                ddlGrpCd.Items.Add(new ListItem(dr["CodeNm"].ToString() + " (" + dr["CodeCd"].ToString() + ") ", dr["CodeCd"].ToString()));
            }
        }

        private void MakeTopMainDdl()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIMAINCD_S01
            DataTable dtMainReturn = MaterialMngBlo.SpreadClassiMainCdInfo(Session["LangCd"].ToString(), strViewDt, ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue);

            ddlMainCd.Items.Clear();
            ddlMainCd.Items.Add(new ListItem(TextNm["ENTIRE"], ""));

            foreach (DataRow dr in dtMainReturn.Select())
            {
                ddlMainCd.Items.Add(new ListItem(dr["CodeNm"].ToString() + " (" + dr["CodeCd"].ToString() + ") ", dr["CodeCd"].ToString()));
            }
        }

        private void MakeTopSubDdl()
        {
            txtCdSearch.Text = "";
            txtCdNmSearch.Text = "";

            strViewDt = DateTime.Now.ToString("s").Substring(0,10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSISUBCD_S00
            DataTable dtSubReturn = MaterialMngBlo.SpreadClassiSubCdInfo(ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue, ddlMainCd.SelectedValue);

            ddlSubCd.Items.Clear();
            ddlSubCd.Items.Add(new ListItem(TextNm["ENTIRE"], ""));

            foreach (DataRow dr in dtSubReturn.Select())
            {
                ddlSubCd.Items.Add(new ListItem(dr["CodeNm"].ToString() + " (" + dr["CodeCd"].ToString() + ") ", dr["CodeCd"].ToString()));
            }
        }

        protected void ddlRentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeTopSvcDdl();

                MakeTopGrpDdl();
                MakeTopMainDdl();
                MakeTopSubDdl();

                ChangeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlSvcZoneCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeTopGrpDdl();
                MakeTopMainDdl();
                MakeTopSubDdl();

                ChangeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlGrpCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeTopMainDdl();
                MakeTopSubDdl();

                ChangeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlMainCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeTopSubDdl();

                ChangeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlSubCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ChangeTopSubDdl();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        private void ChangeTopSubDdl()
        {
            if (!string.IsNullOrEmpty(ddlRentCd.SelectedValue) &&
                !string.IsNullOrEmpty(ddlSvcZoneCd.SelectedValue) &&
                !string.IsNullOrEmpty(ddlGrpCd.SelectedValue) &&
                !string.IsNullOrEmpty(ddlMainCd.SelectedValue) &&
                !string.IsNullOrEmpty(ddlSubCd.SelectedValue))
            {
                LoadData();
            }
            else
            {
 //               ResetControls();
            }

        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvItemList_LayoutCreated(object sender, EventArgs e)
        {
            Literal ltSection = (Literal)lvItemList.FindControl("ltSection");
            ltSection.Text = TextNm["SECTIONCD"];
            Literal ltSvcZone = (Literal)lvItemList.FindControl("ltSvcZone");
            ltSvcZone.Text = TextNm["WAREHOUSECD"];
            Literal ltItemNm = (Literal)lvItemList.FindControl("ltItemNm");
            ltItemNm.Text = TextNm["NAME"];
            Literal ltItemCd = (Literal)lvItemList.FindControl("ltItemCd");
            ltItemCd.Text = TextNm["CD"];
            Literal ltQty = (Literal)lvItemList.FindControl("ltQty");
            ltQty.Text = TextNm["HAVEQTY"];
            Literal ltPrice = (Literal)lvItemList.FindControl("ltPrice");
            ltPrice.Text = TextNm["UNITPRICE"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvItemList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {

                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    Literal ltSection = (Literal)e.Item.FindControl("ltSection");
                    ltSection.Text = TextNm["SECTIONCD"];
                    Literal ltSvcZone = (Literal)e.Item.FindControl("ltSvcZone");
                    ltSvcZone.Text = TextNm["WAREHOUSECD"];
                    Literal ltItemNm = (Literal)e.Item.FindControl("ltItemNm");
                    ltItemNm.Text = TextNm["NAME"];
                    Literal ltItemCd = (Literal)e.Item.FindControl("ltItemCd");
                    ltItemCd.Text = TextNm["CD"];
                    Literal ltQty = (Literal)e.Item.FindControl("ltQty");
                    ltQty.Text = TextNm["HAVEQTY"];
                    Literal ltPrice = (Literal)e.Item.FindControl("ltPrice");
                    ltPrice.Text = TextNm["UNITPRICE"];

                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvItemList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["RentNm"].ToString()))
                    {
                        Literal ltSectionList = (Literal)iTem.FindControl("ltSectionList");
                        ltSectionList.Text = TextLib.StringDecoder(drView["RentNm"].ToString()) + " (" + drView["RentCd"].ToString() + ") ";
                    }
                } 

                if (!string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()))
                {
                    Literal ltSvcZoneList = (Literal)iTem.FindControl("ltSvcZoneList");
                    ltSvcZoneList.Text = drView["SvcZoneCd"].ToString();                    
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    Literal ltItemNmList = (Literal)iTem.FindControl("ltItemNmList");
                    ltItemNmList.Text = TextLib.StringDecoder(drView["ClassNm"].ToString());
                }

                if (!string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    Literal ltItemCdList = (Literal)iTem.FindControl("ltItemCdList");
                    ltItemCdList.Text = drView["ClassCd"].ToString();                    
                }

                if (!string.IsNullOrEmpty(drView["Qty"].ToString()))
                {
                    Literal ltQtyList = (Literal)iTem.FindControl("ltQtyList");
                    ltQtyList.Text = drView["Qty"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UnitSellingPrice"].ToString()))
                {     
                    Literal ltPriceList = (Literal)iTem.FindControl("ltPriceList");
                    ltPriceList.Text = drView["UnitSellingPrice"].ToString();
                }               
            }
        }

        /// <summary>
        /// 코드검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearchCd_Click(object sender, EventArgs e)
        {
            try
            {
                LoadSearchCdData();

                ddlRentCd.ClearSelection();
                ddlGrpCd.ClearSelection();
                ddlMainCd.ClearSelection();
                ddlSubCd.ClearSelection();
                ddlSvcZoneCd.ClearSelection();

                txtCdNmSearch.Text = "";


            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 코드명검색
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearchCdNm_Click(object sender, EventArgs e)
        {
            try
            {
                LoadSearchCdNmData();

                ddlRentCd.ClearSelection();
                ddlGrpCd.ClearSelection();
                ddlMainCd.ClearSelection();
                ddlSubCd.ClearSelection();
                ddlSvcZoneCd.ClearSelection();

                txtCdSearch.Text = "";
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}
