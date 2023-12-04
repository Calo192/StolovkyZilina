using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;
using System.Globalization;

namespace StolovkyZilina.Controllers
{
	public class GameController : Controller
	{
		private readonly IRepository<Game> gameRepository;
		private readonly IRepository<GamePlay> gamePlayRepository;
		private readonly IRepository<GameLanguage> languagerepository;
		private readonly IRepository<UserProfile> profileRepository;
		private readonly IGameRelationRepository<GameOwner> gameOwnerRepository;
		private readonly IContentRatingRepository blogPostLikeRepository;
		private readonly IContentCommentRepository contentCommentRepository;
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public GameController(IGameRelationRepository<GameOwner> gameOwnerRepository, 
			IRepository<GameLanguage> languagerepository,
			IRepository<UserProfile> profileRepository,
            IRepository<Game> gameRepository,
			IRepository<GamePlay> gamePlayRepository,
			IContentRatingRepository blogPostLikeRepository,
			IContentCommentRepository contentCommentRepository,
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager)
		{
			this.gameRepository = gameRepository;
			this.gamePlayRepository = gamePlayRepository;
			this.languagerepository = languagerepository;
			this.profileRepository = profileRepository;
			this.gameOwnerRepository = gameOwnerRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
			this.contentCommentRepository = contentCommentRepository;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string urlHandle)
		{
			var game = await gameRepository.GetAsync(urlHandle);
			var gameDetailsViewModel = new GameDetailsViewModel();
			GameOwner gameOwner = null;


			if (signInManager.IsSignedIn(User))
			{
				var userProfile = await profileRepository.GetAsync(Guid.Parse(userManager.GetUserId(User)));
				gameOwner = await gameOwnerRepository.GetAsync(game.Id, userProfile.Id);
			}
			var gameOwners = await gameOwnerRepository.GetAllByGameIdAsync(game.Id);
			var favorite = false;
			var owner = false;
			var want = 0;
			var wantToPlay = 0;

			if (signInManager.IsSignedIn(User) && gameOwner != null)
			{
				favorite = gameOwner.IsFavorite;
				owner = gameOwner.IsOwner;
				want = gameOwner.BuyingInterest;
				wantToPlay = gameOwner.PlaiyngInterest;
			}

			if (game != null)
			{
				int[] totalLikes = new int[5];

				for (int i = 0; i < 5; i++)
				{
					totalLikes[i] = await blogPostLikeRepository.GetTotalRatings(game.ContentId, i + 1);
				}

				var comments = await contentCommentRepository.GetAllCommentsByContentIdAsync(game.ContentId);

				var gameCommentsForView = new List<ContentCommentViewModel>();

				foreach (var comment in comments)
				{
					gameCommentsForView.Add(new ContentCommentViewModel
					{
						Content = comment.Content,
						DateAdded = comment.DateAdded,
						UserName = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
					});
				}

				gameDetailsViewModel = new GameDetailsViewModel
				{
					Id = game.Id,
					ContentId = game.ContentId,
					Name = game.Name,
					Desc = game.Desc,
					ShortDesc = game.ShortDesc,
					Difficulty = game.Difficulty,
					Playtime = game.Playtime,
					SpaceRequired = game.SpaceRequirement,
					MinPlayerCount = game.MinPlayerCount,
					MaxPlayerCount = game.MaxPlayerCount,
					Approved = game.Approved,
					Author = game.Author,
					FeaturedImage = game.FeaturedImage,
					UrlHandle = urlHandle,
					Tags = game.Content.Tags,
					TotalLikes = totalLikes,
					Comments = gameCommentsForView,
					Favorite = favorite,
					Own = owner,
					Want = want,
					WantToPlay = wantToPlay,
					Relations = gameOwners.ToList()
				};
			}

			var plays = await gamePlayRepository.GetAllAsync(urlHandle);
			if (plays.Any())
			{
				gameDetailsViewModel.PlayViewModels = new List<PlayViewModel>();
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
						EndTime = play.EndTime,
						StartTime = play.StartTime,
						GameFeaturedImage = play.Game.FeaturedImage,
						GameId = play.Game.Id,
						GameName = play.Game.Name,
						GameUrlHandle = play.Game.UrlHandle,
						Desc = play.Desc,
						IsOnPointsGame = play.Game.OnPoints,
						IsCoopGame = play.Game.Cooperative,
						Location = play.Location,
						Players = players.OrderByDescending(p => p.Score).ToList()
					};

					gameDetailsViewModel.PlayViewModels.Add(playModel);
				}
			}

			return View(gameDetailsViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Index(GameDetailsViewModel gameDetailsViewModel)
		{
			if (signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(gameDetailsViewModel.CommentContent))
			{
				var domainModel = new Comment
				{
					ContentId = gameDetailsViewModel.ContentId,
					Content = gameDetailsViewModel.CommentContent,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					DateAdded = DateTime.Now
				};

				await contentCommentRepository.AddAsync(domainModel);
				return RedirectToAction("Index", "Game",
					new { urlHandle = gameDetailsViewModel.UrlHandle });
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Owner(Guid id)
		{
			var game = await gameRepository.GetAsync(id);
			if (signInManager.IsSignedIn(User))
			{
				var userId = Guid.Parse(userManager.GetUserId(User));
				var userProfile = await profileRepository.GetAsync(userId);
				var gameOwner = await gameOwnerRepository.GetAsync(id, userProfile.Id);
				var languages = await languagerepository.GetAllAsync();
				var model = new GameOwnerViewmodel()
				{
					GameId = id,
					OwnerId = userProfile.Id,
					Name = game.Name,
					Languages = languages.Select(x => new SelectListItem { Text = x.LanguageName, Value = x.Id.ToString() })
				};
				if (gameOwner != null)
				{
					model.Condition = gameOwner.Condition ?? 0;
					model.DeluxeComponents = gameOwner.DeluxeComponents;
					model.PromoContent = gameOwner.PromoContent;
					model.Insert = gameOwner.Insert;
					model.IsFavorite = gameOwner.IsFavorite;
					model.WantToBuy = gameOwner.BuyingInterest;
					model.PlaiyngInterest = gameOwner.PlaiyngInterest;
					model.SelectedLanguage = gameOwner.LanguageId.ToString();
					if (gameOwner.IsOwner)
					{
						model.Ownership = 2;
					}
					else if (gameOwner.BuyingInterest > 0)
					{
						model.Ownership = 1;
					}
					else
					{
						model.Ownership = 0;
					}

				}

				return View(model);
			}
			return RedirectToAction("Index", "Game", new { urlHandle = game.UrlHandle });
		}

		[HttpPost]
		public async Task<IActionResult> Owner(GameOwnerViewmodel gameOwnerViewmodel)
		{
			var game = await gameRepository.GetAsync(gameOwnerViewmodel.GameId);
			if (signInManager.IsSignedIn(User))
			{
				var userId = Guid.Parse(userManager.GetUserId(User));
				var userProfile = await profileRepository.GetAsync(userId);
				var existingOwner = await gameOwnerRepository.GetAsync(game.Id, userProfile.Id);
				var gameOwner = new GameOwner()
				{
					GameId = gameOwnerViewmodel.GameId,
					OwnerId = gameOwnerViewmodel.OwnerId,
					LanguageId = gameOwnerViewmodel.Ownership == 2 ? Guid.Parse(gameOwnerViewmodel.SelectedLanguage) : null,
					Condition = gameOwnerViewmodel.Ownership == 2 ? gameOwnerViewmodel.Condition : null,
					IsOwner = gameOwnerViewmodel.Ownership == 2,
					IsFavorite = gameOwnerViewmodel.IsFavorite,
					BuyingInterest = gameOwnerViewmodel.Ownership == 1 ? gameOwnerViewmodel.WantToBuy : 0,
					PlaiyngInterest = gameOwnerViewmodel.PlaiyngInterest,
					Insert = gameOwnerViewmodel.Ownership == 2 && gameOwnerViewmodel.Insert,
					DeluxeComponents = gameOwnerViewmodel.Ownership == 2 && gameOwnerViewmodel.DeluxeComponents,
					PromoContent = gameOwnerViewmodel.Ownership == 2 && gameOwnerViewmodel.PromoContent
				};

				if (existingOwner == null)
				{
					var gameOwnerRes = await gameOwnerRepository.AddAsync(gameOwner);
				}
				else
				{
					gameOwner.Id = existingOwner.Id;
					var updatedOwner = await UpdateOrDeleteGameOwner(gameOwner);
				}
			}
			return RedirectToAction("Index", "Game",
					new { urlHandle = game.UrlHandle });
		}

		[HttpGet]
		public async Task<IActionResult> AddToFavorites(Guid id)
		{
			var game = await gameRepository.GetAsync(id);
			
			if (signInManager.IsSignedIn(User))
			{
				var userId = Guid.Parse(userManager.GetUserId(User));
				var userProfile = await profileRepository.GetAsync(userId);
				var gameOwner = await gameOwnerRepository.GetAsync(game.Id, userProfile.Id);
				if (gameOwner == null)
				{
					gameOwner = new GameOwner()
					{
						GameId = id,
						OwnerId = userProfile.Id,
						IsFavorite = true
					};
					var gameOwnerRes = await gameOwnerRepository.AddAsync(gameOwner);
				}
				else
				{
					gameOwner.IsFavorite = !gameOwner.IsFavorite;
					var updatedOwner = await UpdateOrDeleteGameOwner(gameOwner);
				}
			}
			return RedirectToAction("Index", "Game",
					new { urlHandle = game.UrlHandle });
		}

		[HttpGet]
		public async Task<IActionResult> WantToPlay(Guid id)
		{
			var game = await gameRepository.GetAsync(id);
			if (signInManager.IsSignedIn(User))
			{
				var userId = Guid.Parse(userManager.GetUserId(User));
				var userProfile = await profileRepository.GetAsync(userId);
				var gameOwner = await gameOwnerRepository.GetAsync(id, userProfile.Id);
				var languages = await languagerepository.GetAllAsync();
				var model = new GameOwnerViewmodel()
				{
					GameId = id,
					OwnerId = userProfile.Id,
					Name = game.Name,
					Languages = languages.Select(x => new SelectListItem { Text = x.LanguageName, Value = x.Id.ToString() })
				};
				if (gameOwner != null)
				{
					model.PlaiyngInterest = gameOwner.PlaiyngInterest;
				}

				return View(model);
			}
			return RedirectToAction("Index", "Game", new { urlHandle = game.UrlHandle });
		}

		[HttpPost]
		public async Task<IActionResult> WantToPlay(GameOwnerViewmodel gameOwnerViewmodel)
		{
			var game = await gameRepository.GetAsync(gameOwnerViewmodel.GameId);
			if (signInManager.IsSignedIn(User))
			{
				var userId = Guid.Parse(userManager.GetUserId(User));
				var userProfile = await profileRepository.GetAsync(userId);
				var existingOwner = await gameOwnerRepository.GetAsync(game.Id, userProfile.Id);
				var gameOwner = new GameOwner()
				{
					GameId = gameOwnerViewmodel.GameId,
					OwnerId = gameOwnerViewmodel.OwnerId,
					PlaiyngInterest = gameOwnerViewmodel.PlaiyngInterest,
				};

				if (existingOwner == null)
				{
					var gameOwnerRes = await gameOwnerRepository.AddAsync(gameOwner);
				}
				else
				{
					existingOwner.PlaiyngInterest = gameOwnerViewmodel.PlaiyngInterest;
					var updatedOwner = await UpdateOrDeleteGameOwner(existingOwner);
				}
			}
			return RedirectToAction("Index", "Game",
					new { urlHandle = game.UrlHandle });
		}

		private async Task<GameOwner> UpdateOrDeleteGameOwner(GameOwner gameOwner)
		{
			if (gameOwner.IsOwner || gameOwner.IsFavorite || gameOwner.BuyingInterest > 0 || gameOwner.PlaiyngInterest > 0)
			{
				return await gameOwnerRepository.UpdateAsync(gameOwner);
			}
			else
			{
				return await gameOwnerRepository.DeleteAsync(gameOwner.Id);
			}
		}
	}
}
