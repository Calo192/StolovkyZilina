using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.Requests;
using StolovkyZilina.Repositories;
using StolovkyZilina.Util;

namespace StolovkyZilina.Controllers
{
    public class GamesController : Controller
    {
        private readonly IRepository<Game> gameRepository;
        private readonly ITagRepository tagRepository;
		private readonly IRepository<Content> contentRepository;

		public GamesController(IRepository<Game> gameRepository, ITagRepository tagRepository, IRepository<Content> contentRepository)
        {
            this.gameRepository = gameRepository;
            this.tagRepository = tagRepository;
			this.contentRepository = contentRepository;
		}

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var games = await gameRepository.GetAllAsync();

            return View(games);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var tags = await tagRepository.GetAllAsync();

            var model = new GameRequest
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(GameRequest addGameRequest, IFormFile fileInput)
        {
            var game = new Game
            {
                Name = addGameRequest.Name,
                Desc = addGameRequest.Desc,
                ShortDesc = addGameRequest.ShortDesc,
                Difficulty = addGameRequest.Difficulty,
				Playtime = addGameRequest.Playtime,
                SpaceRequirement = addGameRequest.SpaceRequirement,
				MinPlayerCount = addGameRequest.MinPlayerCount,
                MaxPlayerCount = addGameRequest.MaxPlayerCount,
				Cooperative = addGameRequest.Cooperative,
				OnPoints = addGameRequest.OnPoints,
				UrlHandle = addGameRequest.Name.Replace(' ', '-'),
                Author = addGameRequest.Author,
                Approved = addGameRequest.Approved
            };

            var newContent = await contentRepository.AddAsync(new Content());

            if (newContent != null)
                game.Content = newContent;


			if (fileInput != null && fileInput.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    fileInput.CopyTo(memoryStream);

                    game.FeaturedImage = ImageUtil.CropToSquare(memoryStream.ToArray());
                }
            }

            var selectedTags = new List<Tag>();
            // map tags from selected tags
            foreach (var selectedTagId in addGameRequest.SelectedTags)
            {
                var existingTag = await tagRepository.GetAsync(Guid.Parse(selectedTagId));

                if (existingTag != null)
                {
                    selectedTags.Add(existingTag);
                }
            }

            game.Content.Tags = selectedTags;

            await contentRepository.UpdateAsync(game.Content);

            await gameRepository.AddAsync(game);

            return RedirectToAction("List");
        }
    }
}
