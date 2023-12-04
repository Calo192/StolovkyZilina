namespace StolovkyZilina.Util
{
	public static class NumberToTextUtil
	{
		public static string ConditionToString(int condition)
		{
			switch (condition)
			{
				case 0:
					return "Velmi zly";
				case 1:
					return "Zly";
				case 2:
					return "Hratelny";
				case 3:
					return "Dobry";
				case 4:
					return "Velmi dobry";
			}
			return string.Empty;
		}

		public static string StatusToString(int status)
		{
			switch (status)
			{
				case 0: return "Nová";

				case 1: return "Aktívna";

				case 2: return "Vyriešená";
			}
			return string.Empty;
		}

		public static string MessageTypeToString(int type)
		{
			switch (type)
			{
				case 0: return "Bug";

				case 1: return "Požiadavka";

				case 2: return "Nápad";
			}
			return string.Empty;
		}
	}
}
