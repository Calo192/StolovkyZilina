namespace StolovkyZilina.Models.Domain
{
	public class AdminMessage
	{
		public Guid Id { get; set; }
		public string Message { get; set; }
		public string Subject { get; set; }
		public string Author { get; set; }
		public int Status { get; set; }
        public int Type { get; set; }
        public DateTime Date { get; set; }
    }
}
