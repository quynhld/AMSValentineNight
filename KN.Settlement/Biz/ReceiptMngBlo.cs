using System.Data;

using KN.Settlement.Dac;

namespace KN.Settlement.Biz
{
    public class ReceiptMngBlo
    {
        #region SpreadMngSalesBillingList : 아파트 통합 입주자 리스트 과금관리

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngSalesBillingList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-30
         * 용       도 : 아파트 통합 입주자 리스트 과금관리
         * Input    값 : SpreadMngBillingList(intPageSize, intNowPage, strTenantNm, strRentCd, strRoomNo, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadMngSalesBillingList : 아파트 통합 입주자 리스트 과금관리
        /// </summary>
        /// <param name="intPageSize">한페이지당라인수</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strTenantNm">계약자명</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadMngSalesBillingList(int intPageSize, int intNowPage, string strTenantNm, string strRentCd, string strRoomNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ReceiptMngDao.SelectMngSalesBillingList(intPageSize, intNowPage, strTenantNm, strRentCd, strRoomNo, strLangCd);

            return dsReturn;
        }

        #endregion

        #region SpreadMngRentBillingList : 오피스 및 리테일  통합 입주자 리스트 과금관리

        /**********************************************************************************************
         * Mehtod   명 : SpreadMngRentBillingList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-13
         * 용       도 : 오피스 및 리테일  통합 입주자 리스트 과금관리
         * Input    값 : SpreadMngRentBillingList(intPageSize, intNowPage, strTenantNm, strRentCd, strRoomNo, strLangCd)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadMngRentBillingList : 오피스 및 리테일  통합 입주자 리스트 과금관리
        /// </summary>
        /// <param name="intPageSize">한페이지당라인수</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strTenantNm">계약자명</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadMngRentBillingList(int intPageSize, int intNowPage, string strTenantNm, string strRentCd, string strRoomNo, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ReceiptMngDao.SelectMngRentBillingList(intPageSize, intNowPage, strTenantNm, strRentCd, strRoomNo, strLangCd);

            return dsReturn;
        }
        //Baotv 
        public static DataSet GetUtilBillingList(string strRentCd, string strRoomNo, string chargeTy,string billType,string dateS,string strIsPrinted)
        {
            var dsReturn = ReceiptMngDao.GetUtilBillingList(strRentCd, strRoomNo, chargeTy, billType, dateS,strIsPrinted);
            return dsReturn;
        }
        public static DataTable UpdateUtilBillingList(string strRentCd, string strRoomNo, string chargeTy, string billType, string dateS)
        {
            var dsReturn = ReceiptMngDao.UpdateUtilBillingList(strRentCd, strRoomNo, chargeTy, billType, dateS);
            return dsReturn;
        }

        public static DataTable UpdateUtilRequestDt(string strRentCd, string chargeTy, string dateR, string dateS, string isPrint)
        {
            var dsReturn = ReceiptMngDao.UpdateUtilRequestDt(strRentCd, chargeTy, dateR, dateS, isPrint);
            return dsReturn;
        }

        #endregion

        #region SpreadPrintListForEntireIssuing : 모아찍기를 위한 프린트 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadPrintListForEntireIssuing
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-13
         * 용       도 : 모아찍기를 위한 프린트 리스트 조회
         * Input    값 : SpreadPrintListForEntireIssuing(섹션코드, 호, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadPrintListForEntireIssuing : 모아찍기를 위한 프린트 리스트 조회
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
        public static DataTable SpreadPrintListForEntireIssuing(string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                                string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.SelectPrintListForEntireIssuing(strRentCd, strRoomNo, strYear, strMonth, strStartDt, strEndDt, strItemCd, strUserTaxCd, strRssNo, 
                                                                     strLangCd);

            return dtReturn;
        }

        #endregion

        #region SpreadPrintListForHoadon : 화돈 프린트 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadPrintListForHoadon
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-05-17
         * 용       도 : 화돈 프린트 리스트 조회
         * Input    값 : SpreadPrintListForHoadon(섹션코드, 호, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/

        /// <summary>
        /// SpreadPrintListForHoadon : 화돈 프린트 리스트 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드/param>
        /// <param name="strRoomNo">호</param>
        /// <param name="strMonth">조회월</param>
        /// <param name="strStartDt">조회시작일</param>
        /// <param name="strEndDt">조회종료일</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="strUserTaxCd">세금코드</param>
        /// <param name="strRssNo">사업자번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRoomNo"> </param>
        /// <param name="strMonth"> </param>
        /// <param name="strStartDt"> </param>
        /// <param name="strEndDt"> </param>
        /// <param name="strCompanyNm"> </param>
        /// <param name="strRoomNo"> </param>
        /// <param name="strEndDt"> </param>
        /// <param name="strItemCd"> </param>
        /// <param name="strUserTaxCd"> </param>
        /// <param name="strRssNo"> </param>
        /// <param name="strLangCd"> </param>
        /// <param name="strCompanyNm"> </param>
        /// <param name="strMonth"> </param>
        /// <param name="strStartDt"> </param>
        public static DataTable SpreadPrintListForHoadon(string strRentCd, string strRoomNo, string strCompanyNm, string strMonth, string strStartDt, string strEndDt,
                                                         string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            var dtReturn = ReceiptMngDao.SelectPrintListForHoadon(strRentCd, strRoomNo, strCompanyNm, strMonth, strStartDt, strEndDt, strItemCd, strUserTaxCd, strRssNo, strLangCd);
            return dtReturn;
        }

        public static DataTable SelectHoadonPrintOut(string strRentCd, string strRoomNo, string strStartDt, string strEndDt,
                                                       string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
           var dtReturn = ReceiptMngDao.SelectHoadonPrintOut(strRentCd, strRoomNo, strStartDt, strEndDt, strItemCd, strUserTaxCd, strRssNo, strLangCd);
            return dtReturn;
        }

        public static DataTable SpreadPrintExcelForHoadon(string strRentCd, string strRoomNo, string strCompanyNm, string strMonth, string strStartDt, string strEndDt,
                                                         string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            var dtReturn = ReceiptMngDao.SpreadPrintExcelForHoadon(strRentCd, strRoomNo, strCompanyNm, strMonth, strStartDt, strEndDt, strItemCd, strUserTaxCd, strRssNo, strLangCd);
            return dtReturn;
        }

        public static DataTable SpreadPrintExcelForHoadonPrintOut(string strRentCd, string strRoomNo, string strStartDt, string strEndDt,
                                                       string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
           var dtReturn = ReceiptMngDao.SpreadPrintExcelForHoadonPrintOut(strRentCd, strRoomNo, strStartDt, strEndDt, strItemCd, strUserTaxCd, strRssNo, strLangCd);
            return dtReturn;
        }

        #endregion

        #region SelectPrintListForAPTHoadon : 화돈 프린트 리스트 조회

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
        /// <returns>DataTable</returns>
        public static DataTable SelectPrintListForAPTHoadon(string strCompNo, string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                            string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd, int intStartFloor, int intEndFloor)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.SelectPrintListForAPTHoadon(strCompNo, strRentCd, strRoomNo, strYear, strMonth, strStartDt, strEndDt, strItemCd, strUserTaxCd,
                                                                 strRssNo, strLangCd, intStartFloor, intEndFloor);

            return dtReturn;
        }

        //BaoTV
        public static DataTable SelectPrintListAptHoadon(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string strTenantNm, string strLangCd)
        {
            var dtReturn = ReceiptMngDao.SelectPrintListAptHoadon(strRentCd, strRoomNo, strPayDt, strItemCd, strTenantNm, strLangCd);
            return dtReturn;
        }

        public static DataTable SelectPrintListAptAdjHoadon(string strRentCd, string strRoomNo, string strPayDt,
                                                           string strItemCd, string strTenantNm, string strLangCd)
        {
            var dtReturn = ReceiptMngDao.SelectPrintListAptAdjHoadon(strRentCd, strRoomNo, strPayDt, strItemCd, strTenantNm, strLangCd);
            return dtReturn;
        }

        //BaoTV
        public static DataTable SelectPaymentAptForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            var dtReturn = ReceiptMngDao.SelectPaymentAptForTransfer(strRentCd, strRoomNo, strPayDt, strEPayDt, strItemCd, strTenantNm, strIsSent);
            return dtReturn;
        }

        //BaoTV
        public static DataTable SelectPaymentTowerForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            var dtReturn = ReceiptMngDao.SelectPaymentTowerForTransfer(strRentCd, strRoomNo, strPayDt, strEPayDt, strItemCd, strTenantNm, strIsSent);
            return dtReturn;
        }


        //BaoTV
        public static DataTable SelectInvoiceAptForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            var dtReturn = ReceiptMngDao.SelectInvoiceAptForTransfer(strRentCd, strRoomNo, strPayDt, strEPayDt, strItemCd, strTenantNm, strIsSent);
            return dtReturn;
        }

        #region SelectExcelInvoiceAptForTransfer : 정산정보조회

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

            dsReturn = ReceiptMngDao.SelectExcelInvoiceAptForTransfer(strRentCd, strRoomNo, strPayDt, strEPayDt, strItemCd, strTenantNm, strIsSent);

            return dsReturn;
        }

        #endregion


        public static DataTable SelectInvoiceTowerForTransfer(string strRentCd, string strRoomNo, string strPayDt, string strEPayDt,
                                                            string strItemCd, string strTenantNm, string strIsSent)
        {
            var dtReturn = ReceiptMngDao.SelectInvoiceTowerForTransfer(strRentCd, strRoomNo, strPayDt, strEPayDt, strItemCd, strTenantNm, strIsSent);
            return dtReturn;
        }

        public static DataTable SelectReprintListAptHoadon(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string invoiceNo, string strLangCd)
        {
            var dtReturn = ReceiptMngDao.SelectRepintListAptHoadon(strRentCd, strRoomNo, strPayDt, strItemCd, invoiceNo, strLangCd);
            return dtReturn;
        }

        //PhuongTV
        public static DataTable SelectListAptHoadonForCancel(string strRentCd, string strRoomNo, string strPayDt,
                                                           string strItemCd, string invoiceNo, string strLangCd)
        {
            var dtReturn = ReceiptMngDao.SelectListAptHoadonForCancel(strRentCd, strRoomNo, strPayDt, strItemCd, invoiceNo, strLangCd);
            return dtReturn;
        }

        //BaoTV
        public static DataTable SelectCancelListAptHoadon(string strInvoiceNo, string strSerialNo)
        {
            var dtReturn = ReceiptMngDao.SelectCancelListAptHoadon(strInvoiceNo, strSerialNo);
            return dtReturn;
        }

        //PhuongTV
        public static DataTable SelectCancelListKNHoadon(string strInvoiceNo)
        {
            var dtReturn = ReceiptMngDao.SelectCancelListKNHoadon(strInvoiceNo);
            return dtReturn;
        }


        #endregion

        #region Select InvoiceNo for Replace

        public static DataTable SelectAptHoadonForReplace(string strInvoiceNo)
        {
            var dtReturn = ReceiptMngDao.SelectAptHoadonForReplace(strInvoiceNo);
            return dtReturn;
        }

        public static DataTable SelectAptHoadonForReplaceDetail(string strInvoiceNo, string strRefSeq)
        {
            var dtReturn = ReceiptMngDao.SelectAptHoadonForReplaceDetail(strInvoiceNo, strRefSeq);
            return dtReturn;
        }

        #endregion

        #region Select For APT Parking Fee PrintOut Invoice

        //Phuongtv
        public static DataTable SelectPrintListHoadonAptPKF(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string printYN, string strLangCd)
        {
            var dtReturn = ReceiptMngDao.SelectPrintListHoadonAptPKF(strRentCd, strRoomNo, strPayDt, strItemCd, printYN, strLangCd);
            return dtReturn;
        }

        //Phuongtv
        public static DataTable SelectSpecialHoadonAptPKF(string strRentCd, string strRoomNo, string strPayDt,
                                                            string strItemCd, string printYN, string strLangCd, string strCompNm, string strInvoiceNo)
        {
            var dtReturn = ReceiptMngDao.SelectSpecialHoadonAptPKF(strRentCd, strRoomNo, strPayDt, strItemCd, printYN, strLangCd, strCompNm, strInvoiceNo);
            return dtReturn;
        }

        //Phuongtv
        public static DataSet SelectIssAmtPrintoutHoadonMerge(string IssDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ReceiptMngDao.SelectIssAmtPrintoutHoadonMerge(IssDt);

            return dsReturn;
        }


        #endregion

        #region SpreadHoadonForEntireIssuing : 모아찍기를 위한 화돈 대상 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadHoadonForEntireIssuing
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 모아찍기를 위한 화돈 대상 리스트 조회
         * Input    값 : SpreadHoadonForEntireIssuing(호, 섹션코드, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadHoadonForEntireIssuing : 모아찍기를 위한 화돈 대상 리스트 조회
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
        public static DataTable SpreadHoadonForEntireIssuing(string strRoomNo, string strRentCd, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                             string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.SelectHoadonForEntireIssuing(strRoomNo, strRentCd, strYear, strMonth, strStartDt, strEndDt, strItemCd, strUserTaxCd, strRssNo,
                                                                  strLangCd);

            return dtReturn;
        }

        #endregion

        #region RegistryPrintReciptList : 영수증 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryPrintReciptList
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
        public static DataTable RegistryPrintReciptList(string strPrintSeq, string strBillCd, string strDocCd, string strRentCd, int intFloorNo,
                                                        string strRoomNo, string strMngYear, string strMngMonth, string strPaymentCd, string strUserSeq,
                                                        string strCashier, string strDescription, double dblAmtViNo, double dblDongToDollar,
                                                        string strCompNo, string strInsMemNo, string strInsIP, string strDebitCreditCd, string strPaymentDt,
                                                        int intPaymentSeq, int intPaymentDetSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertPrintReciptList(strPrintSeq, strBillCd, strDocCd, strRentCd, intFloorNo, strRoomNo, strMngYear,
                                                           strMngMonth, strPaymentCd, strUserSeq, strCashier, strDescription, dblAmtViNo, dblDongToDollar,
                                                           strCompNo, strInsMemNo, strInsIP, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);

            return dtReturn;
        }

        /**********************************************************************************************
         * Mehtod   명 : RegistryPrintReciptList
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
        /// <returns></returns>
        public static DataTable RegistryPrintReciptList(string strPrintSeq, string strBillCd, string strDocCd, string strRentCd, int intFloorNo,
                                                        string strRoomNo, string strMngYear, string strMngMonth, string strPaymentCd, string strUserSeq,
                                                        string strCashier, string strDescription, double dblAmtViNo, double dblDongToDollar,
                                                        string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();

            string strDebitCreditCd = string.Empty;
            string strPaymentDt = string.Empty;
            int intPaymentSeq = 0;
            int intPaymentDetSeq = 0;

            dtReturn = ReceiptMngDao.InsertPrintReciptList(strPrintSeq, strBillCd, strDocCd, strRentCd, intFloorNo, strRoomNo, strMngYear,
                                                           strMngMonth, strPaymentCd, strUserSeq, strCashier, strDescription, dblAmtViNo, dblDongToDollar,
                                                           strCompNo, strInsMemNo, strInsIP, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);

            return dtReturn;
        }

        #endregion

        #region RegistryPrintReciptParkingCardMinusList : 주차카드용 마이너스 영수증 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryPrintReciptParkingCardMinusList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 주차카드용 마이너스 영수증 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryPrintReciptParkingCardMinusList : 주차카드용 마이너스 영수증 등록
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">환불일</param>
        /// <param name="intPaymentSeq">환불상세순번</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsIP">입력IP/param>
        /// <returns></returns>
        public static DataTable RegistryPrintReciptParkingCardMinusList(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertPrintReciptParkingCardMinusList(strDebitCreditCd, strPaymentDt, intPaymentSeq, strCompNo, strInsMemNo, strInsIP);

            return dtReturn;
        }

        #endregion

        #region InsertHoaDonParkingAPTReturn

        public static DataTable InsertHoaDonParkingAPTReturn(string strRentCd, string strRoomNo, string strParkingCardNo, string strPaymentDt, string strPaymentSeq, string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertHoaDonParkingAPTReturn(strRentCd, strRoomNo, strParkingCardNo, strPaymentDt, strPaymentSeq, strCompNo, strInsMemNo, strInsIP);

            return dtReturn;
        }

        #endregion

        #region RegistryPrintReciptRentalMngMinusList : 임대료 및 관리비용 마이너스 영수증 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryPrintReciptRentalMngMinusList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-04
         * 용       도 : 임대료 및 관리비용 마이너스 영수증 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryPrintReciptRentalMngMinusList : 임대료 및 관리비용 마이너스 영수증 등록
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">환불일</param>
        /// <param name="intPaymentSeq">환불상세순번</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsIP">입력IP/param>
        /// <returns></returns>
        public static DataTable RegistryPrintReciptRentalMngMinusList(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strItemCd, string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertPrintReciptRentalMngMinusList(strDebitCreditCd, strPaymentDt, intPaymentSeq, strItemCd, strCompNo, strInsMemNo, strInsIP);

            return dtReturn;
        }

        #endregion

        #region RegistryPrintAddonList : 영수증 발행자 및 발급자 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryPrintAddonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 영수증 발행자 및 발급자 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryPrintAddonList : 영수증 발행자 및 발급자 등록
        /// </summary>
        /// <param name="strPrintSeq">프린트순번</param>
        /// <param name="intPrintDetSeq">프린트상세순번</param>
        /// <param name="strCompNo">소속회사코드</param>
        /// <param name="strInsMemNo">담당자사번</param>
        /// <param name="strInsIP">담당자IP</param>
        /// <returns></returns>
        public static DataTable RegistryPrintAddonList(string strPrintSeq, int intPrintDetSeq, string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertPrintAddonList(strPrintSeq, intPrintDetSeq, strCompNo, strInsMemNo, strInsIP);

            return dtReturn;
        }

        #endregion

        #region RegistryMoneyInfo : 금전 로그입력

        /**********************************************************************************************
         * Mehtod   명 : RegistryMoneyInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-23
         * 용       도 : 금전 로그입력
         * Input    값 : RegistryMoneyInfo(대차코드, 지불일, 지불순번, 지불상세순번)
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
        public static DataTable RegistryMoneyInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertMoneyInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq);

            return dtReturn;
        }

        #endregion

        #region RegistryMoneyMinusInfo : 금전 로그 차감입력

        /**********************************************************************************************
         * Mehtod   명 : RegistryMoneyMinusInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 금전 로그 차감입력
         * Input    값 : RegistryMoneyMinusInfo(대차코드, 지불일, 지불순번, 프린트순번, 회사코드, 사원코드, 접속IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryMoneyMinusInfo : 금전 로그 차감입력
        /// </summary>
        /// <param name="strDebitCreditCd">대차코드</param>
        /// <param name="strPaymentDt">지불일</param>
        /// <param name="intPaymentSeq">지불순번</param>
        /// <param name="strPrintSeq">프린트순번</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원코드</param>
        /// <param name="strInsMemIP">접속IP</param>
        /// <returns></returns>
        public static DataTable RegistryMoneyMinusInfo(string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, string strPrintSeq, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertMoneyMinusInfo(strDebitCreditCd, strPaymentDt, intPaymentSeq, strPrintSeq, strInsCompNo, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion 
                
        #region RegistryTempPrintOutList : 통합 영수증 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempPrintOutList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-17
         * 용       도 : 통합 영수증 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempPrintOutList : 통합 영수증 등록
        /// </summary>
        /// <param name="strPrintDt">출력일자</param>
        /// <param name="intPrintDtSeq">출력일자순번</param>
        /// <param name="strPrintSeq">출력순번</param>
        /// <param name="strPrintDetSeq">출력상세순번</param>
        /// <returns></returns>
        public static DataTable RegistryTempPrintOutList(string strPrintDt, int intPrintDtSeq, string strPrintSeq, int strPrintDetSeq)
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_SET_INSERT_PRINTINFO_S04
            dtReturn = ReceiptMngDao.InsertTempPrintOutList(strPrintDt, intPrintDtSeq, strPrintSeq, strPrintDetSeq);

            return dtReturn;
        }

        #endregion

        #region RegistryCNAPTTempHoadonList : 체스넛 아파트 임시 세금계산서 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryCNAPTTempHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 체스넛 아파트 임시 세금계산서 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryCNAPTTempHoadonList : 체스넛 아파트 임시 세금계산서 등록
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
        public static DataTable RegistryCNAPTTempHoadonList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                            string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                            string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertCNAPTTempHoadonList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintOutDt, intPrintOutSeq,
                                                               strBillCd, strBillNo, strLangCd, strInsCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryKNAPTTempHoadonList : 경남비나 아파트상가 임시 세금계산서 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryKNAPTTempHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 경남비나 아파트상가 임시 세금계산서 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryKNAPTTempHoadonList : 경남비나 아파트상가 임시 세금계산서 등록
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
        public static DataTable RegistryKNAPTTempHoadonList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                            string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                            string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertKNAPTTempHoadonList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintOutDt, intPrintOutSeq,
                                                               strBillCd, strBillNo, strLangCd, strInsCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryKNTempHoadonList : 경남비나 오피스 및 리테일 임시 세금계산서 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryKNTempHoadonList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 경남비나 오피스 및 리테일 임시 세금계산서 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryKNTempHoadonList : 경남비나 오피스 및 리테일 임시 세금계산서 등록
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
        public static DataTable RegistryKNTempHoadonList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                         string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                         string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReceiptMngDao.InsertKNTempHoadonList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintOutDt, intPrintOutSeq,
                                                            strBillCd, strBillNo, strLangCd, strInsCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryCNAPTTempHoadonTotalList : 체스넛 아파트 임시 세금계산서 합산 테이블 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryCNAPTTempHoadonTotalList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 체스넛 아파트 임시 세금계산서 합산 테이블 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryCNAPTTempHoadonTotalList : 체스넛 아파트 임시 세금계산서 합산 테이블 등록
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
        public static object[] RegistryCNAPTTempHoadonTotalList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                                string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                                string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ReceiptMngDao.InsertCNAPTTempHoadonTotalList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintOutDt, intPrintOutSeq,
                                                                    strBillCd, strBillNo, strLangCd, strInsCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryKNAPTTempHoadonTotalList : 경남비나 아파트상가 임시 세금계산서 합산 테이블 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryKNAPTTempHoadonTotalList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 경남비나 아파트상가 임시 세금계산서 합산 테이블 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryKNAPTTempHoadonTotalList : 경남비나 아파트상가 임시 세금계산서 합산 테이블 등록
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
        public static object[] RegistryKNAPTTempHoadonTotalList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                                string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                                string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ReceiptMngDao.InsertKNAPTTempHoadonTotalList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintOutDt, intPrintOutSeq,
                                                                    strBillCd, strBillNo, strLangCd, strInsCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryKNTempHoadonTotalList : 경남비나 오피스 및 리테일 임시 세금계산서 합산 테이블 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryKNTempHoadonTotalList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 경남비나 오피스 및 리테일 임시 세금계산서 합산 테이블 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryKNTempHoadonTotalList : 경남비나 오피스 및 리테일 임시 세금계산서 합산 테이블 등록
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
        public static object[] RegistryKNTempHoadonTotalList(string strUserSeq, string strDebitCreditCd, string strPaymentDt, int intPaymentSeq, int intPaymentDetSeq,
                                                             string strPrintOutDt, int intPrintOutSeq, string strBillCd, string strBillNo, string strLangCd,
                                                             string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ReceiptMngDao.InsertKNTempHoadonTotalList(strUserSeq, strDebitCreditCd, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strPrintOutDt, intPrintOutSeq,
                                                                  strBillCd, strBillNo, strLangCd, strInsCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryTempHoadonTotalInfo : 모아찍을 내용을 세금계산서 합산 테이블 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempHoadonTotalInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-28
         * 용       도 : 모아찍을 내용을 세금계산서 합산 테이블 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempHoadonTotalInfo : 모아찍을 내용을 세금계산서 합산 테이블 등록
        /// </summary>
        /// <param name="strPrintOutDt"></param>
        /// <param name="intPrintOutSeq"></param>
        /// <param name="strTitle"></param>
        /// <returns></returns>
        public static DataTable RegistryTempHoadonTotalInfo(string strPrintOutDt, int intPrintOutSeq, string strTitle)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[3];

            dtReturn = ReceiptMngDao.InsertTempHoadonTotalInfo(strPrintOutDt, intPrintOutSeq, strTitle);

            return dtReturn;
        }

        #endregion

        #region RemoveTempPrintList : 영수증 출력 내역 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempPrintList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-02-17
         * 용       도 : 영수증 출력 내역 삭제
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempPrintList : 영수증 출력 내역 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveTempPrintList()
        {
            object[] objReturn = new object[2];

            objReturn = ReceiptMngDao.DeleteTempPrintList();

            return objReturn;
        }

        #endregion
    }
}