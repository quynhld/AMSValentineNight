using System.Data;

using KN.Config.Dac;
using KN.Config.Ent;

namespace KN.Config.Biz
{
    public class MenuMngBlo
    {
        #region SpreadMenuInfo : 메뉴 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : 메뉴 조회
         * Input    값 : SpreadMenuInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMenuInfo : 메뉴 조회
        /// </summary>
        /// <returns></returns>
        public static DataTable SpreadMenuInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MenuMngDao.SelectMenuInfo();

            return dtReturn;
        }

        #endregion

        #region SpreadMenuInfo : 메뉴 조회 (권한관리용)

        /**********************************************************************************************
         * Mehtod   명 : SpreadMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-16
         * 용       도 : 메뉴 조회 (권한관리용)
         * Input    값 : SpreadMenuInfo(언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadMenuInfo : 메뉴 조회 (권한관리용)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SpreadMenuInfo(string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MenuMngDao.SelectMenuInfo(strLangCd);

            return dtReturn;
        }

        #endregion

        #region WatchMenuLink : 메뉴링크 일부 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchMenuLink
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴링크일부 조회
         * Input    값 : WatchMenuLink(메뉴순번, 링크코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchMenuLink : 메뉴링크 일부 조회
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strLinkCd">링크코드</param>
        /// <returns></returns>
        public static DataTable WatchMenuLink(int intMenuSeq, string strLinkCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MenuMngDao.SelectMenuLink(intMenuSeq, strLinkCd);

            return dtReturn;
        }

        #endregion

        #region WatchMenuParam : 메뉴파라미터 일부 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchMenuParam
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴파라미터 일부 조회
         * Input    값 : WatchMenuParam(메뉴순번, 파라미터코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchMenuParam : 메뉴파라미터 일부 조회
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strParamCd">파라미터코드</param>
        /// <returns></returns>
        public static DataTable WatchMenuParam(int intMenuSeq, string strParamCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = MenuMngDao.SelectMenuParam(intMenuSeq, strParamCd);

            return dtReturn;
        }

        #endregion

        #region WatchMenuMng : 메뉴 상세 조회 (권한관리용)

        /**********************************************************************************************
         * Mehtod   명 : WatchMenuMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-16
         * 용       도 : 메뉴 상세 조회 (권한관리용)
         * Input    값 : WatchMenuMng(언어코드, 메뉴순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// WatchMenuMng : 메뉴 상세 조회 (권한관리용)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <returns></returns>
        public static DataSet WatchMenuMng(string strLangCd, int intMenuSeq)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = MenuMngDao.SelectMenuMng(strLangCd, intMenuSeq);

            return dsReturn;
        }

        #endregion

        #region RegistryMenuInfo : 메뉴 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : 메뉴 등록
         * Input    값 : RegistryMenuInfo(MenuDs.MenuInfo 객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMenuInfo : 메뉴 등록
        /// </summary>
        /// <param name="miMenu">MenuDs.MenuInfo 객체</param>
        /// <returns></returns>
        public static object[] RegistryMenuInfo(MenuDs.MenuInfo miMenu)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.InsertMenuInfo(miMenu);

            return objReturn;
        }

        #endregion

        #region RegistryMenuLink : 메뉴 링크 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryMenuLink
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 링크 등록
         * Input    값 : RegistryMenuLink(메뉴순번, 링크코드, 링크페이지명)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMenuLink : 메뉴 링크 등록
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strLinkCd">링크코드</param>
        /// <param name="strLinkPageNm">링크페이지명</param>
        /// <returns></returns>
        public static object[] RegistryMenuLink(int intMenuSeq, string strLinkCd, string strLinkPageNm)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.InsertMenuLink(intMenuSeq, strLinkCd, strLinkPageNm);

            return objReturn;
        }

        #endregion

        #region RegistryMenuParam : 메뉴 파라미터 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryMenuParam
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 파라미터 등록
         * Input    값 : RegistryMenuParam(메뉴순번, 파라미터코드, 파라미터명)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryMenuParam : 메뉴 파라미터 등록
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strParamCd">파라미터코드</param>
        /// <param name="strParamNm">파라미터명</param>
        /// <returns></returns>
        public static object[] RegistryMenuParam(int intMenuSeq, string strParamCd, string strParamNm)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.InsertMenuParam(intMenuSeq, strParamCd, strParamNm);

            return objReturn;
        }

        #endregion

        #region ModifyMenuInfo : 메뉴 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 메뉴 수정
         * Input    값 : ModifyMenuInfo()
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// ModifyMenuInfo : 메뉴 수정
        /// </summary>
        /// <returns></returns>
        public static object[] ModifyMenuInfo(MenuDs.MenuInfo miMenu)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.UpdateMenuInfo(miMenu);

            return objReturn;
        }

        #endregion

        #region ModifyMenuLink : 메뉴 링크 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMenuLink
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 링크 수정
         * Input    값 : ModifyMenuLink(메뉴순번, 기존링크코드, 새링크코드, 링크페이지명)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMenuLink : 메뉴 링크 수정
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strOldLinkCd">기존 링크코드</param>
        /// <param name="strNewLinkCd">새 링크코드</param>
        /// <param name="strLinkPageNm">링크페이지명</param>
        /// <returns></returns>
        public static object[] ModifyMenuLink(int intMenuSeq, string strOldLinkCd, string strNewLinkCd, string strLinkPageNm)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.DeleteMenuLink(intMenuSeq, strOldLinkCd);

            if (objReturn != null)
            {
                objReturn = MenuMngDao.InsertMenuLink(intMenuSeq, strNewLinkCd, strLinkPageNm);
            }

            return objReturn;
        }

        #endregion

        #region ModifyMenuParam : 메뉴 파라미터 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMenuParam
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 파라미터 수정
         * Input    값 : ModifyMenuParam(메뉴순번, 기존파라미터코드, 새파라미터코드, 파라미터명)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyMenuParam : 메뉴 파라미터 수정
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strOldParamCd">기존 파라미터코드</param>
        /// <param name="strNewParamCd">새 파라미터코드</param>
        /// <param name="strParamNm">파라미터명</param>
        /// <returns></returns>
        public static object[] ModifyMenuParam(int intMenuSeq, string strOldParamCd, string strNewParamCd, string strParamNm)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.DeleteMenuParam(intMenuSeq, strOldParamCd);

            if (objReturn != null)
            {
                objReturn = MenuMngDao.InsertMenuParam(intMenuSeq, strNewParamCd, strParamNm);
            }

            return objReturn;
        }

        #endregion

        #region ModifyMenuInfo : 메뉴 권한 수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-18
         * 용       도 : 메뉴 권한 수정
         * Input    값 : ModifyMenuInfo(메뉴순번, 읽기권한, 쓰기권한, 수정삭제권한, 수정기업코드, 수정사번, 수정IP)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// ModifyMenuInfo : 메뉴 권한 수정
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strReadAuth">읽기권한</param>
        /// <param name="strWriteAuth">쓰기권한</param>
        /// <param name="strModAuth">수정삭제권한</param>
        /// <param name="strModCompCd">수정기업코드</param>
        /// <param name="strModMemNo">수정사번</param>
        /// <param name="strModMemIP">수정IP</param>
        /// <returns></returns>
        public static object[] ModifyMenuInfo(int intMenuSeq, string strReadAuth, string strWriteAuth, string strModAuth, string strModCompCd, string strModMemNo, string strModMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.UpdateMenuInfo(intMenuSeq, strReadAuth, strWriteAuth, strModAuth, strModCompCd, strModMemNo, strModMemIP);

            return objReturn;
        }

        #endregion

        #region RemoveMenuMng : 메뉴 관련 사항 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMenuMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-15
         * 용       도 : 메뉴 관련 사항 삭제
         * Input    값 : RemoveMenuInfo(메뉴순번)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// RemoveMenuMng : 메뉴 관련 사항 삭제
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <returns></returns>
        public static object[] RemoveMenuMng(int intMenuSeq)
        {
            object[] objReturn = new object[2];

            // 메뉴 링크정보 삭제
            objReturn = MenuMngDao.DeleteMenuLink(intMenuSeq);

            if (objReturn != null)
            {
                // 메뉴 파라미터 정보 삭제
                objReturn = MenuMngDao.DeleteMenuParam(intMenuSeq);

                if (objReturn != null)
                {
                    // 메뉴 정보 삭제
                    objReturn = MenuMngDao.DeleteMenuInfo(intMenuSeq);
                }
            }

            return objReturn;
        }

        #endregion

        #region RemoveMenuLink : 메뉴 링크 일부 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMenuLink
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 링크 일부 삭제
         * Input    값 : RemoveMenuLink(메뉴순번, 링크코드)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// RemoveMenuLink : 메뉴 링크 일부 삭제
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strLinkCd">링크코드</param>
        /// <returns></returns>
        public static object[] RemoveMenuLink(int intMenuSeq, string strLinkCd)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.DeleteMenuLink(intMenuSeq, strLinkCd);

            return objReturn;
        }

        #endregion

        #region RemoveMenuParam : 메뉴 파라미터 일부 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveMenuParam
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 파라미터 일부 삭제
         * Input    값 : RemoveMenuParam(메뉴순번, 파라미터코드)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// RemoveMenuParam : 메뉴 파라미터 일부 삭제
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strParamCd">파라미터코드</param>
        /// <returns></returns>
        public static object[] RemoveMenuParam(int intMenuSeq, string strParamCd)
        {
            object[] objReturn = new object[2];

            objReturn = MenuMngDao.DeleteMenuParam(intMenuSeq, strParamCd);

            return objReturn;
        }

        #endregion
    }
}
