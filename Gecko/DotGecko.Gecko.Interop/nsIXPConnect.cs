using System;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop.JavaScript;
using nsISupports = System.Object;
using nsQIResult = System.Object;
using JSContextPtr = System.IntPtr;
using JSClassPtr = System.IntPtr;
using JSObjectPtr = System.IntPtr;
using JSValPtr = System.IntPtr;
using JSValConstPtr = System.IntPtr;
using JSPropertyOp = System.IntPtr;
using JSEqualityOp = System.IntPtr;
using nsScriptObjectTracerPtr = System.IntPtr;
using nsCCTraversalCallbackRef = System.IntPtr;
using nsAXPCNativeCallContextPtr = System.IntPtr;
using nsWrapperCachePtr = System.IntPtr;

namespace DotGecko.Gecko.Interop
{
	/* The core XPConnect public interfaces. */

	[ComImport, Guid("8916a320-d118-11d3-8f3a-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXPConnectJSObjectHolder //: nsISupports
	{
		JSObjectPtr JSObject { get; }
	}

	[ComImport, Guid("f819a95a-6ab5-4a02-bda6-32861e859581"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXPConnectWrappedNative : nsIXPConnectJSObjectHolder
	{
		#region nsIXPConnectJSObjectHolder Members

		new JSObjectPtr JSObject { get; }

		#endregion

		/* attribute 'JSObject' inherited from nsIXPConnectJSObjectHolder */
		nsISupports Native { [return: MarshalAs(UnmanagedType.IUnknown)] get; }
		JSObjectPtr JSObjectPrototype { get; }

		/**
		 * These are here as an aid to nsIXPCScriptable implementors
		 */

		nsIXPConnect XPConnect { get; }
		nsIInterfaceInfo FindInterfaceWithMember(JsId nameID);
		nsIInterfaceInfo FindInterfaceWithName(JsId nameID);

		void DebugDump(Int16 depth);

		void RefreshPrototype();
		/* 
		 * This returns a pointer into the instance and care should be taken
		 * to make sure the pointer is not kept past the life time of the
		 * object it points into.
		 */
		IntPtr GetSecurityInfoAddress();
	}

	[ComImport, Guid("BED52030-BCA6-11d2-BA79-00805F8A5DD7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXPConnectWrappedJS : nsIXPConnectJSObjectHolder
	{
		#region nsIXPConnectJSObjectHolder Members

		new JSObjectPtr JSObject { get; }

		#endregion

		/* attribute 'JSObject' inherited from nsIXPConnectJSObjectHolder */
		nsIInterfaceInfo InterfaceInfo { get; }
		Guid InterfaceIID { [return: MarshalAs(UnmanagedType.LPStruct)] get; }

		void DebugDump(Int16 depth);

		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 0)]
		nsQIResult AggregatedQueryInterface([In] ref Guid uuid);
	}

	/***************************************************************************/

	/**
	 * This is a sort of a placeholder interface. It is not intended to be
	 * implemented. It exists to give the nsIXPCSecurityManager an iid on
	 * which to gate a specific activity in XPConnect.
	 *
	 * That activity is...
	 *
	 * When JavaScript code uses a component that is itself implemented in
	 * JavaScript then XPConnect will build a wrapper rather than directly
	 * expose the JSObject of the component. This allows components implemented
	 * in JavaScript to 'look' just like any other xpcom component (from the
	 * perspective of the JavaScript caller). This insulates the component from
	 * the caller and hides any properties or methods that are not part of the
	 * interface as declared in xpidl. Usually this is a good thing.
	 *
	 * However, in some cases it is useful to allow the JS caller access to the
	 * JS component's underlying implementation. In order to facilitate this
	 * XPConnect supports the 'wrappedJSObject' property. The caller code can do:
	 *
	 * // 'foo' is some xpcom component (that might be implemented in JS).
	 * try {
	 *   var bar = foo.wrappedJSObject;
	 *   if(bar) {
	 *      // bar is the underlying JSObject. Do stuff with it here.
	 *   }
	 * } catch(e) {
	 *   // security exception?
	 * }
	 *
	 * Recall that 'foo' above is an XPConnect wrapper, not the underlying JS
	 * object. The property get "foo.wrappedJSObject" will only succeed if three
	 * conditions are met:
	 *
	 * 1) 'foo' really is an XPConnect wrapper around a JSObject.
	 * 2) The underlying JSObject actually implements a "wrappedJSObject"
	 *    property that returns a JSObject. This is called by XPConnect. This
	 *    restriction allows wrapped objects to only allow access to the underlying
	 *    JSObject if they choose to do so. Ususally this just means that 'foo'
	 *    would have a property tht looks like:
	 *       this.wrappedJSObject = this.
	 * 3) The implemementation of nsIXPCSecurityManager (if installed) allows
	 *    a property get on the interface below. Although the JSObject need not
	 *    implement 'nsIXPCWrappedJSObjectGetter', XPConnect will ask the
	 *    security manager if it is OK for the caller to access the only method
	 *    in nsIXPCWrappedJSObjectGetter before allowing the activity. This fits
	 *    in with the security manager paradigm and makes control over accessing
	 *    the property on this interface the control factor for getting the
	 *    underlying wrapped JSObject of a JS component from JS code.
	 *
	 * Notes:
	 *
	 * a) If 'foo' above were the underlying JSObject and not a wrapper at all,
	 *    then this all just works and XPConnect is not part of the picture at all.
	 * b) One might ask why 'foo' should not just implement an interface through
	 *    which callers might get at the underlying object. There are three reasons:
	 *   i)   XPConnect would still have to do magic since JSObject is not a
	 *        scriptable type.
	 *   ii)  JS Components might use aggregation (like C++ objects) and have
	 *        different JSObjects for different interfaces 'within' an aggregate
	 *        object. But, using an additional interface only allows returning one
	 *        underlying JSObject. However, this allows for the possibility that
	 *        each of the aggregte JSObjects could return something different.
	 *        Note that one might do: this.wrappedJSObject = someOtherObject;
	 *   iii) Avoiding the explicit interface makes it easier for both the caller
	 *        and the component.
	 *
	 *  Anyway, some future implementation of nsIXPCSecurityManager might want
	 *  do special processing on 'nsIXPCSecurityManager::CanGetProperty' when
	 *  the interface id is that of nsIXPCWrappedJSObjectGetter.
	 */

	[ComImport, Guid("254bb2e0-6439-11d4-8fe0-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXPCWrappedJSObjectGetter //: nsISupports
	{
		nsISupports NeverCalled { [return: MarshalAs(UnmanagedType.IUnknown)] get; }
	}

	/***************************************************************************/

	/*
	 * This interface is implemented by outside code and registered with xpconnect
	 * via nsIXPConnect::setFunctionThisTranslator.
	 *
	 * The reason this exists is to support calls to JavaScript event callbacks
	 * needed by the DOM via xpconnect from C++ code.
	 *
	 * We've added support for wrapping JS function objects as xpcom interfaces
	 * by declaring the given interface as a [function] interface. However, to
	 * support the requirements of JS event callbacks we need to call the JS
	 * function with the 'this' set as the JSObject for which the event is being
	 * fired; e.g. a form node.
	 *
	 * We've decided that for all cases we care about the appropriate 'this' object
	 * can be derived from the first param in the call to the callback. In the
	 * event handler case the first param is an event object.
	 *
	 * Though we can't change all the JS code so that it would setup its own 'this',
	 * we can add plugin 'helper' support to xpconnect. And that is what we have
	 * here.
	 *
	 * The idea is that at startup time some code that cares about this issue
	 * (e.g. the DOM helper code) can register a nsIXPCFunctionThisTranslator
	 * object with xpconnect to handle calls to [function] interfaces of a given
	 * iid. When xpconnect goes to invoke a method on a wrapped JSObject for
	 * an interface marked as [function], xpconnect will check if the first param
	 * of the method is an xpcom object pointer and if so it will check to see if a
	 * nsIXPCFunctionThisTranslator has been registered for the given iid of the
	 * interface being called. If so it will call the translator and get an
	 * interface pointer to use as the 'this' for the call. If the translator
	 * returns a non-null interface pointer (which it should then have addref'd
	 * since it is being returned as an out param), xpconnect will attempt to build
	 * a wrapper around the pointer and get a JSObject from that wrapper to use
	 * as the 'this' for the call.
	 *
	 * If a null interface pointer is returned then xpconnect will use the default
	 * 'this' - the same JSObject as the function object it is calling.
	 *
	 * The translator can also return a non-null aIIDOfResult to tell xpconnect what
	 * type of wrapper to build. If that is null then xpconnect will assume the
	 * wrapper should be for nsISupports. For objects that support flattening -
	 * i.e. expose nsIClassInfo and that interface's getInterfaces method - then
	 * a flattened wrapper will be created and no iid was really necessary.
	 *
	 * XXX aHideFirstParamFromJS is intended to allow the trimming of that first
	 * param (used to indicate 'this') from the actual call to the JS code. The JS
	 * DOM does not require this functionality and it is **NOT YET IMPLEMENTED**
	 *
	 */

	[ComImport, Guid("039ef260-2a0d-11d5-90a7-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXPCFunctionThisTranslator //: nsISupports
	{
		nsISupports TranslateThis([MarshalAs(UnmanagedType.IUnknown)] nsISupports aInitialThis,
								  nsIInterfaceInfo aInterfaceInfo,
								  UInt16 aMethodIndex,
								  out Boolean aHideFirstParamFromJS,
								  out Guid aIIDOfResult);
	}

	/***************************************************************************/

	public static class nsIXPConnectConstants
	{
		public const UInt32 INIT_JS_STANDARD_CLASSES = 1 << 0;
		public const UInt32 FLAG_SYSTEM_GLOBAL_OBJECT = 1 << 1;
		public const UInt32 OMIT_COMPONENTS_OBJECT = 1 << 2;
	}

	[ComImport, Guid("fb780ace-dced-432b-bb82-8df7d4f919c8"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIXPConnect //: nsISupports
	{
		/**
		 * Initializes classes on a global object that has already been created.
		 */
		void InitClasses(JSContextPtr aJSContext, JSObjectPtr aGlobalJSObj);

		/**
		 * Creates a new global object using the given aCOMObj as the global
		 * object. The object will be set up according to the flags (defined
		 * below). If you do not pass INIT_JS_STANDARD_CLASSES, then aCOMObj
		 * must implement nsIXPCScriptable so it can resolve the standard
		 * classes when asked by the JS engine.
		 *
		 * @param aJSContext the context to use while creating the global object.
		 * @param aCOMObj the native object that represents the global object.
		 * @param aIID the IID used to wrap the global object.
		 * @param aPrincipal the principal of the code that will run in this
		 *                   compartment. Can be null if not on the main thread.
		 * @param aExtraPtr must be passed if aPrincipal is null. Used to separate
		 *                  code from the same principal into different
		 *                  compartments, as for sandboxes.
		 * @param aFlags one of the flags below specifying what options this
		 *               global object wants.
		 */
		nsIXPConnectJSObjectHolder
		InitClassesWithNewWrappedGlobal(
					  JSContextPtr aJSContext,
					  [MarshalAs(UnmanagedType.IUnknown)] nsISupports aCOMObj,
					  [In] ref Guid aIID,
					  nsIPrincipal aPrincipal,
					  [MarshalAs(UnmanagedType.IUnknown)] nsISupports aExtraPtr,
					  UInt32 aFlags);

		/**
		* wrapNative will create a new JSObject or return an existing one.
		*
		* The JSObject is returned inside a refcounted nsIXPConnectJSObjectHolder.
		* As long as this holder is held the JSObject will be protected from
		* collection by JavaScript's garbage collector. It is a good idea to
		* transfer the JSObject to some equally protected place before releasing
		* the holder (i.e. use JS_SetProperty to make this object a property of
		* some other JSObject).
		*
		* This method now correctly deals with cases where the passed in xpcom
		* object already has an associated JSObject for the cases:
		*  1) The xpcom object has already been wrapped for use in the same scope
		*     as an nsIXPConnectWrappedNative.
		*  2) The xpcom object is in fact a nsIXPConnectWrappedJS and thus already
		*     has an underlying JSObject.
		*  3) The xpcom object implements nsIScriptObjectOwner; i.e. is an idlc
		*     style DOM object for which we can call GetScriptObject to get the
		*     JSObject it uses to represent itself into JavaScript.
		*
		* It *might* be possible to QueryInterface the nsIXPConnectJSObjectHolder
		* returned by the method into a nsIXPConnectWrappedNative or a
		* nsIXPConnectWrappedJS.
		*
		* This method will never wrap the JSObject involved in an
		* XPCNativeWrapper before returning.
		*
		* Returns:
		*    success:
		*       NS_OK
		*    failure:
		*       NS_ERROR_XPC_BAD_CONVERT_NATIVE
		*       NS_ERROR_XPC_CANT_GET_JSOBJECT_OF_DOM_OBJECT
		*       NS_ERROR_FAILURE
		*/
		nsIXPConnectJSObjectHolder
		WrapNative(JSContextPtr aJSContext,
				   JSObjectPtr aScope,
				   [MarshalAs(UnmanagedType.IUnknown)] nsISupports aCOMObj,
				   [In] ref Guid aIID);

		/**
		 * Same as wrapNative, but also returns the JSObject in aVal. C++ callers
		 * can pass in null for the aHolder argument, but in that case they must
		 * ensure that aVal is rooted.
		 * aIID may be null, it means the same as passing in
		 * &NS_GET_IID(nsISupports) but when passing in null certain shortcuts
		 * can be taken because we know without comparing IIDs that the caller is
		 * asking for an nsISupports wrapper.
		 * If aAllowWrapper, then the returned value will be wrapped in the proper
		 * type of security wrapper on top of the XPCWrappedNative (if needed).
		 * This method doesn't push aJSContext on the context stack, so the caller
		 * is required to push it if the top of the context stack is not equal to
		 * aJSContext.
		 */
		void
		WrapNativeToJSVal(JSContextPtr aJSContext,
						  JSObjectPtr aScope,
						  [MarshalAs(UnmanagedType.IUnknown)] nsISupports aCOMObj,
						  nsWrapperCachePtr aCache,
						  [In] ref Guid aIID,
						  Boolean aAllowWrapper,
						  out JsVal aVal,
						  out nsIXPConnectJSObjectHolder aHolder);

		/**
		* wrapJS will yield a new or previously existing xpcom interface pointer
		* to represent the JSObject passed in.
		*
		* This method now correctly deals with cases where the passed in JSObject
		* already has an associated xpcom interface for the cases:
		*  1) The JSObject has already been wrapped as a nsIXPConnectWrappedJS.
		*  2) The JSObject is in fact a nsIXPConnectWrappedNative and thus already
		*     has an underlying xpcom object.
		*  3) The JSObject is of a jsclass which supports getting the nsISupports
		*     from the JSObject directly. This is used for idlc style objects
		*     (e.g. DOM objects).
		*
		* It *might* be possible to QueryInterface the resulting interface pointer
		* to nsIXPConnectWrappedJS.
		*
		* Returns:
		*   success:
		*     NS_OK
		*    failure:
		*       NS_ERROR_XPC_BAD_CONVERT_JS
		*       NS_ERROR_FAILURE
		*/
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 3)]
		nsQIResult WrapJS(JSContextPtr aJSContext, JSObjectPtr aJSObj, [In] ref Guid aIID);

		/**
		 * Wraps the given jsval in a nsIVariant and returns the new variant.
		 */
		nsIVariant JSValToVariant(JSContextPtr cx, JSValPtr aJSVal);

		/**
		* This only succeeds if the JSObject is a nsIXPConnectWrappedNative.
		* A new wrapper is *never* constructed.
		*/
		nsIXPConnectWrappedNative GetWrappedNativeOfJSObject(JSContextPtr aJSContext, JSObjectPtr aJSObj);

		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetNativeOfWrapper(JSContextPtr aJSContext, JSObjectPtr aJSObj);

		JSObjectPtr GetJSObjectOfWrapper(JSContextPtr aJSContext, JSObjectPtr aJSObj);

		void SetSecurityManagerForJSContext(JSContextPtr aJSContext, nsIXPCSecurityManager aManager, UInt16 flags);

		void GetSecurityManagerForJSContext(JSContextPtr aJSContext, out nsIXPCSecurityManager aManager, out UInt16 flags);

		/**
		* The security manager to use when the current JSContext has no security
		* manager.
		*/
		void SetDefaultSecurityManager(nsIXPCSecurityManager aManager, UInt16 flags);

		void GetDefaultSecurityManager(out nsIXPCSecurityManager aManager, out UInt16 flags);

		nsIStackFrame
		CreateStackFrameLocation(UInt32 aLanguage,
								 [MarshalAs(UnmanagedType.LPStr)] String aFilename,
								 [MarshalAs(UnmanagedType.LPStr)] String aFunctionName,
								 Int32 aLineNumber,
								 nsIStackFrame aCaller);

		/**
		* Deprecated do-nothing function.
		*/
		void SyncJSContexts();

		nsIStackFrame CurrentJSStack { get; }
		nsAXPCNativeCallContextPtr CurrentNativeCallContext { get; }
		/* pass nsnull to clear pending exception */
		nsIException PendingException { get; set; }

		void DebugDump(Int16 depth);
		void DebugDumpObject([MarshalAs(UnmanagedType.IUnknown)] nsISupports aCOMObj, Int16 depth);
		void DebugDumpJSStack(Boolean showArgs,
							  Boolean showLocals,
							  Boolean showThisProps);
		void DebugDumpEvalInJSStackFrame(UInt32 aFrameNumber,
										 [MarshalAs(UnmanagedType.LPStr)] String aSourceText);

		/**
		* Set fallback JSContext to use when xpconnect can't find an appropriate
		* context to use to execute JavaScript.
		*
		* NOTE: This method is DEPRECATED. 
		*       Use nsIThreadJSContextStack::safeJSContext instead.
		*/
		void SetSafeJSContextForCurrentThread(JSContextPtr cx);

		/**
		* wrapJSAggregatedToNative is just like wrapJS except it is used in cases
		* where the JSObject is also aggregated to some native xpcom Object.
		* At present XBL is the only system that might want to do this.
		*
		* XXX write more!
		*
		* Returns:
		*   success:
		*     NS_OK
		*    failure:
		*       NS_ERROR_XPC_BAD_CONVERT_JS
		*       NS_ERROR_FAILURE
		*/
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 3)]
		nsQIResult
		WrapJSAggregatedToNative([MarshalAs(UnmanagedType.IUnknown)] nsISupports aOuter,
								 JSContextPtr aJSContext,
								 JSObjectPtr aJSObj,
								 [In] ref Guid aIID);

		// Methods added since mozilla 0.6....

		/**
		* This only succeeds if the native object is already wrapped by xpconnect.
		* A new wrapper is *never* constructed.
		*/
		nsIXPConnectWrappedNative
		GetWrappedNativeOfNativeObject(JSContextPtr aJSContext,
									   JSObjectPtr aScope,
									   [MarshalAs(UnmanagedType.IUnknown)] nsISupports aCOMObj,
									   [In] ref Guid aIID);

		nsIXPCFunctionThisTranslator GetFunctionThisTranslator([In] ref Guid aIID);

		nsIXPCFunctionThisTranslator SetFunctionThisTranslator([In] ref Guid aIID, nsIXPCFunctionThisTranslator aTranslator);

		nsIXPConnectJSObjectHolder
		ReparentWrappedNativeIfFound(JSContextPtr aJSContext,
									 JSObjectPtr aScope,
									 JSObjectPtr aNewParent,
									 [MarshalAs(UnmanagedType.IUnknown)] nsISupports aCOMObj);

		void MoveWrappers(JSContextPtr aJSContext, JSObjectPtr aOldScope, JSObjectPtr aNewScope);

		void ClearAllWrappedNativeSecurityPolicies();

		nsIXPConnectJSObjectHolder
		GetWrappedNativePrototype(JSContextPtr aJSContext,
								  JSObjectPtr aScope,
								  nsIClassInfo aClassInfo);

		void ReleaseJSContext(JSContextPtr aJSContext, Boolean noGC);

		[return: MarshalAs(UnmanagedType.LPStruct)]
		JsVal VariantToJS(JSContextPtr ctx, JSObjectPtr scope, nsIVariant value);
		nsIVariant JSToVariant(JSContextPtr ctx, [In] ref JsVal value);

		/**
		 * Preconfigure XPCNativeWrapper automation so that when a scripted
		 * caller whose filename starts with filenamePrefix accesses a wrapped
		 * native that is not flagged as "system", the wrapped native will be
		 * automatically wrapped with an XPCNativeWrapper.
		 *
		 * @param aFilenamePrefix the UTF-8 filename prefix to match, which
		 *                        should end with a slash (/) character
		 * @param aWantNativeWrappers whether XPConnect should produce native
		 *                            wrappers for scripts whose paths begin
		 *                            with this prefix
		 */
		void FlagSystemFilenamePrefix([MarshalAs(UnmanagedType.LPStr)] String aFilenamePrefix,
									  Boolean aWantNativeWrappers);

		/**
		 * Restore an old prototype for wrapped natives of type
		 * aClassInfo. This should be used only when restoring an old
		 * scope into a state close to where it was prior to
		 * being reinitialized.
		 */
		void RestoreWrappedNativePrototype(JSContextPtr aJSContext,
										   JSObjectPtr aScope,
										   nsIClassInfo aClassInfo,
										   nsIXPConnectJSObjectHolder aPrototype);

		/**
		 * Create a sandbox for evaluating code in isolation using
		 * evalInSandboxObject().
		 *
		 * @param cx A context to use when creating the sandbox object.
		 * @param principal The principal (or NULL to use the null principal)
		 *                  to use when evaluating code in this sandbox.
		 */
		nsIXPConnectJSObjectHolder CreateSandbox(JSContextPtr cx, nsIPrincipal principal);

		/**
		 * Evaluate script in a sandbox, completely isolated from all
		 * other running scripts.
		 *
		 * @param source The source of the script to evaluate.
		 * @param cx The context to use when setting up the evaluation of
		 *           the script. The actual evaluation will happen on a new
		 *           temporary context.
		 * @param sandbox The sandbox object to evaluate the script in.
		 * @param returnStringOnly The only results to come out of the
		 *                         computation (including exceptions) will
		 *                         be coerced into strings created in the
		 *                         sandbox.
		 * @return The result of the evaluation as a jsval. If the caller
		 *         intends to use the return value from this call the caller
		 *         is responsible for rooting the jsval before making a call
		 *         to this method.
		 */
		[return: MarshalAs(UnmanagedType.LPStruct)]
		JsVal EvalInSandboxObject([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String source, JSContextPtr cx,
											 nsIXPConnectJSObjectHolder sandbox,
											 Boolean returnStringOnly);

		/**
		 * Root JS objects held by aHolder.
		 * @param aHolder The object that hold the JS objects that should be rooted.
		 * @param aTrace The tracer for aHolder.
		 */
		void AddJSHolder(IntPtr aHolder, nsScriptObjectTracerPtr aTracer);

		/**
		 * Stop rooting the JS objects held by aHolder.
		 * @param aHolder The object that hold the rooted JS objects.
		 */
		void RemoveJSHolder(IntPtr aHolder);

		/**
		 * Note aJSContext as a child to the cycle collector.
		 * @param aJSContext The JSContext to note.
		 * @param aCb The cycle collection traversal callback.
		 */
		void NoteJSContext(JSContextPtr aJSContext, nsCCTraversalCallbackRef aCb);

		/**
		 * Get the JSEqualityOp pointer to use for identifying JSObjects that hold
		 * a pointer to a nsIXPConnectWrappedNative or to the native in their
		 * private date. See IS_WRAPPER_CLASS in xpcprivate.h for details.
		 */
		void GetXPCWrappedNativeJSClassInfo(out JSEqualityOp equality);

		/**
		 * Whether or not XPConnect should report all JS exceptions when returning
		 * from JS into C++. False by default, although any value set in the
		 * MOZ_REPORT_ALL_JS_EXCEPTIONS environment variable will override the value
		 * passed here.
		 */
		void SetReportAllJSExceptions(Boolean reportAllJSExceptions);

		/**
		 * Trigger a JS garbage collection.
		 */
		void GarbageCollect();

		/**
		 * Define quick stubs on the given object, @a proto.
		 *
		 * @param cx
		 *     A context.  Requires request.
		 * @param proto
		 *     The (newly created) prototype object for a DOM class.  The JS half
		 *     of an XPCWrappedNativeProto.
		 * @param flags
		 *     Property flags for the quick stub properties--should be either
		 *     JSPROP_ENUMERATE or 0.
		 * @param interfaceCount
		 *     The number of interfaces the class implements.
		 * @param interfaceArray
		 *     The interfaces the class implements; interfaceArray and
		 *     interfaceCount are like what nsIClassInfo.getInterfaces returns.
		 */
		Boolean DefineDOMQuickStubs(
			JSContextPtr cx,
			JSObjectPtr proto,
			UInt32 flags,
			UInt32 interfaceCount,
			[In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPStruct, SizeParamIndex = 3)] Guid[] interfaceArray);

		/**
		 * Creates a JS object holder around aObject that will hold the object
		 * alive for as long as the holder stays alive.
		 */
		nsIXPConnectJSObjectHolder HoldObject(JSContextPtr aJSContext, JSObjectPtr aObject);

		/**
		 * Return the caller object of the current call from JS.
		 */
		void GetCaller(out JSContextPtr aJSContext, out JSObjectPtr aObject);

		/**
		 * When we place the browser in JS debug mode, there can't be any
		 * JS on the stack. This is because we currently activate debugMode 
		 * on all scripts in the JSRuntime when the debugger is activated.
		 * This method will turn debug mode on or off when the context 
		 * stack reaches zero length.
		 */
		void SetDebugModeWhenPossible(Boolean mode);
	}
}
