using Microsoft.VisualStudio.TestTools.UnitTesting;
using Print.Moe.Net.App_Code.Service;
using Print.Moe.Net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Print.Moe.Net.App_Code.Service.Tests
{
    [TestClass()]
    [DeploymentItem(@"f:\img.xml")]
    public class ImgServiceTests
    {
        public TestContext TestContext { get; set; }
        [TestMethod()]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
       @"f:\img.xml",
        "row",
        DataAccessMethod.Sequential)]
        public void selectImgListTest()
        {

            DefaultConfig defaultConfig = new DefaultConfig();
            defaultConfig.author= TestContext.DataRow["author"].ToString();
            defaultConfig.w_max = Convert.ToInt32( TestContext.DataRow["width"].ToString());
            defaultConfig.h_max = Convert.ToInt32(TestContext.DataRow["height"].ToString());
            
            Assert.IsNotNull(selectImgList(defaultConfig));
        }
        public static DataSet selectImgList(DefaultConfig config)
        {
            string sql = "SELECT * FROM images WHERE 1=1";
            if (!(config.author == null || config.author.Length == 0))
                sql += string.Format(" and author LIKE CONCAT(CONCAT('%', '{0}'),'%')", config.author);

            for (int i = 0; i < config.tags.Count; i++)
            {
                if (i == 0)
                {
                    sql += string.Format(" and tags LIKE CONCAT(CONCAT('%', '{0}'),'%')", config.tags[0]);
                }
                else
                {
                    sql += string.Format(" or tags LIKE CONCAT(CONCAT('%', '{0}'),'%')", config.tags[i]);
                }

            }
            if (config.fileSize != 0)
                sql += " and file_size <=" + config.fileSize;

            if (config.eTime != 0)
                sql += " and images.created_at >=" + config.eTime;

            sql += " and width >= " + config.w_min + " and width <= " + config.w_max + " and height>= " + config.h_min + " and height <=" + config.h_max;
            switch (config.order)
            {
                case 3: sql += " ORDER BY created_at"; break;
                case 2: sql += " ORDER BY height"; break;
                case 1: sql += " ORDER BY width"; break;
                case 0: sql += " ORDER BY score"; break;
                default: sql += " ORDER BY created_at"; break;
            }
            switch (config.sort)
            {
                case 1: sql += " DESC"; break;
                default: sql += " ASC"; break;
            }
            sql += string.Format(" limit {0}", config.limit);
            DataSet ds = DBhelper.getDS(sql);
            return ds;
        }
    }
}