using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public sealed class PreferencesBranch
	{
		internal PreferencesBranch(nsIPrefBranch prefBranch)
		{
			m_PrefBranch = prefBranch;
		}

		public String Root
		{
			get { return PrefBranch.Root; }
		}

		[IndexerName("Values")]
		public Object this[String prefName]
		{
			get
			{
				Object value;
				if (!TryGetValue(prefName, out value))
				{
					throw new ArgumentException("Invalid preference type", "prefName");
				}
				return value;
			}
			set
			{
				Int32 currentPrefType = PrefBranch.GetPrefType(prefName);
				Int32 newPrefType = GetValueType(value);

				if ((currentPrefType != nsIPrefBranchConstants.PREF_INVALID) && (currentPrefType != newPrefType))
				{
					throw new InvalidCastException("Invalid value type");
				}

				switch (currentPrefType)
				{
					case nsIPrefBranchConstants.PREF_STRING:
						PrefBranch.SetCharPref(prefName, (String)value);
						break;
					case nsIPrefBranchConstants.PREF_INT:
						PrefBranch.SetIntPref(prefName, (Int32)value);
						break;
					case nsIPrefBranchConstants.PREF_BOOL:
						PrefBranch.SetBoolPref(prefName, (Boolean)value ? -1 : 0);
						break;
				}
			}
		}

		public IList<String> ChildList
		{
			get
			{
				UInt32 count;
				IntPtr ptr = PrefBranch.GetChildList(String.Empty, out count);
				var values = new String[count];
				for (UInt32 i = 0; i < count; ++i)
				{
					IntPtr pStr = Marshal.ReadIntPtr(ptr, (Int32)i * IntPtr.Size);
					String str = Marshal.PtrToStringAnsi(pStr);
					values[i] = str;
				}
				return Array.AsReadOnly(values);
			}
		}

		public Object GetValue(String prefName, Object defaultValue)
		{
			Object value;
			return TryGetValue(prefName, out value) ? value : defaultValue;
		}

		public Boolean TryGetValue(String prefName, out Object value)
		{
			Int32 currentPrefType = PrefBranch.GetPrefType(prefName);
			switch (currentPrefType)
			{
				case nsIPrefBranchConstants.PREF_STRING:
					value = PrefBranch.GetCharPref(prefName);
					return true;
				case nsIPrefBranchConstants.PREF_INT:
					value = PrefBranch.GetIntPref(prefName);
					return true;
				case nsIPrefBranchConstants.PREF_BOOL:
					value = PrefBranch.GetBoolPref(prefName);
					return true;
				default:
					value = null;
					return false;
			}
		}

		public Boolean HasUserValue(String prefName)
		{
			return PrefBranch.PrefHasUserValue(prefName);
		}

		public void ClearUserValue(String prefName)
		{
			PrefBranch.ClearUserPref(prefName);
		}

		public Boolean GetIsLocked(String prefName)
		{
			return PrefBranch.PrefIsLocked(prefName);
		}

		public void SetIsLocked(String prefName, Boolean locked)
		{
			if (locked)
			{
				PrefBranch.LockPref(prefName);
			}
			else
			{
				PrefBranch.UnlockPref(prefName);
			}
		}

		private nsIPrefBranch PrefBranch
		{
			get { return m_PrefBranch; }
		}

		private static Int32 GetValueType(Object value)
		{
			if (value == null)
			{
				return nsIPrefBranchConstants.PREF_INVALID;
			}
			if (value is String)
			{
				return nsIPrefBranchConstants.PREF_STRING;
			}
			if (value is Int32)
			{
				return nsIPrefBranchConstants.PREF_INT;
			}
			if (value is Boolean)
			{
				return nsIPrefBranchConstants.PREF_BOOL;
			}

			throw new ArgumentException("Value type can be String, Int32 or Boolean", "value");
		}

		private readonly nsIPrefBranch m_PrefBranch;
	}
}
