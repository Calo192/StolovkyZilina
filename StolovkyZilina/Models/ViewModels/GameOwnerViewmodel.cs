using Microsoft.AspNetCore.Mvc.Rendering;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class GameOwnerViewmodel
	{
		public Guid OwnerId { get; set; }
		public Guid GameId { get; set; }
		public int Condition { get; set; }
		public bool DeluxeComponents { get; set; }
		public bool PromoContent { get; set; }
		public bool Insert { get; set; }
		public string Name { get; set; }
		public bool IsFavorite { get; set; }
		public int Ownership { get; set; }
		public int WantToBuy { get; set; }
		public int PlaiyngInterest { get; set; }
		public IEnumerable<SelectListItem> Languages { get; set; }
		public string SelectedLanguage { get; set; }
	}
}
