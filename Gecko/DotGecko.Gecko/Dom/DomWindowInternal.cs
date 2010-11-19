using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomWindowInternal : DomWindow2
	{
		private DomWindowInternal(nsIDOMWindowInternal domWindowInternal)
			: base(domWindowInternal)
		{
			Debug.Assert(domWindowInternal != null);
			m_DomWindowInternal = domWindowInternal;
		}

		internal static DomWindowInternal Create(nsIDOMWindowInternal domWindowInternal)
		{
			return domWindowInternal != null ? new DomWindowInternal(domWindowInternal) : null;
		}

		public Int32 InnerWidth
		{
			get { return m_DomWindowInternal.InnerWidth; }
			set { m_DomWindowInternal.InnerWidth = value; }
		}

		public Int32 InnerHeight
		{
			get { return m_DomWindowInternal.InnerHeight; }
			set { m_DomWindowInternal.InnerHeight = value; }
		}

		new internal nsIDOMWindowInternal DomObj { get { return m_DomWindowInternal; } }

		private readonly nsIDOMWindowInternal m_DomWindowInternal;
	}
}
