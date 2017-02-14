using System.Data;

using KN.Resident.Dac;

namespace KN.Resident.Biz
{
    public class RoomMngBlo
    {
        #region WatchExigstRoomInfo : 호실존재정보조회

        /**********************************************************************************************
         * Mehtod   명 : WatchExigstRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-17
         * 용       도 : 호실존재정보조회
         * Input    값 : WatchExigstRoomInfo(strRentCd, strRoomNo, strSearchDt)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchExigstRoomInfo : 호실존재정보조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataTable WatchExigstRoomInfo(string strRentCd, string strRoomNo, string strSearchDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[3];

            objParams[0] = strRentCd;
            objParams[1] = strRoomNo;
            objParams[2] = strSearchDt;

            dtReturn = RoomMngDao.SelectExigstRoomInfo(strRentCd, strRoomNo, strSearchDt);

            return dtReturn;
        }

        #endregion

        #region WatchRoomExistList : 중복정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchRoomExistList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-04
         * 용       도 : 중복정보 조회
         * Input    값 : WatchRoomExistList(임대구분코드, 층번호, 방번호, 시작일)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchRoomExistList : 중복정보 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strStartDt">시작일</param>
        /// <returns></returns>
        public static DataTable WatchRoomExistList(string strRentCd, int intFloorNo, string strRoomNo, string strStartDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RoomMngDao.SelectRoomExistList(strRentCd, intFloorNo, strRoomNo, strStartDt);

            return dtReturn;
        }

        #endregion

        #region SpreadRoomInfo : 호실 정보조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-27
         * 용       도 : 호실 정보조회
         * Input    값 : SpreadRoomInfo(strRoomNo, strSearchDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadRoomInfo : 호실 정보조회
        /// </summary>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataSet SpreadRoomInfo(string strRoomNo, string strSearchDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RoomMngDao.SelectRoomInfo(strRoomNo, strSearchDt);

            return dsReturn;
        }

        #endregion

        #region SelectParkingCarNoForSpecial 

        /**********************************************************************************************
         * Mehtod   명 : SelectParkingCarNoPrintOutSpcInv
         * 개   발  자 : 양영석
         * 생   성  일 : 2013-06-20
         * 용       도 : 호실 정보조회
         * Input    값 : SelectParkingCarNoPrintOutSpcInv(strRoomNo, strSearchDt)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadRoomInfo : 호실 정보조회
        /// </summary>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataSet SelectParkingCarNoPrintOutSpcInv(string strRoomNo, string strSearchDt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = RoomMngDao.SelectParkingCarNoPrintOutSpcInv(strRoomNo, strSearchDt);

            return dsReturn;
        }

        #endregion

        #region Get Apt Vesting Date

        /**********************************************************************************************
         * Mehtod   명 : SelectAptVestingDate
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-21
         * 용       도 : Room 거주자 정보 조회
         * Input    값 : SelectAptVestingDate(strRoomNo)
         * Ouput    값 : DataTable
         **********************************************************************************************/

        public static DataTable SelectAptVestingDate(string strRoomNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RoomMngDao.SelectAptVestingDate(strRoomNo);

            return dtReturn;
        }

        #endregion


        #region WatchRoomUserInfo : Room 거주자 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : WatchRoomUserInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-09-21
         * 용       도 : Room 거주자 정보 조회
         * Input    값 : WatchRoomUserInfo(strRentCd, strRoomNo, strSearchDt)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// WatchRoomUserInfo : Room 거주자 정보 조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="strRoomNo">호실</param>
        /// <param name="strSearchDt">조회일</param>
        /// <returns></returns>
        public static DataTable WatchRoomUserInfo(string strRentCd, string strRoomNo, string strSearchDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RoomMngDao.SelectRoomUserInfo(strRentCd, strRoomNo, strSearchDt);

            return dtReturn;
        }

        public static DataTable WatchUserListInfo(string strRentCd, string strRoomNo, string strCompNm)
        {
            var dtReturn = RoomMngDao.WatchUserListInfo(strRentCd, strRoomNo, strCompNm);

            return dtReturn;
        }

        #endregion

        #region Select Room Info for Air-con overtime

        public static DataTable SelectRoomInfoOverTime(string strRentCd, string strRoomNo, string strCompNm)
        {
            var dtReturn = RoomMngDao.SelectRoomInfoOverTime(strRentCd, strRoomNo, strCompNm);

            return dtReturn;
        }

        #endregion

        #region SpreadRoomList : 호실 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRoomList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-02
         * 용       도 : 호실 목록조회
         * Input    값 : SpreadRoomList(strRentCd, intFloorNo)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadRoomList : 호실 목록조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intFloorNo">조회층</param>
        /// <returns></returns>
        public static DataTable SpreadRoomList(string strRentCd, int intFloorNo)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RoomMngDao.SelectRoomList(strRentCd, intFloorNo);

            return dtReturn;
        }

        #endregion

        #region SpreadFloorList : Floor 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadFloorList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-31
         * 용       도 : Floor 조회
         * Input    값 : SpreadFloorList(임대구분코드, 조회년도, 조회월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadFloorList : Floor 조회
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMonth">조회월</param>
        /// <returns></returns>
        public static DataTable SpreadFloorList(string strRentCd, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RoomMngDao.SelectFloorList(strRentCd, strYear, strMonth);

            return dtReturn;
        }

        #endregion

        #region SpreadFloorList : Floor 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadFloorList
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-31
         * 용       도 : Floor 조회
         * Input    값 : SpreadFloorList(시작층, 조회년도, 조회월)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadFloorList : Floor 조회
        /// </summary>
        /// <param name="intStartFloor">시작층</param>
        /// <param name="strYear">조회년</param>
        /// <param name="strMonth">조회월</param>
        /// <returns></returns>
        public static DataTable SpreadFloorList(int intStartFloor, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RoomMngDao.SelectFloorList(intStartFloor, strYear, strMonth);

            return dtReturn;
        }

        #endregion

        #region SpreadRoomList : 호실 및 층 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRoomList
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-10-02
         * 용       도 : 호실 및 층 목록조회
         * Input    값 : SpreadRoomList(strRentCd)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadRoomList : 호실 및 층 목록조회
        /// </summary>
        /// <param name="strRentCd">섹션코드</param>
        /// <param name="intFloorNo">조회층</param>
        /// <returns></returns>
        public DataTable SpreadRoomList(string strRentCd)
        {
            DataTable dtReturn = new DataTable();

            int intFloorNo = 0;

            dtReturn = RoomMngDao.SelectRoomList(strRentCd, intFloorNo);

            return dtReturn;
        }

        #endregion

        #region SpreadRoomInfo : 방정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-01
         * 용       도 : 방 정보 조회
         * Input    값 : SpreadRoomInfo(임대코드, 층번호, 방번호, 조회일자)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadRoomInfo : 방 정보 조회
        /// </summary>
        /// <param name="strRentCd">임대코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">방번호</param>
        /// <param name="strNowDt">조회일자</param>
        /// <returns></returns>
        public static DataTable SpreadRoomInfo(string strRentCd, int intFloorNo, string strRoomNo, string strNowDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RoomMngDao.SelectRoomInfo(strRentCd, intFloorNo, strRoomNo, strNowDt);

            return dtReturn;
        }

        #endregion

        #region SpreadRoomlistInfo : 방목록 조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadRoomlistInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-31
         * 용       도 : 방목록 조회
         * Input    값 : SpreadRoomlistInfo(임대구분코드, 층번호, 조회년도, 조회월)
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
        public static DataTable SpreadRoomlistInfo(string strRentCd, int intFloorNo, string strYear, string strMonth)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = RoomMngDao.SelectRoomlistInfo(strRentCd, intFloorNo, strYear, strMonth);

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

            dtReturn = RoomMngDao.SelectRoomInfo(strRentCd, strRoomNo, strNowDt);

            return dtReturn;
        }

        #endregion

        #region RegistryRoomInfo : 해당 룸정보 등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-01
         * 용       도 : 해당 룸정보 등록
         * Input    값 : RegistryRoomInfo(임대구분코드, 층번호, 룸번호, 적용시작일자, 실면적, 임대인순번, 
         *                                임대업체명, 등록회사코드, 등록사번, 등록사번IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryRoomInfo : 해당 룸정보 등록
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strStartDt">적용시작일자</param>
        /// <param name="fltLeasingArea">실면적</param>
        /// <param name="intTenantSeq">임대인순번</param>
        /// <param name="strTenantCompNm">임대기업명</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">접근IP</param>
        /// <returns></returns>
        public static object[] RegistryRoomInfo(string strRentCd, int intFloorNo, string strRoomNo, string strStartDt, double fltLeasingArea, 
                                                int intTenantSeq, string strTenantCompNm, string strInsCompCd, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = RoomMngDao.InsertRoomInfo(strRentCd, intFloorNo, strRoomNo, strStartDt, fltLeasingArea, intTenantSeq, strTenantCompNm, 
                                                  strInsCompCd, strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion

        #region RemoveRoomBuilding : 해당 동 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveRoomBuilding
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-01
         * 용       도 : 해당 동 정보 삭제
         * Input    값 : RemoveRoomBuilding(임대구분코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveRoomBuilding : 해당 동 정보 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">접근IP</param>
        /// <returns></returns>
        public static object[] RemoveRoomBuilding(string strRentCd, string strInsCompCd, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = RoomMngDao.DeleteRoomBuilding(strRentCd, strInsCompCd, strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion

        #region RemoveRoomFloor : 특정 Floor 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveRoomFloor
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-01
         * 용       도 : 특정 Floor 정보 삭제
         * Input    값 : InsertFloorList(임대구분코드, 층번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveRoomFloor : 특정 Floor 정보 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">접근IP</param>
        /// <returns></returns>
        public static object[] RemoveRoomFloor(string strRentCd, int intFloorNo, string strInsCompCd, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = RoomMngDao.DeleteRoomFloor(strRentCd, intFloorNo, strInsCompCd, strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion

        #region RemoveRoomInfo : 특정 ROOM 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveRoomInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-09-01
         * 용       도 : 특정 ROOM 정보 삭제
         * Input    값 : InsertFloorList(임대구분코드, 층번호, 룸번호)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveRoomInfo :특정 ROOM 정보 삭제
        /// </summary>
        /// <param name="strRentCd">임대구분코드</param>
        /// <param name="intFloorNo">층번호</param>
        /// <param name="strRoomNo">룸번호</param>
        /// <param name="strInsCompCd">등록회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">접근IP</param>
        /// <returns></returns>
        public static object[] RemoveRoomInfo(string strRentCd, int intFloorNo, string strRoomNo, string strInsCompCd, string strInsMemNo, string strInsMemIp)
        {
            object[] objReturn = new object[2];

            objReturn = RoomMngDao.DeleteRoomInfo(strRentCd, intFloorNo, strRoomNo, strInsCompCd, strInsMemNo, strInsMemIp);

            return objReturn;
        }

        #endregion
    }
}