
using Print.Moe.Net.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Print.Moe.Net.App_Code
{
    public static class DataTool
    {
        public static Dictionary<string,int> getTags(int number)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            string sql = string.Format("select * from tag where total >{0} and tag not like '%(%' order by total desc",number);
            DataSet ds = MySQLTool.getDataSet(sql);
            foreach(DataRow row in ds.Tables[0].Rows)
            {
                dict.Add((string)row["tag"], (int)row["total"]);
            }
            return dict;
        }
        public static string getNewImageCount(int day)
        {
            string sql = string.Format("select count(*) as count from images where created_at>={0}", DateUtil.getTime - 24 * 60 * 60 * day);
            DataSet ds = MySQLTool.getDataSet(sql);
            return ds.Tables[0].Rows[0]["count"].ToString();
        }
        public static string getCount(int day)
        {
            string sql = string.Format("select count(*) as count from apistatistics where time>={0}", DateUtil.getTime-24*60*60*day);
            DataSet ds = MySQLTool.getDataSet(sql);
            return ds.Tables[0].Rows[0]["count"].ToString();
        }
        public static List<int> getChartStatistics(long endTime)
        {
            
            string sql0 = string.Format(@"SELECT COUNT( * ) count,  '{0}'TIME FROM  `apistatistics` WHERE  `time` >{1} and `time`<  {2} ", endTime,endTime, endTime+3600);
            for(int i = 0; i < 8; i++)
            {
                endTime = endTime - 60 * 60;
                sql0 = sql0 + string.Format(@" UNION ALL SELECT COUNT( * ) count,  '{0}'TIME FROM  `apistatistics`  WHERE  `time` >{1} and `time`<  {2} ", endTime,endTime, endTime + 3600);
            }
            sql0 += " order by time";
            DataSet ds = MySQLTool.getDataSet(sql0);
            List<int> list = new List<int>();

            foreach(DataRow row in ds.Tables[0].Rows)
            {
                list.Add(Convert.ToInt32( row["count"]));
            }
            return list;
        }
        public static List<int> getChartStatisticsDays()
        {
            long endDay = DateUtil.zeroTimes;
            string sql0 = string.Format(@"SELECT COUNT( * ) count,  '{0}'TIME FROM  `apistatistics` WHERE  `time` >{1} and `time`<  {2} ", endDay, endDay, endDay + 24*60*60);
            for (int i = 0; i < 11; i++)
            {
                endDay = endDay - 24*60 * 60;
                sql0 = sql0 + string.Format(@" UNION ALL SELECT COUNT( * ) count,  '{0}'TIME FROM  `apistatistics`  WHERE  `time` >{1} and `time`<  {2} ", endDay, endDay, endDay + 3600);
            }
            sql0 += " order by time";
            DataSet ds = MySQLTool.getDataSet(sql0);
            List<int> list = new List<int>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(Convert.ToInt32(row["count"]));
            }
            return list;
        }
        public static string getChartStatistics(long endTime,string type)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            string sql0 = string.Format(@"SELECT COUNT( * ) count,  '{0}'TIME FROM  `apistatistics` WHERE  `time` >{1} and `time`<  {2} and `func` ='{3}' ", endTime, endTime, endTime + 3600,type);
            for (int i = 0; i < 8; i++)
            {
                endTime = endTime - 60 * 60;
                sql0 = sql0 + string.Format(@" UNION ALL SELECT COUNT( * ) count,  '{0}'TIME FROM  `apistatistics`  WHERE  `time` >{1} and `time`< {2} and `func` ='{3}' ", endTime, endTime, endTime + 3600,type);
            }
            sql0 += " order by time";
            DataSet ds = MySQLTool.getDataSet(sql0);
            ArrayList list = new ArrayList();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                list.Add(row["count"]);
            }
            return js.Serialize(list);
        }
        public static string getTags()
        {
            string sql = "SELECT * FROM  `wordcount`   ORDER BY  `wordcount`.`id` DESC LIMIT 1";
            DataSet ds = MySQLTool.getDataSet(sql);
            return ds.Tables[0].Rows[0]["result"].ToString();
        }
        public static void setCount(string time)
        {
            string sql = string.Format("INSERT IGNORE INTO config VALUES('{0}',0)",time);
            DataSet ds = MySQLTool.getDataSet(sql);
        }
        public static void addHis(string api,string from,string kid)
        {
            string sql = string.Format("INSERT INTO apistatistics(time,func,ip,kid) VALUES('{0}','{1}','{2}','{3}')", DateUtil.getTime,api,from,kid);
            MySQLTool.runSql(sql);
        }
        public static void addHis(string api, string from, List<ImageModel> list)
        {
            string sql = string.Format("INSERT INTO apistatistics(time,func,ip,kid) VALUES" );
            long time = DateUtil.getTime;
            foreach (ImageModel img in list)
            {
                sql += string.Format("('{0}', '{1}', '{2}', '{3}')",time,api,from,img.Kid);
            }
            MySQLTool.runSql(sql);
        }
        public static string getSum()
        {
            string sql = "SELECT count(*) as sum from images" ;
            DataSet ds = MySQLTool.getDataSet(sql);
            if (ds == null)
            {
                return "0";
            }
            return ds.Tables[0].Rows[0]["sum"].ToString();
        }
        public static string getSum(string table)
        {
            string sql = "SELECT count(*) as sum from "+table;
            DataSet ds = MySQLTool.getDataSet(sql);
            if (ds == null)
            {
                return "0";
            }
            return ds.Tables[0].Rows[0]["sum"].ToString();
        }
        public static decimal getFileTotalSize()
        {
            string sql = "SELECT SUM( file_size ) /1024 /1024 /1024 as sum FROM  `images`  WHERE 1";
            DataSet ds = MySQLTool.getDataSet(sql);
            Decimal num =Convert.ToDecimal ( ds.Tables[0].Rows[0]["sum"].ToString());
            
            return Math.Round(num, 2, MidpointRounding.AwayFromZero);
        }
        
    }
}