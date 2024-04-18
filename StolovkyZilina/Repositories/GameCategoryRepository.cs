using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class GameCategoryRepository : IRepository<GameCategory>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public GameCategoryRepository(StolovkyDbContext stolovkyDbContext)
		{
			this.stolovkyDbContext = stolovkyDbContext;
		}

		public async Task<GameCategory> AddAsync(GameCategory item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public async Task<GameCategory?> DeleteAsync(Guid id)
		{
			var ecistingCategory = await stolovkyDbContext.GameCategories.FindAsync(id);
			if (ecistingCategory != null)
			{
				stolovkyDbContext.GameCategories.Remove(ecistingCategory);
				await stolovkyDbContext.SaveChangesAsync();
				return ecistingCategory;
			}
			return null;
		}

		public async Task<IEnumerable<GameCategory>> GetAllAsync()
		{
			return await stolovkyDbContext.GameCategories.ToListAsync();
		}

		public Task<IEnumerable<GameCategory>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<GameCategory?> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<GameCategory?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<GameCategory?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public Task<GameCategory?> UpdateAsync(GameCategory item)
		{
			throw new NotImplementedException();
		}
	}
}
