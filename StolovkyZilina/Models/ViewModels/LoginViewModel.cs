using StolovkyZilina.Util;
using System.ComponentModel.DataAnnotations;

namespace StolovkyZilina.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = Constants.MandatoryFieldError)]
		public string Username { get; set; }
		[Required(ErrorMessage = Constants.MandatoryFieldError)]
		public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool RepeatedAttempt { get; set; }
	}
}
