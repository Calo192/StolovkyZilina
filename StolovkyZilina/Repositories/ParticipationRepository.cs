using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class ParticipationRepository : IRepository<ParticipationVote>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public ParticipationRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<ParticipationVote> AddAsync(ParticipationVote item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public Task<ParticipationVote?> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<ParticipationVote>> GetAllAsync()
		{
			return await stolovkyDbContext.ParticipationVotes.ToListAsync();
		}

		public Task<IEnumerable<ParticipationVote>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<ParticipationVote?> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<ParticipationVote?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<ParticipationVote?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public Task<ParticipationVote?> UpdateAsync(ParticipationVote item)
		{
			throw new NotImplementedException();
		}
	}
}
