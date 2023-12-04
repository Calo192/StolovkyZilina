using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public class ContentCommentRepository : IContentCommentRepository
	{
		private readonly StolovkyDbContext stolovkyDbContext;

		public ContentCommentRepository(StolovkyDbContext stolovkyDbContext)
        {
			this.stolovkyDbContext = stolovkyDbContext;
		}

        public async Task<Comment> AddAsync(Comment comment)
		{
			await stolovkyDbContext.Comments.AddAsync(comment);
			await stolovkyDbContext.SaveChangesAsync();
			return comment;
		}

		public async Task<IEnumerable<Comment>> GetAllCommentsByContentIdAsync(Guid contentId)
		{
			return await stolovkyDbContext.Comments.Where(x => x.ContentId == contentId).OrderByDescending(x => x.DateAdded).ToListAsync();
		}
	}
}
