using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;
using StolovkyZilina.Util;

namespace StolovkyZilina.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly IRepository<UserProfile> userProfileRepository;
		private readonly IRepository<Feed> feedRepository;

		public AccountController(UserManager<IdentityUser> userManager, 
			SignInManager<IdentityUser> signInManager, 
			IRepository<UserProfile> userProfileRepository,
			IRepository<Feed> feedRepository)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.userProfileRepository = userProfileRepository;
			this.feedRepository = feedRepository;
		}

        [HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel registerViewModel) 
		{
			if (ModelState.IsValid)
			{
                var iuser = new IdentityUser
                {
                    UserName = registerViewModel.Username,
                    Email = registerViewModel.Email,
                };
				var result = await userManager.CreateAsync(iuser, registerViewModel.Password);

				if (result.Succeeded)
				{
					var roleResult = await userManager.AddToRoleAsync(iuser, Constants.UserP3);
					if (roleResult.Succeeded)
					{
						var createdUser = await userManager.FindByNameAsync(registerViewModel.Username);
						if (createdUser != null)
						{
							var newUserResult = await userProfileRepository.AddAsync(new UserProfile() { UserId = Guid.Parse(createdUser.Id) });

							if (newUserResult != null)
							{
								string userName = registerViewModel.Username;
								string profileUrl = Url.Action("Profile", "Account", new { userName = userName });
								var newFeed = new Feed()
								{
									DateAdded = DateTime.UtcNow,
									Body = "<p><a href=\""+ profileUrl+"\">"+ registerViewModel.Username + "</a> sa zaregistroval</p>",
								};
								await feedRepository.AddAsync(newFeed);
							}
						}
						return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					ModelState.AddModelError(nameof(LoginViewModel.Username), result.Errors.FirstOrDefault()?.Description);
				}
            }
			
			return View();
        }

		[HttpGet]
		public IActionResult Login(string returnUrl)
		{
			var model = new LoginViewModel { ReturnUrl = returnUrl };
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
            if (ModelState.IsValid)
            {
				var signres = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
				if (signres != null)
				{
                    if (signres.Succeeded)
					{
						if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl))
						{
							return Redirect(loginViewModel.ReturnUrl);
						}

						return RedirectToAction("Index", "Home");
					}
					else
					{
                        ModelState.AddModelError(nameof(LoginViewModel.Password), Constants.IncorrectPassError);
                        loginViewModel.RepeatedAttempt = true;
                        return View(loginViewModel);
					}
				}
			}
            
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}

        [HttpGet]
        public IActionResult AccessDenied()
        {
			return View();
        }

		[HttpGet]
		public async Task<IActionResult> EditProfile(string userName)
		{
			var user = await userManager.FindByNameAsync(userName);
			if (user != null)
			{
				var userProfile = await userProfileRepository.GetAsync(Guid.Parse(user.Id));
				if (userProfile == null)
				{
					userProfile = await userProfileRepository.AddAsync(new UserProfile() { UserId = Guid.Parse(user.Id) });
				}
				return View(new UserProfileViewModel(userProfile, user.Email, user.PhoneNumber, user.UserName));
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public async Task<IActionResult> Profile(string userName)
		{
			var user = await userManager.FindByNameAsync(userName);
			if (user != null)
			{
				var userProfile = await userProfileRepository.GetAsync(Guid.Parse(user.Id));
				if (userProfile == null)
				{
					userProfile = await userProfileRepository.AddAsync(new UserProfile() { UserId = Guid.Parse(user.Id) });
				}
				return View(new UserProfileViewModel(userProfile, user.Email, user.PhoneNumber, user.UserName));
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpPost]
		public async Task<IActionResult> EditProfile(string submitButton, UserProfileViewModel userProfilevm, IFormFile fileInput)
		{
			if (submitButton == "imgSubmit")
			{
				if (fileInput != null && fileInput.Length > 0)
				{
					using (var memoryStream = new MemoryStream())
					{
						fileInput.CopyTo(memoryStream);

						userProfilevm.FeaturedImage = ImageUtil.CropToSquare(memoryStream.ToArray());
					}
					var user = await userManager.FindByIdAsync(userProfilevm.UserId.ToString());
					string profileUrl = Url.Action("Profile", "Account", new { userName = user.UserName });
					var newFeed = new Feed()
					{
						DateAdded = DateTime.UtcNow,
						Body = "<p><a href=\"" + profileUrl + "\">" + user.UserName + "</a> si zmenil profilovú fotku</p>",
					};
					await feedRepository.AddAsync(newFeed);

					await userProfileRepository.UpdateAsync(userProfilevm.GetUserProfile());
				}
				return View(userProfilevm);
			}
			else if (submitButton == "formSubmit")
			{
				await userProfileRepository.UpdateAsync(userProfilevm.GetUserProfile());
				var user = await userManager.FindByIdAsync(userProfilevm.UserId.ToString());
				string profileUrl = Url.Action("Profile", "Account", new { userName = user.UserName });
				if (userProfilevm.PhoneNumber != user.PhoneNumber)
				{
					user.PhoneNumber = userProfilevm.PhoneNumber;
					await userManager.UpdateAsync(user);
					var phonefeed = new Feed()
					{
						DateAdded = DateTime.UtcNow,
						Body = "<p><a href=\"" + profileUrl + "\">" + user.UserName + "</a> si zmenil telefónne číslo</p>",
					};
					await feedRepository.AddAsync(phonefeed);
				}

				var newFeed = new Feed()
				{
					DateAdded = DateTime.UtcNow,
					Body = "<p><a href=\"" + profileUrl + "\">" + user.UserName + "</a> si upravil osobné informácie</p>",
				};
				await feedRepository.AddAsync(newFeed);
				return RedirectToAction("Profile", "Account", new { userName = user.UserName });
			}
			else if (submitButton == "passSubmit")
			{
				var user = await userManager.FindByIdAsync(userProfilevm.UserId.ToString());
				if (!string.IsNullOrEmpty(userProfilevm.Password))
				{
					user.PasswordHash = userManager.PasswordHasher.HashPassword(user, userProfilevm.Password);
					var updatedUser = await userManager.UpdateAsync(user);
					await signInManager.SignOutAsync();
					return RedirectToAction("Login", "Account");
				}
			}

			return View(userProfilevm);
		}
	}
}
