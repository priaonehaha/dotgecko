using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomWindowCollection : IEnumerable<DomWindow>
	{
		private DomWindowCollection(nsIDOMWindowCollection domWindowCollection)
		{
			Debug.Assert(domWindowCollection != null);
			m_DomWindowCollection = domWindowCollection;
		}

		internal static DomWindowCollection Create(nsIDOMWindowCollection domWindowCollection)
		{
			return domWindowCollection != null ? new DomWindowCollection(domWindowCollection) : null;
		}

		public UInt32 Length { get { return m_DomWindowCollection.Length; } }

		//[IndexerName("Item")]
		public DomWindow this[UInt32 index]
		{
			get
			{
				nsIDOMWindow domWindow = m_DomWindowCollection.Item(index);
				return DomWindow.Create(domWindow);
			}
		}

		//[IndexerName("NamedItem")]
		public DomWindow this[String name]
		{
			get
			{
				nsIDOMWindow domWindow = m_DomWindowCollection.NamedItem(name);
				return DomWindow.Create(domWindow);
			}
		}

		IEnumerator<DomWindow> IEnumerable<DomWindow>.GetEnumerator()
		{
			for (UInt32 i = 0; i < Length; ++i)
			{
				yield return this[i];
			}
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<DomWindow>)this).GetEnumerator();
		}

		private readonly nsIDOMWindowCollection m_DomWindowCollection;
	}
}
