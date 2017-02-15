using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace KN.Web.Resident.Contract
{
    public partial class ContractPDFView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //getdata();
            Page.DataBind();
        }


        protected string getURL()
        {
            string conLogconnString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(conLogconnString);
            string strCmd = string.Format("select * from ContractPDF where rentCD ='{0}' and rentSeq ='{1}'", Request["RentCd"], Request["RentSeq"]);
            SqlDataAdapter adap = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(strCmd, conn);
            adap.SelectCommand = cmd;
            DataTable dtbPDF = new DataTable();
            adap.Fill(dtbPDF);
            string FilePath = string.Empty;
            if (dtbPDF.Rows.Count > 0)
            {
                FilePath = "../.."+dtbPDF.Rows[0][4].ToString().Replace("//", "/");
            }
            return FilePath;
        }

        protected string hello()
        {
            return "hellooo";
        }
        


    }

}