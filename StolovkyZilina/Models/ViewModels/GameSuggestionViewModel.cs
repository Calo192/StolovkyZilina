namespace StolovkyZilina.Models.ViewModels
{
	public class GameSuggestionViewModel
	{
        public Guid GameId { get; set; }
        public string UrlHandle { get; set; }
		public string Name { get; set; }
		public double Score { get; set; }
		public byte[]? FeaturedImage { get; set; }
	}
}
