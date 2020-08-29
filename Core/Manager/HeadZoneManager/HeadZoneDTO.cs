using Newtonsoft.Json;

namespace Core.Manager.HeadZoneManager
{
	public class HeadZoneDTO
	{
		[JsonProperty("headZoneId")]
		public long HeadOfZoneId { get; set; }

		[JsonProperty("diciplinePresence")]
		public int? DiciplinePresence { get; set; }

		[JsonProperty("firstSession")]
		public int? FirstSession { get; set; }

		[JsonProperty("secondSession")]
		public int? SecondSession { get; set; }

		[JsonProperty("thirdSession")]
		public int? ThirdSession { get; set; }

		[JsonProperty("cleanlinessZone")]
		public int? CleanlinessOfZone { get; set; }

		[JsonProperty("completenessTeam")]
		public int? CompletenessOfTeam { get; set; }

		[JsonProperty("dataOfGarbage")]
		public int? DataOfGarbage { get; set; }

		[JsonProperty("presenceId")]
		public long? PresenceId { get; set; }

		[JsonProperty("typeZone")]
		public string TypeZone { get; set; }
	}
}
