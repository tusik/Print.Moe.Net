using Print.Moe.Net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Print.Moe.Net.App_Code.Service
{
    public class ImgService
    {
        public static ImageModel selectImg(DefaultConfig config)
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
            DataSet ds = MySQLTool.getDataSet(sql);
            return toImage(ds.Tables[0].Rows[0]);
        }
        public static List<ImageModel> selectImgList(DefaultConfig config)
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
            DataSet ds = MySQLTool.getDataSet(sql);
            List<ImageModel> list = new List<ImageModel>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(toImage(row));
            }
            return list;
        }
        public static string selectImgListSql(DefaultConfig config, int index, int num, string username)
        {
            string sql = "SELECT *,ifnull(ff.status,0) as tstatus FROM images LEFT JOIN (SELECT kid AS fkid, COUNT( * ) AS num FROM favor GROUP BY fkid) AS f ON f.fkid = images.kid ";
            sql += string.Format("left join(select kid as ffkid, status from favor where uid = (select uid from user where username = '{0}')) as ff on ff.ffkid = images.kid  WHERE 1 = 1", username);
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
            if(index!=-1&&num!=-1)
            sql += string.Format(" limit {0},{1}", index, num);
            return sql;
        }
        public static List<ImageModel> selectImgList(DefaultConfig config, int index, int num, string username)
        {
            string sql = selectImgListSql(config,index,num,username);
            DataSet ds = MySQLTool.getDataSet(sql);
            List<ImageModel> list = new List<ImageModel>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(toImageWithLikes(row));
            }
            return list;
        }
        public static List<ImageModel> selectImgListByUser(DefaultConfig config, int index, int num, string username)
        {
            string sql = "SELECT *,ifnull(ff.status,0) as tstatus FROM images LEFT JOIN (SELECT kid AS fkid, COUNT( * ) AS num FROM favor GROUP BY fkid) AS f ON f.fkid = images.kid ";
            sql += string.Format("left join(select kid as ffkid, status from favor where uid = (select uid from user where username = '{0}')) as ff on ff.ffkid = images.kid  WHERE 1 = 1 and  kid in ((select kid from favor where status=1 and uid = (select uid from user where username = '{0}')))", username);
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

            sql += string.Format(" limit {0},{1}", index, num);
            DataSet ds = MySQLTool.getDataSet(sql);
            List<ImageModel> list = new List<ImageModel>();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(toImageWithLikes(row));
            }
            return list;
        }
        public static int selectImgListCount(DefaultConfig config)
        {
            string sql = selectImgListSql(config,-1,-1,config.username);
            sql = "select count(*) as count from (" + sql + ") as newTable";
            DataSet ds = MySQLTool.getDataSet(sql);

            return Convert.ToInt32(ds.Tables[0].Rows[0]["count"].ToString());
        }
        public static int selectImgListCountByUser(DefaultConfig config, string username)
        {
            string sql = string.Format("SELECT count(*) as count FROM images WHERE 1=1 and kid in ((select kid from favor where status=1 and uid = (select uid from user where username = '{0}')))", username);
            if (!(config.author == null || config.author.Length == 0))
                sql += string.Format(" and author LIKE CONCAT(CONCAT('%', '{0}'),'%')", config.author);
            for (int i=0;i< config.tags.Count;i++)
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
            if (config.ext != "all")
            {
                sql += string.Format(" and file_url like '%{0}'", config.ext);
            }
            sql += " and width >= " + config.w_min + " and width <= " + config.w_max + " and height>= " + config.h_min + " and height <=" + config.h_max;
            if (config.username != "")
            {
                sql += string.Format(" and kid in(select kid from favor where uid =(select uid from user where username ='{0}')) ",config.username);
            }
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

            DataSet ds = MySQLTool.getDataSet(sql);

            return Convert.ToInt32(ds.Tables[0].Rows[0]["count"].ToString());
        }
        public static ImageModel selectImgById(long id)
        {
            string sql = "SELECT * FROM images WHERE kid=" + id;
            DataSet ds = MySQLTool.getDataSet(sql);
            return toImage(ds.Tables[0].Rows[0]);
        }
        public static List<ImageModel> random(DefaultConfig config) 
        {
            string sql = "SELECT* FROM `images` WHERE id >= (SELECT floor(RAND() * (SELECT MAX(id) FROM `images`))) and created_at>=" + config.eTime;
            if (!(config.author == null || config.author.Length == 0))
            {
                sql += string.Format(" and author LIKE CONCAT(CONCAT('%', '{0}'),'%')", config.author);
            }
            if (!(config.tags.Count == 0))
            {
                if (config.tags[0] != null)
                {
                    sql += string.Format(" and tags LIKE CONCAT(CONCAT('%', '{0}'),'%')", config.tags[0]);
                }
                
                config.tags.Remove(config.tags[0]);
                foreach (string tag in config.tags)
                {
                    sql += string.Format(" or tags LIKE CONCAT(CONCAT('%', '{0}'),'%')", tag);
                }
            }
            if (config.username != "")
            {
                sql += string.Format(" and kid in(select kid from favor where uid =(select uid from user where username ='{0}')) ", config.username);
            }
            if (config.fileSize != 0)
            {
                sql += string.Format(" and file_size <={0} ", config.fileSize);
            }
            sql += string.Format(" and width >= {0} and width <= {1} and height>= {2} and height <={3} order BY id LIMIT {4};", config.w_min, config.w_max, config.h_min, config.h_max, config.limit);

            DataSet ds = MySQLTool.getDataSet(sql);
            List<ImageModel> list = new List<ImageModel>();
            if (ds==null)
            {
                ImageModel image = new ImageModel();
                image.Kid = "244060";
                list.Add(image);
            }
            else
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    list.Add(toImage(row));
                }
            }
            
            return list;
        }
        public static ImageModel toImage(DataRow row)
        {

            ImageModel img = new ImageModel(
                Convert.ToInt64(row["id"]),
                row["kid"].ToString(),
                row["author"].ToString(),
                row["tags"].ToString(),
                row["created_at"].ToString(),
                row["source"].ToString(),
                row["score"].ToString(),
                row["md5"].ToString(),
                row["file_size"].ToString(),
                row["file_url"].ToString(),
                row["preview_url"].ToString(),
                row["sample_url"].ToString(),
                row["jpeg_url"].ToString(),
                Convert.ToInt32(row["has_children"]),
                Convert.ToDouble(row["width"]),
                Convert.ToDouble(row["height"]));
            return img;
        }
        public static ImageModel toImageWithLikes(DataRow row)
        {

            ImageModel img = new ImageModel(
                Convert.ToInt64(row["id"]),
                row["kid"].ToString(),
                row["author"].ToString(),
                row["tags"].ToString(),
                row["created_at"].ToString(),
                row["source"].ToString(),
                row["score"].ToString(),
                row["md5"].ToString(),
                row["file_size"].ToString(),
                row["file_url"].ToString(),
                row["preview_url"].ToString(),
                row["sample_url"].ToString(),
                row["jpeg_url"].ToString(),
                Convert.ToInt32(row["has_children"]),
                Convert.ToDouble(row["width"]),
                Convert.ToDouble(row["height"]),
                row["num"].ToString(),
                Convert.ToInt32(row["tstatus"]));
            return img;
        }
    }
}