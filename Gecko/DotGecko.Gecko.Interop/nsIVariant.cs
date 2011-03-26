using System;
using System.Runtime.InteropServices;
using System.Text;
using DotGecko.Gecko.Interop.JavaScript;
using nsISupports = System.Object;
using DOMStringMarshaler = DotGecko.Gecko.Interop.AStringMarshaler;

namespace DotGecko.Gecko.Interop
{
	public static class nsIDataTypeConstants
	{
		// These MUST match the declarations in xpt_struct.h. 
		// Otherwise the world is likely to explode.   
		// From xpt_struct.h ...
		public const UInt16 VTYPE_INT8 = 0;             // TD_INT8              = 0,
		public const UInt16 VTYPE_INT16 = 1;            // TD_INT16             = 1,
		public const UInt16 VTYPE_INT32 = 2;            // TD_INT32             = 2,
		public const UInt16 VTYPE_INT64 = 3;            // TD_INT64             = 3,
		public const UInt16 VTYPE_UINT8 = 4;            // TD_UINT8             = 4,
		public const UInt16 VTYPE_UINT16 = 5;           // TD_UINT16            = 5,
		public const UInt16 VTYPE_UINT32 = 6;           // TD_UINT32            = 6,
		public const UInt16 VTYPE_UINT64 = 7;           // TD_UINT64            = 7,
		public const UInt16 VTYPE_FLOAT = 8;            // TD_FLOAT             = 8, 
		public const UInt16 VTYPE_DOUBLE = 9;           // TD_DOUBLE            = 9,
		public const UInt16 VTYPE_BOOL = 10;            // TD_BOOL              = 10,
		public const UInt16 VTYPE_CHAR = 11;            // TD_CHAR              = 11,
		public const UInt16 VTYPE_WCHAR = 12;           // TD_WCHAR             = 12,
		public const UInt16 VTYPE_VOID = 13;            // TD_VOID              = 13,
		public const UInt16 VTYPE_ID = 14;              // TD_PNSIID            = 14,
		public const UInt16 VTYPE_DOMSTRING = 15;       // TD_DOMSTRING         = 15,
		public const UInt16 VTYPE_CHAR_STR = 16;        // TD_PSTRING           = 16,
		public const UInt16 VTYPE_WCHAR_STR = 17;       // TD_PWSTRING          = 17,
		public const UInt16 VTYPE_INTERFACE = 18;       // TD_INTERFACE_TYPE    = 18,
		public const UInt16 VTYPE_INTERFACE_IS = 19;    // TD_INTERFACE_IS_TYPE = 19,
		public const UInt16 VTYPE_ARRAY = 20;           // TD_ARRAY             = 20,
		public const UInt16 VTYPE_STRING_SIZE_IS = 21;  // TD_PSTRING_SIZE_IS   = 21,
		public const UInt16 VTYPE_WSTRING_SIZE_IS = 22; // TD_PWSTRING_SIZE_IS  = 22,
		public const UInt16 VTYPE_UTF8STRING = 23;      // TD_UTF8STRING        = 23,
		public const UInt16 VTYPE_CSTRING = 24;         // TD_CSTRING           = 24,
		public const UInt16 VTYPE_ASTRING = 25;         // TD_ASTRING           = 25,
		public const UInt16 VTYPE_EMPTY_ARRAY = 254;
		public const UInt16 VTYPE_EMPTY = 255;
	}

	/* The long avoided variant support for xpcom. */
	[ComImport, Guid("4d12e540-83d7-11d5-90ed-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIDataType //: nsISupports
	{
	}

	/**
	 * XPConnect has magic to transparently convert between nsIVariant and JS types.
	 * We mark the interface [scriptable] so that JS can use methods
	 * that refer to this interface. But we mark all the methods and attributes
	 * [noscript] since any nsIVariant object will be automatically converted to a
	 * JS type anyway.
	 */
	[ComImport, Guid("81e4c2de-acac-4ad6-901a-b5fb1b851a0d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIVariant //: nsISupports
	{
		UInt16 DataType { get; }

		SByte GetAsInt8();
		Int16 GetAsInt16();
		Int32 GetAsInt32();
		Int64 GetAsInt64();
		Byte GetAsUint8();
		UInt16 GetAsUint16();
		UInt32 GetAsUint32();
		UInt64 GetAsUint64();
		Single GetAsFloat();
		Double GetAsDouble();
		Boolean GetAsBool();
		[return: MarshalAs(UnmanagedType.U1)]
		Char GetAsChar();
		Char GetAsWChar();
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult GetAsID(out Guid retval);
		void GetAsAString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		void GetAsDOMString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		void GetAsACString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		void GetAsAUTF8String([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		[return: MarshalAs(UnmanagedType.LPStr)]
		String GetAsString();
		[return: MarshalAs(UnmanagedType.LPWStr)]
		String GetAsWString();

		[return: MarshalAs(UnmanagedType.IUnknown)]
		nsISupports GetAsISupports();

		[return: MarshalAs(UnmanagedType.LPStruct)]
		JsVal GetAsJSVal();

		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 0)]
		Object GetAsInterface(out Guid iid);

		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		nsResult GetAsArray(out UInt16 type, out Guid iid, out UInt32 count, out IntPtr ptr);

		[return: MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 0)]
		String GetAsStringWithSize(out UInt32 size);

		[return: MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)]
		String GetAsWStringWithSize(out UInt32 size);
	}

	/**
	 * An object that implements nsIVariant may or may NOT also implement this
	 * nsIWritableVariant.
	 * 
	 * If the 'writable' attribute is false then attempts to call any of the 'set'
	 * methods can be expected to fail. Setting the 'writable' attribute may or
	 * may not succeed.
	 *
	 */
	[ComImport, Guid("5586a590-8c82-11d5-90f3-0010a4e73d9a"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface nsIWritableVariant : nsIVariant
	{
		#region nsIVariant Memebers

		new UInt16 DataType { get; }
		new SByte GetAsInt8();
		new Int16 GetAsInt16();
		new Int32 GetAsInt32();
		new Int64 GetAsInt64();
		new Byte GetAsUint8();
		new UInt16 GetAsUint16();
		new UInt32 GetAsUint32();
		new UInt64 GetAsUint64();
		new Single GetAsFloat();
		new Double GetAsDouble();
		new Boolean GetAsBool();
		[return: MarshalAs(UnmanagedType.U1)]
		new Char GetAsChar();
		new Char GetAsWChar();
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		new nsResult GetAsID(out Guid retval);
		new void GetAsAString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] StringBuilder result);
		new void GetAsDOMString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] StringBuilder result);
		new void GetAsACString([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] StringBuilder result);
		new void GetAsAUTF8String([In, Out, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] StringBuilder result);
		[return: MarshalAs(UnmanagedType.LPStr)]
		new String GetAsString();
		[return: MarshalAs(UnmanagedType.LPWStr)]
		new String GetAsWString();
		[return: MarshalAs(UnmanagedType.IUnknown)]
		new nsISupports GetAsISupports();
		[return: MarshalAs(UnmanagedType.LPStruct)]
		new JsVal GetAsJSVal();
		[return: MarshalAs(UnmanagedType.Interface, IidParameterIndex = 0)]
		new Object GetAsInterface(out Guid iid);
		[PreserveSig]
		[return: MarshalAs(UnmanagedType.U4)]
		new nsResult GetAsArray(out UInt16 type, out Guid iid, out UInt32 count, out IntPtr ptr);
		[return: MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 0)]
		new String GetAsStringWithSize(out UInt32 size);
		[return: MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)]
		new String GetAsWStringWithSize(out UInt32 size);

		#endregion

		Boolean Writable { get; set; }

		void SetAsInt8(SByte aValue);
		void SetAsInt16(Int16 aValue);
		void SetAsInt32(Int32 aValue);
		void SetAsInt64(Int64 aValue);
		void SetAsUint8(Byte aValue);
		void SetAsUint16(UInt16 aValue);
		void SetAsUint32(UInt32 aValue);
		void SetAsUint64(UInt64 aValue);
		void SetAsFloat(Single aValue);
		void SetAsDouble(Double aValue);
		void SetAsBool(Boolean aValue);
		void SetAsChar([MarshalAs(UnmanagedType.U1)] Char aValue);
		void SetAsWChar(Char aValue);
		void SetAsID([In] ref Guid aValue);
		void SetAsAString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AStringMarshaler))] String aValue);
		void SetAsDOMString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(DOMStringMarshaler))] String aValue);
		void SetAsACString([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(ACStringMarshaler))] String aValue);
		void SetAsAUTF8String([In, MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(AUTF8StringMarshaler))] String aValue);
		void SetAsString([MarshalAs(UnmanagedType.LPStr)] String aValue);
		void SetAsWString([MarshalAs(UnmanagedType.LPWStr)] String aValue);
		void SetAsISupports([MarshalAs(UnmanagedType.IUnknown)] nsISupports aValue);

		void SetAsInterface([In] ref Guid iid, [MarshalAs(UnmanagedType.Interface, IidParameterIndex = 0)] Object iface);

		void SetAsArray(UInt16 type, [In] ref Guid iid, UInt32 count, IntPtr ptr);

		void SetAsStringWithSize(UInt32 size, [MarshalAs(UnmanagedType.LPStr, SizeParamIndex = 0)] String str);

		void SetAsWStringWithSize(UInt32 size, [MarshalAs(UnmanagedType.LPWStr, SizeParamIndex = 0)] String str);

		void SetAsVoid();
		void SetAsEmpty();
		void SetAsEmptyArray();

		void SetFromVariant(nsIVariant aValue);
	}
}
