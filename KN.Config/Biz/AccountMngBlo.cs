using System.Data;

using KN.Config.Dac;

namespace KN.Config.Biz
{
    public class AccountMngBlo
    {
        #region SpreadAccountInfo : 보유 계좌 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-20
         * 용       도 : 보유 계좌 정보 조회
         * Input    값 : SpreadAccountInfo(회사코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadAccountInfo : 보유 계좌 정보 조회
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <returns></returns>
        public static DataTable SpreadAccountInfo(string strCompCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = AccountMngDao.SelectAccountInfo(strCompCd);

            return dtReturn;
        }

        //BaoTv
        public static DataTable SpreadBankAccountInfo(string strCompCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = AccountMngDao.SelectBankAccountInfo(strCompCd);

            return dtReturn;
        }

        #endregion

        #region SpreadBankBookInfo : 보유 계좌 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadBankBookInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 보유 계좌 정보 조회
         * Input    값 : SpreadBankBookInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadBankBookInfo : 보유 계좌 정보 조회
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <returns></returns>
        public static DataTable SpreadBankBookInfo(string strCompCd) 
        {
            DataTable dtReturn = new DataTable();

            dtReturn = AccountMngDao.SelectBankBookInfo(strCompCd);

            return dtReturn;
        }

        #endregion

        #region SpreadChestNutAccountInfo : 체스넛 보유 계좌 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadChestNutAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 체스넛 보유 계좌 정보 조회
         * Input    값 : SpreadChestNutAccountInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadChestNutAccountInfo : 체스넛 보유 계좌 정보 조회
        /// </summary>
        /// <returns></returns>
        public static DataTable SpreadChestNutAccountInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = AccountMngDao.SelectChestNutAccountInfo();

            return dtReturn;
        }

        #endregion

        #region SpreadKNVinaAccountInfo : 경남비나 보유 계좌 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadKNVinaAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 경남비나 보유 계좌 정보 조회
         * Input    값 : SpreadKNVinaAccountInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadKNVinaAccountInfo : 경남비나 보유 계좌 정보 조회
        /// </summary>
        /// <returns></returns>
        public static DataTable SpreadKNVinaAccountInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = AccountMngDao.SelectKNVinaAccountInfo();

            return dtReturn;
        }

        #endregion

        #region RegistryAccountInfo : 보유 계좌 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 보유 계좌 정보 등록
         * Input    값 : RegistryAccountInfo(회사코드, 은행명, 계좌번호, 회계코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryAccountInfo : 보유 계좌 정보 등록
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strBankNm">은행명</param>
        /// <param name="strAccountNo">계좌번호</param>
        /// <param name="strAccCd">회계코드</param>
        /// <returns></returns>
        public static object[] RegistryAccountInfo(string strCompCd, string strBankNm, string strAccountNo, string strAccCd)
        {
            object[] objReturn = new object[2];

            objReturn = AccountMngDao.InsertAccountInfo(strCompCd, strBankNm, strAccountNo, strAccCd);

            return objReturn;
        }

        #endregion

        #region ModifyAccountInfo : 보유 계좌 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 보유 계좌 정보 수정
         * Input    값 : ModifyAccountInfo(회사코드, 계좌순번, 은행명, 계좌번호, 회계코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyAccountInfo : 보유 계좌 정보 수정
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strAccountCd">계좌순번</param>
        /// <param name="strBankNm">은행명</param>
        /// <param name="strAccountNo">계좌번호</param>
        /// <param name="strAccCd">회계코드</param>
        /// <returns></returns>
        public static object[] ModifyAccountInfo(string strCompCd, string strAccountCd, string strBankNm, string strAccountNo, string strAccCd)
        {
            object[] objReturn = new object[2];

            objReturn = AccountMngDao.UpdateAccountInfo(strCompCd, strAccountCd, strBankNm, strAccountNo, strAccCd);

            return objReturn;
        }

        #endregion

        #region RemoveAccountInfo : 보유 계좌 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveAccountInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-12-24
         * 용       도 : 보유 계좌 정보 삭제
         * Input    값 : RemoveAccountInfo(회사코드, 계좌순번)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveAccountInfo : 보유 계좌 정보 삭제
        /// </summary>
        /// <param name="strCompCd">회사코드</param>
        /// <param name="strAccountCd">계좌순번</param>
        /// <returns></returns>
        public static object[] RemoveAccountInfo(string strCompCd, string strAccountCd)
        {
            object[] objReturn = new object[2];

            objReturn = AccountMngDao.DeleteAccountInfo(strCompCd, strAccountCd);

            return objReturn;
        }

        #endregion
    }
}
