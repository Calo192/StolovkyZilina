using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class ContentViewModel
	{
		public Guid Id { get; set; }
		public Guid ContentId { get; set; }
		public string UrlHandle { get; set; }
		public ICollection<Tag> Tags { get; set; }
		public int[] TotalLikes { get; set; }
		public string CommentContent { get; set; }
		public IEnumerable<ContentCommentViewModel> Comments { get; set; }
	}
}
