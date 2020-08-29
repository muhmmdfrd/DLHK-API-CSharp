using Newtonsoft.Json;

namespace Core.Manager.GarbageManager
{
	public class GarbageDTO
	{
		[JsonProperty("garbageId")]
		public long GerbageId { get; set; }

		[JsonProperty("dicipline")]
		public int? Dicipline { get; set; }

		[JsonProperty("tps")]
		public int? TPS { get; set; }

		[JsonProperty("separation")]
		public int? Separation { get; set; }

		[JsonProperty("calculation")]
		public int? Calculation { get; set; }

		[JsonProperty("volumeOrganic")]
		public int? VolumeOfOrganic { get; set; }

		[JsonProperty("volumeAnorganic")]
		public int? VolumeOfAnorganic { get; set; }

		[JsonProperty("presenceId")]
		public long? PresenceId { get; set; }
	}
}
