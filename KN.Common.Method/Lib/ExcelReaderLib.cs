using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using Excel;

namespace KN.Common.Method.Lib
{
    public class ExcelReaderLib : ExcelHelperLib
    {
        private bool isReadAllAsText = false;

        public ExcelReaderLib()
        {
        }

        public ExcelReaderLib(int intStartRow, int intStartCol) : base(intStartCol, intStartRow)
        {
        }

        public bool ReadAllAsText
        {
            get 
            {
                return isReadAllAsText; 
            }

            set 
            {
                isReadAllAsText = value; 
            }
        }

        private void ValidatePropertiesForExtract()
        {

            if (this.StartRow <= 0)
            {
                throw new Exception("The StartRow Property is Invalid. Must be Greater or Equal Than 1.");
            }

            if (this.StartColumn <= 0)
            {
                throw new Exception("The StartColumn Property is Invalid. Must be Greater or Equal Than 1.");
            }
        }

        public DataTable ExtractDataTable(string strFilePath)
        {
            return ExtractDataTable(strFilePath, StartRow, StartColumn);
        }

        public DataTable ExtractDataTable(string strFilePath, int intRow, int intCol)
        {
            ValidatePropertiesForExtract();

            OleDbConnection connExcel;

            connExcel = new OleDbConnection(CreateConnectionString(strFilePath));
            connExcel.Open();

            DataTable dtReturn = new DataTable();

            string strSheetName = GetFirstSheet(connExcel);

            string strSheet = strSheetName + (strSheetName.Replace("'","").EndsWith("$") ? "" : "$");
            string strCommand = string.Format("SELECT * FROM [{0}]", strSheet);

            OleDbCommand odcComm = new OleDbCommand(strCommand, connExcel);
            OleDbDataAdapter oddAdapter = new OleDbDataAdapter(odcComm);

            oddAdapter.Fill(dtReturn);

            connExcel.Close();

            return dtReturn;
        }

        protected override string ExtraProps()
        {
            if (isReadAllAsText)
            {
                return " IMEX=1;";
            }

            return string.Empty;
        }

        //Read Excel By BaoTV
        public DataSet ExtractDataTable2010(String filePath)
        {          
            var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            //4. DataSet - Create column names from first row
            excelReader.IsFirstRowAsColumnNames = true;
            var result = excelReader.AsDataSet();

            //5. Data Reader methods
            while (excelReader.Read())
            {
               // var  A = excelReader.GetInt32(0);
            }

            //6. Free resources (IExcelDataReader is IDisposable)
            stream.Close();
            excelReader.Close();

            //stream.Dispose();
            return result;
        }
    }
}