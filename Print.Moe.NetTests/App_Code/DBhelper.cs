using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// DBhelper 的摘要说明
/// </summary>
public static class DBhelper
{
    public static MySqlConnection conn;
    public static string connstr = "server=by.cx;user id=print;password=ymjymj;database=print;charset=utf8";
    public static void openLink()
    {
        conn = new MySqlConnection(connstr);
        if (conn.State==System.Data.ConnectionState.Closed)
        {
            conn.Open();
        }
    }
    public static void closeLink()
    {
        conn = new MySqlConnection(connstr);
        if (conn.State == System.Data.ConnectionState.Open)
        {
            conn.Close();
        }
    }
    public static int runSQL(String sql)
    {
        openLink();
        MySqlCommand cmd = new MySqlCommand(sql, conn);
        int res = cmd.ExecuteNonQuery();
        closeLink();
        return res;
    }
    public static DataSet getDS(String sql)
    {
        openLink();
        MySqlDataAdapter da = new MySqlDataAdapter(sql,conn);
        DataSet ds = new DataSet();
        da.Fill(ds);
        
        closeLink();
        return ds;
    }

}