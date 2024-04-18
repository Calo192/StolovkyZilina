using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class UserProfileRepository : IRepository<UserProfile>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public UserProfileRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<UserProfile> AddAsync(UserProfile item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public Task<UserProfile?> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<UserProfile>> GetAllAsync()
		{
            return await stolovkyDbContext.UserProfiles.ToListAsync();
        }

		public Task<IEnumerable<UserProfile>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public async Task<UserProfile?> GetAsync(Guid id)
		{
			return await stolovkyDbContext.UserProfiles.FirstOrDefaultAsync(x => x.UserId == id);
		}

		public async Task<UserProfile?> GetAsync(string id)
		{
			return await stolovkyDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
		}

		public Task<UserProfile?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public async Task<UserProfile?> UpdateAsync(UserProfile item)
		{
			var existing = await stolovkyDbContext.UserProfiles.FirstOrDefaultAsync(x => x.Id == item.Id);
			if (existing != null)
			{
				existing.Name = item.Name;
				existing.Surname = item.Surname;
				existing.FeaturedImage = item.FeaturedImage;
				existing.PrefferedDifficulty = item.PrefferedDifficulty;
				existing.PrefferedPlaytime = item.PrefferedPlaytime;
				existing.PrefferedPlayerCount = item.PrefferedPlayerCount;
				existing.PrefferedPlayDay = item.PrefferedPlayDay;
				existing.City = item.City;
				existing.Influence = item.Influence;
                existing.Desc = item.Desc;
				existing.Competitive = item.Competitive;
				existing.GamesOwned = item.GamesOwned;

				await stolovkyDbContext.SaveChangesAsync();
				return existing;
			}
			return null;
		}
	}
}
