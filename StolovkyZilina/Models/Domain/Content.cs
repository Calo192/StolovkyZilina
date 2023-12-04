namespace StolovkyZilina.Models.Domain
{
	public class Content
	{
		public Guid Id { get; set; }
		public ICollection<Tag> Tags { get; set; }
		public ICollection<Rating> Likes { get; set; }
		public ICollection<Comment> Comments { get; set; }

        public Content()
        {
            Tags = new List<Tag>();
			Likes = new List<Rating>();
			Comments = new List<Comment>();
        }
    }
}
