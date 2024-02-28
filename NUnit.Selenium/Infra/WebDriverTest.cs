using System;
using CloudBeat.Kit.NUnit;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;

namespace CbExamples.NUnit.Infra
{
    public class WebDriverTest : TestBase
    {
        private IWebDriver _driver = null;

        public EventFiringWebDriver Driver { get; private set; }

        [SetUp]
        public void SetUpWebDriver()
        {
            //if (CbNUnit.Current.Config.HasMandatory())
                SetupRemoteChromeDriver();
            //else
              //  SetupLocalChromeDriver();
        }

        private void SetupLocalChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            Driver = new EventFiringWebDriver(_driver);
            CbNUnit.WrapWebDriver(Driver);
        }

        private void SetupRemoteChromeDriver()
        {
            ChromeOptions options = new ChromeOptions();

            _driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), options.ToCapabilities());
            Driver = new EventFiringWebDriver(_driver);
            CbNUnit.WrapWebDriver(Driver);
        }

        [TearDown]
        public void DisposeWebDriver()
        {
            if (_driver != null)
            {
                if (TestContext.CurrentContext.Result.FailCount > 0)
                {
                    try
                    {
                        string screenshot = _driver.TakeScreenshot().AsBase64EncodedString;
                        CbNUnit.AddScreenshot(screenshot);
                    }
                    catch { }
                }
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

