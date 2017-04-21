using System.Data;

using KN.Settlement.Dac;
using KN.Common.Method.Lib;

namespace KN.Settlement.Biz
{
    public class InvoiceMngBlo
    {
        #region Select HoadonListInfo

        public static DataTable SpreadHoadonListInfo(string strRentCd, string strRoomNo, string strItemCd, string strSvcYear, string strSvcMM, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectHoadonListInfo(strRentCd, strRoomNo, strItemCd, strSvcYear, strSvcMM, strLangCd);

            return dtReturn;
        }

        public static DataTable SelectHoadonListMaster(string strRentCd, string strRoomNo, string strItemCd, string strSvcYear, string strSvcMM, string strLangCd, string invoiceYN)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectHoadonListMaster(strRentCd, strRoomNo, strItemCd, strSvcYear, strSvcMM, strLangCd, invoiceYN);
            return dtReturn;
        }

        public static DataTable SelectHoadonListMaster1(string strRentCd, string strRoomNo, string strInvoiceNo, string strItemCd, string strLangCd, string invoiceYN, string strStartDt, string strEndDt,string compNm)
        {
            var dtReturn = InvoiceMngDao.SelectHoadonListMaster1(strRentCd, strRoomNo, strInvoiceNo, strItemCd, strLangCd, invoiceYN, strStartDt, strEndDt,compNm);
            return dtReturn;
        }

        #region Select Exchange Rate by Payment Date

        public static DataTable SelectExRateByPayDt(string strRentCd, string strPayDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectExRateByPayDt(strRentCd, strPayDt);
            return dtReturn;
        }

        #endregion

        #region Select KeangNam Hoadon List for Cancel

        public static DataTable SelectKNHoadonListCanCel(string strRentCd, string strRoomNo, string strInvoiceNo, string strItemCd, string strLangCd, string billTy, string strStartDt, string strEndDt, string compNm)
        {
            var dtReturn = InvoiceMngDao.SelectKNHoadonListCanCel(strRentCd, strRoomNo, strInvoiceNo, strItemCd, strLangCd, billTy, strStartDt, strEndDt, compNm);
            return dtReturn;
        }

        #endregion

        //BaoTV
        public static DataTable SelectHoadonListExcel(string strRentCd, string strRoomNo, string strInvoiceNo, string strItemCd, string strLangCd, string invoiceYN, string strStartDt, string strEndDt,string compNm)
        {
            var dtReturn = InvoiceMngDao.SelectHoadonListExcel(strRentCd, strRoomNo, strInvoiceNo, strItemCd, strLangCd, invoiceYN, strStartDt, strEndDt,compNm);
            return dtReturn;
        }

        public static DataTable SelectHoadonListDetail(string refSerialNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectHoadonListDetail(refSerialNo);
            return dtReturn;
        }
         public static DataTable SelectReprintHoadon(string refPrintBundleNo)
         {
             DataTable dtReturn = new DataTable();

             dtReturn = InvoiceMngDao.SelectReprintHoadon(refPrintBundleNo);
             return dtReturn;
         }

         public static DataTable SelectReprintHoadon(string refPrintBundleNo, string invoiceNo)
         {
             DataTable dtReturn = new DataTable();

             dtReturn = InvoiceMngDao.SelectReprintHoadon(refPrintBundleNo,invoiceNo);
             return dtReturn;
         }

         public static DataTable SelectReprintInvoiceNo(string compNo)
         {
             DataTable dtReturn = new DataTable();

             dtReturn = InvoiceMngDao.SelectReprintInvoiceNo(compNo);
             return dtReturn;
         }

         public static DataTable SelectBillCode(string typeCode, string langCode, string compNo)
         {
             DataTable dtReturn = new DataTable();

             dtReturn = InvoiceMngDao.SelectBillCode(typeCode, langCode, compNo);
             return dtReturn;
         }

        #endregion

        #region SelectExistLineForInvoiceNo : 중복된 세금계산서 번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistLineForInvoiceNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-23
         * 용       도 : 중복된 세금계산서 번호 조회
         * Input    값 : InsertHoadonAddon(새세금계산서 번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExistLineForInvoiceNo : 중복된 세금계산서 번호 조회
        /// </summary>
        /// <param name="strNewInvoiceNo">새세금계산서 번호</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectExistLineForInvoiceNo(string strNewInvoiceNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectExistLineForInvoiceNo(strNewInvoiceNo);

            return dtReturn;
        }

        #endregion

        #region Updating Invoice no for hoadon : 인보이스 번호 업데이트 실시

        /**********************************************************************************************
         * Mehtod   명 : UpdatingInvoiceNoForHoadon
         * 개   발  자 : BaoTv
         * 생   성  일 : 2013-03-05
         * 용       도 : Updating Invoice no for print each hoadon 
         * Input    값 : 
         * Ouput    값 : DataTable
        **********************************************************************************************/
        /// <summary>
        /// UpdatingInvoiceNoForHoadon : Updating Innvoice No for Hoadon to print
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="refSerialNo"></param>
        /// <param name="monthAmtNo"></param>
        /// <param name="rentCd"></param>
        /// <param name="PrintBundleNo"></param>
        /// <returns></returns>
        public static DataTable UpdatingInvoiceNoForHoadon(string contractType, string refSerialNo, string monthAmtNo, string rentCd, string PrintBundleNo,string billCD)
        {

            var dtreturn = InvoiceMngDao.UpdatingInvoiceNoForHoadon(contractType, refSerialNo, monthAmtNo, rentCd, PrintBundleNo,billCD);

            return dtreturn;
        }

        public static DataTable UpdatingInvoiceSpecialIssueDt(string printBundleNo, string issueDt)
        {

            var dtreturn = InvoiceMngDao.UpdatingInvoiceSpecialIssueDt(printBundleNo,issueDt);

            return dtreturn;
        }


        #endregion

        #region Update HoadonInfoAPTNew CC

        public static DataTable UpdatingHoadonInfoAPTNewCC(string printBundleNo)
        {
            var dtreturn = InvoiceMngDao.UpdatingHoadonInfoAPTNewCC(printBundleNo);

            return dtreturn;
        }

        public static DataTable UpdatingCancelPaymentListAPTNew(string invoiceNo, string roomNo, string payDt)
        {
            var dtreturn = InvoiceMngDao.UpdatingCancelPaymentListAPTNew(invoiceNo, roomNo, payDt);

            return dtreturn;
        }

        #endregion

        #region Update HoadonInfo Hidden

        public static DataTable UpdatingHoadonInfoHiddenY(string printBundleNo)
        {
            var dtreturn = InvoiceMngDao.UpdatingHoadonInfoHiddenY(printBundleNo);

            return dtreturn;
        }

        public static DataTable UpdatingHoadonInfoHiddenN(string printBundleNo)
        {
            var dtreturn = InvoiceMngDao.UpdatingHoadonInfoHiddenN(printBundleNo);

            return dtreturn;
        }

        #endregion

        #region Update KN Report Cancel HoadonInfo NM to CC

        public static DataTable UpdatingKNHoadonInfoCancelCC(string printBundleNo)
        {
            var dtreturn = InvoiceMngDao.UpdatingKNHoadonInfoCancelCC(printBundleNo);

            return dtreturn;
        }

        public static DataTable UpdatingKNHoadonInfoCancelNM(string printBundleNo)
        {
            var dtreturn = InvoiceMngDao.UpdatingKNHoadonInfoCancelNM(printBundleNo);

            return dtreturn;
        }

        #endregion

        #region Update PrintBundleNo for HoadonInfo

        //Update Set PRINT_BUNDLE_NO = REF_SEQ_NO */      

        public static DataTable UpdatingPrintBundleNoHoadonInfo(string refSeq, string printBundleNo)
        {
            var dtreturn = InvoiceMngDao.UpdatingPrintBundleNoHoadonInfo(refSeq, printBundleNo);

            return dtreturn;
        }

        #endregion

        #region  Update PrintBundleNo for Cancel HoadonInfo KeangNam

        //Update Set PRINT_BUNDLE_NO = REF_SEQ_NO */      

        public static DataTable UpdatingPrintBundleNoKNCanHoadonInfo(string refSeq, string printBundleNo)
        {
            var dtreturn = InvoiceMngDao.UpdatingPrintBundleNoKNCanHoadonInfo(refSeq, printBundleNo);

            return dtreturn;
        }

        #endregion

        #region Updating DebitList

        ///**********************************************************************************************
        // * Mehtod   명 : UpdatingPrintBundleNoForDebitList 
        // * 개   발  자 : Phuongtv
        // * 생   성  일 : 2013-04-02
        // * 용       도 : Updating PRINT_BUNDLE_NO
        // * Input    값 : 
        // * Ouput    값 : DataTable
        // **********************************************************************************************/
        ///// <summary>
        ///// UpdatingInvoiceNoForHoadon : Updating Innvoice No for Hoadon to print
        ///// </summary>
        ///// <param name="contractType"></param>
        ///// <param name="refSerialNo"></param>
        ///// <param name="monthAmtNo"></param>
        ///// <param name="rentCd"></param>
        ///// <param name="PrintBundleNo"></param>
        ///// <returns></returns>
        public static DataTable UpdatingPrintBundleNo(string refSeq, string printBundleNo )
        {
            var dtreturn = InvoiceMngDao.UpdatingPrintBundleNo(refSeq ,printBundleNo);

            return dtreturn;
        }

        public static DataTable UpdatingPrintParkingDebitList(string refSeq, string printBundleNo)
        {
            var dtreturn = InvoiceMngDao.UpdatingPrintParkingDebitList(refSeq, printBundleNo);

            return dtreturn;
        }

        public static DataTable UpdatingPrintBundleNoYN(string rentCd, string refSerialNo, string printBundleNo, string refSeq)
        {
            var dtreturn = new DataTable();

            dtreturn = InvoiceMngDao.UpdatingPrintBundleNoYN(rentCd, refSerialNo, printBundleNo, refSeq);

            return dtreturn;
        }

        public static DataTable UpdatingNullPrintBundleNoHoadonInfo(string rentCd)
        {
            var dtreturn = new DataTable();

            dtreturn = InvoiceMngDao.UpdatingNullPrintBundleNoHoadonInfo(rentCd);

            return dtreturn;
        }

        public static DataTable UpdatingNullPrintBundleNo(string rentCd)
        {
            var dtreturn = new DataTable();

            dtreturn = InvoiceMngDao.UpdatingNullPrintBundleNo(rentCd);

            return dtreturn;
        }

        public static DataTable UpdatingNullParkingDebitList(string rentCd)
        {
            var dtreturn = new DataTable();

            dtreturn = InvoiceMngDao.UpdatingNullParkingDebitList(rentCd);

            return dtreturn;
        }

        public static DataTable UpdatingNullPrintBundleNoYN(string rentCd)
        {
            var dtreturn = new DataTable();

            dtreturn = InvoiceMngDao.UpdatingNullPrintBundleNoYN(rentCd);

            return dtreturn;
        }

        public static DataTable UpdatingCreateYN(string printBundleNo)
        {
            var dtreturn = new DataTable();

            dtreturn = InvoiceMngDao.UpdatingCreateYN(printBundleNo);

            return dtreturn;
        }
        public static DataTable UpdatingIsPrintAPT(string refPrintNo)
        {
            var dtreturn = new DataTable();

            dtreturn = InvoiceMngDao.UpdatingIsPrintAPT(refPrintNo);

            return dtreturn;
        }

        #endregion

        /**********************************************************************************************
         * Mehtod   명 : UpdatingBundleSeqNoForResetS
         * 개   발  자 : BaoTv
         * 생   성  일 : 2013-04-03
         * 용       도 : UpdatingBundleSeqNo for reset 
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdatingInvoiceNoForHoadon : Updating Innvoice No for Hoadon to print
        /// </summary>
        /// <returns></returns>
        public static DataTable UpdatingBundleSeqNoForReset()
        {
            var dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.UpdatingBundleSeqNoForReset();

            return dtReturn;
        }

        #region Reset Ref_PrintNo For APT Management

        /**********************************************************************************************
         * Mehtod   명 : UpdatingRefPrintNoAPTForReset
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-05-06
         * 용       도 : Updating Ref_PrintNo  for reset 
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdatingInvoiceNoForHoadon : Updating Innvoice No for Hoadon to print
        /// </summary>
        /// <returns></returns>
        public static DataTable UpdatingRefPrintNoAPTForReset()
        {
            var dtReturn = InvoiceMngDao.UpdatingRefPrintNoAPTForReset();

            return dtReturn;
        }

        public static DataTable UpdatingRefPrintNoHoaDonAPTForReset()
        {
            var dtReturn = InvoiceMngDao.UpdatingRefPrintNoHoaDonAPTForReset();

            return dtReturn;
        }

        #endregion

        #region Reset Ref_PrintNo For HoaDonParkingAPT

        /**********************************************************************************************
         * Mehtod   명 : UpdatingRefPrintNoAPTParkingForReset
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-06-15
         * 용       도 : Updating Ref_PrintNo  for reset 
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdatingInvoiceNoForHoadon : Updating Innvoice No for Hoadon to print
        /// </summary>
        /// <returns></returns>
        public static DataTable UpdatingRefPrintNoAPTParkingForReset()
        {
            var dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.UpdatingRefPrintNoAPTParkingForReset();

            return dtReturn;
        }

        #endregion

        #region Update Ref_PrintNo For Print APT Management

        /**********************************************************************************************
         * Mehtod   명 : UpdatingRefPrintNoAPTForReset
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-05-06
         * 용       도 : Updating Ref_PrintNo  for reset 
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdatingInvoiceNoForHoadon : Updating Innvoice No for Hoadon to print
        /// </summary>
        /// <returns></returns>
        /// 
        public static DataTable UpdatingRefPrintNoForAPT(string refSeq, string rentCd, string refPrintNo, string payDt)
        {
            var dtReturn = new DataTable();

           //dtReturn = InvoiceMngDao.UpdatingRefPrintNoForAPT(refSeq, rentCd, refPrintNo, payDt);

            return dtReturn;
        }

        public static DataTable UpdatingRefPrintNoForAPTNew(string refSeq, string rentCd, string refPrintNo, string payDt,int seq)
        {
            var dtReturn = InvoiceMngDao.UpdatingRefPrintNoForAPT(refSeq, rentCd, refPrintNo, payDt,seq);

            return dtReturn;
        }

        public static DataTable UpdatingRefPrintNoForHoaDonAPT(string refSeq,  string refPrintNo)
        {
            var dtReturn = InvoiceMngDao.UpdatingRefPrintNoForHoaDonAPT(refSeq, refPrintNo);

            return dtReturn;
        }

        #endregion


        #region Update Ref_PrintNo For HoaDonParkingAPT -phuongtv

        public static DataTable UpdatingRefPrintNoForHoaDonParkingAPT(string refSeq, string rentCd, string refPrintNo, string payDt, int seq)
        {
            var dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.UpdatingRefPrintNoForHoaDonParkingAPT(refSeq, rentCd, refPrintNo, payDt, seq);

            return dtReturn;
        }

        #endregion


        #region UPdate Ref_PrintNo For Merge Apt Parking Invoice

        public static DataTable UpdatingMergeAptParkingFee(string refSeq, string rentCd, string refPrintNo, string payDt)
        {
            var dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.UpdatingMergeAptParkingFee(refSeq, rentCd, refPrintNo, payDt);

            return dtReturn;
        }
         
        #endregion

        #region Update IsPrint YN For APt

        public static DataTable UpdatingPrintYNForAPT(string strPSeq, int intSeq)
        {
            var dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.UpdatingPrintYNForAPT(strPSeq, intSeq);

            return dtReturn;
        }

        #endregion

        #region Update Invoice No for HoadonInfoApt

        public static DataTable UpdatingInvoiceNoHoadonInfoApt(string printSeq, string InsCompCd, string InsMemNo, string InsMemIP)
        {
           // var dtReturn = InvoiceMngDao.UpdatingInvoiceNoHoadonInfoApt(printSeq, InsCompCd, InsMemNo, InsMemIP);

            return null;
        }

        public static DataTable UpdatingInvoiceNoHoadonInfoAptNew(string printSeq, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var dtReturn = InvoiceMngDao.UpdatingInvoiceNoHoadonInfoApt(printSeq, InsCompCd, InsMemNo, InsMemIP);

            return dtReturn;
        }

        public static DataTable UpdatingInvoiceAdjNoHoadonInfoAptNew(string printSeq, string oldInvoiceNo, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var dtReturn = InvoiceMngDao.UpdatingInvoiceAdjNoHoadonInfoAptNew(printSeq, oldInvoiceNo, InsCompCd, InsMemNo, InsMemIP);

            return dtReturn;
        }

        public static DataTable InsertInvoiceIncreaseApt(string refSeq, double dbAmount,string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var dtReturn = InvoiceMngDao.InsertInvoiceIncreaseApt(refSeq,dbAmount, InsCompCd, InsMemNo, InsMemIP);

            return dtReturn;
        }

        #endregion


        #region Update Invoice No for HoadonInfoApt(Parking Fee)

        public static DataTable UpdatingInvoiceNoHoadonAptPKF(string printSeq, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var dtReturn = InvoiceMngDao.UpdatingInvoiceNoHoadonAptPKF(printSeq, InsCompCd, InsMemNo, InsMemIP);

            return dtReturn;
        }

        #endregion

        #region Update InvoiceNo for HoadonInfoApt From HoadonInfoAptTrans (Parking Fee)

        public static DataTable UpdatingInvoiceNoHoadonAptPKF_Trans(string printSeq, string InsCompCd, string InsMemNo, string InsMemIP, string PrintDt)
        {
            var dtReturn = InvoiceMngDao.UpdatingInvoiceNoHoadonAptPKF_Trans(printSeq, InsCompCd, InsMemNo, InsMemIP, PrintDt);

            return dtReturn;
        }

        #endregion

        #region Insert Cancel Invoice for HoadonInfoApt

        public static DataTable InsertCancelInvoiceHoadonInfoApt(string printNo, string invoiceNo,string reson, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var dtReturn = InvoiceMngDao.InsertCancelInvoiceHoadonInfoApt(printNo,invoiceNo, reson,InsCompCd, InsMemNo, InsMemIP);

            return dtReturn;
        }

        #endregion

        #region Insert Merge Invoice for HoadonInfoApt

        public static DataTable InsertMergeInvoiceHoadonInfoApt(string invoiceNo, string insCompCd, string insMemNo, string insMemIP)
        {
            var dtReturn = InvoiceMngDao.InsertMergeInvoiceHoadonInfoApt(invoiceNo, insCompCd, insMemNo, insMemIP);

            return dtReturn;
        }

        #endregion

        #region Insert Merge Invoice A0000 for HoadonInfoApt

        public static DataTable InsertMergeInvoiceA000HoadonInfoApt(string invoiceNo, string insCompCd, string insMemNo, string insMemIP)
        {
            var dtReturn = InvoiceMngDao.InsertMergeInvoiceA0000HoadonInfoApt(invoiceNo, insCompCd, insMemNo, insMemIP);

            return dtReturn;
        }

        #endregion

        #region Insert Merge Invoice for HoadonInfoAptParkingFee

        public static DataTable InsertMergeInvoiceAptPKF(string invoiceNo, string insCompCd, string insMemNo, string insMemIP, string PrintDt)
        {
            var dtReturn = InvoiceMngDao.InsertMergeInvoiceAptPKF(invoiceNo, insCompCd, insMemNo, insMemIP, PrintDt);

            return dtReturn;
        }

        #endregion

        #region Insert Adjustment Invoice for HoadonInfoApt

        public static DataTable InsertAdjustInvoiceHoadonInfoApt(string invoiceNo, string insCompCd, string insMemNo, string insMemIP)
        {
            var dtReturn = InvoiceMngDao.InsertAdjustInvoiceHoadonInfoApt(invoiceNo, insCompCd, insMemNo, insMemIP);

            return dtReturn;
        }

        #endregion

        #region Insert Temp Invoice for Cancel HoadonInfoApt

        public static DataTable InsertTempInvoiceApt(string invoiceNo)
        {
            var dtReturn = InvoiceMngDao.InsertTempInvoiceApt(invoiceNo);

            return dtReturn;
        }

        //BaoTV
        public static DataTable UpdatingCancelInvoiceForApt(string strPSeq, int intSeq,double netAmt, double vatAmt)
        {
            var dtReturn = InvoiceMngDao.UpdatingCancelInvoiceForApt(strPSeq, intSeq,netAmt,vatAmt);

            return dtReturn;
        }

        #endregion






        #region SelectExistLineForAPTInvoiceNo : 아파트용 중복된 세금계산서 번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistLineForAPTInvoiceNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-23
         * 용       도 : 아파트용 중복된 세금계산서 번호 조회
         * Input    값 : InsertHoadonAddon(새세금계산서 번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExistLineForAPTInvoiceNo : 아파트용 중복된 세금계산서 번호 조회
        /// </summary>
        /// <param name="strNewInvoiceNo">새세금계산서 번호</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectExistLineForAPTInvoiceNo(string strNewInvoiceNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            dtReturn = InvoiceMngDao.SelectExistLineForAPTInvoiceNo(strNewInvoiceNo);

            return dtReturn;
        }

        #endregion

        #region SelectInvoiceReprintList : 세금계산서 재출력 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectInvoiceReprintList
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-23
         * 용       도 : 세금계산서 재출력 리스트 조회
         * Input    값 : SelectInvoiceReprintList(섹션코드, 호, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectInvoiceReprintList : 화돈 프린트 리스트 조회
        /// </summary>
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
        public static DataTable SelectInvoiceReprintList(string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                         string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectInvoiceReprintList(strRentCd, strRoomNo, strYear, strMonth, strStartDt, strEndDt, strItemCd, strUserTaxCd, strRssNo, strLangCd);

            return dtReturn;
        }

        //Bao-TV
        public static DataTable SelectInvoiceReprintExcel(string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                         string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            var dtReturn = InvoiceMngDao.SelectInvoiceReprintExcel(strRentCd, strRoomNo, strYear, strMonth, strStartDt, strEndDt, strItemCd, strUserTaxCd, strRssNo, strLangCd);

            return dtReturn;
        }

        #endregion

        #region SelectInvoiceReprintListForAPT : 아파트용 세금계산서 재출력 리스트 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectInvoiceReprintListForAPT
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-23
         * 용       도 : 아파트용 세금계산서 재출력 리스트 조회
         * Input    값 : SelectInvoiceReprintList(섹션코드, 호, 조회년, 조회월, 조회시작일, 조회종료일, 항목코드, 세금코드, 사업자번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 아파트용 세금계산서 재출력 리스트 조회
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strYear">년</param>
        /// <param name="strMonth">월</param>
        /// <param name="strStartDt">조회시작일</param>
        /// <param name="strEndDt">조회종료일</param>
        /// <param name="strItemCd">항목코드</param>
        /// <param name="strUserTaxCd">세금코드</param>
        /// <param name="strRssNo">사업자번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="intStartFloor">조회시작층</param>
        /// <param name="intEndFloor">조회종료층</param>
        /// <returns></returns>
        public static DataTable SelectInvoiceReprintListForAPT(string strCompNo, string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                               string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd, int intStartFloor, int intEndFloor)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectInvoiceReprintListForAPT(strCompNo, strRentCd, strRoomNo, strYear, strMonth, strStartDt, strEndDt, strItemCd, strUserTaxCd,
                                                                    strRssNo, strLangCd, intStartFloor, intEndFloor);

            return dtReturn;
        }

        #endregion

        #region SelectMaxInvoiceNo : 세금계산서 마지막번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMaxInvoiceNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-25
         * 용       도 : 세금계산서 마지막번호 조회
         * Input    값 : SelectMaxInvoiceNo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMaxInvoiceNo : 화돈 프린트 리스트 조회
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectMaxInvoiceNo(string compNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectMaxInvoiceNo(compNo);

            return dtReturn;
        }

        #endregion

        #region SelectMaxInvoiceNoForAPT : 아파트용 세금계산서 마지막번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMaxInvoiceNoForAPT
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-25
         * 용       도 : 아파트용 세금계산서 마지막번호 조회
         * Input    값 : SelectMaxInvoiceNoForAPT()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMaxInvoiceNoForAPT : 아파트용 세금계산서 마지막번호 조회
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectMaxInvoiceNoForAPT()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.SelectMaxInvoiceNoForAPT();

            return dtReturn;
        }

        #endregion

        #region InsertHoadonAddon : 화돈출력 로그 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertHoadonAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-10
         * 용       도 : 화돈출력 로그 등록
         * Input    값 : InsertHoadonAddon(회사코드, 섹션코드, 수납문서코드, 아이템코드, 회계코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertHoadonAddon : 화돈출력 로그 등록
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strMemNo">담당자사번</param>
        /// <param name="strMemIp">담당자IP</param>
        /// <param name="strText">텍스트</param>
        /// <returns>object[]</returns>
        public static object[] InsertHoadonAddon(string strCompCd, string strMemNo, string strMemIp, string strText)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.InsertHoadonAddon(strCompCd, strMemNo, strMemIp, strText);

            return objReturn;
        }

        #endregion   

        public static object[] ModifyHoadonListInfo(string strInvoiceNo, string strSerialNo, string strPrintSeq, int intPrintDetSeq, string strSvcYear, string strSvcMM,
                                                    double dblExchageRate, double dblAmtViNo, string strDescription, string strUserNm, string strUserCd,
                                                    string strUserSeq, string strInsAddress, string strInsDetAddress, string strRentCd, string strFeeTy, string strInsRemark)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.UpdateHoadonListInfo(strInvoiceNo, strSerialNo, strPrintSeq, intPrintDetSeq, strSvcYear, strSvcMM, dblExchageRate,
                                                           dblAmtViNo, strDescription, strUserNm, strUserCd, strUserSeq, strInsAddress, strInsDetAddress,
                                                           strRentCd, strFeeTy, strInsRemark);

            return objReturn;
        }

        public static object[] UpdateHoadonListNew(string strInvoiceNo, string strBillCd, string strSerialNo, string strSvcYear, string strSvcMM, string strUserNm, string strUserAddr, string strUserDetAddress,
                                                    string strUserTaxCd, string strDescription, string strPaymentDt, string strSvcStartDt, string strSvcEndDt, double dbUnitPrice, double dbVatAmt, double dbDongToDollar,
                                                    double dbTotSellingPrice, string strIssuingDate, int intQty, string strRefSerialNo, string strRoomNo)
        {
            var objReturn = InvoiceMngDao.UpdateHoadonListNew(strInvoiceNo, strBillCd, strSerialNo, strSvcYear, strSvcMM, strUserNm, strUserAddr, strUserDetAddress,
                                                                   strUserTaxCd, strDescription, strPaymentDt, strSvcStartDt, strSvcEndDt, dbUnitPrice, dbVatAmt, dbDongToDollar,
                                                                   dbTotSellingPrice, strIssuingDate, intQty, strRefSerialNo, strRoomNo);

            return objReturn;
        }

        public static object[] CancelHoadon(string strRefSerialNo)
        {

            var objReturn = InvoiceMngDao.CancelHoadon(strRefSerialNo);

            return objReturn;
        }

        public static object[] UpdatelHoadonReprintList(string strRentCd, string strRefSerialNo, string strRefPrintBundleNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.UpdatelHoadonReprintList(strRentCd, strRefSerialNo, strRefPrintBundleNo);

            return objReturn;
        }

        public static object[] UpdatelHoadonReprintList1(string strRefPrintBundleNo)
        {

           var objReturn = InvoiceMngDao.UpdatelHoadonReprintList1(strRefPrintBundleNo);

            return objReturn;
        }


        //BaoTv
        public static object[] UpdatedAptDebitPrintNo(string strRefPrintBundleNo,string strFeeSeq)
        {
            var objReturn = InvoiceMngDao.UpdatedAptDebitPrintNo(strRefPrintBundleNo, strFeeSeq);

            return objReturn;
        }

        public static object[] UpdatedAptDebitPrintNoForMerge(string strRefPrintBundleNo, string strFeeSeq)
        {
            var objReturn = InvoiceMngDao.UpdatedAptDebitPrintNoForMerge(strRefPrintBundleNo, strFeeSeq);

            return objReturn;
        }

        public static object[] UpdatedAptInvoicePrintNo(string strRefPrintBundleNo, string strFeeSeq)
        {
            var objReturn = InvoiceMngDao.UpdatedAptDebitPrintNo(strRefPrintBundleNo, strFeeSeq);

            return objReturn;
        }

        //BaoTv
        public static object[] UpdatedAptDebitPrinted(string strRefPrintBundleNo)
        {
            var objReturn = InvoiceMngDao.UpdatedAptDebitPrinted(strRefPrintBundleNo);

            return objReturn;
        }

        #region InsertTempHoadonForEachAPT : 아파트용 임시화돈 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForEachAPT
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-09-07
         * 용       도 : 아파트용 임시화돈 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonForEachAPT : 아파트용 임시화돈 등록
        /// </summary>
        /// <param name="strLangCd"></param>
        /// <param name="strUserSeq"></param>
        /// <param name="strUserNm"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strAddr"></param>
        /// <param name="strDetAddr"></param>
        /// <param name="strUserTaxCd"></param>
        /// <param name="strModPaymentDt"></param>
        /// <param name="strCompNm"></param>
        /// <param name="strInvoiceContYn"></param>
        /// <param name="strHoadonNo"></param>
        /// <param name="strInvoiceNo"></param>
        /// <param name="strDescription"></param>
        /// <param name="dblTotSellingPrice"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static DataTable InsertTempHoadonForEachAPT(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt,
                                                           int intPaymentSeq, int intPaymentDetSeq, string strAddr, string strDetAddr, string strUserTaxCd,
                                                           string strModPaymentDt, string strCompNm, string strInvoiceContYn, string strHoadonNo,
                                                           string strInvoiceNo, string strDescription, double dblTotSellingPrice,
                                                           string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.InsertTempHoadonForEachAPT(strLangCd, strUserSeq, strUserNm, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strAddr,
                                                                strDetAddr, strUserTaxCd, strModPaymentDt, strCompNm, strInvoiceContYn,
                                                                strHoadonNo, strInvoiceNo, strDescription, dblTotSellingPrice, strInsCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region InsertTempHoadonForPark : 주차장용 임시화돈 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForPark
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-09-07
         * 용       도 : 주차장용 임시화돈 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonForPark : 주차장용 임시화돈 등록
        /// </summary>
        /// <param name="strLangCd"></param>
        /// <param name="strUserSeq"></param>
        /// <param name="strUserNm"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strAddr"></param>
        /// <param name="strDetAddr"></param>
        /// <param name="strUserTaxCd"></param>
        /// <param name="strModPaymentDt"></param>
        /// <param name="strCompNm"></param>
        /// <param name="strInvoiceContYn"></param>
        /// <param name="strHoadonNo"></param>
        /// <param name="strInvoiceNo"></param>
        /// <param name="strDescription"></param>
        /// <param name="dblTotSellingPrice"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static DataTable InsertTempHoadonForPark(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt,
                                                        int intPaymentSeq, int intPaymentDetSeq, string strAddr, string strDetAddr, string strUserTaxCd,
                                                        string strModPaymentDt, string strCompNm, string strInvoiceContYn, string strHoadonNo,
                                                        string strInvoiceNo, string strDescription, double dblTotSellingPrice,
                                                        string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.InsertTempHoadonForPark(strLangCd, strUserSeq, strUserNm, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strAddr,
                                                             strDetAddr, strUserTaxCd, strModPaymentDt, strCompNm, strInvoiceContYn,
                                                             strHoadonNo, strInvoiceNo, strDescription, dblTotSellingPrice, strInsCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region InsertTempHoadonForEachAPTRetail : 아파트 리테일 임시화돈 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForEachAPTRetail
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-09-07
         * 용       도 : 아파트 리테일 임시화돈 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strLangCd"></param>
        /// <param name="strUserSeq"></param>
        /// <param name="strUserNm"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="intPaymentDetSeq"></param>
        /// <param name="strAddr"></param>
        /// <param name="strDetAddr"></param>
        /// <param name="strUserTaxCd"></param>
        /// <param name="strModPaymentDt"></param>
        /// <param name="strCompNm"></param>
        /// <param name="strInvoiceContYn"></param>
        /// <param name="strHoadonNo"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static DataTable InsertTempHoadonForEachAPTRetail(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt,
                                                                 int intPaymentSeq, int intPaymentDetSeq, string strAddr, string strDetAddr, string strUserTaxCd,
                                                                 string strModPaymentDt, string strCompNm, string strInvoiceContYn, string strHoadonNo,
                                                                 string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.InsertTempHoadonForEachAPTRetail(strLangCd, strUserSeq, strUserNm, strPaymentDt, intPaymentSeq, intPaymentDetSeq, strAddr,
                                                                      strDetAddr, strUserTaxCd, strModPaymentDt, strCompNm, strInvoiceContYn,
                                                                      strHoadonNo, strInsCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region InsertTempHoadonForEachTower : 타워용 임시화돈 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForEachTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-09-07
         * 용       도 : 타워용 임시화돈 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strLangCd"></param>
        /// <param name="strUserSeq"></param>
        /// <param name="strUserNm"></param>
        /// <param name="strPaymentDt"></param>
        /// <param name="intPaymentSeq"></param>
        /// <param name="strAddr"></param>
        /// <param name="strDetAddr"></param>
        /// <param name="strUserTaxCd"></param>
        /// <param name="strModPaymentDt"></param>
        /// <param name="strCompNm"></param>
        /// <param name="strInvoiceContYn"></param>
        /// <param name="strHoadonNo"></param>
        /// <param name="strInsCompCd"></param>
        /// <param name="strInsMemNo"></param>
        /// <param name="strInsMemIP"></param>
        /// <returns></returns>
        public static DataTable InsertTempHoadonForEachTower(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt,
                                                             int intPaymentSeq, string strAddr, string strDetAddr, string strUserTaxCd,
                                                             string strModPaymentDt, string strCompNm, string strInvoiceContYn, string strHoadonNo,
                                                             string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.InsertTempHoadonForEachTower(strLangCd, strUserSeq, strUserNm, strPaymentDt, intPaymentSeq, strAddr,
                                                                  strDetAddr, strUserTaxCd, strModPaymentDt, strCompNm, strInvoiceContYn,
                                                                  strHoadonNo, strInsCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region InsertTempHoadonForTemp : 타워용 임시화돈 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForTemp
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-17
         * 용       도 : 타워용 임시화돈 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonForTemp : 타워용 임시화돈 등록
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <param name="intTempDocSeq">임시문서순번</param>
        /// <param name="strOldInvoiceNo">구세금계산서번호</param>
        /// <param name="strNewInvoiceNo">새세금계산서번호</param>
        /// <param name="intPrintDetSeq">출력순번</param>
        /// <param name="strDescription">제목</param>
        /// <param name="dblTotSellingPrice">총판매가</param>
        /// <returns></returns>
        public static DataTable InsertTempHoadonForTemp(string strTempDocNo, int intTempDocSeq, string strOldInvoiceNo, string strNewInvoiceNo, int intPrintDetSeq,
                                                        string strDescription, double dblTotSellingPrice)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.InsertTempHoadonForTemp(strTempDocNo, intTempDocSeq, strOldInvoiceNo, strNewInvoiceNo, intPrintDetSeq, strDescription, dblTotSellingPrice);

            return dtReturn;
        }

        #endregion

        #region InsertTempHoadonForTemp : 타워용 임시화돈 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForTemp
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-11-02
         * 용       도 : 타워용 임시화돈 등록
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonForTemp : 타워용 임시화돈 등록
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <param name="intTempDocSeq">임시문서순번</param>
        /// <param name="strOldInvoiceNo">구세금계산서번호</param>
        /// <param name="strNewInvoiceNo">새세금계산서번호</param>
        /// <param name="intPrintDetSeq">출력순번/param>
        /// <param name="strDescription">제목</param>
        /// <param name="strInvoiceDt">세금계산서발행일</param>
        /// <param name="dblTotSellingPrice">총판매가</param>
        /// <returns></returns>
        public static DataTable InsertTempHoadonForTemp(string strTempDocNo, int intTempDocSeq, string strOldInvoiceNo, string strNewInvoiceNo, int intPrintDetSeq,
                                                        string strDescription, string strInvoiceDt, double dblTotSellingPrice)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.InsertTempHoadonForTemp(strTempDocNo, intTempDocSeq, strOldInvoiceNo, strNewInvoiceNo, intPrintDetSeq, strDescription, 
                                                             strInvoiceDt, dblTotSellingPrice);

            return dtReturn;
        }

        #endregion

        #region InsertHoadonInfoApt

        /**********************************************************************************************
         * Mehtod   명 : InsertHoadonInfoApt
         * 개   발  자 : phuongtv
         * 생   성  일 : 2012-10-22
         * 용       도 : 타워용 화돈 확정테이블에 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertHoadonInfoApt : 타워용 화돈 확정테이블에 등록
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <returns>object[]</returns>
        public static object[] InsertHoadonInfoApt(string strUserSeq, string SetSeq, string InsCompCd, string InsMemNo, string InsMemIP, string PayDt)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.InsertHoadonInfoApt(strUserSeq, SetSeq, InsCompCd, InsMemNo, InsMemIP, PayDt);

            return objReturn;
        }

        public static object[] ReplaceHoadonInfoApt(string strInvoiceNo, double dbNetAmt, double dbVatAmt, double GrandAmt, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.ReplaceHoadonInfoApt(strInvoiceNo, dbNetAmt, dbVatAmt, GrandAmt, InsCompCd, InsMemNo, InsMemIP);

            return objReturn;
        }

        #endregion

        #region InsertTempHoadonForConfirm : 타워용 화돈 확정테이블에 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForConfirm
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-22
         * 용       도 : 타워용 화돈 확정테이블에 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonForConfirm : 타워용 화돈 확정테이블에 등록
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <returns>object[]</returns>
        public static object[] InsertTempHoadonForConfirm(string strTempDocNo,string billCD)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.InsertTempHoadonForConfirm(strTempDocNo, billCD);

            return objReturn;
        }

        //BaoTv
        public static object[] InsertHoadonInfo(string strRentCd,string strTempDocNo,string requestDt, string insMemNo, string insMemIp)
        {
            var objReturn = InvoiceMngDao.InsertHoadonInfo(strRentCd,strTempDocNo,requestDt,insMemNo,insMemIp);

            return objReturn;
        }

        #endregion

        #region InsertTempHoadonForConfirmAPT : 아파트 화돈 확정테이블에 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForConfirmAPT
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-22
         * 용       도 : 아파트 화돈 확정테이블에 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonForConfirmAPT : 아파트 화돈 확정테이블에 등록
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <returns>object[]</returns>
        public static object[] InsertTempHoadonForConfirmAPT(string strTempDocNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.InsertTempHoadonForConfirmAPT(strTempDocNo);

            return objReturn;
        }

        #endregion

        #region InsertTempHoadonForConfirmTower : 아파트 주차비용 화돈 확정테이블에 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForConfirmTower
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-22
         * 용       도 : 아파트 주차비용 화돈 확정테이블에 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonForConfirmTower : 아파트 주차비용 화돈 확정테이블에 등록
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <returns>object[]</returns>
        public static object[] InsertTempHoadonForConfirmTower(string strTempDocNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.InsertTempHoadonForConfirmTower(strTempDocNo);

            return objReturn;
        }

        #endregion

        #region InsertTempHoadonBindForKeangNam : 경남 비나용 세금계산서 묶기

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonBindForKeangNam
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-11-20
         * 용       도 : 경남 비나용 세금계산서 묶기
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonBindForKeangNam : 경남 비나용 세금계산서 묶기
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <returns>object[]</returns>
        public static object[] InsertTempHoadonBindForKeangNam(string strTempDocNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.InsertTempHoadonBindForKeangNam(strTempDocNo);

            return objReturn;
        }

        #endregion

        #region UPDATE HOADONPRINTINFO
        public static DataTable HoadonPrintInfo(string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.UpdateHoadonPrintInfo(strCompNo, strInsMemNo, strInsIP);

            return dtReturn;
        }
        #endregion

        #region UPDATE VATINVOICE_S00
        public static DataTable VATInvoice()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.UpdateVATInvoice();

            return dtReturn;
        }
        #endregion

        #region UPDATE HOADONPREVIEWINFO_S00
        public static DataTable HoadonPreviewInfo(string strUserSeq, string strPaymentDt, string strPaymentSeq, string strUserTaxCd, string strModPaymentDt, string strInvoiceContYn)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = InvoiceMngDao.UpdateHoadonPreviewInfo(strUserSeq, strPaymentDt, strPaymentSeq, strUserTaxCd, strModPaymentDt, strInvoiceContYn);

            return dtReturn;
        }
        #endregion

        #region UpdateInvoiceConfirm : 세금계산서 조회수 증가

        /**********************************************************************************************
         * Mehtod   명 : UpdateInvoiceConfirm
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-23
         * 용       도 : 세금계산서 조회수 증가
         * Input    값 : UpdateInvoiceConfirm(새세금계산서 번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateInvoiceConfirm : 세금계산서 조회수 증가
        /// </summary>
        /// <param name="strNewInvoiceNo">새세금계산서 번호</param>
        /// <returns>object[]</returns>
        public static object[] UpdateInvoiceConfirm(string strNewInvoiceNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.UpdateInvoiceConfirm(strNewInvoiceNo);

            return objReturn;
        }

        #endregion


        #region UpdateInvoiceConfirmForAPT : 아파트용 세금계산서 조회수 증가

        /**********************************************************************************************
         * Mehtod   명 : UpdateInvoiceConfirmForAPT
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-30
         * 용       도 : 아파트용 세금계산서 조회수 증가
         * Input    값 : UpdateInvoiceConfirmForAPT(새세금계산서 번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateInvoiceConfirmForAPT : 세금계산서 조회수 증가
        /// </summary>
        /// <param name="strNewInvoiceNo">새세금계산서 번호</param>
        /// <returns>object[]</returns>
        public static object[] UpdateInvoiceConfirmForAPT(string strNewInvoiceNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.UpdateInvoiceConfirmForAPT(strNewInvoiceNo);

            return objReturn;
        }

        #endregion

        #region UpdateInvoiceConfirmForParking : 주차장용 세금계산서 조회수 증가

        /**********************************************************************************************
         * Mehtod   명 : UpdateInvoiceConfirmForParking
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-30
         * 용       도 : 주차장용 세금계산서 조회수 증가
         * Input    값 : UpdateInvoiceConfirmForParking(새세금계산서 번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateInvoiceConfirmForParking : 세금계산서 조회수 증가
        /// </summary>
        /// <param name="strNewInvoiceNo">새세금계산서 번호</param>
        /// <returns>object[]</returns>
        public static object[] UpdateInvoiceConfirmForParking(string strNewInvoiceNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.UpdateInvoiceConfirmForParking(strNewInvoiceNo);

            return objReturn;
        }

        #endregion

        #region UpdateInvoiceConfirm : 화돈 재발행용 취소

        /**********************************************************************************************
         * Mehtod   명 : UpdateInvoiceConfirm
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-23
         * 용       도 : 화돈 재발행용 취소
         * Input    값 : UpdateInvoiceConfirm(새세금계산서 번호, 재생성용 세금계산서번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateInvoiceConfirm : 화돈 재발행용 취소
        /// </summary>
        /// <param name="strNewInvoiceNo">새세금계산서 번호</param>
        /// <param name="strReInvoiceNo">재생성용 세금계산서번호</param>
        /// <returns>object[]</returns>
        public static object[] UpdateInvoiceConfirm(string strNewInvoiceNo, string strReInvoiceNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.UpdateInvoiceConfirm(strNewInvoiceNo, strReInvoiceNo);

            return objReturn;
        }

        #endregion

        #region UpdateInvoiceConfirmForParking : 주차장 화돈 재발행용 취소

        /**********************************************************************************************
         * Mehtod   명 : UpdateInvoiceConfirmForParking
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-30
         * 용       도 : 주차장 화돈 재발행용 취소
         * Input    값 : UpdateInvoiceConfirmForParking(새세금계산서 번호, 재생성용 세금계산서번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateInvoiceConfirmForParking : 주차장 화돈 재발행용 취소
        /// </summary>
        /// <param name="strNewInvoiceNo">새세금계산서 번호</param>
        /// <param name="strReInvoiceNo">재생성용 세금계산서번호</param>
        /// <returns>object[]</returns>
        public static object[] UpdateInvoiceConfirmForParking(string strNewInvoiceNo, string strReInvoiceNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.UpdateInvoiceConfirmForParking(strNewInvoiceNo, strReInvoiceNo);

            return objReturn;
        }

        #endregion

        #region UpdateInvoiceConfirmForAPT : 체스넛 화돈 재발행용 취소

        /**********************************************************************************************
         * Mehtod   명 : UpdateInvoiceConfirmForAPT
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-30
         * 용       도 : 체스넛 화돈 재발행용 취소
         * Input    값 : UpdateInvoiceConfirmForAPT(새세금계산서 번호, 재생성용 세금계산서번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateInvoiceConfirmForAPT : 체스넛 화돈 재발행용 취소
        /// </summary>
        /// <param name="strNewInvoiceNo">새세금계산서 번호</param>
        /// <param name="strReInvoiceNo">재생성용 세금계산서번호</param>
        /// <returns>object[]</returns>
        public static object[] UpdateInvoiceConfirmForAPT(string strNewInvoiceNo, string strReInvoiceNo)
        {
            object[] objReturn = new object[2];

            objReturn = InvoiceMngDao.UpdateInvoiceConfirmForAPT(strNewInvoiceNo, strReInvoiceNo);

            return objReturn;
        }

        #endregion
    }
}
