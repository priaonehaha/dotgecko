using System.IO;

namespace DotGecko.Xpidl.Parser
{
	public interface IXpidlParser
	{
		XpidlFile Parse(TextReader xpidlTextReader);
	}
}
