using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class GamePlayRepository : IRepository<GamePlay>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public GamePlayRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<GamePlay> AddAsync(GamePlay item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public async Task<GamePlay?> DeleteAsync(Guid id)
		{
			var existingPlay = await stolovkyDbContext.Plays.FindAsync(id);
			if (existingPlay != null)
			{
				stolovkyDbContext.Plays.Remove(existingPlay);
				await stolovkyDbContext.SaveChangesAsync();
				return existingPlay;
			}
			return null;
		}

		public async Task<IEnumerable<GamePlay>> GetAllAsync()
		{
			return await stolovkyDbContext.Plays.Include(x => x.Results).ThenInclude(x => x.Player).Include(x => x.Location).Include(x => x.Game).Include(x => x.Content).OrderByDescending(x => x.StartTime).ToListAsync();
		}

		public async Task<IEnumerable<GamePlay>> GetAllAsync(string urlHandle)
		{
			return await stolovkyDbContext.Plays.Include(x => x.Results).ThenInclude(x => x.Player).Include(x => x.Location).Include(x => x.Game).Include(x => x.Content).Where(x => x.Game.UrlHandle == urlHandle).OrderByDescending(x => x.StartTime).ToListAsync();
		}

		public async Task<GamePlay?> GetAsync(Guid id)
		{
			return await stolovkyDbContext.Plays.Include(x => x.Results).ThenInclude(x => x.Player).Include(x => x.Location).Include(x => x.Game).Include(x => x.Content).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<GamePlay?> GetAsync(string urlHandle)
		{
			return await stolovkyDbContext.Plays.Include(x => x.Results).ThenInclude(x => x.Player).Include(x => x.Location).Include(x => x.Game).Include(x => x.Content).FirstOrDefaultAsync(x => x.Game.UrlHandle == urlHandle);
		}

		public async Task<GamePlay?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public async Task<GamePlay?> UpdateAsync(GamePlay item)
		{
            var existingGamePlay = await stolovkyDbContext.Plays.Include(x => x.Results).ThenInclude(x => x.Player).Include(x => x.Location).Include(x => x.Game).FirstOrDefaultAsync(x => x.Id == item.Id);
            if (existingGamePlay != null)
            {
                existingGamePlay.Id = item.Id;
				existingGamePlay.ContentId = item.ContentId;
                existingGamePlay.GameId = item.GameId;
                existingGamePlay.LocationId = item.LocationId;
                existingGamePlay.StartTime = item.StartTime;
                existingGamePlay.EndTime = item.EndTime;
                existingGamePlay.Desc = item.Desc;
				existingGamePlay.Results = item.Results;

				await stolovkyDbContext.SaveChangesAsync();
                return existingGamePlay;
            }
            return null;
        }
	}
}
