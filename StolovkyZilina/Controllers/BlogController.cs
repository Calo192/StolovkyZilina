using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
    public class BlogController : Controller
    {
        private readonly IRepository<BlogPost> bloGpostRepository;
		private readonly IContentRatingRepository blogPostLikeRepository;
		private readonly IContentCommentRepository contentCommentRepository;
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;

		public BlogController(IRepository<BlogPost> bloGpostRepository, 
			IContentRatingRepository blogPostLikeRepository, 
			IContentCommentRepository contentCommentRepository, 
			UserManager<IdentityUser> userManager, 
			SignInManager<IdentityUser> signInManager)
        {
            this.bloGpostRepository = bloGpostRepository;
			this.blogPostLikeRepository = blogPostLikeRepository;
			this.contentCommentRepository = contentCommentRepository;
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blog = await bloGpostRepository.GetAsync(urlHandle);

			var blogDetailsViewModel = new BlogDetailsViewModel();

			if (blog != null)
            {
                int [] totalLikes = new int[5];

				for(int i = 0; i < 5; i++) 
				{
					totalLikes[i] = await blogPostLikeRepository.GetTotalRatings(blog.Id,i+1);
				}

				var comments = await contentCommentRepository.GetAllCommentsByContentIdAsync(blog.Id);

				var blogCommentsForView = new List<ContentCommentViewModel>();

				foreach (var comment in comments)
				{
					blogCommentsForView.Add(new ContentCommentViewModel
					{ 
						Content = comment.Content,
						DateAdded = comment.DateAdded,
						UserName = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
					});
				}

				blogDetailsViewModel = new BlogDetailsViewModel
				{
					Id = blog.Id,
					Content = blog.Content,
					PageTitle = blog.PageTitle,
					Author = blog.Author,
					FeaturedImage = blog.FeaturedImage,
					Heading = blog.Heading,
					PublishDate = blog.PublishDate,
					ShortDesc = blog.ShortDesc,
					UrlHandle = urlHandle,
					Visible = blog.Visible,
					Tags = blog.Tags,
					TotalLikes = totalLikes,
					Comments = blogCommentsForView,
				};
			}

            return View(blogDetailsViewModel);
        }

		[HttpPost]
		public async Task<IActionResult> Index(BlogDetailsViewModel blogDetailsViewModel)
		{
			if (signInManager.IsSignedIn(User) && !string.IsNullOrWhiteSpace(blogDetailsViewModel.CommentContent))
			{
				var domainModel = new Comment
				{
					ContentId = blogDetailsViewModel.Id,
					Content = blogDetailsViewModel.CommentContent,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					DateAdded = DateTime.Now
				};
				await contentCommentRepository.AddAsync(domainModel);
				return RedirectToAction("Index", "Blog", 
					new { urlHandle = blogDetailsViewModel.UrlHandle});
			}

			return View();
		}
    }
}
