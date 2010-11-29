using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public sealed class DomHistory : IEnumerable<String>
	{
		private DomHistory(nsIDOMHistory domHistory)
		{
			Debug.Assert(domHistory != null);
			m_DomHistory = domHistory;
		}

		internal static DomHistory Create(nsIDOMHistory domHistory)
		{
			return domHistory != null ? new DomHistory(domHistory) : null;
		}

		public Int32 Length { get { return m_DomHistory.Length; } }

		public String Current { get { return XpcomStringHelper.Get(m_DomHistory.GetCurrent); } }

		public String Previous { get { return XpcomStringHelper.Get(m_DomHistory.GetPrevious); } }

		public String Next { get { return XpcomStringHelper.Get(m_DomHistory.GetNext); } }

		public void Back()
		{
			m_DomHistory.Back();
		}

		public void Forward()
		{
			m_DomHistory.Forward();
		}

		public void Go(Int32 aDelta)
		{
			m_DomHistory.Go(aDelta);
		}

		[IndexerName("Item")]
		public String this[UInt32 index]
		{
			get
			{
				String retval = XpcomStringHelper.Get(m_DomHistory.Item, index);
				return retval;
			}
		}

		IEnumerator<String> IEnumerable<String>.GetEnumerator()
		{
			for (UInt32 i = 0; i < Length; ++i)
			{
				yield return this[i];
			}
			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<String>)this).GetEnumerator();
		}

		private readonly nsIDOMHistory m_DomHistory;
	}
}
