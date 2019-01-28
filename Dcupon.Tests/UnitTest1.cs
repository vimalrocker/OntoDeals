using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dcupon.Models;
using Dcupon.DAL;
using System.Collections.Generic;

namespace Dcupon.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var test = Helper.Encrypt("Admin!23", true);

            AdminProfile obj = new AdminProfile();

            AdminModel model = new AdminModel();
            obj.Username = "test@gmail.com";
            obj.Password = "fsMCeOWkhYA=";
            obj.Role = 1;
            obj.IsActive = 0;
            model.Save(obj);
           



        }
    }
 
}
