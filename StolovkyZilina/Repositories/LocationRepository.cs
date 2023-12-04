using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class LocationRepository : IRepository<Location>
    {
        private readonly StolovkyDbContext stolovkyDbContext;

        public LocationRepository(StolovkyDbContext stolovkyDbContext)
        {
            this.stolovkyDbContext = stolovkyDbContext;
        }
        public async Task<Location> AddAsync(Location item)
        {
            await stolovkyDbContext.AddAsync(item);
            await stolovkyDbContext.SaveChangesAsync();
            return item;
        }

        public async Task<Location?> DeleteAsync(Guid id)
        {
            var existingLocation = await stolovkyDbContext.Locations.FindAsync(id);
            if (existingLocation != null)
            {
                stolovkyDbContext.Locations.Remove(existingLocation);
                await stolovkyDbContext.SaveChangesAsync();
                return existingLocation;
            }
            return null;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await stolovkyDbContext.Locations.ToListAsync();
        }

		public Task<IEnumerable<Location>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public async Task<Location?> GetAsync(Guid id)
        {
            return await stolovkyDbContext.Locations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Location?> GetAsync(string urlHandle)
        {
            throw new NotImplementedException();
        }

		public Task<Location?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public Task<Location?> UpdateAsync(Location item)
        {
            throw new NotImplementedException();
        }
    }
}
