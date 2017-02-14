using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Board.Dac
{
    public class BoardInfoDao
    {
        #region SelectBoardInfo : 게시판 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectBoardInfo
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
        public static DataSet SelectBoardInfo(int intPageSize, int intNowPage, string strBoardTy, string strBoardCd, string strKeyCd, string strKeyWord,
                                              string strAccessAuth, string strCompNo, string strMemNo)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[9];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strBoardTy;
            objParams[3] = strBoardCd;
            objParams[4] = strKeyCd;
            objParams[5] = TextLib.StringEncoder(strKeyWord);
            objParams[6] = strAccessAuth;
            objParams[7] = strCompNo;
            objParams[8] = strMemNo;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_BRD_SELECT_BOARDINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectBoardDetailView : 게시판 상세보기 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectBoardDetail
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 상세보기 조회
         * Input    값 : SelectBoardDetail(게시판구분, 게시판코드, 게시판글순번, 접근권한, 접속회사코드, 조회사원ID)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectBoardDetail : 게시판 상세보기 조회
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strCompNo">접속회사코드</param>
        /// <param name="strMemNo">조회사원ID</param>
        /// <returns></returns>
        public static DataTable SelectBoardDetail(string strBoardTy, string strBoardCd, int intBoardSeq, string strAccessAuth, string strCompNo, string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[6];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;
            objParams[3] = strAccessAuth;
            objParams[4] = strCompNo;
            objParams[5] = strMemNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_SELECT_BOARDINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectBoardFileCntInfo : 게시판 첨부파일 개수 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectBoardFileCntInfo
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
        public static DataTable SelectBoardFileCntInfo(int intMenuSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = intMenuSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_SELECT_BOARDMNGINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectBoardAccess : 게시판 접근코드 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectBoardAccess
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-09
         * 용       도 : 게시판 접근코드 조회
         * Input    값 : SelectBoardAccess(게시판구분, 게시판코드, 게시판글순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectBoardAccess : 게시판 접근코드 조회
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <returns></returns>
        public static DataTable SelectBoardAccess(string strBoardTy, string strBoardCd, int intBoardSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_SELECT_BOARDINFO_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectBoardMngInfo : 게시판관리(첨부파일개수)

        /**********************************************************************************************
         * Mehtod   명 : SelectBoardMngInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 게시판관리(첨부파일개수)
         * Input    값 : 
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectBoardMngInfo : 게시판관리(첨부파일개수)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectBoardMngInfo(string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_FILEADDONCOUNTINFO_M00", objParams);

            return dtReturn;
        }

        #endregion 

        #region InsertBoardInfo : 게시판 글등록

        /**********************************************************************************************
         * Mehtod   명 : InsertBoardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 글등록
         * Input    값 : InsertBoardInfo(게시판구분, 게시판코드, 게시판글제목, 게시판내용, 접근권한, 보기권한, 쓰기권한, 
         *                               수정권한, 회사코드, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertBoardInfo : 게시판 글등록
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="strBoardTitle">게시판글제목</param>
        /// <param name="strBoardContent">게시판내용</param>
        /// <param name="strViewCompNo">접근권한</param>
        /// <param name="strViewAuth">보기권한</param>
        /// <param name="strInsAuth">쓰기권한</param>
        /// <param name="strModAuth">수정권한</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <param name="strInsMemIP">사원IP</param>
        /// <returns></returns>
        public static DataTable InsertBoardInfo(string strBoardTy, string strBoardCd, string strBoardTitle, string strBoardContent, string strViewCompNo,
                                                string strViewAuth, string strInsAuth, string strModAuth, string strInsCompNo, string strInsMemNo,
                                                string strInsMemIP)
        {
            object[] objParams = new object[11];
            DataTable dtReturn = new DataTable();

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = TextLib.StringEncoder(strBoardTitle);
            objParams[3] = TextLib.StringEncoder(strBoardContent);
            objParams[4] = strViewCompNo;
            objParams[5] = strViewAuth;
            objParams[6] = strInsAuth;
            objParams[7] = strModAuth;
            objParams[8] = strInsCompNo;
            objParams[9] = strInsMemNo;
            objParams[10] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_INSERT_BOARDINFO_M00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertBoardAddon : 게시판 첨부파일 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertBoardAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 첨부파일 등록
         * Input    값 : InsertBoardAddon(게시판구분, 게시판코드, 게시판글순번, 첨부파일순번, 저장화일경로, 
         *                               저장화일크기, 고유화일명, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertBoardAddon : 게시판 첨부파일 등록
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="intBoardAddSeq">첨부파일순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <param name="strInsMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] InsertBoardAddon(string strBoardTy, string strBoardCd, int intBoardSeq, int intBoardAddSeq, string strFilePath, string strFileSize,
                                                string strFileRealNm, string strInsCompNo, string strInsMemNo)
        {
            object[] objParams = new object[9];
            object[] objReturns = new object[2];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;
            objParams[3] = intBoardAddSeq;
            objParams[4] = strFilePath;
            objParams[5] = strFileSize;
            objParams[6] = strFileRealNm;
            objParams[7] = strInsCompNo;
            objParams[8] = strInsMemNo;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_INSERT_BOARDADDON_M00", objParams);

            return objReturns;
        }

        #endregion

        #region InsertBoardReply : 게시판 답글등록

        /**********************************************************************************************
         * Mehtod   명 : InsertBoardReply
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-15
         * 용       도 : 게시판 답글등록
         * Input    값 : InsertBoardReply(게시판구분, 게시판코드, 게시판글번호, 게시판글제목, 게시판내용, 접근회사코드, 
         *                                보기권한, 쓰기권한, 수정권한, 등록회사코드, 사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertBoardReply : 게시판 답글등록
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
        public static DataTable InsertBoardReply(string strBoardTy, string strBoardCd, int intBoardSeq, string strBoardTitle, string strBoardContent, string strViewCompNo,
                                                 string strViewAuth, string strInsAuth, string strModAuth, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[12];
            DataTable dtReturn = new DataTable();

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;
            objParams[3] = strBoardTitle;
            objParams[4] = strBoardContent;
            objParams[5] = strViewCompNo;
            objParams[6] = strViewAuth;
            objParams[7] = strInsAuth;
            objParams[8] = strModAuth;
            objParams[9] = strInsCompNo;
            objParams[10] = strInsMemNo;
            objParams[11] = strInsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_INSERT_BOARDINFO_M01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertFileAddonCntInfo : 첨부파일 관리 목록 추가

        /**********************************************************************************************
         * Mehtod   명 : InsertFileAddonCntInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 첨부파일 관리 목록 추가
         * Input    값 : itnMenuSeq
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 첨부파일 관리 목록 추가
        /// </summary>
        /// <param name="itnMenuSeq">메뉴순번</param>
        /// <returns></returns>
        internal static object[] InsertFileAddonCntInfo(int itnMenuSeq)
        {
            object[] objParams = new object[1];
            object[] objReturns = new object[2];

            objParams[0] = itnMenuSeq;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_FILEADDONCOUNTINFO_M00", objParams);

            return objReturns;
        }

        #endregion            

        #region UpdateBoardInfo : 게시판 글수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateBoardInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 글수정
         * Input    값 : UpdateBoardInfo(게시판구분, 게시판코드, 게시판글순번, 게시판글제목, 게시판내용, 
         *                               보기권한, 쓰기권한, 수정권한, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateBoardInfo : 게시판 글수정
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="strBoardTitle">게시판글제목</param>
        /// <param name="strBoardContent">게시판내용</param>
        /// <param name="strViewCompNo">대상기업번호</param>
        /// <param name="strViewAuth">보기권한</param>
        /// <param name="strInsAuth">쓰기권한</param>
        /// <param name="strModAuth">수정권한</param>
        /// <param name="strModCompNo">수정기업코드</param>
        /// <param name="strModMemNo">사원번호</param>
        /// <param name="strModMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] UpdateBoardInfo(string strBoardTy, string strBoardCd, int intBoardSeq, string strBoardTitle, string strBoardContent,
                                               string strViewCompNo, string strViewAuth, string strInsAuth, string strModAuth, string strModCompNo,
                                               string strModMemNo, string strModMemIP)
        {
            object[] objParams = new object[12];
            object[] objReturns = new object[2];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;
            objParams[3] = TextLib.StringEncoder(strBoardTitle);
            objParams[4] = TextLib.StringEncoder(strBoardContent);
            objParams[5] = strViewCompNo;
            objParams[6] = strViewAuth;
            objParams[7] = strInsAuth;
            objParams[8] = strModAuth;
            objParams[9] = strModCompNo;
            objParams[10] = strModMemNo;
            objParams[11] = strModMemIP;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_UPDATE_BOARDINFO_M00", objParams);

            return objReturns;
        }

        #endregion

        #region UpdateBoardAddon : 게시판 첨부파일 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateBoardAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 첨부파일 수정
         * Input    값 : UpdateBoardAddon(게시판구분, 게시판코드, 게시판글순번, 첨부파일순번, 저장화일경로, 
         *                               저장화일크기, 고유화일명, 수정기업코드, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateBoardAddon : 게시판 첨부파일 등록
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="intBoardAddSeq">첨부파일순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <param name="strModCompNo">수정기업코드</param>
        /// <param name="strModMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] UpdateBoardAddon(string strBoardTy, string strBoardCd, int intBoardSeq, int intBoardAddSeq, string strFilePath,
                                                string strFileSize, string strFileRealNm, string strModCompNo, string strModMemNo)
        {
            object[] objParams = new object[9];
            object[] objReturns = new object[2];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;
            objParams[3] = intBoardAddSeq;
            objParams[4] = strFilePath;
            objParams[5] = strFileSize;
            objParams[6] = strFileRealNm;
            objParams[7] = strModCompNo;
            objParams[8] = strModMemNo;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_UPDATE_BOARDADDON_M00", objParams);

            return objReturns;
        }

        #endregion

        #region UpdateBoardViewCnt : 게시판 조회수 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateBoardViewCnt
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 조회수 수정
         * Input    값 : UpdateBoardViewCnt(게시판구분, 게시판코드, 게시판글순번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// UpdateBoardViewCnt : 게시판 조회수 수정
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <returns></returns>
        public static object[] UpdateBoardViewCnt(string strBoardTy, string strBoardCd, int intBoardSeq)
        {
            object[] objParams = new object[3];
            object[] objReturns = new object[2];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_UPDATE_BOARDVIEWCNT_M00", objParams);

            return objReturns;
        }

        #endregion

        #region UpdateBoardMngInfo : 게시판관리(첨부파일개수수정)

        /**********************************************************************************************
         * Mehtod   명 : UpdateBoardMngInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 게시판관리(첨부파일개수수정)
         * Input    값 : intMemuSeq, intFileCnt, strReplyYn
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateBoardMngInfo : 게시판관리(첨부파일개수수정)
        /// </summary>
        /// <param name="intMemuSeq">메뉴순번</param>
        /// <param name="intFileCnt">첨부파일개수</param>
        /// <param name="strReplyYn">답글허용여부</param>
        /// <returns></returns>
        public static object[] UpdateBoardMngInfo(int intMemuSeq, int intFileCnt, string strReplyYn)
        {
            object[] objParams = new object[3];
            object[] objReturns = new object[2];

            objParams[0] = intMemuSeq;
            objParams[1] = intFileCnt;
            objParams[2] = strReplyYn;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_FILEADDONCOUNTINFO_M00", objParams);

            return objReturns;
        }

        #endregion   

        #region DeleteDetailView : 게시판글 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteDetailView
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판글 삭제
         * Input    값 : DeleteDetailView(게시판구분, 게시판코드, 게시판글순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteDetailView : 게시판글 삭제
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <returns></returns>
        public static object[] DeleteDetailView(string strBoardTy, string strBoardCd, int intBoardSeq)
        {
            object[] objParams = new object[3];
            object[] objReturns = new object[2];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_DELETE_BOARDINFO_M00", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteFileInfomation : 게시판 첨부파일 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteFileInfomation
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-14
         * 용       도 : 게시판 첨부파일 삭제
         * Input    값 : DeleteFileInfomation(게시판구분, 게시판코드, 게시판글순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteFileInfomation : 게시판 첨부파일 삭제
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <returns></returns>
        public static object[] DeleteFileInfomation(string strBoardTy, string strBoardCd, int intBoardSeq)
        {
            object[] objParams = new object[3];
            object[] objReturns = new object[2];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_DELETE_BOARDADDON_M00", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteEachFileInfomation : 게시판 첨부파일 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteEachFileInfomation
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-15
         * 용       도 : 게시판 첨부파일 삭제
         * Input    값 : DeleteEachFileInfomation(게시판구분, 게시판코드, 게시판글순번, 첨부파일순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteEachFileInfomation : 게시판 첨부파일 삭제
        /// </summary>
        /// <param name="strBoardTy">게시판구분</param>
        /// <param name="strBoardCd">게시판코드</param>
        /// <param name="intBoardSeq">게시판글순번</param>
        /// <param name="intBoardAddSeq">첨부파일순번</param>
        /// <returns></returns>
        public static object[] DeleteEachFileInfomation(string strBoardTy, string strBoardCd, int intBoardSeq, int intBoardAddSeq)
        {
            object[] objParams = new object[4];
            object[] objReturns = new object[2];

            objParams[0] = strBoardTy;
            objParams[1] = strBoardCd;
            objParams[2] = intBoardSeq;
            objParams[3] = intBoardAddSeq;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_DELETE_BOARDADDON_M01", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteFileAddonCntInfo : 첨부파일 관리 목록 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteFileAddonCntInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-12
         * 용       도 : 첨부파일 관리 목록 삭제
         * Input    값 : itnMenuSeq
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 첨부파일 관리 목록 삭제
        /// </summary>
        /// <param name="itnMenuSeq">메뉴순번</param>
        /// <returns></returns>
        internal static object[] DeleteFileAddonCntInfo(int itnMenuSeq)
        {
            object[] objParams = new object[1];
            object[] objReturns = new object[2];

            objParams[0] = itnMenuSeq;

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_FILEADDONCOUNTINFO_M00", objParams);

            return objReturns;
        }

        #endregion
    }
}