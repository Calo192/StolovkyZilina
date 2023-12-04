using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Util;
using System.Net.WebSockets;

namespace StolovkyZilina.Data
{
	public class AuthDbContext : IdentityDbContext
	{
		public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);


			// Seed Roles (User, Admin, SuperAdmin)

			var adminRoleId			= "37cc67e1-41ca-461c-bf34-2b5e62dbae32";
			var superAdminRoleId	= "3cfd9eee-08cb-4da3-9e6f-c3166b50d3b0";
            var userP1Id			= "a0cab2c3-6558-4a1c-be81-dfb39180da3d";
            var userP2Id			= "09969b9a-e73c-44cd-a8c1-1e454db046bd";
            var userP3Id			= "b04bd9fc-d775-4215-8084-19947c03400c";
            

            var roles = new List<IdentityRole>
			{
				new IdentityRole
				{
					Name= Constants.Admin,
					NormalizedName = Constants.Admin,
					Id = adminRoleId,
					ConcurrencyStamp = adminRoleId
				},
				new IdentityRole
				{
					Name = Constants.SuperAdmin,
					NormalizedName = Constants.SuperAdmin,
					Id = superAdminRoleId,
					ConcurrencyStamp = superAdminRoleId
				},
                new IdentityRole
                {
                    Name = Constants.UserP1,
					NormalizedName = Constants.UserP1,
					Id = userP1Id,
                    ConcurrencyStamp = userP1Id
                },
                new IdentityRole
                {
                    Name = Constants.UserP2,
					NormalizedName = Constants.UserP2,
					Id = userP2Id,
                    ConcurrencyStamp = userP2Id
                },
                new IdentityRole
                {
                    Name = Constants.UserP3,
					NormalizedName = Constants.UserP3,
					Id = userP3Id,
                    ConcurrencyStamp = userP3Id
                }
            };


			builder.Entity<IdentityRole>().HasData(roles);

			// Seed SuperAdminUser
			var superAdminId = "472ba632-6133-44a1-b158-6c10bd7d850d";
			var superAdminUser = new IdentityUser
			{
				UserName = Constants.SuperAdminEmail,
				Email = Constants.SuperAdminEmail,
				NormalizedEmail = Constants.SuperAdminEmail.ToUpper(),
				NormalizedUserName = Constants.SuperAdminEmail.ToUpper(),
				Id = superAdminId
			};


			superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
				.HashPassword(superAdminUser, "Zidan9!");


			builder.Entity<IdentityUser>().HasData(superAdminUser);


			// Add All roles to SuperAdminUser
			var superAdminRoles = new List<IdentityUserRole<string>>
			{
				new IdentityUserRole<string>
				{
					RoleId = adminRoleId,
					UserId = superAdminId
				},
				new IdentityUserRole<string>
				{
					RoleId = superAdminRoleId,
					UserId = superAdminId
				},
                new IdentityUserRole<string>
                {
                    RoleId = userP1Id,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userP2Id,
                    UserId = superAdminId
                },
                new IdentityUserRole<string>
                {
                    RoleId = userP3Id,
                    UserId = superAdminId
                }
            };

			builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

		}
	}
}
