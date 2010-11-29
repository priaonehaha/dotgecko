using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("081e31e0-a144-11d3-8c7c-00609792278c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISecureBrowserUI //: nsISupports
	{
		void Init(nsIDOMWindow window);

		UInt32 State { get; }

		void GetTooltipText([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
	}
}
