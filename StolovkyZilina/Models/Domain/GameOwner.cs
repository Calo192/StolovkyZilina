namespace StolovkyZilina.Models.Domain
{
    public class GameOwner
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public Guid GameId { get; set; }
        public Guid? LanguageId { get; set; }
        public int? Condition { get; set; }
		public bool DeluxeComponents { get; set; }
		public bool PromoContent { get; set; }
		public bool Insert { get; set; }
		public bool IsOwner { get; set; }
		public bool IsFavorite { get; set; }
		public int BuyingInterest { get; set; }
		public int PlaiyngInterest { get; set; }
		public UserProfile Owner { get; set; }
		public Game BoardGame { get; set; }
		public GameLanguage? Language { get; set; }
	}
}
