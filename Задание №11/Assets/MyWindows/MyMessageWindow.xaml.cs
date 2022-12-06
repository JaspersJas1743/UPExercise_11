using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.IO;
using System;

namespace Задание__11.Assets.MyWindows
{
	public partial class MyMessageWindow : Window
	{
		public MyMessageWindow()
			=> InitializeComponent();

		private void DragMove(object sender, MouseButtonEventArgs e)
		{
			if(Mouse.LeftButton == MouseButtonState.Pressed)
				this.DragMove();
		}

		private void YesClick(object sender, RoutedEventArgs e)
			=> DialogResult = true;

		private void NoClick(object sender, RoutedEventArgs e)
			=> DialogResult = false;

		private void SetImage(string path)
			=> MainImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"..\..\..\.." + path));

		private void SetText(string text)
			=> MainText.Text = text;

		internal static bool Show(string text, string pathToImage)
		{
			MyMessageWindow myMessageWindow = new MyMessageWindow();
			myMessageWindow.SetText(text: text);
			myMessageWindow.SetImage(path: pathToImage);
			return myMessageWindow.ShowDialog() ?? false;
		}

		private void DeactivateClick(object sender, RoutedEventArgs e)
			=> this.WindowState = WindowState.Minimized;
	}
}
