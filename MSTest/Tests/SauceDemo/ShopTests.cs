using System;
using CbExamples.MSTest.Pages.SauceDemo;
using CbExamples.MSTest.Infra;

namespace CbExamples.MSTest.Tests.SauceDemo
{
    [TestClass]
    public class ShopTests : WebDriverTest
    {
		[DataTestMethod]
        [Description("Test add/remove buttons")]
		[DataRow("standard_user", "secret_sauce")]
        [DataRow("problem_user", "secret_sauce")]
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

