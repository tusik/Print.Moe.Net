using Microsoft.VisualStudio.TestTools.UnitTesting;
using Print.Moe.Net.App_Code.Service.Tests;
using Print.Moe.Net.Controllers;
using Print.Moe.Net.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Print.Moe.Net.Controllers.Tests
{
    [TestClass()]
    [DeploymentItem(@"f:\user.xml")]
    public class RegControllerTests
    {
        public TestContext TestContext { get; set; }
        [TestMethod()]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML",
       @"f:\user.xml",
        "row",
        DataAccessMethod.Sequential)]
        public void regTest()
        {
            UserModel user = new UserModel();
            user.Username = TestContext.DataRow["username"].ToString();
            user.Passwd = TestContext.DataRow["passwd"].ToString();
            user.Repasswd = TestContext.DataRow["repasswd"].ToString();

            Assert.IsTrue(regFunc(user));

        }
        public bool regFunc(UserModel user)
        {


            if (user.Repasswd.Equals(user.Passwd))
            {
                if (!UserService.usernameExist(user))
                {
                    if (UserService.reg(user))
                    {
                      
                        return true;
                    }
                }
            }
            return false;
        }
        
    }
}