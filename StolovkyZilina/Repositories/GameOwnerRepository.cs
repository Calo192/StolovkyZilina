using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;

namespace StolovkyZilina.Repositories
{
	public class GameOwnerRepository : IGameRelationRepository<GameOwner>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public GameOwnerRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

		public async Task<GameOwner> AddAsync(GameOwner item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public async Task<GameOwner?> DeleteAsync(Guid id)
		{
			var existingRelationship = await stolovkyDbContext.Owners.FindAsync(id);
			if (existingRelationship != null)
			{
				stolovkyDbContext.Owners.Remove(existingRelationship);
				await stolovkyDbContext.SaveChangesAsync();
				return existingRelationship;
			}
			return null;
		}

		public async Task<IEnumerable<GameOwner>> GetAllByGameIdAsync(Guid gameId)
		{
			return await stolovkyDbContext.Owners.Include(x => x.Language).Include(x => x.BoardGame).Include(x => x.Owner).Where(x => x.GameId == gameId).ToListAsync();
		}

		public async Task<IEnumerable<GameOwner>> GetAllByUserIdAsync(Guid userId)
		{
			return await stolovkyDbContext.Owners.Include(x => x.Language).Include(x => x.BoardGame).Include(x => x.Owner).Where(x => x.OwnerId == userId).ToListAsync();
		}

		public async Task<GameOwner> GetAsync(Guid gameId, Guid userId)
		{
			return await stolovkyDbContext.Owners.Include(x => x.Language).Include(x => x.BoardGame).Include(x => x.Owner).FirstOrDefaultAsync(x => x.GameId == gameId && x.OwnerId == userId);
		}

		public async Task<GameOwner> GetByGameIdAsync(Guid gameId)
		{
			return await stolovkyDbContext.Owners.Include(x => x.Language).Include(x => x.BoardGame).Include(x => x.Owner).FirstOrDefaultAsync(x => x.GameId == gameId);
		}

		public async Task<GameOwner> GetByUserIdAsync(Guid userId)
		{
			return await stolovkyDbContext.Owners.Include(x => x.Language).Include(x => x.BoardGame).Include(x => x.Owner).FirstOrDefaultAsync(x => x.OwnerId == userId);
		}

		public async Task<GameOwner?> UpdateAsync(GameOwner owner)
		{
			var existingOwner = await stolovkyDbContext.Owners.Include(x => x.Language).Include(x => x.BoardGame).Include(x => x.Owner).FirstOrDefaultAsync(x => x.Id == owner.Id);

			if (existingOwner != null) 
			{
				existingOwner.LanguageId = owner.LanguageId;
				existingOwner.Condition = owner.Condition;
				existingOwner.DeluxeComponents = owner.DeluxeComponents;
				existingOwner.PromoContent = owner.PromoContent;
				existingOwner.Insert = owner.Insert;
				existingOwner.IsOwner = owner.IsOwner;
				existingOwner.IsFavorite = owner.IsFavorite;
				existingOwner.BuyingInterest = owner.BuyingInterest;
				existingOwner.PlaiyngInterest = owner.PlaiyngInterest;

				await stolovkyDbContext.SaveChangesAsync();
				return existingOwner;
			}

			return null;
		}
	}
}
