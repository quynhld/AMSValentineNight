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

using KN.Parking.Biz;

namespace KN.Web.Park
{
    public partial class ParkingCardList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        int intPageNo = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    InitControls();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            ltSearchCardNo.Text = TextNm["CARDNO"];
            lnkbtnSearch.Text = TextNm["SEARCH"];
            ltAddonFile.Text = TextNm["EXCELUPLOAD"];

            imgbtnRegist.OnClientClick = "javascript:return fnIssuingCheck('" + AlertNm["CONF_PRCEED_WORK"] + "','" + AlertNm["ALERT_INSERT_BLANK"] + "');";

            fuExcelUpload.Attributes["onchange"] = "javascript:fnCheckFileTypeExcel(this,'" + AlertNm["INFO_PERMIT_ONLY_XLS"] + "');";

            lnkbtnFileUpload.Text = TextNm["EXCELUPLOAD"];
            lnkbtnFileUpload.OnClientClick = "javascript:return fnCheckFileUpload('" + AlertNm["ALERT_SELECT_FILE"] + "');";

            CommCdDdlUtil.MakeEtcSubCdDdlTitle(ddlCarTyCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_CARTY);
            CommCdDdlUtil.MakeEtcSubCdDdlTitle(ddlInsCarTyCd, Session["LangCd"].ToString(), CommValue.ETCCD_VALUE_CARTY);
            MakeInvoiceYN();
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

        protected void LoadData()
        {
            string strKeyWord = string.Empty;

            CheckParams();

            strKeyWord = txtKeyWord.Text;

            DataSet dsReturn = new DataSet();

            if (!string.IsNullOrEmpty(strKeyWord))
            {
                if (strKeyWord.Length < 8)
                {
                    strKeyWord = txtKeyWord.Text.PadLeft(8, '0');
                }
            }

            // KN_USP_PRK_SELECT_PARKINGTAGLISTINFO_S01
            dsReturn = ParkingMngBlo.SpreadParkingTagListInfo(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), strKeyWord, ddlCarTyCd.SelectedValue, Session["LangCd"].ToString(), ddlIssYN.SelectedValue.ToString());

            if (dsReturn != null)
            {
                lvTagList.DataSource = dsReturn.Tables[1];
                lvTagList.DataBind();

                sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(dsReturn.Tables[0].Rows[0]["TotalCnt"].ToString())
                    , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));

                spanPageNavi.InnerHtml = sbPageNavi.ToString();
            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvTagList_LayoutCreated(object sender, EventArgs e)
        {
            // ListView내부 테이블의 타이틀 정의
            ((Literal)lvTagList.FindControl("ltSeq")).Text = TextNm["SEQ"];
            ((Literal)lvTagList.FindControl("ltCardNo")).Text = TextNm["CARDNO"];
            ((Literal)lvTagList.FindControl("ltTagNo")).Text = TextNm["PARKINGTAGNO"];
            ((Literal)lvTagList.FindControl("ltCarTyCd")).Text = TextNm["CARTY"];
            ((Literal)lvTagList.FindControl("ltIssuedYn")).Text = TextNm["ISSUING"];
        }

        protected void lvTagList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    // ListView에서 빈 데이터의 경우 알림메세지 정의
                    Literal ltSeq = (Literal)e.Item.FindControl("ltSeq");
                    ltSeq.Text = TextNm["SEQ"];
                    Literal ltCardNo = (Literal)e.Item.FindControl("ltCardNo");
                    ltCardNo.Text = TextNm["CARDNO"];
                    Literal ltTagNo = (Literal)e.Item.FindControl("ltTagNo");
                    ltTagNo.Text = TextNm["PARKINGTAGNO"];
                    Literal ltCarTyCd = (Literal)e.Item.FindControl("ltCarTyCd");
                    ltCarTyCd.Text = TextNm["CARTY"];
                    Literal ltIssuedYn = (Literal)e.Item.FindControl("ltIssuedYn");
                    ltIssuedYn.Text = TextNm["ISSUING"];

                    ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvTagList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                if (!string.IsNullOrEmpty(drView["RealSeq"].ToString()))
                {
                    Literal ltSeq = (Literal)iTem.FindControl("ltSeq");
                    ltSeq.Text = drView["RealSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CardSeq"].ToString()))
                {
                    TextBox txtHfSeq = (TextBox)iTem.FindControl("txtHfSeq");
                    txtHfSeq.Text = drView["CardSeq"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CardNo"].ToString()))
                {
                    Literal ltCardNo = (Literal)iTem.FindControl("ltCardNo");
                    ltCardNo.Text = drView["CardNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["TagNo"].ToString()))
                {
                    Literal ltTagNo = (Literal)iTem.FindControl("ltTagNo");

                    ltTagNo.Text = drView["TagNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CarTyCdNm"].ToString()))
                {
                    Literal ltCarTyCd = (Literal)iTem.FindControl("ltCarTyCd");
                    ltCarTyCd.Text = drView["CarTyCdNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["IssuedYn"].ToString()))
                {
                    DropDownList ddlIssuedYn = (DropDownList)iTem.FindControl("ddlIssuedYn");
                    ddlIssuedYn.SelectedValue = drView["IssuedYn"].ToString();

                    if (ddlIssuedYn.SelectedValue.Equals(CommValue.CHOICE_VALUE_YES))
                    {
                        ddlIssuedYn.Enabled = CommValue.AUTH_VALUE_FALSE;
                    }
                    else
                    {
                        ddlIssuedYn.Enabled = CommValue.AUTH_VALUE_TRUE;
                    }
                }

                ImageButton imgbtnDelete = (ImageButton)iTem.FindControl("imgbtnDelete");
                imgbtnDelete.OnClientClick = "javascript:return fnValidateCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";
                ImageButton imgbtnModify = (ImageButton)iTem.FindControl("imgbtnModify");
                imgbtnModify.OnClientClick = "javascript:return fnValidateCheck('" + AlertNm["CONF_PRCEED_WORK"] + "');";
            }
        }

        protected void lvTagList_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intSeq = 0;

                TextBox txtHfSeq = (TextBox)lvTagList.Items[e.ItemIndex].FindControl("txtHfSeq");
                intSeq = Int32.Parse(txtHfSeq.Text);

                // KN_USP_PRK_DELETE_PARKINGTAGLISTINFO_M00
                ParkingMngBlo.RemoveTagListInfo(intSeq);

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvTagList_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                int intSeq = 0;
                string strStatement = string.Empty;

                TextBox txtHfSeq = (TextBox)lvTagList.Items[e.ItemIndex].FindControl("txtHfSeq");
                intSeq = Int32.Parse(txtHfSeq.Text);

                // KN_USP_PRK_UPDATE_PARKINGTAGLISTINFO_S00
                DataTable dtReturn = ParkingMngBlo.ModifyTagListInfo(intSeq);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        if (dtReturn.Rows[0]["ReturnYn"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                        {
                            strStatement = AlertNm["INFO_MODIFY_ITEM"];
                        }
                        else
                        {
                            strStatement = AlertNm["INFO_CANCEL"];
                        }
                    }
                }
                else
                {
                    strStatement = AlertNm["INFO_CANCEL"];
                }

                StringBuilder sb = new StringBuilder();

                sb.Append("alert('");
                sb.Append(strStatement);
                sb.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sb.ToString(), CommValue.AUTH_VALUE_TRUE);

                LoadData();

            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 조회버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                LoadData();
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }

        }

        /// <summary>
        /// 페이징버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
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
                // 세션체크
                AuthCheckLib.CheckSession();

                string strStatement = string.Empty;

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

                        //strFileType == CommValue.EXCEL_CONTTYPE_VALUE_XLSX && strExtension.ToLower().Equals(CommValue.EXCEL_TYPE_TEXT_XLSX
                        if (strFileType == CommValue.EXCEL_CONTTYPE_VALUE_XLS && strExtension.ToLower().Equals(CommValue.EXCEL_TYPE_TEXT_XLS))
                        {
                            // Excel Data 리딩
                            ExcelReaderLib erReader = new ExcelReaderLib();
                            DataTable dtTable = new DataTable();

                            dtTable = erReader.ExtractDataTable(fuExcelUpload.PostedFile.FileName);

                            int intRowCnt = CommValue.NUMBER_VALUE_0;

                            // 각 컬럼별 Validation 체크 후 등록
                            foreach (DataRow dr in dtTable.Select())
                            {
                                string strCardNo = string.Empty;
                                string strTagNo = string.Empty;
                                string strCarTyCd = string.Empty;

                                if (!string.IsNullOrEmpty(dr["CardNo"].ToString()))
                                {
                                    strCardNo = dr["CardNo"].ToString();
                                }
                                else
                                {
                                    if (intRowCnt == CommValue.NUMBER_VALUE_0)
                                    {
                                        isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                        break;
                                    }
                                }

                                if (!string.IsNullOrEmpty(dr["TagNo"].ToString()))
                                {
                                    strTagNo = dr["TagNo"].ToString();
                                }
                                else
                                {
                                    if (intRowCnt == CommValue.NUMBER_VALUE_0)
                                    {
                                        isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                        break;
                                    }
                                }

                                if (!string.IsNullOrEmpty(dr["CarTyCd"].ToString()))
                                {
                                    strCarTyCd = dr["CarTyCd"].ToString();
                                }
                                else
                                {
                                    if (intRowCnt == CommValue.NUMBER_VALUE_0)
                                    {
                                        isFailInsert = CommValue.AUTH_VALUE_TRUE;
                                        break;
                                    }
                                }

                                // KN_USP_PRK_INSERT_PARKINGTAGLISTINFO_S00
                                ParkingMngBlo.RegistryTagListInfo(strCardNo, strTagNo, strCarTyCd, CommValue.CHOICE_VALUE_YES, CommValue.CHOICE_VALUE_NO, CommValue.CHOICE_VALUE_YES);

                                intRowCnt++;
                            }

                            if (isFailInsert)
                            {
                                strStatement = AlertNm["ALERT_INSERT_BLANK"];
                            }
                            else
                            {
                                strStatement = AlertNm["INFO_REGIST_ITEM"];
                            }
                        }
                        else
                        {
                            strStatement = AlertNm["INFO_PERMIT_ONLY_XLS"];
                        }
                    }
                    else
                    {
                        strStatement = AlertNm["ALERT_SELECT_FILE"];
                    }
                }
                else
                {
                    strStatement = AlertNm["ALERT_SELECT_FILE"];
                }

                StringBuilder sb = new StringBuilder();
                
                sb.Append("alert('");
                sb.Append(strStatement);
                sb.Append("');");
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sb.ToString(), CommValue.AUTH_VALUE_TRUE);

                LoadData();
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
                // 세션 체크
                AuthCheckLib.CheckSession();

                string strCardNo = txtInsCardNo.Text.PadLeft(8, '0');
                string strTagNo = txtInsTagNo.Text;
                string strCarTyCd = ddlInsCarTyCd.SelectedValue;
                string strIssueYn = ddlInsIssuedYn.SelectedValue;

                DataTable dtReturn = new DataTable();

                // KN_USP_PRK_INSERT_PARKINGTAGLISTINFO_S00
                dtReturn = ParkingMngBlo.RegistryTagListInfo(strCardNo, strTagNo, strCarTyCd, strIssueYn, CommValue.CHOICE_VALUE_NO, CommValue.CHOICE_VALUE_NO);

                if (dtReturn != null)
                {
                    if (dtReturn.Rows.Count > CommValue.NUMBER_VALUE_0)
                    {
                        StringBuilder sbWarning = new StringBuilder();

                        if (dtReturn.Rows[0]["ReturnYn"].ToString().Equals(CommValue.CHOICE_VALUE_YES))
                        {
                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["INFO_REGIST_ITEM"]);
                            sbWarning.Append("');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);

                            txtInsCardNo.Text = string.Empty;
                            txtInsTagNo.Text = string.Empty;
                            ddlInsCarTyCd.SelectedValue = "0000";
                            ddlInsIssuedYn.SelectedValue = CommValue.CHOICE_VALUE_YES;

                            hfCurrentPage.Value = CommValue.NUMBER_VALUE_ONE;

                            LoadData();
                        }
                        else
                        {
                            sbWarning.Append("alert('");
                            sbWarning.Append(AlertNm["INFO_CANT_INSERT_DEPTH"]);
                            sbWarning.Append("');");

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlIssYN_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void MakeInvoiceYN()
        {
            ddlIssYN.Items.Clear();
            ddlIssYN.Items.Add(new ListItem("No", "N"));
            ddlIssYN.Items.Add(new ListItem("Yes", "Y"));
        }
    }
}
