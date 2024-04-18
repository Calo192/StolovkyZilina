using Microsoft.AspNetCore.Mvc.Rendering;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.Requests
{
	public class EventRequest
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool MakeGamesSuggestion { get; set; }
		public DateTime Time { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
        public IEnumerable<SelectListItem> Tags { get; set; }
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
        public IEnumerable<SelectListItem> Locations { get; set; }
        public string SelectedLocationId { get; set; }
        public int AuctionOption { get; set; }
    }
}
