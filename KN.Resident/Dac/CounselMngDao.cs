using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

using KN.Resident.Ent;

namespace KN.Resident.Dac
{
    public class CounselMngDao
    {
        #region SelectCounselInfo : 상담 정보 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectCounselInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 조회
         * Input    값 : SelectCounselInfo(페이지 사이즈, 현재 페이지번호, 상담코드, 검색어 코드, 검색어, 접근권한, 언어코드, 접근회사코드, 사원번호, 주문코드
         *                                 등록시작일, 등록종료일, 지역코드, 산업코드, 임대시작일, 임대종료일)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectCounselInfo : 상담 정보 조회
        /// </summary>
        /// <param name="intPageSize">페이지 사이즈</param>
        /// <param name="intNowPage">현재 페이지번호</param>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="strKeyCd">검색어 코드</param>
        /// <param name="strKeyWord">검색어</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strLangCd">언어코드</param>
        /// <param name="strCompNo">접근회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strOrderCd">주문코드</param>
        /// <param name="strStartInsDt">등록시작일</param>
        /// <param name="strEndInsDt">등록종료일</param>
        /// <param name="strAreaCd">지역코드</param>
        /// <param name="strIndustryCd">산업코드</param>
        /// <param name="strStartLeaseDt">임대시작일</param>
        /// <param name="strEndLeaseDt">임대종료일</param>
        /// <returns></returns>
        public static DataSet SelectCounselInfo(int intPageSize, int intNowPage, string strCounselCd, string strKeyCd, string strKeyWord, string strAccessAuth, 
                                                string strLangCd, string strCompNo, string strMemNo, string strOrderCd, string strStartInsDt, string strEndInsDt, 
                                                string strAreaCd, string strIndustryCd, string strStartLeaseDt, string strEndLeaseDt)
        {
            object[] objParams = new object[16];
            DataSet dsReturn = new DataSet();

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = strCounselCd;
            objParams[3] = strKeyCd;
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strKeyWord));
            objParams[5] = strAccessAuth;
            objParams[6] = strLangCd;
            objParams[7] = strCompNo;
            objParams[8] = strMemNo;
            objParams[9] = strOrderCd;
            objParams[10] = (TextLib.MakeNullToEmpty(strStartInsDt));
            objParams[11] = (TextLib.MakeNullToEmpty(strEndInsDt));
            objParams[12] = strAreaCd;
            objParams[13] = strIndustryCd;
            objParams[14] = (TextLib.MakeNullToEmpty(strStartLeaseDt));
            objParams[15] = (TextLib.MakeNullToEmpty(strEndLeaseDt));

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_COUNSELINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectCounselDetInfo : 상담 정보 상세 조회

        /**********************************************************************************************
         * Mehtod   명 : SelectCounselDetInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 상세 조회
         * Input    값 : SelectCounselDetInfo(상담코드, 상담순번, 접근권한, 회사코드, 사원번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectCounselDetInfo : 상담 정보 상세 조회
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataSet SelectCounselDetInfo(string strCounselCd, int intCounselSeq, string strAccessAuth, string strCompNo, string strMemNo, string strLangCd)
        {
            object[] objParams = new object[6];
            DataSet dsReturn = new DataSet();

            objParams[0] = strCounselCd;
            objParams[1] = intCounselSeq;
            objParams[2] = strAccessAuth;
            objParams[3] = strCompNo;
            objParams[4] = strMemNo;
            objParams[5] = strLangCd;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_RES_SELECT_COUNSELINFO_S01", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectCounselDetailView : 상담 정보 상세 조회 (수정용)

        /**********************************************************************************************
         * Mehtod   명 : SelectCounselDetailView
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-24
         * 용       도 : 상담 정보 상세 조회
         * Input    값 : SelectCounselDetailView(상담코드, 상담순번, 접근권한, 회사코드, 사원번호, 언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectCounselDetailView : 상담 정보 상세 조회
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strCompNo">회사코드</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectCounselDetailView(string strCounselCd, int intCounselSeq, string strAccessAuth, string strCompNo, string strMemNo, string strLangCd)
        {
            object[] objParams = new object[6];

            DataTable dtReturn = new DataTable();

            objParams[0] = strCounselCd;
            objParams[1] = intCounselSeq;
            objParams[2] = strAccessAuth;
            objParams[3] = strCompNo;
            objParams[4] = strMemNo;
            objParams[5] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_SELECT_COUNSELINFO_S02", objParams);

            return dtReturn;
        }

        #endregion

        #region UpdateCounselInfo : 상담 정보 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateCounselInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 수정
         * Input    값 : UpdateCounselInfo(CounselMngDs.CounselInfo 객체)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateCounselInfo : 상담 정보 수정
        /// </summary>
        /// <param name="csinfo">CounselMngDs.CounselInfo 객체</param>
        /// <returns></returns>
        public static object[] UpdateCounselInfo(CounselMngDs.CounselInfo csinfo)
        {
            object[] objParams = new object[65];
            object[] objReturn = new object[2];

            objParams[0] = csinfo.CounselCd;
            objParams[1] = csinfo.CounselSeq;
            objParams[2] = csinfo.IndustryCd;
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.IndustryEtcNm));
            objParams[4] = csinfo.CountryCd;
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CountryEtcNm));
            objParams[6] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompNm));
            objParams[7] = csinfo.CompTyCd;
            objParams[8] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompEtcTyNm));
            objParams[9] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompGrade));
            objParams[10] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompAddr));
            objParams[11] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompDetAddr));
            objParams[12] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.ChargerNm));
            objParams[13] = csinfo.CompTelNo;
            objParams[14] = csinfo.CompTelFrontNo;
            objParams[15] = csinfo.CompTelMidNo;
            objParams[16] = csinfo.CompTelRearNo;
            objParams[17] = csinfo.CompFaxNo;
            objParams[18] = csinfo.CompFaxFrontNo;
            objParams[19] = csinfo.CompFaxMidNo;
            objParams[20] = csinfo.CompFaxRearNo;
            objParams[21] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.EmailID));
            objParams[22] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.EmailServer));
            objParams[23] = csinfo.UsingAreaCd;
            objParams[24] = csinfo.UsingAreaEtcNm;
            objParams[25] = csinfo.CurrRentalCd;
            objParams[26] = csinfo.CurrRentalEtcNm;
            objParams[27] = csinfo.CurrMngFee;
            objParams[28] = csinfo.CurrServiceFareCd;
            objParams[29] = csinfo.CurrServiceFareEtcNm;
            objParams[30] = csinfo.StaffNo;
            objParams[31] = csinfo.StaffNoEtc;
            objParams[32] = csinfo.ContCommeceYear;
            objParams[33] = csinfo.ContPeriodCd;
            objParams[34] = csinfo.ContPeriodEtcNm;
            objParams[35] = csinfo.CarNoCd;
            objParams[36] = csinfo.CarNoEtcNm;
            objParams[37] = csinfo.MotoBikeNoCd;
            objParams[38] = csinfo.MotoBikeNoEtcNm;
            objParams[39] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.TransferPlanReason));
            objParams[40] = csinfo.TransferCostBuget;
            objParams[41] = csinfo.LeaseAreaCd;
            objParams[42] = csinfo.LeaseAreaEtcNm;
            objParams[43] = csinfo.ExpectedRentalCd;
            objParams[44] = csinfo.ExpectedRentalEtcNm;
            objParams[45] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.FavorateDirection));
            objParams[46] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.FavorateFloor));
            objParams[47] = csinfo.CompBudget;
            objParams[48] = csinfo.ExpectedlPeriodCd;
            objParams[49] = csinfo.ExpectedlPeriodEtcNm;
            objParams[50] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.LeaseYear));
            objParams[51] = csinfo.LeaseMonth;
            objParams[52] = csinfo.DecisionCd;
            objParams[53] = csinfo.DecisionEtcNm;
            objParams[54] = csinfo.NeedParkNo;
            objParams[55] = csinfo.InternalConstCd;
            objParams[56] = csinfo.InternalConstEtcNm;
            objParams[57] = csinfo.MovingLocationCd;
            objParams[58] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.MovingLocationEtcNm));
            objParams[59] = csinfo.ReadAuth;
            objParams[60] = csinfo.WriteAuth;
            objParams[61] = csinfo.ModDelAuth;
            objParams[62] = csinfo.ModCompNo;
            objParams[63] = csinfo.ModMemNo;
            objParams[64] = csinfo.ModMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_COUNSELINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateCounselAddon : 상담 정보 중 비고 수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateCounselAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 중 비고 수정
         * Input    값 : UpdateCounselAddon(상담코드, 상담순번, 등록사번, 등록사원IP, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateCounselAddon : 상담 정보 중 비고 등록
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">등록사원IP</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] UpdateCounselAddon(string strCounselCd, int intCounselSeq, string strInsCompNo, string strInsMemNo, string strInsMemIp, string strRemark)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strCounselCd;
            objParams[1] = intCounselSeq;
            objParams[2] = strInsCompNo;
            objParams[3] = strInsMemNo;
            objParams[4] = strInsMemIp;
            objParams[5] = strRemark;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_UPDATE_COUNSELADDON_M00", objParams);

            return objReturn;
        }

        #endregion

        #region InsertCounselInfo : 상담 정보 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertCounselInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-22
         * 용       도 : 상담 정보 등록
         * Input    값 : InsertCounselInfo(CounselMngDs.CounselInfo 객체)
         * Ouput    값 : CounselSeq값 리턴
         **********************************************************************************************/
        /// <summary>
        /// InsertCounselInfo : 상담 정보 등록
        /// </summary>
        /// <param name="csinfo">CounselMngDs.CounselInfo 객체</param>
        /// <returns>CounselSeq값 리턴</returns>
        public static DataTable InsertCounselInfo(CounselMngDs.CounselInfo csinfo)
        {
            object[] objParams = new object[64];
            DataTable dtReturn = new DataTable();

            objParams[0] = csinfo.CounselCd;
            objParams[1] = csinfo.IndustryCd;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.IndustryEtcNm));
            objParams[3] = csinfo.CountryCd;
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CountryEtcNm));
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompNm));
            objParams[6] = csinfo.CompTyCd;
            objParams[7] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompEtcTyNm));
            objParams[8] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompGrade));
            objParams[9] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompAddr));
            objParams[10] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.CompDetAddr));
            objParams[11] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.ChargerNm));
            objParams[12] = TextLib.MakeNullToEmpty(csinfo.CompTelNo);
            objParams[13] = TextLib.MakeNullToEmpty(csinfo.CompTelFrontNo);
            objParams[14] = TextLib.MakeNullToEmpty(csinfo.CompTelMidNo);
            objParams[15] = TextLib.MakeNullToEmpty(csinfo.CompTelRearNo);
            objParams[16] = TextLib.MakeNullToEmpty(csinfo.CompFaxNo);
            objParams[17] = TextLib.MakeNullToEmpty(csinfo.CompFaxFrontNo);
            objParams[18] = TextLib.MakeNullToEmpty(csinfo.CompFaxMidNo);
            objParams[19] = TextLib.MakeNullToEmpty(csinfo.CompFaxRearNo);
            objParams[20] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.EmailID));
            objParams[21] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.EmailServer));
            objParams[22] = csinfo.UsingAreaCd;
            objParams[23] = csinfo.UsingAreaEtcNm;
            objParams[24] = csinfo.CurrRentalCd;
            objParams[25] = csinfo.CurrRentalEtcNm;
            objParams[26] = csinfo.CurrMngFee;
            objParams[27] = csinfo.CurrServiceFareCd;
            objParams[28] = csinfo.CurrServiceFareEtcNm;
            objParams[29] = csinfo.StaffNo;
            objParams[30] = csinfo.StaffNoEtc;
            objParams[31] = csinfo.ContCommeceYear;
            objParams[32] = csinfo.ContPeriodCd;
            objParams[33] = csinfo.ContPeriodEtcNm;
            objParams[34] = csinfo.CarNoCd;
            objParams[35] = csinfo.CarNoEtcNm;
            objParams[36] = csinfo.MotoBikeNoCd;
            objParams[37] = csinfo.MotoBikeNoEtcNm;
            objParams[38] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.TransferPlanReason));
            objParams[39] = csinfo.TransferCostBuget;
            objParams[40] = csinfo.LeaseAreaCd;
            objParams[41] = csinfo.LeaseAreaEtcNm;
            objParams[42] = csinfo.ExpectedRentalCd;
            objParams[43] = csinfo.ExpectedRentalEtcNm;
            objParams[44] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.FavorateDirection));
            objParams[45] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.FavorateFloor));
            objParams[46] = csinfo.CompBudget;
            objParams[47] = csinfo.ExpectedlPeriodCd;
            objParams[48] = csinfo.ExpectedlPeriodEtcNm;
            objParams[49] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.LeaseYear));
            objParams[50] = csinfo.LeaseMonth;
            objParams[51] = csinfo.DecisionCd;
            objParams[52] = csinfo.DecisionEtcNm;
            objParams[53] = csinfo.NeedParkNo;
            objParams[54] = csinfo.InternalConstCd;
            objParams[55] = csinfo.InternalConstEtcNm;
            objParams[56] = csinfo.MovingLocationCd;
            objParams[57] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(csinfo.MovingLocationEtcNm));
            objParams[58] = csinfo.ReadAuth;
            objParams[59] = csinfo.WriteAuth;
            objParams[60] = csinfo.ModDelAuth;
            objParams[61] = csinfo.InsCompNo;
            objParams[62] = csinfo.InsMemNo;
            objParams[63] = csinfo.InsMemIP;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_RES_INSERT_COUNSELINFO_S00", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertCounselAddon : 상담 정보 중 비고 등록

        /**********************************************************************************************
         * Mehtod   명 : InsertCounselAddon
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-23
         * 용       도 : 상담 정보 중 비고 등록
         * Input    값 : InsertCounselAddon(상담코드, 상담순번, 회사코드, 등록사번, 등록사원IP, 비고)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertCounselAddon : 상담 정보 중 비고 등록
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">등록사원IP</param>
        /// <param name="strRemark">비고</param>
        /// <returns></returns>
        public static object[] InsertCounselAddon(string strCounselCd, int intCounselSeq, string strInsCompNo, string strInsMemNo, string strInsMemIp, string strRemark)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strCounselCd;
            objParams[1] = intCounselSeq;
            objParams[2] = strInsCompNo;
            objParams[3] = strInsMemNo;
            objParams[4] = strInsMemIp;
            objParams[5] = strRemark;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_INSERT_COUNSELADDON_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteCounselInfo : 상담 정보 삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteCounselInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-24
         * 용       도 : 상담 정보 삭제
         * Input    값 : DeleteCounselInfo(상담코드, 상담순번, 회사코드, 등록사번, 등록사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteCounselInfo : 상담 정보 삭제
        /// </summary>
        /// <param name="strCounselCd">상담코드</param>
        /// <param name="intCounselSeq">상담순번</param>
        /// <param name="strInsCompNo">회사코드</param>
        /// <param name="strInsMemNo">등록사번</param>
        /// <param name="strInsMemIp">등록사원IP</param>
        /// <returns></returns>
        public static object[] DeleteCounselInfo(string strCounselCd, int intCounselSeq, string strInsCompNo, string strInsMemNo, string strInsMemIp)
        {
            object[] objParams = new object[5];
            object[] objReturn = new object[2];

            objParams[0] = strCounselCd;
            objParams[1] = intCounselSeq;
            objParams[2] = strInsCompNo;
            objParams[3] = strInsMemNo;
            objParams[4] = strInsMemIp;

            objReturn = SPExecute.ExecReturnNo("KN_USP_RES_DELETE_COUNSELINFO_M00", objParams);

            return objReturn;
        }

        #endregion
    }
}