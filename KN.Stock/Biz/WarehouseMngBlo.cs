using System.Data;

using KN.Stock.Dac;

namespace KN.Stock.Biz
{
    public class WarehouseMngBlo
    {
        #region SpreadWarehouseInfo : 창고관리 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 창고관리 목록조회
         * Input    값 : SpreadWarehouseInfo(페이지 목록수, 현재 페이지, 섹션코드, 서비스존코드, 검색어)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// SpreadWarehouseInfo : 창고관리 목록조회
        /// </summary>
        /// <param name="intPageSize">페이지 목록수</param>
        /// <param name="intNowPage">현재 페이지</param>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strSearchTxt">검색어</param>
        /// <returns></returns>
        public static DataSet SpreadWarehouseInfo(int intPageSize, int intNowPage, string strSectionCd, string strSvcZoneCd, string strSearchTxt)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = WarehouseMngDao.SelectWarehouseInfo(intPageSize, intNowPage, strSectionCd, strSvcZoneCd, strSearchTxt);

            return dsReturn;
        }

        #endregion

        #region SpreadWarehouseInfo : 창고관리 목록조회 ( DropDownList용 )

        /**********************************************************************************************
         * Mehtod   명 : SpreadWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-07
         * 용       도 : 창고관리 목록조회 ( DropDownList용 )
         * Input    값 : SpreadWarehouseInfo(섹션코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// SpreadWarehouseInfo : 창고관리 목록조회 ( DropDownList용 )
        /// </summary>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <returns></returns>
        public static DataTable SpreadWarehouseInfo(string strSectionCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.SelectWarehouseInfo(strSectionCd);

            return dtReturn;
        }

        #endregion

        #region RegistryWarehouseInfo : 창고관리 창고등록

        /**********************************************************************************************
         * Mehtod   명 : RegistryWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 창고관리 창고등록
         * Input    값 : RegistryWarehouseInfo(섹션코드, 서비스존코드, 비고, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RegistryWarehouseInfo : 창고관리 창고등록
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] RegistryWarehouseInfo(string strSectionCd, string strSvcZoneCd, string strRemark, string strCompCd, string strMemNo, string strMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = WarehouseMngDao.InsertWarehouseInfo(strSectionCd, strSvcZoneCd, strRemark, strCompCd, strMemNo, strMemIP);

            return objReturn;
        }

        #endregion

        #region ModifyWarehouseInfo : 창고관리 창고수정

        /**********************************************************************************************
         * Mehtod   명 : ModifyWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 창고관리 창고수정
         * Input    값 : ModifyWarehouseInfo(섹션코드, 서비스존코드, 비고, 사원번호, 사원IP)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// ModifyWarehouseInfo : 창고관리 창고수정
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <param name="strRemark">비고</param>
        /// <param name="strMemNo">사원번호</param>
        /// <param name="strMemIP">사원IP</param>
        /// <returns></returns>
        public static object[] ModifyWarehouseInfo(string strSectionCd, string strSvcZoneCd, string strRemark, string strCompCd, string strMemNo, string strMemIP)
        {
            object[] objReturn = new object[2];

            objReturn = WarehouseMngDao.UpdateWarehouseInfo(strSectionCd, strSvcZoneCd, strRemark, strCompCd, strMemNo, strMemIP);

            return objReturn;
        }

        #endregion

        #region RemoveWarehouseInfo : 창고관리 창고삭제

        /**********************************************************************************************
         * Mehtod   명 : RemoveWarehouseInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-05
         * 용       도 : 창고관리 창고삭제
         * Input    값 : RemoveWarehouseInfo(섹션코드, 서비스존코드)
         * Ouput    값 : object[]
         **********************************************************************************************/
        /// <summary>
        /// RemoveWarehouseInfo : 창고관리 창고삭제
        /// </summary>
        /// <param name="strSectionCd">섹션코드</param>
        /// <param name="strSvcZoneCd">서비스존코드</param>
        /// <returns></returns>
        public static object[] RemoveWarehouseInfo(string strSectionCd, string strSvcZoneCd)
        {
            object[] objReturn = new object[2];

            objReturn = WarehouseMngDao.DeleteWarehouseInfo(strSectionCd, strSvcZoneCd);

            return objReturn;
        }

        #endregion

        #region SpreadStoredInfo : 입고관리 목록조회

        /**********************************************************************************************
         * Mehtod   명 : SpreadStoredInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록조회
         * Input    값 : SpreadStoredInfo(페이지 목록수, 현재 페이지)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록조회
        /// </summary>
        /// <param name="intPageSize">페이지 목록수</param>
        /// <param name="intNowPage">현재 페이지</param>
        /// <returns></returns>
        public static DataSet SpreadStoredInfo(int intPageSize, int intNowPage)
        {
            DataSet dsReturn = new DataSet();

            dsReturn = WarehouseMngDao.SelectStoredInfo(intPageSize, intNowPage);

            return dsReturn;
        }

        #endregion

        #region WatchStoredDetailInfo : 입고관리 목록상세보기

        /**********************************************************************************************
         * Mehtod   명 : WatchStoredDetailInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록상세보기
         * Input    값 : WatchStoredDetailInfo(주문번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기
        /// </summary>
        /// <param name="strOrderSeq">주문번호</param>
        /// <returns></returns>
        public static DataTable WatchStoredDetailInfo(string strOrderSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.SelectStoredDetailInfo(strOrderSeq);

            return dtReturn;
        }

        #endregion        

        #region WatchStoredGoodInfo : 입고관리 목록 DropDownList

        /**********************************************************************************************
         * Mehtod   명 : WatchStoredGoodInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록상세보기
         * Input    값 : WatchStoredDetailInfo(주문번호, 품목코드, 주문순번)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기
        /// </summary>
        /// <param name="strOrderSeq">주문번호</param>
        /// <param name="strClassCd">품목코드</param>
        /// <returns></returns>
        public static DataTable WatchStoredGoodInfo(string strOrderSeq, int intOrderDetSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.SelectStoredGoodInfo(strOrderSeq, intOrderDetSeq);

            return dtReturn;
        }

        #endregion  

        #region RegistryWareHouseMngInfo : 입고관리 목록 추가

        /**********************************************************************************************
         * Mehtod   명 : RegistryWareHouseMngInfo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-03
         * 용       도 : 입고관리 목록 추가
         * Input    값 : RegistryWareHouseMngInfo(입고순번, 발주순번, 발주신청상세순번, 기업코드, 작성자사번, 작성자IP)
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
        public static DataTable RegistryWareHouseMngInfo(string strWarehouseSeq, string strOrderSeq, int intOrderDetSeq, string strCompNo, string strCompCd, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.InsertWareHouseMngInfo(strWarehouseSeq, strOrderSeq, intOrderDetSeq, strCompNo, strCompCd, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion 

        #region ModifyStoredGoodInfo : 입고관리 목록 수정 WarehousingMngInfo

        /**********************************************************************************************
         * Mehtod   명 : ModifyStoredGoodInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록 수정 WarehousingMngInfo
         * Input    값 : ModifyStoredGoodInfo(입고순번,발주신청상세순번, 입고수량, 입고일)
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
        public static DataTable ModifyStoredGoodInfo(string strOrderSeq, int intOrderDetSeq, int intWareQty, string strWareDt)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.UpdateStoredGoodInfo(strOrderSeq, intOrderDetSeq, intWareQty, strWareDt);

            return dtReturn;
        }

        #endregion

        #region RegistryStoredGoodInfo : 입고관리 목록 추가 WarehousingReceiptInfo

        /**********************************************************************************************
         * Mehtod   명 : RegistryStoredGoodInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록 추가 WarehousingReceiptInfo
         * Input    값 : RegistryStoredGoodInfo(입고순번,발주신청상세순번, 입고수량, 입고일, 신청자사번, 비고, 작성자사번, 작성자IP)
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
        public static DataTable RegistryStoredGoodInfo(string strOrderSeq, int intOrderDetSeq, int intWareQty, string strWareDt, string strReceitMemNo, string strRemark, string strCompCd, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.InsertStoredGoodInfo(strOrderSeq, intOrderDetSeq, intWareQty, strWareDt, strReceitMemNo, strRemark, strCompCd, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion

        #region WatchStoredGoodDetailInfo : 입고관리 목록상세보기

        /**********************************************************************************************
         * Mehtod   명 : WatchStoredGoodDetailInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-10-29
         * 용       도 : 입고관리 목록상세보기
         * Input    값 : WatchStoredGoodDetailInfo(입고물품번호, 언어코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기
        /// </summary>
        /// <param name="strOrderSeq">입고물품번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable WatchStoredGoodDetailInfo(string strOrderSeq, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.SelectStoredGoodDetailInfo(strOrderSeq, strLangCd);

            return dtReturn;
        }

        #endregion  

        #region RegistryStoredPayInfo : 입고관리 목록 추가 WarehousingPayInfo

        /**********************************************************************************************
         * Mehtod   명 : RegistryStoredPayInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-01
         * 용       도 : 입고관리 목록 추가 WarehousingPayInfo
         * Input    값 : RegistryStoredPayInfo(입고순번,발주신청상세순번, 지급액, 지급날짜, 지급수단, 비고, 작성자, 작성자IP)
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
        public static DataTable RegistryStoredPayInfo(string strOrderSeq, int intOrderDetSeq, double dbPayAmt, string strPayedDt, string strPaymentCd, string strRemark, string strCompCd, string strInsMemNo, string strInsMemIp)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.InsertStoredPayInfo(strOrderSeq, intOrderDetSeq, dbPayAmt, strPayedDt, strPaymentCd, strRemark, strCompCd, strInsMemNo, strInsMemIp);

            return dtReturn;
        }

        #endregion

        #region WatchStoredPayDetailInfo : 입고관리 목록상세보기(금액)

        /**********************************************************************************************
         * Mehtod   명 : WatchStoredPayDetailInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-01
         * 용       도 : 입고관리 목록상세보기(금액)
         * Input    값 : WatchStoredPayDetailInfo(입고물품번호,언어코드)
         * Ouput    값 : DataTable
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기(금액)
        /// </summary>
        /// <param name="strOrderSeq">입고물품번호</param>
        /// <param name="strLangCd">언어코드</param>
        /// <returns></returns>
        public static DataTable WatchStoredPayDetailInfo(string strOrderSeq, string strLangCd)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.SelectStoredPayDetailInfo(strOrderSeq, strLangCd);

            return dtReturn;
        }

        #endregion  

        #region WatchStoredPayInfo : 입고관리 목록 DropDownList(금액)

        /**********************************************************************************************
         * Mehtod   명 : WatchStoredPayInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-01
         * 용       도 : 입고관리 목록 DropDownList(금액)
         * Input    값 : WatchStoredPayInfo(주문번호, 품목코드)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 입고관리 목록상세보기
        /// </summary>
        /// <param name="strOrderSeq">주문번호</param>
        /// <param name="strClassCd">품목코드</param>
        /// <returns></returns>
        public static DataTable WatchStoredPayInfo(string strOrderSeq, int intOrderDetSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.SelectStoredPayInfo(strOrderSeq, intOrderDetSeq);

            return dtReturn;
        }

        #endregion  

        #region WatchReleaseRequestInfo : 출고요청 여부 확인

        /**********************************************************************************************
         * Mehtod   명 : WatchReleaseRequestInfo
         * 개   발  자 : 김범수
         * 생   성  일 : 2010-11-03
         * 용       도 : 출고요청 여부 확인
         * Input    값 : WatchStoredDetailInfo(출고요청번호, 출고요청상세번호)
         * Ouput    값 : DataSet
         **********************************************************************************************/
        /// <summary>
        /// 출고요청 여부 확인
        /// </summary>
        /// <param name="strOrderSeq">출고요청번호</param>
        /// <param name="strOrderDetSeq">출고요청상세번호</param>
        /// <returns></returns>
        public static DataTable WatchReleaseRequestInfo(string strOrderSeq, int intOrderDetSeq)
        {
            DataTable dtReturn = new DataTable();

            dtReturn = WarehouseMngDao.SelectReleaseRequestInfo(strOrderSeq, intOrderDetSeq);

            return dtReturn;
        }

        #endregion
    }


}
