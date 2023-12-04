using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.Requests;
using StolovkyZilina.Repositories;
using StolovkyZilina.Util;

namespace StolovkyZilina.Controllers
{
    public class AdminGamesController : Controller
	{
		private readonly IRepository<Game> gameRepository;
        private readonly ITagRepository tagRepository;
        private readonly IRepository<Content> contentRepository;

        public AdminGamesController(IRepository<Game> gameRepository, ITagRepository tagRepository, IRepository<Content> contentRepository)
        {
			this.gameRepository = gameRepository;
            this.tagRepository = tagRepository;
            this.contentRepository = contentRepository;
        }

        [HttpGet]
		public async Task<IActionResult> List()
		{
			// Call the repository
			var games = await gameRepository.GetAllAsync();

			var ordered = games.OrderBy(x => x.Name).ToList().OrderBy(x => x.Approved).ToList();

			return View(ordered);
		}

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var game = await gameRepository.GetAsync(id);
            var tagDomainModel = await tagRepository.GetAllAsync();

            if (game != null)
            {
                //game.Content = await contentRepository.GetAsync(game.ContentId);
                var model = new GameRequest
				{
                    Id = game.Id,
                    ContentId = game.ContentId,
                    Name = game.Name,
                    Desc = game.Desc,
                    Author = game.Author,
                    ShortDesc = game.ShortDesc,
                    Difficulty = game.Difficulty,
                    Playtime = game.Playtime,
                    FeaturedImage = game.FeaturedImage,
                    MinPlayerCount = game.MinPlayerCount,
                    MaxPlayerCount = game.MaxPlayerCount,
                    Cooperative = game.Cooperative,
                    OnPoints = game.OnPoints,
                    SpaceRequirement = game.SpaceRequirement,
                    Approved = game.Approved,

                    Tags = tagDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                    SelectedTags = game.Content.Tags.Select(x => x.Id.ToString()).ToArray()
                };
                return View(model);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GameRequest editGameRequest, IFormFile fileInput)
        {
            var gagmeDomainModel = new Game
            {
                Id = editGameRequest.Id,
                ContentId = editGameRequest.ContentId,
                Name = editGameRequest.Name,
                Author = editGameRequest.Author,
                Desc = editGameRequest.Desc,
                ShortDesc = editGameRequest.ShortDesc,
                Difficulty = editGameRequest.Difficulty,
                Playtime = editGameRequest.Playtime,
                MinPlayerCount = editGameRequest.MinPlayerCount,
                MaxPlayerCount = editGameRequest.MaxPlayerCount,
                OnPoints = editGameRequest.OnPoints,
                Cooperative = editGameRequest.Cooperative,
                FeaturedImage = editGameRequest.FeaturedImage,
                SpaceRequirement = editGameRequest.SpaceRequirement,
                UrlHandle = editGameRequest.Name.Replace(' ', '-'),
                Approved = editGameRequest.Approved
            };

			if (fileInput != null && fileInput.Length > 0)
			{
				using (var memoryStream = new MemoryStream())
				{
					fileInput.CopyTo(memoryStream);
					gagmeDomainModel.FeaturedImage = ImageUtil.CropToSquare(memoryStream.ToArray());
				}
			}

			var selectedTags = new List<Tag>();
            foreach (var tag in editGameRequest.SelectedTags)
            {
                if (Guid.TryParse(tag, out var t))
                {
                    var foundaTag = await tagRepository.GetAsync(t);

                    if (foundaTag != null)
                    {
                        selectedTags.Add(foundaTag);
                    }
                }
            }

            gagmeDomainModel.Content = await contentRepository.GetAsync(gagmeDomainModel.ContentId);
            gagmeDomainModel.Content.Tags = selectedTags;

            var updatedGame = await gameRepository.UpdateAsync(gagmeDomainModel);
            if (updatedGame != null)
            {
                return RedirectToAction("Edit");
            }
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(GameRequest editGameRequest)
        {
            var deletedGame = await gameRepository.DeleteAsync(editGameRequest.Id);
            if (deletedGame != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new { id = editGameRequest.Id });
        }
    }
}
