using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Задание__11.Assets.MyWindows;

namespace Задание__11
{
	public partial class MainWindow : Window
	{
		private string _currentFile = string.Empty;
		private Encoding _currentEncoding = Encoding.Default;

		public MainWindow()
		{
			InitializeComponent();
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		}

		private void SaveAsFileButtonClick(object sender, RoutedEventArgs e)
		{
			SaveFileWindow saveFileWindow = new SaveFileWindow();
			if(saveFileWindow.ShowDialog() == false)
				return;
			string file = _currentFile = saveFileWindow.EnteredFileFullName;
			Encoding encoding = saveFileWindow.EnteredEncoding;
			Task task = File.WriteAllTextAsync(path: file, MainTextBox.Text, encoding: encoding);
			Application.Current.MainWindow.Title = saveFileWindow.EnteredFileName;
			EncodingBlock.Text = $"Кодировка: {saveFileWindow.EnteredEncodingStringFormat}";
			task.GetAwaiter();
		}

		private void OpenFileButtonClick(object sender, RoutedEventArgs e)
		{
			OpenFileWindow openFileWindow = new OpenFileWindow();
			if(openFileWindow.ShowDialog() == false)
				return;
			string file = _currentFile = openFileWindow.EnteredFileFullName;
			Encoding encoding = openFileWindow.EnteredEncoding;
			Task<string> textFromFile = File.ReadAllTextAsync(path: file, encoding: encoding);
			Application.Current.MainWindow.Title = openFileWindow.EnteredFileName;
			EncodingBlock.Text = $"Кодировка: {openFileWindow.EnteredEncodingStringFormat}";
			MainTextBox.Text = textFromFile.GetAwaiter().GetResult();
		}

		private void SaveFileButtonClick(object sender, RoutedEventArgs e)
			=> File.WriteAllTextAsync(path: _currentFile, MainTextBox.Text, encoding: _currentEncoding).GetAwaiter();
	}
}
