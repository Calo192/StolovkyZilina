using StolovkyZilina.Util;

namespace StolovkyZilina.Models.ViewModels
{
	public class UserViewModel
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string Password{ get; set; }
		public bool VerifiedEmail { get; set; }
		public string Role { get; set; }

		public string GetRoleTranslated()
		{
			return Constants.TranslateRole(Role);
		}
	}
}
