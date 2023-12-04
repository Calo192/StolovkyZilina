namespace StolovkyZilina.Models.Domain
{
	public class PlayerPlayResult
	{
		public Guid Id { get; set; }
		public int Result { get; set; }
		public Guid? PlayerId { get; set; }
		public UserProfile? Player { get; set; }
		public string? GuestPlayerName { get; set; }
		public string? PlayerInfo { get; set; }
		public Guid GamePlayId { get; set; }
		public GamePlay GamePlay { get; set; }
	}
}
