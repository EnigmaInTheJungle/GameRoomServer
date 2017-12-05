using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameRoomsAPI;
using System.Threading;
using Moq;
using GameRoomsAPI.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTestAuthReg
    {
        [TestMethod]
        public void TestCreateUser()
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser() { UserName = "NewUser" };
            IdentityResult result = manager.Create(user, "123abc");
            Assert.AreEqual(result, IdentityResult.Success);
        }

        [TestMethod]
        public void TestCreateNewUserMethod()
        {
            var user = new Register();
            IdentityResult result = user.CreateNewUser();
            Assert.AreEqual(result, IdentityResult.Success);
        }

        [TestMethod]
        public void TestLoginUser()
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);
            var user = new IdentityUser() { UserName = "NewUser" };
            IdentityResult result = manager.Create(user, "123abc");
            
            var userManager = new UserManager<IdentityUser>(userStore);
            var userLogin = userManager.Find("NewUser", "123abc");
            Assert.AreNotEqual(null, userLogin);
        }

        //[TestMethod]
        //public void TestLoginUserMethod()
        //{
        //    var user = new Login();
        //    IdentityUser result = user.SignInUser();
        //    Assert.AreNotEqual(null, result);

        //}



    }
}
