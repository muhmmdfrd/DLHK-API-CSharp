using Newtonsoft.Json;

namespace Core.Manager.ItemManager
{
	public class ItemDTO
	{
		[JsonProperty("itemId")]
		public long ItemId { get; set; }

		[JsonProperty("itemCode")]
		public string ItemCode { get; set; }

		[JsonProperty("itemName")]
		public string ItemName { get; set; }

		[JsonProperty("itemPhoto")]
		public byte[] ItemPhoto { get; set; }

		[JsonProperty("qty")]
		public int? ItemQty { get; set; }

		[JsonProperty("note")]
		public string Note { get; set; }

		[JsonProperty("categoryId")]
		public long? CategoryId { get; set; }

		[JsonProperty("categoryName")]
		public string CategoryName { get; set; }

		[JsonProperty("suplierName")]
		public string SuplierName { get; set; }
	}
}
