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
    }
}
