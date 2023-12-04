using Microsoft.AspNetCore.Mvc;

namespace StolovkyZilina.Controllers
{
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
