using System;
using CloudBeat.Kit.NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;

namespace CbExamples.NUnit.Infra
{
    public class WebDriverTest : TestBase
    {
        private IWebDriver _driver = null;

        public EventFiringWebDriver Driver { get; private set; }

        [SetUp]
        public void SetUpWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            Driver = new EventFiringWebDriver(_driver);
            CbNUnit.WrapWebDriver(Driver);
        }

        [TearDown]
        public void DisposeWebDriver()
        {
            if (_driver != null)
            {
                try
                {
                    _driver.Close();
                    _driver.Quit();
                }
                catch { }
                _driver = null;
                Driver = null;
            }
        }
    }
}

