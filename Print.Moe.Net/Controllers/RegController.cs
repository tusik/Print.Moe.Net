using Print.Moe.Net.App_Code.Service;
using Print.Moe.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Print.Moe.Net.Controllers
{
    public class RegController : Controller
    {
        // GET: Reg
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult reg()
        {
            UserModel user = new UserModel();
            string username = Request["username"];
            string passwd = Request["passwd"];
            string repasswd = Request["repasswd"];
            user.Username = username;
            user.Passwd = passwd;
            user.Repasswd = repasswd;
            if (regFunc(user))
            {
                return Redirect("/User");
            }
            else
            {
                return Redirect("/Reg");
            }
            
        }
        public bool regFunc(UserModel user)
        {
            
            
            if (user.Repasswd.Equals(user.Passwd))
            {
                if (!UserService.usernameExist(user))
                {
                    if (UserService.reg(user))
                    {
                        HttpCookie cookie = new HttpCookie("user");
                        cookie.Value = user.Username;
                        cookie.Expires = DateTime.MaxValue;
                        Response.Cookies.Add(cookie);
                        return true;
                    }
                }
            }
            return false;
        }
    }
}