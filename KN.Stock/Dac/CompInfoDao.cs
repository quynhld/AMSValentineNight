using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Stock.Dac
{
    public class CompInfoDao
    {
        #region SelectCompInfo : 공급업체 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 조회
         * Input    값 : SelectCompInfo(페이지별 리스트 크기, 현재페이지, 언어코드,  검색어코드, 검색어)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectCompInfo : 공급업체 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strKeyCd">검색어코드(0001 : 기업명, 0002 : 소개)</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataSet SelectCompInfo(int intPageSize, int intNowPage, string strLangCd, string strKeyCd, string strKeyWord)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[5];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strLangCd;
            objParams[3] = strKeyCd;
            objParams[4] = TextLib.StringEncoder(strKeyWord);

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_COMPINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectPopupCompInfo : 공급업체 조회 (팝업용)

        /**********************************************************************************************
         * Mehtod   명 : SelectPopupCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-26
         * 용       도 : 공급업체 조회 (팝업용)
         * Input    값 : SelectPopupCompInfo(언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectPopupCompInfo : 공급업체 조회 (팝업용)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectPopupCompInfo(string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_COMPINFO_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectCompDetailView : 공급업체 상세보기 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectCompDetail
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 상세보기 조회
         * Input    값 : SelectCompDetail(공급업체코드, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectCompDetail : 공급업체 상세보기 조회
        /// </summary>
        /// <param name="strCompNo">공급업체코드</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectCompDetail(string strCompNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strCompNo;
            objParams[1] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_COMPINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertCompInfo : 공급업체 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 등록
         * Input    값 : InsertCompInfo(공급업체명, 산업코드, 기타산업명, 대표자명, 기업소개, 대표기업여부, 
         *                              대표기업코드, 기업타입, 기업주소, 기업상세주소, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertCompInfo : 공급업체 등록 
        /// </summary>
        /// <param name="strCompNm">공급업체명</param>
        /// <param name="strBizIndusCd">산업코드</param>
        /// <param name="strBizEtcNm">기타산업명</param>
        /// <param name="strPresidentNm">대표자명</param>
        /// <param name="strIntroduce">기업소개</param>
        /// <param name="strCompTelFrontNo">전화번호지역번호</param>
        /// <param name="strCompTelMidNo">전화번호국번호</param>
        /// <param name="strCompTelRearNo">전화번호식별번호</param>
        /// <param name="strCompFaxFrontNo">FAX지역번호</param>
        /// <param name="strCompFaxMidNo">FAX국번호</param>
        /// <param name="strCompFaxRearNo">FAX식별번호</param>
        /// <param name="strChargerNm">담당자명</param>
        /// <param name="strCompFormYn">대표기업여부</param>
        /// <param name="strMotherCompNo">대표기업코드</param>
        /// <param name="strCompTyCd">기업타입</param>
        /// <param name="strAddr">기업주소</param>
        /// <param name="strDetAddr">기업상세주소</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static DataTable InsertCompInfo(string strCompNm, string strBizIndusCd, string strBizEtcNm, string strPresidentNm, string strIntroduce,
            	                               string strCompTelFrontNo, string strCompTelMidNo, string strCompTelRearNo,
                                               string strCompFaxFrontNo, string strCompFaxMidNo, string strCompFaxRearNo, string strChargerNm,
                                               string strCompFormYn, string strMotherCompNo, string strCompTyCd, string strAddr, string strDetAddr,
                                               string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[20];
            DataTable dtReturn = new DataTable();

            objParams[0] = TextLib.StringEncoder(strCompNm);
            objParams[1] = strBizIndusCd;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strBizEtcNm));
            objParams[3] = TextLib.StringEncoder(strPresidentNm);
            objParams[4] = TextLib.StringEncoder(strIntroduce);
            objParams[5] = strCompTelFrontNo;
            objParams[6] = strCompTelMidNo;
            objParams[7] = strCompTelRearNo;
            objParams[8] = strCompFaxFrontNo;
            objParams[9] = strCompFaxMidNo;
            objParams[10] = strCompFaxRearNo;
            objParams[11] = TextLib.StringEncoder(strChargerNm);
            objParams[12] = strCompFormYn;
            objParams[13] = strMotherCompNo;
            objParams[14] = strCompTyCd;
            objParams[15] = TextLib.StringEncoder(strAddr);
            objParams[16] = TextLib.StringEncoder(strDetAddr);
            objParams[17] = strCompCd;
            objParams[18] = strInsMemNo;
            objParams[19] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_COMPINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertCompAddon : 공급업체 첨부파일 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertCompAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-15
         * 용       도 : 공급업체 첨부파일 등록
         * Input    값 : InsertCompAddon(공급업체번호, 첨부파일순번, 저장화일경로, 저장화일크기, 고유화일명, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertCompAddon : 공급업체 첨부파일 등록
        /// </summary>
        /// <param name="strCompNo">공급업체번호</param>
        /// <param name="intCompSeq">첨부파일순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] InsertCompAddon(string strCompNo, int intCompSeq, string strFilePath, string strFileSize, string strFileRealNm, string strCompCd, string strInsMemNo)
        {
            object[] objParams = new object[7];
            object[] objReturns = new object[2];

            objParams[0] = strCompNo;
            objParams[1] = intCompSeq;
            objParams[2] = strFilePath;
            objParams[3] = strFileSize;
            objParams[4] = strFileRealNm;
            objParams[5] = strCompCd;
            objParams[6] = strInsMemNo;

            objReturns = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_COMPADDON_M00", objParams);

            return objReturns;
        }

        #endregion

        #region UpdateCompInfo : 공급업체 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 수정
         * Input    값 : UpdateCompInfo(공급업체코드명, 공급업체명, 산업코드, 기타산업명, 대표자명, 기업소개, 대표기업여부, 
         *                              대표기업코드, 기업타입, 기업주소, 기업상세주소, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateCompInfo : 공급업체 수정
        /// </summary>
        /// <param name="strCompCd">공급업체코드명</param>
        /// <param name="strCompNm">공급업체명</param>
        /// <param name="strBizIndusCd">산업코드</param>
        /// <param name="strBizEtcNm">기타산업명</param>
        /// <param name="strPresidentNm">대표자명</param>
        /// <param name="strIntroduce">기업소개</param>
        /// <param name="strCompTelFrontNo">전화번호지역번호</param>
        /// <param name="strCompTelMidNo">전화번호국번호</param>
        /// <param name="strCompTelRearNo">전화번호식별번호</param>
        /// <param name="strCompFaxFrontNo">FAX지역번호</param>
        /// <param name="strCompFaxMidNo">FAX국번호</param>
        /// <param name="strCompFaxRearNo">FAX식별번호</param>
        /// <param name="strChargerNm">담당자명</param>
        /// <param name="strCompFormYn">대표기업여부</param>
        /// <param name="strMotherCompNo">대표기업코드</param>
        /// <param name="strCompTyCd">기업타입</param>
        /// <param name="strAddr">기업주소</param>
        /// <param name="strDetAddr">기업상세주소</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] UpdateCompInfo(string strCompCd, string strCompNm, string strBizIndusCd, string strBizEtcNm, string strPresidentNm, string strIntroduce,
                                              string strCompTelFrontNo, string strCompTelMidNo, string strCompTelRearNo,
                                              string strCompFaxFrontNo, string strCompFaxMidNo, string strCompFaxRearNo, string strChargerNm,
                                              string strCompFormYn, string strMotherCompNo, string strCompTyCd, string strAddr, string strDetAddr,
                                              string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[21];
            object[] objReturns = new object[2];

            objParams[0] = strCompCd;
            objParams[1] = TextLib.StringEncoder(strCompNm);
            objParams[2] = strBizIndusCd;
            objParams[3] = TextLib.StringEncoder(strBizEtcNm);
            objParams[4] = TextLib.StringEncoder(strPresidentNm);
            objParams[5] = strIntroduce;
            objParams[6] = strCompTelFrontNo;
            objParams[7] = strCompTelMidNo;
            objParams[8] = strCompTelRearNo;
            objParams[9] = strCompFaxFrontNo;
            objParams[10] = strCompFaxMidNo;
            objParams[11] = strCompFaxRearNo;
            objParams[12] = TextLib.StringEncoder(strChargerNm);
            objParams[13] = strCompFormYn;
            objParams[14] = strMotherCompNo;
            objParams[15] = strCompTyCd;
            objParams[16] = TextLib.StringEncoder(strAddr);
            objParams[17] = TextLib.StringEncoder(strDetAddr);
            objParams[18] = strInsCompCd;
            objParams[19] = strInsMemNo;
            objParams[20] = strInsMemIP;

            objReturns = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_COMPINFO_M00", objParams);

            return objReturns;
        }

        #endregion

        #region UpdateCompAddon : 공급업체 첨부파일 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateCompAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-15
         * 용       도 : 공급업체 첨부파일 수정
         * Input    값 : UpdateCompAddon(공급업체번호, 첨부파일순번, 저장화일경로, 저장화일크기, 고유화일명, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateCompAddon : 공급업체 첨부파일 등록
        /// </summary>
        /// <param name="strCompNo">공급업체번호</param>
        /// <param name="intCompSeq">첨부파일순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] UpdateCompAddon(string strCompNo, int intCompSeq, string strFilePath, string strFileSize, string strFileRealNm, string strCompCd, string strInsMemNo)
        {
            object[] objParams = new object[7];
            object[] objReturns = new object[2];

            objParams[0] = strCompNo;
            objParams[1] = intCompSeq;
            objParams[2] = strFilePath;
            objParams[3] = strFileSize;
            objParams[4] = strFileRealNm;
            objParams[5] = strCompCd;
            objParams[6] = strInsMemNo;

            objReturns = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_COMPADDON_M00", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteDetailView : 공급업체 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteDetailView
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 삭제
         * Input    값 : DeleteDetailView(공급업체번호, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteDetailView : 공급업체 삭제
        /// </summary>
        /// <param name="strCompNo">공급업체번호</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strIP">사원IP</param>
        /// <returns></returns>
        public static object[] DeleteDetailView(string strCompNo, string strCompCd, string strMemNo, string strIP)
        {
            object[] objParams = new object[4];
            object[] objReturns = new object[2];

            objParams[0] = strCompNo;
            objParams[1] = strCompCd;
            objParams[2] = strMemNo;
            objParams[3] = strIP;

            objReturns = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_COMPINFO_M00", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteFileInfomation : 공급업체 첨부파일 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteFileInfomation
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-15
         * 용       도 : 공급업체 첨부파일 삭제
         * Input    값 : DeleteFileInfomation(공급업체번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteFileInfomation : 공급업체 첨부파일 삭제
        /// </summary>
        /// <param name="strCompNo">공급업체번호</param>
        /// <returns></returns>
        public static object[] DeleteFileInfomation(string strCompNo)
        {
            object[] objParams = new object[1];
            object[] objReturns = new object[2];

            objParams[0] = strCompNo;

            objReturns = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_COMPADDON_M00", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteEachFileInfomation : 공급업체 첨부파일 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteEachFileInfomation
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-15
         * 용       도 : 공급업체 첨부파일 삭제
         * Input    값 : DeleteEachFileInfomation(공급업체번호, 첨부파일순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteEachFileInfomation : 공급업체 첨부파일 삭제
        /// </summary>
        /// <param name="strCompNo">공급업체번호</param>
        /// <param name="intCompSeq">첨부파일순번</param>
        /// <returns></returns>
        public static object[] DeleteEachFileInfomation(string strCompNo, int intCompSeq)
        {
            object[] objParams = new object[2];
            object[] objReturns = new object[2];

            objParams[0] = strCompNo;
            objParams[1] = intCompSeq;

            objReturns = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_COMPADDON_M01", objParams);

            return objReturns;
        }

        #endregion
    }
}