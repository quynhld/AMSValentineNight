using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KN.Common.Method.Lib;
using KN.Common.Method.Util;
using KNNEW.Models;

namespace KNNEW.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            if (Request.Url != null)
            {
                var strPath = Request.Url.PathAndQuery;
                DataSet dsReturn = new DataSet();
                dsReturn = CommMenuUtil.SpreadMenuMng("0002", "00000002", TextLib.StringEncoder("/Main.aspx"), 1);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Menu()
        {
            if (Request.Url != null)
            {
                var strPath = Request.Url.PathAndQuery;
                DataSet dsReturn = new DataSet();
                dsReturn = CommMenuUtil.SpreadMenuMng("0002", "00000002", TextLib.StringEncoder("/Main.aspx"), 1);
            }
            return null;
        }
        public ActionResult TopMenu()
        {
             IEnumerable<TopMenu> TopMenu = null;            
            if (Request.Url != null)
            {
                var strPath = Request.Url.PathAndQuery;
                DataSet dsReturn = new DataSet();
                dsReturn = CommMenuUtil.SpreadMenuMng("0002", "00000002", TextLib.StringEncoder(strPath), 1);
                TopMenu = Convert(dsReturn);
            }
            return PartialView(TopMenu);
        }

        public ActionResult LeftMenu()
        {
            return PartialView("LeftMenu");
        }
        public List<TopMenu> Convert(DataSet ds)
        {
            var lstLibCount = new List<TopMenu>();
            if ((ds != null) && (ds.Tables.Count > 0))
            {
                lstLibCount.AddRange(from DataRow dr in ds.Tables[0].Rows
                                     select new TopMenu()
                                     {
                                         MenuSeq = 1,
                                         MenuName = dr["MenuTxt"].ToString()
                                     });
            }
            return lstLibCount;
        }
    }
}
