using Newtonsoft.Json;

namespace Core.Manager.ImeiManager
{
	public class ImeiDTO
	{
		[JsonProperty("ImeiId")]
		public long ImeiId { get; set; }

		[JsonProperty("ImeiName")]
		public string ImeiName { get; set; }

		[JsonProperty("device")]
		public string Device { get; set; }
	}
}
