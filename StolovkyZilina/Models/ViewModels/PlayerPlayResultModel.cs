using StolovkyZilina.Util;
using System.ComponentModel.DataAnnotations;

namespace StolovkyZilina.Models.ViewModels
{
	public class PlayerPlayResultModel
	{
        public Guid Id { get; set; }
        public int Result { get; set; }
		public bool IsWinner { get; set; }
		[Required(ErrorMessage = Constants.MandatoryFieldError)]
		public string PlayerName { get; set; }
		public string? PlayerInfo { get; set; }

		public PlayerPlayResultModel()
		{
			Result = 0;
			PlayerName = string.Empty;
			PlayerInfo = string.Empty;
		}
	}
}
