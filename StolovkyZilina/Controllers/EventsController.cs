using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Requests;
using StolovkyZilina.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Util;
using StolovkyZilina.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Azure.Core;

namespace StolovkyZilina.Controllers
{
	public class EventsController : Controller
	{
		private readonly ITagRepository tagRepository;
		private readonly IRepository<Location> locationRepository;
		private readonly IRepository<Event> eventRepository;
		private readonly IGameRelationRepository<GameOwner> gameRelationRepository;
		private readonly IRepository<Game> gameRepository;
		private readonly IRepository<GameVote> gameVoteRepository;
		private readonly IRepository<Feed> feedRepository;
		private readonly IRepository<GamePoll> gamePollRepository;
		private readonly IRepository<ParticipationVote> participationRepository;
		private readonly IContentRatingRepository blogPostLikeRepository;
		private readonly IContentCommentRepository contentCommentRepository;
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IRepository<Content> contentRepository;
		private readonly IRepository<UserProfile> profileRepository;
		private readonly IContentRatingRepository contentRatingRepository;

		public EventsController(ITagRepository tagRepository,
			IRepository<Location> locationRepository,
			IRepository<Event> eventRepository,
			IRepository<Game> gameRepository,
			IRepository<GameVote> gameVoteRepository,
			IRepository<Feed> feedRepository,
			IRepository<GamePoll> gamePollRepository,
			IRepository<ParticipationVote> participationRepository,
			IRepository<Content> contentRepository,
			IRepository<UserProfile> profileRepository,
			IGameRelationRepository<GameOwner> gameRelationRepository,
			IContentRatingRepository contentRatingRepository,
			IContentRatingRepository blogPostLikeRepository,
			IContentCommentRepository contentCommentRepository,
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager)
		{
			this.tagRepository = tagRepository;
			this.locationRepository = locationRepository;
			this.eventRepository = eventRepository;
			this.gameRelationRepository = gameRelationRepository;
			this.gameRepository = gameRepository;
			this.gameVoteRepository = gameVoteRepository;
			this.feedRepository = feedRepository;
			this.gamePollRepository = gamePollRepository;
			this.participationRepository = participationRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
			this.contentCommentRepository = contentCommentRepository;
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.contentRepository = contentRepository;
			this.profileRepository = profileRepository;
			this.contentRatingRepository = contentRatingRepository;
		}

		private async Task<Guid> GetUserId()
		{
			if (signInManager.IsSignedIn(User))
			{
				var userId = Guid.Parse(userManager.GetUserId(User));
				var userProfile = await profileRepository.GetAsync(userId);
				return userProfile.Id;
			}
			return Guid.Empty;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> List()
		{
			var events = await eventRepository.GetAllAsync();
			return View(events);
		}


		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new EventRequest();
			var tags = await tagRepository.GetAllAsync();
			var locations = await locationRepository.GetAllAsync();
			model.Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

			if (locations.Any())
			{
				var updated = new List<Location>();
				foreach (var location in locations)
				{
					updated.Add(location);
				}
				model.Locations = updated.Select(location => new SelectListItem { Text = location.Name, Value = location.Id.ToString() });
			}

			model.Time = DateTime.Now;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(EventRequest request, IFormFile fileInput)
		{
			var newEvent = new Event
			{
				Description = request.Description,
				ShortDescription = request.ShortDescription,
				Name = request.Name,
				Time = request.Time,
				MakeGamesSuggestion = request.MakeGamesSuggestion
			};

			if (!string.IsNullOrEmpty(request.SelectedLocationId) && request.SelectedLocationId != Guid.Empty.ToString())
			{
				newEvent.LocationId = Guid.Parse(request.SelectedLocationId);
			}

			var newContent = await contentRepository.AddAsync(new Content());

			if (newContent != null)
			{
				newEvent.Content = newContent;
			}

			if (fileInput != null && fileInput.Length > 0)
			{
				using (var memoryStream = new MemoryStream())
				{
					fileInput.CopyTo(memoryStream);

					newEvent.FeaturedImage = ImageUtil.CropToSquare(memoryStream.ToArray());
				}
			}

			newEvent.Content.Tags = new List<Tag>();

			foreach (var selectedTagId in request.SelectedTags)
			{
				var existingTag = await tagRepository.GetAsync(Guid.Parse(selectedTagId));

				if (existingTag != null)
				{
					newEvent.Content.Tags.Add(existingTag);
				}
			}

			await contentRepository.UpdateAsync(newEvent.Content);

			await eventRepository.AddAsync(newEvent);

			return RedirectToAction(nameof(List));
		}

		[HttpGet]
		public async Task<IActionResult> Detail(Guid id)
		{
			var selectedEvent = await eventRepository.GetAsync(id);
			var comments = await contentCommentRepository.GetAllCommentsByContentIdAsync(selectedEvent.ContentId);
			var eventCommentsForView = new List<ContentCommentViewModel>();

			foreach (var comment in comments)
			{
				eventCommentsForView.Add(new ContentCommentViewModel
				{
					Content = comment.Content,
					DateAdded = comment.DateAdded,
					UserName = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
				});
			}

			int[] totalLikes = new int[5];

			for (int i = 0; i < 5; i++)
			{
				totalLikes[i] = await contentRatingRepository.GetTotalRatings(selectedEvent.ContentId, i + 1);
			}

			var viewModel = new EventViewModel()
			{
				Id = selectedEvent.Id,
				Name = selectedEvent.Name,
				Description = selectedEvent.Description,
				ShortDescription = selectedEvent.ShortDescription,
				FeaturedImage = selectedEvent.FeaturedImage,
				Location = selectedEvent.Location,
				LocationId = selectedEvent.LocationId,
				MakeGamesSuggestion = selectedEvent.MakeGamesSuggestion,
				Time = selectedEvent.Time,
				TotalLikes = totalLikes,
				ContentId = selectedEvent.ContentId,
				Tags = selectedEvent.Content.Tags,
				Comments = eventCommentsForView,
				GamePolls = selectedEvent.GamePolls,
				CurrentProfileId = await GetUserId()
			};

			foreach (var vote in selectedEvent.ParticipationVotes)
			{
				var userProfile = await profileRepository.GetAsync(vote.UserId.ToString());
				var identityUser = await userManager.FindByIdAsync(userProfile.UserId.ToString());
				viewModel.ParticipationVotes.Add(new ParticipationVoteViewModel
				{
					Alias = identityUser.UserName,
					FeaturedImage = userProfile.FeaturedImage,
					VoteStatus = vote.VoteStatus
				});
			}
			if (signInManager.IsSignedIn(User))
			{
				var userId = await GetUserId();
				var existingVote = selectedEvent?.ParticipationVotes?.FirstOrDefault(x => x.UserId == userId);
				if (existingVote != null)
				{
					viewModel.ParticipationVote = existingVote.VoteStatus;
				}
			}

			return View(viewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Detail(EventViewModel eventViewModel)
		{
			if (signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(eventViewModel.CommentContent))
			{
				var domainModel = new Comment
				{
					ContentId = eventViewModel.ContentId,
					Content = eventViewModel.CommentContent,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					DateAdded = DateTime.Now
				};
				var userName = userManager.GetUserName(User);
				var eventUrl = Url.Action("Detail", "Events", new { id = eventViewModel.Id });
				var profileUrl = Url.Action("Profile", "Account", new { userName = userName });
				var comentatorLink = "<a href =\"" + profileUrl + "\">" + userName + "</a>";

				var body = "<figure>\r\n<blockquote class=\"blockquote\">\r\n<p><i>'" + eventViewModel.CommentContent + "'</i></p>\r\n </blockquote>\r\n<figcaption class=\"blockquote-footer\">\r\n " + comentatorLink + " takto okomentoval partiu hry <cite title=\"Source Title\"><a href =\"" + eventUrl + "\">" + eventViewModel.Name + "</a></cite>\r\n</figcaption>\r\n</figure>";
				var temp = "<p>" + comentatorLink + " okomentoval udalost <a href =\"" + eventUrl + "\">" + eventViewModel.Name + "</a> nasledovne: </br>" + eventViewModel.CommentContent + "</p>";

				var newFeed = new Feed()
				{
					DateAdded = DateTime.UtcNow,
					Body = body
				};
				await feedRepository.AddAsync(newFeed);
				await contentCommentRepository.AddAsync(domainModel);
			}
			return RedirectToAction(nameof(Detail), new { id = eventViewModel.Id });
		}

		[HttpGet]
		public async Task<IActionResult> ParticipationVote(Guid id, int voteStatus)
		{
			var selectedEvent = await eventRepository.GetAsync(id);

			var userId = await GetUserId();

			if (selectedEvent.ParticipationVotes == null)
			{
				selectedEvent.ParticipationVotes = new List<ParticipationVote>();
			}
			var existingVote = selectedEvent.ParticipationVotes.FirstOrDefault(x => x.UserId == userId);

			if (existingVote != null)
			{
				existingVote.VoteStatus = voteStatus;
				await eventRepository.UpdateAsync(selectedEvent);
			}
			else
			{
				var newVote = new ParticipationVote
				{
					EventId = id,
					UserId = userId,
					VoteStatus = voteStatus
				};

				var votes = await participationRepository.GetAllAsync();

				await participationRepository.AddAsync(newVote);
			}

			return RedirectToAction(nameof(Detail), new { id });
		}

		[HttpGet]
		public async Task<IActionResult> CreateGamePoll(Guid id)
		{
			var request = new CreateGamePollRequest();
			request.EventId = id;
			var selectedEvent = await eventRepository.GetAsync(id);
			var attendees = selectedEvent.ParticipationVotes.Where(p => p.VoteStatus == 2);
			var attendeesProfiles = new List<UserProfile>();
			var games = await gameRepository.GetAllAsync();
			if (games.Any(g => g.Approved))
			{
				foreach (var game in games)
				{
					if (game.Approved) request.AllValidGameNames.Add(game.Name);
				}
			}

			foreach (var attendee in attendees)
			{
				var userProfile = await profileRepository.GetAsync(attendee.UserId.ToString());
				attendeesProfiles.Add(userProfile);
			}
			var relations = new List<GameOwner>();
			if (attendeesProfiles.Any())
			{
				foreach (var attendee in attendeesProfiles)
				{
					var relation = await gameRelationRepository.GetAllByUserIdAsync(attendee.Id);
					if (relation != null)
					{
						relations.AddRange(relation);
					}
				}
			}

			var suggestions = new List<GameSuggestionViewModel>();
			foreach (var relation in relations)
			{
				if (!suggestions.Any(s => s.GameId == relation.GameId)
					&& relation.IsOwner
					&& relation.BoardGame.SpaceRequirement <= selectedEvent.Location.Space
					&& relation.BoardGame.Approved)
				{
					var suggestionViewModel = new GameSuggestionViewModel
					{
						GameId = relation.GameId,
						UrlHandle = relation.BoardGame.UrlHandle,
						Name = relation.BoardGame.Name,
						FeaturedImage = relation.BoardGame.FeaturedImage
					};

					foreach (var rel in relations.Where(r => r.GameId == relation.GameId))
					{
						if (rel.IsFavorite) suggestionViewModel.Score += 15;
						if (rel.PlaiyngInterest > 0) suggestionViewModel.Score += rel.PlaiyngInterest * 10;
					}

					suggestions.Add(suggestionViewModel);
				}
			}
			request.Suggestions = suggestions.Where(s => s.Score > 0).OrderByDescending(s => s.Score).TakeLast(attendees.Count()).ToList();

			return View(request);
		}

		[HttpPost]
		public async Task<IActionResult> CreateGamePoll(CreateGamePollRequest request)
		{
			var selectedEvent = await eventRepository.GetAsync(request.EventId);

			GamePoll gamePoll = new GamePoll
			{
				EventId = request.EventId,
				Event = selectedEvent,
				NumberOfVotesPerUser = request.NumberOfVotesPerUser,
				PollName = request.Name,
				GamesInPoll = new List<Game>(),
				GameVotes = new List<GameVote>()
			};

			foreach (var gameName in request.GamesInPoll)
			{
				var game = await gameRepository.GetAsyncByName(gameName);
				if (game != null)
				{
					gamePoll.GamesInPoll.Add(game);
				}
			}

			await gamePollRepository.AddAsync(gamePoll);

			return RedirectToAction(nameof(Detail), new { id = request.EventId });
		}

		[HttpGet]
		public async Task<IActionResult> VoteForGame(Guid gameId, Guid eventId, Guid gamePollId)
		{
			var userid = await GetUserId();

			var existingVotes = await gameVoteRepository.GetAllAsync(gamePollId.ToString());
			var existingVote = existingVotes.FirstOrDefault(x => x.GameId == gameId && x.UserId == userid);
			if (existingVote != null)
			{
				await gameVoteRepository.DeleteAsync(existingVote.Id);
			}
			else
			{
				var selectedEvent = await eventRepository.GetAsync(eventId);

				var gameVote = new GameVote
				{
					GameId = gameId,
					UserId = userid,
					GamePollId = gamePollId
				};

				await gameVoteRepository.AddAsync(gameVote);
			}
			
			return RedirectToAction(nameof(Detail), new { id = eventId });
		}
	}
}
