using Microsoft.AspNetCore.Mvc.Rendering;
using StolovkyZilina.Models.ViewModels;

namespace StolovkyZilina.Models.Requests
{
    public class GamePlayRequest
    {
		public Guid Id { get; set; }
		public Guid ContentId { get; set; }
		public Guid? EventId { get; set; }
		public List<PlayerPlayResultModel> Results { get; set; }
        public string? Desc { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<string> Games { get; set; }
		public string SelectedGame { get; set; }
		public bool IsCoopGame { get; set; }
		public bool IsOnPointsGame { get; set; }
		public IEnumerable<SelectListItem>? Locations { get; set; }
        public string? SelectedLocationId { get; set; }
        public List<string> Users { get; set; }

        public GamePlayRequest()
        {
            Results = new List<PlayerPlayResultModel> { };
            Games = new List<string>();
            Users = new List<string>();
        }
    }
}
