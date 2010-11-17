using System;

namespace DotGecko.Gecko.Interop
{
	internal static partial class Xpcom
	{
		private sealed class DirectoryServiceProvider : nsIDirectoryServiceProvider
		{
			internal DirectoryServiceProvider(IAppFileLocation appFileLocation)
			{
				m_AppFileLocation = appFileLocation;
			}

			private IAppFileLocation AppFileLocation
			{
				get { return m_AppFileLocation; }
			}

			nsIFile nsIDirectoryServiceProvider.GetFile(String prop, out Boolean persistent)
			{
				persistent = true;

				if (AppFileLocation == null)
				{
					return null;
				}

				String location;
				switch (prop)
				{
					case NS_APP_USER_PROFILE_50_DIR:
						location = AppFileLocation.ProfileDirectory;
						break;
					default:
						location = null;
						break;
				}

				return !String.IsNullOrWhiteSpace(location) ? NewNativeLocalFile(location) : null;
			}

			private readonly IAppFileLocation m_AppFileLocation;
		}
	}
}
