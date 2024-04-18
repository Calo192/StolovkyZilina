using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class GameRepository : IRepository<Game>
	{
        private readonly StolovkyDbContext stolovkyDbContext;

        public GameRepository(StolovkyDbContext stolovkyDbContext)
        {
            this.stolovkyDbContext = stolovkyDbContext;
        }

        public async Task<Game> AddAsync(Game game)
		{
            await stolovkyDbContext.AddAsync(game);
            await stolovkyDbContext.SaveChangesAsync();
            return game;
        }

		public async Task<Game?> DeleteAsync(Guid id)
		{
			var existingGame = await stolovkyDbContext.Games.FindAsync(id);
			if (existingGame != null)
			{
				stolovkyDbContext.Games.Remove(existingGame);
				await stolovkyDbContext.SaveChangesAsync();
				return existingGame;
			}
			return null;
		}

		public async Task<IEnumerable<Game>> GetAllAsync()
		{
            return await stolovkyDbContext.Games.Include(x => x.Content).Include(x => x.Owners).ToListAsync();
        }

		public Task<IEnumerable<Game>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public async Task<Game?> GetAsync(Guid id)
		{
            return await stolovkyDbContext.Games.Include(x => x.Content).Include(x => x.Content.Tags).Include(x => x.Content.Likes).Include(x => x.Content.Comments).Include(x => x.GameCategory).FirstOrDefaultAsync(x => x.Id == id);
		}

		public async Task<Game?> GetAsync(string urlHandle)
		{
			return await stolovkyDbContext.Games.Include(x => x.Content).Include(x => x.Content.Tags).Include(x => x.Content.Likes).Include(x => x.Content.Comments).Include(x => x.GameCategory).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
		}

		public async Task<Game?> GetAsyncByName(string name)
		{
			return await stolovkyDbContext.Games.FirstOrDefaultAsync(x => x.Name == name);
		}

		public async Task<Game?> UpdateAsync(Game game)
		{
			var existingGame = await stolovkyDbContext.Games.Include(x => x.Content).FirstOrDefaultAsync(x => x.Id == game.Id);
			if (existingGame != null)
			{
				existingGame.Id = game.Id;
				existingGame.ContentId = game.ContentId;
                existingGame.Name = game.Name;
                existingGame.Author = game.Author;
                existingGame.Desc = game.Desc;
                existingGame.ShortDesc = game.ShortDesc;
                existingGame.Difficulty = game.Difficulty;
                existingGame.Playtime = game.Playtime;
                existingGame.MinPlayerCount = game.MinPlayerCount;
                existingGame.MaxPlayerCount = game.MaxPlayerCount;
				existingGame.Cooperative = game.Cooperative;
				existingGame.OnPoints = game.OnPoints;
				existingGame.FeaturedImage = game.FeaturedImage;
                existingGame.SpaceRequirement = game.SpaceRequirement;
                existingGame.UrlHandle = game.UrlHandle;
				existingGame.GameCategoryId = game.GameCategoryId;
				existingGame.Approved = game.Approved;
				existingGame.Content = game.Content;

				await stolovkyDbContext.SaveChangesAsync();
				return existingGame;
			}
			return null;
		}
	}
}
