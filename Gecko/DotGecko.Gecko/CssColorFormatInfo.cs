using System;
using System.Drawing;
using System.Globalization;

namespace DotGecko.Gecko
{
	internal sealed class CssColorFormatInfo : IFormatProvider, ICustomFormatter
	{
		public static CssColorFormatInfo CurrentInfo
		{
			get { return new CssColorFormatInfo(); }
		}

		public Object GetFormat(Type formatType)
		{
			return formatType == typeof(ICustomFormatter) ? this : null;
		}

		public String Format(String format, Object arg, IFormatProvider formatProvider)
		{
			if (arg is Color)
			{
				return FormatColor(format, (Color)arg);
			}

			return HandleOtherFormats(format, arg);
		}

		private static String FormatColor(String format, Color arg)
		{
			if (String.IsNullOrWhiteSpace(format))
			{
				return arg.ToString();
			}

			var numberFormatInfo =
				new NumberFormatInfo
				{
					NumberDecimalDigits = 1,
					PercentDecimalDigits = 0,
					PercentNegativePattern = 1,
					PercentPositivePattern = 1
				};
			switch (format)
			{
				case "hex":
					{
						return String.Format(numberFormatInfo, "#{0:x2}{1:x2}{2:x2}", arg.R, arg.G, arg.B);
					}
				case "HEX":
					{
						return String.Format(numberFormatInfo, "#{0:X2}{1:X2}{2:X2}", arg.R, arg.G, arg.B);
					}
				case "rgb":
					{
						return String.Format(numberFormatInfo, "rgb({0}, {1}, {2})", arg.R, arg.G, arg.B);
					}
				case "rgb%":
					{
						return String.Format(numberFormatInfo, "rgb({0:P}, {1:P}, {2:P})", arg.R / 255d, arg.G / 255d, arg.B / 255d);
					}
				case "rgba":
					{
						return String.Format(numberFormatInfo, "rgba({0}, {1}, {2}, {3:0.#})", arg.R, arg.G, arg.B, arg.A / 255d);
					}
				case "rgba%":
					{
						return String.Format(numberFormatInfo, "rgba({0:P}, {1:P}, {2:P}, {3:0.#})", arg.R / 255d, arg.G / 255d, arg.B / 255d, arg.A / 255d);
					}
				case "hsl":
					{
						return String.Format(numberFormatInfo, "hsl({0:F0}, {1:P}, {2:P})", arg.GetHue(), arg.GetSaturation(), arg.GetBrightness());
					}
				case "hsla":
					{
						return String.Format(numberFormatInfo, "hsla({0:F0}, {1:P}, {2:P}, {3:0.#})", arg.GetHue(), arg.GetSaturation(), arg.GetBrightness(), arg.A / 255d);
					}
				default:
					{
						throw new FormatException(String.Format("Invalid format specified: \"{0}\".", format));
					}
			}
		}

		private static String HandleOtherFormats(String format, Object arg)
		{
			if (arg is IFormattable)
			{
				return ((IFormattable)arg).ToString(format, CultureInfo.CurrentCulture);
			}

			if (arg != null)
			{
				return arg.ToString();
			}

			return String.Empty;
		}
	}
}
