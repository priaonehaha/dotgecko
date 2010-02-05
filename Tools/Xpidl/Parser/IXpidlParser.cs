using System.IO;

namespace XPIDL.Parser
{
	internal interface IXpidlParser
	{
		XpidlFile Parse(TextReader xpidlTextReader);
	}
}
