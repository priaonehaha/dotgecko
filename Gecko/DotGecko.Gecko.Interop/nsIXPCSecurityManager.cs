using System;
using System.Runtime.InteropServices;
using nsISupports = System.Object;
using JSContextPtr = System.IntPtr;
using JSObjectPtr = System.IntPtr;
using JSStackFramePtr = System.IntPtr;
using nsAXPCNativeCallContextPtr = System.IntPtr;
using jsid = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public static class nsIXPCSecurityManagerConstants
	{
		/**
		 * These flags are used when calling nsIXPConnect::SetSecurityManager
		 */
		public const UInt32 HOOK_CREATE_WRAPPER = 1 << 0;
		public const UInt32 HOOK_CREATE_INSTANCE = 1 << 1;
		public const UInt32 HOOK_GET_SERVICE = 1 << 2;
		public const UInt32 HOOK_CALL_METHOD = 1 << 3;
		public const UInt32 HOOK_GET_PROPERTY = 1 << 4;
		public const UInt32 HOOK_SET_PROPERTY = 1 << 5;

		public const UInt32 HOOK_ALL = HOOK_CREATE_WRAPPER |
									   HOOK_CREATE_INSTANCE |
									   HOOK_GET_SERVICE |
									   HOOK_CALL_METHOD |
									   HOOK_GET_PROPERTY |
									   HOOK_SET_PROPERTY;

		/*
		 * Used for aAction below
		 */
		public const UInt32 ACCESS_CALL_METHOD = 0;
		public const UInt32 ACCESS_GET_PROPERTY = 1;
		public const UInt32 ACCESS_SET_PROPERTY = 2;
	}

	[ComImport, Guid("31431440-f1ce-11d2-985a-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXPCSecurityManager //: nsISupports
	{
		/**
		* For each of these hooks returning NS_OK means 'let the action continue'.
		* Returning an error code means 'veto the action'. XPConnect will return
		* JS_FALSE to the js engine if the action is vetoed. The implementor of this
		* interface is responsible for setting a JS exception into the JSContext
		* if that is appropriate.
		*/

		void CanCreateWrapper(JSContextPtr aJSContext,
							  [In] ref Guid aIID,
							  [MarshalAs(UnmanagedType.IUnknown)] nsISupports aObj,
							  nsIClassInfo aClassInfo,
							  ref IntPtr aPolicy);

		void CanCreateInstance(JSContextPtr aJSContext,
							   [In] ref Guid aCID);

		void CanGetService(JSContextPtr aJSContext,
						   [In] ref Guid aCID);

		void CanAccess(UInt32 aAction,
					   nsAXPCNativeCallContextPtr aCallContext,
					   JSContextPtr aJSContext,
					   JSObjectPtr aJSObject,
					   [MarshalAs(UnmanagedType.IUnknown)] nsISupports aObj,
					   nsIClassInfo aClassInfo,
					   jsid aName,
					   ref IntPtr aPolicy);
	}
}
