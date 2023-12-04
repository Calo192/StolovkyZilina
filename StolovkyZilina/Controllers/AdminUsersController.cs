using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;
using StolovkyZilina.Util;
using System.Data;

namespace StolovkyZilina.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminUsersController : Controller
	{
		private readonly IUserRepository userRepository;
        private readonly UserManager<IdentityUser> userManager;

        public AdminUsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
			this.userRepository = userRepository;
            this.userManager = userManager;
        }

        [HttpGet]
		public async Task<IActionResult> List()
		{
			var users = await userRepository.GetAll();

			var UserViewModel = new UsersViewModel();
			UserViewModel.Users = new List<UserViewModel>();

			foreach (var user in users)
			{
				var roles = await userManager.GetRolesAsync(user);
				UserViewModel.Users.Add(new UserViewModel()
				{
					Id = Guid.Parse(user.Id),
					Name = user.UserName,
					Email = user.Email,
					PhoneNumber = user.PhoneNumber,
					VerifiedEmail = user.EmailConfirmed,
                    Role = Constants.GetHighestRole(roles.ToList())
                });
			}

			return View(UserViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> Delete(Guid id)
		{
			var user = await userManager.FindByIdAsync(id.ToString());

			if (user != null)
			{
				var identityResult = await userManager.DeleteAsync(user);

				if (identityResult != null && identityResult.Succeeded)
				{
					return RedirectToAction("List", "AdminUsers");
				}

			}

			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Edit(Guid id)
		{
			var user = await userManager.FindByIdAsync(id.ToString());
			var roles = await userManager.GetRolesAsync(user);
			var highestRole = Constants.GetHighestRole(roles.ToList());
			var uvm = new UserViewModel()
			{
				Id = id,
				Email = user.Email,
				Name = user.UserName,
				PhoneNumber = user.PhoneNumber,
				VerifiedEmail = user.EmailConfirmed,
				Role = highestRole,
			};

			return View(uvm);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(UserViewModel uvm)
		{
			if (uvm != null)
			{
				var user = await userManager.FindByIdAsync(uvm.Id.ToString());
				if (user != null)
				{
					user.PhoneNumber = uvm.PhoneNumber;
					user.Email = uvm.Email;
					user.UserName = uvm.Name;
					if (!string.IsNullOrEmpty(uvm.Password))
					{
						user.PasswordHash = userManager.PasswordHasher.HashPassword(user, uvm.Password);
					}
					var allRoles = Constants.GetAllRoles(false);
					var roles = Constants.GetFittingRoles(uvm.Role);
					foreach (var role in allRoles)
					{
						var isInRole = await userManager.IsInRoleAsync(user, role);
						if (isInRole && !roles.Contains(role))
						{
							await userManager.RemoveFromRoleAsync(user, role);
						}
						if (!isInRole && roles.Contains(role))
						{
							await userManager.AddToRoleAsync(user, role);
						}
					}
					var updatedUser = await userManager.UpdateAsync(user);
				}
			}
			
			return View(uvm);
		}
	}
}
