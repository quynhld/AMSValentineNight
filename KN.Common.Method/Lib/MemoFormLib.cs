using System;
using System.Data;
using System.Text;
using System.Web;

using KN.Common.Base.Code;
using KN.Common.Method.Util;

namespace KN.Common.Method.Lib
{
    public class MemoFormLib
    {
        #region MakeReleaseChargeForm : 출고 담당자 결제 메모 폼

        /**********************************************************************************************
         * Mehtod   명 : MakeReleaseChargeForm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-11
         * 용       도 : 출고 담당자 결제 메모 폼
         * Input    값 : MakeReleaseChargeForm(담당자명, 출고번호)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeReleaseChargeForm : 출고 담당자 결제 메모 폼
        /// </summary>
        /// <param name="strChargerNm">담당자명</param>
        /// <param name="strReleaseSeq">출고번호</param>
        /// <returns></returns>
        public static string MakeReleaseChargeForm(string strChargerNm, string strReleaseSeq)
        {
            string strReturn = string.Empty;

            StringBuilder sbTxt = new StringBuilder();

            // 양영석 : 영문 & 베트남문 번역 필요
            sbTxt.Append("<table class='mailTb'>");
            sbTxt.Append("	<tr>                                                           ");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg1.gif' alt='경남기업' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='mCont'>");
            
            if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                sbTxt.Append("		    <span>" + strChargerNm + "님</span>");
                sbTxt.Append("		    <span>결제 처리하실 안건이 있습니다. 확인 부탁드립니다.</span> <span class='FloatR2'> <div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>결제처리하기</a></span></div></div></div></div> </span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_ENGLISH))
            {
                sbTxt.Append("		    <span>" + strChargerNm + "</span>");
                sbTxt.Append("		    <span>(영문)결제 처리하실 안건이 있습니다. 확인 부탁드립니다.</span> <span class='FloatR2'> <div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>결제처리하기</a></span></div></div></div></div> </span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_VIETNAMESE))
            {
                sbTxt.Append("		    <span>" + strChargerNm + "</span>");
                sbTxt.Append("		    <span>Có trường hợp cần xử lý thanh toán. Đề nghị kiểm tra.</span> <span class='FloatR2'> <div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>결제처리하기</a></span></div></div></div></div> </span>");
            }

            sbTxt.Append("		</td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr><td class='P0'>- 영문 / 베트남문 번역 필요 -</td></tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg3.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("</table>");

            strReturn = sbTxt.ToString();

            return strReturn;
        }

        #endregion

        #region MakeReleaseConfirmForm : 출고 승인 메모 폼

        /**********************************************************************************************
         * Mehtod   명 : MakeReleaseConfirmForm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-12
         * 용       도 : 출고 승인 메모 폼
         * Input    값 : MakeReleaseConfirmForm(신청자, 출고번호)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeReleaseConfirmForm : 출고 승인 메모 폼
        /// </summary>
        /// <param name="strApplierNm">신청자</param>
        /// <param name="strReleaseSeq">출고번호</param>
        /// <returns></returns>
        public static string MakeReleaseConfirmForm(string strApplierNm, string strReleaseSeq)
        {
            string strReturn = string.Empty;

            StringBuilder sbTxt = new StringBuilder();

            // 양영석 : 영문 & 베트남문 번역 필요
            sbTxt.Append("<table class='mailTb'>");
            sbTxt.Append("	<tr>                                                           ");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg1.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='mCont'>");
            
            if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "님</span>");
                sbTxt.Append("		    <span>출고 승인되었습니다. 확인 부탁드립니다.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>출고승인확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_ENGLISH))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>(영문)출고 승인되었습니다. 확인 부탁드립니다.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>출고승인확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_VIETNAMESE))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>Cho phép xuất kho. Đề nghị xác nhận.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>출고승인확인하기</a></span></div></div></div></div></span>");
            }

            sbTxt.Append("		</td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr><td class='P0'>- 영문 / 베트남문 번역 필요 -</td></tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg3.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("</table>");

            strReturn = sbTxt.ToString();

            return strReturn;
        }

        #endregion

        #region MakeReleaseDenyForm : 출고 반려 메모 폼

        /**********************************************************************************************
         * Mehtod   명 : MakeReleaseDenyForm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-14
         * 용       도 : 출고 반려 메모 폼
         * Input    값 : MakeReleaseDenyForm(신청자, 출고번호)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeReleaseDenyForm : 출고 반려 메모 폼
        /// </summary>
        /// <param name="strApplierNm">신청자</param>
        /// <param name="strReleaseSeq">출고번호</param>
        /// <returns></returns>
        public static string MakeReleaseDenyForm(string strApplierNm, string strReleaseSeq)
        {
            string strReturn = string.Empty;

            StringBuilder sbTxt = new StringBuilder();

            // 양영석 : 영문 & 베트남문 번역 필요
            sbTxt.Append("<table class='mailTb'>");
            sbTxt.Append("	<tr>                                                           ");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg1.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='mCont'>");
            
            if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "님</span>");
                sbTxt.Append("		    <span>출고 신청이 반려되었습니다. 죄송합니다.</span><span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>출고반려확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_ENGLISH))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>(영문)출고 신청이 반려되었습니다. 죄송합니다.</span><span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>출고반려확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_VIETNAMESE))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>Đề nghị xuất kho bị từ chối. Xin lỗi.</span><span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strReleaseSeq + "'>출고반려확인하기</a></span></div></div></div></div></span>");
            }

            sbTxt.Append("		</td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr><td class='P0'>- 영문 / 베트남문 번역 필요 -</td></tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg3.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("</table>");

            strReturn = sbTxt.ToString();

            return strReturn;
        }

        #endregion

        #region MakeOrderChargeForm : 발주 담당자 결제 메모 폼

        /**********************************************************************************************
         * Mehtod   명 : MakeOrderChargeForm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 발주 담당자 결제 메모 폼
         * Input    값 : MakeOrderChargeForm(담당자명, 발주번호)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeOrderChargeForm : 발주 담당자 결제 메모 폼
        /// </summary>
        /// <param name="strChargerNm">담당자명</param>
        /// <param name="strOrderSeq">발주번호</param>
        /// <returns></returns>
        public static string MakeOrderChargeForm(string strChargerNm, string strOrderSeq)
        {
            string strReturn = string.Empty;

            StringBuilder sbTxt = new StringBuilder();

            // 양영석 : 영문 & 베트남문 번역 필요
            sbTxt.Append("<table class='mailTb'>");
            sbTxt.Append("	<tr>                                                           ");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg1.gif' alt='경남기업' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='mCont'>");

            if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                sbTxt.Append("		    <span>" + strChargerNm + "님</span>");
                sbTxt.Append("		    <span>결제 처리하실 안건이 있습니다. 확인 부탁드립니다.</span> <span class='FloatR2'> <div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>결제처리하기</a></span></div></div></div></div> </span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_ENGLISH))
            {
                sbTxt.Append("		    <span>" + strChargerNm + "</span>");
                sbTxt.Append("		    <span>(영문)결제 처리하실 안건이 있습니다. 확인 부탁드립니다.</span> <span class='FloatR2'> <div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>결제처리하기</a></span></div></div></div></div> </span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_VIETNAMESE))
            {
                sbTxt.Append("		    <span>" + strChargerNm + "</span>");
                sbTxt.Append("		    <span>Có trường hợp cần xử lý thanh toán. Đề nghị kiểm tra.</span> <span class='FloatR2'> <div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>결제처리하기</a></span></div></div></div></div> </span>");
            }

            sbTxt.Append("		</td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr><td class='P0'>- 영문 / 베트남문 번역 필요 -</td></tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg3.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("</table>");

            strReturn = sbTxt.ToString();

            return strReturn;
        }

        #endregion

        #region MakePurchaseRequestForm : 구매 요청 메모 폼

        /**********************************************************************************************
         * Mehtod   명 : MakePurchaseRequestForm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 구매 요청 메모 폼
         * Input    값 : MakeReleaseConfirmForm(신청자, 발주번호)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakePurchaseRequestForm : 구매 요청 메모 폼
        /// </summary>
        /// <param name="strApplierNm">신청자</param>
        /// <param name="strReleaseSeq">발주번호</param>
        /// <returns></returns>
        public static string MakePurchaseRequestForm(string strApplierNm, string strOrderSeq)
        {
            string strReturn = string.Empty;

            StringBuilder sbTxt = new StringBuilder();

            // 양영석 : 영문 & 베트남문 번역 필요
            sbTxt.Append("<table class='mailTb'>");
            sbTxt.Append("	<tr>                                                           ");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg1.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='mCont'>");

            if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "님</span>");
                sbTxt.Append("		    <span>발주 승인되었습니다. 구매 부탁드립니다.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>구매사항확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_ENGLISH))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>(영문)발주 승인되었습니다. 구매 부탁드립니다.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>구매사항확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_VIETNAMESE))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>Đề nghị đặt hàng đã được cho phép. Hãy mua hàng.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>구매사항확인하기</a></span></div></div></div></div></span>");
            }

            sbTxt.Append("		</td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr><td class='P0'>- 영문 / 베트남문 번역 필요 -</td></tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg3.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("</table>");

            strReturn = sbTxt.ToString();

            return strReturn;
        }

        #endregion

        #region MakePurchaseDenyForm : 발주 반려 메모 폼

        /**********************************************************************************************
         * Mehtod   명 : MakePurchaseDenyForm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-11-02
         * 용       도 : 발주 반려 메모 폼
         * Input    값 : MakePurchaseDenyForm(신청자, 출고번호)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakePurchaseDenyForm : 발주 반려 메모 폼
        /// </summary>
        /// <param name="strApplierNm">신청자</param>
        /// <param name="strOrderSeq">출고번호</param>
        /// <returns></returns>
        public static string MakePurchaseDenyForm(string strApplierNm, string strOrderSeq)
        {
            string strReturn = string.Empty;

            StringBuilder sbTxt = new StringBuilder();

            // 양영석 : 영문 & 베트남문 번역 필요
            sbTxt.Append("<table class='mailTb'>");
            sbTxt.Append("	<tr>                                                           ");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg1.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='mCont'>");

            if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "님</span>");
                sbTxt.Append("		    <span>발주 신청이 반려되었습니다. 죄송합니다.</span><span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>발주반려확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_ENGLISH))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>(영문)발주 신청이 반려되었습니다. 죄송합니다.</span><span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>발주반려확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_VIETNAMESE))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>Yêu cầu đặt hàng đã bị từ chối. Xin lỗi.</span><span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_ORDERVIEW + "?" + CommValue.PAGEPARAM_VALUE_PURCHASEVIEW + "=" + strOrderSeq + "'>발주반려확인하기</a></span></div></div></div></div></span>");
            }

            sbTxt.Append("		</td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr><td class='P0'>- 영문 / 베트남문 번역 필요 -</td></tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg3.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("</table>");

            strReturn = sbTxt.ToString();

            return strReturn;
        }

        #endregion

        #region MakeWarehousingConfirmForm : 입고 완료 메모 폼

        /**********************************************************************************************
         * Mehtod   명 : MakeWarehousingConfirmForm
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-10-15
         * 용       도 : 입고 완료 메모 폼
         * Input    값 : MakeReleaseConfirmForm(신청자, 발주번호)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakeWarehousingConfirmForm : 입고 완료 메모 폼
        /// </summary>
        /// <param name="strApplierNm">신청자</param>
        /// <param name="strOrderSeq">발주번호</param>
        /// <returns></returns>
        public static string MakeWarehousingConfirmForm(string strApplierNm, string strOrderSeq)
        {
            string strReturn = string.Empty;

            StringBuilder sbTxt = new StringBuilder();

            // 양영석 : 영문 & 베트남문 번역 필요
            sbTxt.Append("<table class='mailTb'>");
            sbTxt.Append("	<tr>                                                           ");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg1.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='mCont'>");

            if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "님</span>");
                sbTxt.Append("		    <span>입고 완료되었습니다. 수령 부탁드립니다.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strOrderSeq + "'>입고확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_ENGLISH))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>(영문)입고 완료되었습니다. 수령 부탁드립니다.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strOrderSeq + "'>입고확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_VIETNAMESE))
            {
                sbTxt.Append("		    <span>" + strApplierNm + "</span>");
                sbTxt.Append("		    <span>Đã hoàn tất nhập kho, đề nghị đến lấy hàng</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_RELEASEVIEW + "?" + CommValue.PAGEPARAM_VALUE_RELEASEVIEW + "=" + strOrderSeq + "'>입고확인하기</a></span></div></div></div></div></span>");
            }

            sbTxt.Append("		</td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr><td class='P0'>- 영문 / 베트남문 번역 필요 -</td></tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg3.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("</table>");

            strReturn = sbTxt.ToString();

            return strReturn;
        }

        #endregion

        #region MakeMasterkeyRequestForm : 분할납부 요청 메모 폼

        /**********************************************************************************************
         * Mehtod   명 : MakeMasterkeyRequestForm
         * 개   발  자 : 김범수
         * 생   성  일 : 2011-01-07
         * 용       도 : 분할납부 요청 메모 폼
         * Input    값 : MakeMasterkeyRequestForm()
         * Ouput    값 : string
         **********************************************************************************************/

        public static string MakeMasterkeyRequestForm()
        {
            string strReturn = string.Empty;

            StringBuilder sbTxt = new StringBuilder();

            // 양영석 : 영문 & 베트남문 번역 필요
            sbTxt.Append("<table class='mailTb'>");
            sbTxt.Append("	<tr>                                                           ");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg1.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='mCont'>");

            if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_KOREAN))
            {
                sbTxt.Append("		    <span>분할납부 요청되었습니다. 승인 부탁드립니다.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_MASTERKEYLIST + "'>요청내용확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_ENGLISH))
            {
                sbTxt.Append("		    <span>(영문요망)분할납부 요청되었습니다. 승인 부탁드립니다.</span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_MASTERKEYLIST + "'>요청내용확인하기</a></span></div></div></div></div></span>");
            }
            else if (HttpContext.Current.Session["LangCd"].ToString().Equals(CommValue.LANG_VALUE_VIETNAMESE))
            {
                sbTxt.Append("		    <span>Đề nghị được thanh toán từng phần, đề nghị cho phép </span> <span class='FloatR2'><div class='Btn-Type1-wp'><div class='Btn-Tp-L'><div class='Btn-Tp-R'><div class='Btn-Tp-M'><span><a href='" + CommValue.PAGE_VALUE_MASTERKEYLIST + "'>요청내용확인하기</a></span></div></div></div></div></span>");
            }

            sbTxt.Append("		</td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("	<tr><td class='P0'>- 영문 / 베트남문 번역 필요 -</td></tr>");
            sbTxt.Append("	<tr>");
            sbTxt.Append("		<td class='P0'><img src='/Common/Images/Common/KnImg3.gif' alt='' /></td>");
            sbTxt.Append("	</tr>");
            sbTxt.Append("</table>");

            strReturn = sbTxt.ToString();

            return strReturn;
        }

        #endregion
    }
}
