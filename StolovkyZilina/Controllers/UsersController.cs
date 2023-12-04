using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
	public class UsersController : Controller
    {
        private readonly IRepository<UserProfile> userProfileRepository;
        private readonly UserManager<IdentityUser> userManager;

        public UsersController(IRepository<UserProfile> userProfileRepository, UserManager<IdentityUser> userManager)
        {
            this.userProfileRepository = userProfileRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var users = await userProfileRepository.GetAllAsync();

            var list = new List<UserProfileViewModel>();

            foreach (var user in users)
            {
                var identityUser = await userManager.FindByIdAsync(user.UserId.ToString());
                list.Add(new UserProfileViewModel(user, identityUser.Email, identityUser.PhoneNumber, identityUser.UserName));
            }

            return View(list);
        }
    }
}
