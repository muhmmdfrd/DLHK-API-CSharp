using Newtonsoft.Json;

namespace Core.Manager.ApplicantManager
{
	public class ApplicantDTO
	{
		[JsonProperty("applicantId")]
		public long ApplicantId { get; set; }

		[JsonProperty("applicantName")]
		public string ApplicantName { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }
	}
}
