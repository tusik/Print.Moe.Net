using Print.Moe.Net.App_Code;
using Print.Moe.Net.App_Code.Service;
using Print.Moe.Net.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Print.Moe.Net.Controllers
{
    public class ImgController : Controller
    {
        // GET: Img
        public ActionResult Index()
        {
            return View();
        }
        public void Preview()
        {
            int id =Convert.ToInt32( Request["id"]);
            string ext = @"."+Request["ext"];
            int height = Convert.ToInt32(Request["height"]);
            string pathh = "C:\\Onedrive\\OneDrive - ITBYCX\\pmcdn\\" + Convert.ToInt32(id) / 1000 + "\\" + id + getExt(ext);
            System.Drawing.Image img = System.Drawing.Image.FromFile(pathh);
            int width = img.Width;
            int newHeight = 600;
            int newWidth = Convert.ToInt32( newHeight*1.0 / height * width);
            Bitmap b = new Bitmap(img);
            System.Drawing.Image i = ImgTool.resizeImage(b, new Size(newHeight, newWidth));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            i.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Response.ClearContent();
            Response.BinaryWrite(ms.ToArray());
            DataTool.addHis("json", IPAddress(), id.ToString());
            if (ext.Equals(".png"))
            { 
                Response.ContentType = "image/png";
            }
            else
            {
                Response.ContentType = "image/jpeg";
            }
        }
        public JsonResult SelectImgJson()
        {
            
            String id = Request["id"];
            List<ImageModel> list = new List<ImageModel>();
            if (!(id == null || id.Equals("")))
            {
                list.Add(ImgService.selectImgById(Convert.ToInt64(id)));
            }

            else
            {
                DefaultConfig config= setConfig();
                if (Request["limit"] != null)
                {
                    config.limit = Convert.ToInt32(Request["limit"]);
                }
                if (config.order == StaticVal.Order.ORDER_BY_RANDOM)
                {
                    
                    list.AddRange(ImgService.random(config));
                }
                else
                {
                    list.Add(ImgService.selectImg(config));
                }
                
            }
            DataTool.addHis("json", IPAddress(), list);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public DefaultConfig setConfig()
        {
            DefaultConfig config = new DefaultConfig();
            String order = Request["order"];
            String days = Request["days"];
            String w_max = Request["w_max"];
            String w_min = Request["w_min"];
            String h_max = Request["h_max"];
            String h_min = Request["h_min"];
            config.tags.Add(Request["tag"]);
            config.author = Request["author"];
            String limit = Request["limit"];
            String desc = Request["desc"];
            String size = Request["size"];
            string username = Request["username"];
            if (!(username == null || username.Equals(""))) { config.username = username; }
            if (!(size == null || size.Equals("")))
                {
                    config.fileSize = Convert.ToInt64(size);
                }
                if (!(order == null || order.Equals("")))
                {
                    config.order = Convert.ToInt32(order);
                }
                if (!(days == null || days.Equals("")))
                {
                    config.days = Convert.ToInt32(days);
                    config.eTime = DateUtil.zeroTime(config.days);
                }
                if (!(h_max == null || h_max.Equals("")))
                {
                    config.h_max = Convert.ToInt32(h_max);
                }
                if (!(h_min == null || h_min.Equals("")))
                {
                    config.h_min = Convert.ToInt32(h_min);
                }
                if (!(w_max == null || w_max.Equals("")))
                {
                    config.w_max = Convert.ToInt32(w_max);
                }
                if (!(w_min == null || w_min.Equals("")))
                {
                    config.w_min = Convert.ToInt32(w_min);
                }
                if (!(limit == null || limit.Equals("")))
                {
                    config.limit = Convert.ToInt32(limit);
                }
                if (!(desc == null || desc.Equals("")))
                {
                    config.sort = Convert.ToInt32(desc);
                }
            return config;
        }
        public void SelectImgPng( )
        {
            
            DefaultConfig config = new DefaultConfig();
            String id = Request["id"];
            
            List<ImageModel> list = new List<ImageModel>();
            if (!(id == null || id.Equals("")))
            {
                list.Add(ImgService.selectImgById(Convert.ToInt64(id)));
            }
            else
            {
                config = setConfig();
                if (config.order == StaticVal.Order.ORDER_BY_RANDOM)
                {
                    list.AddRange(ImgService.random(config));
                }
                else
                {
                    list.Add(ImgService.selectImg(config));
                }

            }
            DataTool.addHis("png", IPAddress(), list[0].Kid);
            string pathh = "C:\\Onedrive\\OneDrive - ITBYCX\\pmcdn\\" + Convert.ToInt32(list[0].Kid) / 1000 + "\\" + list[0].Kid + getExt(list[0].File_url);
            FileStream fs = new FileStream(pathh, FileMode.Open);//可以是其他重载方法   
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();

            if (getExt(list[0].File_url).Equals(".png"))
            {

                Response.ContentType = "image/png";
                Response.BinaryWrite(byData);  
            }
            else
            {

                Response.ContentType = "image/jpeg";
                Response.BinaryWrite(byData);
            }
            
        }
        public string getExt(string url)
        {
            string[] tmp = url.Split('.');
            return @"."+tmp[tmp.Length - 1];
        }
        private static string getJsonByObject(Object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string myJson = jss.Serialize(obj);
            return myJson;
        }
        public string IPAddress()
        {
                string userIP;
                // HttpRequest Request = HttpContext.Current.Request; 
                 // ForumContext.Current.Context.Request; 
                                                                   // 如果使用代理，获取真实IP 
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != "")
                    userIP = Request.ServerVariables["REMOTE_ADDR"];
                else
                    userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (userIP == null || userIP == "")
                    userIP = Request.UserHostAddress;
                return userIP;
            
        }
        public string getTags()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string rs = DataTool.getTags();
            string[] temp = rs.Split(';');
            List<Tag> list = new List<Tag>();
            List<Tag> nlist = new List<Tag>();
            foreach (string t in temp)
            {
                if (t.Length > 1)
                {
                    string[] tmp = t.Split(',');
                    Tag tag = new Tag();
                    tag.name = tmp[0];
                    tag.times = Convert.ToInt32(tmp[1]);
                    list.Add(tag);
                }
                
            }
            Random random = new Random();
            for(int i = 0; i < 4; i++)
            {
                nlist.Add(list[random.Next(list.Count)]);
            }
            return js.Serialize(nlist);
        }
    }
}