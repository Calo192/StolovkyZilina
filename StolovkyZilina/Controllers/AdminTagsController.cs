using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Data;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.Requests;
using StolovkyZilina.Models.ViewModels;
using StolovkyZilina.Repositories;

namespace StolovkyZilina.Controllers
{
    [Authorize(Roles = "Admin")]
	public class AdminTagsController : Controller
    {
        public ITagRepository TagRepository { get; }

        public AdminTagsController(ITagRepository tagRepository)
        {
            TagRepository = tagRepository;
        }

		[HttpGet]
		[ActionName("List")]
		public async Task<IActionResult> List()
		{
			var tags = await TagRepository.GetAllAsync();

			var tagsViewModel = new AdminTagsViewModel() { Tags = tags.ToList() };

			return View(tagsViewModel);
		}

		[HttpPost]
		[ActionName("List")]
		public async Task<IActionResult> List(AdminTagsViewModel adminTagsViewModel)
		{
			var name = adminTagsViewModel.Name;
			var dispName = adminTagsViewModel.DisplayName;
			if (string.IsNullOrEmpty(name))
			{
				name = dispName;
			}
			var tag = new Tag { Name = name, DisplayName = dispName };
			await TagRepository.AddAsync(tag);
			return RedirectToAction("List");
		}

		[HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await TagRepository.GetAsync(id);

            if (tag != null)
            {
                var editTagReq = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagReq);
            }

            return View(null);
        }

		[HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };

            var updatedTag = await TagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {

            }
            else
            {
                
            }

			return RedirectToAction("List");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(Guid id) 
        {
            var deletedTag = await TagRepository.DeleteAsync(id);

			return RedirectToAction("List");
		}
    }
}
