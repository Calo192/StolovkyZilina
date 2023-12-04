using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class ContentRatingRepository : IContentRatingRepository
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public ContentRatingRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

		public async Task<Rating> AddRatingForContent(Rating rating)
		{
			await stolovkyDbContext.AddAsync(rating);
			await stolovkyDbContext.SaveChangesAsync();
			return rating;
		}

		public async Task<Rating?> GetRatingFromUser(Guid userId, Guid contentId)
		{
			return await stolovkyDbContext.Ratings.FirstOrDefaultAsync(x => x.UserId == userId && x.ContentId == contentId);
		}

		public async Task<int> GetTotalRatings(Guid blogPostId)
		{
			return await stolovkyDbContext.Ratings.CountAsync(x => x.ContentId == blogPostId);
		}

		public async Task<int> GetTotalRatings(Guid blogPostId, int rating)
		{
			return await stolovkyDbContext.Ratings.CountAsync(x => x.ContentId == blogPostId && x.UserRating == rating);
		}

		public async Task<Rating?> UpdateAsync(Rating rating)
		{
			var existingRating = await stolovkyDbContext.Ratings.FirstOrDefaultAsync(x => x.Id == rating.Id);

			if (existingRating != null)
			{
				existingRating.UserRating = rating.UserRating;
				await stolovkyDbContext.SaveChangesAsync();
				return existingRating;
			}
			return null;
		}
	}
}
