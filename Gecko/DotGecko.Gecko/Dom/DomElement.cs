using System;
using System.Diagnostics;
using System.Text;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public class DomElement : DomNode
	{
		internal DomElement(nsIDOMElement domElement)
			: base(domElement)
		{
			Debug.Assert(domElement != null);
			m_DomElement = domElement;
		}

		internal static DomElement Create(nsIDOMElement domElement)
		{
			if (domElement == null)
			{
				return null;
			}

			if (domElement is nsIDOMHTMLElement)
			{
				return DomHtmlElement.Create((nsIDOMHTMLElement)domElement);
			}

			return new DomElement(domElement);
		}

		public String TagName
		{
			get { return XpcomString.Get(m_DomElement.GetTagName); }
		}

		public String GetAttribute(String name)
		{
			String retval = XpcomString.Get(m_DomElement.GetAttribute, name);
			return retval;
		}

		public void SetAttribute(String name, String value)
		{
			m_DomElement.SetAttribute(name, value);
		}

		public void RemoveAttribute(String name)
		{
			m_DomElement.RemoveAttribute(name);
		}

		public DomAttribute GetAttributeNode(String name)
		{
			nsIDOMAttr domAttribute = m_DomElement.GetAttributeNode(name);
			return DomAttribute.Create(domAttribute);
		}

		public DomAttribute SetAttributeNode(DomAttribute newAttr)
		{
			nsIDOMAttr domAttribute = m_DomElement.SetAttributeNode(newAttr.DomObj);
			return DomAttribute.Create(domAttribute);
		}

		public DomAttribute RemoveAttributeNode(DomAttribute oldAttr)
		{
			nsIDOMAttr domAttribute = m_DomElement.RemoveAttributeNode(oldAttr.DomObj);
			return DomAttribute.Create(domAttribute);
		}

		public DomNodeList GetElementsByTagName(String name)
		{
			nsIDOMNodeList domNodeList = m_DomElement.GetElementsByTagName(name);
			return DomNodeList.Create(domNodeList);
		}

		public String GetAttributeNS(String namespaceURI, String localName)
		{
			String retval = XpcomString.Get(m_DomElement.GetAttributeNS, namespaceURI, localName);
			return retval;
		}

		public void SetAttributeNS(String namespaceURI, String qualifiedName, String value)
		{
			m_DomElement.SetAttributeNS(namespaceURI, qualifiedName, value);
		}

		public void RemoveAttributeNS(String namespaceURI, String localName)
		{
			m_DomElement.RemoveAttributeNS(namespaceURI, localName);
		}

		public DomAttribute GetAttributeNodeNS(String namespaceURI, String localName)
		{
			nsIDOMAttr domAttribute = m_DomElement.GetAttributeNodeNS(namespaceURI, localName);
			return DomAttribute.Create(domAttribute);
		}

		public DomAttribute SetAttributeNodeNS(DomAttribute newAttr)
		{
			nsIDOMAttr domAttribute = m_DomElement.SetAttributeNodeNS(newAttr.DomObj);
			return DomAttribute.Create(domAttribute);
		}

		public DomNodeList GetElementsByTagNameNS(String namespaceURI, String localName)
		{
			nsIDOMNodeList domNodeList = m_DomElement.GetElementsByTagNameNS(namespaceURI, localName);
			return DomNodeList.Create(domNodeList);
		}

		public Boolean HasAttribute(String name)
		{
			return m_DomElement.HasAttribute(name);
		}

		public Boolean HasAttributeNS(String namespaceURI, String localName)
		{
			return m_DomElement.HasAttributeNS(namespaceURI, localName);
		}

		private readonly nsIDOMElement m_DomElement;
	}
}
