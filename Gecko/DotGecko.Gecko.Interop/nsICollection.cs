using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using nsQIResult = System.Object;

namespace DotGecko.Gecko.Interop
{
	[ComImport, Guid("83b6019c-cbc4-11d2-8cca-0060b0fc14a3"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsICollection : nsISerializable
	{
		#region nsISerializable Members

		new void Read(nsIObjectInputStream aInputStream);
		new void Write(nsIObjectOutputStream aOutputStream);

		#endregion

		UInt32 Count();
		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetElementAt(UInt32 index);
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		nsQIResult QueryElementAt(UInt32 index, [In] ref Guid uuid);
		void SetElementAt(UInt32 index, [MarshalAs(UnmanagedType.IUnknown)] nsISupports item);
		void AppendElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports item);
		void RemoveElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports item);

		nsIEnumerator Enumerate();

		void Clear();
	}
}
