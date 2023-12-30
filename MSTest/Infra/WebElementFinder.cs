using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace CbExamples.MSTest.Infra
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

        public static IWebElement GetElement(IWebElement parent, By elementLocator, int timeOutInSecs = 30, bool throwOnTimeout = false)
        {
            try
            {
                var wrapsDriver = parent as IWrapsDriver;
                if (wrapsDriver == null) return null;
                var wait = new WebDriverWait(wrapsDriver.WrappedDriver, TimeSpan.FromSeconds(timeOutInSecs));
                WebDriverWaitCondition(wait);
                return wait.Until(d => FindVisibleAndEnabledElement(parent, elementLocator));
            }
            catch (WebDriverTimeoutException ex)
            {
                if (throwOnTimeout)
                    throw new WebDriverTimeoutException($"Unable to find WebElement {elementLocator} due to: " + ex.Message);
                return null;
            }
        }

        public static ReadOnlyCollection<IWebElement> GetElements(IWebDriver driver, By elementLocator, int timeOutInSecs = 30, bool throwOnTimeout = false)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSecs));
                WebDriverWaitCondition(wait);
                return wait.Until(d => FindElements(d, elementLocator));
            }
            catch (WebDriverTimeoutException ex)
            {
                if (throwOnTimeout)
                    throw new WebDriverTimeoutException($"Unable to find WebElement {elementLocator} due to: " + ex.Message);
                return null;
            }
        }

        private static ReadOnlyCollection<IWebElement> FindElements(ISearchContext searchContext, By elementLocator)
        {
            var elementList = searchContext.FindElements(elementLocator);

            if (elementList.Count > 0)
            {
                return elementList;
            }
            throw new NoSuchElementException($"No elements with locator {elementLocator} are found.");
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

