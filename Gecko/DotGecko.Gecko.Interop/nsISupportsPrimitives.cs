using System;
using System.Runtime.InteropServices;
using System.Text;
using nsISupports = System.Object;
using PRTime = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	public static class nsISupportsPrimitiveConstants
	{
		public const UInt16 TYPE_ID = 1;
		public const UInt16 TYPE_CSTRING = 2;
		public const UInt16 TYPE_STRING = 3;
		public const UInt16 TYPE_PRBOOL = 4;
		public const UInt16 TYPE_PRUINT8 = 5;
		public const UInt16 TYPE_PRUINT16 = 6;
		public const UInt16 TYPE_PRUINT32 = 7;
		public const UInt16 TYPE_PRUINT64 = 8;
		public const UInt16 TYPE_PRTIME = 9;
		public const UInt16 TYPE_CHAR = 10;
		public const UInt16 TYPE_PRINT16 = 11;
		public const UInt16 TYPE_PRINT32 = 12;
		public const UInt16 TYPE_PRINT64 = 13;
		public const UInt16 TYPE_FLOAT = 14;
		public const UInt16 TYPE_DOUBLE = 15;
		public const UInt16 TYPE_VOID = 16;
		public const UInt16 TYPE_INTERFACE_POINTER = 17;
	}

	/**
	 * Primitive base interface.
	 *
	 * These first three are pointer types and do data copying
	 * using the nsIMemory. Be careful!
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("d0d4b136-1dd1-11b2-9371-f0727ef827c0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPrimitive //: nsISupports
	{
		UInt16 Type { get; }
	}

	/**
	 * Scriptable storage for nsID structures
	 *
	 * @status FROZEN
	 */
	[ComImport, Guid("d18290a0-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsID : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Guid Data { [return: MarshalAs(UnmanagedType.LPStruct)] get; [param: MarshalAs(UnmanagedType.LPStruct)] set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for ASCII strings
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("d65ff270-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsCString : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		void GetData([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		void SetData([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String value);
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for Unicode strings
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("d79dc970-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsString : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		void GetData([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void SetData([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String value);
		[return: MarshalAs(UnmanagedType.LPWStr)]
		String ToString();
	}

	/**
	 * The rest are truly primitive and are passed by value
	 */

	/**
	 * Scriptable storage for booleans
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("ddc3b490-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRBool : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Boolean Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for 8-bit integers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("dec2e4e0-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRUint8 : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Byte Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for unsigned 16-bit integers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("dfacb090-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRUint16 : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		UInt16 Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for unsigned 32-bit integers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("e01dc470-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRUint32 : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		UInt32 Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for 64-bit integers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("e13567c0-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRUint64 : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		UInt64 Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for NSPR date/time values
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("e2563630-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRTime : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		PRTime Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for single character values
	 * (often used to store an ASCII character)
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("e2b05e40-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsChar : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Char Data { [return: MarshalAs(UnmanagedType.U1)] get; [param: MarshalAs(UnmanagedType.U1)] set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for 16-bit integers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("e30d94b0-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRInt16 : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Int16 Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for 32-bit integers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("e36c5250-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRInt32 : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Int32 Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for 64-bit integers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("e3cb0ff0-4a1c-11d3-9890-006008962422"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsPRInt64 : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Int64 Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for floating point numbers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("abeaa390-4ac0-11d3-baea-00805f8a5dd7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsFloat : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Single Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for doubles
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("b32523a0-4ac0-11d3-baea-00805f8a5dd7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsDouble : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		Double Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for generic pointers
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("464484f0-568d-11d3-baf8-00805f8a5dd7"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsVoid : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		IntPtr Data { get; set; }
		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}

	/**
	 * Scriptable storage for other XPCOM objects
	 * 
	 * @status FROZEN
	 */
	[ComImport, Guid("995ea724-1dd1-11b2-9211-c21bdd3e7ed0"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsISupportsInterfacePointer : nsISupportsPrimitive
	{
		#region nsISupportsPrimitive Members

		new UInt16 Type { get; }

		#endregion

		nsISupports Data { [return: MarshalAs(UnmanagedType.IUnknown)] get; [param: MarshalAs(UnmanagedType.IUnknown)] set; }
		Guid DataIID { [return: MarshalAs(UnmanagedType.LPStruct)] get; [param: MarshalAs(UnmanagedType.LPStruct)] set; }

		[return: MarshalAs(UnmanagedType.LPStr)]
		String ToString();
	}
}
