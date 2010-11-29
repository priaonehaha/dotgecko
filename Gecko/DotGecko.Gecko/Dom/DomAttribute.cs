using System;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomAttribute : DomNode
	{
		private DomAttribute(nsIDOMAttr domAttribute)
			: base(domAttribute)
		{
			Debug.Assert(domAttribute != null);
			m_DomAttribute = domAttribute;
		}

		internal static DomAttribute Create(nsIDOMAttr domAttribute)
		{
			return domAttribute != null ? new DomAttribute(domAttribute) : null;
		}

		public String Name { get { return XpcomStringHelper.Get(m_DomAttribute.GetName); } }

		public Boolean Specified { get { return m_DomAttribute.Specified; } }

		public String Value
		{
			get { return XpcomStringHelper.Get(m_DomAttribute.GetValue); }
			set { m_DomAttribute.SetValue(value); }
		}

		public DomElement OwnerElement { get { return DomElement.Create(m_DomAttribute.OwnerElement); } }

		new internal nsIDOMAttr DomObj { get { return m_DomAttribute; } }

		private readonly nsIDOMAttr m_DomAttribute;
	}
}
