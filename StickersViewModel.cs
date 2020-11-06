using System.IO;
using System.Linq;

namespace HRAshton.CustomStickerExtender
{
	public class StickersViewModel
	{
		public string[] Images { get; set; }

		public StickersViewModel()
		{
			Images = Directory.GetFiles("Pictures/")
				.Select(x => new FileInfo(x))
				.Select(x => x.FullName)
				.ToArray();
		}
	}
}