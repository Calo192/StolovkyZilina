using System.ComponentModel.DataAnnotations;

namespace StolovkyZilina.Models.Domain
{
	public class ParticipationVote
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid EventId { get; set; }
		public int VoteStatus { get; set; }
	}
}
