using System;
using System.ComponentModel;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	[Flags]
	public enum RequestState : uint
	{
		Start = nsIWebProgressListenerConstants.STATE_START,
		Redirecting = nsIWebProgressListenerConstants.STATE_REDIRECTING,
		Transferring = nsIWebProgressListenerConstants.STATE_TRANSFERRING,
		Negotiating = nsIWebProgressListenerConstants.STATE_NEGOTIATING,
		Stop = nsIWebProgressListenerConstants.STATE_STOP,

		StateIsRequest = nsIWebProgressListenerConstants.STATE_IS_REQUEST,
		StateIsDocument = nsIWebProgressListenerConstants.STATE_IS_DOCUMENT,
		StateIsNetwork = nsIWebProgressListenerConstants.STATE_IS_NETWORK,
		StateIsWindow = nsIWebProgressListenerConstants.STATE_IS_WINDOW,

		StateRestoring = nsIWebProgressListenerConstants.STATE_RESTORING,
	}

	[Flags]
	public enum SecurityState : uint
	{
		IsInsecure = nsIWebProgressListenerConstants.STATE_IS_INSECURE,
		IsBroken = nsIWebProgressListenerConstants.STATE_IS_BROKEN,
		IsSecure = nsIWebProgressListenerConstants.STATE_IS_SECURE,

		SecureHigh = nsIWebProgressListenerConstants.STATE_SECURE_HIGH,
		SecureMed = nsIWebProgressListenerConstants.STATE_SECURE_MED,
		SecureLow = nsIWebProgressListenerConstants.STATE_SECURE_LOW,

		IdentityEvToplevel = nsIWebProgressListenerConstants.STATE_IDENTITY_EV_TOPLEVEL,
	}

	public sealed class RequestStateChangeEventArgs : CancelEventArgs
	{
		internal RequestStateChangeEventArgs(RequestState requestState)
			: base(false)
		{
			m_RequestState = requestState;
		}

		public RequestState State
		{
			get { return m_RequestState; }
		}

		private readonly RequestState m_RequestState;
	}

	public sealed class RequestProgressChangeEventArgs : CancelEventArgs
	{
		internal RequestProgressChangeEventArgs(Int64 curSelfProgress, Int64 maxSelfProgress, Int64 curTotalProgress, Int64 maxTotalProgress)
			: base(false)
		{
			this.m_CurSelfProgress = curSelfProgress;
			this.m_MaxSelfProgress = maxSelfProgress;
			this.m_CurTotalProgress = curTotalProgress;
			this.m_MaxTotalProgress = maxTotalProgress;
		}

		public Int64 CurSelfProgress
		{
			get { return this.m_CurSelfProgress; }
		}

		public Int64 MaxSelfProgress
		{
			get { return this.m_MaxSelfProgress; }
		}

		public Int64 CurTotalProgress
		{
			get { return this.m_CurTotalProgress; }
		}

		public Int64 MaxTotalProgress
		{
			get { return this.m_MaxTotalProgress; }
		}

		private readonly Int64 m_CurSelfProgress;
		private readonly Int64 m_MaxSelfProgress;
		private readonly Int64 m_CurTotalProgress;
		private readonly Int64 m_MaxTotalProgress;
	}

	public sealed class LocationChangeEventArgs : CancelEventArgs
	{
		internal LocationChangeEventArgs(Uri location)
			: base(false)
		{
			m_Location = location;
		}

		public Uri Location
		{
			get { return m_Location; }
		}

		private readonly Uri m_Location;
	}

	public sealed class RequestStatusChangeEventArgs : CancelEventArgs
	{
		internal RequestStatusChangeEventArgs(UInt32 status, String message)
			: base(false)
		{
			m_Status = status;
			m_Message = message;
		}

		public UInt32 Status
		{
			get { return m_Status; }
		}

		public String Message
		{
			get { return m_Message; }
		}

		private readonly UInt32 m_Status;
		private readonly String m_Message;
	}

	public sealed class SecurityChangeEventArgs : CancelEventArgs
	{
		internal SecurityChangeEventArgs(SecurityState securityState)
			: base(false)
		{
			m_SecurityState = securityState;
		}

		public SecurityState SecurityState
		{
			get { return m_SecurityState; }
		}

		private readonly SecurityState m_SecurityState;
	}

	public sealed class RefreshAttemptedEventArgs : CancelEventArgs
	{
		internal RefreshAttemptedEventArgs(Uri refreshUri, Int32 delay, Boolean sameUri)
			: base(false)
		{
			m_RefreshUri = refreshUri;
			m_Delay = delay;
			m_SameUri = sameUri;
		}

		public Uri RefreshUri { get { return m_RefreshUri; } }

		public Int32 Delay { get { return m_Delay; } }

		public Boolean SameUri { get { return m_SameUri; } }

		private readonly Uri m_RefreshUri;
		private readonly Int32 m_Delay;
		private readonly Boolean m_SameUri;
	}
}
