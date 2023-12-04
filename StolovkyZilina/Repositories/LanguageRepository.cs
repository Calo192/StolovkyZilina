using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class LanguageRepository : IRepository<GameLanguage>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public LanguageRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<GameLanguage> AddAsync(GameLanguage item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public async Task<GameLanguage?> DeleteAsync(Guid id)
		{
			var existingLanguage = await stolovkyDbContext.Languages.FindAsync(id);
			if (existingLanguage != null)
			{
				stolovkyDbContext.Languages.Remove(existingLanguage);
				await stolovkyDbContext.SaveChangesAsync();
				return existingLanguage;
			}
			return null;
		}

		public async Task<IEnumerable<GameLanguage>> GetAllAsync()
		{
			return await stolovkyDbContext.Languages.ToListAsync();
		}

		public Task<IEnumerable<GameLanguage>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<GameLanguage?> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<GameLanguage?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<GameLanguage?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public Task<GameLanguage?> UpdateAsync(GameLanguage item)
		{
			throw new NotImplementedException();
		}
	}
}
