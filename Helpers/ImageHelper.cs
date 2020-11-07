using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace HRAshton.CustomStickerExtender.Helpers
{
	public static class ImageHelper
	{
		public static BitmapImage Image2BitmapImage(Image bmp)
		{
			using var stream = new MemoryStream();
			bmp.Save(stream, ImageFormat.Png);

			stream.Position = 0;
			var result = new BitmapImage();
			result.BeginInit();
			result.CacheOption = BitmapCacheOption.OnLoad;
			result.StreamSource = stream;
			result.EndInit();
			result.Freeze();
			
			return result;
		}
		
		public static Bitmap Transparent2Color(Image bmp1, Color target)
		{
			var bmp2 = new Bitmap(bmp1.Width, bmp1.Height);
			var rect = new Rectangle(Point.Empty, bmp1.Size);
			
			using var g = Graphics.FromImage(bmp2);
			g.Clear(target);
			g.DrawImageUnscaledAndClipped(bmp1, rect);
			
			return bmp2;
		}
	}
}