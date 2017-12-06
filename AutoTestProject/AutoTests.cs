using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutoTestProject
{
    [TestClass]
    public class JSChromeTest
    {
        static IWebDriver web = null;

        [ClassInitialize]
        public static void Setup(TestContext tc)
        {
            web = new ChromeDriver();
            web.Navigate().GoToUrl("http://localhost:63818/Register.aspx");
        }

        [TestInitialize]
        public void RefreshWeb()
        {
            web.Navigate().Refresh();
        }

        [ClassCleanup]
        public static void QuitWeb()
        {

            web.Quit();

        }

        [DataTestMethod]
        [DataRow("UserName", "")]
        [DataRow("StatusMessage", "")]
        [DataRow("Password", "")]
        [DataRow("ConfirmPassword", "")]
        public void JSTestChrome(string field, string result)
        {
            IWebElement webel = web.FindElement(By.Id(field));
            Assert.AreEqual(result, webel.GetAttribute("value"));
        }

        [DataTestMethod]
        [DataRow("UserName", "")]
        [DataRow("StatusMessage", "")]
        [DataRow("Password", "")]
        [DataRow("ConfirmPassword", "")]
        public void JSTestCalcChromeSeimpleCheck(string field, string result)
        {
            web.FindElement(By.Id(field)).Click();
            string res = web.FindElement(By.Id("answer")).GetAttribute("value");
            Assert.AreEqual(result, res);
        }

        [DataTestMethod]
        [DataRow("UserName", "NewUserName")]
        [DataRow("StatusMessage", "NewStatusMessage")]
        [DataRow("Password", "NewPassword")]
        [DataRow("ConfirmPassword", "NewConfirmPassword")]
        public void JSTestCalcChromeComplexCheck(string field, string text)
        {
            web.FindElement(By.Id(field)).SendKeys(text);
            string res = web.FindElement(By.Id(field)).GetAttribute("value");
            Assert.AreEqual(text, res);
        }

        [DataTestMethod]
        public void JSTestCalcChromeRealJob(string firstbutton, string operation, string secondbutton, string result)
        {
            web.FindElement(By.Id("UserName")).SendKeys("NewUserName");
            web.FindElement(By.Id("StatusMessage")).SendKeys("NewStatusMessage");
            web.FindElement(By.Id("Password")).SendKeys("NewPassword");
            web.FindElement(By.Id("ConfirmPassword")).SendKeys("NewPassword");

            web.FindElement(By.LinkText("Register")).Click();

            //Assert.AreEqual(result, res);
        }
    }
}
