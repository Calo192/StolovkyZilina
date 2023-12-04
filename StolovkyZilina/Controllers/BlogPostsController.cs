using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly IRepository<BlogPost> bloGpostRepository;

        public BlogPostsController(IRepository<BlogPost> bloGpostRepository)
        {
            this.bloGpostRepository = bloGpostRepository;
        }

        [HttpGet]
        public async Task <IActionResult> List()
        {
            var blogs = await bloGpostRepository.GetAllAsync();

            return View(blogs);
        }
    }
}
