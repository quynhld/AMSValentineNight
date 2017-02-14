using System;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base.Code;

namespace KN.Common.Method.Lib
{
    /// <summary>
    /// 권한관련 Library
    /// </summary>
    public class AuthCheckLib
    {
        #region CheckAuthMenu : 권한여부에 따른 페이지 이동처리

        /**********************************************************************************************
         * Mehtod   명 : CheckAuthMenu
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : 권한여부에 따른 페이지 이동처리
         * Input    값 : CheckAuthMenu(접근권한, 페이지권한)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// CheckAuthMenu : 권한여부에 따른 페이지 이동처리
        /// </summary>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strPageAuth">페이지권한</param>
        public static void CheckAuthMenu(string strAccessAuth, string strPageAuth)
        {
            int intAccessAuth = 0;
            int intPageAuth = 0;
            int intFullAuth = 0;

            bool isAuthOk = false;

            // 페이지 권한값이 Full이면 권한체크 중단
            if (strPageAuth.Equals(CommValue.AUTH_VALUE_ENTIRE))
            {
                return;
            }

            // 각 권한값이 Null이거나 빈값이면 인덱스 페이지로 이동
            if (string.IsNullOrEmpty(strAccessAuth) || string.IsNullOrEmpty(strPageAuth))
            {
                HttpContext.Current.Response.Redirect(CommValue.PAGE_VALUE_INDEX, false);
            }
            else
            {
                intAccessAuth = Int32.Parse(strAccessAuth);
                intPageAuth = Int32.Parse(strPageAuth);
                intFullAuth = Int32.Parse(CommValue.AUTH_VALUE_FULL);

                // 접근권한값이 페이지권한값보다 작을 경우에만 Loop
                while (intAccessAuth <= intPageAuth)
                {
                    if (intFullAuth <= intPageAuth)
                    {
                        // 현재 페이지 권한값과 접근권한값이 같으면 True 반환
                        if (intFullAuth == intAccessAuth)
                        {
                            isAuthOk = true;
                            break;
                        }
                        // 현재 페이지 권한값이 개인권한값보다 작으면 False 반환
                        else if (intFullAuth < intAccessAuth)
                        {
                            break;
                        }

                        intPageAuth = intPageAuth - intFullAuth;
                        intFullAuth = intFullAuth / 2;
                    }
                    else
                    {
                        intFullAuth = intFullAuth / 2;
                    }
                }

                if (!isAuthOk)
                {
                    HttpContext.Current.Response.Redirect(CommValue.PAGE_VALUE_INDEX, false);
                }
            }
        }

        #endregion

        #region CheckAuthPage : 페이지별 권한여부 체크

        /**********************************************************************************************
         * Mehtod   명 : CheckAuthPage
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-18
         * 용       도 : 페이지별 권한여부 체크
         * Input    값 : CheckAuthPage(접근권한, 페이지권한)
         * Ouput    값 : 권한여부
         **********************************************************************************************/
        /// <summary>
        /// CheckAuthPage : 페이지별 권한여부 체크
        /// </summary>
        /// <param name="strAccessAuth">접근권한</param>
        /// <param name="strPageAuth">페이지권한</param>
        /// <returns>권한여부</returns>
        public static bool CheckAuthPage(string strAccessAuth, string strPageAuth)
        {
            int intAccessAuth = 0;
            int intPageAuth = 0;
            int intFullAuth = 0;

            bool isAuthOk = false;

            // 페이지 권한값이 Full이면 권한체크 중단
            if (strPageAuth.Equals(CommValue.AUTH_VALUE_ENTIRE))
            {
                isAuthOk = true;
            }
            else
            {
                // 각 권한값이 Null이거나 빈값이면 인덱스 페이지로 이동
                if (!string.IsNullOrEmpty(strAccessAuth) && !string.IsNullOrEmpty(strPageAuth))
                {
                    intAccessAuth = Int32.Parse(strAccessAuth);
                    intPageAuth = Int32.Parse(strPageAuth);
                    intFullAuth = Int32.Parse(CommValue.AUTH_VALUE_FULL);

                    // 접근권한값이 페이지권한값보다 작을 경우에만 Loop
                    while (intAccessAuth <= intPageAuth)
                    {
                        if (intFullAuth <= intPageAuth)
                        {
                            // 현재 페이지 권한값과 접근권한값이 같으면 True 반환
                            if (intFullAuth == intAccessAuth)
                            {
                                isAuthOk = true;
                                break;
                            }
                            // 현재 페이지 권한값이 개인권한값보다 작으면 False 반환
                            else if (intFullAuth < intAccessAuth)
                            {
                                break;
                            }

                            intPageAuth = intPageAuth - intFullAuth;
                            intFullAuth = intFullAuth / 2;
                        }
                        else
                        {
                            intFullAuth = intFullAuth / 2;
                        }
                    }
                }
            }

            return isAuthOk;
        }

        #endregion

        #region CheckSession : Session 여부에 따른 페이지 이동처리

        /**********************************************************************************************
         * Mehtod   명 : CheckSession
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-17
         * 용       도 : Session 여부에 따른 페이지 이동처리
         * Input    값 : CheckSession()
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// CheckSession : Session 여부에 따른 페이지 이동처리
        /// </summary>
        public static void CheckSession()
        {
            if (HttpContext.Current.Session["MemNo"] == null)
            {
                HttpContext.Current.Response.Redirect(CommValue.PAGE_VALUE_INDEX);
            }
            else if (string.IsNullOrEmpty(HttpContext.Current.Session["MemNo"].ToString()))
            {
                HttpContext.Current.Response.Redirect(CommValue.PAGE_VALUE_INDEX);
            }
        }

        /**********************************************************************************************
         * Mehtod   명 : CheckPopupSession
         * 개   발  자 : 양영석
         * 생   성  일 : 2011-05-20
         * 용       도 : Session 여부에 따른 Popup 페이지처리
         * Input    값 : CheckPopupSession()
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// CheckPopupSession : Session 여부에 따른 Popup 페이지처리
        /// </summary>
        public static string CheckPopupSession()
        {
            if (HttpContext.Current.Session["MemNo"] == null)
            {
                return CommValue.CLOSE_WINDOWS;
            }
            else if (string.IsNullOrEmpty(HttpContext.Current.Session["MemNo"].ToString()))
            {
                return CommValue.CLOSE_WINDOWS;
            }
            else
            {
                return string.Empty;
            }
        }

        #endregion

        #region CheckchkData : 권한용 체크박스 체크된 데이터만 처리

        /**********************************************************************************************
         * Mehtod   명 : CheckchkData
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-18
         * 용       도 : 권한용 체크박스 체크된 데이터만 처리
         * Input    값 : CheckchkData(CheckBoxList 객체, 합산값을 담을 TextBox 객체)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// CheckchkData : 권한용 체크박스 체크된 데이터만 처리
        /// </summary>
        /// <param name="chkList">CheckBoxList 객체</param>
        /// <param name="txtData">합산값을 담을 TextBox 객체</param>
        public static void CheckchkData(CheckBoxList chkList, TextBox txtData)
        {
            int intTotalAuth = 1;

            foreach (ListItem ltChkBox in chkList.Items)
            {
                if (ltChkBox.Selected)
                {
                    intTotalAuth = intTotalAuth + Int32.Parse(ltChkBox.Value);
                }
            }

            txtData.Text = intTotalAuth.ToString();
        }

        #endregion

        #region CheckFullData : 권한용 체크박스 전체 체크 처리

        /**********************************************************************************************
         * Mehtod   명 : CheckFullData
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-18
         * 용       도 : 권한용 체크박스 전체 체크 처리
         * Input    값 : CheckFullData(CheckBoxList 객체, 합산값을 담을 TextBox 객체)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// CheckFullData : 권한용 체크박스 전체 체크 처리
        /// </summary>
        /// <param name="chkList">CheckBoxList 객체</param>
        /// <param name="txtData">합산값을 담을 TextBox 객체</param>
        public static void CheckFullData(CheckBoxList chkList, TextBox txtData)
        {
            int intTotalAuth = 1;

            foreach (ListItem ltChkBox in chkList.Items)
            {
                ltChkBox.Selected = true;
                intTotalAuth = intTotalAuth + Int32.Parse(ltChkBox.Value);
            }

            txtData.Text = intTotalAuth.ToString();
        }

        #endregion

        #region CheckNoData : 권한용 체크박스 숫자입력 받은 후 체크처리

        /**********************************************************************************************
         * Mehtod   명 : CheckNoData
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-08-18
         * 용       도 : 권한용 체크박스 숫자입력 받은 후 체크처리
         * Input    값 : CheckNoData(CheckBoxList 객체, 입력받은 권한코드, 합산값을 담을 TextBox 객체)
         * Ouput    값 : void
         **********************************************************************************************/
        /// <summary>
        /// CheckNoData : 권한용 체크박스 숫자입력 받은 후 체크처리
        /// </summary>
        /// <param name="chkList">CheckBoxList 객체</param>
        /// <param name="intData">입력받은 권한코드</param>
        /// <param name="txtBasket">합산값을 담을 TextBox 객체</param>
        public static void CheckNoData(CheckBoxList chkList, int intData, TextBox txtBasket)
        {
            int intMaxValue = Int32.Parse(CommValue.AUTH_VALUE_FULL);
            int intTotalValue = intData;

            while (true)
            {
                if (intMaxValue < intData)
                {
                    break;
                }
                intMaxValue = intMaxValue / 2;
            }

            while (intData > 0 && intMaxValue > 0)
            {
                // 체크박스리스트 다중체크
                chkList.Items.FindByValue(intMaxValue.ToString().PadLeft(8, '0')).Selected = true;

                intData = intData - intMaxValue;
                while (true)
                {
                    if (intMaxValue < intData)
                    {
                        break;
                    }
                    intMaxValue = intMaxValue / 2;
                }
            }

            txtBasket.Text = intTotalValue.ToString();
        }

        #endregion
    }
}
