using System.Data;
using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Settlement.Dac
{
    public class InvoiceMngDao
    {
        #region Select HoadonListInfo

        public static DataTable SelectHoadonListInfo(string strRentCd, string strRoomNo, string strItemCd, string strSvcYear, string strSvcMM, string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[6];

            objParam[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParam[1] = TextLib.MakeNullToEmpty(strRoomNo);
            objParam[2] = TextLib.MakeNullToEmpty(strItemCd);
            objParam[3] = TextLib.MakeNullToEmpty(strSvcYear);
            objParam[4] = TextLib.MakeNullToEmpty(strSvcMM);
            objParam[5] = TextLib.MakeNullToEmpty(strLangCd);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_RENTALMNGFEE_S01", objParam);

            return dtReturn;
        }

        public static DataTable SelectHoadonListMaster(string strRentCd, string strRoomNo, string strItemCd, string strSvcYear, string strSvcMM, string strLangCd, string invoiceYN)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[7];

            objParam[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParam[1] = TextLib.MakeNullToEmpty(strRoomNo);
            objParam[2] = TextLib.MakeNullToEmpty(strItemCd);
            objParam[3] = TextLib.MakeNullToEmpty(strSvcYear);
            objParam[4] = TextLib.MakeNullToEmpty(strSvcMM);
            objParam[5] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[6] = TextLib.MakeNullToEmpty(invoiceYN);

            dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_INVOICE_LIST", objParam);

            return dtReturn;
        }

        public static DataTable SelectHoadonListMaster1(string strRentCd, string strRoomNo, string strInvoiceNo, string strItemCd, string strLangCd, string invoiceYN, string strStartDt, string strEndDt,string compNm)
        {
            var objParam = new object[9];

            objParam[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParam[1] = TextLib.MakeNullToEmpty(strRoomNo);
            objParam[2] = TextLib.MakeNullToEmpty(strInvoiceNo);
            objParam[3] = TextLib.MakeNullToEmpty(strItemCd);           
            objParam[4] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[5] = TextLib.MakeNullToEmpty(invoiceYN);
            objParam[6] = TextLib.MakeNullToEmpty(strStartDt);
            objParam[7] = TextLib.MakeNullToEmpty(strEndDt);
            objParam[8] = TextLib.MakeNullToEmpty(compNm);

            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_INVOICE_LIST1", objParam);

            return dtReturn;
        }

        //BaoTv
        public static DataTable SelectHoadonListExcel(string strRentCd, string strRoomNo, string strInvoiceNo, string strItemCd, string strLangCd, string invoiceYN, string strStartDt, string strEndDt, string compNm)
        {
            var objParam = new object[9];

            objParam[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParam[1] = TextLib.MakeNullToEmpty(strRoomNo);
            objParam[2] = TextLib.MakeNullToEmpty(strInvoiceNo);
            objParam[3] = TextLib.MakeNullToEmpty(strItemCd);
            objParam[4] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[5] = TextLib.MakeNullToEmpty(invoiceYN);
            objParam[6] = TextLib.MakeNullToEmpty(strStartDt);
            objParam[7] = TextLib.MakeNullToEmpty(strEndDt);
            objParam[8] = TextLib.MakeNullToEmpty(compNm);
            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_INVOICE_LIST_EXCEL", objParam);

            return dtReturn;
        }

        public static DataTable SelectHoadonListDetail(string refSerialNo,string billCD,string empty)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(refSerialNo);
            objParam[0] = TextLib.MakeNullToEmpty(billCD);

            dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_INVOICE_DETAIL_LIST", objParam);

            return dtReturn;
        }

        public static DataTable SelectHoadonListDetail(string refSerialNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(refSerialNo);
            

            dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_INVOICE_DETAIL_LIST", objParam);

            return dtReturn;
        }

        public static DataTable SelectHoadonListDetail(string refSerialNo,string tmpInvoiceNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refSerialNo);
            objParam[1] = TextLib.MakeNullToEmpty(tmpInvoiceNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_INVOICE_DETAIL_LIST1", objParam);

            return dtReturn;
        }

        public static DataTable SelectReprintHoadon(string refPrintBundleNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(refPrintBundleNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_RPT_SELECT_INVOICE_REPRINT_LIST", objParam);

            return dtReturn;
        }

        public static DataTable SelectReprintHoadon(string refPrintBundleNo,string invoiceNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refPrintBundleNo);
            objParam[1] = TextLib.MakeNullToEmpty(invoiceNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_RPT_SELECT_INVOICE_REPRINT_LIST", objParam);

            return dtReturn;
        }

        public static DataTable SelectReprintInvoiceNo(string compNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(compNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_INVOICENO_LIST", objParam);

            return dtReturn;
        }

        public static DataTable SelectBillCode(string typeCode, string langCode, string compNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[3];

            objParam[0] = TextLib.MakeNullToEmpty(typeCode);
            objParam[1] = TextLib.MakeNullToEmpty(langCode);
            objParam[2] = TextLib.MakeNullToEmpty(compNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_COMM_SELECT_COMM_CODE_LIST", objParam);

            return dtReturn;
        }

        #endregion

        #region Select KeangNam Hoadon List for Cancel

        public static DataTable SelectKNHoadonListCanCel(string strRentCd, string strRoomNo, string strInvoiceNo, string strItemCd, string strLangCd, string strBillTy, string strStartDt, string strEndDt, string compNm)
        {
            var objParam = new object[9];

            objParam[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParam[1] = TextLib.MakeNullToEmpty(strRoomNo);
            objParam[2] = TextLib.MakeNullToEmpty(strInvoiceNo);
            objParam[3] = TextLib.MakeNullToEmpty(strItemCd);
            objParam[4] = TextLib.MakeNullToEmpty(strLangCd);
            objParam[5] = TextLib.MakeNullToEmpty(strBillTy);
            objParam[6] = TextLib.MakeNullToEmpty(strStartDt);
            objParam[7] = TextLib.MakeNullToEmpty(strEndDt);
            objParam[8] = TextLib.MakeNullToEmpty(compNm);

            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_SELECT_KN_CANCELINVOICE_LIST_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region Select Exchange Rate by Payment Date

        public static DataTable SelectExRateByPayDt(string strRentCd, string strPayDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParam[1] = TextLib.MakeNullToEmpty(strPayDt);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_EXRATE_BY_PAYDT", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectExistLineForInvoiceNo : 중복된 세금계산서 번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistLineForInvoiceNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-23
         * 용       도 : 중복된 세금계산서 번호 조회
         * Input    값 : SelectExistLineForInvoiceNo(새세금계산서 번호)
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
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(strNewInvoiceNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_INVOICEFORCONFIRM_S01", objParam);

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
        /// <param name="printBundleNo"></param>
        /// <returns></returns>
        public static DataTable UpdatingInvoiceNoForHoadon(string contractType, string refSerialNo, string monthAmtNo, string rentCd, string printBundleNo,string billcd)
        {
            var objParam = new object[6];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);
            objParam[1] = TextLib.MakeNullToEmpty(refSerialNo);
            objParam[2] = TextLib.MakeNullToEmpty(printBundleNo);
            objParam[3] = double.Parse(monthAmtNo.Replace(".", ""));
            objParam[4] = TextLib.MakeNullToEmpty(contractType);
            objParam[5] = TextLib.MakeNullToEmpty(billcd);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_HOADON_INFO_U00", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingInvoiceSpecialIssueDt(string printBundleNo, string issueDt)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(printBundleNo);
            objParam[1] = TextLib.MakeNullToEmpty(issueDt);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_HOADON_SPECIAL_ISSUEDATE_INFO_U00", objParam);

            return dtReturn;
        }

        #endregion
        





















        #region Reset Invoice Bundle Seq No : 인보이스 묶음 프린터 번호 초기화

        /**********************************************************************************************
         * Mehtod   명 : UpdatingBundleSeqNoForReset
         * 개   발  자 : Jeong Seung Hwa
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
            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_UPDATE_RESET_PRINT_NO");

            return dtReturn;
        }

        #endregion

        #region Reset Ref_PrintNo For APT Management

        /**********************************************************************************************
         * Mehtod   명 : UpdatingRefPrintNoAPTForReset
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-05-06
         * 용       도 : Updating Ref_PrintNo for reset 
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdatingInvoiceNoForHoadon : Updating Innvoice No for Hoadon to print
        /// </summary>
        /// <returns></returns>
        public static DataTable UpdatingRefPrintNoAPTForReset()
        {
            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_UPDATE_RESET_REFPRINT_APT");

            return dtReturn;
        }
        public static DataTable UpdatingRefPrintNoHoaDonAPTForReset()
        {
            var dtReturn = SPExecute.ExecReturnSingle("KN_SCR_UPDATE_RESET_REFPRINT_HOADON_APT");

            return dtReturn;
        }

        #endregion

        #region Reset Ref_PrintNo For HoadonParkingAPT

        /**********************************************************************************************
         * Mehtod   명 : UpdatingRefPrintNoAPTParkingForReset
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-06-15
         * 용       도 : Updating Ref_PrintNo for reset 
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

            dtReturn = SPExecute.ExecReturnSingle("KN_SCR_UPDATE_RESET_REFPRINT_APT_PARKING_U00");

            return dtReturn;
        }

        #endregion

        
        #region Update Ref_PrintNo For Print APT Management

        /**********************************************************************************************
         * Mehtod   명 : UpdatingRefPrintNoForAPT
         * 개   발  자 : Jeong Seung Hwa
         * 생   성  일 : 2013-03-05
         * 용       도 : Updating Invoice no for print each hoadon 
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdatingInvoiceNoForHoadon : 
        /// </summary>
        /// <param name="contractType"></param>
        /// <param name="refSerialNo"></param>
        /// <param name="monthAmtNo"></param>
        /// <param name="rentCd"></param>
        /// <param name="printBundleNo"></param>
        /// <returns></returns>
        public static DataTable UpdatingRefPrintNoForAPT(string refSeq, string rentCd, string refPrintNo, string payDt, int seq)
        {
            var dtReturn = new DataTable();
            var objParam = new object[5];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);
            objParam[1] = TextLib.MakeNullToEmpty(refSeq);
            objParam[2] = TextLib.MakeNullToEmpty(refPrintNo);
            objParam[3] = TextLib.MakeNullToEmpty(payDt);
            objParam[4] = seq;


            //dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_PAYMENTLIST_PRINT_U00", objParam);
            dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_PAYMENTLIST_PRINT_U01", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingRefPrintNoForHoaDonAPT(string refSeq,string refPrintNo)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refSeq);
            objParam[1] = TextLib.MakeNullToEmpty(refPrintNo);
            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_HOADON_APT_PRINT_M00", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingMergeAptParkingFee(string refSeq, string rentCd, string refPrintNo, string payDt)
        {
            var objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);
            objParam[1] = TextLib.MakeNullToEmpty(refSeq);
            objParam[2] = TextLib.MakeNullToEmpty(refPrintNo);
            objParam[3] = TextLib.MakeNullToEmpty(payDt);


            DataTable dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_APTPKF_PRINT_U00", objParam);

            return dtReturn;
        }

        //--------Update '---------------------
        public static DataTable UpdatingPrintYNForAPT(string strPSeq, int intSeq)
        {
            var dtReturn = new DataTable();
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(strPSeq);
            objParam[1] = intSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_PAYMENTLIST_PRINTYN_U00", objParam);

            return dtReturn;
        }


        //--------Update Bao'---------------------
        public static DataTable UpdatingCancelInvoiceForApt(string strPSeq, int intSeq, double netAmt, double vatAmt)
        {
            var objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(strPSeq);
            objParam[1] = intSeq;
            objParam[2] = netAmt;
            objParam[3] = vatAmt;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_CANCELINVOICE_APT_U00", objParam);

            return dtReturn;
        }
        //---------UPdate Invoice No For HoadonInfoApt--------------

        public static DataTable UpdatingInvoiceNoHoadonInfoApt(string printSeq, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(printSeq);            
            objParam[1] = TextLib.MakeNullToEmpty(InsCompCd);
            objParam[2] = TextLib.MakeNullToEmpty(InsMemNo);
            objParam[3] = TextLib.MakeNullToEmpty(InsMemIP);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_INVOICENO_HOADONINFOAPT_U00", objParam);

            return dtReturn;
        }


        public static DataTable UpdatingInvoiceAdjNoHoadonInfoAptNew(string printSeq, string oldInvoiceNo, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var objParam = new object[5];

            objParam[0] = TextLib.MakeNullToEmpty(printSeq);
            objParam[1] = TextLib.MakeNullToEmpty(oldInvoiceNo);
            objParam[2] = TextLib.MakeNullToEmpty(InsCompCd);
            objParam[3] = TextLib.MakeNullToEmpty(InsMemNo);
            objParam[4] = TextLib.MakeNullToEmpty(InsMemIP);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_INVOICENO_HOADONINFOAPT_U01", objParam);

            return dtReturn;
        }

        public static DataTable InsertInvoiceIncreaseApt(string refSeq, double dbAmount, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var objParam = new object[5];

            objParam[0] = TextLib.MakeNullToEmpty(refSeq);
            objParam[1] = TextLib.MakeNullToEmpty(InsCompCd);
            objParam[2] = TextLib.MakeNullToEmpty(InsMemNo);
            objParam[3] = TextLib.MakeNullToEmpty(InsMemIP);
            objParam[4] = dbAmount;

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_INSERT_INCREASE_INVOICE_HOADONAPT_M00", objParam);

            return dtReturn;
        }

        //-------Update InvoiceNo For HoadonInfoApt(Parking Fee)

        public static DataTable UpdatingInvoiceNoHoadonAptPKF(string printSeq, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            var objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(printSeq);
            objParam[1] = TextLib.MakeNullToEmpty(InsCompCd);
            objParam[2] = TextLib.MakeNullToEmpty(InsMemNo);
            objParam[3] = TextLib.MakeNullToEmpty(InsMemIP);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_INVOICENO_HOADON_APTPKF_U00", objParam);

            return dtReturn;
        }

        //--------------------Update InvoiceNo for HoaDonParkingAPT From HoaDonParkingAPTTran---------------

        public static DataTable UpdatingInvoiceNoHoadonAptPKF_Trans(string printSeq, string InsCompCd, string InsMemNo, string InsMemIP, string PrintDt)
        {
            var objParam = new object[5];

            objParam[0] = TextLib.MakeNullToEmpty(printSeq);
            objParam[1] = TextLib.MakeNullToEmpty(InsCompCd);
            objParam[2] = TextLib.MakeNullToEmpty(InsMemNo);
            objParam[3] = TextLib.MakeNullToEmpty(InsMemIP);
            objParam[4] = TextLib.MakeNullToEmpty(PrintDt);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_HOADON_APTPKF_TRAN_U00", objParam);

            return dtReturn;
        }


        public static DataTable InsertCancelInvoiceHoadonInfoApt(string printNo, string invoiceNo, string reson, string insCompCd, string insMemNo, string insMemIP)
        {
            var objParam = new object[6];

            objParam[0] = TextLib.MakeNullToEmpty(printNo);
            objParam[1] = TextLib.MakeNullToEmpty(invoiceNo);
            objParam[2] = TextLib.MakeNullToEmpty(reson);
            objParam[3] = TextLib.MakeNullToEmpty(insCompCd);
            objParam[4] = TextLib.MakeNullToEmpty(insMemNo);
            objParam[5] = TextLib.MakeNullToEmpty(insMemIP);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_INSERT_INVOICE_CANCEL_I00", objParam);

            return dtReturn;
        }

        public static DataTable InsertMergeInvoiceHoadonInfoApt(string invoiceNo, string insCompCd, string insMemNo, string insMemIP)
        {
            var objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(invoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(insCompCd);
            objParam[2] = TextLib.MakeNullToEmpty(insMemNo);
            objParam[3] = TextLib.MakeNullToEmpty(insMemIP);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_INSERT_INVOICE_MERGE_I00", objParam);

            return dtReturn;
        }

        public static DataTable InsertMergeInvoiceA0000HoadonInfoApt(string invoiceNo, string insCompCd, string insMemNo, string insMemIP)
        {
            var objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(invoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(insCompCd);
            objParam[2] = TextLib.MakeNullToEmpty(insMemNo);
            objParam[3] = TextLib.MakeNullToEmpty(insMemIP);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_INSERT_INVOICE_A0000_MERGE_I00", objParam);

            return dtReturn;
        }

        public static DataTable InsertMergeInvoiceAptPKF(string invoiceNo, string insCompCd, string insMemNo, string insMemIP, string PrintDt)
        {
            var objParam = new object[5];

            objParam[0] = TextLib.MakeNullToEmpty(invoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(insCompCd);
            objParam[2] = TextLib.MakeNullToEmpty(insMemNo);
            objParam[3] = TextLib.MakeNullToEmpty(insMemIP);
            objParam[4] = TextLib.MakeNullToEmpty(PrintDt);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_INSERT_INVOICE_APTPKF_MERGE_I00", objParam);

            return dtReturn;
        }

        public static DataTable InsertAdjustInvoiceHoadonInfoApt(string invoiceNo, string insCompCd, string insMemNo, string insMemIP)
        {
            var objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(invoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(insCompCd);
            objParam[2] = TextLib.MakeNullToEmpty(insMemNo);
            objParam[3] = TextLib.MakeNullToEmpty(insMemIP);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_INSERT_INVOICE_ADJUST_I00", objParam);

            return dtReturn;
        }


        public static DataTable InsertTempInvoiceApt(string invoiceNo)
        {
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(invoiceNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_INSERT_INVOICEAPT_TEMP_M001", objParam);

            return dtReturn;
        }

        #endregion


        #region Update Ref_PrintNo For HoaDonParkingAPT -- phuongtv

        public static DataTable UpdatingRefPrintNoForHoaDonParkingAPT(string refSeq, string rentCd, string refPrintNo, string payDt, int seq)
        {
            var dtReturn = new DataTable();
            var objParam = new object[5];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);
            objParam[1] = TextLib.MakeNullToEmpty(refSeq);
            objParam[2] = TextLib.MakeNullToEmpty(refPrintNo);
            objParam[3] = TextLib.MakeNullToEmpty(payDt);
            objParam[4] = seq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_HoaDonParkingAPT_REFPRINT_U00", objParam);

            return dtReturn;
        }

        #endregion


        #region SelectExistLineForAPTInvoiceNo : 아파트용 중복된 세금계산서 번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExistLineForAPTInvoiceNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-23
         * 용       도 : 아파트용 중복된 세금계산서 번호 조회
         * Input    값 : SelectExistLineForAPTInvoiceNo(새세금계산서 번호)
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

            objParam[0] = TextLib.MakeNullToEmpty(strNewInvoiceNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_INVOICEFORCONFIRM_S05", objParam);

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
        public static DataTable SelectInvoiceReprintList(string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
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

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_INVOICEFORCONFIRM_S00", objParams);

            return dtReturn;
        }

        //Bao-TV
        public static DataTable SelectInvoiceReprintExcel(string strRentCd, string strRoomNo, string strYear, string strMonth, string strStartDt, string strEndDt,
                                                         string strItemCd, string strUserTaxCd, string strRssNo, string strLangCd)
        {
            var objParams = new object[10];

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

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_INVOICEFORCONFIRM_S06", objParams);

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

            object[] objParams = new object[13];

            objParams[0] = strCompNo;
            objParams[1] = strRentCd;
            objParams[2] = strRoomNo;
            objParams[3] = strYear;
            objParams[4] = strMonth;
            objParams[5] = strStartDt.Replace("-", "");
            objParams[6] = strEndDt.Replace("-", "");
            objParams[7] = strItemCd;
            objParams[8] = strUserTaxCd;
            objParams[9] = strRssNo;
            objParams[10] = strLangCd;
            objParams[11] = intStartFloor;
            objParams[12] = intEndFloor;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_INVOICEFORCONFIRM_S04", objParams);

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
        /// SelectMaxInvoiceNo : 세금계산서 마지막번호 조회
        /// </summary>
        /// <returns>DataTable</returns>
        public static DataTable SelectMaxInvoiceNo(string compNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = compNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_NEXTINVOICE_NO", objParams);

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



            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_SELECT_INVOICEFORCONFIRM_S03");

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
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strSettleCd">수납문서코드</param>
        /// <param name="strItemCd">아이템코드</param>
        /// <param name="strAccCd">회계코드</param>
        /// <returns>object[]</returns>
        public static object[] InsertHoadonAddon(string strCompCd, string strMemNo, string strMemIp, string strText)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];
            
            objParams[0] = strCompCd;
            objParams[1] = strMemNo;
            objParams[2] = strMemIp;
            objParams[3] = strText;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_HOADONADDON_M00", objParams);

            return objReturn;
        }

        #endregion

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
        public static DataTable InsertTempHoadonForEachAPT(string strLangCd, string strUserSeq, string strUserNm, string strPaymentDt,
                                                           int intPaymentSeq, int intPaymentDetSeq, string strAddr, string strDetAddr, string strUserTaxCd,
                                                           string strModPaymentDt, string strCompNm, string strInvoiceContYn, string strHoadonNo,
                                                           string strInvoiceNo, string strDescription, double dblTotSellingPrice,
                                                           string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[19];
            
	        objParams[0] = strLangCd;
            objParams[1] = strUserSeq;
            objParams[2] = strUserNm;
            objParams[3] = strPaymentDt;
            objParams[4] = intPaymentSeq;
            objParams[5] = intPaymentDetSeq;
            objParams[6] = strAddr;
            objParams[7] = strDetAddr;
            objParams[8] = strUserTaxCd;
            objParams[9] = strModPaymentDt;
            objParams[10] = strCompNm;
            objParams[11] = strInvoiceContYn;
            objParams[12] = strHoadonNo;
            objParams[13] = strInvoiceNo;
            objParams[14] = strDescription;
            objParams[15] = dblTotSellingPrice;
            objParams[16] = strInsCompCd;
            objParams[17] = strInsMemNo;
            objParams[18] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_TEMPINVOICEINFO_M00", objParams);

            return dtReturn;
        }

        #endregion


        #region InsertTempHoadonForPark : 주차장용 임시화돈 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForPark
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-09-07
         * 용       도 : 아파트용 임시화돈 등록
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
            object[] objParams = new object[19];

            objParams[0] = strLangCd;
            objParams[1] = strUserSeq;
            objParams[2] = strUserNm;
            objParams[3] = strPaymentDt;
            objParams[4] = intPaymentSeq;
            objParams[5] = intPaymentDetSeq;
            objParams[6] = strAddr;
            objParams[7] = strDetAddr;
            objParams[8] = strUserTaxCd;
            objParams[9] = strModPaymentDt;
            objParams[10] = strCompNm;
            objParams[11] = strInvoiceContYn;
            objParams[12] = strHoadonNo;
            objParams[13] = strInvoiceNo;
            objParams[14] = strDescription;
            objParams[15] = dblTotSellingPrice;
            objParams[16] = strInsCompCd;
            objParams[17] = strInsMemNo;
            objParams[18] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_TEMPINVOICEINFO_M04", objParams);

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
            object[] objParams = new object[16];

            objParams[0] = strLangCd;
            objParams[1] = strUserSeq;
            objParams[2] = strUserNm;
            objParams[3] = strPaymentDt;
            objParams[4] = intPaymentSeq;
            objParams[5] = intPaymentDetSeq;
            objParams[6] = strAddr;
            objParams[7] = strDetAddr;
            objParams[8] = strUserTaxCd;
            objParams[9] = strModPaymentDt;
            objParams[10] = strCompNm;
            objParams[11] = strInvoiceContYn;
            objParams[12] = strHoadonNo;
            objParams[13] = strInsCompCd;
            objParams[14] = strInsMemNo;
            objParams[15] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_TEMPINVOICEINFO_M01", objParams);

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
            object[] objParams = new object[15];

            objParams[0] = strLangCd;
            objParams[1] = strUserSeq;
            objParams[2] = strUserNm;
            objParams[3] = strPaymentDt;
            objParams[4] = intPaymentSeq;
            objParams[5] = strAddr;
            objParams[6] = strDetAddr;
            objParams[7] = strUserTaxCd;
            objParams[8] = strModPaymentDt;
            objParams[9] = strCompNm;
            objParams[10] = strInvoiceContYn;
            objParams[11] = strHoadonNo;
            objParams[12] = strInsCompCd;
            objParams[13] = strInsMemNo;
            objParams[14] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_INSERT_TEMPINVOICEINFO_M02", objParams);

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
        /// <param name="intPrintDetSeq">출력순번/param>
        /// <param name="strDescription">제목</param>
        /// <param name="dblTotSellingPrice">총판매가</param>
        /// <returns></returns>
        public static DataTable InsertTempHoadonForTemp(string strTempDocNo, int intTempDocSeq, string strOldInvoiceNo, string strNewInvoiceNo, int intPrintDetSeq,
                                                        string strDescription, double dblTotSellingPrice)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[7];

            objParams[0] = strTempDocNo;
            objParams[1] = intTempDocSeq;
            objParams[2] = strOldInvoiceNo;
            objParams[3] = strNewInvoiceNo;
            objParams[4] = intPrintDetSeq;
            objParams[5] = strDescription;
            objParams[6] = dblTotSellingPrice;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_INVOICEFORTEMP_S00", objParams);

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
            object[] objParams = new object[8];

            objParams[0] = strTempDocNo;
            objParams[1] = intTempDocSeq;
            objParams[2] = strOldInvoiceNo;
            objParams[3] = strNewInvoiceNo;
            objParams[4] = intPrintDetSeq;
            objParams[5] = strDescription;
            objParams[6] = strInvoiceDt;
            objParams[7] = dblTotSellingPrice;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_SET_INSERT_INVOICEFORTEMP_S04", objParams);

            return dtReturn;
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
            object[] objParams = new object[2];

            objParams[0] = strTempDocNo;
            objParams[1] = billCD;
            
            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_INVOICEFORTEMP_S01", objParams);

            return objReturn;
        }

        public static object[] InsertHoadonInfo(string strRentCd, string strTempDocNo, string requestDt, string insMemNo, string insMemIp)
        {
            var objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strTempDocNo;
            objParams[2] = requestDt;
            objParams[3] = insMemNo;
            objParams[4] = insMemIp;
            var objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_INVOICE_M001", objParams);

            return objReturn;
        }

        #endregion

        #region InsertTempHoadonForConfirmAPT : 아파트 화돈 확정테이블에 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempHoadonForConfirmAPT
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-10-22
         * 용       도 : 아파트 주차비용 화돈 확정테이블에 등록
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertTempHoadonForConfirmAPT : 아파트 주차비용 화돈 확정테이블에 등록
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <returns>object[]</returns>
        public static object[] InsertTempHoadonForConfirmAPT(string strTempDocNo)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = strTempDocNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_INVOICEFORTEMP_S02", objParams);

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
            object[] objParams = new object[1];

            objParams[0] = strTempDocNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_INVOICEFORTEMP_S03", objParams);

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
            object[] objParams = new object[1];

            objParams[0] = strTempDocNo;

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_INSERT_INVOICEFORTEMP_S05", objParams);

            return objReturn;
        }

        #endregion

        #region InsertHoadonInfoApt

        /**********************************************************************************************
         * Mehtod   명 : InsertHoadonInfoApt
         * 개   발  자 : phuongtv
         * 생   성  일 : 2012-11-20
         * 용       도 : 경남 비나용 세금계산서 묶기
         * Input    값 : 
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertHoadonInfoApt : 경남 비나용 세금계산서 묶기
        /// </summary>
        /// <param name="strTempDocNo">임시문서번호</param>
        /// <returns>object[]</returns>
        public static object[] InsertHoadonInfoApt(string strUserSeq, string SetSeq, string InsCompCd, string InsMemNo, string InsMemIP, string PayDt)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strUserSeq;
            objParams[1] = SetSeq;
            objParams[2] = InsCompCd;
            objParams[3] = InsMemNo;
            objParams[4] = InsMemIP;
            objParams[5] = PayDt;

            objReturn = SPExecute.ExecReturnNo("KN_USP_APT_INSERT_HOADONINFOAPT_I00", objParams);

            return objReturn;
        }

        public static object[] ReplaceHoadonInfoApt(string strInvoiceNo, double dbNetAmt, double dbVatAmt, double GrandAmt, string InsCompCd, string InsMemNo, string InsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = strInvoiceNo;
            objParams[1] = dbNetAmt;
            objParams[2] = dbVatAmt;
            objParams[3] = GrandAmt;
            objParams[4] = InsCompCd;
            objParams[5] = InsMemNo;
            objParams[6] = InsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_APT_REPLACE_HOADONINFOAPT_I00", objParams);

            return objReturn;
        }

        #endregion

        #region Update HoadonListInfo

        public static object[] UpdateHoadonListInfo(string strInvoiceNo, string strSerialNo, string strPrintSeq, int intPrintDetSeq, string strSvcYear, string strSvcMM,
                                                    double dblExchageRate, double dblAmtViNo, string strDescription, string strUserNm, string strUserCd,
                                                    string strUserSeq, string strInsAddress, string strInsDetAddress, string strRentCd, string strFeeTy, string strInsRemark)
        {
            object[] objReturn = new object[2];
            object[] objParam = new object[17];

            objParam[0] = TextLib.MakeNullToEmpty(strInvoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(strSerialNo);
            objParam[2] = TextLib.MakeNullToEmpty(strPrintSeq);
            objParam[3] = intPrintDetSeq;
            objParam[4] = TextLib.MakeNullToEmpty(strSvcYear);
            objParam[5] = TextLib.MakeNullToEmpty(strSvcMM);
            objParam[6] = dblExchageRate;
            objParam[7] = dblAmtViNo;
            objParam[8] = TextLib.MakeNullToEmpty(strDescription);
            objParam[9] = TextLib.MakeNullToEmpty(strUserNm);
            objParam[10] = TextLib.MakeNullToEmpty(strUserCd);
            objParam[11] = TextLib.MakeNullToEmpty(strUserSeq);
            objParam[12] = TextLib.MakeNullToEmpty(strInsAddress);
            objParam[13] = TextLib.MakeNullToEmpty(strInsDetAddress);
            objParam[14] = TextLib.MakeNullToEmpty(strRentCd);
            objParam[15] = TextLib.MakeNullToEmpty(strFeeTy);
            objParam[16] = TextLib.MakeNullToEmpty(strInsRemark);

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_RENTALMNGFEE_M00", objParam);

            return objReturn;
        }

        public static object[] UpdateHoadonListNew(string strInvoiceNo, string strBillCd, string strSerialNo, string strSvcYear, string strSvcMM, string strUserNm, string strUserAddr, string strUserDetAddress, 
                                                    string strUserTaxCd, string strDescription, string strPaymentDt, string strSvcStartDt, string strSvcEndDt, double dbUnitPrice, double dbVatAmt, double dbDongToDollar,
                                                    double dbTotSellingPrice, string strIssuingDate, int intQty, string strRefSerialNo, string strRoomNo)
        {
            var objParam = new object[21];

            objParam[0] = TextLib.MakeNullToEmpty(strInvoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(strBillCd);
            objParam[2] = TextLib.MakeNullToEmpty(strSerialNo);            
            objParam[3] = TextLib.MakeNullToEmpty(strSvcYear);
            objParam[4] = TextLib.MakeNullToEmpty(strSvcMM);
            objParam[5] = TextLib.MakeNullToEmpty(strUserNm);
            objParam[6] = TextLib.MakeNullToEmpty(strUserAddr);
            objParam[7] = TextLib.MakeNullToEmpty(strUserDetAddress);
            objParam[8] = TextLib.MakeNullToEmpty(strUserTaxCd);
            objParam[9] = TextLib.MakeNullToEmpty(strDescription);
            objParam[10] = TextLib.MakeNullToEmpty(strPaymentDt);
            objParam[11] = TextLib.MakeNullToEmpty(strSvcStartDt);
            objParam[12] = TextLib.MakeNullToEmpty(strSvcEndDt);
            objParam[13] = dbUnitPrice;
            objParam[14] = dbVatAmt;
            objParam[15] = dbDongToDollar;
            objParam[16] = dbTotSellingPrice;
            objParam[17] = TextLib.MakeNullToEmpty(strIssuingDate);
            objParam[18] = intQty;
            objParam[19] = TextLib.MakeNullToEmpty(strRefSerialNo);
            objParam[20] = TextLib.MakeNullToEmpty(strRoomNo);

            var objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_INVOICE", objParam);

            return objReturn;
        }

        public static object[] CancelHoadon( string strRefSerialNo)
        {
            object[] objReturn = new object[2];
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(strRefSerialNo);

            objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_INVOICE_CANCEL", objParam);

            return objReturn;
        }
        public static object[] UpdatelHoadonReprintList(string strRentCd, string strRefSerialNo, string strRefPrintBundleNo)
        {
            object[] objReturn = new object[2];
            object[] objParam = new object[3];

            objParam[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParam[1] = TextLib.MakeNullToEmpty(strRefSerialNo);
            objParam[2] = TextLib.MakeNullToEmpty(strRefPrintBundleNo);

            objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_REPRINT_LIST", objParam);

            return objReturn;
        }

        public static object[] UpdatelHoadonReprintList1(string strRefPrintBundleNo)
        {
            var objParam = new object[1];
            objParam[0] = TextLib.MakeNullToEmpty(strRefPrintBundleNo);
            var objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_REPRINT_LIST_001", objParam);
            return objReturn;
        }

        //BaoTv
        public static object[] UpdatedAptDebitPrintNo(string strRefPrintBundleNo, string strFeeSeq)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(strRefPrintBundleNo);
            objParam[1] = int.Parse(strFeeSeq);

            var objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_DEBIT_PRINTNO_M00", objParam);

            return objReturn;
        }

        //BaoTv
        public static object[] UpdatedAptDebitPrintNoForMerge(string strRefPrintBundleNo, string strFeeSeq)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(strRefPrintBundleNo);
            objParam[1] = strFeeSeq;

            var objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_DEBIT_PRINTNO_MERGE_M00", objParam);

            return objReturn;
        }

        //BaoTv
        public static object[] UpdatedAptDebitPrinted(string strRefPrintBundleNo)
        {
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(strRefPrintBundleNo);

            var objReturn = SPExecute.ExecReturnNo("KN_SCR_UPDATE_DEBIT_PRINTNO_M01", objParam);

            return objReturn;
        }
        #endregion

        #region UPDATE HOADONPRINTINFO
        public static DataTable UpdateHoadonPrintInfo(string strCompNo, string strInsMemNo, string strInsIP)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[3];

            objParam[0] = strCompNo;
            objParam[1] = strInsMemNo;
            objParam[2] = strInsIP;
            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_HOADONPRINTINFO_M00", objParam);

            return dtReturn;
        }
        #endregion

        #region UPDATE VATINVOICE_S00
        public static DataTable UpdateVATInvoice()
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[0];

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_VATINVOICE_M00", objParam);

            return dtReturn;
        }
        #endregion

        #region UPDATE HOADONPREVIEWINFO_S00

        public static DataTable UpdateHoadonPreviewInfo(string strUserSeq, string strPaymentDt, string strPaymentSeq, string strUserTaxCd, string strModPaymentDt, string strInvoiceContYn)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[6];

            objParam[0] = strUserSeq;
            objParam[1] = strPaymentDt;
            objParam[2] = strPaymentSeq;
            objParam[3] = strUserTaxCd;
            objParam[4] = strModPaymentDt;
            objParam[5] = strInvoiceContYn;
            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_UPDATE_HOADONPREVIEWINFO_S00_M00", objParam);

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
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(strNewInvoiceNo);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_UPDATE_INVOICEFORTEMP_M00", objParam);

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
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(strNewInvoiceNo);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_UPDATE_INVOICEFORTEMP_M04", objParam);

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
            object[] objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(strNewInvoiceNo);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_UPDATE_INVOICEFORTEMP_M05", objParam);

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
            object[] objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(strNewInvoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(strReInvoiceNo);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_UPDATE_INVOICEFORTEMP_M01", objParam);

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
            object[] objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(strNewInvoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(strReInvoiceNo);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_UPDATE_INVOICEFORTEMP_M02", objParam);

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
            object[] objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(strNewInvoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(strReInvoiceNo);

            objReturn = SPExecute.ExecReturnNo("KN_USP_SET_UPDATE_INVOICEFORTEMP_M03", objParam);

            return objReturn;
        }

        #endregion

        #region Update HoadonInfoAPTNew CC

        public static DataTable UpdatingHoadonInfoAPTNewCC(string printBundleNo)
        {
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_CANCEL_INVOICE_CC_U00", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingCancelPaymentListAPTNew(string invoiceNo, string roomNo, string payDt)
        {
            var objParam = new object[3];

            objParam[0] = TextLib.MakeNullToEmpty(invoiceNo);
            objParam[1] = TextLib.MakeNullToEmpty(roomNo);
            objParam[2] = TextLib.MakeNullToEmpty(payDt);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_CANCEL_PaymentListAPTNew_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region Update HoadonInfo Hidden

        public static DataTable UpdatingHoadonInfoHiddenY(string printBundleNo)
        {
            var objParam = new object[1];
           
            objParam[0] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_HIDDEN_DEBIT_Y_U00", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingHoadonInfoHiddenN(string printBundleNo)
        {
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_HIDDEN_DEBIT_N_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region Update KN Report Cancel HoadonInfo NM to CC

        public static DataTable UpdatingKNHoadonInfoCancelCC(string printBundleNo)
        {
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_KN_UPDATE_CANCEL_HOADONINFO_CC_U00", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingKNHoadonInfoCancelNM(string printBundleNo)
        {
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_KN_UPDATE_CANCEL_HOADONINFO_NM_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region Update PrintBundleNo for HoadonInfo

        //Update Set PRINT_BUNDLE_NO = REF_SEQ_NO */

        public static DataTable UpdatingPrintBundleNoHoadonInfo(string refSeq, string printBundleNo)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refSeq);
            objParam[1] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_HIDDEN_DEBIT_REF_SERIAL_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region Update PrintBundleNo for Cancel HoadonInfo KeangNam

        //Update Set PRINT_BUNDLE_NO = REF_SEQ_NO */

        public static DataTable UpdatingPrintBundleNoKNCanHoadonInfo(string refSeq, string printBundleNo)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refSeq);
            objParam[1] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_KN_UPDATE_CANCEL_INVOICE_U00", objParam);

            return dtReturn;
        }

        #endregion

        #region Updating Invoice no for hoadon : 인보이스 번호 업데이트 실시

        /**********************************************************************************************
         * Mehtod   명 : UpdatingInvoiceNoForHoadon
         * 개   발  자 : Jeong Seung Hwa
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
        /// <param name="refSeq"> </param>
        /// <param name="printBundleNo"></param>
        /// <returns></returns>
        /* Update Set PRINT_BUNDLE_NO = REF_SEQ_NO */
        public static DataTable UpdatingPrintBundleNo(string refSeq, string printBundleNo)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refSeq);
            objParam[1] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_DEBIT_NOTE_U00", objParam);

            return dtReturn;
        }

        /* Update Set Ref_PrintNo = REF_SEQ_NO */
        public static DataTable UpdatingPrintParkingDebitList(string refSeq, string printBundleNo)
        {
            var objParam = new object[2];

            objParam[0] = TextLib.MakeNullToEmpty(refSeq);
            objParam[1] = TextLib.MakeNullToEmpty(printBundleNo);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_PARKING_DEBIT_NOTE_U00", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingPrintBundleNoYN(string rentCd, string refSerialNo, string printBundleNo, string refSeq)
        {
            var dtReturn = new DataTable();
            var objParam = new object[4];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);
            objParam[1] = TextLib.MakeNullToEmpty(refSerialNo);
            objParam[2] = TextLib.MakeNullToEmpty(printBundleNo);
            objParam[3] = TextLib.MakeNullToEmpty(refSeq);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_DEBIT_NOTE_U11", objParam);

            return dtReturn;
        }

        /*----------------------Update PRINT_BUNDLE_NO to Null---------------*/

        public static DataTable UpdatingNullPrintBundleNoHoadonInfo(string rentCd)
        {
            var dtReturn = new DataTable();
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_PRINTBUNDLENO_HOADONINFO_U00", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingNullPrintBundleNo(string rentCd)
        {
            var dtReturn = new DataTable();
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);
            

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_DEBIT_NOTE_U01", objParam);

            return dtReturn;
        }

        //**********************************************************************************************
        // * Mehtod   명 :UpdatingNullParkingDebitList
         //* Descriptions : Update ParkingDebitList set Ref_PrintNo = '' 
         //* Author : phuongtv
         //* PG: KN_USP_UPDATE_PARKING_DEBIT_NOTE_U01
        // * Date : 2014.10.16        
        //**********************************************************************************************
        public static DataTable UpdatingNullParkingDebitList(string rentCd)
        {
            var dtReturn = new DataTable();
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_UPDATE_PARKING_DEBIT_NOTE_U01", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingNullPrintBundleNoYN(string rentCd)
        {
            var dtReturn = new DataTable();
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(rentCd);


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_DEBIT_NOTE_NULL", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingCreateYN(string printBundleNo)
        {
            var dtReturn = new DataTable();
            var objParam = new object[1];
           
            objParam[0] = TextLib.MakeNullToEmpty(printBundleNo);
           

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_DEBIT_NOTE_U02", objParam);

            return dtReturn;
        }

        public static DataTable UpdatingIsPrintAPT(string refPrintNo)
        {
            var dtReturn = new DataTable();
            var objParam = new object[1];

            objParam[0] = TextLib.MakeNullToEmpty(refPrintNo);


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RPT_UPDATE_INVOICE_APT_U00", objParam);

            return dtReturn;
        }

        #endregion
    }
}
