using System;

namespace Xpidl.Parser
{
	public class XpidlParserException : Exception
	{
		internal XpidlParserException(String message)
			: base(message)
		{ }

		internal XpidlParserException(String message, Exception innerException)
			: base(message, innerException)
		{ }
	}

	public class XpidlParserSyntaxException : XpidlParserException
	{
		internal XpidlParserSyntaxException(String message, String tokenText, Int32 line, Int32 position)
			: base(message)
		{
			m_TokenText = tokenText;
			m_Line = line;
			m_Position = position;
		}

		public String TokenText
		{
			get { return m_TokenText; }
		}

		public Int32 Line
		{
			get { return m_Line; }
		}

		public Int32 Position
		{
			get { return m_Position; }
		}

		private readonly String m_TokenText;
		private readonly Int32 m_Line;
		private readonly Int32 m_Position;
	}
}
