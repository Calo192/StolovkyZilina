using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class AdminCategoriesViewModel
	{
		public string DisplayName { get; set; }
		public List<GameCategory> Categories { get; set; }
	}
}
