using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using nsQIResult = System.Object;
using nsISupportsArrayEnumFunc = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	/*
	 * This entire interface is deprecated and should not be used.
	 * See nsIArray and nsIMutableArray for the new implementations.
	 *
	 * http://groups.google.com/groups?q=nsisupportsarray+group:netscape.public.mozilla.xpcom&hl=en&lr=&ie=UTF-8&oe=UTF-8&selm=3D779491.3050506%40netscape.com&rnum=2
	 * http://groups.google.com/groups?q=nsisupportsarray+group:netscape.public.mozilla.xpcom&hl=en&lr=&ie=UTF-8&oe=UTF-8&selm=al8412%245ab2%40ripley.netscape.com&rnum=8
	 */
	[ComImport, Guid("791eafa0-b9e6-11d1-8031-006008159b5a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface nsISupportsArray : nsICollection
	{
		#region nsISerializable Members

		new void Read(nsIObjectInputStream aInputStream);
		new void Write(nsIObjectOutputStream aOutputStream);

		#endregion

		#region nsICollection Members

		new UInt32 Count();
		[return: MarshalAs(UnmanagedType.IUnknown)]
		new nsISupports GetElementAt(UInt32 index);
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 1)]
		new nsQIResult QueryElementAt(UInt32 index, [In] ref Guid uuid);
		new void SetElementAt(UInt32 index, [MarshalAs(UnmanagedType.IUnknown)] nsISupports item);
		new void AppendElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports item);
		new void RemoveElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports item);

		new nsIEnumerator Enumerate();

		new void Clear();

		#endregion

		Boolean Equals(nsISupportsArray other);

		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports ElementAt(UInt32 aIndex);

		Int32 IndexOf([MarshalAs(UnmanagedType.IUnknown)] nsISupports aPossibleElement);
		Int32 IndexOfStartingAt([MarshalAs(UnmanagedType.IUnknown)] nsISupports aPossibleElement, UInt32 aStartIndex);
		Int32 LastIndexOf([MarshalAs(UnmanagedType.IUnknown)] nsISupports aPossibleElement);

		// xpcom-compatible versions
		Int32 GetIndexOf([MarshalAs(UnmanagedType.IUnknown)] nsISupports aPossibleElement);
		Int32 GetIndexOfStartingAt([MarshalAs(UnmanagedType.IUnknown)] nsISupports aPossibleElement, UInt32 aStartIndex);
		Int32 GetLastIndexOf([MarshalAs(UnmanagedType.IUnknown)] nsISupports aPossibleElement);

		Boolean InsertElementAt([MarshalAs(UnmanagedType.IUnknown)] nsISupports aElement, UInt32 aIndex);
		Boolean ReplaceElementAt([MarshalAs(UnmanagedType.IUnknown)] nsISupports aElement, UInt32 aIndex);

		Boolean RemoveElementAt(UInt32 aIndex);
		Boolean RemoveLastElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports aElement);

		// xpcom-compatible versions
		void DeleteLastElement([MarshalAs(UnmanagedType.IUnknown)] nsISupports aElement);
		void DeleteElementAt(UInt32 aIndex);

		Boolean AppendElements(nsISupportsArray aElements);

		void Compact();

		Boolean EnumerateForwards(nsISupportsArrayEnumFunc aFunc, IntPtr aData);

		Boolean EnumerateBackwards(nsISupportsArrayEnumFunc aFunc, IntPtr aData);

		nsISupportsArray Clone();

		Boolean MoveElement(Int32 aFrom, Int32 aTo);

		Boolean InsertElementsAt(nsISupportsArray aOther, UInt32 aIndex);

		Boolean RemoveElementsAt(UInt32 aIndex, UInt32 aCount);

		Boolean SizeTo(Int32 aSize);
	}
}
