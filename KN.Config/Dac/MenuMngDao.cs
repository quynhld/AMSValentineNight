using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

using KN.Config.Ent;

namespace KN.Config.Dac
{
    public class MenuMngDao
    {
        #region SelectMenuInfo : 메뉴 목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : 메뉴 목록 조회
         * Input    값 : SelectMenuInfo()
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMenuInfo : 메뉴 목록 조회
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectMenuInfo()
        {
            DataTable dtReturn = new DataTable();

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MENUINFO_S00");

            return dtReturn;
        }

        #endregion

        #region SelectMenuInfo : 메뉴 목록 조회 (권한관리용)

        /**********************************************************************************************
         * Mehtod   명 : SelectMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-16
         * 용       도 : 메뉴 목록 조회 (권한관리용)
         * Input    값 : SelectMenuInfo(언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMenuInfo : 메뉴 목록 조회 (권한관리용)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectMenuInfo(string strLangCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[1];

            objParam[0] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MENUINFO_S01", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMenuLink : 메뉴링크 일부 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMenuLink
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴링크 일부 조회
         * Input    값 : SelectMenuLink(메뉴순번, 링크코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMenuLink : 메뉴링크 일부 조회
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strLinkCd">링크코드</param>
        /// <returns></returns>
        public static DataTable SelectMenuLink(int intMenuSeq, string strLinkCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[2];

            objParam[0] = intMenuSeq;
            objParam[1] = strLinkCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MENULINK_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMenuParam : 메뉴파라미터 일부 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectMenuParam
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴파라미터 일부 조회
         * Input    값 : SelectMenuParam(메뉴순번, 파라미터코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectMenuParam : 메뉴파라미터 일부 조회
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strParamCd">파라미터코드</param>
        /// <returns></returns>
        public static DataTable SelectMenuParam(int intMenuSeq, string strParamCd)
        {
            DataTable dtReturn = new DataTable();
            object[] objParam = new object[2];

            objParam[0] = intMenuSeq;
            objParam[1] = strParamCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_MNG_SELECT_MENUPARAM_S00", objParam);

            return dtReturn;
        }

        #endregion

        #region SelectMenuMng : 메뉴 상세 조회 (권한관리용)

        /**********************************************************************************************
         * Mehtod   명 : SelectMenuMng
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-16
         * 용       도 : 메뉴 상세 조회 (권한관리용)
         * Input    값 : SelectMenuMng(언어코드, 메뉴순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectMenuMng : 메뉴 상세 조회 (권한관리용)
        /// </summary>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <returns></returns>
        public static DataSet SelectMenuMng(string strLangCd, int intMenuSeq)
        {
            DataSet dsReturn = new DataSet();
            object[] objParam = new object[2];

            objParam[0] = strLangCd;
            objParam[1] = intMenuSeq;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_MNG_SELECT_MENUMNG_S00", objParam);

            return dsReturn;
        }

        #endregion

        #region InsertMenuInfo : 메뉴 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-09
         * 용       도 : 메뉴 등록
         * Input    값 : InsertMenuInfo(MenuDs.MenuInfo 객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMenuInfo : 메뉴 등록
        /// </summary>
        /// <param name="miMenu">MenuDs.MenuInfo 객체</param>
        /// <returns></returns>
        public static object[] InsertMenuInfo(MenuDs.MenuInfo miMenu)
        {
            object[] objParams = new object[16];
            object[] objReturn = new object[2];

            objParams[0] = miMenu.Depth1;
            objParams[1] = miMenu.Depth2;
            objParams[2] = miMenu.Depth3;
            objParams[3] = miMenu.Depth4;
            objParams[4] = TextLib.StringEncoder(miMenu.MenuTitle);
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(miMenu.MenuUrl));
            objParams[6] = miMenu.HiddenYn;
            objParams[7] = miMenu.ReadAuth;
            objParams[8] = miMenu.WriteAuth;
            objParams[9] = miMenu.ModDelAuth;
            objParams[10] = miMenu.BoardYn;
            objParams[11] = TextLib.MakeNullToEmpty(miMenu.BoardTy);
            objParams[12] = TextLib.MakeNullToEmpty(miMenu.BoardCd);
            objParams[13] = miMenu.InsCompNo;
            objParams[14] = miMenu.InsMemNo;
            objParams[15] = miMenu.InsMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MENUINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertMenuLink : 메뉴 링크 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertMenuLink
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 링크 등록
         * Input    값 : InsertMenuLink(메뉴순번, 링크코드, 링크페이지명)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMenuLink : 메뉴 링크 등록
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strLinkCd">링크코드</param>
        /// <param name="strLinkPageNm">링크페이지명</param>
        /// <returns></returns>
        public static object[] InsertMenuLink(int intMenuSeq, string strLinkCd, string strLinkPageNm)
        {
            object[] objParams = new object[3];
            object[] objReturn = new object[2];

            objParams[0] = intMenuSeq;
            objParams[1] = strLinkCd;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strLinkPageNm));

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MENULINK_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertMenuParam : 메뉴 파라미터 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertMenuParam
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 파라미터 등록
         * Input    값 : InsertMenuParam(메뉴순번, 파라미터코드, 파라미터명)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertMenuParam : 메뉴 파라미터 등록
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strParamCd">파라미터코드</param>
        /// <param name="strParamNm">파라미터명</param>
        /// <returns></returns>
        public static object[] InsertMenuParam(int intMenuSeq, string strParamCd, string strParamNm)
        {
            object[] objParams = new object[3];
            object[] objReturn = new object[2];

            objParams[0] = intMenuSeq;
            objParams[1] = strParamCd;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strParamNm));

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_INSERT_MENUPARAM_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateMenuInfo : 메뉴 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 메뉴 수정
         * Input    값 : UpdateMenuInfo(MenuDs.MenuInfo 객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMenuInfo : 메뉴 수정
        /// </summary>
        /// <param name="miMenu">MenuDs.MenuInfo 객체</param>
        /// <returns></returns>
        public static object[] UpdateMenuInfo(MenuDs.MenuInfo miMenu)
        {
            object[] objParams = new object[17];
            object[] objReturn = new object[2];

            objParams[0] = miMenu.MenuSeq;
            objParams[1] = miMenu.Depth1;
            objParams[2] = miMenu.Depth2;
            objParams[3] = miMenu.Depth3;
            objParams[4] = miMenu.Depth4;
            objParams[5] = TextLib.StringEncoder(miMenu.MenuTitle);
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(miMenu.MenuUrl));
            objParams[7] = miMenu.HiddenYn;
            objParams[8] = miMenu.ReadAuth;
            objParams[9] = miMenu.WriteAuth;
            objParams[10] = miMenu.ModDelAuth;
            objParams[11] = miMenu.BoardYn;
            objParams[12] = TextLib.MakeNullToEmpty(miMenu.BoardTy);
            objParams[13] = TextLib.MakeNullToEmpty(miMenu.BoardCd);
            objParams[14] = miMenu.ModCompNo;
            objParams[15] = miMenu.ModMemNo;
            objParams[16] = miMenu.ModMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MENUINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateMenuInfo : 메뉴 권한 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-18
         * 용       도 : 메뉴 권한 수정
         * Input    값 : UpdateMenuInfo(메뉴순번, 읽기권한, 쓰기권한, 수정삭제권한, 수정기업코드, 수정사번, 수정IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateMenuInfo : 메뉴 권한 수정
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strReadAuth">읽기권한</param>
        /// <param name="strWriteAuth">쓰기권한</param>
        /// <param name="strModAuth">수정삭제권한</param>
        /// <param name="strModCompCd">수정기업코드</param>
        /// <param name="strModMemNo">수정사번</param>
        /// <param name="strModMemIP">수정IP</param>
        /// <returns></returns>
        public static object[] UpdateMenuInfo(int intMenuSeq, string strReadAuth, string strWriteAuth, string strModAuth, string strModCompCd, string strModMemNo, string strModMemIP)
        {
            object[] objParams = new object[7];
            object[] objReturn = new object[2];

            objParams[0] = intMenuSeq;
            objParams[1] = strReadAuth;
            objParams[2] = strWriteAuth;
            objParams[3] = strModAuth;
            objParams[4] = strModCompCd;
            objParams[5] = strModMemNo;
            objParams[6] = strModMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_UPDATE_MENUINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteMenuLink : 메뉴 링크 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMenuLink
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-15
         * 용       도 : 메뉴 링크 삭제
         * Input    값 : DeleteMenuLink(메뉴순번)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// DeleteMenuLink : 메뉴 목록 삭제
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <returns></returns>
        public static object[] DeleteMenuLink(int intMenuSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = intMenuSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_MENULINK_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteMenuLink : 메뉴 링크 일부 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMenuLink
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 링크 일부 삭제
         * Input    값 : DeleteMenuLink(메뉴순번, 링크코드)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// DeleteMenuLink : 메뉴 링크 일부 삭제
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strLinkCd">링크코드</param>
        /// <returns></returns>
        public static object[] DeleteMenuLink(int intMenuSeq, string strLinkCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intMenuSeq;
            objParams[1] = strLinkCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_MENULINK_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteMenuParam : 메뉴 파라미터 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMenuParam
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-15
         * 용       도 : 메뉴 파라미터 삭제
         * Input    값 : DeleteMenuParam(메뉴순번)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// DeleteMenuParam : 메뉴 파라미터 삭제
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <returns></returns>
        public static object[] DeleteMenuParam(int intMenuSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = intMenuSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_MENUPARAM_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteMenuInfo : 메뉴 목록 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMenuInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-11
         * 용       도 : 메뉴 목록 삭제
         * Input    값 : DeleteMenuInfo(메뉴순번)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// DeleteMenuInfo : 메뉴 목록 삭제
        /// </summary>
        /// <returns></returns>
        public static object[] DeleteMenuInfo(int intMenuSeq)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[1];

            objParams[0] = intMenuSeq;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_MENUINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteMenuParam : 메뉴 파라미터 일부 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteMenuParam
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 메뉴 파라미터 일부 삭제
         * Input    값 : DeleteMenuParam(메뉴순번, 파라미터코드)
         * Ouput    값 : object[] 
         **********************************************************************************************/
        /// <summary>
        /// DeleteMenuParam : 메뉴 파라미터 일부 삭제
        /// </summary>
        /// <param name="intMenuSeq">메뉴순번</param>
        /// <param name="strParamCd">파라미터코드</param>
        /// <returns></returns>
        public static object[] DeleteMenuParam(int intMenuSeq, string strParamCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = intMenuSeq;
            objParams[1] = strParamCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_MNG_DELETE_MENUPARAM_M01", objParams);

            return objReturn;
        }

        #endregion
    }
}
