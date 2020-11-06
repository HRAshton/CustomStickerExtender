using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HRAshton.CustomStickerExtender
{
	public class ImagePathConverter : IValueConverter
	{
		public object Convert(object path, Type targetType, object parameter, CultureInfo culture)
		{
			var bi = new BitmapImage();
			bi.BeginInit();
			bi.DecodePixelWidth = 100;
			bi.CacheOption = BitmapCacheOption.OnLoad;
			bi.UriSource = new Uri((string)path ?? throw new Exception());
			bi.EndInit();
			return bi;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}