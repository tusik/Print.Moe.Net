using Microsoft.VisualStudio.TestTools.UnitTesting;
using Print.Moe.Net.App_Code;
using Print.Moe.Net.App_Code.Service;
using Print.Moe.Net.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Print.Moe.Net.Controllers.Tests
{
    [TestClass()]
    public class UserControllerTests
    {
        [TestMethod()]
        public void SelectTest()
        {
            Dictionary<String, String> collection = new Dictionary<string, string>();
            collection["tag"] = "girl";
            collection["height"] = "1080";
            collection["width"] = "1920";
            collection["author"] = "1920";
            DefaultConfig config = new DefaultConfig();
            config.limit = 9;
            string[] AllTags = { };
            if (collection["tags"] != null )
            {
                if (collection["tags"] != null)
                {
                    AllTags = collection["tags"].Split(',');
                }
                else
                {
                    
                }

                if (AllTags.Length != 0)
                {

                    foreach (string tmp in AllTags)
                    {
                        config.tags.Add(tmp);

                    }

                }
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
           

            var imgs = ImgService.selectImgList(config);
            Assert.IsNotNull(imgs);
        }
    }
}