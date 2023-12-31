using System;
using CbExamples.MSTest.Infra;
using CbExamples.MSTest.Pages.SauceDemo;

namespace CbExamples.MSTest.Tests.SauceDemo
{
    [TestClass]
    public class LoginTests : WebDriverTest
    {
		[TestMethod("Standard user login behaviour")]
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
    }
}

