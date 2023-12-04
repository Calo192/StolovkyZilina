using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StolovkyZilina.Repositories
{
	public class AdminMessagesRepository : IRepository<AdminMessage>
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public AdminMessagesRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<AdminMessage> AddAsync(AdminMessage item)
		{
			await stolovkyDbContext.AddAsync(item);
			await stolovkyDbContext.SaveChangesAsync();
			return item;
		}

		public async Task<AdminMessage?> DeleteAsync(Guid id)
		{
			var message = await stolovkyDbContext.AdminMessages.FindAsync(id);
			if (message != null)
			{
				stolovkyDbContext.AdminMessages.Remove(message);
				await stolovkyDbContext.SaveChangesAsync();
				return message;
			}
			return null;
		}

		public async Task<IEnumerable<AdminMessage>> GetAllAsync()
		{
			return await stolovkyDbContext.AdminMessages.OrderByDescending(x => x.Date).ToListAsync();
		}

		public Task<IEnumerable<AdminMessage>> GetAllAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public async Task<AdminMessage?> GetAsync(Guid id)
		{
			return await stolovkyDbContext.AdminMessages.FirstOrDefaultAsync(x => x.Id == id);
		}

		public Task<AdminMessage?> GetAsync(string urlHandle)
		{
			throw new NotImplementedException();
		}

		public Task<AdminMessage?> GetAsyncByName(string name)
		{
			throw new NotImplementedException();
		}

		public async Task<AdminMessage?> UpdateAsync(AdminMessage item)
		{
			var existingMessage = await stolovkyDbContext.AdminMessages.FirstOrDefaultAsync(x => x.Id == item.Id);
			if (existingMessage != null)
			{
				existingMessage.Id = item.Id;
				existingMessage.Subject = item.Subject;
				existingMessage.Status = item.Status;
				existingMessage.Author = item.Author;
				existingMessage.Date = item.Date;
				existingMessage.Message = item.Message;
				existingMessage.Type = item.Type;

				await stolovkyDbContext.SaveChangesAsync();
				return existingMessage;
			}
			return null;
		}
	}
}
