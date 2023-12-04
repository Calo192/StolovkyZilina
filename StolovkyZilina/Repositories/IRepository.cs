namespace StolovkyZilina.Repositories
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T?> GetAsync(Guid id);
		Task<T?> GetAsync(string urlHandle);
		Task<IEnumerable<T>> GetAllAsync(string urlHandle);
		Task<T?> GetAsyncByName(string name);
		Task<T> AddAsync(T item);
		Task<T?> UpdateAsync(T item);
		Task<T?> DeleteAsync(Guid id);
	}
}
