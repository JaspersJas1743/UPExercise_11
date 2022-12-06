using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using Задание__11.Assets.Utility;

namespace Задание__11.Assets.MyWindows
{
    public partial class OpenFileWindow : Window
	{
		public OpenFileWindow()
			=> InitializeComponent();

		private void DragMove(object sender, MouseButtonEventArgs e)
		{
			if(Mouse.LeftButton == MouseButtonState.Pressed)
				this.DragMove();
		}

		private void DeactivateClick(object sender, RoutedEventArgs e)
			=> this.WindowState = WindowState.Minimized;

		private void ClickForSelectFile(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFile = new OpenFileDialog
			{
				FileName = String.Empty,
				DefaultExt = ".rtf",
				Filter = "Текстовый файл|*.txt|Файл RTF|*.rtf|Бинарный файл|*.bin",
				Title = "Открыть файл",
				ValidateNames = true
			};
			if(!openFile.ShowDialog() ?? false)
			{
				MyMessageWindow.Show(text: "Выберите файл!", pathToImage: @"\Assets\Images\error.png");
				return;
			}
			PickedFile.FileName = FileNameBox.Text = openFile.SafeFileName;
			PickedFile.FileFullName = openFile.FileName;
			if(PickedFile.FileName.EndsWith(value: ".txt"))
				UICreator.CreateSelectionEncoding(grid: MainGrid, button: SelectedButton, window: this);
		}

		private void SelectedClick(object sender, RoutedEventArgs e)
			=> DialogResult = !String.IsNullOrWhiteSpace(value: PickedFile.FileName);

	}
}
