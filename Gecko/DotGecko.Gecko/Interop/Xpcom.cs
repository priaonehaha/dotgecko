using System;
using System.IO;

namespace DotGecko.Gecko.Interop
{
	public static partial class Xpcom
	{
		public static void InitEmbedding(String geckoPath)
		{
			if (geckoPath == null)
			{
				throw new ArgumentNullException("geckoPath");
			}
			if (!File.Exists(Path.Combine(geckoPath, xpcom)))
			{
				throw new ArgumentException("Invalid path", "geckoPath");
			}

			const String pathEnvVar = "PATH";
			Environment.SetEnvironmentVariable(
				pathEnvVar,
				Environment.ExpandEnvironmentVariables(String.Format("{0};%{1}%", geckoPath, pathEnvVar)),
				EnvironmentVariableTarget.Process);

			nsILocalFile binDirectory;
			using (var path = new nsACString())
			{
				path.Assign(geckoPath);
				NS_NewNativeLocalFile(path, true, out binDirectory);
			}
			nsIDirectoryServiceProvider appFileLocationProvider = null;

			UInt32 nsresult = NS_InitXPCOM2(out ms_ServiceManager, binDirectory, appFileLocationProvider);
			if (nsresult != 0)
			{
				throw new ApplicationException(String.Format("Failed on NS_InitXPCOM2: 0x{0:X8}", nsresult));
			}

			nsresult = NS_GetComponentManager(out ms_ComponentManager);
			if (nsresult != 0)
			{
				throw new ApplicationException(String.Format("Failed on NS_GetComponentManager: 0x{0:X8}", nsresult));
			}

			nsresult = NS_GetComponentRegistrar(out ms_ComponentRegistrar);
			if (nsresult != 0)
			{
				throw new ApplicationException(String.Format("Failed on NS_GetComponentRegistrar: 0x{0:X8}", nsresult));
			}
		}

		public static void TermEmbedding()
		{
			UInt32 nsresult = NS_ShutdownXPCOM(ServiceManager);
			if (nsresult != 0)
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

		internal static T GetService<T>(Guid classID)
		{
			Guid aIID = typeof(T).GUID;
			return (T)ServiceManager.GetService(ref classID, ref aIID);
		}

		internal static T GetService<T>(String contractID)
		{
			Guid aIID = typeof(T).GUID;
			return (T)ServiceManager.GetServiceByContractID(contractID, ref aIID);
		}

		internal static T CreateInstance<T>(Guid classID)
		{
			Guid aIID = typeof(T).GUID;
			return (T)ComponentManager.CreateInstance(ref classID, null, ref aIID);
		}

		internal static T CreateInstance<T>(String contractID)
		{
			Guid aIID = typeof(T).GUID;
			return (T)ComponentManager.CreateInstanceByContractID(contractID, null, ref aIID);
		}

		private static nsIServiceManager ms_ServiceManager;
		private static nsIComponentManager ms_ComponentManager;
		private static nsIComponentRegistrar ms_ComponentRegistrar;
	}
}
