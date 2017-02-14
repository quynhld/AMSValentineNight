using System.Data;

using KN.Stock.Dac;

namespace KN.Stock.Biz
{
    public class MaterialMngBlo
    {
        #region SpreadGoodsInfo : 자재관리 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadGoodsInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-25
         * 용       도 : 자재관리 목록조회
         * Input    값 : SpreadGoodsInfo(페이지 목록수, 현재 페이지, 구역코드, 서비스존코드, 그룹코드, 메인코드, 서브코드, 코드명, 언어코드, 상태코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadGoodsInfo : 자재관리 목록조회
        /// </summary>
        /// <param name="intPageSize">페이지 목록수</param>
        /// <param name="intNowPage">현재 페이지</param>
        /// <param name="strSectionCd">구역코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <param name="strCdNm">코드명</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strStatusCd">상태코드</param>
        /// <returns></returns>
        public static DataSet SpreadGoodsInfo(int intPageSize, int intNowPage, string strSectionCd, string strSvcZoneCd, string strGrpCd
                                            , string strMainCd, string strSubCd, string strCdNm, string strLangCd, string strStatusCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MaterialMngDao.SelectGoodsInfo(intPageSize, intNowPage, strSectionCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, strCdNm, strLangCd, strStatusCd);

            return dsReturn;
        }

        #endregion

        #region SpreadExgistGoodsInfo : 자재관리 중복 재고조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadExgistGoodsInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-26
         * 용       도 : 자재관리 중복 재고조회
         * Input    값 : SelectExgistGoodsInfo(구역코드, 서비스존코드, 그룹코드, 메인코드, 서브코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadExgistGoodsInfo : 자재관리 목록조회
        /// </summary>
        /// <param name="strSectionCd">구역코드</param>
        /// <param name="stSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <returns></returns>
        public static DataTable SpreadExgistGoodsInfo(string strSectionCd, string stSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialMngDao.SelectExgistGoodsInfo(strSectionCd, stSvcZoneCd, strGrpCd, strMainCd, strSubCd);

            return dtReturn;
        }

        #endregion

        #region WatchGoodsViewInfo : 자재관리 상세조회

        /**********************************************************************************************
         * Mehtod   명 : WatchGoodsViewInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-28
         * 용       도 : 자재관리 상세조회
         * Input    값 : WatchGoodsViewInfo(언어코드, 섹션코드, 서비스존코드, 그룹코드, 메인코드, 서브코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchGoodsViewInfo : 자재관리 목록조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <returns></returns>
        public static DataTable WatchGoodsViewInfo(string strLangCd, string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd)
        {
            DataTable dsReturn = new DataTable();

            dsReturn = MaterialMngDao.SelectGoodsViewInfo(strLangCd, strSectionCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd);

            return dsReturn;
        }

        #endregion

        #region WatchGoodsGraphInfo : 자재관리 그래프조회 (구매양, 구매액)

        /**********************************************************************************************
         * Mehtod   명 : WatchGoodsGraphInfo
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
        public static DataSet WatchGoodsGraphInfo(string strRentCd, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd, string strClassiCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MaterialMngDao.SelectGoodsGraphInfo(strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd, strClassiCd);

            return dsReturn;
        }

        #endregion

        #region SpreadClassiGrpCdInfo : 자재관리 그룹코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadClassiGrpCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-25
         * 용       도 : 자재관리 그룹코드 조회
         * Input    값 : SpreadClassiGrpCdInfo(언어코드, 조회일자, 임대코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadClassiGrpCdInfo : 자재관리 그룹코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strSectionCd">임대코드</param>
        /// <returns></returns>
        public static DataTable SpreadClassiGrpCdInfo(string strLangCd, string strViewDt, string strSectionCd, string strSvcZoneCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialMngDao.SelectClassiGrpCdInfo(strLangCd, strViewDt, strSectionCd, strSvcZoneCd);

            return dtReturn;
        }

        #endregion

        #region SpreadClassiMainCdInfo : 자재관리 메인코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadClassiMainCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-25
         * 용       도 : 자재관리 메인코드 조회
         * Input    값 : SpreadClassiMainCdInfo(언어코드, 조회일자, 임대코드, 서비스존코드, 그룹코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadClassiMainCdInfo : 자재관리 메인코드 조회
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strViewDt">조회일자</param>
        /// <param name="strSectionCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <returns></returns>
        public static DataTable SpreadClassiMainCdInfo(string strLangCd, string strViewDt, string strSectionCd, string strSvcZoneCd, string strGrpCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialMngDao.SelectClassiMainCdInfo(strLangCd, strViewDt, strSectionCd, strSvcZoneCd, strGrpCd);

            return dtReturn;
        }

        #endregion

        #region SpreadClassiSubCdInfo : 자재관리 품목코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadClassiSubCdInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-25
         * 용       도 : 자재관리 품목코드 조회
         * Input    값 : SpreadClassiSubCdInfo(임대코드, 서비스존코드, 그룹코드, 메인코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadClassiSubCdInfo : 자재관리 품목코드 조회
        /// </summary>
        /// <param name="strSectionCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <returns></returns>
        public static DataTable SpreadClassiSubCdInfo(string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialMngDao.SelectClassiSubCdInfo(strSectionCd, strSvcZoneCd, strGrpCd, strMainCd);

            return dtReturn;
        }

        #endregion

        #region WatchGoodsDetailViewInfo : 자재관리 세부현황

        /**********************************************************************************************
         * Mehtod   명 : WatchGoodsDetailViewInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-28
         * 용       도 : 자재관리 세부현황
         * Input    값 : WatchGoodsViewInfo(섹션코드, 서비스존코드, 그룹코드, 메인코드, 서브코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchGoodsViewInfo : 자재관리 목록조회
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <returns></returns>
        public static DataTable WatchGoodsDetailViewInfo(string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd)
        {
            DataTable dsReturn = new DataTable();

            dsReturn = MaterialMngDao.SelectGoodsDetailViewInfo(strSectionCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd);

            return dsReturn;
        }

        #endregion        

        #region SpreadTempChargerInfo : 임시 담당자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadTempChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 임시 담당자 목록 조회
         * Input    값 : SpreadTempChargerInfo(임시번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadTempChargerInfo : 임시 담당자 목록 조회
        /// </summary>
        /// <param name="intTmpSeq">임시번호</param>
        /// <returns></returns>
        public static DataTable SpreadTempChargerInfo(int intTmpSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialMngDao.SelectTempChargerInfo(intTmpSeq);

            return dtReturn;
        }

        #endregion

        #region RegistryGoodsInfo : 자재관리 품목 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryGoodsInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-26
         * 용       도 : 자재관리 품목 등록
         * Input    값 : RegistryGoodsInfo(임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드, 분류명, 기업명, 수량, 단위코드, 원가, 매가, 부가세비율, 
         *                                 부가세포함여부, 긴급가능여부, 비고, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryGoodsInfo : 자재관리 품목 등록
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
        public static object[] RegistryGoodsInfo(string strSectionCd, string strSvcCd, string strClassiGrpCd, string strClassiMainCd, string strClassCd, string strClassNm, string strCompNo,
            int intQty, string strScaleCd, double dblUnitPrimeCost, double dblUnitSellingPrice, double dblVATRatio, string strVATYn, string strEmergencyYn, string strRemark,
            string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = MaterialMngDao.InsertGoodsInfo(strSectionCd, strSvcCd, strClassiGrpCd, strClassiMainCd, strClassCd, strClassNm, strCompNo, intQty, strScaleCd, dblUnitPrimeCost,
                dblUnitSellingPrice, dblVATRatio, strVATYn, strEmergencyYn, strRemark, strCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryTempChargerInfo : 임시 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 임시 담당자 등록
         * Input    값 : RegistryTempChargerInfo(임시순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempChargerInfo : 임시 담당자 등록
        /// </summary>
        /// <param name="intTmpSeq">임시순번</param>
        /// <param name="strChargeMemNo">담당자사번</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static DataTable RegistryTempChargerInfo(int intTmpSeq, string strChargeMemNo, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtRetrun = new DataTable();

            dtRetrun = MaterialMngDao.InsertTempChargerInfo(intTmpSeq, strChargeMemNo, strCompCd, strInsMemNo, strInsMemIP);

            return dtRetrun;
        }

        #endregion

        #region RegistryTempChargerFromReleaseCharger : 현 출고 담당자 임시 담당자 정보로 이동

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempChargerFromReleaseCharger
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 현 출고 담당자 임시 담당자 정보로 이동
         * Input    값 : RegistryTempChargerFromReleaseCharger(임시순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempChargerFromReleaseCharger : 현 출고 담당자 임시 담당자 정보로 이동
        /// </summary>
        /// <returns></returns>
        public static DataTable RegistryTempChargerFromReleaseCharger()
        {
            DataTable dtRetrun = new DataTable();

            dtRetrun = MaterialMngDao.InsertTempChargerFromReleaseCharger();

            return dtRetrun;
        }

        #endregion

        #region RegistryTempChargerFromGoodsOrderCharger : 현 구매 담당자 임시 담당자 정보로 이동

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempChargerFromGoodsOrderCharger
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 현 구매 담당자 임시 담당자 정보로 이동
         * Input    값 : RegistryTempChargerFromGoodsOrderCharger(임시순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempChargerFromGoodsOrderCharger : 현 구매 담당자 임시 담당자 정보로 이동
        /// </summary>
        /// <returns></returns>
        public static DataTable RegistryTempChargerFromGoodsOrderCharger()
        {
            DataTable dtRetrun = new DataTable();

            dtRetrun = MaterialMngDao.InsertTempChargerFromGoodsOrderCharger();

            return dtRetrun;
        }

        #endregion

        #region RegistryReleaseChargerInfo : 출고 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryReleaseChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 출고 담당자 등록
         * Input    값 : RegistryReleaseChargerInfo(담당자순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryReleaseChargerInfo : 출고 담당자 등록
        /// </summary>
        /// <param name="intChargerSeq">담당자순번</param>
        /// <param name="strChargeMemNo">담당자사번</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] RegistryReleaseChargerInfo(int intChargerSeq, string strChargeMemNo, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = MaterialMngDao.InsertReleaseChargerInfo(intChargerSeq, strChargeMemNo, strCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryGoodsOrderChargerInfo : 구매 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryGoodsOrderChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 구매 담당자 등록
         * Input    값 : RegistryGoodsOrderChargerInfo(담당자순번, 담당자사번, 입력사번, 입력IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryGoodsOrderChargerInfo : 구매 담당자 등록
        /// </summary>
        /// <param name="intChargerSeq">담당자순번</param>
        /// <param name="strChargeMemNo">담당자사번</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIP">입력IP</param>
        /// <returns></returns>
        public static object[] RegistryGoodsOrderChargerInfo(int intChargerSeq, string strChargeMemNo, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = MaterialMngDao.InsertGoodsOrderChargerInfo(intChargerSeq, strChargeMemNo, strCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion 

        #region ModifyGoodsInfo : 자재관리 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyGoodsInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-01
         * 용       도 : 자재관리 수정
         * Input    값 : ModifyGoodsInfo(언어코드, 그룹코드, 메인코드, 서브코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 
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
        public static DataTable ModifyGoodsInfo(string strLangCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd, string stcCompNo, int intQty,
            string strScaleCd, double dblUnitPrimeCost, double dblUnitSellingPrice, double dblVATRatio, string strVATYn, string strEmergencyYn, string strRemark,
            string strCompCd, string strModMemNo, string strModMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MaterialMngDao.UpdateGoodsInfo(strLangCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, stcCompNo, intQty, strScaleCd, dblUnitPrimeCost, dblUnitSellingPrice,
                dblVATRatio, strVATYn, strEmergencyYn, strRemark, strCompCd, strModMemNo, strModMemIP);

            return dtReturn;
        }

        #endregion        
        
        #region RemoveTempChargerInfo : 임시 담당자 목록 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 임시 담당자 목록 삭제
         * Input    값 : RemoveTempChargerInfo(임시순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempChargerInfo : 임시 담당자 목록 삭제
        /// </summary>
        /// <param name="intTmpSeq">임시순번</param>
        /// <returns></returns>
        public static object[] RemoveTempChargerInfo(int intTmpSeq)
        {
            object[] objReturn = new object[2];

            objReturn = MaterialMngDao.DeleteTempChargerInfo(intTmpSeq);

            return objReturn;
        }

        #endregion

        #region RemoveReleaseChargerInfo : 기존 출고 담당자 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveReleaseChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 기존 출고 담당자 삭제
         * Input    값 : RemoveReleaseChargerInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveReleaseChargerInfo : 기존 출고 담당자 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveReleaseChargerInfo()
        {
            object[] objReturn = new object[2];

            objReturn = MaterialMngDao.DeleteReleaseChargerInfo();

            return objReturn;
        }

        #endregion

        #region RemoveGoodsOrderChargerInfo : 기존 구매 담당자 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveGoodsOrderChargerInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-06
         * 용       도 : 기존 구매 담당자 삭제
         * Input    값 : RemoveGoodsOrderChargerInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveGoodsOrderChargerInfo : 기존 구매 담당자 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveGoodsOrderChargerInfo()
        {
            object[] objReturn = new object[2];

            objReturn = MaterialMngDao.DeleteGoodsOrderChargerInfo();

            return objReturn;
        }

        #endregion

        #region WatchGoodsViewInfo : 자재관리 상세조회(코드 검색)

        /**********************************************************************************************
         * Mehtod   명 : WatchGoodsViewInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-21
         * 용       도 : 자재관리 상세조회(코드검색)
         * Input    값 : WatchGoodsViewInfo(언어코드, 코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchGoodsViewInfo : 자재관리 목록조회(코드검색)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strSearchCd">검색코드</param>
        /// <returns></returns>
        public static DataTable WatchGoodsViewInfo(string strLangCd, string strSearchCd)
        {
            DataTable dsReturn = new DataTable();

            dsReturn = MaterialMngDao.SelectGoodsViewInfo(strLangCd, strSearchCd);

            return dsReturn;
        }

        #endregion

        #region WatchGoodsViewInfo1 : 자재관리 상세조회(코드명 검색)

        /**********************************************************************************************
         * Mehtod   명 : WatchGoodsViewInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-21
         * 용       도 : 자재관리 상세조회(코드명 검색)
         * Input    값 : WatchGoodsViewInfo(언어코드, 검색명)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchGoodsViewInfo : 자재관리 목록조회(코드명 검색)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strSearchCd">검색명</param>
        /// <returns></returns>
        public static DataTable WatchGoodsViewInfo1(string strLangCd, string strSearchCdNm)
        {
            DataTable dsReturn = new DataTable();

            dsReturn = MaterialMngDao.SelectGoodsViewInfo1(strLangCd, strSearchCdNm);

            return dsReturn;
        }

        #endregion
    }
}
