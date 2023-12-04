using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using StolovkyZilina.Models.Domain;
using StolovkyZilina.Models.Requests;
using StolovkyZilina.Repositories;
using System.Text.RegularExpressions;

namespace StolovkyZilina.Controllers
{
    public class InfoController : Controller
	{
        private readonly IRepository<Location> repository;
        public InfoController(IRepository<Location> repository)
        {
            this.repository = repository;
        }

        [HttpGet]
		public IActionResult Info()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> List() 
		{
			var locations = await repository.GetAllAsync();

			return View(locations);
		}

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new AddLocationRequest();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLocationRequest addLocationRequest)
        {
			string pattern = @"width=""\d+"" height=""\d+""";

			// Define the replacement string with the new width and height values
			string replacement = @"width=""100%"" height=""500"" class=""p-3""";

			var location = new Location()
            {
                Name = addLocationRequest.Name,
                ShortDesc = addLocationRequest.ShortDesc,
                PostalCode = addLocationRequest.PostalCode,
                Street = addLocationRequest.Street,
                BuildingNumber = addLocationRequest.BuildingNumber,
                DoorNumer = addLocationRequest.DoorNumer,
                FloorNumber = addLocationRequest.FloorNumber,
                City = addLocationRequest.City,
                Space = addLocationRequest.Space,
                UrlHandle = addLocationRequest.Name.Replace(' ', '-'),
                GoogleMapsLink = Regex.Replace(addLocationRequest.GoogleMapsLink, pattern, replacement)
            };

            await repository.AddAsync(location);

            return RedirectToAction("List");
        }
    }
}
