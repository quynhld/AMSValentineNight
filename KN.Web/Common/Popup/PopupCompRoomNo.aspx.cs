using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Resident.Biz;
using KN.Stock.Biz;

namespace KN.Web.Common.Popup
{
    public partial class PopupCompRoomNo : BasePage
    {
        public const string PARAM_DATA1 = "CompID";
        public const string PARAM_DATA2 = "RoomID";
        public const string PARAM_DATA3 = "UserSeqId";
        public const string PARAM_DATA4 = "CompNmS";
        public const string PARAM_DATA5 = "RentCdS";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();                       
                      // KN_USP_STK_SELECT_COMPINFO_S02
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
            bool isReturn;

            // PreCondition
            // 1. 메뉴 관리 페이지를 거쳐서 넘어오지 않을 경우 리턴
            // 2. 인자값이 제대로 넘어오지 않을 경우 리턴
            // 메뉴 관리 페이지를 거쳐서 넘어오지 않을 경우 리턴
            // 인자값이 제대로 넘어오지 않을 경우 리턴
            if (Request.Params[PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[PARAM_DATA1]))
                {
                    HfReturnCompNmID.Value = Request.Params[PARAM_DATA1];
                    HfReturnRoomNoId.Value = Request.Params[PARAM_DATA2];
                    HfReturnUserSeqId.Value = Request.Params[PARAM_DATA3];
                    hfCompNmS.Value = Request.Params[PARAM_DATA4];
                    //txtCompNm.Text = Request.Params[PARAM_DATA4];                   
                    hfRentCd.Value = Request.Params[PARAM_DATA5];                  
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
        /// 컨트롤 초기화
        /// </summary>
        protected void InitControls()
        {
            ltCompNo.Text = TextNm["SEQ"];
            ltCompNm.Text = TextNm["COMPNM"];
            ltBizIndusNm.Text = "Room No";
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
                if (!e.Item.ItemType.Equals(ListViewItemType.EmptyItem)) return;
                ((Literal)e.Item.FindControl("ltCompNo")).Text = TextNm["UserSeq"];
                ((Literal)e.Item.FindControl("ltCompNm")).Text = TextNm["UserNm"];
                ((Literal)e.Item.FindControl("ltBizIndusNm")).Text = TextNm["RoomNo"];               

                // ListView에서 빈 데이터의 경우 알림메세지 정의
                ((Literal)e.Item.FindControl("ltINFO_HAS_NO_DATA")).Text = AlertNm["INFO_HAS_NO_DATA"];
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
                var iTem = (ListViewDataItem)e.Item;
                var drView = (DataRowView)iTem.DataItem;

                if (!string.IsNullOrEmpty(drView["SEQ"].ToString()))
                {
                    var ltDataCompNo = (Literal)e.Item.FindControl("ltDataCompNo");
                    ltDataCompNo.Text = drView["SEQ"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["UserNm"].ToString()))
                {
                    var ltDataCompNm = (Literal)e.Item.FindControl("ltDataCompNm");
                    ltDataCompNm.Text = drView["UserNm"].ToString();
                }

                if (!string.IsNullOrEmpty(drView["RoomNo"].ToString()))
                {
                    var ltDataBizIndusNm = (Literal)e.Item.FindControl("ltDataBizIndusNm");
                    ltDataBizIndusNm.Text = drView["RoomNo"].ToString();
                }
            }
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        protected void LoadData()
        {
            //KN_USP_RES_SELECT_ROOMINFO_S09
            var dtReturn = RoomMngBlo.WatchUserListInfo(hfRentCd.Value, txtInsRoomNo.Text, txtCompNm.Text);

            if (dtReturn == null) return;
            lvCompList.DataSource = dtReturn;
            lvCompList.DataBind();
        }
    }
}
