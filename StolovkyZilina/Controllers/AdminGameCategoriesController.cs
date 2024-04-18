using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
	public class AdminGameCategoriesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		private readonly IRepository<GameCategory> categoryRepository;

		public AdminGameCategoriesController(IRepository<GameCategory> categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		[HttpGet]
		[ActionName("List")]
		public async Task<IActionResult> List()
		{
			var categories = await categoryRepository.GetAllAsync();

			var langViewModel = new AdminCategoriesViewModel() { Categories = categories.ToList() };

			return View(langViewModel);
		}

		[HttpPost]
		[ActionName("List")]
		public async Task<IActionResult> List(AdminCategoriesViewModel adminCategoriesViewModel)
		{
			var cat = new GameCategory { Name = adminCategoriesViewModel.DisplayName };
			await categoryRepository.AddAsync(cat);
			return RedirectToAction("List");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id)
		{
			var deletedLang = await categoryRepository.DeleteAsync(id);

			return RedirectToAction("List");
		}
	}
}
