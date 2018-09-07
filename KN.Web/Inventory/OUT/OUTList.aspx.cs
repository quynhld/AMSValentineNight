using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using KN.Common.Base;
using KN.Common.Base.Code;
using KN.Common.Method.Common;
using KN.Common.Method.Lib;
using KN.Common.Method.Log;
using KN.Common.Method.Util;
using System.Data.Sql;
using System.Data.SqlClient;
using KN.Settlement.Biz;
using System.Configuration ;
using KN.Config.Biz;

namespace KN.Web.Inventory
{
    public partial class OUTList : BasePage
    {
        StringBuilder sbPageNavi = new StringBuilder();
        PageNoListUtil pageUtil = new PageNoListUtil();

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        string strSelect = string.Empty;
        SqlDataAdapter adapter = new SqlDataAdapter();
        //conn.ConnectionString = ConfigurationManager.ConnectionStrings["TempDBConnection"].ConnectionString;
        DataSet ds = new DataSet();

        int intPageNo = CommValue.NUMBER_VALUE_0;

        protected void Page_Load(object sender, EventArgs e)
        {
            AuthCheckLib.CheckSession();
            try
            {
                if (!IsPostBack)
                {
                    CheckParams();
                    InitControls();
                    LoadData();
                }
                
            }
            catch (Exception ex)
            {
                ErrLogger.MakeLogger(ex);
            }
        }

        protected void InitControls()
        {
        }
       
        private void LoadData()
        {
            cmd.CommandText = stringSelectPaging(CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(hfCurrentPage.Value), hfStartDt.Value.Replace("-", ""), hfEndDt.Value.Replace("-", ""));
            cmd.Connection = conn;

            adapter.SelectCommand = cmd;
            try
            {
                if(conn.State != ConnectionState.Open )
                {
                    conn.Open();
                }
                adapter.Fill(ds);
                lvLstOUT.DataSource = ds.Tables[1];
                lvLstOUT.DataBind();
                if (ds.Tables[1].Rows.Count > 0)
                {
                    sbPageNavi.Append(pageUtil.MakePageIndex(Int32.Parse(hfCurrentPage.Value), CommValue.BOARD_VALUE_PAGESIZE, Int32.Parse(ds.Tables[0].Rows[0]["TotalCnt"].ToString())
                        , TextNm["FIRST"], TextNm["END"], TextNm["PREV"], TextNm["NEXT"]));
                    spanPageNavi.InnerHtml = sbPageNavi.ToString();
                }
            }
            catch(Exception ex)
            {
                conn.Close();
            }
        }

        private void CheckParams()
        {
            if (!string.IsNullOrEmpty(hfCurrentPage.Value))
            {
                intPageNo = Int32.Parse(hfCurrentPage.Value);
                hfCurrentPage.Value = intPageNo.ToString();
            }
            else
            {
                intPageNo = CommValue.BOARD_VALUE_DEFAULTPAGE;
                hfCurrentPage.Value = intPageNo.ToString();
            }
        }

        protected void imgbtnPageMove_Click(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }

        private string stringSelectPaging(int pageSize, int pageNow, string startDate,string endDate)
        {
            string str = @"
	                        DECLARE
	                         @intPageSize		INT
	                        ,@intNowPage		INT
	                        ,@strStartDt		VARCHAR(8)
	                        ,@strEndDt			VARCHAR(8)

	                        SET @intPageSize	= {0} -- pagesize
	                        SET @intNowPage		= {1} -- pagenow
	                        SET @strStartDt		= '{2}' -- startdate
	                        SET @strEndDt		= '{3}' -- endate

	                        SELECT  COUNT(*) AS TotalCnt
		                        FROM  dbo.Inventory_OUT AS A
		                        WHERE  1 = 1
		                        AND  CONVERT(VARCHAR(8), A.CreateDate, 112)	>= CASE WHEN ISNULL(@strStartDt, '') = '' THEN CONVERT(VARCHAR(8), A.CreateDate, 112)
		                                                                        ELSE '' + @strStartDt + '' END
		                        AND  CONVERT(VARCHAR(8), A.CreateDate, 112)	<= CASE WHEN ISNULL(@strEndDt, '') = '' THEN CONVERT(VARCHAR(8), A.CreateDate, 112)
		                                                                        ELSE '' + @strEndDt + '' END		 		
	                        ;WITH LogList
	                        AS 
	                        (
	                        SELECT  ROW_NUMBER() OVER (ORDER BY A.[CreateDate] DESC) AS RealSeq
			                        ,A.[INV_OUT_ID]
		                            ,A.[CreateDate]
                                    ,A.CreateUser
		                            ,A.[ModifyDate]
		                            ,A.[ModifyUser]
		                            ,A.[UsedFor]
		                            ,A.[Note]
		                            ,A.[Status]
		                        FROM  dbo.Inventory_OUT AS A
		                        WHERE  1 = 1
		                        AND  CONVERT(VARCHAR(8), A.CreateDate, 112)	>= CASE WHEN ISNULL(@strStartDt, '') = '' THEN CONVERT(VARCHAR(8), A.CreateDate, 112)
		                                                                        ELSE '' + @strStartDt + '' END
		                        AND  CONVERT(VARCHAR(8), A.CreateDate, 112)	<= CASE WHEN ISNULL(@strEndDt, '') = '' THEN CONVERT(VARCHAR(8), A.CreateDate, 112)
		                                                                        ELSE '' + @strEndDt + '' END
	                        )
	                        SELECT  X.RealSeq
		                            ,X.INV_OUT_ID
                                    ,X.[CreateDate]
                                    ,X.CreateUser
		                            ,X.[ModifyDate]
		                            ,X.[ModifyUser]
		                            ,X.[UsedFor]
		                            ,X.[Note]
		                            ,X.[Status]
		                        FROM  LogList AS X
		                        WHERE  1 = 1
		                        AND  X.RealSeq <= @intNowPage * @intPageSize
		                        AND  X.RealSeq >= (@intNowPage-1) * @intPageSize + 1";
            return string.Format( str,pageSize,pageNow,startDate,endDate);
        }

        protected void lnkbtnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}