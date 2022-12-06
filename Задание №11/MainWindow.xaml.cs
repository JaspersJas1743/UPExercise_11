using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WindowsInput;
using WindowsInput.Native;
using Задание__11.Assets.MyWindows;
using Задание__11.Assets.Utility;

namespace Задание__11
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Encoding.RegisterProvider(provider: CodePagesEncodingProvider.Instance);
			MainTextBox.Focus();
		}

		private void DragMove(object sender, MouseButtonEventArgs e)
		{
			if(Mouse.LeftButton == MouseButtonState.Pressed)
				Application.Current.MainWindow.DragMove();
		}

		private void ExitClick(object sender, RoutedEventArgs e)
		{
			if(AskUser(question: "Сохранить текущий файл?"))
				ActionWithFile(dialogWindow: new SaveFileWindow(), action: PickedFile.Save);
			Application.Current.Shutdown();
		}

		private void DeactivateClick(object sender, RoutedEventArgs e)
			=> Application.Current.MainWindow.WindowState = WindowState.Minimized;

		private void SaveAsFileButtonClick(object sender, RoutedEventArgs e)
			=> ActionWithFile(dialogWindow: new SaveFileWindow(), action: PickedFile.Save);

		private void OpenFileButtonClick(object sender, RoutedEventArgs e)
			=> ActionWithFile(dialogWindow: new OpenFileWindow(), action: PickedFile.Open);

		private void SaveFileButtonClick(object sender, RoutedEventArgs e)
			=> PickedFile.Save(richTextBox: MainTextBox);

		private void PrintFileButtonClick(object sender, RoutedEventArgs e)
		{
			PrintDialog printDialog = new PrintDialog();
			if(printDialog.ShowDialog() ?? false)
				printDialog.PrintVisual(visual: (Visual)MainTextBox, description: "Печать");
		}

		private void ActionWithFile(Window dialogWindow, Action<RichTextBox> action)
		{
			if(!dialogWindow.ShowDialog() ?? false)
				return;
			action(obj: MainTextBox);
			if (PickedFile.FileName.EndsWith(".txt"))
				EncodingBlock.Text = $"Кодировка: {PickedFile.EncodingStringFormat}";
			CurrentFile.Text = $"Текущий файл: {PickedFile.FileName}";
		}

		private void NewFileButtonClick(object sender, RoutedEventArgs e)
		{
			if(AskUser(question: "Сохранить текущий файл?"))
				ActionWithFile(dialogWindow: new SaveFileWindow(), action: PickedFile.Save);
			MainTextBox.Document.Blocks.Clear();
			PickedFile.FileFullName = PickedFile.FileName = "Безымянный";
			EncodingBlock.Text = PickedFile.EncodingStringFormat = String.Empty;
			PickedFile.LeadToEncoding = Encoding.Default;
			CurrentFile.Text = $"Текущий файл: {PickedFile.FileName}";
		}

		private static bool AskUser(string question)
			=> MyMessageWindow.Show(text: question, @"\Assets\Images\question.png");

		private void OneKeyClick(object sender, RoutedEventArgs e)
		{
			MainTextBox.Focus();
			new InputSimulator().Keyboard.ModifiedKeyStroke(
				modifierKeyCode: VirtualKeyCode.CONTROL,
				keyCode: (VirtualKeyCode)Enum.Parse(enumType: typeof(VirtualKeyCode), value: ((Button)sender).Name)
			);
		}

		private void SecondKeyClick(object sender, RoutedEventArgs e)
		{
			MainTextBox.Focus();
			new InputSimulator().Keyboard.ModifiedKeyStroke(
				modifierKeyCodes: new[] { VirtualKeyCode.CONTROL, VirtualKeyCode.SHIFT }, keyCode: (VirtualKeyCode)Enum.Parse(
					enumType: typeof(VirtualKeyCode), value: String.Concat(values: ((Button)sender).Name.SkipLast(count: 1))
				)
			);
		}

		private void DateButton_Click(object sender, RoutedEventArgs e)
			=> MainTextBox.AppendText(textData: DateTime.Now.ToString(format: " dd.MM.yyyy"));

		private void TimeButton_Click(object sender, RoutedEventArgs e)
			=> MainTextBox.AppendText(textData: DateTime.Now.ToString(format: " HH:mm:ss"));
	}
}
