﻿using System;
using CbExamples.NUnit.Infra;
using CbExamples.NUnit.Pages.SauceDemo;
using CloudBeat.Kit.MSTest.Attributes;

namespace CbExamples.NUnit.Tests.SauceDemo
{
    [TestClass]
    public class LoginTests : WebDriverTest
    {
        [CbTestMethod("Standard user login behaviour")]
        public void StandardUserTest()
		{
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();
            loginPage.EnterUsername("standard_user");
            loginPage.EnterPassword("secret_sauce");
            loginPage.PressLoginButton();
        }

        [CbTestMethod("Locked out user login behaviour")]
        public void LockedOutUserTest()
        {

        }

        [CbTestMethod("Problem user login behaviour")]
        public void ProblemUserTest()
        {

        }
    }
}
