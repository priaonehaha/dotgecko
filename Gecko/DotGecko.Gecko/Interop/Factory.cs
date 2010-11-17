using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	internal sealed class Factory<T> : nsIFactory, IDisposable where T : new()
	{
		internal Factory(String contractId)
		{
			m_ContractId = contractId;
		}

		internal String ContractId
		{
			get { return m_ContractId; }
		}

		internal Boolean IsRegistered
		{
			get { return m_IsRegistered; }
		}

		internal void Register()
		{
			if (!IsRegistered)
			{
				Type targetType = typeof(T);
				Xpcom.ComponentRegistrar.RegisterFactory(targetType.GUID, targetType.FullName, ContractId, this);
				m_IsRegistered = true;
			}
		}

		internal void Unregister()
		{
			if (IsRegistered)
			{
				Type targetType = typeof(T);
				Xpcom.ComponentRegistrar.UnregisterFactory(targetType.GUID, this);
				m_IsRegistered = false;
			}
		}

		nsResult nsIFactory.CreateInstance(Object aOuter, ref Guid iid, out IntPtr retval)
		{
			Trace.TraceInformation("nsIFactory.CreateInstance: {0}", iid);

			var target = new T();
			IntPtr pUnk = Marshal.GetIUnknownForObject(target);
			Marshal.QueryInterface(pUnk, ref iid, out retval);
			Marshal.Release(pUnk);
			return retval != IntPtr.Zero ? nsResult.NS_OK : nsResult.NS_NOINTERFACE;
		}

		void nsIFactory.LockFactory(Boolean aLock)
		{
			//TODO: Implement me!
			Trace.TraceInformation("nsIFactory.LockFactory: {0}", aLock);
		}

		public void Dispose()
		{
			if (m_IsDisposed)
			{
				return;
			}
			m_IsDisposed = true;

			Unregister();
			GC.SuppressFinalize(this);
		}

		~Factory()
		{
			Dispose();
		}

		private readonly String m_ContractId;
		private Boolean m_IsRegistered;
		private Boolean m_IsDisposed;
	}
}
