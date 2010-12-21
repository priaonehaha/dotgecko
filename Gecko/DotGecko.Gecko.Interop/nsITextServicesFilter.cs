using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("5BEC321F-59AC-413a-A4AD-8A8D7C50A0D0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsITextServicesFilter //: nsISupports
	{
		/**
		 * Indicates whether the content node should be skipped by the iterator
		 *  @param aNode - node to skip
		 */
		Boolean Skip(nsIDOMNode aNode);
	}
}
