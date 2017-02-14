using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Stock.Dac
{
    public class ReleaseInfoDao
    {
        #region SelectReleaseRequestInfo : 출고요청 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SelectReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-29
         * 용       도 : 출고요청 목록조회
         * Input    값 : SelectReleaseRequestInfo(페이지 목록수, 현재 페이지, 섹션코드, 서비스존코드, 그룹코드, 메인코드,
         *                                        서브코드, 코드명, 언어코드, 처리과정코드. 상태코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectReleaseRequestInfo : 출고요청 목록조회
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
        public static DataSet SelectReleaseRequestInfo(int intPageSize, int intNowPage, string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd
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

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_RELEASEREQUESTINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectReleaseRequestDetailInfo : 출고요청 상세조회

        /**********************************************************************************************
         * Mehtod   명 : SelectReleaseRequestDetailInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-12
         * 용       도 : 출고요청 상세조회
         * Input    값 : SelectReleaseRequestDetailInfo(출고순번, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectReleaseRequestDetailInfo : 출고요청 상세조회
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectReleaseRequestDetailInfo(string strReleaseSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[2];

            objParams[0] = strReleaseSeq;
            objParams[1] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_RELEASEREQUESTINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectTempReleaseRequestInfo : 출고요청 임시 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SelectTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시 목록조회
         * Input    값 : SelectTempReleaseRequestInfo(임시순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectTempReleaseRequestInfo : 출고요청 임시 목록조회
        /// </summary>
        /// <param name="intTmpReleaseSeq">임시순번</param>
        /// <returns></returns>
        public static DataTable SelectTempReleaseRequestInfo(int intTmpReleaseSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = intTmpReleaseSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_TMPRELEASEINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectReleaseChargeInfo : 출고 담당자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectReleaseChargeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-08
         * 용       도 : 출고 담당자 목록 조회
         * Input    값 : SelectReleaseChargeInfo(조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectReleaseChargeInfo : 출고 담당자 목록 조회
        /// </summary>
        /// <param name="strNowDt">조회일자</param>
        /// <returns></returns>
        public static DataTable SelectReleaseChargeInfo(string strNowDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strNowDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_RELEASECHARGERINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectReleaseRequestAddon : 최하위 출고 담당자 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectReleaseRequestAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 최하위 출고 담당자 조회
         * Input    값 : SelectReleaseRequestAddon(출고신청번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectReleaseRequestAddon : 출고 담당자 목록 조회
        /// </summary>
        /// <param name="strReleaseSeq">출고신청번호</param>
        /// <returns></returns>
        public static DataTable SelectReleaseRequestAddon(string strReleaseSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strReleaseSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_RELEASEREQUESTADDON_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region insertTempReleaseRequestInfo : 출고요청 임시등록

        /**********************************************************************************************
         * Mehtod   명 : insertTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-30
         * 용       도 : 출고요청 임시등록
         * Input    값 : insertTempReleaseRequestInfo(임시순번, 임시상세순번, 임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드, 수량, 보유수량, 자동승인여부)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// insertTempReleaseRequestInfo : 출고요청 임시등록
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
        public static DataTable insertTempReleaseRequestInfo(int intTmpReleaseSeq, int intTmpReleaseDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd
            ,string strClassiMainCd, string strClassCd, int intQty, int intHaveQty, string strApproval)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[10];

            objParams[0] = intTmpReleaseSeq;
            objParams[1] = intTmpReleaseDetSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRentCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassiGrpCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassiMainCd));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassCd));
            objParams[7] = intQty;
            objParams[8] = intHaveQty;
            objParams[9] = strApproval;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_TMPRELEASEINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region insertTempReleaseRequestInfo : 출고요청 임시등록

        /**********************************************************************************************
         * Mehtod   명 : insertTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시등록
         * Input    값 : insertTempReleaseRequestInfo(임시순번, 임시상세순번, 임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// insertTempReleaseRequestInfo : 출고요청 임시등록
        /// </summary>
        /// <param name="intTmpReleaseSeq">임시순번</param>
        /// <param name="intTmpReleaseDetSeq">임시상세순번</param>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <returns></returns>
        public static DataTable insertTempReleaseRequestInfo(int intTmpReleaseSeq, int intTmpReleaseDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd
            , string strClassiMainCd, string strClassCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[7];

            objParams[0] = intTmpReleaseSeq;
            objParams[1] = intTmpReleaseDetSeq;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRentCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassiGrpCd));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassiMainCd));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strClassCd));

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_TMPRELEASEINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertReleaseRequestInfo : 출고요청 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 등록
         * Input    값 : insertTempReleaseRequestInfo(순번, 상세순번, 임대코드, 서비스존코드, 분류그룹코드, 분류메인코드, 분류코드, 순번
         *                                          , 상세순번, 임대코드, 분류그룹코드, 분류메인코드, 분류코드, 출고부서코드
         *                                          , 출고사번, 출고일자, 수량, 발생비용, 진행코드, 상황코드, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertReleaseRequestInfo : 출고요청 임시등록
        /// </summary>
        /// <param name="strReleaseSeq">순번</param>
        /// <param name="intReleaseDetSeq">상세순번</param>
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
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static DataTable insertReleaseRequestInfo(string strReleaseSeq, int intReleaseDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd,
            string strClassCd, string strProcessDeptCd, string strProcessCompCd, string strProcessMemNo, string strProcessDt, int intQty, string strRemark, 
            double dblProcessFee, string strProcessRemark, string strProgressCd, string strStateCd, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[21];

            objParams[0] = strReleaseSeq;
            objParams[1] = intReleaseDetSeq;
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
            objParams[13] = dblProcessFee;
            objParams[14] = strProcessRemark;
            objParams[15] = strProgressCd;
            objParams[16] = strStateCd;
            objParams[17] = strApprovalYn;
            objParams[18] = strCompCd;
            objParams[19] = strInsMemNo;
            objParams[20] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_RELEASEREQUESTINFO_S00", objParams);

            return dtReturn;
        }

        /// <summary>
        /// InsertReleaseRequestInfo : 출고요청 임시등록
        /// </summary>
        /// <param name="strReleaseSeq">순번</param>
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
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static DataTable insertReleaseRequestInfo(string strReleaseSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd,
            string strClassCd, string strProcessDeptCd, string strProcessMemNo, string strProcessDt, int intQty, string strRemark,
            double dblProcessFee, string strProcessRemark, string strProgressCd, string strStateCd, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[19];

            objParams[0] = strReleaseSeq;
            objParams[1] = strRentCd;
            objParams[2] = strSvcZoneCd;
            objParams[3] = strClassiGrpCd;
            objParams[4] = strClassiMainCd;
            objParams[5] = strClassCd;
            objParams[6] = strProcessDeptCd;
            objParams[7] = strProcessMemNo;
            objParams[8] = strProcessDt;
            objParams[9] = intQty;
            objParams[10] = strRemark;
            objParams[11] = dblProcessFee;
            objParams[12] = strProcessRemark;
            objParams[13] = strProgressCd;
            objParams[14] = strStateCd;
            objParams[15] = strApprovalYn;
            objParams[16] = strCompCd;
            objParams[17] = strInsMemNo;
            objParams[18] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_RELEASEREQUESTINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertTmpReleaseRequestAddon : 품목별 출고 임시 승인 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertTmpReleaseRequestAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 품목별 출고 임시 승인 담당자 등록
         * Input    값 : InsertTmpReleaseRequestAddon(순번, 상세순번, 임시담당자순번, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertTmpReleaseRequestAddon : 품목별 출고 임시 승인 담당자 등록
        /// </summary>
        /// <param name="strReleaseSeq">순번</param>
        /// <param name="intReleaseDetSeq">상세순번</param>
        /// <param name="intTempSeq">임시담당자순번</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] InsertTmpReleaseRequestAddon(string strReleaseSeq, int intReleaseDetSeq, int intTempSeq, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[7];

            objParams[0] = strReleaseSeq;
            objParams[1] = intReleaseDetSeq;
            objParams[2] = intTempSeq;
            objParams[3] = strApprovalYn;
            objParams[4] = strCompCd;
            objParams[5] = strInsMemNo;
            objParams[6] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_RELEASEREQUESTADDON_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertReleaseRequestAddon : 품목별 출고 승인 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertReleaseRequestAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 품목별 출고 승인 담당자 등록
         * Input    값 : InsertReleaseRequestAddon(순번, 상세순번, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertReleaseRequestAddon : 품목별 출고 승인 담당자 등록
        /// </summary>
        /// <param name="strReleaseSeq">순번</param>
        /// <param name="intReleaseDetSeq">상세순번</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] InsertReleaseRequestAddon(string strReleaseSeq, int intReleaseDetSeq, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strReleaseSeq;
            objParams[1] = intReleaseDetSeq;
            objParams[2] = strApprovalYn;
            objParams[3] = strCompCd;
            objParams[4] = strInsMemNo;
            objParams[5] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_RELEASEREQUESTADDON_M01", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateTempReleaseRequestInfo : 출고요청 임시 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시 정보 수정
         * Input    값 : UpdateTempReleaseRequestInfo(임시순번, 임시상세순번, 수량)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateTempReleaseRequestInfo : 출고요청 임시 정보 수정
        /// </summary>
        /// <param name="intTempReleaseSeq">임시순번</param>
        /// <param name="intTempReleaseDetSeq">임시상세순번</param>
        /// <param name="intQty">수량</param>
        /// <returns></returns>

        public static object[] UpdateTempReleaseRequestInfo(int intTempReleaseSeq, int intTempReleaseDetSeq, int intQty)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[3];

            objParams[0] = intTempReleaseSeq;
            objParams[1] = intTempReleaseDetSeq;
            objParams[2] = intQty;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_TMPRELEASEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateReleaseRequestConfirm : 출고승인 등록

        /**********************************************************************************************
         * Mehtod   명 : UpdateReleaseRequestConfirm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고승인 등록
         * Input    값 : UpdateReleaseRequestConfirm(출고순번, 출고상세순번, 담당자순번, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateReleaseRequestConfirm : 출고승인 등록
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="intReleaseDetSeq">출고상세순번</param>
        /// <param name="intChargeSeq">담당자순번</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] UpdateReleaseRequestConfirm(string strReleaseSeq, int intReleaseDetSeq, int intChargeSeq, string strRemark)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strReleaseSeq;
            objParams[1] = intReleaseDetSeq;
            objParams[2] = intChargeSeq;
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_RELEASEREQUESTCONFIRM_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateReleaseRequestDeny : 출고반려 등록

        /**********************************************************************************************
         * Mehtod   명 : UpdateReleaseRequestDeny
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고반려 등록
         * Input    값 : UpdateReleaseRequestDeny(출고순번, 출고상세순번, 담당자순번, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateReleaseRequestDeny : 출고반려 등록
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="intReleaseDetSeq">출고상세순번</param>
        /// <param name="intChargeSeq">담당자순번</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] UpdateReleaseRequestDeny(string strReleaseSeq, int intReleaseDetSeq, int intChargeSeq, string strRemark)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strReleaseSeq;
            objParams[1] = intReleaseDetSeq;
            objParams[2] = intChargeSeq;
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_RELEASEREQUESTDENY_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateReleaseRequestItem : 출고상태값 수정 (품목별)

        /**********************************************************************************************
         * Mehtod   명 : UpdateReleaseRequestItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고상태값 수정 (품목별)
         * Input    값 : UpdateReleaseRequest(출고순번, 출고상세순번, 출고관련상태코드, 승인여부코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateReleaseRequestItem : 출고상태값 수정 (품목별)
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="intReleaseDetSeq">출고상세순번</param>
        /// <param name="strProcessCd">출고관련상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <returns></returns>
        public static object[] UpdateReleaseRequestItem(string strReleaseSeq, int intReleaseDetSeq, string strProcessCd, string strStateCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strReleaseSeq;
            objParams[1] = intReleaseDetSeq;
            objParams[2] = strProcessCd;
            objParams[3] = strStateCd;
            objParams[4] = 0;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00", objParams);

            return objReturn;
        }

        /// <summary>
        /// UpdateReleaseRequestItem : 출고상태값 수정 (품목별)
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="intReleaseDetSeq">출고상세순번</param>
        /// <param name="strProcessCd">출고관련상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <param name="intQty">신청수량</param>
        /// <returns></returns>
        public static object[] UpdateReleaseRequestItem(string strReleaseSeq, int intReleaseDetSeq, string strProcessCd, string strStateCd, int intQty)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strReleaseSeq;
            objParams[1] = intReleaseDetSeq;
            objParams[2] = strProcessCd;
            objParams[3] = strStateCd;
            objParams[4] = intQty;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateReleaseRequestSeq : 출고상태값 수정 (출고번호별)

        /**********************************************************************************************
         * Mehtod   명 : UpdateReleaseRequestSeq
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고상태값 수정 (출고번호별)
         * Input    값 : UpdateReleaseRequestSeq(출고순번, 출고관련상태코드, 승인여부코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateReleaseRequestSeq : 출고상태값 수정 (출고번호별)
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="strProcessCd">출고관련상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <returns></returns>
        public static object[] UpdateReleaseRequestSeq(string strReleaseSeq, string strProcessCd, string strStateCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[3];

            objParams[0] = strReleaseSeq;
            objParams[1] = strProcessCd;
            objParams[2] = strStateCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateReleaseRequestBasicInfo : 출고요청 기본 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateReleaseRequestBasicInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고요청 기본 정보 수정
         * Input    값 : UpdateReleaseRequestBasicInfo(출고순번, 출고일자, 비고, 처리비용, 처리비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateReleaseRequestBasicInfo : 출고요청 기본 정보 수정
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="strProcessDt">출고일자</param>
        /// <param name="strRemark">비고</param>
        /// <param name="dblProcessFee">처리비용</param>
        /// <param name="strFmsRemark">처리비고</param>
        /// <returns></returns>
        public static object[] UpdateReleaseRequestBasicInfo(string strReleaseSeq, string strProcessDt, string strRemark, double dblProcessFee, string strFmsRemark)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strReleaseSeq;
            objParams[1] = strProcessDt;
            objParams[2] = strRemark;
            objParams[3] = dblProcessFee;
            objParams[4] = strFmsRemark;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_RELEASEREQUESTINFO_M02", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteLatestTempReleaseRequestInfo : 출고요청 최근 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteLatestTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 최근 임시 정보 삭제
         * Input    값 : DeleteLatestTempReleaseRequestInfo()
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// DeleteLatestTempReleaseRequestInfo : 출고요청 최근 임시 정보 삭제
        /// </summary>
        public static object[] DeleteLatestTempReleaseRequestInfo()
        {
            object[] objReturn = new object[2];

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_TMPRELEASEINFO_M00");

            return objReturn;
        }

        #endregion

        #region DeleteTempReleaseRequestInfo : 출고요청 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시 정보 삭제
         * Input    값 : DeleteTempReleaseRequestInfo(임시순번, 임시상세순번)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempReleaseRequestInfo : 출고요청 임시 정보 삭제
        /// </summary>
        /// <param name="intTempReleaseSeq">임시순번</param>
        /// <param name="intTempReleaseDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] DeleteTempReleaseRequestInfo(int intTempReleaseSeq, int intTempReleaseDetSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intTempReleaseSeq;
            objParams[1] = intTempReleaseDetSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_TMPRELEASEINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteTempReleaseRequestInfo : 출고요청 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시 정보 삭제
         * Input    값 : DeleteTempReleaseRequestInfo(임시순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteTempReleaseRequestInfo : 출고요청 임시 정보 삭제
        /// </summary>
        /// <param name="intTempReleaseSeq">임시순번</param>
        /// <returns></returns>

        public static object[] DeleteTempReleaseRequestInfo(int intTempReleaseSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = intTempReleaseSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_TMPRELEASEINFO_M02", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteReleaseRequestMng : 출고요청 관련 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteReleaseRequestMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-12
         * 용       도 : 출고요청 관련 정보 삭제
         * Input    값 : DeleteReleaseRequestMng(출고순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteReleaseRequestMng : 출고요청 관련 정보 삭제
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <returns></returns>
        public static object[] DeleteReleaseRequestMng(string strReleaseSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = strReleaseSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_RELEASEREQUESTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

    }
}