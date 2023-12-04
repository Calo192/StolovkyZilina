using Microsoft.AspNetCore.Mvc;

namespace StolovkyZilina.Controllers
{
	public class PlaymatesController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
