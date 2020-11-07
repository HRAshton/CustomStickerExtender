using System.IO;
using System.Linq;

namespace HRAshton.CustomStickerExtender.Models
{
	public class StickersViewModel
	{
		public StickerPack[] StickerPacks { get; set; }

		public StickersViewModel()
		{
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