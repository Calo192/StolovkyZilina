using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.Requests;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class BlogPostRatingController : Controller
	{
		private readonly IContentRatingRepository contentRatingRepository;

		public BlogPostRatingController(IContentRatingRepository contentRatingRepository)
		{
			this.contentRatingRepository = contentRatingRepository;
		}

		[HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddRating([FromBody] AddRatingRequest addRatingRequest)
		{
			if (addRatingRequest != null)
			{
				var previousRating = await contentRatingRepository.GetRatingFromUser(addRatingRequest.UserId, addRatingRequest.ContentId);
				if (previousRating != null)
				{
					var previousRatingValue = previousRating.UserRating;
					previousRating.UserRating = addRatingRequest.Rating;
					var updatedRating = await contentRatingRepository.UpdateAsync(previousRating);
					if (updatedRating != null)
					{
						return Ok(previousRatingValue);
					}
				}

				var model = new Rating
				{
					ContentId = addRatingRequest.ContentId,
					UserId = addRatingRequest.UserId,
					UserRating = addRatingRequest.Rating
				};
				await contentRatingRepository.AddRatingForContent(model);
			}

			return Ok();
		}

		[HttpGet]
		[Route("{contentId}/{userRating}/totalLikes")]
		public async Task<IActionResult> GetTotalRatings([FromRoute] Guid contentId, [FromRoute] int userRating)
		{
			var totalLikes = 0;
			if (userRating != 0)
			{
				totalLikes = await contentRatingRepository.GetTotalRatings(contentId, userRating);
			}
			else
			{
				totalLikes = await contentRatingRepository.GetTotalRatings(contentId);
			}
			return Ok(totalLikes);
		}
	}
}
