using Newtonsoft.Json;

namespace Core.Manager.ImeiManager
{
	public class ImeiDTO
	{
		[JsonProperty("imeiId")]
		public long ImeiId { get; set; }

		[JsonProperty("imeiName")]
		public string ImeiName { get; set; }

		[JsonProperty("device")]
		public string Device { get; set; }
	}
}
