using System.Data;

using KN.Common.Base;
using KN.Common.Method.Lib;

namespace KN.Stock.Dac
{
    public class WarehouseMngDao
    {
        #region SelectWarehouseInfo : 창고관리 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SelectWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 창고관리 목록조회
         * Input    값 : SelectWarehouseInfo(페이지 목록수, 현재 페이지, 섹션코드, 서비스존코드, 검색어)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SelectWarehouseInfo : 창고관리 목록조회
        /// </summary>
        /// <param name="intPageSize">페이지 목록수</param>
        /// <param name="intNowPage">현재 페이지</param>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strSearchTxt">검색어</param>
        /// <returns></returns>
        public static DataSet SelectWarehouseInfo(int intPageSize, int intNowPage, string strSectionCd, string strSvcZoneCd, string strSearchTxt)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[5];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSectionCd));
            objParams[3] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSvcZoneCd));
            objParams[4] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSearchTxt));

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_WAREHOUSEINFO_S00", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectWarehouseInfo : 창고관리 목록조회 ( DropDownList용 )

        /**********************************************************************************************
         * Mehtod   명 : SelectWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-07
         * 용       도 : 창고관리 목록조회 ( DropDownList용 )
         * Input    값 : SelectWarehouseInfo(섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SelectWarehouseInfo : 창고관리 목록조회 ( DropDownList용 )
        /// </summary>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <returns></returns>
        public static DataTable SelectWarehouseInfo(string strSectionCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strSectionCd));

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_WAREHOUSEINFO_S01", objParams);

            return dtReturn;
        }

        #endregion

        #region InsertWarehouseInfo : 창고관리 창고등록

        /**********************************************************************************************
         * Mehtod   명 : InsertWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 창고관리 창고등록
         * Input    값 : InsertWarehouseInfo(섹션코드, 서비스존코드, 비고, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// InsertWarehouseInfo : 창고관리 창고등록
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] InsertWarehouseInfo(string strSectionCd, string strSvcZoneCd, string strRemark, string strCompCd, string strMemNo, string strMemIP)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strSectionCd;
            objParams[1] = strSvcZoneCd;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));
            objParams[3] = strCompCd;
            objParams[4] = strMemNo;
            objParams[5] = strMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_INSERT_WAREHOUSEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region UpdateWarehouseInfo : 창고관리 창고수정

        /**********************************************************************************************
         * Mehtod   명 : UpdateWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 창고관리 창고수정
         * Input    값 : UpdateWarehouseInfo(섹션코드, 서비스존코드, 비고, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// UpdateWarehouseInfo : 창고관리 창고수정
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] UpdateWarehouseInfo(string strSectionCd, string strSvcZoneCd, string strRemark, string strCompCd, string strMemNo, string strMemIP)
        {
            object[] objParams = new object[6];
            object[] objReturn = new object[2];

            objParams[0] = strSectionCd;
            objParams[1] = strSvcZoneCd;
            objParams[2] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));
            objParams[3] = strCompCd;
            objParams[4] = strMemNo;
            objParams[5] = strMemIP;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_UPDATE_WAREHOUSEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region DeleteWarehouseInfo : 창고관리 창고삭제

        /**********************************************************************************************
         * Mehtod   명 : DeleteWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 창고관리 창고삭제
         * Input    값 : DeleteWarehouseInfo(섹션코드, 서비스존코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// DeleteWarehouseInfo : 창고관리 창고삭제
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <returns></returns>
        public static object[] DeleteWarehouseInfo(string strSectionCd, string strSvcZoneCd)
        {
            object[] objReturn = new object[2];
            object[] objParams = new object[2];

            objParams[0] = strSectionCd;
            objParams[1] = strSvcZoneCd;

            objReturn = SPExecute.ExecReturnNo("KN_USP_STK_DELETE_WAREHOUSEINFO_M00", objParams);

            return objReturn;
        }

        #endregion

        #region SelectStoredInfo : 입고관리 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SelectStoredInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록조회
         * Input    값 : SelectStoredInfo(페이지 목록수, 현재 페이지)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록조회
        /// </summary>
        /// <param name="intPageSize">페이지 목록수</param>
        /// <param name="intNowPage">현재 페이지</param>
        /// <returns></returns>
        public static DataSet SelectStoredInfo(int intPageSize, int intNowPage)
        {
            DataSet dsReturn = new DataSet();

            object[] objParams = new object[2];

            objParams[0] = intPageSize;
            objParams[1] = intNowPage;

            dsReturn = SPExecute.ExecReturnMulti("KN_USP_STK_SELECT_WAREHOUSEINFO_S02", objParams);

            return dsReturn;
        }

        #endregion

        #region SelectStoredDetailInfo : 입고관리 목록상세보기

        /**********************************************************************************************
         * Mehtod   명 : SelectStoredInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록상세보기
         * Input    값 : SelectStoredInfo(주문번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기
        /// </summary>
        /// <param name="strOrderSeq">주문번호</param>
        /// <returns></returns>
        public static DataTable SelectStoredDetailInfo(string strOrderSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[1];

            objParams[0] = strOrderSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_WAREHOUSEINFO_S03", objParams);

            return dtReturn;
        }

        #endregion        

        #region SelectStoredGoodInfo : 입고관리 목록 DropDownList

        /**********************************************************************************************
         * Mehtod   명 : SelectStoredGoodInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록 DropDownList
         * Input    값 : SelectStoredGoodInfo(주문번호, 품목코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기
        /// </summary>
        /// <param name="strOrderSeq">주문번호</param>
        /// <param name="strClassCd">품목코드</param>
        /// <returns></returns>
        public static DataTable SelectStoredGoodInfo(string strOrderSeq, int strOrderDetSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strOrderSeq;
            objParams[1] = strOrderDetSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_WAREHOUSEINFO_S04", objParams);

            return dtReturn;
        }

        #endregion  

        #region InsertWareHouseMngInfo : 입고관리 목록 추가

        /**********************************************************************************************
         * Mehtod   명 : InsertWareHouseMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-03
         * 용       도 : 입고관리 목록 추가
         * Input    값 : InsertWareHouseMngInfo(입고순번, 발주순번, 발주신청상세순번, 기업코드, 작성자사번, 작성자IP)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWarehouseSeq">입고순번</param>
        /// <param name="strOrderSeq">발주순번</param>
        /// <param name="intOrderDetSeq">발주상세순번</param>
        /// <param name="strCompNo">기업코드</param>
        /// <param name="strInsMemNo">입력사번</param>
        /// <param name="strInsMemIp">입럭IP</param>
        /// <returns></returns>
        public static DataTable InsertWareHouseMngInfo(string strWarehouseSeq, string strOrderSeq, int intOrderDetSeq, string strCompNo, string strCompCd, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[7];

            objParams[0] = strWarehouseSeq;
            objParams[1] = strOrderSeq;
            objParams[2] = intOrderDetSeq;
            objParams[3] = strCompNo;
            objParams[4] = strCompCd;
            objParams[5] = strInsMemNo;
            objParams[6] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_WAREHOUSEMNGINFO_S00", objParams);

            return dtReturn;
        }

        #endregion 

        #region UpdateStoredGoodInfo : 입고관리 목록 수정 WarehousingMngInfo

        /**********************************************************************************************
         * Mehtod   명 : UpdateStoredGoodInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록 수정 WarehousingMngInfo
         * Input    값 : UpdateStoredGoodInfo(입고순번,발주신청상세순번, 입고수량, 입고일)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        ///  입고관리 목록 수정 WarehousingMngInfo
        /// </summary>
        /// <param name="strOrderSeq">입고순번</param>
        /// <param name="intOrderDetSeq">발주신청상세순번</param>
        /// <param name="intWareQty">입고수량</param>
        /// <param name="strWareDt">입고일</param>
        /// <returns></returns>
        public static DataTable UpdateStoredGoodInfo(string strOrderSeq, int intOrderDetSeq, int intWareQty, string strWareDt)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[4];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = intWareQty;
            objParams[3] = strWareDt;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_UPDATE_WAREHOUSEINFO_M01", objParams);

            return dtReturn;
        }

        #endregion  

        #region InsertStoredGoodInfo : 입고관리 목록 추가 WarehousingReceiptInfo

        /**********************************************************************************************
         * Mehtod   명 : InsertStoredGoodInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록 수정 WarehousingMngInfo
         * Input    값 : UpdateStoredGoodInfo(입고순번,발주신청상세순번, 입고수량, 입고일, 신청자사번, 비고, 작성자사번, 작성자IP)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록 추가 WarehousingReceiptInfo
        /// </summary>
        /// <param name="strOrderSeq">입고순번</param>
        /// <param name="intOrderDetSeq">발주신청상세순번</param>
        /// <param name="intWareQty">입고수량</param>
        /// <param name="strWareDt">입고일</param>
        /// <param name="strReceitMemNo">신청자사번</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strInsMemNo">작성자사번</param>
        /// <param name="strInsMemIp">작성자IP</param>
        /// <returns></returns>
        public static DataTable InsertStoredGoodInfo(string strOrderSeq, int intOrderDetSeq, int intWareQty, string strWareDt, string strReceitMemNo, string strRemark, string strCompCd, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[9];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = intWareQty;
            objParams[3] = strWareDt;
            objParams[4] = strReceitMemNo;
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));
            objParams[6] = strCompCd;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_WAREHOUSEINFO_M01", objParams);

            return dtReturn;
        }

        #endregion 

        #region SelectStoredGoodDetailInfo : 입고관리 목록상세보기

        /**********************************************************************************************
         * Mehtod   명 : SelectStoredGoodDetailInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록상세보기
         * Input    값 : SelectStoredGoodDetailInfo(입고물품번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기
        /// </summary>
        /// <param name="strOrderSeq">입고물품번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectStoredGoodDetailInfo(string strOrderSeq, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strOrderSeq;
            objParams[1] = strLangCd;


            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_WAREHOUSEINFO_S05", objParams);

            return dtReturn;
        }

        #endregion  

        #region InsertStoredPayInfo : 입고관리 목록 추가 WarehousingPayInfo

        /**********************************************************************************************
         * Mehtod   명 : InsertStoredPayInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-01
         * 용       도 : 입고관리 목록 추가 WarehousingPayInfo
         * Input    값 : InsertStoredPayInfo(입고순번,발주신청상세순번, 지급액, 지급날짜, 지급수단, 비고, 작성자, 작성자IP)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록 추가 WarehousingPayInfo
        /// </summary>
        /// <param name="strOrderSeq">입고순번</param>
        /// <param name="intOrderDetSeq">발주신청상세순번</param>
        /// <param name="dbPayAmt">지급액</param>
        /// <param name="strPayedDt">지급날짜</param>
        /// <param name="strPaymentCd">지급수단</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strInsMemNo">작성자</param>
        /// <param name="strInsMemIp">작성자IP</param>
        /// <returns></returns>
        public static DataTable InsertStoredPayInfo(string strOrderSeq, int intOrderDetSeq, double dbPayAmt, string strPayedDt, string strPaymentCd, string strRemark, string strCompCd, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[9];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;
            objParams[2] = dbPayAmt;
            objParams[3] = strPayedDt;
            objParams[4] = strPaymentCd;
            objParams[5] = TextLib.StringEncoder(TextLib.MakeNullToEmpty(strRemark));
            objParams[6] = strCompCd;
            objParams[7] = strInsMemNo;
            objParams[8] = strInsMemIp;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_INSERT_WAREHOUSEINFO_M02", objParams);

            return dtReturn;
        }

        #endregion 

        #region SelectStoredPayDetailInfo : 입고관리 목록상세보기(금액)

        /**********************************************************************************************
         * Mehtod   명 : SelectStoredPayDetailInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-01
         * 용       도 : 입고관리 목록상세보기(금액)
         * Input    값 : SelectStoredPayDetailInfo(입고물품번호,언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기(금액)
        /// </summary>
        /// <param name="strOrderSeq">입고물품번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable SelectStoredPayDetailInfo(string strOrderSeq, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strOrderSeq;
            objParams[1] = strLangCd;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_WAREHOUSEINFO_S06", objParams);

            return dtReturn;
        }

        #endregion 

        #region SelectStoredPayInfo : 입고관리 DropDownList(금액)

        /**********************************************************************************************
         * Mehtod   명 : SelectStoredPayInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-01
         * 용       도 : 입고관리 DropDownList(금액)
         * Input    값 : SelectStoredPayInfo(주문번호, 품목코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기
        /// </summary>
        /// <param name="strOrderSeq">주문번호</param>
        /// <param name="strClassCd">품목코드</param>
        /// <returns></returns>
        public static DataTable SelectStoredPayInfo(string strOrderSeq, int intOrderDetSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_WAREHOUSEINFO_S07", objParams);

            return dtReturn;
        }

        #endregion  

        #region SelectReleaseRequestInfo : 출고요청 여부 확인

        /**********************************************************************************************
         * Mehtod   명 : SelectReleaseRequestInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-03
         * 용       도 : 출고요청 여부 확인
         * Input    값 : SelectReleaseRequestInfo(주문번호, 품목코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 출고요청 여부 확인
        /// </summary>
        /// <param name="strOrderSeq">출고요청번호</param>
        /// <param name="strOrderDetSeq">출고요청상세번호</param>
        /// <returns></returns>
        public static DataTable SelectReleaseRequestInfo(string strOrderSeq, int intOrderDetSeq)
        {
            DataTable dtReturn = new DataTable();

            object[] objParams = new object[2];

            objParams[0] = strOrderSeq;
            objParams[1] = intOrderDetSeq;

            dtReturn = SPExecute.ExecReturnSingle("KN_USP_STK_SELECT_GOODSORDERINFO_S02", objParams);

            return dtReturn;
        }

        #endregion 
    }
}
