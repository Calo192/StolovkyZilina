using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class PlayerMmrRepository : IRepository<PlayerMmr>
	{
		public Task<PlayerMmr> AddAsync(PlayerMmr item)
		{
			throw new NotImplementedException();
		}

		public Task<PlayerMmr?> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<PlayerMmr>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<PlayerMmr>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<PlayerMmr?> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<PlayerMmr?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<PlayerMmr?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public Task<PlayerMmr?> UpdateAsync(PlayerMmr item)
		{
			throw new NotImplementedException();
		}
	}
}
