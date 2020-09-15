using Newtonsoft.Json;
using System;

namespace Core.Manager.LeaveManager
{
	public class LeaveDTO
	{
		[JsonProperty("leaveId")]
		public long LeaveId { get; set; }

		[JsonProperty("personName")]
		public string PersonName { get; set; }

		[JsonProperty("phone")]
		public string Phone { get; set; }

		[JsonProperty("employeeNumber")]
		public string EmployeeNumber { get; set; }

		[JsonProperty("location")]
		public string Location { get; set; }

		[JsonProperty("dateOfLeave")]
		public DateTime? DateOfLeave { get; set; }

		[JsonProperty("desc")]
		public string Description { get; set; }

		[JsonProperty("leaveStatus")]
		public string LeaveStatus { get; set; }

		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }
	}
}
