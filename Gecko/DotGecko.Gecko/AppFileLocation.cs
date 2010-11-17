using System;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public class AppFileLocation : IAppFileLocation
	{
		public virtual String ProfileDirectory
		{
			get { return null; }
		}
	}
}
