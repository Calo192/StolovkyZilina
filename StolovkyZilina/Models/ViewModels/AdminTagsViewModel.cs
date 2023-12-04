using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class AdminTagsViewModel
	{
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public List<Tag> Tags { get; set; }
	}
}
