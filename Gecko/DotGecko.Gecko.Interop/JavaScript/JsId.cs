using System;
using System.Runtime.InteropServices;

namespace DotGecko.Gecko.Interop.JavaScript
{
	[StructLayout(LayoutKind.Sequential)]
	public struct JsId : IEquatable<JsId>
	{
		public static explicit operator UInt32(JsId jsid)
		{
			return jsid.Bits;
		}

		public static explicit operator JsId(UInt32 bits)
		{
			return new JsId(bits);
		}

		public static Boolean operator ==(JsId jsid1, JsId jsid2)
		{
			return jsid1.Equals(jsid2);
		}

		public static Boolean operator !=(JsId jsid1, JsId jsid2)
		{
			return !(jsid1 == jsid2);
		}

		public Boolean Equals(JsId other)
		{
			return Bits == other.Bits;
		}

		public override Boolean Equals(Object obj)
		{
			if (!(obj is JsId))
			{
				return false;
			}

			return Equals((JsId)obj);
		}

		public override Int32 GetHashCode()
		{
			return Bits.GetHashCode();
		}

		private JsId(UInt32 bits)
		{
			m_Bits = bits;
		}

		private UInt32 Bits { get { return m_Bits; } }

		private const Byte JSID_TYPE_STRING = 0x0;
		private const Byte JSID_TYPE_INT = 0x1;
		private const Byte JSID_TYPE_VOID = 0x2;
		private const Byte JSID_TYPE_OBJECT = 0x4;
		private const Byte JSID_TYPE_DEFAULT_XML_NAMESPACE = 0x6;
		private const Byte JSID_TYPE_MASK = 0x7;

		private readonly UInt32 m_Bits;
	}
}
