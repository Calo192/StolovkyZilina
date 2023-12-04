using StolovkyZilina.Models.Domain;

namespace StolovkyZilina.Models.ViewModels
{
	public class PlayViewModel : ContentViewModel
	{
		public string GameName { get; set; }
		public Guid GameId { get; set; }
		public string GameUrlHandle { get; set; }
		public string? Desc { get; set; }
		public byte[]? GameFeaturedImage { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public Location? Location { get; set; }
		public bool IsCoopGame { get; set; }
		public bool IsOnPointsGame { get; set; }
		public List<PlayerRecord> Players { get; set; }

		public PlayViewModel()
		{
			Players = new List<PlayerRecord>();
		}

		public string GetBeforeTimeText()
		{
			if (StartTime != null && StartTime != default(DateTime))
			{
				var time = DateTime.Now - StartTime;
				if (time.Value.TotalHours < -2)
				{
					return "O " + ((int)time.Value.TotalHours)*(-1) + " hodín";
				}
				else if (time.Value.TotalMinutes < 0)
				{
					return "O chvíľu";
				}
				else if(time.Value.TotalHours < 2)
				{
					return "Pred chvíľou";
				}
				else if (time.Value.TotalDays < 2)
				{
					return "Pred " + (int)time.Value.TotalHours + " hodinami";
				}
				else
				{
					return "Pred " + (int)time.Value.TotalDays + " dňami";
				}
			}
			else
			{
				return string.Empty;
			}
		}

		public string GetState()
		{
			if (StartTime != null && StartTime != default(DateTime) && (EndTime == null || EndTime == default(DateTime)))
			{
				if (StartTime > DateTime.Now)
				{
					return GameState.Plánovaná.ToString();
				}
				else
				{
					return GameState.Rozohraná.ToString();
				}
			}
			else if (StartTime != null && StartTime != default(DateTime) && EndTime != null && EndTime != default(DateTime))
			{
				if (Players.Any(p => p.Score > 0))
				{
					return GameState.Ukončená.ToString();
				}
				else
				{
					return GameState.Nedohraná.ToString();
				}
			}
			return string.Empty;
		}
	}
	public class PlayerRecord
	{
		public string UserName { get; set; }
		public string Info { get; set; }
		public bool IsGuest { get; set; }
		public byte[]? PlayerImage { get; set; }
        public int Score { get; set; }
    }

	public enum GameState
	{
		Plánovaná,
		Rozohraná,
		Ukončená,
		Nedohraná
	}
}
