using System.Threading.Tasks;
using CbExamples.NUnitPlaywright.Infra;
using CloudBeat.Kit.NUnit.Attributes;
using CloudBeat.Kit.Playwright;
using Microsoft.Playwright;
using NUnit.Framework;

namespace CbExamples.NUnitPlaywright.Pages.SauceDemo
{
    public class LoginPage : PageObjectBase
	{
		private const string DEFAULT_BASE_URL = "https://www.saucedemo.com";
		private readonly string baseUrl;

		#region Page Elements

		private ILocator LoginBtn => page.Locator("#login-button");

        private ILocator UsernameField => page.Locator("#user-name");

        private ILocator PasswordField => page.Locator("#password");

        private ILocator UsernameFieldInvalid => page.Locator("#user-nameINVALID");

        private ILocator ErrorMessage => page.Locator("//h3[@data-test='error']");

        #endregion

        public LoginPage(IPage page, string baseUrl = null) : base(page) 
		{
			this.baseUrl = baseUrl;
		}

		[CbStep("Open \"Login\" page")]
		public async Task Open()
		{
            await page.GotoAsync(baseUrl ?? DEFAULT_BASE_URL);
		}

        [CbStep("Assert \"Login\" page opened successfully")]
        public void AssertPageOpen()
		{
			var loginBtn = LoginBtn;
			if (loginBtn == null)
				Assert.Fail("Login page assertion failed - Login button not found");
		}

        [CbStep("Type \"{username}\" in \"Username\" field")]
        public async Task EnterUsername(string username)
		{
			var usernameFld = UsernameField;
			if (usernameFld == null)
				Assert.Fail("Username field not found");
			await usernameFld.ClickAsync();
			await usernameFld.FillAsync(username);
		}

        [CbStep("Type invalid \"{username}\" in \"Username\" field")]
        public async Task EnterUsernameInvalid(string username)
        {
            var usernameFld = UsernameField;
            if (usernameFld == null)
                Assert.Fail("Username field not found");
            await usernameFld.ClickAsync();
            await usernameFld.FillAsync(username);
        }

        [CbStep("Type \"{password}\" in \"Password\" field")]
        public async Task EnterPassword(string password)
        {
            var passwordFld = PasswordField;
            if (passwordFld == null)
                Assert.Fail("Password field not found");
            await passwordFld.ClickAsync();
            await passwordFld.FillAsync(password);
        }

        [CbStep("Click on \"Login\" button")]
        public async Task PressLoginButton()
		{
			var loginBtn = LoginBtn;
            if (loginBtn == null)
                Assert.Fail("Login button not found");
			await loginBtn.ClickAsync();
        }

        [CbStep("Assert successful login")]
        public async Task AssertLoginSuccess()
        {
            // For assertions CloudBeat.Kit.Playwright.CbAssertions should be used instead of Microsoft.Playwright.Assertions
            // using Microsoft.Playwright.Assertions when running from CloudBeat will lead to exception
            await CbAssertions.Expect(LoginBtn).ToBeHiddenAsync();
        }

        [CbStep("Assert login error message: {message}")]
        public async Task AssertLoginErrorMessage(string message)
        {
            await CbAssertions.Expect(ErrorMessage).ToContainTextAsync(message);
        }
    }
}
