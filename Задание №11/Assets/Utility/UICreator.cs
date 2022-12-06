using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Задание__11.Assets.Utility
{
	internal static class UICreator
	{
		public static void SetEncoding(Encoding encoding, string header, MenuItem item)
		{
			PickedFile.LeadToEncoding = encoding;
			item.Header = header;
			PickedFile.EncodingStringFormat = header.ToString();
		}

		public static void CreateSelectionEncoding(Grid grid, Button button, Window window)
		{
			grid.Children.Add(element: new TextBlock
			{
				VerticalAlignment = VerticalAlignment.Top, Text = "Кодировка: ", Width = 100, 
				HorizontalAlignment = HorizontalAlignment.Left, Height = 30, FontSize = 16,
				Margin = new Thickness(left: 35, top: 72, right: 10, bottom: 32),
				Style = (Style)Application.Current.FindResource(resourceKey: "MyTextBlock"),
			});
			Menu menu = new Menu
			{
				VerticalAlignment = VerticalAlignment.Top, Height = 30,
				HorizontalAlignment = HorizontalAlignment.Left, Width = 200,
				Margin = new Thickness(left: 140, top: 72, right: 10, bottom: 32),
				Style = (Style)Application.Current.FindResource(resourceKey: "MyMenu")
			};
			MenuItem itemWin1251 = new MenuItem 
			{ 
				Header = "Windows-1251", Style = (Style)Application.Current.FindResource(resourceKey: "MyMenuItem"),
				ToolTip = new ToolTip { Content = "Открыть в кодировке Windows-1251", Width = 220,
					Style = (Style)Application.Current.FindResource(resourceKey: "MyToolTip") }
			};
			MenuItem itemUnicode = new MenuItem 
			{ 
				Header = "Unicode", Style = (Style)Application.Current.FindResource(resourceKey: "MyMenuItem"),
				ToolTip = new ToolTip { Content = "Открыть в кодировке Unicode", Width = 185,
					Style = (Style)Application.Current.FindResource(resourceKey: "MyToolTip") }
			};
			MenuItem item = new MenuItem 
			{ 
				Name = "MeinHeader", Header = "Кодировка", Items = { itemWin1251, itemUnicode },
				VerticalContentAlignment = VerticalAlignment.Center, FontSize = 13, Width = 200,
				Style = (Style)Application.Current.FindResource(resourceKey: "MyMenuItem") , Height = 30,
				ToolTip = new ToolTip { Content = "Выбрать кодировку для текстового файла", Width = 250,
					Style = (Style)Application.Current.FindResource(resourceKey: "MyToolTip") }
			};
			itemWin1251.Click += (sender, e) => SetEncoding(encoding: Encoding.GetEncoding(name: "windows-1251"), header: "Windows-1251", item: item);
			itemUnicode.Click += (sender, e) => SetEncoding(encoding: Encoding.Unicode, header: "Unicode", item: item);
			menu.Items.Add(newItem: item);
			grid.Children.Add(element: menu);
			button.Margin = new Thickness(left: 240, top: 117, right: 10, bottom: 7);
			window.Height = 160;
		}
	}
}
