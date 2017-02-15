using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Resident.Biz;

namespace KN.Web.Config.Section
{
    public partial class SectionList : BasePage
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
                        Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
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

            if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1]))
            {
                txtHfRentCd.Text = Request.Params[Master.PARAM_DATA1].ToString();
                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        protected void InitControls()
        {
            ltSearchMenu.Text = TextNm["SEARCH"] + TextNm["COND"];

            ltFloor.Text = TextNm["FLOOR"];
            ltSection.Text = TextNm["ROOMNO"];
            ltCompNm.Text = TextNm["COMPNM"];
            ltLeasingArea.Text = TextNm["LEASINGAREA"] + " (" + TextNm["SMETER"] + ")";
            ltInsDt.Text = TextNm["APPLYDT"];
            //ltSampleFile.Text = TextNm["SAMPLEFILE"];

            //txtFloor.Attributes["onkeypress"] = "javascript:IsNumeric(this, '" + AlertNm["ALERT_INSERT_ONLY_NO"] + "');";
            //txtLeasingArea.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";

            //imgbtnRegist.OnClientClick = "javascript:return fnCheckValidate('" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            //ltAddonFile.Text = TextNm["EXCELUPLOAD"];
            //fuExcelUpload.Attributes["onchange"] = "javascript:fnCheckFileTypeExcel(this,'" + AlertNm["INFO_PERMIT_ONLY_XLS"] + "');";

            //lnkbtnFileUpload.Text = TextNm["EXCELUPLOAD"];
            //lnkbtnFileUpload.OnClientClick = "javascript:return fnCheckFileUpload('" + AlertNm["ALERT_SELECT_FILE"] + "');";
            //lnkbtnEntireReset.Text = TextNm["ENTIRE"] + " " + TextNm["RESET"];
            //lnkbtnEntireReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ALLROOM"] + "');";
            //lnkbtnFloorReset.Text = TextNm["FLOOR"] + " " + TextNm["RESET"];
            //lnkbtnFloorReset.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_FLOORROOM"] + "');";

            MakeYearDdl();

            MakeMonthDdl();

            InitFloorDdl();
        }

        /// <summary>
        /// 층 정보용 DropdownList 생성
        /// </summary>
        private void InitFloorDdl()
        {
            // KN_USP_RES_SELECT_FLOOR_S00
            DataTable dtReturn = RoomMngBlo.SpreadFloorList(txtHfRentCd.Text, ddlYear.SelectedValue, ddlMonth.SelectedValue);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    ddlFloor.Items.Clear();
                    ddlFloor.Visible = CommValue.AUTH_VALUE_TRUE;

                    foreach (DataRow dr in dtReturn.Select())
                    {
                        ddlFloor.Items.Add(new ListItem(dr["FloorNo"].ToString() + "F", dr["FloorNo"].ToString()));
                    }
                }
                else
                {
                    ddlFloor.Visible = CommValue.AUTH_VALUE_FALSE;
                }
            }
        }

        /// <summary>
        /// 년도용 DropdownList 생성
        /// </summary>
        private void MakeYearDdl()
        {
            ddlYear.Items.Clear();

            for (int intTmpI = CommValue.START_YEAR; intTmpI <= DateTime.Now.Year; intTmpI++)
            {
                ddlYear.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString()));
            }

            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// 월용 DropdownList 생성
        /// </summary>
        private void MakeMonthDdl()
        {
            ddlMonth.Items.Clear();

            for (int intTmpI = 1; intTmpI <= 12; intTmpI++)
            {
                ddlMonth.Items.Add(new ListItem(intTmpI.ToString(), intTmpI.ToString().PadLeft(2, '0')));
            }

            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        protected void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            int intFloor = 0;

            string strTmpRentalTy = string.Empty;
            string strTmpFloor = string.Empty;

            if (ddlFloor.Visible == CommValue.AUTH_VALUE_FALSE)
            {
                intFloor = 0;
            }
            else
            {
                intFloor = Int32.Parse(ddlFloor.SelectedValue);
            }

            DataTable dtListReturn = new DataTable();

            // KN_USP_RES_SELECT_ROOMINFO_S06
            dtListReturn = RoomMngBlo.SpreadRoomlistInfo(txtHfRentCd.Text, intFloor, ddlYear.SelectedValue, ddlMonth.SelectedValue);

            if (dtListReturn != null)
            {
                lvSectionList.DataSource = dtListReturn;
                lvSectionList.DataBind();
            }

            if (!string.IsNullOrEmpty(txtHfRentCd.Text))
            {
                DataTable dtRent = CommCdInfo.SelectSubCdWithTitle(CommValue.COMMCD_VALUE_ETC, CommValue.ETCCD_VALUE_RENTAL);

                strTmpRentalTy = dtRent.Rows[Int32.Parse(txtHfRentCd.Text)]["CodeNm"].ToString();
            }

            if (ddlFloor.Visible)
            {
                strTmpFloor = ddlFloor.SelectedItem.Text;
            }

            ltSearchCond.Text = ddlYear.SelectedValue + "." + ddlMonth.SelectedValue + " " + strTmpRentalTy + " " + strTmpFloor;

            ResetData();
        }

        protected void ResetData()
        {
            //txtFloor.Text = string.Empty;
            //txtSection.Text = string.Empty;
            //txtCompNm.Text = string.Empty;
            //hfCompSeq.Value = string.Empty;
            //txtLeasingArea.Text = string.Empty;
            //txtInsDt.Text = string.Empty;
            //hfInsDt.Value = string.Empty;
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvSectionList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        protected void lvSectionList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["FloorNo"].ToString()))
                {
                    Literal ltFloor = (Literal)iTem.FindControl("ltFloor");
                    ltFloor.Text = drView["FloorNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    Literal ltSection = (Literal)iTem.FindControl("ltSection");
                    ltSection.Text = drView["RoomNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TenantCompNm"].ToString()))
                {
                    Literal ltCompNm = (Literal)iTem.FindControl("ltCompNm");
                    ltCompNm.Text = TextLib.TextCutString(TextLib.StringDecoder(drView["TenantCompNm"].ToString()), 35, "..");
                    //TextBox txtCompNm = (TextBox)iTem.FindControl("txtCompNm");
                    //txtCompNm.Text = TextLib.StringDecoder(drView["TenantCompNm"].ToString());
                }

                //TextBox txtLeasingArea = (TextBox)iTem.FindControl("txtLeasingArea");
                //txtLeasingArea.Attributes["onkeypress"] = "javascript:IsNumericOrDot(this, '" + AlertNm["ALERT_INSERT_NO_N_DOT"] + "');";
                Literal ltLeasingArea = (Literal)iTem.FindControl("ltLeasingArea");

                if (!string.IsNullOrEmpty(drView["LeasingArea"].ToString()))
                {
                    ltLeasingArea.Text = drView["LeasingArea"].ToString();
                    //txtLeasingArea.Text = drView["LeasingArea"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["StartDt"].ToString()))
                {
                    string strDate = drView["StartDt"].ToString();

                    TextBox txtInsDt = (TextBox)iTem.FindControl("txtInsDt");
                    HiddenField hfInsDt = (HiddenField)iTem.FindControl("hfInsDt");
                    TextBox txtHfOriginDt = (TextBox)iTem.FindControl("txtHfOriginDt");

                    Literal ltInsDt = (Literal)iTem.FindControl("ltInsDt");

                    txtInsDt.Text = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2);
                    hfInsDt.Value = strDate;
                    txtHfOriginDt.Text = strDate;

                    ltInsDt.Text = strDate.Substring(0, 4) + "-" + strDate.Substring(4, 2) + "-" + strDate.Substring(6, 2);
                    //StringBuilder sbInsdDt = new StringBuilder();

                    //sbInsdDt.Append("<a href='#'><img align='absmiddle' onclick=\"Calendar(this, '");
                    //sbInsdDt.Append(txtInsDt.ClientID);
                    //sbInsdDt.Append("', '");
                    //sbInsdDt.Append(hfInsDt.ClientID);
                    //sbInsdDt.Append("', true)\" src='/Common/Images/Common/calendar.gif' style='cursor:pointer;' value='' /></a>");

                    //ltInsDt.Text = sbInsdDt.ToString();
                }

                //ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                //imgbtnModify.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_MODIFY_ITEM"] + "');";

                //ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                //imgbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ITEM"] + "');";
            }
        }

        protected void lvSectionList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Literal ltFloor = (Literal)lvSectionList.Items[e.ItemIndex].FindControl("ltFloor");
                Literal ltSection = (Literal)lvSectionList.Items[e.ItemIndex].FindControl("ltSection");
                TextBox txtCompNm = (TextBox)lvSectionList.Items[e.ItemIndex].FindControl("txtCompNm");
                TextBox txtLeasingArea = (TextBox)lvSectionList.Items[e.ItemIndex].FindControl("txtLeasingArea");
                HiddenField hfInsDt = (HiddenField)lvSectionList.Items[e.ItemIndex].FindControl("hfInsDt");
                TextBox txtHfOriginDt = (TextBox)lvSectionList.Items[e.ItemIndex].FindControl("txtHfOriginDt");
                HiddenField hfCompSeq = (HiddenField)lvSectionList.Items[e.ItemIndex].FindControl("hfCompSeq");

                string strHfOriginDt = txtHfOriginDt.Text.Replace("-", "");
                string strHfInsDt = hfInsDt.Value.Replace("-", "");

                if (Int32.Parse(strHfOriginDt) < Int32.Parse(strHfInsDt))
                {
                    int intCompSeq = 0;
                    string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                    if (!string.IsNullOrEmpty(hfCompSeq.Value))
                    {
                        intCompSeq = Int32.Parse(hfCompSeq.Value);
                    }

                    // KN_USP_RES_INSERT_ROOMINFO_M00
                    RoomMngBlo.RegistryRoomInfo(txtHfRentCd.Text, Int32.Parse(ltFloor.Text), ltSection.Text, strHfInsDt, double.Parse(txtLeasingArea.Text), intCompSeq,
                                                txtCompNm.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                    Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
                }
                else
                {
                    hfAlertText.Value = AlertNm["ALERT_INSERT_LATERDT"];
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvSectionList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                Literal ltTmpFloor = (Literal)lvSectionList.Items[e.ItemIndex].FindControl("ltFloor");
                Literal ltTmpSection = (Literal)lvSectionList.Items[e.ItemIndex].FindControl("ltSection");

                // KN_USP_RES_DELETE_ROOMINFO_M00
                RoomMngBlo.RemoveRoomInfo(txtHfRentCd.Text, Int32.Parse(ltTmpFloor.Text), ltTmpSection.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlFloor_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

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
                //// 세션체크
                //AuthCheckLib.CheckSession();

                //if (fuExcelUpload.HasFile)
                //{
                //    char[] chDiv = { '.' };
                //    string strFileType = fuExcelUpload.PostedFile.ContentType.ToString();
                //    string[] strTmpArray = fuExcelUpload.PostedFile.FileName.Split(chDiv);
                //    string strExtension = string.Empty;

                //    DataTable dtTmpTable = new DataTable();

                //    if (strTmpArray.Length > 0)
                //    {
                //        bool isFailInsert = CommValue.AUTH_VALUE_FALSE;
                //        strExtension = strTmpArray[strTmpArray.Length - 1];

                //        if (strFileType == CommValue.EXCEL_CONTTYPE_VALUE_XLS && strExtension.ToLower().Equals(CommValue.EXCEL_TYPE_TEXT_XLS))
                //        {
                //            // Excel Data 리딩
                //            ExcelReaderLib erReader = new ExcelReaderLib();
                //            DataTable dtTable = new DataTable();

                //            dtTable = erReader.ExtractDataTable(fuExcelUpload.PostedFile.FileName);

                //            // 각 컬럼별 Validation 체크 후 등록
                //            foreach (DataRow dr in dtTable.Select())
                //            {
                //                string strSectionCd = string.Empty;
                //                string strFloorNo = string.Empty;
                //                string strSectionNo = string.Empty;
                //                string strCompNm = string.Empty;
                //                string strLeasingArea = string.Empty;
                //                string strAppliedDt = string.Empty;
                //                string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //                if (!string.IsNullOrEmpty(dr["SectionCd"].ToString()))
                //                {
                //                    strSectionCd = dr["SectionCd"].ToString();
                //                }
                //                else
                //                {
                //                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                //                    break;
                //                }

                //                if (!string.IsNullOrEmpty(dr["FloorNo"].ToString()))
                //                {
                //                    strFloorNo = dr["FloorNo"].ToString();
                //                }
                //                else
                //                {
                //                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                //                    break;
                //                }

                //                if (!string.IsNullOrEmpty(dr["SectionNo"].ToString()))
                //                {
                //                    strSectionNo = dr["SectionNo"].ToString();
                //                }
                //                else
                //                {
                //                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                //                    break;
                //                }

                //                if (!string.IsNullOrEmpty(dr["CompNm"].ToString()))
                //                {
                //                    strCompNm = dr["CompNm"].ToString();
                //                }

                //                if (!string.IsNullOrEmpty(dr["LeasingArea"].ToString()))
                //                {
                //                    strLeasingArea = dr["LeasingArea"].ToString();
                //                }
                //                else
                //                {
                //                    strLeasingArea = "0";
                //                }

                //                if (!string.IsNullOrEmpty(dr["AppliedDt"].ToString()))
                //                {
                //                    strAppliedDt = dr["AppliedDt"].ToString();
                //                }
                //                else
                //                {
                //                    isFailInsert = CommValue.AUTH_VALUE_TRUE;
                //                    break;
                //                }

                //                // 신규데이터 등록
                //                hfAlertText.Value = AlertNm["INFO_REGIST_ITEM"];

                //                // KN_USP_RES_INSERT_ROOMINFO_M00
                //                RoomMngBlo.RegistryRoomInfo(strSectionCd, Int32.Parse(strFloorNo), strSectionNo, strAppliedDt, double.Parse(strLeasingArea), 0, TextLib.StringEncoder(TextLib.MakeNullToEmpty(strCompNm)),
                //                                            Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);
                //            }

                //            if (isFailInsert)
                //            {
                //                hfAlertText.Value = AlertNm["ALERT_INSERT_BLANK"];
                //                LoadData();
                //            }
                //            else
                //            {
                //                hfAlertText.Value = AlertNm["INFO_REGIST_ITEM"];
                //                Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
                //            }
                //        }
                //        else
                //        {
                //            hfAlertText.Value = AlertNm["INFO_PERMIT_ONLY_XLS"];
                //            LoadData();
                //        }
                //    }
                //    else
                //    {
                //        hfAlertText.Value = AlertNm["ALERT_SELECT_FILE"];
                //        LoadData();
                //    }
                //}
                //else
                //{
                //    hfAlertText.Value = AlertNm["ALERT_SELECT_FILE"];
                //    LoadData();
                //}
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnEntireReset_Click(object sender, EventArgs e)
        {
            try
            {
                //// 세션체크
                //AuthCheckLib.CheckSession();

                //string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //// KN_USP_RES_DELETE_ROOMINFO_M02
                //RoomMngBlo.RemoveRoomBuilding(txtHfRentCd.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                //LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnFloorReset_Click(object sender, EventArgs e)
        {
            try
            {
                //// 세션체크
                //AuthCheckLib.CheckSession();

                //string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //// KN_USP_RES_DELETE_ROOMINFO_M01
                //RoomMngBlo.RemoveRoomFloor(txtHfRentCd.Text, Int32.Parse(ddlFloor.SelectedValue), Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                //LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void imgbtnRegist_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                //// 세션체크
                //AuthCheckLib.CheckSession();

                //int intFloor = Int32.Parse(txtFloor.Text);
                //int intTenantSeq = 0;

                //string strRoomNo = txtSection.Text;
                //double fltLeasingArea = double.Parse(txtLeasingArea.Text);
                //string strStartDt = hfInsDt.Value.Replace("-", "");

                //if (!string.IsNullOrEmpty(hfCompSeq.Value))
                //{
                //    intTenantSeq = Int32.Parse(hfCompSeq.Value);
                //}

                //string strInsMemIP = Request.ServerVariables["REMOTE_ADDR"].ToString();

                //// KN_USP_RES_SELECT_ROOMINFO_S07
                //DataTable dtReturn = RoomMngBlo.WatchRoomExistList(txtHfRentCd.Text, intFloor, strRoomNo, strStartDt);

                //if (dtReturn != null)
                //{
                //    if (dtReturn.Rows.Count > 0)
                //    {
                //        if (Int32.Parse(dtReturn.Rows[0]["ExistCnt"].ToString()) > 0)
                //        {
                //            hfAlertText.Value = AlertNm["INFO_CANT_INSERT_DEPTH"];
                //            LoadData();
                //        }
                //        else
                //        {
                //            // KN_USP_RES_INSERT_ROOMINFO_M00
                //            RoomMngBlo.RegistryRoomInfo(txtHfRentCd.Text, intFloor, strRoomNo, strStartDt, fltLeasingArea, intTenantSeq, txtCompNm.Text, 
                //                                        Session["CompCd"].ToString(), Session["MemNo"].ToString(), strInsMemIP);

                //            Response.Redirect(Master.PAGE_LIST + "?" + Master.PARAM_DATA1 + "=" + txtHfRentCd.Text, CommValue.AUTH_VALUE_FALSE);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}