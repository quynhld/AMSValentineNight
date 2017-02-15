using System;
using System.Configuration;
using System.Data;
using System.Web.UI;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;

using KN.Stock.Biz;

namespace KN.Web.Stock.Company
{
    public partial class CompView : BasePage
    {
        //파일 업로드 경로 설정
        public static readonly string strDBFileUpload = ConfigurationSettings.AppSettings["UploadDBFolder"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            try
            {
                if (!IsPostBack)
                {
                    if (CheckParams())
                    {
                        InitControls();

                        LoadData();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 파라미터 체크하는 메소드
        /// </summary>
        private bool CheckParams()
        {
            bool isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params[Master.PARAM_DATA1] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params[Master.PARAM_DATA1].ToString()))
                {
                    txtHfCompNo.Text = Request.Params[Master.PARAM_DATA1].ToString();
                    isReturnOk = CommValue.AUTH_VALUE_TRUE;
                }
            }

            return isReturnOk;
        }

        /// <summary>
        /// 컨트롤을 초기화하는 메소드
        /// </summary>
        private void InitControls()
        {
            ltCompNm.Text = TextNm["COMPNM"];
            ltRepresentiveNm.Text = TextNm["CEO"];
            ltChargerNm.Text = TextNm["CHARGERNM"];
            ltIndus.Text = TextNm["INDUS"];
            ltTel.Text = TextNm["TEL"];
            ltFax.Text = TextNm["FAX"];
            ltAddr.Text = TextNm["ADDR"];
            ltCompTy.Text = TextNm["COMPTY"];
            ltFileAddon1.Text = TextNm["FILEADDON"];
            ltFileAddon2.Text = TextNm["FILEADDON"];
            ltFileAddon3.Text = TextNm["FILEADDON"];
            ltIntro.Text = TextNm["INTRO"];

            lnkbtnModify.Text = TextNm["MODIFY"];
            lnkbtnModify.Visible = Master.isModDelAuthOk;
            lnkbtnDelete.Text = TextNm["DELETE"];
            lnkbtnDelete.Visible = Master.isModDelAuthOk;
            lnkbtnList.Text = TextNm["LIST"];

            lnkbtnDelete.OnClientClick = "javascript:return fnConfirm('" + AlertNm["CONF_DELETE_ISSUE"] + "')";            
        }

        /// <summary>
        /// 데이터 로딩
        /// </summary>
        private void LoadData()
        {
            // 세션체크
            AuthCheckLib.CheckSession();

            // KN_USP_STK_SELECT_COMPINFO_S01
            DataTable dtReturn = CompInfoBlo.WatchCompInfo(txtHfCompNo.Text, Session["LangCd"].ToString());

            if (dtReturn != null)
            {
                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompNm"].ToString()))
                {
                    ltInsCompNm.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CompNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["PresidentNm"].ToString()))
                {
                    ltInsRepresentiveNm.Text = TextLib.StringDecoder(dtReturn.Rows[0]["PresidentNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["ChargerNm"].ToString()))
                {
                    ltInsChargerNm.Text = TextLib.StringDecoder(dtReturn.Rows[0]["ChargerNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["BizNm"].ToString()))
                {
                    itInsIndus.Text = TextLib.StringDecoder(dtReturn.Rows[0]["BizNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompTyNm"].ToString()))
                {
                    ltInsCompTy.Text = TextLib.StringDecoder(dtReturn.Rows[0]["CompTyNm"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompTelFrontNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtReturn.Rows[0]["CompTelMidNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtReturn.Rows[0]["CompTelRearNo"].ToString()))
                {
                    ltInsCompTelFrontNo.Text = dtReturn.Rows[0]["CompTelFrontNo"].ToString();
                    ltInsCompTelMidNo.Text = dtReturn.Rows[0]["CompTelMidNo"].ToString();
                    ltInsCompTelRearNo.Text = dtReturn.Rows[0]["CompTelRearNo"].ToString();
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["CompFaxFrontNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtReturn.Rows[0]["CompFaxMidNo"].ToString()) &&
                    !string.IsNullOrEmpty(dtReturn.Rows[0]["CompFaxRearNo"].ToString()))
                {
                    ltInsCompFaxFrontNo.Text = dtReturn.Rows[0]["CompFaxFrontNo"].ToString();
                    ltInsCompFaxMidNo.Text = dtReturn.Rows[0]["CompFaxMidNo"].ToString();
                    ltInsCompFaxRearNo.Text = dtReturn.Rows[0]["CompFaxRearNo"].ToString();
                }


                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Addr"].ToString()))
                {
                    ltInsAddr.Text = TextLib.StringDecoder(dtReturn.Rows[0]["Addr"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["DetAddr"].ToString()))
                {
                    ltInsDetAddr.Text = TextLib.StringDecoder(dtReturn.Rows[0]["DetAddr"].ToString());
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["FilePath1"].ToString()))
                {
                    ltInsfileAddon1.Text = "<a href='" + strDBFileUpload + dtReturn.Rows[0]["FilePath1"].ToString() + "'>" + dtReturn.Rows[0]["FileRealNm1"].ToString() + "</a>";
                    txtHfFilePath1.Text = dtReturn.Rows[0]["FilePath1"].ToString();
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["FilePath2"].ToString()))
                {
                    ltInsfileAddon2.Text = "<a href='" + strDBFileUpload + dtReturn.Rows[0]["FilePath2"].ToString() + "'>" + dtReturn.Rows[0]["FileRealNm2"].ToString() + "</a>";
                    txtHfFilePath2.Text = dtReturn.Rows[0]["FilePath2"].ToString();
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["FilePath3"].ToString()))
                {
                    ltInsfileAddon3.Text = "<a href='" + strDBFileUpload + dtReturn.Rows[0]["FilePath3"].ToString() + "'>" + dtReturn.Rows[0]["FileRealNm3"].ToString() + "</a>";
                    txtHfFilePath3.Text = dtReturn.Rows[0]["FilePath3"].ToString();
                }

                if (!string.IsNullOrEmpty(dtReturn.Rows[0]["Introduce"].ToString()))
                {
                    ltInsIntro.Text = TextLib.StringDecoder(dtReturn.Rows[0]["Introduce"].ToString());
                }
            }
        }

        /// <summary>
        /// 수정이벤트 클릭시 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_MODIFY + "?" + Master.PARAM_DATA1 + "=" + txtHfCompNo.Text, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 삭제버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                // KN_USP_STK_DELETE_COMPINFO_M00
                // KN_USP_STK_DELETE_COMPADDON_M00
                CompInfoBlo.RemoveDetailView(txtHfCompNo.Text, Session["CompCd"].ToString(), Session["MemNo"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), txtHfFilePath1.Text, txtHfFilePath2.Text, txtHfFilePath3.Text);

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DeleteItem", "javascript:fnAlert('" + AlertNm["INFO_DELETE_ISSUE"] + "','" + Master.PAGE_LIST + "')", CommValue.AUTH_VALUE_TRUE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        /// <summary>
        /// 목록버튼 클릭시 이벤트 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                Response.Redirect(Master.PAGE_LIST, CommValue.AUTH_VALUE_FALSE);
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
    }
}
