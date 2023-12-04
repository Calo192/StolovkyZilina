namespace StolovkyZilina.Models.Domain
{
    public class BlogPost : Content
    {
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDesc { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public string UrlHandle { get; set; }
        public DateTime PublishDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }
	}
}
