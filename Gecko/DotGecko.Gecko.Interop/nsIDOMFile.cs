using System;
using System.Runtime.InteropServices;
using System.Text;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("0845E8AE-56BD-4F0E-962A-3B3E92638A0B"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMFile //: nsISupports
	{
		//fileName and fileSize are now deprecated attributes
		void GetFileName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		UInt64 FileSize { get; }

		void GetName([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
		UInt64 Size { get; }
		void GetType([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);

		void GetAsText([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String encoding,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval); // raises(FileException) on retrieval
		void GetAsDataURL([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);             // raises(FileException) on retrieval
		void GetAsBinary([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);              // raises(FileException) on retrieval
	}

	[ComImport, Guid("fc41a294-8c9a-4639-b8ed-7c04f8017ef6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDOMFile_1_9_2_BRANCH //: nsISupports
	{
		void GetMozFullPath([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder retval);
	}
}
