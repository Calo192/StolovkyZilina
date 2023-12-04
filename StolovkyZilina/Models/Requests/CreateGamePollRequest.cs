using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;

namespace StolovkyZilina.Models.Requests
{
	public class CreateGamePollRequest
	{
        public int NumberOfVotesPerUser { get; set; }
        public string Name { get; set; }
        public Guid EventId { get; set; }
		public List<string> GamesInPoll { get; set; }
		public ICollection<string> AllValidGameNames { get; set; }
		public List<GameSuggestionViewModel> Suggestions { get; set; }

		public CreateGamePollRequest()
		{
			GamesInPoll = new List<string>();
			AllValidGameNames = new List<string>();
			Suggestions = new List<GameSuggestionViewModel>();
			NumberOfVotesPerUser = 1;
		}
    }
}
