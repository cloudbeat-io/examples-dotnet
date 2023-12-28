using System;
using CbExamples.NUnit.Infra;
using CbExamples.NUnit.Pages.SauceDemo;
using NUnit.Framework;

namespace CbExamples.NUnit.Tests.SauceDemo
{
	public class LoginTests : WebDriverTest
    {
        [Test(Description = "Standard user login behaviour")]
        public void StandardUserTest()
		{
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.PressLoginButton();
        }

        [Test(Description = "Locked out user login behaviour")]
        public void LockedOutUserTest()
        {

        }

        [Test(Description = "Problem user login behaviour")]
        public void ProblemUserTest()
        {

        }
    }
}

