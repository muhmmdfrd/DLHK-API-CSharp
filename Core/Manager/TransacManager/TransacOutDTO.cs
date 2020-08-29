using Newtonsoft.Json;
using System;

namespace Core.Manager.TransacManager
{
	public class TransacOutDTO
	{
		[JsonProperty("transacId")]
		public long TransacId { get; set; }

		[JsonProperty("itemCode")]
		public string ItemCode { get; set; }

		[JsonProperty("itemName")]
		public string ItemName { get; set; }

		[JsonProperty("qty")]
		public int? Qty { get; set; }

		[JsonProperty("note")]
		public string Note { get; set; }

		[JsonProperty("dataOfTransac")]
		public DateTime? DateTransac { get; set; }

		[JsonProperty("typeOfTransac")]
		public string TypeOfTransac { get; set; }

		[JsonProperty("userRecorder")]
		public string UserRecorder { get; set; }

		[JsonProperty("userRequest")]
		public string UserRequest { get; set; }
	}
}
