using Microsoft.AspNetCore.Mvc.Rendering;
namespace StolovkyZilina.Models.Requests
{
    public class BlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDesc { get; set; }
        public byte[]? FeaturedImage { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
        //display tags
        public IEnumerable<SelectListItem> Tags { get; set; }
        //collect tags
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}
