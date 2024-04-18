using StolovkyZilina.Models.Domain;
using System.Linq.Expressions;

namespace StolovkyZilina.Models.ViewModels
{
	public class GameDetailsViewModel : ContentViewModel
	{
		public string Name { get; set; }
		public string? Desc { get; set; }
		public string? Category{ get; set; }
		public string? ShortDesc { get; set; }
		public int? Difficulty { get; set; }
		public int? Playtime { get; set; }
		public int? SpaceRequired { get; set; }
		public int MinPlayerCount { get; set; }
		public int MaxPlayerCount { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public string? Author { get; set; }
		public bool Approved { get; set; }
		public bool Favorite { get; set; }
		public bool Own { get; set; }
		public int Want { get; set; }
		public int WantToPlay { get; set; }
		public List<GameOwner> Relations { get; set; }
		public List<PlayViewModel> PlayViewModels { get; set; }

		public double GetDifficilty()
		{
			if (Difficulty != null)
			{
				return (((double)Difficulty+1) / ((double)5)) * 100;
			}
			return 50;
		}

		public double GetPlaytime()
		{
			if (Playtime != null)
			{
				return (((double)Playtime + 1) / ((double)5)) * 100;
			}
			return 50;
		}

		public double GetSpaceReq()
		{
			if (SpaceRequired != null)
			{
				return (((double)SpaceRequired + 1) / ((double)3)) * 100;
			}
			return 50;
		}

		public string GetDifficultyString()
		{
			switch (Difficulty)
			{
				case 0:
					return "Veľmi ľahká";
				case 1:
					return "Celkom ľahká";
				case 2:
					return "Akurát";
				case 3:
					return "Celkom ťažká";
				case 4:
					return "Veľmi ťažká";
			}
			return string.Empty;
		}

		public string GetPlaytimeString()
		{
			switch (Playtime)
			{
				case 0:
					return "Veľmi rýchla";
				case 1:
					return "Celkom rýchla";
				case 2:
					return "Akurát";
				case 3:
					return "Celkom dlhá";
				case 4:
					return "Veľmi dlhá";
			}
			return string.Empty;
		}

		public string GetSpaceReqString()
		{
			switch (SpaceRequired)
			{
				case 0:
					return "Malá";
				case 1:
					return "Stredná";
				case 2:
					return "Veľká";
			}
			return string.Empty;
		}
	}
}
