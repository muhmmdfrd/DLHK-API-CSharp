using Newtonsoft.Json;

namespace Core.Manager.RegionManager
{
	public class RegionDTO
	{
		[JsonProperty("regionId")]
		public long RegionId { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }
	}
}
