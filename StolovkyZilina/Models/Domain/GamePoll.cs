namespace StolovkyZilina.Models.Domain
{
	public class GamePoll
	{
		public Guid Id { get; set; }
		public int NumberOfVotesPerUser { get; set; }
        public string PollName { get; set; }
        public Guid EventId { get; set; }
		public Event Event { get; set; }
		public ICollection<Game> GamesInPoll { get; set; }
		public ICollection<GameVote> GameVotes { get; set; }
	}
}
