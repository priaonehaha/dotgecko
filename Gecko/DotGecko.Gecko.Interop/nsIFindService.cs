using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("5060b801-340e-11d5-be5b-b3e063ec6a3c"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIFindService //: nsISupports
	{
		/*
		 * The sole purpose of the Find service is to store globally the
		 * last used Find settings
		 *
		 */

		void GetSearchString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetSearchString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		void GetReplaceString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);
		void SetReplaceString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);

		Boolean FindBackwards { get; set; }
		Boolean WrapFind { get; set; }
		Boolean EntireWord { get; set; }
		Boolean MatchCase { get; set; }
	}
}
