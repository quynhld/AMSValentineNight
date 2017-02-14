using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Stock.Dac
{
    public class GoodsOrderInfoDao
    {
        #region SelectGoodsOrderInfo : 발주요청 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-29
         * 용       도 : 발주요청 목록조회
         * Input    값 : SelectGoodsOrderInfo(페이지 목록수, 현재 페이지, 섹션코드, 서비스존코드, 그룹코드, 메인코드,
         *                                    서브코드, 코드명, 언어코드, 처리과정코드, 상태코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsOrderInfo : 발주요청 목록조회
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
        public static DataSet SelectGoodsOrderInfo(int intPageSize, int intNowPage, string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd
            , string strCdNm, string strLangCd, string strProcessCd, string strStatusCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[11];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSectionCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strGrpCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMainCd));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSubCd));
            objParams[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strCdNm));
            objParams[8] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strLangCd));
            objParams[9] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strProcessCd));
            objParams[10] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strStatusCd));

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_GOODSORDERINFO_S00", objParams);

            return dsReturn;
        }

        #endregion
        
        #region SelectGoodsOrderDetailInfo : 구매요청 상세조회

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsOrderDetailInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-29
         * 용       도 : 구매요청 상세조회
         * Input    값 : SelectGoodsOrderDetailInfo(구매순번, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsOrderDetailInfo : 구매요청 상세조회
        /// </summary>
        /// <param name="strOrderSeq">구매순번</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectGoodsOrderDetailInfo(string strOrderSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[2];

            objParams[0] = strOrderSeq;
            objParams[1] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_GOODSORDERINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectTempGoodsOrderInfo : 발주요청 임시 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SelectTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 발주요청 임시 목록조회
         * Input    값 : SelectTempGoodsOrderInfo(임시순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectTempGoodsOrderInfo : 발주요청 임시 목록조회
        /// </summary>
        /// <param name="intTmpGoodsOrderSeq">임시순번</param>
        /// <returns></returns>
        public static DataTable SelectTempGoodsOrderInfo(int intTmpGoodsOrderSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = intTmpGoodsOrderSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_TMPGOODSORDERINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectGoodsOrderChargeInfo : 발주 담당자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsOrderChargeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 발주 담당자 목록 조회
         * Input    값 : SelectGoodsOrderChargeInfo(조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsOrderChargeInfo : 발주 담당자 목록 조회
        /// </summary>
        /// <param name="strNowDt">조회일자</param>
        /// <returns></returns>
        public static DataTable SelectGoodsOrderChargeInfo(string strNowDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strNowDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_GOODSORDERCHARGERINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdateGoodsInfo : 물품 보유수 변경

        /**********************************************************************************************
         * Mehtod   명 : UpdateGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 물품 보유수 변경
         * Input    값 : UpdateGoodsOrderInfo(섹션코드, 서비스존코드, 그룹코드, 메인코드, 서브코드, 코드명, 수량, 회원번호, 회원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateGoodsInfo : 물품 보유수 변경
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
        public static object[] UpdateGoodsInfo(string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd, int intQty,string strCompCd, string strMemNo, string strMemIP) 
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[9];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSectionCd));
            objParams[1] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strGrpCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strMainCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSubCd));
            objParams[5] = intQty;
            objParams[6] = strCompCd;
            objParams[7] = strMemNo;
            objParams[8] = strMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_GOODSINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateTempGoodsOrderInfo : 발주요청 임시 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 임시 정보 수정
         * Input    값 : UpdateTempGoodsOrderInfo(임시순번, 임시상세순번, 수량)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateTempGoodsOrderInfo : 발주요청 임시 정보 수정
        /// </summary>
        /// <param name="intTempGoodsOrderSeq">임시순번</param>
        /// <param name="intTempGoodsOrderDetSeq">임시상세순번</param>
        /// <param name="intQty">수량</param>
        /// <returns></returns>

        public static object[] UpdateTempGoodsOrderInfo(int intTempGoodsOrderSeq, int intTempGoodsOrderDetSeq, int intQty)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[3];

            objParams[0] = intTempGoodsOrderSeq;
            objParams[1] = intTempGoodsOrderDetSeq;
            objParams[2] = intQty;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_TMPGOODSORDERINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateGoodsOrderConfirm : 발주승인 등록

        /**********************************************************************************************
         * Mehtod   명 : UpdateGoodsOrderConfirm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-29
         * 용       도 : 발주승인 등록
         * Input    값 : UpdateGoodsOrderConfirm(발주순번, 발주상세순번, 담당자순번, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateGoodsOrderConfirm : 발주승인 등록
        /// </summary>
        /// <param name="strOrderSeq">발주순번</param>
        /// <param name="intOrderDetSeq">발주상세순번</param>
        /// <param name="intChargeSeq">담당자순번</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] UpdateGoodsOrderConfirm(string strOrderSeq, int intOrderDetSeq, int intChargeSeq, string strRemark)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = intChargeSeq;
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_GOODSORDERCONFIRM_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateGooodsOrderDeny : 발주요청반려 등록

        /**********************************************************************************************
         * Mehtod   명 : UpdateGooodsOrderDeny
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-02
         * 용       도 : 발주반려 등록
         * Input    값 : UpdateGooodsOrderDeny(발주요청순번, 발주요청상세순번, 담당자순번, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateGooodsOrderDeny : 발주요청반려 등록
        /// </summary>
        /// <param name="strOrderSeq">발주요청순번</param>
        /// <param name="intOrderDetSeq">발주요청상세순번</param>
        /// <param name="intChargeSeq">담당자순번</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] UpdateGooodsOrderDeny(string strOrderSeq, int intOrderDetSeq, int intChargeSeq, string strRemark)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = intChargeSeq;
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_GOODSORDERDENY_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateGoodsOrderItem : 발주상태값 수정 (품목별)

        /**********************************************************************************************
         * Mehtod   명 : UpdateGoodsOrderItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-29
         * 용       도 : 발주상태값 수정 (품목별)
         * Input    값 : UpdateGoodsOrder(발주순번, 발주상세순번, 발주관련상태코드, 승인여부코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateGoodsOrderItem : 발주상태값 수정 (품목별)
        /// </summary>
        /// <param name="strOrderSeq">발주순번</param>
        /// <param name="intOrderDetSeq">발주상세순번</param>
        /// <param name="strProcessCd">발주관련상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <returns></returns>
        public static object[] UpdateGoodsOrderItem(string strOrderSeq, int intOrderDetSeq, string strProcessCd, string strStateCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = strProcessCd;
            objParams[3] = strStateCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_GOODSORDERINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateGoodsOrderBasicInfo : 구매요청 기본 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateGoodsOrderBasicInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-03
         * 용       도 : 구매요청 기본 정보 수정
         * Input    값 : UpdateGoodsOrderBasicInfo(구매순번, 구매일자, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateGoodsOrderBasicInfo : 구매요청 기본 정보 수정
        /// </summary>
        /// <param name="strReleaseSeq">구매순번</param>
        /// <param name="strProcessDt">구매일자</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] UpdateGoodsOrderBasicInfo(string strReleaseSeq, string strProcessDt, string strRemark)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[3];

            objParams[0] = strReleaseSeq;
            objParams[1] = strProcessDt;
            objParams[2] = strRemark;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_GOODSORDERINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteLatestTempGoodOrderInfo : 발주요청 최근 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteLatestTempGoodOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 발주요청 최근 임시 정보 삭제
         * Input    값 : DeleteLatestTempGoodOrderInfo()
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// DeleteLatestTempGoodOrderInfo : 발주요청 최근 임시 정보 삭제
        /// </summary>
        public static object[] DeleteLatestTempGoodOrderInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_TMPGOODSORDERINFO_M00");

            return objReturn;
        }

        #endregion

        #region DeleteTempGoodsOrderInfo : 발주요청 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 임시 정보 삭제
         * Input    값 : DeleteTempGoodsOrderInfo(임시순번, 임시상세순번)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempGoodsOrderInfo : 발주요청 임시 정보 삭제
        /// </summary>
        /// <param name="intTempGoodsOrderSeq">임시순번</param>
        /// <param name="intTempGoodsOrderDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] DeleteTempGoodsOrderInfo(int intTempGoodsOrderSeq, int intTempGoodsOrderDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intTempGoodsOrderSeq;
            objParams[1] = intTempGoodsOrderDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_TMPGOODSORDERINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region insertTempGoodsOrderInfo : 발주요청 임시등록

        /**********************************************************************************************
         * Mehtod   명 : insertTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 발주요청 임시등록
         * Input    값 : insertTempGoodsOrderInfo(임시순번, 임시상세순번, 임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// insertTempGoodsOrderInfo : 발주요청 임시등록
        /// </summary>
        /// <param name="intTmpGoodsOrderSeq">임시순번</param>
        /// <param name="intTmpGoodsOrderDetSeq">임시상세순번</param>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <returns></returns>
        public static DataTable insertTempGoodsOrderInfo(int intTmpGoodsOrderSeq, int intTmpGoodsOrderDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd
            , string strClassiMainCd, string strClassCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[7];

            objParams[0] = intTmpGoodsOrderSeq;
            objParams[1] = intTmpGoodsOrderDetSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRentCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassiGrpCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassiMainCd));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassCd));

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_TMPGOODSORDERINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region insertTempGoodsOrderInfo : 발주요청 임시등록

        /**********************************************************************************************
         * Mehtod   명 : insertTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 임시등록
         * Input    값 : insertTempGoodsOrderInfo(임시순번, 임시상세순번, 임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드, 수량, 보유수량, 자동승인여부)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// insertTempGoodsOrderInfo : 발주요청 임시등록
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
        public static DataTable insertTempGoodsOrderInfo(int intTmpGoodsOrderSeq, int intTmpGoodsOrderDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd
            , string strClassiMainCd, string strClassCd, int intQty, int intHaveQty, string strApproval)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[10];

            objParams[0] = intTmpGoodsOrderSeq;
            objParams[1] = intTmpGoodsOrderDetSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRentCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassiGrpCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassiMainCd));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassCd));
            objParams[7] = intQty;
            objParams[8] = intHaveQty;
            objParams[9] = strApproval;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_TMPGOODSORDERINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertGoodsOrderInfo : 발주요청 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertGoodsOrderInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 등록
         * Input    값 : InsertGoodsOrderInfo(순번, 상세순번, 임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드, 순번
         *                                          , 상세순번, 임대코드, 분류그룹코드, 분류메인코드, 분류코드, 출고부서코드
         *                                          , 출고사번, 출고일자, 수량, 진행코드, 상황코드, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertGoodsOrderInfo : 발주요청 임시등록
        /// </summary>
        /// <param name="strOrderSeq">순번</param>
        /// <param name="intOrderDetSeq">상세순번</param>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <param name="strProcessDeptCd">발주부서코드</param>
        /// <param name="strProcessMemNo">발주사번</param>
        /// <param name="strProcessDt">발주일자</param>
        /// <param name="intQty">수량</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strProgressCd">진행코드</param>
        /// <param name="strStateCd">상황코드</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strReleaseSeq">출고신청순번</param>
        /// <param name="intReleaseDetSeq">출고신청상세순번</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static DataTable InsertGoodsOrderInfo(string strOrderSeq, int intOrderDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd,
            string strClassCd, string strProcessDeptCd, string strProcessCompCd, string strProcessMemNo, string strProcessDt, int intQty, string strRemark,
            string strProgressCd, string strStateCd, string strApprovalYn, string strReleaseSeq, int intReleaseDetSeq, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[21];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = strRentCd;
            objParams[3] = strSvcZoneCd;
            objParams[4] = strClassiGrpCd;
            objParams[5] = strClassiMainCd;
            objParams[6] = strClassCd;
            objParams[7] = strProcessDeptCd;
            objParams[8] = strProcessCompCd;
            objParams[9] = strProcessMemNo;
            objParams[10] = strProcessDt;
            objParams[11] = intQty;
            objParams[12] = strRemark;
            objParams[13] = strProgressCd;
            objParams[14] = strStateCd;
            objParams[15] = strApprovalYn;
            objParams[16] = strReleaseSeq;
            objParams[17] = intReleaseDetSeq;
            objParams[18] = strCompCd;
            objParams[19] = strInsMemNo;
            objParams[20] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_GOODSORDERINFO_S00", objParams);

            return dtReturn;
        }
        #endregion

        #region InsertTmpGoodsInfoAddon : 발주 임시 승인 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTmpGoodsInfoAddon
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주 임시 승인 담당자 등록
         * Input    값 : InsertTmpGoodsInfoAddon(순번, 상세순번, 임시담당자순번, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertTmpGoodsInfoAddon : 발주 임시 승인 담당자 등록
        /// </summary>
        /// <param name="strReleaseSeq">순번</param>
        /// <param name="intReleaseDetSeq">상세순번</param>
        /// <param name="intTempSeq">임시담당자순번</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] InsertTmpGoodsInfoAddon(string strOrderSeq, int intOrderDetSeq, int intTempSeq, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = intTempSeq;
            objParams[3] = strApprovalYn;
            objParams[4] = strCompCd;
            objParams[5] = strInsMemNo;
            objParams[6] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_GOODSORDERADDON_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertGoodsOrderAddon : 발주 승인 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertGoodsOrderAddon
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주 승인 담당자 등록
         * Input    값 : InsertGoodsOrderAddon(순번, 상세순번, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertGoodsOrderAddon : 발주 승인 담당자 등록
        /// </summary>
        /// <param name="strReleaseSeq">순번</param>
        /// <param name="intReleaseDetSeq">상세순번</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] InsertGoodsOrderAddon(string strOrderSeq, int intOrderDetSeq, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = strApprovalYn;
            objParams[3] = strCompCd;
            objParams[4] = strInsMemNo;
            objParams[5] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_GOODSORDERADDON_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteTempGoodsOrderInfo : 발주요청 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempGoodsOrderInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주요청 임시 정보 삭제
         * Input    값 : DeleteTempGoodsOrderInfo(임시순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempGoodsOrderInfo : 발주요청 임시 정보 삭제
        /// </summary>
        /// <param name="intTempGoodsOrderSeq">임시순번</param>
        /// <returns></returns>

        public static object[] DeleteTempGoodsOrderInfo(int intTempGoodsOrderSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = intTempGoodsOrderSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_TMPGOODSORDERINFO_M02", objParams);

            return objReturn;
        }

        #endregion

        #region SelectGoodsOrderAddon : 최하위 발주 담당자 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectGoodsOrderAddon
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-15
         * 용       도 : 최하위 발주 담당자 조회
         * Input    값 : SelectGoodsOrderAddon(발주신청번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectGoodsOrderAddon : 발주 담당자 목록 조회
        /// </summary>
        /// <param name="strReleaseSeq">발주신청번호</param>
        /// <returns></returns>
        public static DataTable SelectGoodsOrderAddon(string strOrderSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strOrderSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_GOODSORDERADDON_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region DeleteGoodsOrderMng : 발주요청 관련 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteGoodsOrderMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-02
         * 용       도 : 발주요청 관련 정보 삭제
         * Input    값 : DeleteGoodsOrderMng(발주순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteGoodsOrderMng : 발주요청 관련 정보 삭제
        /// </summary>
        /// <param name="strReleaseSeq">발주순번</param>
        /// <returns></returns>
        public static object[] DeleteGoodsOrderMng(string strOrderSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = strOrderSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_GOODSORDERINFO_M00", objParams);

            return objReturn;
        }

        #endregion
    }
}
