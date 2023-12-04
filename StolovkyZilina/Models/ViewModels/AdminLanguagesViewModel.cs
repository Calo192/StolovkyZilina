using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class AdminLanguagesViewModel
	{
		public string DisplayName { get; set; }
		public List<GameLanguage> Languages { get; set; }
	}
}
