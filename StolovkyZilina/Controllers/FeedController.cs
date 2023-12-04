using Microsoft.AspNetCore.Mvc;
using X.PagedList;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
    public class FeedController : Controller
    {
        private readonly IRepository<Feed> feedRepository;

        public FeedController(IRepository<Feed> feedRepository)
        {
            this.feedRepository = feedRepository;
        }

        [HttpGet]
        public async Task<IActionResult> List(int? page)
        {
            int pageSize = 25;
            int pageNumber = page ?? 1;
            var feeds = await feedRepository.GetAllAsync();
            IPagedList<Feed> pagedModel = feeds.ToPagedList(pageNumber, pageSize);
            return View(pagedModel);
        }
    }
}