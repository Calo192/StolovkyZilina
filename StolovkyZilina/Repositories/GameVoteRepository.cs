using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class GameVoteRepository : IRepository<GameVote>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public GameVoteRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<GameVote> AddAsync(GameVote item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public async Task<GameVote?> DeleteAsync(Guid id)
		{
			var existing = await stolovkyDbContext.GameVotes.FindAsync(id);
			if (existing != null)
			{
				stolovkyDbContext.GameVotes.Remove(existing);
				await stolovkyDbContext.SaveChangesAsync();
				return existing;
			}
			return null;
		}

		public Task<IEnumerable<GameVote>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<GameVote>> GetAllAsync(string gamePollId)
		{
			return await stolovkyDbContext.GameVotes.Include(x => x.Game).Include(x => x.User).Where(x => x.GamePollId == Guid.Parse(gamePollId)).ToListAsync();
		}

		public async Task<GameVote?> GetAsync(Guid id)
		{
			return await stolovkyDbContext.GameVotes.Include(x => x.Game).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task<GameVote?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<GameVote?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public Task<GameVote?> UpdateAsync(GameVote item)
		{
			throw new NotImplementedException();
		}
	}
}
