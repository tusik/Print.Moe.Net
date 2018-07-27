using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Print.Moe.Net.Models
{
    public class UserModel
    {
        private int uid;
        private string username;
        private string passwd;
        private string fingerprint;
        private string repasswd;
        public string Repasswd { get; set; }
        public string Username { get; set ; }
        public string Passwd { get ; set; }

    }
}