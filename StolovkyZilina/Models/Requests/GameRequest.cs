using Microsoft.AspNetCore.Mvc.Rendering;

namespace StolovkyZilina.Models.Requests
{
	public class GameRequest
	{
		public Guid Id { get; set; }
		public Guid ContentId { get; set; }
		public string Name { get; set; }
		public string? Author { get; set; }
		public string? Desc { get; set; }
		public string? ShortDesc { get; set; }
		public int? Difficulty { get; set; }
		public int? Playtime { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public int MinPlayerCount { get; set; }
		public int MaxPlayerCount { get; set; }
		public bool Cooperative { get; set; }
		public bool OnPoints { get; set; }
		public int? SpaceRequirement { get; set; }
		public bool Approved { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; }
		public string SelectedCategory { get; set; }
		public IEnumerable<SelectListItem> Tags { get; set; }
		public string[] SelectedTags { get; set; } = Array.Empty<string>();

		public int GetImageSizeInKB()
		{
			if (FeaturedImage == null)
			{
				// Handle the case where the byte array is null (or optionally return 0 KB).
				return 0;
			}

			// Calculate the size in KB by dividing the length of the byte array by 1024.
			int sizeInKB = FeaturedImage.Length / 1024;

			return sizeInKB;
		}

		public string UrlHandle { get; set; }
	}
}
