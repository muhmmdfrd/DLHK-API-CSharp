using Newtonsoft.Json;

namespace DLHK_API.Models
{
	public class ApiResponse<T> where T:class
	{
		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("messageCode")]
		public int MessageCode { get; set; }

		[JsonProperty("errorCode")]
		public int ErrorCode { get; set; }

		[JsonProperty("data")]
		public T Data { get; set; }
	}
}