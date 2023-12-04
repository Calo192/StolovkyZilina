using System.Net.WebSockets;

namespace StolovkyZilina.Util
{
	public static class Constants
	{
		// 
		public const string SuperAdminEmail = "superadmin@sa.com";

		// Registration
		public const int MinimumCharactersInPassword = 6;

		// Error hlasky
		public const string MandatoryFieldError = "Povinné pole";
		public const string MinimumCharactersInPasswordError = "Nesplnený minimálny počet znakov";
		public const string EmailFormatError = "Nesprávny email formát";
		public const string IncorrectPassError = "Nesprávne heslo";
		public const string NonExistingGame = "Neexistujúca hra";
		


		public const string UserP3 = "UserP3";
		public const string UserP2 = "UserP2";
		public const string UserP1 = "UserP1";
		public const string Admin = "Admin";
		public const string SuperAdmin = "SuperAdmin";

		public static int GetRolePriority(string role)
		{
			switch (role)
			{
				case UserP3:
					return 1;
				case UserP2:
					return 2;
				case UserP1:
					return 3;
				case Admin:
					return 4;
				case SuperAdmin:
					return 5;
				default:
					return 0;
			}
		}

		public static string TranslateRole(string role)
		{
			switch (role)
			{
				case UserP3:
					return "Návštevník";
				case UserP2:
					return "Hráč";
				case UserP1:
					return "Organizátor";
				case Admin:
					return "Admin";
				case SuperAdmin:
					return "SuperAdmin";
				default:
					return "N/A";
			}
		}

		public static List<string> GetAllRoles(bool incSuperadmin)
		{
			var roles = new List<string>() { UserP3, UserP2, UserP1, Admin };
			if (incSuperadmin) roles.Add(SuperAdmin);
			return roles;
		}

		public static List<string> GetAllRolesTranslated(bool incSuperadmin)
		{
			var roles = new List<string>() { TranslateRole(UserP3), TranslateRole(UserP2), TranslateRole(UserP1), TranslateRole(Admin) };
			if (incSuperadmin) roles.Add(TranslateRole(SuperAdmin));
			return roles;
		}

		public static string GetHighestRole(List<string> roles)
		{
			if (roles.Any())
			{
				var highestRole = roles.First();

				foreach (var role in roles)
				{
					if (GetRolePriority(role) > GetRolePriority(highestRole))
					{
						highestRole = role;
					}
				}

				return highestRole;
			}

			return TranslateRole(null);
		}

		public static List<string> GetFittingRoles(string role)
		{
			var roles = new List<string>();

			if (!string.IsNullOrEmpty(role))
			{
				var priority = GetRolePriority(role);

				var allRoles = GetAllRoles(false);
				foreach (var r in allRoles)
				{
					if(GetRolePriority(r) <= priority)
					{
						roles.Add(r);
					}
				}
				return roles;
			}
            else
            {
				return null;
            }
		}

		public static readonly string[] ImageLinks =
		{
			"https://www.svgrepo.com/show/512065/emoji-sad-square-410.svg",
			"https://www.svgrepo.com/show/512063/emoji-sad-square-413.svg",
			"https://www.svgrepo.com/show/512012/emoji-neutral-square-406.svg",
			"https://www.svgrepo.com/show/511969/emoji-happy-square-414.svg",
			"https://www.svgrepo.com/show/511971/emoji-happy-square-411.svg"
		};
	}
}
