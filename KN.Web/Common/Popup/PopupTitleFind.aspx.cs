using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;

using KN.Config.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupTitleFind : BasePage
    {
        public const string PARAM_DATA1 = "TitleCd";

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

                        // KN_USP_MNG_SELECT_MENUTXTINFO_S00
                        dtReturn = TxtMngBlo.SpreadMenuTxtInfo(string.Empty, string.Empty);

                        if (dtReturn != null)
                        {
                            lvTitleList.DataSource = dtReturn;
                            lvTitleList.DataBind();
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
            // 인자값이 제대로 넘어오지 않을 경우 리턴
            if (Request.Params[PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA1].ToString()))
                {
                    HfReturnBox.Value = Request.Params[PARAM_DATA1].ToString();
                    //Session["FindTitleYn"] = null;
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

            return isReturn;
        }

        /// <summary>
        /// 컨트롤초기화
        /// </summary>
        protected void InitControls()
        {
            ltExpressCd.Text = TextNm["EXPRESSCD"];
            ltMenuVi.Text = TextNm["VIETLANG"];
            ltMenuEn.Text = TextNm["ENLANG"];
            ltMenuKr.Text = TextNm["KORLANG"];

            lnkbtnCancel.Text = TextNm["CANCEL"];
        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvTitleList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            try
            {
                // ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
                if (e.Item.ItemType.Equals(ListViewItemType.EmptyItem))
                {
                    ((Literal)e.Item.FindControl("ltExpressCd")).Text = TextNm["EXPRESSCD"];
                    ((Literal)e.Item.FindControl("ltMenuVi")).Text = TextNm["VIETLANG"];
                    ((Literal)e.Item.FindControl("ltMenuEn")).Text = TextNm["ENLANG"];
                    ((Literal)e.Item.FindControl("ltMenuKr")).Text = TextNm["KORLANG"];

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
        protected void lvTitleList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {
                ListViewDataItem iTem = (ListViewDataItem)e.Item;
                System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["ExpressCd"].ToString()))
                {
                    Literal ltExpressCd = (Literal)e.Item.FindControl("ltDataExpressCd");
                    ltExpressCd.Text = drView["ExpressCd"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MenuVi"].ToString()))
                {
                    Literal ltMenuVi = (Literal)e.Item.FindControl("ltDataMenuVi");
                    ltMenuVi.Text = drView["MenuVi"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MenuEn"].ToString()))
                {
                    Literal ltMenuEn = (Literal)e.Item.FindControl("ltDataMenuEn");
                    ltMenuEn.Text = drView["MenuEn"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["MenuKr"].ToString()))
                {
                    Literal ltMenuKr = (Literal)e.Item.FindControl("ltDataMenuKr");
                    ltMenuKr.Text = drView["MenuKr"].ToString();
                }
            }
        }
    }
}