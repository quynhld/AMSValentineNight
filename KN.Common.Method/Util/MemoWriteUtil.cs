using System.Data;
using System.Text;
using System.Web.UI.WebControls;

using KN.Common.Base.Code;
using KN.Common.Method.Common;

namespace KN.Common.Method.Util
{
    /// <summary>
    /// 쪽지발송유틸
    /// </summary>
    public class MemoWriteUtil
    {
        #region MakeGroupDdl : 그룹 DropdownList를 생성

        /**********************************************************************************************
         * Mehtod   명 : MakeGroupDdl
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-06
         * 용       도 : 그룹 DropdownList를 생성
         * Input    값 : MakeMainCdDdlTitle(언어코드)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// 공통코드(메인코드)로 타이틀 있는 DropdownList를 생성
        /// </summary>
        /// <param name="ddlParamNm">DropDownList 객체</param>
        /// <param name="strAuthLvl">권한그룹코드</param>
        /// <param name="strLangCd">언어코드</param>
        public static void MakeGroupDdl(DropDownList ddlParamNm, string strAuthLvl, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            // KN_USP_COMM_SELECT_GRPCD_S01
            dtReturn = CommMemoMng.SelectGrpInfo(strAuthLvl, strLangCd);
            
            ddlParamNm.Items.Clear();           

            foreach (DataRow dr in dtReturn.Select())
            {
                ddlParamNm.Items.Add(new ListItem(dr["GroupNm"].ToString(), dr["MemAuthTy"].ToString()));
            }
        }

        #endregion

        #region SpreadGroupMemInfo : 그룹에 속한 Member 가져오기

        /**********************************************************************************************
         * Mehtod   명 : SpreadGroupMemInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-06
         * 용       도 : 그룹에 속한 Member 가져오기
         * Input    값 : SpreadGroupMemInfo(그룹권한, 권한타입, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadGroupMemInfo : 그룹에 속한 Member 가져오기
        /// </summary>
        /// <param name="strAuthLvl">그룹권한</param>
        /// <param name="strMemAuthTy">권한타입</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadGroupMemInfo(string strAuthLvl, string strMemAuthTy, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommMemoMng.SelectGroupMemInfo(strAuthLvl, strMemAuthTy, strLangCd);
    
            return dtReturn;
        }

        #endregion        

        #region SpreadAddMember : 메모 대상자 추가

        /**********************************************************************************************
         * Mehtod   명 : SpreadAddMember
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-06
         * 용       도 : 메모 대상자 추가
         * Input    값 : SpreadGroupMemInfo(회사코드, 사원번호)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadAddMember : 메모 대상자 추가
        /// </summary>
        /// <param name="strComNo">회사코드</param>
        /// <param name="strMemno">사원번호</param>
        /// <returns></returns>
        public static DataTable SpreadAddMember(string strComNo, string strMemno)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = CommMemoMng.SelectAddMember(strComNo, strMemno);    

            return dtReturn;
        }

        #endregion        

        #region RegistrySendMemo : 메모보내기(시스템)

        /**********************************************************************************************
         * Mehtod   명 : RegistrySendMemo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-07
         * 용       도 : 메모보내기(시스템)
         * Input    값 : RegistrySendMemo(제목, 내용, 사원번호, 받는사원번호, 사원IP)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// RegistrySendMemo : 메모보내기(시스템)
        /// </summary>
        /// <param name="strMemoTitle">제목</param>
        /// <param name="strMemoContent">내용</param>
        /// <param name="strInsCompNo">받은회사코드</param>
        /// <param name="strInsMemNo">받은사원번호</param>
        /// <param name="strInsMemIP">입력자IP</param>
        /// <returns></returns>
        public static object[] RegistrySendMemo(string strMemoTitle, string strMemoContent, string strInsCompNo, string strInsMemNo, string strInsMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = CommMemoMng.InsertSendMemo(strMemoTitle, strMemoContent, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
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
            object[] objReturn = new object[2];

            objReturn = CommMemoMng.InsertSendMemoBoard(strMemoTitle, strMemoContent, strInsCompNo, strInsMemNo, strInsMemIP);

            return objReturn;
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

            dtReturn = CommMemoMng.InsertSendMemoDetail(strReceiveCompNo, strReceiveMemNo);

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

            objReturns = CommMemoMng.InsertMemoAddon(intMemoSeq, strFilePath, strFileSize, strFileRealNm);

            return objReturns;
        }

        #endregion
    }
}
