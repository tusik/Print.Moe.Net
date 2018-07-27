using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Print.Moe.Net.App_Code
{
    public class ConnectionPool
    {
        private static ConnectionPool cpool = null;
        private static Object objlock = typeof(ConnectionPool);
        private int size = 100;
        private int used =0;
        private ArrayList pool = null;
        private string ConnectionStr = System.Configuration.ConfigurationManager.ConnectionStrings["print"].ToString();
        public ConnectionPool()
        {
            pool = new ArrayList();
        }
        public static ConnectionPool getPool()
        {
            lock (objlock)
            {
                if (cpool == null)
                {
                    cpool = new ConnectionPool();
                }
                return cpool;
            }
            
        }
        public MySqlConnection GetConnection()
        {
            lock (pool)
            {
                MySqlConnection tmp = null;
                if (pool.Count > 0)
                {
                    tmp = (MySqlConnection)pool[0];
                    tmp.Open();
                    pool.RemoveAt(0);

                    if (!isUserful(tmp))
                    {
                        used--;
                        tmp = GetConnection();
                    }
                }
                else
                {
                    if (used <= size)
                    {
                        try
                        {
                            MySqlConnection conn = new MySqlConnection(ConnectionStr);
                            conn.Open();
                            used++;
                            tmp = conn;
                        }
                        catch (Exception e)
                        {

                        }
                    }
                }
                    return tmp;
            }
        }
        public void closeConnection(MySqlConnection con)
        {
            lock (pool)
            {
               if (con != null)
                    {
                        pool.Add(con);
                    }
                }
        }
            private bool isUserful(MySqlConnection con)
            {
             //主要用于不同用户
             bool result = true;
             if (con != null)
             {
                 string sql = "select 1";//随便执行对数据库操作
                 MySqlCommand cmd = new MySqlCommand(sql, con);
                 try
                 {
                     cmd.ExecuteScalar().ToString();
                 }
                 catch
                 {
                     result = false;
                 }
 
             }
             return result;
         }

    }
}