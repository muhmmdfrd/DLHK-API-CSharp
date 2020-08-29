using Newtonsoft.Json;
using System;

namespace Core.Manager.CoordinatorManager
{
	public class CoordinatorDTO
	{
		[JsonProperty("coordinatorId")]
		public long CoordinatorId { get; set; }

		[JsonProperty("percentOfPresence")]
		public int? PercentOfPresence { get; set; }

		[JsonProperty("percentOfReport")]
		public int? PercentOfReport { get; set; }

		[JsonProperty("percentOfCompletion")]
		public int? PercentOfCompletion { get; set; }

		[JsonProperty("percentOfSatisfaction")]
		public int? PercentOfSatisfaction { get; set; }

		[JsonProperty("cleanliness")]
		public int? Cleanliness { get; set; }

		[JsonProperty("dataOfGarbage")]
		public int? DataOfGarbage { get; set; }

		[JsonProperty("employeeId")]
		public long? EmployeeId { get; set; }

		[JsonIgnore]
		public DateTime DateOfAssessment { get; set; } 
	}
}
