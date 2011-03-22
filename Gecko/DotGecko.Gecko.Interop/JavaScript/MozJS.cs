using System;
using System.Runtime.InteropServices;
using JSContext = System.IntPtr;
using JSRuntime = System.IntPtr;
using JSType = System.IntPtr;
using JSObject = System.IntPtr;
using JSString = System.IntPtr;
using JSFunction = System.IntPtr;
using JSArgumentFormatter = System.IntPtr;
using JSContextCallback = System.IntPtr;
using JSVersion = System.IntPtr;
using jsval = System.IntPtr;
using JSCompartmentCallback = System.IntPtr;
using JSWrapObjectCallback = System.IntPtr;
using JSPreWrapCallback = System.IntPtr;
using JSCrossCompartmentCall = System.IntPtr;
using JSScript = System.IntPtr;
using JSCompartment = System.IntPtr;

namespace DotGecko.Gecko.Interop.JavaScript
{
	public static class MozJS
	{
		/*
		 * Microseconds since the epoch, midnight, January 1, 1970 UTC.  See the
		 * comment in jstypes.h regarding safe int64 usage.
		 */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Int64 JS_Now();

		/* Don't want to export data, so provide accessors for non-inline jsvals. */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern jsval JS_GetNaNValue(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern jsval JS_GetNegativeInfinityValue(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern jsval JS_GetPositiveInfinityValue(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern jsval JS_GetEmptyStringValue(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSString JS_GetEmptyString(JSRuntime rt);

		/*
		 * Format is a string of the following characters (spaces are insignificant),
		 * specifying the tabulated type conversions:
		 *
		 *   b      JSBool          Boolean
		 *   c      uint16/jschar   ECMA uint16, Unicode char
		 *   i      int32           ECMA int32
		 *   u      uint32          ECMA uint32
		 *   j      int32           Rounded int32 (coordinate)
		 *   d      jsdouble        IEEE double
		 *   I      jsdouble        Integral IEEE double
		 *   S      JSString *      Unicode string, accessed by a JSString pointer
		 *   W      jschar *        Unicode character vector, 0-terminated (W for wide)
		 *   o      JSObject *      Object reference
		 *   f      JSFunction *    Function private
		 *   v      jsval           Argument value (no conversion)
		 *   *      N/A             Skip this argument (no vararg)
		 *   /      N/A             End of required arguments
		 *
		 * The variable argument list after format must consist of &b, &c, &s, e.g.,
		 * where those variables have the types given above.  For the pointer types
		 * char *, JSString *, and JSObject *, the pointed-at memory returned belongs
		 * to the JS runtime, not to the calling native code.  The runtime promises
		 * to keep this memory valid so long as argv refers to allocated stack space
		 * (so long as the native function is active).
		 *
		 * Fewer arguments than format specifies may be passed only if there is a /
		 * in format after the last required argument specifier and argc is at least
		 * the number of required arguments.  More arguments than format specifies
		 * may be passed without error; it is up to the caller to deal with trailing
		 * unconverted arguments.
		 */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ConvertArguments(JSContext cx, UInt32 argc, jsval argv, [MarshalAs(UnmanagedType.LPStr)] String format /*, ...*/);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ConvertArgumentsVA(JSContext cx, UInt32 argc, jsval argv, [MarshalAs(UnmanagedType.LPStr)] String format, IntPtr ap);

		/*
		 * Add and remove a format string handler for JS_{Convert,Push}Arguments{,VA}.
		 * The handler function has this signature (see jspubtd.h):
		 *
		 *   JSBool MyArgumentFormatter(JSContext *cx, const char *format,
		 *                              JSBool fromJS, jsval **vpp, va_list *app);
		 *
		 * It should return true on success, and return false after reporting an error
		 * or detecting an already-reported error.
		 *
		 * For a given format string, for example "AA", the formatter is called from
		 * JS_ConvertArgumentsVA like so:
		 *
		 *   formatter(cx, "AA...", JS_TRUE, &sp, &ap);
		 *
		 * sp points into the arguments array on the JS stack, while ap points into
		 * the stdarg.h va_list on the C stack.  The JS_TRUE passed for fromJS tells
		 * the formatter to convert zero or more jsvals at sp to zero or more C values
		 * accessed via pointers-to-values at ap, updating both sp (via *vpp) and ap
		 * (via *app) to point past the converted arguments and their result pointers
		 * on the C stack.
		 *
		 * When called from JS_PushArgumentsVA, the formatter is invoked thus:
		 *
		 *   formatter(cx, "AA...", JS_FALSE, &sp, &ap);
		 *
		 * where JS_FALSE for fromJS means to wrap the C values at ap according to the
		 * format specifier and store them at sp, updating ap and sp appropriately.
		 *
		 * The "..." after "AA" is the rest of the format string that was passed into
		 * JS_{Convert,Push}Arguments{,VA}.  The actual format trailing substring used
		 * in each Convert or PushArguments call is passed to the formatter, so that
		 * one such function may implement several formats, in order to share code.
		 *
		 * Remove just forgets about any handler associated with format.  Add does not
		 * copy format, it points at the string storage allocated by the caller, which
		 * is typically a string constant.  If format is in dynamic storage, it is up
		 * to the caller to keep the string alive until Remove is called.
		 */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_AddArgumentFormatter(JSContext cx, [MarshalAs(UnmanagedType.LPStr)] String format, JSArgumentFormatter formatter);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_RemoveArgumentFormatter(JSContext cx, [MarshalAs(UnmanagedType.LPStr)] String format);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ConvertValue(JSContext cx, jsval v, JSType type, ref jsval vp);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ValueToObject(JSContext cx, jsval v, ref JSObject objp);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSFunction JS_ValueToFunction(JSContext cx, jsval v);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSFunction JS_ValueToConstructor(JSContext cx, jsval v);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSString JS_ValueToString(JSContext cx, jsval v);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSString JS_ValueToSource(JSContext cx, jsval v);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ValueToNumber(JSContext cx, jsval v, ref Double dp);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_DoubleIsInt32(Double d, ref Int32 ip);

		/*
		 * Convert a value to a number, then to an int32, according to the ECMA rules
		 * for ToInt32.
		 */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ValueToECMAInt32(JSContext cx, jsval v, ref Int32 ip);

		/*
		 * Convert a value to a number, then to a uint32, according to the ECMA rules
		 * for ToUint32.
		 */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ValueToECMAUint32(JSContext cx, jsval v, ref UInt32 ip);

		/*
		 * Convert a value to a number, then to an int32 if it fits by rounding to
		 * nearest; but failing with an error report if the double is out of range
		 * or unordered.
		 */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ValueToInt32(JSContext cx, jsval v, ref Int32 ip);

		/*
		 * ECMA ToUint16, for mapping a jsval to a Unicode point.
		 */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ValueToUint16(JSContext cx, jsval v, ref UInt16 ip);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_ValueToBoolean(JSContext cx, jsval v, ref Boolean bp);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSType JS_TypeOfValue(JSContext cx, jsval v);

		[DllImport(mozjs, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static extern String JS_GetTypeName(JSContext cx, JSType type);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_StrictlyEqual(JSContext cx, jsval v1, jsval v2, ref Boolean equal);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_SameValue(JSContext cx, jsval v1, jsval v2, ref Boolean same);

		/************************************************************************/

		/*
		 * Initialization, locking, contexts, and memory allocation.
		 *
		 * It is important that the first runtime and first context be created in a
		 * single-threaded fashion, otherwise the behavior of the library is undefined.
		 * See: http://developer.mozilla.org/en/docs/Category:JSAPI_Reference
		 */
		[DllImport(mozjs, EntryPoint = "JS_Init", ExactSpelling = true)]
		public static extern JSRuntime JS_NewRuntime(UInt32 maxbytes);

		[DllImport(mozjs, EntryPoint = "JS_Finish", ExactSpelling = true)]
		public static extern void JS_DestroyRuntime(JSRuntime rt);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_ShutDown();

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_BeginRequest(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_EndRequest(JSContext cx);

		/* Yield to pending GC operations, regardless of request depth */
		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_YieldRequest(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Int32 JS_SuspendRequest(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_ResumeRequest(JSContext cx, Int32 saveDepth);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_IsInRequest(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_Lock(JSRuntime rt);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_Unlock(JSRuntime rt);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSContextCallback JS_SetContextCallback(JSRuntime rt, JSContextCallback cxCallback);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSContext JS_NewContext(JSRuntime rt, Int32 stackChunkSize);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_DestroyContext(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_DestroyContextNoGC(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_DestroyContextMaybeGC(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_GetContextPrivate(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_SetContextPrivate(JSContext cx, IntPtr data);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSRuntime JS_GetRuntime(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSContext JS_ContextIterator(JSRuntime rt, ref JSContext iterp);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSVersion JS_GetVersion(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSVersion JS_SetVersion(JSContext cx, JSVersion version);

		[DllImport(mozjs, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static extern String JS_VersionToString(JSVersion version);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSVersion JS_StringToVersion([MarshalAs(UnmanagedType.LPStr)] String str);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern UInt32 JS_GetOptions(JSContext cx);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern UInt32 JS_SetOptions(JSContext cx, UInt32 options);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern UInt32 JS_ToggleOptions(JSContext cx, UInt32 options);

		[DllImport(mozjs, ExactSpelling = true)]
		[return: MarshalAs(UnmanagedType.LPStr)]
		public static extern String JS_GetImplementationVersion();

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSCompartmentCallback JS_SetCompartmentCallback(JSRuntime rt, JSCompartmentCallback callback);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSWrapObjectCallback JS_SetWrapObjectCallbacks(JSRuntime rt, JSWrapObjectCallback callback, JSPreWrapCallback precallback);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSCrossCompartmentCall JS_EnterCrossCompartmentCall(JSContext cx, JSObject target);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSCrossCompartmentCall JS_EnterCrossCompartmentCallScript(JSContext cx, JSScript target);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_LeaveCrossCompartmentCall(JSCrossCompartmentCall call);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_SetCompartmentPrivate(JSContext cx, JSCompartment compartment, IntPtr data);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern void JS_GetCompartmentPrivate(JSContext cx, JSCompartment compartment);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_WrapObject(JSContext cx, ref JSObject objp);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern Boolean JS_WrapValue(JSContext cx, jsval vp);

		[DllImport(mozjs, ExactSpelling = true)]
		public static extern JSObject JS_TransplantObject(JSContext cx, JSObject origobj, JSObject target);


		private const String mozjs = "mozjs.dll";
	}
}
