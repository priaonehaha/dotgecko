using System;

namespace DotGecko.Gecko.Interop
{
	internal static class nsIURIExtensions
	{
		internal static Uri ToUri(this nsIURI nsUri)
		{
			if (nsUri == null)
			{
				return null;
			}

			String spec = XpcomStringHelper.Get(nsUri.GetSpec);
			if (String.IsNullOrEmpty(spec))
			{
				return null;
			}

			Uri result;
			return Uri.TryCreate(spec, UriKind.Absolute, out result) ? result : null;
		}

		internal static nsIURI ToNsUri(this Uri uri)
		{
			if (uri == null)
			{
				return null;
			}
			var ioService = XpcomHelper.GetService<nsIIOService>(Xpcom.NS_IOSERVICE_CONTRACTID);
			nsIURI nsUri = ioService.NewURI(uri.AbsoluteUri, null, null);
			return nsUri;
		}
	}
}
