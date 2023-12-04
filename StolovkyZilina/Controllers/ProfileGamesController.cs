using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
	public class ProfileGamesController : Controller
	{
		private readonly IGameRelationRepository<GameOwner> gameRelationRepository;

		public ProfileGamesController(IGameRelationRepository<GameOwner> gameRelationRepository)
        {
			this.gameRelationRepository = gameRelationRepository;
		}

		public async Task<IActionResult> ListGames(Guid id)
		{
			var games = await gameRelationRepository.GetAllByUserIdAsync(id);
			return View(games);
		}

		public async Task<IActionResult> ListOwners(Guid id)
		{
			var games = await gameRelationRepository.GetAllByGameIdAsync(id);
			return View(games);
		}
	}
}
