using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
namespace Print.Moe.Net.App_Code
{
    
    public class MySQLTool
    {
        public static string connstr = System.Configuration.ConfigurationManager.ConnectionStrings["print"].ToString();
        public static MySqlConnection conn;
        public static void openLink()
        {
            conn = ConnectionPool.getPool().GetConnection();
            while (conn == null)
            {

            }
        }
        public static void closeLink()
        {
            conn.Close();
            ConnectionPool.getPool().closeConnection(conn);
        }
        public static DataSet getDataSet(String sql)
        {
            try
            {
                openLink();
                MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);

                closeLink();
                return ds;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
        public static int runSql(String sql)
        {
            try
            {
                openLink();

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                int res = cmd.ExecuteNonQuery();
                closeLink();
                return res;
            }
            catch (Exception e)
            {
                return -1;
            }
            
            
        }
    }
}