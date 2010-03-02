using System.IO;
using DotGecko.Xpidl.Parser;

namespace DotGecko.Xpidl.Formatter
{
	public interface IXpidlFormatter
	{
		void Format(XpidlFile xpidlFile, TextWriter textWriter);
	}
}
