using System.Text;

namespace KN.Common.Method.Util
{
    /// <summary>
    /// 페이징 관련 처리 Utility
    /// </summary>
    public class PageNoListUtil
    {
        #region MakePageIndex : 페이징인덱스를 만듦
        /**********************************************************************************************
         * Mehtod   명 : MakePageIndex
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-12
         * 용       도 : 페이징인덱스를 만듦
         * Input    값 : MakePageIndex(현재 페이지번호, 페이지당 리스트 수, 전체레코드수, 텍스트'처음', 텍스트'마지막', 텍스트'이전', 텍스트'다음')
         * Ouput    값 : string
         **********************************************************************************************/
        /// <summary>
        /// MakePageIndex : 페이징인덱스를 만듦
        /// </summary>
        /// <param name="intPageNo">현재 페이지번호</param>
        /// <param name="intItemPerPage">페이지당 리스트 수</param>
        /// <param name="intEntireRecordNo">전체레코드수</param>
        /// <param name="strStart">텍스트'처음'</param>
        /// <param name="strEnd">텍스트'마지막'</param>
        /// <param name="strPrev">텍스트'이전'</param>
        /// <param name="strNext">텍스트'다음'</param>
        /// <returns></returns>
        public string MakePageIndex(int intPageNo, int intItemPerPage, int intEntireRecordNo, string strStart, string strEnd, string strPrev, string strNext)
        {
            //이미지 경로
            string strFirstImg = "/Common/Images/Btn/prev2.gif";
            string strEndImg = "/Common/Images/Btn/next2.gif";
            string strPreImg = "/Common/Images/Btn/prev.gif";
            string strNextImg = "/Common/Images/Btn/next.gif";

            // 페이지 계산
            int intPartNo = 1; // 페이지를 끊었을때의 개수
            int intPagePerPart = 10;
            int intEntirePageNo = 1; // 전체 페이지 개수
            int intEntirePartNo = 1; // 전체 파트 개수

            StringBuilder sbPage = new StringBuilder();

            string strResult = null;

            if ((intEntireRecordNo % intItemPerPage) == 0)
            { // 전체 페이지 개수
                intEntirePageNo = intEntireRecordNo / intItemPerPage;
                intEntirePageNo = intEntirePageNo == 0 ? 1 : intEntirePageNo;
            }
            else
            {
                intEntirePageNo = (intEntireRecordNo / intItemPerPage) + 1;
            }
            if ((intEntirePageNo % intPagePerPart) == 0)
            { // 전체 파트갯수
                intEntirePartNo = intEntirePageNo / intPagePerPart;
            }
            else
            {
                intEntirePartNo = (intEntirePageNo / intPagePerPart) + 1;
            }
            intPartNo = ((intPageNo - 1) / intPagePerPart) + 1;
            sbPage.Append("<div class='pagination'>");

            if (intPageNo == 1)
            {
                sbPage.Append(" <img src='" + strFirstImg + "' alt='" + strStart + "'/>&nbsp;" + "<img src='" + strPreImg + "' alt='" + strPrev + "'/>\n");
            }
            else
            {
                sbPage.Append("<a href='javascript:fnMovePage(1)'><img src='" + strFirstImg + "' alt='" + strStart + "'/></a>\n");
                if (intPartNo == 1)
                {
                    sbPage.Append("<img src='" + strPreImg + "' alt='" + strPrev + "'/>\n");
                }
                else
                {
                    sbPage.Append("<a href='javascript:fnMovePage(" + ((intPartNo - 1) * intPagePerPart) + ")'><img src='" + strPreImg + "' alt='" + strPrev + "'/></a>\n");
                }
            }
            sbPage.Append("<span>");

            if (intPartNo == intEntirePartNo)
            { // 마지막 파트이다. 페이지가 intItemPerPage개가 안될수가  있다.

                for (int i = ((intPartNo - 1) * intPagePerPart + 1); i < (intPartNo * intPagePerPart) + 9; i++)
                {

                    if (i <= intEntirePageNo)
                    {// 존재하는 페이지

                        if (i == intPageNo)
                        { // 현재페이지 표시
                            sbPage.Append("<strong>" + i + "</strong>\n");
                        }
                        else
                        {
                            sbPage.Append("<a href='javascript:fnMovePage(" + i + ")'>" + i + "</a>\n");
                        }

                    }
                    else
                    { // 존재하지 않는 페이지
                        //sbPage.Append("<font color=\'#aaaaaa\'>" + i + "</font>\n");
                    }

                }

            }
            else
            { // 마지막 파트가 아니면 무조거 intItemPerPage개 다 찍으면 된다.
                for (int i = ((intPartNo - 1) * intPagePerPart + 1); i < (intPartNo * intPagePerPart) + 1; i++)
                {
                    if (i == intPageNo)
                    { // 현재페이지 표시
                        sbPage.Append("<strong>" + i + "</strong>\n");
                    }
                    else
                    {
                        sbPage.Append("<a href='javascript:fnMovePage(" + i + ")'>" + i + "</a>\n");
                    }
                }
            }

            sbPage.Append("</span>");

            if (intPageNo == intEntirePageNo)
            {
                sbPage.Append("<img src='" + strNextImg + "' alt='" + strNext + "'/>&nbsp;" + "<img src='" + strEndImg + "' alt='" + strEnd + "'/>\n");
            }
            else
            {
                if (intPartNo == intEntirePartNo)
                {
                    sbPage.Append("<img src=" + strNextImg + " alt='" + strNext + "'/>\n");
                }
                else
                {
                    sbPage.Append("<a href='javascript:fnMovePage(" + (intPartNo * intPagePerPart + 1) + ")'><img src='" + strNextImg + "' alt='" + strNext + "'/></a>\n");
                }
                sbPage.Append("<a href='javascript:fnMovePage(" + intEntirePageNo + ")'><img src='" + strEndImg + "' alt='" + strEnd + "'/></a>\n");
            }

            sbPage.Append("</div>");
            strResult = sbPage.ToString();
            return strResult;
        }
        #endregion
    }
}
