using Print.Moe.Net.App_Code.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Print.Moe.Net.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (this.TempData["error"]!=null)
            {
                ViewBag.loginError = 1;
            }
            return View();
        }
        public ActionResult login()
        {
            string username = Request["username"];
            string passwd = Request["passwd"];
            if (UserService.login(username, passwd))
            {
                HttpCookie cookie = new HttpCookie("user");
                cookie.Value = username;
                Response.Cookies.Add(cookie);
            }
            else
            {
                this.TempData["error"] = 0;
                return RedirectToAction("Index");
            }
            return Redirect("/User");
        }
    }
}