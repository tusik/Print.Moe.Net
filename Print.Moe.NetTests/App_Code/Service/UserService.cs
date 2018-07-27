using Print.Moe.Net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Print.Moe.Net.App_Code.Service.Tests
{
    public class UserService
    {
        public static int updateFavor(string uname,string kid)
        {
            string sql = string.Format("insert into favor(uid,kid,status) values((select uid from user where username='{0}'),'{1}','{2}') ON DUPLICATE KEY UPDATE status=-status;select status from favor where uid=(select uid from user where username='{0}') and kid={1}", uname,kid,1);
            DataSet ds = DBhelper.getDS(sql);
            int status = (int)ds.Tables[0].Rows[0][0];
            return status;
        }
        public static bool usernameExist(UserModel user)
        {
            string sql = string.Format("select * from user where username ='{0}'", user.Username);
            DataSet ds = DBhelper.getDS(sql);
            if (ds.Tables[0].Rows.Count>0)
            {
                return true;
            }
            return false;
        }
        public static bool reg(UserModel user)
        {
            string sql = string.Format("insert into user (username,passwd) values ('{0}','{1}')", user.Username,user.Passwd);
            int res = DBhelper.runSQL(sql);
            if (res>0)
            {
                return true ;
            }
            return false;
        }
        public static bool login(string username,string passwd)
        {
            string sql = string.Format("update user set fingerprint =1 where username='{0}' and passwd= '{1}'", username, passwd);
            int num = MySQLTool.runSql(sql);
            DataSet ds = DBhelper.getDS(sql);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}