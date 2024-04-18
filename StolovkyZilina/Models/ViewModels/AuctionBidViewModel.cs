using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class AuctionBidViewModel
	{
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public ICollection<string> AllValidGameNames { get; set; }
		public string SelectedGame { get; set; }
        public int Bid { get; set; }
        public int MaxBid { get; set; }
        public int DesiredPlayerCount { get; set; }

		public AuctionBidViewModel()
		{
			AllValidGameNames = new List<string>();
		}
	}
}
