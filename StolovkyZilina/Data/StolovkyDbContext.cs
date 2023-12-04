using Microsoft.EntityFrameworkCore;
using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Data
{
    public class StolovkyDbContext : DbContext
    {
        public StolovkyDbContext(DbContextOptions<StolovkyDbContext> options) : base(options)
        {
        }

		public DbSet<Content> Contents { get; set; }
		public DbSet<BlogPost> BlogPosts { get; set; }
		public DbSet<Tag> Tags { get; set; }
		public DbSet<Rating> Ratings { get; set; }
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Game> Games { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<GameOwner> Owners { get; set; }
        public DbSet<GameLanguage> Languages { get; set; }
		public DbSet<Location> Locations { get; set; }
		public DbSet<GamePlay> Plays { get; set; }
		public DbSet<PlayerPlayResult> PlayerPlayResults { get; set; }
		public DbSet<Feed> Feeds { get; set; }
		public DbSet<AdminMessage> AdminMessages { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<ParticipationVote> ParticipationVotes { get; set; }
		public DbSet<GamePoll> GamePolls { get; set; }
		public DbSet<GameVote> GameVotes { get; set; }
	}
}
