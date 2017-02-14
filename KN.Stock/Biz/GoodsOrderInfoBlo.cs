using System.Data;

using KN.Stock.Dac;

namespace KN.Stock.Biz
{
    public class GoodsOrderInfoBlo
    {
        #region SpreadGoodsOrderInfo : 발주요청 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-29
         * 용       도 : 발주요청 목록조회
         * Input    값 : SpreadGoodsOrderInfo(페이지 목록수, 현재 페이지, 섹션코드, 서비스존코드, 그룹코드, 메인코드,
         *                                    서브코드, 코드명, 언어코드, 처리과정코드, 상태코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadGoodsOrderInfo : 발주요청 목록조회
        /// </summary>
        /// <param name="intPageSize">페이지 목록수</param>
        /// <param name="intNowPage">현재 페이지</param>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <param name="strCdNm">코드명</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strProcessCd">처리과정코드</param>
        /// <param name="strStatusCd">상태코드</param>
        /// <returns></returns>
        public static DataSet SpreadGoodsOrderInfo(int intPageSize, int intNowPage, string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd
            , string strCdNm, string strLangCd, string strProcessCd, string strStatusCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = GoodsOrderInfoDao.SelectGoodsOrderInfo(intPageSize, intNowPage, strSectionCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, strCdNm, strLangCd, strProcessCd, strStatusCd);

            return dsReturn;
        }

        #endregion

        #region WatchGoodsOrderDetailInfo : 구매요청 상세조회

        /**********************************************************************************************
         * Mehtod   명 : WatchGoodsOrderDetailInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-29
         * 용       도 : 구매요청 상세조회
         * Input    값 : SelectGoodsOrderDetailInfo(구매순번, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchGoodsOrderDetailInfo : 구매요청 상세조회
        /// </summary>
        /// <param name="strOrderSeq">구매순번</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet WatchGoodsOrderDetailInfo(string strOrderSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = GoodsOrderInfoDao.SelectGoodsOrderDetailInfo(strOrderSeq, strLangCd);

            return dsReturn;
        }

        #endregion

        #region SpreadTempGoodsOrderInfo : 발주요청 임시 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 발주요청 임시 목록조회
         * Input    값 : SpreadTempGoodsOrderInfo(임시순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadTempGoodsOrderInfo : 발주요청 임시 목록조회
        /// </summary>
        /// <param name="intTmpGoodsOrderSeq">임시순번</param>
        /// <returns></returns>
        public static DataTable SpreadTempGoodsOrderInfo(int intTmpGoodsOrderSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = GoodsOrderInfoDao.SelectTempGoodsOrderInfo(intTmpGoodsOrderSeq);

            return dtReturn;
        }

        #endregion

        #region SpreadGoodsOrderChargeInfo : 발주 담당자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadGoodsOrderChargeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 발주 담당자 목록 조회
         * Input    값 : SpreadGoodsOrderChargeInfo(조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadGoodsOrderChargeInfo : 발주 담당자 목록 조회
        /// </summary>
        /// <param name="strNowDt">조회일자</param>
        /// <returns></returns>
        public static DataTable SpreadGoodsOrderChargeInfo(string strNowDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = GoodsOrderInfoDao.SelectGoodsOrderChargeInfo(strNowDt);

            return dtReturn;
        }

        #endregion

        #region ModifyGoodsInfo : 물품 보유수 변경

        /**********************************************************************************************
         * Mehtod   명 : ModifyGoodsInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 물품 보유수 변경
         * Input    값 : ModifyGoodsInfo(섹션코드, 서비스존코드, 그룹코드, 메인코드, 서브코드, 코드명, 수량, 회원번호, 회원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyGoodsInfo : 물품 보유수 변경
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strGrpCd">그룹코드</param>
        /// <param name="strMainCd">메인코드</param>
        /// <param name="strSubCd">서브코드</param>
        /// <param name="intQty">수량</param>
        /// <param name="strMemNo">회원번호</param>
        /// <param name="strMemIP">회원IP</param>
        /// <returns></returns>
        public static object[] ModifyGoodsInfo(string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd, int intQty, string strCompCd, string strMemNo, string strMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.UpdateGoodsInfo(strSectionCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, intQty, strCompCd, strMemNo, strMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyTempGoodsOrderInfo : 발주요청 임시 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 임시 정보 수정
         * Input    값 : ModifyTempGoodsOrderInfo(임시순번, 임시상세순번, 수량)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyTempGoodsOrderInfo : 발주요청 임시 정보 수정
        /// </summary>
        /// <param name="intTempGoodsOrderSeq">임시순번</param>
        /// <param name="intTempGoodsOrderDetSeq">임시상세순번</param>
        /// <param name="intQty">수량</param>
        /// <returns></returns>

        public static object[] ModifyTempGoodsOrderInfo(int intTempGoodsOrderSeq, int intTempGoodsOrderDetSeq, int intQty)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.UpdateTempGoodsOrderInfo(intTempGoodsOrderSeq, intTempGoodsOrderDetSeq, intQty);

            return objReturn;
        }

        #endregion

        #region ModifyGoodsOrderConfirm : 발주승인 등록

        /**********************************************************************************************
         * Mehtod   명 : ModifyGoodsOrderConfirm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-29
         * 용       도 : 발주승인 등록
         * Input    값 : ModifyGoodsOrderConfirm(발주순번, 발주상세순번, 담당자순번, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyGoodsOrderConfirm : 발주승인 등록
        /// </summary>
        /// <param name="strOrderSeq">발주순번</param>
        /// <param name="intOrderDetSeq">발주상세순번</param>
        /// <param name="intChargeSeq">담당자순번</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] ModifyGoodsOrderConfirm(string strOrderSeq, int intOrderDetSeq, int intChargeSeq, string strRemark)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.UpdateGoodsOrderConfirm(strOrderSeq, intOrderDetSeq, intChargeSeq, strRemark);

            return objReturn;
        }

        #endregion

        #region ModifyGooodsOrderDeny : 발주요청반려 등록

        /**********************************************************************************************
         * Mehtod   명 : UpdateGooodsOrderDeny
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-02
         * 용       도 : 발주반려 등록
         * Input    값 : ModifyGooodsOrderDeny(발주요청순번, 발주요청상세순번, 담당자순번, 비고, 발주상태코드, 승인여부코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyGooodsOrderDeny : 발주요청반려 등록
        /// </summary>
        /// <param name="strOrderSeq">발주요청순번</param>
        /// <param name="intOrderDetSeq">발주요청상세순번</param>
        /// <param name="intChargeSeq">담당자순번</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strProcessCd">발주상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <returns></returns>
        public static object[] ModifyGooodsOrderDeny(string strOrderSeq, int intOrderDetSeq, int intChargeSeq, string strRemark, string strProcessCd, string strStateCd)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.UpdateGooodsOrderDeny(strOrderSeq, intOrderDetSeq, intChargeSeq, strRemark);

            if (objReturn != null)
            {
                objReturn = GoodsOrderInfoDao.UpdateGoodsOrderItem(strOrderSeq, intOrderDetSeq, strProcessCd, strStateCd);
            }

            return objReturn;
        }

        #endregion

        #region ModifyGoodsOrderItem : 발주상태값 수정 (품목별)

        /**********************************************************************************************
         * Mehtod   명 : ModifyGoodsOrderItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-29
         * 용       도 : 발주상태값 수정 (품목별)
         * Input    값 : UpdateGoodsOrder(발주순번, 발주상세순번, 발주관련상태코드, 승인여부코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyGoodsOrderItem : 발주상태값 수정 (품목별)
        /// </summary>
        /// <param name="strOrderSeq">발주순번</param>
        /// <param name="intOrderDetSeq">발주상세순번</param>
        /// <param name="strProcessCd">발주관련상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <returns></returns>
        public static object[] ModifyGoodsOrderItem(string strOrderSeq, int intOrderDetSeq, string strProcessCd, string strStateCd)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.UpdateGoodsOrderItem(strOrderSeq, intOrderDetSeq, strProcessCd, strStateCd);

            return objReturn;
        }

        #endregion

        #region ModifyGoodsOrderBasicInfo : 구매요청 기본 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyGoodsOrderBasicInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-03
         * 용       도 : 구매요청 기본 정보 수정
         * Input    값 : ModifyGoodsOrderBasicInfo(구매순번, 구매일자, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyGoodsOrderBasicInfo : 구매요청 기본 정보 수정
        /// </summary>
        /// <param name="strReleaseSeq">구매순번</param>
        /// <param name="strProcessDt">구매일자</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] ModifyGoodsOrderBasicInfo(string strReleaseSeq, string strProcessDt, string strRemark)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.UpdateGoodsOrderBasicInfo(strReleaseSeq, strProcessDt, strRemark);

            return objReturn;
        }

        #endregion

        #region RemoveLatestTempGoodOrderInfo : 발주요청 최근 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveLatestTempGoodOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 발주요청 최근 임시 정보 삭제
         * Input    값 : RemoveLatestTempGoodOrderInfo()
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// RemoveLatestTempGoodOrderInfo : 발주요청 최근 임시 정보 삭제
        /// </summary>
        public static object[] RemoveLatestTempGoodOrderInfo()
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.DeleteLatestTempGoodOrderInfo();
            return objReturn;
        }

        #endregion

        #region RemoveTempGoodsOrderInfo : 발주요청 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 임시 정보 삭제
         * Input    값 : RemoveTempGoodsOrderInfo(임시순번, 임시상세순번)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempGoodsOrderInfo : 출고요청 임시 정보 삭제
        /// </summary>
        /// <param name="intTempGoodsOrderSeq">임시순번</param>
        /// <param name="intTempGoodsOrderDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] RemoveTempGoodsOrderInfo(int intTempGoodsOrderSeq, int intTempGoodsOrderDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intTempGoodsOrderSeq;
            objParams[1] = intTempGoodsOrderDetSeq;

            objReturn = GoodsOrderInfoDao.DeleteTempGoodsOrderInfo(intTempGoodsOrderSeq, intTempGoodsOrderDetSeq);

            return objReturn;
        }

        #endregion

        #region RegistryTempGoodsOrderInfo : 발주요청 임시등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 발주요청 임시등록
         * Input    값 : RegistryTempGoodsOrderInfo(임시순번, 임시상세순번, 임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempGoodsOrderInfo : 출고요청 임시등록
        /// </summary>
        /// <param name="intTmpGoodsOrderSeq">임시순번</param>
        /// <param name="intTmpGoodsOrderDetSeq">임시상세순번</param>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <returns></returns>
        public static DataTable RegistryTempGoodsOrderInfo(int intTmpGoodsOrderSeq, int intTmpGoodsOrderDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd
            , string strClassiMainCd, string strClassCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = GoodsOrderInfoDao.insertTempGoodsOrderInfo(intTmpGoodsOrderSeq, intTmpGoodsOrderDetSeq, strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd, strClassCd);

            return dtReturn;
        }

        #endregion

        #region RegistryTempGoodsOrderInfo : 발주요청 임시등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 임시등록
         * Input    값 : RegistryTempGoodsOrderInfo(임시순번, 임시상세순번, 임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드, 수량, 보유수량, 자동승인여부)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempGoodsOrderInfo : 발주요청 임시등록
        /// </summary>
        /// <param name="intTmpReleaseSeq">임시순번</param>
        /// <param name="intTmpReleaseDetSeq">임시상세순번</param>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <param name="intQty">수량</param>
        /// <param name="intHaveQty">보유수량</param>
        /// <param name="strApproval">자동승인여부</param>
        /// <returns></returns>
        public static DataTable RegistryTempGoodsOrderInfo(int intTmpGoodsOrderSeq, int intTmpGoodsOrderDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd
            , string strClassiMainCd, string strClassCd, int intQty, int intHaveQty, string strApproval)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = GoodsOrderInfoDao.insertTempGoodsOrderInfo(intTmpGoodsOrderSeq, intTmpGoodsOrderDetSeq, strRentCd, strSvcZoneCd, strClassiGrpCd
            , strClassiMainCd, strClassCd, intQty, intHaveQty, strApproval);

            return dtReturn;
        }

        #endregion

        #region RegistryGoodsOrderInfo : 발주요청 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryGoodsOrderInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 등록
         * Input    값 : RegistryGoodsOrderInfo(순번, 상세순번, 임대코드, 분류그룹코드, 분류메인코드, 분류코드, 순번
         *                                          , 상세순번, 임대코드, 분류그룹코드, 분류메인코드, 분류코드, 출고부서코드
         *                                          , 출고사번, 출고일자, 수량, 발생비용, 진행코드, 상황코드, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryGoodsOrderInfo : 발주요청 임시등록
        /// </summary>
        /// <param name="strOrderSeq">순번</param>
        /// <param name="intOrderDetSeq">상세순번</param>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <param name="strProcessDeptCd">출고부서코드</param>
        /// <param name="strProcessMemNo">출고사번</param>
        /// <param name="strProcessDt">출고일자</param>
        /// <param name="intQty">수량</param>
        /// <param name="strRemark">비고</param>
        /// <param name="dblProcessFee">발생비용</param>
        /// <param name="strProcessRemark">비용발생사유</param>
        /// <param name="strProgressCd">진행코드</param>
        /// <param name="strStateCd">상황코드</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strReleaseSeq">출고신청순번</param>
        /// <param name="intReleaseDetSeq">출고신청상세순번</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>

        public static DataTable RegistryGoodsOrderInfo(string strOrderSeq, int intOrderDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd,
            string strClassCd, string strProcessDeptCd, string strProcessCompCd, string strProcessMemNo, string strProcessDt, int intQty, string strRemark,
            string strProgressCd, string strStateCd, string strApprovalYn, string strReleaseSeq, int intReleaseDetSeq, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = GoodsOrderInfoDao.InsertGoodsOrderInfo(strOrderSeq, intOrderDetSeq, strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd
                , strClassCd, strProcessDeptCd, strProcessCompCd,  strProcessMemNo, strProcessDt, intQty, strRemark, strProgressCd
                , strStateCd, strApprovalYn, strReleaseSeq, intReleaseDetSeq, strCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryTmpGoodsOrderAddon : 발주 임시 승인 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTmpGoodsOrderAddon
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주 임시 승인 담당자 등록
         * Input    값 : RegistryTmpGoodsOrderAddon(순번, 상세순번, 임시담당자순번, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryTmpReleaseRequestAddon : 발주 임시 승인 담당자 등록
        /// </summary>
        /// <param name="intOrderDetSeq">순번</param>
        /// <param name="intOrderDetSeq">상세순번</param>
        /// <param name="intTempSeq">임시담당자순번</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] RegistryTmpGoodsOrderAddon(string strOrderSeq, int intOrderDetSeq, int intTempSeq, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.InsertTmpGoodsInfoAddon(strOrderSeq, intOrderDetSeq, intTempSeq, strApprovalYn, strCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryGoodsOrderAddon : 발주 승인 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryGoodsOrderAddon
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주 승인 담당자 등록
         * Input    값 : RegistryGoodsOrderAddon(순번, 상세순번, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryGoodsOrderAddon : 발주 승인 담당자 등록
        /// </summary>
        /// <param name="strOrderSeq">순번</param>
        /// <param name="intOrderDetSeq">상세순번</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] RegistryGoodsOrderAddon(string strOrderSeq, int intOrderDetSeq, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.InsertGoodsOrderAddon(strOrderSeq, intOrderDetSeq, strApprovalYn, strCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RemoveTempGoodsOrderInfo : 발주요청 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 임시 정보 삭제
         * Input    값 : RemoveTempGoodsOrderInfo(임시순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempGoodsOrderInfo : 발주요청 임시 정보 삭제
        /// </summary>
        /// <param name="intTempGoodsOrderSeq">임시순번</param>
        /// <returns></returns>

        public static object[] RemoveTempGoodsOrderInfo(int intTempGoodsOrderSeq)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.DeleteTempGoodsOrderInfo(intTempGoodsOrderSeq);

            return objReturn;
        }

        #endregion

        #region SpreadGoodsOrderAddon : 최하위 발주 담당자 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadGoodsOrderAddon
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-15
         * 용       도 : 최하위 발주 담당자 조회
         * Input    값 : SpreadGoodsOrderAddon(출고신청번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadGoodsOrderAddon : 발주 담당자 목록 조회
        /// </summary>
        /// <param name="strReleaseSeq">발주신청번호</param>
        /// <returns></returns>
        public static DataTable SpreadGoodsOrderAddon(string strOrderSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = GoodsOrderInfoDao.SelectGoodsOrderAddon(strOrderSeq);

            return dtReturn;
        }

        #endregion

        #region RemoveGoodsOrderMng : 발주요청 관련 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveGoodsOrderMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-02
         * 용       도 : 발주요청 관련 정보 삭제
         * Input    값 : DeleteGoodsOrderMng(발주순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveGoodsOrderMng : 발주요청 관련 정보 삭제
        /// </summary>
        /// <param name="strReleaseSeq">발주순번</param>
        /// <returns></returns>
        public static object[] RemoveGoodsOrderMng(string strOrderSeq)
        {
            object[] objReturn = new object[2];

            objReturn = GoodsOrderInfoDao.DeleteGoodsOrderMng(strOrderSeq);

            return objReturn;
        }

        #endregion

    }
}