using System.IO;
using System.Linq;
using System.Windows.Media;
using HRAshton.CustomStickerExtender.Properties;

namespace HRAshton.CustomStickerExtender.Models
{
	public class StickersViewModel
	{
		public Color StickerPanelBackground { get; set; }
		
		public StickerPack[] StickerPacks { get; set; }

		public StickersViewModel()
		{
			StickerPanelBackground = Settings.Default.StickersPanelBackground;
			
			StickerPacks = Directory.GetDirectories("Pictures/")
				.Select(dirName => new StickerPack
				{
					Name = new DirectoryInfo(dirName).Name, 
					StickerFullNames = Directory
						.GetFiles(dirName, "*.png", SearchOption.TopDirectoryOnly)
						.Select(fileName => new FileInfo(fileName).FullName)
						.ToArray(),
				})
				.ToArray();
		}
	}
}