using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
    public class GamePollRepository : IRepository<GamePoll>
    {
        private readonly StolovkyDbContext stolovkyDbContext;

        public GamePollRepository(StolovkyDbContext stolovkyDbContext)
        {
            this.stolovkyDbContext = stolovkyDbContext;
        }

        public async Task<GamePoll> AddAsync(GamePoll item)
        {
            await stolovkyDbContext.AddAsync(item);
            await stolovkyDbContext.SaveChangesAsync();
            return item;
        }

        public Task<GamePoll?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GamePoll>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GamePoll>> GetAllAsync(string urlHandle)
        {
            throw new NotImplementedException();
        }

        public async Task<GamePoll?> GetAsync(Guid id)
        {
			return await stolovkyDbContext.GamePolls.Include(x => x.GamesInPoll).Include(x => x.Event).ThenInclude(x => x.ParticipationVotes).Include(x => x.GameVotes).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
		}

        public Task<GamePoll?> GetAsync(string urlHandle)
        {
            throw new NotImplementedException();
        }

        public Task<GamePoll?> GetAsyncByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<GamePoll?> UpdateAsync(GamePoll item)
        {
            throw new NotImplementedException();
        }
    }
}
