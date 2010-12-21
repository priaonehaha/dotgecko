using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("13a1b39e-72e5-442d-aa73-5905ffaf837b"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIPluginTag //: nsISupports
	{
		void GetDescription([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void GetFilename([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void GetVersion([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder retval);
		Boolean Disabled { get; set; }
		Boolean Blocklisted { get; set; }
	}
}
