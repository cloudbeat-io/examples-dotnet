using System;
using OpenQA.Selenium;

namespace CbExamples.NUnit.Infra
{
	public class PageObjectBase
    {
		protected readonly IWebDriver driver;

        public PageObjectBase(IWebDriver driver)
		{
			this.driver = driver;
		}
	}
}

