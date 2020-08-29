using Newtonsoft.Json;

namespace Core.Manager.SuplierManager
{
	public class SuplierDTO
	{
		[JsonProperty("suplierId")]
		public long SuplierId { get; set; }

		[JsonProperty("suplierName")]
		public string SuplierName { get; set; }

		[JsonProperty("address")]
		public string Address { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("note")]
		public string Note { get; set; }
	}
}
