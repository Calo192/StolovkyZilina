using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class ContentRepository : IRepository<Content>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public ContentRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<Content> AddAsync(Content item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public async Task<Content?> DeleteAsync(Guid id)
		{
			var existingContent = await stolovkyDbContext.Contents.FindAsync(id);
			if (existingContent != null)
			{
				stolovkyDbContext.Contents.Remove(existingContent);
				await stolovkyDbContext.SaveChangesAsync();
				return existingContent;
			}
			return null;
		}

		public Task<IEnumerable<Content>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Content>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public async Task<Content?> GetAsync(Guid id)
		{
            return await stolovkyDbContext.Contents.Include(x => x.Tags).Include(x => x.Likes).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }

		public Task<Content?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<Content?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public async Task<Content?> UpdateAsync(Content item)
		{
            var existingContent = await stolovkyDbContext.Contents.Include(x => x.Tags).Include(x => x.Likes).Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == item.Id);
            if (existingContent != null)
            {
                existingContent.Id = item.Id;
                existingContent.Tags = item.Tags;
                existingContent.Likes = item.Likes;
                existingContent.Comments = item.Comments;

                await stolovkyDbContext.SaveChangesAsync();
                return existingContent;
            }
            return null;
        }
	}
}
