using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomNodeList : IEnumerable<DomNode>
	{
		private DomNodeList(nsIDOMNodeList domNodeList)
		{
			Debug.Assert(domNodeList != null);
			m_DomNodeList = domNodeList;
		}

		internal static DomNodeList Create(nsIDOMNodeList domNodeList)
		{
			return domNodeList != null ? new DomNodeList(domNodeList) : null;
		}

		[IndexerName("Item")]
		public DomNode this[UInt32 index]
		{
			get { return DomNode.Create(m_DomNodeList.Item(index)); }
		}

		public UInt32 Length
		{
			get { return m_DomNodeList.Length; }
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

		private readonly nsIDOMNodeList m_DomNodeList;
	}
}
