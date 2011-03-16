using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("8792d77e-1dd2-11b2-ac7f-9bc9be4f2916"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIJSSubScriptLoader //: nsISupports
	{
		/**
		 * This method should only be called from JS!
		 * In JS, the signature looks like:
		 * rv loadSubScript (url [, obj] [, charset]);
		 * @param url the url if the sub-script, it MUST be either a file:,
		 *            resource:, or chrome: url, and MUST be local.
		 * @param obj an optional object to evaluate the script onto, it
		 *            defaults to the global object of the caller.
		 * @param charset optionally specifies the character encoding of
		 *                the file. If absent, the file is interpreted
				  as ASCII.
		 * @retval rv the value returned by the sub-script
		 */
		void LoadSubScript([MarshalAs(UnmanagedType.LPWStr)] String url);
	}
}
