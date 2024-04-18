namespace StolovkyZilina.Models.Domain
{
	public class Event
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public bool MakeGamesSuggestion { get; set; }
		public int AuctionType { get; set; }
		public Guid LocationId { get; set; }
		public Location Location { get; set; }
		public DateTime Time { get; set; }
		public Guid ContentId { get; set; }
		public Content Content { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public List<ParticipationVote> ParticipationVotes { get; set; }
		public List<GamePoll> GamePolls { get; set; }
		public List<GamePlay> GamePlays { get; set; }

		public Event()
		{
			ParticipationVotes = new List<ParticipationVote>();
			GamePolls = new List<GamePoll>();
		}
	}
}
