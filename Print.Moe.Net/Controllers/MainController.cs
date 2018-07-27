using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Print.Moe.Net.Models;
using Print.Moe.Net.App_Code;
using Print.Moe.Net.App_Code.Service;

namespace Print.Moe.Net.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            DefaultConfig con = new DefaultConfig();
            con.fileSize =Convert.ToInt64(1.5 * 1024.0 * 1024.0);
            con.limit = 1;
            ViewBag.sss = con.fileSize;
            ViewBag.img = ImgService.random(con)[0].Kid;
            ViewBag.sum = DataTool.getSum();
            ViewBag.count = DataTool.getCount(1);
            return View();
        }
    }
}