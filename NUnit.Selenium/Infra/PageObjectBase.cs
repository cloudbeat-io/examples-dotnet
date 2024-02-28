using System;
using OpenQA.Selenium;

namespace CbExamples.NUnit.Infra
{
	public class PageObjectBase
    {
        protected static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();
        protected readonly IWebDriver driver;

        public PageObjectBase(IWebDriver driver)
		{
			this.driver = driver;
		}
	}
}

