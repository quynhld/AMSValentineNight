using System.Configuration;
using System.Data;

using KN.Common.Method.Lib;

using KN.Board.Dac;

namespace KN.Board.Biz
{
    public class BoardInfoBlo
    {
        //파일 업로드 경로 설정
        public static readonly string strAppFileUpload = ConfigurationSettings.AppSettings["UploadServerFolder"].ToString();

        #region SpreadBoardInfo : 게시판 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadBoardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-12
         * 용       도 : 게시판 조회
         * Input    값 : SelectBoardInfo(페이지별 리스트 크기, 현재페이지, 게시판구분, 게시판코드, 검색어코드, 검색어, 접근권한, 소속회사코드, 조회사원ID)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectBoardInfo : 게시판 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="strKeyCd">검색어코드(0001 : 제목, 0002 : 내용, 0003 : 전체)</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strCompNo">소속회사코드</param>
        /// <param name="strMemNo">조회사원ID</param>
        /// <returns></returns>
        public static DataSet SpreadBoardInfo(int intPageSize, int intNowPage, string strBoardTy, string strBoardCd, string strKeyCd, string strKeyWord,
                                              string strAccessAuth, string strCompNo, string strMemNo)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = BoardInfoDao.SelectBoardInfo(intPageSize, intNowPage, strBoardTy, strBoardCd, strKeyCd, strKeyWord, strAccessAuth, strCompNo, strMemNo);

            return dsReturn;
        }

        #endregion

        #region WatchBoardInfo : 게시판 상세보기

        /**********************************************************************************************
         * Mehtod   명 : WatchBoardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-12
         * 용       도 : 게시판 상세보기
         * Input    값 : WatchBoardInfo(게시판구분, 게시판코드, 게시판글순번, 접근권한, 회사코드, 조회사원ID)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchBoardInfo : 게시판 상세보기
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">조회사원ID</param>
        /// <returns></returns>
        public static DataTable WatchBoardInfo(string strBoardTy, string strBoardCd, int intBoardSeq, string strAccessAuth, string strCompNo, string strMemNo)
        {
            DataTable dtReturn = new DataTable();
            object[] objReturn = new object[2];

            // 조회수 증가 시킴
            // KN_USP_BRD_UPDATE_BOARDVIEWCNT_M00
            objReturn = BoardInfoDao.UpdateBoardViewCnt(strBoardTy, strBoardCd, intBoardSeq);

            if (objReturn != null)
            {
                // 게시판 상세정보 조회
                // KN_USP_BRD_SELECT_BOARDINFO_S01
                dtReturn = BoardInfoDao.SelectBoardDetail(strBoardTy, strBoardCd, intBoardSeq, strAccessAuth, strCompNo, strMemNo);
            }
            else
            {
                dtReturn = null;
            }

            return dtReturn;
        }

        #endregion

        #region WatchBoardFileCntInfo : 게시판 첨부파일 개수 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchBoardFileCntInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 게시판 첨부파일 개수 조회
         * Input    값 : strBoardCd
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 게시판 첨부파일 개수 조회
        /// </summary>
        /// <param name="strBoardCd">게시판코드</param>
        /// <returns></returns>
        public static DataTable WatchBoardFileCntInfo(int intMenuSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BoardInfoDao.SelectBoardFileCntInfo(intMenuSeq);

            return dtReturn;
        }
        #endregion  

        #region WatchBoardAccess : 게시판 접근코드 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchBoardAccess
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-09
         * 용       도 : 게시판 접근코드 조회
         * Input    값 : WatchBoardAccess(게시판구분, 게시판코드, 게시판글순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchBoardAccess : 게시판 접근코드 조회
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <returns></returns>
        public static DataTable WatchBoardAccess(string strBoardTy, string strBoardCd, int intBoardSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BoardInfoDao.SelectBoardAccess(strBoardTy, strBoardCd, intBoardSeq);

            return dtReturn;
        }

        #endregion

        #region SpreadBoardMngInfo : 게시판관리(첨부파일개수)

        /**********************************************************************************************
         * Mehtod   명 : SpreadBoardMngInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 게시판관리(첨부파일개수)
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadBoardMngInfo : 게시판관리(첨부파일개수)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadBoardMngInfo(string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BoardInfoDao.SelectBoardMngInfo(strLangCd);

            return dtReturn;
        }

        #endregion 

        #region RegistryBoardInfo : 게시판 글등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryBoardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 글등록
         * Input    값 : RegistryBoardInfo(게시판구분, 게시판코드, 게시판글제목, 게시판내용, 
         *                               보기권한, 쓰기권한, 수정권한, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryBoardInfo : 게시판 글등록
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="strBoardTitle">게시판글제목</param>
        /// <param name="strBoardContent">게시판내용</param>
        /// <param name="strViewAuth">보기권한</param>
        /// <param name="strInsAuth">쓰기권한</param>
        /// <param name="strModAuth">수정권한</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static DataTable RegistryBoardInfo(string strBoardTy, string strBoardCd, string strBoardTitle, string strBoardContent, string strViewCompNo, string strViewAuth,
                                                  string strInsAuth, string strModAuth, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BoardInfoDao.InsertBoardInfo(strBoardTy, strBoardCd, strBoardTitle, strBoardContent, strViewCompNo, strViewAuth, strInsAuth, strModAuth, strInsCompNo,
                                                    strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryBoardAddon : 게시판 첨부파일 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryBoardAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 첨부파일 등록
         * Input    값 : RegistryBoardAddon(게시판구분, 게시판코드, 게시판글순번, 첨부파일순번, 저장화일경로, 
         *                               저장화일크기, 고유화일명, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryBoardAddon : 게시판 첨부파일 등록
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="intBoardAddSeq">첨부파일순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] RegistryBoardAddon(string strBoardTy, string strBoardCd, int intBoardSeq, int intBoardAddSeq, string strFilePath, string strFileSize,
                                                  string strFileRealNm, string strInsCompNo, string strInsMemNo)
        {
            object[] objReturns = new object[2];

            objReturns = BoardInfoDao.InsertBoardAddon(strBoardTy, strBoardCd, intBoardSeq, intBoardAddSeq, strFilePath, strFileSize, strFileRealNm, strInsCompNo, strInsMemNo);

            return objReturns;
        }

        #endregion

        #region RegistryBoardReply : 게시판 답글등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryBoardReply
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-15
         * 용       도 : 게시판 답글등록
         * Input    값 : RegistryBoardReply(게시판구분, 게시판코드, 게시판글번호, 게시판글제목, 게시판내용, 접근회사코드,
         *                               보기권한, 쓰기권한, 수정권한, 등록회사코드, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistryBoardReply : 게시판 답글등록
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글번호</param>
        /// <param name="strBoardTitle">게시판글제목</param>
        /// <param name="strBoardContent">게시판내용</param>
        /// <param name="strViewCompNo">접근회사코드</param>
        /// <param name="strViewAuth">보기권한</param>
        /// <param name="strInsAuth">쓰기권한</param>
        /// <param name="strModAuth">수정권한</param>
        /// <param name="strInsCompNo">등록회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static DataTable RegistryBoardReply(string strBoardTy, string strBoardCd, int intBoardSeq, string strBoardTitle, string strBoardContent, string strViewCompNo,
                                                   string strViewAuth, string strInsAuth, string strModAuth, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = BoardInfoDao.InsertBoardReply(strBoardTy, strBoardCd, intBoardSeq, strBoardTitle, strBoardContent, strViewCompNo, strViewAuth, strInsAuth, strModAuth,
                                                     strInsCompNo, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistryFileAddonCntInfo : 첨부파일 관리 목록 추가

        /**********************************************************************************************
         * Mehtod   명 : RegistryFileAddonCntInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 첨부파일 관리 목록 추가
         * Input    값 : RegistryFileAddonCntInfo(itnMenuSeq)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 첨부파일 관리 목록 추가
        /// </summary>
        /// <param name="itnMenuSeq">메뉴순번</param>
        /// <returns></returns>
        public static object[] RegistryFileAddonCntInfo(int itnMenuSeq)
        {
            object[] objReturn = new object[2];

            // 첨부파일 관리 목록 추가
            objReturn = BoardInfoDao.InsertFileAddonCntInfo(itnMenuSeq);

            return objReturn;
        }

        #endregion

        #region ModifyBoardInfo : 게시판 글수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyBoardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 글수정
         * Input    값 : ModifyBoardInfo(게시판구분, 게시판코드, 게시판글순번, 게시판글제목, 게시판내용, 
         *                               보기권한, 쓰기권한, 수정권한, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyBoardInfo : 게시판 글수정
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="strBoardTitle">게시판글제목</param>
        /// <param name="strBoardContent">게시판내용</param>
        /// <param name="strViewAuth">보기권한</param>
        /// <param name="strInsAuth">쓰기권한</param>
        /// <param name="strModAuth">수정권한</param>
        /// <param name="strModMemNo">사원번호</param>
        /// <param name="strModMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] ModifyBoardInfo(string strBoardTy, string strBoardCd, int intBoardSeq, string strBoardTitle, string strBoardContent,
                                               string strViewCompNo, string strViewAuth, string strInsAuth, string strModAuth, string strModCompNo,
                                               string strModMemNo, string strModMemIP)
        {
            object[] objReturns = new object[2];

            objReturns = BoardInfoDao.UpdateBoardInfo(strBoardTy, strBoardCd, intBoardSeq, strBoardTitle, strBoardContent, strViewCompNo, strViewAuth,
                                                      strInsAuth, strModAuth, strModCompNo, strModMemNo, strModMemIP);

            return objReturns;
        }

        #endregion

        #region ModifyBoardAddon : 게시판 첨부파일 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyBoardAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 첨부파일 수정
         * Input    값 : ModifyBoardAddon(게시판구분, 게시판코드, 게시판글순번, 첨부파일순번, 저장화일경로, 
         *                               저장화일크기, 고유화일명, 사원번호, 기존화일경로)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyBoardAddon : 게시판 첨부파일 수정
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="intBoardAddSeq">첨부파일순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <param name="strModMemNo">사원번호</param>
        /// <param name="strModCompNo">수정기업코드</param>
        /// <param name="strExistFile">기존화일경로</param>
        /// <returns></returns>
        public static object[] ModifyBoardAddon(string strBoardTy, string strBoardCd, int intBoardSeq, int intBoardAddSeq, string strFilePath, 
                                                string strFileSize, string strFileRealNm, string strModCompNo, string strModMemNo, string strExistFile)
        {
            object[] objReturns = new object[2];

            if (!string.IsNullOrEmpty(strExistFile))
            {
                // 게시판 첨부파일 삭제
                FileLib.FileDelete(strExistFile);
            }

            objReturns = BoardInfoDao.UpdateBoardAddon(strBoardTy, strBoardCd, intBoardSeq, intBoardAddSeq, strFilePath, strFileSize, strFileRealNm, strModCompNo, strModMemNo);

            return objReturns;
        }

        #endregion

        #region ModifyBoardMngInfo : 게시판관리(첨부파일개수수정)

        /**********************************************************************************************
         * Mehtod   명 : ModifyBoardMngInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 게시판관리(첨부파일개수수정)
         * Input    값 : intMemuSeq, intFileCnt, strReplyYn
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 게시판관리(첨부파일개수수정)
        /// </summary>
        /// <param name="intMemuSeq">메뉴순번</param>
        /// <param name="intFileCnt">첨부파일개수</param>
        /// <param name="strReplyYn">답글허용여부</param>
        /// <returns></returns>
        public static object[] ModifyBoardMngInfo(int intMemuSeq, int intFileCnt, string strReplyYn)
        {
            object[] objReturns = new object[2];

            objReturns = BoardInfoDao.UpdateBoardMngInfo(intMemuSeq, intFileCnt, strReplyYn);

            return objReturns;
        }

        #endregion 
       
        #region RemoveDetailView : 게시판 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveDetailView
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 정보 삭제
         * Input    값 : RemoveDetailView(게시판구분, 게시판코드, 게시판글순번, 화일경로1, 화일경로2, 화일경로3)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveDetailView : 게시판 정보 삭제
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="strFilePath1">화일경로1</param>
        /// <param name="strFilePath2">화일경로2</param>
        /// <param name="strFilePath3">화일경로3</param>
        /// <returns></returns>
        public static object[] RemoveDetailView(string strBoardTy, string strBoardCd, int intBoardSeq, string strFilePath1, string strFilePath2, string strFilePath3)
        {
            object[] objReturn = new object[2];

            // 게시판 글 삭제
            objReturn = BoardInfoDao.DeleteDetailView(strBoardTy, strBoardCd, intBoardSeq);

            if (objReturn != null)
            {
                // 게시판 첨부파일 삭제
                FileLib.FileDelete(strAppFileUpload + strFilePath1);
                FileLib.FileDelete(strAppFileUpload + strFilePath2);
                FileLib.FileDelete(strAppFileUpload + strFilePath3);

                // 게시판 첨부파일 정보 삭제
                objReturn = BoardInfoDao.DeleteFileInfomation(strBoardTy, strBoardCd, intBoardSeq);
            }
            else
            {
                objReturn = null;
            }

            return objReturn;
        }

        #endregion

        #region RemoveBoardAddon : 게시판 첨부파일 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveBoardAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-15
         * 용       도 : 게시판 첨부파일 삭제
         * Input    값 : RemoveBoardAddon(게시판구분, 게시판코드, 게시판글순번, 첨부파일순번, 사원번호, 사원IP, 첨부파일경로)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveBoardAddon : 게시판 정보 삭제
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="intBoardAddSeq">첨부파일순번</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strIP">사원IP</param>
        /// <param name="strFilePath">첨부파일경로</param>
        /// <returns></returns>
        public static object[] RemoveBoardAddon(string strBoardTy, string strBoardCd, int intBoardSeq, int intBoardAddSeq, string strFilePath)
        {
            object[] objReturn = new object[2];

            // 게시판 첨부파일 정보 삭제
            objReturn = BoardInfoDao.DeleteEachFileInfomation(strBoardTy, strBoardCd, intBoardSeq, intBoardAddSeq);

            if (objReturn != null)
            {
                FileLib.FileDelete(strAppFileUpload + strFilePath);
            }

            return objReturn;
        }

        #endregion

        #region RemoveFileAddonCntInfo : 첨부파일 관리 목록 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveFileAddonCntInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 첨부파일 관리 목록 삭제
         * Input    값 : RegistryFileAddonCntInfo(itnMenuSeq)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itnMenuSeq"></param>
        /// <returns></returns>
        public static object[] RemoveFileAddonCntInfo(int itnMenuSeq)
        {
            object[] objReturn = new object[2];

            // 첨부파일 관리 목록 삭제
            objReturn = BoardInfoDao.DeleteFileAddonCntInfo(itnMenuSeq);

            return objReturn;
        }

        #endregion
    }
}