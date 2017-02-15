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

using KN.Board.Biz;

namespace KN.Web.Board.Memo
{
    public partial class MemoWrite : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    // 컨트롤 초기화
                    InitControls();

                    LoadData();
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            ltAuthority.Text = TextNm["AUTHCD"];
            ltGrpSelect.Text = TextNm["SELECTGROUP"];
            ltChkAll.Text = TextNm["ENTIRE_SELECTION"];
            ltTitle.Text = TextNm["TITLE"];
            ltContent.Text = TextNm["CONTENTS"];
            ltFileAddon.Text = TextNm["FILEADDON"];

            lnkbtnAdd.Text = TextNm["ADD"];
            lnkbtnDel.Text = TextNm["DELETE"];
            lnkbtnSend.Text = TextNm["SEND"];

            lnkbtnSend.OnClientClick = "javascript:return fnValidateData('" + AlertNm["ALERT_INSERT_TITLE"] + "','" + AlertNm["ALERT_INSERT_CONTEXT"] + "')";

            // 회사코드 세팅
            // KN_USP_COMM_SELECT_MEMCOMPINFO_S00
            MultiCdDdlUtil.MakeMemCompCdNoTitle(ddlAuth, Session["CompCd"].ToString());

            // Group DropDownList Setting
            MakeGroupDdl();
        }

        /// <summary>
        /// 데이터 로드 및 바인딩
        /// </summary>
        private void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_COMM_SELECT_GRPCD_S02
            dtReturn = MemoWriteUtil.SpreadGroupMemInfo(ddlAuth.SelectedValue, txtHfGrpMemNo.Text, Session["LangCd"].ToString());

            lvMemberList.DataSource = dtReturn;
            lvMemberList.DataBind();
        }

        /// <summary>
        /// 그룹 DropdownList 생성
        /// </summary>
        private void MakeGroupDdl()
        {
            // KN_USP_COMM_SELECT_GRPCD_S01
            MemoWriteUtil.MakeGroupDdl(ddlGrpSelect, ddlAuth.SelectedValue, Session["LangCd"].ToString());

            ddlGrpSelect.Items.Insert(0, new ListItem(TextNm["ENTIRE"], ""));
        }

        protected void ddlAuth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtReturn = new DataTable();

                // KN_USP_COMM_SELECT_GRPCD_S02
                dtReturn = MemoWriteUtil.SpreadGroupMemInfo(ddlAuth.SelectedValue, txtHfGrpMemNo.Text, Session["LangCd"].ToString());

                lvMemberList.DataSource = dtReturn;
                lvMemberList.DataBind();

                chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void ddlGrpSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtHfGrpMemNo.Text = ddlGrpSelect.SelectedValue.ToString();

                LoadData();

                chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lvMemberList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["MemAuthTy"].ToString()))
                {
                    Literal ltMemberList = (Literal)iTem.FindControl("ltMemberList");
                    ltMemberList.Text = TextLib.StringDecoder(drView["GroupNm"].ToString());

                    TextBox txtHfChkMemNo = (TextBox)iTem.FindControl("txtHfChkMemNo");
                    txtHfChkMemNo.Text = TextLib.StringDecoder(drView["AuthLvl"].ToString() + "_" + drView["MemNo"].ToString());
                }
            }
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMemberList_ItemCreated(object sender, ListViewItemEventArgs e)
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

        /// <summary>
        /// 추가버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            LoadAddMemberData();
        }

        /// <summary>
        /// 삭제버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnDel_Click(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            if (lbAddMemberList.SelectedIndex >= 0)
            {
                lbAddMemberList.Items.RemoveAt(Int32.Parse(lbAddMemberList.SelectedIndex.ToString()));
            }
            else
            {
                StringBuilder sbWarning = new StringBuilder();

                sbWarning.Append("alert('");
                sbWarning.Append(AlertNm["INFO_HAS_NO_SELECTED_ITEM"]);
                sbWarning.Append("');");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
            }
        }

        /// <summary>
        /// 직원 데이터 로드
        /// </summary>
        protected void LoadAddMemberData()
        {
            for (int intTmpI = 0; intTmpI < lvMemberList.Items.Count; intTmpI++)
            {
                TextBox txtHfChkMemNo = (TextBox)lvMemberList.Items[intTmpI].FindControl("txtHfChkMemNo");
                CheckBox chkMemberList = (CheckBox)lvMemberList.Items[intTmpI].FindControl("chkMemberList");

                if (chkMemberList.Checked)
                {
                    if (!string.IsNullOrEmpty(txtHfChkMemNo.Text))
                    {
                        string[] strArrMemNo = txtHfChkMemNo.Text.Split('_');

                        // KN_USP_COMM_SELECT_GRPCD_S03
                        DataTable dtReturn = MemoWriteUtil.SpreadAddMember(strArrMemNo[0], strArrMemNo[1]);

                        foreach (DataRow dr in dtReturn.Select())
                        {
                            bool isExist = CommValue.AUTH_VALUE_FALSE;

                            for (int intTmpj = 0; intTmpj < lbAddMemberList.Items.Count; intTmpj++)
                            {
                                string[] strArrComMemNo = lbAddMemberList.Items[intTmpj].Value.ToString().Split('_');

                                if (dr["CompNo"].ToString().Equals(strArrComMemNo[0]) &&
                                    dr["MemNo"].ToString().Equals(strArrComMemNo[1]))
                                {
                                    isExist = CommValue.AUTH_VALUE_TRUE;
                                    break;
                                }
                            }

                            if (!isExist)
                            {
                                lbAddMemberList.Items.Add(new ListItem(TextLib.StringDecoder(dr["MemNm"].ToString()), dr["CompNo"].ToString() + "_" + dr["MemNo"].ToString()));
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// list내의 로그 선택 checkbox 전체 체크 변경시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        protected void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool isAllCheck = chkAll.Checked;

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
            if (lvMemberList.Items.Count > CommValue.NUMBER_VALUE_0)
            {
                for (int intTmpI = 0; intTmpI < lvMemberList.Items.Count; intTmpI++)
                {
                    ((CheckBox)lvMemberList.Items[intTmpI].FindControl("chkMemberList")).Checked = isAllCheck;
                }
            }
            else
            {
                chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
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

                for (int intTmpI = 0; intTmpI < lvMemberList.Items.Count; intTmpI++)
                {
                    if (((CheckBox)lvMemberList.Items[intTmpI].FindControl("chkMemberList")).Checked)
                    {
                        intCheckCount++;
                    }

                }

                if (intCheckCount == lvMemberList.Items.Count)
                {
                    chkAll.Checked = CommValue.AUTH_VALUE_TRUE;
                }
                else
                {
                    chkAll.Checked = CommValue.AUTH_VALUE_FALSE;
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void lnkbtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                string strUserIP = Request.ServerVariables["REMOTE_ADDR"].ToString();
                string strCompNo = Session["CompCd"].ToString(); 
                string strMemNo = Session["MemNo"].ToString();

                string strOldFileNm = string.Empty;
                string strNewFileNm = string.Empty;

                // KN_USP_BRD_INSERT_MEMOINFO_M00
                MemoWriteUtil.RegistrySendMemoBoard(txtTitle.Text, txtContext.Text, strCompNo, strMemNo, strUserIP);

                if (lbAddMemberList.Items.Count != 0)
                {
                    for (int intTmpI = 0; intTmpI < lbAddMemberList.Items.Count; intTmpI++)
                    {
                        string[] strArrMemInfo = lbAddMemberList.Items[intTmpI].Value.ToString().Split('_');
                        // KN_USP_BRD_INSERT_MEMOINFO_S00
                        DataTable dtReturn = MemoMngBlo.RegistrySendMemoDetail(strArrMemInfo[0], strArrMemInfo[1]);

                        if (dtReturn != null)
                        {
                            if (dtReturn.Rows.Count > 0)
                            {
                                if (fileAddon.Visible)
                                {
                                    if (fileAddon.HasFile)
                                    {
                                        strOldFileNm = fileAddon.FileName;
                                        strNewFileNm = UploadFile(fileAddon);

                                        // KN_USP_BRD_INSERT_MEMOADDON_M00
                                        MemoMngBlo.RegistryMemoAddon(Int32.Parse(dtReturn.Rows[0]["MemoSeq"].ToString()), strNewFileNm, fileAddon.FileBytes.GetLength(0).ToString(), strOldFileNm);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    // KN_USP_BRD_INSERT_MEMOINFO_S00
                    DataTable dtReturn = MemoMngBlo.RegistrySendMemoDetail(strCompNo, strMemNo);

                    if (dtReturn != null)
                    {
                        if (dtReturn.Rows.Count > 0)
                        {
                            if (fileAddon.Visible)
                            {
                                if (fileAddon.HasFile)
                                {
                                    strOldFileNm = fileAddon.FileName;
                                    strNewFileNm = UploadFile(fileAddon);

                                    // KN_USP_BRD_INSERT_MEMOADDON_M00
                                    MemoWriteUtil.RegistryMemoAddon(Int32.Parse(dtReturn.Rows[0]["MemoSeq"].ToString()), strNewFileNm, fileAddon.FileBytes.GetLength(0).ToString(), strOldFileNm);
                                }
                            }
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InsertItem", "javascript:fnAlert('" + AlertNm["INFO_REGIST_ISSUE"] + "','" + Master.PAGE_WRITE + "');document.location.href=\"" + Master.PAGE_LIST + "\";", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 화일을 업로드 처리함.
        /// </summary>
        /// <param name="fuFiles"></param>
        /// <returns></returns>
        public string UploadFile(FileUpload fuFiles)
        {
            string strReturn = string.Empty;
            object[] objReturns = FileLib.UploadImageFiles(fuFiles, "", "Images");

            if (objReturns != null)
            {
                strReturn = objReturns[1].ToString();
                return strReturn;
            }
            else
            {
                return null;
            }
        }
    }
}