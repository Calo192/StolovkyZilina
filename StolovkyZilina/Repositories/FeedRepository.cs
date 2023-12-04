using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class FeedRepository : IRepository<Feed>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public FeedRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<Feed> AddAsync(Feed item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public Task<Feed?> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<Feed>> GetAllAsync()
		{
			return await stolovkyDbContext.Feeds.Include(x => x.Tags).OrderByDescending(x => x.DateAdded).ToListAsync();
		}

		public Task<IEnumerable<Feed>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<Feed?> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<Feed?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<Feed?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public Task<Feed?> UpdateAsync(Feed item)
		{
			throw new NotImplementedException();
		}
	}
}
