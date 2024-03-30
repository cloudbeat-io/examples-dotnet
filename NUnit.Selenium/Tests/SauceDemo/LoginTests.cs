using System;
using CbExamples.NUnit.Infra;
using CbExamples.NUnit.Pages.SauceDemo;
using CloudBeat.Kit.Common.Attributes;
using CloudBeat.Kit.Common.Enums;
using NUnit.Framework;

namespace CbExamples.NUnit.Tests.SauceDemo
{
    [Category("Login")]
    [CbTestMode(CbTestModeEnum.Mobile)]
    public class LoginTests : WebDriverTest
    {
        [Test(Description = "Standard user login behaviour"), Order(1)]
        [Category("JIRA=ISO-124")]
        [Category("User=Standard")]
        [Category("Nightly")]
        public void StandardUserTest()
		{
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            /*loginPage.AssertPageOpen();
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.PressLoginButton();
            loginPage.AssertLoginSuccess();*/
        }

        [Test(Description = "Locked out user login behaviour")]
        [Category("Regression")]
        [Category("Nightly")]
        [Order(2)]
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

        [Test(Description = "Invalid user login behaviour")]
        [Order(3)]
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

        [Test]
        [Ignore("Must be ignored")]
        [Category("Ignore")]
        public void IgnoreMe()
        {

        }
    }
}

