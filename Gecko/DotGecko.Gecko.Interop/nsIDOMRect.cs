using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("71735f62-ac5c-4236-9a1f-5ffb280d531c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMRect //: nsISupports
	{
		nsIDOMCSSPrimitiveValue Top { get; }
		nsIDOMCSSPrimitiveValue Right { get; }
		nsIDOMCSSPrimitiveValue Bottom { get; }
		nsIDOMCSSPrimitiveValue Left { get; }
	}
}
