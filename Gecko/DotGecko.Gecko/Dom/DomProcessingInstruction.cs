using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomProcessingInstruction : DomNode
	{
		private DomProcessingInstruction(nsIDOMProcessingInstruction domProcessingInstruction)
			: base(domProcessingInstruction)
		{
			Debug.Assert(domProcessingInstruction != null);
			m_DomProcessingInstruction = domProcessingInstruction;
		}

		internal static DomProcessingInstruction Create(nsIDOMProcessingInstruction domProcessingInstruction)
		{
			return domProcessingInstruction != null ? new DomProcessingInstruction(domProcessingInstruction) : null;
		}

		public String Target { get { return XpcomStringHelper.Get(m_DomProcessingInstruction.GetTarget); } }

		public String Data
		{
			get { return XpcomStringHelper.Get(m_DomProcessingInstruction.GetData); }
			set { m_DomProcessingInstruction.SetData(value); }
		}

		private readonly nsIDOMProcessingInstruction m_DomProcessingInstruction;
	}
}
