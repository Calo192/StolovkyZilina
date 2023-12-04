using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class BlogDetailsViewModel : ContentViewModel
	{
		public string Heading { get; set; }
		public string PageTitle { get; set; }
		public string Content { get; set; }
		public string ShortDesc { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public DateTime PublishDate { get; set; }
		public string Author { get; set; }
		public bool Visible { get; set; }
    }
}
