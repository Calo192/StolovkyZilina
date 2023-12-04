using Microsoft.AspNetCore.Identity;

namespace StolovkyZilina.Repositories
{
	public interface IUserRepository
	{
		public Task<IEnumerable<IdentityUser>> GetAll();
	}
}
