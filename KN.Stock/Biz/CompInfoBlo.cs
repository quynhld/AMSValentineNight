using System.Configuration;
using System.Data;

using KN.Common.Method.Lib;
using KN.Stock.Dac;

namespace KN.Stock.Biz
{
    public class CompInfoBlo
    {
        //파일 업로드 경로 설정
        public static readonly string strAppFileUpload = ConfigurationSettings.AppSettings["UploadServerFolder"].ToString();

        #region SpreadCompInfo : 공급업체 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 조회
         * Input    값 : SelectCompInfo(페이지별 리스트 크기, 현재페이지, 언어코드, 검색어코드, 검색어)
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
        public static DataSet SpreadCompInfo(int intPageSize, int intNowPage, string strLangCd, string strKeyCd, string strKeyWord)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = CompInfoDao.SelectCompInfo(intPageSize, intNowPage, strLangCd, strKeyCd, strKeyWord);

            return dsReturn;
        }

        #endregion

        #region SpreadPopupCompInfo : 공급업체 조회 (팝업용)

        /**********************************************************************************************
         * Mehtod   명 : SpreadPopupCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-26
         * 용       도 : 공급업체 조회 (팝업용)
         * Input    값 : SpreadPopupCompInfo(언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadPopupCompInfo : 공급업체 조회 (팝업용)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadPopupCompInfo(string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CompInfoDao.SelectPopupCompInfo(strLangCd);

            return dtReturn;
        }

        #endregion

        #region WatchCompInfo : 공급업체 상세보기

        /**********************************************************************************************
         * Mehtod   명 : WatchCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 상세보기 조회
         * Input    값 : WatchCompInfo(공급업체코드, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectCompDetail : 공급업체 상세보기 조회
        /// </summary>
        /// <param name="strCompNo">공급업체코드</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable WatchCompInfo(string strCompNo, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CompInfoDao.SelectCompDetail(strCompNo, strLangCd);

            return dtReturn;
        }

        #endregion

        #region RegistryCompInfo : 공급업체 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 등록
         * Input    값 : InsertCompInfo(공급업체명, 산업코드, 기타산업명, 대표자명, 기업소개, 대표기업여부, 
         *                               대표기업코드, 기업타입, 기업주소, 기업상세주소, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryCompInfo : 공급업체 글등록 
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
        public static DataTable RegistryCompInfo(string strCompNm, string strBizIndusCd, string strBizEtcNm, string strPresidentNm, string strIntroduce,
                                                 string strCompTelFrontNo, string strCompTelMidNo, string strCompTelRearNo,
                                                 string strCompFaxFrontNo, string strCompFaxMidNo, string strCompFaxRearNo, string strChargerNm,
                                                 string strCompFormYn, string strMotherCompNo, string strCompTyCd, string strAddr, string strDetAddr,
                                                 string strCompCd, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CompInfoDao.InsertCompInfo(strCompNm, strBizIndusCd, strBizEtcNm, strPresidentNm, strIntroduce, 
                                                  strCompTelFrontNo, strCompTelMidNo, strCompTelRearNo,
                                                  strCompFaxFrontNo, strCompFaxMidNo, strCompFaxRearNo, strChargerNm,strCompFormYn, strMotherCompNo,
                                                  strCompTyCd, strAddr, strDetAddr, strCompCd, strInsMemNo, strInsMemIP);

            return dtReturn;
        }
        #endregion

        #region RegistryCompAddon : 공급업체 첨부파일 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryCompAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 첨부파일 등록
         * Input    값 : RegistryCompAddon(공급업체번호, 첨부파일순번, 저장화일경로, 저장화일크기, 고유화일명, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryCompAddon : 공급업체 첨부파일 등록
        /// </summary>
        /// <param name="strCompNo">공급업체번호</param>
        /// <param name="intCompSeq">첨부파일순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] RegistryCompAddon(string strCompNo, int intCompSeq, string strFilePath, string strFileSize, string strFileRealNm, string strCompCd, string strInsMemNo)
        {
            object[] objReturns = new object[2];

            objReturns = CompInfoDao.InsertCompAddon(strCompNo, intCompSeq, strFilePath, strFileSize, strFileRealNm, strCompCd, strInsMemNo);

            return objReturns;
        }

        #endregion

        #region ModifyCompInfo : 공급업체 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyCompInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-14
         * 용       도 : 공급업체 수정
         * Input    값 : UpdateCompInfo(공급업체코드명, 공급업체명, 산업코드, 기타산업명, 대표자명, 기업소개, 대표기업여부, 
         *                              대표기업코드, 기업타입, 기업주소, 기업상세주소, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyCompInfo : 공급업체 수정
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
        public static object[] ModifyCompInfo(string strCompCd, string strCompNm, string strBizIndusCd, string strBizEtcNm, string strPresidentNm, string strIntroduce,
                                              string strCompTelFrontNo, string strCompTelMidNo, string strCompTelRearNo,
                                              string strCompFaxFrontNo, string strCompFaxMidNo, string strCompFaxRearNo, string strChargerNm,
                                              string strCompFormYn, string strMotherCompNo, string strCompTyCd, string strAddr, string strDetAddr,
                                              string strInsCompCd, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturns = new object[2];

            objReturns = CompInfoDao.UpdateCompInfo(strCompCd, strCompNm, strBizIndusCd, strBizEtcNm, strPresidentNm, strIntroduce,
                                                    strCompTelFrontNo, strCompTelMidNo, strCompTelRearNo,
                                                    strCompFaxFrontNo, strCompFaxMidNo, strCompFaxRearNo, strChargerNm, strCompFormYn, strMotherCompNo,
                                                    strCompTyCd, strAddr, strDetAddr, strInsCompCd, strInsMemNo, strInsMemIP);

            return objReturns;
        }

        #endregion

        #region ModifyCompAddon : 공급업체 첨부파일 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyCompAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-15
         * 용       도 : 공급업체 첨부파일 수정
         * Input    값 : UpdateCompAddon(공급업체번호, 첨부파일순번, 저장화일경로, 저장화일크기, 고유화일명, 사원번호, 기존화일경로)
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
        /// <param name="strModMemNo">사원번호</param>
        /// <param name="strExistFile">기존화일경로</param>
        /// <returns></returns>
        public static object[] ModifyCompAddon(string strCompNo, int intCompSeq, string strFilePath, string strFileSize, string strFileRealNm, string strCompCd, string strModMemNo, string strExistFile)
        {
            object[] objReturns = new object[2];

            if (!string.IsNullOrEmpty(strExistFile))
            {
                // 공급업체 첨부파일 삭제
                FileLib.FileDelete(strExistFile);
            }

            objReturns = CompInfoDao.UpdateCompAddon(strCompNo, intCompSeq, strFilePath, strFileSize, strFileRealNm, strCompCd, strModMemNo);

            return objReturns;
        }
        #endregion

        #region RemoveDetailViewWithNoFiles : 파일을 제외한 공급업체 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveDetailViewWithNoFiles
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 파일을 제외한 공급업체 정보 삭제
         * Input    값 : RemoveDetailViewWithNoFiles(공급업체번호, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveDetailViewWithNoFiles : 파일을 제외한 공급업체 정보 삭제
        /// </summary>
        /// <param name="strCompNo">공급업체번호</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strIP">사원IP</param>
        /// <returns></returns>
        public static object[] RemoveDetailViewWithNoFiles(string strCompNo, string strCompCd, string strMemNo, string strIP)
        {
            object[] objReturn = new object[2];

            // 공급업체 글 삭제
            objReturn = CompInfoDao.DeleteDetailView(strCompNo, strCompCd, strMemNo, strIP);

            return objReturn;
        }

        #endregion

        #region RemoveDetailView : 공급업체 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveDetailView
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-15
         * 용       도 : 공급업체 정보 삭제
         * Input    값 : RemoveDetailView(공급업체번호, 사원번호, 사원IP, 화일경로1, 화일경로2, 화일경로3)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveDetailView : 공급업체 정보 삭제
        /// </summary>
        /// <param name="strCompNo">공급업체번호</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strIP">사원IP</param>
        /// <param name="strFilePath1">화일경로1</param>
        /// <param name="strFilePath2">화일경로2</param>
        /// <param name="strFilePath3">화일경로3</param>
        /// <returns></returns>
        public static object[] RemoveDetailView(string strCompNo, string strCompCd, string strMemNo, string strIP, string strFilePath1, string strFilePath2, string strFilePath3)
        {
            object[] objReturn = new object[2];

            // 공급업체 삭제
            objReturn = CompInfoDao.DeleteDetailView(strCompNo, strCompCd, strMemNo, strIP);

            if (objReturn != null)
            {
                // 공급업체 첨부파일 삭제
                FileLib.FileDelete(strAppFileUpload + strFilePath1);
                FileLib.FileDelete(strAppFileUpload + strFilePath2);
                FileLib.FileDelete(strAppFileUpload + strFilePath3);

                // 공급업체 첨부파일 정보 삭제
                objReturn = CompInfoDao.DeleteFileInfomation(strCompNo);
            }
            else
            {
                objReturn = null;
            }

            return objReturn;
        }

        #endregion

        #region RemoveCompAddon : 공급업체 첨부파일 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveCompAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-15
         * 용       도 : 공급업체 첨부파일 삭제
         * Input    값 : RemoveCompAddon(공급업체번호, 첨부파일순번, 첨부파일경로)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveCompAddon : 공급업체 정보 삭제
        /// </summary>
        /// <param name="strCompNo">공급업체구분</param>
        /// <param name="intCompSeq">공급업체글순번</param>
        /// <param name="strFilePath">첨부파일경로</param>
        /// <returns></returns>
        public static object[] RemoveCompAddon(string strCompNo, int intCompSeq, string strFilePath)
        {
            object[] objReturn = new object[2];

            // 공급업체 첨부파일 정보 삭제
            objReturn = CompInfoDao.DeleteEachFileInfomation(strCompNo, intCompSeq);

            if (objReturn != null)
            {
                FileLib.FileDelete(strAppFileUpload + strFilePath);
            }

            return objReturn;
        }

        #endregion
    }
}
