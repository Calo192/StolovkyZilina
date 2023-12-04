using StolovkyZilina.Util;
using System.ComponentModel.DataAnnotations;

namespace StolovkyZilina.Models.ViewModels
{
	public class RegisterViewModel
	{
		[Required (ErrorMessage = Constants.MandatoryFieldError)]
		public string Username { get; set; }

        [Required (ErrorMessage = Constants.MandatoryFieldError)]
		[EmailAddress(ErrorMessage = Constants.EmailFormatError)]
		public string Email { get; set; }

        [Required]
		[MinLength(Constants.MinimumCharactersInPassword, ErrorMessage = Constants.MinimumCharactersInPasswordError)]
        public string Password { get; set; }
	}
}
