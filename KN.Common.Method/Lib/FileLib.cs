using System;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;

using KN.Common.Base;
using KN.Common.Method.Log;

namespace KN.Common.Method.Lib
{
    public class FileLib
    {
        //파일 업로드 경로 설정
        public static readonly string strAppFileUpload = ConfigurationSettings.AppSettings["UploadServerFolder"].ToString();

        #region FileMove : 기존경로에서 새로운 경로로 화일을 이동시킴
        /***********************************************************************************************
         * Function 명 : FileMove
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : 기존경로에서 새로운 경로로 화일을 이동시킴
         * Input    값 : FileMove(기존 화일경로, 옮겨질 화일경로)
         * Ouput    값 : void
         ***********************************************************************************************/
        /// <summary>
        /// FileMove : 기존경로에서 새로운 경로로 화일을 이동시킴
        /// </summary>
        /// <param name="strOriginalPath">기존 화일경로</param>
        /// <param name="strMovedPath">옮겨질 화일경로</param>
        public static void FileMove(string strOriginalPath, string strMovedNewPath)
        {
            try
            {
                FileInfo fInfo = new FileInfo(strOriginalPath);

                fInfo.MoveTo(strMovedNewPath);

                if (File.Exists(strOriginalPath))
                {
                    File.Delete(strOriginalPath);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        #endregion

        #region FileDelete : 화일삭제
        /***********************************************************************************************
         * Function 명 : FileDelete
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : 화일삭제
         * Input    값 : FileDelete(화일경로)
         * Ouput    값 : void
         ***********************************************************************************************/
        /// <summary>
        /// FileDelete : 화일삭제
        /// </summary>
        /// <param name="strOriginalPath">화일경로</param>
        public static void FileDelete(string strOriginalPath)
        {
            try
            {
                if (File.Exists(strOriginalPath))
                {
                    File.Delete(strOriginalPath);
                }
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }
        #endregion

        #region MakeFileName : Unique한 파일명 생성
        /***********************************************************************************************
         * Function 명 : MakeFileName
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : 기존파일명을 변경해줄 경우 사용, 두번째 인자는 화일 Full경로 혹은 화일명 둘다 가능함.
         * Input    값 : MakeFileName( 기업의 경우 기업번호 / 사원의 경우 사원번호 ( 없으면 "" ), 화일이름을 포함한 Full 경로 또는 화일 이름)
         * Ouput    값 : 새로운 화일명 String
         ***********************************************************************************************/
        /// <summary>
        /// MakeFileName : Unique한 파일명 생성
        /// </summary>
        /// <param name="strForeName">기업의 경우 기업번호 / 사원의 경우 사원번호 ( 없으면 "" )</param>
        /// <param name="strFileName">화일이름을 포함한 Full 경로 또는 화일 이름</param>
        /// <returns></returns>
        public static string MakeFileName(string strForeNm, string strFileNm)
        {
            string strReturnNm = "";

            if (string.IsNullOrEmpty(strFileNm))
            {
                strReturnNm = "temp.xxx";
            }
            else
            {
                if (!string.IsNullOrEmpty(strForeNm))
                {
                    strReturnNm = strReturnNm + strForeNm;
                }

                if (!string.IsNullOrEmpty(strFileNm))
                {
                    string strOnlyFileName = System.IO.Path.GetFileName(strFileNm);
                    strReturnNm = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(strOnlyFileName);
                }
            }

            return strReturnNm;
        }
        #endregion

        #region UploadImageFiles : 이미지 업로드
        /***********************************************************************************************
         * Function 명 : UploadImageFiles
         * 개   발  자 : 양영석
         * 생   성  일 : 2010-07-13
         * 용       도 : 이미지 업로드
         * Input    값 : UploadImageFiles(FileUpload 컨트롤, 화일생성시 반드시 붙여야할 접두어(빈칸시 타임스탬프로 변경), DB에 저장되는 경로)
         * Ouput    값 : 새로운 화일명 String
         ***********************************************************************************************/
        /// <summary>
        /// UploadImageFiles : 이미지 업로드
        /// </summary>
        /// <param name="fuploadImage">FileUpload 컨트롤</param>
        /// <param name="strTypeNm">화일생성시 반드시 붙여야할 접두어(빈칸시 타임스탬프로 변경)</param>
        /// <param name="strDir">DB에 저장되는 서브경로(단어만 가능)</param>
        /// <returns>
        /// object[0] : Full Path
        /// object[1] : DB에 저장되는 Path
        /// </returns>
        public static object[] UploadImageFiles(FileUpload fuploadImage, string strTypeNm, string strDir)
        {
            object[] objArrReturn = new object[2];

            string strFileNm = string.Empty;
            string strFullNm = string.Empty;
            string strPreNm = string.Empty;
            string strSaveNm = string.Empty;
            string strDefaultPath = string.Empty;

            strDefaultPath = strAppFileUpload;
            // 화일 업로드창에 올라간 화일이 있는지 체크하는 부분
            if (fuploadImage.HasFile)
            {
                // 독립된 이미지명 생성
                strFileNm = MakeFileName(strTypeNm, fuploadImage.FileName);

                DirectoryInfo dir = new DirectoryInfo(strDefaultPath);

                // 기본 Directory 생성
                if (!dir.Exists)
                {
                    dir.Create();
                }

                // 서브 디렉토리 생성
                if (!string.IsNullOrEmpty(strDir))
                {
                    dir.CreateSubdirectory(strDir);
                }

                strPreNm = strDefaultPath.Replace("\\", "/");


                if (!String.IsNullOrEmpty(strFileNm))
                {
                    string strTempDir = "";
                    string strPartDir = "";

                    strTempDir = strDir.Replace("\\", "/");

                    // 맨 앞에 "/"가 있을 경우 처리하는 부분
                    strPartDir = strTempDir.Substring(0, 1);

                    if (strPartDir == "/")
                    {
                        strTempDir = strTempDir.Substring(1);
                    }

                    // 맨 뒤에 "/"가 있을 경우 처리하는 부분
                    strPartDir = strTempDir.Substring(strTempDir.Length - 1, 1);

                    if (strPartDir == "/")
                    {
                        strTempDir = strTempDir.Substring(0, strTempDir.Length - 1);
                    }

                    strPartDir = strPreNm.Substring(strPreNm.Length - 1, 1);

                    if (strPartDir == "/")
                    {
                        strPreNm = strPreNm.Substring(0, strPreNm.Length - 1);
                    }

                    // 화일 Full경로 생성
                    strFullNm = strAppFileUpload + strTempDir + "/" + strFileNm;
                    strSaveNm = strTempDir + "/" + strFileNm;
                    
                    FileInfo fileInfo = new FileInfo(strFullNm);

                    string strNewFileNm = "";

                    // 기존이미지가 존재할 경우
                    if (fileInfo.Exists)
                    {
                        int intFileIndex = 0;

                        // 확장자 분리
                        string strFileExtension = fileInfo.Extension;

                        // 확장자를 제외한 화일명 추출
                        string strRealNm = strFileNm.Replace(strFileExtension, "");

                        // 다른 화일명이 생성될때까지 Loop
                        do
                        {
                            intFileIndex++;
                            strNewFileNm = strRealNm + intFileIndex.ToString() + strFileExtension;
                            fileInfo = new FileInfo(strAppFileUpload + strDir + "/" + strNewFileNm);
                        }
                        while (fileInfo.Exists);

                        // 화일 Full경로 생성
                        strFullNm = strDir + strNewFileNm;

                        // 화일 업로드
                        fuploadImage.PostedFile.SaveAs(strFullNm);
                    }
                    else
                    {
                        fuploadImage.PostedFile.SaveAs(strFullNm);
                    }

                    objArrReturn[0] = strFullNm;    // Full Path 정보
                    objArrReturn[1] = strSaveNm;    // DB에 저장될 Path 정보
                }
                else
                {
                    // 화일 업로드
                    objArrReturn = null;
                }

                return objArrReturn;
            }
            else
            {
                objArrReturn = null;
                return objArrReturn;
            }
        }
        #endregion
    }
}
