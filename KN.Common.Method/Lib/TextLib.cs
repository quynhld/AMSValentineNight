using System;
using System.Text;
using System.Text.RegularExpressions;

namespace KN.Common.Method.Lib
{
    /// <summary>
    /// 텍스트 처리 관련 클래스
    /// </summary>
    public class TextLib
    {
        #region TextCutString : 문자열을 일정길이의 바이트로 자르고 말줄임표를 붙임.

        /**********************************************************************************************
         * Mehtod   명 : TextCutString
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-29
         * 용       도 : 문자열을 일정길이의 바이트로 자르고 말줄임표를 붙임.
         * Input    값 : TextCutstring(문자열, 최대치길이(바이트단위), 말줄임표)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// 문자열을 일정길이의 바이트로 자르고 말줄임표를 붙임.
        /// </summary>
        /// <param name="strText">문자열</param>
        /// <param name="strBtyLength">최대치길이(바이트단위)</param>
        /// <param name="strCutText">말줄임표</param>
        /// <returns>string</returns>
        public static string TextCutString(string strText, int strBtyLength, string strCutText)
        {
            // TODO : 문자열을 인코딩 - 유니코드를 ks_c_5601-1987로 변환
            System.Text.Encoding TextEncoding = System.Text.Encoding.GetEncoding("ks_c_5601-1987");

            int iSize = 0;
            string CutTextResult = "";

            for (int intTmpI = 0; intTmpI < strText.Length; intTmpI++)
            {
                byte[] arrBty = TextEncoding.GetBytes(strText[intTmpI].ToString());
                iSize += arrBty.Length;

                if (iSize > strBtyLength)
                {
                    break;
                }
                else
                {
                    CutTextResult += strText[intTmpI].ToString();
                }
            }

            // 줄임말 텍스트 합친 후 리턴
            if (iSize > strBtyLength)
            {
                CutTextResult = CutTextResult + strCutText.ToString();
            }
            return CutTextResult;
        }

        #endregion

        #region StringEncoder : 문자열의 특수문자를 변형함.

        /**********************************************************************************************
         * Mehtod   명 : StringEncoder
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-29
         * 용       도 : 문자열의 특수문자를 변형함.
         * Input    값 : StringEncoder(string[StringBuilder])
         * Ouput    값 : string / StringBuilder
         **********************************************************************************************/
        /// <summary>
        /// 문자열의 특수문자를 제거함.
        /// </summary>
        /// <param name="strOrigin">문자열</param>
        /// <returns>변형된 문자열</returns>
        public static string StringEncoder(string strOrigin)
        {
            strOrigin = strOrigin.Trim();
            strOrigin = strOrigin.Replace("&", "&amp;");
            strOrigin = strOrigin.Replace("'", "&#39;");
            strOrigin = strOrigin.Replace("\"", "&quot;");
            strOrigin = strOrigin.Replace("-", "&#45;");
            strOrigin = strOrigin.Replace("<", "&lt;");
            strOrigin = strOrigin.Replace(">", "&gt;");
            strOrigin = strOrigin.Replace("\r\n", "<br />");

            return strOrigin;
        }

        public static StringBuilder StringEncoder(StringBuilder strOrigin)
        {
            strOrigin = strOrigin.Replace("&", "&amp;");
            strOrigin = strOrigin.Replace("'", "&#39;");
            strOrigin = strOrigin.Replace("\"", "&quot;");
            strOrigin = strOrigin.Replace("-", "&#45;");
            strOrigin = strOrigin.Replace("<", "&lt;");
            strOrigin = strOrigin.Replace(">", "&gt;");
            strOrigin = strOrigin.Replace("\r\n", "<br />");

            return strOrigin;
        }
        
        #endregion

        #region StringDecoder : 변형된 문자열의 특수문자를 복구함.

        /**********************************************************************************************
         * Mehtod   명 : StringDecoder
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-29
         * 용       도 : 변형된 문자열의 특수문자를 복구함.
         * Input    값 : StringDecoder(string[StringBuilder])
         * Ouput    값 : string / StringBuilder
         **********************************************************************************************/
        /// <summary>
        /// 변형된 문자열의 특수문자를 복구함.
        /// </summary>
        /// <param name="strOrigin">변형된 문자열</param>
        /// <returns>복구된 문자열</returns>
        public static StringBuilder StringDecoder(StringBuilder strOrigin)
        {
            strOrigin = strOrigin.Replace("<br />", "\r\n");
            strOrigin = strOrigin.Replace("&gt;", ">");
            strOrigin = strOrigin.Replace("&lt;", "<");
            strOrigin = strOrigin.Replace("&#45;", "-");
            strOrigin = strOrigin.Replace("&quot;", "\"");
            strOrigin = strOrigin.Replace("&#39;", "'");
            strOrigin = strOrigin.Replace("&amp;", "&");
            
            return strOrigin;
        }

        public static string StringDecoder(string strOrigin)
        {
            strOrigin = strOrigin.Replace("<br />", "\r\n");
            strOrigin = strOrigin.Replace("&gt;", ">");
            strOrigin = strOrigin.Replace("&lt;", "<");
            strOrigin = strOrigin.Replace("&#45;", "-");
            strOrigin = strOrigin.Replace("&quot;", "\"");
            strOrigin = strOrigin.Replace("&#39;", "'");
            strOrigin = strOrigin.Replace("&amp;", "&");

            return strOrigin;
        }

        #endregion

        #region RemoveTag : Tag를 제거함

        /**********************************************************************************************
         * Mehtod   명 : RemoveTag
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-29
         * 용       도 : 태그를 제거함
         * Input    값 : RemoveTag(string[StringBuilder])
         * Ouput    값 : string / StringBuilder
         **********************************************************************************************/
        /// <summary>
        /// 태그를 제거함
        /// </summary>
        /// <param name="strText">문자열</param>
        /// <returns>태그가 제거된 문자열</returns>
        public static string RemoveTag(string strText)
        {
            string strReturns = string.Empty;
            Regex regex = new Regex("<[^>]+>");

            strReturns = regex.Replace(strText, "");

            return strReturns;
        }

        public static StringBuilder RemoveTag(StringBuilder strText)
        {
            StringBuilder strReturns;
            string strTmp = string.Empty;
            Regex regex = new Regex("<[^>]+>");

            strTmp = strText.ToString();

            strReturns = new StringBuilder(regex.Replace(strTmp, ""));

            return strReturns;
        }

        #endregion

        #region RemoveTagAndCut : Tag 제거및 글자 자른후 말줄임

        /**********************************************************************************************
         * Mehtod   명 : RemoveTagAndCut
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-06-29
         * 용       도 : Tag 제거및 글자 자른후 말줄임
         * Input    값 : RemoveTagAndCut(원문텍스트, 자를길이, 덧붙일말줄임표)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// Tag 제거및 글자 자른후 말줄임
        /// </summary>
        /// <param name="strText">내용</param>
        /// <param name="CutLenght">문자를 자를수</param>
        /// <param name="strCutText">말줄임표시</param>
        /// <returns></returns>
        public static string RemoveTagAndCut(string strText,int CutLenght, string strCutText)
        {
            string strReturns = string.Empty;
            Regex regex = new Regex("<[^>]+>");

            strReturns = regex.Replace(strText, "");
            strReturns = TextCutString(strReturns, CutLenght, strCutText);

            
            return strReturns;
        }

        #endregion

        #region MakeNullToEmpty : NULL값일 경우 빈값으로 리턴함.

        /**********************************************************************************************
         * Mehtod   명 : MakeNullToEmpty
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-28
         * 용       도 : NULL값일 경우 빈값으로 리턴함.
         * Input    값 : MakeNullToEmpty(원문텍스트)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// NULL값일 경우 빈값으로 리턴함.
        /// </summary>
        /// <param name="strText">원문텍스트</param>
        /// <returns></returns>
        public static string MakeNullToEmpty(string strText)
        {
            string strReturn = string.Empty;

            if (string.IsNullOrEmpty(strText))
            {
                strText = "";
            }

            strReturn = strText;

            return strReturn;
        }

        #endregion

        #region MakeNullToZero : NULL값일 경우 0값으로 리턴함.

        /**********************************************************************************************
         * Mehtod   명 : MakeNullToZero
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-22
         * 용       도 : NULL값일 경우 0값으로 리턴함.
         * Input    값 : MakeNullToZero(원문텍스트)
         * Ouput    값 : int
         **********************************************************************************************/
        /// <summary>
        /// NULL값일 경우 빈값으로 리턴함.
        /// </summary>
        /// <param name="objNumber">원문텍스트</param>
        /// <returns></returns>
        public static object MakeNullToZero(object objNumber)
        {
            object objReturn = null;

            if (typeof(Int32).Equals(objNumber.GetType()))
            {
                objReturn = Int32.Parse(objNumber.ToString());
            }
            else if (typeof(double).Equals(objNumber.GetType()))
            {
                objReturn = double.Parse(objNumber.ToString());
            }
            else
            {
                objReturn = 0;
            }

            return objReturn;
        }

        #endregion

        #region MakeVietRealNo : 일반적인 숫자를 베트남 타입 실수숫자로 리턴함.

        /**********************************************************************************************
         * Mehtod   명 : MakeVietRealNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-22
         * 용       도 : 일반적인 숫자를 베트남 타입 실수숫자로 리턴함.
         * Input    값 : MakeVietRealNo(원문텍스트)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// 일반적인 숫자를 베트남 타입 실수숫자로 리턴함.
        /// </summary>
        /// <param name="strNormalNumber">원문텍스트</param>
        /// <returns></returns>
        public static string MakeVietRealNo(string strNormalNumber)
        {
            string strReturn = string.Empty;

            strNormalNumber = strNormalNumber.Replace(',', '@');
            strNormalNumber = strNormalNumber.Replace('.', ',');
            strNormalNumber = strNormalNumber.Replace('@', '.');

            strReturn = strNormalNumber;

            return strReturn;
        }

        #endregion

        #region MakeVietIntNo : 일반적인 숫자를 베트남 타입 정수숫자로 리턴함.

        /**********************************************************************************************
         * Mehtod   명 : MakeVietIntNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-12-22
         * 용       도 : 일반적인 숫자를 베트남 타입 정수숫자로 리턴함.
         * Input    값 : MakeVietIntNo(원문텍스트)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// 일반적인 숫자를 베트남 타입 정수숫자로 리턴함.
        /// </summary>
        /// <param name="strNormalNumber">원문텍스트</param>
        /// <returns></returns>
        public static string MakeVietIntNo(string strNormalNumber)
        {
            string strReturn = string.Empty;

            strNormalNumber = strNormalNumber.Replace(',', '.');

            strReturn = strNormalNumber;

            return strReturn;
        }

        #endregion

        #region MakeOriginRealNo : 베트남 타입 실수숫자를 일반적인 숫자로 리턴함.

        /**********************************************************************************************
         * Mehtod   명 : MakeOriginRealNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-02
         * 용       도 : 베트남 타입 실수숫자를 일반적인 숫자로 리턴함.
         * Input    값 : MakeOriginRealNo(원문텍스트)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// 베트남 타입 실수숫자를 일반적인 숫자로 리턴함.
        /// </summary>
        /// <param name="strNormalNumber">원문텍스트</param>
        /// <returns></returns>
        public static string MakeOriginRealNo(string strNormalNumber)
        {
            string strReturn = string.Empty;

            strNormalNumber = strNormalNumber.Replace(".", "");
            strNormalNumber = strNormalNumber.Replace(",", ".");

            strReturn = strNormalNumber;

            return strReturn;
        }

        #endregion

        #region MakeOriginIntNo : 베트남 타입 정수숫자를 일반적인 숫자로 리턴함.

        /**********************************************************************************************
         * Mehtod   명 : MakeOriginIntNo
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-03-02
         * 용       도 : 베트남 타입 정수숫자를 일반적인 숫자로 리턴함.
         * Input    값 : MakeOriginIntNo(원문텍스트)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// 베트남 타입 정수숫자를 일반적인 숫자로 리턴함.
        /// </summary>
        /// <param name="strNormalNumber">원문텍스트</param>
        /// <returns></returns>
        public static string MakeOriginIntNo(string strNormalNumber)
        {
            string strReturn = string.Empty;

            strNormalNumber = strNormalNumber.Replace(".", ",");

            strReturn = strNormalNumber;

            return strReturn;
        }

        #endregion

        #region MakeDateEightDigit : 8자리 숫자형 문자열을 YYYY-MM-DD 형태로 반환함.

        /**********************************************************************************************
         * Mehtod   명 : MakeDateEightDigit
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-04
         * 용       도 : 8자리 숫자형 문자열을 YYYY-MM-DD 형태로 반환함.
         * Input    값 : MakeDateEightDigit(원문텍스트)
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// 8자리 숫자형 문자열을 YYYY-MM-DD 형태로 반환함.
        /// </summary>
        /// <param name="strText">원문텍스트</param>
        /// <returns></returns>
        public static string MakeDateEightDigit(string strText)
        {
            string strReturn = string.Empty;

            if (strText.Length != 8)
            {
                strReturn = "";
            }
            else
            {
                StringBuilder sbList = new StringBuilder();

                sbList.Append(strText.Substring(0, 4));
                sbList.Append("-");
                sbList.Append(strText.Substring(4, 2));
                sbList.Append("-");
                sbList.Append(strText.Substring(6, 2));

                strReturn = sbList.ToString();
            }

            return strReturn;
        }

        public static string MakeDateSixDigit(string strText)
        {
            string strReturn = string.Empty;

            if (strText.Length != 6)
            {
                strReturn = "";
            }
            else
            {
                StringBuilder sbList = new StringBuilder();

                sbList.Append(strText.Substring(0, 4));
                sbList.Append("-");
                sbList.Append(strText.Substring(4, 2));

                strReturn = sbList.ToString();
            }

            return strReturn;
        }

        #endregion

        #region MakeDateEightSlash : Make slash for date
        /**********************************************************************************************
         * Mehtod   명 : MakeDateSlash
         * 개   발  자 : 양영석
         * 생   성  일 : 2013-04-05
         * 용       도 : 8자리 숫자형 문자열을 YYYY-MM-DD 형태로 반환함.
         * Input    값 : MakeDateSlash
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// 8자리 숫자형 문자열을 YYYY-MM-DD 형태로 반환함.
        /// </summary>
        /// <param name="strText">원문텍스트</param>
        /// <returns></returns>
        public static string MakeDateEightSlash(string strText)
        {
            string strReturn = string.Empty;

            if (strText.Length != 8)
            {
                strReturn = "";
            }
            else
            {
                StringBuilder sbList = new StringBuilder();

                sbList.Append(strText.Substring(0, 4));
                sbList.Append("/");
                sbList.Append(strText.Substring(4, 2));
                sbList.Append("/");
                sbList.Append(strText.Substring(6, 2));

                strReturn = sbList.ToString();
            }

            return strReturn;
        }

        public static string MakeDateSixSlash(string strText)
        {
            string strReturn = string.Empty;

            if (strText.Length != 6)
            {
                strReturn = "";
            }
            else
            {
                StringBuilder sbList = new StringBuilder();

                sbList.Append(strText.Substring(0, 4));
                sbList.Append("/");
                sbList.Append(strText.Substring(4, 2));

                strReturn = sbList.ToString();
            }

            return strReturn;
        }

        #endregion

        #region MakeRoundDownHundred : 100자리 버린값으로 리턴함.

        /**********************************************************************************************
         * Mehtod   명 : MakeRoundDownHundred
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-01-19
         * 용       도 : 100자리 버린값으로 리턴함.
         * Input    값 : MakeRoundDownHundred(원문텍스트)
         * Ouput    값 : double
         **********************************************************************************************/
        /// <summary>
        /// 100자리 버린값으로 리턴함.
        /// </summary>
        /// <param name="dblNumber">원문텍스트</param>
        /// <returns></returns>
        public static double MakeRoundDownHundred(double dblNumber)
        {
            double dblReturn = 0d;

            dblReturn = dblNumber / 100d;

            dblReturn = Math.Truncate(dblReturn) * 100;

            return dblReturn;
        }

        #endregion

        #region MakeRoundDownThousand : 1000자리 버린값으로 리턴함.

        /**********************************************************************************************
         * Mehtod   명 : MakeRoundDownThousand
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-01-19
         * 용       도 : 1000자리 버린값으로 리턴함.
         * Input    값 : MakeRoundDownThousand(원문텍스트)
         * Ouput    값 : double
         **********************************************************************************************/
        /// <summary>
        /// 1000자리 버린값으로 리턴함.
        /// </summary>
        /// <param name="dblNumber">원문텍스트</param>
        /// <returns></returns>
        public static double MakeRoundDownThousand(double dblNumber)
        {
            double dblReturn = 0d;

            dblReturn = dblNumber / 1000d;

            dblReturn = Math.Truncate(dblReturn) * 1000;

            return dblReturn;
        }

        #endregion
    }
}
