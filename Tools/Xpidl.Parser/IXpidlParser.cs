using System.IO;

namespace Xpidl.Parser
{
	public interface IXpidlParser
	{
		XpidlFile Parse(TextReader xpidlTextReader);
	}
}
