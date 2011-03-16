using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("5822776a-049c-4de7-adb6-dd9efc39d082"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMBlob //: nsISupports
	{
		UInt64 Size { get; }
		void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);

		nsIDOMBlob Slice(UInt64 start,
						 UInt64 length,
						 [Optional, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String contentType);

		nsIInputStream InternalStream { get; }
		// The caller is responsible for releasing the internalUrl from the
		// moz-filedata: protocol handler
		void GetInternalUrl(nsIPrincipal principal, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
	}

	[ComImport, Guid("ae1405b0-e411-481e-9606-b29ec7982687"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMFile : nsIDOMBlob
	{
		#region nsIDOMBlob Members

		new UInt64 Size { get; }
		new void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		new nsIDOMBlob Slice(UInt64 start, UInt64 length, [Optional, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String contentType);
		new nsIInputStream InternalStream { get; }
		new void GetInternalUrl(nsIPrincipal principal, [In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);

		#endregion

		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		void GetMozFullPath([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);

		// This performs no security checks!
		void GetMozFullPathInternal([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);

		// These are all deprecated and not in spec. Will be removed in a future
		// release
		void GetFileName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		UInt64 FileSize { get; }
		void GetAsText([MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String encoding,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval); // raises(FileException) on retrieval
		void GetAsDataURL([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);             // raises(FileException) on retrieval
		void GetAsBinary([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);              // raises(FileException) on retrieval
	}
}
