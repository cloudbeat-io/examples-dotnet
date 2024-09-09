using System.Collections.ObjectModel;
using CbExamples.NUnit4.Infra;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CbExamples.MSTest.Pages.SauceDemo
{
    public class ProductsPage : PageObjectBase
    {
        public ProductsPage(IWebDriver driver) : base(driver)
        {
        }

        #region Page Elements

        private ReadOnlyCollection<IWebElement> AddToCartButtonList => WebElementFinder.GetElements(driver, By.XPath("//button[text()='Add to cart']"));

        private ReadOnlyCollection<IWebElement> RemoveButtonList => WebElementFinder.GetElements(driver, By.XPath("//button[text()='Remove']"));

        private ReadOnlyCollection<IWebElement> PriceBarList => WebElementFinder.GetElements(driver, By.ClassName("pricebar"));

        #endregion

        public void AssertProductsCount(int expectedProductsCount)
        {
            if (AddToCartButtonList == null)
                Assert.Fail("No \"Add to cart\" buttons are found");
            Assert.That(
                AddToCartButtonList.Count, Is.EqualTo(expectedProductsCount),
                "Assert amount of products");
        }

        public void AssertPriceBarButtonText(int priceBarIndex, string expectedText)
        {
            var priceBarList = PriceBarList;
            if (priceBarList == null)
                Assert.Fail("No price bar elements are found");
            if (priceBarList.Count - 1 < priceBarIndex)
                Assert.Fail("There are less price bar elements than expected");
            var selectedPriceBar = priceBarList[priceBarIndex];
            var button = selectedPriceBar.FindElement(By.TagName("button"));
            if (button == null)
                Assert.Fail("No button inside price bar element is found");
            Assert.That(
                expectedText, Is.EqualTo(button.Text));
        }

        public void ClickAddToCartButton(int buttonIndex)
        {
            var addToCartButtonList = AddToCartButtonList;
            if (addToCartButtonList == null)
                Assert.Fail("No \"Add to cart\" buttons are found");
            if (addToCartButtonList.Count - 1 < buttonIndex)
                Assert.Fail("There are less \"Add to cart\" buttons than expected");
            addToCartButtonList[buttonIndex].Click();
        }

        public void ClickRemoveButton(int buttonIndex)
        {
            var removeButtonList = RemoveButtonList;
            if (removeButtonList == null)
                Assert.Fail("No \"Remove\" buttons are found");
            if (removeButtonList.Count - 1 < buttonIndex)
                Assert.Fail("There are less \"Remove\" buttons than expected");
            removeButtonList[buttonIndex].Click();
        }
    }
}

