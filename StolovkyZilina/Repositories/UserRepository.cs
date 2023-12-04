using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Util;

namespace StolovkyZilina.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AuthDbContext authDbContext;

		public UserRepository(AuthDbContext authDbContext)
        {
			this.authDbContext = authDbContext;
		}

        public async Task<IEnumerable<IdentityUser>> GetAll()
		{
			var users = await authDbContext.Users.ToListAsync();

			var sueprAdmin = users.FirstOrDefault(x => !string.IsNullOrEmpty(x.Email) && x.Email.Equals(Constants.SuperAdminEmail));

			if (sueprAdmin != null)
			{
				users.Remove(sueprAdmin);
			}

			return users;
		}
	}
}
