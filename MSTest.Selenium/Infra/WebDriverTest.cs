using CloudBeat.Kit.MSTest;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Events;

namespace CbExamples.MSTest.Infra
{
    [TestClass]
    public abstract class WebDriverTest : CbTest
    {
        private IWebDriver _driver = null;

        public EventFiringWebDriver Driver { get; private set; }

        [TestInitialize]
        public void SetUpWebDriver()
        {
            ChromeOptions options = new ChromeOptions();
            _driver = new ChromeDriver(options);
            Driver = new EventFiringWebDriver(_driver);
            CbMSTest.WrapWebDriver(Driver);
        }

        [TestCleanup]
        public void DisposeWebDriver()
        {
            if (_driver != null)
            {
                CbMSTest.AddScreenshotOnError();

                try
                {
                    _driver.Close();
                    _driver.Quit();
                }
                catch
                {
                }

                _driver = null;
                Driver = null;
            }
        }
    }
}

