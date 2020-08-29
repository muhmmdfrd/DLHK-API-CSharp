using Newtonsoft.Json;
using System;

namespace Core.Manager.InterviewManager
{
	public class InterviewDTO
	{
		[JsonProperty("interviewId")]
		public long InterviewId { get; set; }

		[JsonProperty("interviewer")]
		public string Interviewer { get; set; }

		[JsonProperty("place")]
		public string Place { get; set; }

		[JsonProperty("dateOfInterview")]
		public DateTime? DateOfInterview { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }
	}
}
