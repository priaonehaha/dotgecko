using System;
using System.Runtime.InteropServices;

namespace Xpidl.Parser
{
	internal class XpidlType
	{
		public const String Void = "void";
		public const String Boolean = "boolean";
		public const String Octet = "octet";
		public const String Short = "short";
		public const String Long = "long";
		public const String LongLong = "long long";
		public const String UnsignedShort = "unsigned short";
		public const String UnsignedLong = "unsigned long";
		public const String UnsignedLongLong = "unsigned long long";
		public const String Float = "float";
		public const String Double = "double";
		public const String Char = "char";
		public const String WChar = "wchar";
		public const String String = "string";
		public const String WString = "wstring";
		public const String AString = "AString";
		public const String ACString = "ACString";
		public const String AUTF8String = "AUTF8String";
		public const String DOMString = "DOMString";

		public XpidlType(String typeName)
		{
			m_Name = typeName;
			m_ClrType = GetClrType(m_Name, out m_MarshalAs);
		}

		public String Name
		{
			get { return m_Name; }
		}

		public Type ClrType
		{
			get { return m_ClrType; }
		}

		public MarshalAsAttribute MarshalAs
		{
			get { return m_MarshalAs; }
		}

		public override String ToString()
		{
			return Name;
		}

		private static Type GetClrType(String xpidlType, out MarshalAsAttribute marshalAs)
		{
			marshalAs = null;
			switch (xpidlType)
			{
				case Void:
					return typeof(void);
				case Boolean:
					return typeof(Boolean);
				case Octet:
					return typeof(Byte);
				case Short:
					return typeof(Int16);
				case Long:
					return typeof(Int32);
				case LongLong:
					return typeof(Int64);
				case UnsignedShort:
					return typeof(UInt16);
				case UnsignedLong:
					return typeof(UInt32);
				case UnsignedLongLong:
					return typeof(UInt64);
				case Float:
					return typeof(Single);
				case Double:
					return typeof(Double);
				case Char:
					return typeof(Byte);
				case WChar:
					return typeof(Char);
				case String:
					marshalAs = new MarshalAsAttribute(UnmanagedType.LPStr);
					return typeof(String);
				case WString:
					marshalAs = new MarshalAsAttribute(UnmanagedType.LPWStr);
					return typeof(String);
			}
			return null;
		}

		private readonly String m_Name;
		private readonly Type m_ClrType;
		private readonly MarshalAsAttribute m_MarshalAs;
	}
}
