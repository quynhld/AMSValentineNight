namespace KN.Common.Method.Util
{
    /// <summary>
    /// 리포팅툴 프린팅 관련 처리 Utility
    /// </summary>
    public class ReportPrintUtil
    {
        /// <summary>
        /// ShowReport : 리포트 보여주는 메소드
        /// </summary>
        /// <param name="strMrdPageNm">mrd화일명</param>
        /// <param name="strParams">파라미터용 2차원배열</param>
        /// <param name="strWebSvrIP">웹서버IP</param>
        /// <param name="strMrdSvrIP">리포팅서버IP</param>
        public void ShowReport(string strMrdPageNm, string [,] strParams, string strWebSvrIP, string strMrdSvrIP)
        {
            int intParamCnt = strParams.Length;
            string strReportParams = "/rv WorkType[PRINT]";

            for (int intTmpI = 0; intTmpI < intParamCnt; intTmpI++)
            {
                strReportParams = strReportParams + " " + strParams[intTmpI, 0] + "[" + strParams[intTmpI, 1] + "]";
            }
        }

        public void MakeReport(string strMrdPageNm, string strWebSvrIP, string strMrdSvrIP)
        {
            string strHomeDir = string.Empty;
            string strHomePath = string.Empty;
            string strParam = string.Empty;

            strHomeDir = "http://" + strMrdSvrIP + "/Common/Controls/";
            strHomePath = "D:\\\"KSystem.net\\Common";

            strParam = strParam + strHomePath + "\\controls\\YlwIISClient.exe";
            strParam = strParam + " /i 2";
            strParam = strParam + " /u " + strMrdSvrIP + "/Common/FileDownload/FileService";
            strParam = strParam + " /m YlwIISClient.exe";
            strParam = strParam + " /r rdn";
            strParam = strParam + " /f " + strMrdPageNm;
            strParam = strParam + " /k ] ::::: ";
            strParam = strParam + " /l 1";
            strParam = strParam + " /s ";
            strParam = strParam + " /z ReportNET";
            strParam = strParam + " /d " + strHomePath + "\\ReportNET";
        }
    }
}
