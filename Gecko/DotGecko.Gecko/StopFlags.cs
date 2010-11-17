using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	[Flags]
	public enum StopFlags : uint
	{
		Network = nsIWebNavigationConstants.STOP_NETWORK,
		Content = nsIWebNavigationConstants.STOP_CONTENT,
		All = nsIWebNavigationConstants.STOP_ALL
	}
}
