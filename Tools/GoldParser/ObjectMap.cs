using System;

namespace GoldParser
{
	/// <summary>
	/// Maps integer values used for transition vectors to objects.
	/// </summary>
	public class ObjectMap
	{
		/// <summary>
		/// Creates new instance of <see cref="ObjectMap"/> class.
		/// </summary>
		internal ObjectMap()
		{
			m_MapProvider = new SortedMapProvider(MINSIZE);
		}

		/// <summary>
		/// Gets number of entries in the map.
		/// </summary>
		public Int32 Count
		{
			get { return m_MapProvider.Count; }
		}

		/// <summary>
		/// Gets or sets read only flag.
		/// </summary>
		public Boolean ReadOnly
		{
			get { return m_Readonly; }
			set
			{
				if (m_Readonly != value)
				{
					SetMapProvider(value);
					m_Readonly = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets value by key.
		/// </summary>
		public Object this[Int32 key]
		{
			get { return m_MapProvider[key]; }
			set { m_MapProvider.Add(key, value); }
		}

		/// <summary>
		/// Returns key by index.
		/// </summary>
		/// <param name="index">Zero based index of the requested key.</param>
		/// <returns>Returns key for the given index.</returns>
		public Int32 GetKey(Int32 index)
		{
			return m_MapProvider.GetEntry(index).Key;
		}

		/// <summary>
		/// Removes entry by its key.
		/// </summary>
		/// <param name="key"></param>
		public void Remove(Int32 key)
		{
			m_MapProvider.Remove(key);
		}

		/// <summary>
		/// Adds a new key and value pair. 
		/// If key exists then value is applied to existing key.
		/// </summary>
		/// <param name="key">New key to add.</param>
		/// <param name="value">Value for the key.</param>
		public void Add(Int32 key, Object value)
		{
			m_MapProvider.Add(key, value);
		}

		private void SetMapProvider(Boolean readOnly)
		{
			Int32 count = m_MapProvider.Count;
			MapProvider provider = m_MapProvider;
			if (readOnly)
			{
				var pr = (SortedMapProvider)m_MapProvider;
				if (pr.LastKey <= MAXINDEX)
				{
					provider = new IndexMapProvider();
				}
				else if (count <= MAXARRAYCOUNT)
				{
					provider = new ArrayMapProvider(m_MapProvider.Count);
				}
			}
			else
			{
				if (!(provider is SortedMapProvider))
				{
					provider = new SortedMapProvider(m_MapProvider.Count);
				}
			}
			if (provider != m_MapProvider)
			{
				for (Int32 i = 0; i < count; i++)
				{
					Entry entry = m_MapProvider.GetEntry(i);
					provider.Add(entry.Key, entry.Value);
				}
				m_MapProvider = provider;
			}
		}

		private struct Entry
		{
			internal Entry(Int32 key, Object value)
			{
				Key = key;
				Value = value;
			}

			internal Int32 Key;
			internal Object Value;
		}

		private abstract class MapProvider
		{
			internal Int32 Count
			{
				get { return m_Count; }
			}

			internal abstract Object this[Int32 key]
			{
				get;
			}

			internal abstract Entry GetEntry(Int32 index);

			internal abstract void Add(Int32 key, Object value);

			internal virtual void Remove(Int32 key)
			{
				throw new InvalidOperationException();
			}

			protected Int32 m_Count;        // Entry count in the collection.
		}

		private class SortedMapProvider : MapProvider
		{
			internal SortedMapProvider(Int32 capacity)
			{
				m_Entries = new Entry[capacity];
			}

			internal Int32 LastKey
			{
				get { return m_LastKey; }
			}

			internal override Object this[Int32 key]
			{
				get
				{
					Int32 minIndex = 0;
					Int32 maxIndex = m_Count - 1;
					if (maxIndex >= 0 && key <= m_LastKey)
					{
						do
						{
							Int32 midIndex = (maxIndex + minIndex) / 2;
							if (key <= m_Entries[midIndex].Key)
							{
								maxIndex = midIndex;
							}
							else
							{
								minIndex = midIndex + 1;
							}
						} while (minIndex < maxIndex);
						if (key == m_Entries[minIndex].Key)
						{
							return m_Entries[minIndex].Value;
						}
					}
					return null;
				}
			}

			internal override Entry GetEntry(Int32 index)
			{
				return m_Entries[index];
			}

			internal override void Add(Int32 key, Object value)
			{
				Boolean found;
				Int32 index = FindInsertIndex(key, out found);
				if (found)
				{
					m_Entries[index].Value = value;
					return;
				}
				if (m_Count >= m_Entries.Length)
				{
					var entries = new Entry[m_Entries.Length + GROWTH];
					Array.Copy(m_Entries, 0, entries, 0, m_Entries.Length);
					m_Entries = entries;
				}
				if (index < m_Count)
				{
					Array.Copy(m_Entries, index, m_Entries, index + 1, m_Count - index);
				}
				else
				{
					m_LastKey = key;
				}
				m_Entries[index].Key = key;
				m_Entries[index].Value = value;
				m_Count++;
			}

			internal override void Remove(Int32 key)
			{
				Int32 index = FindIndex(key);
				if (index >= 0)
				{
					Int32 tailSize = (m_Count - 1) - index;
					if (tailSize > 0)
					{
						Array.Copy(m_Entries, index + 1, m_Entries, index, tailSize);
					}
					else if (m_Count > 1)
					{
						m_LastKey = m_Entries[m_Count - 2].Key;
					}
					else
					{
						m_LastKey = INVALIDKEY;
					}
					m_Count--;
					m_Entries[m_Count].Key = INVALIDKEY;
					m_Entries[m_Count].Value = null;
				}
			}

			private Int32 FindIndex(Int32 key)
			{
				Int32 minIndex = 0;
				if (m_Count > 0 && key <= m_LastKey)
				{
					Int32 maxIndex = m_Count - 1;
					do
					{
						Int32 midIndex = (maxIndex + minIndex) / 2;
						if (key <= m_Entries[midIndex].Key)
						{
							maxIndex = midIndex;
						}
						else
						{
							minIndex = midIndex + 1;
						}
					} while (minIndex < maxIndex);
					if (key == m_Entries[minIndex].Key)
					{
						return minIndex;
					}
				}
				return -1;
			}

			private Int32 FindInsertIndex(Int32 key, out Boolean found)
			{
				Int32 minIndex = 0;
				if (m_Count > 0 && key <= m_LastKey)
				{
					Int32 maxIndex = m_Count - 1;
					do
					{
						Int32 midIndex = (maxIndex + minIndex) / 2;
						if (key <= m_Entries[midIndex].Key)
						{
							maxIndex = midIndex;
						}
						else
						{
							minIndex = midIndex + 1;
						}
					} while (minIndex < maxIndex);
					found = (key == m_Entries[minIndex].Key);
					return minIndex;
				}
				found = false;
				return m_Count;
			}

			private Entry[] m_Entries; // Array of entries.
			private Int32 m_LastKey;      // Bigest key number.
		}

		private class IndexMapProvider : MapProvider
		{
			internal IndexMapProvider()
			{
				m_Array = new Object[MAXINDEX + 1];
				for (Int32 i = m_Array.Length; --i >= 0; )
				{
					m_Array[i] = Unassigned.Value;
				}
			}

			internal override Object this[Int32 key]
			{
				get
				{
					if (key >= m_Array.Length || key < 0)
					{
						return null;
					}
					return m_Array[key];
				}
			}

			internal override Entry GetEntry(Int32 index)
			{
				Int32 idx = -1;
				for (Int32 i = 0; i < m_Array.Length; i++)
				{
					Object value = m_Array[i];
					if (value != Unassigned.Value)
					{
						idx++;
					}
					if (idx == index)
					{
						return new Entry(i, value);
					}
				}
				return new Entry();
			}

			internal override void Add(Int32 key, Object value)
			{
				m_Array[key] = value;
				m_Count++;
			}

			private readonly Object[] m_Array; // Array of entries.			
		}

		private class ArrayMapProvider : MapProvider
		{
			internal ArrayMapProvider(Int32 capacity)
			{
				m_Entries = new Entry[capacity];
			}

			internal override Object this[Int32 key]
			{
				get
				{
					for (Int32 i = m_Count; --i >= 0; )
					{
						Entry entry = m_Entries[i];
						Int32 entryKey = entry.Key;

						if (entryKey > key)
						{
							continue;
						}

						if (entryKey == key)
						{
							return entry.Value;
						}

						if (entryKey < key)
						{
							return null;
						}
					}
					return null;
				}
			}

			internal override Entry GetEntry(Int32 index)
			{
				return m_Entries[index];
			}

			internal override void Add(Int32 key, Object value)
			{
				m_Entries[m_Count].Key = key;
				m_Entries[m_Count].Value = value;
				m_Count++;
			}

			private readonly Entry[] m_Entries; // Array of entries.
		}

		private sealed class Unassigned
		{
			private Unassigned()
			{ }

			internal readonly static Unassigned Value = new Unassigned();
		}

		private const Int32 MAXINDEX = 255;
		private const Int32 GROWTH = 32;
		private const Int32 MINSIZE = 32;
		private const Int32 MAXARRAYCOUNT = 12;
		private const Int32 INVALIDKEY = Int32.MaxValue;

		private Boolean m_Readonly;
		private MapProvider m_MapProvider;
	}
}
