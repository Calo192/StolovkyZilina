using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.Requests;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;
using StolovkyZilina.Util;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StolovkyZilina.Controllers
{
	public class PlaysController : Controller
	{
		private readonly IRepository<Game> gameRepository;
		private readonly IRepository<Location> locationRepository;
		private readonly IRepository<GamePlay> gamePlayRepository;
		private readonly IRepository<Event> eventRepository;
		private readonly IUserRepository userRepository;
		private readonly IRepository<UserProfile> userProfileRepository;
		private readonly IContentCommentRepository contentCommentRepository;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IRepository<Feed> feedRepository;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IContentRatingRepository contentRatingRepository;
		private readonly IRepository<Content> contentRepository;

		public PlaysController(IRepository<Game> gameRepository,
			IRepository<Location> locationRepository,
			IRepository<GamePlay> gamePlayRepository,
			IRepository<Event> eventRepository,
			IUserRepository userRepository,
			IRepository<UserProfile> userProfileRepository,
			IContentCommentRepository contentCommentRepository,
			UserManager<IdentityUser> userManager,
			IRepository<Feed> feedRepository,
			SignInManager<IdentityUser> signInManager,
			IContentRatingRepository contentRatingRepository,
			IRepository<Content> contentRepository)
		{
			this.gameRepository = gameRepository;
			this.locationRepository = locationRepository;
			this.gamePlayRepository = gamePlayRepository;
			this.eventRepository = eventRepository;
			this.userRepository = userRepository;
			this.userProfileRepository = userProfileRepository;
			this.contentCommentRepository = contentCommentRepository;
			this.userManager = userManager;
			this.feedRepository = feedRepository;
			this.signInManager = signInManager;
			this.contentRatingRepository = contentRatingRepository;
			this.contentRepository = contentRepository;
		}

		[HttpGet]
		public async Task<IActionResult> List()
		{
			var plays = await gamePlayRepository.GetAllAsync();
			var modelList = new List<PlayViewModel>();
			foreach (var play in plays)
			{
				var players = new List<PlayerRecord>();
				foreach (var player in play.Results)
				{
					var isGuest = false;
					var userName = string.Empty;
					if (player.PlayerId != null)
					{
						var user = await userManager.FindByIdAsync(player.Player.UserId.ToString());
						userName = user.UserName;
					}
					else
					{
						isGuest = true;
						userName = player.GuestPlayerName;
					}

					players.Add(new PlayerRecord()
					{
						UserName = userName,
						IsGuest = isGuest,
						Info = player.PlayerInfo,
						PlayerImage = player?.Player?.FeaturedImage,
						Score = player.Result
					});
				}

				var playModel = new PlayViewModel()
				{
					Id = play.Id,
					ContentId = play.ContentId,
					EventId = play.EventId,
					EndTime = play.EndTime,
					StartTime = play.StartTime,
					GameFeaturedImage = play.Game.FeaturedImage,
					GameId = play.Game.Id,
					GameName = play.Game.Name,
					GameUrlHandle = play.Game.UrlHandle,
					Desc = play.Desc,
					IsCoopGame = play.Game.Cooperative,
					IsOnPointsGame = play.Game.OnPoints,
					Location = play.Location,
					Event = play.Event,
					Players = players.OrderByDescending(p => p.Score).ToList()
				};

				modelList.Add(playModel);
			}

			return View(modelList);
		}

		[HttpGet]
		public async Task<IActionResult> Add(int playerCount, Guid eventId)
		{
			var users = await userRepository.GetAll();
			var model = new GamePlayRequest();
			var games = await gameRepository.GetAllAsync();
			var locations = await locationRepository.GetAllAsync();

			for (int i = 0; i < playerCount; i++)
			{
				model.Results.Add(new PlayerPlayResultModel());
			}

			if (games.Any(g => g.Approved))
			{
				foreach (var game in games)
				{
					if (game.Approved) model.Games.Add(game.Name);
				}
			}

			if (users.Any())
			{
				foreach (var user in users)
				{
					model.Users.Add(user.UserName);
				}
			}

			if (locations.Any())
			{
				var updated = new List<Location>();
				updated.Add(new Location() { Name = "-", Id = Guid.Empty, });
				foreach (var location in locations)
				{
					updated.Add(location);
				}
				model.Locations = updated.Select(location => new SelectListItem { Text = location.Name, Value = location.Id.ToString() });
			}

			if (eventId != null && eventId != Guid.Empty)
			{
				model.EventId = (Guid)eventId;

				var gamingEvent = await eventRepository.GetAsync(eventId);
				if (gamingEvent != null)
				{
					model.SelectedLocationId = gamingEvent.Location.Id.ToString();
				}
			}

			model.StartTime = DateTime.Now;
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(GamePlayRequest addGamePlayRequest, string submitButton)
		{
			var game = await ValidateGameName(addGamePlayRequest.SelectedGame);
			if (ModelState.IsValid)
			{
				if (addGamePlayRequest.SelectedLocationId == Guid.Empty.ToString())
				{
					addGamePlayRequest.SelectedLocationId = null;
				}

				var play = new GamePlay()
				{
					Desc = addGamePlayRequest.Desc,
					EndTime = addGamePlayRequest.EndTime,
					StartTime = addGamePlayRequest.StartTime,
					EventId = addGamePlayRequest.EventId,
					GameId = game.Id,
					Results = new List<PlayerPlayResult>()
				};

				if (!string.IsNullOrEmpty(addGamePlayRequest.SelectedLocationId))
				{
					play.LocationId = Guid.Parse(addGamePlayRequest.SelectedLocationId);
				}

				var players = string.Empty;
				foreach (var result in addGamePlayRequest.Results)
				{
					var ppr = await ParseResult(result, play);

					if (string.IsNullOrEmpty(ppr.GuestPlayerName))
					{
						var profileUrl = Url.Action("Profile", "Account", new { userName = result.PlayerName });
						if (result == addGamePlayRequest.Results.First())
						{
							players += "<a href =\"" + profileUrl + "\">" + result.PlayerName + "</a>";
						}
						else
						{
							if (result == addGamePlayRequest.Results.Last())
							{
								players += " a " + "<a href =\"" + profileUrl + "\">" + result.PlayerName + "</a>";
							}
							else
							{
								players += ", " + "<a href =\"" + profileUrl + "\">" + result.PlayerName + "</a>";
							}
						}
					}
					else
					{
						if (result == addGamePlayRequest.Results.First())
						{
							players += result.PlayerName;
						}
						else
						{
							if (result == addGamePlayRequest.Results.Last())
							{
								players += ", " + result.PlayerName;
							}
							else
							{
								players += " a " + result.PlayerName;
							}
						}
					}
					play.Results.Add(ppr);
				}

				var newContent = await contentRepository.AddAsync(new Content());
				play.Content = newContent;

				var res = await gamePlayRepository.AddAsync(play);
				if (res != null)
				{
					var gameUrl = Url.Action("Index", "Game", new { urlHandle = play.Game.UrlHandle });
					var newFeed = new Feed()
					{
						DateAdded = DateTime.UtcNow,
						Body = "<p>Hráči " + players + " sa zúčastnili partie hry <a href =\"" + gameUrl + "\">" + play.Game.Name + "</a></p>",
					};
					await feedRepository.AddAsync(newFeed);
				}

				if (submitButton == "startGame")
				{
					return RedirectToAction("List");
				}
				else
				{
					return RedirectToAction("Edit", "Plays", new { id = res.Id });
				}
			}
			return View(addGamePlayRequest);
		}

		private async Task<PlayerPlayResult> ParseResult(PlayerPlayResultModel pprm, GamePlay play)
		{
			var ppr = new PlayerPlayResult();

			ppr.Id = pprm.Id;
			ppr.PlayerInfo = pprm.PlayerInfo;
			ppr.Result = pprm.IsWinner ? 10 : pprm.Result;
			ppr.GamePlay = play;

			var user = await userManager.FindByNameAsync(pprm.PlayerName);
			if (user == null)
			{
				ppr.GuestPlayerName = pprm.PlayerName;
			}
			else
			{
				var profile = await userProfileRepository.GetAsync(Guid.Parse(user.Id));
				ppr.PlayerId = profile.Id;
				ppr.Player = profile;
			}

			return ppr;
		}

		[HttpGet]
		public async Task<IActionResult> Detail(Guid id)
		{
			var play = await gamePlayRepository.GetAsync(id);
			var players = new List<PlayerRecord>();
			foreach (var player in play.Results)
			{
				var isGuest = false;
				var userName = string.Empty;
				if (player.PlayerId != null)
				{
					var user = await userManager.FindByIdAsync(player.Player.UserId.ToString());
					userName = user.UserName;
				}
				else
				{
					isGuest = true;
					userName = player.GuestPlayerName;
				}

				players.Add(new PlayerRecord()
				{
					UserName = userName,
					IsGuest = isGuest,
					Info = player.PlayerInfo,
					PlayerImage = player?.Player?.FeaturedImage,
					Score = player.Result
				});
			}

			int[] totalLikes = new int[5];

			for (int i = 0; i < 5; i++)
			{
				totalLikes[i] = await contentRatingRepository.GetTotalRatings(play.ContentId, i + 1);
			}

			var comments = await contentCommentRepository.GetAllCommentsByContentIdAsync(play.ContentId);

			var commentsForView = new List<ContentCommentViewModel>();

			foreach (var comment in comments)
			{
				commentsForView.Add(new ContentCommentViewModel
				{
					Content = comment.Content,
					DateAdded = comment.DateAdded,
					UserName = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
				});
			}

			var playModel = new PlayViewModel()
			{
				Id = id,
				ContentId = play.ContentId,
				EventId = play.EventId,
				EndTime = play.EndTime,
				StartTime = play.StartTime,
				GameFeaturedImage = play.Game.FeaturedImage,
				GameId = play.Game.Id,
				IsCoopGame = play.Game.Cooperative,
				IsOnPointsGame = play.Game.OnPoints,
				GameName = play.Game.Name,
				Desc = play.Desc,
				Comments = commentsForView,
				GameUrlHandle = play.Game.UrlHandle,
				Location = play.Location,
				Event = play.Event,
				TotalLikes = totalLikes,
				Players = players.OrderByDescending(p => p.Score).ToList()
			};


			return View(playModel);
		}

		[HttpPost]
		public async Task<IActionResult> Detail(PlayViewModel gamePlayViewModel)
		{
			if (signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(gamePlayViewModel.CommentContent))
			{
				var domainModel = new Comment
				{
					ContentId = gamePlayViewModel.ContentId,
					Content = gamePlayViewModel.CommentContent,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					DateAdded = DateTime.Now
				};
				var userName = userManager.GetUserName(User);
				var gameUrl = Url.Action("Index", "Game", new { urlHandle = gamePlayViewModel.GameUrlHandle });
				var profileUrl = Url.Action("Profile", "Account", new { userName = userName });
				var comentatorLink = "<a href =\"" + profileUrl + "\">" + userName + "</a>";

				var body = "<figure>\r\n<blockquote class=\"blockquote\">\r\n<p><i>'" + gamePlayViewModel.CommentContent + "'</i></p>\r\n </blockquote>\r\n<figcaption class=\"blockquote-footer\">\r\n " + comentatorLink + " takto okomentoval partiu hry <cite title=\"Source Title\"><a href =\"" + gameUrl + "\">" + gamePlayViewModel.GameName + "</a></cite>\r\n</figcaption>\r\n</figure>";
				var temp = "<p>" + comentatorLink + " okomentoval partiu hry <a href =\"" + gameUrl + "\">" + gamePlayViewModel.GameName + "</a> nasledovne: </br>" + gamePlayViewModel.CommentContent + "</p>";

				var newFeed = new Feed()
				{
					DateAdded = DateTime.UtcNow,
					Body = body
				};
				await feedRepository.AddAsync(newFeed);
				await contentCommentRepository.AddAsync(domainModel);
			}
			return RedirectToAction("Detail", "Plays", new { id = gamePlayViewModel.Id });
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var play = await gamePlayRepository.GetAsync(id);

			var users = await userRepository.GetAll();
			var model = new GamePlayRequest();
			var locations = await locationRepository.GetAllAsync();

			foreach (var res in play.Results)
			{

				var playername = res.GuestPlayerName;
				if (string.IsNullOrEmpty(playername))
				{
					var user = await userManager.FindByIdAsync(res.Player.UserId.ToString());
					playername = user.UserName;
				}

				model.Results.Add(new PlayerPlayResultModel() 
				{ 
					Id = res.Id, 
					PlayerName = playername,
					PlayerInfo = res.PlayerInfo, 
					Result = res.Result, 
					IsWinner = (res.Result > 0 && !res.GamePlay.Game.OnPoints) || (res.GamePlay.Game.OnPoints && res.Result == play.Results.Max(result => result.Result))
				});
			}

			model.SelectedGame = play.Game.Name;

			if (users.Any())
			{
				foreach (var user in users)
				{
					model.Users.Add(user.UserName);
				}
			}

			if (locations.Any())
			{
				var updated = new List<Location>();
				updated.Add(new Location() { Name = "-", Id = Guid.Empty, });
				foreach (var location in locations)
				{
					updated.Add(location);
				}
				model.Locations = updated.Select(location => new SelectListItem { Text = location.Name, Value = location.Id.ToString() });
			}

			model.StartTime = play.StartTime;
			model.EndTime = play.EndTime;
			model.Desc = play.Desc;
			model.Id = play.Id;
			model.ContentId = play.ContentId;
			model.EventId = play.EventId;
			model.SelectedLocationId = play.LocationId.ToString();
			model.IsCoopGame = play.Game.Cooperative;
			model.IsOnPointsGame = play.Game.OnPoints;

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(GamePlayRequest updateRequest)
		{
			var game = await ValidateGameName(updateRequest.SelectedGame);
			if (ModelState.IsValid)
			{
				if (updateRequest.SelectedLocationId == Guid.Empty.ToString())
				{
					updateRequest.SelectedLocationId = null;
				}


				if (updateRequest.EventId != null && updateRequest.EventId != Guid.Empty)
				{
                    var playsInEvent = await gamePlayRepository.GetAllAsync("p_" + updateRequest.EventId?.ToString());
                    if (playsInEvent.Any())
                    {
						foreach (var result in updateRequest.Results)
						{
                            var user = await userManager.FindByNameAsync(result.PlayerName);
							if (user != null)
							{
                                var profile = await userProfileRepository.GetAsync(Guid.Parse(user.Id));
								if (profile != null)
								{
									if (playsInEvent.Any(p => p.Id != updateRequest.Id && p.Results.Any(r => r.PlayerId == profile.Id)))
									{
										if (updateRequest.Results.Max(r => r.Result) == result.Result && 
											playsInEvent.Any(p => p.Id != updateRequest.Id && p.Results.Where(r => r.PlayerId == profile.Id).All(r => r.Result != p.Results.Max(res => res.Result))))
										{
                                            profile.Influence += 1;
                                            await userProfileRepository.UpdateAsync(profile);
                                        }
									}
									else
									{
                                        profile.Influence += result.Result == updateRequest.Results.Max(r => r.Result) ? 4 : 3;
                                        await userProfileRepository.UpdateAsync(profile);
                                    }
                                }
                            }
                        }
                    }
                }

                var playDomain = new GamePlay()
				{
					Desc = updateRequest.Desc,
					StartTime = updateRequest.StartTime,
					EndTime = updateRequest.EndTime,
					GameId = game.Id,
					EventId = updateRequest.EventId,
					LocationId = string.IsNullOrEmpty(updateRequest.SelectedLocationId) ? null : Guid.Parse(updateRequest.SelectedLocationId),
					Id = updateRequest.Id,
					ContentId = updateRequest.ContentId,
				};

				playDomain.Results = new List<PlayerPlayResult>();
				foreach (var res in updateRequest.Results)
				{
					var ppr = await ParseResult(res, playDomain);
					ppr.GamePlayId = playDomain.Id;

					playDomain.Results.Add(ppr);
				}
				var content = await contentRepository.GetAsync(playDomain.ContentId);
				playDomain.Content = content;
				var updatedPlay = await gamePlayRepository.UpdateAsync(playDomain);
				if (updatedPlay != null)
				{
					var gameUrl = Url.Action("Index", "Game", new { urlHandle = updatedPlay.Game.UrlHandle });
					var newFeed = new Feed()
					{
						DateAdded = DateTime.UtcNow,
						Body = "<p>Partia hry <a href =\"" + gameUrl + "\">" + updatedPlay.Game.Name + "</a> bola aktualizovaná</p>",
					};
					await feedRepository.AddAsync(newFeed);
					return RedirectToAction("List");
				}
			}
			return View(updateRequest);
		}


		private async Task<Game> ValidateGameName(string gameName)
		{
			var game = await gameRepository.GetAsyncByName(gameName);

			if (game == null)
			{
				ModelState.AddModelError(nameof(GamePlayRequest.SelectedGame), Constants.NonExistingGame);
			}

			return game;
		}
	}
}
