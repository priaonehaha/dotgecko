using System;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop.JavaScript;
using nsISupports = System.Object;
using JSContextPtr = System.IntPtr;
using JSTracerPtr = System.IntPtr;
using JSObjectPtr = System.IntPtr;
using JSValPtr = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	public static class nsIXPCScriptableConstants
	{
		/* bitflags used for 'flags' (only 32 bits available!) */

		public const UInt32 WANT_PRECREATE = 1 << 0;
		public const UInt32 WANT_CREATE = 1 << 1;
		public const UInt32 WANT_POSTCREATE = 1 << 2;
		public const UInt32 WANT_ADDPROPERTY = 1 << 3;
		public const UInt32 WANT_DELPROPERTY = 1 << 4;
		public const UInt32 WANT_GETPROPERTY = 1 << 5;
		public const UInt32 WANT_SETPROPERTY = 1 << 6;
		public const UInt32 WANT_ENUMERATE = 1 << 7;
		public const UInt32 WANT_NEWENUMERATE = 1 << 8;
		public const UInt32 WANT_NEWRESOLVE = 1 << 9;
		public const UInt32 WANT_CONVERT = 1 << 10;
		public const UInt32 WANT_FINALIZE = 1 << 11;
		public const UInt32 WANT_CHECKACCESS = 1 << 12;
		public const UInt32 WANT_CALL = 1 << 13;
		public const UInt32 WANT_CONSTRUCT = 1 << 14;
		public const UInt32 WANT_HASINSTANCE = 1 << 15;
		public const UInt32 WANT_TRACE = 1 << 16;
		public const UInt32 USE_JSSTUB_FOR_ADDPROPERTY = 1 << 17;
		public const UInt32 USE_JSSTUB_FOR_DELPROPERTY = 1 << 18;
		public const UInt32 USE_JSSTUB_FOR_SETPROPERTY = 1 << 19;
		public const UInt32 DONT_ENUM_STATIC_PROPS = 1 << 20;
		public const UInt32 DONT_ENUM_QUERY_INTERFACE = 1 << 21;
		public const UInt32 DONT_ASK_INSTANCE_FOR_SCRIPTABLE = 1 << 22;
		public const UInt32 CLASSINFO_INTERFACES_ONLY = 1 << 23;
		public const UInt32 ALLOW_PROP_MODS_DURING_RESOLVE = 1 << 24;
		public const UInt32 ALLOW_PROP_MODS_TO_PROTOTYPE = 1 << 25;
		public const UInt32 DONT_SHARE_PROTOTYPE = 1 << 26;
		public const UInt32 DONT_REFLECT_INTERFACE_NAMES = 1 << 27;
		public const UInt32 WANT_EQUALITY = 1 << 28;
		public const UInt32 WANT_OUTER_OBJECT = 1 << 29;

		// The high order bit is RESERVED for consumers of these flags. 
		// No implementor of this interface should ever return flags 
		// with this bit set.
		public const UInt32 RESERVED = 1u << 31;
	}

	/**
	 * Note: This is not really an XPCOM interface.  For example, callers must
	 * guarantee that they set the *_retval of the various methods that return a
	 * boolean to PR_TRUE before making the call.  Implementations may skip writing
	 * to *_retval unless they want to return PR_FALSE.
	 */
	[ComImport, Guid("a40ce52e-2d8c-400f-9af2-f8784a656070"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXPCScriptable //: nsISupports
	{
		String ClassName { [return: MarshalAs(UnmanagedType.LPStr)] get; }
		UInt32 ScriptableFlags { get; }

		void PreCreate([MarshalAs(UnmanagedType.IUnknown)] nsISupports nativeObj, JSContextPtr cx,
					   JSObjectPtr globalObj, out JSObjectPtr parentObj);

		void Create(nsIXPConnectWrappedNative wrapper,
					  JSContextPtr cx, JSObjectPtr obj);

		void PostCreate(nsIXPConnectWrappedNative wrapper,
						  JSContextPtr cx, JSObjectPtr obj);

		Boolean AddProperty(nsIXPConnectWrappedNative wrapper,
						   JSContextPtr cx, JSObjectPtr obj, JsId id,
						   JSValPtr vp);

		Boolean DelProperty(nsIXPConnectWrappedNative wrapper,
						   JSContextPtr cx, JSObjectPtr obj, JsId id,
						   JSValPtr vp);

		// The returnCode should be set to NS_SUCCESS_I_DID_SOMETHING if
		// this method does something.
		Boolean GetProperty(nsIXPConnectWrappedNative wrapper,
						   JSContextPtr cx, JSObjectPtr obj, JsId id,
						   JSValPtr vp);

		// The returnCode should be set to NS_SUCCESS_I_DID_SOMETHING if
		// this method does something.
		Boolean SetProperty(nsIXPConnectWrappedNative wrapper,
						   JSContextPtr cx, JSObjectPtr obj, JsId id,
						   JSValPtr vp);

		Boolean Enumerate(nsIXPConnectWrappedNative wrapper,
						 JSContextPtr cx, JSObjectPtr obj);

		Boolean NewEnumerate(nsIXPConnectWrappedNative wrapper,
							JSContextPtr cx, JSObjectPtr obj,
							UInt32 enum_op, JSValPtr statep, out JsId idp);

		Boolean NewResolve(nsIXPConnectWrappedNative wrapper,
						  JSContextPtr cx, JSObjectPtr obj, JsId id,
						  UInt32 flags, out JSObjectPtr objp);

		Boolean Convert(nsIXPConnectWrappedNative wrapper,
					   JSContextPtr cx, JSObjectPtr obj,
					   UInt32 type, JSValPtr vp);

		void Finalize(nsIXPConnectWrappedNative wrapper,
						JSContextPtr cx, JSObjectPtr obj);

		Boolean CheckAccess(nsIXPConnectWrappedNative wrapper,
						   JSContextPtr cx, JSObjectPtr obj, JsId id,
						   UInt32 mode, JSValPtr vp);

		Boolean Call(nsIXPConnectWrappedNative wrapper,
					JSContextPtr cx, JSObjectPtr obj,
					UInt32 argc, JSValPtr argv, JSValPtr vp);

		Boolean Construct(nsIXPConnectWrappedNative wrapper,
						 JSContextPtr cx, JSObjectPtr obj,
						 UInt32 argc, JSValPtr argv, JSValPtr vp);

		Boolean HasInstance(nsIXPConnectWrappedNative wrapper,
						   JSContextPtr cx, JSObjectPtr obj,
						   [In] ref JsVal val, out Boolean bp);

		void Trace(nsIXPConnectWrappedNative wrapper,
				   JSTracerPtr trc, JSObjectPtr obj);

		Boolean Equality(nsIXPConnectWrappedNative wrapper,
						JSContextPtr cx, JSObjectPtr obj, [In] ref JsVal val);

		JSObjectPtr OuterObject(nsIXPConnectWrappedNative wrapper,
								JSContextPtr cx, JSObjectPtr obj);

		void PostCreatePrototype(JSContextPtr cx, JSObjectPtr proto);
	}
}
