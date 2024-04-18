using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class EventRepository : IRepository<Event>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public EventRepository(StolovkyDbContext stolovkyDbContext)
		{
			this.stolovkyDbContext = stolovkyDbContext;
		}

		public async Task<Event> AddAsync(Event item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public async Task<Event?> DeleteAsync(Guid id)
		{
			var existingEvent = await GetAsync(id);
			if (existingEvent != null)
			{
				stolovkyDbContext.Events.Remove(existingEvent);
				if (existingEvent.Content != null)
				{
					if (existingEvent.Content.Tags.Any())
					{
						stolovkyDbContext.Tags.RemoveRange(existingEvent.Content.Tags);
					}
					if (existingEvent.Content.Likes.Any())
					{
						stolovkyDbContext.Ratings.RemoveRange(existingEvent.Content.Likes);
					}
					if (existingEvent.Content.Comments.Any())
					{
						stolovkyDbContext.Comments.RemoveRange(existingEvent.Content.Comments);
					}
				}

				if (existingEvent.ParticipationVotes.Any())
				{
					stolovkyDbContext.ParticipationVotes.RemoveRange(existingEvent.ParticipationVotes);
				}

				if (existingEvent.GamePolls.Any())
				{
					foreach (var gamePoll in existingEvent.GamePolls)
					{
						if (gamePoll.GameVotes.Any())
						{
							stolovkyDbContext.GameVotes.RemoveRange(gamePoll.GameVotes);
						}
					}
					stolovkyDbContext.GamePolls.RemoveRange(existingEvent.GamePolls);
				}

				await stolovkyDbContext.SaveChangesAsync();
				return existingEvent;
			}
			return null;
		}

		public async Task<IEnumerable<Event>> GetAllAsync()
		{
			// complete this method
			return await stolovkyDbContext.Events
				.Include(x => x.Content)
				.Include(x => x.Content.Tags)
				.Include(x => x.Content.Likes)
				.Include(x => x.Content.Comments)
				.Include(x => x.ParticipationVotes)
				.Include(x => x.Location)
				.Include(x => x.GamePolls).ThenInclude(x => x.GameVotes).OrderByDescending(x => x.Time)
				.ToListAsync();
		}

		public Task<IEnumerable<Event>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public async Task<Event?> GetAsync(Guid id)
		{
			return await stolovkyDbContext.Events
				.Include(x => x.Content)
				.Include(x => x.Content.Tags)
				.Include(x => x.Content.Likes)
				.Include(x => x.Content.Comments)
				.Include(x => x.ParticipationVotes)
				.Include(x => x.Location)
				.Include(x => x.GamePolls).ThenInclude(x => x.GameVotes)
				.Include(x => x.GamePolls).ThenInclude(x => x.GamesInPoll)
				.FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task<Event?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public async Task<Event?> GetAsyncByName(string name)
		{
			return await stolovkyDbContext.Events
				.Include(x => x.Content)
				.Include(x => x.Content.Tags)
				.Include(x => x.Content.Likes)
				.Include(x => x.Content.Comments)
				.Include(x => x.ParticipationVotes)
                .Include(x => x.GamePolls).ThenInclude(x => x.GameVotes)
				.Include(x => x.GamePolls).ThenInclude(x => x.GamesInPoll)
				.FirstOrDefaultAsync(x => x.Name == name);
		}

		public async Task<Event?> UpdateAsync(Event item)
		{
			var existingEvent = await GetAsync(item.Id);
			if (existingEvent != null)
			{
				existingEvent.Id = item.Id;
				existingEvent.Name = item.Name;
				existingEvent.Description = item.Description;
				existingEvent.ShortDescription = item.ShortDescription;
				existingEvent.Time = item.Time;
				existingEvent.MakeGamesSuggestion = item.MakeGamesSuggestion;
                existingEvent.AuctionType = item.AuctionType;
                existingEvent.LocationId = item.LocationId;
				existingEvent.Content = item.Content;
				existingEvent.ParticipationVotes = item.ParticipationVotes;
                existingEvent.GamePolls = item.GamePolls;

                await stolovkyDbContext.SaveChangesAsync();
				return existingEvent;
			}
			return null;
		}
	}
}
