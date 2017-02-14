using System.Data;

using KN.Common.Base;

namespace KN.Config.Dac
{
    public class AccountMngDao
    {
        #region SelectAccountInfo : 보유 계좌 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-20
         * 용       도 : 보유 계좌 정보 조회
         * Input    값 : SelectAccountInfo(섹터코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectAccountInfo : 보유 계좌 정보 조회
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        public static DataTable SelectAccountInfo(string strCompCd)
        {
            object[] objParam = new object[1];
            DataTable dtReturn = new DataTable();

            objParam[0] = strCompCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTINFO_S00", objParam);

            return dtReturn;
        }

        public static DataTable SelectBankAccountInfo(string strCompCd)
        {
            object[] objParam = new object[1];
            DataTable dtReturn = new DataTable();

            objParam[0] = strCompCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTINFO_S04", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectBankBookInfo : 보유 계좌 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectBankBookInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 보유 계좌 정보 조회
         * Input    값 : SelectAccountInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectBankBookInfo : 보유 계좌 정보 조회
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <returns></returns>
        public static DataTable SelectBankBookInfo(string strCompCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strCompCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectChestNutAccountInfo : 체스넛보유 계좌 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectChestNutAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 체스넛보유 계좌 정보 조회
         * Input    값 : SelectChestNutAccountInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectChestNutAccountInfo : 체스넛보유 계좌 정보 조회
        /// </summary>
        public static DataTable SelectChestNutAccountInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTINFO_S02");

            return dtReturn;
        }

        #endregion

        #region SelectKNVinaAccountInfo : 경남비나 보유 계좌 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectKNVinaAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 경남비나 보유 계좌 정보 조회
         * Input    값 : SelectKNVinaAccountInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectKNVinaAccountInfo : 경남비나 보유 계좌 정보 조회
        /// </summary>
        public static DataTable SelectKNVinaAccountInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_ACCOUNTINFO_S03");

            return dtReturn;
        }

        #endregion

        #region InsertAccountInfo : 보유 계좌 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 보유 계좌 정보 등록
         * Input    값 : InsertAccountInfo(회사코드, 은행명, 계좌번호, 회계코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertAccountInfo : 보유 계좌 정보 등록
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strBankNm">은행명</param>
        /// <param name="strAccountNo">계좌번호</param>
        /// <param name="strAccCd">회계코드</param>
        /// <returns></returns>
        public static object[] InsertAccountInfo(string strCompCd, string strBankNm, string strAccountNo, string strAccCd)
        {
            object[] objParams = new object[4];
            object[] objReturn = new object[2];

            objParams[0] = strCompCd;
            objParams[1] = strBankNm;
            objParams[2] = strAccountNo;
            objParams[3] = strAccCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_ACCOUNTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateAccountInfo : 보유 계좌 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 보유 계좌 정보 수정
         * Input    값 : UpdateAccountInfo(회사코드, 계좌순번, 은행명, 계좌번호, 회계코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateAccountInfo : 보유 계좌 정보 수정
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strAccountCd">계좌순번</param>
        /// <param name="strBankNm">은행명</param>
        /// <param name="strAccountNo">계좌번호</param>
        /// <param name="strAccCd">회계코드</param>
        /// <returns></returns>
        public static object[] UpdateAccountInfo(string strCompCd, string strAccountCd, string strBankNm, string strAccountNo, string strAccCd)
        {
            object[] objParams = new object[5];
            object[] objReturn = new object[2];

            objParams[0] = strCompCd;
            objParams[1] = strAccountCd;
            objParams[2] = strBankNm;
            objParams[3] = strAccountNo;
            objParams[4] = strAccCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_ACCOUNTINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteAccountInfo : 보유 계좌 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 보유 계좌 정보 삭제
         * Input    값 : DeleteAccountInfo(회사코드, 계좌순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteAccountInfo : 보유 계좌 정보 삭제
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strAccountCd">계좌순번</param>
        /// <returns></returns>
        public static object[] DeleteAccountInfo(string strCompCd, string strAccountCd)
        {
            object[] objParams = new object[2];
            object[] objReturn = new object[2];

            objParams[0] = strCompCd;
            objParams[1] = strAccountCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_ACCOUNTINFO_M00", objParams);

            return objReturn;
        }

        #endregion
    }
}
