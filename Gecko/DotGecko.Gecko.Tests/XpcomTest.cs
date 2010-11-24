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
			Xpcom.InitEmbedding(@"c:\xulrunner", null);
		}

		[AssemblyCleanup]
		public static void TerminateXpcom()
		{
			Xpcom.TermEmbedding();
		}

		[TestMethod]
		public void HasServiceManager()
		{
			Assert.IsNotNull(Xpcom.ServiceManager);
		}

		[TestMethod]
		public void HasComponentManager()
		{
			Assert.IsNotNull(Xpcom.ComponentManager);
		}

		[TestMethod]
		public void HasComponentRegistrar()
		{
			Assert.IsNotNull(Xpcom.ComponentRegistrar);
		}
	}
}
