namespace StolovkyZilina.Models.Domain
{
	public class PlayerMmr
	{
		public Guid Id { get; set; }
		public double Mmr { get; set; }
		public Guid UserId { get; set; }
		public Guid GameId { get; set; }
		public UserProfile User { get; set; }
		public Game Game { get; set; }
	}
}
