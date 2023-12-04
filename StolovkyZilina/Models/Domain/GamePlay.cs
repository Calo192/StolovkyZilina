using System.Numerics;

namespace StolovkyZilina.Models.Domain
{
	public class GamePlay
	{
		public Guid Id { get; set; }
		public List<PlayerPlayResult> Results { get; set; }
		public Guid GameId { get; set; }
		public Game Game { get; set; }
		public Guid ContentId { get; set; }
		public Content Content { get; set; }
		public string? Desc { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public Location? Location { get; set; }
		public Guid? LocationId { get; set; }
		public Guid? EventId { get; set; }
		public Event? Event { get; set; }
	}
}
