using Newtonsoft.Json;

namespace Core.Manager.RoleManager
{
	public class RoleDTO
	{
		[JsonProperty("roleId")]
		public long RoleId { get; set; }

		[JsonProperty("roleName")]
		public string RoleName { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }
	}
}
