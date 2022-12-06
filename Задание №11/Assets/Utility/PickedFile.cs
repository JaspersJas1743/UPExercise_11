using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Задание__11.Assets.Utility
{
	internal static class PickedFile
	{
		public static string EncodingStringFormat { get; set; } = String.Empty;
		public static Encoding LeadToEncoding { get; set; } = Encoding.Default;
		public static string FileName { get; set; } = String.Empty;
		public static string FileFullName { get; set; } = String.Empty;

		private static readonly Dictionary<string, Action<RichTextBox>> _openActions = new Dictionary<string, Action<RichTextBox>>
		{
			["txt"] = richTextBox => richTextBox.AppendText(textData: File.ReadAllText(path: FileFullName, encoding: LeadToEncoding)),
			["bin"] = richTextBox => richTextBox.AppendText(textData: Encoding.Default.GetString(bytes: File.ReadAllBytes(path: FileFullName))),
			["rtf"] = richTextBox => richTextBox.Selection.Load(stream: File.Open(path: FileFullName, mode: FileMode.OpenOrCreate), DataFormats.Rtf)
		};

		private static readonly Dictionary<string, Action<RichTextBox>> _saveActions = new Dictionary<string, Action<RichTextBox>>
		{
			["txt"] = richTextBox => File.WriteAllText(path: FileFullName, contents: GetRange(richTextBox: richTextBox).Text, encoding: LeadToEncoding),
			["bin"] = richTextBox => File.WriteAllBytes(path: FileFullName, bytes: LeadToEncoding.GetBytes(s: GetRange(richTextBox: richTextBox).Text)),
			["rtf"] = richTextBox => GetRange(richTextBox: richTextBox).Save(stream: File.OpenWrite(path: FileFullName), dataFormat: DataFormats.Rtf)
		};

		public static TextRange GetRange(RichTextBox richTextBox)
			=> new TextRange(position1: richTextBox.Document.ContentStart, position2: richTextBox.Document.ContentEnd);

		public static void Open(RichTextBox richTextBox)
		{
			richTextBox.Document.Blocks.Clear();
			_openActions[FileName.Split(separator: '.').Last()](obj: richTextBox);
		}

		public static void Save(RichTextBox richTextBox)
			=> _saveActions[FileName.Split(separator: '.').Last()](obj: richTextBox);
	}
}
