using System;
using CloudBeat.Kit.NUnit.Attributes;
using NUnit.Framework;

namespace CbExamples.NUnit.Infra
{
    [CbNUnitTest]
    [TestFixture]
    public class TestBase
	{
        protected static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        public TestBase()
		{
		}
	}
}

