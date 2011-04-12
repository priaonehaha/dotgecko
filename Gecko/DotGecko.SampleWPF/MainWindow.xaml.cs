using System;
using System.Windows;

namespace DotGecko.SampleWPF
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Loaded += OnLoaded;
		}

		private void OnLoaded(Object sender, RoutedEventArgs routedEventArgs)
		{
			browserElement.Browser.LoadUri("http://www.google.com/");
		}
	}
}
