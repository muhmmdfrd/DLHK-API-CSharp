using Newtonsoft.Json;

namespace Core.Manager.UserManager
{
	public class UserDTO
	{
		[JsonProperty("userId")]
		public long UserId { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonProperty("personName")]
		public string PersonName { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("zoneName")]
		public string ZoneName { get; set; }

		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		[JsonProperty("photo")]
		public byte[] Photo { get; set; }

		[JsonProperty("shift")]
		public string Shift { get; set; }
	}
}