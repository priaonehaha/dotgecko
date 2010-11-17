using System;

namespace DotGecko.Gecko
{
	internal static class FlagsExtensions
	{
		internal static Boolean HasFlag(this Int32 value, Int32 flag)
		{
			return (value & flag) == flag;
		}

		internal static Boolean HasFlag(this UInt32 value, UInt32 flag)
		{
			return (value & flag) == flag;
		}
	}
}
