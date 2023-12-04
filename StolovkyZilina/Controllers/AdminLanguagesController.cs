using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
	public class AdminLanguagesController : Controller
	{
		private readonly IRepository<GameLanguage> languageRepository;

		public AdminLanguagesController(IRepository<GameLanguage> languageRepository)
        {
			this.languageRepository = languageRepository;
		}

        [HttpGet]
		[ActionName("List")]
		public async Task<IActionResult> List()
		{
			var tags = await languageRepository.GetAllAsync();

			var langViewModel = new AdminLanguagesViewModel() { Languages = tags.ToList() };

			return View(langViewModel);
		}

		[HttpPost]
		[ActionName("List")]
		public async Task<IActionResult> List(AdminLanguagesViewModel adminLanguagesViewModel)
		{
			var lang = new GameLanguage { LanguageName = adminLanguagesViewModel.DisplayName };
			await languageRepository.AddAsync(lang);
			return RedirectToAction("List");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deletedLang = await languageRepository.DeleteAsync(id);

			return RedirectToAction("List");
		}
	}
}
