using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Repositories
{
	public interface IContentCommentRepository
	{
		Task<Comment> AddAsync(Comment comment);

		Task<IEnumerable<Comment>> GetAllCommentsByContentIdAsync(Guid contentId);
	}
}
