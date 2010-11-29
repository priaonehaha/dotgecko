using System;
using System.Text;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko.Dom
{
	public class DomCharacterData : DomNode
	{
		internal DomCharacterData(nsIDOMCharacterData domCharacterData)
			: base(domCharacterData)
		{
			m_DomCharacterData = domCharacterData;
		}

		internal static DomCharacterData Create(nsIDOMCharacterData domCharacterData)
		{
			if (domCharacterData == null)
			{
				return null;
			}

			if (domCharacterData is nsIDOMText)
			{
				return DomText.Create((nsIDOMText)domCharacterData);
			}
			
			if (domCharacterData is nsIDOMComment)
			{
				return DomComment.Create((nsIDOMComment)domCharacterData);
			}

			return new DomCharacterData(domCharacterData);
		}

		public String Data
		{
			get { return XpcomStringHelper.Get(m_DomCharacterData.GetData); }
			set { m_DomCharacterData.SetData(value); }
		}
		
		public UInt32 Length { get { return m_DomCharacterData.Length; } }

		public String SubstringData(UInt32 offset, UInt32 count)
		{
			String retval = XpcomStringHelper.Get(m_DomCharacterData.SubstringData, offset, count);
			return retval;
		}

		public void AppendData(String arg)
		{
			m_DomCharacterData.AppendData(arg);
		}

		public void InsertData(UInt32 offset, String arg)
		{
			m_DomCharacterData.InsertData(offset, arg);
		}

		public void DeleteData(UInt32 offset, UInt32 count)
		{
			m_DomCharacterData.DeleteData(offset, count);
		}

		public void ReplaceData(UInt32 offset, UInt32 count, String arg)
		{
			m_DomCharacterData.ReplaceData(offset, count, arg);
		}

		private readonly nsIDOMCharacterData m_DomCharacterData;
	}
}
