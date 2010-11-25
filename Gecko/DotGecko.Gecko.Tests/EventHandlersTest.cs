using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotGecko.Gecko.Tests
{
	internal class AEventArgs : EventArgs
	{
		public Boolean Handled { get; set; }
	}

	internal class BEventArgs : AEventArgs
	{}

	[TestClass]
	public class EventHandlersTest
	{
		[TestMethod]
		public void AddRemove()
		{
			var events = new EventHandlers<String>(this);

			Boolean firstAdded = events.Add<AEventArgs>("a", AEventHandler);
			Assert.IsTrue(firstAdded);
			firstAdded = events.Add<AEventArgs>("a", AEventHandler);
			Assert.IsFalse(firstAdded);

			firstAdded = events.Add<BEventArgs>("b", BEventHandler);
			Assert.IsTrue(firstAdded);
			firstAdded = events.Add<BEventArgs>("b", BEventHandler);
			Assert.IsFalse(firstAdded);


			Boolean lastRemoved = events.Remove<BEventArgs>("b", BEventHandler);
			Assert.IsFalse(lastRemoved);
			lastRemoved = events.Remove<BEventArgs>("b", BEventHandler);
			Assert.IsTrue(lastRemoved);

			lastRemoved = events.Remove<AEventArgs>("a", AEventHandler);
			Assert.IsFalse(lastRemoved);
			lastRemoved = events.Remove<AEventArgs>("a", AEventHandler);
			Assert.IsTrue(lastRemoved);
		}

		[TestMethod]
		public void Contravariance()
		{
			var events = new EventHandlers<String>(this);

			Boolean firstAdded = events.Add<AEventArgs>("v", AEventHandler);
			Assert.IsTrue(firstAdded);
			firstAdded = events.Add<AEventArgs>("v", AEventHandler);
			Assert.IsFalse(firstAdded);

			var e = new BEventArgs();
			events.Raise("v", e);
			Assert.IsTrue(e.Handled);
		}

		[TestMethod]
		public void UnknownEventArgs()
		{
			var events = new EventHandlers<String>(this);

			Boolean firstAdded = events.Add<AEventArgs>("v", AEventHandler);
			Assert.IsTrue(firstAdded);
			firstAdded = events.Add<AEventArgs>("v", AEventHandler);
			Assert.IsFalse(firstAdded);

			EventArgs e = new AEventArgs();
			events.Raise("v", e);
			Assert.IsTrue(((AEventArgs)e).Handled);

			e = new BEventArgs();
			events.Raise("v", e);
			Assert.IsTrue(((BEventArgs)e).Handled);
		}

		private static void AEventHandler(Object sender, AEventArgs e)
		{
			e.Handled = true;
		}

		private static void BEventHandler(Object sender, BEventArgs e)
		{
			e.Handled = true;
		}
	}
}
