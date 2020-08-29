using Newtonsoft.Json;

namespace Core.Manager.DrainageManager
{
	public class DrainageDTO
	{
		[JsonProperty("drainangeId")]
		public long DrainageId { get; set; }

		[JsonProperty("dicipline")]
		public int? Dicipline { get; set; }

		[JsonProperty("completeness")]
		public int? Completeness { get; set; }

		[JsonProperty("cleanliness")]
		public int? Cleanliness { get; set; }

		[JsonProperty("sediment")]
		public int? Sediment { get; set; }

		[JsonProperty("weed")]
		public int? Weed { get; set; }

		[JsonProperty("presenceId")]
		public long? PresenceId { get; set; }
	}
}
