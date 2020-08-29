using Newtonsoft.Json;

namespace Core.Manager.ZoneManager
{
	public class ZoneDTO
	{
		[JsonProperty("zoneId")]
		public long ZoneId { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("regionId")]
		public long? RegionId { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }
	}
}
