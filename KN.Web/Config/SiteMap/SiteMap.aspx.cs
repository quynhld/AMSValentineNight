using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using KN.Common.Method.Lib;

namespace KN.Web.Config.SiteMap
{
    public partial class SiteMap : BasePage
    {
        string Depth1 = string.Empty;
        int intRowCnt = CommValue.NUMBER_VALUE_0;

        bool isStart = CommValue.AUTH_VALUE_TRUE;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                if (!IsPostBack)
                {
                    // 컨트롤 초기화
                    InitControls();

                    // 데이터 로드
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

        }

        /// <summary>
        /// 데이터를 로드하는 메소드
        /// </summary>
        private void LoadData()
        {
            DataTable dtReturn = new DataTable();

            // 사이트맵 리스트 조회
            // KN_USP_COMM_SELECT_SITEMAP_S00
            dtReturn = SiteMapUtil.SpreadSiteMapInfo(Session["LangCd"].ToString(), Session["MemAuthTy"].ToString());

            if (dtReturn != null)
            {
                lvSiteMapList.DataSource = dtReturn;
                lvSiteMapList.DataBind();

            }
        }

        /// <summary>
        /// ListView내부 테이블의 타이틀 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvSiteMapList_LayoutCreated(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ListView에서 빈 데이터의 경우 타이틀 및 알림메세지 정의
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvSiteMapList_ItemCreated(object sender, ListViewItemEventArgs e)
        {

        }

        /// <summary>
        /// ListView에서 데이터 바인딩
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvSiteMapList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            ListViewDataItem iTem = (ListViewDataItem)e.Item;
            System.Data.DataRowView drView = (System.Data.DataRowView)iTem.DataItem;

            if (e.Item.ItemType.Equals(ListViewItemType.DataItem))
            {

                StringBuilder sbContent = new StringBuilder();

                if (!string.IsNullOrEmpty(drView["Depth1"].ToString()))
                {
                    if (!string.IsNullOrEmpty(drView["Depth2"].ToString()))
                    {
                        if (!string.IsNullOrEmpty(drView["Depth3"].ToString()))
                        {

                            // Depth1 메뉴 
                            if (drView["Depth2"].ToString().Equals("0") && drView["Depth3"].ToString().Equals("0"))
                            {
                                Literal ltContent = (Literal)iTem.FindControl("ltContent");
                                ltContent.Text = drView["MenuNm"].ToString();

                                StringBuilder sbDepth1 = new StringBuilder();

                                if (isStart)
                                {
                                    isStart = CommValue.AUTH_VALUE_FALSE;
                                    intRowCnt++;
                                }
                                else
                                {
                                    if (intRowCnt >= 3)
                                    {
                                        // 네번째 사이트맵 에서 떨어뜨림
                                        if (intRowCnt % 3 == 0)
                                        {
                                            sbDepth1.Append("</table></div><div class='Clear'></div>");
                                            sbDepth1.Append("<div class='stmap'><table>");
                                            intRowCnt++;
                                        }
                                        else
                                        {
                                            sbDepth1.Append("</table></div><div class='stmap'><table>");
                                            intRowCnt++;
                                        }

                                    }
                                    else
                                    {
                                        sbDepth1.Append("</table></div><div class='stmap'><table>");
                                        intRowCnt++;
                                    }

                                }

                                sbDepth1.Append("<th class='dp1'>");
                                sbDepth1.Append(ltContent.Text);
                                sbDepth1.Append("</th>");

                                ltContent.Text = sbContent.Append(sbDepth1).ToString();
                            }

                            // Depth2 메뉴
                            else if (!drView["Depth2"].ToString().Equals("0") && drView["Depth3"].ToString().Equals("0"))
                            {
                                Literal ltContent = (Literal)iTem.FindControl("ltContent");
                                ltContent.Text = drView["MenuNm"].ToString();

                                StringBuilder sbDepth1 = new StringBuilder();
                                sbDepth1.Append("<th><div class='Txt-Ftype2-wp'><div class='Txt-Ftype2-L'><div class='Txt-Ftype2-R'><div class='Txt-Ftype2-M'><span>");
                                sbDepth1.Append(ltContent.Text);
                                sbDepth1.Append("</span></div></div></div></div></th>");

                                ltContent.Text = sbContent.Append(sbDepth1).ToString();
                            }

                            // Depth3 메뉴
                            else
                            {
                                Literal ltContent = (Literal)iTem.FindControl("ltContent");
                                ltContent.Text = drView["MenuNm"].ToString();

                                StringBuilder sbDepth1 = new StringBuilder();
                                sbDepth1.Append("<td class='dp3'>");
                                sbDepth1.Append("<a href=" + drView["MenuUrl"].ToString() + ">" + ltContent.Text + "</a>");
                                sbDepth1.Append("</td>");

                                ltContent.Text = sbContent.Append(sbDepth1).ToString();
                            }
                        }
                    }
                }

            }
        }
    }
}