using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class GamePollDetailViewModel
	{
		public int NumberOfVotesPerUser { get; set; }
		public string PollName { get; set; }
		public Event Event { get; set; }
		public ICollection<Game> GamesInPoll { get; set; }
		public ICollection<GameVote> GameVotes { get; set; }
	}
}
