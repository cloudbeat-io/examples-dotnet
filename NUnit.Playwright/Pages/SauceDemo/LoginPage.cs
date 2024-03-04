using System;
using CbExamples.NUnit.Infra;
using CloudBeat.Kit.NUnit.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CbExamples.NUnit.Pages.SauceDemo
{
	public class LoginPage : PageObjectBase
	{
		private const string DEFAULT_BASE_URL = "https://www.saucedemo.com";
		private readonly string baseUrl;

		#region Page Elements

		private IWebElement LoginBtn => WebElementFinder.GetElement(driver, By.Id("login-button"));

        private IWebElement UsernameField => WebElementFinder.GetElement(driver, By.Id("user-name"));

        private IWebElement PasswordField => WebElementFinder.GetElement(driver, By.Id("password"));

        private IWebElement UsernameFieldInvalid => WebElementFinder.GetElement(driver, By.Id("user-nameINVALID"));

        #endregion

        public LoginPage(IWebDriver driver, string baseUrl = null) : base(driver) 
		{
			this.baseUrl = baseUrl;
		}

		[CbStep("Open \"Login\" page")]
		public void Open()
		{
			driver.Navigate().GoToUrl(baseUrl ?? DEFAULT_BASE_URL);
		}

        [CbStep("Assert \"Login\" page opened successfully")]
        public void AssertPageOpen()
		{
			var loginBtn = LoginBtn;
			if (loginBtn == null)
				Assert.Fail("Login page assertion failed - Login button not found");
		}

        [CbStep("Type \"{username}\" in \"Username\" field")]
        public void EnterUsername(string username)
		{
			var usernameFld = UsernameField;
			if (usernameFld == null)
				Assert.Fail("Username field not found");
			usernameFld.Click();
			usernameFld.SendKeys(username);
		}

        [CbStep("Type invalid \"{username}\" in \"Username\" field")]
        public void EnterUsernameInvalid(string username)
        {
            var usernameFld = UsernameField;
            if (usernameFld == null)
                Assert.Fail("Username field not found");
            usernameFld.Click();
            usernameFld.SendKeys(username);
        }

        [CbStep("Type \"{password}\" in \"Password\" field")]
        public void EnterPassword(string password)
        {
            var passwordFld = PasswordField;
            if (passwordFld == null)
                Assert.Fail("Password field not found");
            passwordFld.Click();
            passwordFld.SendKeys(password);
        }

        [CbStep("Click on \"Login\" button")]
        public void PressLoginButton()
		{
			var loginBtn = LoginBtn;
            if (loginBtn == null)
                Assert.Fail("Login button not found");
			loginBtn.Click();
        }

        [CbStep("Assert successful login")]
        public void AssertLoginSuccess()
        {
            if (LoginBtn != null)
                Assert.Fail("Login failed");
        }

        public void AssertLoginErrorMessage(string v)
        {
            throw new NotImplementedException();
        }
    }
}

