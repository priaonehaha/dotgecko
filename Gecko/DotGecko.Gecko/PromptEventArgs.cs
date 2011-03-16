using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DotGecko.Gecko.Interop;

namespace DotGecko.Gecko
{
	public class DialogEventArgs : EventArgs
	{
		internal DialogEventArgs(String dialogTitle, String text)
		{
			m_DialogTitle = dialogTitle;
			m_Text = text;
		}

		public String DialogTitle
		{
			get { return m_DialogTitle; }
		}

		public String Text
		{
			get { return m_Text; }
		}

		private readonly String m_DialogTitle;
		private readonly String m_Text;
	}

	public class AlertEventArgs : DialogEventArgs
	{
		internal AlertEventArgs(String dialogTitle, String text, String checkMsg, Boolean checkState)
			: base(dialogTitle, text)
		{
			m_CheckMsg = checkMsg;
			CheckState = checkState;
		}

		public String CheckMsg
		{
			get { return m_CheckMsg; }
		}

		public Boolean CheckState { get; set; }

		private readonly String m_CheckMsg;
	}

	public sealed class ConfirmEventArgs : AlertEventArgs
	{
		internal ConfirmEventArgs(String dialogTitle, String text, String checkMsg, Boolean checkState)
			: this(dialogTitle, text, nsIPromptServiceConstants.STD_OK_CANCEL_BUTTONS, null, null, null, checkMsg, checkState)
		{ }

		internal ConfirmEventArgs(String dialogTitle, String text,
			UInt32 buttonFlags, String button1Title, String button2Title, String button3Title,
			String checkMsg, Boolean checkState)
			: base(dialogTitle, text, checkMsg, checkState)
		{
			m_Buttons = new List<Button>(3);
			m_ReadOnlyButtons = new ReadOnlyCollection<Button>(m_Buttons);

			var title = new[] { button1Title, button2Title, button3Title };
			for (Int32 i = 0; i < 3; ++i)
			{
				UInt32 flags = (buttonFlags >> (i * 8)) & 0xFF;
				if (flags != 0)
				{
					m_Buttons.Add(new Button((ButtonKind)flags, title[i]));
				}
			}

			if (buttonFlags.HasFlag(nsIPromptServiceConstants.BUTTON_POS_2_DEFAULT))
			{
				m_DefaultButton = 2;
			}
			else if (buttonFlags.HasFlag(nsIPromptServiceConstants.BUTTON_POS_1_DEFAULT))
			{
				m_DefaultButton = 1;
			}
			else
			{
				m_DefaultButton = 0;
			}

			m_DelayEnable = buttonFlags.HasFlag(nsIPromptServiceConstants.BUTTON_DELAY_ENABLE);
		}

		public ReadOnlyCollection<Button> Buttons
		{
			get { return m_ReadOnlyButtons; }
		}

		public Byte DefaultButton
		{
			get { return m_DefaultButton; }
		}

		public Boolean DelayEnable
		{
			get { return m_DelayEnable; }
		}

		public Int32 ButtonPressed { get; set; }

		public enum ButtonKind : uint
		{
			Ok = nsIPromptServiceConstants.BUTTON_TITLE_OK,
			Cancel = nsIPromptServiceConstants.BUTTON_TITLE_CANCEL,
			Yes = nsIPromptServiceConstants.BUTTON_TITLE_YES,
			No = nsIPromptServiceConstants.BUTTON_TITLE_NO,
			Save = nsIPromptServiceConstants.BUTTON_TITLE_SAVE,
			DontSave = nsIPromptServiceConstants.BUTTON_TITLE_DONT_SAVE,
			Revert = nsIPromptServiceConstants.BUTTON_TITLE_REVERT,
			Custom = nsIPromptServiceConstants.BUTTON_TITLE_IS_STRING
		}

		public struct Button
		{
			internal Button(ButtonKind kind, String title)
			{
				m_Kind = kind;
				m_Title = title;
			}

			public ButtonKind Kind
			{
				get { return m_Kind; }
			}

			public String Title
			{
				get { return m_Title; }
			}

			private readonly ButtonKind m_Kind;
			private readonly String m_Title;
		}

		private readonly List<Button> m_Buttons;
		private readonly ReadOnlyCollection<Button> m_ReadOnlyButtons;
		private readonly Byte m_DefaultButton;
		private readonly Boolean m_DelayEnable;
	}

	public sealed class PromptEventArgs : AlertEventArgs
	{
		internal PromptEventArgs(String dialogTitle, String text, String value, String checkMsg, Boolean checkState)
			: base(dialogTitle, text, checkMsg, checkState)
		{
			Value = value;
		}

		public String Value { get; set; }

		public Boolean Result { get; set; }
	}

	public sealed class PromptUsernameAndPasswordEventArgs : AlertEventArgs
	{
		internal PromptUsernameAndPasswordEventArgs(String dialogTitle, String text, String userName, String password, String checkMsg, Boolean checkState)
			: base(dialogTitle, text, checkMsg, checkState)
		{
			UserName = userName;
			Password = password;
		}

		public String UserName { get; set; }

		public String Password { get; set; }

		public Boolean Result { get; set; }
	}

	public sealed class SelectEventArgs : DialogEventArgs
	{
		internal SelectEventArgs(String dialogTitle, String text, IEnumerable<String> items)
			: base(dialogTitle, text)
		{
			m_Items = new List<String>(items);
			m_ReadOnlyItems = new ReadOnlyCollection<String>(m_Items);
		}

		public ReadOnlyCollection<String> Items
		{
			get { return m_ReadOnlyItems; }
		}

		public Int32 SelectedIndex { get; set; }

		public Boolean Result { get; set; }

		private readonly List<String> m_Items;
		private readonly ReadOnlyCollection<String> m_ReadOnlyItems;
	}

	public enum AuthLevel : uint
	{
		None = nsIAuthPrompt2Constants.LEVEL_NONE,
		PasswordEncrypted = nsIAuthPrompt2Constants.LEVEL_PW_ENCRYPTED,
		Secure = nsIAuthPrompt2Constants.LEVEL_SECURE
	}

	[Flags]
	public enum AuthFlags : uint
	{
		AuthHost = nsIAuthInformationConstants.AUTH_HOST,
		AuthProxy = nsIAuthInformationConstants.AUTH_PROXY,
		NeedDomain = nsIAuthInformationConstants.NEED_DOMAIN,
		OnlyPassword = nsIAuthInformationConstants.ONLY_PASSWORD,
		PreviousFailed = nsIAuthInformationConstants.PREVIOUS_FAILED
	}

	public class PromptAuthEventArgs : CancelEventArgs
	{
		internal PromptAuthEventArgs(AuthLevel authLevel, nsIAuthInformation authInformation, String checkMsg, Boolean checkState)
			: base(false)
		{
			m_AuthLevel = authLevel;
			m_AuthFlags = (AuthFlags)authInformation.Flags;
			m_Realm = XpcomStringHelper.Get(authInformation.GetRealm);
			m_AuthScheme = XpcomStringHelper.Get(authInformation.GetAuthenticationScheme);
			Username = XpcomStringHelper.Get(authInformation.GetUsername);
			Password = XpcomStringHelper.Get(authInformation.GetPassword);
			Domain = XpcomStringHelper.Get(authInformation.GetDomain);
			m_CheckMsg = checkMsg;
			CheckState = checkState;
		}

		public AuthLevel Level
		{
			get { return m_AuthLevel; }
		}

		public AuthFlags Flags
		{
			get { return m_AuthFlags; }
		}

		public String Realm
		{
			get { return m_Realm; }
		}

		public String Scheme
		{
			get { return m_AuthScheme; }
		}

		public String Username { get; set; }

		public String Password { get; set; }

		public String Domain { get; set; }

		public String CheckMsg
		{
			get { return m_CheckMsg; }
		}

		public Boolean CheckState { get; set; }

		private readonly AuthLevel m_AuthLevel;
		private readonly AuthFlags m_AuthFlags;
		private readonly String m_Realm;
		private readonly String m_AuthScheme;
		private readonly String m_CheckMsg;
	}

	public sealed class AsyncPromptAuthEventArgs : PromptAuthEventArgs, nsICancelable
	{
		internal AsyncPromptAuthEventArgs(nsIAuthPromptCallback callback, Object context, AuthLevel authLevel, nsIAuthInformation authInformation, String checkMsg, Boolean checkState)
			: base(authLevel, authInformation, checkMsg, checkState)
		{
			m_AuthCallback = callback;
			m_Context = context;
			m_AuthInformation = authInformation;
		}

		public void Commit()
		{
			m_AuthInformation.SetUsername(Username);
			m_AuthInformation.SetPassword(Password);
			m_AuthInformation.SetDomain(Domain);
			m_AuthCallback.OnAuthAvailable(m_Context, m_AuthInformation);
		}

		new public void Cancel()
		{
			m_AuthCallback.OnAuthCancelled(m_Context, true);
		}

		public event EventHandler<EventArgs> CancelPrompt;

		#region Implementation of nsICancelable

		void nsICancelable.Cancel(nsResult aReason)
		{
			var cancelPrompt = CancelPrompt;
			if (cancelPrompt != null)
			{
				cancelPrompt(this, EventArgs.Empty);
			}
			m_AuthCallback.OnAuthCancelled(m_Context, false);
		}

		#endregion

		private readonly nsIAuthPromptCallback m_AuthCallback;
		private readonly Object m_Context;
		private readonly nsIAuthInformation m_AuthInformation;
	}
}
