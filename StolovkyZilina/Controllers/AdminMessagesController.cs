using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
	public class AdminMessagesController : Controller
	{
		private readonly IRepository<AdminMessage> adminMessageRepository;
		private readonly UserManager<IdentityUser> userManager;

		public AdminMessagesController(IRepository<AdminMessage> adminMessageRepository, UserManager<IdentityUser> userManager)
		{
			this.adminMessageRepository = adminMessageRepository;
			this.userManager = userManager;
		}

		[HttpGet]
		[ActionName("Send")]
		public IActionResult Send()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Send")]
		public async Task<IActionResult> Send(AdminMessageViewModel adminMessageViewModel)
		{
			var newAdminMessage = new AdminMessage();
			newAdminMessage.Message = adminMessageViewModel.Message;
			newAdminMessage.Date = DateTime.Now;
			newAdminMessage.Subject = adminMessageViewModel.Subject;
			newAdminMessage.Status = 0;
			newAdminMessage.Type = adminMessageViewModel.Type;
			newAdminMessage.Author = userManager.GetUserName(User);
			adminMessageRepository.AddAsync(newAdminMessage);

			return RedirectToAction("List", "AdminMessages");
		}

		[HttpGet]
		public async Task<IActionResult> List(int? status)
		{
			var messages = await adminMessageRepository.GetAllAsync();

			var messagesWM = new List<AdminMessageViewModel>();

			foreach (var message in messages)
			{
				if (status == null || message.Status == status)
				{
					messagesWM.Add(new AdminMessageViewModel(message));
				}
			}

			return View(messagesWM);
		}

		[HttpGet]
		[ActionName("Delete")]
		public async Task<IActionResult> Delete(Guid Id)
		{
			await adminMessageRepository.DeleteAsync(Id);

			return RedirectToAction("List");
		}

		[HttpGet]
		[ActionName("Detail")]
		public async Task<IActionResult> Detail(Guid Id)
		{
			var message = await adminMessageRepository.GetAsync(Id);

			return View(new AdminMessageViewModel(message));
		}

		[HttpPost]
		[ActionName("Detail")]
		public async Task<IActionResult> Detail(AdminMessageViewModel messageViewModel)
		{
			var message = await adminMessageRepository.UpdateAsync(messageViewModel.GetMessage());

			if (message == null)
			{
				return View(messageViewModel);
			}
			return RedirectToAction("List");
		}
	}
}
