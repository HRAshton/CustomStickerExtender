using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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

		public static Bitmap ResizeImage(Image image, Size size)
		{
			var destImage = new Bitmap(size.Width, size.Height);

			destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using var graphics = Graphics.FromImage(destImage);
			graphics.CompositingMode = CompositingMode.SourceCopy;
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

			var imageSize = GetSize(size, image.Size);
			var destRect = new Rectangle(0, destImage.Height - imageSize.Height, imageSize.Width, imageSize.Height);
			
			using var wrapMode = new ImageAttributes();
			wrapMode.SetWrapMode(WrapMode.Tile);
			graphics.DrawImage(
				image,
				destRect,
				0,
				0,
				image.Width,
				image.Height,
				GraphicsUnit.Pixel,
				wrapMode);

			return destImage;
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

		private static Size GetSize(Size target, Size source)
		{
			var wCoeff = (float)target.Width / source.Width;
			var hCoeff = (float)target.Height / source.Height;
			var minCoeff = Math.Min(wCoeff, hCoeff);
			
			return new Size(
				width: (int)(source.Width * minCoeff),
				height: (int)(source.Height * minCoeff)
			);
		}
	}
}