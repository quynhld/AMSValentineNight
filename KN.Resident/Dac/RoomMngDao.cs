using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Resident.Dac
{
    public class RoomMngDao
    {
        #region SelectExigstRoomInfo : 호실존재정보조회

        /**********************************************************************************************
         * Mehtod   명 : SelectExigstRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-17
         * 용       도 : 호실존재정보조회
         * Input    값 : SelectExigstRoomInfo(strRentCd, strRoomNo, strSearchDt)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectExigstRoomInfo : 호실존재정보조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataTable SelectExigstRoomInfo(string strRentCd, string strRoomNo, string strSearchDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = TextLib.MakeNullToEmpty(strSearchDt);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRoomExistList : 중복정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRoomExistList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-04
         * 용       도 : 중복정보 조회
         * Input    값 : SelectRoomExistList(임대구분코드, 층번호, 방번호, 시작일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRoomExistList : 중복정보 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strStartDt">시작일</param>
        /// <returns></returns>
        public static DataTable SelectRoomExistList(string strRentCd, int intFloorNo, string strRoomNo, string strStartDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = intFloorNo;
            objParams[2] = strRoomNo;
            objParams[3] = strStartDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S07", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectFloorList : Floor 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectFloorList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-31
         * 용       도 : Floor 조회
         * Input    값 : SelectFloorList(임대구분코드, 조회년도, 조회월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Floor 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMonth">조회월</param>
        /// <returns></returns>
        public static DataTable SelectFloorList(string strRentCd, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strYear;
            objParams[2] = strMonth;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_FLOOR_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectFloorList : Floor 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectFloorList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-31
         * 용       도 : Floor 조회
         * Input    값 : SelectFloorList(시작층, 조회년도, 조회월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// Floor 조회
        /// </summary>
        /// <param name="intStartFloor">시작층</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMonth">조회월</param>
        /// <returns></returns>
        public static DataTable SelectFloorList(int intStartFloor, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = intStartFloor;
            objParams[1] = strYear;
            objParams[2] = strMonth;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_FLOOR_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRoomList : 호실 및 층 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRoomList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-02
         * 용       도 : 호실 및 층 목록조회
         * Input    값 : SelectRoomList(strRentCd, intFloorNo)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRoomList : 호실 및 층 목록조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intFloorNo">조회층</param>
        /// <returns></returns>
        public static DataTable SelectRoomList(string strRentCd, int intFloorNo)
        {
            DataTable dtReturn = new DataTable();

            if (intFloorNo != 0)
            {
                // 호실 목록조회
                object[] objParams = new object[2];

                objParams[0] = strRentCd;
                objParams[1] = intFloorNo;

                dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S04", objParams);
            }
            else
            {
                // 층 정보조회
                object[] objParams = new object[1];

                objParams[0] = strRentCd;

                dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S03", objParams);
            }

            return dtReturn;
        }

        #endregion

        #region SelectRoomlistInfo : 방목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRoomlistInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-31
         * 용       도 : 방목록 조회
         * Input    값 : SelectRoomlistInfo(임대구분코드, 층번호, 조회년도, 조회월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 방목록 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMonth">조회월</param>
        /// <returns></returns>
        public static DataTable SelectRoomlistInfo(string strRentCd, int intFloorNo, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = intFloorNo;
            objParams[2] = strYear;
            objParams[3] = strMonth;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S06", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRoomInfo : 호실 정보조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 호실 정보조회
         * Input    값 : SelectRoomInfo(strRoomNo, strSearchDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectRoomInfo : 호실 정보조회
        /// </summary>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataSet SelectRoomInfo(string strRoomNo, string strSearchDt)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[2];

            objParams[0] = strRoomNo;
            objParams[1] = TextLib.MakeNullToEmpty(strSearchDt);

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_ROOMINFO_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectParkingCarNoForSpecial PrintOutInvoice 

        /**********************************************************************************************
         * Mehtod   명 : SelectParkingCarNoForSpecial
         * 개   발  자 : Phuongtv
         * 생   성  일 : 2013-06-20
         * 용       도 : 호실 정보조회
         * Input    값 : SelectParkingCarNoForSpecial(strRoomNo, strSearchDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectRoomInfo : 호실 정보조회
        /// </summary>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataSet SelectParkingCarNoPrintOutSpcInv(string strRoomNo, string strSearchDt)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[2];

            objParams[0] = strRoomNo;
            objParams[1] = TextLib.MakeNullToEmpty(strSearchDt);

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_ROOMINFO_S11", objParams);

            return dsReturn;
        }

        #endregion

        #region Get Apt Vesting Date
        /**********************************************************************************************
         * Mehtod   명 : SelectAptVestingDate
         * 개   발  자 : phuongtv
         * 생   성  일 : 2013-09-19
         * 용       도 :
         * Input    값 : SelectAptVestingDate(strRoomNo)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRoomUserInfo : Room 거주자 정보 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataTable SelectAptVestingDate(string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strRoomNo;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_VESTINGDATE_S00", objParams);

            return dtReturn;
        }

        #endregion


        #region SelectRoomUserInfo : Room 거주자 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRoomUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-21
         * 용       도 : Room 거주자 정보 조회
         * Input    값 : SelectRoomUserInfo(strRentCd, strRoomNo, strSearchDt)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRoomUserInfo : Room 거주자 정보 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataTable SelectRoomUserInfo(string strRentCd, string strRoomNo, string strSearchDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = TextLib.MakeNullToEmpty(strSearchDt);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S01", objParams);

            return dtReturn;
        }
       


        public static DataTable WatchUserListInfo(string strRentCd, string strRoomNo, string strCompNm)
        {
            var objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = TextLib.MakeNullToEmpty(strCompNm);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S09", objParams);

            return dtReturn;
        }

        #endregion

        #region Select Room Info for Air-con overtime

        public static DataTable SelectRoomInfoOverTime(string strRentCd, string strRoomNo, string strCompNm)
        {
            var objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = TextLib.MakeNullToEmpty(strCompNm);

            var dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S12", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRoomInfo : 방번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-01
         * 용       도 : 방번호 조회
         * Input    값 : SelectRoomInfo(임대구분코드, 층번호, 방번호, 조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRoomInfo : 방번호 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strNowDt">조회일자(YYYYMMDD)</param>
        /// <returns></returns>
        public static DataTable SelectRoomInfo(string strRentCd, int intFloorNo, string strRoomNo, string strNowDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = intFloorNo;
            objParams[2] = strRoomNo;
            objParams[3] = strNowDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S05", objParams);

            return dtReturn;
        }

        #endregion

        #region SelectRoomInfo : 방번호 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2012-07-03
         * 용       도 : 방번호 조회
         * Input    값 : SelectRoomInfo(임대구분코드, 방번호, 조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectRoomInfo : 방번호 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strNowDt">조회일자(YYYYMMDD)</param>
        /// <returns></returns>
        public static DataTable SelectRoomInfo(string strRentCd, string strRoomNo, string strNowDt)
        {
            DataTable dtReturn = new DataTable();
            object[] objParams = new object[3];

            objParams[0] = TextLib.MakeNullToEmpty(strRentCd);
            objParams[1] = TextLib.MakeNullToEmpty(strRoomNo);
            objParams[2] = TextLib.MakeNullToEmpty(strNowDt);

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_ROOMINFO_S08", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertRoomInfo : 해당 룸정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-01
         * 용       도 : 해당 룸정보 등록
         * Input    값 : InsertRoomInfo(임대구분코드, 층번호, 룸번호, 적용시작일자, 실면적, 임대인순번, 
         *                              임대업체명, 등록회사코드, 등록사번, 등록사번IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertRoomInfo : 해당 룸정보 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strStartDt">적용시작일자</param>
        /// <param name="fltLeasingArea">실면적</param>
        /// <param name="intTenantSeq">임대인순번</param>
        /// <param name="strTenantCompNm">임대업체명</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">접근IP</param>
        /// <returns></returns>
        public static object[] InsertRoomInfo(string strRentCd, int intFloorNo, string strRoomNo, string strStartDt, double fltLeasingArea,
                                              int intTenantSeq, string strTenantCompNm, string strInsCompCd, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[10];

            objParams[0] = strRentCd;
            objParams[1] = intFloorNo;
            objParams[2] = strRoomNo;
            objParams[3] = strStartDt;
            objParams[4] = fltLeasingArea;
            objParams[5] = intTenantSeq;
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strTenantCompNm));
            objParams[7] = strInsCompCd;
            objParams[8] = strInsMemNo;
            objParams[9] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_ROOMINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteRoomBuilding : 해당 동 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRoomBuilding
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-01
         * 용       도 : 해당 동 정보 삭제
         * Input    값 : DeleteRoomBuilding(임대구분코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteRoomBuilding : 해당 동 정보 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">접근IP</param>
        /// <returns></returns>
        public static object[] DeleteRoomBuilding(string strRentCd, string strInsCompCd, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[4];

            objParams[0] = strRentCd;
            objParams[1] = strInsCompCd;
            objParams[2] = strInsMemNo;
            objParams[3] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_ROOMINFO_M02", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteRoomFloor : 특정 Floor 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRoomFloor
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-01
         * 용       도 : 특정 Floor 정보 삭제
         * Input    값 : InsertFloorList(임대구분코드, 층번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteRoomFloor : 특정 Floor 정보 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">접근IP</param>
        /// <returns></returns>
        public static object[] DeleteRoomFloor(string strRentCd, int intFloorNo, string strInsCompCd, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[5];

            objParams[0] = strRentCd;
            objParams[1] = intFloorNo;
            objParams[2] = strInsCompCd;
            objParams[3] = strInsMemNo;
            objParams[4] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_ROOMINFO_M01", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteRoomInfo : 특정 ROOM 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-01
         * 용       도 : 특정 ROOM 정보 삭제
         * Input    값 : InsertFloorList(임대구분코드, 층번호, 룸번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteRoomInfo :특정 ROOM 정보 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">접근IP</param>
        /// <returns></returns>
        public static object[] DeleteRoomInfo(string strRentCd, int intFloorNo, string strRoomNo, string strInsCompCd, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[6];

            objParams[0] = strRentCd;
            objParams[1] = intFloorNo;
            objParams[2] = strRoomNo;
            objParams[3] = strInsCompCd;
            objParams[4] = strInsMemNo;
            objParams[5] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_ROOMINFO_M00", objParams);

            return objReturn;
        }

        #endregion
    }
}