using System;
using System.Data;
using System.Text;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Lib;

namespace KN.Common.Method.Common
{
    public class CommMemoMng
    {
        #region SelectGrpInfo : 그룹 DropdownList 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectGrpInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-06
         * 용       도 : 그룹 DropdownList 조회
         * Input    값 : SelectGrpInfo(권한그룹코드, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMainCdWithTitle : 그룹 DropdownList 조회
        /// </summary>
        /// <param name="strAuthLvl">권한그룹코드</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns>DataTable</returns>
        public static DataTable SelectGrpInfo(string strAuthLvl, string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[2];

            objParams[0] = strAuthLvl;
            objParams[1] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_GRPCD_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectGroupMemInfo : 그룹에 속한 Member 가져오기

        /**********************************************************************************************
         * Mehtod   명 : SelectGroupMemInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-06
         * 용       도 : 그룹에 속한 Member 가져오기
         * Input    값 : SelectGroupMemInfo(그룹권한, 권한타입, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectGroupMemInfo : 그룹에 속한 Member 가져오기
        /// </summary>
        /// <param name="strAuthLvl">그룹권한</param>
        /// <param name="strMemAuthTy">권한타입</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectGroupMemInfo(string strAuthLvl, string strMemAuthTy, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strAuthLvl;
            objParams[1] = strMemAuthTy;
            objParams[2] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_GRPCD_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectAddMember : 메모 대상자 추가

        /**********************************************************************************************
         * Mehtod   명 : SelectAddMember
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-06
         * 용       도 : 메모 대상자 추가
         * Input    값 : SelectAddMember(회사코드, 사원번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectAddMember : 메모 대상자 추가
        /// </summary>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <returns></returns>
        public static DataTable SelectAddMember(string strCompNo, string strMemNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = TextLib.StringEncoder(strCompNo);
            objParams[1] = TextLib.StringEncoder(strMemNo);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_COMM_SELECT_GRPCD_S03", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertSendMemo : 메모보내기(시스템)

        /**********************************************************************************************
         * Mehtod   명 : InsertSendMemo
         * 개   발  자 : 김범수
         * 생   성  일 : 2011-01-19
         * 용       도 : 메모 보내기
         * Input    값 : InsertSendMemo(제목, 내용, 사원번호, 받는사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertSendMemo : 메모 보내기
        /// </summary>
        /// <param name="strMemoTitle">제목</param>
        /// <param name="strMemoContent">내용</param>
        /// <param name="strInsCompNo">받은회사코드</param>
        /// <param name="strInsMemNo">받은사원번호</param>
        /// <param name="strInsMemIP">입력자IP</param>
        /// <returns></returns>
        public static object[] InsertSendMemo(string strMemoTitle, string strMemoContent, string strInsCompNo, string strInsMemNo, string strInsMemIP)
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
    }
}
