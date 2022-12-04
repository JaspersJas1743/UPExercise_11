using Microsoft.Win32;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Задание__11.Assets.MyWindows
{
	public partial class SaveFileWindow : Window
	{
		private const string _defaultFileName = "Безымянный.txt";

		public SaveFileWindow()
			=> InitializeComponent();

		public string EnteredEncodingStringFormat { get; private set; } = String.Empty;
		public Encoding EnteredEncoding { get; private set; } = Encoding.Default;
		public string EnteredFileName { get; private set; } = String.Empty;
		public string EnteredFileFullName { get; private set; } = String.Empty;

		private void SetEncoding(Encoding encoding, MenuItem menuItem)
		{
			EnteredEncoding = encoding;
			MainHeader.Header = menuItem.Header;
			EnteredEncodingStringFormat = menuItem.Header.ToString();
		}

		private void Windows1251Click(object sender, RoutedEventArgs e)
			=> SetEncoding(encoding: Encoding.GetEncoding("windows-1251"), menuItem: (MenuItem)sender);

		private void UnicodeClick(object sender, RoutedEventArgs e)
			=> SetEncoding(encoding: Encoding.Unicode, menuItem: (MenuItem)sender);

		private void ClickForSelectFile(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFile = new SaveFileDialog
			{
				FileName = String.Empty,
				DefaultExt = ".rtf",
				Filter = "Текстовый файл|*.txt|Файлы RTF|*.rtf",
				Title = "Сохранить файл",
				ValidateNames = true
			};
			if(saveFile.ShowDialog() ?? false)
			{
				EnteredFileName = FileNameBox.Text = saveFile.SafeFileName;
				EnteredFileFullName = saveFile.FileName;
			} else
				EnteredFileFullName = EnteredFileName = FileNameBox.Text = _defaultFileName;
		}

		private void SelectedClick(object sender, RoutedEventArgs e)
			=> DialogResult = !String.IsNullOrEmpty(EnteredFileName) && Encoding.Default != EnteredEncoding;
	}
}
