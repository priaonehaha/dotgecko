using DotGecko.Gecko.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotGecko.Gecko.Tests
{
	[TestClass]
	public class XpcomTest
	{
		[AssemblyInitialize]
		public static void InitializeXpcom(TestContext testContext)
		{
			XpcomHelper.InitEmbedding(@"c:\xulrunner", null);
		}

		[AssemblyCleanup]
		public static void TerminateXpcom()
		{
			XpcomHelper.TermEmbedding();
		}

		[TestMethod]
		public void HasServiceManager()
		{
			Assert.IsNotNull(XpcomHelper.ServiceManager);
		}

		[TestMethod]
		public void HasComponentManager()
		{
			Assert.IsNotNull(XpcomHelper.ComponentManager);
		}

		[TestMethod]
		public void HasComponentRegistrar()
		{
			Assert.IsNotNull(XpcomHelper.ComponentRegistrar);
		}
	}
}
