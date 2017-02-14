using System.Configuration;
using System.Data;

using KN.Common.Method.Lib;
using KN.Board.Dac;

namespace KN.Board.Biz
{
    public class MemoMngBlo
    {
        //파일 업로드 경로 설정
        public static readonly string strAppFileUpload = ConfigurationSettings.AppSettings["UploadServerFolder"].ToString();

        #region SpreadMemoMng : 메모 조회 (공통영역)

        /**********************************************************************************************
         * Mehtod   명 : SpreadMemoMng 
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 메모 조회 (공통영역)
         * Input    값 : SpreadMemoMng(사원번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMemoMng : 메모 조회 (공통영역)
        /// </summary>
        ///<param name="strMemNo">사원번호</param>
        /// <returns>DataSet</returns>
        public static DataTable SpreadMemoMng(string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemoMngDao.SelectMemoMng(strMemNo);

            return dtReturn;
        }

        #endregion

        #region SpreadMemoInfo : 메모 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-04
         * 용       도 : 메모 조회
         * Input    값 : SpreadMemoInfo(페이지별 리스트 크기, 현재페이지, 로그인한사원ID)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        public static DataSet SpreadMemoInfo(int intPageSize, int intNowPage, string strMemNo, string strKeyCd, string strKeyWord)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MemoMngDao.SelectMemoInfo(intPageSize, intNowPage, strMemNo, strKeyCd, strKeyWord);

            return dsReturn;
        }

        #endregion

        #region WatchMemoDetail : 메모 상세보기

        /**********************************************************************************************
         * Mehtod   명 : WatchMemoDetail
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-04
         * 용       도 : 메모 상세보기
         * Input    값 : SelectMemoDetail(메모순번, 회사코드, 사원번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchMemoDetail : 메모 상세보기
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <returns></returns>
        public static DataTable WatchMemoDetail(int intMemoSeq, string strCompNo, string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemoMngDao.SelectMemoDetail(intMemoSeq, strCompNo, strMemNo);

            return dtReturn;
        }

        #endregion   

        #region SpreadSendMemoInfo : 보낸 메모 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadSendMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 보낸 메모 조회
         * Input    값 : SpreadSendMemoInfo(페이지별 리스트 크기, 현재페이지, 로그인한사원ID)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadSendMemoInfo : 보낸 메모 조회
        /// </summary>
        /// <param name="intPageSize">페이지별 리스트 크기</param>
        /// <param name="intNowPage">현재페이지</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strKeyCd">검색코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <returns></returns>
        public static DataSet SpreadSendMemoInfo(int intPageSize, int intNowPage, string strCompNo, string strMemNo, string strKeyCd, string strKeyWord)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MemoMngDao.SelectSendMemoInfo(intPageSize, intNowPage, strCompNo, strMemNo, strKeyCd, strKeyWord);

            return dsReturn;
        }

        #endregion

        #region WatchSendMemoDetail : 보낸메모 상세보기

        /**********************************************************************************************
         * Mehtod   명 : WatchSendMemoDetail
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 보낸메모 상세보기
         * Input    값 : WatchSendMemoDetail(메모순번, 받는이사번)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchSendMemoDetail : 보낸메모 상세보기
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strReceiveCompNo">받는이회사코드</param>
        /// <param name="strReceiveMemNo">받는이사번</param>
        /// <returns></returns>
        public static DataTable WatchSendMemoDetail(int intMemoSeq, string strReceiveCompNo, string strReceiveMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemoMngDao.SelectSendMemoDetail(intMemoSeq, strReceiveCompNo, strReceiveMemNo);

            return dtReturn;
        }

        #endregion

        #region RegistrySendMemoBoard : 메모 보내기

        /**********************************************************************************************
         * Mehtod   명 : RegistrySendMemoBoard
         * 개   발  자 : 김범수
         * 생   성  일 : 2011-01-19
         * 용       도 : 메모 보내기
         * Input    값 : RegistrySendMemo(제목, 내용, 사원번호, 받는사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistrySendMemoBoard : 메모 보내기
        /// </summary>
        /// <param name="strMemoTitle">제목</param>
        /// <param name="strMemoContent">내용</param>
        /// <param name="strInsCompNo">받은회사코드</param>
        /// <param name="strInsMemNo">받은사원번호</param>
        /// <param name="strInsMemIP">입력자IP</param>
        /// <returns></returns>
        public static object[] RegistrySendMemoBoard(string strMemoTitle, string strMemoContent, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] dtReturn = new object[2];

            dtReturn = MemoMngDao.InsertSendMemoBoard(strMemoTitle, strMemoContent, strInsCompNo, strInsMemNo, strInsMemIP);

            return dtReturn;
        }

        #endregion

        #region RegistrySendMemoDetail : 메모보내기(내용권한)

        /**********************************************************************************************
         * Mehtod   명 : RegistrySendMemoDetail
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-07
         * 용       도 : 메모보내기
         * Input    값 : RegistrySendMemoDetail( 받는사원번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistrySendMemoDetail : 메모 보내기(권한)
        /// </summary>
        /// <param name="strReceiveCompNo">입력회사코드</param>
        /// <param name="strReceiveMemNo">입력사번</param>
        /// <returns></returns>
        public static DataTable RegistrySendMemoDetail(string strReceiveCompNo, string strReceiveMemNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MemoMngDao.InsertSendMemoDetail(strReceiveCompNo, strReceiveMemNo);

            return dtReturn;
        }

        #endregion

        #region RegistryMemoAddon : 메모 첨부파일 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryMemoAddon
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-07
         * 용       도 : 메모 첨부파일 등록
         * Input    값 : RegistryMemoAddon(순번, 저장화일경로, 저장화일크기, 고유화일명)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMemoAddon : 메모 첨부파일 등록
        /// </summary>
        /// <param name="intMemoSeq">순번</param>
        /// <param name="strFilePath">저장화일경로</param>
        /// <param name="strFileSize">저장화일크기</param>
        /// <param name="strFileRealNm">고유화일명</param>
        /// <returns></returns>
        public static object[] RegistryMemoAddon(int intMemoSeq, string strFilePath, string strFileSize, string strFileRealNm)
        {
            object[] objReturns = new object[2];

            objReturns = MemoMngDao.InsertMemoAddon(intMemoSeq, strFilePath, strFileSize, strFileRealNm);

            return objReturns;
        }

        #endregion

        #region ModifyMemoInfo : 메모확인

        /**********************************************************************************************
         * Mehtod   명 : ModifyMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 메모확인
         * Input    값 : ModifyMemoInfo(메모순번, 회사번호, 사원번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMemoInfo : 메모확인
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strReceiveCompNo">회사번호</param>
        /// <param name="strReceiveMemNo">사원번호</param>
        /// <returns></returns>
        public static object[] ModifyMemoInfo(int intMemoSeq, string strReceiveCompNo, string strReceiveMemNo)
        {
            object[] objParams = new object[3];
            object[] objReturns = new object[2];

            objReturns = MemoMngDao.UpdateMemoInfo(intMemoSeq, strReceiveCompNo, strReceiveMemNo);

            return objReturns;
        }

        #endregion  

        #region RemoveMemoInfo : 받은 메모 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 받은 메모 삭제
         * Input    값 : RemoveMemoInfo(메모순번, 회사코드, 사원번호, 파일경로)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// 받은 메모 삭제
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strReceiveMemNo">사원번호</param>
        /// <param name="strFilePath">파일경로</param>
        /// <returns></returns>
        public static object[] RemoveMemoInfo(int intMemoSeq, string strCompNo, string strReceiveMemNo, string strFilePath)
        {
            object[] objReturn = new object[2];

            // 게시판 글 삭제
            objReturn = MemoMngDao.DeleteMemoInfo(intMemoSeq, strCompNo, strReceiveMemNo);

            if (objReturn != null)
            {
                // 게시판 첨부파일 삭제
                FileLib.FileDelete(strAppFileUpload + strFilePath);

                // 게시판 첨부파일 정보 삭제
                objReturn = MemoMngDao.DeleteFileInfomation(intMemoSeq);
            }
            else
            {
                objReturn = null;
            }

            return objReturn;
        }

        #endregion

        #region RemoveSendMemoInfo : 보낸 메모 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveSendMemoInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-05
         * 용       도 : 보낸 메모 삭제
         * Input    값 : RemoveSendMemoInfo(intMemoSeq, strReceiveCompNo, strReceiveMemNo)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveSendMemoInfo : 보낸 메모 삭제
        /// </summary>
        /// <param name="intMemoSeq">메모순번</param>
        /// <param name="strReceiveCompNo">받은회사코드</param>
        /// <param name="strReceiveMemNo">받는사원번호</param>
        /// <returns></returns>
        public static object[] RemoveSendMemoInfo(int intMemoSeq, string strReceiveCompNo, string strReceiveMemNo)
        {
            object[] objReturn = new object[2];

            objReturn = MemoMngDao.DeleteSendMemoInfo(intMemoSeq, strReceiveCompNo, strReceiveMemNo);

            return objReturn;
        }

        #endregion
    }
}
