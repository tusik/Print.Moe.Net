using Print.Moe.Net.App_Code;
using Print.Moe.Net.App_Code.Service;
using Print.Moe.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
namespace Print.Moe.Net.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            if (Request.Cookies["user"] == null)
            {
                return Redirect("/Login");
            }
            ViewBag.pageName = "dashboard";
            ViewBag.totalSize = DataTool.getFileTotalSize();
            ViewBag.newCount = DataTool.getNewImageCount(1);
            ViewBag.total = DataTool.getSum();
            ViewBag.count = DataTool.getCount(1);
            return View();
        }
        public ActionResult Select(FormCollection collection)
        {
            if (Request.Cookies["user"] == null)
            {
                return Redirect("/Login");
            }
            int page = 1;
            if (Request["page"] != null)
            {
                page=Convert.ToInt32(Request["page"].ToString());
            }
            
            DefaultConfig config = new DefaultConfig();
            config.username = Request.Cookies["user"].Value.ToString();
            config.limit = 9;
            string[] AllTags = { };
            if (collection["tags"] != null||Request["tags"]!=null)
            {
                if (collection["tags"] != null)
                {
                    AllTags = collection["tags"].Split(',');
                }
                else
                {
                    AllTags =Request["tags"].Split(',');
                }
                 
                if (AllTags.Length != 0)
                {

                    foreach (string tmp in AllTags)
                    {
                        config.tags.Add(tmp);

                    }

                }
            }
            else
            {

                
            }
            if (collection["height"] != null&& collection["height"] !="")
            {
                string h = collection["height"];
                config.h_max = Convert.ToInt32(collection["height"]);

            }
            if (collection["width"] != null && collection["width"] != "")
            {
                config.w_max = Convert.ToInt32(collection["width"]);

            }
            if (collection["author"] != null && collection["author"] != "")
            {
                config.author = collection["author"];
            }
            if (collection["order"] != null && collection["order"] != "")
            {
                if (collection["order"] == "height")
                {
                    config.order = StaticVal.Order.ORDER_BY_HEIGHT;
                }else if (collection["order"] == "width")
                {
                    config.order = StaticVal.Order.ORDER_BY_WIDTH;
                }
                else if (collection["order"] == "width")
                {
                    config.order = StaticVal.Order.ORDER_BY_WIDTH;
                }
                else if (collection["order"] == "time")
                {
                    config.order = StaticVal.Order.ORDER_BY_CREATETIME;
                }
                else if (collection["order"] == "likes")
                {
                    config.order = StaticVal.Order.ORDER_BY_LIKES;
                }
            }
            if (collection["desc"] != null && collection["desc"] != "")
            {
                if (collection["desc"]=="1")
                {
                    config.sort = StaticVal.Sort.desc;
                }
                else
                {
                    config.sort = StaticVal.Sort.asc;
                }
                
            }
            if (collection["ext"] != null && collection["ext"] != "")
            {
                config.ext = collection["ext"];
            }
            List<string> t = new List<string>();
            ViewBag.url = getUrl(config);
            string tagStr = "";
            List<SelectListItem> tags = new List<SelectListItem>();
            Dictionary<string,int> taglist= DataTool.getTags(2000);
            foreach (var item in taglist)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item.Key.ToString(), Value = item.Key.ToString() };
                foreach (string i in config.tags)
                {
                    if (i == item.Key)
                    {
                        selectListItem.Selected = true;
                        if (tagStr.Length > 0)
                        {
                            tagStr += ",";
                        }
                        tagStr += i;
                    }
                }
                tags.Add(selectListItem);
            }
            ViewBag.height = config.h_max;
            ViewBag.width = config.w_max;
            ViewBag.taglist = tags;
            ViewBag.tagStr = tagStr;
            ViewBag.pageName = "map";
            int number = ImgService.selectImgListCount(config);
            int pageNum =Convert.ToInt32(Math.Ceiling( number*1.0 / 9.0).ToString());
            ViewBag.pageNum = pageNum;
            ViewBag.page = page;
            var imgs = ImgService.selectImgList(config,9*(page-1),9, Request.Cookies["user"].Value.ToString());
            List<SelectListItem> exts= new List<SelectListItem> { new SelectListItem { Text = "png", Value = "png" }, new SelectListItem { Text = "jpg", Value = "jpg" } };
            List<SelectListItem> desc = new List<SelectListItem> { new SelectListItem { Text = "true", Value = "1" }, new SelectListItem { Text = "false", Value = "0" } };
            List<SelectListItem> order = new List<SelectListItem> {
                new SelectListItem { Text = "time", Value = "time" },
                new SelectListItem { Text = "height", Value = "height" },
                new SelectListItem { Text = "width", Value = "width" },
                new SelectListItem { Text = "likes", Value = "likes" }};
            if (config.ext == "png")
            {
                exts[0].Selected = true;
            }else if(config.ext == "jpg")
            {
                exts[1].Selected = true;
            }
            if (!config.author.Equals(StaticVal.DEFAULT_AUTHOR))
            {
                ViewBag.author = config.author;
            }
            else
            {
                ViewBag.author = "";
            }
            switch (config.sort)
            {
                case StaticVal.Sort.desc: desc[0].Selected = true; break;
                case StaticVal.Sort.asc: desc[1].Selected = true; break;
                default: desc[0].Selected = true; break;
            }
            switch (config.order)
            {
                case StaticVal.Order.ORDER_BY_CREATETIME:order[0].Selected = true;break;
                case StaticVal.Order.ORDER_BY_HEIGHT:order[1].Selected = true;break;
                case StaticVal.Order.ORDER_BY_WIDTH:order[2].Selected = true;break;
                case StaticVal.Order.ORDER_BY_LIKES:order[3].Selected = true;break;
                default:order[0].Selected = true;break;
            }
            ViewBag.order = order;
            ViewBag.desc = desc;
            ViewBag.exts = exts;
            ViewBag.imgs= imgs;
            
            return View();
        }
        public ActionResult FavorImg(FormCollection collection)
        {
            if (Request.Cookies["user"] == null)
            {
                return Redirect("/Login");
            }
            
            int page = 1;
            if (Request["page"] != null)
            {
                page = Convert.ToInt32(Request["page"].ToString());
            }

            DefaultConfig config = new DefaultConfig();
            config.username = Request.Cookies["user"].Value;
            config.limit = 9;
            string[] AllTags = { };
            if (collection["tags"] != null || Request["tags"] != null)
            {
                if (collection["tags"] != null)
                {
                    AllTags = collection["tags"].Split(',');
                }
                else
                {
                    AllTags = Request["tags"].Split(',');
                }

                if (AllTags.Length != 0)
                {

                    foreach (string tmp in AllTags)
                    {
                        config.tags.Add(tmp);

                    }

                }
            }
            else
            {


            }
            if (collection["height"] != null && collection["height"] != "")
            {
                string h = collection["height"];
                config.h_max = Convert.ToInt32(collection["height"]);
            }
            if (collection["width"] != null && collection["width"] != "")
            {
                config.w_max = Convert.ToInt32(collection["width"]);
            }
            if (collection["author"] != null && collection["author"] != "")
            {
                config.author = collection["author"];
            }
            if (collection["order"] != null && collection["order"] != "")
            {
                if (collection["order"] == "height")
                {
                    config.order = StaticVal.Order.ORDER_BY_HEIGHT;
                }
                else if (collection["order"] == "width")
                {
                    config.order = StaticVal.Order.ORDER_BY_WIDTH;
                }
                else if (collection["order"] == "width")
                {
                    config.order = StaticVal.Order.ORDER_BY_WIDTH;
                }
                else if (collection["order"] == "time")
                {
                    config.order = StaticVal.Order.ORDER_BY_CREATETIME;
                }
                else if (collection["order"] == "likes")
                {
                    config.order = StaticVal.Order.ORDER_BY_LIKES;
                }
            }
            if (collection["desc"] != null && collection["desc"] != "")
            {
                if (collection["desc"] == "1")
                {
                    config.sort = StaticVal.Sort.desc;
                }
                else
                {
                    config.sort = StaticVal.Sort.asc;
                }

            }
            if (collection["ext"] != null && collection["ext"] != "")
            {
                config.ext = collection["ext"];
            }
            List<string> t = new List<string>();
            ViewBag.url = getUrl(config);
            string tagStr = "";
            List<SelectListItem> tags = new List<SelectListItem>();
            Dictionary<string, int> taglist = DataTool.getTags(1000);
            foreach (var item in taglist)
            {
                SelectListItem selectListItem = new SelectListItem { Text = item.Key.ToString(), Value = item.Key.ToString() };
                foreach (string i in config.tags)
                {
                    if (i == item.Key)
                    {
                        selectListItem.Selected = true;
                        if (tagStr.Length > 0)
                        {
                            tagStr += ",";
                        }
                        tagStr += i;
                    }
                }
                tags.Add(selectListItem);
            }
            ViewBag.height = config.h_max;
            ViewBag.width = config.w_max;
            ViewBag.taglist = tags;
            ViewBag.tagStr = tagStr;
            ViewBag.pageName = "favorimg";
            DefaultConfig tmpConfig = config.Clone();
            int number = ImgService.selectImgListCountByUser(tmpConfig, Request["user"]);
            int pageNum = Convert.ToInt32(Math.Ceiling(number * 1.0 / 9.0).ToString());
            ViewBag.pageNum = pageNum;
            ViewBag.page = page;
            var imgs = ImgService.selectImgListByUser(config, 9 * (page - 1) , 9, Request.Cookies["user"].Value.ToString());
            List<SelectListItem> exts = new List<SelectListItem> { new SelectListItem { Text = "png", Value = "png" }, new SelectListItem { Text = "jpg", Value = "jpg" } };
            List<SelectListItem> desc = new List<SelectListItem> { new SelectListItem { Text = "true", Value = "1" }, new SelectListItem { Text = "false", Value = "0" } };
            List<SelectListItem> order = new List<SelectListItem> {
                new SelectListItem { Text = "time", Value = "time" },
                new SelectListItem { Text = "height", Value = "height" },
                new SelectListItem { Text = "width", Value = "width" },
                new SelectListItem { Text = "likes", Value = "likes" }};
            if (config.ext == "png")
            {
                exts[0].Selected = true;
            }
            else if (config.ext == "jpg")
            {
                exts[1].Selected = true;
            }
            if (!config.author.Equals(StaticVal.DEFAULT_AUTHOR))
            {
                ViewBag.author = config.author;
            }
            else
            {
                ViewBag.author = "";
            }
            switch (config.sort)
            {
                case StaticVal.Sort.desc: desc[0].Selected = true; break;
                case StaticVal.Sort.asc: desc[1].Selected = true; break;
                default: desc[0].Selected = true; break;
            }
            switch (config.order)
            {
                case StaticVal.Order.ORDER_BY_CREATETIME: order[0].Selected = true; break;
                case StaticVal.Order.ORDER_BY_HEIGHT: order[1].Selected = true; break;
                case StaticVal.Order.ORDER_BY_WIDTH: order[2].Selected = true; break;
                case StaticVal.Order.ORDER_BY_LIKES: order[3].Selected = true; break;
                default: order[0].Selected = true; break;
            }
            ViewBag.order = order;
            ViewBag.desc = desc;
            ViewBag.exts = exts;
            ViewBag.imgs = imgs;
            return View();
        }
        public string getUrl(DefaultConfig config)
        {
            string url = @"http://img.misaka.asia/img/SelectImgPng?";
            if (config.username != "")
            {
                url += "username=" + Request.Cookies["user"].Value+"&";
            }
            if (config.h_max != StaticVal.DEFAULT_MAX_HEIGHT)
            {
                url += "height=" + config.h_max + "&";
            }
            if (config.w_max != StaticVal.DEFAULT_MAX_HEIGHT)
            {
                url += "width=" + config.w_max + "&";
            }
            if (config.ext != "all")
            {
                url += "ext=" + config.ext + "&";
            }
            if (config.author!=StaticVal.DEFAULT_AUTHOR)
            {
                url += "author=" + config.author + "&";
            }
            if (config.tags.Count != 0)
            {
                string tag = "";
                for(int i=0;i< config.tags.Count; i++)
                {
                    tag += config.tags[i];
                    if ((i + 1) != config.tags.Count)
                    {
                        tag += ",";
                    }
                }
                url += "tags=" + tag + "&";
            }
            return url;
        }
        public string favor()
        {
            string uname = Request.Cookies["user"].Value.ToString();
            string kid = Request["kid"];
            int status = UserService.updateFavor(uname, kid);
            ImageModel image = ImgService.selectImgById(Convert.ToInt64(kid));
            JavaScriptSerializer javaScript = new JavaScriptSerializer();
            if (status==1)
            {
                return "1";
            }
            else if(status==-1)
            {
                return "-1";
            }
            else
            {
                return "0";
            }
        }
        public void logout()
        {
            HttpContext.Response.Cookies["user"].Expires = DateTime.Now.AddDays(-1);
            Response.Redirect("/");
        }
    }
}