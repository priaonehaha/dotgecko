using System;

namespace GoldParser
{
	/// <summary>
	/// This callback is used by parser to notify read source line.
	/// Use parser.LineText to get line source.
	/// </summary>
	public delegate void SourceLineReadCallback(Parser parser, Int32 lineStart, Int32 lineLength);
}
