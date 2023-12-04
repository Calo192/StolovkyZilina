namespace StolovkyZilina.Models.Domain
{
	public class Reaction
	{
		public Guid Id { get; set; }
		public Guid ContentId { get; set; }
		public Guid UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
