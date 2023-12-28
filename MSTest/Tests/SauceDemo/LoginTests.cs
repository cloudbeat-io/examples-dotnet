using System;
using CbExamples.NUnit.Infra;
using CbExamples.NUnit.Pages.SauceDemo;

namespace CbExamples.NUnit.Tests.SauceDemo
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

        }

        [TestMethod("Problem user login behaviour")]
        public void ProblemUserTest()
        {

        }
    }
}

