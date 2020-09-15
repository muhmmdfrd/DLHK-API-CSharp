using Newtonsoft.Json;

namespace Core.Manager.SweeperManager
{
	public class SweeperDTO
	{
		[JsonProperty("sweeperId")]
		public long SweeperId { get; set; }

		[JsonProperty("dicipline")]
		public int? Dicipline { get; set; }

		[JsonProperty("completeness")]
		public int? Completeness { get; set; }

		[JsonProperty("road")]
		public int? Road { get; set; }

		[JsonProperty("sidewalk")]
		public int? Sidewalk { get; set; }

		[JsonProperty("waterRope")]
		public int? WaterRope { get; set; }

		[JsonProperty("roadMedian")]
		public int? RoadMedian { get; set; }

		[JsonProperty("score")]
		public int Score { get; set; }

		[JsonProperty("presenceId")]
		public long? PresenceId { get; set; }

		[JsonProperty("location")]
		public string Location { get; set; }
	}
}
