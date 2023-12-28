using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CbExamples.NUnit.Infra
{
	public static class WebElementFinder
	{
        public static IWebElement GetElement(IWebDriver driver, By elementLocator, int timeOutInSecs = 30, bool throwOnTimeout = false)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSecs));
                WebDriverWaitCondition(wait);
                return wait.Until(d => FindVisibleAndEnabledElement(d, elementLocator));
            }
            catch (WebDriverTimeoutException ex)
            {
                if (throwOnTimeout)
                    throw new WebDriverTimeoutException($"Unable to find WebElement {elementLocator} due to: " + ex.Message);
                return null;
            }
        }

        private static IWebElement FindVisibleAndEnabledElement(ISearchContext searchContext, By elementLocator)
        {
            IWebElement element = searchContext.FindElement(elementLocator);

            if (element.Displayed && element.Enabled)
            {
                return element;
            }
            throw new ElementNotVisibleException($"WebElement with locator {elementLocator} is not visible.");
        }

        private static readonly Action<WebDriverWait> WebDriverWaitCondition = (wait) =>
        {
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
        };
    }
}

