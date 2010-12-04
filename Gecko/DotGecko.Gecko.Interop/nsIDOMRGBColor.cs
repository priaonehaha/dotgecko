using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("6aff3102-320d-4986-9790-12316bb87cf9"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMRGBColor //: nsISupports
	{
		nsIDOMCSSPrimitiveValue Red { get; }
		nsIDOMCSSPrimitiveValue Green { get; }
		nsIDOMCSSPrimitiveValue Blue { get; }
	}
}
