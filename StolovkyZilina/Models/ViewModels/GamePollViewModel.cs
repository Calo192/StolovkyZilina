using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class GamePollViewModel
	{
		public Guid Id { get; set; }
		public int NumberOfVotesPerUser { get; set; }
		public Guid EventId { get; set; }
		public Event Event { get; set; }
		public ICollection<Game> GamesInPoll { get; set; }
		public ICollection<GameVote> GameVotes { get; set; }
	}
}
