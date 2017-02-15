using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Stock.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupCompFind : BasePage
    {
        public const string PARAM_DATA1 = "CompTy";
        public const string PARAM_DATA2 = "CompCd";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        DataTable dtReturn = new DataTable();

                        // KN_USP_STK_SELECT_COMPINFO_S02
                        dtReturn = CompInfoBlo.SpreadPopupCompInfo(Session["LangCd"].ToString());

                        if (dtReturn != null)
                        {
                            lvCompList.DataSource = dtReturn;
                            lvCompList.DataBind();
                        }
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
            if (Session["FindCompYn"] != null)
            {
                // 메뉴 관리 페이지를 거쳐서 넘어오지 않을 경우 리턴
                if (Session["FindCompYn"].ToString().Equals(CommValue.CONCLUSION_TYPE_TEXT_YES))
                {
                    // 인자값이 제대로 넘어오지 않을 경우 리턴
                    if (Request.Params[PARAM_DATA1] != null)
                    {
                        if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA1].ToString()) &&
                            !string.IsNullOrEmpty(Request.Params[PARAM_DATA2].ToString()))
                        {
                            HfReturnTxtBox.Value = Request.Params[PARAM_DATA1].ToString();
                            HfReturnCdBox.Value = Request.Params[PARAM_DATA2].ToString();
                            Session["FindCompYn"] = null;
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
        /// 컨트롤 초기화
        /// </summary>
        protected void InitControls()
        {
            ltCompNo.Text = TextNm["SEQ"];
            ltCompNm.Text = TextNm["COMPNM"];
            ltBizIndusNm.Text = TextNm["INDUS"];
            ltTelNo.Text = TextNm["TEL"];

            lnkbtnCancel.Text = TextNm["CANCEL"];
        }


        /// <summary>
        /// ListView에서 레이아웃의 각 컨트롤 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCompList_LayoutCreated(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCompList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    ((Literal)e.Item.FindControl("ltCompNo")).Text = TextNm["SEQ"];
                    ((Literal)e.Item.FindControl("ltCompNm")).Text = TextNm["COMPNM"];
                    ((Literal)e.Item.FindControl("ltBizIndusNm")).Text = TextNm["INDUS"];
                    ((Literal)e.Item.FindControl("ltTelNo")).Text = TextNm["TEL"];

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
        /// 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvCompList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["CompNo"].ToString()))
                {
                    Literal ltDataCompNo = (Literal)e.Item.FindControl("ltDataCompNo");
                    ltDataCompNo.Text = drView["CompNo"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompNm"].ToString()))
                {
                    Literal ltDataCompNm = (Literal)e.Item.FindControl("ltDataCompNm");
                    ltDataCompNm.Text = drView["CompNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["BizIndusNm"].ToString()))
                {
                    Literal ltDataBizIndusNm = (Literal)e.Item.FindControl("ltDataBizIndusNm");
                    ltDataBizIndusNm.Text = drView["BizIndusNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["CompTelFrontNo"].ToString()) &&
                    !string.IsNullOrEmpty(drView["CompTelMidNo"].ToString()) &&
                    !string.IsNullOrEmpty(drView["CompTelRearNo"].ToString()))
                {
                    Literal ltDataTelNo = (Literal)e.Item.FindControl("ltDataTelNo");

                    ltDataTelNo.Text = drView["CompTelFrontNo"].ToString() + "-" + drView["CompTelMidNo"].ToString() + "-" + drView["CompTelRearNo"].ToString();
                }
            }
        }
    }
}
