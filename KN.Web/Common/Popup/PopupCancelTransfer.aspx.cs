using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;

using KN.Settlement.Biz;
using KN.Manage.Biz;
using KN.Resident.Biz;
using KN.Settlement.Biz;
using KN.Common.Method.Common;
using System.Data;
using System.Text;


namespace KN.Web.Common.Popup
{
    public partial class PopupCancelTransfer : BasePage
    {

        public string strMRDFile = string.Empty;
        public string NOW_DOMAIN = string.Empty;

        public string strRentCd = string.Empty;
        public string strInvoiceNo = string.Empty;
        public string strRoomNo = string.Empty;
        public string strUserSeq = string.Empty;
        public string strMemNo = string.Empty;
        public string strIP = string.Empty;
        public string strRefSeq = string.Empty;
        public string strListType = string.Empty;
        public string strSlipNo = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    if (!CheckParams())
                    {
                        var sbWarning = new StringBuilder();

                        sbWarning.Append("alert('");
                        sbWarning.Append(AlertNm["INFO_ACCESS_WRONG"]);
                        sbWarning.Append("');");
                        sbWarning.Append("self.close();");

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Warning", sbWarning.ToString(), CommValue.AUTH_VALUE_TRUE);
                    }
                    else
                    {
                        InitControls();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
            ltReasonType.Text = "Reason Type";
            ltContents.Text = "Content";

            MakeCancelTransferDdl();
        }

        private bool CheckParams()
        {
            var isReturnOk = CommValue.AUTH_VALUE_FALSE;

            if (Request.Params["Datum0"] != null)
            {
                if (!string.IsNullOrEmpty(Request.Params["Datum0"]))
                {
                    strRentCd = Request.Params["Datum0"];
                    hfRentCd.Value = strRentCd.ToString();
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum1"]))
                {
                    strInvoiceNo = Request.Params["Datum1"];
                    hfRefInvoiceNo.Value = strInvoiceNo.ToString();
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum2"]))
                {
                    strRoomNo = Request.Params["Datum2"];
                    hfRoomNo.Value = strRoomNo.ToString();
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum3"]))
                {
                    strUserSeq = Request.Params["Datum3"];
                    hfUserSeq.Value = strUserSeq.ToString();
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum4"]))
                {
                    strMemNo = Request.Params["Datum4"];
                    hfMemNo.Value = strMemNo.ToString();
                   
                }
                if (!string.IsNullOrEmpty(Request.Params["Datum5"]))
                {
                    strIP = Request.Params["Datum5"];
                    hfIP.Value = strIP.ToString();

                }
                if (!string.IsNullOrEmpty(Request.Params["Datum6"]))
                {
                    strRefSeq = Request.Params["Datum6"];
                    hfRefSeq.Value = strRefSeq.ToString();

                }
                if (!string.IsNullOrEmpty(Request.Params["Datum7"]))
                {
                    strListType = Request.Params["Datum7"];
                    hfListType.Value = strListType.ToString();

                }

                if (!string.IsNullOrEmpty(Request.Params["Datum8"]))
                {
                    strSlipNo = Request.Params["Datum8"];
                    hfSlipNo.Value = strSlipNo.ToString();

                }

                isReturnOk = CommValue.AUTH_VALUE_TRUE;
            }

            return isReturnOk;
        }

        private void MakeCancelTransferDdl()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MngPaymentBlo.SelectCancelCode("Cancel_Type",Session["LangCd"].ToString(), "");

            ddlReason.Items.Clear();

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlReason.Items.Add(new ListItem(dr["CodeNm"].ToString(), dr["CodeCd"].ToString()));
            }

        }

        protected void lnkbtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                // 세션체크
                AuthCheckLib.CheckSession();

                MngPaymentBlo.CancelData(hfRoomNo.Value, hfUserSeq.Value, hfMemNo.Value, hfIP.Value, txtCancellReason.Text, hfRefSeq.Value, hfRefInvoiceNo.Value, ddlReason.SelectedValue.ToString(), hfListType.Value, hfSlipNo.Value);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "CancelSuccess();", CommValue.AUTH_VALUE_TRUE);
            }
           
             catch (Exception ex)
             {
                 ErrLogger.MakeLogger(ex);
             }              
        }

        protected void lnkbtnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PreviewPrint", "rd_Close1();", CommValue.AUTH_VALUE_TRUE);
        }


    }
}