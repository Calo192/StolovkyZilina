namespace StolovkyZilina.Repositories
{
	public interface IGameRelationRepository<T>
	{
		Task<T> AddAsync(T item);
		Task<IEnumerable<T>> GetAllByGameIdAsync(Guid gameId);
		Task<IEnumerable<T>> GetAllByUserIdAsync(Guid userId);
		Task<T> GetByGameIdAsync(Guid gameId);
		Task<T> GetByUserIdAsync(Guid userId);
		Task<T> GetAsync(Guid gameId, Guid userId);
		Task<T?> UpdateAsync(T item);
		Task<T?> DeleteAsync(Guid id);
	}
}
