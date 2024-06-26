﻿using Microsoft.AspNetCore.Identity;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Models.ViewModels
{
	public class EventViewModel : ContentViewModel
	{
		public string Name { get; set; }
		public Guid CurrentProfileId { get; set; }
		public bool MakeGamesSuggestion { get; set; }
        public int AuctionType { get; set; }
        public Guid LocationId { get; set; }
		public Location Location { get; set; }
		public DateTime Time { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public List<ParticipationVoteViewModel> ParticipationVotes { get; set; }
		public int ParticipationVote { get; set; }
		public List<GamePoll> GamePolls { get; set; }
		public List<PlayViewModel> GamePlayViewModels { get; set; }
        public List<GameSuggestionViewModel> Suggestions { get; set; }
        public List<AuctionOffer> AuctionBids { get; set; }

        public EventViewModel()
        {
            ParticipationVotes = new List<ParticipationVoteViewModel>();
			GamePolls = new List<GamePoll>();
			GamePlayViewModels = new List<PlayViewModel>();
			ParticipationVote = -1;
		}
    }
}
