using System;
using System.Windows.Forms;
using DotGecko.Controls;

namespace DotGecko.SampleWinForms
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();

			browserControl = new WebBrowserControl();
			browserControl.Width = 720;
			browserControl.Height = 405;
			this.Controls.Add(browserControl);

			Activated += OnActivated;
		}

		private void OnActivated(Object sender, EventArgs eventArgs)
		{
			browserControl.Browser.LoadUri("http://www.google.com/");
		}

		private readonly WebBrowserControl browserControl;
	}
}
