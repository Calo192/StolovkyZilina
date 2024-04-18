using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class AuctionOfferRepository : IRepository<AuctionOffer>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public AuctionOfferRepository(StolovkyDbContext stolovkyDbContext)
		{
			this.stolovkyDbContext = stolovkyDbContext;
		}
		public async Task<AuctionOffer> AddAsync(AuctionOffer item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public Task<AuctionOffer?> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<IEnumerable<AuctionOffer>> GetAllAsync()
		{
            return await stolovkyDbContext.AuctionOffers.Include(x => x.User).Include(x => x.Game).OrderByDescending(x => x.Offer).ToListAsync();
        }

		public async Task<IEnumerable<AuctionOffer>> GetAllAsync(string urlHandle)
		{
            return await stolovkyDbContext.AuctionOffers.Include(x => x.User).Include(x => x.Game).Where(x => x.EventId == Guid.Parse(urlHandle)).OrderByDescending(x => x.Offer).ToListAsync();
        }

		public Task<AuctionOffer?> GetAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<AuctionOffer?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<AuctionOffer?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public Task<AuctionOffer?> UpdateAsync(AuctionOffer item)
		{
			throw new NotImplementedException();
		}
	}
}
