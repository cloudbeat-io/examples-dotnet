using System;
using CbExamples.MSTest.Infra;
using CbExamples.MSTest.Pages.SauceDemo;
using CloudBeat.Kit.Common.Attributes;
using CloudBeat.Kit.Common.Enums;

namespace CbExamples.MSTest.Tests.SauceDemo
{
    [TestClass]
    [TestCategory("Login")]
    [CbTestMode(CbTestModeEnum.Web)]
    public class LoginTests : WebDriverTest
    {
		[TestMethod("Standard user login behaviour")]
        [TestCategory("JIRA=ISO-124")]
        [TestCategory("User=Standard")]
        [TestCategory("Nightly")]
        [Priority(1)]
		public void StandardUserTest()
		{
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.PressLoginButton();
        }

        [TestMethod("Locked out user login behaviour")]
        [TestCategory("Regression")]
        [TestCategory("Nightly")]
        [Priority(2)]
        public void LockedOutUserTest()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.PressLoginButton();
            loginPage.AssertLoginErrorMessage("Epic sadface: Sorry, this user has been locked out.");
        }

        [TestMethod("Invalid user login behaviour")]
        [Priority(3)]
        public void InvalidUserTest()
        {
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();
            loginPage.EnterUsername("invalid_user");
            loginPage.EnterPassword("invalid_password");
            loginPage.PressLoginButton();
            loginPage.AssertLoginErrorMessage("Epic sadface: Username and password do not match any user in this service");
        }
        [TestMethod]
        [Ignore("This method must be ignored")]
        [TestCategory("Ignore")]
        public void SkipMe()
        {

        }
    }
}

