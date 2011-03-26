using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop.JavaScript
{
	[StructLayout(LayoutKind.Sequential)]
	public struct JsVal : IEquatable<JsVal>
	{
		public static readonly JsVal JSVAL_NULL = BuildJsVal(JSVAL_TAG_NULL, 0u);
		public static readonly JsVal JSVAL_ZERO = BuildJsVal(JSVAL_TAG_INT32, 0u);
		public static readonly JsVal JSVAL_ONE = BuildJsVal(JSVAL_TAG_INT32, 1u);
		public static readonly JsVal JSVAL_FALSE = BuildJsVal(JSVAL_TAG_BOOLEAN, 0u);
		public static readonly JsVal JSVAL_TRUE = BuildJsVal(JSVAL_TAG_BOOLEAN, 1u);
		public static readonly JsVal JSVAL_VOID = BuildJsVal(JSVAL_TAG_UNDEFINED, 0u);

		public static explicit operator UInt64(JsVal jsval)
		{
			return jsval.Bits;
		}

		public static explicit operator JsVal(UInt64 bits)
		{
			return new JsVal(bits);
		}

		public static Boolean operator ==(JsVal jsval1, JsVal jsval2)
		{
			return jsval1.Equals(jsval2);
		}

		public static Boolean operator !=(JsVal jsval1, JsVal jsval2)
		{
			return !(jsval1 == jsval2);
		}

		public static JsVal BuildJsVal(UInt32 tag, UInt32 payload)
		{
			UInt64 value = (((UInt64)tag) << 32) | payload;
			return new JsVal(value);
		}

		public Boolean Equals(JsVal other)
		{
			return Bits == other.Bits;
		}

		public override Boolean Equals(Object obj)
		{
			if (!(obj is JsVal))
			{
				return false;
			}

			return Equals((JsVal)obj);
		}

		public override Int32 GetHashCode()
		{
			return Bits.GetHashCode();
		}

		private JsVal(UInt64 bits)
		{
			m_Bits = bits;
		}

		private UInt64 Bits { get { return m_Bits; } }

		private const Byte JSVAL_TYPE_DOUBLE = 0x00;
		private const Byte JSVAL_TYPE_INT32 = 0x01;
		private const Byte JSVAL_TYPE_UNDEFINED = 0x02;
		private const Byte JSVAL_TYPE_BOOLEAN = 0x03;
		private const Byte JSVAL_TYPE_MAGIC = 0x04;
		private const Byte JSVAL_TYPE_STRING = 0x05;
		private const Byte JSVAL_TYPE_NULL = 0x06;
		private const Byte JSVAL_TYPE_OBJECT = 0x07;

		private const UInt32 JSVAL_TAG_CLEAR = 0xFFFF0000;
		private const UInt32 JSVAL_TAG_INT32 = JSVAL_TAG_CLEAR | JSVAL_TYPE_INT32;
		private const UInt32 JSVAL_TAG_UNDEFINED = JSVAL_TAG_CLEAR | JSVAL_TYPE_UNDEFINED;
		private const UInt32 JSVAL_TAG_STRING = JSVAL_TAG_CLEAR | JSVAL_TYPE_STRING;
		private const UInt32 JSVAL_TAG_BOOLEAN = JSVAL_TAG_CLEAR | JSVAL_TYPE_BOOLEAN;
		private const UInt32 JSVAL_TAG_MAGIC = JSVAL_TAG_CLEAR | JSVAL_TYPE_MAGIC;
		private const UInt32 JSVAL_TAG_NULL = JSVAL_TAG_CLEAR | JSVAL_TYPE_NULL;
		private const UInt32 JSVAL_TAG_OBJECT = JSVAL_TAG_CLEAR | JSVAL_TYPE_OBJECT;

		private readonly UInt64 m_Bits;
	}
}
