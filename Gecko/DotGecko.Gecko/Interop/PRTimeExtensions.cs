using System;
using PRTime = System.UInt64;

namespace DotGecko.Gecko.Interop
{
	internal static class ExtensionMethods
	{
		internal static DateTime ToDateTime(this PRTime prTime)
		{
			var ts = TimeSpan.FromMilliseconds(prTime);
			return ms_PRTimeOrigin + ts;
		}

		internal static DateTime ToDateTime(this Int64 prTime)
		{
			var ts = TimeSpan.FromMilliseconds(prTime);
			return ms_PRTimeOrigin + ts;
		}

		internal static PRTime ToPRTime(this DateTime dateTime)
		{
			var ts = new TimeSpan(dateTime.ToUniversalTime().Ticks - ms_PRTimeOrigin.Ticks);
			return (UInt64)Math.Round(ts.TotalMilliseconds);
		}

		private static readonly DateTime ms_PRTimeOrigin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
	}
}
