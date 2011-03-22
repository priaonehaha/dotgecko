using System;
using System.Runtime.InteropServices;
using nsXPTMethodInfoPtr = System.IntPtr;
using nsXPTConstantPtr = System.IntPtr;
using nsXPTParamInfoPtr = System.IntPtr;
using nsXPTType = System.IntPtr;
using nsIIDPtrShared = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	/* The nsIInterfaceInfo public declaration. */
	/* this is NOT intended to be scriptable */
	[ComImport, Guid("215DBE04-94A7-11d2-BA58-00805F8A5DD7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIInterfaceInfo //: nsISupports
	{
		String Name { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		Guid InterfaceIID { [return: MarshalAs(UnmanagedType.LPStruct)] get; }

		Boolean IsScriptable();

		nsIInterfaceInfo Parent { get; }

		/**
		* These include counts for parent (and all ancestors).
		*/
		UInt16 MethodCount { get; }
		UInt16 ConstantCount { get; }

		/**
		* These include methods and constants for parent (and all ancestors).
		* 
		* These do *not* make copies ***explicit bending of XPCOM rules***.
		*/

		nsXPTMethodInfoPtr GetMethodInfo(UInt16 index);

		nsXPTMethodInfoPtr GetMethodInfoForName([MarshalAs(UnmanagedType.LPStr)] String methodName, out UInt16 index);

		nsXPTConstantPtr GetConstant(UInt16 index);


		/**
		* Get the interface information or iid associated with a param of some
		* method in this interface.
		*/

		nsIInterfaceInfo GetInfoForParam(UInt16 methodIndex, nsXPTParamInfoPtr param);

		[return: MarshalAs(UnmanagedType.LPStruct)]
		Guid GetIIDForParam(UInt16 methodIndex, nsXPTParamInfoPtr param);


		/**
		* These do *not* make copies ***explicit bending of XPCOM rules***.
		*/

		nsXPTType GetTypeForParam(UInt16 methodIndex, nsXPTParamInfoPtr param, UInt16 dimension);

		Byte GetSizeIsArgNumberForParam(UInt16 methodIndex, nsXPTParamInfoPtr param, UInt16 dimension);

		Byte GetLengthIsArgNumberForParam(UInt16 methodIndex, nsXPTParamInfoPtr param, UInt16 dimension);

		Byte GetInterfaceIsArgNumberForParam(UInt16 methodIndex, nsXPTParamInfoPtr param);

		Boolean IsIID([In] ref Guid IID);

		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetNameShared();
		[return: MarshalAs(UnmanagedType.LPStruct)]
		Guid GetIIDShared();

		Boolean IsFunction();

		Boolean HasAncestor([In] ref Guid iid);

		nsResult GetIIDForParamNoAlloc(UInt16 methodIndex, nsXPTParamInfoPtr param, [Out, MarshalAs(UnmanagedType.LPStruct)] out Guid iid);
	}
}
