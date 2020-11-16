using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HRAshton.CustomStickerExtender.Helpers;
using HRAshton.CustomStickerExtender.Models;
using HRAshton.CustomStickerExtender.Properties;
using SendKeys = System.Windows.Forms.SendKeys;

namespace HRAshton.CustomStickerExtender
{
	/// <summary>
	/// Interaction logic for HoverControl.xaml
	/// </summary>
	public partial class HoverControl : Window
	{
		public HoverControl()
		{
			InitializeComponent();
			DataContext = new StickersViewModel();
		}

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			WinApiHelpers.SetNoActivate(this);
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			Close();
		}

		private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
		{
			if (!((sender as Image)?.Source is BitmapImage thumbImage))
			{
				return;
			}
			
			var buf = Clipboard.GetDataObject();
			
			var target = GetBitmapImage(thumbImage.UriSource.LocalPath);

			Clipboard.SetImage(target);

			SendKeys.SendWait("^v");
			
			Thread.Sleep(1000);

			if (buf != null)
			{
				Clipboard.SetDataObject(buf);
			}
		}

		private static BitmapImage GetBitmapImage(string fileName)
		{
			var origImage = System.Drawing.Image.FromFile(fileName);
			var colorizedBmp = ImageHelper.Transparent2Color(origImage, Settings.Default.PastedStickerBackground);
			var bitmapImage = ImageHelper.Image2BitmapImage(colorizedBmp);

			return bitmapImage;
		}
		
	}
}