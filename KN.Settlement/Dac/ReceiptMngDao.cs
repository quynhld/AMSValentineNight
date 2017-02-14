using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Settlement.Dac
{
    public class ReceiptMngDao
    {
        #region SelectMngSalesBillingList : 아파트 통합 입주자 리스트 과금관리

        /**********************************************************************************************
         * Mehtod   명 : SelectMngSalesBillingList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-30
         * 용       도 : 아파트 통합 입주자 리스트 과금관리
         * Input    값 : SelectMngSalesBillingList(intPageSize, intNowPage, strTenantNm, strRentCd, strRoomNo, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectMngSalesBillingList : 아파트 통합 입주자 리스트 과금관리
        /// </summary>
        /// <param name="intPageSize">한페이지당라인수</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strTenantNm">계약자명</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectMngSalesBillingList(int intPageSize, int intNowPage, string strTenantNm, string strRentCd, string strRoomNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[6];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTenantNm));
            objParams[3] = strRentCd;
            objParams[4] = TextLib.MakeNullToEmpty(strRoomNo);
            objParams[5] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_BILLINGINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMngRentBillingList : 오피스 및 리테일  통합 입주자 리스트 과금관리

        /**********************************************************************************************
         * Mehtod   명 : SelectMngRentBillingList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-13
         * 용       도 : 오피스 및 리테일  통합 입주자 리스트 과금관리
         * Input    값 : SelectMngRentBillingList(intPageSize, intNowPage, strTenantNm, strRentCd, strRoomNo, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectMngRentBillingList : 오피스 및 리테일  통합 입주자 리스트 과금관리
        /// </summary>
        /// <param name="intPageSize">한페이지당라인수</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strTenantNm">계약자명</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectMngRentBillingList(int intPageSize, int intNowPage, string strTenantNm, string strRentCd, string strRoomNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[6];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTenantNm));
            objParams[3] = strRentCd;
            objParams[4] = TextLib.MakeNullToEmpty(strRoomNo);
            objParams[5] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_BILLINGINFO_S01", objParams);

            return dsReturn;
        }

        public static DataSet GetUtilBillingList(string strRentCd, string strRoomNo, string chargeTy, string billType, string dateS, string strIsPrinted)
        {
            var objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = chargeTy;
            objParams[3] = dateS;
            objParams[4] = strIsPrinted;
            var dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_BILLINGINFO_S02", objParams);

            return dsReturn;
        }

        public static DataTable UpdateUtilBillingList(string strRentCd, string strRoomNo, string chargeTy, string billType, string dateS)
        {
            var objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = chargeTy;
            objParams[3] = dateS;
            var dsReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_BILLINGINFO_M00", objParams);

            return dsReturn;
        }


        public static DataTable UpdateUtilRequestDt(string strRentCd, string chargeTy, string dateR, string dateS, string isPrint)
        {
            var objParams = new object[5];
            objParams[0] = strRentCd;
            objParams[1] = chargeTy;
            objParams[2] = dateS;
            objParams[3] = dateR;
            objParams[4] = isPrint;
            var dsReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_BILLINGINFO_M01", objParams);
            return dsReturn;
        }
        #endregion

        #region SelectPrintListForEntireIssuing : 모아찍기를 위한 프린트 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectPrintListForEntireIssuing
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-13
         * 용       도 : 모아찍기를 위한 프린트 리스트 조회
         * Input    값 : SelectPrintListForEntireIssuing(섹션코드, 호, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectPrintListForEntireIssuing : 모아찍기를 위한 프린트 리스트 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드/param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMonth">조회월</param>
        /// <param name="strStartDt">조회시작일</param>
        /// <param name="strEndDt">조회종료일</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="strUserTaxCd">세금코드</param>
        /// <param name="strRssNo">사업자번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectPrintListForEntireIssuing(string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                                string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[10];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strYear;
            objParams[3] = strMonth;
            objParams[4] = strStartDt;
            objParams[5] = strEndDt;
            objParams[6] = strItemCd;
            objParams[7] = strUserTaxCd;
            objParams[8] = strRssNo;
            objParams[9] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_PRINTINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectPrintListForHoadon : 화돈 프린트 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectPrintListForHoadon
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-17
         * 용       도 : 화돈 프린트 리스트 조회
         * Input    값 : SelectPrintListForHoadon(섹션코드, 호, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/

        /// <summary>
        /// SelectPrintListForHoadon : 화돈 프린트 리스트 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strCompanyNm"> </param>
        /// <param name="strMonth">조회월</param>
        /// <param name="strStartDt">조회시작일</param>
        /// <param name="strEndDt">조회종료일</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="strUserTaxCd">세금코드</param>
        /// <param name="strRssNo">사업자번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectPrintListForHoadon(string strRentCd, string strRoomNo, string strCompanyNm, string strMonth, string strStartDt, string strEndDt,
                                                         string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {

            var objParams = new object[10];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strCompanyNm;
            objParams[3] = strMonth;
            objParams[4] = strStartDt;
            objParams[5] = strEndDt;
            objParams[6] = strItemCd;
            objParams[7] = strUserTaxCd;
            objParams[8] = strRssNo;
            objParams[9] = strLangCd;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_HOADONINFO_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectHoadonPrintOut(string strRentCd, string strRoomNo, string strStartDt, string strEndDt,
                                                        string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[8];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;           
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;
            objParams[4] = strItemCd;
            objParams[5] = strUserTaxCd;
            objParams[6] = strRssNo;
            objParams[7] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_HOADONPRINTOUT_S00", objParams);

            return dtReturn;
        }

        public static DataTable SpreadPrintExcelForHoadon(string strRentCd, string strRoomNo, string strCompanyNm, string strMonth, string strStartDt, string strEndDt,
                                                         string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {

            var objParams = new object[10];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strCompanyNm;
            objParams[3] = strMonth;
            objParams[4] = strStartDt;
            objParams[5] = strEndDt;
            objParams[6] = strItemCd;
            objParams[7] = strUserTaxCd;
            objParams[8] = strRssNo;
            objParams[9] = strLangCd;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_HOADONINFO_S03", objParams);

            return dtReturn;
        }

        public static DataTable SpreadPrintExcelForHoadonPrintOut(string strRentCd, string strRoomNo, string strStartDt, string strEndDt,
                                                       string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[8];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;           
            objParams[2] = strStartDt;
            objParams[3] = strEndDt;
            objParams[4] = strItemCd;
            objParams[5] = strUserTaxCd;
            objParams[6] = strRssNo;
            objParams[7] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_HOADONPRINTOUT_S03", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectPrintListForAPTHoadon : 아파트 화돈 프린트 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectPrintListForAPTHoadon
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-26
         * 용       도 : 화돈 프린트 리스트 조회
         * Input    값 : SelectPrintListForAPTHoadon(회사코드, 섹션코드, 호, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectPrintListForAPTHoadon : 화돈 프린트 리스트 조회
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMonth">조회월</param>
        /// <param name="strStartDt">조회시작일</param>
        /// <param name="strEndDt">조회종료일</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="strUserTaxCd">세금코드</param>
        /// <param name="strRssNo">사업자번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectPrintListForAPTHoadon(string strCompNo, string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                            string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd, int intStartFloor, int intEndFloor)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[13];

            objParams[0] = strCompNo;
            objParams[1] = strRentCd;
            objParams[2] = strRoomNo;
            objParams[3] = strYear;
            objParams[4] = strMonth;
            objParams[5] = strStartDt.Replace("-", "");
            objParams[6] = strEndDt.Replace("-","");
            objParams[7] = strItemCd;
            objParams[8] = strUserTaxCd;
            objParams[9] = strRssNo;
            objParams[10] = strLangCd;
            objParams[11] = intStartFloor;
            objParams[12] = intEndFloor;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_HOADONINFO_S01", objParams);

            return dtReturn;
        }


        //BaoTv
        public static DataTable SelectPrintListAptHoadon(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string strTenantNm, string strLangCd)
        {
            var objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strItemCd;
            objParams[4] = strTenantNm;
            objParams[5] = strLangCd;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADONINFO_S01", objParams);

            return dtReturn;
        }

        public static DataTable SelectPrintListAptAdjHoadon(string strRentCd, string strRoomNo, string strPayDt,
                                                           string strItemCd, string strTenantNm, string strLangCd)
        {
            var objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strItemCd;
            objParams[4] = strTenantNm;
            objParams[5] = strLangCd;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SELECT_APT_ADJ_HOADONINFO_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectPaymentAptForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strEPayDt;
            objParams[4] = strItemCd;
            objParams[5] = strTenantNm;
            objParams[6] = strIsSent;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADONINFO_S03", objParams);

            return dtReturn;
        }

        public static DataTable SelectPaymentTowerForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strEPayDt;
            objParams[4] = strItemCd;
            objParams[5] = strTenantNm;
            objParams[6] = strIsSent;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADONINFO_S06", objParams);

            return dtReturn;
        }

        public static DataTable SelectInvoiceAptForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strEPayDt;
            objParams[4] = strItemCd;
            objParams[5] = strTenantNm;
            objParams[6] = strIsSent;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADONINFO_S04", objParams);

            return dtReturn;
        }

        #region SelectExcelInvoiceAptForTransfer

        /**********************************************************************************************
         * Mehtod   명 : SelectExcelInvoiceAptForTransfer
         * 개   발  자 : 양영석
         * 생   성  일 : 2013-07-15
         * 용       도 : 정산정보조회
         * Input    값 : SelectExcelInvoiceAptForTransfer(strRentCd, strRoomNo, strPayDt, strEPayDt , strItemCd,strTenantNm,strIsSent)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectExcelInvoiceAptForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strEPayDt;
            objParams[4] = strItemCd;
            objParams[5] = strTenantNm;
            objParams[6] = strIsSent;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_EXCEL_SELECT_APT_HOADONINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        public static DataTable SelectInvoiceTowerForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            var objParams = new object[7];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strEPayDt;
            objParams[4] = strItemCd;
            objParams[5] = strTenantNm;
            objParams[6] = strIsSent;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADONINFO_S05", objParams);

            return dtReturn;
        }

        //BaoTv
        public static DataTable SelectRepintListAptHoadon(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string strInvoiceNo, string strLangCd)
        {
            var objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strItemCd;
            objParams[4] = strInvoiceNo;
            objParams[5] = strLangCd;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADONINFO_S02", objParams);

            return dtReturn;
        }

        //PhuongTV
        public static DataTable SelectListAptHoadonForCancel(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string strInvoiceNo, string strLangCd)
        {
            var objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strItemCd;
            objParams[4] = strInvoiceNo;
            objParams[5] = strLangCd;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADONCANCEL_S01", objParams);

            return dtReturn;
        }

        //BaoTv
        public static DataTable SelectCancelListAptHoadon(string strInvoiceNo, string strSerialNo)
        {
            var objParams = new object[2];

            objParams[0] = strInvoiceNo;
            objParams[1] = strSerialNo;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADONCANCEL_S001", objParams);

            return dtReturn;
        }

        //PhuongTV
        public static DataTable SelectCancelListKNHoadon(string strInvoiceNo)
        {
            var objParams = new object[1];

            objParams[0] = strInvoiceNo;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_KN_HOADONCANCEL_S00", objParams);

            return dtReturn;
        }

        #endregion

        public static DataTable SelectAptHoadonForReplace(string strInvoiceNo)
        {
            var objParams = new object[1];


            objParams[0] = strInvoiceNo;            


            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADON_OLD_S00", objParams);

            return dtReturn;
        }

        public static DataTable SelectAptHoadonForReplaceDetail(string strInvoiceNo, string strRefSeq)
        {
            var objParams = new object[2];


            objParams[0] = strInvoiceNo;
            objParams[1] = strRefSeq;


            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_HOADON_OLD_S01", objParams);

            return dtReturn;
        }


        //SELECT For Apt Parking Fee Print Out Invoice
        public static DataTable SelectPrintListHoadonAptPKF(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string printYN, string strLangCd)
        {
            var objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strItemCd;
            objParams[4] = printYN;
            objParams[5] = strLangCd;            

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_PKFEE_S00", objParams);

            return dtReturn;
        }

        #region Select Issuing Amount For Printout Invoice Merge

        public static DataSet SelectIssAmtPrintoutHoadonMerge(string IssDt)
        {
            DataSet dsReturn = new DataSet();

            var objParams = new object[1];

            objParams[0] = IssDt;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_ISS_TOTAL_S00", objParams);

            return dsReturn;
        }

        #endregion

        //SELECT For Apt Parking Fee Print Out Special Invoice
        public static DataTable SelectSpecialHoadonAptPKF(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string printYN, string strLangCd, string strCompNm, string strInvoiceNo)
        {
            var objParams = new object[8];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strPayDt;
            objParams[3] = strItemCd;
            objParams[4] = printYN;
            objParams[5] = strLangCd;
            objParams[6] = strCompNm;
            objParams[7] = strInvoiceNo;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_APT_PKFEE_S01", objParams);

            return dtReturn;
        }

        #region SelectHoadonForEntireIssuing : 모아찍기를 위한 화돈 대상 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectHoadonForEntireIssuing
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 모아찍기를 위한 화돈 대상 리스트 조회
         * Input    값 : SelectHoadonForEntireIssuing(호, 섹션코드, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectHoadonForEntireIssuing : 모아찍기를 위한 화돈 대상 리스트 조회
        /// </summary>
        /// <param name="strRoomNo">호</param>
        /// <param name="strRentCd">섹션코드/param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMonth">조회월</param>
        /// <param name="strStartDt">조회시작일</param>
        /// <param name="strEndDt">조회종료일</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="strUserTaxCd">세금코드</param>
        /// <param name="strRssNo">사업자번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectHoadonForEntireIssuing(string strRoomNo, string strRentCd, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                             string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[10];

            objParams[0] = strRoomNo;
            objParams[1] = strRentCd;
            objParams[2] = strYear;
            objParams[3] = strMonth;
            objParams[4] = strStartDt;
            objParams[5] = strEndDt;
            objParams[6] = strItemCd;
            objParams[7] = strUserTaxCd;
            objParams[8] = strRssNo;
            objParams[9] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_LEDGERINFO_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertPrintReciptList : 영수증 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertPrintReciptList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-16
         * 용       도 : 영수증 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 영수증 등록
        /// </summary>
        /// <param name="strPrintSeq">출력번호</param>
        /// <param name="strBillCd">Bill코드</param>
        /// <param name="strDocCd">문서코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intFloorNo">층</param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strMngYear">해당년</param>
        /// <param name="strMngMonth">해당월</param>
        /// <param name="strPaymentCd">결제코드</param>
        /// <param name="strUserSeq">사용자번호</param>
        /// <param name="strCashier">수납자</param>
        /// <param name="strDescription">내용</param>
        /// <param name="dblAmtViNo">수납액(동)</param>
        /// <param name="dblDongToDollar">환율</param>
        /// <param name="strCompNo">기업번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsIP">접속IP</param>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">지불일</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="intPaymentDetSeq">지불상세순번</param>
        /// <returns></returns>
        public static DataTable InsertPrintReciptList(string strPrintSeq, string strBillCd, string strDocCd, string strRentCd, int intFloorNo,
                                                      string strRoomNo, string strMngYear, string strMngMonth, string strPaymentCd, string strUserSeq,
                                                      string strCashier, string strDescription, double dblAmtViNo, double dblDongToDollar, 
                                                      string strCompNo, string strInsMemNo, string strInsIP, string strDebitCreditCd, string strPaymentDt,
                                                      int intPaymentSeq, int intPaymentDetSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[21];

            objParam[0] = TextLib.MakeNullToEmpty(strPrintSeq);
            objParam[1] = strBillCd;
            objParam[2] = strDocCd;
            objParam[3] = strRentCd;
            objParam[4] = intFloorNo;
            objParam[5] = strRoomNo;
            objParam[6] = strMngYear;
            objParam[7] = strMngMonth;
            objParam[8] = strPaymentCd;
            objParam[9] = strUserSeq;
            objParam[10] = strCashier;
            objParam[11] = strDescription;
            objParam[12] = dblAmtViNo;
            objParam[13] = dblDongToDollar;
            objParam[14] = strCompNo;
            objParam[15] = strInsMemNo;
            objParam[16] = strInsIP;
            objParam[17] = strDebitCreditCd;
            objParam[18] = strPaymentDt;
            objParam[19] = intPaymentSeq;
            objParam[20] = intPaymentDetSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_PRINTINFO_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertPrintReciptParkingCardMinusList : 주차카드용 마이너스 영수증 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertPrintReciptParkingCardMinusList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 주차카드용 마이너스 영수증 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 주차카드용 마이너스 영수증 등록
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">환불일</param>
        /// <param name="intPaymentSeq">환불상세순번</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsIP">입력IP/param>
        /// <returns></returns>
        public static DataTable InsertPrintReciptParkingCardMinusList(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[6];

            objParam[0] = strDebitCreditCd;
            objParam[1] = strPaymentDt;
            objParam[2] = intPaymentSeq;
            objParam[3] = strCompNo;
            objParam[4] = strInsMemNo;
            objParam[5] = strInsIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_PRINTINFO_S02", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertHoaDonParkingAPTReturn

        public static DataTable InsertHoaDonParkingAPTReturn(string strRentCd, string strRoomNo, string strParkingCardNo, string strPaymentDt, string strPaymentSeq, string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[8];

            objParam[0] = strRentCd;
            objParam[1] = strRoomNo;
            objParam[2] = strParkingCardNo;
            objParam[3] = strPaymentDt;
            objParam[4] = strPaymentSeq;
            objParam[5] = strCompNo;
            objParam[6] = strInsMemNo;
            objParam[7] = strInsIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_PRK_INSERT_HOADONPARKING_APT_RETURN_I00", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertPrintReciptRentalMngMinusList : 임대료 및 관리비용 마이너스 영수증 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertPrintReciptRentalMngMinusList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-04
         * 용       도 : 임대료 및 관리비용 마이너스 영수증 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 임대료 및 관리비용 마이너스 영수증 등록
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">환불일</param>
        /// <param name="intPaymentSeq">환불상세순번</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsIP">입력IP/param>
        /// <returns></returns>
        public static DataTable InsertPrintReciptRentalMngMinusList(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strItemCd, string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[7];

            objParam[0] = strDebitCreditCd;
            objParam[1] = strPaymentDt;
            objParam[2] = intPaymentSeq;
            objParam[3] = strItemCd;
            objParam[4] = strCompNo;
            objParam[5] = strInsMemNo;
            objParam[6] = strInsIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_PRINTINFO_S03", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertPrintAddonList : 영수증 발행자 및 발급자 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertPrintAddonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 영수증 발행자 및 발급자 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertPrintAddonList : 영수증 발행자 및 발급자 등록
        /// </summary>
        /// <param name="strPrintSeq">프린트순번</param>
        /// <param name="intPrintDetSeq">프린트상세순번</param>
        /// <param name="strCompNo">소속회사코드</param>
        /// <param name="strInsMemNo">담당자사번</param>
        /// <param name="strInsIP">담당자IP</param>
        /// <returns></returns>
        public static DataTable InsertPrintAddonList(string strPrintSeq, int intPrintDetSeq, string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[5];

            objParam[0] = TextLib.MakeNullToEmpty(strPrintSeq);
            objParam[1] = intPrintDetSeq;
            objParam[2] = strCompNo;
            objParam[3] = strInsMemNo;
            objParam[4] = strInsIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_PRINTINFO_S01", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertMoneyInfo : 금전 로그입력

        /**********************************************************************************************
         * Mehtod   명 : InsertMoneyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-23
         * 용       도 : 금전 로그입력
         * Input    값 : InsertMoneyInfo(대차코드, 지불일, 지불순번, 지불상세순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 금전 로그입력
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">지불일</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="intPaymentDetSeq">지불상세순번</param>
        /// <returns></returns>
        public static DataTable InsertMoneyInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strDebitCreditCd;
            objParams[1] = strPaymentDt;
            objParams[2] = intPaymentSeq;
            objParams[3] = intPaymentDetSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_MONEYINFO_M00", objParams);

            return dtReturn;
        }

        #endregion 

        #region InsertMoneyMinusInfo : 금전 로그 차감입력

        /**********************************************************************************************
         * Mehtod   명 : InsertMoneyMinusInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 금전 로그 차감입력
         * Input    값 : InsertMoneyMinusInfo(대차코드, 지불일, 지불순번, 프린트순번, 회사코드, 사원코드, 접속IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertMoneyMinusInfo : 금전 로그 차감입력
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">지불일</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="strPrintSeq">프린트순번</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원코드</param>
        /// <param name="strInsMemIP">접속IP</param>
        /// <returns></returns>
        public static DataTable InsertMoneyMinusInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strPrintSeq, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[7];

            objParams[0] = strDebitCreditCd;
            objParams[1] = strPaymentDt;
            objParams[2] = intPaymentSeq;
            objParams[3] = strPrintSeq;
            objParams[4] = strInsCompNo;
            objParams[5] = strInsMemNo;
            objParams[6] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_MONEYINFO_M01", objParams);

            return dtReturn;
        }

        #endregion 

        #region InsertTempPrintOutList : 통합 영수증 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempPrintOutList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-17
         * 용       도 : 통합 영수증 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempPrintOutList : 통합 영수증 등록
        /// </summary>
        /// <param name="strPrintDt">출력일자</param>
        /// <param name="intPrintDtSeq">출력일자순번</param>
        /// <param name="strPrintSeq">출력순번</param>
        /// <param name="strPrintDetSeq">출력상세순번</param>
        /// <returns></returns>
        public static DataTable InsertTempPrintOutList(string strPrintDt, int intPrintDtSeq, string strPrintSeq, int strPrintDetSeq)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(strPrintDt);
            objParam[1] = intPrintDtSeq;
            objParam[2] = strPrintSeq;
            objParam[3] = strPrintDetSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_PRINTINFO_S04", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertCNAPTTempHoadonList : 체스넛 아파트 임시 세금계산서 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertCNAPTTempHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 체스넛 아파트 임시 세금계산서 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertCNAPTTempHoadonList : 체스넛 아파트 임시 세금계산서 등록
        /// </summary>
        /// <param name="strUserSeq"></param>
        /// <param name="strDebitCreditCd"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strPrintOutDt"></param>
        /// <param name="intPrintOutSeq"></param>
        /// <param name="strBillCd"></param>
        /// <param name="strBillNo"></param>
        /// <param name="strLangCd"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static DataTable InsertCNAPTTempHoadonList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                          string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd, 
                                                          string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[13];

            objParam[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParam[1] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParam[2] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParam[3] = intPaymentSeq;
            objParam[4] = intPaymentDetSeq;
            objParam[5] = TextLib.MakeNullToEmpty(strPrintOutDt);
            objParam[6] = intPrintOutSeq;
            objParam[7] = TextLib.MakeNullToEmpty(strBillCd);
            objParam[8] = TextLib.MakeNullToEmpty(strBillNo);
            objParam[9] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[10] = TextLib.MakeNullToEmpty(strInsCompCd);
            objParam[11] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParam[12] = TextLib.MakeNullToEmpty(strInsMemIP);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_TEMPHOADONINFO_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertKNAPTTempHoadonList : 경남비나 아파트상가 임시 세금계산서 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertKNAPTTempHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 경남비나 아파트상가 임시 세금계산서 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertKNAPTTempHoadonList : 경남비나 아파트상가 임시 세금계산서 등록
        /// </summary>
        /// <param name="strUserSeq"></param>
        /// <param name="strDebitCreditCd"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strPrintOutDt"></param>
        /// <param name="intPrintOutSeq"></param>
        /// <param name="strBillCd"></param>
        /// <param name="strBillNo"></param>
        /// <param name="strLangCd"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static DataTable InsertKNAPTTempHoadonList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                          string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                          string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[13];

            objParam[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParam[1] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParam[2] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParam[3] = intPaymentSeq;
            objParam[4] = intPaymentDetSeq;
            objParam[5] = TextLib.MakeNullToEmpty(strPrintOutDt);
            objParam[6] = intPrintOutSeq;
            objParam[7] = TextLib.MakeNullToEmpty(strBillCd);
            objParam[8] = TextLib.MakeNullToEmpty(strBillNo);
            objParam[9] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[10] = TextLib.MakeNullToEmpty(strInsCompCd);
            objParam[11] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParam[12] = TextLib.MakeNullToEmpty(strInsMemIP);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_TEMPHOADONINFO_S01", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertKNTempHoadonList : 경남비나 오피스 및 리테일 임시 세금계산서 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertKNTempHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 경남비나 오피스 및 리테일 임시 세금계산서 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertKNTempHoadonList : 경남비나 오피스 및 리테일 임시 세금계산서 등록
        /// </summary>
        /// <param name="strUserSeq"></param>
        /// <param name="strDebitCreditCd"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strPrintOutDt"></param>
        /// <param name="intPrintOutSeq"></param>
        /// <param name="strBillCd"></param>
        /// <param name="strBillNo"></param>
        /// <param name="strLangCd"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static DataTable InsertKNTempHoadonList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                       string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                       string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[13];

            objParam[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParam[1] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParam[2] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParam[3] = intPaymentSeq;
            objParam[4] = intPaymentDetSeq;
            objParam[5] = TextLib.MakeNullToEmpty(strPrintOutDt);
            objParam[6] = intPrintOutSeq;
            objParam[7] = TextLib.MakeNullToEmpty(strBillCd);
            objParam[8] = TextLib.MakeNullToEmpty(strBillNo);
            objParam[9] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[10] = TextLib.MakeNullToEmpty(strInsCompCd);
            objParam[11] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParam[12] = TextLib.MakeNullToEmpty(strInsMemIP);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_TEMPHOADONINFO_S02", objParam);

            return dtReturn;
        }

        #endregion

        #region InsertCNAPTTempHoadonTotalList : 체스넛 아파트 임시 세금계산서 합산 테이블 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertCNAPTTempHoadonTotalList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 체스넛 아파트 임시 세금계산서 합산 테이블 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertCNAPTTempHoadonTotalList : 체스넛 아파트 임시 세금계산서 합산 테이블 등록
        /// </summary>
        /// <param name="strUserSeq"></param>
        /// <param name="strDebitCreditCd"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strPrintOutDt"></param>
        /// <param name="intPrintOutSeq"></param>
        /// <param name="strBillCd"></param>
        /// <param name="strBillNo"></param>
        /// <param name="strLangCd"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static object[] InsertCNAPTTempHoadonTotalList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                              string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                              string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParam = new object[13];

            objParam[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParam[1] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParam[2] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParam[3] = intPaymentSeq;
            objParam[4] = intPaymentDetSeq;
            objParam[5] = TextLib.MakeNullToEmpty(strPrintOutDt);
            objParam[6] = intPrintOutSeq;
            objParam[7] = TextLib.MakeNullToEmpty(strBillCd);
            objParam[8] = TextLib.MakeNullToEmpty(strBillNo);
            objParam[9] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[10] = TextLib.MakeNullToEmpty(strInsCompCd);
            objParam[11] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParam[12] = TextLib.MakeNullToEmpty(strInsMemIP);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_TEMPHOADONINFO_M00", objParam);

            return objReturn;
        }

        #endregion

        #region InsertKNAPTTempHoadonTotalList : 경남비나 아파트상가 임시 세금계산서 합산 테이블 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertKNAPTTempHoadonTotalList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 경남비나 아파트상가 임시 세금계산서 합산 테이블 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertKNAPTTempHoadonTotalList : 경남비나 아파트상가 임시 세금계산서 합산 테이블 등록
        /// </summary>
        /// <param name="strUserSeq"></param>
        /// <param name="strDebitCreditCd"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strPrintOutDt"></param>
        /// <param name="intPrintOutSeq"></param>
        /// <param name="strBillCd"></param>
        /// <param name="strBillNo"></param>
        /// <param name="strLangCd"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static object[] InsertKNAPTTempHoadonTotalList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                               string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                               string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParam = new object[13];

            objParam[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParam[1] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParam[2] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParam[3] = intPaymentSeq;
            objParam[4] = intPaymentDetSeq;
            objParam[5] = TextLib.MakeNullToEmpty(strPrintOutDt);
            objParam[6] = intPrintOutSeq;
            objParam[7] = TextLib.MakeNullToEmpty(strBillCd);
            objParam[8] = TextLib.MakeNullToEmpty(strBillNo);
            objParam[9] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[10] = TextLib.MakeNullToEmpty(strInsCompCd);
            objParam[11] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParam[12] = TextLib.MakeNullToEmpty(strInsMemIP);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_TEMPHOADONINFO_M01", objParam);

            return objReturn;
        }

        #endregion

        #region InsertKNTempHoadonTotalList : 경남비나 오피스 및 리테일 임시 세금계산서 합산 테이블 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertKNTempHoadonTotalList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 경남비나 오피스 및 리테일 임시 세금계산서 합산 테이블 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertKNTempHoadonTotalList : 경남비나 오피스 및 리테일 임시 세금계산서 합산 테이블 등록
        /// </summary>
        /// <param name="strUserSeq"></param>
        /// <param name="strDebitCreditCd"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strPrintOutDt"></param>
        /// <param name="intPrintOutSeq"></param>
        /// <param name="strBillCd"></param>
        /// <param name="strBillNo"></param>
        /// <param name="strLangCd"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static object[] InsertKNTempHoadonTotalList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                            string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                            string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParam = new object[13];

            objParam[0] = TextLib.MakeNullToEmpty(strUserSeq);
            objParam[1] = TextLib.MakeNullToEmpty(strDebitCreditCd);
            objParam[2] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParam[3] = intPaymentSeq;
            objParam[4] = intPaymentDetSeq;
            objParam[5] = TextLib.MakeNullToEmpty(strPrintOutDt);
            objParam[6] = intPrintOutSeq;
            objParam[7] = TextLib.MakeNullToEmpty(strBillCd);
            objParam[8] = TextLib.MakeNullToEmpty(strBillNo);
            objParam[9] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[10] = TextLib.MakeNullToEmpty(strInsCompCd);
            objParam[11] = TextLib.MakeNullToEmpty(strInsMemNo);
            objParam[12] = TextLib.MakeNullToEmpty(strInsMemIP);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_TEMPHOADONINFO_M02", objParam);

            return objReturn;
        }

        #endregion

        #region InsertTempHoadonTotalInfo : 모아찍을 내용을 세금계산서 합산 테이블 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonTotalInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 모아찍을 내용을 세금계산서 합산 테이블 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonTotalInfo : 모아찍을 내용을 세금계산서 합산 테이블 등록
        /// </summary>
        /// <param name="strPrintOutDt"></param>
        /// <param name="intPrintOutSeq"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static DataTable InsertTempHoadonTotalInfo(string strPrintOutDt, int intPrintOutSeq, string strTitle)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[3];

            objParam[0] = TextLib.MakeNullToEmpty(strPrintOutDt);
            objParam[1] = intPrintOutSeq;
            objParam[2] = TextLib.MakeNullToEmpty(strTitle);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_TEMPHOADONTOTALINFO_M00", objParam);

            return dtReturn;
        }

        #endregion

        #region DeleteTempPrintList : 영수증 출력 내역 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempPrintList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-17
         * 용       도 : 영수증 출력 내역 삭제
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempPrintList : 영수증 출력 내역 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteTempPrintList()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_DELETE_PRINTINFO_M00");

            return objReturn;
        }

        #endregion
    }
}