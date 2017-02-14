using System.Data;

using KN.Stock.Dac;

namespace KN.Stock.Biz
{
    public class ReleaseInfoBlo
    {
        #region SpreadReleaseRequestInfo : 출고요청 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-29
         * 용       도 : 출고요청 목록조회
         * Input    값 : SpreadReleaseRequestInfo(페이지 목록수, 현재 페이지, 섹션코드, 서비스존코드, 그룹코드, 메인코드,
         *                                        서브코드, 코드명, 언어코드, 처리과정코드. 상태코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadReleaseRequestInfo : 출고요청 목록조회
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
        public static DataSet SpreadReleaseRequestInfo(int intPageSize, int intNowPage, string strSectionCd, string strSvcZoneCd, string strGrpCd, string strMainCd, string strSubCd
            , string strCdNm, string strLangCd, string strProcessCd, string strStatusCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ReleaseInfoDao.SelectReleaseRequestInfo(intPageSize, intNowPage, strSectionCd, strSvcZoneCd, strGrpCd, strMainCd, strSubCd, strCdNm, strLangCd, strProcessCd, strStatusCd);

            return dsReturn;
        }

        #endregion

        #region WatchReleaseRequestDetailInfo : 출고요청 상세조회

        /**********************************************************************************************
         * Mehtod   명 : WatchReleaseRequestDetailInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-12
         * 용       도 : 출고요청 상세조회
         * Input    값 : WatchReleaseRequestDetailInfo(출고순번, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchReleaseRequestDetailInfo : 출고요청 목록조회
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet WatchReleaseRequestDetailInfo(string strReleaseSeq, string strLangCd)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = ReleaseInfoDao.SelectReleaseRequestDetailInfo(strReleaseSeq, strLangCd);

            return dsReturn;
        }

        #endregion

        #region SpreadTempReleaseRequestInfo : 출고요청 임시 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시 목록조회
         * Input    값 : SpreadTempReleaseRequestInfo(임시순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadTempReleaseRequestInfo : 출고요청 임시 목록조회
        /// </summary>
        /// <param name="intTmpReleaseSeq">임시순번</param>
        /// <returns></returns>
        public static DataTable SpreadTempReleaseRequestInfo(int intTmpReleaseSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReleaseInfoDao.SelectTempReleaseRequestInfo(intTmpReleaseSeq);

            return dtReturn;
        }

        #endregion

        #region SpreadReleaseChargeInfo : 출고 담당자 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadReleaseChargeInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-08
         * 용       도 : 출고 담당자 목록 조회
         * Input    값 : SpreadReleaseChargeInfo(조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadReleaseChargeInfo : 출고 담당자 목록 조회
        /// </summary>
        /// <param name="strNowDt">조회일자</param>
        /// <returns></returns>
        public static DataTable SpreadReleaseChargeInfo(string strNowDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReleaseInfoDao.SelectReleaseChargeInfo(strNowDt);

            return dtReturn;
        }

        #endregion

        #region SpreadReleaseRequestAddon : 최하위 출고 담당자 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadReleaseRequestAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 최하위 출고 담당자 조회
         * Input    값 : SpreadReleaseRequestAddon(출고신청번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadReleaseRequestAddon : 출고 담당자 목록 조회
        /// </summary>
        /// <param name="strReleaseSeq">출고신청번호</param>
        /// <returns></returns>
        public static DataTable SpreadReleaseRequestAddon(string strReleaseSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReleaseInfoDao.SelectReleaseRequestAddon(strReleaseSeq);

            return dtReturn;
        }

        #endregion

        #region RegistryTempReleaseRequestInfo : 출고요청 임시등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-30
         * 용       도 : 출고요청 임시등록
         * Input    값 : RegistryTempReleaseRequestInfo(임시순번, 임시상세순번, 임대코드, 분류그룹코드, 분류메인코드, 분류코드, 수량, 자동승인여부)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempReleaseRequestInfo : 출고요청 임시등록
        /// </summary>
        /// <param name="intTmpReleaseSeq">임시순번</param>
        /// <param name="intTmpReleaseDetSeq">임시상세순번</param>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <param name="intQty">수량</param>
        /// <param name="strApproval">자동승인여부</param>
        /// <returns></returns>
        public static DataTable RegistryTempReleaseRequestInfo(int intTmpReleaseSeq, int intTmpReleaseDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd
            , string strClassiMainCd, string strClassCd, int intQty, int intHaveQty, string strApproval)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReleaseInfoDao.insertTempReleaseRequestInfo(intTmpReleaseSeq, intTmpReleaseDetSeq, strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd, strClassCd, intQty, intHaveQty, strApproval);

            return dtReturn;
        }

        #endregion

        #region RegistryTempReleaseRequestInfo : 출고요청 임시등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시등록
         * Input    값 : RegistryTempReleaseRequestInfo(임시순번, 임시상세순번, 임대코드, 분류그룹코드, 분류메인코드, 분류코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryTempReleaseRequestInfo : 출고요청 임시등록
        /// </summary>
        /// <param name="intTmpReleaseSeq">임시순번</param>
        /// <param name="intTmpReleaseDetSeq">임시상세순번</param>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="strClassiGrpCd">분류그룹코드</param>
        /// <param name="strClassiMainCd">분류메인코드</param>
        /// <param name="strClassCd">분류코드</param>
        /// <returns></returns>
        public static DataTable RegistryTempReleaseRequestInfo(int intTmpReleaseSeq, int intTmpReleaseDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd
            , string strClassiMainCd, string strClassCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReleaseInfoDao.insertTempReleaseRequestInfo(intTmpReleaseSeq, intTmpReleaseDetSeq, strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd, strClassCd);

            return dtReturn;
        }

        #endregion

        #region RegistryReleaseRequestInfo : 출고요청 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 등록
         * Input    값 : RegistryReleaseRequestInfo(순번, 상세순번, 임대코드, 분류그룹코드, 분류메인코드, 분류코드, 순번
         *                                          , 상세순번, 임대코드, 분류그룹코드, 분류메인코드, 분류코드, 출고부서코드
         *                                          , 출고사번, 출고일자, 수량, 발생비용, 진행코드, 상황코드, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryReleaseRequestInfo : 출고요청 임시등록
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
        public static DataTable RegistryReleaseRequestInfo(string strReleaseSeq, int intReleaseDetSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd,
            string strClassCd, string strProcessDeptCd, string strProcessCompCd, string strProcessMemNo, string strProcessDt, int intQty, string strRemark,
            double dblProcessFee, string strProcessRemark, string strProgressCd, string strStateCd, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReleaseInfoDao.insertReleaseRequestInfo(strReleaseSeq, intReleaseDetSeq, strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd
                , strClassCd, strProcessDeptCd, strProcessCompCd, strProcessMemNo, strProcessDt, intQty, strRemark, dblProcessFee, strProcessRemark, strProgressCd
                , strStateCd, strApprovalYn,strCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        /// <summary>
        /// RegistryReleaseRequestInfo : 출고요청 임시등록
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
        public static DataTable RegistryReleaseRequestInfo(string strReleaseSeq, string strRentCd, string strSvcZoneCd, string strClassiGrpCd, string strClassiMainCd,
            string strClassCd, string strProcessDeptCd, string strProcessMemNo, string strProcessDt, int intQty, string strRemark,
            double dblProcessFee, string strProcessRemark, string strProgressCd, string strStateCd, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = ReleaseInfoDao.insertReleaseRequestInfo(strReleaseSeq, strRentCd, strSvcZoneCd, strClassiGrpCd, strClassiMainCd
                , strClassCd, strProcessDeptCd, strProcessMemNo, strProcessDt, intQty, strRemark, dblProcessFee, strProcessRemark, strProgressCd
                , strStateCd, strApprovalYn, strCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryTmpReleaseRequestAddon : 품목별 출고 임시 승인 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryTmpReleaseRequestAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 품목별 출고 임시 승인 담당자 등록
         * Input    값 : RegistryTmpReleaseRequestAddon(순번, 상세순번, 임시담당자순번, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryTmpReleaseRequestAddon : 품목별 출고 임시 승인 담당자 등록
        /// </summary>
        /// <param name="strReleaseSeq">순번</param>
        /// <param name="intReleaseDetSeq">상세순번</param>
        /// <param name="intTempSeq">임시담당자순번</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] RegistryTmpReleaseRequestAddon(string strReleaseSeq, int intReleaseDetSeq, int intTempSeq, string strApprovalYn,string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.InsertTmpReleaseRequestAddon(strReleaseSeq, intReleaseDetSeq, intTempSeq, strApprovalYn,strCompCd, strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region RegistryReleaseRequestAddon : 품목별 출고 승인 담당자 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryReleaseRequestAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 품목별 출고 승인 담당자 등록
         * Input    값 : RegistryReleaseRequestAddon(순번, 상세순번, 승인여부, 등록사번, 등록IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryReleaseRequestAddon : 품목별 출고 승인 담당자 등록
        /// </summary>
        /// <param name="strReleaseSeq">순번</param>
        /// <param name="intReleaseDetSeq">상세순번</param>
        /// <param name="strApprovalYn">승인여부</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIP">등록IP</param>
        /// <returns></returns>
        public static object[] RegistryReleaseRequestAddon(string strReleaseSeq, int intReleaseDetSeq, string strApprovalYn, string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.InsertReleaseRequestAddon(strReleaseSeq, intReleaseDetSeq, strApprovalYn, strCompCd,strInsMemNo, strInsMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyTempReleaseRequestInfo : 출고요청 임시 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시 정보 수정
         * Input    값 : ModifyTempReleaseRequestInfo(임시순번, 임시상세순번, 수량)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyTempReleaseRequestInfo : 출고요청 임시 정보 수정
        /// </summary>
        /// <param name="intTempReleaseSeq">임시순번</param>
        /// <param name="intTempReleaseDetSeq">임시상세순번</param>
        /// <param name="intQty">수량</param>
        /// <returns></returns>

        public static object[] ModifyTempReleaseRequestInfo(int intTempReleaseSeq, int intTempReleaseDetSeq, int intQty)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.UpdateTempReleaseRequestInfo(intTempReleaseSeq, intTempReleaseDetSeq, intQty);

            return objReturn;
        }

        #endregion

        #region ModifyReleaseRequestConfirm : 출고승인 등록

        /**********************************************************************************************
         * Mehtod   명 : ModifyReleaseRequestConfirm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고승인 등록
         * Input    값 : ModifyReleaseRequestConfirm(출고순번, 출고상세순번, 담당자순번, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyReleaseRequestConfirm : 출고승인 등록
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="intReleaseDetSeq">출고상세순번</param>
        /// <param name="intChargeSeq">담당자순번</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] ModifyReleaseRequestConfirm(string strReleaseSeq, int intReleaseDetSeq, int intChargeSeq, string strRemark)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.UpdateReleaseRequestConfirm(strReleaseSeq, intReleaseDetSeq, intChargeSeq, strRemark);

            return objReturn;
        }

        #endregion

        #region ModifyReleaseRequestDeny : 출고반려 등록

        /**********************************************************************************************
         * Mehtod   명 : ModifyReleaseRequestDeny
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고반려 등록
         * Input    값 : ModifyReleaseRequestDeny(출고순번, 출고상세순번, 담당자순번, 비고, 출고상태코드, 승인여부코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyReleaseRequestDeny : 출고반려 등록
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="intReleaseDetSeq">출고상세순번</param>
        /// <param name="intChargeSeq">담당자순번</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strProcessCd">출고상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <returns></returns>
        public static object[] ModifyReleaseRequestDeny(string strReleaseSeq, int intReleaseDetSeq, int intChargeSeq, string strRemark, string strProcessCd, string strStateCd)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.UpdateReleaseRequestDeny(strReleaseSeq, intReleaseDetSeq, intChargeSeq, strRemark);

            if (objReturn != null)
            {
                objReturn = ReleaseInfoDao.UpdateReleaseRequestItem(strReleaseSeq, intReleaseDetSeq, strProcessCd, strStateCd);
            }

            return objReturn;
        }

        #endregion

        #region ModifyReleaseRequestItem : 출고상태값 수정 (품목별)

        /**********************************************************************************************
         * Mehtod   명 : ModifyReleaseRequestItem
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고상태값 수정 (품목별)
         * Input    값 : ModifyReleaseRequest(출고순번, 출고상세순번, 출고관련상태코드, 승인여부코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyReleaseRequestItem : 출고상태값 수정 (품목별)
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="intReleaseDetSeq">출고상세순번</param>
        /// <param name="strProcessCd">출고관련상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <returns></returns>
        public static object[] ModifyReleaseRequestItem(string strReleaseSeq, int intReleaseDetSeq, string strProcessCd, string strStateCd)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.UpdateReleaseRequestItem(strReleaseSeq, intReleaseDetSeq, strProcessCd, strStateCd);

            return objReturn;
        }

        /// <summary>
        /// ModifyReleaseRequestItem : 출고상태값 수정 (품목별)
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="intReleaseDetSeq">출고상세순번</param>
        /// <param name="strProcessCd">출고관련상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <param name="intQty">신청수량</param>
        /// <returns></returns>
        public static object[] ModifyReleaseRequestItem(string strReleaseSeq, int intReleaseDetSeq, string strProcessCd, string strStateCd, int intQty)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.UpdateReleaseRequestItem(strReleaseSeq, intReleaseDetSeq, strProcessCd, strStateCd, intQty);

            return objReturn;
        }

        #endregion

        #region ModifyReleaseRequestSeq : 출고상태값 수정 (출고번호별)

        /**********************************************************************************************
         * Mehtod   명 : ModifyReleaseRequestSeq
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고상태값 수정 (출고번호별)
         * Input    값 : ModifyReleaseRequestSeq(출고순번, 출고관련상태코드, 승인여부코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyReleaseRequestSeq : 출고상태값 수정 (출고번호별)
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="strProcessCd">출고관련상태코드</param>
        /// <param name="strStateCd">승인여부코드</param>
        /// <returns></returns>
        public static object[] ModifyReleaseRequestSeq(string strReleaseSeq, string strProcessCd, string strStateCd)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.UpdateReleaseRequestSeq(strReleaseSeq, strProcessCd, strStateCd);

            return objReturn;
        }

        #endregion

        #region ModifyReleaseRequestBasicInfo : 출고요청 기본 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyReleaseRequestBasicInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고요청 기본 정보 수정
         * Input    값 : ModifyReleaseRequestBasicInfo(출고순번, 출고일자, 비고, 처리비용, 처리비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyReleaseRequestBasicInfo : 출고요청 기본 정보 수정
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <param name="strProcessDt">출고일자</param>
        /// <param name="strRemark">비고</param>
        /// <param name="dblProcessFee">처리비용</param>
        /// <param name="strFmsRemark">처리비고</param>
        /// <returns></returns>
        public static object[] ModifyReleaseRequestBasicInfo(string strReleaseSeq, string strProcessDt, string strRemark, double dblProcessFee, string strFmsRemark)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.UpdateReleaseRequestBasicInfo(strReleaseSeq, strProcessDt, strRemark, dblProcessFee, strFmsRemark);

            return objReturn;
        }

        #endregion

        #region RemoveLatestTempReleaseRequestInfo : 출고요청 최근 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveLatestTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 최근 임시 정보 삭제
         * Input    값 : RemoveLatestTempReleaseRequestInfo()
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveLatestTempReleaseRequestInfo : 출고요청 최근 임시 정보 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] RemoveLatestTempReleaseRequestInfo()
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.DeleteLatestTempReleaseRequestInfo();

            return objReturn;
        }

        #endregion

        #region RemoveTempReleaseRequestInfo : 출고요청 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시 정보 삭제
         * Input    값 : DeleteTempReleaseRequestInfo(임시순번, 임시상세순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempReleaseRequestInfo : 출고요청 임시 정보 삭제
        /// </summary>
        /// <param name="intTempReleaseSeq">임시순번</param>
        /// <param name="intTempReleaseDetSeq">임시상세순번</param>
        /// <returns></returns>
        public static object[] RemoveTempReleaseRequestInfo(int intTempReleaseSeq, int intTempReleaseDetSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.DeleteTempReleaseRequestInfo(intTempReleaseSeq, intTempReleaseDetSeq);

            return objReturn;
        }

        #endregion

        #region RemoveTempReleaseRequestInfo : 출고요청 임시 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveTempReleaseRequestInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-01
         * 용       도 : 출고요청 임시 정보 삭제
         * Input    값 : DeleteTempReleaseRequestInfo(임시순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveTempReleaseRequestInfo : 출고요청 임시 정보 삭제
        /// </summary>
        /// <param name="intTempReleaseSeq">임시순번</param>
        /// <returns></returns>
        public static object[] RemoveTempReleaseRequestInfo(int intTempReleaseSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = intTempReleaseSeq;

            objReturn = ReleaseInfoDao.DeleteTempReleaseRequestInfo(intTempReleaseSeq);

            return objReturn;
        }

        #endregion

        #region RemoveReleaseRequestMng : 출고요청 관련 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveReleaseRequestMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-12
         * 용       도 : 출고요청 관련 정보 삭제
         * Input    값 : RemoveReleaseRequestMng(출고순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveReleaseRequestMng : 출고요청 관련 정보 삭제
        /// </summary>
        /// <param name="strReleaseSeq">출고순번</param>
        /// <returns></returns>
        public static object[] RemoveReleaseRequestMng(string strReleaseSeq)
        {
            object[] objReturn = new object[2];

            objReturn = ReleaseInfoDao.DeleteReleaseRequestMng(strReleaseSeq);

            return objReturn;
        }

        #endregion
    }
}
