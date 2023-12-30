using System;
using CbExamples.MSTest.Infra;
using CloudBeat.Kit.MSTest.Attributes;
using OpenQA.Selenium;

namespace CbExamples.MSTest.Pages.SauceDemo
{
	public class LoginPage : PageObjectBase
	{
		private const string DEFAULT_BASE_URL = "https://www.saucedemo.com";
		private readonly string baseUrl;

		#region Page Elements

		private IWebElement LoginBtn => WebElementFinder.GetElement(driver, By.Id("login-button"), 5);

        private IWebElement UsernameField => WebElementFinder.GetElement(driver, By.Id("user-name"));

        private IWebElement PasswordField => WebElementFinder.GetElement(driver, By.Id("password"));

        private IWebElement errorMessage => WebElementFinder.GetElement(driver, By.XPath("//h3[@data-test='error']"));

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

		[CbStep("Assert login error with message: {expectedMessage}")]
        public void AssertLoginErrorMessage(string expectedMessage)
        {
            if (errorMessage == null)
                Assert.Fail("Error message element not found");
            string actualMessage = errorMessage.Text;
            Assert.AreEqual<string>(expectedMessage, actualMessage);
        }
    }
}

