using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace KN.Common.Method.Lib
{
    public abstract class ExcelHelperLib
    {
        private string strSheetNm = string.Empty;

        //private static string strConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source={0};Extended Properties=\"Excel 8.0;{1}\"";
        private static string strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='{2};{1};'";
        private static string secondConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;{1}";
        private static string thirdConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;{1}";
        private int intStartRow = 1;
        private int intStartCol = 1;

        private bool isHasHeaders = true;

        protected ExcelHelperLib()
        {
        }

        protected ExcelHelperLib(int intParamRow, int intParamCol)
        {
            intStartCol = intParamCol;
            intStartRow = intParamRow;
        }

        protected string CreateConnectionString(string file)
        {
            FileInfo fInf = new FileInfo(file);
            string fileExtension = string.Empty;
            
            string strExtProps = string.Empty;
            switch( fInf.Extension)
            {
                case ".xls":
                    fileExtension = "Excel 8.0";
                    break;
                case ".xlsx":
                    fileExtension = "Excel 12.0 Xml";
                    break;
            }
            strExtProps += ExtraProps();

            if (this.HasHeaders)
            {
                strExtProps += " HDR=YES;";
            }
            else
            {
                strExtProps += " HDR=NO;";
            }

            return string.Format(strConnectionString, file, strExtProps,fileExtension);
        }

        protected string CreateConnectionString(string file,string extension)
        {
            string strExtProps = string.Empty;

            strExtProps += ExtraProps();

            if (this.HasHeaders)
            {
                strExtProps += " HDR=YES;";
            }
            else
            {
                strExtProps += " HDR=NO;";
            }

            return string.Format(strConnectionString, file, strExtProps);
        }

        protected virtual string ExtraProps()
        {
            return string.Empty;
        }

        public int StartRow
        {
            get 
            {
                return intStartRow; 
            }

            set
            {
                intStartRow = value; 
            }
        }

        public int StartColumn
        {
            get 
            {
                return intStartCol; 
            }

            set 
            {
                intStartCol = value; 
            }
        }

        public bool HasHeaders
        {
            get
            {
                return isHasHeaders; 
            }

            set
            {
                isHasHeaders = value; 
            }
        }

        public string SheetName
        {
            get 
            {
                return strSheetNm; 
            }

            set 
            {
                strSheetNm = value;
            }
        }

        internal static string GetFirstSheet(OleDbConnection conn)
        {
            DataTable dtReturn = conn.GetSchema("Tables");

            if (dtReturn != null && dtReturn.Rows.Count > 0)
            {
                return dtReturn.Rows[0]["TABLE_NAME"].ToString();
            }
            else
            {
                throw new Exception("A Valid sheet was not found in the workbook.");
            }
        }

    }
}
