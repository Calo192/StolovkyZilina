using StolovkyZilina.Models.Domain;
using System.Threading.Tasks;

namespace StolovkyZilina.Repositories
{
	public interface IContentRatingRepository
	{
		Task<int> GetTotalRatings(Guid contentId);
		Task<int> GetTotalRatings(Guid contentId, int rating);
		Task<Rating?> GetRatingFromUser(Guid userId, Guid contentId);
		Task<Rating> AddRatingForContent(Rating rating);
		Task<Rating?> UpdateAsync(Rating rating);
	}
}
