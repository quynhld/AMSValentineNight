using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Board.Dac
{
    public class MemoMngDao
    {
        #region SelectMemoMng : 메모 조회 (공통영역)

        /**********************************************************************************************
         * Mehtod   명 : SelectMemoMng 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 메모 조회 (공통영역)
         * Input    값 : SelectMemoMng(사원번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMemoMng : 메모 조회 (공통영역)
        /// </summary>
        ///<param name="strMemNo">사원번호</param>
        /// <returns>DataSet</returns>
        public static DataTable SelectMemoMng(string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParam = new object[1];

            objParam[0] = strMemNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_SELECT_MEMODETAILINFO_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMemoInfo : 메모 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-04
         * 용       도 : 메모 조회
         * Input    값 : SelectMemoInfo(페이지별 리스트 크기, 현재페이지, 로그인한사원ID)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SelectMemoInfo(int intPageSize, int intNowPage, string strMemNo, string strKeyCd, string strKeyWord)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[5];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.StringEncoder(strMemNo);
            objParams[3] = strKeyCd;
            objParams[4] = TextLib.StringEncoder(strKeyWord);

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_BRD_SELECT_MEMOINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectMemoDetail : 메모 상세보기

        /**********************************************************************************************
         * Mehtod   명 : SelectMemoDetail
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-04
         * 용       도 : 메모 상세보기
         * Input    값 : SelectMemoDetail(intMemoSeq, strMemno)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 메모 상세보기
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strMemno">사원번호</param>
        /// <returns></returns>
        public static DataTable SelectMemoDetail(int intMemoSeq, string strCompNo, string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = intMemoSeq;
            objParams[1] = strCompNo;
            objParams[2] = strMemNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_SELECT_MEMODETAILINFO_S01", objParams);

            return dtReturn;
        }

        #endregion  

        #region SelectSendMemoInfo : 보낸 메모 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectSendMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 보낸 메모 조회
         * Input    값 : SelectSendMemoInfo(페이지별 리스트 크기, 현재페이지, 로그인한사원ID)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectSendMemoInfo : 보낸 메모 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataSet SelectSendMemoInfo(int intPageSize, int intNowPage, string strCompNo, string strMemNo, string strKeyCd, string strKeyWord)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[6];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.StringEncoder(strCompNo);
            objParams[3] = TextLib.StringEncoder(strMemNo);
            objParams[4] = strKeyCd;
            objParams[5] = TextLib.StringEncoder(strKeyWord);

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_BRD_SELECT_MEMOINFO_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectSendMemoDetail : 보낸메모 상세보기

        /**********************************************************************************************
         * Mehtod   명 : SelectSendMemoDetail
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 보낸메모 상세보기
         * Input    값 : SelectSendMemoDetail(메모순번, 받는이사번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectSendMemoDetail : 보낸메모 상세보기
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strReceiveCompNo">받는이회사코드</param>
        /// <param name="strReceiveMemNo">받는이사번</param>
        /// <returns></returns>
        public static DataTable SelectSendMemoDetail(int intMemoSeq, string strReceiveCompNo, string strReceiveMemNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = intMemoSeq;
            objParams[1] = TextLib.StringEncoder(strReceiveCompNo);
            objParams[2] = TextLib.StringEncoder(strReceiveMemNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_SELECT_MEMOINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertSendMemoBoard : 메모 보내기

        /**********************************************************************************************
         * Mehtod   명 : InsertSendMemoBoard
         * 개   발  자 : 김범수
         * 생   성  일 : 2011-01-19
         * 용       도 : 메모 보내기
         * Input    값 : InsertSendMemo(제목, 내용, 사원번호, 받는사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertSendMemoBoard : 메모 보내기
        /// </summary>
        /// <param name="strMemoTitle">제목</param>
        /// <param name="strMemoContent">내용</param>
        /// <param name="strInsCompNo">받은회사코드</param>
        /// <param name="strInsMemNo">받은사원번호</param>
        /// <param name="strInsMemIP">입력자IP</param>
        /// <returns></returns>
        public static object[] InsertSendMemoBoard(string strMemoTitle, string strMemoContent, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objParams = new object[5];
            object[] objReturn = new object[2];

            objParams[0] = TextLib.StringEncoder(strMemoTitle);
            objParams[1] = TextLib.StringEncoder(strMemoContent);
            objParams[2] = TextLib.StringEncoder(strInsCompNo);
            objParams[3] = TextLib.StringEncoder(strInsMemNo);
            objParams[4] = strInsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_BRD_INSERT_MEMOINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertSendMemoDetail : 메모 보내기(권한)

        /**********************************************************************************************
         * Mehtod   명 : InsertSendMemoDetail
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-07
         * 용       도 : 메모 보내기(권한)
         * Input    값 : InsertSendMemoDetail(입력회사코드, 입력사번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// InsertSendMemoDetail : 메모 보내기(권한)
        /// </summary>
        /// <param name="strReceiveCompNo">입력회사코드</param>
        /// <param name="strReceiveMemNo">입력사번</param>
        /// <returns></returns>
        public static DataTable InsertSendMemoDetail(string strReceiveCompNo, string strReceiveMemNo)
        {
            object[] objParams = new object[2];
            DataTable dtReturn = new DataTable();

            objParams[0] = TextLib.StringEncoder(strReceiveCompNo);
            objParams[1] = TextLib.StringEncoder(strReceiveMemNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_BRD_INSERT_MEMOINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertMemoAddon : 메모 첨부파일 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertMemoAddon
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-07
         * 용       도 : 메모 첨부파일 등록
         * Input    값 : InsertMemoAddon(메모순번, 저장화일경로, 저장화일크기, 고유화일명)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMemoAddon : 메모 첨부파일 등록
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <returns></returns>
        public static object[] InsertMemoAddon(int intMemoSeq, string strFilePath, string strFileSize, string strFileRealNm)
        {
            object[] objParams = new object[4];
            object[] objReturns = new object[2];

            objParams[0] = intMemoSeq;
            objParams[1] = TextLib.StringEncoder(strFilePath);
            objParams[2] = strFileSize;
            objParams[3] = TextLib.StringEncoder(strFileRealNm);

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_INSERT_MEMOADDON_M00", objParams);

            return objReturns;
        }

        #endregion

        #region UpdateMemoInfo : 메모확인

        /**********************************************************************************************
         * Mehtod   명 : UpdateMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 메모확인
         * Input    값 : UpdateMemoInfo(메모순번, 회사번호, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMemoInfo : 메모확인
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strReceiveCompNo">회사번호</param>
        /// <param name="strReceiveMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] UpdateMemoInfo(int intMemoSeq, string strReceiveCompNo, string strReceiveMemNo)
        {
            object[] objParams = new object[3];
            object[] objReturns = new object[2];

            objParams[0] = intMemoSeq;
            objParams[1] = TextLib.StringEncoder(strReceiveCompNo);
            objParams[2] = TextLib.StringEncoder(strReceiveMemNo);

            objReturns = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MEMOINFO_M00", objParams);

            return objReturns;
        }

        #endregion  

        #region DeleteMemoInfo : 받은 메모 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 받은 메모 삭제
         * Input    값 : DeleteMemoInfo(메모순번, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 받은 메모 삭제
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strCompNo">회사번호</param>
        /// <param name="strReceiveMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] DeleteMemoInfo(int intMemoSeq, string strCompNo, string strReceiveMemNo)
        {
            object[] objParams = new object[3];
            object[] objReturns = new object[2];

            objParams[0] = intMemoSeq;
            objParams[1] = strCompNo;
            objParams[2] = TextLib.StringEncoder(strReceiveMemNo);

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_DELETE_MEMOINFO_M00", objParams);

            return objReturns;
        }

        #endregion

        #region DeleteFileInfomation : 메모 첨부파일 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteFileInfomation
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-07
         * 용       도 : 메모 첨부파일 삭제
         * Input    값 : DeleteFileInfomation(게시판글순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 보낸 메모 삭제
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strReceiveMemNo">받는사원번호</param>
        /// <returns></returns>
        public static object[] DeleteFileInfomation(int intMemoSeq)
        {
            object[] objParams = new object[1];
            object[] objReturns = new object[2];

            objParams[0] = intMemoSeq;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_DELETE_MEMOADDON_M00", objParams);

            return objReturns;
        }

        #endregion  
        
        #region DeleteSendMemoInfo : 보낸 메모 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteSendMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 보낸 메모 삭제
         * Input    값 : DeleteSendMemoInfo(메모순번, 받은회사코드, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteSendMemoInfo : 보낸 메모 삭제
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strReceiveCompNo">받은회사코드</param>
        /// <param name="strReceiveMemNo">받는사원번호</param>
        /// <returns></returns>
        public static object[] DeleteSendMemoInfo(int intMemoSeq, string strReceiveCompNo, string strReceiveMemNo)
        {
            object[] objParams = new object[3];
            object[] objReturns = new object[2];

            objParams[0] = intMemoSeq;
            objParams[1] = strReceiveCompNo;
            objParams[2] = strReceiveMemNo;

            objReturns = SPExecute.ExecReturnNo("KN_USP_BRD_DELETE_MEMOINFO_M01", objParams);

            return objReturns;
        }

        #endregion 
    }
}
