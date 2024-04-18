namespace StolovkyZilina.Models.Domain
{
	public class Game
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
        public string? Desc { get; set; }
        public string? ShortDesc { get; set; }
        public int? Difficulty { get; set; }
		public int? Playtime { get; set; }
		public int? SpaceRequirement { get; set; }
		public int MinPlayerCount { get; set; }
		public int MaxPlayerCount { get; set; }
		public bool Cooperative { get; set; }
		public bool OnPoints { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public string UrlHandle { get; set; }
		public string? Author { get; set; }
		public bool Approved { get; set; }
		public Guid ContentId { get; set; }
		public Content Content { get; set; }
		public Guid? GameCategoryId { get; set; }
		public GameCategory? GameCategory { get; set; }
		public List<GameOwner> Owners { get; set; }
		public List<GamePlay> Plays { get; set; }
		public ICollection<GamePoll> GamePolls { get; set; }
	}
}
