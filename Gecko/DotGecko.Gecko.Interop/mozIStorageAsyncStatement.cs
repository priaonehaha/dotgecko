using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DotGecko.Gecko.Interop
{
	/**
	 * An asynchronous SQL statement.  This differs from mozIStorageStatement by
	 * only being usable for asynchronous execution.  (mozIStorageStatement can
	 * be used for both synchronous and asynchronous purposes.)  This specialization
	 * for asynchronous operation allows us to avoid needing to acquire
	 * synchronization primitives also used by the asynchronous execution thread.
	 * In contrast, mozIStorageStatement may need to acquire the primitives and
	 * consequently can cause the main thread to lock for extended intervals while
	 * the asynchronous thread performs some long-running operation.
	 */
	[ComImport, Guid("2400f64d-2cb3-49a9-b01e-f03cacb8aa6e"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface mozIStorageAsyncStatement : mozIStorageBaseStatement
	{
		#region mozIStorageBindingParams Members

		new void BindByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, nsIVariant aValue);
		new void BindUTF8StringByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
								  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		new void BindStringByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
							  [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		new void BindDoubleByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Double aValue);
		new void BindInt32ByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Int32 aValue);
		new void BindInt64ByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName, Int64 aValue);
		new void BindNullByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName);
		new void BindBlobByName([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aName,
							[MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue,
							UInt32 aValueSize);
		new void BindByIndex(UInt32 aIndex, nsIVariant aValue);
		new void BindUTF8StringByIndex(UInt32 aIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		new void BindStringByIndex(UInt32 aIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		new void BindDoubleByIndex(UInt32 aIndex, Double aValue);
		new void BindInt32ByIndex(UInt32 aIndex, Int32 aValue);
		new void BindInt64ByIndex(UInt32 aIndex, Int64 aValue);
		new void BindNullByIndex(UInt32 aIndex);
		new void BindBlobByIndex(UInt32 aIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue, UInt32 aValueSize);

		#endregion

		#region mozIStorageBaseStatement Members

		new void DoFinalize();
		[Obsolete] new void BindUTF8StringParameter(UInt32 aParamIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		[Obsolete] new void BindStringParameter(UInt32 aParamIndex, [In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		[Obsolete] new void BindDoubleParameter(UInt32 aParamIndex, Double aValue);
		[Obsolete] new void BindInt32Parameter(UInt32 aParamIndex, Int32 aValue);
		[Obsolete] new void BindInt64Parameter(UInt32 aParamIndex, Int64 aValue);
		[Obsolete] new void BindNullParameter(UInt32 aParamIndex);
		[Obsolete] new void BindBlobParameter(UInt32 aParamIndex, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] Byte[] aValue, UInt32 aValueSize);
		new void BindParameters(mozIStorageBindingParamsArray aParameters);
		new mozIStorageBindingParamsArray NewBindingParamsArray();
		new mozIStoragePendingStatement ExecuteAsync([Optional] mozIStorageStatementCallback aCallback);
		new Int32 State { get; }
		new void EscapeStringForLIKE(
			[In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue,
			Char aEscapeChar,
			[In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder retval);

		#endregion

		/*
		 * 'params' provides a magic JS helper that lets you assign parameters by
		 * name.  Unlike the helper on mozIStorageStatement, you cannot enumerate
		 * in order to find out what parameters are legal.
		 *
		 * This does not work for BLOBs.  You must use an explicit binding API for
		 * that.
		 *
		 * example:
		 *  stmt.params.foo = 1;
		 *  stmt.params["bar"] = 2;
		 *  let argName = "baz";
		 *  stmt.params[argName] = 3;
		 *
		 * readonly attribute nsIMagic params;
		 */
	}
}
