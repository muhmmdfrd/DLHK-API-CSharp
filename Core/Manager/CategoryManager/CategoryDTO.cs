using Newtonsoft.Json;

namespace Core.Manager.CategoryManager
{
	public class CategoryDTO
	{
		[JsonProperty("categoryId")]
		public long CategoryId { get; set; }

		[JsonProperty("categoryCode")]
		public string CategoryCode { get; set; }

		[JsonProperty("categoryName")]
		public string CategoryName { get; set; }
	}
}
