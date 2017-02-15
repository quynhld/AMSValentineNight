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

using KN.Stock.Biz;

namespace KN.Web.Stock.Material
{
    public partial class MaterialMngList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        string strViewDt = string.Empty;
        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
                    InitControls();

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
        /// 컨트롤 초기화
        /// </summary>
        private void InitControls()
        {
            ltAddonFile.Text = TextNm["EXCELUPLOAD"];
            ltSearch.Text = TextNm["SEARCH"];
            ltCdNm.Text = TextNm["CDNM"];

            lnkbtnSearch.Text = TextNm["SEARCH"];
            lnkbtnReset.Text = TextNm["RESET"];

            //lnkbtnPrint.Text = TextNm["PRINT"];
            lnkbtnReleaseRequest.Text = TextNm["RELEASE"] + " " + TextNm["REQUEST"];
            lnkbtnReleaseRequest.Visible = Master.isWriteAuthOk;
            lnkbtnOrderRequest.Text = TextNm["ORDER"] + " " + TextNm["REQUEST"];
            lnkbtnOrderRequest.Visible = Master.isWriteAuthOk;
            lnkbtnAddItems.Text = TextNm["ITEMADD"];
            lnkbtnAddItems.Visible = Master.isWriteAuthOk;

            lnkbtnFileUpload.Text = TextNm["EXCELUPLOAD"];
            lnkbtnFileUpload.OnClientClick = "javascript:return fnCheckFileUpload('" + AlertNm["ALERT_SELECT_FILE"] + "');";

            CommCdDdlUtil.MakeEtcSubCdDdlUserTitle(ddlRentCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_RENTAL, TextNm["ENTIRE"]);

            CommCdDdlUtil.MakeSubCdDdlUserTitle(ddlStatusCd, Session["LangCd"].ToString(), CommValue.COMMCD_VALUE_SELECT, CommValue.SELECT_TYPE_VALUE_STOCK, TextNm["ENTIRE"]);

            MakeSvcZoneDdl();

            MakeTopGrpDdl();

            MakeTopMainDdl();

            MakeTopSubDdl();
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
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

            // KN_USP_STK_SELECT_GOODSINFO_S00
            dsReturn = MaterialMngBlo.SpreadGoodsInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), ddlRentCd.SelectedValue
                                                    , ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue, ddlMainCd.SelectedValue, ddlSubCd.SelectedValue
                                                    , txtCdNm.Text, Session["LangCd"].ToString(), ddlStatusCd.SelectedValue);

            if (dsReturn != null)
            {
                lvMaterialList.DataSource = dsReturn.Tables[1];
                lvMaterialList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        private void MakeSvcZoneDdl()
        {
            ddlSvcZoneCd.Items.Clear();

            ddlSvcZoneCd.Items.Add(new ListItem(TextNm["ENTIRE"], ""));

            if (!string.IsNullOrEmpty(ddlRentCd.SelectedValue))
            {
                // KN_USP_STK_SELECT_WAREHOUSEINFO_S01
                DataTable dtSvcSection = WarehouseMngBlo.SpreadWarehouseInfo(ddlRentCd.SelectedValue);

                foreach (DataRow dr in dtSvcSection.Select())
                {
                    ddlSvcZoneCd.Items.Add(new ListItem(dr["SvcZoneCd"].ToString(), dr["SvcZoneCd"].ToString()));
                }
            }
        }

        private void MakeTopGrpDdl()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIGRPCD_S01
            DataTable dtGrpReturn = MaterialMngBlo.SpreadClassiGrpCdInfo(Session["LangCd"].ToString(), strViewDt, ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue);

            ddlGrpCd.Items.Clear();

            ddlGrpCd.DataSource = dtGrpReturn;
            ddlGrpCd.DataTextField = "CodeNm";
            ddlGrpCd.DataValueField = "CodeCd";
            ddlGrpCd.DataBind();

            ddlGrpCd.Items.Insert(0, new ListItem(TextNm["ENTIRE"], ""));

        }

        private void MakeTopMainDdl()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSIMAINCD_S01
            DataTable dtMainReturn = MaterialMngBlo.SpreadClassiMainCdInfo(Session["LangCd"].ToString(), strViewDt, ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue);

            ddlMainCd.Items.Clear();

            ddlMainCd.DataSource = dtMainReturn;
            ddlMainCd.DataTextField = "CodeNm";
            ddlMainCd.DataValueField = "CodeCd";
            ddlMainCd.DataBind();

            ddlMainCd.Items.Insert(0, new ListItem(TextNm["ENTIRE"], ""));
        }

        private void MakeTopSubDdl()
        {
            strViewDt = DateTime.Now.ToString("s").Substring(0, 10).Replace("-", "").Replace(".", "");

            // KN_USP_STK_SELECT_CLASSISUBCD_S00
            DataTable dtSubReturn = MaterialMngBlo.SpreadClassiSubCdInfo(ddlRentCd.SelectedValue, ddlSvcZoneCd.SelectedValue, ddlGrpCd.SelectedValue, ddlMainCd.SelectedValue);

            ddlSubCd.Items.Clear();

            ddlSubCd.DataSource = dtSubReturn;
            ddlSubCd.DataTextField = "CodeNm";
            ddlSubCd.DataValueField = "CodeCd";
            ddlSubCd.DataBind();

            ddlSubCd.Items.Insert(0, new ListItem(TextNm["ENTIRE"], ""));
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMaterialList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvMaterialList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvMaterialList.FindControl("ltGoodsCd")).Text = TextNm["ITEM"] + " " + TextNm["CD"];
            ((Literal)lvMaterialList.FindControl("ltGoodsNm")).Text = TextNm["ITEM"] + " " + TextNm["NAME"];
            ((Literal)lvMaterialList.FindControl("ltQty")).Text = TextNm["QTY"];
            ((Literal)lvMaterialList.FindControl("ltReleaseRequest")).Text = TextNm["RELEASE"] + " " + TextNm["REQUEST"] + " " + TextNm["QTY"];
            ((Literal)lvMaterialList.FindControl("ltOrderRequset")).Text = TextNm["ORDER"] + " " + TextNm["REQUEST"] + " " + TextNm["QTY"];
            ((Literal)lvMaterialList.FindControl("ltEmergency")).Text = TextNm["AUTOAPPROVAL"];
            ((Literal)lvMaterialList.FindControl("ltUse")).Text = TextNm["USEYN"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMaterialList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 타이틀 정의
                    ((Literal)e.Item.FindControl("ltSeq")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltGoodsCd")).Text = TextNm["ITEM"] + " " + TextNm["CD"];
                    ((Literal)e.Item.FindControl("ltGoodsNm")).Text = TextNm["ITEM"] + " " + TextNm["NAME"];
                    ((Literal)e.Item.FindControl("ltQty")).Text = TextNm["QTY"];
                    ((Literal)e.Item.FindControl("ltReleaseRequest")).Text = TextNm["RELEASE"] + " " + TextNm["REQUEST"] + " " + TextNm["QTY"];
                    ((Literal)e.Item.FindControl("ltOrderRequset")).Text = TextNm["ORDER"] + " " + TextNm["REQUEST"] + " " + TextNm["QTY"];
                    ((Literal)e.Item.FindControl("ltUse")).Text = TextNm["USEYN"];

                    ((CheckBox)e.Item.FindControl("chkAll")).Enabled = CommValue.AUTH_VALUE_FALSE;

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
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMaterialList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["RealSeq"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsSeq")).Text = drView["RealSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RentCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["SvcZoneCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiGrpCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassiMainCd"].ToString()) &&
                    !string.IsNullOrEmpty(drView["ClassCd"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsGoodsCd")).Text = drView["RentCd"].ToString() + drView["SvcZoneCd"].ToString() + "-" + drView["ClassiGrpCd"].ToString() + drView["ClassiMainCd"].ToString() + drView["ClassCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["ClassNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsGoodsNm")).Text = drView["ClassNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["Qty"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = drView["Qty"].ToString();
                }
                else
                {
                    ((Literal)iTem.FindControl("ltInsQty")).Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["RequestQtyCnt"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsReleaseRequest")).Text = drView["RequestQtyCnt"].ToString();
                }
                else
                {
                    ((Literal)iTem.FindControl("ltInsReleaseRequest")).Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["OrderQtyCnt"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsOrderRequset")).Text = drView["OrderQtyCnt"].ToString();
                }
                else
                {
                    ((Literal)iTem.FindControl("ltInsOrderRequset")).Text = CommValue.NUMBER_VALUE_ZERO;
                }

                if (!string.IsNullOrEmpty(drView["EmergencyYn"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsEmergency")).Text = drView["EmergencyYn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StatusNm"].ToString()))
                {
                    ((Literal)iTem.FindControl("ltInsUse")).Text = drView["StatusNm"].ToString();

                    CheckBox chkboxList = ((CheckBox)iTem.FindControl("chkboxList"));

                    if (drView["StatusNm"].ToString().Equals(CommValue.STOCK_TYPE_VALUE_UNUSEABLE))
                    {
                        chkboxList.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                }
            }
        }

        /// <summary>
        /// list내의 선택checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllCheck = ((CheckBox)lvMaterialList.FindControl("chkAll")).Checked;

            try
            {
                CheckAll(isAllCheck);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 전체 체크시 list내의 모든 체크박스를 체크 Method
        /// </summary>
        /// <param name="isAllCheck"></param>
        private void CheckAll(bool isAllCheck)
        {
            for (int intTmpI = 0; intTmpI < lvMaterialList.Items.Count; intTmpI++)
            {
                if (((CheckBox)lvMaterialList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                {
                    ((CheckBox)lvMaterialList.Items[intTmpI].FindControl("chkboxList")).Checked = isAllCheck;
                }
            }
        }

        /// <summary>
        /// 리스트 각 행별 체크 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void chkboxList_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                int intCheckCount = 0;
                int intCheckDisabled = 0;

                for (int intTmpI = 0; intTmpI < lvMaterialList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvMaterialList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                    {
                        if (((CheckBox)lvMaterialList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            intCheckCount++;
                        }
                    }
                    else
                    {
                        intCheckDisabled++;
                    }
                }

                if (intCheckCount == (lvMaterialList.Items.Count - intCheckDisabled))
                {
                    ((CheckBox)lvMaterialList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    ((CheckBox)lvMaterialList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;
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

                ((CheckBox)lvMaterialList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 상세보기 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnDetailView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                StringBuilder sbList = new StringBuilder();

                sbList.Append(Master.PAGE_VIEW);
                sbList.Append("?");
                sbList.Append(Master.PARAM_DATA1);
                sbList.Append("=");
                sbList.Append(hfRentCd.Value);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA2);
                sbList.Append("=");
                sbList.Append(hfSvcZoneCd.Value);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA3);
                sbList.Append("=");
                sbList.Append(hfGrpCd.Value);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA4);
                sbList.Append("=");
                sbList.Append(hfMainCd.Value);
                sbList.Append("&");
                sbList.Append(Master.PARAM_DATA5);
                sbList.Append("=");
                sbList.Append(hfSubCd.Value);

                Response.Redirect(sbList.ToString(), CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlRentCd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                MakeSvcZoneDdl();
                MakeTopGrpDdl();
                MakeTopMainDdl();
                MakeTopSubDdl();
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
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (((CheckBox)lvMaterialList.FindControl("chkAll")) != null)
                {
                    ((CheckBox)lvMaterialList.FindControl("chkAll")).Checked = CommValue.AUTH_VALUE_FALSE;
                }

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                ddlRentCd.SelectedValue = string.Empty;
                ddlSvcZoneCd.SelectedValue = string.Empty;
                ddlGrpCd.SelectedValue = string.Empty;
                ddlMainCd.SelectedValue = string.Empty;
                ddlSubCd.SelectedValue = string.Empty;
                ddlStatusCd.SelectedValue = string.Empty;
                txtCdNm.Text = string.Empty;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnFileUpload_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (fuExcelUpload.HasFile)
                {
                    char[] chDiv = { '.' };
                    string strFileType = fuExcelUpload.PostedFile.ContentType.ToString();
                    string[] strTmpArray = fuExcelUpload.PostedFile.FileName.Split(chDiv);
                    string strExtension = string.Empty;

                    DataTable dtTmpTable = new DataTable();

                    if (strTmpArray.Length > 0)
                    {
                        bool isFailInsert = CommValue.AUTH_VALUE_FALSE;
                        strExtension = strTmpArray[strTmpArray.Length - 1];

                        if (strFileType == CommValue.EXCEL_CONTTYPE_VALUE_XLS && strExtension.ToLower().Equals(CommValue.EXCEL_TYPE_TEXT_XLS))
                        {
                            // Excel Data 리딩
                            ExcelReaderLib erReader = new ExcelReaderLib();
                            DataTable dtTable = new DataTable();

                            dtTable = erReader.ExtractDataTable(fuExcelUpload.PostedFile.FileName);

                            // 각 컬럼별 Validation 체크 후 등록
                            foreach (DataRow dr in dtTable.Select())
                            {
                                string strRentCd = string.Empty;
                                string strSvcZoneCd = string.Empty;
                                string strClassiGrpCd = string.Empty;
                                string strClassiMainCd = string.Empty;
                                string strClassCd = string.Empty;
                                string strClassNm = string.Empty;
                                string strCompNo = string.Empty;
                                string strScaleCd = string.Empty;
                                string strVATYn = string.Empty;
                                string strEmergencyYn = string.Empty;
                                string strStatusCd = string.Empty;
                                string strRemark = string.Empty;

                                int intQty = 0;
                                double dblUnitPrimeCost = 0.0d;
                                double dblUnitSellingPrice = 0.0d;
                                double dblVATRatio = 0.0d;

                                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                                if (!string.IsNullOrEmpty(dr["RentCd"].ToString()))
                                {
                                    strRentCd = dr["RentCd"].ToString();
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["SvcZoneCd"].ToString()))
                                {
                                    strSvcZoneCd = dr["SvcZoneCd"].ToString();
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["ClassiGrpCd"].ToString()))
                                {
                                    strClassiGrpCd = dr["ClassiGrpCd"].ToString();
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["ClassiMainCd"].ToString()))
                                {
                                    strClassiMainCd = dr["ClassiMainCd"].ToString();
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["ClassCd"].ToString()))
                                {
                                    strClassCd = dr["ClassCd"].ToString();
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["ClassNm"].ToString()))
                                {
                                    strClassNm = dr["ClassNm"].ToString();
                                }
                                else
                                {
                                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }

                                if (!string.IsNullOrEmpty(dr["CompNo"].ToString()))
                                {
                                    strCompNo = dr["CompNo"].ToString();
                                }
                                else
                                {
                                    strCompNo = string.Empty;
                                }

                                if (!string.IsNullOrEmpty(dr["Qty"].ToString()))
                                {
                                    intQty = Int32.Parse(dr["Qty"].ToString());
                                }
                                else
                                {
                                    intQty = 0;
                                }

                                if (!string.IsNullOrEmpty(dr["ScaleCd"].ToString()))
                                {
                                    strScaleCd = dr["ScaleCd"].ToString();
                                }
                                else
                                {
                                    strScaleCd = CommValue.CODE_VALUE_INIT;
                                }

                                if (!string.IsNullOrEmpty(dr["UnitPrimeCost"].ToString()))
                                {
                                    dblUnitPrimeCost = double.Parse(dr["UnitPrimeCost"].ToString());
                                }
                                else
                                {
                                    dblUnitPrimeCost = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["UnitSellingPrice"].ToString()))
                                {
                                    dblUnitSellingPrice = double.Parse(dr["UnitSellingPrice"].ToString());
                                }
                                else
                                {
                                    dblUnitSellingPrice = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["VATRatio"].ToString()))
                                {
                                    dblVATRatio = double.Parse(dr["VATRatio"].ToString());
                                }
                                else
                                {
                                    dblVATRatio = 0.0d;
                                }

                                if (!string.IsNullOrEmpty(dr["VATYn"].ToString()))
                                {
                                    strVATYn = dr["VATYn"].ToString();
                                }
                                else
                                {
                                    strVATYn = CommValue.CONCLUSION_TYPE_TEXT_YES;
                                }

                                if (!string.IsNullOrEmpty(dr["EmergencyYn"].ToString()))
                                {
                                    strEmergencyYn = dr["EmergencyYn"].ToString();
                                }
                                else
                                {
                                    strEmergencyYn = CommValue.CONCLUSION_TYPE_TEXT_YES;
                                }

                                if (!string.IsNullOrEmpty(dr["StatusCd"].ToString()))
                                {
                                    strStatusCd = dr["StatusCd"].ToString();
                                }
                                else
                                {
                                    strStatusCd = CommValue.CODE_VALUE_INIT;
                                }

                                if (!string.IsNullOrEmpty(dr["Remark"].ToString()))
                                {
                                    strRemark = dr["Remark"].ToString();
                                }
                                else
                                {
                                    strRemark = string.Empty;
                                }

                                // 신규데이터 등록
                                bool isExist = CommValue.AUTH_VALUE_FALSE;

                                // KN_USP_STK_SELECT_GOODSINFO_S01
                                DataTable dtReturn = MaterialMngBlo.SpreadExgistGoodsInfo(strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd, strClassCd);

                                if (dtReturn.Rows.Count > 0)
                                {
                                    isExist = CommValue.AUTH_VALUE_TRUE;
                                }

                                if (!isExist)
                                {
                                    // KN_USP_STK_INSERT_GOODSINFO_M00
                                    MaterialMngBlo.RegistryGoodsInfo(strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd, strClassCd, strClassNm, strCompNo, intQty,
                                                                     strScaleCd, dblUnitPrimeCost, dblUnitSellingPrice, dblVATRatio, strVATYn, strEmergencyYn, strRemark,
                                                                     Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                                }
                            }

                            if (isFailInsert)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["ALERT_INSERT_BLANK"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                                LoadData();
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_REGIST_ITEM"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_PERMIT_ONLY_XLS"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                            LoadData();
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["ALERT_SELECT_FILE"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                        LoadData();
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["ALERT_SELECT_FILE"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnReleaseRequest_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intCheckCount = 0;
                string strParam = string.Empty;

                for (int intTmpI = 0; intTmpI < lvMaterialList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvMaterialList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                    {
                        if (((CheckBox)lvMaterialList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            intCheckCount++;
                            strParam = strParam + ((Literal)lvMaterialList.Items[intTmpI].FindControl("ltInsGoodsCd")).Text + CommValue.PARAM_VALUE_CHAR_ELEMENT;
                        }
                    }
                }

                if (intCheckCount == 0)
                {
                    Session[Master.PARAM_DATA1] = string.Empty;
                    Session[Master.PARAM_TRANSFER] = string.Empty;

                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    strParam = strParam.Substring(0, strParam.Length - 1);
                    // 선택된 항목 넘겨주는 부분
                    Session[Master.PARAM_DATA1] = strParam;
                    // Return 시킬 페이지 정보를 넘겨주는 부분
                    Session[Master.PARAM_TRANSFER] = Master.PAGE_TRANSFER;
                    Response.Redirect(Master.PAGE_REDIRECT, CommValue.AUTH_VALUE_FALSE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnOrderRequest_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intCheckCount = 0;
                string strParam = string.Empty;

                for (int intTmpI = 0; intTmpI < lvMaterialList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvMaterialList.Items[intTmpI].FindControl("chkboxList")).Enabled)
                    {
                        if (((CheckBox)lvMaterialList.Items[intTmpI].FindControl("chkboxList")).Checked)
                        {
                            intCheckCount++;
                            strParam = strParam + ((Literal)lvMaterialList.Items[intTmpI].FindControl("ltInsGoodsCd")).Text + CommValue.PARAM_VALUE_CHAR_ELEMENT;
                        }
                    }
                }

                if (intCheckCount == 0)
                {
                    Session[Master.PARAM_DATA1] = string.Empty;
                    Session[Master.PARAM_TRANSFER] = string.Empty;

                    Response.Redirect(Master.PAGE_REFLECT, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    strParam = strParam.Substring(0, strParam.Length - 1);
                    // 선택된 항목 넘겨주는 부분
                    Session[Master.PARAM_DATA1] = strParam;
                    // Return 시킬 페이지 정보를 넘겨주는 부분
                    Session[Master.PARAM_TRANSFER] = Master.PAGE_NOAUTH;

                    Response.Redirect(Master.PAGE_REFLECT, CommValue.AUTH_VALUE_FALSE);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnAddItems_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_WRITE, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        //강제 Post 처리
        //public static void GetDataSet(string targetUrl, string parameters)
        //{
        //    HttpWebRequest request;
        //    string sendUrl = string.Empty;

        //    sendUrl = targetUrl;

        //    // Create the web request

        //    Uri newUri = new Uri(sendUrl);
        //    request = (HttpWebRequest)WebRequest.Create(newUri);
        //    request.Method = WebRequestMethods.Http.Post;
        //    request.UserAgent = HttpContext.Current.Request.UserAgent;

        //    // 인코딩1 - UTF-8
        //    byte[] data = Encoding.UTF8.GetBytes(parameters);

        //    request.ContentType = "application/x-www-form-urlencoded";
        //    request.ContentLength = data.Length;

        //    Stream dataStream = request.GetRequestStream();
        //    dataStream.Write(data, 0, data.Length);
        //    dataStream.Close();
        //}
    }
}
