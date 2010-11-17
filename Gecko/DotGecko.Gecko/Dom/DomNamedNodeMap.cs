using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomNamedNodeMap : IEnumerable<DomNode>
	{
		private DomNamedNodeMap(nsIDOMNamedNodeMap domNamedNodeMap)
		{
			Debug.Assert(domNamedNodeMap != null);
			m_DomNamedNodeMap = domNamedNodeMap;
		}

		internal static DomNamedNodeMap Create(nsIDOMNamedNodeMap domNamedNodeMap)
		{
			return domNamedNodeMap != null ? new DomNamedNodeMap(domNamedNodeMap) : null;
		}

		public DomNode GetNamedItem(String name)
		{
			nsIDOMNode domNode = m_DomNamedNodeMap.GetNamedItem(name);
			return DomNode.Create(domNode);
		}

		public DomNode SetNamedItem(DomNode arg)
		{
			nsIDOMNode domNode = m_DomNamedNodeMap.SetNamedItem(arg.DomObj);
			return DomNode.Create(domNode);
		}

		public DomNode RemoveNamedItem(String name)
		{
			nsIDOMNode domNode = m_DomNamedNodeMap.RemoveNamedItem(name);
			return DomNode.Create(domNode);
		}

		[IndexerName("Item")]
		public DomNode this[UInt32 index]
		{
			get
			{
				nsIDOMNode domNode = m_DomNamedNodeMap.Item(index);
				return DomNode.Create(domNode);
			}
		}

		public UInt32 Length { get { return m_DomNamedNodeMap.Length; } }

		public DomNode GetNamedItemNS(String namespaceURI, String localName)
		{
			nsIDOMNode domNode = m_DomNamedNodeMap.GetNamedItemNS(namespaceURI, localName);
			return DomNode.Create(domNode);
		}

		public DomNode SetNamedItemNS(DomNode arg)
		{
			nsIDOMNode domNode = m_DomNamedNodeMap.SetNamedItemNS(arg.DomObj);
			return DomNode.Create(domNode);
		}

		public DomNode RemoveNamedItemNS(String namespaceURI, String localName)
		{
			nsIDOMNode domNode = m_DomNamedNodeMap.RemoveNamedItemNS(namespaceURI, localName);
			return DomNode.Create(domNode);
		}

		IEnumerator<DomNode> IEnumerable<DomNode>.GetEnumerator()
		{
			for (UInt32 i = 0; i < Length; ++i)
			{
				yield return this[i];
			}
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<DomNode>)this).GetEnumerator();
		}

		private readonly nsIDOMNamedNodeMap m_DomNamedNodeMap;
	}
}
