using CloudBeat.Kit.Common.Attributes;
using CloudBeat.Kit.Common.Enums;
using CbExamples.NUnit.Infra;
using NUnit.Framework;
using CbExamples.NUnit.Pages.SauceDemo;

namespace CbExamples.NUnit.Tests.SauceDemo
{
    [CbTestMode(CbTestModeEnum.Web)]
    public class ShopTests : WebDriverTest
    {
        [Description("Test add/remove buttons")]
        [TestCase("standard_user", "secret_sauce")]
        [TestCase("problem_user", "secret_sauce")]
        [Category("ShoppingCart")]
        [Category("WithLogin")]
        public void ProductsAddToRemoveFromCart(string username, string password)
		{
            var loginPage = new LoginPage(Driver);
            loginPage.Open();
            loginPage.AssertPageOpen();
            loginPage.EnterUsername(username);
            loginPage.EnterPassword(password);
            loginPage.PressLoginButton();
            loginPage.AssertLoginSuccess();
            var productsPage = new ProductsPage(Driver);
            productsPage.AssertProductsCount(6);
            productsPage.ClickAddToCartButton(0);
            productsPage.AssertPriceBarButtonText(0, "Remove");
            productsPage.ClickRemoveButton(0);
            productsPage.AssertPriceBarButtonText(0, "Add to cart");
        }
	}
}

