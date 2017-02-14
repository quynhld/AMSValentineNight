using System.Data;

using KN.Common.Base.Code;
using KN.Config.Dac;

namespace KN.Config.Biz
{
    public class VatMngBlo
    {
        #region WatchVatInfo : 항목별 부가세율 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-21
         * 용       도 : 항목별 부가세율 조회
         * Input    값 : WatchVatInfo(섹터코드, 기준일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchVatInfo : 최종시점의 환율조회
        /// </summary>
        /// <param name="strRentCd">섹터코드</param>
        /// <param name="strSearchDt">기준일자</param>
        /// <returns></returns>
        public static DataTable WatchVatInfo(string strRentCd, string strSearchDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = VatMngDao.SelectVatInfo(strRentCd, strSearchDt);

            return dtReturn;
        }

        /// <summary>
        /// WatchVatInfo : 최종시점의 환율조회
        /// </summary>
        /// <param name="strRentCd">섹터코드</param>
        /// <returns></returns>
        public static DataTable WatchVatInfo(string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            string strSearchDt = string.Empty;

            dtReturn = VatMngDao.SelectVatInfo(strRentCd, strSearchDt);

            return dtReturn;
        }

        #endregion
        
        #region SpreadExistVatInfo : 부가세 중복조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadExistVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-19
         * 용       도 : 부가세 중복조회
         * Input    값 : SpreadExistVatInfo(strVatCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadExistVatInfo : 부가세 중복조회
        /// </summary>
        /// <param name="strVatCd">부가세종류</param>
        /// <returns></returns>
        public static DataTable SpreadExistVatInfo(string strVatCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = VatMngDao.SelectExistVatInfo(strVatCd);

            return dtReturn;
        }

        #endregion

        #region WatchVatDetailInfo : 부가세율 상세조회

        /**********************************************************************************************
         * Mehtod   명 : WatchVatDetailInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-20
         * 용       도 : 부가세율 상세조회
         * Input    값 : WatchVatDetailInfo(적용일, 부가세종류)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchVatDetailInfo : 부가세율 상세조회
        /// </summary>
        /// <param name="strPageNm">접근페이지명</param>
        /// <param name="strFeeTy">관리비/임대료타입</param>
        /// <param name="strChargeTy">전기요금/수도요금타입</param>
        /// <param name="strNowDt">비교기준일</param>
        /// <returns></returns>
        public static DataTable WatchVatDetailInfo(string strPageNm, string strFeeTy, string strChargeTy, string strNowDt)
        {
            DataTable dtReturn = new DataTable();
            string strVatCd = string.Empty;

            if (strPageNm.ToUpper().Equals(CommValue.PAGE_VALUE_MNGFEELIST.ToUpper()) ||
                strPageNm.ToUpper().Equals(CommValue.PAGE_VALUE_MNGFEEVIEW.ToUpper()) ||
                strPageNm.ToUpper().Equals(CommValue.PAGE_VALUE_MNGFEEITEMWRITE.ToUpper()) ||
                strPageNm.ToUpper().Equals(CommValue.PAGE_VALUE_MNGFEEITEMVIEW.ToUpper()))
            {
                // 관리비 혹은 임대료항목
                if (strFeeTy.Equals(CommValue.FEETY_VALUE_MNGFEE))
                {
                    // 관리비 처리(ITEM_TYPE_VALUE_MNGFEE)
                    strVatCd = CommValue.ITEM_TYPE_VALUE_MNGFEE;
                }
                else if (strFeeTy.Equals(CommValue.FEETY_VALUE_RENTALFEE))
                {
                    // 임대료처리(ITEM_TYPE_VALUE_RENTALFEE)
                    strVatCd = CommValue.ITEM_TYPE_VALUE_RENTALFEE;
                }
            }
            else if (strPageNm.ToUpper().Equals(CommValue.PAGE_VALUE_LATEFEELIST.ToUpper()) ||
                strPageNm.ToUpper().Equals(CommValue.PAGE_VALUE_LATEFEEVIEW.ToUpper()))
            {
                // 관리비연체료 혹은 임대료연체료항목
                if (strFeeTy.Equals(CommValue.FEETY_VALUE_MNGFEE))
                {
                    // 관리비연체료 처리(ITEM_TYPE_VALUE_LATEMNGFEE)
                    strVatCd = CommValue.ITEM_TYPE_VALUE_LATEMNGFEE;
                }
                else if (strFeeTy.Equals(CommValue.FEETY_VALUE_RENTALFEE))
                {
                    // 임대료연체료 처리(ITEM_TYPE_VALUE_LATERENTALFEE)
                    strVatCd = CommValue.ITEM_TYPE_VALUE_LATERENTALFEE;
                }
            }
            else
            {
                // 전기요금 및 수도요금 또는 주차매출
                if (strChargeTy.Equals(CommValue.CHARGETY_VALUE_ELECTRICITY))
                {
                    // 전기요금처리(ITEM_TYPE_VALUE_ELECCHARGE)
                    strVatCd = CommValue.ITEM_TYPE_VALUE_ELECCHARGE;
                }
                else if (strChargeTy.Equals(CommValue.CHARGETY_VALUE_WATER))
                {
                    // 수도요금처리(ITEM_TYPE_VALUE_WATERCHARGE)
                    strVatCd = CommValue.ITEM_TYPE_VALUE_WATERCHARGE;
                }
                else
                {
                    // 주차매출 (ITEM_TYPE_VALUE_PARKINGFEE)
                    strVatCd = CommValue.ITEM_TYPE_VALUE_PARKINGFEE;
                }
            }

            dtReturn = VatMngDao.SelectVatDetailInfo(strNowDt, strVatCd);

            return dtReturn;
        }

        #endregion

        #region SpreadVatInfo : 부가세 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-18
         * 용       도 : 부가세 조회
         * Input    값 : SpreadVatInfo(페이지별 리스트 크기, 현재페이지, 언어코드, 부가세종류)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadVatInfo : 부가세 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strVatCd">부가세종류</param>
        /// <returns></returns>
        public static DataSet SpreadVatInfo(int intPageSize, int intNowPage, string strLangCd, string strVatCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = VatMngDao.SelectVatInfo(intPageSize, intNowPage, strLangCd, strVatCd);

            return dsReturn;
        }

        #endregion

        #region RegistryVatInfo : 부가세 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-18
         * 용       도 : 부가세 등록
         * Input    값 : RegistryVatInfo(부가세종류코드, 적용시작일자, 부가세율, 등록회사코드, 등록사번, 등록IP)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 부가세 등록
        /// </summary>
        /// <param name="strVatCd">부가세종류코드</param>
        /// <param name="strStartDt">적용시작일자</param>
        /// <param name="dblVatRatio">부가세율</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strIp">등록IP</param>
        /// <returns></returns>
        public static object[] RegistryVatInfo(string strVatCd, string strStartDt, double dblVatRatio, string strInsCompNo, string strInsMemNo, string strIp)
        {
            object[] objReturn = new object[2];

            objReturn = VatMngDao.InsertVatInfo(strVatCd, strStartDt, dblVatRatio, strInsCompNo, strInsMemNo, strIp);

            return objReturn;
        }

        #endregion

        #region ModifyVatInfo : 부가세 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyVatInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-19
         * 용       도 : 부가세 수정
         * Input    값 : ModifyVatInfo(부가세종류코드, 적용시작일자, 부가세율, 등록회사코드, 등록사번, 등록IP)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 부가세 등록
        /// </summary>
        /// <param name="strVatCd">부가세종류코드</param>
        /// <param name="strStartDt">적용시작일자</param>
        /// <param name="dblVatRatio">부가세율</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strIp">등록IP</param>
        /// <returns></returns>
        public static object[] ModifyVatInfo(string strVatCd, string strStartDt, double dblVatRatio, string strInsCompNo, string strInsMemNo, string strIp)
        {
            object[] objReturn = new object[2];

            objReturn = VatMngDao.UpdateVatInfo(strVatCd, strStartDt, dblVatRatio, strInsCompNo, strInsMemNo, strIp);

            return objReturn;
        }

        #endregion
    }
}
