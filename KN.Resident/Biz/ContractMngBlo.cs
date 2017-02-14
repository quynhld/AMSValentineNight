using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using KN.Resident.Ent;
using KN.Resident.Dac;

namespace KN.Resident.Biz
{
    public class ContractMngBlo
    {
        #region SpreadSalesInfo : 아파트 임대계약정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadSalesInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-13
         * 용       도 : 아파트 임대계약정보 조회
         * Input    값 : SpreadSalesInfo(페이지크기, 현재페이지번호, 섹션코드, 검색조건코드, 검색어, 저장여부)
         * Ouput    값 : DataSet
         **********************************************************************************************/

        /// <summary>
        /// SpreadSalesInfo : 임대계약정보 조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strKeyCd">검색조건코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strSaveYn">저장여부</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strLessorTy"> </param>
        /// <returns></returns>
        public static DataSet SpreadSalesInfo(int intPageSize, int intNowPage, string strRentCd, string strKeyCd, string strKeyWord, string strSaveYn, string strLangCd,string strLessorTy)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ContractMngDao.SelectSalesInfo(intPageSize, intNowPage, strRentCd, strKeyCd, strKeyWord, strSaveYn, strLangCd,strLessorTy);

            return dsReturn;
        }

        #endregion

        #region WatchSalesInfoView : 아파트 임대계약정보 상세조회

        /**********************************************************************************************
         * Mehtod   명 : WatchSalesInfoView
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-13
         * 용       도 : 임대계약정보 상세조회
         * Input    값 : WatchSalesInfoView(언어코드, 임대구분코드, 임대순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchRentInfo : 임대계약정보 상세조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대순번</param>
        /// <returns></returns>
        public static DataSet WatchSalesInfoView(string strLangCd, string strRentCd, int intRentSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ContractMngDao.SelectSalesInfoView(strLangCd, strRentCd, intRentSeq);

            return dsReturn;
        }

        #endregion

        #region SpreadSalesInfoExcelView : 아파트 임대계약정보 엑셀 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadSalesInfoExcelView (상세보기 조회용)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-18
         * 용       도 : 아파트 임대계약정보 엑셀 조회
         * Input    값 : SpreadSalesInfoExcelView(섹션코드, 검색조건코드, 검색어, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadSalesInfoExcelView : 아파트 임대계약정보 엑셀 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strKeyCd">검색조건코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadSalesInfoExcelView(string strRentCd, string strKeyCd, string strKeyWord, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.SelectSalesInfoExcelView(strRentCd, strKeyCd, strKeyWord, strLangCd);

            return dtReturn;
        }

        #endregion

        #region WatchRentInfoView : 임대계약정보 상세보기 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchRentInfoView (상세보기 조회용)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-27
         * 용       도 : 임대계약정보 상세보기 조회
         * Input    값 : WatchRentInfoView(언어코드, 임대구분코드, 임대순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchRentInfoView : 임대계약정보 상세보기 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대순번</param>
        /// <returns></returns>
        public static DataSet WatchRentInfoView(string strLangCd, string strRentCd, int intRentSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ContractMngDao.SelectRentInfoView(strLangCd, strRentCd, intRentSeq);

            return dsReturn;
        }

        #endregion

        #region WatchRentInfoDetailView : 임대계약정보 상세보기 수정용 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchRentInfoDetailView (수정용 조회용)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-29
         * 용       도 : 임대계약정보 상세보기 수정용 조회
         * Input    값 : WatchRentInfoView(언어코드, 임대구분코드, 임대순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchRentInfoDetailView : 임대계약정보 상세보기 수정용 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intRentSeq">임대순번</param>
        /// <returns></returns>
        public static DataSet WatchRentInfoDetailView(string strLangCd, string strRentCd, string strCompNo, string strMemNo, int intRentSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ContractMngDao.SelectRentInfoDetailView(strLangCd, strRentCd, strCompNo, strMemNo, intRentSeq);

            return dsReturn;
        }

        #endregion

        #region WatchExistSalesInfo : 임대계약 중복 방번호 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExistSalesInfo (임대계약 중복 방번호 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-06
         * 용       도 : 임대계약 중복 방번호 조회
         * Input    값 : WatchExistSalesInfo(층번호, 방번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchExistSalesInfo : 임대계약 중복 방번호 조회
        /// </summary>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataTable WatchExistSalesInfo(int intFloorNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.SelectExistSalesInfo(intFloorNo, strRoomNo);

            return dtReturn;
        }

        #endregion

        #region SpreadRentInfo : 오피스 및 리테일 임대계약정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRentInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-19
         * 용       도 : 오피스 및 리테일 임대계약정보 조회
         * Input    값 : SpreadRentInfo(페이지크기, 현재페이지번호, 섹션코드, 검색조건코드, 검색어, 저장여부)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadRentInfo : 오피스 및 리테일 임대계약정보 조회
        /// </summary>
        /// <param name="intPageSize">페이지크기</param>
        /// <param name="intNowPage">현재페이지번호</param>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strKeyCd">검색조건코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strSaveYn">저장여부</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SpreadRentInfo(int intPageSize, int intNowPage, string strRentCd, string strKeyCd, string strKeyWord, string strSaveYn, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ContractMngDao.SelectRentInfo(intPageSize, intNowPage, strRentCd, strKeyCd, strKeyWord, strSaveYn, strLangCd);

            return dsReturn;
        }

        #endregion

        #region SpreadRentInfoExcelView : 오피스 및 리테일 임대계약정보 엑셀 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRentInfoExcelView (상세보기 조회용)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-18
         * 용       도 : 오피스 및 리테일 임대계약정보 엑셀 조회
         * Input    값 : SpreadRentInfoExcelView(섹션코드, 검색조건코드, 검색어, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadRentInfoExcelView : 오피스 및 리테일 임대계약정보 엑셀 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strKeyCd">검색조건코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadRentInfoExcelView(string strRentCd, string strKeyCd, string strKeyWord, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.SelectRentInfoExcelView(strRentCd, strKeyCd, strKeyWord, strLangCd);

            return dtReturn;
        }

        public static DataTable SpreadTenantBalanceExcelView(string feeTy, string feeTyDt, string period, string periodE, string payDt, string payDtE, string roomNo, string userNm,string paidCd)
        {
            var dtReturn = ContractMngDao.SpreadTenantBalanceExcelView(feeTy, feeTyDt, period, periodE,payDt,payDtE,roomNo,userNm,paidCd);

            return dtReturn;
        }

        public static DataTable SpreadTenantBalanceInvoiceExcelView(string feeTy, string feeTyDt, string period, string periodE, string payDt, string payDtE, string roomNo, string userNm, string paidCd, string invoiceNo)
        {
            var dtReturn = ContractMngDao.SpreadTenantBalanceInvoiceExcelView(feeTy, feeTyDt, period, periodE, payDt, payDtE, roomNo, userNm, paidCd, invoiceNo);

            return dtReturn;
        }

        public static DataTable SpreadVatOutputExcelView(string period)
        {
            var dtReturn = ContractMngDao.SpreadVatOutputExcelView( period);

            return dtReturn;
        }

        public static DataTable SpreadReceivableFromTenant(string period,string feeTy,string feeTyDt)
        {
            var dtReturn = ContractMngDao.SpreadReceivableFromTenant(period,feeTy,feeTyDt);

            return dtReturn;
        }

        public static DataTable SpreadPrepaidRevenue(string period, string periodE, string paidDt,string paidDtE)
        {
            var dtReturn = ContractMngDao.SSpreadPrepaidRevenue(period, periodE, paidDt,paidDtE);

            return dtReturn;
        }

        #endregion

        #region WatchExistRentInfo : 오피스 및 리테일 임대계약 중복 방번호 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExistRentInfo (오피스 및 리테일 임대계약 중복 방번호 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-22
         * 용       도 : 오피스 및 리테일 임대계약 중복 방번호 조회
         * Input    값 : WatchExistRentInfo(층번호, 방번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchExistRentInfo : 오피스 및 리테일 임대계약 중복 방번호 조회
        /// </summary>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <returns></returns>
        public static DataTable WatchExistRentInfo(int intFloorNo, string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.SelectExistRentInfo(intFloorNo, strRoomNo);

            return dtReturn;
        }

        #endregion

        #region SpreadTempRentDepositInfo : 오피스 및 리테일 선수금 임시정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadTempRentDepositInfo (오피스 및 리테일 선수금 임시정보 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 및 리테일 선수금 임시정보 조회
         * Input    값 : SpreadTempRentDepositInfo(임시순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadTempRentDepositInfo : 오피스 및 리테일 선수금 임시정보 조회
        /// </summary>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <returns></returns>
        public static DataTable SpreadTempRentDepositInfo(int intDepositTmpSeq, string strCompNo, string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.SelectTempRentDepositInfo(intDepositTmpSeq, strCompNo, strMemNo);

            return dtReturn;
        }

        #endregion

        #region SpreadRentDepositInfo : 오피스 및 리테일 선수금 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRentDepositInfo (오피스 및 리테일 선수금 정보 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-28
         * 용       도 : 오피스 및 리테일 선수금 정보 조회
         * Input    값 : SpreadRentDepositInfo(계약코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadRentDepositInfo : 오피스 및 리테일 선수금 정보 조회
        /// </summary>
        /// <param name="strContractNo">계약코드</param>
        /// <returns></returns>
        public static DataTable SpreadRentDepositInfo(string strContractNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.SelectRentDepositInfo(strContractNo);

            return dtReturn;
        }

        #endregion
        
        #region SpreadTempRentFeeInfo : 오피스 및 리테일 임대료 임시정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadTempRentFeeInfo (오피스 및 리테일 임대료 임시정보 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 오피스 및 리테일 선수금 임시정보 조회
         * Input    값 : SpreadTempRentFeeInfo(임시순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadTempRentFeeInfo : 오피스 및 리테일 임대료 임시정보 조회
        /// </summary>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <returns></returns>
        public static DataTable SpreadTempRentFeeInfo(int intRentFeeTmpSeq, string strCompNo, string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.SelectTempRentFeeInfo(intRentFeeTmpSeq, strCompNo, strMemNo);

            return dtReturn;
        }

        #endregion

        #region SpreadRentFeeInfo : 오피스 및 리테일 임대료 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRentFeeInfo (오피스 및 리테일 임대료 정보 조회)
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 및 리테일 임대료 정보 조회
         * Input    값 : SpreadRentFeeInfo(계약코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadRentFeeInfo : 오피스 및 리테일 임대료 정보 조회
        /// </summary>
        /// <param name="strContractNo">계약코드</param>
        /// <returns></returns>
        public static DataTable SpreadRentFeeInfo(string strContractNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.SelectRentFeeInfo(strContractNo);

            return dtReturn;
        }

        #endregion

        #region RegistrySalesMngInfo : 아파트 계약관련정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistrySalesMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-12
         * 용       도 : 아파트 계약관련정보 등록
         * Input    값 : RegistrySalesMngInfo(아파트 계약정보 객체, 공동소유주 객체, 계약정보 법인 객체)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistrySalesMngInfo : 아파트 계약관련정보 등록
        /// </summary>
        /// <param name="rsDs">아파트 계약정보 객체</param>
        /// <param name="rsColDs">공동소유주 객체</param>
        /// <param name="rsComDs">계약정보법인 객체</param>
        /// <returns></returns>
        public static DataTable RegistrySalesMngInfo(RentMngDs.SalesInfo rsDs, RentMngDs.SalesColInfo rsColDs, RentMngDs.SalesCompInfo rsComDs)
        {
            var dtReturn = ContractMngDao.InsertSalesInfo(rsDs);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    var intRentSeq = 0;

                    intRentSeq = Int32.Parse(dtReturn.Rows[0]["RentSeq"].ToString());
                    var contractNo = dtReturn.Rows[0]["ContractNo"].ToString();

                    if (rsColDs != null)
                    {
                        rsColDs.RentSeq = intRentSeq;
                        ContractMngDao.InsertSalesCoInfo(rsColDs);
                    }

                    if (rsComDs != null)
                    {
                        rsComDs.RentSeq = intRentSeq;
                        ContractMngDao.InsertSalesCompInfo(rsComDs);
                    }
                    if (rsDs.APPL_YN=="Y")
                    {                                              
                        var listFitFee = rsDs.ListFitFee.Split(Char.Parse("|"));
                        //foreach (var fee in listFitFee.Select(s => listFitFee.Split(Char.Parse(","))).TakeWhile(fee => fee.ToString() != ""))
                        //{
                        //    objReturn = ContractMngDao.InsertFitFee(fee,contractNo,"N");
                        //}
                        foreach (var fitFee in listFitFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())))
                        {
                            if(fitFee[0].Equals(""))break;
                            var mngfeeItem = new string[7];
                            mngfeeItem[0] = fitFee[0];
                            mngfeeItem[1] = fitFee[1];
                            mngfeeItem[2] = fitFee[2];
                            mngfeeItem[3] = fitFee[3];
                            mngfeeItem[4] = "001";//rsDs.CompNo;
                            mngfeeItem[5] = rsDs.InsMemNo;
                            mngfeeItem[6] = rsDs.InsMemIp;
                            ContractMngDao.InsertMngFee(mngfeeItem, contractNo, "Y");
                        }
                    }
                    //Insert MngFee
                    var listMngFee = rsDs.ListMngFee.Split(Char.Parse("|"));
                    foreach (var fitFee in listMngFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())))
                    {
                        if (fitFee[0].Equals("")) break;
                        var mngfeeItem = new string[7];
                        mngfeeItem[0] = fitFee[0];
                        mngfeeItem[1] = fitFee[1];
                        mngfeeItem[2] = fitFee[2];
                        mngfeeItem[3] = fitFee[3];
                        mngfeeItem[4] = "001";//rsDs.CompNo;
                        mngfeeItem[5] = rsDs.InsMemNo;
                        mngfeeItem[6] = rsDs.InsMemIp;
                        ContractMngDao.InsertMngFee(mngfeeItem, contractNo, "N");
                    }
                }
            }

            return dtReturn;
        }

        //Baotv - Delete Fee
        public static object[] DeleteMngInfo(int feeSeq,string contracNo)
        {
            var objReturn = ContractMngDao.DeleteMngInfo(feeSeq, contracNo);

            return objReturn;
        }

        //Baotv - Delete Rent Fee
        public static object[] DeleteRentFeeInfo(int feeSeq, string contracNo)
        {
            var objReturn = ContractMngDao.DeleteRentFeeInfo(feeSeq, contracNo);

            return objReturn;
        }

        #endregion

        #region RegistryTempRentDepositInfo : 오피스 / 리테일 보증금 임시 저장

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 / 리테일 보증금 임시 저장
         * Input    값 : RegistryTempRentDepositInfo(섹션코드, 임시순번, 상세순번, 보증금예상납부일, 보증금, 적용환율, 보증금수납일, 보증금수납액)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempRentDepositInfo : 오피스 / 리테일 보증금 임시 저장
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <param name="strDepositExpDt">보증금예상납부일</param>
        /// <param name="dblDepositExpAmt">보증금</param>
        /// <param name="dblDepositExcRate">적용환율</param>
        /// <param name="strDepositPayDt">보증금수납일</param>
        /// <param name="dblDepositPayAmt">보증금수납액</param>
        /// <returns></returns>
        public static DataTable RegistryTempRentDepositInfo(string strRentCd, int intDepositTmpSeq, int intDepositSeq, string strCompNo, string strMemNo, string strDepositExpDt, double dblDepositExpAmt, double dblDepositExcRate, string strDepositPayDt, double dblDepositPayAmt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.InsertTempRentDepositInfo(strRentCd, intDepositTmpSeq, intDepositSeq, strCompNo, strMemNo, strDepositExpDt, dblDepositExpAmt, dblDepositExcRate, strDepositPayDt, dblDepositPayAmt);

            return dtReturn;
        }

        #endregion

        #region RegistryRentInfo : 리테일 및 오피스 임대 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryRentInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-25
         * 용       도 : 리테일 및 오피스 임대 정보 등록
         * Input    값 : RegistryRentInfo(리테일 및 오피스 임대 정보 객체)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryRentInfo : 리테일 및 오피스 임대 정보 등록
        /// </summary>
        /// <param name="riDs">리테일 및 오피스 임대 정보 객체</param>
        /// <returns></returns>
        public static DataTable RegistryRentInfo(RentMngDs.RentInfo riDs)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.InsertRentInfo(riDs);

            return dtReturn;
        }

        #endregion

        #region RegistryRentDepositInfo : 리테일 및 오피스 임대 보증금 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-25
         * 용       도 : 리테일 및 오피스 임대 보증금 정보 등록
         * Input    값 : RegistryRentDepositInfo(계약번호, 임시순번, 회사번호, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryRentDepositInfo : 리테일 및 오피스 임대 보증금 정보 등록
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <param name="strInsCompNo">회사번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static DataTable RegistryRentDepositInfo(string strContractNo, int intDepositTmpSeq, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.InsertRentDepositInfo(strContractNo, intDepositTmpSeq, strInsCompNo, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryRentMngInfo : 리테일 및 오피스 임대 관련 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryRentMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-25
         * 용       도 : 리테일 및 오피스 임대 관련 정보 등록
         * Input    값 : RegistryRentMngInfo(리테일 및 오피스 임대 정보 객체, 보증금임시순번, 임대료임시순번, 회사번호, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryRentMngInfo : 리테일 및 오피스 임대 관련 정보 등록
        /// </summary>
        /// <param name="riDs">리테일 및 오피스 임대 정보 객체</param>
        /// <param name="intDepositTmpSeq">보증금임시순번</param>
        /// <param name="intRentFeeTmpSeq">임대료임시순번</param>
        /// <param name="strInsCompNo">회사번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static DataTable RegistryRentMngInfo(RentMngDs.RentInfo riDs, int intDepositTmpSeq, int intRentFeeTmpSeq, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            string strContractNo = string.Empty;

            var dtReturn = ContractMngDao.InsertRentInfo(riDs);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    strContractNo = dtReturn.Rows[0]["ContractNo"].ToString();

                    ContractMngDao.InsertRentDepositInfo(strContractNo, intDepositTmpSeq, strInsCompNo, strInsMemNo, strInsMemIP);

                    //Insert Rent Fee
                    if (riDs.PodiumYn.Equals("N"))
                    {
                        var listRentFee = riDs.ListRentFee.Split(Char.Parse("|"));
                        foreach (var fitFee in listRentFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())))
                        {
                            if (fitFee[0].Equals("")) break;
                            var feeItem = new string[8];
                            feeItem[0] = strContractNo;
                            feeItem[1] = fitFee[0];
                            feeItem[2] = fitFee[1];
                            feeItem[3] = fitFee[2];
                            feeItem[4] = fitFee[3];
                            feeItem[5] = "001";//rsDs.CompNo;
                            feeItem[6] = riDs.InsMemNo;
                            feeItem[7] = riDs.InsMemIP;
                            ContractMngDao.InsertRentFeeInfo(feeItem);
                        }
                    }
                }

                //Insert FittingFee
                if (riDs.APPL_YN == "Y")
                {
                    var listFitFee = riDs.ListFitFee.Split(Char.Parse("|"));
                    foreach (var fitFee in listFitFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())).TakeWhile(fitFee => !fitFee[0].Equals("")))
                    {
                        var fitfeeItem = new string[7];
                        fitfeeItem[0] = fitFee[0];
                        fitfeeItem[1] = fitFee[1];
                        fitfeeItem[2] = fitFee[2];
                        fitfeeItem[3] = fitFee[3];
                        fitfeeItem[4] = "001";//rsDs.CompNo;
                        fitfeeItem[5] = riDs.InsMemNo;
                        fitfeeItem[6] = riDs.InsMemIP;
                        ContractMngDao.InsertMngFee(fitfeeItem, strContractNo, "Y");
                    }
                }

                //Insert MngFee
                var listMngFee = riDs.ListMngFee.Split(Char.Parse("|"));
                foreach (var fitFee in listMngFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())))
                {
                    if (fitFee[0].Equals("")) break;
                    var mngfeeItem = new string[7];
                    mngfeeItem[0] = fitFee[0];
                    mngfeeItem[1] = fitFee[1];
                    mngfeeItem[2] = fitFee[2];
                    mngfeeItem[3] = fitFee[3];
                    mngfeeItem[4] = "001";//rsDs.CompNo;
                    mngfeeItem[5] = riDs.InsMemNo;
                    mngfeeItem[6] = riDs.InsMemIP;
                    ContractMngDao.InsertMngFee(mngfeeItem, strContractNo, "N");
                }
            }

            return dtReturn;
        }

        #endregion

        #region RegistryTempRentFeeInfo : 오피스 / 리테일 임대료 임시 저장

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 오피스 / 리테일 임대료 임시 저장
         * Input    값 : RegistryTempRentFeeInfo(섹션코드, 임시순번, 상세순번, 임대료적용시작일, 임대료적용종료일, 적용환율, 임대료청구액)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempRentFeeInfo : 오피스 / 리테일 보증금 임시 저장
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <param name="intRentFeeSeq">상세순번</param>
        /// <param name="strRentFeeStartDt">임대료적용시작일</param>
        /// <param name="strRentFeeEndDt">임대료적용종료일</param>
        /// <param name="dblRentFeeExcRate">적용환율</param>
        /// <param name="dblRentFeePayAmt">임대료청구액</param>
        /// <returns></returns>
        public static DataTable RegistryTempRentFeeInfo(string strRentCd, int intRentFeeTmpSeq, int intRentFeeSeq, string strCompNo, string strMemNo, string strRentFeeStartDt, string strRentFeeEndDt, double dblRentFeeExcRate, double dblRentFeePayAmt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ContractMngDao.InsertTempRentFeeInfo(strRentCd, intRentFeeTmpSeq, intRentFeeSeq, strCompNo, strMemNo, strRentFeeStartDt, strRentFeeEndDt, dblRentFeeExcRate, dblRentFeePayAmt);

            return dtReturn;
        }

        #endregion

        #region RegistryMakeAPTMngFeeListInfo : 아파트 관리비 대상자 수동 생성

        /**********************************************************************************************
         * Mehtod   명 : RegistryMakeAPTMngFeeListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-15
         * 용       도 : 아파트 관리비 대상자 수동 생성
         * Input    값 : RegistryMakeAPTMngFeeListInfo(관리비 대상자 수동 생성)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMakeAPTMngFeeListInfo : 아파트 관리비 대상자 수동 생성
        /// </summary>
        /// <returns></returns>
        public static object[] RegistryMakeAPTMngFeeListInfo()
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.InsertMakeAPTMngFeeListInfo();

            return objReturn;
        }

        #endregion

        #region RegistryMakeAPTRMngFeeListInfo : 아파트 리테일 관리비 대상자 수동 생성

        /**********************************************************************************************
         * Mehtod   명 : RegistryMakeAPTRMngFeeListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-15
         * 용       도 : 아파트 리테일 관리비 대상자 수동 생성
         * Input    값 : RegistryMakeAPTRMngFeeListInfo(관리비 대상자 수동 생성)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMakeAPTRMngFeeListInfo : 아파트 리테일 관리비 대상자 수동 생성
        /// </summary>
        /// <returns></returns>
        public static object[] RegistryMakeAPTRMngFeeListInfo()
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.InsertMakeAPTRMngFeeListInfo();

            return objReturn;
        }

        #endregion

        #region RegistryMakeOfficeRetailFeeListInfo : 관리비 및 임대료 대상자 수동 생성

        /**********************************************************************************************
         * Mehtod   명 : RegistryMakeOfficeRetailMngFeeListInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-15
         * 용       도 : 관리비 및 임대료 대상자 수동 생성
         * Input    값 : RegistryMakeOfficeRetailMngFeeListInfo(관리비 대상자 수동 생성)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMakeOfficeRetailMngFeeListInfo : 관리비 및 임대료 대상자 수동 생성
        /// </summary>
        /// <returns></returns>
        public static object[] RegistryMakeOfficeRetailFeeListInfo()
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.InsertMakeOfficeRetailMngFeeListInfo();

            return objReturn;
        }

        #endregion

        #region ModifySalesMngInfo : 아파트 계약관련정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifySalesMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-29
         * 용       도 : 아파트 계약관련정보 수정
         * Input    값 : ModifySalesMngInfo(아파트 계약정보 객체, 공동소유주 객체, 계약정보 법인 객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifySalesMngInfo : 아파트 계약관련정보 수정
        /// </summary>
        /// <param name="rsDs">아파트 계약정보 객체</param>
        /// <param name="rsColDs">공동소유주 객체</param>
        /// <param name="rsComDs">계약정보 법인 객체</param>
        /// <returns></returns>
        public static object[] ModifySalesMngInfo(RentMngDs.SalesInfo rsDs, RentMngDs.SalesColInfo rsColDs, RentMngDs.SalesCompInfo rsComDs)
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.UpdateSalesInfo(rsDs);

            if (objReturn != null)
            {
                if (rsColDs != null)
                {
                    objReturn = ContractMngDao.UpdateSalesCoInfo(rsColDs);
                }

                if (rsComDs != null)
                {
                    objReturn = ContractMngDao.UpdateSalesCompInfo(rsComDs);
                }
                //if (rsDs.APPL_YN == "Y")
                //{
                //    var listFitFee = rsDs.ListFitFee.Split(Char.Parse("|"));
                //    foreach (var fitFee in listFitFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())))
                //    {
                //        if (fitFee[0].Equals("")) break;
                //        if (fitFee[1].Equals(""))
                //        {
                //            var feeItem = new string[7];
                //            feeItem[0] = fitFee[2];
                //            feeItem[1] = fitFee[3];
                //            feeItem[2] = fitFee[4];
                //            feeItem[3] = fitFee[5];
                //            feeItem[4] = "001";//rsDs.CompNo;
                //            feeItem[5] = rsDs.InsMemNo;
                //            feeItem[6] = rsDs.InsMemIp;


                //            ContractMngDao.InsertMngFee(feeItem, rsDs.ContractNo, "Y");
                //        }
                //        else
                //        {
                //            if (fitFee[0].Equals("N"))
                //            {
                //                continue;
                //            }
                //            var feeItem = new string[8];                            
                //            feeItem[0] = fitFee[2];
                //            feeItem[1] = fitFee[3];
                //            feeItem[2] = fitFee[4];
                //            feeItem[3] = fitFee[5];
                //            feeItem[4] = fitFee[1];
                //            feeItem[5] = "001";//rsDs.CompNo;
                //            feeItem[6] = rsDs.InsMemNo;
                //            feeItem[7] = rsDs.InsMemIp;
                //            ContractMngDao.UpdateMngInfo(feeItem, rsDs.ContractNo);
                //        }
                       
                //    }
                //}
                if (rsDs.APPL_YN == "Y")
                {
                    var listFitFee = rsDs.ListFitFee.Split(Char.Parse("|"));
                    foreach (var fitFee in listFitFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())).TakeWhile(fitFee => !fitFee[0].Equals("")))
                    {
                        if (fitFee[1].Equals(""))
                        {
                            var feeItem = new string[7];
                            feeItem[0] = fitFee[2];
                            feeItem[1] = fitFee[3];
                            feeItem[2] = fitFee[4];
                            feeItem[3] = fitFee[5];
                            feeItem[4] = "001";//rsDs.CompNo;
                            feeItem[5] = rsDs.InsMemNo;
                            feeItem[6] = rsDs.InsMemIp;


                            ContractMngDao.InsertMngFee(feeItem, rsDs.ContractNo, "Y");
                        }
                        else
                        {
                            if (fitFee[0].Equals("N"))
                            {
                                continue;
                            }
                            var feeItem = new string[8];
                            feeItem[0] = fitFee[2];
                            feeItem[1] = fitFee[3];
                            feeItem[2] = fitFee[4];
                            feeItem[3] = fitFee[5];
                            feeItem[4] = fitFee[1];
                            feeItem[5] = "001";//rsDs.CompNo;
                            feeItem[6] = rsDs.InsMemNo;
                            feeItem[7] = rsDs.InsMemIp;
                            ContractMngDao.UpdateMngInfo(feeItem, rsDs.ContractNo);
                        }
                    }
                }

                var listMngFee = rsDs.ListMngFee.Split(Char.Parse("|"));
                foreach (var fitFee in listMngFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())).TakeWhile(fitFee => !fitFee[0].Equals("")))
                {
                    if (fitFee[1].Equals(""))
                    {
                        var feeItem = new string[7];
                        feeItem[0] = fitFee[2];
                        feeItem[1] = fitFee[3];
                        feeItem[2] = fitFee[4];
                        feeItem[3] = fitFee[5];
                        feeItem[4] = "001"; //rsDs.CompNo;
                        feeItem[5] = rsDs.InsMemNo;
                        feeItem[6] = rsDs.InsMemIp;


                        ContractMngDao.InsertMngFee(feeItem, rsDs.ContractNo, "N");
                    }
                    else
                    {
                        if (fitFee[0].Equals("N"))
                        {
                            continue;
                        }
                        var feeItem = new string[8];
                        feeItem[0] = fitFee[2];
                        feeItem[1] = fitFee[3];
                        feeItem[2] = fitFee[4];
                        feeItem[3] = fitFee[5];
                        feeItem[4] = fitFee[1];
                        feeItem[5] = "001"; //rsDs.CompNo;
                        feeItem[6] = rsDs.InsMemNo;
                        feeItem[7] = rsDs.InsMemIp;
                        ContractMngDao.UpdateMngInfo(feeItem, rsDs.ContractNo);
                    }
                }
            }

            return objReturn;
        }

        #endregion

        #region ModifyTempRentDepositInfo : 오피스 / 리테일 보증금 임시 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 / 리테일 보증금 임시 수정
         * Input    값 : ModifyTempRentDepositInfo(섹션코드, 임시순번, 상세순번, 보증금예상납부일, 보증금, 적용환율, 보증금수납일, 보증금수납액)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyTempRentDepositInfo : 오피스 / 리테일 보증금 임시 수정
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <param name="strDepositExpDt">보증금예상납부일</param>
        /// <param name="dblDepositExpAmt">보증금</param>
        /// <param name="dblDepositExcRate">적용환율</param>
        /// <param name="strDepositPayDt">보증금수납일</param>
        /// <param name="dblDepositPayAmt">보증금수납액</param>
        /// <returns></returns>
        public static object[] ModifyTempRentDepositInfo(string strRentCd, int intDepositTmpSeq, int intDepositSeq, string strCompNo, string strMemNo, string strDepositExpDt, double dblDepositExpAmt, double dblDepositExcRate, string strDepositPayDt, double dblDepositPayAmt)
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.UpdateTempRentDepositInfo(strRentCd, intDepositTmpSeq, intDepositSeq, strCompNo, strMemNo, strDepositExpDt, dblDepositExpAmt, dblDepositExcRate, strDepositPayDt, dblDepositPayAmt);

            return objReturn;
        }

        #endregion

        #region ModifyTempRentFeeInfo : 오피스 / 리테일 임대료 임시 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 오피스 / 리테일 임대료 임시 수정
         * Input    값 : ModifyTempRentFeeInfo(섹션코드, 임시순번, 상세순번, 임대료적용시작일, 임대료적용종료일, 임대료적용환율, 임대료적용달러)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyTempRentFeeInfo : 오피스 / 리테일 임대료 임시 수정
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <param name="intRentFeeSeq">상세순번</param>
        /// <param name="strRentFeeStartDt">임대료적용시작일</param>
        /// <param name="strRentFeeEndDt">임대료적용종료일</param>
        /// <param name="dblRentFeeExcRate">임대료적용환율</param>
        /// <param name="dblRentFeePayAmt">임대료적용달러</param>
        /// <returns></returns>
        public static object[] ModifyTempRentFeeInfo(string strRentCd, int intRentFeeTmpSeq, int intRentFeeSeq, string strCompNo, string strMemNo, string strRentFeeStartDt, string strRentFeeEndDt,
                                                     double dblRentFeeExcRate, double dblRentFeePayAmt)
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.UpdateTempRentFeeInfo(strRentCd, intRentFeeTmpSeq, intRentFeeSeq, strCompNo, strMemNo, strRentFeeStartDt, strRentFeeEndDt, dblRentFeeExcRate, dblRentFeePayAmt);

            return objReturn;
        }

        #endregion

        #region ModifyRentDepoitInfo : 오피스 / 리테일 보증금 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyRentDepoitInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-28
         * 용       도 : 오피스 / 리테일 보증금 수정
         * Input    값 : ModifyRentDepoitInfo(계약번호, 임시순번, 상세순번, 보증금예상납부일, 보증금, 적용환율, 보증금수납일, 보증금수납액)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyRentDepoitInfo : 오피스 / 리테일 보증금 임시 수정
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <param name="strDepositExpDt">보증금예상납부일</param>
        /// <param name="dblDepositExpAmt">보증금</param>
        /// <param name="dblDepositExcRate">적용환율</param>
        /// <param name="strDepositPayDt">보증금수납일</param>
        /// <param name="dblDepositPayAmt">보증금수납액</param>
        /// <returns></returns>
        public static object[] ModifyRentDepoitInfo(string strContractNo, int intDepositSeq, string strDepositExpDt, double dblDepositExpAmt, double dblDepositExcRate, string strDepositPayDt, double dblDepositPayAmt)
        {
            object[] dtReturn = new object[2];

            dtReturn = ContractMngDao.UpdateRentDepoitInfo(strContractNo, intDepositSeq, strDepositExpDt, dblDepositExpAmt, dblDepositExcRate, strDepositPayDt, dblDepositPayAmt);

            return dtReturn;
        }

        #endregion
        
        #region ModifyRentFeeInfo : 오피스 / 리테일 임대료 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 / 리테일 임대료 수정
         * Input    값 : ModifyRentFeeInfo(계약번호, 상세순번, 임대료적용시작일, 임대료적용종료일, 임대료적용환율, 임대료적용달러)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyRentFeeInfo : 오피스 / 리테일 임대료 임시 수정
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <param name="intRentFeeSeq">상세순번</param>
        /// <param name="strRentFeeStartDt">임대료적용시작일</param>
        /// <param name="strRentFeeEndDt">임대료적용종료일</param>
        /// <param name="dblRentFeeExcRate">임대료적용환율</param>
        /// <param name="dblRentFeePayAmt">임대료적용달러</param>
        /// <returns></returns>
        public static object[] ModifyRentFeeInfo(string strContractNo, int intRentFeeSeq, string strRentFeeStartDt, string strRentFeeEndDt,
                                                 double dblRentFeeExcRate, double dblRentFeePayAmt)
        {
            object[] objReturn = new object[2];

           // objReturn = ContractMngDao.UpdateRentFeeInfo(strContractNo, intRentFeeSeq, strRentFeeStartDt, strRentFeeEndDt, dblRentFeeExcRate, dblRentFeePayAmt);

            return objReturn;
        }

        #endregion

        #region ModifyRentMngInfo : 리테일 및 오피스 임대 관련 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyRentMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-30
         * 용       도 : 리테일 및 오피스 임대 관련 정보 수정
         * Input    값 : ModifyRentMngInfo(리테일 및 오피스 임대 정보 객체, 임시임대료번호, 임시보증금번호, 회사번호, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyRentMngInfo : 리테일 및 오피스 임대 관련 정보 수정
        /// </summary>
        /// <param name="riDs">리테일 및 오피스 임대 정보 객체</param>
        /// <param name="intRentFeeTmpSeq">임시임대료번호</param>
        /// <param name="intDepositTmpSeq">임시보증금번호</param>
        /// <param name="strInsCompNo">회사번호</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] ModifyRentMngInfo(RentMngDs.RentInfo riDs, int intRentFeeTmpSeq, int intDepositTmpSeq, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            var objReturn = new object[2];
            var strContractNo = riDs.ContractNo;

            // 리테일 및 오피스 임대 관련 정보 수정
            ContractMngDao.UpdateRentInfo(riDs);
            //Insert Rent Fee
            if (riDs.PodiumYn.Equals("N"))
            {
                var listRentFee = riDs.ListRentFee.Split(Char.Parse("|"));
                foreach (var fitFee in listRentFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())))
                {
                    if (fitFee[0].Equals("")) break;
                    if (fitFee[0].Equals("Y"))
                    {
                        if ((fitFee[1].Equals("")))
                        {
                            var feeItem = new string[8];
                            feeItem[0] = strContractNo;
                            feeItem[1] = fitFee[2];
                            feeItem[2] = fitFee[3];
                            feeItem[3] = fitFee[4];
                            feeItem[4] = fitFee[5];
                            feeItem[5] = "001";//rsDs.CompNo;
                            feeItem[6] = riDs.InsMemNo;
                            feeItem[7] = riDs.InsMemIP;
                            ContractMngDao.InsertRentFeeInfo(feeItem);
                        }
                        else
                        {
                            var feeItem = new string[6];
                            feeItem[0] = strContractNo;
                            feeItem[1] = fitFee[1];
                            feeItem[2] = fitFee[2];
                            feeItem[3] = fitFee[3];
                            feeItem[4] = fitFee[4];
                            feeItem[5] = fitFee[5];
                            ContractMngDao.UpdateRentFeeInfo(feeItem);
                        }
                    }

                }
            }
            if (riDs.APPL_YN == "Y")
            {
                var listFitFee = riDs.ListFitFee.Split(Char.Parse("|"));
                foreach (var fitFee in listFitFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())).TakeWhile(fitFee => !fitFee[0].Equals("")))
                {
                    if (fitFee[1].Equals(""))
                    {
                        var feeItem = new string[7];
                        feeItem[0] = fitFee[2];
                        feeItem[1] = fitFee[3];
                        feeItem[2] = fitFee[4];
                        feeItem[3] = fitFee[5];
                        feeItem[4] = "001";//rsDs.CompNo;
                        feeItem[5] = riDs.InsMemNo;
                        feeItem[6] = riDs.InsMemIP;


                        ContractMngDao.InsertMngFee(feeItem, riDs.ContractNo, "Y");
                    }
                    else
                    {
                        if (fitFee[0].Equals("N"))
                        {
                           continue;
                        }
                        var feeItem = new string[8];
                        feeItem[0] = fitFee[2];
                        feeItem[1] = fitFee[3];
                        feeItem[2] = fitFee[4];
                        feeItem[3] = fitFee[5];
                        feeItem[4] = fitFee[1];
                        feeItem[5] = "001";//rsDs.CompNo;
                        feeItem[6] = riDs.InsMemNo;
                        feeItem[7] = riDs.InsMemIP;
                        ContractMngDao.UpdateMngInfo(feeItem, riDs.ContractNo);
                    }
                }
            }

            var listMngFee = riDs.ListMngFee.Split(Char.Parse("|"));
                foreach (var fitFee in listMngFee.Select(s => s.Split(Char.Parse(","))).TakeWhile(fitFee => !String.IsNullOrEmpty(fitFee.ToString())).TakeWhile(fitFee => !fitFee[0].Equals("")))
                {
                    if (fitFee[1].Equals(""))
                    {
                        var feeItem = new string[7];
                        feeItem[0] = fitFee[2];
                        feeItem[1] = fitFee[3];
                        feeItem[2] = fitFee[4];
                        feeItem[3] = fitFee[5];
                        feeItem[4] = "001"; //rsDs.CompNo;
                        feeItem[5] = riDs.InsMemNo;
                        feeItem[6] = riDs.InsMemIP;


                        ContractMngDao.InsertMngFee(feeItem, riDs.ContractNo, "N");
                    }
                    else
                    {
                        if (fitFee[0].Equals("N"))
                        {
                            continue;
                        }
                        var feeItem = new string[8];
                        feeItem[0] = fitFee[2];
                        feeItem[1] = fitFee[3];
                        feeItem[2] = fitFee[4];
                        feeItem[3] = fitFee[5];
                        feeItem[4] = fitFee[1];
                        feeItem[5] = "001"; //rsDs.CompNo;
                        feeItem[6] = riDs.InsMemNo;
                        feeItem[7] = riDs.InsMemIP;
                        ContractMngDao.UpdateMngInfo(feeItem, riDs.ContractNo);
                    }
                }

            return objReturn;
        }

        #endregion

        #region RemoveEntireTempRentDepositInfo : 오피스 / 리테일 보증금 미삭제 데이터 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveEntireTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 / 리테일 보증금 미삭제 데이터 삭제
         * Input    값 : RemoveEntireTempRentDepositInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveEntireTempRentDepositInfo : 오피스 / 리테일 보증금 미삭제 데이터 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveEntireTempRentDepositInfo()
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.DeleteEntireTempRentDepositInfo();

            return objReturn;
        }

        #endregion
        
        #region RemoveEntireTempRentFeeInfo : 오피스 / 리테일 임대료 미삭제 데이터 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveEntireTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 / 리테일 임대료 미삭제 데이터 삭제
         * Input    값 : RemoveEntireTempRentFeeInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveEntireTempRentFeeInfo : 오피스 / 리테일 임대료 미삭제 데이터 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveEntireTempRentFeeInfo()
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.DeleteEntireTempRentFeeInfo();

            return objReturn;
        }

        #endregion

        #region RemoveEntireTempRentMng : 오피스 / 리테일 미삭제 임시 데이터 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveEntireTempRentMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 / 리테일 미삭제 임시 데이터 삭제
         * Input    값 : RemoveEntireTempRentMng()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveEntireTempRentMng : 오피스 / 리테일 미삭제 임시 데이터 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveEntireTempRentMng()
        {
            object[] objReturn = new object[2];

            // 임시 보증금 데이터 삭제
            objReturn = ContractMngDao.DeleteEntireTempRentDepositInfo();

            if (objReturn != null)
            {
                // 임시 임대료 데이터 삭제
                objReturn = ContractMngDao.DeleteEntireTempRentFeeInfo();
            }

            return objReturn;
        }

        #endregion

        #region RemoveTempRentDepositInfo : 오피스 / 리테일 보증금 임시저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-21
         * 용       도 : 오피스 / 리테일 보증금 임시저장부분 삭제
         * Input    값 : RemoveTempRentDepositInfo(임시순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempRentDepositInfo : 오피스 / 리테일 보증금 임시저장부분 삭제
        /// </summary>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <returns></returns>
        public static object[] RemoveTempRentDepositInfo(int intDepositTmpSeq, string strCompNo, string strMemNo)
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.DeleteTempRentDepositInfo(intDepositTmpSeq, strCompNo, strMemNo);

            return objReturn;
        }

        #endregion

        #region RemoveTempRentFeeInfo : 오피스 / 리테일 임대료 임시저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-21
         * 용       도 : 오피스 / 리테일 임대료 임시저장부분 삭제
         * Input    값 : RemoveTempRentFeeInfo(임시순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempRentFeeInfo : 오피스 / 리테일 임대료 임시저장부분 삭제
        /// </summary>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <returns></returns>
        public static object[] RemoveTempRentFeeInfo(int intRentFeeTmpSeq, string strCompNo, string strMemNo)
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.DeleteTempRentFeeInfo(intRentFeeTmpSeq, strCompNo, strMemNo);

            return objReturn;
        }

        #endregion

        #region RemoveTempRentDepositInfo : 오피스 / 리테일 보증금 임시저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempRentDepositInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-24
         * 용       도 : 오피스 / 리테일 보증금 임시저장부분 삭제
         * Input    값 : RemoveTempRentDepositInfo(임시순번, 상세순번)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempRentDepositInfo : 오피스 / 리테일 보증금 임시저장부분 삭제
        /// </summary>
        /// <param name="intDepositTmpSeq">임시순번</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <returns></returns>
        public static object[] RemoveTempRentDepositInfo(int intDepositTmpSeq, int intDepositSeq, string strCompNo, string strMemNo)
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.DeleteTempRentDepositInfo(intDepositTmpSeq, intDepositSeq, strCompNo, strMemNo);

            return objReturn;
        }

        #endregion

        #region RemoveTempRentFeeInfo : 오피스 / 리테일 임대료 임시저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-17
         * 용       도 : 오피스 / 리테일 임대료 임시저장부분 삭제
         * Input    값 : RemoveTempRentFeeInfo(임시순번, 상세순번)
         * Ouput    값 : Bool
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempRentFeeInfo : 오피스 / 리테일 임대료 임시저장부분 삭제
        /// </summary>
        /// <param name="intRentFeeTmpSeq">임시순번</param>
        /// <param name="intRentFeeSeq">상세순번</param>
        /// <returns></returns>
        public static object[] RemoveTempRentFeeInfo(int intRentFeeTmpSeq, int intRentFeeSeq, string strCompNo, string strMemNo)
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.DeleteTempRentFeeInfo(intRentFeeTmpSeq, intRentFeeSeq, strCompNo, strMemNo);

            return objReturn;
        }

        #endregion

        #region RemoveRentDepoitInfo : 오피스 / 리테일 보증금 저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveRentDepoitInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-28
         * 용       도 : 오피스 / 리테일 보증금 저장부분 삭제
         * Input    값 : RemoveRentDepoitInfo(계약번호, 상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveRentDepoitInfo : 오피스 / 리테일 보증금 저장부분 삭제
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <param name="intDepositSeq">상세순번</param>
        /// <returns></returns>
        public static object[] RemoveRentDepoitInfo(string strContractNo, int intDepositSeq, string strCompNo, string strMemNo)
        {
            object[] objReturn = new object[2];

            objReturn = ContractMngDao.DeleteRentDepoitInfo(strContractNo, intDepositSeq, strCompNo, strMemNo);

            return objReturn;
        }

        #endregion

        #region RemoveRentFeeInfo : 오피스 / 리테일 임대료 저장부분 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveRentFeeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-11-19
         * 용       도 : 오피스 / 리테일 임대료 저장부분 삭제
         * Input    값 : RemoveRentFeeInfo(계약번호, 상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveRentFeeInfo : 오피스 / 리테일 임대료 저장부분 삭제
        /// </summary>
        /// <param name="strContractNo">계약번호</param>
        /// <param name="intRentfeeSeq">상세순번</param>
        /// <returns></returns>
        public static object[] RemoveRentFeeInfo(string strContractNo, int intRentfeeSeq, string strCompNo, string strMemNo)
        {
            object[] objReturns = new object[2];

           // objReturns = ContractMngDao.DeleteRentFeeInfo(strContractNo, intRentfeeSeq, strCompNo, strMemNo);

            return objReturns;
        }

        #endregion

        #region RemoveSaleInfo : 아파트 임대정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveSaleInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-12-13
         * 용       도 : 아파트 임대정보 삭제
         * Input    값 : RemoveRentInfo(임대계약구분코드, 임대계약순번, 삭제사유, 삭제기업코드, 삭제사번, 삭제IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveSaleInfo : 아파트 임대정보 삭제
        /// </summary>
        /// <param name="strRentCd">임대계약구분코드</param>
        /// <param name="intRentSeq">임대계약순번</param>
        /// <param name="strDelReason">삭제사유</param>
        /// <param name="strDelCompCd">삭제기업코드</param>
        /// <param name="strDelMemNo">삭제사번</param>
        /// <param name="strDelIP">삭제IP</param>
        /// <returns></returns>
        public static object[] RemoveSaleInfo(string strRentCd, int intRentSeq, string strDelReason, string strDelCompCd, string strDelMemNo, string strDelIP)
        {
            object[] objReturn = new object[2];

			// 아파트 및 아파트 상가 삭제 정책
			// 1. 임대기간 전 삭제건 발생
			//    백업테이블에 정보 백업 후 삭제
			// 2. 임대기간 시작후 발생
			//    백업테이블에 저장하지 않고 판매일자 어제로 등록

            // KN_USP_RES_DELETE_SALESINFO_M00
            objReturn = ContractMngDao.DeleteSaleInfo(strRentCd, intRentSeq, strDelReason, strDelCompCd, strDelMemNo, strDelIP);

            return objReturn;
        }

        #endregion
        
        #region RemoveRentInfo : 임대정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveRentInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-05
         * 용       도 : 임대정보 삭제
         * Input    값 : RemoveRentInfo(임대계약구분코드, 임대계약순번, 삭제사유, 삭제회사코드, 삭제사번, 삭데IP)
         * Ouput    값 : object[]
         **********************************************************************************************/

        /// <summary>
        /// RemoveRentInfo : 임대정보 삭제
        /// </summary>
        /// <param name="strRentCd">임대계약구분코드</param>
        /// <param name="intRentSeq">임대계약순번</param>
        /// <param name="strDelReason">삭제사유</param>
        /// <param name="strDelMemNo">삭제사번</param>
        /// <param name="strDelIP">삭제IP</param>
        /// <param name="reason"> </param>
        /// <returns></returns>
        public static object[] RemoveRentInfo(string strRentCd, int intRentSeq, string strDelReason, string strDelCompNo, string strDelMemNo, string strDelIP, string reason,string terminateDt)
        {
            object[] objReturns;

            // 리테일 및 오피스 삭제 정책
			// 임대기간에 무관하게 백업후 보증금 목록, 임대료 목록 및 임대정보 테이블 삭제

            // 임대 메인테이블 Backup 처리
            // KN_USP_RES_INSERT_BACKUPRENTINFO_S00
            var dtReturn = ContractMngDao.InsertBackUpRentInfo(strRentCd, intRentSeq, strDelReason, strDelCompNo, strDelMemNo, strDelIP);

            if (dtReturn != null)
            {
                if (dtReturn.Rows.Count > 0)
                {
                    var intRentBackupSeq = 0;

                    intRentBackupSeq = Int32.Parse(dtReturn.Rows[0]["RentBackupSeq"].ToString());
                    var strContract = dtReturn.Rows[0]["ContractNo"].ToString();

                    // 임대 관리비 정보 테이블 Backup 처리
                    // KN_USP_RES_INSERT_BACKUPRENTDEPOSITINFO_M00
                    objReturns = ContractMngDao.InsertBackUpRentDepositInfo(intRentBackupSeq, strContract);

                    if (objReturns != null)
                    {
 
                        // 임대 임대료 정보 테이블 Backup 처리
                        // KN_USP_RES_INSERT_BACKUPRENTFEEINFO_M00
                        objReturns = ContractMngDao.InsertBackUpRentFeeInfo(intRentBackupSeq, strContract);

                        if (objReturns != null)
                        {
                            if (reason.Equals("0002"))
                            {
                                //KN_USP_RES_TERMINATE_RENTINFO_M00
                                objReturns = ContractMngDao.TerminateRentInfo(strRentCd, intRentSeq,terminateDt);
                            }
                            else
                            {
                                // 임대정보 보증금 자료 삭제
                                // KN_USP_RES_DELETE_RENTDEPOSITINFO_M01
                                objReturns = ContractMngDao.DeleteRentDepositionInfo(strContract);

                                if (objReturns != null)
                                {
                                    // 임대정보 임대료 자료 삭제
                                    // KN_USP_RES_DELETE_RENTFEEINFO_M01
                                    objReturns = ContractMngDao.DeleteRentFeeInfo(strContract);

                                    if (objReturns != null)
                                    {
                                        // 임대 메인테이블 삭제
                                        // KN_USP_RES_DELETE_RENTINFO_M00
                                        objReturns = ContractMngDao.DeleteRentInfo(strRentCd, intRentSeq);
                                    }
                                }
                            }

                        }
                    }
                }
                else
                {
                    objReturns = null;
                }
            }
            else
            {
                objReturns = null;
            }

            return objReturns;
        }

        #endregion
    }
}