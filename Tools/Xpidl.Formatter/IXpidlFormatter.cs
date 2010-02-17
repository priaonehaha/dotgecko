using System.IO;
using Xpidl.Parser;

namespace Xpidl.Formatter
{
	public interface IXpidlFormatter
	{
		void Format(XpidlFile xpidlFile, TextWriter textWriter);
	}
}
