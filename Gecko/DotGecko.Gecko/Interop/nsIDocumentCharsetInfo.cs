using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop
{
	// XXX doc me
	// XXX mark the right params "const"
	[ComImport, Guid("2d40b291-01e1-11d4-9d0e-0050040007b2"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsIDocumentCharsetInfo //: nsISupports
	{
		nsIAtom ForcedCharset { get; set; }

		Boolean ForcedDetector { get; set; }

		nsIAtom ParentCharset { get; set; }

		Int32 ParentCharsetSource { get; set; }
	}
}
