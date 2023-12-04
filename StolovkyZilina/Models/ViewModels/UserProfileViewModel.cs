using StolovkyZilina.Models.Domain;
using System.Text;

namespace StolovkyZilina.Models.ViewModels
{
	public class UserProfileViewModel
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Alias { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public string? City { get; set; }
		public string? Desc { get; set; }
		public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public byte[]? FeaturedImage { get; set; }
		public int? PrefferedDifficulty { get; set; }
		public int? PrefferedPlaytime { get; set; }
		public int? PrefferedPlayerCount { get; set; }
		public string Password { get; set; }
		public bool Competitive { get; set; }
		public List<GameOwner> GamesOwned { get; set; }

		public bool[] DaysAvailable { get; set; }

		public UserProfileViewModel()
		{
			DaysAvailable = new bool[7];
		}

		private string GetDaysAvailable()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (var d in DaysAvailable)
			{
				stringBuilder.Append(d ? "1" : "0");
			}
			return stringBuilder.ToString();
		}

		private void SetDaysAvailable(string days)
		{
			DaysAvailable = new bool[7];

			if (!string.IsNullOrEmpty(days))
			{
				// Convert each character to boolean and store in the array
				for (int i = 0; i < days.Length; i++)
				{
					DaysAvailable[i] = days[i] == '1';
				}
			}
		}

		public UserProfileViewModel(UserProfile userProfile, string? email, string? phoneNumber, string alias)
		{
			this.Id = userProfile.Id;
			this.UserId = userProfile.UserId;
			this.Alias = alias;
            this.Name = userProfile.Name;
			this.Surname = userProfile.Surname;
			this.PrefferedPlaytime = userProfile.PrefferedPlaytime;
			this.PrefferedPlayerCount = userProfile.PrefferedPlayerCount;
			this.PrefferedDifficulty = userProfile.PrefferedDifficulty;
			this.FeaturedImage = userProfile.FeaturedImage;
			this.GamesOwned = userProfile.GamesOwned;
			this.Email = email;
			this.PhoneNumber = phoneNumber;
			this.City = userProfile.City;
			this.Desc = userProfile.Desc;
			this.Competitive = userProfile.Competitive;
			SetDaysAvailable(userProfile.PrefferedPlayDay);
		}

		public UserProfile GetUserProfile()
		{
			return new UserProfile()
			{
				Id = this.Id,
				UserId = this.UserId,
				Name = this.Name,
				Surname = this.Surname,
				PrefferedPlaytime = this.PrefferedPlaytime,
				PrefferedPlayerCount = this.PrefferedPlayerCount,
				PrefferedDifficulty = this.PrefferedDifficulty,
				FeaturedImage = this.FeaturedImage,
				GamesOwned = this.GamesOwned,
				City = this.City,
				Desc = this.Desc,
				Competitive = this.Competitive,
				PrefferedPlayDay = GetDaysAvailable()
			};
		}
	}
}
