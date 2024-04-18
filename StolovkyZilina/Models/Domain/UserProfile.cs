using Microsoft.AspNetCore.Identity;

namespace StolovkyZilina.Models.Domain
{
	public class UserProfile
	{
        public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public int? PrefferedDifficulty { get; set; }
		public int? PrefferedPlaytime { get; set; }
		public string? PrefferedPlayDay { get; set; }
		public int? PrefferedPlayerCount { get; set; }
		public int Influence { get; set; }
		public string? City { get; set; }
		public string? Desc { get; set; }
        public List<GameOwner> GamesOwned { get; set; }
		public bool Competitive { get; set; }
	}
}
