using System.IO;

namespace Xpidl.Parser
{
	internal interface IXpidlParser
	{
		XpidlFile Parse(TextReader xpidlTextReader);
	}
}
