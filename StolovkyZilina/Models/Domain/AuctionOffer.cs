namespace StolovkyZilina.Models.Domain
{
	public class AuctionOffer
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid EventId { get; set; }
		public Guid GameId { get; set; }
		public UserProfile User { get; set; }
		public Game Game { get; set; }
		public int? IdealPlayerCount { get; set; }
		public int? Offer { get; set; }
	}
}
