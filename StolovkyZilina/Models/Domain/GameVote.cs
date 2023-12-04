namespace StolovkyZilina.Models.Domain
{
	public class GameVote
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid GameId { get; set; }
		public Guid GamePollId { get; set; }
		public UserProfile User { get; set; }
		public Game Game { get; set; }
	}
}
