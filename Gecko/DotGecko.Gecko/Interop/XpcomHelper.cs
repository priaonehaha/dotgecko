using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	internal static partial class XpcomHelper
	{
		internal static void InitEmbedding(String geckoPath, IAppFileLocation appFileLocation)
		{
			if (geckoPath == null)
			{
				throw new ArgumentNullException("geckoPath");
			}
			if (!File.Exists(Path.Combine(geckoPath, "xpcom.dll")))
			{
				throw new ArgumentException("Invalid path", "geckoPath");
			}

			const String pathEnvVar = "PATH";
			Environment.SetEnvironmentVariable(
				pathEnvVar,
				Environment.ExpandEnvironmentVariables(String.Format("{0};%{1}%", geckoPath, pathEnvVar)),
				EnvironmentVariableTarget.Process);

			nsILocalFile binDirectory = NewNativeLocalFile(geckoPath);
			ms_AppFileLocationProvider = new DirectoryServiceProvider(appFileLocation);

			nsResult nsresult = Xpcom.NS_InitXPCOM2(out ms_ServiceManager, binDirectory, ms_AppFileLocationProvider);
			if (nsresult != nsResult.NS_OK)
			{
				throw new ApplicationException(String.Format("Failed on NS_InitXPCOM2: {0}", nsresult));
			}

			nsresult = Xpcom.NS_GetComponentManager(out ms_ComponentManager);
			if (nsresult != nsResult.NS_OK)
			{
				throw new ApplicationException(String.Format("Failed on NS_GetComponentManager: {0}", nsresult));
			}

			nsresult = Xpcom.NS_GetComponentRegistrar(out ms_ComponentRegistrar);
			if (nsresult != nsResult.NS_OK)
			{
				throw new ApplicationException(String.Format("Failed on NS_GetComponentRegistrar: {0}", nsresult));
			}
		}

		internal static void TermEmbedding()
		{
			nsResult nsresult = Xpcom.NS_ShutdownXPCOM(ServiceManager);
			if (nsresult != nsResult.NS_OK)
			{
				throw new ApplicationException(String.Format("Failed on NS_ShutdownXPCOM: 0x{0:X8}", nsresult));
			}
		}

		internal static nsIServiceManager ServiceManager
		{
			get { return ms_ServiceManager; }
		}

		internal static nsIComponentManager ComponentManager
		{
			get { return ms_ComponentManager; }
		}

		internal static nsIComponentRegistrar ComponentRegistrar
		{
			get { return ms_ComponentRegistrar; }
		}

		internal static T GetService<T>(Guid classID) where T : class
		{
			Guid aIID = typeof(T).GUID;
			return (T)ServiceManager.GetService(classID, aIID);
		}

		internal static T GetService<T>(String contractID) where T : class
		{
			Guid aIID = typeof(T).GUID;
			return (T)ServiceManager.GetServiceByContractID(contractID, aIID);
		}

		internal static T CreateInstance<T>(Guid classID) where T : class
		{
			Guid aIID = typeof(T).GUID;
			return (T)ComponentManager.CreateInstance(classID, null, aIID);
		}

		internal static T CreateInstance<T>(String contractID) where T : class
		{
			Guid aIID = typeof(T).GUID;
			return (T)ComponentManager.CreateInstanceByContractID(contractID, null, aIID);
		}

		internal static nsILocalFile NewNativeLocalFile(String path, Boolean followLinks = true)
		{
			nsILocalFile nativeLocalFile;
			return Xpcom.NS_NewNativeLocalFile(path, followLinks, out nativeLocalFile) == nsResult.NS_OK ? nativeLocalFile : null;
		}

		#region QueryInterface

		internal static T QueryInterface<T>(Object source) where T : class
		{
			Guid iid = typeof(T).GUID;
			return (T)QueryInterface(source, iid);
		}

		internal static Object QueryInterface(Object source, Guid iid)
		{
			Object result;
			if (!TryQueryInterface(source, iid, out result))
			{
				throw new NotImplementedException();
			}
			return result;
		}

		internal static Boolean TryQueryInterface<T>(Object source, out T result) where T : class
		{
			Guid iid = typeof(T).GUID;
			Object tmpResult;
			if (TryQueryInterface(source, iid, out tmpResult))
			{
				result = (T)tmpResult;
				return true;
			}
			result = null;
			return false;
		}

		internal static Boolean TryQueryInterface(Object source, Guid iid, out Object result)
		{
			if (source != null)
			{
				IntPtr pUnk = Marshal.GetIUnknownForObject(source);
				if (pUnk != IntPtr.Zero)
				{
					IntPtr ppv;
					Marshal.QueryInterface(pUnk, ref iid, out ppv);
					Marshal.Release(pUnk);

					if (ppv != IntPtr.Zero)
					{
						result = Marshal.GetObjectForIUnknown(ppv);
						Marshal.Release(ppv);

						return true;
					}
				}
			}

			result = null;
			return false;
		}

		#endregion

		#region RequestInterface

		internal static T RequestInterface<T>(Object source) where T : class
		{
			Guid iid = typeof(T).GUID;
			return (T)RequestInterface(source, iid);
		}

		internal static Object RequestInterface(Object source, Guid iid)
		{
			Object result;
			if (!TryRequestInterface(source, iid, out result))
			{
				throw new NotImplementedException();
			}
			return result;
		}

		internal static Boolean TryRequestInterface<T>(Object source, out T result) where T : class
		{
			Guid iid = typeof(T).GUID;
			Object tmpResult;
			if (TryRequestInterface(source, iid, out tmpResult))
			{
				result = (T)tmpResult;
				return true;
			}
			result = null;
			return false;
		}

		internal static Boolean TryRequestInterface(Object source, Guid iid, out Object result)
		{
			if (source != null)
			{
				IntPtr pUnk = Marshal.GetIUnknownForObject(source);
				if (pUnk != IntPtr.Zero)
				{
					Guid interfaceRequestorIID = typeof (nsIInterfaceRequestor).GUID;
					IntPtr pInterfaceRequestor;
					Marshal.QueryInterface(pUnk, ref interfaceRequestorIID, out pInterfaceRequestor);
					Marshal.Release(pUnk);

					if (pInterfaceRequestor != IntPtr.Zero)
					{
						var interfaceRequestor = (nsIInterfaceRequestor)Marshal.GetObjectForIUnknown(pInterfaceRequestor);
						Marshal.Release(pInterfaceRequestor);

						IntPtr ppv;
						interfaceRequestor.GetInterface(iid, out ppv);
						Marshal.ReleaseComObject(interfaceRequestor);

						if (ppv != IntPtr.Zero)
						{
							result = Marshal.GetObjectForIUnknown(ppv);
							Marshal.Release(ppv);

							return true;
						}
					}
				}
			}

			result = null;
			return false;
		}

		#endregion

		#region GetInterface

		internal static T GetInterface<T>(Object source)
		{
			Guid iid = typeof(T).GUID;
			return (T)GetInterface(source, iid);
		}

		internal static Object GetInterface(Object source, Guid iid)
		{
			Object result;
			if (!TryGetInterface(source, iid, out result))
			{
				throw new NotImplementedException();
			}
			return result;
		}

		internal static Boolean TryGetInterface<T>(Object source, out T result) where T : class
		{
			Guid iid = typeof(T).GUID;
			Object tmpResult;
			if (TryGetInterface(source, iid, out tmpResult))
			{
				result = (T)tmpResult;
				return true;
			}
			result = null;
			return false;
		}

		internal static Boolean TryGetInterface(Object source, Guid iid, out Object result)
		{
			if (source != null)
			{
				IntPtr pUnk = Marshal.GetIUnknownForObject(source);
				if (pUnk != IntPtr.Zero)
				{
					IntPtr ppv;
					Marshal.QueryInterface(pUnk, ref iid, out ppv);

					if (ppv == IntPtr.Zero)
					{
						Guid interfaceRequestorIID = typeof (nsIInterfaceRequestor).GUID;
						IntPtr pInterfaceRequestor;
						Marshal.QueryInterface(pUnk, ref interfaceRequestorIID, out pInterfaceRequestor);

						if (pInterfaceRequestor != IntPtr.Zero)
						{
							var interfaceRequestor = (nsIInterfaceRequestor)Marshal.GetObjectForIUnknown(pInterfaceRequestor);
							Marshal.Release(pInterfaceRequestor);

							interfaceRequestor.GetInterface(iid, out ppv);
							Marshal.ReleaseComObject(interfaceRequestor);
						}
					}

					Marshal.Release(pUnk);

					if (ppv != IntPtr.Zero)
					{
						result = Marshal.GetObjectForIUnknown(ppv);
						Marshal.Release(ppv);

						return true;
					}
				}
			}

			result = null;
			return false;
		}

		#endregion

		private static nsIDirectoryServiceProvider ms_AppFileLocationProvider;
		private static nsIServiceManager ms_ServiceManager;
		private static nsIComponentManager ms_ComponentManager;
		private static nsIComponentRegistrar ms_ComponentRegistrar;
	}
}
