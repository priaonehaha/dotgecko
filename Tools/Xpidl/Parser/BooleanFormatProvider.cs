using System;

namespace Xpidl.Parser
{
	internal sealed class BooleanFormatProvider : IFormatProvider, ICustomFormatter
	{
		public Object GetFormat(Type formatType)
		{
			return formatType == typeof(ICustomFormatter) ? this : null;
		}

		public String Format(String format, Object arg, IFormatProvider formatProvider)
		{
			if (!String.IsNullOrEmpty(format) && (arg != null) && (arg.GetType() == typeof(Boolean)))
			{
				String[] values = format.Split('|');
				if (values.Length != 2)
				{
					throw new FormatException("Invalid Boolean format string");
				}
				return values[(Boolean)arg ? 0 : 1];
			}
			return null;
		}
	}
}
