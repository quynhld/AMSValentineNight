using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Stock.Dac
{
    public class MaterialMngDao
    {
        #region SelectGoodsInfo : 자재관리 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-25
         * 용       도 : 자재관리 목록조회
         * Input    값 : SelectGoodsInfo(페이지 목록수, 현재 페이지, 섹션코드, 서비스존코드, 그룹코드, 메인코드, 서브코드, 코드명, 언어코드, 상태코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsInfo : 자재관리 목록조회
        /// </summary>
        /// <param name="intPageSize">페이지 목록수</param>
        /// <param name="intNowPage">현재 페이지</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <param name="strCdNm">코드명</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strStatusCd">상태코드</param>
        /// <returns></returns>
        public static DataSet SelectGoodsInfo(int intPageSize, int intNowPage, string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd, string strCdNm, string strLangCd, string strStatusCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[10];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSectionCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strGrpCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMainCd));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSubCd));
            objParams[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strCdNm));
            objParams[8] = strLangCd;
            objParams[9] = strStatusCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_GOODSINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectExgistGoodsInfo : 자재관리 중복 재고조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExgistGoodsInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-26
         * 용       도 : 자재관리 중복 재고조회
         * Input    값 : SelectExgistGoodsInfo(섹션코드, 서비스존코드, 그룹코드, 메인코드, 서브코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExgistGoodsInfo : 자재관리 목록조회
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <returns></returns>
        public static DataTable SelectExgistGoodsInfo(string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[5];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSectionCd));
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strGrpCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMainCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSubCd));

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_GOODSINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectGoodsViewInfo : 자재관리 상세조회

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsViewInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-28
         * 용       도 : 자재관리 상세조회
         * Input    값 : SelectGoodsViewInfo(언어코드, 섹션코드, 서비스존코드, 그룹코드, 메인코드, 서브코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsViewInfo : 자재관리 목록조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <returns></returns>
        public static DataTable SelectGoodsViewInfo(string strLangCd, string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd)
        {
            DataTable dsReturn = new DataTable();

            object[] objParams = new object[6];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strLangCd));
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSectionCd));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strGrpCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMainCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSubCd));

            dsReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_GOODSINFO_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectGoodsGraphInfo : 자재관리 그래프조회 (구매양, 구매액)

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsViewInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-04
         * 용       도 : 자재관리 그래프조회 (구매양, 구매액)
         * Input    값 : SelectGoodsGraphInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 자재관리 그래프조회 (구매양, 구매액)
        /// </summary>
        /// <param name="strRentCd"></param>
        /// <param name="strSvcZoneCd"></param>
        /// <param name="strClassiGrpCd"></param>
        /// <param name="strClassiMainCd"></param>
        /// <param name="strClassiCd"></param>
        /// <returns></returns>
        public static DataSet SelectGoodsGraphInfo(string strRentCd, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd, string strClassiCd)
        {
            DataSet dsReturn = new DataSet();
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = strSvcZoneCd;
            objParams[2] = strClassiGrpCd;
            objParams[3] = strClassiMainCd;
            objParams[4] = strClassiCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_RELEASECHARGERINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectClassiGrpCdInfo : 자재관리 그룹코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectClassiGrpCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-25
         * 용       도 : 자재관리 그룹코드 조회
         * Input    값 : SelectClassiGrpCdInfo(언어코드, 조회일자, 임대코드, 서비스존코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectClassiGrpCdInfo : 자재관리 그룹코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strSectionCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <returns></returns>
        public static DataTable SelectClassiGrpCdInfo(string strLangCd, string strViewDt, string strSectionCd, string strSvcZoneCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[4];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;
            objParams[2] = strSectionCd;
            objParams[3] = strSvcZoneCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_CLASSIGRPCD_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectClassiMainCdInfo : 자재관리 메인코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectClassiMainCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-25
         * 용       도 : 자재관리 메인코드 조회
         * Input    값 : SelectClassiMainCdInfo(언어코드, 조회일자, 임대코드, 서비스존코드, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectClassiMainCdInfo : 자재관리 메인코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strSectionCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <returns></returns>
        public static DataTable SelectClassiMainCdInfo(string strLangCd, string strViewDt, string strSectionCd, string strSvcZoneCd, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[5];

            objParams[0] = strLangCd;
            objParams[1] = strViewDt;
            objParams[2] = strSectionCd;
            objParams[3] = strSvcZoneCd;
            objParams[4] = strGrpCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_CLASSIMAINCD_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectClassiSubCdInfo : 자재관리 품목코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectClassiSubCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-25
         * 용       도 : 자재관리 품목코드 조회
         * Input    값 : SelectClassiSubCdInfo(임대코드, 서비스존코드, 그룹코드, 메인코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectClassiSubCdInfo : 자재관리 품목코드 조회
        /// </summary>
        /// <param name="strSectionCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <returns></returns>
        public static DataTable SelectClassiSubCdInfo(string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[4];

            objParams[0] = strSectionCd;
            objParams[1] = strSvcZoneCd;
            objParams[2] = strGrpCd;
            objParams[3] = strMainCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_CLASSISUBCD_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectGoodsDetailViewInfo : 자재관리 세부현황

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsDetailViewInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-01
         * 용       도 : 자재관리 세부현황
         * Input    값 : SelectGoodsDetailViewInfo(섹션코드, 서비스존코드, 그룹코드, 메인코드, 서브코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsViewInfo : 자재관리 목록조회
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <returns></returns>
        public static DataTable SelectGoodsDetailViewInfo(string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd)
        {
            DataTable dsReturn = new DataTable();

            object[] objParams = new object[5];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSectionCd));
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strGrpCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMainCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSubCd));

            dsReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_GOODSINFO_S03", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectTempChargerInfo : 임시 담당자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectTempChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 임시 담당자 목록 조회
         * Input    값 : SelectTempChargerInfo(임시번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectTempChargerInfo : 임시 담당자 목록 조회
        /// </summary>
        /// <param name="intTmpSeq">임시번호</param>
        /// <returns></returns>
        public static DataTable SelectTempChargerInfo(int intTmpSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = intTmpSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_TEMPCHARGERINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertGoodsInfo : 자재관리 품목 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertGoodsInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-26
         * 용       도 : 자재관리 품목 등록
         * Input    값 : InsertGoodsInfo(임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드, 분류명, 기업명, 수량, 단위코드, 원가, 매가, 부가세비율,
         *                               부가세포함여부, 긴급가능여부, 비고, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertGoodsInfo : 자재관리 품목 등록
        /// </summary>
        /// <param name="strSectionCd">임대코드</param>
        /// <param name="strSvcCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <param name="strClassNm">분류명</param>
        /// <param name="strCompNo">기업명</param>
        /// <param name="intQty">수량</param>
        /// <param name="strScaleCd">단위코드</param>
        /// <param name="dblUnitPrimeCost">원가</param>
        /// <param name="dblUnitSellingPrice">매가</param>
        /// <param name="dblVATRatio">부가세비율</param>
        /// <param name="strVATYn">부가세포함여부</param>
        /// <param name="strEmergencyYn">긴급가능여부</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] InsertGoodsInfo(string strSectionCd, string strSvcCd, string strClassiGrpCd, string strClassiMainCd, string strClassCd, string strClassNm, string strCompNo,
            int intQty, string strScaleCd, double dblUnitPrimeCost, double dblUnitSellingPrice, double dblVATRatio, string strVATYn, string strEmergencyYn, string strRemark,
            string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[18];

            objParams[0] = strSectionCd;
            objParams[1] = strSvcCd;
            objParams[2] = strClassiGrpCd;
            objParams[3] = strClassiMainCd;
            objParams[4] = TextLib.MakeNullToEmpty(strClassCd);
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassNm));
            objParams[6] = TextLib.MakeNullToEmpty(strCompNo);
            objParams[7] = intQty;
            objParams[8] = strScaleCd;
            objParams[9] = dblUnitPrimeCost;
            objParams[10] = dblUnitSellingPrice;
            objParams[11] = dblVATRatio;
            objParams[12] = strVATYn;
            objParams[13] = strEmergencyYn;
            objParams[14] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));
            objParams[15] = strCompCd;
            objParams[16] = strInsMemNo;
            objParams[17] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_GOODSINFO_M00", objParams);

            return objReturn;
        }

        #endregion
        
        #region InsertTempChargerInfo : 임시 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTempChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 임시 담당자 등록
         * Input    값 : InsertTempChargerInfo(임시순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempChargerInfo : 임시 담당자 등록
        /// </summary>
        /// <param name="intTmpSeq">임시순번</param>
        /// <param name="strChargeMemNo">담당자사번</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static DataTable InsertTempChargerInfo(int intTmpSeq, string strChargeMemNo, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtRetrun = new DataTable();
            object[] objParams = new object[5];

            objParams[0] = intTmpSeq;
            objParams[1] = strChargeMemNo;
            objParams[2] = strCompCd;
            objParams[3] = strInsMemNo;
            objParams[4] = strInsMemIP;

            dtRetrun = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_TEMPCHARGERINFO_S00", objParams);

            return dtRetrun;
        }

        #endregion      

        #region InsertTempChargerFromReleaseCharger : 현 출고 담당자 임시 담당자 정보로 이동

        /**********************************************************************************************
         * Mehtod   명 : InsertTempChargerFromReleaseCharger
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 현 출고 담당자 임시 담당자 정보로 이동
         * Input    값 : InsertTempChargerFromReleaseCharger(임시순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempChargerFromReleaseCharger : 현 출고 담당자 임시 담당자 정보로 이동
        /// </summary>
        /// <returns></returns>
        public static DataTable InsertTempChargerFromReleaseCharger()
        {
            DataTable dtRetrun = new DataTable();

            dtRetrun = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_TEMPCHARGERINFO_S01");

            return dtRetrun;
        }

        #endregion

        #region InsertTempChargerFromGoodsOrderCharger : 현 구매 담당자 임시 담당자 정보로 이동

        /**********************************************************************************************
         * Mehtod   명 : InsertTempChargerFromGoodsOrderCharger
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 현 구매 담당자 임시 담당자 정보로 이동
         * Input    값 : InsertTempChargerFromGoodsOrderCharger(임시순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertTempChargerFromGoodsOrderCharger : 현 구매 담당자 임시 담당자 정보로 이동
        /// </summary>
        /// <returns></returns>
        public static DataTable InsertTempChargerFromGoodsOrderCharger()
        {
            DataTable dtRetrun = new DataTable();

            dtRetrun = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_TEMPCHARGERINFO_S02");

            return dtRetrun;
        }

        #endregion 

        #region InsertReleaseChargerInfo : 출고 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertReleaseChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 출고 담당자 등록
         * Input    값 : InsertReleaseChargerInfo(담당자순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertReleaseChargerInfo : 출고 담당자 등록
        /// </summary>
        /// <param name="intChargerSeq">담당자순번</param>
        /// <param name="strChargeMemNo">담당자사번</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] InsertReleaseChargerInfo(int intChargerSeq, string strChargeMemNo,string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = intChargerSeq;
            objParams[1] = strChargeMemNo;
            objParams[2] = strCompCd;
            objParams[3] = strInsMemNo;
            objParams[4] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_RELEASECHARGERINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertGoodsOrderChargerInfo : 구매 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertGoodsOrderChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 구매 담당자 등록
         * Input    값 : InsertGoodsOrderChargerInfo(담당자순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertGoodsOrderChargerInfo : 구매 담당자 등록
        /// </summary>
        /// <param name="intChargerSeq">담당자순번</param>
        /// <param name="strChargeMemNo">담당자사번</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] InsertGoodsOrderChargerInfo(int intChargerSeq, string strChargeMemNo, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = intChargerSeq;
            objParams[1] = strChargeMemNo;
            objParams[2] = strCompCd;
            objParams[3] = strInsMemNo;
            objParams[4] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_GOODSORDERCHARGERINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateGoodsInfo : 자재관리 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateGoodsInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-01
         * 용       도 : 자재관리 수정
         * Input    값 : UpdateGoodsInfo(언어코드, 그룹코드, 메인코드, 서브코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateGoodsInfo : 자재관리 수정
        /// </summary>
        /// <param name="strLangCd"></param>
        /// <param name="strSvcZoneCd"></param>
        /// <param name="strGrpCd"></param>
        /// <param name="strMainCd"></param>
        /// <param name="strSubCd"></param>
        /// <param name="stcCompNo"></param>
        /// <param name="intQty"></param>
        /// <param name="strScaleCd"></param>
        /// <param name="dblUnitPrimeCost"></param>
        /// <param name="dblUnitSellingPrice"></param>
        /// <param name="dblVATRatio"></param>
        /// <param name="strVATYn"></param>
        /// <param name="strEmergencyYn"></param>
        /// <param name="strRemark"></param>
        /// <param name="strModMemNo"></param>
        /// <param name="strModMemIP"></param>
        /// <returns></returns>
        public static DataTable UpdateGoodsInfo(string strLangCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd, string stcCompNo, int intQty,
            string strScaleCd, double dblUnitPrimeCost, double dblUnitSellingPrice, double dblVATRatio, string strVATYn, string strEmergencyYn, string strRemark,
            string strCompCd, string strModMemNo, string strModMemIP)
        {
            DataTable dsReturn = new DataTable();

            object[] objParams = new object[17];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strLangCd));
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strGrpCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMainCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSubCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(stcCompNo));
            objParams[6] = intQty;
            objParams[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strScaleCd));
            objParams[8] = dblUnitPrimeCost;
            objParams[9] = dblUnitSellingPrice;
            objParams[10] = dblVATRatio;
            objParams[11] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strVATYn));
            objParams[12] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strEmergencyYn));
            objParams[13] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));
            objParams[14] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strCompCd));
            objParams[15] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strModMemNo));
            objParams[16] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strModMemIP));

            dsReturn = SPExecute.ExecReturnSingle("KN_USP_STK_UPDATE_GOODSINFO_M01", objParams);

            return dsReturn;
        }

        #endregion       

        #region DeleteTempChargerInfo : 임시 담당자 목록 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 임시 담당자 목록 삭제
         * Input    값 : DeleteTempChargerInfo(임시순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempChargerInfo : 임시 담당자 목록 삭제
        /// </summary>
        /// <param name="intTmpSeq">임시순번</param>
        /// <returns></returns>
        public static object[] DeleteTempChargerInfo(int intTmpSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = intTmpSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_TEMPCHARGERINFO_M00", objParams);

            return objReturn;
        }

        #endregion        

        #region DeleteReleaseChargerInfo : 기존 출고 담당자 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteReleaseChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 기존 출고 담당자 삭제
         * Input    값 : DeleteTempChargerInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteReleaseChargerInfo : 기존 출고 담당자 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteReleaseChargerInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_RELEASECHARGERINFO_M00");

            return objReturn;
        }

        #endregion

        #region DeleteGoodsOrderChargerInfo : 기존 구매 담당자 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteGoodsOrderChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 기존 구매 담당자 삭제
         * Input    값 : DeleteGoodsOrderChargerInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteGoodsOrderChargerInfo : 기존 구매 담당자 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteGoodsOrderChargerInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_GOODSORDERCHARGERINFO_M00");

            return objReturn;
        }

        #endregion

        #region SelectGoodsViewInfo : 자재관리 상세조회(코드 검색)

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsViewInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-21
         * 용       도 : 자재관리 상세조회(코드 검색)
         * Input    값 : SelectGoodsViewInfo(언어코드, 코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsViewInfo : 자재관리 목록조회(코드 검색)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strSearchCd">검색코드</param>
        /// <returns></returns>
        public static DataTable SelectGoodsViewInfo(string strLangCd, string strSearchCd)
        {
            DataTable dsReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strLangCd));
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSearchCd));

            dsReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_GOODSINFO_S04", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectGoodsViewInfo1 : 자재관리 상세조회(코드명 검색)

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsViewInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-21
         * 용       도 : 자재관리 상세조회(코드명 검색)
         * Input    값 : SelectGoodsViewInfo(언어코드, 코드명)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsViewInfo : 자재관리 목록조회(코드명 검색)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strSearchCd">코드명</param>
        /// <returns></returns>
        public static DataTable SelectGoodsViewInfo1(string strLangCd, string strSearchCdNm)
        {
            DataTable dsReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strLangCd));
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSearchCdNm));

            dsReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_GOODSINFO_S05", objParams);

            return dsReturn;
        }

        #endregion
    }
}
